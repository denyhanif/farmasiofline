using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for ViewPrescriptionOrder
/// </summary>
public class ViewPrescriptionOrder
{
    public List<PrescriptionOrderDrug> PrescriptionOrderDrug { get; set; }
    public List<PrescriptionOrderCompoundHeader> PrescriptionOrderCompoundHeader { get; set; }
    public List<PrescriptionOrderCompoundDetail> PrescriptionOrderCompoundDetail { get; set; }
}

public class PrescriptionOrderDrug
{
    public string ItemName { get; set; }
    public string Quantity { get; set; }
    public string Uom { get; set; }
    public string Frequency { get; set; }
    public string Dose { get; set; }
    public string DoseUom { get; set; }
    public string Instruction { get; set; }
    public string Route { get; set; }
    public int Iter { get; set; }
    public string Routine { get; set; }
    public bool IsConsumables { get; set; }
    public bool IsDoseText { get; set; }
    public string dose_text { get; set; }
}

public class PrescriptionOrderCompoundHeader
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
    public string dose_text { get; set; }
    public bool IsDoseText { get; set; }
}

public class PrescriptionOrderCompoundDetail
{
    public Guid prescription_compound_detail_id { get; set; }
    public Guid prescription_compound_header_id { get; set; }
    public long item_id { get; set; }
    public string item_name { get; set; }
    public string quantity { get; set; }
    public long uom_id { get; set; }
    public string uom_code { get; set; }
    public short item_sequence { get; set; }
    public bool is_additional { get; set; }
    public long organization_id { get; set; }
    public string dose { get; set; }
    public long dose_uom_id { get; set; }
    public string dose_uom_code { get; set; }
    public string dose_text { get; set; }
    public bool IsDoseText { get; set; }
}



public class ResultViewPrescriptionOrder
{
    private ViewPrescriptionOrder lists = new ViewPrescriptionOrder();
    [JsonProperty("data")]
    public ViewPrescriptionOrder list { get { return lists; } }
}

public class ResultPrescriptionEdited
{
    private PrescriptionEditedData lists = new PrescriptionEditedData();
    [JsonProperty("data")]
    public PrescriptionEditedData list { get { return lists; } }
    
   public string Company { set; get; }
   public string Status { set; get; }
   public int Code { set; get; }
   public string Message { set; get; }
    
}
public class PrescriptionEditedData
{
    public HeaderPrescriptionHistory header;
    public List<DoctorPrescription> doctor_prescriptions;
    public List<PharmacistRelease> pharmacist_releases;
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

public class DoctorPrescription
{
    public string item_name { get; set; }
    public string quantity { get; set; }
    public string uom { get; set; }
    public string frequency { get; set; }
    public string dose { get; set; }
    public string dose_uom { get; set; }
    public string administration_route { get; set; }
    public Nullable<DateTime> created_date { get; set; }
    public string instruction { get; set; }
    public string edit_action { get; set; }
    public string edit_reason { get; set; }
    public bool? is_compound { get; set; }
    public bool? is_additional { get; set; }
    public int item_sequence { get; set; }
    public Nullable<Guid> doctor_prescription_id { get; set; }
    public Nullable<long> phar_prescription_id { get; set; }


}

public class PharmacistRelease
{
    public string item_name { get; set; }
    public string quantity { get; set; }
    public string uom { get; set; }
    public string frequency { get; set; }
    public string dose { get; set; }
    public string dose_uom { get; set; }
    public string administration_route { get; set; }
    public Nullable<DateTime> created_date { get; set; }
    public string instruction { get; set; }
    public string edit_action { get; set; }
    public string edit_reason { get; set; }
    public bool? is_compound { get; set; }
    public bool? is_additional { get; set; }
    public int item_sequence { get; set; }
    public string doctor_prescription_id { get; set; }
    public Nullable<long> phar_prescription_id { get; set; }


}

public class ViewDoctorPharmachyEdited
{
    public string d_item_name { get; set; }
    public string d_quantity { get; set; }
    public string d_uom { get; set; }
    public string d_frequency { get; set; }
    public string d_dose { get; set; }
    public string d_dose_uom { get; set; }
    public string d_administration_route { get; set; }
    public Nullable<DateTime> d_created_date { get; set; }
    public string d_instruction { get; set; }
    public string d_edit_action { get; set; }
    public string d_edit_reason { get; set; }
    public bool? d_is_compound { get; set; }
    public bool? d_is_additional { get; set; }
    public int? d_item_sequence { get; set; }
    public Nullable<Guid> d_doctor_prescription_id { get; set; }
    public string d_phar_prescription_id { get; set; }

    public string p_item_name { get; set; }
    public string p_quantity { get; set; }
    public string p_uom { get; set; }
    public string p_frequency { get; set; }
    public string p_dose { get; set; }
    public string p_dose_uom { get; set; }
    public string p_administration_route { get; set; }
    public Nullable<DateTime> p_created_date { get; set; }
    public string p_instruction { get; set; }
    public string p_edit_action { get; set; }
    public string p_edit_reason { get; set; }
    public bool? p_is_compound { get; set; }
    public bool? p_is_additional { get; set; }
    public int p_item_sequence { get; set; }
    public string p_doctor_prescription_id { get; set; }
    public string p_phar_prescription_id { get; set; }
}