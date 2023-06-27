using AW3.GR.OpenAI.Application.Common.Interfaces.Repositories;
using AW3.GR.OpenAI.Application.Modules.Authors.DTOs;
using AW3.GR.OpenAI.Domain.AuthorAggregate.ValueObjects;
using AW3.GR.OpenAI.Domain.Common.Errors;
using AW3.GR.OpenAI.Domain.Quotes;
using ErrorOr;
using MapsterMapper;
using MediatR;

namespace AW3.GR.OpenAI.Application.Modules.Authors.Commands.CreateQuote;

public class CreateQuoteCommandHandler : IRequestHandler<CreateQuoteCommand, ErrorOr<AuthorDTO>>
{
    private readonly IAuthorRepository _authorRepository;
    private readonly IMapper _mapper;

    public CreateQuoteCommandHandler(IAuthorRepository authorRepository, IMapper mapper)
    {
        _authorRepository = authorRepository;
        _mapper = mapper;
    }

    public async Task<ErrorOr<AuthorDTO>> Handle(CreateQuoteCommand request, CancellationToken cancellationToken)
    {
        var author = await _authorRepository.FirstOrDefaultAsync(a => a.Id == AuthorId.Create(request.AuthorId), cancellationToken);

        if (author is null)
            return Errors.Author.AuthorNotFound;

        var newQuote = Quote.Create(request.Quote.Content);
        author.AddQuote(newQuote);

        await _authorRepository.SaveChangesAsync(cancellationToken);

        return _mapper.Map<AuthorDTO>(author);
    }
}
