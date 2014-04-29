<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" Theme="Default" EnableEventValidation="true" CodeBehind="Administration.aspx.cs" Inherits="LoanPricerWebBased.Administration" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="Scripts/jquery-1.6.4.min.js"></script>
    <link rel="Stylesheet" type="text/css" href="CSS/uploadify.css" />

    <script type="text/javascript" src="scripts/jquery.uploadify.js"></script>
    <style type="text/css">
        RadWindow .rwIcon {
            display: none !important;
        }

        .filterDiv {
            margin: 20px 0px 10px 0px;
        }

        .col-md-offset-31 {
            margin-left: 21%;
        }

        .col-md-offset-31-setting {
            margin-left: 24%;
        }

        .newclass-users {
            width: 25%;
            padding-left: 15px;
            padding-top: 10px;
        }

        .newclass-users-btn {
            padding-left: 225px;
            padding-top: 10px;
            margin-bottom: 10px;
            height: 60px;
        }

        .newclass-users-grdvw {
            height: auto;
        }

        .newclass-email {
            width: 40%;
            padding-left: 15px;
            padding-top: 10px;
        }

        .newclass-email-btn {
            padding-left: 240px;
            padding-top: 10px;
            margin-bottom: 10px;
        }
    </style>
    <script type="text/javascript">
        var imgUrl = null;
        function RadConfirm(sender, args) {
            var callBackFunction = Function.createDelegate(sender, function (shouldSubmit) {
                if (shouldSubmit) {
                    this.click();
                }
            });

            var text = "Are you sure you want to delete all records?";
            radconfirm(text, callBackFunction, 300, 160, null, "realedge associates");
            args.set_cancel(true);
        }


        $(document).ready(function () {
            $("#<%=fuFile.ClientID %>").fileUpload({
                'uploader': 'scripts/uploader.swf',
                'cancelImg': 'images/cancel.png',
                'buttonText': 'Browse Files',
                'script': 'Upload.ashx',
                'folder': 'uploads',
                'fileDesc': 'CSV Files',
                'fileExt': '*.csv;*.xls;*.xlsx;',
                'multi': false,
                'auto': true,
                onComplete: function (a, b, c, d, e) {
                    $("#fuUploaded").val("uploaded");
                }
            });
        });

            function FileExist() {
                if ($("#fuUploaded").val() == "uploaded")
                    return true;
                else {
                    alert("Please select a file to Import");
                    return false;
                }
            }
            var imgUrl = null;
            function alertCallBackFn(arg) {

            }
    </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" runat="server">
    <table width="100%" style="margin-left: 35px">
        <tr>
            <td>
                <telerik:RadFormDecorator ID="RadFormDecorator1" runat="server" Skin="Web20" DecoratedControls="Buttons" />
                <telerik:RadTabStrip ID="RadTabStrip1" runat="server" Skin="Web20" MultiPageID="RadMultiPage1" Width="96%"
                    SelectedIndex="0" Align="Left" BorderStyle="Solid" BorderWidth="1px">
                    <Tabs>

                        <telerik:RadTab Text="Manage Users" NavigateUrl="administration.aspx?page=userdetails" runat="server" Width="150px">
                        </telerik:RadTab>
                        <telerik:RadTab Text="System Settings" NavigateUrl="administration.aspx?page=settings" runat="server" Width="150px">
                        </telerik:RadTab>

                        <telerik:RadTab Text="Manage Data" NavigateUrl="administration.aspx?page=managedata" runat="server" Width="150px">
                        </telerik:RadTab>
                    </Tabs>
                </telerik:RadTabStrip>
                <telerik:RadMultiPage runat="server" ID="RadMultiPage1" SelectedIndex="0" BorderStyle="Solid" BorderWidth="1px" Width="96%">

                    <telerik:RadPageView ID="RadPageView2" runat="server" Height="800px">
                        <telerik:RadPanelBar ID="pnlUserDetails" runat="server" Width="1290px" Height="500px" Skin="Web20">
                            <Items>
                                <telerik:RadPanelItem runat="server" Text="Manage User Details">
                                    <ContentTemplate>
                                        <table style="width: 100%; margin-left: 10px">
                                            <tr>
                                                <td>

                                                    <p>
                                                        <asp:Label ID="Label1" runat="server" Visible="false"></asp:Label>
                                                    </p>

                                                    <!-- BEGIN FORM-->

                                                    <table>
                                                        <tr>
                                                            <td class="newclass-users">
                                                                <label>Name</label></td>
                                                            <td>
                                                                <telerik:RadTextBox ID="txtName" runat="server" Skin="Web20" class="form-control">
                                                                </telerik:RadTextBox>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtName" ValidationGroup="Create" ErrorMessage="Name is required"></asp:RequiredFieldValidator>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="newclass-users">
                                                                <label>Email</label></td>
                                                            <td>
                                                                <telerik:RadTextBox ID="txtCreatUserEmail" runat="server" Skin="Web20" class="form-control"></telerik:RadTextBox>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtCreatUserEmail" ValidationGroup="Create" ErrorMessage="Email is required"></asp:RequiredFieldValidator>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="newclass-users">
                                                                <label>Password</label></td>
                                                            <td>
                                                                <telerik:RadTextBox ID="txtPassword" runat="server" TextMode="Password" Skin="Web20" class="form-control"></telerik:RadTextBox>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtPassword" ValidationGroup="Create" ErrorMessage="Password is required"></asp:RequiredFieldValidator>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="newclass-users">
                                                                <label>Confirm Password</label></td>
                                                            <td>
                                                                <telerik:RadTextBox ID="txtConfirmPassword" TextMode="Password" runat="server" Skin="Web20" class="form-control"></telerik:RadTextBox>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtConfirmPassword" ValidationGroup="Create" ErrorMessage="Confirm Password is required"></asp:RequiredFieldValidator>
                                                                <asp:CompareValidator ID="cvPassword" runat="server" ControlToCompare="txtPassword" ControlToValidate="txtConfirmPassword" ValidationGroup="Create" ErrorMessage="Password doesn't match"></asp:CompareValidator>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td></td>
                                                            <td>
                                                                <telerik:RadButton ID="btnSave" Width="70px" class="btn default" runat="server" Skin="Web20" Text="Save" OnClick="btnSave_Click" ValidationGroup="Create"></telerik:RadButton>

                                                                <telerik:RadButton ID="btnClear" Width="70px" class="btn default" runat="server" Text="Clear" OnClick="btnClear_Click" Skin="Web20"></telerik:RadButton>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan="2">
                                                                <asp:Label ID="lblError" runat="server" Visible="False" ForeColor="Red"></asp:Label></td>
                                                        </tr>
                                                    </table>
                                                    <br />
                                                    <div class="horizontal_scroll grdLoans_cnt">

                                                        <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel2" runat="server">
                                                        </telerik:RadAjaxLoadingPanel>
                                                        <asp:Panel ID="pnl2" runat="server" Width="1250px" ScrollBars="Both">
                                                            <telerik:RadAjaxPanel ID="panel2" runat="server" LoadingPanelID="RadAjaxLoadingPanel2">
                                                                <telerik:RadGrid ID="grdAccounts" runat="server" Skin="Web20" AutoGenerateColumns="false" AllowSorting="True" AllowPaging="True" OnItemCommand="grdAccounts_ItemCommand" OnItemDataBound="grdAccounts_ItemDataBound" OnSortCommand="grdAccounts_SortCommand" AllowFilteringByColumn="True">
                                                                    <ClientSettings>
                                                                        <Scrolling AllowScroll="true" UseStaticHeaders="true" />
                                                                    </ClientSettings>
                                                                    <MasterTableView Width="100%" CommandItemDisplay="Top" AllowFilteringByColumn="True">
                                                                        <CommandItemSettings ShowAddNewRecordButton="false" />

                                                                        <PagerStyle Mode="NextPrevNumericAndAdvanced"></PagerStyle>
                                                                        <CommandItemSettings ShowExportToWordButton="true" ShowExportToCsvButton="true"></CommandItemSettings>
                                                                        <Columns>
                                                                            <telerik:GridBoundColumn DataField="Name" HeaderText="Name">
                                                                                <HeaderStyle Width="180px" />
                                                                            </telerik:GridBoundColumn>
                                                                            <telerik:GridBoundColumn DataField="**" HeaderText="Password">
                                                                                <HeaderStyle Width="110px" />
                                                                            </telerik:GridBoundColumn>
                                                                            <telerik:GridBoundColumn DataField="Email" HeaderText="Email">
                                                                                <HeaderStyle Width="250px" />
                                                                            </telerik:GridBoundColumn>
                                                                            <telerik:GridBoundColumn DataField="LastPasswordReset" HeaderText="Last Password Reset" DataFormatString="{0:dd-MM-yyyy}">
                                                                                <HeaderStyle Width="130px" />
                                                                            </telerik:GridBoundColumn>
                                                                            <telerik:GridBoundColumn DataField="CreatedDate" HeaderText="Created Date" DataFormatString="{0:dd-MM-yyyy}">
                                                                                <HeaderStyle Width="110px" />
                                                                            </telerik:GridBoundColumn>
                                                                            <telerik:GridTemplateColumn>
                                                                                <ItemTemplate>
                                                                                    <telerik:RadButton ID="btnChangeRole" runat="server" Text="Change Role" CommandName="ChangeRole" CommandArgument='<%# Eval("ID") %>' Skin="Web20"></telerik:RadButton>
                                                                                    <%-- <telerik:RadDropDownList ID="ddlChangeRole" runat="server" ></telerik:RadDropDownList>--%>
                                                                                    <telerik:RadDropDownList ID="ddlChangeRole" runat="server" AutoPostBack="true" Skin="Web20" Width="100px">
                                                                                    </telerik:RadDropDownList>
                                                                                    <%--<asp:Button ID="btnChangeRole" runat="server" Text="Change Role" CommandName="ChangeRole" CommandArgument='<%# Eval("ID") %>' OnClientClick="return confirm('Are you sure');" />--%>
                                                                                    <%--<asp:DropDownList ID="ddlChangeRole" runat="server"></asp:DropDownList>--%>
                                                                                    <telerik:RadButton ID="btnResetPassword" runat="server" Text="Reset Password" CommandName="Reset" CommandArgument='<%# Eval("ID") %>' Skin="Web20" OnClientClicking="showConfirmRadWindow"></telerik:RadButton>
                                                                                    <telerik:RadButton ID="btnRemove" runat="server" Text="Delete" CommandName="Remove" CommandArgument='<%# Eval("ID") %>' Skin="Web20"></telerik:RadButton>
                                                                                    <telerik:RadButton ID="btnLogOff" runat="server" Text="LogOff" CommandName="LogOff" CommandArgument='<%# Eval("ID") %>' Skin="Web20"></telerik:RadButton>
                                                                                    <%--<asp:Button ID="btnResetPassword" runat="server" Text="Reset Password" CommandName="Reset" CommandArgument='<%# Eval("ID") %>' OnClientClick="return confirm('Are you sure');" />--%>
                                                                                    <%--<asp:Button ID="btnRemove" runat="server" Text="Delete" CommandName="Remove" OnClientClick="return confirm('Are you sure');" CommandArgument='<%# Eval("ID") %>' />--%>
                                                                                </ItemTemplate>
                                                                            </telerik:GridTemplateColumn>
                                                                        </Columns>
                                                                    </MasterTableView>
                                                                </telerik:RadGrid>

                                                                <telerik:RadWindow ID="confirmWindow" runat="server" VisibleTitlebar="false" VisibleStatusbar="false"
                                                                    Modal="true" Behaviors="None" Height="150px" Width="300px">
                                                                    <ContentTemplate>
                                                                        <div style="padding-left: 30px; padding-top: 20px; width: 200px; float: left; color: black;">
                                                                            <asp:Label ID="lblConfirm" Font-Size="14px" Text="Are you sure you want to reset password?"
                                                                                runat="server" Skin="Web20"></asp:Label>
                                                                            <br />
                                                                            <br />
                                                                            <telerik:RadButton ID="RadButtonYes" runat="server" Text="Yes" AutoPostBack="false"
                                                                                OnClientClicked="confirmResult" Skin="Web20">
                                                                            </telerik:RadButton>
                                                                            <telerik:RadButton ID="RadButtonNo" runat="server" Text="No" AutoPostBack="false"
                                                                                OnClientClicked="confirmResult" Skin="Web20">
                                                                            </telerik:RadButton>
                                                                        </div>
                                                                    </ContentTemplate>
                                                                </telerik:RadWindow>
                                                            </telerik:RadAjaxPanel>
                                                        </asp:Panel>

                                                    </div>
                                                </td>
                                            </tr>
                                        </table>
                                    </ContentTemplate>
                                </telerik:RadPanelItem>
                                <telerik:RadPanelItem runat="server" Text="Manage Locked Accounts">
                                    <ContentTemplate>
                                        <table style="width: 100%; margin-left: 10px">
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lblLockedAccountMessage" runat="server" Font-Bold="true"></asp:Label>
                                                    <br />
                                                    <br />
                                                    <div class="horizontal_scroll grdLoans_cnt">
                                                        <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server">
                                                        </telerik:RadAjaxLoadingPanel>
                                                        <asp:Panel ID="pnl1" runat="server" Width="1250px" ScrollBars="Both">
                                                            <telerik:RadAjaxPanel ID="panel1" runat="server" LoadingPanelID="RadAjaxLoadingPanel1">
                                                                <telerik:RadGrid ID="grdUsers" runat="server" Skin="Web20" AutoGenerateColumns="false" AllowSorting="True" AllowPaging="True" OnItemCommand="grdUsers_ItemCommand" OnSortCommand="grdUsers_SortCommand" AllowFilteringByColumn="True">
                                                                    <ExportSettings HideStructureColumns="true">
                                                                    </ExportSettings>
                                                                    <GroupingSettings CaseSensitive="false"></GroupingSettings>
                                                                    <MasterTableView Width="100%" CommandItemDisplay="Top" AutoGenerateColumns="false" AllowFilteringByColumn="True">
                                                                        <CommandItemSettings ShowAddNewRecordButton="false" />
                                                                        <PagerStyle Mode="NextPrevNumericAndAdvanced"></PagerStyle>
                                                                        <CommandItemSettings ShowExportToWordButton="true" ShowExportToCsvButton="true"></CommandItemSettings>
                                                                        <Columns>
                                                                            <telerik:GridBoundColumn DataField="Name" HeaderText="Name" FilterControlWidth="120px" CurrentFilterFunction="Contains">
                                                                            </telerik:GridBoundColumn>
                                                                            <telerik:GridBoundColumn DataField="Password" HeaderText="Password">
                                                                            </telerik:GridBoundColumn>
                                                                            <telerik:GridBoundColumn DataField="LastPasswordReset" HeaderText="Last Password Reset" DataFormatString="{0:dd-MM-yyyy}">
                                                                            </telerik:GridBoundColumn>
                                                                            <telerik:GridTemplateColumn ShowFilterIcon="false">
                                                                                <ItemTemplate>
                                                                                    <telerik:RadButton ID="btnUnlock" runat="server" Text="Un Lock" Skin="Web20" CommandName="Unlock" CommandArgument='<%# Eval("ID") %>'></telerik:RadButton>
                                                                                </ItemTemplate>
                                                                            </telerik:GridTemplateColumn>
                                                                        </Columns>
                                                                    </MasterTableView>
                                                                </telerik:RadGrid>
                                                            </telerik:RadAjaxPanel>
                                                        </asp:Panel>
                                                    </div>
                                                </td>
                                            </tr>
                                        </table>
                                    </ContentTemplate>
                                </telerik:RadPanelItem>
                                <telerik:RadPanelItem runat="server" Text="Manage User Logged Onto System">
                                    <ContentTemplate>
                                        <table style="width: 100%; margin-left: 10px">
                                            <tr>
                                                <td>
                                                    <asp:Panel ID="Panel8" runat="server" Width="1250px" ScrollBars="Both">
                                                        <telerik:RadAjaxPanel ID="RadAjaxPanel11" runat="server">
                                                            <telerik:RadGrid ID="grdLockedAcc" runat="server" Skin="Web20" AutoGenerateColumns="false" AllowSorting="True" AllowPaging="True" OnItemCommand="grdLockedAcc_ItemCommand" OnSortCommand="grdLockedAcc_SortCommand" AllowFilteringByColumn="True" Width="1250px">
                                                                <ClientSettings>
                                                                    <Scrolling AllowScroll="true" UseStaticHeaders="true" />
                                                                </ClientSettings>
                                                                <MasterTableView Width="100%" CommandItemDisplay="Top" AllowFilteringByColumn="True">
                                                                    <CommandItemSettings ShowAddNewRecordButton="false" />
                                                                    <PagerStyle Mode="NextPrevNumericAndAdvanced"></PagerStyle>
                                                                    <CommandItemSettings ShowExportToWordButton="true" ShowExportToCsvButton="true"></CommandItemSettings>
                                                                    <Columns>
                                                                        <telerik:GridBoundColumn DataField="Name" HeaderText="Name">
                                                                        </telerik:GridBoundColumn>
                                                                        <telerik:GridBoundColumn DataField="**" HeaderText="Password">
                                                                        </telerik:GridBoundColumn>
                                                                        <telerik:GridBoundColumn DataField="Email" HeaderText="Email">
                                                                        </telerik:GridBoundColumn>
                                                                        <telerik:GridBoundColumn DataField="LastPasswordReset" HeaderText="Last Password Reset" DataFormatString="{0:dd-MM-yyyy}">
                                                                        </telerik:GridBoundColumn>
                                                                        <telerik:GridBoundColumn DataField="CreatedDate" HeaderText="Created Date" DataFormatString="{0:dd-MM-yyyy}">
                                                                        </telerik:GridBoundColumn>
                                                                        <telerik:GridTemplateColumn>
                                                                            <ItemTemplate>
                                                                                <telerik:RadButton ID="btnLogOff" runat="server" Text="LogOff" CommandName="LogOff" CommandArgument='<%# Eval("ID") %>' Skin="Web20"></telerik:RadButton>
                                                                                <%--<asp:Button ID="btnResetPassword" runat="server" Text="Reset Password" CommandName="Reset" CommandArgument='<%# Eval("ID") %>' OnClientClick="return confirm('Are you sure');" />--%>
                                                                                <%--<asp:Button ID="btnRemove" runat="server" Text="Delete" CommandName="Remove" OnClientClick="return confirm('Are you sure');" CommandArgument='<%# Eval("ID") %>' />--%>
                                                                            </ItemTemplate>
                                                                        </telerik:GridTemplateColumn>
                                                                    </Columns>
                                                                </MasterTableView>
                                                            </telerik:RadGrid>

                                                            <telerik:RadWindow ID="RadWindow1" runat="server" VisibleTitlebar="false" VisibleStatusbar="false"
                                                                Modal="true" Behaviors="None" Height="150px" Width="300px">
                                                                <ContentTemplate>
                                                                    <div style="padding-left: 30px; padding-top: 20px; width: 200px; float: left; color: black;">
                                                                        <asp:Label ID="Label4" Font-Size="14px" Text="Are you sure you want to reset password?"
                                                                            runat="server" Skin="Web20"></asp:Label>
                                                                        <br />
                                                                        <br />
                                                                        <telerik:RadButton ID="RadButton5" runat="server" Text="Yes" AutoPostBack="false"
                                                                            OnClientClicked="confirmResult" Skin="Web20">
                                                                        </telerik:RadButton>
                                                                        <telerik:RadButton ID="RadButton6" runat="server" Text="No" AutoPostBack="false"
                                                                            OnClientClicked="confirmResult" Skin="Web20">
                                                                        </telerik:RadButton>
                                                                    </div>
                                                                </ContentTemplate>
                                                            </telerik:RadWindow>
                                                        </telerik:RadAjaxPanel>
                                                    </asp:Panel>
                                                </td>
                                            </tr>
                                        </table>
                                    </ContentTemplate>
                                </telerik:RadPanelItem>
                            </Items>
                        </telerik:RadPanelBar>
                    </telerik:RadPageView>

                    <telerik:RadPageView ID="RadPageView4" runat="server" Height="500px">
                        <%--<uc1:contactus runat="server" id="ContactUs" />--%>
                        <telerik:RadPanelBar ID="pnlSystemSettings" runat="server" Width="1290px" Height="500px" Skin="Web20">
                            <Items>
                                <telerik:RadPanelItem runat="server" Text="Settings">
                                    <ContentTemplate>
                                        <table style="width: 100%; margin-left: 10px">
                                            <tr>
                                                <td>
                                                    <br />

                                                    <p>
                                                        <asp:Label ID="Label3" runat="server" Visible="false"></asp:Label>
                                                    </p>

                                                    <!-- BEGIN FORM-->

                                                    <asp:Label ID="lblMessage" runat="server" Visible="false"></asp:Label>
                                                    <table border="2">
                                                        <tr>
                                                            <td align="center" style="font-size: medium; font-weight: bold; height: 40px;">Setting</td>
                                                            <td align="center" style="font-size: medium; font-weight: bold">Current Value</td>
                                                            <td align="center" style="font-size: medium; font-weight: bold">Enter New Value</td>
                                                        </tr>
                                                        <tr>
                                                            <td style="width: 200px; margin-left: 4px">
                                                                <label>System Idle Lockout(Mins)</label>
                                                            </td>
                                                            <td id="txtIdleTime" runat="server" style="text-align: center; background-color: lightgray; width: 150px"></td>
                                                            <td align="center">-</td>
                                                        </tr>
                                                        <tr>
                                                            <td style="width: 200px">
                                                                <label>Max Loan Duration (Years)</label></td>
                                                            <td id="txtCurrentLoanYear" runat="server" style="text-align: center; background-color: lightgray;"></td>
                                                            <td>
                                                                <telerik:RadTextBox ID="txtYears" runat="server" Skin="Web20" class="form-control"></telerik:RadTextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan="3"></td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan="3">
                                                                <telerik:RadButton ID="btnSaveSettings" Width="70px" runat="server" Skin="Web20" OnClick="btnSaveSettings_Click" Text="Save"></telerik:RadButton>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                        </table>
                                        <br />
                                    </ContentTemplate>
                                </telerik:RadPanelItem>
                                <telerik:RadPanelItem runat="server" Text="Email Configuration">
                                    <ContentTemplate>
                                        <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel3" runat="server">
                                        </telerik:RadAjaxLoadingPanel>
                                        <telerik:RadAjaxPanel ID="panel3" runat="server" LoadingPanelID="RadAjaxLoadingPanel3">
                                            <table style="width: 100%; margin-left: 10px">
                                                <tr>
                                                    <td>

                                                        <p>
                                                            <asp:Label ID="Label2" runat="server" Visible="false"></asp:Label>
                                                        </p>

                                                        <!-- BEGIN FORM-->

                                                        <table>
                                                            <tr>
                                                                <td class="newclass-email">
                                                                    <label>Email</label></td>
                                                                <td>
                                                                    <telerik:RadTextBox ID="txtEmail" runat="server" Skin="Web20" class="form-control">
                                                                    </telerik:RadTextBox>
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtEmail" ValidationGroup="Create" ErrorMessage="txtEmail is required"></asp:RequiredFieldValidator>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="newclass-email">
                                                                    <label>Group</label></td>
                                                                <td>
                                                                    <asp:DropDownList ID="ddlGroups" runat="server" SkinID="Web20" Enabled="true" Width="163px"></asp:DropDownList>
                                                                    <%--<input type="password" class="form-control" placeholder="Password">--%></td>
                                                            </tr>
                                                            <tr>
                                                                <td></td>
                                                                <td>
                                                                    <telerik:RadButton ID="btnClearEmail" Width="70px" class="form-control" runat="server" Text="Clear" OnClick="btnClearEmail_Click" Skin="Web20"></telerik:RadButton>
                                                                    <telerik:RadButton ID="btnSaveEmail" Width="70px" class="form-control" runat="server" Text="Save" ValidationGroup="CreateEmail" OnClick="btnSaveEmail_Click" Skin="Web20"></telerik:RadButton>
                                                                </td>

                                                            </tr>
                                                            <asp:HiddenField ID="hfEmailGroupId" runat="server" />
                                                            <tr>
                                                                <td class="newclass-email">
                                                                    <label>Email Receiver Group</label></td>
                                                                <td>
                                                                    <asp:DropDownList ID="ddlGroupReceiver" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlGroupReceiver_SelectedIndexChanged" Width="163px">
                                                                    </asp:DropDownList></td>
                                                            </tr>
                                                        </table>

                                                        <br />
                                                        <div class="horizontal_scroll grdLoans_cnt">


                                                            <label id="lblEmailError" runat="server" visible="false"></label>
                                                            <asp:Panel ID="pnl3" runat="server" Width="1250px" ScrollBars="Both">
                                                                <telerik:RadGrid ID="grdEmail" runat="server" Skin="Web20" AutoGenerateColumns="false" AllowSorting="True" AllowPaging="True" OnItemCommand="grdEmail_ItemCommand" OnSortCommand="grdEmail_SortCommand" AllowFilteringByColumn="True">
                                                                    <MasterTableView Width="100%" CommandItemDisplay="Top" AllowFilteringByColumn="True">
                                                                        <CommandItemSettings ShowAddNewRecordButton="false" />
                                                                        <PagerStyle Mode="NextPrevNumericAndAdvanced"></PagerStyle>
                                                                        <CommandItemSettings ShowExportToWordButton="true" ShowExportToCsvButton="true"></CommandItemSettings>
                                                                        <Columns>
                                                                            <telerik:GridBoundColumn DataField="Name" HeaderText="Name">
                                                                            </telerik:GridBoundColumn>
                                                                            <telerik:GridBoundColumn DataField="GroupName" HeaderText="Group">
                                                                            </telerik:GridBoundColumn>
                                                                            <telerik:GridTemplateColumn HeaderText="Action">
                                                                                <ItemTemplate>
                                                                                    <telerik:RadButton ID="btnEdit" Skin="Web20" runat="server" Text="Edit" CommandName="EditRow" CommandArgument='<%# Eval("ID") %>'></telerik:RadButton>
                                                                                    <telerik:RadButton ID="btnRemove" Skin="Web20" runat="server" Text="Delete" CommandName="Remove" CommandArgument='<%# Eval("ID") %>'></telerik:RadButton>
                                                                                    <telerik:RadButton ID="btnSendEmail" Skin="Web20" runat="server" Text="Send Email" CommandName="SendEmail" CommandArgument='<%# Eval("GroupName") %>'></telerik:RadButton>
                                                                                </ItemTemplate>
                                                                            </telerik:GridTemplateColumn>
                                                                        </Columns>
                                                                    </MasterTableView>
                                                                </telerik:RadGrid>
                                                            </asp:Panel>

                                                        </div>
                                                    </td>
                                                </tr>
                                            </table>
                                        </telerik:RadAjaxPanel>
                                    </ContentTemplate>
                                </telerik:RadPanelItem>

                            </Items>
                        </telerik:RadPanelBar>
                    </telerik:RadPageView>






                    <telerik:RadPageView ID="RadPageView10" runat="server" Height="800px">
                        <telerik:RadPanelBar ID="pnlBar1" runat="server" Width="1293px" Height="500px" Skin="Web20">
                            <Items>
                                <telerik:RadPanelItem runat="server" Text="Remove Data">
                                    <ContentTemplate>
                                        <div style="background-color: #B7C7E4; width: 100%; height: 150px;">
                                            <br />
                                            <div class="form-group">
                                                <label class="col-md-2 control-label">Loans</label>
                                                <telerik:RadButton ID="btnRemoveLoan" runat="server" Text="Remove All Loans" Skin="Web20" OnClientClicking="RadConfirm" Width="170px" OnClick="btnRemoveLoan_Click"></telerik:RadButton>
                                                <asp:Label ID="lblRemoveLoanMessage" runat="server"></asp:Label>
                                            </div>
                                            <div class="form-group">
                                                <label class="col-md-2 control-label">Quotes and Trades</label>
                                                <telerik:RadButton ID="btnRemoveQuotes" runat="server" Text="Remove All Quotes and Trades" OnClientClicking="RadConfirm" Skin="Web20" Width="170px" OnClick="btnRemoveQuotes_Click"></telerik:RadButton>
                                                <asp:Label ID="lblRemoveQuotesMessage" runat="server"></asp:Label>
                                            </div>
                                            <telerik:RadWindowManager ID="RadWindowManager1"
                                                runat="server" EnableShadow="true" IconUrl="~">
                                            </telerik:RadWindowManager>
                                        </div>
                                    </ContentTemplate>
                                </telerik:RadPanelItem>
                                <telerik:RadPanelItem runat="server" Text="Import / Export Data">
                                    <ContentTemplate>
                                        <div style="background-color: #B7C7E4; width: 100%; height: 300px;">
                                            <div style="font-weight: bold; width: 1000px;" class="col-md-2 control-label">
                                                Select the csv file you wish to use, it's type and whether you wish to import or export.  Then press Run Operation.
                                                <br />
                                               
                                            </div>
                                            <div class="form-group" style="margin-top: 15px">
                                                <label class="col-md-2 control-label">CSV TO Upload</label>
                                                <asp:FileUpload ID="fuFile" runat="server"></asp:FileUpload>
                                            </div>
                                            <div class="form-group">

                                                <label class="col-md-2 control-label">Separator </label>

                                                <telerik:RadDropDownList ID="ddlSeparator" runat="server" Skin="Web20">
                                                    <Items>
                                                        <telerik:DropDownListItem Text="Comma" Value="Comma" Selected="true" />
                                                        <telerik:DropDownListItem Text="Space" Value="Space" />
                                                        <telerik:DropDownListItem Text="Semicolon" Value="Semicolon" />
                                                        <telerik:DropDownListItem Text="Tab" Value="Tab" />
                                                    </Items>
                                                </telerik:RadDropDownList>


                                            </div>
                                            <div class="form-group">
                                                <label class="col-md-2 control-label">Document Type</label>

                                                <telerik:RadDropDownList ID="ddlImportType" runat="server" Skin="Web20">
                                                    <Items>
                                                        <telerik:DropDownListItem Text="Loan" Value="Loan" Selected="true" />
                                                        <telerik:DropDownListItem Text="Quotes & Traders" Value="Quotes" />
                                                        <telerik:DropDownListItem Text="Counterparties" Value="Counterparties" />
                                                        <telerik:DropDownListItem Text="EUR Curve" Value="EUR Curve" />
                                                        <telerik:DropDownListItem Text="US Curve" Value="US Curve" />
                                                    </Items>
                                                </telerik:RadDropDownList>
                                            </div>

                                            <div class="form-group">
                                                <label class="col-md-2 control-label">
                                                    Import / Export</label>
                                                <telerik:RadDropDownList ID="ddlImportExport" runat="server" Skin="Web20">
                                                    <Items>
                                                        <telerik:DropDownListItem Text="Import" Value="Import" Selected="true" />
                                                        <telerik:DropDownListItem Text="Export" Value="Export" />
                                                    </Items>
                                                </telerik:RadDropDownList>
                                            </div>

                                            <div class="form-group">
                                                <label class="col-md-2 control-label">
                                                    <asp:Label ID="lblExportStatus" runat="server"></asp:Label>
                                                </label>
                                                <telerik:RadButton ID="btnExport" Skin="Web20" runat="server" Text="Run Operation" OnClick="btnExport_Click" OnClientClicking="showConfirmRadWindowForExport"></telerik:RadButton>

                                                <telerik:RadWindow ID="rwExportYesNo" runat="server" VisibleTitlebar="false" VisibleStatusbar="false"
                                                    Modal="true" Behaviors="None" Height="150px" Width="300px">
                                                    <ContentTemplate>
                                                        <div style="padding-left: 30px; padding-top: 20px; width: 200px; float: left; color: black;">
                                                            <asp:Label ID="Label27" Font-Size="14px" Text="Are you sure you want to continue?"
                                                                runat="server" Skin="Web20"></asp:Label>
                                                            <br />
                                                            <br />
                                                            <telerik:RadButton ID="rbExportYes" runat="server" Text="Yes" AutoPostBack="false"
                                                                OnClientClicked="confirmResultExport" Skin="Web20">
                                                            </telerik:RadButton>
                                                            <telerik:RadButton ID="rbExportNo" runat="server" Text="No" AutoPostBack="false"
                                                                OnClientClicked="confirmResultExport" Skin="Web20">
                                                            </telerik:RadButton>
                                                        </div>
                                                    </ContentTemplate>
                                                </telerik:RadWindow>
                                            </div>
                                        </div>
                                    </ContentTemplate>
                                </telerik:RadPanelItem>
                                <telerik:RadPanelItem runat="server" Text="Manage Countries List">
                                    <ContentTemplate>
                                        <table style="width: 100%; margin-left: 10px;">
                                            <tr>
                                                <td>
                                                    <%--<p style="font: bold; text-align: justify; margin-left: 30px; font-size: large;">
                                                        Country Detail
                                                    </p>--%>
                                                    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanelCountry" runat="server">
                                                    </telerik:RadAjaxLoadingPanel>


                                                    <p>
                                                        <asp:Label ID="lblCountryError" runat="server" Visible="false"></asp:Label>
                                                    </p>

                                                    <!-- BEGIN FORM-->

                                                    <table>
                                                        <tr>
                                                            <td class="newclass-users">
                                                                <label>Name</label></td>
                                                            <td>
                                                                <telerik:RadTextBox ID="txtCountryName" runat="server" Skin="Web20" class="form-control">
                                                                </telerik:RadTextBox>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtCountryName" ValidationGroup="Create" ErrorMessage="Name is required"></asp:RequiredFieldValidator>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="newclass-users">
                                                                <label>Code</label></td>
                                                            <td>
                                                                <telerik:RadTextBox ID="txtCountryCode" runat="server" Skin="Web20" class="form-control">
                                                                </telerik:RadTextBox>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="txtCountryCode" ValidationGroup="Create" ErrorMessage="Code is required"></asp:RequiredFieldValidator>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td></td>
                                                            <td align="left">
                                                                <telerik:RadButton ID="btnCountrySave" Width="75px" runat="server" Skin="Web20" Text="Save" OnClick="btnCountrySave_Click"></telerik:RadButton>
                                                                <telerik:RadButton ID="btnCountryClear" Width="75px" runat="server" Text="Clear" OnClick="btnCountryClear_Click" Skin="Web20"></telerik:RadButton>
                                                            </td>
                                                        </tr>
                                                        <asp:HiddenField ID="hfCountryID" runat="server" />
                                                        <tr>
                                                            <td colspan="2">
                                                                <asp:Label ID="Label10" runat="server" Visible="False" ForeColor="Red"></asp:Label></td>
                                                        </tr>
                                                    </table>

                                                    <br />
                                                    <div class="horizontal_scroll grdLoans_cnt">
                                                        <asp:Panel ID="pnl4" runat="server" Width="1250px" ScrollBars="Both">
                                                            <telerik:RadGrid ID="grdCountry" runat="server" Skin="Web20" AutoGenerateColumns="false" AllowSorting="True" AllowPaging="True" OnItemCommand="grdCountry_ItemCommand" OnSortCommand="grdCountry_SortCommand" AllowFilteringByColumn="True">
                                                                <MasterTableView Width="100%" CommandItemDisplay="Top" AllowFilteringByColumn="True">
                                                                    <CommandItemSettings ShowAddNewRecordButton="false" />
                                                                    <PagerStyle Mode="NextPrevNumericAndAdvanced"></PagerStyle>
                                                                    <CommandItemSettings ShowExportToWordButton="true" ShowExportToCsvButton="true"></CommandItemSettings>
                                                                    <Columns>
                                                                        <telerik:GridBoundColumn DataField="Name" HeaderText="Name">
                                                                        </telerik:GridBoundColumn>
                                                                        <telerik:GridBoundColumn DataField="Code" HeaderText="Code">
                                                                        </telerik:GridBoundColumn>
                                                                        <telerik:GridTemplateColumn AllowFiltering="false">
                                                                            <ItemTemplate>
                                                                                <telerik:RadButton ID="btnCountryEdit" Skin="Web20" runat="server" Text="Edit" CommandName="EditCountry" CommandArgument='<%# Eval("ID") %>'></telerik:RadButton>
                                                                                <%--<telerik:RadButton ID="btnCountryRemove" OnClientClicking="showConfirmCountryDelete" Skin="Web20" runat="server" Text="Delete" CommandName="RemoveCountry" CommandArgument='<%# Eval("ID") %>'></telerik:RadButton>--%>
                                                                                <asp:Button ID="btnCountryRemove" Text="Delete" runat="server" OnClientClick="javascript:if(!confirm('Are you sure want to delete this record?')){return false;}" CommandName="RemoveCountry" CommandArgument='<%# Eval("ID") %>' />
                                                                            </ItemTemplate>
                                                                        </telerik:GridTemplateColumn>
                                                                    </Columns>
                                                                </MasterTableView>
                                                            </telerik:RadGrid>
                                                            <telerik:RadWindow ID="confirmCountryDelete" runat="server" VisibleTitlebar="false" VisibleStatusbar="false"
                                                                Modal="true" Behaviors="None" Height="150px" Width="300px">
                                                                <ContentTemplate>
                                                                    <div style="padding-left: 30px; padding-top: 20px; width: 200px; float: left; color: black;">
                                                                        <asp:Label ID="Label15" Font-Size="14px" Text="Are you sure you want to delete record?"
                                                                            runat="server" Skin="Web20"></asp:Label>
                                                                        <br />
                                                                        <br />
                                                                        <telerik:RadButton ID="rbCountrySuccess" runat="server" Text="Yes" AutoPostBack="false"
                                                                            OnClientClicked="confirmCountryResult" Skin="Web20">
                                                                        </telerik:RadButton>
                                                                        <telerik:RadButton ID="rbCountryFailure" runat="server" Text="No" AutoPostBack="false"
                                                                            OnClientClicked="confirmCountryResult" Skin="Web20">
                                                                        </telerik:RadButton>
                                                                    </div>
                                                                </ContentTemplate>
                                                            </telerik:RadWindow>
                                                        </asp:Panel>
                                                    </div>
                                                </td>
                                            </tr>
                                        </table>
                                    </ContentTemplate>
                                </telerik:RadPanelItem>
                                <telerik:RadPanelItem runat="server" Text="Manage Counterparties List">
                                    <ContentTemplate>
                                        <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel9" runat="server">
                                        </telerik:RadAjaxLoadingPanel>
                                        <telerik:RadAjaxPanel ID="panel9" runat="server" LoadingPanelID="RadAjaxLoadingPanel9">
                                            <table style="width: 100%; margin-left: 10px">
                                                <tr>
                                                    <td>
                                                        <%--<p style="font: bold; text-align: justify; margin-left: 30px; font-size: large;">
                                                            CounterParty Detail
                                                        </p>--%>

                                                        <p>
                                                            <asp:Label ID="Label5" runat="server" Visible="false"></asp:Label>
                                                        </p>

                                                        <!-- BEGIN FORM-->

                                                        <table>
                                                            <tr>
                                                                <td class="newclass-email">
                                                                    <label>Name</label></td>
                                                                <td>
                                                                    <telerik:RadTextBox ID="txtCPName" runat="server" Skin="Web20" class="form-control">
                                                                    </telerik:RadTextBox>
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="txtCPName" ValidationGroup="Create" ErrorMessage="Name is required"></asp:RequiredFieldValidator>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="newclass-email">
                                                                    <label>Region</label></td>
                                                                <td>
                                                                    <telerik:RadDropDownList ID="txtCPRegion" runat="server" Skin="Web20">
                                                                    </telerik:RadDropDownList>
                                                            </tr>
                                                            <tr>
                                                                <td class="newclass-email">
                                                                    <label>Type</label></td>
                                                                <td>
                                                                    <%--  <telerik:RadTextBox ID="txtCPType" runat="server" Skin="Web20" class="form-control">
                                                                    </telerik:RadTextBox>--%>
                                                                    <telerik:RadDropDownList ID="txtCPType" runat="server" Skin="Web20">
                                                                        <Items>
                                                                            <telerik:DropDownListItem Text="Asset Manager" Value="Asset Manager" Selected="true" />
                                                                            <telerik:DropDownListItem Text="Bank Portfolio" Value="Bank Portfolio" />
                                                                            <telerik:DropDownListItem Text="Hedge Fund" Value="Hedge Fund" />
                                                                            <telerik:DropDownListItem Text="Broker" Value="Broker" />
                                                                            <telerik:DropDownListItem Text="Family office" Value="Family office" />
                                                                            <telerik:DropDownListItem Text="Pension Fund" Value="Pension Fund" />

                                                                        </Items>
                                                                    </telerik:RadDropDownList>
                                                                </td>
                                                            </tr>
                                                            <asp:HiddenField ID="hfCPID" runat="server" />
                                                            <tr>
                                                                <td></td>
                                                                <td>
                                                                    <telerik:RadButton ID="btnCPSave" class="form-control" Width="75px" runat="server" Text="Save" ValidationGroup="CreateEmail" OnClick="btnCPSave_Click" Skin="Web20"></telerik:RadButton>

                                                                    <telerik:RadButton ID="btnCPClear" class="form-control" Width="75px" runat="server" Text="Clear" OnClick="btnCPClear_Click" Skin="Web20"></telerik:RadButton>
                                                                </td>
                                                            </tr>
                                                        </table>

                                                        <br />
                                                        <div class="horizontal_scroll grdLoans_cnt">
                                                            <label id="lblCPError" runat="server" visible="false"></label>
                                                            <asp:Panel ID="Panel4" runat="server" Width="1250px" ScrollBars="Both">
                                                                <telerik:RadGrid ID="grdCounterParty" runat="server" Skin="Web20" AutoGenerateColumns="false" AllowSorting="True" AllowPaging="True" OnItemCommand="grdCounterParty_ItemCommand" OnSortCommand="grdCounterParty_SortCommand" AllowFilteringByColumn="True">
                                                                    <MasterTableView Width="100%" CommandItemDisplay="Top" AllowFilteringByColumn="True">
                                                                        <CommandItemSettings ShowAddNewRecordButton="false" />
                                                                        <PagerStyle Mode="NextPrevNumericAndAdvanced"></PagerStyle>
                                                                        <CommandItemSettings ShowExportToWordButton="true" ShowExportToCsvButton="true"></CommandItemSettings>
                                                                        <Columns>
                                                                            <telerik:GridBoundColumn DataField="Name" HeaderText="Name">
                                                                            </telerik:GridBoundColumn>
                                                                            <telerik:GridBoundColumn DataField="Region" HeaderText="Region">
                                                                            </telerik:GridBoundColumn>
                                                                            <telerik:GridBoundColumn DataField="Type" HeaderText="Type">
                                                                            </telerik:GridBoundColumn>
                                                                            <telerik:GridTemplateColumn HeaderText="Action">
                                                                                <ItemTemplate>
                                                                                    <telerik:RadButton ID="btnCPEdit" Skin="Web20" runat="server" Text="Edit" CommandName="EditCP" CommandArgument='<%# Eval("ID") %>'></telerik:RadButton>
                                                                                    <%--<telerik:RadButton ID="btnCPRemove" OnClientClicking="showConfirmCounterPartyDelete" Skin="Web20" runat="server" Text="Delete" CommandName="RemoveCP" CommandArgument='<%# Eval("ID") %>'></telerik:RadButton>--%>
                                                                                    <asp:Button ID="btnCPRemove" Text="Delete" runat="server" OnClientClick="javascript:if(!confirm('Are you sure want to delete this record?')){return false;}" CommandName="RemoveCP" CommandArgument='<%# Eval("ID") %>' />
                                                                                </ItemTemplate>
                                                                            </telerik:GridTemplateColumn>
                                                                        </Columns>
                                                                    </MasterTableView>
                                                                </telerik:RadGrid>
                                                                <telerik:RadWindow ID="confirmCounterPartyDelete" runat="server" VisibleTitlebar="false" VisibleStatusbar="false"
                                                                    Modal="true" Behaviors="None" Height="150px" Width="300px">
                                                                    <ContentTemplate>
                                                                        <div style="padding-left: 30px; padding-top: 20px; width: 200px; float: left; color: black;">
                                                                            <asp:Label ID="Label16" Font-Size="14px" Text="Are you sure you want to delete record?"
                                                                                runat="server" Skin="Web20"></asp:Label>
                                                                            <br />
                                                                            <br />
                                                                            <telerik:RadButton ID="rbCounterPartySuccess" runat="server" Text="Yes" AutoPostBack="false"
                                                                                OnClientClicked="confirmCounterPartyResult" Skin="Web20">
                                                                            </telerik:RadButton>
                                                                            <telerik:RadButton ID="rbCounterPartyFailure" runat="server" Text="No" AutoPostBack="false"
                                                                                OnClientClicked="confirmCounterPartyResult" Skin="Web20">
                                                                            </telerik:RadButton>
                                                                        </div>
                                                                    </ContentTemplate>
                                                                </telerik:RadWindow>
                                                            </asp:Panel>
                                                        </div>
                                                    </td>
                                                </tr>
                                            </table>

                                        </telerik:RadAjaxPanel>
                                    </ContentTemplate>
                                </telerik:RadPanelItem>
                                <telerik:RadPanelItem runat="server" Text="Manage Credit Agency">
                                    <ContentTemplate>
                                        <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel7" runat="server">
                                        </telerik:RadAjaxLoadingPanel>
                                        <telerik:RadAjaxPanel ID="RadAjaxPanel3" runat="server" LoadingPanelID="RadAjaxLoadingPanel7">
                                            <table style="width: 100%; margin-left: 10px">
                                                <tr>
                                                    <td>


                                                        <p>
                                                            <asp:Label ID="Label17" runat="server" Visible="false"></asp:Label>
                                                        </p>

                                                        <!-- BEGIN FORM-->

                                                        <table>
                                                            <tr>
                                                                <td class="newclass-email">
                                                                    <label>Credit Agency</label></td>
                                                                <td>
                                                                    <telerik:RadTextBox ID="txtCreditAgency" runat="server" Skin="Web20" class="form-control">
                                                                    </telerik:RadTextBox>

                                                                </td>
                                                            </tr>


                                                            <asp:HiddenField ID="hdnCreditAgency" runat="server" />
                                                            <tr>
                                                                <td></td>
                                                                <td>
                                                                    <telerik:RadButton ID="btnSaveAgency" class="form-control" Width="75px" runat="server" Text="Save" OnClick="btnSaveAgency_Click" Skin="Web20"></telerik:RadButton>

                                                                    <telerik:RadButton ID="btnClearAgency" class="form-control" Width="75px" runat="server" Text="Clear" OnClick="btnClearAgency_Click" Skin="Web20"></telerik:RadButton>
                                                                </td>
                                                            </tr>
                                                        </table>

                                                        <br />
                                                        <div class="horizontal_scroll grdLoans_cnt">
                                                            <label id="Label18" runat="server" visible="false"></label>
                                                            <asp:Panel ID="Panel10" runat="server" Width="1250px" ScrollBars="Both">
                                                                <telerik:RadGrid ID="grdAgency" runat="server" Skin="Web20" AutoGenerateColumns="false" AllowSorting="True" AllowPaging="True" OnItemCommand="grdAgency_ItemCommand" OnSortCommand="grdAgency_SortCommand" AllowFilteringByColumn="True">
                                                                    <MasterTableView Width="100%" CommandItemDisplay="Top" AllowFilteringByColumn="True">
                                                                        <CommandItemSettings ShowAddNewRecordButton="false" />
                                                                        <PagerStyle Mode="NextPrevNumericAndAdvanced"></PagerStyle>
                                                                        <CommandItemSettings ShowExportToWordButton="true" ShowExportToCsvButton="true"></CommandItemSettings>
                                                                        <Columns>
                                                                            <telerik:GridBoundColumn DataField="CreditAgency1" HeaderText="CreditAgency">
                                                                            </telerik:GridBoundColumn>


                                                                            <telerik:GridTemplateColumn HeaderText="Action">
                                                                                <ItemTemplate>
                                                                                    <telerik:RadButton ID="btnAgencyEdit" Skin="Web20" runat="server" Text="Edit" CommandName="EditA" CommandArgument='<%# Eval("ID") %>'></telerik:RadButton>
                                                                                    <%--<telerik:RadButton ID="btnCreditRatingRemove" Skin="Web20" runat="server" Text="Delete" CommandName="RemoveCR" CommandArgument='<%# Eval("ID") %>' OnClientClicking="showConfirmRatingDelete"></telerik:RadButton>--%>
                                                                                    <asp:Button ID="btnAgencyRemove" Text="Delete" runat="server" OnClientClick="javascript:if(!confirm('Are you sure want to delete this record?')){return false;}" CommandName="RemoveA" CommandArgument='<%# Eval("ID") %>' />
                                                                                </ItemTemplate>
                                                                            </telerik:GridTemplateColumn>
                                                                        </Columns>
                                                                    </MasterTableView>
                                                                </telerik:RadGrid>

                                                            </asp:Panel>
                                                        </div>
                                                    </td>
                                                </tr>
                                            </table>

                                        </telerik:RadAjaxPanel>
                                    </ContentTemplate>
                                </telerik:RadPanelItem>
                                <telerik:RadPanelItem runat="server" Text="Manage Credit Ratings">
                                    <ContentTemplate>
                                        <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel6" runat="server">
                                        </telerik:RadAjaxLoadingPanel>
                                        <telerik:RadAjaxPanel ID="RadAjaxPanel2" runat="server" LoadingPanelID="RadAjaxLoadingPanel4">
                                            <table style="width: 100%; margin-left: 10px">
                                                <tr>
                                                    <td>


                                                        <p>
                                                            <asp:Label ID="Label11" runat="server" Visible="false"></asp:Label>
                                                        </p>

                                                        <!-- BEGIN FORM-->

                                                        <table>
                                                            <tr>
                                                                <td class="newclass-email">
                                                                    <label>Credit Agency</label></td>
                                                                <td>
                                                                    <%-- <telerik:RadTextBox ID="txtCreditAgency" runat="server" Skin="Web20" class="form-control">
                                                                    </telerik:RadTextBox>--%>
                                                                    <telerik:RadDropDownList ID="ddlAgency" runat="server" Skin="Web20"></telerik:RadDropDownList>

                                                                </td>
                                                            </tr>

                                                            <tr>
                                                                <td class="newclass-email">
                                                                    <label>Rating</label></td>
                                                                <td>
                                                                    <telerik:RadTextBox ID="txtRatings" runat="server" Skin="Web20" class="form-control">
                                                                    </telerik:RadTextBox>
                                                                </td>
                                                            </tr>
                                                            <asp:HiddenField ID="hdnCreditRatings" runat="server" />
                                                            <tr>
                                                                <td></td>
                                                                <td>
                                                                    <telerik:RadButton ID="btnSaveRating" class="form-control" Width="75px" runat="server" Text="Save" OnClick="btnSaveRating_Click" Skin="Web20"></telerik:RadButton>

                                                                    <telerik:RadButton ID="btnClearRating" class="form-control" Width="75px" runat="server" Text="Clear" OnClick="btnClearRating_Click" Skin="Web20"></telerik:RadButton>
                                                                </td>
                                                            </tr>
                                                        </table>

                                                        <br />
                                                        <div class="horizontal_scroll grdLoans_cnt">
                                                            <label id="Label12" runat="server" visible="false"></label>
                                                            <asp:Panel ID="Panel7" runat="server" Width="1250px" ScrollBars="Both">
                                                                <telerik:RadGrid ID="grdCreditRatings" runat="server" Skin="Web20" AutoGenerateColumns="false" AllowSorting="True" AllowPaging="True" OnItemCommand="grdCreditRatings_ItemCommand" OnSortCommand="grdCreditRatings_SortCommand" AllowFilteringByColumn="True" OnItemDataBound="grdCreditRatings_ItemDataBound">
                                                                    <MasterTableView Width="100%" CommandItemDisplay="Top" AllowFilteringByColumn="True">
                                                                        <CommandItemSettings ShowAddNewRecordButton="false" />
                                                                        <PagerStyle Mode="NextPrevNumericAndAdvanced"></PagerStyle>
                                                                        <CommandItemSettings ShowExportToWordButton="true" ShowExportToCsvButton="true"></CommandItemSettings>
                                                                        <Columns>
                                                                            <%--<telerik:GridBoundColumn DataField="CreditAgency" HeaderText="CreditAgency">
                                                                            </telerik:GridBoundColumn>--%>
                                                                            <telerik:GridTemplateColumn HeaderText="CreditAgency">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblAgencyName" runat="server"></asp:Label>
                                                                                </ItemTemplate>
                                                                            </telerik:GridTemplateColumn>
                                                                            <telerik:GridBoundColumn DataField="Rating" HeaderText="Rating">
                                                                            </telerik:GridBoundColumn>
                                                                            <telerik:GridTemplateColumn HeaderText="Action">
                                                                                <ItemTemplate>
                                                                                    <telerik:RadButton ID="btnCreditRatingEdit" Skin="Web20" runat="server" Text="Edit" CommandName="EditCR" CommandArgument='<%# Eval("ID") %>'></telerik:RadButton>
                                                                                    <%--<telerik:RadButton ID="btnCreditRatingRemove" Skin="Web20" runat="server" Text="Delete" CommandName="RemoveCR" CommandArgument='<%# Eval("ID") %>' OnClientClicking="showConfirmRatingDelete"></telerik:RadButton>--%>
                                                                                    <asp:Button ID="btnCreditRatingRemove" Text="Delete" runat="server" OnClientClick="javascript:if(!confirm('Are you sure want to delete this record?')){return false;}" CommandName="RemoveCR" CommandArgument='<%# Eval("ID") %>' />
                                                                                </ItemTemplate>
                                                                            </telerik:GridTemplateColumn>
                                                                        </Columns>
                                                                    </MasterTableView>
                                                                </telerik:RadGrid>
                                                                <telerik:RadWindow ID="confirmRatingDelete" runat="server" VisibleTitlebar="false" VisibleStatusbar="false"
                                                                    Modal="true" Behaviors="None" Height="150px" Width="300px">
                                                                    <ContentTemplate>
                                                                        <div style="padding-left: 30px; padding-top: 20px; width: 200px; float: left; color: black;">
                                                                            <asp:Label ID="Label14" Font-Size="14px" Text="Are you sure you want to delete record?"
                                                                                runat="server" Skin="Web20"></asp:Label>
                                                                            <br />
                                                                            <br />
                                                                            <telerik:RadButton ID="rbRatingSuccess" runat="server" Text="Yes" AutoPostBack="false"
                                                                                OnClientClicked="confirmRatingResult" Skin="Web20">
                                                                            </telerik:RadButton>
                                                                            <telerik:RadButton ID="rbRatingFailure" runat="server" Text="No" AutoPostBack="false"
                                                                                OnClientClicked="confirmRatingResult" Skin="Web20">
                                                                            </telerik:RadButton>
                                                                        </div>
                                                                    </ContentTemplate>
                                                                </telerik:RadWindow>
                                                            </asp:Panel>
                                                        </div>
                                                    </td>
                                                </tr>
                                            </table>

                                        </telerik:RadAjaxPanel>
                                    </ContentTemplate>
                                </telerik:RadPanelItem>
                                <telerik:RadPanelItem runat="server" Text="Manage Borrowers List">
                                    <ContentTemplate>
                                        <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel4" runat="server">
                                        </telerik:RadAjaxLoadingPanel>

                                        <telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server" LoadingPanelID="RadAjaxLoadingPanel4">
                                            <table style="width: 100%; margin-left: 10px">
                                                <tr>
                                                    <td>


                                                        <p>
                                                            <asp:Label ID="Label6" runat="server" Visible="false"></asp:Label>
                                                        </p>

                                                        <!-- BEGIN FORM-->

                                                        <table>
                                                            <tr>
                                                                <td class="newclass-email">
                                                                    <label>Name</label></td>
                                                                <td>
                                                                    <telerik:RadTextBox ID="txtBorrowerName" runat="server" Skin="Web20" class="form-control">
                                                                    </telerik:RadTextBox>

                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="newclass-email">
                                                                    <label>Region</label></td>
                                                                <td>
                                                                    <telerik:RadDropDownList ID="ddlBorrowerRegion" runat="server" Skin="Web20">
                                                                    </telerik:RadDropDownList>
                                                            </tr>
                                                            <tr>
                                                                <td class="newclass-email">
                                                                    <label>Type</label></td>
                                                                <td>
                                                                    <telerik:RadTextBox ID="txtBorrowerType" runat="server" Skin="Web20" class="form-control">
                                                                    </telerik:RadTextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="newclass-email">
                                                                    <label>Grid</label>
                                                                </td>
                                                                <td>
                                                                    <telerik:RadTextBox ID="txtGrid" runat="server" Skin="Web20"></telerik:RadTextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="newclass-email">
                                                                    <label>Summit Credit</label>
                                                                </td>
                                                                <td>
                                                                    <telerik:RadTextBox ID="txtSummitCredit" runat="server" Skin="Web20"></telerik:RadTextBox>
                                                                </td>
                                                            </tr>
                                                            <asp:HiddenField ID="hdnBorrower" runat="server" />
                                                            <tr>
                                                                <td></td>
                                                                <td>
                                                                    <telerik:RadButton ID="btnBorrowerSave" class="form-control" Width="75px" runat="server" Text="Save" OnClick="btnBorrowerSave_Click" Skin="Web20"></telerik:RadButton>

                                                                    <telerik:RadButton ID="btnBorrowerClear" class="form-control" Width="75px" runat="server" Text="Clear" OnClick="btnBorrowerClear_Click" Skin="Web20"></telerik:RadButton>
                                                                </td>
                                                            </tr>
                                                        </table>

                                                        <br />
                                                        <div class="horizontal_scroll grdLoans_cnt">
                                                            <label id="Label7" runat="server" visible="false"></label>
                                                            <asp:Panel ID="Panel5" runat="server" Width="1250px" ScrollBars="Both">
                                                                <telerik:RadGrid ID="grdBorrowers" runat="server" Skin="Web20" AutoGenerateColumns="false" AllowSorting="True" AllowPaging="True" OnItemCommand="grdBorrower_ItemCommand" OnSortCommand="grdBorrower_SortCommand" AllowFilteringByColumn="True">
                                                                    <MasterTableView Width="100%" CommandItemDisplay="Top" AllowFilteringByColumn="True">
                                                                        <CommandItemSettings ShowAddNewRecordButton="false" />
                                                                        <PagerStyle Mode="NextPrevNumericAndAdvanced"></PagerStyle>
                                                                        <CommandItemSettings ShowExportToWordButton="true" ShowExportToCsvButton="true"></CommandItemSettings>
                                                                        <Columns>
                                                                            <telerik:GridBoundColumn DataField="Name" HeaderText="Name">
                                                                            </telerik:GridBoundColumn>
                                                                            <telerik:GridBoundColumn DataField="Region" HeaderText="Region">
                                                                            </telerik:GridBoundColumn>
                                                                            <telerik:GridBoundColumn DataField="Type" HeaderText="Type">
                                                                            </telerik:GridBoundColumn>
                                                                            <telerik:GridBoundColumn DataField="Grid" HeaderText="Grid">

                                                                            </telerik:GridBoundColumn>
                                                                            <telerik:GridBoundColumn DataField="SummitCreditEntity" HeaderText="Summit Credit Entity"></telerik:GridBoundColumn>
                                                                            <telerik:GridTemplateColumn HeaderText="Action">
                                                                                <ItemTemplate>
                                                                                    <telerik:RadButton ID="btnBEdit" Skin="Web20" runat="server" Text="Edit" CommandName="EditB" CommandArgument='<%# Eval("ID") %>'></telerik:RadButton>
                                                                                    <asp:Button ID="btnBRemove" Text="Delete" runat="server" OnClientClick="javascript:if(!confirm('Are you sure want to delete this record?')){return false;}" CommandName="RemoveB" CommandArgument='<%# Eval("ID") %>' />
                                                                                </ItemTemplate>
                                                                            </telerik:GridTemplateColumn>
                                                                        </Columns>
                                                                    </MasterTableView>
                                                                </telerik:RadGrid>
                                                            </asp:Panel>
                                                        </div>
                                                    </td>
                                                </tr>
                                            </table>

                                        </telerik:RadAjaxPanel>
                                    </ContentTemplate>
                                </telerik:RadPanelItem>
                                <telerik:RadPanelItem runat="server" Text="View Duplicate Loans">
                                    <ContentTemplate>
                                        <table style="width: 100%; margin-left: 10px">
                                            <tr>
                                                <td>
                                                    <asp:Panel ID="pnlDuplicate" runat="server" Width="1280px" ScrollBars="Both" Height="400px">
                                                        <telerik:RadGrid ID="grdDuplicateLoans" runat="server" Skin="Web20" AllowSorting="true" AutoGenerateColumns="false" OnSortCommand="grdDuplicateLoans_SortCommand" AllowFilteringByColumn="True" OnItemCommand="grdDuplicateLoans_ItemCommand" Width="1250px">
                                                            <ClientSettings>
                                                                <Scrolling AllowScroll="true" UseStaticHeaders="true" />
                                                            </ClientSettings>
                                                            <MasterTableView Width="100%" CommandItemDisplay="Top" AllowFilteringByColumn="True">
                                                                <CommandItemSettings ShowAddNewRecordButton="false" />
                                                                <HeaderStyle Width="110px" />
                                                                <PagerStyle Mode="NextPrevNumericAndAdvanced"></PagerStyle>
                                                                <CommandItemSettings ShowExportToWordButton="true" ShowExportToCsvButton="true"></CommandItemSettings>
                                                                <Columns>
                                                                    <telerik:GridBoundColumn DataField="ID" HeaderText="ID">
                                                                    </telerik:GridBoundColumn>
                                                                    <telerik:GridBoundColumn DataField="CodeName" HeaderText="CodeName">
                                                                    </telerik:GridBoundColumn>

                                                                    <telerik:GridBoundColumn DataField="Borrower" HeaderText="Borrower">
                                                                    </telerik:GridBoundColumn>
                                                                    <telerik:GridBoundColumn DataField="Country" HeaderText="Country">
                                                                    </telerik:GridBoundColumn>
                                                                    <telerik:GridBoundColumn DataField="Sector" HeaderText="Sector">
                                                                    </telerik:GridBoundColumn>
                                                                    <telerik:GridBoundColumn DataField="Signing_Date" HeaderText="Signing_Date">
                                                                    </telerik:GridBoundColumn>
                                                                    <telerik:GridBoundColumn DataField="Maturity_Date" HeaderText="Maturity_Date">
                                                                    </telerik:GridBoundColumn>
                                                                    <telerik:GridBoundColumn DataField="FixedOrFloating" HeaderText="FixedOrFloating">
                                                                    </telerik:GridBoundColumn>
                                                                    <telerik:GridBoundColumn DataField="Margin" HeaderText="Margin">
                                                                    </telerik:GridBoundColumn>
                                                                    <telerik:GridBoundColumn DataField="Currency" HeaderText="Currency">
                                                                    </telerik:GridBoundColumn>
                                                                    <telerik:GridBoundColumn DataField="CouponFrequency" HeaderText="CouponFrequency">
                                                                    </telerik:GridBoundColumn>
                                                                    <telerik:GridBoundColumn DataField="FacilitySize" HeaderText="FacilitySize">
                                                                    </telerik:GridBoundColumn>
                                                                    <telerik:GridCheckBoxColumn DataField="Bilateral" HeaderText="Bilateral">
                                                                        <HeaderStyle Width="100px" />
                                                                    </telerik:GridCheckBoxColumn>
                                                                    <telerik:GridBoundColumn DataField="Amortizing" HeaderText="Amortizing">
                                                                    </telerik:GridBoundColumn>

                                                                </Columns>
                                                            </MasterTableView>
                                                        </telerik:RadGrid>
                                                    </asp:Panel>
                                                </td>
                                            </tr>
                                        </table>
                                    </ContentTemplate>
                                </telerik:RadPanelItem>
                                <telerik:RadPanelItem runat="server" Text="Manage Currency">
                                    <ContentTemplate>
                                        <table style="width: 100%; margin-left: 10px;">
                                            <tr>
                                                <td>

                                                    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel5" runat="server">
                                                    </telerik:RadAjaxLoadingPanel>
                                                    <p>
                                                        <asp:Label ID="Label8" runat="server" Visible="false"></asp:Label>
                                                    </p>
                                                    <table>
                                                        <tr>
                                                            <td class="newclass-users">
                                                                <label>Name</label></td>
                                                            <td>
                                                                <telerik:RadTextBox ID="txtCurrency" runat="server" Skin="Web20" class="form-control">
                                                                </telerik:RadTextBox>
                                                            </td>
                                                        </tr>

                                                        <tr>
                                                            <td></td>
                                                            <td align="left">
                                                                <telerik:RadButton ID="btnCurrencySave" Width="75px" runat="server" Skin="Web20" Text="Save" OnClick="btnCurrencySave_Click"></telerik:RadButton>
                                                                <telerik:RadButton ID="btnCurrencyClear" Width="75px" runat="server" Text="Clear" OnClick="btnCurrencyClear_Click" Skin="Web20"></telerik:RadButton>
                                                            </td>
                                                        </tr>
                                                        <asp:HiddenField ID="hdnCurrency" runat="server" />
                                                        <tr>
                                                            <td colspan="2">
                                                                <asp:Label ID="Label9" runat="server" Visible="False" ForeColor="Red"></asp:Label></td>
                                                        </tr>
                                                    </table>

                                                    <br />
                                                    <div class="horizontal_scroll grdLoans_cnt">
                                                        <asp:Panel ID="Panel6" runat="server" Width="1250px" ScrollBars="Both">
                                                            <telerik:RadGrid ID="grdCurrency" runat="server" Skin="Web20" AutoGenerateColumns="false" AllowSorting="True" AllowPaging="True" OnItemCommand="grdCurrency_ItemCommand" OnSortCommand="grdCurrency_SortCommand" AllowFilteringByColumn="True">
                                                                <MasterTableView Width="100%" CommandItemDisplay="Top" AllowFilteringByColumn="True">
                                                                    <CommandItemSettings ShowAddNewRecordButton="false" />
                                                                    <PagerStyle Mode="NextPrevNumericAndAdvanced"></PagerStyle>
                                                                    <CommandItemSettings ShowExportToWordButton="true" ShowExportToCsvButton="true"></CommandItemSettings>
                                                                    <Columns>
                                                                        <telerik:GridBoundColumn DataField="Currancy" HeaderText="Currency">
                                                                        </telerik:GridBoundColumn>

                                                                        <telerik:GridTemplateColumn AllowFiltering="false">
                                                                            <ItemTemplate>
                                                                                <telerik:RadButton ID="btnCurrencyEdit" Skin="Web20" runat="server" Text="Edit" CommandName="EditCurrency" CommandArgument='<%# Eval("ID") %>'></telerik:RadButton>

                                                                                <asp:Button ID="btnDelete" Text="Delete" runat="server" OnClientClick="javascript:if(!confirm('Are you sure want to delete record?')){return false;}" CommandName="RemoveCurrency" CommandArgument='<%# Eval("ID") %>' />
                                                                            </ItemTemplate>

                                                                        </telerik:GridTemplateColumn>
                                                                    </Columns>
                                                                </MasterTableView>
                                                            </telerik:RadGrid>
                                                            <telerik:RadWindow ID="confirmQuoteDelete" runat="server" VisibleTitlebar="false" VisibleStatusbar="false"
                                                                Modal="true" Behaviors="None" Height="150px" Width="300px">
                                                                <ContentTemplate>
                                                                    <div style="padding-left: 30px; padding-top: 20px; width: 200px; float: left; color: black;">
                                                                        <asp:Label ID="Label13" Font-Size="14px" Text="Are you sure you want to delete record?"
                                                                            runat="server" Skin="Web20"></asp:Label>
                                                                        <br />
                                                                        <br />
                                                                        <telerik:RadButton ID="rbQuoteSuccess" runat="server" Text="Yes" AutoPostBack="false"
                                                                            OnClientClicked="confirmResult" Skin="Web20">
                                                                        </telerik:RadButton>
                                                                        <telerik:RadButton ID="rbQuoteFailure" runat="server" Text="No" AutoPostBack="false"
                                                                            OnClientClicked="confirmResult" Skin="Web20">
                                                                        </telerik:RadButton>
                                                                    </div>
                                                                </ContentTemplate>
                                                            </telerik:RadWindow>
                                                        </asp:Panel>
                                                    </div>
                                                </td>
                                            </tr>
                                        </table>
                                    </ContentTemplate>
                                </telerik:RadPanelItem>
                            </Items>
                        </telerik:RadPanelBar>
                    </telerik:RadPageView>
                </telerik:RadMultiPage>
            </td>
        </tr>
    </table>



    <script type="text/javascript">
        function showConfirmRadWindow(sender, args) {
            $find("<%=confirmWindow.ClientID %>").show();
            $find("<%=RadButtonYes.ClientID %>").focus();
            args.set_cancel(true);
        }
        function confirmResult(sender, args) {
            var oWnd = $find("<%=confirmWindow.ClientID %>");
            oWnd.close();
            if (sender.get_text() == "Yes") {
                var masterTable = $find("<%=grdAccounts.ClientID%>").get_masterTableView();
                var btnResetPassword = masterTable.get_dataItems()[0].findControl('btnResetPassword');
                btnResetPassword.click();
            }
        }

        function showConfirmRadWindowForExport(sender, args) {

            if ($("#ctl00_cphBody_ddlImportExport").val() == "Export") {
                $("#ctl00_cphBody_pnlBar1_i1_rwExportYesNo_C_Label27").text("You wish to Export " + $("#ctl00_cphBody_pnlBar1_i1_ddlImportType").val() + " table.  Press Yes to proceed.");
            }
            else
                $("#ctl00_cphBody_pnlBar1_i1_rwExportYesNo_C_Label27").text("You wish to Import " + $("#ctl00_cphBody_pnlBar1_i1_ddlImportType").val() + " table.  Press Yes to proceed.");

            $find("<%=rwExportYesNo.ClientID %>").show();
            $find("<%=rbExportYes.ClientID %>").focus();
            args.set_cancel(true);

        }
        function confirmResultExport(sender, args) {
            var oWnd = $find("<%=rwExportYesNo.ClientID %>");
            oWnd.close();
            if (sender.get_text() == "Yes") {
                $find("<%=btnExport.ClientID %>").click();
            }
        }
    </script>


    <script type="text/javascript">
        function showConfirmQuoteDelete(sender, args, arg1) {
            alert(arg1);
            $find("<%=confirmQuoteDelete.ClientID %>").show();
            $find("<%=rbQuoteSuccess.ClientID %>").focus();
            args.set_cancel(true);
        }
        function confirmResult(sender, args) {
            var oWnd = $find("<%=confirmQuoteDelete.ClientID %>");
            oWnd.close();
            if (sender.get_text() == "Yes") {
                var masterTable = $find("<%=grdCurrency.ClientID%>").get_masterTableView();
                var btnDelete = masterTable.get_dataItems()[0].findControl('btnCurrencyRemove');
                btnDelete.click();
            }
        }


        function showConfirmRatingDelete(sender, args) {
            $find("<%=confirmRatingDelete.ClientID %>").show();
            $find("<%=rbRatingSuccess.ClientID %>").focus();
            args.set_cancel(true);
        }
        function confirmRatingResult(sender, args) {
            var oWnd = $find("<%=confirmRatingDelete.ClientID %>");
            oWnd.close();
            if (sender.get_text() == "Yes") {
                var masterTable = $find("<%=grdCreditRatings.ClientID%>").get_masterTableView();
                var btnDelete = masterTable.get_dataItems()[0].findControl('btnCreditRatingRemove');
                btnDelete.click();
            }
        }


        function showConfirmCountryDelete(sender, args) {
            $find("<%=confirmCountryDelete.ClientID %>").show();
            $find("<%=rbCountrySuccess.ClientID %>").focus();
            args.set_cancel(true);
        }
        function confirmCountryResult(sender, args) {
            var oWnd = $find("<%=confirmCountryDelete.ClientID %>");
            oWnd.close();
            if (sender.get_text() == "Yes") {
                var masterTable = $find("<%=grdCountry.ClientID%>").get_masterTableView();
                var btnDelete = masterTable.get_dataItems()[0].findControl('btnCountryRemove');
                btnDelete.click();
            }
        }


        function showConfirmCounterPartyDelete(sender, args) {
            $find("<%=confirmCounterPartyDelete.ClientID %>").show();
            $find("<%=rbCounterPartySuccess.ClientID %>").focus();
            args.set_cancel(true);
        }
        function confirmCounterPartyResult(sender, args) {
            var oWnd = $find("<%=confirmCounterPartyDelete.ClientID %>");

            oWnd.close();
            if (sender.get_text() == "Yes") {

                var masterTable = $find("<%=grdCounterParty.ClientID%>").get_masterTableView();
                var btnDelete = masterTable.get_dataItems()[0].findControl('btnCPRemove');
                btnDelete.click();
            }
        }
    </script>
</asp:Content>
