using Owin;

namespace VaraniumSharp.Monolith.HostSetup
{
    public class OwinStartup
    {
        public void Configuration(IAppBuilder app)
        {
            app.UseNancy();
        }
    }
}