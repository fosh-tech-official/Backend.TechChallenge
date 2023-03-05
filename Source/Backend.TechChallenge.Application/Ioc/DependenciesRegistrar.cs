using Backend.TechChallenge.Application.Mappers.Contracts;
using Backend.TechChallenge.Application.Mappers.Implementations;
using Backend.TechChallenge.Application.Services.Contracts;
using Backend.TechChallenge.Application.Services.Implementations;
using Microsoft.Extensions.DependencyInjection;

namespace Backend.TechChallenge.Application.Ioc;

public static class DependenciesRegistrar
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddSingleton<IUserAppService, UserAppService>();
        services.AddSingleton<ICreateUserResponseDtoMapper, CreateUserResponseDtoMapper>();

        return services;
    }
}