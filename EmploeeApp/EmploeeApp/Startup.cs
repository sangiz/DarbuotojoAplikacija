using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(EmploeeApp.Startup))]
namespace EmploeeApp
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
