using FluentAssertions;
using NUnit.Framework;
using System;
using VaraniumSharp.Monolith.Tests.Helpers;
using VaraniumSharp.Monolith.Wrappers;

namespace VaraniumSharp.Monolith.Tests.Wrappers
{
    public class PushoverWrapperTests
    {
        #region Public Methods

        [Test]
        public void ConfigurationWithBothApiTokenAndDefaultSendKeySetsUpBaseClassCorrectly()
        {
            // arrange
            const string apiToken = "RandomKey";
            var config = new PushoverConfigDummy(true)
            {
                ApiToken = apiToken
            };

            // act
            var sut = new PushoverWrapper(config);

            // assert
            sut.Configuration.Should().Be(config);
            sut.AppKey.Should().Be(apiToken);
            sut.DefaultUserGroupSendKey.Should().BeNullOrEmpty();
        }

        [Test]
        public void ConfigurationWithOnlyApiTokenCorrectlySetsUpBaseClass()
        {
            // arrange
            const string apiToken = "RandomKey";
            const string sendKey = "SendKey";
            var config = new PushoverConfigDummy(true)
            {
                ApiToken = apiToken,
                DefaultSendKey = sendKey
            };

            // act
            var sut = new PushoverWrapper(config);

            // assert
            sut.AppKey.Should().Be(apiToken);
            sut.DefaultUserGroupSendKey.Should().Be(sendKey);
        }

        [Test]
        public void TryingToSendWithInvalidDetailsThrowsAnException()
        {
            // arrange
            const string apiToken = "RandomKey";
            var config = new PushoverConfigDummy(true)
            {
                ApiToken = apiToken
            };
            var sut = new PushoverWrapper(config);
            var act = new Action(() =>
            {
                sut.Push("Test", "Test");
            });

            // act
            // assert
            act.ShouldThrow<Exception>();
        }

        #endregion
    }
}