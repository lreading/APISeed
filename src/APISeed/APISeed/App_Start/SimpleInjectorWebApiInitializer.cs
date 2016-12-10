using System.Web.Http;
using SimpleInjector;
using SimpleInjector.Integration.WebApi;
using Owin;
using APISeed.Extensions.Owin;

namespace APISeed.App_Start
{
    /// <summary>
    /// 
    /// </summary>
    public static class SimpleInjectorWebApiInitializer
    {
        /// <summary>
        /// Initialize the container and register it as Web API Dependency Resolver.
        /// </summary>
        public static void Initialize(IAppBuilder app)
        {
            var container = new Container();

            app.UseOwinContextInjector(container);

            container.Options.DefaultScopedLifestyle = new WebApiRequestLifestyle();
            
            InitializeContainer(container, app);

            container.RegisterWebApiControllers(GlobalConfiguration.Configuration);
       
            container.Verify();
            
            GlobalConfiguration.Configuration.DependencyResolver = new SimpleInjectorWebApiDependencyResolver(container);
        }
     
        private static void InitializeContainer(Container container, IAppBuilder app)
        {
            DataLayer.IocRegistration.Register(container, app);
        }
    }
}