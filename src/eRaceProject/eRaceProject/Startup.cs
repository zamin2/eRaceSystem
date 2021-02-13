using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(eRaceProject.Startup))]
namespace eRaceProject
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
