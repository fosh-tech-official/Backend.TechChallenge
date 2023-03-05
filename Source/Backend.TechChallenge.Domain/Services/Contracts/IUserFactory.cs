using Backend.TechChallenge.Domain.Entities;
using Backend.TechChallenge.Domain.Entities.Enums;

namespace Backend.TechChallenge.Domain.Services.Contracts;

public interface IUserFactory
{
    User? Create(string? name, string? email, string? address, string? phone, decimal? money, UserType? userType);
}