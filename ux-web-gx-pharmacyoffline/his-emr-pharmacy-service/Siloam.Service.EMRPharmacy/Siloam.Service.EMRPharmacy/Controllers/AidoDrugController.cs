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

namespace Siloam.Service.EMRPharmacy.Controllers
{
    public class AidoDrugController : BaseController
    {
        private readonly IHubContext<MessageHub> messageHubContexts;


        public AidoDrugController(IUnitOfWork unitOfWork, IHubContext<MessageHub> messageHubContext) : base(unitOfWork)
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

        [HttpPost("insertaidoticket/{OrganizationId:long}/{PatientId:long}/{AdmissionId:long}/{EncounterId}/{JsonRequest}/{JsonResponse}/{SiloamTrxId}")]
        [ProducesResponseType(typeof(ResponseData<string>), 200)]
        public IActionResult InsertData(long OrganizationId, long PatientId, long AdmissionId, Guid EncounterId, string JsonRequest, string JsonResponse, Guid SiloamTrxId)
        {

            int page = 1;
            int total = 0;

            try
            {

                string data = IUnitOfWorks.UnitOfWorkAidoDrug().InsertData(OrganizationId, PatientId, AdmissionId, EncounterId, JsonRequest, JsonResponse, SiloamTrxId, "18");

                messageHubContexts.Clients.All.InvokeAsync("Insert AIDO Ticketing", data);

                HttpResults = new ResponseData<string>("Data successfully inserted", Siloam.System.Web.StatusCode.OK, StatusMessage.Success, data, page, total);
            }
            catch (Exception ex)
            {

                int exCode = ex.HResult;

                if (exCode == -2147467259)
                {

                    HttpResults = new ResponseMessage(Siloam.System.Web.StatusCode.InternalServerErrorException, StatusMessage.Error, ex.Message, total);
                    Task<string> slackNotification = SlackMessageAsync(ValueStorage.SlackUrl, ValueStorage.ApiName, ValueStorage.ApiUrl + "/insertaidoticket", "[POST]Insert Aido Ticket", EncounterId.ToString(), ex.Message);

                }
                else
                {

                    HttpResults = new ResponseMessage(Siloam.System.Web.StatusCode.UnprocessableEntity, StatusMessage.Fail, ex.Message, total);
                    Task<string> slackNotification = SlackMessageAsync(ValueStorage.SlackUrl, ValueStorage.ApiName, ValueStorage.ApiUrl + "/insertaidoticket", "[POST]Insert Aido Ticket", EncounterId.ToString(), ex.Message);

                }

            }

            return HttpResponse(HttpResults);

        }

        [HttpPut("updateaidoticket/{SiloamTrxId}")]
        [ProducesResponseType(typeof(ResponseData<string>), 200)]
        public IActionResult UpdateDate(Guid SiloamTrxId)
        {

            int page = 1;
            int total = 0;

            try
            {
                string data = IUnitOfWorks.UnitOfWorkAidoDrug().UpdateData(SiloamTrxId);
                if (data == "SUCCESS")
                {
                    HttpResults = new ResponseData<string>("Data successfully updated", Siloam.System.Web.StatusCode.OK, StatusMessage.Success, data, page, total);
                }
                else if (data == "DATA NOT FOUND" || data == "Sequence contains no elements")
                {
                    HttpResults = new ResponseMessage(Siloam.System.Web.StatusCode.NotFound, StatusMessage.Fail, "DATA NOT FOUND", total);
                }
                else
                {
                    HttpResults = new ResponseMessage(Siloam.System.Web.StatusCode.InternalServerErrorException, StatusMessage.Error, "ERROR", total);
                }
            }
            catch (Exception ex)
            {

                int exCode = ex.HResult;

                if (exCode == -2147467259)
                {

                    HttpResults = new ResponseMessage(Siloam.System.Web.StatusCode.InternalServerErrorException, StatusMessage.Error, ex.Message, total);
                    Task<string> slackNotification = SlackMessageAsync(ValueStorage.SlackUrl, ValueStorage.ApiName, ValueStorage.ApiUrl + "/updateaidoticket", "[PUT]Insert Aido Ticket", SiloamTrxId.ToString(), ex.Message);

                }
                else
                {

                    HttpResults = new ResponseMessage(Siloam.System.Web.StatusCode.UnprocessableEntity, StatusMessage.Fail, ex.Message, total);
                    Task<string> slackNotification = SlackMessageAsync(ValueStorage.SlackUrl, ValueStorage.ApiName, ValueStorage.ApiUrl + "/updateaidoticket", "[PUT]Insert Aido Ticket", SiloamTrxId.ToString(), ex.Message);

                }

            }

            return HttpResponse(HttpResults);

        }

