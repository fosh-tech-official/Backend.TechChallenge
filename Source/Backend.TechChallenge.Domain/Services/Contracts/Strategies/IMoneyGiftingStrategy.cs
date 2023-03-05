using Backend.TechChallenge.Domain.Entities;
using Backend.TechChallenge.Domain.Entities.Enums;

namespace Backend.TechChallenge.Domain.Services.Contracts.Strategies;

public interface IMoneyGiftingStrategy
{
    void Execute(User? user);

    UserType UserType { get; }
}