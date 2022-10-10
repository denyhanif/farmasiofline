using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for WorklistPharmacy
/// </summary>
public class WorklistPharmacy
{
    public Guid EncounterId { get; set; }
    public Int64 OrganizationId { get; set; }
    public Int64 AdmissionId { get; set; }
    public string AdmissionNo { get; set; }
    public DateTime AdmissionDate { get; set; }
    public string LocalMrNo { get; set; }
    public Nullable<Int64> PatientId { get; set; }
    public string PatientName { get; set; }
    public string Gender { get; set; }
    public DateTime BirthDate { get; set; }
    public string Age { get; set; }
    public string DoctorName { get; set; }
    public string PayerName { get; set; }
    public Boolean IsVIP { get; set; }
    public string CentralizedMrNo { get; set; }
    public string OrganizationCode { get; set; }
}

public class ResultWorklistPharmacy
{
    private List<WorklistPharmacy> lists = new List<WorklistPharmacy>();
    [JsonProperty("data")]
    public List<WorklistPharmacy> list { get { return lists; } }
}