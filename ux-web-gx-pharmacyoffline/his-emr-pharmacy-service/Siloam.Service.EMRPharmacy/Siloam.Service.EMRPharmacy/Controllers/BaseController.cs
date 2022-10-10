/* ------------------------------------------------ */
/* Controller: BaseController                       */
/* ------------------------------------------------ */

/* Alfa Irawan                                      */
/* Siloam Software Engineering Team                 */
/* Wednesday, 27 September 2017                     */
/* Version 2.0                                      */
/*                                                  */
/* Copyright @ 2017-2018 Siloam                     */

/* ------------------------------------------------ */
/* Update Ver   : 2.0.1                             */
/* Update Person: Alfa Irawan                       */
/* Update Date  : 12 February 2018                  */
/* ------------------------------------------------ */




using System.Collections.Generic;
using Siloam.System.Web;
using Siloam.System.Data;
using Siloam.Service.EMRPharmacy.Commons;
using Microsoft.AspNetCore.Mvc;



namespace Siloam.Service.EMRPharmacy.Controllers
{

	
    [Produces("application/json")]
    [ProducesResponseType(typeof(ResponseMessage), 500)]
    [ProducesResponseType(typeof(ResponseMessage), 400)]
    [ProducesResponseType(typeof(ResponseData<Dictionary<string, string>>), 422)]
    public class BaseController : Controller
    {

		
        protected HttpResult HttpResults;

		
        protected IUnitOfWork IUnitOfWorks;

		
        public BaseController() { }

		
        public BaseController(IUnitOfWork iUnitofWorks)
        {

            IUnitOfWorks = iUnitofWorks;

        }

		
        public ObjectResult HttpResponse(HttpResult result)
        {
			
            ObjectResult objectResult = new ObjectResult(result);

            objectResult.StatusCode = result.GetResponseStatusCode();
            objectResult.Value = result;

            return objectResult;
			
        }

		
        protected override void Dispose(bool disposing)
        {
			
            IUnitOfWorks.Dispose();
            base.Dispose(disposing);
			
        }

    }
	
}
