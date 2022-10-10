<%@ Page Language="C#" AutoEventWireup="true" CodeFile="BigPatientHistory.aspx.cs" Inherits="Form_BigPatientHistory" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <!-- Tell the browser to be responsive to screen width -->
    <meta content="width=device-width, initial-scale=1, maximum-scale=1, user-scalable=no" name="viewport" />
    <!-- Bootstrap 3.3.6 -->
    <link rel="stylesheet" href="~/Content/bootstrap/css/bootstrap.css" />
    <!-- Bootstrap Switch -->
    <%--<link rel="stylesheet" href="~/Content/bootstrap-switch/bootstrap3/bootstrap-switch.css" />
    <link rel="stylesheet" href="~/Content/bootstrap-switch/bootstrap3/bootstrap-switch.min.css" />--%>
    <!-- Font Awesome -->
    <link rel="stylesheet" href="~/Content/font-awesome/css/font-awesome.css" />
    <!-- Ionicons -->
    <link rel="stylesheet" href="~/Content/ionicons/css/ionicons.css" />
    <!-- Ichek -->
    <link rel="stylesheet" href="~/Content/plugins/iCheck/all.css" />
    <!-- Datepicker -->
    <link rel="stylesheet" href="../Content/plugins/datepicker/datepicker3.css" />
    <!-- DropDown -->
    <link rel="stylesheet" href="~/Content/plugins/select2/select2.min.css" />
    <!-- Pretty Checkbox -->
    <link rel="stylesheet" href="../Content/plugins/pretty/pretty-checkbox.css" />
    <!-- EMR-Doctor -->
    <link rel="stylesheet" href="~/Content/EMR-Doctor.css" />
    <!-- Mask -->
    <link rel="stylesheet" href="~/Content/plugins/jasny-bootstrap/css/jasny-bootstrap.min.css" />
    <!-- Theme style -->
    <link rel="stylesheet" href="~/Content/dist/css/AdminLTE.css" />
    <link rel="stylesheet" href="~/Content/dist/css/skins/skin-blue-light.css" />
    <link rel="stylesheet" href="~/Content/Site.css" />

    <link href="favicon.ico" rel="shortcut icon" type="image/x-icon" />


    <link href="~/favicon.ico" rel="shortcut icon" type="image/x-icon" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />

</head>


