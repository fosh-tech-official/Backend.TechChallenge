using Backend.TechChallenge.Domain.Entities;
using Backend.TechChallenge.Infrastructure.File.Models;

namespace Backend.TechChallenge.Infrastructure.File.Mappers.Contracts;

public interface IUserFileMapper
{
    string? Map(UserFile? userFile);
    UserFile? Map(User? user);
    UserFile? Map(string? userLine, char separator);
}