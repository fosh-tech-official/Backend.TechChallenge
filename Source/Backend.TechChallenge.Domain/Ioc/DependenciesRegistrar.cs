using Backend.TechChallenge.Domain.Services.Contracts;
using Backend.TechChallenge.Domain.Services.Contracts.Strategies;
using Backend.TechChallenge.Domain.Services.Implementations;
using Backend.TechChallenge.Domain.Services.Implementations.Strategies;
using Microsoft.Extensions.DependencyInjection;

namespace Backend.TechChallenge.Domain.Ioc;

public static class DependenciesRegistrar
{
    public static IServiceCollection AddDomain(this IServiceCollection services)
    {
        services.AddSingleton<IUserFactory, UserFactory>();
        services.AddSingleton<IMoneyGiftingStrategy, NormalUserMoneyGiftingStrategy>();
        services.AddSingleton<IMoneyGiftingStrategy, PremiumUserMoneyGiftingStrategy>();
        services.AddSingleton<IMoneyGiftingStrategy, SuperUserMoneyGiftingStrategy>();
        
        return services;
    }
}