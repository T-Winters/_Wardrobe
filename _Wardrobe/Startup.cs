using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(_Wardrobe.Startup))]
namespace _Wardrobe
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
