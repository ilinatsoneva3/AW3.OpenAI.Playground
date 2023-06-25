using AW3.GR.OpenAI.Application.Common.Interfaces.Repositories;
using AW3.GR.OpenAI.Application.Common.Interfaces.Services;
using AW3.GR.OpenAI.Application.Interfaces;
using AW3.GR.OpenAI.Domain.Common.Enums;
using AW3.GR.OpenAI.Domain.Common.Errors;
using ErrorOr;
using MediatR;

namespace AW3.GR.OpenAI.Application.Modules.Quotes.Commands.AskOpenAi;

internal sealed class OpenAiQueryHandler : IRequestHandler<OpenAiQuery, ErrorOr<OpenAiResponse>>
{
    private readonly ISearchHistoryRepository _searchHistoryRepository;
    private readonly IOpenAiClient _aiClient;
    private readonly IDateTimeProvider _dateTimeProvider;
    private readonly IUserContextService _userContextService;

    public OpenAiQueryHandler(
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

    public async Task<ErrorOr<OpenAiResponse>> Handle(OpenAiQuery request, CancellationToken cancellationToken)
    {
        var userId = _userContextService.UserId;

        if (userId is null)
            return Errors.User.UserNotFound;

        var result = string.Empty;

        OpenAiQuestionType.TryFromName(request.Type, out OpenAiQuestionType type);

        if (type is null)
            return Errors.Quote.InvalidQuestionType;

        switch (request.Type)
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
            type,
            request.Name,
            _dateTimeProvider.UtcNow,
            result,
            userId);

        _searchHistoryRepository.CreateSearchHistoryEntryAsync(searchHistoryEntryToAdd);

        return new OpenAiResponse(result);
    }
}