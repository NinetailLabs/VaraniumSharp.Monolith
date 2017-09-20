using System;
using System.Security.Claims;
using Owin.StatelessAuth;
using Serilog;
using VaraniumSharp.Attributes;
using VaraniumSharp.Enumerations;
using VaraniumSharp.Monolith.Interfaces.Configuration;
using VaraniumSharp.Monolith.Interfaces.Security;

namespace VaraniumSharp.Monolith.Security
{
    /// <summary>
    /// Handle OAuth token validation
    /// </summary>
    [AutomaticContainerRegistration(typeof(ITokenValidator), ServiceReuse.Singleton)]
    public class ValidationHandler : ITokenValidator
    {
        #region Constructor

        /// <summary>
        /// DI Constructor
        /// </summary>
        /// <param name="oAuthTokenValidator">OAuthTokenValidator instance</param>
        /// <param name="configuration">OAuthTokenValidatorConfiguration instance</param>
        public ValidationHandler(IOAuthTokenValidator oAuthTokenValidator, IOAuthTokenValidatorConfiguration configuration)
        {
            _oAuthTokenValidator = oAuthTokenValidator;
            _oAuthTokenValidatorConfiguration = configuration;
        }

        #endregion

        #region Public Methods

        public ClaimsPrincipal ValidateUser(string token)
        {
            try
            {
                var vToken = _oAuthTokenValidator.ValidateToken(token);
                var claims = new ClaimsIdentity(
                    vToken.Claims,
                    _oAuthTokenValidatorConfiguration.AuthenticationType,
                    _oAuthTokenValidatorConfiguration.ClaimRepresentingUsername,
                    _oAuthTokenValidatorConfiguration.ClaimRepresentingRole);

                return new ClaimsPrincipal(claims);
            }
            catch (Exception exception)
            {
                Log.Logger.ForContext("Module", nameof(ValidationHandler)).Error(exception, "Error occured during validation of token");
                return null;
            }
        }

        #endregion

        #region Variables

        /// <summary>
        /// OAuthTokenValidator instance
        /// </summary>
        private readonly IOAuthTokenValidator _oAuthTokenValidator;

        /// <summary>
        /// OAuthTokenValidatorConfiguration
        /// </summary>
        private readonly IOAuthTokenValidatorConfiguration _oAuthTokenValidatorConfiguration;

        #endregion
    }
}