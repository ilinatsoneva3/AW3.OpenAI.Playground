namespace AW3.GR.OpenAI.Application.UnitTests.Modules.TestUtils.Constants;

public static partial class Constants
{
    public static class Author
    {
        public static readonly string Id = "bb0931c7-8a3e-45fc-88e7-305d86eed3cc";

        public static readonly string InvalidId = "3f63f71b-377d-4bce-aac1-3fd8c753b012";

        public static readonly List<string> QuoteIdList = new()
        {
            "fa33ee4c-0a54-4f99-86b8-83ffad812f1f",
            "45c918e8-c80b-45d5-8052-6c207d5376b2",
            "8da66720-ff62-4549-a6ac-5ddce1152ffe"
        };

        public const string FirstName = "J.";

        public const string MiddleName = "K.";

        public const string LastName = "Rowling";

        public const string FullName = "J. K. Rowling";

        public const string QuoteContent = "Content";

        public static string QuoteContentFromIndex(int index)
            => $"{index} {QuoteContent}";

        public static string QuoteIdFromIndex(int index)
            => QuoteIdList[index];
    }
}
