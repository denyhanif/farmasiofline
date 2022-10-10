using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Siloam.Service.EMRPharmacy.Models;
using Siloam.Service.EMRPharmacy.Models.ViewModels;
using Siloam.Service.EMRPharmacy.Models.Functional;
using Siloam.Service.EMRPharmacy.Models.Parameter;

namespace Siloam.Service.EMRPharmacy.Repositories.IRepositories
{
    public interface ISingleQueueRepository
    {
        string UpdateDone(long OrganizationId, long PatientId, long AdmissionId, long DoctorId, Guid EncounterId, bool IsRetail, string Updater);
        string SyncWorklistSingleQ(string JsonString);
        string SyncCancelWorklistSingleQ(Guid queue_engine_trx_id, string JsonString);
        SingleQueue GetDataSingleQueue(Int64 OrganizationId, Int64 DoctorId, Int64 PatientId, Int64 AdmissionId);
        string CancelSingleQueue(Param_CancelSingleQ param_CSQ);
    }
}
