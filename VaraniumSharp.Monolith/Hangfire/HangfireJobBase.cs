using Hangfire;
using VaraniumSharp.Attributes;

namespace VaraniumSharp.Monolith.Hangfire
{
    /// <summary>
    /// Base Job class for use with Hangfire.
    /// Implementing the class directly will result in a fire-and-forget job, otherwise Setup can be overwritten to implement other job types
    /// </summary>
    [AutomaticConcretionContainerRegistration]
    public abstract class HangfireJobBase
    {
        #region Public Methods

        /// <summary>
        /// This is the method that will be executed by Hangfire
        /// </summary>
        // ReSharper disable once MemberCanBeProtected.Global
        public abstract void Execute();

        /// <summary>
        /// By overriding in Inherited classes the Hangfire job can be configured differently.
        /// If not overridden the job will be executed as a fire-and-forget job.
        /// </summary>
        public virtual void Setup()
        {
            BackgroundJob.Enqueue(() => Execute());
        }

        #endregion
    }
}