namespace VaraniumSharp.Monolith.Interfaces.Notifications
{
    /// <summary>
    /// Send Pushover notifications
    /// </summary>
    public interface IPushoverNotifier
    {
        #region Properties

        bool IsEnabled { get; }

        #endregion

        #region Public Methods

        /// <summary>
        /// Send a Pushover notification.
        /// This method will only send a Push if a default send key is provided, if no key is provided the message will be silently dropped and an warning logged
        /// </summary>
        /// <param name="title">Title of the Push</param>
        /// <param name="message">Body text of the Push</param>
        void SendNotification(string title, string message);

        /// <summary>
        /// Send a Pusover notification.
        /// </summary>
        /// <param name="title">Title of the Push</param>
        /// <param name="message">Body text of the Push</param>
        /// <param name="sendKey">Key of the user or group to send to</param>
        void SendNotification(string title, string message, string sendKey);

        #endregion
    }
}