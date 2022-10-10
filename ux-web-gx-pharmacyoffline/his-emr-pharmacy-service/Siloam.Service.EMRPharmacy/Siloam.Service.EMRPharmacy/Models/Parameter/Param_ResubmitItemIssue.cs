using Siloam.Service.EMRPharmacy.Models.SyncHope;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Siloam.Service.EMRPharmacy.Models.Parameter
{
    public class Param_ResubmitItemIssue
    {
        public long OrganizationId { get; set; }
        public long PatientId { get; set; }
        public long DoctorId { get; set; }
        public long store_id { get; set; }
        public long Admissionid { get; set; }
        public Guid EncounterId { get; set; }
        public long SubmitBy { get; set; }
        public List<ItemIssueDrugCons> drugCons { get; set; }
        public List<ItemIssueCompounds> compounds { get; set; }
        public List<ItemIssueAdditionalItem> additionalItems { get; set; }
        public PharmacyAppropriatnessReview appropriatnessReview { get; set; }
        public long Admissionid_SentHope { get; set; }
    }
}
