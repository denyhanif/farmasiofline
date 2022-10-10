using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Siloam.Service.EMRPharmacy.Models
{
    public class ItemPrice
    {
        public long SalesItemId { get; set; }
        public string SalesItemName { get; set; }
        public string quantity { get; set; }
        public string Uom { get; set; }
        public string SubTotal { get; set; }
        public string TotalPrice { get; set; }
        public string PatientNet { get; set; }
        public string PayerNet { get; set; }
        public string PatientNetTotal { get; set; }
        public string PayerNetTotal { get; set; }
        public string Frequency { get; set; }
        public string Instruction { get; set; }
    }
}
