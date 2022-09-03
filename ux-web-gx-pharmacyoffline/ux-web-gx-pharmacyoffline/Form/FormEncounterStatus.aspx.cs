using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Form_FormEncounterStatus : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

            divFrame.Visible = true;

            HyperLink test = Master.FindControl("HyperLink6") as HyperLink;
            test.Style.Add("background-color", "#D6DBFF");
            test.Style.Add("color", "#000000 !important");

            string url = "~/Form/EncounterStatus.aspx?orgId=2";
            myIframe.Src = url;
            updateBIG.Update();
        }
    }
}