using AW3.GR.OpenAI.Application.Modules.Authentication.Common;
using AW3.GR.OpenAI.Domain.Entities;
using Mapster;

namespace AW3.GR.OpenAI.API.Common.Mapping;

public class AuthenticationMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<User, UserDto>();
    }
}
