using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Siloam.Service.EMRPharmacy.Models;
using Siloam.Service.EMRPharmacy.Models.ViewModels;
using Siloam.Service.EMRPharmacy.Models.Functional;


namespace Siloam.Service.EMRPharmacy.Repositories.IRepositories
{
    public interface ISyncRepository
    {
        string SyncPrescription(long OrganizationId, long AdmissionId, string JsonString);
        string SyncConsumables(long OrganizationId, long AdmissionId, string JsonString);
        string SyncDrugMySiloam(string JsonString);
        string SyncPrescriptionToHope(long OrganizationId, long AdmissionId, long StoreId, long DoctorId, long UserId, string JsonString);
    }
}
