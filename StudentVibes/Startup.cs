using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(StudentVibes.Startup))]
namespace StudentVibes
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
