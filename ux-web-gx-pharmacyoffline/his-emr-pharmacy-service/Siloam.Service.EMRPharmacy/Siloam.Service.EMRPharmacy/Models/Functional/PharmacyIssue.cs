using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Siloam.Service.EMRPharmacy.Models.Functional
{
    public class PharmacyIssue
    {
        public Int64 PresOrganizationId { get; set; }
        public Int64 PresAdmissionId { get; set; }
        public Int64 TransOrganizationId { get; set; }
        public Guid EmrPrescriptionId { get; set; }
        public Int64 PrescriptionId { get; set; }
        public Int64 AdditionalId { get; set; }
        public string StockQuantity { get; set; }
        public Int64 ItemId { get; set; }
        public string ItemCode { get; set; }
        public string ItemName { get; set; }
        public string PresQuantity { get; set; }
        public int Iter { get; set; }
        public string OutstandingQuantity { get; set; }
        public string IssuedQuantity { get; set; }
        public Int64 UomId { get; set; }
        public string UomCode { get; set; }
        public Int64 FrequencyId { get; set; }
        public string FrequencyCode { get; set; }
        public Int64 RouteId { get; set; }
        public string RouteCode { get; set; }
        public string DosageId { get; set; }
        public long dose_uom_id { get; set; }
        public string dose_uom { get; set; }
        public string DoseText { get; set; }
        public string Remarks { get; set; }
        public Boolean IsRoutine { get; set; }
        public Boolean IsConsumables { get; set; }
        public Guid CompoundId { get; set; }
        public string CompoundName { get; set; }
        public Int64 DoctorId { get; set; }
        public string DoctorName { get; set; }
        public string SubTotalPrice { get; set; }
        public string ItemPrice { get; set; }
        public string Notes { get; set; }
        public int IsHistory { get; set; }
        public int IsAdditional { get; set; }
        public int IsNew { get; set; }
        public string ConnStatus { get; set; }
    }
}
