using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;

/// <summary>
/// Summary description for clsPrescription
/// </summary>
public class clsSingleQueue
{
    public clsSingleQueue()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public static async Task<string> getDataSingleQueue(Int64 organization_id, string Search1, string Search2, int Search_type, DateTime adm_date)
    {
        try
        {
            HttpClient http_ = new HttpClient();
            http_.BaseAddress = new Uri(ConfigurationManager.AppSettings["urlPharmacy"].ToString());

            http_.DefaultRequestHeaders.Accept.Clear();
            http_.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

            var task = Task.Run(async () =>
            {
                return await http_.GetAsync(string.Format($"/singlequeuesoap/" + organization_id + "/" + Search1 + "/" + Search2 + "/" + Search_type + "/" + adm_date.ToString("yyyy-MM-dd")));
            });

            return task.Result.Content.ReadAsStringAsync().Result;
        }
        catch (Exception ex)
        {
            return ex.Message;
        }
    }
    public static async Task<string> postDataSingleQueue(Int64 userID, string userName, SingleQueue datasinglequeue)
    {
        var JsonString = JsonConvert.SerializeObject(datasinglequeue);
        var httpContent = new StringContent(JsonString, Encoding.UTF8, "application/json");

        try
        {
            HttpClient http_ = new HttpClient();
            http_.BaseAddress = new Uri(ConfigurationManager.AppSettings["urlPharmacy"].ToString());

            http_.DefaultRequestHeaders.Accept.Clear();
            http_.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

            var task = Task.Run(async () =>
            {
                return await http_.PostAsync(string.Format($"/singlequeuetransaction/" + userID + "/" + userName), httpContent);
            });

            return task.Result.Content.ReadAsStringAsync().Result;
        }
        catch (Exception ex)
        {
            return ex.Message;
        }
    }
   

    //CheckPrice TO HOPE
    public static async Task<string> CheckPricePrescription(Int64 orgID, Int64 ptnID, Int64 admID, string encID, List<CheckPriceRequest> dataprescription)
    {
        var JsonString = JsonConvert.SerializeObject(dataprescription);
        var httpContent = new StringContent(JsonString, Encoding.UTF8, "application/json");

        try
        {
            HttpClient price = new HttpClient();
            price.BaseAddress = new Uri(ConfigurationManager.AppSettings["urlRecord"].ToString());

            price.DefaultRequestHeaders.Accept.Clear();
            price.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

            var task = Task.Run(async () =>
            {
                return await price.PostAsync(string.Format($"/checkprice/" + orgID + "/" + ptnID + "/" + admID + "/" + encID), httpContent);
            });

            return task.Result.Content.ReadAsStringAsync().Result;
        }
        catch (Exception ex)
        {
            return ex.Message;
        }
    }

    //CheckPrice TO HOPE
    public static async Task<string> CheckPricePrescription(SingleQueue datasinglequeue)
    {
        var JsonString = JsonConvert.SerializeObject(datasinglequeue);
        var httpContent = new StringContent(JsonString, Encoding.UTF8, "application/json");

        try
        {
            HttpClient price = new HttpClient();
            price.BaseAddress = new Uri(ConfigurationManager.AppSettings["urlPharmacy"].ToString());

            price.DefaultRequestHeaders.Accept.Clear();
            price.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

            var task = Task.Run(async () =>
            {
                return await price.PostAsync(string.Format($"/singlequeuecheckprice"), httpContent);
            });

            return task.Result.Content.ReadAsStringAsync().Result;
        }
        catch (Exception ex)
        {
            return ex.Message;
        }
    }

    //Versi lama dan mungkin tidak dipakai lagi
    public static async Task<string> getQueueLineData(string hospital_id)
    {
        string mysiloamurl = ConfigurationManager.AppSettings["BaseURL_MySiloam_OPAdmin"].ToString();
        try
        {
            HttpClient httpCL = new HttpClient();

            //string urlapi = "http:///10.85.139.13/msm-be-opadmin-master/api/v2/generals/queue-line/hospital/39764039-37b9-4176-a025-ef7b2e124ba4";
            //httpCL.BaseAddress = new Uri("http:///10.85.139.13");

            httpCL.BaseAddress = new Uri(ConfigurationManager.AppSettings["BaseURL_MySiloam_OPAdmin"].ToString());

            httpCL.DefaultRequestHeaders.Accept.Clear();
            httpCL.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

            var task = Task.Run(async () =>
            {
                return await httpCL.GetAsync(string.Format($""+ mysiloamurl + "/api/v2/generals/queue-line/hospital/" + hospital_id));
            });

            return task.Result.Content.ReadAsStringAsync().Result;
        }
        catch (Exception ex)
        {
            return ex.Message;
        }
    }

    public static async Task<string> getQueueLineDataNew(string hospital_id)
    {
        string mysiloamurl = ConfigurationManager.AppSettings["BaseURL_MySiloam_OPAdmin"].ToString();
        try
        {
            HttpClient httpCL = new HttpClient();

            //string urlapi = "http:///10.85.139.13/msm-be-opadmin-master/api/v2/generals/visit-type/hospital/39764039-37b9-4176-a025-ef7b2e124ba4?categoryVisitType=2";
            //httpCL.BaseAddress = new Uri("http:///10.85.139.13");

            httpCL.BaseAddress = new Uri(ConfigurationManager.AppSettings["BaseURL_MySiloam_OPAdmin"].ToString());
            
            httpCL.DefaultRequestHeaders.Accept.Clear();
            httpCL.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

            var task = Task.Run(async () =>
            {
                //return await httpCL.GetAsync(string.Format($"/msm-be-opadmin-master/api/v2/generals/visit-type/hospital/" + hospital_id + "?categoryVisitType=" + categorytype));
                return await httpCL.GetAsync(string.Format($""+ mysiloamurl + "/api/v2/generals/visit-type/hospital/" + hospital_id));
            });

            return task.Result.Content.ReadAsStringAsync().Result;
        }
        catch (Exception ex)
        {
            return ex.Message;
        }
    }

}