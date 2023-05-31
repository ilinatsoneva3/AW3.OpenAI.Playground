using AW3.GR.OpenAI.Application.Common.Interfaces.Services;

namespace AW3.GR.OpenAI.Infrastructure.Services;

public class DateTimeProvider : IDateTimeProvider
{
    public DateTime UtcNow => DateTime.UtcNow;
}