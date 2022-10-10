using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Doctor
/// </summary>
public class Doctor
{
    public Int64? doctor_id { get; set; }
    public string name { get; set; }
}

public class ResultDoctor
{
    //private List<Doctor> lists = new List<Doctor>();
    [JsonProperty("data")]
    public Doctor doc { get; set; }
}