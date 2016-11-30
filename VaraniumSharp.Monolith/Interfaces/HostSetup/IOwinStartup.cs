using Owin;

namespace VaraniumSharp.Monolith.Interfaces.HostSetup
{
    /// <summary>
    /// OwinStartup inteface
    /// </summary>
    public interface IOwinStartup
    {
        #region Public Methods

        /// <summary>
        /// Set up Owin middleware
        /// </summary>
        /// <param name="app"></param>
        void Configuration(IAppBuilder app);

        #endregion
    }
}