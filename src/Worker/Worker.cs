namespace Worker
{
    using System.Threading.Tasks;
    using System.Threading;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using Microsoft.Extensions.Logging;
    using Abstraction.Configurations;
    using Domain.Services;
    using System;

    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private readonly IConfiguration _configurations;
        private readonly WorkerConfigurations _workerConfigurations;
        private readonly IProfileService _profileService;

        public Worker(
            ILogger<Worker> logger,
            IServiceScopeFactory scopeFactory)
        {
            _logger = logger;
            _serviceScopeFactory = scopeFactory;
            // dependency injection
            using var scope = _serviceScopeFactory.CreateScope();
            _configurations = scope.ServiceProvider.GetRequiredService<IConfiguration>();
            _workerConfigurations = scope.ServiceProvider.GetRequiredService<WorkerConfigurations>();
            _configurations.GetSection("Worker").Bind(_workerConfigurations);
            // Services
            _profileService = scope.ServiceProvider.GetRequiredService<IProfileService>();
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogTrace("[Worker__ExecuteAsync] Worker running | Interval={interval} milliseconds", _workerConfigurations.Interval);
                await _profileService.Get(new Guid());
                await Task.Delay(_workerConfigurations.Interval == 0 ? 1000 : _workerConfigurations.Interval, stoppingToken);
            }
        }
    }
}
