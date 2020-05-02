namespace Infrastructure.Repositories
{
    using System;
    using System.Threading.Tasks;

    public interface IRepository<T>
    {
        Task<T> Get(Guid id);
    }
}