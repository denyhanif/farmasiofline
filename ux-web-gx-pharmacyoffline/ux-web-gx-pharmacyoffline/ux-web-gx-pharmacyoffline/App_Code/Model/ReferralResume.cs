using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for MedicalResume
/// </summary>
/// 

public class ReferralResume
{
    public ResumeHeader resumeheader { get; set; }
    public List<ResumeDetail> resumedetail { get; set; }
}

public class ResultReferralResume
{
    private ReferralResume lists = new ReferralResume();
    [JsonProperty("data")]
    public ReferralResume list { get { return lists; } }
}


public class ResumeDetail
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
    public string LocalMrNo { get; set; }
    public string PatientName { get; set; }
    public string BirthDate { get; set; }
    public string Gender { get; set; }
    public string Admission { get; set; }
    public string Subjective { get; set; }
    public string Objective { get; set; }
    public string Diagnosis { get; set; }
    public string PlanningProcedure { get; set; }
    public string Prescription { get; set; }
    public bool IsEditPrescription { get; set; }
    public string IsLab { get; set; }
    public string IsRad { get; set; }
    public string ConnStatus { get; set; }
    public int CountData { get; set; }
    public string CreatedDate { get; set; }
    public string ModifiedDate { get; set; }
    public Guid PageSOAP { get; set; }
    public long DoctorId { get; set; }
    public bool IsTeleconsultation { get; set; }
    public string OrderLab { get; set; }
    public string OrderRad { get; set; }
    public string DoctorReferral { get; set; }
    public string referral_type { get; set; }
    public string referral_remark { get; set; }
    public long DoctorReferralId { get; set; }
    public string RefType { get; set; }
    public long DoctorRef { get; set; }
}



public class PatientReferral
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
    public Guid PageSOAP { get; set; }
    public bool IsTeleconsultation { get; set; }
    public string OrderLab { get; set; }
    public string OrderRad { get; set; }
    public string DoctorReferral { get; set; }
    public string referral_type { get; set; }
    public string referral_remark { get; set; }
    public int IsSelf { get; set; }
    public string DoctorNameOri { get; set; } = "";
}

public class ResultPatientReferral
{
    private List<PatientReferral> lists = new List<PatientReferral>();
    [JsonProperty("data")]
    public List<PatientReferral> list { get { return lists; } }
}

public class PatientReferralBalasan
{
    public List<PatientReferral> referrals { get; set; }
    public List<PatientReferral> balasan { get; set; }
}

public class ResultPatientReferralBalasan
{
    private PatientReferralBalasan lists = new PatientReferralBalasan();
    [JsonProperty("data")]
    public PatientReferralBalasan list { get { return lists; } }
}


