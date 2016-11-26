using FluentAssertions;
using Moq;
using NUnit.Framework;
using VaraniumSharp.Monolith.Enumerations;
using VaraniumSharp.Monolith.HostSetup;
using VaraniumSharp.Monolith.Interfaces.Configuration;
using VaraniumSharp.Monolith.Tests.Helpers;

namespace VaraniumSharp.Monolith.Tests.HostSetup
{
    public class OwinStartupTest
    {
        #region Public Methods

        [Test]
        public void RegisterHangfireForStartup()
        {
            // arrange
            var hangfireConfigDummy = new Mock<IHangfireConfiguration>();
            hangfireConfigDummy.Setup(t => t.Enabled).Returns(true);
            hangfireConfigDummy.Setup(t => t.StorageEngine).Returns(HangfireStorageEngine.MemoryStorage);
            hangfireConfigDummy.Setup(t => t.EnableDashboard).Returns(false);

            var appBuilderDummy = new AppBuilderFixture();
            var sut = new OwinStartup(hangfireConfigDummy.Object);

            // act
            sut.Configuration(appBuilderDummy);

            // assert
            appBuilderDummy.MiddleWareRegistrationInvocations.Should().Be(1);
        }

        [Test]
        public void RegisterNancyForStartup()
        {
            // arrange
            var hangfireConfigDummy = new Mock<IHangfireConfiguration>();
            var appBuilderDummy = new AppBuilderFixture();
            var sut = new OwinStartup(hangfireConfigDummy.Object);

            // act
            sut.Configuration(appBuilderDummy);

            // assert
            appBuilderDummy.MiddleWareRegistrationInvocations.Should().Be(1);
        }

        #endregion
    }
}