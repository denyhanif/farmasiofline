using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Siloam.Service.EMRPharmacy.Models
{
    public class AidoFailed
    {
        public long aido_failed_sync_id { get; set; }
        public long organization_id { get; set; }
        public long patient_id { get; set; }
        public long admission_id { get; set; }
        public Guid encounter_id { get; set; }
        public string token { get; set; }
        public string jsonrequest_send_drug { get; set; }
        public string jsonresponse_send_drug { get; set; }
        public string error_message { get; set; }
        public string channel_id { get; set; }
        public DateTime created_date { get; set; }
    }
}
