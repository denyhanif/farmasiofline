using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Siloam.Service.EMRPharmacy.Models.ViewModels
{
    public class ViewTransactionHeader
    {
        public long pharmacy_transaction_header_id { get; set; }
        public string transaction_admission_no { get; set; }
        public DateTime transaction_date { get; set; }
    }
}
