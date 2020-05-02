namespace Infrastructure.Repositories
{
    using System;
    using System.Threading.Tasks;
    using Abstraction.Entities;

    public class ProfileRepository : IProfileRepository
    {
        public Task<Profile> Get(Guid id)
        {
            return Task.Factory.StartNew(() => new Profile { Name = "my name", Document = "my document", Picture = "my picture" });
        }
    }
}