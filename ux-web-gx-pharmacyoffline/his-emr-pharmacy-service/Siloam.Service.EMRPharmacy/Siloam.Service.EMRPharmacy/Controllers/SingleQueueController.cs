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

namespace Siloam.Service.EMRPharmacy.Controllers
{
    public class SingleQueueController : BaseController
    {
        private readonly IHubContext<MessageHub> messageHubContexts;


        public SingleQueueController(IUnitOfWork unitOfWork, IHubContext<MessageHub> messageHubContext) : base(unitOfWork)
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

        [HttpPut("singlequeuedonepharmacy/{OrganizationId:long}/{PatientId:long}/{AdmissionId:long}/{DoctorId:long}/{EncounterId}/{IsRetail}/{Updater}")]
        [ProducesResponseType(typeof(ResponseData<string>), 200)]
        public IActionResult UpdateDone(long OrganizationId, long PatientId, long AdmissionId, long DoctorId, Guid EncounterId, bool IsRetail, string Updater)
        {

            int page = 1;
            int total = 0;

            try
            {
                string data = IUnitOfWorks.UnitOfWorkSingleQueue().UpdateDone(OrganizationId, PatientId, AdmissionId, DoctorId, EncounterId, IsRetail, Updater);
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
                string paramslack =OrganizationId.ToString()+"/"+PatientId.ToString()+"/"+AdmissionId.ToString()+"/"+DoctorId.ToString()+"/"+EncounterId.ToString()+"/"+IsRetail.ToString()+"/"+Updater.ToString();
                if (exCode == -2147467259)
                {

                    HttpResults = new ResponseMessage(Siloam.System.Web.StatusCode.InternalServerErrorException, StatusMessage.Error, ex.Message, total);
                    Task<string> slackNotification = SlackMessageAsync(ValueStorage.SlackUrl, ValueStorage.ApiName, ValueStorage.ApiUrl + "/singlequeuedonepharmacy", "[PUT]Update Done By OrganizationId, PatientId, AdmissionId, DoctorId, EncounterId, IsRetail, Updater", paramslack.ToString(), ex.Message);

                }
                else
                {

                    HttpResults = new ResponseMessage(Siloam.System.Web.StatusCode.UnprocessableEntity, StatusMessage.Fail, ex.Message, total);
                    Task<string> slackNotification = SlackMessageAsync(ValueStorage.SlackUrl, ValueStorage.ApiName, ValueStorage.ApiUrl + "/singlequeuedonepharmacy", "[PUT]Update Done By OrganizationId, PatientId, AdmissionId, DoctorId, EncounterId, IsRetail, Updater", paramslack.ToString(), ex.Message);

                }

            }

            return HttpResponse(HttpResults);

        }

