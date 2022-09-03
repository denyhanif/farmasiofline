using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Form_FormWorklistTeleConsult : System.Web.UI.Page
{
    HiddenField organizationId;
    HiddenField patientId;
    HiddenField admId;
    HiddenField encId;
    HiddenField isLabRad;
    HiddenField printBY;
    HiddenField doctorName;
    HiddenField pageSOAP;

    protected void Page_Load(object sender, EventArgs e)
    {
        long OrgId;
        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "dropSearch", " $('.selectpicker').selectpicker();", addScriptTags: true);

        if (!IsPostBack)
        {
            if (Request.QueryString["OrgId"] != null && Request.QueryString["UserId"] != null)
            {
                var registryflag = ConfigurationManager.AppSettings["registryflag"].ToString();

                if (registryflag == "1")
                {
                    ConfigurationManager.AppSettings["urlPharmacy"] = SiloamConfig.Functions.GetValue("urlExtension").ToString();
                    ConfigurationManager.AppSettings["urlPrescription"] = SiloamConfig.Functions.GetValue("urlTransaction").ToString();
                    ConfigurationManager.AppSettings["urlFunctional"] = SiloamConfig.Functions.GetValue("urlMaster").ToString();
                    ConfigurationManager.AppSettings["urlRecord"] = SiloamConfig.Functions.GetValue("urlPharmacy").ToString();
                    ConfigurationManager.AppSettings["urlMaster"] = SiloamConfig.Functions.GetValue("urlMaster").ToString();
                    ConfigurationManager.AppSettings["urlHISDataCollection"] = SiloamConfig.Functions.GetValue("urlHISDataCollection").ToString();
                    ConfigurationManager.AppSettings["urlUserManagement"] = SiloamConfig.Functions.GetValue("urlUserManagement").ToString();
                    ConfigurationManager.AppSettings["DB_Emr"] = SiloamConfig.Functions.GetValue("DB_Emr").ToString();
                    ConfigurationManager.AppSettings["DB_EMRTransaction"] = SiloamConfig.Functions.GetValue("DB_EMRTransaction").ToString();
                    ConfigurationManager.AppSettings["urlIntegration"] = SiloamConfig.Functions.GetValue("urlIntegration").ToString();
                    ConfigurationManager.AppSettings["DB_Integration"] = SiloamConfig.Functions.GetValue("DB_Integration").ToString();
                }
                TextSearchDate.Attributes.Add("ReadOnly", "ReadOnly");
                TextSearchDate.Text = DateTime.Now.ToString("dd MMM yyyy");

                OrgId = long.Parse(Request.QueryString["OrgId"]);
                hfOrgId.Value = OrgId.ToString();
                hfUserId.Value = Request.QueryString["UserId"].ToString();
                getWorklist(OrgId, DateTime.Now, "");

                LabelPtnName.Attributes.Add("ReadOnly", "ReadOnly");
                LabelMrno.Attributes.Add("ReadOnly", "ReadOnly");
                LabelDocName.Attributes.Add("ReadOnly", "ReadOnly");
                lblNextDoctor.Attributes.Add("ReadOnly", "ReadOnly");
                txtDate.Attributes.Add("ReadOnly", "ReadOnly");
                lblPhone.Attributes.Add("ReadOnly", "ReadOnly");
                lblOthers.Attributes.Add("ReadOnly", "ReadOnly");

                this.rdImmunoComp.Enabled = false;
                //this.rdImmunoComp.Attributes.Remove("onClick");

                this.rdInfectious.Enabled = false;
                //this.rdInfectious.Attributes.Remove("onClick");

                DataTable dt = clsWorklistTeleConsult.getDoctorByOrganization(OrgId);
                ddlDoctor.DataSource = null;
                ddlDoctor.DataSource = dt;
                ddlDoctor.DataValueField = "Id";
                ddlDoctor.DataTextField = "Name";
                ddlDoctor.DataBind();

            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, GetType(), "notif1", "alert('Parameter Tidak Lengkap!')", true);
            }
        }
    }

    public void getWorklist(long OrgId, DateTime SearchDate, string MrNo)
    {
        DataTable dataworklist = new DataTable();
        dataworklist = clsWorklistTeleConsult.GetWorklistTeleConsult(OrgId, SearchDate, MrNo);

        gvw_worklist_TC.DataSource = null;
        DataRow[] dataFilter = dataworklist.Select("isNextAppointment = 1 AND Exclude = 0");
        if (dataFilter.Length > 0)
        {
            gvw_worklist_TC.DataSource = dataFilter.CopyToDataTable();
        }
        gvw_worklist_TC.DataBind();

        gvExclude.DataSource = null;
        DataRow[] dataFilter1 = dataworklist.Select("isNextAppointment = 1 AND Exclude = 1");
        if (dataFilter1.Length > 0)
        {
            gvExclude.DataSource = dataFilter1.CopyToDataTable();
        }
        gvExclude.DataBind();

        ddlFinish.SelectedValue = "-1";
        DDL_AppointType.SelectedValue = "1";

        Session[Helper.SessionListTeleConsult] = dataworklist;
    }

    protected void ButtonSearch_Click(object sender, EventArgs e)
    {
        long OrgId = long.Parse(Request.QueryString["OrgId"]);
        getWorklist(OrgId, DateTime.Parse(TextSearchDate.Text), TextSearch.Text);
    }

    protected void DDL_AppointType_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            gvw_worklist_TC.DataSource = null;
            gvExclude.DataSource = null;

            DataRow[] dataFilter;
            DataRow[] dataFilter1;

            //dipakai sementara
            long OrgId = long.Parse(Request.QueryString["OrgId"]);
            DataTable dataworklist = new DataTable();
            dataworklist = clsWorklistTeleConsult.GetWorklistTeleConsult(OrgId, DateTime.Parse(TextSearchDate.Text), TextSearch.Text);

            if (DDL_AppointType.SelectedValue == "-1" && ddlFinish.SelectedValue == "-1")
            {
                //dataFilter = ((DataTable)Session[Helper.SessionListTeleConsult]).Select("Exclude = 0");
                //dataFilter1 = ((DataTable)Session[Helper.SessionListTeleConsult]).Select("Exclude = 1");
                dataFilter = dataworklist.Select("Exclude = 0");
                dataFilter1 = dataworklist.Select("Exclude = 1");

                if (dataFilter.Length > 0)
                {
                    gvw_worklist_TC.DataSource = dataFilter.CopyToDataTable();
                }

                if (dataFilter1.Length > 0)
                {
                    gvExclude.DataSource = dataFilter1.CopyToDataTable();
                }
            }
            else if (DDL_AppointType.SelectedValue == "-1" && ddlFinish.SelectedValue != "-1")
            {
                //dataFilter = ((DataTable)Session[Helper.SessionListTeleConsult]).Select("Exclude = 0 AND is_send_email = '" + ddlFinish.SelectedValue + "'");
                //dataFilter1 = ((DataTable)Session[Helper.SessionListTeleConsult]).Select("Exclude = 1 AND is_send_email = '" + ddlFinish.SelectedValue + "'");
                dataFilter = dataworklist.Select("Exclude = 0 AND is_send_email = '" + ddlFinish.SelectedValue + "'");
                dataFilter1 = dataworklist.Select("Exclude = 1 AND is_send_email = '" + ddlFinish.SelectedValue + "'");

                if (dataFilter.Length > 0)
                {
                    gvw_worklist_TC.DataSource = dataFilter.CopyToDataTable();
                }

                if (dataFilter1.Length > 0)
                {
                    gvExclude.DataSource = dataFilter1.CopyToDataTable();
                }
            }
            else if (DDL_AppointType.SelectedValue != "-1" && ddlFinish.SelectedValue == "-1")
            {
                //dataFilter = ((DataTable)Session[Helper.SessionListTeleConsult]).Select("Exclude = 0 AND isNextAppointment = " + DDL_AppointType.SelectedValue);
                //dataFilter1 = ((DataTable)Session[Helper.SessionListTeleConsult]).Select("Exclude = 1 AND isNextAppointment = " + DDL_AppointType.SelectedValue);
                dataFilter = dataworklist.Select("Exclude = 0 AND isNextAppointment = " + DDL_AppointType.SelectedValue);
                dataFilter1 = dataworklist.Select("Exclude = 1 AND isNextAppointment = " + DDL_AppointType.SelectedValue);

                if (dataFilter.Length > 0)
                {
                    gvw_worklist_TC.DataSource = dataFilter.CopyToDataTable();
                }

                if (dataFilter1.Length > 0)
                {
                    gvExclude.DataSource = dataFilter1.CopyToDataTable();
                }
            }
            else if (DDL_AppointType.SelectedValue != "-1" && ddlFinish.SelectedValue != "-1")
            {
                //dataFilter = ((DataTable)Session[Helper.SessionListTeleConsult]).Select("Exclude = 0 AND isNextAppointment = " + DDL_AppointType.SelectedValue + "AND is_send_email = " + ddlFinish.SelectedValue + "");
                //dataFilter1 = ((DataTable)Session[Helper.SessionListTeleConsult]).Select("Exclude = 1 AND isNextAppointment = " + DDL_AppointType.SelectedValue + "AND is_send_email = " + ddlFinish.SelectedValue + "");
                dataFilter = dataworklist.Select("Exclude = 0 AND isNextAppointment = " + DDL_AppointType.SelectedValue + "AND is_send_email = " + ddlFinish.SelectedValue + "");
                dataFilter1 = dataworklist.Select("Exclude = 1 AND isNextAppointment = " + DDL_AppointType.SelectedValue + "AND is_send_email = " + ddlFinish.SelectedValue + "");

                if (dataFilter.Length > 0)
                {
                    gvw_worklist_TC.DataSource = dataFilter.CopyToDataTable();
                }

                if (dataFilter1.Length > 0)
                {
                    gvExclude.DataSource = dataFilter1.CopyToDataTable();
                }

            }

            gvw_worklist_TC.DataBind();
            gvExclude.DataBind();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    protected void ButtonSendEmail_Click(object sender, EventArgs e)
    {
        try
        {
            if (HF_rdiid.Value != "")
            {
                btnSave.Enabled = true;
            }
            else if (HF_rdiid.Value == "")
            {
                btnSave.Enabled = false;
            }

            DataSet ds = clsWorklistTeleConsult.getPatientDetailNurse(long.Parse(HF_orgid.Value), long.Parse(HF_admid.Value), long.Parse(HF_ptnid.Value), Guid.Parse(HF_encid.Value));

            repeatRad.DataSource = null;
            repeatLab.DataSource = null;
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Select("template_name = 'LAB'").Count() > 0)
                {
                    DataRow[] datalab = ds.Tables[0].Select("template_name = 'LAB'");
                    if (datalab.Length > 0)
                    {
                        repeatLab.DataSource = datalab.CopyToDataTable();
                    }
                }
                if (ds.Tables[0].Select("template_name = 'RAD'").Count() > 0)
                {
                    DataRow[] datarad = ds.Tables[0].Select("template_name = 'RAD'");
                    if (datarad.Length > 0)
                    {
                        repeatRad.DataSource = datarad.CopyToDataTable();
                    }
                }
            }
            repeatLab.DataBind();
            repeatRad.DataBind();

            gvAlkes.DataSource = null;
            gvvDrug.DataSource = null;
            if (ds.Tables[1].Rows.Count > 0)
            {
                if (ds.Tables[1].Select("is_consumables = 0").Count() > 0)
                {
                    DataRow[] datadrug = ds.Tables[1].Select("is_consumables = 0");
                    if (datadrug.Length > 0)
                    {
                        gvvDrug.DataSource = datadrug.CopyToDataTable();
                    }
                }
                if (ds.Tables[1].Select("is_consumables = 1").Count() > 0)
                {
                    DataRow[] datacons = ds.Tables[1].Select("is_consumables = 1");
                    if (datacons.Length > 0)
                    {
                        gvAlkes.DataSource = datacons.CopyToDataTable();
                    }
                }
            }
            gvvDrug.DataBind();
            gvAlkes.DataBind();

            if (ds.Tables[2].Rows.Count > 0)
            {
                lblOthers.Text = ds.Tables[2].Rows[0]["planning_remarks"].ToString();
            }
            else
            {
                lblOthers.Text = "";
            }

            if (ds.Tables[3].Rows.Count > 0)
            {
                rdInfectious.SelectedValue = ds.Tables[3].Rows[0]["isInfectious"].ToString();
                rdImmunoComp.SelectedValue = ds.Tables[3].Rows[0]["isImmunoCompromised"].ToString();
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }

    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (ddlDoctor.SelectedValue != "0")
        {
            if (HF_rdiid.Value != "")
            {
                try
                {
                    string time = txtTime.Text.ToString();

                    var isValid = IsValidTimeFormat(time);

                    if (isValid == true)
                    {
                        string urlAddress = Server.MapPath("~/Template/DoctorReferenceLetter.html");

                        string body = File.ReadAllText(urlAddress);

                        string date = txtDate.Text.ToString();

                        DateTime newDateTime = Convert.ToDateTime(date).Add(TimeSpan.Parse(time));
                        long orgid = long.Parse(HF_orgid.Value.ToString());
                        long admid = long.Parse(HF_admid.Value.ToString());
                        long ptid = long.Parse(HF_ptnid.Value.ToString());
                        Guid encid = Guid.Parse(HF_encid.Value.ToString());
                        long rdi = long.Parse(HF_rdiid.Value.ToString());
                        long ri = long.Parse(ddlDoctor.SelectedValue.ToString());
                        long usr = long.Parse(hfUserId.Value.ToString());

                        clsWorklistTeleConsult.UpdateReferenceDoctor(orgid, admid, ptid, encid, rdi, newDateTime,
                            2, ri, usr, txtEmail.Text.ToString(),
                            txtRemarks.Text.ToString(), body.ToString());

                        ScriptManager.RegisterStartupScript(Page, GetType(), "notifsubmit", "showSuccess();", true);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(Page, GetType(), "notif2", "alert('Time is not valid!')", true);
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            else
            {
                btnSave.Enabled = false;
                ScriptManager.RegisterStartupScript(Page, GetType(), "notif2", "alert('Cannot Save NON APPOINTMENT')", true);
            }
        }
        else
        {
            ScriptManager.RegisterStartupScript(Page, GetType(), "notif2", "alert('Silahkan Pilih Dokter')", true);
        }
    }

    public bool IsValidTimeFormat(string input)
    {
        TimeSpan dummyOutput;
        return TimeSpan.TryParse(input, out dummyOutput);
    }
}