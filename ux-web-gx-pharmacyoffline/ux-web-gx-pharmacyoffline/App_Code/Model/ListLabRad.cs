using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for ListLabRad
/// </summary>
public class ListLabRad
{
    public ListLabRad(){}

    public string item_type { get; set; }
    public string item_name { get; set; }
    public bool is_cito { get; set; }
    public string ClinicalDiagnosis { get; set; }
    public string DoctorName { get; set; }
    public string Remarks { get; set; }
    public string OrderDate { get; set; }
    public short fasting_flag { get; set; }
    public bool isCOVID { get; set; }
    public bool is_future_order { get; set; }
    public DateTime future_order_date { get; set; }
}

public class ResultListLabRad
{
    private List<ListLabRad> lists = new List<ListLabRad>();
    [JsonProperty("data")]
    public List<ListLabRad> list { get { return lists; } }
}