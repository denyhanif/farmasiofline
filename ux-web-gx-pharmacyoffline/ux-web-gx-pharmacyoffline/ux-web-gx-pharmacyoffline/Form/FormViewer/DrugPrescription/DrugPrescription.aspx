<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DrugPrescription.aspx.cs" Inherits="Form_FormViewer_Drug_Prescription_DrugPrescription" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
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
    <style>
        .font-content-dashboard {
            font-size: 14px !important;
        }

        th {
            background: #F2F3F4;
        }
    </style>
</head>

<body>
    <form id="form1" runat="server">
        <div>
            <div class="row">
                <div class="col-sm-12" style="position: fixed; margin-top: -50px;">
                    <label class="font-content-dashboard" style="font-size: 14px; font-weight: bold; display: block; font-family: sans-serif;">Edit Reason :</label>
                    <asp:Label runat="server" ID="txt_reason" Style="font-family: sans-serif; font-size: 12px; margin: 10px 0px 10px 0px; letter-spacing: 0.3px;" Text="Insufficient Stock, Incorrect Dose, Incorrect Quantity"></asp:Label>

                    <label class="font-content-dashboard" style="margin-top: 10px; font-size: 14px; font-weight: bold; display: block; font-family: sans-serif;">Call Doctor: </label>
                    <asp:Label runat="server" ID="txt_calldoctor" Style="font-family: sans-serif; font-size: 12px; margin: 10px 0px 10px 0px; letter-spacing: 0.3px;" Text="Contacted, 25 Mar 2019 - 12:00"></asp:Label>

                    <label class="font-content-dashboard" style="margin-top: 10px; font-size: 14px; font-weight: bold; display: block; font-family: sans-serif;">Pharmacist Note : </label>
                    <asp:Label runat="server" ID="txt_pharmacistnote" Style="font-family: sans-serif; font-size: 12px; margin: 10px 0px 10px 0px; letter-spacing: 0.3px;" Text="Panadol habis dan diganti dengan SUMAGESIC, sudah melakukan confirm ke dokter Eka."></asp:Label>

                    <div style="margin-top: 10px; height: 350px;overflow-y: scroll;overflow-x:hidden">
                        <div class="row"  ">
                            <div style="width: 100%">
                                <table style="width:100%;border: 1px solid #F2F3F4">
                                    <thead >
                                        <tr style="background-color: #F2F3F4;border: 1px solid #D3D3D3;" >
                                            <td style="">
                                                <label style="margin-top: 10px;  font-size: 14px; font-weight: bold; display: block; font-family: sans-serif;padding-left:30px">Doctor’s Prescription</label>
                                            </td>
                                             <td style="border: 1px solid #F2F3F4">
                                                <label style="margin-top: 10px; font-size: 14px; font-weight: bold; display: block; font-family: sans-serif;padding-left:30px">Pharmacist Release</label>
                                            </td>

                                        </tr>
                                    </thead>
                                    <tbody >
                                        <asp:Repeater runat="server" ID="rpt_doctordrugs">
                                            <ItemTemplate>
                                                <tr style="border: 1px solid #F2F3F4">
                                                    <td style='border: 1px solid #D3D3D3;padding:10px 0px 30px 30px;color:<%# Eval("d_edit_action").ToString() == "change" || Eval("d_edit_action").ToString() == "delete" ? "#E21100" : Eval("d_edit_action").ToString() == "add" ? "#0A5CC6" : "#171717"%>'>
                                                        
                                                        <asp:Label runat="server" Text='<%# Eval("d_item_name" ).ToString().ToUpper() + "&nbsp  " %>'> </asp:Label>
                                                            <asp:Label runat="server" Visible='<%# Eval("d_edit_action").ToString() == "delete" ? true: false %>'>
                                                              <span style="height:88px;width:auto;border-radius:4px;background-color:#E21100;color:#F2F3F4;font-size:12px;padding:2px 7px 2px 7px">Not Releashed</span>  </br>

                                                            </asp:Label>
                                                            
                                                        </br>
                                                        <asp:label runat="server" Text="Jumlah :" Visible='<%# string.IsNullOrEmpty(Eval("d_quantity").ToString()) ? false :true %>'></asp:label><asp:Label runat="server" Text='<%# Eval("d_quantity" ).ToString() + "&nbsp  " %>'/> <asp:Label runat="server" Text='<%# Eval("d_uom" ).ToString()+ "&nbsp  " %>'/></br>
                                                        <asp:Label runat="server" Text='<%# Eval("d_frequency" ).ToString().ToUpper()+ "&nbsp  " %>'/>  <asp:Label runat="server" Text='<%# Eval("d_dose" ).ToString().ToUpper()+ "&nbsp  " %>'/> <asp:Label runat="server" Text='<%# Eval("d_dose_uom" ).ToString().ToUpper()+ "&nbsp  " %>'/></br>
                                                        <asp:label runat="server" Visible='<%# string.IsNullOrEmpty(Eval("d_administration_route").ToString()) ? false :true %>' Text="Rute : "></asp:label><asp:Label runat="server" Text='<%# Eval("d_administration_route" ).ToString()+ "&nbsp  " %>'/> <asp:label runat="server" Visible='<%# string.IsNullOrEmpty(Eval("d_created_date").ToString()) ? false :true %>' Text="Tanggal :" > </asp:label><asp:Label runat="server" Text='<%# string.IsNullOrEmpty(Eval("d_created_date").ToString()) ? "" : Convert.ToDateTime(DataBinder.Eval(Container.DataItem,"d_created_date")).ToString("dd MMM yyyy") %>'/></br>
                                                        <asp:label runat="server" Text="Pemberian :" Visible='<%# string.IsNullOrEmpty(Eval("d_instruction").ToString()) ? false :true %>' ></asp:label><asp:Label runat="server" Text='<%# Eval("d_instruction" ).ToString().ToLower()+ "&nbsp  " %>'></asp:Label>
                                                    </td>
                                                    <td style='border: 1px solid #D3D3D3;padding:10px 0px 30px 30px;color:<%# Eval("d_edit_action").ToString() == "change" || Eval("d_edit_action").ToString() == "delete" ? "#E21100" : Eval("d_edit_action").ToString() == "add" ? "#0A5CC6" : "#171717"%>'>
                                                       <asp:Label runat="server" Text='<%# Eval("p_item_name" ).ToString().ToUpper() + "&nbsp  " %>'> </asp:Label></br>
                                                        <asp:label runat="server" Text="Jumlah :" Visible='<%# string.IsNullOrEmpty(Eval("p_quantity").ToString()) ? false :true %>'></asp:label><asp:Label runat="server" Text='<%# Eval("p_quantity" ).ToString() + "&nbsp  " %>'/> <asp:Label runat="server" Text='<%# Eval("p_uom" ).ToString()+ "&nbsp  " %>'/></br>
                                                        <asp:Label runat="server" Text='<%# Eval("p_frequency" ).ToString().ToUpper()+ "&nbsp  " %>'/>  <asp:Label runat="server" Text='<%# Eval("p_dose" ).ToString().ToUpper()+ "&nbsp  " %>'/> <asp:Label runat="server" Text='<%# Eval("p_dose_uom" ).ToString().ToUpper()+ "&nbsp  " %>'/></br>
                                                        <asp:label runat="server" Visible='<%# string.IsNullOrEmpty(Eval("p_administration_route").ToString()) ? false :true %>' Text="Rute : "></asp:label><asp:Label runat="server" Text='<%# Eval("p_administration_route" ).ToString()+ "&nbsp  " %>'/> <asp:label runat="server" Visible='<%# string.IsNullOrEmpty(Eval("p_created_date").ToString()) ? false :true %>' Text="Tanggal :" > </asp:label><asp:Label runat="server" Text='<%# string.IsNullOrEmpty(Eval("p_created_date").ToString()) ? "" : Convert.ToDateTime(DataBinder.Eval(Container.DataItem,"p_created_date")).ToString("dd MMM yyyy") %>'/></br>
                                                        <asp:label runat="server" Text="Pemberian :" Visible='<%# string.IsNullOrEmpty(Eval("p_instruction").ToString()) ? false :true %>' ></asp:label><asp:Label runat="server" Text='<%# Eval("p_instruction" ).ToString().ToLower()+ "&nbsp  " %>'></asp:Label>
                                                   </td>
                                                </tr>
                                            </ItemTemplate>

                                        </asp:Repeater>

                                    </tbody>



                                </table>
                            </div>
                         
                        </div>

                    </div>

                   <%-- <div style="margin-top: 10px; height: 594px; overflow-y: scroll;">
                        <asp:GridView runat="server" ID="gv_releashed_pharmachy" CssClass="table-condensed " Height="594px" AutoGenerateColumns="False"
                            BorderWidth="1" BorderColor="#F2F3F4">
                            <Columns>
                                <asp:TemplateField runat="server" HeaderText="Doctor’s Prescription" ItemStyle-Width="26%" HeaderStyle-Font-Size="12px" ControlStyle-ForeColor="#171717" ItemStyle-VerticalAlign="Top" HeaderStyle-CssClass="font-content-dashboard table-sub-header-label">
                                    <ItemTemplate>
                                        <asp:Label Font-Size="12px" class="font-content-dashboard" Font-Names="Helvetica, Arial, sans-serif" ID="date_soap" runat="server" Text=""> <%# Eval("Prescription").ToString().Replace("\\n","<br />") %> </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField runat="server" HeaderText="Pharmacist Release" ItemStyle-Width="26%" HeaderStyle-Font-Size="12px" ItemStyle-VerticalAlign="Top" HeaderStyle-CssClass="font-content-dashboard table-sub-header-label">
                                    <ItemTemplate>
                                        <asp:Label Font-Size="12px" class="font-content-dashboard" Font-Names="Helvetica, Arial, sans-serif" ID="date_soap" runat="server" Text=""> <%# Eval("Prescription").ToString().Replace("\\n","<br />") %> </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>--%>

                    <%--                                <iframe name="Iframereleasedpharmacy" id="Iframereleasedpharmacy" runat="server" style="width: 100%; height: 70vh; border: none;"></iframe>--%>
                </div>

            </div>
        </div>
    </form>
</body>
</html>
