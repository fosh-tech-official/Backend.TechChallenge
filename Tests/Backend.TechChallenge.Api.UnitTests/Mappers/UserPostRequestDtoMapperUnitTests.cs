using Backend.TechChallenge.Api.Dtos.User.Post;
using Backend.TechChallenge.Api.Mappers.Contracts;
using Backend.TechChallenge.Api.Mappers.Implementations;
using Backend.TechChallenge.Application.Dtos;
using FluentAssertions;
using Xunit;

namespace Backend.TechChallenge.Api.UnitTests.Mappers;

public class UserPostRequestDtoMapperTests
{
    private readonly IUserPostRequestDtoMapper _sut;

    public UserPostRequestDtoMapperTests()
    {
        _sut = new UserPostRequestDtoMapper();
    }

    [Fact]
    public void Map_WithValidUserPostRequestDto_ShouldReturnCreateUserRequestDto()
    {
        // Arrange
        var userPostRequestDto = new UserPostRequestDto
        {
            Name = "Àlex MM",
            Email = "alex@example.com",
            Address = "Calle Piruleta 123",
            Phone = "+34 874 999 123",
            UserType = "Normal",
            Money = "100.50"
        };

        // Act
        var result = _sut.Map(userPostRequestDto);

        // Assert
        result.Should().NotBeNull();
        result.Name.Should().Be(userPostRequestDto.Name);
        result.Email.Should().Be(userPostRequestDto.Email);
        result.Address.Should().Be(userPostRequestDto.Address);
        result.Phone.Should().Be(userPostRequestDto.Phone);
        result.Money.Should().Be(decimal.Parse(userPostRequestDto.Money));
        result.UserType.Should().Be(UserTypeDto.Normal);
    }

    [Fact]
    public void Map_WithNullUserPostRequestDto_ShouldReturnNull()
    {
        // Arrange
        UserPostRequestDto userPostRequestDto = null;

        // Act
        var result = _sut.Map(userPostRequestDto);

        // Assert
        result.Should().BeNull();
    }

    [Fact]
    public void Map_WithEmptyMoney_ShouldReturnCreateUserRequestDtoWithZeroMoney()
    {
        // Arrange
        var userPostRequestDto = new UserPostRequestDto
        {
            Name = "Àlex MM",
            Email = "alex@example.com",
            Address = "Calle Piruleta 123",
            Phone = "+34 874 999 123",
            UserType = "Normal",
            Money = ""
        };

        // Act
        var result = _sut.Map(userPostRequestDto);

        // Assert
        result.Should().NotBeNull();
        result.Money.Should().Be(0);
    }

    [Fact]
    public void Map_WithInvalidUserType_ShouldReturnCreateUserRequestDtoWithDefaultUserType()
    {
        // Arrange
        var userPostRequestDto = new UserPostRequestDto
        {
            Name = "Àlex MM",
            Email = "alex@example.com",
            Address = "Calle Piruleta 123",
            Phone = "+34 874 999 123",
            UserType = "InvalidUserType",
            Money = "100.50"
        };

        // Act
        var result = _sut.Map(userPostRequestDto);

        // Assert
        result.Should().NotBeNull();
        result.UserType.Should().Be(default);
    }
}
