using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Siloam.Service.EMRPharmacy.Models
{
    public class TeleconsulShipment
    {
        public long teleconsul_shipment_id { get; set; }
        public long teleconsul_drug_ticket_id { get; set; }
        public string shipment_name { get; set; }
        public bool is_active { get; set; }
        public DateTime created_date { get; set; }
        public DateTime modified_date { get; set; }
    }
}
