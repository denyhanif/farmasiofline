using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Form_PrintAll : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            DataSet result = clsPatientHistory.getAllForPrint(long.Parse(Request.QueryString["OrgID"]), long.Parse(Request.QueryString["AdmID"]), Guid.Parse(Request.QueryString["EncID"]));

            DataTable headerData = result.Tables[0];
            DataTable contentData = result.Tables[1];

        }
    }
}