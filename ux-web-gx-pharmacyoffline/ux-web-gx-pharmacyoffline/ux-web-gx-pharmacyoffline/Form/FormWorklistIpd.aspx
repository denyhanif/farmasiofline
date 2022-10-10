<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FormWorklistIpd.aspx.cs" Inherits="Form_FormWorklistIpd" %>


<!DOCTYPE html>

<html lang="en" style="overflow: hidden">
<head id="Head1" runat="server">
    <title>Siloam Pharmacy</title>

    <!-- Tell the browser to be responsive to screen width -->
    <meta content="width=device-width, initial-scale=1, maximum-scale=1, user-scalable=no" name="viewport" />
    <!-- Bootstrap 3.3.6 -->
    <link rel="stylesheet" href="~/Content/bootstrap/css/bootstrap.css" />
    <!-- Font Awesome -->
    <link rel="stylesheet" href="~/Content/font-awesome/css/font-awesome.css" />
    <!-- Font icomoon -->
    <link rel="stylesheet" href="~/Content/font-awesome/css/style.css" />
    <!-- Datepicker -->
    <link rel="stylesheet" href="~/Content/plugins/datepicker/datepicker3.css" />
    <!-- DropDown -->
    <link rel="stylesheet" href="~/Content/plugins/select2/select2.min.css" />

    <!-- Select Bootstrap -->
    <link rel="stylesheet" href="~/Content/bootstrap-select/css/bootstrap-select.css" />
    <!-- EMR-PharmacyOffline -->
    <link rel="stylesheet" href="~/Content/EMR-PharmacyOffline.css" />
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

    <%--<asp:ContentPlaceHolder ID="headContent" runat="server" />--%>
