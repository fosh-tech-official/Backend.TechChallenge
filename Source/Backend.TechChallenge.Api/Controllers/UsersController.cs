using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Backend.TechChallenge.Api.Dtos.Common;
using Backend.TechChallenge.Api.Dtos.User.Post;
using Backend.TechChallenge.Api.Mappers.Contracts;
using Backend.TechChallenge.Application.Dtos.Enums;
using Backend.TechChallenge.Application.Services.Contracts;
using Microsoft.AspNetCore.Http;

namespace Backend.TechChallenge.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserAppService _userAppService;
        private readonly IUserPostRequestDtoMapper _userPostRequestDtoMapper;
        private readonly IUserPostResponseDtoMapper _userPostResponseDtoMapper;

        public UsersController(
            IUserAppService userAppService,
            IUserPostRequestDtoMapper userPostRequestDtoMapper,
            IUserPostResponseDtoMapper userPostResponseDtoMapper)
        {
            _userAppService = userAppService ?? throw new ArgumentNullException(nameof(userAppService));
            _userPostRequestDtoMapper =
                userPostRequestDtoMapper ?? throw new ArgumentNullException(nameof(userPostRequestDtoMapper));
            _userPostResponseDtoMapper =
                userPostResponseDtoMapper ?? throw new ArgumentNullException(nameof(userPostResponseDtoMapper));
        }

        [HttpPost]
        public async Task<IActionResult> CreateUserAsync([FromBody] UserPostRequestDto request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var requestDto = _userPostRequestDtoMapper.Map(request);
            var result = await _userAppService.CreateAsync(requestDto);

            switch (result.Result)
            {
                case CreateUserResponseResult.Created:
                    var response = _userPostResponseDtoMapper.Map(result);
                    return Created(string.Empty, response);

                case CreateUserResponseResult.Duplicated:
                    const string userDuplicatedMessage = "The user is duplicated";

                    return Conflict(new ErrorResponseDto(new[] { userDuplicatedMessage }));

                case CreateUserResponseResult.NotValid:
                    const string userNotValidMessage = "The user is not valid";

                    return Conflict(new ErrorResponseDto(new[] { userNotValidMessage }));

                case CreateUserResponseResult.UnhandledError:
                    const string unexpectedErrorMessage = "An unexpected error occurred";

                    ObjectResult errorObjectResult = new(unexpectedErrorMessage)
                    {
                        StatusCode = StatusCodes.Status500InternalServerError,
                        Value = new ErrorResponseDto(new[] { unexpectedErrorMessage })
                    };
                    return errorObjectResult;

                default:
                    return BadRequest();
            }
        }
    }
}