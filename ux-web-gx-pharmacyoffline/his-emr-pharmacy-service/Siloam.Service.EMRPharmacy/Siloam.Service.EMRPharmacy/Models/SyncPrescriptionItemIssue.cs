using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Siloam.Service.EMRPharmacy.Models
{
    public class SyncPrescriptionItemIssue
    {
        public Guid MedicalEncounterInterfaceId { get; set; }
        public Guid MedicalOrderInterfaceId { get; set; }
        public Nullable<long> MedicalOrderId { get; set; }
        public short SalesPriorityId { get; set; }
        public itemIssued itemIssued { get; set; }
    }
    public class itemIssued
    {
        public List<itemIssue> itemIssue { get; set; }
    }
    public class itemIssue
    {
        public long itemId { get; set; }
        public decimal quantity { get; set; }
        public long uomid { get; set; }
        
    }
}
