using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using Microsoft.AspNetCore.Mvc;
using Siloam.System.Web;
using Siloam.System.Data;
using Siloam.Service.EMRPharmacy.Models;
using Siloam.Service.EMRPharmacy.Models.ViewModels;
using Siloam.Service.EMRPharmacy.Models.Functional;
using Siloam.Service.EMRPharmacy.Commons;
using Siloam.Service.EMRPharmacy.Hub;
using Microsoft.AspNetCore.SignalR;
using AutoMapper;
using System.Threading.Tasks;
using System.Net.Http;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Siloam.Service.EMRPharmacy.Models.Parameter;
using Siloam.Service.EMRPharmacy.Models.SyncHope;

namespace Siloam.Service.EMRPharmacy.Controllers
{
    public class PharmacyController : BaseController
    {
        private readonly IHubContext<MessageHub> messageHubContexts;


        public PharmacyController(IUnitOfWork unitOfWork, IHubContext<MessageHub> messageHubContext) : base(unitOfWork)
        {

            messageHubContexts = messageHubContext;

        }

        public static async Task<string> SlackMessageAsync(string SlackUrl, string ApiName, string ApiUrl, string ApiFunction, string Parameter, string ErrorMessage)
        {
            using (var httpClient = new HttpClient())
            {
                using (var request = new HttpRequestMessage(new HttpMethod("POST"), SlackUrl))
                {
                    SlackHeader slackHeader = new SlackHeader();
                    slackHeader.text = "<!channel> ERROR DETECTED ON API :\n *<" + ApiUrl + "|" + ApiName + ">*";

                    SlackDetail slackDetail = new SlackDetail();
                    slackDetail.fallback = "Error Message: \n         " + ErrorMessage;
                    slackDetail.title = "Error Message: \n         " + ErrorMessage;
                    slackDetail.author_name = "Function: " + ApiFunction;
                    slackDetail.text = "*Parameters*: \n         " + Parameter;

                    List<SlackDetail> slackDetails = new List<SlackDetail>();
                    slackDetails.Add(slackDetail);
                    slackHeader.attachments = slackDetails;

                    var jsonSlack = Newtonsoft.Json.JsonConvert.SerializeObject(slackHeader).ToString();

                    request.Content = new StringContent(jsonSlack, Encoding.UTF8, "application/json");
                    var response = await httpClient.SendAsync(request);

                    return response.ToString();
                }
            }
        }

        [HttpPost("pharmacyrecord/{SubmitBy:long}/{UserName}/{QueueNo}/{DeliveryFee}/{PayerCoverage}/{IsDefaultCoverage:int}")]
        [ProducesResponseType(typeof(ResponseData<string>), 200)]
        public IActionResult Create(Int64 SubmitBy, string UserName, string QueueNo, string DeliveryFee, string PayerCoverage, short IsDefaultCoverage, [FromBody]PharmacyData model)
        {

            int page = 1;
            int total = 0;
            //List<PharmacyCompoundHeader> compoundHeaders = new List<PharmacyCompoundHeader>();
            //List<PharmacyCompoundDetail> compoundDetails = new List<PharmacyCompoundDetail>();
            //model.compound_header = compoundHeaders;
            //model.compound_detail = compoundDetails;

            string jsonModel = JsonConvert.SerializeObject(model);
            string usedosetext = "";

            if (model == null)
            {

                HttpResults = new ResponseMessage(Siloam.System.Web.StatusCode.BadRequest, StatusMessage.Fail, HttpResponseMessageKey.DataUnsuccessfullyCreated, total);
                Task<string> slackNotification = SlackMessageAsync(ValueStorage.SlackUrl, ValueStorage.ApiName, ValueStorage.ApiUrl + "/pharmacyrecord", "[POST]Submit Verify Prescription", SubmitBy + "/" + jsonModel, "Data Unsuccessfully Created");
                goto response;

            }

            try
            {
                long transAdmId = model.header.Admissionid;
                string transAdmNo = model.header.AdmissionNo;
                List<long> aidopayerid = new List<long>();
                List<long> mysiloampayerid = new List<long>();
                string payertemp = "";
                payertemp = IUnitOfWorks.UnitOfWorkAidoDrug().GetSettingAidoPayerId(model.header.OrganizationId);
                aidopayerid = payertemp.Split(',').Select(Int64.Parse).ToList();
                
                payertemp = "";
                payertemp = IUnitOfWorks.UnitOfWorkAidoDrug().GetSettingMysiloamPayerId(model.header.OrganizationId);
                mysiloampayerid = payertemp.Split(',').Select(Int64.Parse).ToList();

                long pharmacypayerid = IUnitOfWorks.UnitOfWorkPharmacy().GetPayerIdRecord(model.header.OrganizationId, model.header.PatientId, model.header.Admissionid, model.header.EncounterId);
                
                if (model.header.ChannelId == "18" || aidopayerid.Contains(pharmacypayerid))
                {
                    int CountAIDO = IUnitOfWorks.UnitOfWorkAidoDrug().GetCountAIDOOrder(model.header.OrganizationId, model.header.PatientId, model.header.Admissionid, model.header.EncounterId);
                    if (CountAIDO > 0)
                    {
                        HttpResults = new ResponseData<string>("Data unsuccessfully updated", Siloam.System.Web.StatusCode.OK, StatusMessage.Success, "|AIDOFAILED|ALREADY VERIFIED AND SENT TO AIDO", page, total);
                    }
                    else
                    {
                        string syncStatusAIDO = "";
                        string syncMessageAIDO = "";
                        string result = "";
                        string JsonResponse = "";
                        string ErrMsg = "";
                        List<ItemPrice> itemPrices = new List<ItemPrice>();
                        itemPrices = IUnitOfWorks.UnitOfWorkAidoSync().GetItemSync(model.header.OrganizationId, model.header.PatientId, model.header.Admissionid, model.header.EncounterId, model.prescription, DeliveryFee, model.header.IsEditDrug, model.header.IsEditConsumables);

                        //HIT API AIDO -> GET STATUS & MESSAGE
                        AidoRequestModel aido = new AidoRequestModel();
                        Guid SiloamTrxId = new Guid();
                        SiloamTrxId = IUnitOfWorks.UnitOfWorkAidoDrug().GetSiloamTrxId(model.header.OrganizationId, model.header.PatientId, model.header.Admissionid, model.header.EncounterId);
                        if (SiloamTrxId == Guid.Empty)
                        {
                            SiloamTrxId = Guid.NewGuid();
                        }
                        aido.requiredDate = DateTime.Now;
                        aido.shippedDate = DateTime.Now;
                        aido.totalPrice = int.Parse(itemPrices.First().TotalPrice.ToString());
                        aido.siloamTrxId = SiloamTrxId;
                        List<AidoDrug> drug = new List<AidoDrug>();
                        drug = (from a in itemPrices
                                select new AidoDrug
                                {
                                    name = a.SalesItemName,
                                    qty = a.quantity,
                                    uom = a.Uom,
                                    price = a.SubTotal
                                }).ToList();
                        aido.items = drug;

                        string JsonStringAido = JsonConvert.SerializeObject(aido);
                        string token = IUnitOfWorks.UnitOfWorkAidoSync().GenerateJSONWebToken(model.header.OrganizationId, model.header.PatientId, model.header.Admissionid, model.header.DoctorId);
                        result = IUnitOfWorks.UnitOfWorkAidoSync().AidoSyncPrescription(JsonStringAido, model.header.OrganizationId, model.header.PatientId, model.header.Admissionid, model.header.DoctorId, token);
                        if (!result.ToUpper().Contains("KONEKSI KE AIDO GAGAL"))
                        {
                            JObject Response = (JObject)JsonConvert.DeserializeObject<dynamic>(result);
                            syncStatusAIDO = Response.Property("status").Value.ToString();
                            syncMessageAIDO = Response.Property("message").Value.ToString();
                            ErrMsg = Response.Property("message").Value.ToString();
                            JsonResponse = result;
                        }
                        else
                        {
                            //if exception or timeout
                            syncStatusAIDO = "FAIL";
                            syncMessageAIDO = "KONEKSI KE AIDO GAGAL";
                            ErrMsg = result;
                        }

                        if (syncStatusAIDO.ToLower() == "ok")
                        {
                            string insertaidoticket = IUnitOfWorks.UnitOfWorkAidoDrug().InsertData(model.header.OrganizationId, model.header.PatientId, model.header.Admissionid, model.header.EncounterId, JsonStringAido, JsonResponse, SiloamTrxId, model.header.ChannelId);
                            string data = IUnitOfWorks.UnitOfWorkPharmacy().UpdateRecordSubmit(SubmitBy, transAdmId, transAdmNo, QueueNo, DeliveryFee, PayerCoverage, model);
                            messageHubContexts.Clients.All.InvokeAsync("Update Submit Data", data);
                            HttpResults = new ResponseData<string>("Data successfully updated", Siloam.System.Web.StatusCode.OK, StatusMessage.Success, data + "|SUCCESS|", page, total);
                        }
                        else
                        {
                            string data = IUnitOfWorks.UnitOfWorkPharmacy().UpdateRecordSubmit(SubmitBy, transAdmId, transAdmNo, QueueNo, DeliveryFee, PayerCoverage, model);
                            string insertaidofailed = IUnitOfWorks.UnitOfWorkAidoSync().InsertDataLogFailed(model.header.OrganizationId, model.header.PatientId, model.header.Admissionid, model.header.EncounterId, token, JsonStringAido, JsonResponse, ErrMsg, model.header.ChannelId);
                            messageHubContexts.Clients.All.InvokeAsync("Update Submit Data", data);
                            HttpResults = new ResponseData<string>("Data successfully updated", Siloam.System.Web.StatusCode.OK, StatusMessage.Success, data + "|AIDOFAILED|" + syncMessageAIDO, page, total);
                        }
                    }
                }
                else if (model.header.ChannelId == "5" || model.header.ChannelId == "9" || mysiloampayerid.Contains(pharmacypayerid))
                {
                    int CountMySiloam = IUnitOfWorks.UnitOfWorkAidoDrug().GetCountAIDOOrder(model.header.OrganizationId, model.header.PatientId, model.header.Admissionid, model.header.EncounterId);
                    if (CountMySiloam > 0)
                    {
                        HttpResults = new ResponseData<string>("Data unsuccessfully updated", Siloam.System.Web.StatusCode.OK, StatusMessage.Success, "|MYSILOAMFAILED|ALREADY VERIFIED AND SENT TO MYSILOAM", page, total);
                    }
                    else
                    {
                        //MYSILOAM
                        string syncStatusMySiloam = "";
                        string syncMessageMySiloam = "";
                        string resultMySiloam = "";
                        string JsonResponseMySiloam = "";
                        string ErrMsgMySiloam = "";
                        List<ItemPrice> itemPrices = new List<ItemPrice>();
                        itemPrices = IUnitOfWorks.UnitOfWorkAidoSync().GetItemSyncTeleconsultation(model.header.OrganizationId, model.header.PatientId, model.header.Admissionid, model.header.EncounterId, model.prescription, DeliveryFee, model.header.IsEditDrug, model.header.IsEditConsumables, PayerCoverage, IsDefaultCoverage);

                        //HIT API AIDO -> GET STATUS & MESSAGE
                        MySiloamRequestDrug silo = new MySiloamRequestDrug();
                        //string value = itemPrices.First().PatientNetTotal;
                        //int asdf2 = Convert.ToInt32(Math.Floor(Convert.ToDouble(value)));
                        silo.totalPatientNet = Convert.ToInt32(Math.Floor(Convert.ToDouble(itemPrices.First().PatientNetTotal.ToString())));
                        silo.totalPayerNet = Convert.ToInt32(Math.Floor(Convert.ToDouble(itemPrices.First().PayerNetTotal.ToString())));
                        silo.payerCoverage = Convert.ToInt32(Math.Floor(Convert.ToDouble(PayerCoverage)));
                        if (mysiloampayerid.Contains(pharmacypayerid))
                        {
                            silo.totalPrice = int.Parse(itemPrices.First().TotalPrice.ToString());
                            silo.totalPatientNet = silo.totalPrice;
                        }
                        else
                        {
                            if (silo.totalPayerNet < silo.payerCoverage && silo.totalPayerNet != 0)
                            {
                                HttpResults = new ResponseData<string>("Data successfully updated", Siloam.System.Web.StatusCode.OK, StatusMessage.Success, "FAIL|MYSILOAMFAILED|COVERAGE BIGGER THAN TOTAL PAYER NET " + silo.totalPayerNet.ToString(), page, total);
                            }
                            silo.totalPrice = silo.totalPayerNet - silo.payerCoverage + silo.totalPatientNet;
                        }
                        silo.isSelfCollection = model.header.IsSelfCollection;
                        List<MySiloamDrug> drug = new List<MySiloamDrug>();
                        drug = (from a in itemPrices
                                select new MySiloamDrug
                                {
                                    itemId = a.SalesItemId,
                                    name = a.SalesItemName,
                                    qty = a.quantity,
                                    uom = a.Uom,
                                    patientNet = a.PatientNet,
                                    payerNet = a.PayerNet,
                                    frequency = a.Frequency,
                                    instruction = a.Instruction
                                }).ToList();
                        Guid TrxId = new Guid();
                        TrxId = IUnitOfWorks.UnitOfWorkAidoDrug().GetSiloamTrxId(model.header.OrganizationId, model.header.PatientId, model.header.Admissionid, model.header.EncounterId);
                        if (TrxId == Guid.Empty)
                        {
                            TrxId = Guid.NewGuid();
                        }
                        silo.items = drug;
                        silo.admissionHopeId = model.header.Admissionid.ToString();
                        silo.userId = SubmitBy.ToString();
                        silo.userName = UserName;
                        silo.source = "EMR";
                        silo.siloamTrxId = TrxId;
                        silo.appointmentId = IUnitOfWorks.UnitOfWorkPharmacy().GetAppointmentId(model.header.OrganizationId, model.header.PatientId, model.header.Admissionid, model.header.DoctorId);

                        string JsonStringMySiloam = JsonConvert.SerializeObject(silo);
                        //string token = IUnitOfWorks.UnitOfWorkAidoSync().GenerateJSONWebToken(model.header.OrganizationId, model.header.PatientId, model.header.Admissionid, model.header.DoctorId);
                        resultMySiloam = IUnitOfWorks.UnitOfWorkSync().SyncDrugMySiloam(JsonStringMySiloam);
                        if (!resultMySiloam.ToUpper().Contains("KONEKSI KE MYSILOAM GAGAL"))
                        {
                            JObject ResponseSilo = (JObject)JsonConvert.DeserializeObject<dynamic>(resultMySiloam);
                            syncStatusMySiloam = ResponseSilo.Property("status").Value.ToString();
                            syncMessageMySiloam = ResponseSilo.Property("message").Value.ToString();
                            ErrMsgMySiloam = ResponseSilo.Property("message").Value.ToString();
                            JsonResponseMySiloam = resultMySiloam;
                        }
                        else
                        {
                            //if exception or timeout
                            syncStatusMySiloam = "FAIL";
                            syncMessageMySiloam = "KONEKSI KE MYSILOAM GAGAL";
                            ErrMsgMySiloam = resultMySiloam;
                        }

                        if (syncStatusMySiloam.ToLower() == "ok")
                        {
                            string insertaidoticket = IUnitOfWorks.UnitOfWorkAidoDrug().InsertData(model.header.OrganizationId, model.header.PatientId, model.header.Admissionid, model.header.EncounterId, JsonStringMySiloam, JsonResponseMySiloam, TrxId, model.header.ChannelId);
                            string data = IUnitOfWorks.UnitOfWorkPharmacy().UpdateRecordSubmit(SubmitBy, transAdmId, transAdmNo, QueueNo, DeliveryFee, PayerCoverage, model);
                            messageHubContexts.Clients.All.InvokeAsync("Update Submit Data", data);
                            HttpResults = new ResponseData<string>("Data successfully updated", Siloam.System.Web.StatusCode.OK, StatusMessage.Success, data + "|SUCCESS|", page, total);
                        }
                        else
                        {
                            string insertaidofailed = IUnitOfWorks.UnitOfWorkAidoSync().InsertDataLogFailed(model.header.OrganizationId, model.header.PatientId, model.header.Admissionid, model.header.EncounterId, "", JsonStringMySiloam, JsonResponseMySiloam, ErrMsgMySiloam, model.header.ChannelId);
                            string data = IUnitOfWorks.UnitOfWorkPharmacy().UpdateRecordSubmit(SubmitBy, transAdmId, transAdmNo, QueueNo, DeliveryFee, PayerCoverage, model);
                            messageHubContexts.Clients.All.InvokeAsync("Update Submit Data", data);
                            HttpResults = new ResponseData<string>("Data successfully updated", Siloam.System.Web.StatusCode.OK, StatusMessage.Success, data + "|MYSILOAMFAILED|" + syncMessageMySiloam, page, total);
                        }
                    }
                }
                else
                {
                    usedosetext = IUnitOfWorks.UnitOfWorkPharmacy().GetSettingDoseText(model.header.OrganizationId);
                    var processStart = IUnitOfWorks.UnitOfWorkPharmacy().InsertToTimeStampTable(model.header.EncounterId, "PHARMACY-CALL API SUBMIT");
                    //from API HOPE
                    string syncStatusCons = "";
                    string syncStatus = "";

                    int tempcount = model.prescription.Count(p => p.is_consumables == 1);

                    model.prescription.ForEach(p => p.prescription_sync_id = Guid.NewGuid());
                    model.compound_header.ForEach(p => p.compound_header_sync_id = Guid.NewGuid());
                    model.compound_detail.ForEach(p => p.compound_detail_sync_id = Guid.NewGuid());
                    SyncPrescription pres = new SyncPrescription();
                    List<OrderItem> items = new List<OrderItem>();
                    List<OrderItem> temp = new List<OrderItem>();
                    //List<OrderItem> compheader = new List<OrderItem>();
                    //List<OrderItem> compdetail = new List<OrderItem>();
                    //List<OrderItem> tempcomp = new List<OrderItem>();
                    temp = IUnitOfWorks.UnitOfWorkPharmacy().GetAdditional(model.header.OrganizationId, model.header.Admissionid, model.header.EncounterId, 1, usedosetext);
                    items = (from a in model.prescription
                             where a.is_consumables == 0 && decimal.Parse(a.IssuedQty) > 0
                             orderby a.item_sequence
                             select new OrderItem
                             {
                                 AdministrationFrequencyId = a.frequency_id,
                                 AdministrationInstruction = a.remarks,
                                 AdministrationRouteId = a.administration_route_id,
                                 DispensingInstruction = "",
                                 Dose = a.dose_text != "" && usedosetext == "TRUE" ? "0.0001" : a.dosage_id,
                                 DoseText = a.dose_text,
                                 DoseUomId = a.dose_uom_id,
                                 DrugId = a.item_id,
                                 IsPrn = false,
                                 PatientInformation = "",
                                 Quantity = a.IssuedQty,
                                 Repeat = a.iteration,
                                 MedicalEncounterEntryInterfaceId = a.prescription_sync_id
                             }).ToList();
                    if (temp != null)
                    {
                        items.AddRange(temp);
                    }
                    //if (model.compound_header.Count(p => p.is_additional == false) > 0)
                    //{
                    //    tempcomp = IUnitOfWorks.UnitOfWorkPharmacy().GetAdditionalCompound(model.header.OrganizationId, model.header.DoctorId, model.header.Admissionid, model.header.EncounterId, true);
                    //    compheader = (from a in model.compound_header
                    //                  where a.is_additional == false
                    //                  orderby a.item_sequence
                    //                  select new OrderItem
                    //                  {
                    //                      AdministrationFrequencyId = a.administration_frequency_id,
                    //                      AdministrationInstruction = a.administration_instruction,
                    //                      AdministrationRouteId = a.administration_route_id,
                    //                      DispensingInstruction = "",
                    //                      Dose = a.dose,
                    //                      DoseText = "",
                    //                      DoseUomId = a.dose_uom_id,
                    //                      DrugId = long.Parse(ValueStorage.CompoundItem),
                    //                      IsPrn = false,
                    //                      PatientInformation = "",
                    //                      Quantity = a.quantity,
                    //                      Repeat = a.iter,
                    //                      MedicalEncounterEntryInterfaceId = a.compound_header_sync_id
                    //                  }).ToList();

                    //    compdetail = (from a in model.compound_detail
                    //                  join b in model.compound_header
                    //                  on a.prescription_compound_header_id equals b.prescription_compound_header_id
                    //                  where b.is_additional == false && a.is_additional == false
                    //                  orderby a.item_sequence
                    //                  select new OrderItem
                    //                  {
                    //                      AdministrationFrequencyId = a.administration_frequency_id == 0 ? 7 : a.administration_frequency_id,
                    //                      AdministrationInstruction = "",
                    //                      AdministrationRouteId = a.administration_route_id == 0 ? 1 : a.administration_route_id,
                    //                      DispensingInstruction = "",
                    //                      Dose = a.dose == "0.000" || a.dose == "" ? "1.0" : a.dose,
                    //                      DoseText = "",
                    //                      DoseUomId = a.dose_uom_id == 0 ? 226 : a.dose_uom_id,
                    //                      DrugId = a.item_id,
                    //                      IsPrn = false,
                    //                      PatientInformation = "",
                    //                      Quantity = (Decimal.Parse(a.quantity) * Decimal.Parse(b.quantity)).ToString(),
                    //                      Repeat = 0,
                    //                      MedicalEncounterEntryInterfaceId = a.compound_detail_sync_id
                    //                  }).ToList();

                    //    items.AddRange(compheader);
                    //    items.AddRange(compdetail);
                    //    if (tempcomp != null)
                    //    {
                    //        items.AddRange(tempcomp);
                    //    }
                    //}

                    //SYNC DRUG TO HOPE
                    if (items.Count() > 0)
                    {
                        pres.MedicalOrderInterfaceId = Guid.Empty;
                        pres.MedicalOrderId = null;
                        pres.MedicalEncounterInterfaceId = model.header.EncounterId;
                        pres.SalesPriorityId = 1;
                        pres.Notes = model.header.PharmacyNotes;
                        pres.OrderItems = items;

                        string JsonString = JsonConvert.SerializeObject(pres);
                        var hopeStart = IUnitOfWorks.UnitOfWorkPharmacy().InsertToTimeStampTable(model.header.EncounterId, "PHARMACY-REQUEST API DRUG HOPE");
                        syncStatus = IUnitOfWorks.UnitOfWorkSync().SyncPrescription(model.header.OrganizationId, model.header.Admissionid, JsonString);
                        var hopeFinish = IUnitOfWorks.UnitOfWorkPharmacy().InsertToTimeStampTable(model.header.EncounterId, "PHARMACY-RESPONSE API DRUG HOPE");
                    }
                    else
                    {
                        syncStatus = "success";
                    }

                    //SYNC CONSUMABLE TO HOPE
                    if (model.prescription.Count(p => p.is_consumables == 1) > 0)
                    {
                        SyncConsumables cons = new SyncConsumables();
                        List<OrderItemConsumables> itemcons = new List<OrderItemConsumables>();
                        List<OrderItemConsumables> tempcons = new List<OrderItemConsumables>();
                        tempcons = IUnitOfWorks.UnitOfWorkPharmacy().GetAdditionalConsumables(model.header.OrganizationId, model.header.Admissionid, model.header.EncounterId, 1);
                        itemcons = (from a in model.prescription
                                    where a.is_consumables == 1 && decimal.Parse(a.IssuedQty) > 0
                                    orderby a.item_sequence
                                    select new OrderItemConsumables
                                    {
                                        UsageInstruction = a.remarks,
                                        ItemId = a.item_id,
                                        PatientInformation = "",
                                        Quantity = a.IssuedQty,
                                        DispensingInstruction = "",
                                        MedicalEncounterEntryInterfaceId = a.prescription_sync_id
                                    }).ToList();
                        if (tempcons != null)
                        {
                            itemcons.AddRange(tempcons);
                        }
                        cons.MedicalOrderInterfaceId = Guid.Empty;
                        cons.MedicalOrderId = null;
                        cons.MedicalEncounterInterfaceId = model.header.EncounterId;
                        cons.SalesPriorityId = 1;
                        cons.Notes = model.header.PharmacyNotes;
                        cons.OrderItems = itemcons;

                        string JsonStringCons = JsonConvert.SerializeObject(cons);
                        syncStatusCons = IUnitOfWorks.UnitOfWorkSync().SyncConsumables(model.header.OrganizationId, model.header.Admissionid, JsonStringCons);
                    }
                    else
                    {
                        syncStatusCons = "success";
                    }

                    if (syncStatus.ToLower() == "success" && syncStatusCons.ToLower() == "success")
                    {
                        string data = IUnitOfWorks.UnitOfWorkPharmacy().UpdateRecordSubmit(SubmitBy, transAdmId, transAdmNo, QueueNo, DeliveryFee, PayerCoverage, model);
                        string updateFlagHope = IUnitOfWorks.UnitOfWorkPharmacy().UpdateFlagHOPE(model.header.OrganizationId, model.header.Admissionid, model.header.EncounterId);
                        messageHubContexts.Clients.All.InvokeAsync("Update Submit Data", data);
                        HttpResults = new ResponseData<string>("Data successfully updated", Siloam.System.Web.StatusCode.OK, StatusMessage.Success, data + "|SUCCESS|", page, total);
                    }
                    else if (syncStatus.ToLower() == "success" && syncStatusCons.ToLower() != "success")
                    {
                        //model.prescription.RemoveAll(x => x.is_consumables == 1);
                        string data = IUnitOfWorks.UnitOfWorkPharmacy().UpdateRecordSubmit(SubmitBy, transAdmId, transAdmNo, QueueNo, DeliveryFee, PayerCoverage, model);
                        string updateFlagHope = IUnitOfWorks.UnitOfWorkPharmacy().UpdateFlagHOPE(model.header.OrganizationId, model.header.Admissionid, model.header.EncounterId);
                        messageHubContexts.Clients.All.InvokeAsync("Update Submit Data", data);
                        HttpResults = new ResponseData<string>("Data successfully updated", Siloam.System.Web.StatusCode.OK, StatusMessage.Success, data + "|CONSUMABLEFAILED|" + syncStatusCons, page, total);
                    }
                    else if (syncStatus.ToLower() != "success" && syncStatusCons.ToLower() == "success")
                    {
                        //model.prescription.RemoveAll(x => x.is_consumables == 0);
                        string data = IUnitOfWorks.UnitOfWorkPharmacy().UpdateRecordSubmit(SubmitBy, transAdmId, transAdmNo, QueueNo, DeliveryFee, PayerCoverage, model);
                        string updateFlagHope = IUnitOfWorks.UnitOfWorkPharmacy().UpdateFlagHOPE(model.header.OrganizationId, model.header.Admissionid, model.header.EncounterId);
                        messageHubContexts.Clients.All.InvokeAsync("Update Submit Data", data);
                        HttpResults = new ResponseData<string>("Data successfully updated", Siloam.System.Web.StatusCode.OK, StatusMessage.Success, data + "|DRUGFAILED|" + syncStatus, page, total);
                    }
                    else
                    {
                        string data = IUnitOfWorks.UnitOfWorkPharmacy().UpdateRecordSubmit(SubmitBy, transAdmId, transAdmNo, QueueNo, DeliveryFee, PayerCoverage, model);
                        messageHubContexts.Clients.All.InvokeAsync("Update Submit Data", data);
                        if (syncStatus.ToLower() == syncStatusCons.ToLower())
                        {
                            HttpResults = new ResponseData<string>("Data successfully updated", Siloam.System.Web.StatusCode.OK, StatusMessage.Success, data + "|HOPEFAILED|" + syncStatus, page, total);
                        }
                        else
                        {
                            HttpResults = new ResponseData<string>("Data successfully updated", Siloam.System.Web.StatusCode.OK, StatusMessage.Success, data + "|HOPEFAILED|Drug Failed: " + syncStatus + ", Consumable Failed: " + syncStatusCons, page, total);
                        }
                    }
                }
            }
            catch (Exception ex)
            {

                int exCode = ex.HResult;

                if (exCode == -2147467259)
                {

                    HttpResults = new ResponseMessage(Siloam.System.Web.StatusCode.InternalServerErrorException, StatusMessage.Error, ex.Message, total);
                    Task<string> slackNotification = SlackMessageAsync(ValueStorage.SlackUrl, ValueStorage.ApiName, ValueStorage.ApiUrl + "/pharmacyrecord", "[POST]Submit Verify Prescription", SubmitBy + "/" + jsonModel, ex.Message);

                }
                else
                {

                    HttpResults = new ResponseMessage(Siloam.System.Web.StatusCode.UnprocessableEntity, StatusMessage.Fail, ex.Message, total);
                    Task<string> slackNotification = SlackMessageAsync(ValueStorage.SlackUrl, ValueStorage.ApiName, ValueStorage.ApiUrl + "/pharmacyrecord", "[POST]Submit Verify Prescription", SubmitBy + "/" + jsonModel, ex.Message);

                }

            }

            response:
            return HttpResponse(HttpResults);

        }

