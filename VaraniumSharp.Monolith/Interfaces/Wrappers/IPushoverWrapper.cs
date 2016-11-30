using PushoverClient;

namespace VaraniumSharp.Monolith.Interfaces.Wrappers
{
    /// <summary>
    /// Interface for Pushover wrapper
    /// </summary>
    public interface IPushoverWrapper
    {
        #region Properties

        /// <summary>
        /// Get the configuration for Pushover
        /// </summary>
        IPushoverConfiguration Configuration { get; }

        #endregion

        #region Public Methods

        /// <summary>
        /// Push a message
        /// </summary>
        /// <param name="title">Message title</param>
        /// <param name="message">The body of the message</param>
        /// <param name="userKey">The user or group key (optional if you have set a default already)</param>
        /// <param name="device">Send to a specific device</param>
        /// <returns></returns>
        PushResponse Push(string title, string message, string userKey = "", string device = "");

        #endregion
    }
}