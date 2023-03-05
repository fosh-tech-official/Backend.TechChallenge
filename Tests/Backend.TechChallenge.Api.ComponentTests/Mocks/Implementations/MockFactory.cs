using Backend.TechChallenge.Api.ComponentTests.Mocks.Contracts;

namespace Backend.TechChallenge.Api.ComponentTests.Mocks.Implementations;

public sealed class MockFactory : IMockFactory
{
    private readonly IServiceProvider _serviceProvider;
    private readonly IEnumerable<Action<IServiceCollection, IServiceProvider>> _replaceActions;

    public MockFactory(IServiceProvider serviceProvider, 
        IEnumerable<Action<IServiceCollection, IServiceProvider>> replaceActions)
    {
        _serviceProvider = serviceProvider 
                           ?? throw new ArgumentNullException(nameof(serviceProvider));

        _replaceActions = replaceActions;
    }

    public TService Get<TService>()
        where TService : class
    {
        var iMock = _serviceProvider.GetService<IMock<TService>>();
        return iMock?.Instance;
    }

    public void Clear()
    {
        var iMocks = _serviceProvider.GetServices<IMock>();
        foreach (var item in iMocks)
            item.Clear();
    }

    public void Replace(IServiceCollection apiServices)
    {
        foreach (var action in _replaceActions)
        {
            action(apiServices, _serviceProvider);
        }
    }
}