using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for PatientHistoryEncounter
/// </summary>
public class PatientHistoryEncounterPediatric : PatientHistoryEncounterData
{
    public List<historypediatricdataSOAP> historypediatricdata { get; set; }
}

public class ResultPatientHistoryEncounterPediatric
{
    private PatientHistoryEncounterPediatric lists = new PatientHistoryEncounterPediatric();
    [JsonProperty("data")]
    public PatientHistoryEncounterPediatric list { get { return lists; } }
}

public class historypediatricdataSOAP
{
    public string pediatric_data_type { get; set; }
    public string value { get; set; }
    public string remarks { get; set; }
}