using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using APISeed.Web.Filters;
using APISeed.Domain;
using APISeed.DataLayer;

namespace APISeed.Web.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }

        //[AuthorizeRoles(Roles.Administrator)]
        public ActionResult ErrorLog()
        {
            var errorRepo = new ErrorRepository();
            var errors = errorRepo.Collection;
            return View();
        }
    }
}