<%@ Page Language="C#" AutoEventWireup="true" CodeFile="NursePengkajianAwal.aspx.cs" Inherits="Form_PrintViewer_Content_NursePengkajianAwal" %>

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


    <!-- Mask -->
    <link rel="stylesheet" href="~/Content/plugins/jasny-bootstrap/css/jasny-bootstrap.min.css" />
    <!-- Theme style -->
    <link rel="stylesheet" href="~/Content/dist/css/AdminLTE.css" />
    <link rel="stylesheet" href="~/Content/dist/css/skins/skin-blue-light.css" />
    <link rel="stylesheet" href="~/Content/Site.css" />

    <link href="favicon.ico" rel="shortcut icon" type="image/x-icon" />
    <link href="~/favicon.ico" rel="shortcut icon" type="image/x-icon" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />


    <style type="text/css">
        @media print {
            thead {
                display: table-header-group;
            }

            tfoot {
                display: table-footer-group;
            }
        }

        .bordertable {
            border: 1px solid #cdced9;
        }

        tr.spaceUnder > td {
            padding-bottom: 3em;
        }

        #mydiv {
            margin-top: 10%;
            position: fixed;
            bottom: 0px;
            left: 5px
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

        /*td {
            padding-top: 10px;
            padding-bottom: 10px;
        }*/

        #tableSOAP {
            width: 100%;
            border: 1px solid #CDCED9;
        }

            #tableSOAP td {
                padding-top: 5px;
                padding-bottom: 5px;
                border: 1px solid #CDCED9;
                padding-left: 5px;
            }

            #tableSOAP th {
                background-color: black;
                color: white;
                padding-top: 5px;
                padding-bottom: 5px;
                border: 1px solid #CDCED9;
                padding-left: 5px;
                -webkit-print-color-adjust: exact;
                background-image: radial-gradient(#000,#000);
            }

        .tbl-nurse td {
            font-size: 14px;
        }

        .bg-kolom-title {
            background-color: #f4f4f4;
            padding: 5px 10px 5px 10px;
            vertical-align: top;
        }

        .bg-kolom-content {
            background-color: #ffffff;
            padding: 5px 10px 5px 10px;
        }
    </style>

</head>

<script type="text/javascript">
    function printpreview() {
        window.print();

        return false;
    }
</script>

