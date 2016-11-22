using VaraniumSharp.Extensions;
using VaraniumSharp.Monolith.Enumerations;
using VaraniumSharp.Monolith.Interfaces;

namespace VaraniumSharp.Monolith.Configuration
{
    public class PushoverConfiguration : IPushoverConfiguration
    {
        #region Properties

        /// <summary>
        /// API key for Pushover.
        /// To register your application or get your Api token <see>
        ///         <cref>https://pushover.net/apps</cref>
        ///     </see>
        /// </summary>
        public string ApiToken => ConfigurationKeys.PushoverApiToken.GetConfigurationValue<string>();

        /// <summary>
        /// Default user to send to if no other user or group key is provided
        /// </summary>
        public string DefaultSendKey => ConfigurationKeys.PushoverDefaultSendKey.GetConfigurationValue<string>();

        /// <summary>
        /// Should Pushover be used to send notifications.
        /// Setting this to false will prevent the Pushover service from sending messages even if it is called in code
        /// </summary>
        public bool IsEnabled => ConfigurationKeys.PushoverEnabled.GetConfigurationValue<bool>();

        #endregion
    }
}