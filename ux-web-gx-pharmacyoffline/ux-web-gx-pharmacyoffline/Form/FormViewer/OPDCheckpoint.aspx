<%@ Page Language="C#" AutoEventWireup="true" CodeFile="OPDCheckpoint.aspx.cs" Inherits="Form_FormViewer_OPDCheckpoint" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <!-- Tell the browser to be responsive to screen width -->
    <meta content="width=device-width, initial-scale=1, maximum-scale=1, user-scalable=no" name="viewport" />
    <!-- Bootstrap 3.3.6 -->
    <link rel="stylesheet" href="~/Content/bootstrap/css/bootstrap.min.css" />
    <!-- Font Awesome -->
    <link rel="stylesheet" href="~/Content/font-awesome/css/font-awesome.min.css" />
    <!-- Datepicker -->
    <link rel="stylesheet" href="~/Content/plugins/datepicker/datepicker130.css" />
    <!-- Mask -->
    <link rel="stylesheet" href="~/Content/plugins/jasny-bootstrap/css/jasny-bootstrap.min.css" />
    <!-- Theme style -->
    <link rel="stylesheet" href="~/Content/dist/css/skins/skin-blue-light.css" />
    <link rel="stylesheet" href="~/Content/Site.css" />
    <%--<link rel="stylesheet" href="~/Content/dist/css/AdminLTE.css" />--%>
    <%--<link rel="stylesheet" href="~/Content/EMR-PharmacyOffline.css" />--%>

    <link href="~/favicon.ico" rel="shortcut icon" type="image/x-icon" />

    <style type="text/css">
        .btn-tab {
            background-color: #7178B2; /*#1A2268;*/
            color: white;
            border-radius: 7px 7px 0px 0px;
            min-width: 200px;
            outline: none !important;
            position:relative;
            z-index:1;
        }

            .btn-tab:hover {
                background-color: #F2F3F4;
                color: #1a2268;
                /*border-color: #1a2268;
                border-width: 1px 1px 0px 1px;*/
                margin-bottom: -2px;
                height: 32px;
                font-weight: bold;
                min-width: 200px;
                border-radius: 7px 7px 0px 0px;
                outline: none !important;
                position:relative;
                z-index:1;
            }

        .btn-tab-active {
            background-color: #F2F3F4;
            color: #1a2268;
            /*border-color: #1a2268;
            border-width: 1px 1px 0px 1px;*/
            margin-bottom: -2px;
            height: 32px;
            font-weight: bold;
            min-width: 200px;
            border-radius: 7px 7px 0px 0px;
            outline: none !important;
            position:relative;
            z-index:2;
        }

        h4 {
            color: #1A2268;
        }

        .txt-search {
            max-width: 200px;
            display: inline-block;
            margin-right: 5px;
        }

        .div-search-result {
            background-color: #fbfbfc; /*#eaecfa;*/
            border-radius: 7px;
            padding: 15px;
            margin-bottom:15px;
        }

        .div-search-result-done {
            background-color: #fbfbfc; /*#eafaec;*/
            border-radius: 7px;
            padding: 15px;
            margin-bottom:15px;
        }

        .box-table-shadow {
            background-color: white;
            /* border: 1px solid #ddd; */
            padding: 5px;
            box-shadow: 1px 1px 5px -3px;
            border-radius: 6px;
        }

        .div-sub-header {
            display: inline-block !important;
            font-size: 11px;
            padding-right:25px;
        }

        .table-nursecord {
            width: 100%;
            border:0;
        }

            .table-nursecord th {
                border: 0;
                font-size:13px;
                background-color:#F2F3F4;
                color:#0013B5;
                font-weight:bold;
            }

            .table-nursecord td {
                border: 0;
                font-size:11px;
                padding-bottom:3px !important;
            }

        .table-vertical-divider {
        border-left:1px solid #F2F3F4 !important;
        }

        .table-header-racikan {
            width: 100%;
        }

            .table-header-racikan th {
                border: 0;
                font-size:13px;
            }

            .table-header-racikan td {
                border: 0;
                font-size:11px;
            }

        .table-detail-racikan {
                width: 100%;
                border: 0;
            }

            .table-detail-racikan th {
                border: 0;
                font-size:13px;
            }

            .table-detail-racikan td {
                border: 0;
                font-size:11px;
            }

        .no-padding {
        padding:0px !important;
        }

        .textbox-red {
            outline-color: red;
            border-color: red;
        }

            .textbox-red:focus {
                outline-color: red;
                border-color: red;
                box-shadow: 0 0 8px #ff000066;
            }

        .chk-box {
            display: inline-block;
            background-color: #ffffff;
            padding: 1px 7px 1px 7px;
            border-radius: 20px;
            border: 1px solid #b1b1b1;
        }

            .chk-box label {
                vertical-align: text-top;
                /*margin-left: 5px;*/
                font-size: 12px;
            }

            .chk-box input[type=radio] {
                display:none;
            }

            .chk-box input[type=checkbox] {
                display:none;
            }

        .chk-box-checked {
            display: inline-block;
            background-color: #7178B2; /*#0075ff;*/
            padding: 1px 7px 1px 7px;
            border-radius: 20px;
            color: white;
            border: 1px solid #ffffff;
        }

            .chk-box-checked label {
                vertical-align: text-top;
                /*margin-left: 5px;*/
                font-size: 12px;
            }

            .chk-box-checked input[type=radio] {
                display:none;
            }

            .chk-box-checked input[type=checkbox] {
                display:none;
            }

        /* LOADING */
        @keyframes LoadingBar {
            0% {
                transform: translateX(-102%)
            }

            45% {
                transform: translateX(0)
            }

            55% {
                transform: translateX(0)
            }

            90% {
                transform: translateX(102%)
            }

            100% {
                transform: translateX(102%)
            }
        }

        .loading-bar {
            display: block;
            position: absolute;
            top: 0;
            left: 0;
            width: 100%;
            height: 3px;
            overflow-x: hidden;
            background-color: powderblue; /* use px color $primary-light */
        }

        .loading-bar:before {
            content: "";
            position: absolute;
            display: block;
            top: 0;
            left: 0;
            right: 0;
            bottom: 0;
            width: 102%;
            background-color: #1a2268; /* use px color $primary-default */
            animation: LoadingBar 1.5s cubic-bezier(.5,.01,.51,1) infinite;
        }

        /** Input styling **/
        .text-input {
            position: relative;
            display: inline;
            /*height: 56px;
            width: 100%;
            margin-bottom: 16px;*/
        }
        .text-input > input {
            /*position: absolute;
            bottom: 0;
            display: block;
            height: 40px;
            width: 100%;
            line-height: 40px;
            border: 1;
            border-bottom: 1px solid #9e9e9e;
            outline: 1;
            padding: 0 8px;*/
            /*   transition: all 1s ease-in-out; */
        }
        .text-input > input::placeholder {
            color: transparent;
        }
        .text-input > label {
            position: absolute;
            bottom: -9px;
            left: 9px;
            color: #9e9e9e;
            cursor: text;
            transition: all 0.2s ease-in-out;
            user-select: none;
        }
        .text-input > input:focus,
        .text-input > input:not(:placeholder-shown) {
            /*border-bottom: 2px solid #6c17e1;*/
        }
        .text-input > input:focus + label,
        .text-input > input:not(:placeholder-shown) + label {
            bottom: 20px;
            left: 3px;
            font-size: 14px;
            /*color:#6c17e1;*/
        }

        .disable-obj {
            pointer-events: none;
            opacity: 0.6;
            display:inline-block;
        }
    </style>
</head>

