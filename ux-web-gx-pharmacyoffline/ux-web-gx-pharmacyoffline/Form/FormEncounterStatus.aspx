<%@ Page Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="FormEncounterStatus.aspx.cs" Inherits="Form_FormEncounterStatus" %>
<%@ MasterType VirtualPath="~/Site.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
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
                <%--===================================================CONTENT START===================================================--%>
                <div runat="server" id="divFrame" style="margin-top:8%">
                    <iframe name="myIframe" id="myIframe" runat=server style="width:100%; height: calc(100vh - 47px); border:none; margin-top:-8%; overflow-y:auto"></iframe>
                </div>                
                <%--====================================================CONTENT END===================================================--%>



                </ContentTemplate>
        </asp:UpdatePanel>




        <script type="text/javascript">

            function CheckNumeric()
            {
                return event.keyCode >= 48 && event.keyCode <= 57 || event.keyCode == 46;
            }

            function validateFloatKeyPress(el)
            {
                var v = parseFloat(el.value);
                var strv = el.value.split('.');
                var strval = strv[1];
                // alert(strval.length);
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
                    //el.value = (isNaN(v)) ? '' : v.toFixed(2);
                }
            }


            function AutoExpand(txtbox)
            {
                txtbox.style.height = "1px";
                txtbox.style.height = (25 + txtbox.scrollHeight) + "px";
            }

            function validateFloatKeyPress(el) {
                var v = parseFloat(el.value);
                var strv = el.value.split('.');
                var strval = strv[1];
                // alert(strval.length);
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
                    //el.value = (isNaN(v)) ? '' : v.toFixed(2);
                }
            }
            </script>
    </body>
</asp:Content>