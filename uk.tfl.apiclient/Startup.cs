using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using uk.tfl.apiclient.ApiClients;
using uk.tfl.apiclient.Interfaces;
using uk.tfl.apiclient.Services;

namespace uk.tfl.apiclient
{
    public static class Startup
    {
        public static IServiceCollection ConfigureServices()
        {
            IServiceCollection serviceCollection = new ServiceCollection();
            serviceCollection.AddSingleton<IRoadApiClient, RoadApiClient>();
            serviceCollection.AddSingleton<IRestClient, RestClient>();

            var builder = new ConfigurationBuilder();
            builder.SetBasePath(Directory.GetCurrentDirectory())
                   .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

            IConfiguration config = builder.Build();
            serviceCollection.AddSingleton<IConfiguration>(config);

            DependencyResolver.Instance.Init(serviceCollection);
            return serviceCollection;
        }
    }
}
