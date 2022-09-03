using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for PatientDashboard
/// </summary>


public class PatientDashboard
{
    public ViewPatientHeader patientheader { get; set; }
    public List<ViewHealthInfo> patienthealthinfo { get; set; }
    public List<ViewNotification> patientnotification { get; set; }
    public List<ViewProcedure> patientprocedure { get; set; }
    public List<ViewProcedureResult> proceduresults { get; set; }
}

public class ResultPatientDashboard
{
    private List<PatientDashboard> lists = new List<PatientDashboard>();
    [JsonProperty("data")]
    public List<PatientDashboard> list { get { return lists; } }
}

public class ViewPatientHeader
{
    public Guid EncounterId { get; set; }
    public string PatientName { get; set; }
    public int Gender { get; set; }
    public string MrNo { get; set; }
    public string DoctorName { get; set; }
    public string AdmissionNo { get; set; }
    public DateTime BirthDate { get; set; }
    public string Religion { get; set; }
    public Int16 AdmissionTypeId { get; set; }
    public Int64 PayerId { get; set; }
    public string PayerName { get; set; }
    public string Formularium { get; set; }
}

public class ViewHealthInfo
{
    public int other_health_info_type { get; set; }
    public string other_health_info_value { get; set; }
    public string other_health_info_remarks { get; set; }
}

public class ViewNotification
{
    public string notification { get; set; }
    public string doctor_name { get; set; }
    public string created_date { get; set; }
    public long doctor_id { get; set; }
}

public class ViewProcedure
{
    public short procedure_type { get; set; }
    public string procedure_remarks { get; set; }
    public DateTime procedure_date { get; set; }
    public string doctor_name { get; set; }
}

public class ViewProcedureResult
{
    public string admission { get; set; }
    public string doctor_name { get; set; }
    public string planning_remarks { get; set; }
}

public class AdmissionHistory //Data Collection
{
    public Int64 OrganizationId { get; set; }
    public string OrgCd { get; set; }
    public Int64 AdmissionId { get; set; }
    public string AdmissionNo { get; set; }
    public DateTime AdmissionDate { get; set; }
    public int AdmissionMonth { get; set; }
    public int AdmissionWeek { get; set; }
    public string DoctorName { get; set; }
    public string Specialty { get; set; }
    public int isMyself { get; set; }
    public string LabSalesOrderNo { get; set; }
    public string RadSalesOrderNo { get; set; }
    public string Type { get; set; }
    public string Diagnosis { get; set; }
    public string connStatus { get; set; }
    public string encounterId { get; set; }
}

public class ResultAdmissionHistory
{
    private List<AdmissionHistory> lists = new List<AdmissionHistory>();
    [JsonProperty("data")]
    public List<AdmissionHistory> list { get { return lists; } }
}