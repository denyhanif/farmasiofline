using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Siloam.Service.EMRPharmacy.Models.AutoSync
{
    public class PrescriptionCentral
    {
        public Guid prescription_id { get; set; }
        public long organization_id { get; set; }
        public long admission_id { get; set; }
        public long doctor_id { get; set; }
        public Guid encounter_ticket_id { get; set; }
        public Int64 item_id { get; set; }
        public string quantity { get; set; }
        public string issued_qty { get; set; }
        public Int64 uom_id { get; set; }
        public Int64 frequency_id { get; set; }
        public string dosage_id { get; set; }
        public long dose_uom_id { get; set; }
        public string dose_text { get; set; }
        public Int64 administration_route_id { get; set; }
        public int iteration { get; set; }
        public string remarks { get; set; }
        public int is_routine { get; set; }
        public int is_consumables { get; set; }
        public short item_sequence { get; set; }
    }
}
