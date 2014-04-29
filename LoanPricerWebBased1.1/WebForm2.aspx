<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm2.aspx.cs" Inherits="LoanPricerWebBased.WebForm2" %>

<!DOCTYPE html>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
     <telerik:RadScriptManager runat="server" ID="RadScriptManager1" />
    <telerik:RadDatePicker ID="RadDatePicker1" runat="server" MinDate="2006/1/1" Width="150px">
        <DateInput runat="server" ID="DateInput">
            <ClientEvents OnLoad="onLoad"></ClientEvents>
        </DateInput>
    </telerik:RadDatePicker>
    <telerik:RadDropDownList ID="ddl" runat="server">
        <Items>

            <telerik:DropDownListItem Text="1" />
            <telerik:DropDownListItem Text="1" />
        </Items>
    </telerik:RadDropDownList>
    </div>
    </form>
</body>
</html>
