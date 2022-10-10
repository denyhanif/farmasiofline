using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for RadiologyResult
/// </summary>

public class DocumentResult
{
    public long ScanId { get; set; }
    public DateTime AdmissionDate { get; set; }
    public string AdmissionNo { get; set; }
    public string UploaderName { get; set; }
    public string Path { get; set; }
    public string DocumentName { get; set; }
    public string image_type_value { get; set; }
    public string image_remark { get; set; }
}

public class ResultDocumentResult
{
    private List<DocumentResult> lists = new List<DocumentResult>();
    [JsonProperty("data")]
    public List<DocumentResult> list { get { return lists; } }
}



