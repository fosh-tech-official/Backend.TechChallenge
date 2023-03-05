using Backend.TechChallenge.Api.ComponentTests.Mocks.Contracts;
using NSubstitute;
using NSubstitute.ClearExtensions;

namespace Backend.TechChallenge.Api.ComponentTests.Mocks;

public class NSubstituteMock<TService> : IMock<TService>
    where TService : class
{
    public TService Instance { get; }

    protected Action<TService> DefaultSubstitute { get; set; }

    public NSubstituteMock()
    {
        Instance = Substitute.For<TService>();
    }

    public NSubstituteMock(Action<TService> defaultSubstitute)
        : this()
    {
        DefaultSubstitute = defaultSubstitute;
    }

    public virtual void Clear()
    {
        Instance.ClearSubstitute();
        DefaultSubstitute?.Invoke(Instance);
    }
}