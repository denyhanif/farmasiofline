using log4net;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Form_FormViewer_FormChangePassword : System.Web.UI.Page
{
    protected static readonly ILog log = LogManager.GetLogger(typeof(Form_FormViewer_FormChangePassword));

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            HF_Username.Value = Request.QueryString["Username"];

            var registryflag = ConfigurationManager.AppSettings["registryflag"].ToString();
            if (registryflag == "1")
            {
                ConfigurationManager.AppSettings["urlUserManagement"] = SiloamConfig.Functions.GetValue("urlUserManagement").ToString();
            }
        }
    }

    //fungsi untuk menampilkan toast via akses javascript
    void ShowToastr(string message, string title, string type)
    {
        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "toastr_message",
            String.Format("toastr.{0}('{1}', '{2}');", type.ToLower(), message, title), addScriptTags: true);
    }

    //fungsi save change password
    protected void Pass_ButtonSavePass_Click(object sender, EventArgs e)
    {
        try
        {
            var hasil = clsCommon.ChangePasswordUser(HF_Username.Value, Pass_TextOldPass.Text, Pass_TextNewPass.Text, HF_Username.Value);
            //var hasilCentral = clsCommon.ChangePasswordUserCentral(HF_Username.Value, Pass_TextOldPass.Text, Pass_TextNewPass.Text, HF_Username.Value);

            var Response = (JObject)JsonConvert.DeserializeObject<dynamic>(hasil.Result);
            var Status = Response.Property("status").Value.ToString();
            var Message = Response.Property("message").Value.ToString();

            if (Status == "Fail")
            {
                p_Add.Attributes.Remove("style");
                p_Add.Attributes.Add("style", "display:block; color:red;");
                p_Add.InnerHtml = Message;
                //ShowToastr(Status + "! " + Message, "Save Failed", "error");
            }
            else
            {
                p_Add.Attributes.Remove("style");
                p_Add.Attributes.Add("style", "display:block; color:green;");
                p_Add.InnerHtml = "Change Password Success! <br />Silakan Login Kembali.";

                //ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "message", "$('#modalAfterSave').modal('show');", addScriptTags: true);
                clearFormPass();

                Pass_TextOldPass.Enabled = false;
                Pass_TextNewPass.Enabled = false;
                Pass_TextNewPass_confirm.Enabled = false;

                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "hidemodal", "parent.hideChangePass();", true);
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    //fungsi untuk clear form input
    void clearFormPass()
    {
        Pass_TextOldPass.Text = "";
        Pass_TextNewPass.Text = "";
        Pass_TextNewPass_confirm.Text = "";
    }

    //fungsi klik button relogin
    protected void ButtonRelogin_Click(object sender, EventArgs e)
    {
        Session.Abandon();
        Response.Redirect("~/Form/General/Login.aspx", false);
        Context.ApplicationInstance.CompleteRequest();
    }
}