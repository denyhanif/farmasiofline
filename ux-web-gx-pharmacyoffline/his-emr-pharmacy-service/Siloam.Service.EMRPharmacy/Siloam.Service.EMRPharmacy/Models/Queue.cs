using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Siloam.Service.EMRPharmacy.Models
{
    public class Queue
    {
        public Guid reasonId { get; set; }
        public string source { get; set; }
        public string userId { get; set; }
        public string userName { get; set; }
    }

}
