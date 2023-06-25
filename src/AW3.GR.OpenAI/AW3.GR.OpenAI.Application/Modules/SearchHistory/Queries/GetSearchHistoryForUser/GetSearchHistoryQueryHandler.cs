using AW3.GR.OpenAI.Application.Common.Interfaces.Repositories;
using AW3.GR.OpenAI.Application.Common.Interfaces.Services;
using AW3.GR.OpenAI.Domain.Common.Errors;
using ErrorOr;
using MapsterMapper;
using MediatR;

namespace AW3.GR.OpenAI.Application.Modules.SearchHistory.Queries.GetSearchHistoryForUser;

public sealed class GetSearchHistoryQueryHandler
    : IRequestHandler<GetSearchHistoryQuery, ErrorOr<List<SearchHistoryResponse>>>
{
    private readonly ISearchHistoryRepository _searchHistoryRepository;
    private readonly IUserContextService _userContextService;
    private readonly IMapper _mapper;

    public GetSearchHistoryQueryHandler(
        ISearchHistoryRepository searchHistoryRepository,
        IUserContextService userContextService,
        IMapper mapper)
    {
        _searchHistoryRepository = searchHistoryRepository;
        _userContextService = userContextService;
        _mapper = mapper;
    }

    public async Task<ErrorOr<List<SearchHistoryResponse>>> Handle(GetSearchHistoryQuery request, CancellationToken cancellationToken)
    {
        var userId = _userContextService.UserId;

        if (userId is null)
            return Errors.User.UserNotFound;

        var searchHistory = _searchHistoryRepository.GetAllByUserIdAsync(userId);

        return searchHistory
            .Select(_mapper.Map<SearchHistoryResponse>)
            .ToList();
    }
}
