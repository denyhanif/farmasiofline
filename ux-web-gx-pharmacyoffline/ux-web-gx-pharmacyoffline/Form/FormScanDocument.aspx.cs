using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Form_FormScanDocument : System.Web.UI.Page
{
    HiddenField patientId;
    HiddenField admissionid;
    Label admissionDate;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            DateTime today = System.DateTime.Now;
            txtDateFromNew.Text = today.ToString("dd MMM yyyy");
            txtToDateNew.Text = today.ToString("dd MMM yyyy");


            HyperLink test = Master.FindControl("HyperLink7") as HyperLink;
            test.Style.Add("background-color", "#D6DBFF");
            test.Style.Add("color", "#000000 !important");
        }

        //var localIPAdress = "";

        //localIPAdress = GetLocalIPAddress();

        //string url = "http://" + localIPAdress + "/scan";
        //myIframe.Src = url;
        //updateBIG.Update();
    }

    protected void btnDataAdmission_OnCLick(object sender, EventArgs e)
    {
        int selRowIndex = ((GridViewRow)(((LinkButton)sender).Parent.Parent)).RowIndex;
        Int64 orgId = Helper.organizationId;
        patientId = (HiddenField)gvw_worklist.Rows[selRowIndex].FindControl("hdnPatientId");
        admissionid = (HiddenField)gvw_worklist.Rows[selRowIndex].FindControl("hdnAdmissionid");
        admissionDate = (Label)gvw_worklist.Rows[selRowIndex].FindControl("lblAdmissionDate");
        string dateConverted = DateTime.Parse(admissionDate.Text).ToString("yyyy-MM-dd");

        //var localIPAdress = GetLocalIPAddress();
        var localIPAdress = "10.83.254.38"; //hardcode for testing purpose

        string url = "http://" + localIPAdress + "/scan?admID=" + admissionid.Value.ToString() + "&orgID=" + Helper.organizationId.ToString() + "&user=" + "Farmasi Offline" + "&admDate=" + dateConverted + "&pID=" + patientId.Value.ToString();
        myIframe.Src = url;

    }

        protected void btnSearchWorklist_OnCLick(object sender, EventArgs e)
    {

        if (txtSearch.Text != "")
        {
            imageSection.Visible = false;
            ViewAdmissionMR listworklistScanMR = new ViewAdmissionMR();

            var getAdmission = clsUpload.GetAdmission(txtSearch.Text, Helper.organizationId, DateTime.Parse(txtDateFromNew.Text), DateTime.Parse(txtToDateNew.Text));
            var dataAdmission = JsonConvert.DeserializeObject<ResultViewAdmissionMR>(getAdmission.Result.ToString());
            listworklistScanMR = dataAdmission.list;


            if (listworklistScanMR.mrheader != null)
            {
                lblPatientName.Text = listworklistScanMR.mrheader.PatientName;
                lblMRno.Text = txtSearch.Text;
                lblGender.Text = listworklistScanMR.mrheader.Gender;
                lblDOB.Text = listworklistScanMR.mrheader.DOB;

                gvw_worklist.DataSource = null;
                if (listworklistScanMR.mrdetail != null && listworklistScanMR.mrdetail.Count != 0)
                {
                    imageSection.Visible = false;
                    DataTable dtworklistScan = Helper.ToDataTable(listworklistScanMR.mrdetail);
                    gvw_worklist.DataSource = dtworklistScan;
                    gvw_worklist.DataBind();
                }
                else
                {
                    gvw_worklist.DataBind();
                    imageSection.Visible = true;
                }
            }
            else
            {
                gvw_worklist.DataBind();
                imageSection.Visible = true;
            }
        }

        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "popUPTes", "alert('Silahkan isi kolom pencarian')", true);
        }
    }


    protected void btnPatientName_Click(object sender, EventArgs e)
    {

    }


    public static string GetLocalIPAddress()
    {
        var host = Dns.GetHostEntry(Dns.GetHostName());
        foreach (var ip in host.AddressList)
        {
            if (ip.AddressFamily == AddressFamily.InterNetwork)
            {
                return ip.ToString();
            }
        }
        throw new Exception("No network adapters with an IPv4 address in the system!");
    }
}