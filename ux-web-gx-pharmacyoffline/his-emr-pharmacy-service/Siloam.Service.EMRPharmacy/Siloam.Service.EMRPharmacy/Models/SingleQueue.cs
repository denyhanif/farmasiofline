using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Siloam.Service.EMRPharmacy.Models
{
    public class SingleQueue
    {
        public Guid queue_engine_trx_id { get; set; }
        public Guid queue_engine_id { get; set; }
        //public string transaction_type_id { get; set; }
        //public string prescription_type_id { get; set; }
        //public string url_detail_trx { get; set; }
        //public string status_id { get; set; }
        //public Guid queue_line_hospital_id { get; set; }
        public bool is_retail { get; set; }
        public bool is_cancel { get; set; }
        //public bool is_using_pharmacy { get; set; }
        //public Nullable<int> reason_id { get; set; }
        //public string created_name { get; set; }
        //public Nullable<DateTime> created_date { get; set; }
        //public string created_from { get; set; }
        //public string modified_by { get; set; }
        //public Nullable<DateTime> modified_from { get; set; }
        //public string modified_name { get; set; }

    }

    public class ResultListSingleQueue
    {
        public List<SingleQueue> data { set; get; }
        public string status { set; get; }
        public string message { set; get; }
    }
    public class ResultSingleQueue
    {
        public SingleQueue data { set; get; }
        public string status { set; get; }
        public string message { set; get; }
    }
}
