using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Siloam.Service.EMRPharmacy.Models
{
    public class SingleQWorklist
    {
        public string transactionTypeId { get; set; }
        public string prescriptionTypeId { get; set; }
        public string urlDetailTrx { get; set; }
        public Guid queueLineHospitalId { get; set; }
        public Nullable<Guid> visitTypeHospitalId { get; set; }
        public Nullable<DateTime> printVerifyTime { get; set; }
        public Int64 storeLocationId { get; set; }
        public string storeLocationName { get; set; }
    }

    public class SingleQWorklistData
    {
        public Guid patientVisitId { get; set; }
        public List<SingleQWorklist> transactionList { set; get; }
        public string source { get; set; }
        public string userId { get; set; }
        public string userName { get; set; }
        public bool isFromPharmacy { get; set; }
        public Guid hospitalId { get; set; }
    }
}
