using Backend.TechChallenge.Application.Dtos;
using Backend.TechChallenge.Application.Mappers.Contracts;
using Backend.TechChallenge.Application.Mappers.Implementations;
using Backend.TechChallenge.Domain.Entities;
using Backend.TechChallenge.Domain.Entities.Enums;
using FluentAssertions;
using Xunit;

namespace Backend.TechChallenge.Application.UnitTests.Mappers;

public class CreateUserResponseDtoMapperTests
{
    private readonly ICreateUserResponseDtoMapper _sut;

    public CreateUserResponseDtoMapperTests()
    {
        _sut = new CreateUserResponseDtoMapper();
    }

    [Fact]
    public void Map_WithNullSource_ReturnsEmptyDto()
    {
        // Act
        var result = _sut.Map(null, UserType.Normal);

        // Assert
        result.Should().NotBeNull();
        result.Name.Should().BeNull();
        result.Address.Should().BeNull();
        result.Email.Should().BeNull();
        result.Money.Should().BeNull();
        result.Phone.Should().BeNull();
        result.UserType.Should().BeNull();
    }

    [Fact]
    public void Map_WithValidSource_ReturnsMappedDto()
    {
        // Arrange
        var source = new NormalUser("Àlex MM", "alex@example.com", "Calle Piruleta 123", "+34 874 999 123", 100.0m);

        // Act
        var result = _sut.Map(source, UserType.Normal);

        // Assert
        result.Should().NotBeNull();
        result.Name.Should().Be("Àlex MM");
        result.Address.Should().Be("Calle Piruleta 123");
        result.Email.Should().Be("alex@example.com");
        result.Money.Should().Be(100.0m);
        result.Phone.Should().Be("+34 874 999 123");
        result.UserType.Should().Be(UserTypeDto.Normal);
    }
}
