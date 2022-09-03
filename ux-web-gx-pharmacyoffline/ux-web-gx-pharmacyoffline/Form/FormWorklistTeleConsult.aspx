<%@ Page Language="C#" AutoEventWireup="true" ValidateRequest="false" CodeFile="FormWorklistTeleConsult.aspx.cs" Inherits="Form_FormWorklistTeleConsult" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <!-- Tell the browser to be responsive to screen width -->
    <meta content="width=device-width, initial-scale=1, maximum-scale=1, user-scalable=no" name="viewport" />
    <!-- Bootstrap 3.3.6 -->
    <link rel="stylesheet" href="~/Content/bootstrap/css/bootstrap.min.css" />
    <!-- Font Awesome -->
    <link rel="stylesheet" href="~/Content/font-awesome/css/font-awesome.min.css" />
    <!-- Datepicker -->
    <link rel="stylesheet" href="~/Content/plugins/datepicker/datepicker130.css" />
    <!-- DropDown -->
    <link href="~/Content/bootstrap-select/css/bootstrap-select.css" rel="stylesheet"  type="text/css" />

    <link href="../favicon.ico" rel="shortcut icon" type="image/x-icon" />

    <script src="../Content/toast/sweetalert2.min.js"></script>
    <link rel="stylesheet" href="../Content/toast/sweetalert2.min.css" />

