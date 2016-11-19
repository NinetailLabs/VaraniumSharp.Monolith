namespace VaraniumSharp.Monolith.Interfaces
{
    /// <summary>
    /// TopShelf Service interface
    /// </summary>
    public interface ITopShelfService
    {
        #region Properties

        /// <summary>
        /// Get the topShelfConfiguration used by Owin
        /// </summary>
        IHostConfiguration HostConfiguration { get; }

        /// <summary>
        /// Gets the configuration used by the TopShelf service
        /// </summary>
        ITopShelfConfiguration TopShelfConfiguration { get; }

        #endregion

        #region Public Methods

        /// <summary>
        /// Called when TopShelf service is started
        /// </summary>
        void Start();

        /// <summary>
        /// Called when TopShelf service is stopped
        /// </summary>
        void Stop();

        #endregion
    }
}