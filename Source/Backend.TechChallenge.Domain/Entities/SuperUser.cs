using Backend.TechChallenge.Domain.Entities.Enums;

namespace Backend.TechChallenge.Domain.Entities;

public class SuperUser : User
{
    public SuperUser(string name, string email, string address, string phone, decimal money)
        : base(name, email, address, phone, money)
    {
    }

    public override UserType UserType => UserType.SuperUser;
}