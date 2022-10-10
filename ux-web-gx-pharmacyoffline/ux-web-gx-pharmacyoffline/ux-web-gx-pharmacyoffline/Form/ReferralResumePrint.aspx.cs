using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
public partial class Form_ReferralResumePrint : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            var registryflag = ConfigurationManager.AppSettings["registryflag"].ToString();

            if (registryflag == "1")
            {
                ConfigurationManager.AppSettings["urlPharmacy"] = SiloamConfig.Functions.GetValue("urlExtension").ToString();
                ConfigurationManager.AppSettings["urlPrescription"] = SiloamConfig.Functions.GetValue("urlTransaction").ToString();
                ConfigurationManager.AppSettings["urlFunctional"] = SiloamConfig.Functions.GetValue("urlMaster").ToString();
                ConfigurationManager.AppSettings["urlRecord"] = SiloamConfig.Functions.GetValue("urlPharmacy").ToString();
                ConfigurationManager.AppSettings["urlMaster"] = SiloamConfig.Functions.GetValue("urlMaster").ToString();
                ConfigurationManager.AppSettings["urlHISDataCollection"] = SiloamConfig.Functions.GetValue("urlHISDataCollection").ToString();
                ConfigurationManager.AppSettings["urlUserManagement"] = SiloamConfig.Functions.GetValue("urlUserManagement").ToString();
                ConfigurationManager.AppSettings["DB_Emr"] = SiloamConfig.Functions.GetValue("DB_Emr").ToString();
            }
            //initializevalue(2, 1202556, 2000005537759, Guid.Parse("28303d4c-59b8-4304-9e2c-329f530442da"));
            if (Request.QueryString["PatientId"] != null && Request.QueryString["AdmissionId"] != null && Request.QueryString["EncounterId"] != null && Request.QueryString["OrganizationId"] != null)
            {
                long PatientId = long.Parse(Request.QueryString["PatientId"]);
                long AdmissionId = long.Parse(Request.QueryString["AdmissionId"]);
                Guid EncounterId = Guid.Parse(Request.QueryString["EncounterId"]);
                long OrganizationId = long.Parse(Request.QueryString["OrganizationId"]);

                initializevalue(OrganizationId, PatientId, AdmissionId, EncounterId);
            }
            else
            {
                string close = @"<script type='text/javascript'>
                                window.returnValue = true;
                                window.close();
                                </script>";
                base.Response.Write(close);
            }
        }
    }

    public void initializevalue(long OrganizationId, long PatientId, long AdmissionId, Guid EncounterId)
    {
        try
        {
            var DataGeneral = clsReferralResume.GetReferralResume(OrganizationId, PatientId, AdmissionId, EncounterId);

            ResultReferralResume JsongetPatientHistoryGeneral = JsonConvert.DeserializeObject<ResultReferralResume>(DataGeneral.Result.ToString());
            if (JsongetPatientHistoryGeneral != null)
            {
                hfpreviewpres.Value = DataGeneral.Result.ToString();

                var GetSettings = clsSettings.GetAppSettings(OrganizationId);
                var theSettings = JsonConvert.DeserializeObject<ResultViewOrganizationSetting>(GetSettings.Result.ToString());
                List<ViewOrganizationSetting> listSettings = new List<ViewOrganizationSetting>();
                listSettings = theSettings.list;

                //List<ResumeDetail> datatemp = JsongetPatientHistoryGeneral.list.resumedetail.Where(x => x.RefType == "Rujukan").ToList();
                List<ResumeDetail> data = JsongetPatientHistoryGeneral.list.resumedetail;
                //foreach (ResumeDetail item in datatemp)
                //{
                //    data.Add(item);
                //    if (JsongetPatientHistoryGeneral.list.resumedetail.Where(x => x.RefType == "Balasan" && x.DoctorRef == item.DoctorReferralId).Any())
                //    {
                //        ResumeDetail resumeDetail = JsongetPatientHistoryGeneral.list.resumedetail.Where(x => x.RefType == "Balasan" && x.DoctorRef == item.DoctorReferralId).FirstOrDefault();
                //        data.Add(resumeDetail);
                //    }
                //}
                foreach (var col in data)
                {
                    col.CountData = data.Count();
                }

                RptReferralPrint.DataSource = Helper.ToDataTable(data);
                RptReferralPrint.DataBind();
            }
            else
            {
                RptReferralPrint.Visible = false;
            }

        }
        catch (Exception ex)
        {
            throw ex.InnerException;
        }
    }

}