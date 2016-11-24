﻿namespace VaraniumSharp.Monolith.Enumerations
{
    /// <summary>
    /// Contains keys for reading configuration values from App.config
    /// </summary>
    public static class ConfigurationKeys
    {
        #region Variables

        /// <summary>
        /// Indicates if Hangfire should be enabled
        /// </summary>
        public const string HangfireEnabled = "hangfire.enabled";

        /// <summary>
        /// Indicate if the Hangfire dashboard should be enabled
        /// </summary>
        public const string HangfireEnableDashboard = "hangfire.dashboardenabled";

        /// <summary>
        /// Connection string to use if the SqlServer Storage Engine is used
        /// </summary>
        public const string HangfireSqlConnectionString = "hangfire.sqlconnectionstring";

        /// <summary>
        /// Method used to store Hangfire execution details
        /// </summary>
        public const string HangfireStorageEngine = "hangfire.storageengine";

        /// <summary>
        /// API key for Pushover
        /// </summary>
        public const string PushoverApiToken = "pushover.apitoken";

        /// <summary>
        /// Default device key for Pushover
        /// </summary>
        public const string PushoverDefaultSendKey = "pushover.defaultsendkey";

        /// <summary>
        /// Key indicating if Pushover is enabled
        /// </summary>
        public const string PushoverEnabled = "pushover.isenabled";

        /// <summary>
        /// Host address of the service
        /// </summary>
        public const string ServiceHost = "service.host";

        /// <summary>
        /// The port on which the service is hosted
        /// </summary>
        public const string ServicePort = "service.port";

        /// <summary>
        /// The name of the service as it is registered in the service control manager
        /// </summary>
        public const string ServiceName = "service.name";

        /// <summary>
        /// The display name of the service in the service control manager
        /// </summary>
        public const string ServiceDisplayName = "service.displayname";

        /// <summary>
        /// The description of the service in the service control manager
        /// </summary>
        public const string ServiceDescription = "service.description";

        /// <summary>
        /// Start mode to use when the service is installed
        /// </summary>
        public const string ServiceStartMode = "service.startmode";

        #endregion
    }
}