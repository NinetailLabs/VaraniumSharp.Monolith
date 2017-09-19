using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http;
using System.Threading.Tasks;
using VaraniumSharp.Monolith.Exceptions;
using VaraniumSharp.Monolith.Interfaces.Configuration;

namespace VaraniumSharp.Monolith.Security
{
    /// <summary>
    /// Validates OAuth tokens
    /// </summary>
    public class OAuthTokenValidator
    {
        #region Constructor

        /// <summary>
        /// DI Constructor
        /// </summary>
        /// <param name="oAuthTokenValidatorConfiguration">Validator Configuration</param>
        public OAuthTokenValidator(IOAuthTokenValidatorConfiguration oAuthTokenValidatorConfiguration)
        {
            _authTokenValidatorConfiguration = oAuthTokenValidatorConfiguration;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Initialize the token validator by retrieving the signing keys from the Identity Server
        /// </summary>
        /// <exception cref="SigningKeysException">Thrown if keys could not be retrieved or if their conversion to <see cref="JsonWebKey"/> failed</exception>
        public async Task InitAsync()
        {
            try
            {
                using (var client = new HttpClient())
                {
                    var result = await client.GetAsync(_authTokenValidatorConfiguration.SigningKeyUrl);
                    if (!result.IsSuccessStatusCode)
                    {
                        throw new SigningKeysException(
                            $"An error occured while attempting to retrieve Identity Server public keys from {_authTokenValidatorConfiguration.SigningKeyUrl}. The server responded with {result.StatusCode} {result.ReasonPhrase}");
                    }

                    var keyz = JsonConvert.DeserializeObject<WebKeys>(await result.Content.ReadAsStringAsync());
                    _validationParameters = new TokenValidationParameters
                    {
                        ValidateAudience = true,
                        ValidateIssuerSigningKey = true,
                        ValidateIssuer = true,
                        RequireExpirationTime = _authTokenValidatorConfiguration.RequiresExpirationTime,
                        ValidAudience = _authTokenValidatorConfiguration.TargetAudience,
                        IssuerSigningKeys = keyz.Keys,
                        ValidIssuer = _authTokenValidatorConfiguration.ValidIssuer,
                        ValidateLifetime = _authTokenValidatorConfiguration.RequiresExpirationTime
                    };
                }
            }
            catch (Exception exception)
            {
                throw new SigningKeysException($"An error occured while retrieving signing keys from Identity Server at {_authTokenValidatorConfiguration.SigningKeyUrl}.\r\nSee InnerException for more details", exception);
            }
        }

        /// <summary>
        /// Validate the received OAuth token.
        /// </summary>
        /// <param name="token">String containing the token to validate</param>
        /// <returns>Security token if the token is valid</returns>
        public JwtSecurityToken ValidateToken(string token)
        {
            // See https://auth0.com/docs/api-auth/tutorials/verify-access-token
            SecurityToken sToken;
            var tokenHandler = new JwtSecurityTokenHandler();
            tokenHandler.ValidateToken(token, _validationParameters, out sToken);

            return new JwtSecurityToken(token);
        }

        #endregion

        #region Variables

        /// <summary>
        /// Configuration instance
        /// </summary>
        private readonly IOAuthTokenValidatorConfiguration _authTokenValidatorConfiguration;

        /// <summary>
        /// Parameters to use for Token validation
        /// </summary>
        private TokenValidationParameters _validationParameters;

        #endregion

        /// <summary>
        /// Class used to deserialize the Json keys received from the server
        /// </summary>
        [SuppressMessage("ReSharper", "ClassNeverInstantiated.Local", Justification = "Only ever deserialized into")]
        [SuppressMessage("ReSharper", "UnusedAutoPropertyAccessor.Local", Justification = "Deserialized into by Json")]
        private class WebKeys
        {
            #region Properties

            /// <summary>
            /// Collection of Keys for the server
            /// </summary>
            public List<JsonWebKey> Keys { get; set; }

            #endregion
        }
    }
}