<body style="padding-top: 15px;">
    <form id="form1" runat="server">

        <asp:ScriptManager ID="ScriptManager1" runat="server">
            <Scripts>
                <asp:ScriptReference Path="~/Content/plugins/jQuery/jQuery-2.2.0.min.js" />
                <asp:ScriptReference Path="~/Content/bootstrap/js/bootstrap.min.js" />
                <asp:ScriptReference Path="~/Content/plugins/jasny-bootstrap/js/jasny-bootstrap.min.js" />
                <asp:ScriptReference Path="~/Content/dist/js/app.min.js" />
            </Scripts>
        </asp:ScriptManager>

        <div id="tblContent" style="padding: 10px;">
            
            <table style="width: 100%;">
                <tr>
                    <td style="width:50%"></td>
                    <td style="width:25%">
                        <label style="font-weight:bold; font-family:Arial, Helvetica, sans-serif; font-size:14px">Tanggal Dibuat (</label>
                        <label style="font-weight:bold; font-family:Arial, Helvetica, sans-serif; font-size:14px">Created Date</label>
                        <label style="font-weight:bold; font-family:Arial, Helvetica, sans-serif; font-size:14px">)</label>
                        <br />
                        <asp:Label ID="lblCreatedOrderDate" runat="server" Font-Bold="false"  Font-Names="Helvetica" Font-Size="14px"></asp:Label>
                    </td>
                    <td style="width:25%">
                        <label style="font-weight:bold; font-family:Arial, Helvetica, sans-serif; font-size:14px">Tanggal Diubah (</label>
                        <label style="font-weight:bold; font-family:Arial, Helvetica, sans-serif; font-size:14px">Modified Date</label>
                        <label style="font-weight:bold; font-family:Arial, Helvetica, sans-serif; font-size:14px">)</label>
                        <br/>
                        <asp:Label ID="lblModifiedOrderDate" runat="server" Font-Bold="false"  Font-Names="Helvetica" Font-Size="14px"></asp:Label>
                    </td>
                </tr>
            </table>

            <div style="padding-bottom:10px;">
            <label style="font-size:16px; font-weight:bold;">Pengkajian Awal Rawat Jalan</label>
                </div>
            <br />

            <table border="1" style="width: 100%; border-color:black;" class="tbl-nurse">
                <tr style="background-color: black; color: white;">
                    <th style="text-align: left; font-size: 16px; width: 25%; padding: 5px 10px 5px 10px;">Keterangan</th>
                    <th style="text-align: left; font-size: 16px; width: 75%; padding: 5px 10px 5px 10px;">Deskripsi</th>
                </tr>

                <tr runat="server" id="divemergency1" visible="false">
                    <td class="bg-kolom-title"><b>Triage</b> </td>
                    <td style="padding: 0px;">
                        <table style="width: 100%; border-bottom:1px solid black;">
                            <tr>
                                <td style="width: 100%; vertical-align: top;" class="bg-kolom-content">
                                    <asp:Label runat="server" Style="font-weight: bold" ID="lblskortriage" Text="-" />
                                    &nbsp;-&nbsp; 
                                    <asp:Label runat="server" Style="font-weight: bold" ID="lbltraumatriage" Text="-" />
                                </td>
                            </tr>
                        </table>
                        <table style="width: 100%; border-bottom:1px solid black;">
                            <tr>
                                <td style="width: 50%; border-right: 1px solid black; vertical-align: top;" class="bg-kolom-content">
                                    <b>Cara Pasien Datang : </b> <br /><asp:Label runat="server" ID="lblpasiendatang" Text="-" />
                                </td>
                                <td style="width: 50%; vertical-align: top;" class="bg-kolom-content">
                                    <b>Keadaan Umum : </b> <br /><asp:Label runat="server" ID="lblkeadaanumum" Text="-" />
                                </td>
                                
                            </tr>
                        </table>
                        <table style="width: 100%;">
                            <tr>
                                <td style="width: 25%; border-right: 1px solid black; vertical-align: top;" class="bg-kolom-content">
                                    <b>Airway : </b> <br /><asp:Label ID="lblairway" runat="server" Text="-"></asp:Label>
                                </td>
                                <td style="width: 25%; border-right: 1px solid black; vertical-align: top;" class="bg-kolom-content">
                                    <b>Breathing : </b> <br /><asp:Label ID="lblbreathing" runat="server" Text="-"></asp:Label>
                                </td>
                                <td style="width: 25%; border-right: 1px solid black; vertical-align: top;" class="bg-kolom-content">
                                    <b>Circulation : </b> <br /><asp:Label ID="lblcirculation" runat="server" Text="-"></asp:Label>
                                </td>
                                <td style="width: 25%; vertical-align: top;" class="bg-kolom-content">
                                    <b>Disability : </b> <br /> <asp:Label ID="lbldisability" runat="server" Text="-"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>

                <tr>
                    <td class="bg-kolom-title"><b>Keluhan Utama</b> </td>
                    <td class="bg-kolom-content">
                        <asp:Label ID="Lbl_keluhanutama" runat="server" Text="-"></asp:Label>
                        <br />
                        <asp:Label ID="lbl_anamnesis" runat="server" Text="-"></asp:Label>
                        <br />
                        <br />
                        <b>Hamil : </b>
                        <asp:Label ID="lbl_hamil" runat="server" Text="-"></asp:Label>
                        <br />
                        <b>Menyusui : </b>
                        <asp:Label ID="lbl_menyusui" runat="server" Text="-"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="bg-kolom-title"><b>Kunjungan Ke Daerah Endemis</b> </td>
                    <td class="bg-kolom-content">
                        <asp:Label ID="lbl_endemicarea" runat="server" Text="-"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="bg-kolom-title"><b>Skrining Penyakit Infeksius</b> </td>
                    <td class="bg-kolom-content">
                        <asp:Repeater runat="server" ID="repeaterInfeksius">
                            <ItemTemplate>
                                <li>
                                    <asp:Label ID="lbl_infeksius" runat="server" Text='<%#Eval("Remarks") %>' />
                                </li>
                            </ItemTemplate>
                        </asp:Repeater>
                        <asp:Label ID="lbl_infeksius_empty" runat="server" Text="-" Style="display: none;"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="bg-kolom-title"><b>Pengobatan & Alergi</b> </td>
                    <td style="padding: 0px;">
                        <table style="width: 100%;">
                            <tr>
                                <td style="width: 25%; border-right: 1px solid black; vertical-align: top;" class="bg-kolom-content">
                                    <b>Pengobatan Rutin</b>
                                    <br />
                                    <asp:Repeater runat="server" ID="repeaterPengobatanRutin">
                                        <ItemTemplate>
                                            <li>
                                                <asp:Label ID="lbl_pengobatanrutin" runat="server" Text='<%#Eval("Value") %>' />
                                            </li>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                    <asp:Label ID="lbl_pengobatanrutin_empty" runat="server" Text="-" Style="display: none;"></asp:Label>
                                </td>
                                <td style="width: 25%; border-right: 1px solid black; vertical-align: top;" class="bg-kolom-content">
                                    <b>Alergi Obat</b>
                                    <br />
                                    <asp:Repeater runat="server" ID="repeaterAlergiObat">
                                        <ItemTemplate>
                                            <li>
                                                <asp:Label ID="lbl_alergiobat" runat="server" Text='<%#Eval("Value") %>' />
                                            </li>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                    <asp:Label ID="lbl_alergiobat_empty" runat="server" Text="-" Style="display: none;"></asp:Label>
                                </td>
                                <td style="width: 25%; border-right: 1px solid black; vertical-align: top;" class="bg-kolom-content">
                                    <b>Alergi Makanan</b>
                                    <br />
                                    <asp:Repeater runat="server" ID="repeaterAlergiMakanan">
                                        <ItemTemplate>
                                            <li>
                                                <asp:Label ID="lbl_alergimakanan" runat="server" Text='<%#Eval("Value") %>' />
                                            </li>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                    <asp:Label ID="lbl_alergimakanan_empty" runat="server" Text="-" Style="display: none;"></asp:Label>
                                </td>
                                <td style="width: 25%; vertical-align: top;" class="bg-kolom-content">
                                    <b>Alergi Lainnya</b>
                                    <br />
                                    <asp:Repeater runat="server" ID="repeaterAlergiOther">
                                        <ItemTemplate>
                                            <li>
                                                <asp:Label ID="lbl_alergiother" runat="server" Text='<%#Eval("Value") %>' />
                                            </li>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                    <asp:Label ID="lbl_alergiother_empty" runat="server" Text="-" Style="display: none;"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td class="bg-kolom-title"><b>Riwayat Penyakit</b> </td>
                    <td style="padding: 0px;">
                        <table style="width: 100%;">
                            <tr>
                                <td style="width: 35%; border-right: 1px solid black; vertical-align: top;" class="bg-kolom-content">
                                    <b>Riwayat Operasi</b>
                                    <br />
                                    <asp:Repeater runat="server" ID="repeaterRiwayatOperasi">
                                        <ItemTemplate>
                                            <li>
                                                <asp:Label ID="lbl_riwayatoperasi" runat="server" Text='<%#Eval("Value") %>' />
                                            </li>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                    <asp:Label ID="lbl_riwayatoperasi_empty" runat="server" Text="-" Style="display: none;"></asp:Label>
                                </td>
                                <td style="width: 30%; border-right: 1px solid black; vertical-align: top;" class="bg-kolom-content">
                                    <b>Riwayat Penyakit Dahulu</b>
                                    <br />
                                    <asp:Repeater runat="server" ID="repeaterPenyakitDahulu">
                                        <ItemTemplate>
                                            <li>
                                                <asp:Label ID="lbl_penyakitdahulu" runat="server" Text='<%#Eval("Remarks") %>' />
                                            </li>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                    <asp:Label ID="lbl_penyakitdahulu_empty" runat="server" Text="-" Style="display: none;"></asp:Label>
                                </td>
                                <td style="width: 35%; vertical-align: top;" class="bg-kolom-content">
                                    <b>Riwayat Penyakit Keluarga</b>
                                    <br />
                                    <asp:Repeater runat="server" ID="repeaterPenyakitKeluarga">
                                        <ItemTemplate>
                                            <li>
                                                <asp:Label ID="lbl_penyakitkeluarga" runat="server" Text='<%#Eval("Remarks") %>' />
                                            </li>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                    <asp:Label ID="lbl_penyakitkeluarga_empty" runat="server" Text="-" Style="display: none;"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td class="bg-kolom-title"><b>Pemeriksaan Umum</b> </td>
                    <td style="padding: 0px;">
                        <table style="width: 100%; border-bottom:1px solid black;">
                            <tr>
                                <td style="width: 30%; border-right: 1px solid black; vertical-align: top;" class="bg-kolom-content">
                                    <b>Eye : </b> <br /><asp:Label ID="lbl_eye" runat="server" Text="-"></asp:Label>
                                </td>
                                <td style="width: 30%; border-right: 1px solid black; vertical-align: top;" class="bg-kolom-content">
                                    <b>Move : </b> <br /><asp:Label ID="lbl_move" runat="server" Text="-"></asp:Label>
                                </td>
                                <td style="width: 30%; border-right: 1px solid black; vertical-align: top;" class="bg-kolom-content">
                                    <b>Verbal : </b> <br /><asp:Label ID="lbl_verbal" runat="server" Text="-"></asp:Label>
                                </td>
                                <td style="width: 10%; vertical-align: top;" class="bg-kolom-content">
                                    <b>Skor : </b> <asp:Label ID="lbl_skor" runat="server" Text="-"></asp:Label>
                                </td>
                            </tr>
                        </table>
                        <table style="width: 100%; border-bottom:1px solid black;">
                            <tr>
                                <td style="width: 100%; vertical-align: top;" class="bg-kolom-content">
                                    <b>Skala Nyeri : </b> <asp:Label ID="lbl_skalanyeri" runat="server" Text="-"></asp:Label>
                                </td>
                            </tr>
                        </table>
                        <table style="width: 100%; border-bottom:1px solid black;">
                            <tr>
                                <td style="width: 16%; border-right: 1px solid black; vertical-align: top;" class="bg-kolom-content">
                                    <b>Tekanan Darah : </b><br /> <asp:Label ID="lbl_tekanandarah" runat="server" Text="-"></asp:Label>
                                </td>
                                <td style="width: 12%; border-right: 1px solid black; vertical-align: top;" class="bg-kolom-content">
                                    <b>Nadi : </b><br /> <asp:Label ID="lbl_nadi" runat="server" Text="-"></asp:Label>
                                </td>
                                <td style="width: 16%; border-right: 1px solid black; vertical-align: top;" class="bg-kolom-content">
                                    <b>Pernapasan : </b><br /> <asp:Label ID="lbl_pernapasan" runat="server" Text="-"></asp:Label>
                                </td>
                                <td style="width: 11%; border-right: 1px solid black; vertical-align: top;" class="bg-kolom-content">
                                    <b>SpO2 : </b><br /> <asp:Label ID="lbl_spo2" runat="server" Text="-"></asp:Label>
                                </td>
                                <td style="width: 11%; border-right: 1px solid black; vertical-align: top;" class="bg-kolom-content">
                                    <b>Suhu : </b><br /> <asp:Label ID="lbl_suhu" runat="server" Text="-"></asp:Label>
                                </td>
                                <td style="width: 11%; border-right: 1px solid black; vertical-align: top;" class="bg-kolom-content">
                                    <b>Berat : </b><br /> <asp:Label ID="lbl_berat" runat="server" Text="-"></asp:Label>
                                </td>
                                <td style="width: 11%; border-right: 1px solid black; vertical-align: top;" class="bg-kolom-content">
                                    <b>Tinggi : </b><br /> <asp:Label ID="lbl_tinggi" runat="server" Text="-"></asp:Label>
                                </td>
                                <td style="width: 14%; vertical-align: top;" class="bg-kolom-content">
                                    <b>Lingkar Kepala : </b><br /> <asp:Label ID="lbl_lingkarkepala" runat="server" Text="-"></asp:Label>
                                </td>
                            </tr>
                        </table>
                        <table style="width: 100%;">
                            <tr>
                                <td style="width: 30%; border-right: 1px solid black; vertical-align: top;" class="bg-kolom-content">
                                    <b>Status Mental : </b><br /> <asp:Label ID="lbl_statusmental" runat="server" Text="-"></asp:Label>
                                </td>
                                <td style="width: 30%; border-right: 1px solid black; vertical-align: top;" class="bg-kolom-content">
                                    <b>Kesadaran : </b><br /> <asp:Label ID="lbl_kesadaran" runat="server" Text="-"></asp:Label>
                                </td>
                                <td style="width: 40%; vertical-align: top;" class="bg-kolom-content">
                                    <b>Risiko Jatuh : </b>
                                    <br />
                                    <asp:Repeater runat="server" ID="repeaterResikoJatuh">
                                        <ItemTemplate>
                                            <li>
                                                <asp:Label ID="lbl_risikojatuh" runat="server" Text='<%#Eval("Remarks") %>' />
                                            </li>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                    <asp:Label ID="lbl_risikojatuh_empty" runat="server" Text="-" Style="display: none;"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>

                <tr runat="server" id="divobgyn1" visible="false">
                    <td class="bg-kolom-title"><b>Riwayat Kebidanan</b> </td>
                    <td style="padding: 0px;">
                        <table style="width: 100%;">
                            <tr>
                                <td style="width: 15%; border-right: 1px solid black; vertical-align: top;" class="bg-kolom-content">
                                    <b>Menarche Umur</b>
                                    <br />
                                    <asp:Label ID="lbl_menarche" runat="server" Text="-"></asp:Label>
                                </td>
                                <td style="width: 25%; border-right: 1px solid black; vertical-align: top;" class="bg-kolom-content">
                                    <b>Haid</b>
                                    <br />
                                    <asp:Label ID="lbl_haid" runat="server" Text="-"></asp:Label>
                                </td>
                                <td style="width: 25%; border-right: 1px solid black; vertical-align: top;" class="bg-kolom-content">
                                    <b>Keluhan Saat Haid</b>
                                    <br />
                                    <asp:Label ID="lbl_haidproblem" runat="server" Text="-"></asp:Label>
                                </td>
                                <td style="width: 35%; vertical-align: top;" class="bg-kolom-content">
                                    <b>Penggunaan Kontrasepsi</b>
                                    <br />
                                    <asp:Repeater runat="server" ID="repeaterContraception">
                                        <ItemTemplate>
                                            <li>
                                                <asp:Label ID="lbl_kontrasepsijenis" runat="server" Text='<%#Eval("value") %>' /> ,
                                                <asp:Label ID="lbl_kontrasepsisejak" runat="server" Text='<%#Eval("remarks") %>' /> -
                                                <asp:Label ID="lbl_kontrasepsihingga" runat="server" Text='<%#Eval("status") %>' />
                                            </li>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                    <asp:Label ID="lbl_kontrasepsi_empty" runat="server" Text="-" Style="display: none;"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>

                <tr runat="server" id="divobgyn2" visible="false">
                    <td class="bg-kolom-title"><b>Riwayat Kehamilan</b> </td>
                    <td style="padding: 5px 0px;">
                        <div style="padding-top:5px;" runat="server" id="divpregnanthistory">
                        <table style="width:100%;">
                            <tr>
                                <td style="width:10%; font-size:10px; font-weight:bold; padding-left:10px;"> PREGNANCY </td>
                                <td style="width:10%; font-size:10px; font-weight:bold;"> CHILD'S AGE </td>
                                <td style="width:10%; font-size:10px; font-weight:bold;"> GENDER </td>
                                <td style="width:15%; font-size:10px; font-weight:bold;"> BIRTH WEIGHT </td>
                                <td style="width:15%; font-size:10px; font-weight:bold;"> DELIVERY METHOD </td>
                                <td style="width:15%; font-size:10px; font-weight:bold;"> ASSISTED BY </td>
                                <td style="width:15%; font-size:10px; font-weight:bold;"> LOCATION </td>
                                <td style="width:10%; font-size:10px; font-weight:bold;"> BIRTH STATUS </td>
                            </tr>
                            <asp:Repeater runat="server" ID="rptpregnanthistory">
                                <ItemTemplate>
                                    <tr>
                                        <td style="width:10%; font-size:14px; border-top:1px solid #000000; padding: 2px 4px; padding-left:10px;">
                                            <asp:Label ID="lblpregnancy" runat="server" Text='<%#Eval("pregnancy_sequence") %>'/>
                                        </td>
                                        <td style="width:10%; font-size:14px; border-top:1px solid #000000; padding: 2px 4px;">
                                            <asp:Label ID="lblchildage" runat="server" Text='<%#Eval("child_age") + " " + Eval("age_type") %>'/>
                                        </td>
                                        <td style="width:10%; font-size:14px; border-top:1px solid #000000; padding: 2px 4px;">
                                            <asp:Label ID="lblgender" runat="server" Text='<%#Eval("child_sex").ToString() == "1" ? "Pria" : Eval("child_sex").ToString() == "2" ? "Wanita" : "N/A" %>'/>
                                        </td>
                                        <td style="width:15%; font-size:14px; border-top:1px solid #000000; padding: 2px 4px;">
                                            <asp:Label ID="lblbbl" runat="server" Text='<%#Eval("BBL") + " gr" %>'/>
                                        </td>
                                        <td style="width:15%; font-size:14px; border-top:1px solid #000000; padding: 2px 4px;">
                                            <asp:Label ID="lblmethod" runat="server" Text='<%#Eval("labor_type") %>'/>
                                        </td>
                                        <td style="width:15%; font-size:14px; border-top:1px solid #000000; padding: 2px 4px;">
                                            <asp:Label ID="lblassisted" runat="server" Text='<%#Eval("labor_helper") %>'/>
                                        </td>
                                        <td style="width:15%; font-size:14px; border-top:1px solid #000000; padding: 2px 4px;">
                                            <asp:Label ID="lbllocation" runat="server" Text='<%#Eval("labor_place") %>'/>
                                        </td>
                                        <td style="width:10%; font-size:14px; border-top:1px solid #000000; padding: 2px 4px;">
                                            <asp:Label ID="lblstatus" runat="server" Text='<%#Eval("labor_doa").ToString() == "1" ? "Hidup" : Eval("labor_doa").ToString() == "2" ? "Mati" : "N/A" %>'/>
                                        </td>
                                    </tr>
                                </ItemTemplate>
                            </asp:Repeater>
                        </table>
                        </div>
                        <asp:Label ID="lbl_pregnanthistory_empty" runat="server" Text="-" Style="display: none; margin-left:10px;"></asp:Label>
                    </td>
                </tr>

                <tr runat="server" id="divobgyn3" visible="false">
                    <td class="bg-kolom-title"><b>Data Kehamilan</b> </td>
                    <td style="padding: 0px;">
                        <table style="width: 100%;">
                            <tr>
                                <td style="width: 40%; border-right: 1px solid black; vertical-align: top;" class="bg-kolom-content">
                                    <b>GPA</b>
                                    <br />
                                    <asp:Label ID="lbl_g" runat="server" Text="-"></asp:Label> &nbsp;&nbsp;
                                    <asp:Label ID="lbl_p" runat="server" Text="-"></asp:Label> &nbsp;&nbsp;
                                    <asp:Label ID="lbl_a" runat="server" Text="-"></asp:Label> &nbsp;&nbsp;
                                </td>
                                <td style="width: 30%; border-right: 1px solid black; vertical-align: top;" class="bg-kolom-content">
                                    <b>HPHT</b>
                                    <br />
                                    <asp:Label ID="lbl_hpht" runat="server" Text="-"></asp:Label>
                                </td>
                                <td style="width: 30%; vertical-align: top;" class="bg-kolom-content">
                                    <b>Tafsiran Hari Lahir</b>
                                    <br />
                                    <asp:Label ID="lbl_thl" runat="server" Text="-"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>

                <tr runat="server" id="divpediatric1" visible="false">
                    <td class="bg-kolom-title"><b>Tumbuh Kembang Anak</b> </td>
                    <td style="padding: 0px;">
                        <table style="width: 100%;">
                            <tr>
                                <td style="width: 16.6%; border-right: 1px solid black; vertical-align: top;" class="bg-kolom-content">
                                    <b>Tengkurap</b>
                                    <br />
                                    <asp:Label ID="lbl_tengkurap" runat="server" Text="-"></asp:Label>
                                </td>
                                <td style="width: 16.6%; border-right: 1px solid black; vertical-align: top;" class="bg-kolom-content">
                                    <b>Duduk</b>
                                    <br />
                                    <asp:Label ID="lbl_duduk" runat="server" Text="-"></asp:Label>
                                </td>
                                <td style="width: 16.6%; border-right: 1px solid black; vertical-align: top;" class="bg-kolom-content">
                                    <b>Merangkak</b>
                                    <br />
                                    <asp:Label ID="lbl_merangkak" runat="server" Text="-"></asp:Label>
                                </td>
                                <td style="width: 16.6%; border-right: 1px solid black; vertical-align: top;" class="bg-kolom-content">
                                    <b>Berdiri</b>
                                    <br />
                                    <asp:Label ID="lbl_berdiri" runat="server" Text="-"></asp:Label>
                                </td>
                                <td style="width: 16.6%; border-right: 1px solid black; vertical-align: top;" class="bg-kolom-content">
                                    <b>Berjalan</b>
                                    <br />
                                    <asp:Label ID="lbl_berjalan" runat="server" Text="-"></asp:Label>
                                </td>
                                <td style="width: 16.6%; vertical-align: top;" class="bg-kolom-content">
                                    <b>Berbicara</b>
                                    <br />
                                    <asp:Label ID="lbl_berbicara" runat="server" Text="-"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>

                <tr>
                    <td class="bg-kolom-title"><b>Catatan Untuk Dokter</b> </td>
                    <td class="bg-kolom-content">
                        <asp:Label ID="lbl_catatanuntukdokter" runat="server" Text="-"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="bg-kolom-title"><b>Catatan Dari Dokter</b> </td>
                    <td class="bg-kolom-content">
                        <asp:Label ID="lbl_catatandaridokter" runat="server" Text="-"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="bg-kolom-title"><b>Masalah Nutrisi Khusus</b> </td>
                    <td class="bg-kolom-content">
                        <asp:Label ID="lbl_masalahnutrisi" runat="server" Text="-"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="bg-kolom-title"><b>Puasa</b> </td>
                    <td class="bg-kolom-content">
                        <asp:Label ID="lbl_puasa" runat="server" Text="-"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="bg-kolom-title"><b>Bahasa Sehari-hari</b> </td>
                    <td class="bg-kolom-content">
                        <asp:Label ID="lbl_bahasa" runat="server" Text="-"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="bg-kolom-title"><b>Perlu Penerjemah</b> </td>
                    <td class="bg-kolom-content">
                        <asp:Label ID="lbl_penerjemah" runat="server" Text="-"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="bg-kolom-title"><b>Metode Belajar yang Disukai</b> </td>
                    <td class="bg-kolom-content">
                        <asp:Label ID="lbl_metodebelajar" runat="server" Text="-"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="bg-kolom-title"><b>Masalah Terkait Metode Pembelajaran</b> </td>
                    <td class="bg-kolom-content">
                        <asp:Label ID="lbl_masalahbelajar" runat="server" Text="-"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="bg-kolom-title"><b>Kesediaan Pasien & Keluarga Menerima Informasi & Edukasi</b> </td>
                    <td class="bg-kolom-content">
                        <asp:Label ID="lbl_kesediaanpasien" runat="server" Text="-"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="bg-kolom-title"><b>Informasi & Edukasi Kesehatan yang Dibutuhkan</b> </td>
                    <td class="bg-kolom-content">
                        <%--<asp:Label ID="lbl_informasidibutuhkan" runat="server" Text="-"></asp:Label>
                        <br />--%>
                        <asp:Repeater runat="server" ID="repeaterInfoEdu">
                            <ItemTemplate>
                                <li>
                                    <asp:Label ID="lbl_infoedu" runat="server" Text='<%#Eval("Value") %>' />
                                </li>
                            </ItemTemplate>
                        </asp:Repeater>
                        <asp:Label ID="lbl_infoedu_empty" runat="server" Text="-" Style="display: none;"></asp:Label>
                    </td>
                </tr>

                <tr runat="server" id="divobgyn4" visible="false">
                    <td class="bg-kolom-title"><b>Status Pernikahan</b> </td>
                    <td class="bg-kolom-content">
                        <asp:Label ID="lbl_statuspernikahan" runat="server" Text="-"></asp:Label>
                    </td>
                </tr>

                <tr>
                    <td class="bg-kolom-title"><b>Respon Emosi</b> </td>
                    <td class="bg-kolom-content">
                        <asp:Label ID="lbl_responemosi" runat="server" Text="-"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="bg-kolom-title"><b>Nilai/ Nilai Budaya/ Kepercayaan</b> </td>
                    <td class="bg-kolom-content">
                        <asp:Label ID="lbl_nilai" runat="server" Text="-"></asp:Label>
                    </td>
                </tr>
            </table>

            <br />
            <br />
            <br />
            <br />
            <br />

            <table border="0" style="width: 100%">
                <tr>
                    <td style="width: 70%;"> &nbsp; </td>
                    <td style="width: 30%; text-align: left; font-size:12px;">
                        <div style="border-bottom: 1px solid black; padding: 5px;">
                            <asp:Label ID="LabelNamaUser" runat="server" Text="Nama :"></asp:Label>
                        </div>
                        <div style="padding: 5px;">
                            Perawat <i>(Nurse)</i>
                        </div>
                        <br />

                    </td>
                </tr>
            </table>

        </div>

    </form>
</body>
</html>
