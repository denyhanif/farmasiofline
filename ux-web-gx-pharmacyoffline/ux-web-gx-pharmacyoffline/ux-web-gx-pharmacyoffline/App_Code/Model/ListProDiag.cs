using System;
using System.Collections.Generic;
using Newtonsoft.Json;

/// <summary>
/// Summary description for ListProDiag
/// </summary>

public class ListProDiag
{
    public ListProDiag(){}

    public string SalesItemType { get; set; }
    public string OrderType { get; set; }
    public string SalesItemName { get; set; }
    public string ClinicalDiagnosis { get; set; }
    public string DoctorName { get; set; }
    public bool IsFutureOrder { get; set; }
    public bool IsCOVID { get; set; }
    public DateTime OrderDate { get; set; }
}

public class ResultListProDiag
{
    private List<ListProDiag> lists = new List<ListProDiag>();
    [JsonProperty("data")]
    public List<ListProDiag> list { get { return lists; } }
}