        [HttpPost("pharmacyrecorditemissue/{UserName}/{QueueNo}/{DeliveryFee}/{PayerCoverage}/{IsDefaultCoverage:int}/{SubmitBy:long}")]
        [ProducesResponseType(typeof(ResponseData<string>), 200)]
        public IActionResult CreateItemIssue( string UserName, string QueueNo, string DeliveryFee, string PayerCoverage, short IsDefaultCoverage, Int64 SubmitBy, [FromBody] PharmacyData model)
        {

            int page = 1;
            int total = 0;
            //List<PharmacyCompoundHeader> compoundHeaders = new List<PharmacyCompoundHeader>();
            //List<PharmacyCompoundDetail> compoundDetails = new List<PharmacyCompoundDetail>();
            //model.compound_header = compoundHeaders;
            //model.compound_detail = compoundDetails;

            string jsonModel = JsonConvert.SerializeObject(model);
            string usedosetext = "";

            if (model == null)
            {

                HttpResults = new ResponseMessage(Siloam.System.Web.StatusCode.BadRequest, StatusMessage.Fail, HttpResponseMessageKey.DataUnsuccessfullyCreated, total);
                Task<string> slackNotification = SlackMessageAsync(ValueStorage.SlackUrl, ValueStorage.ApiName, ValueStorage.ApiUrl + "/pharmacyrecorditemissue", "[POST]Submit Verify Prescription", SubmitBy + "/" + jsonModel, "Data Unsuccessfully Created");
                goto response;

            }

            try
            {
                long transAdmId = model.header.Admissionid;
                string transAdmNo = model.header.AdmissionNo;
                List<long> aidopayerid = new List<long>();
                List<long> mysiloampayerid = new List<long>();
                string payertemp = "";
                payertemp = IUnitOfWorks.UnitOfWorkAidoDrug().GetSettingAidoPayerId(model.header.OrganizationId);
                aidopayerid = payertemp.Split(',').Select(Int64.Parse).ToList();

                payertemp = "";
                payertemp = IUnitOfWorks.UnitOfWorkAidoDrug().GetSettingMysiloamPayerId(model.header.OrganizationId);
                mysiloampayerid = payertemp.Split(',').Select(Int64.Parse).ToList();

                long pharmacypayerid = IUnitOfWorks.UnitOfWorkPharmacy().GetPayerIdRecord(model.header.OrganizationId, model.header.PatientId, model.header.Admissionid, model.header.EncounterId);

                if (model.header.ChannelId == "18" || aidopayerid.Contains(pharmacypayerid))
                {
                    int CountAIDO = IUnitOfWorks.UnitOfWorkAidoDrug().GetCountAIDOOrder(model.header.OrganizationId, model.header.PatientId, model.header.Admissionid, model.header.EncounterId);
                    if (CountAIDO > 0)
                    {
                        HttpResults = new ResponseData<string>("Data unsuccessfully updated", Siloam.System.Web.StatusCode.OK, StatusMessage.Success, "|AIDOFAILED|ALREADY VERIFIED AND SENT TO AIDO", page, total);
                    }
                    else
                    {
                        string syncStatusAIDO       = "";
                        string syncMessageAIDO      = "";
                        string result               = "";
                        string JsonResponse         = "";
                        string ErrMsg               = "";
                        List<ItemPrice> itemPrices  = new List<ItemPrice>();
                        itemPrices                  = IUnitOfWorks.UnitOfWorkAidoSync().GetItemSync(model.header.OrganizationId, model.header.PatientId, model.header.Admissionid, model.header.EncounterId, model.prescription, DeliveryFee, model.header.IsEditDrug, model.header.IsEditConsumables);

                        //HIT API AIDO -> GET STATUS & MESSAGE
                        AidoRequestModel aido       = new AidoRequestModel();
                        Guid SiloamTrxId            = new Guid();
                        SiloamTrxId                 = IUnitOfWorks.UnitOfWorkAidoDrug().GetSiloamTrxId(model.header.OrganizationId, model.header.PatientId, model.header.Admissionid, model.header.EncounterId);
                        if (SiloamTrxId == Guid.Empty)
                        {
                            SiloamTrxId             = Guid.NewGuid();
                        }
                        aido.requiredDate           = DateTime.Now;
                        aido.shippedDate            = DateTime.Now;
                        aido.totalPrice             = int.Parse(itemPrices.First().TotalPrice.ToString());
                        aido.siloamTrxId            = SiloamTrxId;
                        List<AidoDrug> drug         = new List<AidoDrug>();
                        drug = (from a in itemPrices
                                select new AidoDrug
                                {
                                    name = a.SalesItemName,
                                    qty = a.quantity,
                                    uom = a.Uom,
                                    price = a.SubTotal
                                }).ToList();
                        aido.items = drug;

                        string JsonStringAido   = JsonConvert.SerializeObject(aido);
                        string token            = IUnitOfWorks.UnitOfWorkAidoSync().GenerateJSONWebToken(model.header.OrganizationId, model.header.PatientId, model.header.Admissionid, model.header.DoctorId);
                        result                  = IUnitOfWorks.UnitOfWorkAidoSync().AidoSyncPrescription(JsonStringAido, model.header.OrganizationId, model.header.PatientId, model.header.Admissionid, model.header.DoctorId, token);
                        if (!result.ToUpper().Contains("KONEKSI KE AIDO GAGAL"))
                        {
                            JObject Response = (JObject)JsonConvert.DeserializeObject<dynamic>(result);
                            syncStatusAIDO = Response.Property("status").Value.ToString();
                            syncMessageAIDO = Response.Property("message").Value.ToString();
                            ErrMsg = Response.Property("message").Value.ToString();
                            JsonResponse = result;
                        }
                        else
                        {
                            //if exception or timeout
                            syncStatusAIDO = "FAIL";
                            syncMessageAIDO = "KONEKSI KE AIDO GAGAL";
                            ErrMsg = result;
                        }

                        if (syncStatusAIDO.ToLower() == "ok")
                        {
                            string insertaidoticket = IUnitOfWorks.UnitOfWorkAidoDrug().InsertData(model.header.OrganizationId, model.header.PatientId, model.header.Admissionid, model.header.EncounterId, JsonStringAido, JsonResponse, SiloamTrxId, model.header.ChannelId);
                            string data = IUnitOfWorks.UnitOfWorkPharmacy().UpdateRecordSubmit(SubmitBy, transAdmId, transAdmNo, QueueNo, DeliveryFee, PayerCoverage, model);
                            messageHubContexts.Clients.All.InvokeAsync("Update Submit Data", data);
                            HttpResults = new ResponseData<string>("Data successfully updated", Siloam.System.Web.StatusCode.OK, StatusMessage.Success, data + "|SUCCESS|", page, total);
                        }
                        else
                        {
                            string data = IUnitOfWorks.UnitOfWorkPharmacy().UpdateRecordSubmit(SubmitBy, transAdmId, transAdmNo, QueueNo, DeliveryFee, PayerCoverage, model);
                            string insertaidofailed = IUnitOfWorks.UnitOfWorkAidoSync().InsertDataLogFailed(model.header.OrganizationId, model.header.PatientId, model.header.Admissionid, model.header.EncounterId, token, JsonStringAido, JsonResponse, ErrMsg, model.header.ChannelId);
                            messageHubContexts.Clients.All.InvokeAsync("Update Submit Data", data);
                            HttpResults = new ResponseData<string>("Data successfully updated", Siloam.System.Web.StatusCode.OK, StatusMessage.Success, data + "|AIDOFAILED|" + syncMessageAIDO, page, total);
                        }
                    }
                }
                else if (model.header.ChannelId == "5" || model.header.ChannelId == "9" || mysiloampayerid.Contains(pharmacypayerid))
                {
                    int CountMySiloam = IUnitOfWorks.UnitOfWorkAidoDrug().GetCountAIDOOrder(model.header.OrganizationId, model.header.PatientId, model.header.Admissionid, model.header.EncounterId);
                    if (CountMySiloam > 0)
                    {
                        HttpResults = new ResponseData<string>("Data unsuccessfully updated", Siloam.System.Web.StatusCode.OK, StatusMessage.Success, "|MYSILOAMFAILED|ALREADY VERIFIED AND SENT TO MYSILOAM", page, total);
                    }
                    else
                    {
                        //MYSILOAM
                        string syncStatusMySiloam = "";
                        string syncMessageMySiloam = "";
                        string resultMySiloam = "";
                        string JsonResponseMySiloam = "";
                        string ErrMsgMySiloam = "";
                        List<ItemPrice> itemPrices = new List<ItemPrice>();
                        itemPrices = IUnitOfWorks.UnitOfWorkAidoSync().GetItemSyncTeleconsultation(model.header.OrganizationId, model.header.PatientId, model.header.Admissionid, model.header.EncounterId, model.prescription, DeliveryFee, model.header.IsEditDrug, model.header.IsEditConsumables, PayerCoverage, IsDefaultCoverage);

                        //HIT API AIDO -> GET STATUS & MESSAGE
                        MySiloamRequestDrug silo = new MySiloamRequestDrug();
                        //string value = itemPrices.First().PatientNetTotal;
                        //int asdf2 = Convert.ToInt32(Math.Floor(Convert.ToDouble(value)));
                        silo.totalPatientNet = Convert.ToInt32(Math.Floor(Convert.ToDouble(itemPrices.First().PatientNetTotal.ToString())));
                        silo.totalPayerNet = Convert.ToInt32(Math.Floor(Convert.ToDouble(itemPrices.First().PayerNetTotal.ToString())));
                        silo.payerCoverage = Convert.ToInt32(Math.Floor(Convert.ToDouble(PayerCoverage)));
                        if (mysiloampayerid.Contains(pharmacypayerid))
                        {
                            silo.totalPrice = int.Parse(itemPrices.First().TotalPrice.ToString());
                            silo.totalPatientNet = silo.totalPrice;
                        }
                        else
                        {
                            if (silo.totalPayerNet < silo.payerCoverage && silo.totalPayerNet != 0)
                            {
                                HttpResults = new ResponseData<string>("Data successfully updated", Siloam.System.Web.StatusCode.OK, StatusMessage.Success, "FAIL|MYSILOAMFAILED|COVERAGE BIGGER THAN TOTAL PAYER NET " + silo.totalPayerNet.ToString(), page, total);
                            }
                            silo.totalPrice = silo.totalPayerNet - silo.payerCoverage + silo.totalPatientNet;
                        }
                        silo.isSelfCollection = model.header.IsSelfCollection;
                        List<MySiloamDrug> drug = new List<MySiloamDrug>();
                        drug = (from a in itemPrices
                                select new MySiloamDrug
                                {
                                    itemId = a.SalesItemId,
                                    name = a.SalesItemName,
                                    qty = a.quantity,
                                    uom = a.Uom,
                                    patientNet = a.PatientNet,
                                    payerNet = a.PayerNet,
                                    frequency = a.Frequency,
                                    instruction = a.Instruction
                                }).ToList();
                        Guid TrxId = new Guid();
                        TrxId = IUnitOfWorks.UnitOfWorkAidoDrug().GetSiloamTrxId(model.header.OrganizationId, model.header.PatientId, model.header.Admissionid, model.header.EncounterId);
                        if (TrxId == Guid.Empty)
                        {
                            TrxId = Guid.NewGuid();
                        }
                        silo.items = drug;
                        silo.admissionHopeId = model.header.Admissionid.ToString();
                        silo.userId = SubmitBy.ToString();
                        silo.userName = UserName;
                        silo.source = "EMR";
                        silo.siloamTrxId = TrxId;
                        silo.appointmentId = IUnitOfWorks.UnitOfWorkPharmacy().GetAppointmentId(model.header.OrganizationId, model.header.PatientId, model.header.Admissionid, model.header.DoctorId);

                        string JsonStringMySiloam = JsonConvert.SerializeObject(silo);
                        //string token = IUnitOfWorks.UnitOfWorkAidoSync().GenerateJSONWebToken(model.header.OrganizationId, model.header.PatientId, model.header.Admissionid, model.header.DoctorId);
                        resultMySiloam = IUnitOfWorks.UnitOfWorkSync().SyncDrugMySiloam(JsonStringMySiloam);
                        if (!resultMySiloam.ToUpper().Contains("KONEKSI KE MYSILOAM GAGAL"))
                        {
                            JObject ResponseSilo = (JObject)JsonConvert.DeserializeObject<dynamic>(resultMySiloam);
                            syncStatusMySiloam = ResponseSilo.Property("status").Value.ToString();
                            syncMessageMySiloam = ResponseSilo.Property("message").Value.ToString();
                            ErrMsgMySiloam = ResponseSilo.Property("message").Value.ToString();
                            JsonResponseMySiloam = resultMySiloam;
                        }
                        else
                        {
                            //if exception or timeout
                            syncStatusMySiloam = "FAIL";
                            syncMessageMySiloam = "KONEKSI KE MYSILOAM GAGAL";
                            ErrMsgMySiloam = resultMySiloam;
                        }

                        if (syncStatusMySiloam.ToLower() == "ok")
                        {
                            string insertaidoticket = IUnitOfWorks.UnitOfWorkAidoDrug().InsertData(model.header.OrganizationId, model.header.PatientId, model.header.Admissionid, model.header.EncounterId, JsonStringMySiloam, JsonResponseMySiloam, TrxId, model.header.ChannelId);
                            string data = IUnitOfWorks.UnitOfWorkPharmacy().UpdateRecordSubmit(SubmitBy, transAdmId, transAdmNo, QueueNo, DeliveryFee, PayerCoverage, model);
                            messageHubContexts.Clients.All.InvokeAsync("Update Submit Data", data);
                            HttpResults = new ResponseData<string>("Data successfully updated", Siloam.System.Web.StatusCode.OK, StatusMessage.Success, data + "|SUCCESS|", page, total);
                        }
                        else
                        {
                            string insertaidofailed = IUnitOfWorks.UnitOfWorkAidoSync().InsertDataLogFailed(model.header.OrganizationId, model.header.PatientId, model.header.Admissionid, model.header.EncounterId, "", JsonStringMySiloam, JsonResponseMySiloam, ErrMsgMySiloam, model.header.ChannelId);
                            string data = IUnitOfWorks.UnitOfWorkPharmacy().UpdateRecordSubmit(SubmitBy, transAdmId, transAdmNo, QueueNo, DeliveryFee, PayerCoverage, model);
                            messageHubContexts.Clients.All.InvokeAsync("Update Submit Data", data);
                            HttpResults = new ResponseData<string>("Data successfully updated", Siloam.System.Web.StatusCode.OK, StatusMessage.Success, data + "|MYSILOAMFAILED|" + syncMessageMySiloam, page, total);
                        }
                    }
                }
                else
                {
                    usedosetext = IUnitOfWorks.UnitOfWorkPharmacy().GetSettingDoseText(model.header.OrganizationId);
                    var processStart = IUnitOfWorks.UnitOfWorkPharmacy().InsertToTimeStampTable(model.header.EncounterId, "PHARMACY-CALL API SUBMIT");
                    //from API HOPE
                    
                    string syncStatus = "";
                    string syncMessage = "";

                    int tempcount = model.prescription.Count(p => p.is_consumables == 1);

                    model.prescription.ForEach(p => p.prescription_sync_id = Guid.NewGuid());
                    model.compound_header.ForEach(p => p.compound_header_sync_id = Guid.NewGuid());
                    model.compound_detail.ForEach(p => p.compound_detail_sync_id = Guid.NewGuid());
                    
                    SyncPrescriptionItemIssue presItemIssue = new SyncPrescriptionItemIssue();
                    List<itemIssue> items = new List<itemIssue>();
                    List<itemIssue> temp = new List<itemIssue>();

                    temp = IUnitOfWorks.UnitOfWorkPharmacy().GetAdditionalItemIssue(model.header.OrganizationId, model.header.Admissionid, model.header.EncounterId, 1);
                    items = (from a in model.prescription
                             where decimal.Parse(a.IssuedQty) > 0
                             orderby a.item_sequence
                             select new itemIssue
                             {
                                 itemId = a.item_id,
                                 quantity = decimal.Parse(a.IssuedQty),
                                 uomid = a.uom_id
                             }).ToList();
                    if (temp != null)
                    {
                        items.AddRange(temp);
                    }

                    //SYNC DRUG TO HOPE
                    if (items.Count() > 0)
                    {
                        itemIssued test = new itemIssued();
                        test.itemIssue = items;

                        presItemIssue.MedicalOrderInterfaceId = Guid.Empty;
                        presItemIssue.MedicalOrderId = null;
                        presItemIssue.MedicalEncounterInterfaceId = model.header.EncounterId;
                        presItemIssue.SalesPriorityId = 1;
                        presItemIssue.itemIssued = test;

                        string JsonString = JsonConvert.SerializeObject(presItemIssue);
                        var hopeStart = IUnitOfWorks.UnitOfWorkPharmacy().InsertToTimeStampTable(model.header.EncounterId, "PHARMACY-REQUEST API DRUG HOPE");
                        var syncItemIssue = IUnitOfWorks.UnitOfWorkSync().SyncPrescriptionItemIssue(model.header.OrganizationId, model.header.Admissionid, model.header.store_id, model.header.DoctorId, SubmitBy, JsonString);
                        var hopeFinish = IUnitOfWorks.UnitOfWorkPharmacy().InsertToTimeStampTable(model.header.EncounterId, "PHARMACY-RESPONSE API DRUG HOPE");

                        var jsonDeserializeItemIssue = JsonConvert.DeserializeObject<ReturnValueItemIssue>(syncItemIssue.ToString());
                        syncStatus = jsonDeserializeItemIssue.status;
                        syncMessage = jsonDeserializeItemIssue.message;

                        if (syncStatus.ToLower() == "success")
                        {

                            model.prescription = (from a in model.prescription
                                                  join b in jsonDeserializeItemIssue.data.itemIssue on a.item_id equals b.ItemId
                                                  into c
                                                  from b in c.DefaultIfEmpty()
                                                  select new PharmacyPrescription
                                                  {
                                                      administration_route_code = a.administration_route_code,
                                                      administration_route_id = a.administration_route_id,
                                                      compound_id = a.compound_id,
                                                      compound_name = a.compound_name,
                                                      doctor_name = a.doctor_name,
                                                      dosage_id = a.dosage_id,
                                                      dose_text = a.dose_text,
                                                      dose_uom = a.dose_uom,
                                                      dose_uom_id = a.dose_uom_id,
                                                      frequency_code = a.frequency_code,
                                                      frequency_id = a.frequency_id,
                                                      IssuedQty = a.IssuedQty,
                                                      is_consumables = a.is_consumables,
                                                      is_routine = a.is_routine,
                                                      item_id = a.item_id,
                                                      item_name = a.item_name,
                                                      item_sequence = a.item_sequence,
                                                      iteration = a.iteration,
                                                      MainStoreQuantity = a.MainStoreQuantity,
                                                      origin_prescription_id = a.origin_prescription_id,
                                                      prescription_id = a.prescription_id,
                                                      prescription_no = a.prescription_no,
                                                      prescription_sync_id = a.prescription_sync_id,
                                                      quantity = a.quantity,
                                                      remarks = a.remarks,
                                                      SubStoreQuantity = a.SubStoreQuantity,
                                                      uom_code = a.uom_code,
                                                      uom_id = a.uom_id,
                                                      hope_aritem_id = b.ArItemId == null ? 0 : long.Parse(b.ArItemId.ToString())
                                                  }).ToList();
                        }
                    }
                    else
                    {
                        syncStatus = "success";
                    }

                    string data = "";
                    if (syncStatus.ToLower() == "success")
                    {
                        data = IUnitOfWorks.UnitOfWorkPharmacy().UpdateRecordSubmitItemIssue(SubmitBy, transAdmId, transAdmNo, QueueNo, DeliveryFee, PayerCoverage, model);
                        string updateFlagHope = IUnitOfWorks.UnitOfWorkPharmacy().UpdateFlagHOPE(model.header.OrganizationId, model.header.Admissionid, model.header.EncounterId);
                        messageHubContexts.Clients.All.InvokeAsync("Update Submit Data", data);
                        HttpResults = new ResponseData<string>("Data successfully updated", Siloam.System.Web.StatusCode.OK, StatusMessage.Success, data + "|SUCCESS|", page, total);
                    }
                    
                    else if (syncStatus.ToLower() != "success" )
                    {
                        HttpResults = new ResponseData<string>(syncMessage, Siloam.System.Web.StatusCode.OK, StatusMessage.Fail, data + "|DRUGFAILED, " + syncMessage + "|" + syncStatus, page, total);
                    }
                    
                }
            }
            catch (Exception ex)
            {

                int exCode = ex.HResult;

                if (exCode == -2147467259)
                {

                    HttpResults = new ResponseMessage(Siloam.System.Web.StatusCode.InternalServerErrorException, StatusMessage.Error, ex.Message, total);
                    Task<string> slackNotification = SlackMessageAsync(ValueStorage.SlackUrl, ValueStorage.ApiName, ValueStorage.ApiUrl + "/pharmacyrecord", "[POST]Submit Verify Prescription", SubmitBy + "/" + jsonModel, ex.Message);

                }
                else
                {

                    HttpResults = new ResponseMessage(Siloam.System.Web.StatusCode.UnprocessableEntity, StatusMessage.Fail, ex.Message, total);
                    Task<string> slackNotification = SlackMessageAsync(ValueStorage.SlackUrl, ValueStorage.ApiName, ValueStorage.ApiUrl + "/pharmacyrecord", "[POST]Submit Verify Prescription", SubmitBy + "/" + jsonModel, ex.Message);

                }

            }

        response:
            return HttpResponse(HttpResults);

        }


