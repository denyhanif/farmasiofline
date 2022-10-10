using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Siloam.Service.EMRPharmacy.Models.ViewModels
{
    public class ViewOrganizationSetting
    {
        public Guid organization_setting_id { get; set; }
        public long organization_id { get; set; }
        public string setting_name { get; set; }
        public string setting_value { get; set; }
        public bool is_active { get; set; }
    }
}
