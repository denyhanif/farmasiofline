using System;
using System.Collections.Generic;

namespace Siloam.Service.EMRPharmacy.Models.AutoSync
{
    public class MySiloamRequestLabNew
    {
        public decimal totalPatientNet { get; set; }
        public decimal totalPayerNet { get; set; }
        public int payerCoverage { get; set; }
        public decimal totalPrice { get; set; }
        public DateTime rangeDateFrom { get; set; }
        public DateTime rangeDateTo { get; set; }
        public Guid siloamTrxId { get; set; }
        public List<MySiloamRequestLabDetail> items { get; set; }
    }
    public class MySiloamRequestLabDetail
    {
        public long itemId { get; set; }
        public string name { get; set; }
        public string patientNet { get; set; }
        public string payerNet { get; set; }
        public string remarks { get; set; }
    }
}