<body style="padding-top: 0px; padding-bottom: 0px; font-size: 12px;">

    <style type="text/css">
        .shadows {
            border: 1px;
            border-radius: 15px;
            box-shadow: 5px 5px 5px #9293A0;
            margin-top: 0px;
        }
    </style>   

    <form id="form1" runat="server">

        <asp:ScriptManager ID="ScriptManager1" runat="server">
            <Scripts>
                <asp:ScriptReference Path="~/Content/plugins/jQuery/jQuery-2.2.0.min.js" />
                <asp:ScriptReference Path="~/Content/bootstrap/js/bootstrap.min.js" />
                <asp:ScriptReference Path="~/Content/plugins/jasny-bootstrap/js/jasny-bootstrap.min.js" />
                <asp:ScriptReference Path="~/Content/plugins/iCheck/icheck.min.js" />
                <asp:ScriptReference Path="~/Content/plugins/datepicker/bootstrap-datepicker.js" />
                <asp:ScriptReference Path="~/Content/plugins/select2/select2.full.min.js" />
                <%--<asp:ScriptReference Path="~/Scripts/bootbox.min.js" />--%>
                <asp:ScriptReference Path="~/Content/dist/js/app.min.js" />
                <%--<asp:ScriptReference Path="~/Scripts/site.js" />--%>
            </Scripts>
        </asp:ScriptManager>

        <asp:HiddenField ID="HFisBahasa" runat="server" />
        <asp:HiddenField ID="hf_tab_postion" runat="server" />

        <asp:UpdatePanel runat="server" ID="UPpatientHistoryResult" UpdateMode="Conditional">
            <ContentTemplate>

                <asp:HiddenField ID="hfOrgId" runat="server" />
                <asp:HiddenField ID="hfPatientId" runat="server" />
                <asp:HiddenField ID="hfEncounterId" runat="server" />
                <asp:HiddenField ID="hfAdmissionId" runat="server" />
                <asp:HiddenField ID="hfPageSoapId" runat="server" />
                <asp:HiddenField ID="hfCompoundName" runat="server" />
                <asp:HiddenField ID="selected_form_filter" runat="server" />
                <asp:HiddenField ID="selected_doctor_filter" runat="server" />
                <asp:HiddenField ID="hf_all_form" runat="server" />
                <asp:HiddenField ID="hf_all_doctor" runat="server" />

                <div class="container-fluid kartu-pasien" style="display: none">
                    <asp:Button runat="server" ID="btnCompoundDetail" Text="" CssClass="hidden" OnClick="btnCompoundDetail_Click" />
                </div>

                <div style="width: 100%; height: 45px; background-color: #e7e8ed; z-index: 4; position: sticky; font-size: 12px">

                    <a href="javascript:tab_patientHistory(1);" id="lnb_emr">
                        <div id="btn_emr" runat="server" class="col-sm-1" style="top: 15%; width: 130px; text-align: center; margin-right: 0px; margin-left: 5px; padding: 6px 3px 3px 3px; border-radius: 5px 0px 0px 5px; background-color: #d6dbff; border: 1px solid #bdbfd8;">
                            <span>
                                <img src="<%= Page.ResolveClientUrl("~/Images/PatientHistory/ic_EMR.svg") %>" style="height: 25px; width: 25px; margin-right: 3px; margin-top: -3px;" /></span><b> Electronic MR </b>
                        </div>
                    </a>

                    <asp:LinkButton ID="LB_otherEMR" runat="server" OnClientClick="tab_patientHistory(2);" OnClick="LB_otherEMR_Click">
                    <%--<a href="javascript:tab_patientHistory(2);" id="lnb_other_emr">--%>
                        <div id="btn_other_emr" runat="server" class="col-sm-1" style="top: 15%; width: 130px; text-align: center; margin-right: 5px; margin-left: -1px; padding: 6px 3px 3px 3px; border-radius: 0px 5px 5px 0px; border: 1px solid #bdbfd8;">
                            <span>
                                <img src="<%= Page.ResolveClientUrl("~/Images/PatientHistory/ic_OtherUnitEMR.svg") %>" style="height: 25px; width: 25px; margin-right: 3px; margin-top: -3px;" /></span><b> Other Unit MR </b>
                        </div>
                    <%--</a>--%>
                    </asp:LinkButton>

                    <asp:LinkButton ID="LB_scannedEMR" runat="server" OnClientClick="tab_patientHistory(3);" OnClick="LB_scannedEMR_Click">
                    <%--<a href="javascript:tab_patientHistory(3);" id="lnb_scannedEMR">--%>
                        <div id="btn_scanned_emr" runat="server" class="col-sm-1" style="top: 15%; width: 130px; text-align: center; margin-right: 5px; margin-left: 5px; padding: 6px 3px 3px 3px; border-radius: 5px; border: 1px solid #bdbfd8;">
                            <span>
                                <img src="<%= Page.ResolveClientUrl("~/Images/PatientHistory/ic_ScannedMR.svg") %>" style="height: 25px; width: 25px; margin-right: 3px; margin-top: -3px;" /></span><b> Scanned MR </b>
                        </div>
                    <%--</a>--%>
                    </asp:LinkButton>

                    <asp:LinkButton ID="LB_hopeEMR" runat="server" OnClientClick="tab_patientHistory(4);" OnClick="LB_hopeEMR_Click">
                    <%--<a href="javascript:tab_patientHistory(4);" id="lnb_hopeEMR">--%>
                        <div id="btn_hope_emr" runat="server" class="col-sm-1" style="top: 15%; width: 130px; text-align: center; margin-right: 5px; margin-left: 5px; padding: 6px 3px 3px 3px; border-radius: 5px; border: 1px solid #bdbfd8;">
                            <span>
                                <img src="<%= Page.ResolveClientUrl("~/Images/PatientHistory/ic_Hope.png") %>" style="height: 17px; width: 50px; margin: 1px 3px 4px 0px;" /></span><b> MR </b>
                        </div>
                    <%--</a>--%>
                    </asp:LinkButton>

                    <div style="display:inline-flex; margin-top: 12px; float: right; margin-right: 35px;">
                        <asp:UpdateProgress ID="uProgPatientHistory" runat="server" AssociatedUpdatePanelID="UPpatientHistoryResult">
                            <ProgressTemplate>
                                <img alt="" style="background-color: transparent; height: 22px; margin-left: 25px;" src="<%= Page.ResolveClientUrl("~/Images/Background/small-loader.gif") %>" />
                            </ProgressTemplate>
                        </asp:UpdateProgress>
                    </div>

                </div>


                <div style="background: #fdfdfd; min-height: calc(100vh - 170px); padding-bottom: 5px;">

                    <%-- search box --%>

                    <div class="row" style="width: 100%;">
                        <div class="col-sm-6">
                            
                        </div>
                        <div class="col-sm-6">

                            <div id="src_emr" runat="server" style="display: none">
                                <table border="0" style="float: right;">
                                    <tr>
                                        <td style="width: 55px; text-align: center;">
                                            <label id="lblbhs_unitemrdate">Date </label>
                                        </td>
                                        <td>
                                            <asp:TextBox class="form-control" runat="server" ID="DateTextboxStart_emr" AutoCompleteType="Disabled" name="date" Style="font-size: 12px; background-color: white;" placeholder="Input date..." onmousedown="dateStart_emr();" />
                                        </td>
                                        <td style="width: 20px; text-align: center;">- </td>
                                        <td>
                                            <asp:TextBox class="form-control" runat="server" ID="DateTextboxEnd_emr" AutoCompleteType="Disabled" name="date" Style="font-size: 12px; background-color: white;" placeholder="Input date..." onmousedown="dateEnd_emr();" />
                                        </td>
                                        <td style="width: 20px; text-align: center;">&nbsp; </td>
                                    </tr>
                                </table>
                            </div>

                            <div id="src_other_unit" runat="server" style="display: none; padding-top: 15px;">
                                <table border="0" style="float: right;">
                                    <tr>
                                        <td style="width: 55px; text-align: center;">
                                            <%--<label style="display: <%=setENG%>;">Years </label>
                                                <label style="display: <%=setIND%>;">Tahun </label>--%>
                                            <label id="lblbhs_otheremryears">Years </label>
                                        </td>
                                        <td>
                                            <asp:DropDownList runat="server" ID="ddl_year_other_mr" Style="font-size: 12px; width: 75px;" OnTextChanged="ddl_year_other_mr_TextChanged" AutoPostBack="true">
                                                <asp:ListItem Text="2 years" Value="2" Selected="True"></asp:ListItem>
                                                <asp:ListItem Text="3 years" Value="3"></asp:ListItem>
                                                <asp:ListItem Text="5 years" Value="5"></asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                        <td style="width: 55px; text-align: center;">
                                            <%--<label style="display: <%=setENG%>;">Date </label>
                                                <label style="display: <%=setIND%>;">Tanggal </label>--%>
                                            <label id="lblbhs_otheremrdate">Date </label>
                                        </td>
                                        <td>
                                            <asp:TextBox class="form-control" runat="server" ID="DateTextboxStart_other" AutoCompleteType="Disabled" name="date" Style="font-size: 12px; background-color: white;" placeholder="Input date..." onmousedown="dateStart_otherUnit();" />
                                        </td>
                                        <td style="width: 20px; text-align: center;">- </td>
                                        <td>
                                            <asp:TextBox class="form-control" runat="server" ID="DateTextboxEnd_other" AutoCompleteType="Disabled" name="date" Style="font-size: 12px; background-color: white;" placeholder="Input date..." onmousedown="dateEnd_otherUnit();" />
                                        </td>
                                        <td style="width: 55px; text-align: center;">
                                            <label>Unit </label>
                                        </td>
                                        <td style="width: 50px">
                                            <asp:DropDownList runat="server" ID="ddl_unit_other_mr">
                                                <asp:ListItem Text="Unit" Value="" Selected="True"></asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                        <td style="width: 20px; text-align: center;">&nbsp; </td>
                                        <td>
                                            <asp:Button ID="btn_search_other_unit" runat="server" Text="Search" CssClass="btn btn-lightGreen btn-emr-small" OnClick="btn_search_other_unit_Click" OnClientClick="tab_patientHistory(2);" Style="padding-top: 1px" />
                                        </td>
                                    </tr>
                                </table>
                            </div>

                            <div id="src_scanned_emr" runat="server" style="display: none; padding-top: 15px;">
                                <table border="0" style="float: right;">
                                    <tr>
                                        <td style="width: 55px; text-align: center;">
                                            <%--<label style="display: <%=setENG%>;">Date </label>
                                                <label style="display: <%=setIND%>;">Tanggal </label>--%>
                                            <label id="lblbhs_scanemrdate">Date </label>
                                        </td>
                                        <td>
                                            <asp:TextBox class="form-control" runat="server" ID="DateTextboxStart_scanned" AutoCompleteType="Disabled" name="date" Style="font-size: 12px; background-color: white;" placeholder="Input date..." onmousedown="dateStart_ScannedMR();" />
                                        </td>
                                        <td style="width: 20px; text-align: center;">- </td>
                                        <td>
                                            <asp:TextBox class="form-control" runat="server" ID="DateTextboxEnd_scanned" AutoCompleteType="Disabled" name="date" Style="font-size: 12px; background-color: white;" placeholder="Input date..." onmousedown="dateEnd_ScannedMR();" />
                                        </td>
                                        <td style="width: 20px; text-align: center;">&nbsp; </td>
                                        <td>
                                            <asp:TextBox ID="src_scanned_form" Width="120px" runat="server" placeholder="Filter Form" ReadOnly="true" OnClick="OnClick();"> 
                                            </asp:TextBox><span class="fa fa-angle-down form-control-feedback" style="margin-top: 9px;  margin-right: 235px;"></span>
                                            <!-- #### kotak pencarian #### -->
                                            <div id="testpopup" style="display: none; position: absolute; border: 1px groove; min-width: 350px; background-color: white; max-width: 800px; z-index: 1; right: 240px">
                                                <asp:HiddenField runat="server" ID="conAll" />
                                                <asp:HiddenField runat="server" ID="txtSearchItem"></asp:HiddenField>
                                                <asp:UpdatePanel runat="server">
                                                    <ContentTemplate>
                                                        <div style="padding: 5px; max-height: 200px; overflow-y: auto;" class="scrollEMR">
                                                            <div id="select_all_form" runat="server">
                                                            </div>
                                                            <div>
                                                                <div style="font-size: 12px"><b>OPD</b></div>
                                                                <div id="opd_scanned_filter" style="font-size: 11px" runat="server"></div>
                                                            </div>
                                                            <div>
                                                                <div style="font-size: 12px"><b>IPD</b></div>
                                                                <div id="ipd_scanned_filter" runat="server"></div>
                                                            </div>
                                                            <div>
                                                                <div style="font-size: 12px"><b>MCU</b></div>
                                                                <div id="mcu_scanned_filter" runat="server"></div>
                                                            </div>
                                                        </div>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </div>
                                            <!-- #### end kotak pencarian #### -->
                                        </td>
                                        <td style="width: 20px; text-align: center;">&nbsp; </td>
                                        <td>
                                        <asp:TextBox ID="txtDoctorFilter" Width="120px" runat="server" placeholder="Filter Doctor" ReadOnly="true" OnClick="doctorClick();"> 
                                        </asp:TextBox><span class="fa fa-angle-down form-control-feedback" style="margin-top: 9px; margin-right: 95px;"></span>
                                        <!-- #### kotak pencarian #### -->
                                        <div id="doctorPopUp" style="display: none; position: absolute; border: 1px groove; min-width: 350px; background-color: white; max-width: 800px; z-index: 1; right: 100px">
                                            <asp:HiddenField runat="server" ID="DocAll" />
                                            <asp:HiddenField runat="server" ID="txtSrcDoctor"></asp:HiddenField>
                                            <asp:UpdatePanel runat="server">
                                                <ContentTemplate>
                                                    <div style="padding: 5px; max-height: 200px; overflow-y: auto;" class="scrollEMR">
                                                        <div id="select_all_doctor" runat="server">
                                                        </div>
                                                        <div>
                                                            <div style="font-size: 12px"><b>OPD</b></div>
                                                            <div id="opd_doctor_filter" style="font-size: 11px" runat="server"></div>
                                                        </div>
                                                        <div>
                                                            <div style="font-size: 12px"><b>IPD</b></div>
                                                            <div id="ipd_doctor_filter" runat="server"></div>
                                                        </div>
                                                        <div>
                                                            <div style="font-size: 12px"><b>MCU</b></div>
                                                            <div id="mcu_doctor_filter" runat="server"></div>
                                                        </div>
                                                    </div>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </div>
                                        <!-- #### end kotak pencarian #### -->
                                    </td>
                                    <td style="width: 20px; text-align: center;">&nbsp; </td>
                                        <td>
                                            <asp:Button ID="btn_scanned_mr" runat="server" Text="Search" CssClass="btn btn-lightGreen btn-emr-small" OnClick="btn_scanned_mr_Click" />
                                        </td>
                                    </tr>
                                </table>
                            </div>

                            <div id="src_hope_emr" runat="server" style="display: none; padding-top: 15px;">
                                <table border="0" style="float: right;">
                                    <tr>
                                        <td style="width: 55px; text-align: center;">
                                            <%--<label style="display: <%=setENG%>;">Date </label>
                                                <label style="display: <%=setIND%>;">Tanggal </label>--%>
                                            <label id="lblbhs_hopeemrdate">Date </label>
                                        </td>
                                        <td>
                                            <asp:TextBox class="form-control" runat="server" ID="DateTextboxStart_hopeEmr" AutoCompleteType="Disabled" name="date" Style="font-size: 12px; background-color: white;" placeholder="Input date..." onmousedown="dateStart_HOPEemr();" />
                                        </td>
                                        <td style="width: 20px; text-align: center;">- </td>
                                        <td>
                                            <asp:TextBox class="form-control" runat="server" ID="DateTextboxEnd_hopeEmr" AutoCompleteType="Disabled" name="date" Style="font-size: 12px; background-color: white;" placeholder="Input date..." onmousedown="dateEnd_HOPEemr();" />
                                        </td>
                                        <td style="width: 20px; text-align: center;">&nbsp; </td>
                                        <td>
                                            <asp:Button ID="btn_search_hopeEmr" runat="server" Text="Search" CssClass="btn btn-lightGreen btn-emr-small" OnClick="btn_search_hopeEmr_Click" />
                                        </td>
                                    </tr>
                                </table>
                            </div>

                        </div>
                    </div>

                    <a class="item" href="javascript:topFunction();">
                        <div id="myIDtoTop" class="bottomMenuu hidee">
                            <span>
                                <img src="../../Images/Result/ic_Arrow_Top.png" /></span>
                        </div>
                    </a>

                    <%-- end search box --%>

                    <%-- content box --%>

                    <div style="width: 100%;">

                        <div id="emr_div" runat="server" style="display: block">
                            <%--<iframe name="myIframe" id="myIframe" runat=server style="width:100%; height:550px; border:none; margin-top:-4%; overflow-y:scroll"></iframe>--%>
                            <iframe name="emr_data" id="emr_data" runat="server" style="width: 100%; height: calc(100vh - 47px); border: none; overflow-y: auto; margin-bottom: -9px;"></iframe>
                            <div style="display: none">
                                <asp:HiddenField ID="status_dataEmr" runat="server" />
                                <div id="tblPatientHistory" runat="server"></div>
                                <div id="img_noData_emr" runat="server" style="text-align: center; display: none;">
                                    <div>
                                        <br />
                                        <br />
                                        <img src="<%= Page.ResolveClientUrl("~/Images/Background/ic_noData.svg") %>" style="height: auto; width: 200px; margin-right: 3px;" />
                                    </div>
                                    <div runat="server" id="no_patient_data">
                                        <span>
                                            <h3 style="font-weight: 700; color: #585A6F">
                                                <label id="lblbhs_nodataunitemr">Oops! There is no data </label>
                                            </h3>
                                        </span>
                                        <span style="font-size: 14px; color: #585A6F">
                                            <label id="lblbhs_subnodataunitemr">Please search another date or parameter </label>
                                        </span>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div id="scanned_emr_div" runat="server" style="display: none;">
                            <div id="img_noData_scanned_mr" runat="server" style="text-align: center; display: none;">
                                <div>
                                    <img src="<%= Page.ResolveClientUrl("~/Images/Background/ic_noData.svg") %>" style="height: auto; width: 200px; margin-right: 3px; margin-top: 40px" />
                                </div>
                                <div runat="server" id="Div1">
                                    <span>
                                        <h3 style="font-weight: 700; color: #585A6F">
                                            <%--<label style="display: <%=setENG%>;">Oops! There is no data </label>
                                                <label style="display: <%=setIND%>;">Oops! Tidak ada data </label>--%>
                                            <label id="lblbhs_nodatascanemr">Oops! There is no data </label>
                                        </h3>
                                    </span>
                                    <span style="font-size: 14px; color: #585A6F">
                                        <%--<label style="display: <%=setENG%>;">Please search another date or parameter </label>
                                            <label style="display: <%=setIND%>;">Silakan cari tanggal atau parameter lain </label>--%>
                                        <label id="lblbhs_subnodatascanemr">Please search another date or parameter </label>
                                    </span>
                                </div>
                            </div>
                            <asp:HiddenField ID="status_scannedEmr" runat="server" />
                            <div style="margin: 20px;">
                                <div class="row" style="margin: 0px 0px 15px 0px;">
                                    <div class="col-sm-2" style="background-color: #2a3593; color: #52ccd6; border-radius: 10px 0px 0px 10px;">
                                        <label class="scannedTitle">O</label>
                                        <label class="scannedSubTitle">PD / ED</label>
                                    </div>
                                    <div class="col-sm-10" style="border-radius: 0px 10px 10px 0px; border: solid 1px lightgrey; padding: 12px 0px 8px 0px;">
                                        <a href="javascript:openTabScannedMR(1)" id="opd_ed_scanned">
                                            <div>
                                                <span style="font-size: 25px; font-weight: bold; color: #2a3593;"><i class="fa fa-caret-right" style="margin-left: 10px;"></i>Outpatient / Emergency </span>
                                            </div>
                                        </a>
                                        <div id="opd_data" runat="server" class="scannedIsi" style="display: block"></div>
                                    </div>
                                </div>

                                <div class="row" style="margin: 0px 0px 15px 0px;">
                                    <div class="col-sm-2" style="background-color: #0b6735; color: #83debf; border-radius: 10px 0px 0px 10px;">
                                        <label class="scannedTitle">I</label>
                                        <label class="scannedSubTitle">PD / ED</label>
                                    </div>
                                    <div class="col-sm-10" style="border-radius: 0px 10px 10px 0px; border: solid 1px lightgrey; padding: 12px 0px 8px 0px;">
                                        <a href="javascript:openTabScannedMR(2)" id="ipd_ed_scanned">
                                            <div>
                                                <span style="font-size: 25px; font-weight: bold; color: #0b6735;"><i class="fa fa-caret-right" style="margin-left: 10px;"></i>Inpatient / Emergency </span>
                                            </div>
                                        </a>
                                        <div id="ipd_data" runat="server" class="scannedIsi" style="display: block"></div>
                                    </div>
                                </div>

                                <div class="row" style="margin: 0px 0px 15px 0px;">
                                    <div class="col-sm-2" style="background-color: #d6721d; color: #eaf09d; border-radius: 10px 0px 0px 10px;">
                                        <label class="scannedTitle">M</label>
                                        <label class="scannedSubTitle">CU</label>
                                    </div>
                                    <div class="col-sm-10" style="border-radius: 0px 10px 10px 0px; border: solid 1px lightgrey; padding: 12px 0px 8px 0px;">
                                        <a href="javascript:openTabScannedMR(3)" id="mcu_ed_scanned">
                                            <div>
                                                <span style="font-size: 25px; font-weight: bold; color: #d6721d;"><i class="fa fa-caret-right" style="margin-left: 10px;"></i>Medical Check Up </span>
                                            </div>
                                        </a>
                                        <div id="mcu_data" runat="server" class="scannedIsi" style="display: block"></div>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div id="hope_emr_div" runat="server" style="display: none;">

                            <asp:HiddenField ID="status_hopeEmr" runat="server" />
                            <div class="container-fluid" style="margin-top: 13px; margin-bottom: 8px; display: none;">
                                <div class="col-sm-2" style="font-size: 17px; padding-left: 0px"><b>Hope MR List</b></div>
                                <div class="col-sm-2" style="float: right" id="srcDoctorHopeEMR" runat="server">
                                    <asp:TextBox runat="server" ID="src_doctorName_hopeEmr" placeholder="Search doctor" AutoPostBack="true" OnTextChanged="src_doctorName_hopeEmr_TextChanged"></asp:TextBox>
                                    <span class="fa fa-search form-control-feedback" style="margin-top: -6px; margin-right: 4px;"></span>
                                </div>
                            </div>
                            


                            
                               <div class="row" style="margin:20px">
                                   <div class="col-sm-2 no-padding" style="border-radius: 7px; border: solid 2px lightgrey;">
                                       <div id="hope__emr" style="width:100%;overflow-y:auto;height: calc(100vh - 135px)">
                                                <asp:UpdatePanel runat="server" ID="updateListMRHope">
                                                    <ContentTemplate>
                                                        <asp:GridView runat="server" ID="gvHopeMRList" AutoGenerateColumns="False" 
                                                        CssClass="table-condensed table-hover" 
                                                        BorderWidth="0" BorderColor="LightGray" 
                                                        ShowHeader="false" Width="100%">
                                                            <Columns>
                                                            <asp:TemplateField>
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="btnTanggalHopeMR" runat="server" OnClick="btnTanggalHopeMR_Click">
                                                                        <div style="padding: 5px; margin-top: -6px; border-top: 1px solid lightgrey; font-size:12px">
                                                                            <div style="padding-bottom: 5px;">
                                                                                <asp:Image runat="server" ID="Image1" ImageUrl="~/Images/PatientHistory/ic_newtab_blue.svg" />
                                                                                <b style="text-decoration: underline; color: #0000FF;"><%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem,"admissionDate")).ToString("dd MMM yyyy") %> </b>
                                                                                <asp:HiddenField ID="hdnLinkHopeMR" runat="server" Value='<%# Bind("linkMRHOPE") %>' />
                                                                            </div>
                                                                            <asp:Label runat="server" ID="lblHopeMrAdmNo" Style="color: #A9A9A9;" Text='<%# Bind("admissionNo") %>'></asp:Label>
                                                                            <br />
                                                                            <asp:Label runat="server" ID="lblHopeMrDoc" Style="color: #A9A9A9;" Text='<%# Bind("entryUser") %>'></asp:Label>
                                                                       </div>
                                                                    </asp:LinkButton>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            </Columns>
                                                        </asp:GridView>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </div>
                                   </div>
                                   <div class="col-sm-10">
                                       <div style="border-radius: 7px; border: solid 2px lightgrey;">
                                           <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="updateListMRHope">
                                                <ProgressTemplate>
                                                    <div style="background-color: rgba(255,255,255,0.5); text-align: center; z-index: 5; position: fixed; width: 100%; left: 0px; height: calc(100vh - 50px); border-radius: 6px;">
                                                        <div class="modal-backdrop" style="background-color: red; opacity: 0; text-align: center">
                                                        </div>
                                                        <div style="margin-top: 7%;margin-left:14%">
                                                            <img alt="" height="225px" width="225px" style="background-color: transparent; vertical-align: middle;" class="login-box-body" src="<%= Page.ResolveClientUrl("~/Images/Background/loading-beat.gif") %>" />
                                                        </div>
                                                    </div>
                                                </ProgressTemplate>
                                            </asp:UpdateProgress>
                                            <iframe name="myIframe" id="myIframe" runat="server" style="width: 100%; height: calc(100vh - 135px); border: none; margin-bottom: -6px;"></iframe>
                                            <div id="img_noData_hope_emr" runat="server" style="text-align: center; display: none; height: calc(100vh - 135px);">
                                                <div>
                                                   <br />
                                                   <br />
                                                   <img src="<%= Page.ResolveClientUrl("~/Images/Background/ic_noData.svg") %>" style="height: auto; width: 200px; margin-right: 3px; margin-top: 40px" />
                                               </div>
                                                <div runat="server" id="Div2">
                                                   <span>
                                                       <h3 style="font-weight: 700; color: #585A6F">
                                                           <%--<label style="display: <%=setENG%>;">Oops! There is no data </label>
                                                               <label style="display: <%=setIND%>;">Oops! Tidak ada data </label>--%>
                                                           <label id="lblbhs_nodatahopeemr">Oops! There is no data </label>
                                                       </h3>
                                                   </span>
                                                   <span style="font-size: 14px; color: #585A6F">
                                                       <%--<label style="display: <%=setENG%>;">Please search another date or parameter </label>
                                                           <label style="display: <%=setIND%>;">Silakan cari tanggal atau parameter lain </label>--%>
                                                       <label id="lblbhs_subnodatahopeemr">Please search another date or parameter </label>
                                                   </span>
                                               </div>
                                            </div>
                                       </div>
                                   </div>
                               </div>
  
                        </div>

                        <div id="other_unit_emr" runat="server" style="display: none;">
                            <asp:HiddenField ID="status_other_unit_Emr" runat="server" />
                            <div class="container-fluid" style="margin-top: 13px; margin-bottom: 8px; display: none;">
                                <div class="col-sm-2" style="font-size: 12px; font-family: Arial, Helvetica, sans-serif; padding-left: 0px"><b>Other Unit EMR</b></div>
                            </div>

                            <div id="other_unit_emr_data" runat="server"></div>
                            <div id="img_noData_other_mr" runat="server" style="text-align: center; display: none;">
                                <div>
                                    <br />
                                    <br />
                                    <img src="<%= Page.ResolveClientUrl("~/Images/Background/ic_noData.svg") %>" style="height: auto; width: 200px; margin-right: 3px; margin-top: 40px" />
                                </div>
                                <div runat="server" id="Div3">
                                    <span>
                                        <h3 style="font-weight: 700; color: #585A6F">
                                            <%--<label style="display: <%=setENG%>;">Oops! There is no data </label>
                                                <label style="display: <%=setIND%>;">Oops! Tidak ada data </label>--%>
                                            <label id="lblbhs_nodataotheremr">Oops! There is no data </label>
                                        </h3>
                                    </span>
                                    <span style="font-size: 14px; color: #585A6F">
                                        <%--<label style="display: <%=setENG%>;">Please search another date or parameter </label>
                                            <label style="display: <%=setIND%>;">Silakan cari tanggal atau parameter lain </label>--%>
                                        <label id="lblbhs_subnodataotheremr">Please search another date or parameter </label>
                                    </span>
                                </div>
                            </div>
                        </div>
                        <asp:HiddenField ID="hf_load_encounter" runat="server" Value="0" />
                        <asp:HiddenField ID="hf_string_builder" runat="server" Value="" />
                    </div>

                    <%-- end content box --%>
                </div>

            </ContentTemplate>
        </asp:UpdatePanel>



        <%-- ========================================================== modalCompound ================================================================ --%>
        <div class="modal fade" id="modalCompound" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
            <div class="modal-dialog" style="width: 70%;">
                <div class="modal-content" style="border-radius: 7px; height: 100%;">
                    <div class="modal-header" style="height: 40px; padding-top: 10px; padding-bottom: 5px">
                        <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                        <h4 class="modal-title" style="text-align: left">
                            <asp:Label ID="headerCompound" Style="font-family: Helvetica, Arial, sans-serif; font-weight: bold" runat="server"></asp:Label></h4>
                    </div>
                    <div class="modal-body">
                        <div style="width: 100%; background-color: white">
                            <asp:GridView ID="gvw_detail_compound" runat="server" CssClass="table table-striped table-condensed" BorderColor="Transparent" AutoGenerateColumns="false">
                                <Columns>
                                    <asp:TemplateField HeaderText="Item" HeaderStyle-ForeColor="#3C8DBC" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:Label ID="itemName" Wrap="true" Style="resize: none" BackColor="Transparent" Width="400px" ReadOnly="true" BorderColor="Transparent" runat="server" Text='<%# Bind("itemName") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Qty" HeaderStyle-ForeColor="#3C8DBC" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:TextBox ID="Oty" Wrap="true" Style="resize: none" TextMode="MultiLine" BackColor="Transparent" Width="100%" ReadOnly="true" BorderColor="Transparent" runat="server" Text='<%# Bind("quantity") %>'></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="U.O.M." HeaderStyle-ForeColor="#3C8DBC" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:TextBox ID="uom" Wrap="true" Style="resize: none" TextMode="MultiLine" BackColor="Transparent" Width="100%" ReadOnly="true" BorderColor="Transparent" runat="server" Text='<%# Bind("uom") %>'></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Dose Text" HeaderStyle-ForeColor="#3C8DBC" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:TextBox ID="dose_text" Wrap="true" Style="resize: none" TextMode="MultiLine" BackColor="Transparent" Width="100%" ReadOnly="true" BorderColor="Transparent" runat="server" Text='<%# Bind("doseText") %>'></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Instruction" HeaderStyle-ForeColor="#3C8DBC" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:TextBox ID="instruction" Wrap="true" Style="resize: none" TextMode="MultiLine" BackColor="Transparent" Width="100%" ReadOnly="true" BorderColor="Transparent" runat="server" Text='<%# Bind("instruction") %>'></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <%-- ========================================================== modalCompound ================================================================ --%>
    </form>

    <script type="text/javascript">
        //  $(window).load(function() {
        //$(".loadPage").fadeOut("slow");
        //  });

        function OnClick() {
            if (testpopup.style.display == "none") {
                testpopup.style.display = "";
                $("[id$='txtSearchItem']").focus();
                doctorPopUp.style.display = "none";
            }
            else
                testpopup.style.display = "none";
        }

        function doctorClick() {
            if (doctorPopUp.style.display == "none") {
                doctorPopUp.style.display = "";
                $("[id$='txtSrcDoctor']").focus();
                testpopup.style.display = "none";
            }
            else
                doctorPopUp.style.display = "none";
        }

        function Open(content) {
            document.getElementById('<%=hfCompoundName.ClientID%>').value = content;
            document.getElementById('<%=btnCompoundDetail.ClientID%>').click();
            openModal();
            return true;
        }

        function openTabScannedMR(type) {
            if (type == 1) {
                var opd_data = document.getElementById('<%= opd_data.ClientID%>');

                if (opd_data.style.display == "block")
                    opd_data.style.display = "none";
                else
                    opd_data.style.display = "block";

            } else if (type == 2) {
                var ipd_data = document.getElementById('<%= ipd_data.ClientID%>');

                if (ipd_data.style.display == "block")
                    ipd_data.style.display = "none";
                else
                    ipd_data.style.display = "block";

            } else {
                var mcu_data = document.getElementById('<%= mcu_data.ClientID%>');

                if (mcu_data.style.display == "block")
                    mcu_data.style.display = "none";
                else
                    mcu_data.style.display = "block";
            }
        }

        function openModal() {
            $('#modalCompound').modal('show');
            return true;
        }

        function modalLaboratory(AdmId) {
            $('#laboratoryResult').modal('show');
        }

        $(document).ready(function () {
            //fungsi untuk action in postback updatepanel
            var prm = Sys.WebForms.PageRequestManager.getInstance();
            var hf_tab_postion = document.getElementById('<%= hf_tab_postion.ClientID%>');
            if (prm != null) {

                prm.add_beginRequest(function (sender, e) {
                    if (sender._postBackSettings.panelsToUpdate != null) {

                    }
                });

                prm.add_endRequest(function (sender, e) {
                    if (sender._postBackSettings.panelsToUpdate != null) {
                        if (hf_tab_postion.value == "1")
                            tab_patientHistory(1);
                        else if (hf_tab_postion.value == "2")
                            tab_patientHistory(2);
                        else if (hf_tab_postion.value == "3")
                            tab_patientHistory(3);
                        else if (hf_tab_postion.value == "4")
                            tab_patientHistory(4);
                        else
                            tab_patientHistory(5);
                    }
                });
            };
        });

        function tab_patientHistory(content) {
            var hf_tab_postion = document.getElementById('<%= hf_tab_postion.ClientID%>');

            /* Button */
            var btn_emr = document.getElementById('<%= btn_emr.ClientID %>');
            var btn_scanned_emr = document.getElementById('<%= btn_scanned_emr.ClientID%>');
            var btn_hope_emr = document.getElementById('<%= btn_hope_emr.ClientID%>');
            var btn_other_emr = document.getElementById('<%= btn_other_emr.ClientID%>');

            /* Div */
            var emr_div = document.getElementById('<%= emr_div.ClientID%>');
            var hope_emr_div = document.getElementById('<%= hope_emr_div.ClientID%>');
            var scanned_emr_div = document.getElementById('<%= scanned_emr_div.ClientID%>');
            var other_unit_emr = document.getElementById('<%= other_unit_emr.ClientID%>');

            /* Search */
            var src_scanned_emr = document.getElementById('<%= src_scanned_emr.ClientID%>');
            var src_hope_emr = document.getElementById('<%= src_hope_emr.ClientID%>');
            var src_other_unit = document.getElementById('<%= src_other_unit.ClientID%>');

            if (content == 1) { // validasi untuk tab Electronic Unit MR
                hf_tab_postion.value = "1";
                var status_dataEmr = document.getElementById('<%= status_dataEmr.ClientID%>');
                var img_noData_emr = document.getElementById('<%= img_noData_emr.ClientID%>');

                btn_emr.style.backgroundColor = "#d6dbff";
                btn_scanned_emr.style.backgroundColor = "transparent";
                btn_other_emr.style.backgroundColor = "transparent";
                btn_hope_emr.style.backgroundColor = "transparent";

                emr_div.style.display = "block";
                hope_emr_div.style.display = "none";
                scanned_emr_div.style.display = "none";
                other_unit_emr.style.display = "none";

                src_scanned_emr.style.display = "none";
                src_hope_emr.style.display = "none";
                src_other_unit.style.display = "none";

                if (status_dataEmr.value != "empty") {
                    if (status_dataEmr.value === "LOAD MORE") {
                        btn_load_more.style.display = "none";
                    }
                    img_noData_emr.style.display = "none";
                } else {
                    img_noData_emr.style.display = "block";
                }
            } else if (content == 2) { // validasi untuk tab Other Unit MR
                hf_tab_postion.value = "2";
                var status_other_unit_Emr = document.getElementById('<%= status_other_unit_Emr.ClientID%>');
                var img_noData_other_mr = document.getElementById('<%= img_noData_other_mr.ClientID%>');

                btn_emr.style.backgroundColor = "transparent";
                btn_scanned_emr.style.backgroundColor = "transparent";
                btn_other_emr.style.backgroundColor = "#d6dbff";
                btn_hope_emr.style.backgroundColor = "transparent";

                emr_div.style.display = "none";
                hope_emr_div.style.display = "none";
                scanned_emr_div.style.display = "none";
                other_unit_emr.style.display = "block";

                src_scanned_emr.style.display = "none";
                src_hope_emr.style.display = "none";
                src_other_unit.style.display = "block";


                img_noData_other_mr.style.display = "none";
                if (status_other_unit_Emr.value == "empty")
                    img_noData_other_mr.style.display = "block";

            } else if (content == 3) { // validasi untuk tab Scanned MR
                hf_tab_postion.value = "3";
                btn_emr.style.backgroundColor = "transparent";
                btn_scanned_emr.style.backgroundColor = "#d6dbff";
                btn_other_emr.style.backgroundColor = "transparent";
                btn_hope_emr.style.backgroundColor = "transparent";

                emr_div.style.display = "none";
                hope_emr_div.style.display = "none";
                scanned_emr_div.style.display = "block";
                other_unit_emr.style.display = "none";

                src_scanned_emr.style.display = "block";
                src_hope_emr.style.display = "none";
                src_other_unit.style.display = "none";

            } else { // validasi untuk tab HOPE MR
                hf_tab_postion.value = "4";
                var status_hopeEmr = document.getElementById('<%= status_hopeEmr.ClientID%>');
                var img_noData_hope_emr = document.getElementById('<%= img_noData_hope_emr.ClientID%>');

                btn_emr.style.backgroundColor = "transparent";
                btn_scanned_emr.style.backgroundColor = "transparent";
                btn_other_emr.style.backgroundColor = "transparent";
                btn_hope_emr.style.backgroundColor = "#d6dbff";

                emr_div.style.display = "none";
                hope_emr_div.style.display = "block";
                scanned_emr_div.style.display = "none";
                other_unit_emr.style.display = "none";

                src_scanned_emr.style.display = "none";
                src_hope_emr.style.display = "block";
                src_other_unit.style.display = "none";

                img_noData_hope_emr.style.display = "none";
                if (status_hopeEmr.value == "empty")
                    img_noData_hope_emr.style.display = "block";
            }
        }

        function remove() {
            var tblPatientHistory = document.getElementById('<%=tblPatientHistory.ClientID%>');
            while (tblPatientHistory.hasChildNodes()) {
                tblPatientHistory.removeChild(tblPatientHistory.lastChild);
            }
        }

        function dateStart_emr() {
            var dp = $('#<%=DateTextboxStart_emr.ClientID%>');
            var maxDate = new Date();
            dp.datepicker({
                changeMonth: true,
                changeYear: true,
                format: "dd M yyyy",
                language: "tr",
                endDate: maxDate
            }).on('changeDate', function (ev) {
                $(this).blur();
                $(this).datepicker('hide');
            });
        }

        function dateEnd_emr() {
            var dp = $('#<%=DateTextboxEnd_emr.ClientID%>');
            var maxDate = new Date();
            dp.datepicker({
                changeMonth: true,
                changeYear: true,
                format: "dd M yyyy",
                language: "tr",
                endDate: maxDate
            }).on('changeDate', function (ev) {
                $(this).blur();
                $(this).datepicker('hide');
            });
        }

        function dateStart_otherUnit() {
            var ddlYear = document.getElementById("<%=ddl_year_other_mr.ClientID %>");
            var selectedValue = ddlYear.value;

            var minDate = new Date();
            var maxDate = new Date();
            var pastYear = minDate.getFullYear() - selectedValue;
            minDate.setFullYear(pastYear);
            minDate.setMonth(12);
            minDate.setDate(1);

            var dp = $('#<%=DateTextboxStart_other.ClientID%>');
            dp.datepicker({
                autoclose: true,
                startDate: minDate,
                format: "dd M yyyy",
                endDate: maxDate
            }).on('changeDate', function (ev) {
                $(this).blur();
                $(this).datepicker('hide');
            });
        }

        function dateEnd_otherUnit() {
            var dp = $('#<%=DateTextboxEnd_other.ClientID%>');
            var ddlYear = document.getElementById("<%=ddl_year_other_mr.ClientID %>");
            var selectedValue = ddlYear.value;

            var minDate = new Date();
            var maxDate = new Date();
            var pastYear = minDate.getFullYear() - selectedValue;
            minDate.setFullYear(pastYear);
            minDate.setMonth(12);
            minDate.setDate(1);

            dp.datepicker({
                autoclose: true,
                format: "dd M yyyy",
                startDate: minDate,
                endDate: maxDate
            }).on('changeDate', function (ev) {
                $(this).blur();
                $(this).datepicker('hide');
            });
        }

        function dateStart_HOPEemr() {
            var dp = $('#<%=DateTextboxStart_hopeEmr.ClientID%>');
            var maxDate = new Date();
            dp.datepicker({
                changeMonth: true,
                changeYear: true,
                format: "dd M yyyy",
                language: "tr",
                endDate: maxDate
            }).on('changeDate', function (ev) {
                $(this).blur();
                $(this).datepicker('hide');
            });
        }

        function dateEnd_HOPEemr() {
            var dp = $('#<%=DateTextboxEnd_hopeEmr.ClientID%>');
            var maxDate = new Date();
            dp.datepicker({
                changeMonth: true,
                changeYear: true,
                format: "dd M yyyy",
                language: "tr",
                endDate: maxDate
            }).on('changeDate', function (ev) {
                $(this).blur();
                $(this).datepicker('hide');
            });
        }

        function dateStart_ScannedMR() {
            testpopup.style.display = "none";
            var dp = $('#<%=DateTextboxStart_scanned.ClientID%>');
            var maxDate = new Date();
            dp.datepicker({
                changeMonth: true,
                changeYear: true,
                format: "dd M yyyy",
                language: "tr",
                endDate: maxDate
            }).on('changeDate', function (ev) {
                $(this).blur();
                $(this).datepicker('hide');
            });
        }

        function dateEnd_ScannedMR() {
            testpopup.style.display = "none";
            var maxDate = new Date();
            var dp = $('#<%=DateTextboxEnd_scanned.ClientID%>');
            dp.datepicker({
                changeMonth: true,
                changeYear: true,
                format: "dd M yyyy",
                language: "tr",
                endDate: maxDate
            }).on('changeDate', function (ev) {
                $(this).blur();
                $(this).datepicker('hide');
            });
        }

        const unique = (value, index, self) => {
            return self.indexOf(value) === index
        }

        function selectAllDoctor() {
            var hf_all_doctor = document.getElementById('<%=hf_all_doctor.ClientID%>').value.split(';');

            if (document.getElementById("all_doctor_filter").checked) {
                for (var i = 0; i < hf_all_doctor.length; i++) {
                    document.getElementById("" + hf_all_doctor[i] + "").checked = true;
                }
                document.getElementById('<%=selected_doctor_filter.ClientID%>').value = "";
                document.getElementById('<%=selected_doctor_filter.ClientID%>').value = document.getElementById('<%=hf_all_doctor.ClientID%>').value;
            } else {
                for (var i = 0; i < hf_all_doctor.length; i++) {
                    document.getElementById("" + hf_all_doctor[i] + "").checked = false;
                }
                document.getElementById('<%=selected_doctor_filter.ClientID%>').value = "";
            }
        }

        function selectAllForm() {
            var formAll = document.getElementById('<%=hf_all_form.ClientID%>').value.split(',');
            console.log("All Form : ", formAll);

            if (document.getElementById("all_form_filter").checked) {
                for (var i = 0; i < formAll.length; i++) {
                    document.getElementById("" + formAll[i] + "").checked = true;
                }
                document.getElementById('<%=selected_form_filter.ClientID%>').value = "";
                document.getElementById('<%=selected_form_filter.ClientID%>').value = document.getElementById('<%=hf_all_form.ClientID%>').value;
            } else {
                for (var i = 0; i < formAll.length; i++) {
                    document.getElementById("" + formAll[i] + "").checked = false;
                }
                document.getElementById('<%=selected_form_filter.ClientID%>').value = "";
            }
        }

        function doctorSelected(content) {
            var selected_doctor_filter = document.getElementById('<%=selected_doctor_filter.ClientID%>');
            var form = "";
            var all_doctor_filter = document.getElementById('all_doctor_filter');
            var allForm = document.getElementById('<%= DocAll.ClientID%>');

            if (selected_doctor_filter.value != "") {
                var status = false;
                var arrayCheckForm = selected_doctor_filter.value.split(';');

                status = false;
                for (var i = 0; i < arrayCheckForm.length; i++) {
                    if (arrayCheckForm[i] != content) {
                        if (form == "") {
                            form = arrayCheckForm[i];
                        } else {
                            form = form + ";" + arrayCheckForm[i];
                        }
                    }
                    else {
                        status = true;
                    }
                }

                if (status == false) {
                    form = form + ";" + content;
                }

            } else {
                form = content;
            }

            selected_doctor_filter.value = "";
            selected_doctor_filter.value = form;

            chechkAllDoctor();
        }

        function chechkAllDoctor() {
            var selected_doctor_filter = document.getElementById('<%=selected_doctor_filter.ClientID%>');
            var allDoctor = document.getElementById('<%= DocAll.ClientID%>');
            var all_doctor_filter = document.getElementById('all_doctor_filter');

            if (all_doctor_filter != null) {
                var tempData = selected_doctor_filter.value.split(';').filter(unique);
                console.log(tempData.length, "-", parseInt(allDoctor.value));

                if (tempData.length === parseInt(allDoctor.value))
                    all_doctor_filter.checked = true;
                else
                    all_doctor_filter.checked = false;
            }
        }

        function formSelected(content) {
            var formSelected = document.getElementById('<%=selected_form_filter.ClientID%>');
            var form = "";
            var allChechked = document.getElementById('all_form_filter');
            var allForm = document.getElementById('<%= conAll.ClientID%>');

            if (formSelected.value != "") {
                var status = false;
                var arrayCheckForm = formSelected.value.split(',');

                status = false;
                for (var i = 0; i < arrayCheckForm.length; i++) {
                    if (arrayCheckForm[i] != content) {
                        if (form == "") {
                            form = arrayCheckForm[i];
                        } else {
                            form = form + "," + arrayCheckForm[i];
                        }
                    }
                    else {
                        status = true;
                    }
                }

                if (status == false) {
                    form = form + "," + content;
                }

            } else {
                form = content;
            }

            formSelected.value = "";
            formSelected.value = form;

            chechkAllFormType();
        }

        function chechkAllFormType() {
            var formSelected = document.getElementById('<%=selected_form_filter.ClientID%>');
            var allForm = document.getElementById('<%= conAll.ClientID%>');
            var allChechked = document.getElementById('all_form_filter');

            if (allChechked != null) {
                var tempData = formSelected.value.split(',').filter(unique);
                if (tempData.length === parseInt(allForm.value))
                    allChechked.checked = true;
                else
                    allChechked.checked = false;
            }
        }

        function topFunction() {
            document.body.scrollTop = 0;
            document.documentElement.scrollTop = 0;
        }

        $('body').scroll(function (e) {
            if ($(this).scrollTop() > 250) {
                $("#myIDtoTop").attr('class', 'bottomMenuu showw');
            } else {
                $("#myIDtoTop").attr('class', 'bottomMenuu hidee');
            }
        });

    </script>

</body>

</html>
