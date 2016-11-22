using FluentAssertions;
using Moq;
using NUnit.Framework;
using System;
using VaraniumSharp.Monolith.Interfaces;
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
            var logTuple = LoggerFixtureHelper.SetupLogCatcher();
            var sut = new PushoverNotifier(configuration);

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
            const string apiToken = "fakeToken";
            var configuration = new PushoverConfigDummy(false)
            {
                ApiToken = apiToken
            };
            var logTuple = LoggerFixtureHelper.SetupLogCatcher();
            var sut = new PushoverNotifier(configuration);

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
            const string apiToken = "fakeToken";
            const string sendkey = "Send";
            var configuration = new PushoverConfigDummy(true)
            {
                ApiToken = apiToken,
                DefaultSendKey = sendkey
            };
            var logTuple = LoggerFixtureHelper.SetupLogCatcher();
            var sut = new PushoverNotifier(configuration);

            // act
            sut.SendNotification("Test", "Test");

            // assert
            logTuple.Item2.Verify(t => t.Error(It.IsAny<Exception>(), "An error occured while trying to send a Push"), Times.Once);

            LoggerFixtureHelper.SwitchLogger(logTuple.Item1);
        }

        [Test]
        public void PushoverDoesNotActivateIfDisabledInSettings()
        {
            // arrange
            var config = new PushoverConfigDummy(false);
            var logTuple = LoggerFixtureHelper.SetupLogCatcher();

            // act
            var sut = new PushoverNotifier(config);

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

            // act
            var sut = new PushoverNotifier(config);

            // assert
            sut.IsEnabled.Should().BeTrue();
            LoggerFixtureHelper.SwitchLogger(logTuple.Item1);
        }

        #endregion

        private class PushoverConfigDummy : IPushoverConfiguration
        {
            #region Constructor

            public PushoverConfigDummy(bool enable)
            {
                IsEnabled = enable;
            }

            #endregion

            #region Properties

            public string ApiToken { get; set; }
            public string DefaultSendKey { get; set; }
            public bool IsEnabled { get; }

            #endregion
        }
    }
}