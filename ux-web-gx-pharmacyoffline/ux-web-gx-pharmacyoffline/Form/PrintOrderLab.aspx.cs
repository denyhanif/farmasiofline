using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Form_PrintOrderLab : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
            List<ListLabRad> listworklist = new List<ListLabRad>();

            List<ListLabRad> CitoLab = new List<ListLabRad>();
            List<ListLabRad> ClinicLab = new List<ListLabRad>();
            List<ListLabRad> MicroLab = new List<ListLabRad>();
            List<ListLabRad> AnatomicalLab = new List<ListLabRad>();
            List<ListLabRad> MDCLab = new List<ListLabRad>();
            List<ListLabRad> PanelLab = new List<ListLabRad>();
            List<ListLabRad> Others = new List<ListLabRad>();

            DataTable dtCitoLab = new DataTable();
            DataTable dtClinicLab = new DataTable();
            DataTable dtMicroLab = new DataTable();
            DataTable dtAnatomicalLab = new DataTable();
            DataTable dtMDCLab = new DataTable();
            DataTable dtPanelLab = new DataTable();
            DataTable dtOthers = new DataTable();

            DataTable dtCitoLab_FO = new DataTable();
            DataTable dtClinicLab_FO = new DataTable();
            DataTable dtMicroLab_FO = new DataTable();
            DataTable dtAnatomicalLab_FO = new DataTable();
            DataTable dtMDCLab_FO = new DataTable();
            DataTable dtPanelLab_FO = new DataTable();
            DataTable dtOthers_FO = new DataTable();

            long OrgId = long.Parse(Request.QueryString["OrgID"]);
            long PatientIT = long.Parse(Request.QueryString["PatientID"]);
            long AdmID = long.Parse(Request.QueryString["AdmID"]);
            string isLab = Request.QueryString["IsLabRad"];
            string encId = Request.QueryString["EncID"].ToString();
            int isLabInt = 0;

            if (isLab.ToLower() == "true")
            {
                isLabInt = 1;
            }
            else
            {
                isLabInt = 0;
            }

            var worklist = clsLabRadOrder.getDetailLabRad(OrgId, PatientIT,AdmID,encId,isLabInt);
            var JsonWorklist = JsonConvert.DeserializeObject<ResultListLabRad>(worklist.Result.ToString());

            listworklist = JsonWorklist.list;

            var GetSettings = clsSettings.GetAppSettings(OrgId);
            var theSettings = JsonConvert.DeserializeObject<ResultViewOrganizationSetting>(GetSettings.Result.ToString());
            List<ViewOrganizationSetting> listSettings = new List<ViewOrganizationSetting>();
            listSettings = theSettings.list;

            if (listSettings.Find(y => y.setting_name.ToUpper() == "USE_COVID19").setting_value.ToUpper() == "TRUE")
            {
                string flagCovid = listworklist[0].isCOVID.ToString();
                if (flagCovid.ToLower() == "true")
                {
                    ImageCovid.Visible = true;
                    ImageCovid_FO.Visible = true;
                }
                else if (flagCovid.ToLower() == "false")
                {
                    ImageCovid.Visible = false;
                    ImageCovid_FO.Visible = false;
                }
            }

            foreach (ListLabRad data in listworklist)
            {
                if (data.item_type == "CitoLab")
                {
                    CitoLab.Add(data);
                }
                if (data.item_type == "ClinicLab")
                {
                    ClinicLab.Add(data);
                }
                if (data.item_type == "MicroLab")
                {
                    MicroLab.Add(data);
                }
                if (data.item_type == "PatologiLab")
                {
                    AnatomicalLab.Add(data);
                }
                if (data.item_type == "MDCLab")
                {
                    MDCLab.Add(data);
                }
                if (data.item_type == "PanelLab")
                {
                    PanelLab.Add(data);
                }
                if (data.item_type.ToLower() == "others")
                {
                    Others.Add(data);
                }
            }

            lblPregnant.Text = (from data in listworklist
                                where data.item_type == "PREGNANCY"
                                select data.item_name).FirstOrDefault().ToString().ToLower() == "TRUE".ToLower() ? "Hamil" : "";
            lblPregnant_FO.Text = lblPregnant.Text;

            lblBreastfeed.Text = (from data in listworklist
                                  where data.item_type == "BREASTFEEDING"
                                  select data.item_name).FirstOrDefault().ToString().ToLower() == "TRUE".ToLower() ? "Menyusui" : "";
            lblBreastfeed_FO.Text = lblBreastfeed.Text;

            if (lblPregnant.Text == "" && lblBreastfeed.Text == "")
            {
                lblPregnant.Text = "-";
            }

            if (lblPregnant.Text != "" && lblBreastfeed.Text != "")
            {
                lblPregnant.Text = lblPregnant.Text + ",";
            }

            if (lblPregnant_FO.Text == "" && lblBreastfeed_FO.Text == "")
            {
                lblPregnant_FO.Text = "-";
            }

            if (lblPregnant_FO.Text != "" && lblBreastfeed_FO.Text != "")
            {
                lblPregnant_FO.Text = lblPregnant_FO.Text + ",";
            }

            dtCitoLab = Helper.ToDataTable(CitoLab.Where(x=>x.is_future_order == false).ToList());
            dtClinicLab = Helper.ToDataTable(ClinicLab.Where(x => x.is_future_order == false).ToList());
            dtMicroLab = Helper.ToDataTable(MicroLab.Where(x => x.is_future_order == false).ToList());
            dtAnatomicalLab = Helper.ToDataTable(AnatomicalLab.Where(x => x.is_future_order == false).ToList());
            dtMDCLab = Helper.ToDataTable(MDCLab.Where(x => x.is_future_order == false).ToList());
            dtPanelLab = Helper.ToDataTable(PanelLab.Where(x => x.is_future_order == false).ToList());
            dtOthers = Helper.ToDataTable(Others.Where(x => x.is_future_order == false).ToList());


            if (dtCitoLab.Rows.Count != 0)
            {
                lblNoCito.Visible = false;
            }

            if (dtClinicLab.Rows.Count != 0)
            {
                lblNoClinicalPathology.Visible = false;
            }

            if (dtMicroLab.Rows.Count != 0)
            {
                lblNoMicroB.Visible = false;
            }

            if (dtAnatomicalLab.Rows.Count != 0)
            {
                lblNoAnatomicalPathology.Visible = false;
            }

            if (dtMDCLab.Rows.Count != 0)
            {
                lblNoMDC.Visible = false;
            }

            if (dtPanelLab.Rows.Count != 0)
            {
                lblNoPanel.Visible = false;
            }

            if (dtOthers.Rows.Count != 0)
            {
                lblWarn.Visible = true;
                lblNoOthers.Visible = false;
            }

            repeatCito.DataSource = dtCitoLab;
            repeatCito.DataBind();

            repeatClinical.DataSource = dtClinicLab;
            repeatClinical.DataBind();

            repeatMicrobiology.DataSource = dtMicroLab;
            repeatMicrobiology.DataBind();

            repeatAnatomical.DataSource = dtAnatomicalLab;
            repeatAnatomical.DataBind();

            repeatMDC.DataSource = dtMDCLab;
            repeatMDC.DataBind();

            repeatPanel.DataSource = dtPanelLab;
            repeatPanel.DataBind();

            repeatOthers.DataSource = dtOthers;
            repeatOthers.DataBind();

            List<string> listLabString = new List<string> { "ClinicLab", "MicroLab", "CitoLab", "PanelLab", "PatologiLab", "Others", "MDCLab" };

            var ClinicalDiagnosis = (from a in listworklist
                                 where listLabString.Contains(a.item_type) && a.is_future_order == false
                                     select (a.ClinicalDiagnosis != null ? a.ClinicalDiagnosis.ToString() : "-"));

            lblClinicalDiagnosis.Text = ClinicalDiagnosis.Count() == 0 ? "-" : ClinicalDiagnosis.First();

            lblNamaDokter.Text = listworklist[0].DoctorName;

            var COD = (from a in listworklist
                          where listLabString.Contains(a.item_type) && a.is_future_order == false
                          select (a.OrderDate != null ? a.OrderDate.ToString() : "-"));
            lblCreatedOrderDate.Text = COD.Count() == 0 ? "-" : COD.First();


            //FUTURE ORDER
            dtCitoLab_FO = Helper.ToDataTable(CitoLab.Where(x => x.is_future_order == true).ToList());
            dtClinicLab_FO = Helper.ToDataTable(ClinicLab.Where(x => x.is_future_order == true).ToList());
            dtMicroLab_FO = Helper.ToDataTable(MicroLab.Where(x => x.is_future_order == true).ToList());
            dtAnatomicalLab_FO = Helper.ToDataTable(AnatomicalLab.Where(x => x.is_future_order == true).ToList());
            dtMDCLab_FO = Helper.ToDataTable(MDCLab.Where(x => x.is_future_order == true).ToList());
            dtPanelLab_FO = Helper.ToDataTable(PanelLab.Where(x => x.is_future_order == true).ToList());
            dtOthers_FO = Helper.ToDataTable(Others.Where(x => x.is_future_order == true).ToList());


            if (dtCitoLab_FO.Rows.Count != 0)
            {
                lblNoCito_FO.Visible = false;
            }

            if (dtClinicLab_FO.Rows.Count != 0)
            {
                lblNoClinicalPathology_FO.Visible = false;
            }

            if (dtMicroLab_FO.Rows.Count != 0)
            {
                lblNoMicroB_FO.Visible = false;
            }

            if (dtAnatomicalLab_FO.Rows.Count != 0)
            {
                lblNoAnatomicalPathology_FO.Visible = false;
            }

            if (dtMDCLab_FO.Rows.Count != 0)
            {
                lblNoMDC_FO.Visible = false;
            }

            if (dtPanelLab_FO.Rows.Count != 0)
            {
                lblNoPanel_FO.Visible = false;
            }

            if (dtOthers_FO.Rows.Count != 0)
            {
                lblWarn_FO.Visible = true;
                lblNoOthers_FO.Visible = false;
            }

            repeatCito_FO.DataSource = dtCitoLab_FO;
            repeatCito_FO.DataBind();

            repeatClinical_FO.DataSource = dtClinicLab_FO;
            repeatClinical_FO.DataBind();

            repeatMicrobiology_FO.DataSource = dtMicroLab_FO;
            repeatMicrobiology_FO.DataBind();

            repeatAnatomical_FO.DataSource = dtAnatomicalLab_FO;
            repeatAnatomical_FO.DataBind();

            repeatMDC_FO.DataSource = dtMDCLab_FO;
            repeatMDC_FO.DataBind();

            repeatPanel_FO.DataSource = dtPanelLab_FO;
            repeatPanel_FO.DataBind();

            repeatOthers_FO.DataSource = dtOthers_FO;
            repeatOthers_FO.DataBind();

            List<string> listLabString_FO = new List<string> { "ClinicLab", "MicroLab", "CitoLab", "PanelLab", "PatologiLab", "Others", "MDCLab" };

            var ClinicalDiagnosis_FO = (from a in listworklist
                                 where listLabString_FO.Contains(a.item_type) && a.is_future_order == true
                                        select (a.ClinicalDiagnosis != null ? a.ClinicalDiagnosis.ToString() : "-"));

            lblClinicalDiagnosis_FO.Text = ClinicalDiagnosis_FO.Count() == 0 ? "-" : ClinicalDiagnosis_FO.First();

            lblNamaDokter_FO.Text = listworklist[0].DoctorName;

            var COD_FO = (from a in listworklist
                       where listLabString_FO.Contains(a.item_type) && a.is_future_order == true
                       select (a.future_order_date != null ? a.future_order_date.ToString("dd MMM yyyy") : "-"));
            lblCreatedOrderDate_FO.Text = COD_FO.Count() == 0 ? "-" : COD_FO.First();


            var dataLab = listworklist.Where(x => x.is_future_order == false && listLabString_FO.Contains(x.item_type)).ToList();
            var dataLabFO = listworklist.Where(x => x.is_future_order == true && listLabString_FO.Contains(x.item_type)).ToList();
            if (dataLab.Count == 0)
            {
                div_lab.Visible = false;
                div_break.Visible = false;
            }
            if (dataLabFO.Count == 0)
            {
                div_lab_fo.Visible = false;
                div_break.Visible = false;
            }

        }
    }
}