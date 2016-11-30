using PushoverClient;
using VaraniumSharp.Attributes;
using VaraniumSharp.Enumerations;
using VaraniumSharp.Monolith.Interfaces;
using VaraniumSharp.Monolith.Interfaces.Wrappers;

namespace VaraniumSharp.Monolith.Wrappers
{
    /// <summary>
    /// Wrapper for <see cref="Pushover"/> that allows it to be used with <see cref="IPushoverConfiguration"/>.
    /// The wrapper also makes Pushover play well with dependency injection
    /// </summary>
    [AutomaticContainerRegistration(typeof(IPushoverWrapper), ServiceReuse.Singleton)]
    public sealed class PushoverWrapper : Pushover, IPushoverWrapper
    {
        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="configuration">Pushover configuration class</param>
        public PushoverWrapper(IPushoverConfiguration configuration)
            : base(configuration.ApiToken)
        {
            Configuration = configuration;

            if (!string.IsNullOrEmpty(configuration.DefaultSendKey))
            {
                DefaultUserGroupSendKey = configuration.DefaultSendKey;
            }
        }

        #endregion

        #region Properties

        /// <summary>
        /// Get the configuration for Pushover
        /// </summary>
        public IPushoverConfiguration Configuration { get; }

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
        public new PushResponse Push(string title, string message, string userKey = "", string device = "")
        {
            return base.Push(title, message, userKey, device);
        }

        #endregion
    }
}