using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Form_FormPatientHistory : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            HyperLink test = Master.FindControl("HyperLink1") as HyperLink;
            test.Style.Add("background-color", "#D6DBFF");
            test.Style.Add("color", "#000000 !important");
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {

        string patientId = "";


        var dataPatientId = clsPatientHistory.getPatientId(txtMRno.Text, Helper.organizationId);
        var patientIdOwned = (JObject)JsonConvert.DeserializeObject<dynamic>(dataPatientId.Result.ToString());

        if (patientIdOwned.Property("status").Value.ToString() == "Fail")
        {
            divFrame.Visible = false;
            img_noData.Visible = true;
            no_patient_data.Visible = true;
            searchSection.Attributes.Remove("style");
            searchSection.Attributes.Add("style", "margin-top:-4% ; margin-right:-12%; position:absolute; width:100%; margin-bottom:10%");
        }

        else
        {
            divFrame.Visible = true;
            searchSection.Attributes.Remove("style");
            searchSection.Attributes.Add("style", "margin-top:-7% ; margin-right:-12%; position:absolute; width:100%; margin-bottom:10%");
            patientId = patientIdOwned.Property("data").Value.ToString();
            img_noData.Visible = false;
            no_patient_data.Visible = false;
            string url = "~/Form/PatientHistory.aspx?PatientId=" + patientId + "&OrganizationId=" + Helper.organizationId;

            //Session["urlFrame"] = url;

            myIframe.Src = url;
        }
        updateBIG.Update();
    }
}