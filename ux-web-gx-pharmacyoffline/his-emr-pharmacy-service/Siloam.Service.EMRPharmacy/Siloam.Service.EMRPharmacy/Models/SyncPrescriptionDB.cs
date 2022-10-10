using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Siloam.Service.EMRPharmacy.Models
{
    public class SyncPrescriptionDB
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
        public bool is_consumables { get; set; }
        public string phar_notes { get; set; }
    }
}
