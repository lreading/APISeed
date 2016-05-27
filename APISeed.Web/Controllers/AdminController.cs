using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using APISeed.Web.Filters;
using APISeed.Domain;
using APISeed.DataLayer;
using APISeed.Domain.Errors;

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
            return View(errors);
        }

        [HttpPost]
        public JsonResult GetErrorDetails(int id)
        {
            var errorRepo = new ErrorRepository();
            var error = errorRepo.Get(id);
            return Json(new ErrorDetail().FromXML(error.AllXml), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult MarkErrorAsRead(int id)
        {
            var errorRepo = new ErrorRepository();
            var error = errorRepo.Get(id);
            error.Viewed = true;
            error.TimeViewedUtc = DateTime.UtcNow;
            errorRepo.Update(error);
            return Json(true, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult ResolveError(int id)
        {
            var errorRepo = new ErrorRepository();
            var error = errorRepo.Get(id);
            error.Resolved = true;
            error.TimeResolvedUtc = DateTime.UtcNow;
            errorRepo.Update(error);
            return Json(true, JsonRequestBehavior.AllowGet);
        }
    }
}