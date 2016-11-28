using VaraniumSharp.Attributes;
using VaraniumSharp.Enumerations;
using VaraniumSharp.Extensions;
using VaraniumSharp.Monolith.Enumerations;
using VaraniumSharp.Monolith.Interfaces.Configuration;

namespace VaraniumSharp.Monolith.Configuration
{
    /// <summary>
    /// Provides configuration values for setting up Hangfire
    /// </summary>
    [AutomaticContainerRegistration(typeof(IHangfireConfiguration), ServiceReuse.Singleton)]
    public class HangfireConfiguration : IHangfireConfiguration
    {
        #region Properties

        /// <summary>
        /// Should the Hangfire service be enabled
        /// </summary>
        public bool Enabled => ConfigurationKeys.HangfireEnabled.GetConfigurationValue<bool>();

        /// <summary>
        /// Should the Hangfire Dashboard be enabled.
        /// Note that this setting has no effect if Hangfire is disabled
        /// </summary>
        public bool EnableDashboard => ConfigurationKeys.HangfireEnableDashboard.GetConfigurationValue<bool>();

        #endregion
    }
}