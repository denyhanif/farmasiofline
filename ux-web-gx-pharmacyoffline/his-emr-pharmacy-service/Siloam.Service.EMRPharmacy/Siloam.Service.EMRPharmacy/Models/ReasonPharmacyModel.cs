using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Siloam.Service.EMRPharmacy.Models
{
    public class ReasonPharmacyModel
    {
        public long ReasonId { set; get; }
        public string Reason { set; get; }
        public bool IsActive { set; get; }
        public int Sequence { set; get; }
        public string CreatedBy { set; get; }
        public DateTime CreatedDate { set; get; }
        public string ModifiedBy { set; get; }
        public DateTime ModifiedDate { set; get; }
    }
}
