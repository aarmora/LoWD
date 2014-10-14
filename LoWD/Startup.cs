using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(LoWD.Startup))]
namespace LoWD
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
