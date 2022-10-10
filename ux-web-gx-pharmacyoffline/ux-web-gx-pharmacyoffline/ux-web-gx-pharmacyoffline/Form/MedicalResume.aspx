<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MedicalResume.aspx.cs" Inherits="Form_MedicalResume" %>

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

       
    <link href="~/favicon.ico" rel="shortcut icon" type="image/x-icon" /><meta name="viewport" content="width=device-width, initial-scale=1.0" />

</head>
<body style="padding-top:15px;">
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
                thead { display: table-header-group; }
                tfoot { display: table-footer-group; }
            }
            .bordertable{
                border: 1px solid #cdced9;
            }
            tr.spaceUnder>td {
              padding-bottom: 3em;
            }

            #mydiv{
               margin-top:10%;
               position: fixed;
               bottom: 0px;
               left:5px
            }

            @media print {
                tr.page-break  { display: block; page-break-before: always; }
            }
            
            table { page-break-inside:auto }
            tr    { page-break-inside:avoid; page-break-after:auto }
            thead { display:table-header-group }
            tfoot { display:table-footer-group }

         </style>
        
        <div>
        <asp:updatepanel runat="server">
        <ContentTemplate>
            <div>
                <asp:HiddenField runat="server" ID="hfpreviewpres" />
                <br />
                <div>
                    <table style="width:99%;">
                        <%-- =============================================== HEADER ==================================================== --%>
                        <thead>
                            <tr>
                                <td colspan="2">            
                                    <div class="btn-group btn-group-justified" style="padding-bottom:10px" role="group" aria-label="...">
                                      <div class="btn-group" role="group" style="vertical-align:top">
                                        <asp:ImageButton ID="ImgLogoSH"  ImageUrl="~/Images/Icon/logo-SH.png" runat="server" Enabled="false" />
                                      </div>
                                  
                                      <div class="btn-group" role="group" style="text-align:left;padding-right:10px;padding-left:30px">
                                        <div><b>Resume Medis <asp:Label runat="server" ID="AdmissionType"></asp:Label></b></div>
                                          <table style="width:100%">
                                              <tr>
                                                  <td style="padding-right:20px;vertical-align:top">
                                                      <label style="font-size:11px">MR</label>
                                                  </td>
                                                  <td>
                                                      <label>: </label><asp:Label runat="server" ID="lblmrno" style="font-size:11px"></asp:Label>
                                                  </td>
                                               </tr>
                                              <tr>
                                                  <td style="padding-right:20px;vertical-align:top">
                                                      <label style="font-size:11px">Name</label>
                                                  </td>
                                                  <td>
                                                      <label>: <asp:Label runat="server" ID="lblnamepatient" style="font-size:11px"></asp:Label>
                                                  </td>
                                              </tr>
                                              <tr>
                                                  <td style="padding-right:20px;vertical-align:top">
                                                      <label style="font-size:11px">DOB/Age</label>
                                                  </td>
                                                  <td>
                                                      <label>: <asp:Label runat="server" ID="lbldobpatient" style="font-size:11px"></asp:Label>
                                                  </td>
                                              </tr>
                                              <tr>
                                                  <td style="padding-right:20px;vertical-align:top">
                                                      <label style="font-size:11px">Sex</label>
                                                  </td>
                                                  <td>
                                                      <label>: <asp:Label runat="server" ID="lblsexpatient" style="font-size:11px"></asp:Label>
                                                  </td>
                                              </tr>
                                              <tr>
                                                  <td style="padding-right:20px;vertical-align:top">
                                                      <label style="font-size:11px">Doctor</label>
                                                  </td>
                                                  <td>
                                                      <label>: <asp:Label runat="server" ID="lbldoctorprimary" style="font-size:11px"></asp:Label>
                                                  </td>
                                              </tr>
                                          </table>
                                      </div>
                                    </div>
                                </td>
                            </tr>
                        </thead>
                        <%-- =============================================== END HEADER ==================================================== --%>

                        <tr style="background-color:black;border:solid 1px #cdced9">
                            <td style="width:20%;border-right:solid 1px #cdced9"><label style="color:white;"><label style="padding-left:10px;">Keterangan</label></td>
                            <td style="width:80%"><label style="color:white"><label style="padding-left:10px">Deskripsi</label></td>
                        </tr>

                        <%-- =============================================== CHEIF COMPLAINT ==================================================== --%>
                        <tr style="border:solid 1px #cdced9">
                            <td style="background-color:#efefef;vertical-align:top;border-right:solid 1px #cdced9">
                                <label style="padding-left:10px;font-weight:bold;font-size:11px">Chief Complaint</label><br />
                                <label style="padding-left:10px;font-weight:bold;font-style:italic;font-size:11px">(Keluhan Utama)</label></td>
                            <td><div style="padding-left:10px;padding-right:10px;padding-top:10px;padding-bottom:10px;">
                                <asp:Label runat="server" ID="lblChiefComplaint" Style="font-size:11px;vertical-align:top">
                                </asp:Label>
                                </div>
                            </td>
                        </tr>
                        <%-- =============================================== END CHEIF COMPLAINT ==================================================== --%>
                        <%-- =============================================== ANAMNESIS ==================================================== --%>
                        <tr style="border:solid 1px #cdced9">
                            <td style="background-color:#efefef;vertical-align:top;border-right:solid 1px #cdced9">
                                <label style="padding-left:10px;font-weight:bold;font-size:11px">Anamnesis</label><br />
                                <label style="padding-left:10px;font-weight:bold;font-style:italic;font-size:11px">(Anamnesa)</label></td>
                            <td><div style="padding-left:10px;padding-right:10px;">
                                <table>
                                    <tr>
                                        <td style="width:250px;border-right:solid 1px #cdced9;padding-right:10px">
                                            <asp:Label runat="server" ID="Anamnesis" Style="font-size:11px"></asp:Label>
                                        </td>
                                        <td style="padding-left:10px">
                                            <label style="font-size:11px;font-weight:bold">Pregnant </label>
                                            <label style="font-size:10px;font-weight:bold;font-style:italic">(Hamil)</label>
                                            <asp:Label runat="server" ID="lblispregnant" Style="padding-left:72px">: -</asp:Label><br />
                                            <label style="font-size:11px;font-weight:bold">Breast Feeding </label>
                                            <label style="font-size:10px;font-weight:bold;font-style:italic">(Menyusui)</label>
                                            <asp:Label runat="server" ID="lblbreastfeed" Style="padding-left:20px">: -</asp:Label>
                                        </td>
                                    </tr>
                                </table>
                                </div>
                            </td>
                        </tr>
                        <%-- =============================================== END ANAMNESIS ==================================================== --%>
                        <%-- =============================================== MEDICATION & ALLERGIES ==================================================== --%>
                        <tr style="border:solid 1px #cdced9">
                            <td style="background-color:#efefef;vertical-align:top;border-right:solid 1px #cdced9">
                                <label style="padding-left:10px;font-weight:bold;font-size:11px">Medication & Allergies</label>
                                <br />
                                <label style="padding-left:10px;font-weight:bold;font-style:italic;font-size:11px">(Pengobatan & Alergi)</label>
                            

                            </td>
                            <td><div style="padding-left:10px;">
                                <table>
                                    <tr>
                                        <td style="width:200px;vertical-align:top;padding-bottom:10px;border-right:solid 1px #cdced9;padding-right:10px">
                                            <label style="font-size:11px;font-weight:bold">Routine Medication</label><br />
                                            <label style="font-size:10px;font-weight:bold;font-style:italic;">(Pengobatan Rutin)</label><br />
                                            <asp:Label runat="server" ID="lblnoroute" Font-Size="10px">No Routine Medication</asp:Label>
                                            <asp:Repeater runat="server" ID="rptRoutine" >
                                                <ItemTemplate>
                                                    <li>
                                                        <asp:Label ID="NameLabel" runat="server" Style="font-size:10px" Text='<%#Eval("value") %>' Enabled="false" />
                                                    </li>
                                                </ItemTemplate>
                                            </asp:Repeater>
                                        </td>
                                        <td style="width:150px;padding-left:10px;padding-bottom:10px;vertical-align:top;border-right:solid 1px #cdced9;padding-right:10px">
                                            <label style="font-size:11px;font-weight:bold">Drug Allergies</label><br />
                                            <label style="font-size:10px;font-weight:bold;font-style:italic;">(Alergi Obat)</label><br />
                                            <asp:Label runat="server" ID="lblnodrugallergy" Font-Size="10px">No Drug Allergy</asp:Label>
                                            <asp:Repeater runat="server" ID="rptDrugAllergies" >
                                                <ItemTemplate>
                                                    <li>
                                                        <asp:Label ID="NameLabel" runat="server" Style="font-size:10px" Text='<%#Eval("value") %>' Enabled="false" />
                                                    </li>
                                                </ItemTemplate>
                                            </asp:Repeater>
                                        </td>
                                        <td style="padding-left:10px;padding-bottom:10px;vertical-align:top;border-right:solid 1px #cdced9;padding-right:10px">
                                            <label style="font-size:11px;font-weight:bold">Food Allergies</label><br />
                                            <label style="font-size:10px;font-weight:bold;font-style:italic;">(Alergi Makanan)</label><br />
                                            <asp:Label runat="server" ID="lblnofoodallergy" Font-Size="10px">No Food Allergy</asp:Label>
                                            <asp:Repeater runat="server" ID="rptFoodAllergies" >
                                                <ItemTemplate>
                                                    <li>
                                                        <asp:Label ID="NameLabel" runat="server" Style="font-size:10px" Text='<%#Eval("value") %>' Enabled="false" />
                                                    </li>
                                                </ItemTemplate>
                                            </asp:Repeater>
                                        </td>
                                        <td style="padding-left:10px;padding-bottom:10px;vertical-align:top;padding-right:10px">
                                            <label style="font-size:11px;font-weight:bold">Other Allergies</label><br />
                                            <label style="font-size:10px;font-weight:bold;font-style:italic;">(Alergi Lainnya)</label><br />
                                            <asp:Label runat="server" ID="lblnootherallergy" Font-Size="10px">No Other Allergy</asp:Label>
                                            <asp:Repeater runat="server" ID="rptOtherAllergies" >
                                                <ItemTemplate>
                                                    <li>
                                                        <asp:Label ID="NameLabel" runat="server" Style="font-size:10px" Text='<%#Eval("value") %>' Enabled="false" />
                                                    </li>
                                                </ItemTemplate>
                                            </asp:Repeater>
                                        </td>
                                    </tr>
                                </table>
                                </div>
                            </td>
                        </tr>
                        <%-- =============================================== END MEDICATION & ALLERGIES ==================================================== --%>
                        <%-- =============================================== ILLNESS HISTORY ==================================================== --%>
                        <tr style="border:solid 1px #cdced9">
                            <td style="background-color:#efefef;vertical-align:top;border-right:solid 1px #cdced9">
                                <label style="padding-left:10px;font-weight:bold;font-size:11px">Illness History</label><br />
                                <label style="padding-left:10px;font-weight:bold;font-style:italic;font-size:11px">(Riwayat Penyakit)</label></td>
                            <td><div style="padding-left:10px;">
                                <table>
                                    <tr>
                                        <td style="width:200px;vertical-align:top;padding-bottom:10px;border-right:solid 1px #cdced9;padding-right:10px">
                                            <label style="font-size:11px;font-weight:bold">Surgery History</label><br />
                                            <label style="font-size:10px;font-weight:bold;font-style:italic;">(Riwayat Operasi)</label><br />
                                            <asp:Label runat="server" ID="lblnosurgery" Font-Size="10px">No Surgery History</asp:Label>
                                            <asp:Repeater runat="server" ID="rptSurgeryHistory" >
                                                <ItemTemplate>
                                                    <li>
                                                        <asp:Label ID="NameLabel" runat="server" Style="font-size:10px" Text='<%#Eval("value") %>' Enabled="false" />
                                                    </li>
                                                </ItemTemplate>
                                            </asp:Repeater>
                                        </td>
                                        <td style="width:200px;padding-left:10px;padding-bottom:10px;vertical-align:top;border-right:solid 1px #cdced9;padding-right:10px">
                                            <label style="font-size:11px;font-weight:bold">Disease History</label><br />
                                            <label style="font-size:10px;font-weight:bold;font-style:italic;">(Riwayat Penyakit)</label><br />
                                            <asp:Label runat="server" ID="lblnodisease" Font-Size="10px">No Disease History</asp:Label>
                                            <asp:Repeater runat="server" ID="rptDiseaseHistory" >
                                                <ItemTemplate>
                                                    <li>
                                                        <asp:Label ID="NameLabel" runat="server" Style="font-size:10px" Text='<%#Eval("remarks") %>' Enabled="false" />
                                                    </li>
                                                </ItemTemplate>
                                            </asp:Repeater>
                                        </td>
                                        <td style="width:200px;padding-left:10px;padding-bottom:10px;vertical-align:top;padding-right:10px">
                                            <label style="font-size:11px;font-weight:bold">Family Disease History</label><br />
                                            <label style="font-size:10px;font-weight:bold;font-style:italic;">(Riwayat Penyakit Keluarga)</label><br />
                                            <asp:Label runat="server" ID="lblnofamilydisease" Font-Size="10px">No Family Disease History</asp:Label>
                                            <asp:Repeater runat="server" ID="rptFamilyDisease" >
                                                <ItemTemplate>
                                                    <li>
                                                        <asp:Label ID="NameLabel" runat="server" Style="font-size:10px" Text='<%#Eval("remarks") %>' Enabled="false" />
                                                    </li>
                                                </ItemTemplate>
                                            </asp:Repeater>
                                        </td>
                                    </tr>
                                </table>
                                </div>
                            </td>
                        </tr>
                        <%-- =============================================== END ILLNESS HISTORY ==================================================== --%>
                        <%-- =============================================== GENERAL EXAMINATION ==================================================== --%>
                        <tr style="border:solid 1px #cdced9">
                            <td style="background-color:#efefef;vertical-align:top;padding-top:10px;border-right:solid 1px #cdced9">
                                <label style="padding-left:10px;font-weight:bold;font-size:11px">General Examination</label><br />
                                <label style="padding-left:10px;font-weight:bold;font-style:italic;font-size:11px">(Pemeriksaan Umum)</label></td>
                            <td><div>
                                <table>
                                    <tr>
                                        <td style="vertical-align:top;padding-top:10px;padding-bottom:10px;padding-left:10px;border-right:solid 1px #cdced9;padding-right:10px">
                                            <label style="font-size:11px;font-weight:bold"> Eye </label>
                                            <label style="font-size:10px;font-weight:bold;font-style:italic;">(Mata)</label>
                                            <label style="font-size:11px;font-weight:bold">:</label><br />
                                            <asp:Label runat="server" ID="eye" Style="font-size:11px" Text="-"/>
                                        </td>
                                        <td style="padding-left:10px;padding-top:10px;padding-bottom:10px;vertical-align:top;border-right:solid 1px #cdced9;padding-right:10px">
                                            <label style="font-size:11px;font-weight:bold"> Move </label>
                                            <label style="font-size:10px;font-weight:bold;font-style:italic;">(Motorik)</label>
                                            <label style="font-size:11px;font-weight:bold">:</label><br />
                                            <asp:Label runat="server" ID="move" Style="font-size:11px" Text="-"/>
                                        </td>
                                        <td style="padding-left:10px;padding-top:10px;padding-bottom:10px;vertical-align:top;border-right:solid 1px #cdced9;padding-right:10px">
                                            <label style="font-size:11px;font-weight:bold"> Verbal </label>
                                            <label style="font-size:10px;font-weight:bold;font-style:italic;">(Verbal)</label>
                                            <label style="font-size:11px;font-weight:bold">:</label><br />
                                            <asp:Label runat="server" ID="verbal" Style="font-size:11px" Text="-"/>
                                        </td>
                                        <td style="padding-left:10px;padding-top:10px;padding-bottom:10px;vertical-align:top;padding-right:10px">
                                            <label style="font-size:11px;font-weight:bold"> Score </label>
                                            <label style="font-size:10px;font-weight:bold;font-style:italic;">(Skor)</label>
                                            <label style="font-size:11px;font-weight:bold">:</label><br />
                                            <asp:Label runat="server" ID="painscore" Style="font-size:11px" Text="-"/>
                                        </td>
                                    </tr>
                                </table>
                                </div>
                                <div style="border-top:solid 1px #cdced9">
                                <table>
                                    <tr>
                                        <td style="vertical-align:top;padding-top:10px;padding-bottom:10px;padding-left:10px;padding-right:10px">
                                            <label style="font-size:11px;font-weight:bold"> Pain Scale </label>
                                            <label style="font-size:10px;font-weight:bold;font-style:italic;"> (Skala Nyeri)</label>
                                            <label style="font-size:11px;font-weight:bold">:</label><br />
                                            <asp:Label runat="server" Style="font-size:11px" ID="lblpainscale" Text="-"/>
                                        </td>
                                    </tr>
                                </table>
                                </div>
                                <div style="border-top:solid 1px #cdced9">
                                <table>
                                    <tr>
                                        <td style="padding-left:10px;padding-top:10px;padding-bottom:10px;vertical-align:top;border-right:solid 1px #cdced9;padding-right:10px">
                                            <label style="font-size:11px;font-weight:bold"> Blood Pressure </label><br />
                                            <label style="font-size:10px;font-weight:bold;font-style:italic;">(Tekanan Darah)</label>
                                            <br /><asp:Label runat="server" Style="font-size:11px" ID="bloodpreassure"  Text="-"/> <label style="font-size:11px">mmHg</label>
                                        </td>
                                        <td style="padding-left:10px;padding-top:10px;padding-bottom:10px;vertical-align:top;border-right:solid 1px #cdced9;padding-right:10px">
                                            <label style="font-size:11px;font-weight:bold"> Pulse Rate </label><br />
                                            <label style="font-size:10px;font-weight:bold;font-style:italic;">(Nadi)</label>
                                            <br /><asp:Label runat="server" Style="font-size:11px" ID="pulse"  Text="-"/><label style="font-size:11px">x/mnt</label> 
                                        </td>
                                        <td style="padding-left:10px;padding-top:10px;padding-bottom:10px;vertical-align:top;border-right:solid 1px #cdced9;padding-right:10px">
                                            <label style="font-size:11px;font-weight:bold"> Respiratory Rate </label><br />
                                            <label style="font-size:10px;font-weight:bold;font-style:italic;">(Pernapasan)</label>
                                            <br /><asp:Label runat="server" Style="font-size:11px" ID="respiratory"  Text="-"/><label style="font-size:11px">x/mnt</label> 
                                        </td>
                                        <td style="padding-left:10px;padding-top:10px;padding-bottom:10px;vertical-align:top;border-right:solid 1px #cdced9;padding-right:10px">
                                            <label style="font-size:11px;font-weight:bold"> SpO2 </label><br />
                                            <label style="font-size:10px;font-weight:bold;font-style:italic;">(SpO2)</label>
                                            <br /><asp:Label runat="server" Style="font-size:11px" ID="spo" Text="-"/><label style="font-size:11px">%</label> 
                                        </td>
                                        <td style="padding-left:10px;padding-top:10px;padding-bottom:10px;vertical-align:top;border-right:solid 1px #cdced9;padding-right:10px">
                                            <label style="font-size:11px;font-weight:bold"> Temperature </label><br />
                                            <label style="font-size:10px;font-weight:bold;font-style:italic;">(Suhu)</label>
                                            <br /><asp:Label runat="server" Style="font-size:11px" ID="temperature" Text="-"/> &deg;<label style="font-size:11px">C</label>
                                        </td>
                                        <td style="padding-left:10px;padding-top:10px;padding-bottom:10px;vertical-align:top;border-right:solid 1px #cdced9;padding-right:10px">
                                            <label style="font-size:11px;font-weight:bold"> Weight </label><br />
                                            <label style="font-size:10px;font-weight:bold;font-style:italic;">(Berat)</label>
                                            <br /><asp:Label runat="server" Style="font-size:11px" ID="weight" Text="-"/> <label style="font-size:11px">kg</label>
                                        </td>
                                        <td style="vertical-align:top;padding-top:10px;padding-bottom:10px;padding-left:10px; border-right:solid 1px #cdced9; padding-right:10px">
                                            <label style="font-size:11px;font-weight:bold"> Height </label><br />
                                            <label style="font-size:10px;font-weight:bold;font-style:italic;">(Tinggi)</label>
                                            <br /><asp:Label runat="server" Style="font-size:11px" ID="height" Text="-"/> <label style="font-size:11px">cm</label>
                                        </td>
                                         <td style="vertical-align:top;padding-top:10px;padding-bottom:10px;padding-left:10px;padding-right:10px">
                                            <label style="font-size:11px;font-weight:bold"> Head Circumference </label><br />
                                            <label style="font-size:10px;font-weight:bold;font-style:italic;">(Lingkar Kepala)</label>
                                            <br /><asp:Label runat="server" Style="font-size:17px" ID="headcircumference" Text="-"/>
                                        </td>
                                    </tr>
                                </table>
                                </div>
                                <div style="border-top:solid 1px #cdced9">
                                <table>
                                    <tr>
                                        <td style="vertical-align:top;padding-top:10px;padding-bottom:10px;padding-left:10px;border-right:solid 1px #cdced9;padding-right:10px">
                                            <label style="font-size:11px;font-weight:bold"> Mental Status </label><br />
                                            <label style="font-size:10px;font-weight:bold;font-style:italic;">(Status Mental)</label>
                                            <br /><asp:Label runat="server" Style="font-size:11px" ID="lblmentalstatus" Text="-"/>
                                        </td>
                                        <td style="vertical-align:top;padding-top:10px;padding-bottom:10px;padding-left:10px;border-right:solid 1px #cdced9;padding-right:10px">
                                            <label style="font-size:11px;font-weight:bold"> Consciousness </label><br />
                                            <label style="font-size:10px;font-weight:bold;font-style:italic;">(Kesadaran)</label>
                                            <br /><asp:Label runat="server" Style="font-size:11px" ID="lblconsciousness" Text="-"/>
                                        </td>
                                        <td style="vertical-align:top;padding-top:10px;padding-bottom:10px;padding-left:10px;padding-right:10px">
                                            <label style="font-size:11px;font-weight:bold"> Fall Risk </label><br />
                                            <label style="font-size:10px;font-weight:bold;font-style:italic;">(Risiko Jatuh)</label>
                                            <br />
                                        
                                            <asp:Repeater runat="server" ID="rptfallrisk" >
                                                <ItemTemplate>
                                                   <li>
                                                        <asp:Label ID="NameLabel" runat="server" Style="font-size:11px" Text='<%#Eval("remarks") %>' Enabled="false" />
                                                    </li>
                                                </ItemTemplate>
                                            </asp:Repeater>
                                            <asp:Label ID="NoNameLabel" runat="server" Style="font-size:11px" Text="-" Enabled="false" />
                                        </td>
                                    </tr>
                                </table>
                                </div>
                            </td>
                        </tr>
                        <%-- =============================================== END GENERAL EXAMINATION ==================================================== --%>
                        <%-- =============================================== PHYSICAL EXAMINATION ==================================================== --%>
                        <tr style="border:solid 1px #cdced9">
                            <td style="background-color:#efefef;vertical-align:top;padding-top:10px;border-right:solid 1px #cdced9">
                                <label style="padding-left:10px;font-weight:bold;font-size:11px">Physical Examination</label><br />
                                <label style="padding-left:10px;font-weight:bold;font-style:italic;font-size:11px">(Pemeriksaan Fisik)</label></td>
                            <td><div style="padding-left:10px;padding-right:10px;padding-top:10px;padding-bottom:10px">
                                <div style="padding-bottom:5px">
                                    <asp:Label runat="server" Style="font-size:11px" ID="lblphysicalexamination">
                                        -
                                    </asp:Label>
                                </div>
                                </div>
                            </td>
                        </tr>
                        <%-- =============================================== END PHYSICAL EXAMINATION ==================================================== --%>
                        <%-- =============================================== DIAGNOSIS ==================================================== --%>
                        <tr style="border:solid 1px #cdced9">
                            <td style="background-color:#efefef;vertical-align:top;padding-top:10px;border-right:solid 1px #cdced9">
                                <label style="padding-left:10px;font-weight:bold;font-size:11px">Diagnosis</label><br />
                                <label style="padding-left:10px;font-weight:bold;font-style:italic;font-size:11px">(Diagnosa)</label></td>
                            <td><div style="padding-left:10px;padding-right:10px;padding-top:10px;padding-bottom:10px">
                                <div style="padding-bottom:5px">
                                    <asp:Label runat="server" Style="font-size:11px" ID="primarydiagnosis">
                                        -
                                    </asp:Label>
                                </div>
                                </div>
                            </td>
                        </tr>
                        <%-- =============================================== END DIAGNOSIS ==================================================== --%>
                        <%-- =============================================== PLANNING & PROCEDURE ==================================================== --%>
                        <tr style="border:solid 1px #cdced9">
                            <td style="background-color:#efefef;vertical-align:top;padding-top:10px;padding-bottom:10px;border-right:solid 1px #cdced9">
                                <label style="padding-left:10px;font-weight:bold;font-size:11px">Planning & Procedure</label><br />
                                <label style="padding-left:10px;font-weight:bold;font-style:italic;font-size:11px">(Tindakan di RS)</label></td>
                            <td><div style="padding-left:10px;padding-right:10px;padding-top:10px;padding-bottom:10px">
                                <div style="padding-bottom:5px">
                                <asp:Label runat="server" Style="font-size:11px" ID="procedure">
                                   -
                                </asp:Label>
                                </div>
                                </div>
                            </td>
                        </tr>
                        <%-- =============================================== END PLANNING & PROCEDURE ==================================================== --%>
                       <%-- =============================================== PROCEDURE RESULT ==================================================== --%>
                       <tr style="border: solid 1px #cdced9;">
                           <td style="background-color:#efefef;vertical-align:top;border-right:solid 1px #cdced9">
                               <label style="padding-left:10px;font-weight:bold;font-size:11px">Procedure Result</label><br />
                               <label style="padding-left:10px;font-weight:bold;font-style:italic;font-size:11px">(Hasil Tindakan)</label></td>
                           <td><div style="padding-left:10px;padding-right:10px;padding-top:10px;padding-bottom:10px;">
                                <asp:Label runat="server" ID="procedureResult" Style="font-size:11px;vertical-align:top">
                                </asp:Label>
                                </div>
                            </td>
                       </tr>
                        <%-- =============================================== END PROCEDURE RESULT ==================================================== --%>
                        <%-- =============================================== PRESCRIPTION ==================================================== --%>
                        <tr style="border:solid 1px #cdced9;">
                            <td style="background-color:#efefef;vertical-align:top;padding-top:10px;border-right:solid 1px #cdced9">
                                <label style="padding-left:10px;font-weight:bold;font-size:11px">Prescription</label><br />
                                <label style="padding-left:10px;font-weight:bold;font-style:italic;font-size:11px">(Resep)</label>
                            </td>
                            <td><div style="padding-left:10px;padding-right:10px;padding-top:10px;padding-bottom:10px">
                                <label style="font-size:11px;font-weight:bold;">Drugs </label>
                                <label style="font-size:10px;font-weight:bold;font-style:italic;">(Obat)</label><br />
                            
                                <asp:Label runat="server" ID="drugs" Font-Size="11px" Text="No Drug Prescription" />
                                <div style="padding-bottom:5px" runat="server" id="divdrugsprescription">
                                    <table>
                                        <tr class="bordertable" style="vertical-align:top">
                                            <td style="width:200px;padding-left:10px">
                                                <label Style="font-size:10px;font-weight:bold">Item</label>
                                            </td>
                                            <td style="width:50px">
                                                <label Style="font-size:10px;font-weight:bold">Qty</label>
                                            </td>
                                            <td style="width:40px">
                                                <label Style="font-size:10px;font-weight:bold">U.O.M</label>
                                            </td>
                                            <td style="width:100px">
                                                <label Style="font-size:10px;font-weight:bold">Frequency</label>
                                            </td>
                                            <td style="width:50px">
                                                <label Style="font-size:10px;font-weight:bold">Dose</label>
                                            </td>
                                            <td style="width:100px">
                                                <label Style="font-size:10px;font-weight:bold">Dose UoM</label>
                                            </td>
                                            <td style="width:140px">
                                                <label Style="font-size:10px;font-weight:bold">Instruction</label>
                                            </td>
                                            <td style="width:80px">
                                                <label Style="font-size:10px;font-weight:bold">Route</label>
                                            </td>
                                            <td style="width:50px">
                                                <label Style="font-size:10px;font-weight:bold">Iter</label>
                                            </td>
                                            <td style="width:50px">
                                                <label Style="font-size:10px;font-weight:bold">Routine</label>
                                            </td>
                                        </tr>
                                    
                                        <asp:Repeater runat="server" ID="prescriptiondrugs" >
                                            <ItemTemplate>
                                                <tr class="bordertable" style="vertical-align:top">
                                                   <td style="width:200px;padding-left:10px">
                                                    <asp:Label ID="NameLabel" Style="font-size:10px" Width="90%" runat="server" Text='<%#Eval("salesItemName") %>' Enabled="false" />                                               
                                                   </td>
                                                    <td style="width:50px">
                                                        <asp:Label ID="Label1" Style="font-size:10px" Width="90%" runat="server" Enabled="false"><%#Eval("quantity","{0:G29}") %></asp:Label>
                                                    </td>
                                                    <td style="width:40px">
                                                        <asp:Label ID="Label2" Style="font-size:10px" Width="90%" runat="server" Enabled="false"><%#Eval("uom") %></asp:Label>
                                                    </td>
                                                    <td style="width:100px">
                                                        <asp:Label ID="Label3" Style="font-size:10px" Width="90%" runat="server" Enabled="false"><%#Eval("frequency") %></asp:Label>
                                                    </td>
                                                    <td style="width:50px">
                                                        <asp:Label ID="Label8" Style="font-size:10px" Width="90%" runat="server" Enabled="false"><%#Eval("dose","{0:G29}") %></asp:Label>
                                                    </td>
                                                    <td style="width:100px">
                                                        <asp:Label ID="Label9" Style="font-size:10px" Width="90%" runat="server" Enabled="false"><%#Eval("dose_uom") %></asp:Label>
                                                    </td>
                                                    <td style="width:140px">
                                                        <asp:Label ID="Label4" Style="font-size:10px" Width="90%" runat="server" Enabled="false"><%#Eval("instruction") %></asp:Label>
                                                    </td>
                                                    <td style="width:80px">
                                                        <asp:Label ID="Label5" Style="font-size:10px" Width="90%" runat="server" Enabled="false"><%#Eval("route") %></asp:Label>
                                                    </td>
                                                    <td style="width:50px">
                                                        <asp:Label ID="Label6" Style="font-size:10px" Width="90%" runat="server" Enabled="false"><%#Eval("iteration") %></asp:Label>
                                                    </td>
                                                    <td style="width:50px">
                                                        <asp:Label ID="Label7" Style="font-size:10px" Width="90%" runat="server" Enabled="false"><%#Eval("routine") %></asp:Label>
                                                    </td>
                                                </tr> 
                                            </ItemTemplate>
                                        </asp:Repeater>
                                
                                    </table>
                                </div>
                                </div>
                                </td>
                        </tr>

                        <%-- =============================================== RACIKAN ==================================================== --%>
                    <tr style="border: solid 1px #cdced9">
                        <td style="background-color:#efefef;vertical-align:top;padding-top:10px;border-right:solid 1px #cdced9">
                            <label style="padding-left: 13px; font-weight: bold; font-size: 13px"></label>
                        </td>
                        <td><div style="padding-left:10px;padding-right:10px;padding-top:10px;padding-bottom:10px">
                            <label style="font-size: 11px; font-weight: bold;">Compounds </label>
                            <label style="font-size: 10px; font-weight: bold; font-style: italic;">(Racikan)</label><br />

                            <asp:Label runat="server" ID="noracikan" Font-Size="11px" Text="No Compound Prescription" />
                            <div style="padding-bottom: 5px" runat="server" id="divRacikan">
                                <table>
                                    <tr class="bordertable" style="vertical-align: top">
                                        <td style="width: 200px; padding-left: 13px">
                                            <label style="font-size: 10px; font-weight: bold">Name</label>
                                        </td>
                                        <td style="width: 50px">
                                            <label style="font-size: 10px; font-weight: bold">Dose</label>
                                        </td>
                                        <td style="width: 100px">
                                            <label style="font-size: 10px; font-weight: bold">Dose UoM</label>
                                        </td>
                                        <td style="width: 100px">
                                            <label style="font-size: 10px; font-weight: bold">Frequency</label>
                                        </td>
                                        <td style="width: 80px">
                                            <label style="font-size: 10px; font-weight: bold">Route</label>
                                        </td>
                                        <td style="width: 190px">
                                            <label style="font-size: 10px; font-weight: bold">Instruction</label>
                                        </td>
                                        <td style="width: 50px">
                                            <label style="font-size: 10px; font-weight: bold">Qty</label>
                                        </td>
                                        <td style="width: 40px">
                                            <label style="font-size: 10px; font-weight: bold">U.O.M</label>
                                        </td>
                                        <td style="width: 50px">
                                            <label style="font-size: 10px; font-weight: bold">Iter</label>
                                        </td>
                                    </tr>

                                    <asp:Repeater runat="server" ID="RepeaterRacikan" OnItemDataBound="RepeaterRacikan_ItemDataBound">
                                        <ItemTemplate>
                                            <tr class="bordertable" style="vertical-align: top">
                                                <td style="width: 200px; padding-left: 13px">
                                                    <asp:HiddenField ID="HF_racikanheaderid" runat="server" Value='<%#Eval("prescription_compound_header_id") %>' />
                                                    <asp:Label ID="racikanname" Style="font-size: 10px" Width="90%" runat="server" Text='<%#Eval("compound_name") %>' Enabled="false" />
                                                </td>
                                                <td style="width: 50px">
                                                    <asp:Label ID="racikandose" Style="font-size: 10px" Width="90%" runat="server" Enabled="false"><%#Eval("dose","{0:G29}") %></asp:Label>
                                                </td>
                                                <td style="width: 100px">
                                                    <asp:Label ID="racikandoseuom" Style="font-size: 10px" Width="90%" runat="server" Enabled="false"><%#Eval("dose_uom") %></asp:Label>
                                                </td>
                                                <td style="width: 100px">
                                                    <asp:Label ID="racikanfrequency" Style="font-size: 10px" Width="90%" runat="server" Enabled="false"><%#Eval("frequency_code") %></asp:Label>
                                                </td>
                                                <td style="width: 80px">
                                                    <asp:Label ID="racikanroute" Style="font-size: 10px" Width="90%" runat="server" Enabled="false"><%#Eval("administration_route_code") %></asp:Label>
                                                </td>
                                                <td style="width: 190px">
                                                    <asp:Label ID="racikaninstruction" Style="font-size: 10px" Width="90%" runat="server" Enabled="false"><%#Eval("administration_instruction") %></asp:Label>
                                                </td>
                                                <td style="width: 50px">
                                                    <asp:Label ID="racikanqty" Style="font-size: 10px" Width="90%" runat="server" Enabled="false"><%#Eval("quantity","{0:G29}") %></asp:Label>
                                                </td>
                                                <td style="width: 40px">
                                                    <asp:Label ID="racikanuom" Style="font-size: 10px" Width="90%" runat="server" Enabled="false"><%#Eval("uom_code") %></asp:Label>
                                                </td>
                                                <td style="width: 50px">
                                                    <asp:Label ID="racikaniter" Style="font-size: 10px" Width="90%" runat="server" Enabled="false"><%#Eval("iter") %></asp:Label>
                                                </td>
                                            </tr>
                                            <tr class="bordertable">
                                                <td colspan="4">
                                                    <table style="width:100%;">
                                                         <asp:Repeater runat="server" ID="RepeaterRacikanDetail">
                                                             <HeaderTemplate>
                                                                 <tr>
                                                                     <td style="padding-left:13px;">
                                                                         <label style="font-size: 10px; font-weight: bold">Item</label>
                                                                     </td>
                                                                     <td>
                                                                         <label style="font-size: 10px; font-weight: bold">Dose</label>
                                                                     </td>
                                                                 </tr>
                                                             </HeaderTemplate>
                                                             <ItemTemplate>
                                                                 <tr>
                                                                     <td style="padding-left:13px;">
                                                                         <asp:Label ID="racikanname" Style="font-size: 10px" Width="90%" runat="server" Text='<%#Eval("item_name") %>' Enabled="false" />
                                                                     </td>
                                                                     <td>
                                                                        <asp:Label id="lbldose" runat="server" Font-Size="10px" Text='<%# Bind("dose") %>' style='<%# Eval("IsDoseText").ToString().ToLower() == "true" ? "display:none" : "display:inline" %>'></asp:Label>
                                                                        <asp:Label id="lbldoseuom" runat="server" Font-Size="10px" Text='<%# Bind("dose_uom_code") %>' style='<%# Eval("IsDoseText").ToString().ToLower() == "true" ? "display:none" : "display:inline" %>'></asp:Label>
                                                                        <asp:Label id="lbldosetext" runat="server" Font-Size="10px" Text='<%# Bind("dose_text") %>' style='<%# Eval("IsDoseText").ToString().ToLower() == "true" ? "display:inline" : "display:none" %>'></asp:Label>
                                                                     </td>
                                                                 </tr>
                                                             </ItemTemplate>
                                                         </asp:Repeater>
                                                    </table>
                                                </td>
                                                <td colspan="5" style="padding-left:13px; vertical-align:top;">
                                                    <label style="font-size: 10px; font-weight: bold">Instruction</label>
                                                    <br />
                                                    <asp:Label ID="Label10" Style="font-size: 10px" Width="90%" runat="server" Enabled="false"><%#Eval("administration_instruction") %></asp:Label>
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:Repeater>

                                </table>
                            </div>
                            </div>
                        </td>
                    </tr>

                        <%-- =============================================== PRESCRIPTION ==================================================== --%>
                        <tr style="border:solid 1px #cdced9"  runat="server" id="trcompound" visible="false">
                            <td style="background-color:#efefef;vertical-align:top;padding-top:10px;border-right:solid 1px #cdced9">
                                <label style="padding-left:10px;font-weight:bold"></label></td>
                                <td>
                                    <div style="padding-left:10px;padding-right:10px;padding-top:10px;padding-bottom:10px">
                                    <label style="font-size:11px;font-weight:bold;">Compound</label><br />
                                    <asp:Label runat="server" ID="compound" Font-Size="11px" Text="No Compound Prescription" />
                                    <div runat="server" id="divcompoundprescription">
                                        <table>
                                            <tr class="bordertable" style="vertical-align:top">
                                                <td style="width:200px;padding-left:10px">
                                                    <label Style="font-size:10px;font-weight:bold">Item</label>
                                                </td>
                                                <td style="width:50px">
                                                    <label Style="font-size:10px;font-weight:bold">Qty</label>
                                                </td>
                                                <td style="width:40px">
                                                    <label Style="font-size:10px;font-weight:bold">U.O.M</label>
                                                </td>
                                                <td style="width:100px">
                                                    <label Style="font-size:10px;font-weight:bold">Frequency</label>
                                                </td>
                                                <td style="width:140px">
                                                    <label Style="font-size:10px;font-weight:bold">Dose & Instruction</label>
                                                </td>
                                                <td style="width:80px">
                                                    <label Style="font-size:10px;font-weight:bold">Route</label>
                                                </td>
                                                <td style="width:50px">
                                                    <label Style="font-size:10px;font-weight:bold">Iter</label>
                                                </td>
                                            </tr>
                                            <asp:Repeater runat="server" ID="prescriptioncompound" OnItemDataBound="Repeater1_ItemDataBound" >
                                            <ItemTemplate>
                                                <tr class="bordertable" style="vertical-align:middle">
                                                   <td style="width:200px;padding-left:10px">
                                                       <asp:Label ID="NameLabel" Style="font-size:10px" Width="90%" runat="server" Text='<%#Eval("compound_name") %>' Enabled="false" />                                               
                                                   </td>
                                                    <td style="width:50px">
                                                        <asp:Label ID="Label1" Style="font-size:10px" Width="90%" runat="server" Enabled="false"><%#Eval("quantity","{0:G29}") %></asp:Label>
                                                    </td>
                                                    <td style="width:40px">
                                                        <asp:Label ID="Label2" Style="font-size:10px" Width="90%" runat="server" Enabled="false"><%#Eval("uom") %></asp:Label>
                                                    </td>
                                                    <td style="width:100px">
                                                        <asp:Label ID="Label3" Style="font-size:10px" Width="90%" runat="server" Enabled="false"><%#Eval("frequency") %></asp:Label>
                                                    </td>
                                                    <td style="width:140px">
                                                        <asp:Label ID="Label4" Style="font-size:10px" Width="90%" runat="server" Enabled="false"><%#Eval("instruction") %></asp:Label>
                                                    </td>
                                                    <td style="width:80px">
                                                        <asp:Label ID="Label5" Style="font-size:10px" Width="90%" runat="server" Enabled="false"><%#Eval("route") %></asp:Label>
                                                    </td>
                                                    <td style="width:50px">
                                                        <asp:Label ID="Label6" Style="font-size:10px" Width="90%" runat="server" Enabled="false"><%#Eval("iteration") %></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr class="bordertable" style="vertical-align:top">
                                                    <td style="width:250px" colspan="7">
                                                        <asp:Repeater ID="rptCompDetail" runat="server">
                                                            <ItemTemplate>  
                                                                <li style="padding-left:15px">
                                                                    <asp:Label ID="NameLabel" Font-Size="10px" Width="90%" runat="server" Text='<%#Eval("salesItemName") %>' Enabled="false" />
                                                                    <br />
                                                                    <asp:Label ID="Label1" Font-Size="10px" Width="90%" runat="server" Enabled="false">Qty: <%#Eval("Remarks") %></asp:Label>
                                                                </li>
                                                            </ItemTemplate>
                                                        </asp:Repeater>
                                                    </td>
                                                </tr>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                        </table>
    <%--                                <asp:Repeater runat="server" ID="rptCompound" OnItemDataBound="Repeater1_ItemDataBound">
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
                        <tr style="border:solid 1px #cdced9">
                            <td style="background-color:#efefef;vertical-align:top;padding-top:10px;border-right:solid 1px #cdced9">
                                <label style="padding-left:10px;font-weight:bold"></label></td>
                                <td>
                                    <div style="padding-left:10px;padding-right:10px;padding-top:10px;padding-bottom:10px">
                                    <label style="font-size:11px;font-weight:bold;">Consumables </label>
                                        <label style="font-size:10px;font-weight:bold;font-style:italic;">(Alat Kesehatan)</label>
                                        <br />
                                    <asp:Label runat="server" ID="cons" Font-Size="11px" Text="No Consumable Prescription" />
                                    <div runat="server" id="divconsumables">
                                        <table>
                                        <tr class="bordertable" style="vertical-align:top">
                                            <td style="width:350px;padding-left:10px">
                                                <label Style="font-size:10px;font-weight:bold">Item</label>
                                            </td>
                                            <td style="width:50px">
                                                <label Style="font-size:10px;font-weight:bold">Qty</label>
                                            </td>
                                            <td style="width:40px">
                                                <label Style="font-size:10px;font-weight:bold">U.O.M</label>
                                            </td>
                                            <td style="width:240px">
                                                <label Style="font-size:10px;font-weight:bold">Dose & Instruction</label>
                                            </td>
                                        </tr>
                                    
                                        <asp:Repeater runat="server" ID="prescriptionconsumables" >
                                            <ItemTemplate>
                                                <tr class="bordertable" style="vertical-align:top">
                                                   <td style="width:350px;padding-left:10px">
                                                    <asp:Label ID="NameLabel" Style="font-size:10px" Width="90%" runat="server" Text='<%#Eval("salesItemName") %>' Enabled="false" />                                               
                                                   </td>
                                                    <td style="width:50px">
                                                        <asp:Label ID="Label1" Style="font-size:10px" Width="90%" runat="server" Enabled="false"><%#Eval("quantity","{0:G29}") %></asp:Label>
                                                    </td>
                                                    <td style="width:40px">
                                                        <asp:Label ID="Label2" Style="font-size:10px" Width="90%" runat="server" Enabled="false"><%#Eval("uom") %></asp:Label>
                                                    </td>
                                                    <td style="width:240px">
                                                        <asp:Label ID="Label4" Style="font-size:10px" Width="90%" runat="server" Enabled="false"><%#Eval("instruction") %></asp:Label>
                                                    </td>
                                                </tr> 
                                            </ItemTemplate>
                                        </asp:Repeater>
                                
                                    </table>
                                    </div>
                                </div>
                            
                            </td>
                        </tr>
                        <tr style="border:solid 1px #cdced9" runat="server" id="trAdditionalDrugs">
                            <td style="background-color:#efefef;vertical-align:top;padding-top:10px;border-right:solid 1px #cdced9">
                                <label style="padding-left:10px;font-weight:bold"></label></td>
                                <td>
                                    <div style="padding-left:10px;padding-right:10px;padding-top:10px;padding-bottom:10px">
                                    <label style="font-size:11px;font-weight:bold;">Additional Drugs </label>
                                        <label style="font-size:10px;font-weight:bold;font-style:italic;">(Tambahan Obat)</label><br />
                                    <asp:Label runat="server" ID="lbladditionalpres" Font-Size="11px" Text="No Additional Drugs Prescription" />
                                    <div runat="server" id="dvAdditionalPres">
                                        <table>
                                            <tr class="bordertable" style="vertical-align:top">
                                            <td style="width:200px;padding-left:10px">
                                                <label Style="font-size:10px;font-weight:bold">Item</label>
                                            </td>
                                            <td style="width:50px">
                                                <label Style="font-size:10px;font-weight:bold">Qty</label>
                                            </td>
                                            <td style="width:40px">
                                                <label Style="font-size:10px;font-weight:bold">U.O.M</label>
                                            </td>
                                            <td style="width:100px">
                                                <label Style="font-size:10px;font-weight:bold">Frequency</label>
                                            </td>
                                            <td style="width:50px">
                                                <label Style="font-size:10px;font-weight:bold">Dose</label>
                                            </td>
                                            <td style="width:100px">
                                                <label Style="font-size:10px;font-weight:bold">Dose UoM</label>
                                            </td>
                                            <td style="width:140px">
                                                <label Style="font-size:10px;font-weight:bold">Instruction</label>
                                            </td>
                                            <td style="width:80px">
                                                <label Style="font-size:10px;font-weight:bold">Route</label>
                                            </td>
                                            <td style="width:50px">
                                                <label Style="font-size:10px;font-weight:bold">Iter</label>
                                            </td>
                                            <td style="width:50px">
                                                <label Style="font-size:10px;font-weight:bold">Routine</label>
                                            </td>
                                        </tr>
                                    
                                        <asp:Repeater runat="server" ID="rptAdditionalDrugs" >
                                            <ItemTemplate>
                                                <tr class="bordertable" style="vertical-align:top">
                                                   <td style="width:200px;padding-left:10px">
                                                    <asp:Label ID="NameLabel" Style="font-size:10px" Width="90%" runat="server" Text='<%#Eval("salesItemName") %>' Enabled="false" />                                               
                                                   </td>
                                                    <td style="width:50px">
                                                        <asp:Label ID="Label1" Style="font-size:10px" Width="90%" runat="server" Enabled="false"><%#Eval("quantity","{0:G29}") %></asp:Label>
                                                    </td>
                                                    <td style="width:40px">
                                                        <asp:Label ID="Label2" Style="font-size:10px" Width="90%" runat="server" Enabled="false"><%#Eval("uom") %></asp:Label>
                                                    </td>
                                                    <td style="width:100px">
                                                        <asp:Label ID="Label3" Style="font-size:10px" Width="90%" runat="server" Enabled="false"><%#Eval("frequency") %></asp:Label>
                                                    </td>
                                                    <td style="width:50px">
                                                        <asp:Label ID="Label8" Style="font-size:10px" Width="90%" runat="server" Enabled="false"><%#Eval("dose","{0:G29}") %></asp:Label>
                                                    </td>
                                                    <td style="width:100px">
                                                        <asp:Label ID="Label9" Style="font-size:10px" Width="90%" runat="server" Enabled="false"><%#Eval("dose_uom") %></asp:Label>
                                                    </td>
                                                    <td style="width:140px">
                                                        <asp:Label ID="Label4" Style="font-size:10px" Width="90%" runat="server" Enabled="false"><%#Eval("instruction") %></asp:Label>
                                                    </td>
                                                    <td style="width:80px">
                                                        <asp:Label ID="Label5" Style="font-size:10px" Width="90%" runat="server" Enabled="false"><%#Eval("route") %></asp:Label>
                                                    </td>
                                                    <td style="width:50px">
                                                        <asp:Label ID="Label6" Style="font-size:10px" Width="90%" runat="server" Enabled="false"><%#Eval("iteration") %></asp:Label>
                                                    </td>
                                                    <td style="width:50px">
                                                        <asp:Label ID="Label7" Style="font-size:10px" Width="90%" runat="server" Enabled="false"><%#Eval("routine") %></asp:Label>
                                                    </td>
                                                </tr> 
                                            </ItemTemplate>
                                        </asp:Repeater>
                                
                                    </table>
                                    </div>
                                </div>
                            
                            </td>
                        </tr>

                        <%-- =============================================== RACIKAN Additional ==================================================== --%>
                        <tr style="border: solid 1px #cdced9" runat="server" ID="trAdditionalRacikan">
                            <td style="background-color:#efefef;vertical-align:top;padding-top:10px;border-right:solid 1px #cdced9">
                                <label style="padding-left: 13px; font-weight: bold; font-size: 13px"></label>
                            </td>
                            <td><div style="padding-left:10px;padding-right:10px;padding-top:10px;padding-bottom:10px">
                                <label style="font-size: 11px; font-weight: bold;">Additional Compounds </label>
                                <label style="font-size: 10px; font-weight: bold; font-style: italic;">(Racikan Tambahan)</label><br />

                                <asp:Label runat="server" ID="noracikanadd" Font-Size="11px" Text="No Additional Compound Prescription" />
                                <div style="padding-bottom: 5px" runat="server" id="divRacikanAdditional">
                                    <table>
                                        <tr class="bordertable" style="vertical-align: top">
                                            <td style="width: 200px; padding-left: 13px">
                                                <label style="font-size: 10px; font-weight: bold">Name</label>
                                            </td>
                                            <td style="width: 50px">
                                                <label style="font-size: 10px; font-weight: bold">Dose</label>
                                            </td>
                                            <td style="width: 100px">
                                                <label style="font-size: 10px; font-weight: bold">Dose UoM</label>
                                            </td>
                                            <td style="width: 100px">
                                                <label style="font-size: 10px; font-weight: bold">Frequency</label>
                                            </td>
                                            <td style="width: 80px">
                                                <label style="font-size: 10px; font-weight: bold">Route</label>
                                            </td>
                                            <td style="width: 190px">
                                                <label style="font-size: 10px; font-weight: bold">Instruction</label>
                                            </td>
                                            <td style="width: 50px">
                                                <label style="font-size: 10px; font-weight: bold">Qty</label>
                                            </td>
                                            <td style="width: 40px">
                                                <label style="font-size: 10px; font-weight: bold">U.O.M</label>
                                            </td>
                                            <td style="width: 50px">
                                                <label style="font-size: 10px; font-weight: bold">Iter</label>
                                            </td>
                                        </tr>

                                        <asp:Repeater runat="server" ID="RepeaterRAcikanAdd" OnItemDataBound="RepeaterRAcikanAdd_ItemDataBound">
                                            <ItemTemplate>
                                                <tr class="bordertable" style="vertical-align: top">
                                                    <td style="width: 200px; padding-left: 13px">
                                                        <asp:HiddenField ID="HF_racikanheaderid_add" runat="server" Value='<%#Eval("prescription_compound_header_id") %>' />
                                                        <asp:Label ID="racikanname" Style="font-size: 10px" Width="90%" runat="server" Text='<%#Eval("compound_name") %>' Enabled="false" />
                                                    </td>
                                                    <td style="width: 50px">
                                                        <asp:Label ID="racikandose" Style="font-size: 10px" Width="90%" runat="server" Enabled="false"><%#Eval("dose","{0:G29}") %></asp:Label>
                                                    </td>
                                                    <td style="width: 100px">
                                                        <asp:Label ID="racikandoseuom" Style="font-size: 10px" Width="90%" runat="server" Enabled="false"><%#Eval("dose_uom") %></asp:Label>
                                                    </td>
                                                    <td style="width: 100px">
                                                        <asp:Label ID="racikanfrequency" Style="font-size: 10px" Width="90%" runat="server" Enabled="false"><%#Eval("frequency_code") %></asp:Label>
                                                    </td>
                                                    <td style="width: 80px">
                                                        <asp:Label ID="racikanroute" Style="font-size: 10px" Width="90%" runat="server" Enabled="false"><%#Eval("administration_route_code") %></asp:Label>
                                                    </td>
                                                    <td style="width: 190px">
                                                        <asp:Label ID="racikaninstruction" Style="font-size: 10px" Width="90%" runat="server" Enabled="false"><%#Eval("administration_instruction") %></asp:Label>
                                                    </td>
                                                    <td style="width: 50px">
                                                        <asp:Label ID="racikanqty" Style="font-size: 10px" Width="90%" runat="server" Enabled="false"><%#Eval("quantity","{0:G29}") %></asp:Label>
                                                    </td>
                                                    <td style="width: 40px">
                                                        <asp:Label ID="racikanuom" Style="font-size: 10px" Width="90%" runat="server" Enabled="false"><%#Eval("uom_code") %></asp:Label>
                                                    </td>
                                                    <td style="width: 50px">
                                                        <asp:Label ID="racikaniter" Style="font-size: 10px" Width="90%" runat="server" Enabled="false"><%#Eval("iter") %></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr class="bordertable">
                                                <td colspan="4">
                                                    <table style="width:100%;">
                                                         <asp:Repeater runat="server" ID="RepeaterRacikanDetailAdd">
                                                             <HeaderTemplate>
                                                                 <tr>
                                                                     <td style="padding-left:13px;">
                                                                         <label style="font-size: 10px; font-weight: bold">Item</label>
                                                                     </td>
                                                                     <td>
                                                                         <label style="font-size: 10px; font-weight: bold">Dose</label>
                                                                     </td>
                                                                 </tr>
                                                             </HeaderTemplate>
                                                             <ItemTemplate>
                                                                 <tr>
                                                                     <td style="padding-left:13px;">
                                                                         <asp:Label ID="racikanname" Style="font-size: 10px" Width="90%" runat="server" Text='<%#Eval("item_name") %>' Enabled="false" />
                                                                     </td>
                                                                     <td>
                                                                        <asp:Label id="lbldose" runat="server" Font-Size="10px" Text='<%# Bind("dose") %>' style='<%# Eval("IsDoseText").ToString().ToLower() == "true" ? "display:none" : "display:inline" %>'></asp:Label>
                                                                        <asp:Label id="lbldoseuom" runat="server" Font-Size="10px" Text='<%# Bind("dose_uom_code") %>' style='<%# Eval("IsDoseText").ToString().ToLower() == "true" ? "display:none" : "display:inline" %>'></asp:Label>
                                                                        <asp:Label id="lbldosetext" runat="server" Font-Size="10px" Text='<%# Bind("dose_text") %>' style='<%# Eval("IsDoseText").ToString().ToLower() == "true" ? "display:inline" : "display:none" %>'></asp:Label>
                                                                     </td>
                                                                 </tr>
                                                             </ItemTemplate>
                                                         </asp:Repeater>
                                                    </table>
                                                </td>
                                                <td colspan="5" style="padding-left:13px; vertical-align:top;">
                                                    <label style="font-size: 10px; font-weight: bold">Instruction</label>
                                                    <br />
                                                    <asp:Label ID="Label10" Style="font-size: 10px" Width="90%" runat="server" Enabled="false"><%#Eval("administration_instruction") %></asp:Label>
                                                </td>
                                            </tr>
                                            </ItemTemplate>
                                        </asp:Repeater>

                                    </table>
                                </div>
                                </div>
                            </td>
                        </tr>

                        <tr style="border:solid 1px #cdced9" runat="server" id="trAdditionalConsumables">
                            <td style="background-color:#efefef;vertical-align:top;padding-top:10px;border-right:solid 1px #cdced9">
                                <label style="padding-left:10px;font-weight:bold"></label></td>
                                <td>
                                    <div style="padding-left:10px;padding-right:10px;padding-top:10px;padding-bottom:10px">
                                    <label style="font-size:11px;font-weight:bold;">Additional Consumables</label>
                                        <label style="font-size:10px;font-weight:bold;font-style:italic;">(Tambah Alat Kesehatan)</label><br />
                                    <asp:Label runat="server" ID="lbladditionalcons" Font-Size="11px" Text="No Consumable Prescription" />
                                    <div runat="server" id="dvAdditionalConsumables">
                                        <table>
                                        <tr class="bordertable" style="vertical-align:top">
                                            <td style="width:350px;padding-left:10px">
                                                <label Style="font-size:10px;font-weight:bold">Item</label>
                                            </td>
                                            <td style="width:50px">
                                                <label Style="font-size:10px;font-weight:bold">Qty</label>
                                            </td>
                                            <td style="width:40px">
                                                <label Style="font-size:10px;font-weight:bold">U.O.M</label>
                                            </td>
                                            <td style="width:240px">
                                                <label Style="font-size:10px;font-weight:bold">Dose & Instruction</label>
                                            </td>
                                        </tr>
                                    
                                        <asp:Repeater runat="server" ID="rptAdditionalConsumables" >
                                            <ItemTemplate>
                                                <tr class="bordertable" style="vertical-align:top">
                                                   <td style="width:350px;padding-left:10px">
                                                    <asp:Label ID="NameLabel" Style="font-size:10px" Width="90%" runat="server" Text='<%#Eval("salesItemName") %>' Enabled="false" />                                               
                                                   </td>
                                                    <td style="width:50px">
                                                        <asp:Label ID="Label1" Style="font-size:10px" Width="90%" runat="server" Enabled="false"><%#Eval("quantity","{0:G29}") %></asp:Label>
                                                    </td>
                                                    <td style="width:40px">
                                                        <asp:Label ID="Label2" Style="font-size:10px" Width="90%" runat="server" Enabled="false"><%#Eval("uom") %></asp:Label>
                                                    </td>
                                                    <td style="width:240px">
                                                        <asp:Label ID="Label4" Style="font-size:10px" Width="90%" runat="server" Enabled="false"><%#Eval("instruction") %></asp:Label>
                                                    </td>
                                                </tr> 
                                            </ItemTemplate>
                                        </asp:Repeater>
                                
                                    </table>
                                    </div>
                                </div>
                            
                            </td>
                        </tr>
                        <tr>
                            <td >

                            </td>
                            <td>
                                <table style="border-spacing:20px;border-collapse: separate;margin-left:9%;width:100%">
                                    <div class="row">
                                        <div style="margin-left:70%">
                                            <br />
                                            <br />
                                            <br />
                                            <br />
                                            <asp:Label runat="server" ID="lblNamaDokter"></asp:Label>
                                            <hr style="margin-top:0%;margin-bottom:0%"/>
                                            <asp:Label runat="server" ID="lblDokterKet" Text="Dokter(Physician)"></asp:Label>
                                        </div>
                                    </div>
                                </table>
                            </td>
                        </tr>
                    </table>
                    <div id="mydiv">
                        <table>
                            <tr width="100%">
                                <td>
                                    <asp:Label runat="server" Text="Printed By : " Font-Size="10px"></asp:Label>
                                    <asp:Label runat="server" ID="lblPrintedBy" Font-Size="10px"></asp:Label>
                                    <%--<br />--%>
                                    <asp:Label runat="server" Text="/"></asp:Label>
                                    <asp:Label runat="server" Text="Print Date :" Font-Size="10px"></asp:Label>
                                    <asp:Label runat="server" ID="lblPrintDate" Font-Size="10px"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:updatepanel>
        </div>
    </form>
</body>
</html>
