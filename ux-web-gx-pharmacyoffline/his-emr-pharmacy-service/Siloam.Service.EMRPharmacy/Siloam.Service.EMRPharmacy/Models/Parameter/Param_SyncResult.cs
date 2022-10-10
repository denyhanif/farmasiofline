using Siloam.Service.EMRPharmacy.Models.SyncHope;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Siloam.Service.EMRPharmacy.Models.Parameter
{
    public class Param_SyncResult
    {
        public string SubmitPressResult { get; set; }
        public string HopeStatusResult { get; set; }
        public string HopeMessageResult { get; set; }
        public List<DrugItem> HopedataResult { get; set; }
        public string SingleQStatusResult { get; set; }
        public string SingleQMessageResult { get; set; }
    }
}
