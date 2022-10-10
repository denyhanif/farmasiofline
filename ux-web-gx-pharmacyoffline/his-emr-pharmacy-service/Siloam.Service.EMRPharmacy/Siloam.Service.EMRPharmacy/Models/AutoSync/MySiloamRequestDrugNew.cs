using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Siloam.Service.EMRPharmacy.Models.AutoSync
{
    public class MySiloamRequestDrugNew
    {
        public int totalPatientNet { get; set; }
        public int totalPayerNet { get; set; }
        public int payerCoverage { get; set; }
        public int totalPrice { get; set; }
        public bool isSelfCollection { get; set; }
        public List<MySiloamDrugNew> items { get; set; }
        public List<DeliveryFeeType> deliveries { get; set; }
        public string admissionHopeId { get; set; }
        public string hospitalHopeId { get; set; }
        public string patientHopeId { get; set; }
        public string doctorHopeId { get; set; }
        public string encounterId { get; set; }
        public string userId { get; set; }
        public string source { get; set; }
        public string userName { get; set; }
        public Guid siloamTrxId { get; set; }
        public Guid appointmentId { get; set; }
        public string transactionId { get; set; }
        public bool isPrescribe { get; set; }
    }

    public class MySiloamDrugNew
    {
        public long itemId { get; set; }
        public string name { get; set; }
        public string qty { get; set; }
        public string uom { get; set; }
        public string patientNet { get; set; }
        public string payerNet { get; set; }
        public string frequency { get; set; }
        public string instruction { get; set; }
        public bool isSendAvailable { get; set; }
    }

    public class DeliveryFeeType
    {
        public long deliveryHeaderId { get; set; }
        public string name { get; set; }
        public decimal amount { get; set; }
        public int estimation { get; set; }
        public string remarks { get; set; }
    }
}
