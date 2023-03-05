using Backend.TechChallenge.Api.Common.Models;
using Backend.TechChallenge.Api.Controllers.Entities;
using Backend.TechChallenge.Api.Domain.Model;

namespace Backend.TechChallenge.Api.Controllers
{
    public static class Converter
    {
        public static UserBase ToDomainModel(CreateUserRequest source)
        {
            UserBase target = null;
            
            switch (source.Type)
            {
                case UserType.Normal: target = new NormalUser(); break;
                case UserType.SuperUser: target = new SuperUser(); break;
                case UserType.Premium: target = new PremiumUser(); break;
            }

            target.Address = source.Address;
            target.Phone = source.Phone;
            target.Email = source.Email;
            target.Money = source.Money;
            target.Name = source.Name;

            return target;
        }
    }
}
