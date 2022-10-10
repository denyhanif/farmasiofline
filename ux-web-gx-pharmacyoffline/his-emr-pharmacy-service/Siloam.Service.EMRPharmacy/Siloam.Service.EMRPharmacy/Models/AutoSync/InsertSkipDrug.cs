using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Siloam.Service.EMRPharmacy.Models.AutoSync
{
    public class InsertSkipDrug
    {
        public long OrganizationId { get; set; }
        public long PatientId { get; set; }
        public long AdmissionId { get; set; }
        public long DoctorId { get; set; }
        public Guid EncounterId { get; set; }
        public bool IsCancel { get; set; }
        public string Remarks { get; set; }
        public string DeliveryAddress { get; set; }
        public string DeliveryNotes { get; set; }
        public string Source { get; set; }
    }
}
