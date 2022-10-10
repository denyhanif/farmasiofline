using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Siloam.Service.EMRPharmacy.Models
{
    public class LogTemp
    {
        public long log_id { get; set; }
        public Guid encounter_id { get; set; }
        public string log_type { get; set; }
        public string soap_return { get; set; }
        public DateTime created_date { get; set; }
    }
}
