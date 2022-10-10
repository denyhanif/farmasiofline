using a = System;
using System.Collections.Generic;
using Siloam.Service.EMRPharmacy.Repositories.IRepositories;
using Siloam.Service.EMRPharmacy.Commons;
using Siloam.Service.EMRPharmacy.Models;
using Siloam.Service.EMRPharmacy.Models.ViewModels;
using Siloam.Service.EMRPharmacy.Models.Functional;
using System.Linq;
using System.Data.SqlClient;
using System.Data;
using System.Xml.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;

namespace Siloam.Service.EMRPharmacy.Repositories
{
    public class LogZoomRepository : DatabaseConfig, ILogZoomRepository
    {
        public LogZoomRepository() : base() { }

        public LogZoomRepository(DatabaseContext context) : base(context) { }

        public string baseURL = Config.Configuration.Functions.GetValue("BaseURL_MySiloam_Doctor").ToString();

        public string InsertLogZoom(Guid EncounterId, DateTime time)
        {
            string data = "";
            try
            {
                LogTemp temp = new LogTemp();
                temp.log_id = 0;
                temp.encounter_id = EncounterId;
                temp.log_type = "DOCTOR-BEGIN ZOOM CONS";
                temp.soap_return = "";
                temp.created_date = time;
                Context.Add(temp);
                Context.SaveChanges();
                data = "SUCCESS";
                return data;
            }
            catch (Exception ex)
            {
                data = ex.Message;
                return data;
            }
        }

        public void InsertLogMySiloam(Guid EncounterId, DateTime time, string Message)
        {
            try
            {
                LogTemp temp = new LogTemp();
                temp.log_id = 0;
                temp.encounter_id = EncounterId;
                temp.log_type = "DOCTOR-CALL MYSILOAM ZOOM CONS";
                temp.soap_return = Message;
                temp.created_date = time;
                Context.Add(temp);
                Context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string DoneCheckInTele(Guid EncounterId, DateTime time, long OrganizationId, long PatientId, long AdmissionId)
        {
            DataTable dt = new DataTable();
            ZoomLog init = new ZoomLog();
            RequestZoomLog model = new RequestZoomLog();
            ResponseZoomLog data = new ResponseZoomLog();
            string result = "";
            try
            {
                using (SqlConnection conn = new SqlConnection(Siloam.System.ApplicationSetting.ConnectionString))
                {
                    conn.Open();
                    SqlCommand cmd = conn.CreateCommand();
                    cmd.CommandText = "spGetAppointmentData";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("OrganizationId", OrganizationId));
                    cmd.Parameters.Add(new SqlParameter("PatientId", PatientId));
                    cmd.Parameters.Add(new SqlParameter("AdmissionId", AdmissionId));
                    cmd.Parameters.Add(new SqlParameter("EncounterId", EncounterId));
                    using (var da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }
                    init = (from DataRow dr in dt.Rows
                            select new ZoomLog
                            {
                                AppointmentId = Guid.Parse(dr["AppointmentId"].ToString()),
                                HospitalId = Guid.Parse(dr["HospitalId"].ToString()),
                                UserId = Guid.Parse(dr["UserId"].ToString())
                            }).First();
                }

                model.appointmentId = init.AppointmentId;
                model.statusId = "5";
                model.source = "EMR - Doctor Teleconsultation";
                model.userId = init.UserId;
                model.time = DateTimeOffset.Parse(time.AddHours(-7).ToString(), null).DateTime;

                var JsonString = JsonConvert.SerializeObject(model);
                var content = new StringContent(JsonString, a.Text.Encoding.UTF8, "application/json");

                HttpClient http = new HttpClient();
                http.BaseAddress = new a.Uri(baseURL + "/api/v2/appointments/");

                http.DefaultRequestHeaders.Accept.Clear();
                http.DefaultRequestHeaders.Accept.Add(new a.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                var task = Task.Run(async () =>
                {
                    return await http.PostAsync(baseURL + "/api/v2/appointments/tele/log/hospital/" + init.HospitalId + "/doctor/" + model.userId, content);
                });

                result = task.Result.Content.ReadAsStringAsync().Result;
                var Response = (JObject)JsonConvert.DeserializeObject<dynamic>(result);
                string status = Response.Property("status").Value.ToString();
                string message = Response.Property("message").Value.ToString();

                if (status.ToLower() == "ok")
                {

                    result = "SUCCESS";
                    data.status = "SUCCESS";
                    data.message = message;
                    InsertLogMySiloam(EncounterId, DateTime.Now, message);
                    return data.status;

                }
                else
                {
                    data.status = "FAILED";
                    data.message = message;
                    InsertLogMySiloam(EncounterId, DateTime.Now, message);
                    return data.status;
                }
            }
            catch (a.Exception ex)
            {
                InsertLogMySiloam(EncounterId, DateTime.Now, ex.Message);
                return ex.Message;
            }
        }
    }
}