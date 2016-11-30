using DryIoc;
using Hangfire;
using System;
using VaraniumSharp.Attributes;

namespace VaraniumSharp.Monolith.Hangfire
{
    /// <summary>
    /// Job Activator for Hangfire to allow Hangfire to resolve classes from the container
    /// </summary>
    [AutomaticContainerRegistration(typeof(JobActivator))]
    public class HangfireContainerJobActivator : JobActivator
    {
        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="container">DryIoc container</param>
        public HangfireContainerJobActivator(IContainer container)
        {
            _container = container;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Called when a job is activated
        /// </summary>
        /// <param name="jobType">The Type of the job to activate</param>
        /// <returns>Requested type</returns>
        public override object ActivateJob(Type jobType)
        {
            return _container.Resolve(jobType);
        }

        #endregion

        #region Variables

        private readonly IContainer _container;

        #endregion
    }
}