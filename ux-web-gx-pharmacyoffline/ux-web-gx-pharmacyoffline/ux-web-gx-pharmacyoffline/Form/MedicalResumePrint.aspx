<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MedicalResumePrint.aspx.cs" Inherits="Form_MedicalResumePrint" %>


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
                    <asp:Image ID="ImageCovid" runat="server" ImageUrl="~/Images/Icon/CV-stamp.svg" Visible="false" style="width:150px; height:75px; position: absolute; right: 15px; top: 35px;" />

                    <%-- =============================================== START DATE SECTION ==================================================== --%>
                    <table style="width: 100%;">
                        <tr>
                            <td style="width: 50%;"></td>
                            <td style="width: 25%;">
                                <label style="font-weight: bold; font-family: Arial, Helvetica, sans-serif; font-size: 14px;">Tanggal Dibuat (</label>
                                <label style="font-weight: bold; font-family: Arial, Helvetica, sans-serif; font-size: 14px;">Created Date</label>
                                <label style="font-weight: bold; font-family: Arial, Helvetica, sans-serif; font-size: 14px;">)</label>
                                <br />
                                <asp:Label ID="lblCreatedOrderDate" runat="server" Font-Bold="false" Font-Names="Helvetica" Font-Size="14px"></asp:Label>
                            </td>
                            <td style="width: 25%">
                                <label style="font-weight: bold; font-family: Arial, Helvetica, sans-serif; font-size: 14px;">Tanggal Diubah (</label>
                                <label style="font-weight: bold; font-family: Arial, Helvetica, sans-serif; font-size: 14px;">Modified Date</label>
                                <label style="font-weight: bold; font-family: Arial, Helvetica, sans-serif; font-size: 14px;">)</label>
                                <br />
                                <asp:Label ID="lblModifiedOrderDate" runat="server" Font-Bold="false" Font-Names="Helvetica" Font-Size="14px"></asp:Label>
                            </td>

                        </tr>
                    </table>
                    <%-- =============================================== END DATE SECTION ==================================================== --%>
                    <br />
                    <table style="width: 100%;">
                        <%-- =============================================== HEADER ==================================================== --%>
                        <%--                        <thead>
                            <tr>
                                <td colspan="2">            
                                    <div class="btn-group btn-group-justified" style="padding-bottom:12px" role="group" aria-label="...">
                                      <div class="btn-group" role="group" style="vertical-align:top">
                                        <asp:ImageButton  ImageUrl="~/Images/Icon/logo-SH.png" runat="server" Enabled="false" />
                                      </div>
                                  
                                      <div class="btn-group" role="group" style="text-align:left;padding-right:12px;padding-left:30px">
                                        <div><b>Resume Medis <asp:Label runat="server" ID="AdmissionType"></asp:Label></b></div>
                                          <table style="width:100%">
                                              <tr>
                                                  <td style="padding-right:20px;vertical-align:top">
                                                      <label style="font-size:14px">MR</label>
                                                  </td>
                                                  <td>
                                                      <label>: </label><asp:Label runat="server" ID="lblmrno" style="font-size:14px"></asp:Label>
                                                  </td>
                                               </tr>
                                              <tr>
                                                  <td style="padding-right:20px;vertical-align:top">
                                                      <label style="font-size:14px">Name</label>
                                                  </td>
                                                  <td>
                                                      <label>: <asp:Label runat="server" ID="lblnamepatient" style="font-size:14px"></asp:Label>
                                                  </td>
                                              </tr>
                                              <tr>
                                                  <td style="padding-right:20px;vertical-align:top">
                                                      <label style="font-size:14px">DOB/Age</label>
                                                  </td>
                                                  <td>
                                                      <label>: <asp:Label runat="server" ID="lbldobpatient" style="font-size:14px"></asp:Label>
                                                  </td>
                                              </tr>
                                              <tr>
                                                  <td style="padding-right:20px;vertical-align:top">
                                                      <label style="font-size:14px">Sex</label>
                                                  </td>
                                                  <td>
                                                      <label>: <asp:Label runat="server" ID="lblsexpatient" style="font-size:14px"></asp:Label>
                                                  </td>
                                              </tr>
                                              <tr>
                                                  <td style="padding-right:20px;vertical-align:top">
                                                      <label style="font-size:14px">Doctor</label>
                                                  </td>
                                                  <td>
                                                      <label>: <asp:Label runat="server" ID="lbldoctorprimary" style="font-size:14px"></asp:Label>
                                                  </td>
                                              </tr>
                                          </table>
                                      </div>
                                    </div>
                                </td>
                            </tr>
                        </thead>--%>
                        <%-- =============================================== END HEADER ==================================================== --%>


                        <%-- =============================================== START TITLE ==================================================== --%>

                        <label style="font-size: 14px; font-weight: bold">Resume Medis (</label>
                        <label style="font-size: 14px; font-weight: bold; font-style: italic">Medical Resume</label>
                        <label style="font-size: 14px; font-weight: bold">)</label>
                        <tbody>
                            <tr style="background-color: black; border: solid 1px #cdced9; width: 100%">
                                <td style="width: 25%; padding: 5px; border-right: solid 1px #cdced9">
                                    <label style="color: white; font-weight:bold;">Keterangan</label>
                                </td>
                                <td colspan="2" style="width: 75%; padding: 5px; ">
                                    <label style="color: white; font-weight:bold;">Deskripsi</label>
                                </td>
                            </tr>
                        </tbody>
                        <%-- =============================================== END TITLE ==================================================== --%>

                        <%-- ================================================= TRIAGE ==================================================== --%>
                        <tr style="border: solid 1px #cdced9; width: 100%" id="divemergency1" runat="server" visible="false">
                                <td style="background-color: #efefef; width: 25%;" class="itemtable-priview-title">
                                    <label style=" font-weight: bold; font-size: 14px">Triage</label><br />
                                    <label style=" font-style: italic; font-size: 14px">(Triage)</label>
                                </td>
                                <td colspan="2" style="border-right: solid 1px #cdced9; width: 75%;">
                                    <table style="width: 100%;">
                                        <tr>
                                            <td class="itemtable-priview" style="border-right: 0px;">
                                                <asp:Label runat="server" Style="font-size: 14px; font-weight: bold" ID="lblskortriage" Text="-" />
                                                &nbsp;-&nbsp; 
                                                <asp:Label runat="server" Style="font-size: 14px; font-weight: bold" ID="lbltraumatriage" Text="-" />
                                            </td>
                                        </tr>
                                    </table>

                                    <div style="border-top: solid 1px #cdced9">
                                        <table style="width: 100%;">
                                            <tr>
                                                <td class="itemtable-priview" style="width:50%;">
                                                    <label style="font-size: 14px; font-weight: bold">Patient's Arrival </label>
                                                    <br />
                                                    <label style="font-size: 14px; font-style: italic;">(Cara Pasien Datang) : </label>
                                                    <br />
                                                    <asp:Label runat="server" Style="font-size: 14px" ID="lblpasiendatang" Text="-" />
                                                </td>
                                                <td class="itemtable-priview" style="width:50%;">
                                                    <label style="font-size: 14px; font-weight: bold">General Condition </label>
                                                    <br />
                                                    <label style="font-size: 14px; font-style: italic;">(Keadaan Umum) : </label>
                                                    <br />
                                                    <asp:Label runat="server" Style="font-size: 14px" ID="lblkeadaanumum" Text="-" />
                                                </td>
                                            </tr>
                                        </table>
                                    </div>

                                    <div style="border-top: solid 1px #cdced9">
                                        <table style="width: 100%;">
                                            <tr>
                                                <td class="itemtable-priview" style="width:25%;">
                                                    <label style="font-size: 14px; font-weight: bold">Àirway </label>
                                                    <br />
                                                    <label style="font-size: 14px; font-style: italic;">(Saluran Udara) : </label>
                                                    <br />
                                                    <asp:Label runat="server" Style="font-size: 14px" ID="lblairway" Text="-" />
                                                </td>
                                                <td class="itemtable-priview" style="width:25%;">
                                                    <label style="font-size: 14px; font-weight: bold">Breathing </label>
                                                    <br />
                                                    <label style="font-size: 14px; font-style: italic;">(Perfpasan) : </label>
                                                    <br />
                                                    <asp:Label runat="server" Style="font-size: 14px" ID="lblbreathing" Text="-" />
                                                </td>
                                                <td class="itemtable-priview" style="width:25%;">
                                                    <label style="font-size: 14px; font-weight: bold">Circulation </label>
                                                    <br />
                                                    <label style="font-size: 14px; font-style: italic;">(Sirkulasi) : </label>
                                                    <br />
                                                    <asp:Label runat="server" Style="font-size: 14px" ID="lblcirculation" Text="-" />
                                                </td>
                                                <td class="itemtable-priview" style="width:25%;">
                                                    <label style="font-size: 14px; font-weight: bold">Disability </label>
                                                    <br />
                                                    <label style="font-size: 14px; font-style: italic;">(Disabilitas) : </label>
                                                    <br />
                                                    <asp:Label runat="server" Style="font-size: 14px" ID="lbldisability" Text="-" />
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </td>
                            </tr>

                        <%-- =============================================== END TRIAGE ================================================== --%>

                        <%-- =============================================== CHEIF COMPLAINT ==================================================== --%>
                        <tbody>
                            <tr style="border: solid 1px #cdced9; width: 100%">
                                <td style="width: 25%; background-color: #efefef;" class="itemtable-priview-title">
                                    <label style=" font-weight: bold; font-size: 14px;">Chief Complaint</label><br />
                                    <label style=" font-style: italic; font-size: 14px;">(Keluhan Utama)</label>
                                </td>
                                <td style="width: 50%;" class="itemtable-priview">
                                    <asp:Label runat="server" ID="lblChiefComplaint" Style="font-size: 14px; vertical-align: top">
                                    </asp:Label>
                                </td>
                                <td style="width: 25%;" class="itemtable-priview">
                                    <label style="font-size: 14px; font-weight: bold;">Pregnant </label>
                                    <label style="font-size: 14px; font-style: italic">(Hamil)</label>
                                    <asp:Label runat="server" ID="lblispregnant" Style="padding-left: 72px">: -</asp:Label><br />
                                    <label style="font-size: 14px; font-weight: bold;">Breast Feeding </label>
                                    <label style="font-size: 14px; font-style: italic">(Menyusui)</label>
                                    <asp:Label runat="server" ID="lblbreastfeed" Style="padding-left: 12px;">: -</asp:Label>
                                </td>
                            </tr>
                        </tbody>
                        <%-- =============================================== END CHEIF COMPLAINT ==================================================== --%>



                        <%-- =============================================== ANAMNESIS ==================================================== --%>
                        <tbody>
                            <tr style="border: solid 1px #cdced9; width: 100%;">
                                <td style="width: 25%; background-color: #efefef;" class="itemtable-priview-title">
                                    <label style=" font-weight: bold; font-size: 14px">Anamnesis</label><br />
                                    <label style=" font-style: italic; font-size: 14px">(Anamnesa)</label></td>
                                <td style="width: 75%;" colspan="2" class="itemtable-priview">
                                    <asp:Label runat="server" ID="Anamnesis" Style="font-size: 14px;"></asp:Label>
                                </td>
                            </tr>
                        </tbody>
                        <%-- =============================================== END ANAMNESIS ==================================================== --%>



                        <%-- =============================================== MEDICATION & ALLERGIES ==================================================== --%>
                        <tbody>
                            <tr style="border: solid 1px #cdced9; width: 100%">
                                <td style="width: 25%; background-color: #efefef;" class="itemtable-priview-title">
                                    <label style=" font-weight: bold; font-size: 14px">Medication & Allergies</label>
                                    <br />
                                    <label style=" font-style: italic; font-size: 14px">(Pengobatan & Alergi)</label>
                                </td>
                                <td colspan="2" style="width: 75%; border-right: solid 1px #cdced9;">
                                    <table style="width: 100%;">
                                        <tr style="width: 100%;">
                                            <td style="width: 25%;" class="itemtable-priview">
                                                <label style="font-size: 14px; font-weight: bold">Routine Medication</label><br />
                                                <label style="font-size: 14px; font-style: italic;">(Pengobatan Rutin)</label><br />
                                                <asp:Label runat="server" ID="lblnoroute" Font-Size="12px">-</asp:Label>
                                                <asp:Repeater runat="server" ID="rptRoutine">
                                                    <ItemTemplate>
                                                        <li>
                                                            <asp:Label ID="NameLabel" runat="server" Style="font-size: 14px" Text='<%#Eval("value") %>' Enabled="false" />
                                                        </li>
                                                    </ItemTemplate>
                                                </asp:Repeater>
                                            </td>
                                            <td style="width: 25%;" class="itemtable-priview">
                                                <label style="font-size: 14px; font-weight: bold">Drug Allergies</label><br />
                                                <label style="font-size: 14px; font-style: italic;">(Alergi Obat)</label><br />
                                                <asp:Label runat="server" ID="lblnodrugallergy" Font-Size="12px">-</asp:Label>
                                                <asp:Repeater runat="server" ID="rptDrugAllergies">
                                                    <ItemTemplate>
                                                        <li>
                                                            <asp:Label ID="NameLabel" runat="server" Style="font-size: 14px" Text='<%#Eval("value") %>' Enabled="false" />
                                                        </li>
                                                    </ItemTemplate>
                                                </asp:Repeater>
                                            </td>
                                            <td style="width: 25%;" class="itemtable-priview">
                                                <label style="font-size: 14px; font-weight: bold">Food Allergies</label><br />
                                                <label style="font-size: 14px; font-style: italic;">(Alergi Makanan)</label><br />
                                                <asp:Label runat="server" ID="lblnofoodallergy" Font-Size="12px">-</asp:Label>
                                                <asp:Repeater runat="server" ID="rptFoodAllergies">
                                                    <ItemTemplate>
                                                        <li>
                                                            <asp:Label ID="NameLabel" runat="server" Style="font-size: 14px" Text='<%#Eval("value") %>' Enabled="false" />
                                                        </li>
                                                    </ItemTemplate>
                                                </asp:Repeater>
                                            </td>
                                            <td style="width: 25%; border-right: 0px;" class="itemtable-priview">
                                                <label style="font-size: 14px; font-weight: bold">Other Allergies</label><br />
                                                <label style="font-size: 14px; font-style: italic;">(Alergi Lainnya)</label><br />
                                                <asp:Label runat="server" ID="lblnootherallergy" Font-Size="12px">-</asp:Label>
                                                <asp:Repeater runat="server" ID="rptOtherAllergies">
                                                    <ItemTemplate>
                                                        <li>
                                                            <asp:Label ID="NameLabel" runat="server" Style="font-size: 14px" Text='<%#Eval("value") %>' Enabled="false" />
                                                        </li>
                                                    </ItemTemplate>
                                                </asp:Repeater>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </tbody>
                        <%-- =============================================== END MEDICATION & ALLERGIES ==================================================== --%>



                        <%-- =============================================== ILLNESS HISTORY ==================================================== --%>
                        <tbody>
                            <tr style="border: solid 1px #cdced9; width: 100%">
                                <td style="width: 25%; background-color: #efefef;" class="itemtable-priview-title">
                                    <label style=" font-weight: bold; font-size: 14px">Illness History</label><br />
                                    <label style=" font-style: italic; font-size: 14px">(Riwayat Penyakit)</label></td>
                                <td colspan="2" style="width: 75%; border-right: solid 1px #cdced9;">
                                    <table style="width: 100%;">
                                        <tr style="width: 100%;">
                                            <td style="width: 35%;" class="itemtable-priview">
                                                <label style="font-size: 14px; font-weight: bold">Surgery History</label><br />
                                                <label style="font-size: 14px; font-style: italic;">(Riwayat Operasi)</label><br />
                                                <asp:Label runat="server" ID="lblnosurgery" Font-Size="12px">-</asp:Label>
                                                <asp:Repeater runat="server" ID="rptSurgeryHistory">
                                                    <ItemTemplate>
                                                        <li>
                                                            <asp:Label ID="NameLabel" runat="server" Style="font-size: 14px" Text='<%#Eval("value") %>' Enabled="false" />
                                                        </li>
                                                    </ItemTemplate>
                                                </asp:Repeater>
                                            </td>
                                            <td style="width: 35%;" class="itemtable-priview">
                                                <label style="font-size: 14px; font-weight: bold">Disease History</label><br />
                                                <label style="font-size: 14px; font-style: italic;">(Riwayat Penyakit)</label><br />
                                                <asp:Label runat="server" ID="lblnodisease" Font-Size="12px">-</asp:Label>
                                                <asp:Repeater runat="server" ID="rptDiseaseHistory">
                                                    <ItemTemplate>
                                                        <li>
                                                            <asp:Label ID="NameLabel" runat="server" Style="font-size: 14px" Text='<%#Eval("remarks") %>' Enabled="false" />
                                                        </li>
                                                    </ItemTemplate>
                                                </asp:Repeater>
                                            </td>
                                            <td style="width: 30%; border-right: 0px;" class="itemtable-priview">
                                                <label style="font-size: 14px; font-weight: bold">Family Disease History</label><br />
                                                <label style="font-size: 14px; font-style: italic;">(Riwayat Penyakit Keluarga)</label><br />
                                                <asp:Label runat="server" ID="lblnofamilydisease" Font-Size="12px">-</asp:Label>
                                                <asp:Repeater runat="server" ID="rptFamilyDisease">
                                                    <ItemTemplate>
                                                        <li>
                                                            <asp:Label ID="NameLabel" runat="server" Style="font-size: 14px" Text='<%#Eval("remarks") %>' Enabled="false" />
                                                        </li>
                                                    </ItemTemplate>
                                                </asp:Repeater>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </tbody>
                        <%-- =============================================== END ILLNESS HISTORY ==================================================== --%>



                        <%-- =============================================== GENERAL EXAMINATION ==================================================== --%>
                        <tbody>
                            <tr style="border: solid 1px #cdced9; width: 100%">
                                <td style="background-color: #efefef; width: 25%;" class="itemtable-priview-title">
                                    <label style=" font-weight: bold; font-size: 14px">General Examination</label><br />
                                    <label style=" font-style: italic; font-size: 14px">(Pemeriksaan Umum)</label></td>
                                <td colspan="2" style="border-right: solid 1px #cdced9; width: 75%;">
                                    <table style="width: 100%;">
                                        <tr style="width: 100%;">
                                            <td class="itemtable-priview">
                                                <label style="font-size: 14px; font-weight: bold">Eye </label>
                                                <label style="font-size: 14px; font-style: italic;">(Mata)</label>
                                                <label style="font-size: 14px;">:</label><br />
                                                <asp:Label runat="server" ID="eye" Style="font-size: 14px" Text="-" />
                                            </td>
                                            <td class="itemtable-priview">
                                                <label style="font-size: 14px; font-weight: bold">Move </label>
                                                <label style="font-size: 14px; font-style: italic;">(Motorik)</label>
                                                <label style="font-size: 14px;">:</label><br />
                                                <asp:Label runat="server" ID="move" Style="font-size: 14px" Text="-" />
                                            </td>
                                            <td class="itemtable-priview">
                                                <label style="font-size: 14px; font-weight: bold">Verbal </label>
                                                <label style="font-size: 14px; font-style: italic;">(Verbal)</label>
                                                <label style="font-size: 14px;">:</label><br />
                                                <asp:Label runat="server" ID="verbal" Style="font-size: 14px" Text="-" />
                                            </td>
                                            <td style="border-right: 0px;" class="itemtable-priview">
                                                <label style="font-size: 14px; font-weight: bold">Score </label>
                                                <label style="font-size: 14px; font-style: italic;">(Skor)</label>
                                                <label style="font-size: 14px;">:</label><br />
                                                <asp:Label runat="server" ID="painscore" Style="font-size: 14px" Text="-" />
                                            </td>
                                        </tr>
                                    </table>

                                    <div style="border-top: solid 1px #cdced9">
                                        <table style="width: 100%;">
                                            <tr>
                                                <td class="itemtable-priview" style="border-right: 0px;">
                                                    <label style="font-size: 14px; font-weight: bold">Pain Scale </label>
                                                    <label style="font-size: 14px; font-style: italic;">(Skala Nyeri)</label>
                                                    <label style="font-size: 14px;">:</label><br />
                                                    <asp:Label runat="server" Style="font-size: 14px" ID="lblpainscale" Text="-" />
                                                </td>
                                            </tr>
                                        </table>
                                    </div>

                                    <div style="border-top: solid 1px #cdced9">
                                        <table style="width: 100%;">
                                            <tr style="width: 100%;">
                                                <td class="itemtable-priview">
                                                    <label style="font-size: 14px; font-weight: bold">Blood Pressure </label>
                                                    <br />
                                                    <label style="font-size: 14px; font-style: italic;">(Tekanan Darah) : </label>
                                                    <br />
                                                    <asp:Label runat="server" Style="font-size: 14px" ID="bloodpreassure" Text="-" />
                                                </td>
                                                <td class="itemtable-priview">
                                                    <label style="font-size: 14px; font-weight: bold">Pulse Rate </label>
                                                    <br />
                                                    <label style="font-size: 14px; font-style: italic;">(Nadi) : </label>
                                                    <br />
                                                    <asp:Label runat="server" Style="font-size: 14px" ID="pulse" Text="-" />
                                                </td>
                                                <td class="itemtable-priview">
                                                    <label style="font-size: 14px; font-weight: bold">Respiratory Rate </label>
                                                    <br />
                                                    <label style="font-size: 14px; font-style: italic;">(Pernapasan) : </label>
                                                    <br />
                                                    <asp:Label runat="server" Style="font-size: 14px" ID="respiratory" Text="-" />
                                                </td>
                                                <td class="itemtable-priview">
                                                    <label style="font-size: 14px; font-weight: bold">SpO2 </label>
                                                    <br />
                                                    <label style="font-size: 14px; font-style: italic;">(SpO2) : </label>
                                                    <br />
                                                    <asp:Label runat="server" Style="font-size: 14px" ID="spo" Text="-" />
                                                </td>
                                                <td class="itemtable-priview">
                                                    <label style="font-size: 14px; font-weight: bold">Temperature </label>
                                                    <br />
                                                    <label style="font-size: 14px; font-style: italic;">(Suhu) : </label>
                                                    <br />
                                                    <asp:Label runat="server" Style="font-size: 14px" ID="temperature" Text="-" />
                                                </td>
                                                <td class="itemtable-priview">
                                                    <label style="font-size: 14px; font-weight: bold">Weight </label>
                                                    <br />
                                                    <label style="font-size: 14px; font-style: italic;">(Berat) : </label>
                                                    <br />
                                                    <asp:Label runat="server" Style="font-size: 14px" ID="weight" Text="-" />
                                                </td>
                                                <td class="itemtable-priview">
                                                    <label style="font-size: 14px; font-weight: bold">Height </label>
                                                    <br />
                                                    <label style="font-size: 14px; font-style: italic;">(Tinggi) : </label>
                                                    <br />
                                                    <asp:Label runat="server" Style="font-size: 14px" ID="height" Text="-" />
                                                </td>
                                                <td class="itemtable-priview" style="border-right: 0px;">
                                                    <label style="font-size: 14px; font-weight: bold">Head Circumference </label>
                                                    <br />
                                                    <label style="font-size: 14px; font-style: italic;">(Lingkar Kepala) :</label>
                                                    <br />
                                                    <asp:Label runat="server" Style="font-size: 14px" ID="headcircumference" Text="-" />
                                                </td>
                                            </tr>
                                        </table>
                                    </div>

                                    <div style="border-top: solid 1px #cdced9">
                                        <table style="width: 100%;">
                                            <tr style="width: 100%;">
                                                <td class="itemtable-priview">
                                                    <label style="font-size: 14px; font-weight: bold">Mental Status </label>
                                                    <br />
                                                    <label style="font-size: 14px; font-style: italic;">(Status Mental) : </label>
                                                    <br />
                                                    <asp:Label runat="server" Style="font-size: 14px" ID="lblmentalstatus" Text="-" />
                                                </td>
                                                <td class="itemtable-priview">
                                                    <label style="font-size: 14px; font-weight: bold">Consciousness </label>
                                                    <br />
                                                    <label style="font-size: 14px; font-style: italic;">(Kesadaran) : </label>
                                                    <br />
                                                    <asp:Label runat="server" Style="font-size: 14px" ID="lblconsciousness" Text="-" />
                                                </td>
                                                <td class="itemtable-priview" style="border-right: 0px;">
                                                    <label style="font-size: 14px; font-weight: bold">Fall Risk </label>
                                                    <br />
                                                    <label style="font-size: 14px; font-style: italic;">(Risiko Jatuh) : </label>
                                                    <br />
                                                    <asp:Label runat="server" ID="lblnofallrisk" Font-Size="12px">-</asp:Label>
                                                    <asp:Repeater runat="server" ID="rptfallrisk">
                                                        <ItemTemplate>
                                                            <li>
                                                                <asp:Label ID="NameLabel" runat="server" Style="font-size: 14px" Text='<%#Eval("remarks") %>' Enabled="false" />
                                                            </li>
                                                        </ItemTemplate>
                                                    </asp:Repeater>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>

                                    <div style="border-top: solid 1px #cdced9" id="divobgyn1" runat="server" visible="false">
                                        <table style="width: 100%;">
                                            <tr style="width: 100%;">
                                                <td class="itemtable-priview">
                                                    <label style="font-size: 14px; font-weight: bold">GPA </label>
                                                    <%--<br />
                                                    <label style="font-size: 14px; font-style: italic;">(GPA) : </label>--%>
                                                    <br />
                                                    <asp:Label runat="server" Style="font-size: 14px" ID="lbl_g" Text="-" /> &nbsp;&nbsp;
                                                    <asp:Label runat="server" Style="font-size: 14px" ID="lbl_p" Text="-" /> &nbsp;&nbsp;
                                                    <asp:Label runat="server" Style="font-size: 14px" ID="lbl_a" Text="-" /> &nbsp;&nbsp;
                                                </td>
                                                <td class="itemtable-priview">
                                                    <label style="font-size: 14px; font-weight: bold">HPHT </label>
                                                    <%--<br />
                                                    <label style="font-size: 14px; font-style: italic;">(HPHT) : </label>--%>
                                                    <br />
                                                    <asp:Label runat="server" Style="font-size: 14px" ID="lbl_hpht" Text="-" />
                                                </td>
                                                <td class="itemtable-priview" style="border-right: 0px;">
                                                    <label style="font-size: 14px; font-weight: bold">THL </label>
                                                    <%--<br />
                                                    <label style="font-size: 14px; font-style: italic;">(THL) : </label>--%>
                                                    <br />
                                                    <asp:Label runat="server" Style="font-size: 14px" ID="lbl_thl" Text="-" />
                                                   
                                                </td>
                                            </tr>
                                        </table>
                                    </div>

                                    

                                </td>
                            </tr>
                        </tbody>
                        <%-- =============================================== END GENERAL EXAMINATION ==================================================== --%>

                        <%-- =============================================== PEDIATRIC ==================================================== --%>
                        <tbody id="tbodypediatric" runat="server" visible="false">
                            <tr style="border: solid 1px #cdced9; width: 100%">
                                <td style="width: 25%; background-color: #efefef;" class="itemtable-priview-title">
                                    <label style=" font-weight: bold; font-size: 14px">Growth Histrory</label><br />
                                    <label style=" font-style: italic; font-size: 14px">(Riwayat Tumbuh Kembang)</label></td>
                                <td colspan="2" style="width: 75%;">
                                    <div id="divpediatric1" runat="server" visible="false">
                                        <table style="width: 100%;">
                                            <tr style="width: 100%;">
                                                <td class="itemtable-priview">
                                                    <label style="font-size: 14px; font-weight: bold">Tengkurap </label>
                                                    <br />
                                                    <asp:Label runat="server" Style="font-size: 14px" ID="lbl_tengkurap" Text="-" />
                                                </td>
                                                <td class="itemtable-priview">
                                                    <label style="font-size: 14px; font-weight: bold">Duduk </label>
                                                    <br />
                                                    <asp:Label runat="server" Style="font-size: 14px" ID="lbl_duduk" Text="-" />
                                                </td>
                                                <td class="itemtable-priview">
                                                    <label style="font-size: 14px; font-weight: bold">Merangkak </label>
                                                    <br />
                                                    <asp:Label runat="server" Style="font-size: 14px" ID="lbl_merangkak" Text="-" />
                                                </td>
                                                <td class="itemtable-priview">
                                                    <label style="font-size: 14px; font-weight: bold">Berdiri </label>
                                                    <br />
                                                    <asp:Label runat="server" Style="font-size: 14px" ID="lbl_berdiri" Text="-" />
                                                </td>
                                                <td class="itemtable-priview">
                                                    <label style="font-size: 14px; font-weight: bold">Berjalan </label>
                                                    <br />
                                                    <asp:Label runat="server" Style="font-size: 14px" ID="lbl_berjalan" Text="-" />
                                                </td>
                                                <td class="itemtable-priview" style="border-right:0px;">
                                                    <label style="font-size: 14px; font-weight: bold">Berbicara </label>
                                                    <br />
                                                    <asp:Label runat="server" Style="font-size: 14px" ID="lbl_berbicara" Text="-" />
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </td>
                            </tr>
                        </tbody>
                        <%-- =============================================== END PEDIATRIC ==================================================== --%>



                        <%-- =============================================== PHYSICAL EXAMINATION ==================================================== --%>
                        <tbody>
                            <tr style="border: solid 1px #cdced9; width: 100%">
                                <td style="width: 25%; background-color: #efefef;" class="itemtable-priview-title">
                                    <label style=" font-weight: bold; font-size: 14px">Physical Examination</label><br />
                                    <label style=" font-style: italic; font-size: 14px">(Pemeriksaan Fisik)</label></td>
                                <td colspan="2" class="itemtable-priview" style="width: 75%;">
                                    <asp:Label runat="server" Style="font-size: 14px" ID="lblphysicalexamination">-</asp:Label>
                                </td>
                            </tr>
                        </tbody>
                        <%-- =============================================== END PHYSICAL EXAMINATION ==================================================== --%>



                        <%-- =============================================== DIAGNOSIS ==================================================== --%>
                        <tbody>
                            <tr style="border: solid 1px #cdced9; width: 100%">
                                <td style="background-color: #efefef; width: 25%;" class="itemtable-priview-title">
                                    <label style=" font-weight: bold; font-size: 14px">Diagnosis</label><br />
                                    <label style=" font-style: italic; font-size: 14px">(Diagnosa)</label></td>
                                <td colspan="2" class="itemtable-priview" style="width: 75%;">
                                    <asp:Label runat="server" Style="font-size: 14px" ID="primarydiagnosis">-</asp:Label>
                                </td>
                            </tr>
                        </tbody>
                        <%-- =============================================== END DIAGNOSIS ==================================================== --%>



                        <%-- =============================================== PLANNING & PROCEDURE ==================================================== --%>
                        <tbody>
                            <tr style="border: solid 1px #cdced9; width: 100%">
                                <td style="background-color: #efefef; width: 25%;" class="itemtable-priview-title">
                                    <label style=" font-weight: bold; font-size: 14px">Planning & Procedure</label><br />
                                    <label style=" font-style: italic; font-size: 14px">(Tindakan di RS)</label></td>
                                <td colspan="2" class="itemtable-priview" style="width: 75%">
                                    <asp:Label runat="server" Style="font-size: 14px" ID="procedure">-</asp:Label>
                                </td>
                            </tr>
                        </tbody>
                        <%-- =============================================== END PLANNING & PROCEDURE ==================================================== --%>



                        <%-- =============================================== PROCEDURE RESULT ==================================================== --%>
                        <tbody>
                            <tr style="border: solid 1px #cdced9; width: 100%">
                                <td style="background-color: #efefef; width: 25%;" class="itemtable-priview-title">
                                    <label style=" font-weight: bold; font-size: 14px">Procedure Result</label><br />
                                    <label style=" font-style: italic; font-size: 14px">(Hasil Tindakan)</label></td>
                                <td colspan="2" style="width: 75%">
                                    <table style="width:100%;">
                                        <tr>
                                            <td class="itemtable-priview">
                                                <asp:Label runat="server" Style="font-size: 14px" ID="procedureResult">-</asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                    
                                    <div style="border-top: solid 1px #cdced9" id="divdocremark" runat="server">
                                    </div>
                                </td>
                            </tr>
                        </tbody>
                        <%-- =============================================== END PROCEDURE RESULT ==================================================== --%>

                        
                        <%-- =============================================== LAB RAD ==================================================== --%>
                        <tbody>
                            <tr style="border: solid 1px #cdced9; width: 100%">
                                <td style="width: 25%; background-color: #efefef;" class="itemtable-priview-title">
                                    <label style=" font-weight: bold; font-size: 14px">Lab & Rad</label><br />
                                    <label style=" font-style: italic; font-size: 14px">(Lab & Rad)</label></td>
                                <td colspan="2" style="width: 75%; border-right: solid 1px #cdced9;">
                                    <table style="width: 100%;">
                                        <tr style="width: 100%;">
                                            <td style="width: 50%;" class="itemtable-priview">
                                                <label style="font-size: 14px; font-weight: bold">Laboratory</label><br />
                                                <label style="font-size: 14px; font-style: italic;">(Laboratorium)</label><br />
                                                <asp:Label runat="server" ID="lblnolab" Font-Size="12px">-</asp:Label>
                                                <asp:Repeater runat="server" ID="rptLabHistory">
                                                    <ItemTemplate>
                                                        <li>
                                                            <asp:Label ID="lbl_item" runat="server" Style="font-size: 14px" Text='<%#Eval("item_name") %>' Enabled="false" />
                                                            <asp:Label ID="lbl_iscito" runat="server" Style="font-size: 14px" Text='<%#Eval("is_cito").ToString().ToLower() == "true" ? "(CITO)" : "" %>' Enabled="false" />
                                                        </li>
                                                    </ItemTemplate>
                                                </asp:Repeater>

                                                <br />
                                                <br />
                                                <div>
                                                    <label style="font-size: 14px; font-weight: bold">Others Lab</label><br />
                                                    <label style="font-size: 14px; font-style: italic;">(Laboratorium Lain-lain)</label><br />
                                                    <asp:Label runat="server" ID="Lbl_OtherLab" Font-Size="12px">-</asp:Label>
                                                </div>
                                            </td>
                                            <td style="width: 50%;" class="itemtable-priview">
                                                <label style="font-size: 14px; font-weight: bold">Radiology</label><br />
                                                <label style="font-size: 14px; font-style: italic;">(Radiologi)</label><br />
                                                <asp:Label runat="server" ID="lblnorad" Font-Size="12px">-</asp:Label>
                                                <asp:Repeater runat="server" ID="rptRadHistory">
                                                    <ItemTemplate>
                                                        <li>
                                                            <asp:Label ID="lbl_item" runat="server" Style="font-size: 14px" Text='<%#Eval("item_name") %>' Enabled="false" />
                                                            <asp:Label ID="lbl_leftright" runat="server" Style="font-size: 14px" Text='<%#Eval("remarks").ToString() != "" ? "(" + Eval("remarks").ToString() + ")" : "" %>' Enabled="false" />
                                                        </li>
                                                    </ItemTemplate>
                                                </asp:Repeater>

                                                <br />
                                                <br />
                                                <div>
                                                    <label style="font-size: 14px; font-weight: bold">Others Rad</label><br />
                                                    <label style="font-size: 14px; font-style: italic;">(Radiologi Lain-lain)</label><br />
                                                    <asp:Label runat="server" ID="Lbl_OtherRad" Font-Size="12px">-</asp:Label>
                                                </div>
                                            </td>
                                            
                                        </tr>
                                        <tr style="width: 100%;">
                                            <td style="width: 50%; border-top: solid 1px #cdced9;" class="itemtable-priview">
                                                <label style="font-size: 14px; font-weight: bold">Laboratory Future Order : <asp:Label ID="lblLab_FO_Date" runat="server" Text="-"></asp:Label></label><br />   
                                                <label style="font-size: 14px; font-style: italic;">(Laboratorium Mendatang : <asp:Label ID="lblLab_FO_Date2" runat="server" Text="-"></asp:Label>)</label><br />
                                                <asp:Label runat="server" ID="lblnolabFO" Font-Size="12px">-</asp:Label>
                                                <asp:Repeater runat="server" ID="rptLabHistoryFO">
                                                    <ItemTemplate>
                                                        <li>
                                                            <asp:Label ID="lbl_item" runat="server" Style="font-size: 14px" Text='<%#Eval("item_name") %>' Enabled="false" />
                                                            <asp:Label ID="lbl_iscito" runat="server" Style="font-size: 14px" Text='<%#Eval("is_cito").ToString().ToLower() == "true" ? "(CITO)" : "" %>' Enabled="false" />
                                                        </li>
                                                    </ItemTemplate>
                                                </asp:Repeater>

                                                <br />
                                                <br />
                                                <div>
                                                    <label style="font-size: 14px; font-weight: bold">Others Lab Future Order : <asp:Label ID="lblLab_FO_Date_other" runat="server" Text="-"></asp:Label></label><br />
                                                    <label style="font-size: 14px; font-style: italic;">(Laboratorium Lain-lain Mendatang : <asp:Label ID="lblLab_FO_Date2_other" runat="server" Text="-"></asp:Label>)</label><br />
                                                    <asp:Label runat="server" ID="Lbl_OtherLab_FO" Font-Size="12px">-</asp:Label>
                                                </div>
                                            </td>
                                            <td style="width: 50%; border-top: solid 1px #cdced9;" class="itemtable-priview">
                                                <label style="font-size: 14px; font-weight: bold">Radiology Future Order : <asp:Label ID="lblRad_FO_Date" runat="server" Text="-"></asp:Label></label><br />
                                                <label style="font-size: 14px; font-style: italic;">(Radiologi Mendatang : <asp:Label ID="lblRad_FO_Date2" runat="server" Text="-"></asp:Label>)</label><br />
                                                
                                                <asp:Label runat="server" ID="lblnoradFO" Font-Size="12px">-</asp:Label>
                                                <asp:Repeater runat="server" ID="rptRadHistoryFO">
                                                    <ItemTemplate>
                                                        <li>
                                                            <asp:Label ID="lbl_item" runat="server" Style="font-size: 14px" Text='<%#Eval("item_name") %>' Enabled="false" />
                                                            <asp:Label ID="lbl_leftright" runat="server" Style="font-size: 14px" Text='<%#Eval("remarks").ToString() != "" ? "(" + Eval("remarks").ToString() + ")" : "" %>' Enabled="false" />
                                                        </li>
                                                    </ItemTemplate>
                                                </asp:Repeater>

                                                <br />
                                                <br />
                                                <div>
                                                    <label style="font-size: 14px; font-weight: bold">Others Rad Future Order : <asp:Label ID="lblRad_FO_Date_other" runat="server" Text="-"></asp:Label></label><br />
                                                    <label style="font-size: 14px; font-style: italic;">(Radiologi Lain-lain Mendatang : <asp:Label ID="lblRad_FO_Date2_other" runat="server" Text="-"></asp:Label>)</label><br />
                                                    <asp:Label runat="server" ID="Lbl_OtherRad_FO" Font-Size="12px">-</asp:Label>
                                                </div>
                                            </td>
                                            
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </tbody>
                        <%-- =============================================== END ILLNESS HISTORY ==================================================== --%>

                        <%-- =============================================== PROCEDURE DIAGNOSTIC ==================================================== --%>
                        <tbody>
                            <tr style="border: solid 1px #cdced9; width: 100%">
                                <td style="width: 25%; background-color: #efefef;" class="itemtable-priview-title">
                                    <label style=" font-weight: bold; font-size: 14px">Diagnostic & Procedure</label><br />
                                    <label style=" font-style: italic; font-size: 14px">(Diagnostic & Procedure)</label></td>
                                <td colspan="2" style="width: 75%; border-right: solid 1px #cdced9;">
                                    <table style="width: 100%;">
                                        <tr style="width: 100%;">
                                            <td style="width: 50%;" class="itemtable-priview">
                                                <label style="font-size: 14px; font-weight: bold">Diagnostic</label><br />
                                                <label style="font-size: 14px; font-style: italic;">(Diagnostic)</label><br />
                                                <asp:Label runat="server" ID="lblnodiagnostic" Font-Size="12px">-</asp:Label>
                                                <asp:Repeater runat="server" ID="rptDiagnostic">
                                                    <ItemTemplate>
                                                        <li>
                                                            <asp:Label ID="lbl_item_diagnostic" runat="server" Style="font-size: 14px" Text='<%#Eval("SalesItemName") %>' Enabled="false" />
                                                        </li>
                                                    </ItemTemplate>
                                                </asp:Repeater>

                                                <br />
                                                <br />
                                                <div>
                                                    <label style="font-size: 14px; font-weight: bold">Others Diagnostic</label><br />
                                                    <label style="font-size: 14px; font-style: italic;">(Diagnostic Lain-lain)</label><br />
                                                    <asp:Label runat="server" ID="Lbl_OtherDiagnostic" Font-Size="12px">-</asp:Label>
                                                </div>
                                            </td>
                                            <td style="width: 50%;" class="itemtable-priview">
                                                <label style="font-size: 14px; font-weight: bold">Procedure</label><br />
                                                <label style="font-size: 14px; font-style: italic;">(Procedure)</label><br />
                                                <asp:Label runat="server" ID="lblnoprocedure" Font-Size="12px">-</asp:Label>
                                                <asp:Repeater runat="server" ID="rptProcedure">
                                                    <ItemTemplate>
                                                        <li>
                                                            <asp:Label ID="lbl_item_procedure" runat="server" Style="font-size: 14px" Text='<%#Eval("SalesItemName") %>' Enabled="false" />
                                                        </li>
                                                    </ItemTemplate>
                                                </asp:Repeater>

                                                <br />
                                                <br />
                                                <div>
                                                    <label style="font-size: 14px; font-weight: bold">Others Procedure</label><br />
                                                    <label style="font-size: 14px; font-style: italic;">(Procedure Lain-lain)</label><br />
                                                    <asp:Label runat="server" ID="Lbl_OtherProcedure" Font-Size="12px">-</asp:Label>
                                                </div>
                                            </td>
                                        </tr>
                                        <tr style="width: 100%;">
                                            <td style="width: 50%; border-top: solid 1px #cdced9;" class="itemtable-priview">
                                                <label style="font-size: 14px; font-weight: bold">Diagnostic Future Order : <asp:Label ID="lblDiagnostic_FO_Date" runat="server" Text="-"></asp:Label></label><br />   
                                                <label style="font-size: 14px; font-style: italic;">(Diagnostic Mendatang : <asp:Label ID="lblDiagnostic_FO_Date2" runat="server" Text="-"></asp:Label>)</label><br />
                                                <asp:Label runat="server" ID="lblnodiagnosticFO" Font-Size="12px">-</asp:Label>
                                                <asp:Repeater runat="server" ID="rptDiagnosticFO">
                                                    <ItemTemplate>
                                                        <li>
                                                            <asp:Label ID="lbl_item_diagnostic_fo" runat="server" Style="font-size: 14px" Text='<%#Eval("SalesItemName") %>' Enabled="false" />
                                                        </li>
                                                    </ItemTemplate>
                                                </asp:Repeater>

                                                <br />
                                                <br />
                                                <div>
                                                    <label style="font-size: 14px; font-weight: bold">Others Diagnostic Future Order : <asp:Label ID="lblDiagnostic_FO_Date_other" runat="server" Text="-"></asp:Label></label><br />
                                                    <label style="font-size: 14px; font-style: italic;">(Diagnostic Lain-lain Mendatang : <asp:Label ID="lblDiagnostic_FO_Date2_other" runat="server" Text="-"></asp:Label>)</label><br />
                                                    <asp:Label runat="server" ID="Lbl_OtherDiagnostic_FO" Font-Size="12px">-</asp:Label>
                                                </div>
                                            </td>
                                            <td style="width: 50%; border-top: solid 1px #cdced9;" class="itemtable-priview">
                                                <label style="font-size: 14px; font-weight: bold">Procedure Future Order : <asp:Label ID="lblProcedure_FO_Date" runat="server" Text="-"></asp:Label></label><br />
                                                <label style="font-size: 14px; font-style: italic;">(Procedure Mendatang : <asp:Label ID="lblProcedure_FO_Date2" runat="server" Text="-"></asp:Label>)</label><br />
                                                <asp:Label runat="server" ID="lblnoprocedureFO" Font-Size="12px">-</asp:Label>
                                                <asp:Repeater runat="server" ID="rptProcedureFO">
                                                    <ItemTemplate>
                                                        <li>
                                                            <asp:Label ID="lbl_item" runat="server" Style="font-size: 14px" Text='<%#Eval("SalesItemName") %>' Enabled="false" />
                                                        </li>
                                                    </ItemTemplate>
                                                </asp:Repeater>

                                                <br />
                                                <br />
                                                <div>
                                                    <label style="font-size: 14px; font-weight: bold">Others Procedure Future Order : <asp:Label ID="lblProcedure_FO_Date_other" runat="server" Text="-"></asp:Label></label><br />
                                                    <label style="font-size: 14px; font-style: italic;">(Procedure Lain-lain Mendatang : <asp:Label ID="lblProcedure_FO_Date2_other" runat="server" Text="-"></asp:Label>)</label><br />
                                                    <asp:Label runat="server" ID="Lbl_OtherProcedure_FO" Font-Size="12px">-</asp:Label>
                                                </div>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </tbody>
                        <%-- =============================================== END PROCEDURE DIAGNOSTIC ==================================================== --%>

                        <%-- =============================================== PRESCRIPTION ==================================================== --%>
                        <tbody>
                            <tr style="border: solid 1px #cdced9; width: 100%">
                                <td style="background-color: #efefef; width: 25%;" class="itemtable-priview-title">
                                    <label style=" font-weight: bold; font-size: 14px">Prescription</label><br />
                                    <label style=" font-style: italic; font-size: 14px">(Resep)</label>
                                </td>
                                <td colspan="2" class="itemtable-priview" style="width: 75%;">
                                    <label style="font-size: 14px; font-weight: bold;">Drugs </label>
                                    <label style="font-size: 14px; font-style: italic;">(Obat)</label><br />
                                    <asp:Label runat="server" ID="drugs" Font-Size="13px" Text="-" />
                                    <div style="padding-bottom: 5px" runat="server" id="divdrugsprescription">
                                        <asp:GridView ID="prescriptiondrugs" runat="server" AutoGenerateColumns="False" CssClass="table table-striped table-bordered table-condensed"
                                            EmptyDataText="" Font-Names="Helvetica" RowStyle-Width="100%" HeaderStyle-Width="100%" ShowHeader="true">
                                            <PagerStyle CssClass="pagination-ys" />
                                            <Columns>
                                                <asp:TemplateField HeaderText="Drug" HeaderStyle-Width="14%" ItemStyle-Width="14%" ItemStyle-HorizontalAlign="left" HeaderStyle-Font-Size="11px" HeaderStyle-CssClass="wordbreak contentTable">
                                                    <ItemTemplate>
                                                        <asp:Label ID="NameLabel" runat="server" Text='<%#Eval("salesItemName") %>' Enabled="false" Style="word-break: break-word" CssClass="contentTable" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Qty" HeaderStyle-Width="5%" ItemStyle-Width="5%" ItemStyle-HorizontalAlign="Center" HeaderStyle-Font-Size="11px" HeaderStyle-CssClass="wordbreak">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label1" Style="word-break: break-word" runat="server" Enabled="false" CssClass="contentTable"><%#Eval("quantity","{0:G29}") %></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="UOM" HeaderStyle-Width="6%" ItemStyle-Width="6%" ItemStyle-HorizontalAlign="Center" HeaderStyle-Font-Size="11px" HeaderStyle-CssClass="wordbreak">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label2" Style="word-break: break-word" runat="server" Enabled="false" CssClass="contentTable"><%#Eval("uom") %></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Frequency" HeaderStyle-Width="10%" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Center" HeaderStyle-Font-Size="11px" HeaderStyle-CssClass="wordbreak">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label3" Style="word-break: break-word" runat="server" Enabled="false" CssClass="contentTable"><%#Eval("frequency") %></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Dose" HeaderStyle-Width="6%" ItemStyle-Width="6%" ItemStyle-HorizontalAlign="Center" HeaderStyle-Font-Size="11px" HeaderStyle-CssClass="wordbreak">
                                                    <ItemTemplate>
                                                        <div runat="server" Visible='<%# Eval("IsDoseText").ToString() == "False" %>'>
                                                            <asp:Label ID="Label8" Style="word-break: break-word" runat="server" Enabled="false" CssClass="contentTable"><%#Eval("dose","{0:G29}") %></asp:Label>
                                                            &nbsp;
                                                            <asp:Label ID="Label9" Style="word-break: break-word" runat="server" Enabled="false" CssClass="contentTable"><%#Eval("dose_uom") %></asp:Label>
                                                        </div>
                                                        <asp:Label ID="lblDoseText" Style="word-break: break-word" runat="server" Enabled="false" CssClass="contentTable"  Visible='<%# Eval("IsDoseText") %>' Text='<%# Bind("doseText") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <%--<asp:TemplateField HeaderText="Dose UOM" HeaderStyle-Width="11%" ItemStyle-Width="11%" ItemStyle-HorizontalAlign="Center" HeaderStyle-Font-Size="11px" HeaderStyle-CssClass="wordbreak">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label9" Style="word-break: break-word" runat="server" Enabled="false" CssClass="contentTable"><%#Eval("dose_uom") %></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>--%>
                                                <asp:TemplateField HeaderText="Instruction" HeaderStyle-Width="14%" ItemStyle-Width="14%" ItemStyle-HorizontalAlign="center" HeaderStyle-Font-Size="11px" HeaderStyle-CssClass="wordbreak">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label4" Style="word-break: break-word" runat="server" Enabled="false" CssClass="contentTable"><%#Eval("instruction") %></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Route" HeaderStyle-Width="7%" ItemStyle-Width="7%" ItemStyle-HorizontalAlign="Center" HeaderStyle-Font-Size="11px" HeaderStyle-CssClass="wordbreak">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label5" Style="word-break: break-word" runat="server" Enabled="false" CssClass="contentTable"><%#Eval("route") %></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Iter" HeaderStyle-Width="5%" ItemStyle-Width="5%" ItemStyle-HorizontalAlign="Center" HeaderStyle-Font-Size="11px" HeaderStyle-CssClass="wordbreak">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label6" Style="word-break: break-word" runat="server" Enabled="false" CssClass="contentTable"><%#Eval("iteration") %></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Routine" HeaderStyle-Width="5%" ItemStyle-Width="5%" ItemStyle-HorizontalAlign="Center" HeaderStyle-Font-Size="11px" HeaderStyle-CssClass="wordbreak">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label7" Style="word-break: break-word" runat="server" Enabled="false" CssClass="contentTable"><%#Eval("routine") %></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                    <br />
                                    <label style="font-size: 14px; font-weight: bold;">Doctor Prescription Notes </label>
                                    <label style="font-size: 14px; font-style: italic;">(Catatan Resep Dokter)</label><br />
                                    <asp:Label runat="server" ID="drugsNoteByDoctor" Font-Size="13px" Text="-" />
                                    </div>
                                </td>
                            </tr>
                        </tbody>
                        <%-- =============================================== END PRESCRIPTION ==================================================== --%>

                        <%-- =============================================== RACIKAN ==================================================== --%>
                        <tr style="border: solid 1px #cdced9; width: 100%;">
                            <td style="background-color: #efefef; width: 25%;" class="itemtable-priview-title">
                                <label style="padding-left: 13px; font-weight: bold; font-size: 13px"></label>
                            </td>
                            <td colspan="2" style="width: 75%;" class="itemtable-priview">
                                <label style="font-size: 14px; font-weight: bold;">Compounds </label>
                                <label style="font-size: 14px; font-style: italic;">(Racikan)</label><br />

                                <asp:Label runat="server" ID="noracikan" Font-Size="13px" Text="-" />
                                <div style="padding-bottom: 5px" runat="server" id="divRacikan">

                                    <table style="width: 100%; display:none;" class="table table-striped table-bordered table-condensed">
                                        <tr class="bordertable" style="vertical-align: top; width: 100%;">
                                            <td style="width: 30%; padding-left: 13px" class="wordbreak">
                                                <label style="font-size: 11px; font-weight: bold">Name</label>
                                            </td>
                                            <td style="width: 15%" class="wordbreak">
                                                <label style="font-size: 11px; font-weight: bold">Dose</label>
                                                
                                            </td>
                                           <%-- <td style="width: 15%" class="wordbreak">
                                                <label style="font-size: 11px; font-weight: bold">Dose UoM</label>
                                            </td>--%>
                                            <td style="width: 10%" class="wordbreak">
                                                <label style="font-size: 11px; font-weight: bold">Frequency</label>
                                            </td>
                                            <td style="width: 10%" class="wordbreak">
                                                <label style="font-size: 11px; font-weight: bold">Route</label>
                                            </td>
                                            <td style="width: 20%" class="wordbreak">
                                                <label style="font-size: 11px; font-weight: bold">Instruction</label>
                                            </td>
                                            <td style="width: 5%" class="wordbreak">
                                                <label style="font-size: 11px; font-weight: bold">Qty</label>
                                            </td>
                                            <td style="width: 5%" class="wordbreak">
                                                <label style="font-size: 11px; font-weight: bold">U.O.M</label>
                                            </td>
                                            <td style="width: 5%" class="wordbreak">
                                                <label style="font-size: 11px; font-weight: bold">Iter</label>
                                            </td>
                                        </tr>

                                        <asp:Repeater runat="server" ID="RepeaterRacikan">
                                            <ItemTemplate>
                                                <tr class="bordertable" style="vertical-align: top; width: 100%">
                                                    <td style="width: 30%; padding-left: 13px">
                                                        <asp:Label ID="racikanname" Style="font-size: 13px" Width="90%" runat="server" Text='<%#Eval("compound_name") %>' Enabled="false" />
                                                    </td>
                                                    <td style="width: 15%;" class="wordbreak">
                                                       <%-- <asp:Label ID="racikandose" Style="font-size: 13px" Width="90%" runat="server" Enabled="false"><%#Eval("dose","{0:G29}") %></asp:Label>--%>
                                                        <div runat="server" Visible='<%# Eval("IsDoseText").ToString() == "False" %>'>
                                                            <asp:Label ID="Label8" Style="word-break: break-word" runat="server" Enabled="false" CssClass="contentTable"><%#Eval("dose","{0:G29}") %></asp:Label>
                                                            &nbsp;
                                                            <asp:Label ID="Label9" Style="word-break: break-word" runat="server" Enabled="false" CssClass="contentTable"><%#Eval("dose_uom") %></asp:Label>
                                                        </div>
                                                        <asp:Label ID="lblDoseText" Style="word-break: break-word" runat="server" Enabled="false" CssClass="contentTable"  Visible='<%# Eval("IsDoseText") %>' Text='<%# Bind("dose_text") %>'></asp:Label>
                                                    </td>
                                                   <%-- <td style="width: 15%;" class="wordbreak">
                                                        <asp:Label ID="racikandoseuom" Style="font-size: 13px" Width="90%" runat="server" Enabled="false"><%#Eval("dose_uom") %></asp:Label>
                                                    </td>--%>
                                                    <td style="width: 10%;" class="wordbreak">
                                                        <asp:Label ID="racikanfrequency" Style="font-size: 13px" Width="90%" runat="server" Enabled="false"><%#Eval("frequency_code") %></asp:Label>
                                                    </td>
                                                    <td style="width: 10%;" class="wordbreak">
                                                        <asp:Label ID="racikanroute" Style="font-size: 13px" Width="90%" runat="server" Enabled="false"><%#Eval("administration_route_code") %></asp:Label>
                                                    </td>
                                                    <td style="width: 20%;" class="wordbreak">
                                                        <asp:Label ID="racikaninstruction" Style="font-size: 13px" Width="90%" runat="server" Enabled="false"><%#Eval("administration_instruction") %></asp:Label>
                                                    </td>
                                                    <td style="width: 5%;" class="wordbreak">
                                                        <asp:Label ID="racikanqty" Style="font-size: 13px" Width="90%" runat="server" Enabled="false"><%#Eval("quantity","{0:G29}") %></asp:Label>
                                                    </td>
                                                    <td style="width: 5%;" class="wordbreak">
                                                        <asp:Label ID="racikanuom" Style="font-size: 13px" Width="90%" runat="server" Enabled="false"><%#Eval("uom_code") %></asp:Label>
                                                    </td>
                                                    <td style="width: 5%;" class="wordbreak">
                                                        <asp:Label ID="racikaniter" Style="font-size: 13px" Width="90%" runat="server" Enabled="false"><%#Eval("iter") %></asp:Label>
                                                    </td>
                                                </tr>
                                            </ItemTemplate>
                                        </asp:Repeater>

                                    </table>

                                    <asp:GridView ID="gvw_racikan_header" runat="server" AutoGenerateColumns="False" CssClass="table table-condensed table-header-racikan"
                                        EmptyDataText="No Data" DataKeyNames="prescription_compound_header_id" OnRowDataBound="gvw_racikan_header_RowDataBound">
                                        <Columns>
                                            <asp:TemplateField ItemStyle-Width="100%" HeaderStyle-BackColor="#f4f4f4" ItemStyle-CssClass="no-padding" ItemStyle-BorderWidth="0px">
                                                <HeaderTemplate>
                                                    <table style="width: 100%;" class="header-table">
                                                        <tr style="font-weight: bold; background-color: #f4f4f4;">
                                                            <td style="width: 30%; padding-left: 10px;">
                                                                <label id="lblbhs_racik_compoundname">Name</label></td>
                                                            <%--<td style="width: 5%;">
                                                                <label id="lblbhs_racik_dose">Text</label></td>--%>
                                                            <td style="width: 15%;">
                                                                <label id="lblbhs_racik_doseuom">Dose</label></td>
                                                            <td style="width: 10%;">
                                                                <label id="lblbhs_racik_freq">Frequency</label></td>
                                                            <td style="width: 15.5%;">
                                                                <label id="lblbhs_racik_route">Route</label></td>
                                                            <td style="width: 15%;">
                                                                <label id="lblbhs_racik_instruction">Instruction</label></td>
                                                            <td style="width: 5%;">
                                                                <label id="lblbhs_racik_qty">Qty</label></td>
                                                            <td style="width: 5%;">
                                                                <label id="lblbhs_racik_uom">UoM</label></td>
                                                            <td style="width: 5%;">
                                                                <label id="lblbhs_racik_iter">Iter</label></td>
                                                        </tr>
                                                    </table>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:HiddenField ID="HF_headerid_racikan" runat="server" Value='<%# Bind("prescription_compound_header_id") %>' />

                                                    <table style="width: 100%;" border="1" class="table-condensed">
                                                        <tr>
                                                            <td style="width: 30%; font-weight: bold; padding-left: 15px;">
                                                                <div>
                                                                    <asp:Label ID="lbl_nama_racikan" runat="server" Text='<%# Bind("compound_name") %>'></asp:Label>
                                                                </div>
                                                            </td>
                                                            <%--<td style="width: 5%; text-align: right;">
                                                            </td> --%>   
                                                            <td style="width: 15%;">
                                                                <asp:Label ID="lbl_dosis_racikan" runat="server" Text='<%# Bind("dose") %>' style='<%# Eval("IsDoseText").ToString().ToLower() == "true" ? "display:none" : "display:inline" %>'></asp:Label>
                                                                <asp:Label ID="lbl_dosisunit_racikan" runat="server" Text='<%# Bind("dose_uom") %>' style='<%# Eval("IsDoseText").ToString().ToLower() == "true" ? "display:none" : "display:inline" %>'></asp:Label>
                                                                <asp:Label id="lbl_dosistext_racikan" runat="server"  Text='<%# Bind("dose_text") %>' style='<%# Eval("IsDoseText").ToString().ToLower() == "true" ? "display:inline" : "display:inline" %>'></asp:Label>
                                                            </td>
                                                            <td style="width: 10%;">
                                                                <asp:Label ID="lbl_frekuensi_racikan" runat="server" Text='<%# Bind("frequency_code") %>'></asp:Label></td>
                                                            <td style="width: 15%;">
                                                                <asp:Label ID="lbl_rute_racikan" runat="server" Text='<%# Bind("administration_route_code") %>'></asp:Label></td>
                                                            <td style="width: 15%;">
                                                                <asp:Label ID="lbl_instruksi_racikan" runat="server" Text='<%# Bind("administration_instruction") %>'></asp:Label></td>
                                                            <td style="width: 5%; text-align: right;">
                                                                <asp:Label ID="lbl_jml_racikan" runat="server" Text='<%# Bind("quantity") %>'></asp:Label></td>
                                                            <td style="width: 5%;">
                                                                <asp:Label ID="lbl_unit_racikan" runat="server" Text='<%# Bind("uom_code") %>'></asp:Label></td>
                                                            <td style="width: 5%; text-align: right;">
                                                                <asp:Label ID="lbl_iter_racikan" runat="server" Text='<%# Bind("iter") %>'></asp:Label></td>
                                                        
                                                        </tr>
                                                    </table>

                                                    <div class="row" style="margin-left: 15px; margin-right: 15px; background-color: #f2f3f4; padding-top: 10px; padding-bottom: 10px;">
                                                        <div class="col-sm-6" style="width:50%; display:inline-block;">
                                                            <asp:GridView ID="gvw_racikan_detail" runat="server" AutoGenerateColumns="False" CssClass="table-detail-racikan"
                                                                DataKeyNames="prescription_compound_detail_id">
                                                                <Columns>
                                                                    <asp:BoundField ItemStyle-Width="50%" DataField="item_name" HeaderText="Item" ShowHeader="false" ItemStyle-VerticalAlign="Top" />
                                                                    <asp:TemplateField ItemStyle-Width="15%" HeaderText="Dose" ShowHeader="false">
                                                                        <ItemTemplate>
                                                                            <asp:Label id="lbldose" runat="server"  Text='<%# Bind("dose") %>' style='<%# Eval("IsDoseText").ToString().ToLower() == "true" ? "display:none" : "display:inline" %>'></asp:Label>
                                                                            <asp:Label id="lbldoseuom" runat="server"  Text='<%# Bind("dose_uom_code") %>' style='<%# Eval("IsDoseText").ToString().ToLower() == "true" ? "display:none" : "display:inline" %>'></asp:Label>
                                                                            <asp:Label id="lbldosetext" runat="server"  Text='<%# Bind("dose_text") %>' style='<%# Eval("IsDoseText").ToString().ToLower() == "true" ? "display:inline" : "display:inline" %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField ItemStyle-Width="5%">
                                                                        <ItemTemplate>
                                                                            &nbsp;
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:BoundField ItemStyle-Width="10%" DataField="quantity" HeaderText="Qty" ShowHeader="false" ItemStyle-HorizontalAlign="Right" ItemStyle-VerticalAlign="Top" HeaderStyle-CssClass="text-right" Visible="false" />
                                                                    <asp:TemplateField ItemStyle-Width="5%">
                                                                        <ItemTemplate>
                                                                            &nbsp;
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:BoundField ItemStyle-Width="15%" DataField="uom_code" HeaderText="Unit" ShowHeader="false" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-CssClass="text-left" Visible="false" />
                                                                </Columns>
                                                            </asp:GridView>
                                                        </div>
                                                        <div class="col-sm-6" style="border-left: 1px dashed #d0d1d2; margin-left: -1px; width:45%; display:inline-block; vertical-align:top;">
                                                            <label id="lblbhs_racik_note" style="font-weight: bold;">Instruksi Racikan Untuk Farmasi</label>
                                                            <br />
                                                            <asp:Label ID="lbl_instruksi_racikan_farmasi" runat="server" Text='<%# Eval("compound_note").ToString().Replace("\n","<br />") %>'></asp:Label>
                                                            <asp:HiddenField ID="HF_lbl_instruksi_racikan_farmasi" runat="server" Value='<%# Bind("compound_note") %>' />
                                                        </div>
                                                    </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>

                                </div>
                            </td>
                        </tr>


                        <%-- =============================================== START COMPOUND ==================================================== --%>
                        <tr style="border: solid 1px #cdced9" runat="server" id="trcompound" visible="false">
                            <td style="width:25%; background-color: #efefef;" class="itemtable-priview-title">
                                <label style="padding-left: 12px; font-weight: bold"></label>
                            </td>
                            <td colspan="2" style="width:75%;" class="itemtable-priview">
                                <div style="padding-left: 12px; padding-right: 12px; padding-top: 12px; padding-bottom: 12px">
                                    <label style="font-size: 14px; font-weight: bold;">Compound</label><br />
                                    <asp:Label runat="server" ID="compound" Font-Size="13px" Text="No Compound Prescription" />
                                    <div runat="server" id="divcompoundprescription">
                                        <table style="width: 100%;">
                                            <tr class="bordertable" style="vertical-align: top; width: 100%;">
                                                <td style="width: 200px; padding-left: 12px">
                                                    <label style="font-size: 11px; font-weight: bold">Item</label>
                                                </td>
                                                <td style="width: 50px">
                                                    <label style="font-size: 14px; font-weight: bold">Qty</label>
                                                </td>
                                                <td style="width: 40px">
                                                    <label style="font-size: 14px; font-weight: bold">U.O.M</label>
                                                </td>
                                                <td style="width: 100px">
                                                    <label style="font-size: 14px; font-weight: bold">Frequency</label>
                                                </td>
                                                <td style="width: 140px">
                                                    <label style="font-size: 14px; font-weight: bold">Dose & Instruction</label>
                                                </td>
                                                <td style="width: 80px">
                                                    <label style="font-size: 14px; font-weight: bold">Route</label>
                                                </td>
                                                <td style="width: 50px">
                                                    <label style="font-size: 14px; font-weight: bold">Iter</label>
                                                </td>
                                            </tr>
                                            <asp:Repeater runat="server" ID="prescriptioncompound" OnItemDataBound="Repeater1_ItemDataBound">
                                                <ItemTemplate>
                                                    <tr class="bordertable" style="vertical-align: top; width: 100%;">
                                                        <td style="width: 200px; padding-left: 12px">
                                                            <asp:Label ID="NameLabel" Style="font-size: 14px" Width="90%" runat="server" Text='<%#Eval("compound_name") %>' Enabled="false" />
                                                        </td>
                                                        <td style="width: 50px">
                                                            <asp:Label ID="Label1" Style="font-size: 14px" Width="90%" runat="server" Enabled="false"><%#Eval("quantity","{0:G29}") %></asp:Label>
                                                        </td>
                                                        <td style="width: 40px">
                                                            <asp:Label ID="Label2" Style="font-size: 14px" Width="90%" runat="server" Enabled="false"><%#Eval("uom") %></asp:Label>
                                                        </td>
                                                        <td style="width: 100px">
                                                            <asp:Label ID="Label3" Style="font-size: 14px" Width="90%" runat="server" Enabled="false"><%#Eval("frequency") %></asp:Label>
                                                        </td>
                                                        <td style="width: 140px">
                                                            <asp:Label ID="Label4" Style="font-size: 14px" Width="90%" runat="server" Enabled="false"><%#Eval("instruction") %></asp:Label>
                                                        </td>
                                                        <td style="width: 80px">
                                                            <asp:Label ID="Label5" Style="font-size: 14px" Width="90%" runat="server" Enabled="false"><%#Eval("route") %></asp:Label>
                                                        </td>
                                                        <td style="width: 50px">
                                                            <asp:Label ID="Label6" Style="font-size: 14px" Width="90%" runat="server" Enabled="false"><%#Eval("iteration") %></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr class="bordertable" style="vertical-align: top; width: 100%;">
                                                        <td style="width: 250px" colspan="7">
                                                            <asp:Repeater ID="rptCompDetail" runat="server">
                                                                <ItemTemplate>
                                                                    <li style="padding-left: 15px">
                                                                        <asp:Label ID="NameLabel" Font-Size="12px" Width="90%" runat="server" Text='<%#Eval("salesItemName") %>' Enabled="false" />
                                                                        <br />
                                                                        <asp:Label ID="Label1" Font-Size="12px" Width="90%" runat="server" Enabled="false">Qty: <%#Eval("Remarks") %></asp:Label>
                                                                    </li>
                                                                </ItemTemplate>
                                                            </asp:Repeater>
                                                        </td>
                                                    </tr>
                                                </ItemTemplate>
                                            </asp:Repeater>
                                        </table>

                                        <%-- <asp:Repeater runat="server" ID="rptCompound" OnItemDataBound="Repeater1_ItemDataBound">
                                        <ItemTemplate>
                                            <asp:Label ID="NameLabel" runat="server" Font-Size="12px" Font-Bold="true" Font-Names="Helvetica, Arial, sans-serif" Enabled="false"><%#Eval("item_name") %>&nbsp&nbsp<%#Eval("quantity") %>&nbsp<%#Eval("uom_code") %>,&nbsp<%#Eval("frequency_code") %>,&nbsp<%#Eval("remarks") %>,&nbsp<%#Eval("administration_route_code") %>,&nbsp<%#Eval("is_routine") %></asp:Label>
                                            <br />
                                            <asp:Repeater ID="rptCompDetail" runat="server">
                                                <ItemTemplate>
                                                    <li>
                                                        <asp:Label ID="NameLabel" runat="server" Font-Size="12px" Font-Names="Helvetica, Arial, sans-serif" Enabled="false"><%#Eval("item_name") %>&nbsp&nbsp<%#Eval("quantity") %>&nbsp<%#Eval("uom_code") %>,&nbsp<%#Eval("frequency_code") %>,&nbsp<%#Eval("remarks") %>,&nbsp<%#Eval("administration_route_code") %>,&nbsp<%#Eval("is_routine") %></asp:Label>
                                                    </li>
                                                </ItemTemplate>
                                            </asp:Repeater>
                                            <br />
                                        </ItemTemplate>
                                    </asp:Repeater>--%>

                                        <%--<asp:Repeater runat="server" ID="prescriptioncompound" OnItemDataBound="Repeater1_ItemDataBound">
                                        <ItemTemplate>
                                                <asp:Label ID="NameLabel" Width="90%" runat="server" Text='<%#Eval("salesItemName") %>' Enabled="false" />
                                               <br />
                                               <asp:Label ID="Label1" Width="90%" runat="server" Enabled="false">Qty: <%#Eval("Remarks") %></asp:Label>
                                            <br />
                                            <asp:Repeater ID="rptCompDetail" runat="server">
                                                <ItemTemplate>  
                                                    <li style="padding-left:15px">
                                                        <asp:Label ID="NameLabel" Width="90%" runat="server" Text='<%#Eval("salesItemName") %>' Enabled="false" />
                                                        <br />
                                                        <asp:Label ID="Label1" Width="90%" runat="server" Enabled="false">Qty: <%#Eval("Remarks") %></asp:Label>
                                                    </li>
                                                </ItemTemplate>
                                            </asp:Repeater>
                                            <br />
                                        </ItemTemplate>
                                    </asp:Repeater>--%>
                                    </div>
                                </div>
                            </td>
                        </tr>
                        <%-- =============================================== END COMPOUND ==================================================== --%>

                        <%-- =============================================== START CONSUMABLE ==================================================== --%>
                        <tbody>
                            <tr style="border: solid 1px #cdced9; width: 100%">
                                <td style="background-color: #efefef; width: 25%" class="itemtable-priview-title">
                                    <label style="padding-left: 12px; font-weight: bold"></label>
                                </td>
                                <td colspan="2" class="itemtable-priview" style="width: 75%">
                                    <label style="font-size: 14px; font-weight: bold;">Consumables </label>
                                    <label style="font-size: 14px; font-style: italic;">(Alat Kesehatan)</label>
                                    <br />
                                    <asp:Label runat="server" ID="cons" Font-Size="13px" Text="-" />
                                    <div runat="server" id="divconsumables">

                                        <asp:GridView ID="prescriptionconsumables" runat="server" AutoGenerateColumns="False" CssClass="table table-striped table-bordered table-condensed"
                                            EmptyDataText="" Font-Names="Helvetica" RowStyle-Width="100%" HeaderStyle-Width="100%" ShowHeader="true">
                                            <PagerStyle CssClass="pagination-ys" />
                                            <Columns>
                                                <asp:TemplateField HeaderText="Item" HeaderStyle-Width="15%" ItemStyle-Width="15%" ItemStyle-HorizontalAlign="left" HeaderStyle-Font-Size="11px" HeaderStyle-CssClass="wordbreak contentTable">
                                                    <ItemTemplate>
                                                        <asp:Label ID="NameLabel" CssClass="contentTable" Style="word-break: break-word" Width="90%" runat="server" Text='<%#Eval("salesItemName") %>' Enabled="false" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Qty" HeaderStyle-Width="5%" ItemStyle-Width="5%" ItemStyle-HorizontalAlign="Center" HeaderStyle-Font-Size="11px" HeaderStyle-CssClass="wordbreak contentTable">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label1" CssClass="contentTable" Style="word-break: break-word" Width="90%" runat="server" Enabled="false"><%#Eval("quantity","{0:G29}") %></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="UOM" HeaderStyle-Width="5%" ItemStyle-Width="5%" ItemStyle-HorizontalAlign="Center" HeaderStyle-Font-Size="11px" HeaderStyle-CssClass="wordbreak contentTable">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label2" CssClass="contentTable" Style="word-break: break-word" Width="90%" runat="server" Enabled="false"><%#Eval("uom") %></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Dose & Instruction" HeaderStyle-Width="15%" ItemStyle-Width="15%" ItemStyle-HorizontalAlign="Center" HeaderStyle-Font-Size="11px" HeaderStyle-CssClass="wordbreak contentTable">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label4" CssClass="contentTable" Style="word-break: break-word" Width="90%" runat="server" Enabled="false"><%#Eval("instruction") %></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>

                                    </div>
                                </td>
                            </tr>
                        </tbody>
                        <%-- =============================================== END CONSUMABLE ==================================================== --%>


                        <%-- =============================================== START ADD. PRESC ==================================================== --%>
                        <tbody>
                            <tr style="border: solid 1px #cdced9; width: 100%" runat="server" id="trAdditionalDrugs">
                                <td style="background-color: #efefef; width: 25%" class="itemtable-priview-title">
                                    <label style="padding-left: 12px; font-weight: bold"></label>
                                </td>
                                <td colspan="2" class="itemtable-priview" style="width: 75%">
                                    <label style="font-size: 14px; font-weight: bold;">Additional Drugs </label>
                                    <label style="font-size: 14px; font-style: italic;">(Tambahan Obat)</label><br />
                                    <asp:Label runat="server" ID="lbladditionalpres" Font-Size="13px" Text="-" />
                                    <div runat="server" id="dvAdditionalPres">

                                        <asp:GridView ID="rptAdditionalDrugs" runat="server" AutoGenerateColumns="False" CssClass="table table-striped table-bordered table-condensed"
                                            EmptyDataText="" Font-Names="Helvetica" RowStyle-Width="100%" HeaderStyle-Width="100%" ShowHeader="true">
                                            <PagerStyle CssClass="pagination-ys" />
                                            <Columns>
                                                <asp:TemplateField HeaderText="Drug" HeaderStyle-Width="14%" ItemStyle-Width="14%" ItemStyle-HorizontalAlign="left" HeaderStyle-Font-Size="11px" HeaderStyle-CssClass="wordbreak contentTable">
                                                    <ItemTemplate>
                                                        <asp:Label ID="NameLabel" runat="server" Text='<%#Eval("salesItemName") %>' Enabled="false" Style="word-break: break-word" CssClass="contentTable" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Qty" HeaderStyle-Width="5%" ItemStyle-Width="5%" ItemStyle-HorizontalAlign="Center" HeaderStyle-Font-Size="11px" HeaderStyle-CssClass="wordbreak">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label1" Style="word-break: break-word" runat="server" Enabled="false" CssClass="contentTable"><%#Eval("quantity","{0:G29}") %></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="UOM" HeaderStyle-Width="6%" ItemStyle-Width="6%" ItemStyle-HorizontalAlign="Center" HeaderStyle-Font-Size="11px" HeaderStyle-CssClass="wordbreak">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label2" Style="word-break: break-word" runat="server" Enabled="false" CssClass="contentTable"><%#Eval("uom") %></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Frequency" HeaderStyle-Width="10%" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Center" HeaderStyle-Font-Size="11px" HeaderStyle-CssClass="wordbreak">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label3" Style="word-break: break-word" runat="server" Enabled="false" CssClass="contentTable"><%#Eval("frequency") %></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Dose" HeaderStyle-Width="6%" ItemStyle-Width="6%" ItemStyle-HorizontalAlign="Center" HeaderStyle-Font-Size="11px" HeaderStyle-CssClass="wordbreak">
                                                    <ItemTemplate>
                                                        <div runat="server" Visible='<%# Eval("IsDoseText").ToString() == "False" %>'>
                                                            <asp:Label ID="Label8" Style="word-break: break-word" runat="server" Enabled="false" CssClass="contentTable"><%#Eval("dose","{0:G29}") %></asp:Label>
                                                            &nbsp;
                                                            <asp:Label ID="Label9" Style="word-break: break-word" runat="server" Enabled="false" CssClass="contentTable"><%#Eval("dose_uom") %></asp:Label>
                                                        </div>
                                                        <asp:Label ID="lblDoseText" Style="word-break: break-word" runat="server" Enabled="false" CssClass="contentTable"  Visible='<%# Eval("IsDoseText") %>' Text='<%# Bind("doseText") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <%--<asp:TemplateField HeaderText="Dose UOM" HeaderStyle-Width="11%" ItemStyle-Width="11%" ItemStyle-HorizontalAlign="Center" HeaderStyle-Font-Size="11px" HeaderStyle-CssClass="wordbreak">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label9" Style="word-break: break-word" runat="server" Enabled="false" CssClass="contentTable"><%#Eval("dose_uom") %></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>--%>
                                                <asp:TemplateField HeaderText="Instruction" HeaderStyle-Width="14%" ItemStyle-Width="14%" ItemStyle-HorizontalAlign="center" HeaderStyle-Font-Size="11px" HeaderStyle-CssClass="wordbreak">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label4" Style="word-break: break-word" runat="server" Enabled="false" CssClass="contentTable"><%#Eval("instruction") %></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Route" HeaderStyle-Width="7%" ItemStyle-Width="7%" ItemStyle-HorizontalAlign="Center" HeaderStyle-Font-Size="11px" HeaderStyle-CssClass="wordbreak">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label5" Style="word-break: break-word" runat="server" Enabled="false" CssClass="contentTable"><%#Eval("route") %></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Iter" HeaderStyle-Width="5%" ItemStyle-Width="5%" ItemStyle-HorizontalAlign="Center" HeaderStyle-Font-Size="11px" HeaderStyle-CssClass="wordbreak">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label6" Style="word-break: break-word" runat="server" Enabled="false" CssClass="contentTable"><%#Eval("iteration") %></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Routine" HeaderStyle-Width="5%" ItemStyle-Width="5%" ItemStyle-HorizontalAlign="Center" HeaderStyle-Font-Size="11px" HeaderStyle-CssClass="wordbreak">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label7" Style="word-break: break-word" runat="server" Enabled="false" CssClass="contentTable"><%#Eval("routine") %></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                    <br />
                                    <label style="font-size: 14px; font-weight: bold;">Doctor Additional Prescription Notes </label>
                                    <label style="font-size: 14px; font-style: italic;">(Catatan Tambahan Resep Dokter)</label><br />
                                    <asp:Label runat="server" ID="additionalDrugsNoteByDoctor" Font-Size="13px" Text="-" />
                                </td>
                            </tr>
                        </tbody>
                        <%-- =============================================== END ADD. PRESC ==================================================== --%>

                        <%-- =============================================== RACIKAN ADDITIONAL ==================================================== --%>
                        <tr style="border: solid 1px #cdced9; width: 100%;" runat="server" id="trAdditionalRacikan">
                            <td style="background-color: #efefef; width: 25%;" class="itemtable-priview-title">
                                <label style="padding-left: 13px; font-weight: bold; font-size: 13px"></label>
                            </td>
                            <td colspan="2" style="width: 75%;" class="itemtable-priview">
                                <label style="font-size: 14px; font-weight: bold;">Additional Compounds </label>
                                <label style="font-size: 14px; font-style: italic;">(Racikan Tambahan)</label><br />

                                <asp:Label runat="server" ID="noracikanadd" Font-Size="13px" Text="-" />
                                <div style="padding-bottom: 5px" runat="server" id="divRacikanAdditional">

                                    <table style="width: 100%; display:none;" class="table table-striped table-bordered table-condensed">
                                        <tr class="bordertable" style="vertical-align: top; width: 100%;">
                                            <td style="width: 30%; padding-left: 13px;" class="wordbreak">
                                                <label style="font-size: 11px; font-weight: bold">Name</label>
                                            </td>
                                            <td style="width: 15%" class="wordbreak">
                                                <label style="font-size: 11px; font-weight: bold">Dose</label>
                                            </td>
                                            <%--<td style="width: 15%" class="wordbreak">
                                                <label style="font-size: 11px; font-weight: bold">Dose UoM</label>
                                            </td>--%>
                                            <td style="width: 10%" class="wordbreak">
                                                <label style="font-size: 11px; font-weight: bold">Frequency</label>
                                            </td>
                                            <td style="width: 10%" class="wordbreak">
                                                <label style="font-size: 11px; font-weight: bold">Route</label>
                                            </td>
                                            <td style="width: 20%" class="wordbreak">
                                                <label style="font-size: 11px; font-weight: bold">Instruction</label>
                                            </td>
                                            <td style="width: 5%" class="wordbreak">
                                                <label style="font-size: 11px; font-weight: bold">Qty</label>
                                            </td>
                                            <td style="width: 5%" class="wordbreak">
                                                <label style="font-size: 11px; font-weight: bold">U.O.M</label>
                                            </td>
                                            <td style="width: 5%" class="wordbreak">
                                                <label style="font-size: 11px; font-weight: bold">Iter</label>
                                            </td>
                                        </tr>

                                        <asp:Repeater runat="server" ID="RepeaterRAcikanAdd">
                                            <ItemTemplate>
                                                <tr class="bordertable" style="vertical-align: top; width: 100%">
                                                    <td style="width: 30%; padding-left: 13px">
                                                        <asp:Label ID="racikanname" Style="font-size: 13px" Width="90%" runat="server" Text='<%#Eval("compound_name") %>' Enabled="false" />
                                                    </td>
                                                    <td style="width: 15%;" class="wordbreak">
                                                       <%-- <asp:Label ID="racikandose" Style="font-size: 13px" Width="90%" runat="server" Enabled="false"><%#Eval("dose","{0:G29}") %></asp:Label>--%>
                                                        <div runat="server" Visible='<%# Eval("IsDoseText").ToString() == "False" %>'>
                                                            <asp:Label ID="Label8" Style="word-break: break-word" runat="server" Enabled="false" CssClass="contentTable"><%#Eval("dose","{0:G29}") %></asp:Label>
                                                            &nbsp;
                                                            <asp:Label ID="Label9" Style="word-break: break-word" runat="server" Enabled="false" CssClass="contentTable"><%#Eval("dose_uom") %></asp:Label>
                                                        </div>
                                                        <asp:Label ID="lblDoseText" Style="word-break: break-word" runat="server" Enabled="false" CssClass="contentTable"  Visible='<%# Eval("IsDoseText") %>' Text='<%# Bind("dose_text") %>'></asp:Label>
                                                    </td>
                                                    <%--<td style="width: 15%;" class="wordbreak">
                                                        <asp:Label ID="racikandoseuom" Style="font-size: 13px" Width="90%" runat="server" Enabled="false"><%#Eval("dose_uom") %></asp:Label>
                                                    </td>--%>
                                                    <td style="width: 10%;" class="wordbreak">
                                                        <asp:Label ID="racikanfrequency" Style="font-size: 13px" Width="90%" runat="server" Enabled="false"><%#Eval("frequency_code") %></asp:Label>
                                                    </td>
                                                    <td style="width: 10%;" class="wordbreak">
                                                        <asp:Label ID="racikanroute" Style="font-size: 13px" Width="90%" runat="server" Enabled="false"><%#Eval("administration_route_code") %></asp:Label>
                                                    </td>
                                                    <td style="width: 20%;" class="wordbreak">
                                                        <asp:Label ID="racikaninstruction" Style="font-size: 13px" Width="90%" runat="server" Enabled="false"><%#Eval("administration_instruction") %></asp:Label>
                                                    </td>
                                                    <td style="width: 5%;" class="wordbreak">
                                                        <asp:Label ID="racikanqty" Style="font-size: 13px" Width="90%" runat="server" Enabled="false"><%#Eval("quantity","{0:G29}") %></asp:Label>
                                                    </td>
                                                    <td style="width: 5%;" class="wordbreak">
                                                        <asp:Label ID="racikanuom" Style="font-size: 13px" Width="90%" runat="server" Enabled="false"><%#Eval("uom_code") %></asp:Label>
                                                    </td>
                                                    <td style="width: 5%;" class="wordbreak">
                                                        <asp:Label ID="racikaniter" Style="font-size: 13px" Width="90%" runat="server" Enabled="false"><%#Eval("iter") %></asp:Label>
                                                    </td>
                                                </tr>
                                            </ItemTemplate>
                                        </asp:Repeater>

                                    </table>

                                    <asp:GridView ID="gvw_racikan_header_add" runat="server" AutoGenerateColumns="False" CssClass="table table-condensed table-header-racikan"
                                        EmptyDataText="No Data" DataKeyNames="prescription_compound_header_id" OnRowDataBound="gvw_racikan_header_add_RowDataBound">
                                        <Columns>
                                            <asp:TemplateField ItemStyle-Width="100%" HeaderStyle-BackColor="#f4f4f4" ItemStyle-CssClass="no-padding" ItemStyle-BorderWidth="0px">
                                                <HeaderTemplate>
                                                    <table style="width: 100%;" class="header-table">
                                                        <tr style="font-weight: bold; background-color: #f4f4f4;">
                                                            <td style="width: 30%; padding-left: 10px;">
                                                                <label id="lblbhs_racik_compoundname">Name</label></td>
                                                            <%--<td style="width: 5%;">
                                                                <label id="lblbhs_racik_dose">Text</label></td>--%>
                                                            <td style="width: 15%;">
                                                                <label id="lblbhs_racik_doseuom">Dose</label></td>
                                                            <td style="width: 10%;">
                                                                <label id="lblbhs_racik_freq">Frequency</label></td>
                                                            <td style="width: 15.5%;">
                                                                <label id="lblbhs_racik_route">Route</label></td>
                                                            <td style="width: 15%;">
                                                                <label id="lblbhs_racik_instruction">Instruction</label></td>
                                                            <td style="width: 5%;">
                                                                <label id="lblbhs_racik_qty">Qty</label></td>
                                                            <td style="width: 5%;">
                                                                <label id="lblbhs_racik_uom">UoM</label></td>
                                                            <td style="width: 5%;">
                                                                <label id="lblbhs_racik_iter">Iter</label></td>
                                                        </tr>
                                                    </table>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:HiddenField ID="HF_headerid_racikan" runat="server" Value='<%# Bind("prescription_compound_header_id") %>' />

                                                    <table style="width: 100%;" border="1" class="table-condensed">
                                                        <tr>
                                                            <td style="width: 30%; font-weight: bold; padding-left: 15px;">
                                                                <div>
                                                                    <asp:Label ID="lbl_nama_racikan" runat="server" Text='<%# Bind("compound_name") %>'></asp:Label>
                                                                </div>
                                                            </td>
                                                            <%--<td style="width: 5%; text-align: right;">
                                                            </td> --%>   
                                                            <td style="width: 15%;">
                                                                <asp:Label ID="lbl_dosis_racikan" runat="server" Text='<%# Bind("dose") %>' style='<%# Eval("IsDoseText").ToString().ToLower() == "true" ? "display:none" : "display:inline" %>'></asp:Label>
                                                                <asp:Label ID="lbl_dosisunit_racikan" runat="server" Text='<%# Bind("dose_uom") %>' style='<%# Eval("IsDoseText").ToString().ToLower() == "true" ? "display:none" : "display:inline" %>'></asp:Label>
                                                                <asp:Label id="lbl_dosistext_racikan" runat="server"  Text='<%# Bind("dose_text") %>' style='<%# Eval("IsDoseText").ToString().ToLower() == "true" ? "display:inline" : "display:inline" %>'></asp:Label>
                                                            </td>
                                                            <td style="width: 10%;">
                                                                <asp:Label ID="lbl_frekuensi_racikan" runat="server" Text='<%# Bind("frequency_code") %>'></asp:Label></td>
                                                            <td style="width: 15%;">
                                                                <asp:Label ID="lbl_rute_racikan" runat="server" Text='<%# Bind("administration_route_code") %>'></asp:Label></td>
                                                            <td style="width: 15%;">
                                                                <asp:Label ID="lbl_instruksi_racikan" runat="server" Text='<%# Bind("administration_instruction") %>'></asp:Label></td>
                                                            <td style="width: 5%; text-align: right;">
                                                                <asp:Label ID="lbl_jml_racikan" runat="server" Text='<%# Bind("quantity") %>'></asp:Label></td>
                                                            <td style="width: 5%;">
                                                                <asp:Label ID="lbl_unit_racikan" runat="server" Text='<%# Bind("uom_code") %>'></asp:Label></td>
                                                            <td style="width: 5%; text-align: right;">
                                                                <asp:Label ID="lbl_iter_racikan" runat="server" Text='<%# Bind("iter") %>'></asp:Label></td>
                                                        
                                                        </tr>
                                                    </table>

                                                    <div class="row" style="margin-left: 15px; margin-right: 15px; background-color: #f2f3f4; padding-top: 10px; padding-bottom: 10px;">
                                                        <div class="col-sm-6" style="width:50%; display:inline-block;">
                                                            <asp:GridView ID="gvw_racikan_detail_add" runat="server" AutoGenerateColumns="False" CssClass="table-detail-racikan"
                                                                DataKeyNames="prescription_compound_detail_id">
                                                                <Columns>
                                                                    <asp:BoundField ItemStyle-Width="50%" DataField="item_name" HeaderText="Item" ShowHeader="false" ItemStyle-VerticalAlign="Top" />
                                                                    <asp:TemplateField ItemStyle-Width="15%" HeaderText="Dose" ShowHeader="false">
                                                                        <ItemTemplate>
                                                                            <asp:Label id="lbldose" runat="server"  Text='<%# Bind("dose") %>' style='<%# Eval("IsDoseText").ToString().ToLower() == "true" ? "display:none" : "display:inline" %>'></asp:Label>
                                                                            <asp:Label id="lbldoseuom" runat="server"  Text='<%# Bind("dose_uom_code") %>' style='<%# Eval("IsDoseText").ToString().ToLower() == "true" ? "display:none" : "display:inline" %>'></asp:Label>
                                                                            <asp:Label id="lbldosetext" runat="server"  Text='<%# Bind("dose_text") %>' style='<%# Eval("IsDoseText").ToString().ToLower() == "true" ? "display:inline" : "display:inline" %>'></asp:Label>
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField ItemStyle-Width="5%">
                                                                        <ItemTemplate>
                                                                            &nbsp;
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:BoundField ItemStyle-Width="10%" DataField="quantity" HeaderText="Qty" ShowHeader="false" ItemStyle-HorizontalAlign="Right" ItemStyle-VerticalAlign="Top" HeaderStyle-CssClass="text-right" Visible="false" />
                                                                    <asp:TemplateField ItemStyle-Width="5%">
                                                                        <ItemTemplate>
                                                                            &nbsp;
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:BoundField ItemStyle-Width="15%" DataField="uom_code" HeaderText="Unit" ShowHeader="false" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-CssClass="text-left" Visible="false" />
                                                                </Columns>
                                                            </asp:GridView>
                                                        </div>
                                                        <div class="col-sm-6" style="border-left: 1px dashed #d0d1d2; margin-left: -1px; width:45%; display:inline-block; vertical-align:top;">
                                                            <label id="lblbhs_racik_note" style="font-weight: bold;">Instruksi Racikan Untuk Farmasi</label>
                                                            <br />
                                                            <asp:Label ID="lbl_instruksi_racikan_farmasi" runat="server" Text='<%# Eval("compound_note").ToString().Replace("\n","<br />") %>'></asp:Label>
                                                            <asp:HiddenField ID="HF_lbl_instruksi_racikan_farmasi" runat="server" Value='<%# Bind("compound_note") %>' />
                                                        </div>
                                                    </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>

                                </div>
                            </td>
                        </tr>

                        <%-- =============================================== START ADD. CONSUMABLE ==================================================== --%>
                        <tbody>
                            <tr style="border: solid 1px #cdced9; width: 100%" runat="server" id="trAdditionalConsumables">
                                <td style="background-color: #efefef; width: 25%" class="itemtable-priview-title">
                                    <label style="padding-left: 12px; font-weight: bold"></label>
                                </td>
                                <td colspan="2" class="itemtable-priview" style="width: 75%">
                                    <label style="font-size: 14px; font-weight: bold;">Additional Consumables</label>
                                    <label style="font-size: 14px; font-style: italic;">(Tambah Alat Kesehatan)</label><br />
                                    <asp:Label runat="server" ID="lbladditionalcons" Font-Size="13px" Text="-" />
                                    <div runat="server" id="dvAdditionalConsumables">
                                        <asp:GridView ID="rptAdditionalConsumables" runat="server" AutoGenerateColumns="False" CssClass="table table-striped table-bordered table-condensed"
                                            EmptyDataText="" Font-Names="Helvetica" RowStyle-Width="100%" HeaderStyle-Width="100%" ShowHeader="true">
                                            <PagerStyle CssClass="pagination-ys" />
                                            <Columns>
                                                <asp:TemplateField HeaderText="Item" HeaderStyle-Width="15%" ItemStyle-Width="15%" ItemStyle-HorizontalAlign="left" HeaderStyle-Font-Size="11px" HeaderStyle-CssClass="wordbreak contentTable">
                                                    <ItemTemplate>
                                                        <asp:Label ID="NameLabel" CssClass="contentTable" Style="word-break: break-word" Width="90%" runat="server" Text='<%#Eval("salesItemName") %>' Enabled="false" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Qty" HeaderStyle-Width="5%" ItemStyle-Width="5%" ItemStyle-HorizontalAlign="Center" HeaderStyle-Font-Size="11px" HeaderStyle-CssClass="wordbreak contentTable">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label1" CssClass="contentTable" Style="word-break: break-word" Width="90%" runat="server" Enabled="false"><%#Eval("quantity","{0:G29}") %></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="UOM" HeaderStyle-Width="5%" ItemStyle-Width="5%" ItemStyle-HorizontalAlign="Center" HeaderStyle-Font-Size="11px" HeaderStyle-CssClass="wordbreak contentTable">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label2" CssClass="contentTable" Style="word-break: break-word" Width="90%" runat="server" Enabled="false"><%#Eval("uom") %></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Dose & Instruction" HeaderStyle-Width="15%" ItemStyle-Width="15%" ItemStyle-HorizontalAlign="Center" HeaderStyle-Font-Size="11px" HeaderStyle-CssClass="wordbreak contentTable">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label4" CssClass="contentTable" Style="word-break: break-word" Width="90%" runat="server" Enabled="false"><%#Eval("instruction") %></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </td>
                            </tr>
                        </tbody>
                        <%-- =============================================== END ADD. CONSUMABLE ==================================================== --%>
                    </table>

                    <%-- =============================================== START ADD. FOOTER ==================================================== --%>
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
                    <%-- =============================================== END ADD. FOOTER ==================================================== --%>

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
                                    <asp:Label runat="server" ID="lblNamaDokter"></asp:Label>
                                    <hr style="margin-top: 0%; margin-bottom: 0%" />
                                    <asp:Label runat="server" ID="lblDokterKet" Text="Dokter(Physician)"></asp:Label>

                                </td>
                            </tr>
                        </tbody>
                    </table>
                    <%-- =============================================== END SIGN ==================================================== --%>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>

    </form>
</body>
</html>
