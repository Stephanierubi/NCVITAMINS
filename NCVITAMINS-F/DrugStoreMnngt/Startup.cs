using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(DrugStoreMnngt.Startup))]
namespace DrugStoreMnngt
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
