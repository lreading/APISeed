using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(APISeed.Startup))]

namespace APISeed
{
    /// <summary>
    /// 
    /// </summary>
    public partial class Startup
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="app"></param>
        public void Configuration(IAppBuilder app)
        {
            App_Start.SimpleInjectorWebApiInitializer.Initialize(app);
            ConfigureAuth(app);
        }
    }
}