        [HttpPost("submitprintpresscription/{UserName}/{QueueNo}/{DeliveryFee}/{PayerCoverage}/{SubmitBy:long}")]
        [ProducesResponseType(typeof(ResponseData<SubmitPrintPrescription>), 200)]
        public IActionResult SubmitPrintPresscription(string UserName, string QueueNo, string DeliveryFee, string PayerCoverage, Int64 SubmitBy, [FromBody] PharmacyData model)
        {
            int page = 1;
            int total = 0;
            string jsonModel            = JsonConvert.SerializeObject(model);
            string JsonSingleQWorklist  = JsonConvert.SerializeObject(model.singleQWorklistData);
            string usedosetext          = "";

            if (model == null)
            {

                HttpResults                     = new ResponseMessage(Siloam.System.Web.StatusCode.BadRequest, StatusMessage.Fail, HttpResponseMessageKey.DataUnsuccessfullyCreated, total);
                Task<string> slackNotification  = SlackMessageAsync(ValueStorage.SlackUrl, ValueStorage.ApiName, ValueStorage.ApiUrl + "/submitprintpresscription", "[POST]Submit Verify Prescription", SubmitBy + "/" + jsonModel, "Data Unsuccessfully Created");
                goto response;

            }

            try
            {
                long transAdmId             = model.header.Admissionid;
                string transAdmNo           = model.header.AdmissionNo;
                List<long> aidopayerid      = new List<long>();
                List<long> mysiloampayerid  = new List<long>();
                string payertemp            = "";
                payertemp                   = IUnitOfWorks.UnitOfWorkAidoDrug().GetSettingAidoPayerId(model.header.OrganizationId);
                aidopayerid                 = payertemp.Split(',').Select(Int64.Parse).ToList();

                payertemp                   = "";
                payertemp                   = IUnitOfWorks.UnitOfWorkAidoDrug().GetSettingMysiloamPayerId(model.header.OrganizationId);
                mysiloampayerid             = payertemp.Split(',').Select(Int64.Parse).ToList();

                long pharmacypayerid        = IUnitOfWorks.UnitOfWorkPharmacy().GetPayerIdRecord(model.header.OrganizationId, model.header.PatientId, model.header.Admissionid, model.header.EncounterId);


                usedosetext                 = IUnitOfWorks.UnitOfWorkPharmacy().GetSettingDoseText(model.header.OrganizationId);
                var processStart            = IUnitOfWorks.UnitOfWorkPharmacy().InsertToTimeStampTable(model.header.EncounterId, "PHARMACY-CALL API SUBMIT");
                //from API HOPE
                // HOPE
                bool isFlagHope = true;
                string syncHopeStatus = "";
                string syncHopeMessage = "";
                string syncStatusCons = "";
                string syncStatusDrugs = "";
                string syncStatus           = "";
                //SignleQueue
                string dataWorklistSingleQ = "SIGNLEQSUCCESS";
                string syncSQStatus = "OK";
                string syncSQMessage = "OK";
                int tempcount               = model.prescription.Count(p => p.is_consumables == 1);

                model.prescription.ForEach(p => p.prescription_sync_id = Guid.NewGuid());
                model.compound_header.ForEach(p => p.compound_header_sync_id = Guid.NewGuid());
                model.compound_detail.ForEach(p => p.compound_detail_sync_id = Guid.NewGuid());
                SyncPrescription pres       = new SyncPrescription();
                SingleQueue model_SQ        = new SingleQueue();
                List<OrderItem> items       = new List<OrderItem>();
                List<OrderItem> temp        = new List<OrderItem>();

                temp    = IUnitOfWorks.UnitOfWorkPharmacy().GetAdditional(model.header.OrganizationId, model.header.Admissionid, model.header.EncounterId, 1, usedosetext);
                items   = (from a in model.prescription
                             where a.is_consumables == 0 && decimal.Parse(a.IssuedQty) > 0
                             orderby a.item_sequence
                             select new OrderItem
                             {
                                 AdministrationFrequencyId  = a.frequency_id,
                                 AdministrationInstruction  = a.remarks,
                                 AdministrationRouteId      = a.administration_route_id,
                                 DispensingInstruction      = "",
                                 Dose = a.dose_text != "" && usedosetext == "TRUE" ? "0.0001" : a.dosage_id,
                                 DoseText = a.dose_text,
                                 DoseUomId = a.dose_uom_id,
                                 DrugId = a.item_id,
                                 IsPrn = false,
                                 PatientInformation = "",
                                 Quantity = a.IssuedQty,
                                 Repeat = a.iteration,
                                 MedicalEncounterEntryInterfaceId = a.prescription_sync_id
                             }).ToList();
                if (temp != null)
                {
                    items.AddRange(temp);
                }


                //SYNC DRUG TO HOPE//SYNC DRUG TO HOPE
                if (items.Count() > 0)
                {
                    pres.MedicalOrderInterfaceId        = Guid.Empty;
                    pres.MedicalOrderId                 = null;
                    pres.MedicalEncounterInterfaceId    = model.header.EncounterId;
                    pres.SalesPriorityId                = 1;
                    pres.Notes                          = model.header.PharmacyNotes;
                    pres.OrderItems                     = items;

                    string JsonString                   = JsonConvert.SerializeObject(pres);
                    var hopeStart                       = IUnitOfWorks.UnitOfWorkPharmacy().InsertToTimeStampTable(model.header.EncounterId, "PHARMACY-REQUEST API DRUG HOPE");
                    syncStatus                          = IUnitOfWorks.UnitOfWorkSync().SyncPrescription(model.header.OrganizationId, model.header.Admissionid, JsonString);
                    var hopeFinish                      = IUnitOfWorks.UnitOfWorkPharmacy().InsertToTimeStampTable(model.header.EncounterId, "PHARMACY-RESPONSE API DRUG HOPE");
                }
                else
                {
                    syncStatus = "success";
                }

                //SYNC CONSUMABLE TO HOPE
                if (model.prescription.Count(p => p.is_consumables == 1) > 0)
                {
                    SyncConsumables cons                = new SyncConsumables();
                    List<OrderItemConsumables> itemcons = new List<OrderItemConsumables>();
                    List<OrderItemConsumables> tempcons = new List<OrderItemConsumables>();
                    tempcons = IUnitOfWorks.UnitOfWorkPharmacy().GetAdditionalConsumables(model.header.OrganizationId, model.header.Admissionid, model.header.EncounterId, 1);
                    itemcons = (from a in model.prescription
                                where a.is_consumables == 1 && decimal.Parse(a.IssuedQty) > 0
                                orderby a.item_sequence
                                select new OrderItemConsumables
                                {
                                    UsageInstruction = a.remarks,
                                    ItemId = a.item_id,
                                    PatientInformation = "",
                                    Quantity = a.IssuedQty,
                                    DispensingInstruction = "",
                                    MedicalEncounterEntryInterfaceId = a.prescription_sync_id
                                }).ToList();
                    if (tempcons != null)
                    {
                        itemcons.AddRange(tempcons);
                    }
                    cons.MedicalOrderInterfaceId = Guid.Empty;
                    cons.MedicalOrderId = null;
                    cons.MedicalEncounterInterfaceId = model.header.EncounterId;
                    cons.SalesPriorityId = 1;
                    cons.Notes = model.header.PharmacyNotes;
                    cons.OrderItems = itemcons;

                    string JsonStringCons = JsonConvert.SerializeObject(cons);
                    syncStatusCons = IUnitOfWorks.UnitOfWorkSync().SyncConsumables(model.header.OrganizationId, model.header.Admissionid, JsonStringCons);
                }
                else
                {
                    syncStatusCons = "success";
                }

                if (!model.header.is_tele && model.header.is_SingleQueue)
                {
                    if (JsonSingleQWorklist != null)
                    {
                        dataWorklistSingleQ         = IUnitOfWorks.UnitOfWorkSingleQueue().SyncWorklistSingleQ(JsonSingleQWorklist);
                        var objResult               = JsonConvert.DeserializeObject<ResultListSingleQueue>(dataWorklistSingleQ);
                        syncSQStatus                = objResult.status;
                        syncSQMessage               = objResult.message;
                        if (syncSQStatus.Equals("OK"))
                        {
                            model_SQ                = objResult.data[0];
                        }
                    }
                    else
                    {
                        dataWorklistSingleQ = "SIGNLEQDATANULL";
                        syncSQStatus        = "Fail";
                        syncSQMessage       = "SingleQueue Worklist Null";
                    }
                }
                Param_RecordSubmit paramRS          = new Param_RecordSubmit();
                paramRS.SubmitBy                    = SubmitBy;
                paramRS.TransAdmId                  = transAdmId;
                paramRS.TransAdmNo                  = transAdmNo;
                paramRS.QueueNo                     = QueueNo;
                paramRS.DeliveryFee                 = DeliveryFee;
                paramRS.PayerCoverage               = PayerCoverage;
                paramRS.ResponseWorklistSingleQueue = dataWorklistSingleQ;
                paramRS.PharmacyDataModel           = model;
                paramRS.SingleQueueDataModel        = model_SQ;

                Param_SyncResult paramSyncResultmodel = new Param_SyncResult();
                SubmitPrintPrescription pressResultData        = new SubmitPrintPrescription();
                //string syncSQStatus                 = status_SQ + "|" + message_SQ ;
    
                if (syncStatusDrugs.ToLower() == "success" && syncStatusCons.ToLower() == "success")
                {
                    syncHopeStatus      = "SUCCESS";
                    syncHopeMessage     = "SUCCESS";
                }
                else if (syncStatusDrugs.ToLower() == "success" && syncStatusCons.ToLower() != "success")
                {
                    syncHopeStatus      = "CONSUMABLEFAILED";
                    syncHopeMessage     = syncStatusCons;
                }
                else if (syncStatusDrugs.ToLower() != "success" && syncStatusCons.ToLower() == "success")
                {
                    syncHopeStatus      = "DRUGFAILED";
                    syncHopeMessage     = syncStatusDrugs;
                }
                else
                {
                    isFlagHope          = false;
                    syncHopeStatus      = "HOPEFAILED";
                    syncHopeMessage     = "Drug Failed: " + syncStatusDrugs + ", Consumable Failed: " + syncStatusCons;
                }

                paramSyncResultmodel.HopeStatusResult       = syncHopeStatus;
                paramSyncResultmodel.HopeMessageResult      = syncHopeMessage;
                paramSyncResultmodel.SingleQStatusResult    = syncSQStatus;
                paramSyncResultmodel.SingleQMessageResult   = syncSQMessage;

                if (isFlagHope)
                {
                    pressResultData = IUnitOfWorks.UnitOfWorkPharmacy().submitPrintPrescriptionSQ(paramRS, paramSyncResultmodel);
                    string updateFlagHope = IUnitOfWorks.UnitOfWorkPharmacy().UpdateFlagHOPE(model.header.OrganizationId, model.header.Admissionid, model.header.EncounterId);
                    messageHubContexts.Clients.All.InvokeAsync("Update Submit Data", pressResultData.SubmitStatusPressResult);
                    HttpResults = new ResponseData<SubmitPrintPrescription>("Data successfully updated and Get Data Pharmacy Print", Siloam.System.Web.StatusCode.OK, StatusMessage.Success, pressResultData, page, total);
                }
                else
                {
                   
                    pressResultData = IUnitOfWorks.UnitOfWorkPharmacy().submitPrintPrescriptionSQ(paramRS, paramSyncResultmodel);
                    messageHubContexts.Clients.All.InvokeAsync("Update Submit Data", pressResultData.SubmitStatusPressResult);
                    HttpResults = new ResponseData<SubmitPrintPrescription>("Data successfully updated and Get Data Pharmacy Print", Siloam.System.Web.StatusCode.OK, StatusMessage.Success, pressResultData, page, total);
                }
            }
            catch (Exception ex)
            {

                int exCode = ex.HResult;

                if (exCode == -2147467259)
                {

                    HttpResults = new ResponseMessage(Siloam.System.Web.StatusCode.InternalServerErrorException, StatusMessage.Error, ex.Message, total);
                    Task<string> slackNotification = SlackMessageAsync(ValueStorage.SlackUrl, ValueStorage.ApiName, ValueStorage.ApiUrl + "/submitprintpresscription", "[POST]Submit Verify Prescription", SubmitBy + "/" + jsonModel, ex.Message);

                }
                else
                {

                    HttpResults = new ResponseMessage(Siloam.System.Web.StatusCode.UnprocessableEntity, StatusMessage.Fail, ex.Message, total);
                    Task<string> slackNotification = SlackMessageAsync(ValueStorage.SlackUrl, ValueStorage.ApiName, ValueStorage.ApiUrl + "/submitprintpresscription", "[POST]Submit Verify Prescription", SubmitBy + "/" + jsonModel, ex.Message);

                }

            }

        response:
            return HttpResponse(HttpResults);

        }
        [HttpPost("submitprintpresscriptionpharmacy/{QueueNo}/{DeliveryFee}/{PayerCoverage}/{SubmitBy:long}")]
        [ProducesResponseType(typeof(ResponseData<SubmitPrintPrescription>), 200)]
        public IActionResult SubmitPrintPresscriptionPharmacy(string QueueNo, string DeliveryFee, string PayerCoverage, Int64 SubmitBy, [FromBody] PharmacyData model)
        {
            int page                    = 1;
            int total                   = 0;
            string jsonModel            = JsonConvert.SerializeObject(model);
            string JsonSingleQWorklist  = JsonConvert.SerializeObject(model.singleQWorklistData);
            string usedosetext          = "";

            try
            {
                LogSyncToHope logSyncDrugToHope = new LogSyncToHope();
                SubmitPrintPrescription pressResultData     = new SubmitPrintPrescription();
                Param_SyncResult paramSyncResultmodel       = new Param_SyncResult();
                Param_RecordSubmit paramRS                  = new Param_RecordSubmit();
                SingleQueue model_SQ                        = new SingleQueue();

                //Payer
                List<long> aidopayerid                      = new List<long>();
                List<long> mysiloampayerid                  = new List<long>();
                string payertemp                            = "";

                // HOPE
                bool isFlagHope                             = true;
                string syncHopeStatus                       = "unsuccess";
                string syncHopeMessage                      = "no return data";
                int syncHopeCode                            = 0;
                List<DrugItem> syncHopeData                 = new List<DrugItem>();

                //SignleQueue
                string dataWorklistSingleQ                  = "SIGNLEQSUCCESS";
                string syncSQStatus                         = "OK";
                string syncSQMessage                        = "OK";

                if (model == null)
                {
                    pressResultData.SubmitStatusPressResult = "PharmacyData is null";
                    pressResultData.HopeStatusResult        = "PharmacyData is null";
                    pressResultData.HopeMessageResult       = "PharmacyData is null";
                    pressResultData.HopedataResult          = syncHopeData;
                    pressResultData.SingleQStatusResult     = "PharmacyData is null";
                    pressResultData.SingleQMessageResult    = "PharmacyData is null";
                    HttpResults = new ResponseData<SubmitPrintPrescription>("Data UnSuccessfully updated and Get Data Pharmacy Print", Siloam.System.Web.StatusCode.UnprocessableEntity, StatusMessage.Fail, pressResultData, page, total);
                    Task<string> slackNotification = SlackMessageAsync(ValueStorage.SlackUrl, ValueStorage.ApiName, ValueStorage.ApiUrl + "/submitprintpresscriptionpharmacy", "[POST]Submit Verify Prescription and Item Issue", SubmitBy + "/" + jsonModel, "Data Unsuccessfully Created");
                    goto response;
                }

                Nullable<DateTime>  verifiedTime = IUnitOfWorks.UnitOfWorkPharmacy().GetPresscriptionVerifyDate(model.header.OrganizationId,model.header.PatientId,model.header.Admissionid,model.header.EncounterId);
                if (verifiedTime != null)
                {
                    pressResultData.SubmitStatusPressResult = "Prescription already verified";
                    pressResultData.HopeStatusResult = "Prescription already verified";
                    pressResultData.HopeMessageResult = "Prescription already verified";
                    pressResultData.HopedataResult = syncHopeData;
                    pressResultData.SingleQStatusResult = "Prescription already verified";
                    pressResultData.SingleQMessageResult = "Prescription already verified";
                    HttpResults = new ResponseData<SubmitPrintPrescription>("Data UnSuccessfully updated and Get Data Pharmacy Print", Siloam.System.Web.StatusCode.UnprocessableEntity, StatusMessage.Fail, pressResultData, page, total);
                    Task<string> slackNotification = SlackMessageAsync(ValueStorage.SlackUrl, ValueStorage.ApiName, ValueStorage.ApiUrl + "/submitprintpresscriptionpharmacy", "[POST]Submit Verify Prescription and Item Issue", SubmitBy + "/" + jsonModel, "Prescription already verified");
                    goto response;
                }
                

                long transAdmId             = model.header.Admissionid;
                string transAdmNo           = model.header.AdmissionNo;


                payertemp                   = IUnitOfWorks.UnitOfWorkAidoDrug().GetSettingAidoPayerId(model.header.OrganizationId);
                aidopayerid                 = payertemp.Split(',').Select(Int64.Parse).ToList();

                payertemp                   = "";
                payertemp                   = IUnitOfWorks.UnitOfWorkAidoDrug().GetSettingMysiloamPayerId(model.header.OrganizationId);
                mysiloampayerid             = payertemp.Split(',').Select(Int64.Parse).ToList();

                long pharmacypayerid        = IUnitOfWorks.UnitOfWorkPharmacy().GetPayerIdRecord(model.header.OrganizationId, model.header.PatientId, model.header.Admissionid, model.header.EncounterId);
                usedosetext                 = IUnitOfWorks.UnitOfWorkPharmacy().GetSettingDoseText(model.header.OrganizationId);
                var processStart            = IUnitOfWorks.UnitOfWorkPharmacy().InsertToTimeStampTable(model.header.EncounterId, "PHARMACY-CALL API SUBMIT");




                //int tempcount                   = model.prescription.Count(p => p.is_consumables == 1);
                model.prescription.ForEach(p    => p.prescription_sync_id = Guid.NewGuid());
                model.compound_header.ForEach(p => p.compound_header_sync_id = Guid.NewGuid());
                model.compound_detail.ForEach(p => p.compound_detail_sync_id = Guid.NewGuid());
                model.additionalItem.ForEach(p => p.additional_item_sync_id = Guid.NewGuid());

                if (model.header.is_SendDataItemIssue)
                {
                    SyncPrescriptionItemIssue presItemIssue = new SyncPrescriptionItemIssue();
                    List <DrugItem> listDrugItem            = new List<DrugItem>();
                    List<itemIssue> items                   = new List<itemIssue>();
                    List<itemIssue> compoundItems           = new List<itemIssue>();
                    List<itemIssue> additionalItems          = new List<itemIssue>();
                    logSyncDrugToHope.startTime                     = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"));
                    logSyncDrugToHope.organization_hope_id          = model.header.OrganizationId;
                    logSyncDrugToHope.admission_hope_id             = model.header.Admissionid;
                    logSyncDrugToHope.store_hope_id                 = model.header.store_id;
                    logSyncDrugToHope.doctor_hope_id                = model.header.DoctorId;
                    logSyncDrugToHope.user_hope_id                  = SubmitBy;
                    logSyncDrugToHope.admission_hope_id_SentHope    = model.header.Admissionid_SentHope;

                    //List<itemIssue> temp                    = new List<itemIssue>();
                    //temp = IUnitOfWorks.UnitOfWorkPharmacy().GetAdditionalItemIssue(model.header.OrganizationId, model.header.Admissionid, model.header.EncounterId, 1);
                    items = (from a in model.prescription
                             where decimal.Parse(a.IssuedQty) > 0 && a.is_SentHope
                             orderby a.item_sequence
                             select new itemIssue
                             {
                                 itemId         = a.item_id,
                                 quantity       = decimal.Parse(a.IssuedQty),
                                 uomid          = a.uom_id
                             }).ToList();

                    compoundItems = (from a in model.compound_detail
                                     where decimal.Parse(a.IssuedQty == null ? "0" : a.IssuedQty) > 0 && a.is_SentHope
                                     orderby a.item_sequence
                                     select new itemIssue
                                     {
                                         itemId         = a.item_id,
                                         quantity       = decimal.Parse(a.IssuedQty),
                                         uomid          = a.uom_id
                                     }).ToList();

                    additionalItems = (from a in model.additionalItem
                                     where decimal.Parse(a.issued_qty == null ? "0" : a.issued_qty) > 0
                                     orderby a.item_sequence
                                     select new itemIssue
                                     {
                                         itemId = a.item_id,
                                         quantity = decimal.Parse(a.issued_qty),
                                         uomid = a.uom_id
                                     }).ToList();

                    //if (temp != null)
                    //{
                    //    items.AddRange(temp);
                    //}

                    if (compoundItems != null 
                        && compoundItems.Count>0)
                    {
                        items.AddRange(compoundItems);
                    }

                    if (additionalItems != null
                        && additionalItems.Count > 0)
                    {
                        items.AddRange(additionalItems);
                    }

                    //SYNC DRUG TO HOPE
                    if (items.Count() > 0)
                    {
                        
                        itemIssued itemissue = new itemIssued();
                        itemissue.itemIssue  = items;

                        presItemIssue.MedicalOrderInterfaceId       = Guid.Empty;
                        presItemIssue.MedicalOrderId                = null;
                        presItemIssue.MedicalEncounterInterfaceId   = model.header.EncounterId;
                        presItemIssue.SalesPriorityId               = 1;
                        presItemIssue.itemIssued                    = itemissue;

                        string JsonString               = JsonConvert.SerializeObject(presItemIssue);
                        logSyncDrugToHope.jsonrequest_senditemissue = JsonString;

                        
                        //var hopeStart       = IUnitOfWorks.UnitOfWorkPharmacy().InsertToTimeStampTable(model.header.EncounterId, "PHARMACY-REQUEST ITEM ISSUE API DRUG HOPE");
                        var syncItemIssue               = IUnitOfWorks.UnitOfWorkSync().SyncPrescriptionToHope(model.header.OrganizationId, model.header.Admissionid_SentHope, model.header.store_id, model.header.DoctorId, SubmitBy, JsonString);
                        //var hopeFinish      = IUnitOfWorks.UnitOfWorkPharmacy().InsertToTimeStampTable(model.header.EncounterId, "PHARMACY-RESPONSE ITEM ISSUE API DRUG HOPE");
                        var jsonDeserializeItemIssue    = JsonConvert.DeserializeObject<DrugItemResponse>(syncItemIssue.ToString());
                                                
                        if (jsonDeserializeItemIssue != null)
                        {
                            syncHopeStatus = jsonDeserializeItemIssue.status;
                            syncHopeMessage = jsonDeserializeItemIssue.message;
                            syncHopeCode = jsonDeserializeItemIssue.code;
                            logSyncDrugToHope.jsonresponse_sendItemIssue = JsonConvert.SerializeObject(jsonDeserializeItemIssue);
                            if (jsonDeserializeItemIssue.data.Count > 0)
                            {
                                syncHopeData = jsonDeserializeItemIssue.data;
                                listDrugItem = jsonDeserializeItemIssue.data;
                            }
                        }
                        else
                        {
                            logSyncDrugToHope.jsonresponse_sendItemIssue = "json Deserialize Prescription Item Issue returns null data";
                        }

                        if (syncHopeCode == 200)
                        {
                            model.prescription = (from a in model.prescription
                                                  join b in listDrugItem on a.item_id equals b.ItemId
                                                  into c
                                                  from b in c.DefaultIfEmpty()
                                                  select new PharmacyPrescription
                                                  {
                                                      administration_route_code     = a.administration_route_code,
                                                      administration_route_id       = a.administration_route_id,
                                                      compound_id                   = a.compound_id,
                                                      compound_name                 = a.compound_name,
                                                      doctor_name                   = a.doctor_name,
                                                      dosage_id                     = a.dosage_id,
                                                      dose_text                     = a.dose_text,
                                                      dose_uom                      = a.dose_uom,
                                                      dose_uom_id                   = a.dose_uom_id,
                                                      frequency_code                = a.frequency_code,
                                                      frequency_id                  = a.frequency_id,
                                                      IssuedQty                     = a.IssuedQty,
                                                      is_consumables                = a.is_consumables,
                                                      is_routine                    = a.is_routine,
                                                      item_id                       = a.item_id,
                                                      item_name                     = a.item_name,
                                                      item_sequence                 = a.item_sequence,
                                                      iteration                     = a.iteration,
                                                      MainStoreQuantity             = a.MainStoreQuantity,
                                                      origin_prescription_id        = a.origin_prescription_id,
                                                      prescription_id               = a.prescription_id,
                                                      prescription_no               = a.prescription_no,
                                                      prescription_sync_id          = a.prescription_sync_id,
                                                      quantity                      = a.quantity,
                                                      remarks                       = a.remarks,
                                                      SubStoreQuantity              = a.SubStoreQuantity,
                                                      uom_code                      = a.uom_code,
                                                      uom_id                        = a.uom_id,
                                                      hope_aritem_id                = b == null ? 0 : long.Parse(b.ARItemId.ToString()),
                                                      is_SentHope                   = a.is_SentHope
                                                  }).ToList();

                            model.compound_detail = (from a in model.compound_detail
                                                        join b in listDrugItem on a.item_id equals b.ItemId
                                                        into c
                                                        from b in c.DefaultIfEmpty()
                                                          select new PharmacyCompoundDetail
                                                          {
                                                                prescription_compound_detail_id = a.prescription_compound_detail_id,
                                                                prescription_compound_header_id = a.prescription_compound_header_id,
                                                                item_id                         = a.item_id,
                                                                item_name                       = a.item_name,
                                                                quantity                        = a.quantity,
                                                                IssuedQty                       = a.IssuedQty == null ? "0" : a.IssuedQty,
                                                                uom_id                          = a.uom_id,
                                                                uom_code                        = a.uom_code,
                                                                administration_frequency_id     = a.administration_frequency_id,
                                                                administration_route_id         = a.administration_route_id,
                                                                item_sequence                   = a.item_sequence,
                                                                is_additional                   = a.is_additional,
                                                                SubStoreQuantity                = a.SubStoreQuantity,
                                                                MainStoreQuantity               = a.MainStoreQuantity,
                                                                compound_detail_sync_id         = a.compound_detail_sync_id,
                                                                dose                            = a.dose,
                                                                dose_uom_id                     = a.dose_uom_id,
                                                                dose_uom_code                   = a.dose_uom_code,
                                                                dose_text                       = a.dose_text,
                                                                IsDoseText                      = a.IsDoseText,
                                                                hope_aritem_id                  = b == null ? 0 : long.Parse(b.ARItemId.ToString()),
                                                                is_SentHope                     = a.is_SentHope
                                                          }).ToList();

                            model.additionalItem = (from a in model.additionalItem
                                                    join b in listDrugItem on a.item_id equals b.ItemId
                                                     into c
                                                     from b in c.DefaultIfEmpty()
                                                     select new PharmacyAdditionalItem
                                                     {
                                                         pharmacy_additional_item_id = a.pharmacy_additional_item_id,
                                                         item_id = a.item_id,
                                                         item_name = a.item_name,
                                                         quantity = a.quantity,
                                                         issued_qty = a.issued_qty,
                                                         uom_id = a.uom_id,
                                                         uom_code = a.uom_code,
                                                         hope_aritem_id = b == null ? 0 : long.Parse(b.ARItemId.ToString()),
                                                         item_sequence = a.item_sequence,
                                                         additional_item_sync_id = a.additional_item_sync_id
                                                     }).ToList();
                        }
                        else
                        { 
                            isFlagHope = false; 
                        }
                    }
                    else
                    {
                        string JsonString = JsonConvert.SerializeObject(presItemIssue);
                        logSyncDrugToHope.jsonrequest_senditemissue = JsonString;
                        isFlagHope = false;
                        syncHopeStatus = "success";
                        syncHopeCode = 200;
                        logSyncDrugToHope.jsonresponse_sendItemIssue = "there are no items that need to be sent to hope";
                    }

                    string data = IUnitOfWorks.UnitOfWorkSync().InsertLogItemsIssue(logSyncDrugToHope);
                }
                else
                {
                    SyncPrescription pres   = new SyncPrescription();
                    List<OrderItem> items   = new List<OrderItem>();
                    List<OrderItem> temp    = new List<OrderItem>();
                    string syncStatusCons   = "";
                    string syncStatusDrugs  = "";

                    temp = IUnitOfWorks.UnitOfWorkPharmacy().GetAdditional(model.header.OrganizationId, model.header.Admissionid, model.header.EncounterId, 1, usedosetext);
                    items = (from a in model.prescription
                             where a.is_consumables == 0 && decimal.Parse(a.IssuedQty) > 0
                             orderby a.item_sequence
                             select new OrderItem
                             {
                                 AdministrationFrequencyId          = a.frequency_id,
                                 AdministrationInstruction          = a.remarks,
                                 AdministrationRouteId              = a.administration_route_id,
                                 DispensingInstruction              = "",
                                 Dose                               = a.dose_text != "" && usedosetext == "TRUE" ? "0.0001" : a.dosage_id,
                                 DoseText                           = a.dose_text,
                                 DoseUomId                          = a.dose_uom_id,
                                 DrugId                             = a.item_id,
                                 IsPrn                              = false,
                                 PatientInformation                 = "",
                                 Quantity                           = a.IssuedQty,
                                 Repeat                             = a.iteration,
                                 MedicalEncounterEntryInterfaceId   = a.prescription_sync_id
                             }).ToList();
                    if (temp != null)
                    {
                        items.AddRange(temp);
                    }
                    model.compound_detail = (from a in model.compound_detail
                                             select new PharmacyCompoundDetail
                                             {
                                                 prescription_compound_detail_id    = a.prescription_compound_detail_id,
                                                 prescription_compound_header_id    = a.prescription_compound_header_id,
                                                 item_id                            = a.item_id,
                                                 item_name                          = a.item_name,
                                                 quantity                           = a.quantity,
                                                 IssuedQty                          = a.IssuedQty == null ? "0" : a.IssuedQty,
                                                 uom_id                             = a.uom_id,
                                                 uom_code                           = a.uom_code,
                                                 administration_frequency_id        = a.administration_frequency_id,
                                                 administration_route_id            = a.administration_route_id,
                                                 item_sequence                      = a.item_sequence,
                                                 is_additional                      = a.is_additional,
                                                 SubStoreQuantity                   = a.SubStoreQuantity,
                                                 MainStoreQuantity                  = a.MainStoreQuantity,
                                                 compound_detail_sync_id            = a.compound_detail_sync_id,
                                                 dose                               = a.dose,
                                                 dose_uom_id                        = a.dose_uom_id,
                                                 dose_uom_code                      = a.dose_uom_code,
                                                 dose_text                          = a.dose_text,
                                                 IsDoseText                         = a.IsDoseText
                                             }).ToList();
                    //SYNC DRUG TO HOPE//SYNC DRUG TO HOPE
                    if (items.Count() > 0)
                    {
                        pres.MedicalOrderInterfaceId                = Guid.Empty;
                        pres.MedicalOrderId                         = null;
                        pres.MedicalEncounterInterfaceId            = model.header.EncounterId;
                        pres.SalesPriorityId                        = 1;
                        pres.Notes                                  = model.header.PharmacyNotes;
                        pres.OrderItems                             = items;

                        string JsonString       = JsonConvert.SerializeObject(pres);
                        //var hopeStart           = IUnitOfWorks.UnitOfWorkPharmacy().InsertToTimeStampTable(model.header.EncounterId, "PHARMACY-REQUEST API DRUG HOPE");
                        syncStatusDrugs         = IUnitOfWorks.UnitOfWorkSync().SyncPrescription(model.header.OrganizationId, model.header.Admissionid, JsonString);
                        //var hopeFinish          = IUnitOfWorks.UnitOfWorkPharmacy().InsertToTimeStampTable(model.header.EncounterId, "PHARMACY-RESPONSE API DRUG HOPE");
                    }
                    else
                    {
                        syncStatusDrugs = "success";
                    }

                    //SYNC CONSUMABLE TO HOPE
                    if (model.prescription.Count(p => p.is_consumables == 1) > 0)
                    {
                        SyncConsumables cons = new SyncConsumables();
                        List<OrderItemConsumables> itemcons = new List<OrderItemConsumables>();
                        List<OrderItemConsumables> tempcons = new List<OrderItemConsumables>();
                        tempcons = IUnitOfWorks.UnitOfWorkPharmacy().GetAdditionalConsumables(model.header.OrganizationId, model.header.Admissionid, model.header.EncounterId, 1);
                        itemcons = (from a in model.prescription
                                    where a.is_consumables == 1 && decimal.Parse(a.IssuedQty) > 0
                                    orderby a.item_sequence
                                    select new OrderItemConsumables
                                    {
                                        UsageInstruction = a.remarks,
                                        ItemId = a.item_id,
                                        PatientInformation = "",
                                        Quantity = a.IssuedQty,
                                        DispensingInstruction = "",
                                        MedicalEncounterEntryInterfaceId = a.prescription_sync_id
                                    }).ToList();
                        if (tempcons != null)
                        {
                            itemcons.AddRange(tempcons);
                        }
                        cons.MedicalOrderInterfaceId = Guid.Empty;
                        cons.MedicalOrderId = null;
                        cons.MedicalEncounterInterfaceId = model.header.EncounterId;
                        cons.SalesPriorityId = 1;
                        cons.Notes = model.header.PharmacyNotes;
                        cons.OrderItems = itemcons;

                        string JsonStringCons = JsonConvert.SerializeObject(cons);
                        syncStatusCons = IUnitOfWorks.UnitOfWorkSync().SyncConsumables(model.header.OrganizationId, model.header.Admissionid, JsonStringCons);
                    }
                    else
                    {
                        syncStatusCons = "success";
                    }

                    if (syncStatusDrugs.ToLower() == "success" && syncStatusCons.ToLower() == "success")
                    {
                        syncHopeStatus = "SUCCESS";
                        syncHopeMessage = "SUCCESS";
                    }
                    else if (syncStatusDrugs.ToLower() == "success" && syncStatusCons.ToLower() != "success")
                    {
                        syncHopeStatus = "CONSUMABLEFAILED";
                        syncHopeMessage = syncStatusCons;
                    }
                    else if (syncStatusDrugs.ToLower() != "success" && syncStatusCons.ToLower() == "success")
                    {
                        syncHopeStatus = "DRUGFAILED";
                        syncHopeMessage = syncStatusDrugs;
                    }
                    else
                    {
                        isFlagHope = false;
                        syncHopeStatus = "HOPEFAILED";
                        syncHopeMessage = "Drug Failed: " + syncStatusDrugs + ", Consumable Failed: " + syncStatusCons;
                    }
                }


                if (!model.header.is_tele 
                    && model.header.is_SingleQueue
                    && (!model.header.is_SendDataItemIssue )
                        || (model.header.is_SendDataItemIssue 
                            && (syncHopeCode==200
                                 || syncHopeCode == 500)))
                {
                    if (JsonSingleQWorklist != null)
                    {
                        dataWorklistSingleQ = IUnitOfWorks.UnitOfWorkSingleQueue().SyncWorklistSingleQ(JsonSingleQWorklist);
                        var objResult = JsonConvert.DeserializeObject<ResultListSingleQueue>(dataWorklistSingleQ);
                        syncSQStatus = objResult.status;
                        syncSQMessage = objResult.message;
                        if (syncSQStatus.Equals("OK"))
                        {
                            model_SQ = objResult.data[0];
                        }
                    }
                    else
                    {
                        dataWorklistSingleQ = "SIGNLEQDATANULL";
                        syncSQStatus = "Fail";
                        syncSQMessage = "SingleQueue Worklist Null";
                    }
                }

                paramRS.SubmitBy                    = SubmitBy;
                paramRS.TransAdmId                  = transAdmId;
                paramRS.TransAdmNo                  = transAdmNo;
                paramRS.QueueNo                     = QueueNo;
                paramRS.DeliveryFee                 = DeliveryFee;
                paramRS.PayerCoverage               = PayerCoverage;
                paramRS.ResponseWorklistSingleQueue = dataWorklistSingleQ;
                paramRS.PharmacyDataModel           = model;
                paramRS.SingleQueueDataModel        = model_SQ;


                paramSyncResultmodel.HopeStatusResult       = syncHopeStatus;
                paramSyncResultmodel.HopeMessageResult      = syncHopeMessage;
                paramSyncResultmodel.HopedataResult         = syncHopeData;
                paramSyncResultmodel.SingleQStatusResult    = syncSQStatus;
                paramSyncResultmodel.SingleQMessageResult   = syncSQMessage;

                if ((model.header.is_SendDataItemIssue && syncHopeCode == 200 && isFlagHope)
                    || (!model.header.is_SendDataItemIssue && isFlagHope))
                {
                    pressResultData = IUnitOfWorks.UnitOfWorkPharmacy().submitPrintPrescription(paramRS, paramSyncResultmodel);
                    string updateFlagHope = IUnitOfWorks.UnitOfWorkPharmacy().UpdateFlagHOPE(model.header.OrganizationId, model.header.Admissionid, model.header.EncounterId);
                    messageHubContexts.Clients.All.InvokeAsync("Update Submit Data", pressResultData.SubmitStatusPressResult);
                    HttpResults = new ResponseData<SubmitPrintPrescription>("Data successfully updated and Get Data Pharmacy Print", Siloam.System.Web.StatusCode.OK, StatusMessage.Success, pressResultData, page, total);
                }
                else if ((model.header.is_SendDataItemIssue && (syncHopeCode == 500 || syncHopeCode == 200) && !isFlagHope)
                     || (!model.header.is_SendDataItemIssue && !isFlagHope))
                {

                    pressResultData = IUnitOfWorks.UnitOfWorkPharmacy().submitPrintPrescription(paramRS, paramSyncResultmodel);
                    messageHubContexts.Clients.All.InvokeAsync("Update Submit Data", pressResultData.SubmitStatusPressResult);
                    HttpResults = new ResponseData<SubmitPrintPrescription>("Data successfully updated and Get Data Pharmacy Print", Siloam.System.Web.StatusCode.OK, StatusMessage.Success, pressResultData, page, total);
                }
                else
                {
                    pressResultData.SubmitStatusPressResult = paramSyncResultmodel.HopeStatusResult;
                    pressResultData.HopeStatusResult        = paramSyncResultmodel.HopeStatusResult;
                    pressResultData.HopeMessageResult       = paramSyncResultmodel.HopeMessageResult;
                    pressResultData.HopedataResult          = paramSyncResultmodel.HopedataResult;
                    pressResultData.SingleQStatusResult     = paramSyncResultmodel.SingleQStatusResult;
                    pressResultData.SingleQMessageResult    = paramSyncResultmodel.SingleQMessageResult;
                    HttpResults = new ResponseData<SubmitPrintPrescription>("Data UnSuccessfully updated and Get Data Pharmacy Print", Siloam.System.Web.StatusCode.UnprocessableEntity, StatusMessage.Fail, pressResultData, page, total);
                }
            }
            catch (Exception ex)
            {

                int exCode = ex.HResult;

                if (exCode == -2147467259)
                {

                    HttpResults = new ResponseMessage(Siloam.System.Web.StatusCode.InternalServerErrorException, StatusMessage.Error, ex.Message, total);
                    Task<string> slackNotification = SlackMessageAsync(ValueStorage.SlackUrl, ValueStorage.ApiName, ValueStorage.ApiUrl + "/submitprintpresscriptionpharmacy", "[POST]Submit Verify Prescription and Item Issue", SubmitBy + "/" + jsonModel, ex.Message);

                }
                else
                {

                    HttpResults = new ResponseMessage(Siloam.System.Web.StatusCode.UnprocessableEntity, StatusMessage.Fail, ex.Message, total);
                    Task<string> slackNotification = SlackMessageAsync(ValueStorage.SlackUrl, ValueStorage.ApiName, ValueStorage.ApiUrl + "/submitprintpresscriptionpharmacy", "[POST]Submit Verify Prescription and Item Issue", SubmitBy + "/" + jsonModel, ex.Message);

                }

            }

        response:
            return HttpResponse(HttpResults);

        }
        [HttpPost("resubmititemissuehope")]
        [ProducesResponseType(typeof(ResponseData<ResponseResubmitHope>), 200)]
        public IActionResult resubmitItemIssueHope([FromBody] Param_ResubmitItemIssue param_ItemIssue)
        {
            int page                    = 1;
            int total                   = 0;
            string JSitemIssue            = JsonConvert.SerializeObject(param_ItemIssue);
            try
            {
                LogSyncToHope logSyncDrugToHope             = new LogSyncToHope();
                ResponseResubmitHope responseResubmitHope   = new ResponseResubmitHope();

                // HOPE
                string syncHopeStatus                       = "unsuccessful";
                string syncHopeMessage                      = "unsuccessful sent to hope";
                int syncHopeCode                            = 0;
                List<DrugItem> syncHopeData                 = new List<DrugItem>();

                if (param_ItemIssue == null)
                {
                    responseResubmitHope.HopeStatusResult   = "Pharmacy Item Data is null";
                    responseResubmitHope.HopeMessageResult  = "Pharmacy Item Data is null";
                    responseResubmitHope.HopedataResult     = syncHopeData;

                    HttpResults = new ResponseData<ResponseResubmitHope>("Data UnSuccessfully updated and resent item issue hope", Siloam.System.Web.StatusCode.UnprocessableEntity, StatusMessage.Fail, responseResubmitHope, page, total);
                    Task<string> slackNotification = SlackMessageAsync(ValueStorage.SlackUrl, ValueStorage.ApiName, ValueStorage.ApiUrl + "/resubmititemissuehope", "[POST]Resubmit Item Issue hope", param_ItemIssue.SubmitBy + "/" + JSitemIssue, "Data Unsuccessfully Created");
                    goto response;
                }
                SyncPrescriptionItemIssue presItemIssue     = new SyncPrescriptionItemIssue();
                List<DrugItem> listDrugItem = new List<DrugItem>();
                List<itemIssue> items = new List<itemIssue>();
                List<itemIssue> compoundItems = new List<itemIssue>();
                List<itemIssue> additionalItems = new List<itemIssue>();
                //List<itemIssue> temp = new List<itemIssue>();
                //temp = IUnitOfWorks.UnitOfWorkPharmacy().GetAdditionalItemIssue(param_ItemIssue.OrganizationId, param_ItemIssue.Admissionid, param_ItemIssue.EncounterId, 1);
                logSyncDrugToHope.startTime                     = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"));
                logSyncDrugToHope.organization_hope_id          = param_ItemIssue.OrganizationId;
                logSyncDrugToHope.admission_hope_id             = param_ItemIssue.Admissionid;
                logSyncDrugToHope.store_hope_id                 = param_ItemIssue.store_id;
                logSyncDrugToHope.doctor_hope_id                = param_ItemIssue.DoctorId;
                logSyncDrugToHope.user_hope_id                  = param_ItemIssue.SubmitBy;
                logSyncDrugToHope.admission_hope_id_SentHope    = param_ItemIssue.Admissionid_SentHope;


                items = (from a in param_ItemIssue.drugCons
                         where a.IssuedQty > 0 && a.is_SentHope
                         orderby a.pharmacy_prescription_id
                         select new itemIssue
                         {
                             itemId = a.itemId,
                             quantity = a.IssuedQty,
                             uomid = a.uomid
                         }).ToList();

                compoundItems = (from a in param_ItemIssue.compounds
                                 where a.IssuedQty > 0 && a.is_SentHope && a.itemId > 0
                                 orderby a.pharmacy_compound_detail_id
                                 select new itemIssue
                                 {
                                     itemId = a.itemId,
                                     quantity = a.IssuedQty,
                                     uomid = a.uomid
                                 }).ToList();

                additionalItems = (from a in param_ItemIssue.additionalItems
                                 where decimal.Parse(a.issued_qty == null ? "0" : a.issued_qty) > 0
                                 select new itemIssue
                                 {
                                     itemId = a.item_id,
                                     quantity = decimal.Parse(a.issued_qty),
                                     uomid = a.uom_id
                                 }).ToList();

                //if (temp != null)
                //{
                //    items.AddRange(temp);
                //}

                if (compoundItems != null
                    && compoundItems.Count > 0)
                {
                    items.AddRange(compoundItems);
                }

                if (additionalItems != null
                    && additionalItems.Count > 0)
                {
                    items.AddRange(additionalItems);
                }

                //SYNC DRUG TO HOPE
                if (items.Count() > 0)
                {

                    itemIssued itemissue                        = new itemIssued();
                    itemissue.itemIssue                         = items;

                    presItemIssue.MedicalOrderInterfaceId       = Guid.Empty;
                    presItemIssue.MedicalOrderId                = null;
                    presItemIssue.MedicalEncounterInterfaceId   = param_ItemIssue.EncounterId;
                    presItemIssue.SalesPriorityId               = 1;
                    presItemIssue.itemIssued                    = itemissue;
                    
                    string JS_pressItemIssue                    = JsonConvert.SerializeObject(presItemIssue);
                    

                    logSyncDrugToHope.jsonrequest_senditemissue = JS_pressItemIssue;


                    var syncItemIssue                               = IUnitOfWorks.UnitOfWorkSync().SyncPrescriptionToHope(param_ItemIssue.OrganizationId, param_ItemIssue.Admissionid_SentHope, param_ItemIssue.store_id, param_ItemIssue.DoctorId, param_ItemIssue.SubmitBy, JS_pressItemIssue);
                    var jsonDeserializeItemIssue                    = JsonConvert.DeserializeObject<DrugItemResponse>(syncItemIssue.ToString());

                    if (jsonDeserializeItemIssue != null)
                    {
                        logSyncDrugToHope.jsonresponse_sendItemIssue = JsonConvert.SerializeObject(jsonDeserializeItemIssue);
                        syncHopeStatus = jsonDeserializeItemIssue.status;
                        syncHopeMessage = jsonDeserializeItemIssue.message;
                        syncHopeCode = jsonDeserializeItemIssue.code;
                        if (jsonDeserializeItemIssue.data.Count > 0)
                        {
                            syncHopeData = jsonDeserializeItemIssue.data;
                            listDrugItem = jsonDeserializeItemIssue.data;
                        }

                        if (syncHopeCode == 200)
                        {
                            param_ItemIssue.drugCons = (from a in param_ItemIssue.drugCons
                                                        join b in listDrugItem on a.itemId equals b.ItemId
                                                    into c
                                                        from b in c.DefaultIfEmpty()
                                                        select new ItemIssueDrugCons
                                                        {
                                                            itemId = a.itemId,
                                                            IssuedQty = a.IssuedQty,
                                                            uomid = a.uomid,
                                                            pharmacy_prescription_id = a.pharmacy_prescription_id,
                                                            ARItemId = b == null ? 0 : long.Parse(b.ARItemId.ToString()),
                                                            is_SentHope = a.is_SentHope
                                                        }).ToList();

                            List<ItemIssueCompounds> headerCompound = new List<ItemIssueCompounds>();
                            if(param_ItemIssue.compounds.Count > 0)
                            {
                                headerCompound = param_ItemIssue.compounds.Where(x => x.itemId == 0).ToList();
                            }
                            param_ItemIssue.compounds = (from a in param_ItemIssue.compounds
                                                         join b in listDrugItem on a.itemId equals b.ItemId
                                                       into c
                                                         from b in c.DefaultIfEmpty()
                                                         select new ItemIssueCompounds
                                                         {
                                                             itemId = a.itemId,
                                                             IssuedQty = a.IssuedQty,
                                                             uomid = a.uomid,
                                                             pharmacy_compound_header_id = a.pharmacy_compound_header_id,
                                                             pharmacy_compound_detail_id = a.pharmacy_compound_detail_id,
                                                             ARItemId = b == null ? 0 : long.Parse(b.ARItemId.ToString()),
                                                             is_SentHope = a.is_SentHope
                                                         }).ToList();
                            if(headerCompound.Count > 0)
                            {
                                param_ItemIssue.compounds.AddRange(headerCompound);
                            }

                            param_ItemIssue.additionalItems = (from a in param_ItemIssue.additionalItems
                                                         join b in listDrugItem on a.item_id equals b.ItemId
                                                       into c
                                                         from b in c.DefaultIfEmpty()
                                                         select new ItemIssueAdditionalItem
                                                         {
                                                             pharmacy_additional_item_id = a.pharmacy_additional_item_id,
                                                             item_id = a.item_id,
                                                             quantity = a.quantity,
                                                             issued_qty = a.issued_qty,
                                                             uom_id = a.uom_id,
                                                             hope_aritem_id = b == null ? 0 : long.Parse(b.ARItemId.ToString()),
                                                             item_sequence = a.item_sequence
                                                         }).ToList();
                        }
                    }
                    else
                    {
                        syncHopeStatus = "no response from web api hope";
                    }
                }
                else
                {
                    syncHopeStatus = "no medicine is sent to hope";
                }

                responseResubmitHope.HopeStatusResult   = syncHopeStatus;
                responseResubmitHope.HopeMessageResult  = syncHopeMessage;
                responseResubmitHope.HopedataResult     = syncHopeData;
                string data = IUnitOfWorks.UnitOfWorkSync().InsertLogItemsIssue(logSyncDrugToHope);
                if (syncHopeCode == 200)
                {
                    responseResubmitHope = IUnitOfWorks.UnitOfWorkPharmacy().resubmititemissue(param_ItemIssue, responseResubmitHope);
                    messageHubContexts.Clients.All.InvokeAsync("Update Submit Data", responseResubmitHope.ResubmitStatusResult);
                    HttpResults = new ResponseData<ResponseResubmitHope>("Data successfully updated and resubmit item issue hope", Siloam.System.Web.StatusCode.OK, StatusMessage.Success, responseResubmitHope, page, total);
                }
                else
                {
                    HttpResults = new ResponseData<ResponseResubmitHope>("Data UnSuccessfully updated and resubmit item issue hope", Siloam.System.Web.StatusCode.UnprocessableEntity, StatusMessage.Fail, responseResubmitHope, page, total);
                }
            }
            catch (Exception ex)
            {
                HttpResults = new ResponseMessage(Siloam.System.Web.StatusCode.InternalServerErrorException, StatusMessage.Error, ex.Message, total);
                Task<string> slackNotification = SlackMessageAsync(ValueStorage.SlackUrl, ValueStorage.ApiName, ValueStorage.ApiUrl + "/resubmititemissuehope", "[POST]resubmit Item Issue Hope", param_ItemIssue.SubmitBy + "/" + JSitemIssue, ex.Message);
            }
        response:
            return HttpResponse(HttpResults);
        }

