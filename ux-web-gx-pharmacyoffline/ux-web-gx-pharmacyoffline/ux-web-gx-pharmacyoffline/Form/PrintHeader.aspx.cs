using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Form_PrintHeader : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
            DataSet result = clsPatientHistory.getAllForPrint(long.Parse(Request.QueryString["OrgID"]), long.Parse(Request.QueryString["AdmID"]), Guid.Parse(Request.QueryString["EncID"]));

            DataTable headerData = result.Tables[0];
            DataTable contentData = result.Tables[1];

            lblNama.Text = headerData.Rows[0].Field<string>("PatientName");
            lblAdmission.Text = headerData.Rows[0].Field<string>("Admission");
            lblDokter.Text = headerData.Rows[0].Field<string>("DoctorName");
            lblMR.Text = headerData.Rows[0].Field<string>("LocalMrNo");
            lblSeks.Text = headerData.Rows[0].Field<string>("Gender");
            lblUmur.Text = headerData.Rows[0].Field<string>("BirthDate");

            if (Request.QueryString["OrgID"] == "31") //RSUSW
            {
                ImgLogoSH.ImageUrl = "~/Images/Icon/logo-RSUSW.jpg";
            }
            else
            {
                ImgLogoSH.ImageUrl = "~/Images/Icon/logo-SH.png";
            }
        }
    }
}