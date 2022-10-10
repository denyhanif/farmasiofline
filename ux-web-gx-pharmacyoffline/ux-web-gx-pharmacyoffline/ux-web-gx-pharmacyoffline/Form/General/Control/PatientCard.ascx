<%@ Control Language="C#" AutoEventWireup="true" CodeFile="PatientCard.ascx.cs" Inherits="Form_General_Control_PatientCard" %>

<asp:UpdatePanel runat="server">
    <ContentTemplate>

        <div class="row">
            <div class="col-sm-2" style="padding-top:10px; padding-right:0px; width:100px; margin-left:5px;">
                <asp:Image runat="server" ID="Image1" Width="54px" Height="54px" Style="vertical-align: bottom;" />
                <asp:Image runat="server" ID="imgSex" Width="25px" style="margin-left:-15px; " />
            </div>
            <div class="col-sm-10" style="padding-left:0px; width:86%;">
                <div class="input-group">
                    <h5>
                        <asp:Label ID="patientName" class="form-group" runat="server" Font-Bold="true" Font-Size="13px"></asp:Label>
                        <asp:Label ID="Label2" class="form-group" runat="server" Font-Bold="true" ForeColor="LightGray" Font-Size="14px">&nbsp;|&nbsp; </asp:Label>
                        <asp:Label ID="localMrNo" class="form-group" runat="server" Font-Bold="true" Font-Size="12px"></asp:Label>
                        <asp:Label ID="Label4" class="form-group" runat="server" Font-Bold="true" ForeColor="LightGray" Font-Size="14px">&nbsp;|&nbsp;</asp:Label>
                        <asp:Label ID="primaryDoctor" class="form-group" runat="server" Font-Bold="true" Font-Size="12px"></asp:Label>
                    </h5>
                </div>
                <div role="group" style="width: 100%" aria-label="...">
                    <div class="btn-group" role="group" style="width: 115px; vertical-align: top">
                        <asp:Label CssClass="form-group" runat="server">
                            <label style="display: <%=setENG%>;"> Admission No. </label>
                            <label style="display: <%=setIND%>;"> No. Admisi </label>
                        </asp:Label><br />
                        <asp:Label CssClass="form-group" Font-Bold="true" runat="server" ID="lblAdmissionNo"></asp:Label>
                    </div>
                    <div class="btn-group" role="group" style="width: 95px; vertical-align: top">
                        <asp:Label CssClass="form-group" runat="server" ToolTip="Date of Birth">
                            <label style="display: <%=setENG%>;"> DOB </label>
                            <label style="display: <%=setIND%>;"> Tgl. Lahir </label>
                        </asp:Label><br />
                        <asp:Label CssClass="form-group" Font-Bold="true" runat="server" ID="lblDOB"></asp:Label>
                    </div>
                    <div class="btn-group" role="group" style="width: 95px; vertical-align: top">
                        <asp:Label CssClass="form-group" runat="server">
                            <label style="display: <%=setENG%>;"> Age </label>
                            <label style="display: <%=setIND%>;"> Umur </label>
                        </asp:Label><br />
                        <asp:Label CssClass="form-group" Font-Bold="true" runat="server" ID="lblAge"></asp:Label>
                    </div>
                    <div class="btn-group" role="group" style="width: 95px; vertical-align: top">
                        <asp:Label CssClass="form-group" runat="server">
                            <label style="display: <%=setENG%>;"> Religion </label>
                            <label style="display: <%=setIND%>;"> Agama </label>
                        </asp:Label><br />
                        <asp:Label CssClass="form-group" Font-Bold="true" runat="server" ID="lblReligion"></asp:Label>
                    </div>
                    <div class="btn-group" role="group" style="max-width: 50%; vertical-align: top">
                        <asp:Label CssClass="form-group" runat="server">
                            <label style="display: <%=setENG%>;"> Payer </label>
                            <label style="display: <%=setIND%>;"> Penanggung </label>
                        </asp:Label><br />
                        <asp:Label CssClass="form-group" style="font-weight:bold; width:100%;" runat="server" ID="lblPayer"></asp:Label>
                    </div>
                </div>
            </div>
        </div>

    </ContentTemplate>
</asp:UpdatePanel>

