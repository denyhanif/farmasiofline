using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Organization
/// </summary>
public class Organization
{
    public Int64 organizationId { get; set; }
    public string code { get; set; }
    public string name { get; set; }
}

public class ResultOrganization
{
    private List<Organization> lists = new List<Organization>();
    [JsonProperty("data")]
    public List<Organization> list { get { return lists; } }
}

public class OrganizationSetting
{
    public Guid organization_setting_id { get; set; }
    public Int64 organization_id { get; set; }
    public string setting_name { get; set; }
    public string setting_value { get; set; }
    public bool is_active { get; set; }
    public DateTime created_date { get; set; }
    public string created_by { get; set; }
    public DateTime modified_date { get; set; }
    public string modified_by { get; set; }
}

public class ResultOrganizationSetting
{
    private OrganizationSetting lists = new OrganizationSetting();
    [JsonProperty("data")]
    public OrganizationSetting list { get { return lists; } }
}