using Moq;
using Serilog;
using System;

namespace VaraniumSharp.Monolith.Tests.Helpers
{
    public static class LoggerFixtureHelper
    {
        #region Public Methods

        /// <summary>
        /// Set up Log with a fixtured ILogger so log writing can be interrogated
        /// </summary>
        /// <returns>Tuple containing the original ILogger and the new mocked ILogger</returns>
        public static Tuple<ILogger, Mock<ILogger>> SetupLogCatcher()
        {
            var oldLog = Log.Logger;
            var loggerFixture = new Mock<ILogger>();
            //This is used by TopShelf
            loggerFixture.Setup(t => t.ForContext("SourceContext", It.IsAny<string>(), false)).Returns(loggerFixture.Object);
            Log.Logger = loggerFixture.Object;
            return new Tuple<ILogger, Mock<ILogger>>(oldLog, loggerFixture);
        }

        public static void SwitchLogger(ILogger logger)
        {
            Log.Logger = logger;
        }

        #endregion Public Methods
    }
}