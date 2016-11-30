using FluentAssertions;
using Hangfire;
using Moq;
using NUnit.Framework;
using System;
using System.Data.SqlClient;
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

        /// <summary>
        /// This test can only measure if SQL server setup has failed, however this is enough to prove that the UseSqlServerStorage method was called.
        /// Due to the way that Hangfire is constructed there is no simple way to use Mocks to prove the required method was called so we instead rely on a negative test.
        /// To prove that a SQL server connection was created we will instead rquire Integration tests
        /// </summary>
        [Test]
        public void UseSqlStorageProviderNegativeProof()
        {
            // arrange
            var globalConfigDummy = new Mock<IGlobalConfiguration>();
            const string testConnectionString = "Server=myServerAddress;Database=myDataBase;Trusted_Connection=True;";
            ApplicationConfigurationHelper.AdjustKeys(ConfigurationKeys.HangfireSqlStorageEnabled, true.ToString());
            ApplicationConfigurationHelper.AdjustKeys(ConfigurationKeys.HangfireSqlConnectionString, testConnectionString);

            var sut = new HangfireSqlStorageProviderConfiguration();

            var act = new Action(() => sut.Apply(globalConfigDummy.Object));

            // act
            // assert
            act.ShouldThrow<SqlException>();
            sut.Enabled.Should().BeTrue();
        }

        #endregion
    }
}