using log4net;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Form_Result : System.Web.UI.Page
{
    protected static readonly ILog log = LogManager.GetLogger(typeof(Form_Result));
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            orgId.Value = Request.QueryString["orgId"];
            ConfigurationManager.AppSettings["urlExtension"] = SiloamConfig.Functions.GetValue("urlExtension").ToString();
            ConfigurationManager.AppSettings["urlViewerResult"] = SiloamConfig.Functions.GetValue("urlViewerResult").ToString();
        }
    }

    void getdataIframe()
    {
        //string localIPAdress = Helper.GetLocalIPAddress();
        string localIPAdress = ConfigurationManager.AppSettings["urlViewerResult"];

        string url = "http://" + localIPAdress + "/viewerresult/Form/result?idPatient=" + src_patient_id.Text;
        myLabRadIframe.Src = url;
    }
    
    protected void BtnSearchMR_Click(object sender, EventArgs e)
    {
        try
        {
            log.Info(LogLibrary.Logging("S", "src_patient_id_TextChanged", "", ""));
            string localIPAdress = ConfigurationManager.AppSettings["urlViewerResult"].ToString();

            if (src_patient_id.Text == "") { src_patient_id.Text = "0"; }

            log.Debug(LogLibrary.Logging("S", "getPatientId", "", ""));
            var dataPatient = clsResult.getPatientId(src_patient_id.Text, long.Parse(orgId.Value));
            log.Debug(LogLibrary.Logging("E", "getPatientId", "", ""));

            var Response = (JObject)JsonConvert.DeserializeObject<dynamic>(dataPatient.Result);
            var status = Response.Property("status").Value.ToString();

            if (status.ToString().ToLower() == "success")
            {
                var patientIdOwned = JsonConvert.DeserializeObject<PatientDataByMRResult>(dataPatient.Result);
                lblNama.Text = patientIdOwned.Patient.PatientName;
                lblDOB.Text = patientIdOwned.Patient.BirthDate;
                lblAge.Text = patientIdOwned.Patient.Age;
                lblReligion.Text = patientIdOwned.Patient.ReligionName;
                if (patientIdOwned.Patient.SexId == 1)
                {
                    ImageICMale.Visible = true;
                    ImageICFemale.Visible = false;
                }
                else if (patientIdOwned.Patient.SexId == 2)
                {
                    ImageICMale.Visible = false;
                    ImageICFemale.Visible = true;
                }

                divLine.Style.Remove("display");

                string url = localIPAdress + "/viewerresult/Form/result?idPatient=" + patientIdOwned.Patient.PatientId;
                myLabRadIframe.Src = url;
            }
            else
            {
                lblNama.Text = "";
                lblDOB.Text = "";
                lblAge.Text = "";
                lblReligion.Text = "";
                divLine.Style.Add("display", "none");

                string url = localIPAdress + "/viewerresult/Form/result?idPatient=12";
                myLabRadIframe.Src = url;
            }

            log.Info(LogLibrary.Logging("E", "src_patient_id_TextChanged", "", ""));
        }
        catch (Exception ex)
        {
            log.Error(LogLibrary.Error("E", "", ex.InnerException.Message));
        }
    }
}