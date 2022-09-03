<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ViewDrugs.ascx.cs" Inherits="Form_FormViewer_Control_ViewDrugs" %>

<style>
    .font-press-header {
        font-size: 13px;
        font-family: Helvetica, Arial, sans-serif;
        font-weight: bold;
    }

    .font-press {
        font-size: 11px;
    }
</style>


<asp:UpdatePanel runat="server">
    <ContentTemplate>
        
        <div class="row">
            <div class="col-sm-12">

                <asp:GridView ID="GvwDrugsOrder" runat="server" AutoGenerateColumns="False" CssClass="table table-striped table-bordered table-condensed thinscroll"
                    EmptyDataText="No Data" Font-Names="Helvetica">
                    <Columns>
                        <asp:TemplateField HeaderText="Item" HeaderStyle-CssClass="font-press-header">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblitemname" CssClass="font-press" Text='<%# Bind("salesItemName") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Dose" HeaderStyle-CssClass="font-press-header">
                            <ItemTemplate>
                                <div runat="server" visible='<%# Eval("IsDoseText").ToString() == "False" %>'>
                                    <asp:Label runat="server" ID="lbldose" CssClass="font-press" Text='<%# Bind("dose") %>'></asp:Label>
                                    &nbsp;
                                    <asp:Label runat="server" ID="lbldoseuom" CssClass="font-press" Text='<%# Bind("dose_uom") %>'></asp:Label>
                                </div>
                                <asp:Label runat="server" ID="lbldosetext" CssClass="font-press" Visible='<%# Eval("IsDoseText") %>' Text='<%# Bind("doseText") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Frequency" HeaderStyle-CssClass="font-press-header">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblfrequency" CssClass="font-press" Text='<%# Bind("frequency") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Route" HeaderStyle-CssClass="font-press-header">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblroute" CssClass="font-press" Text='<%# Bind("route") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Instruction" HeaderStyle-CssClass="font-press-header">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblinstruction" CssClass="font-press" Text='<%# Bind("instruction") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Qty" HeaderStyle-CssClass="font-press-header">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblqty" CssClass="font-press" Text='<%# Bind("quantity") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="UoM" HeaderStyle-CssClass="font-press-header">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lbluom" CssClass="font-press" Text='<%# Bind("uom") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Iter" HeaderStyle-CssClass="font-press-header">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lbliter" CssClass="font-press" Text='<%# Bind("iter") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Routine" HeaderStyle-CssClass="font-press-header">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblroutine" CssClass="font-press" Text='<%# Eval("isRoutine").ToString().ToLower() == "false" ? "No" : "Yes" %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>

            </div>
        </div>

    </ContentTemplate>
</asp:UpdatePanel>

