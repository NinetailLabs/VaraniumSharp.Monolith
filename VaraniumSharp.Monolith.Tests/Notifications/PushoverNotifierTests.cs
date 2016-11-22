using FluentAssertions;
using Moq;
using NUnit.Framework;
using PushoverClient;
using System;
using VaraniumSharp.Monolith.Interfaces.Wrappers;
using VaraniumSharp.Monolith.Notifications;
using VaraniumSharp.Monolith.Tests.Helpers;

namespace VaraniumSharp.Monolith.Tests.Notifications
{
    public class PushoverNotifierTests
    {
        #region Public Methods

        //TODO - Add a test that sends successfully

        [Test]
        public void CannotSendPushIfPushoverIsDisabled()
        {
            // arrange
            const string apiToken = "fakeToken";
            const string sendkey = "Send";
            var configuration = new PushoverConfigDummy(false)
            {
                ApiToken = apiToken,
                DefaultSendKey = sendkey
            };
            var wrapperDummy = new Mock<IPushoverWrapper>();
            var logTuple = LoggerFixtureHelper.SetupLogCatcher();

            wrapperDummy.Setup(t => t.Configuration).Returns(configuration);

            var sut = new PushoverNotifier(wrapperDummy.Object);

            // act
            sut.SendNotification("Test", "Test");

            // assert
            logTuple.Item2.Verify(t => t.Verbose("Cannot send Push, Pushover is disabled"), Times.Once);

            LoggerFixtureHelper.SwitchLogger(logTuple.Item1);
        }

        [Test]
        public void PushIsNotSendIfNoDefaultKeyIsSetAndSendIsAttemptedWithoutASendKey()
        {
            // arrange
            var logTuple = LoggerFixtureHelper.SetupLogCatcher();
            const string apiToken = "fakeToken";
            var configuration = new PushoverConfigDummy(false)
            {
                ApiToken = apiToken
            };

            var wrapperDummy = new Mock<IPushoverWrapper>();
            wrapperDummy.Setup(t => t.Configuration).Returns(configuration);

            var sut = new PushoverNotifier(wrapperDummy.Object);

            // act
            sut.SendNotification("Test", "Test");

            // assert
            logTuple.Item2.Verify(t => t.Warning("DefaultSendKey is not provided and no send key was provided. Cannot send Push"), Times.Once);

            LoggerFixtureHelper.SwitchLogger(logTuple.Item1);
        }

        [Test]
        public void PushLogsErrorIfServerResponseIsInvalid()
        {
            // arrange
            var logTuple = LoggerFixtureHelper.SetupLogCatcher();
            const string apiToken = "fakeToken";
            const string sendkey = "Send";
            var configuration = new PushoverConfigDummy(true)
            {
                ApiToken = apiToken,
                DefaultSendKey = sendkey
            };

            var wrapperDummy = new Mock<IPushoverWrapper>();
            wrapperDummy.Setup(t => t.Configuration).Returns(configuration);
            wrapperDummy.Setup(t => t.Push("Test", "Test", "", "")).Throws<Exception>();

            var sut = new PushoverNotifier(wrapperDummy.Object);

            // act
            sut.SendNotification("Test", "Test");

            // assert
            logTuple.Item2.Verify(t => t.Error(It.IsAny<Exception>(), "An error occured while trying to send a Push"), Times.Once);

            LoggerFixtureHelper.SwitchLogger(logTuple.Item1);
        }

        [Test]
        public void PushNotification()
        {
            // arrange
            var logTuple = LoggerFixtureHelper.SetupLogCatcher();
            const string apiToken = "fakeToken";
            const string sendkey = "Send";
            var configuration = new PushoverConfigDummy(true)
            {
                ApiToken = apiToken,
                DefaultSendKey = sendkey
            };

            var wrapperDummy = new Mock<IPushoverWrapper>();
            wrapperDummy.Setup(t => t.Configuration).Returns(configuration);
            wrapperDummy.Setup(t => t.Push("Test", "Test", sendkey, "")).Returns(new PushResponse());

            var sut = new PushoverNotifier(wrapperDummy.Object);

            // act
            sut.SendNotification("Test", "Test");

            // assert
            wrapperDummy.Verify(t => t.Push("Test", "Test", "", ""), Times.Once);
            logTuple.Item2.Verify(t => t.Debug("Push sent"), Times.Once);

            LoggerFixtureHelper.SwitchLogger(logTuple.Item1);
        }

        [Test]
        public void PushoverDoesNotActivateIfDisabledInSettings()
        {
            // arrange
            var logTuple = LoggerFixtureHelper.SetupLogCatcher();
            var config = new PushoverConfigDummy(false);

            var wrapperDummy = new Mock<IPushoverWrapper>();
            wrapperDummy.Setup(t => t.Configuration).Returns(config);

            // act
            var sut = new PushoverNotifier(wrapperDummy.Object);

            // assert
            sut.IsEnabled.Should().BeFalse();
            logTuple.Item2.Verify(t => t.Information("Setting up Pushover client"), Times.Never);

            LoggerFixtureHelper.SwitchLogger(logTuple.Item1);
        }

        [Test]
        public void PushoverIsSetupCorrectly()
        {
            // arrange
            var logTuple = LoggerFixtureHelper.SetupLogCatcher();
            const string apiToken = "fakeToken";
            var config = new PushoverConfigDummy(true)
            {
                ApiToken = apiToken
            };

            var wrapperDummy = new Mock<IPushoverWrapper>();
            wrapperDummy.Setup(t => t.Configuration).Returns(config);

            // act
            var sut = new PushoverNotifier(wrapperDummy.Object);

            // assert
            sut.IsEnabled.Should().BeTrue();
            LoggerFixtureHelper.SwitchLogger(logTuple.Item1);
        }

        #endregion
    }
}