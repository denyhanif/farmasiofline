using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using log4net;
using System.Configuration;

public partial class Form_Login : System.Web.UI.Page
{
    protected static readonly ILog log = LogManager.GetLogger(typeof(Form_Login));

    List<Organization> listOrganization = new List<Organization>();
    public DataTable organizationdt = new DataTable();

    protected void Page_Load(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "dropSearch", " $('.selectpicker').selectpicker();", addScriptTags: true);

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
                ConfigurationManager.AppSettings["urlExtension"] = SiloamConfig.Functions.GetValue("urlExtension").ToString();
                ConfigurationManager.AppSettings["DB_EMRExtension"] = SiloamConfig.Functions.GetValue("DB_EMRExtension").ToString();
                ConfigurationManager.AppSettings["urlIntegration"] = SiloamConfig.Functions.GetValue("urlIntegration").ToString();
                ConfigurationManager.AppSettings["DB_Integration"] = SiloamConfig.Functions.GetValue("DB_Integration").ToString();
            }

            Session.Abandon();
            try
            {
                if (Session["SessionOrganization"] == null)
                {
                    var organizationData = clsPrescription.getOrganization();
                    var Jsonorganization = JsonConvert.DeserializeObject<ResultOrganization>(organizationData.Result.ToString());
                    listOrganization = Jsonorganization.list;
                    organizationdt = Helper.ToDataTable(listOrganization);
                    Session["SessionOrganization"] = organizationdt;
                }
            }
            catch (Exception ex)
            {
            }
        }
    }

    protected void btnSignIn_Click(object sender, EventArgs e)
    {
        try
        {
            btnSignIn.Enabled = false;


            List<Login> Login = new List<Login>();
            var GetLogin = clsLogin.GetLogin(txtUsername.Text.ToString(), txtPassword.Text.ToString());
            var ListHospital = JsonConvert.DeserializeObject<ResultLogin>(GetLogin.Result.ToString());

            List<Login> HospitalList = new List<Login>();
            HospitalList = ListHospital.list;

            if (HospitalList.Count > 0)
            {
                Session["sessionPharmacistFullName"] = HospitalList[0].full_name;
                Session[Helper.sessionPharmacist] = HospitalList[0].hope_user_id;
                Session["sessionPharmacistUsername"] = HospitalList[0].user_name;
                Session["listOrganization"] = HospitalList[0].hope_organization_id;
                Session["sessionRoleID"] = HospitalList[0].role_id;
                Session[Helper.SessionOrganization] = HospitalList[0].hope_organization_id;

                var GetSettings = clsSettings.GetAppSettings(Int64.Parse(Session["listOrganization"].ToString()));
                var theSettings = JsonConvert.DeserializeObject<ResultViewOrganizationSetting>(GetSettings.Result.ToString());

                List<ViewOrganizationSetting> listSettings = new List<ViewOrganizationSetting>();

                listSettings = theSettings.list;

                string settingValue = (from a in listSettings
                                       where a.setting_name == "IS_COMPOUND"
                                       select a.setting_value
                                       ).Single();

                Session["sessionSettingCompound"] = settingValue;
                
                Response.Redirect("~/Form/FormPrescription.aspx", false);
                Context.ApplicationInstance.CompleteRequest();
            }
            else
            {
                var Response = (JObject)JsonConvert.DeserializeObject<dynamic>(GetLogin.Result);
                var Status = Response.Property("status").Value.ToString();
                var Message = Response.Property("message").Value.ToString();

                pError.InnerText = Status + "! " + Message;
                pError.Attributes.Remove("style");
                pError.Attributes.Add("style", "display:block; color:red;");
                txtUsername.BorderColor = Color.Red;
                txtPassword.BorderColor = Color.Red;
                
            }

            btnSignIn.Enabled = true;
        }
        catch (Exception ex)
        {
            string note = ex.ToString();
            string Message = "Doctor Mapping Not Found";
            string Status = "Fail";
            pError.InnerText = Status + "! " + Message;
            pError.Attributes.Remove("style");
            pError.Attributes.Add("style", "display:block; color:red;");
            txtUsername.BorderColor = Color.Red;
            txtPassword.BorderColor = Color.Red;
            btnSignIn.Enabled = true;
        }
    }


    protected void btnContinue_onClick(object sender, EventArgs e)
    {
        log.Info(LogLibrary.Logging("S", "btnContinue_onClick", Session["sessionPharmacistUsername"].ToString(), ""));
        Session["sessionDefaultStoreName"] = dropdownStore.SelectedItem;
        Session["sessionDefaultStore"] = dropdownStore.SelectedValue;
        Response.Redirect("~/Form/FormPrescription.aspx", false);
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modalChooseStore", "$('#modalChooseStore').modal('hide');", true);
        log.Info(LogLibrary.Logging("E", "btnContinue_onClick", Session["sessionPharmacistUsername"].ToString(), ""));
    }
}