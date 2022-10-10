using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Siloam.Service.EMRPharmacy.Models
{
    public class PrescriptionHistory
    {
        public HeaderPrescriptionHistory header;
        public List<EditedPrescription> doctor_prescriptions;
        public List<EditedPrescription> pharmacist_prescriptions;
    }

    public class HeaderPrescriptionHistory
    {
        public Guid encounter_id { get; set; }
        public String call_doctor { get; set; }
        public long modified_by { get; set; }
        public DateTime modified_date { set; get; }
        public string reason_remarks { set; get; }
        public string pharmacy_notes { set; get; }

    }

    public class EditedPrescription
    {
        public string item_name { get; set; }
        public string compound_name { get; set; }
        public string edit_action { get; set; }
        public string quantity { get; set; }
        public string uom { get; set; }
        public string frequency { get; set; }
        public string dose { get; set; }
        public string dose_uom { get; set; }
        public string administration_route { get; set; }
        public DateTime created_date { get; set; }
        public string instruction {get;set;}
       
        public string edit_reason { get; set; }
        public bool is_compound { get; set; }
        public bool is_compound_header { get; set; }    
        public bool is_additional { get; set; }
        public bool is_consumable { get; set; }
        public int item_sequence { get; set; }
        public Guid doctor_prescription_id { get; set; }
        public long phar_prescription_id { get; set; }
        public string data_type { get; set; }


    }

    public class PharmacistRelease
    {
        public string item_name { get; set; }
        public string compound_name { get; set; }
        public string edit_action { get; set; }
        public string quantity { get; set; }
        public string uom { get; set; }
        public string frequency { get; set; }
        public string dose { get; set; }
        public string dose_uom { get; set; }
        public string administration_route { get; set; }
        public DateTime created_date { get; set; }
        public string instruction { get; set; }

        public string edit_reason { get; set; }
        public bool is_compound { get; set; }
        public bool is_compound_header { get; set; }
        public bool is_additional { get; set; }
        public bool is_consumable { get; set; }
        public int item_sequence { get; set; }
        public Guid doctor_prescription_id { get; set; }
        public long phar_prescription_id { get; set; }
        public string data_type { get; set; }


    }
}
