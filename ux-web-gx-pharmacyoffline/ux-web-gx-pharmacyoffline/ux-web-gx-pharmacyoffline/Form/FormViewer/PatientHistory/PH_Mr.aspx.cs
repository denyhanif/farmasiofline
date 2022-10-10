using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using static SOAPRevisionHistory;

public partial class Form_FormViewer_PatientHistory_PH_Mr : System.Web.UI.Page
{
    HiddenField organizationId;
    HiddenField patientId;
    HiddenField admId;
    HiddenField encId;
    HiddenField printBY;
    HiddenField doctorName;
    HiddenField pageSOAP;

    int flag = 0;

    public string QSpatientid;

    private static int countPage = 0;
    private static int currentPage = 0;
    private static int pageSize = 10;
    private static bool resetDdlPage = false;

    protected void Page_Load(object sender, EventArgs e)
    {
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
            }

            if (Request.QueryString["PatientId"] != null)
            {

                txtDateFromNew.Text = DateTime.Now.AddMonths(-6).ToString("dd MMM yyyy");
                txtToDateNew.Text = DateTime.Now.ToString("dd MMM yyyy");
                txtDateFromNew.Attributes.Add("ReadOnly", "ReadOnly");
                txtToDateNew.Attributes.Add("ReadOnly", "ReadOnly");

                List<string> listType = new List<string>();
                var dataType = clsPatientHistory.getDiseaseClassificationType(Request.QueryString["OrganizationId"]);
                var Response = JsonConvert.DeserializeObject<ResultClassificationType>(dataType.Result.ToString());
                listType.Add("ALL");
                listType.AddRange(Response.list);
                dropType.DataSource = listType;
                dropType.DataBind();

                //Pagination
                currentPage = 1;
                resetDdlPage = true;
                //string doctorId = Request.QueryString["DoctorId"] == null ? "0" : Request.QueryString["DoctorId"];

                getData("ALL", "0");
            }
        }
    }

    protected void getData(string Type, string doctorId)
    {
        string rspn_status = "", rspn_message = "";
        Session[Helper.sessionPatientHistoryLite] = null;

        string en = Request.QueryString["en"];
        if (en != null)
        {
            QSpatientid = Helper.Decrypt(Request.QueryString["PatientId"]);
        }
        else
        {
            QSpatientid = Request.QueryString["PatientId"];
        }

        //var dataPatientHistory = clsPatientHistory.getPatientHistoryOPDIPD(Request.QueryString["OrganizationId"], QSpatientid, DateTime.Parse(txtDateFromNew.Text), DateTime.Parse(txtToDateNew.Text), Type, Request.QueryString["DoctorId"]);
        var dataPatientHistory = clsPatientHistory.getPatientHistoryOPDIPDUsePagination(Request.QueryString["OrganizationId"], QSpatientid, Type, pageSize, currentPage, DateTime.Parse(txtDateFromNew.Text), DateTime.Parse(txtToDateNew.Text), doctorId);
        var Response = JsonConvert.DeserializeObject<ResponsePatientHistoryAll>(dataPatientHistory.Result);
       
        rspn_status = Response.Status;
        rspn_message = Response.Message;

        if (rspn_status.ToString().ToLower() != "Fail".ToLower())
        {
            //var patientIdOwned = JsonConvert.DeserializeObject<ResponsePatientHistoryAll>(dataPatientHistory.Result.ToString());
            List<PatientHistoryAll> listPatientHistoryLite = new List<PatientHistoryAll>();
            List<DoctorList> doctorList = new List<DoctorList>();
            //List<String> listDoctor = new List<String>();

            //listPatientHistoryLite = patientIdOwned.Data;
            listPatientHistoryLite = Response.Data.patientHistory;
            doctorList = Response.Data.doctorList;
            countPage = Response.Data.countPage;

            //listDoctor = (from a in listPatientHistoryLite
            //              select a.DoctorName
            //              ).Distinct().ToList();

            //if (listPatientHistoryLite[0].CountData > 20)
            //{
            //    alertNotif.Visible = true;
            //}
            //else
            //{
            //    alertNotif.Visible = false;
            //}

            //dropDoctor.DataSource = listDoctor;
            //dropDoctor.DataBind();
            //dropDoctor.Items.Insert(0, new ListItem("Dokter"));

            if (dropDoctor.Items.Count <= 1)
            {
                for (int i = 0; i < doctorList.Count; i++)
                {
                    ListItem ls = new ListItem();
                    ls.Value = doctorList[i].doctorId.ToString();
                    ls.Text = doctorList[i].doctorName;
                    dropDoctor.Items.Add(ls);
                }
            }

            //foreach (PatientHistoryLite A in listPatientHistoryLite)
            //{
            //    var result = Regex.Replace(
            //    A.Prescription,                                  // input
            //    @"Doctor",                                      // word to match
            //    @"<b>$0</b>",                                  // "wrap match in bold tags"
            //    RegexOptions.IgnoreCase);
            //    A.Prescription = result;
            //}

            DataTable dtworklist = Helper.ToDataTable(listPatientHistoryLite);

            Session[Helper.sessionPatientHistoryLite] = dtworklist;

            //gvw_detail.DataSource = dtworklist;
            //gvw_detail.DataBind();
            RptListPH.DataSource = dtworklist;
            RptListPH.DataBind();

            List<PatientHistoryAll> listPatientHistoryLiteOPD = listPatientHistoryLite.Where(x => x.AdmissionTypeName == "Outpatient").ToList();
            if (listPatientHistoryLiteOPD.Count > 0)
            {
                HFdashOrgID.Value = listPatientHistoryLiteOPD[0].OrganizationId.ToString();
                HFdashPtnID.Value = listPatientHistoryLiteOPD[0].PatientId.ToString();
                HFdashAdmID.Value = listPatientHistoryLiteOPD[0].AdmissionId.ToString();
                HFdashEncID.Value = listPatientHistoryLiteOPD[0].EncounterId.ToString();
                HFdashDocID.Value = listPatientHistoryLiteOPD[0].DoctorId.ToString();

                div_linkdashboard.Visible = true;
            }

            DivNoData_PH.Visible = false;
        }

        else
        {
            //gvw_detail.DataSource = null;
            //gvw_detail.DataBind();
            RptListPH.DataSource = null;
            RptListPH.DataBind();

            div_linkdashboard.Visible = false;

            DivNoData_PH.Visible = true;
        }

        setPagination();
    }

    protected void txtFromDate_TextChanged(object sender, EventArgs e)
    {
        dropPageOf.SelectedIndex = 0;
        currentPage = 1;
        resetDdlPage = true;

        getData(dropType.SelectedValue, dropDoctor.SelectedValue);
    }

    protected void txtToDateNew_TextChanged(object sender, EventArgs e)
    {
        dropPageOf.SelectedIndex = 0;
        currentPage = 1;
        resetDdlPage = true;

        getData(dropType.SelectedValue, dropDoctor.SelectedValue);
    }

    protected void btnPatientHistory_Click(object sender, ImageClickEventArgs e)
    {

        int selRowIndex = ((RepeaterItem)(((ImageButton)sender).Parent)).ItemIndex;

        organizationId = (HiddenField)RptListPH.Items[selRowIndex].FindControl("hdnOrganization");
        patientId = (HiddenField)RptListPH.Items[selRowIndex].FindControl("hdnPatientID");
        admId = (HiddenField)RptListPH.Items[selRowIndex].FindControl("hdnAdmId");
        encId = (HiddenField)RptListPH.Items[selRowIndex].FindControl("hdnEncId");
        pageSOAP = (HiddenField)RptListPH.Items[selRowIndex].FindControl("hdnPageSoap");

        LabelModalResumePatient.Text = "Patient History";
        string url = "~/Form/FormViewer/MedicalResumePatient.aspx?org_id=" + organizationId.Value + "&ptn_id=" + patientId.Value + "&adm_id=" + admId.Value + "&enc_id=" + encId.Value + "&pagesoap_id=" + pageSOAP.Value + "&headertype=0" + "&username=" + hfPrintBY.Value;
        IframeMedicalResumePatient.Src = url;
        updatePatientHistory.Update();
        ScriptManager.RegisterStartupScript(this, this.GetType(), "openPatientHistoryModal", "openPatientHistoryModal()", true);
    }

    protected void btnPatientDocument_ipd_Click(object sender, ImageClickEventArgs e)
    {

        int selRowIndex = ((RepeaterItem)(((ImageButton)sender).Parent)).ItemIndex;

        HiddenField urlattachment = (HiddenField)RptListPH.Items[selRowIndex].FindControl("hdnDocumentViewer_ipd");

        LabelModalResumePatient.Text = "Document Viewer";
        IframeMedicalResumePatient.Src = urlattachment.Value;
        updatePatientHistory.Update();
        ScriptManager.RegisterStartupScript(this, this.GetType(), "openPatientHistoryModal", "openPatientHistoryModal()", true);
    }

    protected void btnPrintSOAP_Click(object sender, ImageClickEventArgs e)
    {
        int selRowIndex = ((RepeaterItem)(((ImageButton)sender).Parent)).ItemIndex;

        encId = (HiddenField)RptListPH.Items[selRowIndex].FindControl("hdnEncId");
        organizationId = (HiddenField)RptListPH.Items[selRowIndex].FindControl("hdnOrganization");
        admId = (HiddenField)RptListPH.Items[selRowIndex].FindControl("hdnAdmId");
        patientId = (HiddenField)RptListPH.Items[selRowIndex].FindControl("hdnPatientID");
        hfPrintBY.Value = Request.QueryString["PrintBy"];
        doctorName = (HiddenField)RptListPH.Items[selRowIndex].FindControl("hdnDoctorName");

        var localIPAdress = "";

        localIPAdress = GetLocalIPAddress();
        //localIPAdress = "10.83.254.38"; //HARD CODE

        ScriptManager.RegisterStartupScript(
        this, GetType(), "OpenWindow", "window.open('http://" + localIPAdress + "/printingemr?printtype=MedicalResumeLite&OrganizationId=" + organizationId.Value + "&AdmissionId=" + admId.Value + "&EncounterId=" + encId.Value + "&PatientId=" + patientId.Value + "&PrintBy=" + hfPrintBY.Value.ToString() + "&DoctorBy=" + doctorName.Value.ToString() + "','_blank');", true);
    }

    protected void btnPrintSOAPlong_Click(object sender, ImageClickEventArgs e)
    {
        int selRowIndex = ((RepeaterItem)(((ImageButton)sender).Parent)).ItemIndex;

        encId = (HiddenField)RptListPH.Items[selRowIndex].FindControl("hdnEncId");
        organizationId = (HiddenField)RptListPH.Items[selRowIndex].FindControl("hdnOrganization");
        admId = (HiddenField)RptListPH.Items[selRowIndex].FindControl("hdnAdmId");
        patientId = (HiddenField)RptListPH.Items[selRowIndex].FindControl("hdnPatientID");
        pageSOAP = (HiddenField)RptListPH.Items[selRowIndex].FindControl("hdnPageSoap");
        hfPrintBY.Value = Request.QueryString["PrintBy"];

        var localIPAdress = "";

        localIPAdress = GetLocalIPAddress();
        //localIPAdress = "10.83.254.38"; //HARD CODE

        ScriptManager.RegisterStartupScript(
        this, GetType(), "OpenWindow", "window.open('http://" + localIPAdress + "/printingemr?printtype=MedicalResume&OrganizationId=" + organizationId.Value + "&AdmissionId=" + admId.Value + "&EncounterId=" + encId.Value + "&PatientId=" + patientId.Value + "&PageSOAP=" + pageSOAP.Value + "&PrintBy=" + hfPrintBY.Value.ToString() + "','_blank');", true);
    }

    protected void btnPrintReferral_Click(object sender, ImageClickEventArgs e)
    {
        int selRowIndex = ((RepeaterItem)(((ImageButton)sender).Parent)).ItemIndex;

        encId = (HiddenField)RptListPH.Items[selRowIndex].FindControl("hdnEncId");
        organizationId = (HiddenField)RptListPH.Items[selRowIndex].FindControl("hdnOrganization");
        admId = (HiddenField)RptListPH.Items[selRowIndex].FindControl("hdnAdmId");
        patientId = (HiddenField)RptListPH.Items[selRowIndex].FindControl("hdnPatientID");
        pageSOAP = (HiddenField)RptListPH.Items[selRowIndex].FindControl("hdnPageSoap");
        hfPrintBY.Value = Request.QueryString["PrintBy"];

        var localIPAdress = "";

        localIPAdress = GetLocalIPAddress();
        //localIPAdress = "10.83.254.38"; //HARD CODE

        ScriptManager.RegisterStartupScript(
        this, GetType(), "OpenWindow", "window.open('http://" + localIPAdress + "/printingemr?printtype=ReferralResume&OrganizationId=" + organizationId.Value + "&AdmissionId=" + admId.Value + "&EncounterId=" + encId.Value + "&PatientId=" + patientId.Value + "&PageSOAP=" + pageSOAP.Value + "&PrintBy=" + hfPrintBY.Value.ToString() + "','_blank');", true);
    }

    protected void btnPrintRawatInap_Click(object sender, ImageClickEventArgs e)
    {
        int selRowIndex = ((RepeaterItem)(((ImageButton)sender).Parent)).ItemIndex;

        encId = (HiddenField)RptListPH.Items[selRowIndex].FindControl("hdnEncId");
        organizationId = (HiddenField)RptListPH.Items[selRowIndex].FindControl("hdnOrganization");
        admId = (HiddenField)RptListPH.Items[selRowIndex].FindControl("hdnAdmId");
        patientId = (HiddenField)RptListPH.Items[selRowIndex].FindControl("hdnPatientID");
        pageSOAP = (HiddenField)RptListPH.Items[selRowIndex].FindControl("hdnPageSoap");
        hfPrintBY.Value = Request.QueryString["PrintBy"];

        var localIPAdress = "";

        localIPAdress = GetLocalIPAddress();
        //localIPAdress = "10.83.254.38"; //HARD CODE

        ScriptManager.RegisterStartupScript(
        this, GetType(), "OpenWindow", "window.open('http://" + localIPAdress + "/printingemr?printtype=RawatInap&OrganizationId=" + organizationId.Value + "&AdmissionId=" + admId.Value + "&EncounterId=" + encId.Value + "&PatientId=" + patientId.Value + "&PageSOAP=" + pageSOAP.Value + "&PrintBy=" + hfPrintBY.Value.ToString() + "','_blank');", true);
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

    

    protected void btnFilterOnClick(object sender, EventArgs e)
    {
        //if (Session[Helper.sessionPatientHistoryLite] != null)
        //{
        //    DataTable dtworklist = Session[Helper.sessionPatientHistoryLite] as DataTable;
        //    DataTable dtfiltered = new DataTable();

        //    if (dtworklist.Select("DoctorName = '" + dropDoctor.SelectedValue + "'").Count() > 0)
        //    {
        //        dtfiltered = dtworklist.Select("DoctorName = '" + dropDoctor.SelectedValue + "'").CopyToDataTable();
        //        //gvw_detail.DataSource = dtfiltered;
        //        //gvw_detail.DataBind();
        //        RptListPH.DataSource = dtfiltered;
        //        RptListPH.DataBind();
        //    }
        //    else
        //    {
        //        //gvw_detail.DataSource = dtworklist;
        //        //gvw_detail.DataBind();
        //        RptListPH.DataSource = dtworklist;
        //        RptListPH.DataBind();
        //    }
        //}

        //Filter Doctor Direct to API
        dropPageOf.SelectedIndex = 0;
        currentPage = 1;
        resetDdlPage = true;

        getData(dropType.SelectedValue, dropDoctor.SelectedValue);
    }

    //protected void btnPrintOnClick(object sender, EventArgs e)
    //{
    //    List<AllDataForPrint> patientSOAPxHeader = GetRowListSOAPxHeader();

    //    List<AllDataForPrint> patientSOAPxHeaderFiltered = (from a in patientSOAPxHeader
    //                                                        where a.isSelected == 1
    //                                                        select a
    //                                                        ).Distinct().ToList();

    //    DataTable dtPatientSOAP = Helper.ToDataTable(patientSOAPxHeaderFiltered);
    //    Session[Guid.NewGuid().ToString() + Helper.SessionAllDataForPrint] = patientSOAPxHeaderFiltered;
    //}

    //protected List<AllDataForPrint> GetRowListSOAPxHeader()
    //{
    //    int i = 1;
    //    List<AllDataForPrint> data = new List<AllDataForPrint>();
    //    try
    //    {
    //        foreach (GridViewRow rows in gvw_detail.Rows)
    //        {

    //            Label DoctorName = (Label)rows.FindControl("txtDokter");
    //            HiddenField PatientName = (HiddenField)rows.FindControl("hdnPatientName");
    //            HiddenField OrgCode = (HiddenField)rows.FindControl("hdnOrgCode");
    //            HiddenField Birthdate = (HiddenField)rows.FindControl("hdnTTLumur");
    //            HiddenField Gender = (HiddenField)rows.FindControl("hdnJK");
    //            HiddenField localMRno = (HiddenField)rows.FindControl("hdnLocalMrNo");

    //            HiddenField OrgID = (HiddenField)rows.FindControl("hdnOrganization");
    //            HiddenField AdmID = (HiddenField)rows.FindControl("hdnAdmId");
    //            HiddenField EncID = (HiddenField)rows.FindControl("hdnEncId");

    //            TextBox S = (TextBox)rows.FindControl("txtsubjective");
    //            TextBox O = (TextBox)rows.FindControl("txtobjective");
    //            TextBox A = (TextBox)rows.FindControl("txtDiagnosa");
    //            TextBox P = (TextBox)rows.FindControl("txtTindakan");


    //            AllDataForPrint row = new AllDataForPrint();



    //            row.DoctorName = DoctorName.Text;
    //            row.PatientName = PatientName.Value;
    //            row.OrgCode = OrgCode.Value;
    //            row.Birthdate = Birthdate.Value;
    //            row.Gender = Gender.Value;
    //            row.localMRno = localMRno.Value;

    //            row.EncounterId = Guid.Parse(EncID.Value);
    //            row.AdmissionId = long.Parse(AdmID.Value);
    //            row.OrganizationId = long.Parse(OrgID.Value);

    //            row.Subjective = S.Text;
    //            row.Objective = O.Text;
    //            row.Diagnosis = A.Text;
    //            row.PlanningProcedure = P.Text;

    //            row.key = i;
    //            i++;
    //            data.Add(row);
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //    }
    //    return data;
    //}

    protected void labResult_Click(object sender, EventArgs e)
    {
        ImageButton ibtn = sender as ImageButton;
        string admissionId = hf_admiss_id.Value;
        String patientID = hf_patient_id.Value;
        var varResult = clsCommon.GetPatientHeader(Int64.Parse(patientID), hf_ticket_patient.Value.Replace("/", "")).Result;
        var JsongetPatientHistory = JsonConvert.DeserializeObject<ResultPatientHeader>(varResult);

        initializevalueHeader(JsongetPatientHistory.header);
        List<LaboratoryResult> listlaboratory = new List<LaboratoryResult>();
        var dataLaboratory = clsResult.getLaboratoryResult(admissionId.ToString());
        var JsonLaboratory = JsonConvert.DeserializeObject<ResultLaboratoryResult>(dataLaboratory.Result.ToString());
        listlaboratory = new List<LaboratoryResult>();
        listlaboratory = JsonLaboratory.list;
        initializevalueLab(listlaboratory);

        ScriptManager.RegisterStartupScript(this, this.GetType(), "openmodal", "openLabModal()", true);

    }

    protected void radResult_Click(object sender, EventArgs e)
    {
        if (hf_admiss_id.Value != "")
        {
            try
            {
                var varResult = clsCommon.GetPatientHeader(Int64.Parse(hf_patient_id.Value), hf_ticket_patient.Value.Replace("/", "")).Result;
                var JsongetPatientHistory = JsonConvert.DeserializeObject<ResultPatientHeader>(varResult);

                initializevalueHeaderRad(JsongetPatientHistory.header);

                var dataAdmissionDetail = clsResult.getRadResultAdmissionDetail(hf_admiss_id.Value.ToString());
                var JsonAdmissionDetail = JsonConvert.DeserializeObject<ResultRadiologyByWeek>(dataAdmissionDetail.Result);

                List<radiologyByWeek> listAdmissionDetail = JsonAdmissionDetail.list;

                initializevalueRad(listAdmissionDetail);
                ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "openRadModal", "openRadModal();", true);

            }
            catch (Exception ex)
            {
                LogLibrary.Error("getRadResultAdmissionDetail ", hf_admiss_id.Value, ex.Message.ToString());
            }

            hf_admiss_id.Value = "";
            img_noDataRad.Style.Add("display", "none");
        }
        else
        {
            img_noDataRad.Style.Add("display", "");
        }

        ScriptManager.RegisterStartupScript(this, this.GetType(), "openmodal", "openRadModal()", true);

    }

    protected void btnShowDocument_Click(object sender, EventArgs e)
    {
        ImageButton ibtn = sender as ImageButton;
        string admissionId = hf_admiss_id.Value;
        String patientID = hf_patient_id.Value;
        string organizationID = HFdashOrgID.Value;

        List<DocumentResult> listdocument = new List<DocumentResult>();
        var dataDocument = clsResult.getDocResultAdmission(patientID, admissionId, organizationID);
        var JsonDocument = JsonConvert.DeserializeObject<ResultDocumentResult>(dataDocument.Result.ToString());

        listdocument = new List<DocumentResult>();
        if(JsonDocument != null)
            listdocument = JsonDocument.list;

        Session[Helper.SessionDocumentData] = listdocument;
        fillDocResult(listdocument);

        ScriptManager.RegisterStartupScript(this, this.GetType(), "openmodal", "openDocModal()", true);
    }

    public void fillDocResult(List<DocumentResult> listDocumentResult)
    {
        StringBuilder documentInnerHTML = new StringBuilder();

        //string imagePath = ResolveClientUrl("~/Images/ic_newtab_blue.svg");
        if (listDocumentResult.Count != 0)
        {
            string openmodal = "function OpenDocumentImage(urlfile) {" +
                "window.open(urlfile)" +
                "}";
            ScriptManager.RegisterClientScriptBlock(this, GetType(), Guid.NewGuid().ToString(), openmodal, true);


            listDocumentResult = listDocumentResult.OrderByDescending(x => x.AdmissionDate).ToList();

            //code here
            foreach (DocumentResult datadoc in listDocumentResult)
            {
                string iconimg = datadoc.Path.ToUpper().Contains(".PDF") ? "../../../Images/Icon/pdficon.png" : "../../../Images/Icon/jpgicon.png";
                documentInnerHTML.Append("<div style=\"width:100%; cursor: pointer;\">" +
                    "<Label style=\"font-weight:bold;font-size: 20px;\">" + datadoc.image_type_value + "</Label>" +
                    "<div style=\"width: 100 %;box-shadow: 3px 3px 6px 3px #888888;\" onclick=\"OpenDocumentImage('" + datadoc.Path.Replace("\\", "/") + "')\">" +
                    "<img src=\"" + iconimg + "\"style=\"width:6%;padding-top: 5px;padding-bottom: 5px;\">" +
                    "<label>" + datadoc.DocumentName + "</label>" +
                    "</div><br/><br/>" +
                    "<label style=\"font-weight:bold;\">Remarks(Catatan)</label>" + 
                    "<textarea rows=\"4\" cols=\"50\" style=\"width:100%; max-width:100%;\" readonly>" + datadoc.image_remark + "</textarea>" +
                    "</div><br/><br/>");
            }

            div_Document_detail.InnerHtml = documentInnerHTML.ToString();

            img_noDatadoc.Style.Add("display", "none");
        }
        else
        {
            div_Document_detail.InnerHtml = documentInnerHTML.ToString();
            img_noDatadoc.Style.Add("display", "block");
        }
    }


    public void initializevalueHeader(PatientHeader model)
    {
        if (model.Gender == 1)
        {
            Image1.ImageUrl = "~/Images/Dashboard/ic_PatientMale_Big.svg";
            imgSex.ImageUrl = "~/Images/Icon/ic_Male.svg";
        }
        else if (model.Gender == 2)
        {
            Image1.ImageUrl = "~/Images/Dashboard/ic_PatientFemale_Big.svg";
            imgSex.ImageUrl = "~/Images/Icon/ic_Female.svg";
        }

        patientName.Text = model.PatientName;
        localMrNo.Text = model.MrNo;
        primaryDoctor.Text = model.DoctorName;
        lblAdmissionNo.Text = model.AdmissionNo;
        lblDOB.Text = model.BirthDate.ToString("dd MMM yyyy");
        lblAge.Text = clsCommon.GetAge(model.BirthDate);
        lblReligion.Text = model.Religion;
        lblPayer.Text = model.PayerName;
    }

    public void initializevalueLab(List<LaboratoryResult> listlaboratory)
    {
        panel1.InnerHtml = "";
        try
        {
            StringBuilder labHistory = new StringBuilder();

            if (listlaboratory.Count != 0)
            {
                if (listlaboratory[0].ConnStatus == "OFFLINE")
                {
                    img_noConnection.Visible = true;
                    img_noData.Visible = false;
                }
                else
                {
                    List<Int64?> listAdmissionId = listlaboratory.Select(x => x.admissionId).Distinct().ToList();
                    List<gridLaboratory> gridLaboratoryResult = new List<gridLaboratory>();
                    DataTable dt = new DataTable();
                    foreach (Int64? dataAdmission in listAdmissionId)
                    {
                        var admissionData = listlaboratory.Find(x => x.admissionId == dataAdmission);
                        List<String> listOno = listlaboratory.FindAll(x => x.admissionId == dataAdmission && x.IsHeader == 1).Select(x => x.ono).Distinct().ToList();

                        labHistory.Append("<div style=\"color:#4D9B35; border-bottom:1px solid #cdd2dd; border-top:1px solid #cdd2dd;\"><h4>" + String.Format("{0:ddd, MMM d, yyyy}", admissionData.admissionDate) + " - " + admissionData.cliniciaN_NM + "</h4></div>" +
                            "<div><table class=\"table table-striped table-condensed\">" +
                            "<tr>" +
                                "<td><b>Test</b></td>" +
                                "<td><b>Hasil</b></td>" +
                                "<td><b>Unit</b></td>" +
                                "<td><b>Nilai Rujukan</b></td> " +
                            "</tr>");
                        if (listOno.Count != 0)
                        {
                            foreach (String data in listOno)
                            {
                                List<String> testGrop = listlaboratory.FindAll(x => x.admissionId == dataAdmission && x.ono == data && x.IsHeader == 1).Select(x => x.tesT_GROUP).Distinct().ToList();

                                foreach (String dataTestGroup in testGrop)
                                {
                                    labHistory.Append("<tr style=\"background-color:lightgray;\"><td><h7><b>" + dataTestGroup + "</b></h7></td><td></td><td></td><td></td>" +
                                        //"<td></td>" +
                                        //"<td></td>" +
                                        "</tr>");

                                    gridLaboratoryResult = (
                                    from a in listlaboratory
                                    where a.admissionId == dataAdmission && a.ono == data && a.tesT_GROUP == dataTestGroup
                                    select new gridLaboratory
                                    {
                                        tesT_NM = a.tesT_NM,
                                        resulT_VALUE = a.resulT_VALUE,
                                        unit = a.unit,
                                        reF_RANGE = a.reF_RANGE,
                                        ono = a.ono,
                                        dis_sq = a.disP_SEQ,
                                        IsHeader = a.IsHeader
                                    }).ToList();


                                    foreach (gridLaboratory dataLab in gridLaboratoryResult)
                                    {
                                        if (dataLab.IsHeader == 1)
                                        {
                                            labHistory.Append("<tr><td><h7><b>" + dataLab.tesT_NM + "</b></h7></td>" +
                                                                "<td><h7><b>" + dataLab.resulT_VALUE + "</b></h7></td>" +
                                                                "<td><h7><b>" + dataLab.unit + "</b></h7></td>" +
                                                                "<td><h7><b>" + dataLab.reF_RANGE + "</b></h7></td>" +
                                                                //"<td><h7><b>" + dataLab.ono + "</b></h7></td>" +
                                                                //"<td><h7><b>" + dataLab.dis_sq + "</b></h7></td>" +
                                                                "</tr>");
                                        }
                                        else
                                        {
                                            labHistory.Append("<tr><td>" + dataLab.tesT_NM + "</td>" +
                                                            "<td>" + dataLab.resulT_VALUE + "</td>" +
                                                            "<td>" + dataLab.unit + "</td>" +
                                                            "<td>" + dataLab.reF_RANGE + "</td>" +
                                                            //"<td>" + dataLab.ono + "</td>" +
                                                            //"<td>" + dataLab.dis_sq + "</td>" +
                                                            "</tr>");
                                        }
                                    }
                                    gridLaboratoryResult = new List<gridLaboratory>();
                                }
                            }
                            labHistory.Append("</table></div>");
                        }
                    }
                }
            }
            else
            {
                img_noData.Visible = true;
            }

            panel1.InnerHtml = labHistory.ToString();

        }
        catch (Exception ex)
        {

        }
    }

    public void initializevalueHeaderRad(PatientHeader model)
    {
        if (model.Gender == 1)
        {
            Image2.ImageUrl = "~/Images/Dashboard/ic_PatientMale_Big.svg";
            imgSexRad.ImageUrl = "~/Images/Icon/ic_Male.svg";
        }
        else if (model.Gender == 2)
        {
            Image2.ImageUrl = "~/Images/Dashboard/ic_PatientFemale_Big.svg";
            imgSexRad.ImageUrl = "~/Images/Icon/ic_Female.svg";
        }

        patientNameRad.Text = model.PatientName;
        localMrNoRad.Text = model.MrNo;
        primaryDoctorRad.Text = model.DoctorName;
        lblAdmissionNoRad.Text = model.AdmissionNo;
        lblDOBRad.Text = model.BirthDate.ToString("dd MMM yyyy");
        lblAgeRad.Text = clsCommon.GetAge(model.BirthDate);
        lblReligionRad.Text = model.Religion;
        lblPayerRad.Text = model.PayerName;
    }

    public void initializevalueRad(List<radiologyByWeek> listAdmissionDetail)
    {
        StringBuilder radiologyInnerHTML = new StringBuilder();
        string imagePath = ResolveClientUrl("~/Images/PatientHistory/ic_newtab_blue.svg");
        if (listAdmissionDetail.Count != 0)
        {
            List<String> listDate = listAdmissionDetail.Select(x => String.Format("{0:dd MMM yyyy}", x.admissionDate)).Distinct().ToList();

            foreach (String data in listDate)
            {
                List<radiologyByWeek> listHistoryByDate = listAdmissionDetail.FindAll(x => data.Equals(String.Format("{0:dd MMM yyyy}", x.admissionDate)));

                radiologyInnerHTML.Append("<div class=\"sm-col-12\" style=\"background-color:#e7e8ef; font-size:14px; height: 35px;padding-top: 10px;\"><b><div class=\"container-fluid\">" + data + " - " + listHistoryByDate[0].orgCd + " - " + listHistoryByDate[0].doctorName + "</div></b></div> &nbsp;");
                foreach (radiologyByWeek dataRadiology in listHistoryByDate.OrderByDescending(x => x.admissionNo))
                {
                    radiologyInnerHTML.Append("<div class=\"sm-col-12\">" +
                                                    "<div class=\"container-fluid\"><b style=\"margin-right: 3px;\"><a href=\"" + dataRadiology.imageUrl + "\" style=\"color: blue; text-decoration:underline; \" target=\"_blank\">" + dataRadiology.salesItemName + "</a></b><span><img src=\"" + imagePath + "\" style=\"width: 14px; padding-bottom: 2px;\" /></span></div>" +
                                                    "<div class=\"container-fluid\">" + dataRadiology.responseMessage + "</div>" +
                                                "</div> &nbsp;");
                }
            }
            div_Radiology_detail.InnerHtml = radiologyInnerHTML.ToString();
            img_noData.Style.Add("display", "none");
        }
        else
        {
            div_Radiology_detail.InnerHtml = radiologyInnerHTML.ToString();
            img_noData.Style.Add("display", "block");
        }
    }

    protected void dropType_SelectedIndexChanged(object sender, EventArgs e)
    {
        dropPageOf.SelectedIndex = 0;
        currentPage = 1;
        resetDdlPage = true;

        getData(dropType.SelectedValue, dropDoctor.SelectedValue);
    }

    protected void btnrevisionModal_Click(object sender, EventArgs e)
    {
        try
        {
            //var userID = Helper.GetLoginUser(this);
            //log.Info(LogLibrary.Logging("S", "btnrevisionModal_Click", userID, ""));

            //log.Debug(LogLibrary.Logging("S", "getRevisionHistorySOAP ", userID, ""));

            var varResult = clsPatientDetail.getRevisionHistorySOAP(Int64.Parse(HFrevOrgID.Value), Int64.Parse(HFrevPtnID.Value), Int64.Parse(HFrevAdmID.Value), HFrevEncID.Value);
            var JsongetRevisionHistoryData = JsonConvert.DeserializeObject<ResultSOAPLog>(varResult.Result.ToString());

            //log.Debug(LogLibrary.Logging("E", "getRevisionHistorySOAP", userID, ""));

            SOAPLog SL = new SOAPLog();
            if (JsongetRevisionHistoryData != null)
            {
                SL = JsongetRevisionHistoryData.list;

                //List<long> headid = new List<long>();
                List<long> detailid = new List<long>();
                List<LogHeader> resultHeader = new List<LogHeader>();

                //foreach (LogHeader h in SL.Header)
                //{
                //    headid.Add(h.ID);
                //}

                foreach (LogSOAPData s in SL.SOAPData)
                {
                    detailid.Add(s.ID);
                }

                foreach (LogCPOE c in SL.CPOEData)
                {
                    detailid.Add(c.ID);
                }

                foreach (LogPrescription p in SL.PrescriptionData)
                {
                    detailid.Add(p.ID);
                }

                resultHeader = SL.Header.Where(r => detailid.Any(x => x == r.ID)).ToList();

                DataTable RevSoapData = Helper.ToDataTable(SL.SOAPData);
                Session[Helper.SessionRevSoap] = RevSoapData;

                DataTable RevCpoeData = Helper.ToDataTable(SL.CPOEData);
                Session[Helper.SessionRevCpoe] = RevCpoeData;

                DataTable RevPresData = Helper.ToDataTable(SL.PrescriptionData);
                Session[Helper.SessionRevPres] = RevPresData;

                //header is the last bind
                DataTable RevHeaderData = Helper.ToDataTable(resultHeader);
                RepeaterRevisionHeader.DataSource = RevHeaderData;
                RepeaterRevisionHeader.DataBind();


            }

            //log.Info(LogLibrary.Logging("E", "btnrevisionModal_Click", userID, ""));

        }
        catch (Exception ex)
        {
            throw ex;
            //log.Error(LogLibrary.Error("btnrevisionModal_Click", Helper.GetLoginUser(this), ex.InnerException.Message));
        }
    }

    public string getDatafromRow(DataTable dt, string mappingname, string tag)
    {
        DataRow[] drlabeldata = dt.Select("MappingName = '" + mappingname + "'");
        if (drlabeldata.Length > 0)
        {
            return drlabeldata[0][tag].ToString();
        }
        else
        {
            return "";
        }
    }

    protected void RepeaterRevisionHeader_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            string headerId = (e.Item.FindControl("HF_REVHeaderID") as HiddenField).Value;

            Panel panelS = e.Item.FindControl("panelS") as Panel;
            Panel panelO = e.Item.FindControl("panelO") as Panel;
            Panel panelA = e.Item.FindControl("panelA") as Panel;
            Panel panelP = e.Item.FindControl("panelP") as Panel;
            Panel panelCPOE = e.Item.FindControl("panelCPOE") as Panel;
            Panel panelPRES = e.Item.FindControl("panelPRES") as Panel;

            Repeater rptDetailLabRad = e.Item.FindControl("RptLabRad") as Repeater;
            Repeater rptDetailDrugs = e.Item.FindControl("RptDrugs") as Repeater;
            Repeater rptDetailCons = e.Item.FindControl("RptCons") as Repeater;


            if (Session[Helper.SessionRevSoap] != null)
            {
                DataRow[] drS = ((DataTable)Session[Helper.SessionRevSoap]).Select("ID = '" + headerId + "' AND MappingType = 'S'");

                if (drS.Length > 0)
                {
                    DataTable dtSoapS = drS.CopyToDataTable();

                    //Label LabelComplaint = e.Item.FindControl("LabelComplaint") as Label;
                    //LabelComplaint.Text = "<b>Patient Complaint</b> <br/>" + getDatafromRow(dtSoapS, "PATIENT COMPLAINT", "Remarks") + "<br/>";

                    //Label LabelAnamnesis = e.Item.FindControl("LabelAnamnesis") as Label;
                    //LabelAnamnesis.Text = "<b>Anamnesis</b> <br/>" + getDatafromRow(dtSoapS, "ANAMNESIS", "Remarks") ;

                    Label LabelComplaint = e.Item.FindControl("LabelComplaint") as Label;
                    DataRow[] drlabelc = dtSoapS.Select("MappingName = 'PATIENT COMPLAINT'");
                    if (drlabelc.Length > 0)
                    {
                        LabelComplaint.Text = "<b>Patient Complaint</b> <br/>" + drlabelc[0]["Remarks"].ToString().Replace("\n", "<br/>") + "<br/>";
                    }

                    Label LabelAnamnesis = e.Item.FindControl("LabelAnamnesis") as Label;
                    DataRow[] drlabela = dtSoapS.Select("MappingName = 'ANAMNESIS'");
                    if (drlabela.Length > 0)
                    {
                        LabelAnamnesis.Text = "<b>Anamnesis</b> <br/>" + drlabela[0]["Remarks"].ToString().Replace("\n", "<br/>") + "<br/>";
                    }

                    Label LabelDoctorNotesToNurse = e.Item.FindControl("LabelDoctorNotesToNurse") as Label;
                    DataRow[] drlabeld = dtSoapS.Select("MappingName = 'DOCTOR NOTES NURSE'");
                    if (drlabeld.Length > 0)
                    {
                        LabelDoctorNotesToNurse.Text = "<b>Doctor Notes To Nurse</b> <br/>" + drlabeld[0]["Remarks"].ToString().Replace("\n", "<br/>") + "<br/>";
                    }
                }
                else
                {
                    panelS.Visible = false;
                }

                DataRow[] drO = ((DataTable)Session[Helper.SessionRevSoap]).Select("ID = '" + headerId + "' AND MappingType = 'O'");

                if (drO.Length > 0)
                {
                    DataTable dtSoapO = drO.CopyToDataTable();

                    Label LabelOther = e.Item.FindControl("LabelOther") as Label;
                    DataRow[] drlabelother = dtSoapO.Select("MappingName = 'OTHERS'");
                    if (drlabelother.Length > 0)
                    {
                        LabelOther.Text = "<b>Others</b> <br/>" + drlabelother[0]["Remarks"].ToString().Replace("\n", "<br/>") + "<br/><br/>";
                    }

                    Label LabelBloodPresH = e.Item.FindControl("LabelBloodPresH") as Label;
                    DataRow[] drlabelbpH = dtSoapO.Select("MappingName = 'BLOOD PRESSURE HIGH'");
                    if (drlabelbpH.Length > 0)
                    {
                        LabelBloodPresH.Text = "<b>Blood Pressure High</b> : " + drlabelbpH[0]["Value"].ToString() + " mmHg <br/>";
                    }
                    Label LabelBloodPresL = e.Item.FindControl("LabelBloodPresL") as Label;
                    DataRow[] drlabelbpL = dtSoapO.Select("MappingName = 'BLOOD PRESSURE LOW'");
                    if (drlabelbpL.Length > 0)
                    {
                        LabelBloodPresL.Text = "<b>Blood Pressure Low</b> : " + drlabelbpL[0]["Value"].ToString() + " mmHg <br/>";
                    }
                    Label LabelPulse = e.Item.FindControl("LabelPulse") as Label;
                    DataRow[] drlabelpr = dtSoapO.Select("MappingName = 'PULSE RATE'");
                    if (drlabelpr.Length > 0)
                    {
                        LabelPulse.Text = "<b>Pulse</b> : " + drlabelpr[0]["Value"].ToString() + " x/mnt <br/>";
                    }
                    Label LabelRespiratory = e.Item.FindControl("LabelRespiratory") as Label;
                    DataRow[] drlabelrr = dtSoapO.Select("MappingName = 'RESPIRATORY RATE'");
                    if (drlabelrr.Length > 0)
                    {
                        LabelRespiratory.Text = "<b>Respiratory Rate</b> : " + drlabelrr[0]["Value"].ToString() + " x/mnt <br/>";
                    }
                    Label LabelSPO2 = e.Item.FindControl("LabelSPO2") as Label;
                    DataRow[] drlabelsp = dtSoapO.Select("MappingName = 'SPO2'");
                    if (drlabelsp.Length > 0)
                    {
                        LabelSPO2.Text = "<b>SpO2</b> : " + drlabelsp[0]["Value"].ToString() + " % <br/>";
                    }
                    Label LabelTemp = e.Item.FindControl("LabelTemp") as Label;
                    DataRow[] drlabelt = dtSoapO.Select("MappingName = 'TEMPERATURE'");
                    if (drlabelt.Length > 0)
                    {
                        LabelTemp.Text = "<b>Temperature</b> : " + drlabelt[0]["Value"].ToString() + " °C <br/>";
                    }
                    Label LabelWeight = e.Item.FindControl("LabelWeight") as Label;
                    DataRow[] drlabelw = dtSoapO.Select("MappingName = 'WEIGHT'");
                    if (drlabelw.Length > 0)
                    {
                        LabelWeight.Text = "<b>Weight</b> : " + drlabelw[0]["Value"].ToString() + " kg <br/>";
                    }
                    Label LabelHeight = e.Item.FindControl("LabelHeight") as Label;
                    DataRow[] drlabelh = dtSoapO.Select("MappingName = 'HEIGHT'");
                    if (drlabelh.Length > 0)
                    {
                        LabelHeight.Text = "<b>Height</b> : " + drlabelh[0]["Value"].ToString() + " cm <br/>";
                    }
                    Label LabelHeadCir = e.Item.FindControl("LabelHeadCir") as Label;
                    DataRow[] drlabelhc = dtSoapO.Select("MappingName = 'LINGKAR KEPALA'");
                    if (drlabelhc.Length > 0)
                    {
                        LabelHeadCir.Text = "<b>Head Circumference</b> : " + drlabelhc[0]["Value"].ToString() + " cm <br/>";
                    }
                }
                else
                {
                    panelO.Visible = false;
                }

                DataRow[] drA = ((DataTable)Session[Helper.SessionRevSoap]).Select("ID = '" + headerId + "' AND MappingType = 'A'");

                if (drA.Length > 0)
                {
                    DataTable dtSoapA = drA.CopyToDataTable();

                    Label LabelPrimaryDiagnosis = e.Item.FindControl("LabelPrimaryDiagnosis") as Label;
                    DataRow[] drlabelpd = dtSoapA.Select("MappingName = 'PRIMARY DIAGNOSIS'");
                    if (drlabelpd.Length > 0)
                    {
                        LabelPrimaryDiagnosis.Text = "<b>Primary Diagnosis</b> <br/>" + drlabelpd[0]["Remarks"].ToString().Replace("\n", "<br/>") + "<br/>";
                    }
                }
                else
                {
                    panelA.Visible = false;
                }

                DataRow[] drP = ((DataTable)Session[Helper.SessionRevSoap]).Select("ID = '" + headerId + "' AND MappingType = 'P'");

                if (drP.Length > 0)
                {
                    DataTable dtSoapP = drP.CopyToDataTable();

                    Label LabelPlanningProcedure = e.Item.FindControl("LabelPlanningProcedure") as Label;
                    DataRow[] drlabelpp = dtSoapP.Select("MappingName = 'PLANNING PROCEDURE'");
                    if (drlabelpp.Length > 0)
                    {
                        LabelPlanningProcedure.Text = "<b>Planning Procedure</b> <br/>" + drlabelpp[0]["Remarks"].ToString().Replace("\n", "<br/>") + "<br/>";
                    }

                    Label LabelPlanningOthers = e.Item.FindControl("LabelPlanningOthers") as Label;
                    DataRow[] drlabelpo = dtSoapP.Select("MappingName = 'PLANNING OTHERS'");
                    if (drlabelpo.Length > 0)
                    {
                        LabelPlanningOthers.Text = "<b>Planning Others</b> <br/>" + drlabelpo[0]["Remarks"].ToString().Replace("\n", "<br/>") + "<br/>";
                    }

                    Label LabelProcedureResult = e.Item.FindControl("LabelProcedureResult") as Label;
                    DataRow[] drlabelpr = dtSoapP.Select("MappingName = 'PROCEDURE RESULT'");
                    if (drlabelpr.Length > 0)
                    {
                        LabelProcedureResult.Text = "<b>Procedure Result</b> <br/>" + drlabelpr[0]["Remarks"].ToString().Replace("\n", "<br/>") + "<br/>";
                    }
                }
                else
                {
                    panelP.Visible = false;
                }
            }

            if (Session[Helper.SessionRevCpoe] != null)
            {
                DataRow[] drLabRad = ((DataTable)Session[Helper.SessionRevCpoe]).Select("ID = '" + headerId + "'");

                if (drLabRad.Length > 0)
                {
                    DataTable dtSoapLabRad = drLabRad.CopyToDataTable();
                    rptDetailLabRad.DataSource = dtSoapLabRad;
                    rptDetailLabRad.DataBind();
                }
                else
                {
                    panelCPOE.Visible = false;
                }
            }

            if (Session[Helper.SessionRevPres] != null)
            {
                DataRow[] drDrugs = ((DataTable)Session[Helper.SessionRevPres]).Select("ID = '" + headerId + "' AND IsConsumables = 0");

                if (drDrugs.Length > 0)
                {
                    DataTable dtSoapDrug = drDrugs.CopyToDataTable();
                    rptDetailDrugs.DataSource = dtSoapDrug;
                    rptDetailDrugs.DataBind();
                }

                DataRow[] drCons = ((DataTable)Session[Helper.SessionRevPres]).Select("ID = '" + headerId + "' AND IsConsumables = 1");

                if (drCons.Length > 0)
                {
                    DataTable dtSoapCons = drCons.CopyToDataTable();
                    rptDetailCons.DataSource = dtSoapCons;
                    rptDetailCons.DataBind();
                }

                DataRow[] drPres = ((DataTable)Session[Helper.SessionRevPres]).Select("ID = '" + headerId + "'");

                if (drPres.Length == 0)
                {
                    panelPRES.Visible = false;
                }
            }
        }
    }

    protected void btnDashboardModal_Click(object sender, EventArgs e)
    {
        string piencript = Helper.Encrypt(HFdashPtnID.Value);
        string encript = Helper.Encrypt("ENCRYPT");

        string url = "~/Form/FormViewer/FormPatientDashboard.aspx?OrganizationId=" + HFdashOrgID.Value + "&PatientId=" + piencript + "&EncounterId=" + HFdashEncID.Value + "&AdmissionId=" + HFdashAdmID.Value + "&DoctorId=" + HFdashDocID.Value + "&en=" + encript ;
        myDashboardIframe.Src = url;
        UpdatePanelDashboard.Update();
    }

    #region Pagination

    private void setPagination()
    {
        try
        {
            lbFirst.CssClass = currentPage != 1 ? "btn btn-sm btn-primary" : "btn btn-sm btn-primary disabled";
            lbPrevious.CssClass = currentPage != 1 ? "btn btn-sm btn-primary" : "btn btn-sm btn-primary disabled";
            lbNext.CssClass = currentPage != countPage ? (countPage > 0 ? "btn btn-sm btn-primary" : "btn btn-sm btn-primary disabled") : "btn btn-sm btn-primary disabled";
            lbLast.CssClass = currentPage != countPage ? (countPage > 0 ? "btn btn-sm btn-primary" : "btn btn-sm btn-primary disabled") : "btn btn-sm btn-primary disabled";
            lCurrentPage.Text = currentPage.ToString();
            lTotalPage.InnerHtml = "Total Halaman: " + (countPage == 0 ? 1 : countPage).ToString();

            if (resetDdlPage)
            {
                dropPageOf.Items.Clear();

                for (int i = 1; i <= countPage; i++)
                {
                    ListItem ls = new ListItem();
                    ls.Value = i.ToString();
                    ls.Text = "Halaman " + i.ToString();
                    dropPageOf.Items.Add(ls);
                }
            }

            if (countPage <= 0)
            {
                dropPageOf.Items.Clear();
                {
                    ListItem ls = new ListItem();
                    ls.Value = "1";
                    ls.Text = "Halaman 1";
                    dropPageOf.Items.Add(ls);
                }
            }

            //upPagination.Update();
        }

        catch (Exception ex)
        {
            throw ex;
        }
    }

    protected void lbFirst_Click(object sender, EventArgs e)
    {
        dropPageOf.SelectedIndex = 0;
        currentPage = 1;
        resetDdlPage = false;

        getData(dropType.SelectedValue, dropDoctor.SelectedValue);
        ScriptManager.RegisterClientScriptBlock(this, GetType(), Guid.NewGuid().ToString(), "topPage()", true);
    }

    protected void lbPrevious_Click(object sender, EventArgs e)
    {
        dropPageOf.SelectedIndex -= 1;
        currentPage -= 1;
        resetDdlPage = false;

        getData(dropType.SelectedValue, dropDoctor.SelectedValue);
        ScriptManager.RegisterClientScriptBlock(this, GetType(), Guid.NewGuid().ToString(), "topPage()", true);
    }

    protected void lbNext_Click(object sender, EventArgs e)
    {
        dropPageOf.SelectedIndex += 1;
        currentPage += 1;
        resetDdlPage = false;

        getData(dropType.SelectedValue, dropDoctor.SelectedValue);
        ScriptManager.RegisterClientScriptBlock(this, GetType(), Guid.NewGuid().ToString(), "topPage()", true);
    }

    protected void lbLast_Click(object sender, EventArgs e)
    {
        dropPageOf.SelectedIndex = countPage - 1;
        currentPage = countPage;
        resetDdlPage = false;

        getData(dropType.SelectedValue, dropDoctor.SelectedValue);
        ScriptManager.RegisterClientScriptBlock(this, GetType(), Guid.NewGuid().ToString(), "topPage()", true);
    }

    protected void dropPageOf_SelectedIndexChanged(object sender, EventArgs e)
    {
        currentPage = dropPageOf.SelectedIndex + 1;
        resetDdlPage = false;

        getData(dropType.SelectedValue, dropDoctor.SelectedValue);
    }

    #endregion
}