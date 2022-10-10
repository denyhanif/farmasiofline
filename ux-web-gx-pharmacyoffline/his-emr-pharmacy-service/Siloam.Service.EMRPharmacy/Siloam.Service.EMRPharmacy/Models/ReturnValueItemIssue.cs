using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Siloam.Service.EMRPharmacy.Models
{
    public class ReturnValueItemIssue
    {
        public string company { get; set; }
        public string status { get; set; }
        public int code { get; set; }
        public string message { get; set; }
        public Data data { get; set; }
        public int cp { get; set; }
        public int total { get; set; }

        public class Data
        {
            public Itemissue[] itemIssue { get; set; }
        }

        public class Itemissue
        {
            public long ItemId { get; set; }
            public long? ArItemId { get; set; }
        }
    }
}
