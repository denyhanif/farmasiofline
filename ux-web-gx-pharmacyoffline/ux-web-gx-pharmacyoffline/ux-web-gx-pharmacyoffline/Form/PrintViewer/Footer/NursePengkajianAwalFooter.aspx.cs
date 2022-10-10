using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Form_PrintViewer_Footer_NursePengkajianAwalFooter : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try
            {
                long OrgId = long.Parse(Request.QueryString["OrgId"]);

                var GetSettings = clsSettings.GetAppSettings(OrgId);
                var theSettings = JsonConvert.DeserializeObject<ResultViewOrganizationSetting>(GetSettings.Result.ToString());

                List<ViewOrganizationSetting> listSettings = new List<ViewOrganizationSetting>();
                listSettings = theSettings.list;

                int jam = 0;
                if (listSettings.Count != 0)
                {

                    string settingValue = (from a in listSettings
                                           where a.setting_name == "ADD_HOUR"
                                           select a.setting_value
                                           ).Single();

                    jam = int.Parse(settingValue);
                }

                lblPrintDate.Text = DateTime.Now.AddHours(jam).ToString("dd MMM yyyy HH:mm");
                lblPrintedBy.Text = Request.QueryString["PrintBy"].ToString();
            }
            catch (Exception x)
            {
                lblPrintDate.Text = DateTime.Now.ToString("dd MMM yyyy HH:mm") + ".";
                lblPrintedBy.Text = Request.QueryString["PrintBy"].ToString();
            }
        }
    }
}