using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ABMODELE.Startup))]
namespace ABMODELE
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
