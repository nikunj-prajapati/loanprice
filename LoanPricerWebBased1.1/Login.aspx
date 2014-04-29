<%@ Page Language="C#" AutoEventWireup="True" CodeBehind="Login.aspx.cs" Inherits="LoanPricerWebBased.Login" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>


    <%--<link href="~/style/Main.css" rel="stylesheet" type="text/css" />--%>
    <link href="~/style/font-awesome.min.css" rel="stylesheet" type="text/css" />
    <link href="~/style/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <link href="~/style/uniform.default.css" rel="stylesheet" type="text/css" />
    <link href="~/style/style-metronic.css" rel="stylesheet" type="text/css" />
    <link href="~/style/style.css" rel="stylesheet" type="text/css" />
    <link href="~/style/style-responsive.css" rel="stylesheet" type="text/css" />
    <link href="~/style/pages/login.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript">
        var imgUrl = null;
        function alertCallBackFn(arg) {

        }
    </script>
    <style>
        .text {
            background: none repeat scroll 0 0 #FFFFFF;
            border-color: #6788BE;
            color: #000000;
            font: 12px "Segoe UI",Arial,Helvetica,sans-serif;
            border: 1px solid #6788BE;
        }

        .rbDecorated {
            padding-left: 0px !important; /* this could be increased if you want to have more space betwwen the left button edge and the text  */
            text-align: center !important;
            padding-right: 0px !important;
        }
    </style>
</head>
<body class="login" style="background-color: #FFFFFF !important;text-align:center;">
    <div class="logo">
    </div>
    <div class="content" >
        <form id="form1" runat="server" class="login-form">
            <telerik:RadScriptManager ID="RadScriptManager1" runat="server"></telerik:RadScriptManager>
            <telerik:RadWindowManager ID="RadWindowManager1" runat="server" EnableShadow="true" Skin="Web20">
            </telerik:RadWindowManager>
            <telerik:RadAjaxPanel ID="pnl1" runat="server" LoadingPanelID="RadAjaxLoadingPanel1">
                <asp:Panel ID="pnlDefaultButton" runat="server" DefaultButton="btnLogin">
                    <div align="center">
                        <image src="Images/Logo.png" height="150" width="200"></image>

                    </div>
                    <div style="margin-top: 10px">
                        <button class="close" data-close="alert"></button>
                        <asp:Label runat="server" ID="lblErrorMesage" ForeColor="Red"></asp:Label>
                    </div>
                    <div class="form-group">
                        <!--ie8, ie9 does not support html5 placeholder, so we just show field title for that-->
                        <label class="control-label visible-ie8 visible-ie9">Username</label>
                        <div>
                            <i class="fa fa-user"></i>
                            <asp:TextBox ID="txtName" runat="server" EmptyMessage="Username" Width="250px" Height="30px" CssClass="text"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ValidationGroup="vgSubmit" ControlToValidate="txtName"
                                ErrorMessage="Username is required"></asp:RequiredFieldValidator>
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="control-label visible-ie8 visible-ie9">Password</label>
                        <div>
                            <i class="fa fa-lock"></i>
                            <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" Width="250px" Height="30px" CssClass="text" Style="margin-right:5px"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtPassword" ValidationGroup="vgSubmit"
                                ErrorMessage="Password is required"></asp:RequiredFieldValidator>
                        </div>
                    </div>
                    <div class="form-actions" align="center">
                        <%--<label class="checkbox">
                    <input type="checkbox" name="remember" value="1" />
                    Remember me
                </label>--%>

                        <telerik:RadButton ID="btnLogin" runat="server" UseSubmitBehavior="true" Text="Submit" OnClick="btnLogin_Click" ValidationGroup="vgSubmit" Skin="Web20" Width="100px"></telerik:RadButton>
                        <%--<asp:Button ID="btnlogin" runat="server" OnClick="btnLogin_Click" Text="Login" />--%>
                        <br />
                        <br />
                        <telerik:RadButton ID="btnResetPassword" runat="server" Text="Reset Password" OnClick="btnResetPassword_Click" Skin="Web20" Width="100px"></telerik:RadButton>
                        <%--<input id="btnResetPassword" runat="server" value="Reset Password" type="button" onserverclick="btnResetPassword_Click" />--%>
                    </div>
                    <%--  <div class="center" style="margin-left:100px">
                        <h5 class="form-title">www.reasso.com</h5>
                    </div>--%>
                    <div class="center" style="color: lightgray;">
                        <h5 class="form-title">www.reasso.com</h5>
                    </div>

                </asp:Panel>
            </telerik:RadAjaxPanel>
            <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server">
            </telerik:RadAjaxLoadingPanel>
        </form>

    </div>

</body>
</html>
