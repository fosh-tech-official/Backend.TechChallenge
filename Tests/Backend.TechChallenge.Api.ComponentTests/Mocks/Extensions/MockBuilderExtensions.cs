using Backend.TechChallenge.Api.ComponentTests.Mocks.Contracts;
using Backend.TechChallenge.Domain.Repositories.Contracts;

namespace Backend.TechChallenge.Api.ComponentTests.Mocks.Extensions;

public static class MockBuilderExtensions
{
    public static IMockBuilder? AddMocks(this IMockBuilder? mockBuilder)
    {
        if (mockBuilder == null)
            return null;

        mockBuilder.AddMock<IUserRepository, UserRepositoryMock>();

        return mockBuilder;
    }
}