using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(NCVITAMINS.Startup))]
namespace NCVITAMINS
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
