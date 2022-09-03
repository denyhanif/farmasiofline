using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for FAResume
/// </summary>
public class MedicalResumeWDoc : MedicalResume
{
    public List<ResumeDocument> resumedocument { get; set; }
}

public class ResultMedicalResumeWDoc
{
    private MedicalResumeWDoc lists = new MedicalResumeWDoc();
    [JsonProperty("data")]
    public MedicalResumeWDoc list { get { return lists; } }
}

public class ResumeDocument
{
    public string image_type_value { get; set; }
    public string image_remark { get; set; }
}
