using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
public partial class Form_ReferralResumePrint : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
             var registryflag = ConfigurationManager.AppSettings["registryflag"].ToString();

            //if (registryflag == "1")
            //{
            //    ConfigurationManager.AppSettings["urlPharmacy"] = SiloamConfig.Functions.GetValue("urlExtension").ToString();
            //    ConfigurationManager.AppSettings["urlPrescription"] = SiloamConfig.Functions.GetValue("urlTransaction").ToString();
            //    ConfigurationManager.AppSettings["urlFunctional"] = SiloamConfig.Functions.GetValue("urlMaster").ToString();
            //    ConfigurationManager.AppSettings["urlRecord"] = SiloamConfig.Functions.GetValue("urlPharmacy").ToString();
            //    ConfigurationManager.AppSettings["urlMaster"] = SiloamConfig.Functions.GetValue("urlMaster").ToString();
            //    ConfigurationManager.AppSettings["urlHISDataCollection"] = SiloamConfig.Functions.GetValue("urlHISDataCollection").ToString();
            //    ConfigurationManager.AppSettings["urlUserManagement"] = SiloamConfig.Functions.GetValue("urlUserManagement").ToString();
            //    ConfigurationManager.AppSettings["DB_Emr"] = SiloamConfig.Functions.GetValue("DB_Emr").ToString();
            //}
            //if (Request.QueryString["PatientId"] != null && Request.QueryString["AdmissionId"] != null && Request.QueryString["EncounterId"] != null && Request.QueryString["OrganizationId"] != null)
            //{
            //    long PatientId = long.Parse(Request.QueryString["PatientId"]);
            //    long AdmissionId = long.Parse(Request.QueryString["AdmissionId"]);
            //    string AdmissionNo = Request.QueryString["AdmissionNo"];
            //    Guid EncounterId = Guid.Parse(Request.QueryString["EncounterId"]);
            //    long OrganizationId = long.Parse(Request.QueryString["OrganizationId"]);
            //    initializevalue(OrganizationId, Guid.Empty.ToString(), PatientId, AdmissionNo, EncounterId.ToString());

            //}
            //else
            //{
            //    string close = @"<script type='text/javascript'>
            //                    window.returnValue = true;
            //                    window.close();
            //                    </script>";
            //    base.Response.Write(close);
            //}

            initializevalue(2, Guid.Empty.ToString(), 2000002041117, "OPA2209060004", "39836279-1c7c-4a57-94fe-5125081c2f2c");
        }
    }

    public void initializevalue(long OrganizationId, string OperationScheculeId, long PatientId, string AdmissionNo, string EncounterId)
    {
        
        try
        {
            var getDataInpatient = clsInpatient.getRawatInap(OrganizationId, OperationScheculeId, PatientId, AdmissionNo, EncounterId);
            ResultInpatient jsoninpatient = JsonConvert.DeserializeObject<ResultInpatient>(getDataInpatient.Result.ToString());
            InpatientData inpatientData = jsoninpatient.list;
            if (inpatientData != null)
            {
                lbl_dokter.Text = inpatientData.doctor_name;
                lbl_diagnosis.Text = inpatientData.diagnosis;
                //admisiondate
                if (!string.IsNullOrEmpty(inpatientData.admission_date))
                {
                    string[] separateDate = inpatientData.admission_date.Split(' ');
                    string splitDate = separateDate[0];
                    string splitTime = separateDate[1];
                    string timePM_AM = separateDate[2];
                    string[] newadmdate = splitDate.Split('/');
                    int dayadm = Convert.ToInt32(newadmdate[1].Trim());
                    int monthadm = Convert.ToInt32(newadmdate[0].Trim());
                    int yearadm = Convert.ToInt32(newadmdate[2].Trim());
                    lbl_addmision_date.Text = new DateTime(yearadm, monthadm, dayadm).ToString("dd MMMM yyyy");
                    lbl_addmision_time.Text = $"{splitTime.Substring(0, splitTime.Length - 3)} {timePM_AM}";
                }
                else
                {
                    lbl_addmision_date.Text =" - ";
                    lbl_addmision_time.Text = " - ";
                }

                

                //dateTanggal Perkiraan Operasi/TIndakan
                if (inpatientData.operation_schedule_header.operation_schedule_date != null)
                {
                    lbl_tglTindakan.Text = DateTime.Parse(inpatientData.operation_schedule_header.operation_schedule_date.Substring(0, inpatientData.operation_schedule_header.operation_schedule_date.Length - 9).Trim()).ToString("dd MMMM yyyy", CultureInfo.InvariantCulture);

                    lbl_waktuTindakan.Text = DateTime.ParseExact(inpatientData.operation_schedule_header.incision_time, "HH:mm:ss", CultureInfo.CurrentCulture).ToString("hh:mm tt");

                }

                //jam menit
                var getFirstEstimateTime = inpatientData.operation_procedures.Where(x => x.operation_procedure_id != null).FirstOrDefault();

                //nama operasi
                if (inpatientData.operation_procedures.Count() > 0)
                {
                    lbl_jamoperasi.Text = (Convert.ToInt32(getFirstEstimateTime.procedure_estimate_time) / 60).ToString();
                    lbl_menitoperasi.Text = (Convert.ToInt32(getFirstEstimateTime.procedure_estimate_time) % 60).ToString();

                    DataTable dtprocedure = Helper.ToDataTable(inpatientData.operation_procedures);
                    rpt_namaoperasi.DataSource = dtprocedure;
                    rpt_namaoperasi.DataBind();
                    lbl_namaoperasi_no.Visible = false;
                    up_inpatient.Update();
                }
                else
                {
                    lbl_namaoperasi_no.Visible = true;
                }

              


                lbl_ward.Text = inpatientData.ward_name;
                lbl_estimationday.Text = inpatientData.estimation_day;
                lbl_istindakan.Text = inpatientData.operation_procedures.Count() > 0? "Ya" : "Tidak";
                //lbl_tglTindakan.Text = DateTime.Parse(splitDateTindakan.Trim()).ToString("dd MMMM yyyy", CultureInfo.InvariantCulture);
                //lbl_waktuTindakan.Text = inpatientData.operation_schedule_header.incision_time == null? "-" : inpatientData.operation_schedule_header.incision_time;

                
                lbl_anesteticmethod.Text = inpatientData.operation_schedule_header.anesthetia_type_name== null? "-" : inpatientData.operation_schedule_header.anesthetia_type_name;



                lbl_alat.Text = inpatientData.tools_detail == "" ? " -" : inpatientData.tools_detail ;
                lbl_tabelKategori.Text = inpatientData.category == -1 ? " - " : inpatientData.category == 0 ? inpatientData.category_detail.ToString() : inpatientData.category.ToString();
                lbl_recoveryroom.Text = inpatientData.recovery_room;
                lbl_fasting.Text = inpatientData.fasting_procedure_time != 0 ? inpatientData.fasting_procedure_time.ToString()+ " Jam" : "-" ;

                if(inpatientData.lab_Rad_Additionals.Count()>0)
                {
                    
                    lbl_isRadiologi.Text = inpatientData.lab_Rad_Additionals.Where(x => x.is_rad == true).Count() > 0 ? "Ya" : "Tidak";
                    txt_listlaborder.Text = inpatientData.other_lab;
                    txt_listradorder.Text = inpatientData.other_rad;
                    lbl_isLabo.Text = inpatientData.lab_Rad_Additionals.Where(x => x.is_rad == false).Count() > 0 ? "Ya" : "Tidak";
                }
                else
                {
                    lbl_isRadiologi.Text = "Tidak";
                    lbl_isLabo.Text = "Tidak";
                }
                lbl_instruksi.Text = inpatientData.instruction;
                lbl_tanggal.Text = DateTime.Now.ToString("dd MMMM yyyy");
                lbl_jam.Text = DateTime.Now.ToString(" HH:mm") + " WIB";
                lbl_dokterttd.Text = inpatientData.doctor_name;
                lbl_pregnancy.Text = inpatientData.is_pregnancy.ToString();
                lbl_pregnancyrad.Text = inpatientData.is_pregnancy.ToString();
                lbl_patient.Text = inpatientData.patientName;

                lbl_diagnosaklinis_rad.Text = inpatientData.diagnosis;
                lbl_diagnosaklinis_lab.Text = inpatientData.diagnosis;


                List<CpoeTrans> listlabstemp = new List<CpoeTrans>();
                List<CpoeTrans> listradtemp = new List<CpoeTrans>();
                if (inpatientData.lab_Rad_Additionals.Count() > 0)
                {
                    foreach (var a in inpatientData.lab_Rad_Additionals)
                    {
                        
                        if (a.is_rad == true)
                        {
                            CpoeTrans lisrad = new CpoeTrans();
                            lisrad.id = Convert.ToInt64(a.item_id);
                            lisrad.name = a.item_name;
                            lisrad.type = a.item_type;
                            listradtemp.Add(lisrad);

                        }
                        else
                        {
                            CpoeTrans listlab = new CpoeTrans();
                            listlab.id = Convert.ToInt64(a.item_id);
                            listlab.name = a.item_name;
                            listlab.type = a.item_type;
                            listlabstemp.Add(listlab);
                        }
                    }

                    //Data Labora
                    
                    var gettglLab = inpatientData.lab_Rad_Additionals.Where(x => x.is_rad != false).FirstOrDefault();

                    string[] separateDateLab = gettglLab.created_date.Split(' ');
                    string splitDateLab = separateDateLab[0];
                    string splitTimeLab = separateDateLab[1];
                    string timePM_AMLab = separateDateLab[2];

                    string[] newlabordate = splitDateLab.Split('/');
                    int labday = Convert.ToInt32(newlabordate[1].Trim());
                    int labmonth = Convert.ToInt32(newlabordate[0].Trim());
                    int labyear= Convert.ToInt32(newlabordate[2].Trim()); ;
                    lbl_orderdatelab.Text = new DateTime(labyear, labmonth, labday).ToString("dd MMMM yyyy") + $"{splitTimeLab.Substring(0, splitTimeLab.Length - 3)} {timePM_AMLab}";
                    var gettglRad = inpatientData.lab_Rad_Additionals.Where(x => x.is_rad != false).FirstOrDefault();
                    string[] separateDateRad = gettglRad.created_date.Split(' ');
                    string splitDateRad = separateDateRad[0];
                    string splitTimeRad = separateDateRad[1];
                    string timePM_AMRad = separateDateRad[2];

                    string[] newraddate = splitDateLab.Split('/');
                    int radday = Convert.ToInt32(newraddate[1].Trim());
                    int radmonth = Convert.ToInt32(newraddate[0].Trim());
                    int radyear = Convert.ToInt32(newraddate[2].Trim()); ;

                    lbl_orderdatrad.Text = new DateTime(radyear, radmonth, radday).ToString("dd MMMM yyyy") + $"{splitTimeRad.Substring(0, splitTimeRad.Length - 3)} {timePM_AMRad}";

                    List<CpoeTrans> listClinicLab = new List<CpoeTrans>();
                    List<CpoeTrans> listMicroLab = new List<CpoeTrans>();
                    List<CpoeTrans> listCitoLab = new List<CpoeTrans>();
                    List<CpoeTrans> listPanelLab = new List<CpoeTrans>();
                    List<CpoeTrans> listPatologiLab = new List<CpoeTrans>();
                    List<CpoeTrans> listMDCLab = new List<CpoeTrans>();
                    if (listlabstemp != null)
                    {
                        foreach (var rad in listlabstemp)
                        {

                            if (rad.type == "ClinicLab")
                            {
                                listClinicLab.Add(rad);
                            }
                            else if (rad.type == "MicroLab")
                            {
                                listMicroLab.Add(rad);
                            }
                            else if (rad.type == "CitoLab")
                            {
                                listCitoLab.Add(rad);
                            }
                            else if (rad.type == "PanelLab")
                            {
                                listPanelLab.Add(rad);
                            }
                            else if (rad.type == "PatologiLab")
                            {
                                listPatologiLab.Add(rad);
                            }
                            else if (rad.type == "MDCLab")
                            {
                                listMDCLab.Add(rad);
                            }

                        }
                    }

                    if (listClinicLab.Count > 0)
                    {
                        DataTable dtlistClinicLab = Helper.ToDataTable(listClinicLab);
                        rpt_chlinicalpathology.DataSource = dtlistClinicLab;
                        rpt_chlinicalpathology.DataBind();
                        lbl_chlinicalpathology_no.Visible = false;
                        up_inpatient.Update();
                    }
                    else
                    {
                        lbl_chlinicalpathology_no.Visible = true;
                        rpt_chlinicalpathology.DataSource = null;
                        rpt_chlinicalpathology.DataBind();
                        up_inpatient.Update();

                    }
                    if (listMicroLab.Count > 0)
                    {
                        DataTable dtlistlistMicroLab = Helper.ToDataTable(listMicroLab);
                        rpt_Microbiology.DataSource = dtlistlistMicroLab;
                        rpt_Microbiology.DataBind();
                        lbl_Microbiology_no.Visible = false;
                        up_inpatient.Update();
                    }
                    else
                    {
                        lbl_Microbiology_no.Visible = true;
                        rpt_Microbiology.DataSource = null;
                        rpt_Microbiology.DataBind();
                        up_inpatient.Update();

                    }
                    if (listCitoLab.Count > 0)
                    {
                        DataTable dtlistlistCitoLab = Helper.ToDataTable(listCitoLab);
                        rpt_cito.DataSource = dtlistlistCitoLab;
                        rpt_cito.DataBind();
                        lbl_cito_empty.Visible = false;
                        up_inpatient.Update();
                    }
                    else
                    {
                        lbl_cito_empty.Visible = true;
                        rpt_cito.DataSource = null;
                        rpt_cito.DataBind();
                        up_inpatient.Update();

                    }

                    if (listPatologiLab.Count > 0)
                    {
                        DataTable dtlistPatologiLab = Helper.ToDataTable(listPatologiLab);
                        rpt_AnatomicalPathology.DataSource = dtlistPatologiLab;
                        rpt_AnatomicalPathology.DataBind();
                        lbl_anatomicalno.Visible = false;
                        up_inpatient.Update();
                    }
                    else
                    {
                        lbl_anatomicalno.Visible = true;
                        rpt_AnatomicalPathology.DataSource = null;
                        rpt_AnatomicalPathology.DataBind();
                        up_inpatient.Update();

                    }

                   

                    //Data rad
                    List<CpoeTrans> listXRay = new List<CpoeTrans>();
                    List<CpoeTrans> listUSG = new List<CpoeTrans>();
                    List<CpoeTrans> listCT = new List<CpoeTrans>();   
                    List<CpoeTrans> listMRI1 = new List<CpoeTrans>();
                    List<CpoeTrans> listMRI3 = new List<CpoeTrans>();
                    List<CpoeTrans> listRadiology = new List<CpoeTrans>();
                   
                    if (listradtemp != null){
                        foreach(var rad in listradtemp)
                        {
                          
                            if(rad.type == "CT")
                            {
                                listCT.Add(rad);
                            }else if(rad.type == "MRI1")
                            {
                                listMRI1.Add(rad);
                            }
                            else if (rad.type == "MRI3")
                            {
                                listMRI3.Add(rad);
                            }
                            else if (rad.type == "Radiology")
                            {
                                listRadiology.Add(rad);
                            }
                            else if (rad.type == "USG")
                            {
                                listUSG.Add(rad);
                            }
                            else if (rad.type == "XRay")
                            {
                                listXRay.Add(rad);
                            }


                        }

                    }
                    if (listXRay.Count() > 1)
                    {
                        DataTable dtlistXray = Helper.ToDataTable(listXRay);
                        rpt_xray.DataSource = dtlistXray;
                        rpt_xray.DataBind();
                        lbl_radxray_no.Visible = false;
                        up_inpatient.Update();
                    }
                    else
                    {
                        rpt_xray.DataSource = null;
                        rpt_xray.DataBind();
                        lbl_radxray_no.Visible = true;
                        up_inpatient.Update();
                    }
                    if (listUSG.Count() > 0)
                    {
                        DataTable dtlistUsg = Helper.ToDataTable(listUSG);
                        rpt_usg.DataSource = dtlistUsg;
                        rpt_usg.DataBind();
                        lbl_radusg_no.Visible = false;
                        up_inpatient.Update();
                    }
                    else
                    {
                        rpt_usg.DataSource = null;
                        rpt_usg.DataBind();
                        lbl_radusg_no.Visible = true;
                        up_inpatient.Update();
                    }
                    if (listCT.Count() > 0 )
                    {
                        DataTable dtlistCT = Helper.ToDataTable(listCT);
                        rpt_ct.DataSource = dtlistCT;
                        rpt_ct.DataBind();
                        up_inpatient.Update();
                        lbl_radct_no.Visible = false;
                        up_inpatient.Update();
                    }
                    else
                    {
                        lbl_radct_no.Visible = true;
                        rpt_ct.DataSource = null;
                        rpt_ct.DataBind();
                        up_inpatient.Update();
                    }
                    if (listMRI1.Count() > 0)
                    {
                        DataTable dtlistMRI1 = Helper.ToDataTable(listMRI1);
                        rpt_mr15tesla.DataSource = dtlistMRI1;
                        rpt_mr15tesla.DataBind();
                        lbl_mr15tesla_no.Visible = false;
                        up_inpatient.Update();
                    }
                    else
                    {
                        rpt_mr15tesla.DataSource = null;
                        rpt_mr15tesla.DataBind();
                        lbl_mr15tesla_no.Visible = true;
                        up_inpatient.Update();
                    }

                    if (listMRI3.Count() > 0)
                    {
                        DataTable dtlistMRI3 = Helper.ToDataTable(listMRI3);
                        rpt_mri3teslarad.DataSource = dtlistMRI3;
                        rpt_mri3teslarad.DataBind();
                        lbl_mri3tesla_no_rad.Visible = false;
                        up_inpatient.Update();
                    }
                    else
                    {
                        rpt_mri3teslarad.DataSource = null;
                        rpt_mri3teslarad.DataBind();
                        lbl_mri3tesla_no_rad.Visible = true;

                        up_inpatient.Update();
                    }

           

                }


                //Data Radiologi
                txt_listlaborder.Text = inpatientData.other_lab;
                txt_listradorder.Text = inpatientData.other_rad;
                lbl_dokterlaborder.Text = inpatientData.doctor_name;
                //statetement
                lbl_stetement_date.Text = DateTime.Now.ToString("dd MMMM yyyy");
                lbl_statement_patient.Text = inpatientData.patientName;
                lbl_statement_dokter.Text = inpatientData.doctor_name;




            }
            else
            {
                //RptReferralPrint.Visible = false;
            }

        }
        catch (Exception ex)
        {
            throw ex.InnerException;
        }
    }

}