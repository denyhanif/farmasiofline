using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Siloam.Service.EMRPharmacy.Models.AutoSync
{
    public class CheckPriceCpoe
    {
        public long SalesItemId { get; set; }
        public string SalesItemCode { get; set; }
        public string SalesItemName { get; set; }
        public decimal Quantity { get; set; }
        public decimal SinglePrice { get; set; }
        public decimal Amount { get; set; }
        public decimal DiscountPrice { get; set; }
        public decimal PayerNet { get; set; }
        public decimal PatientNet { get; set; }
        public decimal TotalPayerNet { get; set; }
        public decimal TotalPatientNet { get; set; }
        public decimal RoundingPayerNet { get; set; }
        public decimal RoundingPatientNet { get; set; }
        public decimal TotalPayerNetFinal { get; set; }
        public decimal TotalPatientNetFinal { get; set; }
        public int IsCito { get; set; }
    }


    public class CheckPriceCpoeParam
    {
        public long item_id { get; set; }
        public int quantity { get; set; }
        public bool is_cito { get; set; }
		public bool is_future_order { get; set; }
		public DateTime future_order_date { get; set; }

	}
}
