using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Siloam.Service.EMRPharmacy.Models;
using Siloam.Service.EMRPharmacy.Models.AutoSync;

namespace Siloam.Service.EMRPharmacy.Repositories.IRepositories
{
    public interface IAutoDrugSyncRepository
    {
        string SubmitAppropriatenessReviewCentral(InsertAppropriateness model);
        string SendReadyPickupMySiloam(string JsonString);
        string InsertLogReadyPickup(RequestReadyPickup model, string Status, string JsonRequest, string JsonResponse, Guid AppointmentId, bool IsSuccess);
        List<TeleconsulStock> GetTeleconsulStock(long OrganizationId);
        string InsertQuestion(List<InsertQuestion> model);
        string InsertSkipDrug(InsertSkipDrug model);
        string UpdateTicketSync(long OrganizationId, long PatientId, long AdmissionId, Guid EncounterId, bool IsSuccess);
        TeleconsultationDeliveryHeader GetDeliveryFee(string JsonString);
        string SyncDrugMySiloam(string JsonString);
        List<ItemPriceAuto> GetItemPriceAuto(long OrganizationId, long PatientId, long AdmissionId, Guid EncounterId, bool IsResend);
        string AutoSync(long OrganizationId);
        string InsertCentralPrescription(List<PrescriptionCentral> model, long UserId, string Notes);
        string ResendPrescription(long OrganizationId, long PatientId, long AdmissionId, Guid EncounterId, long UserId);
    }
}
