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
    }
}