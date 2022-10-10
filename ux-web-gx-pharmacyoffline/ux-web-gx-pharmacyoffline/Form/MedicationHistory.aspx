<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MedicationHistory.aspx.cs" Inherits="Form_MedicationHistory" %>

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
    <link rel="stylesheet" href="../Content/EMR-PharmacyOffline.css" />
    <!-- Select Bootstrap -->
    <link rel="stylesheet" href="~/Content/bootstrap-select/css/bootstrap-select.css" />

    <link href="favicon.ico" rel="shortcut icon" type="image/x-icon" />

       
    <link href="~/favicon.ico" rel="shortcut icon" type="image/x-icon" /><meta name="viewport" content="width=device-width, initial-scale=1.0" />
    
    <style type="text/css">
        
        .btnSave 
        {
            width: 73px;
            height: 24px;
            font-family: Helvetica;
            font-size: 12px;
            font-weight: bold;
            font-style: normal;
            font-stretch: normal;
            line-height: 1.17;
            border-radius: 4px;
            background-color: #4d9b35;
            color:white;
            border:none;
        }
        
        .btnSave:hover 
        {
            width: 73px;
            height: 24px;
            font-family: Helvetica;
            font-size: 12px;
            font-weight: bold;
            font-style: normal;
            font-stretch: normal;
            line-height: 1.17;
            border-radius: 4px;
            background-color: #42852e;
            color:white;
            border:none;
        }
        
        
        .btn-white {
            color: #000;
            background-color: #fff;
            border-color: #cdced9;
            padding: 3px;
            padding-left: 4px;
            font-size: 12px;
        }

        @media screen and (max-width: 1366px) {
            .shadows {
                border-radius: 10px;
                padding: 7px 7px 7px 7px;
                width: 100%;
                max-width: 1280px;
                border: 2px solid lightgrey;
            }
        }

        @media screen and (min-width: 1367px) {
            .shadows {
                border-radius: 10px;
                padding: 7px 7px 7px 7px;
                width: 100%;
                border: 2px solid lightgrey;
            }
        }

        .shadows-search {
            border: 1px;
            border-radius: 10px;
            box-shadow: 0px 1px 5px #9293A0;
            padding: 7px 7px 7px 7px;
            width: 200px;
        }

        .itemcontainersave > div {
            padding-top: 0px;
            padding-bottom: 2px;
        }

        .bottomMenuu {
            position: fixed;
            top: 85%;
            text-align: left;
            z-index: 20;
        }

        .hidee {
            opacity: 0;
            right: -100px;
            transition: all 0.5s;
        }

        .testborder{
            border: 1px solid red;
            
        }

    </style>
