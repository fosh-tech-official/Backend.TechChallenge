using Backend.TechChallenge.Domain.Entities.Enums;

namespace Backend.TechChallenge.Domain.Entities;

public class PremiumUser : User
{
    public PremiumUser(string name, string email, string address, string phone, decimal money)
        : base(name, email, address, phone, money)
    {
    }

    public override UserType UserType => UserType.Premium;
}