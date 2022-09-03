using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for AdmisionFA
/// </summary>
public class AdmisionFA
{
    public Guid EncounterId { get; set; }
    public long OrganizationId { get; set; }
    public long AdmissionId { get; set; }
    public long PatientId { get; set; }
    public string PatientName { get; set; }
    public string BirthDate { get; set; }
    public string Age { get; set; }
    public string ReligionName { get; set; }
    public string AdmissionDate { get; set; }
    public string AdmissionNo { get; set; }
    public string DoctorName { get; set; }
    public long DoctorId { get; set; }
    public string PayerName { get; set; }
    public string pageFA { get; set; }
}

public class ResultAdmissionFA
{
    private List<AdmisionFA> lists = new List<AdmisionFA>();
    [JsonProperty("data")]
    public List<AdmisionFA> list { get { return lists; } }
    public string Company { set; get; }
    public string Status { set; get; }
    public int Code { set; get; }
    public string Message { set; get; }
}