namespace Infrastructure.Repositories
{
    using Abstraction.Models;

    public class MyRepository : IMyRepository
    {
        public MeModel GetMe()
        {
            return new MeModel
            {
                Document = "05394819637",
                Name = "Marcio Justino"
            };
        }
    }
}