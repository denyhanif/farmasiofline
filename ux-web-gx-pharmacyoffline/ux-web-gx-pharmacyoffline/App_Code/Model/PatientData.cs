using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for PatientData
/// </summary>
public class PatientData
{
    public long PatientId { get; set; }
    public string PatientName { get; set; }
    public string BirthDate { get; set; }
    public string Age { get; set; }
    public string ReligionName { get; set; }
    public short SexId { get; set; }
}

public class resultPatientData
{
    private PatientData lists = new PatientData();
    [JsonProperty("data")]
    public PatientData list { get { return lists; } }
    public string Company { set; get; }
    public string Status { set; get; }
    public int Code { set; get; }
    public string Message { set; get; }
}

public class PatientDataByMRResult
{
    private PatientData patientData = new PatientData();
    [JsonProperty("data")]
    public PatientData Patient { get { return patientData; } }
}

public class resultPatientDataStatus
{
    private string status;
    [JsonProperty("status")]
    public string statusValue { get { return status; } }
}