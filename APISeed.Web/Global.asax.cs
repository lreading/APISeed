using Elmah;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Hosting;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace APISeed.Web
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            var startup = new DataLayer.Schema.Startup();
            startup.Init();
            HostingEnvironment.QueueBackgroundWorkItem(cancellation =>
            {
                var errorGenerator = new BusinessLayer.Errors.StaticErrorGenerator("Error", typeof(WebApiApplication), "http://localhost:61163/");
                errorGenerator.GenerateStaticErrorPages(HostingEnvironment.ApplicationPhysicalPath + "\\ErrorPages");
            });
        }

        #region ELMAH Logging
        private List<string> MessagesToIgnore = new List<string>
        {
            // If the error message contains any of these strings, it will
            // be thrown out and not sent in an or added to the database (if configured).
            // Use caution, as having a loose term here can remove a ton of errors...
        };

        void ErrorMail_Filtering(object sender, ExceptionFilterEventArgs args)
        {
#if DEBUG
            args.Dismiss();
#else
            Filter(args);
#endif
        } // end error mail filtering

        void ErrorLog_Filtering(object sender, ExceptionFilterEventArgs args)
        {

#if DEBUG
            args.Dismiss();
#else
            Filter(args);
#endif
        } // end error log filtering

        void Filter(ExceptionFilterEventArgs args)
        {
            // If the exception message contains any of the strings in the list of messages to ignore,
            // dismiss the exception.  Dismissing causes it to not be emailed or added to the database (if configured)
            if (MessagesToIgnore.Where(x => args.Exception.Message.ToLower().Contains(x.ToLower())).Any())
            {
                args.Dismiss();
            } 

            // This determines if the error is a specific type of exception
            else if (args.Exception.GetBaseException() is HttpRequestValidationException)
            {
                args.Dismiss();
            } 

            // Handles http exceptions
            else if (args.Exception.GetBaseException() is HttpException)
            {
                // Get the exception as an HttpException so we have access to the status code
                var TheException = args.Exception as HttpException;
                if (TheException != null)
                {
                    var HttpCode = TheException.GetHttpCode();        // 404, 500, etc.

                    // A switch statement to determine what to do based on the http status code
                    switch (HttpCode)
                    {
                        // 400 - Bad Request.  Often generated scripts.  We can use our webmaster tools
                        // to identify broken links and such.
                        case 400:
                            args.Dismiss();
                            break;
                        // 404 errors are more often than not from bad SEO or spam bots...
                        // While there COULD be use in knowing if a link is broken, it will
                        // get lost in the noise of all of the junk errors that are generated
                        case 404:
                            args.Dismiss();
                            break;
                        default:
                            break;
                    } 
                } 
            } 
        } 
        #endregion
    }
}
