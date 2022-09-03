using Microsoft.Ajax.Utilities;
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
using System.Drawing;

public partial class Form_BigPatientHistory : System.Web.UI.Page
{
    public List<Dose> listdoseUom = new List<Dose>();
    public List<physicalExm> eye = new List<physicalExm>();
    public List<physicalExm> move = new List<physicalExm>();
    public List<physicalExm> verbal = new List<physicalExm>();
    public List<PatientAdmissionType> patientType = new List<PatientAdmissionType>();

    public string setENG = "none";
    public string setIND = "none";
    public string isBahasa = "";

    public string QSpatientid; 

    protected void Page_Load(object sender, EventArgs e)
    {

        //set bahasa
        //ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "bahasa", "switchBahasa();", true);

        if (!IsPostBack)
        {
            Session[Helper.SessionPreviousRowIndex] = null;

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
                ConfigurationManager.AppSettings["DB_HIS_External"] = SiloamConfig.Functions.GetValue("DB_HIS_External").ToString();

                ConfigurationManager.AppSettings["BaseURL_EMR_Viewer"] = SiloamConfig.Functions.GetValue("BaseURL_EMR_Viewer").ToString();
                
            }

            string en = Request.QueryString["en"];
            if (en != null)
            {
                QSpatientid = Helper.Decrypt(Request.QueryString["idPatient"]);
            }
            else
            {
                QSpatientid = Request.QueryString["idPatient"];
            }

            string piencript = Helper.Encrypt(QSpatientid);
            string encript = Helper.Encrypt("ENCRYPT");

            string localIP = GetLocalIPAddress();
            string baseURLhttp = "http://" + localIP + "/viewer";
            string baseURLhttps = ConfigurationManager.AppSettings["BaseURL_EMR_Viewer"]; //"https://gtn-devws-01.siloamhospitals.com:2123"; //nanti akan ambil dari registry
            emr_data.Src = baseURLhttps + "/form/formviewer/patienthistory/PH_Mr.aspx?OrganizationId=" + Request.QueryString["OrgId"] + "&PatientId=" + piencript + "&PrintBy=" + Request.QueryString["PrintBy"] + "&DoctorId=0" + "&en=" + encript;
            
            //emr_data.Src = baseURLhttps + "/form/PharmacyPatientHistory?OrganizationId=" + Request.QueryString["OrgId"] + "&PatientId=" + Request.QueryString["idPatient"] + "&PrintBy=" + Request.QueryString["PrintBy"];
            //emr_data.Src = "http://" + localIP + "/viewer/form/PharmacyPatientHistory?OrganizationId=" + Request.QueryString["OrgId"] + "&PatientId=" + Request.QueryString["idPatient"] + "&PrintBy=" + Request.QueryString["PrintBy"];
            //emr_data.Src = "~/Form/PharmacyPatientHistory?OrganizationId=" + Request.QueryString["OrgId"] + "&PatientId=" + Request.QueryString["idPatient"] + "&PrintBy=" + Request.QueryString["PrintBy"];

            //if (Request.QueryString["EncounterId"] == null)
            //{
            //    Response.Redirect("~/Form/General/Login.aspx", true);
            //    Context.ApplicationInstance.CompleteRequest();
            //}
            //if (Helper.GetLoginUser(this) == null)
            //{
            //    Response.Redirect("~/Form/General/Login.aspx", true);
            //    Context.ApplicationInstance.CompleteRequest();
            //}
            //var test1 = Helper.GetDoctorID(this);
            //if (Helper.GetDoctorID(this) == "")
            //{
            //    Response.Redirect("~/Form/General/Login.aspx", true);
            //    Context.ApplicationInstance.CompleteRequest();
            //}
            //else
            //{
            // ------------------------------------------------ Fill Master Data ------------------------------------------------- //

            /* ----------------------------------------- Date Search Emr --------------------------------------------------*/
            DateTextboxStart_emr.Text = DateTime.Now.AddMonths(-6).ToString("dd MMM yyyy");
                DateTextboxEnd_emr.Text = DateTime.Now.ToString("dd MMM yyyy");
                DateTextboxStart_emr.Attributes.Add("ReadOnly", "ReadOnly");
                DateTextboxEnd_emr.Attributes.Add("ReadOnly", "ReadOnly");
                /* ----------------------------------------- Date Search Emr --------------------------------------------------*/

                /* ----------------------------------------- Date Search Hope Emr --------------------------------------------------*/
                DateTextboxStart_hopeEmr.Text = DateTime.Now.AddMonths(-24).ToString("dd MMM yyyy");
                DateTextboxEnd_hopeEmr.Text = DateTime.Now.ToString("dd MMM yyyy");
                DateTextboxStart_hopeEmr.Attributes.Add("ReadOnly", "ReadOnly");
                DateTextboxEnd_hopeEmr.Attributes.Add("ReadOnly", "ReadOnly");
                /* ----------------------------------------- Date Search Hope Emr --------------------------------------------------*/

                /* ----------------------------------------- Date Search Other Unit Emr --------------------------------------------------*/
                DateTextboxStart_other.Text = DateTime.Now.AddMonths(-6).ToString("dd MMM yyyy");
                DateTextboxEnd_other.Text = DateTime.Now.ToString("dd MMM yyyy");
                DateTextboxStart_other.Attributes.Add("ReadOnly", "ReadOnly");
                DateTextboxEnd_other.Attributes.Add("ReadOnly", "ReadOnly");
                /* ----------------------------------------- Date Search Other Unit Emr --------------------------------------------------*/

                /* ----------------------------------------- Date Search Scanned MR --------------------------------------------------*/
                DateTextboxStart_scanned.Text = DateTime.Now.AddYears(-5).ToString("dd MMM yyyy");
                DateTextboxEnd_scanned.Text = DateTime.Now.ToString("dd MMM yyyy");
                DateTextboxStart_scanned.Attributes.Add("ReadOnly", "ReadOnly");
                DateTextboxEnd_scanned.Attributes.Add("ReadOnly", "ReadOnly");
                /* ----------------------------------------- Date Search Scanned MR --------------------------------------------------*/

                //HyperLink test = Master.FindControl("PatientHistoryLink") as HyperLink;
                //test.Style.Add("background-color", "#D6DBFF");

                // ------------------------------------------------------------ Electronic MR --------------------------------------------------------------
                eye = new List<physicalExm>();
                Session.Remove(Helper.ViewStatePatientHistoryEye);
                eye.Add(new physicalExm { idph = 1, name = "None" });
                eye.Add(new physicalExm { idph = 2, name = "To Pressure" });
                eye.Add(new physicalExm { idph = 3, name = "To Sound" });
                eye.Add(new physicalExm { idph = 4, name = "Spontaneus" });
                Session[Helper.ViewStatePatientHistoryEye] = eye;

                move = new List<physicalExm>();
                Session.Remove(Helper.ViewStatePatientHistoryMove);
                move.Add(new physicalExm { idph = 1, name = "None" });
                move.Add(new physicalExm { idph = 2, name = "Extension" });
                move.Add(new physicalExm { idph = 3, name = "Flexion to pain stumulus" });
                move.Add(new physicalExm { idph = 4, name = "Withdrawns from pain" });
                move.Add(new physicalExm { idph = 5, name = "Localizes to pain stimulus" });
                move.Add(new physicalExm { idph = 6, name = "Obey Commands" });
                move.Add(new physicalExm { idph = 1, name = "None" });
                Session[Helper.ViewStatePatientHistoryMove] = move;

                verbal = new List<physicalExm>();
                Session.Remove(Helper.ViewStatePatientHistoryVerbal);
                verbal.Add(new physicalExm { idph = 1, name = "None" });
                verbal.Add(new physicalExm { idph = 2, name = "Incomprehensible sounds" });
                verbal.Add(new physicalExm { idph = 3, name = "Inappropriate words" });
                verbal.Add(new physicalExm { idph = 4, name = "Confused" });
                verbal.Add(new physicalExm { idph = 5, name = "Orientated" });
                Session[Helper.ViewStatePatientHistoryVerbal] = verbal;
                // ------------------------------------------------------------ Electronic MR --------------------------------------------------------------

                // --------------------------------------------------------------- Hope MR -----------------------------------------------------------------
                patientType = new List<PatientAdmissionType>();
                Session.Remove(Helper.ViewStatePatientHistoryPatientType);
                patientType.Add(new PatientAdmissionType { admissionTypeId = 1, admissionTypeName = "OPD" });
                patientType.Add(new PatientAdmissionType { admissionTypeId = 2, admissionTypeName = "IPD" });
                patientType.Add(new PatientAdmissionType { admissionTypeId = 3, admissionTypeName = "ED" });
                patientType.Add(new PatientAdmissionType { admissionTypeId = 4, admissionTypeName = "Checkup" });
                Session[Helper.ViewStatePatientHistoryPatientType] = patientType;
            // --------------------------------------------------------------- Hope MR -----------------------------------------------------------------

            // ------------------------------------------------ Fill Master Data ------------------------------------------------- //

                hfPatientId.Value = QSpatientid; //Request.QueryString["idPatient"];
                hfEncounterId.Value = Request.QueryString["EncounterId"];
                hfAdmissionId.Value = Request.QueryString["AdmissionId"];
                hfPageSoapId.Value = Request.QueryString["PageSoapId"];
                hfOrgId.Value = Request.QueryString["OrgId"];

                //getHeader();
                //getEncounter();

            /*Dose UOM */
                try
                {
                    var doseUomData = clsOrderSet.getDose();
                    var JsondoseUom = JsonConvert.DeserializeObject<ResultDose>(doseUomData.Result.ToString());
                    

                    listdoseUom = JsondoseUom.list;
                    Session[Helper.ViewStatePatientHistoryDoseUOM] = listdoseUom;

                }
                catch (Exception ex)
                {

                }

            //load all data
                try
                {
                    var organizationSettingData = clsPatientHistory.getStatusMR(long.Parse(hfOrgId.Value), "HOPE_MR");
                    ResultOrganizationSetting OrganizationSetting = JsonConvert.DeserializeObject<ResultOrganizationSetting>(organizationSettingData.Result.ToString());
                    
                    if (OrganizationSetting.list.setting_value == "TRUE")
                    {
                        //getHopeEmrData();
                    }
                    else
                    {
                        btn_hope_emr.Style.Add("display", "none");
                    }
                }
                catch (Exception ex)
                {
                }

                try
                {
                    var organizationSettingData = clsPatientHistory.getStatusMR(long.Parse(hfOrgId.Value), "SCANNED_MR");
                    ResultOrganizationSetting OrganizationSetting = JsonConvert.DeserializeObject<ResultOrganizationSetting>(organizationSettingData.Result.ToString());
                    if (OrganizationSetting.list.setting_value == "TRUE")
                    {
                        //getScannedData();
                    }
                    else
                    {
                        btn_scanned_emr.Style.Add("display", "none");
                    }
                }
                catch (Exception ex)
                {
                }

                try
                {
                    var organizationSettingData = clsPatientHistory.getStatusMR(long.Parse(hfOrgId.Value), "OTHERUNIT_MR");
                    ResultOrganizationSetting OrganizationSetting = JsonConvert.DeserializeObject<ResultOrganizationSetting>(organizationSettingData.Result.ToString());

                    if (OrganizationSetting.list.setting_value == "TRUE")
                    {
                        //getOtherUnitData(2);
                    }
                    else
                    {
                        btn_other_emr.Style.Add("display", "none");
                        img_noData_other_mr.Style.Add("display", "");
                    }
                }
                catch (Exception ex)
                {
                }
            //}
        }
    }

    void getHopeEmrData()
    {
        try
        {
            GridView gvw_test = new GridView();
            StringBuilder innerHtmlHopeEmr = new StringBuilder();
            List<PatientHistoryHOPEemr> dataHopeEMR = new List<PatientHistoryHOPEemr>();

            var varResult = clsPatientHistory.getHOPEemrData(long.Parse(hfOrgId.Value), hfPatientId.Value, DateTime.Parse(DateTextboxStart_hopeEmr.Text), DateTime.Parse(DateTextboxEnd_hopeEmr.Text));
            var JsongetPatientHistoryHOPEemr = JsonConvert.DeserializeObject<ResultPatientHistoryHOPEemr>(varResult.Result.ToString());

            if (JsongetPatientHistoryHOPEemr.list.Count != 0)
            {
                myIframe.Visible = true;
                dataHopeEMR = JsongetPatientHistoryHOPEemr.list;
                Session[Helper.ViewStatePatientHistoryHOPEemr] = dataHopeEMR;
                //innerHtmlHopeEmr = 
                loadDataHopeEmr(dataHopeEMR);
                status_hopeEmr.Value = "Not empty";
            }
            else
            {
                myIframe.Visible = false;
                gvHopeMRList.DataSource = null;
                gvHopeMRList.DataBind();
                innerHtmlHopeEmr.Append("");
                status_hopeEmr.Value = "empty";
            }
            //hope__emr.InnerHtml = innerHtmlHopeEmr.ToString();
        }
        catch (Exception ex)
        {
        }
    }
    
    void getOtherUnitData(int year)
    {
        Session.Remove(Helper.ViewStateOtherUnitMR);
        try
        {           
            var varResult = clsPatientHistory.getOtherUnitData(Int64.Parse(hfPatientId.Value.ToString()), long.Parse(hfOrgId.Value), year);
            var JsongetOtherUnitData = JsonConvert.DeserializeObject<ResultOtherUnitMR>(varResult.Result.ToString());          

            Session[Helper.ViewStateOtherUnitMR] = JsongetOtherUnitData.list;

            if (JsongetOtherUnitData != null)
            {
                List<OtherUnitMR> OtherUnitData = new List<OtherUnitMR>();
                if (DateTextboxStart_other.Text != "" || DateTextboxEnd_other.Text != "")
                {
                    var tempOtherUnitData = new List<OtherUnitMR>();

                    tempOtherUnitData = JsongetOtherUnitData.list.FindAll(x => x.AdmissionDate >= DateTime.Parse(DateTextboxStart_other.Text.ToString()));
                    OtherUnitData = tempOtherUnitData;

                    if (DateTextboxEnd_other.Text != "")
                    {
                        OtherUnitData = tempOtherUnitData.FindAll(x => x.AdmissionDate <= DateTime.Parse(DateTextboxEnd_other.Text.ToString()).AddDays(1));
                    }
                }
                else
                {
                    OtherUnitData = JsongetOtherUnitData.list;
                }

                StringBuilder otherUnitInnerHTML = new StringBuilder();
                if (OtherUnitData.Count != 0)
                {
                    List<string> unit_filter = new List<string>();
                    unit_filter.AddRange(OtherUnitData.Select(x => x.OrganizationCode).Distinct());
                    //var firstitem = ddl_unit_other_mr.Items[0];
                    ddl_unit_other_mr.Items.Clear();
                    ddl_unit_other_mr.DataSource = unit_filter;
                    ddl_unit_other_mr.DataBind();
                    //ddl_unit_other_mr.Items.Add(firstitem);

                    otherUnitInnerHTML = loadOtherUnitEMR(OtherUnitData);
                    other_unit_emr_data.InnerHtml = otherUnitInnerHTML.ToString();
                    status_other_unit_Emr.Value = "Not empty";
                }
                else
                {
                    status_other_unit_Emr.Value = "empty";
                }
            }
            else
            {
                status_other_unit_Emr.Value = "empty";
            }

        }
        catch (Exception ex)
        {

        }
    }

    StringBuilder loadOtherUnitEMR(List<OtherUnitMR> OtherUnitData)
    {
        //HiddenField userFUllName = Master.FindControl("hfUserFullName") as HiddenField;

        StringBuilder otherUnitInnerHTML = new StringBuilder();
        string imagePath = ResolveClientUrl("~/Images/PatientHistory/ic_newtab_blue.svg");

        otherUnitInnerHTML.Append("<div style=\"border-radius: 7px; border: solid 2px lightgrey; margin:20px;\">");
        OtherUnitData = OtherUnitData.OrderByDescending(x => x.AdmissionDate).ToList();

        foreach (OtherUnitMR data in OtherUnitData)
        {
            otherUnitInnerHTML.Append("<div style=\"border-top: 1px solid #ddd;\">" +
                                    "<div style=\"margin-top: 10px;margin-bottom: 5px;\" class=\"container-fluid\"><a target=\"_blank\" href=\'http://" + data.LinkURL + "&PrintBy=" + Request.QueryString["PrintBy"] + "\'  style=\"color: blue; text-decoration:underline; \"><span><img src=\"" + imagePath + "\" /></span>  <b>" + data.AdmissionDate.ToString("dd MMM yyyy") + "</b></a></div>" +
                                    "<div style=\"margin-bottom: 10px;color: darkgrey;\" class=\"container-fluid\">" + data.AdmissionTypeCode + " " + data.AdmissionNo + " " + data.DoctorName + "</div>" +
                                    "</div>");
        }
        otherUnitInnerHTML.Append("</div>");

        return otherUnitInnerHTML;
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

    void loadDataHopeEmr(List<PatientHistoryHOPEemr> data)
    {
        patientType = (List<PatientAdmissionType>)Session[Helper.ViewStatePatientHistoryPatientType];
        StringBuilder hopeInnerHtml = new StringBuilder();
        List<PatientHistoryHOPEemr> hopeEmrData = new List<PatientHistoryHOPEemr>();
        List<long> admissionId = data.Select(x => x.admissionId).Distinct().ToList();
        int i = 0;
        var localIPAdress = GetLocalIPAddress();
        string imagePath = ResolveClientUrl("~/Images/PatientHistory/ic_newtab_blue.svg");

        //hopeInnerHtml.Append("<div style=\"border-radius: 7px; border: solid 2px lightgrey; margin:20px;\">");

        hopeEmrData = new List<PatientHistoryHOPEemr>();
        for (i = 0; i < admissionId.Count; i++)
        {
            
            hopeEmrData.AddRange(data.FindAll(x => x.admissionId.Equals(admissionId[i])));

            var doctorAdmission = "";

            foreach (PatientHistoryHOPEemr datahope in hopeEmrData)
            {
                doctorAdmission = doctorAdmission + " " + datahope.entryUser;
            }

            //localIPAdress = "10.83.254.38"; //HARD CODE
            var tempLink = "//" + localIPAdress + "/viewermrhope/Form/EMRHope/PatientHistoryDetail.aspx?orgid=" + hfOrgId.Value + "&admid=" + admissionId[i];

            //tempLinkIframe.Value = tempLink.ToString();

            hopeEmrData[i].linkMRHOPE = tempLink.ToString();
            //hopeEmrData[i].admTypePlusAdmId = patientType.Find(x => x.admissionTypeId == hopeEmrData[0].admissionTypeId).admissionTypeName + admissionId[i];

            //hopeInnerHtml.Append("<div style=\"border-top: 1px solid #ddd;\">" +
            //                    "<div style=\"margin-top: 10px;margin-bottom: 5px;\" class=\"container-fluid\"><a onclick=\'" + "klikHopeEmr(); return false;" + "\' href=\'" + "#" + "\'  style=\"color: blue; text-decoration:underline; \"><span><img src=\""+ imagePath + "\" /></span>  <b>" + hopeEmrData[0].admissionDate.ToString("dd MMM yyyy") + "</b></a></div>" +
            //                    "<div style=\"margin-bottom: 10px;color: darkgrey;\" class=\"container-fluid\">" + patientType.Find(x => x.admissionTypeId == hopeEmrData[0].admissionTypeId).admissionTypeName + " " + admissionId[i] + " " + doctorAdmission + "</div>" +
            //                    "</div>");
        }

        //gvHopeMRList.DataSource = null;
        gvHopeMRList.DataSource = Helper.ToDataTable(hopeEmrData.ToList());
        gvHopeMRList.DataBind();
        //hopeInnerHtml.Append("</div>");

        //return hopeInnerHtml;
    }

    protected void btnTanggalHopeMR_Click(object sender, EventArgs e)
    {
        if (Session[Helper.SessionPreviousRowIndex] != null)
        {
            var previousRowIndex = (int)Session[Helper.SessionPreviousRowIndex];
            GridViewRow PreviousRow = gvHopeMRList.Rows[previousRowIndex];
            PreviousRow.BackColor = Color.White;
        }

        GridViewRow row = (GridViewRow)((LinkButton)sender).NamingContainer;
        row.BackColor = ColorTranslator.FromHtml("#CDD2DD");
        Session[Helper.SessionPreviousRowIndex] = row.RowIndex;

        int selRowIndex = ((GridViewRow)(((LinkButton)sender).Parent.Parent)).RowIndex;
        HiddenField targetLink = (HiddenField)gvHopeMRList.Rows[selRowIndex].FindControl("hdnLinkHopeMR");
        myIframe.Src = targetLink.Value;
        UPpatientHistoryResult.Update();
    }


    protected void btnCompoundDetail_Click(object sender, EventArgs e)
    {
        try
        {
            headerCompound.Text = "Header - " + hfCompoundName.Value;
            List<MedicalHistory> tempData = (List<MedicalHistory>)Session[Helper.ViewStatePatientHistoryCompound];
            List<MedicalHistory> detailCompoundData = tempData.FindAll(x => x.compoundName.Equals(hfCompoundName.Value) && x.itemId != 0);
            if (detailCompoundData.Count != 0)
            {
                DataTable dt = Helper.ToDataTable(detailCompoundData);
                gvw_detail_compound.DataSource = dt;
                gvw_detail_compound.DataBind();
            }

        }
        catch (Exception ex)
        {

        }

    }

    void getScannedData()
    {
        Session.Remove(Helper.ViewStateScannedData);

        DataTable dt = new DataTable();
        dt = clsPatientHistory.getScannedData(hfPatientId.Value, 1, long.Parse(hfOrgId.Value));

        if (dt.Rows.Count != 0)
        {
            List<ScannedMR> tempDataTable = new List<ScannedMR>();
            try
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    ScannedMR data = new ScannedMR();
                    data.OrganizationId = Convert.ToInt64(dt.Rows[i]["OrganizationId"]);
                    data.PatientId = Convert.ToInt64(dt.Rows[i]["PatientId"]);
                    data.AdmissionId = Convert.ToInt64(dt.Rows[i]["AdmissionId"]);
                    data.AdmissionNo = Convert.ToString(dt.Rows[i]["AdmissionNo"]);
                    data.AdmissionDate = Convert.ToString(dt.Rows[i]["AdmissionDate"]);
                    data.MrNo = Convert.ToInt64(dt.Rows[i]["OrganizationId"]);
                    data.AdmissionType = Convert.ToString(dt.Rows[i]["AdmissionType"]);
                    data.FormTypeName = Convert.ToString(dt.Rows[i]["FormTypeName"]);
                    data.Path = Convert.ToString(dt.Rows[i]["Path"]);
                    data.DoctorName = Convert.ToString(dt.Rows[i]["DoctorName"]);

                    tempDataTable.Add(data);
                }
            }
            catch (Exception ex)
            {

            }
            //var test = DateTime.ParseExact(tempDataTable[0].AdmissionDate.ToString(), "dd MMM yyyy", CultureInfo.InvariantCulture) ;

            var dateStart = DateTime.Parse(DateTextboxStart_scanned.Text.ToString());
            var dateEnd = DateTime.Parse(DateTextboxEnd_scanned.Text.ToString());
            //Session[Helper.ViewStateScannedData] = tempDataTable.FindAll(x => x.AdmissionDate != "").FindAll(x => Convert.ToDateTime(x.AdmissionDate) >= dateStart && Convert.ToDateTime(x.AdmissionDate) <= dateEnd);

            for (int i = 0; i < tempDataTable.Count; i++)
            {
                if (tempDataTable.ElementAt(i).DoctorName == "")
                {
                    tempDataTable.ElementAt(i).DoctorName = "-";
                }
            }
            Session[Helper.ViewStateScannedData] = tempDataTable;

            fillFilterFormScannedMR();
        }
        else
        {
            StringBuilder scannedMRInner = new StringBuilder();
            scannedMRInner.Append("No Data");
            opd_data.InnerHtml = scannedMRInner.ToString();
            ipd_data.InnerHtml = scannedMRInner.ToString();
            mcu_data.InnerHtml = scannedMRInner.ToString();
        }
    }

    void fillFilterFormScannedMR()
    {
        var dateStart = DateTime.Parse(DateTextboxStart_scanned.Text.ToString());
        var dateEnd = DateTime.Parse(DateTextboxEnd_scanned.Text.ToString());

        List<ScannedMR> tempDataTable = new List<ScannedMR>();
        if (Session[Helper.ViewStateScannedData] != null)
        {
            tempDataTable = ((List<ScannedMR>)Session[Helper.ViewStateScannedData]).FindAll(x => x.AdmissionDate != "").FindAll(x => Convert.ToDateTime(x.AdmissionDate) >= dateStart && Convert.ToDateTime(x.AdmissionDate) <= dateEnd);

            //List<ScannedMR> tempSession = (List<ScannedMR>)Session[Helper.ViewStateScannedData];
            //List<ScannedMR> tempDateNotEmpty = tempSession.FindAll(x => x.AdmissionDate != "");
            //tempDataTable = tempDateNotEmpty.FindAll(x => Convert.ToDateTime(x.AdmissionDate) >= dateStart && Convert.ToDateTime(x.AdmissionDate) <= dateEnd);
        }

        StringBuilder form_filter = new StringBuilder();

        List<ScannedMR> dtScannedFilter = new List<ScannedMR>();
        dtScannedFilter = tempDataTable;
        conAll.Value = "0";
        DocAll.Value = "0";

        if (tempDataTable != null)
        {
            
            if (DateTextboxStart_scanned.Text != "")
            {
                dtScannedFilter = new List<ScannedMR>();
                //var dateTemp = DateTime.Parse(tempDataTable[0].AdmissionDate);
                //var dateStart = DateTime.Parse(DateTextboxStart_scanned.Text.ToString());
                //var dateEnd = DateTime.Parse(DateTextboxEnd_scanned.Text.ToString());

                dtScannedFilter = tempDataTable.FindAll(x => x.AdmissionDate != "").FindAll(x => Convert.ToDateTime(x.AdmissionDate) >= dateStart);
                var temp = dtScannedFilter;
                if (DateTextboxEnd_scanned.Text != "")
                {
                    dtScannedFilter = new List<ScannedMR>();
                    dtScannedFilter = temp.FindAll(x => x.AdmissionDate != "").FindAll(x => Convert.ToDateTime(x.AdmissionDate) <= dateEnd);
                }
            }
            else
            {
                if (DateTextboxEnd_scanned.Text != "")
                {
                    dtScannedFilter = tempDataTable.FindAll(x => DateTime.Parse(x.AdmissionDate) <= DateTime.Parse(DateTextboxEnd_scanned.Text.ToString()));
                }
            }

            if (selected_form_filter.Value != "")
            {
                List<string> formSelected = selected_form_filter.Value.Split(',').ToList();
                var temp = new List<ScannedMR>();
                if (dtScannedFilter.Count != 0)
                    temp = dtScannedFilter;
                else
                    temp = tempDataTable;

                dtScannedFilter = new List<ScannedMR>();

                foreach (string data in formSelected)
                {
                    List<string> formFlagSplit = data.Split('_').ToList();
                    String formName = "";
                    if (formFlagSplit.Count != 1)
                    {
                        for (int i = 0; i < formFlagSplit.Count; i++)
                        {
                            if (i != formFlagSplit.Count - 1)
                            {
                                if (formName == "")
                                    formName = formFlagSplit[i];
                                else
                                    formName = formName + " " + formFlagSplit[i];

                            }
                        }
                    }
                    else
                        formName = formFlagSplit[0];

                    dtScannedFilter.AddRange(temp.FindAll(x => x.FormTypeName == formName && x.AdmissionType == formFlagSplit[formFlagSplit.Count - 1]));
                }
            }

            if (selected_doctor_filter.Value != "")
            {
                List<string> doctorSelected = selected_doctor_filter.Value.Split(';').ToList();
                var temp = new List<ScannedMR>();
                if (dtScannedFilter.Count != 0)
                    temp = dtScannedFilter;
                else
                    temp = tempDataTable;

                dtScannedFilter = new List<ScannedMR>();

                foreach (string data in doctorSelected)
                {
                    List<string> doctorFlag = data.Split('_').ToList();
                    String doctorName = "";
                    if (doctorFlag.Count != 1)
                    {
                        for (int i = 0; i < doctorFlag.Count; i++)
                        {
                            if (i != doctorFlag.Count - 1)
                            {
                                if (doctorName == "")
                                    doctorName = doctorFlag[i];
                                else
                                    doctorName = doctorName + " " + doctorFlag[i];
                            }
                        }
                    }
                    else
                        doctorName = doctorFlag[0];

                    dtScannedFilter.AddRange(temp.FindAll(x => x.DoctorName == doctorName && x.AdmissionType == doctorFlag[doctorFlag.Count - 1]));
                }
            }
            
            //if (dtScannedFilter.Count != 0)
            //{

            /*==================================================== Add Filter for DOCTOR Scanned EMR ====================================================*/
            opd_scanned_filter.InnerHtml = "";
            ipd_scanned_filter.InnerHtml = "";
            mcu_scanned_filter.InnerHtml = "";

            List<string> formFlagDoctor = selected_doctor_filter.Value.Split(';').ToList();

            string selectAllDoctor = "javascript:selectAllDoctor();";
            form_filter.Append("<div class=\"pretty p-icon p-curve\">" +
                            "<input type=\"checkbox\" id=\"all_doctor_filter\" onclick=\"" + selectAllDoctor + "\" />" +
                            "<div class=\"state p-success\"> <i class=\"icon fa fa-check\"></i> <label style=\"font-size: 12px\" > </label > </div >" +
                            "</div>" +
                            "<label for=\"all_doctor_filter\">Show All</label>");
            select_all_doctor.InnerHtml = form_filter.ToString();

            /*---------------------------------------- Filter OPD Doctor ------------------------------------------------*/
            form_filter = new StringBuilder();

            var opdDoctor = tempDataTable.FindAll(x => x.AdmissionType.Equals("OPD") || x.AdmissionType.Equals("ED") || x.AdmissionType.Equals("OTHER"));
            form_filter.Append("<ul style=\"list-style:none; list-style-type:none; padding-left:15px\">");

            hf_all_doctor.Value = "";
            foreach (String data in opdDoctor.DistinctBy(x => x.DoctorName).Select(x => x.DoctorName))
            {
                string listSelectedForm = "javascript:doctorSelected('" + data.ToString() + "_OPD" + "');";
                var idInput = data.ToString() + "_OPD";
                var found = formFlagDoctor.Find(x => x == idInput);

                //if (selected_form_filter.Value != "")
                //    selected_form_filter.Value = selected_form_filter.Value + "," + idInput;
                //else
                //    selected_form_filter.Value = idInput;

                if (found != null)
                    form_filter.Append("<li><div class=\"pretty p-icon p-curve\"> <input id=\"" + idInput + "\" checked =\"checked\" type =\"checkbox\" value=" + data.ToString() + " onclick=\"" + listSelectedForm + "\" /> <div class=\"state p-success\"> <i class=\"icon fa fa-check\"></i> <label style=\"font - size: 12px\"> </label> </div> </div><label for=\"" + idInput + "\">" + data + "</label></li>");
                else
                    form_filter.Append("<li><div class=\"pretty p-icon p-curve\"> <input id=\"" + idInput + "\" type =\"checkbox\" value=" + data.ToString() + " onclick=\"" + listSelectedForm + "\" /> <div class=\"state p-success\"> <i class=\"icon fa fa-check\"></i> <label style=\"font - size: 12px\"> </label> </div> </div><label for=\"" + idInput + "\">" + data + "</label></li>");

                //if (hf_all_doctor.Value.Split(';').Length < dtScannedFilter.Count)
                //{
                    if (hf_all_doctor.Value != "")
                        hf_all_doctor.Value = hf_all_doctor.Value + ";" + idInput;
                    else
                        hf_all_doctor.Value = idInput;
                //}
            }

            form_filter.Append("</ul>");
            DocAll.Value = (0 + opdDoctor.DistinctBy(x => x.DoctorName).Count()).ToString();

            opd_doctor_filter.InnerHtml = form_filter.ToString();
            /*---------------------------------------- Filter OPD Doctor ------------------------------------------------*/

            /*---------------------------------------- Filter IPD Doctor ------------------------------------------------*/
            form_filter = new StringBuilder();

            var ipdDoctor = tempDataTable.FindAll(x => x.AdmissionType.Equals("IPD"));
            form_filter.Append("<ul style=\"list-style:none; list-style-type:none; padding-left:15px\">");

            foreach (String data in ipdDoctor.DistinctBy(x => x.DoctorName).Select(x => x.DoctorName))
            {
                string listSelectedForm = "javascript:doctorSelected('" + data.ToString() + "_IPD" + "');";
                var idInput = data.ToString() + "_IPD";
                var found = formFlagDoctor.Find(x => x == idInput);

                //if (selected_form_filter.Value != "")
                //    selected_form_filter.Value = selected_form_filter.Value + "," + idInput;
                //else
                //    selected_form_filter.Value = idInput;

                if (found != null)
                    form_filter.Append("<li><div class=\"pretty p-icon p-curve\"> <input id=\"" + idInput + "\" checked =\"checked\" type =\"checkbox\" value=" + data.ToString() + " onclick=\"" + listSelectedForm + "\" /> <div class=\"state p-success\"> <i class=\"icon fa fa-check\"></i> <label style=\"font - size: 12px\"> </label> </div> </div><label for=\"" + idInput + "\">" + data + "</label></li>");
                else
                    form_filter.Append("<li><div class=\"pretty p-icon p-curve\"> <input id=\"" + idInput + "\" type =\"checkbox\" value=" + data.ToString() + " onclick=\"" + listSelectedForm + "\" /> <div class=\"state p-success\"> <i class=\"icon fa fa-check\"></i> <label style=\"font - size: 12px\"> </label> </div> </div><label for=\"" + idInput + "\">" + data + "</label></li>");

                //if (hf_all_doctor.Value.Split(';').Length < dtScannedFilter.Count)
                //{
                    if (hf_all_doctor.Value != "")
                        hf_all_doctor.Value = hf_all_doctor.Value + ";" + idInput;
                    else
                        hf_all_doctor.Value = idInput;
                //}

            }
            form_filter.Append("</ul>");
            DocAll.Value = (Int64.Parse(DocAll.Value) + ipdDoctor.DistinctBy(x => x.DoctorName).Count()).ToString();

            ipd_doctor_filter.InnerHtml = form_filter.ToString();
            /*---------------------------------------- Filter IPD Doctor ------------------------------------------------*/

            /*---------------------------------------- Filter MCU Doctor ------------------------------------------------*/
            form_filter = new StringBuilder();

            var mcuDoctor = tempDataTable.FindAll(x => x.AdmissionType.Equals("MCU"));
            form_filter.Append("<ul style=\"list-style:none; list-style-type:none; padding-left:15px\">");

            foreach (String data in mcuDoctor.DistinctBy(x => x.DoctorName).Select(x => x.DoctorName))
            {
                string listSelectedForm = "javascript:doctorSelected('" + data.ToString() + "_MCU" + "');";
                var idInput = data.ToString() + "_MCU";
                var found = formFlagDoctor.Find(x => x == idInput);

                //if (selected_form_filter.Value != "")
                //    selected_form_filter.Value = selected_form_filter.Value + "," + idInput;
                //else
                //    selected_form_filter.Value = idInput;

                if (found != null)
                    form_filter.Append("<li><div class=\"pretty p-icon p-curve\"> <input id=\"" + idInput + "\" checked =\"checked\" type =\"checkbox\" value=" + data.ToString() + " onclick=\"" + listSelectedForm + "\" /> <div class=\"state p-success\"> <i class=\"icon fa fa-check\"></i> <label style=\"font - size: 12px\"> </label> </div> </div><label for=\"" + idInput + "\">" + data + "</label></li>");
                else
                    form_filter.Append("<li><div class=\"pretty p-icon p-curve\"> <input id=\"" + idInput + "\" type =\"checkbox\" value=" + data.ToString() + " onclick=\"" + listSelectedForm + "\" /> <div class=\"state p-success\"> <i class=\"icon fa fa-check\"></i> <label style=\"font - size: 12px\"> </label> </div> </div><label for=\"" + idInput + "\">" + data + "</label></li>");

                //if (hf_all_doctor.Value.Split(';').Length < dtScannedFilter.Count)
                //{
                    if (hf_all_doctor.Value != "")
                        hf_all_doctor.Value = hf_all_doctor.Value + ";" + idInput;
                    else
                        hf_all_doctor.Value = idInput;
                //}

            }
            form_filter.Append("</ul>");
            DocAll.Value = (Int64.Parse(DocAll.Value) + mcuDoctor.DistinctBy(x => x.DoctorName).Count()).ToString();

            mcu_doctor_filter.InnerHtml = form_filter.ToString();
            /*---------------------------------------- Filter IPD Doctor ------------------------------------------------*/
            /*==================================================== End Add Filter for DOCTOR Scanned EMR ====================================================*/

            /*==================================================== Add Filter for FORM ====================================================*/
            opd_scanned_filter.InnerHtml = "";
            ipd_scanned_filter.InnerHtml = "";
            mcu_scanned_filter.InnerHtml = "";
            //List<string> allform = tempDataTable.Select(x => x.FormTypeName).ToList();

            List<string> formFlag = selected_form_filter.Value.Split(',').ToList();
            form_filter = new StringBuilder();

            string selectAll = "javascript:selectAllForm();";
            form_filter.Append("<div class=\"pretty p-icon p-curve\">" +
                            "<input type=\"checkbox\" id=\"all_form_filter\" onclick=\"" + selectAll + "\" />" +
                            "<div class=\"state p-success\"> <i class=\"icon fa fa-check\"></i> <label style=\"font-size: 12px\" > </label > </div >" +
                            "</div>" +
                            "<label for=\"all_form_filter\">Show All</label>");
            select_all_form.InnerHtml = form_filter.ToString();
            /*---------------------------------------- Filter OPD Form ------------------------------------------------*/
            form_filter = new StringBuilder();

            var opd = tempDataTable.FindAll(x => x.AdmissionType.Equals("OPD") || x.AdmissionType.Equals("ED") || x.AdmissionType.Equals("OTHER"));
            form_filter.Append("<ul style=\"list-style:none; list-style-type:none; padding-left:15px\">");

            hf_all_form.Value = "";
            foreach (String data in opd.DistinctBy(x => x.FormTypeName).Select(x => x.FormTypeName))
            {
                string listSelectedForm = "javascript:formSelected('" + data.ToString() + "_OPD" + "');";
                var idInput = data.ToString() + "_OPD";
                var found = formFlag.Find(x => x == idInput);

                //if (selected_form_filter.Value != "")
                //    selected_form_filter.Value = selected_form_filter.Value + "," + idInput;
                //else
                //    selected_form_filter.Value = idInput;

                if (found != null)
                    form_filter.Append("<li><div class=\"pretty p-icon p-curve\"> <input id=\"" + idInput + "\" checked =\"checked\" type =\"checkbox\" value=" + data.ToString() + " onclick=\"" + listSelectedForm + "\" /> <div class=\"state p-success\"> <i class=\"icon fa fa-check\"></i> <label style=\"font - size: 12px\"> </label> </div> </div><label for=\"" + idInput + "\">" + data + "</label></li>");
                else
                    form_filter.Append("<li><div class=\"pretty p-icon p-curve\"> <input id=\"" + idInput + "\" type =\"checkbox\" value=" + data.ToString() + " onclick=\"" + listSelectedForm + "\" /> <div class=\"state p-success\"> <i class=\"icon fa fa-check\"></i> <label style=\"font - size: 12px\"> </label> </div> </div><label for=\"" + idInput + "\">" + data + "</label></li>");

                //if (hf_all_form.Value.Split(',').Length < dtScannedFilter.Count)
                //{
                    if (hf_all_form.Value != "")
                        hf_all_form.Value = hf_all_form.Value + "," + data.ToString() + "_OPD";
                    else
                        hf_all_form.Value = data.ToString() + "_OPD";
                //}

            }
            form_filter.Append("</ul>");
            conAll.Value = (0 + opd.DistinctBy(x => x.FormTypeName).Count()).ToString();

            opd_scanned_filter.InnerHtml = form_filter.ToString();
            /*---------------------------------------- Filter OPD Form ------------------------------------------------*/


            /*---------------------------------------- Filter IPD Form ------------------------------------------------*/
            form_filter = new StringBuilder();
            var ipd = tempDataTable.FindAll(x => x.AdmissionType.Equals("IPD"));
            form_filter.Append("<ul style=\"list-style:none; list-style-type:none; padding-left:15px\">");

            foreach (String data in ipd.DistinctBy(x => x.FormTypeName).Select(x => x.FormTypeName))
            {
                string listSelectedForm = "javascript:formSelected('" + data.ToString() + "_IPD" + "')";
                var idInput = data.ToString() + "_IPD";
                var found = formFlag.Find(x => x == idInput);

                //if (selected_form_filter.Value != "")
                //    selected_form_filter.Value = selected_form_filter.Value + "," + idInput;
                //else
                //    selected_form_filter.Value = idInput;

                if (found != null)
                    form_filter.Append("<li><div class=\"pretty p-icon p-curve\"> <input id=\"" + idInput + "\" checked =\"checked\" type =\"checkbox\" value=" + data.ToString() + " onclick=\"" + listSelectedForm + "\" /> <div class=\"state p-success\"> <i class=\"icon fa fa-check\"></i> <label style=\"font - size: 12px\"> </label> </div> </div><label for=\"" + idInput + "\">" + data + "</label></li>");
                else
                    form_filter.Append("<li><div class=\"pretty p-icon p-curve\"> <input id=\"" + idInput + "\" type =\"checkbox\" value=" + data.ToString() + " onclick=\"" + listSelectedForm + "\" /> <div class=\"state p-success\"> <i class=\"icon fa fa-check\"></i> <label style=\"font - size: 12px\"> </label> </div> </div><label for=\"" + idInput + "\">" + data + "</label></li>");

                //if (hf_all_form.Value.Split(',').Length < dtScannedFilter.Count)
                //{
                    if (hf_all_form.Value != "")
                        hf_all_form.Value = hf_all_form.Value + "," + data.ToString() + "_IPD";
                    else
                        hf_all_form.Value = data.ToString() + "_IPD";
                //}

            }
            form_filter.Append("</ul>");
            conAll.Value = (Int64.Parse(conAll.Value) + ipd.DistinctBy(x => x.FormTypeName).Count()).ToString();

            ipd_scanned_filter.InnerHtml = form_filter.ToString();
            /*---------------------------------------- Filter OPD Form ------------------------------------------------*/


            /*---------------------------------------- Filter MCU Form ------------------------------------------------*/
            form_filter = new StringBuilder();
            var mcu = tempDataTable.FindAll(x => x.AdmissionType.Equals("MCU"));
            form_filter.Append("<ul style=\"list-style:none; list-style-type:none; padding-left:15px\">");

            foreach (String data in mcu.DistinctBy(x => x.FormTypeName).Select(x => x.FormTypeName))
            {
                string listSelectedForm = "javascript:formSelected('" + data.ToString() + "_MCU" + "')";
                var idInput = data.ToString() + "_MCU";
                var found = formFlag.Find(x => x == idInput);

                //if (selected_form_filter.Value != "")
                //    selected_form_filter.Value = selected_form_filter.Value + "," + idInput;
                //else
                //    selected_form_filter.Value = idInput;

                if (found != null)
                    form_filter.Append("<li><div class=\"pretty p-icon p-curve\"> <input id=\"" + idInput + "\" checked =\"checked\" type =\"checkbox\" value=" + data.ToString() + " onclick=\"" + listSelectedForm + "\" /> <div class=\"state p-success\"> <i class=\"icon fa fa-check\"></i> <label style=\"font - size: 12px\"> </label> </div> </div><label for=\"" + idInput + "\">" + data + "</label></li>");
                else
                    form_filter.Append("<li><div class=\"pretty p-icon p-curve\"> <input id=\"" + idInput + "\" type =\"checkbox\" value=" + data.ToString() + " onclick=\"" + listSelectedForm + "\" /> <div class=\"state p-success\"> <i class=\"icon fa fa-check\"></i> <label style=\"font - size: 12px\"> </label> </div> </div><label for=\"" + idInput + "\">" + data + "</label></li>");

                //if (hf_all_form.Value.Split(',').Length < dtScannedFilter.Count)
                //{
                    if (hf_all_form.Value != "")
                        hf_all_form.Value = hf_all_form.Value + "," + data.ToString() + "_MCU";
                    else
                        hf_all_form.Value = data.ToString() + "_MCU";
                //}
            }

            form_filter.Append("</ul>");

            conAll.Value = (Int64.Parse(conAll.Value) + mcu.DistinctBy(x => x.FormTypeName).Count()).ToString();

            mcu_scanned_filter.InnerHtml = form_filter.ToString();
            /*---------------------------------------- Filter MCU Form ------------------------------------------------*/
            /*==================================================== Add Filter for FORM ====================================================*/

            //}
            loadScannedMR(dtScannedFilter);

        }
        else
        {
            form_filter.Append("No Data");
            opd_data.InnerHtml = form_filter.ToString();
            ipd_data.InnerHtml = form_filter.ToString();
            mcu_data.InnerHtml = form_filter.ToString();
        }
    }

    void loadScannedMR(List<ScannedMR> dataScan)
    {

        StringBuilder scannedMRInner = new StringBuilder();
        //var dataScanned = dataScan.DistinctBy(a => new { a.AdmissionDate, a.FormTypeName}).ToList();

        /* ----------------------------------------------- Outpatient / Emergency ----------------------------------------------- */
        List<ScannedMR> opd = dataScan.FindAll(x => x.AdmissionType == "OPD" || x.AdmissionType == "ED" || x.AdmissionType == "OTHER").OrderByDescending(s=> s.AdmissionDate).ToList();
        var dateAdmission = new DateTime();
        //selected_form_filter.Value = "";
        var localIP = GetLocalIPAddress();
        string imagePath = ResolveClientUrl("~/Images/PatientHistory/ic_newtab_blue.svg");

        if (opd.Count != 0)
        {
            scannedMRInner.Append("<div><table class=\"table table-striped table-condensed\">");
            foreach (ScannedMR data in opd)
            {
                if (data.AdmissionDate != "")
                {

                    string link = "http://" + data.Path.Replace("\\", "/").Replace(" ", "%20");
                    if (DateTime.Parse(data.AdmissionDate) == dateAdmission)
                    {
                        scannedMRInner.Append("<tr>" +
                                        "<td style=\"width:15%;\"></td>" +
                                        "<td style=\"color: blue;height: 25px; width:35%;\"><b>" + data.DoctorName + "</b></td>" +
                                        "<td style=\"color: blue;height: 25px; width:50%;\"><a href=\"" + link + "\" style=\"color: blue; text-decoration:underline;\" target=\"_blank\"><span><img src=\"" + imagePath + "\" /></span><b>" + data.FormTypeName + "</b></a></td>" +
                                        "</tr>");
                    }
                    else
                    {
                        scannedMRInner.Append("<tr>" +
                                        "<td style=\"color: blue;height: 25px; width:15%;\">" + DateTime.Parse(data.AdmissionDate).ToString("dd MMM yyyy") + "</td>" +
                                        "<td style=\"color: blue;height: 25px; width:35%;\"><b>" + data.DoctorName + "</b></td>" +
                                        "<td style=\"color: blue;height: 25px; width:50%;\"><a href=\"" + link + "\" style=\"color: blue; text-decoration:underline;\" target=\"_blank\"><span><img src=\"" + imagePath + "\" /></span><b>" + data.FormTypeName + "</b></a></td>" +
                                        "</tr>");
                    }
                    dateAdmission = DateTime.Parse(data.AdmissionDate);
                }
            }
            scannedMRInner.Append("</table></div>");
            opd_data.InnerHtml = scannedMRInner.ToString();
        }
        else
        {
            scannedMRInner.Append("No Data");
            opd_data.InnerHtml = scannedMRInner.ToString();
        }

        /* ----------------------------------------------- Outpatient / Emergency ----------------------------------------------- */

        /* ----------------------------------------------- Inpatient / Emergency ----------------------------------------------- */
        scannedMRInner = new StringBuilder();
        List<ScannedMR> ipd = dataScan.FindAll(x => x.AdmissionType == "IPD").OrderByDescending(s => s.AdmissionDate).ToList();
        if (ipd.Count != 0)
        {
            scannedMRInner.Append("<div><table class=\"table table-striped table-condensed\">");
            dateAdmission = new DateTime();
            foreach (ScannedMR data in ipd)
            {
                if (data.AdmissionDate != "")
                {
                    string link = "http://" + data.Path.Replace("\\", "/").Replace(" ", "%20");
                    if (DateTime.Parse(data.AdmissionDate) == dateAdmission)
                    {
                        scannedMRInner.Append("<tr>" +
                                        "<td style=\"width:15%;\"></td>" +
                                        "<td style=\"color: blue;height: 25px; width:35%;\"><b>" + data.DoctorName + "</b></td>" +
                                        "<td style=\"color: blue;height: 25px; width:50%;\"><a href=\"" + link + "\" style=\"color: blue; text-decoration:underline;\" target=\"_blank\"><span><img src=\"" + imagePath + "\" /></span><b>" + data.FormTypeName + "</b></a></td>" +
                                        "</tr>");
                    }
                    else
                    {
                        scannedMRInner.Append("<tr>" +
                                        "<td style=\"color: blue;height: 25px; width:15%;\">" + DateTime.Parse(data.AdmissionDate).ToString("dd MMM yyyy") + "</td>" +
                                        "<td style=\"color: blue;height: 25px; width:35%;\"><b>" + data.DoctorName + "</b></td>" +
                                        "<td style=\"color: blue;height: 25px; width:50%;\"><a href=\"" + link + "\" style=\"color: blue; text-decoration:underline;\" target=\"_blank\"><span><img src=\"" + imagePath + "\" /></span><b>" + data.FormTypeName + "</b></a></td>" +
                                        "</tr>");
                    }
                    dateAdmission = DateTime.Parse(data.AdmissionDate);
                }
            }
            scannedMRInner.Append("</table></div>");
            ipd_data.InnerHtml = scannedMRInner.ToString();
        }
        else
        {
            scannedMRInner.Append("No Data");
            ipd_data.InnerHtml = scannedMRInner.ToString();
        }
        /* ----------------------------------------------- Inpatient / Emergency ----------------------------------------------- */

        /* ----------------------------------------------- Medical Check Up / Emergency ----------------------------------------------- */
        scannedMRInner = new StringBuilder();
        List<ScannedMR> mcu = dataScan.FindAll(x => x.AdmissionType == "MCU").OrderByDescending(s => s.AdmissionDate).ToList();
        if (mcu.Count != 0)
        {
            scannedMRInner.Append("<div><table class=\"table table-striped table-condensed\">");
            dateAdmission = new DateTime();
            foreach (ScannedMR data in mcu)
            {
                if (data.AdmissionDate != "")
                {
                    string link = "http://" + data.Path.Replace("\\", "/").Replace(" ", "%20");
                    if (DateTime.Parse(data.AdmissionDate) == dateAdmission)
                    {
                        scannedMRInner.Append("<tr>" +
                                        "<td style=\"width:15%;\"></td>" +
                                        "<td style=\"color: blue;height: 25px; width:35%;\"><b>" + data.DoctorName + "</b></td>" +
                                        "<td style=\"color: blue;height: 25px; width:50%;\"><a href=\"" + link + "\" style=\"color: blue; text-decoration:underline;\" target=\"_blank\"><span><img src=\"" + imagePath + "\" /></span><b>" + data.FormTypeName + "</b></a></td>" +
                                        "</tr>");
                    }
                    else
                    {
                        scannedMRInner.Append("<tr>" +
                                        "<td style=\"color: blue;height: 25px; width:15%;\">" + DateTime.Parse(data.AdmissionDate).ToString("dd MMM yyyy") + "</td>" +
                                        "<td style=\"color: blue;height: 25px; width:35%;\"><b>" + data.DoctorName + "</b></td>" +
                                        "<td style=\"color: blue;height: 25px; width:50%;\"><a href=\"" + link + "\" style=\"color: blue; text-decoration:underline;\" target=\"_blank\"><span><img src=\"" + imagePath + "\" /></span><b>" + data.FormTypeName + "</b></a></td>" +
                                        "</tr>");
                    }
                    dateAdmission = DateTime.Parse(data.AdmissionDate);
                }
            }
            scannedMRInner.Append("</table></div>");
            mcu_data.InnerHtml = scannedMRInner.ToString();
        }
        else
        {
            scannedMRInner.Append("No Data");
            mcu_data.InnerHtml = scannedMRInner.ToString();
        }
        /* ----------------------------------------------- Medical Check Up / Emergency ----------------------------------------------- */
    }

    protected void btn_search_hopeEmr_Click(object sender, EventArgs e)
    {
        getHopeEmrData();
    }

    protected void src_doctorName_hopeEmr_TextChanged(object sender, EventArgs e)
    {
        //List<PatientHistoryHOPEemr> dataHopeEMR = (List<PatientHistoryHOPEemr>)Session[Helper.ViewStatePatientHistoryHOPEemr];
        //StringBuilder innerHtmlHopeEmr = new StringBuilder();

        //if (src_doctorName_hopeEmr.Text != "")
        //{
        //    List<PatientHistoryHOPEemr> dataFilterHopeEmr = dataHopeEMR.Where(x => x.entryUser.ToUpper().Contains(src_doctorName_hopeEmr.Text.ToUpper())).ToList();
        //    innerHtmlHopeEmr = loadDataHopeEmr(dataFilterHopeEmr);
        //}
        //else
        //{
        //    innerHtmlHopeEmr = loadDataHopeEmr(dataHopeEMR);
        //}

        //hope__emr.InnerHtml = innerHtmlHopeEmr.ToString();
    }

    protected void btn_search_other_unit_Click(object sender, EventArgs e)
    {
        //List<OtherUnitMR> otherUnitDataSession = (List<OtherUnitMR>)Session[Helper.ViewStateOtherUnitMR];
        var varResult = clsPatientHistory.getOtherUnitData(Int64.Parse(hfPatientId.Value.ToString()), long.Parse(hfOrgId.Value), int.Parse(ddl_year_other_mr.SelectedValue));
        var JsongetOtherUnitData = JsonConvert.DeserializeObject<ResultOtherUnitMR>(varResult.Result.ToString());

        List<OtherUnitMR> otherUnitDataSession = JsongetOtherUnitData.list;

        if (otherUnitDataSession != null)
        {
            List<OtherUnitMR> OtherUnitData = new List<OtherUnitMR>();
            if (DateTextboxStart_other.Text != "" || DateTextboxEnd_other.Text != "")
            {
                var tempOtherUnitData = new List<OtherUnitMR>();

                tempOtherUnitData = otherUnitDataSession.FindAll(x => x.AdmissionDate >= DateTime.Parse(DateTextboxStart_other.Text.ToString()));
                OtherUnitData = tempOtherUnitData;

                if (DateTextboxEnd_other.Text != "")
                {
                    OtherUnitData = tempOtherUnitData.FindAll(x => x.AdmissionDate <= DateTime.Parse(DateTextboxEnd_other.Text.ToString()).AddDays(1));
                }
            }
            else
            {
                OtherUnitData = otherUnitDataSession;
            }

            if (ddl_unit_other_mr.SelectedValue != "")
            {
                var temp = OtherUnitData;
                OtherUnitData = new List<OtherUnitMR>();
                OtherUnitData.AddRange(temp.FindAll(x => x.OrganizationCode.Equals(ddl_unit_other_mr.SelectedValue)));
            }

            StringBuilder otherUnitInnerHTML = new StringBuilder();
            if (OtherUnitData.Count != 0)
            {
                other_unit_emr_data.Visible = true;
                otherUnitInnerHTML = loadOtherUnitEMR(OtherUnitData);
                other_unit_emr_data.InnerHtml = otherUnitInnerHTML.ToString();
                status_other_unit_Emr.Value = "Not empty";
                img_noData_other_mr.Style.Add("display", "none");
            }
            else
            {
                other_unit_emr_data.Visible = false;
                status_other_unit_Emr.Value = "empty";
                img_noData_other_mr.Style.Add("display", "");                
            }
        }
        else
        {
            status_other_unit_Emr.Value = "empty";
            img_noData_other_mr.Style.Add("display", "");
        }
        List<string> unit_filter = new List<string>();
        unit_filter.AddRange(otherUnitDataSession.Select(x => x.OrganizationCode).Distinct());
        //var firstitem = ddl_unit_other_mr.Items[0];
        ddl_unit_other_mr.Items.Clear();
        //ddl_unit_other_mr.Items.Add(firstitem);

        foreach (string data in unit_filter)
        {
            ddl_unit_other_mr.Items.Insert(unit_filter.IndexOf(data), new ListItem { Text = data, Value = data });
        }
    }

    protected void btn_scanned_mr_Click(object sender, EventArgs e)
    {
        getScannedData();
        //fillFilterFormScannedMR();
        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "chechkAllFormType", "chechkAllFormType();", true);
        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "chechkAllDoctor", "chechkAllDoctor();", true);
    }

    protected void ddl_year_other_mr_TextChanged(object sender, EventArgs e)
    {
        getOtherUnitData(int.Parse(ddl_year_other_mr.SelectedValue));
    }

    //protected void txt_link_Click(object sender, EventArgs e)
    //{
        //string pdfPath = "\\10.85.129.54\d\SHARE\ArcMr\00020030\00020030;OPD;CATATAN PERKEMBANGAN TERINTEGRASI;20180904;.pdf";
        //Page.ClientScript.RegisterStartupScript(this.GetType(),"OpenWin", "<script type=text/javascript>window.open('" + pdfPath + "')</script>");

    //}

    protected void LB_hopeEMR_Click(object sender, EventArgs e)
    {
        //if (Session[Helper.ViewStatePatientHistoryHOPEemr] == null)
        //{
            getHopeEmrData();
        //}
    }

    protected void LB_scannedEMR_Click(object sender, EventArgs e)
    {
        //if (Session[Helper.ViewStateScannedData] == null)
        //{
            getScannedData();
        //}
    }

    protected void LB_otherEMR_Click(object sender, EventArgs e)
    {
        if (Session[Helper.ViewStateOtherUnitMR] == null)
        {
            getOtherUnitData(2);
        }
    }
}