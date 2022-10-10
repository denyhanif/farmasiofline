using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
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

public partial class Form_FormViewer_FormPengkajianAwal : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //Log.Info(LogConfig.LogStart());

            //HyperLink test = Master.FindControl("PrintFALink") as HyperLink;
            //test.Style.Add("background-color", "#D6DBFF");

            var registryflag = ConfigurationManager.AppSettings["registryflag"].ToString();
            if (registryflag == "1")
            {
                ConfigurationManager.AppSettings["urlPharmacy"] = SiloamConfig.Functions.GetValue("urlExtension").ToString();
            }

            DateTextboxStart.Text = DateTime.Now.AddMonths(-1).ToString("dd MMM yyyy");
            DateTextboxEnd.Text = DateTime.Now.ToString("dd MMM yyyy");

            datapasienheader.Style.Add("display", "none");
            datacontent.Style.Add("display", "none");

            datapasienheader2.Style.Add("display", "none");
            datacontent2.Style.Add("display", "none");

            //----------------------------------------

            //List<DoctorOrganization> DoctorOrganization = (List<DoctorOrganization>)Session[Helper.SessionDoctorOrganization];
            //DataTable dtdokter = Helper.ToDataTable(DoctorOrganization);
            //DDLDokterName.DataSource = dtdokter;
            //DDLDokterName.DataBind();

            HiddenFieldIPAddress.Value = GetLocalIPAddress();
            HiddenFieldOrgID.Value = Request.QueryString["OrgId"];
            HiddenFieldUserID.Value = Request.QueryString["Fullname"] == null ? "NULL" : Request.QueryString["Fullname"]; //Helper.GetFullName(this).ToString();

            //Log.Info(LogConfig.LogEnd());
        }

        DateTextboxStart.Attributes.Add("ReadOnly", "ReadOnly");
        DateTextboxEnd.Attributes.Add("ReadOnly", "ReadOnly");
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

    void setDataPasienFA(string namapasien, string tgllahir, string umur, string agama)
    {
        LblNamaPasien.Text = namapasien;
        LblTglLahir.Text = tgllahir;
        LblUmur.Text = umur;
        LblAgama.Text = agama;
    }

    void setDataPasienFA2(string namapasien, string tgllahir, string umur, string agama)
    {
        LblNamaPasien2.Text = namapasien;
        LblTglLahir2.Text = tgllahir;
        LblUmur2.Text = umur;
        LblAgama2.Text = agama;
    }

    void GetDataAdmissionFA(Int64 orgId, string localmrno, DateTime datestart, DateTime dateend, Int64 doctorId)
    {
        //Log.Info(LogConfig.LogStart());

        try
        {
            Dictionary<string, string> logParam = new Dictionary<string, string>
            {
                { "Organization_ID", orgId.ToString() },
                { "Mr_No", localmrno },
                { "Date_Start", datestart.ToString() },
                { "Date_End", dateend.ToString() },
                { "Doctor_ID", doctorId.ToString() }
            };
            //Log.Debug(LogConfig.LogStart("getAdmissionFirstAssesment", logParam));
            var getadmfa = clsAdmissionFA.getAdmissionFirstAssesment(orgId, localmrno, datestart, dateend, doctorId);
            var listadmfa = JsonConvert.DeserializeObject<ResultAdmissionFA>(getadmfa.Result.ToString());
            //Log.Debug(LogConfig.LogEnd("getAdmissionFirstAssesment", listadmfa.Status, listadmfa.Message));

            List<AdmisionFA> dataadmfa = new List<AdmisionFA>();
            List<AdmisionFA> dataadmfaddl = new List<AdmisionFA>();

            dataadmfa = listadmfa.list;

            AdmisionFA x = new AdmisionFA();
            x.DoctorName = "Semua Dokter";
            x.DoctorId = 0;
            dataadmfaddl.Add(x);
            dataadmfaddl.AddRange(listadmfa.list);

            if (dataadmfa.Count > 0)
            {
                DataTable dt = Helper.ToDataTable(dataadmfa);

                GridViewPengkajian.DataSource = dt;
                GridViewPengkajian.DataBind();

                setDataPasienFA(dataadmfa[0].PatientName, dataadmfa[0].BirthDate, dataadmfa[0].Age, dataadmfa[0].ReligionName);

                DataTable dtddl = Helper.ToDataTable(dataadmfaddl.Distinct().ToList());
                DDLDokterName.DataTextField = "DoctorName";
                DDLDokterName.DataValueField = "DoctorId";
                DDLDokterName.DataSource = dtddl;
                DDLDokterName.DataBind();

                img_noData_tab1.Visible = false;
                GridViewPengkajian.Visible = true;

                if (dataadmfa.Count >= 20)
                {
                    divalert.Visible = true;
                }
            }
            else
            {
                var Response = (JObject)JsonConvert.DeserializeObject<dynamic>(getadmfa.Result);
                var Status = Response.Property("status").Value.ToString();
                var Message = Response.Property("message").Value.ToString();

                GridViewPengkajian.DataSource = null;
                GridViewPengkajian.DataBind();

                setDataPasienFA("-", "-", "-", "-");

                img_noData_tab1.Visible = true;
                GridViewPengkajian.Visible = false;
            }
        }
        catch (Exception ex)
        {
            //Log.Error(LogConfig.LogError(ex.Message.ToString()), ex);
            throw ex;
            //error_box(ex);
        }

        //Log.Info(LogConfig.LogEnd());
    }

    void GetDataPatient(string localmrno, Int64 orgId)
    {
        //Log.Info(LogConfig.LogStart());

        try
        {

            Dictionary<string, string> logParam = new Dictionary<string, string>
            {
                { "MR_No", localmrno },
                { "Organization_ID", orgId.ToString() }
            };
            //Log.Debug(LogConfig.LogStart("getPatientData", logParam));
            var getpasienfa = clsAdmissionFA.getPatientData(localmrno, orgId);
            var listpasienfa = JsonConvert.DeserializeObject<resultPatientData>(getpasienfa.Result.ToString());
            //Log.Debug(LogConfig.LogEnd("getPatientData", listpasienfa.Status, listpasienfa.Message));

            PatientData datapasienfa = new PatientData();

            datapasienfa = listpasienfa.list;

            if (datapasienfa.PatientName != null)
            {
                setDataPasienFA2(datapasienfa.PatientName, datapasienfa.BirthDate, datapasienfa.Age, datapasienfa.ReligionName);
                if (listpasienfa.list.SexId == 1)
                {
                    ImageICMale2.Visible = true;
                    ImageICFemale2.Visible = false;
                }
                else if (listpasienfa.list.SexId == 2)
                {
                    ImageICMale2.Visible = false;
                    ImageICFemale2.Visible = true;
                }

                //string localIP = "10.83.254.38";
                string localIP = GetLocalIPAddress();
                //resumeMedis.Src = "http://" + localIP + "/viewer/form/PharmacyPatientHistory?PatientId=" + datapasienfa.patientId + "&PrintBy=" + Helper.GetFullName(this.Page);
                resumeMedis.Src = "http://" + localIP + "/viewer/Form/BigPatientHistory.aspx?idPatient=" + datapasienfa.PatientId + "&OrgId=" + HiddenFieldOrgID.Value + "&PrintBy=" + HiddenFieldUserID.Value + "&FlgCmpnd=" + "True";
                img_noData_tab2.Visible = false;
                datacontent2.Style.Add("display", "");

                //log.Info(LogLibrary.Logging("E", "GetDataWorklist", Helper.GetLoginUser(this), ""));
            }
            else
            {
                var Response = (JObject)JsonConvert.DeserializeObject<dynamic>(getpasienfa.Result);
                var Status = Response.Property("status").Value.ToString();
                var Message = Response.Property("message").Value.ToString();

                setDataPasienFA2("-", "-", "-", "-");

                img_noData_tab2.Visible = true;
                datacontent2.Style.Add("display", "none");
            }
        }
        catch (Exception ex)
        {
            //Log.Error(LogConfig.LogError(ex.Message.ToString()), ex);
            throw ex;
            //error_box(ex);
        }

        //Log.Info(LogConfig.LogEnd());
    }

    protected void BtnSearchMR_Click(object sender, EventArgs e)
    {
        //Log.Info(LogConfig.LogStart());

        Int64 ddlx = Int64.Parse(DDLDokterName.SelectedValue.ToString());

        datapasienheader.Style.Add("display", "");
        datacontent.Style.Add("display", "");
        GetDataAdmissionFA(Int64.Parse(HiddenFieldOrgID.Value), TBoxSearchMR.Text, DateTime.Parse(DateTextboxStart.Text.ToString()), DateTime.Parse(DateTextboxEnd.Text.ToString()), Int64.Parse(DDLDokterName.SelectedValue.ToString()));
        HFFlagTab.Value = "1";

        DDLDokterName.SelectedValue = ddlx.ToString();

        //Log.Info(LogConfig.LogEnd());
    }

    protected void BtnSearchMR2_Click(object sender, EventArgs e)
    {
        //Log.Info(LogConfig.LogStart());

        datapasienheader2.Style.Add("display", "");
        datacontent2.Style.Add("display", "");
        GetDataPatient(TBoxSearchMR2.Text, Int64.Parse(HiddenFieldOrgID.Value));
        HFFlagTab.Value = "2";

        //Log.Info(LogConfig.LogEnd());
    }
}