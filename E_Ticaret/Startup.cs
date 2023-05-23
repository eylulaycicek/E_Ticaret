using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(E_Ticaret.Startup))]
namespace E_Ticaret
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
