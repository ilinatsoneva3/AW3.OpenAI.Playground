using ErrorOr;

namespace AW3.GR.OpenAI.Domain.Common.Errors;

public static partial class Errors
{
    public static class Quote
    {
        public static Error InvalidQuestionType = Error.Validation(
            code: "Quotes.InvalidQuestionType",
            description: "You should search by either book or author name");
    }
}
