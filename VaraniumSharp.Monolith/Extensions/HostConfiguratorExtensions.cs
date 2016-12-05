using System.Collections.Generic;
using System.Linq;
using Topshelf.HostConfigurators;
using VaraniumSharp.Monolith.Enumerations;

namespace VaraniumSharp.Monolith.Extensions
{
    /// <summary>
    /// Extension methods for <see cref="HostConfigurator"/>
    /// </summary>
    public static class HostConfiguratorExtensions
    {
        #region Public Methods

        /// <summary>
        /// Apply valid TopShelf command line parameters to the TopShelf configurator
        /// </summary>
        /// <param name="configurator">TopShelf HostConfigurator</param>
        /// <param name="arguments">List of command line arguments</param>
        public static void ApplyValidCommandLine(this HostConfigurator configurator, List<string> arguments)
        {
            var validCommandlineParameters = new List<string>();

            validCommandlineParameters.AddRange(arguments.Where(t => TopShelfCommandLineArguments.Verbs.Contains(t)));
            validCommandlineParameters.AddRange(arguments.Where(t => TopShelfCommandLineArguments.Options.FirstOrDefault(t.StartsWith) != null));
            validCommandlineParameters.AddRange(arguments.Where(t => TopShelfCommandLineArguments.Switches.FirstOrDefault(t.StartsWith) != null));

            configurator.ApplyCommandLine(string.Join(" ", validCommandlineParameters));
        }

        #endregion
    }
}