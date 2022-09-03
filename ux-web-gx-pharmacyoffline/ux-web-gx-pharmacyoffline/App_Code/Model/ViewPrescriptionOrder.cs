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