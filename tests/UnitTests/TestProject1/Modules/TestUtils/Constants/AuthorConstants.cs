namespace AW3.GR.OpenAI.Application.UnitTests.Modules.TestUtils.Constants;

public static class AuthorConstants
{
    public static readonly string Id = "bb0931c7-8a3e-45fc-88e7-305d86eed3cc";

    public static readonly string InvalidId = "3f63f71b-377d-4bce-aac1-3fd8c753b012";


    public static readonly List<string> AuthorIdList = new()
        {
            "bb0931c7-8a3e-45fc-88e7-305d86eed3cc",
            "41a41abd-bd54-404b-834c-62793ad3e54c",
            "9f90049e-cfcd-494f-8b74-65d945ce0d4e"
        };

    public const string FirstName = "FirstName";

    public const string MiddleName = "MiddleName";

    public const string LastName = "LastName";

    public const string FullName = "FirstName MiddleName LastName";

    public static string AuthorFirstNameFromIndex(int index)
        => $"{index} {FirstName}";

    public static string AuthorMiddleNameFromIndex(int index)
        => $"{index} {MiddleName}";

    public static string AuthorLastNameFromIndex(int index)
        => $"{index} {LastName}";

    public static string AuthorFullNameFromIndex(int index)
        => $"{index} {FullName}";

    public static string AuthorIdFromIndex(int index)
        => AuthorIdList[index];

    public static readonly List<string> QuoteIdList = new()
        {
            "fa33ee4c-0a54-4f99-86b8-83ffad812f1f",
            "45c918e8-c80b-45d5-8052-6c207d5376b2",
            "8da66720-ff62-4549-a6ac-5ddce1152ffe"
        };

    public const string QuoteContent = "Content";

    public static string QuoteContentFromIndex(int index)
        => $"{index} {QuoteContent}";

    public static string QuoteIdFromIndex(int index)
        => QuoteIdList[index];
}
