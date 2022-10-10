using System;
using System.Collections.Generic;
using Siloam.Service.EMRPharmacy.Repositories.IRepositories;
using Siloam.Service.EMRPharmacy.Commons;
using Siloam.Service.EMRPharmacy.Models;
using Siloam.Service.EMRPharmacy.Models.ViewModels;
using Siloam.Service.EMRPharmacy.Models.Functional;
using System.Linq;
using System.Xml.Linq;
using System.Data.SqlClient;
using System.Data;
using System.Transactions;
using Microsoft.EntityFrameworkCore;

namespace Siloam.Service.EMRPharmacy.Repositories
{

    public class AidoDrugRepository : DatabaseConfig, IAidoDrugRepository
    {

        public AidoDrugRepository() : base() { }

        public AidoDrugRepository(DatabaseContext context) : base(context)
        {
        }

        public string InsertData(long OrganizationId, long PatientId, long AdmissionId, Guid EncounterId, string JsonRequest, string JsonResponse, Guid SiloamTrxId, string ChannelId)
        {
            DataTable dt = new DataTable();
            string data;
            try
            {
                using (SqlConnection conn = new SqlConnection(Siloam.System.ApplicationSetting.ConnectionString))
                {
                    conn.Open();
                    SqlCommand cmd = conn.CreateCommand();
                    cmd.CommandText = "spInsertAidoTicket";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("OrganizationId", OrganizationId));
                    cmd.Parameters.Add(new SqlParameter("PatientId", PatientId));
                    cmd.Parameters.Add(new SqlParameter("AdmissionId", AdmissionId));
                    cmd.Parameters.Add(new SqlParameter("EncounterId", EncounterId));
                    cmd.Parameters.Add(new SqlParameter("JsonRequest", JsonRequest));
                    cmd.Parameters.Add(new SqlParameter("JsonResponse", JsonResponse));
                    cmd.Parameters.Add(new SqlParameter("SiloamTrxId", SiloamTrxId));
                    cmd.Parameters.Add(new SqlParameter("ChannelId", ChannelId));
                    using (var da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }
                    data = (from DataRow dr in dt.Rows
                            select dr["Result"].ToString()).Single();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return data;
        }

        public string UpdateData(Guid SiloamTrxId)
        {
            string data = "";
            AidoDrugTicket temp = new AidoDrugTicket();
            try
            {
                using (var context = new DatabaseContext(ContextOption))
                {

                    temp = (from aido in context.AidoDrugSet
                            where aido.is_active == true && aido.siloam_trx_id == SiloamTrxId
                            select aido).Single();

                    if (temp == null)
                    {
                        data = "DATA NOT FOUND";
                    }
                    else
                    {
                        temp.is_payment = true;
                        temp.modified_date = DateTime.Now;
                        context.Update(temp);
                        context.SaveChanges();
                        data = "SUCCESS";
                    }
                }
                return data;
            }
            catch(Exception ex)
            {
                return ex.Message;
            }
        }

        //public bool UpdateData(Guid SiloamTrxId, long OrganizationId, long PatientId, long AdmissionId, Guid EncounterId, string Shipment_Name, out string MsgDesc)
        //{
        //    try
        //    {
        //        using (var context = new DatabaseContext(ContextOption))
        //        {
        //            context.Database.ExecuteSqlCommand("spSubmitPaymentAIDO @SiloamTrxId, @PatientId, @OrganizationId, @AdmissionId, @Shipment_Name, @EncounterId", 
        //                new SqlParameter("@SiloamTrxId", SiloamTrxId.ToString()),
        //                new SqlParameter("@PatientId", PatientId),
        //                new SqlParameter("@OrganizationId", OrganizationId),
        //                new SqlParameter("@AdmissionId", AdmissionId),
        //                new SqlParameter("@Shipment_Name", Shipment_Name),
        //                new SqlParameter("@EncounterId", EncounterId.ToString()));
        //            context.SaveChanges();
        //        }

        //        MsgDesc = "SUCCESS";
        //        return true;
        //    }
        //    catch (SqlException sqlErr)
        //    {
        //        MsgDesc = sqlErr.Message;

        //        return false;
        //    }
        //    catch (Exception err)
        //    {
        //        MsgDesc = err.Message;
        //        return false;
        //    }
        //}

        public string GetSettingAidoPayerId(long OrganizationId)
        {
            string data = "";
            using (var context = new DatabaseContext(ContextOption))
            {
                data = (from a in context.SettingSet
                        where a.is_active == true && a.organization_id == OrganizationId && a.setting_name == "AIDO_PAYER_ID"
                        select a.setting_value).First();
            }
            return data;
        }

        public string GetSettingMysiloamPayerId(long OrganizationId)
        {
            string data = "";
            using (var context = new DatabaseContext(ContextOption))
            {
                data = (from a in context.SettingSet
                        where a.is_active == true && a.organization_id == OrganizationId && a.setting_name == "MYSILOAM_PAYER_ID"
                        select a.setting_value).First();
            }
            return data;
        }

        //public int GetCountNewAIDOOrder(long OrganizationId)
        //{
        //    int data = 0;
        //    List<long> payer = new List<long>();
        //    List<long> siloampayer = new List<long>();
        //    string payertemp = "", payersiloam = "";
        //    payertemp = GetSettingAidoPayerId(OrganizationId);
        //    payer = payertemp.Split(',').Select(Int64.Parse).ToList();

        //    payersiloam = GetSettingMysiloamPayerId(OrganizationId);
        //    siloampayer = payersiloam.Split(',').Select(Int64.Parse).ToList();

        //    using (var context = new DatabaseContext(ContextOption))
        //    {
        //        data = (from a in context.RecordSet
        //                where a.is_active == true && a.organization_id == OrganizationId && (payer.Contains(a.payer_id) || siloampayer.Contains(a.payer_id)) && a.take_date == null
        //                      && DateTime.Parse(a.created_date.ToString()).ToString("YYYY-MM-dd") == DateTime.Parse(DateTime.Now.ToString()).ToString("YYYY-MM-dd")
        //                select a).Count();
        //    }
        //    return data;
        //}

        //public int GetCountInvoicedAIDOOrder(long OrganizationId)
        //{
        //    int data = 0;
        //    using (var context = new DatabaseContext(ContextOption))
        //    {
        //        data = (from a in context.AidoDrugSet
        //                join b in context.RecordSet
        //                on new
        //                {
        //                    j1 = a.organization_id,
        //                    j2 = a.patient_id,
        //                    j3 = a.admission_id,
        //                    j4 = a.encounter_id
        //                }
        //                equals new
        //                {
        //                    j1 = b.organization_id,
        //                    j2 = b.patient_id,
        //                    j3 = b.admission_id,
        //                    j4 = b.encounter_id
        //                }
        //                where a.is_active == true && a.organization_id == OrganizationId && a.is_payment == true && b.is_syncHOPE == false
        //                      && DateTime.Parse(a.modified_date.ToString()).ToString("YYYY-MM-dd") == DateTime.Parse(DateTime.Now.ToString()).ToString("YYYY-MM-dd")
        //                select a).Count();
        //    }
        //    return data;
        //}
        public int GetCountInvAndCncl(long OrganizationId)
        {
            DataTable dtCount = new DataTable();
            int data;
            try
            {
                using (SqlConnection conn = new SqlConnection(Siloam.System.ApplicationSetting.ConnectionString))
                {
                    conn.Open();
                    SqlCommand cmdCount = conn.CreateCommand();
                    cmdCount.CommandText = "spGetCountInvAndCncl";
                    cmdCount.CommandType = CommandType.StoredProcedure;
                    cmdCount.Parameters.Add(new SqlParameter("ORGANIZATIONID", OrganizationId));
                    using (var da = new SqlDataAdapter(cmdCount))
                    {
                        da.Fill(dtCount);
                    }
                    data = (from DataRow dr in dtCount.Rows
                            select int.Parse(dr["Result"].ToString())).Single();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return data;
        }

        public int GetCountNewAIDOOrder(long OrganizationId)
        {
            DataTable dt = new DataTable();
            int data;
            try
            {
                using (SqlConnection conn = new SqlConnection(Siloam.System.ApplicationSetting.ConnectionString))
                {
                    conn.Open();
                    SqlCommand cmd = conn.CreateCommand();
                    cmd.CommandText = "spGetCountAidoOrder";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("ORGANIZATIONID", OrganizationId));
                    cmd.Parameters.Add(new SqlParameter("TYPE", 1));
                    using (var da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }
                    data = (from DataRow dr in dt.Rows
                            select int.Parse(dr["Result"].ToString())).Single();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return data;
        }

        public List<Notification> GetCountNewOrder(long OrganizationId)
        {
            List<Notification> data = new List<Notification>();
            DataSet dt = new DataSet();
            try
            {
                using (SqlConnection conn = new SqlConnection(Siloam.System.ApplicationSetting.ConnectionString))
                {
                    conn.Open();
                    SqlCommand cmd = conn.CreateCommand();
                    cmd.CommandText = "spGetCountTeleNewOrder";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("ORGANIZATIONID", OrganizationId));
                    using (var da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }

                    data = (from DataRow dr in dt.Tables[0].Rows
                            select new Notification()
                            {
                                IsInvoiced = bool.Parse(dr["IsInvoiced"].ToString()),
                                CountNotif = int.Parse(dr["CountNotif"].ToString()),
                            }).ToList();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return data;
        }

        public int GetCountInvoicedAIDOOrder(long OrganizationId)
        {
            DataTable dt = new DataTable();
            int data;
            try
            {
                using (SqlConnection conn = new SqlConnection(Siloam.System.ApplicationSetting.ConnectionString))
                {
                    conn.Open();
                    SqlCommand cmd = conn.CreateCommand();
                    cmd.CommandText = "spGetCountAidoOrder";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("ORGANIZATIONID", OrganizationId));
                    cmd.Parameters.Add(new SqlParameter("TYPE", 2));
                    using (var da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }
                    data = (from DataRow dr in dt.Rows
                            select int.Parse(dr["Result"].ToString())).Single();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return data;
        }

        public Guid GetSiloamTrxId(long OrganizationId, long PatientId, long AdmissionId, Guid EncounterId)
        {
            Guid data = new Guid();
            int countdata = 0;
            using (var context = new DatabaseContext(ContextOption))
            {
                countdata = (from a in context.AidoDrugSet
                             where a.is_active == true && a.organization_id == OrganizationId && a.patient_id == PatientId && a.admission_id == AdmissionId && a.encounter_id == EncounterId && a.is_payment == true
                             select a).Count();
                if (countdata == 0)
                {
                    data = Guid.Empty;
                }
                else
                {
                    data = (from a in context.AidoDrugSet
                            where a.is_active == true && a.organization_id == OrganizationId && a.patient_id == PatientId && a.admission_id == AdmissionId && a.encounter_id == EncounterId && a.is_payment == true
                            select a.siloam_trx_id).First();
                }
            }
            return data;
        }

        public int GetCountAIDOOrder(long OrganizationId, long PatientId, long AdmissionId, Guid EncounterId)
        {
            int data = 0;
            using (var context = new DatabaseContext(ContextOption))
            {
                data = (from a in context.AidoDrugSet
                        where a.is_active == true && a.organization_id == OrganizationId && a.patient_id == PatientId && a.admission_id == AdmissionId && a.encounter_id == EncounterId
                        select a).Count();
            }
            return data;
        }
    }
}