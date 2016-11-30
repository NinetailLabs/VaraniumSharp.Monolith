using FluentAssertions;
using NUnit.Framework;
using System.Globalization;
using VaraniumSharp.Monolith.Configuration;
using VaraniumSharp.Monolith.Enumerations;
using VaraniumSharp.Monolith.Tests.Helpers;

namespace VaraniumSharp.Monolith.Tests.Configuration
{
    public class PushoverConfigurationTests
    {
        #region Public Methods

        [Test]
        public void ConfigurationIsLoadedCorrectly()
        {
            // arrange
            const string apiToken = "testToken";
            const string defaultSendKey = "sendKey";
            const bool isEnabled = true;

            ApplicationConfigurationHelper.AdjustKeys(ConfigurationKeys.PushoverApiToken, apiToken);
            ApplicationConfigurationHelper.AdjustKeys(ConfigurationKeys.PushoverDefaultSendKey, defaultSendKey);
            ApplicationConfigurationHelper.AdjustKeys(ConfigurationKeys.PushoverEnabled, isEnabled.ToString(CultureInfo.InvariantCulture));

            // act
            var sut = new PushoverConfiguration();

            // assert
            sut.ApiToken.Should().Be(apiToken);
            sut.DefaultSendKey.Should().Be(defaultSendKey);
            sut.IsEnabled.Should().Be(isEnabled);
        }

        #endregion
    }
}