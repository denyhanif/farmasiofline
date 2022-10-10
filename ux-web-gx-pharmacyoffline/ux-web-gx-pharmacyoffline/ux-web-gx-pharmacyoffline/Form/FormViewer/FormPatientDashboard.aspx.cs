using log4net;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Form_FormViewer_FormPatientDashboard : System.Web.UI.Page
{
    //protected static readonly ILog log = LogManager.GetLogger(typeof(Form_FormViewer_FormPatientDashboard));
    public string QSpatientid;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string en = Request.QueryString["en"];
            if (en != null)
            {
                QSpatientid = Helper.Decrypt(Request.QueryString["PatientId"]);
            }
            else
            {
                QSpatientid = Request.QueryString["PatientId"];
            }

            hfOrgId.Value = Request.QueryString["OrganizationId"];
            hfPatientId.Value = QSpatientid; // Request.QueryString["PatientId"];
            hfEncounterId.Value = Request.QueryString["EncounterId"];
            hfAdmissionId.Value = Request.QueryString["AdmissionId"];
            hfDoctorId.Value = Request.QueryString["DoctorId"];

            lblYear.Text = DateTime.Now.Year.ToString();
            btnNext.Enabled = false;
            btnNext.Style.Add("cursor", "not-allowed");

            getPatientData(long.Parse(hfOrgId.Value), long.Parse(hfPatientId.Value), long.Parse(hfAdmissionId.Value), hfEncounterId.Value);
            getAdmissionHistory(long.Parse(hfPatientId.Value), long.Parse(hfDoctorId.Value));
        }
    }

    void getPatientData(long ogrId, long PatientId, long admissionId, string EncounterId)
    {
        string rspn_status = "", rspn_message = "";

        try
        {
            var varResult = clsPatientDetail.GetPatientDashboard(ogrId, PatientId, admissionId, EncounterId);
            var JsongetMap = (JObject)JsonConvert.DeserializeObject<dynamic>(varResult.Result.ToString());

            rspn_status = JsongetMap.Property("status").Value.ToString();
            rspn_message = JsongetMap.Property("message").Value.ToString();

            if (rspn_status.ToLower() != "fail")
            {
                var objectdata = JsongetMap.Property("data").Value.ToString();
                PatientDashboard JsongetPatientHistory = JsonConvert.DeserializeObject<PatientDashboard>(objectdata);

                var hasiltindakan = JsongetPatientHistory.proceduresults;
                if (hasiltindakan.Count > 0)
                {
                    //DataTable dthasiltindakan = Helper.ToDataTable(hasiltindakan);
                    gvw_hasiltindakan.DataSource = hasiltindakan;
                    gvw_hasiltindakan.DataBind();
                    divriwayattindakan.Style.Add("display", "");

                    divkosong_procresult.Style.Add("display", "none");
                    divisi_procresult.Style.Add("display", "");
                }
                else
                {
                    divnotindakan.Style.Add("display", "");
                    divriwayattindakan.Style.Add("display", "none");

                    divkosong_procresult.Style.Add("display", "");
                    divisi_procresult.Style.Add("display", "none");
                    //flag_responsive++;
                }

                int flagalergikosong = 0;

                var PatientAllergy = JsongetPatientHistory.patienthealthinfo;
                DataTable dtAllergy = Helper.ToDataTable(PatientAllergy);
                dtAllergy.Columns[1].ColumnName = "allergy";
                if (dtAllergy.Select("other_health_info_type = 1").Count() > 0)
                {
                    DrugAllergy.DataSource = dtAllergy.Select("other_health_info_type = 1").CopyToDataTable();
                    DrugAllergy.DataBind();
                    Lblemptyaledrug.Style.Add("display", "none");

                    flagalergikosong = flagalergikosong + 1;
                }
                else
                {
                    DrugAllergy.DataSource = null;
                    DrugAllergy.DataBind();
                }

                if (dtAllergy.Select("other_health_info_type = 2").Count() > 0)
                {
                    FoodAllergy.DataSource = dtAllergy.Select("other_health_info_type = 2").CopyToDataTable();
                    FoodAllergy.DataBind();
                    Lblemptyalefood.Style.Add("display", "none");

                    flagalergikosong = flagalergikosong + 1;
                }
                else
                {
                    FoodAllergy.DataSource = null;
                    FoodAllergy.DataBind();
                }

                if (dtAllergy.Select("other_health_info_type = 7").Count() > 0)
                {
                    OtherAllergy.DataSource = dtAllergy.Select("other_health_info_type = 7").CopyToDataTable();
                    OtherAllergy.DataBind();
                    Lblemptyalelain.Style.Add("display", "none");

                    flagalergikosong = flagalergikosong + 1;
                }
                else
                {
                    OtherAllergy.DataSource = null;
                    OtherAllergy.DataBind();
                }

                if (flagalergikosong == 0)
                {
                    divkosong_allergy.Style.Add("display", "");
                    divisi_allergy.Style.Add("display", "none");
                    //flag_responsive++;
                }
                else
                {
                    divkosong_allergy.Style.Add("display", "none");
                    divisi_allergy.Style.Add("display", "");
                }


                List<ViewHealthInfo> CurrMedication = JsongetPatientHistory.patienthealthinfo.Where(y => y.other_health_info_type == 6).ToList();
                DataTable dtroutine = Helper.ToDataTable(CurrMedication);
                dtroutine.Columns[1].ColumnName = "current_medication";
                if (CurrMedication.Count() > 0)
                {
                    RepCurrentMedication.DataSource = dtroutine;
                    RepCurrentMedication.DataBind();
                    lblemptyroutinemed.Style.Add("display", "none");

                    divkosong_routinemed.Style.Add("display", "none");
                    divisi_routinemed.Style.Add("display", "");
                }
                else
                {
                    RepCurrentMedication.DataSource = null;
                    RepCurrentMedication.DataBind();

                    divkosong_routinemed.Style.Add("display", "");
                    divisi_routinemed.Style.Add("display", "none");
                    //flag_responsive++;
                }

                List<ViewNotification> listreminder = JsongetPatientHistory.patientnotification;
                hfjsonreminder.Value = new JavaScriptSerializer().Serialize(listreminder);

                if (listreminder.Count() > 0)
                {
                    gvw_reminder.DataSource = Helper.ToDataTable(listreminder);
                    gvw_reminder.DataBind();
                    lblemptyreminder.Style.Add("display", "none");

                    divkosong_reminder.Style.Add("display", "none");
                    divisi_reminder.Style.Add("display", "");
                }
                else
                {
                    gvw_reminder.DataSource = null;
                    gvw_reminder.DataBind();

                    divkosong_reminder.Style.Add("display", "");
                    divisi_reminder.Style.Add("display", "none");
                    //flag_responsive++;
                }
            }
            else
            {
                divnotindakan.Style.Add("display", "");
                divriwayattindakan.Style.Add("display", "none");

                divkosong_procresult.Style.Add("display", "");
                divisi_procresult.Style.Add("display", "none");

                divkosong_allergy.Style.Add("display", "");
                divisi_allergy.Style.Add("display", "none");

                divkosong_routinemed.Style.Add("display", "");
                divisi_routinemed.Style.Add("display", "none");

                divkosong_reminder.Style.Add("display", "");
                divisi_reminder.Style.Add("display", "none");
            }

        }
        catch (Exception ex)
        {
            throw ex;
            //log.Error(LogLibrary.Error("getPatientData", Helper.GetLoginUser(this), ex.InnerException.Message));
        }
        //log.Info(LogLibrary.Logging("E", "getPatientData", Helper.GetLoginUser(this), ""));
    }

    void getAdmissionHistory(long PatientId, long DocId)
    {
        //log.Info(LogLibrary.Logging("S", "getAdmissionHistory", Helper.GetLoginUser(this), ""));

        try
        {
            divContentReport.InnerHtml = "";

            long DoctorId = DocId;
            string Year = lblYear.Text.ToString();

            StringBuilder result = new StringBuilder();
            //Generate Header
            //if (isBahasa == "ENG")
            //{
                result.Append("<table border=\"1\" style=\"width:100%;border-color:#b9b9b9;border-style:solid; border-top: none;\" class=\"font-content-dashboard\"><tr class=\"table-sub-header-label\" style=\"height:32px;\"><td style=\"width:100px\"><b>Specialities</b></td><td style=\"width:100px\" colspan=\"4\">" +
                              "<b>January</b></td><td style=\"width:100px\"colspan=\"4\"><b>February</b></td><td style=\"width:100px\" colspan=\"4\">" +
                              "<b>March</b></td><td style=\"width:100px\" colspan=\"4\"><b>April</b></td><td style=\"width:100px\" colspan=\"4\">" +
                              "<b>May</b></td><td style=\"width:100px\" colspan=\"4\"><b>June</b></td><td style=\"width:100px\" colspan=\"4\">" +
                              "<b>July</b></td><td style=\"width:100px\" colspan=\"4\"><b>August</b></td><td style=\"width:100px\" colspan = \"4\">" +
                              "<b>September</b></td><td style=\"width:100px\" colspan=\"4\"><b>October</b></td><td style=\"width:100px\" colspan=\"4\">" +
                              "<b>November</b></td><td style=\"width:100px\" colspan=\"4\"><b>December</b></td></tr>");
            //}
            //else if (isBahasa == "IND")
            //{
            //    result.Append("<table border=\"1\" style=\"width:100%;border-color:#b9b9b9;border-style:solid; border-top: none;\" class=\"font-content-dashboard\"><tr class=\"table-sub-header-label\" style=\"height:32px;\"><td style=\"width:100px\"><b>Spesialis</b></td><td style=\"width:100px\" colspan=\"4\">" +
            //                  "<b>Januari</b></td><td style=\"width:100px\"colspan=\"4\"><b>Februari</b></td><td style=\"width:100px\" colspan=\"4\">" +
            //                  "<b>Maret</b></td><td style=\"width:100px\" colspan=\"4\"><b>April</b></td><td style=\"width:100px\" colspan=\"4\">" +
            //                  "<b>Mei</b></td><td style=\"width:100px\" colspan=\"4\"><b>Juni</b></td><td style=\"width:100px\" colspan=\"4\">" +
            //                  "<b>Juli</b></td><td style=\"width:100px\" colspan=\"4\"><b>Agustus</b></td><td style=\"width:100px\" colspan = \"4\">" +
            //                  "<b>September</b></td><td style=\"width:100px\" colspan=\"4\"><b>Oktober</b></td><td style=\"width:100px\" colspan=\"4\">" +
            //                  "<b>November</b></td><td style=\"width:100px\" colspan=\"4\"><b>Desember</b></td></tr>");
            //}

            //log.Debug(LogLibrary.Logging("S", "GetAdmissionHistory", Helper.GetLoginUser(this), ""));
            var varResult = clsPatientDetail.GetAdmissionHistory(PatientId, DoctorId, int.Parse(Year));
            ResultAdmissionHistory JsongetAdmissionHistory = JsonConvert.DeserializeObject<ResultAdmissionHistory>(varResult.Result.ToString());
            List<AdmissionHistory> DataAdmissionHistory = JsongetAdmissionHistory.list;
            //log.Debug(LogLibrary.Logging("S", "GetAdmissionHistory", Helper.GetLoginUser(this), JsongetAdmissionHistory.ToString()));

            DataTable Result = new DataTable();
            Result = Helper.ToDataTable(DataAdmissionHistory);

            if (Result.Rows.Count > 0)
            {
                Labeladmishis.Style.Add("display", "none");

                DataTable dtOPD = new DataTable();
                DataTable dtIPD = new DataTable();
                DataTable dtED = new DataTable();
                DataTable dtProcedure = new DataTable();

                if (Result.Select("Type = 'OPD'").Count() > 0)
                {
                    dtOPD = Result.Select("Type = 'OPD'").CopyToDataTable();
                }
                if (Result.Select("Type = 'IPD'").Count() > 0)
                {
                    dtIPD = Result.Select("Type = 'IPD'").CopyToDataTable();
                }
                if (Result.Select("Type = 'ED'").Count() > 0)
                {
                    dtED = Result.Select("Type = 'ED'").CopyToDataTable();
                }
                if (Result.Select("Type = 'PROCEDURE'").Count() > 0)
                {
                    dtProcedure = Result.Select("Type = 'PROCEDURE'").CopyToDataTable();
                }

                #region DUMMY
                //dtOPD.Columns.Add("Type");
                //dtOPD.Columns.Add("Specialty");
                //dtOPD.Columns.Add("AdmissionNo");
                //dtOPD.Columns.Add("AdmissionDate");
                //dtOPD.Columns.Add("AdmissionId");
                //dtOPD.Columns.Add("AdmissionMonth");
                //dtOPD.Columns.Add("AdmissionWeek");
                //dtOPD.Columns.Add("LabSalesOrderNo");
                //dtOPD.Columns.Add("RadSalesOrderNo");
                //dtOPD.Columns.Add("OrganizationId");
                //dtOPD.Columns.Add("OrgCd");
                //dtOPD.Columns.Add("DoctorName");
                //dtOPD.Columns.Add("isMyself");

                //dtOPD.Rows.Add(new Object[] { "OPD", "DENTIST", "OPA0000001", "31 January 2018", "12345", "1", "4", "123123", "234234", "2", "Dr.A", "0" });
                //dtOPD.Rows.Add(new Object[] { "OPD", "DENTIST", "OPA01230001", "1 March 2018", "434", "3", "1", "2342", "123458765", "27", "Dr.ACCC", "0" });
                //dtOPD.Rows.Add(new Object[] { "OPD", "GENERAL PRACTITIONER", "OPA0003453001", "1 January 2018", "356546", "1", "1", "-", "-", "4", "Dr.WWWW", "0" });
                //dtOPD.Rows.Add(new Object[] { "OPD", "GENERAL PRACTITIONER", "OPA234450001", "2 December 2018", "353", "12", "1", "-", "-", "21", "Dr.ZZsklvndkjfvndkjfnksjdfksdjfnksjdfnskjdfnksjdfnZZ", "0" });
                //dtOPD.Rows.Add(new Object[] { "OPD", "MEDICAL REHABILITATION", "OPA0045601", "12 December 2018", "2342344", "12", "2", "-", "-", "3", "Dr.B", "1" });
                //dtOPD.Rows.Add(new Object[] { "OPD", "MEDICAL REHABILITATION", "OPA00456301", "13 December 2018", "22342344", "12", "2", "-", "23434", "2", "Dr.B", "1" });
                #endregion

                if (dtOPD.Rows.Count > 0)
                {
                    //Generate OPD
                    result.Append("<tr style=\"background-color:#e67d0533\"><td style=\"text-aligh:left\"><b>OPD</b></td><td style=\"width:100px;\" colspan=\"4\"></td>" +
                                  "<td style=\"width:100px;\" colspan=\"4\"></td><td style=\"width:100px;\" colspan=\"4\"></td>" +
                                  "<td style=\"width:100px;\" colspan=\"4\"></td>" +
                                  "<td style=\"width:100px;\" colspan=\"4\"></td>" +
                                  "<td style=\"width:100px;\" colspan=\"4\"></td>" +
                                  "<td style=\"width:100px;\" colspan=\"4\"></td>" +
                                  "<td style=\"width:100px;\" colspan=\"4\"></td>" +
                                  "<td style=\"width:100px;\" colspan=\"4\"></td>" +
                                  "<td style=\"width:100px;\" colspan=\"4\"></td>" +
                                  "<td style=\"width:100px;\" colspan=\"4\"></td>" +
                                  "<td style=\"width:100px;\" colspan=\"4\"></td></tr>");

                    DataTable GetSpecialty = dtOPD.DefaultView.ToTable(true, "Specialty");

                    //int numberr = 0;
                    for (int i = 0; i < GetSpecialty.Rows.Count; i++)
                    {
                        result.Append("<tr><td style=\"text-align:left;padding-left:5px; text-transform:capitalize;\">" + GetSpecialty.Rows[i]["Specialty"].ToString().ToLower() + "</td>");

                        DataTable LoopAdmission = dtOPD.Select("Specialty = '" + GetSpecialty.Rows[i]["Specialty"].ToString() + "'").CopyToDataTable();

                        for (int month = 1; month <= 12; month++)
                        {
                            if (LoopAdmission.Select("AdmissionMonth = " + month).Count() > 0)
                            {
                                for (int week = 1; week <= 4; week++)
                                {
                                    if (LoopAdmission.Select("AdmissionMonth = " + month + " AND AdmissionWeek = " + week).Count() > 0)
                                    {
                                        DataTable LoopContent = LoopAdmission.Select("AdmissionMonth = " + month + " AND AdmissionWeek = " + week).CopyToDataTable();

                                        var ListAdmission = "";

                                        int CheckColor = 0;
                                        //numberr++;

                                        for (int loopcontent = 0; loopcontent < LoopContent.Rows.Count; loopcontent++)
                                        {
                                            if (loopcontent != 0)
                                            {
                                                ListAdmission += "|";
                                            }

                                            if (LoopContent.Rows[loopcontent]["isMyself"].ToString() == "1")
                                            {
                                                CheckColor = 1;
                                            }

                                            ListAdmission += DateTime.Parse(LoopContent.Rows[loopcontent]["AdmissionDate"].ToString()).ToString("dd MMM yyyy") + "#";

                                            if (LoopContent.Rows[loopcontent]["LabSalesOrderNo"].ToString() != "-")
                                            {
                                                ListAdmission += LoopContent.Rows[loopcontent]["LabSalesOrderNo"].ToString() + "#";
                                            }
                                            else if (LoopContent.Rows[loopcontent]["LabSalesOrderNo"].ToString() == "-")
                                            {
                                                ListAdmission += "-#";
                                            }
                                            if (LoopContent.Rows[loopcontent]["RadSalesOrderNo"].ToString() != "-")
                                            {
                                                ListAdmission += LoopContent.Rows[loopcontent]["RadSalesOrderNo"].ToString() + "#";
                                            }
                                            else if (LoopContent.Rows[loopcontent]["RadSalesOrderNo"].ToString() == "-")
                                            {
                                                ListAdmission += "-#";
                                            }
                                            ListAdmission += LoopContent.Rows[loopcontent]["encounterId"].ToString() + "#"
                                                + LoopContent.Rows[loopcontent]["AdmissionId"].ToString() + "#"
                                                + LoopContent.Rows[loopcontent]["DoctorName"].ToString() + "#"
                                                + LoopContent.Rows[loopcontent]["isMyself"].ToString() + "#"
                                                + LoopContent.Rows[loopcontent]["Diagnosis"].ToString() + "#"
                                                + LoopContent.Rows[loopcontent]["organizationId"].ToString();
                                        }

                                        string link = "javascript:Open('" + ListAdmission + "')";

                                        if (CheckColor == 0)
                                        {
                                            result.Append("<td style=\"width:20px;background-color:gray\"><button type=\"button\" title=\"click for detail\" onclick=\"" + link + "\" style=\"height:100%; width:100%; background-color:gray;border:none;margin:0 0 0 0\">&nbsp;</button>  </td>");
                                        }
                                        else
                                        {
                                            result.Append("<td style=\"width:20px;background-color:cornflowerblue\"><button type=\"button\" title=\"click for detail\" onclick=\"" + link + "\" style=\"height:100%; width:100%; background-color:cornflowerblue;border:none;margin:0 0 0 0\">&nbsp;</button> </td>");
                                        }

                                        //string link = "javascript:Open('" + ListAdmission + "','" + numberr + "')";
                                        //if (CheckColor == 0)
                                        //{
                                        //    result.Append("<td style=\"width:20px;background-color:gray\"><button type=\"button\" onclick=\"" + link + "\" style=\"height:100%; width:100%;background-color:gray;border:none;margin:0 0 0 0\">&nbsp;</button> <button id=\"mybtn" + numberr + "\" type=\"button\"  data-toggle=\"popover-x\" data-target=\"#myPopover" + numberr + "\" data-placement=\"top\">o</button> </td>");
                                        //}
                                        //else
                                        //{
                                        //    result.Append("<td style=\"width:20px;background-color:cornflowerblue\"><button type=\"button\" onclick=\"" + link + "\" style=\"height:100%; width:100%;background-color:cornflowerblue;border:none;margin:0 0 0 0\">&nbsp;</button> <button id=\"mybtn" + numberr + "\" type=\"button\" data-toggle=\"popover-x\" data-target=\"#myPopover" + numberr + "\" data-placement=\"top\">o</button> </td>");
                                        //}
                                    }
                                    else
                                    {
                                        result.Append("<td style=\"width:20px;\"></td>");
                                    }
                                }
                            }
                            else
                            {
                                result.Append("<td style=\"width:20px;\"></td>" +
                                              "<td style=\"width:20px;\"></td>" +
                                              "<td style=\"width:20px;\"></td>" +
                                              "<td style=\"width:20px;\"></td>");
                            }
                        }

                        result.Append("</tr>");
                    }
                }
                if (dtIPD.Rows.Count > 0)
                {
                    //Generate IPD
                    result.Append("<tr style=\"background-color:#e67d0533\"><td style=\"text-aligh:left\"><b>IPD</b></td><td style=\"width:100px;\" colspan=\"4\"></td>" +
                                  "<td style=\"width:100px;\" colspan=\"4\"></td><td style=\"width:100px;\" colspan=\"4\"></td>" +
                                  "<td style=\"width:100px;\" colspan=\"4\"></td>" +
                                  "<td style=\"width:100px;\" colspan=\"4\"></td>" +
                                  "<td style=\"width:100px;\" colspan=\"4\"></td>" +
                                  "<td style=\"width:100px;\" colspan=\"4\"></td>" +
                                  "<td style=\"width:100px;\" colspan=\"4\"></td>" +
                                  "<td style=\"width:100px;\" colspan=\"4\"></td>" +
                                  "<td style=\"width:100px;\" colspan=\"4\"></td>" +
                                  "<td style=\"width:100px;\" colspan=\"4\"></td>" +
                                  "<td style=\"width:100px;\" colspan=\"4\"></td></tr>");

                    result.Append("<tr><td style=\"text-align:left;padding-left:5px; text-transform:capitalize;\"><b>-</b></td>");

                    DataTable LoopAdmission = dtIPD.Select().CopyToDataTable();

                    for (int month = 1; month <= 12; month++)
                    {
                        if (LoopAdmission.Select("AdmissionMonth = " + month).Count() > 0)
                        {
                            for (int week = 1; week <= 4; week++)
                            {
                                if (LoopAdmission.Select("AdmissionMonth = " + month + " AND AdmissionWeek = " + week).Count() > 0)
                                {
                                    DataTable LoopContent = LoopAdmission.Select("AdmissionMonth = " + month + " AND AdmissionWeek = " + week).CopyToDataTable();

                                    var ListAdmission = "";

                                    int CheckColor = 0;

                                    for (int loopcontent = 0; loopcontent < LoopContent.Rows.Count; loopcontent++)
                                    {
                                        if (loopcontent != 0)
                                        {
                                            ListAdmission += "|";
                                        }

                                        if (LoopContent.Rows[loopcontent]["isMyself"].ToString() == "1")
                                        {
                                            CheckColor = 1;
                                        }

                                        ListAdmission += LoopContent.Rows[loopcontent]["AdmissionDate"].ToString() + "#";

                                        if (LoopContent.Rows[loopcontent]["LabSalesOrderNo"].ToString() != "-")
                                        {
                                            ListAdmission += LoopContent.Rows[loopcontent]["LabSalesOrderNo"].ToString() + "#";
                                        }
                                        else if (LoopContent.Rows[loopcontent]["LabSalesOrderNo"].ToString() == "-")
                                        {
                                            ListAdmission += "-#";
                                        }
                                        if (LoopContent.Rows[loopcontent]["RadSalesOrderNo"].ToString() != "-")
                                        {
                                            ListAdmission += LoopContent.Rows[loopcontent]["RadSalesOrderNo"].ToString() + "#";
                                        }
                                        else if (LoopContent.Rows[loopcontent]["RadSalesOrderNo"].ToString() == "-")
                                        {
                                            ListAdmission += "-#";
                                        }
                                        ListAdmission += LoopContent.Rows[loopcontent]["encounterId"].ToString() + "#"
                                            + LoopContent.Rows[loopcontent]["AdmissionId"].ToString() + "#"
                                            + LoopContent.Rows[loopcontent]["DoctorName"].ToString() + "#"
                                            + LoopContent.Rows[loopcontent]["isMyself"].ToString() + "#"
                                            + LoopContent.Rows[loopcontent]["Diagnosis"].ToString() + "#"
                                            + LoopContent.Rows[loopcontent]["organizationId"].ToString();
                                    }

                                    string link = "javascript:Open('" + ListAdmission + "')";

                                    if (CheckColor == 0)
                                    {
                                        result.Append("<td style=\"width:20px;background-color:gray\"><button type=\"button\" title=\"click for detail\" onclick=\"" + link + "\" style=\"height:100%; width:100%;background-color:gray;border:none;margin:0 0 0 0\">&nbsp;</button></td>");
                                    }
                                    else
                                    {
                                        result.Append("<td style=\"width:20px;background-color:cornflowerblue\"><button type=\"button\" title=\"click for detail\" onclick=\"" + link + "\" style=\"height:100%; width:100%;background-color:cornflowerblue;border:none;margin:0 0 0 0\">&nbsp;</button></td>");
                                    }
                                }
                                else
                                {
                                    result.Append("<td style=\"width:20px;\"></td>");
                                }
                            }
                        }
                        else
                        {
                            result.Append("<td style=\"width:20px;\"></td>" +
                                          "<td style=\"width:20px;\"></td>" +
                                          "<td style=\"width:20px;\"></td>" +
                                          "<td style=\"width:20px;\"></td>");
                        }
                    }

                    result.Append("</tr>");

                }
                if (dtED.Rows.Count > 0)
                {
                    //Generate ED
                    result.Append("<tr style=\"background-color:#e67d0533\"><td style=\"text-aligh:left\"><b>ED</b></td><td style=\"width:100px;\" colspan=\"4\"></td>" +
                                  "<td style=\"width:100px;\" colspan=\"4\"></td><td style=\"width:100px;\" colspan=\"4\"></td>" +
                                  "<td style=\"width:100px;\" colspan=\"4\"></td>" +
                                  "<td style=\"width:100px;\" colspan=\"4\"></td>" +
                                  "<td style=\"width:100px;\" colspan=\"4\"></td>" +
                                  "<td style=\"width:100px;\" colspan=\"4\"></td>" +
                                  "<td style=\"width:100px;\" colspan=\"4\"></td>" +
                                  "<td style=\"width:100px;\" colspan=\"4\"></td>" +
                                  "<td style=\"width:100px;\" colspan=\"4\"></td>" +
                                  "<td style=\"width:100px;\" colspan=\"4\"></td>" +
                                  "<td style=\"width:100px;\" colspan=\"4\"></td></tr>");

                    result.Append("<tr><td style=\"text-align:left;padding-left:5px; text-transform:capitalize;\"><b>-</b></td>");

                    DataTable LoopAdmission = dtED.Select().CopyToDataTable();

                    for (int month = 1; month <= 12; month++)
                    {
                        if (LoopAdmission.Select("AdmissionMonth = " + month).Count() > 0)
                        {
                            for (int week = 1; week <= 4; week++)
                            {
                                if (LoopAdmission.Select("AdmissionMonth = " + month + " AND AdmissionWeek = " + week).Count() > 0)
                                {
                                    DataTable LoopContent = LoopAdmission.Select("AdmissionMonth = " + month + " AND AdmissionWeek = " + week).CopyToDataTable();

                                    var ListAdmission = "";

                                    int CheckColor = 0;

                                    for (int loopcontent = 0; loopcontent < LoopContent.Rows.Count; loopcontent++)
                                    {
                                        if (loopcontent != 0)
                                        {
                                            ListAdmission += "|";
                                        }

                                        if (LoopContent.Rows[loopcontent]["isMyself"].ToString() == "1")
                                        {
                                            CheckColor = 1;
                                        }

                                        ListAdmission += LoopContent.Rows[loopcontent]["AdmissionDate"].ToString() + "#";

                                        if (LoopContent.Rows[loopcontent]["LabSalesOrderNo"].ToString() != "-")
                                        {
                                            ListAdmission += LoopContent.Rows[loopcontent]["LabSalesOrderNo"].ToString() + "#";
                                        }
                                        else if (LoopContent.Rows[loopcontent]["LabSalesOrderNo"].ToString() == "-")
                                        {
                                            ListAdmission += "-#";
                                        }
                                        if (LoopContent.Rows[loopcontent]["RadSalesOrderNo"].ToString() != "-")
                                        {
                                            ListAdmission += LoopContent.Rows[loopcontent]["RadSalesOrderNo"].ToString() + "#";
                                        }
                                        else if (LoopContent.Rows[loopcontent]["RadSalesOrderNo"].ToString() == "-")
                                        {
                                            ListAdmission += "-#";
                                        }
                                        ListAdmission += LoopContent.Rows[loopcontent]["encounterId"].ToString() + "#"
                                            + LoopContent.Rows[loopcontent]["AdmissionId"].ToString() + "#"
                                            + LoopContent.Rows[loopcontent]["DoctorName"].ToString() + "#"
                                            + LoopContent.Rows[loopcontent]["isMyself"].ToString() + "#"
                                            + LoopContent.Rows[loopcontent]["Diagnosis"].ToString() + "#"
                                            + LoopContent.Rows[loopcontent]["organizationId"].ToString();
                                    }

                                    string link = "javascript:Open('" + ListAdmission + "')";

                                    if (CheckColor == 0)
                                    {
                                        result.Append("<td style=\"width:20px;background-color:gray\"><button type=\"button\" title=\"click for detail\" onclick=\"" + link + "\" style=\"height:100%; width:100%;background-color:gray;border:none;margin:0 0 0 0\">&nbsp;</button></td>");
                                    }
                                    else
                                    {
                                        result.Append("<td style=\"width:20px;background-color:cornflowerblue\"><button type=\"button\" title=\"click for detail\" onclick=\"" + link + "\" style=\"height:100%; width:100%;background-color:cornflowerblue;border:none;margin:0 0 0 0\">&nbsp;</button></td>");
                                    }
                                }
                                else
                                {
                                    result.Append("<td style=\"width:20px;\"></td>");
                                }
                            }
                        }
                        else
                        {
                            result.Append("<td style=\"width:20px;\"></td>" +
                                          "<td style=\"width:20px;\"></td>" +
                                          "<td style=\"width:20px;\"></td>" +
                                          "<td style=\"width:20px;\"></td>");
                        }
                    }

                    result.Append("</tr>");
                }
                if (dtProcedure.Rows.Count > 0)
                {
                    //Generate Procedure
                    result.Append("<tr style=\"background-color:#e67d0533\"><td style=\"text-aligh:left\"><b>PROCEDURE</b></td><td style=\"width:100px;\" colspan=\"4\"></td>" +
                                  "<td style=\"width:100px;\" colspan=\"4\"></td><td style=\"width:100px;\" colspan=\"4\"></td>" +
                                  "<td style=\"width:100px;\" colspan=\"4\"></td>" +
                                  "<td style=\"width:100px;\" colspan=\"4\"></td>" +
                                  "<td style=\"width:100px;\" colspan=\"4\"></td>" +
                                  "<td style=\"width:100px;\" colspan=\"4\"></td>" +
                                  "<td style=\"width:100px;\" colspan=\"4\"></td>" +
                                  "<td style=\"width:100px;\" colspan=\"4\"></td>" +
                                  "<td style=\"width:100px;\" colspan=\"4\"></td>" +
                                  "<td style=\"width:100px;\" colspan=\"4\"></td>" +
                                  "<td style=\"width:100px;\" colspan=\"4\"></td></tr>");

                    DataTable GetSpecialty = dtProcedure.DefaultView.ToTable(true, "Specialty");

                    for (int i = 0; i < GetSpecialty.Rows.Count; i++)
                    {
                        result.Append("<tr><td style=\"text-align:left;padding-left:5px; text-transform:capitalize;\">" + GetSpecialty.Rows[i]["Specialty"].ToString().ToLower() + "</td>");

                        DataTable LoopAdmission = dtProcedure.Select("Specialty = '" + GetSpecialty.Rows[i]["Specialty"].ToString() + "'").CopyToDataTable();

                        for (int month = 1; month <= 12; month++)
                        {
                            if (LoopAdmission.Select("AdmissionMonth = " + month).Count() > 0)
                            {
                                for (int week = 1; week <= 4; week++)
                                {
                                    if (LoopAdmission.Select("AdmissionMonth = " + month + " AND AdmissionWeek = " + week).Count() > 0)
                                    {
                                        DataTable LoopContent = LoopAdmission.Select("AdmissionMonth = " + month + " AND AdmissionWeek = " + week).CopyToDataTable();

                                        var ListAdmission = "";

                                        int CheckColor = 0;

                                        for (int loopcontent = 0; loopcontent < LoopContent.Rows.Count; loopcontent++)
                                        {
                                            if (loopcontent != 0)
                                            {
                                                ListAdmission += "|";
                                            }

                                            if (LoopContent.Rows[loopcontent]["isMyself"].ToString() == "1")
                                            {
                                                CheckColor = 1;
                                            }

                                            ListAdmission += LoopContent.Rows[loopcontent]["AdmissionDate"].ToString() + "#";

                                            if (LoopContent.Rows[loopcontent]["LabSalesOrderNo"].ToString() != "-")
                                            {
                                                ListAdmission += LoopContent.Rows[loopcontent]["LabSalesOrderNo"].ToString() + "#";
                                            }
                                            else if (LoopContent.Rows[loopcontent]["LabSalesOrderNo"].ToString() == "-")
                                            {
                                                ListAdmission += "-#";
                                            }
                                            if (LoopContent.Rows[loopcontent]["RadSalesOrderNo"].ToString() != "-")
                                            {
                                                ListAdmission += LoopContent.Rows[loopcontent]["RadSalesOrderNo"].ToString() + "#";
                                            }
                                            else if (LoopContent.Rows[loopcontent]["RadSalesOrderNo"].ToString() == "-")
                                            {
                                                ListAdmission += "-#";
                                            }
                                            ListAdmission += LoopContent.Rows[loopcontent]["encounterId"].ToString() + "#"
                                                + LoopContent.Rows[loopcontent]["AdmissionId"].ToString() + "#"
                                                + LoopContent.Rows[loopcontent]["DoctorName"].ToString() + "#"
                                                + LoopContent.Rows[loopcontent]["isMyself"].ToString() + "#"
                                                + LoopContent.Rows[loopcontent]["Diagnosis"].ToString() + "#"
                                                + LoopContent.Rows[loopcontent]["organizationId"].ToString();
                                        }

                                        string link = "javascript:Open('" + ListAdmission + "')";

                                        if (CheckColor == 0)
                                        {
                                            result.Append("<td style=\"width:20px;background-color:gray\"><button type=\"button\" title=\"click for detail\" onclick=\"" + link + "\" style=\"height:100%; width:100%;background-color:gray;border:none;margin:0 0 0 0\">&nbsp;</button></td>");
                                        }
                                        else
                                        {
                                            result.Append("<td style=\"width:20px;background-color:cornflowerblue\"><button type=\"button\" title=\"click for detail\" onclick=\"" + link + "\" style=\"height:100%; width:100%;background-color:cornflowerblue;border:none;margin:0 0 0 0\">&nbsp;</button></td>");
                                        }
                                    }
                                    else
                                    {
                                        result.Append("<td style=\"width:20px;\"></td>");
                                    }
                                }
                            }
                            else
                            {
                                result.Append("<td style=\"width:20px;\"></td>" +
                                              "<td style=\"width:20px;\"></td>" +
                                              "<td style=\"width:20px;\"></td>" +
                                              "<td style=\"width:20px;\"></td>");
                            }
                        }

                        result.Append("</tr>");
                    }
                }
            }
            else
            {
                Labeladmishis.Style.Add("display", "");
            }
            result.Append("</table>");
            divContentReport.InnerHtml = result.ToString();

        }
        catch (Exception ex)
        {
            throw ex;
            //log.Error(LogLibrary.Error("getAdmissionHistory", Helper.GetLoginUser(this), ex.InnerException.Message));
        }
        //log.Info(LogLibrary.Logging("E", "getAdmissionHistory", Helper.GetLoginUser(this), ""));

    }

    protected void btnNext_Click(object sender, EventArgs e)
    {
        int existingYear = int.Parse(lblYear.Text.ToString());
        lblYear.Text = (existingYear + 1).ToString();
        if ((int.Parse(lblYear.Text.ToString())) == DateTime.Now.Year)
        {
            btnNext.Enabled = false;
            btnNext.Style.Add("cursor", "not-allowed");
        }
        getAdmissionHistory(long.Parse(hfPatientId.Value), long.Parse(hfDoctorId.Value));
    }

    protected void btnPrev_Click(object sender, EventArgs e)
    {
        int existingYear = int.Parse(lblYear.Text.ToString());
        lblYear.Text = (existingYear - 1).ToString();
        btnNext.Enabled = true;
        btnNext.Style.Add("cursor", "pointer");
        getAdmissionHistory(long.Parse(hfPatientId.Value), long.Parse(hfDoctorId.Value));
    }
}