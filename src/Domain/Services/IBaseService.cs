namespace Domain.Services
{
    using System;
    using System.Threading.Tasks;

    public interface IBaseService<T>
    {
        Task<T> Get(Guid id);
    }
}