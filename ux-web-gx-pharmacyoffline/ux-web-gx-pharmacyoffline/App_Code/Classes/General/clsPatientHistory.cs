using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Xml.Linq;

/// <summary>
/// Summary description for clsPatientHistory
/// </summary>
public class clsPatientHistory
{
    public clsPatientHistory()
    { }

    public static DataTable getScannedData(string patientId, byte isActive, long orgID)
    {
        DataTable dt = new DataTable();
        try
        {
            string constr = ConfigurationManager.AppSettings["DB_HIS_External"];
            using (SqlConnection conn = new SqlConnection(constr))
            {
                conn.Open();
                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = "spGetScannedData";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("OrganizationId", orgID));
                cmd.Parameters.Add(new SqlParameter("patientID", patientId));
                cmd.Parameters.Add(new SqlParameter("isActive", isActive));
                using (var da = new SqlDataAdapter(cmd))
                {
                    da.Fill(dt);
                }
                conn.Close();
            }

            return dt;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public static async Task<string> getOtherUnitData(Int64 patientID, Int64 organizationId, int year)
    {
        try
        {
            HttpClient medicalClient = new HttpClient();
            medicalClient.BaseAddress = new Uri(ConfigurationManager.AppSettings["urlPharmacy"].ToString());

            medicalClient.DefaultRequestHeaders.Accept.Clear();
            medicalClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

            var task = Task.Run(async () =>
            {
                return await medicalClient.GetAsync(string.Format($"/otherunitmr/" + patientID + "/" + organizationId + "/" + year));
            });

            return task.Result.Content.ReadAsStringAsync().Result;
        }
        catch (Exception ex)
        {
            return ex.Message;
        }
    }

    public static async Task<string> getHOPEemrData(Int64 OrganizationId, string PatientId, DateTime startDate, DateTime endDate)
    {
        var start = startDate.ToString("yyyy-MM-dd");
        var end = endDate.ToString("yyyy-MM-dd");
        try
        {
            HttpClient hopeClient = new HttpClient();
            hopeClient.BaseAddress = new Uri(ConfigurationManager.AppSettings["urlPharmacy"].ToString());

            hopeClient.DefaultRequestHeaders.Accept.Clear();
            hopeClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

            var task = Task.Run(async () =>
            {
                return await hopeClient.GetAsync(string.Format($"/listMRHope/" + OrganizationId + "/" + PatientId + "/" + startDate.ToString("yyyy-MM-dd") + "/" + endDate.ToString("yyyy-MM-dd")));
            });

            return task.Result.Content.ReadAsStringAsync().Result;
        }
        catch (Exception ex)
        {
            return ex.Message;
        }
    }

    public static async Task<string> getStatusMR(Int64 OrgId, string name)
    {
        try
        {
            HttpClient orderSet = new HttpClient();
            orderSet.BaseAddress = new Uri(ConfigurationManager.AppSettings["urlMaster"].ToString());

            orderSet.DefaultRequestHeaders.Accept.Clear();
            orderSet.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            var task = Task.Run(async () =>
            {
                return await orderSet.GetAsync(string.Format($"/organizationsettingbyid/" + OrgId + "/" + name));
            });

            return task.Result.Content.ReadAsStringAsync().Result;
        }
        catch (Exception ex)
        {
            return ex.Message;
        }
    }

    public static async Task<string> getPatientHistoryData(Int64 OrganizationId, Int64 PatientId, Int64 AdmissionId, String EncounterId)
    {
        try
        {
            HttpClient medicalClient = new HttpClient();
            medicalClient.BaseAddress = new Uri(ConfigurationManager.AppSettings["urlPrescription"].ToString());

            medicalClient.DefaultRequestHeaders.Accept.Clear();
            medicalClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

            var task = Task.Run(async () =>
            {
                return await medicalClient.GetAsync(string.Format($"/patienthistory/" + OrganizationId + "/" + PatientId + "/" + AdmissionId + "/" + EncounterId));
            });

            return task.Result.Content.ReadAsStringAsync().Result;
        }
        catch (Exception ex)
        {
            return ex.Message;
        }
    }

    public static async Task<string> getPatientHistoryDataObgyn(Int64 OrganizationId, Int64 PatientId, Int64 AdmissionId, String EncounterId)
    {
        try
        {
            HttpClient medicalClient = new HttpClient();
            medicalClient.BaseAddress = new Uri(ConfigurationManager.AppSettings["urlPrescription"].ToString());

            medicalClient.DefaultRequestHeaders.Accept.Clear();
            medicalClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

            var task = Task.Run(async () =>
            {
                return await medicalClient.GetAsync(string.Format($"/patienthistoryobgyn/" + OrganizationId + "/" + PatientId + "/" + AdmissionId + "/" + EncounterId));
            });

            return task.Result.Content.ReadAsStringAsync().Result;
        }
        catch (Exception ex)
        {
            return ex.Message;
        }
    }

    public static async Task<string> getPatientHistoryDataPediatric(Int64 OrganizationId, Int64 PatientId, Int64 AdmissionId, String EncounterId)
    {
        try
        {
            HttpClient medicalClient = new HttpClient();
            medicalClient.BaseAddress = new Uri(ConfigurationManager.AppSettings["urlPrescription"].ToString());

            medicalClient.DefaultRequestHeaders.Accept.Clear();
            medicalClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

            var task = Task.Run(async () =>
            {
                return await medicalClient.GetAsync(string.Format($"/patienthistorypediatric/" + OrganizationId + "/" + PatientId + "/" + AdmissionId + "/" + EncounterId));
            });

            return task.Result.Content.ReadAsStringAsync().Result;
        }
        catch (Exception ex)
        {
            return ex.Message;
        }
    }

    public static async Task<string> getEncounterPatientHistory(string patientId)
    {
        try
        {
            HttpClient medicalClient = new HttpClient();

            medicalClient.BaseAddress = new Uri(ConfigurationManager.AppSettings["urlPharmacy"].ToString());

            medicalClient.DefaultRequestHeaders.Accept.Clear();
            medicalClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

            var task = Task.Run(async () =>
            {
                return await medicalClient.GetAsync(string.Format($"/encounterhistory/" + patientId));
            });

            return task.Result.Content.ReadAsStringAsync().Result;
        }
        catch (Exception ex)
        {
            return ex.Message;
        }
    }

    public static async Task<string> getPatientId(string mrNO, long orgID)
    {
        try
        {
            HttpClient medicalClient = new HttpClient();

            medicalClient.BaseAddress = new Uri(ConfigurationManager.AppSettings["urlPharmacy"].ToString());

            medicalClient.DefaultRequestHeaders.Accept.Clear();
            medicalClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

            var task = Task.Run(async () =>
            {
                return await medicalClient.GetAsync(string.Format($"/patientidbymr/" + mrNO + "/" + orgID));
            });

            return task.Result.Content.ReadAsStringAsync().Result;
        }
        catch (Exception ex)
        {
            return ex.Message;
        }
    }

    public static async Task<string> getPatientData(string mrNO, long orgID)
    {
        try
        {
            HttpClient medicalClient = new HttpClient();

            medicalClient.BaseAddress = new Uri(ConfigurationManager.AppSettings["urlPharmacy"].ToString());

            medicalClient.DefaultRequestHeaders.Accept.Clear();
            medicalClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

            var task = Task.Run(async () =>
            {
                return await medicalClient.GetAsync(string.Format($"/patientdatabymr/" + mrNO + "/" + orgID));
            });

            return task.Result.Content.ReadAsStringAsync().Result;
        }
        catch (Exception ex)
        {
            return ex.Message;
        }
    }

    public static async Task<string> getPatientHistoryLite(string OrganizationId, string patientId, DateTime StartDate, DateTime EndDate, string Type)
    {
        try
        {
            HttpClient orderSet = new HttpClient();
            orderSet.BaseAddress = new Uri(ConfigurationManager.AppSettings["urlPrescription"].ToString());

            orderSet.DefaultRequestHeaders.Accept.Clear();
            orderSet.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

            var task = Task.Run(async () =>
            {
                return await orderSet.GetAsync(string.Format($"/patienthistorylite/" + OrganizationId + "/" + patientId + "/" + StartDate.ToString("yyyy-MM-dd") +"/" + EndDate.ToString("yyyy-MM-dd") + "/" + Type));
            });

            return task.Result.Content.ReadAsStringAsync().Result;
        }
        catch (Exception ex)
        {
            return ex.Message;
        }
    }

    public static async Task<string> getPatientHistoryOPDIPD(string OrganizationId, string patientId, DateTime StartDate, DateTime EndDate, string Type, string DoctorId)
    {
        try
        {
            HttpClient orderSet = new HttpClient();
            orderSet.BaseAddress = new Uri(ConfigurationManager.AppSettings["urlPrescription"].ToString());

            orderSet.DefaultRequestHeaders.Accept.Clear();
            orderSet.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

            var task = Task.Run(async () =>
            {
                return await orderSet.GetAsync(string.Format($"/patienthistoryall/" + OrganizationId + "/" + patientId + "/" + StartDate.ToString("yyyy-MM-dd") + "/" + EndDate.ToString("yyyy-MM-dd") + "/" + Type + "/" + DoctorId));
            });

            return task.Result.Content.ReadAsStringAsync().Result;
        }
        catch (Exception ex)
        {
            return ex.Message;
        }
    }

    public static async Task<string> getPatientHistoryOPDIPDUsePagination(string organizationId, string patientId, string type, int limit, int page, DateTime startDate, DateTime endDate)
    {
        try
        {
            HttpClient httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri(ConfigurationManager.AppSettings["urlPrescription"].ToString());
            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

            var task = Task.Run(async () =>
            {
                return await httpClient.GetAsync(string.Format($"/patienthistorypaging/" + organizationId + "/" + patientId + "/" + limit + "/" + page + "/" + type + "/" + startDate.ToString("yyyy-MM-dd") + "/" + endDate.ToString("yyyy-MM-dd")));
            });

            return task.Result.Content.ReadAsStringAsync().Result;
        }

        catch (Exception ex)
        {
            return ex.Message;
        }
    }

    public static async Task<string> getDiseaseClassificationType(string OrganizationId)
    {
        try
        {
            HttpClient orderSet = new HttpClient();
            orderSet.BaseAddress = new Uri(ConfigurationManager.AppSettings["urlFunctional"].ToString());

            orderSet.DefaultRequestHeaders.Accept.Clear();
            orderSet.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

            var task = Task.Run(async () =>
            {
                return await orderSet.GetAsync(string.Format($"/diseaseclassificationtype/" + OrganizationId));
            });

            return task.Result.Content.ReadAsStringAsync().Result;
        }
        catch (Exception ex)
        {
            return ex.Message;
        }
    }


    public static DataTable getPrescLite(long PatientId)
    {
        DataTable dt = new DataTable();
        try
        {
            string constr = ConfigurationManager.AppSettings["DB_EMRTransaction"];
            using (SqlConnection conn = new SqlConnection(constr))
            {
                conn.Open();
                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = "spGetPatientHistoryLite";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("PatientId", PatientId));
                using (var da = new SqlDataAdapter(cmd))
                {
                    da.Fill(dt);
                }
                conn.Close();
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return dt;
    }


    public static string ConvertSubjectiveToXML(List<AllDataForPrint> data)
    {
        XDocument doc = new XDocument(new XDeclaration("1.0", "UTF-8", "yes"),
            new XElement("root",
                from p in data
                select new XElement("row",
                        new XAttribute("AdmissionId", p.AdmissionId),
                        new XAttribute("EncounterId", p.EncounterId),
                        new XAttribute("OrganizationId", p.OrganizationId)
                    )
            ));
        return doc.ToString();
    }

    public static DataTable getObat(List<AllDataForPrint> data)
    {
        DataTable dt = new DataTable();
        try
        {

            string param = ConvertSubjectiveToXML(data);
            string constr = ConfigurationManager.AppSettings["DB_Integration"];
            using (SqlConnection conn = new SqlConnection(constr))
            {
                conn.Open();
                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = "spGetPrescription";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("Header", param));
                using (var da = new SqlDataAdapter(cmd))
                {
                    da.Fill(dt);
                }
                conn.Close();
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return dt;
    }

    public static DataSet getAllForPrint( long OrganizationId, long AdmissionId, Guid EncounterId)
    {
        DataSet dt = new DataSet();
        try
        {
            string constr = ConfigurationManager.AppSettings["DB_EMRTRANSACTION"];
            using (SqlConnection conn = new SqlConnection(constr))
            {
                conn.Open();
                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = "spGetPatientHistoryPrint";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("OrganizationId", OrganizationId));
                cmd.Parameters.Add(new SqlParameter("AdmissionId", AdmissionId));
                cmd.Parameters.Add(new SqlParameter("EncounterId", EncounterId));
                using (var da = new SqlDataAdapter(cmd))
                {
                    da.Fill(dt);
                }

                //DataTable dtHeader = dt.Tables[0];
                //DataTable dtContent = dt.Tables[1];

                conn.Close();
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return dt;
    }

    public static DataSet getHeaderForPrint(long OrganizationId, long AdmissionId, Guid EncounterId)
    {
        DataSet dt = new DataSet();
        try
        {
            string constr = ConfigurationManager.AppSettings["DB_EMRExtension"];
            using (SqlConnection conn = new SqlConnection(constr))
            {
                conn.Open();
                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = "spGetHeaderPrint";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("OrganizationId", OrganizationId));
                cmd.Parameters.Add(new SqlParameter("AdmissionId", AdmissionId));
                cmd.Parameters.Add(new SqlParameter("EncounterId", EncounterId));
                using (var da = new SqlDataAdapter(cmd))
                {
                    da.Fill(dt);
                }

                //DataTable dtHeader = dt.Tables[0];
                //DataTable dtContent = dt.Tables[1];

                conn.Close();
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return dt;
    }
}