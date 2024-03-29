﻿using AW3.GR.OpenAI.Domain.Common.Models;
using AW3.GR.OpenAI.Domain.Users.ValueObjects;

namespace AW3.GR.OpenAI.Domain.Users;

public class User : AggregateRoot<UserId, Guid>
{
    public string Username { get; private set; }

    public string Email { get; private set; }

    public string PasswordHash { get; private set; }

    public User(string username, string email, string password) : base(UserId.CreateUnique())
    {
        Username = username;
        Email = email;
        PasswordHash = password;
    }

    public static User Create(string username, string email, string password) => new(username, email, password);

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private User()
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    {
    }
}