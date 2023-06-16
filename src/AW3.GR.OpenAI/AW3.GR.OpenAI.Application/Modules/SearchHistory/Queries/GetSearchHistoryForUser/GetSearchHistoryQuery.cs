using ErrorOr;
using MediatR;

namespace AW3.GR.OpenAI.Application.Modules.SearchHistory.Queries.GetSearchHistoryForUser;

public sealed record GetSearchHistoryQuery() : IRequest<ErrorOr<List<SearchHistoryResponse>>>;
