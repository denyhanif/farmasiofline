using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Siloam.Service.EMRPharmacy.Models.Functional
{
    public class PharmacyPrintLabelPrescription
    {
        public Guid print_label_prescription_id { get; set; }
        public Guid origin_prescompound_id { get; set; }
        public Int64 origin_prescription_id { get; set; }
        public string item_name { get; set; }
        public Decimal quantity { get; set; }
        public string uom_code { get; set; }
        public Decimal dosage_id { get; set; }
        public Int64 dose_uom_id { get; set; }
        public string dose_uom { get; set; }
        public Int64 frequency_id { get; set; }
        public string frequency_code { get; set; }
        public string dose_text { get; set; }
        public Int64 administration_route_id { get; set; }
        public string administration_route_code { get; set; }
        public string remarks { get; set; }
        public string presc_type { get; set; }
        public string PatientInfo { get; set; }
        public bool IsInternal { get; set; }
        public string CompundNotes { get; set; }
        public string alternate_quantity { get; set; }
        public Int64 ar_item_id { get; set; }
        public bool IsCompound { get; set; }
        public string QRStringCode { get; set; }
        public string IssueCode { get; set; }
        public string IsueGroup { get; set; }
    }

    public class RequestPrintLabelPrescription
    {
        public List<PharmacyPrintLabelPrescription> paramprintlabel { get; set; }
    }
}
