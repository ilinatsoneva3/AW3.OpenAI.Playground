using AW3.GR.OpenAI.Application.Common.Interfaces.Repositories;
using AW3.GR.OpenAI.Application.Common.Interfaces.Services;
using AW3.GR.OpenAI.Application.Interfaces;
using AW3.GR.OpenAI.Domain.Common.Errors;
using AW3.GR.OpenAI.Domain.SearchHistories.Events;
using ErrorOr;
using MediatR;

namespace AW3.GR.OpenAI.Application.Modules.Quotes.Commands.AskOpenAI;

public sealed class OpenAIQueryHandler : IRequestHandler<OpenAIQuery, ErrorOr<OpenAIResponse>>
{
    private readonly ISearchHistoryRepository _searchHistoryRepository;
    private readonly IOpenAIClient _aiClient;
    private readonly IDateTimeProvider _dateTimeProvider;
    private readonly IUserContextService _userContextService;

    public OpenAIQueryHandler(
        ISearchHistoryRepository searchHistoryRepository,
        IOpenAIClient aiClient,
        IDateTimeProvider dateTimeProvider,
        IUserContextService userContextService)
    {
        _searchHistoryRepository = searchHistoryRepository;
        _aiClient = aiClient;
        _dateTimeProvider = dateTimeProvider;
        _userContextService = userContextService;
    }

    public async Task<ErrorOr<OpenAIResponse>> Handle(OpenAIQuery request, CancellationToken cancellationToken)
    {
        var userId = _userContextService.UserId;

        if (userId is null)
            return Errors.User.UserNotFound;

        var result = await _aiClient.GetMostPopularQuoteByAuthorNameAsync(request.Name);

        var searchHistory = Domain.SearchHistories.SearchHistory.Create(
            request.Name,
            _dateTimeProvider.UtcNow,
            result,
            userId);

        searchHistory.AddDomainEvent(new SearchHistoryCreated(searchHistory));

        await _searchHistoryRepository.AddAsync(searchHistory, cancellationToken);

        return new OpenAIResponse(result);
    }
}