using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using VaraniumSharp.Monolith.Interfaces.Configuration;
using VaraniumSharp.Monolith.Interfaces.Security;
using VaraniumSharp.Monolith.Security;
using VaraniumSharp.Monolith.Tests.Fixtures;

namespace VaraniumSharp.Monolith.Tests.Security
{
    public class ValidateUserTests
    {
        #region Public Methods

        [Test]
        public void IfTokenValidationFailsNullIsReturned()
        {
            // arrange
            var fixture = new ValidateUserFixture();
            const string token = "blah";

            fixture.OAuthTokenValidatorMock
                .Setup(t => t.ValidateToken(token))
                .Throws<Exception>();

            var sut = fixture.Instance;

            // act
            var result = sut.ValidateUser(token);

            // assert
            result.Should().Be(null);
        }

        [Test]
        public void WithValidTokenClaimsPrincipleIsConstructedCorrectly()
        {
            // arrange
            const string nameClaimKey = "Username";
            const string roleClaimKey = "Role";
            const string username = "Test User";
            const string role = "Tester";
            const string authType = "Token";
            var fixture = new ValidateUserFixture();
            var usernameClaimDummy = new Claim(nameClaimKey, username);
            var roleClaimDummy = new Claim(roleClaimKey, role);

            var token = fixture.TokenGenerator.GenerateToken(
                new ClaimsIdentity(new List<Claim> { usernameClaimDummy, roleClaimDummy }));

            fixture.OAuthTokenValidatorConfigurationMock
                .Setup(t => t.ClaimRepresentingUsername)
                .Returns(nameClaimKey);
            fixture.OAuthTokenValidatorConfigurationMock
                .Setup(t => t.ClaimRepresentingRole)
                .Returns(roleClaimKey);
            fixture.OAuthTokenValidatorConfigurationMock
                .Setup(t => t.AuthenticationType)
                .Returns(authType);
            fixture.OAuthTokenValidatorMock
                .Setup(t => t.ValidateToken(token))
                .Returns(new JwtSecurityToken(token));

            var sut = fixture.Instance;

            // act
            var result = sut.ValidateUser(token);

            // assert
            result.Identity.Name.Should().Be(username);
            result.IsInRole(role).Should().BeTrue();
            result.Identity.IsAuthenticated.Should().BeTrue();
        }

        #endregion

        [SuppressMessage("ReSharper", "MemberCanBePrivate.Local", Justification = "Test Fixture - Unit tests require access to Mocks")]
        private class ValidateUserFixture
        {
            #region Constructor

            public ValidateUserFixture()
            {
                TokenGenerator = new TokenGenerator("Issuer", "Audience");
                Instance = new ValidationHandler(OAuthTokenValidator, OAuthTokenValidatorConfiguration);
            }

            #endregion

            #region Properties

            public ValidationHandler Instance { get; }

            public IOAuthTokenValidator OAuthTokenValidator => OAuthTokenValidatorMock.Object;

            public IOAuthTokenValidatorConfiguration OAuthTokenValidatorConfiguration => OAuthTokenValidatorConfigurationMock.Object;

            public Mock<IOAuthTokenValidatorConfiguration> OAuthTokenValidatorConfigurationMock { get; } = new Mock<IOAuthTokenValidatorConfiguration>();
            public Mock<IOAuthTokenValidator> OAuthTokenValidatorMock { get; } = new Mock<IOAuthTokenValidator>();

            public TokenGenerator TokenGenerator { get; }

            #endregion
        }
    }
}