using Backend.TechChallenge.Api.Mappers.Contracts;
using Backend.TechChallenge.Api.Mappers.Implementations;
using Microsoft.Extensions.DependencyInjection;

namespace Backend.TechChallenge.Api.Ioc;

public static class DependenciesRegistrar
{
    public static IServiceCollection AddApi(this IServiceCollection services)
    {
        services.AddSingleton<IUserPostRequestDtoMapper, UserPostRequestDtoMapper>();
        services.AddSingleton<IUserPostResponseDtoMapper, UserPostResponseDtoMapper>();

        return services;
    }
}