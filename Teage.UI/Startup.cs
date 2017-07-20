using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Teage.UI.Startup))]
namespace Teage.UI
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
