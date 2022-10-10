﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Siloam.Service.EMRPharmacy.Models
{
    public class PharmacyItemIssue
    {
        public long pharmacy_item_issue_id { get; set; }
        public long organization_id { get; set; }
        public long admission_id { get; set; }
        public long ar_item_id { get; set; }
        public long sales_item_id { get; set; }
        public DateTime created_date { get; set; }
        public string created_by { get; set; }
        public DateTime modified_date { get; set; }
        public string modified_by { get; set; }
    }
}