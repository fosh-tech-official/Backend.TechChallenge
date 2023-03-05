using Backend.TechChallenge.Application.Dtos;
using Backend.TechChallenge.Application.Mappers.Contracts;
using Backend.TechChallenge.Domain.Entities;
using Backend.TechChallenge.Domain.Entities.Enums;

namespace Backend.TechChallenge.Application.Mappers.Implementations;

public class CreateUserResponseDtoMapper : ICreateUserResponseDtoMapper
{
    public CreateUserResponseDto Map(User? source, UserType userType)
    {
        if (source == default(User?))
            return new CreateUserResponseDto();

        var result = new CreateUserResponseDto
        {
            Name = source.Name,
            Address = source.Address,
            Email = source.Email,
            Money = source.Money,
            Phone = source.Phone,
            UserType = (UserTypeDto)userType
        };

        return result;
    }
}