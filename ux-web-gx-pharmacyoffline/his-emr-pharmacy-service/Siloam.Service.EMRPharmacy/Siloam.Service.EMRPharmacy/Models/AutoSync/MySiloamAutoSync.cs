using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Siloam.Service.EMRPharmacy.Models.AutoSync
{
    public class MySiloamAutoSync
    {
        public Guid appointmentId { get; set; }
        public long hospitalHopeId { get; set; }
        public string doctorHopeId { get; set; }
        public Guid encounterId { get; set; }
        public string patientHopeId { get; set; }
        public string admissionHopeId { get; set; }
        public string userId { get; set; }
        public string source { get; set; }
        public string userName { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public MySiloamRequestDrugNew mySiloamRequestDrugNew { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public MySiloamRequestLabNew mySiloamRequestLabNew { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public MySiloamRequestRadNew mySiloamRequestRadNew { get; set; }
    }
}
