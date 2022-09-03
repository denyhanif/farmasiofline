using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Form_HeaderPrint : System.Web.UI.Page
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
                ConfigurationManager.AppSettings["DB_EMRTransaction"] = SiloamConfig.Functions.GetValue("DB_EMRTransaction").ToString();
                ConfigurationManager.AppSettings["DB_EMRExtension"] = SiloamConfig.Functions.GetValue("DB_EMRExtension").ToString();
            }

            if (Request.QueryString["AdmissionId"] != null && Request.QueryString["EncounterId"] != null && Request.QueryString["OrganizationId"] != null)
            {
                initializevalue(long.Parse(Request.QueryString["OrganizationId"]), long.Parse(Request.QueryString["AdmissionId"]), Guid.Parse(Request.QueryString["EncounterId"]));
                //ScriptManager.RegisterStartupScript(this, this.GetType(), "printpreview", "printpreview();", true);
                //Page.ClientScript.RegisterStartupScript(this.GetType(), "printpreview", "printpreview()", true);
                //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "printpreview", "printpreview();", true);
            }
        }
    }

    public void initializevalue(long OrganizationId, long AdmissionId, Guid EncounterId)
    {
        DataSet result = clsPatientHistory.getHeaderForPrint(long.Parse(Request.QueryString["OrganizationId"]), long.Parse(Request.QueryString["AdmissionId"]), Guid.Parse(Request.QueryString["EncounterId"]));
        DataTable headerData = result.Tables[0];

        lblmrno.Text = headerData.Rows[0].Field<string>("LocalMrNo");
        lblnamepatient.Text = headerData.Rows[0].Field<string>("PatientName");
        lbldobpatient.Text = headerData.Rows[0].Field<string>("BirthDate");
        lblsexpatient.Text = headerData.Rows[0].Field<string>("Gender");
        lbldoctorprimary.Text = headerData.Rows[0].Field<string>("DoctorName");
        lblAdmission.Text = headerData.Rows[0].Field<string>("Admission");
        lblpayername.Text = headerData.Rows[0].Field<string>("PayerName");

        var GetSettings = clsSettings.GetAppSettings(OrganizationId);
        var theSettings = JsonConvert.DeserializeObject<ResultViewOrganizationSetting>(GetSettings.Result.ToString());

        List<ViewOrganizationSetting> listSettings = new List<ViewOrganizationSetting>();
        listSettings = theSettings.list;

        string unitname = "", unitaddress = "", unitphone = "";
        if (listSettings.Find(y => y.setting_name.ToUpper() == "LOGO_PRINT").setting_value.ToUpper() == "TRUE")
        {
            unitname = listSettings.Find(y => y.setting_name.ToUpper() == "UNIT_NAME").setting_value.ToString();
            unitaddress = listSettings.Find(y => y.setting_name.ToUpper() == "UNIT_ADDRESS").setting_value.ToString();
            unitphone = listSettings.Find(y => y.setting_name.ToUpper() == "UNIT_PHONE").setting_value.ToString();

            LabelUnitAddress.Text = unitname + "<br />" + unitaddress + "<br />" + unitphone;

            if (OrganizationId == 31) //RSUSW
            {
                ImgLogoSH.ImageUrl = "~/Images/Icon/logo-RSUSW.jpg";
            }
            else
            {
                ImgLogoSH.ImageUrl = "~/Images/Icon/logo-SH.png";
            }

            ImgLogoSH.Visible = true;
            LabelUnitAddress.Visible = true;
        }
        else
        {
            ImgLogoSH.Visible = false;
            LabelUnitAddress.Visible = false;
        }

    }

}