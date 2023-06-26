using AW3.GR.OpenAI.Domain.AuthorAggregate.ValueObjects;

namespace AW3.GR.OpenAI.Application.Modules.Authors.Queries.GetAuthors;

public record GetAuthorsResponse(AuthorId Id, string FullName);