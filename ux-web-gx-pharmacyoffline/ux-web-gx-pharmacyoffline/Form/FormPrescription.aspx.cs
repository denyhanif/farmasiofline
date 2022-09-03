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
using System.Globalization;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.UI.HtmlControls;
using log4net;
using System.Text.RegularExpressions;

public partial class Form_FormPrescription : System.Web.UI.Page
{
    HiddenField organizationId;
    HiddenField patientId;
    HiddenField encounterId;
    HiddenField admissionid;
    LinkButton patientName;
    Label localmrno;
    Label doctorname;

    protected static readonly ILog log = LogManager.GetLogger(typeof(Form_FormPrescription));
       
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
            DateTime today = System.DateTime.Now;
            txtDateFromNew.Text = today.ToString("dd MMM yyyy");
            txtToDateNew.Text = today.ToString("dd MMM yyyy");

            HyperLink test = Master.FindControl("ResultLink") as HyperLink;
            test.Style.Add("background-color", "#D6DBFF");
            test.Style.Add("color", "#000000 !important");
        }

        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "lbpatientname", "expandlinkbutton();", true);
    }
    

    protected void btnSearchWorklist_OnCLick(object sender, EventArgs e)
    {
        try
        {
            List<WorklistPharmacy> listworklist = new List<WorklistPharmacy>();
            var worklist = clsPrescription.getSearchWorklist(Int64.Parse(Session["listOrganization"].ToString()), DateTime.Parse(txtDateFromNew.Text), DateTime.Parse(txtToDateNew.Text), txtSearch.Text);
            var Jsonworklist = JsonConvert.DeserializeObject<ResultWorklistPharmacy>(worklist.Result.ToString());
            listworklist = Jsonworklist.list;
            DataTable dtworklist = Helper.ToDataTable(listworklist);
            gvw_worklist.DataSource = dtworklist;
            gvw_worklist.DataBind();

            if (gvw_worklist.Rows.Count == 0)
            {
                imageSection.Visible = true;
            }
            else
            {
                imageSection.Visible = false;
            }
        }
        catch (Exception ex)
        {

        }
    }

    protected void btnPatientName_Click(object sender, EventArgs e)
    {
        int selRowIndex = Convert.ToInt16(txtencounterid.Text);

        organizationId = (HiddenField)gvw_worklist.Rows[selRowIndex].FindControl("onContentOrganizationId");
        patientId = (HiddenField)gvw_worklist.Rows[selRowIndex].FindControl("onContentPatientId");
        encounterId = (HiddenField)gvw_worklist.Rows[selRowIndex].FindControl("onContentEncounterId");
        admissionid = (HiddenField)gvw_worklist.Rows[selRowIndex].FindControl("onContentAdmissionid");
        localmrno = (Label)gvw_worklist.Rows[selRowIndex].FindControl("lblLocalMrNo");
        doctorname = (Label)gvw_worklist.Rows[selRowIndex].FindControl("lblDoctorName");
        patientName = (LinkButton)gvw_worklist.Rows[selRowIndex].FindControl("btnPatientName");

        var s = localmrno.Text;        

        var list = Enumerable
            .Range(0, s.Length / 2)
            .Select(i => s.Substring(i * 2, 2))
            .ToList();
        var res = string.Join("-", list);

        lblPatientName.Text = patientName.Text.ToLower();
        lblDoctorName.Text = doctorname.Text;
        lblMrNo.Text = "MR" +" "+ res;

        headerPatientsection.Visible = true;


        //string url = "~/Form/MedicalResume.aspx?OrganizationId=" + Helper.GetOrganization(this) + "&AdmissionId="+ admissionid.Value.ToString() + "&EncounterId="+ encounterId.Value.ToString() + "&PatientId="+ patientId.Value.ToString() + "&PrintBy="+ Session["sessionPharmacistFullName"].ToString();

        //myIframe.Src = url;

        ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), "scrollset", "alo();", true);

        updateBIG.Update();

    }

}