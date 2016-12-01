using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(TestAPI.Startup))]
namespace TestAPI
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
