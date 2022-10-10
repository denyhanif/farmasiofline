using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Siloam.Service.EMRPharmacy.Models.Functional;

namespace Siloam.Service.EMRPharmacy.Models.SyncHope
{
    public class ResubmitHope
    {
    }
    public class ResponseResubmitHope
    {
        public string ResubmitStatusResult { get; set; }
        public string HopeStatusResult { get; set; }
        public string HopeMessageResult { get; set; }
        public List<DrugItem> HopedataResult { get; set; }
        public PharmacyPickingList pharmacyPickingList { get; set; }
    }
}
