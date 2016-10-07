using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Tienda1.Startup))]
namespace Tienda1
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
