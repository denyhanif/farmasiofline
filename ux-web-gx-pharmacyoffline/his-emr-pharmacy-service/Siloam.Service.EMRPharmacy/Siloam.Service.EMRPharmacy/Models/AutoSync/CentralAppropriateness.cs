using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Siloam.Service.EMRPharmacy.Models.AutoSync
{
    public class CentralAppropriateness
    {
        public Guid central_appropriateness_id { get; set; }
        public long organization_id { get; set; }
        public long patient_id { get; set; }
        public long admission_id { get; set; }
        public Guid encounter_ticket_id { get; set; }
        public bool is_active { get; set; }
        public DateTime appropriate_date { get; set; }
        public string appropriate_by { get; set; }
    }

    public class InsertAppropriateness
    {
        public long OrganizationId { get; set; }
        public long PatientId { get; set; }
        public long AdmissionId { get; set; }
        public Guid EncounterId { get; set; }
        public long UserId { get; set; }
        public bool IsActive { get; set; }
    }
}
