using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Siloam.Service.EMRPharmacy.Models
{
    public class Prescription
    {
        public Int64 pharmacy_prescription_id { get; set; }
        public Guid emr_prescription_id { get; set; }
        public Int64 organization_id { get; set; }
        public Guid encounter_ticket_id { get; set; }
        public Int64 doctor_id { get; set; }
        public Nullable<Int64> origin_prescription_id { get; set; }
        public Guid emr_origin_prescription_id { get; set; }
        public string prescription_no { get; set; }
        public Int64 item_id { get; set; }
        public Decimal quantity { get; set; }
        public Decimal issued_qty { get; set; }
        public Int64 uom_id { get; set; }
        public Int64 dose_uom_id { get; set; }
        public Int64 frequency_id { get; set; }
        public Decimal dosage_id { get; set; }
        public string dose_text { get; set; }
        public Nullable<Int64> administration_route_id { get; set; }
        public Nullable<int> iteration { get; set; }
        public Boolean is_routine { get; set; }
        public string remarks { get; set; }
        public Nullable<Int64> hope_admission_id { get; set; }
        public Nullable<Int64> hope_aritem_id { get; set; }
        public Nullable<Guid> compound_id { get; set; }
        public Boolean is_consumables { get; set; }
        public Guid prescription_sync_id { get; set; }
        public Boolean is_active { get; set; }
        public short item_sequence { get; set; }
        public Nullable<DateTime> created_date { get; set; }
        public string created_by { get; set; }
        public Nullable<DateTime> modified_date { get; set; }
        public string modified_by { get; set; }
    }
}
