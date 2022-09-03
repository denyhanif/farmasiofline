using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Form_PrintFooterPernyataan : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            lblPrintDate.Text = DateTime.Now.ToString("dd MMM yyyy HH:mm");
            lblPrintedBy.Text = Request.QueryString["PrintBy"].ToString();
            lblNamaDokter.Text = Request.QueryString["DoctorBy"].ToString();
        }
    }
}