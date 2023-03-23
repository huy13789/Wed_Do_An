using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Wed_Do_An.Startup))]
namespace Wed_Do_An
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