        [HttpPost("resubmitadditionalitemissuehope")]
        [ProducesResponseType(typeof(ResponseData<ResponseResubmitHope>), 200)]
        public IActionResult resubmitAdditionalItemIssueHope([FromBody] Param_ResubmitItemIssue param_ItemIssue)
        {
            int page = 1;
            int total = 0;
            string JSitemIssue = JsonConvert.SerializeObject(param_ItemIssue);
            try
            {
                LogSyncToHope logSyncDrugToHope = new LogSyncToHope();
                ResponseResubmitHope responseResubmitHope = new ResponseResubmitHope();

                // HOPE
                string syncHopeStatus = "unsuccessful";
                string syncHopeMessage = "unsuccessful sent to hope";
                int syncHopeCode = 0;
                List<DrugItem> syncHopeData = new List<DrugItem>();

                if (param_ItemIssue == null)
                {
                    responseResubmitHope.HopeStatusResult = "Pharmacy Item Data is null";
                    responseResubmitHope.HopeMessageResult = "Pharmacy Item Data is null";
                    responseResubmitHope.HopedataResult = syncHopeData;

                    HttpResults = new ResponseData<ResponseResubmitHope>("Data UnSuccessfully updated and resent additional item issue hope", Siloam.System.Web.StatusCode.UnprocessableEntity, StatusMessage.Fail, responseResubmitHope, page, total);
                    Task<string> slackNotification = SlackMessageAsync(ValueStorage.SlackUrl, ValueStorage.ApiName, ValueStorage.ApiUrl + "/resubmitadditionalitemissuehope", "[POST]Resubmit Additional Item Issue hope", param_ItemIssue.SubmitBy + "/" + JSitemIssue, "Data Unsuccessfully Created");
                    goto response;
                }
                SyncPrescriptionItemIssue presItemIssue = new SyncPrescriptionItemIssue();
                List<DrugItem> listDrugItem = new List<DrugItem>();
                List<itemIssue> items = new List<itemIssue>();
                List<itemIssue> compoundItems = new List<itemIssue>();
                List<itemIssue> additionalItems = new List<itemIssue>();
                logSyncDrugToHope.startTime = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"));
                logSyncDrugToHope.organization_hope_id = param_ItemIssue.OrganizationId;
                logSyncDrugToHope.admission_hope_id = param_ItemIssue.Admissionid;
                logSyncDrugToHope.store_hope_id = param_ItemIssue.store_id;
                logSyncDrugToHope.doctor_hope_id = param_ItemIssue.DoctorId;
                logSyncDrugToHope.user_hope_id = param_ItemIssue.SubmitBy;
                logSyncDrugToHope.admission_hope_id_SentHope = param_ItemIssue.Admissionid_SentHope;

                items = (from a in param_ItemIssue.drugCons
                         where a.IssuedQty > 0 && a.is_SentHope
                         orderby a.pharmacy_prescription_id
                         select new itemIssue
                         {
                             itemId = a.itemId,
                             quantity = a.IssuedQty,
                             uomid = a.uomid
                         }).ToList();

                compoundItems = (from a in param_ItemIssue.compounds
                                 where a.IssuedQty > 0 && a.is_SentHope && a.itemId > 0
                                 orderby a.pharmacy_compound_detail_id
                                 select new itemIssue
                                 {
                                     itemId = a.itemId,
                                     quantity = a.IssuedQty,
                                     uomid = a.uomid
                                 }).ToList();

                additionalItems = (from a in param_ItemIssue.additionalItems
                                   where decimal.Parse(a.issued_qty == null ? "0" : a.issued_qty) > 0
                                   select new itemIssue
                                   {
                                       itemId = a.item_id,
                                       quantity = decimal.Parse(a.issued_qty),
                                       uomid = a.uom_id
                                   }).ToList();

                if (compoundItems != null
                    && compoundItems.Count > 0)
                {
                    items.AddRange(compoundItems);
                }

                if (additionalItems != null
                    && additionalItems.Count > 0)
                {
                    items.AddRange(additionalItems);
                }

                //SYNC DRUG TO HOPE
                if (items.Count() > 0)
                {

                    itemIssued itemissue = new itemIssued();
                    itemissue.itemIssue = items;

                    presItemIssue.MedicalOrderInterfaceId = Guid.Empty;
                    presItemIssue.MedicalOrderId = null;
                    presItemIssue.MedicalEncounterInterfaceId = param_ItemIssue.EncounterId;
                    presItemIssue.SalesPriorityId = 1;
                    presItemIssue.itemIssued = itemissue;

                    string JS_pressItemIssue = JsonConvert.SerializeObject(presItemIssue);


                    logSyncDrugToHope.jsonrequest_senditemissue = JS_pressItemIssue;


                    var syncItemIssue = IUnitOfWorks.UnitOfWorkSync().SyncPrescriptionToHope(param_ItemIssue.OrganizationId, param_ItemIssue.Admissionid_SentHope, param_ItemIssue.store_id, param_ItemIssue.DoctorId, param_ItemIssue.SubmitBy, JS_pressItemIssue);
                    var jsonDeserializeItemIssue = JsonConvert.DeserializeObject<DrugItemResponse>(syncItemIssue.ToString());

                    if (jsonDeserializeItemIssue != null)
                    {
                        logSyncDrugToHope.jsonresponse_sendItemIssue = JsonConvert.SerializeObject(jsonDeserializeItemIssue);
                        syncHopeStatus = jsonDeserializeItemIssue.status;
                        syncHopeMessage = jsonDeserializeItemIssue.message;
                        syncHopeCode = jsonDeserializeItemIssue.code;
                        if (jsonDeserializeItemIssue.data.Count > 0 )
                        {
                            syncHopeData = jsonDeserializeItemIssue.data;
                            listDrugItem = jsonDeserializeItemIssue.data;
                        }

                        if (syncHopeCode == 200)
                        {
                            param_ItemIssue.drugCons = (from a in param_ItemIssue.drugCons
                                                        join b in listDrugItem on a.itemId equals b.ItemId
                                                    into c
                                                        from b in c.DefaultIfEmpty()
                                                        select new ItemIssueDrugCons
                                                        {
                                                            itemId = a.itemId,
                                                            IssuedQty = a.IssuedQty,
                                                            uomid = a.uomid,
                                                            pharmacy_prescription_id = a.pharmacy_prescription_id,
                                                            ARItemId = b == null ? 0 : long.Parse(b.ARItemId.ToString()),
                                                            is_SentHope = a.is_SentHope
                                                        }).ToList();

                            List<ItemIssueCompounds> headerCompound = new List<ItemIssueCompounds>();
                            if (param_ItemIssue.compounds.Count > 0)
                            {
                                headerCompound = param_ItemIssue.compounds.Where(x => x.itemId == 0).ToList();
                            }
                            param_ItemIssue.compounds = (from a in param_ItemIssue.compounds
                                                         join b in listDrugItem on a.itemId equals b.ItemId
                                                       into c
                                                         from b in c.DefaultIfEmpty()
                                                         select new ItemIssueCompounds
                                                         {
                                                             itemId = a.itemId,
                                                             IssuedQty = a.IssuedQty,
                                                             uomid = a.uomid,
                                                             pharmacy_compound_header_id = a.pharmacy_compound_header_id,
                                                             pharmacy_compound_detail_id = a.pharmacy_compound_detail_id,
                                                             ARItemId = b == null ? 0 : long.Parse(b.ARItemId.ToString()),
                                                             is_SentHope = a.is_SentHope
                                                         }).ToList();
                            if (headerCompound.Count > 0)
                            {
                                param_ItemIssue.compounds.AddRange(headerCompound);
                            }

                            param_ItemIssue.additionalItems = (from a in param_ItemIssue.additionalItems
                                                               join b in listDrugItem on a.item_id equals b.ItemId
                                                             into c
                                                               from b in c.DefaultIfEmpty()
                                                               select new ItemIssueAdditionalItem
                                                               {
                                                                   pharmacy_additional_item_id = a.pharmacy_additional_item_id,
                                                                   item_id = a.item_id,
                                                                   quantity = a.quantity,
                                                                   issued_qty = a.issued_qty,
                                                                   uom_id = a.uom_id,
                                                                   hope_aritem_id = b == null ? 0 : long.Parse(b.ARItemId.ToString()),
                                                                   item_sequence = a.item_sequence
                                                               }).ToList();
                        }
                    }
                    else
                    {
                        syncHopeStatus = "no response from web api hope";
                    }
                }
                else
                {
                    syncHopeStatus = "no medicine is sent to hope";
                }

                responseResubmitHope.HopeStatusResult = syncHopeStatus;
                responseResubmitHope.HopeMessageResult = syncHopeMessage;
                responseResubmitHope.HopedataResult = syncHopeData;
                string data = IUnitOfWorks.UnitOfWorkSync().InsertLogItemsIssue(logSyncDrugToHope);
                if (syncHopeCode == 200)
                {
                    responseResubmitHope = IUnitOfWorks.UnitOfWorkPharmacy().resubmitadditionalitemissue(param_ItemIssue, responseResubmitHope);
                    messageHubContexts.Clients.All.InvokeAsync("Update Submit Data", responseResubmitHope.ResubmitStatusResult);
                    HttpResults = new ResponseData<ResponseResubmitHope>("Data successfully updated and resubmit item issue hope", Siloam.System.Web.StatusCode.OK, StatusMessage.Success, responseResubmitHope, page, total);
                }
                else
                {
                    HttpResults = new ResponseData<ResponseResubmitHope>("Data UnSuccessfully updated and resubmit item issue hope", Siloam.System.Web.StatusCode.UnprocessableEntity, StatusMessage.Fail, responseResubmitHope, page, total);
                }
            }
            catch (Exception ex)
            {
                HttpResults = new ResponseMessage(Siloam.System.Web.StatusCode.InternalServerErrorException, StatusMessage.Error, ex.Message, total);
                Task<string> slackNotification = SlackMessageAsync(ValueStorage.SlackUrl, ValueStorage.ApiName, ValueStorage.ApiUrl + "/resubmititemissuehope", "[POST]resubmit Item Issue Hope", param_ItemIssue.SubmitBy + "/" + JSitemIssue, ex.Message);
            }
        response:
            return HttpResponse(HttpResults);
        }

        [HttpPost("pharmacyrecordunittc/{UserName}/{QueueNo}/{DeliveryFee}/{PayerCoverage}/{IsDefaultCoverage:int}/{SubmitBy:long}")]
        [ProducesResponseType(typeof(ResponseData<string>), 200)]
        public IActionResult Create(string UserName, string QueueNo, string DeliveryFee, string PayerCoverage, short IsDefaultCoverage,Int64 SubmitBy, [FromBody] PharmacyData model)
        {

            int page = 1;
            int total = 0;


            string jsonModel            = JsonConvert.SerializeObject(model);
            string JsonSingleQWorklist  = JsonConvert.SerializeObject(model.singleQWorklistData);
            string usedosetext          = "";

            if (model == null)
            {

                HttpResults = new ResponseMessage(Siloam.System.Web.StatusCode.BadRequest, StatusMessage.Fail, HttpResponseMessageKey.DataUnsuccessfullyCreated, total);
                Task<string> slackNotification = SlackMessageAsync(ValueStorage.SlackUrl, ValueStorage.ApiName, ValueStorage.ApiUrl + "/pharmacyrecord", "[POST]Submit Verify Prescription", SubmitBy + "/" + jsonModel, "Data Unsuccessfully Created");
                goto response;

            }

            try
            {
                long transAdmId             = model.header.Admissionid;
                string transAdmNo           = model.header.AdmissionNo;
                List<long> aidopayerid      = new List<long>();
                List<long> mysiloampayerid  = new List<long>();
                string payertemp            = "";
                payertemp                   = IUnitOfWorks.UnitOfWorkAidoDrug().GetSettingAidoPayerId(model.header.OrganizationId);
                aidopayerid                 = payertemp.Split(',').Select(Int64.Parse).ToList();

                payertemp                   = "";
                payertemp                   = IUnitOfWorks.UnitOfWorkAidoDrug().GetSettingMysiloamPayerId(model.header.OrganizationId);
                mysiloampayerid             = payertemp.Split(',').Select(Int64.Parse).ToList();

                long pharmacypayerid        = IUnitOfWorks.UnitOfWorkPharmacy().GetPayerIdRecord(model.header.OrganizationId, model.header.PatientId, model.header.Admissionid, model.header.EncounterId);

               
                usedosetext                 = IUnitOfWorks.UnitOfWorkPharmacy().GetSettingDoseText(model.header.OrganizationId);
                var processStart            = IUnitOfWorks.UnitOfWorkPharmacy().InsertToTimeStampTable(model.header.EncounterId, "PHARMACY-CALL API SUBMIT");
                //from API HOPE
                string syncStatusCons       = "";
                string syncStatus           = "";
                string dataWorklistSingleQ  = "SIGNLEQSUCCESS";
                string status_SQ            = "OK";
                string message_SQ           = "OK";
                int tempcount               = model.prescription.Count(p => p.is_consumables == 1);

                model.prescription.ForEach(p => p.prescription_sync_id = Guid.NewGuid());
                model.compound_header.ForEach(p => p.compound_header_sync_id = Guid.NewGuid());
                model.compound_detail.ForEach(p => p.compound_detail_sync_id = Guid.NewGuid());
                SyncPrescription    pres            = new SyncPrescription();
                SingleQueue         model_SQ        = new SingleQueue();
                List<OrderItem>     items           = new List<OrderItem>();
                List<OrderItem>     temp            = new List<OrderItem>();
                
                temp = IUnitOfWorks.UnitOfWorkPharmacy().GetAdditional(model.header.OrganizationId, model.header.Admissionid, model.header.EncounterId, 1, usedosetext);
                items = (from a in model.prescription
                            where a.is_consumables == 0 && decimal.Parse(a.IssuedQty) > 0
                            orderby a.item_sequence
                            select new OrderItem
                            {
                                AdministrationFrequencyId = a.frequency_id,
                                AdministrationInstruction = a.remarks,
                                AdministrationRouteId = a.administration_route_id,
                                DispensingInstruction = "",
                                Dose = a.dose_text != "" && usedosetext == "TRUE" ? "0.0001" : a.dosage_id,
                                DoseText = a.dose_text,
                                DoseUomId = a.dose_uom_id,
                                DrugId = a.item_id,
                                IsPrn = false,
                                PatientInformation = "",
                                Quantity = a.IssuedQty,
                                Repeat = a.iteration,
                                MedicalEncounterEntryInterfaceId = a.prescription_sync_id
                            }).ToList();
                if (temp != null)
                {
                    items.AddRange(temp);
                }


                //SYNC DRUG TO HOPE//SYNC DRUG TO HOPE
                if (items.Count() > 0)
                {
                    pres.MedicalOrderInterfaceId = Guid.Empty;
                    pres.MedicalOrderId = null;
                    pres.MedicalEncounterInterfaceId = model.header.EncounterId;
                    pres.SalesPriorityId = 1;
                    pres.Notes = model.header.PharmacyNotes;
                    pres.OrderItems = items;

                    string JsonString = JsonConvert.SerializeObject(pres);
                    var hopeStart = IUnitOfWorks.UnitOfWorkPharmacy().InsertToTimeStampTable(model.header.EncounterId, "PHARMACY-REQUEST API DRUG HOPE");
                    syncStatus = IUnitOfWorks.UnitOfWorkSync().SyncPrescription(model.header.OrganizationId, model.header.Admissionid, JsonString);
                    var hopeFinish = IUnitOfWorks.UnitOfWorkPharmacy().InsertToTimeStampTable(model.header.EncounterId, "PHARMACY-RESPONSE API DRUG HOPE");
                }
                else
                {
                    syncStatus = "success";
                }

                //SYNC CONSUMABLE TO HOPE
                if (model.prescription.Count(p => p.is_consumables == 1) > 0)
                {
                    SyncConsumables cons = new SyncConsumables();
                    List<OrderItemConsumables> itemcons = new List<OrderItemConsumables>();
                    List<OrderItemConsumables> tempcons = new List<OrderItemConsumables>();
                    tempcons = IUnitOfWorks.UnitOfWorkPharmacy().GetAdditionalConsumables(model.header.OrganizationId, model.header.Admissionid, model.header.EncounterId, 1);
                    itemcons = (from a in model.prescription
                                where a.is_consumables == 1 && decimal.Parse(a.IssuedQty) > 0
                                orderby a.item_sequence
                                select new OrderItemConsumables
                                {
                                    UsageInstruction = a.remarks,
                                    ItemId = a.item_id,
                                    PatientInformation = "",
                                    Quantity = a.IssuedQty,
                                    DispensingInstruction = "",
                                    MedicalEncounterEntryInterfaceId = a.prescription_sync_id
                                }).ToList();
                    if (tempcons != null)
                    {
                        itemcons.AddRange(tempcons);
                    }
                    cons.MedicalOrderInterfaceId = Guid.Empty;
                    cons.MedicalOrderId = null;
                    cons.MedicalEncounterInterfaceId = model.header.EncounterId;
                    cons.SalesPriorityId = 1;
                    cons.Notes = model.header.PharmacyNotes;
                    cons.OrderItems = itemcons;

                    string JsonStringCons = JsonConvert.SerializeObject(cons);
                    syncStatusCons = IUnitOfWorks.UnitOfWorkSync().SyncConsumables(model.header.OrganizationId, model.header.Admissionid, JsonStringCons);
                }
                else
                {
                    syncStatusCons = "success";
                }

                if (!model.header.is_tele && model.header.is_SingleQueue)
                {
                    if (JsonSingleQWorklist != null)
                    {
                        dataWorklistSingleQ         = IUnitOfWorks.UnitOfWorkSingleQueue().SyncWorklistSingleQ(JsonSingleQWorklist);
                        var objResult               = JsonConvert.DeserializeObject<ResultListSingleQueue>(dataWorklistSingleQ);
                        status_SQ                   = objResult.status;
                        message_SQ                  = objResult.message;
                        if (status_SQ.Equals("OK"))
                        {
                            model_SQ = objResult.data[0];
                        }
                    }
                    else
                    {
                        status_SQ                   = "Fail";
                        dataWorklistSingleQ         = "SIGNLEQDATANULL";
                        message_SQ                  = "SingleQueue Worklist Null";
                    }
                }
                Param_RecordSubmit paramRS          = new Param_RecordSubmit();
                paramRS.SubmitBy                    = SubmitBy;
                paramRS.TransAdmId                  = transAdmId;
                paramRS.TransAdmNo                  = transAdmNo;
                paramRS.QueueNo                     = QueueNo;
                paramRS.DeliveryFee                 = DeliveryFee;
                paramRS.PayerCoverage               = PayerCoverage;
                paramRS.ResponseWorklistSingleQueue = dataWorklistSingleQ;
                paramRS.PharmacyDataModel           = model;
                paramRS.SingleQueueDataModel        = model_SQ;

                string data = "";
                string syncSQStatus = "|" + status_SQ + "|" + message_SQ + "|";
                if (syncStatus.ToLower() == "success" && syncStatusCons.ToLower() == "success")
                {
                    //data = IUnitOfWorks.UnitOfWorkPharmacy().UpdateRecordSubmit(SubmitBy, transAdmId, transAdmNo, QueueNo, DeliveryFee, PayerCoverage,model);
                    data = IUnitOfWorks.UnitOfWorkPharmacy().UpdateRecordSingleQ(paramRS);
                    string updateFlagHope = IUnitOfWorks.UnitOfWorkPharmacy().UpdateFlagHOPE(model.header.OrganizationId, model.header.Admissionid, model.header.EncounterId);
                    messageHubContexts.Clients.All.InvokeAsync("Update Submit Data", data);
                    HttpResults = new ResponseData<string>("Data successfully updated", Siloam.System.Web.StatusCode.OK, StatusMessage.Success, data + "|SUCCESS|" + syncSQStatus, page, total);

                }
                else if (syncStatus.ToLower() == "success" && syncStatusCons.ToLower() != "success")
                {
                    //data = IUnitOfWorks.UnitOfWorkPharmacy().UpdateRecordSubmit(SubmitBy, transAdmId, transAdmNo, QueueNo, DeliveryFee, PayerCoverage, model);
                    data = IUnitOfWorks.UnitOfWorkPharmacy().UpdateRecordSingleQ(paramRS);
                    string updateFlagHope = IUnitOfWorks.UnitOfWorkPharmacy().UpdateFlagHOPE(model.header.OrganizationId, model.header.Admissionid, model.header.EncounterId);
                    messageHubContexts.Clients.All.InvokeAsync("Update Submit Data", data);
                    HttpResults = new ResponseData<string>("Data successfully updated", Siloam.System.Web.StatusCode.OK, StatusMessage.Success, data + "|CONSUMABLEFAILED|" + syncStatusCons + syncSQStatus, page, total);
                }
                else if (syncStatus.ToLower() != "success" && syncStatusCons.ToLower() == "success")
                {
                    //data = IUnitOfWorks.UnitOfWorkPharmacy().UpdateRecordSubmit(SubmitBy, transAdmId, transAdmNo, QueueNo, DeliveryFee, PayerCoverage, model);
                    data = IUnitOfWorks.UnitOfWorkPharmacy().UpdateRecordSingleQ(paramRS);
                    string updateFlagHope = IUnitOfWorks.UnitOfWorkPharmacy().UpdateFlagHOPE(model.header.OrganizationId, model.header.Admissionid, model.header.EncounterId);
                    messageHubContexts.Clients.All.InvokeAsync("Update Submit Data", data);
                    HttpResults = new ResponseData<string>("Data successfully updated", Siloam.System.Web.StatusCode.OK, StatusMessage.Success, data + "|DRUGFAILED|" + syncStatus + syncSQStatus, page, total);
                }
                else
                {
                    //data = IUnitOfWorks.UnitOfWorkPharmacy().UpdateRecordSubmit(SubmitBy, transAdmId, transAdmNo, QueueNo, DeliveryFee, PayerCoverage,model);
                    data = IUnitOfWorks.UnitOfWorkPharmacy().UpdateRecordSingleQ(paramRS);
                    messageHubContexts.Clients.All.InvokeAsync("Update Submit Data", data);
                    if (syncStatus.ToLower() == syncStatusCons.ToLower())
                    {
                        HttpResults = new ResponseData<string>("Data successfully updated", Siloam.System.Web.StatusCode.OK, StatusMessage.Success, data + "|HOPEFAILED|" + syncStatus + syncSQStatus, page, total);
                    }
                    else
                    {
                        HttpResults = new ResponseData<string>("Data successfully updated", Siloam.System.Web.StatusCode.OK, StatusMessage.Success, data + "|HOPEFAILED|Drug Failed: " + syncStatus + ", Consumable Failed: " + syncStatusCons + syncSQStatus, page, total);
                    }
                }
            }
            catch (Exception ex)
            {

                int exCode = ex.HResult;

                if (exCode == -2147467259)
                {

                    HttpResults = new ResponseMessage(Siloam.System.Web.StatusCode.InternalServerErrorException, StatusMessage.Error, ex.Message, total);
                    Task<string> slackNotification = SlackMessageAsync(ValueStorage.SlackUrl, ValueStorage.ApiName, ValueStorage.ApiUrl + "/pharmacyrecord", "[POST]Submit Verify Prescription", SubmitBy + "/" + jsonModel, ex.Message);

                }
                else
                {

                    HttpResults = new ResponseMessage(Siloam.System.Web.StatusCode.UnprocessableEntity, StatusMessage.Fail, ex.Message, total);
                    Task<string> slackNotification = SlackMessageAsync(ValueStorage.SlackUrl, ValueStorage.ApiName, ValueStorage.ApiUrl + "/pharmacyrecord", "[POST]Submit Verify Prescription", SubmitBy + "/" + jsonModel, ex.Message);

                }

            }

        response:
            return HttpResponse(HttpResults);

        }

