using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Siloam.Service.EMRPharmacy.Models.ExpressCheckout
{
    public class ExpressPrescription
    {
        public string ItemId { get; set; }
        public string Quantity { get; set; }
        public string UOMID { get; set; }
    }
}
