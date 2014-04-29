<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" Theme="Default" AutoEventWireup="true" CodeBehind="ShowLog.aspx.cs" Inherits="LoanPricerWebBased.ShowLog" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" runat="server">
    <asp:Button ID="btnGeneratePDF" runat="server" OnClick="btnGeneratePDF_Click" Text="Generate PDF" />

    <asp:GridView ID="grdLogs" runat="server" SkinID="GridView">
    </asp:GridView>

</asp:Content>
