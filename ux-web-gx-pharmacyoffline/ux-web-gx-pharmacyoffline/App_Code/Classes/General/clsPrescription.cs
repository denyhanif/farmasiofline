using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;

/// <summary>
/// Summary description for clsPrescription
/// </summary>
public class clsPrescription
{
    public clsPrescription()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public static async Task<string> getSearchWorklist(Int64 organization_id, DateTime dateform, DateTime dateto, string Search)
    {
        try
        {
            HttpClient httpLogin = new HttpClient();
            httpLogin.BaseAddress = new Uri(ConfigurationManager.AppSettings["urlPharmacy"].ToString());

            httpLogin.DefaultRequestHeaders.Accept.Clear();
            httpLogin.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

            var task = Task.Run(async () =>
            {
                return await httpLogin.GetAsync(string.Format($"/worklistpharmacy/" + organization_id + "/" + dateform.ToString("yyyy-MM-dd") + "/" + dateto.ToString("yyyy-MM-dd") + "?Search=" + Search));
            });

            return task.Result.Content.ReadAsStringAsync().Result;
        }
        catch (Exception ex)
        {
            return ex.Message;
        }
    }

    public static async Task<string> getOrganization()
    {
        try
        {
            HttpClient orderSet = new HttpClient();
            orderSet.BaseAddress = new Uri(ConfigurationManager.AppSettings["urlMaster"].ToString());

            orderSet.DefaultRequestHeaders.Accept.Clear();
            orderSet.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

            var task = Task.Run(async () =>
            {
                return await orderSet.GetAsync(string.Format($"/organization"));
            });

            return task.Result.Content.ReadAsStringAsync().Result;
        }
        catch (Exception ex)
        {
            return ex.Message;
        }
    }
}