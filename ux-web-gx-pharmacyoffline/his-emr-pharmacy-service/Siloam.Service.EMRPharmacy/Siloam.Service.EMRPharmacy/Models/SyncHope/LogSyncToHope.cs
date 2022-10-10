using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Siloam.Service.EMRPharmacy.Models.SyncHope
{
    public class LogSyncToHope
	{
		public long organization_hope_id { get; set; }
		public long admission_hope_id { get; set; }
		public long store_hope_id { get; set; }
		public long doctor_hope_id { get; set; }
		public long user_hope_id { get; set; }
		public string jsonrequest_senditemissue { get; set; }
		public string jsonresponse_sendItemIssue { get; set; }
		public DateTime startTime { get; set; }
		//public DateTime endTime { get; set; }
		public long admission_hope_id_SentHope { get; set; }
	}
}
