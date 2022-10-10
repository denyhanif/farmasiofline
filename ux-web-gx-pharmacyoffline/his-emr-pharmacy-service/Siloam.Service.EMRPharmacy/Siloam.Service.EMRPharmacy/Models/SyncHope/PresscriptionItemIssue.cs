using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Siloam.Service.EMRPharmacy.Models.SyncHope
{
    public class PresscriptionItemIssue
    {
        public Guid MedicalEncounterInterfaceId { get; set; }
        public Guid MedicalOrderInterfaceId { get; set; }
        public Nullable<long> MedicalOrderId { get; set; }
        public short SalesPriorityId { get; set; }
        public List<itemIssue> itemIssued { get; set; }
    }
}
