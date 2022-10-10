<%@ Page Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="FormPrescription.aspx.cs" Inherits="Form_FormPrescription" %>
<%@ MasterType VirtualPath="~/Site.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <asp:TextBox ID="txtencounterid" runat="server" CssClass="hidden"></asp:TextBox>
    <asp:TextBox ID="txtclasschevron" runat="server" CssClass="hidden"></asp:TextBox>
    <style type="text/css">


        .textHeader {
          font-family: Helvetica;
          font-size: 22px;
          font-weight: bold;
          font-style: normal;
          font-stretch: normal;
          line-height: 1.18;
          letter-spacing: normal;
          text-align: left;
          color: #131f8b;
          text-transform: capitalize;   
        }

        .shadowsBox {
            box-shadow: 0 3px 6px 0 rgba(0, 0, 0, 0.16);
        }

        .hiddencol {
            display: none;
        }

        .border {
            border-collapse: collapse;
            border: 1px;
        }

        .shadows {
            border: 1px;
            border-radius: 10px;
            box-shadow: 0px 1px 5px #9293A0;
            padding: 7px 7px 7px 7px;
            width: 300px;
            height: 150px;
        }

        .shadows-search {
            border: 1px;
            border-radius: 2px;
            padding: 7px 7px 7px 7px;
            height: 32px;
            border:solid 1px;
            border-color: #cdced9;
        }

        .leftandtop {
            padding-top  : 10px;
            padding-left : 10px;
        }

        .leftonly {
            padding-left : 15px;
        }

        .sidenavleft {
            height: 100%;
            width: 24%;
            position: fixed;
            z-index: 1;
            background-color: white;
            overflow-x: hidden;
            padding-top: 1%;
            top:0;
        }

        .innersidenavleft {
            position: fixed;
            width: 24%;
            position: fixed;
            z-index: 1;
            background-color: white;
            overflow-x: hidden;
            padding-top: 20px;
            top:0;
        }


        .sidenavright {
            height: 100%;
            width: 250px;
            position: fixed;
            z-index: 1;
            background-color: white;
            overflow-x: hidden;
            padding-top: 50px;
            right:0;
            top:0;
        }


        .main {
            margin-left: 190%; /* Same width as the sidebar + left position in px */
            margin-right: 20%;
            font-size: 28px; /* Increased text to enable scrolling */
            padding: 0px 10px;
        }
        
        .sidenav a {
            padding: 6px 8px 6px 16px;
            text-decoration: none;
            font-size: 25px;
            color: grey;
            display: block;
        }

        @media screen and (max-height: 450px) {
            .sidenav {padding-top: 15px;}
            .sidenav a {font-size: 18px;}
        }

        
        h3 {
            color: black;
            padding-left:7px;
        }

        p {
            font-family: Helvetica;
            font-size:12px;
            padding-left:7px;
        }


        .valuebackground{
           background-color: #dfe0e6; 
           border-radius: 8px; 
           width: 53px; 
        }


                /* Style the tab */
        .tab {
            overflow:hidden;
            background-color: #ffffff;
            padding-top:20px;
        }

        /* Style the buttons inside the tab */
        .tab button {
            background-color: inherit;
            float: left;
            border: none;
            outline: none;
            cursor: pointer;
            height:21px;
            padding-bottom:28px;
            transition: 0.3s;
            font-size: 17px;
        }

        /* Change background color of buttons on hover */
        .tab button:hover {
            background-color: #ddd;
        }

        /* Create an active/current tablink class */
        .tab button.active {
            background-color: #e7e8ef;
        }

        /* Style the tab content */
        .tabcontent {

            padding: 6px 12px;
            border-bottom: 1px solid #ccc;
            padding-bottom: 15px;
            background-color: #e7e8ef;
            border-top-color:none;
            height:120px;
        }

        .tabcontentprescription {

            padding: 6px 12px;
            border-bottom: 1px solid #ccc;
            padding-bottom: 15px;
            border-top-color:none;
            border-bottom:none;
        }


        .valueofprescription {
            font-size:13px;
            font-family:Helvetica;
            text-align:center;
        }

        .valueofprescriptiontime {
            font-size:10px;
            font-family:Helvetica;
            text-align:right;
        }


        .wholediv{
            width: 100px;
            height: 100px;
            background-color: #efefef;
        }

        .wholediv a {
            width: 100px;
            height: 100px;
            display:block;
            background-color: pink;
        }

        .buttonFlat {
             border: 0;
             background: none;
             box-shadow: none;
             border-radius: 0px;
             background-color:#fff;
        }

        .buttonFlat {
             border: 0;
             background: none;
             box-shadow: none;
             border-radius: 0px;
             background-color:#fff;
        }


        .buttonFlat:hover {
             background-color: #e7e8ef;
        }
        
        .focus {
            border: 0;
            background: none;
            box-shadow: none;
            border-radius: 0px;
            background-color:#e7e8ef;
         }

        #patient {
            font-family:Helvetica;
            border-collapse: collapse;
            width: 100%;
        }

        .boldTitle{
          font-family: Helvetica;
          font-size: 12px;
          font-weight: bold;
          font-style: normal;
          font-stretch: normal;
          line-height: 1.18;
          letter-spacing: normal;
          text-align: left;
          color: #171717;
        }

        .boldItem{
          font-family: Helvetica;
          font-size: 12px;
          font-weight: bold;
          font-style: normal;
          font-stretch: normal;
          line-height: 1.18;
          letter-spacing: normal;
          text-align: left;
          color: #171717;
        }

        .greyItem{
          font-family: Helvetica;
          font-size: 12px;
          font-weight: normal;
          font-style: normal;
          font-stretch: normal;
          line-height: 1.17;
          letter-spacing: normal;
          text-align: left;
          color: #585a6f;
        }

        .linkEdit {
          font-family: Helvetica;
          font-size: 11px;
          font-weight: bold;
          font-style: normal;
          font-stretch: normal;
          line-height: 1.18;
          letter-spacing: normal;
          text-align: right;
          color: #cdced9;
          margin-left:35px;
        }

        .blackItem {
          width: 189px;
          height: 24px;
          font-family: Helvetica;
          font-size: 11px;
          font-weight: normal;
          font-style: normal;
          font-stretch: normal;
          line-height: 1.18;
          letter-spacing: normal;
          text-align: left;
          color: #171717;
        }

        .medHistoryButton {
            border:solid 1px;
            border-color:#29af00;
            height:35px;
            width:35px;
            border-radius: 4px;
            background-image:url(/Images/NavigationMenu/ic_MedicationHistory.png);
            background-color:#85f263;
            
        }

        .overlay {
           background:rgb(133,242,99);
           opacity:0.5;
        }


        .btnTake {
            background-color:#e7e8ef;
        }

        .btnTake:hover {
            background-color:#5c1c16;
        }

        .btnCancel{
            width: 103px;
            height: 32px;
            font-family: Helvetica;
            font-size: 12px;
            font-weight: bold;
            font-style: normal;
            font-stretch: normal;
            line-height: 1.17;
            border-radius: 4px;
            background-color: #171717;
            color:white;
            border:none;
        }

        .btnCancel:hover{
            width: 103px;
            height: 32px;
            font-family: Helvetica;
            font-size: 12px;
            font-weight: bold;
            font-style: normal;
            font-stretch: normal;
            line-height: 1.17;
            border-radius: 4px;
            background-color: #303030;
            color:white;
            border:none;
        }


        .btnSave {
            width: 140px;
            height: 32px;
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

        .btnSave:hover {
            width: 140px;
            height: 32px;
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


        .btnVerify {
            width: 140px;
            height: 32px;
            font-family: Helvetica;
            font-size: 12px;
            font-weight: bold;
            font-style: normal;
            font-stretch: normal;
            line-height: 1.17;
            border-radius: 4px;
            background-color: #c43d32;
            color:white;
        }

        .btnOrange {
            width: 140px;
            height: 32px;
            font-family: Helvetica;
            font-size: 12px;
            font-weight: bold;
            font-style: normal;
            font-stretch: normal;
            line-height: 1.17;
            border-radius: 4px;
            background-color: #c43d32;
            color:white;
            border:none;
        }


        .btnOrange:hover {
            width: 140px;
            height: 32px;
            font-family: Helvetica;
            font-size: 12px;
            font-weight: bold;
            font-style: normal;
            font-stretch: normal;
            line-height: 1.17;
            border-radius: 4px;
            background-color: #8e2d25;
            color:white;
            border:none;
        }

        .toggle.android .toggle-group label {
            font-family: Arial, Helvetica, sans-serif;
        }


    </style>

    <body>
        <asp:UpdatePanel runat="server" ID="updateBIG" UpdateMode="Conditional">
            <ContentTemplate>
               
                <%-- ===============================================NAV LEFT===================================================================== --%>

        <table>
            <tr>
                <td>
                    <div id="myDiv" class="sidenavleft" onscroll="scollPos(this);">
                        <div class="innersidenavleft">
                            <div class="tab">
                              <h4 style="background-color:#e7e8ef; padding:8px ; font-size:16px ;" ><b>Worklist</b></h4>  
                            </div>
                            <div id="New" class="tabcontent" style="margin-top:-15px">
                                <div  style="margin-left:-20px">
                                    <div class="col-sm-6" style="margin-top:0%">
                                        <label style="font-family:Helvetica ; font-size:12px">From Date</label>
                                        <br />
                                        <asp:TextBox style="font-family:Helvetica" ID="txtDateFromNew" AutoPostBack="true" runat="server" CssClass="shadows-search" Width="80%" Height="24px" onmousedown="dateStartNew();"></asp:TextBox>
                                    </div>
                                    <div class="col-sm-6" style="margin-top:0%;margin-left:-15%">
                                        <label  style="font-family:Helvetica  ; font-size:12px">To Date</label>
                                        <br />
                                        <asp:TextBox  style="font-family:Helvetica" ID="txtToDateNew" AutoPostBack="true" runat="server" CssClass="shadows-search" Width="80%" Height="24px" onmousedown="dateEndNew();"></asp:TextBox>
                                    </div>
                                </div>
                                <div style="margin-top:15% ; margin-left:-20px">
                                    <div class="col-sm-12">
                                        <label   style="font-family:Helvetica  ; font-size:12px">Search</label>
                                        <br />
                                        <asp:TextBox style="font-family:Helvetica" placeholder="Doctor/Patient name, Adm. no, MR no" Font-Size="12px" ID="txtSearch" runat="server" CssClass="shadows-search" Width="80%" Height="24px" AutoPostBack="true" ></asp:TextBox>
                                        <asp:Button runat="server" ID="btnSearchWorklist" OnClick="btnSearchWorklist_OnCLick" CssClass="btnSave" Width="51px" Height="24px" Text="Search"/>
                                    </div>
                                </div>
                            </div>
                        </div>
                        
                        <asp:HiddenField ID="hdnScrollValue" runat="server"/>

                        <%-----------------------------------------------------------images section start-------------------------------------------------------------%>
                            <div runat="server" id="imageSection" style="margin-top:68%;" visible="false">
                                <div class="col-sm-12"  style="background-color:transparent;border-radius:6px;width:98%;padding-left:5px;padding-right:0px;height:100%">
                                    <div id="divNoData" runat="server" style="text-align:center; margin-top:15% ; margin-bottom:5%"  Visible="true">
                                        <asp:Image ID="imgNoData" Font-Names="Helvetica" Font-Bold="true" Font-Size="12px" runat="server" Text="image" ImageUrl="~/Images/Icon/ic_noData.svg" Width="50%"></asp:Image>
                                    </div>
                                    <div style="text-align:center">
                                        <asp:Label ID="lblOops" runat="server" Text="Data not found" Font-Size="16px" Font-Bold="true" Font-Names="Helvetica" Visible="true" ForeColor="#585A6F"></asp:Label>
                                    </div>
                                    <div style="text-align:center">
                                        <asp:Label ID="lblPlease" runat="server" Text="Please search another date or parameter" Font-Size="12px" Font-Names="Helvetica" Visible="true" ForeColor="#585A6F"></asp:Label>
                                    </div>
                                </div>
                            </div>
                        <%-----------------------------------------------------------images section end-----------------------------------------------------------%>


                        <div class="tabcontentprescription" style="margin-top:178px; margin-left:-12px ; width:108%">       
                            <asp:Button ID="btnPatient" CssClass="hidden" OnClick="btnPatientName_Click" runat="server" Text="Submit" ForeColor="White" Style="background: green; border-radius: 5px; width: 80px;" />
                                    <asp:GridView ID="gvw_worklist" runat="server" AutoGenerateColumns="False"  CssClass="table table-striped table-bordered table-condensed"  BorderColor="Black"
                                    HeaderStyle-CssClass="text-center" HeaderStyle-HorizontalAlign="Center"
                                    ShowHeaderWhenEmpty="True" DataKeyNames="AdmissionNo" ShowHeader="false"
                                    AllowSorting="True" style="border-bottom:none" Font-Names="Helvetica">
                                        <Columns>
                                            <asp:TemplateField ItemStyle-CssClass="collapsediv" HeaderText="" ItemStyle-Width="5%" HeaderStyle-ForeColor="#3C8DBC" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <div class="row" style="margin-bottom:-50px">
                                                        <div class="col-sm-8" style="margin-top:1%;padding-right:0px;width:180px" >
                                                            <a id="LnkPatientName" class="valueofprescription" href='<%# String.Format("javascript:expandpatientname(\"{0}\")", Container.DataItemIndex) %>' style="font-weight:bold;"><%# Eval("PatientName") %></a>
                                                            <asp:LinkButton class="valueofprescription" ID="btnPatientName" runat="server"  Text='<%# Bind("PatientName") %>' OnClick="btnPatientName_Click" Font-Bold="true" BackColor="Transparent" CssClass="hidden"></asp:LinkButton>
                                                            <asp:HiddenField ID="onContentOrganizationId" runat="server"  Value='<%# Bind("organizationId") %>'></asp:HiddenField>
                                                            <asp:HiddenField ID="onContentPatientId" runat="server"  Value='<%# Bind("patientId") %>'></asp:HiddenField>
                                                            <asp:HiddenField ID="onContentAdmissionid" runat="server"  Value='<%# Bind("admissionid") %>'></asp:HiddenField>
                                                            <asp:HiddenField ID="onContentEncounterId" runat="server"  Value='<%# Bind("encounterId") %>'></asp:HiddenField>
                                                        </div>
                                                        <div class="col-sm-4" style="margin-top:1%; margin-left:25px ; padding-right:0px">
                                                            <asp:Label  class="valueofprescriptiontime" ID="Label1" runat="server"  Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem,"AdmissionDate")).ToString("dd MMM yyyy")%>'></asp:Label>
                                                            <asp:Label  class="valueofprescriptiontime" ID="Label2" runat="server"  Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem,"AdmissionDate")).ToString("HH:mm")%>'></asp:Label>
                                                        </div>
                                                    </div>
                                                    
                                                    <div class="row" style="margin-bottom:-50px;margin-top:30px">
                                                        <br />
                                                        <div class="col-sm-3" style="margin-top:1%; padding-right:0px">
                                                            <asp:Label class="valueofprescription" runat="server">MR No</asp:Label>
                                                            <br />
                                                            <asp:Label class="valueofprescription" runat="server">Adm No</asp:Label>
                                                            <br />
                                                            <asp:Label class="valueofprescription" runat="server">Sex</asp:Label>
                                                            <br />
                                                            <asp:Label class="valueofprescription" runat="server">DOB</asp:Label>
                                                            <br />
                                                            <asp:Label class="valueofprescription" runat="server">Doctor</asp:Label>
                                                            <br />
                                                            <br />
                                                            <asp:Label class="valueofprescription" runat="server">Payer</asp:Label>
                                                        </div>
                                                        <div class="col-sm-1" style="margin-top:1%;padding:0px">
                                                            <asp:Label class="valueofprescription" runat="server">:</asp:Label>
                                                            <br />
                                                            <asp:Label class="valueofprescription" runat="server">:</asp:Label>
                                                            <br />
                                                            <asp:Label class="valueofprescription" runat="server">:</asp:Label>
                                                            <br />
                                                            <asp:Label class="valueofprescription" runat="server">:</asp:Label>
                                                            <br />
                                                            <asp:Label class="valueofprescription" runat="server">:</asp:Label>
                                                            <br />
                                                            <br />
                                                            <asp:Label class="valueofprescription" runat="server">:</asp:Label>
                                                        </div>
                                                        <div class="col-sm-7" style="margin-top:1% ; padding-left:0px;padding-right:0px">
                                                            <asp:Label  class="valueofprescription" ID="lblLocalMrNo" runat="server"  Text='<%# Bind("LocalMrNo") %>' > </asp:Label>
                                                            <br />
                                                            <asp:Label class="valueofprescription" ID="LinkButton2" runat="server"  Text='<%# Bind("AdmissionNo") %>' ></asp:Label>
                                                            <br />
                                                            <asp:Label  class="valueofprescription" ID="LinkButton3" runat="server"  Text='<%# Bind("Gender") %>' ></asp:Label>
                                                            <br />
                                                            <asp:Label  class="valueofprescription" ID="LinkButton4" runat="server"  Text='<%# String.Concat(Convert.ToDateTime(DataBinder.Eval(Container.DataItem,"BirthDate")).ToString("dd MMM yyyy")," (",Eval("Age"),")") %>'></asp:Label>
                                                            <br />
                                                            <asp:Label class="valueofprescription" ID="lblDoctorName" runat="server"  Text='<%# Bind("DoctorName") %>' ></asp:Label>
                                                            <br />
                                                            <asp:Label  class="valueofprescription" ID="LinkButton7" runat="server"  Text='<%# Bind("PayerName") %>' ></asp:Label>
                                                        </div>                
                                                    </div>
                                                    <br />
                                                <%--<asp:HiddenField ID="hffrequentdrugs_id" Value='<%# Bind("salesItemId") %>' runat="server" />
                                                <asp:HiddenField ID="hfuom_id" Value='<%# Bind("uom_id") %>' runat="server" />
                                                <asp:HiddenField ID="hfuom_code" Value='<%# Bind("uom_code") %>' runat="server" />
                                                --%><%--<asp:Label id="itemlist" runat="server" Text='<%# Bind("item_list") %>' Font-Size="9px" Font-Names="Roboto"></asp:Label>--%>
                                            </ItemTemplate>
                                                </asp:TemplateField>
                                            <%--<asp:BoundField HeaderText="activeIngredientsName" ItemStyle-Width="15%" ItemStyle-HorizontalAlign="Left" DataField="activeIngredientsName"  SortExpression="activeIngredientsName"></asp:BoundField>
                                            <asp:BoundField HeaderText="totalQuantity" ItemStyle-Width="5%" ItemStyle-HorizontalAlign="Left" DataField="totalQuantity"  SortExpression="totalQuantity"></asp:BoundField>--%>
                                        </Columns>
                                    </asp:GridView>
                        </div>
                        
                    </div>
                    
                </td>
            </tr>
        </table>
                <%-- ===============================================NAV LEFT END===================================================================== --%>               


                
                <%--=========================================================HEADER PATIENT START===================================================--%>


                <div runat="server" id="headerPatientsection" class="row"  style="margin-left:24% ; margin-top:1% ; margin-right:-12%; position:absolute; width:100%" visible="false">
                    <div class="col-sm-12" style="background-color:white;height: 70px;padding-top: 15px;padding-left: 30px;margin-top:-15px;">
                        <div>
                            <asp:Label ID="lblPatientName" runat="server" CssClass="textHeader"></asp:Label>
                            <asp:Label ID="Label4" runat="server" Text="|"  CssClass="textHeader"></asp:Label>
                            <asp:Label ID="lblMrNo" runat="server" CssClass="textHeader"></asp:Label>
                        </div>
                        <div>
                            <asp:Label ID="lblDoctorName" runat="server"></asp:Label>
                        </div>
                    </div>
                </div>
                <%--===================================================HEADER PATIENT END===================================================--%>



                <div runat="server" id="divFrame" style="margin-left:26%;margin-top:7%; width:73%">
                    <iframe name="myIframe" id="myIframe" runat=server style="width:100%; height:515px; border:none; overflow-y:scroll"></iframe>
                </div>
                




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


            function AutoExpand(txtbox)
            {
                txtbox.style.height = "1px";
                txtbox.style.height = (25 + txtbox.scrollHeight) + "px";
            }

            function alo()
            {
                var x = $("[id$='hdnScrollValue']").val();
                document.getElementById("myDiv").scrollTop = x;
            }

            function scollPos(ax)
            {
                var test = ax.scrollTop;
                $("[id$='hdnScrollValue']").val(test);
            }

            function expandpatientname(input)
            {
                document.getElementById("MainContent_txtencounterid").value = input;
                __doPostBack('<%= btnPatient.UniqueID %>', '');             
            }

            function expandlinkbutton() {
                var input = document.getElementById("MainContent_txtencounterid").value;
                var x = document.getElementById("chevron" + input);

                if (input != "")
                {
                    var x = document.getElementById("MainContent_gvw_worklist");
                    var teer = x.getElementsByClassName('collapsediv')[input];
                    teer.style.backgroundColor = "#f1f7b8";
                }
            }

        </script>

    </body>
</asp:Content>