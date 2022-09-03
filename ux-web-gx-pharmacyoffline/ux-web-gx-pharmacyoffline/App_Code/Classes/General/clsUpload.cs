using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;

/// <summary>
/// Summary description for clsUpload
/// </summary>
public class clsUpload
{
    public clsUpload()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public static async Task<string> GetAdmission(string mrno, long orgID, DateTime dateFrom, DateTime dateTo)
    {
        try
        {
            HttpClient httpLogin = new HttpClient();
            httpLogin.BaseAddress = new Uri(ConfigurationManager.AppSettings["urlHOPErep"].ToString());
            httpLogin.DefaultRequestHeaders.Accept.Clear();
            httpLogin.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            var task = Task.Run(async () =>
            {
                return await httpLogin.GetAsync(string.Format($"/admissionmr/" + mrno + "/" + orgID + "/" + dateFrom.ToString("yyyy-MM-dd") + "/" + dateTo.ToString("yyyy-MM-dd")));
            });

            return task.Result.Content.ReadAsStringAsync().Result;
        }
        catch (Exception ex)
        {
            return ex.Message;
        }
    }
}