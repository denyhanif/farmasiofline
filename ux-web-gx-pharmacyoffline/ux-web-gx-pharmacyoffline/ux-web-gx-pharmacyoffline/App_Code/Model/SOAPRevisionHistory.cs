using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for SOAPRevisionHistory
/// </summary>
public class SOAPRevisionHistory
{
    public SOAPRevisionHistory()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public class SOAPLog
    {
        public List<LogHeader> Header { get; set; }
        public List<LogSOAPData> SOAPData { get; set; }
        public List<LogCPOE> CPOEData { get; set; }
        public List<LogPrescription> PrescriptionData { get; set; }
    }

    public class ResultSOAPLog
    {
        private SOAPLog lists = new SOAPLog();
        [JsonProperty("data")]
        public SOAPLog list { get { return lists; } set { lists = value; } }
    }

    public class LogHeader
    {
        public long ID { get; set; }
        public DateTime LogDate { get; set; }
        public string UserName { get; set; }
        public bool IsPharmacy { get; set; }
    }

    public class LogSOAPData
    {
        public long ID { get; set; }
        public string MappingName { get; set; }
        public string MappingType { get; set; }
        public string Value { get; set; }
        public string Remarks { get; set; }
    }

    public class LogCPOE
    {
        public long ID { get; set; }
        public string ItemName { get; set; }
        public string ItemType { get; set; }
        public string Remarks { get; set; }
        public bool IsCITO { get; set; }
    }

    public class LogPrescription
    {
        public long ID { get; set; }
        public string SalesItemName { get; set; }
        public string Dose { get; set; }
        public string DoseUom { get; set; }
        public string Frequency { get; set; }
        public string Route { get; set; }
        public string Instruction { get; set; }
        public string Quantity { get; set; }
        public string Uom { get; set; }
        public int Iteration { get; set; }
        public bool IsRoutine { get; set; }
        public bool IsConsumables { get; set; }
        public bool IsPharmacy { get; set; }
    }
}