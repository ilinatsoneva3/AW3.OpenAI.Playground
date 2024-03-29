﻿using System.Security.Cryptography;
using AW3.GR.OpenAI.Application.Common.Interfaces.Services;
using AW3.GR.OpenAI.Infrastructure.Services.Settings;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.Extensions.Options;

namespace AW3.GR.OpenAI.Infrastructure.Services;

public class PasswordHasher : IPasswordHasher
{
    private readonly int _iterationCount;
    private readonly RandomNumberGenerator _rng;

    public PasswordHasher(IOptions<PasswordHashingOptions>? optionsAccessor = null)
    {
        var options = optionsAccessor?.Value;
        _iterationCount = options!.IterationCount;
        _rng = options.Rng;
    }

    public string HashPassword(string password)
    {
        var hashedPassword = HashPassword(
            password: password,
            prf: KeyDerivationPrf.HMACSHA512,
            saltSize: 128 / 8,
            numBytesRequested: 256 / 8);

        return Convert.ToBase64String(hashedPassword);
    }

    public bool VerifyPassword(string hashedPassword, string password)
    {
        byte[] decodedHashedPassword = Convert.FromBase64String(hashedPassword);

        if (decodedHashedPassword.Length == 0)
        {
            return false;
        }

        var result = VerifyHashedPassword(decodedHashedPassword, password, out int embeddedIterCount);
        return result && embeddedIterCount >= _iterationCount;
    }

    private static bool VerifyHashedPassword(byte[] hashedPassword, string password, out int iterCount)
    {
        iterCount = default;

        try
        {
            // Read header information
            iterCount = (int)ReadNetworkByteOrder(hashedPassword, 5);
            int saltLength = (int)ReadNetworkByteOrder(hashedPassword, 9);

            // Read the salt: must be >= 128 bits
            if (saltLength < 128 / 8)
            {
                return false;
            }
            byte[] salt = new byte[saltLength];
            Buffer.BlockCopy(hashedPassword, 13, salt, 0, salt.Length);

            // Read the subkey (the rest of the payload): must be >= 128 bits
            int subkeyLength = hashedPassword.Length - 13 - salt.Length;
            if (subkeyLength < 128 / 8)
            {
                return false;
            }
            byte[] expectedSubkey = new byte[subkeyLength];
            Buffer.BlockCopy(hashedPassword, 13 + salt.Length, expectedSubkey, 0, expectedSubkey.Length);

            // Hash the incoming password and verify it
            byte[] actualSubkey = KeyDerivation.Pbkdf2(password, salt, KeyDerivationPrf.HMACSHA512, iterCount, subkeyLength);

            return CryptographicOperations.FixedTimeEquals(actualSubkey, expectedSubkey);
        }
        catch
        {
            // This should never occur except in the case of a malformed payload, where
            // we might go off the end of the array. Regardless, a malformed payload
            // implies verification failed.
            return false;
        }
    }

    private byte[] HashPassword(
        string password,
        KeyDerivationPrf prf,
        int saltSize,
        int numBytesRequested)
    {
        byte[] salt = new byte[saltSize];
        _rng.GetBytes(salt);
        byte[] subkey = KeyDerivation.Pbkdf2(password, salt, prf, _iterationCount, numBytesRequested);

        var outputBytes = new byte[13 + salt.Length + subkey.Length];
        outputBytes[0] = 0x01; // format marker
        WriteNetworkByteOrder(outputBytes, 1, (uint)prf);
        WriteNetworkByteOrder(outputBytes, 5, (uint)_iterationCount);
        WriteNetworkByteOrder(outputBytes, 9, (uint)saltSize);
        Buffer.BlockCopy(salt, 0, outputBytes, 13, salt.Length);
        Buffer.BlockCopy(subkey, 0, outputBytes, 13 + saltSize, subkey.Length);
        return outputBytes;
    }

    private static void WriteNetworkByteOrder(byte[] buffer, int offset, uint value)
    {
        buffer[offset + 0] = (byte)(value >> 24);
        buffer[offset + 1] = (byte)(value >> 16);
        buffer[offset + 2] = (byte)(value >> 8);
        buffer[offset + 3] = (byte)(value >> 0);
    }

    private static uint ReadNetworkByteOrder(byte[] buffer, int offset)
    {
        return ((uint)(buffer[offset + 0]) << 24)
            | ((uint)(buffer[offset + 1]) << 16)
            | ((uint)(buffer[offset + 2]) << 8)
            | buffer[offset + 3];
    }
}
