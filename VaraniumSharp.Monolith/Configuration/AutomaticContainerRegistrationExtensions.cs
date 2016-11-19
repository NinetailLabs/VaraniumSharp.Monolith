using Serilog;
using System.Linq;
using VaraniumSharp.DependencyInjection;
using VaraniumSharp.Initiator.Configuration;
using VaraniumSharp.Initiator.DependencyInjection;

namespace VaraniumSharp.Monolith.Configuration
{
    /// <summary>
    /// Extension methods for <see cref="ContainerSetup"/>
    /// </summary>
    public static class AutomaticContainerRegistrationExtensions
    {
        #region Public Methods

        /// <summary>
        /// Set up logging.
        /// This method retrieves all classes that inherits from <see cref="BaseLogConfiguration"/> and executing their Apply method.
        /// Automatically set up Log.Logger
        /// </summary>
        /// <param name="container"></param>
        public static void SetupLogging(this AutomaticContainerRegistration container)
        {
            var loggerConfiguration = new LoggerConfiguration();

            container.ResolveMany<BaseLogConfiguration>()
                .ToList()
                .ForEach(x => x.Apply(loggerConfiguration));

            Log.Logger = loggerConfiguration.CreateLogger();
        }

        #endregion
    }
}