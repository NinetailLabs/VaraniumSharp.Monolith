using System.Collections.Generic;

namespace VaraniumSharp.Monolith.Enumerations
{
    /// <summary>
    /// Contains command line argments used by TopShelf
    /// <see>
    ///     <cref>http://docs.topshelf-project.com/en/latest/overview/commandline.html</cref>
    /// </see>
    /// </summary>
    public static class TopShelfCommandLineArguments
    {
        #region Variables

        /// <summary>
        /// Verbs that can be used when starting the TopShelf service
        /// </summary>
        public static List<string> Verbs = new List<string>
        {
            "run",
            "help",
            "-help",
            "install",
            "start",
            "stop",
            "uninstall"
        };

        /// <summary>
        /// Options that can be used when starting the TopShelf service.
        /// Options are seperated from the values they provide by `:`
        /// </summary>
        public static List<string> Options = new List<string>
        {
            "-username",
            "-password",
            "-instance",
            "-servicename",
            "-description",
            "-displayname"
        };

        /// <summary>
        /// Switches that can be used when starting the TopShelf service
        /// </summary>
        public static List<string> Switches = new List<string>
        {
            "--autostart",
            "--disabled",
            "--manual",
            "--delayed",
            "--localsystem",
            "--networkservice",
            "--interactive",
            "--sudo"
        };

        #endregion
    }
}