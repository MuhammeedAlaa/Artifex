using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ArtGallery.Startup))]
namespace ArtGallery
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
