using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;

/// <summary>
/// Summary description for Worklist
/// </summary>

    public class ViewWorklistDischarge
    {
    public Guid WorklistId { get; set; }
    public Guid ProcessId { get; set; }
    public long OrganizationId { get; set; }
    public DateTime WorklistDate { get; set; }
    public long AdmissionId { get; set; }
    public long PatientId { get; set; }
    public long WardId { get; set; }
    public string PatientName { get; set; }
    public DateTime BirthDate { get; set; }
    public string Age { get; set; }
    public string Gender { get; set; }
    public string PayerName { get; set; }
    public string LocalMrNo { get; set; }
    public string RoomNo { get; set; }
    public bool IsNew { get; set; }
    public string IsLab { get; set; }
    public string IsRad { get; set; }
    public string IsLate { get; set; }
    public long doctorId { get; set; }
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
    public string LMAStatus { get; set; }
    public string UploadLMA { get; set; }
    public string ResumeStatus { get; set; }
    public string UploadResume { get; set; }
    public string AdditionalNotes { get; set; }
    public string ModifiedDate { get; set; }
    public bool flagSubmit { get; set; }
    public string showsubmit { get; set; }
    public long PayerGroupId { get; set; }
}
    public class ListViewWorklistDischarge
    {
        private List<ViewWorklistDischarge> lists = new List<ViewWorklistDischarge>();
        [JsonProperty("data")]
        public List<ViewWorklistDischarge> list { get { return lists; } }
    }


    public class CountPatient
    {
        public int countplan { get; set; }
        public int countplanlate { get; set; }
        public int countprocess { get; set; }
        public int countprocesslate { get; set; }
        public int countdone { get; set; }
    }

    public class ResultCountPatient
    {
        //private CountPatient lists = new CountPatient();
        [JsonProperty("data")]
        public CountPatient list { get; set; }
    }

    public class ViewDischargeRequest
    {
        public Guid ProcessId { get; set; }
        public Guid WorklistId { get; set; }
        public DateTime WorklistDate { get; set; }
        public long AdmissionId { get; set; }
        public string AdmissionNo { get; set; }
        public DateTime SubmitDate { get; set; }
        public long PatientId { get; set; }
        public long WardId { get; set; }
        public string PatientName { get; set; }
        public string PayerName { get; set; }
        public string EmailDate { get; set; }
        public string ConfirmDate { get; set; }
        public long DoctorId { get; set; }
        public string DoctorName { get; set; }
        public string AdditionalNotes { get; set; }
        public bool IsPrimary { get; set; }
        public bool IsPrescription { get; set; }
        public bool IsRetur { get; set; }
        public bool IsNeedPrescription { get; set; }
        public bool IsNeedInsurance { get; set; }
        public string InvoiceDate { get; set; }
        public string FlagDischarged { get; set; }
        public Nullable<bool> FUPatient { get; set; }
        public Nullable<bool> OPDControl { get; set; }
        public Nullable<long> ArInvoiceId { get; set; }
        public string SubDateBed { get; set; }
        public string SubDateService { get; set; }
        public string SubDateItem { get; set; }
        public string FinalDate { get; set; }
        public string Duration { get; set; }
        public string ModifiedDate { get; set; }
        public string isShowDate { get; set; }
        public string islate { get; set; }
        public string localMrNo { get; set; }
        public string birthDate { get; set; }
        public string roomNo { get; set; }
        public string lateservice { get; set; }
        public string lateitem { get; set; }
        public string lateemail { get; set; }
        public string lateconfirm { get; set; }
        public string lateinvoice { get; set; }
        public string latetotal { get; set; }
    }

    public class ResultListDischargeProcess
    {
        //private List<DischargeProcess> lists = new List<DischargeProcess>();
        [JsonProperty("data")]
        public ListDischargeRequest list { get; set; }
    }


    public class ListDischargeRequest
    {
        public List<DischargeRequest> dischargerequests { get; set; }
        public List<SubDischarge> subdischarges { get; set; }
    }
    public class DischargeRequest
    {
        public Guid ProcessId { get; set; }
        public Guid WorklistId { get; set; }
        public DateTime WorklistDate { get; set; }
        public long AdmissionId { get; set; }
        public string AdmissionNo { get; set; }
        public DateTime SubmitDate { get; set; }
        public long PatientId { get; set; }
        public long WardId { get; set; }
        public string PatientName { get; set; }
        public string LocalMrNo { get; set; }
        public string CentralMrNo { get; set; }
        public DateTime BirthDate { get; set; }
        public string Age { get; set; }
        public string RoomNo { get; set; }
        public string islate { get; set; }
        public string PayerName { get; set; }
        public string EmailDate { get; set; }
        public string ConfirmDate { get; set; }
        public string FlagDischarged { get; set; }
        public long DoctorId { get; set; }
        public string DoctorName { get; set; }
        public string AdditionalNotes { get; set; }
        public bool IsPrimary { get; set; }
        public bool IsPrescription { get; set; }
        public bool IsRetur { get; set; }
        public Nullable<long> ArInvoiceId { get; set; }
        public string InvoiceDate { get; set; }
        public Nullable<bool> FUPatient { get; set; }
        public Nullable<bool> OPDControl { get; set; }
        public string ModifiedDate { get; set; }
        public string isShowDate { get; set; }
    }

    public class SubDischarge
    {
        public Guid WorklistId { get; set; }
        public long AdmissionId { get; set; }
        public string SubDate { get; set; }
        public long SubDischargeTypeId { get; set; }
        public string FinalDate { get; set; }
    }

    public class DischargeDetail
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
    }
    public class ListDischargeDetail
    {
        private List<DischargeDetail> lists = new List<DischargeDetail>();
        [JsonProperty("data")]
        public List<DischargeDetail> list { get { return lists; } }
    }

    public class PatientHeaderIpd
    {
        public Guid WorklistId { get; set; }
        public string PatientName { get; set; }
        public string MrNo { get; set; }
        public string DoctorName { get; set; }
        public string AdmissionNo { get; set; }
        public DateTime BirthDate { get; set; }
        public string Age { get; set; }
        public string Gender { get; set; }
        public string PayerName { get; set; }
        public string Religion { get; set; }
    }

    public class ListPatientHeader
    {
        private List<PatientHeaderIpd> lists = new List<PatientHeaderIpd>();
        [JsonProperty("data")]
        public List<PatientHeaderIpd> list { get { return lists; } }
    }

