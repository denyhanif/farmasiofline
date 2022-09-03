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
/// Summary description for clsCommon
/// </summary>
public class clsCommon
{
    public clsCommon()
    {
        //
        // TODO: Add constructor logic here
        //
    }


    public static async Task<string> GetOrganizationSettingbyOrgId(long orgid)
    {
        try
        {
            HttpClient httpLogin = new HttpClient();
            httpLogin.BaseAddress = new Uri(ConfigurationManager.AppSettings["urlMaster"].ToString());

            httpLogin.DefaultRequestHeaders.Accept.Clear();
            httpLogin.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            //httpLogin.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic",
            //    Convert.ToBase64String(ASCIIEncoding.ASCII.GetBytes(string.Format("{0}:{1}",
            //        ConfigurationManager.AppSettings["userSSO"].ToString(), ConfigurationManager.AppSettings["passSSO"].ToString()))));

            var task = Task.Run(async () =>
            {
                return await httpLogin.GetAsync(string.Format($"/organizationsettingbyorgid/" + orgid));
            });

            return task.Result.Content.ReadAsStringAsync().Result;
        }
        catch (Exception ex)
        {
            return ex.Message;
        }
    }


    public static async Task<string> GetSettingValue(long OrganizationId, string SettingName)
    {
        try
        {
            HttpClient httpLogin = new HttpClient();
            httpLogin.BaseAddress = new Uri(ConfigurationManager.AppSettings["urlMaster"].ToString());

            httpLogin.DefaultRequestHeaders.Accept.Clear();
            httpLogin.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            //httpLogin.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic",
            //    Convert.ToBase64String(ASCIIEncoding.ASCII.GetBytes(string.Format("{0}:{1}",
            //        ConfigurationManager.AppSettings["userSSO"].ToString(), ConfigurationManager.AppSettings["passSSO"].ToString()))));

            string applicationId = ConfigurationManager.AppSettings["ApplicationId"].ToString();

            var task = Task.Run(async () =>
            {
                return await httpLogin.GetAsync(string.Format($"/organizationsettingbyid/" + OrganizationId + "/" + SettingName));
            });

            var response = task.Result.Content.ReadAsStringAsync().Result;
            var Response = (JObject)JsonConvert.DeserializeObject<dynamic>(response);
            var data = (JObject)Response.Property("data").Value;

            return data.Property("setting_value").Value.ToString();
        }
        catch (Exception ex)
        {
            return ex.Message;
        }
    }

    public static string GetAge(DateTime BirthDate)
    {
        DateTime today = DateTime.Today;

        int months = today.Month - BirthDate.Month;
        int years = today.Year - BirthDate.Year;

        if (today.Day < BirthDate.Day)
        {
            months--;
        }

        if (months < 0)
        {
            years--;
            months += 12;
        }

        int days = (today - BirthDate.AddMonths((years * 12) + months)).Days;

        return string.Format("{0}Y {1}M {2}D",
                             years,
                             months,
                             days);
    }

    public static async Task<string> GetPatientHeader(long PatientId, string TicketId)
    {
        try
        {
            HttpClient httpLogin = new HttpClient();
            httpLogin.BaseAddress = new Uri(ConfigurationManager.AppSettings["urlPharmacy"].ToString());

            httpLogin.DefaultRequestHeaders.Accept.Clear();
            httpLogin.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            //httpLogin.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic",
            //    Convert.ToBase64String(ASCIIEncoding.ASCII.GetBytes(string.Format("{0}:{1}",
            //        ConfigurationManager.AppSettings["userSSO"].ToString(), ConfigurationManager.AppSettings["passSSO"].ToString()))));

            string applicationId = ConfigurationManager.AppSettings["ApplicationId"].ToString();

            var task = Task.Run(async () =>
            {
                return await httpLogin.GetAsync(string.Format($"/patientheader/" + PatientId + "/" + TicketId));
            });

            return task.Result.Content.ReadAsStringAsync().Result;
        }
        catch (Exception ex)
        {
            return ex.Message;
        }
    }

    public class ParamChangePass
    {
        public string user_name { get; set; }
        public string old_pass { get; set; }
        public string new_pass { get; set; }
        public string modified_by { get; set; }
    }

    public static async Task<string> ChangePasswordUser(string username, string oldpass, string newpass, string modifiedby)
    {
        ParamChangePass param = new ParamChangePass();
        param.user_name = username;
        param.old_pass = oldpass;
        param.new_pass = newpass;
        param.modified_by = modifiedby;

        var JsonString = JsonConvert.SerializeObject(param);
        var content = new StringContent(JsonString, Encoding.UTF8, "application/json");

        try
        {
            //string apicentralums = "http://10.85.129.91:8500"; //untuk persiapan ganti uri base address
            HttpClient http_putuser = new HttpClient();
            http_putuser.BaseAddress = new Uri(ConfigurationManager.AppSettings["urlUserManagement"].ToString());

            http_putuser.DefaultRequestHeaders.Accept.Clear();
            http_putuser.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

            var task = Task.Run(async () =>
            {
                return await http_putuser.PutAsync(string.Format($"/userupdatechangepassword"), content);
            });

            return task.Result.Content.ReadAsStringAsync().Result;
        }
        catch (Exception ex)
        {
            return ex.Message;
        }
    }

    public static async Task<string> ChangePasswordUserCentral(string username, string oldpass, string newpass, string modifiedby)
    {
        ParamChangePass param = new ParamChangePass();
        param.user_name = username;
        param.old_pass = oldpass;
        param.new_pass = newpass;
        param.modified_by = modifiedby;

        var JsonString = JsonConvert.SerializeObject(param);
        var content = new StringContent(JsonString, Encoding.UTF8, "application/json");

        try
        {
            HttpClient http_putuser = new HttpClient();
            http_putuser.BaseAddress = new Uri("http://10.85.129.91:8500");

            http_putuser.DefaultRequestHeaders.Accept.Clear();
            http_putuser.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

            var task = Task.Run(async () =>
            {
                return await http_putuser.PutAsync(string.Format($"/userupdatechangepassword"), content);
            });

            return task.Result.Content.ReadAsStringAsync().Result;
        }
        catch (Exception ex)
        {
            return ex.Message;
        }
    }
}