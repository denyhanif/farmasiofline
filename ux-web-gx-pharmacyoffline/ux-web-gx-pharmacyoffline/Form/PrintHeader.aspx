<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PrintHeader.aspx.cs" Inherits="Form_PrintHeader" %>

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

        </style>

            <asp:HiddenField runat="server" ID="hfOrgID" />
            <asp:HiddenField runat="server" ID="hf_admiss_id" />
            <asp:HiddenField runat="server" ID="hf_patient_id" />
            <asp:HiddenField runat="server" ID="hf_ticket_patient" />
            <asp:HiddenField ID="HiddenLabMark" runat="server" />

        <div>
            <asp:UpdatePanel runat="server">
                <ContentTemplate>
                    <div class="row" style="text-align:center">
                        <asp:Image ID="ImgLogoSH" runat="server" ImageUrl="~/Images/Icon/logo-SH.png" style="width:250px; margin-bottom:5px;" Enabled="false" />
                        <br />
                        <asp:Label runat="server" ID="lblTitle" Text="RESUME MEDIS" Font-Size="14px" Font-Bold="true" Font-Names="Helvetica"></asp:Label>
                        <asp:Label runat="server" ID="lblTitleEng" Text="(MEDICAL RESUME)" Font-Size="14px" Font-Bold="true" Font-Names="Helvetica" Font-Italic="true"></asp:Label>
                    </div>
                    <br />
                    <br />
                    <div class="row" style="margin-left:55%">
                        <div class="col-sm-12" >
                            <table border="0" id="tbl_header_print" class="tbl_worklist">
                                <tr>
                                    <td style="width: 20%">MR </td>
                                    <td style="width: 5%">: </td>
                                    <td style="width: 75%"><asp:Label runat="server" ID="lblMR"></asp:Label></td>
                                </tr>
                                <tr>
                                    <td style="width: 20%">Name (Nama) </td>
                                    <td style="width: 5%">: </td>
                                    <td style="width: 75%"><asp:Label runat="server" ID="lblNama"></asp:Label></td>
                                </tr>
                                <tr>
                                    <td style="width: 20%">DOB/Age (TTL/Umur) </td>
                                    <td style="width: 5%">: </td>
                                    <td style="width: 75%"><asp:Label runat="server" ID="lblUmur"></asp:Label></td>
                                </tr>
                                <tr>
                                    <td style="width: 20%">Sex (Seks) </td>
                                    <td style="width: 5%">: </td>
                                    <td style="width: 75%"><asp:Label runat="server" ID="lblSeks"></asp:Label></td>
                                </tr>
                                <tr>
                                    <td style="width: 20%">Doctor (Dokter) </td>
                                    <td style="width: 5%">: </td>
                                    <td style="width: 75%"><asp:Label runat="server" ID="lblDokter"></asp:Label></td>
                                </tr>
                                <tr>
                                    <td style="width: 20%">Adm No. (No. Adm) </td>
                                    <td style="width: 5%">: </td>
                                    <td style="width: 75%"><asp:Label runat="server" ID="lblAdmission"></asp:Label></td>
                                </tr>
                            </table>
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </form>



    <script type="text/javascript">
    </script>
</body>
</html>

