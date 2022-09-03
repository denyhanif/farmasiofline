<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PharmacyPatientHistory.aspx.cs" Inherits="Form_PharmacyPatientHistory" %>

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
    <link rel="stylesheet" href="~/Content/plugins/datepicker/datepicker3.css" />
    <!-- DropDown -->
    <link rel="stylesheet" href="~/Content/plugins/select2/select2.min.css" />
    <!-- EMR-Doctor -->
    <!-- <link rel="stylesheet" href="Content/EMR-Doctor.css" /> -->
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
<body style="padding-top: 15px;">
    <form id="form1" runat="server">
        <script type="text/javascript">
            function printpreview() {
                window.print();

                return false;
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
            </Scripts>
        </asp:ScriptManager>
        <style type="text/css">
            @media print {
                thead {
                    display: table-header-group;
                }

                tfoot {
                    display: table-footer-group;
                }
            }

            .bordertable {
                border: 1px solid #cdced9;
            }

            tr.spaceUnder > td {
                padding-bottom: 3em;
            }

            #mydiv {
                margin-top: 10%;
                position: fixed;
                bottom: 0px;
                left: 5px
            }

            @media print {
                tr.page-break {
                    display: block;
                    page-break-before: always;
                }
            }

            table {
                page-break-inside: auto
            }

            tr {
                page-break-inside: avoid;
                page-break-after: auto
            }

            thead {
                display: table-header-group
            }

            tfoot {
                display: table-footer-group
            }

            .buttonBlue {
                width: 117px;
                height: 32px;
                border-radius: 4px;
                background-color: #1172f7;
                font-family: Helvetica;
                font-size: 14px;
                font-weight: bold;
                text-align: center;
                color: #ffffff;
                border: none;
            }

                .buttonBlue:hover {
                    width: 117px;
                    height: 32px;
                    border-radius: 4px;
                    background-color: #0d59c1;
                    font-family: Helvetica;
                    font-size: 14px;
                    font-weight: bold;
                    text-align: center;
                    color: #ffffff;
                    border: none;
                }

            .redBox {
                border-radius: 4px;
                border: solid 1px #c43d32;
                background-color: #f5d8d6;
                margin-left: 25px;
                padding-top: 7px;
                padding-left: 7px;
                margin-right: 6%;
                padding-bottom: 7px;
                margin-bottom: 1%;
            }


            .buttonGreen {
                width: 100px;
                height: 26px;
                border-radius: 4px;
                background-color: #4D9B35;
                font-family: Helvetica;
                font-size: 14px;
                font-weight: bold;
                text-align: center;
                color: #ffffff;
                border: none;
            }

                .buttonGreen:hover {
                    width: 100px;
                    height: 26px;
                    border-radius: 4px;
                    background-color: #42852E;
                    font-family: Helvetica;
                    font-size: 14px;
                    font-weight: bold;
                    text-align: center;
                    color: #ffffff;
                    border: none;
                }

            .alertNotif {
                background-color: #f5d8d6;
                color: #c43d32;
                border-radius: 5px 5px;
                border: 2px solid #c43d32;
                padding: 6px;
                font-weight: bold;
                margin-top: 15px;
                text-align: left;
            }
        </style>

        <asp:HiddenField runat="server" ID="hfPrintBY" />
        <asp:HiddenField runat="server" ID="hfOrgID" />
        <asp:HiddenField runat="server" ID="hf_admiss_id" />
        <asp:HiddenField runat="server" ID="hf_patient_id" />
        <asp:HiddenField runat="server" ID="hf_ticket_patient" />
        <asp:HiddenField ID="HiddenLabMark" runat="server" />
        <asp:HiddenField ID="HiddenRadMark" runat="server" />

        <div>
            <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdateMainpanel">
                <ProgressTemplate>
                    <div style="background-color: white; text-align: center; z-index: 5; position: fixed; width: 100%; left: 0px; height: calc(100vh - 30px); border-radius: 6px;">
                        <div class="modal-backdrop" style="background-color: red; opacity: 0; text-align: center">
                        </div>
                        <div style="margin-top: 10%;">
                            <img alt="" height="225px" width="225px" style="background-color: transparent; vertical-align: middle;" class="login-box-body" src="<%= Page.ResolveClientUrl("~/Images/Background/loading-beat.gif") %>" />
                        </div>
                    </div>
                </ProgressTemplate>
            </asp:UpdateProgress>

            <asp:UpdatePanel runat="server" ID="UpdateMainpanel">
                <ContentTemplate>



                    <%--=======================================================CONTENT START===================================================--%>

                    <div>
                        <asp:HiddenField runat="server" ID="hfpreviewpres" />

                        <div class="container-fluid">
                            <div class="row" style="background-color: white">
                                <div class="col-sm-12 text-right" style="min-height: 40px; padding-top: 10px; padding-bottom: 5px; font-size: 12px;">

                                    <div id="div_linkdashboard" style="float:left; display:inline-block; padding-top: 3px;" runat="server" visible="false">
                                    <a href="javascript:DashboardModalSingle()" style="font-weight: bold; font-size: 14px; text-decoration: underline;">Patient Dashboard</a>
                                    </div>

                                    <label>Filter : </label>
                                    <asp:TextBox ID="txtDateFromNew" runat="server" Style="width: 120px; height: 26px;" placeholder="dd mmm yyyy" onmousedown="dateStartNew();" OnTextChanged="txtFromDate_TextChanged" AutoPostBack="true" AutoCompleteType="Disabled"></asp:TextBox>
                                    <label>- </label>
                                    <asp:TextBox ID="txtToDateNew" runat="server" Style="width: 120px; height: 26px;" placeholder="dd mmm yyyy" onmousedown="dateEndNew();" OnTextChanged="txtToDateNew_TextChanged" AutoPostBack="true" AutoCompleteType="Disabled"></asp:TextBox>
                                    <asp:DropDownList ID="dropType" runat="server" Style="margin-left: 15px; min-width: 200px; height: 26px;" OnSelectedIndexChanged="dropType_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                    <asp:DropDownList ID="dropDoctor" runat="server" Style="margin-left: 15px; min-width: 200px; height: 26px;" OnSelectedIndexChanged="btnFilterOnClick" AutoPostBack="true">
                                        <asp:ListItem Text="Dokter" Value="0" />
                                    </asp:DropDownList>
                                    <asp:Button ID="btnFilter" runat="server" Text="Filter" OnClick="btnFilterOnClick" CssClass="buttonGreen" Visible="false" />

                                    <br />

                                    <div class="alertNotif" id="alertNotif" runat="server" visible="false">
                                        <label>Menampilkan maksimum 20 encounter, silahkan ganti range tanggal untuk menampilkan hasil lainnya</label>
                                    </div>

                                </div>
                            </div>

                            <asp:UpdatePanel runat="server">
                                <ContentTemplate>

                                    <div class="row" style="background-color: white">
                                        <div class="col-sm-12" style="min-height: 50px; padding-top: 5px; padding-bottom: 5px;">

                                            <asp:GridView ID="gvw_detail" runat="server" AutoGenerateColumns="False" CssClass="table table-striped table-bordered table-condensed thinscroll"
                                                EmptyDataText="No Data" OnRowDataBound="drugs_data_RowDataBound" Font-Names="Helvetica">
                                                <PagerStyle CssClass="pagination-ys" />
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Admisi" ItemStyle-Width="1%" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Font-Size="13px" HeaderStyle-Font-Names="Helvetica, Arial, sans-serif" HeaderStyle-Font-Bold="true">
                                                        <ItemTemplate>
                                                            <div>
                                                                <asp:Label Height="20px" Font-Size="11px" Font-Names="Helvetica, Arial, sans-serif" ID="txtTanggal" runat="server" Text='<%# Bind("AdmissionDate") %>' BorderStyle="None" Enabled="false" BackColor="Transparent" Font-Bold="true" Style="font-size: 15px"></asp:Label>
                                                                <br />
                                                                <asp:Label runat="server" ID="lblAdmNo" Font-Size="11px" Font-Names="Helvetica, Arial, sans-serif" Text='<%# Bind("admissionNo") %>' BorderStyle="None" Enabled="false" BackColor="Transparent"></asp:Label>
                                                                <label style="font-family: Arial, Helvetica, sans-serif; font-size: 11px; font-weight: bold">
                                                                    Tanggal dibuat
                                                                </label>
                                                                <br />
                                                                <asp:Label ID="lblCreatedDate" runat="server" Text='<%# Bind("createdDate") %>' Font-Size="11px" Font-Names="Helvetica, Arial, sans-serif"></asp:Label>
                                                                <br />
                                                                <label style="font-family: Arial, Helvetica, sans-serif; font-size: 11px; font-weight: bold">
                                                                    Terakhir diubah
                                                                </label>
                                                                <br />
                                                                <asp:Label ID="lblModifiedDate" runat="server" Text='<%# Bind("modifiedDate") %>' Font-Size="11px" Font-Names="Helvetica, Arial, sans-serif"></asp:Label>
                                                                <br />
                                                            </div>
                                                            <div style="text-align: left; margin-top: 10%">
                                                                <asp:ImageButton ID="labResult" Width="20px" Height="20px" ToolTip="View Laboratory" ImageUrl="~/Images/Icon/labE.png" runat="server" Visible='<%# Eval("IsLab").ToString() != "0" %>' OnClick="labResult_Click" OnClientClick='<%# "openLabResultModal(/" + Eval("EncounterId") + "/ ," + Eval("PatientId") + "," + Eval("AdmissionId") + ")"  %>' />
                                                                <asp:ImageButton Width="20px" Height="20px" ToolTip="No Laboratory" ImageUrl="~/Images/Icon/labD.png" runat="server" Enabled="false" Visible='<%# Eval("IsLab").ToString() != "1" %>' Style="cursor: not-allowed;" />
                                                                &nbsp;
                                                                <asp:ImageButton ID="ImageButton1" Width="20px" Height="20px" ToolTip="View Radiology" ImageUrl="~/Images/Icon/radE.png" runat="server" Visible='<%# Eval("isRad").ToString() != "0" %>' OnClick="radResult_Click" OnClientClick='<%# "openRadResultModal(/" + Eval("EncounterId") + "/ ," + Eval("PatientId") + "," + Eval("AdmissionId") + ")"  %>' />
                                                                <asp:ImageButton Width="20px" Height="20px" ToolTip="No Radiology" ImageUrl="~/Images/Icon/radD.png" runat="server" Enabled="false" Visible='<%# Eval("isRad").ToString() != "1" %>' Style="cursor: not-allowed;" />
                                                                &nbsp;
                                                                <asp:HiddenField ID="hdnOrganization" runat="server" Value='<%# Bind("organizationId") %>' />
                                                                <asp:HiddenField ID="hdnPatientID" runat="server" Value='<%# Bind("patientId") %>' />
                                                                <asp:HiddenField ID="hdnAdmId" runat="server" Value='<%# Bind("admissionId") %>' />
                                                                <asp:HiddenField ID="hdnEncId" runat="server" Value='<%# Bind("encounterId") %>' />
                                                                <asp:HiddenField ID="hdnPageSoap" runat="server" Value='<%# Bind("pageSOAP") %>' />

                                                                <asp:HiddenField ID="hdnPatientName" runat="server" Value='<%# Bind("patientName") %>' />
                                                                <asp:HiddenField ID="hdnOrgCode" runat="server" Value='<%# Bind("organizationCode") %>' />
                                                                <asp:HiddenField ID="hdnTTLumur" runat="server" Value='<%# Bind("birthDate") %>' />
                                                                <asp:HiddenField ID="hdnJK" runat="server" Value='<%# Bind("gender") %>' />
                                                                <asp:HiddenField ID="hdnLocalMrNo" runat="server" Value='<%# Bind("localMrNo") %>' />

                                                                <asp:HiddenField ID="hdnDoctorName" runat="server" Value='<%# Bind("DoctorName") %>' />



                                                                <asp:ImageButton ID="btnPatientHistory" Width="20px" Height="20px" ToolTip="View Full Patient History" ImageUrl="~/Images/Dashboard/ic_History.png" runat="server" OnClick="btnPatientHistory_Click" />
                                                                <br />
                                                                <asp:ImageButton ID="btnPrintSOAP" Width="20px" Height="20px" ToolTip="Print Short SOAP" ImageUrl="~/Images/Dashboard/ic_shortPrint.png" runat="server" OnClick="btnPrintSOAP_Click" />
                                                                &nbsp;
                                                                <asp:ImageButton ID="btnPrintSOAPLong" Width="20px" Height="20px" ToolTip="Print Long SOAP" ImageUrl="~/Images/Dashboard/ic_longPrint.png" runat="server" OnClick="btnPrintSOAPlong_Click" />
                                                                &nbsp;
                                                                <asp:ImageButton ID="btnPrintReferral" Width="20px" Height="20px" ToolTip="Print Referral Letter" ImageUrl="~/Images/Icon/ic-rujukan.svg" runat="server" Visible='<%# Eval("CountReferral").ToString() != "0" %>' OnClick="btnPrintReferral_Click" />
                                                                &nbsp;
                                                                <asp:ImageButton ID="btnPrintRawatInap" Width="20px" Height="20px" ToolTip="Print Rawat Inap" ImageUrl="~/Images/Icon/ic-rawatinap.svg" runat="server" Visible='<%# Eval("CountRawatInap").ToString() != "0" %>' OnClick="btnPrintRawatInap_Click" />
                                                            </div>
                                                            <br />
                                                            <a href="javascript:RevModal('<%# Eval("OrganizationId") %>','<%# Eval("PatientId") %>','<%# Eval("AdmissionId") %>','<%# Eval("EncounterId") %>')" style="font-weight: bold; font-size: 14px; text-decoration: underline;">Revision</a>
                                                            <br />
                                                            <%--<br />
                                                            <a href="javascript:DashboardModal('<%# Eval("OrganizationId") %>','<%# Eval("PatientId") %>','<%# Eval("AdmissionId") %>','<%# Eval("EncounterId") %>','<%# Eval("DoctorId") %>')" style="font-weight: bold; font-size: 14px; text-decoration: underline;">Dashboard</a>--%>
                                                            <br />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <%--<asp:TemplateField HeaderText="Dept" ItemStyle-Width="1%" HeaderStyle-Font-Size="11px" HeaderStyle-Font-Names="Helvetica, Arial, sans-serif" HeaderStyle-Font-Bold="true">
                                                <ItemTemplate>
                                                    <asp:Label Width="100%" Height="20px" Font-Size="11px" Font-Names="Helvetica, Arial, sans-serif" ID="txtDept" runat="server" Text='<%# Bind("AdmissionTypeName") %>' BorderStyle="None" Enabled="false" BackColor="Transparent"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>--%>
                                                    <asp:TemplateField HeaderText="Dokter" ItemStyle-Width="3%" HeaderStyle-Font-Size="13px" HeaderStyle-Font-Names="Helvetica, Arial, sans-serif" HeaderStyle-Font-Bold="true">
                                                        <ItemTemplate>
                                                            <asp:Label Width="100%" Height="20px" Font-Size="11px" Font-Names="Helvetica, Arial, sans-serif" ID="txtDokter" runat="server" Text='<%# Bind("DoctorName") %>' BorderStyle="None" Enabled="false" BackColor="Transparent"></asp:Label>
                                                            <br />
                                                            <asp:Label ID="LabelIsTele" runat="server" Text="Teleconsultation" Style="background-color: #cddef4; color: #1172f7; padding: 2px 5px 2px 5px; border-radius: 5px 5px; font-size: 14px;" Visible='<%# Eval("IsTeleconsultation") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="S" ItemStyle-Width="5%" HeaderStyle-Font-Size="13px" HeaderStyle-Font-Names="Helvetica, Arial, sans-serif" HeaderStyle-Font-Bold="true">
                                                        <ItemTemplate>
                                                            <asp:Label Font-Size="11px" Font-Names="Helvetica, Arial, sans-serif" ID="txtsubjective" runat="server" Text='<%# Eval("subjective").ToString().Replace("\n","<br/>").Replace("\\n","<br/>") %>'></asp:Label>
                                                            <%--<asp:TextBox Width="100%" Height="20px" Font-Size="11px" Font-Names="Helvetica, Arial, sans-serif" ID="txtsubjective" runat="server" Text='<%# Bind("subjective") %>' BorderStyle="None" Enabled="false" BackColor="Transparent" TextMode="multiline" Style="resize: none;min-height:240px;max-height:none"></asp:TextBox>--%>
                                                            <%--<%# Eval("subjective") %>--%>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="O" ItemStyle-Width="5%" HeaderStyle-Font-Size="13px" HeaderStyle-Font-Names="Helvetica, Arial, sans-serif" HeaderStyle-Font-Bold="true">
                                                        <ItemTemplate>
                                                            <asp:Label Font-Size="11px" Font-Names="Helvetica, Arial, sans-serif" ID="txtobjective" runat="server" Text='<%# Eval("objective").ToString().Replace("\n","<br/>").Replace("\\n","<br/>") %>'></asp:Label>
                                                            <%--<asp:TextBox Width="100%" Font-Size="11px" Font-Names="Helvetica, Arial, sans-serif" ID="txtobjective" runat="server" Text='<%# Bind("objective") %>' BorderStyle="None" Enabled="false" BackColor="Transparent" TextMode="multiline" Style="resize: none;min-height:240px;max-height:none"></asp:TextBox>--%>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="A" ItemStyle-Width="5%" HeaderStyle-Font-Size="13px" HeaderStyle-Font-Names="Helvetica, Arial, sans-serif" HeaderStyle-Font-Bold="true">
                                                        <ItemTemplate>
                                                            <asp:Label Font-Size="11px" Font-Names="Helvetica, Arial, sans-serif" ID="txtDiagnosa" runat="server" Text='<%# Eval("Diagnosis").ToString().Replace("\n","<br/>") %>'></asp:Label>
                                                            <%--<asp:TextBox Width="100%" Font-Size="11px" Font-Names="Helvetica, Arial, sans-serif" ID="txtDiagnosa" runat="server" Text='<%# Bind("Diagnosis") %>' BorderStyle="None" Enabled="false" BackColor="Transparent" TextMode="multiline" Style="resize: none;min-height:240px;max-height:none"></asp:TextBox>--%>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="P" ItemStyle-Width="5%" HeaderStyle-Font-Size="13px" HeaderStyle-Font-Names="Helvetica, Arial, sans-serif" HeaderStyle-Font-Bold="true">
                                                        <ItemTemplate>
                                                            <asp:Label Font-Size="11px" Font-Names="Helvetica, Arial, sans-serif" ID="txtTindakan" runat="server" Text='<%# Eval("PlanningProcedure").ToString().Replace("\n","<br/>") %>'></asp:Label>
                                                            <%--<asp:TextBox Width="100%" Font-Size="11px" Font-Names="Helvetica, Arial, sans-serif" ID="txtTindakan" runat="server" Text='<%# Bind("PlanningProcedure") %>' BorderStyle="None" Enabled="false" BackColor="Transparent" TextMode="multiline" Style="resize: none;min-height:240px;max-height:none"></asp:TextBox>--%>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Obat & Alkes" ItemStyle-Width="7%" HeaderStyle-Font-Size="13px" HeaderStyle-Font-Names="Helvetica, Arial, sans-serif" HeaderStyle-Font-Bold="true">
                                                        <ItemTemplate>
                                                            <div style='padding-bottom: 5px; display: <%# Eval("IsEditPrescription").ToString().ToLower() == "true" ? "block" : "none" %>'>
                                                                <asp:Label ID="LabelEditedByFarmasi" runat="server" Text="Edited by Pharmacy" Style="background-color: #cddef4; color: #1172f7; padding: 2px 5px 2px 5px; border-radius: 5px 5px; font-size: 14px;"> </asp:Label>
                                                            </div>
                                                            <asp:Label Font-Size="11px" Font-Names="Helvetica, Arial, sans-serif" ID="txtObat" runat="server" Text='<%# Eval("prescription").ToString().Replace("\\n","<br/>") %>'></asp:Label>
                                                            <%--<asp:TextBox Width="100%" Font-Size="11px" Font-Names="Helvetica, Arial, sans-serif" ID="txtObat" runat="server" Text='<%# Bind("prescription") %>' BorderStyle="None" Enabled="false" BackColor="Transparent" TextMode="multiline" Style="resize: none;min-height:240px;max-height:none"></asp:TextBox>--%>
                                                            <%--<%# Eval("prescription") %>--%>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>


                                        </div>
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>

                            <div id="mydiv" style="display: none;">
                                <table>
                                    <tr>
                                        <td>
                                            <asp:Label runat="server" Text="Printed By : " Font-Size="10px"></asp:Label>
                                            <asp:Label runat="server" ID="lblPrintedBy" Font-Size="10px"></asp:Label>
                                            <%--<br />--%>
                                            <asp:Label runat="server" Text="/"></asp:Label>
                                            <asp:Label runat="server" Text="Print Date :" Font-Size="10px"></asp:Label>
                                            <asp:Label runat="server" ID="lblPrintDate" Font-Size="10px"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </div>
                    </div>
                    <%--=======================================================CONTENT END===================================================--%>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>

        <%--        <table style="border-spacing:20px;border-collapse: separate;margin-left:37%;width:62%">
            <tr class="spaceUnder">
                <td colspan="2" style="text-align:center">
                    <asp:Label runat="server" Text="Mengetahui"></asp:Label>
                </td>
            </tr>
            <tr>
               <td align="left" width="65%" style="vertical-align:top">
                    <asp:Label runat="server" ID="lblSuster"  Text="(. . . . . . . . . . . . . . . . . . .)"></asp:Label>
                </td>
                <td align="left" width="35%" style="vertical-align:top">
                    <asp:Label runat="server" ID="lblNamaDokter"  Text="Nama DOkter"></asp:Label>
                </td>
            </tr>
        </table>--%>

        <div class="modal fade" id="laboratoryResult" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">

            <div class="modal-dialog" style="width: 70%;" runat="server">
                <div class="modal-content" style="border-radius: 7px; height: 100%;">
                    <div class="modal-header" style="height: 40px; padding-top: 10px; padding-bottom: 5px">
                        <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                        <h4 class="modal-title" style="text-align: left">
                            <asp:Label ID="lblModalTitle" Style="font-family: Arial, Helvetica, sans-serif;" runat="server" Text="Laboratory Result"></asp:Label></h4>
                    </div>

                    <div class="modal-body">

                        <asp:UpdatePanel runat="server">
                            <ContentTemplate>
                                <div style="width: 100%" class="btn-group" role="group">
                                    <asp:UpdatePanel runat="server">
                                        <ContentTemplate>
                                            <div class="row">
                                                <div class="col-sm-2" style="padding-top: 10px; padding-right: 0px; width: 100px; margin-left: 5px;">
                                                    <asp:Image runat="server" ID="Image1" Width="54px" Height="54px" Style="vertical-align: bottom;" />
                                                    <asp:Image runat="server" ID="imgSex" Width="25px" Style="margin-left: -15px;" />
                                                </div>
                                                <div class="col-sm-10" style="padding-left: 0px; width: 86%;">
                                                    <div class="input-group">
                                                        <h5>
                                                            <asp:Label ID="patientName" class="form-group" runat="server" Font-Bold="true" Font-Size="13px"></asp:Label>
                                                            <asp:Label ID="Label2" class="form-group" runat="server" Font-Bold="true" ForeColor="LightGray" Font-Size="14px">&nbsp;|&nbsp; </asp:Label>
                                                            <asp:Label ID="localMrNo" class="form-group" runat="server" Font-Bold="true" Font-Size="12px"></asp:Label>
                                                            <asp:Label ID="Label4" class="form-group" runat="server" Font-Bold="true" ForeColor="LightGray" Font-Size="14px">&nbsp;|&nbsp;</asp:Label>
                                                            <asp:Label ID="primaryDoctor" class="form-group" runat="server" Font-Bold="true" Font-Size="12px"></asp:Label>
                                                        </h5>
                                                    </div>
                                                    <div role="group" style="width: 100%" aria-label="...">
                                                        <div class="btn-group" role="group" style="width: 115px; vertical-align: top">
                                                            <asp:Label CssClass="form-group" runat="server">
                                                                                <label> Admission No. </label>
                                                            </asp:Label><br />
                                                            <asp:Label CssClass="form-group" Font-Bold="true" runat="server" ID="lblAdmissionNo"></asp:Label>
                                                        </div>
                                                        <div class="btn-group" role="group" style="width: 95px; vertical-align: top">
                                                            <asp:Label CssClass="form-group" runat="server" ToolTip="Date of Birth">
                                                                                <label> DOB </label>
                                                            </asp:Label><br />
                                                            <asp:Label CssClass="form-group" Font-Bold="true" runat="server" ID="lblDOB"></asp:Label>
                                                        </div>
                                                        <div class="btn-group" role="group" style="width: 95px; vertical-align: top">
                                                            <asp:Label CssClass="form-group" runat="server">
                                                                                <label> Age </label>
                                                            </asp:Label><br />
                                                            <asp:Label CssClass="form-group" Font-Bold="true" runat="server" ID="lblAge"></asp:Label>
                                                        </div>
                                                        <div class="btn-group" role="group" style="width: 95px; vertical-align: top">
                                                            <asp:Label CssClass="form-group" runat="server">
                                                                                <label> Religion </label>
                                                            </asp:Label><br />
                                                            <asp:Label CssClass="form-group" Font-Bold="true" runat="server" ID="lblReligion"></asp:Label>
                                                        </div>
                                                        <div class="btn-group" role="group" style="max-width: 50%; vertical-align: top">
                                                            <asp:Label CssClass="form-group" runat="server">
                                                                                <label> Payer </label>
                                                            </asp:Label><br />
                                                            <asp:Label CssClass="form-group" Style="font-weight: bold; width: 100%;" runat="server" ID="lblPayer"></asp:Label>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>

                                    &nbsp;

                                    <div style="overflow-y: scroll; height: 400px;">
                                        <%--<tag1:StdLabResult runat="server" ID="StdLabResult" />--%>
                                        <asp:UpdatePanel runat="server">
                                            <ContentTemplate>
                                                <div runat="server" id="panel1"></div>
                                                <%--<div style="margin-top:2%;text-align:center;border-radius:4px; margin-left:1%; margin-right:1%">
                                                    <asp:Label runat="server" ID="lblNoData" Text="No Laboratory Data" Visible="true" Font-Names="Helvetica" Font-Size="25px"></asp:Label>
                                                </div>--%>
                                                <div id="img_noData" runat="server" visible="false" style="text-align: center;">
                                                    <div>
                                                        <img src="<%= Page.ResolveClientUrl("~/Images/Background/ic_noData.svg") %>" style="height: auto; width: 200px; margin-right: 3px; margin-top: 40px" />
                                                    </div>
                                                    <div runat="server">
                                                        <span>
                                                            <h3 style="font-weight: 700; color: #585A6F">Oops! There is no data</h3>
                                                        </span>
                                                        <span style="font-size: 14px; color: #585A6F">Please search another date or parameter</span>
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

                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </div>
            </div>

        </div>


        <div class="modal fade" id="radiologyResult" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
            <asp:UpdatePanel runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <div class="modal-dialog" style="width: 70%;" runat="server">
                        <div class="modal-content" style="border-radius: 7px; height: 100%;">
                            <div class="modal-header" style="height: 40px; padding-top: 10px; padding-bottom: 5px">
                                <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                                <h4 class="modal-title" style="text-align: left">
                                    <asp:Label ID="Label3" Style="font-weight: bold" runat="server" Text="Radiology Result"></asp:Label></h4>
                            </div>
                            <div class="modal-body" style="background-color: white;">
                                <div style="width: 100%" class="btn-group" role="group">
                                    <asp:UpdatePanel runat="server">
                                        <ContentTemplate>
                                            <div class="row">
                                                <div class="col-sm-2" style="padding-top: 10px; padding-right: 0px; width: 100px; margin-left: 5px;">
                                                    <asp:Image runat="server" ID="Image2" Width="54px" Height="54px" Style="vertical-align: bottom;" />
                                                    <asp:Image runat="server" ID="imgSexRad" Width="25px" Style="margin-left: -15px;" />
                                                </div>
                                                <div class="col-sm-10" style="padding-left: 0px; width: 86%;">
                                                    <div class="input-group">
                                                        <h5>
                                                            <asp:Label ID="patientNameRad" class="form-group" runat="server" Font-Bold="true" Font-Size="13px"></asp:Label>
                                                            <asp:Label ID="Label6" class="form-group" runat="server" Font-Bold="true" ForeColor="LightGray" Font-Size="14px">&nbsp;|&nbsp; </asp:Label>
                                                            <asp:Label ID="localMrNoRad" class="form-group" runat="server" Font-Bold="true" Font-Size="12px"></asp:Label>
                                                            <asp:Label ID="Label8" class="form-group" runat="server" Font-Bold="true" ForeColor="LightGray" Font-Size="14px">&nbsp;|&nbsp;</asp:Label>
                                                            <asp:Label ID="primaryDoctorRad" class="form-group" runat="server" Font-Bold="true" Font-Size="12px"></asp:Label>
                                                        </h5>
                                                    </div>
                                                    <div role="group" style="width: 100%" aria-label="...">
                                                        <div class="btn-group" role="group" style="width: 115px; vertical-align: top">
                                                            <asp:Label CssClass="form-group" runat="server">
                                                                                <label> Admission No. </label>
                                                            </asp:Label><br />
                                                            <asp:Label CssClass="form-group" Font-Bold="true" runat="server" ID="lblAdmissionNoRad"></asp:Label>
                                                        </div>
                                                        <div class="btn-group" role="group" style="width: 95px; vertical-align: top">
                                                            <asp:Label CssClass="form-group" runat="server" ToolTip="Date of Birth">
                                                                                <label> DOB </label>
                                                            </asp:Label><br />
                                                            <asp:Label CssClass="form-group" Font-Bold="true" runat="server" ID="lblDOBRad"></asp:Label>
                                                        </div>
                                                        <div class="btn-group" role="group" style="width: 95px; vertical-align: top">
                                                            <asp:Label CssClass="form-group" runat="server">
                                                                                <label> Age </label>
                                                            </asp:Label><br />
                                                            <asp:Label CssClass="form-group" Font-Bold="true" runat="server" ID="lblAgeRad"></asp:Label>
                                                        </div>
                                                        <div class="btn-group" role="group" style="width: 95px; vertical-align: top">
                                                            <asp:Label CssClass="form-group" runat="server">
                                                                                <label> Religion </label>
                                                            </asp:Label><br />
                                                            <asp:Label CssClass="form-group" Font-Bold="true" runat="server" ID="lblReligionRad"></asp:Label>
                                                        </div>
                                                        <div class="btn-group" role="group" style="max-width: 50%; vertical-align: top">
                                                            <asp:Label CssClass="form-group" runat="server">
                                                                                <label> Payer </label>
                                                            </asp:Label><br />
                                                            <asp:Label CssClass="form-group" Style="font-weight: bold; width: 100%;" runat="server" ID="lblPayerRad"></asp:Label>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>

                                    &nbsp;
                                    
                                    <div style="overflow-y: scroll; height: 400px;">
                                        <%--<tag1:StdLabResult runat="server" ID="StdLabResult" />--%>
                                        <asp:UpdatePanel runat="server">
                                            <ContentTemplate>
                                                <div id="div_Radiology_detail" runat="server"></div>
                                                <div id="img_noDataRad" runat="server" style="text-align: center;">
                                                    <div>
                                                        <img src="<%= Page.ResolveClientUrl("~/Images/Background/ic_noData.svg") %>" style="height: auto; width: 200px; margin-right: 3px; margin-top: 40px" />
                                                    </div>
                                                    <div>
                                                        <span>
                                                            <h3 style="font-weight: 700; color: #585A6F">
                                                                <%--<label style="display: <%=setENG%>;">Oops! There is no data </label>
                                                                            <label style="display: <%=setIND%>;">Oops! Tidak ada data </label>--%>
                                                                <label id="lblbhs_nodata">Oops! There is no data </label>
                                                            </h3>
                                                        </span>
                                                        <span style="font-size: 14px; color: #585A6F">
                                                            <%--<label style="display: <%=setENG%>;">Please search another date or parameter </label>
                                                                        <label style="display: <%=setIND%>;">Silakan cari tanggal atau parameter lain </label>--%>
                                                            <label id="lblbhs_subnodata">Please search another date or parameter </label>
                                                        </span>
                                                    </div>
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


        <asp:UpdatePanel runat="server" ID="updatePatientHistory" UpdateMode="Conditional">
            <ContentTemplate>
                <div id="patientHistoryModal" runat="server" class="modal fade" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
                    <div class="modal-dialog" style="width: 95%;" runat="server">
                        <div class="modal-content" style="border-radius: 7px; height: 100%;">
                            <div class="modal-header" style="height: 40px; padding-top: 10px; padding-bottom: 5px">
                                <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                                <h4 class="modal-title" style="text-align: left">
                                    <asp:Label ID="Label1" Style="font-family: Arial, Helvetica, sans-serif;" runat="server" Text="Patient History"></asp:Label></h4>
                            </div>
                            <div class="modal-body" style="background-color: white; border-radius: 7px">
                                <div style="width: 100%" class="btn-group" role="group">
                                    <iframe name="IframeMedicalResumePatient" id="IframeMedicalResumePatient" runat="server" style="width: 100%; height: 80vh; border: none; margin-top: 0%; overflow-y: scroll; padding-right: 0; padding-left: 0%; margin-left: 0;"></iframe>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>

         <div class="modal fade" id="modalRevision" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
            <div class="modal-dialog" style="width: 70vw;">
                <div class="modal-content" style="border-radius: 7px; height: 85vh;">
                    <asp:UpdatePanel ID="UpdatePanelRevision" runat="server">
                        <ContentTemplate>
                            <div class="modal-header" style="height: 40px; padding-top: 10px; padding-bottom: 5px">
                                <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                                <h4 class="modal-title" style="text-align: left">
                                    <label id="lblbhs_revisionmodal" style="font-family: Helvetica, Arial, sans-serif; font-weight: bold">Revision History </label>
                                </h4>
                                <asp:Button runat="server" ID="btnrevisionModal" Text="" CssClass="hidden" OnClick="btnrevisionModal_Click" />
                                <asp:HiddenField ID="HFrevOrgID" runat="server" />
                                <asp:HiddenField ID="HFrevPtnID" runat="server" />
                                <asp:HiddenField ID="HFrevAdmID" runat="server" />
                                <asp:HiddenField ID="HFrevEncID" runat="server" />
                            </div>
                            <div style="overflow-y: auto; height: 78vh; width: 100%; padding-left: 15px; padding-right: 15px;">
                                <asp:Repeater ID="RepeaterRevisionHeader" runat="server" OnItemDataBound="RepeaterRevisionHeader_ItemDataBound">
                                    <ItemTemplate>

                                        <div class="row">
                                            <div class="col-sm-12" style="background-color:#2a3593; color:white; padding:7px 15px; font-size:14px; font-weight:bold;">
                                                <asp:HiddenField ID="HF_REVHeaderID" runat="server" Value='<%#Eval("ID") %>' />
                                                <asp:Label ID="LabelDate" runat="server" Text='<%# DateTime.Parse(Eval("LogDate").ToString()).ToString("dd MMM yyyy, HH.mm") %>'></asp:Label>
                                                &nbsp;-&nbsp; 
                                                <asp:Label ID="LabelDokter" runat="server" Text='<%#Eval("UserName") %>'></asp:Label>
                                            </div>
                                        </div>

                                        <asp:Panel id="panelS" runat="server" class="row" style="border-top:1px solid #ceced3; border-bottom:1px solid #ceced3; background-color:#f2f3f4;">
                                            <div class="col-sm-2" style="padding-top:7px; padding-bottom:7px;">
                                                <label style="font-size:14px; font-weight:bold;">S</label>
                                            </div>
                                            <div class="col-sm-10" style="border-left:1px solid #ceced3; background-color:white; padding-top:7px; padding-bottom:7px;">
                                                <asp:Label ID="LabelComplaint" runat="server" Text=""></asp:Label>
                                                <asp:Label ID="LabelAnamnesis" runat="server" Text=""></asp:Label>
                                                <asp:Label ID="LabelDoctorNotesToNurse" runat="server" Text=""></asp:Label>
                                            </div>
                                        </asp:Panel>

                                        <asp:Panel id="panelO" runat="server" class="row" style="border-top:1px solid #ceced3; border-bottom:1px solid #ceced3; background-color:#f2f3f4;">
                                            <div class="col-sm-2" style="padding-top:7px; padding-bottom:7px;">
                                                <label style="font-size:14px; font-weight:bold;">O</label>
                                            </div>
                                            <div class="col-sm-10" style="border-left:1px solid #ceced3; background-color:white; padding-top:7px; padding-bottom:7px;">
                                                <asp:Label ID="LabelOther" runat="server" Text=""></asp:Label>
                                                <asp:Label ID="LabelBloodPresH" runat="server" Text=""></asp:Label>
                                                <asp:Label ID="LabelBloodPresL" runat="server" Text=""></asp:Label>
                                                <asp:Label ID="LabelPulse" runat="server" Text=""></asp:Label>
                                                <asp:Label ID="LabelRespiratory" runat="server" Text=""></asp:Label>
                                                <asp:Label ID="LabelSPO2" runat="server" Text=""></asp:Label>
                                                <asp:Label ID="LabelTemp" runat="server" Text=""></asp:Label>
                                                <asp:Label ID="LabelWeight" runat="server" Text=""></asp:Label>
                                                <asp:Label ID="LabelHeight" runat="server" Text=""></asp:Label>
                                                <asp:Label ID="LabelHeadCir" runat="server" Text=""></asp:Label>
                                            </div>
                                        </asp:Panel>

                                        <asp:Panel id="panelA" runat="server" class="row" style="border-top:1px solid #ceced3; border-bottom:1px solid #ceced3; background-color:#f2f3f4;">
                                            <div class="col-sm-2" style="padding-top:7px; padding-bottom:7px;">
                                                <label style="font-size:14px; font-weight:bold;">A</label>
                                            </div>
                                            <div class="col-sm-10" style="border-left:1px solid #ceced3; background-color:white; padding-top:7px; padding-bottom:7px;">
                                                <asp:Label ID="LabelPrimaryDiagnosis" runat="server" Text=""></asp:Label>
                                            </div>
                                        </asp:Panel>

                                        <asp:Panel id="panelP" runat="server" class="row" style="border-top:1px solid #ceced3; border-bottom:1px solid #ceced3; background-color:#f2f3f4;">
                                            <div class="col-sm-2" style="padding-top:7px; padding-bottom:7px;">
                                                <label style="font-size:14px; font-weight:bold;">P</label>
                                            </div>
                                            <div class="col-sm-10" style="border-left:1px solid #ceced3; background-color:white; padding-top:7px; padding-bottom:7px;">
                                                <asp:Label ID="LabelPlanningProcedure" runat="server" Text=""></asp:Label>
                                                <asp:Label ID="LabelPlanningOthers" runat="server" Text=""></asp:Label>
                                                <asp:Label ID="LabelProcedureResult" runat="server" Text=""></asp:Label>
                                            </div>
                                        </asp:Panel>

                                        <asp:Panel id="panelCPOE" runat="server" class="row" style="border-top:1px solid #ceced3; border-bottom:1px solid #ceced3; background-color:#f2f3f4;">
                                            <div class="col-sm-2" style="padding-top:7px; padding-bottom:7px;">
                                                <label style="font-size:14px; font-weight:bold;">CPOE</label>
                                            </div>
                                            <div class="col-sm-10" style="border-left:1px solid #ceced3; background-color:white; padding-top:7px; padding-bottom:7px;">
                                                
                                                <asp:Repeater ID="RptLabRad" runat="server">
                                                    <%--<HeaderTemplate>
                                                        <b>LAB</b>
                                                    </HeaderTemplate>--%>
                                                    <ItemTemplate>
                                                        <div> 
                                                            <li><asp:Label ID="LabelItemName" runat="server" Text='<%#Eval("ItemName") %>'></asp:Label></li>
                                                        </div>
                                                    </ItemTemplate>
                                                </asp:Repeater>
                                                <%--<br />
                                                <asp:Repeater ID="RptRad" runat="server">
                                                    <HeaderTemplate>
                                                        <b>RAD</b>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <div> 
                                                            <asp:Label ID="LabelItemName" runat="server" Text='<%#Eval("ItemName") %>'></asp:Label>
                                                        </div>
                                                    </ItemTemplate>
                                                </asp:Repeater>--%>

                                            </div>
                                        </asp:Panel>

                                        <asp:Panel id="panelPRES" runat="server" class="row" style="border-top:1px solid #ceced3; border-bottom:1px solid #ceced3; background-color:#f2f3f4;">
                                            <div class="col-sm-2" style="padding-top:7px; padding-bottom:7px;">
                                                <label style="font-size:14px; font-weight:bold;">Prescription</label>
                                            </div>
                                            <div class="col-sm-10" style="border-left:1px solid #ceced3; background-color:white; padding-top:7px; padding-bottom:7px;">
                                                
                                                <asp:Repeater ID="RptDrugs" runat="server">
                                                    <HeaderTemplate>
                                                        <b>Drugs</b>
                                                        <table style="width:100%;" class="table-striped table-condensed">
                                                            <tr>
                                                                <th>Item</th>
                                                                <th>Dose</th>
                                                                <th>Frequency</th>
                                                                <th>Route</th>
                                                                <th>Instruction</th>
                                                                <th>Qty</th>
                                                                <th>UoM</th>
                                                                <th>Iter</th>
                                                                <th>Routine</th>
                                                            </tr>
                                                        
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        
                                                            <tr>
                                                                <td><asp:Label ID="lblItemName" runat="server" Text='<%#Eval("SalesItemName") %>'></asp:Label></td>
                                                                <td><asp:Label ID="lblDose" runat="server" Text='<%#Eval("Dose") %>'></asp:Label> &nbsp;&nbsp; <asp:Label ID="lblDoseUom" runat="server" Text='<%#Eval("DoseUom") %>'></asp:Label> </td>
                                                                <td><asp:Label ID="lblFreq" runat="server" Text='<%#Eval("Frequency") %>'></asp:Label></td>
                                                                <td><asp:Label ID="lblRoute" runat="server" Text='<%#Eval("Route") %>'></asp:Label></td>
                                                                <td><asp:Label ID="lblInstruction" runat="server" Text='<%#Eval("Instruction") %>'></asp:Label></td>
                                                                <td><asp:Label ID="lblQty" runat="server" Text='<%#Eval("Quantity") %>'></asp:Label></td>
                                                                <td><asp:Label ID="lblUom" runat="server" Text='<%#Eval("Uom") %>'></asp:Label></td>
                                                                <td><asp:Label ID="lblIter" runat="server" Text='<%#Eval("Iteration") %>'></asp:Label></td>
                                                                <td><asp:Label ID="lblRoutine" runat="server" Text='<%#Eval("IsRoutine").ToString().ToLower() == "false" ? "No" : "Yes" %>'></asp:Label></td>
                                                            </tr>
                                                       
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        </table>
                                                    </FooterTemplate>
                                                </asp:Repeater>
                                                <br />
                                                <asp:Repeater ID="RptCons" runat="server">
                                                    <HeaderTemplate>
                                                        <b>Consumables</b>
                                                        <table style="width:100%;" class="table-striped table-condensed">
                                                            <tr>
                                                                <th>Item</th>
                                                                <th>Qty</th>
                                                                <th>UoM</th>
                                                                <th>Instruction</th>
                                                            </tr>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                            <tr>
                                                                <td><asp:Label ID="lblItemName" runat="server" Text='<%#Eval("SalesItemName") %>'></asp:Label></td>
                                                                <td><asp:Label ID="lblQty" runat="server" Text='<%#Eval("Quantity") %>'></asp:Label></td>
                                                                <td><asp:Label ID="lblUom" runat="server" Text='<%#Eval("Uom") %>'></asp:Label></td>
                                                                <td><asp:Label ID="lblInstruction" runat="server" Text='<%#Eval("Instruction") %>'></asp:Label></td>
                                                            </tr>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        </table>
                                                    </FooterTemplate>
                                                </asp:Repeater>

                                            </div>
                                        </asp:Panel>
                                    

                                    </ItemTemplate>
                                </asp:Repeater>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
        </div>

        
        <div id="DivModalDashboard" class="modal fade" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
            <div class="modal-dialog" style="width: 95%;" runat="server">
                <div class="modal-content" style="border-radius: 7px; height: 100%;">
                    <div class="modal-header" style="height: 40px; padding-top: 10px; padding-bottom: 5px">
                        <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                        <h4 class="modal-title" style="text-align: left">
                            <asp:Label ID="LabelHeaderDashboard" Style="font-family: Arial, Helvetica, sans-serif;" runat="server" Text="Patient Dashboard"></asp:Label>
                        </h4>
                    </div>
                    <div class="modal-body" style="background-color: white; border-radius: 7px; padding:0px;">
                        <asp:UpdatePanel runat="server" ID="UpdatePanelDashboard" UpdateMode="Conditional">
                            <ContentTemplate>
                                <div style="width: 100%" class="btn-group" role="group">
                                    <asp:Button runat="server" ID="btnDashboardModal" Text="" CssClass="hidden" OnClick="btnDashboardModal_Click" />
                                    <asp:HiddenField ID="HFdashOrgID" runat="server" />
                                    <asp:HiddenField ID="HFdashPtnID" runat="server" />
                                    <asp:HiddenField ID="HFdashAdmID" runat="server" />
                                    <asp:HiddenField ID="HFdashEncID" runat="server" />
                                    <asp:HiddenField ID="HFdashDocID" runat="server" />
                                    <iframe name="myDashboardIframe" id="myDashboardIframe" runat="server" style="width: 100%; height: calc(100vh - 110px); border: none; margin-top: 0%; overflow-y: scroll; padding-right: 0; padding-left: 0%; margin-left: 0;"></iframe>
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </div>
            </div>
        </div>
            

    </form>



    <script type="text/javascript">

        function dateStartNew() {
            var dp = $('#<%=txtDateFromNew.ClientID%>');
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

        function dateEndNew() {
            var dp = $('#<%=txtToDateNew.ClientID%>');
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


        //function openLabResultModal(ticket, patient, admission) {
        function openLabResultModal(ticket, patient, admission) {
            var hdnFieldAdmm = document.getElementById("<%=hf_admiss_id.ClientID %>");
            var hdnFieldPatient = document.getElementById("<%=hf_patient_id.ClientID %>");
            var hdnFieldTicket = document.getElementById("<%=hf_ticket_patient.ClientID %>");
            hdnFieldAdmm.value = admission;
            hdnFieldPatient.value = patient;
            hdnFieldTicket.value = ticket;
            document.getElementById("<%=HiddenLabMark.ClientID %>").value = 1;
            //openLabModal();
        }

        //function openRabResultModal(ticket, patient, admission) {
        function openRadResultModal(ticket, patient, admission) {
            var hdnFieldAdmm = document.getElementById("<%=hf_admiss_id.ClientID %>");
            var hdnFieldPatient = document.getElementById("<%=hf_patient_id.ClientID %>");
            var hdnFieldTicket = document.getElementById("<%=hf_ticket_patient.ClientID %>");
            hdnFieldAdmm.value = admission;
            hdnFieldPatient.value = patient;
            hdnFieldTicket.value = ticket;
            document.getElementById("<%=HiddenRadMark.ClientID %>").value = 1;
            //openLabModal();
        }

        function openLabModal() {
            $('#laboratoryResult').modal('show');
        }

        function openRadModal() {
            $('#radiologyResult').modal('show');
        }

        function openPatientHistoryModal() {
            $('#patientHistoryModal').modal('show');
        }

        function RevModal(orgid, ptnid, admid, encid) {
            $('#modalRevision').modal('show');
            document.getElementById('<%=HFrevOrgID.ClientID%>').value = orgid;
            document.getElementById('<%=HFrevPtnID.ClientID%>').value = ptnid;
            document.getElementById('<%=HFrevAdmID.ClientID%>').value = admid;
            document.getElementById('<%=HFrevEncID.ClientID%>').value = encid;
            document.getElementById('<%=btnrevisionModal.ClientID%>').click();
            return true;
        }

        function DashboardModal(orgid, ptnid, admid, encid, docid) {
            $('#DivModalDashboard').modal('show');
            document.getElementById('<%=HFdashOrgID.ClientID%>').value = orgid;
            document.getElementById('<%=HFdashPtnID.ClientID%>').value = ptnid;
            document.getElementById('<%=HFdashAdmID.ClientID%>').value = admid;
            document.getElementById('<%=HFdashEncID.ClientID%>').value = encid;
            document.getElementById('<%=HFdashDocID.ClientID%>').value = docid;
            document.getElementById('<%=btnDashboardModal.ClientID%>').click();
            return true;
        }

        function DashboardModalSingle() {
            $('#DivModalDashboard').modal('show');
            document.getElementById('<%=btnDashboardModal.ClientID%>').click();
            return true;
        }

    </script>
</body>
</html>
