using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using uk.tfl.apiclient;
using uk.tfl.console.Services;


using IHost host = CreateHostBuilder(args).Build();
await host.RunAsync();


static IHostBuilder CreateHostBuilder(string[] args) =>
    Host.CreateDefaultBuilder(args)
    .ConfigureServices((services) =>
    {
        Startup.ConfigureServices();
        services.AddHostedService<ConsoleService>();
    });