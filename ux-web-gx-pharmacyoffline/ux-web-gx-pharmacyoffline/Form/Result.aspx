<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Result.aspx.cs" Inherits="Form_Result" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <!-- Bootstrap 3.3.6 -->
    <link rel="stylesheet" href="~/Content/bootstrap/css/bootstrap.css" />
    <%--<!-- Font Awesome -->
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
    <link rel="stylesheet" href="~/Content/EMR-Doctor.css" />--%>
</head>
<body>
    <form id="form1" runat="server" style="overflow:hidden;">
        <style type="text/css">            
        .btn-lightGreen {
            background-color: #4d9b35;
            color: white;
        }

        .btn-lightGreen:hover {
            background-color: forestgreen;
            color: white;
        }
        </style>
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
        <div style="background-color:white; height:50px;">
            <asp:HiddenField ID="orgId" runat="server" />
            <div class="row" style="background-color: white; border-bottom: 1px solid lightgrey; height:50px; padding-left: 15px;">
                <div class="col-sm-3" style="display: inline; height: 50px; border-right: 1px dashed lightgrey; padding-top: 11px;">
                    <label>Mr No. </label>
                    <asp:TextBox ID="src_patient_id" runat="server" Style="height: 26px; width: 55%;" placeholder="Search here... "></asp:TextBox>
                    <asp:Button ID="BtnSearchMR" runat="server" Text="Search" CssClass="btn btn-lightGreen btn-sm" Style="height: 26px; width: 20%; padding-top: 3px;" OnClick="BtnSearchMR_Click" />
                </div>
                <div class="col-sm-9" style="display: inline; height: 50px; border-left: 1px dashed lightgrey; padding-top: 5px; margin-left: -1px;">
                    <div runat="server" id="divLine" style="display:none">
                        <div>
                        <asp:Label ID="lblNama" runat="server" Font-Bold="true" Font-Names="Helvetica" Font-Size="15px"></asp:Label>
                        </div>
                        <div>
                            <asp:Label ID="lblDOBJudul" runat="server" Text="Tgl. Lahir" Font-Names="Helvetica" Font-Size="12px"></asp:Label>
                            <asp:Label ID="lblDOB" runat="server" Font-Bold="true" Font-Names="Helvetica" Font-Size="12px"></asp:Label>
                            &nbsp;
                            &nbsp;
                            <asp:Label ID="lblAgeJudul" runat="server" Text="Umur" Font-Names="Helvetica" Font-Size="12px"></asp:Label>
                            <asp:Label ID="lblAge" runat="server" Font-Bold="true" Font-Names="Helvetica" Font-Size="12px"></asp:Label>
                            &nbsp;
                            &nbsp;
                            <asp:Label ID="lblReligionJudul" runat="server" Text="Agama" Font-Names="Helvetica" Font-Size="12px"></asp:Label>
                            <asp:Label ID="lblReligion" runat="server" Font-Bold="true" Font-Names="Helvetica" Font-Size="12px"></asp:Label>
                            &nbsp;
                            &nbsp;
                            <asp:Label ID="lblGenderJudul" runat="server" Text="Gender" Font-Names="Helvetica" Font-Size="12px"></asp:Label>
                            <asp:Image ID="ImageICMale" runat="server" ImageUrl="~/Images/Worklist/ic_Male.png" style="height:16px; padding-bottom:2px;" Visible="false" />
                            <asp:Image ID="ImageICFemale" runat="server" ImageUrl="~/Images/Worklist/ic_Female.png" style="height:15px; padding-bottom:2px;" Visible="false" />
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div style="background-color:#e7e8ed">
            <iframe name="myLabRadIframe" id="myLabRadIframe" runat="server" style="width: 100%; height: calc(100vh - 50px); border: none; margin-bottom: -6px;"></iframe>
        </div>
    </form>
</body>
</html>
