using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Siloam.Service.EMRPharmacy.Models
{
    public class SyncConsumables
    {
        public Guid MedicalOrderInterfaceId { get; set; }
        public Nullable<long> MedicalOrderId { get; set; }
        public Guid MedicalEncounterInterfaceId { get; set; }
        public short SalesPriorityId { get; set; }
        public string Notes { get; set; }
        public List<OrderItemConsumables> OrderItems { get; set; }
    }

    public class OrderItemConsumables
    {
        public Guid MedicalEncounterEntryInterfaceId { get; set; }
        public long ItemId { get; set; }
        public string DispensingInstruction { get; set; }
        public string UsageInstruction { get; set; }
        public string PatientInformation { get; set; }
        public string Quantity { get; set; }
    }
}
