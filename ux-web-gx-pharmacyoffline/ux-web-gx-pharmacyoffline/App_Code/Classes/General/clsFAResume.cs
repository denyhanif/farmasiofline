using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;

/// <summary>
/// Summary description for clsFAResume
/// </summary>
public class clsFAResume
{
    public static async Task<string> GetFAResume(long OrganizationId, long PatientId, long AdmissionId, Guid EncounterId)
    {
        try
        {
            HttpClient medicalClient = new HttpClient();
            medicalClient.BaseAddress = new Uri(ConfigurationManager.AppSettings["urlnurse"].ToString());

            medicalClient.DefaultRequestHeaders.Accept.Clear();
            medicalClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

            var task = Task.Run(async () =>
            {
                return await medicalClient.GetAsync(string.Format($"/faresume/" + OrganizationId + "/" + PatientId + "/" + AdmissionId + "/" + EncounterId));
            });

            return task.Result.Content.ReadAsStringAsync().Result;
        }
        catch (Exception ex)
        {
            return ex.Message;
        }
    }

    public static async Task<string> GetFAResumeObgyn(long OrganizationId, long PatientId, long AdmissionId, Guid EncounterId)
    {
        try
        {
            HttpClient medicalClient = new HttpClient();
            medicalClient.BaseAddress = new Uri(ConfigurationManager.AppSettings["urlnurse"].ToString());

            medicalClient.DefaultRequestHeaders.Accept.Clear();
            medicalClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

            var task = Task.Run(async () =>
            {
                return await medicalClient.GetAsync(string.Format($"/faresumeobgyn/" + OrganizationId + "/" + PatientId + "/" + AdmissionId + "/" + EncounterId));
            });

            return task.Result.Content.ReadAsStringAsync().Result;
        }
        catch (Exception ex)
        {
            return ex.Message;
        }
    }

    public static async Task<string> GetFAResumePediatric(long OrganizationId, long PatientId, long AdmissionId, Guid EncounterId)
    {
        try
        {
            HttpClient medicalClient = new HttpClient();
            medicalClient.BaseAddress = new Uri(ConfigurationManager.AppSettings["urlnurse"].ToString());

            medicalClient.DefaultRequestHeaders.Accept.Clear();
            medicalClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

            var task = Task.Run(async () =>
            {
                return await medicalClient.GetAsync(string.Format($"/faresumepediatric/" + OrganizationId + "/" + PatientId + "/" + AdmissionId + "/" + EncounterId));
            });

            return task.Result.Content.ReadAsStringAsync().Result;
        }
        catch (Exception ex)
        {
            return ex.Message;
        }
    }
}