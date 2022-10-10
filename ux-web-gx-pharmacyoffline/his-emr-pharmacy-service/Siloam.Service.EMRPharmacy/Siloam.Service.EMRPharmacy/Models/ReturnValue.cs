using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Siloam.Service.EMRPharmacy.Models
{
    public class ReturnValue
    {
        public string Type { get; set; }
        public string Message { get; set; }
        public string DetailMessage { get; set; }
        public string ResultEntityId { get; set; }
        public int AffectedCount { get; set; }
    }

    public class ResultReturnValue
    {
        private ReturnValue lists = new ReturnValue();
        [JsonProperty("data")]
        public ReturnValue list { get { return lists; } }
    }
}