<body style="padding-top: 0px; padding-bottom: 0px;">
    <form id="form1" runat="server">

        <asp:ScriptManager ID="ScriptManager1" runat="server">
            <Scripts>
                <asp:ScriptReference Path="~/Content/plugins/jQuery/jQuery-2.2.0.min.js" />
                <asp:ScriptReference Path="~/Content/bootstrap/js/bootstrap.min.js" />
                <asp:ScriptReference Path="~/Content/plugins/jasny-bootstrap/js/jasny-bootstrap.min.js" />
                <asp:ScriptReference Path="~/Content/plugins/datepicker/bootstrap-datepicker.js" />
                <asp:ScriptReference Path="~/Content/dist/js/app.min.js" />
            </Scripts>
        </asp:ScriptManager>

        <%--YOUR CODE HTML HERE--%>

        <div class="container-fluid">
            <asp:UpdatePanel ID="UpdatePanelOPDCheckpoint" runat="server">
                <ContentTemplate>

            <div class="row" style="background-color: #F2F3F4; min-height: 100vh;">
                <div class="col-sm-12" style="background-color:#CDD2DD;">
                    <h4><b>Pencarian Pasien</b></h4>
                    <asp:HiddenField ID="HF_OrgID" runat="server" />
                    <asp:HiddenField ID="HF_UserID" runat="server" />
                    <asp:HiddenField ID="HF_UserName" runat="server" />
                    <asp:HiddenField ID="HF_HospitalID" runat="server" />
                    <asp:HiddenField ID="HF_FlagSearchType" runat="server" />
                </div>
                
                <div class="col-sm-12" style="background-color:#CDD2DD;"> <%--border-bottom: 1px solid #1a2453;--%>
                    <asp:Button ID="BtnTabMR" runat="server" CssClass="btn btn-sm btn-tab" Text="Dengan Nomor MR" OnClick="BtnTabMR_Click" />
                    <asp:Button ID="BtnTabPasien" runat="server" CssClass="btn btn-sm btn-tab" Text="Dengan Data Pasien" OnClick="BtnTabPasien_Click" />
                    <asp:Button ID="BtnTabBarcode" runat="server" CssClass="btn btn-sm btn-tab" Text="Dengan Nomor Antrian" OnClick="BtnTabBarcode_Click" />
                </div>
                    <asp:UpdateProgress ID="UpdateProgressPtnList" runat="server" AssociatedUpdatePanelID="UpdatePanelOPDCheckpoint">
                        <ProgressTemplate>
                            <!-- loading v1 -->
                            <div class="modal-backdrop" style="background-color: white; opacity: 0; text-align: center">
                            </div>
                            <div class="loading-bar" style="top:66px; z-index:1;"></div>
                        </ProgressTemplate>
                    </asp:UpdateProgress> 
                    <asp:UpdateProgress ID="UpdateProgressButtonAction" runat="server" AssociatedUpdatePanelID="UpdatePanelProsesBayar">
                        <ProgressTemplate>
                            <!-- loading v1 -->
                            <div class="modal-backdrop" style="background-color: white; opacity: 0; text-align: center">
                            </div>
                            <div class="loading-bar" style="top:66px; z-index:1;"></div>
                        </ProgressTemplate>
                    </asp:UpdateProgress>

                <div class="col-sm-12">
                    <div id="divmrsearch" runat="server" visible="false" style="padding-top: 35px; padding-bottom: 5px;">
                        <div class="text-input">
                            <asp:TextBox ID="TxtSearchMR" runat="server" CssClass="form-control txt-search" placeholder="MR No" onkeydown="return onEnterByMR();"></asp:TextBox>
                            <label for="TxtSearchMR">MR No</label>
                        </div>        
                        <div class="text-input">
                            <asp:TextBox ID="TxtSearchTglAdmMR" runat="server" CssClass="form-control txt-search" placeholder="Admission Date" onkeydown="return preventEnter();" onmousedown="dateAdm_MR();" AutoCompleteType="Disabled"></asp:TextBox>
                            <label for="TxtSearchTglAdmMR">Admission Date</label>
                        </div>     
                        <asp:Button ID="BtnCariPasienMR" runat="server" CssClass="btn btn-primary" Text="Cari Pasien" OnClientClick="return searchinputvalidationMR();" OnClick="BtnCariPasienMR_Click" />     
                    </div>
                    <div id="divpasiensearch" runat="server" visible="false" style="padding-top: 35px; padding-bottom: 5px;">
                        <div class="text-input">
                            <asp:TextBox ID="TxtSearchPasien" runat="server" CssClass="form-control txt-search" placeholder="Patient Name" onkeydown="return onEnterByPasien();"></asp:TextBox>
                            <label for="TxtSearchPasien">Patient Name</label>
                        </div>
                        <div class="text-input">
                            <asp:TextBox ID="TxtSearchTglLahirPasien" runat="server" CssClass="form-control txt-search" placeholder="Patient DOB" onkeydown="return preventEnter();" onmousedown="dateTglLahir_Pasien();" AutoCompleteType="None"></asp:TextBox>
                            <label for="TxtSearchTglLahirPasien">Patient DOB</label>
                        </div>
                        <div class="text-input">
                            <asp:TextBox ID="TxtSearchTglAdmPasien" runat="server" CssClass="form-control txt-search" placeholder="Admission Date" onkeydown="return preventEnter();" onmousedown="dateAdm_Pasien();" AutoCompleteType="Disabled"></asp:TextBox>
                            <label for="TxtSearchTglAdmPasien">Admission Date</label>
                        </div>
                        <asp:Button ID="BtnCariPasienPasien" runat="server" CssClass="btn btn-primary" Text="Cari Pasien" OnClientClick="return searchinputvalidationPasien();" OnClick="BtnCariPasienPasien_Click" />
                    </div>
                    <div id="divbarcodesearch" runat="server" visible="false" style="padding-top: 35px; padding-bottom: 5px;">
                        <div class="text-input">
                            <asp:TextBox ID="TxtSearchBarcode" runat="server" CssClass="form-control txt-search" placeholder="Queue No" onkeydown="return onEnterByBarcode();"></asp:TextBox>
                            <label for="TxtSearchBarcode">Queue No</label>
                        </div>
                        <div class="text-input">
                            <asp:TextBox ID="TxtSearchTglAdmBarcode" runat="server" CssClass="form-control txt-search" placeholder="Admission Date" onkeydown="return preventEnter();" onmousedown="dateAdm_Barcode();" AutoCompleteType="Disabled"></asp:TextBox>
                            <label for="TxtSearchTglAdmBarcode">Admission Date</label>
                        </div>
                        <asp:Button ID="BtnCariPasienBarcode" runat="server" CssClass="btn btn-primary" Text="Cari Pasien" OnClientClick="return searchinputvalidationBarcode();" OnClick="BtnCariPasienBarcode_Click"/>
                    </div>
                </div>
                <div class="col-sm-12" style="margin-top: 25px;">
                    <div id="divresult" runat="server" visible="true" class="div-search-result">
                        <h4 style="color:#76767C;">Hasil Pencarian Data Pasien</h4>

                        <div class="row" style="margin-bottom: 25px;">
                            <div class="col-sm-12" style="margin-bottom:15px;">
                                <div style="font-weight: bold; margin-bottom: 5px; display: none;">Patient</div>
                                <div style="border-bottom:1px dashed #cdd2dd; padding-bottom:15px; padding-top:10px;">

                                    <asp:Image ID="ImageMale" runat="server" ImageUrl="~/Images/Worklist/ic_Male.svg" style="height: 20px; margin-top: -5px;" Visible="false" />
                                    <asp:Image ID="ImageFemale" runat="server" ImageUrl="~/Images/Worklist/ic_Female.svg" style="height: 20px; margin-top: -5px;" Visible="false" />
                                    <asp:Label ID="lbl_patientname" runat="server" Text="-" style="font-weight:bold; font-size:17px;"></asp:Label>
                                    
                                    <br />
                                    <span style="font-size:11px;">DOB / Age : 
                                    <asp:Label ID="lbl_dob" runat="server" Text="-"></asp:Label>
                                    </span>

                                    <div style="padding-top:7px; margin-top:7px;">
                                        <div class="div-sub-header">
                                            <span style="color:#76767C;"> NO. MR </span>
                                            <br />
                                            <asp:Label ID="lbl_mrno" runat="server" Text="-" style="font-weight:bold;"></asp:Label>
                                        </div>
                                        <div class="div-sub-header">
                                            <span style="color:#76767C;"> NO. ADMISSION </span>
                                            <br />
                                            <asp:Label ID="lbl_admno" runat="server" Text="-" style="font-weight:bold;"></asp:Label>
                                        </div>
                                        <div class="div-sub-header">
                                            <span style="color:#76767C;"> DOCTOR </span>
                                            <br />
                                            <asp:Label ID="lbl_doctorname" runat="server" Text="-"></asp:Label>
                                        </div>
                                        <div class="div-sub-header">
                                            <span style="color:#76767C;"> PAYER </span>
                                            <br />
                                            <asp:Label ID="lbl_payer" runat="server" Text="-"></asp:Label>
                                       </div>
                                    </div>
                                    <div style="padding-top:7px; margin-top:7px;" runat="server" id="div_presnotes">
                                        <div class="div-sub-header">
                                            <span style="color:#76767C;"> PRESCRIPTION NOTES </span>
                                            <br />
                                            <asp:Label ID="lbl_presnotes" runat="server" Text="-"></asp:Label>
                                       </div>
                                    </div>
                                </div>
                            </div>
                            <div class="col-sm-12">
                                <div class="row">
                                    <div class="col-sm-4" id="Div_Lab" runat="server">
                                        <div class="box-table-shadow" style="padding:0px; padding-bottom: 5px;">
                                        <div class="row" style="margin-bottom: 6px; padding-left: 6px; padding-top: 6px;">
                                            <div class="col-sm-6" style="font-weight:bold;">
                                                <asp:Image ID="ImageLab" runat="server" ImageUrl="~/Images/Worklist/ic_Lab.svg" style="height: 15px; margin-top: -4px;" />
                                                Laboratorium
                                            </div>
                                            <div class="col-sm-6" style="text-align:right;">
                                                <div id="divchklab1" class="chk-box chk-lab" style="display:none;">
                                                    <asp:CheckBox ID="CheckBoxLab1" runat="server" Text="Order" onclick="klikChk('CheckBoxLab1','divchklab1','chk-lab');" />
                                                </div>
                                            </div>
                                        </div>
                                        <asp:GridView ID="GvwLabOrder" runat="server" AutoGenerateColumns="False" CssClass="table-condensed table-nursecord" BackColor="white"
                                        EmptyDataText="No Data" Font-Names="Helvetica">           
                                            <Columns>
                                                <asp:TemplateField HeaderText="Ambil" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" HeaderStyle-Font-Size="13px" HeaderStyle-Font-Names="Helvetica, Arial, sans-serif" HeaderStyle-Font-Bold="true" HeaderStyle-Width="50px">
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="ChkTakeLab" runat="server" Checked="true" Enabled="false" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>  
                                                <asp:TemplateField HeaderText="Item" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Font-Size="13px" HeaderStyle-Font-Names="Helvetica, Arial, sans-serif" HeaderStyle-Font-Bold="true" ItemStyle-CssClass="table-vertical-divider" HeaderStyle-CssClass="table-vertical-divider">
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" ID="lblitemname" Width="100%" Font-Size="11px" Font-Names="Helvetica, Arial, sans-serif" Text='<%# Bind("itemName") %>'></asp:Label>         
                                                    </ItemTemplate>
                                                </asp:TemplateField>     
                                            </Columns>
                                        </asp:GridView>
                                        </div>
                                    </div>
                                    <div class="col-sm-4" id="Div_Rad" runat="server">
                                        <div class="box-table-shadow" style="padding:0px; padding-bottom: 5px;">
                                        <div class="row" style="margin-bottom: 6px; padding-left: 6px; padding-top: 6px;">
                                            <div class="col-sm-6" style="font-weight:bold;">
                                                <asp:Image ID="ImageRad" runat="server" ImageUrl="~/Images/Worklist/ic_Rad.svg" style="height: 15px; margin-top: -4px;" />
                                                Radiologi
                                            </div>
                                            <div class="col-sm-6" style="text-align:right;">
                                                <div id="divchkrad1" class="chk-box chk-rad" style="display:none;">
                                                    <asp:CheckBox ID="CheckBoxRad1" runat="server" Text="Order" onclick="klikChk('CheckBoxRad1','divchkrad1','chk-rad');" />
                                                </div>
                                            </div>
                                        </div>
                                        <asp:GridView ID="GvwRadOrder" runat="server" AutoGenerateColumns="False" CssClass="table-condensed table-nursecord" BackColor="white"
                                        EmptyDataText="No Data" Font-Names="Helvetica">           
                                            <Columns>
                                                <asp:TemplateField HeaderText="Ambil" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" HeaderStyle-Font-Size="13px" HeaderStyle-Font-Names="Helvetica, Arial, sans-serif" HeaderStyle-Font-Bold="true" HeaderStyle-Width="50px">
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="ChkTakeRad" runat="server" Checked="true" Enabled="false" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Item" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Font-Size="13px" HeaderStyle-Font-Names="Helvetica, Arial, sans-serif" HeaderStyle-Font-Bold="true" ItemStyle-CssClass="table-vertical-divider" HeaderStyle-CssClass="table-vertical-divider">
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" ID="lblitemname" Width="100%" Font-Size="11px" Font-Names="Helvetica, Arial, sans-serif" Text='<%# Bind("itemName") %>'></asp:Label>         
                                                    </ItemTemplate>
                                                </asp:TemplateField>   
                                            </Columns>
                                        </asp:GridView>
                                        </div>
                                    </div>

                                    <div class="col-sm-4" id="Div_Procedure" runat="server">
                                        <div class="box-table-shadow" style="padding:0px; padding-bottom: 5px;">
                                        <div class="row" style="margin-bottom: 6px; padding-left: 6px; padding-top: 6px;">
                                            <div class="col-sm-12" style="font-weight:bold;">
                                                <asp:Image ID="ImageDiag" runat="server" ImageUrl="~/Images/Worklist/ic_Diag.svg" style="height: 15px; margin-top: -4px;" />
                                                Diagnostik
                                            </div>
                                        </div>
                                        <asp:GridView ID="GvwProcedureDiagnosis" runat="server" AutoGenerateColumns="False" CssClass="table-condensed table-nursecord" BackColor="white"
                                        EmptyDataText="No Data" Font-Names="Helvetica">           
                                            <Columns>
                                                <asp:TemplateField HeaderText="Ambil" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" HeaderStyle-Font-Size="13px" HeaderStyle-Font-Names="Helvetica, Arial, sans-serif" HeaderStyle-Font-Bold="true" HeaderStyle-Width="50px">
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="ChkTakeProc" runat="server" Checked="true" Enabled="false" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Item" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Font-Size="13px" HeaderStyle-Font-Names="Helvetica, Arial, sans-serif" HeaderStyle-Font-Bold="true" ItemStyle-CssClass="table-vertical-divider" HeaderStyle-CssClass="table-vertical-divider">
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" ID="lblprocitemname" Width="100%" Font-Size="11px" Font-Names="Helvetica, Arial, sans-serif" Text='<%# Bind("ProcedureItemName") %>'></asp:Label>         
                                                    </ItemTemplate>
                                                </asp:TemplateField>   
                                            </Columns>
                                        </asp:GridView>
                                        </div>
                                    </div>

                                </div>
                            </div>
                        </div>

                        <div id="Div_PatientTable" style="display:none;">

                        <div style="font-weight: bold; margin-bottom: 5px;">Patient</div>
                        <asp:HiddenField ID="HF_PtntID" runat="server" />
                        <asp:HiddenField ID="HF_AdmID" runat="server" />
                        <asp:HiddenField ID="HF_EncID" runat="server" />
                        <asp:HiddenField ID="HF_DokterName" runat="server" />

                        <asp:GridView ID="GvwPatientData" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-condensed thinscroll" BackColor="White"
                            EmptyDataText="No Data" Font-Names="Helvetica">
                            <Columns>
                                <asp:TemplateField HeaderText="Patient Name" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Font-Size="13px" HeaderStyle-Font-Names="Helvetica, Arial, sans-serif" HeaderStyle-Font-Bold="true">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblptnname" Width="100%" Font-Size="11px" Font-Names="Helvetica, Arial, sans-serif" Text='<%# Bind("patientName") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Age" HeaderStyle-Font-Size="13px" HeaderStyle-Font-Names="Helvetica, Arial, sans-serif" HeaderStyle-Font-Bold="true">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblage" Width="100%" Font-Size="11px" Font-Names="Helvetica, Arial, sans-serif" Text='<%# Bind("age") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Gender" HeaderStyle-Font-Size="13px" HeaderStyle-Font-Names="Helvetica, Arial, sans-serif" HeaderStyle-Font-Bold="true">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblgender" Width="100%" Font-Size="11px" Font-Names="Helvetica, Arial, sans-serif" Text='<%# Eval("sexId").ToString() == "1" ? "Male" : "Female" %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="MR No" HeaderStyle-Font-Size="13px" HeaderStyle-Font-Names="Helvetica, Arial, sans-serif" HeaderStyle-Font-Bold="true">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblmrno" Width="100%" Font-Size="11px" Font-Names="Helvetica, Arial, sans-serif" Text='<%# Bind("mrNumber") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Doctor Name" HeaderStyle-Font-Size="13px" HeaderStyle-Font-Names="Helvetica, Arial, sans-serif" HeaderStyle-Font-Bold="true">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lbldoctorname" Width="100%" Font-Size="11px" Font-Names="Helvetica, Arial, sans-serif" Text='<%# Bind("doctorName") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Payer" HeaderStyle-Font-Size="13px" HeaderStyle-Font-Names="Helvetica, Arial, sans-serif" HeaderStyle-Font-Bold="true">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblpayer" Width="100%" Font-Size="11px" Font-Names="Helvetica, Arial, sans-serif" Text='<%# Bind("payerName") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                        </div>

                        <div id="Div_Drugs" runat="server" class="box-table-shadow" style="padding:0px; padding-bottom: 5px; margin-bottom: 20px;">
                        <div class="row" style="margin-bottom: 6px; padding-left: 6px; padding-top: 6px;">
                            <div class="col-sm-6" style="font-weight:bold;">
                                <asp:Image ID="ImageDrugs" runat="server" ImageUrl="~/Images/Worklist/ic_Meds.svg" style="height: 15px; margin-top: -4px;" />
                                Drug
                            </div>
                            <div class="col-sm-6" style="text-align:right; padding-right: 25px;">
                                <div id="divheaderchkdrug1" runat="server" style="display:inline-block">
                                    <div id="divchkdrug1" class="chk-box chk-drug">
                                        <asp:RadioButton ID="RadioDrug1" runat="server" GroupName="DrugGroup" Text="Ambil Semua" onclick="klikChk('RadioDrug1','divchkdrug1','chk-drug');" />
                                    </div>
                                </div>
                                <div id="divheaderchkdrug2" runat="server" style="display:inline-block">
                                    <div id="divchkdrug2" class="chk-box chk-drug">
                                        <asp:RadioButton ID="RadioDrug2" runat="server" GroupName="DrugGroup" Text="Ada Perubahan" onclick="klikChk('RadioDrug2','divchkdrug2','chk-drug');" />
                                    </div>
                                </div>
                            </div>
                        </div>
                        <asp:GridView ID="GvwPrescriptionOrder" runat="server" AutoGenerateColumns="False" CssClass="table-condensed table-nursecord" BackColor="White"
                            EmptyDataText="No Data" Font-Names="Helvetica">
                            <Columns>
                                <asp:TemplateField HeaderText="Item" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Font-Size="13px" HeaderStyle-Font-Names="Helvetica, Arial, sans-serif" HeaderStyle-Font-Bold="true">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblitemname" Width="100%" Font-Size="11px" Font-Names="Helvetica, Arial, sans-serif" Text='<%# Bind("salesItemName") %>'></asp:Label>
                                        <asp:HiddenField ID="HF_itemid" runat="server" Value='<%# Bind("SalesItemId") %>' />
                                        <asp:HiddenField ID="HF_qty" runat="server" Value='<%# Bind("Quantity") %>' />
                                        <asp:HiddenField ID="HF_uomid" runat="server" Value='<%# Bind("UomId") %>' />
                                        <asp:HiddenField ID="HF_iscons" runat="server" Value='<%# Bind("IsConsumables") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Dose" HeaderStyle-Font-Size="13px" HeaderStyle-Font-Names="Helvetica, Arial, sans-serif" HeaderStyle-Font-Bold="true" ItemStyle-CssClass="table-vertical-divider">
                                    <ItemTemplate>
                                        <div runat="server" visible='<%# Eval("IsDoseText").ToString() == "False" %>'>
                                            <asp:Label runat="server" ID="lbldose" Font-Size="11px" Font-Names="Helvetica, Arial, sans-serif" Text='<%# Bind("dose") %>'></asp:Label>
                                            &nbsp;
                                            <asp:Label runat="server" ID="lbldoseuom" Font-Size="11px" Font-Names="Helvetica, Arial, sans-serif" Text='<%# Bind("doseUom") %>'></asp:Label>
                                        </div>
                                        <asp:Label runat="server" ID="lbldosetext" Font-Size="11px" Font-Names="Helvetica, Arial, sans-serif" Visible='<%# Eval("IsDoseText") %>' Text='<%# Bind("doseText") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Frequency" HeaderStyle-Font-Size="13px" HeaderStyle-Font-Names="Helvetica, Arial, sans-serif" HeaderStyle-Font-Bold="true" ItemStyle-CssClass="table-vertical-divider">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblfrequency" Width="100%" Font-Size="11px" Font-Names="Helvetica, Arial, sans-serif" Text='<%# Bind("frequency") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Route" HeaderStyle-Font-Size="13px" HeaderStyle-Font-Names="Helvetica, Arial, sans-serif" HeaderStyle-Font-Bold="true" ItemStyle-CssClass="table-vertical-divider">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblroute" Width="100%" Font-Size="11px" Font-Names="Helvetica, Arial, sans-serif" Text='<%# Bind("route") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Instruction" HeaderStyle-Font-Size="13px" HeaderStyle-Font-Names="Helvetica, Arial, sans-serif" HeaderStyle-Font-Bold="true" ItemStyle-CssClass="table-vertical-divider">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblinstruction" Width="100%" Font-Size="11px" Font-Names="Helvetica, Arial, sans-serif" Text='<%# Bind("instruction") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Qty" HeaderStyle-Font-Size="13px" HeaderStyle-Font-Names="Helvetica, Arial, sans-serif" HeaderStyle-Font-Bold="true" ItemStyle-CssClass="table-vertical-divider">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblqty" Width="100%" Font-Size="11px" Font-Names="Helvetica, Arial, sans-serif" Text='<%# Bind("quantity") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="UoM" HeaderStyle-Font-Size="13px" HeaderStyle-Font-Names="Helvetica, Arial, sans-serif" HeaderStyle-Font-Bold="true" ItemStyle-CssClass="table-vertical-divider">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lbluom" Width="100%" Font-Size="11px" Font-Names="Helvetica, Arial, sans-serif" Text='<%# Bind("uom") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Iter" HeaderStyle-Font-Size="13px" HeaderStyle-Font-Names="Helvetica, Arial, sans-serif" HeaderStyle-Font-Bold="true" ItemStyle-CssClass="table-vertical-divider">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lbliter" Width="100%" Font-Size="11px" Font-Names="Helvetica, Arial, sans-serif" Text='<%# Bind("iter") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Routine" HeaderStyle-Font-Size="13px" HeaderStyle-Font-Names="Helvetica, Arial, sans-serif" HeaderStyle-Font-Bold="true" ItemStyle-CssClass="table-vertical-divider">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblroutine" Width="100%" Font-Size="11px" Font-Names="Helvetica, Arial, sans-serif" Text='<%# Eval("isRoutine").ToString().ToLower() == "true" ? "Yes" : "No" %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Stock" HeaderStyle-Font-Size="13px" HeaderStyle-Font-Names="Helvetica, Arial, sans-serif" HeaderStyle-Font-Bold="true" ItemStyle-CssClass="table-vertical-divider">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblstock" Width="100%" Font-Size="11px" Font-Names="Helvetica, Arial, sans-serif" Text='<%# Eval("TotalQuantity") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                        </div>

                        <div id="Div_Cons" runat="server" class="box-table-shadow" style="padding:0px; padding-bottom: 5px; margin-bottom: 20px;">
                        <div style="font-weight:bold; margin-bottom: 6px; padding-left: 6px; padding-top: 6px;">
                            <asp:Image ID="ImageCons" runat="server" ImageUrl="~/Images/Worklist/ic_Meds.svg" style="height: 15px; margin-top: -4px;" />
                            Consumable
                        </div>
                        <asp:GridView ID="GvwConsumableOrder" runat="server" AutoGenerateColumns="False" CssClass="table-condensed table-nursecord" BackColor="white"
                        EmptyDataText="No Data" Font-Names="Helvetica">           
                            <Columns>
                                <asp:TemplateField HeaderText="Item" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Font-Size="13px" HeaderStyle-Font-Names="Helvetica, Arial, sans-serif" HeaderStyle-Font-Bold="true">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblitemname" Width="100%" Font-Size="11px" Font-Names="Helvetica, Arial, sans-serif" Text='<%# Bind("salesItemName") %>'></asp:Label> 
                                        <asp:HiddenField ID="HF_itemid" runat="server" Value='<%# Bind("SalesItemId") %>' />
                                        <asp:HiddenField ID="HF_qty" runat="server" Value='<%# Bind("Quantity") %>' />
                                        <asp:HiddenField ID="HF_uomid" runat="server" Value='<%# Bind("UomId") %>' />
                                        <asp:HiddenField ID="HF_iscons" runat="server" Value='<%# Bind("IsConsumables") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Qty" HeaderStyle-Font-Size="13px" HeaderStyle-Font-Names="Helvetica, Arial, sans-serif" HeaderStyle-Font-Bold="true" ItemStyle-CssClass="table-vertical-divider">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblqty" Width="100%" Font-Size="11px" Font-Names="Helvetica, Arial, sans-serif" Text='<%# Bind("quantity") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="UoM" HeaderStyle-Font-Size="13px" HeaderStyle-Font-Names="Helvetica, Arial, sans-serif" HeaderStyle-Font-Bold="true" ItemStyle-CssClass="table-vertical-divider">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lbluom" Width="100%" Font-Size="11px" Font-Names="Helvetica, Arial, sans-serif" Text='<%# Bind("uom") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Instruction" HeaderStyle-Font-Size="13px" HeaderStyle-Font-Names="Helvetica, Arial, sans-serif" HeaderStyle-Font-Bold="true" ItemStyle-CssClass="table-vertical-divider">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblinstruction" Width="100%" Font-Size="11px" Font-Names="Helvetica, Arial, sans-serif" Text='<%# Bind("instruction") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Stock" HeaderStyle-Font-Size="13px" HeaderStyle-Font-Names="Helvetica, Arial, sans-serif" HeaderStyle-Font-Bold="true" ItemStyle-CssClass="table-vertical-divider">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblstock" Width="100%" Font-Size="11px" Font-Names="Helvetica, Arial, sans-serif" Text='<%# Eval("TotalQuantity") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                        </div>

                        <div id="Div_Compounds" runat="server" class="box-table-shadow" style="padding:0px; padding-bottom: 5px; margin-bottom: 20px;">
                        <div style="font-weight:bold; margin-bottom: 6px; padding-left: 6px; padding-top: 6px;">
                            <asp:Image ID="ImageRacikan" runat="server" ImageUrl="~/Images/Worklist/ic_Meds.svg" style="height: 15px; margin-top: -4px;" />
                            Compound
                        </div>
                        <asp:GridView ID="gvw_racikan_header" runat="server" AutoGenerateColumns="False" CssClass="table-condensed table-nursecord" BackColor="White"
                            EmptyDataText="No Data" DataKeyNames="compoundId" OnRowDataBound="gvw_racikan_header_RowDataBound">
                            <Columns>
                                <asp:TemplateField ItemStyle-Width="100%" HeaderStyle-BackColor="#f2f3f4" ItemStyle-CssClass="no-padding">
                                    <HeaderTemplate>
                                        <table style="width: 100%;" class="table-header-racikan">
                                            <tr style="font-weight: bold; background-color: white;">
                                                <th style="width: 30%; padding-left: 10px;">
                                                    <label id="lblbhs_racik_compoundname">Name</label></th>
                                                <th style="width: 15%;">
                                                    <label id="lblbhs_racik_doseuom">Dose</label></th>
                                                <th style="width: 10%;">
                                                    <label id="lblbhs_racik_freq">Frequency</label></th>
                                                <th style="width: 15.5%;">
                                                    <label id="lblbhs_racik_route">Route</label></th>
                                                <th style="width: 15%;">
                                                    <label id="lblbhs_racik_instruction">Instruction</label></th>
                                                <th style="width: 5%; text-align:center">
                                                    <label id="lblbhs_racik_qty">Qty</label></th>
                                                <th style="width: 5%;">
                                                    <label id="lblbhs_racik_uom">UoM</label></th>
                                                <th style="width: 5%; text-align:center;">
                                                    <label id="lblbhs_racik_iter">Iter</label></th>
                                            </tr>
                                        </table>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:HiddenField ID="HF_headerid_racikan" runat="server" Value='<%# Bind("compoundId") %>' />
                                        <table style="width: 100%; border-bottom:1px solid #ddd;" class="table-condensed table-header-racikan">
                                            <tr>
                                                <td style="width: 30%; font-weight: bold; padding-left: 15px;">
                                                    <div>
                                                        <asp:Label ID="lbl_nama_racikan" runat="server" Text='<%# Bind("compoundName") %>'></asp:Label>
                                                    </div>
                                                </td>   
                                                <td style="width: 15%;" class="table-vertical-divider">
                                                    <asp:Label ID="lbl_dosis_racikan" runat="server" Text='<%# Bind("dose") %>' style='<%# Eval("isDoseText").ToString().ToLower() == "true" ? "display:none" : "display:inline" %>'></asp:Label>
                                                    <asp:Label ID="lbl_dosisunit_racikan" runat="server" Text='<%# Bind("doseUom") %>' style='<%# Eval("isDoseText").ToString().ToLower() == "true" ? "display:none" : "display:inline" %>'></asp:Label>
                                                    <asp:Label id="lbl_dosistext_racikan" runat="server"  Text='<%# Bind("doseText") %>' style='<%# Eval("isDoseText").ToString().ToLower() == "true" ? "display:inline" : "display:inline" %>'></asp:Label>
                                                </td>
                                                <td style="width: 10%;" class="table-vertical-divider">
                                                    <asp:Label ID="lbl_frekuensi_racikan" runat="server" Text='<%# Bind("frequency") %>'></asp:Label></td>
                                                <td style="width: 15%;" class="table-vertical-divider">
                                                    <asp:Label ID="lbl_rute_racikan" runat="server" Text='<%# Bind("route") %>'></asp:Label></td>
                                                <td style="width: 15%;" class="table-vertical-divider">
                                                    <asp:Label ID="lbl_instruksi_racikan" runat="server" Text='<%# Bind("instruction") %>'></asp:Label></td>
                                                <td style="width: 5%; text-align: center;" class="table-vertical-divider">
                                                    <asp:Label ID="lbl_jml_racikan" runat="server" Text='<%# Bind("quantity") %>'></asp:Label></td>
                                                <td style="width: 5%;" class="table-vertical-divider">
                                                    <asp:Label ID="lbl_unit_racikan" runat="server" Text='<%# Bind("uom") %>'></asp:Label></td>
                                                <td style="width: 5%; text-align: center;" class="table-vertical-divider">
                                                    <asp:Label ID="lbl_iter_racikan" runat="server" Text='<%# Bind("iter") %>'></asp:Label></td>
                                                        
                                            </tr>
                                        </table>

                                        <div class="row" style="margin-left: 0px; margin-right: 0px; background-color: white; padding-top: 10px; padding-bottom: 10px; border-bottom:1px solid #ddd;">
                                            <div class="col-sm-6" style="border-right: 1px dashed #d0d1d2;">
                                                <asp:GridView ID="gvw_racikan_detail" runat="server" AutoGenerateColumns="False" CssClass="table-detail-racikan"
                                                    DataKeyNames="compoundDetailId">
                                                    <Columns>
                                                        <asp:BoundField ItemStyle-Width="50%" DataField="salesItemName" HeaderText="Item" ShowHeader="false" ItemStyle-VerticalAlign="Top" />
                                                        <asp:TemplateField ItemStyle-Width="15%" HeaderText="Dose" ShowHeader="false">
                                                            <ItemTemplate>
                                                                <asp:Label id="lbldose" runat="server"  Text='<%# Bind("dose") %>' style='<%# Eval("isDoseText").ToString().ToLower() == "true" ? "display:none" : "display:inline" %>'></asp:Label>
                                                                <asp:Label id="lbldoseuom" runat="server"  Text='<%# Bind("doseUom") %>' style='<%# Eval("isDoseText").ToString().ToLower() == "true" ? "display:none" : "display:inline" %>'></asp:Label>
                                                                <asp:Label id="lbldosetext" runat="server"  Text='<%# Bind("doseText") %>' style='<%# Eval("isDoseText").ToString().ToLower() == "true" ? "display:inline" : "display:inline" %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField ItemStyle-Width="5%">
                                                            <ItemTemplate>
                                                                &nbsp;
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField ItemStyle-Width="15%" HeaderText="Stock" ShowHeader="false">
                                                            <ItemTemplate>
                                                                <asp:Label runat="server" ID="lblstock" Text='<%# Eval("TotalQuantity") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:BoundField ItemStyle-Width="10%" DataField="quantity" HeaderText="Qty" ShowHeader="false" ItemStyle-HorizontalAlign="Right" ItemStyle-VerticalAlign="Top" HeaderStyle-CssClass="text-right" Visible="false" />
                                                        <asp:TemplateField ItemStyle-Width="5%">
                                                            <ItemTemplate>
                                                                &nbsp;
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:BoundField ItemStyle-Width="15%" DataField="uom" HeaderText="Unit" ShowHeader="false" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-CssClass="text-left" Visible="false" />
                                                    </Columns>
                                                </asp:GridView>
                                            </div>
                                            <div class="col-sm-6" style="border-left: 1px dashed #d0d1d2; margin-left: -1px">
                                                <label id="lblbhs_racik_note" style="font-weight: bold; font-size:13px;">Instruksi Racikan Untuk Farmasi</label>
                                                <br />
                                                <%--<asp:Label ID="lbl_instruksi_racikan_farmasi" runat="server" Text='<%# Eval("compound_note").ToString().Replace("\n","<br />") %>'></asp:Label>
                                                <asp:HiddenField ID="HF_lbl_instruksi_racikan_farmasi" runat="server" Value='<%# Bind("compound_note") %>' />--%>
                                            </div>
                                        </div>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                        </div>

                        

                        <div class="row">
                            <div class="col-sm-12 text-right">
                                <asp:UpdatePanel ID="UpdatePanelProsesBayar" runat="server">
                                    <ContentTemplate>
                                        <div style="display:inline-block;">
                                        <asp:UpdateProgress ID="UpdateProgressProsesBayar" runat="server" AssociatedUpdatePanelID="UpdatePanelProsesBayar">
                                            <ProgressTemplate>
                                                <img alt="" style="background-color: transparent; height: 20px;" src="<%= Page.ResolveClientUrl("~/Images/Background/small-loader.gif") %>" />
                                            </ProgressTemplate>
                                        </asp:UpdateProgress>
                                        </div>
                                        <asp:Button ID="ButtonCheckPrice" runat="server" Text="Check Price" CssClass="btn btn-primary" OnClick="ButtonCheckPrice_Click"/>
                                        <asp:Button ID="ButtonProsesBayar" runat="server" Text="Proses" CssClass="btn btn-success" OnClientClick="openmodal_resumeorder();"/>
                                        
                                    </ContentTemplate>
                                </asp:UpdatePanel> 
                            </div>
                        </div>

                    </div>
                    <div id="divresultnotfound" runat="server" visible="false" class="text-center" style="padding-top:7%;">
                         <img src="<%= Page.ResolveClientUrl("~/Images/Background/ic_noData.svg") %>" style="height: auto; width: 200px;" />
                        <br />
                        <br />
                        <h4 style="color: #585A6F">Oops! There is no data</h4>
                        <span style="font-size: 14px; color: #585A6F;">Try changing the filters or search term.</span>
                    </div>
                </div>
            </div>
                
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>

        <!-- BGN MODAL -->

        <div class="modal fade" id="modalListPatient" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
            <div class="modal-dialog" style="margin-top:25vh; width:700px;">
                <div class="modal-content" style="border-radius: 7px;">
                    
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                        <h4 class="modal-title" style="text-align: left">
                            <label id="lbltitle_ptnlist"> Choose Your Data </label>
                        </h4>
                    </div>
                    <div class="modal-body no-padding" style="padding-bottom:10px !important;">
                        <asp:UpdatePanel ID="UpdatePanelListPatient" runat="server">
                            <ContentTemplate>
                                <asp:GridView ID="Gvw_listpatient" runat="server" AutoGenerateColumns="False" CssClass="table-condensed table-nursecord" BackColor="White"
                                    EmptyDataText="No Data" Font-Names="Helvetica">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Patient Name" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Font-Size="13px" HeaderStyle-Font-Names="Helvetica, Arial, sans-serif" HeaderStyle-Font-Bold="true">
                                            <ItemTemplate>
                                                <asp:HiddenField ID="HF_ptnID" runat="server" Value='<%# Bind("patientId") %>' />
                                                <asp:HiddenField ID="HF_admID" runat="server" Value='<%# Bind("admissionId") %>' />
                                                <asp:HiddenField ID="HF_encID" runat="server" Value='<%# Bind("encounterId") %>' />
                                                <asp:HiddenField ID="HF_docID" runat="server" Value='<%# Bind("doctorId") %>' />
                                                <asp:Label runat="server" ID="lblptnname" Width="100%" Font-Size="11px" Font-Names="Helvetica, Arial, sans-serif" style="font-weight:bold;" Text='<%# Bind("patientName") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Age" HeaderStyle-Font-Size="13px" HeaderStyle-Font-Names="Helvetica, Arial, sans-serif" HeaderStyle-Font-Bold="true" ItemStyle-CssClass="table-vertical-divider">
                                            <ItemTemplate>
                                                <asp:Label runat="server" ID="lblage" Width="100%" Font-Size="11px" Font-Names="Helvetica, Arial, sans-serif" Text='<%# Bind("age") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="MR No" HeaderStyle-Font-Size="13px" HeaderStyle-Font-Names="Helvetica, Arial, sans-serif" HeaderStyle-Font-Bold="true" ItemStyle-CssClass="table-vertical-divider">
                                            <ItemTemplate>
                                                <asp:Label runat="server" ID="lblmrno" Width="100%" Font-Size="11px" Font-Names="Helvetica, Arial, sans-serif" style="color:#337ab7; font-weight:bold;" Text='<%# Bind("mrNumber") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Adm No" HeaderStyle-Font-Size="13px" HeaderStyle-Font-Names="Helvetica, Arial, sans-serif" HeaderStyle-Font-Bold="true" ItemStyle-CssClass="table-vertical-divider">
                                            <ItemTemplate>
                                                <asp:Label runat="server" ID="lbladmno" Width="100%" Font-Size="11px" Font-Names="Helvetica, Arial, sans-serif" style="color:#337ab7; font-weight:bold;" Text='<%# Bind("AdmissionNo") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Doctor Name" HeaderStyle-Font-Size="13px" HeaderStyle-Font-Names="Helvetica, Arial, sans-serif" HeaderStyle-Font-Bold="true" ItemStyle-CssClass="table-vertical-divider">
                                            <ItemTemplate>
                                                <asp:Label runat="server" ID="lbldoctorname" Width="100%" Font-Size="11px" Font-Names="Helvetica, Arial, sans-serif" Text='<%# Bind("doctorName") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Action" HeaderStyle-Font-Size="13px" HeaderStyle-Font-Names="Helvetica, Arial, sans-serif" HeaderStyle-Font-Bold="true" ItemStyle-CssClass="table-vertical-divider">
                                            <ItemTemplate>
                                                <asp:Button ID="ButtonChoosePatient" runat="server" Text="Choose" CssClass="btn btn-primary btn-sm" OnClick="ButtonChoosePatient_Click"/>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                        
                </div>
            </div>
        </div>


        <div class="modal fade" id="modalResumeOrder" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
            <div class="modal-dialog" style="margin-top:20vh; min-width:700px;">
                <div class="modal-content" style="border-radius: 7px;">
                    
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                        <h4 class="modal-title" style="text-align: left">
                            <label id="lbltitle_order"> Resume Order </label>
                        </h4>
                    </div>
                    <div class="modal-body">
                        <asp:UpdatePanel ID="UpdatePanelResumeOrder" runat="server">
                            <ContentTemplate>

                                <div class="row">
                                    <div id="div_notifmsg" runat="server" class="col-sm-12" style="display:none; text-align: center; background-color: #ff4f4f; margin-top: -14px; min-height: 30px; padding-top:4px; color:white;">
                                        <b><asp:Label ID="LabelNotifMsg" runat="server" Text="-"></asp:Label></b>
                                    </div>
                                    <div class="col-sm-7" style="border-right: 1px solid #e5e5e5;">
                                        <div style="padding:10px; text-align:left;">
                                            <table class="table table-condensed">
                                                <tr>
                                                    <td><b>Nama</b></td>
                                                    <td>:</td>
                                                    <td><asp:Label ID="LabelNamaPatient" runat="server" Text="-"></asp:Label></td>
                                                </tr>
                                                <tr>
                                                    <td><b>Umur</b></td>
                                                    <td>:</td>
                                                    <td><asp:Label ID="LabelAgePatient" runat="server" Text="-"></asp:Label></td>
                                                </tr>
                                                <tr>
                                                    <td><b>MR No</b></td>
                                                    <td>:</td>
                                                    <td><asp:Label ID="LabelMRPatient" runat="server" Text="-"></asp:Label></td>
                                                </tr>
                                                <tr>
                                                    <td><b>Dokter</b></td>
                                                    <td>:</td>
                                                    <td><asp:Label ID="LabelDokterPatient" runat="server" Text="-"></asp:Label></td>
                                                </tr>
                                            </table>
                           
                                            <table style="width:100%;">
                                                <tr>
                                                    <td style="width:50%; vertical-align:top;">
                                                        <b>Total Item : </b><br />
                                                        <asp:Label ID="LabelLabCount" runat="server" Text="-" style="display:block;"></asp:Label>
                                                        <asp:Label ID="LabelRadCount" runat="server" Text="-" style="display:block;"></asp:Label>
                                                        <asp:Label ID="LabelProcCount" runat="server" Text="-" style="display:block;"></asp:Label>
                                                        <asp:Label ID="LabelDrugsCount" runat="server" Text="-" style="display:block;"></asp:Label>
                                                        <asp:Label ID="LabelConsCount" runat="server" Text="-" style="display:block;"></asp:Label>
                                                        <asp:Label ID="LabelRacikanCount" runat="server" Text="-" style="display:block;"></asp:Label>
                                                        
                                                    </td>
                                                    <td style="width:50%; vertical-align:top;">
                                                        <b>Prescription Flag : </b><br />
                                                        <asp:Label ID="LabelDrugsFlag" runat="server" Text="-" style="display:block;"></asp:Label>
                                                    </td>
                                                </tr>
                                            </table>

                                        </div>
                                    </div>
                                    <div class="col-sm-5">
                                         <div style="padding:10px; text-align:center;">
                                            <b>Pilih Counter Pharmacy :</b><br />
                                            <asp:DropDownList ID="DDl_PilihPharmacy" runat="server" CssClass="form-control" style="display:inline-block; width:250px;">
                                                <asp:ListItem>-select-</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                        <div style="padding:10px; text-align:center;">
                                            <b>Pilih Counter Cashier :</b><br />
                                            <asp:DropDownList ID="DDl_PilihCashier" runat="server" CssClass="form-control" style="display:inline-block; width:250px;">
                                                <asp:ListItem>-select-</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                        <div style="padding:10px; text-align:center;">
                                            <asp:Button ID="ButtonSumbit" runat="server" Text="SUBMIT" CssClass="btn btn-success" OnClick="ButtonSumbit_Click" />
                                            <asp:UpdateProgress ID="UpdateProgressSubmit" runat="server" AssociatedUpdatePanelID="UpdatePanelResumeOrder">
                                            <ProgressTemplate>
                                                <div class="modal-backdrop" style="background-color: white; opacity: 0; text-align: center">
                                                </div>
                                                <img alt="" style="background-color: transparent; height: 20px;" src="<%= Page.ResolveClientUrl("~/Images/Background/small-loader.gif") %>" />
                                            </ProgressTemplate>
                                        </asp:UpdateProgress>
                                        </div>
                                    </div>
                                </div>

                               
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                        
                </div>
            </div>
        </div>


        <div class="modal fade" id="modalSubmitResult" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true" data-backdrop="static" data-keyboard="false">
            <div class="modal-dialog" style="margin-top:20vh; width:450px;">
                <div class="modal-content" style="border-radius: 7px;">
                    
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                        <%--<h4 class="modal-title" style="text-align: left">
                            <label id="lbltitle_order"> Resume Order </label>
                        </h4>--%>
                    </div>
                    <div class="modal-body">
                        <asp:UpdatePanel ID="UpdatePanelResult" runat="server">
                            <ContentTemplate>

                                <div class="row">
                                    <div class="col-sm-12" style="text-align:center;">
                                        <div id="div_modalsuccess" runat="server">
                                            <span style="display:block; font-size: 90px; color: #5cb85c;" class="fa fa-check-circle"></span>
                                        </div>
                                        <div id="div_modalfail" runat="server">
                                            <span style="display:block; font-size: 90px; color: #d9534f;" class="fa fa-times-circle"></span>
                                        </div>
                                         <b><asp:Label ID="LabelSumbitResult" runat="server" Text="-" style="display:block;"></asp:Label></b>

                                        <asp:Button ID="ButtonOKResult" runat="server" Text="OK" style="margin-top:15px;" CssClass="btn btn-success" OnClick="ButtonOKResult_Click" />
                                    </div>
                                </div>

                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                        
                </div>
            </div>
        </div>

      
        <div class="modal fade" id="modalCheckPrice" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
            <div class="modal-dialog" style="width: 70%">
                <div class="modal-content" style="border-radius: 5px">

                    <asp:UpdatePanel runat="server" ID="updatepanelChkPrice">
                        <ContentTemplate>
                            <div class="modal-header" style="height: 40px; padding-top: 10px; padding-bottom: 5px">
                                <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                                <h5 class="modal-title">
                                    <asp:Label ID="Label24" Style="font-family: Helvetica; font-weight: bold; font-size: 14px" runat="server" Text="Check price to HOPE"></asp:Label></h5>
                            </div>
                            <div class="modal-body" style="padding-left:0; padding-right:0px; padding-bottom:0px;">
                                <div id="divdrugprice" runat="server">
                                    <label style="font-weight:bold; margin-left:15px;"> Drugs </label>
                                    <br />
                                    <table class="table table-condensed table-striped" style="border-bottom: 1px solid #f4f4f4;">
                                        <asp:Repeater ID="RepeaterDrugPrice" runat="server">
                                            <HeaderTemplate>
                                                <tr>
                                                    <th style="padding-left:15px;"> Item </th>
                                                    <th style="text-align:right;"> Qty </th>
                                                    <th style="text-align:left;"> UoM </th>
                                                    <th style="text-align:right;"> Price </th>
                                                    <th style="text-align:right;"> Amount </th>
                                                    <th style="text-align:right;"> Disc. </th>
                                                    <th style="text-align:right; border-left:1px solid lightgray;"> Patient Net </th>
                                                    <th style="text-align:right; border-left:1px solid lightgray; padding-right:15px;"> Payer Net </th>
                                                </tr>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <tr>
                                                    <td style="width:30%; padding-left:15px;">
                                                        <asp:Label ID="LabelItemName" runat="server" Text='<%# Bind("SalesItemName") %>'></asp:Label>
                                                    </td>
                                                    <td style="width:5%; text-align:right">
                                                        <asp:Label ID="LabelQty" runat="server" Text='<%# Eval("Quantity") %>'></asp:Label>
                                                    </td>
                                                    <td style="width:5%; text-align:left" >
                                                        <asp:Label ID="LabelUoM" runat="server" Text='<%# Bind("Uom") %>'></asp:Label>
                                                    </td>
                                                    <td style="width:10%; text-align:right">
                                                        <asp:Label ID="LabelPrice" runat="server" Text='<%# Decimal.Parse(Eval("SinglePrice").ToString().Replace(".",","),new System.Globalization.CultureInfo("id-ID")).ToString("#,##0.00") %>'></asp:Label>

                                                    </td>
                                                    <td style="width:10%; text-align:right">
                                                        <asp:Label ID="LabelAmount" runat="server" Text='<%# Decimal.Parse(Eval("Amount").ToString().Replace(".",","),new System.Globalization.CultureInfo("id-ID")).ToString("#,##0.00") %>'></asp:Label>
                                                    </td>
                                                    <td style="width:10%; text-align:right">
                                                        <asp:Label ID="LabelDisc" runat="server" Text='<%# Decimal.Parse(Eval("DiscountPrice").ToString().Replace(".",","),new System.Globalization.CultureInfo("id-ID")).ToString("#,##0.00") %>'></asp:Label>
                                                    </td>
                                                    <td style="width:15%; text-align:right; border-left:1px solid lightgray;">
                                                        <asp:Label ID="LabelPatientNet" runat="server" Text='<%# Decimal.Parse(Eval("PatientNet").ToString().Replace(".",","),new System.Globalization.CultureInfo("id-ID")).ToString("#,##0.00") %>'></asp:Label>
                                                    </td>
                                                    <td style="width:15%; text-align:right; border-left:1px solid lightgray; padding-right:15px;">
                                                        <asp:Label ID="LabelPayerNet" runat="server" Text='<%# Decimal.Parse(Eval("PayerNet").ToString().Replace(".",","),new System.Globalization.CultureInfo("id-ID")).ToString("#,##0.00") %>'></asp:Label>
                                                    </td>
                                                </tr>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </table>
                                    
                                </div> 
                                
                                <div id="divconsprice" runat="server">
                                    <label style="font-weight:bold; margin-left:15px;"> Consumables </label>
                                    <br />
                                    <table class="table table-condensed table-striped" style="border-bottom: 1px solid #f4f4f4;">
                                        <asp:Repeater ID="RepeaterConsPrice" runat="server">
                                            <HeaderTemplate>
                                                <tr>
                                                    <th style="padding-left:15px;"> Item </th>
                                                    <th style="text-align:right;"> Qty </th>
                                                    <th style="text-align:left;"> UoM </th>
                                                    <th style="text-align:right;"> Price </th>
                                                    <th style="text-align:right;"> Amount </th>
                                                    <th style="text-align:right;"> Disc. </th>
                                                    <th style="text-align:right; border-left:1px solid lightgray;"> Patient Net </th>
                                                    <th style="text-align:right; border-left:1px solid lightgray; padding-right:15px;"> Payer Net </th>
                                                </tr>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <tr>
                                                    <td style="width:30%; padding-left:15px;">
                                                        <asp:Label ID="LabelItemName" runat="server" Text='<%# Bind("SalesItemName") %>'></asp:Label>
                                                    </td>
                                                    <td style="width:5%; text-align:right">
                                                        <asp:Label ID="LabelQty" runat="server" Text='<%# Eval("Quantity") %>'></asp:Label>
                                                    </td>
                                                    <td style="width:5%; text-align:left" >
                                                        <asp:Label ID="LabelUoM" runat="server" Text='<%# Bind("Uom") %>'></asp:Label>
                                                    </td>
                                                    <td style="width:10%; text-align:right">
                                                        <asp:Label ID="LabelPrice" runat="server" Text='<%# Decimal.Parse(Eval("SinglePrice").ToString().Replace(".",","),new System.Globalization.CultureInfo("id-ID")).ToString("#,##0.00") %>'></asp:Label>
                                                    </td>
                                                    <td style="width:10%; text-align:right">
                                                        <asp:Label ID="LabelAmount" runat="server" Text='<%# Decimal.Parse(Eval("Amount").ToString().Replace(".",","),new System.Globalization.CultureInfo("id-ID")).ToString("#,##0.00") %>'></asp:Label>
                                                    </td>
                                                    <td style="width:10%; text-align:right">
                                                        <asp:Label ID="LabelDisc" runat="server" Text='<%# Decimal.Parse(Eval("DiscountPrice").ToString().Replace(".",","),new System.Globalization.CultureInfo("id-ID")).ToString("#,##0.00") %>'></asp:Label>
                                                    </td>
                                                    <td style="width:15%; text-align:right; border-left:1px solid lightgray;">
                                                        <asp:Label ID="LabelPatientNet" runat="server" Text='<%# Decimal.Parse(Eval("PatientNet").ToString().Replace(".",","),new System.Globalization.CultureInfo("id-ID")).ToString("#,##0.00") %>'></asp:Label>
                                                    </td>
                                                    <td style="width:15%; text-align:right; border-left:1px solid lightgray; padding-right:15px;">
                                                        <asp:Label ID="LabelPayerNet" runat="server" Text='<%# Decimal.Parse(Eval("PayerNet").ToString().Replace(".",","),new System.Globalization.CultureInfo("id-ID")).ToString("#,##0.00") %>'></asp:Label>
                                                    </td>
                                                </tr>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </table>
                                    
                                </div>

                                <div id="divlabprice" runat="server">
                                    <label style="font-weight:bold; margin-left:15px;"> Laboratorium </label>
                                    <br />
                                    <table class="table table-condensed table-striped" style="border-bottom: 1px solid #f4f4f4;">
                                        <asp:Repeater ID="RepeaterLabPrice" runat="server">
                                            <HeaderTemplate>
                                                <tr>
                                                    <th style="padding-left:15px;"> Item </th>
                                                    <th style="text-align:right;"> Qty </th>
                                                    <th style="text-align:left;"> UoM </th>
                                                    <th style="text-align:right;"> Price </th>
                                                    <th style="text-align:right;"> Amount </th>
                                                    <th style="text-align:right;"> Disc. </th>
                                                    <th style="text-align:right; border-left:1px solid lightgray;"> Patient Net </th>
                                                    <th style="text-align:right; border-left:1px solid lightgray; padding-right:15px;"> Payer Net </th>
                                                </tr>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <tr>
                                                    <td style="width:30%; padding-left:15px;">
                                                        <asp:Label ID="LabelItemName" runat="server" Text='<%# Bind("SalesItemName") %>'></asp:Label>
                                                    </td>
                                                    <td style="width:5%; text-align:right">
                                                        <asp:Label ID="LabelQty" runat="server" Text='<%# Eval("Quantity") %>'></asp:Label>
                                                    </td>
                                                    <td style="width:5%; text-align:left" >
                                                        <asp:Label ID="LabelUoM" runat="server" Text='<%# Bind("Uom") %>'></asp:Label>
                                                    </td>
                                                    <td style="width:10%; text-align:right">
                                                        <asp:Label ID="LabelPrice" runat="server" Text='<%# Decimal.Parse(Eval("SinglePrice").ToString().Replace(".",","),new System.Globalization.CultureInfo("id-ID")).ToString("#,##0.00") %>'></asp:Label>
                                                    </td>
                                                    <td style="width:10%; text-align:right">
                                                        <asp:Label ID="LabelAmount" runat="server" Text='<%# Decimal.Parse(Eval("Amount").ToString().Replace(".",","),new System.Globalization.CultureInfo("id-ID")).ToString("#,##0.00") %>'></asp:Label>
                                                    </td>
                                                    <td style="width:10%; text-align:right">
                                                        <asp:Label ID="LabelDisc" runat="server" Text='<%# Decimal.Parse(Eval("DiscountPrice").ToString().Replace(".",","),new System.Globalization.CultureInfo("id-ID")).ToString("#,##0.00") %>'></asp:Label>
                                                    </td>
                                                    <td style="width:15%; text-align:right; border-left:1px solid lightgray;">
                                                        <asp:Label ID="LabelPatientNet" runat="server" Text='<%# Decimal.Parse(Eval("PatientNet").ToString().Replace(".",","),new System.Globalization.CultureInfo("id-ID")).ToString("#,##0.00") %>'></asp:Label>
                                                    </td>
                                                    <td style="width:15%; text-align:right; border-left:1px solid lightgray; padding-right:15px;">
                                                        <asp:Label ID="LabelPayerNet" runat="server" Text='<%# Decimal.Parse(Eval("PayerNet").ToString().Replace(".",","),new System.Globalization.CultureInfo("id-ID")).ToString("#,##0.00") %>'></asp:Label>
                                                    </td>
                                                </tr>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </table>
                                    
                                </div>

                                <div id="divradprice" runat="server">
                                    <label style="font-weight:bold; margin-left:15px;"> Radiologi </label>
                                    <br />
                                    <table class="table table-condensed table-striped" style="border-bottom: 1px solid #f4f4f4;">
                                        <asp:Repeater ID="RepeaterRadPrice" runat="server">
                                            <HeaderTemplate>
                                                <tr>
                                                    <th style="padding-left:15px;"> Item </th>
                                                    <th style="text-align:right;"> Qty </th>
                                                    <th style="text-align:left;"> UoM </th>
                                                    <th style="text-align:right;"> Price </th>
                                                    <th style="text-align:right;"> Amount </th>
                                                    <th style="text-align:right;"> Disc. </th>
                                                    <th style="text-align:right; border-left:1px solid lightgray;"> Patient Net </th>
                                                    <th style="text-align:right; border-left:1px solid lightgray; padding-right:15px;"> Payer Net </th>
                                                </tr>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <tr>
                                                    <td style="width:30%; padding-left:15px;">
                                                        <asp:Label ID="LabelItemName" runat="server" Text='<%# Bind("SalesItemName") %>'></asp:Label>
                                                    </td>
                                                    <td style="width:5%; text-align:right">
                                                        <asp:Label ID="LabelQty" runat="server" Text='<%# Eval("Quantity") %>'></asp:Label>
                                                    </td>
                                                    <td style="width:5%; text-align:left" >
                                                        <asp:Label ID="LabelUoM" runat="server" Text='<%# Bind("Uom") %>'></asp:Label>
                                                    </td>
                                                    <td style="width:10%; text-align:right">
                                                        <asp:Label ID="LabelPrice" runat="server" Text='<%# Decimal.Parse(Eval("SinglePrice").ToString().Replace(".",","),new System.Globalization.CultureInfo("id-ID")).ToString("#,##0.00") %>'></asp:Label>
                                                    </td>
                                                    <td style="width:10%; text-align:right">
                                                        <asp:Label ID="LabelAmount" runat="server" Text='<%# Decimal.Parse(Eval("Amount").ToString().Replace(".",","),new System.Globalization.CultureInfo("id-ID")).ToString("#,##0.00") %>'></asp:Label>
                                                    </td>
                                                    <td style="width:10%; text-align:right">
                                                        <asp:Label ID="LabelDisc" runat="server" Text='<%# Decimal.Parse(Eval("DiscountPrice").ToString().Replace(".",","),new System.Globalization.CultureInfo("id-ID")).ToString("#,##0.00") %>'></asp:Label>
                                                    </td>
                                                    <td style="width:15%; text-align:right; border-left:1px solid lightgray;">
                                                        <asp:Label ID="LabelPatientNet" runat="server" Text='<%# Decimal.Parse(Eval("PatientNet").ToString().Replace(".",","),new System.Globalization.CultureInfo("id-ID")).ToString("#,##0.00") %>'></asp:Label>
                                                    </td>
                                                    <td style="width:15%; text-align:right; border-left:1px solid lightgray; padding-right:15px;">
                                                        <asp:Label ID="LabelPayerNet" runat="server" Text='<%# Decimal.Parse(Eval("PayerNet").ToString().Replace(".",","),new System.Globalization.CultureInfo("id-ID")).ToString("#,##0.00") %>'></asp:Label>
                                                    </td>
                                                </tr>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </table>
                                    
                                </div>

                                <div id="divdiagprice" runat="server">
                                    <label style="font-weight:bold; margin-left:15px;"> Diagnostik </label>
                                    <br />
                                    <table class="table table-condensed table-striped" style="border-bottom: 1px solid #f4f4f4;">
                                        <asp:Repeater ID="RepeaterDiagPrice" runat="server">
                                            <HeaderTemplate>
                                                <tr>
                                                    <th style="padding-left:15px;"> Item </th>
                                                    <th style="text-align:right;"> Qty </th>
                                                    <th style="text-align:left;"> UoM </th>
                                                    <th style="text-align:right;"> Price </th>
                                                    <th style="text-align:right;"> Amount </th>
                                                    <th style="text-align:right;"> Disc. </th>
                                                    <th style="text-align:right; border-left:1px solid lightgray;"> Patient Net </th>
                                                    <th style="text-align:right; border-left:1px solid lightgray; padding-right:15px;"> Payer Net </th>
                                                </tr>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <tr>
                                                    <td style="width:30%; padding-left:15px;">
                                                        <asp:Label ID="LabelItemName" runat="server" Text='<%# Bind("SalesItemName") %>'></asp:Label>
                                                    </td>
                                                    <td style="width:5%; text-align:right">
                                                        <asp:Label ID="LabelQty" runat="server" Text='<%# Eval("Quantity") %>'></asp:Label>
                                                    </td>
                                                    <td style="width:5%; text-align:left" >
                                                        <asp:Label ID="LabelUoM" runat="server" Text='<%# Bind("Uom") %>'></asp:Label>
                                                    </td>
                                                    <td style="width:10%; text-align:right">
                                                        <asp:Label ID="LabelPrice" runat="server" Text='<%# Decimal.Parse(Eval("SinglePrice").ToString().Replace(".",","),new System.Globalization.CultureInfo("id-ID")).ToString("#,##0.00") %>'></asp:Label>
                                                    </td>
                                                    <td style="width:10%; text-align:right">
                                                        <asp:Label ID="LabelAmount" runat="server" Text='<%# Decimal.Parse(Eval("Amount").ToString().Replace(".",","),new System.Globalization.CultureInfo("id-ID")).ToString("#,##0.00") %>'></asp:Label>
                                                    </td>
                                                    <td style="width:10%; text-align:right">
                                                        <asp:Label ID="LabelDisc" runat="server" Text='<%# Decimal.Parse(Eval("DiscountPrice").ToString().Replace(".",","),new System.Globalization.CultureInfo("id-ID")).ToString("#,##0.00") %>'></asp:Label>
                                                    </td>
                                                    <td style="width:15%; text-align:right; border-left:1px solid lightgray;">
                                                        <asp:Label ID="LabelPatientNet" runat="server" Text='<%# Decimal.Parse(Eval("PatientNet").ToString().Replace(".",","),new System.Globalization.CultureInfo("id-ID")).ToString("#,##0.00") %>'></asp:Label>
                                                    </td>
                                                    <td style="width:15%; text-align:right; border-left:1px solid lightgray; padding-right:15px;">
                                                        <asp:Label ID="LabelPayerNet" runat="server" Text='<%# Decimal.Parse(Eval("PayerNet").ToString().Replace(".",","),new System.Globalization.CultureInfo("id-ID")).ToString("#,##0.00") %>'></asp:Label>
                                                    </td>
                                                </tr>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </table>
                                    
                                </div>

                                <div id="totalprice">
                                    <table class="table table-condensed" style="margin-bottom:0px;">
                                        <tr style="background-color:#fff4b2;">
                                            <th style="width:70%; text-align:right;"> Subtotal </th>
                                            <th style="width:15%; text-align:right; border-left:1px solid lightgray;"> <asp:Label ID="LabelPatientSubtotal" runat="server" Text="-"></asp:Label> </th>
                                            <th style="width:15%; text-align:right; border-left:1px solid lightgray; padding-right:15px;"> <asp:Label ID="LabelPayerSubtotal" runat="server" Text="-"></asp:Label> </th>
                                        </tr>
                                        <tr>
                                            <th style="width:70%; text-align:right"> Rounding </th>
                                            <th style="width:15%; text-align:right; border-left:1px solid lightgray;"> <asp:Label ID="LabelPatientRounding" runat="server" Text="-"></asp:Label> </th>
                                            <th style="width:15%; text-align:right; border-left:1px solid lightgray; padding-right:15px;"> <asp:Label ID="LabelPayerRounding" runat="server" Text="-"></asp:Label> </th>
                                        </tr>
                                        <tr style="background-color:#f4f4f4;">
                                            <th style="width:70%; text-align:right"> Consultation Fee </th>
                                            <th style="width:15%; text-align:right; border-left:1px solid lightgray;"> <asp:Label ID="LabelConsultationFeePatient" runat="server" Text="-"></asp:Label> </th>
                                            <th style="width:15%; text-align:right; border-left:1px solid lightgray; padding-right:15px;"> <asp:Label ID="LabelConsultationFeePayer" runat="server" Text="-"></asp:Label> </th>
                                        </tr>
                                        <tr style="background-color:#f4f4f4;">
                                            <th style="width:70%; text-align:right"> Administration Fee </th>
                                            <th style="width:15%; text-align:right; border-left:1px solid lightgray;"> <asp:Label ID="LabelAdminFeePatient" runat="server" Text="-"></asp:Label> </th>
                                            <th style="width:15%; text-align:right; border-left:1px solid lightgray; padding-right:15px;"> <asp:Label ID="LabelAdminFeePayer" runat="server" Text="-"></asp:Label> </th>
                                        </tr>
                                        <tr style="background-color:#c9e1c2; border-top: 2px solid #83ab78; font-size:15px;">
                                            <th style="width:70%; text-align:right"> Total </th>
                                            <th style="width:15%; text-align:right; border-left:1px solid lightgray;"> <asp:Label ID="LabelPatientTotal" runat="server" Text="-"></asp:Label> </th>
                                            <th style="width:15%; text-align:right; border-left:1px solid lightgray; padding-right:15px;"> <asp:Label ID="LabelPayerTotal" runat="server" Text="-"></asp:Label> </th>
                                        </tr>
                                    </table>
                                </div>

                            </div>
                            <div class="modal-footer" style="width: 100%;">
                                   <%--<div style="text-align: center; display:none;">
                                        <div style="display:inline-block; background-color:#b2d2ff; border-radius:7px; font-weight:bold; padding:7px;">
                                            Consultation Fee &nbsp;
                                            <asp:Label ID="LabelConsultationFee" runat="server" Text="-" style="background-color:white; padding:2px 5px; border-radius:4px;"></asp:Label>
                                        </div>
                                    </div>--%>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
        </div>
       

        <!-- END MODAL -->

        <%-- ################# --%>


        <script type="text/javascript">

            function openmodal_patientlist() {
                $('#modalListPatient').modal('show');
            }

            function closemodal_patientlist() {
                $('#modalListPatient').modal('hide');
            }

            function openmodal_resumeorder() {
                document.getElementById("<%= DDl_PilihPharmacy.ClientID %>").selectedIndex = 0;
                document.getElementById("<%= DDl_PilihCashier.ClientID %>").selectedIndex = 0;
                $('#modalResumeOrder').modal('show');
            }

            function closemodal_resumeorder() {
                $('#modalResumeOrder').modal('hide');
            }

            function openmodal_resumeresult() {
                $('#modalResumeOrder').modal('hide');
                $('#modalSubmitResult').modal('show');
            }

            function closemodal_resumeresult() {
                $('#modalSubmitResult').modal('hide');
            }

            function dateAdm_MR() {
                var dp = $('#<%=TxtSearchTglAdmMR.ClientID%>');
                dp.datepicker({
                    changeMonth: true,
                    changeYear: true,
                    format: "dd M yyyy",
                    language: "tr",
                    todayHighlight: true
                }).on('changeDate', function (ev) {
                    $(this).blur();
                    $(this).datepicker('hide');
                });
            }

            function dateTglLahir_Pasien() {
                var dp = $('#<%=TxtSearchTglLahirPasien.ClientID%>');
                dp.datepicker({
                    changeMonth: true,
                    changeYear: true,
                    format: "dd M yyyy",
                    language: "tr",
                    todayHighlight: true
                }).on('changeDate', function (ev) {
                    $(this).blur();
                    $(this).datepicker('hide');
                });
            }

            function dateAdm_Pasien() {
                var dp = $('#<%=TxtSearchTglAdmPasien.ClientID%>');
                dp.datepicker({
                    changeMonth: true,
                    changeYear: true,
                    format: "dd M yyyy",
                    language: "tr",
                    todayHighlight: true
                }).on('changeDate', function (ev) {
                    $(this).blur();
                    $(this).datepicker('hide');
                });
            }

            function dateAdm_Barcode() {
                var dp = $('#<%=TxtSearchTglAdmBarcode.ClientID%>');
                dp.datepicker({
                    changeMonth: true,
                    changeYear: true,
                    format: "dd M yyyy",
                    language: "tr",
                    todayHighlight: true
                }).on('changeDate', function (ev) {
                    $(this).blur();
                    $(this).datepicker('hide');
                });
            }

            function searchinputvalidationMR() {
                var inp1 = document.getElementById("<%= TxtSearchMR.ClientID %>");
                var inp2 = document.getElementById("<%= TxtSearchTglAdmMR.ClientID %>");
                inp1.classList.remove("textbox-red");
                inp2.classList.remove("textbox-red");

                if (inp1.value == "") {
                    inp1.classList.add("textbox-red");
                    return false;
                }
                if (inp2.value == "") {
                    inp2.classList.add("textbox-red");
                    return false;
                }
                return true;
            }

            function searchinputvalidationPasien() {
                var inp1 = document.getElementById("<%= TxtSearchPasien.ClientID %>");
                var inp2 = document.getElementById("<%= TxtSearchTglLahirPasien.ClientID %>");
                var inp3 = document.getElementById("<%= TxtSearchTglAdmPasien.ClientID %>");
                inp1.classList.remove("textbox-red");
                inp2.classList.remove("textbox-red");
                inp3.classList.remove("textbox-red");

                if (inp1.value == "") {
                    inp1.classList.add("textbox-red");
                    return false;
                }
                if (inp2.value == "") {
                    inp2.classList.add("textbox-red");
                    return false;
                }
                if (inp3.value == "") {
                    inp3.classList.add("textbox-red");
                    return false;
                }
                return true;
            }

            function searchinputvalidationBarcode() {
                var inp1 = document.getElementById("<%= TxtSearchBarcode.ClientID %>");
                var inp2 = document.getElementById("<%= TxtSearchTglAdmBarcode.ClientID %>");
                inp1.classList.remove("textbox-red");
                inp2.classList.remove("textbox-red");

                if (inp1.value == "") {
                    inp1.classList.add("textbox-red");
                    return false;
                }
                if (inp2.value == "") {
                    inp2.classList.add("textbox-red");
                    return false;
                }
                return true;
            }

            function klikChk(obj_rad_id, obj_div_id, obj_group) {
                var obj_grp = document.getElementsByClassName(obj_group);
                for (var i = 0; i < obj_grp.length; i++) {
                    obj_grp[i].classList = "chk-box chk-drug";
                }

                var obj_rad = document.getElementById(obj_rad_id);
                var obj_div = document.getElementById(obj_div_id);

                if (obj_rad != null) {
                    if (obj_rad.checked == true) {
                        obj_div.classList = "chk-box-checked chk-drug";
                    }
                    else if (obj_rad.checked == false) {
                        obj_div.classList = "chk-box chk-drug";
                    }
                }

                if (obj_rad_id == "RadioDrug1") {
                    var lblchkDrug = document.getElementById("<%= LabelDrugsFlag.ClientID %>").innerText = "(Ambil Semua)";
                }
                else if (obj_rad_id == "RadioDrug2") {
                    var lblchkDrug = document.getElementById("<%= LabelDrugsFlag.ClientID %>").innerText = "(Ada Perubahan)";
                }
            }

            $(document).ready(function () {

                var prm = Sys.WebForms.PageRequestManager.getInstance();
                if (prm != null) {
                    prm.add_endRequest(function (sender, e) {
                        if (sender._postBackSettings.panelsToUpdate != null) {

                            var chkDrug1 = document.getElementById("<%= RadioDrug1.ClientID %>");
                            var chkDrug2 = document.getElementById("<%= RadioDrug2.ClientID %>");
                            if (chkDrug1 != null && chkDrug2 != null) {
                                if (chkDrug1.checked == true) {
                                    klikChk('RadioDrug1', 'divchkdrug1', 'chk-drug');
                                }
                                else if (chkDrug2.checked == true) {
                                    klikChk('RadioDrug2', 'divchkdrug2', 'chk-drug');
                                }
                            }

                        }
                    });
                };
            });

            function onEnterByMR() {
                var e = event.keyCode;
                if (e == 13) {
                    
                    document.getElementById("<%= BtnCariPasienMR.ClientID %>").click();
                    return false;
                }
            }

            function onEnterByPasien() {
                var e = event.keyCode;
                if (e == 13) {
                    
                    document.getElementById("<%= BtnCariPasienPasien.ClientID %>").click();
                    return false;
                }
            }

            function onEnterByBarcode() {
                var e = event.keyCode;
                if (e == 13) {
                    
                    document.getElementById("<%= BtnCariPasienBarcode.ClientID %>").click();
                    return false;
                }
            }
            

            function preventEnter() {
                var e = event.keyCode;
                if (e == 13) {
                    return false;
                }
            }
        </script>

    </form>
</body>

</html>



