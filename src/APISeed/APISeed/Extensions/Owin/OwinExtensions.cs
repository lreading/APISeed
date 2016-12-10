using Owin;
using SimpleInjector;
using SimpleInjector.Extensions.ExecutionContextScoping;

namespace Template.APISeed.Extensions.Owin
{
    /// <summary>
    /// Extension methods for Owin related components
    /// </summary>
    public static class OwinExtensions
    {
        /// <summary>
        /// Extension method to use Owin's context injector alongsize the SimpleInjector IoC
        /// </summary>
        /// <remarks>
        /// Owin works at Runtime: without this, attempting to do DI will fail since there's not
        /// a request when the SimpleInjector DI is taking place
        /// </remarks>
        /// <param name="app"></param>
        /// <param name="container"></param>
        public static void UseOwinContextInjector(this IAppBuilder app, Container container)
        {
            // Create an OWIN middleware to create an execution context scope
            app.Use(async (context, next) =>
            {
                using (container.BeginExecutionContextScope())
                {
                    await next.Invoke();
                }
            });
        }
    }
}