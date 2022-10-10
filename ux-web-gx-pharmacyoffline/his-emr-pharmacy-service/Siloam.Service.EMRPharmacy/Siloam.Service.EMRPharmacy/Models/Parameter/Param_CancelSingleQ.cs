using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Siloam.Service.EMRPharmacy.Models.Parameter
{
    public class Param_CancelSingleQ
    {
        public long OrganizationId { get; set; }
        public long DoctorId { get; set; }
        public long PatientId { get; set; }
        public long AdmissionId { get; set; }
        public Guid Queuetrxid { get; set; }
        public string updateby { get; set; }
        public string jsonrequest_cancel_singleq { get; set; }
        public string jsonresponse_cancel_singleq { get; set; }
        public bool is_cancel { get; set; }
    }
}
