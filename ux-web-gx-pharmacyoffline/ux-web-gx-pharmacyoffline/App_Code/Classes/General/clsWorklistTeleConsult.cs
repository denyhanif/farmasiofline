using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;

/// <summary>
/// Summary description for clsFAResume
/// </summary>
public class clsWorklistTeleConsult
{
    public static DataTable GetWorklistTeleConsult(long OrganizationId, DateTime searchdate, string searchname)
    {
        DataTable dt = new DataTable();
        try
        {
            string constr = ConfigurationManager.AppSettings["DB_Integration"];
            using (SqlConnection conn = new SqlConnection(constr))
            {
                conn.Open();
                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = "spReferenceDoctorGetListAidoPatient";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "spReferenceDoctorGetListAidoPatient";
                cmd.Parameters.Add("@OrganizationId", SqlDbType.BigInt).Value = OrganizationId;
                cmd.Parameters.Add("@SearchDate", SqlDbType.Date).Value = searchdate.Date;
                cmd.Parameters.Add("@Search", SqlDbType.VarChar, 500).Value = searchname;
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
    
    public static DataTable getDoctorByOrganization(long OrganizationId)
    {
        DataTable dt = new DataTable();
        try
        {
            string constr = ConfigurationManager.AppSettings["DB_Integration"];
            using (SqlConnection conn = new SqlConnection(constr))
            {
                conn.Open();
                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = "spReferenceDoctorGetDoctorByOrganization";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("OrganizationId", OrganizationId));
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

    public static DataSet getPatientDetailNurse(long OrganizationId, long AdmissionId, long PatientId, Guid EncounterId)
    {
        DataSet dt = new DataSet();
        try
        {
            string constr = ConfigurationManager.AppSettings["DB_Integration"];
            using (SqlConnection conn = new SqlConnection(constr))
            {
                conn.Open();
                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = "spReferenceDoctorPatientDetailNurse";
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

    public static void UpdateReferenceDoctor(long organization_id, long admission_id, long patient_id, Guid encounter_id,
        long reference_doctor_id, DateTime next_appointment_date, int reference_type_id, long reference_id, long user_id, 
        string email, string remarks, string emailbody)
    {
        try
        {
            string constr = ConfigurationManager.AppSettings["DB_Integration"];
            using (SqlConnection conn = new SqlConnection(constr))
            {
                conn.Open();
                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = "spReferenceDoctorUpdateAppointment";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("organization_id", organization_id));
                cmd.Parameters.Add(new SqlParameter("admission_id", admission_id));
                cmd.Parameters.Add(new SqlParameter("patient_id", patient_id));
                cmd.Parameters.Add(new SqlParameter("encounter_id", encounter_id));

                cmd.Parameters.Add(new SqlParameter("reference_doctor_id", reference_doctor_id));
                cmd.Parameters.Add(new SqlParameter("next_appointment_date", next_appointment_date));
                cmd.Parameters.Add(new SqlParameter("reference_type_id", reference_type_id));
                cmd.Parameters.Add(new SqlParameter("reference_id", reference_id));
                cmd.Parameters.Add(new SqlParameter("user_id", user_id));
                cmd.Parameters.Add(new SqlParameter("email", email));
                cmd.Parameters.Add(new SqlParameter("remarks", remarks));
                cmd.Parameters.Add(new SqlParameter("emailbody", emailbody));
                cmd.ExecuteNonQuery();
                conn.Close();
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

}