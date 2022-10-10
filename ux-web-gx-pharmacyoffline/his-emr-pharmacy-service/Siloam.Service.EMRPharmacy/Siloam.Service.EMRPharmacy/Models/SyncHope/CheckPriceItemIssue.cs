using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Siloam.Service.EMRPharmacy.Models.SyncHope
{
    public class CheckPriceItemIssue
    {
        public long SalesItemId { get; set; }
        public string SalesItemCode { get; set; }
        public string SalesItemName { get; set; }
        public string Uom { get; set; }
        public int RatioUOM1 { get; set; }
        public string issue_quantity { get; set; }
        public string stock_quantity { get; set; }
        public string SinglePrice { get; set; }
        public string Amount { get; set; }
        public string DiscountPrice { get; set; }
        public string PayerNet { get; set; }
        public string PatientNet { get; set; }
        public string TotalPayerNet { get; set; }
        public string TotalPatientNet { get; set; }
        public string RoundingPayerNet { get; set; }
        public string RoundingPatientNet { get; set; }
        public string TotalPayerNetFinal { get; set; }
        public string TotalPatientNetFinal { get; set; }
        public int is_consumables { get; set; }
        public int is_compound { get; set; }
        public Guid prescription_compound_header_id { get; set; }
        public string prescription_compound_name { get; set; }
        public string substore_quantity { get; set; }
    }
}
