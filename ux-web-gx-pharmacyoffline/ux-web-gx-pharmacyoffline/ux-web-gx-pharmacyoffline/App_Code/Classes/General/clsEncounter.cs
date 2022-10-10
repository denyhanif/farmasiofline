using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;

public class clsEncounter
{
    public clsEncounter()
    {
    }

    public static async Task<string> getEncounterDataFilter(string organization, DateTime startDate, DateTime endDate, string search)
    {
        try
        {
            HttpClient orderSet = new HttpClient();
            orderSet.BaseAddress = new Uri(ConfigurationManager.AppSettings["urlExtension"].ToString());

            orderSet.DefaultRequestHeaders.Accept.Clear();
            orderSet.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

            var task = Task.Run(async () =>
            {
                return await orderSet.GetAsync(string.Format($"/reportstatusdoctor/"+ organization +"/"+ startDate.ToString("yyyy-MM-dd") + "/"+endDate.ToString("yyyy-MM-dd") + "/"+search));
            });

            return task.Result.Content.ReadAsStringAsync().Result;
        }
        catch (Exception ex)
        {
            return ex.Message;
        }
    }
}