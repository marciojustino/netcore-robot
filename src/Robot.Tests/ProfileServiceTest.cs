namespace Robot.Tests
{
    using System.Threading.Tasks;
    using Microsoft.Extensions.Logging;
    using Abstraction.Entities;
    using Domain.Services;
    using Infrastructure.Repositories;
    using Moq;
    using Xunit;

    public class ProfileServiceTest
    {
        [Fact]
        public async Task TestGetProfileByIdAsync()
        {
            var profile = new Profile
            {
                Name = "name",
                Document = "document",
                Picture = "Picture"
            };

            var mockLogger = Mock.Of<ILogger<ProfileService>>();
            var mockRepo = new Mock<IProfileRepository>();
            mockRepo.Setup(s => s.Get(profile.Id)).Returns(Task.FromResult(profile));

            var myService = new ProfileService(mockLogger, mockRepo.Object);
            var result = await myService.Get(profile.Id);
            mockRepo.Verify(m => m.Get(profile.Id), Times.Once());
            Assert.Equal(result, profile);
        }
    }
}