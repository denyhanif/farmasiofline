<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PrintOrderLab.aspx.cs" Inherits="Form_PrintOrderLab" %>

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

            #tableSOAP {
                width: 100%;
                border: 1px solid #CDCED9;
            }

                #tableSOAP td {
                    padding-top: 5px;
                    padding-bottom: 5px;
                    border: 1px solid #CDCED9;
                    padding-left: 5px;
                }

                #tableSOAP th {
                    background-color: black;
                    color: white;
                    padding-top: 5px;
                    padding-bottom: 5px;
                    border: 1px solid #CDCED9;
                    padding-left: 5px;
                    -webkit-print-color-adjust: exact;
                    background-image: radial-gradient(#000,#000);
                }

            #tableSOAP_FO {
                width: 100%;
                border: 1px solid #CDCED9;
            }

                #tableSOAP_FO td {
                    padding-top: 5px;
                    padding-bottom: 5px;
                    border: 1px solid #CDCED9;
                    padding-left: 5px;
                }

                #tableSOAP_FO th {
                    background-color: black;
                    color: white;
                    padding-top: 5px;
                    padding-bottom: 5px;
                    border: 1px solid #CDCED9;
                    padding-left: 5px;
                    -webkit-print-color-adjust: exact;
                    background-image: radial-gradient(#000,#000);
                }

            .tableParaf {
                width: 45%;
                border: 1px solid #CDCED9;
            }

                .tableParaf td {
                    padding: 2px;
                    border: 1px solid #CDCED9;
                }

            .fontOrder {
                font-family: Arial, Helvetica, sans-serif;
                font-size: 14px
            }

            .pagebreak { page-break-before: always; }
        </style>

        <asp:HiddenField runat="server" ID="hfOrgID" />
        <asp:HiddenField runat="server" ID="hf_admiss_id" />
        <asp:HiddenField runat="server" ID="hf_patient_id" />
        <asp:HiddenField runat="server" ID="hf_ticket_patient" />
        <asp:HiddenField ID="HiddenLabMark" runat="server" />

        <div id="div_lab" runat="server">
            <asp:UpdatePanel runat="server">
                <ContentTemplate>

                    <asp:Image ID="ImageCovid" runat="server" ImageUrl="~/Images/Icon/CV-stamp.svg" Visible="false" Style="width: 150px; height: 75px; position: absolute; right: 15px; top: 35px;" />
                    <table style="width: 100%;">
                        <tr>
                            <td style="width: 70%"></td>
                            <td style="width: 30%">
                                <label style="font-weight: bold; font-family: Arial, Helvetica, sans-serif; font-size: 14px">Tanggal Dibuat (</label>
                                <label style="font-weight: bold; font-family: Arial, Helvetica, sans-serif; font-size: 14px">Order Date</label>
                                <label style="font-weight: bold; font-family: Arial, Helvetica, sans-serif; font-size: 14px">)</label>
                                <br />
                                <asp:Label ID="lblCreatedOrderDate" runat="server" Font-Bold="false" Font-Names="Helvetica" Font-Size="14px"></asp:Label>
                            </td>
                        </tr>
                    </table>
                    <br />

                    <asp:Label runat="server" Text="Diagnosa Klinis (" Font-Bold="true" Font-Names="Helvetica" Font-Size="14px"></asp:Label>
                    <asp:Label runat="server" Text="Clinical Diagnosis" Font-Bold="true" Font-Italic="true" Font-Names="Helvetica" Font-Size="14px"></asp:Label>
                    <asp:Label runat="server" Text="):" Font-Bold="true" Font-Names="Helvetica" Font-Size="14px"></asp:Label>
                    <br />
                    <asp:Label runat="server" ID="lblClinicalDiagnosis" Text="-" Font-Names="Helvetica" Font-Size="14px"></asp:Label>
                    <br />
                    <br />
                    <asp:Label runat="server" Text="Status Kehamilan (" Font-Bold="true" Font-Names="Helvetica" Font-Size="14px"></asp:Label>
                    <asp:Label runat="server" Text="Pregnancy Status" Font-Bold="true" Font-Italic="true" Font-Names="Helvetica" Font-Size="14px"></asp:Label>
                    <asp:Label runat="server" Text="):" Font-Bold="true" Font-Names="Helvetica" Font-Size="14px"></asp:Label>
                    <asp:Label runat="server" Text="-" ID="lblPregnant" Font-Bold="false" Font-Names="Helvetica" Font-Size="14px"></asp:Label>
                    <asp:Label runat="server" Text="-" ID="lblBreastfeed" Font-Bold="false" Font-Names="Helvetica" Font-Size="14px"></asp:Label>
                    <br />
                    <br />
                    <asp:Label runat="server" Text="Order Laboratory (" Font-Bold="true" Font-Names="Helvetica" Font-Size="14px"></asp:Label>
                    <asp:Label runat="server" Text="Laboratory Order" Font-Bold="true" Font-Italic="true" Font-Names="Helvetica" Font-Size="14px"></asp:Label>
                    <asp:Label runat="server" Text="):" Font-Bold="true" Font-Names="Helvetica" Font-Size="14px"></asp:Label>
                    <table id="tableSOAP">
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
                                <asp:Label runat="server" Text="CITO" Font-Bold="true" Font-Underline="true"></asp:Label>
                            </td>
                            <td>
                                <asp:Label runat="server" ID="lblNoCito" Text="(tidak ada order)" Visible="true"></asp:Label>
                                <asp:Repeater runat="server" ID="repeatCito">
                                    <ItemTemplate>
                                        <asp:Label runat="server" Text="○"></asp:Label>
                                        <asp:Label runat="server" ID="lblCITO"><%# Eval("item_name") %> <%# Eval("fasting_flag").ToString()=="0" ? "" : Eval("fasting_flag").ToString()=="1" ? "*" : "**" %> </asp:Label>
                                        <br />
                                    </ItemTemplate>
                                </asp:Repeater>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label runat="server" Text="Clinical Pathology" Font-Bold="true"></asp:Label>
                            </td>
                            <td>
                                <asp:Label runat="server" ID="lblNoClinicalPathology" Text="(tidak ada order)" Visible="true"></asp:Label>
                                <asp:Repeater runat="server" ID="repeatClinical">
                                    <ItemTemplate>
                                        <asp:Label runat="server" Text="○"></asp:Label>
                                        <asp:Label runat="server" ID="lblClinicalPathology"> <%# Eval("item_name") %> <%# Eval("fasting_flag").ToString()=="0" ? "" : Eval("fasting_flag").ToString()=="1" ? "*" : "**" %> </asp:Label>
                                        <br />
                                    </ItemTemplate>
                                </asp:Repeater>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label runat="server" Text="Microbiology" Font-Bold="true"></asp:Label>
                            </td>
                            <td>
                                <asp:Label runat="server" ID="lblNoMicroB" Text="(tidak ada order)" Visible="true"></asp:Label>
                                <asp:Repeater runat="server" ID="repeatMicrobiology">
                                    <ItemTemplate>
                                        <asp:Label runat="server" Text="○"></asp:Label>
                                        <asp:Label runat="server" ID="lblMicrobiology"><%# Eval("item_name") %> <%# Eval("fasting_flag").ToString()=="0" ? "" : Eval("fasting_flag").ToString()=="1" ? "*" : "**" %> </asp:Label>
                                        <br />
                                    </ItemTemplate>
                                </asp:Repeater>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label runat="server" Text="Anatomical Pathology" Font-Bold="true"></asp:Label>
                            </td>
                            <td>
                                <asp:Label runat="server" ID="lblNoAnatomicalPathology" Text="(tidak ada order)" Visible="true"></asp:Label>
                                <asp:Repeater runat="server" ID="repeatAnatomical">
                                    <ItemTemplate>
                                        <asp:Label runat="server" Text="○"></asp:Label>
                                        <asp:Label runat="server" ID="lblAnatomicalPathology"><%# Eval("item_name") %> <%# Eval("fasting_flag").ToString()=="0" ? "" : Eval("fasting_flag").ToString()=="1" ? "*" : "**" %> </asp:Label>
                                        <br />
                                    </ItemTemplate>
                                </asp:Repeater>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label runat="server" Text="MDC" Font-Bold="true"></asp:Label>
                            </td>
                            <td>
                                <asp:Label runat="server" ID="lblNoMDC" Text="(tidak ada order)" Visible="true"></asp:Label>
                                <asp:Repeater runat="server" ID="repeatMDC">
                                    <ItemTemplate>
                                        <asp:Label runat="server" Text="○"></asp:Label>
                                        <asp:Label runat="server" ID="lblMDC"><%# Eval("item_name") %> <%# Eval("fasting_flag").ToString()=="0" ? "" : Eval("fasting_flag").ToString()=="1" ? "*" : "**" %> </asp:Label>
                                        <br />
                                    </ItemTemplate>
                                </asp:Repeater>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label runat="server" Text="Panel & Others" Font-Bold="true"></asp:Label>
                            </td>
                            <td>
                                <asp:Label runat="server" ID="lblNoPanel" Text="(tidak ada order)" Visible="true"></asp:Label>
                                <asp:Repeater runat="server" ID="repeatPanel">
                                    <ItemTemplate>
                                        <asp:Label runat="server" Text="○"></asp:Label>
                                        <asp:Label runat="server" ID="lblPanelOthers"><%# Eval("item_name") %> <%# Eval("fasting_flag").ToString()=="0" ? "" : Eval("fasting_flag").ToString()=="1" ? "*" : "**" %> </asp:Label>
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
                                <asp:Label runat="server" ID="lblWarn" Text="*order lain-lain dapat ditujukan untuk pemeriksaan lab/rad ataupun pemeriksaan lainnya" Font-Bold="true" Visible="false"></asp:Label>
                                <br />
                                <asp:Label runat="server" ID="lblNoOthers" Text="(tidak ada order)" Visible="true"></asp:Label>
                                <asp:Repeater runat="server" ID="repeatOthers">
                                    <ItemTemplate>
                                        <asp:Label runat="server" Text="○"></asp:Label>
                                        <asp:Label runat="server" ID="lblOthers"><%# Eval("item_name") %> </asp:Label>
                                        <br />
                                    </ItemTemplate>
                                </asp:Repeater>
                            </td>
                        </tr>
                    </table>
                    <br />
                    <div>
                        <asp:Label runat="server" ID="lblCatatan" Text="Catatan:" Font-Bold="true" Font-Italic="true" Font-Size="Medium"></asp:Label>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
            <br />
            <br />
            <br />
            <br />
            <div class="row">

                <div class="col-sm-12">
                    <table border="0" style="width: 100%;">
                        <tr>
                            <td style="width: 45%; vertical-align: bottom;">
                                <label id="lblbhs_puasa0_clinicallab" style="font-size: 12px;"><b>Keterangan</b></label>
                                <br />
                                <label id="lblbhs_puasa1_clinicallab" style="font-size: 11px; font-style: normal;">&#42 Puasa 10-12 jam</label>
                                <br />
                                <label id="lblbhs_puasa2_clinicallab" style="font-size: 11px; font-style: normal;">&#42&#42 Puasa 12-14 jam</label>
                                <br />
                            </td>
                            <td style="width: 10%;"></td>
                            <td style="width: 45%; vertical-align: bottom; padding-left: 10%;">
                                <asp:Label runat="server" ID="lblNamaDokter"></asp:Label>
                                <hr style="margin-top: 0%; margin-bottom: 0%" />
                                <asp:Label runat="server" ID="lblDokterKet" Text="Dokter ("></asp:Label>
                                <asp:Label runat="server" ID="Label1" Text="Physician" Font-Italic="true"></asp:Label>
                                <asp:Label runat="server" ID="Label2" Text=")"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </div>
                <div class="col-sm-12" style="margin-top: 10px;">
                    <table class="tableParaf">
                        <tr>
                            <td colspan="3" style="text-align: center; font-weight: bold; padding: 2px;">OTORISASI PETUGAS</td>
                        </tr>
                        <tr>
                            <td style="text-align: center; width: 35px; font-weight: bold;">Tugas</td>
                            <td style="text-align: center; width: 40px; font-weight: bold;">Nama/Paraf</td>
                            <td style="text-align: center; width: 25px; font-weight: bold;">Tgl/Jam</td>
                        </tr>
                        <tr>
                            <td style="padding-left: 5px;">Phlebotomist</td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td style="padding-left: 5px;">Distribusi Sampel</td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td style="padding-left: 5px;">Print Hasil</td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td style="padding-left: 5px;">Cek Hasil</td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                        </tr>
                    </table>
                </div>
            </div>
        </div>
        <div id="div_break" runat="server" class="pagebreak"></div>
        <div id="div_lab_fo" runat="server">
            <asp:UpdatePanel runat="server">
                <ContentTemplate>

                    <asp:Image ID="ImageCovid_FO" runat="server" ImageUrl="~/Images/Icon/CV-stamp.svg" Visible="false" Style="width: 150px; height: 75px; position: absolute; right: 15px; top: 35px;" />
                    <table style="width: 100%;">
                        <tr>
                            <td style="width: 70%"></td>
                            <td style="width: 30%">
                                <label style="font-weight: bold; font-family: Arial, Helvetica, sans-serif; font-size: 14px">Tanggal Direncanakan (</label>
                                <label style="font-weight: bold; font-family: Arial, Helvetica, sans-serif; font-size: 14px">Planned Date</label>
                                <label style="font-weight: bold; font-family: Arial, Helvetica, sans-serif; font-size: 14px">)</label>
                                <br />
                                <asp:Label ID="lblCreatedOrderDate_FO" runat="server" Font-Bold="false" Font-Names="Helvetica" Font-Size="14px"></asp:Label>
                            </td>
                        </tr>
                    </table>
                    <br />

                    <asp:Label runat="server" Text="Diagnosa Klinis (" Font-Bold="true" Font-Names="Helvetica" Font-Size="14px"></asp:Label>
                    <asp:Label runat="server" Text="Clinical Diagnosis" Font-Bold="true" Font-Italic="true" Font-Names="Helvetica" Font-Size="14px"></asp:Label>
                    <asp:Label runat="server" Text="):" Font-Bold="true" Font-Names="Helvetica" Font-Size="14px"></asp:Label>
                    <br />
                    <asp:Label runat="server" ID="lblClinicalDiagnosis_FO" Text="-" Font-Names="Helvetica" Font-Size="14px"></asp:Label>
                    <br />
                    <br />
                    <asp:Label runat="server" Text="Status Kehamilan (" Font-Bold="true" Font-Names="Helvetica" Font-Size="14px"></asp:Label>
                    <asp:Label runat="server" Text="Pregnancy Status" Font-Bold="true" Font-Italic="true" Font-Names="Helvetica" Font-Size="14px"></asp:Label>
                    <asp:Label runat="server" Text="):" Font-Bold="true" Font-Names="Helvetica" Font-Size="14px"></asp:Label>
                    <asp:Label runat="server" Text="-" ID="lblPregnant_FO" Font-Bold="false" Font-Names="Helvetica" Font-Size="14px"></asp:Label>
                    <asp:Label runat="server" Text="-" ID="lblBreastfeed_FO" Font-Bold="false" Font-Names="Helvetica" Font-Size="14px"></asp:Label>
                    <br />
                    <br />
                    <asp:Label runat="server" Text="Order Laboratory (" Font-Bold="true" Font-Names="Helvetica" Font-Size="14px"></asp:Label>
                    <asp:Label runat="server" Text="Laboratory Order" Font-Bold="true" Font-Italic="true" Font-Names="Helvetica" Font-Size="14px"></asp:Label>
                    <asp:Label runat="server" Text="):" Font-Bold="true" Font-Names="Helvetica" Font-Size="14px"></asp:Label>
                    <table id="tableSOAP_FO">
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
                                <asp:Label runat="server" Text="CITO" Font-Bold="true" Font-Underline="true"></asp:Label>
                            </td>
                            <td>
                                <asp:Label runat="server" ID="lblNoCito_FO" Text="(tidak ada order)" Visible="true"></asp:Label>
                                <asp:Repeater runat="server" ID="repeatCito_FO">
                                    <ItemTemplate>
                                        <asp:Label runat="server" Text="○"></asp:Label>
                                        <asp:Label runat="server" ID="lblCITO"><%# Eval("item_name") %> <%# Eval("fasting_flag").ToString()=="0" ? "" : Eval("fasting_flag").ToString()=="1" ? "*" : "**" %> </asp:Label>
                                        <br />
                                    </ItemTemplate>
                                </asp:Repeater>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label runat="server" Text="Clinical Pathology" Font-Bold="true"></asp:Label>
                            </td>
                            <td>
                                <asp:Label runat="server" ID="lblNoClinicalPathology_FO" Text="(tidak ada order)" Visible="true"></asp:Label>
                                <asp:Repeater runat="server" ID="repeatClinical_FO">
                                    <ItemTemplate>
                                        <asp:Label runat="server" Text="○"></asp:Label>
                                        <asp:Label runat="server" ID="lblClinicalPathology"> <%# Eval("item_name") %> <%# Eval("fasting_flag").ToString()=="0" ? "" : Eval("fasting_flag").ToString()=="1" ? "*" : "**" %> </asp:Label>
                                        <br />
                                    </ItemTemplate>
                                </asp:Repeater>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label runat="server" Text="Microbiology" Font-Bold="true"></asp:Label>
                            </td>
                            <td>
                                <asp:Label runat="server" ID="lblNoMicroB_FO" Text="(tidak ada order)" Visible="true"></asp:Label>
                                <asp:Repeater runat="server" ID="repeatMicrobiology_FO">
                                    <ItemTemplate>
                                        <asp:Label runat="server" Text="○"></asp:Label>
                                        <asp:Label runat="server" ID="lblMicrobiology"><%# Eval("item_name") %> <%# Eval("fasting_flag").ToString()=="0" ? "" : Eval("fasting_flag").ToString()=="1" ? "*" : "**" %> </asp:Label>
                                        <br />
                                    </ItemTemplate>
                                </asp:Repeater>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label runat="server" Text="Anatomical Pathology" Font-Bold="true"></asp:Label>
                            </td>
                            <td>
                                <asp:Label runat="server" ID="lblNoAnatomicalPathology_FO" Text="(tidak ada order)" Visible="true"></asp:Label>
                                <asp:Repeater runat="server" ID="repeatAnatomical_FO">
                                    <ItemTemplate>
                                        <asp:Label runat="server" Text="○"></asp:Label>
                                        <asp:Label runat="server" ID="lblAnatomicalPathology"><%# Eval("item_name") %> <%# Eval("fasting_flag").ToString()=="0" ? "" : Eval("fasting_flag").ToString()=="1" ? "*" : "**" %> </asp:Label>
                                        <br />
                                    </ItemTemplate>
                                </asp:Repeater>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label runat="server" Text="MDC" Font-Bold="true"></asp:Label>
                            </td>
                            <td>
                                <asp:Label runat="server" ID="lblNoMDC_FO" Text="(tidak ada order)" Visible="true"></asp:Label>
                                <asp:Repeater runat="server" ID="repeatMDC_FO">
                                    <ItemTemplate>
                                        <asp:Label runat="server" Text="○"></asp:Label>
                                        <asp:Label runat="server" ID="lblMDC"><%# Eval("item_name") %> <%# Eval("fasting_flag").ToString()=="0" ? "" : Eval("fasting_flag").ToString()=="1" ? "*" : "**" %> </asp:Label>
                                        <br />
                                    </ItemTemplate>
                                </asp:Repeater>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label runat="server" Text="Panel & Others" Font-Bold="true"></asp:Label>
                            </td>
                            <td>
                                <asp:Label runat="server" ID="lblNoPanel_FO" Text="(tidak ada order)" Visible="true"></asp:Label>
                                <asp:Repeater runat="server" ID="repeatPanel_FO">
                                    <ItemTemplate>
                                        <asp:Label runat="server" Text="○"></asp:Label>
                                        <asp:Label runat="server" ID="lblPanelOthers"><%# Eval("item_name") %> <%# Eval("fasting_flag").ToString()=="0" ? "" : Eval("fasting_flag").ToString()=="1" ? "*" : "**" %> </asp:Label>
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
                                <asp:Label runat="server" ID="lblWarn_FO" Text="*order lain-lain dapat ditujukan untuk pemeriksaan lab/rad ataupun pemeriksaan lainnya" Font-Bold="true" Visible="false"></asp:Label>
                                <br />
                                <asp:Label runat="server" ID="lblNoOthers_FO" Text="(tidak ada order)" Visible="true"></asp:Label>
                                <asp:Repeater runat="server" ID="repeatOthers_FO">
                                    <ItemTemplate>
                                        <asp:Label runat="server" Text="○"></asp:Label>
                                        <asp:Label runat="server" ID="lblOthers"><%# Eval("item_name") %> </asp:Label>
                                        <br />
                                    </ItemTemplate>
                                </asp:Repeater>
                            </td>
                        </tr>
                    </table>
                    <br />
                    <div>
                        <asp:Label runat="server" ID="lblCatatan_FO" Text="Catatan:" Font-Bold="true" Font-Italic="true" Font-Size="Medium"></asp:Label>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
            <br />
            <br />
            <br />
            <br />
            <div class="row">

                <div class="col-sm-12">
                    <table border="0" style="width: 100%;">
                        <tr>
                            <td style="width: 45%; vertical-align: bottom;">
                                <label id="lblbhs_puasa0_clinicallab_FO" style="font-size: 12px;"><b>Keterangan</b></label>
                                <br />
                                <label id="lblbhs_puasa1_clinicallab_FO" style="font-size: 11px; font-style: normal;">&#42 Puasa 10-12 jam</label>
                                <br />
                                <label id="lblbhs_puasa2_clinicallab_FO" style="font-size: 11px; font-style: normal;">&#42&#42 Puasa 12-14 jam</label>
                                <br />
                            </td>
                            <td style="width: 10%;"></td>
                            <td style="width: 45%; vertical-align: bottom; padding-left: 10%;">
                                <asp:Label runat="server" ID="lblNamaDokter_FO"></asp:Label>
                                <hr style="margin-top: 0%; margin-bottom: 0%" />
                                <asp:Label runat="server" ID="lblDokterKet_FO" Text="Dokter ("></asp:Label>
                                <asp:Label runat="server" ID="Label1_FO" Text="Physician" Font-Italic="true"></asp:Label>
                                <asp:Label runat="server" ID="Label2_FO" Text=")"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </div>
                <div class="col-sm-12" style="margin-top: 10px;">
                    <table class="tableParaf">
                        <tr>
                            <td colspan="3" style="text-align: center; font-weight: bold; padding: 2px;">OTORISASI PETUGAS</td>
                        </tr>
                        <tr>
                            <td style="text-align: center; width: 35px; font-weight: bold;">Tugas</td>
                            <td style="text-align: center; width: 40px; font-weight: bold;">Nama/Paraf</td>
                            <td style="text-align: center; width: 25px; font-weight: bold;">Tgl/Jam</td>
                        </tr>
                        <tr>
                            <td style="padding-left: 5px;">Phlebotomist</td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td style="padding-left: 5px;">Distribusi Sampel</td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td style="padding-left: 5px;">Print Hasil</td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td style="padding-left: 5px;">Cek Hasil</td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                        </tr>
                    </table>
                </div>
            </div>
        </div>
    </form>



    <script type="text/javascript">
</script>
</body>
</html>

