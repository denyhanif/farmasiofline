<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FormPatientDashboard.aspx.cs" Inherits="Form_FormViewer_FormPatientDashboard" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <!-- Tell the browser to be responsive to screen width -->
    <meta content="width=device-width, initial-scale=1, maximum-scale=1, user-scalable=no" name="viewport" />
    <!-- Bootstrap 3.3.6 -->
    <link rel="stylesheet" href="~/Content/bootstrap/css/bootstrap.css" />
    <!-- Font Awesome -->
    <link rel="stylesheet" href="~/Content/font-awesome/css/font-awesome.css" />
    <!-- Datepicker -->
    <link rel="stylesheet" href="~/Content/plugins/datepicker/datepicker3.css" />
    <!-- Mask -->
    <link rel="stylesheet" href="~/Content/plugins/jasny-bootstrap/css/jasny-bootstrap.min.css" />
    <!-- Theme style -->
    <link rel="stylesheet" href="~/Content/dist/css/AdminLTE.css" />
    <link rel="stylesheet" href="~/Content/dist/css/skins/skin-blue-light.css" />
    <link rel="stylesheet" href="~/Content/EMR-Doctor.css" />
    <link rel="stylesheet" href="~/Content/EMR-PharmacyOffline.css" />
    <!-- Select Bootstrap -->
    <link rel="stylesheet" href="~/Content/bootstrap-select/css/bootstrap-select.css" />

    <link href="~/favicon.ico" rel="shortcut icon" type="image/x-icon" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />

    <style type="text/css">
        
    </style>
</head>

