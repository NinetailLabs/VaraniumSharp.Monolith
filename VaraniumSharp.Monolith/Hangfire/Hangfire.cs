using Owin;
using VaraniumSharp.Monolith.Interfaces.Configuration;

namespace VaraniumSharp.Monolith.Hangfire
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
        public static void UseHangfire(this IAppBuilder appBuilder, IHangfireConfiguration configuration)
        {
            configuration.SetupHangfire(appBuilder);
        }

        #endregion
    }
}