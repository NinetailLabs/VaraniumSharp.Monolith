using PushoverClient;
using Serilog;
using System;
using VaraniumSharp.Monolith.Interfaces;

namespace VaraniumSharp.Monolith.Notifications
{
    /// <summary>
    /// Provides ability to easily send Pushover notifications
    /// </summary>
    //TODO - Add interface
    public class PushoverNotifier
    {
        #region Constructor

        /// <summary>
        /// Parameterless Constructor
        /// </summary>
        public PushoverNotifier(IPushoverConfiguration configuration)
        {
            _configuration = configuration;
            IsEnabled = configuration.IsEnabled;
            _log = Log.Logger.ForContext<PushoverNotifier>();

            if (!_configuration.IsEnabled)
            {
                return;
            }

            _log.Information("Setting up Pushover client");
            _client = string.IsNullOrEmpty(_configuration.DefaultSendKey)
                ? new Pushover(_configuration.ApiToken)
                : new Pushover(_configuration.ApiToken, _configuration.DefaultSendKey);
        }

        #endregion

        #region Properties

        public bool IsEnabled { get; }

        #endregion

        #region Public Methods

        /// <summary>
        /// Send a Pushover notification.
        /// This method will only send a Push if a default send key is provided, if no key is provided the message will be silently dropped and an warning logged
        /// </summary>
        /// <param name="title">Title of the Push</param>
        /// <param name="message">Body text of the Push</param>
        public void SendNotification(string title, string message)
        {
            if (string.IsNullOrEmpty(_configuration.DefaultSendKey))
            {
                _log.Warning("DefaultSendKey is not provided and no send key was provided. Cannot send Push");
                return;
            }
            SendNotification(title, message, string.Empty);
        }

        /// <summary>
        /// Send a Pusover notification.
        /// </summary>
        /// <param name="title">Title of the Push</param>
        /// <param name="message">Body text of the Push</param>
        /// <param name="sendKey">Key of the user or group to send to</param>
        public void SendNotification(string title, string message, string sendKey)
        {
            if (!_configuration.IsEnabled)
            {
                _log.Verbose("Cannot send Push, Pushover is disabled");
                return;
            }

            try
            {
                _client.Push(title, message, sendKey);
                _log.Debug("Push sent");
            }
            catch (Exception exception)
            {
                _log.Error(exception, "An error occured while trying to send a Push");
            }
        }

        #endregion

        #region Variables

        private readonly Pushover _client;
        private readonly IPushoverConfiguration _configuration;
        private readonly ILogger _log;

        #endregion
    }
}