using System;

namespace APISeed.Domain.Auth
{
    public class BearerTokenModel : EntityBase
    {
        public override object Id
        {
            get
            {
                return 0;
            }

            set
            {
                base.Id = value;
            }
        }
        public string access_token { get; set; }
        public string token_type { get; set; }
        public long? expires_in { get; set; }
        public string UserName { get; set; }
        public DateTime? issued { get; set; }
        public DateTime? expires { get; set; }
    }
}
