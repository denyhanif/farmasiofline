using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Form_PrintOrderRad : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
            List<ListLabRad> listworklist = new List<ListLabRad>();

            List<ListLabRad> CT = new List<ListLabRad>();
            List<ListLabRad> MRI1 = new List<ListLabRad>();
            List<ListLabRad> MRI3 = new List<ListLabRad>();
            List<ListLabRad> Radiology = new List<ListLabRad>();
            List<ListLabRad> USG = new List<ListLabRad>();
            List<ListLabRad> Others = new List<ListLabRad>();

            DataTable dtCT = new DataTable();
            DataTable dtMRI1 = new DataTable();
            DataTable dtMRI3 = new DataTable();
            DataTable dtRadiology = new DataTable();
            DataTable dtUSG = new DataTable();
            DataTable dtOthers = new DataTable();

            DataTable dtCT_FO = new DataTable();
            DataTable dtMRI1_FO = new DataTable();
            DataTable dtMRI3_FO = new DataTable();
            DataTable dtRadiology_FO = new DataTable();
            DataTable dtUSG_FO = new DataTable();
            DataTable dtOthers_FO = new DataTable();

            long OrdId = long.Parse(Request.QueryString["OrgID"]);
            long PatientIT = long.Parse(Request.QueryString["PatientID"]);
            long AdmID = long.Parse(Request.QueryString["AdmID"]);
            string isLab = Request.QueryString["IsLabRad"];
            string encId = Request.QueryString["EncID"].ToString();
            int isLabInt = 0;

            if (isLab.ToLower() == "true")
            {
                isLabInt = 2;
            }
            else
            {
                isLabInt = 0;
            }

            var worklist = clsLabRadOrder.getDetailLabRad(OrdId, PatientIT, AdmID, encId, isLabInt);
            var JsonWorklist = JsonConvert.DeserializeObject<ResultListLabRad>(worklist.Result.ToString());

            listworklist = JsonWorklist.list;

            var GetSettings = clsSettings.GetAppSettings(OrdId);
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
                if (data.item_type == "CT")
                {
                    CT.Add(data);
                }
                if (data.item_type == "MRI1")
                {
                    MRI1.Add(data);
                }
                if (data.item_type == "MRI3")
                {
                    MRI3.Add(data);
                }
                if (data.item_type == "Radiology")
                {
                    Radiology.Add(data);
                }
                if (data.item_type == "USG")
                {
                    USG.Add(data);
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

            dtCT = Helper.ToDataTable(CT.Where(x => x.is_future_order == false).ToList());
            dtMRI1 = Helper.ToDataTable(MRI1.Where(x => x.is_future_order == false).ToList());
            dtMRI3 = Helper.ToDataTable(MRI3.Where(x => x.is_future_order == false).ToList());
            dtRadiology = Helper.ToDataTable(Radiology.Where(x => x.is_future_order == false).ToList());
            dtUSG = Helper.ToDataTable(USG.Where(x => x.is_future_order == false).ToList());
            dtOthers = Helper.ToDataTable(Others.Where(x => x.is_future_order == false).ToList());

            if (dtCT.Rows.Count != 0)
            {
                lblNoCT.Visible = false;
            }

            if (dtMRI1.Rows.Count != 0)
            {
                lblNoMRI1.Visible = false;
            }

            if (dtMRI3.Rows.Count != 0)
            {
                lblNoMRI3.Visible = false;
            }

            if (dtRadiology.Rows.Count != 0)
            {
                lblNoXray.Visible = false;
            }

            if (dtUSG.Rows.Count != 0)
            {
                lblNoUSG.Visible = false;
            }

            if (dtOthers.Rows.Count != 0)
            {
                lblWarn.Visible = true;
                lblNoOthers.Visible = false;
            }

            repeatCT.DataSource = dtCT;
            repeatCT.DataBind();

            repeatMRI1.DataSource = dtMRI1;
            repeatMRI1.DataBind();

            repeatMRI3.DataSource = dtMRI3;
            repeatMRI3.DataBind();

            repeatUSG.DataSource = dtUSG;
            repeatUSG.DataBind();

            repeatXRay.DataSource = dtRadiology;
            repeatXRay.DataBind();

            repeatOthers.DataSource = dtOthers;
            repeatOthers.DataBind();

            List<string> listRadString = new List<string> { "CT", "MRI1", "MRI3", "Radiology", "USG", "Others" };       
            var ClinicalDiagnosis = (from a in listworklist
                                 where listRadString.Contains(a.item_type) && a.is_future_order == false
                                 select (a.ClinicalDiagnosis != null ? a.ClinicalDiagnosis.ToString() : "-"));

            lblClinicalDiagnosis.Text = ClinicalDiagnosis.Count() == 0 ? "-" : ClinicalDiagnosis.First();

            lblNamaDokter.Text = listworklist[0].DoctorName;

            var COD = (from a in listworklist
                          where listRadString.Contains(a.item_type) && a.is_future_order == false
                          select (a.OrderDate != null ? a.OrderDate.ToString() : "-"));
            lblCreatedOrderDate.Text = COD.Count() == 0 ? "-" : COD.First();

            if (listworklist[0].is_cito == true)
            {
                lblCITO.Visible = true;
            }
            else
            {
                lblCITO.Visible = false;
            }

            //FUTURE ORDER
            dtCT_FO = Helper.ToDataTable(CT.Where(x => x.is_future_order == true).ToList());
            dtMRI1_FO = Helper.ToDataTable(MRI1.Where(x => x.is_future_order == true).ToList());
            dtMRI3_FO = Helper.ToDataTable(MRI3.Where(x => x.is_future_order == true).ToList());
            dtRadiology_FO = Helper.ToDataTable(Radiology.Where(x => x.is_future_order == true).ToList());
            dtUSG_FO = Helper.ToDataTable(USG.Where(x => x.is_future_order == true).ToList());
            dtOthers_FO = Helper.ToDataTable(Others.Where(x => x.is_future_order == true).ToList());

            if (dtCT_FO.Rows.Count != 0)
            {
                lblNoCT_FO.Visible = false;
            }

            if (dtMRI1_FO.Rows.Count != 0)
            {
                lblNoMRI1_FO.Visible = false;
            }

            if (dtMRI3_FO.Rows.Count != 0)
            {
                lblNoMRI3_FO.Visible = false;
            }

            if (dtRadiology_FO.Rows.Count != 0)
            {
                lblNoXray_FO.Visible = false;
            }

            if (dtUSG_FO.Rows.Count != 0)
            {
                lblNoUSG_FO.Visible = false;
            }

            if (dtOthers_FO.Rows.Count != 0)
            {
                lblWarn_FO.Visible = true;
                lblNoOthers_FO.Visible = false;
            }

            repeatCT_FO.DataSource = dtCT_FO;
            repeatCT_FO.DataBind();

            repeatMRI1_FO.DataSource = dtMRI1_FO;
            repeatMRI1_FO.DataBind();

            repeatMRI3_FO.DataSource = dtMRI3_FO;
            repeatMRI3_FO.DataBind();

            repeatUSG_FO.DataSource = dtUSG_FO;
            repeatUSG_FO.DataBind();

            repeatXRay_FO.DataSource = dtRadiology_FO;
            repeatXRay_FO.DataBind();

            repeatOthers_FO.DataSource = dtOthers_FO;
            repeatOthers_FO.DataBind();

            List<string> listRadString_FO = new List<string> { "CT", "MRI1", "MRI3", "Radiology", "USG", "Others" };
            var ClinicalDiagnosis_FO = (from a in listworklist
                                 where listRadString_FO.Contains(a.item_type) && a.is_future_order == true
                                        select (a.ClinicalDiagnosis != null ? a.ClinicalDiagnosis.ToString() : "-"));

            lblClinicalDiagnosis_FO.Text = ClinicalDiagnosis_FO.Count() == 0 ? "-" : ClinicalDiagnosis_FO.First();

            lblNamaDokter_FO.Text = listworklist[0].DoctorName;

            var COD_FO = (from a in listworklist
                          where listRadString_FO.Contains(a.item_type) && a.is_future_order == true
                          select (a.future_order_date != null ? a.future_order_date.ToString("dd MMM yyyy") : "-"));
            lblCreatedOrderDate_FO.Text = COD_FO.Count() == 0 ? "-" : COD_FO.First();

            if (listworklist[0].is_cito == true)
            {
                lblCITO_FO.Visible = true;
            }
            else
            {
                lblCITO_FO.Visible = false;
            }

            var dataRad = listworklist.Where(x => x.is_future_order == false && listRadString_FO.Contains(x.item_type)).ToList();
            var dataRadFO = listworklist.Where(x => x.is_future_order == true && listRadString_FO.Contains(x.item_type)).ToList();
            if (dataRad.Count == 0)
            {
                div_rad.Visible = false;
                div_break.Visible = false;
            }
            if (dataRadFO.Count == 0)
            {
                div_rad_fo.Visible = false;
                div_break.Visible = false;
            }
        }
    }
}