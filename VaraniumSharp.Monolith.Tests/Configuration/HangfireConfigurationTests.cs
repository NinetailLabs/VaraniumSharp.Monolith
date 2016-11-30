using FluentAssertions;
using Hangfire;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using VaraniumSharp.Monolith.Configuration;
using VaraniumSharp.Monolith.Enumerations;
using VaraniumSharp.Monolith.Hangfire;
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
            var activatorDummy = new Mock<JobActivator>();

            // act
            var sut = new HangfireConfiguration(new List<HangfireStorageConfigurationBase> { hangfireConfigDummy.Object }, activatorDummy.Object, new List<HangfireJobBase>());

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
            var activatorDummy = new Mock<JobActivator>();

            var hangfireConfigDummy = new HangfireStorageProviderFixture();

            var appBuilderDummy = new AppBuilderFixture();
            var sut = new HangfireConfiguration(new List<HangfireStorageConfigurationBase> { hangfireConfigDummy }, activatorDummy.Object, new List<HangfireJobBase>());

            // act
            sut.SetupHangfire(appBuilderDummy);

            // assert
            hangfireConfigDummy.IsActive.Should().BeFalse();
        }

        [Test]
        public void JobsAreCorrectlyConfiguredIfHangfireIsActive()
        {
            // arrange
            ApplicationConfigurationHelper.AdjustKeys(ConfigurationKeys.HangfireEnabled, true.ToString());
            ApplicationConfigurationHelper.AdjustKeys(ConfigurationKeys.HangfireEnableDashboard, false.ToString());
            var activatorDummy = new Mock<JobActivator>();
            var jobDummy = new JobFixture();

            var hangfireConfigDummy = new HangfireStorageProviderFixture();

            var appBuilderDummy = new AppBuilderFixture();
            var sut = new HangfireConfiguration(new List<HangfireStorageConfigurationBase> { hangfireConfigDummy }, activatorDummy.Object, new List<HangfireJobBase> { jobDummy });

            // act
            sut.SetupHangfire(appBuilderDummy);

            // assert
            jobDummy.WasSetUp.Should().BeTrue();
        }

        [Test]
        public void JobsAreNotConfiguredIfHangfireIsNotUsed()
        {
            // arrange
            ApplicationConfigurationHelper.AdjustKeys(ConfigurationKeys.HangfireEnabled, true.ToString());
            ApplicationConfigurationHelper.AdjustKeys(ConfigurationKeys.HangfireEnableDashboard, false.ToString());
            var activatorDummy = new Mock<JobActivator>();
            var jobDummy = new JobFixture();

            var hangfireConfigDummy = new HangfireStorageProviderFixture();

            var appBuilderDummy = new AppBuilderFixture();
            var sut = new HangfireConfiguration(new List<HangfireStorageConfigurationBase> { hangfireConfigDummy }, activatorDummy.Object, new List<HangfireJobBase> { jobDummy });

            // act
            sut.SetupHangfire(appBuilderDummy);

            // assert
            jobDummy.WasSetUp.Should().BeTrue();
        }

        [Test]
        public void SettingUpHangfireWorksCorrectly()
        {
            // arrange
            ApplicationConfigurationHelper.AdjustKeys(ConfigurationKeys.HangfireEnabled, false.ToString());
            ApplicationConfigurationHelper.AdjustKeys(ConfigurationKeys.HangfireEnableDashboard, false.ToString());
            var activatorDummy = new Mock<JobActivator>();

            var hangfireConfigDummy = new HangfireStorageProviderFixture();

            var appBuilderDummy = new AppBuilderFixture();
            var sut = new HangfireConfiguration(new List<HangfireStorageConfigurationBase> { hangfireConfigDummy }, activatorDummy.Object, new List<HangfireJobBase>());

            // act
            sut.SetupHangfire(appBuilderDummy);

            // assert
            hangfireConfigDummy.IsActive.Should().BeFalse();
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

        private class JobFixture : HangfireJobBase
        {
            #region Properties

            public bool WasSetUp { get; private set; }

            #endregion

            #region Public Methods

            public override void Execute()
            {
                //Not needed
            }

            public override void Setup()
            {
                WasSetUp = true;
            }

            #endregion
        }
    }
}