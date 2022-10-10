using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Siloam.Service.EMRPharmacy.Models.AutoSync
{
    public class LogDeliveryFee
    {
        public long log_delivery_fee_id { get; set; } 
        public long organization_id { get; set; }
        public long patient_id { get; set; }
        public long admission_id { get; set; }
        public Guid encounter_id { get; set; }
        public string json_request { get; set; }
        public string json_result { get; set; }
        public decimal distance { get; set; }
        public DateTime created_date { get; set; }
    }
}
