using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Twtter.Application.Startup))]
namespace Twtter.Application
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            app.MapSignalR();
        }
    }
}
