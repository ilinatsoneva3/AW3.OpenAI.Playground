﻿using ErrorOr;

namespace AW3.GR.OpenAI.Domain.Common.Errors;

public static partial class Errors
{
    public static class User
    {
        public static Error DuplicateEmail => Error.Conflict(
            code: "User.DuplicateEmail",
            description: "Email already in use");

        public static Error UserNotFound => Error.NotFound(
            code: "User.NotFound",
            description: "User was not found");
    }
}
