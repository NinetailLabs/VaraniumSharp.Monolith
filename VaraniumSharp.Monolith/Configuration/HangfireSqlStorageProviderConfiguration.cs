using Hangfire;
using VaraniumSharp.Extensions;
using VaraniumSharp.Monolith.Enumerations;

namespace VaraniumSharp.Monolith.Configuration
{
    /// <summary>
    /// Configuration wrapper for Hangfire.SqlServer <see>
    ///         <cref>https://www.nuget.org/packages/Hangfire.SqlServer/</cref>
    ///     </see>
    /// </summary>
    public sealed class HangfireSqlStorageProviderConfiguration : HangfireStorageConfigurationBase
    {
        #region Properties

        /// <summary>
        /// Indicate if the Hangfire Sql Storage provider should be used
        /// </summary>
        public bool Enabled => ConfigurationKeys.HangfireSqlStorageEnabled.GetConfigurationValue<bool>();

        /// <summary>
        /// Connection string for Sql Server that should be used
        /// </summary>
        public string SqlConnectionString => ConfigurationKeys.HangfireSqlConnectionString.GetConfigurationValue<string>();

        #endregion

        #region Private Methods

        /// <summary>
        /// This method is called during the apply method
        /// </summary>
        protected override void ProviderSetup(IGlobalConfiguration hangfireConfiguration)
        {
            if (!Enabled)
            {
                return;
            }

            hangfireConfiguration.UseSqlServerStorage(SqlConnectionString);
            IsActive = true;
        }

        #endregion
    }
}