        [HttpPost("pharmacyrecordunititemissue/{UserName}/{QueueNo}/{DeliveryFee}/{PayerCoverage}/{IsDefaultCoverage:int}/{SubmitBy:long}")]
        [ProducesResponseType(typeof(ResponseData<string>), 200)]
        public IActionResult CreatePharmacyToItemIssue(string UserName, string QueueNo, string DeliveryFee, string PayerCoverage, short IsDefaultCoverage, Int64 SubmitBy, [FromBody] PharmacyData model)
        {

            int page = 1;
            int total = 0;


            //#region debug
            //long admissionId = model.header.Admissionid;
            //string message = "";
            //string resultMessage = "";
            //#endregion  

            string jsonModel = JsonConvert.SerializeObject(model);
            string JsonSingleQWorklist = JsonConvert.SerializeObject(model.singleQWorklistData);
            string usedosetext = "";

            if (model == null)
            {

                HttpResults = new ResponseMessage(Siloam.System.Web.StatusCode.BadRequest, StatusMessage.Fail, HttpResponseMessageKey.DataUnsuccessfullyCreated, total);
                Task<string> slackNotification = SlackMessageAsync(ValueStorage.SlackUrl, ValueStorage.ApiName, ValueStorage.ApiUrl + "/pharmacyrecordunititemissue", "[POST]Submit Verify Prescription To Item Issue", SubmitBy + "/" + jsonModel, "Data Unsuccessfully Created");
                goto response;

            }

            try
            {
                long transAdmId = model.header.Admissionid;
                string transAdmNo = model.header.AdmissionNo;
                List<long> aidopayerid = new List<long>();
                List<long> mysiloampayerid = new List<long>();
                string payertemp = "";
                payertemp = IUnitOfWorks.UnitOfWorkAidoDrug().GetSettingAidoPayerId(model.header.OrganizationId);
                aidopayerid = payertemp.Split(',').Select(Int64.Parse).ToList();

                payertemp = "";
                payertemp = IUnitOfWorks.UnitOfWorkAidoDrug().GetSettingMysiloamPayerId(model.header.OrganizationId);
                mysiloampayerid = payertemp.Split(',').Select(Int64.Parse).ToList();

                long pharmacypayerid = IUnitOfWorks.UnitOfWorkPharmacy().GetPayerIdRecord(model.header.OrganizationId, model.header.PatientId, model.header.Admissionid, model.header.EncounterId);


                usedosetext = IUnitOfWorks.UnitOfWorkPharmacy().GetSettingDoseText(model.header.OrganizationId);
                var processStart = IUnitOfWorks.UnitOfWorkPharmacy().InsertToTimeStampTable(model.header.EncounterId, "PHARMACY-CALL API SUBMIT ITEM ISSUE");
                //from API HOPE
                string syncStatus = "";
                string syncMessage = "";
                string dataWorklistSingleQ = "SIGNLEQSUCCESS";
                string status_SQ = "OK";
                string message_SQ = "OK";
                int tempcount = model.prescription.Count(p => p.is_consumables == 1);

                model.prescription.ForEach(p => p.prescription_sync_id = Guid.NewGuid());
                model.compound_header.ForEach(p => p.compound_header_sync_id = Guid.NewGuid());
                model.compound_detail.ForEach(p => p.compound_detail_sync_id = Guid.NewGuid());
                SingleQueue model_SQ = new SingleQueue();
                SyncPrescriptionItemIssue presItemIssue = new SyncPrescriptionItemIssue();
                List<itemIssue> items = new List<itemIssue>();
                List<itemIssue> temp = new List<itemIssue>();
                

                temp = IUnitOfWorks.UnitOfWorkPharmacy().GetAdditionalItemIssue(model.header.OrganizationId, model.header.Admissionid, model.header.EncounterId, 1);
                items = (from a in model.prescription
                         where decimal.Parse(a.IssuedQty) > 0
                         orderby a.item_sequence
                         select new itemIssue
                         {
                             itemId = a.item_id,
                             quantity = decimal.Parse(a.IssuedQty),
                             uomid = a.uom_id
                         }).ToList();
                if (temp != null)
                {
                    items.AddRange(temp);
                }

                //SYNC DRUG TO HOPE
                if (items.Count() > 0)
                {
                    itemIssued test = new itemIssued();
                    test.itemIssue = items;

                    presItemIssue.MedicalOrderInterfaceId = Guid.Empty;
                    presItemIssue.MedicalOrderId = null;
                    presItemIssue.MedicalEncounterInterfaceId = model.header.EncounterId;
                    presItemIssue.SalesPriorityId = 1;
                    presItemIssue.itemIssued = test;

                    string JsonString = JsonConvert.SerializeObject(presItemIssue);
                    var hopeStart = IUnitOfWorks.UnitOfWorkPharmacy().InsertToTimeStampTable(model.header.EncounterId, "PHARMACY-REQUEST ITEM ISSUE API DRUG HOPE");
                    var syncItemIssue = IUnitOfWorks.UnitOfWorkSync().SyncPrescriptionItemIssue(model.header.OrganizationId, model.header.Admissionid, model.header.store_id, model.header.DoctorId, SubmitBy, JsonString);

                    //message = "1";
                    //resultMessage = syncItemIssue.ToString();

                    var hopeFinish = IUnitOfWorks.UnitOfWorkPharmacy().InsertToTimeStampTable(model.header.EncounterId, "PHARMACY-RESPONSE ITEM ISSUE API DRUG HOPE");
                    //message = "2";
                    //resultMessage = hopeFinish;
                    var jsonDeserializeItemIssue  = JsonConvert.DeserializeObject<ReturnValueItemIssue>(syncItemIssue.ToString());
                    //message = "3";
                    //resultMessage = jsonDeserializeItemIssue.ToString();
                    syncStatus = jsonDeserializeItemIssue.status;
                    //message = "4";
                    //resultMessage = syncStatus;
                    syncMessage = jsonDeserializeItemIssue.message;
                    //message = "5";
                    //resultMessage = syncMessage;
                    if (syncStatus.ToLower() == "success")
                    {
                        //message = "Masuk Case Berhasil ";
                        //resultMessage = syncItemIssue.ToString();
                        model.prescription = (from a in model.prescription
                                              join b in jsonDeserializeItemIssue.data.itemIssue on a.item_id equals b.ItemId
                                              into c from b in c.DefaultIfEmpty()
                                              select new PharmacyPrescription {
                                                  administration_route_code = a.administration_route_code,
                                                  administration_route_id = a.administration_route_id,
                                                  compound_id = a.compound_id,
                                                  compound_name = a.compound_name,
                                                  doctor_name = a.doctor_name,
                                                  dosage_id = a.dosage_id,
                                                  dose_text = a.dose_text,
                                                  dose_uom = a.dose_uom,
                                                  dose_uom_id = a.dose_uom_id,
                                                  frequency_code = a.frequency_code,
                                                  frequency_id = a.frequency_id,
                                                  IssuedQty = a.IssuedQty,
                                                  is_consumables = a.is_consumables,
                                                  is_routine = a.is_routine,
                                                  item_id = a.item_id,
                                                  item_name = a.item_name,
                                                  item_sequence = a.item_sequence,
                                                  iteration = a.iteration,
                                                  MainStoreQuantity = a.MainStoreQuantity,
                                                  origin_prescription_id = a.origin_prescription_id,
                                                  prescription_id = a.prescription_id,
                                                  prescription_no = a.prescription_no,
                                                  prescription_sync_id = a.prescription_sync_id,
                                                  quantity = a.quantity,
                                                  remarks = a.remarks,
                                                  SubStoreQuantity = a.SubStoreQuantity,
                                                  uom_code = a.uom_code,
                                                  uom_id = a.uom_id,
                                                  hope_aritem_id = b.ArItemId == null ? 0 : long.Parse(b.ArItemId.ToString()) 
                                              }).ToList();
                    }
                    //else
                    //{
                    //    message = "Masuk Case Gagal ";
                    //    resultMessage = syncItemIssue.ToString();
                    //}
                }
                else
                {
                    syncStatus = "success";
                }


                if (!model.header.is_tele && model.header.is_SingleQueue)
                {
                    if (JsonSingleQWorklist != null)
                    {


                        //message = "case single queue ";
                        //resultMessage = "case single queue ";
                        dataWorklistSingleQ = IUnitOfWorks.UnitOfWorkSingleQueue().SyncWorklistSingleQ(JsonSingleQWorklist);
                        //message = "case single queue return ";
                        //resultMessage = dataWorklistSingleQ;
                        var objResult = JsonConvert.DeserializeObject<ResultListSingleQueue>(dataWorklistSingleQ);
                        status_SQ = objResult.status;
                        message_SQ = objResult.message;
                        if (status_SQ.Equals("OK"))
                        {
                            model_SQ = objResult.data[0];
                        }
                    }
                    else
                    {
                        status_SQ = "Fail";
                        dataWorklistSingleQ = "SIGNLEQDATANULL";
                        message_SQ = "SingleQueue Worklist Null";
                    }
                }

                Param_RecordSubmit paramRS = new Param_RecordSubmit();
                paramRS.SubmitBy = SubmitBy;
                paramRS.TransAdmId = transAdmId;
                paramRS.TransAdmNo = transAdmNo;
                paramRS.QueueNo = QueueNo;
                paramRS.DeliveryFee = DeliveryFee;
                paramRS.PayerCoverage = PayerCoverage;
                paramRS.ResponseWorklistSingleQueue = dataWorklistSingleQ;
                paramRS.PharmacyDataModel = model;
                paramRS.SingleQueueDataModel = model_SQ;

                string data = "";
                string syncSQStatus = "|" + status_SQ + "|" + message_SQ + "|";
                //message = syncSQStatus;
                //resultMessage = "";
                if (syncStatus.ToLower() == "success")
                {
                    data = IUnitOfWorks.UnitOfWorkPharmacy().UpdateRecordItemIssueSingleQ(paramRS);
                    string updateFlagHope = IUnitOfWorks.UnitOfWorkPharmacy().UpdateFlagHOPE(model.header.OrganizationId, model.header.Admissionid, model.header.EncounterId);

                    //message = "result Berhasil ";
                    //resultMessage = syncMessage;
                    //IUnitOfWorks.UnitOfWorkHistory().addHistory(admissionId, message, resultMessage, "Success");
                    messageHubContexts.Clients.All.InvokeAsync("Update Submit Data", data);
                    HttpResults = new ResponseData<string>("Data successfully updated", Siloam.System.Web.StatusCode.OK, StatusMessage.Success, data + "|SUCCESS|" + syncSQStatus, page, total);

                }
                
                else if (syncStatus.ToLower() != "success")
                {
                    //message = "result gagal ";
                    //iunitofworks.unitofWorkHistory().addHistory(admissionId, message, resultMessage, "Fail");
                    //data = IUnitOfWorks.UnitOfWorkPharmacy().UpdateRecordSingleQ(paramRS);
                    //string updateFlagHope = IUnitOfWorks.UnitOfWorkPharmacy().UpdateFlagHOPE(model.header.OrganizationId, model.header.Admissionid, model.header.EncounterId);
                    //messageHubContexts.Clients.All.InvokeAsync("Update Submit Data", data);
                    HttpResults = new ResponseData<string>(syncMessage, Siloam.System.Web.StatusCode.UnprocessableEntity, StatusMessage.Fail, data + "|DRUGFAILED|" + syncMessage + syncSQStatus, page, total);
                }
            }
            catch (Exception ex)
            {
                //IUnitOfWorks.UnitOfWorkHistory().addHistory(admissionId, message, resultMessage, "Fail");
                int exCode = ex.HResult;

                if (exCode == -2147467259)
                {

                    HttpResults = new ResponseMessage(Siloam.System.Web.StatusCode.InternalServerErrorException, StatusMessage.Error, ex.Message, total);
                    Task<string> slackNotification = SlackMessageAsync(ValueStorage.SlackUrl, ValueStorage.ApiName, ValueStorage.ApiUrl + "/pharmacyrecordunititemissue", "[POST]Submit Verify Prescription To Item Issue", SubmitBy + "/" + jsonModel, ex.Message);

                }
                else
                {

                    HttpResults = new ResponseMessage(Siloam.System.Web.StatusCode.UnprocessableEntity, StatusMessage.Fail, ex.Message, total);
                    Task<string> slackNotification = SlackMessageAsync(ValueStorage.SlackUrl, ValueStorage.ApiName, ValueStorage.ApiUrl + "/pharmacyrecordunititemissue", "[POST]Submit Verify Prescription To Item Issue", SubmitBy + "/" + jsonModel, ex.Message);

                }

            }

        response:
            return HttpResponse(HttpResults);

        }

        [HttpPost("pharmacydruginfo/{OrganizationId:long}/{AdmissionId:long}/{EncounterId}/{Updater:long}")]
        [ProducesResponseType(typeof(ResponseData<string>), 200)]
        public IActionResult UpdateDrugInfo(long OrganizationId, long AdmissionId, Guid EncounterId, long Updater, [FromBody]List<PharmacyDrugInfo> model)
        {

            int page = 1;
            int total = 0;

            try
            {

                string data = IUnitOfWorks.UnitOfWorkPharmacy().UpdateDrugInfo(OrganizationId, AdmissionId, EncounterId, Updater, model);

                messageHubContexts.Clients.All.InvokeAsync("Update Pharmacy Drug Info", data);

                HttpResults = new ResponseData<string>("Data successfully updated", Siloam.System.Web.StatusCode.OK, StatusMessage.Success, data, page, total);

            }
            catch (Exception ex)
            {

                int exCode = ex.HResult;
                string jsonModel = JsonConvert.SerializeObject(model);

                if (exCode == -2147467259)
                {

                    HttpResults = new ResponseMessage(Siloam.System.Web.StatusCode.InternalServerErrorException, StatusMessage.Error, ex.Message, total);
                    Task<string> slackNotification = SlackMessageAsync(ValueStorage.SlackUrl, ValueStorage.ApiName, ValueStorage.ApiUrl + "/pharmacydruginfo", "[POST]Update Drug Info", OrganizationId.ToString() + "/" + AdmissionId.ToString() + "/" + EncounterId.ToString() + "/" + Updater.ToString() + "/" + jsonModel.ToString(), ex.Message);

                }
                else
                {

                    HttpResults = new ResponseMessage(Siloam.System.Web.StatusCode.UnprocessableEntity, StatusMessage.Fail, ex.Message, total);
                    Task<string> slackNotification = SlackMessageAsync(ValueStorage.SlackUrl, ValueStorage.ApiName, ValueStorage.ApiUrl + "/pharmacydruginfo", "[POST]Update Drug Info", OrganizationId.ToString() + "/" + AdmissionId.ToString() + "/" + EncounterId.ToString() + "/" + Updater.ToString() + "/" + jsonModel.ToString(), ex.Message);

                }

            }

            response:
            return HttpResponse(HttpResults);

        }

        [HttpPost("additionalpharmacyrecord/{SubmitBy:long}/{QueueNo}")]
        [ProducesResponseType(typeof(ResponseData<string>), 200)]
        public IActionResult UpdateRecordAdditional(Int64 SubmitBy, string QueueNo, [FromBody]PharmacyData model)
        {

            int page = 1;
            int total = 0;
            //List<PharmacyCompoundHeader> compoundHeaders = new List<PharmacyCompoundHeader>();
            //List<PharmacyCompoundDetail> compoundDetails = new List<PharmacyCompoundDetail>();
            //model.compound_header = compoundHeaders;
            //model.compound_detail = compoundDetails;
            string jsonModel            = JsonConvert.SerializeObject(model);
            string JsonSingleQWorklist  = JsonConvert.SerializeObject(model.singleQWorklistData);
            string usedosetext = "";

            if (model == null)
            {

                HttpResults = new ResponseMessage(Siloam.System.Web.StatusCode.BadRequest, StatusMessage.Fail, HttpResponseMessageKey.DataUnsuccessfullyCreated, total);
                Task<string> slackNotification = SlackMessageAsync(ValueStorage.SlackUrl, ValueStorage.ApiName, ValueStorage.ApiUrl + "/additionalpharmacyrecord", "[POST]Submit Verify Additional Prescription", SubmitBy + "/" + jsonModel, "Data Unsuccessfully Created");
                goto response;

            }

            try
            {
                //from API HOPE
                long transAdmId             = model.header.Admissionid;
                string transAdmNo           = model.header.AdmissionNo;
                string syncStatusCons       = "";
                string syncStatus           = "";
                string dataWorklistSingleQ  = "SIGNLEQSUCCESS";
                string status_SQ            = "OK";
                string message_SQ           = "OK";

                usedosetext = IUnitOfWorks.UnitOfWorkPharmacy().GetSettingDoseText(model.header.OrganizationId);

                model.prescription.ForEach(p => p.prescription_sync_id = Guid.NewGuid());
                model.compound_header.ForEach(p => p.compound_header_sync_id = Guid.NewGuid());
                model.compound_detail.ForEach(p => p.compound_detail_sync_id = Guid.NewGuid());
                SyncPrescription pres   = new SyncPrescription();
                SingleQueue model_SQ    = new SingleQueue();
                List<OrderItem> items   = new List<OrderItem>();
                List<OrderItem> temp    = new List<OrderItem>();
                //List<OrderItem> compheader = new List<OrderItem>();
                //List<OrderItem> compdetail = new List<OrderItem>();
                //List<OrderItem> tempcomp = new List<OrderItem>();
                temp = IUnitOfWorks.UnitOfWorkPharmacy().GetAdditional(model.header.OrganizationId, model.header.Admissionid, model.header.EncounterId, 2, usedosetext);
                items = (from a in model.prescription
                         where a.is_consumables == 0
                         orderby a.item_sequence
                         select new OrderItem
                         {
                             AdministrationFrequencyId = a.frequency_id,
                             AdministrationInstruction = a.remarks,
                             AdministrationRouteId = a.administration_route_id,
                             DispensingInstruction = "",
                             Dose = a.dose_text != "" && usedosetext == "TRUE" ? "0.0001" : a.dosage_id,
                             DoseText = a.dose_text,
                             DoseUomId = a.dose_uom_id,
                             DrugId = a.item_id,
                             IsPrn = false,
                             PatientInformation = "",
                             Quantity = a.quantity,
                             Repeat = a.iteration,
                             MedicalEncounterEntryInterfaceId = a.prescription_sync_id
                         }).ToList();
                if (temp != null)
                {
                    temp.AddRange(items);
                    items = temp;
                }
                //if (model.compound_header.Count(p => p.is_additional == true) > 0)
                //{
                //    tempcomp = IUnitOfWorks.UnitOfWorkPharmacy().GetAdditionalCompound(model.header.OrganizationId, model.header.DoctorId, model.header.Admissionid, model.header.EncounterId, false);
                //    compheader = (from a in model.compound_header
                //                  where a.is_additional == true
                //                  orderby a.item_sequence
                //                  select new OrderItem
                //                  {
                //                      AdministrationFrequencyId = a.administration_frequency_id,
                //                      AdministrationInstruction = a.administration_instruction,
                //                      AdministrationRouteId = a.administration_route_id,
                //                      DispensingInstruction = "",
                //                      Dose = a.dose,
                //                      DoseText = "",
                //                      DoseUomId = a.dose_uom_id,
                //                      DrugId = long.Parse(ValueStorage.CompoundItem),
                //                      IsPrn = false,
                //                      PatientInformation = "",
                //                      Quantity = a.quantity,
                //                      Repeat = a.iter,
                //                      MedicalEncounterEntryInterfaceId = a.compound_header_sync_id
                //                  }).ToList();

                //    compdetail = (from a in model.compound_detail
                //                  join b in model.compound_header
                //                  on a.prescription_compound_header_id equals b.prescription_compound_header_id
                //                  where b.is_additional == true && a.is_additional == true
                //                  orderby a.item_sequence
                //                  select new OrderItem
                //                  {
                //                      AdministrationFrequencyId = a.administration_frequency_id == 0 ? 7 : a.administration_frequency_id,
                //                      AdministrationInstruction = "",
                //                      AdministrationRouteId = a.administration_route_id == 0 ? 1 : a.administration_route_id,
                //                      DispensingInstruction = "",
                //                      Dose = a.dose == "0.000" || a.dose == "" ? "1.0" : a.dose,
                //                      DoseText = "",
                //                      DoseUomId = a.dose_uom_id == 0 ? 226 : a.dose_uom_id,
                //                      DrugId = a.item_id,
                //                      IsPrn = false,
                //                      PatientInformation = "",
                //                      Quantity = (Decimal.Parse(a.quantity) * Decimal.Parse(b.quantity)).ToString(),
                //                      Repeat = 0,
                //                      MedicalEncounterEntryInterfaceId = a.compound_detail_sync_id
                //                  }).ToList();

                //    items.AddRange(compheader);
                //    items.AddRange(compdetail);

                //    if (tempcomp != null)
                //    {
                //        items.AddRange(tempcomp);
                //    }
                //}

                //SYNC DRUG TO HOPE
                if (items.Count() > 0)
                {
                    pres.MedicalOrderInterfaceId = Guid.Empty;
                    pres.MedicalOrderId = null;
                    pres.MedicalEncounterInterfaceId = model.header.EncounterId;
                    pres.SalesPriorityId = 1;
                    pres.Notes = model.header.PharmacyNotes;
                    pres.OrderItems = items;

                    string JsonString = JsonConvert.SerializeObject(pres);
                    syncStatus = IUnitOfWorks.UnitOfWorkSync().SyncPrescription(model.header.OrganizationId, model.header.Admissionid, JsonString);
                }
                else
                {
                    syncStatus = "success";
                }

                //SYNC CONSUMABLE TO HOPE
                if (model.prescription.Count(p => p.is_consumables == 1) > 0)
                {
                    SyncConsumables cons = new SyncConsumables();
                    List<OrderItemConsumables> itemcons = new List<OrderItemConsumables>();
                    List<OrderItemConsumables> tempcons = new List<OrderItemConsumables>();
                    tempcons = IUnitOfWorks.UnitOfWorkPharmacy().GetAdditionalConsumables(model.header.OrganizationId, model.header.Admissionid, model.header.EncounterId, 2);
                    itemcons = (from a in model.prescription
                                where a.is_consumables == 1
                                orderby a.item_sequence
                                select new OrderItemConsumables
                                {
                                    UsageInstruction = a.remarks,
                                    ItemId = a.item_id,
                                    PatientInformation = "",
                                    Quantity = a.quantity,
                                    DispensingInstruction = "",
                                    MedicalEncounterEntryInterfaceId = a.prescription_sync_id
                                }).ToList();
                    if (tempcons != null)
                    {
                        tempcons.AddRange(itemcons);
                        itemcons = tempcons;
                    }
                    cons.MedicalOrderInterfaceId        = Guid.Empty;
                    cons.MedicalOrderId                 = null;
                    cons.MedicalEncounterInterfaceId    = model.header.EncounterId;
                    cons.SalesPriorityId                = 1;
                    cons.Notes                          = model.header.PharmacyNotes;
                    cons.OrderItems                     = itemcons;

                    string JsonStringCons               = JsonConvert.SerializeObject(cons);
                    syncStatusCons                      = IUnitOfWorks.UnitOfWorkSync().SyncConsumables(model.header.OrganizationId, model.header.Admissionid, JsonStringCons);
                }
                else
                {
                    syncStatusCons = "success";
                }

                if (model.header.is_SingleQueue)
                {
                    if (JsonSingleQWorklist != null)
                    {
                        dataWorklistSingleQ = IUnitOfWorks.UnitOfWorkSingleQueue().SyncWorklistSingleQ(JsonSingleQWorklist);
                        var objResult = JsonConvert.DeserializeObject<ResultListSingleQueue>(dataWorklistSingleQ);
                        status_SQ = objResult.status;
                        message_SQ = objResult.message;
                        if (status_SQ.Equals("OK"))
                        {
                            model_SQ = objResult.data[0];
                        }
                    }
                    else
                    {
                        status_SQ = "Fail";
                        dataWorklistSingleQ = "SIGNLEQDATANULL";
                        message_SQ = "SingleQueue Worklist Null";
                    }
                }

                Param_RecordSubmit paramRS          = new Param_RecordSubmit();
                paramRS.SubmitBy                    = SubmitBy;
                paramRS.TransAdmId                  = transAdmId;
                paramRS.TransAdmNo                  = transAdmNo;
                paramRS.QueueNo                     = QueueNo;
                paramRS.DeliveryFee                 = "0";
                paramRS.PayerCoverage               = "0";
                paramRS.ResponseWorklistSingleQueue = dataWorklistSingleQ;
                paramRS.PharmacyDataModel           = model;
                paramRS.SingleQueueDataModel        = model_SQ;

                string data             = "";
                string syncSQStatus     = "|" + status_SQ + "|" + message_SQ + "|";
                if (syncStatus.ToLower() == "success" && syncStatusCons.ToLower() == "success")
                {
                    //string data = IUnitOfWorks.UnitOfWorkPharmacy().UpdateRecordAdditional(SubmitBy, transAdmId, transAdmNo, QueueNo, model);
                    data = IUnitOfWorks.UnitOfWorkPharmacy().UpdateRecordAdditional(paramRS);

                    messageHubContexts.Clients.All.InvokeAsync("Update Submit Data", data);
                    HttpResults = new ResponseData<string>("Data successfully updated", Siloam.System.Web.StatusCode.OK, StatusMessage.Success, data + "|SUCCESS|" + syncSQStatus, page, total);
                }
                else if (syncStatus.ToLower() == "success" && syncStatusCons.ToLower() != "success")
                {
                    //model.prescription.RemoveAll(x => x.is_consumables == 1);
                    //string data = IUnitOfWorks.UnitOfWorkPharmacy().UpdateRecordAdditional(SubmitBy, transAdmId, transAdmNo, QueueNo, model);
                    data = IUnitOfWorks.UnitOfWorkPharmacy().UpdateRecordAdditional(paramRS);
                    messageHubContexts.Clients.All.InvokeAsync("Update Submit Data", data);
                    HttpResults = new ResponseData<string>("Data successfully updated", Siloam.System.Web.StatusCode.OK, StatusMessage.Success, data + "|CONSUMABLEFAILED|" + syncStatusCons + syncSQStatus, page, total);
                }
                else if (syncStatus.ToLower() != "success" && syncStatusCons.ToLower() == "success")
                {
                    //model.prescription.RemoveAll(x => x.is_consumables == 0);
                    //string data = IUnitOfWorks.UnitOfWorkPharmacy().UpdateRecordAdditional(SubmitBy, transAdmId, transAdmNo, QueueNo, model);
                    data = IUnitOfWorks.UnitOfWorkPharmacy().UpdateRecordAdditional(paramRS);
                    messageHubContexts.Clients.All.InvokeAsync("Update Submit Data", data);
                    HttpResults = new ResponseData<string>("Data successfully updated", Siloam.System.Web.StatusCode.OK, StatusMessage.Success, data + "|DRUGFAILED|" + syncStatus + syncSQStatus, page, total);
                }
                else
                {
                    //string data = IUnitOfWorks.UnitOfWorkPharmacy().UpdateRecordAdditional(SubmitBy, transAdmId, transAdmNo, QueueNo, model);
                    data = IUnitOfWorks.UnitOfWorkPharmacy().UpdateRecordAdditional(paramRS);
                    messageHubContexts.Clients.All.InvokeAsync("Update Submit Data", data);
                    if (syncStatus.ToLower() == syncStatusCons.ToLower())
                    {
                        HttpResults = new ResponseData<string>("Data successfully updated", Siloam.System.Web.StatusCode.OK, StatusMessage.Success, data + "|HOPEFAILED|" + syncStatus + syncSQStatus, page, total);
                    }
                    else
                    {
                        HttpResults = new ResponseData<string>("Data successfully updated", Siloam.System.Web.StatusCode.OK, StatusMessage.Success, data + "|HOPEFAILED|Drug Failed: " + syncStatus + ", Consumable Failed: " + syncStatusCons + syncSQStatus, page, total);
                    }
                }

            }
            catch (Exception ex)
            {

                int exCode = ex.HResult;

                if (exCode == -2147467259)
                {

                    HttpResults = new ResponseMessage(Siloam.System.Web.StatusCode.InternalServerErrorException, StatusMessage.Error, ex.Message, total);
                    Task<string> slackNotification = SlackMessageAsync(ValueStorage.SlackUrl, ValueStorage.ApiName, ValueStorage.ApiUrl + "/additionalpharmacyrecord", "[POST]Submit Verify Additional Prescription", SubmitBy + "/" + jsonModel, ex.Message);

                }
                else
                {

                    HttpResults = new ResponseMessage(Siloam.System.Web.StatusCode.UnprocessableEntity, StatusMessage.Fail, ex.Message, total);
                    Task<string> slackNotification = SlackMessageAsync(ValueStorage.SlackUrl, ValueStorage.ApiName, ValueStorage.ApiUrl + "/additionalpharmacyrecord", "[POST]Submit Verify Additional Prescription", SubmitBy + "/" + jsonModel, ex.Message);

                }

            }

            response:
            return HttpResponse(HttpResults);

        }

