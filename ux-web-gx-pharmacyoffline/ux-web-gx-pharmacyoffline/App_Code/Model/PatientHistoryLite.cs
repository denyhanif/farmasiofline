using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for PatientHistoryLite
/// </summary>
public class PatientHistoryLite
{
    public long OrganizationId { get; set; }
    public string OrganizationCode { get; set; }
    public long PatientId { get; set; }
    public long AdmissionId { get; set; }
    public Guid EncounterId { get; set; }
    public string AdmissionNo { get; set; }
    public string AdmissionTypeName { get; set; }
    public string AdmissionDate { get; set; }
    public string DoctorName { get; set; }
    public string Subjective { get; set; }
    public string Objective { get; set; }
    public string Diagnosis { get; set; }
    public string PlanningProcedure { get; set; }
    public string Prescription { get; set; }
    public string IsLab { get; set; }
    public string IsRad { get; set; }
    public string ConnStatus { get; set; }
    public int CountData { get; set; }
    public string LocalMrNo { get; set; }
    public string PatientName { get; set; }
    public string BirthDate { get; set; }
    public string Gender { get; set; }
    public string Admission { get; set; }
    public int CheckPrint { get; set; }
    public string CreatedDate { get; set; }
    public string ModifiedDate { get; set; }
    public bool IsEditPrescription { get; set; }
    public string pageSOAP { get; set; }
    public long DoctorId { get; set; }
    public bool IsTeleconsultation { get; set; }
    public int CountReferral { get; set; } = 0;
    public int CountRawatInap { get; set; } = 0;
}

public class ResultPatientHistoryLite
{
    private List<PatientHistoryLite> lists = new List<PatientHistoryLite>();
    [JsonProperty("data")]
    public List<PatientHistoryLite> list { get { return lists; } }
}

public class ResultPatientHistoryLiteStatus
{
    private string status;
    [JsonProperty("status")]
    public string statusValue { get { return status; } }
}

public class ResultClassificationType
{
    private List<string> lists = new List<string>();
    [JsonProperty("data")]
    public List<string> list { get { return lists; } }
}

//---------------------------------------------------------

public class PatientHistoryAll : PatientHistoryLite
{
    public string isOngoing { get; set; }
    public string urlDocumentViewer { get; set; }
    public string urlChartTTV { get; set; }
    public string UrlDetailPatient { get; set; }
    public int TotalDocument { get; set; }
}

public class PatientHistoryPagination
{
    public int countPage { get; set; }
    public List<PatientHistoryAll> patientHistory { set; get; }
}

public class ResponsePatientHistoryAll
{
    public string Company { set; get; }
    public string Status { set; get; }
    public int Code { set; get; }
    public string Message { set; get; }
    public PatientHistoryPagination Data { set; get; }
    
    //public List<PatientHistoryAll> Data { set; get; }
}