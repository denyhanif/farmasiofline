using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for PatientDataHeaderForPrint
/// </summary>
public class PatientDataHeaderForPrint
{
    public string DoctorName { get; set; }
    public string PatientName { get; set; }
    public string OrgCode { get; set; }
    public string Birthdate { get; set; }
    public string Gender { get; set; }
    public string localMRno { get; set; }
}


public class resultPatientDataHeaderForPrint
{
    private PatientDataHeaderForPrint lists = new PatientDataHeaderForPrint();
    [JsonProperty("data")]
    public PatientDataHeaderForPrint list { get { return lists; } }
}