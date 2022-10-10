using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Siloam.Service.EMRPharmacy.Models
{
    public class AdditionalPharmacyHistory
    {
        public Int64 additional_history_id { get; set; }
        public Int64 additional_record_id { get; set; }
        public string action { get; set; }
        public string remarks { get; set; }
        public DateTime created_date { get; set; }
        public string created_by { get; set; }
    }
}
