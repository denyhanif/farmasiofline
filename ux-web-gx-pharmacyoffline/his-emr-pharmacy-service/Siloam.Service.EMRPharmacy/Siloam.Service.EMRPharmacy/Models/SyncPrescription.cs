using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Siloam.Service.EMRPharmacy.Models
{
    public class SyncPrescription
    {
        public Guid MedicalOrderInterfaceId { get; set; }
        public Nullable<long> MedicalOrderId { get; set; }
        public Guid MedicalEncounterInterfaceId { get; set; }
        public short SalesPriorityId { get; set; }
        public string Notes { get; set; }
        public List<OrderItem> OrderItems { get; set; }
    }

    public class OrderItem
    {
        public Guid MedicalEncounterEntryInterfaceId { get; set; }
        public long DrugId { get; set; }
        public Nullable<long> AdministrationFrequencyId { get; set; }
        public string Dose { get; set; }
        public string DoseText { get; set; }
        public Nullable<long> DoseUomId { get; set; }
        public bool IsPrn { get; set; }
        public Nullable<long> AdministrationRouteId { get; set; }
        public string DispensingInstruction { get; set; }
        public string AdministrationInstruction { get; set; }
        public string PatientInformation { get; set; }
        public string Quantity { get; set; }
        public int Repeat { get; set; }
    }
}
