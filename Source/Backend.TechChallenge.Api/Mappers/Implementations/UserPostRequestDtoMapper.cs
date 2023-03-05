using System;
using Backend.TechChallenge.Api.Dtos.User.Post;
using Backend.TechChallenge.Api.Mappers.Contracts;
using Backend.TechChallenge.Application.Dtos;

namespace Backend.TechChallenge.Api.Mappers.Implementations;

public class UserPostRequestDtoMapper : IUserPostRequestDtoMapper
{
    public CreateUserRequestDto Map(UserPostRequestDto source)
    {
        var result = source != null
            ? new CreateUserRequestDto
            {
                Name = source.Name,
                Address = source.Address,
                Email = source.Email,
                Phone = source.Phone,
                Money = string.IsNullOrEmpty(source.Money) ? 0 : decimal.Parse(source.Money),
                UserType = Enum.TryParse<UserTypeDto>(source.UserType, out var userTypeDto) ? userTypeDto : default
            }
            : null;

        return result;
    }
}