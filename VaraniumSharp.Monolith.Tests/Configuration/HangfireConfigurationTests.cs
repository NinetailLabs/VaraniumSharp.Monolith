using FluentAssertions;
using NUnit.Framework;
using VaraniumSharp.Monolith.Configuration;
using VaraniumSharp.Monolith.Enumerations;
using VaraniumSharp.Monolith.Tests.Helpers;

namespace VaraniumSharp.Monolith.Tests.Configuration
{
    public class HangfireConfigurationTests
    {
        [Test]
        public void ConfigurationIsLoadedCorrectly()
        {
            // arrange
            const string sqlConnectionString = "test";

            ApplicationConfigurationHelper.AdjustKeys(ConfigurationKeys.HangfireEnabled, true.ToString());
            ApplicationConfigurationHelper.AdjustKeys(ConfigurationKeys.HangfireEnableDashboard, false.ToString());
            ApplicationConfigurationHelper.AdjustKeys(ConfigurationKeys.HangfireSqlConnectionString, sqlConnectionString);
            ApplicationConfigurationHelper.AdjustKeys(ConfigurationKeys.HangfireStorageEngine, HangfireStorageEngine.MemoryStorage.ToString());

            // act
            var sut = new HangfireConfiguration();

            // assert
            sut.Enabled.Should().BeTrue();
            sut.EnableDashboard.Should().BeFalse();
            sut.SqlServerConnectionString.Should().Be(sqlConnectionString);
            sut.StorageEngine.Should().Be(HangfireStorageEngine.MemoryStorage);
        }
    }
}