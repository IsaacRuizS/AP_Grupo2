using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(FB.Login.Startup))]
namespace FB.Login
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
