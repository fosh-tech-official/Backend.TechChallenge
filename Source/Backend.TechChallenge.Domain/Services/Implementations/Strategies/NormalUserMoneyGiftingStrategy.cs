using Backend.TechChallenge.Domain.Entities;
using Backend.TechChallenge.Domain.Entities.Enums;
using Backend.TechChallenge.Domain.Services.Contracts.Strategies;

namespace Backend.TechChallenge.Domain.Services.Implementations.Strategies;

public class NormalUserMoneyGiftingStrategy : IMoneyGiftingStrategy
{
    public void Execute(User? user)
    {
        if (user == null)
            return;
        
        if (user.Money > 100)
            user.Money += user.Money * 0.12m;
        else if (user.Money <= 100 && user.Money > 10)
            user.Money += user.Money * 0.8m;
    }

    public UserType UserType => UserType.Normal;
}