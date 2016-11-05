using System.Globalization;
using FluentAssertions;
using NUnit.Framework;
using VaraniumSharp.Monolith.Configuration;
using VaraniumSharp.Monolith.Enumerations;
using VaraniumSharp.Monolith.Tests.Helpers;

namespace VaraniumSharp.Monolith.Tests.Configuration
{
    public class HostConfigurationTests
    {
        [Test]
        public void ConfigurationIsLoadedCorrectly()
        {
            // arrange
            const string host = "http://localhost";
            const int port = 1337;

            ApplicationConfigurationHelper.AdjustKeys(ConfigurationKeys.ServiceHost, host);
            ApplicationConfigurationHelper.AdjustKeys(ConfigurationKeys.ServicePort, port.ToString(CultureInfo.InvariantCulture));

            // act
            var sut = new HostConfiguration();

            // assert
            sut.Host.Should().Be(host);
            sut.Port.Should().Be(port);
            sut.HostUrl.Should().Be($"{host}:{port}");
        }
    }
}