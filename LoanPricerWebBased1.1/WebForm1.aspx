<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="LoanPricerWebBased.WebForm1" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" runat="server">
     <%--<telerik:RadScriptManager runat="server" ID="RadScriptManager1" />--%>
   <%-- <telerik:RadDatePicker ID="RadDatePicker1" runat="server" MinDate="2006/1/1" Width="150px">
        <DateInput runat="server" ID="DateInput">
            <ClientEvents OnLoad="onLoad"></ClientEvents>
        </DateInput>
    </telerik:RadDatePicker>--%>
    <telerik:RadDropDownList ID="ddl" runat="server">
        <Items>

            <telerik:DropDownListItem Text="1" />
            <telerik:DropDownListItem Text="2" />
        </Items>
    </telerik:RadDropDownList>
</asp:Content>
