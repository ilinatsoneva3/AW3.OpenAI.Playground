using AW3.GR.OpenAI.Application.Modules.Authors.DTOs;
using ErrorOr;
using MediatR;

namespace AW3.GR.OpenAI.Application.Modules.Authors.Queries.GetAuthors;

public record GetAuthorsQuery() : IRequest<ErrorOr<IEnumerable<AuthorDTO>>>;
