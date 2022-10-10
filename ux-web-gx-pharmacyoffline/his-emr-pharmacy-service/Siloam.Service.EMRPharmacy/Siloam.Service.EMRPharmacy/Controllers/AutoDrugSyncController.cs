using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using Microsoft.AspNetCore.Mvc;
using Siloam.System.Web;
using Siloam.System.Data;
using Siloam.Service.EMRPharmacy.Models;
using Siloam.Service.EMRPharmacy.Models.ViewModels;
using Siloam.Service.EMRPharmacy.Models.AutoSync;
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
    public class AutoDrugSyncController : BaseController
    {
        private readonly IHubContext<MessageHub> messageHubContexts;


        public AutoDrugSyncController(IUnitOfWork unitOfWork, IHubContext<MessageHub> messageHubContext) : base(unitOfWork)
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

        [HttpPost("centralappropriateness")]
        [ProducesResponseType(typeof(ResponseData<string>), 200)]
        public IActionResult SubmitAppropriatenessReviewCentral([FromBody]InsertAppropriateness model)
        {

            int page = 1;
            int total = 0;

            try
            {

                string data = IUnitOfWorks.UnitOfWorkAutoSync().SubmitAppropriatenessReviewCentral(model);

                messageHubContexts.Clients.All.InvokeAsync("Insert Central Appropriateness", data);

                HttpResults = new ResponseData<string>("Data successfully inserted", Siloam.System.Web.StatusCode.OK, StatusMessage.Success, data, page, total);

            }
            catch (Exception ex)
            {

                int exCode = ex.HResult;
                string jsonModel = JsonConvert.SerializeObject(model);

                if (exCode == -2147467259)
                {

                    HttpResults = new ResponseMessage(Siloam.System.Web.StatusCode.InternalServerErrorException, StatusMessage.Error, ex.Message, total);
                    Task<string> slackNotification = SlackMessageAsync(ValueStorage.SlackUrl, ValueStorage.ApiName, ValueStorage.ApiUrl + "/centralappropriateness", "[POST]Insert Central Appropriateness", jsonModel.ToString(), ex.Message);

                }
                else
                {

                    HttpResults = new ResponseMessage(Siloam.System.Web.StatusCode.UnprocessableEntity, StatusMessage.Fail, ex.Message, total);
                    Task<string> slackNotification = SlackMessageAsync(ValueStorage.SlackUrl, ValueStorage.ApiName, ValueStorage.ApiUrl + "/centralappropriateness", "[POST]Insert Central Appropriateness", jsonModel.ToString(), ex.Message);

                }

            }

            response:
            return HttpResponse(HttpResults);

        }

        [HttpPost("readypickup")]
        [ProducesResponseType(typeof(ResponseData<string>), 200)]
        public IActionResult SendReadyPickupMySiloam([FromBody]RequestReadyPickup model)
        {

            int page = 1;
            int total = 0;

            try
            {
                string syncStatusMySiloam = "";
                string syncMessageMySiloam = "";
                string resultMySiloam = "";
                string JsonResponseMySiloam = "";
                string ErrMsgMySiloam = "";
                string data = "";
                string jsonRequest = "";
                bool FailAIDO = false;

                Guid AppointmentId = IUnitOfWorks.UnitOfWorkPharmacy().GetAppointmentId(model.OrganizationId, model.PatientId, model.AdmissionId, model.DoctorId);

                //if (model.IsSelfPickup == 1 || (!resultMySiloam.ToUpper().Contains("KONEKSI KE AIDO GAGAL") && resultMySiloam.ToUpper().Contains("SUCCESS")))
                //{
                    MySiloamRequestReadyPickup silo = new MySiloamRequestReadyPickup();
                    silo.encounterId = model.EncounterId.ToString();
                    silo.userId = model.UserId.ToString();
                    silo.source = "EMR";
                    silo.userName = model.UserName;
                    jsonRequest = JsonConvert.SerializeObject(silo);
                    resultMySiloam = IUnitOfWorks.UnitOfWorkAutoSync().SendReadyPickupMySiloam(jsonRequest);
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
                        JsonResponseMySiloam = resultMySiloam;
                        FailAIDO = true;
                    }
                //}
                //else
                //{
                //    FailAIDO = true;
                //    //if exception or timeout
                //    syncStatusMySiloam = "FAIL";
                //    syncMessageMySiloam = "KONEKSI KE AIDO GAGAL";
                //    JsonResponseMySiloam = resultMySiloam;
                //}

                //if (!resultMySiloam.ToUpper().Contains("KONEKSI KE AIDO GAGAL"))
                //{
                //    JObject ResponseSilo = (JObject)JsonConvert.DeserializeObject<dynamic>(resultMySiloam);
                //    syncStatusMySiloam = ResponseSilo.Property("status").Value.ToString();
                //    syncMessageMySiloam = ResponseSilo.Property("message").Value.ToString();
                //    ErrMsgMySiloam = ResponseSilo.Property("message").Value.ToString();
                //    JsonResponseMySiloam = resultMySiloam;
                //}
                

                if (syncStatusMySiloam.ToLower() == "ok")
                {
                    data = IUnitOfWorks.UnitOfWorkAutoSync().InsertLogReadyPickup(model, syncStatusMySiloam, jsonRequest, JsonResponseMySiloam, AppointmentId, true);
                    if(data.Equals("SUCCESS"))
                    {
                        data = IUnitOfWorks.UnitOfWorkAutoSync().UpdateDrugsReadyPickup(model, syncStatusMySiloam, jsonRequest, JsonResponseMySiloam, AppointmentId, true);
                    }
                    else
                    {
                        FailAIDO = true;
                    }
                }
                else
                {
                    data = IUnitOfWorks.UnitOfWorkAutoSync().InsertLogReadyPickup(model, syncStatusMySiloam, jsonRequest, JsonResponseMySiloam, AppointmentId, false);
                    FailAIDO = true;
                }

                if (FailAIDO)
                {
                    messageHubContexts.Clients.All.InvokeAsync("Send Pickup Ready", data);
                    HttpResults = new ResponseData<string>("Fail to request pickup," + JsonResponseMySiloam, Siloam.System.Web.StatusCode.OK, StatusMessage.Error, data, page, total);
                }
                else
                {
                    messageHubContexts.Clients.All.InvokeAsync("Send Pickup Ready", data);
                    HttpResults = new ResponseData<string>("Data successfully updated", Siloam.System.Web.StatusCode.OK, StatusMessage.Success, data, page, total);
                }

            }
            catch (Exception ex)
            {

                int exCode = ex.HResult;
                string jsonModel = JsonConvert.SerializeObject(model);

                if (exCode == -2147467259)
                {

                    HttpResults = new ResponseMessage(Siloam.System.Web.StatusCode.InternalServerErrorException, StatusMessage.Error, ex.Message, total);
                    Task<string> slackNotification = SlackMessageAsync(ValueStorage.SlackUrl, ValueStorage.ApiName, ValueStorage.ApiUrl + "/readypickup", "[POST]Send Ready Pickup", jsonModel.ToString(), ex.Message);

                }
                else
                {

                    HttpResults = new ResponseMessage(Siloam.System.Web.StatusCode.UnprocessableEntity, StatusMessage.Fail, ex.Message, total);
                    Task<string> slackNotification = SlackMessageAsync(ValueStorage.SlackUrl, ValueStorage.ApiName, ValueStorage.ApiUrl + "/readypickup", "[POST]Send Ready Pickup", jsonModel.ToString(), ex.Message);

                }

            }

            response:
            return HttpResponse(HttpResults);

        }

        [HttpGet("teleconsulstock/{OrganizationId:long}")]
        [ProducesResponseType(typeof(ResponseData<List<TeleconsulStock>>), 200)]
        public IActionResult GetTeleconsulStock(long OrganizationId)
        {

            int page = 1;
            int total = 0;

            try
            {
                var data = IUnitOfWorks.UnitOfWorkAutoSync().GetTeleconsulStock(OrganizationId);
                
                HttpResults = new ResponseData<List<TeleconsulStock>>("Get Data Teleconsul Stock", Siloam.System.Web.StatusCode.OK, StatusMessage.Success, data, page, total);
            }
            catch (Exception ex)
            {

                int exCode = ex.HResult;

                if (exCode == -2147467259)
                {

                    HttpResults = new ResponseMessage(Siloam.System.Web.StatusCode.InternalServerErrorException, StatusMessage.Error, ex.Message, total);
                    Task<string> slackNotification = SlackMessageAsync(ValueStorage.SlackUrl, ValueStorage.ApiName, ValueStorage.ApiUrl + "/teleconsulstock", "[GET]Get Teleconsul Stock By Organizationid", OrganizationId.ToString(), ex.Message);

                }
                else
                {

                    HttpResults = new ResponseMessage(Siloam.System.Web.StatusCode.UnprocessableEntity, StatusMessage.Fail, ex.Message, total);
                    Task<string> slackNotification = SlackMessageAsync(ValueStorage.SlackUrl, ValueStorage.ApiName, ValueStorage.ApiUrl + "/teleconsulstock", "[GET]Get Teleconsul Stock By Organizationid", OrganizationId.ToString(), ex.Message);
                }

            }

            return HttpResponse(HttpResults);

        }

        [HttpPost("centralprescription/{UserId:long}/{Notes}")]
        [ProducesResponseType(typeof(ResponseData<string>), 200)]
        public IActionResult InsertCentralPrescription(long UserId, string Notes, [FromBody] List<PrescriptionCentral> model)
        {

            int page = 1;
            int total = 0;

            try
            {

                string data = IUnitOfWorks.UnitOfWorkAutoSync().InsertCentralPrescription(model, UserId, Notes);

                messageHubContexts.Clients.All.InvokeAsync("Insert Central Prescription", data);

                HttpResults = new ResponseData<string>("Data successfully inserted", Siloam.System.Web.StatusCode.OK, StatusMessage.Success, data, page, total);

            }
            catch (Exception ex)
            {

                int exCode = ex.HResult;
                string jsonModel = JsonConvert.SerializeObject(model);

                if (exCode == -2147467259)
                {

                    HttpResults = new ResponseMessage(Siloam.System.Web.StatusCode.InternalServerErrorException, StatusMessage.Error, ex.Message, total);
                    Task<string> slackNotification = SlackMessageAsync(ValueStorage.SlackUrl, ValueStorage.ApiName, ValueStorage.ApiUrl + "/centralprescription", "[POST]Insert Central Prescription", jsonModel.ToString(), ex.Message);

                }
                else
                {

                    HttpResults = new ResponseMessage(Siloam.System.Web.StatusCode.UnprocessableEntity, StatusMessage.Fail, ex.Message, total);
                    Task<string> slackNotification = SlackMessageAsync(ValueStorage.SlackUrl, ValueStorage.ApiName, ValueStorage.ApiUrl + "/centralprescription", "[POST]Insert Central Prescription", jsonModel.ToString(), ex.Message);

                }

            }

            response:
            return HttpResponse(HttpResults);

        }

        [HttpPost("resendprescription/{OrganizationId:long}/{PatientId:long}/{AdmissionId:long}/{EncounterId}/{UserId:long}")]
        [ProducesResponseType(typeof(ResponseData<string>), 200)]
        public IActionResult ResendPrescription(long OrganizationId, long PatientId, long AdmissionId, Guid EncounterId, long UserId)
        {

            int page = 1;
            int total = 0;

            try
            {

                string data = IUnitOfWorks.UnitOfWorkAutoSync().ResendPrescription(OrganizationId, PatientId, AdmissionId, EncounterId, UserId);

                messageHubContexts.Clients.All.InvokeAsync("Resend Prescription", data);

                HttpResults = new ResponseData<string>("Data successfully inserted", Siloam.System.Web.StatusCode.OK, StatusMessage.Success, data, page, total);

            }
            catch (Exception ex)
            {

                int exCode = ex.HResult;

                if (exCode == -2147467259)
                {

                    HttpResults = new ResponseMessage(Siloam.System.Web.StatusCode.InternalServerErrorException, StatusMessage.Error, ex.Message, total);
                    Task<string> slackNotification = SlackMessageAsync(ValueStorage.SlackUrl, ValueStorage.ApiName, ValueStorage.ApiUrl + "/resendprescription", "[POST]Insert Resend Prescription", EncounterId.ToString() + ", " + UserId.ToString() , ex.Message);

                }
                else
                {

                    HttpResults = new ResponseMessage(Siloam.System.Web.StatusCode.UnprocessableEntity, StatusMessage.Fail, ex.Message, total);
                    Task<string> slackNotification = SlackMessageAsync(ValueStorage.SlackUrl, ValueStorage.ApiName, ValueStorage.ApiUrl + "/resendprescription", "[POST]Insert Resend Prescription", EncounterId.ToString() + ", " + UserId.ToString(), ex.Message);

                }

            }

            response:
            return HttpResponse(HttpResults);

        }

        [HttpPost("resendorcancelprescription/{OrganizationId:long}/{PatientId:long}/{AdmissionId:long}/{EncounterId}/{UserId:long}/{Notes}/{IsCancel:bool}")]
        [ProducesResponseType(typeof(ResponseData<string>), 200)]
        public IActionResult ResendOrCancelPrescription(long OrganizationId, long PatientId, long AdmissionId, Guid EncounterId, long UserId, string Notes, bool IsCancel)
        {

            int page = 1;
            int total = 0;
            try
            {
                string data = IUnitOfWorks.UnitOfWorkAutoSync().ResendOrCancelPrescription(OrganizationId, PatientId, AdmissionId, EncounterId, UserId, Notes, IsCancel);

                messageHubContexts.Clients.All.InvokeAsync("Resend Prescription", data);

                HttpResults = new ResponseData<string>("Data successfully inserted", Siloam.System.Web.StatusCode.OK, StatusMessage.Success, data, page, total);
            }
            catch (Exception ex)
            {
                int exCode = ex.HResult;

                if (exCode == -2147467259)
                {

                    HttpResults = new ResponseMessage(Siloam.System.Web.StatusCode.InternalServerErrorException, StatusMessage.Error, ex.Message, total);
                    Task<string> slackNotification = SlackMessageAsync(ValueStorage.SlackUrl, ValueStorage.ApiName, ValueStorage.ApiUrl + "/resendorcancelprescription", "[POST]Insert Resend Prescription", EncounterId.ToString() + ", " + UserId.ToString(), ex.Message);

                }
                else
                {

                    HttpResults = new ResponseMessage(Siloam.System.Web.StatusCode.UnprocessableEntity, StatusMessage.Fail, ex.Message, total);
                    Task<string> slackNotification = SlackMessageAsync(ValueStorage.SlackUrl, ValueStorage.ApiName, ValueStorage.ApiUrl + "/resendorcancelprescription", "[POST]Insert Resend Prescription", EncounterId.ToString() + ", " + UserId.ToString(), ex.Message);

                }
            }
            response:
            return HttpResponse(HttpResults);
        }

        [HttpPost("patientquestion")]
        [ProducesResponseType(typeof(ResponseData<string>), 200)]
        public IActionResult InsertQuestion([FromBody]List<InsertQuestion> model)
        {

            int page = 1;
            int total = 0;

            try
            {

                string data = IUnitOfWorks.UnitOfWorkAutoSync().InsertQuestion(model);

                messageHubContexts.Clients.All.InvokeAsync("Insert Question", data);

                HttpResults = new ResponseData<string>("Data successfully inserted", Siloam.System.Web.StatusCode.OK, StatusMessage.Success, data, page, total);

            }
            catch (Exception ex)
            {

                int exCode = ex.HResult;
                string jsonModel = JsonConvert.SerializeObject(model);

                if (exCode == -2147467259)
                {

                    HttpResults = new ResponseMessage(Siloam.System.Web.StatusCode.InternalServerErrorException, StatusMessage.Error, ex.Message, total);
                    Task<string> slackNotification = SlackMessageAsync(ValueStorage.SlackUrl, ValueStorage.ApiName, ValueStorage.ApiUrl + "/patientquestion", "[POST]Insert Patient Question", jsonModel.ToString(), ex.Message);

                }
                else
                {

                    HttpResults = new ResponseMessage(Siloam.System.Web.StatusCode.UnprocessableEntity, StatusMessage.Fail, ex.Message, total);
                    Task<string> slackNotification = SlackMessageAsync(ValueStorage.SlackUrl, ValueStorage.ApiName, ValueStorage.ApiUrl + "/patientquestion", "[POST]Insert Patient Question", jsonModel.ToString(), ex.Message);

                }

            }

            response:
            return HttpResponse(HttpResults);

        }

        [HttpPost("patientskipdrug")]
        [ProducesResponseType(typeof(ResponseData<string>), 200)]
        public IActionResult InsertSkipDrug([FromBody] InsertSkipDrug model)
        {

            int page = 1;
            int total = 0;

            try
            {

                string data = IUnitOfWorks.UnitOfWorkAutoSync().InsertSkipDrug(model);

                messageHubContexts.Clients.All.InvokeAsync("Insert Skip Drug", data);

                HttpResults = new ResponseData<string>("Data successfully inserted", Siloam.System.Web.StatusCode.OK, StatusMessage.Success, data, page, total);

            }
            catch (Exception ex)
            {

                int exCode = ex.HResult;
                string jsonModel = JsonConvert.SerializeObject(model);

                if (exCode == -2147467259)
                {

                    HttpResults = new ResponseMessage(Siloam.System.Web.StatusCode.InternalServerErrorException, StatusMessage.Error, ex.Message, total);
                    Task<string> slackNotification = SlackMessageAsync(ValueStorage.SlackUrl, ValueStorage.ApiName, ValueStorage.ApiUrl + "/patientskipdrug", "[POST]Insert Patient Skip Drug", jsonModel.ToString(), ex.Message);

                }
                else
                {

                    HttpResults = new ResponseMessage(Siloam.System.Web.StatusCode.UnprocessableEntity, StatusMessage.Fail, ex.Message, total);
                    Task<string> slackNotification = SlackMessageAsync(ValueStorage.SlackUrl, ValueStorage.ApiName, ValueStorage.ApiUrl + "/patientskipdrug", "[POST]Insert Patient Skip Drug", jsonModel.ToString(), ex.Message);

                }

            }

            response:
            return HttpResponse(HttpResults);

        }

        [HttpPost("deliveryfee")]
        [ProducesResponseType(typeof(ResponseData<TeleconsultationDeliveryHeader>), 200)]
        public IActionResult GetDeliveryFee([FromBody]DrugDelivery model)
        {

            int page = 1;
            int total = 0;

            try
            {

                string jsonModel = JsonConvert.SerializeObject(model);
                var data = IUnitOfWorks.UnitOfWorkAutoSync().GetDeliveryFee(jsonModel);

                messageHubContexts.Clients.All.InvokeAsync("Get Delivery Fee", data);

                HttpResults = new ResponseData<TeleconsultationDeliveryHeader>("Get Delivery Fee", Siloam.System.Web.StatusCode.OK, StatusMessage.Success, data, page, total);

            }
            catch (Exception ex)
            {

                int exCode = ex.HResult;
                string jsonModel = JsonConvert.SerializeObject(model);

                if (exCode == -2147467259)
                {

                    HttpResults = new ResponseMessage(Siloam.System.Web.StatusCode.InternalServerErrorException, StatusMessage.Error, ex.Message, total);
                    Task<string> slackNotification = SlackMessageAsync(ValueStorage.SlackUrl, ValueStorage.ApiName, ValueStorage.ApiUrl + "/deliveryfee", "[POST]Delivery Fee", jsonModel.ToString(), ex.Message);

                }
                else
                {

                    HttpResults = new ResponseMessage(Siloam.System.Web.StatusCode.UnprocessableEntity, StatusMessage.Fail, ex.Message, total);
                    Task<string> slackNotification = SlackMessageAsync(ValueStorage.SlackUrl, ValueStorage.ApiName, ValueStorage.ApiUrl + "/deliveryfee", "[POST]Delivery Fee", jsonModel.ToString(), ex.Message);

                }

            }

            response:
            return HttpResponse(HttpResults);

        }

        [HttpGet("autoprice/{OrganizationId:long}/{PatientId:long}/{AdmissionId:long}/{EncounterId}")]
        [ProducesResponseType(typeof(ResponseData<List<ItemPriceAuto>>), 200)]
        public IActionResult GetItemPriceAuto(long OrganizationId, long PatientId, long AdmissionId, Guid EncounterId)
        {

            int page = 1;
            int total = 0;

            try
            {
                var data = IUnitOfWorks.UnitOfWorkAutoSync().GetItemPriceAuto(OrganizationId,PatientId,AdmissionId,EncounterId,false);

                HttpResults = new ResponseData<List<ItemPriceAuto>>("Get Data Item Price", Siloam.System.Web.StatusCode.OK, StatusMessage.Success, data, page, total);
            }
            catch (Exception ex)
            {

                int exCode = ex.HResult;
                string paramslack = OrganizationId.ToString()+"/"+PatientId.ToString()+"/"+AdmissionId.ToString()+"/"+EncounterId.ToString();
                if (exCode == -2147467259)
                {

                    HttpResults = new ResponseMessage(Siloam.System.Web.StatusCode.InternalServerErrorException, StatusMessage.Error, ex.Message, total);
                    Task<string> slackNotification = SlackMessageAsync(ValueStorage.SlackUrl, ValueStorage.ApiName, ValueStorage.ApiUrl + "/autoprice", "[GET]Get Item Price Auto By Organizationid,patientid,admissionid,EncounterId", paramslack.ToString(), ex.Message);
                }
                else
                {

                    HttpResults = new ResponseMessage(Siloam.System.Web.StatusCode.UnprocessableEntity, StatusMessage.Fail, ex.Message, total);
                    Task<string> slackNotification = SlackMessageAsync(ValueStorage.SlackUrl, ValueStorage.ApiName, ValueStorage.ApiUrl + "/autoprice", "[GET]Get Item Price Auto By Organizationid,patientid,admissionid,EncounterId", paramslack.ToString(), ex.Message);

                }

            }

            return HttpResponse(HttpResults);

        }

        [HttpPut("ticketsync/{OrganizationId:long}/{PatientId:long}/{AdmissionId:long}/{EncounterId}")]
        [ProducesResponseType(typeof(ResponseData<string>), 200)]
        public IActionResult UpdateTicketSync(long OrganizationId, long PatientId, long AdmissionId, Guid EncounterId, bool IsSuccess)
        {

            int page = 1;
            int total = 0;

            try
            {
                var data = IUnitOfWorks.UnitOfWorkAutoSync().UpdateTicketSync(OrganizationId, PatientId, AdmissionId, EncounterId, IsSuccess);

                HttpResults = new ResponseData<string>("Data Updated", Siloam.System.Web.StatusCode.OK, StatusMessage.Success, data, page, total);
            }
            catch (Exception ex)
            {

                int exCode = ex.HResult;
                string paramslack = OrganizationId.ToString()+"/"+PatientId.ToString()+"/"+AdmissionId.ToString()+"/"+EncounterId.ToString();
                if (exCode == -2147467259)
                {

                    HttpResults = new ResponseMessage(Siloam.System.Web.StatusCode.InternalServerErrorException, StatusMessage.Error, ex.Message, total);
                    Task<string> slackNotification = SlackMessageAsync(ValueStorage.SlackUrl, ValueStorage.ApiName, ValueStorage.ApiUrl + "/ticketsync", "[PUT]Update Ticket Sync By Organizationid,patientid,admissionid,EncounterId", paramslack.ToString(), ex.Message);

                }
                else
                {

                    HttpResults = new ResponseMessage(Siloam.System.Web.StatusCode.UnprocessableEntity, StatusMessage.Fail, ex.Message, total);
                    Task<string> slackNotification = SlackMessageAsync(ValueStorage.SlackUrl, ValueStorage.ApiName, ValueStorage.ApiUrl + "/ticketsync", "[PUT]Update Ticket Sync By Organizationid,patientid,admissionid,EncounterId", paramslack.ToString(), ex.Message);

                }

            }

            return HttpResponse(HttpResults);

        }

        [HttpGet("autosyncdrug/{OrganizationId:long}")]
        [ProducesResponseType(typeof(ResponseData<string>), 200)]
        public IActionResult AutoSync(long OrganizationId)
        {

            int page = 1;
            int total = 0;

            try
            {
                var data = IUnitOfWorks.UnitOfWorkAutoSync().AutoSync(OrganizationId);

                HttpResults = new ResponseData<string>("Auto Sync Drug", Siloam.System.Web.StatusCode.OK, StatusMessage.Success, data, page, total);
            }
            catch (Exception ex)
            {

                int exCode = ex.HResult;

                if (exCode == -2147467259)
                {

                    HttpResults = new ResponseMessage(Siloam.System.Web.StatusCode.InternalServerErrorException, StatusMessage.Error, ex.Message, total);
                    Task<string> slackNotification = SlackMessageAsync(ValueStorage.SlackUrl, ValueStorage.ApiName, ValueStorage.ApiUrl + "/autosyncdrug", "[GET]Auto Sync By Organizationid", OrganizationId.ToString(), ex.Message);
                }
                else
                {

                    HttpResults = new ResponseMessage(Siloam.System.Web.StatusCode.UnprocessableEntity, StatusMessage.Fail, ex.Message, total);
                    Task<string> slackNotification = SlackMessageAsync(ValueStorage.SlackUrl, ValueStorage.ApiName, ValueStorage.ApiUrl + "/autosyncdrug", "[GET]Auto Sync By Organizationid", OrganizationId.ToString(), ex.Message);

                }

            }

            return HttpResponse(HttpResults);

        }
    }
}