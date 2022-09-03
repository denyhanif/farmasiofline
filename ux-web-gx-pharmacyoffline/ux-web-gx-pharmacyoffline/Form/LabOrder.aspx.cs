using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Form_LabOrder : System.Web.UI.Page
{
    HiddenField organizationId;
    HiddenField patientId;
    HiddenField admId;
    HiddenField encId;
    HiddenField isLabRad;
    HiddenField printBY;
    HiddenField doctorName;
    HiddenField pageSOAP;

    protected void Page_Load(object sender, EventArgs e)
    {
        long OrgId,MrNo;

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
            }

            string en = Request.QueryString["en"];
            if (en != null)
            {
                OrgId = long.Parse(Request.QueryString["OrgId"]);
                MrNo = long.Parse(Helper.Decrypt(Request.QueryString["MrNo"]));
            }
            else
            {
                OrgId = long.Parse(Request.QueryString["OrgId"]);
                MrNo = long.Parse(Request.QueryString["MrNo"]);
            }
            
            getWorklist(OrgId, MrNo);
            //getDiagProc(2, 1428601, 2000005993901, Guid.Parse("20edd89a-24eb-4501-950b-1fef6e7d19f1"));
        }
    }

    public void getWorklist(long OrganizationId, long MrNo)
    {
        List<ViewWorklistViewerLabRad> listworklist = new List<ViewWorklistViewerLabRad>();

        var worklist = clsLabRadOrder.getWorkListLabRad(OrganizationId, MrNo);
        var JsonWorklist = JsonConvert.DeserializeObject<ResultViewWorklistViewerLabRad>(worklist.Result.ToString());

        listworklist = JsonWorklist.list;
        DataTable dtworklist = Helper.ToDataTable(listworklist);
        gvw_order.DataSource = dtworklist;
        gvw_order.DataBind();
    }

    protected void btnPrintLab_Click(object sender, EventArgs e)
    {
        int selRowIndex = ((GridViewRow)(((ImageButton)sender).Parent.Parent)).RowIndex;

        encId = (HiddenField)gvw_order.Rows[selRowIndex].FindControl("hdnEncId");
        organizationId = (HiddenField)gvw_order.Rows[selRowIndex].FindControl("hdnOrganizationId");
        admId = (HiddenField)gvw_order.Rows[selRowIndex].FindControl("hdnAdmId");
        patientId = (HiddenField)gvw_order.Rows[selRowIndex].FindControl("hdnPatientId");
        isLabRad = (HiddenField)gvw_order.Rows[selRowIndex].FindControl("hdnIsLab");
        hfPrintBY.Value = Request.QueryString["PrintBy"];

        var localIPAdress = "";

        localIPAdress = GetLocalIPAddress();
        //localIPAdress = "10.83.254.38"; //HARD CODE

        //ScriptManager.RegisterStartupScript(this,GetType(), "OpenPopUp", "Alert('heloo')", true);

        ScriptManager.RegisterStartupScript(
        this, GetType(), "OpenWindow", "window.open('http://" + localIPAdress + "/printingemr?printtype=OrderLab&OrganizationId=" + organizationId.Value + "&AdmissionId=" + admId.Value + "&EncounterId=" + encId.Value + "&PatientId=" + patientId.Value + "&IsLabRad=" + isLabRad.Value.ToString() + "&PrintBy=" + hfPrintBY.Value.ToString() + "','_blank');", true);
    }

    protected void btnPrintRad_Click(object sender, EventArgs e)
    {
        int selRowIndex = ((GridViewRow)(((ImageButton)sender).Parent.Parent)).RowIndex;

        encId = (HiddenField)gvw_order.Rows[selRowIndex].FindControl("hdnEncId");
        organizationId = (HiddenField)gvw_order.Rows[selRowIndex].FindControl("hdnOrganizationId");
        admId = (HiddenField)gvw_order.Rows[selRowIndex].FindControl("hdnAdmId");
        patientId = (HiddenField)gvw_order.Rows[selRowIndex].FindControl("hdnPatientId");
        isLabRad = (HiddenField)gvw_order.Rows[selRowIndex].FindControl("hdnIsLab");
        hfPrintBY.Value = Request.QueryString["PrintBy"];

        var localIPAdress = "";

        localIPAdress = GetLocalIPAddress();

        //localIPAdress = "10.83.254.38"; //HARD CODE

        //ScriptManager.RegisterStartupScript(this,GetType(), "OpenPopUp", "Alert('heloo')", true);

        ScriptManager.RegisterStartupScript(
        this, GetType(), "OpenWindow", "window.open('http://" + localIPAdress + "/printingemr?printtype=OrderRad&OrganizationId=" + organizationId.Value + "&AdmissionId=" + admId.Value + "&EncounterId=" + encId.Value + "&PatientId=" + patientId.Value + "&IsLabRad=" + isLabRad.Value.ToString() + "&PrintBy=" + hfPrintBY.Value.ToString() + "','_blank');", true);
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

    public void getDiagProc(long orgid, long patientid, long admid, Guid encid)
    {
        List<DiagProcItem> listdiagproc = new List<DiagProcItem>();

        var data = clsLabRadOrder.getDiagprocLabRad(orgid, patientid, admid, encid);
        var Jsondata = JsonConvert.DeserializeObject<ResultDiagProcItem>(data.Result.ToString());

        listdiagproc = Jsondata.list;
        DataTable dt_proc = Helper.ToDataTable(listdiagproc.Where(p => p.ProcedureItemTypeId == 5).ToList());
        DataTable dt_diag = Helper.ToDataTable(listdiagproc.Where(p => p.ProcedureItemTypeId == 4).ToList());
        Repeaterproc.DataSource = dt_proc;
        Repeaterproc.DataBind();
        Repeaterdiag.DataSource = dt_diag;
        Repeaterdiag.DataBind();
    }

    protected void btnPopDiagProc_Click(object sender, ImageClickEventArgs e)
    {
        //getDiagProc(long.Parse(HF_orgid.Value), long.Parse(HF_ptnid.Value), long.Parse(HF_admid.Value), Guid.Parse(HF_encid.Value));
        //LabelTglAdm.Text = HF_tgladmn.Value;
        //LabelNoAdm.Text = HF_noadm.Value;
        //LabelDOkter.Text = HF_dokter.Value;

        int selRowIndex = ((GridViewRow)(((ImageButton)sender).Parent.Parent)).RowIndex;

        encId = (HiddenField)gvw_order.Rows[selRowIndex].FindControl("hdnEncId");
        organizationId = (HiddenField)gvw_order.Rows[selRowIndex].FindControl("hdnOrganizationId");
        admId = (HiddenField)gvw_order.Rows[selRowIndex].FindControl("hdnAdmId");
        patientId = (HiddenField)gvw_order.Rows[selRowIndex].FindControl("hdnPatientId");
        pageSOAP = (HiddenField)gvw_order.Rows[selRowIndex].FindControl("hdnPageSoap");
        hfPrintBY.Value = Request.QueryString["PrintBy"];

        var localIPAdress = "";

        localIPAdress = GetLocalIPAddress();

        //localIPAdress = "10.83.254.38"; //HARD CODE

        ScriptManager.RegisterStartupScript(
        this, GetType(), "OpenWindow", "window.open('http://" + localIPAdress + "/printingemr?printtype=ProcedureDiagnostic&OrganizationId=" + organizationId.Value + "&AdmissionId=" + admId.Value + "&EncounterId=" + encId.Value + "&PatientId=" + patientId.Value + "&PageSOAP=" + pageSOAP.Value + "&PrintBy=" + hfPrintBY.Value.ToString() + "','_blank');", true);
    }

    public void getPresOrder(long orgid, long patientid, long admid, Guid encid)
    {
        ViewPrescriptionOrder listpresorder = new ViewPrescriptionOrder();
        List<PrescriptionOrderDrug> listpresdrug = new List<PrescriptionOrderDrug>();
        List<PrescriptionOrderCompoundHeader> listpresracikanheader = new List<PrescriptionOrderCompoundHeader>();
        List<PrescriptionOrderCompoundDetail> listpresracikandetail = new List<PrescriptionOrderCompoundDetail>();

        var datapres = clsLabRadOrder.getPrescriptionOrder(orgid, patientid, admid, encid);
        var Jsondata = JsonConvert.DeserializeObject<ResultViewPrescriptionOrder>(datapres.Result.ToString());

        listpresorder = Jsondata.list;
        listpresdrug = listpresorder.PrescriptionOrderDrug;
        listpresracikanheader = listpresorder.PrescriptionOrderCompoundHeader;
        listpresracikandetail = listpresorder.PrescriptionOrderCompoundDetail;

        if (listpresdrug.Count != 0)
        {
            foreach (var templist in listpresdrug)
            {
                templist.Dose = Helper.formatDecimal(templist.Dose);

                //string[] tempdose = templist.Dose.ToString().Split('.');
                //if (tempdose.Count() > 1)
                //{
                //    if (tempdose[1].Length == 3)
                //    {
                //        if (tempdose[1] == "000")
                //        {
                //            templist.Dose = decimal.Parse(tempdose[0]).ToString();
                //        }
                //        else if (tempdose[1].Substring(tempdose[1].Length - 2) == "00")
                //        {
                //            templist.Dose = tempdose[0] + "." + tempdose[1].Substring(0, 1);
                //        }
                //        else if (tempdose[1].Substring(tempdose[1].Length - 1) == "0")
                //        {
                //            templist.Dose = tempdose[0] + "." + tempdose[1].Substring(0, 2);
                //        }
                //    }
                //}
            }

            DataTable dt_pres = Helper.ToDataTable(listpresdrug.Where(p => p.IsConsumables == false).ToList());
            GvwPrescriptionOrder.DataSource = dt_pres;
            GvwPrescriptionOrder.DataBind();
            DataTable dt_cons = Helper.ToDataTable(listpresdrug.Where(p => p.IsConsumables == true).ToList());
            GvwConsumableOrder.DataSource = dt_cons;
            GvwConsumableOrder.DataBind();
        }


        if (listpresracikanheader.Count > 0)
        {
            foreach (var templist in listpresracikanheader)
            {
                templist.dose = Helper.formatDecimal(templist.dose);
                templist.quantity = Helper.formatDecimal(templist.quantity);
            }


                //if (Helper.ToDataTable(listpresracikanheader.Where(y => y.is_additional == false).ToList()).Rows.Count > 0)
                //{
                DataTable dtracikan = Helper.ToDataTable(listpresracikanheader);
                //Session[Helper.SessionRacikanHeader + hfguidadditional.Value] = dtracikan;

                if (listpresracikandetail.Count > 0)
                {
                    foreach (var templist in listpresracikandetail)
                    {
                        templist.dose = Helper.formatDecimal(templist.dose);
                    }

                    DataTable dtracikandetail = Helper.ToDataTable(listpresracikandetail);
                    Session[Helper.SessionRacikanDetail] = dtracikandetail;
                }

                gvw_racikan_header.DataSource = dtracikan;
                gvw_racikan_header.DataBind();
            //}
            //else
            //{
            //    //Session[Helper.SessionRacikanHeader + hfguidadditional.Value] = null;
            //    gvw_racikan_header.DataSource = null;
            //    gvw_racikan_header.DataBind();
            //    Session[Helper.SessionRacikanDetail] = null;
            //}
        }
        else
        {
            gvw_racikan_header.DataSource = null;
            gvw_racikan_header.DataBind();
            Session[Helper.SessionRacikanDetail] = null;
        }
    }

    protected void btnPopPrescription_Click(object sender, ImageClickEventArgs e)
    {
        getPresOrder(long.Parse(HF_orgid.Value), long.Parse(HF_ptnid.Value), long.Parse(HF_admid.Value), Guid.Parse(HF_encid.Value));
        LabelTglAdmPres.Text = HF_tgladmn.Value;
        LabelNoAdmPres.Text = HF_noadm.Value;
        LabelDOkterPres.Text = HF_dokter.Value;
    }

    protected void btnPrintSOAP_Click(object sender, ImageClickEventArgs e)
    {
        int selRowIndex = ((GridViewRow)(((ImageButton)sender).Parent.Parent)).RowIndex;

        encId = (HiddenField)gvw_order.Rows[selRowIndex].FindControl("hdnEncId");
        organizationId = (HiddenField)gvw_order.Rows[selRowIndex].FindControl("hdnOrganizationId");
        admId = (HiddenField)gvw_order.Rows[selRowIndex].FindControl("hdnAdmId");
        patientId = (HiddenField)gvw_order.Rows[selRowIndex].FindControl("hdnPatientId");
        hfPrintBY.Value = Request.QueryString["PrintBy"];
        doctorName = (HiddenField)gvw_order.Rows[selRowIndex].FindControl("hdnDoctorName");

        var localIPAdress = "";

        localIPAdress = GetLocalIPAddress();
        //localIPAdress = "10.83.254.38"; //HARD CODE

        ScriptManager.RegisterStartupScript(
        this, GetType(), "OpenWindow", "window.open('http://" + localIPAdress + "/printingemr?printtype=MedicalResumeLite&OrganizationId=" + organizationId.Value + "&AdmissionId=" + admId.Value + "&EncounterId=" + encId.Value + "&PatientId=" + patientId.Value + "&PrintBy=" + hfPrintBY.Value.ToString() + "&DoctorBy=" + doctorName.Value.ToString() + "','_blank');", true);
    }

    protected void btnPrintSOAPLong_Click(object sender, ImageClickEventArgs e)
    {
        int selRowIndex = ((GridViewRow)(((ImageButton)sender).Parent.Parent)).RowIndex;

        encId = (HiddenField)gvw_order.Rows[selRowIndex].FindControl("hdnEncId");
        organizationId = (HiddenField)gvw_order.Rows[selRowIndex].FindControl("hdnOrganizationId");
        admId = (HiddenField)gvw_order.Rows[selRowIndex].FindControl("hdnAdmId");
        patientId = (HiddenField)gvw_order.Rows[selRowIndex].FindControl("hdnPatientId");
        pageSOAP = (HiddenField)gvw_order.Rows[selRowIndex].FindControl("hdnPageSoap");
        hfPrintBY.Value = Request.QueryString["PrintBy"];

        var localIPAdress = "";

        localIPAdress = GetLocalIPAddress();

        //localIPAdress = "10.83.254.38"; //HARD CODE

        ScriptManager.RegisterStartupScript(
        this, GetType(), "OpenWindow", "window.open('http://" + localIPAdress + "/printingemr?printtype=MedicalResume&OrganizationId=" + organizationId.Value + "&AdmissionId=" + admId.Value + "&EncounterId=" + encId.Value + "&PatientId=" + patientId.Value + "&PageSOAP=" + pageSOAP.Value + "&PrintBy=" + hfPrintBY.Value.ToString() + "','_blank');", true);
    }

    protected void btnPrintReferral_Click(object sender, ImageClickEventArgs e)
    {
        int selRowIndex = ((GridViewRow)(((ImageButton)sender).Parent.Parent)).RowIndex;

        encId = (HiddenField)gvw_order.Rows[selRowIndex].FindControl("hdnEncId");
        organizationId = (HiddenField)gvw_order.Rows[selRowIndex].FindControl("hdnOrganizationId");
        admId = (HiddenField)gvw_order.Rows[selRowIndex].FindControl("hdnAdmId");
        patientId = (HiddenField)gvw_order.Rows[selRowIndex].FindControl("hdnPatientId");
        pageSOAP = (HiddenField)gvw_order.Rows[selRowIndex].FindControl("hdnPageSoap");
        hfPrintBY.Value = Request.QueryString["PrintBy"];

        var localIPAdress = "";

        localIPAdress = GetLocalIPAddress();
        //localIPAdress = "10.83.254.38"; //HARD CODE

        ScriptManager.RegisterStartupScript(
        this, GetType(), "OpenWindow", "window.open('http://" + localIPAdress + "/printingemr?printtype=ReferralResume&OrganizationId=" + organizationId.Value + "&AdmissionId=" + admId.Value + "&EncounterId=" + encId.Value + "&PatientId=" + patientId.Value + "&PageSOAP=" + pageSOAP.Value + "&PrintBy=" + hfPrintBY.Value.ToString() + "','_blank');", true);
    }

    protected void btnPrintRawatInap_Click(object sender, ImageClickEventArgs e)
    {
        int selRowIndex = ((GridViewRow)(((ImageButton)sender).Parent.Parent)).RowIndex;

        encId = (HiddenField)gvw_order.Rows[selRowIndex].FindControl("hdnEncId");
        organizationId = (HiddenField)gvw_order.Rows[selRowIndex].FindControl("hdnOrganizationId");
        admId = (HiddenField)gvw_order.Rows[selRowIndex].FindControl("hdnAdmId");
        patientId = (HiddenField)gvw_order.Rows[selRowIndex].FindControl("hdnPatientId");
        pageSOAP = (HiddenField)gvw_order.Rows[selRowIndex].FindControl("hdnPageSoap");
        hfPrintBY.Value = Request.QueryString["PrintBy"];

        var localIPAdress = "";

        localIPAdress = GetLocalIPAddress();
        //localIPAdress = "10.83.254.38"; //HARD CODE

        ScriptManager.RegisterStartupScript(
        this, GetType(), "OpenWindow", "window.open('http://" + localIPAdress + "/printingemr?printtype=RawatInap&OrganizationId=" + organizationId.Value + "&AdmissionId=" + admId.Value + "&EncounterId=" + encId.Value + "&PatientId=" + patientId.Value + "&PageSOAP=" + pageSOAP.Value + "&PrintBy=" + hfPrintBY.Value.ToString() + "','_blank');", true);
    }

    protected void gvw_racikan_header_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string headerId = gvw_racikan_header.DataKeys[e.Row.RowIndex].Value.ToString();
            GridView gvDetails = e.Row.FindControl("gvw_racikan_detail") as GridView;

            if (Session[Helper.SessionRacikanDetail] != null)
            {
                DataRow[] dr = ((DataTable)Session[Helper.SessionRacikanDetail]).Select("prescription_compound_header_id = '" + headerId + "'");

                if (dr.Length > 0)
                {
                    //DataTable dtDetail = ((DataTable)Session[Helper.SessionRacikanDetail + hfguidadditional.Value]).Select("prescription_compound_header_id = '" + headerId + "'").CopyToDataTable();
                    DataTable dtDetail = dr.CopyToDataTable();
                    gvDetails.DataSource = dtDetail;
                    gvDetails.DataBind();
                }
            }
        }
    }
}