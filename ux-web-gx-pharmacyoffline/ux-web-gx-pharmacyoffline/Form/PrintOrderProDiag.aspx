<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PrintOrderProDiag.aspx.cs" Inherits="Form_PrintOrderProDiag" %>

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
    <!-- Select Bootstrap -->
    <link rel="stylesheet" href="Content/bootstrap-select/css/bootstrap-select.css" />
    <!-- EMR-Doctor -->
    <link rel="stylesheet" href="Content/EMR-Doctor.css" />
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
                <asp:ScriptReference Path="~/Content/bootstrap-select/js/bootstrap-select.js" />
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

            td {
                padding-top: 10px;
                padding-bottom: 10px;
            }

            #tableSOAPDiagnostic {
                width: 100%;
                border: 1px solid #CDCED9;
            }

                #tableSOAPDiagnostic td {
                    padding-top: 5px;
                    padding-bottom: 5px;
                    border: 1px solid #CDCED9;
                    padding-left: 5px;
                }

                #tableSOAPDiagnostic th {
                    background-color: black;
                    color: white;
                    padding-top: 5px;
                    padding-bottom: 5px;
                    border: 1px solid #CDCED9;
                    padding-left: 5px;
                    -webkit-print-color-adjust: exact;
                    background-image: radial-gradient(#000,#000);
                }

            #tableSOAPDiagnostic_FO {
                width: 100%;
                border: 1px solid #CDCED9;
            }

                #tableSOAPDiagnostic_FO td {
                    padding-top: 5px;
                    padding-bottom: 5px;
                    border: 1px solid #CDCED9;
                    padding-left: 5px;
                }

                #tableSOAPDiagnostic_FO th {
                    background-color: black;
                    color: white;
                    padding-top: 5px;
                    padding-bottom: 5px;
                    border: 1px solid #CDCED9;
                    padding-left: 5px;
                    -webkit-print-color-adjust: exact;
                    background-image: radial-gradient(#000,#000);
                }

            #tableSOAPProcedure {
                width: 100%;
                border: 1px solid #CDCED9;
            }

                #tableSOAPProcedure td {
                    padding-top: 5px;
                    padding-bottom: 5px;
                    border: 1px solid #CDCED9;
                    padding-left: 5px;
                }

                #tableSOAPProcedure th {
                    background-color: black;
                    color: white;
                    padding-top: 5px;
                    padding-bottom: 5px;
                    border: 1px solid #CDCED9;
                    padding-left: 5px;
                    -webkit-print-color-adjust: exact;
                    background-image: radial-gradient(#000,#000);
                }

            #tableSOAPProcedure_FO {
                width: 100%;
                border: 1px solid #CDCED9;
            }

                #tableSOAPProcedure_FO td {
                    padding-top: 5px;
                    padding-bottom: 5px;
                    border: 1px solid #CDCED9;
                    padding-left: 5px;
                }

                #tableSOAPProcedure_FO th {
                    background-color: black;
                    color: white;
                    padding-top: 5px;
                    padding-bottom: 5px;
                    border: 1px solid #CDCED9;
                    padding-left: 5px;
                    -webkit-print-color-adjust: exact;
                    background-image: radial-gradient(#000,#000);
                }

            .fontOrder {
                font-family: Arial, Helvetica, sans-serif;
                font-size: 14px
            }

            .pagebreak { page-break-before: always;}
            .imageCovid {
                width: 150px; 
                height: 75px; 
                position: absolute; 
                right: 15px; 
                margin-top:1.7em;
                /*top: 35px;*/
                page-break-before:auto;
                page-break-before:auto;
                page-break-inside:avoid
            }
        </style>

        <asp:HiddenField runat="server" ID="hfOrgID" />
        <asp:HiddenField runat="server" ID="hf_admiss_id" />
        <asp:HiddenField runat="server" ID="hf_patient_id" />
        <asp:HiddenField runat="server" ID="hf_ticket_patient" />
        <asp:HiddenField ID="HiddenLabMark" runat="server" />

        <div id="div_diagnostic" runat="server" visible="false">
            <asp:UpdatePanel runat="server">
                <ContentTemplate>

                    <asp:Image ID="ImageCovidDiagnostic" runat="server" ImageUrl="~/Images/Icon/CV-stamp.svg" Visible="false" CssClass="imageCovid" />
                    <table style="width: 100%;">
                        <tr>
                            <td style="width: 70%"></td>
                            <td style="width: 30%">
                                <label style="font-weight: bold; font-family: Arial, Helvetica, sans-serif; font-size: 14px">Tanggal Dibuat (</label>
                                <label style="font-weight: bold; font-family: Arial, Helvetica, sans-serif; font-size: 14px">Order Date</label>
                                <label style="font-weight: bold; font-family: Arial, Helvetica, sans-serif; font-size: 14px">)</label>
                                <br />
                                <asp:Label ID="lblCreatedOrderDateDiagnostic" runat="server" Font-Bold="false" Font-Names="Helvetica" Font-Size="14px"></asp:Label>
                            </td>
                        </tr>
                    </table>
                    <br />

                    <asp:Label runat="server" Text="Diagnosa Klinis (" Font-Bold="true" Font-Names="Helvetica" Font-Size="14px"></asp:Label>
                    <asp:Label runat="server" Text="Clinical Diagnosis" Font-Bold="true" Font-Italic="true" Font-Names="Helvetica" Font-Size="14px"></asp:Label>
                    <asp:Label runat="server" Text="):" Font-Bold="true" Font-Names="Helvetica" Font-Size="14px"></asp:Label>

                    <br />

                    <asp:Label ID="lblClinicalDiagnosisDiagnostic" runat="server" Text="-" Font-Names="Helvetica" Font-Size="14px"></asp:Label>

                    <br />
                    <br />

                    <asp:Label runat="server" Text="Status Kehamilan (" Font-Bold="true" Font-Names="Helvetica" Font-Size="14px"></asp:Label>
                    <asp:Label runat="server" Text="Pregnancy Status" Font-Bold="true" Font-Italic="true" Font-Names="Helvetica" Font-Size="14px"></asp:Label>
                    <asp:Label runat="server" Text="):" Font-Bold="true" Font-Names="Helvetica" Font-Size="14px"></asp:Label>
                    <asp:Label runat="server" Text="-" ID="lblPregnantDiagnostic" Font-Bold="false" Font-Names="Helvetica" Font-Size="14px"></asp:Label>
                    <asp:Label runat="server" Text="-" ID="lblBreastfeedDiagnostic" Font-Bold="false" Font-Names="Helvetica" Font-Size="14px"></asp:Label>
                    
                    <br />
                    <br />

                    <asp:Label runat="server" Text="Order Diagnostik (" Font-Bold="true" Font-Names="Helvetica" Font-Size="14px"></asp:Label>
                    <asp:Label runat="server" Text="Diagnostic Order" Font-Bold="true" Font-Italic="true" Font-Names="Helvetica" Font-Size="14px"></asp:Label>
                    <asp:Label runat="server" Text="):" Font-Bold="true" Font-Names="Helvetica" Font-Size="14px"></asp:Label>

                    <table id="tableSOAPDiagnostic">
                        <tr>
                            <th style="width: 15%">
                                <asp:Label runat="server" Text="Order"></asp:Label>
                            </th>
                            <th style="width: 85%">
                                <asp:Label runat="server" Text="Detail"></asp:Label>
                            </th>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label runat="server" Text="Endoscopy" Font-Bold="true"></asp:Label>
                            </td>
                            <td>
                                <asp:Label runat="server" ID="lblNoDiagnosticEndoscopy" Text="(tidak ada order)" Visible="true"></asp:Label>
                                <asp:Repeater runat="server" ID="rptDiagnosticEndoscopy">
                                    <ItemTemplate>
                                        <asp:Label runat="server" Text="○"></asp:Label>
                                        <asp:Label runat="server" ID="lblDiagnosticEndoscopy" Text='<%#Bind("SalesItemName") %>'></asp:Label>
                                        <br />
                                    </ItemTemplate>
                                </asp:Repeater>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label runat="server" Text="Pulmonology" Font-Bold="true"></asp:Label>
                            </td>
                            <td>
                                <asp:Label runat="server" ID="lblNoDiagnosticPulmonology" Text="(tidak ada order)" Visible="true"></asp:Label>
                                <asp:Repeater runat="server" ID="rptDiagnosticPulmonology">
                                    <ItemTemplate>
                                        <asp:Label runat="server" Text="○"></asp:Label>
                                        <asp:Label runat="server" ID="lblDiagnosticPulmonology" Text='<%#Bind("SalesItemName") %>'></asp:Label>
                                        <br />
                                    </ItemTemplate>
                                </asp:Repeater>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label runat="server" Text="Neurology" Font-Bold="true"></asp:Label>
                            </td>
                            <td>
                                <asp:Label runat="server" ID="lblNoDiagnosticNeurology" Text="(tidak ada order)" Visible="true"></asp:Label>
                                <asp:Repeater runat="server" ID="rptDiagnosticNeurology">
                                    <ItemTemplate>
                                        <asp:Label runat="server" Text="○"></asp:Label>
                                        <asp:Label runat="server" ID="lblDiagnosticNeurology" Text='<%#Bind("SalesItemName") %>'></asp:Label>
                                        <br />
                                    </ItemTemplate>
                                </asp:Repeater>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label runat="server" Text="ENT" Font-Bold="true"></asp:Label>
                            </td>
                            <td>
                                <asp:Label runat="server" ID="lblNoDiagnosticENT" Text="(tidak ada order)" Visible="true"></asp:Label>
                                <asp:Repeater runat="server" ID="rptDiagnosticENT">
                                    <ItemTemplate>
                                        <asp:Label runat="server" Text="○"></asp:Label>
                                        <asp:Label runat="server" ID="lblDiagnosticENT" Text='<%#Bind("SalesItemName") %>'></asp:Label>
                                        <br />
                                    </ItemTemplate>
                                </asp:Repeater>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label runat="server" Text="Cardiology" Font-Bold="true"></asp:Label>
                            </td>
                            <td>
                                <asp:Label runat="server" ID="lblNoDiagnosticCardiology" Text="(tidak ada order)" Visible="true"></asp:Label>
                                <asp:Repeater runat="server" ID="rptDiagnosticCardiology">
                                    <ItemTemplate>
                                        <asp:Label runat="server" Text="○"></asp:Label>
                                        <asp:Label runat="server" ID="lblDiagnosticCardiology" Text='<%#Bind("SalesItemName") %>'></asp:Label>
                                        <br />
                                    </ItemTemplate>
                                </asp:Repeater>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label runat="server" Text="Lain-lain" Font-Bold="true"></asp:Label>
                            </td>
                            <td>
                                <asp:Label runat="server" ID="lblNoDiagnosticOthers" Text="(tidak ada order)" Visible="true"></asp:Label>
                                <asp:Repeater runat="server" ID="rptDiagnosticOthers">
                                    <ItemTemplate>
                                        <asp:Label runat="server" Text="○"></asp:Label>
                                        <asp:Label runat="server" ID="lblDiagnosticOthers" Text='<%#Bind("SalesItemName") %>'></asp:Label>
                                        <br />
                                    </ItemTemplate>
                                </asp:Repeater>
                            </td>
                        </tr>
                    </table>
                    <br />
                    <div>
                        <asp:Label runat="server" ID="lblDiagnosticNotes" Text="Catatan:" Font-Bold="true" Font-Italic="true" Font-Size="Medium"></asp:Label>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
            <br />
            <br />
            <br />
            <div class="row">
                <div class="col-sm-12">
                    <table border="0" style="width: 100%;">
                        <tr>
                            <td style="width: 45%;"></td>
                            <td style="width: 10%;"></td>
                            <td style="width: 45%; vertical-align: bottom; padding-left: 10%;">
                                <asp:Label runat="server" ID="lblNamaDokterDiagnostic"></asp:Label>
                                <hr style="margin-top: 0%; margin-bottom: 0%" />
                                <asp:Label runat="server" ID="lblDokterKetDiagnostic" Text="Dokter ("></asp:Label>
                                <asp:Label runat="server" ID="Label1" Text="Physician" Font-Italic="true"></asp:Label>
                                <asp:Label runat="server" ID="Label2" Text=")"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
        </div>
        <div id="div_break_diagnostic" runat="server" class="pagebreak" visible="false"></div>
        <div id="div_diagnostic_fo" runat="server" visible="false">
            <asp:UpdatePanel runat="server">
                <ContentTemplate>

                    <asp:Image ID="ImageCovidDiagnostic_FO" runat="server" ImageUrl="~/Images/Icon/CV-stamp.svg" Visible="false" CssClass="imageCovid"/>
                    <table style="width: 100%;">
                        <tr>
                            <td style="width: 70%"></td>
                            <td style="width: 30%">
                                <label style="font-weight: bold; font-family: Arial, Helvetica, sans-serif; font-size: 14px">Tanggal Direncanakan (</label>
                                <label style="font-weight: bold; font-family: Arial, Helvetica, sans-serif; font-size: 14px">Planned Date</label>
                                <label style="font-weight: bold; font-family: Arial, Helvetica, sans-serif; font-size: 14px">)</label>
                                <br />
                                <asp:Label ID="lblCreatedOrderDateDiagnostic_FO" runat="server" Font-Bold="false" Font-Names="Helvetica" Font-Size="14px"></asp:Label>
                            </td>
                        </tr>
                    </table>

                    <br />

                    <asp:Label runat="server" Text="Diagnosa Klinis (" Font-Bold="true" Font-Names="Helvetica" Font-Size="14px"></asp:Label>
                    <asp:Label runat="server" Text="Clinical Diagnosis" Font-Bold="true" Font-Italic="true" Font-Names="Helvetica" Font-Size="14px"></asp:Label>
                    <asp:Label runat="server" Text="):" Font-Bold="true" Font-Names="Helvetica" Font-Size="14px"></asp:Label>
                    
                    <br />
                   
                    <asp:Label ID="lblClinicalDiagnosisDiagnostic_FO" runat="server" Text="-" Font-Names="Helvetica" Font-Size="14px"></asp:Label>
                    
                    <br />
                    <br />
                    
                    <asp:Label runat="server" Text="Status Kehamilan (" Font-Bold="true" Font-Names="Helvetica" Font-Size="14px"></asp:Label>
                    <asp:Label runat="server" Text="Pregnancy Status" Font-Bold="true" Font-Italic="true" Font-Names="Helvetica" Font-Size="14px"></asp:Label>
                    <asp:Label runat="server" Text="):" Font-Bold="true" Font-Names="Helvetica" Font-Size="14px"></asp:Label>
                    <asp:Label runat="server" Text="-" ID="lblPregnantDiagnostic_FO" Font-Bold="false" Font-Names="Helvetica" Font-Size="14px"></asp:Label>
                    <asp:Label runat="server" Text="-" ID="lblBreastfeedDiagnostic_FO" Font-Bold="false" Font-Names="Helvetica" Font-Size="14px"></asp:Label>
                    
                    <br />
                    <br />
                    
                    <asp:Label runat="server" Text="Order Diagnostik mendatang (" Font-Bold="true" Font-Names="Helvetica" Font-Size="14px"></asp:Label>
                    <asp:Label runat="server" Text="Future Order Diagnostic" Font-Bold="true" Font-Italic="true" Font-Names="Helvetica" Font-Size="14px"></asp:Label>
                    <asp:Label runat="server" Text="):" Font-Bold="true" Font-Names="Helvetica" Font-Size="14px"></asp:Label>

                    <table id="tableSOAPDiagnostic_FO">
                        <tr>
                            <th style="width: 15%">
                                <asp:Label runat="server" Text="Order"></asp:Label>
                            </th>
                            <th style="width: 85%">
                                <asp:Label runat="server" Text="Detail"></asp:Label>
                            </th>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label runat="server" Text="Endoscopy" Font-Bold="true"></asp:Label>
                            </td>
                            <td>
                                <asp:Label runat="server" ID="lblNoDiagnosticEndoscopy_FO" Text="(tidak ada order)" Visible="true"></asp:Label>
                                <asp:Repeater runat="server" ID="rptDiagnosticEndoscopy_FO">
                                    <ItemTemplate>
                                        <asp:Label runat="server" Text="○"></asp:Label>
                                        <asp:Label runat="server" ID="lblDiagnosticEndoscopy_FO" Text='<%#Bind("SalesItemName") %>'></asp:Label>
                                        <br />
                                    </ItemTemplate>
                                </asp:Repeater>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label runat="server" Text="Pulmonology" Font-Bold="true"></asp:Label>
                            </td>
                            <td>
                                <asp:Label runat="server" ID="lblNoDiagnosticPulmonology_FO" Text="(tidak ada order)" Visible="true"></asp:Label>
                                <asp:Repeater runat="server" ID="rptDiagnosticPulmonology_FO">
                                    <ItemTemplate>
                                        <asp:Label runat="server" Text="○"></asp:Label>
                                        <asp:Label runat="server" ID="lblDiagnosticPulmonology_FO" Text='<%#Bind("SalesItemName") %>'></asp:Label>
                                        <br />
                                    </ItemTemplate>
                                </asp:Repeater>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label runat="server" Text="Neurology" Font-Bold="true"></asp:Label>
                            </td>
                            <td>
                                <asp:Label runat="server" ID="lblNoDiagnosticNeurology_FO" Text="(tidak ada order)" Visible="true"></asp:Label>
                                <asp:Repeater runat="server" ID="rptDiagnosticNeurology_FO">
                                    <ItemTemplate>
                                        <asp:Label runat="server" Text="○"></asp:Label>
                                        <asp:Label runat="server" ID="lblDiagnosticNeurology_FO" Text='<%#Bind("SalesItemName") %>'></asp:Label>
                                        <br />
                                    </ItemTemplate>
                                </asp:Repeater>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label runat="server" Text="ENT" Font-Bold="true"></asp:Label>
                            </td>
                            <td>
                                <asp:Label runat="server" ID="lblNoDiagnosticENT_FO" Text="(tidak ada order)" Visible="true"></asp:Label>
                                <asp:Repeater runat="server" ID="rptDiagnosticENT_FO">
                                    <ItemTemplate>
                                        <asp:Label runat="server" Text="○"></asp:Label>
                                        <asp:Label runat="server" ID="lblDiagnosticENT_FO" Text='<%#Bind("SalesItemName") %>'></asp:Label>
                                        <br />
                                    </ItemTemplate>
                                </asp:Repeater>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label runat="server" Text="Cardiology" Font-Bold="true"></asp:Label>
                            </td>
                            <td>
                                <asp:Label runat="server" ID="lblNoDiagnosticCardiology_FO" Text="(tidak ada order)" Visible="true"></asp:Label>
                                <asp:Repeater runat="server" ID="rptDiagnosticCardiology_FO">
                                    <ItemTemplate>
                                        <asp:Label runat="server" Text="○"></asp:Label>
                                        <asp:Label runat="server" ID="lblDiagnosticCardiology_FO" Text='<%#Bind("SalesItemName") %>'></asp:Label>
                                        <br />
                                    </ItemTemplate>
                                </asp:Repeater>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label runat="server" Text="Lain-lain" Font-Bold="true"></asp:Label>
                            </td>
                            <td>
                                <asp:Label runat="server" ID="lblNoDiagnosticOthers_FO" Text="(tidak ada order)" Visible="true"></asp:Label>
                                <asp:Repeater runat="server" ID="rptDiagnosticOthers_FO">
                                    <ItemTemplate>
                                        <asp:Label runat="server" Text="○"></asp:Label>
                                        <asp:Label runat="server" ID="lblDiagnosticOthers_FO" Text='<%#Bind("SalesItemName") %>'></asp:Label>
                                        <br />
                                    </ItemTemplate>
                                </asp:Repeater>
                            </td>
                        </tr>
                    </table>
                    <br />
                    <div>
                        <asp:Label runat="server" ID="lblDiagnosticNotes_FO" Text="Catatan:" Font-Bold="true" Font-Italic="true" Font-Size="Medium"></asp:Label>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
            <br />
            <br />
            <br />
            <div class="row">
                <div class="col-sm-12">
                    <table border="0" style="width: 100%;">
                        <tr>
                            <td style="width: 45%;"></td>
                            <td style="width: 10%;"></td>
                            <td style="width: 45%; vertical-align: bottom; padding-left: 10%;">
                                <asp:Label runat="server" ID="lblNamaDokterDiagnostic_FO"></asp:Label>
                                <hr style="margin-top: 0%; margin-bottom: 0%" />
                                <asp:Label runat="server" ID="lblDokterKetDiagnostic_FO" Text="Dokter ("></asp:Label>
                                <asp:Label runat="server" ID="Label1_FO" Text="Physician" Font-Italic="true"></asp:Label>
                                <asp:Label runat="server" ID="Label2_FO" Text=")"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
        </div>
        <div id="div_break_diagnostic_fo" runat="server" class="pagebreak" visible="false"></div>
        <div id="div_procedure" runat="server" visible="false">
            <asp:UpdatePanel runat="server">
                <ContentTemplate>

                    <asp:Image ID="ImageCovidProcedure" runat="server" ImageUrl="~/Images/Icon/CV-stamp.svg" Visible="false" CssClass="imageCovid" />
                    <table style="width: 100%;">
                        <tr>
                            <td style="width: 70%"></td>
                            <td style="width: 30%">
                                <label style="font-weight: bold; font-family: Arial, Helvetica, sans-serif; font-size: 14px">Tanggal Dibuat (</label>
                                <label style="font-weight: bold; font-family: Arial, Helvetica, sans-serif; font-size: 14px">Order Date</label>
                                <label style="font-weight: bold; font-family: Arial, Helvetica, sans-serif; font-size: 14px">)</label>
                                <br />
                                <asp:Label ID="lblCreatedOrderDateProcedure" runat="server" Font-Bold="false" Font-Names="Helvetica" Font-Size="14px"></asp:Label>
                            </td>
                        </tr>
                    </table>
                    <br />

                    <asp:Label runat="server" Text="Diagnosa Klinis (" Font-Bold="true" Font-Names="Helvetica" Font-Size="14px"></asp:Label>
                    <asp:Label runat="server" Text="Clinical Diagnosis" Font-Bold="true" Font-Italic="true" Font-Names="Helvetica" Font-Size="14px"></asp:Label>
                    <asp:Label runat="server" Text="):" Font-Bold="true" Font-Names="Helvetica" Font-Size="14px"></asp:Label>

                    <br />

                    <asp:Label ID="lblClinicalDiagnosisProcedure" runat="server" Text="-" Font-Names="Helvetica" Font-Size="14px"></asp:Label>

                    <br />
                    <br />

                    <asp:Label runat="server" Text="Status Kehamilan (" Font-Bold="true" Font-Names="Helvetica" Font-Size="14px"></asp:Label>
                    <asp:Label runat="server" Text="Pregnancy Status" Font-Bold="true" Font-Italic="true" Font-Names="Helvetica" Font-Size="14px"></asp:Label>
                    <asp:Label runat="server" Text="):" Font-Bold="true" Font-Names="Helvetica" Font-Size="14px"></asp:Label>
                    <asp:Label runat="server" Text="-" ID="lblPregnantProcedure" Font-Bold="false" Font-Names="Helvetica" Font-Size="14px"></asp:Label>
                    <asp:Label runat="server" Text="-" ID="lblBreastfeedProcedure" Font-Bold="false" Font-Names="Helvetica" Font-Size="14px"></asp:Label>
                    
                    <br />
                    <br />

                    <asp:Label runat="server" Text="Order Prosedur (" Font-Bold="true" Font-Names="Helvetica" Font-Size="14px"></asp:Label>
                    <asp:Label runat="server" Text="Procedure Order" Font-Bold="true" Font-Italic="true" Font-Names="Helvetica" Font-Size="14px"></asp:Label>
                    <asp:Label runat="server" Text="):" Font-Bold="true" Font-Names="Helvetica" Font-Size="14px"></asp:Label>

                    <table id="tableSOAPProcedure">
                        <tr>
                            <th style="width: 100%">
                                <asp:Label runat="server" Text="Order"></asp:Label>
                            </th>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label runat="server" ID="lblNoProcedure" Text="(tidak ada order)" Visible="true"></asp:Label>
                                <asp:Repeater runat="server" ID="rptProcedure">
                                    <ItemTemplate>
                                        <asp:Label runat="server" Text="○"></asp:Label>
                                        <asp:Label runat="server" ID="lblProcedure" Text='<%#Bind("SalesItemName") %>'></asp:Label>
                                        <br />
                                    </ItemTemplate>
                                </asp:Repeater>
                            </td>
                        </tr>
                    </table>
                    <br />
                </ContentTemplate>
            </asp:UpdatePanel>
            <br />
            <br />
            <br />
            <div class="row">
                <div class="col-sm-12">
                    <table border="0" style="width: 100%;">
                        <tr>
                            <td style="width: 45%;"></td>
                            <td style="width: 10%;"></td>
                            <td style="width: 45%; vertical-align: bottom; padding-left: 10%;">
                                <asp:Label runat="server" ID="lblNamaDokterProcedure"></asp:Label>
                                <hr style="margin-top: 0%; margin-bottom: 0%" />
                                <asp:Label runat="server" ID="lblDokterKetProcedure" Text="Dokter ("></asp:Label>
                                <asp:Label runat="server" ID="Label3" Text="Physician" Font-Italic="true"></asp:Label>
                                <asp:Label runat="server" ID="Label4" Text=")"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
        </div>
        <div id="div_break_procedure" runat="server" class="pagebreak" visible="false"></div>
        <div id="div_procedure_fo" runat="server" visible="false">
            <asp:UpdatePanel runat="server">
                <ContentTemplate>

                    <asp:Image ID="ImageCovidProcedure_FO" runat="server" ImageUrl="~/Images/Icon/CV-stamp.svg" Visible="false" CssClass="imageCovid" />
                    <table style="width: 100%;">
                        <tr>
                            <td style="width: 70%"></td>
                            <td style="width: 30%">
                                <label style="font-weight: bold; font-family: Arial, Helvetica, sans-serif; font-size: 14px">Tanggal Direncanakan (</label>
                                <label style="font-weight: bold; font-family: Arial, Helvetica, sans-serif; font-size: 14px">Planned Date</label>
                                <label style="font-weight: bold; font-family: Arial, Helvetica, sans-serif; font-size: 14px">)</label>
                                <br />
                                <asp:Label ID="lblCreatedOrderDateProcedure_FO" runat="server" Font-Bold="false" Font-Names="Helvetica" Font-Size="14px"></asp:Label>
                            </td>
                        </tr>
                    </table>

                    <br />

                    <asp:Label runat="server" Text="Diagnosa Klinis (" Font-Bold="true" Font-Names="Helvetica" Font-Size="14px"></asp:Label>
                    <asp:Label runat="server" Text="Clinical Diagnosis" Font-Bold="true" Font-Italic="true" Font-Names="Helvetica" Font-Size="14px"></asp:Label>
                    <asp:Label runat="server" Text="):" Font-Bold="true" Font-Names="Helvetica" Font-Size="14px"></asp:Label>
                    
                    <br />
                   
                    <asp:Label ID="lblClinicalDiagnosisProcedure_FO" runat="server" Text="-" Font-Names="Helvetica" Font-Size="14px"></asp:Label>
                    
                    <br />
                    <br />
                    
                    <asp:Label runat="server" Text="Status Kehamilan (" Font-Bold="true" Font-Names="Helvetica" Font-Size="14px"></asp:Label>
                    <asp:Label runat="server" Text="Pregnancy Status" Font-Bold="true" Font-Italic="true" Font-Names="Helvetica" Font-Size="14px"></asp:Label>
                    <asp:Label runat="server" Text="):" Font-Bold="true" Font-Names="Helvetica" Font-Size="14px"></asp:Label>
                    <asp:Label runat="server" Text="-" ID="lblPregnantProcedure_FO" Font-Bold="false" Font-Names="Helvetica" Font-Size="14px"></asp:Label>
                    <asp:Label runat="server" Text="-" ID="lblBreastfeedProcedure_FO" Font-Bold="false" Font-Names="Helvetica" Font-Size="14px"></asp:Label>
                    
                    <br />
                    <br />
                    
                    <asp:Label runat="server" Text="Order Prosedur mendatang (" Font-Bold="true" Font-Names="Helvetica" Font-Size="14px"></asp:Label>
                    <asp:Label runat="server" Text="Future Order Procedure" Font-Bold="true" Font-Italic="true" Font-Names="Helvetica" Font-Size="14px"></asp:Label>
                    <asp:Label runat="server" Text="):" Font-Bold="true" Font-Names="Helvetica" Font-Size="14px"></asp:Label>

                    <table id="tableSOAPProcedure_FO">
                        <tr>
                            <th style="width: 100%">
                                <asp:Label runat="server" Text="Order"></asp:Label>
                            </th>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label runat="server" ID="lblNoProcedure_FO" Text="(tidak ada order)" Visible="true"></asp:Label>
                                <asp:Repeater runat="server" ID="rptProcedure_FO">
                                    <ItemTemplate>
                                        <asp:Label runat="server" Text="○"></asp:Label>
                                        <asp:Label runat="server" ID="lblProcedure_FO" Text='<%#Bind("SalesItemName") %>'></asp:Label>
                                        <br />
                                    </ItemTemplate>
                                </asp:Repeater>
                            </td>
                        </tr>
                    </table>
                    <br />
                </ContentTemplate>
            </asp:UpdatePanel>
            <br />
            <br />
            <br />
            <div class="row">
                <div class="col-sm-12">
                    <table border="0" style="width: 100%;">
                        <tr>
                            <td style="width: 45%;"></td>
                            <td style="width: 10%;"></td>
                            <td style="width: 45%; vertical-align: bottom; padding-left: 10%;">
                                <asp:Label runat="server" ID="lblNamaDokterProcedure_FO"></asp:Label>
                                <hr style="margin-top: 0%; margin-bottom: 0%" />
                                <asp:Label runat="server" ID="lblDokterKetProcedure_FO" Text="Dokter ("></asp:Label>
                                <asp:Label runat="server" ID="Label5" Text="Physician" Font-Italic="true"></asp:Label>
                                <asp:Label runat="server" ID="Label6" Text=")"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
        </div>
        <div id="div_break_procedure_fo" runat="server" class="pagebreak" visible="false"></div>
    </form>

    <script type="text/javascript"></script>
</body>
</html>
