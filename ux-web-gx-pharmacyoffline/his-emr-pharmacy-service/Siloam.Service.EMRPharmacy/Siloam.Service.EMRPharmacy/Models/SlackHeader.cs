using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Siloam.Service.EMRPharmacy.Models
{
    public class SlackHeader
    {
        public string text { get; set; }
        public List<SlackDetail> attachments { get; set; }
    }

    public class SlackDetail
    {
        public string fallback { get; set; }
        public string author_name { get; set; }
        public string title { get; set; }
        public string text { get; set; }
    }
}
