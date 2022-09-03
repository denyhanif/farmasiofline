<%@ Page Language="C#" AutoEventWireup="true" CodeFile="HeaderPrint.aspx.cs" Inherits="Form_HeaderPrint" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <!-- Tell the browser to be responsive to screen width -->
    <meta content="width=device-width, initial-scale=1, maximum-scale=1, user-scalable=no" name="viewport" />
    <!-- Bootstrap 3.3.6 -->
    <link rel="stylesheet" href="~/Content/bootstrap/css/bootstrap.css" />


</head>
<body style="padding-top: 20px; padding-bottom: 20px">
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
            <Scripts>
                <asp:ScriptReference Path="~/Content/plugins/jQuery/jQuery-2.2.0.min.js" />
                <asp:ScriptReference Path="~/Content/bootstrap/js/bootstrap.min.js" />
            </Scripts>
        </asp:ScriptManager>
        <div>
            <table style="width: 100%;">
                <tr>
                    <td style="width: 50%; vertical-align: top;">
                        <asp:Image ID="ImgLogoSH" ImageUrl="~/Images/Icon/logo-SH.png" runat="server" Style="width: 350px;" Enabled="false" /> <br />
                        <asp:Label ID="LabelUnitAddress" runat="server" Text=""></asp:Label>
                    </td>
                    <td style="width: 50%;">
                        <table style="width: 100%; display: inline-table;">
                            <tr>
                                <td style="padding-right: 30px; vertical-align: top; width: 170px;">
                                    <label style="font-size: 14px">MR</label>
                                </td>
                                <td>
                                    <label>: </label>
                                    <asp:Label runat="server" ID="lblmrno" Style="font-size: 14px"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td style="padding-right: 30px; vertical-align: top">
                                    <label style="font-size: 14px">Name (Nama)</label>
                                </td>
                                <td>
                                    <label>: </label>
                                    <asp:Label runat="server" ID="lblnamepatient" Style="font-size: 14px"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td style="padding-right: 30px; vertical-align: top">
                                    <label style="font-size: 14px">DOB/Age (TTL/Umur)</label>
                                </td>
                                <td>
                                    <label>: </label>
                                    <asp:Label runat="server" ID="lbldobpatient" Style="font-size: 14px"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td style="padding-right: 30px; vertical-align: top">
                                    <label style="font-size: 14px">Sex (Jenis Kelamin)</label>
                                </td>
                                <td>
                                    <label>: </label>
                                    <asp:Label runat="server" ID="lblsexpatient" Style="font-size: 14px"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td style="padding-right: 30px; vertical-align: top">
                                    <label style="font-size: 14px">Doctor (Dokter)</label>
                                </td>
                                <td>
                                    <label>: </label>
                                    <asp:Label runat="server" ID="lbldoctorprimary" Style="font-size: 14px"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td style="padding-right: 30px; vertical-align: top">
                                    <label style="font-size: 14px">Adm No. (No. Adm)</label>
                                </td>
                                <td>
                                    <label>: </label>
                                    <asp:Label runat="server" ID="lblAdmission" Style="font-size: 14px"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td style="padding-right: 30px; vertical-align: top">
                                    <label style="font-size: 14px">Payer (Payer)</label>
                                </td>
                                <td>
                                    <label>: </label>
                                    <asp:Label runat="server" ID="lblpayername" Style="font-size: 14px"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </div>

    </form>
</body>
</html>
