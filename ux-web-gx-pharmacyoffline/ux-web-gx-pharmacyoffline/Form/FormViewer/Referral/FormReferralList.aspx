<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FormReferralList.aspx.cs" Inherits="Form_FormViewer_Referral_FormReferralList" %>

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
    <!-- Datepicker -->
    <link rel="stylesheet" href="~/Content/plugins/datepicker/datepicker3.css" />
    <!-- Theme style -->
    <link rel="stylesheet" href="~/Content/dist/css/AdminLTE.css" />
    <link rel="stylesheet" href="~/Content/dist/css/skins/skin-blue-light.css" />
    <link rel="stylesheet" href="~/Content/Site.css" />

    <link href="~/favicon.ico" rel="shortcut icon" type="image/x-icon" />

    <style type="text/css">
       .table-sub-header-label {
            color: #00109c;
            background-color: #F2F3F4;
        }
    </style>
</head>
<body style="padding-top: 15px;">
    <form id="form1" runat="server">

        <asp:ScriptManager ID="ScriptManager1" runat="server">
            <Scripts>
                <asp:ScriptReference Path="~/Content/plugins/jQuery/jQuery-2.2.0.min.js" />
                <asp:ScriptReference Path="~/Content/bootstrap/js/bootstrap.min.js" />
                <asp:ScriptReference Path="~/Content/plugins/datepicker/bootstrap-datepicker.js" />
            </Scripts>
        </asp:ScriptManager>

        <asp:UpdatePanel ID="UP_ReferralList" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <div>
                    <asp:Repeater runat="server" ID="rptReferralList">
                        <ItemTemplate>
                            <div>
                                <row>
                                    <asp:Label ID="Label1" Style="font-family: Helvetica; font-weight: bold; font-size:16px; line-height:50px;" runat="server" Visible='<%# Eval("IsSelf").ToString() == "0" ? true : false %>' Text="Rujukan Untuk Dokter Lain"></asp:Label>
                                    <table>
                                        <tr>
                                            <td><asp:Label ID="Label2" runat="server" Text="Kepada TS"></asp:Label></td>
                                            <td style="padding-left:15px; padding-right:5px;"> : </td>
                                            <td><asp:Label ID="Label5"  Style="font-weight: bold;" runat="server" Text='<%# Bind("DoctorReferral") %>'></asp:Label></td>
                                            <td style="padding-left:15px;"><div style="background-color: #228b22; color:white; border-radius: 10px 10px 10px 10px;"><asp:Label ID="Label9" runat="server" Text="" style="margin-left:5px; margin-right:5px;"> <i class="fa fa-check-circle"></i>
                                                <label id="lblbhs_noreminder"> <%#Eval("referral_type").ToString()%> </label>
                                            </asp:Label></div></td>
                                        </tr>
                                        <tr>
                                            <td><asp:Label ID="Label3" runat="server" Text="Dari TS"></asp:Label></td>
                                            <td style="padding-left:15px; padding-right:5px;"> : </td>
                                            <td><asp:Label ID="Label6"  runat="server" Text='<%# Bind("DoctorName") %>'></asp:Label></td>
                                            <td style="padding-left:15px;"></td>
                                        </tr>
                                        <tr>
                                            <td><asp:Label ID="Label4" runat="server" Text="Registrasi"></asp:Label></td>
                                            <td style="padding-left:15px; padding-right:5px;"> : </td>
                                            <td><asp:Label ID="Label7" runat="server" Text='<%# Bind("CreatedDate") %>'></asp:Label> / <asp:Label ID="Label8" runat="server" Text='<%# Bind("AdmissionNo") %>'></asp:Label></td>
                                            <td style="padding-left:15px;"></td>
                                        </tr>
                                    </table>
                                    </br>
                                </row>
                                <row>
                                    <asp:Label ID="Label10" Style="font-family: Helvetica; font-size:12px; line-height:20px;" runat="server" Visible='<%# Eval("IsSelf").ToString() == "1" ? true : false %>' Text="Mohon konsultasi dan tindak lanjut dengan :"></asp:Label>
                                    <asp:TextBox runat="server" CssClass="text-multiline-dialog" style="border: solid; border-width: thin; background-color: #E2E2E2; max-width: 100%; width: 100%; height: 100px; resize: none;" BorderColor="transparent" ID="txtRemark" TextMode="MultiLine" Text='<%# Bind("referral_remark") %>' />
                                    </br>
                                </row>
                                <row>
                                    <a href="javascript:void(0);" style="cursor: pointer;">
                                        <asp:Label ID="Label12" runat="server" onclick ="openTabSoap(this.id)"  Text="" Style="cursor: pointer; font-family: Helvetica; font-weight: bold; font-size:12px;"> <i class="fa fa-caret-down"></i>
                                            <label id="Label11" style="cursor: pointer;">SOAP</label>
                                        </asp:Label>
                                    </a>
                                    <div runat="server" id="soapTable" style='<%# Eval("IsSelf").ToString() == "0" ? "display:none;" : "display:block;" %>' >
                                        <table class="table-condensed table-fill-width" cellspacing="0" rules="all" style="width: 100%; border-top: 1px solid #b9b9b9; border-color:#B9B9B9;border-width:0px;border-collapse:collapse;">
                                            <tr>
                                                <th class="font-content-dashboard table-sub-header-label" style="width:15%;">Doctor</th>
                                                <th class="font-content-dashboard table-sub-header-label" style="width:15%;">Subjective</th>
                                                <th class="font-content-dashboard table-sub-header-label" style="width:15%;">Objective</th>
                                                <th class="font-content-dashboard table-sub-header-label" style="width:15%;">Assestment</th>
                                                <th class="font-content-dashboard table-sub-header-label" style="width:15%;">Planning</th>
                                                <th class="font-content-dashboard table-sub-header-label" style="width:25%;">Prescription</th>
                                            </tr>
                                            <tr>
                                                <td><asp:Label Font-Size="12px" Font-Names="Helvetica, Arial, sans-serif" ID="Label13" runat="server" Text='<%# Bind("DoctorName") %>'></asp:Label></td>
                                                <td><asp:Label Font-Size="12px" class="font-content-dashboard" Font-Names="Helvetica, Arial, sans-serif" ID="Label14" runat="server" Text=""> <%# Eval("Subjective").ToString().Replace("\\n","<br />") %> </asp:Label></td>
                                                <td><asp:Label Font-Size="12px" class="font-content-dashboard" Font-Names="Helvetica, Arial, sans-serif" ID="Label15" runat="server" Text=""> <%# Eval("Objective").ToString().Replace("\\n","<br />") %> </asp:Label></td>
                                                <td><asp:Label Font-Size="12px" class="font-content-dashboard" Font-Names="Helvetica, Arial, sans-serif" ID="Label16" runat="server" Text=""> <%# Eval("Diagnosis").ToString().Replace("\\n","<br />") %> </asp:Label></td>
                                                <td>
                                                    <asp:Label Font-Size="12px" class="font-content-dashboard" Font-Names="Helvetica, Arial, sans-serif" ID="Label18" runat="server" Text=""> <%# Eval("PlanningProcedure").ToString().Replace("\\n","<br />") %> </asp:Label>
                                                    <br/>
                                                    <br/>
                                                    <asp:Label Font-Size="12px" class="font-content-dashboard" Font-Names="Helvetica, Arial, sans-serif" Font-Bold="true" ID="Label21" runat="server" Visible='<%# Eval("OrderLab").ToString() == "" ? false : true %>' Text="Order Lab"></asp:Label>
                                                    <br/>
                                                    <asp:Label Font-Size="12px" class="font-content-dashboard" Font-Names="Helvetica, Arial, sans-serif" ID="Label22" runat="server" Text=""> <%# Eval("OrderLab").ToString().Replace("\\n","<br />") %> </asp:Label>
                                                    <br/>
                                                    <asp:Label Font-Size="12px" class="font-content-dashboard" Font-Names="Helvetica, Arial, sans-serif" Font-Bold="true" ID="Label23" runat="server" Visible='<%# Eval("OrderRad").ToString() == "" ? false : true %>' Text="Order Rad"></asp:Label>
                                                    <br/>
                                                    <asp:Label Font-Size="12px" class="font-content-dashboard" Font-Names="Helvetica, Arial, sans-serif" ID="Label24" runat="server" Text=""> <%# Eval("OrderRad").ToString().Replace("\\n","<br />") %> </asp:Label>
                                                </td>
                                                <td><asp:Label Font-Size="12px" class="font-content-dashboard" Font-Names="Helvetica, Arial, sans-serif" ID="Label17" runat="server" Text=""> <%# Eval("Prescription").ToString().Replace("\\n","<br />") %> </asp:Label></td>
                                            </tr>
                                        </table>
                                    </div>
                                    </br>
                                </row>
                                <row>
                                    <asp:Label ID="Label19" Style="font-family: Helvetica;font-size:11px; float:right;" runat="server" Text="Terima kasih atas bantuan dan kerjasamanya,"></asp:Label>
                                    <br/>
                                    <br/>
                                 </row>
                                <row>
                                    <asp:Label ID="Label20" Style="font-family: Helvetica;font-size:11px; float:right;" runat="server" Text='<%# Bind("DoctorName") %>'></asp:Label>
                                    <br/>
                                    <br/>
                                </row> 
                            </div>
                        </ItemTemplate>
                    </asp:Repeater>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>

    </form>

    <script>
        function openTabSoap(ids) {
            var idparam = ids.replace("rptReferralList_Label12_", "rptReferralList_soapTable_");

            var opd_data = document.getElementById(idparam);

            if (opd_data.style.display == "block") {
                opd_data.style.display = "none";
            }
            else {
                opd_data.style.display = "block";
            }
        }
    </script>
</body>
</html>
