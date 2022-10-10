using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Siloam.Service.EMRPharmacy.Models
{
    public class AidoDrugTicket
    {
        public long aido_drug_ticket_id { get; set; }
        public string aido_transaction_id { get; set; }
        public Guid siloam_trx_id { get; set; }
        public long organization_id { get; set; }
        public long patient_id { get; set; }
        public long admission_id { get; set; }
        public Guid encounter_id { get; set; }
        public bool is_payment { get; set; }
        public string jsonrequest_send_drug { get; set; }
        public string jsonresponse_send_drug { get; set; }
        public bool is_active { get; set; }
        public DateTime created_date { get; set; }
        public DateTime modified_date { get; set; }
        public bool is_readydrug { get; set; }
    }
}
