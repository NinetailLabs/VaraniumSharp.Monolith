using System.IdentityModel.Tokens.Jwt;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;
using VaraniumSharp.Monolith.Exceptions;

namespace VaraniumSharp.Monolith.Interfaces.Security
{
    /// <summary>
    /// Validates OAuth tokens
    /// </summary>
    public interface IOAuthTokenValidator
    {
        #region Public Methods

        /// <summary>
        /// Initialize the token validator by retrieving the signing keys from the Identity Server
        /// </summary>
        /// <exception cref="SigningKeysException">Thrown if keys could not be retrieved or if their conversion to <see cref="JsonWebKey"/> failed</exception>
        Task InitAsync();

        /// <summary>
        /// Validate the received OAuth token.
        /// </summary>
        /// <param name="token">String containing the token to validate</param>
        /// <returns>Security token if the token is valid</returns>
        JwtSecurityToken ValidateToken(string token);

        #endregion
    }
}