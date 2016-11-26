using Hangfire;
using Moq;
using NUnit.Framework;
using VaraniumSharp.Monolith.Enumerations;
using VaraniumSharp.Monolith.HostSetup;
using VaraniumSharp.Monolith.Interfaces.Configuration;
using VaraniumSharp.Monolith.Tests.Helpers;

namespace VaraniumSharp.Monolith.Tests.HostSetup
{
    public class HangfireTests
    {
        #region Public Methods

        [Test]
        public void EnableHangfireWithInMemoryStore()
        {
            // arrange
            var appBuilderDummy = new AppBuilderFixture();
            var configuration = new Mock<IHangfireConfiguration>();
            var globalConfiguration = new Mock<IGlobalConfiguration>();

            configuration.Setup(t => t.Enabled).Returns(true);
            configuration.Setup(t => t.StorageEngine).Returns(HangfireStorageEngine.MemoryStorage);

            // act
            appBuilderDummy.UseHangfire(configuration.Object, globalConfiguration.Object);

            // assert
        }

        #endregion
    }
}