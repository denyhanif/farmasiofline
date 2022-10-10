using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Siloam.Service.EMRPharmacy.Models;
using Siloam.Service.EMRPharmacy.Models.ViewModels;
using Siloam.Service.EMRPharmacy.Models.Functional;
using Siloam.Service.EMRPharmacy.Models.Parameter;
using Siloam.Service.EMRPharmacy.Models.SyncHope;

namespace Siloam.Service.EMRPharmacy.Repositories.IRepositories
{
    public interface IPharmacyRepository
    {
        PharmacyHistory InsertDataHistory(PharmacyHistory Model);
        PharmacyRecord GetDataByEncounterId(Guid EncounterId);
        PharmacyRecord UpdateRecordData(Guid EncounterId, long Updater, long OrganizationId, long PatientId, long AdmissionId, string Remarks);
        string UpdateRecordTake(long OrganizationId, long PatientId, Guid EncounterId, long AdmissionId, DateTime LastModifiedDate, long Updater);
        string UpdateRecordSubmit(long SubmitBy, long TransAdmId, string TransAdmNo, string QueueNo, string DeliveryFee, string PayerCoverage, PharmacyData model);
        string UpdateRecordSingleQ(Param_RecordSubmit recordParamModel);
        SubmitPrintPrescription submitPrintPrescriptionSQ(Param_RecordSubmit param_ReordModel, Param_SyncResult param_SyncResultModel);
        ResponseResubmitHope resubmititemissue(Param_ResubmitItemIssue param_ItemIssue, ResponseResubmitHope param_ResubmitHope);
        ResponseResubmitHope resubmitadditionalitemissue(Param_ResubmitItemIssue param_ItemIssue, ResponseResubmitHope param_ResubmitHope);
        SubmitPrintPrescription submitPrintPrescription(Param_RecordSubmit param_ReordModel, Param_SyncResult param_SyncResultModel);
        List<PharmacyIssue> GetDataPharmacyIssue(Int64 StoreId, Int64 AdmissionId, Int64 OrganizationId);
        string InsertPharmacyTransaction(Int64 Updater, Int64 StoreId, Int64 TransAdmissionId, List<PharmacyIssue> Model);
        List<PharmacyTransactionHistory> GetPharmacyTransactionHistory(Int64 PrescriptionId, int Mode);
        List<PageTransactionHistory> GetPageTransactionHistory(long PatientId, string OrganizationCode, string PresRegNo, string TransRegNo, string DoctorName, string PayerName, Nullable<DateTime> DateFrom, Nullable<DateTime> DateTo);
        PharmacyPrintPrescription GetPrintPrescription(long OrganizationId, long PatientId, long AdmissionId, string EncounterId, long StoreId);
        PharmacyPrintPrescription GetPrintOriginalPrescription(long OrganizationId, long PatientId, long AdmissionId, string EncounterId, long StoreId);
        string UnverifyPharmacyData(long OrganizationId, long PatientId, long AdmissionId, string EncounterId, string Remarks, long Updater);
        List<PharmacyMedHistory> GetPharmacyMedicationHistory(long PatientId, long AdmissionId);
        List<ViewTransactionHeader> GetDataTransactionByPatientOrganization(long PatientId, long OrganizationId);
        List<PharmacyPrintLabel> GetDataPharmacyPrintLabel(long HeaderId);
        string UpdateDrugInfo(long OrganizationId, long AdmissionId, Guid EncounterId, long Updater, List<PharmacyDrugInfo> model);

        //ADDITIONAL
        SubmitPrintPrescription submitPrintAdditionalPresscription(Param_RecordSubmit param_ReordModel, Param_SyncResult param_SyncResultModel);
        string UpdateRecordAdditional(Param_RecordSubmit param_ReordModel);//(long SubmitBy, long TransAdmId, string TransAdmNo, string QueueNo, PharmacyData model)
        string UpdateRecordAdditionalTake(long OrganizationId, long PatientId, Guid EncounterId, long AdmissionId, DateTime LastModifiedDate, long Updater);
        string UnverifyAdditionalPharmacyData(long OrganizationId, long PatientId, long AdmissionId, string EncounterId, string Remarks, long Updater);
        AdditionalPharmacyRecord GetDataAdditionalByEncounterId(Guid EncounterId);
        AdditionalPharmacyHistory InsertDataAdditionalHistory(AdditionalPharmacyHistory Model);
        AdditionalPharmacyRecord UpdateAdditionalUntake(Guid EncounterId, long Updater, long OrganizationId, long PatientId, long AdmissionId, string Remarks);

        List<OrderItem> GetAdditionalCompound(long OrganizationId, long DoctorId, long AdmissionId, Guid EncounterId, bool IsAdditional);
        List<OrderItem> GetAdditional(long OrganizationId, long AdmissionId, Guid EncounterId, int mode, string SettingValue);
        List<OrderItemConsumables> GetAdditionalConsumables(long OrganizationId, long AdmissionId, Guid EncounterId, int mode);
        List<SyncPrescriptionDB> GetPrescriptionSync(long OrganizationId, long AdmissionId, long DoctorId, Guid EncounterId, long Updater);
        PharmacyPrintPrescription GetPrintAdditionalPrescription(long OrganizationId, long PatientId, long AdmissionId, string EncounterId, long StoreId);
        PharmacyPrintPrescription GetPrintOriginalAdditionalPrescription(long OrganizationId, long PatientId, long AdmissionId, string EncounterId, long StoreId);
        string UpdateAdditionalDrugInfo(long OrganizationId, long AdmissionId, Guid EncounterId, long Updater, List<PharmacyDrugInfo> model);

        string UpdateFlagHOPE(long OrganizationId, long AdmissionId, Guid EncounterId);
        List<CheckPrice> GetCheckPrice(long OrganizationId, long PatientId, long AdmissionId, Guid EncounterId, List<CheckPriceRequest> CheckPriceRequests);
        List<CheckPriceItemIssue> GetCheckPriceItemIssue(long OrganizationId, long PatientId, long AdmissionId, Guid EncounterId, long StoreId, bool isTele, List<CheckPriceItemIssueRequest> CheckPriceItemIssueRequests);
        string CancelItemMySiloam(MySiloamCancelItem model);

        List<ReasonPharmacyModel> GetReasonPharmacy();
    }
}
