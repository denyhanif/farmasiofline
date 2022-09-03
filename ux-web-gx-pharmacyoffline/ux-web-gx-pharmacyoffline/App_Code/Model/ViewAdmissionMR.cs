using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for ViewAdmissionMR
/// </summary>
public class ViewAdmissionMR
{
    public AdmissionMRHeader mrheader { get; set; }
    public List<AdmissionMRDetail> mrdetail { get; set; }
}

public class ResultViewAdmissionMR
{
    private ViewAdmissionMR lists = new ViewAdmissionMR();
    [JsonProperty("data")]
    public ViewAdmissionMR list { get { return lists; } }
}