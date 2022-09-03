using log4net;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Form_EncounterStatus : System.Web.UI.Page
{
    DataTable dt = new DataTable();
    protected static readonly ILog log = LogManager.GetLogger(typeof(Form_EncounterStatus));

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ConfigurationManager.AppSettings["urlExtension"] = SiloamConfig.Functions.GetValue("urlExtension").ToString();
            srcStartDate.Text = DateTime.Now.AddMonths(-1).ToString("dd MMM yyyy");
            srcEndDate.Text = DateTime.Now.ToString("dd MMM yyyy");
            orgId.Value = Request.QueryString["orgId"];
            getDataEncounter(orgId.Value, DateTime.Parse(srcStartDate.Text), DateTime.Parse(srcEndDate.Text), srcKey.Text);
        }
    }

    void getDataEncounter(string orgId, DateTime startDate, DateTime endDate, string srcKey)
    {
        try
        {
            log.Info(LogLibrary.Logging("S", "getDataEncounter", srcKey, ""));

            if (srcKey != "")
            {
                log.Debug(LogLibrary.Logging("S", "getEncounterDataFilter", srcKey, ""));
                var data = clsEncounter.getEncounterDataFilter(orgId, startDate, endDate, srcKey);
                var JsonData = JsonConvert.DeserializeObject<ResultReportStatusDoctor>(data.Result.ToString());
                log.Debug(LogLibrary.Logging("E", "getEncounterDataFilter", srcKey, JsonConvert.SerializeObject(JsonData)));

                var listEncounter = JsonData.list.OrderByDescending(x => x.AdmissionDate).ToList();
                Session["ReportStatusDoctor"] = listEncounter;
                dt = Helper.ToDataTable(listEncounter);

                if (gvwEncounter.PageSize < listEncounter.Count)
                    gvwEncounter.PageIndex = 0;
            }
            else
            {
                dt.Columns.Add("LocalMrNo", typeof(string));
                dt.Columns.Add("AdmissionNo", typeof(string));
                dt.Columns.Add("DoctorName", typeof(string));
                dt.Columns.Add("PatientName", typeof(string));
                dt.Columns.Add("AdmissionDate", typeof(DateTime));
                dt.Columns.Add("DoctorStatus", typeof(string));
                dt.Columns.Add("OrderLaboratory", typeof(int));
                dt.Columns.Add("OrderRadiology", typeof(int));
                dt.Columns.Add("DoctorPrescription", typeof(int));
            }
            

            gvwEncounter.DataSource = dt;
            gvwEncounter.DataBind();

            log.Info(LogLibrary.Logging("E", "getDataEncounter", srcKey, ""));
        }
        catch(Exception ex)
        {
            LogLibrary.Error("getDataEncounter", srcKey, ex.Message.ToString());
        }
    }



    protected void btnSearch_Click(object sender, EventArgs e)
    {
        log.Info(LogLibrary.Logging("S", "btnSearch_Click", srcKey.Text, ""));

        getDataEncounter(orgId.Value, DateTime.Parse(srcStartDate.Text), DateTime.Parse(srcEndDate.Text), srcKey.Text);

        log.Info(LogLibrary.Logging("E", "btnSearch_Click", srcKey.Text, ""));
    }

    protected void gvwEncounter_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {

        log.Debug(LogLibrary.Logging("S", "getEncounterDataFilter", srcKey.Text, ""));
        var data = clsEncounter.getEncounterDataFilter(orgId.Value, DateTime.Parse(srcStartDate.Text), DateTime.Parse(srcEndDate.Text), srcKey.Text);
        var JsonData = JsonConvert.DeserializeObject<ResultReportStatusDoctor>(data.Result.ToString());
        log.Debug(LogLibrary.Logging("E", "getEncounterDataFilter", srcKey.Text, JsonConvert.SerializeObject(JsonData)));

        var listEncounter = JsonData.list.OrderByDescending(x => x.AdmissionDate).ToList();

        //List<ReportStatusDoctor> listEncounter = (List<ReportStatusDoctor>)Session["ReportStatusDoctor"];
        dt = new DataTable();
        gvwEncounter.PageIndex = e.NewPageIndex;
        HiddenPageIndex.Value = e.NewPageIndex.ToString();

        dt = Helper.ToDataTable(listEncounter);
        gvwEncounter.DataSource = dt;
        gvwEncounter.DataBind();

    }

    protected void srcStartDate_TextChanged(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "", "checkFilter();", true);
    }

    protected void srcEndDate_TextChanged(object sender, EventArgs e)
    {
        getDataEncounter(orgId.Value, DateTime.Parse(srcStartDate.Text), DateTime.Parse(srcEndDate.Text), srcKey.Text);
    }

    protected void btn_search_Click(object sender, EventArgs e)
    {
        getDataEncounter(orgId.Value, DateTime.Parse(srcStartDate.Text), DateTime.Parse(srcEndDate.Text), srcKey.Text);
    }

    protected void srcKey_TextChanged(object sender, EventArgs e)
    {
        //ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "", "filterData();", true);
        getDataEncounter(orgId.Value, DateTime.Parse(srcStartDate.Text), DateTime.Parse(srcEndDate.Text), srcKey.Text);
    }
}