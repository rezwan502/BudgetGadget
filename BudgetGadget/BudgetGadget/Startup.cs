using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(BudgetGadget.Startup))]
namespace BudgetGadget
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
