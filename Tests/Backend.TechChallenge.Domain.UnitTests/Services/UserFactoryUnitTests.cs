using System;
using System.Collections.Generic;
using Backend.TechChallenge.Domain.Entities;
using Backend.TechChallenge.Domain.Entities.Enums;
using Backend.TechChallenge.Domain.Services.Contracts.Strategies;
using Backend.TechChallenge.Domain.Services.Implementations;
using Backend.TechChallenge.Domain.Services.Implementations.Strategies;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using NSubstitute;
using Xunit;

namespace Backend.TechChallenge.Domain.UnitTests.Services;

public class UserFactoryUnitTests
{
    private IEnumerable<IMoneyGiftingStrategy> _moneyGiftingStrategiesMock;
    private readonly UserFactory _sut;
    private readonly NormalUserMoneyGiftingStrategy _normalUserMoneyGiftingStrategyMock;

    public UserFactoryUnitTests()
    {
        var logger = Substitute.For<ILogger<UserFactory>>();
        _normalUserMoneyGiftingStrategyMock = Substitute.For<NormalUserMoneyGiftingStrategy>();
        _moneyGiftingStrategiesMock = new List<IMoneyGiftingStrategy>
        {
            _normalUserMoneyGiftingStrategyMock,
            Substitute.For<PremiumUserMoneyGiftingStrategy>(),
            Substitute.For<SuperUserMoneyGiftingStrategy>()
        };

        _sut = new UserFactory(logger, _moneyGiftingStrategiesMock);
    }

    [Fact]
    public void Create_WithNullName_ShouldThrowArgumentNullException()
    {
        // Arrange & Act & Assert
        Assert.Throws<ArgumentNullException>(() => _sut.Create(null, "test@test.com", "123 Main St.", "555-1234", 100,
                                                 UserType.Normal));
    }

    [Fact]
    public void Create_WithNullEmail_ShouldThrowArgumentNullException()
    {
        // Arrange & Act & Assert
        Assert.Throws<ArgumentNullException>(() => _sut.Create("John", null, "123 Main St.", "555-1234", 100,
                                                 UserType.Normal));
    }

    [Fact]
    public void Create_WithNullAddress_ShouldThrowArgumentNullException()
    {
        // Arrange & Act & Assert
        Assert.Throws<ArgumentNullException>(() => _sut.Create("John", "test@test.com", null, "555-1234", 100,
                                                 UserType.Normal));
    }

    [Fact]
    public void Create_WithNullPhone_ShouldThrowArgumentNullException()
    {
        // Arrange & Act & Assert
        Assert.Throws<ArgumentNullException>(() => _sut.Create("John", "test@test.com", "123 Main St.", null, 100,
                                                 UserType.Normal));
    }

    [Fact]
    public void Create_WithNullMoney_ShouldThrowArgumentNullException()
    {
        // Arrange & Act & Assert
        Assert.Throws<ArgumentNullException>(() => _sut.Create("John", "test@test.com", "123 Main St.", "555-1234", null,
                                                 UserType.Normal));
    }

    [Fact]
    public void Create_WithNullUserType_ShouldThrowArgumentNullException()
    {
        // Arrange & Act & Assert
        Assert.Throws<ArgumentNullException>(() => _sut.Create("John", "test@test.com", "123 Main St.", "555-1234", 100,
                                                 null));
    }

    [Fact]
    public void Create_WithInvalidUserType_ShouldThrowArgumentException()
    {
        // Arrange & Act & Assert
        Assert.Throws<ArgumentException>(() => _sut.Create("John", "test@test.com", "123 Main St.", "555-1234", 100,
                                             (UserType)99));
    }

    [Fact]
    public void Create_WithNormalUser_ShouldReturnNormalUser()
    {
        // Arrange & Act
        var result = _sut.Create("John", "john@test.com", "123 Main St", "555-5555", 100m, UserType.Normal);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType<NormalUser>();
        result.Name.Should().Be("John");
        result.Email.Should().Be("john@test.com");
        result.Address.Should().Be("123 Main St");
        result.Phone.Should().Be("555-5555");
        result.Money.Should().Be(100m * 0.8m + 100m);
    }

    [Fact]
    public void Create_WithPremiumUser_ShouldReturnPremiumUser()
    {
        // Arrange & Act
        var result = _sut.Create("Jane", "jane@test.com", "456 Elm St", "555-5555", 200m, UserType.Premium);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType<PremiumUser>();
        result.Name.Should().Be("Jane");
        result.Email.Should().Be("jane@test.com");
        result.Address.Should().Be("456 Elm St");
        result.Phone.Should().Be("555-5555");
        result.Money.Should().Be(200m * 2 + 200m);
    }

    [Fact]
    public void Create_WithSuperUser_ShouldReturnSuperUser()
    {
        // Arrange & Act
        var result = _sut.Create("Mike", "mike@test.com", "789 Oak St", "555-5555", 150m, UserType.SuperUser);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeOfType<SuperUser>();
        result.Name.Should().Be("Mike");
        result.Email.Should().Be("mike@test.com");
        result.Address.Should().Be("789 Oak St");
        result.Phone.Should().Be("555-5555");
        result.Money.Should().Be(150m * 0.2m + 150m);
    }

    [Fact]
    public void Create_WithNoMoneyGiftingStrategy_ShouldNotExecuteMoneyGifting()
    {
        // Arrange
        _moneyGiftingStrategiesMock = new List<IMoneyGiftingStrategy>();
        
        // Act
        _sut.Create("test", "test@test.com", "test address", "123456789", 100, UserType.Normal);

        // Assert
        _normalUserMoneyGiftingStrategyMock.Received(0)?.Execute(Arg.Any<User>());
    }

    [Fact]
    public void Create_WithMoneyGiftingStrategy_ShouldCallExecuteMethod()
    {
        // Arrange & Act
        _sut.Create("test", "test@test.com", "test address", "123456789", 100, UserType.Normal);

        // Assert
        _normalUserMoneyGiftingStrategyMock.Received().Execute(Arg.Any<User>());
    }
}