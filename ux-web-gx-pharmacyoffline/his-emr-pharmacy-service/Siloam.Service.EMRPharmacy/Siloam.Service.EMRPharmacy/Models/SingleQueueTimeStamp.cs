using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Siloam.Service.EMRPharmacy.Models
{
    public class SingleQueueTimeStamp
    {
        public long singlequeue_phar_timestamp_id { get; set; }
        public long organization_id { get; set; }
        public long patient_id { get; set; }
        public long admission_id { get; set; }
        public long doctor_id { get; set; }
        public Guid encounter_id { get; set; }
        public bool is_retail { get; set; }
        public Nullable<DateTime> check_time { get; set; }
        public Nullable<DateTime> verify_time { get; set; }
        public Nullable<DateTime> call_time { get; set; }
        public Nullable<DateTime> done_time { get; set; }
        public bool is_done { get; set; }
        public bool is_compound { get; set; }
        public bool is_active { get; set; }
        public DateTime created_date { get; set; }
        public string created_by { get; set; }
        public DateTime modified_date { get; set; }
        public string modified_by { get; set; }
    }
}
