using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Siloam.Service.EMRPharmacy.Commons;
using Siloam.Service.EMRPharmacy.Hub;
using Siloam.Service.EMRPharmacy.Models;
using Siloam.Service.EMRPharmacy.Models.ExpressCheckout;
using Siloam.System.Data;
using Siloam.System.Web;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Siloam.Service.EMRPharmacy.Controllers
{
    public class ExpressCheckoutController : BaseController
    {
        private readonly IHubContext<MessageHub> messageHubContexts;


        public ExpressCheckoutController(IUnitOfWork unitOfWork, IHubContext<MessageHub> messageHubContext) : base(unitOfWork)
        {

            messageHubContexts = messageHubContext;

        }

        [HttpPost("expressitemissue/{OrganizationId:long}/{PatientId:long}/{AdmissionId:long}/{DoctorId:long}/{EncounterId}/{UserName}")]
        [ProducesResponseType(typeof(ResponseData<string>), 200)]
        public IActionResult ExpressProcessItemIssue(long OrganizationId, long PatientId, long AdmissionId, long DoctorId, Guid EncounterId, string UserName)
        {

            int page = 1;
            int total = 0;

            try
            {

                List<ExpressPrescription> Item = new List<ExpressPrescription>();
                Item = IUnitOfWorks.UnitOfWorkExpressCheckout().GetExpressPrescriptions(OrganizationId, AdmissionId, EncounterId);
                
                string data = IUnitOfWorks.UnitOfWorkExpressCheckout().ExpressProcessItemIssue(OrganizationId, PatientId, AdmissionId, DoctorId, EncounterId, UserName, Item);

                messageHubContexts.Clients.All.InvokeAsync("Express Item Issue", data);

                if (data == "SUCCESS")
                {
                    HttpResults = new ResponseData<string>("Data successfully processed", Siloam.System.Web.StatusCode.OK, StatusMessage.Success, data, page, total);
                }
                else
                {
                    HttpResults = new ResponseMessage(Siloam.System.Web.StatusCode.UnprocessableEntity, StatusMessage.Error, data, total);
                }
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