using Backend.TechChallenge.Domain.Repositories.Contracts;
using Backend.TechChallenge.Infrastructure.File.Helpers.Contracts;
using Backend.TechChallenge.Infrastructure.File.Helpers.Implementations;
using Backend.TechChallenge.Infrastructure.File.Mappers.Contracts;
using Backend.TechChallenge.Infrastructure.File.Mappers.Implementations;
using Backend.TechChallenge.Infrastructure.File.Repositories.Implementations;
using Microsoft.Extensions.DependencyInjection;

namespace Backend.TechChallenge.Infrastructure.File.Ioc;

public static class DependenciesRegistrar
{
    public static IServiceCollection AddInfrastructureFile(this IServiceCollection services)
    {
        services.AddSingleton<IUserRepository, UserRepository>();
        services.AddSingleton<IUserFileMapper, UserFileMapper>();
        services.AddSingleton<IFilePathHelper, FilePathHelper>();

        return services;
    }
}