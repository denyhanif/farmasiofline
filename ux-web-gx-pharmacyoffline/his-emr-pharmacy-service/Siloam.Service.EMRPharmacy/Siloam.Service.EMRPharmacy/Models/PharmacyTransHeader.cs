using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Siloam.Service.EMRPharmacy.Models
{
    public class PharmacyTransHeader
    {
        public Int64 pharmacy_transaction_header_id { get; set; }
        public Int64 patient_id { get; set; }
        public Int64 prescription_organization_id { get; set; }
        public Int64 prescription_admission_id { get; set; }
        public Int64 transaction_organization_id { get; set; }
        public Int64 transaction_admission_id { get; set; }
        public DateTime transaction_date { get; set; }
        public Decimal total_price { get; set; }
        public Boolean is_active { get; set; }
        public DateTime created_date { get; set; }
        public string created_by { get; set; }
        public Nullable<DateTime> modified_date { get; set; }
        public string modified_by { get; set; }
    }
}
