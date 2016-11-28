using Hangfire;
using Owin;
using VaraniumSharp.Monolith.Interfaces.Configuration;

namespace VaraniumSharp.Monolith.HostSetup
{
    /// <summary>
    /// Extension methods to allow with setting up Hangfire
    /// </summary>
    public static class Hangfire
    {
        #region Public Methods

        /// <summary>
        /// Apply Hangfire configuration
        /// </summary>
        /// <param name="appBuilder"></param>
        /// <param name="configuration"></param>
        /// <param name="hangfireGlobalConfiguration"></param>
        public static void UseHangfire(this IAppBuilder appBuilder, IHangfireConfiguration configuration, IGlobalConfiguration hangfireGlobalConfiguration)
        {
            if (!configuration.Enabled)
            {
                return;
            }

            if (configuration.EnableDashboard)
            {
                appBuilder.UseHangfireDashboard();
            }
            appBuilder.UseHangfireServer();
        }

        #endregion
    }
}