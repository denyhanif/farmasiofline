using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Siloam.Service.EMRPharmacy.Models.SyncHope
{
    public class CheckPriceItemIssueRequest
    {
        public long item_id { get; set; }
        public string issue_quantity { get; set; }
        public long uom_id { get; set; }
        public int is_consumables { get; set; }
        public int is_compound { get; set; }
        public Guid prescription_compound_header_id { get; set; }
        public string prescription_compound_name { get; set; }
    }
}
