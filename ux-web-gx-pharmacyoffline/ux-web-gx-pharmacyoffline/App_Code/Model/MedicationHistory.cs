using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for MedicationHistory
/// </summary>
public class MedicationHistory
{
    public DateTime AdmissionDate { get; set; }
    public DateTime OrderDate { get; set; }
    public string AdmissionNo { get; set; }
    public string MedicationDoctor { get; set; }
    public string PayerName { get; set; }
    //public long PresId { get; set; }
    public long ItemId { get; set; }
    public string ItemName { get; set; }
    public string Quantity { get; set; }
    public string Uom { get; set; }
    public string Frequency { get; set; }
    public string Dose { get; set; }
    public string DoseText { get; set; }
    public long dose_uom_id { get; set; }
    public string dose_uom { get; set; }
    public string Instruction { get; set; }
    public string Route { get; set; }
    public string Iter { get; set; }
    public string IsRoutine { get; set; }
    public string IsCompound { get; set; }
    public string CompoundName { get; set; }
    //public long OriginPresId { get; set; }
    public string IsConsumables { get; set; }
    public string TotalQuantity { get; set; }
    public string IssuedQuantity { get; set; }
    public string ReturnQty { get; set; }
    public string OutstandingQty { get; set; }
    public long OrganizationId { get; set; }
    public string OrgCode { get; set; }
    public long DoctorId { get; set; }
    public string CompoundId { get; set; }
    public bool IsCompoundHeader { get; set; }
    public bool IsVerified { get; set; }
}

public class ResultMedicationHistory
{
    private List<MedicationHistory> lists = new List<MedicationHistory>();
    [JsonProperty("data")]
    public List<MedicationHistory> list { get { return lists; } }
}