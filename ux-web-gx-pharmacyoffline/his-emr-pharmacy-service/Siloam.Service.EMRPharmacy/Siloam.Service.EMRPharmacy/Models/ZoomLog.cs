using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Siloam.Service.EMRPharmacy.Models
{
    public class ZoomLog
    {
        public Guid AppointmentId { get; set;}
        public Guid UserId { get; set; }
        public Guid HospitalId { get; set; }
    }

    public class RequestZoomLog
    {
        public Guid appointmentId { get; set; }
        public string statusId { get; set; }
        public string source { get; set; }
        public Guid userId { get; set; }
        public DateTime time { get; set; }
    }

    public class ResponseZoomLog
    {
        public string status { get; set; }
        public string message { get; set; }
    }
}
