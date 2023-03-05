using Xunit;

namespace Backend.TechChallenge.Api.ComponentTests;

[CollectionDefinition(nameof(WebApplicationFactoryCollection))]
public class WebApplicationFactoryCollection : ICollectionFixture<ComponentFactory>
{
}