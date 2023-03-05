using Backend.TechChallenge.Api.Mappers.Contracts;
using Backend.TechChallenge.Api.Mappers.Implementations;
using Backend.TechChallenge.Application.Dtos;
using FluentAssertions;
using Xunit;

namespace Backend.TechChallenge.Api.UnitTests.Mappers;

public class UserPostResponseDtoMapperTests
{
    private readonly IUserPostResponseDtoMapper _sut;

    public UserPostResponseDtoMapperTests()
    {
        _sut = new UserPostResponseDtoMapper();
    }
    [Fact]
    public void Map_WithNullSource_ReturnsNull()
    {
        // Arrange
        CreateUserResponseDto source = null;

        // Act
        var result = _sut.Map(source);

        // Assert
        result.Should().BeNull();
    }

    [Fact]
    public void Map_WithValidCreateUserResponseDto_ReturnsValidUserPostResponseDto()
    {
        // Arrange
        var createUserResponseDto = new CreateUserResponseDto
        {
            Name = "Àlex MM",
            Email = "alex@example.com",
            Address = "Calle Piruleta 123",
            Phone = "+34 874 999 123",
            UserType = UserTypeDto.Normal,
            Money = 100.50m
        };

        // Act
        var result = _sut.Map(createUserResponseDto);

        // Assert
        result.Should().NotBeNull();
        result.Name.Should().Be(createUserResponseDto.Name);
        result.Email.Should().Be(createUserResponseDto.Email);
        result.Address.Should().Be(createUserResponseDto.Address);
        result.Phone.Should().Be(createUserResponseDto.Phone);
        result.UserType.Should().Be("Normal");
        result.Money.Should().Be("100.50");
    }

    [Fact]
    public void Map_WithNullCreateUserResponseDto_ReturnsNull()
    {
        // Arrange
        CreateUserResponseDto createUserResponseDto = null;

        // Act
        var result = _sut.Map(createUserResponseDto);

        // Assert
        result.Should().BeNull();
    }

    [Fact]
    public void Map_WithCreateUserResponseDtoWithNullUserType_ReturnsUserPostResponseDtoWithNullUserType()
    {
        // Arrange
        var createUserResponseDto = new CreateUserResponseDto
        {
            Name = "Àlex MM",
            Email = "alex@example.com",
            Address = "Calle Piruleta 123",
            Phone = "+34 874 999 123",
            UserType = null,
            Money = 100.50m
        };

        // Act
        var result = _sut.Map(createUserResponseDto);

        // Assert
        result.Should().NotBeNull();
        result.Name.Should().Be(createUserResponseDto.Name);
        result.Email.Should().Be(createUserResponseDto.Email);
        result.Address.Should().Be(createUserResponseDto.Address);
        result.Phone.Should().Be(createUserResponseDto.Phone);
        result.UserType.Should().BeNull();
        result.Money.Should().Be("100.50");
    }

    [Fact]
    public void Map_WithCreateUserResponseDtoWithZeroMoney_ReturnsUserPostResponseDtoWithZeroMoney()
    {
        // Arrange
        var createUserResponseDto = new CreateUserResponseDto
        {
            Name = "Àlex MM",
            Email = "alex@example.com",
            Address = "Calle Piruleta 123",
            Phone = "+34 874 999 123",
            UserType = UserTypeDto.Normal,
            Money = 0m
        };

        // Act
        var result = _sut.Map(createUserResponseDto);

        // Assert
        result.Should().NotBeNull();
        result.Name.Should().Be(createUserResponseDto.Name);
        result.Email.Should().Be(createUserResponseDto.Email);
        result.Address.Should().Be(createUserResponseDto.Address);
        result.Phone.Should().Be(createUserResponseDto.Phone);
        result.UserType.Should().Be("Normal");
        result.Money.Should().Be("0.00");
    }

    [Fact]
    public void Map_WithCreateUserResponseDtoWithNegativeMoney_ReturnsUserPostResponseDtoWithZeroMoney()
    {
        // Arrange
        var source = new CreateUserResponseDto
        {
            Name = "Àlex MM",
            Email = "alex@example.com",
            Address = "Calle Piruleta 123",
            Phone = "+34 874 999 123",
            UserType = UserTypeDto.Normal,
            Money = -100.50m
        };

        // Act
        var result = _sut.Map(source);

        // Assert
        result.Should().NotBeNull();
        result.Money.Should().Be("-100.50");
    }

    [Fact]
    public void Map_WithCreateUserResponseDtoWithNullMoney_ReturnsUserPostResponseDtoWithZeroMoney()
    {
        // Arrange
        var source = new CreateUserResponseDto
        {
            Name = "Àlex MM",
            Email = "alex@example.com",
            Address = "Calle Piruleta 123",
            Phone = "+34 874 999 123",
            UserType = UserTypeDto.Normal,
            Money = null
        };

        // Act
        var result = _sut.Map(source);

        // Assert
        result.Should().NotBeNull();
        result.Money.Should().Be("0.00");
    }
}