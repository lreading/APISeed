using Dapper;
using System.Linq;
using System.Collections.Generic;

namespace $safeprojectname$
{
    public class ErrorRepository : Repository<Domain.Errors.ElmahErrorModel>
    {
        public ErrorRepository() : base("ELMAH_Error") { }
        public ErrorRepository(Interfaces.IConnectionFactory connectionFactory) : base("ELMAH_Error", connectionFactory) { }

        internal override dynamic Mapping(Domain.Errors.ElmahErrorModel item)
        {
            return new
            {
                id = item.Id,
                errorId = item.ErrorId,
                application = item.Application,
                host = item.Host,
                type = item.Type,
                source = item.Source,
                message = item.Message,
                user = item.User,
                statusCode = item.StatusCode,
                timeUtc = item.TimeUtc,
                allXml = item.AllXml,
                viewed = item.Viewed,
                timeViewedUtc = item.TimeViewedUtc,
                resolved = item.Resolved,
                timeResolvedUtc = item.TimeResolvedUtc
            };
        }

        public override Domain.Errors.ElmahErrorModel Get(object id)
        {
            using (var db = Connection)
            {
                return db.Query<Domain.Errors.ElmahErrorModel>(@"
SELECT          *
FROM            ELMAH_Error
WHERE           Id = @id
AND             Resolved = 0
", new { id = id }).FirstOrDefault();
            }
        }

        public override IEnumerable<Domain.Errors.ElmahErrorModel> GetAll()
        {
            using (var db = Connection)
            {
                return db.Query<Domain.Errors.ElmahErrorModel>(@"
SELECT          *
FROM            ELMAH_Error
WHERE           Resolved = 0
");
            }
        }
    }
}
