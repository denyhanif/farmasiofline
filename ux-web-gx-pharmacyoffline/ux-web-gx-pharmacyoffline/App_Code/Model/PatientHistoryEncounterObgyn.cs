using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for PatientHistoryEncounter
/// </summary>
public class PatientHistoryEncounterObgyn : PatientHistoryEncounterData
{
    public List<historypregnancydataSOAP> historypregnancydata { get; set; }
}

public class ResultPatientHistoryEncounterObgyn
{
    private PatientHistoryEncounterObgyn lists = new PatientHistoryEncounterObgyn();
    [JsonProperty("data")]
    public PatientHistoryEncounterObgyn list { get { return lists; } }
}

public class historypregnancydataSOAP
{
    public string pregnancy_data_type { get; set; }
    public string value { get; set; }
    public string remarks { get; set; }
    public string status { get; set; }
}