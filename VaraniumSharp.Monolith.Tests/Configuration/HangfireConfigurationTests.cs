using FluentAssertions;
using Hangfire;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
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
            var hangfireConfigDummy = new Mock<HangfireStorageConfigurationBase>();

            // act
            var sut = new HangfireConfiguration(new List<HangfireStorageConfigurationBase> { hangfireConfigDummy.Object });

            // assert
            sut.Enabled.Should().BeTrue();
            sut.EnableDashboard.Should().BeFalse();
        }

        [Test]
        public void DisabledHangfireIsNoSetUp()
        {
            // arrange
            ApplicationConfigurationHelper.AdjustKeys(ConfigurationKeys.HangfireEnabled, false.ToString());
            ApplicationConfigurationHelper.AdjustKeys(ConfigurationKeys.HangfireEnableDashboard, false.ToString());

            var hangfireConfigDummy = new HangfireStorageProviderFixture();

            var appBuilderDummy = new AppBuilderFixture();
            var sut = new HangfireConfiguration(new List<HangfireStorageConfigurationBase> { hangfireConfigDummy });

            // act
            sut.SetupHangfire(appBuilderDummy);

            // assert
            hangfireConfigDummy.IsActive.Should().BeFalse();
        }

        [Test]
        public void SettingUpHangfireWorksCorrectly()
        {
            // arrange
            ApplicationConfigurationHelper.AdjustKeys(ConfigurationKeys.HangfireEnabled, true.ToString());
            ApplicationConfigurationHelper.AdjustKeys(ConfigurationKeys.HangfireEnableDashboard, false.ToString());

            var hangfireConfigDummy = new HangfireStorageProviderFixture();

            var appBuilderDummy = new AppBuilderFixture();
            var sut = new HangfireConfiguration(new List<HangfireStorageConfigurationBase> { hangfireConfigDummy });

            // act
            sut.SetupHangfire(appBuilderDummy);

            // assert
            hangfireConfigDummy.IsActive.Should().BeTrue();
        }

        #endregion

        private class HangfireStorageProviderFixture : HangfireStorageConfigurationBase
        {
            #region Private Methods

            protected override void ProviderSetup(IGlobalConfiguration hangfireConfiguration)
            {
                IsActive = true;
                JobStorage.Current = new Mock<JobStorage>().Object;
            }

            #endregion
        }
    }
}