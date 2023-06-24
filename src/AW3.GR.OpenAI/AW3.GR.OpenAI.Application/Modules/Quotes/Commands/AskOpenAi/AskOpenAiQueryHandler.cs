using AW3.GR.OpenAI.Application.Common.Interfaces.Repositories;
using AW3.GR.OpenAI.Application.Common.Interfaces.Services;
using AW3.GR.OpenAI.Application.Interfaces;
using AW3.GR.OpenAI.Domain.Common.Enums;
using AW3.GR.OpenAI.Domain.Common.Errors;
using ErrorOr;
using MediatR;

namespace AW3.GR.OpenAI.Application.Modules.Quotes.Commands.AskOpenAi;

internal sealed class AskOpenAiQueryHandler : IRequestHandler<AskOpenAiQuery, ErrorOr<AskOpenAiResponse>>
{
    private readonly ISearchHistoryRepository _searchHistoryRepository;
    private readonly IOpenAiClient _aiClient;
    private readonly IDateTimeProvider _dateTimeProvider;
    private readonly IUserContextService _userContextService;

    public AskOpenAiQueryHandler(
        ISearchHistoryRepository searchHistoryRepository,
        IOpenAiClient aiClient,
        IDateTimeProvider dateTimeProvider,
        IUserContextService userContextService)
    {
        _searchHistoryRepository = searchHistoryRepository;
        _aiClient = aiClient;
        _dateTimeProvider = dateTimeProvider;
        _userContextService = userContextService;
    }

    public async Task<ErrorOr<AskOpenAiResponse>> Handle(AskOpenAiQuery request, CancellationToken cancellationToken)
    {
        var userId = _userContextService.UserId;

        if (userId is null)
            return Errors.User.UserNotFound;

        var result = string.Empty;

        switch (request.Type.Name)
        {
            case nameof(OpenAiQuestionType.Book):
                result = await _aiClient.GetMostPopularQuoteBookNameAsync(request.Name);
                break;
            case nameof(OpenAiQuestionType.Author):
                result = await _aiClient.GetMostPopularQuoteByAuthorNameAsync(request.Name);
                break;
            default:
                break;
        }

        var searchHistoryEntryToAdd = Domain.SearchHistories.SearchHistory.Create(
            request.Type,
            request.Name,
            _dateTimeProvider.UtcNow,
            result,
            userId);

        _searchHistoryRepository.CreateSearchHistoryEntryAsync(searchHistoryEntryToAdd);

        return new AskOpenAiResponse(result);
    }
}