using System;
using System.Collections.Generic;
using Siloam.Service.EMRPharmacy.Repositories.IRepositories;
using Siloam.Service.EMRPharmacy.Commons;
using Siloam.Service.EMRPharmacy.Models;
using Siloam.Service.EMRPharmacy.Models.ViewModels;
using Siloam.Service.EMRPharmacy.Models.Functional;
using System.Linq;
using System.Xml.Linq;
using System.Data.SqlClient;
using System.Data;
using Siloam.Service.EMRPharmacy.Models.Parameter;
using Newtonsoft.Json;
using static Siloam.Service.EMRPharmacy.Models.SyncPrescriptionItemIssue;
using Siloam.Service.EMRPharmacy.Models.SyncHope;

namespace Siloam.Service.EMRPharmacy.Repositories
{

    public class PharmacyRepository : DatabaseConfig, IPharmacyRepository
    {

        public PharmacyRepository() : base() { }

        public PharmacyRepository(DatabaseContext context) : base(context) { }

        public static string ConvertHeaderToXML(string EncounterId, Int64 OrganizationId, Int64 Admissionid, Int64 PatientId, Int64 DoctorId, string PharmacyNotes, Boolean IsEditDrug, Boolean IsEditCompound, Boolean IsEditConsumables, bool IsSelfCollection)
        {
            XDocument doc = new XDocument(new XDeclaration("1.0", "UTF-8", "yes"),
            new XElement("root",
                new XElement("row",
                    new XAttribute("EncounterId", EncounterId),
                    new XAttribute("OrganizationId", OrganizationId),
                    new XAttribute("Admissionid", Admissionid),
                    new XAttribute("PatientId", PatientId),
                    new XAttribute("DoctorId", DoctorId),
                    new XAttribute("PharmacyNotes", PharmacyNotes),
                    new XAttribute("IsEditDrug", IsEditDrug),
                    new XAttribute("IsEditCompound", IsEditCompound),
                    new XAttribute("IsEditConsumables", IsEditConsumables),
                    new XAttribute("IsSelfCollection", IsSelfCollection)
                )
            ));
            return doc.ToString();
        }
        public static string ConvertPharmacyHeaderWithPrintToXML(string EncounterId, Int64 OrganizationId, Int64 Admissionid, Int64 PatientId, Int64 DoctorId, string PharmacyNotes, Boolean IsEditDrug, Boolean IsEditCompound, Boolean IsEditConsumables, bool IsSelfCollection,Int64 store_id, string prefix_desc)
        {
            if (prefix_desc is null)
            {
                prefix_desc = "";
            }

            XDocument doc = new XDocument(new XDeclaration("1.0", "UTF-8", "yes"),
            new XElement("root",
                new XElement("row",
                    new XAttribute("EncounterId", EncounterId),
                    new XAttribute("OrganizationId", OrganizationId),
                    new XAttribute("Admissionid", Admissionid),
                    new XAttribute("PatientId", PatientId),
                    new XAttribute("DoctorId", DoctorId),
                    new XAttribute("PharmacyNotes", PharmacyNotes),
                    new XAttribute("IsEditDrug", IsEditDrug),
                    new XAttribute("IsEditCompound", IsEditCompound),
                    new XAttribute("IsEditConsumables", IsEditConsumables),
                    new XAttribute("IsSelfCollection", IsSelfCollection),
                    new XAttribute("store_id", store_id),
                    new XAttribute("prefix_desc", prefix_desc)
                )
            ));
            return doc.ToString();
        }

        public static string ConvertPharmacyHeaderSingleQToXML(string EncounterId, Int64 OrganizationId, Int64 Admissionid, Int64 PatientId, Int64 DoctorId, string PharmacyNotes, Boolean IsEditDrug, Boolean IsEditCompound, Boolean IsEditConsumables, bool IsSelfCollection, Int64 store_id, string prefix_desc, bool is_tele,Nullable<DateTime> VerifyTime)
        {
            if (prefix_desc is null)
            {
                prefix_desc = "";
            }

            XDocument doc = new XDocument(new XDeclaration("1.0", "UTF-8", "yes"),
            new XElement("root",
                new XElement("row",
                    new XAttribute("EncounterId", EncounterId),
                    new XAttribute("OrganizationId", OrganizationId),
                    new XAttribute("Admissionid", Admissionid),
                    new XAttribute("PatientId", PatientId),
                    new XAttribute("DoctorId", DoctorId),
                    new XAttribute("PharmacyNotes", PharmacyNotes),
                    new XAttribute("IsEditDrug", IsEditDrug),
                    new XAttribute("IsEditCompound", IsEditCompound),
                    new XAttribute("IsEditConsumables", IsEditConsumables),
                    new XAttribute("IsSelfCollection", IsSelfCollection),
                    new XAttribute("store_id", store_id),
                    new XAttribute("prefix_desc", prefix_desc),
                    new XAttribute("is_tele", is_tele),
                    new XAttribute("VerifyTime", VerifyTime)

                )
            ));
            return doc.ToString();
        }

        public static string ConvertDrugConsToXML(List<ItemIssueDrugCons> drugCons)
        {
            XDocument doc = new XDocument(new XDeclaration("1.0", "UTF-8", "yes"),
                new XElement("root",
                    from p in drugCons
                    select new XElement("row",
                            new XAttribute("itemId"                     , p.itemId),
                            new XAttribute("IssuedQty"                  , p.IssuedQty),
                            new XAttribute("uomid"                      , p.uomid),
                            new XAttribute("hope_aritem_id"             , p.ARItemId),
                            new XAttribute("pharmacy_prescription_id"   , p.pharmacy_prescription_id),
                            new XAttribute("is_SentHope"                , p.is_SentHope)
                        )
                ));
            return doc.ToString();
        }

        public static string ConvertCompoundsToXML(List<ItemIssueCompounds> compounds)
        {
            XDocument doc = new XDocument(new XDeclaration("1.0", "UTF-8", "yes"),
                new XElement("root",
                    from p in compounds
                    select new XElement("row",
                            new XAttribute("itemId"                             , p.itemId),
                            new XAttribute("IssuedQty"                          , p.IssuedQty),
                            new XAttribute("uomid"                              , p.uomid),
                            new XAttribute("hope_aritem_id"                     , p.ARItemId),
                            new XAttribute("pharmacy_compound_header_id"        , p.pharmacy_compound_header_id),
                            new XAttribute("pharmacy_compound_detail_id"        , p.pharmacy_compound_detail_id),
                            new XAttribute("is_SentHope"                        , p.is_SentHope)

                        )
                ));
            return doc.ToString();
        }
        public static string ConvertAdditionalItemsToXML(List<ItemIssueAdditionalItem> additionalItems)
        {
            XDocument doc = new XDocument(new XDeclaration("1.0", "UTF-8", "yes"),
                new XElement("root",
                    from p in additionalItems
                    select new XElement("row",
                            new XAttribute("item_id", p.item_id),
                            new XAttribute("quantity", p.quantity),
                            new XAttribute("issued_qty", p.issued_qty),
                            new XAttribute("uom_id", p.uom_id),
                            new XAttribute("hope_aritem_id", p.hope_aritem_id),
                            new XAttribute("pharmacy_prescription_id", p.pharmacy_additional_item_id),
                            new XAttribute("item_sequence", p.item_sequence)

                        )
                ));
            return doc.ToString();
        }
        
        public static string ConvertPresscriptionToXML(string EncounterId, 
            Int64 OrganizationId, 
            Int64 Admissionid, 
            Int64 PatientId, 
            Int64 DoctorId, 
            string PharmacyNotes, 
            Boolean IsEditDrug, 
            Boolean IsEditCompound, 
            Boolean IsEditConsumables, 
            bool IsSelfCollection, 
            Int64 store_id, 
            string prefix_desc, 
            bool is_tele,
            bool is_singleQueue,
            bool is_ItemIssue, 
            Nullable<DateTime> VerifyTime,
            Int64 Admissionid_SentHope)
        {
            if (prefix_desc is null)
            {
                prefix_desc = "";
            }

            XDocument doc = new XDocument(new XDeclaration("1.0", "UTF-8", "yes"),
            new XElement("root",
                new XElement("row",
                    new XAttribute("EncounterId", EncounterId),
                    new XAttribute("OrganizationId", OrganizationId),
                    new XAttribute("Admissionid", Admissionid),
                    new XAttribute("PatientId", PatientId),
                    new XAttribute("DoctorId", DoctorId),
                    new XAttribute("PharmacyNotes", PharmacyNotes),
                    new XAttribute("IsEditDrug", IsEditDrug),
                    new XAttribute("IsEditCompound", IsEditCompound),
                    new XAttribute("IsEditConsumables", IsEditConsumables),
                    new XAttribute("IsSelfCollection", IsSelfCollection),
                    new XAttribute("store_id", store_id),
                    new XAttribute("prefix_desc", prefix_desc),
                    new XAttribute("is_tele", is_tele),
                    new XAttribute("is_singleQueue", is_singleQueue),
                    new XAttribute("is_itemIssue", is_ItemIssue),
                    new XAttribute("VerifyTime", VerifyTime),
                    new XAttribute("Admissionid_SentHope", Admissionid_SentHope)

                )
            ));
            return doc.ToString();
        }

        public static string ConverDrugInfoToXML(List<PharmacyDrugInfo> data)
        {
            XDocument doc = new XDocument(new XDeclaration("1.0", "UTF-8", "yes"),
                new XElement("root",
                    from p in data
                    select new XElement("row",
                            new XAttribute("pharmacy_druginfo_id", p.pharmacy_druginfo_id),
                            new XAttribute("pharmacy_mapping_id", p.pharmacy_mapping_id),
                            new XAttribute("pharmacy_mapping_name", p.pharmacy_mapping_name),
                            new XAttribute("value", p.value),
                            new XAttribute("remarks", p.remarks)
                        )
                ));
            return doc.ToString();
        }

        public static string ConvertPrescriptionToXML(List<PharmacyPrescription> data)
        {
            XDocument doc = new XDocument(new XDeclaration("1.0", "UTF-8", "yes"),
                new XElement("root",
                    from p in data
                    select new XElement("row",
                            new XAttribute("prescription_id", p.prescription_id),
                            new XAttribute("prescription_no", p.prescription_no),
                            new XAttribute("item_id", p.item_id),
                            new XAttribute("item_name", p.item_name),
                            new XAttribute("quantity", p.quantity),
                            new XAttribute("uom_id", p.uom_id),
                            new XAttribute("uom_code", p.uom_code),
                            new XAttribute("frequency_id", p.frequency_id),
                            new XAttribute("frequency_code", p.frequency_code),
                            new XAttribute("dosage_id", p.dosage_id),
                            new XAttribute("dose_uom_id", p.dose_uom_id),
                            new XAttribute("dose_uom", p.dose_uom),
                            new XAttribute("dose_text", p.dose_text),
                            new XAttribute("administration_route_id", p.administration_route_id),
                            new XAttribute("administration_route_code", p.administration_route_code),
                            new XAttribute("iteration", p.iteration),
                            new XAttribute("remarks", p.remarks),
                            new XAttribute("is_routine", p.is_routine),
                            new XAttribute("is_consumables", p.is_consumables),
                            new XAttribute("compound_id", p.compound_id),
                            new XAttribute("compound_name", p.compound_name),
                            new XAttribute("origin_prescription_id", p.origin_prescription_id),
                            new XAttribute("hope_aritem_id", p.hope_aritem_id),
                            new XAttribute("prescription_sync_id", p.prescription_sync_id),
                            new XAttribute("item_sequence", p.item_sequence),
                            new XAttribute("IssuedQty", p.IssuedQty),
                            new XAttribute("is_SentHope", p.is_SentHope)
                        )
                ));
            return doc.ToString();
        }

        public static string ConvertAdditionalPrescriptionToXML(List<PharmacyIssue> data)
        {
            XDocument doc = new XDocument(new XDeclaration("1.0", "UTF-8", "yes"),
                new XElement("root",
                    from p in data
                    select new XElement("row",
                            new XAttribute("PresOrganizationId", p.PresOrganizationId),
                            new XAttribute("PresAdmissionId", p.PresAdmissionId),
                            new XAttribute("TransOrganizationId", p.TransOrganizationId),
                            new XAttribute("EmrPrescriptionId", p.EmrPrescriptionId),
                            new XAttribute("PrescriptionId", p.PrescriptionId),
                            new XAttribute("AdditionalId", p.AdditionalId),
                            new XAttribute("StockQuantity", p.StockQuantity),
                            new XAttribute("ItemId", p.ItemId),
                            new XAttribute("ItemCode", p.ItemCode),
                            new XAttribute("ItemName", p.ItemName),
                            new XAttribute("PresQuantity", p.PresQuantity),
                            new XAttribute("Iter", p.Iter),
                            new XAttribute("OutstandingQuantity", p.OutstandingQuantity),
                            new XAttribute("IssuedQuantity", p.IssuedQuantity),
                            new XAttribute("UomId", p.UomId),
                            new XAttribute("UomCode", p.UomCode),
                            new XAttribute("FrequencyId", p.FrequencyId),
                            new XAttribute("FrequencyCode", p.FrequencyCode),
                            new XAttribute("RouteId", p.RouteId),
                            new XAttribute("RouteCode", p.RouteCode),
                            new XAttribute("DosageId", p.DosageId),
                            new XAttribute("dose_uom_id", p.dose_uom_id),
                            new XAttribute("dose_uom", p.dose_uom),
                            new XAttribute("DoseText", p.DoseText),
                            new XAttribute("Remarks", p.Remarks),
                            new XAttribute("IsRoutine", p.IsRoutine),
                            new XAttribute("IsConsumables", p.IsConsumables),
                            new XAttribute("CompoundId", p.CompoundId),
                            new XAttribute("CompoundName", p.CompoundName),
                            new XAttribute("DoctorId", p.DoctorId),
                            new XAttribute("DoctorName", p.DoctorName),
                            new XAttribute("SubTotalPrice", p.SubTotalPrice),
                            new XAttribute("ItemPrice", p.ItemPrice),
                            new XAttribute("Notes", p.Notes),
                            new XAttribute("IsHistory", p.IsHistory),
                            new XAttribute("IsAdditional", p.IsAdditional),
                            new XAttribute("IsNew", p.IsNew),
                            new XAttribute("ConnStatus", p.ConnStatus)
                        )
                ));
            return doc.ToString();
        }

        public static string ConvertCompoundHeaderToXML(List<PharmacyCompoundHeader> data)
        {
            XDocument doc = new XDocument(new XDeclaration("1.0", "UTF-8", "yes"),
                new XElement("root",
                    from p in data
                    select new XElement("row",
                            new XAttribute("prescription_compound_header_id", p.prescription_compound_header_id),
                            new XAttribute("compound_name", p.compound_name),
                            new XAttribute("quantity", p.quantity),
                            new XAttribute("uom_id", p.uom_id),
                            new XAttribute("uom_code", p.uom_code),
                            new XAttribute("administration_frequency_id", p.administration_frequency_id),
                            new XAttribute("frequency_code", p.frequency_code),
                            new XAttribute("dose", p.dose),
                            new XAttribute("dose_uom_id", p.dose_uom_id),
                            new XAttribute("dose_uom", p.dose_uom),
                            new XAttribute("administration_route_id", p.administration_route_id),
                            new XAttribute("administration_route_code", p.administration_route_code),
                            new XAttribute("iter", p.iter),
                            new XAttribute("administration_instruction", p.administration_instruction),
                            new XAttribute("compound_note", p.compound_note),
                            new XAttribute("is_additional", p.is_additional),
                            new XAttribute("item_sequence", p.item_sequence),
                            new XAttribute("compound_header_sync_id", p.compound_header_sync_id),
                            new XAttribute("dose_text", p.dose_text),
                            new XAttribute("IssuedQty", !string.IsNullOrEmpty(p.IssuedQty) ?p.IssuedQty:"0")
                        )
                ));
            return doc.ToString();
        }

        public static string ConvertCompoundDetailToXML(List<PharmacyCompoundDetail> data)
        {
            XDocument doc = new XDocument(new XDeclaration("1.0", "UTF-8", "yes"),
                new XElement("root",
                    from p in data
                    select new XElement("row",
                            new XAttribute("prescription_compound_header_id", p.prescription_compound_header_id),
                            new XAttribute("prescription_compound_detail_id", p.prescription_compound_detail_id),
                            new XAttribute("quantity", p.quantity),
                            new XAttribute("uom_id", p.uom_id),
                            new XAttribute("uom_code", p.uom_code),
                            new XAttribute("item_id", p.item_id),
                            new XAttribute("item_name", p.item_name),
                            new XAttribute("item_sequence", p.item_sequence),
                            new XAttribute("is_additional", p.is_additional),
                            new XAttribute("MainStoreQuantity", p.MainStoreQuantity),
                            new XAttribute("SubStoreQuantity", p.SubStoreQuantity),
                            new XAttribute("compound_detail_sync_id", p.compound_detail_sync_id),
                            new XAttribute("dose_text", p.dose_text),
                            new XAttribute("dosage_id", p.dose),
                            new XAttribute("dose_uom_id", p.dose_uom_id)
                        )
                ));
            return doc.ToString();
        }
        public static string ConvertCompoundDetailHopeToXML(List<PharmacyCompoundDetail> data)
        {
            XDocument doc = new XDocument(new XDeclaration("1.0", "UTF-8", "yes"),
                new XElement("root",
                    from p in data
                    select new XElement("row",
                            new XAttribute("prescription_compound_header_id", p.prescription_compound_header_id),
                            new XAttribute("prescription_compound_detail_id", p.prescription_compound_detail_id),
                            new XAttribute("quantity", p.quantity),
                            new XAttribute("IssuedQty", p.IssuedQty),
                            new XAttribute("uom_id", p.uom_id),
                            new XAttribute("uom_code", p.uom_code),
                            new XAttribute("item_id", p.item_id),
                            new XAttribute("item_name", p.item_name),
                            new XAttribute("item_sequence", p.item_sequence),
                            new XAttribute("is_additional", p.is_additional),
                            new XAttribute("MainStoreQuantity", p.MainStoreQuantity),
                            new XAttribute("SubStoreQuantity", p.SubStoreQuantity),
                            new XAttribute("compound_detail_sync_id", p.compound_detail_sync_id),
                            new XAttribute("dose_text", p.dose_text),
                            new XAttribute("dosage_id", p.dose),
                            new XAttribute("dose_uom_id", p.dose_uom_id),
                            new XAttribute("hope_aritem_id", p.hope_aritem_id),
                            new XAttribute("is_SentHope", p.is_SentHope)
                            
                        )
                ));
            return doc.ToString();
        }

        public static string ConvertRequestCheckPriceToXML(List<CheckPriceRequest> data)
        {
            XDocument doc = new XDocument(new XDeclaration("1.0", "UTF-8", "yes"),
                new XElement("root",
                    from p in data
                    select new XElement("row",
                            new XAttribute("item_id", p.item_id),
                            new XAttribute("quantity", p.quantity),
                            new XAttribute("uom_id", p.uom_id),
                            new XAttribute("is_consumables", p.is_consumables)
                        )
                ));
            return doc.ToString();
        }
        public static string ConvertRequestCheckPriceItemIssueToXML(List<CheckPriceItemIssueRequest> data)
        {
            XDocument doc = new XDocument(new XDeclaration("1.0", "UTF-8", "yes"),
                new XElement("root",
                    from p in data
                    select new XElement("row",
                            new XAttribute("item_id", p.item_id),
                            new XAttribute("issue_quantity", p.issue_quantity),
                            new XAttribute("uom_id", p.uom_id),
                            new XAttribute("is_consumables", p.is_consumables),
                            new XAttribute("is_compound", p.is_compound),
                            new XAttribute("prescription_compound_header_id", p.prescription_compound_header_id),
                            new XAttribute("prescription_compound_name", p.prescription_compound_name)

                        )
                ));
            return doc.ToString();
        }
        public static string ConverSingleQueueToXML(long SubmitBy, PharmacyHeader data, SingleQWorklistData request_SQ, SingleQueue model_SQ)
        {
            if (model_SQ == null) model_SQ = new SingleQueue();
            if (request_SQ == null) request_SQ = new SingleQWorklistData();

            XDocument doc = new XDocument(new XDeclaration("1.0", "UTF-8", "yes"),
            new XElement("root",
                new XElement("row",
                    new XAttribute("hospital_hope_id", data.OrganizationId),
                    new XAttribute("doctor_hope_id", data.DoctorId),
                    new XAttribute("patient_hope_id", data.PatientId),
                    new XAttribute("admission_hope_id", data.Admissionid),
                    new XAttribute("patient_visit_id", request_SQ.patientVisitId),
                    new XAttribute("queue_engine_trx_id", model_SQ.queue_engine_trx_id),
                    new XAttribute("queue_engine_id", model_SQ.queue_engine_id),
                    new XAttribute("is_retail", model_SQ.is_retail),
                    new XAttribute("source_data", "MySiloam"),
                    //new XAttribute("jsonrequest_singleq", JsonConvert.SerializeObject(request_SQ)),
                    //new XAttribute("jsonresponse_singleq", jsonResponse_SQ),
                    new XAttribute("submitby", SubmitBy)
                )
            ));
            return doc.ToString();
        }

        public static string ConvertEditReasonToXML(List<PharmacyEditReason> listreason)
        {
            XDocument doc = new XDocument(new XDeclaration("1.0", "UTF-8", "yes"),
                new XElement("root",
                    from p in listreason
                    select new XElement("row",
                            new XAttribute("EditReasonId", p.EditReasonId),
                            new XAttribute("RecordId", p.RecordId),
                            new XAttribute("IsAdditional", p.IsAdditional),
                            new XAttribute("ReasonId", p.ReasonId),
                            new XAttribute("ReasonRemarks", p.ReasonRemarks),
                            new XAttribute("CallDoctor", p.CallDoctor),
                            new XAttribute("IsActive", p.IsActive),
                            new XAttribute("CallDoctorDate", p.CallDoctorDate),
                            new XAttribute("CallDoctorTime", p.CallDoctorTime),
                            new XAttribute("CreatedBy", p.CreatedBy),
                            new XAttribute("CreatedDate", p.CreatedDate),
                            new XAttribute("ModifiedBy", p.ModifiedBy),
                            new XAttribute("ModifiedDate", p.ModifiedDate)
                        )
                ));
            return doc.ToString();
        }

        public static string ConvertAppropriatnessReviewToXML(PharmacyAppropriatnessReview model)
        {
            if(model != null)
            {
                XDocument doc = new XDocument(new XDeclaration("1.0", "UTF-8", "yes"),
                new XElement("root",
                    new XElement("row",
                        new XAttribute("consume_drug", model.consume_drug),
                        new XAttribute("diagnose", model.diagnose),
                        new XAttribute("complete_recipe", model.complete_recipe),
                        new XAttribute("correct_drug_name", model.correct_drug_name),
                        new XAttribute("correct_drug_dossage", model.correct_drug_dossage),
                        new XAttribute("correct_drug_consume", model.correct_drug_consume),
                        new XAttribute("correct_drug_freq", model.correct_drug_freq),
                        new XAttribute("duplicate_drug", model.duplicate_drug),
                        new XAttribute("interaction_drug", model.interaction_drug),
                        new XAttribute("interaction_food", model.interaction_food),
                        new XAttribute("side_effect", model.side_effect),
                        new XAttribute("kontraindikasi", model.kontraindikasi),
                        new XAttribute("allergy", model.allergy),
                        new XAttribute("weight_body", model.weight_body),
                        new XAttribute("is_pregnant", model.is_pregnant),
                        new XAttribute("is_breastfeed", model.is_breastfeed),
                        new XAttribute("consume_drug_check", model.consume_drug_check),
                        new XAttribute("diagnose_check", model.diagnose_check),
                        new XAttribute("duplicate_drug_check", model.duplicate_drug_check),
                        new XAttribute("interaction_drug_check", model.interaction_drug_check),
                        new XAttribute("interaction_food_check", model.interaction_food_check),
                        new XAttribute("allergy_check", model.allergy_check),
                        new XAttribute("weight_body_check", model.weight_body_check),
                        new XAttribute("side_effect_text", model.side_effect_text),
                        new XAttribute("kontraindikasi_text", model.kontraindikasi_text),
                        new XAttribute("pregnant_week", model.pregnant_week),
                        new XAttribute("consume_drug_text", model.consume_drug_text),
                        new XAttribute("duplicate_drug_text", model.duplicate_drug_text),
                        new XAttribute("interaction_drug_text", model.interaction_drug_text),
                        new XAttribute("allergy_text", model.allergy_text)
                    )
                ));

                return doc.ToString();
            }
            else
            {
                return "";
            }
        }

        public static string ConvertAdditionalItemToXML(List<PharmacyAdditionalItem> listadditional)
        {
            XDocument doc = new XDocument(new XDeclaration("1.0", "UTF-8", "yes"),
                new XElement("root",
                    from p in listadditional
                    select new XElement("row",
                            new XAttribute("item_id", p.item_id),
                            new XAttribute("quantity", p.quantity),
                            new XAttribute("issued_qty", p.issued_qty),
                            new XAttribute("uom_id", p.uom_id),
                            new XAttribute("hope_aritem_id", p.hope_aritem_id),
                            new XAttribute("item_sequence", p.item_sequence)
                        )
                ));
            return doc.ToString();
        }

        public static string ConvertEditedMappingToXML(List<PharmacyEditedMapping> listeditedMapping)
        {
            if(listeditedMapping!= null)
            {
                if (listeditedMapping.Count > 0)
                {
                    XDocument doc = new XDocument(new XDeclaration("1.0", "UTF-8", "yes"),
                       new XElement("root",
                           from p in listeditedMapping
                           select new XElement("row",
                                   new XAttribute("pharmacy_type", p.pharmacy_type),
                                   new XAttribute("origin_pharmacy_id", p.origin_pharmacy_id),
                                   new XAttribute("edited_pharmacy_id", p.edited_pharmacy_id),
                                   new XAttribute("edit_action", p.edit_action),
                                   new XAttribute("edit_reason", p.edit_reason)
                               )
                       ));
                    return doc.ToString();
                }
            }

            return "";

        }

        public PharmacyHistory InsertDataHistory(PharmacyHistory Model)
        {
            Context.Add(Model);
            Context.SaveChanges();
            return Model;
        }

        public PharmacyItemIssue insertPharmacyItemIssue(PharmacyItemIssue Model)
        {
            Context.ItemIssueSet.Add(Model);
            Context.SaveChanges();
            return Model;
        }

        public AdditionalPharmacyHistory InsertDataAdditionalHistory(AdditionalPharmacyHistory Model)
        {
            Context.Add(Model);
            Context.SaveChanges();
            return Model;
        }

        public List<Prescription> DeletePrescription(Int64 AdmissionId)
        {
            List<Prescription> data = new List<Prescription>();
            using (var context = new DatabaseContext(ContextOption))
            {

                data = (from pres in context.PrescriptionSet
                        where pres.hope_admission_id == AdmissionId
                        select pres).ToList();
            }
            foreach (var x in data)
            {
                Context.Remove(x);
            }
            Context.SaveChanges();
            return data;
        }

        public PharmacyRecord GetDataByEncounterId(Guid EncounterId)
        {
            PharmacyRecord data;
            using (var context = new DatabaseContext(ContextOption))
            {

                data = (from record in context.RecordSet
                        where record.encounter_id == EncounterId && record.is_active == true
                        select record).Single();
            }
            return data;
        }

        public PharmacyRecord GetDataByOrgIdEncId(long OrganizationId, long PatientId, long AdmissionId, Guid EncounterId)
        {
            PharmacyRecord data;
            using (var context = new DatabaseContext(ContextOption))
            {

                data = (from record in context.RecordSet
                        where record.organization_id == OrganizationId && record.patient_id == PatientId && record.admission_id == AdmissionId && record.encounter_id == EncounterId && record.is_active == true
                        select record).Single();
            }
            return data;
        }

        public AdditionalPharmacyRecord GetDataAdditionalByEncounterId(Guid EncounterId)
        {
            AdditionalPharmacyRecord data;
            using (var context = new DatabaseContext(ContextOption))
            {

                data = (from record in context.AdditionalRecordSet
                        where record.encounter_id == EncounterId && record.is_active == true
                        select record).Single();
            }
            return data;
        }

        public AdditionalPharmacyRecord GetDataAdditionalByOrgIdEncId(long OrganizationId, long PatientId, long AdmissionId, Guid EncounterId)
        {
            AdditionalPharmacyRecord data;
            using (var context = new DatabaseContext(ContextOption))
            {

                data = (from record in context.AdditionalRecordSet
                        where record.organization_id == OrganizationId && record.patient_id == PatientId && record.admission_id == AdmissionId && record.encounter_id == EncounterId && record.is_active == true
                        select record).Single();
            }
            return data;
        }

        public Nullable<DateTime> GetTakeDateByEncounterId(Guid EncounterId)
        {
            Nullable<DateTime> data;
            using (var context = new DatabaseContext(ContextOption))
            {

                data = (from record in context.RecordSet
                        where record.encounter_id == EncounterId && record.is_active == true
                        select record.take_date).Single();
            }
            return data;
        }

