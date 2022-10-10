using a = System;
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
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Security.Cryptography.X509Certificates;

namespace Siloam.Service.EMRPharmacy.Repositories
{

    public class AidoSyncRepository : DatabaseConfig, IAidoSyncRepository
    {

        public AidoSyncRepository() : base() { }

        public AidoSyncRepository(DatabaseContext context) : base(context) { }

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

        public string AidoSyncPrescription(string JsonString, long OrganizationId, long PatientId, long AdmissionId, long DoctorId, string token)
        {
            string data = "";
            string result = "";
            try
            {
                //string token = GenerateJSONWebToken(OrganizationId, PatientId, AdmissionId, DoctorId);
                var content = new StringContent(JsonString, Encoding.UTF8, "application/json");
                HttpClient http = new HttpClient();
                http.BaseAddress = new a.Uri(ValueStorage.AidoUrlSync);

                http.DefaultRequestHeaders.Accept.Clear();
                http.DefaultRequestHeaders.Accept.Add(new a.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                http.DefaultRequestHeaders.Add("token", token);

                var task = Task.Run(async () =>
                {
                    return await http.PostAsync(ValueStorage.AidoUrlSync, content);
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
                            new XAttribute("IssuedQty", p.IssuedQty)
                        )
                ));
            return doc.ToString();
        }

        public List<ItemPrice> GetItemSync(long OrganizationId, long PatientId, long AdmissionId, a.Guid EncounterId, List<PharmacyPrescription> Prescription, string DeliveryFee, bool IsEditDrug, bool IsEditConsumable)
        {
            DataTable dt = new DataTable();
            List<ItemPrice> data = new List<ItemPrice>();
            string xmlPrescription = ConvertPrescriptionToXML(Prescription);
            try
            {
                using (SqlConnection conn = new SqlConnection(Siloam.System.ApplicationSetting.ConnectionString))
                {
                    conn.Open();
                    SqlCommand cmd = conn.CreateCommand();
                    cmd.CommandText = "spGetItemSyncAIDO";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("OrganizationId", OrganizationId));
                    cmd.Parameters.Add(new SqlParameter("PatientId", PatientId));
                    cmd.Parameters.Add(new SqlParameter("AdmissionId", AdmissionId));
                    cmd.Parameters.Add(new SqlParameter("EncounterId", EncounterId));
                    cmd.Parameters.Add(new SqlParameter("Prescription", xmlPrescription));
                    cmd.Parameters.Add(new SqlParameter("DeliveryFee", DeliveryFee));
                    cmd.Parameters.Add(new SqlParameter("IsEditDrug", IsEditDrug));
                    cmd.Parameters.Add(new SqlParameter("IsEditConsumable", IsEditConsumable));
                    using (var da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }
                    data = (from DataRow dr in dt.Rows
                            select new ItemPrice
                            {
                                SalesItemId = long.Parse(dr["SalesItemId"].ToString()),
                                SalesItemName = dr["SalesItemName"].ToString(),
                                Uom = dr["Uom"].ToString(),
                                quantity = dr["quantity"].ToString().Replace(',','.'),
                                SubTotal = dr["SubTotal"].ToString().Replace(',', '.'),
                                TotalPrice = dr["TotalPrice"].ToString().Replace(',', '.')
                            }).ToList();
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

        public List<ItemPrice> GetItemSyncTeleconsultation(long OrganizationId, long PatientId, long AdmissionId, a.Guid EncounterId, List<PharmacyPrescription> Prescription, string DeliveryFee, bool IsEditDrug, bool IsEditConsumable, string PayerCoverage, short IsDefaultCoverage)
        {
            DataTable dt = new DataTable();
            List<ItemPrice> data = new List<ItemPrice>();
            string xmlPrescription = ConvertPrescriptionToXML(Prescription);
            try
            {
                using (SqlConnection conn = new SqlConnection(Siloam.System.ApplicationSetting.ConnectionString))
                {
                    conn.Open();
                    SqlCommand cmd = conn.CreateCommand();
                    cmd.CommandText = "spGetItemSyncTeleconsultation";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("OrganizationId", OrganizationId));
                    cmd.Parameters.Add(new SqlParameter("PatientId", PatientId));
                    cmd.Parameters.Add(new SqlParameter("AdmissionId", AdmissionId));
                    cmd.Parameters.Add(new SqlParameter("EncounterId", EncounterId));
                    cmd.Parameters.Add(new SqlParameter("Prescription", xmlPrescription));
                    cmd.Parameters.Add(new SqlParameter("DeliveryFee", DeliveryFee));
                    cmd.Parameters.Add(new SqlParameter("IsEditDrug", IsEditDrug));
                    cmd.Parameters.Add(new SqlParameter("IsEditConsumable", IsEditConsumable));
                    cmd.Parameters.Add(new SqlParameter("PayerCoverage", PayerCoverage));
                    cmd.Parameters.Add(new SqlParameter("IsDefaultCoverage", IsDefaultCoverage));
                    using (var da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }
                    data = (from DataRow dr in dt.Rows
                            select new ItemPrice
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
                                Instruction = dr["Instruction"].ToString()
                            }).ToList();
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