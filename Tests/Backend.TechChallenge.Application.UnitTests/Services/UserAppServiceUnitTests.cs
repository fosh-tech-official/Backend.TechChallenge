using System.Collections.Generic;
using System.Threading.Tasks;
using Backend.TechChallenge.Application.Dtos;
using Backend.TechChallenge.Application.Dtos.Enums;
using Backend.TechChallenge.Application.Mappers.Contracts;
using Backend.TechChallenge.Application.Services.Implementations;
using Backend.TechChallenge.Domain.Entities;
using Backend.TechChallenge.Domain.Entities.Enums;
using Backend.TechChallenge.Domain.Repositories.Contracts;
using Backend.TechChallenge.Domain.Services.Contracts;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using NSubstitute;
using Xunit;

namespace Backend.TechChallenge.Application.UnitTests.Services;

public class UserAppServiceUnitTests
{
    private readonly IUserFactory _userFactoryMock;
    private readonly ICreateUserResponseDtoMapper _createUserResponseDtoMapperMock;
    private readonly IUserRepository _userRepositoryMock;
    private readonly UserAppService _sut;

    public UserAppServiceUnitTests()
    {
        _userFactoryMock = Substitute.For<IUserFactory>();
        _createUserResponseDtoMapperMock = Substitute.For<ICreateUserResponseDtoMapper>();
        _userRepositoryMock = Substitute.For<IUserRepository>();
        var logger = Substitute.For<ILogger<UserAppService>>();

        _sut = new UserAppService(_userFactoryMock, _createUserResponseDtoMapperMock, _userRepositoryMock, logger);
    }

    [Fact]
    public async Task CreateAsync_WithValidRequest_ShouldCreateUserAndReturnSuccess()
    {
        // Arrange
        var request = new CreateUserRequestDto
        {
            Name = "John",
            Email = "john@example.com",
            Address = "Calle Piruleta 123",
            Phone = "+34 874 999 123",
            Money = 100.00m,
            UserType = (int)UserType.Normal
        };

        var user = new NormalUser(request.Name, request.Email, request.Address, request.Phone, request.Money.Value);
        _userFactoryMock.Create(request.Name, request.Email, request.Address, request.Phone, request.Money,
            (UserType?)request.UserType).Returns(user);

        _userRepositoryMock.GetAllAsync().Returns(new List<User>());

        var expectedResponse = new CreateUserResponseDto
        {
            Result = CreateUserResponseResult.Created
        };
        _createUserResponseDtoMapperMock.Map(user, UserType.Normal).Returns(expectedResponse);

        // Act
        var result = await _sut.CreateAsync(request);

        // Assert
        result.Should().BeEquivalentTo(expectedResponse);
        await _userRepositoryMock.Received().InsertAsync(user);
    }

    [Fact]
    public async Task CreateAsync_WithDuplicateUser_ShouldReturnDuplicateResponse()
    {
        // Arrange
        var request = new CreateUserRequestDto
        {
            Name = "Àlex MM",
            Email = "alex@example.com",
            Address = "Calle Piruleta 123",
            Phone = "+34 874 999 123",
            Money = 100.00m,
            UserType = (int)UserType.Normal
        };

        var user = new NormalUser(request.Name, request.Email, request.Address, request.Phone, request.Money.Value);
        _userFactoryMock.Create(request.Name, request.Email, request.Address, request.Phone, request.Money,
            (UserType?)request.UserType).Returns(user);

        var existingUser = new NormalUser(request.Name, request.Email, request.Address, request.Phone, request.Money.Value);
        _userRepositoryMock.GetAllAsync().Returns(new List<User> { existingUser });

        var expectedResponse = new CreateUserResponseDto
        {
            Result = CreateUserResponseResult.Duplicated
        };

        // Act
        var result = await _sut.CreateAsync(request);

        // Assert
        result.Should().BeEquivalentTo(expectedResponse);
        await _userRepositoryMock.DidNotReceiveWithAnyArgs().InsertAsync(null);
    }
    
    [Fact]
    public async Task CreateAsync_WithInvalidRequest_ShouldReturnNotValidResponse()
    {
        // Arrange
        var request = new CreateUserRequestDto
        {
            Email = "alex@example.com",
            Address = "Calle Piruleta 123",
            Phone = "+34 874 999 123",
            Money = -100 // This is an invalid value
        };

        // Act
        var result = await _sut.CreateAsync(request);

        // Assert
        result.Result.Should().Be(CreateUserResponseResult.NotValid);
        await _userRepositoryMock.DidNotReceive().InsertAsync(Arg.Any<User>());
    }
}