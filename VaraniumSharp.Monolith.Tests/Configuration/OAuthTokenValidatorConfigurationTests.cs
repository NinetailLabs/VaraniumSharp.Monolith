using FluentAssertions;
using NUnit.Framework;
using VaraniumSharp.Monolith.Configuration;
using VaraniumSharp.Monolith.Enumerations;
using VaraniumSharp.Monolith.Tests.Helpers;

namespace VaraniumSharp.Monolith.Tests.Configuration
{
    public class OAuthTokenValidatorConfigurationTests
    {
        #region Public Methods

        [Test]
        public void ConfigurationIsLoadedCorrectly()
        {
            // arrange
            const bool requiresExpirationTime = false;
            const string signingUrl = "https://faketokens.com";
            const string targetAudience = "Testers";
            const string validIssuer = "https://valid.com";
            const string authType = "Token";
            const string userClaim = "Username";
            const string roleClaim = "User";

            ApplicationConfigurationHelper.AdjustKeys(ConfigurationKeys.OAuthRequireExperationTime, requiresExpirationTime.ToString());
            ApplicationConfigurationHelper.AdjustKeys(ConfigurationKeys.OAuthSigningKeysUrl, signingUrl);
            ApplicationConfigurationHelper.AdjustKeys(ConfigurationKeys.OAuthTargetAudiance, targetAudience);
            ApplicationConfigurationHelper.AdjustKeys(ConfigurationKeys.OAuthValidIssuer, validIssuer);
            ApplicationConfigurationHelper.AdjustKeys(ConfigurationKeys.OAuthAuthenticationType, authType);
            ApplicationConfigurationHelper.AdjustKeys(ConfigurationKeys.OAuthClaimRepresetingUsername, userClaim);
            ApplicationConfigurationHelper.AdjustKeys(ConfigurationKeys.OAuthClaimRepresentingRole, roleClaim);
            ApplicationConfigurationHelper.AdjustKeys(ConfigurationKeys.OAuthAuthenticationEnabled, true.ToString());

            // act
            var sut = new OAuthTokenValidatorConfiguration();

            // assert
            sut.SigningKeyUrl.Should().Be(signingUrl);
            sut.RequiresExpirationTime.Should().Be(requiresExpirationTime);
            sut.TargetAudience.Should().Be(targetAudience);
            sut.ValidIssuer.Should().Be(validIssuer);
            sut.AuthenticationType.Should().Be(authType);
            sut.ClaimRepresentingUsername.Should().Be(userClaim);
            sut.ClaimRepresentingRole.Should().Be(roleClaim);
            sut.Enabled.Should().BeTrue();
        }

        #endregion
    }
}