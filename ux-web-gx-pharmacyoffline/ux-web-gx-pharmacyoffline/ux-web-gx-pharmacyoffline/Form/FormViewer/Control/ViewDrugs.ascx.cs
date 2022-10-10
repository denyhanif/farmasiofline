using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Form_FormViewer_Control_ViewDrugs : System.Web.UI.UserControl
{

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
        }
    }

    public void initializevalue(List<PatientHistoryPrescription> ListDrugs)
    {
       
        if (ListDrugs.Count != 0)
        {
            foreach (var templist in ListDrugs)
            {
                templist.dose = Decimal.Parse(Helper.formatDecimal(templist.dose.ToString().Replace(",",".")));
                templist.quantity = Helper.formatDecimal(templist.quantity.ToString().Replace(",","."));
            }

            DataTable TableDrugs = Helper.ToDataTable(ListDrugs);
            GvwDrugsOrder.DataSource = TableDrugs;
            GvwDrugsOrder.DataBind();
        }
    }
}