using System.Globalization;
using Backend.TechChallenge.Domain.Entities;
using Backend.TechChallenge.Infrastructure.File.Mappers.Contracts;
using Backend.TechChallenge.Infrastructure.File.Models;

namespace Backend.TechChallenge.Infrastructure.File.Mappers.Implementations;

public class UserFileMapper : IUserFileMapper
{
    public string? Map(UserFile? userFile)
    {
        if (userFile == null)
            return default;

        var result =
            $"{userFile.Name},{userFile.Email},{userFile.Phone},{userFile.Address},{userFile.UserType},{userFile.Money.ToString(CultureInfo.InvariantCulture)}";
        return result;
    }

    public UserFile? Map(User? user)
    {
        if (user == null)
            return default;

        var result = new UserFile
        {
            Name = user.Name,
            Email = user.Email,
            Address = user.Address,
            Phone = user.Phone,
            UserType = user.UserType.ToString(),
            Money = user.Money
        };
        return result;
    }

    public UserFile? Map(string? userLine, char separator)
    {
        if (userLine == null)
            return default;

        var splits = userLine.Split(separator);

        if (splits.Length < 6)
            return default;

        if (!decimal.TryParse(splits[5], out var money))
            return default;

        var result = new UserFile
        {
            Name = splits[0],
            Email = splits[1],
            Phone = splits[2],
            Address = splits[3],
            UserType = splits[4],
            Money = money
        };
        return result;
    }

}