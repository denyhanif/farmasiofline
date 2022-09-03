using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for ReportStatusDoctor
/// </summary>
public class ReportStatusDoctor
{
    public string LocalMrNo { get; set; }
    public string AdmissionNo { get; set; }
    public DateTime AdmissionDate { get; set; }
    public bool IsPrimaryDoctor { get; set; }
    public string DoctorName { get; set; }
    public string PatientName { get; set; }
    public string DoctorStatus { get; set; }
    public DateTime LastDoctorUpdate { get; set; }
    public string NurseStatus { get; set; }
    public int OrderLaboratory { get; set; }
    public int OrderRadiology { get; set; }
    public int DoctorPrescription { get; set; }
    public string PharmacyTake { get; set; }
    public Nullable<DateTime> TakeDate { get; set; }
    public string PharmacyVerify { get; set; }
    public Nullable<DateTime> VerifyDate { get; set; }
}

public class ResultReportStatusDoctor
{
    private List<ReportStatusDoctor> lists = new List<ReportStatusDoctor>();
    [JsonProperty("data")]
    public List<ReportStatusDoctor> list { get { return lists; } }
}