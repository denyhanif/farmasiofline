using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using log4net;
using System.Globalization;
using System.Data.SqlClient;
using System.Configuration;
using System.Reflection;
using System.Text;
using SiloamConfig;

public partial class Form_FormWorklistIpd : System.Web.UI.Page
{
    protected static readonly ILog log = LogManager.GetLogger(typeof(Form_FormWorklistIpd));

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            hfguidadditional.Value = Guid.NewGuid().ToString();
            //if (Helper.organizationId == "")
            //{
            //    Response.Redirect("~/Form/General/Login.aspx", false);
            //}
            //else
            //{
            //    if (Session[Helper.SessionWard + hfguidadditional.Value] == null)
            //    {
            //        var GetWard = clsWard.GetWard(Helper.organizationId);
            //        var ListWard = JsonConvert.DeserializeObject<ListWard>(GetWard.Result.ToString());

            //        List<Ward> listWard = ListWard.list;
            //        Session[Helper.SessionWard + hfguidadditional.Value] = listWard;
            //    }
                //textboxdateto.Text = DateTime.Now.ToString("dd-MMM-yyyy");
                
            //}

            if (Request.QueryString["OrganizationId"] != null && Request.QueryString["UserName"] != null)
            {
                //ConfigurationManager.AppSettings["urlDischarge"] = "http://10.85.129.54:5600";
                //ConfigurationManager.AppSettings["urlHISDataCollection"] = "http://10.85.129.54:5506";
                //development
                //ConfigurationManager.AppSettings["urlDischarge"] = "http://10.83.254.38:5600";
                //ConfigurationManager.AppSettings["urlHISDataCollection"] = "http://10.83.254.38:5506";
                //production
                //ConfigurationManager.AppSettings["urlDischarge"] = "http://10.85.138.26:5600";
                //ConfigurationManager.AppSettings["urlHISDataCollection"] = "http://10.85.138.5:5506";

                ConfigurationManager.AppSettings["urlDischarge"] = SiloamConfig.Functions.GetValue("urlDischarge").ToString();
                ConfigurationManager.AppSettings["urlHISDataCollection"] = SiloamConfig.Functions.GetValue("urlHISDataCollection").ToString();

                long OrganizationId = long.Parse(Request.QueryString["OrganizationId"]);
                string UserName = Request.QueryString["UserName"].ToString();

                hfName.Value = UserName;
                hfOrganizationId.Value = OrganizationId.ToString();

                getCountPatient();
                GetWorklistProcess();

                List<WardCollection> tempwardcoll = (List<WardCollection>)Session[Helper.wardcollection + hfguidadditional.Value];
                ddlWardListProcess.DataSource = tempwardcoll;
                ddlWardListProcess.DataTextField = "WardName";
                ddlWardListProcess.DataValueField = "WardId";
                ddlWardListProcess.DataBind();
                ddlWardListProcess.Items.Insert(0, new ListItem("Semua Bangsal", "0"));

                divDischargeProcess.Visible = true;
                divprocess.Style.Add("border", "solid #2A3593");

                divplan.Attributes["class"] = "col-sm-2 btnFilter";
                divprocess.Attributes["class"] = "col-sm-2 btnClickCSS";
                divdone.Attributes["class"] = "col-sm-2 btnFilter";

                divDischargePlan.Visible = false;

                divDischargeDone.Visible = false;

                if (txtdateprocess.Text == "")
                {
                    btnClearDateProcess.Visible = false;
                    btnShowCalendarProcess.Visible = true;
                }
                else
                {
                    btnClearDateProcess.Visible = true;
                    btnShowCalendarProcess.Visible = false;
                    //btnClearDatePlan.Attributes.Add("style", "display:''");
                }
                //initializevalue(OrganizationId, PatientId, AdmissionId, EncounterId);
            }
            else
            {
                string close = @"<script type='text/javascript'>
                                window.returnValue = true;
                                window.close();
                                </script>";
                base.Response.Write(close);
            }
        }
    }


    //================================== DISCHARGE PLAN =================================

    protected void getListPatient()
    {
        try
        {
            log.Info(LogLibrary.Logging("S", "getListPatient", hfName.Value, ""));

            List<ViewWorklistDischarge> listWorklistDischarge = new List<ViewWorklistDischarge>();
            lblDateRefreshWorklist.Text = DateTime.Now.ToString("dd MMM yyyy, HH:mm");
            textboxdateto.Text = "";
            if (long.Parse(hfOrganizationId.Value) != 0)
            {
                long wardId = 0;
                if (ddlWardlist.SelectedValue != "")
                    wardId = long.Parse(ddlWardlist.SelectedValue);
                
                log.Debug(LogLibrary.Logging("S", "getListPatientDischarge", hfName.Value, ""));
                var worklist = clsWorklist.getListPatientDischarge(long.Parse(hfOrganizationId.Value), wardId, txtFindPatient.Text);
                var getTotalWorklist = JsonConvert.DeserializeObject<ListViewWorklistDischarge>(worklist.Result.ToString());
                log.Debug(LogLibrary.Logging("E", "getListPatientDischarge", hfName.Value, getTotalWorklist.ToString()));

                if (getTotalWorklist != null)
                {
                    listWorklistDischarge = getTotalWorklist.list.Where(y => y.IsLate != "1").ToList();

                    lblCountPlan.Text = " (" + listWorklistDischarge.Where(y => y.IsPrimary == true && y.IsLate != "1").Count().ToString() + ") ";

                    List<long> WardCollection = (from x in listWorklistDischarge
                                                 select x.WardId).Distinct().ToList();

                    Session[Helper.CollectionDischargePlan + hfguidadditional.Value] = listWorklistDischarge;
                    List<WardCollection> tempwardcoll = new List<WardCollection>();

                    if (Session[Helper.SessionWard + hfguidadditional.Value] == null)
                    {
                        var GetWard = clsWard.GetWard(long.Parse(hfOrganizationId.Value));
                        var ListWard = JsonConvert.DeserializeObject<ListWard>(GetWard.Result.ToString());

                        Session[Helper.SessionWard + hfguidadditional.Value] = ListWard.list;
                    }


                    List<Ward> listWard = (List<Ward>)Session[Helper.SessionWard + hfguidadditional.Value];

                    foreach (var x in WardCollection)
                    {
                        WardCollection temp = new WardCollection();
                        temp.WardId = x;
                        temp.WardName = listWard.Find(y => y.WardId == x).Name;
                        tempwardcoll.Add(temp);
                    }
                    Session[Helper.wardcollection + hfguidadditional.Value] = tempwardcoll;

                    rptDischargePlan.DataSource = tempwardcoll;
                    rptDischargePlan.DataBind();

                }
                else
                {
                    ShowToastr("Data tidak ditemukan.", "error", "error");
                }
            }
            else
            {
                {
                    //Response.Redirect(String.Format("~/Form/General/Login?action=clear"));
                    Context.ApplicationInstance.CompleteRequest();
                    return;
                }
            }
            log.Info(LogLibrary.Logging("E", "getListPatient", hfName.Value, "Finish getListPatient"));
        }
        catch (Exception ex)
        {
            ShowToastr("Terjadi kesalahan pada sistem. Mohon hubungi staf IT di rumah sakit Anda.", "error", "error");
            log.Error(LogLibrary.Error("getListPatient", hfName.Value, ex.Message));
        }
    }

    protected void getCountPatient()
    {
        try
        {
            log.Info(LogLibrary.Logging("S", "getCountPatient", hfName.Value, ""));

            log.Debug(LogLibrary.Logging("S", "getCountPatient", hfName.Value, ""));
            var worklistPatient = clsWorklist.getCountPatient(long.Parse(hfOrganizationId.Value), 0);
            var getTotalWorklist = JsonConvert.DeserializeObject<ResultCountPatient>(worklistPatient.Result.ToString());
            log.Debug(LogLibrary.Logging("E", "getCountPatient", hfName.Value, getTotalWorklist.ToString()));

            if (getTotalWorklist.list != null)
            {
                patientotal.Text = getTotalWorklist.list.countplan.ToString();
                
                processtotal.Text = getTotalWorklist.list.countprocess.ToString();

                donetotal.Text = getTotalWorklist.list.countdone.ToString();
            }
            else
            {
                patientotal.Text = "0";
                processtotal.Text = "0";
                donetotal.Text = "0";
            }

            log.Info(LogLibrary.Logging("E", "getCountPatient", hfName.Value, "Finish getCountPatient"));
        }
        catch (Exception ex)
        {
            log.Error(LogLibrary.Error("btnPreview_click", hfName.Value, ex.Message));
        }
    }

    //================================== DISCHARGE PROCESS =================================

    protected void GetWorklistProcess()
    {
        try
        {
            log.Info(LogLibrary.Logging("S", "GetWorklistProcess", hfName.Value, ""));

            int lateduration;
            int latedurationinsurance;
            int latedurationinvoice;
            int latedurationtotalprivate;
            int latedurationtotalpayer;

            log.Debug(LogLibrary.Logging("S", "GetOrganizationSetting", hfName.Value, ""));
            var orgsetting = clsOrganizationSetting.GetOrganizationSetting(long.Parse(hfOrganizationId.Value));
            var jsonorgsetting = JsonConvert.DeserializeObject<ResultViewOrganizationSetting>(orgsetting.Result.ToString());
            log.Debug(LogLibrary.Logging("E", "GetOrganizationSetting", hfName.Value, jsonorgsetting.ToString()));

            if (jsonorgsetting.list.Count() > 0)
            {
                lateduration = int.Parse(jsonorgsetting.list.Find(y => y.setting_name == "LATE_SUBDISCHARGE").setting_value);
                latedurationinsurance = int.Parse(jsonorgsetting.list.Find(y => y.setting_name == "LATE_INSURANCE").setting_value);
                latedurationinvoice = int.Parse(jsonorgsetting.list.Find(y => y.setting_name == "LATE_INVOICE").setting_value);
                latedurationtotalprivate = int.Parse(jsonorgsetting.list.Find(y => y.setting_name == "LATE_TOTALPRIVATE").setting_value);
                latedurationtotalpayer = int.Parse(jsonorgsetting.list.Find(y => y.setting_name == "LATE_TOTALPAYER").setting_value);
            }
            else
            {
                lateduration = 20; //warna merah untuk service dan item
                latedurationinsurance = 20; //warna merah untuk email dan confirm
                latedurationinvoice = 20; //warna merah invoice selesai
                latedurationtotalprivate = 30; //warna merah untuk lama kerja pasien private
                latedurationtotalpayer = 120; //warna merah untuk lama kerja pasien with payer
            }

            lblDateRefreshProcess.Text = DateTime.Now.ToString("dd MMM yyyy, HH:mm");
            List<ViewDischargeRequest> viewrequest = new List<ViewDischargeRequest>();
            long wardId = 0;
            if (ddlWardListProcess.SelectedValue != "")
                wardId = long.Parse(ddlWardListProcess.SelectedValue);
            var dischargeprocess = clsWorklist.getListDischargeProcess(long.Parse(hfOrganizationId.Value), DateTime.Now.ToString("yyyy-MM-dd"), wardId, txtsearchprocess.Text, 2);
            var jsondischargeprocess = JsonConvert.DeserializeObject<ResultListDischargeProcess>(dischargeprocess.Result.ToString());
            if (jsondischargeprocess.list.dischargerequests != null)
            {
                if (jsondischargeprocess.list.dischargerequests.Where(y => y.ArInvoiceId == 0).Count() > 0)
                {
                    List<DischargeRequest> tempview = new List<DischargeRequest>();
                    tempview = jsondischargeprocess.list.dischargerequests.Where(y => y.ArInvoiceId == 0).ToList();
                    lblCountProcess.Text = " (" + jsondischargeprocess.list.dischargerequests.Where(y => y.ArInvoiceId == 0 && y.IsPrimary == true).Count().ToString() + ") ";
                    foreach (var x in tempview)
                    {

                        if (x.IsPrimary == true)
                        {
                            ViewDischargeRequest tempviewrequest = new ViewDischargeRequest();
                            tempviewrequest.SubmitDate = x.SubmitDate;
                            tempviewrequest.isShowDate = x.isShowDate;
                            tempviewrequest.WorklistDate = x.WorklistDate;
                            tempviewrequest.WorklistId = x.WorklistId;
                            tempviewrequest.ProcessId = x.ProcessId;
                            tempviewrequest.AdmissionId = x.AdmissionId;
                            tempviewrequest.AdmissionNo = x.AdmissionNo;
                            tempviewrequest.PatientId = x.PatientId;
                            tempviewrequest.PatientName = x.PatientName;
                            tempviewrequest.WardId = x.WardId;
                            tempviewrequest.islate = x.islate;
                            if (x.PayerName.Length > 50)
                                tempviewrequest.PayerName = x.PayerName.Substring(0, 50) + " ...";
                            else
                                tempviewrequest.PayerName = x.PayerName;
                            tempviewrequest.DoctorId = x.DoctorId;
                            tempviewrequest.DoctorName = x.DoctorName;
                            tempviewrequest.AdditionalNotes = x.AdditionalNotes;
                            tempviewrequest.ArInvoiceId = x.ArInvoiceId;
                            tempviewrequest.IsPrimary = x.IsPrimary;
                            tempviewrequest.IsPrescription = x.IsPrescription;
                            tempviewrequest.IsRetur = x.IsRetur;
                            tempviewrequest.EmailDate = x.EmailDate;
                            tempviewrequest.ConfirmDate = x.ConfirmDate;
                            tempviewrequest.InvoiceDate = x.InvoiceDate;
                            tempviewrequest.FUPatient = x.FUPatient;
                            tempviewrequest.OPDControl = x.OPDControl;
                            tempviewrequest.localMrNo = x.LocalMrNo;
                            tempviewrequest.birthDate = x.BirthDate.ToString("dd MMM yyyy");
                            tempviewrequest.roomNo = x.RoomNo;
                            tempviewrequest.FlagDischarged = x.FlagDischarged;
                            tempviewrequest.ModifiedDate = x.ModifiedDate;


                            if (jsondischargeprocess.list.subdischarges.Where(y => y.WorklistId == x.WorklistId && y.SubDischargeTypeId == 2).Count() > 0)
                            {
                                tempviewrequest.SubDateBed = jsondischargeprocess.list.subdischarges.Find(y => y.WorklistId == x.WorklistId && y.SubDischargeTypeId == 2).SubDate;
                            }
                            else
                            {
                                tempviewrequest.SubDateBed = DateTime.Parse("01/01/0001 00:00:00").ToString("dd/MM/yyyy HH:mm:ss");
                            }

                            if (jsondischargeprocess.list.subdischarges.Where(y => y.WorklistId == x.WorklistId && y.SubDischargeTypeId == 4).Count() > 0)
                            {
                                tempviewrequest.SubDateService = jsondischargeprocess.list.subdischarges.Find(y => y.WorklistId == x.WorklistId && y.SubDischargeTypeId == 4).SubDate;
                            }
                            else
                            {
                                tempviewrequest.SubDateService = DateTime.Parse("01/01/0001 00:00:00").ToString("dd/MM/yyyy HH:mm:ss");
                            }

                            if (jsondischargeprocess.list.subdischarges.Where(y => y.WorklistId == x.WorklistId && y.SubDischargeTypeId == 8).Count() > 0)
                            {
                                tempviewrequest.SubDateItem = jsondischargeprocess.list.subdischarges.Find(y => y.WorklistId == x.WorklistId && y.SubDischargeTypeId == 8).SubDate;
                                tempviewrequest.IsNeedPrescription = true;
                            }
                            else
                            {
                                tempviewrequest.SubDateItem = DateTime.Parse("01/01/0001 00:00:00").ToString("dd/MM/yyyy HH:mm:ss");
                                tempviewrequest.IsNeedPrescription = true;
                            }


                            //if (jsondischargeprocess.list.dischargerequests.Where(y => y.WorklistId == x.WorklistId).Count() > 0)
                            //{
                            //    if (jsondischargeprocess.list.subdischarges.Where(y => y.WorklistId == x.WorklistId && y.SubDischargeTypeId == 8).Count() > 0)
                            //    {
                            //        tempviewrequest.SubDateItem = jsondischargeprocess.list.subdischarges.Find(y => y.WorklistId == x.WorklistId && y.SubDischargeTypeId == 8).SubDate;
                            //    }
                            //    else
                            //    {
                            //        tempviewrequest.SubDateItem = DateTime.Parse("01/01/0001 00:00:00").ToString("dd/MM/yyyy HH:mm:ss");
                            //    }

                            //    if (jsondischargeprocess.list.dischargerequests.Where(y => y.IsPrescription == true && y.WorklistId == x.WorklistId).Count() == 0)
                            //    {
                            //        if (jsondischargeprocess.list.dischargerequests.Where(y => y.IsRetur == true && y.WorklistId == x.WorklistId).Count() == 0)
                            //        {
                            //            tempviewrequest.SubDateItem = DateTime.Parse("01/01/0001 00:00:00").ToString("dd/MM/yyyy HH:mm:ss");
                            //            tempviewrequest.IsNeedPrescription = false;
                            //        }
                            //        else
                            //            tempviewrequest.IsNeedPrescription = true;
                            //    }
                            //    else
                            //        tempviewrequest.IsNeedPrescription = true;
                            //}

                            if (jsondischargeprocess.list.dischargerequests.Where(y => y.WorklistId == x.WorklistId).Count() > 0)
                            {
                                if (jsondischargeprocess.list.dischargerequests.Where(y => y.IsPrescription == true && y.WorklistId == x.WorklistId).Count() == 0)
                                {
                                    tempviewrequest.IsPrescription = false;
                                }
                                else
                                    tempviewrequest.IsPrescription = true;

                                if (jsondischargeprocess.list.dischargerequests.Where(y => y.IsRetur == true && y.WorklistId == x.WorklistId).Count() == 0)
                                {
                                    tempviewrequest.IsRetur = false;
                                }
                                else
                                    tempviewrequest.IsRetur = true;
                            }


                            if (jsondischargeprocess.list.subdischarges.Where(y => y.WorklistId == x.WorklistId && y.FinalDate != null).Count() > 0)
                            {
                                tempviewrequest.FinalDate = jsondischargeprocess.list.subdischarges.Find(y => y.WorklistId == x.WorklistId && y.FinalDate != null).FinalDate;
                            }
                            else
                            {
                                tempviewrequest.FinalDate = DateTime.Parse("01/01/0001 00:00:00").ToString("dd/MM/yyyy HH:mm:ss");
                            }

                            if (tempviewrequest.SubDateService != "01/01/0001 00:00:00")
                            {
                                TimeSpan servicelate = DateTime.Parse(tempviewrequest.SubDateService).Subtract(tempviewrequest.SubmitDate);
                                double minutelate = servicelate.TotalMinutes;
                                if (minutelate > lateduration)
                                    tempviewrequest.lateservice = "true";
                                else
                                    tempviewrequest.lateservice = "false";
                            }
                            else
                                tempviewrequest.lateservice = "false";

                            if (tempviewrequest.SubDateItem != "01/01/0001 00:00:00")
                            {
                                TimeSpan itemlate = DateTime.Parse(tempviewrequest.SubDateItem).Subtract(tempviewrequest.SubmitDate);
                                double minutelate = itemlate.TotalMinutes;
                                if (minutelate > lateduration)
                                    tempviewrequest.lateitem = "true";
                                else
                                    tempviewrequest.lateitem = "false";
                            }
                            else
                                tempviewrequest.lateitem = "false";

                            if (tempviewrequest.EmailDate != "01/01/0001 00:00:00")
                            {
                                TimeSpan emaillate = DateTime.Parse(tempviewrequest.EmailDate).Subtract(DateTime.Parse(tempviewrequest.FinalDate));
                                double minutelate = emaillate.TotalMinutes;
                                if (minutelate > latedurationinsurance)
                                    tempviewrequest.lateemail = "true";
                                else
                                    tempviewrequest.lateemail = "false";
                            }
                            else
                                tempviewrequest.lateemail = "false";

                            if (tempviewrequest.ConfirmDate != "01/01/0001 00:00:00")
                            {
                                TimeSpan confirmlate = DateTime.Parse(tempviewrequest.ConfirmDate).Subtract(DateTime.Parse(tempviewrequest.EmailDate));
                                double minutelate = confirmlate.TotalMinutes;
                                if (minutelate > latedurationinsurance)
                                    tempviewrequest.lateconfirm = "true";
                                else
                                    tempviewrequest.lateconfirm = "false";
                            }
                            else
                                tempviewrequest.lateconfirm = "false";


                            if (tempviewrequest.InvoiceDate != "01/01/0001 00:00:00")
                            {
                                TimeSpan Invoicelate = DateTime.Parse(tempviewrequest.InvoiceDate).Subtract(DateTime.Parse(tempviewrequest.FinalDate));
                                double minutelate = Invoicelate.TotalMinutes;
                                if (minutelate > latedurationinvoice)
                                    tempviewrequest.lateinvoice = "true";
                                else
                                    tempviewrequest.lateinvoice = "false";
                            }
                            else
                                tempviewrequest.lateinvoice = "false";

                            if (tempviewrequest.PayerName.ToLower() == "private")
                            {
                                if (tempviewrequest.InvoiceDate != "01/01/0001 00:00:00")
                                {
                                    TimeSpan totalprivate = DateTime.Parse(x.InvoiceDate).Subtract(x.SubmitDate);
                                    double minutelate = totalprivate.TotalMinutes;
                                    if (minutelate > latedurationtotalprivate)
                                        tempviewrequest.latetotal = "true";
                                    else
                                        tempviewrequest.latetotal = "false";
                                }
                                else
                                    tempviewrequest.latetotal = "false";
                            }
                            else if (tempviewrequest.PayerName.ToLower() != "private")
                            {
                                if (tempviewrequest.InvoiceDate != "01/01/0001 00:00:00")
                                {
                                    TimeSpan totalprivate = DateTime.Parse(x.InvoiceDate).Subtract(x.SubmitDate);
                                    double minutelate = totalprivate.TotalMinutes;
                                    if (minutelate > latedurationtotalpayer)
                                        tempviewrequest.latetotal = "true";
                                    else
                                        tempviewrequest.latetotal = "false";
                                }
                                else
                                    tempviewrequest.latetotal = "false";
                            }
                            viewrequest.Add(tempviewrequest);
                        }

                    }

                    List<long> WardCollection = (from x in tempview
                                                 select x.WardId).Distinct().ToList();

                    Session[Helper.CollectionDischargeProcess + hfguidadditional.Value] = viewrequest;
                    List<WardCollection> tempwardcoll = new List<WardCollection>();

                    if (Session[Helper.SessionWard + hfguidadditional.Value] == null)
                    {
                        var GetWard = clsWard.GetWard(long.Parse(hfOrganizationId.Value));
                        var ListWard = JsonConvert.DeserializeObject<ListWard>(GetWard.Result.ToString());

                        Session[Helper.SessionWard + hfguidadditional.Value] = ListWard.list;
                    }


                    List<Ward> listWard = (List<Ward>)Session[Helper.SessionWard + hfguidadditional.Value];

                    foreach (var x in WardCollection)
                    {
                        WardCollection temp = new WardCollection();
                        temp.WardId = x;
                        temp.WardName = listWard.Find(y => y.WardId == x).Name;
                        tempwardcoll.Add(temp);
                    }

                    tempwardcoll = (from a in tempwardcoll orderby a.WardName select a).ToList();

                    Session[Helper.wardcollection + hfguidadditional.Value] = tempwardcoll;
                    Session[Helper.CommonColorGrid] = null;
                    rptDischargeProcess.DataSource = tempwardcoll;
                    rptDischargeProcess.DataBind();

                }
                else
                {
                    rptDischargeProcess.DataSource = null;
                    rptDischargeProcess.DataBind();
                }
            }
            else
            {
                ShowToastr("Data tidak ditemukan.", "info", "info");
            }
            log.Info(LogLibrary.Logging("E", "GetWorklistProcess", hfName.Value, "Finish GetWorklistProcess"));
        }
        catch (Exception ex)
        {
            ShowToastr("Terjadi kesalahan pada sistem. Mohon hubungi staf IT di rumah sakit Anda.", "error", "error");
            log.Error(LogLibrary.Error("btnPreview_click", hfName.Value, ex.Message));
        }
    }

    void ShowToastr(string message, string title, string type)
    {
        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "toastr_message",
             String.Format("toastr.{0}('{1}', '{2}');", type.ToLower(), message, title), addScriptTags: true);
    }

    protected void SearchProcess_onChange(object sender, EventArgs e)
    {
        try
        {
            log.Info(LogLibrary.Logging("S", "GetWorklistProcess", hfName.Value, ""));

            if (txtdateprocess.Text == "")
            {
                btnClearDateProcess.Visible = false;
                btnShowCalendarProcess.Visible = true;
            }
            else
            {
                btnClearDateProcess.Visible = true;
                btnShowCalendarProcess.Visible = false;
            }


            if (txtsearchprocess.Text == "")
            {
                btnClearTextFindPatientProcess.Visible = false;
            }
            else
            {
                btnClearTextFindPatientProcess.Visible = true;
            }

            int lateduration;
            int latedurationinsurance;
            int latedurationinvoice;
            int latedurationtotalprivate;
            int latedurationtotalpayer;

            log.Debug(LogLibrary.Logging("S", "GetOrganizationSetting", hfName.Value, ""));
            var orgsetting = clsOrganizationSetting.GetOrganizationSetting(long.Parse(hfOrganizationId.Value));
            var jsonorgsetting = JsonConvert.DeserializeObject<ResultViewOrganizationSetting>(orgsetting.Result.ToString());
            log.Debug(LogLibrary.Logging("E", "GetOrganizationSetting", hfName.Value, jsonorgsetting.ToString()));

            if (jsonorgsetting.list.Count() > 0)
            {
                lateduration = int.Parse(jsonorgsetting.list.Find(y => y.setting_name == "LATE_SUBDISCHARGE").setting_value);
                latedurationinsurance = int.Parse(jsonorgsetting.list.Find(y => y.setting_name == "LATE_INSURANCE").setting_value);
                latedurationinvoice = int.Parse(jsonorgsetting.list.Find(y => y.setting_name == "LATE_INVOICE").setting_value);
                latedurationtotalprivate = int.Parse(jsonorgsetting.list.Find(y => y.setting_name == "LATE_TOTALPRIVATE").setting_value);
                latedurationtotalpayer = int.Parse(jsonorgsetting.list.Find(y => y.setting_name == "LATE_TOTALPAYER").setting_value);
            }
            else
            {
                lateduration = 20; //warna merah untuk service dan item
                latedurationinsurance = 20; //warna merah untuk email dan confirm
                latedurationinvoice = 20; //warna merah invoice selesai
                latedurationtotalprivate = 30; //warna merah untuk lama kerja pasien private
                latedurationtotalpayer = 120; //warna merah untuk lama kerja pasien with payer
            }

            lblDateRefreshProcess.Text = DateTime.Now.ToString("dd MMM yyyy, HH:mm");
            List<ViewDischargeRequest> viewrequest = new List<ViewDischargeRequest>();
            long wardId = 0;
            if (ddlWardListProcess.SelectedValue != "")
                wardId = long.Parse(ddlWardListProcess.SelectedValue);

            var dischargeprocess = clsWorklist.getListDischargeProcess(long.Parse(hfOrganizationId.Value), DateTime.Now.ToString("yyyy-MM-dd"), wardId, txtsearchprocess.Text, 2);
            var jsondischargeprocess = JsonConvert.DeserializeObject<ResultListDischargeProcess>(dischargeprocess.Result.ToString());
            if (jsondischargeprocess.list.dischargerequests != null)
            {
                if (jsondischargeprocess.list.dischargerequests.Where(y => y.ArInvoiceId == 0).Count() > 0)
                {
                    imgnodataprocess.Visible = false;
                    List<DischargeRequest> tempview = new List<DischargeRequest>();
                    if (txtdateprocess.Text != "")
                        tempview = jsondischargeprocess.list.dischargerequests.Where(y => y.ArInvoiceId == 0 && y.WorklistDate.ToString("dd MMM yyyy") == DateTime.Parse(txtdateprocess.Text).ToString("dd MMM yyyy")).ToList();
                    else
                        tempview = jsondischargeprocess.list.dischargerequests.Where(y => y.ArInvoiceId == 0).ToList();

                    lblCountProcess.Text = " (" + jsondischargeprocess.list.dischargerequests.Where(y => y.ArInvoiceId == 0 && y.IsPrimary == true).Count().ToString() + ") ";
                    foreach (var x in tempview)
                    {

                        if (x.IsPrimary == true)
                        {
                            ViewDischargeRequest tempviewrequest = new ViewDischargeRequest();
                            tempviewrequest.SubmitDate = x.SubmitDate;
                            tempviewrequest.isShowDate = x.isShowDate;
                            tempviewrequest.WorklistDate = x.WorklistDate;
                            tempviewrequest.WorklistId = x.WorklistId;
                            tempviewrequest.ProcessId = x.ProcessId;
                            tempviewrequest.AdmissionId = x.AdmissionId;
                            tempviewrequest.AdmissionNo = x.AdmissionNo;
                            tempviewrequest.PatientId = x.PatientId;
                            tempviewrequest.PatientName = x.PatientName;
                            tempviewrequest.WardId = x.WardId;
                            tempviewrequest.islate = x.islate;
                            if (x.PayerName.Length > 50)
                                tempviewrequest.PayerName = x.PayerName.Substring(0, 50) + " ...";
                            else
                                tempviewrequest.PayerName = x.PayerName;
                            tempviewrequest.DoctorId = x.DoctorId;
                            tempviewrequest.DoctorName = x.DoctorName;
                            tempviewrequest.AdditionalNotes = x.AdditionalNotes;
                            tempviewrequest.ArInvoiceId = x.ArInvoiceId;
                            tempviewrequest.IsPrimary = x.IsPrimary;
                            tempviewrequest.IsPrescription = x.IsPrescription;
                            tempviewrequest.IsRetur = x.IsRetur;
                            tempviewrequest.EmailDate = x.EmailDate;
                            tempviewrequest.ConfirmDate = x.ConfirmDate;
                            tempviewrequest.InvoiceDate = x.InvoiceDate;
                            tempviewrequest.FUPatient = x.FUPatient;
                            tempviewrequest.OPDControl = x.OPDControl;
                            tempviewrequest.localMrNo = x.LocalMrNo;
                            tempviewrequest.birthDate = x.BirthDate.ToString("dd MMM yyyy");
                            tempviewrequest.roomNo = x.RoomNo;
                            tempviewrequest.FlagDischarged = x.FlagDischarged;
                            tempviewrequest.ModifiedDate = x.ModifiedDate;

                            if (jsondischargeprocess.list.subdischarges.Where(y => y.WorklistId == x.WorklistId && y.SubDischargeTypeId == 2).Count() > 0)
                            {
                                tempviewrequest.SubDateBed = jsondischargeprocess.list.subdischarges.Find(y => y.WorklistId == x.WorklistId && y.SubDischargeTypeId == 2).SubDate;
                            }
                            else
                            {
                                tempviewrequest.SubDateBed = DateTime.Parse("01/01/0001 00:00:00").ToString("dd/MM/yyyy HH:mm:ss");
                            }

                            if (jsondischargeprocess.list.subdischarges.Where(y => y.WorklistId == x.WorklistId && y.SubDischargeTypeId == 4).Count() > 0)
                            {
                                tempviewrequest.SubDateService = jsondischargeprocess.list.subdischarges.Find(y => y.WorklistId == x.WorklistId && y.SubDischargeTypeId == 4).SubDate;
                            }
                            else
                            {
                                tempviewrequest.SubDateService = DateTime.Parse("01/01/0001 00:00:00").ToString("dd/MM/yyyy HH:mm:ss");
                            }


                            if (jsondischargeprocess.list.subdischarges.Where(y => y.WorklistId == x.WorklistId && y.SubDischargeTypeId == 8).Count() > 0)
                            {
                                tempviewrequest.SubDateItem = jsondischargeprocess.list.subdischarges.Find(y => y.WorklistId == x.WorklistId && y.SubDischargeTypeId == 8).SubDate;
                                tempviewrequest.IsNeedPrescription = true;
                            }
                            else
                            {
                                tempviewrequest.SubDateItem = DateTime.Parse("01/01/0001 00:00:00").ToString("dd/MM/yyyy HH:mm:ss");
                                tempviewrequest.IsNeedPrescription = true;
                            }

                            //if (jsondischargeprocess.list.dischargerequests.Where(y => y.WorklistId == x.WorklistId).Count() > 0)
                            //{
                            //    if (jsondischargeprocess.list.subdischarges.Where(y => y.WorklistId == x.WorklistId && y.SubDischargeTypeId == 8).Count() > 0)
                            //    {
                            //        tempviewrequest.SubDateItem = jsondischargeprocess.list.subdischarges.Find(y => y.WorklistId == x.WorklistId && y.SubDischargeTypeId == 8).SubDate;
                            //    }
                            //    else
                            //    {
                            //        tempviewrequest.SubDateItem = DateTime.Parse("01/01/0001 00:00:00").ToString("dd/MM/yyyy HH:mm:ss");
                            //    }

                            //    if (jsondischargeprocess.list.dischargerequests.Where(y => y.IsPrescription == true && y.WorklistId == x.WorklistId).Count() == 0)
                            //    {
                            //        if (jsondischargeprocess.list.dischargerequests.Where(y => y.IsRetur == true && y.WorklistId == x.WorklistId).Count() == 0)
                            //        {
                            //            tempviewrequest.SubDateItem = DateTime.Parse("01/01/0001 00:00:00").ToString("dd/MM/yyyy HH:mm:ss");
                            //            tempviewrequest.IsNeedPrescription = false;
                            //        }
                            //        else
                            //            tempviewrequest.IsNeedPrescription = true;
                            //    }
                            //    else
                            //        tempviewrequest.IsNeedPrescription = true;
                            //}

                            if (jsondischargeprocess.list.dischargerequests.Where(y => y.WorklistId == x.WorklistId).Count() > 0)
                            {
                                if (jsondischargeprocess.list.dischargerequests.Where(y => y.IsPrescription == true && y.WorklistId == x.WorklistId).Count() == 0)
                                {
                                    tempviewrequest.IsPrescription = false;
                                }
                                else
                                    tempviewrequest.IsPrescription = true;

                                if (jsondischargeprocess.list.dischargerequests.Where(y => y.IsRetur == true && y.WorklistId == x.WorklistId).Count() == 0)
                                {
                                    tempviewrequest.IsRetur = false;
                                }
                                else
                                    tempviewrequest.IsRetur = true;
                            }


                            if (jsondischargeprocess.list.subdischarges.Where(y => y.WorklistId == x.WorklistId && y.FinalDate != null).Count() > 0)
                            {
                                tempviewrequest.FinalDate = jsondischargeprocess.list.subdischarges.Find(y => y.WorklistId == x.WorklistId && y.FinalDate != null).FinalDate;
                            }
                            else
                            {
                                tempviewrequest.FinalDate = DateTime.Parse("01/01/0001 00:00:00").ToString("dd/MM/yyyy HH:mm:ss");
                            }

                            if (tempviewrequest.SubDateService != "01/01/0001 00:00:00")
                            {
                                TimeSpan servicelate = DateTime.Parse(tempviewrequest.SubDateService).Subtract(tempviewrequest.SubmitDate);
                                double minutelate = servicelate.TotalMinutes;
                                if (minutelate > lateduration)
                                    tempviewrequest.lateservice = "true";
                                else
                                    tempviewrequest.lateservice = "false";
                            }
                            else
                                tempviewrequest.lateservice = "false";

                            if (tempviewrequest.SubDateItem != "01/01/0001 00:00:00")
                            {
                                TimeSpan itemlate = DateTime.Parse(tempviewrequest.SubDateItem).Subtract(tempviewrequest.SubmitDate);
                                double minutelate = itemlate.TotalMinutes;
                                if (minutelate > lateduration)
                                    tempviewrequest.lateitem = "true";
                                else
                                    tempviewrequest.lateitem = "false";
                            }
                            else
                                tempviewrequest.lateitem = "false";

                            if (tempviewrequest.EmailDate != "01/01/0001 00:00:00")
                            {
                                TimeSpan emaillate = DateTime.Parse(tempviewrequest.EmailDate).Subtract(DateTime.Parse(tempviewrequest.FinalDate));
                                double minutelate = emaillate.TotalMinutes;
                                if (minutelate > latedurationinsurance)
                                    tempviewrequest.lateemail = "true";
                                else
                                    tempviewrequest.lateemail = "false";
                            }
                            else
                                tempviewrequest.lateemail = "false";

                            if (tempviewrequest.ConfirmDate != "01/01/0001 00:00:00")
                            {
                                TimeSpan confirmlate = DateTime.Parse(tempviewrequest.ConfirmDate).Subtract(DateTime.Parse(tempviewrequest.EmailDate));
                                double minutelate = confirmlate.TotalMinutes;
                                if (minutelate > latedurationinsurance)
                                    tempviewrequest.lateconfirm = "true";
                                else
                                    tempviewrequest.lateconfirm = "false";
                            }
                            else
                                tempviewrequest.lateconfirm = "false";


                            if (tempviewrequest.InvoiceDate != "01/01/0001 00:00:00")
                            {
                                TimeSpan Invoicelate = DateTime.Parse(tempviewrequest.InvoiceDate).Subtract(DateTime.Parse(tempviewrequest.FinalDate));
                                double minutelate = Invoicelate.TotalMinutes;
                                if (minutelate > latedurationinvoice)
                                    tempviewrequest.lateinvoice = "true";
                                else
                                    tempviewrequest.lateinvoice = "false";
                            }
                            else
                                tempviewrequest.lateinvoice = "false";

                            if (tempviewrequest.PayerName.ToLower() == "private")
                            {
                                if (tempviewrequest.InvoiceDate != "01/01/0001 00:00:00")
                                {
                                    TimeSpan totalprivate = DateTime.Parse(x.InvoiceDate).Subtract(x.SubmitDate);
                                    double minutelate = totalprivate.TotalMinutes;
                                    if (minutelate > latedurationtotalprivate)
                                        tempviewrequest.latetotal = "true";
                                    else
                                        tempviewrequest.latetotal = "false";
                                }
                                else
                                    tempviewrequest.latetotal = "false";
                            }
                            else if (tempviewrequest.PayerName.ToLower() != "private")
                            {
                                if (tempviewrequest.InvoiceDate != "01/01/0001 00:00:00")
                                {
                                    TimeSpan totalprivate = DateTime.Parse(x.InvoiceDate).Subtract(x.SubmitDate);
                                    double minutelate = totalprivate.TotalMinutes;
                                    if (minutelate > latedurationtotalpayer)
                                        tempviewrequest.latetotal = "true";
                                    else
                                        tempviewrequest.latetotal = "false";
                                }
                                else
                                    tempviewrequest.latetotal = "false";
                            }
                            viewrequest.Add(tempviewrequest);
                        }

                    }

                    List<long> WardCollection = (from x in tempview
                                                 select x.WardId).Distinct().ToList();

                    Session[Helper.CollectionDischargeProcess + hfguidadditional.Value] = viewrequest;
                    List<WardCollection> tempwardcoll = new List<WardCollection>();

                    if (Session[Helper.SessionWard + hfguidadditional.Value] == null)
                    {
                        var GetWard = clsWard.GetWard(long.Parse(hfOrganizationId.Value));
                        var ListWard = JsonConvert.DeserializeObject<ListWard>(GetWard.Result.ToString());

                        Session[Helper.SessionWard + hfguidadditional.Value] = ListWard.list;
                    }


                    List<Ward> listWard = (List<Ward>)Session[Helper.SessionWard + hfguidadditional.Value];

                    foreach (var x in WardCollection)
                    {
                        WardCollection temp = new WardCollection();
                        temp.WardId = x;
                        temp.WardName = listWard.Find(y => y.WardId == x).Name;
                        tempwardcoll.Add(temp);
                    }
                    Session[Helper.wardcollection + hfguidadditional.Value] = tempwardcoll;
                    Session[Helper.CommonColorGrid] = null;
                    rptDischargeProcess.DataSource = tempwardcoll;
                    rptDischargeProcess.DataBind();
                }
                else
                {
                    lblCountProcess.Text = "(0)";
                    imgnodataprocess.Visible = true;
                    rptDischargeProcess.DataSource = null;
                    rptDischargeProcess.DataBind();
                }
            }
            else
            {
                ShowToastr("Data tidak ditemukan.", "info", "info");
            }
            log.Info(LogLibrary.Logging("E", "GetWorklistProcess", hfName.Value, "Finish GetWorklistProcess"));
        }
        catch (Exception ex)
        {
            ShowToastr("Terjadi kesalahan pada sistem. Mohon hubungi staf IT di rumah sakit Anda.", "error", "error");
            log.Error(LogLibrary.Error("btnPreview_click", hfName.Value, ex.Message));
        }
    }


    //================================== DISCHARGE PLAN =================================

    protected void SearchPlan_onChange(object sender, EventArgs e)
    {
        try
        {
            log.Info(LogLibrary.Logging("S", "getListPatient", hfName.Value, ""));

            if (txtFindPatient.Text != "")
            {
                btnClearTextFindPatientPlan.Visible = true;
            }
            else
            {
                btnClearTextFindPatientPlan.Visible = false;
            }


            if (textboxdateto.Text == "")
            {
                btnClearDatePlan.Visible = false;
                btnShowCalendarPlan.Visible = true;
            }
            else
            {
                btnClearDatePlan.Visible = true;
                btnShowCalendarPlan.Visible = false;
                //btnClearDatePlan.Attributes.Add("style", "display:''");
            }

            List<ViewWorklistDischarge> listWorklistDischarge = new List<ViewWorklistDischarge>();
            lblDateRefreshWorklist.Text = DateTime.Now.ToString("dd MMM yyyy, HH:mm");
            if (long.Parse(hfOrganizationId.Value) != 0)
            {

                long wardId = 0;
                if (ddlWardlist.SelectedValue != "")
                    wardId = long.Parse(ddlWardlist.SelectedValue);

                log.Debug(LogLibrary.Logging("S", "getListPatientDischarge", hfName.Value, ""));
                var worklist = clsWorklist.getListPatientDischarge(long.Parse(hfOrganizationId.Value), wardId, txtFindPatient.Text);
                var getTotalWorklist = JsonConvert.DeserializeObject<ListViewWorklistDischarge>(worklist.Result.ToString());
                log.Debug(LogLibrary.Logging("E", "getListPatientDischarge", hfName.Value, getTotalWorklist.ToString()));

                if (getTotalWorklist != null)
                {
                    if (textboxdateto.Text != "")
                        listWorklistDischarge = getTotalWorklist.list.Where(y => y.IsLate != "1" && DateTime.Parse(y.WorklistDate.ToString("yyyy-MM-dd")) == DateTime.Parse(DateTime.Parse(textboxdateto.Text).ToString("yyyy-MM-dd"))).ToList();
                    else
                        listWorklistDischarge = getTotalWorklist.list.Where(y => y.IsLate != "1").ToList();

                    lblCountPlan.Text = " (" + listWorklistDischarge.Where(y => y.IsPrimary == true && y.IsLate != "1").Count().ToString() + ") ";

                    List<long> WardCollection = (from x in listWorklistDischarge
                                                 select x.WardId).Distinct().ToList();

                    Session[Helper.CollectionDischargePlan + hfguidadditional.Value] = listWorklistDischarge;
                    List<WardCollection> tempwardcoll = new List<WardCollection>();

                    if (Session[Helper.SessionWard + hfguidadditional.Value] == null)
                    {
                        var GetWard = clsWard.GetWard(long.Parse(hfOrganizationId.Value));
                        var ListWard = JsonConvert.DeserializeObject<ListWard>(GetWard.Result.ToString());

                        Session[Helper.SessionWard + hfguidadditional.Value] = ListWard.list;
                    }


                    List<Ward> listWard = (List<Ward>)Session[Helper.SessionWard + hfguidadditional.Value];

                    foreach (var x in WardCollection)
                    {
                        WardCollection temp = new WardCollection();
                        temp.WardId = x;
                        temp.WardName = listWard.Find(y => y.WardId == x).Name;
                        tempwardcoll.Add(temp);
                    }
                    Session[Helper.wardcollection + hfguidadditional.Value] = tempwardcoll;

                    rptDischargePlan.DataSource = tempwardcoll;
                    rptDischargePlan.DataBind();

                }
                else
                {
                    ShowToastr("Data tidak ditemukan.", "error", "error");
                }
            }
            else
            {
                {
                    //Response.Redirect(String.Format("~/Form/General/Login?action=clear"));
                    Context.ApplicationInstance.CompleteRequest();
                    return;
                }
            }
            log.Info(LogLibrary.Logging("E", "getListPatient", hfName.Value, "Finish getListPatient"));
        }
        catch (Exception ex)
        {
            ShowToastr("Terjadi kesalahan pada sistem. Mohon hubungi staf IT di rumah sakit Anda.", "error", "error");
            log.Error(LogLibrary.Error("getListPatient", hfName.Value, ex.Message));
        }
    }

    protected void SearchDone_onChange(object sender, EventArgs e)
    {
        GetWorklistDone();
        getCountPatient();
    }

    protected void rptDischargeProcess_onItemBound(object sender, RepeaterItemEventArgs e)
    {
        if (Session[Helper.CollectionDischargeProcess + hfguidadditional.Value] != null)
        {
            List<ViewDischargeRequest> listWorklistDischarge = (List<ViewDischargeRequest>)Session[Helper.CollectionDischargeProcess + hfguidadditional.Value];
            var data = ((WardCollection)e.Item.DataItem).WardId;
            DataTable dt = Helper.ToDataTable(listWorklistDischarge.Where(y => y.WardId == data).ToList());


            var gvLIstDischargeProcess = (GridView)e.Item.FindControl("gvLIstDischargeProcess");
            Label headername = (Label)e.Item.FindControl("LblHeader");
            Label lblcountward = (Label)e.Item.FindControl("lblcountward");
            lblcountward.Text = " ( " + listWorklistDischarge.Where(y => y.WardId == data && y.IsPrimary == true).Count().ToString() + " )";
            if (Session[Helper.SessionWard + hfguidadditional.Value] == null)
            {
                var GetWard = clsWard.GetWard(long.Parse(hfOrganizationId.Value));
                var ListWard = JsonConvert.DeserializeObject<ListWard>(GetWard.Result.ToString());

                List<Ward> listWard = ListWard.list;
                Session[Helper.SessionWard + hfguidadditional.Value] = listWard;
            }

            List<Ward> templistWard = (List<Ward>)Session[Helper.SessionWard + hfguidadditional.Value];
            string WardName = templistWard.Find(y => y.WardId == data).Name;

            TextInfo textInfo = new CultureInfo("en-US", false).TextInfo;
            WardName = textInfo.ToTitleCase(WardName);

            Session[Helper.CommonColorGrid] = null;
            headername.Text = WardName;
            gvLIstDischargeProcess.DataSource = dt;
            gvLIstDischargeProcess.DataBind();
        }
    }
    //================================== DISCHARGE DONE =================================

    protected void GetWorklistDone()
    {
        try
        {
            log.Info(LogLibrary.Logging("S", "GetWorklistDone", hfName.Value, ""));

            if (txtsearchdone.Text != "")
            {
                btnClearTextFindPatientDone.Visible = true;
                btnShowsearchdone.Visible = false;
            }
            else
            {
                btnClearTextFindPatientDone.Visible = false;
                btnShowsearchdone.Visible = true;
            }

            //var date = "";
            //if(txtdatedoneto.Text == "")
            //{
            //    date = DateTime.Now.ToString("yyyy-MM-dd");
            //}
            //else
            //{
            //    date = DateTime.Parse(txtdatedoneto.Text).ToString("yyyy-MM-dd");
            //}

            int lateduration;
            int latedurationinsurance;
            int latedurationinvoice;
            int latedurationtotalprivate;
            int latedurationtotalpayer;

            log.Debug(LogLibrary.Logging("S", "GetOrganizationSetting", hfName.Value, ""));
            var orgsetting = clsOrganizationSetting.GetOrganizationSetting(long.Parse(hfOrganizationId.Value));
            var jsonorgsetting = JsonConvert.DeserializeObject<ResultViewOrganizationSetting>(orgsetting.Result.ToString());
            log.Debug(LogLibrary.Logging("E", "GetOrganizationSetting", hfName.Value, jsonorgsetting.ToString()));

            if (jsonorgsetting.list.Count() > 0)
            {
                lateduration = int.Parse(jsonorgsetting.list.Find(y => y.setting_name == "LATE_SUBDISCHARGE").setting_value);
                latedurationinsurance = int.Parse(jsonorgsetting.list.Find(y => y.setting_name == "LATE_INSURANCE").setting_value);
                latedurationinvoice = int.Parse(jsonorgsetting.list.Find(y => y.setting_name == "LATE_INVOICE").setting_value);
                latedurationtotalprivate = int.Parse(jsonorgsetting.list.Find(y => y.setting_name == "LATE_TOTALPRIVATE").setting_value);
                latedurationtotalpayer = int.Parse(jsonorgsetting.list.Find(y => y.setting_name == "LATE_TOTALPAYER").setting_value);
            }
            else
            {
                lateduration = 20; //warna merah untuk service dan item
                latedurationinsurance = 20; //warna merah untuk email dan confirm
                latedurationinvoice = 20; //warna merah invoice selesai
                latedurationtotalprivate = 30; //warna merah untuk lama kerja pasien private
                latedurationtotalpayer = 120; //warna merah untuk lama kerja pasien with payer
            }

            lblDateRefreshDone.Text = DateTime.Now.ToString("dd MMM yyyy, HH:mm");
            List<ViewDischargeRequest> viewrequest = new List<ViewDischargeRequest>();
            long wardId = 0;
            if (ddlWardListDone.SelectedValue != "")
                wardId = long.Parse(ddlWardListDone.SelectedValue);

            string date = DateTime.Now.ToString("yyy-MM-dd");
            if (txtdatedoneto.Text != "")
                date = DateTime.Parse(txtdatedoneto.Text).ToString("yyy-MM-dd");

            log.Debug(LogLibrary.Logging("S", "getListDischargeProcess", hfName.Value, ""));
            var dischargeprocess = clsWorklist.getListDischargeProcess(long.Parse(hfOrganizationId.Value), date, wardId, txtsearchdone.Text, 1);//DateTime.Now.ToString("yyyy-MM-dd")
            var jsondischargeprocess = JsonConvert.DeserializeObject<ResultListDischargeProcess>(dischargeprocess.Result.ToString());
            log.Debug(LogLibrary.Logging("E", "getListDischargeProcess", hfName.Value, jsondischargeprocess.ToString()));

            if (jsondischargeprocess.list.dischargerequests != null)
            {
                if (jsondischargeprocess.list.dischargerequests.Where(y => y.ArInvoiceId != 0).Count() > 0)
                {
                    List<DischargeRequest> tempview = new List<DischargeRequest>();
                    nodatadone.Visible = false;
                    tempview = jsondischargeprocess.list.dischargerequests.Where(y => y.ArInvoiceId != 0).ToList();
                    lblCountDone.Text = " (" + jsondischargeprocess.list.dischargerequests.Where(y => y.ArInvoiceId != 0 && y.IsPrimary == true).Count().ToString() + ") ";
                    foreach (var x in tempview)
                    {

                        if (x.IsPrimary == true)
                        {
                            ViewDischargeRequest tempviewrequest = new ViewDischargeRequest();
                            tempviewrequest.SubmitDate = x.SubmitDate;
                            tempviewrequest.isShowDate = x.isShowDate;
                            tempviewrequest.WorklistId = x.WorklistId;
                            tempviewrequest.ProcessId = x.ProcessId;
                            tempviewrequest.AdmissionId = x.AdmissionId;
                            tempviewrequest.AdmissionNo = x.AdmissionNo;
                            tempviewrequest.PatientId = x.PatientId;
                            tempviewrequest.PatientName = x.PatientName;
                            tempviewrequest.WardId = x.WardId;
                            tempviewrequest.islate = x.islate;
                            if (x.PayerName.Length > 50)
                                tempviewrequest.PayerName = x.PayerName.Substring(0, 50) + " ...";
                            else
                                tempviewrequest.PayerName = x.PayerName;

                            //tempviewrequest.PayerName = x.PayerName;
                            tempviewrequest.DoctorId = x.DoctorId;
                            tempviewrequest.DoctorName = x.DoctorName;
                            tempviewrequest.AdditionalNotes = x.AdditionalNotes;
                            tempviewrequest.ArInvoiceId = x.ArInvoiceId;
                            tempviewrequest.IsPrimary = x.IsPrimary;
                            tempviewrequest.IsPrescription = x.IsPrescription;
                            tempviewrequest.IsRetur = x.IsRetur;
                            tempviewrequest.EmailDate = x.EmailDate;
                            tempviewrequest.ConfirmDate = x.ConfirmDate;
                            tempviewrequest.InvoiceDate = x.InvoiceDate;
                            tempviewrequest.FUPatient = x.FUPatient;
                            tempviewrequest.OPDControl = x.OPDControl;
                            tempviewrequest.localMrNo = x.LocalMrNo;
                            tempviewrequest.birthDate = x.BirthDate.ToString("dd MMM yyyy");
                            tempviewrequest.roomNo = x.RoomNo;
                            tempviewrequest.FlagDischarged = x.FlagDischarged;

                            TimeSpan test = DateTime.Parse(x.InvoiceDate).Subtract(x.SubmitDate);
                            int hour = 0;
                            if (test.Days > 0)
                            {
                                hour = test.Hours + (24 * test.Days);
                            }
                            else
                                hour = test.Hours;

                            string minute = "";
                            if (test.Minutes < 10)
                                minute = "0" + test.Minutes.ToString();
                            else
                                minute = test.Minutes.ToString();
                            string second = "";
                            if (test.Seconds < 10)
                                second = "0" + test.Seconds.ToString();
                            else
                                second = test.Seconds.ToString();

                            tempviewrequest.Duration = hour.ToString() + ":" + minute + ":" + second;

                            tempviewrequest.ModifiedDate = x.ModifiedDate;

                            if (jsondischargeprocess.list.subdischarges.Where(y => y.WorklistId == x.WorklistId && y.SubDischargeTypeId == 2).Count() > 0)
                            {
                                tempviewrequest.SubDateBed = jsondischargeprocess.list.subdischarges.Find(y => y.WorklistId == x.WorklistId && y.SubDischargeTypeId == 2).SubDate;
                            }
                            else
                            {
                                tempviewrequest.SubDateBed = DateTime.Parse("01/01/0001 00:00:00").ToString("dd/MM/yyyy HH:mm:ss");
                            }

                            if (jsondischargeprocess.list.subdischarges.Where(y => y.WorklistId == x.WorklistId && y.SubDischargeTypeId == 4).Count() > 0)
                            {
                                tempviewrequest.SubDateService = jsondischargeprocess.list.subdischarges.Find(y => y.WorklistId == x.WorklistId && y.SubDischargeTypeId == 4).SubDate;
                            }
                            else
                            {
                                tempviewrequest.SubDateService = DateTime.Parse("01/01/0001 00:00:00").ToString("dd/MM/yyyy HH:mm:ss");
                            }

                            if (jsondischargeprocess.list.subdischarges.Where(y => y.WorklistId == x.WorklistId && y.SubDischargeTypeId == 8).Count() > 0)
                            {
                                tempviewrequest.SubDateItem = jsondischargeprocess.list.subdischarges.Find(y => y.WorklistId == x.WorklistId && y.SubDischargeTypeId == 8).SubDate;
                                tempviewrequest.IsNeedPrescription = true;
                            }
                            else
                            {
                                tempviewrequest.SubDateItem = DateTime.Parse("01/01/0001 00:00:00").ToString("dd/MM/yyyy HH:mm:ss");
                                tempviewrequest.IsNeedPrescription = true;
                            }


                            //if (jsondischargeprocess.list.dischargerequests.Where(y => y.WorklistId == x.WorklistId).Count() > 0)
                            //{
                            //    if (jsondischargeprocess.list.subdischarges.Where(y => y.WorklistId == x.WorklistId && y.SubDischargeTypeId == 8).Count() > 0)
                            //    {
                            //        tempviewrequest.SubDateItem = jsondischargeprocess.list.subdischarges.Find(y => y.WorklistId == x.WorklistId && y.SubDischargeTypeId == 8).SubDate;
                            //    }
                            //    else
                            //    {
                            //        tempviewrequest.SubDateItem = DateTime.Parse("01/01/0001 00:00:00").ToString("dd/MM/yyyy HH:mm:ss");
                            //    }

                            //    if (jsondischargeprocess.list.dischargerequests.Where(y => y.IsPrescription == true && y.WorklistId == x.WorklistId).Count() == 0)
                            //    {
                            //        if (jsondischargeprocess.list.dischargerequests.Where(y => y.IsRetur == true && y.WorklistId == x.WorklistId).Count() == 0)
                            //        {
                            //            tempviewrequest.IsNeedPrescription = false;
                            //            tempviewrequest.SubDateItem = DateTime.Parse("01/01/0001 00:00:00").ToString("dd/MM/yyyy HH:mm:ss");
                            //        }
                            //        else
                            //            tempviewrequest.IsNeedPrescription = true;
                            //    }
                            //    else
                            //        tempviewrequest.IsNeedPrescription = true;
                            //}


                            if (jsondischargeprocess.list.subdischarges.Where(y => y.WorklistId == x.WorklistId && y.FinalDate != null).Count() > 0)
                            {
                                tempviewrequest.FinalDate = jsondischargeprocess.list.subdischarges.Find(y => y.WorklistId == x.WorklistId && y.FinalDate != null).FinalDate;
                            }
                            else
                            {
                                tempviewrequest.FinalDate = DateTime.Parse("01/01/0001 00:00:00").ToString("dd/MM/yyyy HH:mm:ss");
                            }

                            if (tempviewrequest.SubDateService != "01/01/0001 00:00:00")
                            {
                                TimeSpan servicelate = DateTime.Parse(tempviewrequest.SubDateService).Subtract(tempviewrequest.SubmitDate);
                                double minutelate = servicelate.TotalMinutes;
                                if (minutelate > lateduration)
                                    tempviewrequest.lateservice = "true";
                                else
                                    tempviewrequest.lateservice = "false";
                            }
                            else
                                tempviewrequest.lateservice = "false";

                            if (tempviewrequest.SubDateItem != "01/01/0001 00:00:00")
                            {
                                TimeSpan itemlate = DateTime.Parse(tempviewrequest.SubDateItem).Subtract(tempviewrequest.SubmitDate);
                                double minutelate = itemlate.TotalMinutes;
                                if (minutelate > lateduration)
                                    tempviewrequest.lateitem = "true";
                                else
                                    tempviewrequest.lateitem = "false";
                            }
                            else
                                tempviewrequest.lateitem = "false";

                            if (tempviewrequest.EmailDate != "01/01/0001 00:00:00")
                            {
                                TimeSpan emaillate = DateTime.Parse(tempviewrequest.EmailDate).Subtract(DateTime.Parse(tempviewrequest.FinalDate));
                                double minutelate = emaillate.TotalMinutes;
                                if (minutelate > latedurationinsurance)
                                    tempviewrequest.lateemail = "true";
                                else
                                    tempviewrequest.lateemail = "false";
                            }
                            else
                                tempviewrequest.lateemail = "false";

                            if (tempviewrequest.ConfirmDate != "01/01/0001 00:00:00")
                            {
                                TimeSpan confirmlate = DateTime.Parse(tempviewrequest.ConfirmDate).Subtract(DateTime.Parse(tempviewrequest.EmailDate));
                                double minutelate = confirmlate.TotalMinutes;
                                if (minutelate > latedurationinsurance)
                                    tempviewrequest.lateconfirm = "true";
                                else
                                    tempviewrequest.lateconfirm = "false";
                            }
                            else
                                tempviewrequest.lateconfirm = "false";


                            if (tempviewrequest.InvoiceDate != "01/01/0001 00:00:00")
                            {
                                TimeSpan Invoicelate = DateTime.Parse(tempviewrequest.InvoiceDate).Subtract(DateTime.Parse(tempviewrequest.FinalDate));
                                double minutelate = Invoicelate.TotalMinutes;
                                if (minutelate > latedurationinvoice)
                                    tempviewrequest.lateinvoice = "true";
                                else
                                    tempviewrequest.lateinvoice = "false";
                            }
                            else
                                tempviewrequest.lateinvoice = "false";

                            if (tempviewrequest.PayerName.ToLower() == "private")
                            {
                                if (tempviewrequest.InvoiceDate != "01/01/0001 00:00:00")
                                {
                                    TimeSpan totalprivate = DateTime.Parse(x.InvoiceDate).Subtract(x.SubmitDate);
                                    double minutelate = totalprivate.TotalMinutes;
                                    if (minutelate > latedurationtotalprivate)
                                        tempviewrequest.latetotal = "true";
                                    else
                                        tempviewrequest.latetotal = "false";
                                }
                                else
                                    tempviewrequest.latetotal = "false";
                            }
                            else if (tempviewrequest.PayerName.ToLower() != "private")
                            {
                                if (tempviewrequest.InvoiceDate != "01/01/0001 00:00:00")
                                {
                                    TimeSpan totalprivate = DateTime.Parse(x.InvoiceDate).Subtract(x.SubmitDate);
                                    double minutelate = totalprivate.TotalMinutes;
                                    if (minutelate > latedurationtotalpayer)
                                        tempviewrequest.latetotal = "true";
                                    else
                                        tempviewrequest.latetotal = "false";
                                }
                                else
                                    tempviewrequest.latetotal = "false";
                            }

                            viewrequest.Add(tempviewrequest);
                        }

                    }

                    List<long> WardCollection = (from x in tempview
                                                 select x.WardId).Distinct().ToList();

                    Session[Helper.CollectionDischargeDone + hfguidadditional.Value] = viewrequest;
                    List<WardCollection> tempwardcoll = new List<WardCollection>();

                    if (Session[Helper.SessionWard + hfguidadditional.Value] == null)
                    {
                        var GetWard = clsWard.GetWard(long.Parse(hfOrganizationId.Value));
                        var ListWard = JsonConvert.DeserializeObject<ListWard>(GetWard.Result.ToString());

                        Session[Helper.SessionWard + hfguidadditional.Value] = ListWard.list;
                    }


                    List<Ward> listWard = (List<Ward>)Session[Helper.SessionWard + hfguidadditional.Value];

                    foreach (var x in WardCollection)
                    {
                        WardCollection temp = new WardCollection();
                        temp.WardId = x;
                        temp.WardName = listWard.Find(y => y.WardId == x).Name;
                        tempwardcoll.Add(temp);
                    }

                    tempwardcoll = (from a in tempwardcoll orderby a.WardName select a).ToList();
                    Session[Helper.wardcollection + hfguidadditional.Value] = tempwardcoll;

                    rptDischargeDone.DataSource = tempwardcoll;
                    rptDischargeDone.DataBind();
                }
                else
                {
                    Session[Helper.wardcollection + hfguidadditional.Value] = null;
                    lblCountDone.Text = "(0)";
                    nodatadone.Visible = true;
                    rptDischargeDone.DataSource = null;
                    rptDischargeDone.DataBind();
                }

                bindddldone();
            }
            else
            {
                ShowToastr("Data tidak ditemukan.", "error", "error");
            }
            //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "processclick", "processclick();", true);
            log.Info(LogLibrary.Logging("E", "GetWorklistDone", hfName.Value, "Finish GetWorklistDone"));
        }
        catch (Exception ex)
        {
            ShowToastr("Terjadi kesalahan pada sistem. Mohon hubungi staf IT di rumah sakit Anda.", "error", "error");
            log.Error(LogLibrary.Error("GetWorklistDone", hfName.Value, ex.Message));
        }
    }

    protected void bindddldone()
    {
        if (Session[Helper.wardcollection + hfguidadditional.Value] != null)
        {
            List<WardCollection> tempwardcoll = (List<WardCollection>)Session[Helper.wardcollection + hfguidadditional.Value];
            if (tempwardcoll.Where(y => y.WardId == 0).Count() == 0)
            {
                WardCollection temp = new WardCollection();
                temp.WardId = 0;
                temp.WardName = "Semua Bangsal";
                tempwardcoll.Insert(0, temp);
            }


            ddlWardListDone.DataSource = tempwardcoll;
            ddlWardListDone.DataTextField = "WardName";
            ddlWardListDone.DataValueField = "WardId";
            if (ddlWardListDone.SelectedValue.ToString() == "")
                ddlWardListDone.SelectedValue = "0";
            else
                ddlWardListDone.SelectedValue = ddlWardListDone.SelectedValue;
            ddlWardListDone.DataBind();
        }
        else
        {
            List<WardCollection> tempwardcoll = new List<WardCollection>();
            ddlWardListDone.DataSource = tempwardcoll;
            ddlWardListDone.DataTextField = "WardName";
            ddlWardListDone.DataValueField = "WardId";
            ddlWardListDone.DataBind();
        }
    }

    protected void rptDischargePlan_onItemBound(object sender, RepeaterItemEventArgs e)
    {
        if (Session[Helper.CollectionDischargePlan + hfguidadditional.Value] != null)
        {
            var data = ((WardCollection)e.Item.DataItem).WardId;
            List<ViewWorklistDischarge> listWorklistDischarge = (List<ViewWorklistDischarge>)Session[Helper.CollectionDischargePlan + hfguidadditional.Value];

            Label headername = (Label)e.Item.FindControl("LblHeader");
            Label lblcountward = (Label)e.Item.FindControl("lblcountward");

            lblcountward.Text = " ( " + listWorklistDischarge.Where(y => y.WardId == data && y.IsPrimary == true).Count().ToString() + " )";

            if (Session[Helper.SessionWard + hfguidadditional.Value] == null)
            {
                var GetWard = clsWard.GetWard(long.Parse(hfOrganizationId.Value));
                var ListWard = JsonConvert.DeserializeObject<ListWard>(GetWard.Result.ToString());

                List<Ward> listWard = ListWard.list;
                Session[Helper.SessionWard + hfguidadditional.Value] = listWard;
            }

            List<Ward> templistWard = (List<Ward>)Session[Helper.SessionWard + hfguidadditional.Value];
            string WardName = templistWard.Find(y => y.WardId == data).Name;
            headername.Text = WardName;

            List<ViewWorklistDischarge> listworklistsaatini = listWorklistDischarge.Where(y => y.IsLate == "0").ToList();

            List<Guid> tempworklistid = new List<Guid>();

            tempworklistid = (from x in listworklistsaatini
                              select x.WorklistId).Distinct().ToList();

            foreach (var x in tempworklistid)
            {
                List<ViewWorklistDischarge> tempworklist = listworklistsaatini.Where(y => y.WorklistId == x).ToList();
                if (tempworklist.Where(y => (y.WaitStatus == "TD" || y.WaitStatus == "TU") && y.VisitValue.ToLower() == "pending").Count() > 0)
                {
                    listworklistsaatini.Find(y => y.WorklistId == x && y.IsPrimary == true).flagSubmit = false;
                }
                else if (tempworklist.Where(y => y.IsPrescription == true && y.PrescriptionValue.ToLower() == "pending").Count() > 0)
                {
                    listworklistsaatini.Find(y => y.WorklistId == x && y.IsPrimary == true).flagSubmit = false;
                }
                else if (tempworklist.Where(y => y.IsRetur == true && y.ReturValue.ToLower() == "pending").Count() > 0)
                {
                    listworklistsaatini.Find(y => y.WorklistId == x && y.IsPrimary == true).flagSubmit = false;
                }

                else if (tempworklist.Where(y => y.IsPrimary == true && y.ResumeStatus.ToLower() == "pending").Count() > 0)
                {
                    listworklistsaatini.Find(y => y.WorklistId == x && y.IsPrimary == true).flagSubmit = false;
                }
                else
                    listworklistsaatini.Find(y => y.WorklistId == x && y.IsPrimary == true).flagSubmit = true;

                if (tempworklist.Where(y => y.IsPrimary == true && y.PayerName.ToLower() != "private").Count() > 0)
                {
                    if (tempworklist.Where(y => y.IsPrimary == true && y.LMAStatus.ToLower() == "pending").Count() > 0)
                    {
                        listworklistsaatini.Find(y => y.WorklistId == x && y.IsPrimary == true).flagSubmit = false;
                    }
                }
                //if (tempworklist.Find(y => y.IsPrimary == true).PayerName.ToLower() == "private")
                //    tempworklist.Find(y => y.IsPrimary == true).showsubmit = "1";
            }


            Session[Helper.CommonColorGrid] = null;
            DataTable dtsaatini = Helper.ToDataTable(listworklistsaatini.Where(y => y.IsLate == "0" && y.WardId == data).ToList());
            var gvwListDischargePlan = (GridView)e.Item.FindControl("gvLIstNewDischarge");
            gvwListDischargePlan.DataSource = dtsaatini;
            gvwListDischargePlan.DataBind();

            Session[Helper.CommonColorGrid] = null;
            DataTable dtakandatang = Helper.ToDataTable(listWorklistDischarge.Where(y => y.IsLate == "2" && y.WardId == data).ToList());
            var gvLIstAkanDatang = (GridView)e.Item.FindControl("gvLIstAkanDatang");
            var divflaglabel = (System.Web.UI.HtmlControls.HtmlGenericControl)e.Item.FindControl("divflaglabel");
            if (dtakandatang.Rows.Count > 0)
            {

                gvLIstAkanDatang.DataSource = dtakandatang;
                gvLIstAkanDatang.DataBind();
            }
            else
            {
                divflaglabel.Style.Add("display", "none");
                gvLIstAkanDatang.Style.Add("display", "none");

            }
        }

    }

    protected void rptDischargeDone_onItemBound(object sender, RepeaterItemEventArgs e)
    {
        if (Session[Helper.CollectionDischargeDone + hfguidadditional.Value] != null)
        {
            List<ViewDischargeRequest> listWorklistDischarge = (List<ViewDischargeRequest>)Session[Helper.CollectionDischargeDone + hfguidadditional.Value];
            var data = ((WardCollection)e.Item.DataItem).WardId;
            DataTable dt = Helper.ToDataTable(listWorklistDischarge.Where(y => y.WardId == data).ToList());


            var gvLIstDischargeProcess = (GridView)e.Item.FindControl("gvLIstDischargeDone");
            Label headername = (Label)e.Item.FindControl("LblHeader");
            Label lblcountward = (Label)e.Item.FindControl("lblcountward");
            lblcountward.Text = " ( " + listWorklistDischarge.Where(y => y.WardId == data && y.IsPrimary == true).Count().ToString() + " )";
            if (Session[Helper.SessionWard + hfguidadditional.Value] == null)
            {
                var GetWard = clsWard.GetWard(long.Parse(hfOrganizationId.Value));
                var ListWard = JsonConvert.DeserializeObject<ListWard>(GetWard.Result.ToString());

                List<Ward> listWard = ListWard.list;
                Session[Helper.SessionWard + hfguidadditional.Value] = listWard;
            }

            List<Ward> templistWard = (List<Ward>)Session[Helper.SessionWard + hfguidadditional.Value];
            string WardName = templistWard.Find(y => y.WardId == data).Name;

            TextInfo textInfo = new CultureInfo("en-US", false).TextInfo;
            WardName = textInfo.ToTitleCase(WardName);

            Session[Helper.CommonColorGrid] = null;
            headername.Text = WardName;
            gvLIstDischargeProcess.DataSource = dt;
            gvLIstDischargeProcess.DataBind();
        }
    }

    protected void LnkPatientDetailDone_onClick(object sender, EventArgs e)
    {
        try
        {
            log.Info(LogLibrary.Logging("S", "LnkPatientDetailDone_onClick", hfName.Value, ""));


            int selRowIndex = ((GridViewRow)(((LinkButton)sender).Parent.Parent)).RowIndex;
            int repeaterindex = ((RepeaterItem)(((LinkButton)sender).Parent.Parent.Parent.Parent.NamingContainer)).ItemIndex;

            RepeaterItemCollection ri = rptDischargeDone.Items;

            var gvLIstDischargeProcess = (GridView)ri[repeaterindex].FindControl("gvLIstDischargeDone");

            HiddenField worklistId = (HiddenField)gvLIstDischargeProcess.Rows[selRowIndex].FindControl("lbWorklistId");
            HiddenField admissionId = (HiddenField)gvLIstDischargeProcess.Rows[selRowIndex].FindControl("lbAdmissionId");
            HiddenField ModifiedDate = (HiddenField)gvLIstDischargeProcess.Rows[selRowIndex].FindControl("ModifiedDate");

            log.Debug(LogLibrary.Logging("S", "GetPatientDetail", hfName.Value, ""));
            var patientDetail = clsWorklist.GetPatientDetail(Int64.Parse(hfOrganizationId.Value), Guid.Parse(worklistId.Value));
            ListDischargeDetail getTotalpatientDetail = JsonConvert.DeserializeObject<ListDischargeDetail>(patientDetail.Result.ToString());
            log.Debug(LogLibrary.Logging("E", "GetPatientDetail", hfName.Value, getTotalpatientDetail.ToString()));

            if (getTotalpatientDetail != null)
            {
                var resultFirstPatientDetail = (from zzz in getTotalpatientDetail.list
                                                where zzz.IsPrimary == true
                                                select new DischargeDetail
                                                {
                                                    PatientName = zzz.PatientName,
                                                    Gender = zzz.Gender,
                                                    LocalMrNo = zzz.LocalMrNo,
                                                    WorklistDate = zzz.WorklistDate,
                                                    RoomNo = zzz.RoomNo,
                                                    Age = zzz.Age,
                                                    BirthDate = zzz.BirthDate,
                                                    PayerName = zzz.PayerName,
                                                    MobileNo = zzz.MobileNo,
                                                    AdditionalNotes = zzz.AdditionalNotes,
                                                    DoctorName = zzz.DoctorName,
                                                    WaitStatus = zzz.WaitStatus,
                                                    Remarks = zzz.Remarks,
                                                    IsPrescription = zzz.IsPrescription,
                                                    PrescriptionValue = zzz.PrescriptionValue,
                                                    IsRetur = zzz.IsRetur,
                                                    ReturValue = zzz.ReturValue,
                                                    WorklistId = zzz.WorklistId,
                                                    VisitValue = zzz.VisitValue,
                                                    AdmissionNo = zzz.AdmissionNo,
                                                    IsDoctorVisit = zzz.IsDoctorVisit,
                                                    CreatedBy = zzz.CreatedBy,
                                                    CreatedDate = zzz.CreatedDate,
                                                    ModifiedBy = zzz.ModifiedBy,
                                                    ModifiedDate = zzz.ModifiedDate,
                                                    SubmitBy = zzz.SubmitBy,
                                                    SubmitDate = zzz.SubmitDate
                                                }).First();
                lblName.Text = resultFirstPatientDetail.PatientName;


                lblSexId.Text = resultFirstPatientDetail.Gender;
                if (lblSexId.Text == "M")
                    lblSexId.Style.Add("color", "#2A3593");
                else
                    lblSexId.Style.Add("color", "#FF00D5");

                lblAdmissionNoDetail.Text = resultFirstPatientDetail.AdmissionNo;
                lblMrNo.Text = resultFirstPatientDetail.LocalMrNo;
                lblWorklistDate.Text = resultFirstPatientDetail.WorklistDate.ToString("dd-MMM-yyyy");
                lblRoom.Text = resultFirstPatientDetail.RoomNo;
                lblPayerName.Text = resultFirstPatientDetail.PayerName;
                lblAge.Text = resultFirstPatientDetail.Age;
                lblDoB.Text = resultFirstPatientDetail.BirthDate.ToString("dd MMM yyyy");
                lblMobileNo.Text = resultFirstPatientDetail.MobileNo == null || resultFirstPatientDetail.MobileNo == "" ? "-" : resultFirstPatientDetail.MobileNo;
                //lblMobileNo.Text = "089638986866";
                lblPrimaryDoctorName.Text = resultFirstPatientDetail.DoctorName;
                lblRetur.Text = resultFirstPatientDetail.IsRetur == true ? "Retur Obat" : "";
                lblPrescription.Text = resultFirstPatientDetail.IsPrescription == true ? "Resep Pulang" : "";
                if (resultFirstPatientDetail.WaitStatus == "TD")
                {
                    if (resultFirstPatientDetail.VisitValue == "DONE")
                    {
                        imgWaitStatus.ImageUrl = "~/Images/Icon/icon_checked_locked.svg";
                        lblWaitStatus.Text = "Tunggu dokter";
                        lblRemarks.Visible = false;
                    }
                    else
                    {
                        imgWaitStatus.ImageUrl = "~/Images/Icon/icon_unchecked.svg";
                        lblWaitStatus.Text = "Tunggu dokter";
                        lblRemarks.Visible = false;
                    }
                }
                else if (resultFirstPatientDetail.WaitStatus == "TUTD")
                {
                    imgWaitStatus.ImageUrl = "~/Images/Icon/icon_uncheckedcross.svg";
                    lblWaitStatus.Text = "Tidak tunggu dokter";
                    lblRemarks.Visible = false;
                }
                else if (resultFirstPatientDetail.WaitStatus == "TU")
                {
                    if (resultFirstPatientDetail.VisitValue == "DONE")
                    {
                        imgWaitStatus.ImageUrl = "~/Images/Icon/icon_checked_locked.svg";
                        lblWaitStatus.Text = "Tidak tunggu dokter";
                        lblRemarks.Visible = true;
                    }
                    else
                    {
                        imgWaitStatus.ImageUrl = "~/Images/Icon/icon_unchecked.svg";
                        lblWaitStatus.Text = "Tidak tunggu dokter";
                        lblRemarks.Visible = true;
                    }

                }
                lblRemarks.Text = "</br>tunggu hasil " + resultFirstPatientDetail.Remarks;

                if (resultFirstPatientDetail.IsPrescription == true)
                {
                    if (resultFirstPatientDetail.PrescriptionValue == "DONE")
                    {
                        imgPrescription.ImageUrl = "~/Images/Icon/icon_checked_locked.svg";
                        lblPrescription.Text = "Resep pulang";
                    }
                    else if (resultFirstPatientDetail.PrescriptionValue == "PENDING")
                    {
                        imgPrescription.ImageUrl = "~/Images/Icon/icon_unchecked.svg";
                        lblPrescription.Text = "Resep pulang";
                    }
                }
                else
                {
                    imgPrescription.ImageUrl = "~/Images/Icon/icon_uncheckedcross.svg";
                    lblPrescription.Text = "Tidak ada resep pulang";
                }

                if (resultFirstPatientDetail.IsRetur == true)
                {
                    if (resultFirstPatientDetail.ReturValue == "DONE")
                    {
                        imgRetur.ImageUrl = "~/Images/Icon/icon_checked_locked.svg";
                        lblRetur.Text = "Retur obat";
                    }
                    else if (resultFirstPatientDetail.ReturValue == "PENDING")
                    {
                        imgRetur.ImageUrl = "~/Images/Icon/icon_unchecked.svg";
                        lblRetur.Text = "Retur obat";
                    }
                }
                else
                {
                    imgRetur.ImageUrl = "~/Images/Icon/icon_uncheckedcross.svg";
                    lblRetur.Text = "Tidak ada retur obat";
                }

                lblAdditionalNotes.Text = resultFirstPatientDetail.AdditionalNotes.Replace("\n", "<br />");
                lblCreatedBy.Text = resultFirstPatientDetail.CreatedBy;
                lblCreatedDate.Text = resultFirstPatientDetail.CreatedDate.ToString("dd/MM/yyy HH:mm");

                if (resultFirstPatientDetail.CreatedDate.ToString("yyyy-MM-dd HH:mm:ss") == "0001-01-01 00:00:00")
                {
                    lblCreatedDate.Visible = false;
                }

                lblUpdatedBy.Text = resultFirstPatientDetail.ModifiedBy;
                lblUpdatedDate.Text = resultFirstPatientDetail.ModifiedDate.ToString("dd/MM/yyy HH:mm");

                if (resultFirstPatientDetail.ModifiedDate.ToString("yyyy-MM-dd HH:mm:ss") == "0001-01-01 00:00:00")
                {
                    lblUpdatedDate.Visible = false;
                }

                lblSubmitBy.Text = resultFirstPatientDetail.SubmitBy;
                lblSubmitDate.Text = resultFirstPatientDetail.SubmitDate.ToString("dd/MM/yyy HH:mm");

                if (resultFirstPatientDetail.SubmitDate.ToString("yyyy-MM-dd HH:mm:ss") == "0001-01-01 00:00:00")
                {
                    lblSubmitDate.Visible = false;
                }

                DataTable dtDoctorConsultation = new DataTable();
                var resultPatientDetailConsultation = (from zzz in getTotalpatientDetail.list
                                                       where zzz.IsPrimary == false
                                                       select new DischargeDetail
                                                       {
                                                           DoctorName = zzz.DoctorName,
                                                           WaitStatus = zzz.WaitStatus,
                                                           Remarks = zzz.Remarks,
                                                           IsPrescription = zzz.IsPrescription,
                                                           PrescriptionValue = zzz.PrescriptionValue,
                                                           IsRetur = zzz.IsRetur,
                                                           ReturValue = zzz.ReturValue,
                                                           WorklistId = zzz.WorklistId,
                                                           VisitValue = zzz.VisitValue,
                                                           IsDoctorVisit = zzz.IsDoctorVisit
                                                       }).ToList();

                if (resultPatientDetailConsultation.Count > 0)
                {
                    llblNoKonsulen.Visible = false;
                    //tempwardcoll = (from a in tempwardcoll orderby a.WardName select a).ToList();

                    dtDoctorConsultation = Helper.ToDataTable(resultPatientDetailConsultation);
                    repeaterDoctorContultation.DataSource = dtDoctorConsultation;
                    repeaterDoctorContultation.DataBind();
                }
                else
                {
                    llblNoKonsulen.Visible = true;
                    repeaterDoctorContultation.DataSource = null;
                    repeaterDoctorContultation.DataBind();
                }
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalDetail();", true);
            }
            else
            {
                ShowToastr("Data tidak ditemukan.", "error", "error");
            }
            log.Info(LogLibrary.Logging("E", "LnkPatientDetailDone_onClick", hfName.Value, "Finish LnkPatientDetailDone_onClick"));
        }
        catch (Exception ex)
        {
            log.Error(LogLibrary.Error("LnkPatientDetailDone_onClick", hfName.Value, ex.Message));
        }
    }

    //================================= TEST WEB SOCKET ================================================

    protected void refreshSocket_onClick(object sender, EventArgs e)
    {
        try
        {
            log.Info(LogLibrary.Logging("S", "getListPatient", hfName.Value, ""));

            List<ViewWorklistDischarge> listWorklistDischarge = new List<ViewWorklistDischarge>();
            lblDateRefreshWorklist.Text = DateTime.Now.ToString("dd MMM yyyy, HH:mm");
            if (hfOrganizationId.Value != "")
            {
                List<Guid> tempworklistid = new List<Guid>();
                long wardId = 0;
                if (ddlWardlist.SelectedValue != "")
                    wardId = long.Parse(ddlWardlist.SelectedValue);

                string jsonworklistsocket = jsonworklist.Value;

                log.Debug(LogLibrary.Logging("S", "getListPatientDischarge", hfName.Value, ""));
                //var worklist = clsWorklist.getListPatientDischarge(long.Parse(hfOrganizationId.Value), DateTime.Parse(textboxdateto.Text).ToString("yyyy-MM-dd"), wardId, txtFindPatient.Text);
                var getTotalWorklist = JsonConvert.DeserializeObject<ListViewWorklistDischarge>(jsonworklist.Value);
                log.Debug(LogLibrary.Logging("E", "getListPatientDischarge", hfName.Value, getTotalWorklist.ToString()));

                if (getTotalWorklist != null)
                {
                    listWorklistDischarge = getTotalWorklist.list.Where(y => y.WorklistDate.ToString("yyyy-MM-dd") == DateTime.Parse(textboxdateto.Text).ToString("yyyy-MM-dd")).ToList();

                    tempworklistid = (from x in listWorklistDischarge
                                      select x.WorklistId).Distinct().ToList();

                    lblCountPlan.Text = " (" + listWorklistDischarge.Where(y => y.IsLate == "0" && y.IsPrimary == true).Count().ToString() + ") ";
                    foreach (var x in tempworklistid)
                    {
                        List<ViewWorklistDischarge> tempworklist = listWorklistDischarge.Where(y => y.WorklistId == x).ToList();
                        if (tempworklist.Where(y => y.IsDoctorVisit == true && y.VisitValue.ToLower() == "pending").Count() > 0)
                        {
                            listWorklistDischarge.Find(y => y.WorklistId == x && y.IsPrimary == true).flagSubmit = false;
                        }
                        else if (tempworklist.Where(y => y.IsPrescription == true && y.PrescriptionValue.ToLower() == "pending").Count() > 0)
                        {
                            listWorklistDischarge.Find(y => y.WorklistId == x && y.IsPrimary == true).flagSubmit = false;
                        }
                        else if (tempworklist.Where(y => y.IsRetur == true && y.ReturValue.ToLower() == "pending").Count() > 0)
                        {
                            listWorklistDischarge.Find(y => y.WorklistId == x && y.IsPrimary == true).flagSubmit = false;
                        }
                        else if (tempworklist.Where(y => y.IsPrimary == true && y.LMAStatus.ToLower() == "pending").Count() > 0)
                        {
                            listWorklistDischarge.Find(y => y.WorklistId == x && y.IsPrimary == true).flagSubmit = false;
                        }
                        else if (tempworklist.Where(y => y.IsPrimary == true && y.ResumeStatus.ToLower() == "pending").Count() > 0)
                        {
                            listWorklistDischarge.Find(y => y.WorklistId == x && y.IsPrimary == true).flagSubmit = false;
                        }
                        else
                            listWorklistDischarge.Find(y => y.WorklistId == x && y.IsPrimary == true).flagSubmit = true;
                    }

                    List<long> WardCollection = (from x in listWorklistDischarge
                                                 where x.IsLate == "0"
                                                 select x.WardId).Distinct().ToList();

                    Session[Helper.CollectionDischargePlan + hfguidadditional.Value] = listWorklistDischarge.Where(y => y.IsLate == "0").ToList();
                    List<WardCollection> tempwardcoll = new List<WardCollection>();

                    if (Session[Helper.SessionWard + hfguidadditional.Value] == null)
                    {
                        var GetWard = clsWard.GetWard(long.Parse(hfOrganizationId.Value));
                        var ListWard = JsonConvert.DeserializeObject<ListWard>(GetWard.Result.ToString());

                        Session[Helper.SessionWard + hfguidadditional.Value] = ListWard.list;
                    }


                    List<Ward> listWard = (List<Ward>)Session[Helper.SessionWard + hfguidadditional.Value];

                    foreach (var x in WardCollection)
                    {
                        WardCollection temp = new WardCollection();
                        temp.WardId = x;
                        temp.WardName = listWard.Find(y => y.WardId == x).Name;
                        tempwardcoll.Add(temp);
                    }
                    Session[Helper.wardcollection + hfguidadditional.Value] = tempwardcoll;

                    rptDischargePlan.DataSource = tempwardcoll;
                    rptDischargePlan.DataBind();

                }
                else
                {
                    ShowToastr("Data tidak ditemukan.", "error", "error");
                }
            }
            else
            {
                {
                    //Response.Redirect(String.Format("~/Form/General/Login?action=clear"));
                    Context.ApplicationInstance.CompleteRequest();
                    return;
                }
            }
            log.Info(LogLibrary.Logging("E", "getListPatient", hfName.Value, "Finish getListPatient"));
        }
        catch (Exception ex)
        {
            ShowToastr("Terjadi kesalahan pada sistem. Mohon hubungi staf IT di rumah sakit Anda.", "error", "error");
            log.Error(LogLibrary.Error("getListPatient", hfName.Value, ex.Message));
        }
    }

    protected void LnkProcess_onClick(object sender, EventArgs e)
    {
        try
        {
            log.Info(LogLibrary.Logging("S", "LnkProcess_onClick", hfName.Value, ""));

            txtsearchprocess.Text = "";
            divDischargeProcess.Visible = true;
            divprocess.Style.Add("border", "solid #2A3593");

            divprocess.Style.Add("padding-top", "7px");
            divdone.Style.Add("padding-top", "2px");
            divplan.Style.Add("padding-top", "2px");

            divplan.Attributes["class"] = "col-sm-2 btnFilter";
            divprocess.Attributes["class"] = "col-sm-2 btnClickCSS";
            divdone.Attributes["class"] = "col-sm-2 btnFilter";

            divDischargePlan.Visible = false;
            divDischargeDone.Visible = false;

            divplan.Style.Add("border", "none");
            divdone.Style.Add("border", "none");

            ddlWardListProcess.SelectedValue = "0";
            txtdateprocess.Text = "";
            GetWorklistProcess();
            getCountPatient();

            List<WardCollection> tempwardcoll = (List<WardCollection>)Session[Helper.wardcollection + hfguidadditional.Value];
            WardCollection temp = new WardCollection();
            temp.WardId = 0;
            temp.WardName = "Semua Bangsal";
            tempwardcoll.Insert(0, temp);

            ddlWardListProcess.DataSource = tempwardcoll;
            ddlWardListProcess.DataTextField = "WardName";
            ddlWardListProcess.DataValueField = "WardId";
            ddlWardListProcess.SelectedValue = "0";
            ddlWardListProcess.DataBind();

            if (txtdateprocess.Text == "")
            {
                btnClearDateProcess.Visible = false;
                btnShowCalendarProcess.Visible = true;
            }
            else
            {
                btnClearDateProcess.Visible = true;
                btnShowCalendarProcess.Visible = false;
            }

            log.Info(LogLibrary.Logging("E", "LnkProcess_onClick", hfName.Value, "Finish LnkProcess_onClick"));
        }
        catch (Exception ex)
        {
            log.Error(LogLibrary.Error("LnkProcess_onClick", hfName.Value, ex.Message));
        }
    }

    protected void LnkDone_onClick(object sender, EventArgs e)
    {
        try
        {
            log.Info(LogLibrary.Logging("S", "LnkDone_onClick", hfName.Value, ""));

            if (txtdatedoneto.Text != "")
            {
                btnShowClear.Visible = true;
                btnShowCalendar.Visible = false;
            }
            else
            {
                btnShowClear.Visible = false;
                btnShowCalendar.Visible = true;
            }


            txtdatedoneto.Text = "";
            txtsearchdone.Text = "";
            divDischargeProcess.Visible = false;
            divDischargePlan.Visible = false;
            divDischargeDone.Visible = true;

            divdone.Style.Add("border", "solid #2A3593");
            divdone.Style.Add("padding-top", "7px");
            divplan.Style.Add("padding-top", "0px");
            divprocess.Style.Add("padding-top", "0px");

            divplan.Style.Add("border", "none");
            divprocess.Style.Add("border", "none");


            divplan.Attributes["class"] = "col-sm-2 btnFilter";
            divprocess.Attributes["class"] = "col-sm-2 btnFilter";
            divdone.Attributes["class"] = "col-sm-2 btnClickCSS";

            GetWorklistDone();
            getCountPatient();



            log.Info(LogLibrary.Logging("E", "LnkDone_onClick", hfName.Value, "Finish LnkDone_onClick"));
        }
        catch (Exception ex)
        {
            log.Error(LogLibrary.Error("LnkDone_onClick", hfName.Value, ex.Message));
        }
    }

    protected void LnkPlan_onClick(object sender, EventArgs e)
    {
        try
        {
            log.Info(LogLibrary.Logging("S", "LnkPlan_onClick", hfName.Value, ""));

            if (textboxdateto.Text == "")
            {
                btnClearDatePlan.Visible = false;
                btnShowCalendarPlan.Visible = true;
            }
            else
            {
                btnClearDatePlan.Visible = true;
                btnShowCalendarPlan.Visible = false;
            }

            txtFindPatient.Text = "";
            //textboxdateto.Text = DateTime.Now.ToString("dd-MMM-yyyy");

            divDischargePlan.Visible = true;
            divplan.Style.Add("border", "solid #2A3593");

            divplan.Attributes["class"] = "col-sm-2 btnClickCSS";
            divplan.Style.Add("padding-top", "7px");
            divdone.Style.Add("padding-top", "2px");
            divprocess.Style.Add("padding-top", "2px");
            divprocess.Attributes["class"] = "col-sm-2 btnFilter";
            divdone.Attributes["class"] = "col-sm-2 btnFilter";

            divDischargeProcess.Visible = false;
            divprocess.Style.Add("border", "none");

            divDischargeDone.Visible = false;
            divdone.Style.Add("border", "none");
            ddlWardlist.SelectedValue = "0";

            getListPatient();
            getCountPatient();

            List<WardCollection> tempwardcoll = (List<WardCollection>)Session[Helper.wardcollection + hfguidadditional.Value];
            WardCollection temp = new WardCollection();
            temp.WardId = 0;
            temp.WardName = "Semua Bangsal";
            tempwardcoll.Insert(0, temp);

            ddlWardlist.DataSource = tempwardcoll;
            ddlWardlist.DataTextField = "WardName";
            ddlWardlist.DataValueField = "WardId";
            ddlWardlist.SelectedValue = "0";
            ddlWardlist.DataBind();

            log.Info(LogLibrary.Logging("E", "LnkPlan_onClick", hfName.Value, "Finish LnkPlan_onClick"));
        }
        catch (Exception ex)
        {
            log.Error(LogLibrary.Error("LnkPlan_onClick", hfName.Value, ex.Message));
        }
    }

    protected void LnkPatientDetailProcess_onClick(object sender, EventArgs e)
    {
        try
        {
            log.Info(LogLibrary.Logging("S", "LnkPatientDetailProcess_onClick", hfName.Value, ""));

            int selRowIndex = ((GridViewRow)(((LinkButton)sender).Parent.Parent)).RowIndex;
            int repeaterindex = ((RepeaterItem)(((LinkButton)sender).Parent.Parent.Parent.Parent.NamingContainer)).ItemIndex;

            RepeaterItemCollection ri = rptDischargeProcess.Items;

            var gvLIstDischargeProcess = (GridView)ri[repeaterindex].FindControl("gvLIstDischargeProcess");

            HiddenField worklistId = (HiddenField)gvLIstDischargeProcess.Rows[selRowIndex].FindControl("lbWorklistId");
            HiddenField admissionId = (HiddenField)gvLIstDischargeProcess.Rows[selRowIndex].FindControl("lbAdmissionId");
            HiddenField ModifiedDate = (HiddenField)gvLIstDischargeProcess.Rows[selRowIndex].FindControl("ModifiedDate");

            var patientDetail = clsWorklist.GetPatientDetail(Int64.Parse(hfOrganizationId.Value), Guid.Parse(worklistId.Value));
            ListDischargeDetail getTotalpatientDetail = JsonConvert.DeserializeObject<ListDischargeDetail>(patientDetail.Result.ToString());

            if (getTotalpatientDetail != null)
            {
                var resultFirstPatientDetail = (from zzz in getTotalpatientDetail.list
                                                where zzz.IsPrimary == true
                                                select new DischargeDetail
                                                {
                                                    PatientName = zzz.PatientName,
                                                    Gender = zzz.Gender,
                                                    LocalMrNo = zzz.LocalMrNo,
                                                    WorklistDate = zzz.WorklistDate,
                                                    RoomNo = zzz.RoomNo,
                                                    Age = zzz.Age,
                                                    BirthDate = zzz.BirthDate,
                                                    PayerName = zzz.PayerName,
                                                    MobileNo = zzz.MobileNo,
                                                    AdditionalNotes = zzz.AdditionalNotes,
                                                    DoctorName = zzz.DoctorName,
                                                    WaitStatus = zzz.WaitStatus,
                                                    Remarks = zzz.Remarks,
                                                    IsPrescription = zzz.IsPrescription,
                                                    PrescriptionValue = zzz.PrescriptionValue,
                                                    IsRetur = zzz.IsRetur,
                                                    ReturValue = zzz.ReturValue,
                                                    WorklistId = zzz.WorklistId,
                                                    VisitValue = zzz.VisitValue,
                                                    AdmissionNo = zzz.AdmissionNo,
                                                    IsDoctorVisit = zzz.IsDoctorVisit,
                                                    CreatedBy = zzz.CreatedBy,
                                                    CreatedDate = zzz.CreatedDate,
                                                    ModifiedBy = zzz.ModifiedBy,
                                                    ModifiedDate = zzz.ModifiedDate,
                                                    SubmitBy = zzz.SubmitBy,
                                                    SubmitDate = zzz.SubmitDate
                                                }).First();
                lblName.Text = resultFirstPatientDetail.PatientName;


                lblSexId.Text = resultFirstPatientDetail.Gender;
                if (lblSexId.Text == "M")
                    lblSexId.Style.Add("color", "#2A3593");
                else
                    lblSexId.Style.Add("color", "#FF00D5");

                lblAdmissionNoDetail.Text = resultFirstPatientDetail.AdmissionNo;
                lblMrNo.Text = resultFirstPatientDetail.LocalMrNo;
                lblWorklistDate.Text = resultFirstPatientDetail.WorklistDate.ToString("dd-MMM-yyyy");
                lblRoom.Text = resultFirstPatientDetail.RoomNo;
                lblPayerName.Text = resultFirstPatientDetail.PayerName;
                lblAge.Text = resultFirstPatientDetail.Age;
                lblDoB.Text = resultFirstPatientDetail.BirthDate.ToString("dd MMM yyyy");
                lblMobileNo.Text = resultFirstPatientDetail.MobileNo == null || resultFirstPatientDetail.MobileNo == "" ? "-" : resultFirstPatientDetail.MobileNo;
                //lblMobileNo.Text = "089638986866";
                lblPrimaryDoctorName.Text = resultFirstPatientDetail.DoctorName;
                lblRetur.Text = resultFirstPatientDetail.IsRetur == true ? "Retur Obat" : "";
                lblPrescription.Text = resultFirstPatientDetail.IsPrescription == true ? "Resep Pulang" : "";
                if (resultFirstPatientDetail.WaitStatus == "TD")
                {
                    if (resultFirstPatientDetail.VisitValue == "DONE")
                    {
                        imgWaitStatus.ImageUrl = "~/Images/Icon/icon_checked_locked.svg";
                        lblWaitStatus.Text = "Tunggu dokter";
                        lblRemarks.Visible = false;
                    }
                    else
                    {
                        imgWaitStatus.ImageUrl = "~/Images/Icon/icon_unchecked.svg";
                        lblWaitStatus.Text = "Tunggu dokter";
                        lblRemarks.Visible = false;
                    }
                }
                else if (resultFirstPatientDetail.WaitStatus == "TUTD")
                {
                    imgWaitStatus.ImageUrl = "~/Images/Icon/icon_uncheckedcross.svg";
                    lblWaitStatus.Text = "Tidak tunggu dokter";
                    lblRemarks.Visible = false;
                }
                else if (resultFirstPatientDetail.WaitStatus == "TU")
                {
                    if (resultFirstPatientDetail.VisitValue == "DONE")
                    {
                        imgWaitStatus.ImageUrl = "~/Images/Icon/icon_checked_locked.svg";
                        lblWaitStatus.Text = "Tidak tunggu dokter";
                        lblRemarks.Visible = true;
                    }
                    else
                    {
                        imgWaitStatus.ImageUrl = "~/Images/Icon/icon_unchecked.svg";
                        lblWaitStatus.Text = "Tidak tunggu dokter";
                        lblRemarks.Visible = true;
                    }

                }
                lblRemarks.Text = "</br>tunggu hasil " + resultFirstPatientDetail.Remarks;

                if (resultFirstPatientDetail.IsPrescription == true)
                {
                    if (resultFirstPatientDetail.PrescriptionValue == "DONE")
                    {
                        imgPrescription.ImageUrl = "~/Images/Icon/icon_checked_locked.svg";
                        lblPrescription.Text = "Resep pulang";
                    }
                    else if (resultFirstPatientDetail.PrescriptionValue == "PENDING")
                    {
                        imgPrescription.ImageUrl = "~/Images/Icon/icon_unchecked.svg";
                        lblPrescription.Text = "Resep pulang";
                    }
                }
                else
                {
                    imgPrescription.ImageUrl = "~/Images/Icon/icon_uncheckedcross.svg";
                    lblPrescription.Text = "Tidak ada resep pulang";
                }

                if (resultFirstPatientDetail.IsRetur == true)
                {
                    if (resultFirstPatientDetail.ReturValue == "DONE")
                    {
                        imgRetur.ImageUrl = "~/Images/Icon/icon_checked_locked.svg";
                        lblRetur.Text = "Retur obat";
                    }
                    else if (resultFirstPatientDetail.ReturValue == "PENDING")
                    {
                        imgRetur.ImageUrl = "~/Images/Icon/icon_unchecked.svg";
                        lblRetur.Text = "Retur obat";
                    }
                }
                else
                {
                    imgRetur.ImageUrl = "~/Images/Icon/icon_uncheckedcross.svg";
                    lblRetur.Text = "Tidak ada retur obat";
                }

                lblAdditionalNotes.Text = resultFirstPatientDetail.AdditionalNotes.Replace("\n", "<br />");
                lblCreatedBy.Text = resultFirstPatientDetail.CreatedBy;
                lblCreatedDate.Text = resultFirstPatientDetail.CreatedDate.ToString("dd/MM/yyy HH:mm");

                if (resultFirstPatientDetail.CreatedDate.ToString("yyyy-MM-dd HH:mm:ss") == "0001-01-01 00:00:00")
                {
                    lblCreatedDate.Visible = false;
                }

                lblUpdatedBy.Text = resultFirstPatientDetail.ModifiedBy;
                lblUpdatedDate.Text = resultFirstPatientDetail.ModifiedDate.ToString("dd/MM/yyy HH:mm");

                if (resultFirstPatientDetail.ModifiedDate.ToString("yyyy-MM-dd HH:mm:ss") == "0001-01-01 00:00:00")
                {
                    lblUpdatedDate.Visible = false;
                }

                lblSubmitBy.Text = resultFirstPatientDetail.SubmitBy;
                lblSubmitDate.Text = resultFirstPatientDetail.SubmitDate.ToString("dd/MM/yyy HH:mm");

                if (resultFirstPatientDetail.SubmitDate.ToString("yyyy-MM-dd HH:mm:ss") == "0001-01-01 00:00:00")
                {
                    lblSubmitDate.Visible = false;
                }

                DataTable dtDoctorConsultation = new DataTable();
                var resultPatientDetailConsultation = (from zzz in getTotalpatientDetail.list
                                                       where zzz.IsPrimary == false
                                                       select new DischargeDetail
                                                       {
                                                           DoctorName = zzz.DoctorName,
                                                           WaitStatus = zzz.WaitStatus,
                                                           Remarks = zzz.Remarks,
                                                           IsPrescription = zzz.IsPrescription,
                                                           PrescriptionValue = zzz.PrescriptionValue,
                                                           IsRetur = zzz.IsRetur,
                                                           ReturValue = zzz.ReturValue,
                                                           WorklistId = zzz.WorklistId,
                                                           VisitValue = zzz.VisitValue,
                                                           IsDoctorVisit = zzz.IsDoctorVisit
                                                       }).ToList();

                if (resultPatientDetailConsultation.Count > 0)
                {
                    llblNoKonsulen.Visible = false;
                    dtDoctorConsultation = Helper.ToDataTable(resultPatientDetailConsultation);
                    repeaterDoctorContultation.DataSource = dtDoctorConsultation;
                    repeaterDoctorContultation.DataBind();
                }
                else
                {
                    llblNoKonsulen.Visible = true;
                    repeaterDoctorContultation.DataSource = null;
                    repeaterDoctorContultation.DataBind();
                }
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalDetail();", true);
            }
            else
            {
                ShowToastr("Data tidak ditemukan", "error", "error");
            }
            log.Info(LogLibrary.Logging("E", "LnkPatientDetailProcess_onClick", hfName.Value, "Finish LnkPatientDetailProcess_onClick"));
        }
        catch (Exception ex)
        {
            log.Error(LogLibrary.Error("LnkPatientDetailProcess_onClick", hfName.Value, ex.Message));
        }
    }

    protected void LnkPatientDetailPlan_onClick(object sender, EventArgs e)
    {
        try
        {
            log.Info(LogLibrary.Logging("S", "LnkPatientDetailPlan_onClick", hfName.Value, ""));

            int selRowIndex = ((GridViewRow)(((LinkButton)sender).Parent.Parent)).RowIndex;
            int repeaterindex = ((RepeaterItem)(((LinkButton)sender).Parent.Parent.Parent.Parent.NamingContainer)).ItemIndex;

            RepeaterItemCollection ri = rptDischargePlan.Items;

            var gvLIstDischargeProcess = (GridView)ri[repeaterindex].FindControl("gvLIstNewDischarge");

            HiddenField worklistId = (HiddenField)gvLIstDischargeProcess.Rows[selRowIndex].FindControl("lbWorklistId");
            HiddenField admissionId = (HiddenField)gvLIstDischargeProcess.Rows[selRowIndex].FindControl("lbAdmissionId");
            HiddenField ModifiedDate = (HiddenField)gvLIstDischargeProcess.Rows[selRowIndex].FindControl("ModifiedDate");

            log.Debug(LogLibrary.Logging("S", "GetPatientDetail", hfName.Value, ""));
            var patientDetail = clsWorklist.GetPatientDetail(Int64.Parse(hfOrganizationId.Value), Guid.Parse(worklistId.Value));
            ListDischargeDetail getTotalpatientDetail = JsonConvert.DeserializeObject<ListDischargeDetail>(patientDetail.Result.ToString());
            log.Debug(LogLibrary.Logging("E", "GetPatientDetail", hfName.Value, getTotalpatientDetail.ToString()));

            if (getTotalpatientDetail != null)
            {
                var resultFirstPatientDetail = (from zzz in getTotalpatientDetail.list
                                                where zzz.IsPrimary == true
                                                select new DischargeDetail
                                                {
                                                    PatientName = zzz.PatientName,
                                                    Gender = zzz.Gender,
                                                    LocalMrNo = zzz.LocalMrNo,
                                                    WorklistDate = zzz.WorklistDate,
                                                    RoomNo = zzz.RoomNo,
                                                    Age = zzz.Age,
                                                    BirthDate = zzz.BirthDate,
                                                    PayerName = zzz.PayerName,
                                                    MobileNo = zzz.MobileNo,
                                                    AdditionalNotes = zzz.AdditionalNotes,
                                                    DoctorName = zzz.DoctorName,
                                                    WaitStatus = zzz.WaitStatus,
                                                    Remarks = zzz.Remarks,
                                                    IsPrescription = zzz.IsPrescription,
                                                    PrescriptionValue = zzz.PrescriptionValue,
                                                    IsRetur = zzz.IsRetur,
                                                    ReturValue = zzz.ReturValue,
                                                    WorklistId = zzz.WorklistId,
                                                    VisitValue = zzz.VisitValue,
                                                    AdmissionNo = zzz.AdmissionNo,
                                                    IsDoctorVisit = zzz.IsDoctorVisit,
                                                    CreatedBy = zzz.CreatedBy,
                                                    CreatedDate = zzz.CreatedDate,
                                                    ModifiedBy = zzz.ModifiedBy,
                                                    ModifiedDate = zzz.ModifiedDate,
                                                    SubmitBy = zzz.SubmitBy,
                                                    SubmitDate = zzz.SubmitDate
                                                }).First();
                lblName.Text = resultFirstPatientDetail.PatientName;


                lblSexId.Text = resultFirstPatientDetail.Gender;
                if (lblSexId.Text == "M")
                    lblSexId.Style.Add("color", "#2A3593");
                else
                    lblSexId.Style.Add("color", "#FF00D5");

                lblAdmissionNoDetail.Text = resultFirstPatientDetail.AdmissionNo;
                lblMrNo.Text = resultFirstPatientDetail.LocalMrNo;
                lblWorklistDate.Text = resultFirstPatientDetail.WorklistDate.ToString("dd-MMM-yyyy");
                lblWorklistTime.Text = resultFirstPatientDetail.WorklistDate.ToString("HH:mm");
                lblRoom.Text = resultFirstPatientDetail.RoomNo;
                lblPayerName.Text = resultFirstPatientDetail.PayerName;
                lblAge.Text = resultFirstPatientDetail.Age;
                lblDoB.Text = resultFirstPatientDetail.BirthDate.ToString("dd MMM yyyy");
                lblMobileNo.Text = resultFirstPatientDetail.MobileNo == null || resultFirstPatientDetail.MobileNo == "" ? "-" : resultFirstPatientDetail.MobileNo;
                //lblMobileNo.Text = "089638986866";
                lblPrimaryDoctorName.Text = resultFirstPatientDetail.DoctorName;
                lblRetur.Text = resultFirstPatientDetail.IsRetur == true ? "Retur Obat" : "";
                lblPrescription.Text = resultFirstPatientDetail.IsPrescription == true ? "Resep Pulang" : "";
                if (resultFirstPatientDetail.WaitStatus == "TD")
                {
                    if (resultFirstPatientDetail.VisitValue == "DONE")
                    {
                        imgWaitStatus.ImageUrl = "~/Images/Icon/icon_checked_locked.svg";
                        lblWaitStatus.Text = "Tunggu dokter";
                        lblRemarks.Visible = false;
                    }
                    else
                    {
                        imgWaitStatus.ImageUrl = "~/Images/Icon/icon_unchecked.svg";
                        lblWaitStatus.Text = "Tunggu dokter";
                        lblRemarks.Visible = false;
                    }
                }
                else if (resultFirstPatientDetail.WaitStatus == "TUTD")
                {
                    imgWaitStatus.ImageUrl = "~/Images/Icon/icon_uncheckedcross.svg";
                    lblWaitStatus.Text = "Tidak tunggu dokter";
                    lblRemarks.Visible = false;
                }
                else if (resultFirstPatientDetail.WaitStatus == "TU")
                {
                    if (resultFirstPatientDetail.VisitValue == "DONE")
                    {
                        imgWaitStatus.ImageUrl = "~/Images/Icon/icon_checked_locked.svg";
                        lblWaitStatus.Text = "Tidak tunggu dokter";
                        lblRemarks.Visible = true;
                    }
                    else
                    {
                        imgWaitStatus.ImageUrl = "~/Images/Icon/icon_unchecked.svg";
                        lblWaitStatus.Text = "Tidak tunggu dokter";
                        lblRemarks.Visible = true;
                    }

                }
                lblRemarks.Text = "</br>tunggu hasil " + resultFirstPatientDetail.Remarks;

                if (resultFirstPatientDetail.IsPrescription == true)
                {
                    if (resultFirstPatientDetail.PrescriptionValue == "DONE")
                    {
                        imgPrescription.ImageUrl = "~/Images/Icon/icon_checked_locked.svg";
                        lblPrescription.Text = "Resep pulang";
                    }
                    else if (resultFirstPatientDetail.PrescriptionValue == "PENDING")
                    {
                        imgPrescription.ImageUrl = "~/Images/Icon/icon_unchecked.svg";
                        lblPrescription.Text = "Resep pulang";
                    }
                }
                else
                {
                    imgPrescription.ImageUrl = "~/Images/Icon/icon_uncheckedcross.svg";
                    lblPrescription.Text = "Tidak ada resep pulang";
                }

                if (resultFirstPatientDetail.IsRetur == true)
                {
                    if (resultFirstPatientDetail.ReturValue == "DONE")
                    {
                        imgRetur.ImageUrl = "~/Images/Icon/icon_checked_locked.svg";
                        lblRetur.Text = "Retur obat";
                    }
                    else if (resultFirstPatientDetail.ReturValue == "PENDING")
                    {
                        imgRetur.ImageUrl = "~/Images/Icon/icon_unchecked.svg";
                        lblRetur.Text = "Retur obat";
                    }
                }
                else
                {
                    imgRetur.ImageUrl = "~/Images/Icon/icon_uncheckedcross.svg";
                    lblRetur.Text = "Tidak ada retur obat";
                }

                lblAdditionalNotes.Text = resultFirstPatientDetail.AdditionalNotes.Replace("\n", "<br />");
                lblCreatedBy.Text = resultFirstPatientDetail.CreatedBy;
                lblCreatedDate.Text = resultFirstPatientDetail.CreatedDate.ToString("dd/MM/yyy HH:mm");

                if (resultFirstPatientDetail.CreatedDate.ToString("yyyy-MM-dd HH:mm:ss") == "0001-01-01 00:00:00")
                {
                    lblCreatedDate.Visible = false;
                }

                lblUpdatedBy.Text = resultFirstPatientDetail.ModifiedBy;
                lblUpdatedDate.Text = resultFirstPatientDetail.ModifiedDate.ToString("dd/MM/yyy HH:mm");

                if (resultFirstPatientDetail.ModifiedDate.ToString("yyyy-MM-dd HH:mm:ss") == "0001-01-01 00:00:00")
                {
                    lblUpdatedDate.Visible = false;
                }

                lblSubmitBy.Text = resultFirstPatientDetail.SubmitBy;
                lblSubmitDate.Text = resultFirstPatientDetail.SubmitDate.ToString("dd/MM/yyy HH:mm");

                if (resultFirstPatientDetail.SubmitDate.ToString("yyyy-MM-dd HH:mm:ss") == "0001-01-01 00:00:00")
                {
                    lblSubmitDate.Visible = false;
                }

                DataTable dtDoctorConsultation = new DataTable();
                var resultPatientDetailConsultation = (from zzz in getTotalpatientDetail.list
                                                       where zzz.IsPrimary == false
                                                       select new DischargeDetail
                                                       {
                                                           DoctorName = zzz.DoctorName,
                                                           WaitStatus = zzz.WaitStatus,
                                                           Remarks = zzz.Remarks,
                                                           IsPrescription = zzz.IsPrescription,
                                                           PrescriptionValue = zzz.PrescriptionValue,
                                                           IsRetur = zzz.IsRetur,
                                                           ReturValue = zzz.ReturValue,
                                                           WorklistId = zzz.WorklistId,
                                                           VisitValue = zzz.VisitValue,
                                                           IsDoctorVisit = zzz.IsDoctorVisit
                                                       }).ToList();

                if (resultPatientDetailConsultation.Count > 0)
                {
                    llblNoKonsulen.Visible = false;
                    dtDoctorConsultation = Helper.ToDataTable(resultPatientDetailConsultation);
                    repeaterDoctorContultation.DataSource = dtDoctorConsultation;
                    repeaterDoctorContultation.DataBind();
                }
                else
                {
                    llblNoKonsulen.Visible = true;
                    repeaterDoctorContultation.DataSource = null;
                    repeaterDoctorContultation.DataBind();
                }

                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalDetail();", true);

            }
            else
            {
                ShowToastr("Data tidak ditemukan.", "error", "error");
            }
            log.Info(LogLibrary.Logging("E", "LnkPatientDetailPlan_onClick", hfName.Value, "Finish LnkPatientDetailPlan_onClick"));
        }
        catch (Exception ex)
        {
            ShowToastr("Terjadi kesalahan pada sistem. Mohon hubungi staf IT di rumah sakit Anda.", "error", "error");
            log.Error(LogLibrary.Error("LnkPatientDetailPlan_onClick", hfName.Value, ex.Message));
        }
    }

    //================================== DISCHARGE PLAN LATE =================================

    protected void LnkPatientDetailPlanLate_onClick(object sender, EventArgs e)
    {
        try
        {
            log.Info(LogLibrary.Logging("S", "LnkPatientDetailPlanLate_onClick", hfName.Value, ""));

            int selRowIndex = ((GridViewRow)(((LinkButton)sender).Parent.Parent)).RowIndex;
            int repeaterindex = ((RepeaterItem)(((LinkButton)sender).Parent.Parent.Parent.Parent.NamingContainer)).ItemIndex;

            RepeaterItemCollection ri = rptDischargePlan.Items;

            var gvLIstDischargeProcess = (GridView)ri[repeaterindex].FindControl("gvLIstAkanDatang");

            HiddenField worklistId = (HiddenField)gvLIstDischargeProcess.Rows[selRowIndex].FindControl("lbWorklistId");
            HiddenField admissionId = (HiddenField)gvLIstDischargeProcess.Rows[selRowIndex].FindControl("lbAdmissionId");
            HiddenField ModifiedDate = (HiddenField)gvLIstDischargeProcess.Rows[selRowIndex].FindControl("ModifiedDate");

            log.Debug(LogLibrary.Logging("S", "GetPatientDetail", hfName.Value, ""));
            var patientDetail = clsWorklist.GetPatientDetail(Int64.Parse(hfOrganizationId.Value), Guid.Parse(worklistId.Value));
            ListDischargeDetail getTotalpatientDetail = JsonConvert.DeserializeObject<ListDischargeDetail>(patientDetail.Result.ToString());
            log.Debug(LogLibrary.Logging("E", "GetPatientDetail", hfName.Value, getTotalpatientDetail.ToString()));

            if (getTotalpatientDetail != null)
            {
                var resultFirstPatientDetail = (from zzz in getTotalpatientDetail.list
                                                where zzz.IsPrimary == true
                                                select new DischargeDetail
                                                {
                                                    PatientName = zzz.PatientName,
                                                    Gender = zzz.Gender,
                                                    LocalMrNo = zzz.LocalMrNo,
                                                    WorklistDate = zzz.WorklistDate,
                                                    RoomNo = zzz.RoomNo,
                                                    Age = zzz.Age,
                                                    BirthDate = zzz.BirthDate,
                                                    PayerName = zzz.PayerName,
                                                    MobileNo = zzz.MobileNo,
                                                    AdditionalNotes = zzz.AdditionalNotes,
                                                    DoctorName = zzz.DoctorName,
                                                    WaitStatus = zzz.WaitStatus,
                                                    Remarks = zzz.Remarks,
                                                    IsPrescription = zzz.IsPrescription,
                                                    PrescriptionValue = zzz.PrescriptionValue,
                                                    IsRetur = zzz.IsRetur,
                                                    ReturValue = zzz.ReturValue,
                                                    WorklistId = zzz.WorklistId,
                                                    VisitValue = zzz.VisitValue,
                                                    AdmissionNo = zzz.AdmissionNo,
                                                    IsDoctorVisit = zzz.IsDoctorVisit,
                                                    CreatedBy = zzz.CreatedBy,
                                                    CreatedDate = zzz.CreatedDate,
                                                    ModifiedBy = zzz.ModifiedBy,
                                                    ModifiedDate = zzz.ModifiedDate,
                                                    SubmitBy = zzz.SubmitBy,
                                                    SubmitDate = zzz.SubmitDate
                                                }).First();
                lblName.Text = resultFirstPatientDetail.PatientName;


                lblSexId.Text = resultFirstPatientDetail.Gender;
                if (lblSexId.Text == "M")
                    lblSexId.Style.Add("color", "#2A3593");
                else
                    lblSexId.Style.Add("color", "#FF00D5");

                lblAdmissionNoDetail.Text = resultFirstPatientDetail.AdmissionNo;
                lblMrNo.Text = resultFirstPatientDetail.LocalMrNo;
                lblWorklistDate.Text = resultFirstPatientDetail.WorklistDate.ToString("dd-MMM-yyyy");
                lblRoom.Text = resultFirstPatientDetail.RoomNo;
                lblPayerName.Text = resultFirstPatientDetail.PayerName;
                lblAge.Text = resultFirstPatientDetail.Age;
                lblDoB.Text = resultFirstPatientDetail.BirthDate.ToString("dd MMM yyyy");
                lblMobileNo.Text = resultFirstPatientDetail.MobileNo == null || resultFirstPatientDetail.MobileNo == "" ? "-" : resultFirstPatientDetail.MobileNo;
                //lblMobileNo.Text = "089638986866";
                lblPrimaryDoctorName.Text = resultFirstPatientDetail.DoctorName;
                lblRetur.Text = resultFirstPatientDetail.IsRetur == true ? "Retur Obat" : "";
                lblPrescription.Text = resultFirstPatientDetail.IsPrescription == true ? "Resep Pulang" : "";
                if (resultFirstPatientDetail.WaitStatus == "TD")
                {
                    if (resultFirstPatientDetail.VisitValue == "DONE")
                    {
                        imgWaitStatus.ImageUrl = "~/Images/Icon/icon_checked_locked.svg";
                        lblWaitStatus.Text = "Tunggu dokter";
                        lblRemarks.Visible = false;
                    }
                    else
                    {
                        imgWaitStatus.ImageUrl = "~/Images/Icon/icon_unchecked.svg";
                        lblWaitStatus.Text = "Tunggu dokter";
                        lblRemarks.Visible = false;
                    }
                }
                else if (resultFirstPatientDetail.WaitStatus == "TUTD")
                {
                    imgWaitStatus.ImageUrl = "~/Images/Icon/icon_uncheckedcross.svg";
                    lblWaitStatus.Text = "Tidak tunggu dokter";
                    lblRemarks.Visible = false;
                }
                else if (resultFirstPatientDetail.WaitStatus == "TU")
                {
                    if (resultFirstPatientDetail.VisitValue == "DONE")
                    {
                        imgWaitStatus.ImageUrl = "~/Images/Icon/icon_checked_locked.svg";
                        lblWaitStatus.Text = "Tidak tunggu dokter";
                        lblRemarks.Visible = true;
                    }
                    else
                    {
                        imgWaitStatus.ImageUrl = "~/Images/Icon/icon_unchecked.svg";
                        lblWaitStatus.Text = "Tidak tunggu dokter";
                        lblRemarks.Visible = true;
                    }

                }
                lblRemarks.Text = "</br>tunggu hasil " + resultFirstPatientDetail.Remarks;

                if (resultFirstPatientDetail.IsPrescription == true)
                {
                    if (resultFirstPatientDetail.PrescriptionValue == "DONE")
                    {
                        imgPrescription.ImageUrl = "~/Images/Icon/icon_checked_locked.svg";
                        lblPrescription.Text = "Resep pulang";
                    }
                    else if (resultFirstPatientDetail.PrescriptionValue == "PENDING")
                    {
                        imgPrescription.ImageUrl = "~/Images/Icon/icon_unchecked.svg";
                        lblPrescription.Text = "Resep pulang";
                    }
                }
                else
                {
                    imgPrescription.ImageUrl = "~/Images/Icon/icon_uncheckedcross.svg";
                    lblPrescription.Text = "Tidak ada resep pulang";
                }

                if (resultFirstPatientDetail.IsRetur == true)
                {
                    if (resultFirstPatientDetail.ReturValue == "DONE")
                    {
                        imgRetur.ImageUrl = "~/Images/Icon/icon_checked_locked.svg";
                        lblRetur.Text = "Retur obat";
                    }
                    else if (resultFirstPatientDetail.ReturValue == "PENDING")
                    {
                        imgRetur.ImageUrl = "~/Images/Icon/icon_unchecked.svg";
                        lblRetur.Text = "Retur obat";
                    }
                }
                else
                {
                    imgRetur.ImageUrl = "~/Images/Icon/icon_uncheckedcross.svg";
                    lblRetur.Text = "Tidak ada retur obat";
                }

                lblAdditionalNotes.Text = resultFirstPatientDetail.AdditionalNotes.Replace("\n", "<br />");
                lblCreatedBy.Text = resultFirstPatientDetail.CreatedBy;
                lblCreatedDate.Text = resultFirstPatientDetail.CreatedDate.ToString("dd/MM/yyy HH:mm");

                if (resultFirstPatientDetail.CreatedDate.ToString("yyyy-MM-dd HH:mm:ss") == "0001-01-01 00:00:00")
                {
                    lblCreatedDate.Visible = false;
                }

                lblUpdatedBy.Text = resultFirstPatientDetail.ModifiedBy;
                lblUpdatedDate.Text = resultFirstPatientDetail.ModifiedDate.ToString("dd/MM/yyy HH:mm");

                if (resultFirstPatientDetail.ModifiedDate.ToString("yyyy-MM-dd HH:mm:ss") == "0001-01-01 00:00:00")
                {
                    lblUpdatedDate.Visible = false;
                }

                lblSubmitBy.Text = resultFirstPatientDetail.SubmitBy;
                lblSubmitDate.Text = resultFirstPatientDetail.SubmitDate.ToString("dd/MM/yyy HH:mm");

                if (resultFirstPatientDetail.SubmitDate.ToString("yyyy-MM-dd HH:mm:ss") == "0001-01-01 00:00:00")
                {
                    lblSubmitDate.Visible = false;
                }

                DataTable dtDoctorConsultation = new DataTable();
                var resultPatientDetailConsultation = (from zzz in getTotalpatientDetail.list
                                                       where zzz.IsPrimary == false
                                                       select new DischargeDetail
                                                       {
                                                           DoctorName = zzz.DoctorName,
                                                           WaitStatus = zzz.WaitStatus,
                                                           Remarks = zzz.Remarks,
                                                           IsPrescription = zzz.IsPrescription,
                                                           PrescriptionValue = zzz.PrescriptionValue,
                                                           IsRetur = zzz.IsRetur,
                                                           ReturValue = zzz.ReturValue,
                                                           WorklistId = zzz.WorklistId,
                                                           VisitValue = zzz.VisitValue,
                                                           IsDoctorVisit = zzz.IsDoctorVisit
                                                       }).ToList();

                if (resultPatientDetailConsultation.Count > 0)
                {
                    llblNoKonsulen.Visible = false;
                    dtDoctorConsultation = Helper.ToDataTable(resultPatientDetailConsultation);
                    repeaterDoctorContultation.DataSource = dtDoctorConsultation;
                    repeaterDoctorContultation.DataBind();
                }
                else
                {
                    llblNoKonsulen.Visible = true;
                    repeaterDoctorContultation.DataSource = null;
                    repeaterDoctorContultation.DataBind();
                }

                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModalDetail();", true);
            }
            else
            {
                ShowToastr("Data Tidak Ditemukan", "info", "info");
            }
            log.Info(LogLibrary.Logging("E", "LnkPatientDetailPlanLate_onClick", hfName.Value, "Finish LnkPatientDetailPlanLate_onClick"));
        }
        catch (Exception ex)
        {
            log.Error(LogLibrary.Error("LnkPatientDetailPlanLate_onClick", hfName.Value, ex.Message));
        }
    }

    protected void labResult_Click(object sender, EventArgs e)
    {
        try
        {
            log.Info(LogLibrary.Logging("S", "labResult_Click", hfName.Value, ""));

            int selRowIndex = ((GridViewRow)(((ImageButton)sender).Parent.Parent)).RowIndex;
            int repeaterindex = ((RepeaterItem)(((ImageButton)sender).Parent.Parent.Parent.Parent.NamingContainer)).ItemIndex;

            RepeaterItemCollection ri = rptDischargePlan.Items;

            var gvLIstNewDischarge = (GridView)ri[repeaterindex].FindControl("gvLIstNewDischarge");

            HiddenField worklistId = (HiddenField)gvLIstNewDischarge.Rows[selRowIndex].FindControl("lbWorklistId");
            Guid worklistId1 = Guid.Parse(worklistId.Value);
            HiddenField patientId = (HiddenField)gvLIstNewDischarge.Rows[selRowIndex].FindControl("lbPatientId");
            string patientId1 = patientId.Value.ToString();

            var patientHeader = clsWorklist.GetPatientHeader(long.Parse(patientId1), worklistId1);
            var getPatientHeader = JsonConvert.DeserializeObject<ListPatientHeader>(patientHeader.Result.ToString());
            var getFirstPatientHeader = (from a in getPatientHeader.list
                                         select new PatientHeaderIpd
                                         {
                                             WorklistId = a.WorklistId,
                                             PatientName = a.PatientName,
                                             MrNo = a.MrNo,
                                             DoctorName = a.DoctorName,
                                             AdmissionNo = a.AdmissionNo,
                                             BirthDate = a.BirthDate,
                                             Age = a.Age,
                                             Gender = a.Gender,
                                             PayerName = a.PayerName,
                                             Religion = a.Religion
                                         }).FirstOrDefault();

            initializevalueHeader(getFirstPatientHeader);

            HiddenField admissionId = (HiddenField)gvLIstNewDischarge.Rows[selRowIndex].FindControl("lbAdmissionId");
            List<LaboratoryResult> listlaboratory = new List<LaboratoryResult>();
            var dataLaboratory = clsWorklist.getLaboratoryResult(admissionId.Value.ToString());
            var JsonLaboratory = JsonConvert.DeserializeObject<ResultLaboratoryResult>(dataLaboratory.Result.ToString());
            listlaboratory = new List<LaboratoryResult>();
            listlaboratory = JsonLaboratory.list;

            initializevalue(listlaboratory);

            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openLabModal();", true);
            log.Info(LogLibrary.Logging("E", "labResult_Click", hfName.Value, "Finish labResult_Click"));

        }
        catch (Exception ex)
        {
            ShowToastr("Terjadi kesalahan pada sistem. Mohon hubungi staf IT di rumah sakit Anda.", "error", "error");
            log.Error(LogLibrary.Error("labResult_Click", hfName.Value, ex.Message));
        }
    }

    protected void labResultLate_Click(object sender, EventArgs e)
    {
        try
        {
            log.Info(LogLibrary.Logging("S", "labResultLate_Click", hfName.Value, ""));

            int selRowIndex = ((GridViewRow)(((ImageButton)sender).Parent.Parent)).RowIndex;
            int repeaterindex = ((RepeaterItem)(((ImageButton)sender).Parent.Parent.Parent.Parent.NamingContainer)).ItemIndex;

            RepeaterItemCollection ri = rptDischargePlan.Items;

            var gvLIstNewDischarge = (GridView)ri[repeaterindex].FindControl("gvLIstAkanDatang");

            HiddenField worklistId = (HiddenField)gvLIstNewDischarge.Rows[selRowIndex].FindControl("lbWorklistId");
            Guid worklistId1 = Guid.Parse(worklistId.Value);
            HiddenField patientId = (HiddenField)gvLIstNewDischarge.Rows[selRowIndex].FindControl("lbPatientId");
            string patientId1 = patientId.Value.ToString();

            var patientHeader = clsWorklist.GetPatientHeader(long.Parse(patientId1), worklistId1);
            var getPatientHeader = JsonConvert.DeserializeObject<ListPatientHeader>(patientHeader.Result.ToString());

            var getFirstPatientHeader = (from a in getPatientHeader.list
                                         select new PatientHeaderIpd
                                         {
                                             WorklistId = a.WorklistId,
                                             PatientName = a.PatientName,
                                             MrNo = a.MrNo,
                                             DoctorName = a.DoctorName,
                                             AdmissionNo = a.AdmissionNo,
                                             BirthDate = a.BirthDate,
                                             Age = a.Age,
                                             Gender = a.Gender,
                                             PayerName = a.PayerName,
                                             Religion = a.Religion
                                         }).FirstOrDefault();

            initializevalueHeader(getFirstPatientHeader);

            HiddenField admissionId = (HiddenField)gvLIstNewDischarge.Rows[selRowIndex].FindControl("lbAdmissionId");
            List<LaboratoryResult> listlaboratory = new List<LaboratoryResult>();
            var dataLaboratory = clsWorklist.getLaboratoryResult(admissionId.Value.ToString());
            var JsonLaboratory = JsonConvert.DeserializeObject<ResultLaboratoryResult>(dataLaboratory.Result.ToString());
            listlaboratory = new List<LaboratoryResult>();
            listlaboratory = JsonLaboratory.list;

            initializevalue(listlaboratory);

            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openLabModal();", true);
            log.Info(LogLibrary.Logging("E", "labResultLate_Click", hfName.Value, "Finish labResultLate_Click"));
        }
        catch (Exception ex)
        {
            log.Error(LogLibrary.Error("labResultLate_Click", hfName.Value, ex.Message));
        }
    }

    protected void gvLIstNewDischarge_DataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow && !((e.Row.RowState == (DataControlRowState.Edit | DataControlRowState.Alternate)) || (e.Row.RowState == DataControlRowState.Edit)))
        {
            HiddenField hdnWorklistId = (HiddenField)e.Row.FindControl("lbWorklistId");
            if (Session[Helper.CommonColorGrid] == null)
            {
                Common temp = new Common();
                temp.WorklistId = hdnWorklistId.Value;
                temp.color = "#F2F3F4";
                Session[Helper.CommonColorGrid] = temp;
                e.Row.BackColor = System.Drawing.ColorTranslator.FromHtml(temp.color);
            }
            else
            {
                Common temp = (Common)Session[Helper.CommonColorGrid];
                if (temp.WorklistId == hdnWorklistId.Value)
                {
                    e.Row.BackColor = System.Drawing.ColorTranslator.FromHtml(temp.color);
                }
                else
                {
                    if (temp.color == "#ffffff")
                    {
                        e.Row.BackColor = System.Drawing.ColorTranslator.FromHtml("#F2F3F4");
                        temp.WorklistId = hdnWorklistId.Value;
                        temp.color = "#F2F3F4";
                        Session[Helper.CommonColorGrid] = temp;
                    }
                    else
                    {
                        e.Row.BackColor = System.Drawing.ColorTranslator.FromHtml("#ffffff");
                        temp.WorklistId = hdnWorklistId.Value;
                        temp.color = "#ffffff";
                        Session[Helper.CommonColorGrid] = temp;
                    }

                }
            }
        }

        if (e.Row.Cells[0].Text != "")
        {
            e.Row.BackColor = System.Drawing.Color.White;
            e.Row.ForeColor = System.Drawing.ColorTranslator.FromHtml("#A5A5A5");
        }

    }

    protected void gvLIstDischargeProcess_DataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow && !((e.Row.RowState == (DataControlRowState.Edit | DataControlRowState.Alternate)) || (e.Row.RowState == DataControlRowState.Edit)))
        {
            HiddenField hdnWorklistId = (HiddenField)e.Row.FindControl("lbWorklistId");
            HiddenField hfFinalDate = (HiddenField)e.Row.FindControl("hfFinalDate");

            //System.Web.UI.HtmlControls.HtmlGenericControl divred = (System.Web.UI.HtmlControls.HtmlGenericControl)e.Row.FindControl("divred");

            Label payerName = (Label)e.Row.FindControl("payerName");
            string finaldate = DateTime.Parse(hfFinalDate.Value).ToString("dd MMM yyyy HH:mm");
            string todaydate = DateTime.Now.ToString("dd MMM yyyy HH:mm");

            TimeSpan datediff = DateTime.Parse(todaydate).Subtract(DateTime.Parse(finaldate));
            double minutelate = datediff.TotalMinutes;


            log.Debug(LogLibrary.Logging("S", "GetOrganizationSetting", hfName.Value, ""));
            var orgsetting = clsOrganizationSetting.GetOrganizationSetting(Int64.Parse(hfOrganizationId.Value));
            var jsonorgsetting = JsonConvert.DeserializeObject<ResultViewOrganizationSetting>(orgsetting.Result.ToString());
            log.Debug(LogLibrary.Logging("E", "GetOrganizationSetting", hfName.Value, jsonorgsetting.ToString()));


            int settinglateprivate = int.Parse(jsonorgsetting.list.Find(y => y.setting_name == "MONITOR_PRIVATE").setting_value);
            int settinglatepayer = int.Parse(jsonorgsetting.list.Find(y => y.setting_name == "MONITOR_PAYER").setting_value);
            if (payerName.Text.ToLower() == "private" && minutelate > settinglateprivate && finaldate != "01 Jan 0001 00:00")
            {
                e.Row.BackColor = System.Drawing.ColorTranslator.FromHtml("#FFE5E5");
                //divred.Style.Add("border-left", "solid 1px red");
            }
            else if (payerName.Text.ToLower() != "private" && minutelate > settinglatepayer && finaldate != "01 Jan 0001 00:00")
            {
                e.Row.BackColor = System.Drawing.ColorTranslator.FromHtml("#FFE5E5");
                //divred.Style.Add("border-left", "solid 1px red");
            }
            else
            {
                if (Session[Helper.CommonColorGrid] == null)
                {
                    Common temp = new Common();
                    temp.WorklistId = hdnWorklistId.Value;
                    temp.color = "#F2F3F4";
                    Session[Helper.CommonColorGrid] = temp;
                    e.Row.BackColor = System.Drawing.ColorTranslator.FromHtml(temp.color);
                }
                else
                {
                    Common temp = (Common)Session[Helper.CommonColorGrid];
                    if (temp.WorklistId == hdnWorklistId.Value)
                    {
                        e.Row.BackColor = System.Drawing.ColorTranslator.FromHtml(temp.color);
                    }
                    else
                    {
                        if (temp.color == "#ffffff")
                        {
                            e.Row.BackColor = System.Drawing.ColorTranslator.FromHtml("#F2F3F4");
                            temp.WorklistId = hdnWorklistId.Value;
                            temp.color = "#F2F3F4";
                            Session[Helper.CommonColorGrid] = temp;
                        }
                        else
                        {
                            e.Row.BackColor = System.Drawing.ColorTranslator.FromHtml("#ffffff");
                            temp.WorklistId = hdnWorklistId.Value;
                            temp.color = "#ffffff";
                            Session[Helper.CommonColorGrid] = temp;
                        }

                    }
                }
            }
        }

        if (e.Row.Cells[0].Text != "")
        {
            e.Row.BackColor = System.Drawing.Color.White;
            e.Row.ForeColor = System.Drawing.ColorTranslator.FromHtml("#A5A5A5");
        }
    }

    protected void gvLIstDischargeDone_DataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow && !((e.Row.RowState == (DataControlRowState.Edit | DataControlRowState.Alternate)) || (e.Row.RowState == DataControlRowState.Edit)))
        {
            HiddenField hdnWorklistId = (HiddenField)e.Row.FindControl("lbWorklistId");
            if (Session[Helper.CommonColorGrid] == null)
            {
                Common temp = new Common();
                temp.WorklistId = hdnWorklistId.Value;
                temp.color = "#F2F3F4";
                Session[Helper.CommonColorGrid] = temp;
                e.Row.BackColor = System.Drawing.ColorTranslator.FromHtml(temp.color);
            }
            else
            {
                Common temp = (Common)Session[Helper.CommonColorGrid];
                if (temp.WorklistId == hdnWorklistId.Value)
                {
                    e.Row.BackColor = System.Drawing.ColorTranslator.FromHtml(temp.color);
                }
                else
                {
                    if (temp.color == "#ffffff")
                    {
                        e.Row.BackColor = System.Drawing.ColorTranslator.FromHtml("#F2F3F4");
                        temp.WorklistId = hdnWorklistId.Value;
                        temp.color = "#F2F3F4";
                        Session[Helper.CommonColorGrid] = temp;
                    }
                    else
                    {
                        e.Row.BackColor = System.Drawing.ColorTranslator.FromHtml("#ffffff");
                        temp.WorklistId = hdnWorklistId.Value;
                        temp.color = "#ffffff";
                        Session[Helper.CommonColorGrid] = temp;
                    }
                }
            }
        }
    }

    protected void DDLWardList_onSelectedIndexChanged(object sender, EventArgs e)
    {
        getListPatient();
        getCountPatient();
    }

    protected void DDLWardListDone_onSelectedIndexChanged(object sender, EventArgs e)
    {
        GetWorklistDone();
        getCountPatient();
    }

    protected void DDLWardListProcess_onSelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            log.Info(LogLibrary.Logging("S", "GetWorklistProcess", hfName.Value, ""));

            int lateduration;
            int latedurationinsurance;
            int latedurationinvoice;
            int latedurationtotalprivate;
            int latedurationtotalpayer;

            log.Debug(LogLibrary.Logging("S", "GetOrganizationSetting", hfName.Value, ""));
            var orgsetting = clsOrganizationSetting.GetOrganizationSetting(Int64.Parse(hfOrganizationId.Value));
            var jsonorgsetting = JsonConvert.DeserializeObject<ResultViewOrganizationSetting>(orgsetting.Result.ToString());
            log.Debug(LogLibrary.Logging("E", "GetOrganizationSetting", hfName.Value, jsonorgsetting.ToString()));

            if (jsonorgsetting.list.Count() > 0)
            {
                lateduration = int.Parse(jsonorgsetting.list.Find(y => y.setting_name == "LATE_SUBDISCHARGE").setting_value);
                latedurationinsurance = int.Parse(jsonorgsetting.list.Find(y => y.setting_name == "LATE_INSURANCE").setting_value);
                latedurationinvoice = int.Parse(jsonorgsetting.list.Find(y => y.setting_name == "LATE_INVOICE").setting_value);
                latedurationtotalprivate = int.Parse(jsonorgsetting.list.Find(y => y.setting_name == "LATE_TOTALPRIVATE").setting_value);
                latedurationtotalpayer = int.Parse(jsonorgsetting.list.Find(y => y.setting_name == "LATE_TOTALPAYER").setting_value);
            }
            else
            {
                lateduration = 20; //warna merah untuk service dan item
                latedurationinsurance = 20; //warna merah untuk email dan confirm
                latedurationinvoice = 20; //warna merah invoice selesai
                latedurationtotalprivate = 30; //warna merah untuk lama kerja pasien private
                latedurationtotalpayer = 120; //warna merah untuk lama kerja pasien with payer
            }

            lblDateRefreshProcess.Text = DateTime.Now.ToString("dd MMM yyyy, HH:mm");
            List<ViewDischargeRequest> viewrequest = new List<ViewDischargeRequest>();
            long wardId = 0;
            if (ddlWardListProcess.SelectedValue != "")
                wardId = long.Parse(ddlWardListProcess.SelectedValue);

            var dischargeprocess = clsWorklist.getListDischargeProcess(Int64.Parse(hfOrganizationId.Value), DateTime.Now.ToString("yyyy-MM-dd"), wardId, txtsearchprocess.Text, 2);
            var jsondischargeprocess = JsonConvert.DeserializeObject<ResultListDischargeProcess>(dischargeprocess.Result.ToString());
            if (jsondischargeprocess.list.dischargerequests != null)
            {
                if (jsondischargeprocess.list.dischargerequests.Where(y => y.ArInvoiceId == 0).Count() > 0)
                {
                    List<DischargeRequest> tempview = new List<DischargeRequest>();
                    if (txtdateprocess.Text != "")
                        tempview = jsondischargeprocess.list.dischargerequests.Where(y => y.ArInvoiceId == 0 && y.WorklistDate.ToString("dd MMM yyyy") == DateTime.Parse(txtdateprocess.Text).ToString("dd MMM yyyy")).ToList();
                    else
                        tempview = jsondischargeprocess.list.dischargerequests.Where(y => y.ArInvoiceId == 0).ToList();

                    lblCountProcess.Text = " (" + jsondischargeprocess.list.dischargerequests.Where(y => y.ArInvoiceId == 0 && y.IsPrimary == true).Count().ToString() + ") ";
                    foreach (var x in tempview)
                    {

                        if (x.IsPrimary == true)
                        {
                            ViewDischargeRequest tempviewrequest = new ViewDischargeRequest();
                            tempviewrequest.SubmitDate = x.SubmitDate;
                            tempviewrequest.isShowDate = x.isShowDate;
                            tempviewrequest.WorklistDate = x.WorklistDate;
                            tempviewrequest.WorklistId = x.WorklistId;
                            tempviewrequest.ProcessId = x.ProcessId;
                            tempviewrequest.AdmissionId = x.AdmissionId;
                            tempviewrequest.AdmissionNo = x.AdmissionNo;
                            tempviewrequest.PatientId = x.PatientId;
                            tempviewrequest.PatientName = x.PatientName;
                            tempviewrequest.WardId = x.WardId;
                            tempviewrequest.islate = x.islate;
                            if (x.PayerName.Length > 50)
                                tempviewrequest.PayerName = x.PayerName.Substring(0, 50) + " ...";
                            else
                                tempviewrequest.PayerName = x.PayerName;
                            tempviewrequest.DoctorId = x.DoctorId;
                            tempviewrequest.DoctorName = x.DoctorName;
                            tempviewrequest.AdditionalNotes = x.AdditionalNotes;
                            tempviewrequest.ArInvoiceId = x.ArInvoiceId;
                            tempviewrequest.IsPrimary = x.IsPrimary;
                            tempviewrequest.IsPrescription = x.IsPrescription;
                            tempviewrequest.IsRetur = x.IsRetur;
                            tempviewrequest.EmailDate = x.EmailDate;
                            tempviewrequest.ConfirmDate = x.ConfirmDate;
                            tempviewrequest.InvoiceDate = x.InvoiceDate;
                            tempviewrequest.FUPatient = x.FUPatient;
                            tempviewrequest.OPDControl = x.OPDControl;
                            tempviewrequest.localMrNo = x.LocalMrNo;
                            tempviewrequest.birthDate = x.BirthDate.ToString("dd MMM yyyy");
                            tempviewrequest.roomNo = x.RoomNo;
                            tempviewrequest.FlagDischarged = x.FlagDischarged;
                            tempviewrequest.ModifiedDate = x.ModifiedDate;

                            if (jsondischargeprocess.list.subdischarges.Where(y => y.WorklistId == x.WorklistId && y.SubDischargeTypeId == 2).Count() > 0)
                            {
                                tempviewrequest.SubDateBed = jsondischargeprocess.list.subdischarges.Find(y => y.WorklistId == x.WorklistId && y.SubDischargeTypeId == 2).SubDate;
                            }
                            else
                            {
                                tempviewrequest.SubDateBed = DateTime.Parse("01/01/0001 00:00:00").ToString("dd/MM/yyyy HH:mm:ss");
                            }

                            if (jsondischargeprocess.list.subdischarges.Where(y => y.WorklistId == x.WorklistId && y.SubDischargeTypeId == 4).Count() > 0)
                            {
                                tempviewrequest.SubDateService = jsondischargeprocess.list.subdischarges.Find(y => y.WorklistId == x.WorklistId && y.SubDischargeTypeId == 4).SubDate;
                            }
                            else
                            {
                                tempviewrequest.SubDateService = DateTime.Parse("01/01/0001 00:00:00").ToString("dd/MM/yyyy HH:mm:ss");
                            }

                            if (jsondischargeprocess.list.subdischarges.Where(y => y.WorklistId == x.WorklistId && y.SubDischargeTypeId == 8).Count() > 0)
                            {
                                tempviewrequest.SubDateItem = jsondischargeprocess.list.subdischarges.Find(y => y.WorklistId == x.WorklistId && y.SubDischargeTypeId == 8).SubDate;
                                tempviewrequest.IsNeedPrescription = true;
                            }
                            else
                            {
                                tempviewrequest.SubDateItem = DateTime.Parse("01/01/0001 00:00:00").ToString("dd/MM/yyyy HH:mm:ss");
                                tempviewrequest.IsNeedPrescription = true;
                            }


                            //if (jsondischargeprocess.list.dischargerequests.Where(y => y.WorklistId == x.WorklistId).Count() > 0)
                            //{
                            //    if (jsondischargeprocess.list.subdischarges.Where(y => y.WorklistId == x.WorklistId && y.SubDischargeTypeId == 8).Count() > 0)
                            //    {
                            //        tempviewrequest.SubDateItem = jsondischargeprocess.list.subdischarges.Find(y => y.WorklistId == x.WorklistId && y.SubDischargeTypeId == 8).SubDate;
                            //        tempviewrequest.IsNeedPrescription = true;
                            //    }
                            //    else
                            //    {
                            //        tempviewrequest.SubDateItem = DateTime.Parse("01/01/0001 00:00:00").ToString("dd/MM/yyyy HH:mm:ss");
                            //        tempviewrequest.IsNeedPrescription = true;
                            //    }

                            //    //if (jsondischargeprocess.list.dischargerequests.Where(y => y.IsPrescription == true && y.WorklistId == x.WorklistId).Count() == 0)
                            //    //{
                            //    //    if (jsondischargeprocess.list.dischargerequests.Where(y => y.IsRetur == true && y.WorklistId == x.WorklistId).Count() == 0)
                            //    //    {
                            //    //        tempviewrequest.SubDateItem = DateTime.Parse("01/01/0001 00:00:00").ToString("dd/MM/yyyy HH:mm:ss");
                            //    //        tempviewrequest.IsNeedPrescription = false;
                            //    //    }
                            //    //    else
                            //    //        tempviewrequest.IsNeedPrescription = true;
                            //    //}
                            //    //else
                            //    //    tempviewrequest.IsNeedPrescription = true;
                            //}

                            if (jsondischargeprocess.list.dischargerequests.Where(y => y.WorklistId == x.WorklistId).Count() > 0)
                            {
                                if (jsondischargeprocess.list.dischargerequests.Where(y => y.IsPrescription == true && y.WorklistId == x.WorklistId).Count() == 0)
                                {
                                    tempviewrequest.IsPrescription = false;
                                }
                                else
                                    tempviewrequest.IsPrescription = true;

                                if (jsondischargeprocess.list.dischargerequests.Where(y => y.IsRetur == true && y.WorklistId == x.WorklistId).Count() == 0)
                                {
                                    tempviewrequest.IsRetur = false;
                                }
                                else
                                    tempviewrequest.IsRetur = true;
                            }


                            if (jsondischargeprocess.list.subdischarges.Where(y => y.WorklistId == x.WorklistId && y.FinalDate != null).Count() > 0)
                            {
                                tempviewrequest.FinalDate = jsondischargeprocess.list.subdischarges.Find(y => y.WorklistId == x.WorklistId && y.FinalDate != null).FinalDate;
                            }
                            else
                            {
                                tempviewrequest.FinalDate = DateTime.Parse("01/01/0001 00:00:00").ToString("dd/MM/yyyy HH:mm:ss");
                            }

                            if (tempviewrequest.SubDateService != "01/01/0001 00:00:00")
                            {
                                TimeSpan servicelate = DateTime.Parse(tempviewrequest.SubDateService).Subtract(tempviewrequest.SubmitDate);
                                double minutelate = servicelate.TotalMinutes;
                                if (minutelate > lateduration)
                                    tempviewrequest.lateservice = "true";
                                else
                                    tempviewrequest.lateservice = "false";
                            }
                            else
                                tempviewrequest.lateservice = "false";

                            if (tempviewrequest.SubDateItem != "01/01/0001 00:00:00")
                            {
                                TimeSpan itemlate = DateTime.Parse(tempviewrequest.SubDateItem).Subtract(tempviewrequest.SubmitDate);
                                double minutelate = itemlate.TotalMinutes;
                                if (minutelate > lateduration)
                                    tempviewrequest.lateitem = "true";
                                else
                                    tempviewrequest.lateitem = "false";
                            }
                            else
                                tempviewrequest.lateitem = "false";

                            if (tempviewrequest.EmailDate != "01/01/0001 00:00:00")
                            {
                                TimeSpan emaillate = DateTime.Parse(tempviewrequest.EmailDate).Subtract(DateTime.Parse(tempviewrequest.FinalDate));
                                double minutelate = emaillate.TotalMinutes;
                                if (minutelate > latedurationinsurance)
                                    tempviewrequest.lateemail = "true";
                                else
                                    tempviewrequest.lateemail = "false";
                            }
                            else
                                tempviewrequest.lateemail = "false";

                            if (tempviewrequest.ConfirmDate != "01/01/0001 00:00:00")
                            {
                                TimeSpan confirmlate = DateTime.Parse(tempviewrequest.ConfirmDate).Subtract(DateTime.Parse(tempviewrequest.EmailDate));
                                double minutelate = confirmlate.TotalMinutes;
                                if (minutelate > latedurationinsurance)
                                    tempviewrequest.lateconfirm = "true";
                                else
                                    tempviewrequest.lateconfirm = "false";
                            }
                            else
                                tempviewrequest.lateconfirm = "false";


                            if (tempviewrequest.InvoiceDate != "01/01/0001 00:00:00")
                            {
                                TimeSpan Invoicelate = DateTime.Parse(tempviewrequest.InvoiceDate).Subtract(DateTime.Parse(tempviewrequest.FinalDate));
                                double minutelate = Invoicelate.TotalMinutes;
                                if (minutelate > latedurationinvoice)
                                    tempviewrequest.lateinvoice = "true";
                                else
                                    tempviewrequest.lateinvoice = "false";
                            }
                            else
                                tempviewrequest.lateinvoice = "false";

                            if (tempviewrequest.PayerName.ToLower() == "private")
                            {
                                if (tempviewrequest.InvoiceDate != "01/01/0001 00:00:00")
                                {
                                    TimeSpan totalprivate = DateTime.Parse(x.InvoiceDate).Subtract(x.SubmitDate);
                                    double minutelate = totalprivate.TotalMinutes;
                                    if (minutelate > latedurationtotalprivate)
                                        tempviewrequest.latetotal = "true";
                                    else
                                        tempviewrequest.latetotal = "false";
                                }
                                else
                                    tempviewrequest.latetotal = "false";
                            }
                            else if (tempviewrequest.PayerName.ToLower() != "private")
                            {
                                if (tempviewrequest.InvoiceDate != "01/01/0001 00:00:00")
                                {
                                    TimeSpan totalprivate = DateTime.Parse(x.InvoiceDate).Subtract(x.SubmitDate);
                                    double minutelate = totalprivate.TotalMinutes;
                                    if (minutelate > latedurationtotalpayer)
                                        tempviewrequest.latetotal = "true";
                                    else
                                        tempviewrequest.latetotal = "false";
                                }
                                else
                                    tempviewrequest.latetotal = "false";
                            }
                            viewrequest.Add(tempviewrequest);
                        }

                    }

                    List<long> WardCollection = (from x in tempview
                                                 select x.WardId).Distinct().ToList();

                    Session[Helper.CollectionDischargeProcess + hfguidadditional.Value] = viewrequest;
                    List<WardCollection> tempwardcoll = new List<WardCollection>();

                    if (Session[Helper.SessionWard + hfguidadditional.Value] == null)
                    {
                        var GetWard = clsWard.GetWard(long.Parse(hfOrganizationId.Value));
                        var ListWard = JsonConvert.DeserializeObject<ListWard>(GetWard.Result.ToString());

                        Session[Helper.SessionWard + hfguidadditional.Value] = ListWard.list;
                    }


                    List<Ward> listWard = (List<Ward>)Session[Helper.SessionWard + hfguidadditional.Value];

                    foreach (var x in WardCollection)
                    {
                        WardCollection temp = new WardCollection();
                        temp.WardId = x;
                        temp.WardName = listWard.Find(y => y.WardId == x).Name;
                        tempwardcoll.Add(temp);
                    }
                    Session[Helper.wardcollection + hfguidadditional.Value] = tempwardcoll;
                    Session[Helper.CommonColorGrid] = null;
                    rptDischargeProcess.DataSource = tempwardcoll;
                    rptDischargeProcess.DataBind();
                }
                else
                {
                    lblCountProcess.Text = "(0)";
                    imgnodataprocess.Visible = true;
                    rptDischargeProcess.DataSource = null;
                    rptDischargeProcess.DataBind();
                }
            }
            else
            {
                ShowToastr("Data tidak ditemukan.", "info", "info");
            }
            log.Info(LogLibrary.Logging("E", "GetWorklistProcess", hfName.Value, "Finish GetWorklistProcess"));
        }
        catch (Exception ex)
        {
            ShowToastr("Terjadi kesalahan pada sistem. Mohon hubungi staf IT di rumah sakit Anda.", "error", "error");
            log.Error(LogLibrary.Error("btnPreview_click", hfName.Value, ex.Message));
        }
    }

    protected void DatePlan_OnChange(object sender, EventArgs e)
    {
        try
        {
            getCountPatient();
            getListPatient();

            List<WardCollection> tempwardcoll = (List<WardCollection>)Session[Helper.wardcollection + hfguidadditional.Value];
            WardCollection temp = new WardCollection();
            temp.WardId = 0;
            temp.WardName = "Semua Bangsal";
            tempwardcoll.Insert(0, temp);

            ddlWardlist.DataSource = tempwardcoll;
            ddlWardlist.DataTextField = "WardName";
            ddlWardlist.DataValueField = "WardId";
            ddlWardlist.SelectedValue = "0";
            ddlWardlist.DataBind();
        }
        catch (Exception ex)
        {
            ShowToastr("Terjadi kesalahan pada sistem. Mohon hubungi staf IT di rumah sakit Anda.", "error", "error");
            log.Error(LogLibrary.Error("DatePlan_OnChange", hfName.Value, ex.Message));
        }


    }

    protected void DateDone_OnChange(object sender, EventArgs e)
    {
        try
        {
            var date = "";
            if (txtdatedoneto.Text == "")
            {
                date = DateTime.Now.ToString("yyyy-MM-dd");
            }
            else
            {
                date = DateTime.Parse(txtdatedoneto.Text).ToString("yyyy-MM-dd");
            }

            log.Info(LogLibrary.Logging("S", "GetWorklistDone", hfName.Value, ""));

            if (txtdatedoneto.Text != "")
            {
                btnShowClear.Visible = true;
                btnShowCalendar.Visible = false;
            }
            else
            {
                btnShowClear.Visible = false;
                btnShowCalendar.Visible = true;
            }

            int lateduration;
            int latedurationinsurance;
            int latedurationinvoice;
            int latedurationtotalprivate;
            int latedurationtotalpayer;

            log.Debug(LogLibrary.Logging("S", "GetOrganizationSetting", hfName.Value, ""));
            var orgsetting = clsOrganizationSetting.GetOrganizationSetting(Int64.Parse(hfOrganizationId.Value));
            var jsonorgsetting = JsonConvert.DeserializeObject<ResultViewOrganizationSetting>(orgsetting.Result.ToString());
            log.Debug(LogLibrary.Logging("E", "GetOrganizationSetting", hfName.Value, jsonorgsetting.ToString()));

            if (jsonorgsetting.list.Count() > 0)
            {
                lateduration = int.Parse(jsonorgsetting.list.Find(y => y.setting_name == "LATE_SUBDISCHARGE").setting_value);
                latedurationinsurance = int.Parse(jsonorgsetting.list.Find(y => y.setting_name == "LATE_INSURANCE").setting_value);
                latedurationinvoice = int.Parse(jsonorgsetting.list.Find(y => y.setting_name == "LATE_INVOICE").setting_value);
                latedurationtotalprivate = int.Parse(jsonorgsetting.list.Find(y => y.setting_name == "LATE_TOTALPRIVATE").setting_value);
                latedurationtotalpayer = int.Parse(jsonorgsetting.list.Find(y => y.setting_name == "LATE_TOTALPAYER").setting_value);
            }
            else
            {
                lateduration = 20; //warna merah untuk service dan item
                latedurationinsurance = 20; //warna merah untuk email dan confirm
                latedurationinvoice = 20; //warna merah invoice selesai
                latedurationtotalprivate = 30; //warna merah untuk lama kerja pasien private
                latedurationtotalpayer = 120; //warna merah untuk lama kerja pasien with payer
            }

            lblDateRefreshDone.Text = DateTime.Now.ToString("dd MMM yyyy, HH:mm");
            List<ViewDischargeRequest> viewrequest = new List<ViewDischargeRequest>();
            long wardId = 0;
            if (ddlWardListDone.SelectedValue != "")
                wardId = long.Parse(ddlWardListDone.SelectedValue);

            log.Debug(LogLibrary.Logging("S", "getListDischargeProcess", hfName.Value, ""));
            var dischargeprocess = clsWorklist.getListDischargeProcess(Int64.Parse(hfOrganizationId.Value), date, wardId, txtsearchdone.Text, 1);
            var jsondischargeprocess = JsonConvert.DeserializeObject<ResultListDischargeProcess>(dischargeprocess.Result.ToString());
            log.Debug(LogLibrary.Logging("E", "getListDischargeProcess", hfName.Value, jsondischargeprocess.ToString()));

            if (jsondischargeprocess.list.dischargerequests != null)
            {
                if (jsondischargeprocess.list.dischargerequests.Where(y => y.ArInvoiceId != 0).Count() > 0)
                {
                    nodatadone.Visible = false;
                    List<DischargeRequest> tempview = new List<DischargeRequest>();
                    if (txtdatedoneto.Text != "")
                        tempview = jsondischargeprocess.list.dischargerequests.Where(y => y.ArInvoiceId != 0 && DateTime.Parse(y.InvoiceDate).ToString("dd MMM yyyy") == DateTime.Parse(txtdatedoneto.Text).ToString("dd MMM yyyy")).ToList();
                    else
                        tempview = jsondischargeprocess.list.dischargerequests.Where(y => y.ArInvoiceId != 0).ToList();

                    //tempview = jsondischargeprocess.list.dischargerequests.Where(y => y.ArInvoiceId != 0).ToList();
                    lblCountDone.Text = " (" + jsondischargeprocess.list.dischargerequests.Where(y => y.ArInvoiceId != 0 && y.IsPrimary == true).Count().ToString() + ") ";
                    foreach (var x in tempview)
                    {

                        if (x.IsPrimary == true)
                        {
                            ViewDischargeRequest tempviewrequest = new ViewDischargeRequest();
                            tempviewrequest.SubmitDate = x.SubmitDate;
                            tempviewrequest.isShowDate = x.isShowDate;
                            tempviewrequest.WorklistId = x.WorklistId;
                            tempviewrequest.ProcessId = x.ProcessId;
                            tempviewrequest.AdmissionId = x.AdmissionId;
                            tempviewrequest.AdmissionNo = x.AdmissionNo;
                            tempviewrequest.PatientId = x.PatientId;
                            tempviewrequest.PatientName = x.PatientName;
                            tempviewrequest.WardId = x.WardId;
                            tempviewrequest.islate = x.islate;
                            tempviewrequest.PayerName = x.PayerName;
                            tempviewrequest.DoctorId = x.DoctorId;
                            tempviewrequest.DoctorName = x.DoctorName;
                            tempviewrequest.AdditionalNotes = x.AdditionalNotes;
                            tempviewrequest.ArInvoiceId = x.ArInvoiceId;
                            tempviewrequest.IsPrimary = x.IsPrimary;
                            tempviewrequest.IsPrescription = x.IsPrescription;
                            tempviewrequest.IsRetur = x.IsRetur;
                            tempviewrequest.EmailDate = x.EmailDate;
                            tempviewrequest.ConfirmDate = x.ConfirmDate;
                            tempviewrequest.InvoiceDate = x.InvoiceDate;
                            tempviewrequest.FUPatient = x.FUPatient;
                            tempviewrequest.OPDControl = x.OPDControl;
                            tempviewrequest.localMrNo = x.LocalMrNo;
                            tempviewrequest.birthDate = x.BirthDate.ToString("dd MMM yyyy");
                            tempviewrequest.roomNo = x.RoomNo;
                            tempviewrequest.FlagDischarged = x.FlagDischarged;

                            TimeSpan test = DateTime.Parse(x.InvoiceDate).Subtract(x.SubmitDate);
                            int hour = 0;
                            if (test.Days > 0)
                            {
                                hour = test.Hours + (24 * test.Days);
                            }
                            else
                                hour = test.Hours;

                            string minute = "";
                            if (test.Minutes < 10)
                                minute = "0" + test.Minutes.ToString();
                            else
                                minute = test.Minutes.ToString();
                            string second = "";
                            if (test.Seconds < 10)
                                second = "0" + test.Seconds.ToString();
                            else
                                second = test.Seconds.ToString();

                            tempviewrequest.Duration = hour.ToString() + ":" + minute + ":" + second;

                            tempviewrequest.ModifiedDate = x.ModifiedDate;

                            if (jsondischargeprocess.list.subdischarges.Where(y => y.WorklistId == x.WorklistId && y.SubDischargeTypeId == 2).Count() > 0)
                            {
                                tempviewrequest.SubDateBed = jsondischargeprocess.list.subdischarges.Find(y => y.WorklistId == x.WorklistId && y.SubDischargeTypeId == 2).SubDate;
                            }
                            else
                            {
                                tempviewrequest.SubDateBed = DateTime.Parse("01/01/0001 00:00:00").ToString("dd/MM/yyyy HH:mm:ss");
                            }

                            if (jsondischargeprocess.list.subdischarges.Where(y => y.WorklistId == x.WorklistId && y.SubDischargeTypeId == 4).Count() > 0)
                            {
                                tempviewrequest.SubDateService = jsondischargeprocess.list.subdischarges.Find(y => y.WorklistId == x.WorklistId && y.SubDischargeTypeId == 4).SubDate;
                            }
                            else
                            {
                                tempviewrequest.SubDateService = DateTime.Parse("01/01/0001 00:00:00").ToString("dd/MM/yyyy HH:mm:ss");
                            }

                            if (jsondischargeprocess.list.subdischarges.Where(y => y.WorklistId == x.WorklistId && y.SubDischargeTypeId == 8).Count() > 0)
                            {
                                tempviewrequest.SubDateItem = jsondischargeprocess.list.subdischarges.Find(y => y.WorklistId == x.WorklistId && y.SubDischargeTypeId == 8).SubDate;
                                tempviewrequest.IsNeedPrescription = true;
                            }
                            else
                            {
                                tempviewrequest.SubDateItem = DateTime.Parse("01/01/0001 00:00:00").ToString("dd/MM/yyyy HH:mm:ss");
                                tempviewrequest.IsNeedPrescription = true;
                            }


                            //if (jsondischargeprocess.list.dischargerequests.Where(y => y.WorklistId == x.WorklistId).Count() > 0)
                            //{
                            //    if (jsondischargeprocess.list.subdischarges.Where(y => y.WorklistId == x.WorklistId && y.SubDischargeTypeId == 8).Count() > 0)
                            //    {
                            //        tempviewrequest.SubDateItem = jsondischargeprocess.list.subdischarges.Find(y => y.WorklistId == x.WorklistId && y.SubDischargeTypeId == 8).SubDate;
                            //    }
                            //    else
                            //    {
                            //        tempviewrequest.SubDateItem = DateTime.Parse("01/01/0001 00:00:00").ToString("dd/MM/yyyy HH:mm:ss");
                            //    }

                            //    if (jsondischargeprocess.list.dischargerequests.Where(y => y.IsPrescription == true && y.WorklistId == x.WorklistId).Count() == 0)
                            //    {
                            //        if (jsondischargeprocess.list.dischargerequests.Where(y => y.IsRetur == true && y.WorklistId == x.WorklistId).Count() == 0)
                            //        {
                            //            tempviewrequest.IsNeedPrescription = false;
                            //            tempviewrequest.SubDateItem = DateTime.Parse("01/01/0001 00:00:00").ToString("dd/MM/yyyy HH:mm:ss");
                            //        }
                            //        else
                            //            tempviewrequest.IsNeedPrescription = true;
                            //    }
                            //    else
                            //        tempviewrequest.IsNeedPrescription = true;
                            //}


                            if (jsondischargeprocess.list.subdischarges.Where(y => y.WorklistId == x.WorklistId && y.FinalDate != null).Count() > 0)
                            {
                                tempviewrequest.FinalDate = jsondischargeprocess.list.subdischarges.Find(y => y.WorklistId == x.WorklistId && y.FinalDate != null).FinalDate;
                            }
                            else
                            {
                                tempviewrequest.FinalDate = DateTime.Parse("01/01/0001 00:00:00").ToString("dd/MM/yyyy HH:mm:ss");
                            }

                            if (tempviewrequest.SubDateService != "01/01/0001 00:00:00")
                            {
                                TimeSpan servicelate = DateTime.Parse(tempviewrequest.SubDateService).Subtract(tempviewrequest.SubmitDate);
                                double minutelate = servicelate.TotalMinutes;
                                if (minutelate > lateduration)
                                    tempviewrequest.lateservice = "true";
                                else
                                    tempviewrequest.lateservice = "false";
                            }
                            else
                                tempviewrequest.lateservice = "false";

                            if (tempviewrequest.SubDateItem != "01/01/0001 00:00:00")
                            {
                                TimeSpan itemlate = DateTime.Parse(tempviewrequest.SubDateItem).Subtract(tempviewrequest.SubmitDate);
                                double minutelate = itemlate.TotalMinutes;
                                if (minutelate > lateduration)
                                    tempviewrequest.lateitem = "true";
                                else
                                    tempviewrequest.lateitem = "false";
                            }
                            else
                                tempviewrequest.lateitem = "false";

                            if (tempviewrequest.EmailDate != "01/01/0001 00:00:00")
                            {
                                TimeSpan emaillate = DateTime.Parse(tempviewrequest.EmailDate).Subtract(DateTime.Parse(tempviewrequest.FinalDate));
                                double minutelate = emaillate.TotalMinutes;
                                if (minutelate > latedurationinsurance)
                                    tempviewrequest.lateemail = "true";
                                else
                                    tempviewrequest.lateemail = "false";
                            }
                            else
                                tempviewrequest.lateemail = "false";

                            if (tempviewrequest.ConfirmDate != "01/01/0001 00:00:00")
                            {
                                TimeSpan confirmlate = DateTime.Parse(tempviewrequest.ConfirmDate).Subtract(DateTime.Parse(tempviewrequest.EmailDate));
                                double minutelate = confirmlate.TotalMinutes;
                                if (minutelate > latedurationinsurance)
                                    tempviewrequest.lateconfirm = "true";
                                else
                                    tempviewrequest.lateconfirm = "false";
                            }
                            else
                                tempviewrequest.lateconfirm = "false";


                            if (tempviewrequest.InvoiceDate != "01/01/0001 00:00:00")
                            {
                                TimeSpan Invoicelate = DateTime.Parse(tempviewrequest.InvoiceDate).Subtract(DateTime.Parse(tempviewrequest.FinalDate));
                                double minutelate = Invoicelate.TotalMinutes;
                                if (minutelate > latedurationinvoice)
                                    tempviewrequest.lateinvoice = "true";
                                else
                                    tempviewrequest.lateinvoice = "false";
                            }
                            else
                                tempviewrequest.lateinvoice = "false";

                            if (tempviewrequest.PayerName.ToLower() == "private")
                            {
                                if (tempviewrequest.InvoiceDate != "01/01/0001 00:00:00")
                                {
                                    TimeSpan totalprivate = DateTime.Parse(x.InvoiceDate).Subtract(x.SubmitDate);
                                    double minutelate = totalprivate.TotalMinutes;
                                    if (minutelate > latedurationtotalprivate)
                                        tempviewrequest.latetotal = "true";
                                    else
                                        tempviewrequest.latetotal = "false";
                                }
                                else
                                    tempviewrequest.latetotal = "false";
                            }
                            else if (tempviewrequest.PayerName.ToLower() != "private")
                            {
                                if (tempviewrequest.InvoiceDate != "01/01/0001 00:00:00")
                                {
                                    TimeSpan totalprivate = DateTime.Parse(x.InvoiceDate).Subtract(x.SubmitDate);
                                    double minutelate = totalprivate.TotalMinutes;
                                    if (minutelate > latedurationtotalpayer)
                                        tempviewrequest.latetotal = "true";
                                    else
                                        tempviewrequest.latetotal = "false";
                                }
                                else
                                    tempviewrequest.latetotal = "false";
                            }

                            viewrequest.Add(tempviewrequest);
                        }

                    }

                    List<long> WardCollection = (from x in tempview
                                                 select x.WardId).Distinct().ToList();

                    Session[Helper.CollectionDischargeDone + hfguidadditional.Value] = viewrequest;
                    List<WardCollection> tempwardcoll = new List<WardCollection>();

                    if (Session[Helper.SessionWard + hfguidadditional.Value] == null)
                    {
                        var GetWard = clsWard.GetWard(long.Parse(hfOrganizationId.Value));
                        var ListWard = JsonConvert.DeserializeObject<ListWard>(GetWard.Result.ToString());

                        Session[Helper.SessionWard + hfguidadditional.Value] = ListWard.list;
                    }


                    List<Ward> listWard = (List<Ward>)Session[Helper.SessionWard + hfguidadditional.Value];

                    foreach (var x in WardCollection)
                    {
                        WardCollection temp = new WardCollection();
                        temp.WardId = x;
                        temp.WardName = listWard.Find(y => y.WardId == x).Name;
                        tempwardcoll.Add(temp);
                    }
                    Session[Helper.wardcollection + hfguidadditional.Value] = tempwardcoll;

                    rptDischargeDone.DataSource = tempwardcoll;
                    rptDischargeDone.DataBind();
                }
                else
                {
                    lblCountDone.Text = "(0)";
                    nodatadone.Visible = true;
                    rptDischargeDone.DataSource = null;
                    rptDischargeDone.DataBind();
                }

                bindddldone();
            }
            else
            {
                ShowToastr("Data tidak ditemukan.", "error", "error");
            }
            //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "processclick", "processclick();", true);
            log.Info(LogLibrary.Logging("E", "GetWorklistDone", hfName.Value, "Finish GetWorklistDone"));
        }
        catch (Exception ex)
        {
            ShowToastr("Terjadi kesalahan pada sistem. Mohon hubungi staf IT di rumah sakit Anda.", "error", "error");
            log.Error(LogLibrary.Error("GetWorklistDone", hfName.Value, ex.Message));
        }
    }

    protected void clearsearchplan_onclick(object sender, EventArgs e)
    {

    }

    protected void BtnRefreshProcess_OnClick(object sender, EventArgs e)
    {
        txtdateprocess.Text = "";
        GetWorklistProcess();
        getCountPatient();

    }

    protected void BtnRefreshPlan_OnClick(object sender, EventArgs e)
    {
        getCountPatient();
        getListPatient();

    }

    protected void BtnRefreshDone_OnClick(object sender, EventArgs e)
    {
        txtsearchdone.Text = "";
        txtdatedoneto.Text = "";
        GetWorklistDone();
        getCountPatient();
    }

    public void initializevalueHeader(PatientHeaderIpd model)
    {
        if (model.Gender == "M")
        {
            Image1.ImageUrl = "~/Images/Dashboard/ic_PatientMale_Big.svg";
            imgSex.ImageUrl = "~/Images/Icon/ic_Male.svg";
        }
        else if (model.Gender == "F")
        {
            Image1.ImageUrl = "~/Images/Dashboard/ic_PatientFemale_Big.svg";
            imgSex.ImageUrl = "~/Images/Icon/ic_Female.svg";
        }

        patientName.Text = model.PatientName;
        localMrNo.Text = model.MrNo;
        primaryDoctor.Text = model.DoctorName;
        lblAdmissionNo.Text = model.AdmissionNo;
        lbltgllahirlab.Text = model.BirthDate.ToString("dd MMM yyyy");
        lblUsia.Text = model.Age;//clsWorklist.GetAge(model.BirthDate);
        lblReligion.Text = model.Religion;
        lblPayer.Text = model.PayerName;
    }

    protected void clearpatientdone_onClick(object sender, EventArgs e)
    {
        txtsearchdone.Text = "";
        GetWorklistDone();
    }

    public void initializevalue(List<LaboratoryResult> listlaboratory)
    {
        panel1.InnerHtml = "";
        try
        {
            //log.Info(LogLibrary.Logging("S", "getLaboratoryHistory ", Helper.GetLoginUser(this.Page), ""));

            StringBuilder labHistory = new StringBuilder();

            if (listlaboratory.Count != 0)
            {
                if (listlaboratory[0].ConnStatus == "OFFLINE")
                {
                    img_noConnection.Visible = true;
                    img_noData.Style.Add("display", "none");
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
                                            labHistory.Append("<tr><td style=\"width:40%\"><h7><b>" + dataLab.tesT_NM + "</b></h7></td>" +
                                                                "<td style=\"width:20%\"><h7>" + dataLab.resulT_VALUE + "</h7></td>" +
                                                                "<td style=\"width:20%\"><h7>" + dataLab.unit + "</h7></td>" +
                                                                "<td style=\"width:20%\"><h7>" + dataLab.reF_RANGE + "</h7></td>" +
                                                                //"<td><h7><b>" + dataLab.ono + "</b></h7></td>" +
                                                                //"<td><h7><b>" + dataLab.dis_sq + "</b></h7></td>" +
                                                                "</tr>");
                                        }
                                        else
                                        {
                                            labHistory.Append("<tr><td style=\"width:40%\">" + dataLab.tesT_NM + "</td>" +
                                                            "<td style=\"width:20%\">" + dataLab.resulT_VALUE + "</td>" +
                                                            "<td style=\"width:20%\">" + dataLab.unit + "</td>" +
                                                            "<td style=\"width:20%\">" + dataLab.reF_RANGE + "</td>" +
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
                img_noData.Style.Add("display", "");
            }

            panel1.InnerHtml = labHistory.ToString();
            //log.Info(LogLibrary.Logging("E", "getLaboratoryHistory ", Helper.GetLoginUser(this.Page), ""));

        }
        catch (Exception ex)
        {
            throw ex;
            //log.Error(LogLibrary.Error("getLaboratoryResult", Helper.GetLoginUser(this.Page), ex.Message));
        }


    }
}