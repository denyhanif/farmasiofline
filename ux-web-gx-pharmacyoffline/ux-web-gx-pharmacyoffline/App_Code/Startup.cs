using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ux_web_gx_pharmacyoffline.Startup))]
namespace ux_web_gx_pharmacyoffline
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
