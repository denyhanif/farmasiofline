using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for ReferenceLetter
/// </summary>
public class clsReferenceLetter
{
    public clsReferenceLetter()
    {
    }

    public static DataSet getPatientData(long OrganizationId, long AdmissionId, long PatientId, Guid EncounterId)
    {
        DataSet dt = new DataSet();
        try
        {
            string constr = ConfigurationManager.AppSettings["DB_Integration"];
            using (SqlConnection conn = new SqlConnection(constr))
            {
                conn.Open();
                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = "spReferenceDoctorGetPatientDetail";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("OrganizationId", OrganizationId));
                cmd.Parameters.Add(new SqlParameter("AdmissionId", AdmissionId));
                cmd.Parameters.Add(new SqlParameter("PatientId", PatientId));
                cmd.Parameters.Add(new SqlParameter("EncounterId", EncounterId));
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

    public static DataSet InsertReference(long organization_id, long admission_id, long patient_id, Guid encounter_id, short reference_type_id, long reference_id, long user_id, string remarks, short kondisiinfeksius, short kondisiimmun)
    {
        DataSet dt = new DataSet();
        try
        {
            string constr = ConfigurationManager.AppSettings["DB_Integration"];
            using (SqlConnection conn = new SqlConnection(constr))
            {
                conn.Open();
                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = "spReferenceDoctorInsertReference";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("organization_id", organization_id));
                cmd.Parameters.Add(new SqlParameter("admission_id", admission_id));
                cmd.Parameters.Add(new SqlParameter("patient_id", patient_id));
                cmd.Parameters.Add(new SqlParameter("encounter_id", encounter_id));
                cmd.Parameters.Add(new SqlParameter("reference_type_id", reference_type_id));
                cmd.Parameters.Add(new SqlParameter("reference_id", reference_id));
                cmd.Parameters.Add(new SqlParameter("user_id", user_id));
                cmd.Parameters.Add(new SqlParameter("remarks", remarks));
                cmd.Parameters.Add(new SqlParameter("@isInfectious", kondisiinfeksius));
                cmd.Parameters.Add(new SqlParameter("@isImmunoComp", kondisiimmun));
                    
                cmd.ExecuteNonQuery();
                conn.Close();
            }

            return dt;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

}