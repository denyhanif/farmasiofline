using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Siloam.Service.EMRPharmacy.Models.AutoSync
{
    public class CpoeStock
    {
        public long OrganizationId { get; set; }
        public Guid EncounterId { get; set; }
        public long AdmissionId { get; set; }
        public Guid AppointmentId { get; set; }
        public long PatientId { get; set; }
        public long DoctorId { get; set; }
        public long ItemId { get; set; }
        public string ItemType { get; set; }
        public bool IsCito { get; set; }
        public string SalesItemName { get; set; }

    }
}
