using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(EbayOnlineShopping.Startup))]
namespace EbayOnlineShopping
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
