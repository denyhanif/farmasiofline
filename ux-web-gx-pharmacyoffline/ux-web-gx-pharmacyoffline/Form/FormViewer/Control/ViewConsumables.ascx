<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ViewConsumables.ascx.cs" Inherits="Form_FormViewer_Control_ViewConsumables" %>

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

                <asp:GridView ID="GvwConsumablesOrder" runat="server" AutoGenerateColumns="False" CssClass="table table-striped table-bordered table-condensed thinscroll"
                    EmptyDataText="No Data" Font-Names="Helvetica">
                    <Columns>
                        <asp:TemplateField HeaderText="Item" HeaderStyle-CssClass="font-press-header">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblitemname" CssClass="font-press" Text='<%# Bind("salesItemName") %>'></asp:Label>
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
                        <asp:TemplateField HeaderText="Instruction" HeaderStyle-CssClass="font-press-header">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblinstruction" CssClass="font-press" Text='<%# Bind("instruction") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>                       
                    </Columns>
                </asp:GridView>

            </div>
        </div>

    </ContentTemplate>
</asp:UpdatePanel>

