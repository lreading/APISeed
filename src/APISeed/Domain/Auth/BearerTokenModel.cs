using System;

namespace Template.Domain.Auth
{
    public class BearerTokenModel
    {
        public string access_token { get; set; }
        public string token_type { get; set; }
        public long? expires_in { get; set; }
        public string UserName { get; set; }
        public DateTime? issued { get; set; }
        public DateTime? expires { get; set; }
    }
}
