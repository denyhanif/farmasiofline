using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Siloam.Service.EMRPharmacy.Models.SyncHope
{
    public class ItemIssueData
    {
        public List<ItemIssueDrugCons> drugCons { get; set; }
        public List<ItemIssueCompounds> compounds { get; set; }
    }
    public class ItemIssueDrugCons
    {
        public long itemId { get; set; }
        public decimal IssuedQty { get; set; }
        public long uomid { get; set; }
        public long? ARItemId { get; set; }
        public long pharmacy_prescription_id { get; set; }
        public bool is_SentHope { get; set; }
        public Int64 uom_idori { get; set; }
        public decimal uom_ratioori { get; set; }
    }
    public class ItemIssueCompounds
    {
        public long itemId { get; set; }
        public decimal IssuedQty { get; set; }
        public long uomid { get; set; }
        public long? ARItemId { get; set; }
        public Guid pharmacy_compound_header_id { get; set; }
        public Guid pharmacy_compound_detail_id { get; set; }
        public bool is_SentHope { get; set; }
        public Int64 uom_idori { get; set; }
        public decimal uom_ratioori { get; set; }
    }

    public class ItemIssueAdditionalItem
    {
        public long pharmacy_additional_item_id { get; set; }
        public long item_id { get; set; }
        public string quantity { get; set; }
        public string issued_qty { get; set; }
        public long uom_id { get; set; }
        public Int64 hope_aritem_id { get; set; }
        public short item_sequence { get; set; }
    }
}
