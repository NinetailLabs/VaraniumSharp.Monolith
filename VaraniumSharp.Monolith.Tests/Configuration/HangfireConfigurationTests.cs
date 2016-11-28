using FluentAssertions;
using NUnit.Framework;
using VaraniumSharp.Monolith.Configuration;
using VaraniumSharp.Monolith.Enumerations;
using VaraniumSharp.Monolith.Tests.Helpers;

namespace VaraniumSharp.Monolith.Tests.Configuration
{
    public class HangfireConfigurationTests
    {
        #region Public Methods

        [Test]
        public void ConfigurationIsLoadedCorrectly()
        {
            // arrange
            ApplicationConfigurationHelper.AdjustKeys(ConfigurationKeys.HangfireEnabled, true.ToString());
            ApplicationConfigurationHelper.AdjustKeys(ConfigurationKeys.HangfireEnableDashboard, false.ToString());

            // act
            var sut = new HangfireConfiguration();

            // assert
            sut.Enabled.Should().BeTrue();
            sut.EnableDashboard.Should().BeFalse();
        }

        #endregion
    }
}