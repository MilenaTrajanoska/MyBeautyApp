using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Proba.Startup))]
namespace Proba
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
