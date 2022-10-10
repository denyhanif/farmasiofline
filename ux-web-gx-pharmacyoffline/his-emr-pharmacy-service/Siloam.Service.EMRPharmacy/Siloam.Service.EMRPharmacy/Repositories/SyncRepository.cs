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
using Siloam.Service.EMRPharmacy.Models.SyncHope;
using System;

namespace Siloam.Service.EMRPharmacy.Repositories
{

    public class SyncRepository : DatabaseConfig, ISyncRepository
    {

        public SyncRepository() : base() { }

        public SyncRepository(DatabaseContext context) : base(context) { }

        public string SyncPrescription(long OrganizationId, long AdmissionId, string JsonString)
        {
            string data = "";
            try
            {
                var content = new StringContent(JsonString, Encoding.UTF8, "application/json");
                HttpClient http = new HttpClient();
                http.BaseAddress = new a.Uri(ValueStorage.UrlSync);

                http.DefaultRequestHeaders.Accept.Clear();
                http.DefaultRequestHeaders.Accept.Add(new a.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                var task = Task.Run(async () =>
                {
                    return await http.PostAsync(ValueStorage.UrlSync + "/SyncDataPrescription/" + OrganizationId + "/" + AdmissionId, content);
                });

                string result = task.Result.Content.ReadAsStringAsync().Result;
                //JObject Response = (JObject)JsonConvert.DeserializeObject<dynamic>(result);
                //status = Response.Property("status").Value.ToString();
                var objResult = JsonConvert.DeserializeObject<ResultReturnValue>(result);
                data = objResult.list.Message;
            }
            catch(a.Exception ex)
            {
                data = "KONEKSI KE HOPE GAGAL";
                return data;
            }

            return data;
        }

        public string SyncConsumables(long OrganizationId, long AdmissionId, string JsonString)
        {
            string data = "";
            try
            {
                var content = new StringContent(JsonString, Encoding.UTF8, "application/json");
                HttpClient http = new HttpClient();
                http.BaseAddress = new a.Uri(ValueStorage.UrlSync);

                http.DefaultRequestHeaders.Accept.Clear();
                http.DefaultRequestHeaders.Accept.Add(new a.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                var task = Task.Run(async () =>
                {
                    return await http.PostAsync(ValueStorage.UrlSync + "/SyncDataConsumables/" + OrganizationId + "/" + AdmissionId, content);
                });

                string result = task.Result.Content.ReadAsStringAsync().Result;
                //JObject Response = (JObject)JsonConvert.DeserializeObject<dynamic>(result);
                //status = Response.Property("status").Value.ToString();
                var objResult = JsonConvert.DeserializeObject<ResultReturnValue>(result);
                data = objResult.list.Message;
            }
            catch (a.Exception ex)
            {
                data = "KONEKSI KE HOPE GAGAL";
                return data;
            }

            return data;
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

        public string SyncPrescriptionItemIssue(long OrganizationId, long AdmissionId,long StoreId,long DoctorId, long UserId,  string JsonString)
        {
            try
            {
                var content = new StringContent(JsonString, Encoding.UTF8, "application/json");
                HttpClient http = new HttpClient();
                http.BaseAddress = new a.Uri(ValueStorage.UrlSync);

                http.DefaultRequestHeaders.Accept.Clear();
                http.DefaultRequestHeaders.Accept.Add(new a.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                var task = Task.Run(async () =>
                {
                    return await http.PostAsync(ValueStorage.UrlSync + "/SyncDataPrescriptionItemIssue/" + OrganizationId + "/" + AdmissionId + "/" + StoreId + "/" + DoctorId + "/" + UserId, content);
                });
                return task.Result.Content.ReadAsStringAsync().Result;
            }
            catch (a.Exception ex)
            {
                return ex.Message;
            }
        }
        public string SyncPrescriptionToHope(long OrganizationId, long AdmissionId_SentHope, long StoreId, long DoctorId, long UserId, string JsonString)
        {
            try
            {
                var content         = new StringContent(JsonString, Encoding.UTF8, "application/json");
                HttpClient http     = new HttpClient();
                http.BaseAddress    = new a.Uri(ValueStorage.UrlSync);
                http.Timeout        = TimeSpan.FromMinutes(5);
                http.DefaultRequestHeaders.Accept.Clear();
                http.DefaultRequestHeaders.Accept.Add(new a.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                var task = Task.Run(async () =>
                {
                    return await http.PostAsync(ValueStorage.UrlSync + "/syncpresscriptiontohope/" + OrganizationId + "/" + AdmissionId_SentHope + "/" + StoreId + "/" + DoctorId + "/" + UserId, content);
                });
                return task.Result.Content.ReadAsStringAsync().Result;
            }
            catch (a.Exception ex)
            {
                return ex.Message;
            }
        }
        public string InsertLogItemsIssue(LogSyncToHope logSyncDrugToHope)
        {
            DataTable dt = new DataTable();
            string data;
            try
            {
                using (SqlConnection conn = new SqlConnection(Siloam.System.ApplicationSetting.ConnectionString))
                {
                    conn.Open();
                    SqlCommand cmd = conn.CreateCommand();
                    cmd.CommandText = "spINSERT_PHARMACY_ITEMISSUE_LOG";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("organization_hope_id", logSyncDrugToHope.organization_hope_id));
                    cmd.Parameters.Add(new SqlParameter("admission_hope_id", logSyncDrugToHope.admission_hope_id));
                    cmd.Parameters.Add(new SqlParameter("store_hope_id", logSyncDrugToHope.store_hope_id));
                    cmd.Parameters.Add(new SqlParameter("doctor_hope_id", logSyncDrugToHope.doctor_hope_id));
                    cmd.Parameters.Add(new SqlParameter("user_hope_id", logSyncDrugToHope.user_hope_id));
                    cmd.Parameters.Add(new SqlParameter("jsonrequest_senditemissue", logSyncDrugToHope.jsonrequest_senditemissue));
                    cmd.Parameters.Add(new SqlParameter("jsonresponse_sendItemIssue", logSyncDrugToHope.jsonresponse_sendItemIssue));
                    cmd.Parameters.Add(new SqlParameter("startTime", logSyncDrugToHope.startTime));
                    cmd.Parameters.Add(new SqlParameter("endTime", DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"))));
                    cmd.Parameters.Add(new SqlParameter("admission_hope_id_sentHope", logSyncDrugToHope.admission_hope_id_SentHope));
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
    }
}