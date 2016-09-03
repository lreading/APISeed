using Microsoft.AspNet.Identity;
using System.Web.Http;
using System.Web.Http.Controllers;

namespace $safeprojectname$.Controllers
{
    [Authorize]
    public abstract class BaseAPIController<T> : ApiController where T : Domain.EntityBase
    {
        public DataLayer.Interfaces.IRepository<T> Repo { get; set; }
        private string _userId;
        internal bool UseGuard = true;

        protected override void Initialize(HttpControllerContext controllerContext)
        {
            Repo.Guard(UseGuard);
            base.Initialize(controllerContext);
        }

        public virtual IHttpActionResult Get()
        {
            Repo.SetUser(User.Identity.GetUserId());
            return Ok(Repo.GetAll());
        }

        public virtual IHttpActionResult Get(int id)
        {
            Repo.SetUser(User.Identity.GetUserId());
            return Ok(Repo.Get(id));
        }

        public virtual IHttpActionResult Post([FromBody]T obj)
        {
            Repo.SetUser(User.Identity.GetUserId());
            Repo.Add(obj);
            return Ok(obj);
        }

        public virtual IHttpActionResult Put([FromBody]T obj)
        {
            Repo.SetUser(User.Identity.GetUserId());
            Repo.Update(obj);
            return Ok(obj);
        }

        public virtual IHttpActionResult Delete(int id)
        {
            Repo.SetUser(User.Identity.GetUserId());
            Repo.Delete(id);
            return Ok(id);
        }
    }
}