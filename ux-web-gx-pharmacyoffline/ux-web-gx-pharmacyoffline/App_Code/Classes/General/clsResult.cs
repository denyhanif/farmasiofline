using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;

/// <summary>
/// Summary description for clsResult
/// </summary>
public class clsResult
{
    public clsResult() { }

    public static async Task<string> getLaboratoryResult(String admissionId)
    {
        try
        {
            HttpClient labResult = new HttpClient();
            labResult.BaseAddress = new Uri(ConfigurationManager.AppSettings["urlHISDataCollection"].ToString());

            labResult.DefaultRequestHeaders.Accept.Clear();
            labResult.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

            var task = Task.Run(async () =>
            {
                return await labResult.GetAsync(string.Format($"/labresult/" + admissionId));
            });

            return task.Result.Content.ReadAsStringAsync().Result;
        }
        catch (Exception ex)
        {
            return ex.Message;
        }
    }

    public static async Task<string> getRadResultAdmissionDetail(String admissionIdList)
    {
        try
        {
            HttpClient labResult = new HttpClient();
            labResult.BaseAddress = new Uri(ConfigurationManager.AppSettings["urlHISDataCollection"].ToString());

            labResult.DefaultRequestHeaders.Accept.Clear();
            labResult.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

            var task = Task.Run(async () =>
            {
                return await labResult.GetAsync(string.Format($"/radresult/" + admissionIdList));
            });

            return task.Result.Content.ReadAsStringAsync().Result;
        }
        catch (Exception ex)
        {
            return ex.Message;
        }
    }

    public static async Task<string> getPatientId(string mrNo, Int64 organizationId)
    {
        try
        {
            HttpClient patientId = new HttpClient();
            patientId.BaseAddress = new Uri(ConfigurationManager.AppSettings["urlExtension"].ToString());

            patientId.DefaultRequestHeaders.Accept.Clear();
            patientId.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

            var task = Task.Run(async () =>
            {
                return await patientId.GetAsync(string.Format($"/patientdatabymr/" + mrNo + "/" + organizationId));
            });

            return task.Result.Content.ReadAsStringAsync().Result;
        }
        catch (Exception ex)
        {
            return ex.Message;
        }
    }

    public static async Task<string> getDocResultAdmission(String patientId, string admissionId, string organizationId)
    {
        try
        {
            HttpClient labResult = new HttpClient();
            labResult.BaseAddress = new Uri(ConfigurationManager.AppSettings["urlHISDataCollection"].ToString());

            labResult.DefaultRequestHeaders.Accept.Clear();
            labResult.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

            var task = Task.Run(async () =>
            {
                return await labResult.GetAsync(string.Format($"/docresultadmission/" + patientId + "/" + admissionId + "/" + organizationId));
            });

            return task.Result.Content.ReadAsStringAsync().Result;
        }
        catch (Exception ex)
        {
            return ex.Message;
        }
    }
}