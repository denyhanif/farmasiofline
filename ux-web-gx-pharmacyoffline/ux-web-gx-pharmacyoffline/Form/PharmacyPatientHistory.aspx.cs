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

public partial class Form_PharmacyPatientHistory : System.Web.UI.Page
{

    HiddenField organizationId;
    HiddenField patientId;
    HiddenField admId;
    HiddenField encId;
    HiddenField printBY;
    HiddenField doctorName;
    HiddenField pageSOAP;

    int flag = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        if(!IsPostBack)
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

                getData("ALL");
            }
        }
    }

    protected void getData(string Type)
    {

        Session[Helper.sessionPatientHistoryLite] = null;

        var dataPatientHistory = clsPatientHistory.getPatientHistoryLite(Request.QueryString["OrganizationId"], Request.QueryString["PatientId"], DateTime.Parse(txtDateFromNew.Text), DateTime.Parse(txtToDateNew.Text), Type);

        var Response = JsonConvert.DeserializeObject<dynamic>(dataPatientHistory.Result);
        var Message = Response.Property("status").Value.ToString();



        if (Message.ToString().ToLower() != "Fail".ToLower())
        {
            var patientIdOwned = JsonConvert.DeserializeObject<ResultPatientHistoryLite>(dataPatientHistory.Result.ToString());
            List<PatientHistoryLite> listPatientHistoryLite = new List<PatientHistoryLite>();
            List<String> listDoctor = new List<String>();

            listPatientHistoryLite = patientIdOwned.list;

            listDoctor = (from a in listPatientHistoryLite
                          select a.DoctorName
                          ).Distinct().ToList();

            if (listPatientHistoryLite[0].CountData > 20)
            {
                alertNotif.Visible = true;
            }
            else
            {
                alertNotif.Visible = false;
            }

            dropDoctor.DataSource = listDoctor;
            dropDoctor.DataBind();
            dropDoctor.Items.Insert(0, new ListItem("Dokter"));

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

            gvw_detail.DataSource = dtworklist;
            gvw_detail.DataBind();
            //styleze();

            HFdashOrgID.Value = listPatientHistoryLite[0].OrganizationId.ToString();
            HFdashPtnID.Value = listPatientHistoryLite[0].PatientId.ToString();
            HFdashAdmID.Value = listPatientHistoryLite[0].AdmissionId.ToString();
            HFdashEncID.Value = listPatientHistoryLite[0].EncounterId.ToString();
            HFdashDocID.Value = listPatientHistoryLite[0].DoctorId.ToString();

            div_linkdashboard.Visible = true;
        }

        else
        {
            gvw_detail.DataSource = null;
            gvw_detail.DataBind();

            div_linkdashboard.Visible = false;
        }
    }

    protected void styleze()
    {
        //    ini dicomment karena pakai label, kalau mau balik pakai text area ini di uncomment

        //foreach (GridViewRow rows in gvw_detail.Rows)
        //{
        //    TextBox txtObat = (TextBox)rows.FindControl("txtObat");
        //    TextBox txtsubjective = (TextBox)rows.FindControl("txtsubjective");
        //    TextBox txtobjective = (TextBox)rows.FindControl("txtobjective");
        //    //txtObat.Text.Replace("\\n", "\r\n");

        //    string text_str = txtObat.Text;
        //    text_str = text_str.Replace("\\n", "\r\n");
        //    txtObat.Text = string.Empty;
        //    txtObat.Text = text_str;

        //    string text_strSub = txtsubjective.Text;
        //    text_strSub = text_strSub.Replace("\\n", "\r\n");
        //    txtsubjective.Text = string.Empty;
        //    txtsubjective.Text = text_strSub;

        //    string text_strObj = txtobjective.Text;
        //    text_strObj = text_strObj.Replace("\\n", "\r\n");
        //    txtobjective.Text = string.Empty;
        //    txtobjective.Text = text_strObj;
        //}

        //foreach (GridViewRow rows in gvw_detail.Rows)
        //{
        //    TextBox txtObat = (TextBox)rows.FindControl("txtObat");
        //    TextBox txtsubjective = (TextBox)rows.FindControl("txtsubjective");
        //    TextBox txtobjective = (TextBox)rows.FindControl("txtobjective");
        //    TextBox txtanamnesis = (TextBox)rows.FindControl("txtDiagnosa");
        //    TextBox txtprocedure = (TextBox)rows.FindControl("txtTindakan");

        //    int heightObat = txtObat.Text.Split('\n').Length;
        //    int heightS = txtsubjective.Text.Split('\n').Length;
        //    int heightO = txtobjective.Text.Split('\n').Length;
        //    int heightA = txtanamnesis.Text.Split('\n').Length;
        //    int heightP = txtprocedure.Text.Split('\n').Length;
        //    int[] arr = new int[5];
        //    arr = new int[] { heightObat, heightS, heightO, heightA, heightP };
        //    int maxHeight = arr.Max() + 2;

        //    //int maxHeight = 150;

        //    txtObat.Rows = maxHeight;
        //    txtsubjective.Rows = maxHeight;
        //    txtobjective.Rows = maxHeight;
        //    txtanamnesis.Rows = maxHeight;
        //    txtprocedure.Rows = maxHeight;
        //}
    }

    protected void txtFromDate_TextChanged(object sender, EventArgs e)
    {
        getData(dropType.SelectedValue);
    }

    protected void txtToDateNew_TextChanged(object sender, EventArgs e)
    {
        getData(dropType.SelectedValue);
    }

    protected void drugs_data_RowDataBound(Object sender, GridViewRowEventArgs e)
    {
        try
        {

        }
        catch (Exception ex)
        {

        }
    }

    protected void btnPrintSOAP_Click(object sender, EventArgs e)
    {
        int selRowIndex = ((GridViewRow)(((ImageButton)sender).Parent.Parent)).RowIndex;

        encId = (HiddenField)gvw_detail.Rows[selRowIndex].FindControl("hdnEncId");
        organizationId = (HiddenField)gvw_detail.Rows[selRowIndex].FindControl("hdnOrganization");
        admId = (HiddenField)gvw_detail.Rows[selRowIndex].FindControl("hdnAdmId");
        patientId = (HiddenField)gvw_detail.Rows[selRowIndex].FindControl("hdnPatientID");
        hfPrintBY.Value = Request.QueryString["PrintBy"];
        doctorName = (HiddenField)gvw_detail.Rows[selRowIndex].FindControl("hdnDoctorName");

        var localIPAdress = "";

        localIPAdress = GetLocalIPAddress();
        //localIPAdress = "10.83.254.38"; //HARD CODE

        ScriptManager.RegisterStartupScript(
        this, GetType(), "OpenWindow", "window.open('http://"+ localIPAdress+ "/printingemr?printtype=MedicalResumeLite&OrganizationId=" + organizationId.Value+ "&AdmissionId=" + admId.Value + "&EncounterId=" + encId.Value + "&PatientId=" + patientId.Value + "&PrintBy=" + hfPrintBY.Value.ToString() + "&DoctorBy=" + doctorName.Value.ToString() + "','_blank');", true);
    }

    protected void btnPrintSOAPlong_Click(object sender, EventArgs e)
    {
        int selRowIndex = ((GridViewRow)(((ImageButton)sender).Parent.Parent)).RowIndex;

        encId = (HiddenField)gvw_detail.Rows[selRowIndex].FindControl("hdnEncId");
        organizationId = (HiddenField)gvw_detail.Rows[selRowIndex].FindControl("hdnOrganization");
        admId = (HiddenField)gvw_detail.Rows[selRowIndex].FindControl("hdnAdmId");
        patientId = (HiddenField)gvw_detail.Rows[selRowIndex].FindControl("hdnPatientID"); 
        pageSOAP = (HiddenField)gvw_detail.Rows[selRowIndex].FindControl("hdnPageSoap"); 
        hfPrintBY.Value = Request.QueryString["PrintBy"];

        var localIPAdress = "";

        localIPAdress = GetLocalIPAddress();
        //localIPAdress = "10.83.254.38"; //HARD CODE

        ScriptManager.RegisterStartupScript(
        this, GetType(), "OpenWindow", "window.open('http://" + localIPAdress + "/printingemr?printtype=MedicalResume&OrganizationId=" + organizationId.Value + "&AdmissionId=" + admId.Value + "&EncounterId=" + encId.Value + "&PatientId=" + patientId.Value + "&PageSOAP=" + pageSOAP.Value + "&PrintBy=" + hfPrintBY.Value.ToString() + "','_blank');", true);
    }

    protected void btnPrintReferral_Click(object sender, EventArgs e)
    {
        int selRowIndex = ((GridViewRow)(((ImageButton)sender).Parent.Parent)).RowIndex;

        encId = (HiddenField)gvw_detail.Rows[selRowIndex].FindControl("hdnEncId");
        organizationId = (HiddenField)gvw_detail.Rows[selRowIndex].FindControl("hdnOrganization");
        admId = (HiddenField)gvw_detail.Rows[selRowIndex].FindControl("hdnAdmId");
        patientId = (HiddenField)gvw_detail.Rows[selRowIndex].FindControl("hdnPatientID");
        pageSOAP = (HiddenField)gvw_detail.Rows[selRowIndex].FindControl("hdnPageSoap");
        hfPrintBY.Value = Request.QueryString["PrintBy"];

        var localIPAdress = "";

        localIPAdress = GetLocalIPAddress();
        //localIPAdress = "10.83.254.38"; //HARD CODE

        ScriptManager.RegisterStartupScript(
        this, GetType(), "OpenWindow", "window.open('http://" + localIPAdress + "/printingemr?printtype=ReferralResume&OrganizationId=" + organizationId.Value + "&AdmissionId=" + admId.Value + "&EncounterId=" + encId.Value + "&PatientId=" + patientId.Value + "&PageSOAP=" + pageSOAP.Value + "&PrintBy=" + hfPrintBY.Value.ToString() + "','_blank');", true);
    }

    protected void btnPrintRawatInap_Click(object sender, EventArgs e)
    {
        int selRowIndex = ((GridViewRow)(((ImageButton)sender).Parent.Parent)).RowIndex;

        encId = (HiddenField)gvw_detail.Rows[selRowIndex].FindControl("hdnEncId");
        organizationId = (HiddenField)gvw_detail.Rows[selRowIndex].FindControl("hdnOrganization");
        admId = (HiddenField)gvw_detail.Rows[selRowIndex].FindControl("hdnAdmId");
        patientId = (HiddenField)gvw_detail.Rows[selRowIndex].FindControl("hdnPatientID");
        pageSOAP = (HiddenField)gvw_detail.Rows[selRowIndex].FindControl("hdnPageSoap");
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


    protected void btnPatientHistory_Click(object sender, EventArgs e)
    {

        int selRowIndex = ((GridViewRow)(((ImageButton)sender).Parent.Parent)).RowIndex;
        organizationId = (HiddenField)gvw_detail.Rows[selRowIndex].FindControl("hdnOrganization");
        patientId = (HiddenField)gvw_detail.Rows[selRowIndex].FindControl("hdnPatientID");
        admId = (HiddenField)gvw_detail.Rows[selRowIndex].FindControl("hdnAdmId");
        encId = (HiddenField)gvw_detail.Rows[selRowIndex].FindControl("hdnEncId");
        pageSOAP = (HiddenField)gvw_detail.Rows[selRowIndex].FindControl("hdnPageSoap");
        //string url = "~/Form/PatientHistoryByEncounter.aspx?PatientId=" + patientId.Value + "&OrganizationId=" + organizationId.Value + "&AdmId=" + admId.Value + "&EncId=" + encId.Value;
        string url = "~/Form/FormViewer/MedicalResumePatient.aspx?org_id=" + organizationId.Value + "&ptn_id=" + patientId.Value + "&adm_id=" + admId.Value + "&enc_id=" + encId.Value + "&pagesoap_id=" + pageSOAP.Value + "&headertype=0" + "&username=" + hfPrintBY.Value;
        IframeMedicalResumePatient.Src = url;
        updatePatientHistory.Update();
        ScriptManager.RegisterStartupScript(this, this.GetType(), "openPatientHistoryModal", "openPatientHistoryModal()", true);
    }

    protected void btnFilterOnClick(object sender, EventArgs e)
    {
        if (Session[Helper.sessionPatientHistoryLite] != null)
        {
            DataTable dtworklist = Session[Helper.sessionPatientHistoryLite] as DataTable;
            DataTable dtfiltered = new DataTable();

            if (dtworklist.Select("DoctorName = '" + dropDoctor.Text + "'").Count() > 0)
            {
                dtfiltered = dtworklist.Select("DoctorName = '" + dropDoctor.Text + "'").CopyToDataTable();
                gvw_detail.DataSource = dtfiltered;
                gvw_detail.DataBind();
            }
            else
            {
                gvw_detail.DataSource = dtworklist;
                gvw_detail.DataBind();
            }
            styleze();
        }
    }

    protected void btnPrintOnClick(object sender, EventArgs e)
    {
        List<AllDataForPrint> patientSOAPxHeader = GetRowListSOAPxHeader();

        List<AllDataForPrint> patientSOAPxHeaderFiltered = (from a in patientSOAPxHeader
                                                            where a.isSelected == 1
                                                            select a
                                                            ).Distinct().ToList();

        DataTable dtPatientSOAP = Helper.ToDataTable(patientSOAPxHeaderFiltered);
        Session[Guid.NewGuid().ToString() + Helper.SessionAllDataForPrint] = patientSOAPxHeaderFiltered;
    }

    protected List<AllDataForPrint> GetRowListSOAPxHeader()
    {
        int i = 1;
        List<AllDataForPrint> data = new List<AllDataForPrint>();
        try
        {
            foreach (GridViewRow rows in gvw_detail.Rows)
            {

                Label DoctorName = (Label)rows.FindControl("txtDokter");
                HiddenField PatientName = (HiddenField)rows.FindControl("hdnPatientName");
                HiddenField OrgCode = (HiddenField)rows.FindControl("hdnOrgCode");
                HiddenField Birthdate = (HiddenField)rows.FindControl("hdnTTLumur");
                HiddenField Gender = (HiddenField)rows.FindControl("hdnJK");
                HiddenField localMRno = (HiddenField)rows.FindControl("hdnLocalMrNo");

                HiddenField OrgID = (HiddenField)rows.FindControl("hdnOrganization");
                HiddenField AdmID = (HiddenField)rows.FindControl("hdnAdmId");
                HiddenField EncID = (HiddenField)rows.FindControl("hdnEncId");

                TextBox S = (TextBox)rows.FindControl("txtsubjective");
                TextBox O = (TextBox)rows.FindControl("txtobjective");
                TextBox A = (TextBox)rows.FindControl("txtDiagnosa");
                TextBox P = (TextBox)rows.FindControl("txtTindakan");


                AllDataForPrint row = new AllDataForPrint();



                row.DoctorName = DoctorName.Text;
                row.PatientName = PatientName.Value;
                row.OrgCode = OrgCode.Value;
                row.Birthdate = Birthdate.Value;
                row.Gender = Gender.Value;
                row.localMRno = localMRno.Value;

                row.EncounterId = Guid.Parse(EncID.Value);
                row.AdmissionId = long.Parse(AdmID.Value);
                row.OrganizationId = long.Parse(OrgID.Value);

                row.Subjective = S.Text;
                row.Objective = O.Text;
                row.Diagnosis = A.Text;
                row.PlanningProcedure = P.Text;
                
                row.key = i;
                i++;
                data.Add(row);
            }
        }
        catch (Exception ex)
        {
        }
        return data;
    }
    

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
        getData(dropType.SelectedValue);
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
        string url = "~/Form/FormViewer/FormPatientDashboard.aspx?OrganizationId=" + HFdashOrgID.Value + "&PatientId=" + HFdashPtnID.Value + "&EncounterId=" + HFdashEncID.Value + "&AdmissionId=" + HFdashAdmID.Value + "&DoctorId=" + HFdashDocID.Value;
        myDashboardIframe.Src = url;
        UpdatePanelDashboard.Update();
    }
}