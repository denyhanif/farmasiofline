using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Siloam.Service.EMRPharmacy.Models.Functional
{
    public class PharmacyTransactionHistory
    {
        public string DrugName { get; set; }
        public string TotalQuantity { get; set; }
        public string PrescriptionRegistration { get; set; }
        public string TransactionRegistration { get; set; }
        public string IssuedBy { get; set; }
        public string IssuedQty { get; set; }
        public string ReturnQty { get; set; }
        public string OutstandingQty { get; set; }
        public string StoreName { get; set; }
        public string DoctorName { get; set; }
        public string HospitalName { get; set; }
        public string Notes { get; set; }
        public string ConnStatus { get; set; }
    }
}
