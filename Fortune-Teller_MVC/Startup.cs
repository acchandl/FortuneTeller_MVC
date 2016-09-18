using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Fortune_Teller_MVC.Startup))]
namespace Fortune_Teller_MVC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