        public Nullable<Int64> GetTakeByEncounterId(Guid EncounterId)
        {
            Nullable<Int64> data;
            using (var context = new DatabaseContext(ContextOption))
            {

                data = (from record in context.RecordSet
                        where record.encounter_id == EncounterId && record.is_active == true
                        select record.take_by).Single();
            }
            return data;
        }

        public Nullable<Int64> GetAdditionalTakeByEncounterId(Guid EncounterId)
        {
            Nullable<Int64> data;
            using (var context = new DatabaseContext(ContextOption))
            {

                data = (from record in context.AdditionalRecordSet
                        where record.encounter_id == EncounterId && record.is_active == true
                        select record.take_by).Single();
            }
            return data;
        }

        public Nullable<DateTime> GetVerifyDate(Guid EncounterId)
        {
            Nullable<DateTime> data;
            using (var context = new DatabaseContext(ContextOption))
            {
                data = (from record in context.RecordSet
                        where record.encounter_id == EncounterId && record.is_active == true
                        select record.submit_date).Single();
            }
            return data;
        }

        public Nullable<DateTime> GetPresscriptionVerifyDate(Int64 organizationId, Int64 patientId, Int64 admissionId, Guid EncounterId)
        {
            Nullable<DateTime> data;
            using (var context = new DatabaseContext(ContextOption))
            {
                data = (from record in context.RecordSet
                        where record.is_active == true
                        && record.organization_id == organizationId
                        && record.patient_id == patientId
                        && record.admission_id == admissionId
                        && record.encounter_id == EncounterId 
                        select record.submit_date).Single();
            }
            return data;
        }

        public Nullable<DateTime> GetAdditionalVerifyDate(Guid EncounterId)
        {
            Nullable<DateTime> data;
            using (var context = new DatabaseContext(ContextOption))
            {
                data = (from record in context.AdditionalRecordSet
                        where record.encounter_id == EncounterId && record.is_active == true
                        select record.submit_date).Single();
            }
            return data;
        }

        public int GetCountTransaction(Guid EncounterId)
        {
            int data;
            using (var context = new DatabaseContext(ContextOption))
            {
                data = (from record in context.RecordSet
                        join trans in context.TransHeaderSet
                        on record.admission_id equals trans.prescription_admission_id
                        where record.encounter_id == EncounterId && record.is_active == true && trans.is_active == true
                        select trans.pharmacy_transaction_header_id).Count();
            }
            return data;
        }

        public int CountSupervisor (long UserId, long OrganizationId)
        {
            int data = 0;
            using (var context = new DatabaseContext(ContextOption))
            {
                data = (from userrole in context.UserRoleSet
                        where userrole.hope_user_id == UserId && userrole.hope_organization_id == OrganizationId && userrole.application_id == Guid.Parse("CA43CDEC-30B8-489F-903E-E7F657132AB9") && userrole.role_id == Guid.Parse("332A1C18-A715-4222-8BF9-5A714B921CA2")
                        select userrole).Count();
            }
            return data;
        }

        public PharmacyRecord UpdateRecordData(Guid EncounterId, long Updater, long OrganizationId, long PatientId, long AdmissionId, string Remarks)
        {
            PharmacyRecord data;
            data = GetDataByOrgIdEncId(OrganizationId, PatientId, AdmissionId, EncounterId);
            if (data == null)
            {
                return null;
            }
            else
            {
                
                data.take_date = null;
                data.take_by = null;
                data.IsEditDrug = false;
                data.IsEditCompound = false;
                data.IsEditConsumables = false;
                data.modified_by = Updater.ToString();
                data.modified_date = DateTime.Now;
                Context.Update(data);
                Context.SaveChanges();

                PharmacyHistory history = new PharmacyHistory();
                history.history_id = 0;
                history.record_id = data.record_id;
                history.action = "UNTAKE";
                history.remarks = Remarks;
                history.created_date = DateTime.Now;
                history.created_by = Updater.ToString();
                var addhistory = InsertDataHistory(history);
            }
            return data;
        }

