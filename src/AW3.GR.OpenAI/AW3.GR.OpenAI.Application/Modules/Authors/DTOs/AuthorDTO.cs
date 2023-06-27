using AW3.GR.OpenAI.Domain.AuthorAggregate.ValueObjects;

namespace AW3.GR.OpenAI.Application.Modules.Authors.DTOs;

public record AuthorDTO(AuthorId Id, string FullName, IEnumerable<QuoteDTO> Quotes);
