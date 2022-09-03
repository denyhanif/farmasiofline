using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for ViewWorklistViewerLabRad
/// </summary>
public class ViewWorklistViewerLabRad
{
    public ViewWorklistViewerLabRad(){}

    public Guid EncounterId { get; set; }
    public long AdmissionId { get; set; }
    public long OrganizationId { get; set; }
    public string DoctorName { get; set; }
    public long PatientId { get; set; }
    public string AdmissionNo { get; set; }
    public string AdmissionDate { get; set; }
    public bool IsLab { get; set; }
    public bool IsRad { get; set; }
    public bool IsProcedure { get; set; }
    public bool IsPrescription {get;set;}
    public string pageSOAP { get; set; }
    public int CountReferral { get; set; } = 0;
    public int CountRawatInap { get; set; } = 0;
}

public class ResultViewWorklistViewerLabRad
{
    private List<ViewWorklistViewerLabRad> lists = new List<ViewWorklistViewerLabRad>();
    [JsonProperty("data")]
    public List<ViewWorklistViewerLabRad> list { get { return lists; } }
}

public class DiagProcItem
{
    public string ProcedureItemName { get; set; }
    public long ProcedureItemTypeId { get; set; }
}

public class ResultDiagProcItem
{
    private List<DiagProcItem> lists = new List<DiagProcItem>();
    [JsonProperty("data")]
    public List<DiagProcItem> list { get { return lists; } }
}