</head>
    
    <body>
        <form id="form1" runat="server">
            <script type="text/javascript">
            $(window).load(function() {
		        $(".loadPage").fadeOut("slow");
            });



            function PageLoad() {
                $('.selecpicker').selectpicker();
            }

            $(document).ready(function () {
            $('.selecpicker').selectpicker();
            var prm = Sys.WebForms.PageRequestManager.getInstance();
            if (prm != null) {
                prm.add_endRequest(function (sender, e) {
                    if (sender._postBackSettings.panelsToUpdate != null) {
                        $('.selecpicker').selectpicker();
                    }
                });
            };          

            function Open(content) {
                document.getElementById('<%=hfCompoundName.ClientID%>').value = content;
                document.getElementById('<%=btnSample.ClientID%>').click();
                openModal();
                return true;
            }

            function openModal() {
                $('#modalCompound').modal('show');
                return true;
            }

            $(window).scroll(function () {

                if ($(this).scrollTop() > 0) {
                    $('.filterdata').fadeOut();
                }
                else {
                    $('.filterdata').fadeIn();
                }
            });

            function topFunction() {
                document.body.scrollTop = 0;
                document.documentElement.scrollTop = 0;
            }

            $('body').scroll(function (e) {
                if ($(this).scrollTop() > 250) {
                    $("#myIDtoTop").attr('class', 'bottomMenuu showw');
                } else {
                    $("#myIDtoTop").attr('class', 'bottomMenuu hidee');
                }
            });

            function switchBahasa() {
                var bahasa = document.getElementById('<%=HFisBahasa.ClientID%>').value;
                if (bahasa == "ENG") {
                    if (document.getElementById('lblbhs_nodata') != null) {
                        document.getElementById('lblbhs_nodata').innerHTML = "Oops! There is no data";
                        document.getElementById('lblbhs_subnodata').innerHTML = "Please search another date or parameter";
                    }

                }
                else if (bahasa == "IND") {
                    if (document.getElementById('lblbhs_nodata') != null) {
                        document.getElementById('lblbhs_nodata').innerHTML = "Oops! Tidak ada data";
                        document.getElementById('lblbhs_subnodata').innerHTML = "Silakan cari tanggal atau parameter lain";
                    }
                }
            }

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
            
            <asp:HiddenField ID="HFisBahasa" runat="server" />
            <asp:UpdatePanel runat="server" UpdateMode="Conditional" ID="UPmedicalHistory">
            <ContentTemplate>
                <asp:Panel ID="medical_history_panel" runat="server">
                    <div class="container-fluid kartu-pasien">
                        <asp:HiddenField ID="hfPatientId" runat="server" />
                        <asp:HiddenField ID="hfEncounterId" runat="server" />
                        <asp:HiddenField ID="hfAdmissionId" runat="server" />
                        <asp:HiddenField ID="hfPageSoapId" runat="server" />
                        <asp:HiddenField ID="hfCompoundName" runat="server" />
                    </div>

                    <div style="width: 100%; background-color: white; min-height: calc(100vh - 125px);">

                        <%--<section id="main_page" style="background-color: red; margin-top: -150px; padding-top: 150px; margin-bottom: 75px"></section>--%>
                        <div class="filterdata btn-group btn-group-justified" style="background-color: whitesmoke; height: 70px; position: fixed; margin-top: -50px; padding-left: 15px; padding-top: 15px;" role="group" aria-label="...">
                            <table border="0">
                                <tr>
                                    <td>Organizations </td>
                                    <td>&nbsp; </td>
                                    <td>Encounter Times </td>
                                    <td>&nbsp; </td>
                                    <td>Search by admission or doctor </td>
                                    <td>&nbsp; </td>
                                    <td>&nbsp; </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:DropDownList ID="dllOrganizationCode" runat="server" Height="25px" Width="130px" Style="border-radius: 2px; border: solid 1px #cdced9; height: 24px; max-width: 100%; width: 100%; resize: none;" OnTextChanged="dllOrganizationCode_OnTextChange" AutoPostBack="true"></asp:DropDownList>
                                    </td>
                                    <td style="width: 20px;">&nbsp; </td>
                                    <td>
                                        <asp:DropDownList ID="ddlEncounterMode" runat="server" Height="25px" Width="130px" Style="border-radius: 2px; border: solid 1px #cdced9; height: 24px; max-width: 100%; width: 100%; resize: none;">
                                            <asp:ListItem Text="Last Encounter" Value="1"></asp:ListItem>
                                            <asp:ListItem Text="Last 5 Encounter" Value="5"></asp:ListItem>
                                            <asp:ListItem Text="Last 10 Encounter" Value="10"></asp:ListItem>
                                            <asp:ListItem Text="Last 20 Encounter" Value="20"></asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                    <td style="width: 30px; text-align: center;">OR </td>
                                    <td>
                                        <asp:DropDownList ID="ddldoctor" runat="server" Height="25px" Width="130px" CssClass="selectpicker" data-live-search="true" data-style="btn-white" data-size="7" data-width="200px" data-height="24px" data-dropup-auto="false" data-container="body" Style="border-radius: 2px; border: solid 1px #cdced9; height: 24px; max-width: 100%; width: 100%; resize: none; z-index: -1;">
                                        </asp:DropDownList>
                                    </td>
                                    <td style="width: 20px;">&nbsp; </td>
                                    <td>
                                        <asp:Button ID="btnSearch" runat="server" Width="70px" Text="Search" CssClass="btn btn-emr-small btn-lightGreen" OnClick="btnSearch_Click" />
                                    </td>
                                </tr>
                            </table>
                        </div>

                        <div id="img_noData" runat="server" visible="false" style="text-align: center; padding-top: 6%;">
                            <div>
                                <img src="<%= Page.ResolveClientUrl("~/Images/Background/ic_noData.svg") %>" style="height: auto; width: 200px; margin-right: 3px; margin-top: 40px" />
                            </div>
                            <div runat="server" id="no_patient_data">
                                <span>
                                    <h3 style="font-weight: 700; color: #585A6F">
                                        <%--<label style="display: <%=setENG%>;">Oops! There is no data </label>
                                        <label style="display: <%=setIND%>;">Oops! Tidak ada data </label>--%>
                                        <label id="lblbhs_nodata">Oops! There is no data </label>
                                    </h3>
                                </span>
                                <span style="font-size: 14px; color: #585A6F">
                                    <%--<label style="display: <%=setENG%>;">Please search another date or parameter </label>
                                    <label style="display: <%=setIND%>;">Silakan cari tanggal atau parameter lain </label>--%>
                                    <label id="lblbhs_subnodata">Please search another date or parameter </label>
                                </span>
                            </div>
                        </div>

                        <div style="width: 100%;padding-top:2%">
                            <div runat="server" id="prescription" role="group" style="overflow: auto"></div>
                        </div>
                    </div>

                    <!-- buat jaga2 -->
                    <div class="teta" style="height: 175px; display: none;"></div>
                    <asp:GridView ID="gvw_history" runat="server" Visible="false" EmptyDataText="No Data">
                    </asp:GridView>
                    <!-- end buat jaga2 -->

                </asp:Panel>

                <a class="item" href="javascript:topFunction();">
                    <div id="myIDtoTop" class="bottomMenuu hidee">
                        <span>
                            <img src="../Images/Result/ic_Arrow_Top.png" /></span>
                    </div>
                </a>
            </ContentTemplate>
        </asp:UpdatePanel>

            <div class="modal fade" id="modalCompound" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
            <asp:UpdatePanel ID="UPCompound" UpdateMode="Conditional" runat="server">
                <ContentTemplate>
                    <div class="modal-dialog" style="width: 70%;">
                        <div class="modal-content" style="border-radius: 7px; height: 100%;">
                            <div class="modal-header" style="height: 40px; padding-top: 10px; padding-bottom: 5px">
                                <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                                <h4 class="modal-title" style="text-align: left">
                                    <asp:Button runat="server" ID="btnSample" Text="" CssClass="hidden" OnClick="compoundDetail_Click" />
                                    <asp:Label ID="headerCompound" Style="font-family: Helvetica, Arial, sans-serif; font-weight: bold" runat="server"></asp:Label></h4>
                            </div>
                            <div class="btn-group btn-group-justified" style="width: 100%; background-color: lightgrey" role="group" aria-label="...">
                                <div class="btn-group container-fluid" role="group" style="width: 3%">
                                    <div>
                                        <label>Order Set Name</label>
                                    </div>
                                    <div style="font-weight: bold">
                                        <asp:Label runat="server" ReadOnly="true" ID="orderSetName" Width="100%"></asp:Label>
                                    </div>
                                </div>

                                <div class="btn-group" role="group" style="width: 1%">
                                    <div>
                                        <label>Qty</label>
                                    </div>
                                    <div style="font-weight: bold">
                                        <asp:Label runat="server" ReadOnly="true" ID="qtyOrderSetName"></asp:Label>
                                    </div>
                                </div>

                                <div class="btn-group" role="group">
                                    <div>
                                        <label>U.O.M.</label>
                                    </div>
                                    <div style="font-weight: bold">
                                        <asp:Label runat="server" ReadOnly="true" ID="uomOrderSetName"></asp:Label>
                                    </div>
                                </div>

                                <div class="btn-group" role="group">
                                    <div>
                                        <label>Frequency</label>
                                    </div>
                                    <div style="font-weight: bold">
                                        <asp:Label runat="server" ReadOnly="true" ID="frequencyOrderSetName"></asp:Label>
                                    </div>
                                </div>

                                <div class="btn-group" role="group">
                                    <div>
                                        <label>Dose</label>
                                    </div>
                                    <div style="font-weight: bold">
                                        <asp:Label runat="server" ReadOnly="true" ID="doseOrderSetName"></asp:Label>
                                    </div>
                                </div>

                                <div class="btn-group" role="group">
                                    <div>
                                        <label>Dose Text</label>
                                    </div>
                                    <div style="font-weight: bold">
                                        <asp:Label runat="server" ReadOnly="true" ID="dose_textOrderSetName"></asp:Label>
                                    </div>
                                </div>

                                <div class="btn-group" role="group">
                                    <div>
                                        <label>Instruction</label>
                                    </div>
                                    <div style="font-weight: bold">
                                        <asp:Label runat="server" ReadOnly="true" ID="instructionOrderSetName"></asp:Label>
                                    </div>
                                </div>

                                <div class="btn-group" role="group">
                                    <div>
                                        <label>Route</label>
                                    </div>
                                    <div style="font-weight: bold">
                                        <asp:Label runat="server" ReadOnly="true" ID="routeOrderSetName"></asp:Label>
                                    </div>
                                </div>

                                <div class="btn-group" role="group">
                                    <div>
                                        <label>Iter</label>
                                    </div>
                                    <div style="font-weight: bold">
                                        <asp:Label runat="server" ReadOnly="true" ID="iterOrderSetName"></asp:Label>
                                    </div>
                                </div>
                            </div>
                            <div class="modal-body">
                                <div style="width: 100%; background-color: white">
                                    <asp:GridView ID="gvw_detail_compound" runat="server" CssClass="table table-striped table-condensed" BorderColor="Transparent" AutoGenerateColumns="false">
                                        <Columns>
                                            <asp:TemplateField HeaderText="Item" HeaderStyle-ForeColor="#3C8DBC" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="itemName" Wrap="true" Style="resize: none" BackColor="Transparent" Width="400px" ReadOnly="true" BorderColor="Transparent" runat="server" Text='<%# Bind("itemName") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Qty" HeaderStyle-ForeColor="#3C8DBC" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="Oty" Wrap="true" Style="resize: none" TextMode="MultiLine" BackColor="Transparent" Width="100%" ReadOnly="true" BorderColor="Transparent" runat="server" Text='<%# Bind("quantity") %>'></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="U.O.M." HeaderStyle-ForeColor="#3C8DBC" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="uom" Wrap="true" Style="resize: none" TextMode="MultiLine" BackColor="Transparent" Width="100%" ReadOnly="true" BorderColor="Transparent" runat="server" Text='<%# Bind("uom") %>'></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Dose Text" HeaderStyle-ForeColor="#3C8DBC" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="dose_text" Wrap="true" Style="resize: none" TextMode="MultiLine" BackColor="Transparent" Width="100%" ReadOnly="true" BorderColor="Transparent" runat="server" Text='<%# Bind("doseText") %>'></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Instruction" HeaderStyle-ForeColor="#3C8DBC" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="instruction" Wrap="true" Style="resize: none" TextMode="MultiLine" BackColor="Transparent" Width="100%" ReadOnly="true" BorderColor="Transparent" runat="server" Text='<%# Bind("instruction") %>'></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </div>
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
             </form>
    <%--MODAL REALESHED VARMASI--%>
         <div class="modal fade" id="modal-released-pharmacy">
            <div class="modal-dialog" style="top: -1%; height: 80%; width: 728px;">
                <div class="modal-content" style="">
                    <div class="modal-header" style="height: 35px; padding-top: 7px; padding-bottom: 5px">
                        <h4 class="modal-title">
                        <asp:Label runat="server" Font-Bold="true" style="display:inline" ID="Label4" ClientIDMode="Static" Text="Drug Prescription (Pharmacy Edit)"></asp:Label></h4>
                        <button style="border: none;background: transparent;position: absolute;font-size:14px;font-weight:bold;top: 10px;right: 20px;" data-dismiss="modal" aria-hidden="true"><i class="fa fa-times" aria-hidden="true"></i></button>
                    </div>
                    <div class="modal-body" style="margin-top:0px">
                        <div class="row">
                            <div class="col-sm-12">
                                <iframe name="Iframereleasedpharmacy" id="Iframereleasedpharmacy" runat="server" style="width: 100%; height: 75vh; border: none;"></iframe>
                            </div>
                            
                        </div>
                    </div>

                </div>
            </div>
        </div>
    </body>


   
</html>

<script>
      function showModalRelasedByFarmasi() {
                    $('#modal-released-pharmacy').modal('show');
                    return true;
      }
</script>


