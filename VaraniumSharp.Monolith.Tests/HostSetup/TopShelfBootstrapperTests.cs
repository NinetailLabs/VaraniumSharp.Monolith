using FluentAssertions;
using Moq;
using NUnit.Framework;
using System.Linq;
using VaraniumSharp.Monolith.Configuration;
using VaraniumSharp.Monolith.HostSetup;
using VaraniumSharp.Monolith.Interfaces;
using VaraniumSharp.Monolith.Tests.Helpers;

namespace VaraniumSharp.Monolith.Tests.HostSetup
{
    public class TopShelfBootstrapperTests
    {
        [Test]
        public void ExecutingHostWorks()
        {
            // arrange
            var loggerTuple = LoggerFixtureHelper.SetupLogCatcher();

            const string serviceName = "Test Service";
            const string serviceDisplayName = "Test Display";
            const string serviceDescription = "Test Description";
            var loggedString = string.Empty;

            var fixture = new TopShelfBootstrapperFixture();
            fixture.TopShelfConfigurationMock.Setup(t => t.DisplayName).Returns(serviceDisplayName);
            fixture.TopShelfConfigurationMock.Setup(t => t.Name).Returns(serviceName);
            fixture.TopShelfConfigurationMock.Setup(t => t.Description).Returns(serviceDescription);

            loggerTuple.Item2.Setup(t => t.Information(It.IsAny<string>(), It.IsAny<object[]>())).Callback<string, object[]>((t, x) =>
            {
                loggedString += x.FirstOrDefault();
            });

            // act
            fixture.SetupInstance();

            // assert
            fixture.Instance.TopShelfHost.Should().NotBeNull();
            loggedString.Should().Contain(serviceName);
            loggedString.Should().Contain(serviceDisplayName);

            LoggerFixtureHelper.SwitchLogger(loggerTuple.Item1);
        }

        private class TopShelfBootstrapperFixture
        {
            public Mock<ITopShelfService> TopShelfServiceMock { get; } = new Mock<ITopShelfService>();
            public Mock<ITopShelfConfiguration> TopShelfConfigurationMock { get; } = new Mock<ITopShelfConfiguration>();

            public TopShelfBootstrapper Instance { get; private set; }
            public ITopShelfService GeTopShelfService => TopShelfServiceMock.Object;
            public ITopShelfConfiguration GetTopShelfConfiguration => TopShelfConfigurationMock.Object;

            public void SetupInstance()
            {
                TopShelfServiceMock.Setup(t => t.TopShelfConfiguration).Returns(GetTopShelfConfiguration);
                Instance = new TopShelfBootstrapper(GeTopShelfService);
            }
        }
    }
}