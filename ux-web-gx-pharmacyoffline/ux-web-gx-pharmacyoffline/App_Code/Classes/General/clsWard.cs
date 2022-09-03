using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Net.Http;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Text;

/// <summary>
/// Summary description for Ward
/// </summary>
public class clsWard
{
    public static async Task<string> GetWard(long? organizationId)
    {
        try
        {
            HttpClient httpListWard = new HttpClient();
            httpListWard.BaseAddress = new Uri(ConfigurationManager.AppSettings["urlHisDataCollection"].ToString());

            httpListWard.DefaultRequestHeaders.Accept.Clear();
            httpListWard.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

            var taskListWard = Task.Run(async () =>
            {
                return await httpListWard.GetAsync(string.Format($"/ward/" + organizationId));
            });

            var responseListWard = taskListWard.Result.Content.ReadAsStringAsync().Result;
            return taskListWard.Result.Content.ReadAsStringAsync().Result;
        }
        catch (Exception ex)
        {
            return ex.Message;
        }
    }
}