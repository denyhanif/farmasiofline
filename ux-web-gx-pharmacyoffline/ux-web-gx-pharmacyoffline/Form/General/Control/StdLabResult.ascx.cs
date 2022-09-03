using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Form_General_Control_StdLabResult : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    public void initializevalue(List<LaboratoryResult> listlaboratory)
    {
        panel1.InnerHtml = "";
        try
        {
            //log.Info(LogLibrary.Logging("S", "getLaboratoryHistory ", Helper.GetLoginUser(this.Page), ""));

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
            //log.Info(LogLibrary.Logging("E", "getLaboratoryHistory ", Helper.GetLoginUser(this.Page), ""));

        }
        catch (Exception ex)
        {
            //log.Error(LogLibrary.Error("getLaboratoryResult", Helper.GetLoginUser(this.Page), ex.InnerException.Message));
        }


    }
}