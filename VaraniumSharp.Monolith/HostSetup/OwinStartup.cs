using Owin;
using Owin.StatelessAuth;
using VaraniumSharp.Attributes;
using VaraniumSharp.Monolith.Hangfire;
using VaraniumSharp.Monolith.Interfaces.Configuration;
using VaraniumSharp.Monolith.Interfaces.HostSetup;
using VaraniumSharp.Monolith.Security;

namespace VaraniumSharp.Monolith.HostSetup
{
    /// <summary>
    /// Class used by Owin to configure middleware during startup
    /// </summary>
    [AutomaticContainerRegistration(typeof(IOwinStartup))]
    public class OwinStartup : IOwinStartup
    {
        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="hangfireConfiguration">Hangfire Configuration</param>
        /// <param name="oAuthTokenValidatorConfiguration">OAuthTokenValidator Configuration</param>
        /// <param name="tokenValidator">TokenValidator instance</param>
        public OwinStartup(IHangfireConfiguration hangfireConfiguration, IOAuthTokenValidatorConfiguration oAuthTokenValidatorConfiguration, ITokenValidator tokenValidator)
        {
            _hangfireConfiguration = hangfireConfiguration;
            _authTokenValidatorConfiguration = oAuthTokenValidatorConfiguration;
            _tokenValidator = tokenValidator;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Set up Owin middleware
        /// </summary>
        /// <param name="app"></param>
        public void Configuration(IAppBuilder app)
        {
            app.UserStatelessOAuthTokenValidation(_tokenValidator, _authTokenValidatorConfiguration);
            app.UseHangfire(_hangfireConfiguration);
            app.UseNancy();
        }

        #endregion

        #region Variables

        private readonly IOAuthTokenValidatorConfiguration _authTokenValidatorConfiguration;

        private readonly IHangfireConfiguration _hangfireConfiguration;
        private readonly ITokenValidator _tokenValidator;

        #endregion
    }
}