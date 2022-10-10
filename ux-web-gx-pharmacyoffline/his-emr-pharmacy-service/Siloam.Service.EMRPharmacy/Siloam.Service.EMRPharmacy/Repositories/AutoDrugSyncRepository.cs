using a = System;
using System.Collections.Generic;
using Siloam.Service.EMRPharmacy.Repositories.IRepositories;
using Siloam.Service.EMRPharmacy.Commons;
using Siloam.Service.EMRPharmacy.Models;
using Siloam.Service.EMRPharmacy.Models.ViewModels;
using Siloam.Service.EMRPharmacy.Models.AutoSync;
using System.Linq;
using System.Xml.Linq;
using System.Data.SqlClient;
using System.Data;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Siloam.Service.EMRPharmacy.Repositories
{

    public class AutoDrugSyncRepository : DatabaseConfig, IAutoDrugSyncRepository
    {

        public AutoDrugSyncRepository() : base() { }

        public AutoDrugSyncRepository(DatabaseContext context) : base(context) { }

        public string SubmitAppropriatenessReviewCentral(InsertAppropriateness model)
        {
            string data = "";
            CentralAppropriateness temp = new CentralAppropriateness();
            try
            {
                temp = (from a in Context.AppropriateSet
                        where a.organization_id == model.OrganizationId && a.patient_id == model.PatientId && a.admission_id == model.AdmissionId && a.encounter_ticket_id == model.EncounterId
                        select a).FirstOrDefault();
                if (temp != null)
                {
                    temp.is_active = model.IsActive;
                    temp.appropriate_date = a.DateTime.Now;
                    temp.appropriate_by = model.UserId.ToString();
                    Context.Update(temp);
                    Context.SaveChanges();
                }
                else
                {
                    CentralAppropriateness insert = new CentralAppropriateness();
                    insert.central_appropriateness_id = a.Guid.NewGuid();
                    insert.organization_id = model.OrganizationId;
                    insert.patient_id = model.PatientId;
                    insert.admission_id = model.AdmissionId;
                    insert.encounter_ticket_id = model.EncounterId;
                    insert.is_active = model.IsActive;
                    insert.appropriate_date = a.DateTime.Now;
                    insert.appropriate_by = model.UserId.ToString();
                    Context.Add(insert);
                    Context.SaveChanges();
                }
                data = "SUCCESS";
            }
            catch (a.Exception ex)
            {
                data = ex.Message;
            }
            return data;
        }

        public string SendReadyPickupMySiloam(string JsonString)
        {
            string data = "";
            try
            {
                var content = new StringContent(JsonString, Encoding.UTF8, "application/json");
                HttpClient http = new HttpClient();
                http.BaseAddress = new a.Uri(ValueStorage.MySiloamUrlSync);

                http.DefaultRequestHeaders.Accept.Clear();
                http.DefaultRequestHeaders.Accept.Add(new a.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                var task = Task.Run(async () =>
                {
                    return await http.PutAsync(ValueStorage.MySiloamUrlSync + "/api/v2/mobile/doctors/drug-order/ready", content);
                });

                data = task.Result.Content.ReadAsStringAsync().Result;
                //JObject Response = (JObject)JsonConvert.DeserializeObject<dynamic>(result);
                //data = Response.Property("message").Value.ToString();
            }
            catch (a.Exception ex)
            {
                data = "KONEKSI KE MYSILOAM GAGAL. Exception: " + ex.Message;
                return data;
            }

            return data;
        }

        public string InsertLogReadyPickup(RequestReadyPickup model, string Status, string JsonRequest, string JsonResponse, a.Guid AppointmentId, bool IsSuccess)
        {
            string data = "";
            LogReadyPickup log = new LogReadyPickup();
            AidoDrugTicket temp = new AidoDrugTicket();
            try
            {
                if (IsSuccess)
                {
                    temp = (from aido in Context.AidoDrugSet
                            where aido.is_active == true && aido.organization_id == model.OrganizationId && aido.patient_id == model.PatientId && aido.admission_id == model.AdmissionId && aido.encounter_id == model.EncounterId
                            select aido).Single();

                    if (temp == null)
                    {
                        data = "DATA NOT FOUND";
                    }
                    else
                    {
                        temp.is_readydrug = true;
                        Context.Update(temp);
                        Context.SaveChanges();
                    }
                }
                log.LogReadyPickupId = 0;
                log.OrganizationId = model.OrganizationId;
                log.PatientId = model.PatientId;
                log.AdmissionId = model.AdmissionId;
                log.DoctorId = model.DoctorId;
                log.EncounterId = model.EncounterId;
                log.AppointmentId = AppointmentId;
                log.Status = Status;
                log.JsonRequest = JsonRequest;
                log.JsonResponse = JsonResponse;
                log.CreateDate = a.DateTime.Now;
                log.CreateBy = model.UserId.ToString();
                Context.Add(log);
                Context.SaveChanges();
                data = "SUCCESS";
            }
            catch (a.Exception ex)
            {
                data = ex.Message;
            }
            return data;
        }

        public string UpdateDrugsReadyPickup(RequestReadyPickup model, string Status, string JsonRequest, string JsonResponse, a.Guid AppointmentId, bool IsSuccess)
        {
            string data = "";
            LogReadyPickup log = new LogReadyPickup();
            DrugsToready temp = new DrugsToready();
            try
            {
                if (IsSuccess)
                {
                    temp = (from aido in Context.DrugsToreadySet
                            where aido.organization_id == model.OrganizationId &&  aido.admission_id == model.AdmissionId && aido.encounter_ticket_id == model.EncounterId
                            select aido).Single();

                    if (temp == null)
                    {
                        data = "DATA NOT FOUND";
                    }
                    else
                    {
                        temp.is_ready = true;
                        Context.Update(temp);
                        Context.SaveChanges();
                    }
                }
                //log.LogReadyPickupId = 0;
                //log.OrganizationId = model.OrganizationId;
                //log.PatientId = model.PatientId;
                //log.AdmissionId = model.AdmissionId;
                //log.DoctorId = model.DoctorId;
                //log.EncounterId = model.EncounterId;
                //log.AppointmentId = AppointmentId;
                //log.Status = Status;
                //log.JsonRequest = JsonRequest;
                //log.JsonResponse = JsonResponse;
                //log.CreateDate = a.DateTime.Now;
                //log.CreateBy = model.UserId.ToString();
                //Context.Add(log);
                //Context.SaveChanges();
                data = "SUCCESS";
            }
            catch (a.Exception ex)
            {
                data = ex.Message;
            }
            return data;
        }

        public List<TeleconsulStock> GetTeleconsulStock(long OrganizationId)
        {
            List<TeleconsulStock> data = new List<TeleconsulStock>();
            DataTable dt = new DataTable();
            try
            {
                using (SqlConnection conn = new SqlConnection(Siloam.System.ApplicationSetting.ConnectionString))
                {
                    conn.Open();
                    SqlCommand cmd = conn.CreateCommand();
                    cmd.CommandText = "spGetTeleconsulItemStockFlag";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("OrganizationId", OrganizationId));
                    using (var da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }
                    data = (from DataRow dr in dt.Rows
                            select new TeleconsulStock
                            {
                                OrganizationId = long.Parse(dr["OrganizationId"].ToString()),
                                PatientId = long.Parse(dr["PatientId"].ToString()),
                                AdmissionId = long.Parse(dr["AdmissionId"].ToString()),
                                DoctorId = long.Parse(dr["DoctorId"].ToString()),
                                EncounterId = a.Guid.Parse(dr["EncounterId"].ToString()),
                                EmptyFlag = int.Parse(dr["EmptyFlag"].ToString()),
                                latitude = dr["latitude"].ToString().Replace(',', '.'),
                                longitude = dr["longitude"].ToString().Replace(',', '.'),
                                city = dr["city"].ToString(),
                                province = dr["province"].ToString(),
                                appointmentid = a.Guid.Parse(dr["appointmentid"].ToString()),
                                channelid = dr["channelid"].ToString(),
                                deliveryaddress = dr["deliveryaddress"].ToString(),
                                deliverynotes = dr["deliverynotes"].ToString(),
                                IsResend = bool.Parse(dr["IsResend"].ToString()),
                                PayerId = long.Parse(dr["PayerId"].ToString()),
                                TransactionId = dr["TransactionId"].ToString(),
                                IsPrescription = int.Parse(dr["IsPrescription"].ToString()),
                                IsPayer = int.Parse(dr["IsPayer"].ToString()),
                                isDilution = bool.Parse(dr["is_dilution"].ToString()),
                                isInteraction = bool.Parse(dr["is_interaction"].ToString())
                            }).ToList();
                }
            }
            catch (a.Exception ex)
            {
                throw ex;
            }
            return data;
        }

        public static string ConvertPrescriptionToXML(List<PrescriptionCentral> data)
        {
            XDocument doc = new XDocument(new XDeclaration("1.0", "UTF-8", "yes"),
                new XElement("root",
                    from p in data
                    select new XElement("row",
                            new XAttribute("prescription_id", p.prescription_id),
                            new XAttribute("organization_id", p.organization_id),
                            new XAttribute("admission_id", p.admission_id),
                            new XAttribute("doctor_id", p.doctor_id),
                            new XAttribute("encounter_ticket_id", p.encounter_ticket_id),
                            new XAttribute("item_id", p.item_id),
                            new XAttribute("quantity", p.quantity),
                            new XAttribute("issued_qty", p.issued_qty),
                            new XAttribute("uom_id", p.uom_id),
                            new XAttribute("frequency_id", p.frequency_id),
                            new XAttribute("dosage_id", p.dosage_id),
                            new XAttribute("dose_uom_id", p.dose_uom_id),
                            new XAttribute("dose_text", p.dose_text),
                            new XAttribute("administration_route_id", p.administration_route_id),
                            new XAttribute("iteration", p.iteration),
                            new XAttribute("remarks", p.remarks),
                            new XAttribute("is_routine", p.is_routine),
                            new XAttribute("is_consumables", p.is_consumables),
                            new XAttribute("item_sequence", p.item_sequence)
                        )
                ));
            return doc.ToString();
        }
        public string InsertCentralPrescription(List<PrescriptionCentral> model, long UserId, string Notes)
        {
            DataTable dt = new DataTable();
            string data;
            string xmlInsertPrescription = ConvertPrescriptionToXML(model);
            try
            {
                using (SqlConnection conn = new SqlConnection(Siloam.System.ApplicationSetting.ConnectionString))
                {
                    conn.Open();
                    SqlCommand cmd = conn.CreateCommand();
                    cmd.CommandText = "spInsertCentralPrescription";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("PrescriptionData", xmlInsertPrescription));
                    cmd.Parameters.Add(new SqlParameter("UserId", UserId));
                    cmd.Parameters.Add(new SqlParameter("Notes", Notes));
                    using (var da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }
                    data = (from DataRow dr in dt.Rows
                            select dr["Result"].ToString()).Single();
                }
            }
            catch (a.Exception ex)
            {
                throw ex;
            }
            return data;
        }

        public string ResendPrescription(long OrganizationId, long PatientId, long AdmissionId, a.Guid EncounterId, long UserId)
        {
            DataTable dt = new DataTable();
            string data;
            try
            {
                using (SqlConnection conn = new SqlConnection(Siloam.System.ApplicationSetting.ConnectionString))
                {
                    conn.Open();
                    SqlCommand cmd = conn.CreateCommand();
                    cmd.CommandText = "spInsertResendPrescription";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("OrganizationId", OrganizationId));
                    cmd.Parameters.Add(new SqlParameter("PatientId", PatientId));
                    cmd.Parameters.Add(new SqlParameter("AdmissionId", AdmissionId));
                    cmd.Parameters.Add(new SqlParameter("EncounterId", EncounterId));
                    cmd.Parameters.Add(new SqlParameter("UserId", UserId));
                    using (var da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }
                    data = (from DataRow dr in dt.Rows
                            select dr["Result"].ToString()).Single();
                }
            }
            catch (a.Exception ex)
            {
                throw ex;
            }
            return data;
        }

        public string ResendOrCancelPrescription(long OrganizationId, long PatientId, long AdmissionId, a.Guid EncounterId, long UserId, string Notes, bool isCancel)
        {
            DataTable dt = new DataTable();
            string data;
            try
            {
                using (SqlConnection conn = new SqlConnection(Siloam.System.ApplicationSetting.ConnectionString))
                {
                    conn.Open();
                    SqlCommand cmd = conn.CreateCommand();
                    cmd.CommandText = "spInsertResendORCancelPrescription";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("OrganizationId", OrganizationId));
                    cmd.Parameters.Add(new SqlParameter("PatientId", PatientId));
                    cmd.Parameters.Add(new SqlParameter("AdmissionId", AdmissionId));
                    cmd.Parameters.Add(new SqlParameter("EncounterId", EncounterId));
                    cmd.Parameters.Add(new SqlParameter("UserId", UserId));
                    cmd.Parameters.Add(new SqlParameter("Notes", Notes));
                    cmd.Parameters.Add(new SqlParameter("isCancel", isCancel));
                    using (var da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }
                    data = (from DataRow dr in dt.Rows
                            select dr["Result"].ToString()).Single();
                }
            }
            catch (a.Exception ex)
            {
                throw ex;
            }
            return data;
        }
        public static string ConverInsertQuestionToXML(List<InsertQuestion> data)
        {
            XDocument doc = new XDocument(new XDeclaration("1.0", "UTF-8", "yes"),
                new XElement("root",
                    from p in data
                    select new XElement("row",
                            new XAttribute("EncounterId", p.EncounterId),
                            new XAttribute("OrganizationId", p.OrganizationId),
                            new XAttribute("PatientId", p.PatientId),
                            new XAttribute("AdmissionId", p.AdmissionId),
                            new XAttribute("DoctorId", p.DoctorId),
                            new XAttribute("CategoryId", p.CategoryId),
                            new XAttribute("DeliveryAddress", p.DeliveryAddress),
                            new XAttribute("DeliveryNotes", p.DeliveryNotes),
                            new XAttribute("Question", p.Question)
                        )
                ));
            return doc.ToString();
        }

        public string InsertQuestion(List<InsertQuestion> model)
        {
            DataTable dt = new DataTable();
            string data;
            string xmlInsertQuestion = ConverInsertQuestionToXML(model);
            try
            {
                using (SqlConnection conn = new SqlConnection(Siloam.System.ApplicationSetting.ConnectionString))
                {
                    conn.Open();
                    SqlCommand cmd = conn.CreateCommand();
                    cmd.CommandText = "spInsertTeleconsulQuestion";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("PatientData", xmlInsertQuestion));
                    cmd.Parameters.Add(new SqlParameter("Source", model.First().Source));
                    using (var da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }
                    data = (from DataRow dr in dt.Rows
                            select dr["Result"].ToString()).Single();
                }
            }
            catch (a.Exception ex)
            {
                throw ex;
            }
            return data;
        }

        public string InsertSkipDrug(InsertSkipDrug model)
        {
            DataTable dt = new DataTable();
            string data;
            try
            {
                using (SqlConnection conn = new SqlConnection(Siloam.System.ApplicationSetting.ConnectionString))
                {
                    conn.Open();
                    SqlCommand cmd = conn.CreateCommand();
                    cmd.CommandText = "spInsertSkipDrug";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("OrganizationId", model.OrganizationId));
                    cmd.Parameters.Add(new SqlParameter("PatientId", model.PatientId));
                    cmd.Parameters.Add(new SqlParameter("AdmissionId", model.AdmissionId));
                    cmd.Parameters.Add(new SqlParameter("EncounterId", model.EncounterId));
                    cmd.Parameters.Add(new SqlParameter("IsCancel", model.IsCancel));
                    cmd.Parameters.Add(new SqlParameter("Remarks", model.Remarks));
                    cmd.Parameters.Add(new SqlParameter("DeliveryAddress", model.DeliveryAddress));
                    cmd.Parameters.Add(new SqlParameter("DeliveryNotes", model.DeliveryNotes));
                    cmd.Parameters.Add(new SqlParameter("Source", model.Source));
                    using (var da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }
                    data = (from DataRow dr in dt.Rows
                            select dr["Result"].ToString()).Single();
                }
            }
            catch (a.Exception ex)
            {
                throw ex;
            }
            return data;
        }

        public string UpdateTicketSync(long OrganizationId, long PatientId, long AdmissionId, a.Guid EncounterId, bool IsSuccess)
        {
            DataTable dt = new DataTable();
            string data;
            try
            {
                using (SqlConnection conn = new SqlConnection(Siloam.System.ApplicationSetting.ConnectionString))
                {
                    conn.Open();
                    SqlCommand cmd = conn.CreateCommand();
                    cmd.CommandText = "spUpdateTeleconsulTicketSync";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("OrganizationId", OrganizationId));
                    cmd.Parameters.Add(new SqlParameter("PatientId", PatientId));
                    cmd.Parameters.Add(new SqlParameter("AdmissionId", AdmissionId));
                    cmd.Parameters.Add(new SqlParameter("EncounterId", EncounterId));
                    cmd.Parameters.Add(new SqlParameter("IsSuccess", IsSuccess));
                    using (var da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }
                    data = (from DataRow dr in dt.Rows
                            select dr["Result"].ToString()).Single();
                }
            }
            catch (a.Exception ex)
            {
                throw ex;
            }
            return data;
        }

        public TeleconsultationDeliveryHeader GetDeliveryFee(string JsonString)
        {
            string data = "";
            TeleconsultationDeliveryHeader listDeliveryFee = new TeleconsultationDeliveryHeader();
            try
            {
                var content = new StringContent(JsonString, Encoding.UTF8, "application/json");
                HttpClient http = new HttpClient();
                http.BaseAddress = new a.Uri(ValueStorage.UrlDeliveryFee);

                http.DefaultRequestHeaders.Accept.Clear();
                http.DefaultRequestHeaders.Accept.Add(new a.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                var task = Task.Run(async () =>
                {
                    return await http.PostAsync(ValueStorage.UrlDeliveryFee + "/calculatedrugdeliveryfee", content);
                });

                data = task.Result.Content.ReadAsStringAsync().Result;
                var objDeliveryFee = JsonConvert.DeserializeObject<ResultTeleconsultationDelivery>(data);
                listDeliveryFee = objDeliveryFee.list;
            }
            catch (a.Exception ex)
            {
                throw ex;
            }

            return listDeliveryFee;
        }

        public string SyncDrugMySiloam(string JsonString)
        {
            string data = "";
            try
            {
                var content = new StringContent(JsonString, Encoding.UTF8, "application/json");
                HttpClient http = new HttpClient();
                http.BaseAddress = new a.Uri(ValueStorage.MySiloamUrlSync);

                http.DefaultRequestHeaders.Accept.Clear();
                http.DefaultRequestHeaders.Accept.Add(new a.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                var task = Task.Run(async () =>
                {
                    return await http.PostAsync(ValueStorage.MySiloamUrlSync + "/api/v2/mobile/doctors/drug-order", content);
                });

                data = task.Result.Content.ReadAsStringAsync().Result;
                //JObject Response = (JObject)JsonConvert.DeserializeObject<dynamic>(result);
                //data = Response.Property("message").Value.ToString();
            }
            catch (a.Exception ex)
            {
                data = "KONEKSI KE MYSILOAM GAGAL. Exception: " + ex.Message;
                return data;
            }

            return data;
        }

        public int GetCountAIDOOrder(long OrganizationId, long PatientId, long AdmissionId, a.Guid EncounterId)
        {
            int data = 0;
            using (var context = new DatabaseContext(ContextOption))
            {
                data = (from a in context.AidoDrugSet
                        where a.is_active == true && a.organization_id == OrganizationId && a.patient_id == PatientId && a.admission_id == AdmissionId && a.encounter_id == EncounterId
                        select a).Count();
            }
            return data;
        }

        public a.Guid GetSiloamTrxId(long OrganizationId, long PatientId, long AdmissionId, a.Guid EncounterId)
        {
            a.Guid data = new a.Guid();
            int countdata = 0;
            using (var context = new DatabaseContext(ContextOption))
            {
                countdata = (from a in context.AidoDrugSet
                             where a.is_active == true && a.organization_id == OrganizationId && a.patient_id == PatientId && a.admission_id == AdmissionId && a.encounter_id == EncounterId && a.is_payment == true
                             select a).Count();
                if (countdata == 0)
                {
                    data = a.Guid.Empty;
                }
                else
                {
                    data = (from a in context.AidoDrugSet
                            where a.is_active == true && a.organization_id == OrganizationId && a.patient_id == PatientId && a.admission_id == AdmissionId && a.encounter_id == EncounterId && a.is_payment == true
                            select a.siloam_trx_id).First();
                }
            }
            return data;
        }

        public string InsertDataTicket(long OrganizationId, long PatientId, long AdmissionId, a.Guid EncounterId, string JsonRequest, string JsonResponse, a.Guid SiloamTrxId, string ChannelId)
        {
            DataTable dt = new DataTable();
            string data;
            try
            {
                using (SqlConnection conn = new SqlConnection(Siloam.System.ApplicationSetting.ConnectionString))
                {
                    conn.Open();
                    SqlCommand cmd = conn.CreateCommand();
                    cmd.CommandText = "spInsertAidoTicket";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("OrganizationId", OrganizationId));
                    cmd.Parameters.Add(new SqlParameter("PatientId", PatientId));
                    cmd.Parameters.Add(new SqlParameter("AdmissionId", AdmissionId));
                    cmd.Parameters.Add(new SqlParameter("EncounterId", EncounterId));
                    cmd.Parameters.Add(new SqlParameter("JsonRequest", JsonRequest));
                    cmd.Parameters.Add(new SqlParameter("JsonResponse", JsonResponse));
                    cmd.Parameters.Add(new SqlParameter("SiloamTrxId", SiloamTrxId));
                    cmd.Parameters.Add(new SqlParameter("ChannelId", ChannelId));
                    using (var da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }
                    data = (from DataRow dr in dt.Rows
                            select dr["Result"].ToString()).Single();
                }
            }
            catch (a.Exception ex)
            {
                throw ex;
            }
            return data;
        }

        public string InsertDataLogFailed(long OrganizationId, long PatientId, long AdmissionId, a.Guid EncounterId, string token, string JsonRequest, string JsonResponse, string ErrorMsg, string ChannelId)
        {
            string result = "";
            AidoFailed data = new AidoFailed();
            data.aido_failed_sync_id = 0;
            data.organization_id = OrganizationId;
            data.patient_id = PatientId;
            data.admission_id = AdmissionId;
            data.encounter_id = EncounterId;
            data.token = token;
            data.jsonrequest_send_drug = JsonRequest;
            data.jsonresponse_send_drug = JsonResponse;
            data.error_message = ErrorMsg;
            data.created_date = a.DateTime.Now;
            data.channel_id = ChannelId;
            Context.Add(data);
            Context.SaveChanges();
            return result;
        }

        public List<ItemPriceAuto> GetItemPriceAuto(long OrganizationId, long PatientId, long AdmissionId, a.Guid EncounterId, bool IsResend)
        {
            List<ItemPriceAuto> data = new List<ItemPriceAuto>();
            DataTable dt = new DataTable();
            try
            {
                using (SqlConnection conn = new SqlConnection(Siloam.System.ApplicationSetting.ConnectionString))
                {
                    conn.Open();
                    SqlCommand cmd = conn.CreateCommand();
                    cmd.CommandText = "spGetItemSyncTeleconsultationAuto";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 120;
                    cmd.Parameters.Add(new SqlParameter("OrganizationId", OrganizationId));
                    cmd.Parameters.Add(new SqlParameter("PatientId", PatientId));
                    cmd.Parameters.Add(new SqlParameter("AdmissionId", AdmissionId));
                    cmd.Parameters.Add(new SqlParameter("EncounterId", EncounterId));
                    cmd.Parameters.Add(new SqlParameter("IsResend", IsResend));
                    using (var da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }
                    data = (from DataRow dr in dt.Rows
                            select new ItemPriceAuto
                            {
                                SalesItemId = long.Parse(dr["SalesItemId"].ToString()),
                                SalesItemName = dr["SalesItemName"].ToString(),
                                Uom = dr["Uom"].ToString(),
                                quantity = dr["quantity"].ToString().Replace(',', '.'),
                                SubTotal = dr["SubTotal"].ToString().Replace(',', '.'),
                                TotalPrice = dr["TotalPrice"].ToString().Replace(',', '.'),
                                PatientNet = dr["PatientNet"].ToString().Replace(',', '.'),
                                PatientNetTotal = dr["PatientNetTotal"].ToString().Replace(',', '.'),
                                PayerNet = dr["PayerNet"].ToString().Replace(',', '.'),
                                PayerNetTotal = dr["PayerNetTotal"].ToString().Replace(',', '.'),
                                Frequency = dr["Frequency"].ToString(),
                                Instruction = dr["Instruction"].ToString(),
                                IsSendAvailable = bool.Parse(dr["IsSendAvailable"].ToString())
                            }).ToList();
                }
            }
            catch (a.Exception ex)
            {
                throw ex;
            }
            return data;
        }

        public string GetSettingAidoPharmacyId(long OrganizationId)
        {
            string data = "";
            using (var context = new DatabaseContext(ContextOption))
            {
                data = (from a in context.SettingSet
                        where a.is_active == true && a.organization_id == OrganizationId && a.setting_name == "AIDO_PHARMACY_ID"
                        select a.setting_value).First();
            }
            return data;
        }

        public string GetTransactionId(long OrganizationId, long PatientId, long AdmissionId, long DoctorId)
        {
            DataTable dt = new DataTable();
            string data;
            try
            {
                using (SqlConnection conn = new SqlConnection(Siloam.System.ApplicationSetting.ConnectionString))
                {
                    conn.Open();
                    SqlCommand cmd = conn.CreateCommand();
                    cmd.CommandText = "spGetAidoTransactionId";
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
                            select dr["transaction_id"].ToString()).Single();
                }
            }
            catch (a.Exception ex)
            {
                throw ex;
            }
            return data;
        }

        public string GenerateJSONWebToken(long OrganizationId, long PatientId, long AdmissionId, long DoctorId)
        {
            string token = "";
            try
            {
                var utc0 = new a.DateTime(1970, 1, 1, 0, 0, 0, 0, a.DateTimeKind.Utc);
                var issueTime = a.DateTime.Now;
                var iat = (int)issueTime.Subtract(utc0).TotalSeconds;
                var exp = (int)issueTime.AddMinutes(60).Subtract(utc0).TotalSeconds;

                AidoPayload payload = new AidoPayload();
                AidoPayloadData data = new AidoPayloadData();
                data.partner = "siloam";
                data.orderId = GetTransactionId(OrganizationId, PatientId, AdmissionId, DoctorId); //"552";
                data.pharmacyId = GetSettingAidoPharmacyId(OrganizationId);
                payload.data = data;
                payload.iat = iat;
                payload.exp = exp;

                JWT jwt = new JWT();
                token = jwt.Encode(payload, JwtHashAlgorithm.HS256);
            }
            catch (a.Exception ex)
            {
                token = "";
            }

            return token;
        }

        public string GenerateJSONWebTokenNew(long OrganizationId, long PatientId, long AdmissionId, long DoctorId)
        {
            string token = "";
            try
            {
                var utc0 = new a.DateTime(1970, 1, 1, 0, 0, 0, 0, a.DateTimeKind.Utc);
                var issueTime = a.DateTime.Now;
                var iat = (int)issueTime.Subtract(utc0).TotalSeconds;
                var exp = (int)issueTime.AddMinutes(60).Subtract(utc0).TotalSeconds;

                AidoPayloadNew payload = new AidoPayloadNew();

                payload.role = "partner";
                payload.partner = GetSiloID(DoctorId, OrganizationId);
                payload.iat = iat;
                payload.exp = exp;
                payload.aud = "aidos";
                payload.iss = "aidos";

                JWT jwt = new JWT();
                token = jwt.EncodeV2(payload, JwtHashAlgorithm.HS256, ValueStorage.AidoSecretV2);
            }
            catch (a.Exception ex)
            {
                token = "";
            }

            return token;
        }

        public string GetSiloID(long UserId, long OrganizationId)
        {
            a.Guid data = new a.Guid();
            using (var context = new DatabaseContext(ContextOption))
            {
                data = (from userrole in context.UserRoleSet
                        where userrole.hope_user_id == UserId && userrole.hope_organization_id == OrganizationId && userrole.role_id == a.Guid.Parse("46754A28-7A4B-42A3-B36F-B817D7978B11")
                        select userrole.user_id).FirstOrDefault();
            }
            return data.ToString();
        }

        public string AidoSyncPrescription(string JsonString, long OrganizationId, long PatientId, long AdmissionId, long DoctorId, string token)
        {
            string data = "";
            string result = "";
            try
            {
                //string token = GenerateJSONWebToken(OrganizationId, PatientId, AdmissionId, DoctorId);
                var content = new StringContent(JsonString, Encoding.UTF8, "application/json");
                HttpClient http = new HttpClient();
                http.BaseAddress = new a.Uri(ValueStorage.AidoUrlSyncNew);

                http.DefaultRequestHeaders.Accept.Clear();
                http.DefaultRequestHeaders.Accept.Add(new a.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                http.DefaultRequestHeaders.Add("token", token);

                var task = Task.Run(async () =>
                {
                    return await http.PostAsync(ValueStorage.AidoUrlSyncNew, content);
                });

                result = task.Result.Content.ReadAsStringAsync().Result;
                //JObject Response = (JObject)JsonConvert.DeserializeObject<dynamic>(result);
                //data = Response.Property("status").Value.ToString();
            }
            catch (a.Exception ex)
            {
                data = "KONEKSI KE AIDO GAGAL. Exception: " + ex.Message;
                return data;
            }

            return result;
        }

        public string InsertLogDeliveryFee(long OrganizationId, long PatientId, long AdmissionId, a.Guid EncounterId, string JsonRequest, string JsonResult, decimal Distance)
        {
            string result = "";
            LogDeliveryFee model = new LogDeliveryFee();
            try
            {
                model.log_delivery_fee_id = 0;
                model.organization_id = OrganizationId;
                model.patient_id = PatientId;
                model.admission_id = AdmissionId;
                model.encounter_id = EncounterId;
                model.json_request = JsonRequest;
                model.json_result = JsonResult;
                model.distance = Distance;
                model.created_date = a.DateTime.Now;
                Context.Add(model);
                Context.SaveChanges();
                result = "SUCCESS";
            }
            catch(a.Exception ex)
            {
                result = ex.Message;
            }
            return result;
        }


        public string SendReadyPickupAido(string JsonString, string token)
        {
            string data = "";
            string result = "";
            try
            {
                //string token = GenerateJSONWebToken(OrganizationId, PatientId, AdmissionId, DoctorId);
                var content = new StringContent(JsonString, Encoding.UTF8, "application/json");
                HttpClient http = new HttpClient();
                http.BaseAddress = new a.Uri(ValueStorage.AidoUrlSyncDrug);

                http.DefaultRequestHeaders.Accept.Clear();
                http.DefaultRequestHeaders.Accept.Add(new a.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                http.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);

                var task = Task.Run(async () =>
                {
                    return await http.PostAsync(ValueStorage.AidoUrlSyncDrug, content);
                });

                result = task.Result.Content.ReadAsStringAsync().Result;
                //JObject Response = (JObject)JsonConvert.DeserializeObject<dynamic>(result);
                //data = Response.Property("status").Value.ToString();
            }
            catch (a.Exception ex)
            {
                data = "KONEKSI KE AIDO GAGAL. Exception: " + ex.Message;
                return data;
            }

            return result;
        }

        public string SendPickupAido(a.Guid AppointmentId, long OrganizationId, long PatientId, long AdmissionId, long DoctorId, a.Guid EncounterId)
        {
            string data = "";
            string result = "";
            try
            {
                string token = "";
                token = GenerateJSONWebTokenNew(OrganizationId, PatientId, AdmissionId, DoctorId);

                RequestReadyPickupAido aido = new RequestReadyPickupAido();

                //aido.siloamTrxId = AppointmentId.ToString();
                aido.siloamTrxId = GetSiloamTrxId(OrganizationId, PatientId, AdmissionId, EncounterId).ToString();
                string JsonStringAido = JsonConvert.SerializeObject(aido);
                result = SendReadyPickupAido(JsonStringAido, token);
            }
            catch (a.Exception ex)
            {
                data = "KONEKSI KE AIDO GAGAL. Exception: " + ex.Message;
                return data;
            }

            return result;
        }

        public string AutoSync(long OrganizationId)
        {
            string data = "SUCCESS";
            string ListID = "";
            string jsonDelivery = "";
            string jsonResultDelivery = "";
            decimal distance = 0;
            List<TeleconsulStock> init = new List<TeleconsulStock>();
            List<TeleconsulStock> empty = new List<TeleconsulStock>();
            List<TeleconsulStock> run = new List<TeleconsulStock>();
            List<TeleconsulStock> dilution = new List<TeleconsulStock>();
            List<TeleconsulStock> interaction = new List<TeleconsulStock>();
            List<InsertQuestion> listQuestion = new List<InsertQuestion>();
            List<DeliveryFeeType> deliveryempty = new List<DeliveryFeeType>();
            List<MySiloamDrugNew> drugempty = new List<MySiloamDrugNew>();

            TeleconsultationDeliveryHeader dummyheader = new TeleconsultationDeliveryHeader();
            TeleconsultationDelivery dummy = new TeleconsultationDelivery();
            List<TeleconsultationDelivery> listdummy = new List<TeleconsultationDelivery>();

            try
            {
                bool LiveAidoDelivery = GetSettingLiveAidoDelivery(OrganizationId).ToUpper().Equals("TRUE");
                //Running Lab & Rad migration without drug
                FilterDataLabRad(OrganizationId);
                dummy.amount = 0;
                dummy.estimation = 0;
                dummy.price_header_id = 0;
                dummy.price_type_name = "SELF PICKUP";
                dummy.remarks = "";
                listdummy.Add(dummy);
                dummyheader.distance = 0;
                dummyheader.detail = listdummy;

                init                = GetTeleconsulStock(OrganizationId);
                List<string> EncId  = new List<string>();
                EncId               = (from a in init select a.EncounterId.ToString()).ToList();
                ListID              = a.String.Join(',', EncId);
                empty               = init.Where(x => x.EmptyFlag > 0).ToList();
                run                 = init.Where(x => x.EmptyFlag == 0 
                                                && ((!x.IsResend && !x.isDilution && !x.isInteraction) 
                                                || x.IsResend)).ToList();
                dilution            = init.Where(x => x.EmptyFlag == 0 && !x.IsResend && x.isDilution).ToList();
                interaction         = init.Where(x => x.EmptyFlag == 0 && !x.IsResend && !x.isDilution && x.isInteraction).ToList();

                //jika ada stok yang kosong, insert question
                if (dilution.Count > 0)
                {
                    foreach(TeleconsulStock x in dilution)
                    {
                        InsertQuestion tempDilution = new InsertQuestion();
                        tempDilution.EncounterId = x.EncounterId;
                        tempDilution.OrganizationId = x.OrganizationId;
                        tempDilution.PatientId = x.PatientId;
                        tempDilution.DoctorId = x.DoctorId;
                        tempDilution.AdmissionId = x.AdmissionId;
                        tempDilution.CategoryId = 6;//dilution
                        tempDilution.Question = "";
                        tempDilution.DeliveryAddress = x.deliveryaddress;
                        tempDilution.DeliveryNotes = x.deliverynotes;
                        tempDilution.Source = "AutoSync";
                        listQuestion.Add(tempDilution);
                        string updatesuccess = UpdateTicketSync(x.OrganizationId, x.PatientId, x.AdmissionId, x.EncounterId, false);
                    }
                    InsertQuestion(listQuestion);
                }
                if (interaction.Count > 0)
                {
                    foreach (TeleconsulStock x in interaction)
                    {
                        InsertQuestion tempInteraction = new InsertQuestion();
                        tempInteraction.EncounterId = x.EncounterId;
                        tempInteraction.OrganizationId = x.OrganizationId;
                        tempInteraction.PatientId = x.PatientId;
                        tempInteraction.DoctorId = x.DoctorId;
                        tempInteraction.AdmissionId = x.AdmissionId;
                        tempInteraction.CategoryId = 7;//interaction
                        tempInteraction.Question = "";
                        tempInteraction.DeliveryAddress = x.deliveryaddress;
                        tempInteraction.DeliveryNotes = x.deliverynotes;
                        tempInteraction.Source = "AutoSync";
                        listQuestion.Add(tempInteraction);
                        string updatesuccess = UpdateTicketSync(x.OrganizationId, x.PatientId, x.AdmissionId, x.EncounterId, false);
                    }
                    InsertQuestion(listQuestion);
                }
                if (empty.Count > 0)
                {
                    foreach (TeleconsulStock x in empty)
                    {
                        InsertQuestion temp = new InsertQuestion();
                        temp.EncounterId = x.EncounterId;
                        temp.OrganizationId = x.OrganizationId;
                        temp.PatientId = x.PatientId;
                        temp.DoctorId = x.DoctorId;
                        temp.AdmissionId = x.AdmissionId;
                        temp.CategoryId = 4;
                        temp.Question = "";
                        temp.DeliveryAddress = x.deliveryaddress;
                        temp.DeliveryNotes = x.deliverynotes;
                        temp.Source = "AutoSync";
                        listQuestion.Add(temp);
                        string updatesuccess = UpdateTicketSync(x.OrganizationId, x.PatientId, x.AdmissionId, x.EncounterId, false);
                    }
                    InsertQuestion(listQuestion);
                }

                //jika stok tidak kosong
                if (run.Count > 0)
                {
                    foreach(TeleconsulStock x in run)
                    {
                        //CHECK IF PRESCRIPTION EXIST
                        jsonDelivery = "";
                        jsonResultDelivery = "";
                        distance = 0;
                        if (x.IsPrescription == 0)
                        {
                            if (x.channelid == "18")
                            {
                                int CountAIDO = 0;
                                CountAIDO = GetCountAIDOOrder(x.OrganizationId, x.PatientId, x.AdmissionId, x.EncounterId);
                                if (CountAIDO == 0 || x.IsResend == true)
                                {
                                    string syncStatusAIDO = "";
                                    string syncMessageAIDO = "";
                                    string result = "";
                                    string JsonResponse = "";
                                    string ErrMsg = "";
                                    string JsonStringAido = "";

                                    //GENERATE TOKEN
                                    string token = "";
                                    token = GenerateJSONWebToken(x.OrganizationId, x.PatientId, x.AdmissionId, x.DoctorId);

                                    //GET PRICE & BUILD MODEL SYNC AIDO
                                    MySiloamRequestDrugNew aido = new MySiloamRequestDrugNew();
                                    //string value = itemPrices.First().PatientNetTotal;
                                    //int asdf2 = Convert.ToInt32(Math.Floor(Convert.ToDouble(value)));
                                    aido.totalPatientNet = 0;
                                    aido.totalPayerNet = 0;
                                    aido.payerCoverage = 0;
                                    aido.totalPrice = 0;
                                    aido.totalPatientNet = aido.totalPrice;
                                    aido.isSelfCollection = false;
                                    a.Guid TrxId = new a.Guid();
                                    TrxId = GetSiloamTrxId(x.OrganizationId, x.PatientId, x.AdmissionId, x.EncounterId);
                                    if (TrxId == a.Guid.Empty)
                                    {
                                        TrxId = a.Guid.NewGuid();
                                    }
                                    aido.admissionHopeId = x.AdmissionId.ToString();
                                    aido.hospitalHopeId = x.OrganizationId.ToString();
                                    aido.patientHopeId = x.PatientId.ToString();
                                    aido.doctorHopeId = x.DoctorId.ToString();
                                    aido.encounterId = x.EncounterId.ToString();
                                    aido.userId = "999999999999";
                                    aido.userName = "AutoSyncEMR";
                                    aido.source = "EMR";
                                    aido.siloamTrxId = TrxId;
                                    aido.appointmentId = x.appointmentid;
                                    aido.transactionId = x.TransactionId;
                                    aido.items = drugempty;
                                    aido.deliveries = deliveryempty;
                                    aido.isPrescribe = false;
                                    JsonStringAido = JsonConvert.SerializeObject(aido);
                                    result = AidoSyncPrescription(JsonStringAido, x.OrganizationId, x.PatientId, x.AdmissionId, x.DoctorId, token);
                                    if (!result.ToUpper().Contains("KONEKSI KE AIDO GAGAL"))
                                    {
                                        JObject Response = (JObject)JsonConvert.DeserializeObject<dynamic>(result);
                                        if (Response["status"] == null)
                                        {
                                            syncStatusAIDO = "fail";
                                        }
                                        else
                                        {
                                            syncStatusAIDO = Response.Property("status").Value.ToString();
                                        }
                                        syncMessageAIDO = Response.Property("message").Value.ToString();
                                        ErrMsg = Response.Property("message").Value.ToString();
                                        JsonResponse = result;
                                    }
                                    else
                                    {
                                        //if exception or timeout
                                        syncStatusAIDO = "FAIL";
                                        syncMessageAIDO = "KONEKSI KE AIDO GAGAL";
                                        ErrMsg = result;
                                    }
                                    if (syncStatusAIDO.ToLower() == "ok")
                                    {
                                        string insertaidoticket = InsertDataTicket(x.OrganizationId, x.PatientId, x.AdmissionId, x.EncounterId, JsonStringAido, JsonResponse, TrxId, x.channelid);
                                        string updatesuccess = UpdateTicketSync(x.OrganizationId, x.PatientId, x.AdmissionId, x.EncounterId, true);
                                    }
                                    else
                                    {
                                        string insertaidofailed = InsertDataLogFailed(x.OrganizationId, x.PatientId, x.AdmissionId, x.EncounterId, token, JsonStringAido, JsonResponse, ErrMsg, x.channelid);
                                        InsertQuestion tempFail = new InsertQuestion();
                                        tempFail.EncounterId = x.EncounterId;
                                        tempFail.OrganizationId = x.OrganizationId;
                                        tempFail.PatientId = x.PatientId;
                                        tempFail.DoctorId = x.DoctorId;
                                        tempFail.AdmissionId = x.AdmissionId;
                                        tempFail.CategoryId = 2;
                                        tempFail.Question = "";
                                        tempFail.DeliveryAddress = x.deliveryaddress;
                                        tempFail.DeliveryNotes = x.deliverynotes;
                                        tempFail.Source = "AutoSync";
                                        listQuestion.Add(tempFail);
                                        InsertQuestion(listQuestion);
                                        string updatesuccess = UpdateTicketSync(x.OrganizationId, x.PatientId, x.AdmissionId, x.EncounterId, false);
                                    }
                                }
                            }

                            //begin for channelid mysiloam
                            if (x.channelid == "5" || x.channelid == "9")
                            {
                                int CountMySiloam = 0;
                                //get tabel log ticket, jika belum pernah ada data berhasil, lanjut proses
                                CountMySiloam = GetCountAIDOOrder(x.OrganizationId, x.PatientId, x.AdmissionId, x.EncounterId);
                                if (CountMySiloam == 0 || x.IsResend == true)
                                {
                                    //GET PRICE & BUILD MODEL SYNC MYSILOAM
                                    string syncStatusMySiloam = "";
                                    string syncMessageMySiloam = "";
                                    string resultMySiloam = "";
                                    string JsonResponseMySiloam = "";
                                    string ErrMsgMySiloam = "";
                                    string JsonStringMySiloam = "";
                                    MySiloamRequestDrugNew silo = new MySiloamRequestDrugNew();
                                    //string value = itemPrices.First().PatientNetTotal;
                                    //int asdf2 = Convert.ToInt32(Math.Floor(Convert.ToDouble(value)));
                                    silo.totalPatientNet = 0;
                                    silo.totalPayerNet = 0;
                                    silo.payerCoverage = 0;
                                    silo.totalPrice = 0;
                                    silo.totalPatientNet = silo.totalPrice;
                                    silo.isSelfCollection = false;
                                    a.Guid TrxId = new a.Guid();
                                    TrxId = GetSiloamTrxId(x.OrganizationId, x.PatientId, x.AdmissionId, x.EncounterId);
                                    if (TrxId == a.Guid.Empty)
                                    {
                                        TrxId = a.Guid.NewGuid();
                                    }
                                    silo.admissionHopeId = x.AdmissionId.ToString();
                                    silo.hospitalHopeId = x.OrganizationId.ToString();
                                    silo.patientHopeId = x.PatientId.ToString();
                                    silo.doctorHopeId = x.DoctorId.ToString();
                                    silo.encounterId = x.EncounterId.ToString();
                                    silo.userId = "999999999999";
                                    silo.userName = "AutoSyncEMR";
                                    silo.source = "EMR";
                                    silo.siloamTrxId = TrxId;
                                    silo.appointmentId = x.appointmentid;
                                    silo.transactionId = x.TransactionId;
                                    silo.items = drugempty;
                                    silo.deliveries = deliveryempty;
                                    silo.isPrescribe = false;
                                    JsonStringMySiloam = JsonConvert.SerializeObject(silo);
                                    resultMySiloam = SyncDrugMySiloam(JsonStringMySiloam);
                                    if (!resultMySiloam.ToUpper().Contains("KONEKSI KE MYSILOAM GAGAL"))
                                    {
                                        JObject ResponseSilo = (JObject)JsonConvert.DeserializeObject<dynamic>(resultMySiloam);
                                        syncStatusMySiloam = ResponseSilo.Property("status").Value.ToString();
                                        syncMessageMySiloam = ResponseSilo.Property("message").Value.ToString();
                                        ErrMsgMySiloam = ResponseSilo.Property("message").Value.ToString();
                                        JsonResponseMySiloam = resultMySiloam;
                                    }
                                    else
                                    {
                                        syncStatusMySiloam = "FAIL";
                                        syncMessageMySiloam = "KONEKSI KE MYSILOAM GAGAL";
                                        ErrMsgMySiloam = resultMySiloam;
                                    }
                                    if (syncStatusMySiloam.ToLower() == "ok")
                                    {
                                        string insertaidoticket = InsertDataTicket(x.OrganizationId, x.PatientId, x.AdmissionId, x.EncounterId, JsonStringMySiloam, JsonResponseMySiloam, TrxId, x.channelid);
                                        string updatesuccess = UpdateTicketSync(x.OrganizationId, x.PatientId, x.AdmissionId, x.EncounterId, true);
                                    }
                                    else
                                    {
                                        string insertaidofailed = InsertDataLogFailed(x.OrganizationId, x.PatientId, x.AdmissionId, x.EncounterId, "", JsonStringMySiloam, JsonResponseMySiloam, ErrMsgMySiloam, x.channelid);
                                        InsertQuestion tempFail = new InsertQuestion();
                                        tempFail.EncounterId = x.EncounterId;
                                        tempFail.OrganizationId = x.OrganizationId;
                                        tempFail.PatientId = x.PatientId;
                                        tempFail.DoctorId = x.DoctorId;
                                        tempFail.AdmissionId = x.AdmissionId;
                                        tempFail.CategoryId = 2;
                                        tempFail.Question = "";
                                        tempFail.DeliveryAddress = x.deliveryaddress;
                                        tempFail.DeliveryNotes = x.deliverynotes;
                                        tempFail.Source = "AutoSync";
                                        listQuestion.Add(tempFail);
                                        InsertQuestion(listQuestion);
                                        string updatesuccess = UpdateTicketSync(x.OrganizationId, x.PatientId, x.AdmissionId, x.EncounterId, false);
                                    }
                                }
                            }
                        }
                        else
                        {
                            //get item price dan flag send available
                            List<ItemPriceAuto> itemPrices = new List<ItemPriceAuto>();
                            itemPrices = GetItemPriceAuto(x.OrganizationId, x.PatientId, x.AdmissionId, x.EncounterId, x.IsResend);

                            //jika ada item yang tidak bisa dikirim, delivery fee insert dummy
                            TeleconsultationDeliveryHeader resultDelivery = new TeleconsultationDeliveryHeader();
                            if ((itemPrices.Where(s => s.IsSendAvailable == false).Count() > 0) || x.IsPayer == 1 || x.latitude == "0" || x.longitude == "0")
                            {
                                resultDelivery = dummyheader;
                            }
                            else
                            {
                               
                                //get from API deliveryfee jika semua item bisa dikirim
                                DrugDelivery temp = new DrugDelivery();
                                DeliveryDetail tempDetail = new DeliveryDetail();
                                tempDetail.latitude = x.latitude;
                                tempDetail.longtitude = x.longitude;
                                tempDetail.city = x.city;
                                tempDetail.province = x.province;
                                temp.destination = tempDetail;
                                temp.organization_id = OrganizationId;
                                temp.weight = 1;
                                temp.travelMode = "DRIVING";
                                temp.avoidHighways = false;
                                temp.avoidTolls = true;
                                jsonDelivery = JsonConvert.SerializeObject(temp);
                                resultDelivery = GetDeliveryFee(jsonDelivery);

                                if (resultDelivery.detail == null)
                                {
                                    resultDelivery.detail = listdummy;
                                }
                                else
                                {
                                    resultDelivery.detail.Add(dummy);
                                }
                                distance = resultDelivery.distance;
                            }

                            jsonResultDelivery = JsonConvert.SerializeObject(resultDelivery);

                            //begin for channelid AIDO
                            if (x.channelid == "18")
                            {
                                int CountAIDO = 0;
                                CountAIDO = GetCountAIDOOrder(x.OrganizationId, x.PatientId, x.AdmissionId, x.EncounterId);
                                if (CountAIDO == 0 || x.IsResend == true)
                                {
                                    string syncStatusAIDO = "";
                                    string syncMessageAIDO = "";
                                    string result = "";
                                    string JsonResponse = "";
                                    string ErrMsg = "";
                                    string JsonStringAido = "";

                                    //GENERATE TOKEN
                                    string token = "";
                                    token = GenerateJSONWebToken(x.OrganizationId, x.PatientId, x.AdmissionId, x.DoctorId);

                                    //GET PRICE & BUILD MODEL SYNC AIDO
                                    MySiloamRequestDrugNew aido = new MySiloamRequestDrugNew();
                                    //string value = itemPrices.First().PatientNetTotal;
                                    //int asdf2 = Convert.ToInt32(Math.Floor(Convert.ToDouble(value)));
                                    aido.totalPatientNet = a.Convert.ToInt32(a.Math.Floor(a.Convert.ToDouble(itemPrices.First().PatientNetTotal.ToString())));
                                    aido.totalPayerNet = a.Convert.ToInt32(a.Math.Floor(a.Convert.ToDouble(itemPrices.First().PayerNetTotal.ToString())));
                                    aido.payerCoverage = 0;
                                    aido.totalPrice = int.Parse(itemPrices.First().TotalPrice.ToString());
                                    aido.totalPatientNet = aido.totalPrice;
                                    aido.isSelfCollection = itemPrices.Where(s => s.IsSendAvailable == false).Count() > 0 ? true : false;
                                    a.Guid TrxId = new a.Guid();
                                    TrxId = GetSiloamTrxId(x.OrganizationId, x.PatientId, x.AdmissionId, x.EncounterId);
                                    if (TrxId == a.Guid.Empty)
                                    {
                                        TrxId = a.Guid.NewGuid();
                                    }
                                    aido.admissionHopeId = x.AdmissionId.ToString();
                                    aido.hospitalHopeId = x.OrganizationId.ToString();
                                    aido.patientHopeId = x.PatientId.ToString();
                                    aido.doctorHopeId = x.DoctorId.ToString();
                                    aido.encounterId = x.EncounterId.ToString();
                                    aido.userId = "999999999999";
                                    aido.userName = "AutoSyncEMR";
                                    aido.source = "EMR";
                                    aido.siloamTrxId = TrxId;
                                    aido.appointmentId = x.appointmentid;
                                    aido.transactionId = x.TransactionId;
                                    List<MySiloamDrugNew> drugaido = new List<MySiloamDrugNew>();
                                    drugaido = (from a in itemPrices
                                                select new MySiloamDrugNew
                                                {
                                                    itemId = a.SalesItemId,
                                                    name = a.SalesItemName,
                                                    qty = a.quantity,
                                                    uom = a.Uom,
                                                    patientNet = a.PatientNet,
                                                    payerNet = a.PayerNet,
                                                    frequency = a.Frequency,
                                                    instruction = a.Instruction,
                                                    isSendAvailable = a.IsSendAvailable
                                                }).ToList();
                                    aido.items = drugaido;
                                    List<DeliveryFeeType> deliveryaido = new List<DeliveryFeeType>();
                                    

                                    if (LiveAidoDelivery)
                                    {
                                        deliveryaido = (from a in listdummy
                                                        select new DeliveryFeeType
                                                        {
                                                            deliveryHeaderId = a.price_header_id,
                                                            amount = a.amount,
                                                            estimation = a.estimation,
                                                            name = a.price_type_name,
                                                            remarks = a.remarks
                                                        }).ToList();
                                    }
                                    else
                                    {
                                        deliveryaido = (from a in resultDelivery.detail
                                                        select new DeliveryFeeType
                                                        {
                                                            deliveryHeaderId = a.price_header_id,
                                                            amount = a.amount,
                                                            estimation = a.estimation,
                                                            name = a.price_type_name,
                                                            remarks = a.remarks
                                                        }).ToList();
                                    }
                                    aido.deliveries = deliveryaido;
                                    aido.isPrescribe = true;
                                    JsonStringAido = JsonConvert.SerializeObject(aido);
                                    result = AidoSyncPrescription(JsonStringAido, x.OrganizationId, x.PatientId, x.AdmissionId, x.DoctorId, token);
                                    if (!result.ToUpper().Contains("KONEKSI KE AIDO GAGAL"))
                                    {
                                        JObject Response = (JObject)JsonConvert.DeserializeObject<dynamic>(result);
                                        if (Response["status"] == null)
                                        {
                                            syncStatusAIDO = "fail";
                                        }
                                        else
                                        {
                                            syncStatusAIDO = Response.Property("status").Value.ToString();
                                        }
                                        syncMessageAIDO = Response.Property("message").Value.ToString();
                                        ErrMsg = Response.Property("message").Value.ToString();
                                        JsonResponse = result;
                                    }
                                    else
                                    {
                                        //if exception or timeout
                                        syncStatusAIDO = "FAIL";
                                        syncMessageAIDO = "KONEKSI KE AIDO GAGAL";
                                        ErrMsg = result;
                                    }
                                    if (syncStatusAIDO.ToLower() == "ok")
                                    {
                                        string insertaidoticket = InsertDataTicket(x.OrganizationId, x.PatientId, x.AdmissionId, x.EncounterId, JsonStringAido, JsonResponse, TrxId, x.channelid);
                                        string updatesuccess = UpdateTicketSync(x.OrganizationId, x.PatientId, x.AdmissionId, x.EncounterId, true);
                                    }
                                    else
                                    {
                                        string insertaidofailed = InsertDataLogFailed(x.OrganizationId, x.PatientId, x.AdmissionId, x.EncounterId, token, JsonStringAido, JsonResponse, ErrMsg, x.channelid);
                                        InsertQuestion tempFail = new InsertQuestion();
                                        tempFail.EncounterId = x.EncounterId;
                                        tempFail.OrganizationId = x.OrganizationId;
                                        tempFail.PatientId = x.PatientId;
                                        tempFail.DoctorId = x.DoctorId;
                                        tempFail.AdmissionId = x.AdmissionId;
                                        tempFail.CategoryId = 2;
                                        tempFail.Question = "";
                                        tempFail.DeliveryAddress = x.deliveryaddress;
                                        tempFail.DeliveryNotes = x.deliverynotes;
                                        tempFail.Source = "AutoSync";
                                        listQuestion.Add(tempFail);
                                        InsertQuestion(listQuestion);
                                        string updatesuccess = UpdateTicketSync(x.OrganizationId, x.PatientId, x.AdmissionId, x.EncounterId, false);
                                    }
                                }
                            }

                            //begin for channelid mysiloam
                            if (x.channelid == "5" || x.channelid == "9")
                            {
                                int CountMySiloam = 0;
                                //get tabel log ticket, jika belum pernah ada data berhasil, lanjut proses
                                CountMySiloam = GetCountAIDOOrder(x.OrganizationId, x.PatientId, x.AdmissionId, x.EncounterId);
                                if (CountMySiloam == 0 || x.IsResend == true)
                                {
                                    //GET PRICE & BUILD MODEL SYNC MYSILOAM
                                    string syncStatusMySiloam = "";
                                    string syncMessageMySiloam = "";
                                    string resultMySiloam = "";
                                    string JsonResponseMySiloam = "";
                                    string ErrMsgMySiloam = "";
                                    string JsonStringMySiloam = "";
                                    MySiloamAutoSync silo = new MySiloamAutoSync();
                                    //Sync Lab,Rad,Lab
                                    silo = FilterDataLabRad(OrganizationId, x.EncounterId);

                                    if(silo == null)
                                    {
                                        silo = new MySiloamAutoSync();
                                        silo.hospitalHopeId = OrganizationId;
                                        silo.encounterId = x.EncounterId;
                                        silo.appointmentId = x.appointmentid;
                                        silo.admissionHopeId = x.AdmissionId.ToString();
                                        silo.doctorHopeId = x.DoctorId.ToString();
                                        silo.patientHopeId = x.PatientId.ToString();
                                        silo.userId = "999999999999";
                                        silo.userName = "AutoSyncEMR";
                                        silo.source = "EMR";
                                    }
                                    else
                                    {
                                        silo.mySiloamRequestDrugNew = new MySiloamRequestDrugNew();
                                    }
                                    //string value = itemPrices.First().PatientNetTotal;
                                    //int asdf2 = Convert.ToInt32(Math.Floor(Convert.ToDouble(value)));
                                    silo.mySiloamRequestDrugNew.totalPatientNet = a.Convert.ToInt32(a.Math.Floor(a.Convert.ToDouble(itemPrices.First().PatientNetTotal.ToString())));
                                    silo.mySiloamRequestDrugNew.totalPayerNet = a.Convert.ToInt32(a.Math.Floor(a.Convert.ToDouble(itemPrices.First().PayerNetTotal.ToString())));
                                    silo.mySiloamRequestDrugNew.payerCoverage = 0;
                                    silo.mySiloamRequestDrugNew.totalPrice = int.Parse(itemPrices.First().TotalPrice.ToString());
                                    silo.mySiloamRequestDrugNew.totalPatientNet = silo.mySiloamRequestDrugNew.totalPrice;
                                    silo.mySiloamRequestDrugNew.isSelfCollection = itemPrices.Where(s => s.IsSendAvailable == false).Count() > 0 ? true : false;
                                    a.Guid TrxId = new a.Guid();
                                    TrxId = GetSiloamTrxId(x.OrganizationId, x.PatientId, x.AdmissionId, x.EncounterId);
                                    if (TrxId == a.Guid.Empty)
                                    {
                                        TrxId = a.Guid.NewGuid();
                                    }
                                    silo.mySiloamRequestDrugNew.admissionHopeId = x.AdmissionId.ToString();
                                    silo.mySiloamRequestDrugNew.hospitalHopeId = x.OrganizationId.ToString();
                                    silo.mySiloamRequestDrugNew.patientHopeId = x.PatientId.ToString();
                                    silo.mySiloamRequestDrugNew.doctorHopeId = x.DoctorId.ToString();
                                    silo.mySiloamRequestDrugNew.encounterId = x.EncounterId.ToString();
                                    silo.mySiloamRequestDrugNew.userId = "999999999999";
                                    silo.mySiloamRequestDrugNew.userName = "AutoSyncEMR";
                                    silo.mySiloamRequestDrugNew.source = "EMR";
                                    silo.mySiloamRequestDrugNew.siloamTrxId = TrxId;
                                    silo.mySiloamRequestDrugNew.appointmentId = x.appointmentid;
                                    silo.mySiloamRequestDrugNew.transactionId = x.TransactionId;
                                    List<MySiloamDrugNew> drug = new List<MySiloamDrugNew>();
                                    drug = (from a in itemPrices
                                            select new MySiloamDrugNew
                                            {
                                                itemId = a.SalesItemId,
                                                name = a.SalesItemName,
                                                qty = a.quantity,
                                                uom = a.Uom,
                                                patientNet = a.PatientNet,
                                                payerNet = a.PayerNet,
                                                frequency = a.Frequency,
                                                instruction = a.Instruction,
                                                isSendAvailable = a.IsSendAvailable
                                            }).ToList();
                                    silo.mySiloamRequestDrugNew.items = drug;
                                    List<DeliveryFeeType> delivery = new List<DeliveryFeeType>();

                                    if (LiveAidoDelivery)
                                    {
                                        delivery = (from a in listdummy
                                                    select new DeliveryFeeType
                                                    {
                                                        deliveryHeaderId = a.price_header_id,
                                                        amount = a.amount,
                                                        estimation = a.estimation,
                                                        name = a.price_type_name,
                                                        remarks = a.remarks
                                                    }).ToList();
                                    }
                                    else
                                    {
                                        delivery = (from a in resultDelivery.detail
                                                    select new DeliveryFeeType
                                                    {
                                                        deliveryHeaderId = a.price_header_id,
                                                        amount = a.amount,
                                                        estimation = a.estimation,
                                                        name = a.price_type_name,
                                                        remarks = a.remarks
                                                    }).ToList();
                                    }
                                   

                                    silo.mySiloamRequestDrugNew.deliveries = delivery;
                                    silo.mySiloamRequestDrugNew.isPrescribe = true;
                                    JsonStringMySiloam = JsonConvert.SerializeObject(silo);
                                    resultMySiloam = SyncDrugMySiloam(JsonStringMySiloam);
                                    if (!resultMySiloam.ToUpper().Contains("KONEKSI KE MYSILOAM GAGAL"))
                                    {
                                        JObject ResponseSilo = (JObject)JsonConvert.DeserializeObject<dynamic>(resultMySiloam);
                                        syncStatusMySiloam = ResponseSilo.Property("status").Value.ToString();
                                        syncMessageMySiloam = ResponseSilo.Property("message").Value.ToString();
                                        ErrMsgMySiloam = ResponseSilo.Property("message").Value.ToString();
                                        JsonResponseMySiloam = resultMySiloam;
                                        bool status = syncStatusMySiloam.ToLower() == "ok" ? true : false;
                                        string insertaidoticket = UpdateCpoeStatusSync(OrganizationId, long.Parse(silo.admissionHopeId), silo.encounterId, status);
                                    }
                                    else
                                    {
                                        syncStatusMySiloam = "FAIL";
                                        syncMessageMySiloam = "KONEKSI KE MYSILOAM GAGAL";
                                        ErrMsgMySiloam = resultMySiloam;
                                        string insertaidoticket = UpdateCpoeStatusSync(OrganizationId, long.Parse(silo.admissionHopeId), silo.encounterId, false);
                                    }
                                    if (syncStatusMySiloam.ToLower() == "ok")
                                    {
                                        string insertaidoticket = InsertDataTicket(x.OrganizationId, x.PatientId, x.AdmissionId, x.EncounterId, JsonStringMySiloam, JsonResponseMySiloam, TrxId, x.channelid);
                                        string updatesuccess = UpdateTicketSync(x.OrganizationId, x.PatientId, x.AdmissionId, x.EncounterId, true);
                                    }
                                    else
                                    {
                                        string insertaidofailed = InsertDataLogFailed(x.OrganizationId, x.PatientId, x.AdmissionId, x.EncounterId, "", JsonStringMySiloam, JsonResponseMySiloam, ErrMsgMySiloam, x.channelid);
                                        InsertQuestion tempFail = new InsertQuestion();
                                        tempFail.EncounterId = x.EncounterId;
                                        tempFail.OrganizationId = x.OrganizationId;
                                        tempFail.PatientId = x.PatientId;
                                        tempFail.DoctorId = x.DoctorId;
                                        tempFail.AdmissionId = x.AdmissionId;
                                        tempFail.CategoryId = 2;
                                        tempFail.Question = "";
                                        tempFail.DeliveryAddress = x.deliveryaddress;
                                        tempFail.DeliveryNotes = x.deliverynotes;
                                        tempFail.Source = "AutoSync";
                                        listQuestion.Add(tempFail);
                                        InsertQuestion(listQuestion);
                                        string updatesuccess = UpdateTicketSync(x.OrganizationId, x.PatientId, x.AdmissionId, x.EncounterId, false);
                                    }
                                }
                            }
                        }
                        string insertLogDelivery = InsertLogDeliveryFee(x.OrganizationId, x.PatientId, x.AdmissionId, x.EncounterId, jsonDelivery, jsonResultDelivery, distance);
                    }
                }
            }
            catch (a.Exception ex)
            {
                data = ex.Message;
            }
            return ListID + data;
        }

        private MySiloamAutoSync FilterDataLabRad(long OrganizationId, a.Guid EncounterId = new a.Guid())
        {
            MySiloamAutoSync dataLabRad = new MySiloamAutoSync();
            List<CpoeStock> loParam = GetCpoeStock(OrganizationId, EncounterId);
            if(loParam.Count == 0)
            {
                return null;
            }

            List<CheckPriceCpoe> loReturnPrice = GetCheckPriceCpoe(loParam, OrganizationId, loParam[0].PatientId, loParam[0].EncounterId.ToString(), loParam[0].AdmissionId);

            dataLabRad.mySiloamRequestLabNew = new MySiloamRequestLabNew();
            dataLabRad.mySiloamRequestRadNew = new MySiloamRequestRadNew();

            if (loParam.Count > 0)
            {
                dataLabRad.hospitalHopeId = OrganizationId;
                dataLabRad.encounterId = loParam[0].EncounterId;
                dataLabRad.appointmentId = loParam[0].AppointmentId;
                dataLabRad.admissionHopeId = loParam[0].AdmissionId.ToString();
                dataLabRad.doctorHopeId = loParam[0].DoctorId.ToString();
                dataLabRad.patientHopeId = loParam[0].PatientId.ToString();
                dataLabRad.userId = "999999999999";
                dataLabRad.userName = "AutoSyncEMR";
                dataLabRad.source = "EMR";

                //HEADER LAB
                var labValue = loParam.Where(x => x.ItemType.Contains("Lab") && x.EncounterId == loParam[0].EncounterId).ToList();

                if (labValue.Count > 0)
                {
                    var totalPayerNetLab = loReturnPrice.Where(x => labValue.Any(y => y.ItemId == x.SalesItemId)).Sum(x => x.TotalPayerNet);
                    var totalPatientNetLab = loReturnPrice.Where(x => labValue.Any(y => y.ItemId == x.SalesItemId)).Sum(x => x.TotalPatientNet);
                    var totalPriceLab = loReturnPrice.Where(x => labValue.Any(y => y.ItemId == x.SalesItemId)).Sum(x => x.PayerNet) -
                       loReturnPrice.Where(x => labValue.Any(y => y.ItemId == x.SalesItemId)).Sum(x => x.DiscountPrice);

                    dataLabRad.mySiloamRequestLabNew.totalPayerNet = totalPayerNetLab;
                    dataLabRad.mySiloamRequestLabNew.totalPatientNet = totalPatientNetLab;
                    dataLabRad.mySiloamRequestLabNew.payerCoverage = 0;
                    dataLabRad.mySiloamRequestLabNew.totalPrice = totalPriceLab;
                    dataLabRad.mySiloamRequestLabNew.rangeDateFrom = a.DateTime.Today;
                    dataLabRad.mySiloamRequestLabNew.rangeDateTo = a.DateTime.Today.AddDays(3);
                    dataLabRad.mySiloamRequestLabNew.siloamTrxId = a.Guid.NewGuid();

                    var labDetails = new List<MySiloamRequestLabDetail>();

                    foreach (CpoeStock labStock in labValue)
                    {
                        labDetails.Add(new MySiloamRequestLabDetail
                        {
                            itemId = labStock.ItemId,
                            name = labStock.SalesItemName,
                            patientNet = loReturnPrice.Where(x => x.SalesItemId == labStock.ItemId).FirstOrDefault()?.PatientNet.ToString(),
                            payerNet = loReturnPrice.Where(x => x.SalesItemId == labStock.ItemId).FirstOrDefault()?.PayerNet.ToString(),
                            remarks = ""
                        });
                    }
                    dataLabRad.mySiloamRequestLabNew.items = labDetails;
                }
                else
                {
                    dataLabRad.mySiloamRequestLabNew = null;
                }

                //HEADER RAD
                var radValue = loParam.Where(x => !x.ItemType.Contains("Lab") && x.EncounterId == loParam[0].EncounterId).ToList();
                if (radValue.Count > 0)
                {
                    var totalPayerNetRad = loReturnPrice.Where(x => radValue.Any(y => y.ItemId == x.SalesItemId)).Sum(x => x.TotalPayerNet);
                    var totalPatientNetRad = loReturnPrice.Where(x => radValue.Any(y => y.ItemId == x.SalesItemId)).Sum(x => x.TotalPatientNet);
                    var totalPriceRad = loReturnPrice.Where(x => radValue.Any(y => y.ItemId == x.SalesItemId)).Sum(x => x.PayerNet) -
                       loReturnPrice.Where(x => radValue.Any(y => y.ItemId == x.SalesItemId)).Sum(x => x.DiscountPrice);
                    dataLabRad.mySiloamRequestRadNew.totalPayerNet = totalPayerNetRad;
                    dataLabRad.mySiloamRequestRadNew.totalPatientNet = totalPatientNetRad;
                    dataLabRad.mySiloamRequestRadNew.payerCoverage = 0;
                    dataLabRad.mySiloamRequestRadNew.totalPrice = totalPriceRad;
                    dataLabRad.mySiloamRequestRadNew.rangeDateFrom = a.DateTime.Today;
                    dataLabRad.mySiloamRequestRadNew.rangeDateTo = a.DateTime.Today.AddDays(3);
                    dataLabRad.mySiloamRequestLabNew.siloamTrxId = a.Guid.NewGuid();


                    var radDetails = new List<MySiloamRequestRadDetail>();
                    foreach (CpoeStock radStock in radValue)
                    {
                        radDetails.Add(new MySiloamRequestRadDetail
                        {
                            itemId = radStock.ItemId,
                            name = radStock.SalesItemName,
                            patientNet = loReturnPrice.Where(x => x.SalesItemId == radStock.ItemId).FirstOrDefault()?.PatientNet.ToString(),
                            payerNet = loReturnPrice.Where(x => x.SalesItemId == radStock.ItemId).FirstOrDefault()?.PayerNet.ToString(),
                            remarks = ""
                        });
                    }
                    dataLabRad.mySiloamRequestRadNew.items = radDetails;
                }
                else
                {
                    dataLabRad.mySiloamRequestRadNew = null;
                }

                if (EncounterId == a.Guid.Empty)
                {
                    string syncStatusMySiloam = "";
                    string resultMySiloam = "";
                    string JsonStringMySiloam = "";
                    dataLabRad.mySiloamRequestDrugNew = null;
                    JsonStringMySiloam = JsonConvert.SerializeObject(dataLabRad);
                    resultMySiloam = SyncDrugMySiloam(JsonStringMySiloam);

                    if (!resultMySiloam.ToUpper().Contains("KONEKSI KE MYSILOAM GAGAL"))
                    {
                        JObject ResponseSilo = (JObject)JsonConvert.DeserializeObject<dynamic>(resultMySiloam);
                        syncStatusMySiloam = ResponseSilo.Property("status").Value.ToString();
                        bool status = syncStatusMySiloam.ToLower() == "ok" ? true : false;
                        string insertaidoticket = UpdateCpoeStatusSync(OrganizationId, long.Parse(dataLabRad.admissionHopeId), dataLabRad.encounterId, status);
                    }
                    else
                    {
                        string insertaidoticket = UpdateCpoeStatusSync(OrganizationId, long.Parse(dataLabRad.admissionHopeId), dataLabRad.encounterId, false);
                        syncStatusMySiloam = "FAIL";
                    }
                }
            }
            else
            {
                dataLabRad = null;
            }

            return dataLabRad;
        }

        public List<CpoeStock> GetCpoeStock(long OrganizationId, a.Guid EncounterId)
        {
            List<CpoeStock> data = new List<CpoeStock>();
            DataTable dt = new DataTable();
            try
            {
                using (SqlConnection conn = new SqlConnection(Siloam.System.ApplicationSetting.ConnectionString))
                {
                    var paramEncounter = EncounterId == a.Guid.Empty ? null : EncounterId.ToString();
                    conn.Open();
                    SqlCommand cmd = conn.CreateCommand();
                    cmd.CommandText = "spGetTeleconsulLabRadFlag";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("OrganizationId", OrganizationId));
                    cmd.Parameters.Add(new SqlParameter("EncounterId", paramEncounter));
                    using (var da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }
                    data = (from DataRow dr in dt.Rows
                            select new CpoeStock
                            {
                                OrganizationId = long.Parse(dr["OrganizationId"].ToString()),
                                PatientId = long.Parse(dr["PatientId"].ToString()),
                                AdmissionId = long.Parse(dr["AdmissionId"].ToString()),
                                DoctorId = long.Parse(dr["DoctorId"].ToString()),
                                EncounterId = a.Guid.Parse(dr["EncounterId"].ToString()),
                                ItemId = long.Parse(dr["ItemId"].ToString()),
                                ItemType = dr["ItemType"].ToString().Replace(',', '.'),
                                AppointmentId = a.Guid.Parse(dr["AppointmentId"].ToString()),
                                SalesItemName = dr["SalesItemName"].ToString().Replace(',', '.')
                            }).ToList();
                }
            }
            catch (a.Exception ex)
            {
                throw ex;
            }
            return data;
        }

        public List<CheckPriceCpoe> GetCheckPriceCpoe(List<CpoeStock> model, long OrganizationId, long PatientId, string EncounterId, long AdmissionId)
        {
            DataTable dt = new DataTable();
            List <CheckPriceCpoe> data = new List<CheckPriceCpoe>();

            try
            {
                string connstringuse = ValueStorage.IntegrationConnectionString;
                using (SqlConnection conn = new SqlConnection(connstringuse))
                {
                    conn.Open();
                    SqlCommand cmd = conn.CreateCommand();
                    cmd.CommandText = "spGetCheckPriceCPOE";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("OrganizationId", OrganizationId));
                    cmd.Parameters.Add(new SqlParameter("PatientId", PatientId));
                    cmd.Parameters.Add(new SqlParameter("AdmissionId", AdmissionId));
                    cmd.Parameters.Add(new SqlParameter("EncounterId", EncounterId));

                    string xmlcpoetrans = ConvertCpoeTransToXML(model);
                    cmd.Parameters.Add(new SqlParameter("Cpoe", xmlcpoetrans));

                    using (var da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }

                    data = (from DataRow dr in dt.Rows
                            select new CheckPriceCpoe
                            {
                                Amount = decimal.Parse(dr["Amount"].ToString()),
                                DiscountPrice = decimal.Parse(dr["DiscountPrice"].ToString()),
                                IsCito = int.Parse(dr["IsCito"].ToString()),
                                PatientNet = decimal.Parse(dr["PatientNet"].ToString()),
                                PayerNet = decimal.Parse(dr["PayerNet"].ToString()),
                                Quantity = decimal.Parse(dr["Quantity"].ToString()),
                                TotalPatientNet = decimal.Parse(dr["TotalPatientNet"].ToString()),
                                TotalPayerNet = decimal.Parse(dr["TotalPayerNet"].ToString()),
                                SalesItemId = long.Parse(dr["SalesItemId"].ToString()),
                                SalesItemName = dr["SalesItemName"].ToString(),
                                TotalPayerNetFinal = decimal.Parse(dr["TotalPayerNetFinal"].ToString()),
                                TotalPatientNetFinal = decimal.Parse(dr["TotalPatientNetFinal"].ToString())
                            }).ToList();
                }
            }
            catch (a.Exception ex)
            {
                throw ex;
            }
            return data;
        }

        public static string ConvertCpoeTransToXML(List<CpoeStock> data)
        {
            var cpoelist = from p in data
                           select new CheckPriceCpoeParam()
                           {
                               item_id = p.ItemId,
                               quantity = 1,
                               is_cito = p.IsCito
                           };



            List<CheckPriceCpoeParam> CheckPriceCpoeParams = cpoelist.ToList<CheckPriceCpoeParam>();

            var cpoelcpist = from x in CheckPriceCpoeParams
                             group x by (x.item_id, x.is_cito) into g
                             select new CheckPriceCpoeParam() { item_id = g.Key.item_id, is_cito = g.Key.is_cito, quantity = g.Sum(x => x.quantity) };

            CheckPriceCpoeParams = cpoelcpist.ToList<CheckPriceCpoeParam>();


            XDocument doc = new XDocument(new XDeclaration("1.0", "UTF-8", "yes"),
                new XElement("root",
                    from p in CheckPriceCpoeParams
                    where p.item_id != 0
                    select new XElement("row",
                            new XAttribute("item_id", p.item_id),
                            new XAttribute("quantity", p.quantity),
                            new XAttribute("is_cito", p.is_cito ? 1 : 0)
                        )
                ));
            return doc.ToString();
        }

        public string GetSettingLiveAidoDelivery(long OrganizationId)
        {
            string data = "";
            using (var context = new DatabaseContext(ContextOption))
            {
                data = (from a in context.SettingSet
                        where a.is_active == true && a.organization_id == OrganizationId && a.setting_name == "USE_AIDO_DELIVERY"
                        select a.setting_value).First();
            }
            return data;
        }

        public string UpdateCpoeStatusSync(long OrganizationId, long AdmissionId, a.Guid EncounterId, bool IsSuccess)
        {
            DataTable dt = new DataTable();
            string data;
            try
            {
                string connstringuse = ValueStorage.IntegrationConnectionString;
                using (SqlConnection conn = new SqlConnection(connstringuse))
                {
                    conn.Open();
                    SqlCommand cmd = conn.CreateCommand();
                    cmd.CommandText = "spUpdateCpoeSync";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("OrganizationId", OrganizationId));
                    cmd.Parameters.Add(new SqlParameter("AdmissionId", AdmissionId));
                    cmd.Parameters.Add(new SqlParameter("EncounterId", EncounterId));
                    cmd.Parameters.Add(new SqlParameter("IsSuccess", IsSuccess));
                    using (var da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }
                    data = (from DataRow dr in dt.Rows
                            select dr["Result"].ToString()).Single();
                }
            }
            catch (a.Exception ex)
            {
                throw ex;
            }
            return data;
        }
    }
}