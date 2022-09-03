using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using log4net;
using Microsoft.Ajax.Utilities;
using Newtonsoft.Json;

public partial class Form_PatientHistory : System.Web.UI.Page
{
    protected static readonly ILog log = LogManager.GetLogger(typeof(Form_PatientHistory));

    public static List<Dose> listdoseUom = new List<Dose>();
    public static List<physicalExm> eye = new List<physicalExm>();
    public static List<physicalExm> move = new List<physicalExm>();
    public static List<physicalExm> verbal = new List<physicalExm>();


    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {


            // ------------------------------------------------------------ Electronic MR --------------------------------------------------------------
            eye = new List<physicalExm>();
            eye.Add(new physicalExm { idph = 1, name = "None" });
            eye.Add(new physicalExm { idph = 2, name = "To Pressure" });
            eye.Add(new physicalExm { idph = 3, name = "To Sound" });
            eye.Add(new physicalExm { idph = 4, name = "Spontaneus" });

            move = new List<physicalExm>();
            move.Add(new physicalExm { idph = 1, name = "None" });
            move.Add(new physicalExm { idph = 2, name = "Extension" });
            move.Add(new physicalExm { idph = 3, name = "Flexion to pain stumulus" });
            move.Add(new physicalExm { idph = 4, name = "Withdrawns from pain" });
            move.Add(new physicalExm { idph = 5, name = "Localizes to pain stimulus" });
            move.Add(new physicalExm { idph = 6, name = "Obey Commands" });
            move.Add(new physicalExm { idph = 1, name = "None" });

            verbal = new List<physicalExm>();
            verbal.Add(new physicalExm { idph = 1, name = "None" });
            verbal.Add(new physicalExm { idph = 2, name = "Incomprehensible sounds" });
            verbal.Add(new physicalExm { idph = 3, name = "Inappropriate words" });
            verbal.Add(new physicalExm { idph = 4, name = "Confused" });
            verbal.Add(new physicalExm { idph = 5, name = "Orientated" });

            // ------------------------------------------------------------ Electronic MR --------------------------------------------------------------



            /* ----------------------------------------- Date Search Emr --------------------------------------------------*/
            DateTextboxStart_emr.Text = DateTime.Now.AddMonths(-3).ToString("dd MMM yyyy");
            DateTextboxEnd_emr.Text = DateTime.Now.ToString("dd MMM yyyy");
            /* ----------------------------------------- Date Search Emr --------------------------------------------------*/

            if (Request.QueryString["PatientId"] != null)
            {

                hfPatientId.Value = Request.QueryString["PatientId"];
                hfOrgId.Value = Request.QueryString["OrganizationId"];
                getEncounter();
            }
        }            
    }

    public void initializevaluePatientHeader(PatientHeader model)
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
        //primaryDoctor.Text = model.DoctorName;
        //lblAdmissionNo.Text = model.AdmissionNo;
        lblDOB.Text = model.BirthDate.ToString("dd MMM yyyy");
        lblAge.Text = clsCommon.GetAge(model.BirthDate);
        lblReligion.Text = model.Religion;
    }

    public void initializevalue(List<LaboratoryResult> listlaboratory)
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

    StringBuilder loadDataPatientHistory(Int64 OrganizationId, Int64 PatientId, Int64 AdmissionId, String EncounterId)
    {
        /*-------------------------------------------------- Get Laboratory -----------------------------------------------*/
        try
        {
            List<LaboratoryResult> listlaboratory = new List<LaboratoryResult>();
            var dataLaboratory = clsResult.getLaboratoryResult(AdmissionId.ToString());
            var JsonLaboratory = JsonConvert.DeserializeObject<ResultLaboratoryResult>(dataLaboratory.Result.ToString());

            if (JsonLaboratory.list.Count != 0)
            {
                listlaboratory = new List<LaboratoryResult>();
                listlaboratory = JsonLaboratory.list;

                initializevalue(listlaboratory);
            }
        }
        catch (Exception ex)
        {
            LogLibrary.Error("getEncounter", hfPatientId.Value.ToString() + " " + hfEncounterId.Value, ex.Message.ToString());
            return new StringBuilder();
        }



        try
        {
            GridView gvw_test = new GridView();
            var varResult = clsPatientHistory.getPatientHistoryData(OrganizationId, PatientId, AdmissionId, EncounterId);
            var JsongetPatientHistoryData = JsonConvert.DeserializeObject<ResultPatientHistoryEncounterData>(varResult.Result.ToString());
            StringBuilder patientHistory = new StringBuilder();

            # region Data Patien History
            if (JsongetPatientHistoryData != null)
            {
                ResultPatientHistoryEncounterData patientHistoryData = JsongetPatientHistoryData;

                // ------------------------------------ Illness History ---------------------------------
                PatientHistoryHeader headerPatient = patientHistoryData.list.historyheader;
                // ------------------------------------ Illness History ---------------------------------
                List<PatientHistoryIllness> illnessHistory = patientHistoryData.list.historyillness;
                illnessHistory.OrderBy(x => x.type);
                List<String> illCategory = illnessHistory.DistinctBy(x => x.type).Select(x => x.type).ToList();

                // ------------------------------------ history physical exam ---------------------------------
                List<PatientHistoryPhysicalExam> physicalExams = patientHistoryData.list.historyphysicalexam;

                // ------------------------------------ history diagnosis ---------------------------------
                List<PatientHistoryDiagnosis> historyDiagnoses = patientHistoryData.list.historydiagnosis;

                // ------------------------------------ history planning ---------------------------------
                List<PatientHistoryPlanning> historyPlanning = patientHistoryData.list.historyplanning;

                // ------------------------------------ history prescription ---------------------------------
                List<PatientHistoryPrescription> historyPrescriptions = patientHistoryData.list.historyprescription;
                List<PatientHistoryPrescription> tmpDrugs = historyPrescriptions.FindAll(x => x.compoundName == "" && x.isConsumables == false && x.IsDoctor == 1 && x.IsAdditional == 0).OrderBy(x => x.salesItemName).ToList();
                List<PatientHistoryPrescription> tempPharmacistDrugs = historyPrescriptions.FindAll(x => x.compoundName == "" && x.isConsumables == false && x.IsDoctor == 0 && x.IsAdditional == 0).OrderBy(x => x.salesItemName).ToList();

                List<PatientHistoryPrescription> tmpConsumableDoctor = historyPrescriptions.FindAll(x => x.isConsumables == true && x.IsDoctor == 1 && x.IsAdditional == 0).OrderBy(x => x.salesItemName).ToList();
                List<PatientHistoryPrescription> tmpCompound = historyPrescriptions.FindAll(x => x.compoundName != "").OrderBy(x => x.salesItemName).ToList();
                List<String> compoundName = tmpCompound.DistinctBy(x => x.compoundName).Select(x => x.compoundName).ToList();
                List<PatientHistoryPrescription> tmpAdditionalDoctor = historyPrescriptions.FindAll(x => x.compoundName == "" && x.isConsumables == false && x.IsDoctor == 1 && x.IsAdditional == 1).OrderBy(x => x.salesItemName).ToList();

                List<PatientHistoryPrescription> tmpPrescriptionPharmacist = historyPrescriptions.FindAll(x => x.compoundName == "" && x.isConsumables == false && x.IsDoctor == 0 && x.IsAdditional == 0).OrderBy(x => x.salesItemName).ToList();
                List<PatientHistoryPrescription> tmpConsumablePharmacist = historyPrescriptions.FindAll(x => x.isConsumables == true && x.IsDoctor == 0 && x.IsAdditional == 0).OrderBy(x => x.salesItemName).ToList();
                List<PatientHistoryPrescription> tmpAdditionalPharmacist = historyPrescriptions.FindAll(x => x.compoundName == "" && x.isConsumables == false && x.IsDoctor == 0 && x.IsAdditional == 1).OrderBy(x => x.salesItemName).ToList();

                //-------------------------------- history clinical --------------------------------------------
                string linkLaboratory = "javascript:modalLaboratory('" + AdmissionId + "')";
                string linkRadiology = "javascript:modalRadiology('" + AdmissionId + "')";

                List<PatientHistoryClinicalFinding> patientHistoryClinicals = patientHistoryData.list.historyclinical;
                var laboratoryData = "";
                if (patientHistoryClinicals[0].countData != 0)
                {
                    laboratoryData = "<div class=\"btn-group\" role=\"group\" style=\"vertical-align:top\"><a href=\"" + linkLaboratory + "\"><img src=\"../../Images/Result/ic_Lab.png\" />  <label style=\"color: blue; text-decoration:underline; \"><b>Laboratory Result</b></label></a></div>";
                }
                else
                {
                    laboratoryData = "";
                }

                var radiologyData = "";
                if (patientHistoryClinicals[1].countData != 0)
                {
                    radiologyData = "<div class=\"btn-group\" role=\"group\" style=\"vertical-align:top\"><a href=\"" + linkRadiology + "\"><img src=\"../../Images/Result/ic_Rad.png\" />  <label  style=\"color: orange; text-decoration:underline; \"><b>Radiology Result</b></label></a></div>";
                }
                else
                {
                    radiologyData = "";
                }

                var statusPregnancy = "";
                if (historyDiagnoses.Find(x => x.mappingName.Equals("PREGNANCY")).value == "True")
                    statusPregnancy = "Yes";
                else
                    statusPregnancy = "No";

                var statusBreastFeeding = "";
                if (historyDiagnoses.Find(x => x.mappingName.Equals("BREASTFEEDING")).value == "True")
                    statusBreastFeeding = "Yes";
                else
                    statusBreastFeeding = "No";

                var imageResume = "/Images/PatientHistory/ic_Resume.png";


                string linkResume = "javascript:Preview(" + AdmissionId + ", /" + EncounterId + "/ );";

                patientHistory.Append("<div style=\"background-color:white; margin:20px; border-radius:7px; border: solid 2px lightgrey; \">" +
                                    "<table class=\"table table-divider\" style=\"margin-bottom: 0px;\">");
                patientHistory.Append("<tr>" +
                                        "<td colspan=\"2\" style=\"border-top:0px;\">" +
                                        "<div class=\"btn-group btn-group-justified\" role=\"group\" aria-label=\"...\">" +
                                            "<div class=\"btn-group\" role=\"group\" style=\"vertical-align:top; font-size:15px;\">" +
                                                "<b>" + patientHistoryData.list.historyheader.admissionDate.ToString("dddd, dd MMMM yyyy") + " - " +
                                                patientHistoryData.list.historyheader.organizationCode + " - " + patientHistoryData.list.historyheader.admissionTypeName + " - " +
                                                patientHistoryData.list.historyheader.doctorName + "</b" +
                                            "</div>" +
                                        //"<div class=\"btn-group\" role=\"group\" style=\"vertical-align:top; margin-left: 40%;\">" +
                                        //    "<a href=\"" + linkResume + "\" style=\"color: blue; text-decoration:underline; \"><span><img src=\'" + imageResume + "\' style=\"height:25px; width:25px; margin-right:3px; \" /></span>Medical Resume</a>" +
                                        //"</div>" +
                                        "</div>" +
                                        "</td>" +
                                      "</tr>");

                var tempChiefComplaint = historyDiagnoses.Find(x => x.mappingName == "PATIENT COMPLAINT").remarks.Split('\n').ToList();
                var notesChiefComplaint = "";
                if (tempChiefComplaint.Count != 0)
                {
                    for (int i = 0; i < tempChiefComplaint.Count; i++)
                    {
                        notesChiefComplaint = notesChiefComplaint + "<div>" + tempChiefComplaint[i] + "</div>";
                    }
                }
                else
                {
                    notesChiefComplaint = historyDiagnoses.Find(x => x.mappingName == "PATIENT COMPLAINT").remarks;
                }

                patientHistory.Append("<tr><td style=\"background-color:whitesmoke; width:20%;\"> <b>Chief Complaint</b><br/><b><i>(Keluhan Utama)</i></b></td><td>" + notesChiefComplaint + "</td></tr>");

                var tempAnemsis = patientHistoryData.list.historyanamnesis.remarks.Split('\n').ToList();
                var notesAnemsis = "";
                if (tempAnemsis.Count != 0)
                {
                    for (int i = 0; i < tempAnemsis.Count; i++)
                    {
                        notesAnemsis = notesAnemsis + "<div>" + tempAnemsis[i] + "</div>";
                    }
                }
                else
                {
                    notesAnemsis = patientHistoryData.list.historyanamnesis.remarks;
                }


                patientHistory.Append("<tr><td style=\"background-color:whitesmoke; width:20%;\"> <b>Anamnesis</b><br/><b><i>(Anamnesa)</i></b></td>" +
                                        "<td style=\"padding-top:0px; padding-bottom:0px;\">" +
                                        "<div class=\"btn-group btn-group-justified\" role=\"group\" aria-label=\"...\">" +
                                            "<div class=\"btn-group\" role=\"group\" style=\"vertical-align:top; padding:8px 8px 8px 0px;\">" +
                                                "<div>" + notesAnemsis + "</div>" +
                                            "</div>" +
                                            "<div class=\"btn-group\" role=\"group\" style=\"vertical-align:top; padding:8px; border-left:1px solid lightgrey;\">" +
                                                "<div><b>Pregnant</b><font size=\"2\"><b><i> (Hamil)</i></b></font></div><div>" + statusPregnancy + "</div>" +
                                            "</div>" +
                                            "<div class=\"btn-group\" role=\"group\" style=\"vertical-align:top; padding:8px; border-left:1px solid lightgrey;\">" +
                                                "<div><b>Breast Feeding</b><font size=\"2\"><b><i> (Menyusui)</i></b></font></div><div>" + statusBreastFeeding + "</div>" +
                                            "</div>" +
                                        "</div>" +
                                        "</td></tr>");

                /*-------------------------------- Routine Medication --------------------------------*/
                var routineMedication = illnessHistory.FindAll(x => x.type.Equals("RoutineMedication"));
                var routineMedicationInner = "";
                routineMedicationInner = "<b>Routine Medication</b><br/><font size=\"2\"><b><i>(Pengobatan Rutin)</i></b></font><ul style=\"padding-left:15px;\">";
                if (routineMedication.Count != 0)
                {
                    foreach (PatientHistoryIllness data in routineMedication)
                    {
                        if (data.value.ToUpper() != "LAIN-LAIN")
                            routineMedicationInner = routineMedicationInner + "<li>" + data.value + "</li>";
                        else
                            routineMedicationInner = routineMedicationInner + "<li>" + data.remarks + "</li>";
                    }
                }
                else
                    routineMedicationInner = routineMedicationInner + "-";
                routineMedicationInner = routineMedicationInner + "</ul>";

                /*-------------------------------- Drug Allergy --------------------------------*/
                var drugAlergy = illnessHistory.FindAll(x => x.type.Equals("DrugAllergy"));
                var drugAlergyInner = "";
                drugAlergyInner = "<b>Drug Allergy</b><br/><font size=\"2\"><b><i>(Alergi Obat)</i></b></font><ul style=\"padding-left:15px;\">";
                if (drugAlergy.Count != 0)
                {
                    foreach (PatientHistoryIllness data in drugAlergy)
                    {
                        if (data.value.ToUpper() != "LAIN-LAIN")
                            drugAlergyInner = drugAlergyInner + "<li>" + data.value + "</li>";
                        else
                            drugAlergyInner = drugAlergyInner + "<li>" + data.remarks + "</li>";
                    }
                }
                else
                    drugAlergyInner = drugAlergyInner + "-";
                drugAlergyInner = drugAlergyInner + "</ul>";

                /*-------------------------------- Food Allergy --------------------------------*/
                var foodAlergy = illnessHistory.FindAll(x => x.type.Equals("FoodAllergy"));
                var foodAlergyInner = "";
                foodAlergyInner = "<b>Food Allergy</b><br/><font size=\"2\"><b><i>(Alergi Makanan)</i></b></font><ul style=\"padding-left:15px;\">";
                if (foodAlergy.Count != 0)
                {
                    foreach (PatientHistoryIllness data in foodAlergy)
                    {
                        if (data.value.ToUpper() != "LAIN-LAIN")
                            foodAlergyInner = foodAlergyInner + "<li>" + data.value + "</li>";
                        else
                            foodAlergyInner = foodAlergyInner + "<li>" + data.remarks + "</li>";
                    }
                }
                else
                    foodAlergyInner = foodAlergyInner + "No Food Allergy";
                foodAlergyInner = foodAlergyInner + "</ul>";

                /*-------------------------------- Surgery --------------------------------*/
                var sugeryHistory = illnessHistory.FindAll(x => x.type.Equals("Surgery"));
                var sugeryHistoryInner = "";
                sugeryHistoryInner = "<b>Surgery History</b><br/><font size=\"2\"><b><i>(Riwayat Operasi)</i></b></font><ul style=\"padding-left:15px;\">";
                if (sugeryHistory.Count != 0)
                {
                    foreach (PatientHistoryIllness data in sugeryHistory)
                    {
                        if (data.value.ToUpper() != "LAIN-LAIN")
                            sugeryHistoryInner = sugeryHistoryInner + "<li>" + data.value + "</li>";
                        else
                            sugeryHistoryInner = sugeryHistoryInner + "<li>" + data.remarks + "</li>";
                    }
                }
                else
                    sugeryHistoryInner = sugeryHistoryInner + "-";
                sugeryHistoryInner = sugeryHistoryInner + "</ul>";


                /*-------------------------------- Diseas History ( Personal Disease ) --------------------------------*/
                var diseasHistory = illnessHistory.FindAll(x => x.type.Equals("PersonalDisease"));
                var diseasHistoryInner = "";
                diseasHistoryInner = "<b>Disease History</b><br/><font size=\"2\"><b><i>(Riwayat Penyakit)</i></b></font><ul style=\"padding-left:15px;\">";
                if (diseasHistory.Count != 0)
                {
                    foreach (PatientHistoryIllness data in diseasHistory)
                    {
                        if (data.value.ToUpper() != "LAIN-LAIN")
                            diseasHistoryInner = diseasHistoryInner + "<li>" + data.remarks + "</li>";
                        else
                            diseasHistoryInner = diseasHistoryInner + "<li>" + data.remarks + "</li>";
                    }
                }
                else
                    diseasHistoryInner = diseasHistoryInner + "-";
                diseasHistoryInner = diseasHistoryInner + "</ul>";

                /*-------------------------------- Family Disease --------------------------------*/
                var familyDiseasHistory = illnessHistory.FindAll(x => x.type.Equals("FamilyDisease"));
                var familyDiseasHistoryInner = "";
                familyDiseasHistoryInner = "<b>Family Disease History</b><br/><font size=\"2\"><b><i>(Riwayat Penyakit Keluarga)</i></b></font><ul style=\"padding-left:15px;\">";
                if (familyDiseasHistory.Count != 0)
                {
                    foreach (PatientHistoryIllness data in familyDiseasHistory)
                    {
                        if (data.value.ToUpper() != "LAIN-LAIN")
                            familyDiseasHistoryInner = familyDiseasHistoryInner + "<li>" + data.remarks + "</li>";
                        else
                            familyDiseasHistoryInner = familyDiseasHistoryInner + "<li>" + data.remarks + "</li>";
                    }
                }
                else
                    familyDiseasHistoryInner = familyDiseasHistoryInner + "-";


                familyDiseasHistoryInner = familyDiseasHistoryInner + "</ul>";
                patientHistory.Append("<tr><td style=\"background-color:whitesmoke; width:20%;\">" +
                                            "<div><b>Medication & Allergies</b><br/><font size=\"2\"><b><i>(Pengobatan & Alergi)</i></b></font></div>" +
                                        //"<div style=\"color: blue; text-decoration:underline; \"><span>Revision</span></div>" +
                                        "</td>" +
                                        "<td style=\"padding-top:0px; padding-bottom:0px;\">" +
                                            "<div class=\"btn-group btn-group-justified\" role=\"group\" aria-label=\"...\">" +
                                                "<div class=\"btn-group\" role=\"group\" style=\"vertical-align:top; padding:8px 8px 8px 0px;\">" +
                                                    "<div>" + routineMedicationInner + "</div>" +
                                                "</div>" +
                                                "<div class=\"btn-group\" role=\"group\" style=\"vertical-align:top; padding:8px; border-left:1px solid lightgrey;\">" +
                                                    "<div>" + drugAlergyInner + "</div>" +
                                                "</div>" +
                                                "<div class=\"btn-group\" role=\"group\" style=\"vertical-align:top; padding:8px; border-left:1px solid lightgrey;\">" +
                                                    "<div>" + foodAlergyInner + "</div>" +
                                                "</div>" +
                                            "</div>" +
                                        "</td>" +
                                        "</tr>");
                patientHistory.Append("<tr><td style=\"background-color:whitesmoke; width:20%;\">" +
                                            "<div><b>Illness History</b><br/><font size=\"2\"><b><i>(Riwayat Penyakit)</i></b></font></div>" +
                                        //"<div style=\"color: blue; text-decoration:underline; \"><span>Revision</span></div>" +
                                        "</td>" +
                                        "<td style=\"padding-top:0px; padding-bottom:0px;\">" +
                                            "<div class=\"btn-group btn-group-justified\" role=\"group\" aria-label=\"...\">" +
                                                "<div class=\"btn-group\" role=\"group\" style=\"vertical-align:top; padding:8px 8px 8px 0px;\">" +
                                                    "<div>" + sugeryHistoryInner + "</div>" +
                                                "</div>" +
                                                "<div class=\"btn-group\" role=\"group\" style=\"vertical-align:top; padding:8px; border-left:1px solid lightgrey;\">" +
                                                    "<div>" + diseasHistoryInner + "</div>" +
                                                "</div>" +
                                                "<div class=\"btn-group\" role=\"group\" style=\"vertical-align:top; padding:8px; border-left:1px solid lightgrey;\">" +
                                                    "<div>" + familyDiseasHistoryInner + "</div>" +
                                                "</div>" +
                                            "</div>" +
                                        "</td></tr>");

                List<PatientHistoryDiagnosis> endemicArea = historyDiagnoses.FindAll(x => x.mappingId.Equals(Guid.Parse("6A10C1FA-7C43-4E7C-A855-EAEA815BCADE")));
                var contentEndemicArea = "";

                if (endemicArea.Count != 0)
                {
                    foreach (PatientHistoryDiagnosis data in endemicArea)
                    {
                        if (data.remarks.ToUpper() != "LAIN-LAIN")
                            contentEndemicArea = contentEndemicArea + "<li>" + data.remarks + "</li>";
                        else
                            contentEndemicArea = contentEndemicArea + "<li>" + data.value + "</li>";
                    }
                }
                else
                    contentEndemicArea = contentEndemicArea + "-";


                List<PatientHistoryDiagnosis> screeningEndemic = historyDiagnoses.FindAll(x => x.mappingId.Equals(Guid.Parse("1979ddcb-33bc-4187-be92-04fbdb0a50d6")));
                var contentScreeningEndemic = "";

                if (screeningEndemic.Count != 0)
                {
                    foreach (PatientHistoryDiagnosis data in screeningEndemic)
                    {
                        if (data.remarks.ToUpper() != "LAIN-LAIN")
                            contentScreeningEndemic = contentScreeningEndemic + "<li>" + data.remarks + "</li>";
                        else
                            contentScreeningEndemic = contentScreeningEndemic + "<li>" + data.value + "</li>";
                    }
                }
                else
                    contentScreeningEndemic = contentScreeningEndemic + "-";


                patientHistory.Append("<tr><td style=\"background-color:whitesmoke; width:20%;\">" +
                                            "<div><b>Endemic Area Visitation</b><br/><font size=\"2\"><b><i>(Kunjungan ke Daerah Endemis)</i></b></font></div>" +
                                        "</td>" +
                                        "<td style=\"padding-top:0px; padding-bottom:0px;\">" +
                                            "<div class=\"btn-group btn-group-justified\" role=\"group\" aria-label=\"...\">" +
                                                "<div class=\"btn-group\" role=\"group\" style=\"vertical-align:top; padding:8px 8px 8px 0px; width:40%;\">" +
                                                    "<div><b>Have Been to Endemic Area</b><br/><font size=\"2\"><b><i>(Kunjungan ke Daerah Endemis)</i></b></font></div>" +
                                                    "<div>" + contentEndemicArea + "</div>" +
                                                "</div>" +
                                                "<div class=\"btn-group\" role=\"group\" style=\"vertical-align:top; padding:8px; border-left:1px solid lightgrey; width:55%;\">" +
                                                    "<div><b>Screening Infectious Disease</b><br/><font size=\"2\"><b><i>(Pengecekan Penyakit Menular)</i></b></font></div>" +
                                                    "<div>" + contentScreeningEndemic + "</div>" +
                                                "</div>" +
                                            "</div>" +
                                        "</td></tr>");

                var nutrition = historyDiagnoses.Find(x => x.mappingId.Equals(Guid.Parse("82B114B2-303C-43EC-963B-851B19A11EEA")));
                var fasting = historyDiagnoses.Find(x => x.mappingId.Equals(Guid.Parse("BB077100-EAAE-41E4-91DB-B2B10154EE48")));

                patientHistory.Append("<tr><td style=\"background-color:whitesmoke; width:20%;\">" +
                                            "<div><b>Nutrition & Fasting</b><br/></div>" +
                                            "<div style=\"font-size:13px;\"><b><i>(Nutrisi & Puasa)</i></b></div>" +
                                        //"<div style=\"color: blue; text-decoration:underline; \"><span>Revision</span></div>" +
                                        "</td>" +
                                        "<td style=\"padding-top:0px; padding-bottom:0px;\">" +
                                            "<div class=\"btn-group btn-group-justified\" role=\"group\" aria-label=\"...\">" +
                                                "<div class=\"btn-group\" role=\"group\" style=\"vertical-align:top; padding:8px 8px 8px 0px; width:40%;\">" +
                                                    "<div><b>Nutrition Problem</b></div>" +
                                                    "<div style=\"font-size:13px;\"><b><i>(Masalah Nutrisi)</i></b></div>" +
                                                    "<div>" + nutrition.value + " " + nutrition.remarks + "</div>" +
                                                "</div>" +
                                                "<div class=\"btn-group\" role=\"group\" style=\"vertical-align:top; padding:8px; border-left:1px solid lightgrey; width:55%;\">" +
                                                    "<div><b>Fasting</b></div>" +
                                                    "<div style=\"font-size:13px;\"><b><i>(Puasa)</i></b></div>" +
                                                    "<div>" + fasting.value + " " + fasting.remarks + "</div>" +
                                                "</div>" +
                                            "</div>" +
                                        "</td></tr>");

                var tempChest = physicalExams.Find(x => x.mappingId == Guid.Parse("7218971c-e89f-4172-ae3c-b7fb855c1d6d")).remarks.Split('\n').ToList();
                var notesChest = "";
                if (tempChest.Count != 0)
                {
                    for (int i = 0; i < tempChest.Count; i++)
                    {
                        notesChest = notesChest + "<div>" + tempChest[i] + "</div>";
                    }
                }
                else
                {
                    notesChest = physicalExams.Find(x => x.mappingId == Guid.Parse("7218971c-e89f-4172-ae3c-b7fb855c1d6d")).remarks;
                }

                var tempChestString = "";
                if (notesChest != "")
                {
                    tempChestString = "<div class=\"btn-group btn-group-justified\" role=\"group\" aria-label=\"...\">" +
                        "<div class=\"btn-group\" role=\"group\" style=\"vertical-align:top; padding:8px; border-top:1px solid lightgrey;\"> <div>" + notesChest + "</div></div>" +
                    "</div>";
                }
                else
                {
                    tempChestString = "<div class=\"btn-group btn-group-justified\" role=\"group\" aria-label=\"...\">" +
                        "<div class=\"btn-group\" role=\"group\" style=\"vertical-align:top; padding:8px; border-top:1px solid lightgrey;\"> <div> - </div></div>" +
                    "</div>";
                }

                var total = 0;
                var eyeData = physicalExams.Find(a => a.mappingName == "GCS EYE").value;
                var moveData = physicalExams.Find(a => a.mappingName == "GCS MOVE").value;
                var verbalData = physicalExams.Find(a => a.mappingName == "GCS VERBAL").value;

                string eyeName = "";
                if (eyeData != "")
                {
                    eyeName = eye.Find(x => x.idph.ToString() == physicalExams.Find(a => a.mappingName == "GCS EYE").value).name;
                    total = total + int.Parse(eyeData);
                }

                string moveName = "";
                if (moveData != "")
                {
                    moveName = move.Find(x => x.idph.ToString() == physicalExams.Find(a => a.mappingName == "GCS MOVE").value).name;
                    total = total + int.Parse(moveData);
                }

                string verbalName = "";
                if (verbalData != "")
                {
                    if (verbalData == "T")
                    {
                        verbalName = "Tracheostomy";
                        total = 0;
                    }
                    else if (verbalData == "A")
                    {
                        verbalName = "Aphasia";
                        total = 0;
                    }
                    else
                    {
                        verbalName = verbal.Find(x => x.idph.ToString() == physicalExams.Find(a => a.mappingName == "GCS VERBAL").value).name;
                        total = total + int.Parse(verbalData);
                    }
                }
                var painScale = physicalExams.Find(x => x.mappingId.Equals(Guid.Parse("3aae8dc2-484f-4f3c-a01b-1b0c3f107176")));
                var painScaleString = "-";
                if (painScale != null)
                {
                    painScaleString = painScale.value;
                }

                patientHistory.Append("<tr><td style=\"background-color:whitesmoke; width:20%;\"><b>Physical Examination</b></br><font size=\"2\"><b><i>(Pemeriksaan Fisik)</i></b></font></td>");
                patientHistory.Append("<td style=\"padding:0px;\">" +
                    "<div class=\"btn-group btn-group-justified\" role=\"group\" aria-label=\"...\">" +
                        "<div class=\"btn-group\" role=\"group\" style=\"vertical-align:top; padding:8px;\"><b>Eye</b><font size=\"2\"><b><i> (Mata) : </i></b></font><asp:Label runat =\"server\" Text=\"\" Font-Bold =\"true\"/>" + physicalExams.Find(x => x.mappingName == "GCS EYE").value + ". " + eyeName + "</div>" +
                        "<div class=\"btn-group\" role=\"group\" style=\"vertical-align:top; padding:8px; border-left:1px solid lightgrey;\"><b>Move</b><font size=\"2\"><b><i> (Motorik) : </i></b></font><asp:Label runat=\"server\" Font-Bold=\"true\" Text=\"\" />" + physicalExams.Find(x => x.mappingName == "GCS MOVE").value + ". " + moveName + "</div>" +
                        "<div class=\"btn-group\" role=\"group\" style=\"vertical-align:top; padding:8px; border-left:1px solid lightgrey;\"><b>Verbal</b><font size=\"2\"><b><i> (Verbal) : </i></b></font><asp:Label runat =\"server\" Font-Bold=\"true\" Text=\"\" />" + physicalExams.Find(x => x.mappingName == "GCS VERBAL").value + ". " + verbalName + "</div>" +
                        "<div class=\"btn-group\" role=\"group\" style=\"vertical-align:top; padding:8px; border-left:1px solid lightgrey;\"><b>Score</b><font size=\"2\"><b><i> (Skor) : </i></b></font><asp:Label runat =\"server\" Font-Bold=\"true\" Text=\"\" />" + total + "</div>" +
                    "</div>" +
                    "<div class=\"btn-group btn-group-justified\" role=\"group\" aria-label=\"...\">" +
                        "<div class=\"btn-group\" role=\"group\" style=\"vertical-align:top; padding:8px; border-top:1px solid lightgrey;\"><b>Pain Scale</b><font size=\"2\"><b><i> (Skala Nyeri) : </i></b></font><asp:Label runat =\"server\" Text=\"\" Font-Bold =\"true\"/>" + painScaleString + "</div>" +
                    "</div>" +
                    "<div class=\"btn-group btn-group-justified\" role=\"group\" aria-label=\"...\">" +
                        "<div class=\"btn-group\" role=\"group\" style=\"vertical-align:top; padding:8px; border-top:1px solid lightgrey;\">" +
                            "<div><b>Blood pressure</b><br/><font size=\"2\"><b><i>(Tekanan Darah)</i></b></font></div><div>" + physicalExams.Find(x => x.mappingName == "BLOOD PRESSURE HIGH").value + "/" + physicalExams.Find(x => x.mappingName == "BLOOD PRESSURE LOW").value + " mm/Hg</div>" +
                        "</div>" +
                        "<div class=\"btn-group\" role=\"group\" style=\"vertical-align:top; padding:8px; border-top:1px solid lightgrey; border-left:1px solid lightgrey;\">" +
                            "<div><b>Pulse Rate</b><br/<font size=\"2\"><b><i>(Nadi)</i></b></font></div><div>" + physicalExams.Find(x => x.mappingName == "PULSE RATE").value + " x/mnt</div>" +
                        "</div>" +
                        "<div class=\"btn-group\" role=\"group\" style=\"vertical-align:top; padding:8px; border-top:1px solid lightgrey; border-left:1px solid lightgrey;\">" +
                            "<div><b>Respiratory Rate</b><br/><font size=\"2\"><b><i>(Pernapasan)</i></b></font></div><div>" + physicalExams.Find(x => x.mappingName == "RESPIRATORY RATE").value + " x/mnt</div>" +
                        "</div>" +
                        "<div class=\"btn-group\" role=\"group\" style=\"vertical-align:top; padding:8px; border-top:1px solid lightgrey; border-left:1px solid lightgrey;\">" +
                            "<div><b>SpO2</b><br/><font size=\"2\"><b><i>(SpO2)</i></b></font></div><div>" + physicalExams.Find(x => x.mappingName == "SPO2").value + " %</div>" +
                        "</div>" +
                        "<div class=\"btn-group\" role=\"group\" style=\"vertical-align:top; padding:8px; border-top:1px solid lightgrey; border-left:1px solid lightgrey;\">" +
                            "<div><b>Temperature</b><br/><font size=\"2\"><b><i>(Suhu)</i></b></font></div><div>" + physicalExams.Find(x => x.mappingName == "TEMPERATURE").value + " &#176;C</div>" +
                        "</div>" +
                        "<div class=\"btn-group\" role=\"group\" style=\"vertical-align:top; padding:8px; border-top:1px solid lightgrey; border-left:1px solid lightgrey;\">" +
                            "<div><b>Weight</b><br/><font size=\"2\"><b><i>(Berat)</i></b></font></div><div>" + physicalExams.Find(x => x.mappingName == "WEIGHT").value + " kg</div>" +
                        "</div>" +
                        "<div class=\"btn-group\" role=\"group\" style=\"vertical-align:top; padding:8px; border-top:1px solid lightgrey; border-left:1px solid lightgrey;\">" +
                            "<div><b>Height</b><br/><font size=\"2\"><b><i>(Tinggi)</i></b></font></div><div>" + physicalExams.Find(x => x.mappingName == "HEIGHT").value + " cm</div>" +
                        "</div>" +
                    "</div>" +
                    "<div class=\"btn-group btn-group-justified\" role=\"group\" aria-label=\"...\">" +
                        "<div class=\"btn-group\" role=\"group\" style=\"vertical-align:top; padding:8px; border-top:1px solid lightgrey;\">  <div><b>Mental Status</b></div> <div><font size=\"2\"><b><i>(Status Mental)</i></b></font></div>  <div class=\"btn-group\" role=\"group\" style=\"vertical-align:top\"> " + physicalExams.Find(x => x.mappingName == "MENTAL STATUS").remarks + "</div>  </div>" +
                        "<div class=\"btn-group\" role=\"group\" style=\"vertical-align:top; padding:8px; border-top:1px solid lightgrey; border-left:1px solid lightgrey;\">  <div><b>Consciousness Level</b></div> <div><font size=\"2\"><b><i>(Kesadaran)</i></b></font></div> <div class=\"btn-group\" role=\"group\" style=\"vertical-align:top\"> " + physicalExams.Find(x => x.mappingName == "CONSCIOUSNESS LEVEL").value + "</div>  </div>" +
                    "</div>" + tempChestString +
                    "</td></tr>");
                var diagnosis = historyDiagnoses.Find(x => x.mappingId.Equals(Guid.Parse("d24d0881-7c06-4563-bf75-3a20b843dc47")));
                var tempDiagnosis = diagnosis.remarks.Split('\n').ToList();
                var notesDiagnosis = "";
                if (tempDiagnosis.Count != 0)
                {
                    for (int i = 0; i < tempDiagnosis.Count; i++)
                    {
                        notesDiagnosis = notesDiagnosis + "<div>" + tempDiagnosis[i] + "</div>";
                    }
                }
                else
                {
                    notesDiagnosis = diagnosis.remarks;
                }

                patientHistory.Append("<tr><td style=\"background-color:whitesmoke; width:20%;\"><b>Diagnosis</b><br/><font size=\"2\"><b><i>(Diagnosa)</i></b></font></td>" +
                                    "<td>" + notesDiagnosis + "</td></tr>");

                var tempPlanProcedure = historyPlanning.Find(x => x.mappingId.Equals(Guid.Parse("337a371f-baf5-424a-bdc5-c320c277cac6"))).remarks.Split('\n').ToList();
                var notesPlanProcedure = "";
                if (tempPlanProcedure.Count != 0)
                {
                    for (int i = 0; i < tempPlanProcedure.Count; i++)
                    {
                        notesPlanProcedure = notesPlanProcedure + "<div>" + tempPlanProcedure[i] + "</div>";
                    }
                }
                else
                {
                    notesPlanProcedure = historyPlanning.Find(x => x.mappingId.Equals(Guid.Parse("337a371f-baf5-424a-bdc5-c320c277cac6"))).remarks;
                }
                patientHistory.Append("<tr><td style=\"background-color:whitesmoke; width:20%;\"><b>Planning & Procedure</b><br/><font size=\"2\"><b><i>(Tindakan di RS)</i></b></font></td><td>" + notesPlanProcedure + "</td></tr>");

                var tempProcedure = headerPatient.procedureNotes.Split('\n').ToList();
                var notesProcedure = "";
                if (tempProcedure.Count != 0)
                {
                    for (int i = 0; i < tempProcedure.Count; i++)
                    {
                        notesProcedure = notesProcedure + "<div>" + tempProcedure[i] + "</div>";
                    }
                }
                else
                {
                    notesProcedure = headerPatient.procedureNotes;
                }

                patientHistory.Append("<tr><td style=\"background-color:whitesmoke; width:20%;\"><b>Procedure Notes</b><br/><font size=\"2\"><b><i>(Catatan Tindakan)</i></b></font></td><td>" + notesProcedure + "</td></tr>");

                patientHistory.Append("<tr><td style=\"background-color:whitesmoke; width:20%;\"><b>Clinical Findings</b><br/><font size=\"2\"><b><i>(Temuan Klinis)</i></b></font></td><td>" +
                    "<div class=\"btn-group btn-group-justified\" role=\"group\" aria-label=\"...\">" + laboratoryData + " " + radiologyData + "</div>&nbsp;" +
                    "</td></tr>");
                patientHistory.Append("<tr><td style=\"background-color:whitesmoke; width:20%; border-radius:0px 0px 0px 7px;\"><b>Prescription</b></br><font size=\"2\"><b><i>(Resep)</i></b></font></td><td style=\"padding:0px;\">");

                if (tmpDrugs.Count != 0)
                {
                    var tempDoctorDrugs = tmpDrugs.FindAll(x => x.IsDoctor == 1);

                    /*---------------------------------------------------------- Doctor Prescription --------------------------------------------------*/
                    if (tempDoctorDrugs.Count != 0)
                    {
                        patientHistory.Append("<div><div style=\"padding:6px 6px 0px 8px;background-color:#b4e3fa;height:35px;font-family:Helvetica;font-size:14px;\"><b>DOCTOR Prescription</b><font size=\"2\"><b><i> (Resep Dokter)</i></b></font></div>");
                        patientHistory.Append("<table class=\"table table-striped table-condensed\" style=\"margin-bottom: 0px;\"><tr>" +
                                                "<td><b>Item</b></td>" +
                                                "<td><b>Dose</b></td>" +
                                                "<td><b>Dose UOM</b></td>" +
                                                "<td><b>Frequency</b></td>" +
                                                "<td><b>Route</b></td>" +
                                                "<td><b>Instruction</b></td>" +
                                                "<td><b>Qty</b></td>" +
                                                "<td><b>U.O.M</b></td>" +
                                                "<td><b>Iter</b></td>" +
                                                "<td><b>Routine</b></td>" +
                                                "</tr>");
                        foreach (PatientHistoryPrescription dataDrugs in tempDoctorDrugs)
                        {
                            if (dataDrugs.quantity.Contains(".000"))
                            {
                                dataDrugs.quantity = dataDrugs.quantity.Replace(".000", "");
                            }
                            else if (dataDrugs.quantity.Contains(",000"))
                            {
                                dataDrugs.quantity = dataDrugs.quantity.Replace(",000", "");
                            }

                            var doseUom = "";
                            if (listdoseUom.Find(x => x.doseUomId.Equals(dataDrugs.dose_uom_id)) != null)
                                doseUom = listdoseUom.Find(x => x.doseUomId.Equals(dataDrugs.dose_uom_id)).name;

                            var routine = "";
                            if (dataDrugs.isRoutine)
                                routine = "Yes";
                            else
                                routine = "No";

                            patientHistory.Append("<tr>" +
                                                    "<td>" + dataDrugs.salesItemName + "</td>" +
                                                    "<td>" + (Int64)dataDrugs.dose + "</td>" +
                                                    "<td>" + doseUom + "</td>" +
                                                    "<td>" + dataDrugs.frequency + "</td>" +
                                                    "<td>" + dataDrugs.route + "</td>" +
                                                    "<td>" + dataDrugs.instruction + "</td>" +
                                                    "<td>" + dataDrugs.quantity + "</td>" +
                                                    "<td>" + dataDrugs.uom + "</td>" +
                                                    "<td>" + dataDrugs.iter + "</td>" +
                                                    "<td>" + routine + "</td>" +
                                                    "</tr>");
                        }
                        patientHistory.Append("</table></div>");

                        /*---------------------------------------------------------- Doctor Prescription Notes --------------------------------------------------*/
                        var doctorNotes = "-";
                        if (historyPlanning.Find(x => x.mappingId.Equals(Guid.Parse("2DF0294D-F94E-4BA4-8BA1-F017BFB55D92"))).remarks != "")
                        {
                            patientHistory.Append("<div style=\"padding:6px 8px 6px 8px;font-family:Helvetica;background-color:#b4e3fa;margin-bottom: 15px;\">" +
                                                "<div style=\"font-size:14px;\"><b>DOCTOR Prescription Notes</b><font size=\"2\"><b><i> (Catatan Resep Dokter)</i></b></font></div>" +
                                                "<div style=\"font-size:12px;\">");

                            doctorNotes = historyPlanning.Find(x => x.mappingId.Equals(Guid.Parse("2DF0294D-F94E-4BA4-8BA1-F017BFB55D92"))).remarks;
                            var tempnotes = doctorNotes.Split('\n').ToList();
                            if (tempnotes.Count != 0)
                            {
                                doctorNotes = "";
                                for (int i = 0; i < tempnotes.Count; i++)
                                {
                                    patientHistory.Append("<div>" + tempnotes[i] + "</div>");
                                }
                            }
                            else
                            {
                                patientHistory.Append(doctorNotes);
                            }

                            patientHistory.Append("</div>" +
                                                    "</div>");
                        }
                    }

                    /*---------------------------------------------------------- Doctor Consumables --------------------------------------------------*/
                    if (tmpConsumableDoctor.Count != 0)
                    {
                        patientHistory.Append("<div style=\"margin-bottom: 15px;\"><div style=\"padding:6px 6px 0px 8px;background-color:#b4e3fa;height:35px;font-family:Helvetica;font-size:14px;\"><b>DOCTOR Consumables</b><font size=\"2\"><b><i> (Alat Kesehatan Dokter)</i></b></font></div>");
                        patientHistory.Append("<table class=\"table table-striped table-condensed\"><tr>" +
                                                "<td><b>Item</b></td>" +
                                                "<td><b>Qty</b></td>" +
                                                "<td><b>U.O.M</b></td>" +
                                                "<td><b>Instruction</b></td>" +
                                                "</tr>");
                        foreach (PatientHistoryPrescription dataconsumable in tmpConsumableDoctor)
                        {
                            
                            patientHistory.Append("<tr>" +
                                                    "<td>" + dataconsumable.salesItemName + "</td>" +
                                                    "<td>" + dataconsumable.quantity + "</td>" +
                                                    "<td>" + dataconsumable.uom + "</td>" +
                                                    "<td>" + dataconsumable.instruction + "</td>" +
                                                    "</tr>");
                        }
                        patientHistory.Append("</table></div>");
                    }

                    /*---------------------------------------------------------- Doctor Additional --------------------------------------------------*/
                    if (tmpAdditionalDoctor.Count != 0)
                    {
                        patientHistory.Append("<div><div style=\"padding:6px 6px 0px 8px;background-color:#b4e3fa;height:35px;font-family:Helvetica;font-size:14px;\"><b>DOCTOR Additional Prescription</b><font size=\"2\"><b><i> (Tambahan Resep Dokter)</i></b></font></div>");
                        patientHistory.Append("<table class=\"table table-striped table-condensed\" style=\"margin-bottom: 0px;\"><tr>" +
                                                "<td><b>Item</b></td>" +
                                                "<td><b>Qty</b></td>" +
                                                "<td><b>U.O.M</b></td>" +
                                                "<td><b>Instruction</b></td>" +
                                                "</tr>");
                        foreach (PatientHistoryPrescription dataconsumable in tmpAdditionalDoctor)
                        {

                            if (dataconsumable.quantity.Contains(".000"))
                            {
                                dataconsumable.quantity = dataconsumable.quantity.Replace(".000", "");
                            }
                            else if (dataconsumable.quantity.Contains(",000"))
                            {
                                dataconsumable.quantity = dataconsumable.quantity.Replace(",000", "");
                            }

                            patientHistory.Append("<tr>" +
                                                    "<td>" + dataconsumable.salesItemName + "</td>" +
                                                    "<td>" + dataconsumable.quantity + "</td>" +
                                                    "<td>" + dataconsumable.uom + "</td>" +
                                                    "<td>" + dataconsumable.instruction + "</td>" +
                                                    "</tr>");
                        }
                        patientHistory.Append("</table></div>");

                        /*---------------------------------------------------------- Doctor Additional Notes --------------------------------------------------*/
                        var doctorAdditionalNotes = "-";
                        if (historyPlanning.Find(x => x.mappingId.Equals(Guid.Parse("5E34AE60-1D72-4EFD-8440-C4442515AABE"))).remarks != "")
                        {
                            doctorAdditionalNotes = historyPlanning.Find(x => x.mappingId.Equals(Guid.Parse("5E34AE60-1D72-4EFD-8440-C4442515AABE"))).remarks;
                        }

                        patientHistory.Append("<div style=\"padding:6px 8px 6px 8px;font-family:Helvetica;background-color:#b4e3fa;margin-bottom: 15px;\">" +
                                                "<div style=\"font-size:14px;\"><b>DOCTOR Additional Notes</b><font size=\"2\"><b><i> (Catatan Tambahan Resep Dokter)</i></b></font></div>" +
                                                "<div style=\"font-size:12px;\">" + doctorAdditionalNotes + "</div>" +
                                            "</div>");
                    }

                    /*---------------------------------------------------------- Pharmacist Prescription --------------------------------------------------*/
                    if (tempPharmacistDrugs.Count != 0)
                    {
                        patientHistory.Append("<div><div style=\"padding:6px 6px 0px 8px;background-color:#dfe69b;height:35px;font-family:Helvetica;font-size:14px;\"><b>PHARMACIST Prescription</b><font size=\"2\"><b><i> (Resep Farmasi)</i></b></font></div>");
                        patientHistory.Append("<table class=\"table table-striped table-condensed\" style=\"margin-bottom: 0px;\"><tr>" +
                                                "<td><b>Item</b></td>" +
                                                "<td><b>Dose</b></td>" +
                                                "<td><b>Dose UOM</b></td>" +
                                                "<td><b>Frequency</b></td>" +
                                                "<td><b>Route</b></td>" +
                                                "<td><b>Instruction</b></td>" +
                                                "<td><b>Qty</b></td>" +
                                                "<td><b>U.O.M</b></td>" +
                                                "<td><b>Iter</b></td>" +
                                                "<td><b>Routine</b></td>" +
                                                "</tr>");
                        foreach (PatientHistoryPrescription dataDrugs in tempPharmacistDrugs)
                        {
                            if (dataDrugs.quantity.Contains(".000"))
                            {
                                dataDrugs.quantity = dataDrugs.quantity.Replace(".000", "");
                            }
                            else if (dataDrugs.quantity.Contains(",000"))
                            {
                                dataDrugs.quantity = dataDrugs.quantity.Replace(",000", "");
                            }

                            var doseUom = "";
                            if (listdoseUom.Find(x => x.doseUomId.Equals(dataDrugs.dose_uom_id)) != null)
                                doseUom = listdoseUom.Find(x => x.doseUomId.Equals(dataDrugs.dose_uom_id)).name;

                            var routine = "";
                            if (dataDrugs.isRoutine)
                                routine = "Yes";
                            else
                                routine = "No";

                            patientHistory.Append("<tr>" +
                                                    "<td>" + dataDrugs.salesItemName + "</td>" +
                                                    "<td>" + (Int64)dataDrugs.dose + "</td>" +
                                                    "<td>" + doseUom + "</td>" +
                                                    "<td>" + dataDrugs.frequency + "</td>" +
                                                    "<td>" + dataDrugs.route + "</td>" +
                                                    "<td>" + dataDrugs.instruction + "</td>" +
                                                    "<td>" + dataDrugs.quantity + "</td>" +
                                                    "<td>" + dataDrugs.uom + "</td>" +
                                                    "<td>" + dataDrugs.iter + "</td>" +
                                                    "<td>" + routine + "</td>" +
                                                    "</tr>");
                        }
                        patientHistory.Append("</table></div>");


                        /*---------------------------------------------------------- Pharmacist Prescription Notes --------------------------------------------------*/
                        var pharmacistNotes = "-";
                        if (headerPatient.PharmacyNotes != "")
                        {
                            pharmacistNotes = headerPatient.PharmacyNotes;

                            patientHistory.Append("<div style=\"padding:6px 8px 6px 8px;font-family:Helvetica;background-color:#dfe69b;margin-bottom: 15px;\">" +
                                                "<div style=\"font-size:14px;\"><b>PHARMACIST Prescription Notes</b><font size=\"2\"><b><i> (Catatan Resep Farmasi)</i></b></font></div>" +
                                                "<div style=\"font-size:12px;\">");

                            var tempnotes = pharmacistNotes.Split('\n').ToList();
                            if (tempnotes.Count != 0)
                            {
                                for (int i = 0; i < tempnotes.Count; i++)
                                {
                                    patientHistory.Append("<div>" + tempnotes[i] + "</div>");
                                }
                            }
                            else
                            {
                                patientHistory.Append(pharmacistNotes);
                            }

                            patientHistory.Append("</div>" +
                                        "</div>");
                        }
                    }

                    /*---------------------------------------------------------- Pharmacist Consumables --------------------------------------------------*/
                    if (tmpConsumablePharmacist.Count != 0)
                    {
                        patientHistory.Append("<div style=\"margin-bottom: 15px;\"><div style=\"padding:6px 6px 0px 8px;background-color:#dfe69b;height:35px;font-family:Helvetica;font-size:14px;\"><b>PHARMACIST Consumables</b><font size=\"2\"><b><i> (Alat Kesehatan Farmasi)</i></b></font></div>");
                        patientHistory.Append("<table class=\"table table-striped table-condensed\"><tr>" +
                                                "<td><b>Item</b></td>" +
                                                "<td><b>Qty</b></td>" +
                                                "<td><b>U.O.M</b></td>" +
                                                "<td><b>Instruction</b></td>" +
                                                "</tr>");
                        foreach (PatientHistoryPrescription dataconsumable in tmpConsumablePharmacist)
                        {
                            patientHistory.Append("<tr>" +
                                                    "<td>" + dataconsumable.salesItemName + "</td>" +
                                                    "<td>" + dataconsumable.quantity + "</td>" +
                                                    "<td>" + dataconsumable.uom + "</td>" +
                                                    "<td>" + dataconsumable.instruction + "</td>" +
                                                    "</tr>");
                        }
                        patientHistory.Append("</table></div>");
                    }

                    /*---------------------------------------------------------- Pharmacist Additional --------------------------------------------------*/
                    if (tmpAdditionalPharmacist.Count != 0)
                    {
                        patientHistory.Append("<div><div style=\"padding:6px 6px 0px 8px;background-color:#dfe69b;height:35px;font-family:Helvetica;font-size:14px;\"><b>PHARMACIST Additional Prescription</b><font size=\"2\"><b><i> (Tambahan Resep Farmasi)</i></b></font></div>");
                        patientHistory.Append("<table class=\"table table-striped table-condensed\" style=\"margin-bottom: 0px;\"><tr>" +
                                                "<td><b>Item</b></td>" +
                                                "<td><b>Qty</b></td>" +
                                                "<td><b>U.O.M</b></td>" +
                                                "<td><b>Instruction</b></td>" +
                                                "</tr>");
                        foreach (PatientHistoryPrescription dataconsumable in tmpAdditionalPharmacist)
                        {
                            if (dataconsumable.quantity.Contains(".000"))
                            {
                                dataconsumable.quantity = dataconsumable.quantity.Replace(".000", "");
                            }
                            else if (dataconsumable.quantity.Contains(",000"))
                            {
                                dataconsumable.quantity = dataconsumable.quantity.Replace(",000", "");
                            }

                            patientHistory.Append("<tr>" +
                                                    "<td>" + dataconsumable.salesItemName + "</td>" +
                                                    "<td>" + dataconsumable.quantity + "</td>" +
                                                    "<td>" + dataconsumable.uom + "</td>" +
                                                    "<td>" + dataconsumable.instruction + "</td>" +
                                                    "</tr>");
                        }
                        patientHistory.Append("</table></div>");


                        /*---------------------------------------------------------- Pharmacist Additional Notes --------------------------------------------------*/
                        var pharmacistAdditionalNotes = "-";
                        if (headerPatient.AdditionalPharmacyNotes != "")
                        {
                            pharmacistAdditionalNotes = headerPatient.AdditionalPharmacyNotes;
                        }

                        patientHistory.Append("<div style=\"padding:6px 8px 6px 8px;font-family:Helvetica;background-color:#dfe69b;margin-bottom: 15px;\">" +
                                                "<div style=\"font-size:14px;\"><b>PHARMACIST Additional Notes</b><font size=\"2\"><b><i> (Catatan Tambahan Resep Farmasi)</i></b></font></div>" +
                                                "<div style=\"font-size:12px;\">" + pharmacistAdditionalNotes + "</div>" +
                                            "</div>");
                    }

                }

                //if (Helper.GetFlagCompound(this) == "TRUE")
                //{
                //    if (tmpCompound.Count != 0)
                //    {
                //        List<MedicalHistory> detailCompound = new List<MedicalHistory>();
                //        patientHistory.Append("<div style=\"padding-left:8px;\"><b>Compound Prescription</b></div>");
                //        patientHistory.Append("<table class=\"table table-striped table-condensed\"><tr>" +
                //                                "<td><b>Item</b></td>" +
                //                                "<td><b>Qty</b></td>" +
                //                                "<td><b>U.O.M</b></td>" +
                //                                "<td><b>Frequency</b></td>" +
                //                                "<td><b>Dose & Instruction</b></td>" +
                //                                "<td><b>Route</b></td>" +
                //                                "<td><b>Iter</b></td>" +
                //                                "<td><b>Routine</b></td>" +
                //                                "</tr>");
                //        foreach (String cmpName in compoundName)
                //        {
                //            var header = tmpCompound.Find(x => x.compoundName == cmpName && x.itemId == 0);

                //            var routine = "";
                //            if (header.isRoutine)
                //                routine = "Yes";
                //            else
                //                routine = "No";

                //            string link = "javascript:Open('" + cmpName + "')";

                //            patientHistory.Append("<tr>" +
                //                                    "<td><a href=\"" + link + "\" style=\"color: blue; text-decoration:underline; \">" + cmpName + "</a></td>" +
                //                                "<td>" + header.quantity + "</td>" +
                //                                "<td>" + header.uom + "</td>" +
                //                                "<td>" + header.frequency + "</td>" +
                //                                "<td>" + header.instruction + "</td>" +
                //                                "<td>" + header.route + "</td>" +
                //                                "<td>" + header.iter + "</td>" +
                //                                "<td>" + routine + "</td>" +
                //                                "</tr>");


                //            //----------------------------------------------------------------------------

                //            var tmpDataCompound = tmpCompound.OrderBy(x => x.compoundName).ToList().FindAll(x => x.compoundName == cmpName && x.itemId != 0);
                //            foreach (PatientHistoryPrescription dataCompound in tmpDataCompound)
                //            {
                //                detailCompound.Add(new MedicalHistory
                //                {
                //                    compoundName = dataCompound.compoundName,
                //                    doseText = dataCompound.doseText,
                //                    uom = dataCompound.uom,
                //                    frequency = dataCompound.frequency,
                //                    instruction = dataCompound.instruction,
                //                    itemName = dataCompound.salesItemName,
                //                    quantity = Int64.Parse(dataCompound.quantity),
                //                    route = dataCompound.route,
                //                    iter = dataCompound.iter.ToString()
                //                });
                //            }

                //        }
                //        patientHistory.Append("</table>");
                //        Session.Remove(Helper.ViewStatePatientHistoryCompound);
                //        Session[Helper.ViewStatePatientHistoryCompound] = detailCompound;
                //    }
                //}

                patientHistory.Append("</td></tr>");
                patientHistory.Append("</table></div>");
                //-------------------------------- String Patient History Data --------------------------------------------

            }
            #endregion

            //log.Info(LogLibrary.Logging("E", "loadDataPatientHistory", Helper.GetLoginUser(this), ""));
            return patientHistory;
        }
        catch (Exception ex)
        {
            LogLibrary.Error("getEncounter", hfPatientId.Value.ToString() + " " + hfEncounterId.Value, ex.Message.ToString());
            return new StringBuilder();
        }
    }

    protected void btn_search_Click(object sender, EventArgs e)
    {
        var encounterData = (List<PatientHistoryEncounter>)Session[Helper.ViewStateEncounterData];
        string dtend = Request.Form[DateTextboxEnd_emr.UniqueID];
        DateTextboxEnd_emr.Text = dtend;
        dtend = Request.Form[DateTextboxStart_emr.UniqueID];
        DateTextboxStart_emr.Text = dtend;
        List<PatientHistoryEncounter> dataFilter = new List<PatientHistoryEncounter>();
        StringBuilder patientHistory = new StringBuilder();

        #region
        //------------------------------------------------------------ Filter by Date -----------------------------------------------------------------------------------------

        if (DateTextboxStart_emr.Text != "")
        {
            dataFilter = encounterData.FindAll(x => DateTime.Parse(x.admissionDate.ToString()) >= DateTime.Parse(DateTextboxStart_emr.Text.ToString()));
            var temp = dataFilter;
            if (DateTextboxEnd_emr.Text != "")
            {
                dataFilter = temp.FindAll(x => DateTime.Parse(x.admissionDate.ToString()) <= DateTime.Parse(DateTextboxEnd_emr.Text.ToString()));
            }
        }
        else
        {
            if (DateTextboxEnd_emr.Text != "")
            {
                dataFilter = encounterData.FindAll(x => DateTime.Parse(x.admissionDate.ToString()) <= DateTime.Parse(DateTextboxEnd_emr.Text));
            }
            else
            {
                dataFilter = encounterData;
            }
        }
        //------------------------------------------------------------ Filter by Date -----------------------------------------------------------------------------------------
        #endregion

        Session[Helper.ViewStatePageData] = dataFilter;
        hf_load_encounter.Value = "0";
        btn_load_more.Visible = false;

        if (dataFilter.Count != 0)
        {
            tblPatientHistory.Visible = true;
            img_noData_emr.Visible = false;

            patientHistory = loadDataPatientHistory(dataFilter[0].organizationId, dataFilter[0].patientId, dataFilter[0].admissionId, dataFilter[0].encounterId.ToString());
            Session[Helper.ViewStatePatientHistoryInner] = patientHistory;
            tblPatientHistory.InnerHtml = patientHistory.ToString();


            if (dataFilter.Count == 1)
            {
                btn_load_more.Visible = false;
                status_dataEmr.Value = "";
            }
            else
            {
                btn_load_more.Visible = true;
                status_dataEmr.Value = "LOAD MORE";
            }
        }
        else
        {
            patientHistory.Append("<div style=\"background-color:transparant; padding-left: 20px;padding-right: 20px;padding-top: 20px;padding-bottom: 20px; \"></div>");
            tblPatientHistory.InnerHtml = patientHistory.ToString();
            tblPatientHistory.Visible = false;
            img_noData_emr.Visible = true;
            //src_emr.Visible = false;
            status_dataEmr.Value = "empty";
        }
    }

    protected void btn_load_more_Click(object sender, EventArgs e)
    {
        try
        {
            List<PatientHistoryEncounter> encounterData = (List<PatientHistoryEncounter>)Session[Helper.ViewStatePageData];
            var index = int.Parse(hf_load_encounter.Value) + 1;

            hf_load_encounter.Value = index.ToString();
            StringBuilder tmpString = (StringBuilder)Session[Helper.ViewStatePatientHistoryInner];

            StringBuilder patientHistory = loadDataPatientHistory(encounterData[index].organizationId, encounterData[index].patientId, encounterData[index].admissionId, encounterData[index].encounterId.ToString());

            tmpString = tmpString.Append(patientHistory);

            tblPatientHistory.InnerHtml = tmpString.ToString();

            if (index + 1 == encounterData.Count)
            {
                btn_load_more.Visible = false;
            }
        }
        catch (Exception ex)
        {
            LogLibrary.Error("loadMorePatientHistory", hfPatientId.Value.ToString() + " " + hfEncounterId.Value, ex.Message.ToString());
        }
    }


    void getHeader(long patientID, string encounterID)
    {
        try
        {
            var varResult = clsCommon.GetPatientHeader(patientID, encounterID);
            ResultPatientHeader JsongetPatientHistory = JsonConvert.DeserializeObject<ResultPatientHeader>(varResult.Result.ToString());
            PatientHeader header = JsongetPatientHistory.header;
            initializevaluePatientHeader(header);
        }
        catch (Exception ex)
        {
            LogLibrary.Error("GetPatientHeader", hfPatientId.Value.ToString() + " " + hfEncounterId.Value, ex.Message.ToString());
        }

    }

    void getEncounter()
    {
        Session.Remove(Helper.ViewStateEncounterData);
        Session.Remove(Helper.ViewStatePageData);
        Session.Remove(Helper.ViewStatePatientHistoryInner);

        try
        {
            GridView gvw_test = new GridView();
            var varResult = clsPatientHistory.getEncounterPatientHistory(hfPatientId.Value);

            var JsongetPatientHistoryEncounter = JsonConvert.DeserializeObject<ResultPatientHistoryEncounter>(varResult.Result.ToString());

            List<PatientHistoryEncounter> encounterData = new List<PatientHistoryEncounter>();
            List<PatientHistoryEncounter> encounterAllData = new List<PatientHistoryEncounter>();
            encounterAllData = JsongetPatientHistoryEncounter.list.FindAll(x => x.organizationId.Equals(long.Parse(hfOrgId.Value)));
            Session[Helper.ViewStateEncounterData] = (List<PatientHistoryEncounter>)encounterAllData.ToList();

            if (DateTextboxStart_emr.Text != "")
            {
                var tempData = encounterAllData.FindAll(x => DateTime.Parse(x.admissionDate.ToString("dd MMM yyyy")) >= DateTime.Parse(DateTime.Now.AddMonths(-3).ToString("dd MMM yyyy")));
                var temp = tempData;
                if (DateTextboxEnd_emr.Text != "")
                {
                    tempData = temp.FindAll(x => DateTime.Parse(x.admissionDate.ToString("dd MMM yyyy")) <= DateTime.Parse(DateTextboxEnd_emr.Text.ToString()));
                }

                encounterData = tempData.ToList();

            }

            if (encounterAllData.Count == 1)
            {
                getHeader(encounterAllData[0].patientId, encounterAllData[0].encounterId.ToString());
                btn_load_more.Visible = false;
            }

            if (encounterData.Count > 0)
            {

                getHeader(encounterData[0].patientId, encounterData[0].encounterId.ToString());

                Session[Helper.ViewStatePageData] = encounterData;
                Session.Remove(Helper.ViewStatePatientHistoryInner);
                StringBuilder patientHistory = loadDataPatientHistory(encounterData[0].organizationId, encounterData[0].patientId, encounterData[0].admissionId, encounterData[0].encounterId.ToString());
                log.Debug(LogLibrary.Logging("S", "URL getEncounter ", "test", patientHistory.ToString()));

                Session[Helper.ViewStatePatientHistoryInner] = patientHistory;
                tblPatientHistory.InnerHtml = patientHistory.ToString();

                DataTable dt = Helper.ToDataTable(encounterAllData);
                gvw_test.DataSource = dt;
                gvw_test.DataBind();
                img_noData_emr.Visible = false;
            }
            else
            {
                btn_load_more.Visible = false;
                img_noData_emr.Visible = true;
                status_dataEmr.Value = "empty";
            }
        }
        catch (Exception ex)
        {
            LogLibrary.Error("getEncounter", hfPatientId.Value.ToString() + " " + hfEncounterId.Value, ex.Message.ToString());
        }
    }
}

public class physicalExm
{
    public int idph { get; set; }
    public string name { get; set; }
}