using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Siloam.Service.EMRPharmacy.Models;
using Siloam.Service.EMRPharmacy.Models.ExpressCheckout;


namespace Siloam.Service.EMRPharmacy.Repositories.IRepositories
{
    public interface IExpressCheckoutRepository
    {
        List<ExpressPrescription> GetExpressPrescriptions(long OrganizationId, long AdmissionId, Guid EncounterId);
        string ExpressProcessItemIssue(long OrganizationId, long PatientId, long AdmissionId, long DoctorId, Guid EncounterId, string UserName, List<ExpressPrescription> Item);
    }
}
