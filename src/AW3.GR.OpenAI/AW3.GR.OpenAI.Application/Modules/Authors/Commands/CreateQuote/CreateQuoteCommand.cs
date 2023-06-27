using AW3.GR.OpenAI.Application.Modules.Authors.DTOs;
using ErrorOr;
using MediatR;

namespace AW3.GR.OpenAI.Application.Modules.Authors.Commands.CreateQuote;

public sealed record CreateQuoteCommand(string Content, Guid AuthorId)
    : IRequest<ErrorOr<AuthorDTO>>;