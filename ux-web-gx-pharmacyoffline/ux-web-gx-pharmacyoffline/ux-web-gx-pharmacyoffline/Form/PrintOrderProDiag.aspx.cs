using System;
using System.Data;
using System.Linq;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using Newtonsoft.Json;

public partial class Form_PrintOrderProDiag : Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            loadData();
        }
    }

    private void loadData()
    {
        List<ListProDiag> listProDiag = new List<ListProDiag>();

        List<ListProDiag> listDiagnosticEndoscopy = new List<ListProDiag>();
        List<ListProDiag> listDiagnosticPulmonology = new List<ListProDiag>();
        List<ListProDiag> listDiagnosticNeurology = new List<ListProDiag>();
        List<ListProDiag> listDiagnosticENT = new List<ListProDiag>();
        List<ListProDiag> listDiagnosticCardiology = new List<ListProDiag>();
        List<ListProDiag> listDiagnosticOthers = new List<ListProDiag>();
        List<ListProDiag> listProcedure = new List<ListProDiag>();

        DataTable dtDiagnosticEndoscopy = new DataTable();
        DataTable dtDiagnosticPulmonology = new DataTable();
        DataTable dtDiagnosticNeurology = new DataTable();
        DataTable dtDiagnosticENT = new DataTable();
        DataTable dtDiagnosticCardiology = new DataTable();
        DataTable dtDiagnosticOthers = new DataTable();
        DataTable dtProcedure = new DataTable();

        DataTable dtDiagnosticEndoscopy_FO = new DataTable();
        DataTable dtDiagnosticPulmonology_FO = new DataTable();
        DataTable dtDiagnosticNeurology_FO = new DataTable();
        DataTable dtDiagnosticENT_FO = new DataTable();
        DataTable dtDiagnosticCardiology_FO = new DataTable();
        DataTable dtDiagnosticOthers_FO = new DataTable();
        DataTable dtProcedure_FO = new DataTable();

        long orgId = long.Parse(Request.QueryString["OrganizationId"]);
        long patientId = long.Parse(Request.QueryString["PatientId"]);
        long admissionId = long.Parse(Request.QueryString["AdmissionId"]);
        Guid encounterId = Guid.Parse(Request.QueryString["EncounterId"].ToString());

        var result = clsProDiag.getProDiagOrder(orgId, patientId, admissionId, encounterId);
        var jsonResult = JsonConvert.DeserializeObject<ResultListProDiag>(result.Result.ToString());

        listProDiag = jsonResult.list;

        var GetSettings = clsSettings.GetAppSettings(orgId);
        var theSettings = JsonConvert.DeserializeObject<ResultViewOrganizationSetting>(GetSettings.Result.ToString());
        List<ViewOrganizationSetting> listSettings = new List<ViewOrganizationSetting>();
        listSettings = theSettings.list;

        if (listSettings.Find(y => y.setting_name.ToUpper() == "USE_COVID19").setting_value.ToUpper() == "TRUE")
        {
            string flagCovid = listProDiag[0].IsCOVID.ToString();
            if (flagCovid.ToLower() == "true")
            {
                ImageCovidDiagnostic.Visible = true;
                ImageCovidDiagnostic_FO.Visible = true;
                ImageCovidProcedure.Visible = true;
                ImageCovidProcedure_FO.Visible = true;
            }
            else if (flagCovid.ToLower() == "false")
            {
                ImageCovidDiagnostic.Visible = false;
                ImageCovidDiagnostic_FO.Visible = false;
                ImageCovidProcedure.Visible = false;
                ImageCovidProcedure_FO.Visible = false;
            }
        }

        foreach (ListProDiag data in listProDiag)
        {
            if (data.SalesItemType.ToUpper() == "DIAGNOSTIC" && data.OrderType.ToUpper() == "ENDOSCOPY")
            {
                listDiagnosticEndoscopy.Add(data);
            }
            if (data.SalesItemType.ToUpper() == "DIAGNOSTIC" && data.OrderType.ToUpper() == "PULMONOLOGY")
            {
                listDiagnosticPulmonology.Add(data);
            }
            if (data.SalesItemType.ToUpper() == "DIAGNOSTIC" && data.OrderType.ToUpper() == "NEUROLOGY")
            {
                listDiagnosticNeurology.Add(data);
            }
            if (data.SalesItemType.ToUpper() == "DIAGNOSTIC" && data.OrderType.ToUpper() == "ENT")
            {
                listDiagnosticENT.Add(data);
            }
            if (data.SalesItemType.ToUpper() == "DIAGNOSTIC" && data.OrderType.ToUpper() == "CARDIOLOGY")
            {
                listDiagnosticCardiology.Add(data);
            }
            if (data.SalesItemType.ToUpper() == "DIAGNOSTIC" && data.OrderType.ToUpper() == "OTHERS")
            {
                listDiagnosticOthers.Add(data);
            }
            if (data.SalesItemType.ToUpper() == "PROCEDURE" && data.OrderType.ToUpper() == "OTHERS")
            {
                listProcedure.Add(data);
            }
        }


        lblPregnantDiagnostic.Text = (from data in listProDiag
                                      where data.SalesItemType.ToUpper() == "PREGNANCY" &&
                                            data.OrderType.ToUpper() == "PREGNANCY"
                                      select data.SalesItemName).FirstOrDefault().ToString().ToLower() == "TRUE".ToLower() ? "Hamil" : "";
        lblPregnantDiagnostic_FO.Text = lblPregnantDiagnostic.Text;
        lblPregnantProcedure.Text = lblPregnantDiagnostic.Text;
        lblPregnantProcedure_FO.Text = lblPregnantDiagnostic.Text;

        lblBreastfeedDiagnostic.Text = (from data in listProDiag
                                        where data.SalesItemType.ToUpper() == "BREASTFEEDING" &&
                                              data.OrderType.ToUpper() == "BREASTFEEDING"
                                        select data.SalesItemName).FirstOrDefault().ToString().ToLower() == "TRUE".ToLower() ? "Menyusui" : "";
        lblBreastfeedDiagnostic_FO.Text = lblBreastfeedDiagnostic.Text;
        lblBreastfeedProcedure.Text = lblBreastfeedDiagnostic.Text;
        lblBreastfeedProcedure_FO.Text = lblBreastfeedDiagnostic.Text;

        if (lblPregnantDiagnostic.Text == "" && lblBreastfeedDiagnostic.Text == "")
        {
            lblPregnantDiagnostic.Text = "-";
        }

        if (lblPregnantDiagnostic.Text != "" && lblBreastfeedDiagnostic.Text != "")
        {
            lblPregnantDiagnostic.Text = lblPregnantDiagnostic.Text + ",";
        }

        if (lblPregnantDiagnostic_FO.Text == "" && lblBreastfeedDiagnostic_FO.Text == "")
        {
            lblPregnantDiagnostic_FO.Text = "-";
        }

        if (lblPregnantDiagnostic_FO.Text != "" && lblBreastfeedDiagnostic_FO.Text != "")
        {
            lblPregnantDiagnostic_FO.Text = lblPregnantDiagnostic_FO.Text + ",";
        }

        if (lblPregnantProcedure.Text == "" && lblBreastfeedProcedure.Text == "")
        {
            lblPregnantProcedure.Text = "-";
        }

        if (lblPregnantProcedure.Text != "" && lblBreastfeedProcedure.Text != "")
        {
            lblPregnantProcedure.Text = lblPregnantProcedure.Text + ",";
        }

        if (lblPregnantProcedure_FO.Text == "" && lblBreastfeedProcedure_FO.Text == "")
        {
            lblPregnantProcedure_FO.Text = "-";
        }

        if (lblPregnantProcedure_FO.Text != "" && lblBreastfeedProcedure_FO.Text != "")
        {
            lblPregnantProcedure_FO.Text = lblPregnantProcedure_FO.Text + ",";
        }

        //ORDER
        dtDiagnosticEndoscopy = Helper.ToDataTable(listDiagnosticEndoscopy.Where(x => x.IsFutureOrder == false).ToList());
        dtDiagnosticPulmonology = Helper.ToDataTable(listDiagnosticPulmonology.Where(x => x.IsFutureOrder == false).ToList());
        dtDiagnosticNeurology = Helper.ToDataTable(listDiagnosticNeurology.Where(x => x.IsFutureOrder == false).ToList());
        dtDiagnosticENT = Helper.ToDataTable(listDiagnosticENT.Where(x => x.IsFutureOrder == false).ToList());
        dtDiagnosticCardiology = Helper.ToDataTable(listDiagnosticCardiology.Where(x => x.IsFutureOrder == false).ToList());
        dtDiagnosticOthers = Helper.ToDataTable(listDiagnosticOthers.Where(x => x.IsFutureOrder == false).ToList());
        dtProcedure = Helper.ToDataTable(listProcedure.Where(x => x.IsFutureOrder == false).ToList());

        if (dtDiagnosticEndoscopy.Rows.Count != 0)
        {
            lblNoDiagnosticEndoscopy.Visible = false;
        }

        if (dtDiagnosticPulmonology.Rows.Count != 0)
        {
            lblNoDiagnosticPulmonology.Visible = false;
        }

        if (dtDiagnosticNeurology.Rows.Count != 0)
        {
            lblNoDiagnosticNeurology.Visible = false;
        }

        if (dtDiagnosticENT.Rows.Count != 0)
        {
            lblNoDiagnosticENT.Visible = false;
        }

        if (dtDiagnosticCardiology.Rows.Count != 0)
        {
            lblNoDiagnosticCardiology.Visible = false;
        }

        if (dtDiagnosticOthers.Rows.Count != 0)
        {
            lblNoDiagnosticOthers.Visible = false;
        }

        if (dtProcedure.Rows.Count != 0)
        {
            lblNoProcedure.Visible = false;
        }

        rptDiagnosticEndoscopy.DataSource = dtDiagnosticEndoscopy;
        rptDiagnosticEndoscopy.DataBind();

        rptDiagnosticPulmonology.DataSource = dtDiagnosticPulmonology;
        rptDiagnosticPulmonology.DataBind();

        rptDiagnosticNeurology.DataSource = dtDiagnosticNeurology;
        rptDiagnosticNeurology.DataBind();

        rptDiagnosticENT.DataSource = dtDiagnosticENT;
        rptDiagnosticENT.DataBind();

        rptDiagnosticCardiology.DataSource = dtDiagnosticCardiology;
        rptDiagnosticCardiology.DataBind();

        rptDiagnosticOthers.DataSource = dtDiagnosticOthers;
        rptDiagnosticOthers.DataBind();

        rptProcedure.DataSource = dtProcedure;
        rptProcedure.DataBind();

        List<string> listProDiagString = new List<string> { "ENDOSCOPY", "PULMONOLOGY", "NEUROLOGY", "ENT", "CARDIOLOGY", "OTHERS" };
        var ClinicalDiagnosis = (from a in listProDiag
                                 where listProDiagString.Contains(a.OrderType.ToUpper()) && a.IsFutureOrder == false
                                 select (a.ClinicalDiagnosis != null ? a.ClinicalDiagnosis.ToString() : "-"));

        lblClinicalDiagnosisDiagnostic.Text = ClinicalDiagnosis.Count() == 0 ? "-" : ClinicalDiagnosis.First();
        lblClinicalDiagnosisProcedure.Text = ClinicalDiagnosis.Count() == 0 ? "-" : ClinicalDiagnosis.First();

        lblNamaDokterDiagnostic.Text = listProDiag[0].DoctorName;
        lblNamaDokterProcedure.Text = listProDiag[0].DoctorName;

        var COD_Diagnostic = (from a in listProDiag
                              where listProDiagString.Contains(a.OrderType.ToUpper()) && 
                                    a.SalesItemType.ToUpper() == "DIAGNOSTIC" && 
                                    a.IsFutureOrder == false
                              select (a.OrderDate != null && a.OrderDate.Year != 1900 ? a.OrderDate.Hour != 0 ? a.OrderDate.ToString("dd MMM yyyy - HH:mm:ss") : a.OrderDate.ToString("dd MMM yyyy") : "-"));
        lblCreatedOrderDateDiagnostic.Text = COD_Diagnostic.Count() == 0 ? "-" : COD_Diagnostic.First();

        var COD_Procedure = (from a in listProDiag
                             where listProDiagString.Contains(a.OrderType.ToUpper()) &&
                                   a.SalesItemType.ToUpper() == "PROCEDURE" &&
                                   a.IsFutureOrder == false
                             select (a.OrderDate != null && a.OrderDate.Year != 1900 ? a.OrderDate.Hour != 0 ? a.OrderDate.ToString("dd MMM yyyy - HH:mm:ss") : a.OrderDate.ToString("dd MMM yyyy") : "-"));
        lblCreatedOrderDateProcedure.Text = COD_Procedure.Count() == 0 ? "-" : COD_Procedure.First();

        //FUTURE ORDER
        dtDiagnosticEndoscopy_FO = Helper.ToDataTable(listDiagnosticEndoscopy.Where(x => x.IsFutureOrder == true).ToList());
        dtDiagnosticPulmonology_FO = Helper.ToDataTable(listDiagnosticPulmonology.Where(x => x.IsFutureOrder == true).ToList());
        dtDiagnosticNeurology_FO = Helper.ToDataTable(listDiagnosticNeurology.Where(x => x.IsFutureOrder == true).ToList());
        dtDiagnosticENT_FO = Helper.ToDataTable(listDiagnosticENT.Where(x => x.IsFutureOrder == true).ToList());
        dtDiagnosticCardiology_FO = Helper.ToDataTable(listDiagnosticCardiology.Where(x => x.IsFutureOrder == true).ToList());
        dtDiagnosticOthers_FO = Helper.ToDataTable(listDiagnosticOthers.Where(x => x.IsFutureOrder == true).ToList());
        dtProcedure_FO = Helper.ToDataTable(listProcedure.Where(x => x.IsFutureOrder == true).ToList());

        if (dtDiagnosticEndoscopy_FO.Rows.Count != 0)
        {
            lblNoDiagnosticEndoscopy_FO.Visible = false;
        }

        if (dtDiagnosticPulmonology_FO.Rows.Count != 0)
        {
            lblNoDiagnosticPulmonology_FO.Visible = false;
        }

        if (dtDiagnosticNeurology_FO.Rows.Count != 0)
        {
            lblNoDiagnosticNeurology_FO.Visible = false;
        }

        if (dtDiagnosticENT_FO.Rows.Count != 0)
        {
            lblNoDiagnosticENT_FO.Visible = false;
        }

        if (dtDiagnosticCardiology_FO.Rows.Count != 0)
        {
            lblNoDiagnosticCardiology_FO.Visible = false;
        }

        if (dtDiagnosticOthers_FO.Rows.Count != 0)
        {
            lblNoDiagnosticOthers_FO.Visible = false;
        }

        if (dtProcedure_FO.Rows.Count != 0)
        {
            lblNoProcedure_FO.Visible = false;
        }

        rptDiagnosticEndoscopy_FO.DataSource = dtDiagnosticEndoscopy_FO;
        rptDiagnosticEndoscopy_FO.DataBind();

        rptDiagnosticPulmonology_FO.DataSource = dtDiagnosticPulmonology_FO;
        rptDiagnosticPulmonology_FO.DataBind();

        rptDiagnosticNeurology_FO.DataSource = dtDiagnosticNeurology_FO;
        rptDiagnosticNeurology_FO.DataBind();

        rptDiagnosticENT_FO.DataSource = dtDiagnosticENT_FO;
        rptDiagnosticENT_FO.DataBind();

        rptDiagnosticCardiology_FO.DataSource = dtDiagnosticCardiology_FO;
        rptDiagnosticCardiology_FO.DataBind();

        rptDiagnosticOthers_FO.DataSource = dtDiagnosticOthers_FO;
        rptDiagnosticOthers_FO.DataBind();

        rptProcedure_FO.DataSource = dtProcedure_FO;
        rptProcedure_FO.DataBind();

        List<string> listProDiagString_FO = new List<string> { "ENDOSCOPY", "PULMONOLOGY", "NEUROLOGY", "ENT", "CARDIOLOGY", "OTHERS" };
        var ClinicalDiagnosis_FO = (from a in listProDiag
                                    where listProDiagString_FO.Contains(a.OrderType.ToUpper()) && a.IsFutureOrder == true
                                    select (a.ClinicalDiagnosis != null ? a.ClinicalDiagnosis.ToString() : "-"));

        lblClinicalDiagnosisDiagnostic_FO.Text = ClinicalDiagnosis_FO.Count() == 0 ? "-" : ClinicalDiagnosis_FO.First();
        lblClinicalDiagnosisProcedure_FO.Text = ClinicalDiagnosis_FO.Count() == 0 ? "-" : ClinicalDiagnosis_FO.First();

        lblNamaDokterDiagnostic_FO.Text = listProDiag[0].DoctorName;
        lblNamaDokterProcedure_FO.Text = listProDiag[0].DoctorName;

        var COD_DiagnosticFO = (from a in listProDiag
                                where listProDiagString.Contains(a.OrderType.ToUpper()) && 
                                      a.SalesItemType.ToUpper() == "DIAGNOSTIC" &&
                                      a.IsFutureOrder == true
                                select (a.OrderDate != null && a.OrderDate.Year != 1900 ? a.OrderDate.Hour != 0 ? a.OrderDate.ToString("dd MMM yyyy - HH:mm:ss") : a.OrderDate.ToString("dd MMM yyyy") : "-"));
        lblCreatedOrderDateDiagnostic_FO.Text = COD_DiagnosticFO.Count() == 0 ? "-" : COD_DiagnosticFO.First();

        var COD_ProcedureFO = (from a in listProDiag
                               where listProDiagString.Contains(a.OrderType.ToUpper()) &&
                                     a.SalesItemType.ToUpper() == "PROCEDURE" &&
                                     a.IsFutureOrder == true
                               select (a.OrderDate != null && a.OrderDate.Year != 1900 ? a.OrderDate.Hour != 0 ? a.OrderDate.ToString("dd MMM yyyy - HH:mm:ss") : a.OrderDate.ToString("dd MMM yyyy") : "-"));
        lblCreatedOrderDateProcedure_FO.Text = COD_ProcedureFO.Count() == 0 ? "-" : COD_ProcedureFO.First();

        var dataDiagnostic = listProDiag.Where(x => x.IsFutureOrder == false && listProDiagString.Contains(x.OrderType.ToUpper()) && x.SalesItemType.ToUpper() == "DIAGNOSTIC").ToList();
        var dataDiagnosticFO = listProDiag.Where(x => x.IsFutureOrder == true && listProDiagString_FO.Contains(x.OrderType.ToUpper()) && x.SalesItemType.ToUpper() == "DIAGNOSTIC").ToList();
        var dataProcedure = listProDiag.Where(x => x.IsFutureOrder == false && x.OrderType.ToUpper() == "OTHERS" && x.SalesItemType.ToUpper() == "PROCEDURE").ToList();
        var dataProcedureFO = listProDiag.Where(x => x.IsFutureOrder == true && x.OrderType.ToUpper() == "OTHERS" && x.SalesItemType.ToUpper() == "PROCEDURE").ToList();

        // validate what container will be print
        System.Web.UI.HtmlControls.HtmlGenericControl div_BreakEnd = null;
        if (dataDiagnostic.Count > 0)
        {
            div_diagnostic.Visible = true;
            div_break_diagnostic.Visible = true;
            div_BreakEnd = div_break_diagnostic;
        }
        if (dataDiagnosticFO.Count > 0)
        {
            div_diagnostic_fo.Visible = true;
            div_break_diagnostic_fo.Visible = true;
            div_BreakEnd = div_break_diagnostic_fo;
        }
        if (dataProcedure.Count > 0)
        {
            div_procedure.Visible = true;
            div_break_procedure.Visible = true;
            div_BreakEnd = div_break_procedure;
        }
        if (dataProcedureFO.Count > 0)
        {
            div_procedure_fo.Visible = true;
            div_break_procedure_fo.Visible = false;
            div_BreakEnd = div_break_procedure_fo;
        }
        div_BreakEnd.Visible = false;
    }
}
