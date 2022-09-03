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
/// Summary description for clsLogin
/// </summary>
public class userLogin
{

    public string user_name { get; set; }
    public string password { get; set; }
    public string application_id { get; set; }
}

public class clsLogin
{

    public static async Task<string> GetLogin(string UserName, string Password)
    {
        try
        {
            HttpClient httpLogin = new HttpClient();
            httpLogin.BaseAddress = new Uri(ConfigurationManager.AppSettings["urlUserManagement"].ToString());

            httpLogin.DefaultRequestHeaders.Accept.Clear();
            httpLogin.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            //httpLogin.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic",
            //    Convert.ToBase64String(ASCIIEncoding.ASCII.GetBytes(string.Format("{0}:{1}",
            //        ConfigurationManager.AppSettings["userSSO"].ToString(), ConfigurationManager.AppSettings["passSSO"].ToString()))));

            string applicationId = ConfigurationManager.AppSettings["ApplicationId"].ToString();

            userLogin data = new userLogin();
            data.user_name = UserName;
            data.password = Password;
            data.application_id = applicationId;
            var JsonString = JsonConvert.SerializeObject(data);
            var content = new StringContent(JsonString, Encoding.UTF8, "application/json");
            var task = Task.Run(async () =>
            {
                return await httpLogin.PutAsync(string.Format($"/userselectloginbyuserpassappoapps/"), content);
            });

            return task.Result.Content.ReadAsStringAsync().Result;
        }
        catch (Exception ex)
        {
            return ex.Message;
        }
    }
}