using AW3.GR.OpenAI.Application.Modules.SearchHistory.Queries.GetSearchHistoryForUser;
using AW3.GR.OpenAI.Domain.SearchHistoryAggregate;
using Mapster;

namespace AW3.GR.OpenAI.Application.Mapping;

public class SearchHistoryMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<SearchHistory, SearchHistoryResponse>();
    }
}
