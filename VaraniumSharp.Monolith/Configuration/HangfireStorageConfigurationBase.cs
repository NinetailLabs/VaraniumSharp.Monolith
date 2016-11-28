using Hangfire;
using System;
using VaraniumSharp.Attributes;
using VaraniumSharp.Enumerations;

namespace VaraniumSharp.Monolith.Configuration
{
    /// <summary>
    /// Base class for Hangfire storage provider configuration
    /// </summary>
    [AutomaticConcretionContainerRegistration(ServiceReuse.Singleton)]
    public abstract class HangfireStorageConfigurationBase
    {
        #region Properties

        /// <summary>
        /// Is the Storage Provider active
        /// </summary>
        public bool IsActive { get; protected set; }

        /// <summary>
        /// Indicate if the configuration has been applied
        /// </summary>
        public bool WasApplied { get; private set; }

        #endregion

        #region Public Methods

        public void Apply(IGlobalConfiguration hangfireConfiguration)
        {
            lock (_applyLock)
            {
                if (WasApplied)
                {
                    throw new InvalidOperationException("Cannot reapply configuration, it has already been applied");
                }

                ProviderSetup(hangfireConfiguration);

                WasApplied = true;
            }
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// This method is called during the apply method.
        /// It shoudl be overwritten and the appropriate configuration applied
        /// </summary>
        protected abstract void ProviderSetup(IGlobalConfiguration hangfireConfiguration);

        #endregion

        #region Variables

        private readonly object _applyLock = new object();

        #endregion
    }
}