using AW3.GR.OpenAI.Application.Common.Interfaces.Repositories;
using AW3.GR.OpenAI.Application.Common.Interfaces.Services;
using AW3.GR.OpenAI.Application.Interfaces;
using AW3.GR.OpenAI.Domain.Common.Errors;
using AW3.GR.OpenAI.Domain.Enums;
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

        if (userId == null)
            return Errors.User.UserNotFound;

        var result = request.Type switch
        {
            OpenAiQuestionType.Book => await _aiClient.GetMostPopularQuoteBookNameAsync(request.Name),
            OpenAiQuestionType.Author => await _aiClient.GetMostPopularQuoteByAuthorNameAsync(request.Name),
            _ => throw new NotImplementedException()
        };

        var searchHistoryEntryToAdd = Domain.Entities.SearchHistory.Create(
            request.Type,
            request.Name,
            _dateTimeProvider.UtcNow,
            result,
            userId);

        _searchHistoryRepository.CreateSearchHistoryEntryAsync(searchHistoryEntryToAdd);

        return new AskOpenAiResponse(result);
    }
}