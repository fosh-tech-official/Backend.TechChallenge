using Backend.TechChallenge.Domain.Entities;
using Backend.TechChallenge.Domain.Entities.Enums;
using Backend.TechChallenge.Domain.Services.Contracts;
using Backend.TechChallenge.Domain.Services.Contracts.Strategies;
using Microsoft.Extensions.Logging;

namespace Backend.TechChallenge.Domain.Services.Implementations;

public class UserFactory : IUserFactory
{
    private readonly ILogger<UserFactory> _logger;
    private readonly Dictionary<UserType, IMoneyGiftingStrategy> _moneyGiftingStrategies;

    public UserFactory(ILogger<UserFactory> logger, IEnumerable<IMoneyGiftingStrategy> moneyGiftingStrategies)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _moneyGiftingStrategies = moneyGiftingStrategies.ToDictionary(strategy => strategy.UserType, strategy => strategy)
                                  ?? throw new ArgumentNullException(nameof(moneyGiftingStrategies));
    }

    public User? Create(string? name, string? email, string? address, string? phone, decimal? money, UserType? userType)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentNullException(nameof(name));

        if (string.IsNullOrWhiteSpace(email))
            throw new ArgumentNullException(nameof(email));

        if (string.IsNullOrWhiteSpace(address))
            throw new ArgumentNullException(nameof(address));

        if (string.IsNullOrWhiteSpace(phone))
            throw new ArgumentNullException(nameof(phone));

        if (money == default)
            throw new ArgumentNullException(nameof(money));

        if (userType == default)
            throw new ArgumentNullException(nameof(userType));

        User? result = userType switch
        {
            UserType.Normal => new NormalUser(name, email, address, phone, money.Value),
            UserType.Premium => new PremiumUser(name, email, address, phone, money.Value),
            UserType.SuperUser => new SuperUser(name, email, address, phone, money.Value),
            _ => throw new ArgumentException($"Invalid user type '{userType}'")
        };

        _moneyGiftingStrategies.TryGetValue(userType.Value, out var moneyGiftingStrategy);

        if (moneyGiftingStrategy == null)
            _logger.LogWarning("No strategy found for UserType '{UserType}'", userType);

        moneyGiftingStrategy?.Execute(result);

        return result;
    }
}