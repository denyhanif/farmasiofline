using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Siloam.Service.EMRPharmacy.Models
{
    public class UserRole
    {
        public Guid user_id { get; set; }
        public long hope_user_id { get; set; }
        public long hope_organization_id { get; set; }
        public Guid application_id { get; set; }
        public Guid role_id { get; set; }
        public string role_name { get; set; }
    }
}
