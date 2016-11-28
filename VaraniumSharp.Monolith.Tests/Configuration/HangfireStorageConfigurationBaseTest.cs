using FluentAssertions;
using Hangfire;
using Moq;
using NUnit.Framework;
using System;
using VaraniumSharp.Monolith.Configuration;

namespace VaraniumSharp.Monolith.Tests.Configuration
{
    public class HangfireStorageConfigurationBaseTest
    {
        #region Public Methods

        [Test]
        public void ApplyingConfigurationTwiceThrowsAnException()
        {
            // arrange
            var globalConfigDummy = new Mock<IGlobalConfiguration>();
            var sut = new HangfireConfigurationFixture();
            sut.Apply(globalConfigDummy.Object);

            var act = new Action(() => sut.Apply(globalConfigDummy.Object));

            // act
            // assert
            act.ShouldThrow<InvalidOperationException>();
        }

        [Test]
        public void ProviderIsCorrectSetUp()
        {
            // arrange
            var globalConfigDummy = new Mock<IGlobalConfiguration>();
            var sut = new HangfireConfigurationFixture();

            // act
            sut.Apply(globalConfigDummy.Object);

            // assert
            sut.IsActive.Should().BeTrue();
            sut.WasApplied.Should().BeTrue();
        }

        #endregion

        private class HangfireConfigurationFixture : HangfireStorageConfigurationBase
        {
            #region Private Methods

            protected override void ProviderSetup(IGlobalConfiguration hangfireConfiguration)
            {
                IsActive = true;
            }

            #endregion
        }
    }
}