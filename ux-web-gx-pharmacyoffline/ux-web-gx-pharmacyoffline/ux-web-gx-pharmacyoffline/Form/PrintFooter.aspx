<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PrintFooter.aspx.cs" Inherits="Form_PrintFooter" %>

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
                position: fixed;
                bottom: 0px;
                left: 5px;
                width: 100%
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
        </style>

        <div>
            <div id="mydiv">
                <table  style="width:100%">
                    <tr>
                        <td style="width:70%">
                            <asp:Label runat="server" Text="Printed By : " Font-Size="10px"></asp:Label>
                            <asp:Label runat="server" ID="lblPrintedBy" Font-Size="10px"></asp:Label>
                            <%--<br />--%>
                            <asp:Label runat="server" Text="/"></asp:Label>
                            <asp:Label runat="server" Text="Print Date :" Font-Size="10px"></asp:Label>
                            <asp:Label runat="server" ID="lblPrintDate" Font-Size="10px"></asp:Label>
                        </td>
                        <td style="width: 30%; text-align: right; padding-right: 15px;">
                            <asp:Label runat="server" Text="Dokumen ini ditandatangani secara digital" Font-Size="10px" Font-Italic="true"></asp:Label>
                        </td>
                    </tr>
                </table>
            </div>
        </div>
    </form>



    <script type="text/javascript">
</script>
</body>
</html>

