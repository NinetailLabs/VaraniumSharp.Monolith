using FluentAssertions;
using NUnit.Framework;
using System.Linq;
using System.Threading;
using VaraniumSharp.Monolith.NancyModules;

namespace VaraniumSharp.Monolith.Tests.NancyModules
{
    public class HealthModuleTests
    {
        #region Public Methods

        [Test]
        public void HealthEndpointRespondsCorrectly()
        {
            // arrange
            var sut = new HealthModule();

            // act
            var result = sut.Routes.First().Action.Invoke("", CancellationToken.None).Result;

            // assert
            ((string)result).Should().Be("Service is online");
        }

        #endregion
    }
}