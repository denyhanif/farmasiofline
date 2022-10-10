using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for MedicalHistory
/// </summary>
[Serializable]
public class MedicalHistory
{
    public DateTime? admissionDate { get; set; }
    public DateTime? orderDate { get; set; }
    public String admissionNo { get; set; }
    public String admissionDoctor { get; set; }
    public String medicationDoctor { get; set; }
    public Int64? itemId { get; set; }
    public String itemName { get; set; }
    public Int64? quantity { get; set; }
    public String uom { get; set; }
    public String frequency { get; set; }
    public Int64? dose { get; set; }
    public String doseText { get; set; }
    public String instruction { get; set; }
    public String route { get; set; }
    public String iter { get; set; }
    public String isRoutine { get; set; }
    public String isCompound { get; set; }
    public String compoundName { get; set; }
    public String isConsumables { get; set; }

    public class ResultMedicalHistory
    {
        private List<MedicalHistory> lists = new List<MedicalHistory>();
        [JsonProperty("data")]
        public List<MedicalHistory> list { get { return lists; } }
    }

    public MedicalHistory()
    {

    }

}