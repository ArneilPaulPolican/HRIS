using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(HRIS_sample.Startup))]
namespace HRIS_sample
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