        [HttpPost("submitprintadditionalpresscription/{UserName}/{QueueNo}/{SubmitBy:long}")]
        [ProducesResponseType(typeof(ResponseData<string>), 200)]
        public IActionResult submitPrintAdditionalPresscription(string UserName, string QueueNo, Int64 SubmitBy, [FromBody] PharmacyData model)
        {
            int page = 1;
            int total = 0;
            string jsonModel = JsonConvert.SerializeObject(model);
            string JsonSingleQWorklist = JsonConvert.SerializeObject(model.singleQWorklistData);
            string usedosetext = "";

            try
            {
                LogSyncToHope logSyncDrugToHope = new LogSyncToHope();
                SubmitPrintPrescription pressResultData         = new SubmitPrintPrescription();
                Param_SyncResult paramSyncResultmodel           = new Param_SyncResult();
                Param_RecordSubmit paramRS                      = new Param_RecordSubmit();
                SingleQueue model_SQ                            = new SingleQueue();

                // HOPE
                //bool isFlagHope = true;
                string syncHopeStatus = "";
                string syncHopeMessage = "";
                int syncHopeCode = 0;
                List<DrugItem> syncHopeData = new List<DrugItem>();

                //SignleQueue
                string dataWorklistSingleQ = "SIGNLEQSUCCESS";
                string syncSQStatus = "OK";
                string syncSQMessage = "OK";

                if (model == null)
                {
                    pressResultData.SubmitStatusPressResult = "PharmacyData is null";
                    pressResultData.HopeStatusResult = "PharmacyData is null";
                    pressResultData.HopeMessageResult = "PharmacyData is null";
                    pressResultData.HopedataResult = syncHopeData;
                    pressResultData.SingleQStatusResult = "PharmacyData is null";
                    pressResultData.SingleQMessageResult = "PharmacyData is null";
                    HttpResults = new ResponseData<SubmitPrintPrescription>("Data UnSuccessfully updated and Get Data Pharmacy Print", Siloam.System.Web.StatusCode.UnprocessableEntity, StatusMessage.Fail, pressResultData, page, total);
                    Task<string> slackNotification = SlackMessageAsync(ValueStorage.SlackUrl, ValueStorage.ApiName, ValueStorage.ApiUrl + "/submitprintpresscriptionpharmacy", "[POST]Submit Verify Prescription and Item Issue", SubmitBy + "/" + jsonModel, "Data Unsuccessfully Created");
                    goto response;
                }

                long transAdmId = model.header.Admissionid;
                string transAdmNo = model.header.AdmissionNo;
                

                usedosetext = IUnitOfWorks.UnitOfWorkPharmacy().GetSettingDoseText(model.header.OrganizationId);

                model.prescription.ForEach(p => p.prescription_sync_id = Guid.NewGuid());
                model.compound_header.ForEach(p => p.compound_header_sync_id = Guid.NewGuid());
                model.compound_detail.ForEach(p => p.compound_detail_sync_id = Guid.NewGuid());
                model.additionalItem.ForEach(p => p.additional_item_sync_id = Guid.NewGuid());


                //SyncPrescription pres = new SyncPrescription();
                if (model.header.is_SendDataItemIssue)
                {
                    SyncPrescriptionItemIssue presItemIssue = new SyncPrescriptionItemIssue();

                    List<itemIssue> items = new List<itemIssue>();
                    List<itemIssue> temp = new List<itemIssue>();
                    List<itemIssue> compoundItems = new List<itemIssue>();
                    List<itemIssue> additionalItems = new List<itemIssue>();
                    logSyncDrugToHope.startTime = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"));
                    logSyncDrugToHope.organization_hope_id = model.header.OrganizationId;
                    logSyncDrugToHope.admission_hope_id = model.header.Admissionid;
                    logSyncDrugToHope.store_hope_id = model.header.store_id;
                    logSyncDrugToHope.doctor_hope_id = model.header.DoctorId;
                    logSyncDrugToHope.user_hope_id = SubmitBy;
                    logSyncDrugToHope.admission_hope_id_SentHope = model.header.Admissionid_SentHope;

                    items = (from a in model.prescription
                             where decimal.Parse(a.IssuedQty == null ? "0" : a.IssuedQty) > 0 && a.is_SentHope
                             orderby a.item_sequence
                             select new itemIssue
                             {
                                 itemId = a.item_id,
                                 quantity = decimal.Parse(a.IssuedQty),
                                 uomid = a.uom_id
                             }).ToList();

                    compoundItems = (from a in model.compound_detail
                                     where decimal.Parse(a.IssuedQty == null ? "0" : a.IssuedQty) > 0 && a.is_SentHope
                                     orderby a.item_sequence
                                     select new itemIssue
                                     {
                                         itemId = a.item_id,
                                         quantity = decimal.Parse(a.IssuedQty),
                                         uomid = a.uom_id
                                     }).ToList();

                    additionalItems = (from a in model.additionalItem
                                       where decimal.Parse(a.issued_qty == null ? "0" : a.issued_qty) > 0
                                       orderby a.item_sequence
                                       select new itemIssue
                                       {
                                           itemId = a.item_id,
                                           quantity = decimal.Parse(a.issued_qty),
                                           uomid = a.uom_id
                                       }).ToList();

                    if (compoundItems != null
                        && compoundItems.Count > 0)
                    {
                        items.AddRange(compoundItems);
                    }

                    if (additionalItems != null
                        && additionalItems.Count > 0)
                    {
                        items.AddRange(additionalItems);
                    }
                    //SYNC DRUG TO HOPE
                    if (items.Count() > 0)
                    {
                        itemIssued test = new itemIssued();
                        test.itemIssue = items;

                        List<DrugItem> listDrugItem = new List<DrugItem>();
                        presItemIssue.MedicalOrderInterfaceId = Guid.Empty;
                        presItemIssue.MedicalOrderId = null;
                        presItemIssue.MedicalEncounterInterfaceId = model.header.EncounterId;
                        presItemIssue.SalesPriorityId = 1;
                        presItemIssue.itemIssued = test;

                        string JsonString = JsonConvert.SerializeObject(presItemIssue);
                        logSyncDrugToHope.jsonrequest_senditemissue = JsonString;

                        var syncAdditionalItemIssue = IUnitOfWorks.UnitOfWorkSync().SyncPrescriptionToHope(model.header.OrganizationId, model.header.Admissionid_SentHope, model.header.store_id, model.header.DoctorId, model.header.HopeUserId, JsonString);
                        var jsonDeserializeAdditionalItemIssue = JsonConvert.DeserializeObject<DrugItemResponse>(syncAdditionalItemIssue.ToString());

                        if (jsonDeserializeAdditionalItemIssue != null)
                        {
                            logSyncDrugToHope.jsonresponse_sendItemIssue = JsonConvert.SerializeObject(jsonDeserializeAdditionalItemIssue);
                            syncHopeStatus = jsonDeserializeAdditionalItemIssue.status;
                            syncHopeMessage = jsonDeserializeAdditionalItemIssue.message;
                            syncHopeCode = jsonDeserializeAdditionalItemIssue.code;
                            if (jsonDeserializeAdditionalItemIssue.data.Count > 0)
                            {
                                syncHopeData = jsonDeserializeAdditionalItemIssue.data;
                                listDrugItem = jsonDeserializeAdditionalItemIssue.data;
                            }
                        }
                        else
                        {
                            logSyncDrugToHope.jsonresponse_sendItemIssue = "json Deserialize Additional Item Issue returns null data";
                        }

                        if (syncHopeCode == 200 )//syncHopeStatus.ToLower() == "success")
                        {

                            model.prescription = (from a in model.prescription
                                                  join b in listDrugItem on a.item_id equals b.ItemId
                                                  into c
                                                  from b in c.DefaultIfEmpty()
                                                  select new PharmacyPrescription
                                                  {
                                                      administration_route_code = a.administration_route_code,
                                                      administration_route_id   = a.administration_route_id,
                                                      compound_id               = a.compound_id,
                                                      compound_name             = a.compound_name,
                                                      doctor_name               = a.doctor_name,
                                                      dosage_id                 = a.dosage_id,
                                                      dose_text                 = a.dose_text,
                                                      dose_uom                  = a.dose_uom,
                                                      dose_uom_id               = a.dose_uom_id,
                                                      frequency_code            = a.frequency_code,
                                                      frequency_id              = a.frequency_id,
                                                      IssuedQty                 = a.IssuedQty,
                                                      is_consumables            = a.is_consumables,
                                                      is_routine                = a.is_routine,
                                                      item_id                   = a.item_id,
                                                      item_name                 = a.item_name,
                                                      item_sequence             = a.item_sequence,
                                                      iteration                 = a.iteration,
                                                      MainStoreQuantity         = a.MainStoreQuantity,
                                                      origin_prescription_id    = a.origin_prescription_id,
                                                      prescription_id           = a.prescription_id,
                                                      prescription_no           = a.prescription_no,
                                                      prescription_sync_id      = a.prescription_sync_id,
                                                      quantity                  = a.quantity,
                                                      remarks                   = a.remarks,
                                                      SubStoreQuantity          = a.SubStoreQuantity,
                                                      uom_code                  = a.uom_code,
                                                      uom_id                    = a.uom_id,
                                                      hope_aritem_id            = b == null ? 0 : long.Parse(b.ARItemId.ToString()),
                                                      is_SentHope               = a.is_SentHope
                                                  }).ToList();

                            model.compound_detail = (from a in model.compound_detail
                                                     join b in listDrugItem on a.item_id equals b.ItemId
                                                     into c
                                                     from b in c.DefaultIfEmpty()
                                                     select new PharmacyCompoundDetail
                                                     {
                                                         prescription_compound_detail_id    = a.prescription_compound_detail_id,
                                                         prescription_compound_header_id    = a.prescription_compound_header_id,
                                                         item_id                            = a.item_id,
                                                         item_name                          = a.item_name,
                                                         quantity                           = a.quantity,
                                                         IssuedQty                          = a.IssuedQty == null ? "0" : a.IssuedQty,
                                                         uom_id                             = a.uom_id,
                                                         uom_code                           = a.uom_code,
                                                         administration_frequency_id        = a.administration_frequency_id,
                                                         administration_route_id            = a.administration_route_id,
                                                         item_sequence                      = a.item_sequence,
                                                         is_additional                      = a.is_additional,
                                                         SubStoreQuantity                   = a.SubStoreQuantity,
                                                         MainStoreQuantity                  = a.MainStoreQuantity,
                                                         compound_detail_sync_id            = a.compound_detail_sync_id,
                                                         dose                               = a.dose,
                                                         dose_uom_id                        = a.dose_uom_id,
                                                         dose_uom_code                      = a.dose_uom_code,
                                                         dose_text                          = a.dose_text,
                                                         IsDoseText                         = a.IsDoseText,
                                                         hope_aritem_id                     = b == null ? 0 : long.Parse(b.ARItemId.ToString()),
                                                         is_SentHope                        = a.is_SentHope
                                                     }).ToList();
                        }

                        model.additionalItem = (from a in model.additionalItem
                                                join b in listDrugItem on a.item_id equals b.ItemId
                                                 into c
                                                from b in c.DefaultIfEmpty()
                                                select new PharmacyAdditionalItem
                                                {
                                                    pharmacy_additional_item_id = a.pharmacy_additional_item_id,
                                                    item_id = a.item_id,
                                                    item_name = a.item_name,
                                                    quantity = a.quantity,
                                                    issued_qty = a.issued_qty,
                                                    uom_id = a.uom_id,
                                                    uom_code = a.uom_code,
                                                    hope_aritem_id = b == null ? 0 : long.Parse(b.ARItemId.ToString()),
                                                    item_sequence = a.item_sequence,
                                                    additional_item_sync_id = a.additional_item_sync_id
                                                }).ToList();

                    }
                    else
                    {
                        string JsonString = JsonConvert.SerializeObject(presItemIssue);
                        logSyncDrugToHope.jsonrequest_senditemissue = JsonString;
                        syncHopeStatus = "success";
                        syncHopeCode = 200;
                        logSyncDrugToHope.jsonresponse_sendItemIssue = "there are no items that need to be sent to hope";
                    }
                    string data = IUnitOfWorks.UnitOfWorkSync().InsertLogItemsIssue(logSyncDrugToHope);
                }
                else
                {
                    SyncPrescription pres = new SyncPrescription();
                    List<OrderItem> items = new List<OrderItem>();
                    List<OrderItem> temp = new List<OrderItem>();
                    string syncStatusCons = "";
                    string syncStatusDrugs = "";
                    temp = IUnitOfWorks.UnitOfWorkPharmacy().GetAdditional(model.header.OrganizationId, model.header.Admissionid, model.header.EncounterId, 2, usedosetext);
                    items = (from a in model.prescription
                             where a.is_consumables == 0
                             orderby a.item_sequence
                             select new OrderItem
                             {
                                 AdministrationFrequencyId          = a.frequency_id,
                                 AdministrationInstruction          = a.remarks,
                                 AdministrationRouteId              = a.administration_route_id,
                                 DispensingInstruction              = "",
                                 Dose                               = a.dose_text != "" && usedosetext == "TRUE" ? "0.0001" : a.dosage_id,
                                 DoseText                           = a.dose_text,
                                 DoseUomId                          = a.dose_uom_id,
                                 DrugId                             = a.item_id,
                                 IsPrn                              = false,
                                 PatientInformation                 = "",
                                 Quantity                           = a.quantity,
                                 Repeat                             = a.iteration,
                                 MedicalEncounterEntryInterfaceId   = a.prescription_sync_id
                             }).ToList();
                    if (temp != null)
                    {
                        temp.AddRange(items);
                        items = temp;
                    }
                    

                    //SYNC DRUG TO HOPE
                    if (items.Count() > 0)
                    {
                        pres.MedicalOrderInterfaceId        = Guid.Empty;
                        pres.MedicalOrderId                 = null;
                        pres.MedicalEncounterInterfaceId    = model.header.EncounterId;
                        pres.SalesPriorityId                = 1;
                        pres.Notes                          = model.header.PharmacyNotes;
                        pres.OrderItems                     = items;

                        string JsonString       = JsonConvert.SerializeObject(pres);
                        syncStatusDrugs         = IUnitOfWorks.UnitOfWorkSync().SyncPrescription(model.header.OrganizationId, model.header.Admissionid, JsonString);
                    }
                    else
                    {
                        syncStatusDrugs = "success";
                    }

                    //SYNC CONSUMABLE TO HOPE
                    if (model.prescription.Count(p => p.is_consumables == 1) > 0)
                    {
                        SyncConsumables cons                = new SyncConsumables();
                        List<OrderItemConsumables> itemcons = new List<OrderItemConsumables>();
                        List<OrderItemConsumables> tempcons = new List<OrderItemConsumables>();
                        tempcons = IUnitOfWorks.UnitOfWorkPharmacy().GetAdditionalConsumables(model.header.OrganizationId, model.header.Admissionid, model.header.EncounterId, 2);
                        itemcons = (from a in model.prescription
                                    where a.is_consumables == 1
                                    orderby a.item_sequence
                                    select new OrderItemConsumables
                                    {
                                        UsageInstruction                    = a.remarks,
                                        ItemId                              = a.item_id,
                                        PatientInformation                  = "",
                                        Quantity                            = a.quantity,
                                        DispensingInstruction               = "",
                                        MedicalEncounterEntryInterfaceId    = a.prescription_sync_id
                                    }).ToList();
                        if (tempcons != null)
                        {
                            tempcons.AddRange(itemcons);
                            itemcons = tempcons;
                        }
                        model.compound_detail = (from a in model.compound_detail
                                                 select new PharmacyCompoundDetail
                                                 {
                                                     prescription_compound_detail_id    = a.prescription_compound_detail_id,
                                                     prescription_compound_header_id    = a.prescription_compound_header_id,
                                                     item_id                            = a.item_id,
                                                     item_name                          = a.item_name,
                                                     quantity                           = a.quantity,
                                                     IssuedQty                          = a.IssuedQty == null ? "0" : a.IssuedQty,
                                                     uom_id                             = a.uom_id,
                                                     uom_code                           = a.uom_code,
                                                     administration_frequency_id        = a.administration_frequency_id,
                                                     administration_route_id            = a.administration_route_id,
                                                     item_sequence                      = a.item_sequence,
                                                     is_additional                      = a.is_additional,
                                                     SubStoreQuantity                   = a.SubStoreQuantity,
                                                     MainStoreQuantity                  = a.MainStoreQuantity,
                                                     compound_detail_sync_id            = a.compound_detail_sync_id,
                                                     dose                               = a.dose,
                                                     dose_uom_id                        = a.dose_uom_id,
                                                     dose_uom_code                      = a.dose_uom_code,
                                                     dose_text                          = a.dose_text,
                                                     IsDoseText                         = a.IsDoseText
                                                 }).ToList();
                        cons.MedicalOrderInterfaceId = Guid.Empty;
                        cons.MedicalOrderId = null;
                        cons.MedicalEncounterInterfaceId = model.header.EncounterId;
                        cons.SalesPriorityId = 1;
                        cons.Notes = model.header.PharmacyNotes;
                        cons.OrderItems = itemcons;

                        string JsonStringCons = JsonConvert.SerializeObject(cons);
                        syncStatusCons = IUnitOfWorks.UnitOfWorkSync().SyncConsumables(model.header.OrganizationId, model.header.Admissionid, JsonStringCons);
                    }
                    else
                    {
                        syncStatusCons = "success";
                    }

                    if (syncStatusDrugs.ToLower() == "success" && syncStatusCons.ToLower() == "success")
                    {
                        syncHopeStatus = "SUCCESS";
                        syncHopeMessage = "SUCCESS";
                    }
                    else if (syncStatusDrugs.ToLower() == "success" && syncStatusCons.ToLower() != "success")
                    {
                        syncHopeStatus = "CONSUMABLEFAILED";
                        syncHopeMessage = syncStatusCons;
                    }
                    else if (syncStatusDrugs.ToLower() != "success" && syncStatusCons.ToLower() == "success")
                    {
                        syncHopeStatus = "DRUGFAILED";
                        syncHopeMessage = syncStatusDrugs;
                    }
                    else
                    {
                        //isFlagHope = false;
                        syncHopeStatus = "HOPEFAILED";
                        syncHopeMessage = "Drug Failed: " + syncStatusDrugs + ", Consumable Failed: " + syncStatusCons;
                    }
                }
                    
                               

                if ((model.header.is_SingleQueue && !model.header.is_SendDataItemIssue)
                    || (model.header.is_SingleQueue 
                        && model.header.is_SendDataItemIssue 
                        && syncHopeCode == 200))
                {
                    if (JsonSingleQWorklist != null)
                    {
                        dataWorklistSingleQ = IUnitOfWorks.UnitOfWorkSingleQueue().SyncWorklistSingleQ(JsonSingleQWorklist);
                        var objResult = JsonConvert.DeserializeObject<ResultListSingleQueue>(dataWorklistSingleQ);
                        syncSQStatus = objResult.status;
                        syncSQMessage = objResult.message;
                        if (syncSQStatus.Equals("OK"))
                        {
                            model_SQ = objResult.data[0];
                        }
                    }
                    else
                    {
                        syncSQStatus = "Fail";
                        dataWorklistSingleQ = "SIGNLEQDATANULL";
                        syncSQMessage = "SingleQueue Worklist Null";
                    }
                }

                
                paramRS.SubmitBy = SubmitBy;
                paramRS.TransAdmId = transAdmId;
                paramRS.TransAdmNo = transAdmNo;
                paramRS.QueueNo = QueueNo;
                paramRS.DeliveryFee = "0";
                paramRS.PayerCoverage = "0";
                paramRS.ResponseWorklistSingleQueue = dataWorklistSingleQ;
                paramRS.PharmacyDataModel = model;
                paramRS.SingleQueueDataModel = model_SQ;

                paramSyncResultmodel.HopeStatusResult = syncHopeStatus;
                paramSyncResultmodel.HopeMessageResult = syncHopeMessage;
                paramSyncResultmodel.HopedataResult = syncHopeData;
                paramSyncResultmodel.SingleQStatusResult = syncSQStatus;
                paramSyncResultmodel.SingleQMessageResult = syncSQMessage;

                //string data = "";
                if ((!model.header.is_SendDataItemIssue)
                    || (model.header.is_SendDataItemIssue && syncHopeCode == 200)//syncHopeStatus.ToLower() == "success".ToLower())
                    )
                {
                    pressResultData = IUnitOfWorks.UnitOfWorkPharmacy().submitPrintAdditionalPresscription(paramRS, paramSyncResultmodel);
                    //string updateFlagHope = IUnitOfWorks.UnitOfWorkPharmacy().UpdateFlagHOPE(model.header.OrganizationId, model.header.Admissionid, model.header.EncounterId);
                    messageHubContexts.Clients.All.InvokeAsync("Update Submit Data", pressResultData.SubmitStatusPressResult);
                    HttpResults = new ResponseData<SubmitPrintPrescription>("Data successfully updated and Get Data Pharmacy Print", Siloam.System.Web.StatusCode.OK, StatusMessage.Success, pressResultData, page, total);
                }
                else
                {
                    pressResultData.SubmitStatusPressResult = paramSyncResultmodel.HopeStatusResult;
                    pressResultData.HopeStatusResult = paramSyncResultmodel.HopeStatusResult;
                    pressResultData.HopeMessageResult = paramSyncResultmodel.HopeMessageResult;
                    pressResultData.HopedataResult = paramSyncResultmodel.HopedataResult;
                    pressResultData.SingleQStatusResult = paramSyncResultmodel.SingleQStatusResult;
                    pressResultData.SingleQMessageResult = paramSyncResultmodel.SingleQMessageResult;
                    HttpResults = new ResponseData<SubmitPrintPrescription>("Data UnSuccessfully updated and Get Data Pharmacy Print", Siloam.System.Web.StatusCode.UnprocessableEntity, StatusMessage.Fail, pressResultData, page, total);
                }
            }
            catch (Exception ex)
            {

                int exCode = ex.HResult;

                if (exCode == -2147467259)
                {

                    HttpResults = new ResponseMessage(Siloam.System.Web.StatusCode.InternalServerErrorException, StatusMessage.Error, ex.Message, total);
                    Task<string> slackNotification = SlackMessageAsync(ValueStorage.SlackUrl, ValueStorage.ApiName, ValueStorage.ApiUrl + "/submitprintadditionalpresscription", "[POST]Submit Print Additional Prescription", SubmitBy + "/" + jsonModel, ex.Message);

                }
                else
                {

                    HttpResults = new ResponseMessage(Siloam.System.Web.StatusCode.UnprocessableEntity, StatusMessage.Fail, ex.Message, total);
                    Task<string> slackNotification = SlackMessageAsync(ValueStorage.SlackUrl, ValueStorage.ApiName, ValueStorage.ApiUrl + "/submitprintadditionalpresscription", "[POST]Submit Print Additional Prescription", SubmitBy + "/" + jsonModel, ex.Message);

                }

            }

        response:
            return HttpResponse(HttpResults);

        }

        [HttpPost("additionalpharmacydruginfo/{OrganizationId:long}/{AdmissionId:long}/{EncounterId}/{Updater:long}")]
        [ProducesResponseType(typeof(ResponseData<string>), 200)]
        public IActionResult UpdateAdditionalDrugInfo(long OrganizationId, long AdmissionId, Guid EncounterId, long Updater, [FromBody]List<PharmacyDrugInfo> model)
        {

            int page = 1;
            int total = 0;

            try
            {

                string data = IUnitOfWorks.UnitOfWorkPharmacy().UpdateAdditionalDrugInfo(OrganizationId, AdmissionId, EncounterId, Updater, model);

                messageHubContexts.Clients.All.InvokeAsync("Update Additional Pharmacy Drug Info", data);

                HttpResults = new ResponseData<string>("Data successfully updated", Siloam.System.Web.StatusCode.OK, StatusMessage.Success, data, page, total);

            }
            catch (Exception ex)
            {

                int exCode = ex.HResult;
                string jsonModel = JsonConvert.SerializeObject(model);

                if (exCode == -2147467259)
                {

                    HttpResults = new ResponseMessage(Siloam.System.Web.StatusCode.InternalServerErrorException, StatusMessage.Error, ex.Message, total);
                    Task<string> slackNotification = SlackMessageAsync(ValueStorage.SlackUrl, ValueStorage.ApiName, ValueStorage.ApiUrl + "/pharmacydruginfo", "[POST]Update Drug Info", OrganizationId.ToString() + "/" + AdmissionId.ToString() + "/" + EncounterId.ToString() + "/" + Updater.ToString() + "/" + jsonModel.ToString(), ex.Message);

                }
                else
                {

                    HttpResults = new ResponseMessage(Siloam.System.Web.StatusCode.UnprocessableEntity, StatusMessage.Fail, ex.Message, total);
                    Task<string> slackNotification = SlackMessageAsync(ValueStorage.SlackUrl, ValueStorage.ApiName, ValueStorage.ApiUrl + "/pharmacydruginfo", "[POST]Update Drug Info", OrganizationId.ToString() + "/" + AdmissionId.ToString() + "/" + EncounterId.ToString() + "/" + Updater.ToString() + "/" + jsonModel.ToString(), ex.Message);

                }

            }

            response:
            return HttpResponse(HttpResults);

        }
        [HttpPost("pharmacytakeover/{UserId:long}/{OrganizationId:long}/{PatientId:long}/{EncounterId}/{AdmissionId:long}")]
        [ProducesResponseType(typeof(ResponseData<string>), 200)]
        public IActionResult UpdateTakeOver(long UserId, long OrganizationId, long PatientId, Guid EncounterId, long AdmissionId)
        {
            int page = 1;
            int total = 0;
            try
            {
                string data = IUnitOfWorks.UnitOfWorkPharmacy().UpdateTakeOver(UserId, OrganizationId, PatientId, EncounterId, AdmissionId);
                messageHubContexts.Clients.All.InvokeAsync("Update Submit Data", data);
                HttpResults = new ResponseData<string>("Data successfully updated", Siloam.System.Web.StatusCode.OK, StatusMessage.Success, data, page, total);
            }
            catch (Exception ex)
            {
                int exCode = ex.HResult;
                if (exCode == -2147467259)
                {
                    HttpResults = new ResponseMessage(Siloam.System.Web.StatusCode.InternalServerErrorException, StatusMessage.Error, ex.Message, total);
                    Task<string> slackNotification = SlackMessageAsync(ValueStorage.SlackUrl, ValueStorage.ApiName, ValueStorage.ApiUrl + "/pharmacytakeover", "[POST]Take Prescription", UserId.ToString() + "/" + EncounterId.ToString(), ex.Message);
                }
                else
                {
                    HttpResults = new ResponseMessage(Siloam.System.Web.StatusCode.UnprocessableEntity, StatusMessage.Fail, ex.Message, total);
                    Task<string> slackNotification = SlackMessageAsync(ValueStorage.SlackUrl, ValueStorage.ApiName, ValueStorage.ApiUrl + "/pharmacytakeover", "[POST]Take Prescription", UserId.ToString() + "/" + EncounterId.ToString(), ex.Message);
                }
            }

        response:
            return HttpResponse(HttpResults);

        }

        [HttpPost("pharmacytake/{SubmitBy:long}")]
        [ProducesResponseType(typeof(ResponseData<string>), 200)]
        public IActionResult UpdateRecordTake(Int64 SubmitBy, [FromBody]PharmacyData model)
        {

            int page = 1;
            int total = 0;
            //List<PharmacyCompoundHeader> compoundHeaders = new List<PharmacyCompoundHeader>();
            //List<PharmacyCompoundDetail> compoundDetails = new List<PharmacyCompoundDetail>();
            //model.compound_header = compoundHeaders;
            //model.compound_detail = compoundDetails;
            string jsonModel = JsonConvert.SerializeObject(model);

            if (model == null)
            {

                HttpResults = new ResponseMessage(Siloam.System.Web.StatusCode.BadRequest, StatusMessage.Fail, HttpResponseMessageKey.DataUnsuccessfullyCreated, total);
                Task<string> slackNotification = SlackMessageAsync(ValueStorage.SlackUrl, ValueStorage.ApiName, ValueStorage.ApiUrl + "/pharmacytake", "[POST]Take Prescription", SubmitBy + "/" + jsonModel, "Data Unsuccessfully Created");
                goto response;

            }

            try
            {

                string data = IUnitOfWorks.UnitOfWorkPharmacy().UpdateRecordTake(model.header.OrganizationId, model.header.PatientId, model.header.EncounterId,model.header.Admissionid,model.LastModifiedDate,SubmitBy);

                messageHubContexts.Clients.All.InvokeAsync("Update Submit Data", data);

                HttpResults = new ResponseData<string>("Data successfully updated", Siloam.System.Web.StatusCode.OK, StatusMessage.Success, data, page, total);

            }
            catch (Exception ex)
            {

                int exCode = ex.HResult;

                if (exCode == -2147467259)
                {

                    HttpResults = new ResponseMessage(Siloam.System.Web.StatusCode.InternalServerErrorException, StatusMessage.Error, ex.Message, total);
                    Task<string> slackNotification = SlackMessageAsync(ValueStorage.SlackUrl, ValueStorage.ApiName, ValueStorage.ApiUrl + "/pharmacytake", "[POST]Take Prescription", SubmitBy + "/" + jsonModel, ex.Message);

                }
                else
                {

                    HttpResults = new ResponseMessage(Siloam.System.Web.StatusCode.UnprocessableEntity, StatusMessage.Fail, ex.Message, total);
                    Task<string> slackNotification = SlackMessageAsync(ValueStorage.SlackUrl, ValueStorage.ApiName, ValueStorage.ApiUrl + "/pharmacytake", "[POST]Take Prescription", SubmitBy + "/" + jsonModel, ex.Message);

                }

            }

            response:
            return HttpResponse(HttpResults);

        }

        [HttpPost("additionalpharmacytake/{SubmitBy:long}")]
        [ProducesResponseType(typeof(ResponseData<string>), 200)]
        public IActionResult UpdateRecordAdditionalTake(Int64 SubmitBy, [FromBody]PharmacyData model)
        {

            int page = 1;
            int total = 0;
            //List<PharmacyCompoundHeader> compoundHeaders = new List<PharmacyCompoundHeader>();
            //List<PharmacyCompoundDetail> compoundDetails = new List<PharmacyCompoundDetail>();
            //model.compound_header = compoundHeaders;
            //model.compound_detail = compoundDetails;
            string jsonModel = JsonConvert.SerializeObject(model);

            if (model == null)
            {

                HttpResults = new ResponseMessage(Siloam.System.Web.StatusCode.BadRequest, StatusMessage.Fail, HttpResponseMessageKey.DataUnsuccessfullyCreated, total);
                Task<string> slackNotification = SlackMessageAsync(ValueStorage.SlackUrl, ValueStorage.ApiName, ValueStorage.ApiUrl + "/additionalpharmacytake", "[POST]Take Additional Prescription", SubmitBy + "/" + jsonModel, "Data Unsuccessfully Created");
                goto response;

            }

            try
            {

                string data = IUnitOfWorks.UnitOfWorkPharmacy().UpdateRecordAdditionalTake(model.header.OrganizationId, model.header.PatientId, model.header.EncounterId, model.header.Admissionid, model.LastModifiedDate, SubmitBy);

                messageHubContexts.Clients.All.InvokeAsync("Update Submit Data", data);

                HttpResults = new ResponseData<string>("Data successfully updated", Siloam.System.Web.StatusCode.OK, StatusMessage.Success, data, page, total);

            }
            catch (Exception ex)
            {

                int exCode = ex.HResult;

                if (exCode == -2147467259)
                {

                    HttpResults = new ResponseMessage(Siloam.System.Web.StatusCode.InternalServerErrorException, StatusMessage.Error, ex.Message, total);
                    Task<string> slackNotification = SlackMessageAsync(ValueStorage.SlackUrl, ValueStorage.ApiName, ValueStorage.ApiUrl + "/additionalpharmacytake", "[POST]Take Additional Prescription", SubmitBy + "/" + jsonModel, ex.Message);

                }
                else
                {

                    HttpResults = new ResponseMessage(Siloam.System.Web.StatusCode.UnprocessableEntity, StatusMessage.Fail, ex.Message, total);
                    Task<string> slackNotification = SlackMessageAsync(ValueStorage.SlackUrl, ValueStorage.ApiName, ValueStorage.ApiUrl + "/additionalpharmacytake", "[POST]Take Additional Prescription", SubmitBy + "/" + jsonModel, ex.Message);

                }

            }

            response:
            return HttpResponse(HttpResults);

        }

        [HttpPost("pharmacyissue/{Updater:long}/{StoreId:long}")]
        [ProducesResponseType(typeof(ResponseData<string>), 200)]
        public IActionResult InsertPharmacyTransaction(Int64 Updater, Int64 StoreId, [FromBody]List<PharmacyIssue> model)
        {

            int page = 1;
            int total = 0;
            string jsonModel = JsonConvert.SerializeObject(model);
            if (model == null)
            {

                HttpResults = new ResponseMessage(Siloam.System.Web.StatusCode.BadRequest, StatusMessage.Fail, HttpResponseMessageKey.DataUnsuccessfullyCreated, total);
                goto response;

            }

            try
            {

                Int64 TransAdmissionId = 2000003982103; //Call API HOPE

                string data = IUnitOfWorks.UnitOfWorkPharmacy().InsertPharmacyTransaction(Updater, StoreId, TransAdmissionId, model);

                messageHubContexts.Clients.All.InvokeAsync("Update Submit Data", data);

                HttpResults = new ResponseData<string>("Data successfully updated", Siloam.System.Web.StatusCode.OK, StatusMessage.Success, data, page, total);

            }
            catch (Exception ex)
            {

                int exCode = ex.HResult;

                if (exCode == -2147467259)
                {

                    HttpResults = new ResponseMessage(Siloam.System.Web.StatusCode.InternalServerErrorException, StatusMessage.Error, ex.Message, total);
                    Task<string> slackNotification = SlackMessageAsync(ValueStorage.SlackUrl, ValueStorage.ApiName, ValueStorage.ApiUrl + "/pharmacyissue", "[POST]Insert Pharmacy Transaction", Updater + "/" + jsonModel, ex.Message);

                }
                else
                {

                    HttpResults = new ResponseMessage(Siloam.System.Web.StatusCode.UnprocessableEntity, StatusMessage.Fail, ex.Message, total);
                    Task<string> slackNotification = SlackMessageAsync(ValueStorage.SlackUrl, ValueStorage.ApiName, ValueStorage.ApiUrl + "/pharmacyissue", "[POST]Insert Pharmacy Transaction", Updater + "/" + jsonModel, ex.Message);

                }

            }

            response:
            return HttpResponse(HttpResults);

        }

        [HttpGet("pharmacyissue/{StoreId:long}/{AdmissionId:long}/{OrganizationId:long}")]
        [ProducesResponseType(typeof(ResponseData<List<PharmacyIssue>>), 200)]
        public IActionResult GetDataPharmacyIssue(Int64 StoreId, Int64 AdmissionId, Int64 OrganizationId)
        {

            int total = 0, cp = 0;

            try
            {

                var data = IUnitOfWorks.UnitOfWorkPharmacy().GetDataPharmacyIssue(StoreId, AdmissionId, OrganizationId);

                HttpResults = new ResponseData<List<PharmacyIssue>>("Get Data Pharmacy Issue", Siloam.System.Web.StatusCode.OK, StatusMessage.Success, data, cp, total);

            }
            catch (Exception ex)
            {

                int exCode = ex.HResult;
                string paramslack = StoreId.ToString()+"/"+AdmissionId.ToString()+"/"+OrganizationId.ToString();
                if (exCode == -2147467259)
                {
                    HttpResults = new ResponseMessage(Siloam.System.Web.StatusCode.InternalServerErrorException, StatusMessage.Error, ex.Message, total);
                    Task<string> slackNotification = SlackMessageAsync(ValueStorage.SlackUrl, ValueStorage.ApiName, ValueStorage.ApiUrl + "/pharmacyissue", "[GET]Get Data PharmacyIssue Transaction by StoreId, AdmissionId, OrganizationId", paramslack.ToString(), ex.Message);
                }
                else
                {
                    HttpResults = new ResponseMessage(Siloam.System.Web.StatusCode.UnprocessableEntity, StatusMessage.Fail, ex.Message, total);
                    Task<string> slackNotification = SlackMessageAsync(ValueStorage.SlackUrl, ValueStorage.ApiName, ValueStorage.ApiUrl + "/pharmacyissue", "[GET]Get Data PharmacyIssue Transaction by StoreId, AdmissionId, OrganizationId", paramslack.ToString(), ex.Message);
                }

            }

            return HttpResponse(HttpResults);
        }

