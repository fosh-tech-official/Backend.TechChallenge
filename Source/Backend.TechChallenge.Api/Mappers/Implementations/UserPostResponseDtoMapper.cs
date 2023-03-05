using System;
using System.Globalization;
using Backend.TechChallenge.Api.Dtos.User.Post;
using Backend.TechChallenge.Api.Mappers.Contracts;
using Backend.TechChallenge.Application.Dtos;

namespace Backend.TechChallenge.Api.Mappers.Implementations;

public class UserPostResponseDtoMapper : IUserPostResponseDtoMapper
{
    private const string MoneyFormat = "{0:F2}";

    public UserPostResponseDto Map(CreateUserResponseDto source)
    {
        if (source == null)
            return null;

        return new UserPostResponseDto
        {
            Name = source.Name,
            Address = source.Address,
            Email = source.Email,
            Phone = source.Phone,
            UserType = source.UserType == null
                ? null
                : Enum.GetName(typeof(UserTypeDto), source.UserType),
            Money = string.Format(CultureInfo.InvariantCulture, MoneyFormat, source.Money.GetValueOrDefault())
        };
    }
}