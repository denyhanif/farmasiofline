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
/// Summary description for clsInpatient
/// </summary>
public class clsInpatient
{
    public static async Task<string> getRawatInap(long organizationId, string operation_schecule_id, long patient_id, string addmision_no, string encounter_id)
    {
        string StartTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
        try
        {
            HttpClient httpLogin = new HttpClient();
            ConfigurationManager.AppSettings["urlIPDOT"] = SiloamConfig.Functions.GetValue("urlIPDOT").ToString();

            httpLogin.BaseAddress = new Uri(ConfigurationManager.AppSettings["urlIPDOT"].ToString());
            //var url = "http://10.83.254.38:5700";

            httpLogin.DefaultRequestHeaders.Accept.Clear();
            httpLogin.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

            var task = Task.Run(async () =>
            {
                return await httpLogin.GetAsync(string.Format($"/OperationSchedule/refferal_opd_additional/" + organizationId + "/" + operation_schecule_id + "/" + patient_id + "/" + addmision_no + "/" + encounter_id));
            });

            string EndTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
           
            return task.Result.Content.ReadAsStringAsync().Result;
        }
        catch (Exception ex)
        {
            string ErrorTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
            return ex.Message;
        }
    }


}