using AW3.GR.OpenAI.Application.Common.Interfaces.Repositories;
using AW3.GR.OpenAI.Application.Modules.Authors.DTOs;
using ErrorOr;
using MapsterMapper;
using MediatR;

namespace AW3.GR.OpenAI.Application.Modules.Authors.Queries.GetAuthors;

public sealed class GetAuthorsQueryHandler : IRequestHandler<GetAuthorsQuery, ErrorOr<IEnumerable<AuthorDTO>>>
{
    private readonly IAuthorRepository _authorRepository;
    private readonly IMapper _mapper;

    public GetAuthorsQueryHandler(IAuthorRepository authorRepository, IMapper mapper)
    {
        _authorRepository = authorRepository;
        _mapper = mapper;
    }

    public async Task<ErrorOr<IEnumerable<AuthorDTO>>> Handle(GetAuthorsQuery request, CancellationToken cancellationToken)
    {
        var authors = await _authorRepository.GetAllAsync(cancellationToken);
        return authors.Select(_mapper.Map<AuthorDTO>).ToList();
    }
}
