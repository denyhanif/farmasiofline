using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Siloam.Service.EMRPharmacy.Models.Functional
{
    public class PageTransactionHistory
    {
        public string PatientName { get; set; }
        public DateTime BirthDate { get; set; }
        public short SexId { get; set; }
        public string CentralizedMR { get; set; }
        public string LocalMR { get; set; }
        public string PresOrganizationCode { get; set; }
        public string PresAdmissionNo { get; set; }
        public string TransOrganizationCode { get; set; }
        public string TransAdmissionNo { get; set; }
        public string Payername { get; set; }
        public DateTime TransDate { get; set; }
        public string ItemCode { get; set; }
        public string ItemName { get; set; }
        public string Uom { get; set; }
        public string DoctorName { get; set; }
        public string PresQty { get; set; }
        public int Iter { get; set; }
        public string TotalQuantity { get; set; }
        public string IssuedQty { get; set; }
        public string ReturnQty { get; set; }
        public string OutstandingQty { get; set; }
        public string dosage_id { get; set; }
        public string dose_uom { get; set; }
        public string Frequency { get; set; }
        public string Route { get; set; }
        public string DoseText { get; set; }
        public string Remarks { get; set; }
        public string IsRoutine { get; set; }
        public string TotalPrice { get; set; }
        public string ConnStatus { get; set; }
    }
}
