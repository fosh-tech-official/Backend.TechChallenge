using Backend.TechChallenge.Domain.Entities;
using Backend.TechChallenge.Domain.Entities.Enums;
using Backend.TechChallenge.Domain.Services.Contracts.Strategies;

namespace Backend.TechChallenge.Domain.Services.Implementations.Strategies;

public class PremiumUserMoneyGiftingStrategy : IMoneyGiftingStrategy
{
    public void Execute(User? user)
    {
        if (user == null)
            return;

        if (user.Money > 100)
            user.Money += user.Money * 2;
    }
    
    public UserType UserType => UserType.Premium;
}