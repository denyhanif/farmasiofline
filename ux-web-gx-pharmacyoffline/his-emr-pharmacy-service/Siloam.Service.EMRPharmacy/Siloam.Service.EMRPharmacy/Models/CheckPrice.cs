using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Siloam.Service.EMRPharmacy.Models
{
    public class CheckPrice
    {
        public long SalesItemId { get; set; }
        public string SalesItemCode { get; set; }
        public string SalesItemName { get; set; }
        public string Uom { get; set; }
        public string Quantity { get; set; }
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
    }
}
