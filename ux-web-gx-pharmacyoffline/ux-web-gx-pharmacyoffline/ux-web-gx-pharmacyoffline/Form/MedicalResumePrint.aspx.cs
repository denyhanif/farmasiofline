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
public partial class Form_MedicalResumePrint : System.Web.UI.Page
{
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
            }
            //initializevalue(2, 1202556, 2000005537759, Guid.Parse("28303d4c-59b8-4304-9e2c-329f530442da"));
            if (Request.QueryString["PatientId"] != null && Request.QueryString["AdmissionId"] != null && Request.QueryString["EncounterId"] != null && Request.QueryString["OrganizationId"] != null)
            {
                long PatientId = long.Parse(Request.QueryString["PatientId"]);
                long AdmissionId = long.Parse(Request.QueryString["AdmissionId"]);
                Guid EncounterId = Guid.Parse(Request.QueryString["EncounterId"]);
                long OrganizationId = long.Parse(Request.QueryString["OrganizationId"]);

                initializevalue(OrganizationId, PatientId, AdmissionId, EncounterId);
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

    protected dynamic MappingforGetdataSOAP(long OrganizationId, long PatientId, long AdmissionId, Guid EncounterId)
    {
        //set fa sesuai templatenya
        string templateid = Request.QueryString["PageSOAP"];
        if (templateid.ToUpper() == "00000000-0000-0000-0000-000000000000" || templateid.ToUpper() == "7CCD0A7E-9001-48FF-8052-ED07CF5716BB" || templateid.ToUpper() == "882854F0-780A-48BB-89EC-A6FF7519D10B")
        {
            var DataGeneral = clsMedicalResume.GetResumeWDoc(OrganizationId, PatientId, AdmissionId, EncounterId);
            ResultMedicalResumeWDoc JsongetPatientHistoryGeneral = JsonConvert.DeserializeObject<ResultMedicalResumeWDoc>(DataGeneral.Result.ToString());

            hfpreviewpres.Value = DataGeneral.Result.ToString();
            return JsongetPatientHistoryGeneral;
        }
        else if (templateid.ToUpper() == "711671B0-A2B3-4311-9B89-69C146FDAE3A") //EMERGENCY
        {
            var DataGeneral = clsMedicalResume.GetResumeWDoc(OrganizationId, PatientId, AdmissionId, EncounterId);
            ResultMedicalResumeWDoc JsongetPatientHistoryGeneral = JsonConvert.DeserializeObject<ResultMedicalResumeWDoc>(DataGeneral.Result.ToString());

            hfpreviewpres.Value = DataGeneral.Result.ToString();
            return JsongetPatientHistoryGeneral;
        }
        else if (templateid.ToUpper() == "903E0F23-2C60-41F1-8C04-EB3E70D1E002") //OBGYN
        {
            var DataObgyn = clsMedicalResume.GetResumeObgyn(OrganizationId, PatientId, AdmissionId, EncounterId);
            ResultMedicalResumeObgyn JsongetPatientHistoryObgyn = JsonConvert.DeserializeObject<ResultMedicalResumeObgyn>(DataObgyn.Result.ToString());

            hfpreviewpres.Value = DataObgyn.Result.ToString();
            return JsongetPatientHistoryObgyn;
        }
        else if (templateid.ToUpper() == "01D7A679-92EF-4A56-B040-1614B3C9EFAF") //PEDIATRIC
        {
            var DataPediatric = clsMedicalResume.GetResumePediatric(OrganizationId, PatientId, AdmissionId, EncounterId);
            ResultMedicalResumePediatric JsongetPatientHistoryPediatric = JsonConvert.DeserializeObject<ResultMedicalResumePediatric>(DataPediatric.Result.ToString());

            hfpreviewpres.Value = DataPediatric.Result.ToString();
            return JsongetPatientHistoryPediatric;
        }

        return null;
    }

    public void initializevalue(long OrganizationId, long PatientId, long AdmissionId, Guid EncounterId)
    {
        try
        {
            //var Data = clsMedicalResume.GetResume(OrganizationId, PatientId, AdmissionId, EncounterId);
            //ResultMedicalResume JsongetPatientHistory = JsonConvert.DeserializeObject<ResultMedicalResume>(Data.Result.ToString());
            var JsongetPatientHistory = MappingforGetdataSOAP(OrganizationId, PatientId, AdmissionId, EncounterId);

            var GetSettings = clsSettings.GetAppSettings(OrganizationId);
            var theSettings = JsonConvert.DeserializeObject<ResultViewOrganizationSetting>(GetSettings.Result.ToString());
            List<ViewOrganizationSetting> listSettings = new List<ViewOrganizationSetting>();
            listSettings = theSettings.list;

            if (listSettings.Find(y => y.setting_name.ToUpper() == "USE_COVID19").setting_value.ToUpper() == "TRUE")
            {
                string flagCovid = JsongetPatientHistory.list.resumeheader.isCOVID.ToString();
                if (flagCovid.ToLower() == "true")
                {
                    ImageCovid.Visible = true;
                }
                else if (flagCovid.ToLower() == "false")
                {
                    ImageCovid.Visible = false;
                }
            }

            //AdmissionType.Text = JsongetPatientHistory.list.resumeheader.AdmissionType.ToString();
            //lblmrno.Text = JsongetPatientHistory.list.resumeheader.LocalMrNo.ToString();
            //lblnamepatient.Text = JsongetPatientHistory.list.resumeheader.PatientName.ToString();
            //lbldobpatient.Text = JsongetPatientHistory.list.resumeheader.BirthDate.ToString() + "/" + JsongetPatientHistory.list.resumeheader.Age.ToString();
            //lblsexpatient.Text = JsongetPatientHistory.list.resumeheader.Gender.ToString();
            //lbldoctorprimary.Text = JsongetPatientHistory.list.resumeheader.DoctorName.ToString();


            //AdmissionDate.Text = DateTime.Parse(JsongetPatientHistory.list.resumeheader.admissionDate.ToString()).ToString("dd MMM yyyy");
            //PatientData.Text = "MR No. " + JsongetPatientHistory.list.resumeheader.localMrNo.ToString() + " - " + JsongetPatientHistory.list.resumeheader.patientName.ToString();
            //Age.Text = JsongetPatientHistory.list.resumeheader.age.ToString();
            //hfpreviewpres.Value = Data.Result.ToString();

            List<ResumeData> resumedata = JsongetPatientHistory.list.resumedata;
            List<ResumeFA> resumefa = JsongetPatientHistory.list.resumefa;
            List<ResumePrescription> resumepres = JsongetPatientHistory.list.resumeprescription;
            DataTable dt = Helper.ToDataTable(JsongetPatientHistory.list.resumeprescription);
            List<PatientHistoryCPOE> resumecpoe = JsongetPatientHistory.list.resumecpoe;
            List<ResumeProcedureDiagnostic> resumeprocedurediagnostic = JsongetPatientHistory.list.resumeprocedurediagnostic;

            #region ChiefComplain
            string chiefcomplain = (from a in resumedata
                                    where a.MappingId == Guid.Parse("e851f782-8210-49eb-a074-f26c104f5ddf")
                                    select a.Remarks == "" ? "-" : a.Remarks).SingleOrDefault().ToString();
            chiefcomplain = chiefcomplain.Replace("\n", "<br>");
            lblChiefComplaint.Text = chiefcomplain;
            #endregion

            #region Anamnesis
            string tempanamnesis = (from a in resumedata
                                    where a.MappingId == Guid.Parse("2874a832-8503-4cad-b5dd-535775e94ac0")
                                    select a.Remarks == "" ? "-" : a.Remarks).SingleOrDefault().ToString();
            tempanamnesis = tempanamnesis.Replace("\n", "<br>");
            Anamnesis.Text = tempanamnesis;
            var pregnant = (from a in resumedata
                            where a.MappingId == Guid.Parse("078147ba-9e11-4da0-86fa-8bd901d82923")
                            select a.Value).SingleOrDefault().ToString();
            if (pregnant.ToLower() == "true")
                lblispregnant.Text = ": Yes";

            var breastfeed = (from a in resumedata
                              where a.MappingId == Guid.Parse("cf87f125-f2f9-4689-aa5e-91eb26571937")
                              select a.Value).SingleOrDefault().ToString();

            if (breastfeed.ToLower() == "true")
                lblbreastfeed.Text = ": Yes";

            #endregion

            #region Medication & Allergy

            var routine = (from a in resumefa
                           where a.Type == "RoutineMedication"
                           select a).ToList();

            if (routine.Count > 0)
            {
                DataTable dtroutine = Helper.ToDataTable(routine);
                rptRoutine.DataSource = dtroutine;
                rptRoutine.DataBind();
                lblnoroute.Visible = false;
            }
            else
            {
                DataTable dtroutine = null;
                rptRoutine.DataSource = dtroutine;
                rptRoutine.DataBind();
                lblnoroute.Visible = true;
            }


            var drugsallergy = (from a in resumefa
                                where a.Type == "DrugAllergy"
                                select a).ToList();

            if (drugsallergy.Count > 0)
            {
                rptDrugAllergies.DataSource = drugsallergy;
                rptDrugAllergies.DataBind();
                lblnodrugallergy.Visible = false;
            }
            else
            {
                rptDrugAllergies.DataSource = null;
                rptDrugAllergies.DataBind();
                lblnodrugallergy.Visible = true;
            }



            var foodallergy = (from a in resumefa
                               where a.Type == "FoodAllergy"
                               select a).ToList();

            if (foodallergy.Count > 0)
            {
                rptFoodAllergies.DataSource = foodallergy;
                rptFoodAllergies.DataBind();
                lblnofoodallergy.Visible = false;
            }
            else
            {
                rptFoodAllergies.DataSource = null;
                rptFoodAllergies.DataBind();
                lblnofoodallergy.Visible = true;
            }

            var otherallergy = (from a in resumefa
                                where a.Type == "OtherAllergy"
                                select a).ToList();

            if (otherallergy.Count > 0)
            {
                rptOtherAllergies.DataSource = otherallergy;
                rptOtherAllergies.DataBind();
                lblnootherallergy.Visible = false;
            }
            else
            {
                rptOtherAllergies.DataSource = null;
                rptOtherAllergies.DataBind();
                lblnootherallergy.Visible = true;
            }


            #endregion

            #region Illness History
            //IllnessHistory.DataSource = null;
            //var illness = (from a in resumedata
            //                             where a.MappingId == Guid.Parse("1979DDCB-33BC-4187-BE92-04FBDB0A50D6")
            //                             select new { remarks = a.Remarks }).ToList();

            //IllnessHistory.DataSource = illness;
            //IllnessHistory.DataBind();

            var surgery = (from a in resumefa
                           where a.Type == "Surgery"
                           select a).ToList();

            if (surgery.Count > 0)
            {
                rptSurgeryHistory.DataSource = surgery;
                rptSurgeryHistory.DataBind();
                lblnosurgery.Visible = false;
            }
            else
            {
                rptSurgeryHistory.DataSource = null;
                rptSurgeryHistory.DataBind();
                lblnosurgery.Visible = true;
            }


            var personaldisease = (from a in resumefa
                                   where a.Type == "PersonalDisease" && a.Value != "HAD" && a.Value != "PRT" && a.Value != "RHN" && a.Value != "MRS" && a.Value != "COVID"
                                   select a).ToList();

            foreach (var x in personaldisease.Where(x => x.Value == "Lain-lain"))
            {
                x.Value = x.Remarks;
            }

            if (personaldisease.Count > 0)
            {
                rptDiseaseHistory.DataSource = personaldisease;
                rptDiseaseHistory.DataBind();
                lblnodisease.Visible = false;
            }
            else
            {
                rptDiseaseHistory.DataSource = null;
                rptDiseaseHistory.DataBind();
                lblnodisease.Visible = true;
            }


            var familydisease = (from a in resumefa
                                 where a.Type == "FamilyDisease"
                                 select a).ToList();

            foreach (var x in familydisease.Where(x => x.Value == "Lain-lain"))
            {
                x.Value = x.Remarks;
            }

            if (familydisease.Count > 0)
            {
                rptFamilyDisease.DataSource = familydisease;
                rptFamilyDisease.DataBind();
                lblnofamilydisease.Visible = false;
            }
            else
            {
                rptFamilyDisease.DataSource = null;
                rptFamilyDisease.DataBind();
                lblnofamilydisease.Visible = true;
            }


            #endregion

            #region Physical Examination
            var gcsEye = (from a in resumedata
                          where a.MappingId == Guid.Parse("A9C5CD3C-1E02-4DB2-A047-2F1983D1315B")
                          select a.Value).SingleOrDefault().ToString();

            var gcsMove = (from a in resumedata
                           where a.MappingId == Guid.Parse("89B583A5-3003-43AB-9693-60EA6181C8D5")
                           select a.Value).SingleOrDefault().ToString();

            var gcsVerbal = (from a in resumedata
                             where a.MappingId == Guid.Parse("FE4CF48C-17A6-4720-AD23-186517DD9C85")
                             select a.Value).SingleOrDefault().ToString();

            if (gcsEye != "")
            {
                switch (gcsEye)
                {
                    case "1":
                        eye.Text = "1. None";
                        break;
                    case "2":
                        eye.Text = "2.To Pressure";
                        break;
                    case "3":
                        eye.Text = "3. To Sound";
                        break;
                    case "4":
                        eye.Text = "4. Spontaneus";
                        break;
                }
            }

            if (gcsMove != "")
            {
                switch (gcsMove)
                {
                    case "1":
                        move.Text = "1. None";
                        break;
                    case "2":
                        move.Text = "2. Extension";
                        break;
                    case "3":
                        move.Text = "3. Flexion to pain stumulus";
                        break;
                    case "4":
                        move.Text = "4. Withdrawns from pain";
                        break;
                    case "5":
                        move.Text = "5. Localizes to pain stimulus";
                        break;
                    case "6":
                        move.Text = "6. Obey Commands";
                        break;
                }
            }

            if (gcsVerbal != "")
            {
                switch (gcsVerbal)
                {
                    case "1":
                        verbal.Text = "1. None";
                        break;
                    case "2":
                        verbal.Text = "2. Incomprehensible sounds";
                        break;
                    case "3":
                        verbal.Text = "3. Inappropriate words";
                        break;
                    case "4":
                        verbal.Text = "4. Confused";
                        break;
                    case "5":
                        verbal.Text = "5. Orientated";
                        break;
                    case "T":
                        verbal.Text = "T. Tracheostomy";
                        break;
                    case "A":
                        verbal.Text = "A. Aphasia";
                        break;
                }
            }

            if (gcsEye != "" && gcsMove != "" && gcsVerbal != "")
            {
                if (gcsVerbal != "T" && gcsVerbal != "A")
                {
                    int tempgcs = int.Parse(gcsEye) + int.Parse(gcsMove) + int.Parse(gcsVerbal);
                    painscore.Text = tempgcs.ToString();
                }
                else
                {
                    painscore.Text = "-";
                }
            }
            else
                painscore.Text = "-";


            //painscore.Text = (from a in resumedata
            //                  where a.MappingId == Guid.Parse("3aae8dc2-484f-4f3c-a01b-1b0c3f107176")
            //                  select a.Value == "" ? "-" : a.Value).SingleOrDefault().ToString();


            lblpainscale.Text = (from a in resumedata
                                 where a.MappingId == Guid.Parse("3aae8dc2-484f-4f3c-a01b-1b0c3f107176")
                                 select a.Value == "" ? "-" : a.Value).SingleOrDefault().ToString();

            bloodpreassure.Text = (from a in resumedata
                                   where a.MappingId == Guid.Parse("E5EFD220-B68E-4652-AD03-D56EF29FCEBB")
                                   select a.Value == "" ? "-" : a.Value).SingleOrDefault().ToString() + " / " +
                                   (from a in resumedata
                                    where a.MappingId == Guid.Parse("AE3CA8C2-EAB0-41B6-9E3E-3ECB8071A9D0")
                                    select a.Value == "" ? "-" : a.Value + " mmHg").SingleOrDefault().ToString();

            pulse.Text = (from a in resumedata
                          where a.MappingId == Guid.Parse("78cbb61f-4a11-470a-b770-1a44eb04357f")
                          select a.Value == "" ? "-" : a.Value + " x/mnt").SingleOrDefault().ToString();

            respiratory.Text = (from a in resumedata
                                where a.MappingId == Guid.Parse("e6ae2ea9-b321-4756-bf96-2dc232e4a7ba")
                                select a.Value == "" ? "-" : a.Value + "x/mnt").SingleOrDefault().ToString();

            spo.Text = (from a in resumedata
                        where a.MappingId == Guid.Parse("e903246c-df95-4fe0-96d2-cf90c036d3d7")
                        select a.Value == "" ? "-" : a.Value + " %").SingleOrDefault().ToString();

            temperature.Text = (from a in resumedata
                                where a.MappingId == Guid.Parse("2eeca752-a2ea-4426-b3cf-c1ea3833bf30")
                                select a.Value == "" ? "-" : a.Value + " °C").SingleOrDefault().ToString();

            weight.Text = (from a in resumedata
                           where a.MappingId == Guid.Parse("52ce9350-bfb2-4072-8893-d0c6cf8b3634")
                           select a.Value == "" ? "-" : a.Value + " kg").SingleOrDefault().ToString();

            height.Text = (from a in resumedata
                           where a.MappingId == Guid.Parse("2a8dbddb-edfe-4704-876e-5a2d735bb541")
                           select a.Value == "" ? "-" : a.Value + " cm").SingleOrDefault().ToString();

            headcircumference.Text = (from a in resumedata
                                      where a.MappingId == Guid.Parse("A8E0013B-0443-4E7A-B670-4DB9362B40E4")
                                      select a.Value == "" ? "-" : a.Value + " cm").SingleOrDefault().ToString();



            lblmentalstatus.Text = (from a in resumedata
                                    where a.MappingId == Guid.Parse("73cc7d5a-e5a8-4c5d-938d-0f1209d316c2")
                                    select a.Remarks == "" ? "-" : a.Remarks).SingleOrDefault().ToString();

            lblconsciousness.Text = (from a in resumedata
                                     where a.MappingId == Guid.Parse("b4b56979-123c-445c-907a-45cd26f9c909")
                                     select a.Value == "" ? "-" : a.Value).SingleOrDefault().ToString();

            string physicalexamination = (from a in resumedata
                                          where a.MappingId == Guid.Parse("7218971c-e89f-4172-ae3c-b7fb855c1d6d")
                                          select a.Remarks == "" ? "-" : a.Remarks).SingleOrDefault().ToString();
            physicalexamination = physicalexamination.Replace("\n", "<br>");
            lblphysicalexamination.Text = physicalexamination;

            var fallrisk = (from a in resumedata
                            where a.MappingId == Guid.Parse("dc2b9915-0e92-44c2-b66c-2c3eff5a489c")
                            select a).ToList();

            if (fallrisk[0].Remarks == "")
            {
                lblnofallrisk.Visible = true;
                rptfallrisk.Visible = false;
            }
            else
            {
                lblnofallrisk.Visible = false;
                rptfallrisk.Visible = true;
            }


            if (fallrisk.Count > 0)
            {
                foreach (var x in fallrisk)
                {
                    if (x.Value.ToUpper() == "undergo sedation".ToUpper())
                    {
                        x.Value = "Patient undergo sedation";
                    }
                    else if (x.Value.ToUpper() == "physical limitation".ToUpper())
                    {
                        x.Value = "Patient with physical limitation";
                    }
                    else if (x.Value.ToUpper() == "motion aids".ToUpper())
                    {
                        x.Value = "Patient with motion aids";
                    }
                    else if (x.Value.ToUpper() == "Disorder".ToUpper())
                    {
                        x.Value = "Patient with balance disorder";
                    }
                    else if (x.Value.ToUpper() == "fasting".ToUpper())
                    {
                        x.Value = "Fasting patient to undergo further test(Lab/Radiology/etc)";
                    }
                }
                rptfallrisk.DataSource = fallrisk;
                rptfallrisk.DataBind();
            }
            else
            {
                rptfallrisk.DataSource = null;
                rptfallrisk.DataBind();
            }


            //weight.Text = (from a in resumedata
            //               where a.mappingId == Guid.Parse("52ce9350-bfb2-4072-8893-d0c6cf8b3634")
            //               select a.value).SingleOrDefault().ToString();

            //breathing.Text = (from a in resumedata
            //                  where a.mappingId == Guid.Parse("E6AE2EA9-B321-4756-BF96-2DC232E4A7BA")
            //                  select a.value).SingleOrDefault().ToString();

            //mentalstatus.Text = (from a in resumedata
            //                     where a.mappingId == Guid.Parse("73cc7d5a-e5a8-4c5d-938d-0f1209d316c2")
            //                     select a.value).SingleOrDefault().ToString();

            #endregion

            #region procedure

            string procedurevalue = (from a in resumedata
                                     where a.MappingId == Guid.Parse("337a371f-baf5-424a-bdc5-c320c277cac6")
                                     select a.Remarks == "" ? "-" : a.Remarks).SingleOrDefault().ToString();
            procedurevalue = procedurevalue.Replace("\n", "<br>");
            procedure.Text = procedurevalue;
            #endregion

            #region procedureResult

            string procedureResultvalue = (from a in resumedata
                                           where a.MappingId == Guid.Parse("3E20F648-32BA-4D1D-B390-A3C372A7D30A")
                                           select a.Remarks == "  " ? "-" : a.Remarks).SingleOrDefault().ToString();
            procedureResultvalue = procedureResultvalue.Replace("\n", "<br>");
            procedureResult.Text = procedureResultvalue;
            #endregion

            #region diagnosis
            string primarydiagnosisvalue = (from a in resumedata
                                            where a.MappingId == Guid.Parse("d24d0881-7c06-4563-bf75-3a20b843dc47")
                                            select a.Remarks == "" ? "-" : a.Remarks).SingleOrDefault().ToString();
            primarydiagnosisvalue = primarydiagnosisvalue.Replace("\n", "<br>");
            primarydiagnosis.Text = primarydiagnosisvalue;
            #endregion

            #region cpoe

            var labdata = (from a in resumecpoe
                           where a.template_name == "LAB" && a.is_future_order == false
                           select a).ToList();

            if (labdata.Count > 0)
            {
                rptLabHistory.DataSource = labdata;
                rptLabHistory.DataBind();
                lblnolab.Visible = false;
            }
            else
            {
                rptLabHistory.DataSource = null;
                rptLabHistory.DataBind();
                lblnolab.Visible = true;
            }

            string templabother = (from a in resumedata
                                    where a.MappingId == Guid.Parse("5B39A9B4-744B-4AD3-954F-386E32220ABE")
                                    select a.Remarks == "" ? "-" : a.Remarks).SingleOrDefault().ToString();
            templabother = templabother.Replace("\n", "<br>");
            Lbl_OtherLab.Text = templabother;

            var labdataFO = (from a in resumecpoe
                           where a.template_name == "LAB" && a.is_future_order == true
                           select a).ToList();

            if (labdataFO.Count > 0)
            {
                lblLab_FO_Date.Text = labdataFO[0].future_order_date.ToString("dd MMM yyyy");
                lblLab_FO_Date2.Text = labdataFO[0].future_order_date.ToString("dd MMM yyyy");
                rptLabHistoryFO.DataSource = labdataFO;
                rptLabHistoryFO.DataBind();
                lblnolabFO.Visible = false;
           
            }
            else
            {
                rptLabHistoryFO.DataSource = null;
                rptLabHistoryFO.DataBind();
                lblnolabFO.Visible = true;
            }

            string templabotherfo = (from a in resumedata
                                   where a.MappingId == Guid.Parse("E794E3D3-7860-4D52-9166-9A5DF3127E55")
                                   select a.Remarks == "" ? "-" : a.Remarks).SingleOrDefault().ToString();
            templabotherfo = templabotherfo.Replace("\n", "<br>");
            Lbl_OtherLab_FO.Text = templabotherfo;

            if (templabotherfo != "-")
            {
                string templabotherfodate = (from a in resumedata
                                         where a.MappingId == Guid.Parse("E794E3D3-7860-4D52-9166-9A5DF3127E55")
                                         select a.Value == "" ? "-" : a.Value).SingleOrDefault().ToString();
                lblLab_FO_Date_other.Text = templabotherfodate;
                lblLab_FO_Date2_other.Text = templabotherfodate;
            }

            var raddata = (from a in resumecpoe
                           where a.template_name == "RAD" && a.is_future_order == false
                           select a).ToList();

            if (raddata.Count > 0)
            {
                rptRadHistory.DataSource = raddata;
                rptRadHistory.DataBind();
                lblnorad.Visible = false;
            }
            else
            {
                rptRadHistory.DataSource = null;
                rptRadHistory.DataBind();
                lblnorad.Visible = true;
            }

            string tempradother = (from a in resumedata
                                   where a.MappingId == Guid.Parse("61764B36-4BF4-4A03-917E-695E6929AFB3")
                                   select a.Remarks == "" ? "-" : a.Remarks).SingleOrDefault().ToString();
            tempradother = tempradother.Replace("\n", "<br>");
            Lbl_OtherRad.Text = tempradother;

            var raddataFO = (from a in resumecpoe
                           where a.template_name == "RAD" && a.is_future_order == true
                           select a).ToList();

            if (raddataFO.Count > 0)
            {
                lblRad_FO_Date.Text = raddataFO[0].future_order_date.ToString("dd MMM yyyy");
                lblRad_FO_Date2.Text = raddataFO[0].future_order_date.ToString("dd MMM yyyy");
                rptRadHistoryFO.DataSource = raddataFO;
                rptRadHistoryFO.DataBind();
                lblnoradFO.Visible = false;
            }
            else
            {
                rptRadHistoryFO.DataSource = null;
                rptRadHistoryFO.DataBind();
                lblnoradFO.Visible = true;
            }

            string tempradotherfo = (from a in resumedata
                                     where a.MappingId == Guid.Parse("0EE8A241-73A8-49DB-8F4B-8733CDB92C8F")
                                     select a.Remarks == "" ? "-" : a.Remarks).SingleOrDefault().ToString();
            tempradotherfo = tempradotherfo.Replace("\n", "<br>");
            Lbl_OtherRad_FO.Text = tempradotherfo;

            if (tempradotherfo != "-")
            {
                string tempradotherfodate = (from a in resumedata
                                         where a.MappingId == Guid.Parse("0EE8A241-73A8-49DB-8F4B-8733CDB92C8F")
                                         select a.Value == "" ? "-" : a.Value).SingleOrDefault().ToString();
                lblRad_FO_Date_other.Text = tempradotherfodate;
                lblRad_FO_Date2_other.Text = tempradotherfodate;
            }

            #endregion

            #region prescription
            prescriptiondrugs.DataSource = null;
            RepeaterRacikan.DataSource = null;
            prescriptioncompound.DataSource = null;
            prescriptionconsumables.DataSource = null;


            List<ResumePrescription> drugspresc = (from a in resumepres
                                                   where a.isConsumables == false && a.compound_id == Guid.Empty
                                                   select a).ToList();

            List<ResumePrescription> compheaderpresc = (from a in resumepres
                                                        where a.salesItemName == ""
                                                        select a).ToList();

            List<ResumePrescription> consumables = (from a in resumepres
                                                    where a.isConsumables == true
                                                    select a).ToList();

            List<CompoundHeaderSoap> racikanheader = JsongetPatientHistory.list.compound_header;

            List<CompoundHeaderSoap> tmpracikanheader = (from a in racikanheader
                                                         where a.is_additional == false
                                                         select a).ToList();

            List<CompoundHeaderSoap> tmpracikanheaderadd = (from a in racikanheader
                                                            where a.is_additional == true
                                                            select a).ToList();

            foreach (var templist in tmpracikanheader)
            {
                string a = templist.quantity.ToString().Substring(0, 3);
                string[] tempqty = templist.quantity.ToString().Split('.');
                if (tempqty[1].Length == 3)
                {
                    if (tempqty[1] == "000")
                    {
                        templist.quantity = decimal.Parse(tempqty[0]).ToString();
                    }
                    else if (tempqty[1].Substring(tempqty[1].Length - 2) == "00")
                    {
                        templist.quantity = tempqty[0] + "." + tempqty[1].Substring(0, 1);
                    }
                    else if (tempqty[1].Substring(tempqty[1].Length - 1) == "0")
                    {
                        templist.quantity = tempqty[0] + "." + tempqty[1].Substring(0, 2);
                    }
                }

                string[] tempdose = templist.dose.ToString().Split('.');

                if (tempdose[1].Length == 3)
                {
                    if (tempdose[1] == "000")
                    {
                        templist.dose = decimal.Parse(tempdose[0]).ToString();
                    }
                    else if (tempdose[1].Substring(tempdose[1].Length - 2) == "00")
                    {
                        templist.dose = tempdose[0] + "." + tempdose[1].Substring(0, 1);
                    }
                    else if (tempdose[1].Substring(tempdose[1].Length - 1) == "0")
                    {
                        templist.dose = tempdose[0] + "." + tempdose[1].Substring(0, 2);
                    }
                }
            }

            foreach (var templist in tmpracikanheaderadd)
            {
                string a = templist.quantity.ToString().Substring(0, 3);
                string[] tempqty = templist.quantity.ToString().Split('.');
                if (tempqty[1].Length == 3)
                {
                    if (tempqty[1] == "000")
                    {
                        templist.quantity = decimal.Parse(tempqty[0]).ToString();
                    }
                    else if (tempqty[1].Substring(tempqty[1].Length - 2) == "00")
                    {
                        templist.quantity = tempqty[0] + "." + tempqty[1].Substring(0, 1);
                    }
                    else if (tempqty[1].Substring(tempqty[1].Length - 1) == "0")
                    {
                        templist.quantity = tempqty[0] + "." + tempqty[1].Substring(0, 2);
                    }
                }

                string[] tempdose = templist.dose.ToString().Split('.');

                if (tempdose[1].Length == 3)
                {
                    if (tempdose[1] == "000")
                    {
                        templist.dose = decimal.Parse(tempdose[0]).ToString();
                    }
                    else if (tempdose[1].Substring(tempdose[1].Length - 2) == "00")
                    {
                        templist.dose = tempdose[0] + "." + tempdose[1].Substring(0, 1);
                    }
                    else if (tempdose[1].Substring(tempdose[1].Length - 1) == "0")
                    {
                        templist.dose = tempdose[0] + "." + tempdose[1].Substring(0, 2);
                    }
                }
            }


            //List<ResumeDrugs> listpres = (from a in JsongetPatientHistory.list.resumeprescription
            //                where a.compound_id == Guid.Empty && a.isConsumables == false
            //                select new ResumeDrugs
            //                {
            //                    salesItemName = a.salesItemName,
            //                    Remarks = string.IsNullOrEmpty(a.doseText) ? a.quantity.ToString("0.###") + a.uom.ToString() + " - " + a.frequency.ToString() + " (" + a.instruction.ToString() + ") " + a.route.ToString() : a.doseText
            //                }).ToList();
            //DataTable dttemp = Helper.ToDataTable(drugspresc);

            foreach (var x in drugspresc)
            {
                if (x.isRoutine)
                    x.routine = "Yes";
                else
                    x.routine = "No";
            }

            if (drugspresc.Count > 0)
            {
                drugs.Visible = false;
                divdrugsprescription.Visible = true;
                prescriptiondrugs.DataSource = drugspresc.Where(a => a.IsAdditional == 0);
                prescriptiondrugs.DataBind();
            }
            else
            {
                drugs.Visible = true;
                divdrugsprescription.Visible = false;
                prescriptiondrugs.DataSource = null;
                prescriptiondrugs.DataBind();
            }

            if (drugspresc.Where(a => a.IsAdditional == 1).Count() > 0)
            {
                trAdditionalDrugs.Visible = true;
                lbladditionalpres.Visible = false;
                dvAdditionalPres.Visible = true;
                rptAdditionalDrugs.DataSource = drugspresc.Where(a => a.IsAdditional == 1);
                rptAdditionalDrugs.DataBind();
            }
            else
            {
                trAdditionalDrugs.Visible = true;
                lbladditionalpres.Visible = true;
                dvAdditionalPres.Visible = false;
                rptAdditionalDrugs.DataSource = null;
                rptAdditionalDrugs.DataBind();
            }
            //dikomen untuk menampilkan semua obat yg ada untuk case dokter submit hanya presc note

            if (consumables.Count > 0)
            {
                cons.Visible = false;
                divconsumables.Visible = true;
                prescriptionconsumables.DataSource = consumables.Where(a => a.IsAdditional == 0);
                prescriptionconsumables.DataBind();
            }
            else
            {
                cons.Visible = true;
                divconsumables.Visible = false;
                prescriptionconsumables.DataSource = null;
                prescriptionconsumables.DataBind();
            }

            if (consumables.Where(a => a.IsAdditional == 1).Count() > 0)
            {
                trAdditionalConsumables.Visible = true;
                lbladditionalcons.Visible = false;
                dvAdditionalConsumables.Visible = true;

                rptAdditionalConsumables.DataSource = consumables.Where(a => a.IsAdditional == 1);
                rptAdditionalConsumables.DataBind();
            }
            else
            {
                trAdditionalConsumables.Visible = true;
                lbladditionalcons.Visible = true;
                dvAdditionalConsumables.Visible = false;
                rptAdditionalConsumables.DataSource = null;
                rptAdditionalConsumables.DataBind();
            }

            if (tmpracikanheader.Count > 0)
            {
                noracikan.Visible = false;
                divRacikan.Visible = true;
                RepeaterRacikan.DataSource = tmpracikanheader;
                RepeaterRacikan.DataBind();
            }
            else
            {
                noracikan.Visible = true;
                divRacikan.Visible = false;
                RepeaterRacikan.DataSource = null;
                RepeaterRacikan.DataBind();
            }

            if (tmpracikanheaderadd.Count > 0)
            {
                trAdditionalRacikan.Visible = true;
                noracikanadd.Visible = false;
                divRacikanAdditional.Visible = true;
                RepeaterRAcikanAdd.DataSource = tmpracikanheaderadd;
                RepeaterRAcikanAdd.DataBind();
            }
            else
            {
                trAdditionalRacikan.Visible = true;
                noracikanadd.Visible = true;
                divRacikanAdditional.Visible = false;
                RepeaterRAcikanAdd.DataSource = null;
                RepeaterRAcikanAdd.DataBind();
            }

            List<CompoundHeaderSoap> listpresracikanheader = JsongetPatientHistory.list.compound_header;
            List<CompoundDetailSoap> listpresracikandetail = JsongetPatientHistory.list.compound_detail;

            List<CompoundHeaderSoap> templistpresracikanheader = listpresracikanheader.Where(x => x.is_additional == false).ToList();
            List<CompoundDetailSoap> templistpresracikandetail = listpresracikandetail.Where(x => x.is_additional == false).ToList();

            List<CompoundHeaderSoap> templistpresracikanheaderadd = listpresracikanheader.Where(x => x.is_additional == true).ToList();
            List<CompoundDetailSoap> templistpresracikandetailadd = listpresracikandetail.Where(x => x.is_additional == true).ToList();

            if (templistpresracikanheader.Count > 0)
            {
                foreach (var templist in templistpresracikanheader)
                {
                    templist.dose = Helper.formatDecimal(templist.dose);
                    templist.quantity = Helper.formatDecimal(templist.quantity);
                }

                DataTable dtracikan = Helper.ToDataTable(templistpresracikanheader);

                if (templistpresracikandetail.Count > 0)
                {
                    foreach (var templist in templistpresracikandetail)
                    {
                        templist.dose = Helper.formatDecimal(templist.dose);
                    }

                    DataTable dtracikandetail = Helper.ToDataTable(templistpresracikandetail);
                    Session[Helper.SessionRacikanDetail] = dtracikandetail;
                }

                gvw_racikan_header.DataSource = dtracikan;
                gvw_racikan_header.DataBind();
            }
            else
            {
                gvw_racikan_header.DataSource = null;
                gvw_racikan_header.DataBind();
                Session[Helper.SessionRacikanDetail] = null;
            }

            if (templistpresracikanheaderadd.Count > 0)
            {
                foreach (var templist in templistpresracikanheaderadd)
                {
                    templist.dose = Helper.formatDecimal(templist.dose);
                    templist.quantity = Helper.formatDecimal(templist.quantity);
                }

                DataTable dtracikan = Helper.ToDataTable(templistpresracikanheaderadd);

                if (templistpresracikandetailadd.Count > 0)
                {
                    foreach (var templist in templistpresracikandetailadd)
                    {
                        templist.dose = Helper.formatDecimal(templist.dose);
                    }

                    DataTable dtracikandetail = Helper.ToDataTable(templistpresracikandetailadd);
                    Session[Helper.SessionRacikanDetailAdd] = dtracikandetail;
                }

                gvw_racikan_header_add.DataSource = dtracikan;
                gvw_racikan_header_add.DataBind();
            }
            else
            {
                gvw_racikan_header_add.DataSource = null;
                gvw_racikan_header_add.DataBind();
                Session[Helper.SessionRacikanDetailAdd] = null;
            }


            #region doctornotes
            string drugsNoteByDoctorvalue = (from a in resumedata
                                             where a.MappingId == Guid.Parse("2DF0294D-F94E-4BA4-8BA1-F017BFB55D92")
                                             select a.Remarks == "" ? "-" : a.Remarks).SingleOrDefault().ToString();
            drugsNoteByDoctorvalue = drugsNoteByDoctorvalue.Replace("\n", "<br>");
            drugsNoteByDoctor.Text = drugsNoteByDoctorvalue;
            #endregion

            #region doctornotesadditional
            string additionalDrugsNoteByDoctorValue = (from a in resumedata
                                                       where a.MappingId == Guid.Parse("5E34AE60-1D72-4EFD-8440-C4442515AABE")
                                                       select a.Remarks == "" ? "-" : a.Remarks).SingleOrDefault().ToString();
            additionalDrugsNoteByDoctorValue = additionalDrugsNoteByDoctorValue.Replace("\n", "<br>");
            additionalDrugsNoteByDoctor.Text = additionalDrugsNoteByDoctorValue;
            #endregion



            List<ResumeDrugs> listcompheader = (from a in (List<ResumePrescription>)JsongetPatientHistory.list.resumeprescription
                                                where a.salesItemName == "" && a.isConsumables == false
                                                select new ResumeDrugs
                                                {
                                                    salesItemName = a.compound_name,
                                                    Remarks = string.IsNullOrEmpty(a.doseText) ? a.quantity.ToString("0.###") + a.uom.ToString() + " - " + a.frequency.ToString() + " (" + a.instruction.ToString() + ") " + a.route.ToString() : a.doseText
                                                }).ToList();

            if (compheaderpresc.Count > 0)
            {
                compound.Visible = false;
                divcompoundprescription.Visible = true;
                prescriptioncompound.DataSource = compheaderpresc;
                prescriptioncompound.DataBind();
            }
            else
            {
                compound.Visible = true;
                divcompoundprescription.Visible = false;
                prescriptioncompound.DataSource = null;
                prescriptioncompound.DataBind();
            }

            var orgsetting = clsCommon.GetOrganizationSettingbyOrgId(OrganizationId);
            var jsonorgsetting = JsonConvert.DeserializeObject<ResultViewOrganizationSetting>(orgsetting.Result.ToString());
            ViewOrganizationSetting tempvaluecompound = jsonorgsetting.list.Find(y => y.setting_name == "IS_COMPOUND");

            if (tempvaluecompound.setting_value == "FALSE")
            {
                //trcompound.Visible = false;
            }
            else
            {
                //trcompound.Visible = true;
            }

            List<ResumeDrugs> listcons = (from a in (List<ResumePrescription>)JsongetPatientHistory.list.resumeprescription
                                          where a.isConsumables == true
                                          select new ResumeDrugs
                                          {
                                              salesItemName = a.salesItemName,
                                              Remarks = string.IsNullOrEmpty(a.doseText) ? a.quantity.ToString("0.###") + a.uom.ToString() + " - " + a.frequency.ToString() + " (" + a.instruction.ToString() + ") " + a.route.ToString() : a.doseText
                                          }).ToList();



            #endregion

            #region procedure diagnostic

            var diagnosticdata = (from a in resumeprocedurediagnostic
                                  where a.SalesItemType.ToUpper() == "DIAGNOSTIC" && a.IsFutureOrder == false
                                  select a).ToList();

            if (diagnosticdata.Count > 0)
            {
                rptDiagnostic.DataSource = diagnosticdata;
                rptDiagnostic.DataBind();
                lblnodiagnostic.Visible = false;
            }
            else
            {
                rptDiagnostic.DataSource = null;
                rptDiagnostic.DataBind();
                lblnodiagnostic.Visible = true;
            }

            string tempdiagnosticother = (from a in resumedata
                                          where a.MappingId == Guid.Parse("35779378-FC19-41B5-8445-D6C6D358BDE5")
                                          select a.Remarks == "" ? "-" : a.Remarks).SingleOrDefault().ToString();
            tempdiagnosticother = tempdiagnosticother.Replace("\n", "<br>");
            Lbl_OtherDiagnostic.Text = tempdiagnosticother;

            var diagnosticdataFO = (from a in resumeprocedurediagnostic
                                    where a.SalesItemType.ToUpper() == "DIAGNOSTIC" && a.IsFutureOrder == true
                                    select a).ToList();

            if (diagnosticdataFO.Count > 0)
            {
                lblDiagnostic_FO_Date.Text = diagnosticdataFO[0].FutureOrderDate.ToString("dd MMM yyyy");
                lblDiagnostic_FO_Date2.Text = diagnosticdataFO[0].FutureOrderDate.ToString("dd MMM yyyy");
                rptDiagnosticFO.DataSource = diagnosticdataFO;
                rptDiagnosticFO.DataBind();
                lblnodiagnosticFO.Visible = false;

            }
            else
            {
                rptDiagnosticFO.DataSource = null;
                rptDiagnosticFO.DataBind();
                lblnodiagnosticFO.Visible = true;
            }

            string tempdiagnosticotherfo = (from a in resumedata
                                            where a.MappingId == Guid.Parse("DFA41B67-0EDC-45A8-BD69-5E6883FADEF2")
                                            select a.Remarks == "" ? "-" : a.Remarks).SingleOrDefault().ToString();
            tempdiagnosticotherfo = tempdiagnosticotherfo.Replace("\n", "<br>");
            Lbl_OtherDiagnostic_FO.Text = tempdiagnosticotherfo;

            if (tempdiagnosticotherfo != "-")
            {
                string tempdiagnosticotherfodate = (from a in resumedata
                                                    where a.MappingId == Guid.Parse("DFA41B67-0EDC-45A8-BD69-5E6883FADEF2")
                                                    select a.Value == "" ? "-" : a.Value).SingleOrDefault().ToString();
                lblDiagnostic_FO_Date_other.Text = DateTime.Parse(tempdiagnosticotherfodate).ToString("dd MMM yyyy");
                lblDiagnostic_FO_Date2_other.Text = DateTime.Parse(tempdiagnosticotherfodate).ToString("dd MMM yyyy");
            }

            var proceduredata = (from a in resumeprocedurediagnostic
                                 where a.SalesItemType.ToUpper() == "PROCEDURE" && a.IsFutureOrder == false
                                 select a).ToList();

            if (proceduredata.Count > 0)
            {
                rptProcedure.DataSource = proceduredata;
                rptProcedure.DataBind();
                lblnoprocedure.Visible = false;
            }
            else
            {
                rptProcedure.DataSource = null;
                rptProcedure.DataBind();
                lblnoprocedure.Visible = true;
            }

            string tempprocedureother = (from a in resumedata
                                         where a.MappingId == Guid.Parse("E4565CFB-1E9E-47EC-A06E-F21240043289")
                                         select a.Remarks == "" ? "-" : a.Remarks).SingleOrDefault().ToString();
            tempprocedureother = tempprocedureother.Replace("\n", "<br>");
            Lbl_OtherProcedure.Text = tempprocedureother;

            var proceduredataFO = (from a in resumeprocedurediagnostic
                                   where a.SalesItemType.ToUpper() == "PROCEDURE" && a.IsFutureOrder == true
                                   select a).ToList();

            if (proceduredataFO.Count > 0)
            {
                lblProcedure_FO_Date.Text = proceduredataFO[0].FutureOrderDate.ToString("dd MMM yyyy");
                lblProcedure_FO_Date2.Text = proceduredataFO[0].FutureOrderDate.ToString("dd MMM yyyy");
                rptProcedureFO.DataSource = proceduredataFO;
                rptProcedureFO.DataBind();
                lblnoprocedureFO.Visible = false;
            }
            else
            {
                rptProcedureFO.DataSource = null;
                rptProcedureFO.DataBind();
                lblnoprocedureFO.Visible = true;
            }

            string tempprocedureotherfo = (from a in resumedata
                                           where a.MappingId == Guid.Parse("68C2FF43-C93B-4FFB-84FF-C7BEBECA72C5")
                                           select a.Remarks == "" ? "-" : a.Remarks).SingleOrDefault().ToString();
            tempprocedureotherfo = tempprocedureotherfo.Replace("\n", "<br>");
            Lbl_OtherProcedure_FO.Text = tempprocedureotherfo;

            if (tempprocedureotherfo != "-")
            {
                string tempprocedureotherfodate = (from a in resumedata
                                                   where a.MappingId == Guid.Parse("68C2FF43-C93B-4FFB-84FF-C7BEBECA72C5")
                                                   select a.Value == "" ? "-" : a.Value).SingleOrDefault().ToString();
                lblProcedure_FO_Date_other.Text = DateTime.Parse(tempprocedureotherfodate).ToString("dd MMM yyyy");
                lblProcedure_FO_Date2_other.Text = DateTime.Parse(tempprocedureotherfodate).ToString("dd MMM yyyy");
            }

            #endregion

            lblNamaDokter.Text = JsongetPatientHistory.list.resumeheader.DoctorName.ToString();
            lblCreatedOrderDate.Text = JsongetPatientHistory.list.resumeheader.CreatedDate;
            lblModifiedOrderDate.Text = JsongetPatientHistory.list.resumeheader.ModifiedDate;
            //lblPrintDate.Text = DateTime.Now.ToString("dd MMM yyyy HH:mm");
            //lblPrintedBy.Text = Request.QueryString["PrintBy"].ToString();



            //SPECIALTY

            //set sesuai templatenya
            string templateid = Request.QueryString["PageSOAP"];
            if (templateid.ToUpper() == "00000000-0000-0000-0000-000000000000" || templateid.ToUpper() == "7CCD0A7E-9001-48FF-8052-ED07CF5716BB" || templateid.ToUpper() == "882854F0-780A-48BB-89EC-A6FF7519D10B")
            {

            }
            else if (templateid.ToUpper() == "711671B0-A2B3-4311-9B89-69C146FDAE3A") //EMERGENCY
            {
                LoadEmergencyData(resumedata);
                divemergency1.Visible = true;
            }
            else if (templateid.ToUpper() == "903E0F23-2C60-41F1-8C04-EB3E70D1E002") //OBGYN
            {
                List<ResumepregnancydataSOAP> resumedatakehamilan = JsongetPatientHistory.list.resumepregnancydata;
                LoadObgynData(resumedatakehamilan);

                divobgyn1.Visible = true;
            }
            else if (templateid.ToUpper() == "01D7A679-92EF-4A56-B040-1614B3C9EFAF") //PEDIATRIC
            {
                List<ResumepediatricdataSOAP> resumedatapediatric = JsongetPatientHistory.list.resumepediatricdata;
                LoadPediatricData(resumedatapediatric);

                divpediatric1.Visible = true;
                tbodypediatric.Visible = true;
            }


            if (templateid.ToUpper() == "00000000-0000-0000-0000-000000000000" || templateid.ToUpper() == "7CCD0A7E-9001-48FF-8052-ED07CF5716BB" || templateid.ToUpper() == "711671B0-A2B3-4311-9B89-69C146FDAE3A" || templateid.ToUpper() == "882854F0-780A-48BB-89EC-A6FF7519D10B")
            {
                List<ResumeDocument> resumedoc = JsongetPatientHistory.list.resumedocument;

                if (resumedoc.Count > 0)
                {
                    divdocremark.Visible = true;
                    StringBuilder documentInnerHTML = new StringBuilder();
                    int i = 0;
                    foreach (ResumeDocument rd in resumedoc)
                    {
                        if (i == 0)
                        {
                            documentInnerHTML.Append("<label style=\"padding:10px;\">Pemeriksaan(Examination): " + rd.image_type_value + "<br/></label><label style=\"padding:10px;\">Catatan(Remarks):" + rd.image_remark + "</label>");
                        }
                        else
                        {
                            documentInnerHTML.Append("<br/><br/><label style=\"padding:10px;\">Pemeriksaan(Examination): " + rd.image_type_value + "<br/></label><label style=\"padding:10px;\">Catatan(Remarks):" + rd.image_remark + "</label>");
                        }
                        i++;
                    }

                    divdocremark.InnerHtml = documentInnerHTML.ToString();
                }
                else
                {
                    divdocremark.Visible = false;
                }
            }

        }
        catch (Exception ex)
        {
            throw ex.InnerException;
        }
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

    protected void gvw_racikan_header_add_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string headerId = gvw_racikan_header_add.DataKeys[e.Row.RowIndex].Value.ToString();
            GridView gvDetails = e.Row.FindControl("gvw_racikan_detail_add") as GridView;

            if (Session[Helper.SessionRacikanDetailAdd] != null)
            {
                DataRow[] dr = ((DataTable)Session[Helper.SessionRacikanDetailAdd]).Select("prescription_compound_header_id = '" + headerId + "'");

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

    protected void Repeater1_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        var data = ((ResumePrescription)e.Item.DataItem).compound_name;
        ResultMedicalResume JsongetPatientHistory = JsonConvert.DeserializeObject<ResultMedicalResume>(hfpreviewpres.Value);
        List<ResumePrescription> listpres = JsongetPatientHistory.list.resumeprescription;
        List<ResumeDrugs> listcompdetail = (from a in listpres
                                            where a.compound_id != Guid.Empty && a.salesItemName != "" && a.compound_name == data
                                            select new ResumeDrugs
                                            {
                                                salesItemName = a.salesItemName,
                                                Remarks = string.IsNullOrEmpty(a.doseText) ? a.quantity.ToString("0.##") + a.uom.ToString() + " - " + a.frequency.ToString() + " (" + a.instruction.ToString() + ") " + a.route.ToString() : a.doseText
                                            }).ToList();
        var repeater2 = (Repeater)e.Item.FindControl("rptCompDetail");
        repeater2.DataSource = listcompdetail;
        repeater2.DataBind();
    }

    public void LoadObgynData(List<ResumepregnancydataSOAP> resumedatakehamilan)
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

    public void LoadPediatricData(List<ResumepediatricdataSOAP> resumedatapediatric)
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

    public void LoadEmergencyData(List<ResumeData> resumedatatriage)
    {
        lblskortriage.Text = (from a in resumedatatriage
                            where a.MappingId == Guid.Parse("EB3330BD-D413-4B70-999C-0D7AB29FBE36")
                            select a.Value == "" ? "-" : a.Value).SingleOrDefault().ToString();

        lbltraumatriage.Text = (from a in resumedatatriage
                              where a.MappingId == Guid.Parse("64B06F14-6480-46DA-8846-9CAC5A499748")
                              select a.Value == "" ? "-" : a.Value).SingleOrDefault().ToString();

        var txt_pasiendatang = resumedatatriage.Find(x => x.MappingId == Guid.Parse("D2D42792-B16F-472B-B5CB-428980C5003E"));
        if (txt_pasiendatang != null)
        {
            if (txt_pasiendatang.Value.ToLower() == "")
            {
                lblpasiendatang.Text = "-";
            }
            else if(txt_pasiendatang.Value.ToLower() == "sendiri")
            {
                lblpasiendatang.Text = txt_pasiendatang.Value;
            }
            else if (txt_pasiendatang.Value.ToLower() == "dirujuk")
            {
                lblpasiendatang.Text = txt_pasiendatang.Value + " oleh " + txt_pasiendatang.Remarks;
            }
            else if (txt_pasiendatang.Value.ToLower() == "lain-lain")
            {
                lblpasiendatang.Text = txt_pasiendatang.Remarks;
            }
        }

        lblkeadaanumum.Text = (from a in resumedatatriage
                                where a.MappingId == Guid.Parse("8C1799E0-C793-4918-A850-1B9BE72359CF")
                                select a.Value == "" ? "-" : a.Value).SingleOrDefault().ToString();

        lblairway.Text = (from a in resumedatatriage
                               where a.MappingId == Guid.Parse("220F9B0A-4F91-4982-BED3-CA2DDEB1884F")
                               select a.Value == "" ? "-" : a.Value).SingleOrDefault().ToString();

        lblbreathing.Text = (from a in resumedatatriage
                          where a.MappingId == Guid.Parse("DF853881-EADC-4DA8-9140-960F962535E3")
                          select a.Value == "" ? "-" : a.Value).SingleOrDefault().ToString();

        lblcirculation.Text = (from a in resumedatatriage
                          where a.MappingId == Guid.Parse("576416E4-AEF9-45BC-A7E0-AE3CEDF6EE94")
                          select a.Value == "" ? "-" : a.Value).SingleOrDefault().ToString();

        lbldisability.Text = (from a in resumedatatriage
                          where a.MappingId == Guid.Parse("058F59BA-7FA9-443A-994A-E848F4FAEE7F")
                          select a.Value == "" ? "-" : a.Value).SingleOrDefault().ToString();
    }
}