<%@ Control Language="C#" AutoEventWireup="true" CodeFile="StdLabResult.ascx.cs" Inherits="Form_General_Control_StdLabResult" %>

<asp:UpdatePanel runat="server">
    <ContentTemplate>
        <div runat="server" ID="panel1"></div>
<%--<div style="margin-top:2%;text-align:center;border-radius:4px; margin-left:1%; margin-right:1%">
    <asp:Label runat="server" ID="lblNoData" Text="No Laboratory Data" Visible="true" Font-Names="Helvetica" Font-Size="25px"></asp:Label>
</div>--%>
        <div id="img_noData" runat="server" visible="false" style="text-align: center;">
            <div>
                <img src="<%= Page.ResolveClientUrl("~/Images/Background/ic_noData.svg") %>" style="height:auto;width:200px;margin-right:3px;margin-top:40px" />
            </div>
            <div runat="server">
                <span><h3  style="font-weight:700; color:#585A6F">Oops! There is no data</h3></span>
                <span style="font-size:14px; color:#585A6F">Please search another date or parameter</span>
            </div>
        </div>
        <div id="img_noConnection" runat="server" visible="false" style="text-align: center;">
            <img src="<%= Page.ResolveClientUrl("~/Images/Background/ic_noConnection.svg") %>" style="height:auto;width:200px;margin-right:3px;margin-top:40px;" />
            <span><h3  style="font-weight:700; color:#585A6F">No internet connection</h3></span>
            <span style="font-size:14px; color:#585A6F">Please check your connection & refresh</span>
        </div>
    </ContentTemplate>
</asp:UpdatePanel>