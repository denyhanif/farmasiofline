using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for FAResume
/// </summary>
public class MedicalResumePediatric : MedicalResume
{
    public List<ResumepediatricdataSOAP> resumepediatricdata { get; set; }
}

public class ResultMedicalResumePediatric
{
    private MedicalResumePediatric lists = new MedicalResumePediatric();
    [JsonProperty("data")]
    public MedicalResumePediatric list { get { return lists; } }
}

public class ResumepediatricdataSOAP
{
    public string pediatric_data_type { get; set; }
    public string value { get; set; }
    public string remarks { get; set; }
}
