﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Siloam.Service.EMRPharmacy.Models
{
    public class DrugsToready
    {
        public Guid drugs_toready_id { get; set; }
        public Guid encounter_ticket_id { get; set; }
        public long admission_id { get; set; }
        public long organization_id { get; set; }
        public bool is_additional { get; set; }
        public bool is_ready { get; set; }
        public bool is_tele { get; set; }
        public DateTime created_date { get; set; }
        public string created_by { get; set; }
        public DateTime modified_date { get; set; }
        public string modified_by { get; set; }
    }
}
