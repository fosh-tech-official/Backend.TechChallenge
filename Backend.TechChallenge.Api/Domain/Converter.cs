using Backend.TechChallenge.Api.Common.Models;
using Backend.TechChallenge.Api.Domain.Model;
using Backend.TechChallenge.Api.Repo.Entities;

namespace Backend.TechChallenge.Api.Domain
{
    public static class Converter
    {
        public static User ToStoreModel(UserBase source)
        {
            var target = new User
            {
                Address = source.Address,
                Email = source.Email,
                Money = source.Money,
                Name = source.Name,
                Phone = source.Phone
            };

            switch (source.Type) 
            {
                case UserType.Normal: target.Type = "Normal"; break;
                case UserType.SuperUser: target.Type = "SuperUser"; break;
                case UserType.Premium: target.Type = "Premium"; break;
            }

            return target;
        }
    }
}
