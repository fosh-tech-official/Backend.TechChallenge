using System.ComponentModel.DataAnnotations;
using Backend.TechChallenge.Api.Dtos.User.Post;
using FluentAssertions;
using Xunit;

namespace Backend.TechChallenge.Api.UnitTests.Dtos;

public class UserPostRequestDtoTests
{
    [Fact]
    public void WhenAllFieldsAreSet_ValidationShouldPass()
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

        var validationContext = new ValidationContext(userPostRequestDto);

        // Act
        var result = Validator.TryValidateObject(userPostRequestDto, validationContext, null, true);

        // Assert
        result.Should().BeTrue();
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    public void WhenNameIsNullOrEmpty_ValidationShouldFail(string name)
    {
        // Arrange
        var userPostRequestDto = new UserPostRequestDto
        {
            Name = name,
            Email = "alex@example.com",
            Address = "Calle Piruleta 123",
            Phone = "+34 874 999 123",
            UserType = "Normal",
            Money = "100.50"
        };

        var validationContext = new ValidationContext(userPostRequestDto);

        // Act
        var result = Validator.TryValidateObject(userPostRequestDto, validationContext, null, true);

        // Assert
        result.Should().BeFalse();
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    public void WhenEmailIsNullOrEmpty_ValidationShouldFail(string email)
    {
        // Arrange
        var userPostRequestDto = new UserPostRequestDto
        {
            Name = "Àlex MM",
            Email = email,
            Address = "Calle Piruleta 123",
            Phone = "+34 874 999 123",
            UserType = "Normal",
            Money = "100.50"
        };

        var validationContext = new ValidationContext(userPostRequestDto);

        // Act
        var result = Validator.TryValidateObject(userPostRequestDto, validationContext, null, true);

        // Assert
        result.Should().BeFalse();
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    public void WhenAddressIsNullOrEmpty_ValidationShouldFail(string address)
    {
        // Arrange
        var userPostRequestDto = new UserPostRequestDto
        {
            Name = "Àlex MM",
            Email = "alex@example.com",
            Address = address,
            Phone = "+34 874 999 123",
            UserType = "Normal",
            Money = "100.50"
        };

        var validationContext = new ValidationContext(userPostRequestDto);

        // Act
        var result = Validator.TryValidateObject(userPostRequestDto, validationContext, null, true);

        // Assert
        result.Should().BeFalse();
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    public void WhenPhoneIsNullOrEmpty_ValidationShouldFail(string phone)
    {
        // Arrange
        var userPostRequestDto = new UserPostRequestDto
        {
            Name = "Àlex MM",
            Email = "alex@example.com",
            Address = "Calle Piruleta 123",
            Phone = phone,
            UserType = "Normal",
            Money = "100.50"
        };

        var validationContext = new ValidationContext(userPostRequestDto);

        // Act
        var result = Validator.TryValidateObject(userPostRequestDto, validationContext, null, true);

        // Assert
        result.Should().BeFalse();
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    public void WhenUserTypeIsNullOrEmpty_ValidationShouldFail(string userType)
    {
        // Arrange
        var userPostRequestDto = new UserPostRequestDto
        {
            Name = "Àlex MM",
            Email = "alex@example.com",
            Address = "Calle Piruleta 123",
            Phone = "+34 874 999 123",
            UserType = userType,
            Money = "100.50"
        };

        var validationContext = new ValidationContext(userPostRequestDto);

        // Act
        var result = Validator.TryValidateObject(userPostRequestDto, validationContext, null, true);

        // Assert
        result.Should().BeFalse();
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("abc")]
    [InlineData("-1")]
    [InlineData("0")]
    public void WhenMoneyIsInvalid_ValidationShouldFail(string money)
    {
        // Arrange
        var userPostRequestDto = new UserPostRequestDto
        {
            Name = "Àlex MM",
            Email = "alex@example.com",
            Address = "Calle Piruleta 123",
            Phone = "+34 874 999 123",
            UserType = "Normal",
            Money = money
        };

        var validationContext = new ValidationContext(userPostRequestDto);

        // Act
        var result = Validator.TryValidateObject(userPostRequestDto, validationContext, null, true);

        // Assert
        result.Should().BeFalse();
    }

    [Theory]
    [InlineData("100")]
    [InlineData("100.50")]
    [InlineData("100,50")]
    public void WhenMoneyIsValid_ValidationShouldPass(string money)
    {
        // Arrange
        var userPostRequestDto = new UserPostRequestDto
        {
            Name = "Àlex MM",
            Email = "alex@example.com",
            Address = "Calle Piruleta 123",
            Phone = "+34 874 999 123",
            UserType = "Normal",
            Money = money
        };

        var validationContext = new ValidationContext(userPostRequestDto);

        // Act
        var result = Validator.TryValidateObject(userPostRequestDto, validationContext, null, true);

        // Assert
        result.Should().BeTrue();
    }
}