using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for QueueLine
/// </summary>
public class QueueLine
{
    public Guid queue_line_id { get; set; }
    public string queue_line_name { get; set; }
    public string created_by { get; set; }
    public DateTime? created_date { get; set; }
    public string created_name { get; set; }
    public string created_from { get; set; }
    public string modified_by { get; set; }
    public DateTime? modified_date { get; set; }
    public string modified_name { get; set; }
    public string modified_from { get; set; }
    public int button_qty { get; set; }
    public Guid queue_line_hospital_id { get; set; }
    public Guid hospital_id { get; set; }
    public Guid floor_id { get; set; }
    public string floor_name { get; set; }
    public string queue_line_floor_name { get; set; }
}

public class ResultQueueLine
{
    private List<QueueLine> lists = new List<QueueLine>();
    [JsonProperty("data")]
    public List<QueueLine> list { get { return lists; } }
}

public class QueueLineNew
{
    public Guid queue_line_id { get; set; }
    public string queue_line_name { get; set; }
    public int button_qty { get; set; }
    public Guid queue_line_hospital_id { get; set; }
    public Guid hospital_id { get; set; }
    public Guid floor_id { get; set; }
    public string floor_name { get; set; }
    public string visit_type_floor_name { get; set; }
    public string category_visit_type { get; set; }
    public string visit_type_hospital_id { get; set; }
}

public class ResultQueueLineNew
{
    private List<QueueLineNew> lists = new List<QueueLineNew>();
    [JsonProperty("data")]
    public List<QueueLineNew> list { get { return lists; } }
}