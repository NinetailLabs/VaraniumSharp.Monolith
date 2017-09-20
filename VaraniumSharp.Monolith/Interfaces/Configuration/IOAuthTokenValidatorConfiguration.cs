using VaraniumSharp.Monolith.Configuration;

namespace VaraniumSharp.Monolith.Interfaces.Configuration
{
    /// <summary>
    /// Provides configuration for the <see cref="OAuthTokenValidatorConfiguration"/>
    /// </summary>
    public interface IOAuthTokenValidatorConfiguration
    {
        #region Properties

        /// <summary>
        /// Type of authentication
        /// </summary>
        string AuthenticationType { get; }

        /// <summary>
        /// The key of the claim that should be used for the user's role
        /// </summary>
        string ClaimRepresentingRole { get; }

        /// <summary>
        /// The key of the claim that should be used for the user's username
        /// </summary>
        string ClaimRepresentingUsername { get; }

        /// <summary>
        /// Should the validator be used
        /// </summary>
        bool Enabled { get; }

        /// <summary>
        /// Indicate if the token must have an 'expiration' time
        /// </summary>
        bool RequiresExpirationTime { get; }

        /// <summary>
        /// Url where Identity Server's public signing keys can be downloaded
        /// </summary>
        string SigningKeyUrl { get; }

        /// <summary>
        /// Target audience to check against the token's audience
        /// </summary>
        string TargetAudience { get; }

        /// <summary>
        /// Issuer to check against the token's issuer
        /// </summary>
        string ValidIssuer { get; }

        #endregion
    }
}