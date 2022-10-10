using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for OrderSet
/// </summary>
public class OrderSet
{

}

public class Dose
{
    public Int64? doseUomId { get; set; }
    public String code { get; set; }
    public String name { get; set; }
}

public class ResultDose
{
    private List<Dose> lists = new List<Dose>();
    [JsonProperty("data")]
    public List<Dose> list { get { return lists; } }
}