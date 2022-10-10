using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Siloam.Service.EMRPharmacy.Models
{
    public class AidoRequestModel
    {
        public DateTime requiredDate { get; set; }
        public DateTime shippedDate { get; set; }
        public int totalPrice { get; set; }
        public List<AidoDrug> items { get; set; }
        public Guid siloamTrxId { get; set; }
    }

    public class AidoDrug
    {
        public string name { get; set; }
        public string qty { get; set; }
        public string uom { get; set; }
        public string price { get; set; }
    }
}
