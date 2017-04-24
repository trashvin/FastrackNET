using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(FASTWeb.Startup))]
namespace FASTWeb
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
