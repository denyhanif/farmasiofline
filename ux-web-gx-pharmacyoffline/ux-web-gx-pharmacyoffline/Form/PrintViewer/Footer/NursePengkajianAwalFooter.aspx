<%@ Page Language="C#" AutoEventWireup="true" CodeFile="NursePengkajianAwalFooter.aspx.cs" Inherits="Form_PrintViewer_Footer_NursePengkajianAwalFooter" %>

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


    <!-- Mask -->
    <link rel="stylesheet" href="~/Content/plugins/jasny-bootstrap/css/jasny-bootstrap.min.css" />
    <!-- Theme style -->
    <link rel="stylesheet" href="~/Content/dist/css/AdminLTE.css" />
    <link rel="stylesheet" href="~/Content/dist/css/skins/skin-blue-light.css" />
    <link rel="stylesheet" href="~/Content/Site.css" />

    <link href="favicon.ico" rel="shortcut icon" type="image/x-icon" />
    <link href="~/favicon.ico" rel="shortcut icon" type="image/x-icon" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />


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

        /*td {
            padding-top: 10px;
            padding-bottom: 10px;
        }*/

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

        .tbl-nurse td {
            font-size: 14px;
        }

        .bg-kolom-title {
            background-color: #f4f4f4;
            padding: 5px 10px 5px 10px;
            vertical-align: top;
        }

        .bg-kolom-content {
            background-color: #ffffff;
            padding: 5px 10px 5px 10px;
        }

        .tblFooterPosition {
                position: fixed;
                bottom: 0px;
                width: 100%
            }
    </style>

</head>

<script type="text/javascript">
    function printpreview() {
        window.print();

        return false;
    }
</script>

<body style="padding-top: 15px;">
    <form id="form1" runat="server">

        <asp:ScriptManager ID="ScriptManager1" runat="server">
            <Scripts>
                <asp:ScriptReference Path="~/Content/plugins/jQuery/jQuery-2.2.0.min.js" />
                <asp:ScriptReference Path="~/Content/bootstrap/js/bootstrap.min.js" />
                <asp:ScriptReference Path="~/Content/plugins/jasny-bootstrap/js/jasny-bootstrap.min.js" />
                <asp:ScriptReference Path="~/Content/dist/js/app.min.js" />
            </Scripts>
        </asp:ScriptManager>

        <div id="tblFooter" style="padding: 10px;" class="tblFooterPosition">
            <table border="0" style="width: 100%">
                <tr>
                    <td style="width: 70%; text-align: left;">
                        <asp:Label runat="server" Text="Print by : " Font-Size="10px"></asp:Label>
                        <asp:Label runat="server" ID="lblPrintedBy" Font-Size="10px"></asp:Label>
                       
                        <asp:Label runat="server" Text=", "></asp:Label>
                        <asp:Label runat="server" ID="lblPrintDate" Font-Size="10px"></asp:Label>
                    </td>
                    <td style="width: 30%; text-align: right;">
                        <asp:Label runat="server" Text="Dokumen ini ditandatangani secara digital" Font-Size="10px" Font-Italic="true"></asp:Label>
                    </td>
                </tr>
            </table>
        </div>

    </form>
</body>
</html>
