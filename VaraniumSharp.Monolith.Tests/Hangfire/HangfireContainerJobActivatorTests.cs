using DryIoc;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using VaraniumSharp.Monolith.Hangfire;

namespace VaraniumSharp.Monolith.Tests.Hangfire
{
    public class HangfireContainerJobActivatorTests
    {
        #region Public Methods

        [Test]
        public void ActivateJobViaContainer()
        {
            // arrange
            var containerDummy = new Mock<IContainer>();
            var testType = new ContainerContentFixture();

            containerDummy.Setup(t => t.Resolve(typeof(ContainerContentFixture), false)).Returns(testType);

            var sut = new HangfireContainerJobActivator(containerDummy.Object);

            // act
            var result = sut.ActivateJob(typeof(ContainerContentFixture));

            // assert
            result.Should().Be(testType);
        }

        #endregion

        private class ContainerContentFixture
        {}
    }
}