using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;

/// <summary>
/// Summary description for clsPatientDetail
/// </summary>
public class clsPatientDetail
{
    public clsPatientDetail()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public static async Task<string> getRevisionHistorySOAP(long OrganizationId, long PatientId, long AdmissionId, string EncounterId)
    {
        try
        {
            HttpClient http = new HttpClient();
            http.BaseAddress = new Uri(ConfigurationManager.AppSettings["urlPrescription"].ToString());

            http.DefaultRequestHeaders.Accept.Clear();
            http.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

            var task = Task.Run(async () =>
            {
                return await http.GetAsync(string.Format($"/soaplog/" + OrganizationId + "/" + PatientId + "/" + AdmissionId + "/" + EncounterId));
            });

            return task.Result.Content.ReadAsStringAsync().Result;
        }
        catch (Exception ex)
        {
            return ex.Message;
        }
    }

    public static async Task<string> GetPatientDashboard(long organizationId, long PatientId, long admissionid, string EncounterId)
    {
        try
        {
            HttpClient dashboard = new HttpClient();
            dashboard.BaseAddress = new Uri(ConfigurationManager.AppSettings["urlPrescription"].ToString());

            dashboard.DefaultRequestHeaders.Accept.Clear();
            dashboard.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

            var task = Task.Run(async () =>
            {
                return await dashboard.GetAsync(string.Format($"/patientdashboard/" + organizationId + "/" + PatientId + "/" + admissionid + "/" + EncounterId));
            });

            return task.Result.Content.ReadAsStringAsync().Result;
        }
        catch (Exception ex)
        {
            return ex.Message;
        }
    }

    public static async Task<string> GetAdmissionHistory(long PatientId, long doctorId, int Year)
    {
        try
        {
            HttpClient admhis = new HttpClient();
            admhis.BaseAddress = new Uri(ConfigurationManager.AppSettings["urlHISDataCollection"].ToString());

            admhis.DefaultRequestHeaders.Accept.Clear();
            admhis.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

            var task = Task.Run(async () =>
            {
                return await admhis.GetAsync(string.Format($"/AdmissionHistory/" + PatientId + "/" + doctorId + "/" + Year + "/"));
            });

            return task.Result.Content.ReadAsStringAsync().Result;
        }
        catch (Exception ex)
        {
            return ex.Message;
        }
    }
}