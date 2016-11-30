using Hangfire;
using Hangfire.MemoryStorage;
using VaraniumSharp.Extensions;
using VaraniumSharp.Monolith.Enumerations;

namespace VaraniumSharp.Monolith.Configuration
{
    /// <summary>
    /// Configuration wrapper for Hangfire.MemoryStorage <see>
    ///         <cref>https://www.nuget.org/packages/Hangfire.MemoryStorage/</cref>
    ///     </see>
    /// </summary>
    public sealed class HangfireMemoryStorageConfiguration : HangfireStorageConfigurationBase
    {
        #region Properties

        /// <summary>
        /// Indicate if the Hangfire Memory Storage provider should be used
        /// </summary>
        public bool Enabled => ConfigurationKeys.HangfireMemoryStorageEnabled.GetConfigurationValue<bool>();

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

            hangfireConfiguration.UseMemoryStorage();
            IsActive = true;
        }

        #endregion
    }
}