using FluentAssertions;
using HttpMock;
using Microsoft.IdentityModel.Tokens;
using Moq;
using Newtonsoft.Json;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using VaraniumSharp.Monolith.Exceptions;
using VaraniumSharp.Monolith.Interfaces.Configuration;
using VaraniumSharp.Monolith.Security;
using VaraniumSharp.Monolith.Tests.Fixtures;

namespace VaraniumSharp.Monolith.Tests.Security
{
    public class OAuthTokenValidatorTests
    {
        #region Public Methods

        [Test]
        public void IfServerReturnsAnErrorDuringKeyRetrievalAnExceptionIsThrown()
        {
            // arrange
            var fixture = new OAuthTokenValidatorFixture();
            var httpMock = HttpMockRepository.At("http://localhost:6889");
            httpMock
                .Stub(x => x.Get("/keyz"))
                .NotFound();
            httpMock.Start();

            var sut = fixture.Instance;
            var act = new Action(() => sut.InitAsync().Wait());

            // act
            // assert
            act.ShouldThrow<SigningKeysException>();
        }

        [Test]
        public void InvalidServerResponseThrowsAnException()
        {
            // arrange
            var fixture = new OAuthTokenValidatorFixture();
            var httpMock = HttpMockRepository.At("http://localhost:6889");
            httpMock
                .Stub(x => x.Get("/keyz"))
                .Return("")
                .OK();

            var sut = fixture.Instance;
            var act = new Action(() => sut.InitAsync().Wait());

            // act
            // assert
            act.ShouldThrow<SigningKeysException>();
        }

        [Test]
        public async Task TokenThatPassesValidationIsReturned()
        {
            // arrange
            var fixture = new OAuthTokenValidatorFixture();
            var httpMock = HttpMockRepository.At("http://localhost:6889");
            var token = fixture.TokenGenerator.GenerateToken();

            var keys = new WebKeys
            {
                Keys = new List<JsonWebKey> { fixture.TokenGenerator.JsonWebKey }
            };

            httpMock
                .Stub(x => x.Get("/keyz"))
                .Return(JsonConvert.SerializeObject(keys))
                .OK();

            var sut = fixture.Instance;
            await sut.InitAsync();

            // act
            var result = sut.ValidateToken(token);

            // assert
            result.Should().NotBeNull();
            result.Issuer.Should().Be(fixture.ValidIssuer);
        }

        [Test]
        public async Task TokenValidationErrorThrowsAnException()
        {
            // arrange
            var fixture = new OAuthTokenValidatorFixture();
            var httpMock = HttpMockRepository.At("http://localhost:6889");
            var token = fixture.TokenGenerator.GenerateToken(DateTime.UtcNow.AddMinutes(-15), DateTime.UtcNow.AddMinutes(-5), DateTime.UtcNow.AddMinutes(-10));

            var keys = new WebKeys
            {
                Keys = new List<JsonWebKey> { fixture.TokenGenerator.JsonWebKey }
            };

            httpMock
                .Stub(x => x.Get("/keyz"))
                .Return(JsonConvert.SerializeObject(keys))
                .OK();

            var sut = fixture.Instance;
            await sut.InitAsync();
            var act = new Action(() => sut.ValidateToken(token));

            // act
            // assert
            act.ShouldThrow<SecurityTokenExpiredException>();
        }

        #endregion

        [SuppressMessage("ReSharper", "MemberCanBePrivate.Local", Justification = "Test Fixture - Unit tests require access to Mocks")]
        private class OAuthTokenValidatorFixture
        {
            #region Constructor

            public OAuthTokenValidatorFixture()
            {
                OAuthTokenValidatorConfigurationMock
                    .Setup(t => t.SigningKeyUrl)
                    .Returns(SigningKeysUrl);

                OAuthTokenValidatorConfigurationMock
                    .Setup(t => t.RequiresExpirationTime)
                    .Returns(RequiresExpiration);

                OAuthTokenValidatorConfigurationMock
                    .Setup(t => t.TargetAudience)
                    .Returns(TargetAudience);

                OAuthTokenValidatorConfigurationMock
                    .Setup(t => t.ValidIssuer)
                    .Returns(ValidIssuer);

                TokenGenerator = new TokenGenerator(ValidIssuer, TargetAudience);

                Instance = new OAuthTokenValidator(OAuthTokenValidatorConfiguration);
            }

            #endregion

            #region Properties

            public OAuthTokenValidator Instance { get; }

            public IOAuthTokenValidatorConfiguration OAuthTokenValidatorConfiguration => OAuthTokenValidatorConfigurationMock.Object;
            public Mock<IOAuthTokenValidatorConfiguration> OAuthTokenValidatorConfigurationMock { get; } = new Mock<IOAuthTokenValidatorConfiguration>();

            public bool RequiresExpiration => true;

            public string SigningKeysUrl => "http://localhost:6889/keyz";

            public string TargetAudience => "Those people";

            public TokenGenerator TokenGenerator { get; }

            public string ValidIssuer => "https://issuer.com";

            #endregion
        }

        [SuppressMessage("ReSharper", "UnusedAutoPropertyAccessor.Local", Justification = "Required by JsonConvert for serialization")]
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