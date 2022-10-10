using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Siloam.Service.EMRPharmacy.Models.AutoSync
{
    public class LogReadyPickup
    {
        public long LogReadyPickupId { get; set; }
        public long OrganizationId { get; set; }
        public long PatientId { get; set; }
        public long AdmissionId { get; set; }
        public long DoctorId { get; set; }
        public Guid EncounterId { get; set; }
        public Guid AppointmentId { get; set; }
        public string Status { get; set; }
        public string JsonRequest { get; set; }
        public string JsonResponse { get; set; }
        public DateTime CreateDate { get; set; }
        public string CreateBy { get; set; }
    }

    public class RequestReadyPickup
    {
        public long OrganizationId { get; set; }
        public long PatientId { get; set; }
        public long AdmissionId { get; set; }
        public long DoctorId { get; set; }
        public Guid EncounterId { get; set; }
        public long UserId { get; set; }
        public string UserName { get; set; }
        public long IsSelfPickup { get; set; }
    }

    public class MySiloamRequestReadyPickup
    {
        public string encounterId { get; set; }
        public string userId { get; set; }
        public string source { get; set; }
        public string userName { get; set; }
    }

    public class RequestReadyPickupAido
    {
        public string siloamTrxId { get; set; }
    }
}
