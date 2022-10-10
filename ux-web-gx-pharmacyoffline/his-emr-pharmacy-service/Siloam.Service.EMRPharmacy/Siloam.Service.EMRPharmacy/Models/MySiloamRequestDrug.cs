using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Siloam.Service.EMRPharmacy.Models
{
    public class MySiloamRequestDrug
    {
        public int totalPatientNet { get; set; }
        public int totalPayerNet { get; set; }
        public int payerCoverage { get; set; }
        public int totalPrice { get; set; }
        public bool isSelfCollection { get; set; }
        public List<MySiloamDrug> items { get; set; }
        public string admissionHopeId { get; set; }
        public string userId { get; set; }
        public string source { get; set; }
        public string userName { get; set; }
        public Guid siloamTrxId { get; set; }
        public Guid appointmentId { get; set; }
    }

    public class MySiloamDrug
    {
        public long itemId { get; set; }
        public string name { get; set; }
        public string qty { get; set; }
        public string uom { get; set; }
        public string patientNet { get; set; }
        public string payerNet { get; set; }
        public string frequency { get; set; }
        public string instruction { get; set; }
    }

    public class MySiloamCancelItem
    {
        public Guid SiloamTrxId { get; set; }
        public List<ItemCancel> items { get; set; }
    }

    public class ItemCancel
    {
        public long SalesItemId { get; set; }
        public string SalesItemName { get; set; }
    }
}
