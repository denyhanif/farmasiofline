using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for FAResume
/// </summary>
public class MedicalResumeObgyn : MedicalResume
{
    public List<ResumepregnancydataSOAP> resumepregnancydata { get; set; }
}

public class ResultMedicalResumeObgyn
{
    private MedicalResumeObgyn lists = new MedicalResumeObgyn();
    [JsonProperty("data")]
    public MedicalResumeObgyn list { get { return lists; } }
}

public class ResumepregnancydataSOAP
{
    public string pregnancy_data_type { get; set; }
    public string value { get; set; }
    public string remarks { get; set; }
    public string status { get; set; }
}
