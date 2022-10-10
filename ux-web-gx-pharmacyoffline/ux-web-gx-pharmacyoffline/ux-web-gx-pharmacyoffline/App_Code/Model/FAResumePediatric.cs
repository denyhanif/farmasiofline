using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for FAResume
/// </summary>
public class FAResumePediatric : FAResume
{
    public List<ResumepediatricdataFA> resumepediatricdata { get; set; }
}

public class ResultFAResumePediatric
{
    private FAResumePediatric lists = new FAResumePediatric();
    [JsonProperty("data")]
    public FAResumePediatric list { get { return lists; } }
}

public class ResumepediatricdataFA
{
    public string pediatric_data_type { get; set; }
    public string value { get; set; }
    public string remarks { get; set; }
}
