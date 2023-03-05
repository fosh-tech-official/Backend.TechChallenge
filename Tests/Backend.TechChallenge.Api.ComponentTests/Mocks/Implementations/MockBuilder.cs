using Backend.TechChallenge.Api.ComponentTests.Mocks.Contracts;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Backend.TechChallenge.Api.ComponentTests.Mocks.Implementations;

public sealed class MockBuilder : IMockBuilder
{
    public IServiceCollection Services { get; }
    public IList<Action<IServiceCollection, IServiceProvider>> ReplaceActions { get; }

    public MockBuilder()
    {
        Services = new ServiceCollection();
        ReplaceActions = new List<Action<IServiceCollection, IServiceProvider>>();
    }

    public IMockFactory? Build()
    {
        var result = new MockFactory(Services.BuildServiceProvider(), ReplaceActions);
        return result;
    }

    public IMockBuilder AddMock<TService, TMock>()
        where TService : class
        where TMock : class, IMock<TService>
    {
        Services.AddSingleton<IMock<TService>, TMock>();
        Services.AddSingleton<IMock>((p) => (IMock)p.GetService<IMock<TService>>());

        ReplaceActions.Add((testService, mockProvider) => {
                               testService.RemoveAll<TService>();
                               testService.AddSingleton(mockProvider.GetService<IMock<TService>>().Instance);
                           });

        return this;
    }
}