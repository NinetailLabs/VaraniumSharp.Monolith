using Hangfire;

namespace VaraniumSharp.Monolith.Hangfire
{
    /// <summary>
    /// Base Recurring Job class for use with Hangfire.
    /// Implementing this class will result in a recurring job based on the provided Cron expression
    /// </summary>
    public abstract class HangfireReccuringJobBase : HangfireJobBase
    {
        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="cronSchedule">Cron expression for recurring job. <see>
        ///         <cref>https://en.wikipedia.org/wiki/Cron#CRON_expression</cref>
        ///     </see>
        /// </param>
        protected HangfireReccuringJobBase(string cronSchedule)
        {
            CronSchedule = cronSchedule;
        }

        /// <summary>
        /// Parameterless contructor
        /// </summary>
        protected HangfireReccuringJobBase()
        { }

        #endregion

        #region Properties

        /// <summary>
        /// Cron expression for recurring job.
        /// <see>
        ///     <cref>https://en.wikipedia.org/wiki/Cron#CRON_expression</cref>
        /// </see>
        /// </summary>
        public string CronSchedule { get; set; }

        #endregion

        #region Public Methods

        /// <summary>
        /// By overriding in Inherited classes the Hangfire job can be configured differently.
        /// If not overridden the job will be executed as a recurring job
        /// </summary>
        public override void Setup()
        {
            RecurringJob.AddOrUpdate(() => Execute(), CronSchedule);
        }

        #endregion
    }
}