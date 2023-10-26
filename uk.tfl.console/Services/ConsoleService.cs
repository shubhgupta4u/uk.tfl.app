using Microsoft.Extensions.Hosting;
using uk.tfl.apiclient;
using uk.tfl.apiclient.Interfaces;
using uk.tfl.apiclient.Models;

namespace uk.tfl.console.Services
{
    public class ConsoleService : IHostedService
    {
        private readonly IHostApplicationLifetime _appLifetime;
        private readonly List<string> _roadIdentifiers;

        public ConsoleService(
            IHostApplicationLifetime appLifetime)
        {
            _appLifetime = appLifetime;
            _roadIdentifiers=new List<string>();
            string[] args = Environment.GetCommandLineArgs();
            if(args!= null && args.Any() && args.Count() >1)
            {
                for(int i = 1; i < args.Length; i++)
                {
                    _roadIdentifiers.Add(args[i]);
                }
            }
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {

            _appLifetime.ApplicationStarted.Register(() =>
            {
                Task.Run(async () =>
                {
                    int exitCode = 0;
                    try
                    {
                      
                        IRoadApiClient roadApiClient = DependencyResolver.Instance.Resolve<IRoadApiClient>();
                        List<RoadCorridor> roadCorridors = await roadApiClient.GetCorridorsAsync(this._roadIdentifiers);
                       
                        foreach (string name in this._roadIdentifiers)
                        {
                            RoadCorridor roadCorridor = roadCorridors?.FirstOrDefault(a => a.DisplayName.Equals(name, StringComparison.InvariantCultureIgnoreCase));
                            if (roadCorridor != null)
                            {
                                Console.WriteLine(string.Format("The status of the {0} is as follows", roadCorridor.DisplayName));
                                Console.WriteLine(string.Format("Road Status is {0}", roadCorridor.StatusSeverity));
                                Console.WriteLine(string.Format("Road Status Description is  {0}", roadCorridor.StatusSeverityDescription));
                            }
                            else
                            {
                                Console.WriteLine(string.Format("{0} is not a valid road", roadCorridor.DisplayName));
                                exitCode = 1;
                            }
                        }

                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                        exitCode = 1;
                    }
                    finally
                    {
                        Environment.Exit(exitCode);
                        _appLifetime.StopApplication();
                    }
                });
            });

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
