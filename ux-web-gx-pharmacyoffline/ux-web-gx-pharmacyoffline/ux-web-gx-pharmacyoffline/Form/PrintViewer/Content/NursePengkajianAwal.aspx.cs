using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Form_PrintViewer_Content_NursePengkajianAwal : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            var registryflag = ConfigurationManager.AppSettings["registryflag"].ToString();

            if (registryflag == "1")
            {
                ConfigurationManager.AppSettings["urlnurse"] = SiloamConfig.Functions.GetValue("urlnurse").ToString();
            }

            long orgid = long.Parse(Request.QueryString["OrgID"]);
            long ptnid = long.Parse(Request.QueryString["PtnID"]);
            long admid = long.Parse(Request.QueryString["AdmID"]);
            Guid encid = Guid.Parse(Request.QueryString["EncID"]);
            Guid pagecode = Guid.Parse(Request.QueryString["pageFA"]);

            GetDataFAResume(orgid, ptnid, admid, encid);
            //GetDataFAResume(2, 1178884, 2000005895310, Guid.Parse("F69EBFBC-6AE3-4184-BB7B-633648D02C00"));
            //GetDataFAResume(2, 1234185, 2000005910276, Guid.Parse("E7930FF7-C0EC-42CE-BB11-36046DB274AA"));
        }
    }

    protected dynamic MappingforGetdataFA(long OrganizationId, long PatientId, long AdmissionId, Guid EncounterId)
    {
        //set fa sesuai templatenya
        string templateid = Request.QueryString["pageFA"];
        if (templateid.ToUpper() == "00000000-0000-0000-0000-000000000000" || templateid.ToUpper() == "136219C4-7DFF-4490-97F2-62F6667C2346")
        {
            var getfaresumeGeneral = clsFAResume.GetFAResume(OrganizationId, PatientId, AdmissionId, EncounterId);
            ResultFAResume jsonfaresumedataGeneral = JsonConvert.DeserializeObject<ResultFAResume>(getfaresumeGeneral.Result.ToString());

            return jsonfaresumedataGeneral;
        }
        else if (templateid.ToUpper() == "B70A5B64-F652-4931-BEDC-AB9D83C76E6C") //EMERGENCY
        {
            var getfaresumeGeneral = clsFAResume.GetFAResume(OrganizationId, PatientId, AdmissionId, EncounterId);
            ResultFAResume jsonfaresumedataGeneral = JsonConvert.DeserializeObject<ResultFAResume>(getfaresumeGeneral.Result.ToString());

            return jsonfaresumedataGeneral;
        }
        else if (templateid.ToUpper() == "A0FED669-964C-48D5-B831-EE0805B47456") //OBGYN
        {
            var getfaresumeObgyn = clsFAResume.GetFAResumeObgyn(OrganizationId, PatientId, AdmissionId, EncounterId);
            ResultFAResumeObgyn jsonfaresumedataObgyn = JsonConvert.DeserializeObject<ResultFAResumeObgyn>(getfaresumeObgyn.Result.ToString());

            return jsonfaresumedataObgyn;
        }
        else if (templateid.ToUpper() == "7A665075-CDE8-4D78-8094-09016F48AB80") //PEDIATRIC
        {
            var getfaresumePediatric = clsFAResume.GetFAResumePediatric(OrganizationId, PatientId, AdmissionId, EncounterId);
            ResultFAResumePediatric jsonfaresumedataPediatric = JsonConvert.DeserializeObject<ResultFAResumePediatric>(getfaresumePediatric.Result.ToString());

            return jsonfaresumedataPediatric;
        }
        else
        {
            var getfaresumeGeneral = clsFAResume.GetFAResume(OrganizationId, PatientId, AdmissionId, EncounterId);
            ResultFAResume jsonfaresumedataGeneral = JsonConvert.DeserializeObject<ResultFAResume>(getfaresumeGeneral.Result.ToString());

            return jsonfaresumedataGeneral;
        }

        return null;
    }

    void GetDataFAResume(long OrganizationId, long PatientId, long AdmissionId, Guid EncounterId)
    {
        try
        {
            //var getfaresume = clsFAResume.GetFAResume(OrganizationId, PatientId, AdmissionId, EncounterId);
            //ResultFAResume jsonfaresumedata = JsonConvert.DeserializeObject<ResultFAResume>(getfaresume.Result.ToString());
            var jsonfaresumedata = MappingforGetdataFA(OrganizationId, PatientId, AdmissionId, EncounterId);

            lblCreatedOrderDate.Text = jsonfaresumedata.list.resumeheader.CreatedDate;
            lblModifiedOrderDate.Text = jsonfaresumedata.list.resumeheader.ModifiedDate;

            FAResume datafaresume = new FAResume();
            datafaresume = jsonfaresumedata.list;

            if (datafaresume != null)
            {
                //#### header data
                ResumeHeaderFA resumedatafaHeader = jsonfaresumedata.list.resumeheader;

                LabelNamaUser.Text = resumedatafaHeader.CreatedBy.ToString();

                //#### content data [BY MAPPING NAME]
                List<ResumeDataFA> resumedatafaContent = jsonfaresumedata.list.resumedata;

                //----keluhan utama
                var txt_keluhanutama = resumedatafaContent.Find(x => x.MappingName == "PATIENT COMPLAINT").Remarks;
                if (txt_keluhanutama.ToLower() == "")
                {
                    Lbl_keluhanutama.Text = "-";
                }
                else
                {
                    Lbl_keluhanutama.Text = txt_keluhanutama.ToString();
                }

                var txt_anamnesis = resumedatafaContent.Find(x => x.MappingName == "PENGKAJIAN").Value;
                var txt_anamnesis2 = resumedatafaContent.Find(x => x.MappingName == "PENGKAJIAN").Remarks;
                if (txt_anamnesis.ToLower() == "autoanamnesis")
                {
                    lbl_anamnesis.Text = txt_anamnesis;
                }
                else
                {
                    var alo = txt_anamnesis2.Split('#');
                    lbl_anamnesis.Text = txt_anamnesis + " - " + alo[0] + " (" + alo[1] + ")";
                }

                var txt_hamil = resumedatafaContent.Find(x => x.MappingName == "PREGNANCY").Value;
                if (txt_hamil.ToLower() == "false")
                {
                    lbl_hamil.Text = "-";
                }
                else if (txt_hamil.ToLower() == "true")
                {
                    lbl_hamil.Text = "Ya";
                }

                var txt_menyusui = resumedatafaContent.Find(x => x.MappingName == "BREASTFEEDING").Value;
                if (txt_menyusui.ToLower() == "false")
                {
                    lbl_menyusui.Text = "-";
                }
                else if (txt_menyusui.ToLower() == "true")
                {
                    lbl_menyusui.Text = "Ya";
                }

                //----kunjungan daerah endemis
                var txt_endemicarea = resumedatafaContent.Find(x => x.MappingName == "ENDEMIC AREA").Remarks;
                if (txt_endemicarea.ToLower() == "")
                {
                    lbl_endemicarea.Text = "-";
                }
                else
                {
                    lbl_endemicarea.Text = "Ya - " + txt_endemicarea;
                }

                //----skrining penyakit infeksius
                var txt_infeksius = resumedatafaContent.FindAll(x => x.MappingName == "INFECTIOUS DISEASE SYMPTOM");
                if (txt_infeksius.Count > 0 && txt_infeksius[0].Remarks != "")
                {
                    DataTable dtinfeksius = Helper.ToDataTable(txt_infeksius);
                    repeaterInfeksius.DataSource = dtinfeksius;
                    repeaterInfeksius.DataBind();
                }
                else
                {
                    repeaterInfeksius.Visible = false;
                    lbl_infeksius_empty.Style.Add("display", "");
                }

                //----EMV
                int jml = 0;

                var txt_eye = resumedatafaContent.Find(x => x.MappingName == "GCS EYE").Value;
                if (txt_eye.ToLower() == "")
                {
                    lbl_eye.Text = "-";
                }
                else
                {
                    //lbl_eye.Text = txt_eye;
                    switch (txt_eye.ToString())
                    {
                        case "1":
                            lbl_eye.Text = "1. None";
                            break;
                        case "2":
                            lbl_eye.Text = "2. To Pressure";
                            break;
                        case "3":
                            lbl_eye.Text = "3. To Sound";
                            break;
                        case "4":
                            lbl_eye.Text = "4. Spontaneus";
                            break;
                    }
                    jml = jml + int.Parse(txt_eye);
                }

                var txt_move = resumedatafaContent.Find(x => x.MappingName == "GCS MOVE").Value;
                if (txt_move.ToLower() == "")
                {
                    lbl_move.Text = "-";
                }
                else
                {
                    //lbl_move.Text = txt_move;
                    switch (txt_move.ToString())
                    {
                        case "1":
                            lbl_move.Text = "1. None";
                            break;
                        case "2":
                            lbl_move.Text = "2. Extension";
                            break;
                        case "3":
                            lbl_move.Text = "3. Flexion to pain stumulus";
                            break;
                        case "4":
                            lbl_move.Text = "4. Withdrawns from pain";
                            break;
                        case "5":
                            lbl_move.Text = "5. Localizes to pain stimulus";
                            break;
                        case "6":
                            lbl_move.Text = "6. Obey Commands";
                            break;
                    }
                    jml = jml + int.Parse(txt_move);
                }

                var txt_verbal = resumedatafaContent.Find(x => x.MappingName == "GCS VERBAL").Value;
                if (txt_verbal.ToLower() == "")
                {
                    lbl_verbal.Text = "-";
                }
                else
                {
                    //lbl_verbal.Text = txt_verbal;
                    switch (txt_verbal.ToString())
                    {
                        case "1":
                            lbl_verbal.Text = "1. None";
                            break;
                        case "2":
                            lbl_verbal.Text = "2. Incomprehensible sounds";
                            break;
                        case "3":
                            lbl_verbal.Text = "3. Inappropriate words";
                            break;
                        case "4":
                            lbl_verbal.Text = "4. Confused";
                            break;
                        case "5":
                            lbl_verbal.Text = "5. Orientated";
                            break;
                        case "T":
                            lbl_verbal.Text = "T. Tracheostomy";
                            break;
                        case "A":
                            lbl_verbal.Text = "A. Aphasia";
                            break;
                    }
                }

                if (lbl_verbal.Text == "T. Tracheostomy" || lbl_verbal.Text == "A. Aphasia" || lbl_verbal.Text == "-")
                {
                    lbl_skor.Text = "-";
                }
                else
                {
                    jml = jml + int.Parse(txt_verbal);
                    lbl_skor.Text = jml.ToString();
                }

                //----skala nyeri
                var txt_skalanyeri = resumedatafaContent.Find(x => x.MappingName == "PAIN SCALE").Value;
                if (txt_skalanyeri.ToLower() == "")
                {
                    lbl_skalanyeri.Text = "-";
                }
                else
                {
                    lbl_skalanyeri.Text = txt_skalanyeri;
                }

                //----tanda vital
                var txt_tekanandarahhigh = resumedatafaContent.Find(x => x.MappingName == "BLOOD PRESSURE HIGH").Value;
                var txt_tekanandarahlow = resumedatafaContent.Find(x => x.MappingName == "BLOOD PRESSURE LOW").Value;
                if (txt_tekanandarahhigh.ToLower() == "" && txt_tekanandarahlow.ToLower() == "")
                {
                    lbl_tekanandarah.Text = "-";
                }
                else
                {
                    lbl_tekanandarah.Text = txt_tekanandarahhigh + "/" + txt_tekanandarahlow + " mmHg";
                }

                var txt_nadi = resumedatafaContent.Find(x => x.MappingName == "PULSE RATE").Value;
                if (txt_nadi.ToLower() == "")
                {
                    lbl_nadi.Text = "-";
                }
                else
                {
                    lbl_nadi.Text = txt_nadi + " x/mnt";
                }

                var txt_pernapasan = resumedatafaContent.Find(x => x.MappingName == "RESPIRATORY RATE").Value;
                if (txt_pernapasan.ToLower() == "")
                {
                    lbl_pernapasan.Text = "-";
                }
                else
                {
                    lbl_pernapasan.Text = txt_pernapasan + " x/mnt";
                }

                var txt_spo2 = resumedatafaContent.Find(x => x.MappingName == "SPO2").Value;
                if (txt_spo2.ToLower() == "")
                {
                    lbl_spo2.Text = "-";
                }
                else
                {
                    lbl_spo2.Text = txt_spo2 + " %";
                }

                var txt_suhu = resumedatafaContent.Find(x => x.MappingName == "TEMPERATURE").Value;
                if (txt_suhu.ToLower() == "")
                {
                    lbl_suhu.Text = "-";
                }
                else
                {
                    lbl_suhu.Text = txt_suhu + " °C";
                }

                var txt_berat = resumedatafaContent.Find(x => x.MappingName == "WEIGHT").Value;
                if (txt_berat.ToLower() == "")
                {
                    lbl_berat.Text = "-";
                }
                else
                {
                    lbl_berat.Text = txt_berat + " kg";
                }

                var txt_tinggi = resumedatafaContent.Find(x => x.MappingName == "HEIGHT").Value;
                if (txt_tinggi.ToLower() == "")
                {
                    lbl_tinggi.Text = "-";
                }
                else
                {
                    lbl_tinggi.Text = txt_tinggi + " cm";
                }

                var txt_lingkar = resumedatafaContent.Find(x => x.MappingName == "LINGKAR KEPALA").Value;
                if (txt_lingkar.ToLower() == "")
                {
                    lbl_lingkarkepala.Text = "-";
                }
                else
                {
                    lbl_lingkarkepala.Text = txt_lingkar + " cm";
                }

                //----status
                var txt_statusmental = resumedatafaContent.Find(x => x.MappingName == "MENTAL STATUS").Remarks;
                if (txt_statusmental.ToLower() == "")
                {
                    lbl_statusmental.Text = "-";
                }
                else
                {
                    lbl_statusmental.Text = txt_statusmental;
                }

                var txt_kesadaran = resumedatafaContent.Find(x => x.MappingName == "CONSCIOUSNESS LEVEL").Value;
                if (txt_kesadaran.ToLower() == "")
                {
                    lbl_kesadaran.Text = "-";
                }
                else
                {
                    lbl_kesadaran.Text = txt_kesadaran;
                }

                var txt_risikojatuh = resumedatafaContent.FindAll(x => x.MappingName == "FALL RISK");
                if (txt_risikojatuh.Count > 0 && txt_risikojatuh[0].Remarks != "")
                {
                    DataTable dtrisikojatuh = Helper.ToDataTable(txt_risikojatuh);
                    repeaterResikoJatuh.DataSource = dtrisikojatuh;
                    repeaterResikoJatuh.DataBind();
                }
                else
                {
                    repeaterResikoJatuh.Visible = false;
                    lbl_risikojatuh_empty.Style.Add("display", "");
                }

                //----catatan untuk dokter
                var txt_catatanuntukdokter = resumedatafaContent.Find(x => x.MappingName == "NURSE NOTES").Remarks;
                if (txt_catatanuntukdokter.ToLower() == "")
                {
                    lbl_catatanuntukdokter.Text = "-";
                }
                else
                {
                    lbl_catatanuntukdokter.Text = txt_catatanuntukdokter;
                }

                //----catatan dari dokter
                var txt_catatandaridokter = resumedatafaContent.Find(x => x.MappingName == "DOCTOR NOTES NURSE").Remarks;
                if (txt_catatandaridokter.ToLower() == "")
                {
                    lbl_catatandaridokter.Text = "-";
                }
                else
                {
                    lbl_catatandaridokter.Text = txt_catatandaridokter;
                }

                //----masalah nutrisi khusus
                var txt_masalahnutrisi = resumedatafaContent.Find(x => x.MappingName == "NUTRITION").Remarks;
                if (txt_masalahnutrisi.ToLower() == "")
                {
                    lbl_masalahnutrisi.Text = "-";
                }
                else
                {
                    lbl_masalahnutrisi.Text = "Ya - " + txt_masalahnutrisi;
                }

                //----puasa
                var txt_puasa = resumedatafaContent.Find(x => x.MappingName == "FASTING").Remarks;
                if (txt_puasa.ToLower() == "")
                {
                    lbl_puasa.Text = "-";
                }
                else
                {
                    lbl_puasa.Text = "Ya - " + txt_puasa;
                }

                //----bahasa sehari-hari
                var txt_bahasa = resumedatafaContent.Find(x => x.MappingName == "BAHASA").Remarks;
                if (txt_bahasa.ToLower() == "")
                {
                    lbl_bahasa.Text = "-";
                }
                else
                {
                    lbl_bahasa.Text = txt_bahasa;
                }

                //----perlu penerjemah
                var txt_penerjemah = resumedatafaContent.Find(x => x.MappingName == "PENERJEMAH").Remarks;
                if (txt_penerjemah.ToLower() == "")
                {
                    lbl_penerjemah.Text = "-";
                }
                else
                {
                    lbl_penerjemah.Text = txt_penerjemah;
                }

                //----metode belajar
                var txt_metodebelajar = resumedatafaContent.Find(x => x.MappingName == "METODE BELAJAR").Remarks;
                if (txt_metodebelajar.ToLower() == "")
                {
                    lbl_metodebelajar.Text = "-";
                }
                else
                {
                    lbl_metodebelajar.Text = txt_metodebelajar;
                }

                //----masalah belajar
                var txt_masalahbelajar = resumedatafaContent.Find(x => x.MappingName == "MASALAH BELAJAR").Remarks;
                if (txt_masalahbelajar.ToLower() == "")
                {
                    lbl_masalahbelajar.Text = "-";
                }
                else
                {
                    lbl_masalahbelajar.Text = txt_masalahbelajar;
                }

                //----kesediaan pasien
                var txt_kesediaanpasien = resumedatafaContent.Find(x => x.MappingName == "KESEDIAAN MENERIMA INFORMASI").Remarks;
                if (txt_kesediaanpasien.ToLower() == "")
                {
                    lbl_kesediaanpasien.Text = "-";
                }
                else
                {
                    lbl_kesediaanpasien.Text = txt_kesediaanpasien;
                }

                //----informasi yang dibutuhkan
                //var txt_informasidibutuhkan = resumedatafaContent.Find(x => x.MappingName == "INFO EDUKASI KESEHATAN").Remarks;
                //if (txt_informasidibutuhkan.ToLower() == "")
                //{
                //    lbl_informasidibutuhkan.Text = "-";
                //}
                //else
                //{
                //    lbl_informasidibutuhkan.Text = txt_informasidibutuhkan;
                //}

                var txt_infoedu = resumedatafaContent.FindAll(x => x.MappingName == "INFO EDUKASI KESEHATAN");
                if (txt_infoedu.Count > 0)
                {
                    for (int i = 0; i < txt_infoedu.Count; i++)
                    {
                        if (txt_infoedu[i].Remarks != "")
                        {
                            txt_infoedu[i].Value = txt_infoedu[i].Value + " (" + txt_infoedu[i].Remarks + ") ";
                        }
                    }
                    DataTable dtinfoedu = Helper.ToDataTable(txt_infoedu);
                    repeaterInfoEdu.DataSource = dtinfoedu;
                    repeaterInfoEdu.DataBind();
                }
                else
                {
                    repeaterInfoEdu.Visible = false;
                    lbl_infoedu_empty.Style.Add("display", "");
                }

                //----respon emosi
                var txt_responemosi = resumedatafaContent.Find(x => x.MappingName == "RESPON EMOSI").Value;
                if (txt_responemosi.ToLower() == "")
                {
                    lbl_responemosi.Text = "-";
                }
                else if (txt_responemosi.ToLower() == "lain-lain")
                {
                    lbl_responemosi.Text = resumedatafaContent.Find(x => x.MappingName == "RESPON EMOSI").Remarks;
                }
                else
                {
                    lbl_responemosi.Text = txt_responemosi;
                }

                //----nilai
                var txt_nilai = resumedatafaContent.Find(x => x.MappingName == "NILAI BUDAYA KEPERCAYAAN").Remarks;
                if (txt_nilai.ToLower() == "")
                {
                    lbl_nilai.Text = "-";
                }
                else
                {
                    lbl_nilai.Text = txt_nilai;
                }


                //#### health info data [BY TYPE]
                List<ResumeHealthInfoFA> resumedatafaHealthinfo = jsonfaresumedata.list.resumehealthinfo;

                //----pengobatan dan alergi 1
                var txt_pengobatanrutin = resumedatafaHealthinfo.FindAll(x => x.Type == "RoutineMedication");
                if (txt_pengobatanrutin.Count > 0)
                {
                    DataTable dtpengobatanrutin = Helper.ToDataTable(txt_pengobatanrutin);
                    repeaterPengobatanRutin.DataSource = dtpengobatanrutin;
                    repeaterPengobatanRutin.DataBind();
                }
                else
                {
                    repeaterPengobatanRutin.Visible = false;
                    lbl_pengobatanrutin_empty.Style.Add("display", "");
                }

                //----pengobatan dan alergi 2
                var txt_alergiobat = resumedatafaHealthinfo.FindAll(x => x.Type == "DrugAllergy");
                if (txt_alergiobat.Count > 0)
                {
                    DataTable dtalergiobat = Helper.ToDataTable(txt_alergiobat);
                    repeaterAlergiObat.DataSource = dtalergiobat;
                    repeaterAlergiObat.DataBind();
                }
                else
                {
                    repeaterAlergiObat.Visible = false;
                    lbl_alergiobat_empty.Style.Add("display", "");
                }

                //----pengobatan dan alergi 3
                var txt_alergimakanan = resumedatafaHealthinfo.FindAll(x => x.Type == "FoodAllergy");
                if (txt_alergimakanan.Count > 0)
                {
                    DataTable dtalergimakanan = Helper.ToDataTable(txt_alergimakanan);
                    repeaterAlergiMakanan.DataSource = dtalergimakanan;
                    repeaterAlergiMakanan.DataBind();
                }
                else
                {
                    repeaterAlergiMakanan.Visible = false;
                    lbl_alergimakanan_empty.Style.Add("display", "");
                }

                //----pengobatan dan alergi 4
                var txt_alergiother = resumedatafaHealthinfo.FindAll(x => x.Type == "OtherAllergy");
                if (txt_alergiother.Count > 0)
                {
                    DataTable dtalergiother = Helper.ToDataTable(txt_alergiother);
                    repeaterAlergiOther.DataSource = dtalergiother;
                    repeaterAlergiOther.DataBind();
                }
                else
                {
                    repeaterAlergiOther.Visible = false;
                    lbl_alergiother_empty.Style.Add("display", "");
                }

                //----riwayat penyakit 1
                var txt_riwayatoperasi = resumedatafaHealthinfo.FindAll(x => x.Type == "Surgery");
                if (txt_riwayatoperasi.Count > 0)
                {
                    DataTable dtriwayatoperasi = Helper.ToDataTable(txt_riwayatoperasi);
                    repeaterRiwayatOperasi.DataSource = dtriwayatoperasi;
                    repeaterRiwayatOperasi.DataBind();
                }
                else
                {
                    repeaterRiwayatOperasi.Visible = false;
                    lbl_riwayatoperasi_empty.Style.Add("display", "");
                }

                //----riwayat penyakit 2
                var txt_penyakitdahulu = resumedatafaHealthinfo.FindAll(x => x.Type == "PersonalDisease");
                if (txt_penyakitdahulu.Count > 0)
                {
                    for (int i = 0; i < txt_penyakitdahulu.Count; i++)
                    {
                        if (txt_penyakitdahulu[i].Value == "Lain-lain")
                        {
                            txt_penyakitdahulu[i].Value = txt_penyakitdahulu[i].Remarks;
                        }
                    }
                    DataTable dtpenyakitdahulu = Helper.ToDataTable(txt_penyakitdahulu);
                    repeaterPenyakitDahulu.DataSource = dtpenyakitdahulu;
                    repeaterPenyakitDahulu.DataBind();
                }
                else
                {
                    repeaterPenyakitDahulu.Visible = false;
                    lbl_penyakitdahulu_empty.Style.Add("display", "");
                }

                //----riwayat penyakit 3
                var txt_penyakitkeluarga = resumedatafaHealthinfo.FindAll(x => x.Type == "FamilyDisease");
                if (txt_penyakitkeluarga.Count > 0)
                {
                    for (int i = 0; i < txt_penyakitkeluarga.Count; i++)
                    {
                        if (txt_penyakitkeluarga[i].Value == "Lain-lain")
                        {
                            txt_penyakitkeluarga[i].Value = txt_penyakitkeluarga[i].Remarks;
                        }
                    }

                    DataTable dtpenyakitkeluarga = Helper.ToDataTable(txt_penyakitkeluarga);
                    repeaterPenyakitKeluarga.DataSource = dtpenyakitkeluarga;
                    repeaterPenyakitKeluarga.DataBind();
                }
                else
                {
                    repeaterPenyakitKeluarga.Visible = false;
                    lbl_penyakitkeluarga_empty.Style.Add("display", "");
                }

                //SPECIALTY

                //set sesuai templatenya
                string templateid = Request.QueryString["pageFA"];
                if (templateid.ToUpper() == "00000000-0000-0000-0000-000000000000" || templateid.ToUpper() == "136219C4-7DFF-4490-97F2-62F6667C2346")
                {

                }
                else if (templateid.ToUpper() == "B70A5B64-F652-4931-BEDC-AB9D83C76E6C") //EMERGENCY
                {
                    LoadEmergencyData(resumedatafaContent);
                    
                    divemergency1.Visible = true;
                }
                else if (templateid.ToUpper() == "A0FED669-964C-48D5-B831-EE0805B47456") //OBGYN
                {
                    List<ResumepregnancydataFA> resumedatakehamilan = jsonfaresumedata.list.resumepregnancydata;
                    List<ResumepregnancyhistoryFA> resumepregnancyhistory = jsonfaresumedata.list.resumepregnancyhistory;
                    LoadObgynData(resumedatakehamilan, resumepregnancyhistory, resumedatafaContent);

                    divobgyn1.Visible = true;
                    divobgyn2.Visible = true;
                    divobgyn3.Visible = true;
                    divobgyn4.Visible = true;
                }
                else if (templateid.ToUpper() == "7A665075-CDE8-4D78-8094-09016F48AB80") //PEDIATRIC
                {
                    List<ResumepediatricdataFA> resumedatapediatric = jsonfaresumedata.list.resumepediatricdata;
                    LoadPediatricData(resumedatapediatric);

                    divpediatric1.Visible = true;
                }



            }
            else
            {
                //var Response = (JObject)JsonConvert.DeserializeObject<dynamic>(getfaresume.Result);
                //var Status = Response.Property("status").Value.ToString();
                //var Message = Response.Property("message").Value.ToString();
            }


        }
        catch (Exception ex)
        {
            //throw ex;
            //error_box(ex);
        }
    }

    public void LoadObgynData(List<ResumepregnancydataFA> resumedatakehamilan, List<ResumepregnancyhistoryFA> resumepregnancyhistory, List<ResumeDataFA> resumedatafaContent)
    {
        //----MENARCHE
        var txt_menarche = resumedatakehamilan.Find(x => x.pregnancy_data_type == "MENARCHE");
        if (txt_menarche != null)
        {
            if (txt_menarche.value.ToLower() == "")
            {
                lbl_menarche.Text = "-";
            }
            else
            {
                lbl_menarche.Text = txt_menarche.value + " tahun";
            }
        }

        //----HAID
        var txt_haid = resumedatafaContent.Find(x => x.MappingName == "HAID");
        if (txt_haid != null)
        {
            if (txt_haid.Value.ToLower() == "")
            {
                lbl_haid.Text = "-";
            }
            else
            {
                if (txt_haid.Value == "Teratur")
                {
                    lbl_haid.Text = txt_haid.Value;
                }
                else if (txt_haid.Value == "Tidak Teratur")
                {
                    lbl_haid.Text = txt_haid.Value + " , lama " + txt_haid.Remarks + " hari";
                }
            }
        }

        //----HAID PROBLEM
        var txt_haidproblem = resumedatafaContent.Find(x => x.MappingName == "HAID PROBLEM");
        if (txt_haidproblem != null)
        {
            if (txt_haidproblem.Value.ToLower() == "")
            {
                lbl_haidproblem.Text = "-";
            }
            else
            {
                lbl_haidproblem.Text = txt_haidproblem.Value;
            }
        }

        //----HAID PROBLEM
        List<ResumepregnancydataFA> listkontrasepidata = resumedatakehamilan.FindAll(x => x.pregnancy_data_type == "CONTRACEPTION");
        if (listkontrasepidata != null)
        {
            if (listkontrasepidata.Count > 0)
            {
                DataTable dtkontrasepsi = Helper.ToDataTable(listkontrasepidata);

                if (dtkontrasepsi.Rows[0]["value"].ToString() == "Tidak Ada Kontrasepsi")
                {
                    repeaterContraception.DataSource = null;
                    repeaterContraception.DataBind();
                    repeaterContraception.Visible = false;
                    lbl_kontrasepsi_empty.Style.Add("display", "");
                }
                else
                {
                    repeaterContraception.DataSource = dtkontrasepsi;
                    repeaterContraception.DataBind();
                }
            }
        }

        //----PREGNANT HISTORY
        if (resumepregnancyhistory != null)
        {
            if (resumepregnancyhistory.Count > 0)
            {
                DataTable dtpregnanthistory = Helper.ToDataTable(resumepregnancyhistory);

                rptpregnanthistory.DataSource = dtpregnanthistory;
                rptpregnanthistory.DataBind();
            }
            else
            {
                rptpregnanthistory.DataSource = null;
                rptpregnanthistory.DataBind();

                divpregnanthistory.Visible = false;
                lbl_pregnanthistory_empty.Style.Add("display", "");
            }
        }

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

        //----status pernikahan
        var txt_statuspernikahan = resumedatafaContent.Find(x => x.MappingId == Guid.Parse("633F349C-71E7-4D21-8834-6669A70EF802")).Remarks;
        if (txt_statuspernikahan.ToLower() == "")
        {
            lbl_statuspernikahan.Text = "-";
        }
        else
        {
            lbl_statuspernikahan.Text = txt_statuspernikahan;
        }
    }

    public void LoadPediatricData(List<ResumepediatricdataFA> resumedatapediatric)
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

    public void LoadEmergencyData(List<ResumeDataFA> resumedatafaContent)
    {
        var txt_skor = resumedatafaContent.Find(x => x.MappingId == Guid.Parse("EB3330BD-D413-4B70-999C-0D7AB29FBE36")).Value;
        if (txt_skor.ToLower() == "")
        {
            lblskortriage.Text = "-";
        }
        else
        {
            lblskortriage.Text = txt_skor;
        }

        var txt_trauma = resumedatafaContent.Find(x => x.MappingId == Guid.Parse("64B06F14-6480-46DA-8846-9CAC5A499748")).Value;
        if (txt_trauma.ToLower() == "")
        {
            lbltraumatriage.Text = "-";
        }
        else
        {
            lbltraumatriage.Text = txt_trauma;
        }

        var txt_pasiendatang = resumedatafaContent.Find(x => x.MappingId == Guid.Parse("D2D42792-B16F-472B-B5CB-428980C5003E"));
        if (txt_pasiendatang != null)
        {
            if (txt_pasiendatang.Value.ToLower() == "")
            {
                lblpasiendatang.Text = "-";
            }
            else if (txt_pasiendatang.Value.ToLower() == "sendiri")
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

        var txt_keadaan = resumedatafaContent.Find(x => x.MappingId == Guid.Parse("8C1799E0-C793-4918-A850-1B9BE72359CF")).Value;
        if (txt_keadaan.ToLower() == "")
        {
            lblkeadaanumum.Text = "-";
        }
        else
        {
            lblkeadaanumum.Text = txt_keadaan;
        }

        var txt_airway = resumedatafaContent.Find(x => x.MappingId == Guid.Parse("220F9B0A-4F91-4982-BED3-CA2DDEB1884F")).Value;
        if (txt_airway.ToLower() == "")
        {
            lblairway.Text = "-";
        }
        else
        {
            lblairway.Text = txt_airway;
        }

        var txt_breathing = resumedatafaContent.Find(x => x.MappingId == Guid.Parse("DF853881-EADC-4DA8-9140-960F962535E3")).Value;
        if (txt_breathing.ToLower() == "")
        {
            lblbreathing.Text = "-";
        }
        else
        {
            lblbreathing.Text = txt_breathing;
        }

        var txt_circulation = resumedatafaContent.Find(x => x.MappingId == Guid.Parse("576416E4-AEF9-45BC-A7E0-AE3CEDF6EE94")).Value;
        if (txt_circulation.ToLower() == "")
        {
            lblcirculation.Text = "-";
        }
        else
        {
            lblcirculation.Text = txt_circulation;
        }

        var txt_disability = resumedatafaContent.Find(x => x.MappingId == Guid.Parse("058F59BA-7FA9-443A-994A-E848F4FAEE7F")).Value;
        if (txt_disability.ToLower() == "")
        {
            lbldisability.Text = "-";
        }
        else
        {
            lbldisability.Text = txt_disability;
        }
    }
}