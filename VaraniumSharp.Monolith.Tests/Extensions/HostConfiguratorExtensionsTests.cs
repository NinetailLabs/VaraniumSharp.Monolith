using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using Topshelf.HostConfigurators;
using VaraniumSharp.Monolith.Extensions;

namespace VaraniumSharp.Monolith.Tests.Extensions
{
    public class HostConfiguratorExtensionsTests
    {
        #region Public Methods

        [Test]
        public void ApplyCommandLineContainingOnlyValidValues()
        {
            // arrange
            const string verb = "install";
            const string option = "-username:test";
            const string @switch = "--autostart";

            var expectedCommandLine = $"{verb} {option} {@switch}";
            var configurator = new Mock<HostConfigurator>();
            var arguments = new List<string>
            {
                verb,
                option,
                @switch
            };

            // act
            configurator.Object.ApplyValidCommandLine(arguments);

            //assert
            configurator.Verify(t => t.ApplyCommandLine(expectedCommandLine), Times.Once);
        }

        [Test]
        public void ApplyCommandLineThatContainsInvalidValues()
        {
            // arrange
            const string verb = "install";
            const string option = "-username:test";
            const string @switch = "--autostart";

            var expectedCommandLine = $"{verb} {option} {@switch}";
            var configurator = new Mock<HostConfigurator>();
            var arguments = new List<string>
            {
                verb,
                "Some",
                option,
                "127.0.0.1",
                @switch,
                "Rubbish"
            };

            // act
            configurator.Object.ApplyValidCommandLine(arguments);

            //assert
            configurator.Verify(t => t.ApplyCommandLine(expectedCommandLine), Times.Once);
        }

        [Test]
        public void ApplySingleValidOptionToCommandLine()
        {
            // arrange
            const string option = "-username:test";
            var configurator = new Mock<HostConfigurator>();
            var arguments = new List<string>
            {
                option
            };

            // act
            configurator.Object.ApplyValidCommandLine(arguments);

            //assert
            configurator.Verify(t => t.ApplyCommandLine(option), Times.Once);
        }

        [Test]
        public void ApplySingleValidSwitch()
        {
            // arrange
            const string @switch = "--autostart";
            var configurator = new Mock<HostConfigurator>();
            var arguments = new List<string>
            {
                @switch
            };

            // act
            configurator.Object.ApplyValidCommandLine(arguments);

            // assert
            configurator.Verify(t => t.ApplyCommandLine(@switch), Times.Once);
        }

        [Test]
        public void ApplySingleValidVerb()
        {
            // arrange
            const string verb = "run";
            var configurator = new Mock<HostConfigurator>();
            var arguments = new List<string>
            {
                verb
            };

            // act
            configurator.Object.ApplyValidCommandLine(arguments);

            // assert
            configurator.Verify(t => t.ApplyCommandLine(verb), Times.Once);
        }

        #endregion
    }
}