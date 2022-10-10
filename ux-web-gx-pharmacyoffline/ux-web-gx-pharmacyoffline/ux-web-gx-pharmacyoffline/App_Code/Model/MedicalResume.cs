using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for MedicalResume
/// </summary>
/// 

public class MedicalResume
{
    public ResumeHeader resumeheader { get; set; }
    public List<ResumeData> resumedata { get; set; }
    public List<ResumeFA> resumefa { get; set; }
    public List<ResumePrescription> resumeprescription { get; set; }

    public List<CompoundHeaderSoap> compound_header { get; set; }
    public List<CompoundDetailSoap> compound_detail { get; set; }

    public List<PatientHistoryCPOE> resumecpoe { get; set; }
    public List<ResumeProcedureDiagnostic> resumeprocedurediagnostic { get; set; }
}

public class ResultMedicalResume
{
    private MedicalResume lists = new MedicalResume();
    [JsonProperty("data")]
    public MedicalResume list { get { return lists; } }
}

public class ResumeHeader
{
    public Guid EncounterId { get; set; }
    public DateTime AdmissionDate { get; set; }
    public string LocalMrNo { get; set; }
    public string AdmissionType { get; set; }
    public string BirthDate { get; set; }
    public string Age { get; set; }
    public string PatientName { get; set; }
    public string Gender { get; set; }
    public string DoctorName { get; set; }
    public string CreatedDate { get; set; }
    public string ModifiedDate { get; set; }
    public bool isCOVID { get; set; }
}

public class ResumeData
{
    public Guid MappingId { get; set; }
    public string MappingName { get; set; }
    public string Value { get; set; }
    public string Remarks { get; set; }
}

public class ResumeFA
{
    public string Value { get; set; }
    public string Remarks { get; set; }
    public string Type { get; set; }
}


public class ResumePrescription
{
    public Guid prescription_id { get; set; }
    public string salesItemName { get; set; }
    public Decimal quantity { get; set; }
    public string uom { get; set; }
    public string frequency { get; set; }
    public Decimal dose { get; set; }
    public string doseText { get; set; }
    public long dose_uom_id { get; set; }
    public string dose_uom { get; set; }
    public string instruction { get; set; }
    public string route { get; set; }
    public Boolean isRoutine { get; set; }
    public int iteration { get; set; }
    public Boolean isConsumables { get; set; }
    public Guid compound_id { get; set; }
    public string compound_name { get; set; }
    public Guid origin_prescription_id { get; set; }
    public string routine { get; set; }
    public int IsAdditional { get; set; }
    public bool IsDoseText { get; set; }
}

public class ResumeDrugs
{
    public string salesItemName { get; set; }
    public string Remarks { get; set; }
}

public class ResumeProcedureDiagnostic
{
    public string SalesItemName { get; set; }
    public string SalesItemType { get; set; }
    public bool IsFutureOrder { get; set; }
    public DateTime FutureOrderDate { get; set; }
}

public class ResultResumeDrugs
{
    private ResumeDrugs lists = new ResumeDrugs();
    [JsonProperty("data")]
    public ResumeDrugs list { get { return lists; } }
}

