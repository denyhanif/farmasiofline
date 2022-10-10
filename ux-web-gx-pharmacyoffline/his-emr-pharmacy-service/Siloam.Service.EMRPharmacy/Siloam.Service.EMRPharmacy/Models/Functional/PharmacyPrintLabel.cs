using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Siloam.Service.EMRPharmacy.Models.Functional
{
    public class PharmacyPrintLabel
    {
        public Int64 prescription_id { get; set; }
        public string prescription_no { get; set; }
        public Int64 item_id { get; set; }
        public string item_name { get; set; }
        public string quantity { get; set; }
        public Int64 uom_id { get; set; }
        public string uom_code { get; set; }
        public Int64 frequency_id { get; set; }
        public string frequency_code { get; set; }
        public string dosage_id { get; set; }
        public long dose_uom_id { get; set; }
        public string dose_uom { get; set; }
        public string dose_text { get; set; }
        public Int64 administration_route_id { get; set; }
        public string administration_route_code { get; set; }
        public int iteration { get; set; }
        public string remarks { get; set; }
        public bool is_routine { get; set; }
        public bool is_consumables { get; set; }
        public Guid compound_id { get; set; }
        public string compound_name { get; set; }
        public Int64 origin_prescription_id { get; set; }
        public Int64 hope_aritem_id { get; set; }
        public string doctor_name { get; set; }
        public string DOB { get; set; }
        public string age { get; set; }
        public string patientName { get; set; }
        public string localMrNo { get; set; }
        public string admision { get; set; }
        public string CentralizedMrNo { get; set; }
        public string OrganizationCode { get; set; }
        public string OrganizationId { get; set; }
        public bool printInternal { get; set; }
    }
}
