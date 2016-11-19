using Nancy;
using VaraniumSharp.Attributes;
using VaraniumSharp.DependencyInjection;

namespace VaraniumSharp.Monolith.NancyModules
{
    /// <summary>
    /// Provide default Health endpoint
    /// </summary>
    [AutomaticContainerRegistration(typeof(NancyModule))]
    // ReSharper disable once ClassWithVirtualMembersNeverInherited.Global
    public class HealthModule : NancyModule
    {
        #region Constructor

        /// <summary>
        /// Parameterless Constructor
        /// </summary>
        public HealthModule()
            : base("health")
        {
            Get[""] = parameters => HealthResult();
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Basic response for calls to health endpoint
        /// </summary>
        /// <returns></returns>
        protected virtual string HealthResult()
        {
            return "Service is online";
        }

        #endregion
    }
}