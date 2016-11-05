using FluentAssertions;
using NUnit.Framework;
using Topshelf.Runtime;
using VaraniumSharp.Monolith.Configuration;
using VaraniumSharp.Monolith.Enumerations;
using VaraniumSharp.Monolith.Tests.Helpers;

namespace VaraniumSharp.Monolith.Tests.Configuration
{
    public class TopShelfConfigurationTests
    {
        [Test]
        public void ConfigurationIsLoadedCorrectly()
        {
            // arrange
            const string serviceName = "Test";
            const string serviceDisplayName = "Test Display";
            const string serviceDescription = "Test Description";
            const HostStartMode startMode = HostStartMode.Automatic;

            ApplicationConfigurationHelper.AdjustKeys(ConfigurationKeys.ServiceName, serviceName);
            ApplicationConfigurationHelper.AdjustKeys(ConfigurationKeys.ServiceDisplayName, serviceDisplayName);
            ApplicationConfigurationHelper.AdjustKeys(ConfigurationKeys.ServiceDescription, serviceDescription);
            ApplicationConfigurationHelper.AdjustKeys(ConfigurationKeys.ServiceStartMode, startMode.ToString());

            // act
            var sut = new TopShelfConfiguration();

            // assert
            sut.Name.Should().Be(serviceName);
            sut.DisplayName.Should().Be(serviceDisplayName);
            sut.Description.Should().Be(serviceDescription);
            sut.StartMode.Should().Be(startMode);
        }
    }
}