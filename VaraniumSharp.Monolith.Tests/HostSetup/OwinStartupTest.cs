using System.Diagnostics.CodeAnalysis;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using Owin.StatelessAuth;
using VaraniumSharp.Monolith.HostSetup;
using VaraniumSharp.Monolith.Interfaces.Configuration;
using VaraniumSharp.Monolith.Tests.Helpers;

namespace VaraniumSharp.Monolith.Tests.HostSetup
{
    public class OwinStartupTest
    {
        #region Public Methods

        [Test]
        public void RegisterHangfireForStartup()
        {
            // arrange
            var fixture = new OwinStartupFixture();

            fixture.HangfireConfigurationMock
                .Setup(t => t.Enabled)
                .Returns(true);
            fixture.HangfireConfigurationMock
                .Setup(t => t.EnableDashboard)
                .Returns(false);

            var appBuilderDummy = new AppBuilderFixture();
            var sut = fixture.Instance;

            // act
            sut.Configuration(appBuilderDummy);

            // assert
            appBuilderDummy.MiddleWareRegistrationInvocations.Should().Be(1);
        }

        [Test]
        public void RegisterNancyForStartup()
        {
            // arrange
            var fixture = new OwinStartupFixture();
            var sut = fixture.Instance;
            var appBuilderDummy = new AppBuilderFixture();

            // act
            sut.Configuration(appBuilderDummy);

            // assert
            appBuilderDummy.MiddleWareRegistrationInvocations.Should().Be(1);
        }

        public void RegisterOAuthValidatorForStartup()
        {
            // arrange
            var fixture = new OwinStartupFixture();
            var sut = fixture.Instance;
            var appBuilderDummy = new AppBuilderFixture();

            fixture.OAuthTokenValidatorConfigurationMock
                .Setup(t => t.Enabled)
                .Returns(true);

            // act
            sut.Configuration(appBuilderDummy);

            // assert
            appBuilderDummy.MiddleWareRegistrationInvocations.Should().Be(2);
        }

        #endregion

        [SuppressMessage("ReSharper", "MemberCanBePrivate.Local", Justification = "Test Fixture - Unit tests require access to Mocks")]
        private class OwinStartupFixture
        {
            #region Constructor

            public OwinStartupFixture()
            {
                Instance = new OwinStartup(HangfireConfiguration, OAuthTokenValidatorConfiguration, TokenValidator);
            }

            #endregion

            #region Properties

            public IHangfireConfiguration HangfireConfiguration => HangfireConfigurationMock.Object;
            public Mock<IHangfireConfiguration> HangfireConfigurationMock { get; } = new Mock<IHangfireConfiguration>();

            public OwinStartup Instance { get; }

            public IOAuthTokenValidatorConfiguration OAuthTokenValidatorConfiguration => OAuthTokenValidatorConfigurationMock.Object;

            public Mock<IOAuthTokenValidatorConfiguration> OAuthTokenValidatorConfigurationMock { get; } = new Mock<IOAuthTokenValidatorConfiguration>();

            public ITokenValidator TokenValidator => TokenValidatorMock.Object;

            public Mock<ITokenValidator> TokenValidatorMock { get; } = new Mock<ITokenValidator>();

            #endregion
        }
    }
}