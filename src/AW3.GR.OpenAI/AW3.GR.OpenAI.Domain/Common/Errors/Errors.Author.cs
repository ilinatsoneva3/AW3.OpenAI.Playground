using ErrorOr;

namespace AW3.GR.OpenAI.Domain.Common.Errors;

public static partial class Errors
{
    public static class Author
    {
        public static Error AuthorNotFound => Error.NotFound(
            code: "Author.NotFound",
            description: "Author was not found");
    }
}
