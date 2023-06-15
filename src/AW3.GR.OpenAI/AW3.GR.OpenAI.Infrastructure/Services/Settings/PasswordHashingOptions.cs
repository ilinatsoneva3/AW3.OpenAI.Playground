using System.Security.Cryptography;

namespace AW3.GR.OpenAI.Infrastructure.Services.Settings;

public class PasswordHashingOptions
{
    public const string TITLE = "PasswordHashing";

    private static readonly RandomNumberGenerator _defaultRng = RandomNumberGenerator.Create();

    public int IterationCount { get; set; } = 100_000;

    internal RandomNumberGenerator Rng { get; set; } = _defaultRng;
}
