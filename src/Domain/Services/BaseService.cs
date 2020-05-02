namespace Domain.Services
{
    using System;
    using System.Threading.Tasks;
    using Infrastructure.Repositories;
    using Microsoft.Extensions.Logging;

    public abstract class BaseService<T> : IBaseService<T>
    {
        protected readonly ILogger _logger;
        protected readonly IRepository<T> _repo;

        public BaseService(ILogger logger, IRepository<T> repo)
        {
            _logger = logger;
            _repo = repo;
        }

        public Task<T> Get(Guid id)
        {
            return _repo.Get(id);
        }
    }
}