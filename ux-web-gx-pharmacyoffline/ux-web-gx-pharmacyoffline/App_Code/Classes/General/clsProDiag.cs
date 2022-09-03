using System;
using System.Net.Http;
using System.Configuration;
using System.Threading.Tasks;

/// <summary>
/// Summary description for clsProDiag
/// </summary>

public class clsProDiag
{
    public clsProDiag()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public static async Task<string> getProDiagOrder(long organizationId, long patientId, long admissionId, Guid encounterId)
    {
        try
        {
            HttpClient labResult = new HttpClient();
            labResult.BaseAddress = new Uri(ConfigurationManager.AppSettings["urlPharmacy"].ToString());

            labResult.DefaultRequestHeaders.Accept.Clear();
            labResult.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

            var task = Task.Run(async () =>
            {
                return await labResult.GetAsync(string.Format($"/printprocedurediagnostic/" + organizationId + "/" + patientId + "/" + admissionId + "/" + encounterId));
            });

            return task.Result.Content.ReadAsStringAsync().Result;
        }
        catch (Exception ex)
        {
            return ex.Message;
        }
    }
}