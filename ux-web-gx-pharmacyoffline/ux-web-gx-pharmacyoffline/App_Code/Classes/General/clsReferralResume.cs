using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;

/// <summary>
/// Summary description for clsMedicalResume
/// </summary>
public class clsReferralResume
{
    public static async Task<string> GetReferralResume(long OrganizationId, long PatientId, long AdmissionId, Guid EncounterId)
    {
        try
        {
            HttpClient medicalClient = new HttpClient();
            medicalClient.BaseAddress = new Uri(ConfigurationManager.AppSettings["urlPrescription"].ToString());

            medicalClient.DefaultRequestHeaders.Accept.Clear();
            medicalClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

            var task = Task.Run(async () =>
            {
                return await medicalClient.GetAsync(string.Format($"/referralresume/" + OrganizationId + "/" + PatientId + "/" + AdmissionId + "/" + EncounterId));
            });

            return task.Result.Content.ReadAsStringAsync().Result;
        }
        catch (Exception ex)
        {
            return ex.Message;
        }
    }

    public static async Task<string> getPatientReferral(long OrganizationId, long PatientId, long AdmissionId)
    {
        try
        {
            HttpClient orderSet = new HttpClient();
            orderSet.BaseAddress = new Uri(ConfigurationManager.AppSettings["urlPrescription"].ToString());

            orderSet.DefaultRequestHeaders.Accept.Clear();
            orderSet.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

            var task = Task.Run(async () =>
            {
                return await orderSet.GetAsync(string.Format($"/patientreferral/" + OrganizationId + "/" + PatientId + "/" + AdmissionId));
            });

            return task.Result.Content.ReadAsStringAsync().Result;
        }
        catch (Exception ex)
        {
            return ex.Message;
        }
    }

    public static async Task<string> getPatientReferralBalasan(long OrganizationId, long PatientId, long AdmissionId)
    {
        try
        {
            HttpClient orderSet = new HttpClient();
            orderSet.BaseAddress = new Uri(ConfigurationManager.AppSettings["urlPrescription"].ToString());

            orderSet.DefaultRequestHeaders.Accept.Clear();
            orderSet.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

            var task = Task.Run(async () =>
            {
                return await orderSet.GetAsync(string.Format($"/patientreferralbalasan/" + OrganizationId + "/" + PatientId + "/" + AdmissionId));
            });

            return task.Result.Content.ReadAsStringAsync().Result;
        }
        catch (Exception ex)
        {
            return ex.Message;
        }
    }

}