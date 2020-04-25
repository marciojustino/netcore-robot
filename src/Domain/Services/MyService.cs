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
            _logger.LogTrace("[MyService__Process] MyService start");
            await Task.Delay(2000);
            _logger.LogTrace("[MyService__Process] MyService finish");
        }
    }
}