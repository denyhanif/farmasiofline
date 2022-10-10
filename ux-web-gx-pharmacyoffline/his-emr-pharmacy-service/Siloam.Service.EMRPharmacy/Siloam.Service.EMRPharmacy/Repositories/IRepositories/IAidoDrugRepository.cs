using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Siloam.Service.EMRPharmacy.Models;
using Siloam.Service.EMRPharmacy.Models.ViewModels;
using Siloam.Service.EMRPharmacy.Models.Functional;


namespace Siloam.Service.EMRPharmacy.Repositories.IRepositories
{
    public interface IAidoDrugRepository
    {
        string InsertData (long OrganizationId, long PatientId, long AdmissionId, Guid EncounterId, string JsonRequest, string JsonResponse, Guid SiloamTrxId, string ChannelId);
        string UpdateData(Guid SiloamTrxId);
        //int GetCountNewAIDOOrder(long OrganizationId);
        List<Notification> GetCountNewOrder(long OrganizationId);
        //int GetCountInvoicedAIDOOrder(long OrganizationId);
        int GetCountAIDOOrder(long OrganizationId, long PatientId, long AdmissionId, Guid EncounterId);
        Guid GetSiloamTrxId(long OrganizationId, long PatientId, long AdmissionId, Guid EncounterId);
    }
}
