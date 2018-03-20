using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CompleteMvcCmsProject1.Startup))]
namespace CompleteMvcCmsProject1
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
