namespace Worker
{
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using Microsoft.Extensions.Logging;
    using Abstraction.Configurations;
    using Domain.Services;

    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureLogging(loggin =>
                {
                    loggin.ClearProviders();
                    loggin.AddProvider(new CustomLoggerProvider());
                })
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddScoped<WorkerConfigurations>();
                    services.AddTransient<IMyService, MyService>();
                    services.AddHostedService<Worker>();
                });
    }
}
