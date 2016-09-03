using System.Web.Mvc;

namespace $safeprojectname$.Controllers
{
    /// <summary>
    /// Responsible for generating error messages
    /// These files are created as static HTML files when the application starts
    /// <seealso cref="BusinessLayer.Errors.StaticErrorGenerator"/>
    /// </summary>
    public class ErrorController : Controller
    {
        /// <summary>
        /// Bad request
        /// </summary>
        /// <returns></returns>
        public ActionResult Error400()
        {
            var vm = new Models.ErrorPageViewModel
            {
                Message = "Bad Request",
                Title = "400"
            };
            return View("Error", vm);
        }

        /// <summary>
        /// Unauthorized
        /// </summary>
        /// <returns></returns>
        public ActionResult Error401()
        {
            var vm = new Models.ErrorPageViewModel
            {
                Message = "Unauthorized",
                Title = "401"
            };
            return View("Error", vm);
        }

        /// <summary>
        /// Forbidden
        /// </summary>
        /// <returns></returns>
        public ActionResult Error403()
        {
            var vm = new Models.ErrorPageViewModel
            {
                Message = "Forbidden.",
                Title = "403"
            };
            return View("Error", vm);
        }

        /// <summary>
        /// Not found
        /// </summary>
        /// <returns></returns>
        public ActionResult Error404()
        {
            var vm = new Models.ErrorPageViewModel
            {
                Message = "Page not found.",
                Title = "404"
            };
            return View("Error", vm);
        }

        /// <summary>
        /// Server error
        /// </summary>
        /// <returns></returns>
        public ActionResult Error500()
        {
            var vm = new Models.ErrorPageViewModel
            {
                Message = "Server Error",
                Title = "500"
            };
            return View("Error", vm);
        }

        /// <summary>
        /// Out of resources
        /// </summary>
        /// <returns></returns>
        public ActionResult Error503()
        {
            var vm = new Models.ErrorPageViewModel
            {
                Message = "Out of Resources.",
                Title = "503"
            };
            return View("Error", vm);
        }

        /// <summary>
        /// Gateway issue
        /// </summary>
        /// <returns></returns>
        public ActionResult Error504()
        {
            var vm = new Models.ErrorPageViewModel
            {
                Message = "Gateway Time Out",
                Title = "504"
            };
            return View("Error", vm);
        }
    }
}