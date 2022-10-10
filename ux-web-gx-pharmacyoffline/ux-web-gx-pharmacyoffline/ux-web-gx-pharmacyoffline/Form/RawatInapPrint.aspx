<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RawatInapPrint.aspx.cs" Inherits="Form_ReferralResumePrint" %>


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
            *{
                letter-spacing:0.38px;
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
                vertical-align:middle !important;
                    
                border-right: solid 1px #cdced9;
                vertical-align: top;
                padding-left: 20px;
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
            .otorisasi-item{
                border: solid 1px #cdced9; 
                background-color: #ffff;
                padding:5px;
                vertical-align:top;
                text-align:left;
            }


        </style>
 <div>
     <asp:UpdatePanel ID="up_inpatient" runat="server" UpdateMode="Conditional">
         <ContentTemplate>
           <asp:HiddenField runat="server" ID="hfpreviewpres" />
                             <%-- =============================================== START DATE SECTION ==================================================== --%>
                 <table style="width: 80%;">
                                <asp:Label runat="server" style="font-size: 20px; font-weight: bold;display:block">SURAT PENGANTAR RAWAT</asp:Label>
                                <asp:Label runat="server" style="font-size: 18px; font-weight: normal;display:block;font-style:italic">Addmision Referal Letter </asp:Label>
                                <asp:Label runat="server" style="font-size: 18px; font-weight: normal;display:block;margin-bottom:10px">Dari OPD</asp:Label>
                                <br />
                 

                            </table>
                            <%-- =============================================== END DATE SECTION ==================================================== --%>
                           <br />
                 <table style="width: 100%;margin-bottom:20px">
                                
                                <tbody>
                                    <tr style="border: solid 1px #cdced9; width: 100%;">
                                        <td style="width: 35%; background-color: #efefef;" class="itemtable-priview-title">
                                            <label style=" font-weight: bold; font-size: 14px;display:block">Dokter Penanggung Jawab Pelayanan</label>
                                            <label style=" font-weight: normal;font-style:italic; font-size: 13px;display:block">Primary Doctor</label>

                                        </td>
                                        <td style="width: 65%;" colspan="2" class="itemtable-priview">
                                            <asp:Label runat="server" ID="lbl_dokter" Style="font-size: 14px;" Text=""> </asp:Label>
                                        </td>
                                    </tr>
                                </tbody>
                                <tbody>
                                    <tr style="border: solid 1px #cdced9; width: 100%;">
                                        <td style="width: 35%; background-color: #efefef;" class="itemtable-priview-title">
                                            <label style=" font-weight: bold; font-size: 14px;display:block">Diagnosis</label>
                                            <label style=" font-weight: normal;font-style:italic; font-size: 13px;display:block">Diagnosis</label>

                                        </td>
                                        <td style="width: 65%;" colspan="2" class="itemtable-priview">
                                            <asp:Label runat="server" ID="lbl_diagnosis" Style="font-size: 14px;" Text="">Patient has fever for 3 days and also Stomach ache this morning he has thrown up more than 4 times he also got neck pain and swollen head.</asp:Label>
                                        </td>
                                    </tr>
                                </tbody>
                                 <tbody>
                                    <tr style="border: solid 1px #cdced9; width: 100%;">
                                        <td style="width: 35%; background-color: #efefef;" class="itemtable-priview-title">
                                            <label style=" font-weight: bold; font-size: 14px;display:block">Tanggal Masuk Rawat</label>
                                            <label style=" font-weight: normal;font-style:italic; font-size: 13px;display:block">Addmision Date</label>

                                        </td>
                                        <td style="width: 65%; padding: 0;" colspan="2" class="itemtable-priview">
                                            
                                            <table style="width: 100%;">
                                                <tr style="width: 100%;vertical-align:middle">
                                                     <td style="width: 50%;border:none;" class="itemtable-priview">
                                                        <asp:Label Font-Size="14px" Font-Bold="true" ID="lbl_addmision_date" runat="server" Text=""></asp:Label>
                                                        <br/>
                                                    </td>
                                                    <td style="width: 50%;" class="itemtable-priview">
                                                        <asp:Label Font-Size="14px" style="margin-right:20px" Font-Bold="true" ID="Label6" runat="server" Text="Waktu "></asp:Label>
                                                        <asp:Label Font-Size="14px" ID="lbl_addmision_time" runat="server" Text=""> </asp:Label>
                                                    </td>
                                                 </tr>
                                            </table>
                                            
                                        </td>
                                    </tr>
                                </tbody>
                                <tbody>
                                    <tr style="border: solid 1px #cdced9; width: 100%;">
                                        <td style="width: 35%; background-color: #efefef;" class="itemtable-priview-title">
                                            <label style=" font-weight: bold; font-size: 14px;display:block">Bangsal</label>
                                            <label style=" font-weight: normal;font-style:italic; font-size: 13px;display:block">Ward</label>

                                        </td>
                                        <td style="width: 65%;" colspan="2" class="itemtable-priview">
                                            <asp:Label runat="server" ID="lbl_ward" Style="font-size: 14px;" Text="">General </asp:Label>
                                        </td>
                                    </tr>
                                </tbody>

                                <tbody>
                                    <tr style="border: solid 1px #cdced9; width: 100%;">
                                        <td style="width: 35%; background-color: #efefef;" class="itemtable-priview-title">
                                            <label style=" font-weight: bold; font-size: 14px;display:block">Perkiraan Lama Rawat</label>
                                            <label style=" font-weight: normal;font-style:italic; font-size: 13px;display:block">Estimated Length of Stay</label>

                                        </td>
                                        <td style="width: 65%;" colspan="2" class="itemtable-priview">
                                            <asp:Label runat="server" ID="lbl_estimationday" Style="font-size: 14px;" Text=""> > 7 Hari </asp:Label>
                                        </td>
                                    </tr>
                                </tbody>
                                <tbody>
                                    <tr style="border: solid 1px #cdced9; width: 100%;">
                                        <td style="width: 35%; background-color: #efefef;" class="itemtable-priview-title">
                                            <label style=" font-weight: bold; font-size: 14px;display:block">Tindakan Operasi/Prosedur</label>
                                            <label style=" font-weight: normal;font-style:italic; font-size: 13px;display:block">Surgery/Procedure</label>

                                        </td>
                                        <td style="width: 65%;" colspan="2" class="itemtable-priview">
                                            <asp:Label runat="server" ID="lbl_istindakan" Style="font-size: 14px;" Text="">  </asp:Label>
                                        </td>
                                    </tr>
                                </tbody>

                                <tbody>
                                    <tr style="border: solid 1px #cdced9; width: 100%;">
                                        <td style="width: 35%; background-color: #efefef;" class="itemtable-priview-title">
                                            <label style=" font-weight: bold; font-size: 14px;display:block">Tanggal Perkiraan Operasi/TIndakan</label>
                                            <label style=" font-weight: normal;font-style:italic; font-size: 13px;display:block">Inpatient Care Instruction</label>

                                        </td>
                                        <td style="width: 65%; padding: 0;" colspan="2" class="itemtable-priview">
                                            
                                            <table style="width: 100%;">
                                                <tr style="width: 100%;vertical-align:middle">
                                                     <td style="width: 50%;border:none;" class="itemtable-priview">
                                                        <asp:Label Font-Size="14px" Font-Bold="true" ID="lbl_tglTindakan" runat="server" Text="-"></asp:Label>
                                                        <br/>
                                                    </td>
                                                    <td style="width: 50%;" class="itemtable-priview">
                                                        <asp:Label Font-Size="14px" style="margin-right:20px" Font-Bold="true" ID="Label9" runat="server" Text="Waktu "></asp:Label>
                                                        <asp:Label Font-Size="14px" ID="lbl_waktuTindakan" runat="server" Text=""> </asp:Label>
                                                    </td>
                                                 </tr>
                                            </table>
                                            
                                        </td>
                                    </tr>
                                </tbody>
                                <tbody>
                                    <tr style="border: solid 1px #cdced9; width: 100%;">
                                        <td style="width: 35%; background-color: #efefef;" class="itemtable-priview-title">
                                            <label style=" font-weight: bold; font-size: 14px;display:block">Nama Operasi</label>
                                            <label style=" font-weight: normal;font-style:italic; font-size: 13px;display:block">Name of Surgery</label>

                                        </td>
                                        <td style="width: 65%;" colspan="2" class="itemtable-priview">
                                            
                                            <asp:Label runat="server" ID="lbl_namaoperasi_no" Text="Tidak Ada"></asp:Label>
                                            <ul style="list-style-type: circle;padding-left:20px">
                                            <asp:Repeater ID="rpt_namaoperasi" runat="server">
                                                <ItemTemplate>             
                                                     <li><%# Eval("procedure_name") %></li>
                                                </ItemTemplate>
                                            </asp:Repeater>
                                        
                                        </td>
                                    </tr>
                                </tbody>
                                <tbody>
                                    <tr style="border: solid 1px #cdced9; width: 100%;">
                                        <td style="width: 35%; background-color: #efefef;" class="itemtable-priview-title">
                                            <label style=" font-weight: bold; font-size: 14px;display:block"> Lama Operasi/ Tindakan</label>
                                            <label style=" font-weight: normal;font-style:italic; font-size: 13px;display:block">Length of Surgery / Procedure</label>

                                        </td>
                                        <td style="width: 65%;" colspan="2" class="itemtable-priview">
                                            <asp:Label runat="server" ID="lbl_jamoperasi" Style="font-size: 14px;" Text=""> </asp:Label> <label style="font-size: 14px;margin-right:70px"> Jam</label>
                                            
                                             <asp:Label runat="server" ID="lbl_menitoperasi" Style="font-size: 14px;" Text=""></asp:Label> <label style="font-size: 14px;"> Menit</label>

                                        </td>
                                    </tr>
                                </tbody>
                                 <tbody>
                                    <tr style="border: solid 1px #cdced9; width: 100%;">
                                        <td style="width: 35%; background-color: #efefef;" class="itemtable-priview-title">
                                            <label style=" font-weight: bold; font-size: 14px;display:block"> Metode Anaestesi</label>
                                            <label style=" font-weight: normal;font-style:italic; font-size: 13px;display:block">Anesthetic Method</label>

                                        </td>
                                        <td style="width: 65%;" colspan="2" class="itemtable-priview">
                                            <asp:Label runat="server" ID="lbl_anesteticmethod" Style="font-size: 14px;" Text="-">Regional</asp:Label>
                                        </td>
                                    </tr>
                                </tbody>
                                 <tbody>
                                    <tr style="border: solid 1px #cdced9; width: 100%;">
                                        <td style="width: 35%; background-color: #efefef;" class="itemtable-priview-title">
                                            <label style=" font-weight: bold; font-size: 14px;display:block"> Alat</label>
                                            <label style=" font-weight: normal;font-style:italic; font-size: 13px;display:block">instrument</label>

                                        </td>
                                        <td style="width: 65%;" colspan="2" class="itemtable-priview">
                                            
                                            <asp:Label runat="server" ID="lbl_alat" Style="font-size: 14px;" Text="">LOremipsummmm</asp:Label>
                                        </td>
                                    </tr>
                                </tbody>
                                <tbody>
                                    <tr style="border: solid 1px #cdced9; width: 100%;">
                                        <td style="width: 35%; background-color: #efefef;" class="itemtable-priview-title">
                                            <label style=" font-weight: bold; font-size: 14px;display:block">Tabel Kategori</label>
                                            <label style=" font-weight: normal;font-style:italic; font-size: 13px;display:block">Table Category</label>

                                        </td>
                                        <td style="width: 65%;" colspan="2" class="itemtable-priview">
                                            <asp:Label runat="server" ID="lbl_tabelKategori" Style="font-size: 14px;" Text="-"></asp:Label>
                                        </td>
                                    </tr>
                                </tbody>
                                <tbody>
                                    <tr style="border: solid 1px #cdced9; width: 100%;">
                                        <td style="width: 35%; background-color: #efefef;"  class="itemtable-priview-title">
                                            <label style=" font-weight: bold; font-size: 14px;display:block">Setelah Operasi/Tindakan</label>
                                            <label style=" font-weight: normal;font-style:italic; font-size: 13px;display:block">Post Surgery/Procedure</label>

                                        </td>
                                        <td style="width: 65%;" colspan="2" class="itemtable-priview">
                                            <asp:Label runat="server" ID="lbl_recoveryroom" Style="font-size: 14px;" Text="">ICU</asp:Label>
                                        </td>
                                    </tr>
                                </tbody>
                                <tbody>
                                    <tr style="border: solid 1px #cdced9; width: 100%;" >
                                        <td style="width: 35%; background-color: #ffffff;" colspan="4" class="itemtable-priview-title">
                                            <label style=" font-weight: bold; font-size: 14px;display:block">Persiapan Operasi/Tindakan</label>
                                            <label style=" font-weight: normal;font-style:italic; font-size: 13px;display:block">Surgery/Procedure Preparation</label>
                                        </td>
                                    </tr>
                                </tbody>
                                <tbody>
                                    <tr style="border: solid 1px #cdced9; width: 100%;">
                                        <td style="width: 35%; background-color: #efefef;" class="itemtable-priview-title">
                                            <label style=" font-weight: bold; font-size: 14px;display:block">Puasa</label>
                                            <label style=" font-weight: normal;font-style:italic; font-size: 13px;display:block">Fasting</label>

                                        </td>
                                        <td style="width: 65%;" colspan="2" class="itemtable-priview">
                                            <asp:Label runat="server" ID="lbl_fasting" Style="font-size: 14px;" Text=""></asp:Label>
                                        </td>
                                    </tr>
                                </tbody>
                                <tbody>
                                    <tr style="border: solid 1px #cdced9; width: 100%;">
                                        <td style="width: 35%; background-color: #efefef;" class="itemtable-priview-title">
                                            <label style=" font-weight: bold; font-size: 14px;display:block">Radiologi</label>
                                            <label style=" font-weight: normal;font-style:italic; font-size: 13px;display:block">Radiology</label>

                                        </td>
                                        <td style="width: 65%;" colspan="2" class="itemtable-priview">
                                            <asp:Label runat="server" ID="lbl_isRadiologi" Style="font-size: 14px;" Text=""></asp:Label>
                                        </td>
                                    </tr>
                                </tbody>
                                 <tbody>
                                    <tr style="border: solid 1px #cdced9; width: 100%;">
                                        <td style="width: 35%; background-color: #efefef;" class="itemtable-priview-title">
                                            <label style=" font-weight: bold; font-size: 14px;display:block">Laboratorium</label>
                                            <label style=" font-weight: normal;font-style:italic; font-size: 13px;display:block">Laboratory</label>

                                        </td>
                                        <td style="width: 65%;" colspan="2" class="itemtable-priview">
                                            <asp:Label runat="server" ID="lbl_isLabo" Style="font-size: 14px;" Text=""></asp:Label>
                                        </td>
                                    </tr>
                                </tbody>

                                </tbody>
                                <tbody>
                                    <tr style="border: solid 1px #cdced9; width: 100%;">
                                        <td style="width: 35%; background-color: #efefef;" class="itemtable-priview-title">
                                            <label style=" font-weight: bold; font-size: 14px;display:block">Intruksi Rawat Inap</label>
                                            <label style=" font-weight: normal;font-style:italic; font-size: 13px;display:block">hospitalization instructions</label>
                                            
                                        </td>
                                        <td style="width: 65%;" colspan="2" class="itemtable-priview">
                                            <asp:Label runat="server" ID="lbl_instruksi" Style="font-size: 14px;" Text="" > </asp:Label>
                                        </td>
                                    </tr>
                                </tbody>
                                <tbody>
                                    <tr style="border: solid 1px #cdced9; width: 100%;">
                                        <td style="width: 35%; background-color: #efefef;" class="itemtable-priview-title">
                                            <label style=" font-weight: bold; font-size: 14px;display:block">Jenis Pembayaran</label>
                                            <label style=" font-weight: normal;font-style:italic; font-size: 13px;display:block">Payment Option</label>

                                        </td>
                                        <td style="width: 65%; padding: 0;" colspan="2" class="itemtable-priview">
                                            
                                            <table style="width: 100%;">
                                                <tr style="width: 100%;">
                                                     <td style="width: 20%;border:none; vertical-align:middle;padding-left:30px" class="itemtable-priview" >
                                                        <asp:Checkbox ID="tb_selfpayment"  runat="server" Checked="true"></asp:Checkbox>
                                                        <asp:Label Font-Size="14px"  ID="Label27" runat="server" Text="Pribadi"></asp:Label>
                                                        <asp:Label Font-Size="12px" style="display:block;font-style:italic; margin-right:20px" ID="Label32" runat="server" Text="Self Payment"></asp:Label>
                                                    </td>
                                                    <td style="width: 20%;border:none; vertical-align:middle;padding-left:30px" class="itemtable-priview" >
                                                        <asp:Checkbox ID="Checkbox2"  runat="server" Checked="true"></asp:Checkbox>
                                                        <asp:Label Font-Size="14px"  ID="Label33" runat="server" Text="Asuransi/Perusahaan"></asp:Label>
                                                        <asp:Label Font-Size="12px" style="display:block;font-style:italic; margin-right:20px" ID="Label34" runat="server" Text=" Insurance/Corporate"></asp:Label>
                                                    </td>
                                                    <td style="width: 20%;border:none; vertical-align:middle;padding-left:30px" class="itemtable-priview" >
                                                        <asp:Checkbox ID="Checkbox1"  runat="server" Checked="true"></asp:Checkbox>
                                                        <asp:Label Font-Size="14px"  ID="Label28" runat="server" Text="Jaminan Pemerintah"></asp:Label>
                                                        <asp:Label Font-Size="12px" style="display:block;font-style:italic; margin-right:20px" ID="Label29" runat="server" Text=" Goverment"></asp:Label>
                                                    </td>
                                                    
                                                 </tr>
                                            </table>
                                            
                                        </td>
                                    </tr>
                                </tbody>
                            </table>


                    <br />
                    
                    <label style="margin-top:40px">Tanggal</label> <asp:Label ID="lbl_tanggal" runat="server" style="margin-right:30px" Text=" "></asp:Label>
                    <label>Jam</label> <asp:Label ID="lbl_jam" style="margin-bottom:100px" runat="server" Text=""></asp:Label>
                             <%-- =============================================== START SIGN ==================================================== --%>
                            <table style="width: 100%;margin-top:100px">
                                <tbody>
                                    <tr style="width: 100%">
                                        <td  style="width: 35%;">
             
                                            <asp:Label runat="server" ID="lbl_dokterttd" style="margin-bottom:10px"  Text=""></asp:Label>
                                            <hr style=" width:70%;margin-left:0px; margin-top: 10px; margin-bottom: 10px;border-top:1px solid #000000" />
                                            <asp:Label runat="server" ID="Label30" Text="Dokter (Physician)"></asp:Label>


                                        </td>
                                        <td style="width:40%"></td>
                                        <td  style="width: 35%;">
                                            
                                            
                                            <asp:Label runat="server" style="margin-bottom:10px" ID="lbl_patient"  Text=""></asp:Label>
                                            <hr style=" width:100%;margin-left:0px; margin-top: 10px; margin-bottom: 10px;border-top:1px solid #000000" />
                                            <asp:Label runat="server"  ID="Label22" Text="Tanda Tangan Pasien dan Nama Lengkap"></asp:Label>
                                            <asp:Label runat="server" style="display:block" ID="Label1" Text="Patient's Signature and Full Name"></asp:Label>

                                        </td>
                                    </tr>

                                </tbody>
                                
                            </table>
                            <table>
                                <tbody>
                                    <tr style="width:100%">
                                        <td style="width:100%">
                                            <asp:Label runat="server" style="margin-top:5px;display:block" ID="Label23" Text="Tanda tangan digital berlaku sebagai pengganti tandatangan basah"></asp:Label>

                                        </td>

                                    </tr>

                                </tbody>    
                            </table>

                             <table style="width: 100%;margin-top:20px">
                                <tbody>
                                    <tr style="width: 100%">
                                        <td  style="width: 35%;">
                                            <label style="font-weight:bold;font-size:14px">Persiapan Operasi/Tindakan</label>
                                            <label style="font-weight:normal;font-size:13px;font-style:italic;display:block">Surgery/Procedure Preparation</label>
                                        </td>
                                        <td style="width:40%"></td>
                                        <td  style="width: 35%;">
                                           <label style="font-weight:bold;font-size:14px"> Tanggal Dibuat (Order Date)</label>
                                           <asp:label ID="lbl_orderdatelab"  runat="server" Text="" style="display:block;font-weight:normal;font-size:13px;font-style:italic"></asp:label>
                                        
                                        </td>
                                    </tr>
                                </tbody>
                            </table>


                             
                            <table style="width:100%;margin-top:20px">
                                <tbody >
                                    <tr style="">
                                        <td style="text-align:left">
                                            <label style="font-weight:bold;font-size:14px">Diagnosa Klinis ( Clinical Diagnosis) :</label>
                                              <asp:Label style="display:block" ID="lbl_diagnosaklinis_lab" runat="server" Text="J001.91 COMMON COLD 100 Cholera"></asp:Label>
                                         </td>
                                    </tr>
                                </tbody>    
                            </table>
                            <table style="margin-top:25px;width" >
                                <tbody>
                                    <tr>
                                        <td>
                                            <asp:Label style="display:block;font-weight:bold;font-size:14px" ID="Label31" runat="server" Text="Status Kehamilan (Pregnancy Status) : "></asp:Label>
                                            <asp:Label style="display:block" ID="lbl_pregnancy" runat="server" Text="-"></asp:Label>
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                            <asp:Label style="display:block;font-weight:bold;font-size:14px;letter-spacing:0.38px" ID="Label38" runat="server" Text="Order Laboratory (Laboratory Order) :"></asp:Label>
                            
                            <table style="width:80%; margin-top:25px">
                              
                                <tbody>
                                    <tr style="border: solid 1px #cdced9; width: 100%;">
                                        <td style="width: 35%; background-color: #ffff;" class="itemtable-priview-title">
                                            <label style=" font-weight: bold; font-size: 14px;display:block">Order</label>
                                            
                                        </td>
                                        <td style="width: 65%;" colspan="2" class="itemtable-priview">
                                            <label style=" font-weight: bold; font-size: 14px;display:block">Detail</label>
                                        </td>
                                    </tr>
                                </tbody>
                                <tbody>
                                    <tr style="border: solid 1px #cdced9; width: 100%;">
                                        <td style="width: 35%; background-color: #ffff;" class="itemtable-priview-title">
                                            <label  style=" font-weight: bold; font-size: 14px;display:block"> CITO</label>
                                            
                                        </td>
                                        <td style="width: 65%;" colspan="2" class="itemtable-priview">
                                            <asp:Label runat="server" ID="lbl_cito_empty" Text="(tidak ada order)"></asp:Label>
                                            <ul style="list-style-type: circle;padding-left:20px">
                                            <asp:Repeater ID="rpt_cito" runat="server">
                                                <ItemTemplate>             
                                                     <li><%# Eval("name") %></li>
                                                </ItemTemplate>
                                            </asp:Repeater>
                                        </td>
                                    </tr>
                                </tbody>
                                
                                <tbody>
                                    <tr style="border: solid 1px #cdced9; width: 100%;">
                                        <td style="width: 35%; background-color: #ffff;" class="itemtable-priview-title">
                                            <label  style=" font-weight: bold; font-size: 14px;display:block"> Clinical Patholgy</label>
                                            
                                        </td>
                                        <td style="width: 65%;" colspan="2" class="itemtable-priview">
                                            <asp:Label runat="server" ID="lbl_chlinicalpathology_no" Text="(tidak ada order)"></asp:Label>
                                            <ul style="list-style-type: circle;padding-left:20px">
                                            <asp:Repeater ID="rpt_chlinicalpathology" runat="server">
                                                <ItemTemplate>
                                                  <li><%# Eval("name") %></li>
                                                </ItemTemplate>

                                            </asp:Repeater>
                                            </ul>
                                        </td>
                                    </tr>
                                </tbody>
                                <tbody>
                                    <tr style="border: solid 1px #cdced9; width: 100%;">
                                        <td style="width: 35%; background-color: #ffff;" class="itemtable-priview-title">
                                            <label  style=" font-weight: bold; font-size: 14px;display:block"> Microbiology</label>
                                            
                                        </td>
                                        <td style="width: 65%;" colspan="2" class="itemtable-priview">
                                             <asp:Label runat="server" ID="lbl_Microbiology_no" Text="(tidak ada order)"></asp:Label>
                                            <ul style="list-style-type: circle;padding-left:20px">

                                            <asp:Repeater ID="rpt_Microbiology" runat="server">
                                                <ItemTemplate>
                                                  <li><%# Eval("name") %></li>
                                                </ItemTemplate>

                                            </asp:Repeater>
                                            </ul>

                                        </td>
                                    </tr>
                                </tbody>
                                <tbody>
                                    <tr style="border: solid 1px #cdced9; width: 100%;">
                                        <td style="width: 35%; background-color: #ffff;" class="itemtable-priview-title">
                                            <label  style=" font-weight: bold; font-size: 14px;display:block"> Anatomical Pathology</label>
                                            
                                        </td>
                                        <td style="width: 65%;" colspan="2" class="itemtable-priview">
                                            <asp:Label runat="server" ID="lbl_anatomicalno" Text="(tidak ada order)"></asp:Label>
                                            <ul style="list-style-type: circle;padding-left:20px">
                                            <asp:Repeater ID="rpt_AnatomicalPathology" runat="server">
                                                <ItemTemplate>
                                                  <li><%# Eval("name") %></li>
                                                </ItemTemplate>

                                            </asp:Repeater>
                                            </ul>
                                        </td>
                                    </tr>
                                </tbody>
                                <tbody>
                                    <tr style="border: solid 1px #cdced9; width: 100%;">
                                        <td style="width: 35%; background-color: #ffff;" class="itemtable-priview-title">
                                            <label  style=" font-weight: bold; font-size: 14px;display:block"> MRI 3 Tesla</label>
                                            
                                        </td>
                                         <td style="width: 65%;" colspan="2" class="itemtable-priview">
                                            <asp:Label runat="server" ID="lbl_mri3teslano" Text="(tidak ada order)"></asp:Label>

                                            <ul style="list-style-type: circle;padding-left:20px">
                                            <asp:Repeater ID="l_mri3tesla" runat="server">
                                                <ItemTemplate>
                                                  <li><%# Eval("name") %></li>
                                                </ItemTemplate>

                                            </asp:Repeater>
                                            </ul>

                                        </td>
                                    </tr>
                                </tbody>
                                <tbody>
                                    <tr style="border: solid 1px #cdced9; width: 100%;">
                                        <td style="width: 35%; background-color: #ffff;" class="itemtable-priview-title">
                                            <label  style=" font-weight: bold; font-size: 14px;display:block"> Lain-lain</label>
                                            
                                        </td>
                                        <td style="width: 65%;" colspan="2" class="itemtable-priview">
                                            <asp:label ID="txt_listlaborder" runat="server" style=" font-weight: normal; font-size: 14px;display:block" >(tidak ada order)</asp:label>
                                        </td>
                                    </tr>
                                </tbody>
                                
                            </table>    
             
                            <table style="width:100%;margin-top:100px">
                                <tbody>
                                    <tr style=" width: 100%;">
                                        <td> <asp:label ID="Label36" runat="server" style=" font-weight: bold; font-size: 14px;display:block" Text="Catatan:"></asp:label></td>
                                    </tr>
                                </tbody>
                            </table>

                           <table style="width: 100%;margin-top:100px">
                                <tbody>
                                    <tr style="width: 100%">
                                        <td style="width: 35%;">
                                            <label style="font-weight:bold;font-size:14px">Keterangan</label>
                                            <asp:label ID="lbl_puasa1" runat="server" style="font-weight:normal;font-size:13px;font-style:italic;display:block" Text="* Puasa 10-12 jam"></asp:label>
                                            <asp:label ID="lbl_puasa2" runat="server" style="font-weight:normal;font-size:13px;font-style:italic;display:block" Text="** Puasa 12-14 jam"></asp:label>

                                        </td>
                                        <td style="width:45%"></td>
                                        <td  style="width: 35%;">
                                           <asp:label ID="lbl_dokterlaborder"  runat="server" Text="LNL DOKTER2" style="display:block;font-weight:normal;font-size:13px;"></asp:label>
                                           <hr style=" width:100%;margin-left:0px; margin-top: 10px; margin-bottom: 10px;border-top:1px solid #000000" />
                                            <label style="font-size:13px;font-style:italic">Dokter (Physician)</label>

                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                            <table style="width:30%;margin-top:50px;border: solid 1px #cdced9;">
                                
                                <tbody>
                                    <tr style="border: solid 1px #cdced9;">
                                        <td colspan="4" class="otorisasi-item" style="text-align:left;width:33%">
                                            <label style=" font-weight: bold; font-size: 14px;display:block;text-align:center"> OTORISASI PETUGAS</label>
                                        </td>
                                    </tr>
                                    <tr style="border: solid 1px #cdced9;text-align:left">
                                        <td  class="otorisasi-item" style="text-align:left">
                                            <label style=" font-weight: bold; font-size: 14px;display:block"> Tugas</label>
                                        </td>
                                        <td  class="otorisasi-item">
                                            <label style=" font-weight: bold; font-size: 14px;display:block;text-align:center"> Nama/Paraf</label>
                                        </td>
                                        <td  class="otorisasi-item">
                                            <label style=" font-weight: bold; font-size: 14px;display:block"> Tgl/Jam</label>
                                        </td>
                                    </tr>
                                    
                                    <tr style="border: solid 1px #cdced9; width: 100%;text-align:left">
                                        <td  class="otorisasi-item" style="text-align:left" >
                                            <label style=" font-size: 14px;display:block"> Phlebotomist</label>
                                        </td>
                                        <td  lass="otorisasi-item" >
                                            <asp:label id="lbl_nama_phlebotomist" runat="server" style="  font-size: 14px;display:block"> </asp:label>
                                        </td>
                                        <td class="otorisasi-item">
                                            <asp:label id="lbl_tgl_phlebotomist" runat="server" style="  font-size: 14px;display:block"> </asp:label>
                                        </td>
                                    </tr>
                                    <tr style="border: solid 1px #cdced9; width: 100%;text-align:left">
                                        <td  class="otorisasi-item" >
                                            <label style=" font-size: 14px;display:block"> Distribusi Sampel</label>
                                        </td>
                                        <td class="otorisasi-item" >
                                            <asp:label id="lbl_namadistribusisampel" runat="server" style="  font-size: 14px;display:block"> </asp:label>
                                        </td>
                                        <td  class="otorisasi-item">
                                            <asp:label id="lbl_tgl_distribusisampel" runat="server" style="font-size: 14px;display:block"> </asp:label>
                                        </td>
                                    </tr>
                                    <tr style="border: solid 1px #cdced9; width: 100%;text-align:left">
                                        <td  class="otorisasi-item" >
                                            <label style=" font-size: 14px;display:block"> Print Hasil</label>
                                        </td>
                                        <td  class="otorisasi-item">
                                            <asp:label id="lbl_namaprinthasil" runat="server" style="font-size: 14px;display:block"> </asp:label>
                                        </td>
                                        <td  class="otorisasi-item">
                                            <asp:label id="lbl_tglprinthasil" runat="server" style="font-size: 14px;display:block"> </asp:label>
                                        </td>
                                    </tr>
                                    <tr style="border: solid 1px #cdced9; width: 100%;">
                                        <td  class="otorisasi-item" >
                                            <label style=" font-size: 14px;display:block"> Cek Hasil</label>
                                        </td>
                                        <td  class="otorisasi-item">
                                            <asp:label id="lbl_namacekhasil" runat="server" style="font-size: 14px;display:block"> </asp:label>
                                        </td>
                                        <td  class="otorisasi-item">
                                            <asp:label id="lbl_tglcheckhasil" runat="server" style="font-size: 14px;display:block"> </asp:label>
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                            <table style="width: 100%;margin-top:20px">
                                <tbody>
                                    <tr style="width: 100%">
                                        <td  style="width: 35%;">
                                            <label style="font-weight:bold;font-size:14px">Persiapan Operasi/Tindakan</label>
                                            <label style="font-weight:normal;font-size:13px;font-style:italic;display:block">Surgery/Procedure Preparation</label>
                                        </td>
                                        <td style="width:40%"></td>
                                        <td  style="width: 35%;">
                                           <label style="font-weight:bold;font-size:14px"> Tanggal Dibuat (Order Date)</label>
                                           <asp:label ID="lbl_orderdatrad"  runat="server" Text="" style="display:block;font-weight:normal;font-size:13px;font-style:italic"></asp:label>
                                        
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                            <table style="width:100%;margin-top:20px">
                                <tbody >
                                    <tr style="">
                                        <td style="text-align:left">
                                            <label style="font-weight:bold;font-size:14px">Diagnosa Klinis ( Clinical Diagnosis) :</label>
                                              <asp:Label style="display:block" ID="lbl_diagnosaklinis_rad" runat="server" Text="J001.91 COMMON COLD 100 Cholera"></asp:Label>
                                         </td>
                                    </tr>
                                </tbody>    
                            </table>
                            <table style="margin-top:25px">
                                <tbody>
                                    <tr>
                                        <td>
                                            <asp:Label style="display:block;font-weight:bold;font-size:14px" ID="Label44" runat="server" Text="Status Kehamilan (Pregnancy Status) :"></asp:Label>
                                            <asp:Label style="display:block" ID="lbl_pregnancyrad" runat="server" Text="-"></asp:Label>
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                            <asp:Label style="display:block;font-weight:bold;font-size:14px;letter-spacing:0.38px" ID="Label46" runat="server" Text="Order Radiology (Radiology Order) :"></asp:Label>
                            
                            <table style="width:100%; margin-top:25px">
                              
                                <tbody>
                                    <tr style="border: solid 1px #cdced9; width: 100%;">
                                        <td style="width: 35%; background-color: #ffff;" class="itemtable-priview-title">
                                            <label style=" font-weight: bold; font-size: 14px;display:block">Order</label>
                                            
                                        </td>
                                        <td style="width: 65%;" colspan="2" class="itemtable-priview">
                                            <label style=" font-weight: bold; font-size: 14px;display:block">Detail</label>
                                        </td>
                                    </tr>
                                </tbody>
                                <tbody>
                                    <tr style="border: solid 1px #cdced9; width: 100%;">
                                        <td style="width: 35%; background-color: #ffff;" class="itemtable-priview-title">
                                            <label  style=" font-weight: bold; font-size: 14px;display:block"> X Ray</label>
                                            
                                        </td>
                                        <td style="width: 65%;" colspan="2" class="itemtable-priview">
                                            <asp:label ID="lbl_radxray_no" runat="server" style=" font-weight: normal; font-size: 14px;display:block">(tidak ada order)</asp:label>
                                            <ul style="list-style-type: circle;padding-left:20px">
                                            <asp:Repeater ID="rpt_xray" runat="server">
                                                <ItemTemplate>
                                                  <li><%# Eval("name") %></li>
                                                </ItemTemplate>

                                            </asp:Repeater>
                                            </ul>

                                        </td>
                                    </tr>
                                </tbody>

                                <tbody>
                                    <tr style="border: solid 1px #cdced9; width: 100%;">
                                        <td style="width: 35%; background-color: #ffff;" class="itemtable-priview-title">
                                            <label  style=" font-weight: bold; font-size: 14px;display:block"> USG</label>
                                            
                                        </td>
                                        <td style="width: 65%;" colspan="2" class="itemtable-priview">
                                            <asp:label ID="lbl_radusg_no" runat="server" style=" font-weight: normal; font-size: 14px;display:block">(tidak ada order)</asp:label>
                                            <ul style="list-style-type: circle;padding-left:20px">
                                            <asp:Repeater ID="rpt_usg" runat="server">
                                                <ItemTemplate>
                                                  <li><%# Eval("name") %></li>
                                                </ItemTemplate>

                                            </asp:Repeater>
                                            </ul>
                                        </td>
                                    </tr>
                                </tbody>
                                <tbody>
                                    <tr style="border: solid 1px #cdced9; width: 100%;">
                                        <td style="width: 35%; background-color: #ffff;" class="itemtable-priview-title">
                                            <label  style=" font-weight: bold; font-size: 14px;display:block"> CT</label>
                                            
                                        </td>
                                        <td style="width: 65%;" colspan="2" class="itemtable-priview">
                                            <asp:label ID="lbl_radct_no" runat="server" style=" font-weight: normal; font-size: 14px;display:block">(tidak ada order)</asp:label>
                                            <ul style="list-style-type: circle;padding-left:20px">
                                            <asp:Repeater ID="rpt_ct" runat="server">
                                                <ItemTemplate>
                                                  <li><%# Eval("name") %></li>
                                                </ItemTemplate>

                                            </asp:Repeater>
                                            </ul>
                                        </td>
                                    </tr>
                                </tbody>
                                <tbody>
                                    <tr style="border: solid 1px #cdced9; width: 100%;">
                                        <td style="width: 35%; background-color: #ffff;" class="itemtable-priview-title">
                                            <label  style=" font-weight: bold; font-size: 14px;display:block"> MRI 1,5 Tesla</label>
                                            
                                        </td>
                                        <td style="width: 65%;" colspan="2" class="itemtable-priview">
                                            <asp:label ID="lbl_mr15tesla_no" runat="server" style=" font-weight: normal; font-size: 14px;display:block">(tidak ada order)</asp:label>
                                            <ul style="list-style-type: circle;padding-left:20px">
                                            <asp:Repeater ID="rpt_mr15tesla" runat="server">
                                                <ItemTemplate>
                                                  <li><%# Eval("name") %></li>
                                                </ItemTemplate>

                                            </asp:Repeater>
                                            </ul>

                                        </td>
                                    </tr>
                                </tbody>
                                <tbody>
                                    <tr style="border: solid 1px #cdced9; width: 100%;">
                                        <td style="width: 35%; background-color: #ffff;" class="itemtable-priview-title">
                                            <label  style=" font-weight: bold; font-size: 14px;display:block"> MRI 3 Tesla</label>
                                            
                                        </td>
                                        <td style="width: 65%;" colspan="2" class="itemtable-priview">
                                            <asp:label ID="lbl_mri3tesla_no_rad" runat="server" style=" font-weight: normal; font-size: 14px;display:block">(tidak ada order)</asp:label>
                                            <ul style="list-style-type: circle;padding-left:20px">
                                            <asp:Repeater ID="rpt_mri3teslarad" runat="server">
                                                <ItemTemplate>
                                                  <li><%# Eval("name") %></li>
                                                </ItemTemplate>

                                            </asp:Repeater>
                                            </ul>
                                        </td>
                                    </tr>
                                </tbody>
                                <tbody>
                                    <tr style="border: solid 1px #cdced9; width: 100%;">
                                        <td style="width: 35%; background-color: #ffff;" class="itemtable-priview-title">
                                            <label  style=" font-weight: bold; font-size: 14px;display:block"> Lain-lain</label>
                                            
                                        </td>
                                        <td style="width: 65%;" colspan="2" class="itemtable-priview">
                                            <asp:label ID="txt_listradorder" runat="server" style=" font-weight: normal; font-size: 14px;display:block">(tidak ada order)</asp:label>
                                        </td>
                                    </tr>
                                </tbody>
                                
                            </table> 
                            <table style="width:100%;margin-top:10px;margin-bottom:100px">
                                <tbody>
                                    <tr style=" width: 100%;">
                                        <td> <asp:label ID="Label47" runat="server" style=" font-weight: bold; font-size: 14px;display:block" Text="Catatan:"></asp:label></td>
                                    </tr>
                                </tbody>
    
                            </table>
                            <table style="margin-bottom:50px">
                               <tbody>
                                    <tr style=" width: 100%;margin-top:100px">
                                        <td> <asp:label ID="Label48" runat="server" style=" font-weight: bold; font-size: 14px;display:block" Text="PERNYATAAN/ STATEMENT"></asp:label></td>
                                    </tr>
                                </tbody>
                            </table>
                            <table style="width:100%">
                                <tbody>
                                    <tr>
                                        <td>
                                            <label>Tanggal / <i>Date</i></label> 
                                             <asp:label ID="lbl_stetement_date" runat="server" Text='<%# DateTime.Now.ToString("dd MMMM yyyy") %>'></asp:label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <label>Pada saat Pemeriksaan ini dilakukan, saya tidak sedang hamil</label>
                                        </td>
                                    </tr>
                                     <tr>
                                        <td>
                                            
                                            <i>I'm not pregnant during the examination</i>
                                        </td>
                                    </tr>
                                    
                                </tbody>
                            </table>


                            <table style="width: 100%;margin-top:100px">
                                <tbody>
                                    <tr style="width: 100%">
                                        <td style="width: 35%;">
                                            <asp:label ID="lbl_statement_patient"  runat="server" Text="" style="display:block;font-weight:normal;font-size:13px;"></asp:label>

                                            <hr style="border:1px dashed #0000" />
                                            <asp:label ID="Label50" runat="server" style="font-weight:normal;font-size:13px;font-style:italic;display:block" Text="Pasien"><label style="font-style:italic">Patient</label></asp:label>

                                        </td>
                                        <td style="width:45%"></td>
                                        <td  style="width: 35%;">
                                           <asp:label ID="lbl_statement_dokter"  runat="server" Text="" style="display:block;font-weight:normal;font-size:13px;"></asp:label>
                                           <hr style=" width:100%;margin-left:0px; margin-top: 10px; margin-bottom: 10px;border-top:1px solid #000000" />
                                            <label style="font-size:13px;font-style:italic">Dokter (Physician)</label>

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
