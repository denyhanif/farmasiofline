<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FormChangePassword.aspx.cs" Inherits="Form_FormViewer_FormChangePassword" %>

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
    <!-- Mask -->
    <link rel="stylesheet" href="~/Content/plugins/jasny-bootstrap/css/jasny-bootstrap.min.css" />
    <!-- Theme style -->
    <link rel="stylesheet" href="~/Content/dist/css/AdminLTE.css" />
    <link rel="stylesheet" href="~/Content/dist/css/skins/skin-blue-light.css" />
    <link rel="stylesheet" href="~/Content/Site.css" />
    <link rel="stylesheet" href="~/Content/EMR-PharmacyOffline.css" />
    <!-- Select Bootstrap -->
    <link rel="stylesheet" href="~/Content/bootstrap-select/css/bootstrap-select.css" />

    <link href="~/favicon.ico" rel="shortcut icon" type="image/x-icon" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />

    <style type="text/css">
        
    </style>
</head>

<body style="padding-top:0px; padding-bottom:0px;">
    <form id="form1" runat="server">

        <asp:ScriptManager ID="ScriptManager1" runat="server">
            <Scripts>
                <asp:ScriptReference Path="~/Content/plugins/jQuery/jQuery-2.2.0.min.js" />
                <asp:ScriptReference Path="~/Content/bootstrap/js/bootstrap.min.js" />
                <asp:ScriptReference Path="~/Content/plugins/jasny-bootstrap/js/jasny-bootstrap.min.js" />
                <asp:ScriptReference Path="~/Content/plugins/datepicker/bootstrap-datepicker.js" />
                <asp:ScriptReference Path="~/Content/dist/js/app.min.js" />
                <asp:ScriptReference Path="~/Content/bootstrap-select/js/bootstrap-select.js" />
            </Scripts>
        </asp:ScriptManager>

        <%--YOUR CODE HTML HERE--%>

        <asp:UpdatePanel ID="UpdatePanelChangePass" runat="server">
            <ContentTemplate>

        <asp:HiddenField ID="HF_Username" runat="server" />

        <div class="row">
            <div class="col-sm-4">
            </div>

            <div class="col-sm-4 Contentutama">
                <div class="row borderTitle" style="padding-top: 10px; padding-bottom: 10px; display:none;">
                    <div class="col-sm-8 TeksHeader" style="padding-top: 5px;"><b> <label id="lblbhs_changepass" style="font-size:16px;">Change Password</label> </b> </div>
                    <div class="col-sm-4 text-right"> 
                        <asp:UpdateProgress ID="PassuProgSAVE" runat="server" AssociatedUpdatePanelID="UpdatePanelSAVE">
                            <ProgressTemplate>
                            <img alt="" style="background-color: transparent; height: 20px;" src="<%= Page.ResolveClientUrl("~/Images/Background/small-loader.gif") %>" />
                            </ProgressTemplate>
                        </asp:UpdateProgress>

                    </div>
                </div>

                <br />

                <div class="form-group">
                    <label id="lblbhs_recentpass">Recent password</label>
                        <asp:TextBox ID="Pass_TextOldPass" runat="server" CssClass="MaxWidthTextbox form-control" style="height:34px;" TextMode="Password" placeholder="Type here..." AutoCompleteType="Disabled"></asp:TextBox>
                        <a href="#" style="text-decoration: none; color: #171717; right: 25px; margin-top: -27px; position:absolute;" onclick="Toggle('Pass_TextOldPass','.mata1')"><i class="fa fa-eye-slash mata1"></i></a>
                </div>
                <div class="form-group">
                    <label id="lblbhs_newpass">New password</label>
                        <asp:TextBox ID="Pass_TextNewPass" runat="server" CssClass="MaxWidthTextbox form-control" style="height:34px;" TextMode="Password" placeholder="Type here..." AutoCompleteType="Disabled"></asp:TextBox>
                        <a href="#" style="text-decoration: none; color: #171717; right: 25px; margin-top: -27px; position:absolute;" onclick="Toggle('Pass_TextNewPass','.mata2')"><i class="fa fa-eye-slash mata2"></i></a>
                </div>
                <div class="form-group">
                    <label id="lblbhs_confirmnewpass">Confirm new password</label>
                        <asp:TextBox ID="Pass_TextNewPass_confirm" runat="server" CssClass="MaxWidthTextbox form-control" style="height:34px;" TextMode="Password" placeholder="Type here..." AutoCompleteType="Disabled"></asp:TextBox>
                        <a href="#" style="text-decoration: none; color: #171717; right: 25px; margin-top: -27px; position:absolute;" onclick="Toggle('Pass_TextNewPass_confirm','.mata3')"><i class="fa fa-eye-slash mata3"></i></a>
                </div>

                <table border="0" style="width:100%">
                    <tr>
                        <td> 
                            <asp:UpdatePanel ID="UpdatePanelCekPass" runat="server"> <ContentTemplate>
                            <b> <p style="color: red; display: none" id="p_Add" runat="server"> </p> </b> 
                            </ContentTemplate> </asp:UpdatePanel>
                        </td>
                        <td style="width:30%; text-align:right;">             
                            <asp:UpdatePanel ID="UpdatePanelSAVE" runat="server"> <ContentTemplate>
                            <asp:Button ID="Pass_ButtonSavePass" runat="server" Text="Change Password" CssClass="btn btn-lightGreen" OnClientClick="return FormCheck()" OnClick="Pass_ButtonSavePass_Click"></asp:Button>
                            </ContentTemplate> </asp:UpdatePanel>
                         </td>
                    </tr>
                </table>

                <br />
            </div>

            <div class="col-sm-4">
            </div>
        
        </div>

            </ContentTemplate>
        </asp:UpdatePanel>

        <!--###########################################################################################################################################-->
        <!------------------------------------------------------------------ The Modal ------------------------------------------------------------------>

        <!-- ##### Modal Change Password ##### -->
        <div class="modal fade" id="modalAfterSave" role="dialog" tabindex="-1" aria-labelledby="myModalLabel" aria-hidden="true" style="padding-top: 50px;" data-keyboard="false">
       
            <asp:UpdatePanel ID="UpdatePanelAfterChangepass" runat="server" UpdateMode="Conditional"> <ContentTemplate>

            <div class="modal-dialog" style="width: 500px">
                <div class="modal-content">

                    <!-- Modal Header -->
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal">×</button> 
                        <h4 class="modal-title">
                            <label style="color:#4e9c36;"> <i class="fa fa-check"></i> Change Password Success  </label>
                        </h4>
                    </div>

                    <!-- Modal body -->
                    <div class="modal-body">
                        <div class="text-center">
                            Your Password Has Been Changed <br /> <b> Klik OK to Relogin. </b>
                            <br /><br />
                            <asp:Button ID="ButtonRelogin" runat="server" Text="OK" class="btn btn-success" Style="width:90px;" OnClick="ButtonRelogin_Click"/> 
                        </div>
                    </div>
                </div>
            </div>
            </ContentTemplate> </asp:UpdatePanel>
        </div>
        <!-- End of Modal Change Password -->

        <%-- ################# --%>


        <script type="text/javascript">

            //fungsi toogle icon eye pada form password
            function Toggle(obj, cls) {
                var temp = document.getElementById(obj);
                if (temp.type === "password") {
                    temp.type = "text";
                    $(cls).removeClass('fa fa-eye-slash').addClass('fa fa-eye');
                }
                else {
                    temp.type = "password";
                    $(cls).removeClass('fa fa-eye').addClass('fa fa-eye-slash');
                }
            }

            //fungsi  untuk validasi input text kosong, lalu menampilkan notifikasi text berwarna merah
            function FormCheck()
            {
                if ($("[id$='Pass_TextOldPass']").val().length == 0) {
                    $("[id$='Pass_TextOldPass']").focus();
                    $("[id$='p_Add']").removeAttr("style");
                    $("[id$='p_Add']").attr("style", "display:block; color:red;");
                    document.getElementById('<%= p_Add.ClientID %>').innerHTML = "Old Password cannot be empty!";

                    return false;
                }
                else if ($("[id$='Pass_TextNewPass']").val().length == 0) {
                    $("[id$='Pass_TextNewPass']").focus();
                    $("[id$='p_Add']").removeAttr("style");
                    $("[id$='p_Add']").attr("style", "display:block; color:red;");
                    document.getElementById('<%= p_Add.ClientID %>').innerHTML = "New Password cannot be empty!";

                    return false;
                }
                else if ($("[id$='Pass_TextNewPass_confirm']").val().length == 0) {
                    $("[id$='Pass_TextNewPass_confirm']").focus();
                    $("[id$='p_Add']").removeAttr("style");
                    $("[id$='p_Add']").attr("style", "display:block; color:red;");
                    document.getElementById('<%= p_Add.ClientID %>').innerHTML = "Confirm New Password cannot be empty!";

                    return false;
                }
                else if ($("[id$='Pass_TextNewPass']").val() != $("[id$='Pass_TextNewPass_confirm']").val()) {
                    $("[id$='Pass_TextNewPass_confirm']").focus();
                    $("[id$='p_Add']").removeAttr("style");
                    $("[id$='p_Add']").attr("style", "display:block; color:red;");
                    document.getElementById('<%= p_Add.ClientID %>').innerHTML = "Confirm New Password must be same with New Password!";

                    return false;
                }

                return validatePass($("[id$='Pass_TextNewPass']").val());
               
            }

            function validatePass(objval) {
                var value = objval;
                var regex = /^(?=.{8,})(?=.*[a-zA-Z])(?=.*[0-9]).*$/; //(?=.*[@#$%^&+=])
                var bolvalue = regex.test(value);
                if (bolvalue == true) {
                    $("[id$='p_Add']").removeAttr("style");
                    document.getElementById('<%= p_Add.ClientID %>').innerHTML = "";
                    return true;
                }
                else {
                    $("[id$='Pass_TextNewPass']").focus();
                    $("[id$='p_Add']").removeAttr("style");
                    $("[id$='p_Add']").attr("style", "display:block; color:red;");
                    document.getElementById('<%= p_Add.ClientID %>').innerHTML = "The password must has minimum 8 characters at least 1 Alphabet and 1 Number!"; //and 1 Special Character
                    return false;
                }
            }

            //fungsi event klik pada area diluar modal
            $(document).ready(function () { 
                $('#modalAfterSave').on('hidden.bs.modal', function (e) {
                     document.getElementById('<%= ButtonRelogin.ClientID %>').click();
                 });
            });

            $(document).ready(function () {

                var prm = Sys.WebForms.PageRequestManager.getInstance();
                if (prm != null) {
                    prm.add_endRequest(function (sender, e) {
                        if (sender._postBackSettings.panelsToUpdate != null) {

                        }
                    });
                };
            });

        </script>

    </form>
</body>

</html>



