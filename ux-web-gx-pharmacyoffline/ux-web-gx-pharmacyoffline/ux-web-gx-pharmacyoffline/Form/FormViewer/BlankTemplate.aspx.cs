using log4net;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Form_FormViewer_BlankTemplate : System.Web.UI.Page
{
    protected static readonly ILog log = LogManager.GetLogger(typeof(Form_FormViewer_BlankTemplate));

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //HF_Username.Value = Request.QueryString["Username"];

            //var registryflag = ConfigurationManager.AppSettings["registryflag"].ToString();
            //if (registryflag == "1")
            //{
            //    ConfigurationManager.AppSettings["urlUserManagement"] = SiloamConfig.Functions.GetValue("urlUserManagement").ToString();
            //}
        }
    }
}