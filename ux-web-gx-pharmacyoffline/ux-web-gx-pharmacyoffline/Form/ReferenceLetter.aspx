<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ReferenceLetter.aspx.cs" Inherits="Form_ReferenceLetter" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Buat Surat Rujukan</title>
    <!-- Tell the browser to be responsive to screen width -->
    <meta content="width=device-width, initial-scale=1, maximum-scale=1, user-scalable=no" name="viewport" />
    <!-- Bootstrap 3.3.6 -->
    <link rel="stylesheet" href="~/Content/bootstrap/css/bootstrap.css" />

    <!-- Font Awesome -->
    <link rel="stylesheet" href="~/Content/font-awesome/css/font-awesome.css" />
    <!-- Ionicons -->
    <link rel="stylesheet" href="~/Content/ionicons/css/ionicons.css" />
    <!-- Datepicker -->
    <link rel="stylesheet" href="~/Content/plugins/datepicker/datepicker3.css" />
    <!-- DropDown -->
    <link rel="stylesheet" href="~/Content/plugins/select2/select2.min.css" />

    <link href="../favicon.ico" rel="shortcut icon" type="image/x-icon" />

    <link href="~/Content/bootstrap-select/css/bootstrap-select.css" rel="stylesheet"  type="text/css" />

    <script src="../Content/toast/sweetalert2.min.js"></script>
    <link rel="stylesheet" href="../Content/toast/sweetalert2.min.css" />

