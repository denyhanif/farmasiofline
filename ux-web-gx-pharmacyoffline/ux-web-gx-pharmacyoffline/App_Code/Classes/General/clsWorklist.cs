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
/// Summary description for clsWorklist
/// </summary>
public class clsWorklist
{

    public static async Task<string> getListPatientDischarge(long organization_id, long ward_id, string search)
    {
        try
        {
            HttpClient httpListPatientWorklist = new HttpClient();
            httpListPatientWorklist.BaseAddress = new Uri(ConfigurationManager.AppSettings["urlDischarge"].ToString());

            httpListPatientWorklist.DefaultRequestHeaders.Accept.Clear();
            httpListPatientWorklist.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

            var taskListPatientDischarge = Task.Run(async () =>
            {
                return await httpListPatientWorklist.GetAsync(string.Format($"/Worklist/" + organization_id + "/" + ward_id + "?Search=" + search));
            });

            var responseListWard = taskListPatientDischarge.Result.Content.ReadAsStringAsync().Result;
            return taskListPatientDischarge.Result.Content.ReadAsStringAsync().Result;
        }
        catch (Exception ex)
        {
            return ex.Message;
        }
    }

    public static async Task<string> getCountPatient(long organization_id, long ward_id)
    {
        try
        {
            HttpClient httpCountPatient = new HttpClient();
            httpCountPatient.BaseAddress = new Uri(ConfigurationManager.AppSettings["urlDischarge"].ToString());

            httpCountPatient.DefaultRequestHeaders.Accept.Clear();
            httpCountPatient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

            var taskCountPatient = Task.Run(async () =>
            {
                return await httpCountPatient.GetAsync(string.Format($"/CountWorklist/" + organization_id + "/" + ward_id));
            });

            var responseListWard = taskCountPatient.Result.Content.ReadAsStringAsync().Result;
            return taskCountPatient.Result.Content.ReadAsStringAsync().Result;
        }
        catch (Exception ex)
        {
            return ex.Message;
        }
    }

    public static async Task<string> getListDischargeProcessSearch(long organization_id, string submit_date, long wardId, long type, string search)
    {
        try
        {
            HttpClient httpListPatientWorklist = new HttpClient();
            httpListPatientWorklist.BaseAddress = new Uri(ConfigurationManager.AppSettings["urlDischarge"].ToString());

            httpListPatientWorklist.DefaultRequestHeaders.Accept.Clear();
            httpListPatientWorklist.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

            var taskListPatientDischarge = Task.Run(async () =>
            {
                return await httpListPatientWorklist.GetAsync(string.Format($"/GetDischargeRequest/" + organization_id + "/" + submit_date + "/" + wardId + "/" + type + "?search=" + search));
            });

            var responseListWard = taskListPatientDischarge.Result.Content.ReadAsStringAsync().Result;
            return taskListPatientDischarge.Result.Content.ReadAsStringAsync().Result;
        }
        catch (Exception ex)
        {
            return ex.Message;
        }
    }

    public static async Task<string> getListDischargeProcess(long organization_id, string submit_date, long wardId, string search, int type)
    {
        try
        {
            HttpClient httpListPatientWorklist = new HttpClient();
            httpListPatientWorklist.BaseAddress = new Uri(ConfigurationManager.AppSettings["urlDischarge"].ToString());

            httpListPatientWorklist.DefaultRequestHeaders.Accept.Clear();
            httpListPatientWorklist.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

            var taskListPatientDischarge = Task.Run(async () =>
            {
                return await httpListPatientWorklist.GetAsync(string.Format($"/GetDischargeRequest/" + organization_id + "/" + submit_date + "/" + wardId + "/" + type + "?search=" + search));
            });

            var responseListWard = taskListPatientDischarge.Result.Content.ReadAsStringAsync().Result;
            return taskListPatientDischarge.Result.Content.ReadAsStringAsync().Result;
        }
        catch (Exception ex)
        {
            return ex.Message;
        }
    }

    public static async Task<string> getLaboratoryResult(String admissionId)
    {
        try
        {
            HttpClient labResult = new HttpClient();
            labResult.BaseAddress = new Uri(ConfigurationManager.AppSettings["urlHISDataCollection"].ToString());

            labResult.DefaultRequestHeaders.Accept.Clear();
            labResult.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

            var task = Task.Run(async () =>
            {
                return await labResult.GetAsync(string.Format($"/labresult/" + admissionId));
            });

            return task.Result.Content.ReadAsStringAsync().Result;
        }
        catch (Exception ex)
        {
            return ex.Message;
        }
    }

    public static async Task<string> GetPatientHeader(long patientId, Guid worklistId)
    {
        try
        {
            HttpClient httpGetPatientHeader = new HttpClient();
            httpGetPatientHeader.BaseAddress = new Uri(ConfigurationManager.AppSettings["urlDischarge"].ToString());

            httpGetPatientHeader.DefaultRequestHeaders.Accept.Clear();
            httpGetPatientHeader.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

            var taskGetPatientHeader = Task.Run(async () =>
            {
                return await httpGetPatientHeader.GetAsync(string.Format($"/GetHeaderPatient/" + patientId + "/" + worklistId));
            });

            var responseGetPatientHeader = taskGetPatientHeader.Result.Content.ReadAsStringAsync().Result;
            return taskGetPatientHeader.Result.Content.ReadAsStringAsync().Result;
        }
        catch (Exception ex)
        {
            return ex.Message;
        }
    }

    public static async Task<string> GetPatientDetail(long organization_id, Guid worklist_id)
    {
        try
        {
            HttpClient httpPatientDetail = new HttpClient();
            httpPatientDetail.BaseAddress = new Uri(ConfigurationManager.AppSettings["urlDischarge"].ToString());

            httpPatientDetail.DefaultRequestHeaders.Accept.Clear();
            httpPatientDetail.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

            var task = Task.Run(async () =>
            {
                return await httpPatientDetail.GetAsync(string.Format($"/GetDetailDischargeByWorklist/" + organization_id + "/" + worklist_id));
            });

            return task.Result.Content.ReadAsStringAsync().Result;
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
}

