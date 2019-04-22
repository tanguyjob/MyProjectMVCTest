using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MVCTestWeb.Startup))]
namespace MVCTestWeb
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
