using Moq;
using NUnit.Framework;
using VaraniumSharp.Monolith.Hangfire;
using VaraniumSharp.Monolith.Interfaces.Configuration;
using VaraniumSharp.Monolith.Tests.Helpers;

namespace VaraniumSharp.Monolith.Tests.Hangfire
{
    public class HangfireTests
    {
        #region Public Methods

        [Test]
        public void UseHangfire()
        {
            // arrange
            var appBuilderDummy = new AppBuilderFixture();
            var configuration = new Mock<IHangfireConfiguration>();

            configuration.Setup(t => t.Enabled).Returns(true);

            // act
            appBuilderDummy.UseHangfire(configuration.Object);

            // assert
            configuration.Verify(t => t.SetupHangfire(appBuilderDummy), Times.Once);
        }

        #endregion
    }
}