</head>
<body style="padding-top: 15px;">
    <form id="form1" runat="server">

        <asp:ScriptManager ID="ScriptManager1" runat="server">
            <Scripts>
                <asp:ScriptReference Path="~/Content/plugins/jQuery/jQuery-2.2.0.min.js" />
                <asp:ScriptReference Path="~/Content/bootstrap/js/bootstrap.min.js" />
                <asp:ScriptReference Path="~/Content/plugins/datepicker/bootstrap-datepicker.js" />
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
 
        </style>

        <div>
            <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdateMainpanel">
                <ProgressTemplate>
                    <%--<div style="background-color: #cdd2dd; text-align:center; z-index:5; position:fixed; width:100%; left:0px; height:calc(100vh - 50px); border-radius:6px;">
                        <div class="modal-backdrop" style="background-color:red; opacity:0; text-align:center">
                        </div>
                        <div style="margin-top: 12%;">
                            <img alt="" height="225px" width="225px" style="background-color:transparent; vertical-align: middle;" class="login-box-body" src="<%= Page.ResolveClientUrl("~/Images/Background/loading-beat.gif") %>" />
                        </div>
                    </div>--%>
                    <div class="modal-backdrop" style="background-color: black; opacity: 0.6; vertical-align: central; text-align: center; z-index:10000;">
                    </div>
                    <div style="margin-top: 180px; margin-left: -100px; text-align: center; position: fixed; z-index: 11000; left: 50%;">
                        <img alt="" height="200px" width="200px" style="background-color: transparent; vertical-align: middle" class="login-box-body" src="<%= Page.ResolveClientUrl("~/Images/Background/loading-beat.gif") %>" />
                    </div>
                </ProgressTemplate>
            </asp:UpdateProgress>

            <asp:UpdatePanel runat="server" ID="UpdateMainpanel">
                <ContentTemplate>
                    <asp:HiddenField runat="server" ID="hfOrgId" />
                    <asp:HiddenField runat="server" ID="hfUserId" />
                    <%--=======================================================CONTENT START===================================================--%>

                    <div>

                        <div class="container-fluid">
                            <asp:UpdatePanel runat="server">
                                <ContentTemplate>
                                    <div class="row" style="background-color: white">

                                        <div class="col-sm-6" style="text-align:left; font-size:13px;">
                                            <table>
                                                <tr>
                                                    <td style="width: 40px;">Date :</td>
                                                    <td><asp:TextBox ID="TextSearchDate" runat="server" CssClass="form-control" AutoCompleteType="Disabled" onmousedown="dateSearch();"></asp:TextBox></td>
                                                    <td style="width:20px;"> &nbsp; </td>
                                                    <td style="width: 80px;">Search MR/Patient :</td>
                                                    <td><asp:TextBox ID="TextSearch" runat="server" CssClass="form-control"></asp:TextBox></td>
                                                    <td style="width:20px;"> &nbsp; </td>
                                                    <td> <asp:Button ID="ButtonSearch" runat="server" CssClass="btn btn-sm btn-success" Text="Search" OnClick="ButtonSearch_Click" /> </td>   
                                                </tr>
                                            </table>
                                        </div>
                                        <div class="col-sm-6" style="text-align:right; font-size:13px;">
                                            <table style="display:inline-block;">
                                                <tr>
                                                    <td style="width: 115px; text-align:left;">
                                                        Appointment Type :
                                                    </td>
                                                    <td>
                                                        <asp:DropDownList ID="DDL_AppointType" runat="server" CssClass="form-control" style="width:100%; display:inline-block;" OnSelectedIndexChanged="DDL_AppointType_SelectedIndexChanged" AutoPostBack="true">
                                                            <asp:ListItem Value="-1"> - select - </asp:ListItem>
                                                            <asp:ListItem Value="1" Selected="True"> Appointment </asp:ListItem>
                                                            <asp:ListItem Value="0"> Non Appointment </asp:ListItem>
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td style="width:20px;"> &nbsp; </td>
                                                    <td style="width: 50px; text-align:left;">
                                                        Status :
                                                    </td>
                                                    <td>
                                                        <asp:DropDownList ID="ddlFinish" runat="server" CssClass="form-control" style="width:100%; display:inline-block;" OnSelectedIndexChanged="DDL_AppointType_SelectedIndexChanged" AutoPostBack="true">
                                                            <asp:ListItem Value="-1"> - select - </asp:ListItem>
                                                            <asp:ListItem Value="True"> Finish </asp:ListItem>
                                                            <asp:ListItem Value="False"> Not Finish </asp:ListItem>
                                                        </asp:DropDownList>
                                                    </td>
                                                    
                                                </tr>
                                            </table>
                                        </div>
                                        <%--<div class="col-sm-3" style="text-align:right; font-size:13px;">
                                            <asp:Label ID="LabelType" runat="server" Text="Appointment Type : "></asp:Label>
                                            <asp:DropDownList ID="DDL_AppointType" runat="server" CssClass="form-control" style="width:180px; display:inline-block;" OnSelectedIndexChanged="DDL_AppointType_SelectedIndexChanged" AutoPostBack="true">
                                                <asp:ListItem Value="-1"> - select - </asp:ListItem>
                                                <asp:ListItem Value="1" Selected="True"> Appointment </asp:ListItem>
                                                <asp:ListItem Value="0"> Non Appointment </asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                        <div class="col-sm-3" style="text-align:right; font-size:13px;">
                                            <asp:Label ID="Label2" runat="server" Text="Status : "></asp:Label>
                                            <asp:DropDownList ID="ddlFinish" runat="server" CssClass="form-control" style="width:180px; display:inline-block;" OnSelectedIndexChanged="DDL_AppointType_SelectedIndexChanged" AutoPostBack="true">
                                                <asp:ListItem Value="-1"> - select - </asp:ListItem>
                                                <asp:ListItem Value="True"> Finish </asp:ListItem>
                                                <asp:ListItem Value="False"> Not Finish </asp:ListItem>
                                            </asp:DropDownList>
                                        </div>--%>

                                        <div class="col-sm-12" style="min-height: 50px; padding-top: 15px; padding-bottom: 5px;">
                                            
                                            <asp:HiddenField ID="HF_orgid" runat="server" />
                                            <asp:HiddenField ID="HF_ptnid" runat="server" />
                                            <asp:HiddenField ID="HF_admid" runat="server" />
                                            <asp:HiddenField ID="HF_encid" runat="server" />
                                            <asp:HiddenField ID="HF_rdiid" runat="server" />
                                            <asp:HiddenField ID="HF_ex" runat="server" />

                                            <asp:HiddenField ID="HF_DokterName" runat="server" />
                                            <asp:HiddenField ID="HF_Mrno" runat="server" />
                                            <asp:HiddenField ID="HF_PatientName" runat="server" />
                                            
                                            <h4>Daftar Pasien</h4>
                                            <asp:GridView ID="gvw_worklist_TC" runat="server" AutoGenerateColumns="False" CssClass="table table-striped table-bordered table-condensed thinscroll"
                                                EmptyDataText="No Data" Font-Names="Helvetica">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Nomor MR" ItemStyle-Width="10%" HeaderStyle-Font-Size="13px" HeaderStyle-Font-Names="Helvetica, Arial, sans-serif" HeaderStyle-Font-Bold="true">
                                                        <ItemTemplate>
                                                            <asp:Label runat="server" ID="lblLocalMRNo" Width="100%" Height="20px" Font-Size="11px" Font-Names="Helvetica, Arial, sans-serif" Text='<%# Bind("LocalMrNo") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Nama Pasien" ItemStyle-Width="15%" HeaderStyle-Font-Size="13px" HeaderStyle-Font-Names="Helvetica, Arial, sans-serif" HeaderStyle-Font-Bold="true">
                                                        <ItemTemplate>
                                                            <asp:Label runat="server" ID="lblPatientName" Width="100%" Height="20px" Font-Size="11px" Font-Names="Helvetica, Arial, sans-serif" Text='<%# Bind("PatientName") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Sex" ItemStyle-Width="3%" HeaderStyle-Font-Size="13px" HeaderStyle-Font-Names="Helvetica, Arial, sans-serif" HeaderStyle-Font-Bold="true">
                                                        <ItemTemplate>
                                                            <asp:Label runat="server" ID="lblGender" Width="100%" Height="20px" Font-Size="11px" Font-Names="Helvetica, Arial, sans-serif" Text='<%# Eval("SexId").ToString() == "1" ? "M" : "F" %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="DOB" ItemStyle-Width="7%" HeaderStyle-Font-Size="13px" HeaderStyle-Font-Names="Helvetica, Arial, sans-serif" HeaderStyle-Font-Bold="true">
                                                        <ItemTemplate>
                                                            <asp:Label runat="server" ID="lblDob" Width="100%" Height="20px" Font-Size="11px" Font-Names="Helvetica, Arial, sans-serif" Text='<%# DateTime.Parse(Eval("BirthDate").ToString()).ToString("dd MMM yyyy") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Admission Date" ItemStyle-Width="10%" HeaderStyle-Font-Size="13px" HeaderStyle-Font-Names="Helvetica, Arial, sans-serif" HeaderStyle-Font-Bold="true">
                                                        <ItemTemplate>
                                                            <asp:Label runat="server" ID="lblAdmissionDate" Width="100%" Height="20px" Font-Size="11px" Font-Names="Helvetica, Arial, sans-serif" Text='<%# DateTime.Parse(Eval("AdmissionDate").ToString()).ToString("dd MMM yyyy HH:mm:ss") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Admission No" ItemStyle-Width="8%" HeaderStyle-Font-Size="13px" HeaderStyle-Font-Names="Helvetica, Arial, sans-serif" HeaderStyle-Font-Bold="true">
                                                        <ItemTemplate>
                                                            <asp:Label runat="server" ID="lblAdmissionNo" Width="100%" Height="20px" Font-Size="11px" Font-Names="Helvetica, Arial, sans-serif" Text='<%# Bind("AdmissionNo") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Phone" ItemStyle-Width="10%" HeaderStyle-Font-Size="13px" HeaderStyle-Font-Names="Helvetica, Arial, sans-serif" HeaderStyle-Font-Bold="true">
                                                        <ItemTemplate>
                                                            <asp:Label runat="server" ID="lblPhone" Width="100%" Height="20px" Font-Size="11px" Font-Names="Helvetica, Arial, sans-serif" Text='<%# Bind("Phone") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                     <asp:TemplateField HeaderText="Doctor Name" ItemStyle-Width="15%" HeaderStyle-Font-Size="13px" HeaderStyle-Font-Names="Helvetica, Arial, sans-serif" HeaderStyle-Font-Bold="true">
                                                        <ItemTemplate>
                                                            <asp:Label runat="server" ID="lblDoctorName" Width="100%" Height="20px" Font-Size="11px" Font-Names="Helvetica, Arial, sans-serif" Text='<%# Bind("DoctorName") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Type" ItemStyle-Width="3%" HeaderStyle-Font-Size="13px" HeaderStyle-Font-Names="Helvetica, Arial, sans-serif" HeaderStyle-Font-Bold="true">
                                                        <ItemTemplate>
                                                            <asp:Label runat="server" ID="lblType" Width="100%" Height="20px" Font-Size="11px" Font-Names="Helvetica, Arial, sans-serif" Text='<%# Eval("isNextAppointment").ToString() == "1" ? "A" : "NA" %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                     <asp:TemplateField HeaderText="Pres" ItemStyle-Width="6%" HeaderStyle-Font-Size="13px" HeaderStyle-Font-Names="Helvetica, Arial, sans-serif" HeaderStyle-Font-Bold="true">
                                                        <ItemTemplate>
                                                            <asp:Label runat="server" ID="lblPres" Width="100%" Height="20px" Font-Size="11px" Font-Names="Helvetica, Arial, sans-serif" Text='<%# Eval("Prescription").ToString() == "0" ? "NO" : "YES" %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Next Appointment" ItemStyle-Width="8%" HeaderStyle-Font-Size="13px" HeaderStyle-Font-Names="Helvetica, Arial, sans-serif" HeaderStyle-Font-Bold="true">
                                                        <ItemTemplate>
                                                            <asp:Label runat="server" ID="lbl1" Width="100%" Height="20px" Font-Size="11px" Font-Names="Helvetica, Arial, sans-serif" Text='<%# Bind("next_appointment") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Jadwal Lab/Rad" ItemStyle-Width="15%" HeaderStyle-Font-Size="13px" HeaderStyle-Font-Names="Helvetica, Arial, sans-serif" HeaderStyle-Font-Bold="true">
                                                        <ItemTemplate>
                                                            <asp:Label runat="server" ID="lbl2" Width="100%" Height="20px" Font-Size="11px" Font-Names="Helvetica, Arial, sans-serif" Text='<%# Bind("remarks") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="FINISH" ItemStyle-Width="10%" HeaderStyle-Font-Size="13px" HeaderStyle-Font-Names="Helvetica, Arial, sans-serif" HeaderStyle-Font-Bold="true">
                                                        <ItemTemplate>
                                                            <asp:Label runat="server" ID="lblemail" Width="100%" Height="20px" Font-Size="11px" Font-Names="Helvetica, Arial, sans-serif" Text='<%# Eval("is_send_email").ToString() == "True" ? "YES": "NO" %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                   <asp:TemplateField HeaderText="Action" ItemStyle-Width="10%" HeaderStyle-Font-Size="13px" HeaderStyle-Font-Names="Helvetica, Arial, sans-serif" HeaderStyle-Font-Bold="true">
                                                        <ItemTemplate>
                                                            <asp:Button ID="ButtonSendEmail" runat="server" Text="Detail" CssClass="btn btn-sm btn-info" 
                                                                OnClientClick=<%# "openSendEmailModal('" + Eval("OrganizationId") + "','" + Eval("PatientId") + "','" + Eval("AdmissionId") + "','" + Eval("EncounterId") + "','" + Eval("PatientName").ToString().Replace(" ","_").Replace("'","_") + "','" + Eval("LocalMRNo").ToString().Replace(" ","_") + "','" + Eval("DoctorName").ToString().Replace(" ","_").Replace("'","_") + 
                                                                   "','" + Eval("next_appointment_date") + "','" + Eval("next_appointment_time") + "','" + Eval("NextDoctor") + "','" + Eval("reference_id") + "','" + Eval("reference_type_id") +
                                                                   "','" + Eval("reference_doctor_id") + "','" + Eval("Email") + "','" + Eval("remarks") +  "','" + Eval("Phone") + "', '1')" %> OnClick="ButtonSendEmail_Click" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    
                                                </Columns>
                                            </asp:GridView>
                                            <h4><b>PASIEN DENGAN EXCLUSION CRITERIA</b></h4>
                                            <asp:GridView ID="gvExclude" runat="server" AutoGenerateColumns="False" CssClass="table table-striped table-bordered table-condensed thinscroll"
                                                EmptyDataText="No Data" Font-Names="Helvetica">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Nomor MR" ItemStyle-Width="10%" HeaderStyle-Font-Size="13px" HeaderStyle-Font-Names="Helvetica, Arial, sans-serif" HeaderStyle-Font-Bold="true">
                                                        <ItemTemplate>
                                                            <asp:Label runat="server" ID="lblLocalMRNo" Width="100%" Height="20px" Font-Size="11px" Font-Names="Helvetica, Arial, sans-serif" Text='<%# Bind("LocalMrNo") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Nama Pasien" ItemStyle-Width="15%" HeaderStyle-Font-Size="13px" HeaderStyle-Font-Names="Helvetica, Arial, sans-serif" HeaderStyle-Font-Bold="true">
                                                        <ItemTemplate>
                                                            <asp:Label runat="server" ID="lblPatientName" Width="100%" Height="20px" Font-Size="11px" Font-Names="Helvetica, Arial, sans-serif" Text='<%# Bind("PatientName") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Sex" ItemStyle-Width="3%" HeaderStyle-Font-Size="13px" HeaderStyle-Font-Names="Helvetica, Arial, sans-serif" HeaderStyle-Font-Bold="true">
                                                        <ItemTemplate>
                                                            <asp:Label runat="server" ID="lblGender" Width="100%" Height="20px" Font-Size="11px" Font-Names="Helvetica, Arial, sans-serif" Text='<%# Eval("SexId").ToString() == "1" ? "M" : "F" %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="DOB" ItemStyle-Width="7%" HeaderStyle-Font-Size="13px" HeaderStyle-Font-Names="Helvetica, Arial, sans-serif" HeaderStyle-Font-Bold="true">
                                                        <ItemTemplate>
                                                            <asp:Label runat="server" ID="lblDob" Width="100%" Height="20px" Font-Size="11px" Font-Names="Helvetica, Arial, sans-serif" Text='<%# DateTime.Parse(Eval("BirthDate").ToString()).ToString("dd MMM yyyy") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Admission Date" ItemStyle-Width="10%" HeaderStyle-Font-Size="13px" HeaderStyle-Font-Names="Helvetica, Arial, sans-serif" HeaderStyle-Font-Bold="true">
                                                        <ItemTemplate>
                                                            <asp:Label runat="server" ID="lblAdmissionDate" Width="100%" Height="20px" Font-Size="11px" Font-Names="Helvetica, Arial, sans-serif" Text='<%# DateTime.Parse(Eval("AdmissionDate").ToString()).ToString("dd MMM yyyy HH:mm:ss") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Admission No" ItemStyle-Width="8%" HeaderStyle-Font-Size="13px" HeaderStyle-Font-Names="Helvetica, Arial, sans-serif" HeaderStyle-Font-Bold="true">
                                                        <ItemTemplate>
                                                            <asp:Label runat="server" ID="lblAdmissionNo" Width="100%" Height="20px" Font-Size="11px" Font-Names="Helvetica, Arial, sans-serif" Text='<%# Bind("AdmissionNo") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Phone" ItemStyle-Width="10%" HeaderStyle-Font-Size="13px" HeaderStyle-Font-Names="Helvetica, Arial, sans-serif" HeaderStyle-Font-Bold="true">
                                                        <ItemTemplate>
                                                            <asp:Label runat="server" ID="lblPhone" Width="100%" Height="20px" Font-Size="11px" Font-Names="Helvetica, Arial, sans-serif" Text='<%# Bind("Phone") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                     <asp:TemplateField HeaderText="Doctor Name" ItemStyle-Width="15%" HeaderStyle-Font-Size="13px" HeaderStyle-Font-Names="Helvetica, Arial, sans-serif" HeaderStyle-Font-Bold="true">
                                                        <ItemTemplate>
                                                            <asp:Label runat="server" ID="lblDoctorName" Width="100%" Height="20px" Font-Size="11px" Font-Names="Helvetica, Arial, sans-serif" Text='<%# Bind("DoctorName") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Type" ItemStyle-Width="3%" HeaderStyle-Font-Size="13px" HeaderStyle-Font-Names="Helvetica, Arial, sans-serif" HeaderStyle-Font-Bold="true">
                                                        <ItemTemplate>
                                                            <asp:Label runat="server" ID="lblType" Width="100%" Height="20px" Font-Size="11px" Font-Names="Helvetica, Arial, sans-serif" Text='<%# Eval("isNextAppointment").ToString() == "1" ? "A" : "NA" %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                     <asp:TemplateField HeaderText="Pres" ItemStyle-Width="6%" HeaderStyle-Font-Size="13px" HeaderStyle-Font-Names="Helvetica, Arial, sans-serif" HeaderStyle-Font-Bold="true">
                                                        <ItemTemplate>
                                                            <asp:Label runat="server" ID="lblPres" Width="100%" Height="20px" Font-Size="11px" Font-Names="Helvetica, Arial, sans-serif" Text='<%# Eval("Prescription").ToString() == "0" ? "NO" : "YES" %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Next Appointment" ItemStyle-Width="8%" HeaderStyle-Font-Size="13px" HeaderStyle-Font-Names="Helvetica, Arial, sans-serif" HeaderStyle-Font-Bold="true">
                                                        <ItemTemplate>
                                                            <asp:Label runat="server" ID="lbl1" Width="100%" Height="20px" Font-Size="11px" Font-Names="Helvetica, Arial, sans-serif" Text='<%# Bind("next_appointment") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Jadwal Lab/Rad" ItemStyle-Width="15%" HeaderStyle-Font-Size="13px" HeaderStyle-Font-Names="Helvetica, Arial, sans-serif" HeaderStyle-Font-Bold="true">
                                                        <ItemTemplate>
                                                            <asp:Label runat="server" ID="lbl2" Width="100%" Height="20px" Font-Size="11px" Font-Names="Helvetica, Arial, sans-serif" Text='<%# Bind("remarks") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="FINISH" ItemStyle-Width="10%" HeaderStyle-Font-Size="13px" HeaderStyle-Font-Names="Helvetica, Arial, sans-serif" HeaderStyle-Font-Bold="true">
                                                        <ItemTemplate>
                                                            <asp:Label runat="server" ID="lblemail" Width="100%" Height="20px" Font-Size="11px" Font-Names="Helvetica, Arial, sans-serif" Text='<%# Eval("is_send_email").ToString() == "True" ? "YES": "NO" %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                   <asp:TemplateField HeaderText="Action" ItemStyle-Width="10%" HeaderStyle-Font-Size="13px" HeaderStyle-Font-Names="Helvetica, Arial, sans-serif" HeaderStyle-Font-Bold="true">
                                                        <ItemTemplate>
                                                            <asp:Button ID="ButtonSendEmail" runat="server" Text="Detail" CssClass="btn btn-sm btn-info" 
                                                                OnClientClick=<%# "openSendEmailModal('" + Eval("OrganizationId") + "','" + Eval("PatientId") + "','" + Eval("AdmissionId") + "','" + Eval("EncounterId") + "','" + Eval("PatientName").ToString().Replace(" ","_").Replace("'","_") + "','" + Eval("LocalMRNo").ToString().Replace(" ","_") + "','" + Eval("DoctorName").ToString().Replace(" ","_").Replace("'","_") + 
                                                                   "','" + Eval("next_appointment_date") + "','" + Eval("next_appointment_time") + "','" + Eval("NextDoctor") + "','" + Eval("reference_id") + "','" + Eval("reference_type_id") +
                                                                   "','" + Eval("reference_doctor_id") + "','" + Eval("Email") + "','" + Eval("remarks") +  "','" + Eval("Phone") + "', '1')" %> OnClick="ButtonSendEmail_Click" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    
                                                </Columns>
                                            </asp:GridView>
                                            </div>
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>

                        </div>
                    </div>
                    <%--=======================================================CONTENT END===================================================--%>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>


        <div class="modal fade" id="modalSendEmail" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
            <div class="modal-dialog" style="width: 80%; margin-top: 75px;" runat="server">
                <div class="modal-content" style="border-radius: 10px 10px !important;">
                    <div class="modal-header" style="height: 40px; padding-top: 10px; padding-bottom: 5px">
                        <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times; </button>
                        <h4 class="modal-title" style="text-align: left">
                            <asp:Label ID="Label5" Style="font-family: Arial, Helvetica, sans-serif;" runat="server" Text="Send Email"></asp:Label></h4>
                    </div>
                    <div class="modal-body">
                        <asp:UpdatePanel ID="UpdatePanelDetail" runat="server">
                            <ContentTemplate>    
                                <table border="0" style="margin-bottom:10px;">
                                    <tr>
                                        <td>MR No</td>
                                        <td style="width:20px; text-align:center;"> : </td>
                                        <td> <asp:Label ID="LabelMrnoX" runat="server" Text="-" Visible="false"></asp:Label> 
                                            <asp:TextBox ID="LabelMrno" runat="server" Style="border:0px; width:500px;"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>Patient Name</td>
                                        <td style="width:20px; text-align:center;"> : </td>
                                        <td> <asp:Label ID="LabelPtnNameX" runat="server" Text="-" Visible="false"></asp:Label>
                                            <asp:TextBox ID="LabelPtnName" runat="server" Style="border:0px; width:500px;"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>Phone</td>
                                        <td style="width:20px; text-align:center;"> : </td>
                                        <td> <asp:Label ID="Label1" runat="server" Text="-" Visible="false"></asp:Label>
                                            <asp:TextBox ID="lblPhone" runat="server" Style="border:0px; width:500px;"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>Doctor Name</td>
                                        <td style="width:20px; text-align:center;"> : </td>
                                        <td> <asp:Label ID="LabelDocNameX" runat="server" Text="-" Visible="false"></asp:Label>
                                            <asp:TextBox ID="LabelDocName" runat="server" Style="border:0px; width:500px;"></asp:TextBox>
                                        </td>
                                    </tr>
                                </table>

                                <table border="0" style="width:100%">
                                    <tr>
                                        <td style="vertical-align:top; padding-right:5px; width:50%;">
                                            <asp:GridView ID="repeatLab" runat="server" AutoGenerateColumns="False" CssClass="table table-striped table-bordered table-condensed thinscroll"
                                                EmptyDataText="Laboratorium" Font-Names="Helvetica">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Laboratory" ItemStyle-Width="10%" HeaderStyle-Font-Size="13px" HeaderStyle-Font-Names="Helvetica, Arial, sans-serif" HeaderStyle-Font-Bold="true">
                                                        <ItemTemplate>
                                                            <asp:Label runat="server" ID="lblLocalMRNo" Width="100%" Height="20px" Font-Size="11px" Font-Names="Helvetica, Arial, sans-serif" Text='<%# Bind("Name") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>                                        
                                                </Columns>
                                            </asp:GridView>
                                        </td>
                                         <td style="vertical-align:top; padding-left:5px; width:50%;">
                                            <asp:GridView ID="repeatRad" runat="server" AutoGenerateColumns="False" CssClass="table table-striped table-bordered table-condensed thinscroll"
                                                EmptyDataText="Radiologi" Font-Names="Helvetica">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Radiology" ItemStyle-Width="10%" HeaderStyle-Font-Size="13px" HeaderStyle-Font-Names="Helvetica, Arial, sans-serif" HeaderStyle-Font-Bold="true">
                                                        <ItemTemplate>
                                                            <asp:Label runat="server" ID="lblLocalMRNo" Width="100%" Height="20px" Font-Size="11px" Font-Names="Helvetica, Arial, sans-serif" Text='<%# Bind("Name") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>                                        
                                                </Columns>
                                            </asp:GridView>
                                        </td>
                                    </tr>
                                </table>
                                <table border="0" style="margin-bottom:20px;width:100%">
                                    <tr>
                                        <td style="width:15%">
                                            Others
                                        </td>
                                        <td>
                                            <asp:TextBox runat="server" ID="lblOthers" CssClass="form-control" />
                                        </td>
                                    </tr>
                                </table>
                                <table border="0" style="margin-bottom:10px;width:100%">
                                    <tr>
                                        <td>
                                            <asp:GridView ID="gvvDrug" runat="server" AutoGenerateColumns="False" CssClass="table table-striped table-bordered table-condensed thinscroll"
                                                EmptyDataText="Item Obat" Font-Names="Helvetica">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Name" ItemStyle-Width="30%" HeaderStyle-Font-Size="13px" HeaderStyle-Font-Names="Helvetica, Arial, sans-serif" HeaderStyle-Font-Bold="true">
                                                        <ItemTemplate>
                                                            <asp:Label runat="server" ID="lblLocalMRNo" Width="100%" Height="20px" Font-Size="11px" Font-Names="Helvetica, Arial, sans-serif" Text='<%# Bind("Name") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Dose" ItemStyle-Width="5%" HeaderStyle-Font-Size="13px" HeaderStyle-Font-Names="Helvetica, Arial, sans-serif" HeaderStyle-Font-Bold="true">
                                                        <ItemTemplate>
                                                            <div runat="server" Visible='<%# Eval("IsDoseText").ToString() == "False" %>'>
                                                                <asp:Label runat="server" ID="lblPatientName"  Height="20px" Font-Size="11px" Font-Names="Helvetica, Arial, sans-serif" Text='<%# Bind("dosage_id") %>'></asp:Label>
                                                                &nbsp;
                                                                <asp:Label runat="server" ID="lblGender"  Height="20px" Font-Size="11px" Font-Names="Helvetica, Arial, sans-serif" Text='<%# Bind("DoseUom") %>'></asp:Label>
                                                            </div>
                                                            <asp:Label ID="lblDoseText" runat="server" Visible='<%# Eval("IsDoseText") %>' Text='<%# Bind("dose_text") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <%--<asp:TemplateField HeaderText="Dose UOM" ItemStyle-Width="10%" HeaderStyle-Font-Size="13px" HeaderStyle-Font-Names="Helvetica, Arial, sans-serif" HeaderStyle-Font-Bold="true">
                                                        <ItemTemplate>
                                                            <asp:Label runat="server" ID="lblGender" Width="100%" Height="20px" Font-Size="11px" Font-Names="Helvetica, Arial, sans-serif" Text='<%# Bind("DoseUom") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>--%>
                                                    <asp:TemplateField HeaderText="Frequency" ItemStyle-Width="10%" HeaderStyle-Font-Size="13px" HeaderStyle-Font-Names="Helvetica, Arial, sans-serif" HeaderStyle-Font-Bold="true">
                                                        <ItemTemplate>
                                                            <asp:Label runat="server" ID="lblDob" Width="100%" Height="20px" Font-Size="11px" Font-Names="Helvetica, Arial, sans-serif" Text='<%# Bind("Frequency") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Route" ItemStyle-Width="10%" HeaderStyle-Font-Size="13px" HeaderStyle-Font-Names="Helvetica, Arial, sans-serif" HeaderStyle-Font-Bold="true">
                                                        <ItemTemplate>
                                                            <asp:Label runat="server" ID="lblAdmissionDate" Width="100%" Height="20px" Font-Size="11px" Font-Names="Helvetica, Arial, sans-serif" Text='<%# Bind("Route") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Instruction" ItemStyle-Width="10%" HeaderStyle-Font-Size="13px" HeaderStyle-Font-Names="Helvetica, Arial, sans-serif" HeaderStyle-Font-Bold="true">
                                                        <ItemTemplate>
                                                            <asp:Label runat="server" ID="lblAdmissionNo" Width="100%" Height="20px" Font-Size="11px" Font-Names="Helvetica, Arial, sans-serif" Text='<%# Bind("remarks") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                     <asp:TemplateField HeaderText="Quantity" ItemStyle-Width="10%" HeaderStyle-Font-Size="13px" HeaderStyle-Font-Names="Helvetica, Arial, sans-serif" HeaderStyle-Font-Bold="true">
                                                        <ItemTemplate>
                                                            <asp:Label runat="server" ID="lblDoctorName" Width="100%" Height="20px" Font-Size="11px" Font-Names="Helvetica, Arial, sans-serif" Text='<%# Bind("quantity") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="UOM" ItemStyle-Width="5%" HeaderStyle-Font-Size="13px" HeaderStyle-Font-Names="Helvetica, Arial, sans-serif" HeaderStyle-Font-Bold="true">
                                                        <ItemTemplate>
                                                            <asp:Label runat="server" ID="lblType" Width="100%" Height="20px" Font-Size="11px" Font-Names="Helvetica, Arial, sans-serif" Text='<%# Bind("Uom") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Iter" ItemStyle-Width="5%" HeaderStyle-Font-Size="13px" HeaderStyle-Font-Names="Helvetica, Arial, sans-serif" HeaderStyle-Font-Bold="true">
                                                        <ItemTemplate>
                                                            <asp:Label runat="server" ID="lblType" Width="100%" Height="20px" Font-Size="11px" Font-Names="Helvetica, Arial, sans-serif" Text='<%# Bind("iteration") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Routine" ItemStyle-Width="5%" HeaderStyle-Font-Size="13px" HeaderStyle-Font-Names="Helvetica, Arial, sans-serif" HeaderStyle-Font-Bold="true">
                                                        <ItemTemplate>
                                                            <asp:Label runat="server" ID="lblType" Width="100%" Height="20px" Font-Size="11px" Font-Names="Helvetica, Arial, sans-serif" Text='<%# Bind("is_routine") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:GridView ID="gvAlkes" runat="server" AutoGenerateColumns="False" CssClass="table table-striped table-bordered table-condensed thinscroll"
                                                EmptyDataText="Item Alkes" Font-Names="Helvetica">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Name" ItemStyle-Width="45%" HeaderStyle-Font-Size="13px" HeaderStyle-Font-Names="Helvetica, Arial, sans-serif" HeaderStyle-Font-Bold="true">
                                                        <ItemTemplate>
                                                            <asp:Label runat="server" ID="lblLocalMRNo" Width="100%" Height="20px" Font-Size="11px" Font-Names="Helvetica, Arial, sans-serif" Text='<%# Bind("Name") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Quantity" ItemStyle-Width="10%" HeaderStyle-Font-Size="13px" HeaderStyle-Font-Names="Helvetica, Arial, sans-serif" HeaderStyle-Font-Bold="true">
                                                        <ItemTemplate>
                                                            <asp:Label runat="server" ID="lblDoctorName" Width="100%" Height="20px" Font-Size="11px" Font-Names="Helvetica, Arial, sans-serif" Text='<%# Bind("quantity") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="UOM" ItemStyle-Width="15%" HeaderStyle-Font-Size="13px" HeaderStyle-Font-Names="Helvetica, Arial, sans-serif" HeaderStyle-Font-Bold="true">
                                                        <ItemTemplate>
                                                            <asp:Label runat="server" ID="lblType" Width="100%" Height="20px" Font-Size="11px" Font-Names="Helvetica, Arial, sans-serif" Text='<%# Bind("Uom") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Instruction" ItemStyle-Width="30%" HeaderStyle-Font-Size="13px" HeaderStyle-Font-Names="Helvetica, Arial, sans-serif" HeaderStyle-Font-Bold="true">
                                                        <ItemTemplate>
                                                            <asp:Label runat="server" ID="lblAdmissionNo" Width="100%" Height="20px" Font-Size="11px" Font-Names="Helvetica, Arial, sans-serif" Text='<%# Bind("remarks") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>                                          
                                                </Columns>
                                            </asp:GridView>
                                        </td>
                                    </tr>
                                </table>
                                <hr />
                                <div style="text-align:center;">
                                    <table border="0" style="margin-bottom:10px;width:80%; display:inline-block;">
                                        <tr>
                                            <td style="width:20%; text-align:left;">
                                                Tanggal & Jam Appointment
                                            </td>
                                            <td style="width:80%" class="input-group">
                                                <asp:TextBox runat="server" Width="120px" CssClass="form-control" ID="txtDate" AutoCompleteType="Disabled" onmousedown="dateSearch2();"/>
                                                <asp:TextBox runat="server" CssClass="form-control" MaxLength="5" ID="txtTime" Width="70px" placeholder="HH:mm" onkeypress="formatTime(this)"/> 
                                             </td>
                                        </tr>
                                        <tr>
                                            <td style="width:20%; text-align:left;">
                                                Dokter/specialis yang dirujuk
                                            </td>
                                            <td>
                                                <asp:TextBox ID="lblNextDoctor" CssClass="form-control" runat="server"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width:20%; text-align:left;">
                                                Rujukan
                                            </td>
                                            <td>
                                                <asp:DropDownList runat="server" CssClass="form-control selectpicker" data-live-search="true" ID="ddlDoctor">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width:20%; text-align:left;">
                                                Email
                                            </td>
                                            <td>
                                                <asp:TextBox CssClass="form-control" runat="server" ID="txtEmail" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="vertical-align:top; width:20%; text-align:left;">
                                                Tanggal & Jam Lab, Rad & lain-lain
                                            </td>
                                            <td>
                                                <asp:TextBox runat="server" TextMode="MultiLine" MaxLength="5000" ID="txtRemarks" CssClass="form-control" Wrap="true" Height="150px"/>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width:20%; text-align:left;">
                                                
                                            </td>
                                            <td  style="text-align:left; padding-top:10px;">
                                                Pasien yang memiliki salah satu kondisi di bawah tidak dianjurkan untuk pemeriksaan fisik lanjutan
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="vertical-align:top; width:20%; text-align:left;">
                                                
                                            </td>
                                            <td style="vertical-align:top; width:100%; text-align:left;" class="input-group">
                                                <asp:RadioButtonList CssClass="form-group" RepeatLayout="Flow" runat="server" Id="rdInfectious" RepeatDirection="Horizontal">
                                                    <asp:ListItem class="radio-inline" Text=" Yes " Value="1"/>
                                                    <asp:ListItem class="radio-inline" Text=" No " Value="0" />
                                                </asp:RadioButtonList>
                                                &nbsp;&nbsp;&nbsp;
                                                <label class="form-group">Pasien dengan penyakit infeksius (mis: TBC, Covid19)</label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td></td>
                                            <td style="vertical-align:top; width:100%; text-align:left;" class="input-group">
                                                <asp:RadioButtonList CssClass="form-group" RepeatLayout="Flow" runat="server" Id="rdImmunoComp" RepeatDirection="Horizontal">
                                                    <asp:ListItem class="radio-inline" Text=" Yes " Value="1" />
                                                    <asp:ListItem class="radio-inline" Text=" No " Value="0" />
                                                </asp:RadioButtonList>
                                                &nbsp;&nbsp;&nbsp;
                                                <label class="form-group">Pasien dengan immuno-compromised</label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td></td>
                                            <td style="text-align:right; padding-top:15px;">
                                                <div style="display:inline-block;">
                                                    <asp:UpdateProgress ID="UpdateProgress2" runat="server" AssociatedUpdatePanelID="UpdatePanelDetail">
                                                        <ProgressTemplate>
                                                            <%--<div class="modal-backdrop" style="background-color: white; opacity: 0; text-align: center">
                                                            </div>--%>
                                                            
                                                            <img alt="" style="background-color: transparent; height: 20px;" src="<%= Page.ResolveClientUrl("~/Images/Background/small-loader.gif") %>" />
                                                            &nbsp;
                                                        </ProgressTemplate>
                                                    </asp:UpdateProgress>
                                                </div>
                                                <asp:Button ID="btnSave" runat="server" Text="Simpan" CssClass="btn btn-success" OnClientClick="return checkField();" OnClick="btnSave_Click" />
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        
                    </div>

                </div>
            </div>
        </div>

    </form>

    <script type="text/javascript">

        function showSuccess() {
            Swal.fire(
                'Submit Success!',
                'Thank You!',
                'success'
            )
            location.reload();
        }

        $(document).ready(function () {
            $('.selectpicker').selectpicker();
            var prm = Sys.WebForms.PageRequestManager.getInstance();
            if (prm != null) {
                prm.add_endRequest(function (sender, e) {
                    if (sender._postBackSettings.panelsToUpdate != null) {
                        $('.selectpicker').selectpicker();
                    }
                });
            };
        });

        function checkField() {
            var email = document.getElementById("<%=txtEmail.ClientID%>");
            var rdi = document.getElementById("<%=HF_rdiid.ClientID%>");
            var filter = /^([a-zA-Z0-9_.-])+@(([a-zA-Z0-9-])+.)+([a-zA-Z0-9]{2,4})+$/;
            if (!filter.test(email.value)) {
                alert('Please provide a valid email address');
                email.focus;
                return false;
            }
            if (rdi.value == "") {
                alert('Pasien Tidak membutuhkan Rujukan');
                return false;
            }
            return true;
        }

        function formatTime(timeInput) {
            intValidNum = timeInput.value;
            if (intValidNum > 4) {
                if (intValidNum < 24) {
                    if (intValidNum.length == 2) {
                        timeInput.value = timeInput.value + ":";
                        return false;
                    }
                }
                if (intValidNum == 24) {
                    if (intValidNum.length == 2) {
                        timeInput.value = timeInput.value.length - 2 + "0:";
                        return false;
                    }
                }
                if (intValidNum > 24) {
                    if (intValidNum.length == 2) {
                        timeInput.value = "";
                        return false;
                    }
                }

                //Here is where I had trouble targeting the
                //mm and ss in order to add conditions (see hh above).
                //I used slice to assist me.
                //Please let me know if any of you have suggestions/enhancements/corrections.

                if (intValidNum.length == 5 && intValidNum.slice(-2) < 60) {
                    timeInput.value = timeInput.value + ":";
                    return false;
                }
                if (intValidNum.length == 5 && intValidNum.slice(-2) > 60) {
                    timeInput.value = timeInput.value.slice(0, 2) + ":";
                    return false;
                }
                if (intValidNum.length == 5 && intValidNum.slice(-2) == 60) {
                    timeInput.value = timeInput.value.slice(0, 2) + ":00:";
                    return false;
                }
                if (intValidNum.length == 8 && intValidNum.slice(-2) > 60) {
                    timeInput.value = timeInput.value.slice(0, 5) + ":";
                    return false;
                }
                if (intValidNum.length == 8 && intValidNum.slice(-2) == 60) {
                    timeInput.value = timeInput.value.slice(0, 5) + ":00";
                    return false;
                }
            }
        }

        function dateSearch() {
            var dp = $('#<%=TextSearchDate.ClientID%>');
            var maxDate = new Date();
            dp.datepicker({
                changeMonth: true,
                changeYear: true,
                format: "dd M yyyy",
                language: "tr",
                endDate: maxDate
            }).on('changeDate', function (ev) {
                $(this).blur();
                $(this).datepicker('hide');
            });
        }
        function dateSearch2() {
            var dp = $('#<%=txtDate.ClientID%>');
            var maxDate = new Date();
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

        function openSendEmailModal(orgid, ptnid, admid, encid, Ptnname, MRno, DocName, tanggal, jam, nextdoctor, reference_id,
            reference_type_id, reference_doctor_id, email, remarks, phone, ex) {
            $('#modalSendEmail').modal('show');
            document.getElementById("<%=HF_orgid.ClientID %>").value = orgid;
            document.getElementById("<%=HF_ptnid.ClientID %>").value = ptnid;
            document.getElementById("<%=HF_admid.ClientID %>").value = admid;
            document.getElementById("<%=HF_encid.ClientID %>").value = encid;
            document.getElementById("<%=HF_rdiid.ClientID %>").value = reference_doctor_id;
            document.getElementById("<%=HF_ex.ClientID %>").value = ex;

            if (nextdoctor == "") {
                document.getElementById("<%=lblNextDoctor.ClientID %>").value = "-";
            }
            else {
                document.getElementById("<%=lblNextDoctor.ClientID %>").value = nextdoctor;
            }

            if (email == "") {
                document.getElementById("<%=txtEmail.ClientID %>").value = "";
            }
            else {
                document.getElementById("<%=txtEmail.ClientID %>").value = email;
            }

            if (remarks == "") {
                document.getElementById("<%=txtRemarks.ClientID %>").value = "";
            }
            else {
                document.getElementById("<%=txtRemarks.ClientID %>").value = remarks;
            }

            if (tanggal == "") {
                document.getElementById("<%=txtDate.ClientID %>").value = "";
                document.getElementById("<%=txtTime.ClientID %>").value = "";
            }
            else {
                document.getElementById("<%=txtDate.ClientID %>").value = tanggal;
                document.getElementById("<%=txtTime.ClientID %>").value = jam;
            }

            //if (reference_type_id == "2") {
            //    $("[id$='ddlDoctor']").val(reference_id);
            //}
            //else {
            //    $("[id$='ddlDoctor']").val("");
            //}

            var paaa = document.getElementById("<%=HF_PatientName.ClientID %>");
            var nooo = document.getElementById("<%=HF_Mrno.ClientID %>");
            var dooo = document.getElementById("<%=HF_DokterName.ClientID %>");
            paaa.value = Ptnname;
            nooo.value = MRno;
            dooo.value = DocName;

            paaa.value = paaa.value.toString().replace(/_/g, " ");
            nooo.value = nooo.value.toString().replace(/_/g, " ");
            dooo.value = dooo.value.toString().replace(/_/g, " ");

            document.getElementById("<%=lblPhone.ClientID %>").value = phone;
            document.getElementById("<%=LabelPtnName.ClientID %>").value = paaa.value;
            document.getElementById("<%=LabelMrno.ClientID %>").value = nooo.value;
            document.getElementById("<%=LabelDocName.ClientID %>").value = dooo.value;
        }
    </script>
</body>
</html>

