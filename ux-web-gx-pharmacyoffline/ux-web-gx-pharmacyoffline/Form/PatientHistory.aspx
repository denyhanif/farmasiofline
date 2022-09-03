<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PatientHistory.aspx.cs" Inherits="Form_PatientHistory" %>

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
    <!-- Ionicons -->
    <link rel="stylesheet" href="~/Content/ionicons/css/ionicons.css" />
    <!-- Ichek -->
    <link rel="stylesheet" href="~/Content/plugins/iCheck/all.css" />
    <!-- Datepicker -->
    <link rel="stylesheet" href="~/Content/plugins/datepicker/datepicker3.css" />
    <!-- DropDown -->
    <link rel="stylesheet" href="~/Content/plugins/select2/select2.min.css" />
    <!-- Mask -->
    <link rel="stylesheet" href="~/Content/plugins/jasny-bootstrap/css/jasny-bootstrap.min.css" />
    <!-- Theme style -->
    <link rel="stylesheet" href="~/Content/dist/css/AdminLTE.css" />
    <link rel="stylesheet" href="~/Content/dist/css/skins/skin-blue-light.css" />
    <link rel="stylesheet" href="~/Content/Site.css" />

    <link href="favicon.ico" rel="shortcut icon" type="image/x-icon" />

       
    <link href="~/favicon.ico" rel="shortcut icon" type="image/x-icon" /><meta name="viewport" content="width=device-width, initial-scale=1.0" />


        <style type="text/css">
            .btnSave 
            {
                width: 73px;
                height: 24px;
                font-family: Helvetica;
                font-size: 12px;
                font-weight: bold;
                font-style: normal;
                font-stretch: normal;
                line-height: 1.17;
                border-radius: 4px;
                background-color: #4d9b35;
                color:white;
                border:none;
            }

            .btnSave:hover 
            {
                width: 73px;
                height: 24px;
                font-family: Helvetica;
                font-size: 12px;
                font-weight: bold;
                font-style: normal;
                font-stretch: normal;
                line-height: 1.17;
                border-radius: 4px;
                background-color: #42852e;
                color:white;
                border:none;
            }
    </style>
</head>




