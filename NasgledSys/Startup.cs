using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(NasgledSys.Startup))]
namespace NasgledSys
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
