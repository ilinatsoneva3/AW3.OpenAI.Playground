using AW3.GR.OpenAI.Domain.Common.Models;

namespace AW3.GR.OpenAI.Domain.SearchHistories.Events;

public record SearchHistoryCreated(SearchHistory SearchHistory) : IDomainEvent;
