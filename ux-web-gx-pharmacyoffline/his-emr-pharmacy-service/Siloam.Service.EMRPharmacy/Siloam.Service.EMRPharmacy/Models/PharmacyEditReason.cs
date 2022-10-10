using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Siloam.Service.EMRPharmacy.Models
{
    public class PharmacyEditReason
    {
        public long EditReasonId { set; get; }
        public long RecordId { set; get; }
        public bool IsAdditional { set; get; }
        public long ReasonId { set; get; }
        public string ReasonRemarks { set; get; }
        public string CallDoctor { set; get; }
        public bool IsActive { set; get; }
        public string CallDoctorDate { set; get; }
        public string CallDoctorTime { set; get; }
        public string CreatedBy { set; get; }
        public DateTime CreatedDate { set; get; }
        public string ModifiedBy { set; get; }
        public DateTime ModifiedDate { set; get; }
    }
}
