using Owin;
using VaraniumSharp.Attributes;
using VaraniumSharp.Monolith.Interfaces.Configuration;
using VaraniumSharp.Monolith.Interfaces.HostSetup;

namespace VaraniumSharp.Monolith.HostSetup
{
    /// <summary>
    /// Class used by Owin to configure middleware during startup
    /// </summary>
    [AutomaticContainerRegistration(typeof(IOwinStartup))]
    public class OwinStartup : IOwinStartup
    {
        #region Constructor

        public OwinStartup(IHangfireConfiguration hangfireConfiguration)
        {
            _hangfireConfiguration = hangfireConfiguration;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Set up Owin middleware
        /// </summary>
        /// <param name="app"></param>
        public void Configuration(IAppBuilder app)
        {
            app.UseNancy();
            app.UseHangfire(_hangfireConfiguration);
        }

        #endregion

        #region Variables

        private readonly IHangfireConfiguration _hangfireConfiguration;

        #endregion
    }
}