using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for FAResume
/// </summary>
public class FAResumeObgyn : FAResume
{
    public List<ResumepregnancydataFA> resumepregnancydata { get; set; }
    public List<ResumepregnancyhistoryFA> resumepregnancyhistory { get; set; }
}

public class ResultFAResumeObgyn
{
    private FAResumeObgyn lists = new FAResumeObgyn();
    [JsonProperty("data")]
    public FAResumeObgyn list { get { return lists; } }
}

public class ResumepregnancydataFA
{
    public string pregnancy_data_type { get; set; }
    public string value { get; set; }
    public string remarks { get; set; }
    public string status { get; set; }
}

public class ResumepregnancyhistoryFA
{
    public short pregnancy_sequence { get; set; }
    public string child_age { get; set; }
    public string age_type { get; set; }
    public short child_sex { get; set; }
    public string bbl { get; set; }
    public string labor_type { get; set; }
    public string labor_helper { get; set; }
    public string labor_place { get; set; }
    public short labor_doa { get; set; }
}
