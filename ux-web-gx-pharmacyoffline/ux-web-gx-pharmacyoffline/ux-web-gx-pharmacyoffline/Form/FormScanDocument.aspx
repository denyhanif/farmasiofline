<%@ Page Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="FormScanDocument.aspx.cs" Inherits="Form_FormScanDocument" %>
<%@ MasterType VirtualPath="~/Site.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <style type="text/css">
        
        .sidenavleft {
            height: 100%;
            width: 20%;
            position: fixed;
            z-index: 1;
            background-color: white;
            padding-top: 14px;
            top: 0;
        }

        .innersidenavleft {
            position: fixed;
            width: 20%;
            z-index: 1;
            background-color: white;
            padding-top: 20px;
            top: 0;
        }
        
        .valueofprescription {
            font-size: 13px;
            font-family: Helvetica;
            text-align: center;
        }

        .valueofprescriptiontime {
            font-size: 10px;
            font-family: Helvetica;
            text-align: right;
        }

        .table-hover tbody tr:hover td, .table-hover tbody tr:hover th {
          background-color: #d6dbff;
        }

    </style>
   

    <body>
        <asp:UpdatePanel runat="server" ID="updateBIG" UpdateMode="Conditional">
            <ContentTemplate>

                
                <%-- ===============================================NAV LEFT===================================================================== --%>
                <div id="leftSection" class="col-sm-3" style="margin-left: -15px">
                    <table>
                        <tr>
                            <td>
                                <div class="sidenavleft">
                                    <div class="innersidenavleft" style="padding-top: 40px; background-color:#E7E8EF; padding-bottom:20px">
                                        <table style="width: 100%;">
                                            <tr>
                                                <td style="width: 50%;">
                                                    <h4 style="padding: 8px 8px 8px 10px; font-size: 16px;"><b>Worklist</b></h4>
                                                </td>
                                                <td style="width: 50%; text-align: right; padding-right: 15px;">
                                                    <asp:UpdateProgress ID="UpdateProgress2" runat="server" AssociatedUpdatePanelID="upSearch">
                                                        <ProgressTemplate>
                                                            <div class="modal-backdrop" style="background-color: white; opacity: 0; text-align: center">
                                                            </div>
                                                            <img alt="" style="background-color: transparent; height: 20px;" src="<%= Page.ResolveClientUrl("~/Images/Background/small-loader.gif") %>" />
                                                        </ProgressTemplate>
                                                    </asp:UpdateProgress>
                                                </td>
                                            </tr>
                                        </table>

                                        <div id="New" class="tabcontent">
                                            <div>
                                                <div class="col-sm-6" style="padding-left: 5%; padding-right: 0px;">
                                                    <label style="font-family: Helvetica; font-size: 12px;">From Date</label>
                                                    <br />
                                                    <asp:TextBox Style="font-family: Helvetica; font-size: 12px" ID="txtDateFromNew" AutoPostBack="true" runat="server" CssClass="shadows-search" Width="76%" Height="24px" onmousedown="dateStartNew();" AutoCompleteType="Disabled"></asp:TextBox>
                                                </div>
                                                <div class="col-sm-6" style="margin-left:-10%">
                                                    <label style="font-family: Helvetica; font-size: 12px;">To Date</label>
                                                    <br />
                                                    <asp:TextBox Style="font-family: Helvetica; font-size: 12px" ID="txtToDateNew" AutoPostBack="true" runat="server" CssClass="shadows-search" Width="80%" Height="24px" onmousedown="dateEndNew();" AutoCompleteType="Disabled"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div>
                                                <div class="col-sm-12">
                                                    <label style="font-family: Helvetica; font-size: 12px">Search</label>
                                                    <br />
                                                    <asp:TextBox Style="font-family: Helvetica" placeholder="MR number" Font-Size="12px" ID="txtSearch" runat="server" CssClass="shadows-search" Width="80%" Height="24px" OnTextChanged="btnSearchWorklist_OnCLick" AutoPostBack="true" Font-Names="Helvetica"></asp:TextBox>

                                                    <asp:UpdatePanel runat="server" ID="upSearch" UpdateMode="Conditional" style="margin-left: 82%; margin-top: -10%;">
                                                        <ContentTemplate>
                                                            <asp:Button runat="server" ID="btnSearchWorklist" OnClick="btnSearchWorklist_OnCLick" CssClass="btn btn-lightGreen" Width="51px" Height="24px" Text="Search" style="padding:0px;" Font-Names="helvetica" Font-Size="11px"/>
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>

                                                    <asp:UpdateProgress ID="UpdateProgress3" runat="server" AssociatedUpdatePanelID="updateGrid">
                                                        <ProgressTemplate>
                                                            <div class="modal-backdrop" style="background-color: white; opacity: 0; text-align: center">
                                                            </div>
                                                        </ProgressTemplate>
                                                    </asp:UpdateProgress>


                                                </div>
                                            </div>
                                        </div>                                        
                                    </div>


                                    <asp:HiddenField ID="hdnScrollValue" runat="server" />



                                    <asp:UpdatePanel ID="updateGrid" runat="server">
                                        <ContentTemplate>

                                            <div runat="server" id="divPatienHeader" class="row" style="margin-top: 74%; margin-left: 3%; 
                                                margin-right: 3% ; font-family:Arial, Helvetica, sans-serif; font-size:12px;">
                                                <div class="row">
                                                    <div class="col-sm-12">
                                                        <asp:Label runat="server" ID="lblPatientName" Text="NAMA PASIEN" Font-Bold="true" Font-Size="13px"></asp:Label>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-sm-6">
                                                        <label>MR : </label>
                                                        <asp:Label runat="server" ID="lblMRno" Text="-"></asp:Label>
                                                    </div>
                                                    <div class="col-sm-6">
                                                        <label>DOB : </label>
                                                        <asp:Label runat="server" ID="lblDOB" Text="-"></asp:Label>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-sm-12">
                                                        <label>Seks : </label>
                                                        <asp:Label runat="server" ID="lblGender" Text="-"></asp:Label>
                                                    </div>
                                                </div>
                                                <hr style="margin-bottom: 5px; margin-top: 5px;"/>
                                            </div>

                                            
                                            
                                    <%-----------------------------------------------------------images section start-------------------------------------------------------------%>
                                    <div runat="server" id="imageSection" visible="false">
                                        <div class="col-sm-12" style="background-color: transparent; border-radius: 6px; width: 98%; padding-left: 5px; padding-right: 0px; height: 100%">
                                            <div id="divNoData" runat="server" style="text-align: center; margin-top: 15%; margin-bottom: 5%" visible="true">
                                                <asp:Image ID="imgNoData" Font-Names="Helvetica" Font-Bold="true" Font-Size="12px" runat="server" Text="image" ImageUrl="~/Images/Icon/ic_noData.svg" Width="50%"></asp:Image>
                                            </div>
                                            <div style="text-align: center">
                                                <asp:Label ID="lblOops" runat="server" Text="Data not found" Font-Size="16px" Font-Bold="true" Font-Names="Helvetica" Visible="true" ForeColor="#585A6F"></asp:Label>
                                            </div>
                                            <div style="text-align: center">
                                                <asp:Label ID="lblPlease" runat="server" Text="Please search another date or parameter" Font-Size="12px" Font-Names="Helvetica" Visible="true" ForeColor="#585A6F"></asp:Label>
                                            </div>
                                        </div>
                                    </div>
                                    <%-----------------------------------------------------------images section end-----------------------------------------------------------%>


                                            <div id="myDiv" class="tabcontentprescription" style="padding-left: 0px; padding-right: 0px; width: 100%; overflow: hidden auto; max-height: calc(100vh - 278px);" onscroll="scollPos(this);">
                                                <asp:Button ID="btnPatient" CssClass="hidden" OnClick="btnPatientName_Click" runat="server" Text="Submit" ForeColor="White" Style="background: green; border-radius: 5px; width: 80px;" />
                                                <asp:GridView ID="gvw_worklist" runat="server" AutoGenerateColumns="False" CssClass="table table-striped table-bordered table-condensed table-hover" BorderColor="Black"
                                                    HeaderStyle-CssClass="text-center" HeaderStyle-HorizontalAlign="Center"
                                                    ShowHeaderWhenEmpty="True" DataKeyNames="AdmissionNo" ShowHeader="false"
                                                    AllowSorting="True" Style="border-bottom: none" Font-Names="Helvetica">
                                                    <Columns>
                                                        <asp:TemplateField ItemStyle-CssClass="collapsediv" HeaderText="" ItemStyle-Width="5%" HeaderStyle-ForeColor="#3C8DBC" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                                            <ItemTemplate>
                                                                <asp:LinkButton id="btnAdmission" runat="server" OnClick="btnDataAdmission_OnCLick" style="color:#171717">
                                                                    <div class="row">
                                                                        <div class="col-sm-12">
                                                                            <label style="font-size: 13px; font-weight:bold"><%# Eval("AdmissionNo") %> </label>
                                                                            <asp:HiddenField ID="hdnPatientId" runat="server" Value='<%# Bind("patientId") %>'></asp:HiddenField>
                                                                            <asp:HiddenField ID="hdnAdmissionid" runat="server" Value='<%# Bind("admissionid") %>'></asp:HiddenField>
                                                                        </div>
                                                                    </div>
                                                                    <div class="row">
                                                                        <div class="col-sm-12">
                                                                            <table border="0" id="tbl_worklist_pharmacy" class="tbl_worklist">
                                                                                <tr>
                                                                                    <td style="width: 20%; font-family:Arial, Helvetica, sans-serif; font-size:13px">Doctor </td>
                                                                                    <td style="width: 5%;  font-family:Arial, Helvetica, sans-serif; font-size:13px"">: </td>
                                                                                    <td style="width: 75%;  font-family:Arial, Helvetica, sans-serif; font-size:13px""><%# Eval("DoctorName") %> </td>
                                                                                </tr>
                                                                            </table>
                                                                        </div>
                                                                    </div>
                                                                    <div class="row"  style="margin-bottom: 5px;">
                                                                        <div class="col-sm-12">
                                                                            <asp:Label style="font-family:Arial, Helvetica, sans-serif; font-size:12px" ID="lblAdmissionDate" runat="server" Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem,"AdmissionDate")).ToString("dd MMM yyyy")%>'></asp:Label>
                                                                        </div>
                                                                    </div>
                                                                </asp:LinkButton>
                                                                
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </div>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                            </td>
                        </tr>
                    </table>
                </div>
                <%-- ===============================================NAV LEFT END===================================================================== --%>


                <%--===================================================CONTENT START===================================================--%>
                <div runat="server" id="divFrame" style="margin-top:8%">
                    <asp:UpdatePanel runat="server" ID="upIframe">
                        <ContentTemplate>
                            <iframe name="myIframe" id="myIframe" runat=server style="width:100%; height: calc(100vh - 54px); border:none; margin-top:-8%; margin-left:11%; overflow-y:auto"></iframe>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>                
                <%--====================================================CONTENT END===================================================--%>
                               
                </ContentTemplate>
        </asp:UpdatePanel>
        
        <script type="text/javascript">

            function dateStartNew() {
                var dp = $('#<%=txtDateFromNew.ClientID%>');
                dp.datepicker({
                    changeMonth: true,
                    changeYear: true,
                    format: "dd M yyyy",
                    language: "tr"
                }).on('changeDate', function (ev) {
                    $(this).blur();
                    $(this).datepicker('hide');
                });
            }

            function dateEndNew() {
                var dp = $('#<%=txtToDateNew.ClientID%>');
                dp.datepicker({
                    changeMonth: true,
                    changeYear: true,
                    format: "dd M yyyy",
                    language: "tr"
                }).on('changeDate', function (ev) {
                    $(this).blur();
                    $(this).datepicker('hide');
                });
            }

            function CheckNumeric()
            {
                return event.keyCode >= 48 && event.keyCode <= 57 || event.keyCode == 46;
            }
            function validateFloatKeyPress(el)
            {
                var v = parseFloat(el.value);
                var strv = el.value.split('.');
                var strval = strv[1];
                if (strval != null) {
                    if (strval.length == 3) {
                        if (strval == "000") {
                            el.value = strv[0];
                        }
                        else
                            el.value = (isNaN(v)) ? '' : v.toFixed(3);
                    }
                    else if (strval.length == 2) {
                        if (strval == "00") {
                            el.value = strv[0];
                        }
                        else
                            el.value = (isNaN(v)) ? '' : v.toFixed(2);
                    }
                    else if (strval.length == 1) {
                        if (strval == "0") {
                            el.value = strv[0];
                        }
                        else
                            el.value = (isNaN(v)) ? '' : v.toFixed(2);
                    }
                    else
                        el.value = (isNaN(v)) ? '' : v.toFixed(2);
                }
            }

        </script>
    </body>
</asp:Content>
