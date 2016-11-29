using Owin;

namespace VaraniumSharp.Monolith.Interfaces.Configuration
{
    /// <summary>
    /// Hangire basic configuration
    /// </summary>
    public interface IHangfireConfiguration
    {
        #region Properties

        /// <summary>
        /// Should the Hangfire service be enabled
        /// </summary>
        bool Enabled { get; }

        /// <summary>
        /// Should the Hangfire Dashboard be enabled.
        /// Note that this setting has no effect if Hangfire is disabled
        /// </summary>
        bool EnableDashboard { get; }

        #endregion

        #region Public Methods

        /// <summary>
        /// Set up Hangfire based on provided Storage Provides.
        /// Will also enable the Hangfire Dashboard if required
        /// </summary>
        /// <param name="appBuilder"></param>
        void SetupHangfire(IAppBuilder appBuilder);

        #endregion
    }
}