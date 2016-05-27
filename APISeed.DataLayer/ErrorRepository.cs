using APISeed.Domain.Errors;
using APISeed.DataLayer.Interfaces;

namespace APISeed.DataLayer
{
    public class ErrorRepository : Repository<ElmahErrorModel>
    {
        public ErrorRepository() : base("ELMAH_Error") { }
        public ErrorRepository(IConnectionFactory connectionFactory) : base("ELMAH_Error", connectionFactory) { }

        internal override dynamic Mapping(ElmahErrorModel item)
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
    }
}
