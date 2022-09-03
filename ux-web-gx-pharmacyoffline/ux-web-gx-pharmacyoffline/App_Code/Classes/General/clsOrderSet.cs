using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;

/// <summary>
/// Summary description for clsOrderSet
/// </summary>
public class clsOrderSet
{
    public static async Task<string> getDose()
    {
        try
        {
            HttpClient orderSet = new HttpClient();
            orderSet.BaseAddress = new Uri(ConfigurationManager.AppSettings["urlMaster"].ToString());

            orderSet.DefaultRequestHeaders.Accept.Clear();
            orderSet.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

            var task = Task.Run(async () =>
            {
                return await orderSet.GetAsync(string.Format($"/dose"));
            });

            return task.Result.Content.ReadAsStringAsync().Result;
        }
        catch (Exception ex)
        {
            return ex.Message;
        }
    }
}