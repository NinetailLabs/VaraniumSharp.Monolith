using VaraniumSharp.Attributes;
using VaraniumSharp.Enumerations;
using VaraniumSharp.Extensions;
using VaraniumSharp.Monolith.Interfaces.Configuration;
using ConfigurationKeys = VaraniumSharp.Monolith.Enumerations.ConfigurationKeys;

namespace VaraniumSharp.Monolith.Configuration
{
    /// <summary>
    /// Provides configuration for the <see cref="OAuthTokenValidatorConfiguration"/>
    /// </summary>
    [AutomaticContainerRegistration(typeof(IOAuthTokenValidatorConfiguration), ServiceReuse.Singleton)]
    public class OAuthTokenValidatorConfiguration : IOAuthTokenValidatorConfiguration
    {
        #region Properties

        /// <summary>
        /// Indicate if the token must have an 'expiration' time
        /// </summary>
        public bool RequiresExpirationTime => ConfigurationKeys.OAuthRequireExperationTime.GetConfigurationValue<bool>();

        /// <summary>
        /// Url where Identity Server's public signing keys can be downloaded
        /// </summary>
        public string SigningKeyUrl => ConfigurationKeys.OAuthSigningKeysUrl.GetConfigurationValue<string>();

        /// <summary>
        /// Target audience to check against the token's audience
        /// </summary>
        public string TargetAudience => ConfigurationKeys.OAuthTargetAudiance.GetConfigurationValue<string>();

        /// <summary>
        /// Issuer to check against the token's issuer
        /// </summary>
        public string ValidIssuer => ConfigurationKeys.OAuthValidIssuer.GetConfigurationValue<string>();

        #endregion
    }
}