<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Banner.aspx.cs" Inherits="LoanPricerWebBased.Banner" %>

<!DOCTYPE html>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Secure Banner</title>

    <link href="~/style/Main.css" rel="stylesheet" type="text/css" />
    <link href="~/style/style-metronic.css" rel="stylesheet" type="text/css" />
    <link href="~/style/style.css" rel="stylesheet" type="text/css" />
    <link href="~/style/style-responsive.css" rel="stylesheet" type="text/css" />
    <link href="~/style/pages/login.css" rel="stylesheet" type="text/css" />
    <style>
        /* .newclass {
            background-color: #FFFFFF;
            margin: auto;
            position: relative;
            width: 1100px;
        }

        .newbody {
            background-color: #007ACC;
            color: darkblue;
            font-family: Calibri,Verdana,Sans-Serif;
        }*/
        .content.bannerPage {
            margin: 50px auto 0 auto;
        }

        .rbDecorated {
            padding-left: 0px !important; /* this could be increased if you want to have more space betwwen the left button edge and the text  */
            text-align: center !important;
            padding-right:0px !important;
        }

        .bannerPage {
            margin: 20px 0 0 0;
            font-size: 14px;
            line-height: 20px;
        }
    </style>
</head>
<body style="background-color: #FFFFFF !important;" class="newbody login">
    <div align="center">
        <form id="form123" runat="server">
            <telerik:RadScriptManager ID="scrMgr" runat="server"></telerik:RadScriptManager>
            <%--  <div id="tblLogin">
            <div>
             
            </div>
            <br />
            <div>
                <asp:Label ID="lblWelcomeMsg" runat="server" Text="Loan Pricer System" Font-Size="Large"></asp:Label>
            </div>
        </div>--%>
            <div id="newbanner" class="content bannerPage">
                <div align="center">
                    <image src="Images/Logo.png" height="150" width="200"></image>

                </div>
                <p>
                    This is a restricted system and you should only proceed if you have approval to use the system from RealEdge Associates Ltd.  
            You must press OK to continue or Cancel to log out of the system.
                </p>
                <br />
                <br />
                <div>
                    <%-- <asp:Button ID="btnOK" runat="server" Text="OK" PostBackUrl="~/Login.aspx" />--%>
                    <telerik:RadButton ID="btnOK" runat="server" Text="OK" PostBackUrl="~/Login.aspx" Skin="Web20" OnClick="btnOK_Click" Width="80px"></telerik:RadButton>

                    <%-- <input type="button" value="Cancel" onClick="window.location.replace('https://www.google.com');" />--%>
                    <telerik:RadButton ID="btnCancel" runat="server" Text="Cancel"  OnClick="btnCancel_Click" Skin="Web20" Width="80px" ></telerik:RadButton>
                </div>
            </div>
        </form>
    </div>
</body>
</html>
