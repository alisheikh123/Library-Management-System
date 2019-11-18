using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Library_Management_System.Startup))]
namespace Library_Management_System
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
