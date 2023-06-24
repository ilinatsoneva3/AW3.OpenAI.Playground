using Ardalis.SmartEnum;

namespace AW3.GR.OpenAI.Domain.Common.Enums;

public class OpenAiQuestionType : SmartEnum<OpenAiQuestionType>
{
    public OpenAiQuestionType(string name, int value) : base(name, value)
    {
    }

    public static readonly OpenAiQuestionType Book = new(nameof(Book), 0);
    public static readonly OpenAiQuestionType Author = new(nameof(Author), 1);

}