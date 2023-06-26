using AW3.GR.OpenAI.Domain.Common.Models;

namespace AW3.GR.OpenAI.Domain.Quotes.Events;

public sealed record QuoteCreated(Quote Quote) : IDomainEvent;
