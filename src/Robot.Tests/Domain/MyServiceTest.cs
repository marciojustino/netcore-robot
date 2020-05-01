namespace Robot.Tests.Domain
{
    using System.Threading.Tasks;
    using Abstraction.Models;
    using global::Domain.Services;
    using Infrastructure.Repositories;
    using Microsoft.Extensions.Logging;
    using Moq;
    using Xunit;

    public class MyServiceTest
    {
        [Fact]
        public async Task TestProcessAsync()
        {
            var name = "name";
            var document = "document";

            var mockLogger = Mock.Of<ILogger<MyService>>();
            var mockRepo = new Mock<IMyRepository>();
            mockRepo.Setup(s => s.GetMe())
                .Returns(
                    new MeModel
                    {
                        Name = name,
                        Document = document
                    });

            var myService = new MyService(mockLogger, mockRepo.Object);
            await myService.Process();
            mockRepo.Verify(m => m.GetMe(), Times.Once());
        }
    }
}