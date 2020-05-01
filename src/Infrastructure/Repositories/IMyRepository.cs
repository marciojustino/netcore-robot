namespace Infrastructure.Repositories
{
    using Abstraction.Models;

    public interface IMyRepository
    {
        MeModel GetMe();
    }
}