        [HttpPut("pharmacyrecord/{EncounterId}/{Updater:long}/{OrganizationId:long}/{PatientId:long}/{AdmissionId:long}/{Remarks}")]
        [ProducesResponseType(typeof(ResponseData<PharmacyRecord>), 200)]
        public IActionResult UpdateRecordData(Guid EncounterId, long Updater, long OrganizationId, long PatientId, long AdmissionId, string Remarks)
        {

            int total = 0;

            try
            {

                Nullable<Int64> TakeBy = 0;
                int CountSpv = 0;
                Nullable<DateTime> SubmitDate;
                TakeBy = IUnitOfWorks.UnitOfWorkPharmacy().GetTakeByEncounterId(EncounterId);
                CountSpv = IUnitOfWorks.UnitOfWorkPharmacy().CountSupervisor(Updater, OrganizationId);
                SubmitDate = IUnitOfWorks.UnitOfWorkPharmacy().GetVerifyDate(EncounterId);

                if (TakeBy != Updater && TakeBy != 999999999999 //bila di butuhkan ketika pharmacy RollBack
                    && CountSpv == 0)
                {
                    HttpResults = new ResponseMessage(Siloam.System.Web.StatusCode.OK, StatusMessage.Fail, "Prescription already belong to other pharmacist", total);
                }
                else if (SubmitDate != null)
                {
                    HttpResults = new ResponseMessage(Siloam.System.Web.StatusCode.OK, StatusMessage.Fail, "Prescription already verified", total);
                }
                else
                {

                    PharmacyRecord data = IUnitOfWorks.UnitOfWorkPharmacy().UpdateRecordData(EncounterId, Updater, OrganizationId, PatientId, AdmissionId, Remarks);

                    messageHubContexts.Clients.All.InvokeAsync("Update Take Prescription", data);

                    HttpResults = new ResponseData<PharmacyRecord>("Data successfully updated", Siloam.System.Web.StatusCode.OK, StatusMessage.Success, data);
                    
                }

            }
            catch (Exception ex)
            {

                int exCode = ex.HResult;

                if (exCode == -2147467259)
                {
                    HttpResults = new ResponseMessage(Siloam.System.Web.StatusCode.InternalServerErrorException, StatusMessage.Error, ex.Message, total);
                    Task<string> slackNotification = SlackMessageAsync(ValueStorage.SlackUrl, ValueStorage.ApiName, ValueStorage.ApiUrl + "/pharmacyrecord", "[PUT]Untake Pharmacy Record", EncounterId.ToString() + "/" + OrganizationId.ToString() + "/" + Updater.ToString() + "/" + Remarks.ToString(), ex.Message);
                }
                else
                {
                    HttpResults = new ResponseMessage(Siloam.System.Web.StatusCode.UnprocessableEntity, StatusMessage.Fail, ex.Message, total);
                    Task<string> slackNotification = SlackMessageAsync(ValueStorage.SlackUrl, ValueStorage.ApiName, ValueStorage.ApiUrl + "/pharmacyrecord", "[PUT]Untake Pharmacy Record", EncounterId.ToString() + "/" + OrganizationId.ToString() + "/" + Updater.ToString() + "/" + Remarks.ToString(), ex.Message);
                }

            }

            return HttpResponse(HttpResults);
        }

        [HttpPut("additionalpharmacyrecord/{EncounterId}/{Updater:long}/{OrganizationId:long}/{PatientId:long}/{AdmissionId:long}/{Remarks}")]
        [ProducesResponseType(typeof(ResponseData<AdditionalPharmacyRecord>), 200)]
        public IActionResult UpdateAdditionalUntake(Guid EncounterId, long Updater, long OrganizationId, long PatientId, long AdmissionId, string Remarks)
        {

            int total = 0;

            try
            {

                Nullable<Int64> TakeBy = 0;
                int CountSpv = 0;
                Nullable<DateTime> SubmitDate;
                TakeBy = IUnitOfWorks.UnitOfWorkPharmacy().GetAdditionalTakeByEncounterId(EncounterId);
                CountSpv = IUnitOfWorks.UnitOfWorkPharmacy().CountSupervisor(Updater, OrganizationId);
                SubmitDate = IUnitOfWorks.UnitOfWorkPharmacy().GetAdditionalVerifyDate(EncounterId);

                if (TakeBy != Updater && CountSpv == 0)
                {
                    HttpResults = new ResponseMessage(Siloam.System.Web.StatusCode.OK, StatusMessage.Fail, "Additional Prescription already belong to other pharmacist", total);
                }
                else if (SubmitDate != null)
                {
                    HttpResults = new ResponseMessage(Siloam.System.Web.StatusCode.OK, StatusMessage.Fail, "Additional Prescription already verified", total);
                }
                else
                {

                    AdditionalPharmacyRecord data = IUnitOfWorks.UnitOfWorkPharmacy().UpdateAdditionalUntake(EncounterId, Updater, OrganizationId, PatientId, AdmissionId, Remarks);

                    messageHubContexts.Clients.All.InvokeAsync("Update Take Additional Prescription", data);

                    HttpResults = new ResponseData<AdditionalPharmacyRecord>("Data successfully updated", Siloam.System.Web.StatusCode.OK, StatusMessage.Success, data);

                }

            }
            catch (Exception ex)
            {

                int exCode = ex.HResult;

                if (exCode == -2147467259)
                {
                    HttpResults = new ResponseMessage(Siloam.System.Web.StatusCode.InternalServerErrorException, StatusMessage.Error, ex.Message, total);
                    Task<string> slackNotification = SlackMessageAsync(ValueStorage.SlackUrl, ValueStorage.ApiName, ValueStorage.ApiUrl + "/additionalpharmacyrecord", "[PUT]Untake Additional Pharmacy Record", EncounterId.ToString() + "/" + OrganizationId.ToString() + "/" + Updater.ToString() + "/" + Remarks.ToString(), ex.Message);
                }
                else
                {
                    HttpResults = new ResponseMessage(Siloam.System.Web.StatusCode.UnprocessableEntity, StatusMessage.Fail, ex.Message, total);
                    Task<string> slackNotification = SlackMessageAsync(ValueStorage.SlackUrl, ValueStorage.ApiName, ValueStorage.ApiUrl + "/additionalpharmacyrecord", "[PUT]Untake Additional Pharmacy Record", EncounterId.ToString() + "/" + OrganizationId.ToString() + "/" + Updater.ToString() + "/" + Remarks.ToString(), ex.Message);
                }

            }

            return HttpResponse(HttpResults);
        }

        [HttpGet("pharmacytranshistory/{PrescriptionId:long}/{Mode:int}")]
        [ProducesResponseType(typeof(ResponseData<List<PharmacyTransactionHistory>>), 200)]
        public IActionResult GetPharmacyTransactionHistory(Int64 PrescriptionId, int Mode)
        {

            int total = 0, cp = 0;

            try
            {

                var data = IUnitOfWorks.UnitOfWorkPharmacy().GetPharmacyTransactionHistory(PrescriptionId, Mode);

                HttpResults = new ResponseData<List<PharmacyTransactionHistory>>("Get Data Pharmacy Issue", Siloam.System.Web.StatusCode.OK, StatusMessage.Success, data, cp, total);

            }
            catch (Exception ex)
            {

                int exCode = ex.HResult;

                if (exCode == -2147467259)
                {
                    HttpResults = new ResponseMessage(Siloam.System.Web.StatusCode.InternalServerErrorException, StatusMessage.Error, ex.Message, total);
                    Task<string> slackNotification = SlackMessageAsync(ValueStorage.SlackUrl, ValueStorage.ApiName, ValueStorage.ApiUrl + "/pharmacytranshistory", "[GET]Get Pharmacy Transaction History By ", PrescriptionId.ToString() + "/" + Mode.ToString(), ex.Message);
                }
                else
                {
                    HttpResults = new ResponseMessage(Siloam.System.Web.StatusCode.UnprocessableEntity, StatusMessage.Fail, ex.Message, total);
                    Task<string> slackNotification = SlackMessageAsync(ValueStorage.SlackUrl, ValueStorage.ApiName, ValueStorage.ApiUrl + "/pharmacytranshistory", "[GET]Get Pharmacy Transaction History By ", PrescriptionId.ToString() + "/" + Mode.ToString(), ex.Message);
                }

            }

            return HttpResponse(HttpResults);
        }

        [HttpGet("pharmacypagehistory/{PatientId:long}")]
        [ProducesResponseType(typeof(ResponseData<List<PageTransactionHistory>>), 200)]
        public IActionResult GetPageTransactionHistory(long PatientId, string OrganizationCode, string PresRegNo, string TransRegNo, string DoctorName, string PayerName, Nullable<DateTime> DateFrom, Nullable<DateTime> DateTo)
        {

            int total = 0, cp = 0;

            string tempOrg = OrganizationCode == null ? "" : OrganizationCode;
            string tempPres = PresRegNo == null ? "" : PresRegNo;
            string tempTrans = TransRegNo == null ? "" : TransRegNo;
            string tempDoctor = DoctorName == null ? "" : DoctorName;
            string tempPayer = PayerName == null ? "" : PayerName;
            Nullable<DateTime> dtFrom = DateFrom == null ? DateTime.Parse("1800-01-01") : DateFrom;
            Nullable<DateTime> dtTo = DateTo == null ? DateTime.Parse("9999-12-31") : DateTo;

            try
            {

                var data = IUnitOfWorks.UnitOfWorkPharmacy().GetPageTransactionHistory(PatientId, tempOrg, tempPres, tempTrans, tempDoctor, tempPayer, dtFrom, dtTo);

                HttpResults = new ResponseData<List<PageTransactionHistory>>("Get Data Pharmacy Issue", Siloam.System.Web.StatusCode.OK, StatusMessage.Success, data, cp, total);

            }
            catch (Exception ex)
            {

                int exCode = ex.HResult;
                string paramslack = PatientId.ToString()+"/"+tempOrg.ToString() + "/" + tempPres.ToString() + "/" + tempTrans.ToString() + "/" + tempDoctor.ToString() + "/" + tempPayer.ToString() + "/" + dtFrom.ToString() + "/" + dtTo;
                if (exCode == -2147467259)
                {
                    HttpResults = new ResponseMessage(Siloam.System.Web.StatusCode.InternalServerErrorException, StatusMessage.Error, ex.Message, total);
                    Task<string> slackNotification = SlackMessageAsync(ValueStorage.SlackUrl, ValueStorage.ApiName, ValueStorage.ApiUrl + "/pharmacypagehistory", "[GET]Get Page Transaction History By PatientId, tempOrg, tempPres, tempTrans, tempDoctor, tempPayer, dtFrom, dtTo", paramslack.ToString(), ex.Message);
                }
                else
                {
                    HttpResults = new ResponseMessage(Siloam.System.Web.StatusCode.UnprocessableEntity, StatusMessage.Fail, ex.Message, total);
                    Task<string> slackNotification = SlackMessageAsync(ValueStorage.SlackUrl, ValueStorage.ApiName, ValueStorage.ApiUrl + "/pharmacypagehistory", "[GET]Get Page Transaction History By PatientId, tempOrg, tempPres, tempTrans, tempDoctor, tempPayer, dtFrom, dtTo", paramslack.ToString(), ex.Message);
                }

            }

            return HttpResponse(HttpResults);
        }

        [HttpGet("pharmacyprintprescription/{OrganizationId:long}/{PatientId:long}/{AdmissionId:long}/{EncounterId}/{StoreId:long}")]
        [ProducesResponseType(typeof(ResponseData<PharmacyPrintPrescription>), 200)]
        public IActionResult GetPrintPrescription(long OrganizationId, long PatientId, long AdmissionId, string EncounterId, long StoreId)
        {

            int total = 0, cp = 0;

            try
            {

                var data = IUnitOfWorks.UnitOfWorkPharmacy().GetPrintPrescription(OrganizationId, PatientId, AdmissionId, EncounterId, StoreId);

                HttpResults = new ResponseData<PharmacyPrintPrescription>("Get Data Pharmacy Print", Siloam.System.Web.StatusCode.OK, StatusMessage.Success, data, cp, total);

            }
            catch (Exception ex)
            {

                int exCode = ex.HResult;
                string paramslack = OrganizationId.ToString() + "/" + PatientId.ToString() + "/" + AdmissionId.ToString() + "/" + EncounterId.ToString() + "/" + StoreId;
                if (exCode == -2147467259)
                {
                    HttpResults = new ResponseMessage(Siloam.System.Web.StatusCode.InternalServerErrorException, StatusMessage.Error, ex.Message, total);
                    Task<string> slackNotification = SlackMessageAsync(ValueStorage.SlackUrl, ValueStorage.ApiName, ValueStorage.ApiUrl + "/pharmacyprintprescription", "[GET]Get Print Prescription By OrganizationId, PatientId, AdmissionId, EncounterId, StoreId", paramslack.ToString(), ex.Message);
                }
                else
                {
                    HttpResults = new ResponseMessage(Siloam.System.Web.StatusCode.UnprocessableEntity, StatusMessage.Fail, ex.Message, total);
                    Task<string> slackNotification = SlackMessageAsync(ValueStorage.SlackUrl, ValueStorage.ApiName, ValueStorage.ApiUrl + "/pharmacyprintprescription", "[GET]Get Print Prescription By OrganizationId, PatientId, AdmissionId, EncounterId, StoreId", paramslack.ToString(), ex.Message);
                }

            }

            return HttpResponse(HttpResults);
        }

        [HttpGet("additionalpharmacyprintprescription/{OrganizationId:long}/{PatientId:long}/{AdmissionId:long}/{EncounterId}/{StoreId:long}")]
        [ProducesResponseType(typeof(ResponseData<PharmacyPrintPrescription>), 200)]
        public IActionResult GetPrintAdditionalPrescription(long OrganizationId, long PatientId, long AdmissionId, string EncounterId, long StoreId)
        {

            int total = 0, cp = 0;

            try
            {

                var data = IUnitOfWorks.UnitOfWorkPharmacy().GetPrintAdditionalPrescription(OrganizationId, PatientId, AdmissionId, EncounterId, StoreId);

                HttpResults = new ResponseData<PharmacyPrintPrescription>("Get Data Additional Pharmacy Print", Siloam.System.Web.StatusCode.OK, StatusMessage.Success, data, cp, total);

            }
            catch (Exception ex)
            {

                int exCode = ex.HResult;
                string paramslack = OrganizationId.ToString() + "/" + PatientId.ToString() + "/" + AdmissionId.ToString() + "/" + EncounterId.ToString() + "/" + StoreId;
                if (exCode == -2147467259)
                {
                    HttpResults = new ResponseMessage(Siloam.System.Web.StatusCode.InternalServerErrorException, StatusMessage.Error, ex.Message, total);
                    Task<string> slackNotification = SlackMessageAsync(ValueStorage.SlackUrl, ValueStorage.ApiName, ValueStorage.ApiUrl + "/additionalpharmacyprintprescription", "[GET]Get Print Additional Prescription By OrganizationId, PatientId, AdmissionId, EncounterId, StoreId", paramslack.ToString(), ex.Message);
                }
                else
                {
                    HttpResults = new ResponseMessage(Siloam.System.Web.StatusCode.UnprocessableEntity, StatusMessage.Fail, ex.Message, total);
                    Task<string> slackNotification = SlackMessageAsync(ValueStorage.SlackUrl, ValueStorage.ApiName, ValueStorage.ApiUrl + "/additionalpharmacyprintprescription", "[GET]Get Print Additional Prescription By OrganizationId, PatientId, AdmissionId, EncounterId, StoreId", paramslack.ToString(), ex.Message);
                }

            }

            return HttpResponse(HttpResults);
        }

        [HttpPost("pharmacyunverify/{OrganizationId:long}/{PatientId:long}/{AdmissionId:long}/{EncounterId}/{Remarks}/{Updater:long}")]
        [ProducesResponseType(typeof(ResponseData<string>), 200)]
        public IActionResult UnverifyPharmacyData(long OrganizationId, long PatientId, long AdmissionId, string EncounterId, string Remarks, long Updater)
        {

            int page = 1;
            int total = 0;

            try
            {

                string data = IUnitOfWorks.UnitOfWorkPharmacy().UnverifyPharmacyData(OrganizationId, PatientId, AdmissionId, EncounterId, Remarks, Updater);

                messageHubContexts.Clients.All.InvokeAsync("Unverify Data", data);

                HttpResults = new ResponseData<string>("Data successfully unverified", Siloam.System.Web.StatusCode.OK, StatusMessage.Success, data, page, total);

            }
            catch (Exception ex)
            {

                int exCode = ex.HResult;

                if (exCode == -2147467259)
                {

                    HttpResults = new ResponseMessage(Siloam.System.Web.StatusCode.InternalServerErrorException, StatusMessage.Error, ex.Message, total);
                    Task<string> slackNotification = SlackMessageAsync(ValueStorage.SlackUrl, ValueStorage.ApiName, ValueStorage.ApiUrl + "/pharmacyunverify", "[POST]Unverify Prescription", OrganizationId.ToString() + "/" + PatientId.ToString() + "/" + AdmissionId.ToString() + "/" + EncounterId.ToString() + "/" + Remarks.ToString() + "/" + Updater.ToString(), ex.Message);

                }
                else
                {

                    HttpResults = new ResponseMessage(Siloam.System.Web.StatusCode.UnprocessableEntity, StatusMessage.Fail, ex.Message, total);
                    Task<string> slackNotification = SlackMessageAsync(ValueStorage.SlackUrl, ValueStorage.ApiName, ValueStorage.ApiUrl + "/pharmacyunverify", "[POST]Unverify Prescription", OrganizationId.ToString() + "/" + PatientId.ToString() + "/" + AdmissionId.ToString() + "/" + EncounterId.ToString() + "/" + Remarks.ToString() + "/" + Updater.ToString(), ex.Message);

                }

            }

            response:
            return HttpResponse(HttpResults);

        }

        [HttpPost("additionalpharmacyunverify/{OrganizationId:long}/{PatientId:long}/{AdmissionId:long}/{EncounterId}/{Remarks}/{Updater:long}")]
        [ProducesResponseType(typeof(ResponseData<string>), 200)]
        public IActionResult UnverifyAdditionalPharmacyData(long OrganizationId, long PatientId, long AdmissionId, string EncounterId, string Remarks, long Updater)
        {

            int page = 1;
            int total = 0;

            try
            {

                string data = IUnitOfWorks.UnitOfWorkPharmacy().UnverifyAdditionalPharmacyData(OrganizationId, PatientId, AdmissionId, EncounterId, Remarks, Updater);

                messageHubContexts.Clients.All.InvokeAsync("Unverify Data", data);

                HttpResults = new ResponseData<string>("Data successfully unverified", Siloam.System.Web.StatusCode.OK, StatusMessage.Success, data, page, total);

            }
            catch (Exception ex)
            {

                int exCode = ex.HResult;

                if (exCode == -2147467259)
                {

                    HttpResults = new ResponseMessage(Siloam.System.Web.StatusCode.InternalServerErrorException, StatusMessage.Error, ex.Message, total);
                    Task<string> slackNotification = SlackMessageAsync(ValueStorage.SlackUrl, ValueStorage.ApiName, ValueStorage.ApiUrl + "/additionalpharmacyunverify", "[POST]Unverify Additional Prescription", OrganizationId.ToString() + "/" + PatientId.ToString() + "/" + AdmissionId.ToString() + "/" + EncounterId.ToString() + "/" + Remarks.ToString() + "/" + Updater.ToString(), ex.Message);

                }
                else
                {

                    HttpResults = new ResponseMessage(Siloam.System.Web.StatusCode.UnprocessableEntity, StatusMessage.Fail, ex.Message, total);
                    Task<string> slackNotification = SlackMessageAsync(ValueStorage.SlackUrl, ValueStorage.ApiName, ValueStorage.ApiUrl + "/additionalpharmacyunverify", "[POST]Unverify Additional Prescription", OrganizationId.ToString() + "/" + PatientId.ToString() + "/" + AdmissionId.ToString() + "/" + EncounterId.ToString() + "/" + Remarks.ToString() + "/" + Updater.ToString(), ex.Message);

                }

            }

            response:
            return HttpResponse(HttpResults);

        }

        [HttpGet("pharmacymedicationhistory/{PatientId:long}/{AdmissionId:long}")]
        [ProducesResponseType(typeof(ResponseData<List<PharmacyMedHistory>>), 200)]
        public IActionResult GetPharmacyMedicationHistory(long PatientId, long AdmissionId)
        {

            int total = 0, cp = 0;

            try
            {

                var data = IUnitOfWorks.UnitOfWorkPharmacy().GetPharmacyMedicationHistory(PatientId, AdmissionId);

                HttpResults = new ResponseData<List<PharmacyMedHistory>>("Get Data Pharmacy Medication History", Siloam.System.Web.StatusCode.OK, StatusMessage.Success, data, cp, total);

            }
            catch (Exception ex)
            {

                int exCode = ex.HResult;
                string paramslack = PatientId+"/"+AdmissionId;
                if (exCode == -2147467259)
                {
                    HttpResults = new ResponseMessage(Siloam.System.Web.StatusCode.InternalServerErrorException, StatusMessage.Error, ex.Message, total);
                    Task<string> slackNotification = SlackMessageAsync(ValueStorage.SlackUrl, ValueStorage.ApiName, ValueStorage.ApiUrl + "/pharmacymedicationhistory", "[GET]Get Pharmacy Medication History By PatientId, AdmissionId)", paramslack.ToString(), ex.Message);
                }
                else
                {
                    HttpResults = new ResponseMessage(Siloam.System.Web.StatusCode.UnprocessableEntity, StatusMessage.Fail, ex.Message, total);
                    Task<string> slackNotification = SlackMessageAsync(ValueStorage.SlackUrl, ValueStorage.ApiName, ValueStorage.ApiUrl + "/pharmacymedicationhistory", "[GET]Get Pharmacy Medication History By PatientId, AdmissionId)", paramslack.ToString(), ex.Message);
                }

            }

            return HttpResponse(HttpResults);
        }

        [HttpGet("pharmacytransactionheader/{PatientId:long}/{OrganizationId:long}")]
        [ProducesResponseType(typeof(ResponseData<List<ViewTransactionHeader>>), 200)]
        public IActionResult GetDataTransactionByPatientOrganization(long PatientId, long OrganizationId)
        {

            int total = 0, cp = 0;

            try
            {

                var data = IUnitOfWorks.UnitOfWorkPharmacy().GetDataTransactionByPatientOrganization(PatientId, OrganizationId);

                HttpResults = new ResponseData<List<ViewTransactionHeader>>("Get Data Transaction Header", Siloam.System.Web.StatusCode.OK, StatusMessage.Success, data, cp, total);

            }
            catch (Exception ex)
            {

                int exCode = ex.HResult;
                string paramslack = PatientId.ToString() + "/" + OrganizationId.ToString();
                if (exCode == -2147467259)
                {
                    HttpResults = new ResponseMessage(Siloam.System.Web.StatusCode.InternalServerErrorException, StatusMessage.Error, ex.Message, total);
                    Task<string> slackNotification = SlackMessageAsync(ValueStorage.SlackUrl, ValueStorage.ApiName, ValueStorage.ApiUrl + "/pharmacytransactionheader", "[GET]Get Data Transaction By Patient Organization)", paramslack.ToString(), ex.Message);
                }
                else
                {
                    HttpResults = new ResponseMessage(Siloam.System.Web.StatusCode.UnprocessableEntity, StatusMessage.Fail, ex.Message, total);
                    Task<string> slackNotification = SlackMessageAsync(ValueStorage.SlackUrl, ValueStorage.ApiName, ValueStorage.ApiUrl + "/pharmacytransactionheader", "[GET]Get Data Transaction By Patient Organization)", paramslack.ToString(), ex.Message);
                }

            }

            return HttpResponse(HttpResults);
        }

        [HttpGet("pharmacyprintlabel/{HeaderId:long}")]
        [ProducesResponseType(typeof(ResponseData<List<PharmacyPrintLabel>>), 200)]
        public IActionResult GetDataPharmacyPrintLabel(long HeaderId)
        {

            int total = 0, cp = 0;

            try
            {

                var data = IUnitOfWorks.UnitOfWorkPharmacy().GetDataPharmacyPrintLabel(HeaderId);

                HttpResults = new ResponseData<List<PharmacyPrintLabel>>("Get Data Print Label", Siloam.System.Web.StatusCode.OK, StatusMessage.Success, data, cp, total);

            }
            catch (Exception ex)
            {

                int exCode = ex.HResult;

                if (exCode == -2147467259)
                {
                    HttpResults = new ResponseMessage(Siloam.System.Web.StatusCode.InternalServerErrorException, StatusMessage.Error, ex.Message, total);
                    Task<string> slackNotification = SlackMessageAsync(ValueStorage.SlackUrl, ValueStorage.ApiName, ValueStorage.ApiUrl + "/pharmacyprintlabel", "[GET]Get Data Pharmacy Print Label By Headerid)", HeaderId.ToString(), ex.Message);
                }
                else
                {
                    HttpResults = new ResponseMessage(Siloam.System.Web.StatusCode.UnprocessableEntity, StatusMessage.Fail, ex.Message, total);
                    Task<string> slackNotification = SlackMessageAsync(ValueStorage.SlackUrl, ValueStorage.ApiName, ValueStorage.ApiUrl + "/pharmacyprintlabel", "[GET]Get Data Pharmacy Print Label By Headerid)", HeaderId.ToString(), ex.Message);
                }

            }

            return HttpResponse(HttpResults);
        }

        [HttpPost("pharmacysync/{OrganizationId:long}/{AdmissionId:long}/{DoctorId:long}/{EncounterId}/{Updater:long}")]
        [ProducesResponseType(typeof(ResponseData<string>), 200)]
        public IActionResult GetPrescriptionSync(long OrganizationId, long AdmissionId, long DoctorId, Guid EncounterId, long Updater)
        {

            int page = 1;
            int total = 0;
            int countdrug = 0;
            int countconsumables = 0;
            string syncStatusPres = "success";
            string syncStatusCons = "success";
            string usedosetext = "";

            try
            {
                var processStart = IUnitOfWorks.UnitOfWorkPharmacy().InsertToTimeStampTable(EncounterId, "PHARMACY-CALL API SUBMIT RESEND");
                usedosetext = IUnitOfWorks.UnitOfWorkPharmacy().GetSettingDoseText(OrganizationId);
                List<SyncPrescriptionDB> datadrug = IUnitOfWorks.UnitOfWorkPharmacy().GetPrescriptionSync(OrganizationId, AdmissionId, DoctorId, EncounterId, Updater);
                if (datadrug != null)
                {
                    countdrug = datadrug.Count(x => x.is_consumables == false);
                    countconsumables = datadrug.Count(x => x.is_consumables == true);
                    if (countdrug > 0)
                    {
                        SyncPrescription pres = new SyncPrescription();
                        List<OrderItem> items = new List<OrderItem>();
                        items = (from a in datadrug
                                 where a.is_consumables == false
                                 select new OrderItem
                                 {
                                     AdministrationFrequencyId = a.AdministrationFrequencyId,
                                     AdministrationInstruction = a.AdministrationInstruction,
                                     AdministrationRouteId = a.AdministrationRouteId,
                                     DispensingInstruction = a.DispensingInstruction,
                                     Dose = a.DoseText != "" && usedosetext == "TRUE" ? "0.0001" : a.Dose,
                                     DoseText = a.DoseText,
                                     DoseUomId = a.DoseUomId,
                                     DrugId = a.DrugId,
                                     IsPrn = a.IsPrn,
                                     PatientInformation = a.PatientInformation,
                                     Quantity = a.Quantity,
                                     Repeat = a.Repeat,
                                     MedicalEncounterEntryInterfaceId = a.MedicalEncounterEntryInterfaceId
                                 }).ToList();

                        pres.MedicalOrderInterfaceId = Guid.Empty;
                        pres.MedicalOrderId = null;
                        pres.MedicalEncounterInterfaceId = EncounterId;
                        pres.SalesPriorityId = 1;
                        pres.Notes = (from a in datadrug where a.is_consumables == false select a.phar_notes).First().ToString();
                        pres.OrderItems = items;

                        string JsonString = JsonConvert.SerializeObject(pres);
                        var hopeStart = IUnitOfWorks.UnitOfWorkPharmacy().InsertToTimeStampTable(EncounterId, "PHARMACY-REQUEST API DRUG HOPE RESEND");
                        syncStatusPres = IUnitOfWorks.UnitOfWorkSync().SyncPrescription(OrganizationId, AdmissionId, JsonString);
                        var hopeFinish = IUnitOfWorks.UnitOfWorkPharmacy().InsertToTimeStampTable(EncounterId, "PHARMACY-RESPONSE API DRUG HOPE RESEND");
                    }

                    if (countconsumables > 0)
                    {
                        SyncConsumables cons = new SyncConsumables();
                        List<OrderItemConsumables> itemcons = new List<OrderItemConsumables>();
                        itemcons = (from a in datadrug
                                    where a.is_consumables == true
                                    select new OrderItemConsumables
                                    {
                                        UsageInstruction = a.AdministrationInstruction,
                                        ItemId = a.DrugId,
                                        PatientInformation = a.PatientInformation,
                                        Quantity = a.Quantity,
                                        DispensingInstruction = a.DispensingInstruction,
                                        MedicalEncounterEntryInterfaceId = a.MedicalEncounterEntryInterfaceId
                                    }).ToList();

                        cons.MedicalOrderInterfaceId = Guid.Empty;
                        cons.MedicalOrderId = null;
                        cons.MedicalEncounterInterfaceId = EncounterId;
                        cons.SalesPriorityId = 1;
                        cons.Notes = (from a in datadrug where a.is_consumables == true select a.phar_notes).First().ToString();
                        cons.OrderItems = itemcons;

                        string JsonStringCons = JsonConvert.SerializeObject(cons);
                        syncStatusCons = IUnitOfWorks.UnitOfWorkSync().SyncConsumables(OrganizationId, AdmissionId, JsonStringCons);
                    }
                }

                messageHubContexts.Clients.All.InvokeAsync("Re Sync Data");
                if (syncStatusPres.ToLower() == "success" && syncStatusCons.ToLower() == "success")
                {
                    string updateFlagHope = IUnitOfWorks.UnitOfWorkPharmacy().UpdateFlagHOPE(OrganizationId, AdmissionId, EncounterId);
                    HttpResults = new ResponseData<string>("Data successfully updated", Siloam.System.Web.StatusCode.OK, StatusMessage.Success, "SUCCESS|", page, total);
                }
                else if (syncStatusPres.ToLower() == "success" && syncStatusCons.ToLower() != "success")
                {
                    string updateFlagHope = IUnitOfWorks.UnitOfWorkPharmacy().UpdateFlagHOPE(OrganizationId, AdmissionId, EncounterId);
                    HttpResults = new ResponseData<string>("Data successfully updated", Siloam.System.Web.StatusCode.OK, StatusMessage.Success, "CONSUMABLEFAILED|" + syncStatusCons, page, total);
                }
                else if (syncStatusPres.ToLower() != "success" && syncStatusCons.ToLower() == "success")
                {
                    string updateFlagHope = IUnitOfWorks.UnitOfWorkPharmacy().UpdateFlagHOPE(OrganizationId, AdmissionId, EncounterId);
                    HttpResults = new ResponseData<string>("Data successfully updated", Siloam.System.Web.StatusCode.OK, StatusMessage.Success, "DRUGFAILED|" + syncStatusPres, page, total);
                }
                else
                {
                    if (syncStatusPres.ToLower() == syncStatusCons.ToLower())
                    {
                        HttpResults = new ResponseData<string>("Data successfully updated", Siloam.System.Web.StatusCode.OK, StatusMessage.Success, "HOPEFAILED|" + syncStatusPres, page, total);
                    }
                    else
                    {
                        HttpResults = new ResponseData<string>("Data successfully updated", Siloam.System.Web.StatusCode.OK, StatusMessage.Success, "HOPEFAILED|Drug Failed: " + syncStatusPres + ", Consumable Failed: " + syncStatusCons, page, total);
                    }
                }

            }
            catch (Exception ex)
            {

                int exCode = ex.HResult;

                if (exCode == -2147467259)
                {

                    HttpResults = new ResponseMessage(Siloam.System.Web.StatusCode.InternalServerErrorException, StatusMessage.Error, ex.Message, total);
                    Task<string> slackNotification = SlackMessageAsync(ValueStorage.SlackUrl, ValueStorage.ApiName, ValueStorage.ApiUrl + "/pharmacysync", "[POST]Re Sync", EncounterId.ToString(), ex.Message);

                }
                else
                {

                    HttpResults = new ResponseMessage(Siloam.System.Web.StatusCode.UnprocessableEntity, StatusMessage.Fail, ex.Message, total);
                    Task<string> slackNotification = SlackMessageAsync(ValueStorage.SlackUrl, ValueStorage.ApiName, ValueStorage.ApiUrl + "/pharmacysync", "[POST]Re Sync", EncounterId.ToString(), ex.Message);

                }

            }

            response:
            return HttpResponse(HttpResults);

        }