        [HttpGet("getdatasinglequeue/{OrganizationId:long}/{DoctorId:long}/{PatientId:long}/{AdmissionId:long}")]
        [ProducesResponseType(typeof(ResponseData<SingleQueue>), 200)]
        public IActionResult GetDataPrecriptionCentral(Int64 OrganizationId, Int64 DoctorId, Int64 PatientId, Int64 AdmissionId)
        {
            int total = 0, cp = 0;
            try
            {
                var data = IUnitOfWorks.UnitOfWorkSingleQueue().GetDataSingleQueue(OrganizationId, DoctorId, PatientId, AdmissionId);
                if (data != null && !data.queue_engine_trx_id.ToString().Equals(Guid.Empty.ToString()))
                {
                    total = 1;
                    HttpResults = new ResponseData<SingleQueue>("Get Data SingleQueue by Admission Id " + AdmissionId, Siloam.System.Web.StatusCode.OK, StatusMessage.Success, data, cp, total);
                }
                else
                {
                    HttpResults = new ResponseMessage(Siloam.System.Web.StatusCode.OK, StatusMessage.Fail, "Data Not Found", total);
                }
            }
            catch (Exception ex)
            {

                int exCode = ex.HResult;
                string paramslack = OrganizationId.ToString()+"/"+PatientId.ToString()+"/"+AdmissionId.ToString()+"/"+DoctorId.ToString() ;
                if (exCode == -2147467259)
                {
                    HttpResults = new ResponseMessage(Siloam.System.Web.StatusCode.InternalServerErrorException, StatusMessage.Error, ex.Message, total);
                    Task<string> slackNotification = SlackMessageAsync(ValueStorage.SlackUrl, ValueStorage.ApiName, ValueStorage.ApiUrl + "/getdatasinglequeue", "[GET]Get Data Single Queue By OrganiOrganizationId, DoctorId, PatientId, AdmissionId", paramslack.ToString(), ex.Message);
                }
                else
                {
                    HttpResults = new ResponseMessage(Siloam.System.Web.StatusCode.UnprocessableEntity, StatusMessage.Fail, ex.Message, total);
                }
            }

            return HttpResponse(HttpResults);
        }
        [HttpPost("cancelsinglequeue/{Queuetrxid}/{OrganizationId:long}/{DoctorId:long}/{PatientId:long}/{AdmissionId:long}")]
        [ProducesResponseType(typeof(ResponseData<string>), 200)]
        public IActionResult cancelsinglequeue(Guid Queuetrxid, Int64 OrganizationId, Int64 DoctorId, Int64 PatientId, Int64 AdmissionId, [FromBody] Queue modelQ)
        {
            int page = 1;
            int total = 0;
            string jsonQueueModel = "";
            try
            {
                jsonQueueModel = JsonConvert.SerializeObject(modelQ);
                string CancelWorklistSingleQ = "SIGNLEQSUCCESS";
                string status_SQ = "OK";
                string message_SQ = "OK";
                if (modelQ != null)
                {
                    CancelWorklistSingleQ = IUnitOfWorks.UnitOfWorkSingleQueue().SyncCancelWorklistSingleQ(Queuetrxid, jsonQueueModel);
                    var objResult = JsonConvert.DeserializeObject<ResultSingleQueue>(CancelWorklistSingleQ);
                    status_SQ = objResult.status;
                    message_SQ = objResult.message;
                }
                else
                {
                    status_SQ = "Fail";
                    CancelWorklistSingleQ = "SIGNLEQDATANULL";
                    message_SQ = "SingleQueue Worklist Null";
                }
                Param_CancelSingleQ param_CSQ = new Param_CancelSingleQ();
                param_CSQ.Queuetrxid = Queuetrxid;
                param_CSQ.OrganizationId = OrganizationId;
                param_CSQ.DoctorId = DoctorId;
                param_CSQ.PatientId = PatientId;
                param_CSQ.AdmissionId = AdmissionId;
                param_CSQ.updateby = modelQ.userId.ToString();
                param_CSQ.jsonrequest_cancel_singleq = jsonQueueModel.ToString();
                param_CSQ.jsonresponse_cancel_singleq = CancelWorklistSingleQ.ToString();
                if (status_SQ.Equals("OK"))
                {

                    param_CSQ.is_cancel = true;
                    string data = IUnitOfWorks.UnitOfWorkSingleQueue().CancelSingleQueue(param_CSQ);
                    messageHubContexts.Clients.All.InvokeAsync("Update Pharmacy Drug Info", data);
                    HttpResults = new ResponseData<string>("Data successfully updated", Siloam.System.Web.StatusCode.OK, StatusMessage.Success, data, page, total);
                }
                else
                {
                    param_CSQ.is_cancel = false;
                    string data = IUnitOfWorks.UnitOfWorkSingleQueue().CancelSingleQueue(param_CSQ);
                    HttpResults = new ResponseMessage(Siloam.System.Web.StatusCode.BadRequest, StatusMessage.Fail, HttpResponseMessageKey.DataUnsuccessfullyCreated, total);
                    Task<string> slackNotification = SlackMessageAsync(ValueStorage.SlackUrl, ValueStorage.ApiName, ValueStorage.ApiUrl + "/cancelsinglequeue", "[POST]Cancel Single Queueid", Queuetrxid +"/admissionid/"+ AdmissionId + "/" + jsonQueueModel, "Data Unsuccessfully Canceled");
                }
                
            }
            catch (Exception ex)
            {
                int exCode = ex.HResult;

                if (exCode == -2147467259)
                {

                    HttpResults = new ResponseMessage(Siloam.System.Web.StatusCode.InternalServerErrorException, StatusMessage.Error, ex.Message, total);
                    Task<string> slackNotification = SlackMessageAsync(ValueStorage.SlackUrl, ValueStorage.ApiName, ValueStorage.ApiUrl + "/cancelsinglequeue", "[POST]Update Cancel Queue", Queuetrxid.ToString() + jsonQueueModel.ToString(), ex.Message);

                }
                else
                {

                    HttpResults = new ResponseMessage(Siloam.System.Web.StatusCode.UnprocessableEntity, StatusMessage.Fail, ex.Message, total);
                    Task<string> slackNotification = SlackMessageAsync(ValueStorage.SlackUrl, ValueStorage.ApiName, ValueStorage.ApiUrl + "/cancelsinglequeue", "[POST]Update Cancel Queue", Queuetrxid.ToString() + jsonQueueModel.ToString(), ex.Message);

                }
            }
        response:
            return HttpResponse(HttpResults);
        }
    }
}