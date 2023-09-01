using Owin;
using Microsoft.Owin;

[assembly: OwinStartup(typeof(MotorMax.Web.Startup))]
namespace MotorMax.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
