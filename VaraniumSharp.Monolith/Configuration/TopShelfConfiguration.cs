using Topshelf.Runtime;
using VaraniumSharp.Attributes;
using VaraniumSharp.Enumerations;
using VaraniumSharp.Extensions;
using VaraniumSharp.Monolith.Enumerations;
using VaraniumSharp.Monolith.Interfaces;

namespace VaraniumSharp.Monolith.Configuration
{
    /// <summary>
    /// Provides configuration values for setting up TopShelf hosting
    /// </summary>
    [AutomaticContainerRegistration(typeof(ITopShelfConfiguration), ServiceReuse.Singleton)]
    public class TopShelfConfiguration : ITopShelfConfiguration
    {
        #region Properties

        /// <summary>
        /// The description of the service that will be displayed in the service control manager
        /// </summary>
        public string Description => ConfigurationKeys.ServiceDescription.GetConfigurationValue<string>();

        /// <summary>
        /// The display name of the service that will be displayed in the service control manager
        /// </summary>
        public string DisplayName => ConfigurationKeys.ServiceDisplayName.GetConfigurationValue<string>();

        /// <summary>
        /// The name of the service that will be displayed in the service control manager
        /// </summary>
        public string Name => ConfigurationKeys.ServiceName.GetConfigurationValue<string>();

        /// <summary>
        /// Start mode to use when the service is installed
        /// </summary>
        public HostStartMode StartMode => ConfigurationKeys.ServiceStartMode.GetConfigurationValue<HostStartMode>();

        #endregion
    }
}