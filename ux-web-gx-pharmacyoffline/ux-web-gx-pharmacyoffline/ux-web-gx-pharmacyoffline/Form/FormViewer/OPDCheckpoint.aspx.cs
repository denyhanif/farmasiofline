using log4net;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Form_FormViewer_OPDCheckpoint : System.Web.UI.Page
{
    protected static readonly ILog log = LogManager.GetLogger(typeof(Form_FormViewer_OPDCheckpoint));

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            HF_OrgID.Value = Request.QueryString["OrgID"];
            HF_UserID.Value = Request.QueryString["UserID"];
            HF_UserName.Value = Request.QueryString["Username"];
            HF_HospitalID.Value = Request.QueryString["HospitalID"];
            HF_FlagSearchType.Value = Request.QueryString["FlagSearchType"];

            //HF_OrgID.Value = "2";
            //HF_UserID.Value = "123";
            //HF_UserName.Value = "PharmacyUser";
            //string hospital_id = "39764039-37b9-4176-a025-ef7b2e124ba4";
            //string flagSearchType = "MR";

            var registryflag = ConfigurationManager.AppSettings["registryflag"].ToString();
            if (registryflag == "1")
            {
                ConfigurationManager.AppSettings["urlPharmacy"] = SiloamConfig.Functions.GetValue("urlExtension").ToString();
                ConfigurationManager.AppSettings["urlRecord"] = SiloamConfig.Functions.GetValue("urlPharmacy").ToString();
                ConfigurationManager.AppSettings["BaseURL_MySiloam_OPAdmin"] = SiloamConfig.Functions.GetValue("BaseURL_MySiloam_OPAdmin").ToString();
            }
            
            if (HF_FlagSearchType.Value.ToString() == "MR")
            {
                MRsectionactive();
            }

            load_QueueLinelist(HF_HospitalID.Value.ToString());
        }
    }
    protected void BtnTabMR_Click(object sender, EventArgs e)
    {
        MRsectionactive();
        TxtSearchTglAdmMR.Text = DateTime.Now.ToString("dd MMM yyyy");
    }

    protected void BtnTabPasien_Click(object sender, EventArgs e)
    {
        Pasiensectionactive();
        TxtSearchTglAdmPasien.Text = DateTime.Now.ToString("dd MMM yyyy");
    }

    protected void BtnTabBarcode_Click(object sender, EventArgs e)
    {
        Barcodesectionactive();
        TxtSearchTglAdmBarcode.Text = DateTime.Now.ToString("dd MMM yyyy");
    }

    protected void MRsectionactive()
    {
        divmrsearch.Visible = true;
        BtnTabMR.CssClass = "btn btn-sm btn-tab-active";
        TxtSearchTglAdmMR.Text = DateTime.Now.ToString("dd MMM yyyy");

        divpasiensearch.Visible = false;
        BtnTabPasien.CssClass = "btn btn-sm btn-tab";

        divbarcodesearch.Visible = false;
        BtnTabBarcode.CssClass = "btn btn-sm btn-tab";

        ResetResult();
    }

    protected void Pasiensectionactive()
    {
        divmrsearch.Visible = false;
        BtnTabMR.CssClass = "btn btn-sm btn-tab";

        divpasiensearch.Visible = true;
        BtnTabPasien.CssClass = "btn btn-sm btn-tab-active";
        TxtSearchTglAdmPasien.Text = DateTime.Now.ToString("dd MMM yyyy");
        TxtSearchTglLahirPasien.Text = DateTime.Now.AddYears(-30).ToString("dd MMM yyyy"); //DateTime.Parse("01/01/1990").ToString("dd MMM yyyy"); //

        divbarcodesearch.Visible = false;
        BtnTabBarcode.CssClass = "btn btn-sm btn-tab";

        ResetResult();
    }

    protected void Barcodesectionactive()
    {
        divmrsearch.Visible = false;
        BtnTabMR.CssClass = "btn btn-sm btn-tab";

        divpasiensearch.Visible = false;
        BtnTabPasien.CssClass = "btn btn-sm btn-tab";

        divbarcodesearch.Visible = true;
        BtnTabBarcode.CssClass = "btn btn-sm btn-tab-active";
        TxtSearchTglAdmBarcode.Text = DateTime.Now.ToString("dd MMM yyyy");

        ResetResult();
    }

    public void GetSearchData(Int64 organization_id, string Search1, string Search2, int Search_type, DateTime adm_date)
    {
        log.Info(LogLibrary.Logging("S", "GetSearchData", HF_UserName.Value, ""));

        try
        {
            log.Debug(LogLibrary.Logging("S", "clsSingleQueue.getDataSingleQueue", HF_UserName.Value, "params : " + organization_id + "," + Search1 + "," + Search2 + "," + Search_type + "," + adm_date.ToString()));

            SingleQueue data = new SingleQueue();
            var dataGet = clsSingleQueue.getDataSingleQueue(organization_id, Search1, Search2, Search_type, adm_date);
            var dataJson = JsonConvert.DeserializeObject<ResultSingleQueue>(dataGet.Result.ToString());
            data = dataJson.list;

            log.Debug(LogLibrary.Logging("E", "clsSingleQueue.getDataSingleQueue", HF_UserName.Value, dataJson.ToString()));

            Session[Helper.SessionPatientList] = data;

            if (data.patientHeader.Count != 0)
            {
                //---Patient
                List<PatientHeaderSQ> datapasien = new List<PatientHeaderSQ>();
                datapasien = data.patientHeader;
                DataTable tablepasien = Helper.ToDataTable(datapasien);

                if (data.patientHeader.Count > 1)
                {
                    Gvw_listpatient.DataSource = tablepasien;
                    Gvw_listpatient.DataBind();

                    ScriptManager.RegisterStartupScript(Page, GetType(), "showlist", "openmodal_patientlist();", true);
                }
                else
                {
                    bindSelectedDataPatient(data, datapasien[0].patientId, datapasien[0].admissionId);

                    divresult.Visible = true;
                    divresultnotfound.Visible = false;
                }
            }
            else
            {
                // No Data Found
                divresultnotfound.Visible = true;
                divresult.Visible = false;
            }
        }
        catch (Exception ex)
        {
            log.Error(LogLibrary.Error("GetSearchData", HF_UserName.Value, ex.Message.ToString()));
        }

        log.Info(LogLibrary.Logging("E", "GetSearchData", HF_UserName.Value, ""));
    }

    public void bindToHeaderPatient(PatientHeaderSQ header)
    {
        lbl_patientname.Text = header.patientName;
        lbl_dob.Text = header.dob.ToString("dd MMM yyyy") + " / " + header.age;
        if (header.sexId == 1)
        {
            ImageMale.Visible = true;
            ImageFemale.Visible = false;
        }
        else if (header.sexId == 2)
        {
            ImageMale.Visible = false;
            ImageFemale.Visible = true;
        }
        lbl_mrno.Text = header.mrNumber;
        lbl_admno.Text = header.AdmissionNo;
        lbl_doctorname.Text = header.doctorName;
        lbl_payer.Text = header.payerName;
        lbl_presnotes.Text = header.PrescriptionNotes.Replace("\n","<br />");

        if (header.PrescriptionNotes == "")
        {
            div_presnotes.Visible = false;
        }
        else
        {
            div_presnotes.Visible = true;
        }
    }

    public void bindSelectedDataPatient(SingleQueue data, Int64 PtnID, Int64 AdmID)
    {
        log.Info(LogLibrary.Logging("S", "bindSelectedDataPatient", HF_UserName.Value, ""));

        try
        {
            //---Patient
            List<PatientHeaderSQ> datapasien = new List<PatientHeaderSQ>();
            datapasien = data.patientHeader.FindAll(x => x.patientId == PtnID && x.admissionId == AdmID);
            DataTable tablepasien = Helper.ToDataTable(datapasien);

            bindToHeaderPatient(datapasien[0]);
            GvwPatientData.DataSource = tablepasien;
            GvwPatientData.DataBind();

            LabelNamaPatient.Text = datapasien[0].patientName;
            LabelAgePatient.Text = datapasien[0].age;
            LabelMRPatient.Text = datapasien[0].mrNumber;
            LabelDokterPatient.Text = datapasien[0].doctorName;

            HF_PtntID.Value = datapasien[0].patientId.ToString();
            HF_AdmID.Value = datapasien[0].admissionId.ToString();
            HF_EncID.Value = datapasien[0].encounterId.ToString();
            HF_DokterName.Value = datapasien[0].doctorName.ToString();

            if (datapasien[0].IsTransaction == true)
            {
                //ButtonProsesBayar.Visible = false;
                ButtonProsesBayar.Text = "Sudah Diproses";
                ButtonProsesBayar.CssClass = "btn btn-primary disable-obj";
                divheaderchkdrug1.Attributes.Add("class", "disable-obj");
                divheaderchkdrug2.Attributes.Add("class", "disable-obj");

                divresult.Attributes.Remove("class");
                divresult.Attributes.Add("class", "div-search-result-done");
            }
            else
            {
                //ButtonProsesBayar.Visible = true;
                ButtonProsesBayar.Text = "Proses";
                ButtonProsesBayar.CssClass = "btn btn-success";
                divheaderchkdrug1.Attributes.Remove("class");
                divheaderchkdrug2.Attributes.Remove("class");

                divresult.Attributes.Remove("class");
                divresult.Attributes.Add("class", "div-search-result");
            }

            if (datapasien[0].IsTakeAll == true)
            {
                RadioDrug1.Checked = true;
                RadioDrug2.Checked = false;
                ScriptManager.RegisterStartupScript(Page, GetType(), "ceklist", "klikChk('RadioDrug1','divchkdrug1','chk-drug');", true);
            }
            else
            {
                if (datapasien[0].IsTransaction == true)
                {
                    RadioDrug2.Checked = true;
                    RadioDrug1.Checked = false;
                    ScriptManager.RegisterStartupScript(Page, GetType(), "ceklist", "klikChk('RadioDrug2','divchkdrug2','chk-drug');", true);
                }
                else
                {
                    divheaderchkdrug1.Attributes.Remove("class");

                    RadioDrug1.Checked = true;
                    RadioDrug2.Checked = false;
                    ScriptManager.RegisterStartupScript(Page, GetType(), "ceklist", "klikChk('RadioDrug1','divchkdrug1','chk-drug');", true);
                }
            }

            //---Drug
            List<PrescriptionOrderSQ> dataobat = new List<PrescriptionOrderSQ>();
            dataobat = data.prescriptionOrder.FindAll(x => x.isConsumables == false && x.patientId == PtnID && x.admissionId == AdmID);
            foreach (var templist in dataobat)
            {
                templist.dose = Helper.formatDecimal(templist.dose);
                templist.quantity = Helper.formatDecimal(templist.quantity);
                templist.TotalQuantity = Helper.formatDecimal(templist.TotalQuantity);

                templist.urlDetailTrx = GenerateURLPrint("SHORTSOAP", HF_OrgID.Value, HF_AdmID.Value, HF_EncID.Value, HF_PtntID.Value, HF_UserName.Value, HF_DokterName.Value, "");
            }

            if (dataobat.Count != 0)
            {
                DataTable tableobat = Helper.ToDataTable(dataobat);

                GvwPrescriptionOrder.DataSource = tableobat;
                GvwPrescriptionOrder.DataBind();

                LabelDrugsCount.Text = "- Drugs : " + dataobat.Count;
                LabelDrugsCount.Visible = true;
                Div_Drugs.Visible = true;
            }
            else
            {
                LabelDrugsCount.Text = "";
                LabelDrugsCount.Visible = false;
                Div_Drugs.Visible = false;
            }

            //---Cons
            List<PrescriptionOrderSQ> datacons = new List<PrescriptionOrderSQ>();
            datacons = data.prescriptionOrder.FindAll(x => x.isConsumables == true && x.patientId == PtnID && x.admissionId == AdmID);
            foreach (var templist in datacons)
            {
                templist.dose = Helper.formatDecimal(templist.dose);
                templist.quantity = Helper.formatDecimal(templist.quantity);
                templist.TotalQuantity = Helper.formatDecimal(templist.TotalQuantity);

                templist.urlDetailTrx = GenerateURLPrint("SHORTSOAP", HF_OrgID.Value, HF_AdmID.Value, HF_EncID.Value, HF_PtntID.Value, HF_UserName.Value, HF_DokterName.Value, "");
            }

            if (datacons.Count != 0)
            {
                DataTable tablecons = Helper.ToDataTable(datacons);

                GvwConsumableOrder.DataSource = tablecons;
                GvwConsumableOrder.DataBind();

                LabelConsCount.Text = "- Consumables : " + datacons.Count;
                LabelConsCount.Visible = true;
                Div_Cons.Visible = true;
            }
            else
            {
                LabelConsCount.Text = "";
                LabelConsCount.Visible = false;
                Div_Cons.Visible = false;
            }

            //---Racikan
            List<CompoundHeaderSQ> dataRacikanHeader = new List<CompoundHeaderSQ>();
            dataRacikanHeader = data.compoundHeader.FindAll(x => x.patientId == PtnID && x.admissionId == AdmID);
            List<CompoundDetailSQ> dataRacikanDetail = new List<CompoundDetailSQ>();
            //dataRacikanDetail = data.compoundDetail;

            List<CompoundDetailSQ> dataAllRacikanDetail = new List<CompoundDetailSQ>();
            dataAllRacikanDetail = data.compoundDetail;
            foreach (CompoundHeaderSQ header in dataRacikanHeader)
            {
                dataRacikanDetail.AddRange(dataAllRacikanDetail.FindAll(x => x.compoundId == header.compoundId));
            }


            if (dataRacikanHeader.Count > 0)
            {
                foreach (var templist in dataRacikanHeader)
                {
                    templist.dose = Helper.formatDecimal(templist.dose);
                    templist.quantity = Helper.formatDecimal(templist.quantity);

                    templist.urlDetailTrx = GenerateURLPrint("SHORTSOAP", HF_OrgID.Value, HF_AdmID.Value, HF_EncID.Value, HF_PtntID.Value, HF_UserName.Value, HF_DokterName.Value, "");
                }

                DataTable dtracikan = Helper.ToDataTable(dataRacikanHeader);
                //Session[Helper.SessionRacikanHeader + hfguidadditional.Value] = dtracikan;

                if (dataRacikanDetail.Count > 0)
                {
                    foreach (var templist in dataRacikanDetail)
                    {
                        templist.dose = Helper.formatDecimal(templist.dose);
                        templist.TotalQuantity = Helper.formatDecimal(templist.TotalQuantity);

                        templist.urlDetailTrx = GenerateURLPrint("SHORTSOAP", HF_OrgID.Value, HF_AdmID.Value, HF_EncID.Value, HF_PtntID.Value, HF_UserName.Value, HF_DokterName.Value, "");
                    }

                    DataTable dtracikandetail = Helper.ToDataTable(dataRacikanDetail);
                    Session[Helper.SessionRacikanDetail] = dtracikandetail;
                }

                gvw_racikan_header.DataSource = dtracikan;
                gvw_racikan_header.DataBind();

                LabelRacikanCount.Text = "- Racikan : " + dataRacikanHeader.Count;
                LabelRacikanCount.Visible = true;
                Div_Compounds.Visible = true;

                RadioDrug2.Checked = true;
                RadioDrug1.Checked = false;
                divheaderchkdrug1.Attributes.Add("class", "disable-obj");
                ScriptManager.RegisterStartupScript(Page, GetType(), "ceklist", "klikChk('RadioDrug2','divchkdrug2','chk-drug');", true);
            }
            else
            {
                gvw_racikan_header.DataSource = null;
                gvw_racikan_header.DataBind();
                Session[Helper.SessionRacikanDetail] = null;

                LabelRacikanCount.Text = "";
                LabelRacikanCount.Visible = false;
                Div_Compounds.Visible = false;
            }

            //---Lab
            List<CPOEORderSQ> datalab = new List<CPOEORderSQ>();
            datalab = data.cpoeOrder.FindAll(x => x.cpoeType == "LAB" && x.patientId == PtnID && x.admissionId == AdmID);

            foreach (CPOEORderSQ L in datalab)
            {
                L.urlDetailTrx = GenerateURLPrint("LAB", HF_OrgID.Value, HF_AdmID.Value, HF_EncID.Value, HF_PtntID.Value, HF_UserName.Value, HF_DokterName.Value, "");
            }

            if (datalab.Count != 0)
            {
                DataTable tablelab = Helper.ToDataTable(datalab);

                GvwLabOrder.DataSource = tablelab;
                GvwLabOrder.DataBind();

                LabelLabCount.Text = "- Laboratory : " + datalab.Count;
                LabelLabCount.Visible = true;
                Div_Lab.Visible = true;
            }
            else
            {
                LabelLabCount.Text = "";
                LabelLabCount.Visible = false;
                Div_Lab.Visible = false;
            }

            //---Rad
            List<CPOEORderSQ> datarad = new List<CPOEORderSQ>();
            datarad = data.cpoeOrder.FindAll(x => x.cpoeType == "RAD" && x.patientId == PtnID && x.admissionId == AdmID);

            foreach (CPOEORderSQ R in datarad)
            {
                R.urlDetailTrx = GenerateURLPrint("RAD", HF_OrgID.Value, HF_AdmID.Value, HF_EncID.Value, HF_PtntID.Value, HF_UserName.Value, HF_DokterName.Value, "");
            }

            if (datarad.Count != 0)
            {
                DataTable tablerad = Helper.ToDataTable(datarad);

                GvwRadOrder.DataSource = tablerad;
                GvwRadOrder.DataBind();

                LabelRadCount.Text = "- Radiology : " + datarad.Count;
                LabelRadCount.Visible = true;
                Div_Rad.Visible = true;
            }
            else
            {
                LabelRadCount.Text = "";
                LabelRadCount.Visible = false;
                Div_Rad.Visible = false;
            }

            //---Procedure
            List<SQSOAPProcedureDiagnosis> dataprocedure = new List<SQSOAPProcedureDiagnosis>();
            dataprocedure = data.ProcedureDiagOrder.FindAll(x => x.PatientId == PtnID && x.AdmissionId == AdmID);

            if (dataprocedure.Count != 0)
            {
                DataTable tableproc = Helper.ToDataTable(dataprocedure);

                GvwProcedureDiagnosis.DataSource = tableproc;
                GvwProcedureDiagnosis.DataBind();

                LabelProcCount.Text = "- Procedure : " + dataprocedure.Count;
                LabelProcCount.Visible = true;
                Div_Procedure.Visible = true;
            }
            else
            {
                LabelProcCount.Text = "";
                LabelProcCount.Visible = false;
                Div_Procedure.Visible = false;
            }

            //Collect SingleQueue Data
            Session["DataSingleQueue"] = null;

            SingleQueue NewDataSQ = new SingleQueue();
            NewDataSQ.patientHeader = new List<PatientHeaderSQ>();
            NewDataSQ.prescriptionOrder = new List<PrescriptionOrderSQ>();
            NewDataSQ.compoundHeader = new List<CompoundHeaderSQ>();
            NewDataSQ.compoundDetail = new List<CompoundDetailSQ>();
            NewDataSQ.cpoeOrder = new List<CPOEORderSQ>();
            NewDataSQ.ProcedureDiagOrder = new List<SQSOAPProcedureDiagnosis>();

            NewDataSQ.patientHeader.Add(datapasien[0]);
            NewDataSQ.prescriptionOrder.AddRange(dataobat);
            NewDataSQ.prescriptionOrder.AddRange(datacons);
            NewDataSQ.compoundHeader.AddRange(dataRacikanHeader);
            NewDataSQ.compoundDetail.AddRange(dataRacikanDetail);
            NewDataSQ.cpoeOrder.AddRange(datalab);
            NewDataSQ.cpoeOrder.AddRange(datarad);
            NewDataSQ.ProcedureDiagOrder.AddRange(dataprocedure);

            Session["DataSingleQueue"] = NewDataSQ;
        }
        catch (Exception ex)
        {
            log.Error(LogLibrary.Error("bindSelectedDataPatient", HF_UserName.Value, ex.Message.ToString()));
        }

        log.Info(LogLibrary.Logging("E", "bindSelectedDataPatient", HF_UserName.Value, ""));
    }

    protected void gvw_racikan_header_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string headerId = gvw_racikan_header.DataKeys[e.Row.RowIndex].Value.ToString();
            GridView gvDetails = e.Row.FindControl("gvw_racikan_detail") as GridView;

            if (Session[Helper.SessionRacikanDetail] != null)
            {
                DataRow[] dr = ((DataTable)Session[Helper.SessionRacikanDetail]).Select("compoundId = '" + headerId + "'");

                if (dr.Length > 0)
                {
                    DataTable dtDetail = dr.CopyToDataTable();
                    gvDetails.DataSource = dtDetail;
                    gvDetails.DataBind();
                }
            }
        }
    }

    public void ResetResult()
    {
        divresult.Visible = false;
        divresultnotfound.Visible = false;
        GvwPrescriptionOrder.DataSource = null;
        GvwPrescriptionOrder.DataBind();
        GvwConsumableOrder.DataSource = null;
        GvwConsumableOrder.DataBind();
        gvw_racikan_header.DataSource = null;
        gvw_racikan_header.DataBind();
        GvwLabOrder.DataSource = null;
        GvwLabOrder.DataBind();
        GvwRadOrder.DataSource = null;
        GvwRadOrder.DataBind();
        GvwProcedureDiagnosis.DataSource = null;
        GvwProcedureDiagnosis.DataBind();

        TxtSearchMR.Text = "";
        TxtSearchPasien.Text = "";
        //TxtSearchTglLahirPasien.Text = "";
        TxtSearchBarcode.Text = "";

        Session[Helper.SessionPatientList] = null;
    }


    protected void BtnCariPasienMR_Click(object sender, EventArgs e)
    {
        //GetSearchData(Int64.Parse(HF_OrgID.Value), "567432", "aselole", 1, DateTime.Parse("14/9/2020")); //1: by mr, 2: by name dob, 3: by Barcode/QR
        GetSearchData(Int64.Parse(HF_OrgID.Value), TxtSearchMR.Text, "searchMR", 1, DateTime.Parse(TxtSearchTglAdmMR.Text));
    }

    protected void BtnCariPasienPasien_Click(object sender, EventArgs e)
    {
        GetSearchData(Int64.Parse(HF_OrgID.Value), TxtSearchPasien.Text, TxtSearchTglLahirPasien.Text, 2, DateTime.Parse(TxtSearchTglAdmPasien.Text));
    }

    protected void BtnCariPasienBarcode_Click(object sender, EventArgs e)
    {
        GetSearchData(Int64.Parse(HF_OrgID.Value), TxtSearchBarcode.Text, "searchBARCODE", 3, DateTime.Parse(TxtSearchTglAdmBarcode.Text));
    }

    protected void ButtonChoosePatient_Click(object sender, EventArgs e)
    {
        int selRowIndex = ((GridViewRow)(((Button)sender).Parent.Parent)).RowIndex;
        HiddenField HF_ptnID = (HiddenField)Gvw_listpatient.Rows[selRowIndex].FindControl("HF_ptnID");
        HiddenField HF_admID = (HiddenField)Gvw_listpatient.Rows[selRowIndex].FindControl("HF_admID");
        SingleQueue data = (SingleQueue)Session[Helper.SessionPatientList];

        bindSelectedDataPatient(data, Int64.Parse(HF_ptnID.Value), Int64.Parse(HF_admID.Value));

        ScriptManager.RegisterStartupScript(Page, GetType(), "hidelist", "closemodal_patientlist();", true);

        divresult.Visible = true;
        divresultnotfound.Visible = false;
    }

    protected List<CheckPriceRequest> GetRowListForPrice()
    {
        List<CheckPriceRequest> data = new List<CheckPriceRequest>();
   
        foreach (GridViewRow rows in GvwPrescriptionOrder.Rows)
        {
            HiddenField HF_itemid = (HiddenField)rows.FindControl("HF_itemid");
            HiddenField HF_qty = (HiddenField)rows.FindControl("HF_qty");
            HiddenField HF_uomid = (HiddenField)rows.FindControl("HF_uomid");
            HiddenField HF_iscons = (HiddenField)rows.FindControl("HF_iscons");

            CheckPriceRequest row = new CheckPriceRequest();

            row.item_id = Int64.Parse(HF_itemid.Value);
            row.quantity = HF_qty.Value.ToString();
            row.uom_id = Int64.Parse(HF_uomid.Value.ToString());
            row.is_consumables = HF_iscons.Value.ToLower() == "true" ? 1 : 0;

            data.Add(row);
        }


        foreach (GridViewRow rows in GvwConsumableOrder.Rows)
        {
            HiddenField HF_itemid = (HiddenField)rows.FindControl("HF_itemid");
            HiddenField HF_qty = (HiddenField)rows.FindControl("HF_qty");
            HiddenField HF_uomid = (HiddenField)rows.FindControl("HF_uomid");
            HiddenField HF_iscons = (HiddenField)rows.FindControl("HF_iscons");

            CheckPriceRequest row = new CheckPriceRequest();

            row.item_id = Int64.Parse(HF_itemid.Value);
            row.quantity = HF_qty.Value.ToString();
            row.uom_id = Int64.Parse(HF_uomid.Value.ToString());
            row.is_consumables = HF_iscons.Value.ToLower() == "true" ? 1 : 0;

            data.Add(row);
        }
        
        return data;

    }

    public List<SingleQueueCheckPrice> formatAngka(List<SingleQueueCheckPrice> listAllPrescription)
    {
        foreach (var templist in listAllPrescription)
        {
            templist.Quantity = Helper.formatDecimal(templist.Quantity);
            templist.SinglePrice = Helper.formatDecimal(templist.SinglePrice);
            templist.Amount = Helper.formatDecimal(templist.Amount);
            templist.DiscountPrice = Helper.formatDecimal(templist.DiscountPrice);
            templist.PayerNet = Helper.formatDecimal(templist.PayerNet);
            templist.PatientNet = Helper.formatDecimal(templist.PatientNet);

            templist.TotalPayerNet = Helper.formatDecimal(templist.TotalPayerNet);
            templist.TotalPatientNet = Helper.formatDecimal(templist.TotalPatientNet);
            templist.RoundingPayerNet = Helper.formatDecimal(templist.RoundingPayerNet);
            templist.RoundingPatientNet = Helper.formatDecimal(templist.RoundingPatientNet);
            templist.TotalPayerNetFinal = Helper.formatDecimal(templist.TotalPayerNetFinal);
            templist.TotalPatientNetFinal = Helper.formatDecimal(templist.TotalPatientNetFinal);

        }
        return listAllPrescription;
    }

    public dynamic isnull(dynamic data)
    {
        dynamic result = 0;
        if (data != null)
        {
            return data;
        }

        return result;
    }

    public string hitungTotalPrice(ResponseCheckPriceResult priceList, string type, string payer)
    {
        decimal result = 0;
        if (type == "TotalPatientNet")
        {
            result =
                decimal.Parse((priceList.Data.drug_price.Count == 0 ? "0" : priceList.Data.drug_price[0].TotalPatientNet).ToString().Replace(".", ","), new System.Globalization.CultureInfo("id-ID")) +
                decimal.Parse((priceList.Data.cpoe_price.Count == 0 ? "0" : priceList.Data.cpoe_price[0].TotalPatientNet).ToString().Replace(".", ","), new System.Globalization.CultureInfo("id-ID")) +
                decimal.Parse((priceList.Data.procedure_price.Count == 0 ? "0" : priceList.Data.procedure_price[0].TotalPatientNet).ToString().Replace(".", ","), new System.Globalization.CultureInfo("id-ID"));

        }
        else if (type == "TotalPayerNet")
        {
            result =
                decimal.Parse((priceList.Data.drug_price.Count == 0 ? "0" : priceList.Data.drug_price[0].TotalPayerNet).ToString().Replace(".", ","), new System.Globalization.CultureInfo("id-ID")) +
                decimal.Parse((priceList.Data.cpoe_price.Count == 0 ? "0" : priceList.Data.cpoe_price[0].TotalPayerNet).ToString().Replace(".", ","), new System.Globalization.CultureInfo("id-ID")) +
                decimal.Parse((priceList.Data.procedure_price.Count == 0 ? "0" : priceList.Data.procedure_price[0].TotalPayerNet).ToString().Replace(".", ","), new System.Globalization.CultureInfo("id-ID"));
        }
        else if (type == "RoundingPatientNet")
        {
            result =
                decimal.Parse((priceList.Data.drug_price.Count == 0 ? "0" : priceList.Data.drug_price[0].RoundingPatientNet).ToString().Replace(".", ","), new System.Globalization.CultureInfo("id-ID")) +
                decimal.Parse((priceList.Data.cpoe_price.Count == 0 ? "0" : priceList.Data.cpoe_price[0].RoundingPatientNet).ToString().Replace(".", ","), new System.Globalization.CultureInfo("id-ID")) +
                decimal.Parse((priceList.Data.procedure_price.Count == 0 ? "0" : priceList.Data.procedure_price[0].RoundingPatientNet).ToString().Replace(".", ","), new System.Globalization.CultureInfo("id-ID"));
        }
        else if (type == "RoundingPayerNet")
        {
            result =
                decimal.Parse((priceList.Data.drug_price.Count == 0 ? "0" : priceList.Data.drug_price[0].RoundingPayerNet).ToString().Replace(".", ","), new System.Globalization.CultureInfo("id-ID")) +
                decimal.Parse((priceList.Data.cpoe_price.Count == 0 ? "0" : priceList.Data.cpoe_price[0].RoundingPayerNet).ToString().Replace(".", ","), new System.Globalization.CultureInfo("id-ID")) +
                decimal.Parse((priceList.Data.procedure_price.Count == 0 ? "0" : priceList.Data.procedure_price[0].RoundingPayerNet).ToString().Replace(".", ","), new System.Globalization.CultureInfo("id-ID"));
        }
        else if (type == "TotalPatientNetFinal")
        {
            result =
                decimal.Parse((priceList.Data.drug_price.Count == 0 ? "0" : priceList.Data.drug_price[0].TotalPatientNetFinal).ToString().Replace(".", ","), new System.Globalization.CultureInfo("id-ID")) +
                decimal.Parse((priceList.Data.cpoe_price.Count == 0 ? "0" : priceList.Data.cpoe_price[0].TotalPatientNetFinal).ToString().Replace(".", ","), new System.Globalization.CultureInfo("id-ID")) +
                decimal.Parse((priceList.Data.procedure_price.Count == 0 ? "0" : priceList.Data.procedure_price[0].TotalPatientNetFinal).ToString().Replace(".", ","), new System.Globalization.CultureInfo("id-ID")) +
                decimal.Parse((priceList.Data.admin_fee.Count == 0 ? "0" : priceList.Data.admin_fee[0].TotalPatientNetFinal).ToString().Replace(".", ","), new System.Globalization.CultureInfo("id-ID"));

            if (payer.ToUpper().Contains("PRIVATE") || payer.ToUpper().Contains("TELECONSULTATION"))
            {
                result = result + decimal.Parse(priceList.Data.ConsultationFee.ToString().Replace(".", ","), new System.Globalization.CultureInfo("id-ID"));
            }
        }
        else if (type == "TotalPayerNetFinal")
        {
            result =
                decimal.Parse((priceList.Data.drug_price.Count == 0 ? "0" : priceList.Data.drug_price[0].TotalPayerNetFinal).ToString().Replace(".", ","), new System.Globalization.CultureInfo("id-ID")) +
                decimal.Parse((priceList.Data.cpoe_price.Count == 0 ? "0" : priceList.Data.cpoe_price[0].TotalPayerNetFinal).ToString().Replace(".", ","), new System.Globalization.CultureInfo("id-ID")) +
                decimal.Parse((priceList.Data.procedure_price.Count == 0 ? "0" : priceList.Data.procedure_price[0].TotalPayerNetFinal).ToString().Replace(".", ","), new System.Globalization.CultureInfo("id-ID")) +
                decimal.Parse((priceList.Data.admin_fee.Count == 0 ? "0" : priceList.Data.admin_fee[0].TotalPayerNetFinal).ToString().Replace(".", ","), new System.Globalization.CultureInfo("id-ID"));

            if (!payer.ToUpper().Contains("PRIVATE") && !payer.ToUpper().Contains("TELECONSULTATION"))
            {
                result = result + decimal.Parse(priceList.Data.ConsultationFee.ToString().Replace(".", ","), new System.Globalization.CultureInfo("id-ID"));
            }
        }

        return decimal.Parse(result.ToString().Replace(".", ","), new System.Globalization.CultureInfo("id-ID")).ToString("#,##0.00"); ;
    }

    protected void ButtonCheckPrice_Click(object sender, EventArgs e)
    {
        log.Info(LogLibrary.Logging("S", "ButtonCheckPrice_Click", HF_UserName.Value, ""));

        try
        {
            //List<CheckPriceRequest> listpres = GetRowListForPrice();

            SingleQueue DataCollection = (SingleQueue)Session["DataSingleQueue"];
            DataCollection.ProcedureDiagOrder = DataCollection.ProcedureDiagOrder.Where(x => x.ProcedureItemName != "Tidak ada").ToList();

            log.Debug(LogLibrary.Logging("S", "clsSingleQueue.CheckPricePrescription", HF_UserName.Value, "params : " + HF_OrgID.Value + "," + HF_PtntID.Value + "," + HF_AdmID.Value + "," + HF_EncID.Value + "," + JsonConvert.SerializeObject(DataCollection)));

            var postPrice = clsSingleQueue.CheckPricePrescription(DataCollection);
            var priceList = JsonConvert.DeserializeObject<ResponseCheckPriceResult>(postPrice.Result);

            log.Debug(LogLibrary.Logging("E", "clsSingleQueue.CheckPricePrescription", HF_UserName.Value, ""));

            
            if (priceList.Status != "Fail" && priceList.Data != null)
            {
                List<SingleQueueCheckPrice> listpricedrug = formatAngka(priceList.Data.drug_price);
                List<SingleQueueCheckPrice> listpricecpoe = formatAngka(priceList.Data.cpoe_price);
                List<SingleQueueCheckPrice> listpriceprocedure = formatAngka(priceList.Data.procedure_price);

                DataTable dtalldrug = Helper.ToDataTable(listpricedrug);

                //bind drug price
                //DataRow[] drdrug = dtalldrug.Select("is_consumables = 0");
                //if (drdrug.Length > 0)
                List<SingleQueueCheckPrice> drdrug = listpricedrug.Where(x => x.SalesItemTypeId == 6).ToList();
                if (drdrug.Count > 0)
                {
                    RepeaterDrugPrice.DataSource = drdrug;
                    RepeaterDrugPrice.DataBind();
                    divconsprice.Visible = true;
                }
                else
                {
                    RepeaterDrugPrice.DataSource = null;
                    RepeaterDrugPrice.DataBind();
                    divdrugprice.Visible = false;
                }

                //bind consumable price
                //DataRow[] drcons = dtalldrug.Select("is_consumables = 1");
                //if (drcons.Length > 0)
                List<SingleQueueCheckPrice> drcons = listpricedrug.Where(x => x.SalesItemTypeId == 7).ToList();
                if (drcons.Count > 0)
                {
                    RepeaterConsPrice.DataSource = drcons;
                    RepeaterConsPrice.DataBind();
                    divconsprice.Visible = true;
                }
                else
                {
                    RepeaterConsPrice.DataSource = null;
                    RepeaterConsPrice.DataBind();
                    divconsprice.Visible = false;
                }

                //bind Lab price
                List<SingleQueueCheckPrice> drlab = listpricecpoe.Where(x => x.SalesItemTypeId == 2).ToList();
                if (drlab.Count > 0)
                {
                    RepeaterLabPrice.DataSource = drlab;
                    RepeaterLabPrice.DataBind();
                    divlabprice.Visible = true;
                }
                else
                {
                    RepeaterLabPrice.DataSource = null;
                    RepeaterLabPrice.DataBind();
                    divlabprice.Visible = false;
                }

                //bind Rad price
                List<SingleQueueCheckPrice> drrad = listpricecpoe.Where(x => x.SalesItemTypeId == 3).ToList();
                if (drrad.Count > 0)
                {
                    RepeaterRadPrice.DataSource = drrad;
                    RepeaterRadPrice.DataBind();
                    divradprice.Visible = true;
                }
                else
                {
                    RepeaterRadPrice.DataSource = null;
                    RepeaterRadPrice.DataBind();
                    divradprice.Visible = false;
                }

                //bind Diagnostik price
                List<SingleQueueCheckPrice> drdiag = listpriceprocedure.Where(x => x.SalesItemTypeId == 4 || x.SalesItemTypeId == 5).ToList();
                if (drdiag.Count > 0)
                {
                    RepeaterDiagPrice.DataSource = drdiag;
                    RepeaterDiagPrice.DataBind();
                    divdiagprice.Visible = true;
                }
                else
                {
                    RepeaterDiagPrice.DataSource = null;
                    RepeaterDiagPrice.DataBind();
                    divdiagprice.Visible = false;
                }

                //LabelPatientSubtotal.Text = decimal.Parse(dtalldrug.Rows[0]["TotalPatientNet"].ToString().Replace(".", ","), new System.Globalization.CultureInfo("id-ID")).ToString("#,##0.00");
                //LabelPayerSubtotal.Text = decimal.Parse(dtalldrug.Rows[0]["TotalPayerNet"].ToString().Replace(".", ","), new System.Globalization.CultureInfo("id-ID")).ToString("#,##0.00");
                //LabelPatientRounding.Text = decimal.Parse(dtalldrug.Rows[0]["RoundingPatientNet"].ToString().Replace(".", ","), new System.Globalization.CultureInfo("id-ID")).ToString("#,##0.00");
                //LabelPayerRounding.Text = decimal.Parse(dtalldrug.Rows[0]["RoundingPayerNet"].ToString().Replace(".", ","), new System.Globalization.CultureInfo("id-ID")).ToString("#,##0.00");
                //LabelPatientTotal.Text = decimal.Parse(dtalldrug.Rows[0]["TotalPatientNetFinal"].ToString().Replace(".", ","), new System.Globalization.CultureInfo("id-ID")).ToString("#,##0.00");
                //LabelPayerTotal.Text = decimal.Parse(dtalldrug.Rows[0]["TotalPayerNetFinal"].ToString().Replace(".", ","), new System.Globalization.CultureInfo("id-ID")).ToString("#,##0.00");

                if (DataCollection.patientHeader[0].payerName.ToUpper().Contains("PRIVATE") || DataCollection.patientHeader[0].payerName.ToUpper().Contains("TELECONSULTATION"))
                {
                    LabelConsultationFeePatient.Text = decimal.Parse(priceList.Data.ConsultationFee.ToString().Replace(".", ","), new System.Globalization.CultureInfo("id-ID")).ToString("#,##0.00");
                    LabelConsultationFeePayer.Text = decimal.Parse("0", new System.Globalization.CultureInfo("id-ID")).ToString("#,##0.00");
                }
                else
                {
                    LabelConsultationFeePatient.Text = decimal.Parse("0", new System.Globalization.CultureInfo("id-ID")).ToString("#,##0.00");
                    LabelConsultationFeePayer.Text = decimal.Parse(priceList.Data.ConsultationFee.ToString().Replace(".", ","), new System.Globalization.CultureInfo("id-ID")).ToString("#,##0.00");
                }

                LabelAdminFeePatient.Text = decimal.Parse((priceList.Data.admin_fee.Count == 0 ? "0" : priceList.Data.admin_fee[0].TotalPatientNetFinal).ToString().Replace(".", ","), new System.Globalization.CultureInfo("id-ID")).ToString("#,##0.00");
                LabelAdminFeePayer.Text = decimal.Parse((priceList.Data.admin_fee.Count == 0 ? "0" : priceList.Data.admin_fee[0].TotalPayerNetFinal).ToString().Replace(".", ","), new System.Globalization.CultureInfo("id-ID")).ToString("#,##0.00");

                LabelPatientSubtotal.Text = hitungTotalPrice(priceList, "TotalPatientNet", DataCollection.patientHeader[0].payerName);
                LabelPayerSubtotal.Text = hitungTotalPrice(priceList, "TotalPayerNet", DataCollection.patientHeader[0].payerName);
                LabelPatientRounding.Text = hitungTotalPrice(priceList, "RoundingPatientNet", DataCollection.patientHeader[0].payerName);
                LabelPayerRounding.Text = hitungTotalPrice(priceList, "RoundingPayerNet", DataCollection.patientHeader[0].payerName);
                LabelPatientTotal.Text = hitungTotalPrice(priceList, "TotalPatientNetFinal", DataCollection.patientHeader[0].payerName);
                LabelPayerTotal.Text = hitungTotalPrice(priceList, "TotalPayerNetFinal", DataCollection.patientHeader[0].payerName);

                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modalCheckPrice", "$('#modalCheckPrice').modal();", true);
            }
            else
            {
                string apimessage = (priceList.Status.Replace("'", "\'") + " : " + priceList.Message.Replace("'", "\'"));
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alertCheckPrice", "alert('No Data! " + apimessage + "')", true);
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alertCheckPrice", "alert('Calculate Price Error! "+ ex.Message.ToString() + "')", true);
            log.Error(LogLibrary.Error("ButtonCheckPrice_Click", HF_UserName.Value, ex.Message.ToString()));
        }

        log.Info(LogLibrary.Logging("E", "ButtonCheckPrice_Click", HF_UserName.Value, ""));
    }

    public void load_QueueLinelist(string hospital_id)
    {
        log.Info(LogLibrary.Logging("S", "load_QueueLinelist", HF_UserName.Value, ""));

        try
        {
            log.Debug(LogLibrary.Logging("S", "clsSingleQueue.getQueueLineData", HF_UserName.Value, "Params: " + hospital_id));

            var json_line = clsSingleQueue.getQueueLineDataNew(hospital_id); 
            var data_line = JsonConvert.DeserializeObject<ResultQueueLineNew>(json_line.Result.ToString());
            Session["DataQueueLine"] = data_line;

            log.Debug(LogLibrary.Logging("E", "clsSingleQueue.getQueueLineData", HF_UserName.Value, ""));

            List<QueueLineNew> listlinePharmacy = new List<QueueLineNew>();
            List<QueueLineNew> listlineCashier = new List<QueueLineNew>();
            QueueLineNew x = new QueueLineNew();
            x.queue_line_hospital_id = Guid.Empty;
            x.visit_type_floor_name = "-select-";

            listlinePharmacy.Add(x);
            listlinePharmacy.AddRange(data_line.list.Where(p => p.category_visit_type == "3").ToList());

            listlineCashier.Add(x);
            listlineCashier.AddRange(data_line.list.Where(c => c.category_visit_type == "2").ToList());

            DataTable tablelinePharmacy = Helper.ToDataTable(listlinePharmacy);
            DDl_PilihPharmacy.DataSource = tablelinePharmacy;
            DDl_PilihPharmacy.DataTextField = "visit_type_floor_name";
            DDl_PilihPharmacy.DataValueField = "visit_type_hospital_id";
            DDl_PilihPharmacy.DataBind();

            DataTable tablelineCashier = Helper.ToDataTable(listlineCashier);
            DDl_PilihCashier.DataSource = tablelineCashier;
            DDl_PilihCashier.DataTextField = "visit_type_floor_name";
            DDl_PilihCashier.DataValueField = "visit_type_hospital_id";
            DDl_PilihCashier.DataBind();
        }
        catch (Exception ex)
        {
            log.Error(LogLibrary.Error("load_QueueLinelist", HF_UserName.Value, ex.Message.ToString()));
        }

        log.Info(LogLibrary.Logging("E", "load_QueueLinelist", HF_UserName.Value, ""));
    }

    public static string GetLocalIPAddress()
    {
        var host = Dns.GetHostEntry(Dns.GetHostName());
        foreach (var ip in host.AddressList)
        {
            if (ip.AddressFamily == AddressFamily.InterNetwork)
            {
                return ip.ToString();
            }
        }
        throw new Exception("No network adapters with an IPv4 address in the system!");
    }

    public string GenerateURLPrint(string printtype, string org_id, string adm_id, string enc_id, string ptn_id, string printby, string doktername, string pagesoapid)
    {
        string IP = GetLocalIPAddress();
        string URL = "";

        if (printtype == "SHORTSOAP")
        {
            URL = "http://" + IP + "/printingemr?printtype=MedicalResumeLite&OrganizationId=" + org_id + "&AdmissionId=" + adm_id + "&EncounterId=" + enc_id + "&PatientId=" + ptn_id + "&PrintBy=" + printby + "&DoctorBy=" + doktername;
        }
        else if (printtype == "LONGSOAP")
        {
            URL = "http://" + IP + "/printingemr?printtype=MedicalResume&OrganizationId=" + org_id + "&AdmissionId=" + adm_id + "&EncounterId=" + enc_id + "&PatientId=" + ptn_id + "&PageSOAP=" + pagesoapid + "&PrintBy=" + printby;
        }
        else if (printtype == "LAB")
        {
            URL = "http://" + IP + "/printingemr?printtype=OrderLab&OrganizationId=" + org_id + "&AdmissionId=" + adm_id + "&EncounterId=" + enc_id + "&PatientId=" + ptn_id + "&IsLabRad=True" + "&PrintBy=" + printby;
        }
        else if (printtype == "RAD")
        {
            URL = "http://" + IP + "/printingemr?printtype=OrderRad&OrganizationId=" + org_id + "&AdmissionId=" + adm_id + "&EncounterId=" + enc_id + "&PatientId=" + ptn_id + "&IsLabRad=True" + "&PrintBy=" + printby;
        }

        return URL;
    }

    protected void ButtonSumbit_Click(object sender, EventArgs e)
    {
        log.Info(LogLibrary.Logging("S", "ButtonSumbit_Click", HF_UserName.Value, ""));

        if (DDl_PilihCashier.SelectedIndex == 0 || DDl_PilihPharmacy.SelectedIndex == 0)
        {
            //div_notifmsg.Style.Add("display", "block");
            //LabelNotifMsg.Text = "Please Select Cashier dan Pharmacy Counter First!";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alertCheckPrice", "alert('Please Select Cashier and Pharmacy Counter First!')", true);
        }
        else
        {
            ResultQueueLineNew QL = (ResultQueueLineNew)Session["DataQueueLine"];
            QueueLineNew selectedC = QL.list.Where(x => x.visit_type_hospital_id == DDl_PilihCashier.SelectedValue).FirstOrDefault();
            QueueLineNew selectedP = QL.list.Where(x => x.visit_type_hospital_id == DDl_PilihPharmacy.SelectedValue).FirstOrDefault();

            SingleQueue DataCollection = (SingleQueue)Session["DataSingleQueue"];
            DataCollection.patientHeader[0].queueLineHospitalId = selectedC.queue_line_hospital_id.ToString();
            DataCollection.patientHeader[0].visitTypeHospitalId = Guid.Parse(selectedC.visit_type_hospital_id);

            if (RadioDrug1.Checked == true)
            {
                DataCollection.patientHeader[0].IsTakeAll = true;
            }
            else if (RadioDrug2.Checked == true)
            {
                DataCollection.patientHeader[0].IsTakeAll = false;
            }

            foreach (PrescriptionOrderSQ press_item in DataCollection.prescriptionOrder)
            {
                press_item.queueLineHospitalId = selectedP.queue_line_hospital_id.ToString();
                press_item.visitTypeHospitalId = Guid.Parse(selectedP.visit_type_hospital_id);
            }
            foreach (CompoundHeaderSQ racikanH_item in DataCollection.compoundHeader)
            {
                racikanH_item.queueLineHospitalId = selectedP.queue_line_hospital_id.ToString();
                racikanH_item.visitTypeHospitalId = Guid.Parse(selectedP.visit_type_hospital_id);
            }
            foreach (CompoundDetailSQ racikanD_item in DataCollection.compoundDetail)
            {
                racikanD_item.queueLineHospitalId = selectedP.queue_line_hospital_id.ToString();
                racikanD_item.visitTypeHospitalId = Guid.Parse(selectedP.visit_type_hospital_id);
            }
            foreach (CPOEORderSQ labrad_item in DataCollection.cpoeOrder)
            {
                labrad_item.queueLineHospitalId = selectedP.queue_line_hospital_id.ToString();
                labrad_item.visitTypeHospitalId = Guid.Parse(selectedP.visit_type_hospital_id);
            }

            try
            {
                log.Debug(LogLibrary.Logging("S", "clsSingleQueue.postDataSingleQueue", HF_UserName.Value, "params : " + HF_UserID.Value + "," + HF_UserName.Value + "," + JsonConvert.SerializeObject(DataCollection)));

                var postData = clsSingleQueue.postDataSingleQueue(long.Parse(HF_UserID.Value), HF_UserName.Value, DataCollection);
                var dataList = JsonConvert.DeserializeObject<dynamic>(postData.Result);
                var Status = dataList.Property("status").Value.ToString();
                var Message = dataList.Property("message").Value.ToString();

                log.Debug(LogLibrary.Logging("E", "clsSingleQueue.postDataSingleQueue", HF_UserName.Value, "result : " + Status + ":" + Message));

                if (Status == "Fail")
                {
                    //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "post", "alert('Failed to Submit');", true);
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modalresultopen", "openmodal_resumeresult();", true);
                    LabelSumbitResult.Text = "Submit Fail , " + Message;
                    div_modalsuccess.Visible = false;
                    div_modalfail.Visible = true;
                }
                else
                {
                    ResetResult();
                    //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "post", "alert('Submit Success');", true);
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modalresultopen", "openmodal_resumeresult();", true);
                    LabelSumbitResult.Text = "Submit Success";
                    div_modalsuccess.Visible = true;
                    div_modalfail.Visible = false;
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modalresultopen", "openmodal_resumeresult();", true);
                LabelSumbitResult.Text = "Error , " + ex.ToString();
                div_modalsuccess.Visible = false;
                div_modalfail.Visible = true;

                log.Error(LogLibrary.Error("ButtonSumbit_Click", HF_UserName.Value, ex.Message.ToString()));
            }

            //div_notifmsg.Style.Add("display", "none");
            //LabelNotifMsg.Text = "";
        }

        log.Info(LogLibrary.Logging("E", "ButtonSumbit_Click", HF_UserName.Value, ""));
    }

    protected void ButtonOKResult_Click(object sender, EventArgs e)
    {
        ResetResult();
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modalresultclose", "closemodal_resumeresult();", true);
    }
}