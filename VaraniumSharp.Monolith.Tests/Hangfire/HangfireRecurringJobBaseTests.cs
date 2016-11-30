using FluentAssertions;
using Hangfire;
using NUnit.Framework;
using System;
using System.Threading;
using VaraniumSharp.Monolith.Hangfire;

namespace VaraniumSharp.Monolith.Tests.Hangfire
{
    public class HangfireRecurringJobBaseTests
    {
        #region Public Methods

        [Test]
        public void SettingUpReccuringBaseJobWithContructorWorksCorrectly()
        {
            // arrange
            // act
            var sut = new HangfireRecurringFixture(Cron.Daily());

            // assert
            sut.CronSchedule.Should().Be(Cron.Daily());
        }

        [Test]
        public void SetupRecurringJob()
        {
            // arrange
            var wasExecuted = false;
            var sut = new HangfireRecurringFixture();
            HangfireHelperFixture.RegisterType(typeof(HangfireRecurringFixture), ref sut);

            sut.ActionToExecute += () =>
            {
                wasExecuted = true;
            };

            // act
            sut.Setup();
            Thread.Sleep(65000);

            // assert
            wasExecuted.Should().BeTrue();
        }

        #endregion

        private class HangfireRecurringFixture : HangfireReccuringJobBase
        {
            #region Constructor

            public HangfireRecurringFixture()
            {
                CronSchedule = Cron.Minutely();
            }

            public HangfireRecurringFixture(string cron)
                : base(cron)
            {
            }

            #endregion

            #region Properties

            public Action ActionToExecute { get; set; }

            #endregion

            #region Public Methods

            public override void Execute()
            {
                ActionToExecute.Invoke();
            }

            #endregion
        }
    }
}