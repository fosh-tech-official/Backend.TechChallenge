using Backend.TechChallenge.Api.ComponentTests.Mocks.Contracts;
using Backend.TechChallenge.Api.ComponentTests.Mocks.Extensions;
using Backend.TechChallenge.Api.ComponentTests.Mocks.Implementations;
using Microsoft.AspNetCore.TestHost;
using Microsoft.AspNetCore.Mvc.Testing;

namespace Backend.TechChallenge.Api.ComponentTests;

public class ComponentFactory : WebApplicationFactory<Startup>
{
    public IMockFactory Mocks { get; }

    private readonly string _currentDirectory;

    public ComponentFactory()
        : this(Directory.GetCurrentDirectory(),
            new MockBuilder()
                .AddMocks()
                ?.Build())
    {
    }

    private ComponentFactory(string currentDirectory, IMockFactory? iMockFactory)
    {
        if (string.IsNullOrWhiteSpace(currentDirectory))
            throw new ArgumentNullException(nameof(currentDirectory));

        _currentDirectory = currentDirectory;

        Mocks = iMockFactory
                ?? throw new ArgumentNullException(nameof(iMockFactory));
    }

    private Action<IConfigurationBuilder> Configure
        => (c) => c.SetBasePath(_currentDirectory)
               .AddJsonFile("testsettings.json", true)
               .AddEnvironmentVariables()
               .Build();

    protected override IHostBuilder CreateHostBuilder()
    {
        var configurationBuilder = new ConfigurationBuilder();
        Configure(configurationBuilder);
        var configuration = configurationBuilder.Build();

        var builder = Host.CreateDefaultBuilder()
            .ConfigureWebHostDefaults(host =>
                                      {
                                          host.UseStartup<Startup>()
                                              .UseTestServer()
                                              .ConfigureAppConfiguration(config =>
                                                                         {
                                                                             config.AddConfiguration(configuration);
                                                                         });
                                      })
            .ConfigureServices(services =>
                               {
                                   Mocks.Replace(services);
                               });

        return builder;
    }

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
    }
}