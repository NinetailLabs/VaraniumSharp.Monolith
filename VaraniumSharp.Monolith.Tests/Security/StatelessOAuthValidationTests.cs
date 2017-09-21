using FluentAssertions;
using Moq;
using Nancy.Security;
using NUnit.Framework;
using Owin;
using Owin.StatelessAuth;
using VaraniumSharp.Monolith.Interfaces.Configuration;
using VaraniumSharp.Monolith.Security;
using VaraniumSharp.Monolith.Tests.Helpers;

namespace VaraniumSharp.Monolith.Tests.Security
{
    public class StatelessOAuthValidationTests
    {
        #region Public Methods

        [Test]
        public void DisablingStatelessAuthWorksCorrectly()
        {
            // arrange
            var appBuilderMock = new AppBuilderFixture();
            var configurationMock = new Mock<IOAuthTokenValidatorConfiguration>();
            var validatorMock = new Mock<ITokenValidator>();

            // act
            appBuilderMock.UserStatelessOAuthTokenValidation(validatorMock.Object, configurationMock.Object);

            // assert
            appBuilderMock.MiddleWareRegistrationInvocations.Should().Be(0);
        }

        [Test]
        public void EnablingStatelessAuthWorkCorrectly()
        {
            // arrange
            var appBuilderMock = new AppBuilderFixture();
            var configurationMock = new Mock<IOAuthTokenValidatorConfiguration>();
            var validatorMock = new Mock<ITokenValidator>();

            configurationMock
                .Setup(t => t.Enabled)
                .Returns(true);

            // act
            appBuilderMock.UserStatelessOAuthTokenValidation(validatorMock.Object, configurationMock.Object);

            // assert
            appBuilderMock.MiddleWareRegistrationInvocations.Should().Be(1);
        }

        #endregion
    }
}