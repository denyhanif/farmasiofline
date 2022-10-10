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
    public class LogZoomController : BaseController
    {
        private readonly IHubContext<MessageHub> messageHubContexts;


        public LogZoomController(IUnitOfWork unitOfWork, IHubContext<MessageHub> messageHubContext) : base(unitOfWork)
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

        [HttpPost("logzoom/{EncounterId}/{OrganizationId:long}/{PatientId:long}/{AdmissionId:long}")]
        [ProducesResponseType(typeof(ResponseData<string>), 200)]
        public IActionResult InsertLogZoom(Guid EncounterId, long OrganizationId, long PatientId, long AdmissionId)
        {

            int page = 1;
            int total = 0;

            try
            {

                DateTime dateNow = DateTime.Now;
                string data = IUnitOfWorks.UnitOfWorkLogZoom().InsertLogZoom(EncounterId, dateNow);
                string datamysiloam = IUnitOfWorks.UnitOfWorkLogZoom().DoneCheckInTele(EncounterId, dateNow, OrganizationId, PatientId, AdmissionId);

                messageHubContexts.Clients.All.InvokeAsync("Insert Zoom Log", data);

                HttpResults = new ResponseData<string>("Data successfully inserted", Siloam.System.Web.StatusCode.OK, StatusMessage.Success, data, page, total);
            }
            catch (Exception ex)
            {

                int exCode = ex.HResult;

                if (exCode == -2147467259)
                {

                    HttpResults = new ResponseMessage(Siloam.System.Web.StatusCode.InternalServerErrorException, StatusMessage.Error, ex.Message, total);
                    Task<string> slackNotification = SlackMessageAsync(ValueStorage.SlackUrl, ValueStorage.ApiName, ValueStorage.ApiUrl + "/logzoom", "[POST]Insert Log Zoom", EncounterId.ToString(), ex.Message);

                }
                else
                {

                    HttpResults = new ResponseMessage(Siloam.System.Web.StatusCode.UnprocessableEntity, StatusMessage.Fail, ex.Message, total);
                    Task<string> slackNotification = SlackMessageAsync(ValueStorage.SlackUrl, ValueStorage.ApiName, ValueStorage.ApiUrl + "/logzoom", "[POST]Insert Log Zoom", EncounterId.ToString(), ex.Message);

                }

            }

            return HttpResponse(HttpResults);

        }
    }
}