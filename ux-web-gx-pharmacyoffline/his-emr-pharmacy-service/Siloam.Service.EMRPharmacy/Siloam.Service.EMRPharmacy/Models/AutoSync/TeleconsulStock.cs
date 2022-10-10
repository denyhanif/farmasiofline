using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Siloam.Service.EMRPharmacy.Models.AutoSync
{
    public class TeleconsulStock
    {
        public long OrganizationId { get; set; }
        public long PatientId { get; set; }
        public long AdmissionId { get; set; }
        public long DoctorId { get; set; }
        public Guid EncounterId { get; set; }
        public bool IsResend { get; set; }
        public int IsPrescription { get; set; }
        public int EmptyFlag { get; set; }
        public string latitude { get; set; }
        public string longitude { get; set; }
        public string city { get; set; }
        public string province { get; set; }
        public Guid appointmentid { get; set; }
        public string channelid { get; set; }
        public string deliveryaddress { get; set; }
        public string deliverynotes { get; set; }
        public long PayerId { get; set; }
        public string TransactionId { get; set; }
        public int IsPayer { get; set; }
        public bool isDilution { get; set; }
        public bool isInteraction { get; set; }

    }
}
