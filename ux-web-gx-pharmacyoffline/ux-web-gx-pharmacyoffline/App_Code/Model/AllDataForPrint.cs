using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for AllDataForPrint
/// </summary>
public class AllDataForPrint
{
    public int key { get; set; }
    public int isSelected { get; set; }
    public string DoctorName { get; set; }
    public string PatientName { get; set; }
    public string OrgCode { get; set; }
    public string Birthdate { get; set; }
    public string Gender { get; set; }
    public string localMRno { get; set; }
    public string Subjective { get; set; }
    public string Objective { get; set; }
    public string Diagnosis { get; set; }
    public string PlanningProcedure { get; set; }

    public long AdmissionId { get; set; }
    public Guid EncounterId { get; set; }
    public long OrganizationId { get; set; }
}

public class resultAllDataForPrint
{
    private AllDataForPrint lists = new AllDataForPrint();
    [JsonProperty("data")]
    public AllDataForPrint list { get { return lists; } }
}