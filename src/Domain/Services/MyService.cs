namespace Domain.Services
{
    using System.Threading.Tasks;
    using Microsoft.Extensions.Logging;

    public class MyService : IMyService
    {
        private readonly ILogger<MyService> _logger;

        public MyService(ILogger<MyService> logger)
        {
            _logger = logger;
        }

        public async Task Process()
        {
            _logger.LogTrace("[JobobService__Process] Starting MyService");
            await Task.Delay(4000);
        }
    }
}