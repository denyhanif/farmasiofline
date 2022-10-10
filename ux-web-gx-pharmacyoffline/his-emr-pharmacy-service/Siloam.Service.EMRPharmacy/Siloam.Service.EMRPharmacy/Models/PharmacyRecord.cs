using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Siloam.Service.EMRPharmacy.Models
{
    public class PharmacyRecord
    {
        public Int64 record_id { get; set; }
        public Int64 organization_id { get; set; }
        public Int64 admission_id { get; set; }
        public Guid encounter_id { get; set; }
        public Int64 patient_id { get; set; }
        public long payer_id { get; set; }
        public string pharmacy_notes { get; set; }
        public Nullable<DateTime> take_date { get; set; }
        public Nullable<Int64> take_by { get; set; }
        public Nullable<DateTime> submit_date { get; set; }
        public Nullable<Int64> submit_by { get; set; }
        public Nullable<DateTime> print_date { get; set; }
        public Nullable<Int64> print_by { get; set; }
        public Nullable<DateTime> handover_date { get; set; }
        public Nullable<Int64> handover_by { get; set; }
        public bool IsEditDrug { get; set; }
        public bool IsEditCompound { get; set; }
        public bool IsEditConsumables { get; set; }
        public Boolean is_syncHOPE { get; set; }
        public Boolean is_active { get; set; }
        public DateTime created_date { get; set; }
        public string created_by { get; set; }
        public Nullable<DateTime> modified_date { get; set; }
        public string modified_by { get; set; }
        public decimal payer_coverage { get; set; }
        public bool is_selfcollection { get; set; }
    }
}
