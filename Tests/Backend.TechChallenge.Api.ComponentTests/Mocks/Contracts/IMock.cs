namespace Backend.TechChallenge.Api.ComponentTests.Mocks.Contracts;

public interface IMock<out TService> : IMock
    where TService : class
{
    TService Instance { get; }
}

public interface IMock
{
    void Clear();
}