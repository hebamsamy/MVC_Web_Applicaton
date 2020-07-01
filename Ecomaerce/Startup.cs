using Ecomaerce.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Ecomaerce.Startup))]
namespace Ecomaerce
{
    public partial class Startup
    {
        ApplicationDbContext db = new ApplicationDbContext();
        public void Configuration(IAppBuilder app)
        {
           
            ConfigureAuth(app);
            app.MapSignalR();

        }

       
    }
}
