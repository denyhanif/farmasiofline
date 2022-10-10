using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Siloam.Service.EMRPharmacy.Models
{
    public class PharmacyHistory
    {
        public Int64 history_id { get; set; }
        public Int64 record_id { get; set; }
        public string action { get; set; }
        public string remarks { get; set; }
        public DateTime created_date { get; set; }
        public string created_by { get; set; }
    }
}
