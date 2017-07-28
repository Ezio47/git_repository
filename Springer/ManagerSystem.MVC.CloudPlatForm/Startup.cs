using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ManagerSystem.MVC.CloudPlatForm.Startup))]
namespace ManagerSystem.MVC.CloudPlatForm
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
