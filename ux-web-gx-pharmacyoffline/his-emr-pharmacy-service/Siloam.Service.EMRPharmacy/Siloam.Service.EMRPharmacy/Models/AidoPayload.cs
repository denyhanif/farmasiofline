using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Siloam.Service.EMRPharmacy.Models
{
    public class AidoPayload
    {
        public AidoPayloadData data { get; set; }
        public int iat { get; set; }
        public int exp { get; set; }
    }

    public class AidoPayloadData
    {
        public string partner { get; set; }
        public string orderId { get; set; }
        public string pharmacyId { get; set; }
    }

    public class AidoPayloadNew
    {
        public string role { get; set; }
        public string partner { get; set; }
        public int iat { get; set; }
        public int exp { get; set; }
        public string aud { get; set; }
        public string iss { get; set; }
    }
}
