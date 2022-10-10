<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="Form_Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />

    <meta content="width=device-width, initial-scale=1, maximum-scale=1, user-scalable=no" name="viewport" />

    <!-- Bootstrap 3.3.6 -->
    <link rel="stylesheet" href="~/Content/bootstrap/css/bootstrap.css" />
    <link rel="stylesheet" href="~/Content/dist/css/AdminLTE.min.css" />

    <link rel="stylesheet" href="~/Content/plugins/iCheck/square/blue.css" />
    <link rel="stylesheet" href="~/Content/Site.css" />
    <link rel="stylesheet" href="~/Content/bootstrap-select/css/bootstrap-select.css" />

    <link href="../favicon.ico" rel="shortcut icon" type="image/x-icon" />

    <style>
        body {
            background-image: url("<%= Page.ResolveClientUrl("~/Images/Background/bg-offline.svg") %>") !important;
            background-size: cover;
            background-repeat: no-repeat;
        }

        /*body {
            background-color: grey !important;
            background-size:100%;
            background-repeat: no-repeat;
            background-position:center;
        }*/

        .btn-primary {
            background-color: #1A2268;
            transition: all 0.5s;
        }

            .btn-primary:hover {
                background-color: #4350B1;
                transition: all 0.5s;
            }

        .btn-white {
            color: #000;
            background-color: #fff;
            border-color: #cdced9;
            padding: 3px;
            padding-left: 4px;
            font-size: 12px;
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
            color: white;
            border: none;
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
                color: white;
                border: none;
            }
    </style>
