namespace Backend.TechChallenge.Api.ComponentTests.Mocks.Contracts;

public interface IMockFactory
{
    TService Get<TService>() where TService : class;
    void Clear();
    void Replace(IServiceCollection apiServices);
}