using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for FAResume
/// </summary>
public class FAResume
{
    public ResumeHeaderFA resumeheader { get; set; }
    public List<ResumeDataFA> resumedata { get; set; }
    public List<ResumeHealthInfoFA> resumehealthinfo { get; set; }
}

public class ResultFAResume
{
    private FAResume lists = new FAResume();
    [JsonProperty("data")]
    public FAResume list { get { return lists; } }
}

public class ResumeHeaderFA
{
    public Guid EncounterId { get; set; }
    public string AdmissionDate { get; set; }
    public string LocalMrNo { get; set; }
    public string AdmissionType { get; set; }
    public string BirthDate { get; set; }
    public string Age { get; set; }
    public string PatientName { get; set; }
    public string Gender { get; set; }
    public string DoctorName { get; set; }
    public string CreatedDate { get; set; }
    public string ModifiedDate { get; set; }
    public string CreatedBy { get; set; }
    public string ModifiedBy { get; set; }
}

//-----------------------------------------------------------------------

public class ResumeDataFA
{
    public Guid MappingId { get; set; }
    public string MappingName { get; set; }
    public string Value { get; set; }
    public string Remarks { get; set; }
}

//-----------------------------------------------------------------------

public class ResumeHealthInfoFA
{
    public string Value { get; set; }
    public string Remarks { get; set; }
    public string Type { get; set; }
}
