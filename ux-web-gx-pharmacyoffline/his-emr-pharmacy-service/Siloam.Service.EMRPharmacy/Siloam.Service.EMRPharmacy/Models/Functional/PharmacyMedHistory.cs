using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Siloam.Service.EMRPharmacy.Models.Functional
{
    public class PharmacyMedHistory
    {
        public string DoctorName { get; set; }
        public string OrganizationName { get; set; }
        public long PrescriptionId { get; set; }
        public long ItemId { get; set; }
        public string ItemName { get; set; }
        public Decimal Quantity { get; set; }
        public string UomCode { get; set; }
        public string FrequencyCode { get; set; }
        public Decimal DosageId { get; set; }
        public string DoseText { get; set; }
        public string Instruction { get; set; }
        public string RouteCode { get; set; }
        public bool IsRoutine { get; set; }
        public long OriginPrescriptionId { get; set; }
        public Guid CompoundId { get; set; }
        public string CompoundName { get; set; }
        public int Iter { get; set; }
        public bool IsConsumables { get; set; }
        public bool IsAdditional { get; set; }
    }
}
