using AW3.GR.OpenAI.Application.Modules.Authors.Queries.GetAuthors;
using AW3.GR.OpenAI.Domain.Authors;
using Mapster;

namespace AW3.GR.OpenAI.Application.Mapping;

public class AuthorMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<Author, GetAuthorsResponse>()
            .Map(dest => dest.FullName, src => src.GetFullName());
    }
}
