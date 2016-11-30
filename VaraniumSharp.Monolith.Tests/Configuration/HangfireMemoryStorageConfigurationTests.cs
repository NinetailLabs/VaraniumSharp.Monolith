using FluentAssertions;
using Hangfire;
using Moq;
using NUnit.Framework;
using VaraniumSharp.Monolith.Configuration;
using VaraniumSharp.Monolith.Enumerations;
using VaraniumSharp.Monolith.Tests.Helpers;

namespace VaraniumSharp.Monolith.Tests.Configuration
{
    public class HangfireMemoryStorageConfigurationTests
    {
        #region Public Methods

        [Test]
        public void DoNotUseMemoryStorage()
        {
            // arrange
            var globalConfigDummy = new Mock<IGlobalConfiguration>();
            ApplicationConfigurationHelper.AdjustKeys(ConfigurationKeys.HangfireMemoryStorageEnabled, false.ToString());

            var sut = new HangfireMemoryStorageConfiguration();

            // act
            sut.Apply(globalConfigDummy.Object);

            // assert
            sut.Enabled.Should().BeFalse();
            sut.IsActive.Should().BeFalse();
        }

        [Test]
        public void UseMemoryStorage()
        {
            // arrange
            var globalConfigDummy = new Mock<IGlobalConfiguration>();
            ApplicationConfigurationHelper.AdjustKeys(ConfigurationKeys.HangfireMemoryStorageEnabled, true.ToString());

            var sut = new HangfireMemoryStorageConfiguration();

            // act
            sut.Apply(globalConfigDummy.Object);

            // assert
            sut.Enabled.Should().BeTrue();
            sut.IsActive.Should().BeTrue();
        }

        #endregion
    }
}