//[Serializable]
    //public class LaboratoryResult
    //{
    //    public Int64? organizationId { get; set; }
    //    public String orgCd { get; set; }
    //    public Int64? admissionId { get; set; }
    //    public String admissionNo { get; set; }
    //    public DateTime? admissionDate { get; set; }
    //    public String cliniciaN_NM { get; set; }
    //    public String ono { get; set; }
    //    public String disP_SEQ { get; set; }
    //    public String seqno { get; set; }
    //    public String tesT_CD { get; set; }
    //    public String tesT_NM { get; set; }
    //    public String datA_TYP { get; set; }
    //    public String resulT_VALUE { get; set; }
    //    public String resulT_FT { get; set; }
    //    public String unit { get; set; }
    //    public String flag { get; set; }
    //    public String reF_RANGE { get; set; }
    //    public String status { get; set; }
    //    public String tesT_COMMENT { get; set; }
    //    public String tesT_GROUP { get; set; }
    //    public Int64 IsHeader { get; set; }
    //    public string ConnStatus { get; set; }
    //}


    //public class ResultLaboratoryResult
    //{
    //    private List<LaboratoryResult> lists = new List<LaboratoryResult>();
    //    [JsonProperty("data")]
    //    public List<LaboratoryResult> list { get { return lists; } }
    //}

    //public class gridLaboratory
    //{
    //    public String tesT_GROUP { get; set; }
    //    public String tesT_NM { get; set; }
    //    public String resulT_VALUE { get; set; }
    //    public String unit { get; set; }
    //    public String reF_RANGE { get; set; }
    //    public String ono { get; set; }
    //    public String dis_sq { get; set; }
    //    public Int64 IsHeader { get; set; }
    //}
