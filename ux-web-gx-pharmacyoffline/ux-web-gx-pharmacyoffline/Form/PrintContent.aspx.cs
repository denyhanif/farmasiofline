using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;

public partial class Form_PrintContent : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
            DataSet result = clsPatientHistory.getAllForPrint(long.Parse(Request.QueryString["OrgID"]), long.Parse(Request.QueryString["AdmID"]), Guid.Parse(Request.QueryString["EncID"]));

            DataTable headerData = result.Tables[0];
            DataTable contentData = result.Tables[1];
            DataTable travelData = result.Tables[4];
            DataTable racikanHeaderData = result.Tables[2];
            DataTable racikanDetailData = result.Tables[3];

            DataTable docRemarkDetailData = result.Tables[5];

            var GetSettings = clsSettings.GetAppSettings(long.Parse(Request.QueryString["OrgID"]));
            var theSettings = JsonConvert.DeserializeObject<ResultViewOrganizationSetting>(GetSettings.Result.ToString());
            List<ViewOrganizationSetting> listSettings = new List<ViewOrganizationSetting>();
            listSettings = theSettings.list;

            if (listSettings.Find(y => y.setting_name.ToUpper() == "USE_COVID19").setting_value.ToUpper() == "TRUE")
            {
                string flagCovid = headerData.Rows[0].Field<bool>("IsCOVID").ToString();
                if (flagCovid.ToLower() == "true")
                {
                    ImageCovid.Visible = true;
                }
                else if (flagCovid.ToLower() == "false")
                {
                    ImageCovid.Visible = false;
                }
            }

            var S = headerData.Rows[0].Field<string>("Anamnesis").Split('\n');
            var Ov = headerData.Rows[0].Field<string>("Objective").Replace("\\n", "\n");
            var Ov1 = Ov.Split(new[] { "#TTV" }, StringSplitOptions.None);
            var O = Ov1[0].Split('\n');
            var A = headerData.Rows[0].Field<string>("Diagnosis").Split('\n');
            var P = headerData.Rows[0].Field<string>("PlanningProcedure").Split('\n');
            var Pr = headerData.Rows[0].Field<string>("ProcedureResult").Split('\n');

            DataTable dtS = new DataTable();
            DataTable dtO = new DataTable();
            DataTable dtA = new DataTable();
            DataTable dtP = new DataTable();
            DataTable dtPr = new DataTable();

            dtS.Columns.Add("value", typeof(string));
            dtO.Columns.Add("value", typeof(string));
            dtA.Columns.Add("value", typeof(string));
            dtP.Columns.Add("value", typeof(string));
            dtPr.Columns.Add("value", typeof(string));

            for (int i = 0; i < S.Count(); i++)
            {
                DataRow data = dtS.NewRow();
                data[0] = S[i];

                dtS.Rows.Add(data);
            }

            for (int i = 0; i < O.Count(); i++)
            {
                DataRow data = dtO.NewRow();
                data[0] = O[i];

                dtO.Rows.Add(data);
            }

            lblO_TTV.Text = "Vital Signs (TTV)" + Ov1[1].Replace("\n", "<br />");

            for (int i = 0; i < A.Count(); i++)
            {
                DataRow data = dtA.NewRow();
                data[0] = A[i];

                dtA.Rows.Add(data);
            }

            for (int i = 0; i < P.Count(); i++)
            {
                DataRow data = dtP.NewRow();
                data[0] = P[i];

                dtP.Rows.Add(data);
            }

            for (int i = 0; i < Pr.Count(); i++)
            {
                DataRow data = dtPr.NewRow();
                data[0] = Pr[i];

                dtPr.Rows.Add(data);
            }

            if (docRemarkDetailData.Rows.Count > 0)
            {
                divdocremark.Visible = true;
                StringBuilder documentInnerHTML = new StringBuilder();
                int i = 0;
                foreach (DataRow dr in docRemarkDetailData.Rows)
                {
                    if (i == 0)
                    {
                        documentInnerHTML.Append("<label>Pemeriksaan(Examination): " + dr["image_type_value"].ToString() + "<br/>Catatan(Remarks):" + dr["image_remark"].ToString() + "</label>");
                    }
                    else
                    {
                        documentInnerHTML.Append("<br/><br/><label>Pemeriksaan(Examination): " + dr["image_type_value"].ToString() + "<br/>Catatan(Remarks):" + dr["image_remark"].ToString() + "</label>");
                    }
                    i++;
                }

                divdocremark.InnerHtml = documentInnerHTML.ToString();
            }
            else
            {
                divdocremark.Visible = false;
            }

            DataTable TrvSch = travelData.Select("soap_mapping_id = '30F9892A-22C7-4C8B-8005-BE08E105F05F'").CopyToDataTable();
            lblTravel.Text = TrvSch.Rows[0]["remarks"].ToString().Replace("\n", "<br />");
            if (lblTravel.Text == "-")
            {
                divTravelData.Visible = false; 
            }

            repeaterS.DataSource = dtS;
            repeaterS.DataBind();

            repeaterO.DataSource = dtO;
            repeaterO.DataBind();

            repeaterA.DataSource = dtA;
            repeaterA.DataBind();

            repeaterP.DataSource = dtP;
            repeaterP.DataBind();

            repeaterPr.DataSource = dtPr;
            repeaterPr.DataBind();

            lblNamaDokter.Text = headerData.Rows[0].Field<string>("DoctorName");
            lblCreatedOrderDate.Text = headerData.Rows[0].Field<DateTime>("CreatedDate").ToString("dd MMMM yyyy HH:mm:ss");
            lblModifiedOrderDate.Text = headerData.Rows[0].Field<DateTime>("ModifiedDate").ToString("dd MMMM yyyy HH:mm:ss");

            //lblPrintDate.Text = DateTime.Now.ToString("dd MMM yyyy HH:mm");
            //lblPrintedBy.Text = Request.QueryString["PrintBy"].ToString();

            for(int i=0; i< contentData.Rows.Count;i++)
            {
                string[] tempdose = contentData.Rows[i]["Dose"].ToString().Split('.');
                if (tempdose.Count() > 1)
                {
                    if (tempdose[1].Length == 3)
                    {
                        if (tempdose[1] == "000")
                        {
                            contentData.Rows[i]["Dose"] = decimal.Parse(tempdose[0]).ToString();
                        }
                        else if (tempdose[1].Substring(tempdose[1].Length - 2) == "00")
                        {
                            contentData.Rows[i]["Dose"] = tempdose[0] + "." + tempdose[1].Substring(0, 1);
                        }
                        else if (tempdose[1].Substring(tempdose[1].Length - 1) == "0")
                        {
                            contentData.Rows[i]["Dose"] = tempdose[0] + "." + tempdose[1].Substring(0, 2);
                        }
                    }
                }
            }

            gvw_detail.DataSource = contentData;
            gvw_detail.DataBind();

            if (racikanHeaderData.Rows.Count > 0)
            {
                foreach (DataRow dr in racikanHeaderData.Rows) 
                {
                    dr["dose"] = Helper.formatDecimal(dr["dose"].ToString().Replace(',','.'));
                    dr["quantity"] = Helper.formatDecimal(dr["quantity"].ToString().Replace(',', '.'));
                }

                DataTable dtracikan = racikanHeaderData;

                if (racikanDetailData.Rows.Count > 0)
                {
                    foreach (DataRow dr in racikanDetailData.Rows)
                    {
                        dr["dose"] = Helper.formatDecimal(dr["dose"].ToString().Replace(',', '.'));
                    }

                    DataTable dtracikandetail = racikanDetailData;
                    Session[Helper.SessionRacikanDetail] = dtracikandetail;
                }

                gvw_racikan_header.DataSource = dtracikan;
                gvw_racikan_header.DataBind();
            }
            else
            {
                gvw_racikan_header.Visible = false;
                gvw_racikan_header.DataSource = null;
                gvw_racikan_header.DataBind();
                Session[Helper.SessionRacikanDetail] = null;
            }
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
}