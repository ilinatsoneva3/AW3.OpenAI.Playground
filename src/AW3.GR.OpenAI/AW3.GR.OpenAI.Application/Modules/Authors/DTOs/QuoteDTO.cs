using AW3.GR.OpenAI.Domain.Quotes.ValueObjects;

namespace AW3.GR.OpenAI.Application.Modules.Authors.DTOs;

public record QuoteDTO(QuoteId Id, string Content);
