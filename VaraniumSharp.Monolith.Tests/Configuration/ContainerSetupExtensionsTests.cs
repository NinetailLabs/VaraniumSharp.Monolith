using FluentAssertions;
using Moq;
using NUnit.Framework;
using Serilog;
using System.Collections.Generic;
using VaraniumSharp.Initiator.Configuration;
using VaraniumSharp.Initiator.DependencyInjection;
using VaraniumSharp.Monolith.Configuration;

namespace VaraniumSharp.Monolith.Tests.Configuration
{
    public class ContainerSetupExtensionsTests
    {
        #region Public Methods

        [Test]
        public void LoggingSetupWorksCorrectly()
        {
            // arrange
            var containerDummy = new Mock<ContainerSetup>();
            var logDummy = new LogDummy();
            containerDummy.Setup(t => t.ResolveMany<BaseLogConfiguration>())
                .Returns(new List<BaseLogConfiguration> { logDummy });

            // act
            containerDummy.Object.SetupLogging();

            // assert
            logDummy.IsActive.Should().BeTrue();
            logDummy.WasApplied.Should().BeTrue();
        }

        #endregion

        private class LogDummy : BaseLogConfiguration
        {
            #region Private Methods

            protected override void LogSetup(LoggerConfiguration serilogConfiguration)
            {
                IsActive = true;
            }

            #endregion
        }
    }
}