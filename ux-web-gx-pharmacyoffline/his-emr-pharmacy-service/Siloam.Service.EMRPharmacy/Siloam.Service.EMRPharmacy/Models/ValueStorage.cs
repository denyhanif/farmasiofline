using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Siloam.Service.EMRPharmacy.Models
{
    public class ValueStorage
    {
        public static string SlackUrl { get; set; }
        public static string ApiUrl { get; set; }
        public static string ApiName { get; set; }
        public static string UrlSync { get; set; }
        public static string CompoundItem { get; set; }
        public static string AidoUrlSync { get; set; }
        public static string AidoSecret { get; set; }
        public static string AidoPharmacyId { get; set; }
        public static string MySiloamUrlSync { get; set; }
        public static string MySiloamUrlQueueEngine { get; set; }
        public static string UrlDeliveryFee { get; set; }
        public static string AidoUrlSyncNew { get; set; }
        public static string AidoUrlSyncDrug { get; set; }
        public static string AidoSecretV2 { get; set; }
        public static string IntegrationConnectionString { get; set; }
    }
}
