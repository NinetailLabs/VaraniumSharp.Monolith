using System;
using FluentAssertions;
using HttpMock;
using Moq;
using NUnit.Framework;
using VaraniumSharp.Monolith.Exceptions;
using VaraniumSharp.Monolith.Interfaces.Configuration;
using VaraniumSharp.Monolith.Security;

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

        #endregion

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

            public string ValidIssuer => "https://issuer.com";

            #endregion
        }

        // TODO - Test that valid response is treated correctly

        // TODO - Test token validation (How are we even going to do this???)
    }
}