using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Siloam.Service.EMRPharmacy.Models
{
    public class PharmacyData
    {
        public PharmacyHeader header { get; set; }
        public List<PharmacyDiagnosisSigns> diagnosissigns { get; set; }
        public List<PharmacyAllergy> allergy { get; set; }
        public List<PharmacyPrescription> prescription { get; set; }
        public List<PharmacyDrugInfo> druginfo { get; set; }
        public DateTime LastModifiedDate { get; set; }
        public List<PharmacyCompoundHeader> compound_header { get; set; }
        public List<PharmacyCompoundDetail> compound_detail { get; set; }
        public SingleQWorklistData singleQWorklistData { get; set; }
        public List<PharmacyEditReason> editReason { get; set; }
        public PharmacyAppropriatnessReview appropriatnessReview { get; set; }
        public List<PharmacyAdditionalItem> additionalItem { get; set; }
        public List<PharmacyEditedMapping> pharmacyEditedMappings { get; set; }
        
    }

    public class PharmacyHeader
    {
        public Guid EncounterId { get; set; }
        public Int64 OrganizationId { get; set; }
        public Int64 Admissionid { get; set; }
        public string AdmissionNo { get; set; }
        public string LocalMrNo { get; set; }
        public Int64 PatientId { get; set; }
        public string PatientName { get; set; }
        public string ChannelId { get; set; }
        public Nullable<DateTime> IncomingDate { get; set; }
        public Int64 DoctorId { get; set; }
        public string DoctorName { get; set; }
        public Int64 SpecialtyId { get; set; }
        public string SpecialtyName { get; set; }
        public string PharmacyNotes { get; set; }
        public DateTime TakeDate { get; set; }
        public string TakeBy { get; set; }
        public Int64 HopeUserId { get; set; }
        public Boolean IsEditDrug { get; set; }
        public Boolean IsEditCompound { get; set; }
        public Boolean IsEditConsumables { get; set; }
        public string QueueNo { get; set; }
        public string DeliveryFee { get; set; }
        public string PayerCoverage { get; set; }
        public bool IsSelfCollection { get; set; }
        public Int64 store_id { get; set; }
        public string prefix_desc { get; set; }
        public bool is_tele { get; set; }
        public bool is_SingleQueue { get; set; }
        public Nullable<DateTime> VerifyTime { get; set; }
        public bool is_SendDataItemIssue { get; set; }
        public Int64 Admissionid_SentHope { get; set; }
    }

    public class PharmacyDiagnosisSigns
    {
        public Guid MappingId { get; set; }
        public string MappingName { get; set; }
        public string Value { get; set; }
        public string Remarks { get; set; }
    }

    public class PharmacyAllergy
    {
        public string AllergyType { get; set; }
        public string Allergy { get; set; }
        public string AllergyReaction { get; set; }
    }

    public class PharmacyPrescription
    {
        public Guid prescription_id { get; set; }
        public string prescription_no { get; set; }
        public Int64 item_id { get; set; }
        public string item_name { get; set; }
        public string quantity { get; set; }
        public Int64 uom_id { get; set; }
        public string uom_code { get; set; }
        public Int64 frequency_id { get; set; }
        public string frequency_code { get; set; }
        public string dosage_id { get; set; }
        public long dose_uom_id { get; set; }
        public string dose_uom { get; set; }
        public string dose_text { get; set; }
        public Int64 administration_route_id { get; set; }
        public string administration_route_code { get; set; }
        public int iteration { get; set; }
        public string remarks { get; set; }
        public int is_routine { get; set; }
        public int is_consumables { get; set; }
        public Guid compound_id { get; set; }
        public string compound_name { get; set; }
        public Guid origin_prescription_id { get; set; }
        public Int64 hope_aritem_id { get; set; }
        public string doctor_name { get; set; }
        public string SubStoreQuantity { get; set; }
        public string MainStoreQuantity { get; set; }
        public Guid prescription_sync_id { get; set; }
        public short item_sequence { get; set; }
        public string IssuedQty { get; set; }
        public bool is_SentHope { get; set; }
        public Int64 uom_idori { get; set; }
        public string uom_codeori { get; set; }
        public string uom_ratioori { get; set; }
    }

    public class PharmacyDrugInfo
    {
        public Guid pharmacy_druginfo_id { get; set; }
        public Guid pharmacy_mapping_id { get; set; }
        public string pharmacy_mapping_name { get; set; }
        public string value { get; set; }
        public string remarks { get; set; }
    }

    public class PharmacyCompoundHeader
    {
        public Guid prescription_compound_header_id { get; set; }
        public string compound_name { get; set; }
        public string quantity { get; set; }
        public long uom_id { get; set; }
        public string uom_code { get; set; }
        public string dose { get; set; }
        public long dose_uom_id { get; set; }
        public string dose_uom { get; set; }
        public long administration_frequency_id { get; set; }
        public string frequency_code { get; set; }
        public long administration_route_id { get; set; }
        public string administration_route_code { get; set; }
        public string administration_instruction { get; set; }
        public string compound_note { get; set; }
        public int iter { get; set; }
        public bool is_additional { get; set; }
        public short item_sequence { get; set; }
        public Guid compound_header_sync_id { get; set; }
        public string dose_text { get; set; }
        public bool IsDoseText { get; set; }
        public string IssuedQty { get; set; }
    }

    public class PharmacyCompoundDetail
    {
        public Guid prescription_compound_detail_id { get; set; }
        public Guid prescription_compound_header_id { get; set; }
        public long item_id { get; set; }
        public string item_name { get; set; }
        public string quantity { get; set; }
        public string IssuedQty { get; set; }
        public long uom_id { get; set; }
        public string uom_code { get; set; }
        public long administration_frequency_id { get; set; }
        public long administration_route_id { get; set; }
        public short item_sequence { get; set; }
        public bool is_additional { get; set; }
        public string SubStoreQuantity { get; set; }
        public string MainStoreQuantity { get; set; }
        public Guid compound_detail_sync_id { get; set; }
        public string dose { get; set; }
        public long dose_uom_id { get; set; }
        public string dose_uom_code { get; set; }
        public string dose_text { get; set; }
        public bool IsDoseText { get; set; }
        public Int64 hope_aritem_id { get; set; }
        public bool is_SentHope { get; set; }
        public Int64 uom_idori { get; set; }
        public string uom_codeori { get; set; }
        public string uom_ratioori { get; set; }
    }

    public class PharmacyAppropriatnessReview
    {
        public string consume_drug { get; set; }
        public string diagnose { get; set; }
        public bool complete_recipe { get; set; }
        public bool correct_drug_name { get; set; }
        public bool correct_drug_dossage { get; set; }
        public bool correct_drug_consume { get; set; }
        public bool correct_drug_freq { get; set; }
        public string duplicate_drug { get; set; }
        public string interaction_drug { get; set; }
        public string interaction_food { get; set; }
        public bool side_effect { get; set; }
        public bool kontraindikasi { get; set; }
        public string allergy { get; set; }
        public string weight_body { get; set; }
        public bool is_pregnant { get; set; }
        public bool is_breastfeed { get; set; }
        //public string bulanhamil { get; set; }
        public bool consume_drug_check { get; set; }
        public bool diagnose_check { get; set; }
        public bool duplicate_drug_check { get; set; }
        public bool interaction_drug_check { get; set; }
        public bool interaction_food_check { get; set; }
        public bool allergy_check { get; set; }
        public bool weight_body_check { get; set; }
        public string side_effect_text { get; set; }
        public string kontraindikasi_text { get; set; }
        public string pregnant_week { get; set; }
        public string consume_drug_text { get; set; }
        public string duplicate_drug_text { get; set; }
        public string interaction_drug_text { get; set; }
        public string allergy_text { get; set; }

    }

    public class PharmacyAdditionalItem
    {
        public long pharmacy_additional_item_id { get; set; }
        public long item_id { get; set; }
        public string item_name { get; set; }
        public string quantity { get; set; }
        public string issued_qty { get; set; }
        public long uom_id { get; set; }
        public string uom_code { get; set; }
        public Int64 hope_aritem_id { get; set; }
        public short item_sequence { get; set; }
        public Guid additional_item_sync_id { get; set; }
    }

    public class PharmacyEditedMapping
    {
        public string pharmacy_type { get; set; }
        public Guid origin_pharmacy_id { get; set; }
        public long edited_pharmacy_id { get; set; }
        public string edit_action { get; set; }
        public string edit_reason { get; set; }
    }
}
