using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;

/// <summary>
/// Summary description for clsLabRabOrder
/// </summary>
public class clsLabRadOrder
{
    public clsLabRadOrder()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public static async Task<string> getWorkListLabRad(long OrganizationId, long MrNo)
    {
        try
        {
            HttpClient labResult = new HttpClient();
            labResult.BaseAddress = new Uri(ConfigurationManager.AppSettings["urlPharmacy"].ToString());

            labResult.DefaultRequestHeaders.Accept.Clear();
            labResult.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

            var task = Task.Run(async () =>
            {
                return await labResult.GetAsync(string.Format($"/worklistviewerlabrad/" + OrganizationId + "/" + MrNo));
            });

            return task.Result.Content.ReadAsStringAsync().Result;
        }
        catch (Exception ex)
        {
            return ex.Message;
        }
    }

    public static async Task<string> getDetailLabRad(long OrganizationId, long patientId, long admissionId, string EncounterId, int isLabRad)
    {
        try
        {
            ConfigurationManager.AppSettings["urlIntegration"] = SiloamConfig.Functions.GetValue("urlIntegration").ToString();

            HttpClient labResult = new HttpClient();
            labResult.BaseAddress = new Uri(ConfigurationManager.AppSettings["urlIntegration"].ToString());

            labResult.DefaultRequestHeaders.Accept.Clear();
            labResult.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

            var task = Task.Run(async () =>
            {
                return await labResult.GetAsync(string.Format($"/detaillabradprint/" + OrganizationId + "/" + patientId + "/" + admissionId + "/" + EncounterId + "/" + isLabRad));
            });

            return task.Result.Content.ReadAsStringAsync().Result;
        }
        catch (Exception ex)
        {
            return ex.Message;
        }
    }

    public static async Task<string> getDiagprocLabRad(long orgid, long patientid, long admid, Guid encid)
    {
        try
        {
            HttpClient labResult = new HttpClient();
            labResult.BaseAddress = new Uri(ConfigurationManager.AppSettings["urlPharmacy"].ToString());

            labResult.DefaultRequestHeaders.Accept.Clear();
            labResult.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

            var task = Task.Run(async () =>
            {
                return await labResult.GetAsync(string.Format($"/encounterprocedure/" + orgid + "/" + patientid + "/" + admid + "/" + encid));
            });

            return task.Result.Content.ReadAsStringAsync().Result;
        }
        catch (Exception ex)
        {
            return ex.Message;
        }
    }

    public static async Task<string> getPrescriptionOrder(long orgid, long patientid, long admid, Guid encid)
    {
        try
        {
            HttpClient presResult = new HttpClient();
            presResult.BaseAddress = new Uri(ConfigurationManager.AppSettings["urlIntegration"].ToString());

            presResult.DefaultRequestHeaders.Accept.Clear();
            presResult.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

            var task = Task.Run(async () =>
            {
                return await presResult.GetAsync(string.Format($"/prescriptionorder/" + orgid + "/" + patientid + "/" + admid + "/" + encid));
            });

            return task.Result.Content.ReadAsStringAsync().Result;
        }
        catch (Exception ex)
        {
            return ex.Message;
        }
    }
}