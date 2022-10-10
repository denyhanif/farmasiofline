using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for ViewOrganizationSetting
/// </summary>
public class ViewOrganizationSetting
{
    public string setting_name { get; set; }
    public string setting_value { get; set; }
}

public class ResultViewOrganizationSetting
{
    private List<ViewOrganizationSetting> lists = new List<ViewOrganizationSetting>();
    [JsonProperty("data")]
    public List<ViewOrganizationSetting> list { get { return lists; } }
}