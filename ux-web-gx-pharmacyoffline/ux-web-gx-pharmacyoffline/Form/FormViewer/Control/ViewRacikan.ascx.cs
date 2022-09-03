using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Form_FormViewer_Control_ViewRacikan : System.Web.UI.UserControl
{

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
        }
    }

    public void initializevalue(List<CompoundHeaderSoap> listpresracikanheader, List<CompoundDetailSoap> listpresracikandetail)
    {
      
        if (listpresracikanheader.Count != 0)
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