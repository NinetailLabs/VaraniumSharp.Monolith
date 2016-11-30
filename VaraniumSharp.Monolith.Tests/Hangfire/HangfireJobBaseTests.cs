using FluentAssertions;
using NUnit.Framework;
using System;
using System.Threading;
using VaraniumSharp.Monolith.Hangfire;

namespace VaraniumSharp.Monolith.Tests.Hangfire
{
    public class HangfireJobBaseTests
    {
        #region Public Methods

        [Test]
        public void SetUpFireAndForgetJob()
        {
            // arrange
            var wasExecuted = false;
            var sut = new HangfireJobBaseFixture();
            HangfireHelperFixture.RegisterType(typeof(HangfireJobBaseFixture), ref sut);

            sut.TestInvokeAction += () =>
            {
                wasExecuted = true;
            };

            // act
            sut.Setup();
            //TODO - This makes the test brittle, think of a better way to do this
            Thread.Sleep(30000);

            // assert
            wasExecuted.Should().BeTrue();
        }

        #endregion

        private class HangfireJobBaseFixture : HangfireJobBase
        {
            #region Properties

            public Action TestInvokeAction { get; set; }

            #endregion

            #region Public Methods

            public override void Execute()
            {
                TestInvokeAction.Invoke();
            }

            #endregion
        }
    }
}