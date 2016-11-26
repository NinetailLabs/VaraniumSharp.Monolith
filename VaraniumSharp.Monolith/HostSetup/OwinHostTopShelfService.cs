using Microsoft.Owin.Hosting;
using Serilog;
using System;
using VaraniumSharp.Attributes;
using VaraniumSharp.Enumerations;
using VaraniumSharp.Monolith.Interfaces;
using VaraniumSharp.Monolith.Interfaces.HostSetup;

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
        /// <param name="topShelfConfiguration">Topshelf Configuration</param>
        /// <param name="hostConfiguration">Host Configuration</param>
        /// <param name="owinStartup">Owin startup class</param>
        public OwinHostTopShelfService(ITopShelfConfiguration topShelfConfiguration, IHostConfiguration hostConfiguration, IOwinStartup owinStartup)
        {
            TopShelfConfiguration = topShelfConfiguration;
            HostConfiguration = hostConfiguration;
            _logger = Log.Logger.ForContext<OwinHostTopShelfService>();
            _owinStartup = owinStartup;
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
                _webbApp = WebApp.Start(new StartOptions(HostConfiguration.HostUrl), builder => _owinStartup.Configuration(builder));
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

        private readonly IOwinStartup _owinStartup;

        private IDisposable _webbApp;

        #endregion
    }
}