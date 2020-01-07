using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(LuDating.Startup))]
namespace LuDating
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
