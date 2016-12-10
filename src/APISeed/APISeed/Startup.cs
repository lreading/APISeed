using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(Template.APISeed.Startup))]

namespace Template.APISeed
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
            var dalStartup = new DataLayer.Startup.Startup();
            dalStartup.Init();
            App_Start.SimpleInjectorWebApiInitializer.Initialize(app);
            ConfigureAuth(app);
        }
    }
}