</head>
    <body id="bodysidebar" class="hold-transition skin-blue-light sidebar-mini sidebar-collapse" style="padding-top: 0px; width: 100%; min-height: 600px; margin-top: 0px; font-family: ro; vertical-align: top;">
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
            <Scripts>
                <asp:ScriptReference Path="~/Content/plugins/jQuery/jQuery-2.2.0.min.js" />
                <asp:ScriptReference Path="~/Content/bootstrap/js/bootstrap.min.js" />
                <asp:ScriptReference Path="~/Content/plugins/jasny-bootstrap/js/jasny-bootstrap.min.js" />
                <asp:ScriptReference Path="~/Content/plugins/datepicker/bootstrap-datepicker.js" />
                <asp:ScriptReference Path="~/Content/plugins/select2/select2.full.min.js" />
                <asp:ScriptReference Path="~/Content/dist/js/app.min.js" />
                <%--<asp:ScriptReference Path="~/Scripts/site.js" />--%>
                <asp:ScriptReference Path="~/Content/bootstrap-select/js/bootstrap-select.js" />

            </Scripts>
        </asp:ScriptManager>
        <script>
        //var connection = new WebSocket('ws://10.83.254.38:1337');

        //connection.onmessage = function (message) {
        //        try {
        //            var json = JSON.parse(message.data);
        //            if (json.type === 'sudahpulang') {
        //                toastr.success(json.messagenotif, 'Success');
        //                toastr.options.positionClass = "toast-top-right";
        //            }
                                
        //        } catch (e) {
        //            console.log('This doesn\'t look like a valid JSON: ', message.data);
        //            return;
        //        }
                            
        //};

        //connection.onerror = function (error) {
        //        // just in there were some problems with conenction...
        //        console.log('Sorry, but there\'s some problem with your connection or the server is down.')
                
        //    };
        
        function clearTxtSearchDatePlan() {
            document.getElementById("<%= textboxdateto.ClientID %>").value = "";
            return true;
        }

        function clearTxtSearchDateProcess() {
            document.getElementById("<%= txtdateprocess.ClientID %>").value = "";
            return true;
        } 

        function clearTxtSearchDateDone() {
            document.getElementById("<%= txtdatedoneto.ClientID %>").value = "";
            document.getElementById("<%= btnSearchDatePatientDone.ClientID %>").click();
            return true;
        }
       
        function clearTxtFind() {
            document.getElementById("<%= txtFindPatient.ClientID %>").value = "";
            
            return true;
        }

        <%--function searchPatientPlan(e) {
            var KeyID = (window.event) ? event.keyCode : e.keyCode;
            if (KeyID == 13) {
                document.getElementById("<%= btnFindPatientPlan.ClientID %>").click();
                return false;
            }
            return true;
        }--%>

        <%--function showButtonClearTextPatientPlan(e) {
            var KeyID = (window.event) ? event.keyCode : e.keyCode;
            if (document.getElementById("<%= txtFindPatient.ClientID %>").value == "") {
                document.getElementById("<%= btnClearTextFindPatientPlan.ClientID %>").style.display = 'none';
            } else {
                document.getElementById("<%= btnClearTextFindPatientPlan.ClientID %>").style.display = '';
            }
        }--%>

        function clearTxtFindProcess() {
            document.getElementById("<%= txtsearchprocess.ClientID %>").value = "";
            //document.getElementById("<%= btnFindPatientProcess.ClientID %>").click();
            return true;
        }
        
        <%--function searchPatientProcess(e) {
            var KeyID = (window.event) ? event.keyCode : e.keyCode;
            if (KeyID == 13) {
                document.getElementById("<%= btnFindPatientProcess.ClientID %>").click();
                return false;
            }
            return true;
        }--%>

        <%--function showButtonClearTextPatientProcess(e) {
            var KeyID = (window.event) ? event.keyCode : e.keyCode;
            if (document.getElementById("<%= txtsearchprocess.ClientID %>").value == "") {
                document.getElementById("<%= btnClearTextFindPatientProcess.ClientID %>").style.display = 'none';
            } else {
                document.getElementById("<%= btnClearTextFindPatientProcess.ClientID %>").style.display = '';
            }
        }--%>

        function clearTxtFindDone() {
            document.getElementById("<%= txtsearchprocess.ClientID %>").value = "";
            document.getElementById("<%= btnFindPatientDone.ClientID %>").click();
            return true;
        }
        
        <%--function searchPatientDone(e) {
            var KeyID = (window.event) ? event.keyCode : e.keyCode;
            if (KeyID == 13) {
                document.getElementById("<%= btnFindPatientDone.ClientID %>").click();
                return false;
            }
            return true;
        }--%>

        <%--function showButtonClearTextPatientDone(e) {
            var KeyID = (window.event) ? event.keyCode : e.keyCode;
            if (document.getElementById("<%= txtsearchdone.ClientID %>").value == "") {
                document.getElementById("<%= btnClearTextFindPatientDone.ClientID %>").style.display = 'none';
            } else {
                document.getElementById("<%= btnClearTextFindPatientDone.ClientID %>").style.display = '';
            }
        }--%>

        function SendNotificationSudahPulang() {
            var wardid = document.getElementById('<%= WardIDNotification.ClientID %>').value;
            var messagenotif = document.getElementById('<%= MessageNotification.ClientID %>').value;
            var json = JSON.stringify({ type: 'sudahpulang', ward: wardid, messagenotif: messagenotif});

            //for (var i = 0; i < 1000; i++)
            //{
            //    var json = JSON.stringify({ type: 'sudahpulang', ward: wardid, messagenotif: messagenotif + i});
            //    connection.send(json);
            //}
            connection.send(json);

        }

        function SendNotificationBelumPulang() {
            var wardid = document.getElementById('<%= WardIDNotification.ClientID %>').value;
            var messagenotif = document.getElementById('<%= MessageBelumPulang.ClientID %>').value;
            
            var json = JSON.stringify({ type: 'belumpulang', ward: wardid, messagenotif: messagenotif });
            
            connection.send(json);
        }
        

        function dateto() {
            var dpdateto = $('#<%=textboxdateto.ClientID%>');
            dpdateto.datepicker({
                changeMonth: true,
                changeYear: true,
                format: "dd M yyyy",
                language: "tr",
                startDate: '+0d',
                todayHighlight: true
            }).on('changeDate', function (ev) {
                $(this).blur();
                $(this).datepicker('hide');
            });
        }

        function datedoneto() {
            var dpdateto = $('#<%=txtdatedoneto.ClientID%>');
            dpdateto.datepicker({
                changeMonth: true,
                changeYear: true,
                format: "dd M yyyy",
                language: "tr",
                endDate: '+0d',
                todayHighlight: true
            }).on('changeDate', function (ev) {
                $(this).blur();
                $(this).datepicker('hide');
            });
        }

        function datetoprocess() {
            var dpdateto = $('#<%=txtdateprocess.ClientID%>');
            dpdateto.datepicker({
                changeMonth: true,
                changeYear: true,
                format: "dd M yyyy",
                language: "tr",
                todayHighlight: true
            }).on('changeDate', function (ev) {
                $(this).blur();
                $(this).datepicker('hide');
            });
        }

        function openModalDetail() {
            $('#modalDetail').modal('show');
            return true;
        }

        function openLabModal() {
            $('#laboratoryResult').modal('show');
        }

        function topFunction() {
          document.body.scrollTop = 0;
          document.documentElement.scrollTop = 0;
        }
        

        $(window.document).scroll(function (e) {
            if ($(this).scrollTop() > 250) {
                $("#btnGoToTopShow").attr('class', 'bottomMenuu showw');
            } else {
                $("#btnGoToTopShow").attr('class', 'bottomMenuu hidee');
            }
        });

            $(document).ready(function () {

                $('.selecpicker').selectpicker();
                var prm = Sys.WebForms.PageRequestManager.getInstance();
                if (prm != null) {
                    prm.add_endRequest(function (sender, e) {
                        if (sender._postBackSettings.panelsToUpdate != null) {
                            $('.selecpicker').selectpicker();
                        }
                    });
                };
            });



    </script>

        <style>
        

        .btnFilter {
            width: 140px;
            height:50px;
            background-color: white;
            border-radius: 8px;
            margin-top: 8px;
            padding-top:2px;
        }

        .btnClickCSS{
            height: 66px;
            width: 160px;
            padding-top: 5px;
            background-color: white;
            border-radius: 10px;
            font-weight: bold;
            padding-top:7px;
        }

        .bordertable {
            border-right:solid 1px #D6DBFF;
        }

        .tooltipnotes+ .tooltip > .tooltip-innerNo{
            max-width:400px;
            background-color:#FFFDCE;
            color:#000;
            padding:10px;
            margin-left:10px;
            box-shadow:rgba(0, 0, 0, 0.16) 3px 6px;
        }

        .tooltip.in{opacity:1!important;}

        ::-webkit-input-placeholder { /* Safari, Chrome and Opera */
          color: lightgray;
        }

        .textboxempty{
            height: 32px;
            font-size: 12px;
            background-color: white;
            border: solid 1px #cdced9;
            padding-left:10px;
        }

        .shadowtable {
            box-shadow:0px 3px 8px rgba(0, 0, 0, 0.08)
        }

        .btn-group > .btn:first-child {
            margin-left: 0;
            background-color: white;
            height: 32px;
            font-size:12px;
        }

        .linkhover{
            color:#999999;
        }

        .linkhover:hover{
            color:#337ab7;
        }

        .txtdate{
            border: none;
            text-shadow: 0 0 0 #000;
            color: transparent;
            height: 25px;
            cursor: pointer;
            font-size: 12px;
            width: 120px;
            background-color:transparent
        }

        .txtdate:focus{
            outline: none;
        }

        .dropdown-menu{
            font-size:12px;
        }
        
        .margin-detail{
            margin-left: 0px;
            margin-right: 0px;
            margin-top: 20px;
        }

        .margin-detailcontent{
            margin-left: 0px;
            margin-right: 0px;
            margin-top: 10px;
        }

        .lbldetailfooter{
            color:#AAB3B9;
            font-size:12px;
            font-weight:bold;
        }

        .table-hover-worklist > tbody > tr > th {
            background-color: white;
            color: #1A2269;
            vertical-align: middle;
        }

        .textboxempty{
            height: 32px !important;
            font-size: 12px;
            background-color: white;
            border: solid 1px #cdced9;
            padding-left:10px;
        }

        ::-webkit-input-placeholder { /* Safari, Chrome and Opera */
          color: lightgray;
        }

        .datepicker table tr td.disabled {
            background: lightgrey;
            color: #444;
            cursor: not-allowed;
        }
        .datepicker table tr td.today{
            color: #000000;
            background: #ffb733;
            border-color: #ffb733;
        }

        .datepicker table tr td.disabled:hover:hover {
            background: lightgrey;
            color: #444;
            cursor: not-allowed;
        }

        .datepicker table tr td.today:hover:hover {
            background: #285e8e;
            color:white;
        }

        .datepicker table tr td.active.active {
                color: #ffffff;
                background: #285e8e;
                border-color: #285e8e;
        }
        .datepicker table tr td.day:hover{
            background: #285e8e;
            color:white;
        }

        .labelakanadatang{
            width: 136px;
            height: 30px;
            background: #6A6492 0% 0% no-repeat padding-box;
            border-radius: 0px 16px 0px 0px;
            color:white;
            text-align:center;
            padding-top:7px;
        }

        .table{
            margin-bottom:0px;
        }

        .bottomMenuu {
            position: fixed;
            top: 85%;
            text-align: left;
            z-index: 20;
        }

        .hidee {
            opacity: 0;
            right: -100px;
            transition: all 0.5s;
        }

        .showw {
            opacity: 1;
            right: 0;
            transition: all 0.5s;
        }


        .btnTop{
            opacity: 0.3 ;
            
        }
        .btnTop:hover{
            
            opacity: 1;
        }
        .Font{
            font-family: Arial, Helvetica, sans-serif;
            background-color: rgb(221, 221, 221);
            min-height:640px;
        }


        .table-hover-worklist > tbody > tr:hover > td{
            background-color: #d6dbff;
        }

        .table-hover-worklist > tbody > tr:hover > th {
            background-color: white;
        }
        
    </style>

        
    <div class="Font" >

    
    <div class="container-fluid">
        <asp:UpdatePanel runat="server" ID="upError">
            <ContentTemplate>
                <asp:HiddenField runat="server" ID="jsonworklist" />
                <asp:Button runat="server" ID="refreshSocket" CssClass="hidden" OnClick="refreshSocket_onClick" />
                <asp:HiddenField runat="server" ID="WardIDNotification" />
                <asp:HiddenField runat="server" ID="MessageNotification" />
                <asp:HiddenField runat="server" ID="MessageBelumPulang" />
                <asp:HiddenField runat="server" ID="hfName" />
                <asp:HiddenField runat="server" ID="hfOrganizationId" />
                <%--<div class="supreme-container">--%>
                
                        <div class="row" style="padding-top:10px">
                            <div class="col-sm-12">
                                <asp:HiddenField runat="server" ID="hfguidadditional" />
                                <asp:LinkButton runat="server" ID="BtnGetAll" OnClick="LnkPlan_onClick">
                                    <div id="divplan" runat="server" class="col-sm-2 btnFilter" style="display: inline; margin-left: 5px; background-color: white">
                                        <div class="row">
                                            <div class="col-sm-4" style="height:50px;text-align:center;padding-right:0px">
                                                <asp:Label Style="font-size:35px;font-weight:normal;" runat="server" ID="patientotal"></asp:Label></td>
                                            </div>
                                            <div class="col-sm-6" style="height:50px;padding-left:20px;padding-right:0px;padding-top:2px;text-align:left">
                                                <span style="font-size:14px">Rencana</span><br />
                                                <span style="font-size:14px">Pulang</span>
                                            </div>
                                        </div>
                                    </div>
                                </asp:LinkButton>
                        
                                <div class="col-sm-1" style="width:2%;padding-left:6px;padding-top:23px">
                                    <img  style="background-color: transparent; vertical-align: middle" src="<%= Page.ResolveClientUrl("~/Images/Icon/arrow_next.svg") %>" />
                                </div>

                                <asp:LinkButton runat="server" ID="BtnGetProcess" OnClick="LnkProcess_onClick">
                                    <div id="divprocess" runat="server" class="col-sm-2 btnFilter" style="display: inline; margin-left: 5px; background-color: white">
                                        <div class="row">
                                            <div class="col-sm-4" style="height:50px;text-align:center;padding-right:0px">
                                                <asp:Label Style="font-size:35px; font-weight:normal;" runat="server" ID="processtotal"></asp:Label></td>
                                            </div>
                                            <div class="col-sm-6" style="height:50px;padding-left:20px;padding-right:0px;padding-top:2px;text-align:left">
                                                <span style="font-size:14px">Sedang</span><br />
                                                <span style="font-size:14px">Diproses</span>
                                            </div>
                                        </div>
                                    </div>
                                </asp:LinkButton>

                                <div class="col-sm-1" style="width:2%;padding-left:6px;padding-top:23px">
                                    <img  style="background-color: transparent; vertical-align: middle" src="<%= Page.ResolveClientUrl("~/Images/Icon/arrow_next.svg") %>" />
                                </div>
                                <asp:LinkButton runat="server" ID="BtnGetDone" OnClick="LnkDone_onClick" >
                                    <div id="divdone" runat="server" class="col-sm-2 btnFilter" style="display: inline; margin-left: 8px; background-color: white">
                                        <div class="row">
                                            <div class="col-sm-4" style="height:50px;text-align:center;padding-right:0px">
                                                <asp:Label Style="font-size:35px;font-weight:normal;" runat="server" ID="donetotal"></asp:Label></td>
                                            </div>
                                            <div class="col-sm-6" style="height:50px;padding-left:20px;padding-right:0px;padding-top:2px;text-align:left">
                                                <span style="font-size:14px">Sudah</span><br />
                                                <span style="font-size:14px">Billing</span>
                                            </div>
                                        </div>
                                    </div>
                                </asp:LinkButton>

                            </div>
                        </div>
                
                <%--</div>--%>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>

    <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="upError">
        <ProgressTemplate>
            <div class="modal-backdrop" style="background-color: white; z-index:5; opacity: 0.6; text-align: center">
            </div>
            <div style="background-color: transparent; text-align: center; z-index: 5; position: fixed; width: 100%; left: 0px; height: calc(100vh - 200px); border-radius: 6px;">
                <div style="margin-top: 100px;">
                    <img alt="" height="225px" width="225px" style="vertical-align: middle; background: rgba(42,53,147,80%); z-index: 10;border-radius: 50%;padding-top: 10px;padding-right: 0px;padding-left: 0px;padding-bottom: 0px;box-shadow: 5px 5px 10px #bbb;" src="<%= Page.ResolveClientUrl("~/Images/Background/heartbeat-white.svg") %>" />
                </div>
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>

    <asp:UpdateProgress ID="uProgWorklist" runat="server" AssociatedUpdatePanelID="upDischargePlan">
        <ProgressTemplate>
            <div class="modal-backdrop" style="background-color: white; z-index:5; opacity: 0.6; text-align: center">
            </div>
            <div style="background-color: transparent; text-align: center; z-index: 5; position: fixed; width: 100%; left: 0px; height: calc(100vh - 200px); border-radius: 6px;">
                <div style="margin-top: 100px;">
                    <img alt="" height="225px" width="225px" style="vertical-align: middle; background: rgba(42,53,147,80%); z-index: 10;border-radius: 50%;padding-top: 10px;padding-right: 0px;padding-left: 0px;padding-bottom: 0px;box-shadow: 5px 5px 10px #bbb;" src="<%= Page.ResolveClientUrl("~/Images/Background/heartbeat-white.svg") %>" />
                </div>
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>


    <asp:UpdateProgress ID="UpdateProgress3" runat="server" AssociatedUpdatePanelID="upSedangProcess">
        <ProgressTemplate>
            <div class="modal-backdrop" style="background-color: white; z-index:5; opacity: 0.6; text-align: center">
            </div>
            <div style="background-color: transparent; text-align: center; z-index: 5; position: fixed; width: 100%; left: 0px; height: calc(100vh - 200px); border-radius: 6px;">
                <div style="margin-top: 100px;">
                    <img alt="" height="225px" width="225px" style="vertical-align: middle; background: rgba(42,53,147,80%); z-index: 10;border-radius: 50%;padding-top: 10px;padding-right: 0px;padding-left: 0px;padding-bottom: 0px;box-shadow: 5px 5px 10px #bbb;" src="<%= Page.ResolveClientUrl("~/Images/Background/heartbeat-white.svg") %>" />
                </div>
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>


    <asp:UpdateProgress ID="UpdateProgress5" runat="server" AssociatedUpdatePanelID="upSudahBilling">
        <ProgressTemplate>
            <div class="modal-backdrop" style="background-color: white; z-index:5; opacity: 0.6; text-align: center">
            </div>
            <div style="background-color: transparent; text-align: center; z-index: 5; position: fixed; width: 100%; left: 0px; height: calc(100vh - 200px); border-radius: 6px;">
                <div style="margin-top: 100px;">
                    <img alt="" height="225px" width="225px" style="vertical-align: middle; background: rgba(42,53,147,80%); z-index: 10;border-radius: 50%;padding-top: 10px;padding-right: 0px;padding-left: 0px;padding-bottom: 0px;box-shadow: 5px 5px 10px #bbb;" src="<%= Page.ResolveClientUrl("~/Images/Background/heartbeat-white.svg") %>" />
                </div>
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>

    <asp:UpdatePanel runat="server" ID="upDischargePlan">
        <ContentTemplate>
            <div class="supreme-container" id="divDischargePlan" runat="server">
                <div class="row" style="padding-top: 10px">
                    <div class="col-lg-12">
                        <div class="modal-dialog mini-dialog" style="background-color:#EEEEEE">
                            <div class="modal-header mini-header" style="background-color: #EEEEEE; padding-top:8px; border-bottom: none;">
                                <div class="col-lg-4" style="padding-left: 0px; padding-right:0px;">
                                    <asp:HiddenField runat="server" ID="flagdischargeplan" />
                                    <asp:Label ID="lblheaderPlan" Font-Size="25px" runat="server" Text="Rencana Pulang"></asp:Label>
                                    <asp:Label runat="server" ID="lblCountPlan" Font-Size="25px" Font-Bold="false" Text="'0"></asp:Label>
                                    <br />
                                    <asp:LinkButton runat="server" CssClass="linkhover" ID="LinkButton2" Style="cursor:pointer;" OnClick="BtnRefreshPlan_OnClick" ><i class="fa fa-refresh"></i>
                                        <label style="cursor:pointer">Terakhir Diperbarui: </label>
                                        <asp:Label runat="server" ID="lblDateRefreshWorklist"></asp:Label>
                                    </asp:LinkButton>
                                    <div class="has-feedback" style="display: none;">
                                        <span style="margin-top: -9px;" class="fa fa-search form-control-feedback"></span>
                                        <asp:TextBox runat="server" ID="textboxdateto2" CssClass="txtdate" Style="border:none" name="date" placeholder="dd/mm/yyyy" onMouseDown="dateto();" AutoPostBack="true" OnTextChanged="DatePlan_OnChange"  />
                                    </div>
                                </div>

                                <div class="col-lg-8" style="margin-top:13px; text-align:right;padding-left:8%">
                                    <table border="0">
                                        <tr>
                                            <td style="width:20%"><label style="vertical-align:middle; font-size:12px;">FILTER: </label></td>
                                            <td style="width:20%"><asp:DropDownList Style="background-color:white" Font-Size="12px" Font-Names="Helvetica" AutoPostBack="true" ID="ddlWardlist" runat="server" OnSelectedIndexChanged="DDLWardList_onSelectedIndexChanged" CssClass="selecpicker greyItem" data-live-search="true" data-size="7" data-width="132px" data-height="12px" data-dropup-auto="false"></asp:DropDownList></td>
                                            <td style="width:20%">
                                                <div class="has-feedback" style="display: inline;">
                                                    <asp:TextBox AutoCompleteType="Disabled" runat="server" placeholder="Tanggal" CssClass="underlined textboxempty" Style="outline-color: transparent; width:100%; resize: none" Font-Names="Helvetica, Arial, sans-serif" ID="textboxdateto" onMouseDown="dateto();" AutoPostBack="true" OnTextChanged="SearchPlan_onChange" />
                                                    <span  style="margin-top: -8px; margin-right: 5px; margin-left:-20px;">
                                                        <asp:LinkButton runat="server" ID="btnClearDatePlan" OnClientClick="return clearTxtSearchDatePlan();" OnClick="clearsearchplan_onclick"  >
                                                            <i class="fa fa-times-circle"></i>
                                                        </asp:LinkButton>
                                                    </span>
                                                    <span id="btnShowCalendarPlan" visible="false" runat="server" style="margin-top: -9px; color: #AAB3B9" class="fa fa-calendar form-control-feedback"></span>
                                                    <asp:UpdatePanel runat="server" ID="UpdatePanel5">
                                                        <ContentTemplate>
                                                            <asp:Button Style="display: none;" ID="btnSearchDatePatientPlan"  runat="server" Text="" />
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                </div>
                                            </td>
                                            <td style="width:20%">
                                                <asp:Label ID="Label2" Style="font-size:12px;" runat="server" Text="MR/NAMA PASIEN "></asp:Label>
                                            </td>
                                            <td style="width:20%">
                                                <div class="has-feedback" style="display: inline;">
                                                    <asp:TextBox runat="server" AutoComplete="Off" AutoCompleteType="Disabled" placeholder="Cari" CssClass="underlined textboxempty" Style="outline-color: transparent; max-width: 100%; width: 100%; resize: none" Font-Names="Helvetica, Arial, sans-serif" ID="txtFindPatient" AutoPostBack="true" OnTextChanged="SearchPlan_onChange"  />
                                                    <span style="margin-top: -9px; margin-left:-32px; ">
                                                        <asp:LinkButton Visible="false" ID="btnClearTextFindPatientPlan" runat="server" OnClientClick="clearTxtFind();">
                                                            <i class="fa fa-times-circle"></i>
                                                        </asp:LinkButton>
                                                    </span>
                                                    <span style="margin-top: -9px; color: #AAB3B9" class="fa fa-search"></span>
                                                    <asp:UpdatePanel runat="server" ID="UpdatePanel9">
                                                        <ContentTemplate>
                                                            <asp:Button Style="display: none;" ID="btnFindPatientPlan"  runat="server" Text="" OnClick="SearchPlan_onChange" />
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                </div>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </div>

                            <asp:Repeater runat="server" ID="rptDischargePlan" OnItemDataBound="rptDischargePlan_onItemBound">
                                <ItemTemplate>
                                    <asp:HiddenField runat="server" ID="hdnWardId" Value='<%# Bind("WardId") %>' />
                                    <div style="background-color:#107798;margin-left:15px;margin-right:15px;padding:8px;font-weight:bold;border-radius:5px 5px 0px 0px;box-shadow:0px 3px 8px rgba(0, 0, 0, 0.08); position: sticky; top: 4px; z-index: 4;">
                                        <asp:Label runat="server" ID="LblHeader" Style="color:white;font-size:18px;"></asp:Label><asp:Label runat="server" Style="color:white;font-size:18px;font-weight:normal;" ID="lblcountward"></asp:Label>
                                    </div>
                                    <div class="modal-body" style="padding-top: 0px; padding-bottom: 10px" id="Div1" runat="server">
                                        <asp:GridView ID="gvLIstNewDischarge" runat="server" CssClass="table table-hover-worklist shadowtable" BorderColor="Transparent" AutoGenerateColumns="false"
                                            AllowPagging="true" PageSize="8" ShowHeaderWhenEmpty="True" EmptyDataText="No Data" BorderWidth="0" OnRowDataBound="gvLIstNewDischarge_DataBound">
                                            <Columns>
                                                <asp:TemplateField HeaderText="Nama Pasien" HeaderStyle-Font-Size="12px" HeaderStyle-Width="14%" ItemStyle-Font-Size="14px" ItemStyle-Width="14%" HeaderStyle-HorizontalAlign="Left"  ItemStyle-HorizontalAlign="Left" >
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="patientName"  CssClass="tooltipnotes" data-toggle="tooltip" data-html="true" data-placement="auto bottom" title='<%# Eval("AdditionalNotes").ToString().Replace("\n","</br>") %>' Style="color:#2A3593;font-weight:bold;text-decoration:underline;" runat="server"  Visible='<%# Eval("isPrimary").ToString().ToLower() == "true" %>' Text='<%# Bind("PatientName") %>' OnClick="LnkPatientDetailPlan_onClick"></asp:LinkButton>
                                                        <asp:Image Width="50px" Height="20px" ImageUrl="~/Images/Icon/label_new.svg" runat="server" Visible='<%# Eval("isNew").ToString() != "False" && Eval("isPrimary").ToString().ToLower() == "true" %>' />
                                                        <asp:Image Style="margin-bottom: 3px;" Height="16px" ImageUrl="~/Images/Icon/icon_notes.svg" runat="server" Visible='<%# Eval("isPrimary").ToString().ToLower() == "true" && Eval("AdditionalNotes").ToString() != ""  %>' />
                                                        <asp:HiddenField ID="lbAdmissionId" runat="server" Value='<%# Bind("admissionId") %>'></asp:HiddenField>
                                                        <asp:HiddenField ID="lbWorklistId" runat="server" Value='<%# Bind("worklistId") %>'></asp:HiddenField>
                                                        <asp:HiddenField ID="lbDoctorId" runat="server" Value='<%# Bind("doctorId") %>'></asp:HiddenField>
                                                        <asp:HiddenField ID="lbPatientId" runat="server" Value='<%# Bind("patientId") %>'></asp:HiddenField>
                                                        <asp:HiddenField ID="hfwaitStatus" runat="server" Value='<%# Bind("waitStatus") %>'></asp:HiddenField>
                                                        <asp:HiddenField ID="hfisPrescription" runat="server" Value='<%# Bind("isPrescription") %>'></asp:HiddenField>
                                                        <asp:HiddenField ID="hfisRetur" runat="server" Value='<%# Bind("isRetur") %>'></asp:HiddenField>
                                                        <asp:HiddenField ID="hflmaStatus" runat="server" Value='<%# Bind("lmaStatus") %>'></asp:HiddenField>
                                                        <asp:HiddenField ID="hfresumeStatus" runat="server" Value='<%# Bind("resumeStatus") %>'></asp:HiddenField>
                                                        <asp:HiddenField ID="ProcessId" runat="server" Value ='<%# Bind("ProcessId") %>'></asp:HiddenField>
                                                        <asp:HiddenField ID="AdditionalNotes" runat="server" Value ='<%# Bind("AdditionalNotes") %>'></asp:HiddenField>
                                                        <asp:HiddenField ID="ModifiedDate" runat="server" Value ='<%# Bind("ModifiedDate") %>'></asp:HiddenField>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="JK" HeaderStyle-CssClass="headercenter" HeaderStyle-Font-Size="12px" ItemStyle-Font-Size="14px" HeaderStyle-Width="3%"  ItemStyle-Width="3%" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" >
                                                    <ItemTemplate>
                                                        <asp:Label ID="Sex" runat="server" Font-Bold="true"  Visible='<%# Eval("isPrimary").ToString().ToLower() == "true" %>' ><span Style="color:<%# Eval("gender").ToString().ToLower() == "f" ? "#E51B94" : "#1B29E5" %>"><%# Eval("gender") %></span></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="No. Mr" HeaderStyle-Font-Size="12px" HeaderStyle-Width="5%" ItemStyle-Width="5%" ItemStyle-Font-Size="14px" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" >
                                                    <ItemTemplate>
                                                        <asp:Label ID="NoMR" runat="server" Visible='<%# Eval("isPrimary").ToString().ToLower() == "true" %>' Text='<%# Bind("localMrNo") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Rencana Plg" HeaderStyle-Font-Size="12px" ItemStyle-Font-Size="14px" HeaderStyle-Width="7.8%" ItemStyle-Width="7.8%" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" >
                                                    <ItemTemplate>
                                                        <asp:Label ID="umur" runat="server" Visible='<%# Eval("isPrimary").ToString().ToLower() == "true" %>' Text='<%# Bind("WorklistDate","{0:dd MMM yyyy}") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Kmr" HeaderStyle-Font-Size="12px" ItemStyle-Font-Size="14px" HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="4%" ItemStyle-Width="4%" ItemStyle-HorizontalAlign="Left">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Kmr" runat="server" Visible='<%# Eval("isPrimary").ToString().ToLower() == "true" %>' Text='<%# Bind("roomNo") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField HeaderText="Dokter" HeaderStyle-Font-Size="12px" ItemStyle-Font-Size="14px" HeaderStyle-Width="13.57%" ItemStyle-Width="13.57%" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" DataField="doctorName"></asp:BoundField>
                                                <%--<asp:BoundField HeaderText="Penanggung" ItemStyle-Width="17%" ItemStyle-HorizontalAlign="Left" DataField="payerName"></asp:BoundField>--%>
                                                <asp:TemplateField HeaderText="Penanggung" HeaderStyle-Font-Size="12px" ItemStyle-Font-Size="14px" HeaderStyle-Width="15.6%" ItemStyle-Width="15.6%" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" >
                                                    <ItemStyle CssClass="bordertable" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="payerName" ItemStyle-Font-Size="14px" Visible='<%# Eval("isPrimary").ToString().ToLower() == "true" %>' runat="server"  Text='<%# Bind("payerName") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Visit" HeaderStyle-Font-Size="12px"  ItemStyle-Font-Size="14px" HeaderStyle-Width="4.2%" ItemStyle-Width="4.2%" HeaderStyle-CssClass="headercenter" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" >
                                                    <HeaderStyle CssClass="" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="visit" runat="server" Font-Bold="true" Font-Size="13px" Text='<%# Eval("waitStatus").ToString() != "TD" && Eval("waitStatus").ToString() != "TU" ? "TUTD" : "" %>'></asp:Label>
                                                        <asp:Image ID="chkTrueVisitPlan" Style="text-align: center" Width="20px" Height="20px" ImageUrl="~/Images/Icon/icon_checked_locked.svg" runat="server" Visible='<%# Eval("visitValue").ToString().ToLower() == "done" && Eval("WaitStatus").ToString() == "TD" %>'/>
                                                        <%--<asp:ImageButton ID="chkFalseVisitPlan" Width="20px" Height="20px" ImageUrl="~/Images/Icon/icon_0_Unchecked.svg" runat="server" Visible='<%# Eval("visitValue").ToString() == "PENDING" %>' />--%>
                                                        <%--<i class="fa fa-check fa-lg" style="color: #4BDF1D; display: <%# Eval("visitValue").ToString().ToLower() == "done" && Eval("WaitStatus").ToString() == "TD" ? "normal" : "none" %>"></i>--%>
                                                        <asp:Image  ImageUrl="~/Images/Icon/0_check_result.svg" runat="server" Visible='<%# Eval("visitValue").ToString().ToLower() == "done" && Eval("WaitStatus").ToString() == "TU" %>' CssClass="tooltipnotes" data-toggle="tooltip" data-html="true" data-placement="auto bottom" title='<%# Eval("Remarks").ToString().Replace("\n","</br>") %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Resep" HeaderStyle-Font-Size="12px" ItemStyle-Font-Size="14px" HeaderStyle-Width="5%" ItemStyle-Width="5%" HeaderStyle-CssClass="headercenter" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Center" >
                                                    <ItemTemplate>
                                                        <asp:Label ID="prescription" runat="server" Visible='<%# Eval("isPrescription").ToString().ToLower() == "false" %>' Text='<%# Eval("isPrescription").ToString().ToLower() == "false" ? "-" : "" %>'></asp:Label>
                                                        <asp:Image ID="chkTruePrescription" Style="text-align: center" Width="20px" Height="20px" ImageUrl="~/Images/Icon/icon_checked_locked.svg" runat="server" Visible='<%# Eval("prescriptionValue").ToString().ToLower() == "done" %>' />
                                                        <%--<asp:ImageButton ID="chkFalsePrescription" Width="20px" Height="20px" ImageUrl="~/Images/Icon/icon_0_Unchecked.svg" runat="server" Visible='<%#Eval("prescriptionValue").ToString() == "PENDING" %>'  />--%>
                                                        <%--<i class="fa fa-check fa-lg" style="color: #4BDF1D; display: <%# Eval("prescriptionValue").ToString().ToLower() == "done" ? "normal" : "none" %>"></i>--%>
                                                        
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Retur" HeaderStyle-Font-Size="12px" ItemStyle-Font-Size="14px" HeaderStyle-Width="5%" ItemStyle-Width="5%" HeaderStyle-CssClass="headercenter" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Center" >
                                                    <ItemTemplate>
                                                        <asp:Label ID="retur" runat="server" Visible='<%# Eval("isRetur").ToString().ToLower() == "false" %>' Text='<%# Eval("isRetur").ToString().ToLower() == "false" ? "-" : "" %>'></asp:Label>
                                                        <asp:Image ID="chkTrueReturPlan" Style="text-align: center" Width="20px" Height="20px" ImageUrl="~/Images/Icon/icon_checked_locked.svg" runat="server" Visible='<%# Eval("returValue").ToString() == "DONE" %>' />
                                                        <asp:ImageButton ID="chkFalseReturPlan" Width="20px" Height="20px" ImageUrl="~/Images/Icon/icon_unchecked.svg" runat="server" Visible='<%#Eval("returValue").ToString() == "PENDING" %>' />
                                                        <%--<i class="fa fa-check fa-lg" style="color: #4BDF1D; display: <%# Eval("returValue").ToString().ToLower() == "done" ? "normal" : "none" %>"></i>--%>
                                                        
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="LMA" HeaderStyle-Font-Size="12px" ItemStyle-Font-Size="14px" HeaderStyle-Width="4%" ItemStyle-Width="4%" HeaderStyle-CssClass="headercenter" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Center" >
                                                    <ItemTemplate>
                                                        <!--<i class="fa fa-check fa-lg" style="color: #4BDF1D; display: <%# Eval("lmaStatus").ToString().ToLower() == "done" ? "normal" : "none" %>"></i>-->
                                                        <asp:label runat="server" visible='<%# Eval("payergroupid").ToString().ToLower() != "4" && Eval("isPrimary").ToString().ToLower() == "true" %>' Text="-"></asp:label>
                                                        <asp:Image ID="chkTrueLMA" Style="text-align: center" Width="20px" Height="20px" ImageUrl="~/Images/Icon/icon_checked_locked.svg" runat="server" Visible='<%# Eval("lmaStatus").ToString() == "DONE" && Eval("payergroupid").ToString().ToLower() == "4" %>' />
                                                        <%--<asp:ImageButton ID="chkFalseLMA" Width="20px" Height="20px" ImageUrl="~/Images/Icon/icon_0_Unchecked.svg" runat="server" Visible='<%#Eval("lmaStatus").ToString() == "PENDING" %>' />--%>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Resume" HeaderStyle-Font-Size="12px" ItemStyle-Font-Size="14px" HeaderStyle-Width="4%" ItemStyle-Width="4%" HeaderStyle-CssClass="headercenter" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Center" >
                                                    <ItemTemplate>
                                                       <%-- <i class="fa fa-check fa-lg" style="color: #4BDF1D; display: <%# Eval("resumeStatus").ToString().ToLower() == "done" ? "normal" : "none" %>"></i>--%>
                                                        <asp:Image ID="chkTrueResume" Style="text-align: center" Width="20px" Height="20px" ImageUrl="~/Images/Icon/icon_checked_locked.svg" runat="server" Visible='<%# Eval("resumeStatus").ToString().ToLower() == "done" %>'  />
                                                         <%--<asp:ImageButton ID="chkFalseResume" Width="20px" Height="20px" ImageUrl="~/Images/Icon/icon_0_Unchecked.svg" runat="server" Visible='<%#Eval("resumeStatus").ToString() == "PENDING" %>'   />--%>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Hasil" HeaderStyle-Font-Size="12px" ItemStyle-Font-Size="14px" HeaderStyle-Width="5%" ItemStyle-Width="5%" HeaderStyle-CssClass="headercenter" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Center" >
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="labResult" Width="20px" Height="20px" ImageUrl="~/Images/Icon/labE.png" runat="server" Visible='<%# Eval("isLab").ToString() != "0" && Eval("isPrimary").ToString().ToLower() == "true" %>' OnClick="labResult_Click" />
                                                        <asp:ImageButton Width="20px" Height="20px" ImageUrl="~/Images/Icon/labD.png" runat="server" Enabled="false" Visible='<%# Eval("isLab").ToString() != "1" && Eval("isPrimary").ToString().ToLower() == "true" %>' />
                                                        <asp:ImageButton ID="radResult" Width="20px" Height="20px" ToolTip="View Radiology" ImageUrl="~/Images/Icon/radE.png" runat="server" Visible='<%# Eval("isRad").ToString() != "0" && Eval("isPrimary").ToString().ToLower() == "true" %>' />
                                                        <asp:ImageButton Width="20px" Height="20px" ImageUrl="~/Images/Icon/radD.png" runat="server" Enabled="false" Visible='<%# Eval("isRad").ToString() != "1" && Eval("isPrimary").ToString().ToLower() == "true" %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderStyle-Font-Size="12px" ItemStyle-Width="5.92%" HeaderStyle-Width="5.92%" HeaderStyle-CssClass="headercenter" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="ImageButton1" Enabled="false" ImageUrl="~/Images/Icon/Label_selesai.svg" runat="server" Visible='<%# Eval("IsPrimary").ToString().ToLower() == "true" && Eval("flagSubmit").ToString().ToLower() == "true" %>'  />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>

                                        <div style="background-color:white; padding-top:20px;">
                                            <div class="labelakanadatang" id="divflaglabel" runat="server">
                                                <label><strong>AKAN DATANG</strong></label>
                                            </div>
                                        </div>
                                        
                                        <asp:GridView ID="gvLIstAkanDatang" runat="server" CssClass="table table-hover-worklist shadowtable" BorderColor="Transparent" AutoGenerateColumns="false"
                                            AllowPagging="true" PageSize="8" ShowHeaderWhenEmpty="True" EmptyDataText="No Data" ShowHeader="false" BorderWidth="0" OnRowDataBound="gvLIstNewDischarge_DataBound">
                                            <Columns>
                                                <asp:TemplateField HeaderText="Nama Pasien" HeaderStyle-Font-Size="12px" ItemStyle-Font-Size="14px" HeaderStyle-Width="14%" HeaderStyle-HorizontalAlign="Left" ItemStyle-Width="14%" ItemStyle-HorizontalAlign="Left" >
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="patientName" runat="server"  Visible='<%# Eval("isPrimary").ToString().ToLower() == "true" %>' Text='<%# Bind("PatientName") %>' Style="color:#2A3593;font-weight:bold;text-decoration:underline;"  CssClass="tooltipnotes" data-toggle="tooltip" data-html="true" data-placement="auto bottom" title='<%# Eval("AdditionalNotes").ToString().Replace("\n","</br>") %>' OnClick="LnkPatientDetailPlanLate_onClick"></asp:LinkButton>
                                                        <%--<asp:Image Width="50px" Height="20px" ImageUrl="~/Images/Icon/label_new.svg" runat="server" Visible='<%# Eval("isNew").ToString() != "False" && Eval("isPrimary").ToString().ToLower() == "true" %>' />--%>
                                                        <asp:Image Style="margin-bottom: 3px; color:#2A3593;font-weight:bold;text-decoration:underline" Height="16px" ImageUrl="~/Images/Icon/icon_notes.svg" runat="server" Visible='<%# Eval("isPrimary").ToString().ToLower() == "true" && Eval("AdditionalNotes").ToString() != ""  %>'  />
                                                        <asp:HiddenField ID="lbAdmissionId" runat="server" Value='<%# Bind("admissionId") %>'></asp:HiddenField>
                                                        <asp:HiddenField ID="lbWorklistId" runat="server" Value='<%# Bind("worklistId") %>'></asp:HiddenField>
                                                        <asp:HiddenField ID="lbDoctorId" runat="server" Value='<%# Bind("doctorId") %>'></asp:HiddenField>
                                                        <asp:HiddenField ID="lbPatientId" runat="server" Value='<%# Bind("patientId") %>'></asp:HiddenField>
                                                        <asp:HiddenField ID="hfwaitStatus" runat="server" Value='<%# Bind("waitStatus") %>'></asp:HiddenField>
                                                        <asp:HiddenField ID="hfisPrescription" runat="server" Value='<%# Bind("isPrescription") %>'></asp:HiddenField>
                                                        <asp:HiddenField ID="hfisRetur" runat="server" Value='<%# Bind("isRetur") %>'></asp:HiddenField>
                                                        <asp:HiddenField ID="hflmaStatus" runat="server" Value='<%# Bind("lmaStatus") %>'></asp:HiddenField>
                                                        <asp:HiddenField ID="hfresumeStatus" runat="server" Value='<%# Bind("resumeStatus") %>'></asp:HiddenField>
                                                        <asp:HiddenField ID="ProcessId" runat="server" Value ='<%# Bind("ProcessId") %>'></asp:HiddenField>
                                                        <asp:HiddenField ID="AdditionalNotes" runat="server" Value ='<%# Bind("AdditionalNotes") %>'></asp:HiddenField>
                                                        <asp:HiddenField ID="ModifiedDate" runat="server" Value ='<%# Bind("ModifiedDate") %>'></asp:HiddenField>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="JK" HeaderStyle-Font-Size="12px" ItemStyle-Font-Size="14px" HeaderStyle-CssClass="headercenter" HeaderStyle-Width="3%" ItemStyle-Width="3%" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" >
                                                    <ItemTemplate>
                                                        <asp:Label ID="Sex" runat="server" Font-Bold="true"  Visible='<%# Eval("isPrimary").ToString().ToLower() == "true" %>' ><span Style="color:<%# Eval("gender").ToString().ToLower() == "f" ? "#E51B94" : "#1B29E5" %>"><%# Eval("gender") %></span></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="No. Mr" HeaderStyle-Font-Size="12px" ItemStyle-Font-Size="14px" ItemStyle-Width="4%" HeaderStyle-Width="4%" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" >
                                                    <ItemTemplate>
                                                        <asp:Label ID="NoMR" runat="server" Visible='<%# Eval("isPrimary").ToString().ToLower() == "true" %>' Text='<%# Bind("localMrNo") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Rencana Plg" HeaderStyle-Font-Size="12px" ItemStyle-Font-Size="14px" ItemStyle-Width="7.8%" HeaderStyle-Width="7.8%" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" >
                                                    <ItemTemplate>
                                                        <asp:Label ID="umur" runat="server" Visible='<%# Eval("isPrimary").ToString().ToLower() == "true" %>' Text='<%# Bind("WorklistDate","{0:dd MMM yyyy}") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Kmr" HeaderStyle-Font-Size="12px" ItemStyle-Font-Size="14px" ItemStyle-Width="4%" HeaderStyle-Width="4%" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Kmr" runat="server" Visible='<%# Eval("isPrimary").ToString().ToLower() == "true" %>' Text='<%# Bind("roomNo") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField HeaderText="Dokter" HeaderStyle-Font-Size="12px" ItemStyle-Font-Size="14px" ItemStyle-Width="13.57%" HeaderStyle-Width="13.57%" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Center" DataField="doctorName"></asp:BoundField>
                                                <%--<asp:BoundField HeaderText="Penanggung" ItemStyle-Width="17%" ItemStyle-HorizontalAlign="Left" DataField="payerName"></asp:BoundField>--%>
                                                <asp:TemplateField HeaderText="Penanggung" ItemStyle-Font-Size="14px" ItemStyle-Width="15.6%" HeaderStyle-Width="15.6%" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" >
                                                    <ItemStyle CssClass="bordertable" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="payerName" ItemStyle-Font-Size="14px" Visible='<%# Eval("isPrimary").ToString().ToLower() == "true" %>' runat="server"  Text='<%# Bind("payerName") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Visit" HeaderStyle-Font-Size="12px" ItemStyle-Font-Size="14px" ItemStyle-Width="4.2%" HeaderStyle-Width="4.2%" HeaderStyle-CssClass="headercenter" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" >
                                                    <ItemTemplate>
                                                        <asp:Label ID="visit" runat="server" Font-Bold="true" Text='<%# Eval("waitStatus").ToString() != "TD" && Eval("waitStatus").ToString() != "TU" ? "TUTD" : "" %>'></asp:Label>
                                                        <asp:Image ID="chkTrueVisit" Style="text-align: center" Width="20px" Height="20px" ImageUrl="~/Images/Icon/icon_checked_locked.svg" runat="server" Visible='<%# Eval("visitValue").ToString() == "DONE" && Eval("WaitStatus").ToString() == "TD" %>'/>
                                                        <%--<asp:ImageButton ID="chkFalseVisit" Width="20px" Height="20px" ImageUrl="~/Images/Icon/icon_0_Unchecked.svg" runat="server" Visible='<%# Eval("visitValue").ToString() == "PENDING" %>' />--%>
                                                        <%--<i class="fa fa-check fa-lg" style="color: #4BDF1D; display: <%# Eval("visitValue").ToString().ToLower() == "done" && Eval("WaitStatus").ToString() == "TD" ? "normal" : "none" %>"></i>--%>
                                                        <asp:Image  ImageUrl="~/Images/Icon/0_check_result.svg" runat="server" Visible='<%# Eval("visitValue").ToString().ToLower() == "done" && Eval("WaitStatus").ToString() == "TU" %>' data-toggle="tooltip" data-html="true" data-placement="auto bottom" title='<%# Eval("Remarks").ToString().Replace("\n","</br>") %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Resep" HeaderStyle-Font-Size="12px" ItemStyle-Font-Size="14px" ItemStyle-Width="5%" HeaderStyle-Width="5%" HeaderStyle-CssClass="headercenter" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Center" >
                                                    <ItemTemplate>
                                                        <asp:Label ID="prescription" runat="server" Visible='<%# Eval("isPrescription").ToString().ToLower() == "false" %>' Text='<%# Eval("isPrescription").ToString().ToLower() == "false" ? "-" : "" %>'></asp:Label>
                                                        <asp:Image ID="chkTruePrescription" Style="text-align: center" Width="20px" Height="20px" ImageUrl="~/Images/Icon/icon_checked_locked.svg" runat="server" Visible='<%# Eval("prescriptionValue").ToString().ToLower() == "done" %>' />
                                                        <%--<asp:ImageButton ID="chkFalsePrescription" Width="20px" Height="20px" ImageUrl="~/Images/Icon/icon_0_Unchecked.svg" runat="server" Visible='<%#Eval("prescriptionValue").ToString() == "PENDING" %>'  />--%>
                                                        <%--<i class="fa fa-check fa-lg" style="color: #4BDF1D; display: <%# Eval("prescriptionValue").ToString().ToLower() == "done" ? "normal" : "none" %>"></i>--%>
                                                        
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Retur" HeaderStyle-Font-Size="12px" ItemStyle-Font-Size="14px" ItemStyle-Width="5%" HeaderStyle-Width="5%" HeaderStyle-CssClass="headercenter" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Center" >
                                                    <ItemTemplate>
                                                        <asp:Label ID="retur" runat="server" Visible='<%# Eval("isRetur").ToString().ToLower() == "false" %>' Text='<%# Eval("isRetur").ToString().ToLower() == "false" ? "-" : "" %>'></asp:Label>
                                                        <asp:Image ID="chkTrueReturPlan" Style="text-align: center" Width="20px" Height="20px" ImageUrl="~/Images/Icon/icon_checked_locked.svg" runat="server" Visible='<%# Eval("returValue").ToString() == "DONE" %>' />
                                                        <asp:Image ID="chkFalseReturPlan" Width="20px" Height="20px" ImageUrl="~/Images/Icon/icon_unchecked.svg" runat="server" Visible='<%#Eval("returValue").ToString() == "PENDING" %>' />
                                                        <%--<asp:ImageButton ID="chkTrueRetur" Style="text-align: center" Width="20px" Height="20px" ImageUrl="~/Images/Icon/icon_checked.svg" runat="server" Visible='<%# Eval("returValue").ToString() == "DONE" %>' />
                                                        <asp:ImageButton ID="chkFalseRetur" Width="20px" Height="20px" ImageUrl="~/Images/Icon/icon_0_Unchecked.svg" runat="server" Visible='<%#Eval("returValue").ToString() == "PENDING" %>' />--%>
                                                        <%--<i class="fa fa-check fa-lg" style="color: #4BDF1D; display: <%# Eval("returValue").ToString().ToLower() == "done" ? "normal" : "none" %>"></i>--%>
                                                        
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="LMA" HeaderStyle-Font-Size="12px" ItemStyle-Font-Size="14px" ItemStyle-Width="4%" HeaderStyle-Width="4%" HeaderStyle-CssClass="headercenter" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Center" >
                                                    <ItemTemplate>
                                                        <%--<i class="fa fa-check fa-lg" style="color: #4BDF1D; display: <%# Eval("lmaStatus").ToString().ToLower() == "done" ? "normal" : "none" %>"></i>--%>
                                                        <asp:label runat="server" visible='<%# Eval("payergroupid").ToString().ToLower() != "4" %>' Text="-"></asp:label>
                                                        <asp:Image ID="chkTrueLMA" Style="text-align: center" Width="20px" Height="20px" ImageUrl="~/Images/Icon/icon_checked_locked.svg" runat="server" Visible='<%# Eval("lmaStatus").ToString().ToLower() == "done" && Eval("payergroupid").ToString().ToLower() == "4"  %>' />
                                                        <%--<<asp:ImageButton ID="chkFalseLMA" Width="20px" Height="20px" ImageUrl="~/Images/Icon/icon_0_Unchecked.svg" runat="server" Visible='<%#Eval("lmaStatus").ToString() == "PENDING" %>' />--%>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Resume" HeaderStyle-Font-Size="12px" ItemStyle-Font-Size="14px" ItemStyle-Width="4%" HeaderStyle-Width="4%" HeaderStyle-CssClass="headercenter" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Center" >
                                                    <ItemTemplate>
                                                        <%--<i class="fa fa-check fa-lg" style="color: #4BDF1D; display: <%# Eval("resumeStatus").ToString().ToLower() == "done" ? "normal" : "none" %>"></i>--%>
                                                        <asp:Image ID="chkTrueResume" Style="text-align: center" Width="20px" Height="20px" ImageUrl="~/Images/Icon/icon_checked_locked.svg" runat="server" Visible='<%# Eval("resumeStatus").ToString().ToLower() == "done" %>'  />
                                                        <%--<asp:ImageButton ID="chkFalseResume" Width="20px" Height="20px" ImageUrl="~/Images/Icon/icon_0_Unchecked.svg" runat="server" Visible='<%#Eval("resumeStatus").ToString() == "PENDING" %>'   />--%>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Hasil" HeaderStyle-Font-Size="12px" ItemStyle-Font-Size="14px" ItemStyle-Width="5%" HeaderStyle-Width="5%" HeaderStyle-CssClass="headercenter" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Center" >
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="labResult" Width="20px" Height="20px" ImageUrl="~/Images/Icon/labE.png" runat="server" Visible='<%# Eval("isLab").ToString() != "0" && Eval("isPrimary").ToString().ToLower() == "true" %>' OnClick="labResultLate_Click" />
                                                        <asp:ImageButton Width="20px" Height="20px" ImageUrl="~/Images/Icon/labD.png" runat="server" Enabled="false" Visible='<%# Eval("isLab").ToString() != "1" && Eval("isPrimary").ToString().ToLower() == "true" %>'  />
                                                        <asp:ImageButton ID="radResult" Width="20px" Height="20px" ToolTip="View Radiology" ImageUrl="~/Images/Icon/radE.png" runat="server" Visible='<%# Eval("isRad").ToString() != "0" && Eval("isPrimary").ToString().ToLower() == "true" %>' />
                                                        <asp:ImageButton Width="20px" Height="20px" ImageUrl="~/Images/Icon/radD.png" runat="server" Enabled="false" Visible='<%# Eval("isRad").ToString() != "1" && Eval("isPrimary").ToString().ToLower() == "true" %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderStyle-Font-Size="12px" ItemStyle-Width="5.92%" HeaderStyle-Width="5.92%" HeaderStyle-CssClass="headercenter" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="ImageButton1" Style="height:20px" Enabled="false" ImageUrl="~/Images/Icon/Label_selesai.svg" runat="server" Visible='<%# Eval("IsPrimary").ToString().ToLower() == "true" && Eval("flagSubmit").ToString().ToLower() == "true" %>'  />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </ItemTemplate>
                            </asp:Repeater>

                            
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>

    <asp:UpdatePanel runat="server" ID="upSedangProcess">
        <ContentTemplate>
            <div class="supreme-container" id="divDischargeProcess" runat="server">
                <div class="row" style="padding-top: 10px">
                    <div class="col-lg-12">
                        <div class="modal-dialog mini-dialog" style="background-color:#EEEEEE">
                            <div class="modal-header mini-header" style="background-color: #EEEEEE;padding-top:8px; padding-top:8px;border-bottom: none;">
                                <div class="col-sm-4" style="padding-left: 0px; padding-right:0px">
                                    <asp:HiddenField runat="server" ID="flagdischargeprocess" />
                                    <asp:Label ID="lblheaderprocess" Font-Size="25px" runat="server" Text="Sedang Diproses"></asp:Label>
                                    <asp:Label runat="server" ID="lblCountProcess" Font-Size="25px" Font-Bold="false" Text="0"></asp:Label>
                                    <br />
                                    <asp:LinkButton runat="server" CssClass="linkhover" ID="lnkrefreshprocess" Style="cursor:pointer;" OnClick="BtnRefreshProcess_OnClick" ><i class="fa fa-refresh"></i>
                                        <label style="cursor:pointer">Terakhir Diperbarui: </label>
                                        <asp:Label runat="server" ID="lblDateRefreshProcess"></asp:Label>
                                    </asp:LinkButton>
                                </div>

                                <div class="col-lg-8" style="margin-top:13px; text-align:right;padding-left:8%">
                                  <table border="0" >
                                    <tr>
                                        <td style="width:20%"><label style="vertical-align:middle; font-size:12px;">FILTER: </label></td>
                                        <td style="width:20%"><asp:DropDownList Style="background-color:white" Font-Size="12px" Font-Names="Helvetica" AutoPostBack="true" ID="ddlWardListProcess" runat="server" OnSelectedIndexChanged="DDLWardListProcess_onSelectedIndexChanged" CssClass="selecpicker greyItem" data-live-search="true" data-size="7" data-width="132px" data-height="12px" data-dropup-auto="false"></asp:DropDownList></td>
                                        <td style="width:20%; display: none;" >
                                            <div class="has-feedback" style="display: inline;">
                                                <asp:TextBox runat="server" AutoCompleteType="Disabled" placeholder="Tgl Rencana Plg" CssClass="underlined textboxempty" Style="outline-color: transparent; resize: none" Font-Names="Helvetica, Arial, sans-serif" ID="txtdateprocess" onMouseDown="datetoprocess();" AutoPostBack="true" OnTextChanged="SearchProcess_onChange" />
                                                <span style="margin-top: -8px; margin-right: 5px; margin-left:-20px; ">
                                                    <asp:LinkButton  ID="btnClearDateProcess" runat="server" visible="false" OnClientClick="clearTxtSearchDateProcess();" OnClick="SearchProcess_onChange"  >
                                                        <i class="fa fa-times-circle"></i>
                                                    </asp:LinkButton>
                                                </span>
                                                <span id="btnShowCalendarProcess" visible="false" runat="server" style="margin-top: -9px;" class="fa fa-calendar form-control-feedback"></span>
                                            </div>
                                        </td>
                                        <td style="width:20%">
                                            <asp:Label ID="Label6" Style="font-size:12px;" runat="server" Text="MR/NAMA PASIEN"></asp:Label>
                                        </td>
                                        <td style="width:20%">
                                            <div class="has-feedback" style="display: inline;">
                                                <asp:TextBox runat="server" AutoCompleteType="Disabled" placeholder="Cari" CssClass="underlined textboxempty" Style="outline-color: transparent; width: 100%; resize: none" Font-Names="Helvetica, Arial, sans-serif" ID="txtsearchprocess" AutoPostBack="true"  OnTextChanged="SearchProcess_onChange" />

                                                <span style="margin-top: -9px;  margin-left:-32px; ">
	                                                <asp:LinkButton Visible="false" ID="btnClearTextFindPatientProcess" runat="server" OnClientClick="clearTxtFindProcess();" OnClick="SearchProcess_onChange">
		                                                <i class="fa fa-times-circle"></i>
	                                                </asp:LinkButton>
                                                </span>
                                                <span style="margin-top: -9px;color: #AAB3B9;" class="fa fa-search"></span>
                                                <!--<asp:UpdatePanel runat="server" ID="UpdatePanel7">
	                                                <ContentTemplate>
		                                                <asp:Button Style="display: none;" ID="btnFindPatientProcess"  runat="server" Text=""  />
	                                                </ContentTemplate>
                                                </asp:UpdatePanel>-->
                                            </div>
                                        </td>
                                    </tr>
                                </table>
                                </div>
                            </div>
                            <div style="text-align:center">
                                <asp:Image runat="server" ID="imgnodataprocess" ImageUrl="~/Images/Icon/ic_nodataBO.svg" Visible="false" style="padding-top:80px;" />
                            </div>
                            <asp:Repeater runat="server" ID="rptDischargeProcess" OnItemDataBound="rptDischargeProcess_onItemBound">
                                <ItemTemplate>
                                    <asp:HiddenField runat="server" ID="hdnWardId" Value='<%# Bind("WardId") %>' />
                                   <div style="background-color:#107798;margin-left:15px;margin-right:15px;padding:8px;font-weight:bold;border-radius:5px 5px 0px 0px;box-shadow:0px 3px 8px rgba(0, 0, 0, 0.08);  position: sticky; top: 4px; z-index: 4;">
                                        <asp:Label runat="server" ID="LblHeader" Style="color:white;font-size:18px;"></asp:Label><asp:Label runat="server" Style="color:white;font-size:18px;font-weight: normal;" ID="lblcountward"></asp:Label>
                                    </div>
                                    <div class="modal-body" style="padding-top: 0px; padding-bottom: 10px" id="Div1" runat="server">
                                        <asp:GridView ID="gvLIstDischargeProcess" runat="server" CssClass="table table-hover-worklist shadowtable" BorderColor="Transparent" AutoGenerateColumns="false"
                                            AllowPagging="true" PageSize="8" ShowHeaderWhenEmpty="True" EmptyDataText="No Data" BorderWidth="0" OnRowDataBound="gvLIstDischargeProcess_DataBound">
                                            <Columns>
                                                <asp:TemplateField HeaderText="Nama Pasien" HeaderStyle-Font-Size="12px" ItemStyle-Font-Size="14px" HeaderStyle-Width="14%" HeaderStyle-HorizontalAlign="Left" ItemStyle-Width="14%" ItemStyle-HorizontalAlign="Left" >
                                                    <ItemTemplate>
                                                        <%--<div id="divred" runat="server">
                                                            <asp:LinkButton ID="patientName" CssClass="tooltipnotes" data-toggle="tooltip" data-html="true" data-placement="auto bottom" title='<%# Eval("AdditionalNotes").ToString().Replace("\n","</br>") %>' Style="color:#2A3593;font-weight:bold;" runat="server"  Visible='<%# Eval("isPrimary").ToString().ToLower() == "true" %>' Text='<%# Bind("PatientName") %>' OnClick="LnkPatientDetailProcess_onClick"></asp:LinkButton>
                                                            <asp:Image  Width="20px"  Height="13px" ImageUrl="~/Images/Icon/icon_notes.svg" runat="server" Visible='<%# Eval("isPrimary").ToString().ToLower() == "true" && Eval("AdditionalNotes").ToString() != ""  %>' />
                                                        </div>--%>
                                                        <asp:LinkButton ID="patientName" CssClass="tooltipnotes" data-toggle="tooltip" data-html="true" data-placement="auto bottom" title='<%# Eval("AdditionalNotes").ToString().Replace("\n","</br>") %>' Style="color:#2A3593;font-weight:bold;text-decoration:underline" runat="server"  Visible='<%# Eval("isPrimary").ToString().ToLower() == "true" %>' Text='<%# Bind("PatientName") %>' OnClick="LnkPatientDetailProcess_onClick"></asp:LinkButton>
                                                        <%--<asp:Image Width="50px" Height="20px" ImageUrl="~/Images/Icon/label_new.svg" runat="server" Visible='<%# Eval("isNew").ToString() != "False" && Eval("isPrimary").ToString().ToLower() == "true" %>' />--%>
                                                        <asp:Image Style="margin-bottom: 3px;" Height="16px" ImageUrl="~/Images/Icon/icon_notes.svg" runat="server" Visible='<%# Eval("isPrimary").ToString().ToLower() == "true" && Eval("AdditionalNotes").ToString() != ""  %>' />
                                                        <asp:HiddenField ID="lbAdmissionId" runat="server" Value='<%# Bind("admissionId") %>'></asp:HiddenField>
                                                        <asp:HiddenField ID="lbWorklistId" runat="server" Value='<%# Bind("worklistId") %>'></asp:HiddenField>
                                                        <asp:HiddenField ID="lbDoctorId" runat="server" Value='<%# Bind("doctorId") %>'></asp:HiddenField>
                                                        <asp:HiddenField ID="lbPatientId" runat="server" Value='<%# Bind("patientId") %>'></asp:HiddenField>
                                                        <asp:HiddenField ID="hfisPrescription" runat="server" Value='<%# Bind("isPrescription") %>'></asp:HiddenField>
                                                        <asp:HiddenField ID="hfisRetur" runat="server" Value='<%# Bind("isRetur") %>'></asp:HiddenField>
                                                        <asp:HiddenField ID="hfFinalDate" runat="server" Value='<%# Bind("FinalDate") %>' />
                                                        <asp:HiddenField ID="ProcessId" runat="server" Value ='<%# Bind("ProcessId") %>'></asp:HiddenField>
                                                        <asp:HiddenField ID="ModifiedDate" runat="server" Value ='<%# Bind("ModifiedDate") %>'></asp:HiddenField>
                                                        <asp:HiddenField ID="WardId" runat="server" Value ='<%# Bind("WardId") %>'></asp:HiddenField>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="No. Mr" HeaderStyle-Font-Size="12px" ItemStyle-Font-Size="14px" ItemStyle-Width="4%" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" >
                                                    <ItemTemplate>
                                                        <asp:Label ID="NoMR" runat="server" Visible='<%# Eval("isPrimary").ToString().ToLower() == "true" %>' Text='<%# Bind("localMrNo") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Rencana Plg" HeaderStyle-Font-Size="12px" ItemStyle-Font-Size="14px" ItemStyle-Width="8%" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" >
                                                    <ItemTemplate>
                                                        <asp:Label ID="umur" runat="server" Visible='<%# Eval("isPrimary").ToString().ToLower() == "true" %>' Text='<%# Bind("WorklistDate","{0:dd MMM yyyy}") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Kmr" HeaderStyle-Font-Size="12px" ItemStyle-Font-Size="14px" ItemStyle-Width="4%" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" >
                                                    <ItemTemplate>
                                                        <asp:Label ID="Kmr" runat="server" Visible='<%# Eval("isPrimary").ToString().ToLower() == "true" %>' Text='<%# Bind("roomNo") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Penanggung" HeaderStyle-Font-Size="12px" ItemStyle-Font-Size="14px" ItemStyle-Width="12%" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" >
                                                    <ItemTemplate>
                                                        <asp:Label ID="payerName" Visible='<%# Eval("isPrimary").ToString().ToLower() == "true" %>' runat="server"  Text='<%# Bind("payerName") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderStyle-Font-Size="12px" ItemStyle-Width="4%" ItemStyle-Font-Size="14px" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle"  HeaderStyle-HorizontalAlign="Center">
                                                    <HeaderTemplate>
                                                        <label style="padding-left: 8px">Resep</label>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <label style="color: black; display: <%# Eval("IsPrescription").ToString().ToLower() == "false" ? "normal" : "none" %>">-</label>
                                                        <%--<i class="fa fa-check fa-lg" style="color: #4BDF1D; display: <%# Eval("IsPrescription").ToString().ToLower() == "true" ? "normal" : "none" %>"></i>--%>
                                                        <asp:Image ID="chkTruePrescription" Style="text-align: center" Width="20px" Height="20px" ImageUrl="~/Images/Icon/icon_checked_locked.svg" runat="server" Visible='<%# Eval("IsPrescription").ToString().ToLower() == "true" %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderStyle-Font-Size="12px" ItemStyle-Width="4%" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle" >
                                                    <ItemStyle CssClass="bordertable" />
                                                    <HeaderTemplate>
                                                        <label style="padding-left: 2px">Retur</label>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <label style="color : black; display: <%# Eval("IsRetur").ToString().ToLower() == "false" ? "normal" : "none" %>">-</label>
                                                        <%--<i class="fa fa-check fa-lg" style="color: #4BDF1D; display: <%# Eval("IsRetur").ToString().ToLower() == "true" ? "normal" : "none" %>"></i>--%>
                                                        <asp:Image Style="text-align: center" Width="20px" Height="20px" ImageUrl="~/Images/Icon/icon_checked_locked.svg" runat="server" Visible='<%# Eval("IsRetur").ToString().ToLower() == "true" %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderStyle-Font-Size="12px" ItemStyle-Width="7%" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle">
                                                    <ItemStyle CssClass="bordertable" />
                                                    <HeaderTemplate>
                                                        <center>
                                                            <img src="<%= Page.ResolveClientUrl("~/Images/Icon/icon_discharge_nurse.svg") %>" style="text-align: center; vertical-align: middle; padding-top:10px" />
                                                        </center>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <%--<i class="fa fa-check fa-lg" style="color: #4BDF1D; display: <%# Eval("SubmitDate").ToString().ToLower() != "01/01/0001 00:00:00" ? "normal" : "none" %>"></i>--%>
                                                        <asp:Image Style="text-align: center" Width="20px" Height="20px" ImageUrl="~/Images/Icon/icon_checked_locked.svg" runat="server" Visible='<%#  Eval("SubmitDate").ToString().ToLower() != "01/01/0001 00:00:00" %>' />
                                                        <br />
                                                        <asp:Label ID="lblSubDischargeBedDate" runat="server" Visible='<%# Eval("SubmitDate").ToString().ToLower() != "01/01/0001 00:00:00" && Eval("isShowDate ").ToString().ToLower() == "0" %>' Text='<%# Eval("SubmitDate","{0:HH:mm}").ToString() %>'></asp:Label>
                                                        <asp:Label ID="lblSubDischargeBedDate1" runat="server" Visible='<%# Eval("SubmitDate").ToString().ToLower() != "01/01/0001 00:00:00" && Eval("isShowDate").ToString().ToLower() == "1" %>' Text='<%# Eval("SubmitDate","{0:dd/MM/y HH:mm}").ToString() %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderStyle-Font-Size="12px" HeaderText="Service" ItemStyle-Width="7%" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle" >
                                                    <HeaderTemplate>
                                                        <center>
                                                            <img src="<%= Page.ResolveClientUrl("~/Images/Icon/icon_discharge_clerk.svg") %>" style="text-align: center; vertical-align: middle; padding-top:5px" />
                                                        </center>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <!--<i class="fa fa-check fa-lg" style="color: #4BDF1D; display: <%# Eval("SubDateService").ToString().ToLower() != "01/01/0001 00:00:00" ? "normal" : "none" %>"></i>-->
                                                        <asp:Image Style="text-align: center" Width="20px" Height="20px" ImageUrl="~/Images/Icon/icon_checked_locked.svg" runat="server" Visible='<%#  Eval("SubDateService").ToString().ToLower() != "01/01/0001 00:00:00" %>' />
                                                        <br />
                                                        <asp:Label ID="lblSubDischargeServiceDate" runat="server" Style='<%# Eval("lateservice").ToString().ToLower() == "true" ? "color:#D82C1D" : "color:black" %>'  Visible='<%# DateTime.Parse(Eval("SubDateService").ToString()).ToString("dd/MM/yyyy HH:mm:ss") != "01/01/0001 00:00:00" && Eval("isShowDate ").ToString().ToLower() == "0" %>' Text='<%# DateTime.Parse(Eval("SubDateService").ToString()).ToString("HH:mm") %>'></asp:Label>
                                                        <asp:Label ID="lblSubDischargeServiceDate1" runat="server" Style='<%# Eval("lateservice").ToString().ToLower() == "true" ? "color:#D82C1D" : "color:black" %>' Visible='<%# DateTime.Parse(Eval("SubDateService").ToString()).ToString("dd/MM/yyyy HH:mm:ss") != "01/01/0001 00:00:00" && Eval("isShowDate ").ToString().ToLower() == "1" %>' Text='<%# DateTime.Parse(Eval("SubDateService").ToString()).ToString("dd/MM/y HH:mm") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Item" HeaderStyle-Font-Size="12px" ItemStyle-Width="7%" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" >
                                                    <ItemStyle CssClass="bordertable" />
                                                    <HeaderTemplate>
                                                        <center>
                                                            <img src="<%= Page.ResolveClientUrl("~/Images/Icon/icon_discharge_farma.svg") %>" style="text-align: center; vertical-align: middle;padding-top:5px" />
                                                        </center>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <%--<i class="fa fa-check fa-lg" style="color: #4BDF1D; display: <%# Eval("SubDateItem").ToString().ToLower() != "01/01/0001 00:00:00" ? "normal" : "none" %>"></i>--%>
                                                        <asp:Image Style="text-align: center" Width="20px" Height="20px" ImageUrl="~/Images/Icon/icon_checked_locked.svg" runat="server" Visible='<%#  Eval("SubDateItem").ToString().ToLower() != "01/01/0001 00:00:00" %>' />
                                                        <br />
                                                        <asp:Label ID="lblSubDischargeItemDate" runat="server" Style='<%# Eval("lateitem").ToString().ToLower() == "true" ? "color:#D82C1D" : "color:black" %>' Visible='<%# DateTime.Parse(Eval("SubDateItem").ToString()).ToString("dd/MM/yyyy HH:mm:ss") != "01/01/0001 00:00:00" && Eval("isShowDate ").ToString().ToLower() == "0" && Eval("IsNeedPrescription").ToString().ToLower() != "false" %>' Text='<%# DateTime.Parse(Eval("SubDateItem").ToString()).ToString("HH:mm") %>'></asp:Label>
                                                        <asp:Label ID="lblSubDischargeItemDate1" runat="server" Style='<%# Eval("lateitem").ToString().ToLower() == "true" ? "color:#D82C1D" : "color:black" %>' Visible='<%# DateTime.Parse(Eval("SubDateItem").ToString()).ToString("dd/MM/yyyy HH:mm:ss") != "01/01/0001 00:00:00" && Eval("isShowDate ").ToString().ToLower() == "1" && Eval("IsNeedPrescription").ToString().ToLower() != "false" %>' Text='<%# DateTime.Parse(Eval("SubDateItem").ToString()).ToString("dd/MM/y HH:mm") %>'></asp:Label>
                                                        <asp:Label ID="lblTidakPerlu" runat="server" Visible='<%# Eval("IsNeedPrescription").ToString().ToLower() == "false" %>' Text=""></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Final Discharge" HeaderStyle-Font-Size="12px" ItemStyle-Width="7%" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" >
                                                    <ItemTemplate>
                                                        <%--<i class="fa fa-check fa-lg" style="color: #4BDF1D; display: <%# Eval("FinalDate").ToString().ToLower() != "01/01/0001 00:00:00" ? "normal" : "none" %>"></i>--%>
                                                        <asp:Image Style="text-align: center" Width="20px" Height="20px" ImageUrl="~/Images/Icon/icon_checked_locked.svg" runat="server" Visible='<%#  Eval("FinalDate").ToString().ToLower() != "01/01/0001 00:00:00" %>' />
                                                        <br />
                                                        <asp:Label ID="lblFinalDischargeDate" runat="server" Visible='<%# DateTime.Parse(Eval("FinalDate").ToString()).ToString("dd/MM/yyyy HH:mm:ss") != "01/01/0001 00:00:00" && Eval("isShowDate ").ToString().ToLower() == "0" %>' Text='<%# DateTime.Parse(Eval("FinalDate").ToString()).ToString("HH:mm") %>'></asp:Label>
                                                        <asp:Label ID="lblFinalDischargeDate1" runat="server" Visible='<%# DateTime.Parse(Eval("FinalDate").ToString()).ToString("dd/MM/yyyy HH:mm:ss") != "01/01/0001 00:00:00" && Eval("isShowDate ").ToString().ToLower() == "1" %>' Text='<%# DateTime.Parse(Eval("FinalDate").ToString()).ToString("dd/MM/y HH:mm") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Email Ke Asuransi" HeaderStyle-Font-Size="12px" ItemStyle-Width="4%" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Center" >
                                                    <ItemTemplate>
                                                        <asp:Label ID="Lblnoemail" runat="server" Visible='<%# Eval("payerName").ToString().ToLower() == "private" %>' Text="-"></asp:Label>
                                                        <%--<asp:ImageButton ID="chkdisabledemail" Style="text-align: center;cursor:not-allowed" Enabled="false" Width="20px" Height="20px" ImageUrl="~/Images/Icon/icon_check_later.svg" runat="server" Visible='<%# Eval("payerName").ToString().ToLower() != "private" &&  DateTime.Parse(Eval("FinalDate").ToString()).ToString("dd/MM/yyyy HH:mm:ss") == "01/01/0001 00:00:00" %>'  />--%>
                                                        <%--<asp:Image ID="chkTrueEmail" Style="text-align: center;" Width="20px" Height="20px" ImageUrl="~/Images/Icon/icon_checked_locked.svg" runat="server" Enabled='<%# DateTime.Parse(Eval("ConfirmDate").ToString()).ToString("dd/MM/yyyy HH:mm:ss") == "01/01/0001 00:00:00" %>' Visible='<%# Eval("payerName").ToString().ToLower() != "private" &&  DateTime.Parse(Eval("EmailDate").ToString()).ToString("dd/MM/yyyy HH:mm:ss") != "01/01/0001 00:00:00" %>' />--%>
                                                        <%--<asp:ImageButton ID="chkTrueEmail" Style="text-align: center;" Width="20px" Height="20px" ImageUrl="~/Images/Icon/icon_checked.svg" runat="server" Enabled='<%# DateTime.Parse(Eval("ConfirmDate").ToString()).ToString("dd/MM/yyyy HH:mm:ss") == "01/01/0001 00:00:00" %>' Visible='<%# Eval("payerName").ToString().ToLower() != "private" &&  DateTime.Parse(Eval("EmailDate").ToString()).ToString("dd/MM/yyyy HH:mm:ss") != "01/01/0001 00:00:00" %>' OnClick="btnUpdateEmail"  />
                                                        <asp:ImageButton ID="chkFalseEmail" Style="text-align: center;" Width="20px" Height="20px" ImageUrl="~/Images/Icon/icon_0_Unchecked.svg" runat="server" Visible='<%# Eval("payerName").ToString().ToLower() != "private" &&  DateTime.Parse(Eval("EmailDate").ToString()).ToString("dd/MM/yyyy HH:mm:ss") == "01/01/0001 00:00:00" && DateTime.Parse(Eval("FinalDate").ToString()).ToString("dd/MM/yyyy HH:mm:ss") != "01/01/0001 00:00:00" %>' OnClick="btnUpdateEmail"  />--%>
                                                        <asp:Image Style="text-align: center" Width="20px" Height="20px" ImageUrl="~/Images/Icon/icon_checked_locked.svg" runat="server" Visible='<%#  Eval("payerName").ToString().ToLower() != "private" &&  DateTime.Parse(Eval("EmailDate").ToString()).ToString("dd/MM/yyyy HH:mm:ss") != "01/01/0001 00:00:00" %>' />
                                                        <br />
                                                        <asp:Label ID="lblEmailDate" runat="server" Style='<%# Eval("lateemail").ToString().ToLower() == "true" ? "color:#D82C1D" : "color:black" %>' Visible='<%# Eval("payerName").ToString().ToLower() != "private" &&  DateTime.Parse(Eval("EmailDate").ToString()).ToString("dd/MM/yyyy HH:mm:ss") != "01/01/0001 00:00:00" && Eval("isShowDate ").ToString().ToLower() == "0" %>' Text='<%# DateTime.Parse(Eval("EmailDate").ToString()).ToString("HH:mm") %>'></asp:Label>
                                                        <asp:Label ID="lblEmailDate1" runat="server" Style='<%# Eval("lateemail").ToString().ToLower() == "true" ? "color:#D82C1D" : "color:black" %>' Visible='<%# Eval("payerName").ToString().ToLower() != "private" &&  DateTime.Parse(Eval("EmailDate").ToString()).ToString("dd/MM/yyyy HH:mm:ss") != "01/01/0001 00:00:00" && Eval("isShowDate ").ToString().ToLower() == "1" %>' Text='<%# DateTime.Parse(Eval("EmailDate").ToString()).ToString("dd/MM/y HH:mm") %>'></asp:Label>
                                                        <asp:HiddenField runat="server" ID="hdnEmailDateCompare" Value='<%# DateTime.Parse(Eval("EmailDate").ToString()).ToString("dd/MM/yyyy HH:mm") %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Dijamin Asuransi" HeaderStyle-Font-Size="12px" ItemStyle-Width="4%" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Center" >
                                                    <ItemTemplate>
                                                        <asp:Label ID="Lblnoconfirm" runat="server" Visible='<%# Eval("payerName").ToString().ToLower() == "private" %>' Text="-"></asp:Label>
                                                        <%--<asp:ImageButton ID="chkdisabledconfirm" Style="text-align: center;cursor:not-allowed" Enabled="false" Width="20px" Height="20px" ImageUrl="~/Images/Icon/icon_check_later.svg" runat="server" Visible='<%# Eval("payerName").ToString().ToLower() != "private" &&  DateTime.Parse(Eval("EmailDate").ToString()).ToString("dd/MM/yyyy HH:mm:ss") == "01/01/0001 00:00:00" %>'  />
                                                        <asp:Image ID="chkTrueConfirm" Style="text-align: center" Width="20px" Height="20px" ImageUrl="~/Images/Icon/icon_checked_locked.svg" runat="server" Visible='<%# Eval("payerName").ToString().ToLower() != "private" &&  DateTime.Parse(Eval("ConfirmDate").ToString()).ToString("dd/MM/yyyy HH:mm:ss") != "01/01/0001 00:00:00" %>' />--%>
                                                        <%--<asp:ImageButton ID="chkTrueConfirm" Style="text-align: center" Width="20px" Height="20px" ImageUrl="~/Images/Icon/icon_checked.svg" runat="server" Visible='<%# Eval("payerName").ToString().ToLower() != "private" &&  DateTime.Parse(Eval("ConfirmDate").ToString()).ToString("dd/MM/yyyy HH:mm:ss") != "01/01/0001 00:00:00" %>' OnClick="btnUpdateConfirmClear" />
                                                        <asp:ImageButton ID="chkFalseConfirm" Style="text-align: center" Width="20px" Height="20px" ImageUrl="~/Images/Icon/icon_0_Unchecked.svg" runat="server" Visible='<%# Eval("payerName").ToString().ToLower() != "private" &&  DateTime.Parse(Eval("ConfirmDate").ToString()).ToString("dd/MM/yyyy HH:mm:ss") == "01/01/0001 00:00:00" && DateTime.Parse(Eval("EmailDate").ToString()).ToString("dd/MM/yyyy HH:mm:ss") != "01/01/0001 00:00:00" %>' OnClick="btnUpdateConfirm" />--%>
                                                        <asp:Image Style="text-align: center" Width="20px" Height="20px" ImageUrl="~/Images/Icon/icon_checked_locked.svg" runat="server" Visible='<%#  Eval("payerName").ToString().ToLower() != "private" &&  DateTime.Parse(Eval("ConfirmDate").ToString()).ToString("dd/MM/yyyy HH:mm:ss") != "01/01/0001 00:00:00" %>' />
                                                        <br />
                                                        <asp:Label ID="lblConfirmDate" runat="server" Style='<%# Eval("lateconfirm").ToString().ToLower() == "true" ? "color:#D82C1D" : "color:black" %>' Visible='<%# Eval("payerName").ToString().ToLower() != "private" &&  DateTime.Parse(Eval("ConfirmDate").ToString()).ToString("dd/MM/yyyy HH:mm:ss") != "01/01/0001 00:00:00" && Eval("isShowDate ").ToString().ToLower() == "0" %>' Text='<%# DateTime.Parse(Eval("ConfirmDate").ToString()).ToString("HH:mm") %>'></asp:Label>
                                                        <asp:Label ID="lblConfirmDate1" runat="server" Style='<%# Eval("lateconfirm").ToString().ToLower() == "true" ? "color:#D82C1D" : "color:black" %>' Visible='<%# Eval("payerName").ToString().ToLower() != "private" &&  DateTime.Parse(Eval("ConfirmDate").ToString()).ToString("dd/MM/yyyy HH:mm:ss") != "01/01/0001 00:00:00" && Eval("isShowDate ").ToString().ToLower() == "1" %>' Text='<%# DateTime.Parse(Eval("ConfirmDate").ToString()).ToString("dd/MM/y HH:mm") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Invoice Selesai" HeaderStyle-Font-Size="12px" ItemStyle-Width="4%" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" >
                                                    <ItemStyle CssClass="bordertable" />
                                                    <ItemTemplate>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Boleh Pulang" HeaderStyle-Font-Size="12px" ItemStyle-Width="7%" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" >
                                                    <ItemTemplate>
                                                        <asp:Image ID="chkTruePulang" Style="text-align: center;" Width="20px" Height="20px" ImageUrl="~/Images/Icon/icon_checked_locked.svg" runat="server" Visible='<%# Eval("FlagDischarged").ToString().ToLower() != "01/01/0001 00:00:00" %>' />
                                                        <%--<asp:ImageButton ID="chkTruePulang" Style="text-align: center;" Width="20px" Height="20px" ImageUrl="~/Images/Icon/icon_checked.svg" runat="server" Visible='<%# Eval("FlagDischarged").ToString().ToLower() != "01/01/0001 00:00:00" %>' OnClick="btnUpdatePulang"  />
                                                        <asp:ImageButton ID="chkFalsePulang" Style="text-align: center;" Width="20px" Height="20px" ImageUrl="~/Images/Icon/icon_0_Unchecked.svg" runat="server" Visible='<%# Eval("FlagDischarged").ToString().ToLower() == "01/01/0001 00:00:00" %>' OnClick="btnUpdatePulang"  />--%>
                                                        <br />
                                                        <asp:Label ID="lblFlagDischargeDate" runat="server"  Visible='<%# Eval("FlagDischarged").ToString().ToLower() != "01/01/0001 00:00:00" && Eval("isShowDate ").ToString().ToLower() == "0" %>' Text='<%# DateTime.Parse(Eval("FlagDischarged").ToString()).ToString("HH:mm") %>'></asp:Label>
                                                        <asp:Label ID="lblFlagDischargeDate1" runat="server" Visible='<%# Eval("FlagDischarged").ToString().ToLower() != "01/01/0001 00:00:00" && Eval("isShowDate").ToString().ToLower() == "1" %>' Text='<%# DateTime.Parse(Eval("FlagDischarged").ToString()).ToString("dd/MM/y HH:mm") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </ItemTemplate>
                            </asp:Repeater>

                            
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>

    <asp:UpdatePanel runat="server" ID="upSudahBilling">
        <ContentTemplate>
            <div class="supreme-container" id="divDischargeDone" runat="server">
                <div class="row" style="padding-top: 10px">
                    <div class="col-lg-12">
                        <div class="modal-dialog mini-dialog" style="background-color:#EEEEEE">
                            <div class="modal-header mini-header" style="background-color: #EEEEEE;padding-top:8px; border-bottom: none;">
                                <div class="col-lg-4" style="padding-left: 0px; padding-right:0px;">
                                    <asp:HiddenField runat="server" ID="flagdischargedone" />
                                    <asp:Label ID="lblheaderdone" Font-Size="25px" runat="server" Text="Sudah Billing"></asp:Label>
                                    <asp:Label runat="server" ID="lblCountDone" Font-Size="25px" Font-Bold="false" Text="0"></asp:Label>
                                    
                                    <br />
                                    <div class="has-feedback" style="display: none;">
                                        <span style="margin-top: -9px;" class="fa fa-search form-control-feedback"></span>
                                        <asp:TextBox runat="server" ID="txtdatedoneto2" CssClass="txtdate" Style="border:none" name="date" placeholder="dd/mm/yyyy" onMouseDown="datedoneto();" AutoPostBack="true" OnTextChanged="DateDone_OnChange"  />
                                    </div>
                                    <asp:LinkButton runat="server" CssClass="linkhover" ID="lnkrefreshdone" Style="cursor:pointer;" OnClick="BtnRefreshDone_OnClick" ><i class="fa fa-refresh"></i>
                                        <label style="cursor:pointer">Terakhir Diperbarui: </label>
                                        <asp:Label runat="server" ID="lblDateRefreshDone"></asp:Label>
                                    </asp:LinkButton>
                                </div>
                                <div class="col-lg-8" style="margin-top:13px; text-align:right;padding-left:8%">

                                    <table border="0">
                                        <tr>
                                            <td style="width:20%"> <label style="vertical-align:middle; font-size:12px;">FILTER: </label></td>
                                            <td style="width:20%"><asp:DropDownList Style="background-color:white" Font-Size="12px" Font-Names="Helvetica" AutoPostBack="true" ID="ddlWardListDone" runat="server" OnSelectedIndexChanged="DDLWardListDone_onSelectedIndexChanged" CssClass="selecpicker greyItem" data-live-search="true" data-size="7" data-width="132px;" data-height="12px" data-dropup-auto="false"></asp:DropDownList></td>
                                            <td style="margin-left:20px; width:20%">
                                                <div class="has-feedback" style="display: inline; ">
                                                    <asp:TextBox AutoCompleteType="Disabled" runat="server" placeholder="Tgl Invoice" CssClass="underlined textboxempty" Style="outline-color: transparent;width: 100%; resize: none" Font-Names="Helvetica, Arial, sans-serif" ID="txtdatedoneto" onMouseDown="datedoneto();" AutoPostBack="true" OnTextChanged="DateDone_OnChange" />
                                                    <span style="margin-top: -8px; margin-left: -20px;">
                                                        <asp:LinkButton id="btnShowClear" visible="false"  runat="server"  OnClientClick="clearTxtSearchDateDone();" OnClick="DateDone_OnChange" >
                                                            <i class="fa fa-times-circle"></i>
                                                        </asp:LinkButton>
                                                    </span>
                                                    <span id="btnShowCalendar" visible="false" runat="server" style="margin-top: -9px;" class="fa fa-calendar form-control-feedback"></span>
                                                    <asp:UpdatePanel runat="server" ID="UpdatePanel4">
	                                                    <ContentTemplate>
		                                                    <asp:Button Style="display: none;" ID="btnSearchDatePatientDone"  runat="server" Text=""  />
	                                                    </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                </div>
                                            </td>
                                            <td style="width:20%">
                                                <asp:Label ID="Label8" Style="font-size:12px;" runat="server" Text="MR/NAMA PASIEN"></asp:Label>
                                            </td>
                                            <td style="width:20%">
                                                <div class="has-feedback" style="display: inline;">
                                                    <asp:TextBox runat="server" AutoCompleteType="Disabled" placeholder="Cari" CssClass="underlined textboxempty" Style="outline-color: transparent; width: 100%; resize: none" Font-Names="Helvetica, Arial, sans-serif" ID="txtsearchdone" AutoPostBack="true" OnTextChanged="SearchDone_onChange"/>
                                                    <span style="margin-top: -8px; margin-right: 5px; margin-left:-20%; ">
	                                                    <asp:LinkButton Visible="false" ID="btnClearTextFindPatientDone" runat="server" OnClick="clearpatientdone_onClick">
		                                                    <i class="fa fa-times-circle"></i>
	                                                    </asp:LinkButton>
                                                    </span>
                                                    <span id="btnShowsearchdone" runat="server" style="margin-top: -9px;color: #AAB3B9;" class="fa fa-search"></span>
                                                    <asp:UpdatePanel runat="server" ID="UpdatePanel3">
	                                                    <ContentTemplate>
		                                                    <asp:Button Style="display: none;" ID="btnFindPatientDone"  runat="server" Text="" OnClick="SearchDone_onChange" />
	                                                    </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                </div>
                                            </td>
                                        </tr>
                                    </table>
                                </div>                               
                            </div>

                            <div style="text-align:center">
                                <asp:Image runat="server" ID="nodatadone" ImageUrl="~/Images/Icon/ic_nodataBO.svg" Visible="false" style="padding-top:80px;" />
                            </div>

                            <asp:Repeater runat="server" ID="rptDischargeDone" OnItemDataBound="rptDischargeDone_onItemBound">
                                <ItemTemplate>
                                    <asp:HiddenField runat="server" ID="hdnWardId" Value='<%# Bind("WardId") %>' />
                                    <div style="background-color:#107798;margin-left:15px;margin-right:15px;padding:8px;font-weight:bold;border-radius:5px 5px 0px 0px;box-shadow:0px 3px 8px rgba(0, 0, 0, 0.08); position: sticky; top: 4px; z-index: 4;">
                                        <asp:Label runat="server" ID="LblHeader" Style="color:white;font-size:18px;"></asp:Label><asp:Label runat="server" Style="color:white;font-size:18px; font-weight: normal;" ID="lblcountward"></asp:Label>
                                    </div>
                                    <div class="modal-body" style="padding-top: 0px; padding-bottom: 10px" id="Div1" runat="server">
                                        <asp:GridView ID="gvLIstDischargeDone" runat="server" CssClass="table table-hover-worklist shadowtable" BorderColor="Transparent" AutoGenerateColumns="false"
                                            AllowPagging="true" PageSize="8" ShowHeaderWhenEmpty="True" EmptyDataText="No Data" BorderWidth="0" OnRowDataBound="gvLIstDischargeDone_DataBound">
                                            <Columns>
                                                <asp:TemplateField HeaderText="Nama Pasien" HeaderStyle-Font-Size="12px" ItemStyle-Font-Size="14px" HeaderStyle-Width="12%" HeaderStyle-HorizontalAlign="Left" ItemStyle-Width="15%" ItemStyle-HorizontalAlign="Left" >
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="patientName" CssClass="tooltipnotes" data-toggle="tooltip" data-html="true" data-placement="auto bottom" title='<%# Eval("AdditionalNotes").ToString().Replace("\n","</br>") %>' Style="color:#2A3593;font-weight:bold;text-decoration:underline" runat="server"  Visible='<%# Eval("isPrimary").ToString().ToLower() == "true" %>' Text='<%# Bind("PatientName") %>' OnClick="LnkPatientDetailDone_onClick"></asp:LinkButton>
                                                        <asp:Image Style="margin-bottom: 3px;" Height="16px" ImageUrl="~/Images/Icon/icon_notes.svg" runat="server" Visible='<%# Eval("isPrimary").ToString().ToLower() == "true" && Eval("AdditionalNotes").ToString() != ""  %>' />
                                                        <asp:HiddenField ID="lbAdmissionId" runat="server" Value='<%# Bind("admissionId") %>'></asp:HiddenField>
                                                        <asp:HiddenField ID="lbWorklistId" runat="server" Value='<%# Bind("worklistId") %>'></asp:HiddenField>
                                                        <asp:HiddenField ID="lbDoctorId" runat="server" Value='<%# Bind("doctorId") %>'></asp:HiddenField>
                                                        <asp:HiddenField ID="lbPatientId" runat="server" Value='<%# Bind("patientId") %>'></asp:HiddenField>
                                                        <asp:HiddenField ID="hfisPrescription" runat="server" Value='<%# Bind("isPrescription") %>'></asp:HiddenField>
                                                        <asp:HiddenField ID="hfisRetur" runat="server" Value='<%# Bind("isRetur") %>'></asp:HiddenField>
                                                        <asp:HiddenField ID="ProcessId" runat="server" Value ='<%# Bind("ProcessId") %>'></asp:HiddenField>
                                                        <asp:HiddenField ID="ModifiedDate" runat="server" Value ='<%# Bind("ModifiedDate") %>'></asp:HiddenField>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="DPJP" HeaderStyle-Font-Size="12px" ItemStyle-Font-Size="14px" ItemStyle-Width="11%" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" >
                                                    <ItemTemplate>
                                                        <asp:Label ID="NoMR" runat="server" Visible='<%# Eval("isPrimary").ToString().ToLower() == "true" %>' Text='<%# Bind("DoctorName") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Penanggung" HeaderStyle-Font-Size="12px"  ItemStyle-Font-Size="14px" ItemStyle-Width="14%" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" >
                                                    <ItemStyle CssClass="bordertable" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="payerName" Visible='<%# Eval("isPrimary").ToString().ToLower() == "true" %>' runat="server"  Text='<%# Bind("payerName") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField ItemStyle-Width="6%" HeaderStyle-Font-Size="12px" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle">
                                                    <ItemStyle CssClass="bordertable" />
                                                    <HeaderTemplate>
                                                        <center>
                                                            <img src="<%= Page.ResolveClientUrl("~/Images/Icon/icon_discharge_nurse.svg") %>" style="text-align: center; vertical-align: middle;padding-top:10px" />
                                                        </center>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblSubDischargeBedDate" runat="server" Visible='<%# Eval("SubmitDate").ToString().ToLower() != "01/01/0001 00:00:00" && Eval("isShowDate ").ToString().ToLower() == "0" %>' Text='<%# Eval("SubmitDate","{0:HH:mm}").ToString() %>'></asp:Label>
                                                        <asp:Label ID="lblSubDischargeBedDate1" runat="server" Visible='<%# Eval("SubmitDate").ToString().ToLower() != "01/01/0001 00:00:00" && Eval("isShowDate").ToString().ToLower() == "1" %>' Text='<%# Eval("SubmitDate","{0:dd/MM/y HH:mm}").ToString() %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Service" HeaderStyle-Font-Size="12px" ItemStyle-Width="6%" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle" >
                                                    <HeaderTemplate>
                                                        <center>
                                                            <img src="<%= Page.ResolveClientUrl("~/Images/Icon/icon_discharge_clerk.svg") %>" style="text-align: center; vertical-align: middle;padding-top:5px" />
                                                        </center>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblSubDischargeServiceDate" runat="server" Style='<%# Eval("lateservice").ToString().ToLower() == "true" ? "color:#D82C1D" : "color:black" %>' Visible='<%# DateTime.Parse(Eval("SubDateService").ToString()).ToString("dd/MM/yyyy HH:mm:ss") != "01/01/0001 00:00:00" && Eval("isShowDate ").ToString().ToLower() == "0" %>' Text='<%# DateTime.Parse(Eval("SubDateService").ToString()).ToString("HH:mm") %>'></asp:Label>
                                                        <asp:Label ID="lblSubDischargeServiceDate1" runat="server" Style='<%# Eval("lateservice").ToString().ToLower() == "true" ? "color:#D82C1D" : "color:black" %>' Visible='<%# DateTime.Parse(Eval("SubDateService").ToString()).ToString("dd/MM/yyyy HH:mm:ss") != "01/01/0001 00:00:00" && Eval("isShowDate ").ToString().ToLower() == "1" %>' Text='<%# DateTime.Parse(Eval("SubDateService").ToString()).ToString("dd/MM/y HH:mm") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Item" HeaderStyle-Font-Size="12px" ItemStyle-Width="6%" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" >
                                                    <ItemStyle CssClass="bordertable" />
                                                    <HeaderTemplate>
                                                        <center>
                                                            <img src="<%= Page.ResolveClientUrl("~/Images/Icon/icon_discharge_farma.svg") %>" style="text-align: center; vertical-align: middle;padding-top:5px" />
                                                        </center>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblSubDischargeItemDate" runat="server" Style='<%# Eval("lateitem").ToString().ToLower() == "true" ? "color:#D82C1D" : "color:black" %>' Visible='<%# DateTime.Parse(Eval("SubDateItem").ToString()).ToString("dd/MM/yyyy HH:mm:ss") != "01/01/0001 00:00:00" && Eval("isShowDate ").ToString().ToLower() == "0" %>' Text='<%# DateTime.Parse(Eval("SubDateItem").ToString()).ToString("HH:mm") %>'></asp:Label>
                                                        <asp:Label ID="lblSubDischargeItemDate1" runat="server" Style='<%# Eval("lateitem").ToString().ToLower() == "true" ? "color:#D82C1D" : "color:black" %>' Visible='<%# DateTime.Parse(Eval("SubDateItem").ToString()).ToString("dd/MM/yyyy HH:mm:ss") != "01/01/0001 00:00:00" && Eval("isShowDate ").ToString().ToLower() == "1" %>' Text='<%# DateTime.Parse(Eval("SubDateItem").ToString()).ToString("dd/MM/y HH:mm") %>'></asp:Label>
                                                        <%--<asp:Label ID="lblTidakPerlu" runat="server" Visible='<%# Eval("IsNeedPrescription").ToString().ToLower() == "false" %>' Text="Tidak Perlu"></asp:Label>--%>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Final Discharge" HeaderStyle-Font-Size="12px" ItemStyle-Width="6%" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" >
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblFinalDischargeDate" runat="server" Visible='<%# DateTime.Parse(Eval("FinalDate").ToString()).ToString("dd/MM/yyyy HH:mm:ss") != "01/01/0001 00:00:00" && Eval("isShowDate ").ToString().ToLower() == "0" %>' Text='<%# DateTime.Parse(Eval("FinalDate").ToString()).ToString("HH:mm") %>'></asp:Label>
                                                        <asp:Label ID="lblFinalDischargeDate1" runat="server" Visible='<%# DateTime.Parse(Eval("FinalDate").ToString()).ToString("dd/MM/yyyy HH:mm:ss") != "01/01/0001 00:00:00" && Eval("isShowDate ").ToString().ToLower() == "1" %>' Text='<%# DateTime.Parse(Eval("FinalDate").ToString()).ToString("dd/MM/y HH:mm") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Email Ke Asuransi" HeaderStyle-Font-Size="12px" ItemStyle-Width="6%" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" >
                                                    <ItemTemplate>
                                                        <asp:Label ID="Lblnoemail" runat="server" Visible='<%# Eval("payerName").ToString().ToLower() == "private" %>' Text="-"></asp:Label>
                                                        <asp:Label ID="lblEmailDate" runat="server" Style='<%# Eval("lateemail").ToString().ToLower() == "true" ? "color:#D82C1D" : "color:black" %>' Visible='<%# DateTime.Parse(Eval("EmailDate").ToString()).ToString("dd/MM/yyyy HH:mm:ss") != "01/01/0001 00:00:00" && Eval("isShowDate ").ToString().ToLower() == "0" %>' Text='<%# DateTime.Parse(Eval("EmailDate").ToString()).ToString("HH:mm") %>'></asp:Label>
                                                        <asp:Label ID="lblEmailDate1" runat="server" Style='<%# Eval("lateemail").ToString().ToLower() == "true" ? "color:#D82C1D" : "color:black" %>' Visible='<%# DateTime.Parse(Eval("EmailDate").ToString()).ToString("dd/MM/yyyy HH:mm:ss") != "01/01/0001 00:00:00" && Eval("isShowDate ").ToString().ToLower() == "1" %>' Text='<%# DateTime.Parse(Eval("EmailDate").ToString()).ToString("dd/MM/y HH:mm") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Dijamin Asuransi" HeaderStyle-Font-Size="12px" ItemStyle-Width="6%" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" >
                                                    <ItemTemplate>
                                                        <asp:Label ID="Lblnoconfirm" runat="server" Visible='<%# Eval("payerName").ToString().ToLower() == "private" %>' Text="-"></asp:Label>
                                                        <asp:Label ID="lblConfirmDate" runat="server" Style='<%# Eval("lateconfirm").ToString().ToLower() == "true" ? "color:#D82C1D" : "color:black" %>' Visible='<%# Eval("payerName").ToString().ToLower() != "private" && DateTime.Parse(Eval("ConfirmDate").ToString()).ToString("dd/MM/yyyy HH:mm:ss") != "01/01/0001 00:00:00" && Eval("isShowDate ").ToString().ToLower() == "0" %>' Text='<%# DateTime.Parse(Eval("ConfirmDate").ToString()).ToString("HH:mm") %>'></asp:Label>
                                                        <asp:Label ID="lblConfirmDate1" runat="server" Style='<%# Eval("lateconfirm").ToString().ToLower() == "true" ? "color:#D82C1D" : "color:black" %>' Visible='<%# Eval("payerName").ToString().ToLower() != "private" && DateTime.Parse(Eval("ConfirmDate").ToString()).ToString("dd/MM/yyyy HH:mm:ss") != "01/01/0001 00:00:00" && Eval("isShowDate ").ToString().ToLower() == "1" %>' Text='<%# DateTime.Parse(Eval("ConfirmDate").ToString()).ToString("dd/MM/y HH:mm") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Invoice Selesai" HeaderStyle-Font-Size="12px" ItemStyle-Width="6%" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" >
                                                    <ItemStyle CssClass="bordertable" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblInvoiceDate" runat="server" Style='<%# Eval("lateinvoice").ToString().ToLower() == "true" ? "color:#D82C1D" : "color:black" %>' Visible='<%# DateTime.Parse(Eval("InvoiceDate").ToString()).ToString("dd/MM/yyyy HH:mm:ss") != "01/01/0001 00:00:00" && Eval("isShowDate ").ToString().ToLower() == "0" %>' Text='<%# DateTime.Parse(Eval("InvoiceDate").ToString()).ToString("HH:mm") %>'></asp:Label>
                                                        <asp:Label ID="lblInvoiceDate1" runat="server" Style='<%# Eval("lateinvoice").ToString().ToLower() == "true" ? "color:#D82C1D" : "color:black" %>' Visible='<%# DateTime.Parse(Eval("InvoiceDate").ToString()).ToString("dd/MM/yyyy HH:mm:ss") != "01/01/0001 00:00:00" && Eval("isShowDate ").ToString().ToLower() == "1" %>' Text='<%# DateTime.Parse(Eval("InvoiceDate").ToString()).ToString("dd/MM/y HH:mm") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Lama Kerja" HeaderStyle-Font-Size="12px" ItemStyle-Width="6%" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" >
                                                    <ItemStyle CssClass="bordertable" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblDuration" runat="server" Font-Bold="true" Style='<%# Eval("latetotal").ToString().ToLower() == "true" ? "color:#D82C1D" : "color:#4D9B35" %>' Text='<%# Eval("Duration").ToString() %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Boleh Pulang" HeaderStyle-Font-Size="12px" ItemStyle-Width="7%" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" >
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblFlagDischargeDate" runat="server"  Visible='<%# Eval("FlagDischarged").ToString().ToLower() != "01/01/0001 00:00:00" && Eval("isShowDate ").ToString().ToLower() == "0" %>' Text='<%# DateTime.Parse(Eval("FlagDischarged").ToString()).ToString("HH:mm")%>'></asp:Label>
                                                        <asp:Label ID="lblFlagDischargeDate1" runat="server" Visible='<%# Eval("FlagDischarged").ToString().ToLower() != "01/01/0001 00:00:00" && Eval("isShowDate").ToString().ToLower() == "1" %>' Text='<%# DateTime.Parse(Eval("FlagDischarged").ToString()).ToString("dd/MM/y HH:mm")%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </ItemTemplate>
                            </asp:Repeater>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>

    <div id="btnGoToTopShow" class="bottomMenuu hidee"  style="text-align: right; padding-right: 15px;">
        <%--<asp:ImageButton ID="test"  ImageUrl="~/Images/Icon/button_grey.svg" onmouseover="this.src='Images/Icon/button_btt.svg';" onmouseout="this.src='Images/Icon/button_grey.svg';" runat="server" />--%>

        <%--<img CssClass="RightArrow" ID="Image2" ImageUrl="~/Images/Icon/button_grey.svg" onmouseover="mouseOverImage();return false;"  onmouseout="mouseOutImage();return false;" />--%>
        <asp:ImageButton ImageUrl="~/Images/Icon/button_btt.svg" CssClass="btnTop" runat="server" onClientClick="topFunction(); return false;" />
    </div>

    <%--==================modal detail=====================--%>
    <div class="modal fade" id="modalDetail" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
        <div class="modal-dialog" style="width: 43%;">
            <div class="modal-content" style="border-radius: 10px;">
                <div class="modal-body" style="padding:20px">
                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                        <ContentTemplate>
                                
                    <div class="row" style="margin-left: 0px; margin-right: 0px;">
                        <div class="col-sm-11" style="padding-left: 0px; border: none">
                            <asp:Label Style="font-size: 25px; font-family: Helvetica, Arial, sans-serif; font-weight: bold;" runat="server" ID="lblName"></asp:Label>
                            <asp:Image Style="margin-bottom: 10px;margin-left: 8px;margin-right: 8px;" ImageUrl="~/Images/Icon/icon_dot.svg" runat="server"/>
                            <asp:Label Style="font-size: 25px; font-family: Helvetica, Arial, sans-serif; font-weight: bold;" runat="server" ID="lblSexId"></asp:Label>
                            <asp:Image Style="margin-bottom: 10px;margin-left: 8px;margin-right: 8px;" ImageUrl="~/Images/Icon/icon_dot.svg" runat="server"/>
                            <asp:Label Style="font-size: 25px; font-family: Helvetica, Arial, sans-serif; font-weight: bold;" runat="server" ID="lblMrNo"></asp:Label>
                        </div>
                        <div class="col-sm-1" style="padding-right:5px;">
                            <button type="button" class="close" style="font-size:30px;font-weight:normal" data-dismiss="modal" aria-hidden="true">&times;</button>
                        </div>
                        <%--<div style="padding-left: 0px; border: none">
                            <asp:Label style="font-size: 14px; font-family: Helvetica, Arial, sans-serif; font-weight: bold;" runat="server" id="lblSexId"></asp:Label>
                        </div>
                        <div class="col-sm-4" style="padding-left: 0px; border: none">
                            <asp:Label style="font-size: 14px; font-family: Helvetica, Arial, sans-serif; font-weight: bold;" runat="server" id="lblMrNo"></asp:Label>
                        </div>--%>
                    </div>
                    <div class="row margin-detail">
                        <div class="col-sm-3" style="padding-left: 0px; border: none">
                            <label style="font-size: 12px; font-family: Helvetica, Arial, sans-serif; color: #AAB3B9; font-weight: bold;">No. Admisi</label>
                            <br />
                            <asp:Label Style="font-size: 14px;" runat="server" ID="lblAdmissionNoDetail" ></asp:Label>
                        </div>
                        <div class="col-sm-3" style="padding-left: 0px; border: none">
                            <label style="font-size: 12px;  color: #AAB3B9; font-weight: bold;">Kamar</label>
                            <br />
                            <asp:Label Style="font-size: 14px;" runat="server" ID="lblRoom"></asp:Label>
                        </div>
                        <div class="col-sm-3" style="12px; padding-left: 0px; border: none">
                            <label style="font-size: 14px;  color: #AAB3B9; font-weight: bold;">Usia</label>
                            <br />
                            <asp:Label Style="font-size: 14px;" runat="server" ID="lblAge"></asp:Label>
                        </div>
                        <div class="col-sm-3" style="12px; padding-left: 0px; border: none">
                            <label style="font-size: 14px; color: #AAB3B9; font-weight: bold;">Tgl Lahir</label>
                            <br />
                            <asp:Label Style="font-size: 14px;" runat="server" ID="lblDoB"></asp:Label>
                        </div>
                    </div>
                    <div class="row margin-detail">
                        <div class="col-sm-3" style="padding-left: 0px; border: none">
                            <label style="font-size: 12px; color: #AAB3B9; font-weight: bold;">Rencana Plg</label>
                            <br />
                            <asp:Label Style="font-size: 14px;" runat="server" ID="lblWorklistDate"></asp:Label>,
                            <asp:Label Style="font-size: 14px;" runat="server" ID="lblWorklistTime"></asp:Label>
                        </div>
                        <div class="col-sm-6" style="padding-left: 0px; border: none">
                            <label style="font-size: 12px; font-family: Helvetica, Arial, sans-serif; color: #AAB3B9; font-weight: bold;">Penanggung</label>
                            <br />
                            <asp:Label Style="font-size: 14px;" runat="server" ID="lblPayerName"></asp:Label>
                        </div>
                        <div class="col-sm-3" style="padding-left: 0px; border: none">
                            <label style="font-size: 12px; font-family: Helvetica, Arial, sans-serif; color: #AAB3B9; font-weight: bold;">Kontak Darurat </label><label style="color:#E21100;font-weight:bold">!</label>
                            <br />
                            <asp:Label Style="font-size: 14px;color: #2A3593;font-weight: bold;text-decoration: underline;" runat="server" ID="lblMobileNo" ></asp:Label>
                        </div>
                    </div>
                    <div class="row margin-detail">
                        <div class="col-sm-12" style="padding-left: 0px; border-bottom:solid 1px #CDCED9">
                            <label style="font-size: 12px; font-family: Helvetica, Arial, sans-serif; color: #AAB3B9; font-weight: bold;">DPJP</label>
                        </div>
                    </div>
                    <div class="row margin-detailcontent">
                        <div class="col-sm-7" style="padding-left: 0px; border: none">
                            <asp:Label Style="font-size: 14px; font-family: Helvetica, Arial, sans-serif; font-weight: bold;" runat="server" ID="lblPrimaryDoctorName"></asp:Label>
                        </div>
                        <div class="col-sm-5" style="padding-left: 0px; border: none">
                            <asp:Image ID="imgWaitStatus" Style="text-align: center" Width="20px" Height="20px" runat="server" />
                            <asp:Label Style="font-size: 14px; " runat="server" ID="lblWaitStatus"></asp:Label>
                            <asp:Label Style="font-size: 14px; word-break:break-word; padding-left:25px" runat="server" ID="lblRemarks"></asp:Label>
                            <br />
                            <asp:Image ID="imgPrescription" Style="text-align: center" Width="20px" Height="20px" runat="server" />
                            <asp:Label Style="font-size: 14px;  " runat="server" ID="lblPrescription"></asp:Label>
                            <br />
                            <asp:Image ID="imgRetur" Style="text-align: center" Width="20px" Height="20px" runat="server" />
                            <asp:Label Style="font-size: 14px; font-family: Helvetica, Arial, sans-serif; " runat="server" ID="lblRetur"></asp:Label>
                        </div>
                    </div>
                    <div class="row margin-detail">
                        <div class="col-sm-12" style="padding-left: 0px; border-bottom:solid 1px #CDCED9">
                            <label style="font-size: 12px; font-family: Helvetica, Arial, sans-serif; color: #AAB3B9; font-weight: bold;">Konsulen</label>
                        </div>
                    </div>

                    <div class="row margin-detailcontent">
                        <asp:Label ID="llblNoKonsulen" Visible="false" Style="font-style:italic;font-size: 12px;color:#AAB3B9" runat="server" Text="Tidak Ada Dokter Konsulen"></asp:Label>
                        <asp:Repeater runat="server" ID="repeaterDoctorContultation">
                            <ItemTemplate>
                                <div class="row" style="margin-left: 0px; margin-right: 0px;padding-bottom: 16px;">
                                    <div class="col-sm-7" style="padding-left: 0px; border: none">
                                        <asp:Label ID="lblDoctorName" Style="font-weight:bold;font-size: 14px;" runat="server" Text='<%# Eval("DoctorName") %>'></asp:Label>
                                    </div>
                                    <div class="col-sm-5" style="padding-left: 0px; border: none">
                                        <%--'<%# Eval("isPrescription").ToString().ToLower() == "true"  %>'--%>
                                        <asp:Image ID="imgwaitDone" Visible='<%# Eval("isPrimary").ToString().ToLower() == "false" && Eval("VisitValue").ToString().ToLower() == "done"  %>' Style="text-align: center" Width="20px" Height="20px" ImageUrl="~/Images/Icon/icon_checked_locked.svg" runat="server" />
                                        <asp:Image ID="imgwaitPending" Visible='<%# Eval("isPrimary").ToString().ToLower() == "false"  && Eval("VisitValue").ToString().ToLower() == "pending"  %>' Style="text-align: center" Width="20px" Height="20px" ImageUrl="~/Images/Icon/icon_unchecked.svg" runat="server" />
                                        <%--<asp:Image ID="Image2" Visible='<%# Eval("IsDoctorVisit").ToString().ToLower() == "true" && Eval("VisitValue").ToString().ToLower() == "done" && Eval("WaitStatus").ToString().ToLower() == "tu" %>' Style="text-align: center" Width="20px" Height="20px" ImageUrl="~/Images/Icon/icon_checked_locked.svg" runat="server" />
                                        <asp:Image ID="Image3" Visible='<%# Eval("IsDoctorVisit").ToString().ToLower() == "true"  && Eval("VisitValue").ToString().ToLower() == "pending" && Eval("WaitStatus").ToString().ToLower() == "tu" %>' Style="text-align: center" Width="20px" Height="20px" ImageUrl="~/Images/Icon/icon_unchecked.svg" runat="server" />--%>
                                        <asp:Image ID="imgnowait" Visible='<%# Eval("IsDoctorVisit").ToString().ToLower() == "false" && Eval("WaitStatus").ToString().ToLower() == "tutd" %>' Style="text-align: center" Width="20px" Height="20px" ImageUrl="~/Images/Icon/icon_uncheckedcross.svg" runat="server" />
                                        <asp:Label ID="lblstatus" Text='<%# Eval("WaitStatus").ToString().ToLower() == "td" ? "Tunggu Dokter" : "Tidak tunggu dokter"  %>' Style="font-size: 14px; font-family: Helvetica, Arial, sans-serif;" runat="server"></asp:Label>
                                        <asp:Label ID="lblstatusremarks" Visible='<%# Eval("WaitStatus").ToString().ToLower() == "tu" %>' Text='<%# "<br />tunggu hasil "+Eval("Remarks") %>' Style="font-size: 14px; font-family: Helvetica, Arial, sans-serif; word-break:break-word;" runat="server"></asp:Label>
                                        <br />
                                        <asp:Image ID="imgPresDone" Visible='<%# Eval("IsPrescription").ToString().ToLower() == "true" && Eval("PrescriptionValue").ToString().ToLower() == "done" %>' Style="text-align: center" Width="20px" Height="20px" ImageUrl="~/Images/Icon/icon_checked_locked.svg" runat="server" />
                                        <asp:Image ID="imgPresPending" Visible='<%# Eval("IsPrescription").ToString().ToLower() == "true" && Eval("PrescriptionValue").ToString().ToLower() == "pending" %>' Width="20px" Height="20px" ImageUrl="~/Images/Icon/icon_unchecked.svg" runat="server" />
                                        <asp:Image ID="imgNoPres" Visible='<%# Eval("IsPrescription").ToString().ToLower() == "false" %>' Width="20px" Height="20px" ImageUrl="~/Images/Icon/icon_uncheckedcross.svg" runat="server" />
                                        <asp:Label ID="lblPresValue" Text='<%# Eval("IsPrescription").ToString().ToLower() == "true" ? "Resep Pulang" : "Tidak ada resep pulang" %>' Style="font-size: 14px; font-family: Helvetica, Arial, sans-serif;" runat="server"></asp:Label>
                                        
                                        <%-- <br />
                                        <asp:Image ID="imgReturDone" Visible='<%# Eval("ReturValue").ToString().ToLower() == "done" %>' Style="text-align: center" Width="20px" Height="20px" ImageUrl="~/Images/Icon/icon_checked_locked.svg" runat="server" />
                                        <asp:Image ID="imgReturPending" Visible='<%# Eval("ReturValue").ToString().ToLower() == "pending" %>' Width="20px" Height="20px" ImageUrl="~/Images/Icon/icon_0_Check.svg" runat="server" />
                                        <asp:Label Text='<%# Eval("IsRetur").ToString().ToLower() == "true" ? "Resep Pulang" : "" %>' Style="font-size: 14px; font-family: Helvetica, Arial, sans-serif;" runat="server"></asp:Label>--%>
                                    </div>
                                </div>
                            </ItemTemplate>
                        </asp:Repeater>
                    </div>
                    <div class="row margin-detail">
                        <div class="col-sm-12" style="padding-left: 0px; border-bottom:solid 1px #CDCED9">
                            <label style="font-size: 12px; font-family: Helvetica, Arial, sans-serif; color: #AAB3B9; font-weight: bold;">Catatan Tambahan</label>
                        </div>
                    </div>
                    <div class="row margin-detailcontent" >
                        <div class="col-sm-12" style="padding-left: 0px; border: none">
                            <asp:Label Style="font-size: 14px; word-break:break-word;" runat="server" ID="lblAdditionalNotes"></asp:Label>
                        </div>
                    </div>

                    <div class="row margin-detail" style="background-color: #090F4A;margin-left:-20px;margin-right:-20px; margin-bottom: -20px; border-bottom-left-radius: 10px; border-bottom-right-radius: 10px;">
                        <div class="col-sm-4" style="border: none; padding:15px;">
                            <label class="lbldetailfooter">Dibuat Oleh</label>
                            <br />
                            <asp:Label runat="server" Style="color:white" ID="lblCreatedBy"></asp:Label>
                            <br />
                            <asp:Label runat="server" Style="color:white" ID="lblCreatedDate"></asp:Label>
                        </div>
                        <div class="col-sm-4" style="padding-left: 0px; border: none; padding:15px;">
                            <label class="lbldetailfooter">Perubahan Terakhir</label>
                            <br />
                            <asp:Label runat="server" Style="color:white" ID="lblUpdatedBy"></asp:Label>
                            <br />
                            <asp:Label runat="server" Style="color:white" ID="lblUpdatedDate"></asp:Label>
                        </div>
                        <div class="col-sm-4" style="padding-left: 0px; border: none; padding:15px;">
                            <label class="lbldetailfooter">Diselesaikan Oleh</label>
                            <br />
                            <asp:Label runat="server" Style="color:white" ID="lblSubmitBy"></asp:Label>
                            <br />
                            <asp:Label runat="server" Style="color:white" ID="lblSubmitDate"></asp:Label>
                        </div>
                    </div>
                    </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
        </div>
    </div>




    <%--========================modal lab result================--%>
    <div class="modal fade" id="laboratoryResult" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
        <asp:UpdatePanel runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <div class="modal-dialog" style="width: 70%;" runat="server">
                    <div class="modal-content" style="border-radius: 7px; height: 100%;">
                        <div class="modal-header" style="height: 40px; padding-top: 10px; padding-bottom: 5px">
                            <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                            <h4 class="modal-title" style="text-align: left">
                                <asp:Label ID="Label1" Style="font-weight: bold" runat="server" Text="Laboratory Result"></asp:Label></h4>
                        </div>

                        <div class="modal-body">
                            <div style="width: 100%" class="btn-group" role="group">
                                <asp:UpdatePanel runat="server">
                                    <ContentTemplate>
                                        <%--<asp:HiddenField ID="HFisBahasaPC" runat="server" />--%>
                                        <div class="row">
                                            <div class="col-sm-2" style="padding-top: 10px; padding-right: 0px; width: 100px; margin-left: 5px;">
                                                <asp:Image runat="server" ID="Image1" Width="54px" Height="54px" Style="vertical-align: bottom;" />
                                                <asp:Image runat="server" ID="imgSex" Width="25px" Style="margin-left: -15px;" />
                                            </div>
                                            <div class="col-sm-10" style="padding-left: 0px; width: 86%;">
                                                <div class="input-group">
                                                    <h5>
                                                        <asp:Label ID="patientName" class="form-group" runat="server" Font-Bold="true" Font-Size="13px"></asp:Label>
                                                        <asp:Label ID="Label3" class="form-group" runat="server" Font-Bold="true" ForeColor="LightGray" Font-Size="14px">&nbsp;|&nbsp; </asp:Label>
                                                        <asp:Label ID="localMrNo" class="form-group" runat="server" Font-Bold="true" Font-Size="12px"></asp:Label>
                                                        <asp:Label ID="Label4" class="form-group" runat="server" Font-Bold="true" ForeColor="LightGray" Font-Size="14px">&nbsp;|&nbsp;</asp:Label>
                                                        <asp:Label ID="primaryDoctor" class="form-group" runat="server" Font-Bold="true" Font-Size="12px"></asp:Label>
                                                    </h5>
                                                </div>
                                                <div role="group" style="width: 100%" aria-label="...">
                                                    <div class="btn-group" role="group" style="width: 115px; vertical-align: top">
                                                        <asp:Label CssClass="form-group" runat="server">
                                                            <label id="lblbhs_pcadmissionno"> No. Admisi  </label>
                                                        </asp:Label><br />
                                                        <asp:Label CssClass="form-group" Font-Bold="true" runat="server" ID="lblAdmissionNo"></asp:Label>
                                                    </div>
                                                    <div class="btn-group" role="group" style="width: 95px; vertical-align: top">
                                                        <asp:Label CssClass="form-group" runat="server" ToolTip="Date of Birth">
                                                            <%--<label style="display: <%=setENG%>;"> DOB </label>
                                                            <label style="display: <%=setIND%>;"> Tgl. Lahir </label>--%>
                                                            <label id="lblbhs_pcdob"> Tgl Lahir </label>
                                                        </asp:Label><br />
                                                        <asp:Label CssClass="form-group" Font-Bold="true" runat="server" ID="lbltgllahirlab"></asp:Label>
                                                    </div>
                                                    <div class="btn-group" role="group" style="width: 95px; vertical-align: top">
                                                        <asp:Label CssClass="form-group" runat="server">
                                                            <%--<label style="display: <%=setENG%>;"> Age </label>
                                                            <label style="display: <%=setIND%>;"> Umur </label>--%>
                                                            <label id="lblbhs_pcage"> Usia </label>
                                                        </asp:Label><br />
                                                        <asp:Label CssClass="form-group" Font-Bold="true" runat="server" ID="lblUsia"></asp:Label>
                                                    </div>
                                                    <div class="btn-group" role="group" style="width: 95px; vertical-align: top">
                                                        <asp:Label CssClass="form-group" runat="server">
                                                            <%--<label style="display: <%=setENG%>;"> Religion </label>
                                                            <label style="display: <%=setIND%>;"> Agama </label>--%>
                                                            <label id="lblbhs_pcreligion"> Agama </label>
                                                        </asp:Label><br />
                                                        <asp:Label CssClass="form-group" Font-Bold="true" runat="server" ID="lblReligion"></asp:Label>
                                                    </div>
                                                    <div class="btn-group" role="group" style="max-width: 50%; vertical-align: top">
                                                        <asp:Label CssClass="form-group" runat="server">
                                                            <%--<label style="display: <%=setENG%>;"> Payer </label>
                                                            <label style="display: <%=setIND%>;"> Penanggung </label>--%>
                                                            <label id="lblbhs_pcpayer"> Penanggung </label>
                                                        </asp:Label><br />
                                                        <asp:Label CssClass="form-group" Style="font-weight: bold; width: 100%;" runat="server" ID="lblPayer"></asp:Label>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>

                                    </ContentTemplate>
                                </asp:UpdatePanel>

                                &nbsp;
                                <div style="overflow-y: auto; height: 400px;">
                                    <asp:UpdatePanel runat="server">
                                        <ContentTemplate>
                                            <div runat="server" id="panel1"></div>

                                            <%--<div style="margin-top:2%;text-align:center;border-radius:4px; margin-left:1%; margin-right:1%">
                                                <asp:Label runat="server" ID="lblNoData" Text="No Laboratory Data" Visible="true" Font-Names="Helvetica" Font-Size="25px"></asp:Label>
                                            </div>--%>

                                            <div id="img_noData" runat="server" style="text-align: center; display: none;">
                                                <%--<div>
                                                    <img src="<%= Page.ResolveClientUrl("~/Images/Background/ic_noData.svg") %>" style="height: auto; width: 200px; margin-right: 3px; margin-top: 40px" />
                                                </div>--%>
                                                <div runat="server">
                                                    <span>
                                                        <h3 style="font-weight: 700; color: #585A6F">
                                                            <%--<label style="display: <%=setENG%>;">Oops! There is no data </label>
                                                                <label style="display: <%=setIND%>;">Oops! Tidak ada data </label>--%>
                                                            <label id="lblbhs_nodatax">Oops! There is no data </label>
                                                        </h3>
                                                    </span>
                                                    <span style="font-size: 14px; color: #585A6F">
                                                        <%--<label style="display: <%=setENG%>;">Please search another date or parameter </label>
                                                        <label style="display: <%=setIND%>;">Silakan cari tanggal atau parameter lain </label>--%>
                                                    </span>
                                                </div>
                                            </div>
                                            <div id="img_noConnection" runat="server" visible="false" style="text-align: center;">
                                                <img src="<%= Page.ResolveClientUrl("~/Images/Background/ic_noConnection.svg") %>" style="height: auto; width: 200px; margin-right: 3px; margin-top: 40px;" />
                                                <span>
                                                    <h3 style="font-weight: 700; color: #585A6F">No internet connection</h3>
                                                </span>
                                                <span style="font-size: 14px; color: #585A6F">Please check your connection & refresh</span>
                                            </div>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    </div>
        </form>
        </body>
    </html>
