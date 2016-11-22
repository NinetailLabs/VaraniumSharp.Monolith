namespace VaraniumSharp.Monolith.Interfaces
{
    /// <summary>
    /// Configuration for Pushover
    /// </summary>
    public interface IPushoverConfiguration
    {
        #region Properties

        /// <summary>
        /// API key for Pushover.
        /// To register your application or get your Api token <see>
        ///         <cref>https://pushover.net/apps</cref>
        ///     </see>
        /// </summary>
        string ApiToken { get; }

        /// <summary>
        /// Default user to send to if no other user or group key is provided
        /// </summary>
        string DefaultSendKey { get; }

        /// <summary>
        /// Should Pushover be used to send notifications.
        /// Setting this to false will prevent the Pushover service from sending messages even if it is called in code
        /// </summary>
        bool IsEnabled { get; }

        #endregion
    }
}