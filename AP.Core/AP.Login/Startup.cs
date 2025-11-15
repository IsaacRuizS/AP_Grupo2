using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(AP.Login.Startup))]
namespace AP.Login
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
