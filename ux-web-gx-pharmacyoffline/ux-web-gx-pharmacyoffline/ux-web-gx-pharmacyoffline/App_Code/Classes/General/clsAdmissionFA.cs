using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;

/// <summary>
/// Summary description for clsAdmissionFA
/// </summary>
public class clsAdmissionFA
{
    public static async Task<string> getAdmissionFirstAssesment(long org_id, string localmr_no, DateTime datestart, DateTime dateend, long doctor_id)
    {
        try
        {
            HttpClient httpLogin = new HttpClient();
            httpLogin.BaseAddress = new Uri(ConfigurationManager.AppSettings["urlPharmacy"].ToString());

            httpLogin.DefaultRequestHeaders.Accept.Clear();
            httpLogin.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

            var task = Task.Run(async () =>
            {
                return await httpLogin.GetAsync(string.Format($"/admissionfanurse/" + org_id + "/" + localmr_no + "/" + datestart.ToString("yyyy-MM-dd") + "/" + dateend.ToString("yyyy-MM-dd") + "/" + doctor_id));
            });

            return task.Result.Content.ReadAsStringAsync().Result;
        }
        catch (Exception ex)
        {
            return ex.Message;
        }
    }

    public static async Task<string> getPatientOnly(string localmr_no, long org_id)
    {
        try
        {
            HttpClient httpLogin2 = new HttpClient();
            httpLogin2.BaseAddress = new Uri(ConfigurationManager.AppSettings["urlPharmacy"].ToString());

            httpLogin2.DefaultRequestHeaders.Accept.Clear();
            httpLogin2.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

            var task = Task.Run(async () =>
            {
                return await httpLogin2.GetAsync(string.Format($"/patientnursebymr/" + localmr_no + "/" + org_id));
            });

            return task.Result.Content.ReadAsStringAsync().Result;
        }
        catch (Exception ex)
        {
            return ex.Message;
        }
    }

    public static async Task<string> getPatientData(string localmr_no, long org_id)
    {
        try
        {
            HttpClient httpLogin2 = new HttpClient();
            httpLogin2.BaseAddress = new Uri(ConfigurationManager.AppSettings["urlPharmacy"].ToString());

            httpLogin2.DefaultRequestHeaders.Accept.Clear();
            httpLogin2.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

            var task = Task.Run(async () =>
            {
                return await httpLogin2.GetAsync(string.Format($"/patientdatabymr/" + localmr_no + "/" + org_id));
            });

            return task.Result.Content.ReadAsStringAsync().Result;
        }
        catch (Exception ex)
        {
            return ex.Message;
        }
    }
}