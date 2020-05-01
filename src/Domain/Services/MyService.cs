namespace Domain.Services
{
    using System;
    using System.Threading.Tasks;
    using Infrastructure.Repositories;
    using Microsoft.Extensions.Logging;
    using Newtonsoft.Json;

    public class MyService : IMyService
    {
        private readonly ILogger<MyService> _logger;
        private readonly IMyRepository _repo;

        public MyService(ILogger<MyService> logger,
                         IMyRepository repo)
        {
            _logger = logger;
            _repo = repo;
        }

        public async Task Process()
        {
            _logger.LogTrace("[MyService__Process] MyService start");
            var me = _repo.GetMe();
            Console.WriteLine(JsonConvert.SerializeObject(me));
            await Task.Delay(1000);
            _logger.LogTrace("[MyService__Process] MyService finish");
        }
    }
}