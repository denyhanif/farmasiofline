using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

/// <summary>
/// Summary description for CountPatient
/// </summary>
public class CountPatientx
{
    public int countplan { get; set; }
    public int countplanlate { get; set; }
    public int countprocess { get; set; }
    public int countprocesslate { get; set; }
    public int countdone { get; set; }
}

public class ResultCountPatientx
{
    //private List<CountPatient> lists = new List<CountPatient>();
    [JsonProperty("data")]
    public CountPatient list { get; set; }
}
