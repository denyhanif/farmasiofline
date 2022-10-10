using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using static SOAPRevisionHistory;

public partial class Form_FormViewer_Referral_FormReferralList : System.Web.UI.Page
{
    HiddenField organizationId;
    HiddenField patientId;
    HiddenField admId;
    HiddenField encId;
    HiddenField printBY;
    HiddenField doctorName;
    HiddenField pageSOAP;

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
                ConfigurationManager.AppSettings["DB_EMRTransaction"] = SiloamConfig.Functions.GetValue("DB_EMRTransaction").ToString();
            }

            if (Request.QueryString["PatientId"] != null)
            {
                var varResultReferral = clsReferralResume.getPatientReferral(Convert.ToInt64(Request.QueryString["OrganizationId"]), Convert.ToInt64(Request.QueryString["PatientId"]), Convert.ToInt64(Request.QueryString["AdmissionId"]));
                var JsongetMapReferral = JsonConvert.DeserializeObject<ResultPatientReferral>(varResultReferral.Result.ToString());

                List<PatientReferral> patientReferrals = JsongetMapReferral.list;
                patientReferrals = patientReferrals.OrderByDescending(x => x.IsSelf).ToList();
                rptReferralList.DataSource = Helper.ToDataTable(patientReferrals);
                rptReferralList.DataBind();

            }
        }
    }

  

}