        public AdditionalPharmacyRecord UpdateAdditionalUntake(Guid EncounterId, long Updater, long OrganizationId, long PatientId, long AdmissionId, string Remarks)
        {
            AdditionalPharmacyRecord data;
            data = GetDataAdditionalByOrgIdEncId(OrganizationId, PatientId, AdmissionId, EncounterId);
            if (data == null)
            {
                return null;
            }
            else
            {

                data.take_date = null;
                data.take_by = null;
                data.IsEditDrug = false;
                data.IsEditCompound = false;
                data.IsEditConsumables = false;
                data.modified_by = Updater.ToString();
                data.modified_date = DateTime.Now;
                Context.Update(data);
                Context.SaveChanges();

                AdditionalPharmacyHistory history = new AdditionalPharmacyHistory();
                history.additional_history_id = 0;
                history.additional_record_id = data.additional_record_id;
                history.action = "UNTAKE";
                history.remarks = Remarks;
                history.created_date = DateTime.Now;
                history.created_by = Updater.ToString();
                var addhistory = InsertDataAdditionalHistory(history);
            }
            return data;
        }
        public string UpdateTakeOver(long UserId, long OrganizationId, long PatientId, Guid EncounterId, long AdmissionId)
        {
            DataTable dtTO = new DataTable();
            string dataTO;
            try
            {
                using (SqlConnection conn = new SqlConnection(Siloam.System.ApplicationSetting.ConnectionString))
                {
                    conn.Open();
                    SqlCommand cmd = conn.CreateCommand();
                    cmd.CommandText = "spSubmitTakeOverPrescription";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("UserId", UserId));
                    cmd.Parameters.Add(new SqlParameter("OrganizationId", OrganizationId));
                    cmd.Parameters.Add(new SqlParameter("PatientId", PatientId));
                    cmd.Parameters.Add(new SqlParameter("EncounterId", EncounterId));
                    cmd.Parameters.Add(new SqlParameter("AdmissionId", AdmissionId));
                    using (var da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dtTO);
                    }
                    dataTO = (from DataRow dr in dtTO.Rows
                            select dr["Result"].ToString()).Single();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dataTO;
        }
        public string UpdateRecordTake(long OrganizationId, long PatientId, Guid EncounterId, long AdmissionId, DateTime LastModifiedDate, long Updater)
        {
            DataTable dt = new DataTable();
            string data;
            try
            {
                using (SqlConnection conn = new SqlConnection(Siloam.System.ApplicationSetting.ConnectionString))
                {
                    conn.Open();
                    SqlCommand cmd = conn.CreateCommand();
                    cmd.CommandText = "spSUBMIT_TAKEPRESCRIPTION";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("OrganizationId", OrganizationId));
                    cmd.Parameters.Add(new SqlParameter("PatientId", PatientId));
                    cmd.Parameters.Add(new SqlParameter("EncounterId", EncounterId));
                    cmd.Parameters.Add(new SqlParameter("AdmissionId", AdmissionId));
                    cmd.Parameters.Add(new SqlParameter("LastModifiedDate", LastModifiedDate));
                    cmd.Parameters.Add(new SqlParameter("Updater", Updater));
                    using (var da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }
                    data = (from DataRow dr in dt.Rows
                            select dr["Result"].ToString()).Single();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return data;
        }

        public string UpdateRecordAdditionalTake(long OrganizationId, long PatientId, Guid EncounterId, long AdmissionId, DateTime LastModifiedDate, long Updater)
        {
            DataTable dt = new DataTable();
            string data;
            try
            {
                using (SqlConnection conn = new SqlConnection(Siloam.System.ApplicationSetting.ConnectionString))
                {
                    conn.Open();
                    SqlCommand cmd = conn.CreateCommand();
                    cmd.CommandText = "spSUBMIT_TAKEADDITIONALPRESCRIPTION";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("OrganizationId", OrganizationId));
                    cmd.Parameters.Add(new SqlParameter("PatientId", PatientId));
                    cmd.Parameters.Add(new SqlParameter("EncounterId", EncounterId));
                    cmd.Parameters.Add(new SqlParameter("AdmissionId", AdmissionId));
                    cmd.Parameters.Add(new SqlParameter("LastModifiedDate", LastModifiedDate));
                    cmd.Parameters.Add(new SqlParameter("Updater", Updater));
                    using (var da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }
                    data = (from DataRow dr in dt.Rows
                            select dr["Result"].ToString()).Single();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return data;
        }
        public string UpdateRecordSingleQ(Param_RecordSubmit param_ReordModel)
        {
            string data;
            try
            {
                PharmacyData PharmacyDataModel  = param_ReordModel.PharmacyDataModel;
                SingleQueue SingleQDataModel    = param_ReordModel.SingleQueueDataModel;
                DataTable dt                    = new DataTable();
                long SubmitBy                   = param_ReordModel.SubmitBy;
                long TransAdmId                 = param_ReordModel.TransAdmId;
                string TransAdmNo               = param_ReordModel.TransAdmNo;
                string QueueNo                  = param_ReordModel.QueueNo;
                if (QueueNo is null)
                {
                    QueueNo = "";
                }
                string DeliveryFee              = param_ReordModel.DeliveryFee;
                string PayerCoverage            = param_ReordModel.PayerCoverage;

                
                string xmlheader                    = ConvertPharmacyHeaderSingleQToXML(
                                                        PharmacyDataModel.header.EncounterId.ToString(), 
                                                        PharmacyDataModel.header.OrganizationId, 
                                                        PharmacyDataModel.header.Admissionid, 
                                                        PharmacyDataModel.header.PatientId, 
                                                        PharmacyDataModel.header.DoctorId, 
                                                        PharmacyDataModel.header.PharmacyNotes, 
                                                        PharmacyDataModel.header.IsEditDrug, 
                                                        PharmacyDataModel.header.IsEditCompound, 
                                                        PharmacyDataModel.header.IsEditConsumables, 
                                                        PharmacyDataModel.header.IsSelfCollection,
                                                        PharmacyDataModel.header.store_id, 
                                                        PharmacyDataModel.header.prefix_desc, 
                                                        PharmacyDataModel.header.is_tele,
                                                        PharmacyDataModel.header.VerifyTime);
                string xmlprescription              = ConvertPrescriptionToXML(PharmacyDataModel.prescription);
                string xmldruginfo                  = ConverDrugInfoToXML(PharmacyDataModel.druginfo);
                string xmlcompoundheader            = ConvertCompoundHeaderToXML(PharmacyDataModel.compound_header);
                string xmlcompounddetail            = ConvertCompoundDetailToXML(PharmacyDataModel.compound_detail);
                string xmlsingleQ                   = ConverSingleQueueToXML(SubmitBy,PharmacyDataModel.header, PharmacyDataModel.singleQWorklistData, SingleQDataModel);
                string RequestWorklistSingleQueue   = JsonConvert.SerializeObject(PharmacyDataModel.singleQWorklistData);

                using (SqlConnection conn = new SqlConnection(Siloam.System.ApplicationSetting.ConnectionString))
                {
                    conn.Open();
                    SqlCommand cmd = conn.CreateCommand();
                    cmd.CommandText = "spSUBMIT_PRESCRIPTIONSQ";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("Header"                , xmlheader));
                    cmd.Parameters.Add(new SqlParameter("Prescription"          , xmlprescription));
                    cmd.Parameters.Add(new SqlParameter("CompoundHeader"        , xmlcompoundheader));
                    cmd.Parameters.Add(new SqlParameter("CompoundDetail"        , xmlcompounddetail));
                    cmd.Parameters.Add(new SqlParameter("DrugInfo"              , xmldruginfo));
                    cmd.Parameters.Add(new SqlParameter("SingleQ"               , xmlsingleQ));
                    cmd.Parameters.Add(new SqlParameter("Jsonrequest_singleQ"   , RequestWorklistSingleQueue));
                    cmd.Parameters.Add(new SqlParameter("Jsonresponse_singleQ"  , param_ReordModel.ResponseWorklistSingleQueue));
                    cmd.Parameters.Add(new SqlParameter("TransAdmId"            , TransAdmId));
                    cmd.Parameters.Add(new SqlParameter("TransAdmNo"            , TransAdmNo));
                    cmd.Parameters.Add(new SqlParameter("QueueNo"               , QueueNo));
                    cmd.Parameters.Add(new SqlParameter("DeliveryFee"           , DeliveryFee));
                    cmd.Parameters.Add(new SqlParameter("Updater"               , SubmitBy));
                    cmd.Parameters.Add(new SqlParameter("PayerCoverage"         , PayerCoverage));
                    using (var da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }
                    data = (from DataRow dr in dt.Rows
                            select dr["Result"].ToString()).Single();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return data;
        }
        public SubmitPrintPrescription submitPrintPrescriptionSQ(Param_RecordSubmit param_ReordModel,Param_SyncResult param_SyncResultModel)
        {
            DataSet dt = new DataSet();
            SubmitPrintPrescription data = new SubmitPrintPrescription();
            try
            {
                PharmacyData PharmacyDataModel  = param_ReordModel.PharmacyDataModel;
                SingleQueue SingleQDataModel    = param_ReordModel.SingleQueueDataModel;
                long SubmitBy                   = param_ReordModel.SubmitBy;
                long TransAdmId                 = param_ReordModel.TransAdmId;
                string TransAdmNo               = param_ReordModel.TransAdmNo;
                string QueueNo                  = param_ReordModel.QueueNo;
                if (QueueNo is null)
                {
                    QueueNo = "";
                }
                string DeliveryFee              = param_ReordModel.DeliveryFee;
                string PayerCoverage            = param_ReordModel.PayerCoverage;


                string xmlheader                    = ConvertPharmacyHeaderSingleQToXML(
                                                        PharmacyDataModel.header.EncounterId.ToString(),
                                                        PharmacyDataModel.header.OrganizationId,
                                                        PharmacyDataModel.header.Admissionid,
                                                        PharmacyDataModel.header.PatientId,
                                                        PharmacyDataModel.header.DoctorId,
                                                        PharmacyDataModel.header.PharmacyNotes,
                                                        PharmacyDataModel.header.IsEditDrug,
                                                        PharmacyDataModel.header.IsEditCompound,
                                                        PharmacyDataModel.header.IsEditConsumables,
                                                        PharmacyDataModel.header.IsSelfCollection,
                                                        PharmacyDataModel.header.store_id,
                                                        PharmacyDataModel.header.prefix_desc,
                                                        PharmacyDataModel.header.is_tele,
                                                        PharmacyDataModel.header.VerifyTime);
                string xmlprescription              = ConvertPrescriptionToXML(PharmacyDataModel.prescription);
                string xmldruginfo                  = ConverDrugInfoToXML(PharmacyDataModel.druginfo);
                string xmlcompoundheader            = ConvertCompoundHeaderToXML(PharmacyDataModel.compound_header);
                string xmlcompounddetail            = ConvertCompoundDetailToXML(PharmacyDataModel.compound_detail);
                string xmlsingleQ                   = ConverSingleQueueToXML(SubmitBy, PharmacyDataModel.header, PharmacyDataModel.singleQWorklistData, SingleQDataModel);
                string RequestWorklistSingleQueue   = JsonConvert.SerializeObject(PharmacyDataModel.singleQWorklistData);
                string xmleditreason                = ConvertEditReasonToXML(PharmacyDataModel.editReason);

                using (SqlConnection conn           = new SqlConnection(Siloam.System.ApplicationSetting.ConnectionString))
                {
                    conn.Open();
                    SqlCommand cmd = conn.CreateCommand();
                    cmd.CommandText = "spSUBMIT_PRESCRIPTION_GET_PRINT";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("Header", xmlheader));
                    cmd.Parameters.Add(new SqlParameter("Prescription", xmlprescription));
                    cmd.Parameters.Add(new SqlParameter("CompoundHeader", xmlcompoundheader));
                    cmd.Parameters.Add(new SqlParameter("CompoundDetail", xmlcompounddetail));
                    cmd.Parameters.Add(new SqlParameter("DrugInfo", xmldruginfo));
                    cmd.Parameters.Add(new SqlParameter("SingleQ", xmlsingleQ));
                    cmd.Parameters.Add(new SqlParameter("EditReason", xmleditreason));
                    cmd.Parameters.Add(new SqlParameter("Jsonrequest_singleQ", RequestWorklistSingleQueue));
                    cmd.Parameters.Add(new SqlParameter("Jsonresponse_singleQ", param_ReordModel.ResponseWorklistSingleQueue));
                    cmd.Parameters.Add(new SqlParameter("TransAdmId", TransAdmId));
                    cmd.Parameters.Add(new SqlParameter("TransAdmNo", TransAdmNo));
                    cmd.Parameters.Add(new SqlParameter("QueueNo", QueueNo));
                    cmd.Parameters.Add(new SqlParameter("DeliveryFee", DeliveryFee));
                    cmd.Parameters.Add(new SqlParameter("Updater", SubmitBy));
                    cmd.Parameters.Add(new SqlParameter("PayerCoverage", PayerCoverage));
                    using (var da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }
                    data.SubmitStatusPressResult = (from DataRow dr in dt.Tables[0].Rows
                                              select dr["Result"].ToString()).Single();

                    PharmacyPrintHeader h = new PharmacyPrintHeader();
                    h = (from DataRow dr in dt.Tables[1].Rows
                         select new PharmacyPrintHeader
                         {
                             EncounterId                = Guid.Parse(dr["EncounterId"].ToString()),
                             OrganizationId             = long.Parse(dr["OrganizationId"].ToString()),
                             PatientId                  = long.Parse(dr["PatientId"].ToString()),
                             AdmissionId                = long.Parse(dr["AdmissionId"].ToString()),
                             AdmissionNo                = dr["AdmissionNo"].ToString(),
                             LocalMrNo                  = dr["LocalMrNo"].ToString(),
                             PatientName                = dr["PatientName"].ToString(),
                             BirthDate                  = DateTime.Parse(dr["BirthDate"].ToString()).ToString("dd MMM yyyy"),
                             Age                        = dr["Age"].ToString(),
                             Gender                     = dr["Gender"].ToString(),
                             DoctorName                 = dr["DoctorName"].ToString(),
                             SpecialtyName              = dr["SpecialtyName"].ToString(),
                             PrescriptionDate           = DateTime.Parse(dr["PrescriptionDate"].ToString()).ToString("dd MMM yyyy HH:mm"),
                             PrescriptionNo             = dr["PrescriptionNo"].ToString(),
                             PayerName                  = dr["PayerName"].ToString(),
                             QueueNo                    = dr["QueueNo"].ToString(),
                             IsCOVID                    = bool.Parse(dr["IsCOVID"].ToString()),
                             SipNo                      = dr["SipNo"].ToString(),
                             PrintNumber                = dr["PrintNumber"].ToString()
                         }).Single();

                    List<PharmacyPrintPres> p = new List<PharmacyPrintPres>();
                    p = (from DataRow dr in dt.Tables[2].Rows
                         select new PharmacyPrintPres
                         {
                             prescription_id            = long.Parse(dr["prescription_id"].ToString()),
                             item_id                    = long.Parse(dr["item_id"].ToString()),
                             item_name                  = dr["item_name"].ToString(),
                             quantity                   = dr["quantity"].ToString(),
                             uom_id                     = long.Parse(dr["uom_id"].ToString()),
                             uom_code                   = dr["uom_code"].ToString(),
                             frequency_id               = long.Parse(dr["frequency_id"].ToString()),
                             frequency_code             = dr["frequency_code"].ToString(),
                             dosage_id                  = dr["dosage_id"].ToString(),
                             dose_uom_id                = long.Parse(dr["dose_uom_id"].ToString()),
                             dose_uom                   = dr["dose_uom"].ToString(),
                             dose_text                  = dr["dose_text"].ToString(),
                             administration_route_id    = long.Parse(dr["administration_route_id"].ToString()),
                             administration_route_code  = dr["administration_route_code"].ToString(),
                             remarks                    = dr["remarks"].ToString(),
                             iteration                  = int.Parse(dr["iteration"].ToString()),
                             is_consumables             = bool.Parse(dr["is_consumables"].ToString()),
                             is_routine                 = bool.Parse(dr["is_routine"].ToString()),
                             compound_id                = Guid.Parse(dr["compound_id"].ToString()),
                             compound_name              = dr["compound_name"].ToString(),
                             origin_prescription_id     = long.Parse(dr["origin_prescription_id"].ToString()),
                             RackName                   = dr["RackName"].ToString(),
                             PrescriptionDate           = DateTime.Parse(dr["PrescriptionDate"].ToString()).ToString("dd-MMM-yyyy"),
                             IsDoseText                 = bool.Parse(dr["IsDoseText"].ToString()),
                             IssuedQty                  = dr["IssuedQty"].ToString().Replace(",", ".")
                         }).ToList();

                    List<PharmacyPrintAllergy> a = new List<PharmacyPrintAllergy>();
                    a = (from DataRow dr in dt.Tables[3].Rows
                         select new PharmacyPrintAllergy
                         {
                             allergy                    = dr["allergy"].ToString(),
                             reaction                   = dr["reaction"].ToString()
                         }).ToList();

                    List<PharmacyPrintObjective> o = new List<PharmacyPrintObjective>();
                    o = (from DataRow dr in dt.Tables[4].Rows
                         select new PharmacyPrintObjective
                         {
                             soap_mapping_id            = Guid.Parse(dr["soap_mapping_id"].ToString()),
                             soap_mapping_name          = dr["soap_mapping_name"].ToString(),
                             value                      = dr["value"].ToString()
                         }).ToList();

                    List<PharmacyPrintCompoundHeader> ch = new List<PharmacyPrintCompoundHeader>();
                    ch = (from DataRow dr in dt.Tables[5].Rows
                          select new PharmacyPrintCompoundHeader
                          {
                              prescription_compound_header_id   = Guid.Parse(dr["prescription_compound_header_id"].ToString()),
                              compound_name                     = dr["compound_name"].ToString(),
                              quantity                          = dr["quantity"].ToString().Replace(",", "."),
                              uom_id                            = long.Parse(dr["uom_id"].ToString()),
                              uom_code                          = dr["uom_code"].ToString(),
                              administration_frequency_id       = long.Parse(dr["administration_frequency_id"].ToString()),
                              frequency_code                    = dr["frequency_code"].ToString(),
                              dose                              = dr["dose"].ToString().Replace(",", "."),
                              dose_uom_id                       = long.Parse(dr["dose_uom_id"].ToString()),
                              dose_uom                          = dr["dose_uom"].ToString(),
                              administration_route_id           = long.Parse(dr["administration_route_id"].ToString()),
                              administration_route_code         = dr["administration_route_code"].ToString(),
                              administration_instruction        = dr["administration_instruction"].ToString(),
                              iter                              = int.Parse(dr["iter"].ToString()),
                              item_sequence                     = short.Parse(dr["item_sequence"].ToString()),
                              PrescriptionDate                  = DateTime.Parse(dr["PrescriptionDate"].ToString()).ToString("dd-MMM-yyyy"),
                              dose_text                         = dr["dose_text"].ToString(),
                              IsDoseText                        = bool.Parse(dr["IsDoseText"].ToString()),
                              compound_note                     = dr["compound_note"].ToString(),
                              IssuedQty                         = dr["IssuedQty"].ToString().Replace(",", ".")
                          }).ToList();

                    List<PharmacyPrintCompoundDetail> cd = new List<PharmacyPrintCompoundDetail>();
                    cd = (from DataRow dr in dt.Tables[6].Rows
                          select new PharmacyPrintCompoundDetail
                          {
                              prescription_compound_header_id   = Guid.Parse(dr["prescription_compound_header_id"].ToString()),
                              prescription_compound_detail_id   = Guid.Parse(dr["prescription_compound_detail_id"].ToString()),
                              quantity                          = dr["quantity"].ToString().Replace(",", "."),
                              uom_id                            = long.Parse(dr["uom_id"].ToString()),
                              uom_code                          = dr["uom_code"].ToString(),
                              item_id                           = long.Parse(dr["item_id"].ToString()),
                              item_name                         = dr["item_name"].ToString(),
                              item_sequence                     = short.Parse(dr["item_sequence"].ToString()),
                              RackName                          = dr["RackName"].ToString(),
                              dose_uom_id                       = long.Parse(dr["dose_uom_id"].ToString()),
                              dose                              = dr["dose"].ToString().Replace(",", "."),
                              dose_text                         = dr["dose_text"].ToString(),
                              IsDoseText                        = bool.Parse(dr["IsDoseText"].ToString()),
                              dose_uom_code                     = dr["dose_uom_code"].ToString()
                          }).ToList();

                    data.HopeStatusResult       = param_SyncResultModel.HopeStatusResult;
                    data.HopeMessageResult      = param_SyncResultModel.HopeMessageResult;
                    data.SingleQStatusResult    = param_SyncResultModel.SingleQStatusResult;
                    data.SingleQMessageResult   = param_SyncResultModel.SingleQMessageResult;
                    data.printHeader            = h;
                    data.printPres              = p;
                    data.printAllergy           = a;
                    data.printObjective         = o;
                    data.printCompoundHeader    = ch;
                    data.printCompoundDetail    = cd;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return data;
        }
        public ResponseResubmitHope resubmititemissue(Param_ResubmitItemIssue param_ItemIssue, ResponseResubmitHope param_ResubmitHope)
        {
            DataSet dt = new DataSet();
            ResponseResubmitHope data = new ResponseResubmitHope();
            try
            {
                string xmlDrugCons = ConvertDrugConsToXML(param_ItemIssue.drugCons);
                string xmlCompounds = ConvertCompoundsToXML(param_ItemIssue.compounds);
                string xmlAdditionalItems = ConvertAdditionalItemsToXML(param_ItemIssue.additionalItems);
                string xmlappropriatnessreview = ConvertAppropriatnessReviewToXML(param_ItemIssue.appropriatnessReview);

                using (SqlConnection conn = new SqlConnection(Siloam.System.ApplicationSetting.ConnectionString))
                {
                    conn.Open();
                    SqlCommand cmd = conn.CreateCommand();
                    cmd.CommandText = "spResubmit_ItemIssue";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("OrganizationId", param_ItemIssue.OrganizationId));
                    cmd.Parameters.Add(new SqlParameter("PatientId", param_ItemIssue.PatientId));
                    cmd.Parameters.Add(new SqlParameter("DoctorId", param_ItemIssue.DoctorId));
                    cmd.Parameters.Add(new SqlParameter("store_id", param_ItemIssue.store_id));
                    cmd.Parameters.Add(new SqlParameter("Admissionid", param_ItemIssue.Admissionid));
                    cmd.Parameters.Add(new SqlParameter("EncounterId", param_ItemIssue.EncounterId));
                    cmd.Parameters.Add(new SqlParameter("SubmitBy", param_ItemIssue.SubmitBy));
                    cmd.Parameters.Add(new SqlParameter("ItemIssueDrugCons", xmlDrugCons));
                    cmd.Parameters.Add(new SqlParameter("ItemIssueCompounds", xmlCompounds));
                    cmd.Parameters.Add(new SqlParameter("ItemIssueAdditionalItem", xmlAdditionalItems));
                    cmd.Parameters.Add(new SqlParameter("Admissionid_SentHope", param_ItemIssue.Admissionid_SentHope));
                    cmd.Parameters.Add(new SqlParameter("Appropriatness", xmlappropriatnessreview));
                    using (var da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }
                    data.ResubmitStatusResult = (from DataRow dr in dt.Tables[0].Rows
                                                 select dr["Result"].ToString()).Single();

                    if (GetSettingCLMA(param_ItemIssue.OrganizationId).ToUpper().Equals("TRUE"))
                    {
                        PharmacyPickingListHeader h = new PharmacyPickingListHeader();
                        h = (from DataRow dr in dt.Tables[1].Rows
                             select new PharmacyPickingListHeader
                             {
                                 EncounterId = Guid.Parse(dr["EncounterId"].ToString()),
                                 OrganizationId = long.Parse(dr["OrganizationId"].ToString()),
                                 PatientId = long.Parse(dr["PatientId"].ToString()),
                                 AdmissionId = long.Parse(dr["AdmissionId"].ToString()),
                                 AdmissionNo = dr["AdmissionNo"].ToString(),
                                 LocalMrNo = dr["LocalMrNo"].ToString(),
                                 PatientName = dr["PatientName"].ToString(),
                                 BirthDate = DateTime.Parse(dr["BirthDate"].ToString()).ToString("dd MMM yyyy"),
                                 Age = dr["Age"].ToString(),
                                 Gender = dr["Gender"].ToString(),
                                 DoctorName = dr["DoctorName"].ToString(),
                                 SpecialtyName = dr["SpecialtyName"].ToString(),
                                 PrescriptionDate = DateTime.Parse(dr["PrescriptionDate"].ToString()).ToString("dd MMM yyyy HH:mm"),
                                 PrescriptionNo = dr["PrescriptionNo"].ToString(),
                                 PayerName = dr["PayerName"].ToString(),
                                 QueueNo = dr["QueueNo"].ToString(),
                                 IsCOVID = bool.Parse(dr["IsCOVID"].ToString()),
                                 SipNo = dr["SipNo"].ToString(),
                                 PrintNumber = dr["PrintNumber"].ToString(),
                                 StoreName = dr["StoreName"].ToString(),
                                 IssueDate = dr["IssueDate"].ToString(),
                                 IssueBy = dr["IssueBy"].ToString(),
                                 IssueCode = dr["IssueCode"].ToString(),
                                 IssueAdmissionNo = dr["IssueAdmissionNo"].ToString(),
                                 DeliveryAddress = dr["DeliveryAddress"].ToString(),
                                 DeliveryFee = dr["DeliveryFee"].ToString(),
                                 DeliveryCourier = dr["DeliveryCourier"].ToString()
                             }).Single();

                        List<PharmacyPickingListPres> p = new List<PharmacyPickingListPres>();
                        p = (from DataRow dr in dt.Tables[2].Rows
                             select new PharmacyPickingListPres
                             {
                                 prescription_id = long.Parse(dr["prescription_id"].ToString()),
                                 item_id = long.Parse(dr["item_id"].ToString()),
                                 item_name = dr["item_name"].ToString(),
                                 quantity = dr["quantity"].ToString(),
                                 uom_id = long.Parse(dr["uom_id"].ToString()),
                                 uom_code = dr["uom_code"].ToString(),
                                 frequency_id = long.Parse(dr["frequency_id"].ToString()),
                                 frequency_code = dr["frequency_code"].ToString(),
                                 dosage_id = dr["dosage_id"].ToString(),
                                 dose_uom_id = long.Parse(dr["dose_uom_id"].ToString()),
                                 dose_uom = dr["dose_uom"].ToString(),
                                 dose_text = dr["dose_text"].ToString(),
                                 administration_route_id = long.Parse(dr["administration_route_id"].ToString()),
                                 administration_route_code = dr["administration_route_code"].ToString(),
                                 remarks = dr["remarks"].ToString(),
                                 iteration = int.Parse(dr["iteration"].ToString()),
                                 is_consumables = bool.Parse(dr["is_consumables"].ToString()),
                                 is_routine = bool.Parse(dr["is_routine"].ToString()),
                                 compound_id = Guid.Parse(dr["compound_id"].ToString()),
                                 compound_name = dr["compound_name"].ToString(),
                                 origin_prescription_id = long.Parse(dr["origin_prescription_id"].ToString()),
                                 RackName = dr["RackName"].ToString(),
                                 PrescriptionDate = DateTime.Parse(dr["PrescriptionDate"].ToString()).ToString("dd-MMM-yyyy"),
                                 IsDoseText = bool.Parse(dr["IsDoseText"].ToString()),
                                 IssuedQty = dr["IssuedQty"].ToString().Replace(",", "."),
                                 item_sequence = int.Parse(dr["item_sequence"].ToString()),
                                 editedId = Guid.Parse(dr["editedId"].ToString()),
                                 LocationDrug = dr["LocationDrug"].ToString()
                             }).ToList();

                        List<PharmacyPickingListAllergy> a = new List<PharmacyPickingListAllergy>();
                        a = (from DataRow dr in dt.Tables[3].Rows
                             select new PharmacyPickingListAllergy
                             {
                                 allergy = dr["allergy"].ToString(),
                                 reaction = dr["reaction"].ToString()
                             }).ToList();

                        List<PharmacyPickingListObjective> o = new List<PharmacyPickingListObjective>();
                        o = (from DataRow dr in dt.Tables[4].Rows
                             select new PharmacyPickingListObjective
                             {
                                 soap_mapping_id = Guid.Parse(dr["soap_mapping_id"].ToString()),
                                 soap_mapping_name = dr["soap_mapping_name"].ToString(),
                                 value = dr["value"].ToString()
                             }).ToList();

                        List<PharmacyPickingListCompoundHeader> ch = new List<PharmacyPickingListCompoundHeader>();
                        ch = (from DataRow dr in dt.Tables[5].Rows
                              select new PharmacyPickingListCompoundHeader
                              {
                                  prescription_compound_header_id = Guid.Parse(dr["prescription_compound_header_id"].ToString()),
                                  compound_name = dr["compound_name"].ToString(),
                                  quantity = dr["quantity"].ToString().Replace(",", "."),
                                  uom_id = long.Parse(dr["uom_id"].ToString()),
                                  uom_code = dr["uom_code"].ToString(),
                                  administration_frequency_id = long.Parse(dr["administration_frequency_id"].ToString()),
                                  frequency_code = dr["frequency_code"].ToString(),
                                  dose = dr["dose"].ToString().Replace(",", "."),
                                  dose_uom_id = long.Parse(dr["dose_uom_id"].ToString()),
                                  dose_uom = dr["dose_uom"].ToString(),
                                  administration_route_id = long.Parse(dr["administration_route_id"].ToString()),
                                  administration_route_code = dr["administration_route_code"].ToString(),
                                  administration_instruction = dr["administration_instruction"].ToString(),
                                  iter = int.Parse(dr["iter"].ToString()),
                                  item_sequence = short.Parse(dr["item_sequence"].ToString()),
                                  PrescriptionDate = DateTime.Parse(dr["PrescriptionDate"].ToString()).ToString("dd-MMM-yyyy"),
                                  dose_text = dr["dose_text"].ToString(),
                                  IsDoseText = bool.Parse(dr["IsDoseText"].ToString()),
                                  compound_note = dr["compound_note"].ToString(),
                                  IssuedQty = dr["IssuedQty"].ToString().Replace(",", ".")
                              }).ToList();

                        List<PharmacyPickingListCompoundDetail> cd = new List<PharmacyPickingListCompoundDetail>();
                        cd = (from DataRow dr in dt.Tables[6].Rows
                              select new PharmacyPickingListCompoundDetail
                              {
                                  prescription_compound_header_id = Guid.Parse(dr["prescription_compound_header_id"].ToString()),
                                  prescription_compound_detail_id = Guid.Parse(dr["prescription_compound_detail_id"].ToString()),
                                  quantity = dr["quantity"].ToString().Replace(",", "."),
                                  uom_id = long.Parse(dr["uom_id"].ToString()),
                                  uom_code = dr["uom_code"].ToString(),
                                  item_id = long.Parse(dr["item_id"].ToString()),
                                  item_name = dr["item_name"].ToString(),
                                  item_sequence = short.Parse(dr["item_sequence"].ToString()),
                                  RackName = dr["RackName"].ToString(),
                                  dose_uom_id = long.Parse(dr["dose_uom_id"].ToString()),
                                  dose = dr["dose"].ToString().Replace(",", "."),
                                  dose_text = dr["dose_text"].ToString(),
                                  IsDoseText = bool.Parse(dr["IsDoseText"].ToString()),
                                  dose_uom_code = dr["dose_uom_code"].ToString(),
                                  IssuedQty = dr["IssuedQty"].ToString().Replace(",", "."),
                                  editedId = Guid.Parse(dr["editedId"].ToString()),
                                  LocationDrug = dr["LocationDrug"].ToString()
                              }).ToList();

                        data.pharmacyPickingList = new PharmacyPickingList();
                        data.pharmacyPickingList.pickingListHeader = h;
                        data.pharmacyPickingList.pickingListPres = p;
                        data.pharmacyPickingList.pickingListAllergy = a;
                        data.pharmacyPickingList.pickingListObjective = o;
                        data.pharmacyPickingList.pickingListCompoundHeader = ch;
                        data.pharmacyPickingList.pickingListCompoundDetail = cd;
                    }
                    else
                    {
                        data.pharmacyPickingList = new PharmacyPickingList();
                    }

                    data.HopeStatusResult = param_ResubmitHope.HopeStatusResult;
                    data.HopeMessageResult = param_ResubmitHope.HopeMessageResult;
                    data.HopedataResult = param_ResubmitHope.HopedataResult;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return data;
        }

        public ResponseResubmitHope resubmitadditionalitemissue(Param_ResubmitItemIssue param_ItemIssue, ResponseResubmitHope param_ResubmitHope)
        {
            DataSet dt = new DataSet();
            ResponseResubmitHope data = new ResponseResubmitHope();
            try
            {
                string xmlDrugCons = ConvertDrugConsToXML(param_ItemIssue.drugCons);
                string xmlCompounds = ConvertCompoundsToXML(param_ItemIssue.compounds);
                string xmlAdditionalItems = ConvertAdditionalItemsToXML(param_ItemIssue.additionalItems);
                string xmlappropriatnessreview = ConvertAppropriatnessReviewToXML(param_ItemIssue.appropriatnessReview);

                using (SqlConnection conn = new SqlConnection(Siloam.System.ApplicationSetting.ConnectionString))
                {
                    conn.Open();
                    SqlCommand cmd = conn.CreateCommand();
                    cmd.CommandText = "spResubmit_Additional_ItemIssue";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("OrganizationId", param_ItemIssue.OrganizationId));
                    cmd.Parameters.Add(new SqlParameter("PatientId", param_ItemIssue.PatientId));
                    cmd.Parameters.Add(new SqlParameter("DoctorId", param_ItemIssue.DoctorId));
                    cmd.Parameters.Add(new SqlParameter("store_id", param_ItemIssue.store_id));
                    cmd.Parameters.Add(new SqlParameter("Admissionid", param_ItemIssue.Admissionid));
                    cmd.Parameters.Add(new SqlParameter("EncounterId", param_ItemIssue.EncounterId));
                    cmd.Parameters.Add(new SqlParameter("SubmitBy", param_ItemIssue.SubmitBy));
                    cmd.Parameters.Add(new SqlParameter("ItemIssueDrugCons", xmlDrugCons));
                    cmd.Parameters.Add(new SqlParameter("ItemIssueCompounds", xmlCompounds));
                    cmd.Parameters.Add(new SqlParameter("ItemIssueAdditionalItem", xmlAdditionalItems));
                    cmd.Parameters.Add(new SqlParameter("Admissionid_SentHope", param_ItemIssue.Admissionid_SentHope));
                    cmd.Parameters.Add(new SqlParameter("Appropriatness", xmlappropriatnessreview));

                    using (var da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }
                    data.ResubmitStatusResult = (from DataRow dr in dt.Tables[0].Rows
                                                 select dr["Result"].ToString()).Single();

                    if (GetSettingCLMA(param_ItemIssue.OrganizationId).ToUpper().Equals("TRUE"))
                    {
                        PharmacyPickingListHeader h = new PharmacyPickingListHeader();
                        h = (from DataRow dr in dt.Tables[1].Rows
                             select new PharmacyPickingListHeader
                             {
                                 EncounterId = Guid.Parse(dr["EncounterId"].ToString()),
                                 OrganizationId = long.Parse(dr["OrganizationId"].ToString()),
                                 PatientId = long.Parse(dr["PatientId"].ToString()),
                                 AdmissionId = long.Parse(dr["AdmissionId"].ToString()),
                                 AdmissionNo = dr["AdmissionNo"].ToString(),
                                 LocalMrNo = dr["LocalMrNo"].ToString(),
                                 PatientName = dr["PatientName"].ToString(),
                                 BirthDate = DateTime.Parse(dr["BirthDate"].ToString()).ToString("dd MMM yyyy"),
                                 Age = dr["Age"].ToString(),
                                 Gender = dr["Gender"].ToString(),
                                 DoctorName = dr["DoctorName"].ToString(),
                                 SpecialtyName = dr["SpecialtyName"].ToString(),
                                 PrescriptionDate = DateTime.Parse(dr["PrescriptionDate"].ToString()).ToString("dd MMM yyyy HH:mm"),
                                 PrescriptionNo = dr["PrescriptionNo"].ToString(),
                                 PayerName = dr["PayerName"].ToString(),
                                 QueueNo = dr["QueueNo"].ToString(),
                                 IsCOVID = bool.Parse(dr["IsCOVID"].ToString()),
                                 SipNo = dr["SipNo"].ToString(),
                                 PrintNumber = dr["PrintNumber"].ToString(),
                                 StoreName = dr["StoreName"].ToString(),
                                 IssueDate = dr["IssueDate"].ToString(),
                                 IssueBy = dr["IssueBy"].ToString(),
                                 IssueCode = dr["IssueCode"].ToString(),
                                 IssueAdmissionNo = dr["IssueAdmissionNo"].ToString(),
                                 DeliveryAddress = dr["DeliveryAddress"].ToString(),
                                 DeliveryFee = dr["DeliveryFee"].ToString(),
                                 DeliveryCourier = dr["DeliveryCourier"].ToString()
                             }).Single();

                        List<PharmacyPickingListPres> p = new List<PharmacyPickingListPres>();
                        p = (from DataRow dr in dt.Tables[2].Rows
                             select new PharmacyPickingListPres
                             {
                                 prescription_id = long.Parse(dr["prescription_id"].ToString()),
                                 item_id = long.Parse(dr["item_id"].ToString()),
                                 item_name = dr["item_name"].ToString(),
                                 quantity = dr["quantity"].ToString(),
                                 uom_id = long.Parse(dr["uom_id"].ToString()),
                                 uom_code = dr["uom_code"].ToString(),
                                 frequency_id = long.Parse(dr["frequency_id"].ToString()),
                                 frequency_code = dr["frequency_code"].ToString(),
                                 dosage_id = dr["dosage_id"].ToString(),
                                 dose_uom_id = long.Parse(dr["dose_uom_id"].ToString()),
                                 dose_uom = dr["dose_uom"].ToString(),
                                 dose_text = dr["dose_text"].ToString(),
                                 administration_route_id = long.Parse(dr["administration_route_id"].ToString()),
                                 administration_route_code = dr["administration_route_code"].ToString(),
                                 remarks = dr["remarks"].ToString(),
                                 iteration = int.Parse(dr["iteration"].ToString()),
                                 is_consumables = bool.Parse(dr["is_consumables"].ToString()),
                                 is_routine = bool.Parse(dr["is_routine"].ToString()),
                                 compound_id = Guid.Parse(dr["compound_id"].ToString()),
                                 compound_name = dr["compound_name"].ToString(),
                                 origin_prescription_id = long.Parse(dr["origin_prescription_id"].ToString()),
                                 RackName = dr["RackName"].ToString(),
                                 PrescriptionDate = DateTime.Parse(dr["PrescriptionDate"].ToString()).ToString("dd-MMM-yyyy"),
                                 IsDoseText = bool.Parse(dr["IsDoseText"].ToString()),
                                 IssuedQty = dr["IssuedQty"].ToString().Replace(",", "."),
                                 item_sequence = int.Parse(dr["item_sequence"].ToString()),
                                 editedId = Guid.Parse(dr["editedId"].ToString()),
                                 LocationDrug = dr["LocationDrug"].ToString()
                             }).ToList();

                        List<PharmacyPickingListAllergy> a = new List<PharmacyPickingListAllergy>();
                        a = (from DataRow dr in dt.Tables[3].Rows
                             select new PharmacyPickingListAllergy
                             {
                                 allergy = dr["allergy"].ToString(),
                                 reaction = dr["reaction"].ToString()
                             }).ToList();

                        List<PharmacyPickingListObjective> o = new List<PharmacyPickingListObjective>();
                        o = (from DataRow dr in dt.Tables[4].Rows
                             select new PharmacyPickingListObjective
                             {
                                 soap_mapping_id = Guid.Parse(dr["soap_mapping_id"].ToString()),
                                 soap_mapping_name = dr["soap_mapping_name"].ToString(),
                                 value = dr["value"].ToString()
                             }).ToList();

                        List<PharmacyPickingListCompoundHeader> ch = new List<PharmacyPickingListCompoundHeader>();
                        ch = (from DataRow dr in dt.Tables[5].Rows
                              select new PharmacyPickingListCompoundHeader
                              {
                                  prescription_compound_header_id = Guid.Parse(dr["prescription_compound_header_id"].ToString()),
                                  compound_name = dr["compound_name"].ToString(),
                                  quantity = dr["quantity"].ToString().Replace(",", "."),
                                  uom_id = long.Parse(dr["uom_id"].ToString()),
                                  uom_code = dr["uom_code"].ToString(),
                                  administration_frequency_id = long.Parse(dr["administration_frequency_id"].ToString()),
                                  frequency_code = dr["frequency_code"].ToString(),
                                  dose = dr["dose"].ToString().Replace(",", "."),
                                  dose_uom_id = long.Parse(dr["dose_uom_id"].ToString()),
                                  dose_uom = dr["dose_uom"].ToString(),
                                  administration_route_id = long.Parse(dr["administration_route_id"].ToString()),
                                  administration_route_code = dr["administration_route_code"].ToString(),
                                  administration_instruction = dr["administration_instruction"].ToString(),
                                  iter = int.Parse(dr["iter"].ToString()),
                                  item_sequence = short.Parse(dr["item_sequence"].ToString()),
                                  PrescriptionDate = DateTime.Parse(dr["PrescriptionDate"].ToString()).ToString("dd-MMM-yyyy"),
                                  dose_text = dr["dose_text"].ToString(),
                                  IsDoseText = bool.Parse(dr["IsDoseText"].ToString()),
                                  compound_note = dr["compound_note"].ToString(),
                                  IssuedQty = dr["IssuedQty"].ToString().Replace(",", ".")
                              }).ToList();

                        List<PharmacyPickingListCompoundDetail> cd = new List<PharmacyPickingListCompoundDetail>();
                        cd = (from DataRow dr in dt.Tables[6].Rows
                              select new PharmacyPickingListCompoundDetail
                              {
                                  prescription_compound_header_id = Guid.Parse(dr["prescription_compound_header_id"].ToString()),
                                  prescription_compound_detail_id = Guid.Parse(dr["prescription_compound_detail_id"].ToString()),
                                  quantity = dr["quantity"].ToString().Replace(",", "."),
                                  uom_id = long.Parse(dr["uom_id"].ToString()),
                                  uom_code = dr["uom_code"].ToString(),
                                  item_id = long.Parse(dr["item_id"].ToString()),
                                  item_name = dr["item_name"].ToString(),
                                  item_sequence = short.Parse(dr["item_sequence"].ToString()),
                                  RackName = dr["RackName"].ToString(),
                                  dose_uom_id = long.Parse(dr["dose_uom_id"].ToString()),
                                  dose = dr["dose"].ToString().Replace(",", "."),
                                  dose_text = dr["dose_text"].ToString(),
                                  IsDoseText = bool.Parse(dr["IsDoseText"].ToString()),
                                  dose_uom_code = dr["dose_uom_code"].ToString(),
                                  IssuedQty = dr["IssuedQty"].ToString().Replace(",", "."),
                                  editedId = Guid.Parse(dr["editedId"].ToString()),
                                  LocationDrug = dr["LocationDrug"].ToString()
                              }).ToList();

                        data.pharmacyPickingList = new PharmacyPickingList();
                        data.pharmacyPickingList.pickingListHeader = h;
                        data.pharmacyPickingList.pickingListPres = p;
                        data.pharmacyPickingList.pickingListAllergy = a;
                        data.pharmacyPickingList.pickingListObjective = o;
                        data.pharmacyPickingList.pickingListCompoundHeader = ch;
                        data.pharmacyPickingList.pickingListCompoundDetail = cd;
                    }
                    else
                    {
                        data.pharmacyPickingList = new PharmacyPickingList();
                    }

                    data.HopeStatusResult = param_ResubmitHope.HopeStatusResult;
                    data.HopeMessageResult = param_ResubmitHope.HopeMessageResult;
                    data.HopedataResult = param_ResubmitHope.HopedataResult;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return data;
        }
        public SubmitPrintPrescription submitPrintPrescription(Param_RecordSubmit param_ReordModel, Param_SyncResult param_SyncResultModel)
        {
            DataSet dt = new DataSet();
            SubmitPrintPrescription data = new SubmitPrintPrescription();
            try
            {
                PharmacyData PharmacyDataModel      = param_ReordModel.PharmacyDataModel;
                SingleQueue SingleQDataModel        = param_ReordModel.SingleQueueDataModel;
                long SubmitBy                       = param_ReordModel.SubmitBy;
                long TransAdmId                     = param_ReordModel.TransAdmId;
                string TransAdmNo                   = param_ReordModel.TransAdmNo;
                string QueueNo                      = param_ReordModel.QueueNo;
                if (QueueNo is null)
                {
                    QueueNo = "";
                }
                string DeliveryFee      = param_ReordModel.DeliveryFee;
                string PayerCoverage    = param_ReordModel.PayerCoverage;


                string xmlheader = ConvertPresscriptionToXML(
                                                        PharmacyDataModel.header.EncounterId.ToString(),
                                                        PharmacyDataModel.header.OrganizationId,
                                                        PharmacyDataModel.header.Admissionid,
                                                        PharmacyDataModel.header.PatientId,
                                                        PharmacyDataModel.header.DoctorId,
                                                        PharmacyDataModel.header.PharmacyNotes,
                                                        PharmacyDataModel.header.IsEditDrug,
                                                        PharmacyDataModel.header.IsEditCompound,
                                                        PharmacyDataModel.header.IsEditConsumables,
                                                        PharmacyDataModel.header.IsSelfCollection,
                                                        PharmacyDataModel.header.store_id,
                                                        PharmacyDataModel.header.prefix_desc,
                                                        PharmacyDataModel.header.is_tele,
                                                        PharmacyDataModel.header.is_SingleQueue,
                                                        PharmacyDataModel.header.is_SendDataItemIssue,
                                                        PharmacyDataModel.header.VerifyTime,
                                                        PharmacyDataModel.header.Admissionid_SentHope);
                string xmlprescription              = ConvertPrescriptionToXML(PharmacyDataModel.prescription);
                string xmldruginfo                  = ConverDrugInfoToXML(PharmacyDataModel.druginfo);
                string xmlcompoundheader            = ConvertCompoundHeaderToXML(PharmacyDataModel.compound_header);
                string xmlcompounddetail            = ConvertCompoundDetailHopeToXML(PharmacyDataModel.compound_detail);
                string xmlsingleQ                   = ConverSingleQueueToXML(SubmitBy, PharmacyDataModel.header, PharmacyDataModel.singleQWorklistData, SingleQDataModel);
                string RequestWorklistSingleQueue   = JsonConvert.SerializeObject(PharmacyDataModel.singleQWorklistData);
                string xmleditreason                = ConvertEditReasonToXML(PharmacyDataModel.editReason);
                string xmlappropriatnessreview      = ConvertAppropriatnessReviewToXML(PharmacyDataModel.appropriatnessReview);
                string xmladditionalitem            = ConvertAdditionalItemToXML(PharmacyDataModel.additionalItem);
                string xmleditedmapping            = ConvertEditedMappingToXML(PharmacyDataModel.pharmacyEditedMappings);
                

                using (SqlConnection conn = new SqlConnection(Siloam.System.ApplicationSetting.ConnectionString))
                {
                    conn.Open();
                    SqlCommand cmd = conn.CreateCommand();
                    cmd.CommandText = "spSUBMITPRINT_PRESCRIPTION";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("Header"                , xmlheader));
                    cmd.Parameters.Add(new SqlParameter("Prescription"          , xmlprescription));
                    cmd.Parameters.Add(new SqlParameter("CompoundHeader"        , xmlcompoundheader));
                    cmd.Parameters.Add(new SqlParameter("CompoundDetail"        , xmlcompounddetail));
                    cmd.Parameters.Add(new SqlParameter("DrugInfo"              , xmldruginfo));
                    cmd.Parameters.Add(new SqlParameter("SingleQ"               , xmlsingleQ));
                    cmd.Parameters.Add(new SqlParameter("EditReason"            , xmleditreason));
                    cmd.Parameters.Add(new SqlParameter("Jsonrequest_singleQ"   , RequestWorklistSingleQueue));
                    cmd.Parameters.Add(new SqlParameter("Jsonresponse_singleQ"  , param_ReordModel.ResponseWorklistSingleQueue));
                    cmd.Parameters.Add(new SqlParameter("TransAdmId"            , TransAdmId));
                    cmd.Parameters.Add(new SqlParameter("TransAdmNo"            , TransAdmNo));
                    cmd.Parameters.Add(new SqlParameter("QueueNo"               , QueueNo));
                    cmd.Parameters.Add(new SqlParameter("DeliveryFee"           , DeliveryFee));
                    cmd.Parameters.Add(new SqlParameter("Updater"               , SubmitBy));
                    cmd.Parameters.Add(new SqlParameter("PayerCoverage"         , PayerCoverage));
                    cmd.Parameters.Add(new SqlParameter("Appropriatness"        , xmlappropriatnessreview));
                    cmd.Parameters.Add(new SqlParameter("AdditionalItem"        , xmladditionalitem));
                    cmd.Parameters.Add(new SqlParameter("EditedMapping"         , xmleditedmapping));



                    using (var da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }
                    data.SubmitStatusPressResult = (from DataRow dr in dt.Tables[0].Rows
                                              select dr["Result"].ToString()).Single();

                    if (GetSettingCLMA(PharmacyDataModel.header.OrganizationId).ToUpper().Equals("TRUE"))
                    {
                        PharmacyPickingListHeader h = new PharmacyPickingListHeader();
                        h = (from DataRow dr in dt.Tables[1].Rows
                             select new PharmacyPickingListHeader
                             {
                                 EncounterId = Guid.Parse(dr["EncounterId"].ToString()),
                                 OrganizationId = long.Parse(dr["OrganizationId"].ToString()),
                                 PatientId = long.Parse(dr["PatientId"].ToString()),
                                 AdmissionId = long.Parse(dr["AdmissionId"].ToString()),
                                 AdmissionNo = dr["AdmissionNo"].ToString(),
                                 LocalMrNo = dr["LocalMrNo"].ToString(),
                                 PatientName = dr["PatientName"].ToString(),
                                 BirthDate = DateTime.Parse(dr["BirthDate"].ToString()).ToString("dd MMM yyyy"),
                                 Age = dr["Age"].ToString(),
                                 Gender = dr["Gender"].ToString(),
                                 DoctorName = dr["DoctorName"].ToString(),
                                 SpecialtyName = dr["SpecialtyName"].ToString(),
                                 PrescriptionDate = DateTime.Parse(dr["PrescriptionDate"].ToString()).ToString("dd MMM yyyy HH:mm"),
                                 PrescriptionNo = dr["PrescriptionNo"].ToString(),
                                 PayerName = dr["PayerName"].ToString(),
                                 QueueNo = dr["QueueNo"].ToString(),
                                 IsCOVID = bool.Parse(dr["IsCOVID"].ToString()),
                                 SipNo = dr["SipNo"].ToString(),
                                 PrintNumber = dr["PrintNumber"].ToString(),
                                 StoreName = dr["StoreName"].ToString(),
                                 IssueDate = dr["IssueDate"].ToString(),
                                 IssueBy = dr["IssueBy"].ToString(),
                                 IssueCode = dr["IssueCode"].ToString(),
                                 IssueAdmissionNo = dr["IssueAdmissionNo"].ToString(),
                                 DeliveryAddress = dr["DeliveryAddress"].ToString(),
                                 DeliveryFee = dr["DeliveryFee"].ToString(),
                                 DeliveryCourier = dr["DeliveryCourier"].ToString()
                             }).Single();

                        List<PharmacyPickingListPres> p = new List<PharmacyPickingListPres>();
                        p = (from DataRow dr in dt.Tables[2].Rows
                             select new PharmacyPickingListPres
                             {
                                 prescription_id = long.Parse(dr["prescription_id"].ToString()),
                                 item_id = long.Parse(dr["item_id"].ToString()),
                                 item_name = dr["item_name"].ToString(),
                                 quantity = dr["quantity"].ToString(),
                                 uom_id = long.Parse(dr["uom_id"].ToString()),
                                 uom_code = dr["uom_code"].ToString(),
                                 frequency_id = long.Parse(dr["frequency_id"].ToString()),
                                 frequency_code = dr["frequency_code"].ToString(),
                                 dosage_id = dr["dosage_id"].ToString(),
                                 dose_uom_id = long.Parse(dr["dose_uom_id"].ToString()),
                                 dose_uom = dr["dose_uom"].ToString(),
                                 dose_text = dr["dose_text"].ToString(),
                                 administration_route_id = long.Parse(dr["administration_route_id"].ToString()),
                                 administration_route_code = dr["administration_route_code"].ToString(),
                                 remarks = dr["remarks"].ToString(),
                                 iteration = int.Parse(dr["iteration"].ToString()),
                                 is_consumables = bool.Parse(dr["is_consumables"].ToString()),
                                 is_routine = bool.Parse(dr["is_routine"].ToString()),
                                 compound_id = Guid.Parse(dr["compound_id"].ToString()),
                                 compound_name = dr["compound_name"].ToString(),
                                 origin_prescription_id = long.Parse(dr["origin_prescription_id"].ToString()),
                                 RackName = dr["RackName"].ToString(),
                                 PrescriptionDate = DateTime.Parse(dr["PrescriptionDate"].ToString()).ToString("dd-MMM-yyyy"),
                                 IsDoseText = bool.Parse(dr["IsDoseText"].ToString()),
                                 IssuedQty = dr["IssuedQty"].ToString().Replace(",", "."),
                                 item_sequence = int.Parse(dr["item_sequence"].ToString()),
                                 editedId = Guid.Parse(dr["editedId"].ToString()),
                                 LocationDrug = dr["LocationDrug"].ToString()
                             }).ToList();

                        List<PharmacyPickingListAllergy> a = new List<PharmacyPickingListAllergy>();
                        a = (from DataRow dr in dt.Tables[3].Rows
                             select new PharmacyPickingListAllergy
                             {
                                 allergy = dr["allergy"].ToString(),
                                 reaction = dr["reaction"].ToString()
                             }).ToList();

                        List<PharmacyPickingListObjective> o = new List<PharmacyPickingListObjective>();
                        o = (from DataRow dr in dt.Tables[4].Rows
                             select new PharmacyPickingListObjective
                             {
                                 soap_mapping_id = Guid.Parse(dr["soap_mapping_id"].ToString()),
                                 soap_mapping_name = dr["soap_mapping_name"].ToString(),
                                 value = dr["value"].ToString()
                             }).ToList();

                        List<PharmacyPickingListCompoundHeader> ch = new List<PharmacyPickingListCompoundHeader>();
                        ch = (from DataRow dr in dt.Tables[5].Rows
                              select new PharmacyPickingListCompoundHeader
                              {
                                  prescription_compound_header_id = Guid.Parse(dr["prescription_compound_header_id"].ToString()),
                                  compound_name = dr["compound_name"].ToString(),
                                  quantity = dr["quantity"].ToString().Replace(",", "."),
                                  uom_id = long.Parse(dr["uom_id"].ToString()),
                                  uom_code = dr["uom_code"].ToString(),
                                  administration_frequency_id = long.Parse(dr["administration_frequency_id"].ToString()),
                                  frequency_code = dr["frequency_code"].ToString(),
                                  dose = dr["dose"].ToString().Replace(",", "."),
                                  dose_uom_id = long.Parse(dr["dose_uom_id"].ToString()),
                                  dose_uom = dr["dose_uom"].ToString(),
                                  administration_route_id = long.Parse(dr["administration_route_id"].ToString()),
                                  administration_route_code = dr["administration_route_code"].ToString(),
                                  administration_instruction = dr["administration_instruction"].ToString(),
                                  iter = int.Parse(dr["iter"].ToString()),
                                  item_sequence = short.Parse(dr["item_sequence"].ToString()),
                                  PrescriptionDate = DateTime.Parse(dr["PrescriptionDate"].ToString()).ToString("dd-MMM-yyyy"),
                                  dose_text = dr["dose_text"].ToString(),
                                  IsDoseText = bool.Parse(dr["IsDoseText"].ToString()),
                                  compound_note = dr["compound_note"].ToString(),
                                  IssuedQty = dr["IssuedQty"].ToString().Replace(",", ".")
                              }).ToList();

                        List<PharmacyPickingListCompoundDetail> cd = new List<PharmacyPickingListCompoundDetail>();
                        cd = (from DataRow dr in dt.Tables[6].Rows
                              select new PharmacyPickingListCompoundDetail
                              {
                                  prescription_compound_header_id = Guid.Parse(dr["prescription_compound_header_id"].ToString()),
                                  prescription_compound_detail_id = Guid.Parse(dr["prescription_compound_detail_id"].ToString()),
                                  quantity = dr["quantity"].ToString().Replace(",", "."),
                                  uom_id = long.Parse(dr["uom_id"].ToString()),
                                  uom_code = dr["uom_code"].ToString(),
                                  item_id = long.Parse(dr["item_id"].ToString()),
                                  item_name = dr["item_name"].ToString(),
                                  item_sequence = short.Parse(dr["item_sequence"].ToString()),
                                  RackName = dr["RackName"].ToString(),
                                  dose_uom_id = long.Parse(dr["dose_uom_id"].ToString()),
                                  dose = dr["dose"].ToString().Replace(",", "."),
                                  dose_text = dr["dose_text"].ToString(),
                                  IsDoseText = bool.Parse(dr["IsDoseText"].ToString()),
                                  dose_uom_code = dr["dose_uom_code"].ToString(),
                                  IssuedQty = dr["IssuedQty"].ToString().Replace(",", "."),
                                  editedId = Guid.Parse(dr["editedId"].ToString()),
                                  LocationDrug = dr["LocationDrug"].ToString()
                              }).ToList();

                        data.printHeader = new PharmacyPrintHeader();
                        data.printPres = new List<PharmacyPrintPres>();
                        data.printAllergy = new List<PharmacyPrintAllergy>();
                        data.printObjective = new List<PharmacyPrintObjective>();
                        data.printCompoundHeader = new List<PharmacyPrintCompoundHeader>();
                        data.printCompoundDetail = new List<PharmacyPrintCompoundDetail>();
                        data.pharmacyPickingList = new PharmacyPickingList();
                        data.pharmacyPickingList.pickingListHeader = h;
                        data.pharmacyPickingList.pickingListPres = p;
                        data.pharmacyPickingList.pickingListAllergy = a;
                        data.pharmacyPickingList.pickingListObjective = o;
                        data.pharmacyPickingList.pickingListCompoundHeader = ch;
                        data.pharmacyPickingList.pickingListCompoundDetail = cd;
                    }
                    else
                    {
                        PharmacyPrintHeader h = new PharmacyPrintHeader();
                        h = (from DataRow dr in dt.Tables[1].Rows
                             select new PharmacyPrintHeader
                             {
                                 EncounterId = Guid.Parse(dr["EncounterId"].ToString()),
                                 OrganizationId = long.Parse(dr["OrganizationId"].ToString()),
                                 PatientId = long.Parse(dr["PatientId"].ToString()),
                                 AdmissionId = long.Parse(dr["AdmissionId"].ToString()),
                                 AdmissionNo = dr["AdmissionNo"].ToString(),
                                 LocalMrNo = dr["LocalMrNo"].ToString(),
                                 PatientName = dr["PatientName"].ToString(),
                                 BirthDate = DateTime.Parse(dr["BirthDate"].ToString()).ToString("dd MMM yyyy"),
                                 Age = dr["Age"].ToString(),
                                 Gender = dr["Gender"].ToString(),
                                 DoctorName = dr["DoctorName"].ToString(),
                                 SpecialtyName = dr["SpecialtyName"].ToString(),
                                 PrescriptionDate = DateTime.Parse(dr["PrescriptionDate"].ToString()).ToString("dd MMM yyyy HH:mm"),
                                 PrescriptionNo = dr["PrescriptionNo"].ToString(),
                                 PayerName = dr["PayerName"].ToString(),
                                 QueueNo = dr["QueueNo"].ToString(),
                                 IsCOVID = bool.Parse(dr["IsCOVID"].ToString()),
                                 SipNo = dr["SipNo"].ToString(),
                                 PrintNumber = dr["PrintNumber"].ToString()
                             }).Single();

                        List<PharmacyPrintPres> p = new List<PharmacyPrintPres>();
                        p = (from DataRow dr in dt.Tables[2].Rows
                             select new PharmacyPrintPres
                             {
                                 prescription_id = long.Parse(dr["prescription_id"].ToString()),
                                 item_id = long.Parse(dr["item_id"].ToString()),
                                 item_name = dr["item_name"].ToString(),
                                 quantity = dr["quantity"].ToString(),
                                 uom_id = long.Parse(dr["uom_id"].ToString()),
                                 uom_code = dr["uom_code"].ToString(),
                                 frequency_id = long.Parse(dr["frequency_id"].ToString()),
                                 frequency_code = dr["frequency_code"].ToString(),
                                 dosage_id = dr["dosage_id"].ToString(),
                                 dose_uom_id = long.Parse(dr["dose_uom_id"].ToString()),
                                 dose_uom = dr["dose_uom"].ToString(),
                                 dose_text = dr["dose_text"].ToString(),
                                 administration_route_id = long.Parse(dr["administration_route_id"].ToString()),
                                 administration_route_code = dr["administration_route_code"].ToString(),
                                 remarks = dr["remarks"].ToString(),
                                 iteration = int.Parse(dr["iteration"].ToString()),
                                 is_consumables = bool.Parse(dr["is_consumables"].ToString()),
                                 is_routine = bool.Parse(dr["is_routine"].ToString()),
                                 compound_id = Guid.Parse(dr["compound_id"].ToString()),
                                 compound_name = dr["compound_name"].ToString(),
                                 origin_prescription_id = long.Parse(dr["origin_prescription_id"].ToString()),
                                 RackName = dr["RackName"].ToString(),
                                 PrescriptionDate = DateTime.Parse(dr["PrescriptionDate"].ToString()).ToString("dd-MMM-yyyy"),
                                 IsDoseText = bool.Parse(dr["IsDoseText"].ToString()),
                                 IssuedQty = dr["IssuedQty"].ToString().Replace(",", ".")
                             }).ToList();

                        List<PharmacyPrintAllergy> a = new List<PharmacyPrintAllergy>();
                        a = (from DataRow dr in dt.Tables[3].Rows
                             select new PharmacyPrintAllergy
                             {
                                 allergy = dr["allergy"].ToString(),
                                 reaction = dr["reaction"].ToString()
                             }).ToList();

                        List<PharmacyPrintObjective> o = new List<PharmacyPrintObjective>();
                        o = (from DataRow dr in dt.Tables[4].Rows
                             select new PharmacyPrintObjective
                             {
                                 soap_mapping_id = Guid.Parse(dr["soap_mapping_id"].ToString()),
                                 soap_mapping_name = dr["soap_mapping_name"].ToString(),
                                 value = dr["value"].ToString()
                             }).ToList();

                        List<PharmacyPrintCompoundHeader> ch = new List<PharmacyPrintCompoundHeader>();
                        ch = (from DataRow dr in dt.Tables[5].Rows
                              select new PharmacyPrintCompoundHeader
                              {
                                  prescription_compound_header_id = Guid.Parse(dr["prescription_compound_header_id"].ToString()),
                                  compound_name = dr["compound_name"].ToString(),
                                  quantity = dr["quantity"].ToString().Replace(",", "."),
                                  uom_id = long.Parse(dr["uom_id"].ToString()),
                                  uom_code = dr["uom_code"].ToString(),
                                  administration_frequency_id = long.Parse(dr["administration_frequency_id"].ToString()),
                                  frequency_code = dr["frequency_code"].ToString(),
                                  dose = dr["dose"].ToString().Replace(",", "."),
                                  dose_uom_id = long.Parse(dr["dose_uom_id"].ToString()),
                                  dose_uom = dr["dose_uom"].ToString(),
                                  administration_route_id = long.Parse(dr["administration_route_id"].ToString()),
                                  administration_route_code = dr["administration_route_code"].ToString(),
                                  administration_instruction = dr["administration_instruction"].ToString(),
                                  iter = int.Parse(dr["iter"].ToString()),
                                  item_sequence = short.Parse(dr["item_sequence"].ToString()),
                                  PrescriptionDate = DateTime.Parse(dr["PrescriptionDate"].ToString()).ToString("dd-MMM-yyyy"),
                                  dose_text = dr["dose_text"].ToString(),
                                  IsDoseText = bool.Parse(dr["IsDoseText"].ToString()),
                                  compound_note = dr["compound_note"].ToString(),
                                  IssuedQty = dr["IssuedQty"].ToString().Replace(",", ".")
                              }).ToList();

                        List<PharmacyPrintCompoundDetail> cd = new List<PharmacyPrintCompoundDetail>();
                        cd = (from DataRow dr in dt.Tables[6].Rows
                              select new PharmacyPrintCompoundDetail
                              {
                                  prescription_compound_header_id = Guid.Parse(dr["prescription_compound_header_id"].ToString()),
                                  prescription_compound_detail_id = Guid.Parse(dr["prescription_compound_detail_id"].ToString()),
                                  quantity = dr["quantity"].ToString().Replace(",", "."),
                                  uom_id = long.Parse(dr["uom_id"].ToString()),
                                  uom_code = dr["uom_code"].ToString(),
                                  item_id = long.Parse(dr["item_id"].ToString()),
                                  item_name = dr["item_name"].ToString(),
                                  item_sequence = short.Parse(dr["item_sequence"].ToString()),
                                  RackName = dr["RackName"].ToString(),
                                  dose_uom_id = long.Parse(dr["dose_uom_id"].ToString()),
                                  dose = dr["dose"].ToString().Replace(",", "."),
                                  dose_text = dr["dose_text"].ToString(),
                                  IsDoseText = bool.Parse(dr["IsDoseText"].ToString()),
                                  dose_uom_code = dr["dose_uom_code"].ToString()
                              }).ToList();

                        data.printHeader = h;
                        data.printPres = p;
                        data.printAllergy = a;
                        data.printObjective = o;
                        data.printCompoundHeader = ch;
                        data.printCompoundDetail = cd;
                        data.pharmacyPickingList = new PharmacyPickingList();

                    }
                    data.HopeStatusResult       = param_SyncResultModel.HopeStatusResult;
                    data.HopeMessageResult      = param_SyncResultModel.HopeMessageResult;
                    data.HopedataResult         = param_SyncResultModel.HopedataResult;
                    data.SingleQStatusResult    = param_SyncResultModel.SingleQStatusResult;
                    data.SingleQMessageResult   = param_SyncResultModel.SingleQMessageResult;
                    
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return data;
        }
        public string UpdateRecordItemIssueSingleQ(Param_RecordSubmit param_ReordModel)
        {
            string data;
            try
            {
                PharmacyData PharmacyDataModel      = param_ReordModel.PharmacyDataModel;
                SingleQueue SingleQDataModel        = param_ReordModel.SingleQueueDataModel;
                DataTable dt                        = new DataTable();
                long SubmitBy                       = param_ReordModel.SubmitBy;
                long TransAdmId                     = param_ReordModel.TransAdmId;
                string TransAdmNo                   = param_ReordModel.TransAdmNo;
                string QueueNo                      = param_ReordModel.QueueNo;
                if (QueueNo is null)
                {
                    QueueNo = "";
                }
                string DeliveryFee      = param_ReordModel.DeliveryFee;
                string PayerCoverage    = param_ReordModel.PayerCoverage;


                string xmlheader = ConvertPharmacyHeaderSingleQToXML(
                                                        PharmacyDataModel.header.EncounterId.ToString(),
                                                        PharmacyDataModel.header.OrganizationId,
                                                        PharmacyDataModel.header.Admissionid,
                                                        PharmacyDataModel.header.PatientId,
                                                        PharmacyDataModel.header.DoctorId,
                                                        PharmacyDataModel.header.PharmacyNotes,
                                                        PharmacyDataModel.header.IsEditDrug,
                                                        PharmacyDataModel.header.IsEditCompound,
                                                        PharmacyDataModel.header.IsEditConsumables,
                                                        PharmacyDataModel.header.IsSelfCollection,
                                                        PharmacyDataModel.header.store_id,
                                                        PharmacyDataModel.header.prefix_desc,
                                                        PharmacyDataModel.header.is_tele,
                                                        PharmacyDataModel.header.VerifyTime);
                string xmlprescription              = ConvertPrescriptionToXML(PharmacyDataModel.prescription);
                string xmldruginfo                  = ConverDrugInfoToXML(PharmacyDataModel.druginfo);
                string xmlcompoundheader            = ConvertCompoundHeaderToXML(PharmacyDataModel.compound_header);
                string xmlcompounddetail            = ConvertCompoundDetailToXML(PharmacyDataModel.compound_detail);
                string xmlsingleQ                   = ConverSingleQueueToXML(SubmitBy, PharmacyDataModel.header, PharmacyDataModel.singleQWorklistData, SingleQDataModel);
                string RequestWorklistSingleQueue   = JsonConvert.SerializeObject(PharmacyDataModel.singleQWorklistData);

                using (SqlConnection conn = new SqlConnection(Siloam.System.ApplicationSetting.ConnectionString))
                {
                    conn.Open();
                    SqlCommand cmd = conn.CreateCommand();
                    cmd.CommandText = "spSUBMIT_PRESCRIPTIONITEMISSUESQ";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("Header", xmlheader));
                    cmd.Parameters.Add(new SqlParameter("Prescription", xmlprescription));
                    cmd.Parameters.Add(new SqlParameter("CompoundHeader", xmlcompoundheader));
                    cmd.Parameters.Add(new SqlParameter("CompoundDetail", xmlcompounddetail));
                    cmd.Parameters.Add(new SqlParameter("DrugInfo", xmldruginfo));
                    cmd.Parameters.Add(new SqlParameter("SingleQ", xmlsingleQ));
                    cmd.Parameters.Add(new SqlParameter("Jsonrequest_singleQ", RequestWorklistSingleQueue));
                    cmd.Parameters.Add(new SqlParameter("Jsonresponse_singleQ", param_ReordModel.ResponseWorklistSingleQueue));
                    cmd.Parameters.Add(new SqlParameter("TransAdmId", TransAdmId));
                    cmd.Parameters.Add(new SqlParameter("TransAdmNo", TransAdmNo));
                    cmd.Parameters.Add(new SqlParameter("QueueNo", QueueNo));
                    cmd.Parameters.Add(new SqlParameter("DeliveryFee", DeliveryFee));
                    cmd.Parameters.Add(new SqlParameter("Updater", SubmitBy));
                    cmd.Parameters.Add(new SqlParameter("PayerCoverage", PayerCoverage));
                    using (var da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }
                    data = (from DataRow dr in dt.Rows
                            select dr["Result"].ToString()).Single();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return data;
        }
        public string UpdateRecordSubmit(long SubmitBy, long TransAdmId, string TransAdmNo, string QueueNo, string DeliveryFee,string PayerCoverage, PharmacyData model)
        {
            DataTable dt = new DataTable();
            string data;
            string xmlheader = ConvertPharmacyHeaderWithPrintToXML(model.header.EncounterId.ToString(), model.header.OrganizationId, model.header.Admissionid, model.header.PatientId, model.header.DoctorId, model.header.PharmacyNotes, model.header.IsEditDrug, model.header.IsEditCompound, model.header.IsEditConsumables, model.header.IsSelfCollection,model.header.store_id,model.header.prefix_desc);
            string xmlprescription = ConvertPrescriptionToXML(model.prescription);
            string xmldruginfo = ConverDrugInfoToXML(model.druginfo);
            string xmlcompoundheader = ConvertCompoundHeaderToXML(model.compound_header);
            string xmlcompounddetail = ConvertCompoundDetailToXML(model.compound_detail);
            try
            {
                using (SqlConnection conn = new SqlConnection(Siloam.System.ApplicationSetting.ConnectionString))
                {
                    conn.Open();
                    SqlCommand cmd = conn.CreateCommand();
                    cmd.CommandText = "spSUBMIT_PRESCRIPTION";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("Header", xmlheader));
                    cmd.Parameters.Add(new SqlParameter("Prescription", xmlprescription));
                    cmd.Parameters.Add(new SqlParameter("CompoundHeader", xmlcompoundheader));
                    cmd.Parameters.Add(new SqlParameter("CompoundDetail", xmlcompounddetail));
                    cmd.Parameters.Add(new SqlParameter("DrugInfo", xmldruginfo));
                    cmd.Parameters.Add(new SqlParameter("TransAdmId", TransAdmId));
                    cmd.Parameters.Add(new SqlParameter("TransAdmNo", TransAdmNo));
                    cmd.Parameters.Add(new SqlParameter("QueueNo", QueueNo));
                    cmd.Parameters.Add(new SqlParameter("DeliveryFee", DeliveryFee));
                    cmd.Parameters.Add(new SqlParameter("Updater", SubmitBy));
                    cmd.Parameters.Add(new SqlParameter("PayerCoverage", PayerCoverage));
                    using (var da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }
                    data = (from DataRow dr in dt.Rows
                            select dr["Result"].ToString()).Single();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return data;
        }

        public string UpdateRecordSubmitItemIssue(long SubmitBy, long TransAdmId, string TransAdmNo, string QueueNo, string DeliveryFee, string PayerCoverage, PharmacyData model)
        {
            DataTable dt = new DataTable();
            string data;
            string xmlheader = ConvertPharmacyHeaderWithPrintToXML(model.header.EncounterId.ToString(), model.header.OrganizationId, model.header.Admissionid, model.header.PatientId, model.header.DoctorId, model.header.PharmacyNotes, model.header.IsEditDrug, model.header.IsEditCompound, model.header.IsEditConsumables, model.header.IsSelfCollection, model.header.store_id, model.header.prefix_desc);
            string xmlprescription = ConvertPrescriptionToXML(model.prescription);
            string xmldruginfo = ConverDrugInfoToXML(model.druginfo);
            string xmlcompoundheader = ConvertCompoundHeaderToXML(model.compound_header);
            string xmlcompounddetail = ConvertCompoundDetailToXML(model.compound_detail);
            try
            {
                using (SqlConnection conn = new SqlConnection(Siloam.System.ApplicationSetting.ConnectionString))
                {
                    conn.Open();
                    SqlCommand cmd = conn.CreateCommand();
                    cmd.CommandText = "spSUBMIT_PRESCRIPTIONITEMISSUE";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("Header", xmlheader));
                    cmd.Parameters.Add(new SqlParameter("Prescription", xmlprescription));
                    cmd.Parameters.Add(new SqlParameter("CompoundHeader", xmlcompoundheader));
                    cmd.Parameters.Add(new SqlParameter("CompoundDetail", xmlcompounddetail));
                    cmd.Parameters.Add(new SqlParameter("DrugInfo", xmldruginfo));
                    cmd.Parameters.Add(new SqlParameter("TransAdmId", TransAdmId));
                    cmd.Parameters.Add(new SqlParameter("TransAdmNo", TransAdmNo));
                    cmd.Parameters.Add(new SqlParameter("QueueNo", QueueNo));
                    cmd.Parameters.Add(new SqlParameter("DeliveryFee", DeliveryFee));
                    cmd.Parameters.Add(new SqlParameter("Updater", SubmitBy));
                    cmd.Parameters.Add(new SqlParameter("PayerCoverage", PayerCoverage));
                    using (var da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }
                    data = (from DataRow dr in dt.Rows
                            select dr["Result"].ToString()).Single();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return data;
        }
        public string UpdateDrugInfo(long OrganizationId, long AdmissionId, Guid EncounterId, long Updater, List<PharmacyDrugInfo> model)
        {
            DataTable dt = new DataTable();
            string data;
            string xmldruginfo = ConverDrugInfoToXML(model);
            try
            {
                using (SqlConnection conn = new SqlConnection(Siloam.System.ApplicationSetting.ConnectionString))
                {
                    conn.Open();
                    SqlCommand cmd = conn.CreateCommand();
                    cmd.CommandText = "spSUBMIT_DRUGINFO";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("EncounterId", EncounterId));
                    cmd.Parameters.Add(new SqlParameter("AdmissionId", AdmissionId));
                    cmd.Parameters.Add(new SqlParameter("OrganizationId", OrganizationId));
                    cmd.Parameters.Add(new SqlParameter("DrugInfo", xmldruginfo));
                    cmd.Parameters.Add(new SqlParameter("Updater", Updater));
                    using (var da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }
                    data = (from DataRow dr in dt.Rows
                            select dr["Result"].ToString()).Single();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return data;
        }

        public string UpdateRecordAdditional(Param_RecordSubmit param_ReordModel)//(long SubmitBy, long TransAdmId, string TransAdmNo, string QueueNo, PharmacyData model)
        {
            PharmacyData PharmacyDataModel      = param_ReordModel.PharmacyDataModel;
            SingleQueue SingleQDataModel        = param_ReordModel.SingleQueueDataModel;
            DataTable dt                        = new DataTable();
            long SubmitBy                       = param_ReordModel.SubmitBy;
            long TransAdmId                     = param_ReordModel.TransAdmId;
            string TransAdmNo                   = param_ReordModel.TransAdmNo;
            string QueueNo                      = param_ReordModel.QueueNo;
            if (QueueNo is null)
            {
                QueueNo = "";
            }
            string data;
            //string xmlheader                    = ConvertHeaderToXML(   
            //                                                            PharmacyDataModel.header.EncounterId.ToString(),
            //                                                            PharmacyDataModel.header.OrganizationId,
            //                                                            PharmacyDataModel.header.Admissionid,
            //                                                            PharmacyDataModel.header.PatientId,
            //                                                            PharmacyDataModel.header.DoctorId,
            //                                                            PharmacyDataModel.header.PharmacyNotes,
            //                                                            PharmacyDataModel.header.IsEditDrug,
            //                                                            PharmacyDataModel.header.IsEditCompound,
            //                                                            PharmacyDataModel.header.IsEditConsumables,
            //                                                            false);
            string xmlheader = ConvertPharmacyHeaderSingleQToXML(
                                        PharmacyDataModel.header.EncounterId.ToString(),
                                        PharmacyDataModel.header.OrganizationId,
                                        PharmacyDataModel.header.Admissionid,
                                        PharmacyDataModel.header.PatientId,
                                        PharmacyDataModel.header.DoctorId,
                                        PharmacyDataModel.header.PharmacyNotes,
                                        PharmacyDataModel.header.IsEditDrug,
                                        PharmacyDataModel.header.IsEditCompound,
                                        PharmacyDataModel.header.IsEditConsumables,
                                        false,
                                        PharmacyDataModel.header.store_id,
                                        PharmacyDataModel.header.prefix_desc,
                                        PharmacyDataModel.header.is_tele,
                                        PharmacyDataModel.header.VerifyTime);
            string xmlprescription              = ConvertPrescriptionToXML(PharmacyDataModel.prescription);
            string xmldruginfo                  = ConverDrugInfoToXML(PharmacyDataModel.druginfo);
            string xmlcompoundheader            = ConvertCompoundHeaderToXML(PharmacyDataModel.compound_header);
            string xmlcompounddetail            = ConvertCompoundDetailToXML(PharmacyDataModel.compound_detail);
            string xmlsingleQ                   = ConverSingleQueueToXML(SubmitBy, PharmacyDataModel.header, PharmacyDataModel.singleQWorklistData, SingleQDataModel);
            string RequestWorklistSingleQueue   = JsonConvert.SerializeObject(PharmacyDataModel.singleQWorklistData);
            try
            {
                using (SqlConnection conn = new SqlConnection(Siloam.System.ApplicationSetting.ConnectionString))
                {
                    conn.Open();
                    SqlCommand cmd = conn.CreateCommand();
                    //cmd.CommandText = "spSUBMIT_ADDITIONALPRESCRIPTION";
                    cmd.CommandText = "spSUBMIT_ADDITIONALPRESCRIPTIONSQ";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("Header"                , xmlheader));
                    cmd.Parameters.Add(new SqlParameter("Prescription"          , xmlprescription));
                    cmd.Parameters.Add(new SqlParameter("CompoundHeader"        , xmlcompoundheader));
                    cmd.Parameters.Add(new SqlParameter("CompoundDetail"        , xmlcompounddetail));
                    cmd.Parameters.Add(new SqlParameter("DrugInfo"              , xmldruginfo));
                    cmd.Parameters.Add(new SqlParameter("SingleQ"               , xmlsingleQ));
                    cmd.Parameters.Add(new SqlParameter("Jsonrequest_singleQ"   , RequestWorklistSingleQueue));
                    cmd.Parameters.Add(new SqlParameter("Jsonresponse_singleQ"  , param_ReordModel.ResponseWorklistSingleQueue));
                    cmd.Parameters.Add(new SqlParameter("TransAdmId"            , TransAdmId));
                    cmd.Parameters.Add(new SqlParameter("TransAdmNo"            , TransAdmNo));
                    cmd.Parameters.Add(new SqlParameter("QueueNo"               , QueueNo));
                    cmd.Parameters.Add(new SqlParameter("Updater"               , SubmitBy));
                    using (var da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }
                    data = (from DataRow dr in dt.Rows
                            select dr["Result"].ToString()).Single();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return data;
        }

        public SubmitPrintPrescription submitPrintAdditionalPresscription(Param_RecordSubmit param_ReordModel, Param_SyncResult param_SyncResultModel)
        {

            DataSet dt                          = new DataSet();
            PharmacyData PharmacyDataModel      = param_ReordModel.PharmacyDataModel;
            SingleQueue SingleQDataModel        = param_ReordModel.SingleQueueDataModel;
            long SubmitBy                       = param_ReordModel.SubmitBy;
            long TransAdmId                     = param_ReordModel.TransAdmId;
            string TransAdmNo                   = param_ReordModel.TransAdmNo;
            string QueueNo                      = param_ReordModel.QueueNo;
            if (QueueNo is null)
            {
                QueueNo = "";
            }

            SubmitPrintPrescription data = new SubmitPrintPrescription();
            string xmlheader = ConvertPresscriptionToXML(
                                                                            PharmacyDataModel.header.EncounterId.ToString(),
                                                                            PharmacyDataModel.header.OrganizationId,
                                                                            PharmacyDataModel.header.Admissionid,
                                                                            PharmacyDataModel.header.PatientId,
                                                                            PharmacyDataModel.header.DoctorId,
                                                                            PharmacyDataModel.header.PharmacyNotes,
                                                                            PharmacyDataModel.header.IsEditDrug,
                                                                            PharmacyDataModel.header.IsEditCompound,
                                                                            PharmacyDataModel.header.IsEditConsumables,
                                                                            false,
                                                                            PharmacyDataModel.header.store_id,
                                                                            PharmacyDataModel.header.prefix_desc,
                                                                            PharmacyDataModel.header.is_tele,
                                                                            PharmacyDataModel.header.is_SingleQueue,
                                                                            PharmacyDataModel.header.is_SendDataItemIssue,
                                                                            PharmacyDataModel.header.VerifyTime,
                                                                            PharmacyDataModel.header.Admissionid_SentHope);
            string xmlprescription              = ConvertPrescriptionToXML(PharmacyDataModel.prescription);
            string xmldruginfo                  = ConverDrugInfoToXML(PharmacyDataModel.druginfo);
            string xmlcompoundheader            = ConvertCompoundHeaderToXML(PharmacyDataModel.compound_header);
            string xmlcompounddetail            = ConvertCompoundDetailHopeToXML(PharmacyDataModel.compound_detail);
            string xmlsingleQ                   = ConverSingleQueueToXML(SubmitBy, PharmacyDataModel.header, PharmacyDataModel.singleQWorklistData, SingleQDataModel);
            string RequestWorklistSingleQueue   = JsonConvert.SerializeObject(PharmacyDataModel.singleQWorklistData);
            string xmleditreason                = ConvertEditReasonToXML(PharmacyDataModel.editReason);
            string xmlappropriatnessreview      = ConvertAppropriatnessReviewToXML(PharmacyDataModel.appropriatnessReview);
            string xmladditionalitem            = ConvertAdditionalItemToXML(PharmacyDataModel.additionalItem);
            string xmleditedmapping             = ConvertEditedMappingToXML(PharmacyDataModel.pharmacyEditedMappings);

            try
            {
                using (SqlConnection conn = new SqlConnection(Siloam.System.ApplicationSetting.ConnectionString))
                {
                    conn.Open();
                    SqlCommand cmd = conn.CreateCommand();
                    cmd.CommandText = "spSUBMITPRINT_ADDITIONALPRESCRIPTION";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("Header", xmlheader));
                    cmd.Parameters.Add(new SqlParameter("Prescription", xmlprescription));
                    cmd.Parameters.Add(new SqlParameter("CompoundHeader", xmlcompoundheader));
                    cmd.Parameters.Add(new SqlParameter("CompoundDetail", xmlcompounddetail));
                    cmd.Parameters.Add(new SqlParameter("DrugInfo", xmldruginfo));
                    cmd.Parameters.Add(new SqlParameter("SingleQ", xmlsingleQ));
                    cmd.Parameters.Add(new SqlParameter("EditReason", xmleditreason));
                    cmd.Parameters.Add(new SqlParameter("Jsonrequest_singleQ", RequestWorklistSingleQueue));
                    cmd.Parameters.Add(new SqlParameter("Jsonresponse_singleQ", param_ReordModel.ResponseWorklistSingleQueue));
                    cmd.Parameters.Add(new SqlParameter("TransAdmId", TransAdmId));
                    cmd.Parameters.Add(new SqlParameter("TransAdmNo", TransAdmNo));
                    cmd.Parameters.Add(new SqlParameter("QueueNo", QueueNo));
                    cmd.Parameters.Add(new SqlParameter("Updater", SubmitBy));
                    cmd.Parameters.Add(new SqlParameter("Appropriatness", xmlappropriatnessreview));
                    cmd.Parameters.Add(new SqlParameter("AdditionalItem", xmladditionalitem));
                    cmd.Parameters.Add(new SqlParameter("EditedMapping", xmleditedmapping));
                    using (var da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }
                    data.SubmitStatusPressResult = (from DataRow dr in dt.Tables[0].Rows
                                                    select dr["Result"].ToString()).Single();

                    if (GetSettingCLMA(PharmacyDataModel.header.OrganizationId).ToUpper().Equals("TRUE"))
                    {
                        PharmacyPickingListHeader h = new PharmacyPickingListHeader();
                        h = (from DataRow dr in dt.Tables[1].Rows
                             select new PharmacyPickingListHeader
                             {
                                 EncounterId = Guid.Parse(dr["EncounterId"].ToString()),
                                 OrganizationId = long.Parse(dr["OrganizationId"].ToString()),
                                 PatientId = long.Parse(dr["PatientId"].ToString()),
                                 AdmissionId = long.Parse(dr["AdmissionId"].ToString()),
                                 AdmissionNo = dr["AdmissionNo"].ToString(),
                                 LocalMrNo = dr["LocalMrNo"].ToString(),
                                 PatientName = dr["PatientName"].ToString(),
                                 BirthDate = DateTime.Parse(dr["BirthDate"].ToString()).ToString("dd MMM yyyy"),
                                 Age = dr["Age"].ToString(),
                                 Gender = dr["Gender"].ToString(),
                                 DoctorName = dr["DoctorName"].ToString(),
                                 SpecialtyName = dr["SpecialtyName"].ToString(),
                                 PrescriptionDate = DateTime.Parse(dr["PrescriptionDate"].ToString()).ToString("dd MMM yyyy HH:mm"),
                                 PrescriptionNo = dr["PrescriptionNo"].ToString(),
                                 PayerName = dr["PayerName"].ToString(),
                                 QueueNo = dr["QueueNo"].ToString(),
                                 IsCOVID = bool.Parse(dr["IsCOVID"].ToString()),
                                 SipNo = dr["SipNo"].ToString(),
                                 PrintNumber = dr["PrintNumber"].ToString(),
                                 StoreName = dr["StoreName"].ToString(),
                                 IssueDate = dr["IssueDate"].ToString(),
                                 IssueBy = dr["IssueBy"].ToString(),
                                 IssueCode = dr["IssueCode"].ToString(),
                                 IssueAdmissionNo = dr["IssueAdmissionNo"].ToString(),
                                 DeliveryAddress = dr["DeliveryAddress"].ToString(),
                                 DeliveryFee = dr["DeliveryFee"].ToString(),
                                 DeliveryCourier = dr["DeliveryCourier"].ToString()
                             }).Single();

                        List<PharmacyPickingListPres> p = new List<PharmacyPickingListPres>();
                        p = (from DataRow dr in dt.Tables[2].Rows
                             select new PharmacyPickingListPres
                             {
                                 prescription_id = long.Parse(dr["prescription_id"].ToString()),
                                 item_id = long.Parse(dr["item_id"].ToString()),
                                 item_name = dr["item_name"].ToString(),
                                 quantity = dr["quantity"].ToString(),
                                 uom_id = long.Parse(dr["uom_id"].ToString()),
                                 uom_code = dr["uom_code"].ToString(),
                                 frequency_id = long.Parse(dr["frequency_id"].ToString()),
                                 frequency_code = dr["frequency_code"].ToString(),
                                 dosage_id = dr["dosage_id"].ToString(),
                                 dose_uom_id = long.Parse(dr["dose_uom_id"].ToString()),
                                 dose_uom = dr["dose_uom"].ToString(),
                                 dose_text = dr["dose_text"].ToString(),
                                 administration_route_id = long.Parse(dr["administration_route_id"].ToString()),
                                 administration_route_code = dr["administration_route_code"].ToString(),
                                 remarks = dr["remarks"].ToString(),
                                 iteration = int.Parse(dr["iteration"].ToString()),
                                 is_consumables = bool.Parse(dr["is_consumables"].ToString()),
                                 is_routine = bool.Parse(dr["is_routine"].ToString()),
                                 compound_id = Guid.Parse(dr["compound_id"].ToString()),
                                 compound_name = dr["compound_name"].ToString(),
                                 origin_prescription_id = long.Parse(dr["origin_prescription_id"].ToString()),
                                 RackName = dr["RackName"].ToString(),
                                 PrescriptionDate = DateTime.Parse(dr["PrescriptionDate"].ToString()).ToString("dd-MMM-yyyy"),
                                 IsDoseText = bool.Parse(dr["IsDoseText"].ToString()),
                                 IssuedQty = dr["IssuedQty"].ToString().Replace(",", "."),
                                 item_sequence = int.Parse(dr["item_sequence"].ToString()),
                                 editedId = Guid.Parse(dr["editedId"].ToString()),
                                 LocationDrug = dr["LocationDrug"].ToString()
                             }).ToList();

                        List<PharmacyPickingListAllergy> a = new List<PharmacyPickingListAllergy>();
                        a = (from DataRow dr in dt.Tables[3].Rows
                             select new PharmacyPickingListAllergy
                             {
                                 allergy = dr["allergy"].ToString(),
                                 reaction = dr["reaction"].ToString()
                             }).ToList();

                        List<PharmacyPickingListObjective> o = new List<PharmacyPickingListObjective>();
                        o = (from DataRow dr in dt.Tables[4].Rows
                             select new PharmacyPickingListObjective
                             {
                                 soap_mapping_id = Guid.Parse(dr["soap_mapping_id"].ToString()),
                                 soap_mapping_name = dr["soap_mapping_name"].ToString(),
                                 value = dr["value"].ToString()
                             }).ToList();

                        List<PharmacyPickingListCompoundHeader> ch = new List<PharmacyPickingListCompoundHeader>();
                        ch = (from DataRow dr in dt.Tables[5].Rows
                              select new PharmacyPickingListCompoundHeader
                              {
                                  prescription_compound_header_id = Guid.Parse(dr["prescription_compound_header_id"].ToString()),
                                  compound_name = dr["compound_name"].ToString(),
                                  quantity = dr["quantity"].ToString().Replace(",", "."),
                                  uom_id = long.Parse(dr["uom_id"].ToString()),
                                  uom_code = dr["uom_code"].ToString(),
                                  administration_frequency_id = long.Parse(dr["administration_frequency_id"].ToString()),
                                  frequency_code = dr["frequency_code"].ToString(),
                                  dose = dr["dose"].ToString().Replace(",", "."),
                                  dose_uom_id = long.Parse(dr["dose_uom_id"].ToString()),
                                  dose_uom = dr["dose_uom"].ToString(),
                                  administration_route_id = long.Parse(dr["administration_route_id"].ToString()),
                                  administration_route_code = dr["administration_route_code"].ToString(),
                                  administration_instruction = dr["administration_instruction"].ToString(),
                                  iter = int.Parse(dr["iter"].ToString()),
                                  item_sequence = short.Parse(dr["item_sequence"].ToString()),
                                  PrescriptionDate = DateTime.Parse(dr["PrescriptionDate"].ToString()).ToString("dd-MMM-yyyy"),
                                  dose_text = dr["dose_text"].ToString(),
                                  IsDoseText = bool.Parse(dr["IsDoseText"].ToString()),
                                  compound_note = dr["compound_note"].ToString(),
                                  IssuedQty = dr["IssuedQty"].ToString().Replace(",", ".")
                              }).ToList();

                        List<PharmacyPickingListCompoundDetail> cd = new List<PharmacyPickingListCompoundDetail>();
                        cd = (from DataRow dr in dt.Tables[6].Rows
                              select new PharmacyPickingListCompoundDetail
                              {
                                  prescription_compound_header_id = Guid.Parse(dr["prescription_compound_header_id"].ToString()),
                                  prescription_compound_detail_id = Guid.Parse(dr["prescription_compound_detail_id"].ToString()),
                                  quantity = dr["quantity"].ToString().Replace(",", "."),
                                  uom_id = long.Parse(dr["uom_id"].ToString()),
                                  uom_code = dr["uom_code"].ToString(),
                                  item_id = long.Parse(dr["item_id"].ToString()),
                                  item_name = dr["item_name"].ToString(),
                                  item_sequence = short.Parse(dr["item_sequence"].ToString()),
                                  RackName = dr["RackName"].ToString(),
                                  dose_uom_id = long.Parse(dr["dose_uom_id"].ToString()),
                                  dose = dr["dose"].ToString().Replace(",", "."),
                                  dose_text = dr["dose_text"].ToString(),
                                  IsDoseText = bool.Parse(dr["IsDoseText"].ToString()),
                                  dose_uom_code = dr["dose_uom_code"].ToString(),
                                  IssuedQty = dr["IssuedQty"].ToString().Replace(",", "."),
                                  editedId = Guid.Parse(dr["editedId"].ToString()),
                                  LocationDrug = dr["LocationDrug"].ToString()
                              }).ToList();

                        data.printHeader = new PharmacyPrintHeader();
                        data.printPres = new List<PharmacyPrintPres>();
                        data.printAllergy = new List<PharmacyPrintAllergy>();
                        data.printObjective = new List<PharmacyPrintObjective>();
                        data.printCompoundHeader = new List<PharmacyPrintCompoundHeader>();
                        data.printCompoundDetail = new List<PharmacyPrintCompoundDetail>();
                        data.pharmacyPickingList = new PharmacyPickingList();
                        data.pharmacyPickingList.pickingListHeader = h;
                        data.pharmacyPickingList.pickingListPres = p;
                        data.pharmacyPickingList.pickingListAllergy = a;
                        data.pharmacyPickingList.pickingListObjective = o;
                        data.pharmacyPickingList.pickingListCompoundHeader = ch;
                        data.pharmacyPickingList.pickingListCompoundDetail = cd;
                    }
                    else
                    {
                        PharmacyPrintHeader h = new PharmacyPrintHeader();
                        h = (from DataRow dr in dt.Tables[1].Rows
                             select new PharmacyPrintHeader
                             {
                                 EncounterId = Guid.Parse(dr["EncounterId"].ToString()),
                                 OrganizationId = long.Parse(dr["OrganizationId"].ToString()),
                                 PatientId = long.Parse(dr["PatientId"].ToString()),
                                 AdmissionId = long.Parse(dr["AdmissionId"].ToString()),
                                 AdmissionNo = dr["AdmissionNo"].ToString(),
                                 LocalMrNo = dr["LocalMrNo"].ToString(),
                                 PatientName = dr["PatientName"].ToString(),
                                 BirthDate = DateTime.Parse(dr["BirthDate"].ToString()).ToString("dd MMM yyyy"),
                                 Age = dr["Age"].ToString(),
                                 Gender = dr["Gender"].ToString(),
                                 DoctorName = dr["DoctorName"].ToString(),
                                 SpecialtyName = dr["SpecialtyName"].ToString(),
                                 PrescriptionDate = DateTime.Parse(dr["PrescriptionDate"].ToString()).ToString("dd MMM yyyy HH:mm"),
                                 PrescriptionNo = dr["PrescriptionNo"].ToString(),
                                 PayerName = dr["PayerName"].ToString(),
                                 QueueNo = dr["QueueNo"].ToString(),
                                 IsCOVID = bool.Parse(dr["IsCOVID"].ToString()),
                                 SipNo = dr["SipNo"].ToString(),
                                 PrintNumber = dr["PrintNumber"].ToString()
                             }).Single();

                        List<PharmacyPrintPres> p = new List<PharmacyPrintPres>();
                        p = (from DataRow dr in dt.Tables[2].Rows
                             select new PharmacyPrintPres
                             {
                                 prescription_id = long.Parse(dr["prescription_id"].ToString()),
                                 item_id = long.Parse(dr["item_id"].ToString()),
                                 item_name = dr["item_name"].ToString(),
                                 quantity = dr["quantity"].ToString(),
                                 uom_id = long.Parse(dr["uom_id"].ToString()),
                                 uom_code = dr["uom_code"].ToString(),
                                 frequency_id = long.Parse(dr["frequency_id"].ToString()),
                                 frequency_code = dr["frequency_code"].ToString(),
                                 dosage_id = dr["dosage_id"].ToString(),
                                 dose_uom_id = long.Parse(dr["dose_uom_id"].ToString()),
                                 dose_uom = dr["dose_uom"].ToString(),
                                 dose_text = dr["dose_text"].ToString(),
                                 administration_route_id = long.Parse(dr["administration_route_id"].ToString()),
                                 administration_route_code = dr["administration_route_code"].ToString(),
                                 remarks = dr["remarks"].ToString(),
                                 iteration = int.Parse(dr["iteration"].ToString()),
                                 is_consumables = bool.Parse(dr["is_consumables"].ToString()),
                                 is_routine = bool.Parse(dr["is_routine"].ToString()),
                                 compound_id = Guid.Parse(dr["compound_id"].ToString()),
                                 compound_name = dr["compound_name"].ToString(),
                                 origin_prescription_id = long.Parse(dr["origin_prescription_id"].ToString()),
                                 RackName = dr["RackName"].ToString(),
                                 PrescriptionDate = DateTime.Parse(dr["PrescriptionDate"].ToString()).ToString("dd-MMM-yyyy"),
                                 IsDoseText = bool.Parse(dr["IsDoseText"].ToString()),
                                 IssuedQty = dr["IssuedQty"].ToString().Replace(",", ".")
                             }).ToList();

                        List<PharmacyPrintAllergy> a = new List<PharmacyPrintAllergy>();
                        a = (from DataRow dr in dt.Tables[3].Rows
                             select new PharmacyPrintAllergy
                             {
                                 allergy = dr["allergy"].ToString(),
                                 reaction = dr["reaction"].ToString()
                             }).ToList();

                        List<PharmacyPrintObjective> o = new List<PharmacyPrintObjective>();
                        o = (from DataRow dr in dt.Tables[4].Rows
                             select new PharmacyPrintObjective
                             {
                                 soap_mapping_id = Guid.Parse(dr["soap_mapping_id"].ToString()),
                                 soap_mapping_name = dr["soap_mapping_name"].ToString(),
                                 value = dr["value"].ToString()
                             }).ToList();

                        List<PharmacyPrintCompoundHeader> ch = new List<PharmacyPrintCompoundHeader>();
                        ch = (from DataRow dr in dt.Tables[5].Rows
                              select new PharmacyPrintCompoundHeader
                              {
                                  prescription_compound_header_id = Guid.Parse(dr["prescription_compound_header_id"].ToString()),
                                  compound_name = dr["compound_name"].ToString(),
                                  quantity = dr["quantity"].ToString().Replace(",", "."),
                                  uom_id = long.Parse(dr["uom_id"].ToString()),
                                  uom_code = dr["uom_code"].ToString(),
                                  administration_frequency_id = long.Parse(dr["administration_frequency_id"].ToString()),
                                  frequency_code = dr["frequency_code"].ToString(),
                                  dose = dr["dose"].ToString().Replace(",", "."),
                                  dose_uom_id = long.Parse(dr["dose_uom_id"].ToString()),
                                  dose_uom = dr["dose_uom"].ToString(),
                                  administration_route_id = long.Parse(dr["administration_route_id"].ToString()),
                                  administration_route_code = dr["administration_route_code"].ToString(),
                                  administration_instruction = dr["administration_instruction"].ToString(),
                                  iter = int.Parse(dr["iter"].ToString()),
                                  item_sequence = short.Parse(dr["item_sequence"].ToString()),
                                  PrescriptionDate = DateTime.Parse(dr["PrescriptionDate"].ToString()).ToString("dd-MMM-yyyy"),
                                  dose_text = dr["dose_text"].ToString(),
                                  IsDoseText = bool.Parse(dr["IsDoseText"].ToString()),
                                  compound_note = dr["compound_note"].ToString(),
                                  IssuedQty = dr["IssuedQty"].ToString().Replace(",", ".")
                              }).ToList();

                        List<PharmacyPrintCompoundDetail> cd = new List<PharmacyPrintCompoundDetail>();
                        cd = (from DataRow dr in dt.Tables[6].Rows
                              select new PharmacyPrintCompoundDetail
                              {
                                  prescription_compound_header_id = Guid.Parse(dr["prescription_compound_header_id"].ToString()),
                                  prescription_compound_detail_id = Guid.Parse(dr["prescription_compound_detail_id"].ToString()),
                                  quantity = dr["quantity"].ToString().Replace(",", "."),
                                  uom_id = long.Parse(dr["uom_id"].ToString()),
                                  uom_code = dr["uom_code"].ToString(),
                                  item_id = long.Parse(dr["item_id"].ToString()),
                                  item_name = dr["item_name"].ToString(),
                                  item_sequence = short.Parse(dr["item_sequence"].ToString()),
                                  RackName = dr["RackName"].ToString(),
                                  dose_uom_id = long.Parse(dr["dose_uom_id"].ToString()),
                                  dose = dr["dose"].ToString().Replace(",", "."),
                                  dose_text = dr["dose_text"].ToString(),
                                  IsDoseText = bool.Parse(dr["IsDoseText"].ToString()),
                                  dose_uom_code = dr["dose_uom_code"].ToString()
                              }).ToList();

                        data.printHeader = h;
                        data.printPres = p;
                        data.printAllergy = a;
                        data.printObjective = o;
                        data.printCompoundHeader = ch;
                        data.printCompoundDetail = cd;
                        data.pharmacyPickingList = new PharmacyPickingList();
                    }

                    data.HopeStatusResult = param_SyncResultModel.HopeStatusResult;
                    data.HopeMessageResult = param_SyncResultModel.HopeMessageResult;
                    data.HopedataResult = param_SyncResultModel.HopedataResult;
                    data.SingleQStatusResult = param_SyncResultModel.SingleQStatusResult;
                    data.SingleQMessageResult = param_SyncResultModel.SingleQMessageResult;
                    
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return data;
        }

        public string UpdateAdditionalDrugInfo(long OrganizationId, long AdmissionId, Guid EncounterId, long Updater, List<PharmacyDrugInfo> model)
        {
            DataTable dt = new DataTable();
            string data;
            string xmldruginfo = ConverDrugInfoToXML(model);
            try
            {
                using (SqlConnection conn = new SqlConnection(Siloam.System.ApplicationSetting.ConnectionString))
                {
                    conn.Open();
                    SqlCommand cmd = conn.CreateCommand();
                    cmd.CommandText = "spSUBMIT_ADDITIONALDRUGINFO";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("EncounterId", EncounterId));
                    cmd.Parameters.Add(new SqlParameter("AdmissionId", AdmissionId));
                    cmd.Parameters.Add(new SqlParameter("OrganizationId", OrganizationId));
                    cmd.Parameters.Add(new SqlParameter("DrugInfo", xmldruginfo));
                    cmd.Parameters.Add(new SqlParameter("Updater", Updater));
                    using (var da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }
                    data = (from DataRow dr in dt.Rows
                            select dr["Result"].ToString()).Single();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return data;
        }

        public List<PharmacyIssue> GetDataPharmacyIssue(Int64 StoreId, Int64 AdmissionId, Int64 OrganizationId)
        {
            DataTable dt = new DataTable();
            List<PharmacyIssue> data = new List<PharmacyIssue>();
            try
            {
                using (SqlConnection conn = new SqlConnection(Siloam.System.ApplicationSetting.ConnectionString))
                {
                    conn.Open();
                    SqlCommand cmd = conn.CreateCommand();
                    cmd.CommandText = "spGET_PHARMACYISSUE";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("StoreId", StoreId));
                    cmd.Parameters.Add(new SqlParameter("AdmissionId", AdmissionId));
                    cmd.Parameters.Add(new SqlParameter("OrganizationId", OrganizationId));
                    using (var da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }
                    data = (from DataRow dr in dt.Rows
                            select new PharmacyIssue
                            {
                                PresOrganizationId = Int64.Parse(dr["PresOrganizationId"].ToString()),
                                PresAdmissionId = Int64.Parse(dr["PresAdmissionId"].ToString()),
                                TransOrganizationId = Int64.Parse(dr["TransOrganizationId"].ToString()),
                                EmrPrescriptionId = Guid.Parse(dr["EmrPrescriptionId"].ToString()),
                                PrescriptionId = Int64.Parse(dr["PrescriptionId"].ToString()),
                                AdditionalId = Int64.Parse(dr["AdditionalId"].ToString()),
                                StockQuantity = dr["StockQuantity"].ToString(),
                                ItemId = Int64.Parse(dr["ItemId"].ToString()),
                                ItemCode = dr["ItemCode"].ToString(),
                                ItemName = dr["ItemName"].ToString(),
                                PresQuantity = dr["PresQuantity"].ToString(),
                                Iter = int.Parse(dr["Iter"].ToString()),
                                OutstandingQuantity = dr["OutstandingQuantity"].ToString(),
                                IssuedQuantity = dr["IssuedQuantity"].ToString(),
                                UomId = Int64.Parse(dr["UomId"].ToString()),
                                UomCode = dr["UomCode"].ToString(),
                                FrequencyId = Int64.Parse(dr["FrequencyId"].ToString()),
                                FrequencyCode = dr["FrequencyCode"].ToString(),
                                RouteId = Int64.Parse(dr["RouteId"].ToString()),
                                RouteCode = dr["RouteCode"].ToString(),
                                DosageId = dr["DosageId"].ToString(),
                                dose_uom_id = long.Parse(dr["dose_uom_id"].ToString()),
                                dose_uom = dr["dose_uom"].ToString(),
                                DoseText = dr["RouteCode"].ToString(),
                                Remarks = dr["Remarks"].ToString(),
                                IsRoutine = Boolean.Parse(dr["IsRoutine"].ToString()),
                                IsConsumables = Boolean.Parse(dr["IsConsumables"].ToString()),
                                CompoundId = Guid.Parse(dr["CompoundId"].ToString()),
                                CompoundName = dr["CompoundName"].ToString(),
                                DoctorId = Int64.Parse(dr["DoctorId"].ToString()),
                                DoctorName = dr["DoctorName"].ToString(),
                                ItemPrice = dr["ItemPrice"].ToString(),
                                SubTotalPrice = dr["SubTotalPrice"].ToString(),
                                Notes = dr["Notes"].ToString(),
                                IsAdditional = int.Parse(dr["IsAdditional"].ToString()),
                                IsHistory = int.Parse(dr["IsHistory"].ToString()),
                                IsNew = int.Parse(dr["IsNew"].ToString()),
                                ConnStatus = dr["ConnStatus"].ToString()
                            }).ToList();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return data;
        }

        public string InsertPharmacyTransaction(Int64 Updater, Int64 StoreId, Int64 TransAdmissionId, List<PharmacyIssue> Model)
        {
            DataTable dt = new DataTable();
            string data;
            string xmlpharmacy = ConvertAdditionalPrescriptionToXML(Model);
            try
            {
                using (SqlConnection conn = new SqlConnection(Siloam.System.ApplicationSetting.ConnectionString))
                {
                    conn.Open();
                    SqlCommand cmd = conn.CreateCommand();
                    cmd.CommandText = "spSUBMIT_TRANSACTION";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("Updater", Updater));
                    cmd.Parameters.Add(new SqlParameter("StoreId", StoreId));
                    cmd.Parameters.Add(new SqlParameter("TransAdmissionId", TransAdmissionId));
                    cmd.Parameters.Add(new SqlParameter("Pharmacy", xmlpharmacy));
                    using (var da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }
                    data = (from DataRow dr in dt.Rows
                            select dr["Result"].ToString()).Single();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return data;
        }

        public List<PharmacyTransactionHistory> GetPharmacyTransactionHistory(Int64 PrescriptionId, int Mode)
        {
            List<PharmacyTransactionHistory> data = new List<PharmacyTransactionHistory>();
            try
            {
                using (SqlConnection conn = new SqlConnection(Siloam.System.ApplicationSetting.ConnectionString))
                {
                    conn.Open();
                    SqlCommand cmd = conn.CreateCommand();
                    cmd.CommandText = "spGET_TRANSACTIONHISTORY";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("PrescriptionId", PrescriptionId));
                    cmd.Parameters.Add(new SqlParameter("Mode", Mode));
                    using (var reader = cmd.ExecuteReader())
                    {
                        data = reader.MapToList<PharmacyTransactionHistory>();
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return data;
        }

        public List<PageTransactionHistory> GetPageTransactionHistory(long PatientId, string OrganizationCode, string PresRegNo, string TransRegNo, string DoctorName, string PayerName, Nullable<DateTime> DateFrom, Nullable<DateTime> DateTo)
        {
            List<PageTransactionHistory> data = new List<PageTransactionHistory>();
            try
            {
                using (SqlConnection conn = new SqlConnection(Siloam.System.ApplicationSetting.ConnectionString))
                {
                    conn.Open();
                    SqlCommand cmd = conn.CreateCommand();
                    cmd.CommandText = "spGET_TRANSHISTORYPAGE";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("PatientId", PatientId));
                    cmd.Parameters.Add(new SqlParameter("OrganizationCode", OrganizationCode));
                    cmd.Parameters.Add(new SqlParameter("PresRegNo", PresRegNo));
                    cmd.Parameters.Add(new SqlParameter("TransRegNo", TransRegNo));
                    cmd.Parameters.Add(new SqlParameter("DoctorName", DoctorName));
                    cmd.Parameters.Add(new SqlParameter("PayerName", PayerName));
                    cmd.Parameters.Add(new SqlParameter("DateFrom", DateFrom));
                    cmd.Parameters.Add(new SqlParameter("DateTo", DateTo));
                    using (var reader = cmd.ExecuteReader())
                    {
                        data = reader.MapToList<PageTransactionHistory>();
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return data;
        }

        public PharmacyPrintPrescription GetPrintPrescription(long OrganizationId, long PatientId, long AdmissionId, string EncounterId, long StoreId)
        {
            DataSet dt = new DataSet();
            PharmacyPrintPrescription data = new PharmacyPrintPrescription();
            try
            {
                using (SqlConnection conn = new SqlConnection(Siloam.System.ApplicationSetting.ConnectionString))
                {
                    conn.Open();
                    SqlCommand cmd = conn.CreateCommand();
                    cmd.CommandText = "spGET_PRINTPRESCRIPTION";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("OrganizationId", OrganizationId));
                    cmd.Parameters.Add(new SqlParameter("PatientId", PatientId));
                    cmd.Parameters.Add(new SqlParameter("AdmissionId", AdmissionId));
                    cmd.Parameters.Add(new SqlParameter("EncounterId", EncounterId));
                    cmd.Parameters.Add(new SqlParameter("StoreId", StoreId));
                    using (var da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }

                    PharmacyPrintHeader h = new PharmacyPrintHeader();
                    h = (from DataRow dr in dt.Tables[0].Rows
                            select new PharmacyPrintHeader
                            {
                                EncounterId = Guid.Parse(dr["EncounterId"].ToString()),
                                OrganizationId = long.Parse(dr["OrganizationId"].ToString()),
                                PatientId = long.Parse(dr["PatientId"].ToString()),
                                AdmissionId = long.Parse(dr["AdmissionId"].ToString()),
                                AdmissionNo = dr["AdmissionNo"].ToString(),
                                LocalMrNo = dr["LocalMrNo"].ToString(),
                                PatientName = dr["PatientName"].ToString(),
                                BirthDate = DateTime.Parse(dr["BirthDate"].ToString()).ToString("dd MMM yyyy"),
                                Age = dr["Age"].ToString(),
                                Gender = dr["Gender"].ToString(),
                                DoctorName = dr["DoctorName"].ToString(),
                                SpecialtyName = dr["SpecialtyName"].ToString(),
                                PrescriptionDate = DateTime.Parse(dr["PrescriptionDate"].ToString()).ToString("dd MMM yyyy HH:mm"),
                                PrescriptionNo = dr["PrescriptionNo"].ToString(),
                                PayerName = dr["PayerName"].ToString(),
                                QueueNo = dr["QueueNo"].ToString(),
                                IsCOVID = bool.Parse(dr["IsCOVID"].ToString()),
                                SipNo = dr["SipNo"].ToString(),
                                PrintNumber = dr["PrintNumber"].ToString()
                            }).Single();

                    List<PharmacyPrintPres> p = new List<PharmacyPrintPres>();
                    p = (from DataRow dr in dt.Tables[1].Rows
                         select new PharmacyPrintPres
                         {
                             prescription_id = long.Parse(dr["prescription_id"].ToString()),
                             item_id = long.Parse(dr["item_id"].ToString()),
                             item_name = dr["item_name"].ToString(),
                             quantity = dr["quantity"].ToString(),
                             uom_id = long.Parse(dr["uom_id"].ToString()),
                             uom_code = dr["uom_code"].ToString(),
                             frequency_id = long.Parse(dr["frequency_id"].ToString()),
                             frequency_code = dr["frequency_code"].ToString(),
                             dosage_id = dr["dosage_id"].ToString(),
                             dose_uom_id = long.Parse(dr["dose_uom_id"].ToString()),
                             dose_uom = dr["dose_uom"].ToString(),
                             dose_text = dr["dose_text"].ToString(),
                             administration_route_id = long.Parse(dr["administration_route_id"].ToString()),
                             administration_route_code = dr["administration_route_code"].ToString(),
                             remarks = dr["remarks"].ToString(),
                             iteration = int.Parse(dr["iteration"].ToString()),
                             is_consumables = bool.Parse(dr["is_consumables"].ToString()),
                             is_routine = bool.Parse(dr["is_routine"].ToString()),
                             compound_id = Guid.Parse(dr["compound_id"].ToString()),
                             compound_name = dr["compound_name"].ToString(),
                             origin_prescription_id = long.Parse(dr["origin_prescription_id"].ToString()),
                             RackName = dr["RackName"].ToString(),
                             PrescriptionDate = DateTime.Parse(dr["PrescriptionDate"].ToString()).ToString("dd-MMM-yyyy"),
                             IsDoseText = bool.Parse(dr["IsDoseText"].ToString()),
                             IssuedQty = dr["IssuedQty"].ToString().Replace(",", "."),
                             item_sequence = int.Parse(dr["item_sequence"].ToString())
                         }).ToList();

                    List<PharmacyPrintAllergy> a = new List<PharmacyPrintAllergy>();
                    a = (from DataRow dr in dt.Tables[2].Rows
                         select new PharmacyPrintAllergy
                         {
                             allergy = dr["allergy"].ToString(),
                             reaction = dr["reaction"].ToString()
                         }).ToList();

                    List<PharmacyPrintObjective> o = new List<PharmacyPrintObjective>();
                    o = (from DataRow dr in dt.Tables[3].Rows
                         select new PharmacyPrintObjective
                         {
                             soap_mapping_id = Guid.Parse(dr["soap_mapping_id"].ToString()),
                             soap_mapping_name = dr["soap_mapping_name"].ToString(),
                             value = dr["value"].ToString()
                         }).ToList();

                    List<PharmacyPrintCompoundHeader> ch = new List<PharmacyPrintCompoundHeader>();
                    ch = (from DataRow dr in dt.Tables[4].Rows
                          select new PharmacyPrintCompoundHeader
                          {
                              prescription_compound_header_id = Guid.Parse(dr["prescription_compound_header_id"].ToString()),
                              compound_name = dr["compound_name"].ToString(),
                              quantity = dr["quantity"].ToString().Replace(",", "."),
                              uom_id = long.Parse(dr["uom_id"].ToString()),
                              uom_code = dr["uom_code"].ToString(),
                              administration_frequency_id = long.Parse(dr["administration_frequency_id"].ToString()),
                              frequency_code = dr["frequency_code"].ToString(),
                              dose = dr["dose"].ToString().Replace(",", "."),
                              dose_uom_id = long.Parse(dr["dose_uom_id"].ToString()),
                              dose_uom = dr["dose_uom"].ToString(),
                              administration_route_id = long.Parse(dr["administration_route_id"].ToString()),
                              administration_route_code = dr["administration_route_code"].ToString(),
                              administration_instruction = dr["administration_instruction"].ToString(),
                              iter = int.Parse(dr["iter"].ToString()),
                              item_sequence = short.Parse(dr["item_sequence"].ToString()),
                              PrescriptionDate = DateTime.Parse(dr["PrescriptionDate"].ToString()).ToString("dd-MMM-yyyy"),
                              dose_text = dr["dose_text"].ToString(),
                              IsDoseText = bool.Parse(dr["IsDoseText"].ToString()),
                              compound_note = dr["compound_note"].ToString(),
                              IssuedQty = dr["IssuedQty"].ToString().Replace(",", ".")
                          }).ToList();

                    List<PharmacyPrintCompoundDetail> cd = new List<PharmacyPrintCompoundDetail>();
                    cd = (from DataRow dr in dt.Tables[5].Rows
                          select new PharmacyPrintCompoundDetail
                          {
                              prescription_compound_header_id = Guid.Parse(dr["prescription_compound_header_id"].ToString()),
                              prescription_compound_detail_id = Guid.Parse(dr["prescription_compound_detail_id"].ToString()),
                              quantity = dr["quantity"].ToString().Replace(",", "."),
                              uom_id = long.Parse(dr["uom_id"].ToString()),
                              uom_code = dr["uom_code"].ToString(),
                              item_id = long.Parse(dr["item_id"].ToString()),
                              item_name = dr["item_name"].ToString(),
                              item_sequence = short.Parse(dr["item_sequence"].ToString()),
                              RackName = dr["RackName"].ToString(),
                              dose_uom_id = long.Parse(dr["dose_uom_id"].ToString()),
                              dose = dr["dose"].ToString().Replace(",", "."),
                              dose_text = dr["dose_text"].ToString(),
                              IsDoseText = bool.Parse(dr["IsDoseText"].ToString()),
                              dose_uom_code = dr["dose_uom_code"].ToString()
                          }).ToList();

                    data.printHeader = h;
                    data.printPres = p;
                    data.printAllergy = a;
                    data.printObjective = o;
                    data.printCompoundHeader = ch;
                    data.printCompoundDetail = cd;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return data;
        }

        public PharmacyPrintPrescription GetPrintAdditionalPrescription(long OrganizationId, long PatientId, long AdmissionId, string EncounterId, long StoreId)
        {
            DataSet dt = new DataSet();
            PharmacyPrintPrescription data = new PharmacyPrintPrescription();
            try
            {
                using (SqlConnection conn = new SqlConnection(Siloam.System.ApplicationSetting.ConnectionString))
                {
                    conn.Open();
                    SqlCommand cmd = conn.CreateCommand();
                    cmd.CommandText = "spGET_PRINTADDITIONALPRESCRIPTION";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("OrganizationId", OrganizationId));
                    cmd.Parameters.Add(new SqlParameter("PatientId", PatientId));
                    cmd.Parameters.Add(new SqlParameter("AdmissionId", AdmissionId));
                    cmd.Parameters.Add(new SqlParameter("EncounterId", EncounterId));
                    cmd.Parameters.Add(new SqlParameter("StoreId", StoreId));
                    using (var da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }

                    PharmacyPrintHeader h = new PharmacyPrintHeader();
                    h = (from DataRow dr in dt.Tables[0].Rows
                         select new PharmacyPrintHeader
                         {
                             EncounterId = Guid.Parse(dr["EncounterId"].ToString()),
                             OrganizationId = long.Parse(dr["OrganizationId"].ToString()),
                             PatientId = long.Parse(dr["PatientId"].ToString()),
                             AdmissionId = long.Parse(dr["AdmissionId"].ToString()),
                             AdmissionNo = dr["AdmissionNo"].ToString(),
                             LocalMrNo = dr["LocalMrNo"].ToString(),
                             PatientName = dr["PatientName"].ToString(),
                             BirthDate = DateTime.Parse(dr["BirthDate"].ToString()).ToString("dd MMM yyyy"),
                             Age = dr["Age"].ToString(),
                             Gender = dr["Gender"].ToString(),
                             DoctorName = dr["DoctorName"].ToString(),
                             SpecialtyName = dr["SpecialtyName"].ToString(),
                             PrescriptionDate = DateTime.Parse(dr["PrescriptionDate"].ToString()).ToString("dd MMM yyyy HH:mm"),
                             PrescriptionNo = dr["PrescriptionNo"].ToString(),
                             PayerName = dr["PayerName"].ToString(),
                             QueueNo = dr["QueueNo"].ToString(),
                             IsCOVID = bool.Parse(dr["IsCOVID"].ToString()),
                             SipNo = dr["SipNo"].ToString(),
                             PrintNumber = dr["PrintNumber"].ToString()
                         }).Single();

                    List<PharmacyPrintPres> p = new List<PharmacyPrintPres>();
                    p = (from DataRow dr in dt.Tables[1].Rows
                         select new PharmacyPrintPres
                         {
                             prescription_id = long.Parse(dr["prescription_id"].ToString()),
                             item_id = long.Parse(dr["item_id"].ToString()),
                             item_name = dr["item_name"].ToString(),
                             quantity = dr["quantity"].ToString(),
                             uom_id = long.Parse(dr["uom_id"].ToString()),
                             uom_code = dr["uom_code"].ToString(),
                             frequency_id = long.Parse(dr["frequency_id"].ToString()),
                             frequency_code = dr["frequency_code"].ToString(),
                             dosage_id = dr["dosage_id"].ToString(),
                             dose_uom_id = long.Parse(dr["dose_uom_id"].ToString()),
                             dose_uom = dr["dose_uom"].ToString(),
                             dose_text = dr["dose_text"].ToString(),
                             administration_route_id = long.Parse(dr["administration_route_id"].ToString()),
                             administration_route_code = dr["administration_route_code"].ToString(),
                             remarks = dr["remarks"].ToString(),
                             iteration = int.Parse(dr["iteration"].ToString()),
                             is_consumables = bool.Parse(dr["is_consumables"].ToString()),
                             is_routine = bool.Parse(dr["is_routine"].ToString()),
                             compound_id = Guid.Parse(dr["compound_id"].ToString()),
                             compound_name = dr["compound_name"].ToString(),
                             origin_prescription_id = long.Parse(dr["origin_prescription_id"].ToString()),
                             RackName = dr["RackName"].ToString(),
                             PrescriptionDate = DateTime.Parse(dr["PrescriptionDate"].ToString()).ToString("dd-MMM-yyyy"),
                             IsDoseText = bool.Parse(dr["IsDoseText"].ToString()),
                             IssuedQty = dr["IssuedQty"].ToString().Replace(",", "."),
                             item_sequence = int.Parse(dr["item_sequence"].ToString())
                         }).ToList();

                    List<PharmacyPrintAllergy> a = new List<PharmacyPrintAllergy>();
                    a = (from DataRow dr in dt.Tables[2].Rows
                         select new PharmacyPrintAllergy
                         {
                             allergy = dr["allergy"].ToString(),
                             reaction = dr["reaction"].ToString()
                         }).ToList();

                    List<PharmacyPrintObjective> o = new List<PharmacyPrintObjective>();
                    o = (from DataRow dr in dt.Tables[3].Rows
                         select new PharmacyPrintObjective
                         {
                             soap_mapping_id = Guid.Parse(dr["soap_mapping_id"].ToString()),
                             soap_mapping_name = dr["soap_mapping_name"].ToString(),
                             value = dr["value"].ToString()
                         }).ToList();

                    List<PharmacyPrintCompoundHeader> ch = new List<PharmacyPrintCompoundHeader>();
                    ch = (from DataRow dr in dt.Tables[4].Rows
                          select new PharmacyPrintCompoundHeader
                          {
                              prescription_compound_header_id = Guid.Parse(dr["prescription_compound_header_id"].ToString()),
                              compound_name = dr["compound_name"].ToString(),
                              quantity = dr["quantity"].ToString().Replace(",", "."),
                              uom_id = long.Parse(dr["uom_id"].ToString()),
                              uom_code = dr["uom_code"].ToString(),
                              administration_frequency_id = long.Parse(dr["administration_frequency_id"].ToString()),
                              frequency_code = dr["frequency_code"].ToString(),
                              dose = dr["dose"].ToString().Replace(",", "."),
                              dose_uom_id = long.Parse(dr["dose_uom_id"].ToString()),
                              dose_uom = dr["dose_uom"].ToString(),
                              administration_route_id = long.Parse(dr["administration_route_id"].ToString()),
                              administration_route_code = dr["administration_route_code"].ToString(),
                              administration_instruction = dr["administration_instruction"].ToString(),
                              iter = int.Parse(dr["iter"].ToString()),
                              item_sequence = short.Parse(dr["item_sequence"].ToString()),
                              PrescriptionDate = DateTime.Parse(dr["PrescriptionDate"].ToString()).ToString("dd-MMM-yyyy"),
                              dose_text = dr["dose_text"].ToString(),
                              IsDoseText = bool.Parse(dr["IsDoseText"].ToString()),
                              compound_note = dr["compound_note"].ToString(),
                              IssuedQty = dr["IssuedQty"].ToString().Replace(",", ".")
                          }).ToList();

                    List<PharmacyPrintCompoundDetail> cd = new List<PharmacyPrintCompoundDetail>();
                    cd = (from DataRow dr in dt.Tables[5].Rows
                          select new PharmacyPrintCompoundDetail
                          {
                              prescription_compound_header_id = Guid.Parse(dr["prescription_compound_header_id"].ToString()),
                              prescription_compound_detail_id = Guid.Parse(dr["prescription_compound_detail_id"].ToString()),
                              quantity = dr["quantity"].ToString().Replace(",", "."),
                              uom_id = long.Parse(dr["uom_id"].ToString()),
                              uom_code = dr["uom_code"].ToString(),
                              item_id = long.Parse(dr["item_id"].ToString()),
                              item_name = dr["item_name"].ToString(),
                              item_sequence = short.Parse(dr["item_sequence"].ToString()),
                              RackName = dr["RackName"].ToString(),
                              dose_uom_id = long.Parse(dr["dose_uom_id"].ToString()),
                              dose = dr["dose"].ToString().Replace(",", "."),
                              dose_text = dr["dose_text"].ToString(),
                              IsDoseText = bool.Parse(dr["IsDoseText"].ToString()),
                              dose_uom_code = dr["dose_uom_code"].ToString()
                          }).ToList();


                    data.printHeader = h;
                    data.printPres = p;
                    data.printAllergy = a;
                    data.printObjective = o;
                    data.printCompoundHeader = ch;
                    data.printCompoundDetail = cd;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return data;
        }

        public string UnverifyPharmacyData(long OrganizationId, long PatientId, long AdmissionId, string EncounterId, string Remarks, long Updater)
        {
            DataTable dt = new DataTable();
            string data;
            try
            {
                using (SqlConnection conn = new SqlConnection(Siloam.System.ApplicationSetting.ConnectionString))
                {
                    conn.Open();
                    SqlCommand cmd = conn.CreateCommand();
                    cmd.CommandText = "spSUBMIT_UNVERIFY";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("OrganizationId", OrganizationId));
                    cmd.Parameters.Add(new SqlParameter("PatientId", PatientId));
                    cmd.Parameters.Add(new SqlParameter("AdmissionId", AdmissionId));
                    cmd.Parameters.Add(new SqlParameter("EncounterId", EncounterId));
                    cmd.Parameters.Add(new SqlParameter("Remarks", Remarks));
                    cmd.Parameters.Add(new SqlParameter("Updater", Updater));
                    using (var da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }
                    data = (from DataRow dr in dt.Rows
                            select dr["Result"].ToString()).Single();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return data;
        }

        public string UnverifyAdditionalPharmacyData(long OrganizationId, long PatientId, long AdmissionId, string EncounterId, string Remarks, long Updater)
        {
            DataTable dt = new DataTable();
            string data;
            try
            {
                using (SqlConnection conn = new SqlConnection(Siloam.System.ApplicationSetting.ConnectionString))
                {
                    conn.Open();
                    SqlCommand cmd = conn.CreateCommand();
                    cmd.CommandText = "spSUBMIT_ADDITIONALUNVERIFY";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("OrganizationId", OrganizationId));
                    cmd.Parameters.Add(new SqlParameter("PatientId", PatientId));
                    cmd.Parameters.Add(new SqlParameter("AdmissionId", AdmissionId));
                    cmd.Parameters.Add(new SqlParameter("EncounterId", EncounterId));
                    cmd.Parameters.Add(new SqlParameter("Remarks", Remarks));
                    cmd.Parameters.Add(new SqlParameter("Updater", Updater));
                    using (var da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }
                    data = (from DataRow dr in dt.Rows
                            select dr["Result"].ToString()).Single();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return data;
        }

        public PharmacyPickingList GetPickingList(long OrganizationId, long PatientId, long AdmissionId, string EncounterId, long StoreId, bool IsAdditional, string IssueCode, string SpecificCode)
        {
            DataSet dt = new DataSet();
            PharmacyPickingList data = new PharmacyPickingList();
            try
            {
                using (SqlConnection conn = new SqlConnection(Siloam.System.ApplicationSetting.ConnectionString))
                {
                    conn.Open();
                    SqlCommand cmd = conn.CreateCommand();
                    cmd.CommandText = "spGET_PRINTPICKLIST";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("OrganizationId", OrganizationId));
                    cmd.Parameters.Add(new SqlParameter("PatientId", PatientId));
                    cmd.Parameters.Add(new SqlParameter("AdmissionId", AdmissionId));
                    cmd.Parameters.Add(new SqlParameter("EncounterId", EncounterId));
                    cmd.Parameters.Add(new SqlParameter("StoreId", StoreId));
                    cmd.Parameters.Add(new SqlParameter("IsAdditional", IsAdditional));
                    cmd.Parameters.Add(new SqlParameter("IssueCode", IssueCode));
                    cmd.Parameters.Add(new SqlParameter("SpecificCode", SpecificCode));
                    using (var da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }

                    PharmacyPickingListHeader h = new PharmacyPickingListHeader();
                    h = (from DataRow dr in dt.Tables[0].Rows
                         select new PharmacyPickingListHeader
                         {
                             EncounterId = Guid.Parse(dr["EncounterId"].ToString()),
                             OrganizationId = long.Parse(dr["OrganizationId"].ToString()),
                             PatientId = long.Parse(dr["PatientId"].ToString()),
                             AdmissionId = long.Parse(dr["AdmissionId"].ToString()),
                             AdmissionNo = dr["AdmissionNo"].ToString(),
                             LocalMrNo = dr["LocalMrNo"].ToString(),
                             PatientName = dr["PatientName"].ToString(),
                             BirthDate = DateTime.Parse(dr["BirthDate"].ToString()).ToString("dd MMM yyyy"),
                             Age = dr["Age"].ToString(),
                             Gender = dr["Gender"].ToString(),
                             DoctorName = dr["DoctorName"].ToString(),
                             SpecialtyName = dr["SpecialtyName"].ToString(),
                             PrescriptionDate = DateTime.Parse(dr["PrescriptionDate"].ToString()).ToString("dd MMM yyyy HH:mm"),
                             PrescriptionNo = dr["PrescriptionNo"].ToString(),
                             PayerName = dr["PayerName"].ToString(),
                             QueueNo = dr["QueueNo"].ToString(),
                             IsCOVID = bool.Parse(dr["IsCOVID"].ToString()),
                             SipNo = dr["SipNo"].ToString(),
                             PrintNumber = dr["PrintNumber"].ToString(),
                             StoreName = dr["StoreName"].ToString(),
                             IssueDate = dr["IssueDate"].ToString(),
                             IssueBy = dr["IssueBy"].ToString(),
                             IssueCode = dr["IssueCode"].ToString(),
                             IssueAdmissionNo = dr["IssueAdmissionNo"].ToString(),
                             DeliveryAddress = dr["DeliveryAddress"].ToString(),
                             DeliveryFee = dr["DeliveryFee"].ToString(),
                             DeliveryCourier = dr["DeliveryCourier"].ToString()
                         }).Single();

                    List<PharmacyPickingListPres> p = new List<PharmacyPickingListPres>();
                    p = (from DataRow dr in dt.Tables[1].Rows
                         select new PharmacyPickingListPres
                         {
                             prescription_id = long.Parse(dr["prescription_id"].ToString()),
                             item_id = long.Parse(dr["item_id"].ToString()),
                             item_name = dr["item_name"].ToString(),
                             quantity = dr["quantity"].ToString(),
                             uom_id = long.Parse(dr["uom_id"].ToString()),
                             uom_code = dr["uom_code"].ToString(),
                             frequency_id = long.Parse(dr["frequency_id"].ToString()),
                             frequency_code = dr["frequency_code"].ToString(),
                             dosage_id = dr["dosage_id"].ToString(),
                             dose_uom_id = long.Parse(dr["dose_uom_id"].ToString()),
                             dose_uom = dr["dose_uom"].ToString(),
                             dose_text = dr["dose_text"].ToString(),
                             administration_route_id = long.Parse(dr["administration_route_id"].ToString()),
                             administration_route_code = dr["administration_route_code"].ToString(),
                             remarks = dr["remarks"].ToString(),
                             iteration = int.Parse(dr["iteration"].ToString()),
                             is_consumables = bool.Parse(dr["is_consumables"].ToString()),
                             is_routine = bool.Parse(dr["is_routine"].ToString()),
                             compound_id = Guid.Parse(dr["compound_id"].ToString()),
                             compound_name = dr["compound_name"].ToString(),
                             origin_prescription_id = long.Parse(dr["origin_prescription_id"].ToString()),
                             RackName = dr["RackName"].ToString(),
                             PrescriptionDate = DateTime.Parse(dr["PrescriptionDate"].ToString()).ToString("dd-MMM-yyyy"),
                             IsDoseText = bool.Parse(dr["IsDoseText"].ToString()),
                             IssuedQty = dr["IssuedQty"].ToString().Replace(",", "."),
                             item_sequence = int.Parse(dr["item_sequence"].ToString()),
                             editedId = Guid.Parse(dr["editedId"].ToString()),
                             LocationDrug = dr["LocationDrug"].ToString()
                         }).ToList();

                    List<PharmacyPickingListAllergy> a = new List<PharmacyPickingListAllergy>();
                    a = (from DataRow dr in dt.Tables[2].Rows
                         select new PharmacyPickingListAllergy
                         {
                             allergy = dr["allergy"].ToString(),
                             reaction = dr["reaction"].ToString()
                         }).ToList();

                    List<PharmacyPickingListObjective> o = new List<PharmacyPickingListObjective>();
                    o = (from DataRow dr in dt.Tables[3].Rows
                         select new PharmacyPickingListObjective
                         {
                             soap_mapping_id = Guid.Parse(dr["soap_mapping_id"].ToString()),
                             soap_mapping_name = dr["soap_mapping_name"].ToString(),
                             value = dr["value"].ToString()
                         }).ToList();

                    List<PharmacyPickingListCompoundHeader> ch = new List<PharmacyPickingListCompoundHeader>();
                    ch = (from DataRow dr in dt.Tables[4].Rows
                          select new PharmacyPickingListCompoundHeader
                          {
                              prescription_compound_header_id = Guid.Parse(dr["prescription_compound_header_id"].ToString()),
                              compound_name = dr["compound_name"].ToString(),
                              quantity = dr["quantity"].ToString().Replace(",", "."),
                              uom_id = long.Parse(dr["uom_id"].ToString()),
                              uom_code = dr["uom_code"].ToString(),
                              administration_frequency_id = long.Parse(dr["administration_frequency_id"].ToString()),
                              frequency_code = dr["frequency_code"].ToString(),
                              dose = dr["dose"].ToString().Replace(",", "."),
                              dose_uom_id = long.Parse(dr["dose_uom_id"].ToString()),
                              dose_uom = dr["dose_uom"].ToString(),
                              administration_route_id = long.Parse(dr["administration_route_id"].ToString()),
                              administration_route_code = dr["administration_route_code"].ToString(),
                              administration_instruction = dr["administration_instruction"].ToString(),
                              iter = int.Parse(dr["iter"].ToString()),
                              item_sequence = short.Parse(dr["item_sequence"].ToString()),
                              PrescriptionDate = DateTime.Parse(dr["PrescriptionDate"].ToString()).ToString("dd-MMM-yyyy"),
                              dose_text = dr["dose_text"].ToString(),
                              IsDoseText = bool.Parse(dr["IsDoseText"].ToString()),
                              compound_note = dr["compound_note"].ToString(),
                              IssuedQty = dr["IssuedQty"].ToString().Replace(",", ".")
                          }).ToList();

                    List<PharmacyPickingListCompoundDetail> cd = new List<PharmacyPickingListCompoundDetail>();
                    cd = (from DataRow dr in dt.Tables[5].Rows
                          select new PharmacyPickingListCompoundDetail
                          {
                              prescription_compound_header_id = Guid.Parse(dr["prescription_compound_header_id"].ToString()),
                              prescription_compound_detail_id = Guid.Parse(dr["prescription_compound_detail_id"].ToString()),
                              quantity = dr["quantity"].ToString().Replace(",", "."),
                              uom_id = long.Parse(dr["uom_id"].ToString()),
                              uom_code = dr["uom_code"].ToString(),
                              item_id = long.Parse(dr["item_id"].ToString()),
                              item_name = dr["item_name"].ToString(),
                              item_sequence = short.Parse(dr["item_sequence"].ToString()),
                              RackName = dr["RackName"].ToString(),
                              dose_uom_id = long.Parse(dr["dose_uom_id"].ToString()),
                              dose = dr["dose"].ToString().Replace(",", "."),
                              dose_text = dr["dose_text"].ToString(),
                              IsDoseText = bool.Parse(dr["IsDoseText"].ToString()),
                              dose_uom_code = dr["dose_uom_code"].ToString(),
                              IssuedQty = dr["IssuedQty"].ToString().Replace(",", "."),
                              editedId = Guid.Parse(dr["editedId"].ToString()),
                              LocationDrug = dr["LocationDrug"].ToString()
                          }).ToList();


                    data.pickingListHeader = h;
                    data.pickingListPres = p;
                    data.pickingListAllergy = a;
                    data.pickingListObjective = o;
                    data.pickingListCompoundHeader = ch;
                    data.pickingListCompoundDetail = cd;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return data;
        }


        public string ResetItemIssue(long OrganizationId, long AdmissionId, string EncounterId, int IsAdditional, long Updater, List<long> aritemids)
        {
            string aritemtext = string.Join(",", aritemids.ToArray());

            DataTable dt = new DataTable();
            string data;
            try
            {
                using (SqlConnection conn = new SqlConnection(Siloam.System.ApplicationSetting.ConnectionString))
                {
                    conn.Open();
                    SqlCommand cmd = conn.CreateCommand();
                    cmd.CommandText = "spResetItemIssue";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("OrganizationId", OrganizationId));
                    cmd.Parameters.Add(new SqlParameter("AdmissionId", AdmissionId));
                    cmd.Parameters.Add(new SqlParameter("EncounterId", EncounterId));
                    cmd.Parameters.Add(new SqlParameter("ArItemIds", aritemtext));
                    cmd.Parameters.Add(new SqlParameter("IsAdditional", IsAdditional));
                    cmd.Parameters.Add(new SqlParameter("Updater", Updater));
                    using (var da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }
                    data = (from DataRow dr in dt.Rows
                            select dr["Result"].ToString()).Single();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return data;
        }

        public List<PharmacyMedHistory> GetPharmacyMedicationHistory(long PatientId, long AdmissionId)
        {
            List<PharmacyMedHistory> data = new List<PharmacyMedHistory>();
            try
            {
                using (SqlConnection conn = new SqlConnection(Siloam.System.ApplicationSetting.ConnectionString))
                {
                    conn.Open();
                    SqlCommand cmd = conn.CreateCommand();
                    cmd.CommandText = "spGET_PHARMACYMEDHISTORY";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("PatientId", PatientId));
                    cmd.Parameters.Add(new SqlParameter("AdmissionId", AdmissionId));
                    using (var reader = cmd.ExecuteReader())
                    {
                        data = reader.MapToList<PharmacyMedHistory>();
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return data;
        }

        public List<ViewTransactionHeader> GetDataTransactionByPatientOrganization(long PatientId, long OrganizationId)
        {
            List<ViewTransactionHeader> data = new List<ViewTransactionHeader>();
            try
            {
                using (SqlConnection conn = new SqlConnection(Siloam.System.ApplicationSetting.ConnectionString))
                {
                    conn.Open();
                    SqlCommand cmd = conn.CreateCommand();
                    cmd.CommandText = "spGET_PHARMACYTRANSHEADER";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("PatientId", PatientId));
                    cmd.Parameters.Add(new SqlParameter("OrganizationId", OrganizationId));
                    using (var reader = cmd.ExecuteReader())
                    {
                        data = reader.MapToList<ViewTransactionHeader>();
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return data;
        }

        public List<PharmacyPrintLabel> GetDataPharmacyPrintLabel(long HeaderId)
        {
            List<PharmacyPrintLabel> data = new List<PharmacyPrintLabel>();
            try
            {
                using (SqlConnection conn = new SqlConnection(Siloam.System.ApplicationSetting.ConnectionString))
                {
                    conn.Open();
                    SqlCommand cmd = conn.CreateCommand();
                    cmd.CommandText = "spGET_PHARMACYPRINTLABEL";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("HeaderId", HeaderId));
                    using (var reader = cmd.ExecuteReader())
                    {
                        data = reader.MapToList<PharmacyPrintLabel>();
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return data;
        }

        public List<OrderItem> GetAdditionalCompound(long OrganizationId, long DoctorId, long AdmissionId, Guid EncounterId, bool IsAdditional)
        {
            List<OrderItem> data = new List<OrderItem>();
            try
            {
                using (SqlConnection conn = new SqlConnection(Siloam.System.ApplicationSetting.ConnectionString))
                {
                    conn.Open();
                    SqlCommand cmd = conn.CreateCommand();
                    cmd.CommandText = "spGET_ADDITIONALCOMPOUNDSYNC";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("OrganizationId", OrganizationId));
                    cmd.Parameters.Add(new SqlParameter("DoctorId", DoctorId));
                    cmd.Parameters.Add(new SqlParameter("AdmissionId", AdmissionId));
                    cmd.Parameters.Add(new SqlParameter("EncounterId", EncounterId));
                    cmd.Parameters.Add(new SqlParameter("IsAdditional", IsAdditional));
                    using (var reader = cmd.ExecuteReader())
                    {
                        data = reader.MapToList<OrderItem>();
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return data;
        }

        public List<OrderItem> GetAdditional(long OrganizationId, long AdmissionId, Guid EncounterId, int mode, string SettingValue)
        {
            List<OrderItem> data = new List<OrderItem>();
            int count = 0;
            if (mode == 1)
            {
                using (var context = new DatabaseContext(ContextOption))
                {
                    count = (from a in context.AdditionalPrescriptionSet
                             where a.organization_id == OrganizationId && a.hope_admission_id == AdmissionId && a.encounter_ticket_id == EncounterId && a.is_consumables == false && a.issued_qty > 0 && a.is_active == true
                             select a.additional_prescription_id).Count();
                    if (count == 0)
                    {
                        return null;
                    }
                    else
                    {
                        data = (from a in context.AdditionalPrescriptionSet
                                where a.organization_id == OrganizationId && a.hope_admission_id == AdmissionId && a.encounter_ticket_id == EncounterId && a.is_consumables == false && a.issued_qty > 0 && a.is_active == true
                                orderby a.item_sequence
                                select new OrderItem
                                {
                                    AdministrationFrequencyId = a.frequency_id,
                                    AdministrationInstruction = a.remarks,
                                    AdministrationRouteId = a.administration_route_id,
                                    DispensingInstruction = "",
                                    Dose = a.dose_text != "" && SettingValue == "TRUE" ? "0.0001" : a.dosage_id.ToString(),
                                    DoseText = a.dose_text,
                                    DoseUomId = a.dose_uom_id,
                                    DrugId = a.item_id,
                                    IsPrn = false,
                                    PatientInformation = "",
                                    Quantity = a.issued_qty.ToString(),
                                    Repeat = (int)a.iteration,
                                    MedicalEncounterEntryInterfaceId = a.additional_prescription_sync_id
                                }).ToList();
                        return data;
                    }
                }
            }
            else
            {
                using (var context = new DatabaseContext(ContextOption))
                {
                    count = (from a in context.PrescriptionSet
                             where a.organization_id == OrganizationId && a.hope_admission_id == AdmissionId && a.encounter_ticket_id == EncounterId && a.is_consumables == false && a.issued_qty > 0 && a.is_active == true
                             select a.pharmacy_prescription_id).Count();
                    if (count == 0)
                    {
                        return null;
                    }
                    else
                    {
                        data = (from a in context.PrescriptionSet
                                where a.organization_id == OrganizationId && a.hope_admission_id == AdmissionId && a.encounter_ticket_id == EncounterId && a.is_consumables == false && a.issued_qty > 0 && a.is_active == true
                                orderby a.item_sequence
                                select new OrderItem
                                {
                                    AdministrationFrequencyId = a.frequency_id,
                                    AdministrationInstruction = a.remarks,
                                    AdministrationRouteId = a.administration_route_id,
                                    DispensingInstruction = "",
                                    Dose = a.dose_text != "" && SettingValue == "TRUE" ? "0.0001" : a.dosage_id.ToString(),
                                    DoseText = a.dose_text,
                                    DoseUomId = a.dose_uom_id,
                                    DrugId = a.item_id,
                                    IsPrn = false,
                                    PatientInformation = "",
                                    Quantity = a.issued_qty.ToString(),
                                    Repeat = (int)a.iteration,
                                    MedicalEncounterEntryInterfaceId = a.prescription_sync_id
                                }).ToList();
                        return data;
                    }
                }
            }
        }

        public List<OrderItemConsumables> GetAdditionalConsumables(long OrganizationId, long AdmissionId, Guid EncounterId, int mode)
        {
            List<OrderItemConsumables> data = new List<OrderItemConsumables>();
            int count = 0;
            if (mode == 1)
            {
                using (var context = new DatabaseContext(ContextOption))
                {
                    count = (from a in context.AdditionalPrescriptionSet
                             where a.organization_id == OrganizationId && a.hope_admission_id == AdmissionId && a.encounter_ticket_id == EncounterId && a.is_consumables == true && a.issued_qty > 0 && a.is_active == true
                             select a.additional_prescription_id).Count();
                    if (count == 0)
                    {
                        return null;
                    }
                    else
                    {
                        data = (from a in context.AdditionalPrescriptionSet
                                where a.organization_id == OrganizationId && a.hope_admission_id == AdmissionId && a.encounter_ticket_id == EncounterId && a.is_consumables == true && a.issued_qty > 0 && a.is_active == true
                                orderby a.item_sequence
                                select new OrderItemConsumables
                                {
                                    UsageInstruction = a.remarks,
                                    ItemId = a.item_id,
                                    PatientInformation = "",
                                    Quantity = a.issued_qty.ToString(),
                                    DispensingInstruction = "",
                                    MedicalEncounterEntryInterfaceId = a.additional_prescription_sync_id
                                }).ToList();
                        return data;
                    }
                }
            }
            else
            {
                using (var context = new DatabaseContext(ContextOption))
                {
                    count = (from a in context.PrescriptionSet
                             where a.organization_id == OrganizationId && a.hope_admission_id == AdmissionId && a.encounter_ticket_id == EncounterId && a.is_consumables == true && a.issued_qty > 0 && a.is_active == true
                             select a.pharmacy_prescription_id).Count();
                    if (count == 0)
                    {
                        return null;
                    }
                    else
                    {
                        data = (from a in context.PrescriptionSet
                                where a.organization_id == OrganizationId && a.hope_admission_id == AdmissionId && a.encounter_ticket_id == EncounterId && a.is_consumables == true && a.issued_qty > 0 && a.is_active == true
                                orderby a.item_sequence
                                select new OrderItemConsumables
                                {
                                    UsageInstruction = a.remarks,
                                    ItemId = a.item_id,
                                    PatientInformation = "",
                                    Quantity = a.issued_qty.ToString(),
                                    DispensingInstruction = "",
                                    MedicalEncounterEntryInterfaceId = a.prescription_sync_id
                                }).ToList();
                        return data;
                    }
                }
            }
        }

        public List<itemIssue> GetAdditionalItemIssue(long OrganizationId, long AdmissionId, Guid EncounterId, int mode)
        {
            List<itemIssue> data = new List<itemIssue>();
            int count = 0;
            if (mode == 1)
            {
                using (var context = new DatabaseContext(ContextOption))
                {
                    count = (from a in context.AdditionalPrescriptionSet
                             where a.organization_id == OrganizationId && a.hope_admission_id == AdmissionId && a.encounter_ticket_id == EncounterId && a.is_consumables == false && a.issued_qty > 0 && a.is_active == true
                             select a.additional_prescription_id).Count();
                    if (count == 0)
                    {
                        return null;
                    }
                    else
                    {
                        data = (from a in context.AdditionalPrescriptionSet
                                where a.organization_id == OrganizationId && a.hope_admission_id == AdmissionId && a.encounter_ticket_id == EncounterId && a.is_consumables == false && a.issued_qty > 0 && a.is_active == true
                                orderby a.item_sequence
                                select new itemIssue
                                {
                                    itemId = a.item_id,
                                    quantity = a.issued_qty,
                                    uomid = a.uom_id
                                }).ToList();
                        return data;
                    }
                }
            }
            else
            {
                using (var context = new DatabaseContext(ContextOption))
                {
                    count = (from a in context.PrescriptionSet
                             where a.organization_id == OrganizationId && a.hope_admission_id == AdmissionId && a.encounter_ticket_id == EncounterId && a.is_consumables == false && a.issued_qty > 0 && a.is_active == true
                             select a.pharmacy_prescription_id).Count();
                    if (count == 0)
                    {
                        return null;
                    }
                    else
                    {
                        data = (from a in context.PrescriptionSet
                                where a.organization_id == OrganizationId && a.hope_admission_id == AdmissionId && a.encounter_ticket_id == EncounterId && a.is_consumables == false && a.issued_qty > 0 && a.is_active == true
                                orderby a.item_sequence
                                select new itemIssue
                                {
                                    itemId = a.item_id,
                                    quantity = a.issued_qty,
                                    uomid = a.uom_id
                                }).ToList();
                        return data;
                    }
                }
            }
        }

        public List<SyncPrescriptionDB> GetPrescriptionSync(long OrganizationId, long AdmissionId, long DoctorId, Guid EncounterId, long Updater)
        {
            List<SyncPrescriptionDB> data = new List<SyncPrescriptionDB>();
            try
            {
                using (SqlConnection conn = new SqlConnection(Siloam.System.ApplicationSetting.ConnectionString))
                {
                    conn.Open();
                    SqlCommand cmd = conn.CreateCommand();
                    cmd.CommandText = "spSUBMIT_SYNC";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("OrganizationId", OrganizationId));
                    cmd.Parameters.Add(new SqlParameter("AdmissionId", AdmissionId));
                    cmd.Parameters.Add(new SqlParameter("DoctorId", DoctorId));
                    cmd.Parameters.Add(new SqlParameter("EncounterId", EncounterId));
                    cmd.Parameters.Add(new SqlParameter("Updater", Updater));
                    using (var reader = cmd.ExecuteReader())
                    {
                        data = reader.MapToList<SyncPrescriptionDB>();
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return data;
        }

        public PharmacyPrintPrescription GetPrintOriginalPrescription(long OrganizationId, long PatientId, long AdmissionId, string EncounterId, long StoreId)
        {
            DataSet dt = new DataSet();
            PharmacyPrintPrescription data = new PharmacyPrintPrescription();
            try
            {
                using (SqlConnection conn = new SqlConnection(Siloam.System.ApplicationSetting.ConnectionString))
                {
                    conn.Open();
                    SqlCommand cmd = conn.CreateCommand();
                    cmd.CommandText = "spGET_PRINTORIGINALPRESCRIPTION";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("OrganizationId", OrganizationId));
                    cmd.Parameters.Add(new SqlParameter("PatientId", PatientId));
                    cmd.Parameters.Add(new SqlParameter("AdmissionId", AdmissionId));
                    cmd.Parameters.Add(new SqlParameter("EncounterId", EncounterId));
                    cmd.Parameters.Add(new SqlParameter("StoreId", StoreId));
                    using (var da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }

                    PharmacyPrintHeader h = new PharmacyPrintHeader();
                    h = (from DataRow dr in dt.Tables[0].Rows
                         select new PharmacyPrintHeader
                         {
                             EncounterId = Guid.Parse(dr["EncounterId"].ToString()),
                             OrganizationId = long.Parse(dr["OrganizationId"].ToString()),
                             PatientId = long.Parse(dr["PatientId"].ToString()),
                             AdmissionId = long.Parse(dr["AdmissionId"].ToString()),
                             AdmissionNo = dr["AdmissionNo"].ToString(),
                             LocalMrNo = dr["LocalMrNo"].ToString(),
                             PatientName = dr["PatientName"].ToString(),
                             BirthDate = DateTime.Parse(dr["BirthDate"].ToString()).ToString("dd MMM yyyy"),
                             Age = dr["Age"].ToString(),
                             Gender = dr["Gender"].ToString(),
                             DoctorName = dr["DoctorName"].ToString(),
                             SpecialtyName = dr["SpecialtyName"].ToString(),
                             PrescriptionDate = DateTime.Parse(dr["PrescriptionDate"].ToString()).ToString("dd MMM yyyy HH:mm"),
                             PrescriptionNo = dr["PrescriptionNo"].ToString(),
                             PayerName = dr["PayerName"].ToString(),
                             QueueNo = dr["QueueNo"].ToString(),
                             IsCOVID = bool.Parse(dr["IsCOVID"].ToString()),
                             SipNo = dr["SipNo"].ToString(),
                             PrintNumber = dr["PrintNumber"].ToString()
                         }).Single();

                    List<PharmacyPrintPres> p = new List<PharmacyPrintPres>();
                    p = (from DataRow dr in dt.Tables[1].Rows
                         select new PharmacyPrintPres
                         {
                             prescription_id = long.Parse(dr["prescription_id"].ToString()),
                             item_id = long.Parse(dr["item_id"].ToString()),
                             item_name = dr["item_name"].ToString(),
                             quantity = dr["quantity"].ToString(),
                             uom_id = long.Parse(dr["uom_id"].ToString()),
                             uom_code = dr["uom_code"].ToString(),
                             frequency_id = long.Parse(dr["frequency_id"].ToString()),
                             frequency_code = dr["frequency_code"].ToString(),
                             dosage_id = dr["dosage_id"].ToString(),
                             dose_uom_id = long.Parse(dr["dose_uom_id"].ToString()),
                             dose_uom = dr["dose_uom"].ToString(),
                             dose_text = dr["dose_text"].ToString(),
                             administration_route_id = long.Parse(dr["administration_route_id"].ToString()),
                             administration_route_code = dr["administration_route_code"].ToString(),
                             remarks = dr["remarks"].ToString(),
                             iteration = int.Parse(dr["iteration"].ToString()),
                             is_consumables = bool.Parse(dr["is_consumables"].ToString()),
                             is_routine = bool.Parse(dr["is_routine"].ToString()),
                             compound_id = Guid.Parse(dr["compound_id"].ToString()),
                             compound_name = dr["compound_name"].ToString(),
                             origin_prescription_id = long.Parse(dr["origin_prescription_id"].ToString()),
                             RackName = dr["RackName"].ToString(),
                             PrescriptionDate = DateTime.Parse(dr["PrescriptionDate"].ToString()).ToString("dd-MMM-yyyy"),
                             IsDoseText = bool.Parse(dr["IsDoseText"].ToString()),
                             IssuedQty = dr["IssuedQty"].ToString().Replace(",", "."),
                             item_sequence = int.Parse(dr["item_sequence"].ToString())
                         }).ToList();

                    List<PharmacyPrintAllergy> a = new List<PharmacyPrintAllergy>();
                    a = (from DataRow dr in dt.Tables[2].Rows
                         select new PharmacyPrintAllergy
                         {
                             allergy = dr["allergy"].ToString(),
                             reaction = dr["reaction"].ToString()
                         }).ToList();

                    List<PharmacyPrintObjective> o = new List<PharmacyPrintObjective>();
                    o = (from DataRow dr in dt.Tables[3].Rows
                         select new PharmacyPrintObjective
                         {
                             soap_mapping_id = Guid.Parse(dr["soap_mapping_id"].ToString()),
                             soap_mapping_name = dr["soap_mapping_name"].ToString(),
                             value = dr["value"].ToString()
                         }).ToList();

                    List<PharmacyPrintCompoundHeader> ch = new List<PharmacyPrintCompoundHeader>();
                    ch = (from DataRow dr in dt.Tables[4].Rows
                          select new PharmacyPrintCompoundHeader
                          {
                              prescription_compound_header_id = Guid.Parse(dr["prescription_compound_header_id"].ToString()),
                              compound_name = dr["compound_name"].ToString(),
                              quantity = dr["quantity"].ToString().Replace(",", "."),
                              uom_id = long.Parse(dr["uom_id"].ToString()),
                              uom_code = dr["uom_code"].ToString(),
                              administration_frequency_id = long.Parse(dr["administration_frequency_id"].ToString()),
                              frequency_code = dr["frequency_code"].ToString(),
                              dose = dr["dose"].ToString().Replace(",", "."),
                              dose_uom_id = long.Parse(dr["dose_uom_id"].ToString()),
                              dose_uom = dr["dose_uom"].ToString(),
                              administration_route_id = long.Parse(dr["administration_route_id"].ToString()),
                              administration_route_code = dr["administration_route_code"].ToString(),
                              administration_instruction = dr["administration_instruction"].ToString(),
                              iter = int.Parse(dr["iter"].ToString()),
                              item_sequence = short.Parse(dr["item_sequence"].ToString()),
                              PrescriptionDate = DateTime.Parse(dr["PrescriptionDate"].ToString()).ToString("dd-MMM-yyyy"),
                              dose_text = dr["dose_text"].ToString(),
                              IsDoseText = bool.Parse(dr["IsDoseText"].ToString()),
                              compound_note = dr["compound_note"].ToString(),
                              IssuedQty = dr["IssuedQty"].ToString().Replace(",", ".")
                          }).ToList();

                    List<PharmacyPrintCompoundDetail> cd = new List<PharmacyPrintCompoundDetail>();
                    cd = (from DataRow dr in dt.Tables[5].Rows
                          select new PharmacyPrintCompoundDetail
                          {
                              prescription_compound_header_id = Guid.Parse(dr["prescription_compound_header_id"].ToString()),
                              prescription_compound_detail_id = Guid.Parse(dr["prescription_compound_detail_id"].ToString()),
                              quantity = dr["quantity"].ToString().Replace(",", "."),
                              uom_id = long.Parse(dr["uom_id"].ToString()),
                              uom_code = dr["uom_code"].ToString(),
                              item_id = long.Parse(dr["item_id"].ToString()),
                              item_name = dr["item_name"].ToString(),
                              item_sequence = short.Parse(dr["item_sequence"].ToString()),
                              RackName = dr["RackName"].ToString(),
                              dose_uom_id = long.Parse(dr["dose_uom_id"].ToString()),
                              dose = dr["dose"].ToString().Replace(",", "."),
                              dose_text = dr["dose_text"].ToString(),
                              IsDoseText = bool.Parse(dr["IsDoseText"].ToString()),
                              dose_uom_code = dr["dose_uom_code"].ToString()
                          }).ToList();

                    
                    data.printHeader = h;
                    data.printPres = p;
                    data.printAllergy = a;
                    data.printObjective = o;
                    data.printCompoundHeader = ch;
                    data.printCompoundDetail = cd;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return data;
        }

        public PharmacyPrintPrescription GetPrintOriginalAdditionalPrescription(long OrganizationId, long PatientId, long AdmissionId, string EncounterId, long StoreId)
        {
            DataSet dt = new DataSet();
            PharmacyPrintPrescription data = new PharmacyPrintPrescription();
            try
            {
                using (SqlConnection conn = new SqlConnection(Siloam.System.ApplicationSetting.ConnectionString))
                {
                    conn.Open();
                    SqlCommand cmd = conn.CreateCommand();
                    cmd.CommandText = "spGET_PRINTORIGINALADDITIONALPRESCRIPTION";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("OrganizationId", OrganizationId));
                    cmd.Parameters.Add(new SqlParameter("PatientId", PatientId));
                    cmd.Parameters.Add(new SqlParameter("AdmissionId", AdmissionId));
                    cmd.Parameters.Add(new SqlParameter("EncounterId", EncounterId));
                    cmd.Parameters.Add(new SqlParameter("StoreId", StoreId));
                    using (var da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }

                    PharmacyPrintHeader h = new PharmacyPrintHeader();
                    h = (from DataRow dr in dt.Tables[0].Rows
                         select new PharmacyPrintHeader
                         {
                             EncounterId = Guid.Parse(dr["EncounterId"].ToString()),
                             OrganizationId = long.Parse(dr["OrganizationId"].ToString()),
                             PatientId = long.Parse(dr["PatientId"].ToString()),
                             AdmissionId = long.Parse(dr["AdmissionId"].ToString()),
                             AdmissionNo = dr["AdmissionNo"].ToString(),
                             LocalMrNo = dr["LocalMrNo"].ToString(),
                             PatientName = dr["PatientName"].ToString(),
                             BirthDate = DateTime.Parse(dr["BirthDate"].ToString()).ToString("dd MMM yyyy"),
                             Age = dr["Age"].ToString(),
                             Gender = dr["Gender"].ToString(),
                             DoctorName = dr["DoctorName"].ToString(),
                             SpecialtyName = dr["SpecialtyName"].ToString(),
                             PrescriptionDate = DateTime.Parse(dr["PrescriptionDate"].ToString()).ToString("dd MMM yyyy HH:mm"),
                             PrescriptionNo = dr["PrescriptionNo"].ToString(),
                             PayerName = dr["PayerName"].ToString(),
                             QueueNo = dr["QueueNo"].ToString(),
                             IsCOVID = bool.Parse(dr["IsCOVID"].ToString()),
                             SipNo = dr["SipNo"].ToString(),
                             PrintNumber = dr["PrintNumber"].ToString()
                         }).Single();

                    List<PharmacyPrintPres> p = new List<PharmacyPrintPres>();
                    p = (from DataRow dr in dt.Tables[1].Rows
                         select new PharmacyPrintPres
                         {
                             prescription_id = long.Parse(dr["prescription_id"].ToString()),
                             item_id = long.Parse(dr["item_id"].ToString()),
                             item_name = dr["item_name"].ToString(),
                             quantity = dr["quantity"].ToString(),
                             uom_id = long.Parse(dr["uom_id"].ToString()),
                             uom_code = dr["uom_code"].ToString(),
                             frequency_id = long.Parse(dr["frequency_id"].ToString()),
                             frequency_code = dr["frequency_code"].ToString(),
                             dosage_id = dr["dosage_id"].ToString(),
                             dose_uom_id = long.Parse(dr["dose_uom_id"].ToString()),
                             dose_uom = dr["dose_uom"].ToString(),
                             dose_text = dr["dose_text"].ToString(),
                             administration_route_id = long.Parse(dr["administration_route_id"].ToString()),
                             administration_route_code = dr["administration_route_code"].ToString(),
                             remarks = dr["remarks"].ToString(),
                             iteration = int.Parse(dr["iteration"].ToString()),
                             is_consumables = bool.Parse(dr["is_consumables"].ToString()),
                             is_routine = bool.Parse(dr["is_routine"].ToString()),
                             compound_id = Guid.Parse(dr["compound_id"].ToString()),
                             compound_name = dr["compound_name"].ToString(),
                             origin_prescription_id = long.Parse(dr["origin_prescription_id"].ToString()),
                             RackName = dr["RackName"].ToString(),
                             PrescriptionDate = DateTime.Parse(dr["PrescriptionDate"].ToString()).ToString("dd-MMM-yyyy"),
                             IsDoseText = bool.Parse(dr["IsDoseText"].ToString()),
                             IssuedQty = dr["IssuedQty"].ToString().Replace(",", "."),
                             item_sequence = int.Parse(dr["item_sequence"].ToString())
                         }).ToList();

                    List<PharmacyPrintAllergy> a = new List<PharmacyPrintAllergy>();
                    a = (from DataRow dr in dt.Tables[2].Rows
                         select new PharmacyPrintAllergy
                         {
                             allergy = dr["allergy"].ToString(),
                             reaction = dr["reaction"].ToString()
                         }).ToList();

                    List<PharmacyPrintObjective> o = new List<PharmacyPrintObjective>();
                    o = (from DataRow dr in dt.Tables[3].Rows
                         select new PharmacyPrintObjective
                         {
                             soap_mapping_id = Guid.Parse(dr["soap_mapping_id"].ToString()),
                             soap_mapping_name = dr["soap_mapping_name"].ToString(),
                             value = dr["value"].ToString()
                         }).ToList();

                    List<PharmacyPrintCompoundHeader> ch = new List<PharmacyPrintCompoundHeader>();
                    ch = (from DataRow dr in dt.Tables[4].Rows
                          select new PharmacyPrintCompoundHeader
                          {
                              prescription_compound_header_id = Guid.Parse(dr["prescription_compound_header_id"].ToString()),
                              compound_name = dr["compound_name"].ToString(),
                              quantity = dr["quantity"].ToString().Replace(",", "."),
                              uom_id = long.Parse(dr["uom_id"].ToString()),
                              uom_code = dr["uom_code"].ToString(),
                              administration_frequency_id = long.Parse(dr["administration_frequency_id"].ToString()),
                              frequency_code = dr["frequency_code"].ToString(),
                              dose = dr["dose"].ToString().Replace(",", "."),
                              dose_uom_id = long.Parse(dr["dose_uom_id"].ToString()),
                              dose_uom = dr["dose_uom"].ToString(),
                              administration_route_id = long.Parse(dr["administration_route_id"].ToString()),
                              administration_route_code = dr["administration_route_code"].ToString(),
                              administration_instruction = dr["administration_instruction"].ToString(),
                              iter = int.Parse(dr["iter"].ToString()),
                              item_sequence = short.Parse(dr["item_sequence"].ToString()),
                              PrescriptionDate = DateTime.Parse(dr["PrescriptionDate"].ToString()).ToString("dd-MMM-yyyy"),
                              dose_text = dr["dose_text"].ToString(),
                              IsDoseText = bool.Parse(dr["IsDoseText"].ToString()),
                              compound_note = dr["compound_note"].ToString(),
                              IssuedQty = dr["IssuedQty"].ToString().Replace(",", ".")
                          }).ToList();

                    List<PharmacyPrintCompoundDetail> cd = new List<PharmacyPrintCompoundDetail>();
                    cd = (from DataRow dr in dt.Tables[5].Rows
                          select new PharmacyPrintCompoundDetail
                          {
                              prescription_compound_header_id = Guid.Parse(dr["prescription_compound_header_id"].ToString()),
                              prescription_compound_detail_id = Guid.Parse(dr["prescription_compound_detail_id"].ToString()),
                              quantity = dr["quantity"].ToString().Replace(",", "."),
                              uom_id = long.Parse(dr["uom_id"].ToString()),
                              uom_code = dr["uom_code"].ToString(),
                              item_id = long.Parse(dr["item_id"].ToString()),
                              item_name = dr["item_name"].ToString(),
                              item_sequence = short.Parse(dr["item_sequence"].ToString()),
                              RackName = dr["RackName"].ToString(),
                              dose_uom_id = long.Parse(dr["dose_uom_id"].ToString()),
                              dose = dr["dose"].ToString().Replace(",", "."),
                              dose_text = dr["dose_text"].ToString(),
                              IsDoseText = bool.Parse(dr["IsDoseText"].ToString()),
                              dose_uom_code = dr["dose_uom_code"].ToString()
                          }).ToList();


                    data.printHeader = h;
                    data.printPres = p;
                    data.printAllergy = a;
                    data.printObjective = o;
                    data.printCompoundHeader = ch;
                    data.printCompoundDetail = cd;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return data;
        }

        public string InsertToTimeStampTable(Guid EncounterId, string Type)
        {
            DataTable dt = new DataTable();
            string data = "SUCCESS";
            try
            {
                using (SqlConnection conn = new SqlConnection(Siloam.System.ApplicationSetting.ConnectionString))
                {
                    conn.Open();
                    SqlCommand cmd = conn.CreateCommand();
                    cmd.CommandText = "spInsertLogTemp";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("encounter_id", EncounterId));
                    cmd.Parameters.Add(new SqlParameter("is_type", Type));
                    using (var da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return data;
        }

        public string GetSettingDoseText (long OrganizationId)
        {
            string data = "";
            using (var context = new DatabaseContext(ContextOption))
            {
                data = (from a in context.SettingSet
                        where a.is_active == true && a.organization_id == OrganizationId && a.setting_name == "USE_DOSETEXT"
                        select a.setting_value).First();
            }
            return data;
        }

        public string GetSettingCLMA(long OrganizationId)
        {
            string data = "";
            using (var context = new DatabaseContext(ContextOption))
            {
                data = (from a in context.SettingSet
                        where a.is_active == true && a.organization_id == OrganizationId && a.setting_name == "USE_CLMA"
                        select a.setting_value).First();
            }
            return data;
        }

        public string UpdateFlagHOPE (long OrganizationId, long AdmissionId, Guid EncounterId)
        {   
            string data = "";
            PharmacyRecord temp = new PharmacyRecord();
            try
            {
                using (var context = new DatabaseContext(ContextOption))
                {
                    temp = (from a in context.RecordSet
                            where a.is_active == true && a.organization_id == OrganizationId && a.admission_id == AdmissionId && a.encounter_id == EncounterId
                            select a).First();

                    if (temp == null)
                    {
                        data = "NOT FOUND";
                    }
                    else
                    {
                        temp.is_syncHOPE = true;
                        context.Update(temp);
                        context.SaveChanges();
                        data = "SUCCESS";
                    }
                }
                return data;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public long GetPayerIdRecord(long OrganizationId, long PatientId, long AdmissionId, Guid EncounterId)
        {
            long data = 0;
            using (var context = new DatabaseContext(ContextOption))
            {
                data = (from a in context.RecordSet
                        where a.is_active == true && a.organization_id == OrganizationId && a.patient_id == PatientId && a.admission_id == AdmissionId && a.encounter_id == EncounterId
                        select a.payer_id).First();
            }
            return data;
        }

        public List<CheckPrice> GetCheckPrice(long OrganizationId, long PatientId, long AdmissionId, Guid EncounterId, List<CheckPriceRequest> CheckPriceRequests)
        {
            DataTable dt = new DataTable();
            List<CheckPrice> data = new List<CheckPrice>();
            string xmlPrescription = ConvertRequestCheckPriceToXML(CheckPriceRequests);
            try
            {
                using (SqlConnection conn = new SqlConnection(Siloam.System.ApplicationSetting.ConnectionString))
                {
                    conn.Open();
                    SqlCommand cmd = conn.CreateCommand();
                    cmd.CommandText = "spGetCheckPrice";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("OrganizationId", OrganizationId));
                    cmd.Parameters.Add(new SqlParameter("PatientId", PatientId));
                    cmd.Parameters.Add(new SqlParameter("AdmissionId", AdmissionId));
                    cmd.Parameters.Add(new SqlParameter("EncounterId", EncounterId));
                    cmd.Parameters.Add(new SqlParameter("Prescription", xmlPrescription));
                    using (var da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }
                    data = (from DataRow dr in dt.Rows
                            select new CheckPrice
                            {
                                Amount = dr["Amount"].ToString().Replace(',','.'),
                                DiscountPrice = dr["DiscountPrice"].ToString().Replace(',', '.'),
                                PatientNet = dr["PatientNet"].ToString().Replace(',', '.'),
                                PayerNet = dr["PayerNet"].ToString().Replace(',', '.'),
                                Quantity = dr["Quantity"].ToString().Replace(',', '.'),
                                SinglePrice = dr["SinglePrice"].ToString().Replace(',', '.'),
                                SalesItemName = dr["SalesItemName"].ToString(),
                                SalesItemCode = dr["SalesItemCode"].ToString(),
                                SalesItemId = long.Parse(dr["SalesItemId"].ToString()),
                                Uom = dr["Uom"].ToString(),
                                TotalPatientNetFinal = dr["TotalPatientNetFinal"].ToString().Replace(',', '.'),
                                TotalPayerNetFinal = dr["TotalPayerNetFinal"].ToString().Replace(',', '.'),
                                RoundingPatientNet = dr["RoundingPatientNet"].ToString().Replace(',', '.'),
                                RoundingPayerNet = dr["RoundingPayerNet"].ToString().Replace(',', '.'),
                                TotalPatientNet = dr["TotalPatientNet"].ToString().Replace(',', '.'),
                                TotalPayerNet = dr["TotalPayerNet"].ToString().Replace(',', '.'),
                                is_consumables = int.Parse(dr["is_consumables"].ToString())
                            }).ToList();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return data;
        }

        public List<CheckPriceItemIssue> GetCheckPriceItemIssue(long OrganizationId, long PatientId, long AdmissionId, Guid EncounterId,long StoreId,bool isTele, List<CheckPriceItemIssueRequest> CheckPriceItemIssueRequests)
        {
            DataTable dt = new DataTable();
            List<CheckPriceItemIssue> data = new List<CheckPriceItemIssue>();
            string xmlPrescription = ConvertRequestCheckPriceItemIssueToXML(CheckPriceItemIssueRequests);
            try
            {
                using (SqlConnection conn = new SqlConnection(Siloam.System.ApplicationSetting.ConnectionString))
                {
                    conn.Open();
                    SqlCommand cmd = conn.CreateCommand();
                    cmd.CommandText = "spGetCheckPriceItemIssue";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("OrganizationId", OrganizationId));
                    cmd.Parameters.Add(new SqlParameter("PatientId", PatientId));
                    cmd.Parameters.Add(new SqlParameter("AdmissionId", AdmissionId));
                    cmd.Parameters.Add(new SqlParameter("EncounterId", EncounterId));
                    cmd.Parameters.Add(new SqlParameter("StoreId", StoreId));
                    cmd.Parameters.Add(new SqlParameter("isTele", isTele));
                    cmd.Parameters.Add(new SqlParameter("Prescription", xmlPrescription));
                    using (var da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }
                    data = (from DataRow dr in dt.Rows
                            select new CheckPriceItemIssue
                            {
                                Amount = dr["Amount"].ToString().Replace(',', '.'),
                                DiscountPrice = dr["DiscountPrice"].ToString().Replace(',', '.'),
                                PatientNet = dr["PatientNet"].ToString().Replace(',', '.'),
                                PayerNet = dr["PayerNet"].ToString().Replace(',', '.'),
                                issue_quantity = dr["issue_quantity"].ToString().Replace(',', '.'),
                                SinglePrice = dr["SinglePrice"].ToString().Replace(',', '.'),
                                SalesItemName = dr["SalesItemName"].ToString(),
                                SalesItemCode = dr["SalesItemCode"].ToString(),
                                SalesItemId = long.Parse(dr["SalesItemId"].ToString()),
                                Uom = dr["Uom"].ToString(),
                                RatioUOM1 = int.Parse(dr["RatioUOM1"].ToString()),
                                TotalPatientNetFinal = dr["TotalPatientNetFinal"].ToString().Replace(',', '.'),
                                TotalPayerNetFinal = dr["TotalPayerNetFinal"].ToString().Replace(',', '.'),
                                RoundingPatientNet = dr["RoundingPatientNet"].ToString().Replace(',', '.'),
                                RoundingPayerNet = dr["RoundingPayerNet"].ToString().Replace(',', '.'),
                                TotalPatientNet = dr["TotalPatientNet"].ToString().Replace(',', '.'),
                                TotalPayerNet = dr["TotalPayerNet"].ToString().Replace(',', '.'),
                                is_consumables = int.Parse(dr["is_consumables"].ToString()),
                                is_compound = int.Parse(dr["is_compound"].ToString()),
                                prescription_compound_header_id = Guid.Parse(dr["prescription_compound_header_id"].ToString()),
                                prescription_compound_name = dr["prescription_compound_name"].ToString().Replace(',', '.'),
                                substore_quantity = dr["substore_quantity"].ToString().Replace(',', '.'),
                                stock_quantity = dr["stock_quantity"].ToString().Replace(',', '.')

                            }).ToList();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return data;
        }

        public string CancelItemMySiloam(MySiloamCancelItem model)
        {
            DataTable dt = new DataTable();
            string data;
            string ListItem = string.Join(",", model.items.Select(p => p.SalesItemId.ToString()));
            try
            {
                using (SqlConnection conn = new SqlConnection(Siloam.System.ApplicationSetting.ConnectionString))
                {
                    conn.Open();
                    SqlCommand cmd = conn.CreateCommand();
                    cmd.CommandText = "spCancelDrugTeleconsultationMySiloam";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("SiloamTrxId", model.SiloamTrxId));
                    cmd.Parameters.Add(new SqlParameter("SalesItemId", ListItem));
                    using (var da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }
                    data = (from DataRow dr in dt.Rows
                            select dr["Result"].ToString()).Single();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return data;
        }

        public int GetPayerCoverage(long OrganizationId, long PatientId, long AdmissionId, Guid EncounterId)
        {
            int data = 0;
            using (var context = new DatabaseContext(ContextOption))
            {
                data = (from a in context.RecordSet
                        where a.is_active == true && a.organization_id == OrganizationId && a.patient_id == PatientId && a.admission_id == AdmissionId && a.encounter_id == EncounterId
                        select Convert.ToInt32(Math.Floor(Convert.ToDouble(a.payer_coverage.ToString())))).First();
            }
            return data;
        }

        public bool GetIsSelfCollection(long OrganizationId, long PatientId, long AdmissionId, Guid EncounterId)
        {
            bool data = false;
            using (var context = new DatabaseContext(ContextOption))
            {
                data = (from a in context.RecordSet
                        where a.is_active == true && a.organization_id == OrganizationId && a.patient_id == PatientId && a.admission_id == AdmissionId && a.encounter_id == EncounterId
                        select a.is_selfcollection).First();
            }
            return data;
        }

        public Guid GetAppointmentId(long OrganizationId, long PatientId, long AdmissionId, long DoctorId)
        {
            DataTable dt = new DataTable();
            Guid data;
            try
            {
                using (SqlConnection conn = new SqlConnection(Siloam.System.ApplicationSetting.ConnectionString))
                {
                    conn.Open();
                    SqlCommand cmd = conn.CreateCommand();
                    cmd.CommandText = "spGetAppointmentId";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("OrganizationId", OrganizationId));
                    cmd.Parameters.Add(new SqlParameter("PatientId", PatientId));
                    cmd.Parameters.Add(new SqlParameter("AdmissionId", AdmissionId));
                    cmd.Parameters.Add(new SqlParameter("DoctorId", DoctorId));
                    using (var da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }
                    data = (from DataRow dr in dt.Rows
                            select Guid.Parse(dr["Result"].ToString())).Single();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return data;
        }

        public List<PharmacyPrintLabelPrescription> GetDataPharmacyPrintLabelPharmacy(Int64 OrganizationId, Int64 AdmissionId, string EncounterID, int IsAdditional)
        {
            DataTable dt = new DataTable();
            List<PharmacyPrintLabelPrescription> data = new List<PharmacyPrintLabelPrescription>();
            try
            {
                using (SqlConnection conn = new SqlConnection(Siloam.System.ApplicationSetting.ConnectionString))
                {
                    conn.Open();
                    SqlCommand cmd = conn.CreateCommand();
                    cmd.CommandText = "spGetPrintLabelItem";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("OrganizationId", OrganizationId));
                    cmd.Parameters.Add(new SqlParameter("EncounterId", EncounterID));
                    cmd.Parameters.Add(new SqlParameter("AdmissionId", AdmissionId));
                    cmd.Parameters.Add(new SqlParameter("IsAdditional", IsAdditional));
                    using (var da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }
                    data = (from DataRow dr in dt.Rows
                            select new PharmacyPrintLabelPrescription
                            {
                                print_label_prescription_id = Guid.Parse(dr["print_label_prescription_id"].ToString()),
                                origin_prescompound_id = Guid.Parse(dr["origin_prescompound_id"].ToString()),
                                origin_prescription_id = Int64.Parse(dr["origin_prescription_id"].ToString()),
                                item_name = dr["item_name"].ToString(),
                                quantity = decimal.Parse(dr["quantity"].ToString()),
                                uom_code = dr["uom_code"].ToString(),
                                dosage_id = decimal.Parse(dr["dosage_id"].ToString()),
                                dose_uom_id = Int64.Parse(dr["dose_uom_id"].ToString()),
                                dose_uom = dr["dose_uom"].ToString(),
                                frequency_id = Int64.Parse(dr["frequency_id"].ToString()),
                                frequency_code = dr["frequency_code"].ToString(),
                                dose_text = dr["dose_text"].ToString(),
                                administration_route_id = Int64.Parse(dr["administration_route_id"].ToString()),
                                administration_route_code = dr["administration_route_code"].ToString(),
                                remarks = dr["remarks"].ToString(),
                                presc_type = dr["presc_type"].ToString(),
                                PatientInfo = dr["PatientInfo"].ToString(),
                                IsInternal = bool.Parse(dr["IsInternal"].ToString()),
                                CompundNotes = dr["CompundNotes"].ToString(),
                                alternate_quantity = dr["alternate_quantity"].ToString(),
                                ar_item_id = Int64.Parse(dr["ar_item_id"].ToString()),
                                IsCompound = bool.Parse(dr["IsCompound"].ToString()),
                                QRStringCode = dr["QRStringCode"].ToString(),
                                IssueCode = dr["IssueCode"].ToString(),
                                IsueGroup = dr["IsueGroup"].ToString()
                            }).ToList();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return data;
        }

        public List<PharmacyPrintLabelPrescription> GetDataPharmacyScannedList(Int64 OrganizationId, string IssueCode)
        {
            DataTable dt = new DataTable();
            List<PharmacyPrintLabelPrescription> data = new List<PharmacyPrintLabelPrescription>();
            try
            {
                using (SqlConnection conn = new SqlConnection(Siloam.System.ApplicationSetting.ConnectionString))
                {
                    conn.Open();
                    SqlCommand cmd = conn.CreateCommand();
                    cmd.CommandText = "spGetScannedIssueList";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("OrganizationId", OrganizationId));
                    cmd.Parameters.Add(new SqlParameter("issueCode", IssueCode));

                    using (var da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }
                    data = (from DataRow dr in dt.Rows
                            select new PharmacyPrintLabelPrescription
                            {
                                print_label_prescription_id = Guid.Parse(dr["print_label_prescription_id"].ToString()),
                                origin_prescompound_id = Guid.Parse(dr["origin_prescompound_id"].ToString()),
                                origin_prescription_id = Int64.Parse(dr["origin_prescription_id"].ToString()),
                                item_name = dr["item_name"].ToString(),
                                quantity = decimal.Parse(dr["quantity"].ToString()),
                                uom_code = dr["uom_code"].ToString(),
                                dosage_id = decimal.Parse(dr["dosage_id"].ToString()),
                                dose_uom_id = Int64.Parse(dr["dose_uom_id"].ToString()),
                                dose_uom = dr["dose_uom"].ToString(),
                                frequency_id = Int64.Parse(dr["frequency_id"].ToString()),
                                frequency_code = dr["frequency_code"].ToString(),
                                dose_text = dr["dose_text"].ToString(),
                                administration_route_id = Int64.Parse(dr["administration_route_id"].ToString()),
                                administration_route_code = dr["administration_route_code"].ToString(),
                                remarks = dr["remarks"].ToString(),
                                presc_type = dr["presc_type"].ToString(),
                                PatientInfo = dr["PatientInfo"].ToString(),
                                IsInternal = bool.Parse(dr["IsInternal"].ToString()),
                                CompundNotes = dr["CompundNotes"].ToString(),
                                alternate_quantity = dr["alternate_quantity"].ToString(),
                                ar_item_id = Int64.Parse(dr["ar_item_id"].ToString()),
                                IsCompound = bool.Parse(dr["IsCompound"].ToString()),
                                QRStringCode = dr["QRStringCode"].ToString(),
                                IssueCode = dr["IssueCode"].ToString(),
                                IsueGroup = dr["IsueGroup"].ToString()
                            }).ToList();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return data;
        }


        public string SetDataPrintLabelPrescription(RequestPrintLabelPrescription model, long AdmissionId, long OrganizationId, string EncounterId, int IsAdditional, string ProcessBy)
        {

            DataTable dt = new DataTable();
            int countdt = 0;
            List<string> listdata = new List<string>();
            string data = "";

            if (model.paramprintlabel.Count > 0)
            {
                string xmlprintlabel = ConvertPrintLabelPrescriptionToXML(model.paramprintlabel, AdmissionId, OrganizationId, EncounterId, IsAdditional, ProcessBy);
                try
                {
                    using (SqlConnection conn = new SqlConnection(Siloam.System.ApplicationSetting.ConnectionString))
                    {
                        conn.Open();
                        SqlCommand cmd = conn.CreateCommand();
                        cmd.CommandTimeout = 120;
                        cmd.CommandText = "spInsertPrintLabelPrescription";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("Presciption", xmlprintlabel));

                        using (var da = new SqlDataAdapter(cmd))
                        {
                            da.Fill(dt);
                        }
                        countdt = (from DataRow dr in dt.Rows
                                   select dr["result"].ToString()).Count();
                        if (countdt == 0)
                        {
                            data = "SUCCESS";
                        }
                        else if (countdt == 1)
                        {
                            data = (from DataRow dr in dt.Rows
                                    select dr["result"].ToString()).First();
                        }
                        else
                        {
                            listdata = (from DataRow dr in dt.Rows
                                        select dr["result"].ToString()).ToList();
                            data = String.Join("||", listdata);
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }

            return data;

        }

        public static string ConvertPrintLabelPrescriptionToXML(List<PharmacyPrintLabelPrescription> data, long AdmissionId, long OrganizationId, string EncounterId, int IsAdditional, string ProcessBy)
        {
            XDocument doc = new XDocument(new XDeclaration("1.0", "UTF-8", "yes"),
                new XElement("root",
                    from p in data
                    select new XElement("row",
                            new XAttribute("organization_id", OrganizationId),
                            new XAttribute("encounter_ticket_id", EncounterId),
                            new XAttribute("origin_prescompound_id", p.origin_prescompound_id.ToString()),
                            new XAttribute("origin_prescription_id", p.origin_prescription_id),
                            new XAttribute("item_name", p.item_name),
                            new XAttribute("quantity", p.quantity),
                            new XAttribute("uom_code", p.uom_code),
                            new XAttribute("dosage_id", p.dosage_id),
                            new XAttribute("dose_uom_id", p.dose_uom_id),
                            new XAttribute("frequency_id", p.frequency_id),
                            new XAttribute("dose_text", p.dose_text),
                            new XAttribute("administration_route_id", p.administration_route_id),
                            new XAttribute("remarks", p.remarks),
                            new XAttribute("hope_admission_id", AdmissionId),
                            new XAttribute("presc_type", p.presc_type),
                            new XAttribute("PatientInfo", p.PatientInfo),
                            new XAttribute("is_additional", IsAdditional),
                            new XAttribute("IsInternal", p.IsInternal),
                            new XAttribute("CompundNotes", p.CompundNotes),
                            new XAttribute("alternate_quantity", p.alternate_quantity),
                            new XAttribute("ar_item_id", p.ar_item_id),
                            new XAttribute("Createdby", ProcessBy)
                        )
                ));
            return doc.ToString();
        }

        public List<ReasonPharmacyModel> GetReasonPharmacy()
        {
            List<ReasonPharmacyModel> reasonlist = new List<ReasonPharmacyModel>();

            DataTable dt = new DataTable();

            try
            {
                using (SqlConnection conn = new SqlConnection(Siloam.System.ApplicationSetting.ConnectionString))
                {
                    conn.Open();
                    SqlCommand cmd = conn.CreateCommand();
                    cmd.CommandText = "spGet_PharmacyReason";
                    cmd.CommandType = CommandType.StoredProcedure;

                    using (var reader = cmd.ExecuteReader())
                    {
                        reasonlist = reader.MapToList<ReasonPharmacyModel>();
                    }

                    conn.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return reasonlist;
        }

        public string GetCountDrugCMS(long organizationId)
        {
            string result;

            try
            {
                using (SqlConnection conn = new SqlConnection(Siloam.System.ApplicationSetting.ConnectionString))
                {
                    conn.Open();
                    SqlCommand cmd = conn.CreateCommand();
                    cmd.CommandText = "spGET_COUNTDRUGCMS";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("organizationId", organizationId));

                    result = (string)cmd.ExecuteScalar();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }

        public string SetCompleteIssue(RequestCompleteIssue model, long AdmissionId, long OrganizationId, string EncounterId, string IssueCode, string NamaPenerima, string NoHp, string Relasi, string ProcessBy)
        {

            DataTable dt = new DataTable();
            int countdt = 0;
            List<string> listdata = new List<string>();
            string data = "";

            if (model.paramcompleteissue.Count > 0)
            {
                string xmlprintlabel = ConvertCompleteIssueToXML(model.paramcompleteissue);
                try
                {
                    using (SqlConnection conn = new SqlConnection(Siloam.System.ApplicationSetting.ConnectionString))
                    {
                        conn.Open();
                        SqlCommand cmd = conn.CreateCommand();
                        cmd.CommandTimeout = 120;
                        cmd.CommandText = "spInsert_completeIssue";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("OrganizationId", OrganizationId));
                        cmd.Parameters.Add(new SqlParameter("AdmissionId", AdmissionId));
                        cmd.Parameters.Add(new SqlParameter("EncounterId", EncounterId));
                        cmd.Parameters.Add(new SqlParameter("IssueCode", IssueCode));
                        cmd.Parameters.Add(new SqlParameter("NamaPenerima", NamaPenerima));
                        cmd.Parameters.Add(new SqlParameter("NoHp", NoHp));
                        cmd.Parameters.Add(new SqlParameter("Relasi", Relasi));
                        cmd.Parameters.Add(new SqlParameter("SubmitBy", ProcessBy));
                        cmd.Parameters.Add(new SqlParameter("Presciption", xmlprintlabel));

                        using (var da = new SqlDataAdapter(cmd))
                        {
                            da.Fill(dt);
                        }
                        countdt = (from DataRow dr in dt.Rows
                                   select dr["result"].ToString()).Count();
                        if (countdt == 0)
                        {
                            data = "SUCCESS";
                        }
                        else if (countdt == 1)
                        {
                            data = (from DataRow dr in dt.Rows
                                    select dr["result"].ToString()).First();
                        }
                        else
                        {
                            listdata = (from DataRow dr in dt.Rows
                                        select dr["result"].ToString()).ToList();
                            data = String.Join("||", listdata);
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }

            return data;

        }

        public static string ConvertCompleteIssueToXML(List<CompleteIssue> data)
        {
            XDocument doc = new XDocument(new XDeclaration("1.0", "UTF-8", "yes"),
                new XElement("root",
                    from p in data
                    select new XElement("row",
                            new XAttribute("origin_prescription_id", p.origin_prescription_id),
                            new XAttribute("IsCompound", p.IsCompound),
                            new XAttribute("IsTake", p.IsTake),
                            new XAttribute("IsReturn", p.IsReturn),
                            new XAttribute("ActionRemark", p.ActionRemark)
                        )
                ));
            return doc.ToString();
        }



        public PrescriptionHistory GetPrescriptionHistory(long organizationId, string encounterId, long addmissionId)
        {
            DataSet dt = new DataSet();
            PrescriptionHistory ph = new PrescriptionHistory();
            try
            {
                using (SqlConnection conn = new SqlConnection(Siloam.System.ApplicationSetting.ConnectionString))
                {
                    conn.Open();
                    SqlCommand cmd = conn.CreateCommand();
                    cmd.CommandText = "spGET_PrescriptionHistory";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("OrganizationId", organizationId));
                    cmd.Parameters.Add(new SqlParameter("EncounterId", encounterId));
                    cmd.Parameters.Add(new SqlParameter("AdmissionId", addmissionId));
                    using (var da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }



                    HeaderPrescriptionHistory header = new HeaderPrescriptionHistory();
                    header = (from DataRow dr in dt.Tables[0].Rows
                              select new HeaderPrescriptionHistory()
                              {
                                  encounter_id = Guid.Parse(dr["encounter_id"].ToString()),
                                  call_doctor = dr["call_doctor"].ToString(),
                                  modified_by = Int64.Parse(dr["modified_by"].ToString()),
                                  modified_date = DateTime.Parse(dr["modified_date"].ToString()),
                                  reason_remarks = dr["edit_reason"].ToString(),
                                  pharmacy_notes = dr["pharmacy_notes"].ToString(),

                              }
                              ).Single();

                    //List<DoctorPrescription> doctor_prescriptions;
                    // public List<PharmacistRelease> pharmacist_releases;

                    List<EditedPrescription> doctorPrescription = new List<EditedPrescription>();
                    doctorPrescription = (from DataRow dr in dt.Tables[1].Rows
                                          select new EditedPrescription()
                                          {
                                              item_name = dr["item_name"].ToString(),
                                              compound_name = dr["compound_name"].ToString(),
                                              edit_action = dr["edit_action"].ToString(),
                                              quantity = dr["quantity"].ToString().Replace(",", "."),
                                              uom = dr["uom"].ToString(),
                                              frequency = dr["frequency"].ToString(),
                                              dose = dr["dose"].ToString().Replace(",", "."),
                                              dose_uom = dr["dose_uom"].ToString(),
                                              administration_route = dr["route"].ToString(),
                                              created_date = DateTime.Parse(dr["create_date"].ToString()),
                                              instruction = dr["instruction"].ToString(),
                                              edit_reason = dr["edit_reason"].ToString(),
                                              is_compound = bool.Parse(dr["is_compound"].ToString()),
                                              is_compound_header = bool.Parse(dr["is_compoundHeader"].ToString()),
                                              is_additional = bool.Parse(dr["is_additional"].ToString()),
                                              is_consumable= bool.Parse(dr["is_consumable"].ToString()),
                                              item_sequence = Int16.Parse(dr["item_sequence"].ToString()),
                                              doctor_prescription_id = Guid.Parse(dr["doctor_prescription_id"].ToString()),
                                              phar_prescription_id = Int64.Parse(dr["phar_prescription_id"].ToString()),
                                              data_type= dr["data_type"].ToString(),
                                          }
                                          ).ToList();



                    List<EditedPrescription> pharmacistPrescription = new List<EditedPrescription>();
                    pharmacistPrescription = (from DataRow dr in dt.Tables[2].Rows
                                          select new EditedPrescription()
                                          {
                                              item_name = dr["item_name"].ToString(),
                                              compound_name = dr["compound_name"].ToString(),
                                              edit_action = dr["edit_action"].ToString(),
                                              quantity = dr["quantity"].ToString().Replace(",", "."),
                                              uom = dr["uom"].ToString(),
                                              frequency = dr["frequency"].ToString(),
                                              dose = dr["dose"].ToString().Replace(",", "."),
                                              dose_uom = dr["dose_uom"].ToString(),
                                              administration_route = dr["route"].ToString(),
                                              created_date = DateTime.Parse(dr["create_date"].ToString()),
                                              instruction = dr["instruction"].ToString(),
                                              edit_reason = dr["edit_reason"].ToString(),
                                              is_compound = bool.Parse(dr["is_compound"].ToString()),
                                              is_compound_header = bool.Parse(dr["is_compoundHeader"].ToString()),
                                              is_consumable = bool.Parse(dr["is_consumable"].ToString()),
                                              is_additional = bool.Parse(dr["is_additional"].ToString()),
                                              item_sequence = Int16.Parse(dr["item_sequence"].ToString()),
                                              doctor_prescription_id = Guid.Parse(dr["doctor_prescription_id"].ToString()),
                                              phar_prescription_id = Int64.Parse(dr["phar_prescription_id"].ToString()),
                                              data_type = dr["data_type"].ToString(),
                                          }
                                          ).ToList();

                    ph.header = header;
                    ph.doctor_prescriptions = doctorPrescription;
                    ph.pharmacist_prescriptions = pharmacistPrescription;


                }


            }catch(Exception ex)
            {
                throw ex;
            }

            return ph;

        }
    }
}