using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;

/// <summary>
/// Summary description for clsMedicalHistory
/// </summary>
public class clsMedicalHistory
{
    public static async Task<string> getMedicalHistory(String patientId, Int64 type, String value)
    {
        try
        {
            HttpClient medicalClient = new HttpClient();
            medicalClient.BaseAddress = new Uri(ConfigurationManager.AppSettings["urlHISDataCollection"].ToString());

            medicalClient.DefaultRequestHeaders.Accept.Clear();
            medicalClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

            var task = Task.Run(async () =>
            {
                return await medicalClient.GetAsync(string.Format($"/medicationhistory/" + patientId + "/" + type + "/" + value));
            });

            return task.Result.Content.ReadAsStringAsync().Result;
        }
        catch (Exception ex)
        {
            return ex.Message;
        }
    }

    public static async Task<string> getMedicalHistoryNew(String patientId)
    {
        try
        {
            HttpClient medicalClient = new HttpClient();
            medicalClient.BaseAddress = new Uri(ConfigurationManager.AppSettings["urlHISDataCollection"].ToString());

            medicalClient.DefaultRequestHeaders.Accept.Clear();
            medicalClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

            var task = Task.Run(async () =>
            {
                return await medicalClient.GetAsync(string.Format($"/medicationhistory/" + patientId));
            });

            return task.Result.Content.ReadAsStringAsync().Result;
        }
        catch (Exception ex)
        {
            return ex.Message;
        }
    }
}