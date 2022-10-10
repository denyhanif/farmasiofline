<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PrintContent.aspx.cs" Inherits="Form_PrintContent" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <!-- Tell the browser to be responsive to screen width -->
    <meta content="width=device-width, initial-scale=1, maximum-scale=1, user-scalable=no" name="viewport" />
    <!-- Bootstrap 3.3.6 -->
    <link rel="stylesheet" href="~/Content/bootstrap/css/bootstrap.css" />
    <!-- Bootstrap Switch -->
    <%--<link rel="stylesheet" href="~/Content/bootstrap-switch/bootstrap3/bootstrap-switch.css" />
    <link rel="stylesheet" href="~/Content/bootstrap-switch/bootstrap3/bootstrap-switch.min.css" />--%>
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
    <!-- Select Bootstrap -->
    <link rel="stylesheet" href="Content/bootstrap-select/css/bootstrap-select.css" />
    <!-- EMR-Doctor -->
    <link rel="stylesheet" href="Content/EMR-Doctor.css" />
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

            .dataheader td {
                padding-top: 10px;
                padding-bottom: 10px;
            }

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

            #tableTravel {
                width: 100%;
                border: 1px solid #CDCED9;
            }

                #tableTravel td {
                    padding-top: 5px;
                    padding-bottom: 5px;
                    border: 1px solid #CDCED9;
                    padding-left: 5px;
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

        <asp:HiddenField runat="server" ID="hfOrgID" />
        <asp:HiddenField runat="server" ID="hf_admiss_id" />
        <asp:HiddenField runat="server" ID="hf_patient_id" />
        <asp:HiddenField runat="server" ID="hf_ticket_patient" />
        <asp:HiddenField ID="HiddenLabMark" runat="server" />

        <div>
            <asp:UpdatePanel runat="server">
                <ContentTemplate>

                    <asp:Image ID="ImageCovid" runat="server" ImageUrl="~/Images/Icon/CV-stamp.svg" Visible="false" style="width:150px; height:75px; position: absolute; right: 15px; top: 35px;" />
                    <table style="width: 100%;" class="dataheader">
                        <tr>
                            <td style="vertical-align:top;width:50%" >
                                
                            </td>
                            <td style="width:25%">
                                <label style="font-weight:bold; font-family:Arial, Helvetica, sans-serif; font-size:14px">Created Date (</label>
                                <label style="font-weight:bold; font-family:Arial, Helvetica, sans-serif; font-size:14px">Tanggal Dibuat</label>
                                <label style="font-weight:bold; font-family:Arial, Helvetica, sans-serif; font-size:14px">)</label>
                                <br />
                                <asp:Label ID="lblCreatedOrderDate" runat="server" Font-Bold="false"  Font-Names="Helvetica" Font-Size="14px"></asp:Label>
                            </td>
                            <td style="width:25%">
                                <label style="font-weight:bold; font-family:Arial, Helvetica, sans-serif; font-size:14px">Modified Date (</label>
                                <label style="font-weight:bold; font-family:Arial, Helvetica, sans-serif; font-size:14px">Tanggal Diubah</label>
                                <label style="font-weight:bold; font-family:Arial, Helvetica, sans-serif; font-size:14px">)</label>
                                <br/>
                                <asp:Label ID="lblModifiedOrderDate" runat="server" Font-Bold="false"  Font-Names="Helvetica" Font-Size="14px"></asp:Label>
                            </td>
                        </tr>
                    </table>
                    <br />
                    <asp:Label runat="server" Text="Medical Resume (" Font-Bold="true" Font-Names="Helvetica" Font-Size="14px"></asp:Label>
                    <asp:Label runat="server" Text="Resume Medis" Font-Bold="true" Font-Italic="true"  Font-Names="Helvetica" Font-Size="14px"></asp:Label>
                    <asp:Label runat="server" Text=")" Font-Bold="true"  Font-Names="Helvetica" Font-Size="14px"></asp:Label>
                    <table id="tableSOAP" class="dataheader">
                        <tr>
                            <th style="width: 20%">
                                <asp:Label runat="server" Text="Section (Keterangan)"></asp:Label>
                            </th>
                            <th style="width: 80%">
                                <asp:Label runat="server" Text="Description (Deskripsi)"></asp:Label>
                            </th>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label runat="server" Text="Anamnesis" Font-Bold="true"></asp:Label>
                                <br />
                                <asp:Label runat="server" Text="(Anamnesa)"></asp:Label>
                            </td>
                            <td>
                                <asp:Repeater ID="repeaterS" runat="server">
                                    <ItemTemplate>
                                        <asp:Label ID="lblS" runat="server" Text='<%# Bind("value") %>'></asp:Label>
                                        <br />
                                    </ItemTemplate>
                                </asp:Repeater>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label runat="server" Text="Physical Exam" Font-Bold="true"></asp:Label>
                                <br />
                                <asp:Label runat="server" Text="(Pemeriksaan Fisik)"></asp:Label>
                            </td>
                            <td style="padding:0px;">
                                <table style="width:100%;">
                                    <tr>
                                        <td style="width:65%; border-left:0px; border-top:0px; border-bottom:0px; vertical-align:top;">
                                            <asp:Repeater ID="repeaterO" runat="server">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblO" runat="server" Text='<%# Bind("value") %>'></asp:Label>
                                                    <br />
                                                </ItemTemplate>
                                            </asp:Repeater>
                                        </td>
                                        <td style="width:35%; border-right:0px; border-top:0px; border-bottom:0px; vertical-align:top; padding-left:5px;">
                                            <asp:Label ID="lblO_TTV" runat="server" Text="-"></asp:Label>
                                        </td>
                                    </tr> 
                                </table>
                                
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label runat="server" Text="Diagnosis" Font-Bold="true"></asp:Label>
                                <br />
                                <asp:Label runat="server" Text="(Diagnosa)"></asp:Label>
                            </td>
                            <td>
                                <asp:Repeater ID="repeaterA" runat="server">
                                    <ItemTemplate>
                                        <asp:Label ID="lblA" runat="server" Text='<%# Bind("value") %>'></asp:Label>
                                        <br />
                                    </ItemTemplate>
                                </asp:Repeater>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label runat="server" Text="Plan & Procedure" Font-Bold="true"></asp:Label>
                                <br />
                                <asp:Label runat="server" Text="(Tindakan di RS)"></asp:Label>
                            </td>
                            <td>
                                <asp:Repeater ID="repeaterP" runat="server">
                                    <ItemTemplate>
                                        <asp:Label ID="lblP" runat="server" Text='<%# Bind("value") %>'></asp:Label>
                                        <br />
                                    </ItemTemplate>
                                </asp:Repeater>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label runat="server" Text="Procedure Result" Font-Bold="true"></asp:Label>
                                <br />
                                <asp:Label runat="server" Text="(Hasil Tindakan)"></asp:Label>
                            </td>
                            <td style="padding-left: 0; padding-right: 0;">
                                <asp:Repeater ID="repeaterPr" runat="server">
                                    <ItemTemplate>
                                        <asp:Label ID="lblPr" runat="server" Text='<%# Bind("value") %>' style="padding-left: 5px; padding-right: 5px;"></asp:Label>
                                        <br />
                                    </ItemTemplate>
                                </asp:Repeater>

                                <div style="border-top: solid 1px #cdced9; padding-left: 5px; padding-right: 5px" id="divdocremark" runat="server" >
                                 </div>
                            </td>
                        </tr>
                        
                    </table>
                    <div id="divTravelData" runat="server">
                    <table id="tableTravel" class="dataheader">
                        <tr>
                            <td style="width: 20%">
                                <asp:Label runat="server" Text="Travel Recomendation" Font-Bold="true"></asp:Label>
                                <br />
                                <asp:Label runat="server" Text="(Rekomendasi Perjalanan)"></asp:Label>
                            </td>
                            <td style="width: 80%">
                                <asp:Label ID="lblTravel" runat="server" Text="-"></asp:Label>
                            </td>
                        </tr>
                    </table>
                    </div>
                    <br />
                </ContentTemplate>
            </asp:UpdatePanel>

            <asp:UpdatePanel runat="server">
                <ContentTemplate>
                    <asp:GridView ID="gvw_detail" runat="server" AutoGenerateColumns="False" CssClass="table table-striped table-bordered table-condensed"
                        EmptyDataText="" Font-Names="Helvetica">
                        <PagerStyle CssClass="pagination-ys" />
                        <Columns>
                            <asp:TemplateField HeaderText="Item (Obat)" ItemStyle-HorizontalAlign="left" ItemStyle-Width="1%" HeaderStyle-Font-Size="11px">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lblObat" Text='<%# Bind("ItemName") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Qty (Jml)" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="1%" HeaderStyle-Font-Size="11px" HeaderStyle-CssClass="text-center">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lblQty" Text='<%# Bind("Quantity") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="UoM (Unit)"  ItemStyle-HorizontalAlign="Center" ItemStyle-Width="1%" HeaderStyle-Font-Size="11px" HeaderStyle-CssClass="text-center">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lblUnit" Text='<%# Bind("Uom") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Frequency (Frekuensi)" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="1%" HeaderStyle-Font-Size="11px" HeaderStyle-CssClass="text-center">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lblFreq" Text='<%# Bind("Frequency") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Dose (Dosis)" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="1%" HeaderStyle-Font-Size="11px" HeaderStyle-CssClass="text-center">
                                <ItemTemplate>
                                    <div runat="server" Visible='<%# Eval("IsDoseText").ToString() == "False" %>'>
                                        <asp:Label runat="server" ID="lblDosage" Text='<%# Bind("Dose") %>'></asp:Label>
                                        &nbsp;
                                        <asp:Label runat="server" ID="lblDosageUnit" Text='<%# Bind("DoseUom") %>'></asp:Label>
                                    </div>
                                    <asp:Label runat="server" ID="lblDoseText" Visible='<%# Eval("IsDoseText") %>' Text='<%# Bind("DoseText") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <%--<asp:TemplateField HeaderText="Dose UoM (Dosis Unit)" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="1%" HeaderStyle-Font-Size="11px" HeaderStyle-CssClass="text-center">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lblDosageUnit" Text='<%# Bind("DoseUom") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>--%>
                            <asp:TemplateField HeaderText="Instruction (Instruksi)" ItemStyle-HorizontalAlign="center" ItemStyle-Width="1%" HeaderStyle-Font-Size="11px" HeaderStyle-CssClass="text-center">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lblInstruksi" Text='<%# Bind("Instruction") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Route (Rute)" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="1%" HeaderStyle-Font-Size="11px" HeaderStyle-CssClass="text-center">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lblRute" Text='<%# Bind("Route") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Iter (Iter)" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="1%" HeaderStyle-Font-Size="11px" HeaderStyle-CssClass="text-center">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lblIter" Text='<%# Bind("Iter") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Routine (Rutin)"  ItemStyle-HorizontalAlign="Center" ItemStyle-Width="1%" HeaderStyle-Font-Size="11px" HeaderStyle-CssClass="text-center">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lblRoutine" Text='<%# Bind("Routine") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>

                    <asp:GridView ID="gvw_racikan_header" runat="server" AutoGenerateColumns="False" CssClass="table table-condensed table-header-racikan"
                        EmptyDataText="No Data" DataKeyNames="prescription_compound_header_id" OnRowDataBound="gvw_racikan_header_RowDataBound">
                        <Columns>
                            <asp:TemplateField ItemStyle-Width="100%" HeaderStyle-BackColor="#f4f4f4" ItemStyle-CssClass="no-padding" ItemStyle-BorderWidth="0px">
                                <HeaderTemplate>
                                    <table style="width: 100%;" class="header-table">
                                        <tr style="font-weight: bold; background-color: #f4f4f4; font-size:11px;">
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
                                            <td style="width: 30%; padding-left: 15px;">
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
                                                    <asp:BoundField ItemStyle-Width="50%" DataField="item_name" HeaderText="Item" ShowHeader="false" ItemStyle-VerticalAlign="Top" HeaderStyle-Font-Size="11px" />
                                                    <asp:TemplateField ItemStyle-Width="15%" HeaderText="Dose" ShowHeader="false" HeaderStyle-Font-Size="11px">
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
                                                    <asp:BoundField ItemStyle-Width="10%" DataField="quantity" HeaderText="Qty" ShowHeader="false" ItemStyle-HorizontalAlign="Right" ItemStyle-VerticalAlign="Top" HeaderStyle-CssClass="text-right" Visible="false" HeaderStyle-Font-Size="11px" />
                                                    <asp:TemplateField ItemStyle-Width="5%">
                                                        <ItemTemplate>
                                                            &nbsp;
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField ItemStyle-Width="15%" DataField="uom_code" HeaderText="Unit" ShowHeader="false" ItemStyle-HorizontalAlign="Left" ItemStyle-VerticalAlign="Top" HeaderStyle-CssClass="text-left" Visible="false" HeaderStyle-Font-Size="11px" />
                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                        <div class="col-sm-6" style="border-left: 1px dashed #d0d1d2; margin-left: -1px; width:45%; display:inline-block; vertical-align:top;">
                                            <label id="lblbhs_racik_note" style="font-weight: bold; font-size:11px;">Instruksi Racikan Untuk Farmasi</label>
                                            <br />
                                            <asp:Label ID="lbl_instruksi_racikan_farmasi" runat="server" Text='<%# Eval("compound_note").ToString().Replace("\n","<br />") %>'></asp:Label>
                                            <asp:HiddenField ID="HF_lbl_instruksi_racikan_farmasi" runat="server" Value='<%# Bind("compound_note") %>' />
                                        </div>
                                    </div>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </ContentTemplate>
            </asp:UpdatePanel>

            

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

            <div class="row">

                <table style="width: 100%; margin-top: 80px; margin-bottom:10px;">
                    <tr>
                        <td style="width: 50%; padding-left: 2%; padding-top: 20px">
                            <asp:Label runat="server" ID="lblNamaDokter"></asp:Label>
                            <hr style="margin-top: 0%; margin-bottom: 0%" />
                            <asp:Label runat="server" ID="lblDokterKet" Text="Physician(Dokter)"></asp:Label>
                        </td>
                        <td style="width: 50%; padding-left: 2%; padding-right:2%; padding-top: 40px">
                            <hr style="margin-top: 0%; margin-bottom: 0%" />
                            <label>Patient(Pasien)</label>
                        </td>
                    </tr>
                </table>

            </div>
        

        </div>
    </form>

    <script type="text/javascript">
</script>
</body>
</html>

