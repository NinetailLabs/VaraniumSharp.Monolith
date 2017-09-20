using Owin;
using Owin.StatelessAuth;
using VaraniumSharp.Monolith.Interfaces.Configuration;

namespace VaraniumSharp.Monolith.Security
{
    /// <summary>
    /// Extension methods to allow setting up OAuthTokenHandler validation
    /// </summary>
    public static class StatelessOAuthValidation
    {
        #region Public Methods

        /// <summary>
        /// Apply the stateless OAuthTokenValidator configuration
        /// </summary>
        /// <param name="appBuilder">AppBuilder intance</param>
        /// <param name="tokenValidator">ValidationHandler instance</param>
        /// <param name="configuration">OAuthTokenValidatorConfiguration instance</param>
        public static void UserStatelessOAuthTokenValidation(this IAppBuilder appBuilder,
            ITokenValidator tokenValidator, IOAuthTokenValidatorConfiguration configuration)
        {
            if (configuration.Enabled)
            {
                appBuilder.RequiresStatelessAuth(tokenValidator);
            }
        }

        #endregion
    }
}