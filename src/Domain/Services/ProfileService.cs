namespace Domain.Services
{
    using Microsoft.Extensions.Logging;
    using Abstraction.Entities;
    using Infrastructure.Repositories;

    public class ProfileService : BaseService<Profile>, IProfileService
    {
        public ProfileService(ILogger<ProfileService> logger, IRepository<Profile> repo) : base(logger, repo)
        {
        }
    }
}