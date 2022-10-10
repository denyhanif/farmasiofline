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
using System;
using Siloam.Service.EMRPharmacy.Models.Parameter;

namespace Siloam.Service.EMRPharmacy.Repositories
{

    public class SingleQueueRepository : DatabaseConfig, ISingleQueueRepository
    {

        public SingleQueueRepository() : base() { }

        public SingleQueueRepository(DatabaseContext context) : base(context) { }

        public string UpdateDone(long OrganizationId, long PatientId, long AdmissionId, long DoctorId, a.Guid EncounterId, bool IsRetail, string Updater)
        {
            string message = "";
            SingleQueueTimeStamp data = new SingleQueueTimeStamp();
            using (var context = new DatabaseContext(ContextOption))
            {

                data = (from x in context.SQTimeStampSet
                        where x.organization_id == OrganizationId && x.patient_id == PatientId && x.admission_id == AdmissionId && x.doctor_id == DoctorId && x.encounter_id == EncounterId && x.is_retail == IsRetail && x.is_active == true
                        select x).First();

                if (data == null)
                {
                    message = "Data Not Found";
                }
                else
                {
                    data.is_done = true;
                    data.done_time = a.DateTime.Now;
                    data.modified_date = a.DateTime.Now;
                    data.modified_by = Updater;
                    context.Update(data);
                    context.SaveChanges();
                    message = "SUCCESS";
                }
            }
            return message;
        }

        public string SyncWorklistSingleQ(string JsonString)
        {
            string data = "";
            try
            {
                var content = new StringContent(JsonString, Encoding.UTF8, "application/json");
                HttpClient http = new HttpClient();
                string temp = ValueStorage.MySiloamUrlQueueEngine;
                http.BaseAddress = new a.Uri(ValueStorage.MySiloamUrlQueueEngine);

                http.DefaultRequestHeaders.Accept.Clear();
                http.DefaultRequestHeaders.Accept.Add(new a.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                var task = Task.Run(async () =>
                {
                    return await http.PostAsync(ValueStorage.MySiloamUrlQueueEngine + "/api/v2/queue-engine/transaction/emr", content);
                });

                data = task.Result.Content.ReadAsStringAsync().Result;
            }
            catch (a.Exception ex)
            {
                data = "KONEKSI KE MYSILOAM GAGAL. Exception: " + ex.Message;
                return data;
            }

            return data;
        }
        public string SyncCancelWorklistSingleQ(Guid queue_engine_trx_id, string JsonString)
        {
            string data = "";
            try
            {
                HttpClient http = new HttpClient();
                http.BaseAddress = new a.Uri(ValueStorage.MySiloamUrlQueueEngine);

                http.DefaultRequestHeaders.Accept.Clear();
                http.DefaultRequestHeaders.Accept.Add(new a.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                var request = new HttpRequestMessage(HttpMethod.Delete, ValueStorage.MySiloamUrlQueueEngine + "/api/v2/queue-engine/transaction/"+ queue_engine_trx_id.ToString());
                request.Content = new StringContent(JsonString, Encoding.UTF8, "application/json");
                var task = Task.Run(async () =>
                {
                    return await http.SendAsync(request);
                });
                data = task.Result.Content.ReadAsStringAsync().Result;
            }
            catch (a.Exception ex)
            {
                data = "KONEKSI KE MYSILOAM GAGAL. Exception: " + ex.Message;
                return data;
            }

            return data;
        }
        public SingleQueue GetDataSingleQueue(Int64 OrganizationId, Int64 DoctorId, Int64 PatientId, Int64 AdmissionId)
        {
            SingleQueue dataSQ = new SingleQueue();
            DataSet dt = new DataSet();

            try
            {
                using (SqlConnection conn = new SqlConnection(Siloam.System.ApplicationSetting.ConnectionString))
                {
                    conn.Open();
                    SqlCommand cmd = conn.CreateCommand();
                    cmd.CommandText = "spGetDataSingleQueue";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("hospital_hope_id"  , OrganizationId));
                    cmd.Parameters.Add(new SqlParameter("doctor_hope_id"    , DoctorId));
                    cmd.Parameters.Add(new SqlParameter("patient_hope_id"   , PatientId));
                    cmd.Parameters.Add(new SqlParameter("admission_hope_id" , AdmissionId));

                    using (var da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }
                    dataSQ = (from DataRow dr in dt.Tables[0].Rows
                                   select new SingleQueue()
                                   {
                                       queue_engine_trx_id  = Guid.Parse(dr["queue_engine_trx_id"].ToString()),
                                       queue_engine_id      = Guid.Parse(dr["queue_engine_id"].ToString()),
                                       is_retail            = bool.Parse(dr["is_retail"].ToString()),
                                       is_cancel            = bool.Parse(dr["is_cancel"].ToString())
                                   }).Single();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return dataSQ;
        }

        public string CancelSingleQueue(Param_CancelSingleQ param_CSQ)
        {
            DataTable dt = new DataTable();
            string data;
            try
            {
                using (SqlConnection conn = new SqlConnection(Siloam.System.ApplicationSetting.ConnectionString))
                {
                    conn.Open();
                    SqlCommand cmd = conn.CreateCommand();
                    cmd.CommandText = "spUpdateDataSingleQueue";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("hospital_hope_id"              , param_CSQ.OrganizationId));
                    cmd.Parameters.Add(new SqlParameter("doctor_hope_id"                , param_CSQ.DoctorId));
                    cmd.Parameters.Add(new SqlParameter("patient_hope_id"               , param_CSQ.PatientId));
                    cmd.Parameters.Add(new SqlParameter("admission_hope_id"             , param_CSQ.AdmissionId));
                    cmd.Parameters.Add(new SqlParameter("queue_engine_trx_id"           , param_CSQ.Queuetrxid));
                    cmd.Parameters.Add(new SqlParameter("updateby"                      , param_CSQ.updateby));
                    cmd.Parameters.Add(new SqlParameter("jsonrequest_cancel_singleq"    , param_CSQ.jsonrequest_cancel_singleq));
                    cmd.Parameters.Add(new SqlParameter("jsonresponse_cancel_singleq"   , param_CSQ.jsonresponse_cancel_singleq));
                    cmd.Parameters.Add(new SqlParameter("is_cancel"                     , param_CSQ.is_cancel));
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