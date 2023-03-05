using Backend.TechChallenge.Application.Dtos;

namespace Backend.TechChallenge.Application.Services.Contracts;

public interface IUserAppService
{
    Task<CreateUserResponseDto> CreateAsync(CreateUserRequestDto? request);
}