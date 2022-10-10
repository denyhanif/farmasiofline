using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Siloam.Service.EMRPharmacy.Models.SyncHope
{
    public class DrugItem
    {
        public long ItemId { get; set; }
        public long? ARItemId { get; set; }
    }

    public class DrugItemResponse
    {
        public string company { get; set; }
        public string status { get; set; }
        public int code { get; set; }
        public string message { get; set; }
        public List<DrugItem> data { get; set; }
        public int cp { get; set; }
        public int total { get; set; }

    }
}