        //[HttpPut("updateaidoticket")]
        //public IActionResult UpdateData(string siloamTrxId, long OrganizationId, long PatientId, long AdmissionId, string EncounterId, string ShipmentName)
        //{
        //    int page = 1;
        //    int total = 0;

        //    try
        //    {
        //        #region Validation
        //        if (string.IsNullOrEmpty(ShipmentName))
        //        {
        //            HttpResults = new ResponseMessage(Siloam.System.Web.StatusCode.InternalServerErrorException, StatusMessage.Error, "Invalid Shipment Name", total);
        //            Task<string> slackNotification = SlackMessageAsync(ValueStorage.SlackUrl, ValueStorage.ApiName, ValueStorage.ApiUrl + "/aidoresync", "[PUT]Insert Aido Ticket", siloamTrxId, "Invalid Shipment Name");

        //            return HttpResponse(HttpResults);
        //        }

        //        Guid _siloamTrxId = Guid.Empty;
        //        if (!string.IsNullOrEmpty(siloamTrxId))
        //        {
        //            if (!Guid.TryParse(siloamTrxId, out _siloamTrxId))
        //            {
        //                HttpResults = new ResponseMessage(Siloam.System.Web.StatusCode.InternalServerErrorException, StatusMessage.Error, "Invalid SiloamTrxId", total);
        //                Task<string> slackNotification = SlackMessageAsync(ValueStorage.SlackUrl, ValueStorage.ApiName, ValueStorage.ApiUrl + "/aidoresync", "[PUT]Insert Aido Ticket", siloamTrxId, "Invalid SiloamTrxId");

        //                return HttpResponse(HttpResults);
        //            }
        //        }

        //        Guid _EncounterID = Guid.Empty;
        //        if (!string.IsNullOrEmpty(EncounterId))
        //        {
        //            if (!Guid.TryParse(EncounterId, out _EncounterID))
        //            {
        //                HttpResults = new ResponseMessage(Siloam.System.Web.StatusCode.InternalServerErrorException, StatusMessage.Error, "Invalid EncounterID", total);
        //                Task<string> slackNotification = SlackMessageAsync(ValueStorage.SlackUrl, ValueStorage.ApiName, ValueStorage.ApiUrl + "/aidoresync", "[PUT]Insert Aido Ticket", siloamTrxId, "Invalid EncounterID");

        //                return HttpResponse(HttpResults);
        //            }
        //        }
        //        #endregion

        //        string msg = string.Empty;

        //        bool isUpdate = IUnitOfWorks.UnitOfWorkAidoDrug().UpdateData(_siloamTrxId, OrganizationId, PatientId, AdmissionId, _EncounterID, ShipmentName, out msg);
        //        if (!isUpdate)
        //        {
        //            HttpResults = new ResponseMessage(Siloam.System.Web.StatusCode.InternalServerErrorException, StatusMessage.Error, msg, total);                    

        //            return HttpResponse(HttpResults);
        //        }

        //        HttpResults = new ResponseData<string>("Data successfully updated", Siloam.System.Web.StatusCode.OK, StatusMessage.Success, msg, page, total);

        //        return HttpResponse(HttpResults);
        //    }
        //    catch (Exception err)
        //    {
        //        int exCode = err.HResult;

        //        if (exCode == -2147467259)
        //        {

        //            HttpResults = new ResponseMessage(Siloam.System.Web.StatusCode.InternalServerErrorException, StatusMessage.Error, err.Message, total);
        //            Task<string> slackNotification = SlackMessageAsync(ValueStorage.SlackUrl, ValueStorage.ApiName, ValueStorage.ApiUrl + "/updateaidoticket", "[PUT]Insert Aido Ticket", siloamTrxId, err.Message);

        //        }
        //        else
        //        {

        //            HttpResults = new ResponseMessage(Siloam.System.Web.StatusCode.UnprocessableEntity, StatusMessage.Fail, err.Message, total);
        //            Task<string> slackNotification = SlackMessageAsync(ValueStorage.SlackUrl, ValueStorage.ApiName, ValueStorage.ApiUrl + "/updateaidoticket", "[PUT]Insert Aido Ticket", siloamTrxId, err.Message);

