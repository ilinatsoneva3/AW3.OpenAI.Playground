using AW3.GR.OpenAI.Domain.SearchHistories.Events;
using MediatR;

namespace AW3.GR.OpenAI.Application.Modules.Authors;

public class SearchHistoryCreatedEventHandler : INotificationHandler<SearchHistoryCreated>
{
    public Task Handle(SearchHistoryCreated notification, CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}
