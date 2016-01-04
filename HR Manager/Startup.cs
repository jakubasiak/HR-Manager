using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(HR_Manager.Startup))]
namespace HR_Manager
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