        //        }
        //        return HttpResponse(HttpResults);
        //    }
        //}

        [HttpPost("aidoresync/{OrganizationId:long}/{PatientId:long}/{AdmissionId:long}/{DoctorId:long}/{EncounterId}/{Updater:long}/{UserName}/{ChannelId}")]
        [ProducesResponseType(typeof(ResponseData<string>), 200)]
        public IActionResult GetAidoSync(long OrganizationId, long PatientId, long AdmissionId, long DoctorId, Guid EncounterId, long Updater, string UserName, string ChannelId)
        {

            int page = 1;
            int total = 0;
            string syncStatusAIDO = "";
            string syncMessageAIDO = "";
            string result = "";
            string JsonResponse = "";
            string ErrMsg = "";
            int PayerCoverage = 0;
            bool IsSelfCollection = false;

            try
            {
                long pharmacypayerid = IUnitOfWorks.UnitOfWorkPharmacy().GetPayerIdRecord(OrganizationId,PatientId,AdmissionId,EncounterId);
                string payertemp = "", payertempaido = "";
                List<long> mysiloampayerid = new List<long>();
                List<long> aidopayerid = new List<long>();
                payertemp = IUnitOfWorks.UnitOfWorkAidoDrug().GetSettingMysiloamPayerId(OrganizationId);
                mysiloampayerid = payertemp.Split(',').Select(Int64.Parse).ToList();
                payertempaido = IUnitOfWorks.UnitOfWorkAidoDrug().GetSettingAidoPayerId(OrganizationId);
                aidopayerid = payertempaido.Split(',').Select(Int64.Parse).ToList();

                var processStart = IUnitOfWorks.UnitOfWorkPharmacy().InsertToTimeStampTable(EncounterId, "PHARMACY-CALL API SUBMIT RESEND");
                if (ChannelId == "18" || aidopayerid.Contains(pharmacypayerid))
                {
                    int CountAIDO = IUnitOfWorks.UnitOfWorkAidoDrug().GetCountAIDOOrder(OrganizationId, PatientId, AdmissionId, EncounterId);
                    if (CountAIDO > 0)
                    {
                        HttpResults = new ResponseData<string>("Data unsuccessfully updated", Siloam.System.Web.StatusCode.OK, StatusMessage.Success, "AIDOFAILED|ALREADY SENT TO AIDO", page, total);
                    }
                    else
                    {
                        List<PharmacyPrescription> prescription = new List<PharmacyPrescription>();
                        List<ItemPrice> itemPrices = new List<ItemPrice>();
                        itemPrices = IUnitOfWorks.UnitOfWorkAidoSync().GetItemSync(OrganizationId, PatientId, AdmissionId, EncounterId, prescription, "0", false, false);

                        //HIT API AIDO -> GET STATUS
                        AidoRequestModel aido = new AidoRequestModel();
                        Guid SiloamTrxId = new Guid();
                        SiloamTrxId = IUnitOfWorks.UnitOfWorkAidoDrug().GetSiloamTrxId(OrganizationId, PatientId, AdmissionId, EncounterId);
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
                        string token = IUnitOfWorks.UnitOfWorkAidoSync().GenerateJSONWebToken(OrganizationId, PatientId, AdmissionId, DoctorId);
                        result = IUnitOfWorks.UnitOfWorkAidoSync().AidoSyncPrescription(JsonStringAido, OrganizationId, PatientId, AdmissionId, DoctorId, token);
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
                            string insertaidoticket = IUnitOfWorks.UnitOfWorkAidoDrug().InsertData(OrganizationId, PatientId, AdmissionId, EncounterId, JsonStringAido, JsonResponse, SiloamTrxId, ChannelId);
                            HttpResults = new ResponseData<string>("Data successfully updated " + ValueStorage.AidoUrlSync + " content: " + JsonStringAido, Siloam.System.Web.StatusCode.OK, StatusMessage.Success, "SUCCESS|", page, total);
                        }
                        else
                        {
                            string insertaidofailed = IUnitOfWorks.UnitOfWorkAidoSync().InsertDataLogFailed(OrganizationId, PatientId, AdmissionId, EncounterId, token, JsonStringAido, JsonResponse, ErrMsg, ChannelId);
                            HttpResults = new ResponseData<string>("Data successfully updated " + ValueStorage.AidoUrlSync + " content: " + JsonStringAido, Siloam.System.Web.StatusCode.OK, StatusMessage.Success, "AIDOFAILED|" + syncMessageAIDO, page, total);
                        }
                    }
                }
                else if (ChannelId == "5" || ChannelId == "9" || mysiloampayerid.Contains(pharmacypayerid))
                {
                    int CountMySiloam = IUnitOfWorks.UnitOfWorkAidoDrug().GetCountAIDOOrder(OrganizationId, PatientId, AdmissionId, EncounterId);
                    if (CountMySiloam > 0)
                    {
                        HttpResults = new ResponseData<string>("Data unsuccessfully updated", Siloam.System.Web.StatusCode.OK, StatusMessage.Success, "MYSILOAMFAILED|ALREADY SENT TO MYSILOAM", page, total);
                    }
                    else
                    {
                        List<PharmacyPrescription> prescription = new List<PharmacyPrescription>();
                        List<ItemPrice> itemPrices = new List<ItemPrice>();
                        PayerCoverage = IUnitOfWorks.UnitOfWorkPharmacy().GetPayerCoverage(OrganizationId, PatientId, AdmissionId, EncounterId);
                        IsSelfCollection = IUnitOfWorks.UnitOfWorkPharmacy().GetIsSelfCollection(OrganizationId, PatientId, AdmissionId, EncounterId);
                        itemPrices = IUnitOfWorks.UnitOfWorkAidoSync().GetItemSyncTeleconsultation(OrganizationId,PatientId,AdmissionId,EncounterId,prescription,"0",false,false,PayerCoverage.ToString(),0);

                        MySiloamRequestDrug silo = new MySiloamRequestDrug();
                        Guid TrxId = new Guid();
                        TrxId = IUnitOfWorks.UnitOfWorkAidoDrug().GetSiloamTrxId(OrganizationId, PatientId, AdmissionId, EncounterId);
                        if (TrxId == Guid.Empty)
                        {
                            TrxId = Guid.NewGuid();
                        }
                        silo.totalPatientNet = int.Parse(itemPrices.First().PatientNetTotal.ToString());
                        silo.totalPayerNet = int.Parse(itemPrices.First().PayerNetTotal.ToString());
                        silo.payerCoverage = PayerCoverage;
                        silo.isSelfCollection = IsSelfCollection;
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
                        silo.items = drug;
                        silo.admissionHopeId = AdmissionId.ToString();
                        silo.userId = Updater.ToString();
                        silo.userName = UserName;
                        silo.source = "EMR";
                        silo.siloamTrxId = TrxId;
                        silo.appointmentId = IUnitOfWorks.UnitOfWorkPharmacy().GetAppointmentId(OrganizationId, PatientId, AdmissionId, DoctorId);

                        string JsonStringMySiloam = JsonConvert.SerializeObject(silo);
                        string resultMySiloam = IUnitOfWorks.UnitOfWorkSync().SyncDrugMySiloam(JsonStringMySiloam);
                        if (!result.ToUpper().Contains("KONEKSI KE MYSILOAM GAGAL"))
                        {
                            JObject ResponseSilo = (JObject)JsonConvert.DeserializeObject<dynamic>(resultMySiloam);
                            syncStatusAIDO = ResponseSilo.Property("status").Value.ToString();
                            syncMessageAIDO = ResponseSilo.Property("message").Value.ToString();
                            ErrMsg = ResponseSilo.Property("message").Value.ToString();
                            JsonResponse = resultMySiloam;
                        }
                        else
                        {
                            //if exception or timeout
                            syncStatusAIDO = "FAIL";
                            syncMessageAIDO = "KONEKSI KE MYSILOAM GAGAL";
                            ErrMsg = result;
                        }

                        if (syncStatusAIDO.ToLower() == "ok")
                        {
                            string insertaidoticket = IUnitOfWorks.UnitOfWorkAidoDrug().InsertData(OrganizationId, PatientId, AdmissionId, EncounterId, JsonStringMySiloam, JsonResponse, TrxId, ChannelId);
                            HttpResults = new ResponseData<string>("Data successfully updated " + ValueStorage.MySiloamUrlSync + " content: " + JsonStringMySiloam, Siloam.System.Web.StatusCode.OK, StatusMessage.Success, "SUCCESS|", page, total);
                        }
                        else
                        {
                            string insertaidofailed = IUnitOfWorks.UnitOfWorkAidoSync().InsertDataLogFailed(OrganizationId, PatientId, AdmissionId, EncounterId, "", JsonStringMySiloam, JsonResponse, ErrMsg, ChannelId);
                            HttpResults = new ResponseData<string>("Data successfully updated " + ValueStorage.MySiloamUrlSync + " content: " + JsonStringMySiloam, Siloam.System.Web.StatusCode.OK, StatusMessage.Success, "MYSILOAMFAILED|" + syncMessageAIDO, page, total);
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
                    Task<string> slackNotification = SlackMessageAsync(ValueStorage.SlackUrl, ValueStorage.ApiName, ValueStorage.ApiUrl + "/aidoresync", "[POST]Re Sync AIDO", EncounterId.ToString(), ex.Message);

                }
                else
                {

                    HttpResults = new ResponseMessage(Siloam.System.Web.StatusCode.UnprocessableEntity, StatusMessage.Fail, ex.Message, total);
                    Task<string> slackNotification = SlackMessageAsync(ValueStorage.SlackUrl, ValueStorage.ApiName, ValueStorage.ApiUrl + "/aidoresync", "[POST]Re Sync AIDO", EncounterId.ToString(), ex.Message);

                }

            }

            response:
            return HttpResponse(HttpResults);

        }

        [HttpGet("countneworder/{OrganizationId:long}")]
        [ProducesResponseType(typeof(ResponseData<List<Notification>>), 200)]
        public IActionResult GetCountNewOrder(long OrganizationId)
        {
            int total = 0, cp = 0;
            try
            {
                var data = IUnitOfWorks.UnitOfWorkAidoDrug().GetCountNewOrder(OrganizationId);
                if (data != null)
                {
                    total = data.Count();

                    if (total != 0)
                    {
                        HttpResults = new ResponseData<List<Notification>>("Get Data Count Aido New Order", Siloam.System.Web.StatusCode.OK, StatusMessage.Success, data, cp, total);
                    }
                    else
                    {
                        HttpResults = new ResponseMessage(Siloam.System.Web.StatusCode.OK, StatusMessage.Fail, "Data Not Found", total);
                    }
                }
                else
                {
                    HttpResults = new ResponseMessage(Siloam.System.Web.StatusCode.OK, StatusMessage.Fail, "Data Not Found", total);
                }
            }
            catch (Exception ex)
            {
                int exCode = ex.HResult;
                if (exCode == -2147467259)
                {
                    HttpResults = new ResponseMessage(Siloam.System.Web.StatusCode.InternalServerErrorException, StatusMessage.Error, ex.Message, total);
                    Task<string> slackNotification = SlackMessageAsync(ValueStorage.SlackUrl, ValueStorage.ApiName, ValueStorage.ApiUrl + "/countneworder", "[GET]Get Count New Order by Organizationid", OrganizationId.ToString(), ex.Message);
                }
                else
                {
                    HttpResults = new ResponseMessage(Siloam.System.Web.StatusCode.UnprocessableEntity, StatusMessage.Fail, ex.Message, total);
                    Task<string> slackNotification = SlackMessageAsync(ValueStorage.SlackUrl, ValueStorage.ApiName, ValueStorage.ApiUrl + "/countneworder", "[GET]Get Count New Order by Organizationid", OrganizationId.ToString(), ex.Message);
                }

            }
            return HttpResponse(HttpResults);
        }

        [HttpGet("countaidoneworder/{OrganizationId:long}")]
        [ProducesResponseType(typeof(ResponseData<int>), 200)]
        public IActionResult GetCountNewAIDOOrder(long OrganizationId)
        {

            int total = 0, cp = 0;

            try
            {

                var data = IUnitOfWorks.UnitOfWorkAidoDrug().GetCountNewAIDOOrder(OrganizationId);

                HttpResults = new ResponseData<int>("Get Data Count Aido New Order", Siloam.System.Web.StatusCode.OK, StatusMessage.Success, data, cp, total);

            }
            catch (Exception ex)
            {

                int exCode = ex.HResult;

                if (exCode == -2147467259)
                {
                    HttpResults = new ResponseMessage(Siloam.System.Web.StatusCode.InternalServerErrorException, StatusMessage.Error, ex.Message, total);
                    Task<string> slackNotification = SlackMessageAsync(ValueStorage.SlackUrl, ValueStorage.ApiName, ValueStorage.ApiUrl + "/countaidoneworder", "[GET]Get Count New AIDO Order by Organizationid", OrganizationId.ToString(), ex.Message);
                }
                else
                {
                    HttpResults = new ResponseMessage(Siloam.System.Web.StatusCode.UnprocessableEntity, StatusMessage.Fail, ex.Message, total);
                    Task<string> slackNotification = SlackMessageAsync(ValueStorage.SlackUrl, ValueStorage.ApiName, ValueStorage.ApiUrl + "/countaidoneworder", "[GET]Get Count New AIDO Order by Organizationid", OrganizationId.ToString(), ex.Message);
                }

            }

            return HttpResponse(HttpResults);
        }

        [HttpGet("countaidoinvoicedorder/{OrganizationId:long}")]
        [ProducesResponseType(typeof(ResponseData<int>), 200)]
        public IActionResult GetCountInvoicedAIDOOrder(long OrganizationId)
        {

            int total = 0, cp = 0;

            try
            {

                var data = IUnitOfWorks.UnitOfWorkAidoDrug().GetCountInvoicedAIDOOrder(OrganizationId);

                HttpResults = new ResponseData<int>("Get Data Count Aido Invoiced Order", Siloam.System.Web.StatusCode.OK, StatusMessage.Success, data, cp, total);

            }
            catch (Exception ex)
            {

                int exCode = ex.HResult;

                if (exCode == -2147467259)
                {
                    HttpResults = new ResponseMessage(Siloam.System.Web.StatusCode.InternalServerErrorException, StatusMessage.Error, ex.Message, total);
                    Task<string> slackNotification = SlackMessageAsync(ValueStorage.SlackUrl, ValueStorage.ApiName, ValueStorage.ApiUrl + "/countaidoinvoicedorder", "[GET]Get Count Invoiced AIDO Order by Organizationid", OrganizationId.ToString(), ex.Message);
                }
                else
                {
                    HttpResults = new ResponseMessage(Siloam.System.Web.StatusCode.UnprocessableEntity, StatusMessage.Fail, ex.Message, total);
                    Task<string> slackNotification = SlackMessageAsync(ValueStorage.SlackUrl, ValueStorage.ApiName, ValueStorage.ApiUrl + "/countaidoinvoicedorder", "[GET]Get Count Invoiced AIDO Order by Organizationid", OrganizationId.ToString(), ex.Message);
                }

            }

            return HttpResponse(HttpResults);
        }

        [HttpGet("getcountinvandcncl/{OrganizationId:long}")]
        [ProducesResponseType(typeof(ResponseData<int>), 200)]
        public IActionResult GetCountInvAndCncl(long OrganizationId)
        {

            int total = 0, cp = 0;

            try
            {
                var data = IUnitOfWorks.UnitOfWorkAidoDrug().GetCountInvAndCncl(OrganizationId);
                HttpResults = new ResponseData<int>("Get Data Count Invoiced Order and Cancel", Siloam.System.Web.StatusCode.OK, StatusMessage.Success, data, cp, total);
            }
            catch (Exception ex)
            {
                int exCode = ex.HResult;
                if (exCode == -2147467259)
                {
                    HttpResults = new ResponseMessage(Siloam.System.Web.StatusCode.InternalServerErrorException, StatusMessage.Error, ex.Message, total);
                    Task<string> slackNotification = SlackMessageAsync(ValueStorage.SlackUrl, ValueStorage.ApiName, ValueStorage.ApiUrl + "/getcountinvandcncl", "[GET]Get Count Invoiced And Cancel by Organizationid", OrganizationId.ToString(), ex.Message);
                }
                else
                {
                    HttpResults = new ResponseMessage(Siloam.System.Web.StatusCode.UnprocessableEntity, StatusMessage.Fail, ex.Message, total);
                    Task<string> slackNotification = SlackMessageAsync(ValueStorage.SlackUrl, ValueStorage.ApiName, ValueStorage.ApiUrl + "/getcountinvandcncl", "[GET]Get Count Invoiced And Cancel by Organizationid", OrganizationId.ToString(), ex.Message);
                }
            }

            return HttpResponse(HttpResults);
        }
    }
}