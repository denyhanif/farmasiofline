using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;

/// <summary>
/// Summary description for clsSettings
/// </summary>
public class clsSettings
{
    public static async Task<string> GetAppSettings(Int64 org_id)
    {
        try
        {
            HttpClient httpLogin = new HttpClient();
            httpLogin.BaseAddress = new Uri(ConfigurationManager.AppSettings["urlMaster"].ToString());

            httpLogin.DefaultRequestHeaders.Accept.Clear();
            httpLogin.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

            string applicationId = ConfigurationManager.AppSettings["ApplicationId"].ToString();

            var task = Task.Run(async () =>
            {
                return await httpLogin.GetAsync(string.Format($"/organizationsettingbyorgid/" + org_id));
            });

            return task.Result.Content.ReadAsStringAsync().Result;
        }
        catch (Exception ex)
        {
            return ex.Message;
        }
    }
}