<body>
    <form id="form1" runat="server">

        <asp:ScriptManager ID="ScriptManager1" runat="server">
            <Scripts>
                <asp:ScriptReference Path="~/Content/plugins/jQuery/jQuery-2.2.0.min.js" />
                <asp:ScriptReference Path="~/Content/bootstrap/js/bootstrap.min.js" />
                <asp:ScriptReference Path="~/Content/plugins/jasny-bootstrap/js/jasny-bootstrap.min.js" />
                <asp:ScriptReference Path="~/Content/plugins/datepicker/bootstrap-datepicker.js" />
                <asp:ScriptReference Path="~/Content/dist/js/app.min.js" />
                <asp:ScriptReference Path="~/Content/bootstrap-select/js/bootstrap-select.js" />
            </Scripts>
        </asp:ScriptManager>

        <%--YOUR CODE HTML HERE--%>

        <div style="background-color: #CDD2DD;">
            <asp:HiddenField ID="hfOrgId" runat="server" />
            <asp:HiddenField ID="hfPatientId" runat="server" />
            <asp:HiddenField ID="hfEncounterId" runat="server" />
            <asp:HiddenField ID="hfAdmissionId" runat="server" />
            <asp:HiddenField ID="hfDoctorId" runat="server" />

            <div class="container-fluid" style="padding-top: 10px">

                <!-- ######################################################--- box empty ---###################################################### -->
                <div class="row" style="padding-left: 8px; padding-right: 8px;">
                    <div id="divkosong_reminder" runat="server" class="col-lg-3" style="margin-bottom: 15px; padding-left: 7px; padding-right: 7px;">
                        <div class="box-small-empty">
                            <div id="header-reminders-hide" class="padding-title-empty">
                                <asp:Image ID="Image4" ImageUrl="~/Images/Dashboard/ic_Reminder.svg" CssClass="title-img-box" runat="server" />
                                <label id="lblbhs_reminder_hide" class="font-header-dashboard title-text-box" style="color: #C43D32;">Reminder</label>
                            </div>
                            <div class="padding-content-empty">
                                <asp:Label ID="lblemptyreminder_new" runat="server" CssClass="content-text-box-empty" Text=" "> 
                                    <i class="fa fa-ban"></i> <label id="lblbhs_noreminder_hide">No reminder</label>
                                </asp:Label>
                            </div>
                        </div>
                    </div>

                    <div id="divkosong_allergy" runat="server" class="col-lg-3" style="margin-bottom: 15px; padding-left: 7px; padding-right: 7px;">
                        <div class="box-small-empty">
                            <div id="header-allergies-hide" class="padding-title-empty">
                                <asp:Image ID="Image3" ImageUrl="~/Images/Dashboard/ic_Allergies.svg" CssClass="title-img-box" runat="server" />
                                <label id="lblbhs_allergies_hide" class="font-header-dashboard title-text-box" style="color: #C43D32;">Allergies</label>
                            </div>
                            <div class="padding-content-empty">
                                <asp:Label ID="Lblemptyallergy_new" runat="server" CssClass="content-text-box-empty" Text=" "> 
                                    <i class="fa fa-ban"></i> <label id="lblbhs_noallergies_hide">No allergies</label>
                                </asp:Label>
                            </div>
                        </div>
                    </div>

                    <div id="divkosong_routinemed" runat="server" class="col-lg-3" style="margin-bottom: 15px; padding-left: 7px; padding-right: 7px;">
                        <div class="box-small-empty">
                            <div id="header-routinemed-hide" class="padding-title-empty">
                                <asp:Image ID="Image5" ImageUrl="~/Images/Dashboard/ic_Routine_new.svg" CssClass="title-img-box" runat="server" />
                                <label id="lbhbhs_routinemedication_hide" class="font-header-dashboard title-text-box" style="color: #0013b5;">Routine Medication</label>
                            </div>
                            <div class="padding-content-empty">
                                <asp:Label ID="lblemptyroutinemed_new" runat="server" CssClass="content-text-box-empty" Text=" "> 
                                    <i class="fa fa-ban"></i> <label id="lblbhs_noroutinemedication_hide">No routine medication</label>
                                </asp:Label>
                            </div>
                        </div>
                    </div>

                    <div id="divkosong_procresult" runat="server" class="col-lg-3" style="margin-bottom: 15px; padding-left: 7px; padding-right: 7px;">
                        <div class="box-small-empty">
                            <div id="header-procresult-hide" class="padding-title-empty">
                                <asp:Image ID="Image6" ImageUrl="~/Images/Dashboard/ic_LatestHistory_new.svg" CssClass="title-img-box" runat="server" />
                                <label id="lblbhs_riwayattindakan_new" class="font-header-dashboard title-text-box" style="color: #219000;">Procedure Result</label>
                            </div>
                            <div class="padding-content-empty">
                                <asp:Label ID="lblemptyprocresult_new" runat="server" CssClass="content-text-box-empty" Text=" "> 
                                    <i class="fa fa-ban"></i> <label id="lblbhs_notindakan_hide">No procedure result</label>
                                </asp:Label>
                            </div>
                        </div>
                    </div>

                </div>
                <!-- ######################################################--- end box empty ---###################################################### -->

                <!-- ######################################################--- box isi data ---###################################################### -->
                <div class="row">
                    <div id="divisi_reminder" runat="server" class="col-sm-12 box-margin-btm">
                        <asp:UpdatePanel runat="server">
                            <ContentTemplate>
                                <div class="box-big-filled">
                                    <div id="header-reminders" class="header-title-filled">
                                        <div class="row">
                                            <div class="col-sm-3">
                                                <asp:Image ID="Image1" ImageUrl="~/Images/Dashboard/ic_Reminder.svg" CssClass="title-img-box" runat="server" />
                                                <label id="lblbhs_reminder" class="font-header-dashboard title-text-box" style="color: #C43D32;">Reminder</label>
                                            </div>
                                            <div class="col-sm-9" style="text-align: right; padding-top: 5px; padding-right: 0px;">
                                                <asp:HiddenField runat="server" ID="hfjsonreminder" />
                                                <%--<div class="pretty p-icon p-curve">
                                                    <asp:CheckBox runat="server" ID="chkreminder" />
                                                    <div class="state p-success">
                                                        <i class="icon fa fa-check font-content-dashboard"></i>
                                                        <label id="lblbhs_hideotherdoctor" style="font-size: 12px;" class="font-content-dashboard">Hide Others Doctor's Reminder </label>
                                                    </div>
                                                </div>--%>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-sm-12" style="padding-left: 15px;">
                                            <div class="box-padding-btm">
                                                <asp:Label ID="lblemptyreminder" runat="server" CssClass="content-text-box-empty" Text=""> <i class="fa fa-ban"></i>
                                                    <label id="lblbhs_noreminder"> No Reminder </label>
                                                </asp:Label>
                                                <asp:GridView ID="gvw_reminder" runat="server" BorderWidth="0" BorderColor="#b9b9b9"
                                                    AutoGenerateColumns="False" CssClass="table-condensed table-fill-width"
                                                    HeaderStyle-CssClass="text-center" HeaderStyle-HorizontalAlign="Center">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="&nbsp;&nbsp;No" ItemStyle-Width="4%" HeaderStyle-Font-Size="11px" ItemStyle-VerticalAlign="Top" HeaderStyle-CssClass="font-content-dashboard table-sub-header-label">
                                                            <ItemTemplate>
                                                                <asp:Label Font-Size="11px" class="font-content-dashboard" Font-Names="Helvetica, Arial, sans-serif" ID="tindakan" runat="server" Style="padding-left: 10px;"> <%# Container.DataItemIndex + 1 %> </asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="&nbsp;&nbsp;&nbsp;Tgl. Dibuat" ItemStyle-Width="12%" HeaderStyle-Font-Size="11px" ItemStyle-VerticalAlign="Top" HeaderStyle-CssClass="font-content-dashboard table-sub-header-label">
                                                            <ItemTemplate>
                                                                <asp:HiddenField runat="server" ID="hfdoctorid" Value='<%# Bind("doctor_id") %>' />
                                                                <asp:Label Font-Size="11px" class="font-content-dashboard" Font-Names="Helvetica, Arial, sans-serif" ID="tgl_dibuat" runat="server" Text='<%# Bind("created_date") %>' Style="padding-left: 10px;"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="&nbsp;&nbsp;&nbsp;Reminder" ItemStyle-Width="60%" HeaderStyle-Font-Size="11px" ItemStyle-VerticalAlign="Top" HeaderStyle-CssClass="font-content-dashboard table-sub-header-label">
                                                            <ItemTemplate>
                                                                <asp:Label Font-Size="11px" class="font-content-dashboard" Font-Names="Helvetica, Arial, sans-serif" ID="reminder" runat="server" Text='<%# Bind("notification") %>' Style="padding-left: 10px;"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="&nbsp;&nbsp;&nbsp;Doctor" ItemStyle-Width="24%" HeaderStyle-Font-Size="11px" ItemStyle-VerticalAlign="Top" HeaderStyle-CssClass="font-content-dashboard table-sub-header-label">
                                                            <ItemTemplate>
                                                                <asp:Label Font-Size="11px" class="font-content-dashboard" Font-Names="Helvetica, Arial, sans-serif" ID="doctor" runat="server" Text='<%# Bind("doctor_name") %>' Style="padding-left: 10px;"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>

                    <div id="divisi_allergy" runat="server" class="col-sm-12 box-margin-btm">
                        <asp:UpdatePanel runat="server">
                            <ContentTemplate>
                                <div class="box-big-filled">
                                    <div id="header-allergies" class="header-title-filled">
                                        <asp:Image ID="Image8" ImageUrl="~/Images/Dashboard/ic_Allergies.svg" CssClass="title-img-box" runat="server" />
                                        <label id="lblbhs_allergies" class="font-header-dashboard title-text-box" style="color: #C43D32;">Allergies</label>
                                    </div>
                                    <div class="row">
                                        <div style="padding-top: 5px; padding-left: 25px; padding-right: 0px; display: inline-table; min-width: 20%; max-width: 33%;">
                                            <label id="lblbhs_drugs" class="font-subheader-dashboard sub-header-label" style="font-weight: bold; font-size: 14px;">Drugs</label>
                                            <br />
                                            <div class="box-padding-btm">
                                                <asp:Label ID="Lblemptyaledrug" runat="server" CssClass="content-text-box-empty" Text=" "> <i class="fa fa-ban"></i> 
                                            <label id="lblbhs_noallergiesdrugs">No allergies</label>
                                                </asp:Label>
                                                <asp:Repeater runat="server" ID="DrugAllergy">
                                                    <ItemTemplate>
                                                        <li>
                                                            <asp:Label ID="NameLabel" runat="server" class="font-content-dashboard" Text='<%#Eval("allergy") %>' Enabled="false" />
                                                        </li>
                                                    </ItemTemplate>
                                                </asp:Repeater>
                                            </div>
                                        </div>
                                        <div style="padding-top: 5px; padding-left: 10px; display: inline-table; min-width: 20%; max-width: 33%;">
                                            <label id="lblbhs_food" class="font-subheader-dashboard sub-header-label" style="font-weight: bold; font-size: 14px;">Food</label>
                                            <br />
                                            <div class="box-padding-btm">
                                                <asp:Label ID="Lblemptyalefood" runat="server" CssClass="content-text-box-empty" Text=""> <i class="fa fa-ban"></i> 
                                            <label id="lblbhs_noallergiesfood">No allergies</label>
                                                </asp:Label>
                                                <asp:Repeater runat="server" ID="FoodAllergy">
                                                    <ItemTemplate>
                                                        <li>
                                                            <asp:Label ID="NameLabel" class="font-content-dashboard" runat="server" Text='<%#Eval("allergy") %>' Enabled="false" />
                                                        </li>
                                                    </ItemTemplate>
                                                </asp:Repeater>
                                            </div>
                                        </div>
                                        <div style="padding-top: 5px; padding-left: 10px; display: inline-table; min-width: 20%; max-width: 33%;">
                                            <label id="lblbhs_lain2" class="font-subheader-dashboard sub-header-label" style="font-weight: bold; font-size: 14px;">Others</label>
                                            <br />
                                            <div class="box-padding-btm">
                                                <asp:Label ID="Lblemptyalelain" runat="server" CssClass="content-text-box-empty" Text=""> <i class="fa fa-ban"></i> 
                                            <label id="lblbhs_noallergieslain">No allergies</label>
                                                </asp:Label>
                                                <asp:Repeater runat="server" ID="OtherAllergy">
                                                    <ItemTemplate>
                                                        <li>
                                                            <asp:Label ID="NameLabel" class="font-content-dashboard" runat="server" Text='<%#Eval("allergy") %>' Enabled="false" />
                                                        </li>
                                                    </ItemTemplate>
                                                </asp:Repeater>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>

                    <div id="divisi_routinemed" runat="server" class="col-sm-12 box-margin-btm">
                        <asp:UpdatePanel runat="server">
                            <ContentTemplate>
                                <div class="box-big-filled">
                                    <div id="header-routinemed" class="header-title-filled">
                                        <asp:Image ID="Imageroutinemed" ImageUrl="~/Images/Dashboard/ic_Routine_new.svg" CssClass="title-img-box" runat="server" />
                                        <label id="lbhbhs_routinemedication" class="font-header-dashboard title-text-box" style="color: #0013b5;">Routine Medication</label>
                                    </div>
                                    <div class="row">
                                        <div class="col-sm-12" style="padding-top: 5px; padding-left: 25px;">
                                            <div class="box-padding-btm">
                                                <asp:Label ID="lblemptyroutinemed" runat="server" CssClass="content-text-box-empty" Text="">  <i class="fa fa-ban"></i>  
                                                    <label id="lblbhs_noroutinemedication">No routine medication</label>
                                                </asp:Label>

                                                <div class="row">
                                                    <asp:Repeater runat="server" ID="RepCurrentMedication">
                                                        <ItemTemplate>
                                                            <div class="col-sm-4">
                                                                <i class="mdi mdi-circle-medium"></i>
                                                                <asp:Label ID="NameLabel" class="font-content-dashboard" runat="server" Text='<%#Eval("current_medication") %>' Enabled="false" />
                                                            </div>
                                                        </ItemTemplate>
                                                    </asp:Repeater>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>

                    <div id="divisi_procresult" runat="server" class="col-sm-12 box-margin-btm">
                        <asp:UpdatePanel runat="server">
                            <ContentTemplate>
                                <div class="box-big-filled">
                                    <div id="header-procresult" class="header-title-filled">
                                        <asp:Image ID="Image9" ImageUrl="~/Images/Dashboard/ic_LatestHistory_new.svg" CssClass="title-img-box" runat="server" />
                                        <label id="lblbhs_riwayattindakan" class="font-header-dashboard title-text-box" style="color: #219000;">Procedure Result</label>
                                    </div>
                                    <div class="row">
                                        <div class="col-sm-12">
                                            <div class="box-padding-btm">
                                                <div id="divnotindakan" runat="server" class="content-text-box-empty" style="text-align: left; padding-top: 5px; padding-left: 10px; display: none;">
                                                    <i class="fa fa-ban"></i>
                                                    <label id="lblbhs_notindakan">No Procedure Result </label>
                                                </div>
                                                <div id="divriwayattindakan" runat="server" style="font-size: 14px; text-align: left; padding-top: 0px;">
                                                    <asp:GridView runat="server" ID="gvw_hasiltindakan" AutoGenerateColumns="False" CssClass="table-condensed table-fill-width"
                                                        BorderWidth="0" BorderColor="#b9b9b9">
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="&nbsp;Date / Admission" ItemStyle-Width="12%" HeaderStyle-Font-Size="12px" ItemStyle-VerticalAlign="Top" HeaderStyle-CssClass="font-content-dashboard table-sub-header-label">
                                                                <ItemTemplate>
                                                                    <div style="padding-left: 5px;">
                                                                        <asp:Label Font-Size="12px" class="font-content-dashboard" Font-Names="Helvetica, Arial, sans-serif" ID="item_name" runat="server" Text='<%# Bind("admission") %>'></asp:Label>
                                                                    </div>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Doctor" ItemStyle-Width="12%" HeaderStyle-Font-Size="12px" ItemStyle-VerticalAlign="Top" HeaderStyle-CssClass="font-content-dashboard table-sub-header-label">
                                                                <ItemTemplate>
                                                                    <asp:Label Font-Size="12px" class="font-content-dashboard" Font-Names="Helvetica, Arial, sans-serif" ID="item_name" runat="server" Text='<%# Bind("doctor_name") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Procedure Result" ItemStyle-Width="76%" HeaderStyle-Font-Size="12px" ItemStyle-VerticalAlign="Top" HeaderStyle-CssClass="font-content-dashboard table-sub-header-label">
                                                                <ItemTemplate>
                                                                    <asp:Label Font-Size="12px" class="font-content-dashboard" Font-Names="Helvetica, Arial, sans-serif" ID="item_name" runat="server" Text='<%# Eval("planning_remarks").ToString().Replace("\n", "<br />") %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                    </asp:GridView>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>

                    <div class="col-sm-12 box-margin-btm">
                        <asp:UpdatePanel runat="server">
                            <ContentTemplate>
                                <div class="box-big-filled">
                                    <div id="header-admihis" class="header-title-filled">
                                        <div class="row">
                                            <div class="col-sm-4">
                                                <asp:Image ID="Image11" ImageUrl="~/Images/Dashboard/ic_RiwayatKonsultasi.svg" CssClass="title-img-box" runat="server" />
                                                <label id="lblbhs_admissionhistory" class="font-header-dashboard" style="font-weight: bold; font-size: 20px; color: #e67d05;">Admission History</label>
                                            </div>
                                            <div class="col-sm-4 text-center">
                                                <div class="btn-group" role="group" aria-label="..." style="height: 35px">
                                                    <asp:LinkButton runat="server" Style="margin-top: 3px; margin-right: 10px; padding: 0px; height: 30px; box-shadow: none;" CssClass="btn" onmouseover="this.style.color='#e67d05';" onmouseout="this.style.color='#444444';" Font-Size="23px" ID="btnPrev" OnClick="btnPrev_Click"> <i class="fa fa-arrow-circle-left"></i> </asp:LinkButton>
                                                    <asp:Label runat="server" CssClass="btn btn-default" Style="background-color: transparent; outline-color: transparent; border: 0px; box-shadow: none; cursor: default;" Font-Size="20px" Font-Bold="true" ID="lblYear"></asp:Label>
                                                    <asp:LinkButton runat="server" Style="margin-top: 3px; margin-left: 10px; padding: 0px; height: 30px; box-shadow: none;" CssClass="btn" onmouseover="this.style.color='#e67d05';" onmouseout="this.style.color='#444444';" Font-Size="23px" ID="btnNext" OnClick="btnNext_Click"> <i class="fa fa-arrow-circle-right"></i> </asp:LinkButton>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-sm-12">
                                            <div style="text-align: center;" class="box-padding-btm">
                                                <div id="divContentReport" runat="server"></div>
                                                <asp:Label ID="Labeladmishis" runat="server" Style="padding: 10px;" CssClass="content-text-box-empty" Text=""> <br /> <i class="fa fa-ban"></i>
                                                <label id="lblbhs_noadmissionhistory"> No admission history this year  </label>
                                                <br />
                                                </asp:Label>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </div>
                <!-- ######################################################--- end box isi data ---###################################################### -->

                <div class="modal fade" id="modalAssign" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
                    <div class="modal-dialog">
                        <div class="modal-content" style="border-radius: 7px;">
                            <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <div class="modal-header" style="height: 40px; padding-top: 10px; padding-bottom: 5px">
                                        <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                                        <h4 class="modal-title" style="text-align: left">
                                            <asp:Label ID="Label2" Style="font-family: Helvetica, Arial, sans-serif; font-weight: bold" runat="server">
                                            <label id="lblbhs_admissionlist"> Admission List </label>
                                            </asp:Label></h4>
                                    </div>
                                    <div class="modal-body">
                                        <div id="divAdmissionDetail"></div>
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                </div>

                <a class="item" href="javascript:topFunction();">
                    <div id="myIDtoTop" class="bottomMenuu hidee">
                        <span>
                            <img src="../../Images/Result/ic_Arrow_Top.png" title="go to top" /></span>
                    </div>
                </a>

            </div>


        </div>



        <%-- ################# --%>


        <script type="text/javascript">
            $(document).ready(function () {

                var prm = Sys.WebForms.PageRequestManager.getInstance();
                if (prm != null) {
                    prm.add_endRequest(function (sender, e) {
                        if (sender._postBackSettings.panelsToUpdate != null) {

                        }
                    });
                };
            });

            function Open(content) {
            $('#modalAssign').modal('show');

            var list = content.split("|");
            var div = document.createElement('div');
            var popup = document.getElementById('divAdmissionDetail');

            popup.innerHTML = "";

            //div.innerHTML += "<div id='myPopover" + number + "' class='popover popover-x popover-default'> <div class='arrow'></div> <div class='popover-body popover-content'> TES aja </div></div>";

            for (var i = 0; i < list.length; i++) {
                if (i != 0) {
                    div.innerHTML += "<hr>";
                }

                var data = list[i].split("#");

                //var y = data[0].split(" ");
                //var x = y[0].split("/");
                //var tgl = new Date(x[2], x[1], x[0]);

                //var months = ["", "JAN", "FEB", "MAR","APR", "MAY", "JUN", "JUL", "AUG", "SEP", "OCT", "NOV", "DEC"];

                div.innerHTML += "<div class='btn-group btn-group-justified' role='group' aria-label='...'>";
                if (data[6] == "0") {
                    div.innerHTML += "<div class='btn-group' role='group' style='width:79%'><label style='font-weight:bold'>" + data[5] + "</label><br/><label style='font-size:11px'>" + data[0] + " " + "</label><br/<label style='font-size:11px'>" + data[7] + "</label></div>";
                }
                else {
                    div.innerHTML += "<div class='btn-group' role='group' style='width:79%'><label style='color:green;font-weight:bold'>" + data[5] + "</label><br/><label>" + data[0] + "</label><br/<label style='font-size:11px'>" + data[7] + "</label></div>";
                }
                //if (data[1] != "-") {
                //    div.innerHTML += "<div class='btn-group' role='group' style='width:7%'><a href='javascript:Lab(" + data[1].toString() + ")'><img src='../../Images/Icon/labE.png'/ title='Laboratory' width='30px' height='30px'></a></div>";
                //}
                //else if (data[1] == "-") {
                //    div.innerHTML += "<div class='btn-group' role='group' style='width:7%'><img src='../../Images/Icon/labD.png\' title='Laboratory' width='30px' height='30px'></div>";
                //}
                //if (data[2] != "-") {
                //    div.innerHTML += "<div class='btn-group' role='group' style='width:7%'><a href='javascript:Rad(" + data[2].toString() + ")'><img src='../../Images/Icon/radE.png' title='Radiology' width='30px' height='30px'/></a></div>";
                //}
                //else if (data[2] == "-") {
                //    div.innerHTML += "<div class='btn-group' role='group' style='width:7%'><img src='../../Images/Icon/radD.png\' title='Radiology' width='30px' height='30px'></div>";
                //}
                //if (data[3] != "-") {
                //    div.innerHTML += "<div class='btn-group' role='group' style='width:7%'><a href='javascript:EMR(" + data[8].toString() + ", /" + data[3].toString() + "/, " + data[4].toString() + ")'><img src='../../Images/Icon/ic_HistoryA.svg' title='Patient History' width='30px' height='30px'/></a></div>";
                //}
                //else if (data[3] == "-") {
                //    div.innerHTML += "<div class='btn-group' role='group' style='width:7%'><img src='../../Images/Icon/ic_HistoryN.svg\' title='Patient History' width='30px' height='30px'></div>";
                //}
                //div.innerHTML += "<div class='btn-group' role='group' style='width:7%'><img src='../../Images/Dashboard/ic_History.png\' width='30px' height='30px'></div>";
                div.innerHTML += "</div></div>";
            }
            popup.appendChild(div);

            //document.getElementById("mybtn" + number).click();
            return true;
        }
        </script>

    </form>
</body>

</html>



