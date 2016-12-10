using System;
using DataLayer.Repositories;
using Domain;
using log4net;
using System.Threading.Tasks;
using System.Web.Http;

namespace APISeed.Controllers
{
    /// <summary>
    /// The base API controller
    /// </summary>
    public abstract class ApiBaseController<T> : ApiController where T : IEntity
    {

        #region Fields, Properties and Constructors

        /// <summary>
        /// Mechanism to log
        /// </summary>
        internal readonly ILog Logger;

        /// <summary>
        /// The resource's repository
        /// </summary>
        internal readonly IAsyncRepository<T> Repo;

        /// <summary>
        /// Creates a new instance of the base controller
        /// </summary>
        /// <param name="repo"></param>
        protected ApiBaseController(IAsyncRepository<T> repo)
        {
            Repo = repo;
            Logger = LogManager.GetLogger(GetType());
        }

        #endregion

        #region Public Endpoints

        /// <summary>
        /// Gets all of the items without a filter applied
        /// </summary>
        /// <returns></returns>
        public async Task<IHttpActionResult> GetAll()
        {
            // TODO - Restrict access by user
            try
            {
                var items = await Repo.GetAllAsync();
                return Ok(items);
            }
            catch (UnauthorizedAccessException)
            {
                return StatusCode(System.Net.HttpStatusCode.Forbidden);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                return InternalServerError();
            }
        }

        /// <summary>
        /// Gets an item by its id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IHttpActionResult> Get(int id)
        {
            // TODO - Restrict access by user
            try
            {
                var items = await Repo.GetAsync(id);
                return Ok(items);
            }
            catch (UnauthorizedAccessException)
            {
                return StatusCode(System.Net.HttpStatusCode.Forbidden);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                return InternalServerError();
            }
        }

        /// <summary>
        /// Inserts a new item
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public async Task<IHttpActionResult> Post([FromBody]T item)
        {
            // TODO - Restrict access by user
            try
            {
                await Repo.AddAsync(item);
                return CreatedAtRoute("DefaultApi", new { id = item.Id }, item);
            }
            catch (UnauthorizedAccessException)
            {
                return StatusCode(System.Net.HttpStatusCode.Forbidden);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                return InternalServerError();
            }
        }

        /// <summary>
        /// Updates an item
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public async Task<IHttpActionResult> Put([FromBody]T item)
        {
            // TODO - Restrict access by user
            try
            {
                await Repo.UpdateAsync(item);
                return StatusCode(System.Net.HttpStatusCode.Accepted);
            }
            catch (UnauthorizedAccessException)
            {
                return StatusCode(System.Net.HttpStatusCode.Forbidden);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                return InternalServerError();
            }
        }

        /// <summary>
        /// Deletes an item
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IHttpActionResult> Delete(int id)
        {
            // TODO - Restrict access by user
            try
            {
                await Repo.DeleteAsync(id);
                return StatusCode(System.Net.HttpStatusCode.Accepted);
            }
            catch (UnauthorizedAccessException)
            {
                return StatusCode(System.Net.HttpStatusCode.Forbidden);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                return InternalServerError();
            }
        }

        #endregion

        #region Helper Methods

        /// <summary>
        /// Gets all items based on the filter.
        /// </summary>
        /// <remarks>
        /// Due to serialization issues, the derived classes should have
        /// a public endpoint for the getall method that accepts the 
        /// concrete implementation of the filter.  If no special handling
        /// is necessary, they can return this method.
        /// </remarks>
        /// <param name="filter"></param>
        /// <returns></returns>
        protected virtual async Task<IHttpActionResult> GetAll([FromBody]IQueryFilter filter)
        {
            try
            {
                var items = await Repo.GetAllAsync(filter);
                return Ok(items);
            }
            catch (UnauthorizedAccessException)
            {
                return StatusCode(System.Net.HttpStatusCode.Forbidden);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                return InternalServerError();
            }
        }

        #endregion

    }
}