<body style="padding-top:15px;">
    <form id="form1" runat="server">
        <script type="text/javascript">
            function printpreview() {
                window.print();
                
                return false;
            }

            function modalLaboratory(AdmId)
            {
                $('#laboratoryResult').modal('show');
            }

            function dateStart_emr()
            {
                var dp = $('#<%=DateTextboxStart_emr.ClientID%>');
                dp.datepicker({
                    changeMonth: true,
                    changeYear: true,
                    format: "dd M yyyy",
                    language: "tr"
                }).on('changeDate', function (ev) {
                    $(this).blur();
                    $(this).datepicker('hide');
                });
            }

            function dateEnd_emr()
            {
                var dp = $('#<%=DateTextboxEnd_emr.ClientID%>');
                dp.datepicker({
                    changeMonth: true,
                    changeYear: true,
                    format: "dd M yyyy",
                    language: "tr"
                }).on('changeDate', function (ev) {
                    $(this).blur();
                    $(this).datepicker('hide');
                });
            }


        </script>
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
                <asp:ScriptReference Path="~/Content/bootstrap-select/js/bootstrap-select.js" />
            </Scripts>
        </asp:ScriptManager>
        
        <div>
            
            <asp:UpdatePanel runat="server">
                <ContentTemplate>
                    <div class="row" style="background-color: #f4f4f4;margin-top:-16px">
                        <div class="col-sm-8" style="padding-top:5px; padding-right:0px; width:100px; margin-left:2%;">
                            <asp:Image runat="server" ID="Image1" Width="54px" Height="54px" Style="vertical-align: bottom;" />
                            <asp:Image runat="server" ID="imgSex" Width="25px" style="margin-left:-15px; " />
                        </div>
                        <div class="col-sm-7" style="padding-left:0px;margin-right:-8%">
                            <div class="input-group">
                                <h5>
                                    <asp:Label ID="patientName" class="form-group" runat="server" Font-Bold="true" Font-Size="24px" ForeColor="#2a3593"></asp:Label>
                                </h5>
                            </div>
                            <div class="btn-group btn-group-justified" role="group" style="width: 95%;font-family:Arial, Helvetica, sans-serif;font-size:12px" aria-label="...">
                                <div class="btn-group" role="group" style="width: 14%; vertical-align: top">
                                    <asp:Label CssClass="form-group" runat="server">MR:</asp:Label>
                                    <asp:Label CssClass="form-group" runat="server" ID="localMrNo"></asp:Label>
                                    &nbsp;
                                    <asp:Label CssClass="form-group" runat="server">/</asp:Label>
                                    &nbsp;
                                    <asp:Label CssClass="form-group" runat="server">DOB:</asp:Label>
                                    <asp:Label CssClass="form-group" runat="server" ID="lblDOB"></asp:Label>
                                    &nbsp;
                                    <asp:Label CssClass="form-group" runat="server">/</asp:Label>
                                    &nbsp;
                                    <asp:Label CssClass="form-group" runat="server">Age:</asp:Label>
                                    <asp:Label CssClass="form-group" runat="server" ID="lblAge"></asp:Label>
                                    &nbsp;
                                    <asp:Label CssClass="form-group" runat="server">/</asp:Label>
                                    &nbsp;
                                    <asp:Label CssClass="form-group" runat="server">Religion:</asp:Label>
                                    <asp:Label CssClass="form-group" runat="server" ID="lblReligion"></asp:Label>
                                </div>
                            </div>
                        </div>
                        
                        
                        <div class="col-sm-4" id="src_emr" runat="server" style="padding-right:2%;margin-left:-2%;margin-top:1%;padding-top:7px">
                        <table border="0" style="float:right;">
                            <tr>
                                <td style="width:35px; text-align:center;"> Date </td>
                                <td> <asp:TextBox class="form-control" runat="server" ID="DateTextboxStart_emr" name="date" Style="font-size: 12px" placeholder="Input date..." onmousedown="dateStart_emr();" Height="24px" Width="100px"/> </td>
                                <td style="width:20px; text-align:center;"> to </td>
                                <td> <asp:TextBox class="form-control" runat="server" ID="DateTextboxEnd_emr" name="date" Style="font-size: 12px" placeholder="Input date..." onmousedown="dateEnd_emr();" Height="24px" Width="100px"/> </td>
                                <td style="width:20px; text-align:center;"> &nbsp; </td>
                                <td> <asp:Button ID="btn_search_emr" runat="server" Text="Search" CssClass="btnSave" OnClick="btn_search_Click" /></td>
                            </tr>
                        </table>
                        </div>

                    </div>
                </ContentTemplate>
        </asp:UpdatePanel>
           

            <asp:updatepanel runat="server">
                <ContentTemplate>
                    <div>
                        


                        <asp:UpdatePanel runat="server">
                            <ContentTemplate>
                                    <asp:HiddenField ID="hf_load_encounter" runat="server" Value="0" />
                                    <asp:HiddenField ID="hfPatientId" runat="server" />
                                    <asp:HiddenField ID="hfEncounterId" runat="server" />
                                    <asp:HiddenField ID="hfAdmissionId" runat="server" />
                                    <asp:HiddenField ID="hfPageSoapId" runat="server" />
                                    <asp:HiddenField ID="hfCompoundName" runat="server" />
                                    <asp:HiddenField ID="hfOrgId" runat="server" />
                                    <asp:HiddenField ID="selected_form_filter" runat="server"/>
                                <div id="emr_div" runat="server" style="display: block">
                                    <asp:HiddenField ID="status_dataEmr" runat="server" />
                                    <div id="tblPatientHistory" runat="server"></div>
                                    <div id="img_noData_emr" runat="server" visible="false" style="text-align: center">
                                        <div>
                                            <img src="<%= Page.ResolveClientUrl("~/Images/Background/ic_noData.svg") %>" style="height: auto; width: 200px; margin-right: 3px; margin-top: 40px" />
                                        </div>
                                        <div runat="server" id="no_patient_data">
                                            <span>
                                                <h3 style="font-weight: 700; color: #585A6F">Oops! There is no data</h3>
                                            </span>
                                            <span style="font-size: 14px; color: #585A6F">Please search another date or parameter</span>
                                        </div>
                                    </div>
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>


                            <div class="modal fade" id="laboratoryResult" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
                            <asp:UpdatePanel runat="server">
                                <ContentTemplate>
                                    <div class="modal-dialog" style="width: 70%;" runat="server">
                                        <div class="modal-content" style="border-radius: 7px; height: 100%;">
                                            <div class="modal-header" style="height: 40px; padding-top: 10px; padding-bottom: 5px">
                                                <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                                                <h4 class="modal-title" style="text-align: left">
                                                    <asp:Label ID="lblModalTitle" Style="font-family: Roboto; font-weight: bold" runat="server" Text="Laboratory Result"></asp:Label></h4>
                                            </div>

                                            <div class="modal-body" style="background-color: white;">
                                                <div style="width: 100%" class="btn-group" role="group">
                                                    <div style="overflow-y: scroll; height: 400px;">

                                                        <%--<tag1:StdLabResult runat="server" ID="StdLabResult" />--%>
                                                        <asp:UpdatePanel runat="server">
                                                            <ContentTemplate>
                                                                <div runat="server" ID="panel1"></div>
                                                                <%--<div style="margin-top:2%;text-align:center;border-radius:4px; margin-left:1%; margin-right:1%">
                                                                    <asp:Label runat="server" ID="lblNoData" Text="No Laboratory Data" Visible="true" Font-Names="Helvetica" Font-Size="25px"></asp:Label>
                                                                </div>--%>
                                                                <div id="img_noData" runat="server" visible="false" style="text-align: center;">
                                                                    <div>
                                                                        <img src="<%= Page.ResolveClientUrl("~/Images/Background/ic_noData.svg") %>" style="height:auto;width:200px;margin-right:3px;margin-top:40px" />
                                                                    </div>
                                                                    <div runat="server">
                                                                        <span><h3  style="font-weight:700; color:#585A6F">Oops! There is no data</h3></span>
                                                                        <span style="font-size:14px; color:#585A6F">Please search another date or parameter</span>
                                                                    </div>
                                                                </div>
                                                                <div id="img_noConnection" runat="server" visible="false" style="text-align: center;">
                                                                    <img src="<%= Page.ResolveClientUrl("~/Images/Background/ic_noConnection.svg") %>" style="height:auto;width:200px;margin-right:3px;margin-top:40px;" />
                                                                    <span><h3  style="font-weight:700; color:#585A6F">No internet connection</h3></span>
                                                                    <span style="font-size:14px; color:#585A6F">Please check your connection & refresh</span>
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
                    
                    <div class="text-center">
                        <asp:Button runat="server" ID="btn_load_more" Text="Load More..." CssClass="btn btn-default" OnClick="btn_load_more_Click" />
                    </div>

                </ContentTemplate>
            </asp:updatepanel>
        </div>
    </form>
</body>
</html>


