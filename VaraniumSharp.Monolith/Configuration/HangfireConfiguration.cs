using Hangfire;
using Owin;
using System.Collections.Generic;
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
        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="hangfireStorageConfigurations">Collection of all Hangfire Storage Provider configurations</param>
        public HangfireConfiguration(List<HangfireStorageConfigurationBase> hangfireStorageConfigurations)
        {
            _hangfireStorageConfigurations = hangfireStorageConfigurations;
        }

        #endregion

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

        #region Public Methods

        /// <summary>
        /// Set up Hangfire based on provided Storage Provides.
        /// Will also enable the Hangfire Dashboard if required
        /// </summary>
        /// <param name="appBuilder"></param>
        public void SetupHangfire(IAppBuilder appBuilder)
        {
            if (!Enabled)
            {
                return;
            }

            var hangfireConfiguration = GlobalConfiguration.Configuration;
            foreach (var config in _hangfireStorageConfigurations)
            {
                config.Apply(hangfireConfiguration);
            }

            if (EnableDashboard)
            {
                appBuilder.UseHangfireDashboard();
            }

            appBuilder.UseHangfireServer();
        }

        #endregion

        #region Variables

        private readonly List<HangfireStorageConfigurationBase> _hangfireStorageConfigurations;

        #endregion
    }
}