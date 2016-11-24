using Hangfire;
using Hangfire.MemoryStorage;
using Owin;
using System;
using VaraniumSharp.Monolith.Enumerations;
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

            switch (configuration.StorageEngine)
            {
                case HangfireStorageEngine.MemoryStorage:
                    hangfireGlobalConfiguration.UseMemoryStorage();
                    break;

                case HangfireStorageEngine.SqlServer:
                    hangfireGlobalConfiguration.UseSqlServerStorage(configuration.SqlServerConnectionString);
                    break;

                default:
                    throw new ArgumentException($"The Storage engine {configuration.StorageEngine} is currently not supported");
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