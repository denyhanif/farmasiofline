using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Form_FormViewer_Drug_Prescription_DrugPrescription : System.Web.UI.Page
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
                ConfigurationManager.AppSettings["DB_EMRTransaction"] = SiloamConfig.Functions.GetValue("DB_EMRTransaction").ToString();
            }

            

            var getPrescriptionEdited = clsPrescription.getPrescriptionEdited(2, "67367288-1103-4AE5-A8F9-4F15FCC0861F", 2000006221621);
            var jsonPrescriptionEdited = JsonConvert.DeserializeObject<ResultPrescriptionEdited>(getPrescriptionEdited.Result.ToString());

            txt_reason.Text = jsonPrescriptionEdited.list.header.reason_remarks;
            txt_calldoctor.Text = jsonPrescriptionEdited.list.header.call_doctor +"," + jsonPrescriptionEdited.list.header.modified_date.ToString("dd MMMM yyyy - hh:mm");
            txt_pharmacistnote.Text = jsonPrescriptionEdited.list.header.pharmacy_notes;

            List <DoctorPrescription> listDoctor = jsonPrescriptionEdited.list.doctor_prescriptions;
            List<PharmacistRelease> listPharma = jsonPrescriptionEdited.list.pharmacist_releases;

            List<ViewDoctorPharmachyEdited> listtmpedited = new List<ViewDoctorPharmachyEdited>();
            List<DoctorPrescription> listDoctorDrugs = listDoctor.Where(x => x.is_compound == false).ToList();
            List<DoctorPrescription> listDoctorCompound = listDoctor.Where(x => x.is_compound == true).ToList();

            List<PharmacistRelease> listPharmaDrugs = listPharma.Where(x => x.is_compound == false).ToList();
            List<PharmacistRelease> listPharmaCompound = listPharma.Where(x => x.is_compound == true).ToList();

            #region Drugs
            foreach ( var a in listDoctor)
            {
                a.quantity = Helper.formatDecimal(a.quantity);
                a.dose = Helper.formatDecimal(a.dose);
            }
            foreach (var a in listPharma)
            {
                a.quantity = Helper.formatDecimal(a.quantity);
                a.dose = Helper.formatDecimal(a.dose);
            }

            //cek pasangan
            foreach (var itemdoctor in listDoctorDrugs)
            {
                ViewDoctorPharmachyEdited data = new ViewDoctorPharmachyEdited();

                foreach (var itemfarmasi in listPharmaDrugs)
                {
                    if (itemdoctor.phar_prescription_id == itemfarmasi.phar_prescription_id && itemdoctor.doctor_prescription_id.ToString().ToUpper() == itemfarmasi.doctor_prescription_id)
                    {

                        data.d_item_name = itemdoctor.item_name;
                        data.d_quantity = itemdoctor.quantity;
                        data.d_uom = itemdoctor.uom;
                        data.d_frequency = itemdoctor.frequency;
                        data.d_dose = itemdoctor.dose;
                        data.d_dose_uom = itemdoctor.dose_uom;
                        data.d_administration_route = itemdoctor.administration_route;
                        data.d_created_date = itemdoctor.created_date;
                        data.d_instruction = itemdoctor.instruction;
                        data.d_edit_action = itemdoctor.edit_action;
                        data.d_edit_reason = itemdoctor.edit_reason;
                        data.d_is_compound = itemdoctor.is_compound;
                        data.d_is_additional = itemdoctor.is_additional;
                        data.d_item_sequence = itemdoctor.item_sequence;
                        data.d_doctor_prescription_id = itemdoctor.doctor_prescription_id;
                        data.d_phar_prescription_id = itemdoctor.phar_prescription_id.ToString();

                        data.p_item_name = itemfarmasi.item_name;
                        data.p_quantity = itemfarmasi.quantity;
                        data.p_uom = itemfarmasi.uom;
                        data.p_frequency = itemfarmasi.frequency;
                        data.p_dose = itemfarmasi.dose;
                        data.p_dose_uom = itemfarmasi.dose_uom;
                        data.p_administration_route = itemfarmasi.administration_route;
                        data.p_created_date = itemfarmasi.created_date;
                        data.p_instruction = itemfarmasi.instruction;
                        data.p_edit_action = itemfarmasi.edit_action;
                        data.p_edit_reason = itemfarmasi.edit_reason;
                        data.p_is_compound = itemfarmasi.is_compound;
                        data.p_is_additional = itemfarmasi.is_additional;
                        data.p_item_sequence = itemfarmasi.item_sequence;
                        data.p_doctor_prescription_id = itemfarmasi.doctor_prescription_id;
                        data.p_phar_prescription_id = itemfarmasi.phar_prescription_id.ToString();

                        listtmpedited.Add(data);
                    }
                }
               
            }

            //cek edited null
            foreach (var itemfarmasi in listPharmaDrugs)
            {
                ViewDoctorPharmachyEdited data = new ViewDoctorPharmachyEdited();

                if (itemfarmasi.doctor_prescription_id== Guid.Empty.ToString())
                {
                    data.p_item_name = itemfarmasi.item_name;
                    data.p_quantity = itemfarmasi.quantity;
                    data.p_uom = itemfarmasi.uom;
                    data.p_frequency = itemfarmasi.frequency;
                    data.p_dose = itemfarmasi.dose;
                    data.p_dose_uom = itemfarmasi.dose_uom;
                    data.p_administration_route = itemfarmasi.administration_route;
                    data.p_created_date = itemfarmasi.created_date;
                    data.p_instruction = itemfarmasi.instruction;
                    data.p_edit_action = itemfarmasi.edit_action;
                    data.p_edit_reason = itemfarmasi.edit_reason;
                    data.p_is_compound = itemfarmasi.is_compound;
                    data.p_is_additional = itemfarmasi.is_additional;
                    data.p_item_sequence = itemfarmasi.item_sequence;
                    data.p_doctor_prescription_id = itemfarmasi.doctor_prescription_id;
                    data.p_phar_prescription_id = itemfarmasi.phar_prescription_id.ToString();

                    listtmpedited.Add(data);
                }
            }

            //cek origin null

            foreach (var itemdoctor in listDoctorDrugs)
            {
                ViewDoctorPharmachyEdited data = new ViewDoctorPharmachyEdited();

                if (itemdoctor.phar_prescription_id.ToString() =="0")
                {
                    data.d_item_name = itemdoctor.item_name;
                    data.d_quantity = itemdoctor.quantity;
                    data.d_uom = itemdoctor.uom;
                    data.d_frequency = itemdoctor.frequency;
                    data.d_dose = itemdoctor.dose;
                    data.d_dose_uom = itemdoctor.dose_uom;
                    data.d_administration_route = itemdoctor.administration_route;
                    data.d_created_date = itemdoctor.created_date;
                    data.d_instruction = itemdoctor.instruction;
                    data.d_edit_action = itemdoctor.edit_action;
                    data.d_edit_reason = itemdoctor.edit_reason;
                    data.d_is_compound = itemdoctor.is_compound;
                    data.d_is_additional = itemdoctor.is_additional;
                    data.d_item_sequence = itemdoctor.item_sequence;
                    data.d_doctor_prescription_id = itemdoctor.doctor_prescription_id;
                    data.d_phar_prescription_id = itemdoctor.phar_prescription_id.ToString();

                    listtmpedited.Add(data);
                }
            }

            DataTable dtdrugsedited = Helper.ToDataTable(listtmpedited);

            DataTable dttempedit = Helper.ToDataTable(listtmpedited);
            rpt_doctordrugs.DataSource = dtdrugsedited;
            rpt_doctordrugs.DataBind();

            #endregion
            if (Request.QueryString["PatientId"] != null)
            {
                long OrganizationId = Convert.ToInt64(Request.QueryString["OrganizationId"]);
                long PatientId = Convert.ToInt64(Request.QueryString["PatientId"]);

                var varResult = clsPatientHistory.getPatientHistorySOAP(OrganizationId, PatientId);
                var JsonResult = JsonConvert.DeserializeObject<ResultPatientHistoryLite>(varResult.Result.ToString());
                try
                {
                    //var getPrescriptionEdited = clsPrescription.getPrescriptionEdited(2, "AAC9C265-8127-491C-A594-CF2FEF9FA26C", 2000006182606);
                    //var jsonPrescriptionEdited = JsonConvert.DeserializeObject<ResultPrescriptionEdited>(getPrescriptionEdited.Result.ToString());
                }
                catch(Exception ex)
                {
                    
                }

                List<PatientHistoryLite> patienthistory = JsonResult.list;

                DataTable dt = Helper.ToDataTable(patienthistory);
                //gv_releashed_pharmachy.DataSource = dt;
                //gv_releashed_pharmachy.DataBind();


            }
        }
    }
}