using Hangfire;
using Owin;
using System.Collections.Generic;
using System.Linq;
using VaraniumSharp.Attributes;
using VaraniumSharp.Enumerations;
using VaraniumSharp.Extensions;
using VaraniumSharp.Monolith.Enumerations;
using VaraniumSharp.Monolith.Hangfire;
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
        /// <param name="hangfireContainerJobActivator">Hangfire JobActivator using DryIoC</param>
        /// <param name="jobs">Jobs to execute</param>
        public HangfireConfiguration(IEnumerable<HangfireStorageConfigurationBase> hangfireStorageConfigurations, JobActivator hangfireContainerJobActivator, IEnumerable<HangfireJobBase> jobs)
        {
            _hangfireStorageConfigurations = hangfireStorageConfigurations.ToList();
            _hangfireContainerJobActivator = hangfireContainerJobActivator;
            _jobs = jobs.ToList();
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
            hangfireConfiguration.UseActivator(_hangfireContainerJobActivator);
            _hangfireStorageConfigurations.ForEach(config => config.Apply(hangfireConfiguration));

            if (EnableDashboard)
            {
                appBuilder.UseHangfireDashboard();
            }

            appBuilder.UseHangfireServer();

            _jobs.ForEach(t => t.Setup());
        }

        #endregion

        #region Variables

        private readonly JobActivator _hangfireContainerJobActivator;

        private readonly List<HangfireStorageConfigurationBase> _hangfireStorageConfigurations;

        private readonly List<HangfireJobBase> _jobs;

        #endregion
    }
}