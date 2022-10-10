using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for ListPatient
/// </summary>
public class ListPatientIpd
{
    public long admissionId { get; set; }
    public long patientId { get; set; }
    public string patientName { get; set; }
    public long wardId { get; set; }
    public string roomNo { get; set; }
    public string status { get; set; }
    public DateTime flagDischarged { get; set; }
    public long dischargeTypeId { get; set; }
    public long dischargeConditionId { get; set; }
}

public class ResultListPatient
{
    private List<ListPatientIpd> lists = new List<ListPatientIpd>();
    [JsonProperty("data")]
    public List<ListPatientIpd> list { get { return lists; } }
}