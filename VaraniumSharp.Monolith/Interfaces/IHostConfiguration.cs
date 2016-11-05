namespace VaraniumSharp.Monolith.Interfaces
{
    /// <summary>
    /// Provides configuration values for setting up a Host
    /// </summary>
    public interface IHostConfiguration
    {
        /// <summary>
        /// The address on which the service should be hosted 
        /// <example>http://localhost</example>
        /// </summary>
        string Host { get; }

        /// <summary>
        /// The combined Host:Port on which the service will be hosted
        /// </summary>
        string HostUrl { get; }

        /// <summary>
        /// The port on which the service should be hosted
        /// <example>1337</example>
        /// </summary>
        int Port { get; }
    }
}