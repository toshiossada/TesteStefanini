using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Stefanini.Startup))]
namespace Stefanini
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
