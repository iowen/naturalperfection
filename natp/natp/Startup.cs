using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(natp.Startup))]
namespace natp
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
