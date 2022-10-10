<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FormPengkajianAwal.aspx.cs" Inherits="Form_FormViewer_FormPengkajianAwal" %>

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
    <!-- Datepicker -->
    <link rel="stylesheet" href="~/Content/plugins/datepicker/datepicker3.css" />
    <!-- Mask -->
    <link rel="stylesheet" href="~/Content/plugins/jasny-bootstrap/css/jasny-bootstrap.min.css" />
    <!-- Theme style -->
    <link rel="stylesheet" href="~/Content/dist/css/AdminLTE.css" />
    <link rel="stylesheet" href="~/Content/dist/css/skins/skin-blue-light.css" />
    <link rel="stylesheet" href="~/Content/EMR-Doctor.css" />
    <link rel="stylesheet" href="~/Content/EMR-PharmacyOffline.css" />
    <!-- Select Bootstrap -->
    <link rel="stylesheet" href="~/Content/bootstrap-select/css/bootstrap-select.css" />

    <link href="~/favicon.ico" rel="shortcut icon" type="image/x-icon" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />

    <style type="text/css">
        
    </style>
</head>

<body>
    <form id="form1" runat="server">

        <asp:ScriptManager ID="ScriptManager1" runat="server">
            <Scripts>
                <asp:ScriptReference Path="~/Content/plugins/jQuery/jQuery-2.2.0.min.js" />
                <asp:ScriptReference Path="~/Content/bootstrap/js/bootstrap.min.js" />
                <asp:ScriptReference Path="~/Content/plugins/jasny-bootstrap/js/jasny-bootstrap.min.js" />
                <asp:ScriptReference Path="~/Content/plugins/datepicker/bootstrap-datepicker.js" />
                <asp:ScriptReference Path="~/Content/dist/js/app.min.js" />
                <asp:ScriptReference Path="~/Content/bootstrap-select/js/bootstrap-select.js" />
            </Scripts>
        </asp:ScriptManager>

        <%--YOUR CODE HTML HERE--%>

        <script type="text/javascript">
            function dateStart() {
                var dp = $('#<%=DateTextboxStart.ClientID%>');
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
            function dateEnd() {
                var dp = $('#<%=DateTextboxEnd.ClientID%>');
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

            function TabTab() {
                const urlParams = new URLSearchParams(window.location.search);
                const myParam = urlParams.get('Tab');

                if (myParam == "FA") {
                    var tab22 = document.getElementById("tab2");
                    var contenttab22 = document.getElementById("contenttab2");
                    tab22.style.display = "none";
                    contenttab22.style.display = "none";

                    kliktab('1');
                }
                else if (myParam == "RM") {
                    var tab11 = document.getElementById("tab1");
                    var contenttab11 = document.getElementById("contenttab1");
                    tab11.style.display = "none";
                    contenttab11.style.display = "none";

                    kliktab('2');
                }
            }

            $(document).ready(function () {

                TabTab();

                //fungsi untuk mempertahankan javascript saat postback di updatepanel
                var prm = Sys.WebForms.PageRequestManager.getInstance();
                if (prm != null) {

                    prm.add_beginRequest(function (sender, e) {
                        if (sender._postBackSettings.panelsToUpdate != null) {
                            //code
                        }
                    });

                    prm.add_endRequest(function (sender, e) {
                        if (sender._postBackSettings.panelsToUpdate != null) {
                            //code
                            kliktab(document.getElementById('<%= HFFlagTab.ClientID %>').value);
                        }
                    });
                };
            });

            function kliktab(param) {
                var tab11 = document.getElementById("tab1");
                var tab22 = document.getElementById("tab2");

                var contenttab11 = document.getElementById("contenttab1");
                var contenttab22 = document.getElementById("contenttab2");

                if (param == "1") {
                    tab11.classList.remove('tabstyle-deactive');
                    tab11.classList.add('tabstyle-active');

                    tab22.classList.remove('tabstyle-active');
                    tab22.classList.add('tabstyle-deactive');

                    contenttab11.style.display = "";
                    contenttab22.style.display = "none";

                }
                else if (param == "2") {
                    tab22.classList.remove('tabstyle-deactive');
                    tab22.classList.add('tabstyle-active');

                    tab11.classList.remove('tabstyle-active');
                    tab11.classList.add('tabstyle-deactive');

                    contenttab22.style.display = "";
                    contenttab11.style.display = "none";
                }
            }

            function klikEnterCari1(event1) {
                var c = event1.keyCode;
                if (c == 13) {
                    event1.preventDefault();
                //document.getElementById('<%= BtnSearchMR.ClientID %>').click();
                __doPostBack('<%= BtnSearchMR.UniqueID%>', '');
                    return false;
                }
            }

            function klikEnterCari2(event2) {
                var c = event2.keyCode;
                if (c == 13) {
                    event2.preventDefault();
                //document.getElementById('<%= BtnSearchMR2.ClientID %>').click();
                __doPostBack('<%= BtnSearchMR2.UniqueID%>', '');
                    return false;
                }
            }

            function openInNewTab(href) {
                Object.assign(document.createElement('a'), {
                    target: '_blank',
                    href,
                }).click();
            }

            function PreviewPengkajianAwal(admid, encid, ptnid, pagecode) {
                var IP = document.getElementById('<%= HiddenFieldIPAddress.ClientID %>');
            var Organization = document.getElementById('<%= HiddenFieldOrgID.ClientID %>');
            var UserId = document.getElementById('<%= HiddenFieldUserID.ClientID %>');

                //if (IP.value != window.location.hostname) {
                //    IP.value = "10.83.254.38";
                //}
                var url = "http://" + IP.value + "/printingemr?printtype=PengkajianAwalRawatJalan&OrganizationId=" + Organization.value + "&AdmissionId=" + admid + "&EncounterId=" + encid + "&PatientId=" + ptnid + "&PageFA=" + pagecode + "&PrintBy=" + UserId.value + "";

                openInNewTab(url);
            }

        </script>

        <style type="text/css">
            .tabstyle {
                display: inline-flex;
                min-height: 35px;
                padding-top: 10px;
                border-radius: 5px 5px 0px 0px;
                background-color: white;
                padding-left: 15px;
                padding-right: 15px;
                font-weight: bold;
            }

            .tabstyle-active {
                display: inline-flex;
                min-height: 35px;
                padding-top: 7px;
                border-radius: 5px 5px 0px 0px;
                background-color: white;
                padding-left: 15px;
                padding-right: 15px;
                font-weight: bold;
                color: black;
                border-top: 5px solid #2a3593;
            }

            .tabstyle-deactive {
                display: inline-flex;
                min-height: 35px;
                padding-top: 7px;
                border-radius: 5px 5px 0px 0px;
                background-color: #e9e9ef;
                padding-left: 15px;
                padding-right: 15px;
                font-weight: bold;
                color: #74757e;
                border-top: 5px solid #e9e9ef;
            }

            .lblpasien {
                font-weight: bold;
                margin-right: 15px;
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

            .icon-print {
                font-size: 12px;
                background-color: #1172f7;
                padding: 5px;
                border-radius: 50% 50%;
                color: white;
            }
        </style>

        <!-- ##### Content ##### -->

        <asp:UpdatePanel ID="UpdatePanelHF" runat="server">
            <ContentTemplate>
                <asp:HiddenField ID="HFFlagTab" runat="server" />
            </ContentTemplate>
        </asp:UpdatePanel>

        <div style="background-color: #cdced9; padding-left: 25px; padding-top: 10px;">
            <a href="javascript:kliktab('1');">
                <div id="tab1" class="tabstyle-active">Pengkajian Awal</div>
            </a>
            <a href="javascript:kliktab('2');">
                <div id="tab2" class="tabstyle-deactive">Resume Medis</div>
            </a>
        </div>

        <asp:UpdatePanel ID="UpdatePaneltab1" runat="server">
            <ContentTemplate>

                <!-- ##### TAB 1 ##### -->
                <div id="contenttab1" style="background-color: white; min-height: calc(100vh - 95px);">
                    <div id="headercontent" class="container-fluid">
                        <div class="row" style="background-color: white; padding-left: 15px; border-bottom: 1px solid lightgrey;">
                            <div class="col-sm-3" style="display: inline; min-height: 50px; border-right: 1px dashed lightgrey; padding-top: 11px;">
                                <label>Mr No. </label>
                                <asp:TextBox ID="TBoxSearchMR" runat="server" placeholder="Cari disini..." Style="height: 26px; width: 55%;" onkeydown="klikEnterCari1(event);"></asp:TextBox>
                                <asp:Button ID="BtnSearchMR" runat="server" Text="Search" CssClass="btn btn-lightGreen btn-sm" Style="height: 26px; width: 20%; padding-top: 3px;" OnClick="BtnSearchMR_Click" />
                            </div>
                            <div class="col-sm-9" style="display: inline; min-height: 50px; border-left: 1px dashed lightgrey; padding-top: 5px; margin-left: -1px;">
                                <table style="width: 100%;">
                                    <tr>
                                        <td>
                                            <div id="datapasienheader" runat="server">
                                                <asp:Label ID="LblNamaPasien" runat="server" Text="Label" Style="font-weight: bold; font-size: 15px;"></asp:Label>
                                                <br />
                                                <label>Tgl. Lahir : </label>
                                                <asp:Label ID="LblTglLahir" runat="server" Text="Label" CssClass="lblpasien"></asp:Label>
                                                <label>Umur : </label>
                                                <asp:Label ID="LblUmur" runat="server" Text="Label" CssClass="lblpasien"></asp:Label>
                                                <label>Agama : </label>
                                                <asp:Label ID="LblAgama" runat="server" Text="Label" CssClass="lblpasien"></asp:Label>
                                                <%--<label>Gender : </label>
                                                <asp:Image ID="ImageICMale" runat="server" ImageUrl="~/Images/Worklist/ic_Male.png" style="height:16px; padding-bottom:2px;" Visible="false" />
                                                <asp:Image ID="ImageICFemale" runat="server" ImageUrl="~/Images/Worklist/ic_Female.png" style="height:15px; padding-bottom:2px;" Visible="false" />--%>
                                            </div>
                                        </td>
                                        <td style="text-align: right; padding-top: 10px;">
                                            <asp:UpdateProgress ID="upprogprintFA" runat="server" AssociatedUpdatePanelID="UpdatePaneltab1">
                                                <ProgressTemplate>
                                                    <div class="modal-backdrop" style="background-color: white; opacity: 0; text-align: center">
                                                    </div>
                                                    <img alt="" style="background-color: transparent; height: 20px; margin-left: 10px;" src="<%= Page.ResolveClientUrl("~/Images/Background/small-loader.gif") %>" />
                                                </ProgressTemplate>
                                            </asp:UpdateProgress>
                                        </td>
                                    </tr>
                                </table>

                            </div>
                        </div>
                    </div>

                    <div id="datacontent" class="container-fluid" runat="server">
                        <div class="row" style="background-color: white">
                            <div class="col-sm-12 text-right" style="min-height: 40px; padding-top: 10px; padding-bottom: 5px;">
                                <label>Tgl Admisi : </label>
                                <asp:TextBox ID="DateTextboxStart" runat="server" Style="width: 120px; height: 26px;" placeholder="dd mmm yyyy" onmousedown="dateStart();" AutoPostBack="true" AutoCompleteType="Disabled" OnTextChanged="BtnSearchMR_Click"></asp:TextBox>
                                <label>- </label>
                                <asp:TextBox ID="DateTextboxEnd" runat="server" Style="width: 120px; height: 26px;" placeholder="dd mmm yyyy" onmousedown="dateEnd();" AutoPostBack="true" AutoCompleteType="Disabled" OnTextChanged="BtnSearchMR_Click"></asp:TextBox>
                                <asp:DropDownList ID="DDLDokterName" runat="server" Style="margin-left: 15px; max-width: 200px; height: 26px;" OnSelectedIndexChanged="BtnSearchMR_Click">
                                    <asp:ListItem Text="Dokter" Value="0" />
                                </asp:DropDownList>

                                <br />

                                <div class="alertNotif" runat="server" id="divalert" visible="false">
                                    <label>Hasil pencarian melebihi 20 encounter yang bisa ditampilkan, silakan ganti <i>range</i> tanggal untuk menampilkan hasil lainnya</label>
                                </div>
                            </div>
                        </div>

                        <div class="row" style="background-color: white">
                            <div class="col-sm-12" style="min-height: 50px; padding-top: 5px; padding-bottom: 5px;">

                                <div id="img_noData_tab1" runat="server" visible="false" style="text-align: center; padding-top: 3%;">
                                    <div>
                                        <img src="<%= Page.ResolveClientUrl("~/Images/Background/ic_noData_Suster.svg") %>" style="height: auto; width: 200px; margin-right: 3px; margin-top: 40px" />
                                    </div>

                                    <div runat="server" id="no_patient_data_tab1">
                                        <span>
                                            <h3 style="font-weight: 700; color: #585A6F">Oops! There is no data</h3>
                                        </span>
                                        <span style="font-size: 14px; color: #585A6F; font-family: Arial, Helvetica, sans-serif">Please check MR number or search another MR number</span>
                                    </div>
                                </div>

                                <asp:GridView ID="GridViewPengkajian" runat="server" AutoGenerateColumns="False" CssClass="table table-condensed table-striped table-hover" BorderColor="#cdd2dd"
                                    HeaderStyle-CssClass="text-left" ShowHeaderWhenEmpty="True" DataKeyNames="PatientId" EmptyDataText="No Data">

                                    <Columns>
                                        <asp:TemplateField ItemStyle-Width="5%" HeaderText="Cetak" HeaderStyle-CssClass="text-center" ItemStyle-HorizontalAlign="Center" HeaderStyle-BorderColor="#cdd2dd">
                                            <ItemTemplate>
                                                <a href='<%# String.Format("javascript:PreviewPengkajianAwal({0},\"{1}\",{2},\"{3}\")", Eval("AdmissionId"),Eval("encounterId").ToString(),Eval("patientId"),Eval("pageFA").ToString()) %>' style="cursor: pointer;" title="Pengkajian Awal">
                                                    <i class="fa fa-print icon-print"></i>
                                                </a>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField ItemStyle-Width="10%" HeaderText="Tanggal Admisi" DataField="AdmissionDate" ItemStyle-HorizontalAlign="left" HeaderStyle-BorderColor="#cdd2dd"></asp:BoundField>
                                        <asp:BoundField ItemStyle-Width="10%" HeaderText="No. Admisi" DataField="AdmissionNo" ItemStyle-HorizontalAlign="left" HeaderStyle-BorderColor="#cdd2dd"></asp:BoundField>
                                        <asp:BoundField ItemStyle-Width="35%" HeaderText="Dokter" DataField="DoctorName" ItemStyle-HorizontalAlign="left" HeaderStyle-BorderColor="#cdd2dd"></asp:BoundField>
                                        <asp:BoundField ItemStyle-Width="40%" HeaderText="Penjamin" DataField="PayerName" ItemStyle-HorizontalAlign="left" HeaderStyle-BorderColor="#cdd2dd"></asp:BoundField>

                                    </Columns>
                                </asp:GridView>
                                <asp:HiddenField ID="HiddenFieldIPAddress" runat="server" />
                                <asp:HiddenField ID="HiddenFieldOrgID" runat="server" />
                                <asp:HiddenField ID="HiddenFieldUserID" runat="server" />
                            </div>

                        </div>
                    </div>
                </div>

            </ContentTemplate>
        </asp:UpdatePanel>

        <asp:UpdatePanel ID="UpdatePaneltab2" runat="server">
            <ContentTemplate>

                <!-- ##### TAB 2 ##### -->
                <div id="contenttab2" style="background-color: white; min-height: calc(100vh - 95px); display: none;">
                    <div id="headercontent2" class="container-fluid">
                        <div class="row" style="background-color: white; padding-left: 15px; border-bottom: 1px solid lightgrey;">
                            <div class="col-sm-3" style="display: inline; min-height: 50px; border-right: 1px dashed lightgrey; padding-top: 11px;">
                                <label>Mr No. </label>
                                <asp:TextBox ID="TBoxSearchMR2" runat="server" placeholder="Cari disini..." Style="height: 26px; width: 55%;" onkeydown="klikEnterCari2(event);"></asp:TextBox>
                                <asp:Button ID="BtnSearchMR2" runat="server" Text="Search" CssClass="btn btn-lightGreen btn-sm" Style="height: 26px; width: 20%; padding-top: 3px;" OnClick="BtnSearchMR2_Click" />
                            </div>
                            <div class="col-sm-9" style="display: inline; min-height: 50px; border-left: 1px dashed lightgrey; padding-top: 5px; margin-left: -1px;">
                                <table style="width: 100%;">
                                    <tr>
                                        <td>
                                            <div id="datapasienheader2" runat="server">
                                                <asp:Label ID="LblNamaPasien2" runat="server" Text="Label" Style="font-weight: bold; font-size: 15px;"></asp:Label>
                                                <br />
                                                <label>Tgl. Lahir : </label>
                                                <asp:Label ID="LblTglLahir2" runat="server" Text="Label" CssClass="lblpasien"></asp:Label>
                                                <label>Umur : </label>
                                                <asp:Label ID="LblUmur2" runat="server" Text="Label" CssClass="lblpasien"></asp:Label>
                                                <label>Agama : </label>
                                                <asp:Label ID="LblAgama2" runat="server" Text="Label" CssClass="lblpasien"></asp:Label>
                                                <label>Gender : </label>
                                                <asp:Image ID="ImageICMale2" runat="server" ImageUrl="~/Images/Worklist/ic_Male.png" Style="height: 16px; padding-bottom: 2px;" Visible="false" />
                                                <asp:Image ID="ImageICFemale2" runat="server" ImageUrl="~/Images/Worklist/ic_Female.png" Style="height: 15px; padding-bottom: 2px;" Visible="false" />
                                            </div>
                                        </td>
                                        <td style="text-align: right; padding-top: 10px;">
                                            <asp:UpdateProgress ID="upprogprintFA2" runat="server" AssociatedUpdatePanelID="UpdatePaneltab2">
                                                <ProgressTemplate>
                                                    <div class="modal-backdrop" style="background-color: white; opacity: 0; text-align: center">
                                                    </div>
                                                    <img alt="" style="background-color: transparent; height: 20px; margin-left: 10px;" src="<%= Page.ResolveClientUrl("~/Images/Background/small-loader.gif") %>" />
                                                </ProgressTemplate>
                                            </asp:UpdateProgress>
                                        </td>
                                    </tr>
                                </table>


                            </div>
                        </div>
                    </div>

                    <div id="img_noData_tab2" runat="server" visible="false" style="text-align: center; padding-top: 3%;">
                        <div>
                            <img src="<%= Page.ResolveClientUrl("~/Images/Background/ic_noData_Suster.svg") %>" style="height: auto; width: 200px; margin-right: 3px; margin-top: 40px" />
                        </div>

                        <div runat="server" id="no_patient_data_tab2">
                            <span>
                                <h3 style="font-weight: 700; color: #585A6F">Oops! There is no data</h3>
                            </span>
                            <span style="font-size: 14px; color: #585A6F; font-family: Arial, Helvetica, sans-serif">Please check MR number or search another MR number</span>
                        </div>
                    </div>

                    <div id="datacontent2" runat="server">
                        <iframe name="resumeMedis" id="resumeMedis" runat="server" style="width: 100%; height: calc(100vh - 151px); border: none;"></iframe>
                    </div>
                </div>

            </ContentTemplate>
        </asp:UpdatePanel>

        <!-- ##### End Content ##### -->

        <!-- ##### Modal ##### -->
        <div class="modal fade" id="modal" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <div class="modal-dialog" style="width: 800px;">
                        <div class="modal-content" style="border-radius: 7px 7px;">
                            <div class="modal-header" style="padding-top: 10px; padding-bottom: 5px; border-bottom: 1px solid lightgray;">
                                <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                                <h4 style="margin-bottom: 2px;">
                                    <asp:Label ID="lblModalTitle" Style="font-weight: bold" runat="server" Text="Add/Edit Doctor List"></asp:Label>
                                </h4>
                            </div>
                            <div class="modal-body" style="text-align: center;">
                            </div>
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
        <!-- ##### End Modal ##### -->

        <!-- ##### Modal Update Error ##### -->
        <div class="modal in fade" id="modalError" role="dialog" tabindex="-1" aria-labelledby="myModalLabel" aria-hidden="true" style="padding-top: 120px;">

            <div class="modal-dialog" style="width: 500px">
                <div class="modal-content">

                    <!-- Modal Header -->
                    <div class="modal-header" style="background-color: #d02626; color: white; padding: 7px;">
                        <button type="button" class="close" data-dismiss="modal">×</button>
                        <h4 class="modal-title">
                            <i class="fa fa-warning"></i>
                            <label>Error</label>
                        </h4>
                    </div>

                    <!-- Modal body -->
                    <div class="modal-body" style="background-color: white;">

                        <asp:UpdatePanel ID="UpdatePanelError" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>

                                <table style="width: 100%;">
                                    <tr>
                                        <td style="width: 110px; vertical-align: top; font-weight: bold;">Error Time </td>
                                        <td style="width: 20px; vertical-align: top; font-weight: bold;">: </td>
                                        <td>
                                            <asp:Label ID="LabelErrorTime" runat="server" Text="Label"></asp:Label>
                                            , <b>User : </b>
                                            <asp:Label ID="LabelErrorUser" runat="server" Text="Label"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 110px; vertical-align: top; font-weight: bold;">Exception </td>
                                        <td style="width: 20px; vertical-align: top; font-weight: bold;">: </td>
                                        <td>
                                            <asp:Label ID="LabelErrorEx" runat="server" Text="Label"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 110px; vertical-align: top; font-weight: bold;">Exception Detail </td>
                                        <td style="width: 20px; vertical-align: top; font-weight: bold;">: </td>
                                        <td>
                                            <asp:Label ID="LabelErrorExDet" runat="server" Text="Label"></asp:Label>
                                        </td>
                                    </tr>
                                </table>

                                <div data-toggle="collapse" data-target="#detailerror" aria-expanded="false" aria-controls="collapseExample" style="text-align: right;">
                                    <label style="cursor: pointer;"><< Show Detail</label>
                                </div>

                                <div class="collapse" id="detailerror">
                                    <table style="width: 100%;">
                                        <tr>
                                            <td style="width: 110px; vertical-align: top; font-weight: bold;">Source File </td>
                                            <td style="width: 20px; vertical-align: top; font-weight: bold;">: </td>
                                            <td>
                                                <asp:Label ID="LabelErrorExSF" runat="server" Text="Label"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 110px; vertical-align: top; font-weight: bold;">Method </td>
                                            <td style="width: 20px; vertical-align: top; font-weight: bold;">: </td>
                                            <td>
                                                <asp:Label ID="LabelErrorExMethod" runat="server" Text="Label"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 110px; vertical-align: top; font-weight: bold;">Line </td>
                                            <td style="width: 20px; vertical-align: top; font-weight: bold;">: </td>
                                            <td>
                                                <asp:Label ID="LabelErrorExLine" runat="server" Text="Label"></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                </div>

                                <table style="width: 100%;">
                                    <tr>
                                        <td colspan="3">&nbsp; </td>
                                    </tr>
                                    <tr>
                                        <td colspan="3">
                                            <div style="font-size: 13px; font-weight: bold; text-align: justify;">
                                                Silakan dicoba kembali dengan proses Retry, Reload Page, atau Relogin Aplikasi,
                                                jika error masih terjadi dalam periode yang lama, silakan hubungi team IT untuk melaporkan error yang terjadi. Terima kasih. 
                                            </div>
                                        </td>
                                    </tr>

                                </table>

                                <br />
                                <br />
                                <div class="row">
                                    <div class="col-sm-6 text-left">
                                        <%--<asp:Button ID="ButtonRelogin" Style="width: 100px;" CssClass="btn btn-danger btn-sm" runat="server" Text="Re-Login" OnClick="ButtonRelogin_Click" />--%>
                                    </div>
                                    <div class="col-sm-6 text-right">
                                        <%--<asp:Button ID="ButtonReload" Style="width: 100px;" CssClass="btn btn-warning btn-sm" runat="server" Text="Re-Load" OnClick="ButtonReload_Click" />
                                        <asp:Button ID="ButtonRetry" Style="width: 100px;" CssClass="btn btn-warning btn-sm" runat="server" Text="Retry" data-dismiss="modal" />--%>
                                    </div>
                                </div>


                            </ContentTemplate>
                        </asp:UpdatePanel>

                    </div>
                </div>
            </div>
        </div>
        <!-- End of Modal Update Error -->

        <%-- ################# --%>
    </form>
</body>

</html>
