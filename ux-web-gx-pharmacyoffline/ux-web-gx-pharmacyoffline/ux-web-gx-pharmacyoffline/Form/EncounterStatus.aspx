<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EncounterStatus.aspx.cs" Inherits="Form_EncounterStatus" %>
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
    <link rel="stylesheet" href="~/Content/EMR-Doctor.css" />
</head>
<body style="background-color:#cdd2dd; padding:0px">
    <form id="form1" runat="server">
        <script type="text/javascript">
            function txtOnKeyPressFalse() {
                var c = event.keyCode;
                if (c == 13) {
                    document.getElementById('<%=btn_search.ClientID%>').click();
                    return false;
                }
            }
            function dateStart() {
                var dp = $('#<%=srcStartDate.ClientID%>');
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
            function dateEnd() {
                var dp = $('#<%=srcEndDate.ClientID%>');
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
            function checkFilter() {
                var srcKey = document.getElementById("<%= srcKey.ClientID%>");
                if (srcKey.value == "") {
                    srcKey.focus();
                    srcKey.placeholder = "Fill this field ..."
                    srcKey.classList.add("placeholderred");
                    return false;
                } else {
                    document.getElementById("<%= btnSearch.ClientID%>").click();
                    return false;
                }
            }

            $(document).ready(function () {
                var dp = $('#<%=srcStartDate.ClientID%>');
                dp.datepicker({
                    changeMonth: true,
                    changeYear: true,
                    format: "dd M yyyy",
                    language: "tr"
                }).on('changeDate', function (ev) {
                    $(this).blur();
                    $(this).datepicker('hide');
                });

                var dp = $('#<%=srcEndDate.ClientID%>');
                dp.datepicker({
                    changeMonth: true,
                    changeYear: true,
                    format: "dd M yyyy",
                    language: "tr"
                }).on('changeDate', function (ev) {
                    $(this).blur();
                    $(this).datepicker('hide');
                });
            });
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
    
        
    <asp:UpdatePanel runat="server" ID="upReportStatusDoctor">
        <ContentTemplate>
            <input type="hidden" id="orgId" runat="server" />
            <div class="row" style="height: 60px; padding-left:19px ;padding-top:8px; background-color:#E7E8ED; font-size: smaller;">
                <div class="col-sm-1" style="width: 14%;">
                    <div>MR atau Nama Dokter</div>
                    <div><asp:TextBox ID="srcKey" runat="server" placeholder="MR or doctor name" onkeydown="return txtOnKeyPressFalse();" AutoPostBack="true"></asp:TextBox></div>
                </div>
                <div class="col-sm-4">
                    <div>Tanggal Pencarian</div>
                    <div>
                        <asp:TextBox ID="srcStartDate" runat="server" placeholder="Start Date" onmousedown="dateStart();" AutoCompleteType="Disabled"></asp:TextBox> 
                        - 
                        <asp:TextBox ID="srcEndDate" runat="server" placeholder="End Date" onmousedown="dateEnd();" AutoCompleteType="Disabled"></asp:TextBox>
                        &nbsp;
                        <asp:Button CssClass="btn btn-lightGreen" ID="btn_search" Text="Search" OnClientClick="return checkFilter();" runat="server" style="height:22px;width:60px; padding:2px; font-size:13px" />
                    </div>
                </div>
                <%--<div class="col-sm-1">
                    &nbsp;
                    <div><asp:Button CssClass="btn btn-lightGreen" ID="btn_search" Text="Search" OnClick="btn_search_Click" runat="server" /></div>
                </div>--%>
            </div>

            <div>
                <asp:UpdateProgress ID="uProgWorklist" runat="server" AssociatedUpdatePanelID="upReportStatusDoctor">
                    <ProgressTemplate>
                        <div style="background-color: white; opacity:0.9; text-align: center; z-index: 5; position: fixed; width: 100%; left: 0px; height: calc(100vh - 50px); border-radius: 6px;">
                                <div class="modal-backdrop" style="background-color: red; opacity: 0; text-align: center">
                                </div>
                                <div style="margin-top: 12%;">
                                    <img alt="" height="225px" width="225px" style="background-color: transparent; vertical-align: middle;" class="login-box-body" src="<%= Page.ResolveClientUrl("~/Images/Background/loading-beat.gif") %>" />
                                </div>
                            </div>
                    </ProgressTemplate>
                </asp:UpdateProgress>
            </div>



            <div style="background-color:white;width: 100%; font-size: small; padding: 15px 15px 1px 15px;">
                
                <div>
                    <asp:Button ID="btnSearch" OnClick="btnSearch_Click" runat="server" Text="search" style="display:none"/>
                    <input type="hidden" id="HiddenPageIndex" runat="server" />
                    <label style="font-family:Arial, Helvetica, sans-serif;font-size:24px; color:black">Tabel Status Encounter</label>
                    <asp:GridView runat="server" ID="gvwEncounter" ShowHeaderWhenEmpty="true" AutoGenerateColumns="false" AllowPaging="True" PageSize="8"
                        HeaderStyle-CssClass="text-center" HeaderStyle-HorizontalAlign="Center" CssClass="table table-condensed table-hover-worklist" BorderColor="#B9B9B9" HeaderStyle-BorderColor="#B9B9B9"
                        EmptyDataText="No Data" AllowSorting="True" OnPageIndexChanging="gvwEncounter_PageIndexChanging">
                        <PagerStyle CssClass="pagination-ys" />
                        <Columns>
                            <asp:TemplateField HeaderText="No. MR" HeaderStyle-BackColor="#f2f3f4" ItemStyle-Width="3%" HeaderStyle-ForeColor="#0013b5" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" HeaderStyle-BorderColor="#B9B9B9">
                                <ItemTemplate>
                                    <asp:Label runat="server"><%# Eval("LocalMrNo").ToString() %></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Admission Id" HeaderStyle-BackColor="#f2f3f4" ItemStyle-Width="3%" HeaderStyle-ForeColor="#0013b5" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" HeaderStyle-BorderColor="#B9B9B9">
                                <ItemTemplate>
                                    <asp:Label runat="server"><%# Eval("AdmissionNo").ToString() %></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                             <asp:TemplateField HeaderText="Nama Dokter" HeaderStyle-BackColor="#f2f3f4" ItemStyle-Width="9%" HeaderStyle-ForeColor="#0013b5" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" HeaderStyle-BorderColor="#B9B9B9">
                                <ItemTemplate>
                                    <asp:Label runat="server"><%# Eval("DoctorName").ToString() %></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                             <asp:TemplateField HeaderText="Nama Pasien" HeaderStyle-BackColor="#f2f3f4" ItemStyle-Width="7%" HeaderStyle-ForeColor="#0013b5" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" HeaderStyle-BorderColor="#B9B9B9">
                                <ItemTemplate>
                                    <asp:Label runat="server"><%# Eval("PatientName").ToString() %></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                             <asp:TemplateField HeaderText="Tgl. Admisi" HeaderStyle-BackColor="#f2f3f4" ItemStyle-Width="6%" HeaderStyle-ForeColor="#0013b5" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" HeaderStyle-BorderColor="#B9B9B9">
                                <ItemTemplate>
                                    <asp:Label runat="server"><%# Eval("AdmissionDate", "{0:dd MMM yyyy, HH:mm}") %></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Status Dokter" HeaderStyle-BackColor="#f2f3f4" ItemStyle-Width="6%" HeaderStyle-ForeColor="#0013b5" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" HeaderStyle-BorderColor="#B9B9B9">
                                <ItemTemplate>
                                    <asp:Label runat="server"><%# Eval("DoctorStatus").ToString() %></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Lab" HeaderStyle-BackColor="#f2f3f4" ItemStyle-Width="2%" HeaderStyle-ForeColor="#0013b5" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" HeaderStyle-BorderColor="#B9B9B9">
                                <ItemTemplate>
                                    <asp:Label runat="server"><%# Eval("OrderLaboratory").ToString() %></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Rad" HeaderStyle-BackColor="#f2f3f4" ItemStyle-Width="2%" HeaderStyle-ForeColor="#0013b5" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" HeaderStyle-BorderColor="#B9B9B9">
                                <ItemTemplate>
                                    <asp:Label runat="server"><%# Eval("OrderRadiology").ToString() %></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Resep" HeaderStyle-BackColor="#f2f3f4" ItemStyle-Width="2%" HeaderStyle-ForeColor="#0013b5" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" HeaderStyle-BorderColor="#B9B9B9">
                                <ItemTemplate>
                                    <asp:Label runat="server"><%# Eval("DoctorPrescription").ToString() %></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    </form>
</body>
</html>