</head>
<body class="hold-transition">
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
            <Scripts>
                <asp:ScriptReference Path="~/Content/plugins/jQuery/jQuery-2.2.0.min.js" />
                <asp:ScriptReference Path="~/Content/bootstrap/js/bootstrap.min.js" />
                <asp:ScriptReference Path="~/Content/plugins/iCheck/icheck.min.js" />
                <asp:ScriptReference Path="~/Content/bootstrap-select/js/bootstrap-select.js" />
            </Scripts>
        </asp:ScriptManager>

        <asp:UpdateProgress ID="uProgLoginX" runat="server" AssociatedUpdatePanelID="upError">
            <ProgressTemplate>
                <div class="modal-backdrop" style="background-color: white; opacity: 0.6; text-align: center">
                </div>
                <div style="margin-top: 80px; margin-left: -100px; text-align: center; position: fixed; z-index: 2000; left: 50%;">
                    <img alt="" height="200px" width="200px" style="background-color: transparent; vertical-align: middle" class="login-box-body" src="<%= Page.ResolveClientUrl("~/Images/Background/loading-beat.gif") %>" />
                </div>
            </ProgressTemplate>
        </asp:UpdateProgress>

        <div class="login-box" style="width: 400px;">
            <asp:UpdatePanel ID="upError" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <div class="login-box-body" style="background-color: #c6c6c6; width: 100%; border: 1px; border-radius: 5px; box-shadow: 0px 1px 2px rgba(26, 34, 105, 0.5); padding: 30px;">
                        <div class="login-logo" style="color: Blue; font-weight: bold; height: 60px;">
                            <img src="<%= Page.ResolveClientUrl("~/Images/Icon/ic_Logo_Offline.svg") %>" width="80%" />
                            <br />
                        </div>

                        <div class="form-group row">
                            <div class="col-md-12 inputGroupContainer" style="margin-top: 10px;">
                                <div class="input-group">
                                    <span class="input-group-addon"><i class="glyphicon glyphicon-user"></i></span>
                                    <asp:TextBox ID="txtUsername" Style="width: 100%; max-width: 100%;" runat="server" CssClass="form-control" placeholder="Username"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-12 inputGroupContainer" style="margin-top: 10px;">
                                <div class="input-group">
                                    <span class="input-group-addon"><i class="glyphicon glyphicon-lock"></i></span>
                                    <asp:TextBox ID="txtPassword" TextMode="Password" Style="width: 100%; max-width: 100%;" runat="server" CssClass="form-control" placeholder="Password"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-12 inputGroupContainer" style="text-align: right;">
                                <p style="text-align: right; color: red; display: none" id="pError" runat="server"></p>
                                <a href="#modalForgotPass" data-toggle="modal" style="text-align: right; color: green; font-size: 12px; cursor:not-allowed;">Forgot Password?</a>
                            </div>
                            <div class="col-md-12 inputGroupContainer" style="margin-top: 20px;">
                                <asp:Button ID="btnSignIn" OnClientClick="return CheckField();" runat="server" CssClass="btn btn-github" Style="width: 100%; max-width: 100%; height: 42px; font-size: 20px; border-radius: 4px;" Text="Log In" Font-Bold="true" OnClick="btnSignIn_Click" />
                            </div>
                        </div>

                    </div>

                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="btnSignIn" EventName="Click" />
                </Triggers>
            </asp:UpdatePanel>

            <div style="text-align: center; letter-spacing: 8.8px;">
                <br />
                <asp:Label runat="server" Text="OFFLINE MODE" ForeColor="White"></asp:Label>
            </div>
        </div>



        <%-- ============================================= MODAL CHOOSE STORE ============================================== --%>
        <div class="modal fade" id="modalChooseStore" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
            <asp:Panel ID="panel1" runat="server" DefaultButton="btnContinue">
                <div class="modal-dialog" style="width: 30%">
                    <div class="modal-content" style="height: 100%; border-radius: 5px">

                        <asp:UpdatePanel runat="server">
                            <ContentTemplate>
                                <div class="modal-header" style="height: 40px; padding-top: 10px; padding-bottom: 5px">
                                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                                    <h5 class="modal-title">
                                        <asp:Label ID="Label1" Style="font-family: Helvetica; font-weight: bold; font-size: 14px" runat="server" Text="Select your store"></asp:Label></h5>
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>

                        <div class="modal-body">
                            <div style="width: 100%;">
                                <asp:UpdatePanel runat="server">
                                    <ContentTemplate>
                                        <div>
                                            <asp:DropDownList Font-Size="14px" Width="100%" Height="30px" Font-Names="Helvetica" ID="dropdownStore" runat="server" CssClass="selecpicker greyItem" data-live-search="true" data-size="7" data-width="100%" data-height="24px" data-dropup-auto="false" Style="font-size: 14px; height: 30px; padding: 1px; padding-left: 2px; border-radius: 2px; background-color: white" data-container="body" data-style="btn-white"></asp:DropDownList>
                                        </div>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                        </div>
                        <div class="modal-footer" style="width: 100%">
                            <asp:UpdatePanel runat="server">
                                <ContentTemplate>
                                    <asp:Button runat="server" Text="Continue" CssClass="btnSave" class="box" Width="30%" ID="btnContinue" OnClick="btnContinue_onClick" />
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                </div>
            </asp:Panel>
        </div>
        <%-- ============================================= END OF MODAL CHOOSE STORE ============================================== --%>
    </form>

    <script type="text/javascript">
        history.pushState(null, null, document.title);
        window.addEventListener('popstate', function () {
            history.pushState(null, null, document.title);
        });
        function CheckField() {
            var UserName = $("[id$='txtUsername']").val();
            var PassWord = $("[id$='txtPassword']").val();

            $("[id$='txtUsername']").removeAttr("style");
            $("[id$='txtPassword']").removeAttr("style");

            if (UserName.length <= 0 && PassWord.length <= 0) {
                $("[id$='txtUsername']").attr("style", "display:block; border-color:red;");
                $("[id$='txtPassword']").attr("style", "display:block; border-color:red;");
                $("[id$='pError']").removeAttr("style");
                $("[id$='txtUsername']").focus();
                $("[id$='pError']").attr("style", "display:block; color:red;");
                document.getElementById("pError").innerHTML = "Masukkan Username dan Password !";

                return false;
            }
            else if (UserName.length <= 0 && PassWord.length > 0) {
                $("[id$='txtUsername']").attr("style", "display:block; border-color:red;");
                $("[id$='txtUsername']").focus();
                $("[id$='pError']").removeAttr("style");
                $("[id$='pError']").attr("style", "display:block; color:red;");
                document.getElementById("pError").innerHTML = "Masukkan Username !";

                return false;
            }
            else if (UserName.length > 0 && PassWord.length <= 0) {
                $("[id$='txtPassword']").attr("style", "display:block; border-color:red;");
                $("[id$='txtPassword']").focus();
                $("[id$='pError']").removeAttr("style");
                $("[id$='pError']").attr("style", "display:block; color:red;");
                document.getElementById("pError").innerHTML = "Masukkan Password !";

                return false;
            }
            else {
                return true;
            }
        }

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
        });

    </script>


</body>
</html>
