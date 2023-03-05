using Backend.TechChallenge.Application.Dtos;
using Backend.TechChallenge.Domain.Entities;
using Backend.TechChallenge.Domain.Entities.Enums;

namespace Backend.TechChallenge.Application.Mappers.Contracts;

public interface ICreateUserResponseDtoMapper
{
    CreateUserResponseDto Map(User? source, UserType userType);
}