using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Siloam.Service.EMRPharmacy.Models;
using Siloam.Service.EMRPharmacy.Models.ViewModels;
using Siloam.Service.EMRPharmacy.Models.Functional;


namespace Siloam.Service.EMRPharmacy.Repositories.IRepositories
{
    public interface IAidoSyncRepository
    {
        string AidoSyncPrescription(string JsonString, long OrganizationId, long PatientId, long AdmissionId, long DoctorId, string token);
        string GetTransactionId(long OrganizationId, long PatientId, long AdmissionId, long DoctorId);
        List<ItemPrice> GetItemSync(long OrganizationId, long PatientId, long AdmissionId, Guid EncounterId, List<PharmacyPrescription> Prescription, string DeliveryFee, bool IsEditDrug, bool IsEditConsumable);
        string GenerateJSONWebToken(long OrganizationId, long PatientId, long AdmissionId, long DoctorId);
        List<ItemPrice> GetItemSyncTeleconsultation(long OrganizationId, long PatientId, long AdmissionId, Guid EncounterId, List<PharmacyPrescription> Prescription, string DeliveryFee, bool IsEditDrug, bool IsEditConsumable, string PayerCoverage, short IsDefaultCoverage);
    }
}
