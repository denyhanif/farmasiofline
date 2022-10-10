using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Ward
/// </summary>
public class Ward
{
    public long WardId { get; set; }
    public long OrganizationId { get; set; }
    public string Code { get; set; }
    public string Name { get; set; }
    public bool IsActive { get; set; }
}


public class ListWard
{
    private List<Ward> lists = new List<Ward>();
    [JsonProperty("data")]
    public List<Ward> list { get { return lists; } }
}


public class WardCollection
{
    public long WardId { get; set; }
    public string WardName { get; set; }
}

//public class UpdaterValue
//{
//    public long OrganizationId { get; set; }
//    public Guid WorklistId { get; set; }
//    public long AdmissionId { get; set; }
//    public string ModifiedBy { get; set; }
//    public string ModifiedDate { get; set; }
//    public int type { get; set; }
//}