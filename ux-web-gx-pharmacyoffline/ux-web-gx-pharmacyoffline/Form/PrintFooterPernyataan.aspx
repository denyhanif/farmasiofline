<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PrintFooterPernyataan.aspx.cs" Inherits="Form_PrintFooterPernyataan" %>

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
                <asp:ScriptReference Path="~/Content/dist/js/app.min.js" />
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
        </style>

        <div id="mydiv">

            <div style="font-size:14px; padding-right:10px;">
                <div style="text-align: justify; margin-bottom: 7px;">
                    <br />
                    Dengan ini saya memberikan kuasa kepada Siloam Hospitals untuk menyimpan dan memberikan segala keterangan / catatan medis dan lainnya
                untuk keperluan klaim kepada Perusahaan / Asuransi tersebut diatas sesuai kebutuhan polis / perusahaan. Saya bersedia membayar kepada pihak
                Rumah Sakit pada saat :
                </div>

                1. Biaya Pemakaian saya sudah melebihi limit dan / atau tidak ditanggung di dalam polis / ketentuan perusahaan.
                <br />
                2. Belum ada jawaban resmi dari perusahaan / asuransi dan pelayanan bersifat emergency & urgent (life saving).
                <br />

                <br />
                <div style="text-align: justify; margin-bottom: 7px;">
                    <i>I herewith, authorized Siloam Hospitals to release all Medical Records and other data to Corporate / Insurance Company for claiming purposes in
                accordance with the Insurance Policy or Company rule. I will pay the Hospital if :</i>
                </div>

                <i>1. The actual billing is over my limit and / or not borned in the Insurance Policy / Company's rule.
                    <br />
                    2. There is no official reply from Company / Insurance and I am really need Emergency & Urgent Medical Service (life saving).
                    <br />
                </i>
            </div>

            <div class="row">

                <table style="width: 100%; margin-top: 80px; margin-bottom:10px;">
                    <tr>
                        <td style="width: 50%; padding-left: 2%; padding-top: 20px">
                            <asp:Label runat="server" ID="lblNamaDokter"></asp:Label>
                            <hr style="margin-top: 0%; margin-bottom: 0%" />
                            <asp:Label runat="server" ID="lblDokterKet" Text="Dokter(Physician)"></asp:Label>
                        </td>
                        <td style="width: 50%; padding-left: 2%; padding-right:2%; padding-top: 40px">
                            <hr style="margin-top: 0%; margin-bottom: 0%" />
                            <label>Pasien(Patient)</label>
                        </td>
                    </tr>
                </table>

            </div>

            <table style="width: 100%">
                <tr>
                    <td style="width: 70%;">
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
    </form>

</body>
</html>

