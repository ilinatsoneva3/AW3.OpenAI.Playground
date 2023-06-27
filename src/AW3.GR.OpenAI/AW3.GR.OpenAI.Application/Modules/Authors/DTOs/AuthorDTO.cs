namespace AW3.GR.OpenAI.Application.Modules.Authors.DTOs;

public record AuthorDTO(string Id, string FullName, IEnumerable<QuoteDTO> Quotes);
