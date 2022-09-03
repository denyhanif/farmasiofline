<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ReferralResumePrint.aspx.cs" Inherits="Form_ReferralResumePrint" %>


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
            #mydiv {
                margin-top: 10%;
                position: fixed;
                bottom: 0px;
                left: 5px
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

            .itemtable-priview-title {
                border-right: solid 1px #cdced9;
                vertical-align: top;
                padding-top: 10px;
                padding-bottom: 10px;
                padding-left:5px;
                padding-right:5px;
            }

            .itemtable-priview {
                border-right: solid 1px #cdced9;
                vertical-align: top;
                padding: 10px;
            }

            .wordbreak {
                word-break: break-word;
                text-align: center;
            }

            .contentTable {
                font-size: 14px;
            }

            .table-header-racikan {
                border: 1px solid #CDCED9;
            }

            .table-header-racikan th {
                border: 1px solid #CDCED9;
            }

            .table-header-racikan td {
                border: 1px solid #CDCED9;
            }

            .table-detail-racikan {
                width: 100%;
                border: 0;
            }

                .table-detail-racikan th {
                    border: 0;
                }

                .table-detail-racikan td {
                    border: 0;
                }



            .header-table {
                border: 0;
            }

                .header-table th {
                    border: 0;
                }

                .header-table td {
                    border: 0;
                }
        </style>

        <div>
            <asp:UpdatePanel runat="server">
                <ContentTemplate>
                    <asp:HiddenField runat="server" ID="hfpreviewpres" />

                    <asp:Repeater ID="RptReferralPrint" runat="server">
                        <ItemTemplate>
                             <%-- =============================================== START DATE SECTION ==================================================== --%>
                            <table style="width: 100%;">
                                <asp:Label runat="server" style="font-size: 20px; font-weight: bold;" Visible='<%#Eval("RefType").ToString().ToLower() == "balasan" ? true : false %>'>BALASAN </asp:Label>
                                <asp:Label runat="server" style="font-size: 20px; font-weight: bold;">PERMOHONAN KONSULTASI</asp:Label>
                                <br />
                                <tbody>
                                    <tr style="border: solid 1px #cdced9; width: 100%;">
                                        <td style="width: 35%; background-color: #efefef;" class="itemtable-priview-title">
                                            <label style=" font-weight: bold; font-size: 14px">Kepada TS</label>
                                        </td>
                                        <td style="width: 65%;" colspan="2" class="itemtable-priview">
                                            <asp:Label runat="server" ID="lblKepadaTS" Style="font-size: 14px;" Text='<%#Eval("DoctorReferral") %>'></asp:Label>
                                        </td>
                                    </tr>
                                </tbody>
                                <tbody style='<%# Eval("RefType").ToString().ToLower() == "balasan" ? "" : "display:none" %>'>
                                    <tr style="border: solid 1px #cdced9; width: 100%;">
                                        <td style="width: 35%; background-color: #efefef;" class="itemtable-priview-title">
                                            <label style=" font-weight: bold; font-size: 14px">Dari</label>
                                        </td>
                                        <td style="width: 65%;" colspan="2" class="itemtable-priview">
                                            <asp:Label runat="server" ID="lblDari" Style="font-size: 14px;" Text='<%#Eval("DoctorName") %>' ></asp:Label>
                                        </td>
                                    </tr>
                                </tbody>

                            </table>
                            <%-- =============================================== END DATE SECTION ==================================================== --%>
                           <br />
                            <table style="width: 100%;">
                                <label style="font-size: 14px;">Mohon konsultasi dan tindak lanjut dengan :</label>
                                <br />
                                <tbody>
                                    <tr style="border: solid 1px #cdced9; width: 100%;">
                                        <td style="width: 35%; background-color: #efefef;" class="itemtable-priview-title">
                                            <label style=" font-weight: bold; font-size: 14px">Keluhan Utama</label>
                                        </td>
                                        <td style="width: 65%;" colspan="2" class="itemtable-priview">
                                            <asp:Label runat="server" ID="lblS" Style="font-size: 14px;" Text=""><%# Eval("Subjective").ToString().Replace("\\n","<br />") %> </asp:Label>
                                        </td>
                                    </tr>
                                </tbody>
                                <tbody>
                                    <tr style="border: solid 1px #cdced9; width: 100%;">
                                        <td style="width: 35%; background-color: #efefef;" class="itemtable-priview-title">
                                            <label style=" font-weight: bold; font-size: 14px">Hasil Pemeriksaan Yang Ditemukan</label>
                                        </td>
                                        <td style="width: 65%;" colspan="2" class="itemtable-priview">
                                            <asp:Label runat="server" ID="lblO" Style="font-size: 14px;" Text=""><%# Eval("Objective").ToString().Replace("\\n","<br />") %> </asp:Label>
                                        </td>
                                    </tr>
                                </tbody>
                                <tbody>
                                    <tr style="border: solid 1px #cdced9; width: 100%;">
                                        <td style="width: 35%; background-color: #efefef;" class="itemtable-priview-title">
                                            <label style=" font-weight: bold; font-size: 14px">Diagnosa</label>
                                        </td>
                                        <td style="width: 65%;" colspan="2" class="itemtable-priview">
                                            <asp:Label runat="server" ID="lblA" Style="font-size: 14px;" Text=""><%# Eval("Diagnosis").ToString().Replace("\\n","<br />") %> </asp:Label>
                                        </td>
                                    </tr>
                                </tbody>
                                <tbody>
                                    <tr style="border: solid 1px #cdced9; width: 100%;">
                                        <td style="width: 35%; background-color: #efefef;" class="itemtable-priview-title">
                                            <label style=" font-weight: bold; font-size: 14px">Perencanaan Dan Tindakan</label>
                                        </td>
                                        <td style="width: 65%; padding: 0;" colspan="2" class="itemtable-priview">
                                            <div style="border-bottom: solid 1px #cdced9">
                                                <table style="width: 100%;">
                                                    <tr>
                                                        <td class="itemtable-priview" style="border-right: 0px;">
                                                            <asp:Label runat="server" ID="lblP" Style="font-size: 14px;" Text=""><%# Eval("PlanningProcedure").ToString().Replace("\\n","<br />") %></asp:Label>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </div>
                                            <table style="width: 100%;">
                                                <tr style="width: 100%;">
                                                     <td style="width: 50%;" class="itemtable-priview">
                                                        <asp:Label Font-Size="14px" Font-Bold="true" ID="Label21" runat="server" Text="Order Lab :"></asp:Label>
                                                        <br/>
                                                         <asp:Label Font-Size="14px" ID="Label22" runat="server" Text=""> <%# Eval("OrderLab").ToString().Replace("\\n","<br />") %> </asp:Label>
                                                    </td>
                                                    <td style="width: 50%;" class="itemtable-priview">
                                                        <asp:Label Font-Size="14px" Font-Bold="true" ID="Label23" runat="server" Text="Order Rad :"></asp:Label>
                                                        <br/>
                                                        <asp:Label Font-Size="14px" ID="Label24" runat="server" Text=""> <%# Eval("OrderRad").ToString().Replace("\\n","<br />") %> </asp:Label>
                                                    </td>
                                                 </tr>
                                            </table>
                                            <div style="border-top: solid 1px #cdced9">
                                                <table style="width: 100%;">
                                                    <tr>
                                                        <td class="itemtable-priview" style="border-right: 0px;">
                                                            <asp:Label runat="server" ID="Label1" Style="font-size: 14px;" Text=""><%# Eval("Prescription").ToString().Replace("\\n","<br />").Replace("\\ba","<b>").Replace("\\be","</b>") %> </asp:Label>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </div>
                                        </td>
                                    </tr>
                                </tbody>
                                <tbody style='<%# Eval("RefType").ToString().ToLower() == "balasan" ? "display:none" : "" %>'>
                                    <tr style="border: solid 1px #cdced9; width: 100%;">
                                        <td style="width: 35%; background-color: #efefef;" class="itemtable-priview-title">
                                            <label style=" font-weight: bold; font-size: 14px">Remark</label>
                                        </td>
                                        <td style="width: 65%;" colspan="2" class="itemtable-priview">
                                            <asp:Label runat="server" ID="lblRemark" Style="font-size: 14px;" Text=""><%# Eval("referral_remark").ToString().Replace("\\n","<br />") %> </asp:Label>
                                        </td>
                                    </tr>
                                </tbody>
                            </table>

                            <br />
                             <%-- =============================================== START SIGN ==================================================== --%>
                            <table style="width: 100%;">
                                <tbody>
                                    <tr style="width: 100%">
                                        <td style="width: 25%"></td>
                                        <td colspan="2" style="width: 75%; padding-left: 40%">
                                            <br />
                                            <br />
                                            <br />
                                            <br />
                                            <asp:Label runat="server" ID="lblNamaDokter" Text='<%#Eval("DoctorName") %>'></asp:Label>
                                            <hr style="margin-top: 0%; margin-bottom: 0%" />
                                            <asp:Label runat="server" ID="lblDokterKetRef" Visible='<%#Eval("RefType").ToString().ToLower() == "balasan" ? false : true %>' Text="Dokter yang merawat"></asp:Label>
                                            <asp:Label runat="server" ID="lblDokterKetBalas" Visible='<%#Eval("RefType").ToString().ToLower() == "balasan" ? true : false %>' Text="Dokter penerima konsultasi"></asp:Label>

                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                            <%-- =============================================== END SIGN ==================================================== --%>
                        </ItemTemplate>
                    </asp:Repeater>
    
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>

    </form>
</body>
</html>
