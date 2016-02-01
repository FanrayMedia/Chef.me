using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Chef.Web.Startup))]
namespace Chef.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