        [HttpGet("pharmacyprintoriginalprescription/{OrganizationId:long}/{PatientId:long}/{AdmissionId:long}/{EncounterId}/{StoreId:long}")]
        [ProducesResponseType(typeof(ResponseData<PharmacyPrintPrescription>), 200)]
        public IActionResult GetPrintOriginalPrescription(long OrganizationId, long PatientId, long AdmissionId, string EncounterId, long StoreId)
        {

            int total = 0, cp = 0;

            try
            {

                var data = IUnitOfWorks.UnitOfWorkPharmacy().GetPrintOriginalPrescription(OrganizationId, PatientId, AdmissionId, EncounterId, StoreId);

                HttpResults = new ResponseData<PharmacyPrintPrescription>("Get Data Pharmacy Print", Siloam.System.Web.StatusCode.OK, StatusMessage.Success, data, cp, total);

            }
            catch (Exception ex)
            {

                int exCode = ex.HResult;
                string paramslack = OrganizationId.ToString()+"/"+PatientId.ToString() + "/" + AdmissionId.ToString() + "/" + EncounterId.ToString() + "/" + StoreId;
                if (exCode == -2147467259)
                {
                    HttpResults = new ResponseMessage(Siloam.System.Web.StatusCode.InternalServerErrorException, StatusMessage.Error, ex.Message, total);
                    Task<string> slackNotification = SlackMessageAsync(ValueStorage.SlackUrl, ValueStorage.ApiName, ValueStorage.ApiUrl + "/pharmacyprintoriginalprescription", "[GET]Get Print Original Prescription By OrganizationId, PatientId, AdmissionId, EncounterId, StoreId)", paramslack.ToString(), ex.Message);
                }
                else
                {
                    HttpResults = new ResponseMessage(Siloam.System.Web.StatusCode.UnprocessableEntity, StatusMessage.Fail, ex.Message, total);
                    Task<string> slackNotification = SlackMessageAsync(ValueStorage.SlackUrl, ValueStorage.ApiName, ValueStorage.ApiUrl + "/pharmacyprintoriginalprescription", "[GET]Get Print Original Prescription By OrganizationId, PatientId, AdmissionId, EncounterId, StoreId)", paramslack.ToString(), ex.Message);
                }

            }

            return HttpResponse(HttpResults);
        }

        [HttpGet("additionalpharmacyprintoriginalprescription/{OrganizationId:long}/{PatientId:long}/{AdmissionId:long}/{EncounterId}/{StoreId:long}")]
        [ProducesResponseType(typeof(ResponseData<PharmacyPrintPrescription>), 200)]
        public IActionResult GetPrintOriginalAdditionalPrescription(long OrganizationId, long PatientId, long AdmissionId, string EncounterId, long StoreId)
        {

            int total = 0, cp = 0;

            try
            {

                var data = IUnitOfWorks.UnitOfWorkPharmacy().GetPrintOriginalAdditionalPrescription(OrganizationId, PatientId, AdmissionId, EncounterId, StoreId);

                HttpResults = new ResponseData<PharmacyPrintPrescription>("Get Data Additional Pharmacy Print", Siloam.System.Web.StatusCode.OK, StatusMessage.Success, data, cp, total);

            }
            catch (Exception ex)
            {

                int exCode = ex.HResult;
                string paramslack = OrganizationId.ToString() + "/" + PatientId.ToString() + "/" + AdmissionId.ToString() + "/" + EncounterId.ToString() + "/" + StoreId;
                if (exCode == -2147467259)
                {
                    HttpResults = new ResponseMessage(Siloam.System.Web.StatusCode.InternalServerErrorException, StatusMessage.Error, ex.Message, total);
                    Task<string> slackNotification = SlackMessageAsync(ValueStorage.SlackUrl, ValueStorage.ApiName, ValueStorage.ApiUrl + "/additionalpharmacyprintoriginalprescription", "[GET]Get Print Original Additional Prescription By OrganizationId, PatientId, AdmissionId, EncounterId, StoreId)", paramslack.ToString(), ex.Message);
                }
                else
                {
                    HttpResults = new ResponseMessage(Siloam.System.Web.StatusCode.UnprocessableEntity, StatusMessage.Fail, ex.Message, total);
                    Task<string> slackNotification = SlackMessageAsync(ValueStorage.SlackUrl, ValueStorage.ApiName, ValueStorage.ApiUrl + "/additionalpharmacyprintoriginalprescription", "[GET]Get Print Original Additional Prescription By OrganizationId, PatientId, AdmissionId, EncounterId, StoreId)", paramslack.ToString(), ex.Message);
                }

            }

            return HttpResponse(HttpResults);
        }

        [HttpPost("checkprice/{OrganizationId:long}/{PatientId:long}/{AdmissionId:long}/{EncounterId}")]
        [ProducesResponseType(typeof(ResponseData<List<CheckPrice>>), 200)]
        public IActionResult GetCheckPrice(long OrganizationId, long PatientId, long AdmissionId, Guid EncounterId, [FromBody]List<CheckPriceRequest> CheckPriceRequests)
        {

            int total = 0, cp = 0;
            string JsonString = JsonConvert.SerializeObject(CheckPriceRequests);
            if (CheckPriceRequests == null)
            {

                HttpResults = new ResponseMessage(Siloam.System.Web.StatusCode.BadRequest, StatusMessage.Fail, HttpResponseMessageKey.DataUnsuccessfullyCreated, total);

            }

            try
            {

                var data = IUnitOfWorks.UnitOfWorkPharmacy().GetCheckPrice(OrganizationId, PatientId, AdmissionId, EncounterId, CheckPriceRequests);

                HttpResults = new ResponseData<List<CheckPrice>>("Get Data Price", Siloam.System.Web.StatusCode.OK, StatusMessage.Success, data, cp, total);

            }
            catch (Exception ex)
            {

                int exCode = ex.HResult;
                string paramslack = OrganizationId.ToString()+"/"+PatientId.ToString() + "/" + AdmissionId.ToString() + "/" + EncounterId.ToString() + "/" + JsonString;
                if (exCode == -2147467259)
                {
                    HttpResults = new ResponseMessage(Siloam.System.Web.StatusCode.InternalServerErrorException, StatusMessage.Error, ex.Message, total);
                    Task<string> slackNotification = SlackMessageAsync(ValueStorage.SlackUrl, ValueStorage.ApiName, ValueStorage.ApiUrl + "/checkprice", "[GET]Get Check Price by OrganizationId, PatientId, AdmissionId, EncounterId, CheckPriceRequests)", paramslack.ToString(), ex.Message);
                }
                else
                {
                    HttpResults = new ResponseMessage(Siloam.System.Web.StatusCode.UnprocessableEntity, StatusMessage.Fail, ex.Message, total);
                    Task<string> slackNotification = SlackMessageAsync(ValueStorage.SlackUrl, ValueStorage.ApiName, ValueStorage.ApiUrl + "/checkprice", "[GET]Get Check Price by OrganizationId, PatientId, AdmissionId, EncounterId, CheckPriceRequests)", paramslack.ToString(), ex.Message);
                }

            }

            return HttpResponse(HttpResults);
        }

        [HttpPost("checkpriceitemissue/{OrganizationId:long}/{PatientId:long}/{AdmissionId:long}/{EncounterId}/{StoreId:long}/{isTele}")]
        [ProducesResponseType(typeof(ResponseData<List<CheckPriceItemIssue>>), 200)]
        public IActionResult GetCheckPriceItemIssue(long OrganizationId, long PatientId, long AdmissionId, Guid EncounterId,long StoreId,bool isTele, [FromBody] List<CheckPriceItemIssueRequest> CheckPriceItemIssueRequests)
        {

            int total = 0, cp = 0;
            string JsonString = JsonConvert.SerializeObject(CheckPriceItemIssueRequests);
            if (CheckPriceItemIssueRequests == null)
            {

                HttpResults = new ResponseMessage(Siloam.System.Web.StatusCode.BadRequest, StatusMessage.Fail, HttpResponseMessageKey.DataUnsuccessfullyCreated, total);

            }

            try
            {

                var data = IUnitOfWorks.UnitOfWorkPharmacy().GetCheckPriceItemIssue(OrganizationId, PatientId, AdmissionId, EncounterId, StoreId, isTele, CheckPriceItemIssueRequests);

                HttpResults = new ResponseData<List<CheckPriceItemIssue>>("Get Data Price", Siloam.System.Web.StatusCode.OK, StatusMessage.Success, data, cp, total);

            }
            catch (Exception ex)
            {

                int exCode = ex.HResult;
                string paramslack = OrganizationId.ToString() + "/" + PatientId.ToString() + "/" + AdmissionId.ToString() + "/" + EncounterId.ToString() + "/" + JsonString;
                if (exCode == -2147467259)
                {
                    HttpResults = new ResponseMessage(Siloam.System.Web.StatusCode.InternalServerErrorException, StatusMessage.Error, ex.Message, total);
                    Task<string> slackNotification = SlackMessageAsync(ValueStorage.SlackUrl, ValueStorage.ApiName, ValueStorage.ApiUrl + "/checkpriceitemissue", "[GET]Get Check Price by OrganizationId, PatientId, AdmissionId, EncounterId, CheckPriceRequests)", paramslack.ToString(), ex.Message);
                }
                else
                {
                    HttpResults = new ResponseMessage(Siloam.System.Web.StatusCode.UnprocessableEntity, StatusMessage.Fail, ex.Message, total);
                    Task<string> slackNotification = SlackMessageAsync(ValueStorage.SlackUrl, ValueStorage.ApiName, ValueStorage.ApiUrl + "/checkpriceitemissue", "[GET]Get Check Price by OrganizationId, PatientId, AdmissionId, EncounterId, CheckPriceRequests)", paramslack.ToString(), ex.Message);
                }

            }

            return HttpResponse(HttpResults);
        }

        [HttpPost("mysiloamcanceldrug")]
        [ProducesResponseType(typeof(ResponseData<string>), 200)]
        public IActionResult CancelItemMySiloam([FromBody]MySiloamCancelItem model)
        {

            int total = 0, cp = 0;
            string JsonString = JsonConvert.SerializeObject(model);
            if (model == null)
            {

                HttpResults = new ResponseMessage(Siloam.System.Web.StatusCode.BadRequest, StatusMessage.Fail, HttpResponseMessageKey.DataUnsuccessfullyCreated, total);

            }

            try
            {

                var data = IUnitOfWorks.UnitOfWorkPharmacy().CancelItemMySiloam(model);

                HttpResults = new ResponseData<string>("Cancel Drug MySiloam", Siloam.System.Web.StatusCode.OK, StatusMessage.Success, data, cp, total);

            }
            catch (Exception ex)
            {

                int exCode = ex.HResult;

                if (exCode == -2147467259)
                {
                    HttpResults = new ResponseMessage(Siloam.System.Web.StatusCode.InternalServerErrorException, StatusMessage.Error, ex.Message, total);
                    Task<string> slackNotification = SlackMessageAsync(ValueStorage.SlackUrl, ValueStorage.ApiName, ValueStorage.ApiUrl + "/mysiloamcanceldrug", "[POST]Cancel Item MySiloam by Siloam Cancel Item Model)", JsonString.ToString(), ex.Message);
                }
                else
                {
                    HttpResults = new ResponseMessage(Siloam.System.Web.StatusCode.UnprocessableEntity, StatusMessage.Fail, ex.Message, total);
                    Task<string> slackNotification = SlackMessageAsync(ValueStorage.SlackUrl, ValueStorage.ApiName, ValueStorage.ApiUrl + "/mysiloamcanceldrug", "[POST]Cancel Item MySiloam by Siloam Cancel Item Model)", JsonString.ToString(), ex.Message);
                }

            }

            return HttpResponse(HttpResults);
        }

        [HttpGet("pharmacyprintlabelprescription/{EncounterId}/{AdmissionId:long}/{OrganizationId:long}/{IsAdditional:int}")]
        [ProducesResponseType(typeof(ResponseData<List<PharmacyPrintLabelPrescription>>), 200)]
        public IActionResult GetDataPharmacyPrintLabelPresc(string EncounterId, Int64 AdmissionId, Int64 OrganizationId, int IsAdditional)
        {

            int total = 0, cp = 0;

            try
            {

                var data = IUnitOfWorks.UnitOfWorkPharmacy().GetDataPharmacyPrintLabelPharmacy(OrganizationId, AdmissionId, EncounterId, IsAdditional);

                HttpResults = new ResponseData<List<PharmacyPrintLabelPrescription>>("Get Data Pharmacy Print Label Prescription", Siloam.System.Web.StatusCode.OK, StatusMessage.Success, data, cp, total);

            }
            catch (Exception ex)
            {

                int exCode = ex.HResult;
                string paramslack = EncounterId.ToString() + "/" + AdmissionId.ToString() + "/" + OrganizationId.ToString() + "/" + IsAdditional.ToString();
                if (exCode == -2147467259)
                {
                    HttpResults = new ResponseMessage(Siloam.System.Web.StatusCode.InternalServerErrorException, StatusMessage.Error, ex.Message, total);
                    Task<string> slackNotification = SlackMessageAsync(ValueStorage.SlackUrl, ValueStorage.ApiName, ValueStorage.ApiUrl + "/pharmacyprintlabelprescription", "[GET]Get Data PharmacyPrint Label Prescription Transaction by EncounterId, AdmissionId, OrganizationId, IsAdditional", paramslack.ToString(), ex.Message);
                }
                else
                {
                    HttpResults = new ResponseMessage(Siloam.System.Web.StatusCode.UnprocessableEntity, StatusMessage.Fail, ex.Message, total);
                    Task<string> slackNotification = SlackMessageAsync(ValueStorage.SlackUrl, ValueStorage.ApiName, ValueStorage.ApiUrl + "/pharmacyprintlabelprescription", "[GET]Get Data PharmacyPrint Label Prescription Transaction by EncounterId, AdmissionId, OrganizationId, IsAdditional", paramslack.ToString(), ex.Message);
                }

            }

            return HttpResponse(HttpResults);
        }

        [HttpPost("setprintlabelprescrition/{AdmissionId:long}/{OrganizationId:long}/{EncounterId}/{IsAdditional:int}/{ProcessBy}")]
        [ProducesResponseType(typeof(ResponseData<string>), 200)]
        public async Task<IActionResult> SetPrintLabelPrescription(long AdmissionId, long OrganizationId, string EncounterId, int IsAdditional, string ProcessBy, [FromBody] RequestPrintLabelPrescription model)
        {
            string jsonModel = JsonConvert.SerializeObject(model);

            try
            {
                string data = IUnitOfWorks.UnitOfWorkPharmacy().SetDataPrintLabelPrescription(model, AdmissionId, OrganizationId, EncounterId, IsAdditional, ProcessBy);

                HttpResults = new ResponseData<string>("The data has been saved.", Siloam.System.Web.StatusCode.OK, StatusMessage.Success, data);

            }
            catch (Exception ex)
            {

                int exCode = ex.HResult;

                if (exCode == -2147467259)
                {

                    HttpResults = new ResponseMessage(Siloam.System.Web.StatusCode.InternalServerErrorException, StatusMessage.Error, ex.Message, 1);
                    Task<string> slackNotification = SlackMessageAsync(ValueStorage.SlackUrl, ValueStorage.ApiName, ValueStorage.ApiUrl + "/PHARMACY", "[POST]setprintlabelprescrition", AdmissionId + "/" + OrganizationId + "/" + EncounterId + "/" + IsAdditional + "/" + ProcessBy + "body : " + jsonModel.ToString(), ex.Message);

                }
                else
                {

                    HttpResults = new ResponseMessage(Siloam.System.Web.StatusCode.UnprocessableEntity, StatusMessage.Fail, ex.Message, 1);
                    Task<string> slackNotification = SlackMessageAsync(ValueStorage.SlackUrl, ValueStorage.ApiName, ValueStorage.ApiUrl + "/PHARMACY", "[POST]setprintlabelprescrition", AdmissionId + "/" + OrganizationId + "/" + EncounterId + "/" + IsAdditional + "/" + ProcessBy + "body : " + jsonModel.ToString() , ex.Message);

                }
            }

            return HttpResponse(HttpResults);

        }


        [HttpGet("pharmacyeditreason")]
        [ProducesResponseType(typeof(ResponseData<List<ReasonPharmacyModel>>), 200)]
        public IActionResult GetReasonPharmacy()
        {

            int total = 0, cp = 0;

            try
            {

                var data = IUnitOfWorks.UnitOfWorkPharmacy().GetReasonPharmacy();

                HttpResults = new ResponseData<List<ReasonPharmacyModel>>("Get Data Pharmacy Edit Reason Master", Siloam.System.Web.StatusCode.OK, StatusMessage.Success, data, cp, total);

            }
            catch (Exception ex)
            {

                int exCode = ex.HResult;
                string paramslack = "";
                if (exCode == -2147467259)
                {
                    HttpResults = new ResponseMessage(Siloam.System.Web.StatusCode.InternalServerErrorException, StatusMessage.Error, ex.Message, total);
                    //Task<string> slackNotification = SlackMessageAsync(ValueStorage.SlackUrl, ValueStorage.ApiName, ValueStorage.ApiUrl + "/pharmacyprintlabelprescription", "[GET]Get Data PharmacyPrint Label Prescription Transaction by EncounterId, AdmissionId, OrganizationId, IsAdditional", paramslack.ToString(), ex.Message);
                }
                else
                {
                    HttpResults = new ResponseMessage(Siloam.System.Web.StatusCode.UnprocessableEntity, StatusMessage.Fail, ex.Message, total);
                    //Task<string> slackNotification = SlackMessageAsync(ValueStorage.SlackUrl, ValueStorage.ApiName, ValueStorage.ApiUrl + "/pharmacyprintlabelprescription", "[GET]Get Data PharmacyPrint Label Prescription Transaction by EncounterId, AdmissionId, OrganizationId, IsAdditional", paramslack.ToString(), ex.Message);
                }

            }

            return HttpResponse(HttpResults);
        }

        [HttpGet("getCountDrugCMS/{organizationId:long}")]
        [ProducesResponseType(typeof(ResponseData<dynamic>), 200)]
        public IActionResult GetCountDrugCMS(long organizationId)
        {
            int total = 0, cp = 0;

            try
            {
                var result = IUnitOfWorks.UnitOfWorkPharmacy().GetCountDrugCMS(organizationId);
                var resultSplit = result.Split(',');

                string stringResult = "{ \"count_non_additional\": " + resultSplit[0] + ", \"count_additional\": " + resultSplit[1] + " }";
                JObject jsonResult = JObject.Parse(stringResult);
                var data = jsonResult;

                HttpResults = new ResponseData<dynamic>("Get Data Count Drug CMS", Siloam.System.Web.StatusCode.OK, StatusMessage.Success, data, cp, total);
            }
            catch (Exception ex)
            {
                int exCode = ex.HResult;
                if (exCode == -2147467259)
                {
                    HttpResults = new ResponseMessage(Siloam.System.Web.StatusCode.InternalServerErrorException, StatusMessage.Error, ex.Message, total);
                    Task<string> slackNotification = SlackMessageAsync(ValueStorage.SlackUrl, ValueStorage.ApiName, ValueStorage.ApiUrl + "/getCountDrugCMS", "[GET]Get Count Drug CMS by Organizationid", organizationId.ToString(), ex.Message);
                }
                else
                {
                    HttpResults = new ResponseMessage(Siloam.System.Web.StatusCode.UnprocessableEntity, StatusMessage.Fail, ex.Message, total);
                    Task<string> slackNotification = SlackMessageAsync(ValueStorage.SlackUrl, ValueStorage.ApiName, ValueStorage.ApiUrl + "/getCountDrugCMS", "[GET]Get Count Drug CMS by Organizationid", organizationId.ToString(), ex.Message);
                }
            }

            return HttpResponse(HttpResults);
        }

        [HttpGet("pharmacyprintpicklist/{OrganizationId:long}/{PatientId:long}/{AdmissionId:long}/{EncounterId}/{StoreId:long}/{IsAdditional:bool}/{IssueCode}/{SpecificCode}")]
        [ProducesResponseType(typeof(ResponseData<PharmacyPickingList>), 200)]
        public IActionResult GetPickingList(long OrganizationId, long PatientId, long AdmissionId, string EncounterId, long StoreId, bool IsAdditional, string IssueCode, string SpecificCode)
        {

            int total = 0, cp = 0;

            try
            {

                var data = IUnitOfWorks.UnitOfWorkPharmacy().GetPickingList(OrganizationId, PatientId, AdmissionId, EncounterId, StoreId, IsAdditional, IssueCode, SpecificCode);

                HttpResults = new ResponseData<PharmacyPickingList>("Get Data Pharmacy Print Pick List", Siloam.System.Web.StatusCode.OK, StatusMessage.Success, data, cp, total);

            }
            catch (Exception ex)
            {

                int exCode = ex.HResult;
                string paramslack = OrganizationId.ToString() + "/" + PatientId.ToString() + "/" + AdmissionId.ToString() + "/" + EncounterId.ToString() + "/" + StoreId + "/" + IsAdditional.ToString() + "/" + IssueCode;
                if (exCode == -2147467259)
                {
                    HttpResults = new ResponseMessage(Siloam.System.Web.StatusCode.InternalServerErrorException, StatusMessage.Error, ex.Message, total);
                    Task<string> slackNotification = SlackMessageAsync(ValueStorage.SlackUrl, ValueStorage.ApiName, ValueStorage.ApiUrl + "/pharmacyprintpicklist", "[GET]Get Print Pick List By OrganizationId, PatientId, AdmissionId, EncounterId, StoreId, IsAdditional, IssueCode", paramslack.ToString(), ex.Message);
                }
                else
                {
                    HttpResults = new ResponseMessage(Siloam.System.Web.StatusCode.UnprocessableEntity, StatusMessage.Fail, ex.Message, total);
                    Task<string> slackNotification = SlackMessageAsync(ValueStorage.SlackUrl, ValueStorage.ApiName, ValueStorage.ApiUrl + "/pharmacyprintpicklist", "[GET]Get Print Pick List By OrganizationId, PatientId, AdmissionId, EncounterId, StoreId, IsAdditional, IssueCode", paramslack.ToString(), ex.Message);
                }

            }

            return HttpResponse(HttpResults);
        }

        [HttpPost("setcompleteissue/{AdmissionId:long}/{OrganizationId:long}/{EncounterId}/{IssueCode}/{NamaPenerima}/{NoHp}/{Relasi}/{ProcessBy}")]
        [ProducesResponseType(typeof(ResponseData<string>), 200)]
        public async Task<IActionResult> SetCompleteIssue(long AdmissionId, long OrganizationId, string EncounterId, string IssueCode, string NamaPenerima, string NoHp, string Relasi, string ProcessBy, [FromBody] RequestCompleteIssue model)
        {
            string jsonModel = JsonConvert.SerializeObject(model);

            try
            {
                string data = IUnitOfWorks.UnitOfWorkPharmacy().SetCompleteIssue(model, AdmissionId, OrganizationId, EncounterId, IssueCode, NamaPenerima, NoHp, Relasi, ProcessBy);

                HttpResults = new ResponseData<string>("The data has been saved.", Siloam.System.Web.StatusCode.OK, StatusMessage.Success, data);

            }
            catch (Exception ex)
            {

                int exCode = ex.HResult;

                if (exCode == -2147467259)
                {

                    HttpResults = new ResponseMessage(Siloam.System.Web.StatusCode.InternalServerErrorException, StatusMessage.Error, ex.Message, 1);
                    Task<string> slackNotification = SlackMessageAsync(ValueStorage.SlackUrl, ValueStorage.ApiName, ValueStorage.ApiUrl + "/PHARMACY", "[POST]setcompleteissue", AdmissionId + "/" + OrganizationId + "/" + EncounterId + "/" + IssueCode + "/" + ProcessBy + "body : " + jsonModel.ToString(), ex.Message);

                }
                else
                {

                    HttpResults = new ResponseMessage(Siloam.System.Web.StatusCode.UnprocessableEntity, StatusMessage.Fail, ex.Message, 1);
                    Task<string> slackNotification = SlackMessageAsync(ValueStorage.SlackUrl, ValueStorage.ApiName, ValueStorage.ApiUrl + "/PHARMACY", "[POST]setcompleteissue", AdmissionId + "/" + OrganizationId + "/" + EncounterId + "/" + IssueCode + "/" + ProcessBy + "body : " + jsonModel.ToString(), ex.Message);

                }
            }

            return HttpResponse(HttpResults);

        }

        [HttpGet("pharmacyscan/{IssueCode}/{OrganizationId:long}")]
        [ProducesResponseType(typeof(ResponseData<List<PharmacyPrintLabelPrescription>>), 200)]
        public IActionResult GetDataPharmacyScanned(string IssueCode, Int64 OrganizationId)
        {

            int total = 0, cp = 0;

            try
            {

                var data = IUnitOfWorks.UnitOfWorkPharmacy().GetDataPharmacyScannedList(OrganizationId, IssueCode);

                HttpResults = new ResponseData<List<PharmacyPrintLabelPrescription>>("Get Data Pharmacy Scanned List", Siloam.System.Web.StatusCode.OK, StatusMessage.Success, data, cp, total);

            }
            catch (Exception ex)
            {

                int exCode = ex.HResult;
                string paramslack = IssueCode.ToString() +  "/" + OrganizationId.ToString();
                if (exCode == -2147467259)
                {
                    HttpResults = new ResponseMessage(Siloam.System.Web.StatusCode.InternalServerErrorException, StatusMessage.Error, ex.Message, total);
                    Task<string> slackNotification = SlackMessageAsync(ValueStorage.SlackUrl, ValueStorage.ApiName, ValueStorage.ApiUrl + "/pharmacyprintlabelprescription", "[GET]Get Data PharmacyPrint Label Prescription Transaction by EncounterId, AdmissionId, OrganizationId, IsAdditional", paramslack.ToString(), ex.Message);
                }
                else
                {
                    HttpResults = new ResponseMessage(Siloam.System.Web.StatusCode.UnprocessableEntity, StatusMessage.Fail, ex.Message, total);
                    Task<string> slackNotification = SlackMessageAsync(ValueStorage.SlackUrl, ValueStorage.ApiName, ValueStorage.ApiUrl + "/pharmacyprintlabelprescription", "[GET]Get Data PharmacyPrint Label Prescription Transaction by EncounterId, AdmissionId, OrganizationId, IsAdditional", paramslack.ToString(), ex.Message);
                }

            }

            return HttpResponse(HttpResults);
        }


        [HttpPost("resetitemissue/{OrganizationId:long}/{AdmissionId:long}/{EncounterId}/{IsAdditional:int}/{Updater:long}")]
        [ProducesResponseType(typeof(ResponseData<string>), 200)]
        public IActionResult ResetItemIssue(long OrganizationId, long AdmissionId, string EncounterId, int IsAdditional, long Updater, [FromBody] List<long> aritemids)
        {

            int page = 1;
            int total = 0;

            try
            {

                string data = IUnitOfWorks.UnitOfWorkPharmacy().ResetItemIssue(OrganizationId, AdmissionId, EncounterId, IsAdditional, Updater, aritemids);

                messageHubContexts.Clients.All.InvokeAsync("Reset Item Issue Data", data);

                HttpResults = new ResponseData<string>("Data successfully reset", Siloam.System.Web.StatusCode.OK, StatusMessage.Success, data, page, total);

            }
            catch (Exception ex)
            {

                int exCode = ex.HResult;

                if (exCode == -2147467259)
                {

                    HttpResults = new ResponseMessage(Siloam.System.Web.StatusCode.InternalServerErrorException, StatusMessage.Error, ex.Message, total);
                    Task<string> slackNotification = SlackMessageAsync(ValueStorage.SlackUrl, ValueStorage.ApiName, ValueStorage.ApiUrl + "/resetitemissue", "[POST]Reset Item Issue", OrganizationId.ToString() + "/" + AdmissionId.ToString() + "/" + EncounterId.ToString() + "/" + IsAdditional.ToString() + "/" + Updater.ToString(), ex.Message);

                }
                else
                {

                    HttpResults = new ResponseMessage(Siloam.System.Web.StatusCode.UnprocessableEntity, StatusMessage.Fail, ex.Message, total);
                    Task<string> slackNotification = SlackMessageAsync(ValueStorage.SlackUrl, ValueStorage.ApiName, ValueStorage.ApiUrl + "/resetitemissue", "[POST]Reset Item Issue", OrganizationId.ToString() + "/" + AdmissionId.ToString() + "/" + EncounterId.ToString() + "/" + IsAdditional.ToString() + "/" + Updater.ToString(), ex.Message);

                }

            }

        response:
            return HttpResponse(HttpResults);

        }

        [HttpGet("prescriptionhistoryedited/{OrganizationId:long}/{EncounterId}/{AdmissionId:long}")]
        [ProducesResponseType(typeof(ResponseData<dynamic>), 200)]
        public IActionResult PrescriptionHistory(long OrganizationId, string EncounterId, long AdmissionId)
        {
            int total = 0, cp = 0;

            try
            {

                var data = IUnitOfWorks.UnitOfWorkPharmacy().GetPrescriptionHistory(OrganizationId, EncounterId, AdmissionId);

                HttpResults = new ResponseData<PrescriptionHistory>("Get PrescriptionHistory Data Pharmacy by Ticket Id " + EncounterId, Siloam.System.Web.StatusCode.OK, StatusMessage.Success, data, cp, total);

            }
            catch (Exception ex)
            {

                int exCode = ex.HResult;

                if (exCode == -2147467259)
                {
                    HttpResults = new ResponseMessage(Siloam.System.Web.StatusCode.InternalServerErrorException, StatusMessage.Error, ex.Message, total);
                }
                else
                {
                    HttpResults = new ResponseMessage(Siloam.System.Web.StatusCode.UnprocessableEntity, StatusMessage.Fail, ex.Message, total);
                }

            }

            return HttpResponse(HttpResults);
        }


    }
}