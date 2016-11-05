using Topshelf;
using VaraniumSharp.Attributes;
using VaraniumSharp.Enumerations;
using VaraniumSharp.Monolith.Interfaces;

namespace VaraniumSharp.Monolith.HostSetup
{
    /// <summary>
    /// Runner for ITopShelfService
    /// </summary>
    [AutomaticContainerRegistration(typeof(TopShelfBootstrapper), Reuse = ServiceReuse.Singleton)]
    public class TopShelfBootstrapper
    {
        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="service">Service implementing <see cref="ITopShelfService"/></param>
        public TopShelfBootstrapper(ITopShelfService service)
        {
            SetupTopshelfHost(service);
        }

        #endregion Constructor

        #region Properties

        /// <summary>
        /// TopShelf Host
        /// </summary>
        public Host TopShelfHost { get; private set; }

        #endregion Properties

        #region Private Methods

        /// <summary>
        /// Invokes HostFactory.Run on the injected <see cref="ITopShelfService"/>
        /// </summary>
        /// <returns></returns>
        private void SetupTopshelfHost(ITopShelfService service)
        {
            TopShelfHost = HostFactory.New(x =>
            {
                //This line is required otherwise ReSharper unit test won't run
                x.ApplyCommandLine("");
                x.UseSerilog();
                x.Service<ITopShelfService>(s =>
                {
                    s.ConstructUsing(name => service);
                    s.WhenStarted(tService => service.Start());
                    s.WhenStopped(tService => service.Stop());
                });
                x.RunAsLocalService();

                var serviceConfiguration = service.TopShelfConfiguration;
                x.SetDescription(serviceConfiguration.Name);
                x.SetDisplayName(serviceConfiguration.DisplayName);
                x.SetServiceName(serviceConfiguration.Name);
            });
        }

        #endregion Private Methods
    }
}