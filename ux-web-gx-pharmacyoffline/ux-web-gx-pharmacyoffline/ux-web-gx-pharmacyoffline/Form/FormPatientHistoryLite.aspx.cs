using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Form_FormPatientHistoryLite : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        hideHeader();
        if (!IsPostBack)
        {
            HyperLink test = Master.FindControl("HyperLink2") as HyperLink;
            test.Style.Add("background-color", "#D6DBFF");
            test.Style.Add("color", "#000000 !important");
        }
    }

    protected void hideHeader()
    {
        lblNama.Visible = false;
        lblDOB.Visible = false;
        lblDOBJudul.Visible = false;
        lblAge.Visible = false;
        lblAgeJudul.Visible = false;
        lblReligion.Visible = false;
        lblReligionJudul.Visible = false;
    }

    protected void showHeader()
    {
        lblNama.Visible = true;
        lblDOB.Visible = true;
        lblDOBJudul.Visible = true;
        lblAge.Visible = true;
        lblAgeJudul.Visible = true;
        lblReligion.Visible = true;
        lblReligionJudul.Visible = true;
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        string patientId = "";

        var dataPatientId = clsPatientHistory.getPatientData(txtMRno.Text, Helper.organizationId);
        var patientIdOwned = JsonConvert.DeserializeObject<resultPatientData>(dataPatientId.Result.ToString());
        var Response = (JObject)JsonConvert.DeserializeObject<dynamic>(dataPatientId.Result);
        var Status = Response.Property("status").Value.ToString();

        lblNama.Text = patientIdOwned.list.PatientName;
        lblDOB.Text = patientIdOwned.list.BirthDate;
        lblAge.Text = patientIdOwned.list.Age;
        lblReligion.Text = patientIdOwned.list.ReligionName;


        showHeader();


        if (Status == "Fail")
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
            patientId = patientIdOwned.list.PatientId.ToString();
            img_noData.Visible = false;
            no_patient_data.Visible = false;
            string url = "~/Form/PharmacyPatientHistory.aspx?OrganizationId=2&PatientId=" + patientId;

            //Session["urlFrame"] = url;

            myIframe.Src = url;
        }
        showHeader();
        updateBIG.Update();
    }
}