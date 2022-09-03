<%@ Page Language="C#" AutoEventWireup="true" CodeFile="LabOrder.aspx.cs" Inherits="Form_LabOrder" %>


<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <!-- Tell the browser to be responsive to screen width -->
    <meta content="width=device-width, initial-scale=1, maximum-scale=1, user-scalable=no" name="viewport" />
    <!-- Bootstrap 3.3.6 -->
    <link rel="stylesheet" href="~/Content/bootstrap/css/bootstrap.css" />
    <link rel="stylesheet" href="~/Content/dist/css/AdminLTE.css" />
    <!-- Font Awesome -->
    <link rel="stylesheet" href="~/Content/font-awesome/css/font-awesome.css" />
    <!-- Datepicker -->
    <link rel="stylesheet" href="~/Content/plugins/datepicker/datepicker3.css" />
    <!-- Theme style -->
    <link rel="stylesheet" href="~/Content/Site.css" />

    <link href="~/favicon.ico" rel="shortcut icon" type="image/x-icon" />

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

            .buttonBlue {
                width: 117px;
                height: 32px;
                border-radius: 4px;
                background-color: #1172f7;
                font-family: Helvetica;
                font-size: 14px;
                font-weight: bold;
                text-align: center;
                color: #ffffff;
                border: none;
            }

                .buttonBlue:hover {
                    width: 117px;
                    height: 32px;
                    border-radius: 4px;
                    background-color: #0d59c1;
                    font-family: Helvetica;
                    font-size: 14px;
                    font-weight: bold;
                    text-align: center;
                    color: #ffffff;
                    border: none;
                }

            .redBox {
                border-radius: 4px;
                border: solid 1px #c43d32;
                background-color: #f5d8d6;
                margin-left: 25px;
                padding-top: 7px;
                padding-left: 7px;
                margin-right: 6%;
                padding-bottom: 7px;
                margin-bottom: 1%;
            }


            .buttonGreen {
                width: 100px;
                height: 26px;
                border-radius: 4px;
                background-color: #4D9B35;
                font-family: Helvetica;
                font-size: 14px;
                font-weight: bold;
                text-align: center;
                color: #ffffff;
                border: none;
            }

                .buttonGreen:hover {
                    width: 100px;
                    height: 26px;
                    border-radius: 4px;
                    background-color: #42852E;
                    font-family: Helvetica;
                    font-size: 14px;
                    font-weight: bold;
                    text-align: center;
                    color: #ffffff;
                    border: none;
                }

            .alertNotif {
                background-color: #f5d8d6;
                color: #c43d32;
                border-radius: 5px 5px;
                border: 2px solid #c43d32;
                padding: 6px;
                font-weight: bold;
                margin-top: 15px;
                text-align: left;
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
        </style>

        <asp:HiddenField runat="server" ID="hfPrintBY" />
        <asp:HiddenField runat="server" ID="hfOrgID" />
        <asp:HiddenField runat="server" ID="hf_admiss_id" />
        <asp:HiddenField runat="server" ID="hf_patient_id" />
        <asp:HiddenField runat="server" ID="hf_ticket_patient" />
        <asp:HiddenField ID="HiddenLabMark" runat="server" />

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
                    <div class="modal-backdrop" style="background-color: black; opacity: 0.6; vertical-align: central; text-align: center; z-index: 10000;">
                    </div>
                    <div style="margin-top: 180px; margin-left: -100px; text-align: center; position: fixed; z-index: 11000; left: 50%;">
                        <img alt="" height="200px" width="200px" style="background-color: transparent; vertical-align: middle" class="login-box-body" src="<%= Page.ResolveClientUrl("~/Images/Background/loading-beat.gif") %>" />
                    </div>
                </ProgressTemplate>
            </asp:UpdateProgress>

            <asp:UpdatePanel runat="server" ID="UpdateMainpanel">
                <ContentTemplate>

                    <%--=======================================================CONTENT START===================================================--%>

                    <div>
                        <asp:HiddenField runat="server" ID="hfpreviewpres" />

                        <asp:HiddenField ID="HF_orgid" runat="server" />
                        <asp:HiddenField ID="HF_ptnid" runat="server" />
                        <asp:HiddenField ID="HF_admid" runat="server" />
                        <asp:HiddenField ID="HF_encid" runat="server" />

                        <asp:HiddenField ID="HF_tgladmn" runat="server" />
                        <asp:HiddenField ID="HF_noadm" runat="server" />
                        <asp:HiddenField ID="HF_dokter" runat="server" />

                        <div class="container-fluid">
                            <asp:UpdatePanel runat="server">
                                <ContentTemplate>
                                    <div class="row" style="background-color: white">
                                        <div class="col-sm-12" style="min-height: 50px; padding-top: 5px; padding-bottom: 5px;">
                                            <asp:GridView ID="gvw_order" runat="server" AutoGenerateColumns="False" CssClass="table table-striped table-bordered table-condensed thinscroll"
                                                EmptyDataText="No Data" Font-Names="Helvetica">
                                                <PagerStyle CssClass="pagination-ys" />
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Print" ItemStyle-Width="2%" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Font-Size="13px" HeaderStyle-Font-Names="Helvetica, Arial, sans-serif" HeaderStyle-Font-Bold="true">
                                                        <ItemTemplate>
                                                            <div style="text-align: left">
                                                                <asp:ImageButton ID="btnPrintSOAP" Width="20px" Height="20px" ToolTip="Print Short SOAP" ImageUrl="~/Images/Dashboard/ic_shortPrint.png" runat="server" Style="margin-right: 2%" OnClick="btnPrintSOAP_Click" />
                                                                <asp:ImageButton ID="btnPrintSOAPLong" Width="20px" Height="20px" ToolTip="Print Long SOAP" ImageUrl="~/Images/Dashboard/ic_longPrint.png" runat="server" Style="margin-right: 2%" OnClick="btnPrintSOAPLong_Click" />
                                                                <asp:ImageButton ID="btnPrintLab" Width="20px" Height="20px" ToolTip="Print Order Laboratory" ImageUrl="~/Images/Dashboard/ic_PrintLab.svg" runat="server" Visible='<%# Eval("isLab").ToString().ToLower() == "true" ? true : false %>' OnClick="btnPrintLab_Click" Style="margin-right: 2%" />
                                                                <asp:ImageButton ID="btnPrintRad" Width="20px" Height="20px" ToolTip="Print Order Radiology" ImageUrl="~/Images/Dashboard/ic_PrintRad.svg" runat="server" Visible='<%# Eval("isRad").ToString().ToLower() == "true" ? true : false %>' OnClick="btnPrintRad_Click" Style="margin-right: 2%" />
                                                                <asp:ImageButton ID="btnPrintReferral" Width="20px" Height="20px" ToolTip="Print Referral Letter" ImageUrl="~/Images/Icon/ic-rujukan.svg" runat="server" Visible='<%# Eval("CountReferral").ToString() != "0" %>' OnClick="btnPrintReferral_Click" Style="margin-right: 2%"  />
                                                                <asp:ImageButton ID="btnPrintRawatInap" Width="20px" Height="20px" ToolTip="Print Rawat Inap" ImageUrl="~/Images/Icon/ic-rawatinap.svg" runat="server" Visible='<%# Eval("CountRawatInap").ToString() != "0" %>' OnClick="btnPrintRawatInap_Click" Style="margin-right: 2%"  />

                                                                <asp:HiddenField ID="hdnEncId" runat="server" Value='<%# Bind("encounterId") %>' />
                                                                <asp:HiddenField ID="hdnAdmId" runat="server" Value='<%# Bind("admissionId") %>' />
                                                                <asp:HiddenField ID="hdnPatientId" runat="server" Value='<%# Bind("patientId") %>' />
                                                                <asp:HiddenField ID="hdnOrganizationId" runat="server" Value='<%# Bind("organizationId") %>' />
                                                                <asp:HiddenField ID="hdnIsLab" runat="server" Value='<%# Bind("isLab") %>' />
                                                                <asp:HiddenField ID="hdnIsRab" runat="server" Value='<%# Bind("isRad") %>' />
                                                                <asp:HiddenField ID="hdnDoctorName" runat="server" Value='<%# Bind("DoctorName") %>' />
                                                                <asp:HiddenField ID="hdnPageSoap" runat="server" Value='<%# Bind("pageSOAP") %>' />

                                                            </div>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Preview" ItemStyle-Width="1%" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Font-Size="13px" HeaderStyle-Font-Names="Helvetica, Arial, sans-serif" HeaderStyle-Font-Bold="true">
                                                        <ItemTemplate>
                                                            <%--<asp:ImageButton ID="btnPopDiagProc" Width="20px" Height="20px" ToolTip="View Procedure Diagnosis" ImageUrl="~/Images/Dashboard/ic_Diag.svg" runat="server" Visible='<%# Eval("isProcedure").ToString().ToLower() == "true" ? true : false %>' OnClientClick=<%# "openDiagprocModal('" + Eval("OrganizationId") + "','" + Eval("PatientId") + "','" + Eval("AdmissionId") + "','" + Eval("EncounterId") + "','" + Eval("AdmissionDate").ToString().Replace(" ","_") + "','" + Eval("AdmissionNo") + "','" + Eval("DoctorName").ToString().Replace(" ","_") + "')" %> OnClick="btnPopDiagProc_Click" Style="margin-right: 2%" />--%>
                                                            <asp:ImageButton ID="btnPopDiagProc" Width="20px" Height="20px" ToolTip="View Procedure Diagnosis" ImageUrl="~/Images/Dashboard/ic_Diag.svg" runat="server" Visible='<%# Eval("isProcedure").ToString().ToLower() == "true" ? true : false %>' OnClick="btnPopDiagProc_Click" Style="margin-right: 2%" />
                                                            <asp:ImageButton ID="btnPopPrescription" Width="20px" Height="20px" ToolTip="View Prescription" ImageUrl="~/Images/Dashboard/ic_Meds.svg" runat="server" Visible='<%# Eval("isPrescription").ToString().ToLower() == "true" ? true : false %>' OnClientClick=<%# "openPrescriptionModal('" + Eval("OrganizationId") + "','" + Eval("PatientId") + "','" + Eval("AdmissionId") + "','" + Eval("EncounterId") + "','" + Eval("AdmissionDate").ToString().Replace(" ","_") + "','" + Eval("AdmissionNo") + "','" + Eval("DoctorName").ToString().Replace(" ","_") + "')" %> OnClick="btnPopPrescription_Click" Style="margin-right: 2%" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Tanggal Adm." ItemStyle-Width="1%" HeaderStyle-Font-Size="13px" HeaderStyle-Font-Names="Helvetica, Arial, sans-serif" HeaderStyle-Font-Bold="true">
                                                        <ItemTemplate>
                                                            <asp:Label runat="server" ID="lblAdmDate" Width="100%" Height="20px" Font-Size="11px" Font-Names="Helvetica, Arial, sans-serif" Text='<%# Bind("admissionDate") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="No. Admisi" ItemStyle-Width="2%" HeaderStyle-Font-Size="13px" HeaderStyle-Font-Names="Helvetica, Arial, sans-serif" HeaderStyle-Font-Bold="true">
                                                        <ItemTemplate>
                                                            <asp:Label runat="server" ID="lblAdmNo" Width="100%" Height="20px" Font-Size="11px" Font-Names="Helvetica, Arial, sans-serif" Text='<%# Bind("admissionNo") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Dokter" ItemStyle-Width="5%" HeaderStyle-Font-Size="13px" HeaderStyle-Font-Names="Helvetica, Arial, sans-serif" HeaderStyle-Font-Bold="true">
                                                        <ItemTemplate>
                                                            <asp:Label runat="server" ID="lblDokter" Width="100%" Height="20px" Font-Size="11px" Font-Names="Helvetica, Arial, sans-serif" Text='<%# Bind("doctorName") %>'></asp:Label>
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

        <div class="modal fade" id="laboratoryResult" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
            <div class="modal-dialog" style="width: 70%;" runat="server">
                <div class="modal-content" style="border-radius: 7px; height: 100%;">
                    <div class="modal-header" style="height: 40px; padding-top: 10px; padding-bottom: 5px">
                        <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                        <h4 class="modal-title" style="text-align: left">
                            <asp:Label ID="lblModalTitle" Style="font-family: Arial, Helvetica, sans-serif;" runat="server" Text="Laboratory Result"></asp:Label></h4>
                    </div>
                    <div class="modal-body" style="background-color: white;">
                        <div style="width: 100%" class="btn-group" role="group">
                            <asp:UpdatePanel runat="server">
                                <ContentTemplate>
                                    <div class="row">
                                        <div class="col-sm-2" style="padding-top: 10px; padding-right: 0px; width: 100px; margin-left: 5px;">
                                            <asp:Image runat="server" ID="Image1" Width="54px" Height="54px" Style="vertical-align: bottom;" />
                                            <asp:Image runat="server" ID="imgSex" Width="25px" Style="margin-left: -15px;" />
                                        </div>
                                        <div class="col-sm-10" style="padding-left: 0px; width: 86%;">
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
                                                                                <label> Admission No. </label>
                                                    </asp:Label><br />
                                                    <asp:Label CssClass="form-group" Font-Bold="true" runat="server" ID="lblAdmissionNo"></asp:Label>
                                                </div>
                                                <div class="btn-group" role="group" style="width: 95px; vertical-align: top">
                                                    <asp:Label CssClass="form-group" runat="server" ToolTip="Date of Birth">
                                                                                <label> DOB </label>
                                                    </asp:Label><br />
                                                    <asp:Label CssClass="form-group" Font-Bold="true" runat="server" ID="lblDOB"></asp:Label>
                                                </div>
                                                <div class="btn-group" role="group" style="width: 95px; vertical-align: top">
                                                    <asp:Label CssClass="form-group" runat="server">
                                                                                <label> Age </label>
                                                    </asp:Label><br />
                                                    <asp:Label CssClass="form-group" Font-Bold="true" runat="server" ID="lblAge"></asp:Label>
                                                </div>
                                                <div class="btn-group" role="group" style="width: 95px; vertical-align: top">
                                                    <asp:Label CssClass="form-group" runat="server">
                                                                                <label> Religion </label>
                                                    </asp:Label><br />
                                                    <asp:Label CssClass="form-group" Font-Bold="true" runat="server" ID="lblReligion"></asp:Label>
                                                </div>
                                                <div class="btn-group" role="group" style="max-width: 50%; vertical-align: top">
                                                    <asp:Label CssClass="form-group" runat="server">
                                                                                <label> Payer </label>
                                                    </asp:Label><br />
                                                    <asp:Label CssClass="form-group" Style="font-weight: bold; width: 100%;" runat="server" ID="lblPayer"></asp:Label>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>

                            &nbsp;
                                    
                            <div style="overflow-y: scroll; height: 400px;">
                                <asp:UpdatePanel runat="server">
                                    <ContentTemplate>
                                        <div runat="server" id="panel1"></div>
                                        <div id="img_noData" runat="server" visible="false" style="text-align: center;">
                                            <div>
                                                <img src="<%= Page.ResolveClientUrl("~/Images/Background/ic_noData.svg") %>" style="height: auto; width: 200px; margin-right: 3px; margin-top: 40px" />
                                            </div>
                                            <div runat="server">
                                                <span>
                                                    <h3 style="font-weight: 700; color: #585A6F">Oops! There is no data</h3>
                                                </span>
                                                <span style="font-size: 14px; color: #585A6F">Please search another date or parameter</span>
                                            </div>
                                        </div>
                                        <div id="img_noConnection" runat="server" visible="false" style="text-align: center;">
                                            <img src="<%= Page.ResolveClientUrl("~/Images/Background/ic_noConnection.svg") %>" style="height: auto; width: 200px; margin-right: 3px; margin-top: 40px;" />
                                            <span>
                                                <h3 style="font-weight: 700; color: #585A6F">No internet connection</h3>
                                            </span>
                                            <span style="font-size: 14px; color: #585A6F">Please check your connection & refresh</span>
                                        </div>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <asp:UpdatePanel runat="server" ID="updatePatientHistory" UpdateMode="Conditional">
            <ContentTemplate>
                <div id="patientHistoryModal" runat="server" class="modal fade" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
                    <div class="modal-dialog" style="width: 95%;" runat="server">
                        <div class="modal-content" style="border-radius: 7px; height: 100%;">
                            <div class="modal-header" style="height: 40px; padding-top: 10px; padding-bottom: 5px">
                                <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                                <h4 class="modal-title" style="text-align: left">
                                    <asp:Label ID="Label1" Style="font-family: Arial, Helvetica, sans-serif;" runat="server" Text="Patient History"></asp:Label></h4>
                            </div>
                            <div class="modal-body" style="background-color: white; border-radius: 7px">
                                <div style="width: 100%" class="btn-group" role="group">
                                    <iframe name="myIframe" id="myIframe" runat="server" style="width: 100%; height: 398px; border: none; margin-top: 0%; overflow-y: scroll; padding-right: 0; padding-left: 0%; margin-left: 0;"></iframe>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>

        <div class="modal fade" id="modalDiagProc" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
            <div class="modal-dialog" style="width: 600px; margin-top: 75px;" runat="server">
                <div class="modal-content" style="border-radius: 10px 10px !important;">
                    <div class="modal-header" style="height: 40px; padding-top: 10px; padding-bottom: 5px">
                        <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                        <h4 class="modal-title" style="text-align: left">
                            <asp:Label ID="Label3" Style="font-family: Arial, Helvetica, sans-serif;" runat="server" Text="Procedure & Diagnosis"></asp:Label></h4>
                    </div>
                    <div class="modal-body">
                        <asp:UpdatePanel ID="UpdatePanelDiagProc" runat="server">
                            <ContentTemplate>

                                <table border="0" style="margin-bottom: 10px;">
                                    <tr>
                                        <td>Tanggal Admisi</td>
                                        <td style="width: 20px; text-align: center;">: </td>
                                        <td>
                                            <asp:Label ID="LabelTglAdm" runat="server" Text="-"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>No Admisi</td>
                                        <td style="width: 20px; text-align: center;">: </td>
                                        <td>
                                            <asp:Label ID="LabelNoAdm" runat="server" Text="-"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>Dokter</td>
                                        <td style="width: 20px; text-align: center;">: </td>
                                        <td>
                                            <asp:Label ID="LabelDOkter" runat="server" Text="-"></asp:Label>
                                        </td>
                                    </tr>
                                </table>


                                <div style="background-color: aliceblue; text-align: left; margin-bottom: 15px; padding: 5px; border: 1px solid lightgrey; border-radius: 5px;">
                                    <div style="font-weight: bold; border-bottom: 1px solid lightgrey; margin-bottom: 5px;">Procedure</div>
                                    <asp:Repeater ID="Repeaterproc" runat="server">
                                        <ItemTemplate>
                                            <li>
                                                <asp:Label ID="LabelItemProc" runat="server" Text='<%# Eval("ProcedureItemName") %>'></asp:Label>
                                            </li>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </div>

                                <div style="background-color: aliceblue; text-align: left; margin-bottom: 15px; padding: 5px; border: 1px solid lightgrey; border-radius: 5px;">
                                    <div style="font-weight: bold; border-bottom: 1px solid lightgrey; margin-bottom: 5px;">Diagnosis</div>
                                    <asp:Repeater ID="Repeaterdiag" runat="server">
                                        <ItemTemplate>
                                            <li>
                                                <asp:Label ID="LabelItemDiag" runat="server" Text='<%# Eval("ProcedureItemName") %>'></asp:Label>
                                            </li>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>

                </div>
            </div>
        </div>

        <div class="modal fade" id="modalPrescription" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
            <div class="modal-dialog" style="width: 70%; margin-top: 75px;" runat="server">
                <div class="modal-content" style="border-radius: 10px 10px !important;">
                    <div class="modal-header" style="height: 40px; padding-top: 10px; padding-bottom: 5px">
                        <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                        <h4 class="modal-title" style="text-align: left">
                            <asp:Label ID="Label5" Style="font-family: Arial, Helvetica, sans-serif;" runat="server" Text="Prescription"></asp:Label></h4>
                    </div>
                    <div class="modal-body">
                        <asp:UpdatePanel ID="UpdatePanelPrescription" runat="server">
                            <ContentTemplate>

                                <table border="0" style="margin-bottom: 10px;">
                                    <tr>
                                        <td>Tanggal Admisi</td>
                                        <td style="width: 20px; text-align: center;">: </td>
                                        <td>
                                            <asp:Label ID="LabelTglAdmPres" runat="server" Text="-"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>No Admisi</td>
                                        <td style="width: 20px; text-align: center;">: </td>
                                        <td>
                                            <asp:Label ID="LabelNoAdmPres" runat="server" Text="-"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>Dokter</td>
                                        <td style="width: 20px; text-align: center;">: </td>
                                        <td>
                                            <asp:Label ID="LabelDOkterPres" runat="server" Text="-"></asp:Label>
                                        </td>
                                    </tr>
                                </table>

                                <div style="font-weight: bold; margin-bottom: 5px;">Drug</div>
                                <asp:GridView ID="GvwPrescriptionOrder" runat="server" AutoGenerateColumns="False" CssClass="table table-striped table-bordered table-condensed thinscroll"
                                    EmptyDataText="No Data" Font-Names="Helvetica">

                                    <Columns>
                                        <asp:TemplateField HeaderText="Item" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Font-Size="13px" HeaderStyle-Font-Names="Helvetica, Arial, sans-serif" HeaderStyle-Font-Bold="true">
                                            <ItemTemplate>
                                                <asp:Label runat="server" ID="lblitemname" Width="100%" Font-Size="11px" Font-Names="Helvetica, Arial, sans-serif" Text='<%# Bind("ItemName") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Dose" HeaderStyle-Font-Size="13px" HeaderStyle-Font-Names="Helvetica, Arial, sans-serif" HeaderStyle-Font-Bold="true">
                                            <ItemTemplate>
                                                <div runat="server" visible='<%# Eval("IsDoseText").ToString() == "False" %>'>
                                                    <asp:Label runat="server" ID="lbldose" Font-Size="11px" Font-Names="Helvetica, Arial, sans-serif" Text='<%# Bind("Dose") %>'></asp:Label>
                                                    &nbsp;
                                                    <asp:Label runat="server" ID="lbldoseuom" Font-Size="11px" Font-Names="Helvetica, Arial, sans-serif" Text='<%# Bind("DoseUom") %>'></asp:Label>
                                                </div>
                                                <asp:Label runat="server" ID="lbldosetext" Font-Size="11px" Font-Names="Helvetica, Arial, sans-serif" Visible='<%# Eval("IsDoseText") %>' Text='<%# Bind("dose_text") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <%-- <asp:TemplateField HeaderText="Dose UoM" HeaderStyle-Font-Size="13px" HeaderStyle-Font-Names="Helvetica, Arial, sans-serif" HeaderStyle-Font-Bold="true">
                                            <ItemTemplate>
                                                <asp:Label runat="server" ID="lbldoseuom" Width="100%" Font-Size="11px" Font-Names="Helvetica, Arial, sans-serif" Text='<%# Bind("DoseUom") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>--%>
                                        <asp:TemplateField HeaderText="Frequency" HeaderStyle-Font-Size="13px" HeaderStyle-Font-Names="Helvetica, Arial, sans-serif" HeaderStyle-Font-Bold="true">
                                            <ItemTemplate>
                                                <asp:Label runat="server" ID="lblfrequency" Width="100%" Font-Size="11px" Font-Names="Helvetica, Arial, sans-serif" Text='<%# Bind("Frequency") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Route" HeaderStyle-Font-Size="13px" HeaderStyle-Font-Names="Helvetica, Arial, sans-serif" HeaderStyle-Font-Bold="true">
                                            <ItemTemplate>
                                                <asp:Label runat="server" ID="lblroute" Width="100%" Font-Size="11px" Font-Names="Helvetica, Arial, sans-serif" Text='<%# Bind("Route") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Instruction" HeaderStyle-Font-Size="13px" HeaderStyle-Font-Names="Helvetica, Arial, sans-serif" HeaderStyle-Font-Bold="true">
                                            <ItemTemplate>
                                                <asp:Label runat="server" ID="lblinstruction" Width="100%" Font-Size="11px" Font-Names="Helvetica, Arial, sans-serif" Text='<%# Bind("Instruction") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Qty" HeaderStyle-Font-Size="13px" HeaderStyle-Font-Names="Helvetica, Arial, sans-serif" HeaderStyle-Font-Bold="true">
                                            <ItemTemplate>
                                                <asp:Label runat="server" ID="lblqty" Width="100%" Font-Size="11px" Font-Names="Helvetica, Arial, sans-serif" Text='<%# Bind("Quantity") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="UoM" HeaderStyle-Font-Size="13px" HeaderStyle-Font-Names="Helvetica, Arial, sans-serif" HeaderStyle-Font-Bold="true">
                                            <ItemTemplate>
                                                <asp:Label runat="server" ID="lbluom" Width="100%" Font-Size="11px" Font-Names="Helvetica, Arial, sans-serif" Text='<%# Bind("Uom") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Iter" HeaderStyle-Font-Size="13px" HeaderStyle-Font-Names="Helvetica, Arial, sans-serif" HeaderStyle-Font-Bold="true">
                                            <ItemTemplate>
                                                <asp:Label runat="server" ID="lbliter" Width="100%" Font-Size="11px" Font-Names="Helvetica, Arial, sans-serif" Text='<%# Bind("Iter") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Routine" HeaderStyle-Font-Size="13px" HeaderStyle-Font-Names="Helvetica, Arial, sans-serif" HeaderStyle-Font-Bold="true">
                                            <ItemTemplate>
                                                <asp:Label runat="server" ID="lblroutine" Width="100%" Font-Size="11px" Font-Names="Helvetica, Arial, sans-serif" Text='<%# Bind("Routine") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                    </Columns>
                                </asp:GridView>

                                <div style="font-weight: bold; margin-bottom: 5px;">Consumable</div>
                                <asp:GridView ID="GvwConsumableOrder" runat="server" AutoGenerateColumns="False" CssClass="table table-striped table-bordered table-condensed thinscroll"
                                    EmptyDataText="No Data" Font-Names="Helvetica">

                                    <Columns>
                                        <asp:TemplateField HeaderText="Item" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Font-Size="13px" HeaderStyle-Font-Names="Helvetica, Arial, sans-serif" HeaderStyle-Font-Bold="true">
                                            <ItemTemplate>
                                                <asp:Label runat="server" ID="lblitemname" Width="100%" Font-Size="11px" Font-Names="Helvetica, Arial, sans-serif" Text='<%# Bind("ItemName") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Qty" HeaderStyle-Font-Size="13px" HeaderStyle-Font-Names="Helvetica, Arial, sans-serif" HeaderStyle-Font-Bold="true">
                                            <ItemTemplate>
                                                <asp:Label runat="server" ID="lblqty" Width="100%" Font-Size="11px" Font-Names="Helvetica, Arial, sans-serif" Text='<%# Bind("Quantity") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="UoM" HeaderStyle-Font-Size="13px" HeaderStyle-Font-Names="Helvetica, Arial, sans-serif" HeaderStyle-Font-Bold="true">
                                            <ItemTemplate>
                                                <asp:Label runat="server" ID="lbluom" Width="100%" Font-Size="11px" Font-Names="Helvetica, Arial, sans-serif" Text='<%# Bind("Uom") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Instruction" HeaderStyle-Font-Size="13px" HeaderStyle-Font-Names="Helvetica, Arial, sans-serif" HeaderStyle-Font-Bold="true">
                                            <ItemTemplate>
                                                <asp:Label runat="server" ID="lblinstruction" Width="100%" Font-Size="11px" Font-Names="Helvetica, Arial, sans-serif" Text='<%# Bind("Instruction") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                    </Columns>
                                </asp:GridView>



                                <div style="font-weight: bold; margin-bottom: 5px;">Compound</div>
                                <asp:GridView ID="gvw_racikan_header" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-condensed table-header-racikan"
                                    EmptyDataText="No Data" DataKeyNames="prescription_compound_header_id" OnRowDataBound="gvw_racikan_header_RowDataBound">
                                    <Columns>
                                        <asp:TemplateField ItemStyle-Width="100%" HeaderStyle-BackColor="#f4f4f4" ItemStyle-CssClass="no-padding">
                                            <HeaderTemplate>
                                                <table style="width: 100%;">
                                                    <tr style="font-weight: bold; background-color: #f4f4f4;">
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
                                                        <%--<td style="width: 5%; text-align: right;">
                                                        </td> --%>
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


                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>

                </div>
            </div>
        </div>

    </form>

    <script type="text/javascript">
        function openLabResultModal(ticket, patient, admission) {
            var hdnFieldAdmm = document.getElementById("<%=hf_admiss_id.ClientID %>");
            var hdnFieldPatient = document.getElementById("<%=hf_patient_id.ClientID %>");
            var hdnFieldTicket = document.getElementById("<%=hf_ticket_patient.ClientID %>");
            hdnFieldAdmm.value = admission;
            hdnFieldPatient.value = patient;
            hdnFieldTicket.value = ticket;
            document.getElementById("<%=HiddenLabMark.ClientID %>").value = 1;
        }

        function openLabModal() {
            $('#laboratoryResult').modal('show');
        }

        function openPatientHistoryModal() {
            $('#patientHistoryModal').modal('show');
        }

        function openDiagprocModal(orgid, ptnid, admid, encid, admdate, admnno, doctor) {
            $('#modalDiagProc').modal('show');
            document.getElementById("<%=HF_orgid.ClientID %>").value = orgid;
            document.getElementById("<%=HF_ptnid.ClientID %>").value = ptnid;
            document.getElementById("<%=HF_admid.ClientID %>").value = admid;
            document.getElementById("<%=HF_encid.ClientID %>").value = encid;

            var tglll = document.getElementById("<%=HF_tgladmn.ClientID %>");
            var nooo = document.getElementById("<%=HF_noadm.ClientID %>");
            var dooo = document.getElementById("<%=HF_dokter.ClientID %>");
            tglll.value = admdate;
            nooo.value = admnno;
            dooo.value = doctor;

            tglll.value = tglll.value.toString().replace(/_/g, " ");
            dooo.value = dooo.value.toString().replace(/_/g, " ");
        }

        function openPrescriptionModal(orgid, ptnid, admid, encid, admdate, admnno, doctor) {
            $('#modalPrescription').modal('show');
            document.getElementById("<%=HF_orgid.ClientID %>").value = orgid;
            document.getElementById("<%=HF_ptnid.ClientID %>").value = ptnid;
            document.getElementById("<%=HF_admid.ClientID %>").value = admid;
            document.getElementById("<%=HF_encid.ClientID %>").value = encid;

            var tglll = document.getElementById("<%=HF_tgladmn.ClientID %>");
            var nooo = document.getElementById("<%=HF_noadm.ClientID %>");
            var dooo = document.getElementById("<%=HF_dokter.ClientID %>");
            tglll.value = admdate;
            nooo.value = admnno;
            dooo.value = doctor;

            tglll.value = tglll.value.toString().replace(/_/g, " ");
            dooo.value = dooo.value.toString().replace(/_/g, " ");
        }
    </script>
</body>
</html>

