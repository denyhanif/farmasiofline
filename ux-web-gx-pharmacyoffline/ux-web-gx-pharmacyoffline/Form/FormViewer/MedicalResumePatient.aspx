<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MedicalResumePatient.aspx.cs" Inherits="Form_FormViewer_MedicalResumePatient" %>

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
    <link rel="stylesheet" href="~/Content/plugins/datepicker/datepicker130.css" />
    <!-- Mask -->
    <link rel="stylesheet" href="~/Content/plugins/jasny-bootstrap/css/jasny-bootstrap.min.css" />
    <!-- Theme style -->
    <link rel="stylesheet" href="~/Content/dist/css/AdminLTE.css" />
    <link rel="stylesheet" href="~/Content/dist/css/skins/skin-blue-light.css" />
    <%--<link rel="stylesheet" href="~/Content/Site.css" />
    <link rel="stylesheet" href="~/Content/EMR-PharmacyOffline.css" />--%>
    <!-- Select Bootstrap -->
    <link rel="stylesheet" href="~/Content/bootstrap-select/css/bootstrap-select.css" />

    <link href="~/favicon.ico" rel="shortcut icon" type="image/x-icon" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />

    <style type="text/css">
        .table-divider tr td {
            border-bottom: 1px solid lightgrey;
        }

        .table-left-bg {
            background-color: whitesmoke;
            width: 20%;
            font-weight:bold;
            font-size:12px;
        }

        .table-right-bg {
            background-color: white;
            width: 80%;
            font-size:12px;
            padding-left:0px !important; 
            padding-right:0px !important;
        }

        .padding-grp-div-parent {
            padding-top: 0px !important;
            padding-bottom: 0px !important;
        }

        .padding-grp-div-child {
            vertical-align:top;
            padding:8px 8px 8px 8px;
        }

        .border-left {
            border-left: 1px solid lightgrey;
        }


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

        <%@ Register Src="~/Form/FormViewer/Control/ViewDrugs.ascx" TagPrefix="uc" TagName="ViewDrugs" %>
        <%@ Register Src="~/Form/FormViewer/Control/ViewConsumables.ascx" TagPrefix="uc" TagName="ViewCons" %>
        <%@ Register Src="~/Form/FormViewer/Control/ViewRacikan.ascx" TagPrefix="uc" TagName="ViewRacikan" %>

        <div id="div_header_1" runat="server" style="background-color:white; margin:20px;" visible="false">
            <div class="container-fluid">
                <div class="row">
                    <div class="col-sm-6"></div>
                    <div class="col-sm-6" style="padding-right: 2px;">
                        <table class="table-condensed table-divider" style="float:right;">
                            <tr>
                                <td>
                                    MR. No
                                </td>
                                <td> : </td>
                                <td>
                                    <asp:Label ID="lbl_mrno_header" runat="server" Text="-"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Name
                                </td>
                                <td> : </td>
                                <td>
                                    <asp:Label ID="lbl_name_header" runat="server" Text="-"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    DOB/Age
                                </td>
                                <td> : </td>
                                <td>
                                    <asp:Label ID="lbl_dob_header" runat="server" Text="-"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Sex
                                </td>
                                <td> : </td>
                                <td>
                                    <asp:Label ID="lbl_sex_header" runat="server" Text="-"></asp:Label>
                                </td>
                            </tr>
                            <tr style="display:none;">
                                <td>
                                    Doctor
                                </td>
                                <td> : </td>
                                <td>
                                    <asp:Label ID="lbl_doctor_header" runat="server" Text="-"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Adm. No
                                </td>
                                <td> : </td>
                                <td>
                                    <asp:Label ID="lbl_admno_header" runat="server" Text="-"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Payer
                                </td>
                                <td> : </td>
                                <td>
                                    <asp:Label ID="lbl_payer_header" runat="server" Text="-"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
            </div>
        </div>
        <div id="div_header_2" runat="server" style="background-color:whitesmoke; margin:20px; padding:10px 0px; border-radius:7px;" visible="false">
            <div class="container-fluid">
                <div class="row">
                    <div class="col-sm-12">
                        <b> <asp:Label ID="lbl_name_header2" runat="server" Text="-" style="font-size: 20px;"></asp:Label> </b>
                        <br />
                        <b>Adm. No :</b> <asp:Label ID="lbl_admno_header2" runat="server" Text="-"></asp:Label>
                        &nbsp;
                        &nbsp;
                        <b>DOB/Age :</b> <asp:Label ID="lbl_dob_header2" runat="server" Text="-"></asp:Label>
                        &nbsp;
                        &nbsp;
                        <b>Sex :</b> <asp:Label ID="lbl_sex_header2" runat="server" Text="-"></asp:Label>
                    </div>
                </div>
            </div>
        </div>
        <div style="background-color:white; margin:20px; border-radius:7px; border: solid 2px lightgrey;">
            <table class="table table-divider">
                <tr>
                    <td colspan="2">
                        <asp:Label ID="lbl_titlewithdate" runat="server" Text="-" style="font-weight:bold;"></asp:Label>
                    </td>
                </tr>
                <tr id="div_emergency1" runat="server" visible="false">
                    <td class="table-left-bg">
                        <label>Triage</label>
                    </td>
                    <td class="table-right-bg padding-grp-div-parent">
                         <div class="btn-group-justified">
                            <div class="btn-group padding-grp-div-child">
                                <asp:Label ID="lbl_skortriage" runat="server" Text="-"></asp:Label>
                                &nbsp;-&nbsp;
                                <asp:Label ID="lbl_traumatriage" runat="server" Text="-"></asp:Label>
                            </div>
                        </div>
                        <div class="btn-group-justified" style="border-top:1px solid lightgrey">
                            <div class="btn-group padding-grp-div-child">
                                <b>Patient's Arrival</b><br />
                                <asp:Label ID="lbl_pasiendatang" runat="server" Text="-"></asp:Label>
                            </div>
                            <div class="btn-group padding-grp-div-child border-left">
                                <b>General Condition</b><br />
                                <asp:Label ID="lbl_keadaanumum" runat="server" Text="-"></asp:Label>
                            </div>
                        </div>
                        <div class="btn-group-justified" style="border-top:1px solid lightgrey">
                            <div class="btn-group padding-grp-div-child">
                                <b>Airway</b><br />
                                <asp:Label ID="lbl_airway" runat="server" Text="-"></asp:Label>
                            </div>
                            <div class="btn-group padding-grp-div-child border-left">
                                <b>Breathing</b><br />
                                <asp:Label ID="lbl_breathing" runat="server" Text="-"></asp:Label>
                            </div>
                            <div class="btn-group padding-grp-div-child border-left">
                                <b>Circulation</b><br />
                                <asp:Label ID="lbl_circulation" runat="server" Text="-"></asp:Label>
                            </div>
                            <div class="btn-group padding-grp-div-child border-left">
                                <b>Disability</b><br />
                                <asp:Label ID="lbl_disability" runat="server" Text="-"></asp:Label>
                            </div>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td class="table-left-bg">
                        <label>Chief Complaint</label>
                    </td>
                    <td class="table-right-bg">
                        <div class="btn-group-justified">
                            <div class="btn-group padding-grp-div-child">
                                <asp:Label ID="lbl_chiefcomplaint" runat="server" Text="-"></asp:Label>
                            </div>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td class="table-left-bg">
                        <label>Anamnesis</label>
                    </td>
                    <td class="table-right-bg padding-grp-div-parent">
                        <div class="btn-group-justified">
                            <div class="btn-group padding-grp-div-child">
                                <asp:Label ID="lbl_anamnesis" runat="server" Text="-"></asp:Label>
                            </div>
                            <div class="btn-group padding-grp-div-child border-left">
                                <b>Hamil</b><br />
                                <asp:Label ID="lbl_hamil" runat="server" Text="-"></asp:Label>
                            </div>
                            <div class="btn-group padding-grp-div-child border-left">
                                <b>Menyusui</b><br />
                                <asp:Label ID="lbl_menyusui" runat="server" Text="-"></asp:Label>
                            </div>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td class="table-left-bg">
                        <label>Medication & Allergies</label>
                    </td>
                    <td class="table-right-bg padding-grp-div-parent">
                        <div class="btn-group-justified">
                            <div class="btn-group padding-grp-div-child">
                                <b>Routine Medication</b><br />
                                 <asp:Repeater ID="RepeaterRoutineMed" runat="server">
                                    <ItemTemplate>
                                        <li><asp:Label ID="LabelRoutineMedData" runat="server" Text='<%# Bind("value") %>'></asp:Label></li>
                                    </ItemTemplate>
                                </asp:Repeater>
                                <asp:Label ID="Labelroutinempty" runat="server" Text="-" Visible="false"></asp:Label>
                            </div>
                            <div class="btn-group padding-grp-div-child border-left">
                                <b>Drug Allergy</b><br />
                                <asp:Repeater ID="RepeaterDrugAllergy" runat="server">
                                    <ItemTemplate>
                                        <li><asp:Label ID="LabelDrugAllergyData" runat="server" Text='<%# Bind("value") %>'></asp:Label></li>
                                    </ItemTemplate>
                                </asp:Repeater>
                                <asp:Label ID="Labeldrugempty" runat="server" Text="-" Visible="false"></asp:Label>
                            </div>
                            <div class="btn-group padding-grp-div-child border-left">
                                <b>Food Allergy</b><br />
                                 <asp:Repeater ID="RepeaterFoodAllergy" runat="server">
                                    <ItemTemplate>
                                        <li><asp:Label ID="LabelFoodAllergyData" runat="server" Text='<%# Bind("value") %>'></asp:Label></li>
                                    </ItemTemplate>
                                </asp:Repeater>
                                <asp:Label ID="Labelfoodempty" runat="server" Text="-" Visible="false"></asp:Label>
                            </div>
                            <div class="btn-group padding-grp-div-child border-left">
                                <b>Other Allergy</b><br />
                                 <asp:Repeater ID="RepeaterOtherAllergy" runat="server">
                                    <ItemTemplate>
                                        <li><asp:Label ID="LabelOtherAllergyData" runat="server" Text='<%# Bind("value") %>'></asp:Label></li>
                                    </ItemTemplate>
                                </asp:Repeater>
                                <asp:Label ID="Labelotherempty" runat="server" Text="-" Visible="false"></asp:Label>
                            </div>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td class="table-left-bg">
                        <label>Illness History</label>
                    </td>
                    <td class="table-right-bg padding-grp-div-parent">
                        <div class="btn-group-justified">
                            <div class="btn-group padding-grp-div-child">
                                <b>Surgery History</b><br />
                                 <asp:Repeater ID="RepeaterSurgeryHistory" runat="server">
                                    <ItemTemplate>
                                        <li><asp:Label ID="LabelSurgeryHistoryData" runat="server" Text='<%# Bind("value") %>'></asp:Label></li>
                                    </ItemTemplate>
                                </asp:Repeater>
                                <asp:Label ID="LabelSurgeryHistoryEmpty" runat="server" Text="-" Visible="false"></asp:Label>
                            </div>
                            <div class="btn-group padding-grp-div-child border-left">
                                <b>Disease History</b><br />
                                <asp:Repeater ID="RepeaterDiseaseHistory" runat="server">
                                    <ItemTemplate>
                                        <li><asp:Label ID="LabelDiseaseHistoryData" runat="server" Text='<%# Bind("value") %>'></asp:Label></li>
                                    </ItemTemplate>
                                </asp:Repeater>
                                <asp:Label ID="LabelDiseaseHistoryEmpty" runat="server" Text="-" Visible="false"></asp:Label>
                            </div>
                            <div class="btn-group padding-grp-div-child border-left">
                                <b>Family Disease History</b><br />
                                 <asp:Repeater ID="RepeaterFamilyDisease" runat="server">
                                    <ItemTemplate>
                                        <li><asp:Label ID="LabelFamilyDiseaseData" runat="server" Text='<%# Bind("value") %>'></asp:Label></li>
                                    </ItemTemplate>
                                </asp:Repeater>
                                <asp:Label ID="LabelFamilyDiseaseEmpty" runat="server" Text="-" Visible="false"></asp:Label>
                            </div>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td class="table-left-bg">
                        <label>Endemic Area Visitation</label>
                    </td>
                    <td class="table-right-bg padding-grp-div-parent">
                        <div class="btn-group-justified">
                            <div class="btn-group padding-grp-div-child">
                                <b>Have Been to Endemic Area</b><br />
                                <asp:Label ID="lbl_endemicarea" runat="server" Text="-"></asp:Label>
                            </div>
                            <div class="btn-group padding-grp-div-child border-left">
                                <b>Screening Infectious Disease</b><br />
                                <asp:Repeater ID="RepeaterScreeningInfectius" runat="server">
                                    <ItemTemplate>
                                        <li><asp:Label ID="LabelScreeningInfectiusData" runat="server" Text='<%# Bind("remarks") %>'></asp:Label></li>
                                    </ItemTemplate>
                                </asp:Repeater>
                                <asp:Label ID="LabelScreeningInfectiusEmpty" runat="server" Text="-" Visible="false"></asp:Label>
                            </div>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td class="table-left-bg">
                        <label>Nutrition & Fasting</label>
                    </td>
                    <td class="table-right-bg padding-grp-div-parent">
                        <div class="btn-group-justified">
                            <div class="btn-group padding-grp-div-child">
                                <b>Nutrition Problem</b><br />
                                <asp:Label ID="lbl_nutritionproblem" runat="server" Text="-"></asp:Label>
                            </div>
                            <div class="btn-group padding-grp-div-child border-left">
                                <b>Fasting</b><br />
                                <asp:Label ID="lbl_fasting" runat="server" Text="-"></asp:Label>
                            </div>
                        </div>
                    </td>
                </tr>
                <tr id="div_pediatric1" runat="server" visible="false">
                    <td class="table-left-bg">
                        <label>Growth Histrory</label>
                    </td>
                    <td class="table-right-bg padding-grp-div-parent">
                        <div class="btn-group-justified" style="border-top:1px solid lightgrey">
                            <div class="btn-group padding-grp-div-child">
                                <b>Tengkurap</b><br />
                                <asp:Label ID="lbl_tengkurap" runat="server" Text="-"></asp:Label>
                            </div>
                            <div class="btn-group padding-grp-div-child border-left">
                                <b>Duduk</b><br />
                                <asp:Label ID="lbl_duduk" runat="server" Text="-"></asp:Label>
                            </div>
                            <div class="btn-group padding-grp-div-child border-left">
                                <b>Merangkak</b><br />
                                <asp:Label ID="lbl_merangkak" runat="server" Text="-"></asp:Label>
                            </div>
                            <div class="btn-group padding-grp-div-child border-left">
                                <b>Berdiri</b><br />
                                <asp:Label ID="lbl_berdiri" runat="server" Text="-"></asp:Label>
                            </div>
                            <div class="btn-group padding-grp-div-child border-left">
                                <b>Berjalan</b><br />
                                <asp:Label ID="lbl_berjalan" runat="server" Text="-"></asp:Label>
                            </div>
                            <div class="btn-group padding-grp-div-child border-left">
                                <b>Berbicara</b><br />
                                <asp:Label ID="lbl_berbicara" runat="server" Text="-"></asp:Label>
                            </div>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td class="table-left-bg">
                        <label>Physical Examination</label>
                    </td>
                    <td class="table-right-bg padding-grp-div-parent">
                        <div class="btn-group-justified">
                            <div class="btn-group padding-grp-div-child">
                                <b>Eye(Mata) </b><br />
                                <asp:Label ID="lbl_eye" runat="server" Text="-"></asp:Label>
                            </div>
                            <div class="btn-group padding-grp-div-child border-left">
                                <b>Move(Motorik) </b><br />
                                <asp:Label ID="lbl_move" runat="server" Text="-"></asp:Label>
                            </div>
                            <div class="btn-group padding-grp-div-child border-left">
                                <b>Verbal(Verbal) </b><br />
                                <asp:Label ID="lbl_verbal" runat="server" Text="-"></asp:Label>
                            </div>
                            <div class="btn-group padding-grp-div-child border-left">
                                <b>Score </b><br />
                                <asp:Label ID="lbl_score" runat="server" Text="-"></asp:Label>
                            </div>
                        </div>
                        <div class="btn-group-justified" style="border-top:1px solid lightgrey">
                             <div class="btn-group padding-grp-div-child">
                                <b>Pain Scale </b><br />
                                <asp:Label ID="lbl_painscale" runat="server" Text="-"></asp:Label>
                            </div>
                        </div>
                        <div class="btn-group-justified" style="border-top:1px solid lightgrey">
                             <div class="btn-group padding-grp-div-child">
                                <b>Blood Pressure</b><br />
                                <asp:Label ID="lbl_bloodpressure" runat="server" Text="-"></asp:Label>
                            </div>
                            <div class="btn-group padding-grp-div-child border-left">
                                <b>Pulse Rate</b><br />
                                <asp:Label ID="lbl_pulserate" runat="server" Text="-"></asp:Label>
                            </div>
                            <div class="btn-group padding-grp-div-child border-left">
                                <b>Respiratory Rate</b><br />
                                <asp:Label ID="lbl_respiratoryrate" runat="server" Text="-"></asp:Label>
                            </div>
                            <div class="btn-group padding-grp-div-child border-left">
                                <b>SpO2</b><br />
                                <asp:Label ID="lbl_spo2" runat="server" Text="-"></asp:Label>
                            </div>
                            <div class="btn-group padding-grp-div-child border-left">
                                <b>Temperature</b><br />
                                <asp:Label ID="lbl_temperature" runat="server" Text="-"></asp:Label>
                            </div>
                            <div class="btn-group padding-grp-div-child border-left">
                                <b>Weight</b><br />
                                <asp:Label ID="lbl_weight" runat="server" Text="-"></asp:Label>
                            </div>
                            <div class="btn-group padding-grp-div-child border-left">
                                <b>Height</b><br />
                                <asp:Label ID="lbl_height" runat="server" Text="-"></asp:Label>
                            </div>
                            <div class="btn-group padding-grp-div-child border-left">
                                <b>Head Circumference</b><br />
                                <asp:Label ID="lbl_headcirc" runat="server" Text="-"></asp:Label>
                            </div>
                        </div>
                        <div class="btn-group-justified" style="border-top:1px solid lightgrey">
                             <div class="btn-group padding-grp-div-child">
                                <b>Mental Status </b><br />
                                <asp:Label ID="lbl_mentalstatus" runat="server" Text="-"></asp:Label>
                            </div>
                             <div class="btn-group padding-grp-div-child border-left">
                                <b>Consciousness Level</b><br />
                                <asp:Label ID="lbl_conslevel" runat="server" Text="-"></asp:Label>
                            </div>
                        </div>
                        <div class="btn-group-justified" style="border-top:1px solid lightgrey">
                             <div class="btn-group padding-grp-div-child">
                                <asp:Label ID="lbl_soapothers" runat="server" Text="-"></asp:Label>
                            </div>
                        </div>

                        <div id="div_obgyn1" runat="server" class="btn-group-justified" style="border-top:1px solid lightgrey" visible="false">
                             <div class="btn-group padding-grp-div-child">
                                <b>GPA </b><br />
                                <asp:Label runat="server" ID="lbl_g" Text="-" /> &nbsp;&nbsp;
                                <asp:Label runat="server" ID="lbl_p" Text="-" /> &nbsp;&nbsp;
                                <asp:Label runat="server" ID="lbl_a" Text="-" /> &nbsp;&nbsp;
                            </div>
                            <div class="btn-group padding-grp-div-child border-left">
                                <b>HPHT</b><br />
                                <asp:Label ID="lbl_hpht" runat="server" Text="-"></asp:Label>
                            </div>
                            <div class="btn-group padding-grp-div-child border-left">
                                <b>THL</b><br />
                                <asp:Label ID="lbl_thl" runat="server" Text="-"></asp:Label>
                            </div>
                        </div>

                    </td>
                </tr>
                <tr>
                    <td class="table-left-bg">
                        <label>Diagnosis</label>
                    </td>
                    <td class="table-right-bg">
                        <div class="btn-group-justified">
                            <div class="btn-group padding-grp-div-child">
                                <asp:Label ID="lbl_diagnosis" runat="server" Text="-"></asp:Label>
                            </div>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td class="table-left-bg">
                        <label>Planning & Procedure</label>
                    </td>
                    <td class="table-right-bg">
                        <div class="btn-group-justified">
                            <div class="btn-group padding-grp-div-child">
                                <asp:Label ID="lbl_planproc" runat="server" Text="-"></asp:Label>
                            </div>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td class="table-left-bg">
                        <label>Procedure Notes</label>
                    </td>
                    <td class="table-right-bg">
                        <div class="btn-group-justified">
                            <div class="btn-group padding-grp-div-child">
                                <asp:Label ID="lbl_procnotes" runat="server" Text="-"></asp:Label>
                            </div>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td class="table-left-bg">
                        <label>Procedure Result</label>
                    </td>
                    <td class="table-right-bg">
                        <div class="btn-group-justified">
                            <div class="btn-group padding-grp-div-child">
                                <asp:Label ID="lbl_procresult" runat="server" Text="-"></asp:Label>
                            </div>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td class="table-left-bg">
                        <label>Lab & Rad</label>
                    </td>
                    <td class="table-right-bg padding-grp-div-parent">
                        <table style="width: 100%;">
                            <tr style="width: 100%;">
                                <td class="table-right-bg">
                                    <div class="btn-group-justified">
                                        <div class="btn-group padding-grp-div-child">                               
                                            <b>Laboratory</b><br />
                                             <asp:Repeater ID="RepeaterLab" runat="server">
                                                <ItemTemplate>
                                                    <li>
                                                        <asp:Label ID="lbl_labitem" runat="server" Text='<%# Bind("item_name") %>'></asp:Label>
                                                        <asp:Label ID="lbl_iscito" runat="server" Text='<%# Eval("is_cito").ToString().ToLower() == "true" ? "(CITO)" : "" %>'></asp:Label>
                                                    </li>
                                                </ItemTemplate>
                                            </asp:Repeater>
                                            <asp:Label ID="lbl_labitem_empty" runat="server" Text="-" Visible="false"></asp:Label>
                                            <br />
                                            <br />
                                            <b>Others Lab</b><br />
                                            <asp:Label ID="lbl_otherslabitem" runat="server" Text="-"></asp:Label>
                                        </div>
                                        <div class="btn-group padding-grp-div-child border-left">
                                            <b>Radiology</b><br />
                                             <asp:Repeater ID="RepeaterRad" runat="server">
                                                <ItemTemplate>
                                                    <li>
                                                        <asp:Label ID="lbl_raditem" runat="server" Text='<%# Bind("item_name") %>'></asp:Label>
                                                        <asp:Label ID="lbl_isleftright" runat="server" Text='<%# Eval("remarks").ToString() != "" ? "(" + Eval("remarks").ToString() + ")" : "" %>'></asp:Label>
                                                    </li>
                                                </ItemTemplate>
                                            </asp:Repeater>
                                            <asp:Label ID="lbl_raditem_empty" runat="server" Text="-" Visible="false"></asp:Label>
                                            <br />
                                            <br />
                                            <b>Others Rad</b><br />
                                            <asp:Label ID="lbl_othersraditem" runat="server" Text="-"></asp:Label>

                                        </div>
                                    </div>
                                </td>
                            </tr>
                            <tr style="width: 100%;">
                                <td class="table-right-bg">
                                    <div class="btn-group-justified">
                                        <div class="btn-group padding-grp-div-child">                               
                                            <b>Laboratory Future Order : </b>  <asp:Label ID="lblLab_FO_Date" runat="server" Font-Bold="true" Text="-"></asp:Label><br />
                                             <asp:Repeater ID="RepeaterLabFO" runat="server">
                                                <ItemTemplate>
                                                    <li>
                                                        <asp:Label ID="lbl_labitem" runat="server" Text='<%# Bind("item_name") %>'></asp:Label>
                                                        <asp:Label ID="lbl_iscito" runat="server" Text='<%# Eval("is_cito").ToString().ToLower() == "true" ? "(CITO)" : "" %>'></asp:Label>
                                                    </li>
                                                </ItemTemplate>
                                            </asp:Repeater>
                                            <asp:Label ID="lbl_labitem_emptyfo" runat="server" Text="-" Visible="false"></asp:Label>
                                            <br />
                                            <br />
                                            <b>Others Lab Future Order : </b> <asp:Label ID="lblLab_FO_Date_other" runat="server" Font-Bold="true" Text="-"></asp:Label><br />
                                            <asp:Label ID="lbl_otherslabitemfo" runat="server" Text="-"></asp:Label>
                                        </div>
                                        <div class="btn-group padding-grp-div-child border-left">
                                            <b>Radiology Future Order : </b> <asp:Label ID="lblRad_FO_Date" runat="server" Font-Bold="true" Text="-"></asp:Label><br />
                                             <asp:Repeater ID="RepeaterRadFO" runat="server">
                                                <ItemTemplate>
                                                    <li>
                                                        <asp:Label ID="lbl_raditem" runat="server" Text='<%# Bind("item_name") %>'></asp:Label>
                                                        <asp:Label ID="lbl_isleftright" runat="server" Text='<%# Eval("remarks").ToString() != "" ? "(" + Eval("remarks").ToString() + ")" : "" %>'></asp:Label>
                                                    </li>
                                                </ItemTemplate>
                                            </asp:Repeater>
                                            <asp:Label ID="lbl_raditem_emptyfo" runat="server" Text="-" Visible="false"></asp:Label>
                                            <br />
                                            <br />
                                            <b>Others Rad Future Order : </b> <asp:Label ID="lblRad_FO_Date_other" runat="server" Font-Bold="true" Text="-"></asp:Label><br />
                                            <asp:Label ID="lbl_othersraditemfo" runat="server" Text="-"></asp:Label>

                                        </div>
                                    </div>
                                </td>
                            </tr>
                       </table>
                    </td>
                </tr>
                <tr>
                    <td class="table-left-bg">
                        <label>Diagnostic & Procedure</label>
                    </td>
                    <td class="table-right-bg padding-grp-div-parent">
                        <table style="width: 100%;">
                            <tr style="width: 100%;">
                                <td class="table-right-bg">
                                    <div class="btn-group-justified">
                                        <div class="btn-group padding-grp-div-child">                               
                                            <b>Diagnostic</b><br />
                                             <asp:Repeater ID="RepeaterDiagnostic" runat="server">
                                                <ItemTemplate>
                                                    <li>
                                                        <asp:Label ID="lbl_diagnosticitem" runat="server" Text='<%# Bind("salesItemName") %>'></asp:Label>
                                                    </li>
                                                </ItemTemplate>
                                            </asp:Repeater>
                                            <asp:Label ID="lbl_diagnosticitem_empty" runat="server" Text="-" Visible="false"></asp:Label>
                                            <br />
                                            <br />
                                            <b>Others Diagnostic</b><br />
                                            <asp:Label ID="lbl_othersdiagnosticitem" runat="server" Text="-"></asp:Label>
                                        </div>
                                        <div class="btn-group padding-grp-div-child border-left">
                                            <b>Procedure</b><br />
                                             <asp:Repeater ID="RepeaterProcedure" runat="server">
                                                <ItemTemplate>
                                                    <li>
                                                        <asp:Label ID="lbl_procedureitem" runat="server" Text='<%# Bind("salesItemName") %>'></asp:Label>
                                                    </li>
                                                </ItemTemplate>
                                            </asp:Repeater>
                                            <asp:Label ID="lbl_procedureitem_empty" runat="server" Text="-" Visible="false"></asp:Label>
                                            <br />
                                            <br />
                                            <b>Others Procedure</b><br />
                                            <asp:Label ID="lbl_othersprocedureitem" runat="server" Text="-"></asp:Label>

                                        </div>
                                    </div>
                                </td>
                            </tr>
                            <tr style="width: 100%;">
                                <td class="table-right-bg">
                                    <div class="btn-group-justified">
                                        <div class="btn-group padding-grp-div-child">                               
                                            <b>Diagnostic Future Order : </b>  <asp:Label ID="lblDiagnostic_FO_Date" runat="server" Font-Bold="true" Text="-"></asp:Label><br />
                                             <asp:Repeater ID="RepeaterDiagnosticFO" runat="server">
                                                <ItemTemplate>
                                                    <li>
                                                        <asp:Label ID="lbl_diagnosticitem" runat="server" Text='<%# Bind("salesItemName") %>'></asp:Label>
                                                    </li>
                                                </ItemTemplate>
                                            </asp:Repeater>
                                            <asp:Label ID="lbl_diagnosticitem_emptyfo" runat="server" Text="-" Visible="false"></asp:Label>
                                            <br />
                                            <br />
                                            <b>Others Diagnostic Future Order : </b> <asp:Label ID="lblDiagnostic_FO_Date_other" runat="server" Font-Bold="true" Text="-"></asp:Label><br />
                                            <asp:Label ID="lbl_othersdiagnosticitemfo" runat="server" Text="-"></asp:Label>
                                        </div>
                                        <div class="btn-group padding-grp-div-child border-left">
                                            <b>Procedure Future Order : </b> <asp:Label ID="lblProcedure_FO_Date" runat="server" Font-Bold="true" Text="-"></asp:Label><br />
                                             <asp:Repeater ID="RepeaterProcedureFO" runat="server">
                                                <ItemTemplate>
                                                    <li>
                                                        <asp:Label ID="lbl_procedureitem" runat="server" Text='<%# Bind("salesItemName") %>'></asp:Label>
                                                    </li>
                                                </ItemTemplate>
                                            </asp:Repeater>
                                            <asp:Label ID="lbl_procedureitem_emptyfo" runat="server" Text="-" Visible="false"></asp:Label>
                                            <br />
                                            <br />
                                            <b>Others Procedure Future Order : </b> <asp:Label ID="lblProcedure_FO_Date_other" runat="server" Font-Bold="true" Text="-"></asp:Label><br />
                                            <asp:Label ID="lbl_othersprocedureitemfo" runat="server" Text="-"></asp:Label>

                                        </div>
                                    </div>
                                </td>
                            </tr>
                       </table>
                    </td>
                </tr>
                <tr>
                    <td class="table-left-bg">
                        <label>Prescription</label>
                    </td>
                    <td class="table-right-bg">
                        <div class="btn-group-justified" runat="server" id="div_drugs_doctor">
                            <div class="btn-group padding-grp-div-child">
                                <b>Drugs DOCTOR</b><br />
                                <uc:ViewDrugs runat="server" ID="ViewDrugsForm" />
                                <asp:Label ID="lbl_drugsnotes" runat="server" Text="-"></asp:Label>
                            </div>
                        </div>
                        <div class="btn-group-justified" runat="server" id="div_cons_doctor">
                             <div class="btn-group padding-grp-div-child">
                                <b>Consumables DOCTOR</b><br />
                                <uc:ViewCons runat="server" ID="ViewConsForm" />
                            </div>
                        </div>
                        <div class="btn-group-justified" runat="server" id="div_racikan_doctor">
                             <div class="btn-group padding-grp-div-child">
                                <b>Racikan DOCTOR</b><br />
                                <uc:ViewRacikan runat="server" ID="ViewRacikanForm" />
                            </div>
                        </div>
                        <div class="btn-group-justified" runat="server" id="div_drugs_pharmacy">
                            <div class="btn-group padding-grp-div-child">
                                <b>Drugs PHARMACY</b><br />
                                <uc:ViewDrugs runat="server" ID="ViewDrugsFormPharmacy" />
                                <asp:Label ID="lbl_drugsnotespharmacy" runat="server" Text="-"></asp:Label>
                            </div>
                        </div>
                        <div class="btn-group-justified" runat="server" id="div_cons_pharmacy">
                             <div class="btn-group padding-grp-div-child">
                                <b>Consumables PHARMACY</b><br />
                                <uc:ViewCons runat="server" ID="ViewConsFormPharmacy" />
                            </div>
                        </div>
                    </td>
                </tr>
            </table>
        </div>

        <div class="container-fluid">
        
        <br />
        
        <br />
        
        <br />
            </div>

        <%-- ################# --%>


        <script type="text/javascript">
            $(document).ready(function () {

                var prm = Sys.WebForms.PageRequestManager.getInstance();
                if (prm != null) {
                    prm.add_endRequest(function (sender, e) {
                        if (sender._postBackSettings.panelsToUpdate != null) {

                        }
                    });
                };
            });
        </script>

    </form>
</body>

</html>



