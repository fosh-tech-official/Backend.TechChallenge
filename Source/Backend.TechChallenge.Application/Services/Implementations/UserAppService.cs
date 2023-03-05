using Backend.TechChallenge.Application.Dtos;
using Backend.TechChallenge.Application.Dtos.Enums;
using Backend.TechChallenge.Application.Mappers.Contracts;
using Backend.TechChallenge.Application.Services.Contracts;
using Backend.TechChallenge.Domain.Entities;
using Backend.TechChallenge.Domain.Entities.Enums;
using Backend.TechChallenge.Domain.Repositories.Contracts;
using Backend.TechChallenge.Domain.Services.Contracts;
using Microsoft.Extensions.Logging;

namespace Backend.TechChallenge.Application.Services.Implementations;

public class UserAppService : IUserAppService
{
    private readonly IUserFactory _userFactory;
    private readonly ICreateUserResponseDtoMapper _createUserResponseDtoMapper;
    private readonly IUserRepository _userRepository;
    private readonly ILogger<UserAppService> _logger;

    public UserAppService(
        IUserFactory userFactory,
        ICreateUserResponseDtoMapper createUserResponseDtoMapper,
        IUserRepository userRepository,
        ILogger<UserAppService> logger)
    {
        _userFactory = userFactory ?? throw new ArgumentNullException(nameof(userFactory));
        _createUserResponseDtoMapper =
            createUserResponseDtoMapper ?? throw new ArgumentNullException(nameof(createUserResponseDtoMapper));
        _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task<CreateUserResponseDto> CreateAsync(CreateUserRequestDto? request)
    {
        CreateUserResponseDto result;

        try
        {
            var userType = (UserType?)request?.UserType;
            var user = _userFactory.Create(request?.Name, request?.Email, request?.Address, request?.Phone, request?.Money,
                userType);

            var users = await _userRepository.GetAllAsync();
            var existingUser = users.FirstOrDefault(IsDuplicate(user));
            if (existingUser == null)
            {
                await _userRepository.InsertAsync(user);

                _logger.LogInformation("New user '{Email}' created", request?.Email);

                result = _createUserResponseDtoMapper.Map(user, userType.GetValueOrDefault());
                result.Result = CreateUserResponseResult.Created;
            }
            else
            {
                _logger.LogWarning("User '{Email}' already exists", request?.Email);
                result = new CreateUserResponseDto { Result = CreateUserResponseResult.Duplicated };
            }
        }
        catch (ArgumentNullException ex)
        {
            _logger.LogWarning(ex, "User '{Email}' is not valid", request?.Email);
            result = new CreateUserResponseDto { Result = CreateUserResponseResult.NotValid };
        }
        catch (Exception ex)
        {
            _logger.LogWarning(ex, "There was an error while creating user '{Email}'", request?.Email);
            result = new CreateUserResponseDto { Result = CreateUserResponseResult.UnhandledError };
        }

        return result;
    }

    private static Func<User, bool> IsDuplicate(User? user)
    {
        return u => u.Email == user.Email
                    || u.Name == user.Name
                    || u.Phone == user.Phone
                    || u.Address == user.Address;
    }
}