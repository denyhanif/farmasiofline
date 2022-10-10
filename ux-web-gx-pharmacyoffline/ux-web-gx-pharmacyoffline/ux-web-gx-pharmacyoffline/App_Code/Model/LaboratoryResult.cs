using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for LaboratoryResult
/// </summary>
/// 

[Serializable]
public class LaboratoryResult
{
    public Int64? organizationId { get; set; }
    public String orgCd { get; set; }
    public Int64? admissionId { get; set; }
    public String admissionNo { get; set; }
    public DateTime? admissionDate { get; set; }
    public String cliniciaN_NM { get; set; }
    public String ono { get; set; }
    public String disP_SEQ { get; set; }
    public String seqno { get; set; }
    public String tesT_CD { get; set; }
    public String tesT_NM { get; set; }
    public String datA_TYP { get; set; }
    public String resulT_VALUE { get; set; }
    public String resulT_FT { get; set; }
    public String unit { get; set; }
    public String flag { get; set; }
    public String reF_RANGE { get; set; }
    public String status { get; set; }
    public String tesT_COMMENT { get; set; }
    public String tesT_GROUP { get; set; }
    public Int64 IsHeader { get; set; }
    public string ConnStatus { get; set; }
}

public class ResultLaboratoryResult
{
    private List<LaboratoryResult> lists = new List<LaboratoryResult>();
    [JsonProperty("data")]
    public List<LaboratoryResult> list { get { return lists; } }
}

public class gridLaboratory
{
    public String tesT_GROUP { get; set; }
    public String tesT_NM { get; set; }
    public String resulT_VALUE { get; set; }
    public String unit { get; set; }
    public String reF_RANGE { get; set; }
    public String ono { get; set; }
    public String dis_sq { get; set; }
    public Int64 IsHeader { get; set; }
}

public class radiologyByWeek
{
    public Int64? organizationId { get; set; }
    public String orgCd { get; set; }
    public Int64? admissionId { get; set; }
    public String admissionNo { get; set; }
    public DateTime? admissionDate { get; set; }
    public String doctorName { get; set; }
    public String salesItemName { get; set; }
    public String responseMessage { get; set; }
    public String imageUrl { get; set; }
}

public class ResultRadiologyByWeek
{
    private List<radiologyByWeek> lists = new List<radiologyByWeek>();
    [JsonProperty("data")]
    public List<radiologyByWeek> list { get { return lists; } }
}