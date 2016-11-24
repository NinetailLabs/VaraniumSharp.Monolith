using Microsoft.Owin.Hosting;
using Serilog;
using System;
using VaraniumSharp.Attributes;
using VaraniumSharp.Enumerations;
using VaraniumSharp.Monolith.Interfaces;

namespace VaraniumSharp.Monolith.HostSetup
{
    /// <summary>
    /// TopShelf service that set up an Owin instance
    /// </summary>
    [AutomaticContainerRegistration(typeof(ITopShelfService), ServiceReuse.Singleton)]
    public class OwinHostTopShelfService : ITopShelfService
    {
        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="topShelfConfiguration"></param>
        /// <param name="hostConfiguration"></param>
        public OwinHostTopShelfService(ITopShelfConfiguration topShelfConfiguration, IHostConfiguration hostConfiguration)
        {
            TopShelfConfiguration = topShelfConfiguration;
            HostConfiguration = hostConfiguration;
            _logger = Log.Logger.ForContext<OwinHostTopShelfService>();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Get the topShelfConfiguration used by Owin
        /// </summary>
        public IHostConfiguration HostConfiguration { get; }

        /// <summary>
        /// Get the topShelfConfiguration used by the TopShelf service
        /// </summary>
        public ITopShelfConfiguration TopShelfConfiguration { get; }

        #endregion

        #region Public Methods

        /// <summary>
        /// Called when TopShelf service is started
        /// </summary>
        public void Start()
        {
            try
            {
                _logger.Information("Starting on {URL}", HostConfiguration.HostUrl);
                //TODO - Use this to start Owin. This will allow loading OwinStartup class from the DI system instead of new upping it directly which is a win as it allows direct container usage
                //WebApp.Start(new StartOptions(HostConfiguration.HostUrl), builder => (new OwinStartup()).Configuration(builder));
                _webbApp = WebApp.Start<OwinStartup>(HostConfiguration.HostUrl);
                _logger.Information("Startup completed");
            }
            catch (Exception exception)
            {
                _logger.Fatal(exception, "An error occured during startup");
            }
        }

        /// <summary>
        /// Called when TopShelf service is stopped
        /// </summary>
        public void Stop()
        {
            _logger.Information("Shutting down");
            _webbApp?.Dispose();
        }

        #endregion

        #region Variables

        private readonly ILogger _logger;

        private IDisposable _webbApp;

        #endregion
    }
}