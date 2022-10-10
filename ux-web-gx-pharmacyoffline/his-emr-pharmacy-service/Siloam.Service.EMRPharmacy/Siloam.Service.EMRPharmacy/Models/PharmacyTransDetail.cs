using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Siloam.Service.EMRPharmacy.Models
{
    public class PharmacyTransDetail
    {
        public Int64 pharmacy_transaction_detail_id { get; set; }
        public Int64 pharmacy_transaction_header_id { get; set; }
        public Int64 pharmacy_prescription_id { get; set; }
        public Int64 additional_prescription_id { get; set; }
        public Int64 item_id { get; set; }
        public Decimal issued_quantity { get; set; }
        public Decimal item_price { get; set; }
        public string dose_text { get; set; }
        public string notes { get; set; }
        public Boolean is_active { get; set; }
        public DateTime created_date { get; set; }
        public string created_by { get; set; }
        public Nullable<DateTime> modified_date { get; set; }
        public string modified_by { get; set; }
    }
}
