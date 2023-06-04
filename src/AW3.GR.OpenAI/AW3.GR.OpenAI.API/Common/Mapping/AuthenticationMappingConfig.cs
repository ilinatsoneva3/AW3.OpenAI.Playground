using AW3.GR.OpenAI.Application.Modules.Authentication.Commands.Register;
using AW3.GR.OpenAI.Application.Modules.Authentication.Queries.Login;
using AW3.GR.OpenAI.Contracts.Authentication;
using Mapster;

namespace AW3.GR.OpenAI.API.Common.Mapping;

public class AuthenticationMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<RegisterResponse, AuthenticationResponse>()
            .Map(dest => dest, src => src.User)
            .Map(dest => dest.Token, src => src.Token);

        config.NewConfig<LoginResponse, AuthenticationResponse>()
            .Map(dest => dest, src => src.User)
            .Map(dest => dest.Token, src => src.Token);
    }
}
