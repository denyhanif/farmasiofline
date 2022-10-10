using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Siloam.Service.EMRPharmacy.Models.Parameter
{
    public class Param_RecordSubmit
    {
        public long SubmitBy { get; set; }
        public long TransAdmId { get; set; }
        public string TransAdmNo { get; set; }
        public string QueueNo { get; set; }
        public string DeliveryFee { get; set; }
        public string PayerCoverage { get; set; }
        public string ResponseWorklistSingleQueue { get; set; }
        public PharmacyData PharmacyDataModel { get; set; }
        public SingleQueue SingleQueueDataModel { get; set; }
    }
}
