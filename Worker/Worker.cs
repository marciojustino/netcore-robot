namespace Worker
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Abstraction.Configurations;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using Microsoft.Extensions.Logging;

    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private readonly IConfiguration _configurations;
        private readonly WorkerConfigurations _workerConfigurations;

        public Worker(ILogger<Worker> logger,
                      IServiceScopeFactory scopeFactory)
        {
            _logger = logger;
            _serviceScopeFactory = scopeFactory;
            // dependency injection
            using var scope = _serviceScopeFactory.CreateScope();
            _configurations = scope.ServiceProvider.GetRequiredService<IConfiguration>();
            _workerConfigurations = scope.ServiceProvider.GetRequiredService<WorkerConfigurations>();
            _configurations.GetSection("Worker").Bind(_workerConfigurations);
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            if (_workerConfigurations.Interval == 0)
            {
                _workerConfigurations.Interval = 1000; //1 minuto for default
            }

            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Worker running | Interval={interval} milliseconds", _workerConfigurations.Interval);
                await Task.Factory.StartNew(async () => {
                    await Task.Delay(4000, stoppingToken);
                    _logger.LogInformation("Task finished {taskId}", Thread.CurrentThread.ManagedThreadId);
                });
                await Task.Delay(_workerConfigurations.Interval, stoppingToken);
            }
        }
    }
}
