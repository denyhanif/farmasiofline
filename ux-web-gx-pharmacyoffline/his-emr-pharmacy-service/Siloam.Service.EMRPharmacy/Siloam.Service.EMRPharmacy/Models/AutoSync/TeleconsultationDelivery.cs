using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Siloam.Service.EMRPharmacy.Models.AutoSync
{
    public class TeleconsultationDeliveryHeader
    {
        public decimal distance { get; set; }
        public List<TeleconsultationDelivery> detail { get; set; }
    }

    public class TeleconsultationDelivery
    {
        public long price_header_id { get; set; }
        public string price_type_name { get; set; }
        public decimal amount { get; set; }
        public int estimation { get; set; }
        public string remarks { get; set; }
    }

    public class ResultTeleconsultationDelivery
    {
        private TeleconsultationDeliveryHeader lists = new TeleconsultationDeliveryHeader();
        [JsonProperty("data")]
        public TeleconsultationDeliveryHeader list { get { return lists; } }
    }

    public class DrugDelivery
    {
        public long organization_id { get; set; }
        public decimal weight { get; set; }
        public DeliveryDetail destination { get; set; }
        public string travelMode { get; set; }
        public bool avoidHighways { get; set; }
        public bool avoidTolls { get; set; }
    }

    public class DeliveryDetail
    {
        public string latitude { get; set; }
        public string longtitude { get; set; }
        public string city { get; set; }
        public string province { get; set; }
    }
}
