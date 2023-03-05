using System.Threading.Tasks;
using Backend.TechChallenge.Api.Controllers;
using Backend.TechChallenge.Api.Dtos.Common;
using Backend.TechChallenge.Api.Dtos.User.Post;
using Backend.TechChallenge.Api.Mappers.Contracts;
using Backend.TechChallenge.Application.Dtos;
using Backend.TechChallenge.Application.Dtos.Enums;
using Backend.TechChallenge.Application.Services.Contracts;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using Xunit;

namespace Backend.TechChallenge.Api.UnitTests.Controllers
{
    public class UsersControllerUnitTests
    {
        private readonly UsersController _sut;
        private readonly IUserAppService _userAppServiceMock;
        private readonly IUserPostRequestDtoMapper _userPostRequestDtoMapperMock;
        private readonly IUserPostResponseDtoMapper _userPostResponseDtoMapperMock;

        public UsersControllerUnitTests()
        {
            _userAppServiceMock = Substitute.For<IUserAppService>();
            _userPostRequestDtoMapperMock = Substitute.For<IUserPostRequestDtoMapper>();
            _userPostResponseDtoMapperMock = Substitute.For<IUserPostResponseDtoMapper>();
            _sut = new UsersController(_userAppServiceMock, _userPostRequestDtoMapperMock, _userPostResponseDtoMapperMock);
        }

        [Fact]
        public async Task CreateUserAsync_WithValidRequest_ReturnsCreatedResultWithValidResponseDto()
        {
            // Arrange
            var expectedResponseDto = GetValidUserResponse();
            var userRequest = GetValidUserRequest();
            var createUserRequestDto = GetValidCreateUserRequest();
            var createUserResponseDto = GetValidCreateUserResponse();
            _userPostRequestDtoMapperMock.Map(Arg.Any<UserPostRequestDto>()).Returns(createUserRequestDto);
            _userAppServiceMock.CreateAsync(createUserRequestDto).Returns(createUserResponseDto);
            _userPostResponseDtoMapperMock.Map(createUserResponseDto).Returns(expectedResponseDto);

            // Action
            var result = await _sut.CreateUserAsync(userRequest);

            // Assert
            result.Should().BeAssignableTo<CreatedResult>();
            ((CreatedResult)result).Value.Should().Be(expectedResponseDto);
            _userPostRequestDtoMapperMock.Received();
            _userAppServiceMock.Received();
            _userPostResponseDtoMapperMock.Received();
        }

        [Fact]
        public async void CreateUserAsync_WhenUserIsDuplicated_ReturnsConflictResult()
        {
            // Arrange
            var userRequest = GetValidUserRequest();
            var createUserRequestDto = GetValidCreateUserRequest();
            var createUserResponseDto = GetDuplicatedCreateUserResponse();
            _userPostRequestDtoMapperMock.Map(Arg.Any<UserPostRequestDto>()).Returns(createUserRequestDto);
            _userAppServiceMock.CreateAsync(createUserRequestDto).Returns(createUserResponseDto);

            // Action
            var result = await _sut.CreateUserAsync(userRequest);

            // Assert
            result.Should().BeAssignableTo<ConflictObjectResult>();
            ((ErrorResponseDto)((ConflictObjectResult)result).Value)?.Errors.Should().Contain("The user is duplicated");
            _userPostRequestDtoMapperMock.Received();
            _userAppServiceMock.Received();
            _userPostResponseDtoMapperMock.DidNotReceive();
        }


        private static UserPostRequestDto GetValidUserRequest() =>
            new()
            {
                Name = "Mike",
                Email = "mike@gmail.com",
                Address = "Av. Juan G",
                Phone = "+349 1122354215",
                UserType = "Normal",
                Money = "123"
            };

        private static UserPostResponseDto GetValidUserResponse() =>
            new()
            {
                Name = "Mike",
                Email = "mike@gmail.com",
                Address = "Av. Juan G",
                Phone = "+349 1122354215",
                UserType = "Normal",
                Money = "123"
            };

        private static CreateUserRequestDto GetValidCreateUserRequest() =>
            new()
            {
                Name = "Mike",
                Email = "mike@gmail.com",
                Address = "Av. Juan G",
                Phone = "+349 1122354215",
                UserType = UserTypeDto.Normal,
                Money = 123
            };

        private static CreateUserResponseDto GetValidCreateUserResponse() =>
            new()
            {
                Name = "Mike",
                Email = "mike@gmail.com",
                Address = "Av. Juan G",
                Phone = "+349 1122354215",
                UserType = UserTypeDto.Normal,
                Money = 123,
                Result = CreateUserResponseResult.Created
            };

        private static UserPostResponseDto GetDuplicatedUserResponse() =>
            null;

        private static CreateUserResponseDto GetDuplicatedCreateUserResponse() =>
            new()
            {
                Result = CreateUserResponseResult.Duplicated
            };
    }
}