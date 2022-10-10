using a = System;
using System.Collections.Generic;
using Siloam.Service.EMRPharmacy.Repositories.IRepositories;
using Siloam.Service.EMRPharmacy.Commons;
using Siloam.Service.EMRPharmacy.Models;
using Siloam.Service.EMRPharmacy.Models.ViewModels;
using Siloam.Service.EMRPharmacy.Models.ExpressCheckout;
using System.Linq;
using System.Data.SqlClient;
using System.Data;
using System.Xml.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;

namespace Siloam.Service.EMRPharmacy.Repositories
{
    public class ExpressCheckoutRepository : DatabaseConfig, IExpressCheckoutRepository
    {
        public ExpressCheckoutRepository() : base() { }

        public ExpressCheckoutRepository(DatabaseContext context) : base(context) { }
        
        public static string ConvertSalesItemToXML(List<ExpressPrescription> data)
        {
            XDocument doc = new XDocument(new XDeclaration("1.0", "UTF-8", "yes"),
                new XElement("root",
                    from p in data
                    select new XElement("row",
                            new XAttribute("ItemId", p.ItemId),
                            new XAttribute("Quantity", p.Quantity),
                            new XAttribute("UOMID", p.UOMID)
                        )
                ));
            return doc.ToString();
        }

        public List<ExpressPrescription> GetExpressPrescriptions(long OrganizationId, long AdmissionId, Guid EncounterId)
        {
            DataTable dt = new DataTable();
            List<ExpressPrescription> data = new List<ExpressPrescription>();

            try
            {
                using (SqlConnection conn = new SqlConnection(Siloam.System.ApplicationSetting.ConnectionString))
                {
                    conn.Open();
                    SqlCommand cmd = conn.CreateCommand();
                    cmd.CommandText = "spExpressGetPrescription";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("OrganizationId", OrganizationId));
                    cmd.Parameters.Add(new SqlParameter("AdmissionId", AdmissionId));
                    cmd.Parameters.Add(new SqlParameter("EncounterId", EncounterId));
                    using (var da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }
                    data = (from DataRow dr in dt.Rows
                            select new ExpressPrescription
                            {
                                ItemId = long.Parse(dr["ItemId"].ToString()).ToString(),
                                UOMID = long.Parse(dr["UOMID"].ToString()).ToString(),
                                Quantity = decimal.Parse(dr["Quantity"].ToString()).ToString().Replace(',', '.')
                            }).ToList();
                }
            }
            catch (a.Exception ex)
            {
                throw ex;
            }
            return data;
        }

        public string ExpressProcessItemIssue(long OrganizationId, long PatientId, long AdmissionId, long DoctorId, Guid EncounterId, string UserName, List<ExpressPrescription> Item)
        {
            DataTable dt = new DataTable();
            string data = "";
            string ListItem = ConvertSalesItemToXML(Item);
            try
            {
                using (SqlConnection conn = new SqlConnection(Siloam.System.ApplicationSetting.ConnectionString))
                {
                    conn.Open();
                    SqlCommand cmd = conn.CreateCommand();
                    cmd.CommandText = "spExpressProcessItemIssue";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("OrganizationId", OrganizationId));
                    cmd.Parameters.Add(new SqlParameter("PatientId", PatientId));
                    cmd.Parameters.Add(new SqlParameter("AdmissionId", AdmissionId));
                    cmd.Parameters.Add(new SqlParameter("DoctorId", DoctorId));
                    cmd.Parameters.Add(new SqlParameter("EncounterId", EncounterId));
                    cmd.Parameters.Add(new SqlParameter("UserName", UserName));
                    cmd.Parameters.Add(new SqlParameter("ListItem", ListItem));
                    using (var da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }
                    data = (from DataRow dr in dt.Rows
                            select dr["Result"].ToString()).First();
                }
            }
            catch (a.Exception ex)
            {
                data = ex.Message;
            }
            return data;
        }
    }
}