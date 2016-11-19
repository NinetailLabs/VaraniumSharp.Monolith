using VaraniumSharp.Attributes;
using VaraniumSharp.Enumerations;
using VaraniumSharp.Extensions;
using VaraniumSharp.Monolith.Enumerations;
using VaraniumSharp.Monolith.Interfaces;

namespace VaraniumSharp.Monolith.Configuration
{
    /// <summary>
    /// Provides configuration values for setting up a Host
    /// </summary>
    [AutomaticContainerRegistration(typeof(IHostConfiguration), ServiceReuse.Singleton)]
    public class HostConfiguration : IHostConfiguration
    {
        #region Properties

        /// <summary>
        /// The address on which the service should be hosted
        /// <example>http://localhost</example>
        /// </summary>
        public string Host => ConfigurationKeys.ServiceHost.GetConfigurationValue<string>();

        /// <summary>
        /// The combined Host:Port on which the service will be hosted
        /// </summary>
        public string HostUrl => $"{Host}:{Port}";

        /// <summary>
        /// The port on which the service should be hosted
        /// <example>1337</example>
        /// </summary>
        public int Port => ConfigurationKeys.ServicePort.GetConfigurationValue<int>();

        #endregion
    }
}