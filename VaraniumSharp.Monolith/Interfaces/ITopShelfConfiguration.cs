using Topshelf.Runtime;

namespace VaraniumSharp.Monolith.Configuration
{
    public interface ITopShelfConfiguration
    {
        /// <summary>
        /// The description of the service that will be displayed in the service control manager
        /// </summary>
        string Description { get; }

        /// <summary>
        /// The display name of the service that will be displayed in the service control manager
        /// </summary>
        string DisplayName { get; }

        /// <summary>
        /// The name of the service that will be displayed in the service control manager
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Start mode to use when the service is installed
        /// </summary>
        HostStartMode StartMode { get; }
    }
}