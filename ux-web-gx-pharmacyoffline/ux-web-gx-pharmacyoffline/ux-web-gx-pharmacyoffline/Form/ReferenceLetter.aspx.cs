using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net.Mail;
using System.Net;
using System.Data.SqlClient;
using System.Configuration;

public partial class Form_ReferenceLetter : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "dropSearch", " $('.selectpicker').selectpicker();", addScriptTags: true);

        if (!IsPostBack)
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

            if (Request.QueryString["organization_id"] != null && Request.QueryString["encounter_id"] != null &&
                Request.QueryString["admission_id"] != null && Request.QueryString["patient_id"] != null &&
                Request.QueryString["user_id"] != null)
            {
                long OrganizationId = long.Parse(Request.QueryString["organization_id"]);
                long AdmissionId = long.Parse(Request.QueryString["admission_id"]);
                long PatientId = long.Parse(Request.QueryString["patient_id"]);
                Guid EncounterId = Guid.Parse(Request.QueryString["encounter_id"]);
                long UserId = long.Parse(Request.QueryString["user_id"]);

                //long OrganizationId = 2;
                //long AdmissionId = 2000005961240;
                //long PatientId = 2000001996192;
                //Guid EncounterId = Guid.Parse("25C4107F-2509-4F57-A1BA-F4640257AC2E");
                //long UserId = 123;

                hfOrganizationId.Value = OrganizationId.ToString();
                hfAdmissionId.Value = AdmissionId.ToString();
                hfPatientId.Value = PatientId.ToString();
                hfEncounterId.Value = EncounterId.ToString();
                hfUserId.Value = UserId.ToString();

                DataSet dataSet = clsReferenceLetter.getPatientData(OrganizationId, AdmissionId, PatientId, EncounterId);

                if (dataSet.Tables[0].Rows.Count > 0)
                {
                    lblMrNo.Text = dataSet.Tables[0].Rows[0]["LocalMrNo"].ToString();
                    lblAdmissionNo.Text = dataSet.Tables[0].Rows[0]["AdmissionNo"].ToString();
                    lblPatientName.Text = dataSet.Tables[0].Rows[0]["PatientName"].ToString();
                    lblBirthDate.Text = DateTime.Parse(dataSet.Tables[0].Rows[0]["BirthDate"].ToString()).ToString("dd-MM-yyyy");
                    lblChiefComplaintAnamnesis.Text = dataSet.Tables[0].Rows[0]["CF"].ToString() + " - " +
                        dataSet.Tables[0].Rows[0]["Anamnesis"].ToString();
                    lblAssessment.Text = dataSet.Tables[0].Rows[0]["Diagnosa"].ToString();
                    lblOthers.Text = dataSet.Tables[0].Rows[0]["Others"].ToString();
                }

                if (dataSet.Tables[1].Rows.Count > 0)
                {
                    DataTable dtLabRad = dataSet.Tables[1];

                    repeatLab.DataSource = null;
                    if (dtLabRad.Select("template_name = 'LAB'").Count() > 0)
                    {
                        repeatLab.DataSource = dtLabRad.Select("template_name = 'LAB'").CopyToDataTable();
                    }
                    repeatLab.DataBind();

                    repeatRad.DataSource = null;
                    if (dtLabRad.Select("template_name = 'RAD'").Count() > 0)
                    {
                        repeatRad.DataSource = dtLabRad.Select("template_name = 'RAD'").CopyToDataTable();
                    }
                    repeatRad.DataBind();
                }

                if (dataSet.Tables[2].Rows.Count > 0)
                {
                    ddlDoctor.DataSource = null;
                    ddlDoctor.DataSource = dataSet.Tables[2];
                    ddlDoctor.DataValueField = "Id";
                    ddlDoctor.DataTextField = "Name";
                    ddlDoctor.DataBind();
                }

                if (dataSet.Tables[3].Rows.Count > 0)
                {
                    ddlSpesialis.DataSource = null;
                    ddlSpesialis.DataSource = dataSet.Tables[3];
                    ddlSpesialis.DataValueField = "Id";
                    ddlSpesialis.DataTextField = "Name";
                    ddlSpesialis.DataBind();
                }
               
            }
            else
            {
                //string close = @"<script type='text/javascript'>
                //                window.returnValue = true;
                //                window.close();
                //                </script>";
                //base.Response.Write(close);
                ScriptManager.RegisterStartupScript(Page, GetType(), "notif1", "alert('Parameter Tidak Lengkap!')", true);
                
            }
        }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            if (rdDoctor.Checked == true || rdSpesialis.Checked == true)
            {
                short reference_type_id = 0;
                long reference_id = 0;
                if (rdDoctor.Checked == true)
                {
                    reference_type_id = 2;
                    reference_id = long.Parse(ddlDoctor.SelectedValue);
                }
                else if (rdSpesialis.Checked == true)
                {
                    reference_type_id = 3;
                    reference_id = long.Parse(ddlSpesialis.SelectedValue);
                }

                short kondisiinfeksius = 0;
                short kondisiimmun = 0;
                if (RBinfeksiusYes.Checked == true)
                {
                    kondisiinfeksius = 1;
                }
                if (RBimmunoYes.Checked == true)
                {
                    kondisiimmun = 1;
                }

                long OrganizationId = long.Parse(hfOrganizationId.Value);
                long AdmissionId = long.Parse(hfAdmissionId.Value);
                long PatientId = long.Parse(hfPatientId.Value);
                Guid EncounterId = Guid.Parse(hfEncounterId.Value);
                long UserId = long.Parse(hfUserId.Value);

                clsReferenceLetter.InsertReference(OrganizationId, AdmissionId, PatientId, EncounterId, reference_type_id, reference_id, UserId, txtRemarks.Text.ToString(), kondisiinfeksius, kondisiimmun);

                ScriptManager.RegisterStartupScript(Page, GetType(), "notifsubmit", "showSuccess();", true);
            }
            else
            {
                // error no checked
                ScriptManager.RegisterStartupScript(Page, GetType(), "notifsubmit", "alert('Silakan Pilih Dokter atau Spesialis yang dirujuk!')", true);
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }

    }

    protected void rdDoctor_CheckedChanged(object sender, EventArgs e)
    {
        if (rdDoctor.Checked == true)
        {
            rdSpesialis.Checked = false;
            ddlDoctor.Visible = true;
            ddlSpesialis.SelectedIndex = 0;
            ddlSpesialis.Visible = false;
        }
    }

    protected void rdSpesialis_CheckedChanged(object sender, EventArgs e)
    {
        if (rdSpesialis.Checked == true)
        {
            rdDoctor.Checked = false;
            ddlSpesialis.Visible = true;
            ddlDoctor.SelectedIndex = 0;
            ddlDoctor.Visible = false;
        }
    }
}