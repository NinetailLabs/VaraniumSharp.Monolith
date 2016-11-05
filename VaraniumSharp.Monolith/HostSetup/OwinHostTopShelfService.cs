using VaraniumSharp.Attributes;
using VaraniumSharp.Enumerations;
using VaraniumSharp.Monolith.Configuration;
using VaraniumSharp.Monolith.Interfaces;

namespace VaraniumSharp.Monolith.HostSetup
{
    [AutomaticContainerRegistration(typeof(ITopShelfService), Reuse = ServiceReuse.Singleton)]
    public class OwinHostTopShelfService : ITopShelfService
    {
        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="configuration"></param>
        public OwinHostTopShelfService(TopShelfConfiguration configuration)
        {
            Configuration = configuration;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the configuration used by the TopShelf service
        /// </summary>
        public ITopShelfConfiguration Configuration { get; }

        #endregion

        #region Public Methods

        /// <summary>
        /// Called when TopShelf service is started
        /// </summary>
        public void Start()
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Called when TopShelf service is stopped
        /// </summary>
        public void Stop()
        {
            throw new System.NotImplementedException();
        }

        #endregion
    }
}