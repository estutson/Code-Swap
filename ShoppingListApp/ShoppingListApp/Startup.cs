using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ShoppingListApp.Startup))]
namespace ShoppingListApp
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
