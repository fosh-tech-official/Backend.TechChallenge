namespace Backend.TechChallenge.Api.ComponentTests.Mocks.Contracts;

public interface IMockBuilder
{
    IServiceCollection Services { get; }
    IList<Action<IServiceCollection, IServiceProvider>> ReplaceActions { get; }

    IMockBuilder AddMock<TService, TMock>()
        where TService : class
        where TMock : class, IMock<TService>;

    IMockFactory? Build();
}