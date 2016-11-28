using FluentAssertions;
using Hangfire;
using Moq;
using NUnit.Framework;
using VaraniumSharp.Monolith.Configuration;
using VaraniumSharp.Monolith.Enumerations;
using VaraniumSharp.Monolith.Tests.Helpers;

namespace VaraniumSharp.Monolith.Tests.Configuration
{
    public class HangfireSqlStorageProviderConfigurationTests
    {
        #region Public Methods

        [Test]
        public void DoNotUseSqlStorageProvider()
        {
            // arrange
            var globalConfigDummy = new Mock<IGlobalConfiguration>();
            ApplicationConfigurationHelper.AdjustKeys(ConfigurationKeys.HangfireSqlStorageEnabled, false.ToString());

            var sut = new HangfireSqlStorageProviderConfiguration();

            // act
            sut.Apply(globalConfigDummy.Object);

            // assert
            sut.Enabled.Should().BeFalse();
            sut.IsActive.Should().BeFalse();
        }

        [Test]
        public void UseSqlStorageProvider()
        {
            // arrange
            var globalConfigDummy = new Mock<IGlobalConfiguration>();
            const string testConnectionString = "Server=myServerAddress;Database=myDataBase;Trusted_Connection=True;";
            ApplicationConfigurationHelper.AdjustKeys(ConfigurationKeys.HangfireSqlStorageEnabled, true.ToString());
            ApplicationConfigurationHelper.AdjustKeys(ConfigurationKeys.HangfireSqlConnectionString, testConnectionString);

            var sut = new HangfireSqlStorageProviderConfiguration();

            // act
            sut.Apply(globalConfigDummy.Object);

            // assert
            sut.Enabled.Should().BeTrue();
            sut.IsActive.Should().BeTrue();
            sut.SqlConnectionString.Should().Be(testConnectionString);
        }

        #endregion
    }
}