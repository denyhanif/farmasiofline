using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for FAResume
/// </summary>
public class SingleQueue
{
    public List<PatientHeaderSQ> patientHeader { get; set; }
    public List<PrescriptionOrderSQ> prescriptionOrder { get; set; }
    public List<CompoundHeaderSQ> compoundHeader { get; set; }
    public List<CompoundDetailSQ> compoundDetail { get; set; }
    public List<CPOEORderSQ> cpoeOrder { get; set; }
    public List<SQSOAPProcedureDiagnosis> ProcedureDiagOrder { get; set; }
}

public class ResultSingleQueue
{
    private SingleQueue lists = new SingleQueue();
    [JsonProperty("data")]
    public SingleQueue list { get { return lists; } }
}

public class PatientHeaderSQ
{
    public Int64 organizationId { get; set; }
    public Int64 patientId { get; set; }
    public Int64 admissionId { get; set; }
    public Int64 doctorId { get; set; }
    public Guid encounterId { get; set; }
    public string patientName { get; set; }
    public string age { get; set; }
    public int sexId { get; set; }
    public string mrNumber { get; set; }
    public string doctorName { get; set; }
    public string payerName { get; set; }
    public DateTime dob { get; set; }
    public DateTime admissionDate { get; set; }
    public bool isLab { get; set; }
    public bool isRad { get; set; }
    public bool isPrescription { get; set; }
    public Guid PatientVisitId { get; set; }
    public string AdmissionNo { get; set; }
    public bool IsTransaction { get; set; }
    public string urlDetailTrx { get; set; }
    public string queueLineHospitalId { get; set; }
    public Nullable<Guid> visitTypeHospitalId { get; set; }
    public bool IsTakeAll { get; set; }
    public string PrescriptionNotes { get; set; }
}

//-----------------------------------------------------------------------

public class PrescriptionOrderSQ
{
    public Int64 organizationId { get; set; }
    public Int64 patientId { get; set; }
    public Int64 admissionId { get; set; }
    public Int64 doctorId { get; set; }
    public Guid encounterId { get; set; }
    public long SalesItemId { get; set; }
    public string salesItemName { get; set; }
    public string frequency { get; set; }
    public string quantity { get; set; }
    public long UomId { get; set; }
    public string uom { get; set; }
    public string dose { get; set; }
    public string doseUom { get; set; }
    public string route { get; set; }
    public string doseText { get; set; }
    public string instruction { get; set; }
    public int iter { get; set; }
    public bool isRoutine { get; set; }
    public bool isConsumables { get; set; }
    public bool isDoseText { get; set; }
    public string urlDetailTrx { get; set; }
    public string queueLineHospitalId { get; set; }
    public Nullable<Guid> visitTypeHospitalId { get; set; }
    public string TotalQuantity { get; set; }
}

//-----------------------------------------------------------------------

public class CompoundHeaderSQ
{
    public Int64 organizationId { get; set; }
    public Int64 patientId { get; set; }
    public Int64 admissionId { get; set; }
    public Int64 doctorId { get; set; }
    public Guid encounterId { get; set; }
    public Guid compoundId { get; set; }
    public string compoundName { get; set; }
    public string quantity { get; set; }
    public string uom { get; set; }
    public string dose { get; set; }
    public string doseUom { get; set; }
    public string frequency { get; set; }
    public string route { get; set; }
    public string instruction { get; set; }
    public string note { get; set; }
    public int iter { get; set; }
    public string doseText { get; set; }
    public bool isDoseText { get; set; }
    public string urlDetailTrx { get; set; }
    public string queueLineHospitalId { get; set; }
    public Nullable<Guid> visitTypeHospitalId { get; set; }
}

//-----------------------------------------------------------------------

public class CompoundDetailSQ
{
    public Guid compoundDetailId { get; set; }
    public Guid compoundId { get; set; }
    public string salesItemName { get; set; }
    public string quantity { get; set; }
    public string uom { get; set; }
    public string dose { get; set; }
    public string doseUom { get; set; }
    public string doseText { get; set; }
    public bool isDoseText { get; set; }
    public string urlDetailTrx { get; set; }
    public string queueLineHospitalId { get; set; }
    public Nullable<Guid> visitTypeHospitalId { get; set; }
    public string TotalQuantity { get; set; }
}

//-----------------------------------------------------------------------

public class CPOEORderSQ
{
    public Int64 organizationId { get; set; }
    public Int64 patientId { get; set; }
    public Int64 admissionId { get; set; }
    public Int64 doctorId { get; set; }
    public Guid encounterId { get; set; }
    public long ItemId { get; set; }
    public string itemName { get; set; }
    public string cpoeType { get; set; }
    public string urlDetailTrx { get; set; }
    public bool IsSendHOPE { get; set; }
    public string queueLineHospitalId { get; set; }
    public Nullable<Guid> visitTypeHospitalId { get; set; }
}

//-----------------------------------------------------------------------


public class SQSOAPProcedureDiagnosis
{
    public long OrganizationId { get; set; }
    public long PatientId { get; set; }
    public long AdmissionId { get; set; }
    public long DoctorId { get; set; }
    public Guid EncounterId { get; set; }
    public long ProcedureItemId { get; set; }
    public string ProcedureItemName { get; set; }
    public long ProcedureItemTypeId { get; set; }
    public Guid ProcedureId { get; set; }
}