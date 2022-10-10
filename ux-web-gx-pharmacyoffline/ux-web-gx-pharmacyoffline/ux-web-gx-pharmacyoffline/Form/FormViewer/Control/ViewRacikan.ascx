<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ViewRacikan.ascx.cs" Inherits="Form_FormViewer_Control_ViewRacikan" %>

<style>
    .font-press-header {
        font-size: 13px;
        font-family: Helvetica, Arial, sans-serif;
        font-weight: bold;
    }

    .font-press {
        font-size: 11px;
    }

    .table-header-racikan {
        width: 100%;
    }

        .table-header-racikan th {
            border: 0;
            font-size: 13px;
        }

        .table-header-racikan td {
            border: 0;
            font-size: 11px;
        }

    .table-detail-racikan {
        width: 100%;
        border: 0;
    }

        .table-detail-racikan th {
            border: 0;
            font-size: 13px;
        }

        .table-detail-racikan td {
            border: 0;
            font-size: 11px;
        }
</style>


<asp:UpdatePanel runat="server">
    <ContentTemplate>

        <div class="row">
            <div class="col-sm-12">

                <asp:GridView ID="gvw_racikan_header" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-condensed table-header-racikan"
                    EmptyDataText="No Data" DataKeyNames="prescription_compound_header_id" OnRowDataBound="gvw_racikan_header_RowDataBound">
                    <Columns>
                        <asp:TemplateField ItemStyle-Width="100%" HeaderStyle-BackColor="#f4f4f4" ItemStyle-CssClass="no-padding">
                            <HeaderTemplate>
                                <table style="width: 100%;">
                                    <tr style="font-weight: bold; background-color: #f4f4f4;">
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
                                        <th style="width: 5%;">
                                            <label id="lblbhs_racik_qty">Qty</label></th>
                                        <th style="width: 5%;">
                                            <label id="lblbhs_racik_uom">UoM</label></th>
                                        <th style="width: 5%;">
                                            <label id="lblbhs_racik_iter">Iter</label></th>
                                    </tr>
                                </table>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:HiddenField ID="HF_headerid_racikan" runat="server" Value='<%# Bind("prescription_compound_header_id") %>' />
                                <%--<asp:HiddenField ID="HF_uomid_racikan" runat="server" Value='<%# Bind("uom_id") %>' />
                                <asp:HiddenField ID="HF_doseuomid_racikan" runat="server" Value='<%# Bind("dose_uom_id") %>' />
                                <asp:HiddenField ID="HF_freqid_racikan" runat="server" Value='<%# Bind("administration_frequency_id") %>' />
                                <asp:HiddenField ID="HF_routeid_racikan" runat="server" Value='<%# Bind("administration_route_id") %>' />
                                <asp:HiddenField ID="HF_isdosetext_racikan" runat="server" Value='<%# Bind("IsDoseText") %>' />--%>
                                <table style="width: 100%;" class="table-bordered table-condensed">
                                    <tr>
                                        <td style="width: 30%; font-weight: bold; padding-left: 15px;">
                                            <div>
                                                <asp:Label ID="lbl_nama_racikan" runat="server" Text='<%# Bind("compound_name") %>'></asp:Label>
                                            </div>
                                        </td>
                                        <td style="width: 15%;">
                                            <asp:Label ID="lbl_dosis_racikan" runat="server" Text='<%# Bind("dose") %>' Style='<%# Eval("IsDoseText").ToString().ToLower() == "true" ? "display:none": "display:inline" %>'></asp:Label>
                                            <asp:Label ID="lbl_dosisunit_racikan" runat="server" Text='<%# Bind("dose_uom") %>' Style='<%# Eval("IsDoseText").ToString().ToLower() == "true" ? "display:none": "display:inline" %>'></asp:Label>
                                            <asp:Label ID="lbl_dosistext_racikan" runat="server" Text='<%# Bind("dose_text") %>' Style='<%# Eval("IsDoseText").ToString().ToLower() == "true" ? "display:inline": "display:inline" %>'></asp:Label>
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
                                    <div class="col-sm-6" style="border-right: 1px dashed #d0d1d2;">
                                        <asp:GridView ID="gvw_racikan_detail" runat="server" AutoGenerateColumns="False" CssClass="table-detail-racikan"
                                            DataKeyNames="prescription_compound_detail_id">
                                            <Columns>
                                                <asp:BoundField ItemStyle-Width="50%" DataField="item_name" HeaderText="Item" ShowHeader="false" ItemStyle-VerticalAlign="Top" />
                                                <asp:TemplateField ItemStyle-Width="15%" HeaderText="Dose" ShowHeader="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbldose" runat="server" Text='<%# Bind("dose") %>' Style='<%# Eval("IsDoseText").ToString().ToLower() == "true" ? "display:none": "display:inline" %>'></asp:Label>
                                                        <asp:Label ID="lbldoseuom" runat="server" Text='<%# Bind("dose_uom_code") %>' Style='<%# Eval("IsDoseText").ToString().ToLower() == "true" ? "display:none": "display:inline" %>'></asp:Label>
                                                        <asp:Label ID="lbldosetext" runat="server" Text='<%# Bind("dose_text") %>' Style='<%# Eval("IsDoseText").ToString().ToLower() == "true" ? "display:inline": "display:inline" %>'></asp:Label>
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
                                    <div class="col-sm-6" style="border-left: 1px dashed #d0d1d2; margin-left: -1px">
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
        </div>

    </ContentTemplate>
</asp:UpdatePanel>

