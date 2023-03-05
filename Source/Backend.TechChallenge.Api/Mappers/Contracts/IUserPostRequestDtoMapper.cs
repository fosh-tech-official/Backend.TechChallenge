using Backend.TechChallenge.Api.Dtos.User.Post;
using Backend.TechChallenge.Application.Dtos;

namespace Backend.TechChallenge.Api.Mappers.Contracts;

public interface IUserPostRequestDtoMapper
{
    CreateUserRequestDto Map(UserPostRequestDto source);
}