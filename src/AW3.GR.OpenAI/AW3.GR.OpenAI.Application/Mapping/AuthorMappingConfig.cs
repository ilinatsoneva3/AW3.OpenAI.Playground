using AW3.GR.OpenAI.Application.Modules.Authors.DTOs;
using AW3.GR.OpenAI.Domain.Authors;
using Mapster;

namespace AW3.GR.OpenAI.Application.Mapping;

public class AuthorMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<Author, AuthorDTO>()
            .Map(dest => dest.FullName, src => src.GetFullName());
    }
}
