using Owin;

namespace VaraniumSharp.Monolith.HostSetup
{
    /// <summary>
    /// Class used by Owin to configure middleware during startup
    /// </summary>
    public class OwinStartup
    {
        #region Public Methods

        /// <summary>
        /// Set up Owin middleware
        /// </summary>
        /// <param name="app"></param>
        public void Configuration(IAppBuilder app)
        {
            app.UseNancy();
        }

        #endregion
    }
}