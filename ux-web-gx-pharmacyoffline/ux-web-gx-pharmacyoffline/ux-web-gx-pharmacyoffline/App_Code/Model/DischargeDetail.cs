using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for DischargeDetail
/// </summary>
public class DischargeDetailx
{
    public Guid WorklistId { get; set; }
    public DateTime WorklistDate { get; set; }
    public Guid ProcessId { get; set; }
    public string AdmissionNo { get; set; }
    public long AdmissionId { get; set; }
    public long PatientId { get; set; }
    public string PatientName { get; set; }
    public long PatientTypeId { get; set; }
    public long ClassId { get; set; }
    public DateTime BirthDate { get; set; }
    public string Age { get; set; }
    public string Gender { get; set; }
    public long PayerId { get; set; }
    public string PayerName { get; set; }
    public string LocalMrNo { get; set; }
    public string RoomNo { get; set; }
    public string MobileNo { get; set; }
    public long DoctorId { get; set; }
    public string DoctorName { get; set; }
    public bool IsPrimary { get; set; }
    public string WaitStatus { get; set; }
    public string Remarks { get; set; }
    public bool IsDoctorVisit { get; set; }
    public bool IsPrescription { get; set; }
    public bool IsRetur { get; set; }
    public string VisitValue { get; set; }
    public string PrescriptionValue { get; set; }
    public string ReturValue { get; set; }
    public string AdditionalNotes { get; set; }
    public string CreatedBy { get; set; }
    public DateTime CreatedDate { get; set; }
    public string ModifiedBy { get; set; }
    public DateTime ModifiedDate { get; set; }
    public string SubmitBy { get; set; }
    public DateTime SubmitDate { get; set; }
    public string lmaStatus { get; set; }
    public string resumeStatus { get; set; }
}
public class ListDischargeDetailx
{
    private List<DischargeDetailx> lists = new List<DischargeDetailx>();
    [JsonProperty("data")]
    public List<DischargeDetailx> list { get { return lists; } }
}