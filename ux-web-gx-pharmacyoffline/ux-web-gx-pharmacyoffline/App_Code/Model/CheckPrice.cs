using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for WorklistPharmacy
/// </summary>
/// 
public class CheckPriceResult
{
    public string ConsultationFee { get; set; }
    public List<SingleQueueCheckPrice> drug_price { get; set; }
    public List<SingleQueueCheckPrice> cpoe_price { get; set; }
    public List<SingleQueueCheckPrice> procedure_price { get; set; }
    public List<SingleQueueCheckPrice> admin_fee { get; set; }
}

public class ResponseCheckPriceResult
{
    public string Company { set; get; }
    public string Status { set; get; }
    public int Code { set; get; }
    public string Message { set; get; }
    public CheckPriceResult Data { set; get; }
}

public class SingleQueueCheckPrice
{
    public long SalesItemId { get; set; }
    public string SalesItemCode { get; set; }
    public string SalesItemName { get; set; }
    public string Uom { get; set; }
    public string Quantity { get; set; }
    public string SinglePrice { get; set; }
    public string Amount { get; set; }
    public string DiscountPrice { get; set; }
    public string PayerNet { get; set; }
    public string PatientNet { get; set; }
    public string TotalPayerNet { get; set; }
    public string TotalPatientNet { get; set; }
    public string RoundingPayerNet { get; set; }
    public string RoundingPatientNet { get; set; }
    public string TotalPayerNetFinal { get; set; }
    public string TotalPatientNetFinal { get; set; }
    public long SalesItemTypeId { get; set; }
    public string SalesItemType { get; set; }
    public int is_consumables { get; set; }
}

//OLD VERSION-----------------------------------------------------------

public class CheckPrice
{
    public long SalesItemId { get; set; }
    public string SalesItemCode { get; set; }
    public string SalesItemName { get; set; }
    public string Uom { get; set; }
    public string Quantity { get; set; }
    public string SinglePrice { get; set; }
    public string Amount { get; set; }
    public string DiscountPrice { get; set; }
    public string PayerNet { get; set; }
    public string PatientNet { get; set; }
    public string TotalPayerNet { get; set; }
    public string TotalPatientNet { get; set; }
    public string RoundingPayerNet { get; set; }
    public string RoundingPatientNet { get; set; }
    public string TotalPayerNetFinal { get; set; }
    public string TotalPatientNetFinal { get; set; }
    public int salesItemTypeId { get; set; }
    public string salesItemType { get; set; }
    public int is_consumables { get; set; }
    public string consultationFee { get; set; }
}

public class ResultCheckPrice
{
    private List<CheckPrice> lists = new List<CheckPrice>();
    [JsonProperty("data")]
    public List<CheckPrice> list { get { return lists; } }
}

public class CheckPriceRequest
{
    public long item_id { get; set; }
    public string quantity { get; set; }
    public long uom_id { get; set; }
    public int is_consumables { get; set; }
}

public class ResultCheckPriceRequest
{
    private List<CheckPriceRequest> lists = new List<CheckPriceRequest>();
    [JsonProperty("data")]
    public List<CheckPriceRequest> list { get { return lists; } }
}