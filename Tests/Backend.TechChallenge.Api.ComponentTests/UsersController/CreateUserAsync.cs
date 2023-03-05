using System.Net;
using System.Net.Mime;
using System.Text;
using System.Text.Json;
using Backend.TechChallenge.Api.Dtos.User.Post;
using Backend.TechChallenge.Domain.Entities;
using Backend.TechChallenge.Domain.Repositories.Contracts;
using FluentAssertions;
using NSubstitute;
using Xunit;

namespace Backend.TechChallenge.Api.ComponentTests.UsersController; 

[Collection(nameof(WebApplicationFactoryCollection))]
public class CreateUserAsync
{
    private readonly HttpClient _httpClient;
    private readonly IUserRepository _userRepository;

    public CreateUserAsync(ComponentFactory factory)
    {
        _httpClient = factory.CreateDefaultClient();

        factory.Mocks.Clear();
        _userRepository = factory.Mocks.Get<IUserRepository>();
    }

    [Fact]
    public async Task CreateUserAsync_WithValidRequest_ReturnsCreatedResult()
    {
        // Arrange
        var expectedResponse = HttpStatusCode.Created;
        _userRepository.GetAllAsync().Returns(new List<User>());

        // Action
        var response = await _httpClient.PostAsync("users", CreateBody(new UserPostRequestDto
        {
            Name = "Àlex MM",
            Email = "alex@example.com",
            Address = "Calle Piruleta 123",
            Phone = "+34 874 999 123",
            UserType = "Normal",
            Money = "542.32"
        }));

        // Assert
        response.StatusCode.Should().Be(expectedResponse);
    }

    [Fact]
    public async Task CreateUserAsync_WithValidDuplicatedUser_ReturnsConflictResult()
    {
        // Arrange
        var expectedResponse = HttpStatusCode.Conflict;
        _userRepository.GetAllAsync().Returns(new List<User>
        {
            new NormalUser("Àlex MM", "alex@example.com", "Calle Piruleta 123", "+34 874 999 123", 542.32m)
        });

        // Action
        var response = await _httpClient.PostAsync("users", CreateBody(new UserPostRequestDto
        {
            Name = "Àlex MM",
            Email = "alex@example.com",
            Address = "Calle Piruleta 123",
            Phone = "+34 874 999 123",
            UserType = "Normal",
            Money = "100.23"
        }));

        // Assert
        response.StatusCode.Should().Be(expectedResponse);
    }

    private static HttpContent CreateBody(UserPostRequestDto request)
    {
        return new StringContent(JsonSerializer.Serialize(request), Encoding.UTF8, MediaTypeNames.Application.Json);
    }
}