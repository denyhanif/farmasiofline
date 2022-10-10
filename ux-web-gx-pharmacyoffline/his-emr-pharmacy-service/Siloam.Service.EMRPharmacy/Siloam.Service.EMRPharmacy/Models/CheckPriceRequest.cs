using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Siloam.Service.EMRPharmacy.Models
{
    public class CheckPriceRequest
    {
        public long item_id { get; set; }
        public string quantity { get; set; }
        public long uom_id { get; set; }
        public int is_consumables { get; set; }
    }
}
