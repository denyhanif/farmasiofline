using log4net;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Form_FormViewer_MedicalResumePatient : System.Web.UI.Page
{
    protected static readonly ILog log = LogManager.GetLogger(typeof(Form_FormViewer_MedicalResumePatient));
    string log_username = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string username = Request.QueryString["username"];
            string org_id = Request.QueryString["org_id"];
            string ptn_id = Request.QueryString["ptn_id"];
            string adm_id = Request.QueryString["adm_id"];
            string enc_id = Request.QueryString["enc_id"];
            string pagesoap_id = Request.QueryString["pagesoap_id"];
            string headertype = Request.QueryString["headertype"];

            log_username = username;

            var registryflag = ConfigurationManager.AppSettings["registryflag"].ToString();
            if (registryflag == "1")
            {
                ConfigurationManager.AppSettings["urlPrescription"] = SiloamConfig.Functions.GetValue("urlTransaction").ToString();
                ConfigurationManager.AppSettings["DB_EMRExtension"] = SiloamConfig.Functions.GetValue("DB_EMRExtension").ToString();
            }

            //LoadMedicalResume("Tes", 2, 1099112, 2000006061902, "28ae150f-1bec-40c5-83ab-31ff612518ab");
            //LoadMedicalResume("Tes", 2, 1428601, 2000006069267, "bfd3518b-e7bd-4aa5-bada-4c6ffdfdb355");

            LoadHeader(Int64.Parse(org_id), Int64.Parse(adm_id), enc_id, headertype);
            LoadMedicalResume(username, Int64.Parse(org_id), Int64.Parse(ptn_id), Int64.Parse(adm_id), enc_id, pagesoap_id);
        }
    }

    protected dynamic MappingforGetdataSOAP(Int64 org_id, Int64 ptn_id, Int64 adm_id, string enc_id, string pagesoap_id)
    {
        log.Info(LogLibrary.Logging("S", "MappingforGetdataSOAP", log_username, ""));

        try
        {
            //set fa sesuai templatenya
            string templateid = pagesoap_id;
            if (templateid.ToUpper() == "00000000-0000-0000-0000-000000000000" || templateid.ToUpper() == "7CCD0A7E-9001-48FF-8052-ED07CF5716BB")
            {
                log.Debug(LogLibrary.Logging("S", "clsPatientHistory.getPatientHistoryData", log_username, "params : " + org_id + "," + ptn_id + "," + adm_id + "," + enc_id + "," + pagesoap_id));

                var JsonResult_PH = clsPatientHistory.getPatientHistoryData(org_id, ptn_id, adm_id, enc_id);
                var JsonData_PH = JsonConvert.DeserializeObject<ResultPatientHistoryEncounterData>(JsonResult_PH.Result.ToString());

                log.Debug(LogLibrary.Logging("E", "clsPatientHistory.getPatientHistoryData", log_username, ""));

                return JsonData_PH;
            }
            else if (templateid.ToUpper() == "711671B0-A2B3-4311-9B89-69C146FDAE3A") //EMERGENCY
            {
                log.Debug(LogLibrary.Logging("S", "clsPatientHistory.getPatientHistoryData", log_username, "params : " + org_id + "," + ptn_id + "," + adm_id + "," + enc_id + "," + pagesoap_id));

                var JsonResult_PH = clsPatientHistory.getPatientHistoryData(org_id, ptn_id, adm_id, enc_id);
                var JsonData_PH = JsonConvert.DeserializeObject<ResultPatientHistoryEncounterData>(JsonResult_PH.Result.ToString());

                log.Debug(LogLibrary.Logging("E", "clsPatientHistory.getPatientHistoryData", log_username, ""));

                return JsonData_PH;
            }
            else if (templateid.ToUpper() == "903E0F23-2C60-41F1-8C04-EB3E70D1E002") //OBGYN
            {
                log.Debug(LogLibrary.Logging("S", "clsPatientHistory.getPatientHistoryDataObgyn", log_username, "params : " + org_id + "," + ptn_id + "," + adm_id + "," + enc_id + "," + pagesoap_id));

                var JsonResult_PH_Obgyn = clsPatientHistory.getPatientHistoryDataObgyn(org_id, ptn_id, adm_id, enc_id);
                var JsonData_PH_Obgyn = JsonConvert.DeserializeObject<ResultPatientHistoryEncounterObgyn>(JsonResult_PH_Obgyn.Result.ToString());

                log.Debug(LogLibrary.Logging("E", "clsPatientHistory.getPatientHistoryDataObgyn", log_username, ""));

                return JsonData_PH_Obgyn;
            }
            else if (templateid.ToUpper() == "01D7A679-92EF-4A56-B040-1614B3C9EFAF") //PEDIATRIC
            {
                log.Debug(LogLibrary.Logging("S", "clsPatientHistory.getPatientHistoryDataPediatric", log_username, "params : " + org_id + "," + ptn_id + "," + adm_id + "," + enc_id + "," + pagesoap_id));

                var JsonResult_PH_Pediatric = clsPatientHistory.getPatientHistoryDataPediatric(org_id, ptn_id, adm_id, enc_id);
                var JsonData_PH_Pediatric = JsonConvert.DeserializeObject<ResultPatientHistoryEncounterPediatric>(JsonResult_PH_Pediatric.Result.ToString());

                log.Debug(LogLibrary.Logging("E", "clsPatientHistory.getPatientHistoryDataPediatric", log_username, ""));

                return JsonData_PH_Pediatric;
            }
            else
            {
                log.Debug(LogLibrary.Logging("S", "clsPatientHistory.getPatientHistoryData", log_username, "params : " + org_id + "," + ptn_id + "," + adm_id + "," + enc_id + "," + pagesoap_id));

                var JsonResult_PH = clsPatientHistory.getPatientHistoryData(org_id, ptn_id, adm_id, enc_id);
                var JsonData_PH = JsonConvert.DeserializeObject<ResultPatientHistoryEncounterData>(JsonResult_PH.Result.ToString());

                log.Debug(LogLibrary.Logging("E", "clsPatientHistory.getPatientHistoryData", log_username, ""));

                return JsonData_PH;
            }
        }
        catch (Exception ex)
        {
            log.Error(LogLibrary.Error("MappingforGetdataSOAP", log_username, ex.Message.ToString()));
        }

        log.Info(LogLibrary.Logging("E", "MappingforGetdataSOAP", log_username, ""));

        return null;
    }

    protected void LoadMedicalResume(string username, Int64 org_id, Int64 ptn_id, Int64 adm_id, string enc_id, string pagesoap_id)
    {
        log.Info(LogLibrary.Logging("S", "LoadMedicalResume", log_username, ""));

        try
        {      
            //var JsonResult_PH = clsPatientHistory.getPatientHistoryData(org_id, ptn_id, adm_id, enc_id);
            //var JsonData_PH = JsonConvert.DeserializeObject<ResultPatientHistoryEncounterData>(JsonResult_PH.Result.ToString());

            var JsonData_PH = MappingforGetdataSOAP(org_id, ptn_id, adm_id, enc_id, pagesoap_id);

            PatientHistoryHeader HeaderData = JsonData_PH.list.historyheader;
            PatientHistoryAnamnesis AnamnesisData = JsonData_PH.list.historyanamnesis;
            List<PatientHistoryDiagnosis> ListDiagnosisData = JsonData_PH.list.historydiagnosis;
            List<PatientHistoryIllness> ListIllnessData = JsonData_PH.list.historyillness;
            List<PatientHistoryPhysicalExam> ListPhysicalExamData = JsonData_PH.list.historyphysicalexam;
            List<PatientHistoryPlanning> ListPlanningData = JsonData_PH.list.historyplanning;
            List<PatientHistoryCPOE> ListCPOEData = JsonData_PH.list.historycpoe;
            List<PatientHistoryProcedureDiagnostic> ListDiagnosticProcedureData = JsonData_PH.list.historyprocedurediagnostic;

            List<PatientHistoryPrescription> ListPrescriptionData = JsonData_PH.list.historyprescription;
            List<CompoundHeaderSoap> listpresracikanheader = JsonData_PH.list.compound_header;
            List<CompoundDetailSoap> listpresracikandetail = JsonData_PH.list.compound_detail;

            //TITLE
            lbl_titlewithdate.Text = HeaderData.admissionDate.ToString("dddd, dd MMMM yyyy") + " - " +
                                     HeaderData.organizationCode + " - " +
                                     HeaderData.admissionTypeName + " - " +
                                     HeaderData.doctorName;
            
            //DATA

            //Chief COmplaint & Anamnesis
            lbl_chiefcomplaint.Text = ListDiagnosisData.Find(x => x.mappingName == "PATIENT COMPLAINT").remarks.Replace("\n", "<br />") == "" ? "-" : ListDiagnosisData.Find(x => x.mappingName == "PATIENT COMPLAINT").remarks.Replace("\n", "<br />");
            lbl_anamnesis.Text = AnamnesisData.remarks.Replace("\n", "<br />") == "" ? "-" : AnamnesisData.remarks.Replace("\n", "<br />");
            string txt_hamil = ListDiagnosisData.Find(x => x.mappingName == "PREGNANCY").value;
            lbl_hamil.Text = txt_hamil == "true" ? "Ya" : txt_hamil == "false" ? "Tidak" : "-";
            string txt_menyusui = ListDiagnosisData.Find(x => x.mappingName == "BREASTFEEDING").value;
            lbl_menyusui.Text = txt_menyusui == "true" ? "Ya" : txt_menyusui == "false" ? "Tidak" : "-";

            //Medication & Allergies
            List<PatientHistoryIllness> routineMed = ListIllnessData.FindAll(x => x.type.Equals("RoutineMedication"));
            if (routineMed.Count != 0)
            {
                DataTable tableRoutineMed = Helper.ToDataTable(routineMed);
                RepeaterRoutineMed.DataSource = tableRoutineMed;
                RepeaterRoutineMed.DataBind();
            }
            else
            {
                RepeaterRoutineMed.Visible = false;
                Labelroutinempty.Visible = true;
            }

            List<PatientHistoryIllness> drugAlergy = ListIllnessData.FindAll(x => x.type.Equals("DrugAllergy"));
            if (drugAlergy.Count != 0)
            {
                DataTable tableDrugAllergy = Helper.ToDataTable(drugAlergy);
                RepeaterDrugAllergy.DataSource = tableDrugAllergy;
                RepeaterDrugAllergy.DataBind();
            }
            else
            {
                RepeaterDrugAllergy.Visible = false;
                Labeldrugempty.Visible = true;
            }

            List<PatientHistoryIllness> foodAlergy = ListIllnessData.FindAll(x => x.type.Equals("FoodAllergy"));
            if (foodAlergy.Count != 0)
            {
                DataTable tableFoodAllergy = Helper.ToDataTable(foodAlergy);
                RepeaterFoodAllergy.DataSource = tableFoodAllergy;
                RepeaterFoodAllergy.DataBind();
            }
            else
            {
                RepeaterFoodAllergy.Visible = false;
                Labelfoodempty.Visible = true;
            }

            List<PatientHistoryIllness> otherAlergy = ListIllnessData.FindAll(x => x.type.Equals("OtherAllergy"));
            if (otherAlergy.Count != 0)
            {
                DataTable tableOtherAllergy = Helper.ToDataTable(otherAlergy);
                RepeaterOtherAllergy.DataSource = tableOtherAllergy;
                RepeaterOtherAllergy.DataBind();
            }
            else
            {
                RepeaterOtherAllergy.Visible = false;
                Labelotherempty.Visible = true;
            }

            //Illness History
            List<PatientHistoryIllness> surgeryHistory = ListIllnessData.FindAll(x => x.type.Equals("Surgery"));
            if (surgeryHistory.Count != 0)
            {
                DataTable tableSurgeryHistory = Helper.ToDataTable(surgeryHistory);
                RepeaterSurgeryHistory.DataSource = tableSurgeryHistory;
                RepeaterSurgeryHistory.DataBind();
            }
            else
            {
                RepeaterSurgeryHistory.Visible = false;
                LabelSurgeryHistoryEmpty.Visible = true;
            }

            List<PatientHistoryIllness> diseaseHistory = ListIllnessData.FindAll(x => x.type.Equals("PersonalDisease"));
            if (diseaseHistory.Count != 0)
            {
                DataTable tableDiseaseHistory = Helper.ToDataTable(diseaseHistory);
                RepeaterDiseaseHistory.DataSource = tableDiseaseHistory;
                RepeaterDiseaseHistory.DataBind();
            }
            else
            {
                RepeaterDiseaseHistory.Visible = false;
                LabelDiseaseHistoryEmpty.Visible = true;
            }

            List<PatientHistoryIllness> familyDisease = ListIllnessData.FindAll(x => x.type.Equals("FamilyDisease"));
            if (familyDisease.Count != 0)
            {
                DataTable tableFamilyDisease = Helper.ToDataTable(familyDisease);
                RepeaterFamilyDisease.DataSource = tableFamilyDisease;
                RepeaterFamilyDisease.DataBind();
            }
            else
            {
                RepeaterFamilyDisease.Visible = false;
                LabelFamilyDiseaseEmpty.Visible = true;
            }

            //Endemic Area Visitation
            lbl_endemicarea.Text = ListDiagnosisData.Find(x => x.mappingName == "ENDEMIC AREA").remarks.Replace("\n", "<br />") == "" ? "-" : ListDiagnosisData.Find(x => x.mappingName == "ENDEMIC AREA").remarks.Replace("\n", "<br />");

            List<PatientHistoryDiagnosis> screeningInfectius = ListDiagnosisData.FindAll(x => x.mappingName.Equals("INFECTIOUS DISEASE SYMPTOM"));
            if (screeningInfectius.Count != 0)
            {
                DataTable tableScreeningInfectius = Helper.ToDataTable(screeningInfectius);
                RepeaterScreeningInfectius.DataSource = tableScreeningInfectius;
                RepeaterScreeningInfectius.DataBind();
            }
            else
            {
                RepeaterScreeningInfectius.Visible = false;
                LabelScreeningInfectiusEmpty.Visible = true;
            }

            //Nutrition & Fasting
            lbl_nutritionproblem.Text = ListDiagnosisData.Find(x => x.mappingName == "NUTRITION").remarks.Replace("\n", "<br />") == "" ? "-" : ListDiagnosisData.Find(x => x.mappingName == "NUTRITION").remarks.Replace("\n", "<br />");
            lbl_fasting.Text = ListDiagnosisData.Find(x => x.mappingName == "FASTING").remarks.Replace("\n", "<br />") == "" ? "-" : ListDiagnosisData.Find(x => x.mappingName == "FASTING").remarks.Replace("\n", "<br />");

            //Physical Examination
            lbl_eye.Text = ListPhysicalExamData.Find(x => x.mappingName == "GCS EYE").value.Replace("\n", "<br />") == "" ? "-" : ListPhysicalExamData.Find(x => x.mappingName == "GCS EYE").value.Replace("\n", "<br />");
            lbl_move.Text = ListPhysicalExamData.Find(x => x.mappingName == "GCS MOVE").value.Replace("\n", "<br />") == "" ? "-" : ListPhysicalExamData.Find(x => x.mappingName == "GCS MOVE").value.Replace("\n", "<br />");
            lbl_verbal.Text = ListPhysicalExamData.Find(x => x.mappingName == "GCS VERBAL").value.Replace("\n", "<br />") == "" ? "-" : ListPhysicalExamData.Find(x => x.mappingName == "GCS VERBAL").value.Replace("\n", "<br />");
            string score = "-";
            if (lbl_verbal.Text != "A" && lbl_verbal.Text != "T")
            {
                if (lbl_eye.Text != "-" && lbl_move.Text != "-" && lbl_verbal.Text != "-")
                {
                    score = (int.Parse(lbl_eye.Text) + int.Parse(lbl_move.Text) + int.Parse(lbl_verbal.Text)).ToString();
                }
            }
            lbl_score.Text = score;

            lbl_painscale.Text = ListPhysicalExamData.Find(x => x.mappingName == "PAIN SCALE").value.Replace("\n", "<br />") == "" ? "-" : ListPhysicalExamData.Find(x => x.mappingName == "PAIN SCALE").value.Replace("\n", "<br />");

            string bp_low = ListPhysicalExamData.Find(x => x.mappingName == "BLOOD PRESSURE LOW").value.Replace("\n", "<br />") == "" ? "-" : ListPhysicalExamData.Find(x => x.mappingName == "BLOOD PRESSURE LOW").value.Replace("\n", "<br />");
            string bp_hight = ListPhysicalExamData.Find(x => x.mappingName == "BLOOD PRESSURE HIGH").value.Replace("\n", "<br />") == "" ? "-" : ListPhysicalExamData.Find(x => x.mappingName == "BLOOD PRESSURE HIGH").value.Replace("\n", "<br />");
            string bp = "-";
            if (bp_low != "-" || bp_hight != "-")
            {
                bp = bp_low + "/" + bp_hight + " mmHg";
            }
            lbl_bloodpressure.Text = bp;
            lbl_pulserate.Text = ListPhysicalExamData.Find(x => x.mappingName == "PULSE RATE").value.Replace("\n", "<br />") == "" ? "-" : ListPhysicalExamData.Find(x => x.mappingName == "PULSE RATE").value.Replace("\n", "<br />") + " x/mnt";
            lbl_respiratoryrate.Text = ListPhysicalExamData.Find(x => x.mappingName == "RESPIRATORY RATE").value.Replace("\n", "<br />") == "" ? "-" : ListPhysicalExamData.Find(x => x.mappingName == "RESPIRATORY RATE").value.Replace("\n", "<br />") + " x/mnt";
            lbl_spo2.Text = ListPhysicalExamData.Find(x => x.mappingName == "SPO2").value.Replace("\n", "<br />") == "" ? "-" : ListPhysicalExamData.Find(x => x.mappingName == "SPO2").value.Replace("\n", "<br />") + " %";
            lbl_temperature.Text = ListPhysicalExamData.Find(x => x.mappingName == "TEMPERATURE").value.Replace("\n", "<br />") == "" ? "-" : ListPhysicalExamData.Find(x => x.mappingName == "TEMPERATURE").value.Replace("\n", "<br />") + " °C";
            lbl_weight.Text = ListPhysicalExamData.Find(x => x.mappingName == "WEIGHT").value.Replace("\n", "<br />") == "" ? "-" : ListPhysicalExamData.Find(x => x.mappingName == "WEIGHT").value.Replace("\n", "<br />") + " kg";
            lbl_height.Text = ListPhysicalExamData.Find(x => x.mappingName == "HEIGHT").value.Replace("\n", "<br />") == "" ? "-" : ListPhysicalExamData.Find(x => x.mappingName == "HEIGHT").value.Replace("\n", "<br />") + " cm";
            lbl_headcirc.Text = ListPhysicalExamData.Find(x => x.mappingName == "LINGKAR KEPALA").value.Replace("\n", "<br />") == "" ? "-" : ListPhysicalExamData.Find(x => x.mappingName == "LINGKAR KEPALA").value.Replace("\n", "<br />") + " cm";

            lbl_mentalstatus.Text = ListPhysicalExamData.Find(x => x.mappingName == "MENTAL STATUS").value.Replace("\n", "<br />") == "" ? "-" : ListPhysicalExamData.Find(x => x.mappingName == "MENTAL STATUS").value.Replace("\n", "<br />");
            lbl_conslevel.Text = ListPhysicalExamData.Find(x => x.mappingName == "CONSCIOUSNESS LEVEL").value.Replace("\n", "<br />") == "" ? "-" : ListPhysicalExamData.Find(x => x.mappingName == "CONSCIOUSNESS LEVEL").value.Replace("\n", "<br />");

            lbl_soapothers.Text = ListPhysicalExamData.Find(x => x.mappingName == "OTHERS").remarks.Replace("\n", "<br />") == "" ? "-" : ListPhysicalExamData.Find(x => x.mappingName == "OTHERS").remarks.Replace("\n", "<br />");

            lbl_diagnosis.Text = ListDiagnosisData.Find(x => x.mappingName == "PRIMARY DIAGNOSIS").remarks.Replace("\n", "<br />") == "" ? "-" : ListDiagnosisData.Find(x => x.mappingName == "PRIMARY DIAGNOSIS").remarks.Replace("\n", "<br />");

            lbl_planproc.Text = ListPlanningData.Find(x => x.mappingName == "PLANNING PROCEDURE").remarks.Replace("\n", "<br />") == "" ? "-" : ListPlanningData.Find(x => x.mappingName == "PLANNING PROCEDURE").remarks.Replace("\n", "<br />");

            lbl_procnotes.Text = HeaderData.procedureNotes == "" ? "-" : HeaderData.procedureNotes;

            lbl_procresult.Text = ListPlanningData.Find(x => x.mappingName == "PROCEDURE RESULT").remarks.Replace("\n", "<br />") == "" ? "-" : ListPlanningData.Find(x => x.mappingName == "PROCEDURE RESULT").remarks.Replace("\n", "<br />");

            lbl_drugsnotes.Text = ListPlanningData.Find(x => x.mappingName == "DOCTOR NOTES").remarks.Replace("\n", "<br />") == "" ? "" : "<b>Doctor Notes : </b>" + ListPlanningData.Find(x => x.mappingName == "DOCTOR NOTES").remarks.Replace("\n", "<br />");

            lbl_drugsnotespharmacy.Text = HeaderData.PharmacyNotes == "" ? "" : "<b>Pharmacy Notes : </b>" + HeaderData.PharmacyNotes;

            List<PatientHistoryCPOE> LabHistory = ListCPOEData.FindAll(x => x.template_name.Equals("LAB") && !x.is_future_order);
            if (LabHistory.Count != 0)
            {
                DataTable tableLabHistory = Helper.ToDataTable(LabHistory);
                RepeaterLab.DataSource = tableLabHistory;
                RepeaterLab.DataBind();
            }
            else
            {
                RepeaterLab.Visible = false;
                lbl_labitem_empty.Visible = true;
            }

            if (ListPlanningData.Where(x => x.mappingName == "PLANNING OTHER ORDER LAB").Any())
            {
                lbl_otherslabitem.Text = ListPlanningData.Find(x => x.mappingName == "PLANNING OTHER ORDER LAB").remarks.Replace("\n", "<br />") == "" ? "-" : ListPlanningData.Find(x => x.mappingName == "PLANNING OTHER ORDER LAB").remarks.Replace("\n", "<br />");
            }

            List<PatientHistoryCPOE> RadHistory = ListCPOEData.FindAll(x => x.template_name.Equals("RAD") && !x.is_future_order);
            if (RadHistory.Count != 0)
            {
                DataTable tableRadHistory = Helper.ToDataTable(RadHistory);
                RepeaterRad.DataSource = tableRadHistory;
                RepeaterRad.DataBind();
            }
            else
            {
                RepeaterRad.Visible = false;
                lbl_raditem_empty.Visible = true;
            }

            if (ListPlanningData.Where(x => x.mappingName == "PLANNING OTHER ORDER RAD").Any())
            {
                lbl_othersraditem.Text = ListPlanningData.Find(x => x.mappingName == "PLANNING OTHER ORDER RAD").remarks.Replace("\n", "<br />") == "" ? "-" : ListPlanningData.Find(x => x.mappingName == "PLANNING OTHER ORDER RAD").remarks.Replace("\n", "<br />");
            }

            List<PatientHistoryCPOE> LabHistoryFO = ListCPOEData.FindAll(x => x.template_name.Equals("LAB") && x.is_future_order);
            if (LabHistoryFO.Count != 0)
            {
                lblLab_FO_Date.Text = LabHistoryFO[0].future_order_date.ToString("dd MMM yyyy");
                DataTable tableLabHistory = Helper.ToDataTable(LabHistoryFO);
                RepeaterLabFO.DataSource = tableLabHistory;
                RepeaterLabFO.DataBind();
            }
            else
            {
                RepeaterLabFO.Visible = false;
                lbl_labitem_emptyfo.Visible = true;
            }

            if (ListPlanningData.Where(x => x.mappingName == "PLANNING OTHER FUTURE ORDER LAB").Any())
            {
                lblLab_FO_Date_other.Text = ListPlanningData.Find(x => x.mappingName == "PLANNING OTHER FUTURE ORDER LAB").value == "" ? "-" : ListPlanningData.Find(x => x.mappingName == "PLANNING OTHER FUTURE ORDER LAB").value;
                lbl_otherslabitemfo.Text = ListPlanningData.Find(x => x.mappingName == "PLANNING OTHER FUTURE ORDER LAB").remarks.Replace("\n", "<br />") == "" ? "-" : ListPlanningData.Find(x => x.mappingName == "PLANNING OTHER FUTURE ORDER LAB").remarks.Replace("\n", "<br />");
            }


            List<PatientHistoryCPOE> RadHistoryFO = ListCPOEData.FindAll(x => x.template_name.Equals("RAD") && x.is_future_order);
            if (RadHistoryFO.Count != 0)
            {
                lblRad_FO_Date.Text = RadHistoryFO[0].future_order_date.ToString("dd MMM yyyy");
                DataTable tableRadHistory = Helper.ToDataTable(RadHistoryFO);
                RepeaterRadFO.DataSource = tableRadHistory;
                RepeaterRadFO.DataBind();
            }
            else
            {
                RepeaterRadFO.Visible = false;
                lbl_raditem_emptyfo.Visible = true;
            }

            if(ListPlanningData.Where(x => x.mappingName == "PLANNING OTHER FUTURE ORDER RAD").Any())
            {
                lblRad_FO_Date_other.Text = ListPlanningData.Find(x => x.mappingName == "PLANNING OTHER FUTURE ORDER RAD").value == "" ? "-" : ListPlanningData.Find(x => x.mappingName == "PLANNING OTHER FUTURE ORDER RAD").value;
                lbl_othersraditemfo.Text = ListPlanningData.Find(x => x.mappingName == "PLANNING OTHER FUTURE ORDER RAD").remarks.Replace("\n", "<br />") == "" ? "-" : ListPlanningData.Find(x => x.mappingName == "PLANNING OTHER FUTURE ORDER RAD").remarks.Replace("\n", "<br />");
            }



            if(ListDiagnosticProcedureData != null)
            {
                List<PatientHistoryProcedureDiagnostic> DiagnosticHistory = ListDiagnosticProcedureData.FindAll(x => x.salesItemType.ToUpper().Equals("DIAGNOSTIC") && !x.isFutureOrder);
                if (DiagnosticHistory.Count != 0)
                {
                    DataTable tableDisgnosticHistory = Helper.ToDataTable(DiagnosticHistory);
                    RepeaterDiagnostic.DataSource = tableDisgnosticHistory;
                    RepeaterDiagnostic.DataBind();
                }
                else
                {
                    RepeaterDiagnostic.Visible = false;
                    lbl_diagnosticitem_empty.Visible = true;
                }

                if (ListPlanningData.Where(x => x.mappingName == "PLANNING OTHER DIAGNOSTIC").Any())
                {
                    lbl_othersdiagnosticitem.Text = ListPlanningData.Find(x => x.mappingName == "PLANNING OTHER DIAGNOSTIC").remarks.Replace("\n", "<br />") == "" ? "-" : ListPlanningData.Find(x => x.mappingName == "PLANNING OTHER DIAGNOSTIC").remarks.Replace("\n", "<br />");
                }

                List<PatientHistoryProcedureDiagnostic> ProcedureHistory = ListDiagnosticProcedureData.FindAll(x => x.salesItemType.ToUpper().Equals("PROCEDURE") && !x.isFutureOrder);
                if (ProcedureHistory.Count != 0)
                {
                    DataTable tableProcedureHistory = Helper.ToDataTable(ProcedureHistory);
                    RepeaterProcedure.DataSource = tableProcedureHistory;
                    RepeaterProcedure.DataBind();
                }
                else
                {
                    RepeaterProcedure.Visible = false;
                    lbl_procedureitem_empty.Visible = true;
                }

                if (ListPlanningData.Where(x => x.mappingName == "PLANNING OTHER PROCEDURE").Any())
                {
                    lbl_othersprocedureitem.Text = ListPlanningData.Find(x => x.mappingName == "PLANNING OTHER PROCEDURE").remarks.Replace("\n", "<br />") == "" ? "-" : ListPlanningData.Find(x => x.mappingName == "PLANNING OTHER PROCEDURE").remarks.Replace("\n", "<br />");
                }

                List<PatientHistoryProcedureDiagnostic> DiagnosticHistoryFO = ListDiagnosticProcedureData.FindAll(x => x.salesItemType.ToUpper().Equals("DIAGNOSTIC") && x.isFutureOrder);
                if (DiagnosticHistoryFO.Count != 0)
                {
                    lblDiagnostic_FO_Date.Text = DiagnosticHistoryFO[0].futureOrderDate.ToString("dd MMM yyyy");
                    DataTable tableDisgnosticHistory = Helper.ToDataTable(DiagnosticHistoryFO);
                    RepeaterDiagnosticFO.DataSource = tableDisgnosticHistory;
                    RepeaterDiagnosticFO.DataBind();
                }
                else
                {
                    RepeaterDiagnosticFO.Visible = false;
                    lbl_diagnosticitem_emptyfo.Visible = true;
                }

                if (ListPlanningData.Where(x => x.mappingName == "PLANNING OTHER FUTURE DIAGNOSTIC").Any())
                {
                    lblDiagnostic_FO_Date_other.Text = ListPlanningData.Find(x => x.mappingName == "PLANNING OTHER FUTURE DIAGNOSTIC").value == "" ? "-" : ListPlanningData.Find(x => x.mappingName == "PLANNING OTHER FUTURE DIAGNOSTIC").value;
                    lbl_othersdiagnosticitemfo.Text = ListPlanningData.Find(x => x.mappingName == "PLANNING OTHER FUTURE DIAGNOSTIC").remarks.Replace("\n", "<br />") == "" ? "-" : ListPlanningData.Find(x => x.mappingName == "PLANNING OTHER FUTURE DIAGNOSTIC").remarks.Replace("\n", "<br />");
                }

                List<PatientHistoryProcedureDiagnostic> ProcedureHistoryFO = ListDiagnosticProcedureData.FindAll(x => x.salesItemType.ToUpper().Equals("PROCEDURE") && x.isFutureOrder);
                if (ProcedureHistoryFO.Count != 0)
                {
                    lblProcedure_FO_Date.Text = ProcedureHistoryFO[0].futureOrderDate.ToString("dd MMM yyyy");
                    DataTable tableProcedureHistory = Helper.ToDataTable(ProcedureHistoryFO);
                    RepeaterProcedureFO.DataSource = tableProcedureHistory;
                    RepeaterProcedureFO.DataBind();
                }
                else
                {
                    RepeaterProcedureFO.Visible = false;
                    lbl_procedureitem_emptyfo.Visible = true;
                }

                if (ListPlanningData.Where(x => x.mappingName == "PLANNING OTHER FUTURE PROCEDURE").Any())
                {
                    lblProcedure_FO_Date_other.Text = ListPlanningData.Find(x => x.mappingName == "PLANNING OTHER FUTURE PROCEDURE").value == "" ? "-" : ListPlanningData.Find(x => x.mappingName == "PLANNING OTHER FUTURE PROCEDURE").value;
                    lbl_othersprocedureitemfo.Text = ListPlanningData.Find(x => x.mappingName == "PLANNING OTHER FUTURE PROCEDURE").remarks.Replace("\n", "<br />") == "" ? "-" : ListPlanningData.Find(x => x.mappingName == "PLANNING OTHER FUTURE PROCEDURE").remarks.Replace("\n", "<br />");
                }

            }


            //=============================================================================================================

            //DRUGS
            List<PatientHistoryPrescription> ListDrugsDoctor = (from a in ListPrescriptionData
                                                          where a.compoundName == ""
                                                          && a.isConsumables == false
                                                          && a.IsDoctor == 1
                                                          //&& a.IsAdditional == 0
                                                          orderby a.salesItemName
                                                          select a).ToList();
            if (ListDrugsDoctor.Count == 0)
            {
                div_drugs_doctor.Visible = false;
            }
            ViewDrugsForm.initializevalue(ListDrugsDoctor);

            List<PatientHistoryPrescription> ListDrugsPharmacy = (from a in ListPrescriptionData
                                                                where a.compoundName == ""
                                                                && a.isConsumables == false
                                                                && a.IsDoctor == 0
                                                                //&& a.IsAdditional == 0
                                                                orderby a.salesItemName
                                                                select a).ToList();
            if (ListDrugsPharmacy.Count == 0)
            {
                div_drugs_pharmacy.Visible = false;
            }
            ViewDrugsFormPharmacy.initializevalue(ListDrugsPharmacy);

            //CONSUMABLES
            List<PatientHistoryPrescription> ListConsDoctor = (from a in ListPrescriptionData
                                                          where a.compoundName == ""
                                                          && a.isConsumables == true
                                                          && a.IsDoctor == 1
                                                          //&& a.IsAdditional == 0
                                                          orderby a.salesItemName
                                                          select a).ToList();
            if (ListConsDoctor.Count == 0)
            {
                div_cons_doctor.Visible = false;
            }
            ViewConsForm.initializevalue(ListConsDoctor);

            List<PatientHistoryPrescription> ListConsPharmacy = (from a in ListPrescriptionData
                                                               where a.compoundName == ""
                                                               && a.isConsumables == true
                                                               && a.IsDoctor == 0
                                                               //&& a.IsAdditional == 0
                                                               orderby a.salesItemName
                                                               select a).ToList();
            if (ListConsPharmacy.Count == 0)
            {
                div_cons_pharmacy.Visible = false;
            }
            ViewConsFormPharmacy.initializevalue(ListConsPharmacy);

            //RACIKAN
            if (listpresracikanheader.Count == 0)
            {
                div_racikan_doctor.Visible = false;
            }
            ViewRacikanForm.initializevalue(listpresracikanheader, listpresracikandetail);


            //SPECIALTY

            //set sesuai templatenya
            string templateid = pagesoap_id;
            if (templateid.ToUpper() == "00000000-0000-0000-0000-000000000000" || templateid.ToUpper() == "7CCD0A7E-9001-48FF-8052-ED07CF5716BB")
            {

            }
            else if (templateid.ToUpper() == "711671B0-A2B3-4311-9B89-69C146FDAE3A") //EMERGENCY
            {
                List<PatientHistoryPlanning> historydata = JsonData_PH.list.historyplanning;
                LoadEmergencyData(historydata);
                div_emergency1.Visible = true;
            }
            else if (templateid.ToUpper() == "903E0F23-2C60-41F1-8C04-EB3E70D1E002") //OBGYN
            {
                List<historypregnancydataSOAP> historydatakehamilan = JsonData_PH.list.historypregnancydata;
                LoadObgynData(historydatakehamilan);

                div_obgyn1.Visible = true;
            }
            else if (templateid.ToUpper() == "01D7A679-92EF-4A56-B040-1614B3C9EFAF") //PEDIATRIC
            {
                List<historypediatricdataSOAP> historydatapediatric = JsonData_PH.list.historypediatricdata;
                LoadPediatricData(historydatapediatric);

                div_pediatric1.Visible = true;
            }

        }
        catch (Exception ex)
        {
            log.Error(LogLibrary.Error("LoadMedicalResume", log_username, ex.Message.ToString()));
        }

        log.Info(LogLibrary.Logging("E", "LoadMedicalResume", log_username, ""));
    }

    protected void LoadHeader(Int64 org_id, Int64 adm_id, string enc_id, string headertype)
    {
        log.Info(LogLibrary.Logging("S", "LoadHeader", log_username, ""));

        try
        {
            log.Debug(LogLibrary.Logging("S", "clsPatientHistory.getHeaderForPrint", log_username, "params : " + org_id + "," + adm_id + "," + enc_id + "," + headertype));

            DataSet result = clsPatientHistory.getHeaderForPrint(org_id, adm_id, Guid.Parse(enc_id));
            DataTable headerData = result.Tables[0];

            log.Debug(LogLibrary.Logging("E", "clsPatientHistory.getHeaderForPrint", log_username, ""));

            if (headertype == "1")
            {
                lbl_mrno_header.Text = headerData.Rows[0]["LocalMrNo"].ToString();
                lbl_name_header.Text = headerData.Rows[0]["PatientName"].ToString();
                lbl_dob_header.Text = headerData.Rows[0]["BirthDate"].ToString();
                lbl_sex_header.Text = headerData.Rows[0]["Gender"].ToString();
                lbl_doctor_header.Text = headerData.Rows[0]["DoctorName"].ToString();
                lbl_admno_header.Text = headerData.Rows[0]["Admission"].ToString();
                lbl_payer_header.Text = headerData.Rows[0]["PayerName"].ToString();

                div_header_1.Visible = true;
            }
            else if (headertype == "2")
            {
                lbl_name_header2.Text = headerData.Rows[0]["PatientName"].ToString();
                lbl_admno_header2.Text = headerData.Rows[0]["Admission"].ToString();
                lbl_dob_header2.Text = headerData.Rows[0]["BirthDate"].ToString();
                lbl_sex_header2.Text = headerData.Rows[0]["Gender"].ToString();

                div_header_2.Visible = true;
            }
        }
        catch (Exception ex)
        {
            log.Error(LogLibrary.Error("LoadHeader", log_username, ex.Message.ToString()));
        }

        log.Info(LogLibrary.Logging("E", "LoadHeader", log_username, ""));

    }

    public void LoadEmergencyData(List<PatientHistoryPlanning> resumedatatriage)
    {
        log.Info(LogLibrary.Logging("S", "LoadEmergencyData", log_username, ""));

        try
        {
            lbl_skortriage.Text = (from a in resumedatatriage
                                   where a.mappingId == Guid.Parse("EB3330BD-D413-4B70-999C-0D7AB29FBE36")
                                   select a.value == "" ? "-" : a.value).SingleOrDefault().ToString();

            lbl_traumatriage.Text = (from a in resumedatatriage
                                     where a.mappingId == Guid.Parse("64B06F14-6480-46DA-8846-9CAC5A499748")
                                     select a.value == "" ? "-" : a.value).SingleOrDefault().ToString();

            var txt_pasiendatang = resumedatatriage.Find(x => x.mappingId == Guid.Parse("D2D42792-B16F-472B-B5CB-428980C5003E"));
            if (txt_pasiendatang != null)
            {
                if (txt_pasiendatang.value.ToLower() == "")
                {
                    lbl_pasiendatang.Text = "-";
                }
                else if (txt_pasiendatang.value.ToLower() == "sendiri")
                {
                    lbl_pasiendatang.Text = txt_pasiendatang.value;
                }
                else if (txt_pasiendatang.value.ToLower() == "dirujuk")
                {
                    lbl_pasiendatang.Text = txt_pasiendatang.value + " oleh " + txt_pasiendatang.remarks;
                }
                else if (txt_pasiendatang.value.ToLower() == "lain-lain")
                {
                    lbl_pasiendatang.Text = txt_pasiendatang.remarks;
                }
            }

            lbl_keadaanumum.Text = (from a in resumedatatriage
                                    where a.mappingId == Guid.Parse("8C1799E0-C793-4918-A850-1B9BE72359CF")
                                    select a.value == "" ? "-" : a.value).SingleOrDefault().ToString();

            lbl_airway.Text = (from a in resumedatatriage
                               where a.mappingId == Guid.Parse("220F9B0A-4F91-4982-BED3-CA2DDEB1884F")
                               select a.value == "" ? "-" : a.value).SingleOrDefault().ToString();

            lbl_breathing.Text = (from a in resumedatatriage
                                  where a.mappingId == Guid.Parse("DF853881-EADC-4DA8-9140-960F962535E3")
                                  select a.value == "" ? "-" : a.value).SingleOrDefault().ToString();

            lbl_circulation.Text = (from a in resumedatatriage
                                    where a.mappingId == Guid.Parse("576416E4-AEF9-45BC-A7E0-AE3CEDF6EE94")
                                    select a.value == "" ? "-" : a.value).SingleOrDefault().ToString();

            lbl_disability.Text = (from a in resumedatatriage
                                   where a.mappingId == Guid.Parse("058F59BA-7FA9-443A-994A-E848F4FAEE7F")
                                   select a.value == "" ? "-" : a.value).SingleOrDefault().ToString();
        }
        catch (Exception ex)
        {
            log.Error(LogLibrary.Error("LoadEmergencyData", log_username, ex.Message.ToString()));
        }

        log.Info(LogLibrary.Logging("E", "LoadEmergencyData", log_username, ""));
    }

    public void LoadObgynData(List<historypregnancydataSOAP> resumedatakehamilan)
    {
        log.Info(LogLibrary.Logging("S", "LoadObgynData", log_username, ""));

        try
        {
            //----GPA
            var txt_g = resumedatakehamilan.Find(x => x.pregnancy_data_type == "G");
            if (txt_g != null)
            {
                if (txt_g.value.ToLower() == "")
                {
                    lbl_g.Text = "-";
                }
                else
                {
                    lbl_g.Text = "G" + txt_g.value;
                }
            }
            var txt_p = resumedatakehamilan.Find(x => x.pregnancy_data_type == "P");
            if (txt_p != null)
            {
                if (txt_p.value.ToLower() == "")
                {
                    lbl_p.Text = "-";
                }
                else
                {
                    lbl_p.Text = "P" + txt_p.value;
                }
            }
            var txt_a = resumedatakehamilan.Find(x => x.pregnancy_data_type == "A");
            if (txt_a != null)
            {
                if (txt_a.value.ToLower() == "")
                {
                    lbl_a.Text = "-";
                }
                else
                {
                    lbl_a.Text = "A" + txt_a.value;
                }
            }

            //----HPHT
            var txt_hpht = resumedatakehamilan.Find(x => x.pregnancy_data_type == "HPHT");
            if (txt_hpht != null)
            {
                if (txt_hpht.value.ToLower() == "")
                {
                    lbl_hpht.Text = "-";
                }
                else
                {
                    lbl_hpht.Text = txt_hpht.value;
                }
            }

            //----HPHT
            var txt_thl = resumedatakehamilan.Find(x => x.pregnancy_data_type == "THL");
            if (txt_thl != null)
            {
                if (txt_thl.value.ToLower() == "")
                {
                    lbl_thl.Text = "-";
                }
                else
                {
                    lbl_thl.Text = txt_thl.value;
                }
            }
        }
        catch (Exception ex)
        {
            log.Error(LogLibrary.Error("LoadObgynData", log_username, ex.Message.ToString()));
        }

        log.Info(LogLibrary.Logging("E", "LoadObgynData", log_username, ""));
    }

    public void LoadPediatricData(List<historypediatricdataSOAP> resumedatapediatric)
    {
        log.Info(LogLibrary.Logging("S", "LoadPediatricData", log_username, ""));

        try
        {
            var txt_tengkurap = resumedatapediatric.Find(x => x.pediatric_data_type == "TENGKURAP");
            if (txt_tengkurap != null)
            {
                if (txt_tengkurap.value.ToLower() == "")
                {
                    lbl_tengkurap.Text = "-";
                }
                else
                {
                    lbl_tengkurap.Text = txt_tengkurap.value + " bln";
                }
            }
            var txt_duduk = resumedatapediatric.Find(x => x.pediatric_data_type == "DUDUK");
            if (txt_duduk != null)
            {
                if (txt_duduk.value.ToLower() == "")
                {
                    lbl_duduk.Text = "-";
                }
                else
                {
                    lbl_duduk.Text = txt_duduk.value + " bln";
                }
            }
            var txt_merangkak = resumedatapediatric.Find(x => x.pediatric_data_type == "MERANGKAK");
            if (txt_merangkak != null)
            {
                if (txt_merangkak.value.ToLower() == "")
                {
                    lbl_merangkak.Text = "-";
                }
                else
                {
                    lbl_merangkak.Text = txt_merangkak.value + " bln";
                }
            }
            var txt_berdiri = resumedatapediatric.Find(x => x.pediatric_data_type == "BERDIRI");
            if (txt_berdiri != null)
            {
                if (txt_berdiri.value.ToLower() == "")
                {
                    lbl_berdiri.Text = "-";
                }
                else
                {
                    lbl_berdiri.Text = txt_berdiri.value + " bln";
                }
            }
            var txt_berjalan = resumedatapediatric.Find(x => x.pediatric_data_type == "BERJALAN");
            if (txt_berjalan != null)
            {
                if (txt_berjalan.value.ToLower() == "")
                {
                    lbl_berjalan.Text = "-";
                }
                else
                {
                    lbl_berjalan.Text = txt_berjalan.value + " bln";
                }
            }
            var txt_berbicara = resumedatapediatric.Find(x => x.pediatric_data_type == "BERBICARA");
            if (txt_berbicara != null)
            {
                if (txt_berbicara.value.ToLower() == "")
                {
                    lbl_berbicara.Text = "-";
                }
                else
                {
                    lbl_berbicara.Text = txt_berbicara.value + " bln";
                }
            }
        }
        catch (Exception ex)
        {
            log.Error(LogLibrary.Error("LoadPediatricData", log_username, ex.Message.ToString()));
        }

        log.Info(LogLibrary.Logging("E", "LoadPediatricData", log_username, ""));
    }
}