</head>
<body>
    <form id="form1" runat="server">

        <asp:ScriptManager ID="ScriptManager1" runat="server">
            <Scripts>
                <asp:ScriptReference Path="~/Content/plugins/jQuery/jQuery-2.2.0.min.js" />
                <asp:ScriptReference Path="~/Content/bootstrap/js/bootstrap.min.js" />
                <asp:ScriptReference Path="~/Content/plugins/datepicker/bootstrap-datepicker.js" />
                <asp:ScriptReference Path="~/Content/plugins/select2/select2.full.min.js" />
                <asp:ScriptReference Path="~/Content/bootstrap-select/js/bootstrap-select.js" />
            </Scripts>
        </asp:ScriptManager>

        <asp:UpdatePanel runat="server" ID="up"><ContentTemplate>
        <div style="padding:30px">
            <h2><b>Buat Surat Rujukan</b></h2>
            <asp:HiddenField ID="hfOrganizationId" runat="server" />
            <asp:HiddenField ID="hfAdmissionId" runat="server" />
            <asp:HiddenField ID="hfPatientId" runat="server" />
            <asp:HiddenField ID="hfEncounterId" runat="server" />
            <asp:HiddenField ID="hfUserId" runat="server" />
            <table class="form-group table">
                <tr>
                    <td class="form-group" style="width:180px">
                        <b>No Rekam Medis</b>
                    </td>
                    <td>
                        : <asp:Label CssClass="form-group" runat="server" ID="lblMrNo" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <b>No Registrasi</b>
                    </td>
                    <td>
                        : <asp:Label runat="server" ID="lblAdmissionNo" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <b>Nama Pasien</b>
                    </td>
                    <td>
                        : <asp:Label runat="server" ID="lblPatientName" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <b>Tanggal Lahir</b>
                    </td>
                    <td>
                        : <asp:Label runat="server" ID="lblBirthDate" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <b>Keluhan</b>
                    </td>
                    <td>
                        : <asp:Label runat="server" ID="lblChiefComplaintAnamnesis" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <b>Diagnosa</b>
                    </td>
                    <td>
                        : <asp:Label runat="server" ID="lblAssessment" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <b>Laboratory</b>
                    </td>
                    <td>
                        <asp:Repeater runat="server" ID="repeatLab">
                            <ItemTemplate>                                     
                                <li>
                                    <asp:Label runat="server" id="lblLabs" ><%# Eval("Name") %> </asp:Label>
                                </li>
                            </ItemTemplate>
                        </asp:Repeater>      
                    </td>
                </tr>
                <tr>
                    <td>
                        <b>Radiology</b>
                    </td>
                    <td>
                         <asp:Repeater runat="server" ID="repeatRad">
                            <ItemTemplate>
                                <li>
                                    <asp:Label runat="server" id="lblRads" ><%# Eval("Name") %> </asp:Label>
                                </li>
                            </ItemTemplate>
                        </asp:Repeater>   
                    </td>
                </tr>
                <tr>
                    <td>
                        <b>Others</b>
                    </td>
                    <td>
                        : <asp:Label runat="server" ID="lblOthers" />
                    </td>
                </tr>
            </table>
            <table class="form-group table">
                <tr style="vertical-align:top;">
                    <td class="form-group" style="width:180px">
                        <b>Rujukan Dokter</b>
                    </td>
                    <td>
                        <div style="padding-bottom:15px;">
                            <asp:RadioButton runat="server" AutoPostBack="true" ID="rdDoctor" Text=" Dokter" OnCheckedChanged="rdDoctor_CheckedChanged"/> &nbsp;&nbsp;&nbsp;
                            <asp:DropDownList runat="server" Visible="false" CssClass="form-control selectpicker" data-live-search="true" data-width="50%" ID="ddlDoctor">
                            </asp:DropDownList>
                        </div>
                        <div style="padding-bottom:15px;">
                            <asp:RadioButton runat="server" AutoPostBack="true" ID="rdSpesialis" Text=" Spesialis" OnCheckedChanged="rdSpesialis_CheckedChanged"/> &nbsp;&nbsp;&nbsp;
                            <asp:DropDownList runat="server" Visible="false" CssClass="form-control selectpicker" data-live-search="true" data-width="50%" ID="ddlSpesialis">
                            </asp:DropDownList>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td style="border-bottom: 1px solid #ddd;">
                        <b>Apakah pasien memiliki kondisi berikut</b>
                    </td>
                    <td style="border-bottom: 1px solid #ddd;">
                        <div style="padding-bottom:15px;">
                            <label>Pasien dengan penyakit infeksius (mis: TBC, Covid19)</label>
                            <br />
                            <asp:RadioButton ID="RBinfeksiusYes" runat="server" Text=" Yes" GroupName="infeksius" /> &nbsp;&nbsp;&nbsp;
                            <asp:RadioButton ID="RBinfeksiusNo" runat="server" Text=" No" GroupName="infeksius" />
                        </div>
                        <div style="padding-bottom:15px;">
                            <label>Pasien dengan immuno-compromised</label>
                            <br />
                            <asp:RadioButton ID="RBimmunoYes" runat="server" Text=" Yes" GroupName="immuno" /> &nbsp;&nbsp;&nbsp;
                            <asp:RadioButton ID="RBimmunoNo" runat="server" Text=" No" GroupName="immuno" />
                        </div>
                        <label style="font-weight:bold;">Pasien yang memiliki salah satu kondisi di atas TIDAK DIANJURKAN untuk pemeriksaan fisik lanjutan</label>
                    </td>
                </tr>
            </table>
            
            <table class="form-group table" style="display:none; visibility:hidden;">
                <tr>
                    <td style="width:15%">
                        Catatan Tambahan
                    </td>
                    <td style="width:55%">
                        <asp:TextBox runat="server" TextMode="MultiLine" MaxLength="5000" ID="txtRemarks" CssClass="form-control" Wrap="true" Height="200px"/>
                    </td>
                    <td style="width:30%"></td>
                </tr>
            </table>
            <div style="padding-top:15px; text-align:right;">
                <asp:Button runat="server" CssClass="btn btn-primary" Text="SIMPAN" ID="btnSubmit" OnClientClick="return Validate();" OnClick="btnSubmit_Click"/>
            </div>
            
        </div>
        </ContentTemplate></asp:UpdatePanel>

         <div class="modal fade" id="modalSendEmail" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
            <div class="modal-dialog" style="width: 80%; margin-top: 75px;" runat="server">
                <div class="modal-content" style="border-radius: 10px 10px !important;">
                    <div class="modal-header" style="height: 40px; padding-top: 10px; padding-bottom: 5px">
                        <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                        <h4 class="modal-title" style="text-align: left">
                            <asp:Label ID="Label5" Style="font-family: Arial, Helvetica, sans-serif;" runat="server" Text="Confirmation"></asp:Label></h4>
                    </div>
                    <div class="modal-body">
                        <asp:UpdatePanel ID="UpdatePanelPrescription" runat="server">
                            <ContentTemplate>    

                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </div>
            </div>
        </div>

    </form>

    <script>
        function showSuccess() {
            Swal.fire(
                'Submit Success!',
                'Thank You!',
                'success'
            )
        }

        $(document).ready(function () {
            $('.selectpicker').selectpicker();
            var prm = Sys.WebForms.PageRequestManager.getInstance();
            if (prm != null) {
                prm.add_endRequest(function (sender, e) {
                    if (sender._postBackSettings.panelsToUpdate != null) {
                        $('.selectpicker').selectpicker();
                    }
                });
            };
        });

        function Validate() {
            var rbrujukandokter = document.getElementById("<%=rdDoctor.ClientID%>");
            var rbrujukanspesialis = document.getElementById("<%=rdSpesialis.ClientID%>");

            var rbinfeksiusyes = document.getElementById("<%=RBinfeksiusYes.ClientID%>");
            var rbinfeksiusno = document.getElementById("<%=RBinfeksiusNo.ClientID%>");
            var rbimmunyes = document.getElementById("<%=RBimmunoYes.ClientID%>");
            var rbimmunno = document.getElementById("<%=RBimmunoNo.ClientID%>");

            if (rbrujukandokter.checked == false && rbrujukanspesialis.checked == false) {
                alert("Silakan Pilih Dokter atau Spesialis yang dirujuk!");
                return false;
            }

            if (rbinfeksiusyes.checked == false && rbinfeksiusno.checked == false) {
                alert("Silakan lengkapi kondisi pasien!");
                return false;
            }
            if (rbimmunyes.checked == false && rbimmunno.checked == false) {
                alert("Silakan lengkapi kondisi pasien!");
                return false;
            }

            if (rbinfeksiusyes.checked == true || rbimmunyes.checked == true) {
                var c = confirm("Pasien memiliki kondisi yang tidak dianjurkan untuk pemeriksaan fisik.\n\nApakah anda tetap ingin melanjutkan?");
                if (c == false) {
                    return false;
                }
            }
   

            <%--var rb1 = document.getElementById("<%=rdInfectious.ClientID%>");
            var rb2 = document.getElementById("<%=rdImmunoComp.ClientID%>");
            var radio1 = rb1.getElementsByTagName("input");
            var radio2 = rb2.getElementsByTagName("input");
            var value1 = "";
            var value2 = "";
            var isChecked1 = false;
            var isChecked2 = false;

            for (var i = 0; i < radio1.length; i++) {
                if (radio1[i].checked) {
                    isChecked1 = true;
                    value1 = radio1[i].value
                    break;
                }
            }
            for (var i = 0; i < radio2.length; i++) {
                if (radio2[i].checked) {
                    isChecked2 = true;
                    value2 = radio2[i].value
                    break;
                }
            }
            if (!isChecked1 && isChecked2) {
                alert("Silahkan Isi kondisi pasien");
                return false;
                break;
            }
            else {
                if (value1 != "0" || value2 != "0") {
                    $('#modalSendEmail').modal('show');
                    return false;
                }
            }
            --%>
            return true;
        }
    </script>
</body>
</html>
