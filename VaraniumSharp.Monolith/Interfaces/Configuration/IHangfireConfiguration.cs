using VaraniumSharp.Monolith.Enumerations;

namespace VaraniumSharp.Monolith.Interfaces.Configuration
{
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

        /// <summary>
        /// Connection string to use if the SqlServer storage engine is used
        /// </summary>
        string SqlServerConnectionString { get; }

        /// <summary>
        /// The storage engine that Hangfire should use.
        /// <see cref="HangfireStorageEngine"/>
        /// </summary>
        HangfireStorageEngine StorageEngine { get; }

        #endregion
    }
}