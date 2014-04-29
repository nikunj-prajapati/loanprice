<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MessageBox.ascx.cs"
    Inherits="LoanPricerWebBased.MessageBox" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Button ID="btnMessageBox" runat="server" Style="display: none;" />
<ajaxToolkit:ModalPopupExtender ID="mpeMessageBox" runat="server" BackgroundCssClass="LGBack"
    PopupControlID="pnlMessageBox" TargetControlID="btnMessageBox" BehaviorID="mpeMessageBox">
</ajaxToolkit:ModalPopupExtender>
<asp:Panel ID="pnlMessageBox" runat="server" Width="350px" CssClass="MsgBox rounded_corners"
    Style="display: block">
    <div class="cross" onclick="$find('mpeMessageBox').hide();" title="Close">
        X
    </div>
    <asp:UpdatePanel ID="pnl" runat="server" UpdateMode="Always">
        <ContentTemplate>
            <asp:Panel ID="pnlBody" runat="server" CssClass="MsgBody" DefaultButton="btnOk">
                <h2>
                    <asp:Label ID="lblHeader" runat="server">Message</asp:Label></h2>
                <div>
                    <asp:Label ID="lblMessage" runat="server" CssClass="msg_lbl" Text="Message will come here need to big long"></asp:Label>
                </div>
                <div>
                    <asp:Button ID="btnOk" runat="server" Text="OK" CssClass="msg_btn" OnClientClick="$find('mpeMessageBox').hide(); return false;" />
                </div>

                <script type="text/javascript">
                    function TimerClose() {
                        $find('mpeMessageBox').hide();
                        window.scrollTo(0, 0);
                    }
                    var prm = Sys.WebForms.PageRequestManager.getInstance();
                    prm.add_endRequest(EndRequest);

                    function EndRequest(sender, args) {
                        setTimeout(TimerClose, 1000);
                    }
                </script>

            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Panel>

<script type="text/javascript" language="javascript">
    function ShowMessage(header, message) {
        $('#<%= lblHeader.ClientID %>').html(header);
        $('#<%= lblMessage.ClientID %>').html(message);
        $find('mpeMessageBox').show();
    }
    function ShowMessageWithAutoHide(header, message) {
        $('#<%= lblHeader.ClientID %>').html(header);
        $('#<%= lblMessage.ClientID %>').html(message);
        $find('mpeMessageBox').show();
        SetOut();
    }
</script>

