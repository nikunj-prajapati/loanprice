<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="LoanPricerWebBased.Default" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%--<%@ Register TagPrefix="telerik" Namespace="Telerik.Charting" Assembly="Telerik.Web.UI" %>--%>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <!--Script references. -->
    <!--Reference the jQuery library. -->
    <script src="Scripts/jquery-1.6.4.min.js"></script>
    <!--Reference the SignalR library. -->
    <%--  <script src="Scripts/jquery.signalR-2.0.0.min.js"></script>
    <script src='<%: ResolveClientUrl("~/signalr/hubs") %>'></script>
    <script src="Scripts/DuplexLoansAndQuotes.js"></script>--%>

    <script src="Scripts/jquery.numeric.js" type="text/javascript"></script>

    <%--<script src="Scripts/Calculations.js" type="text/javascript"></script>--%>

    <style>
        
    </style>
    <script type="text/jscript">

        function funCheckDuplicate() {
            function Confirm() {
                var confirm_value = document.createElement("INPUT");
                confirm_value.type = "hidden";
                confirm_value.name = "confirm_value";
                if (confirm("Do you want to save data?")) {
                    confirm_value.value = "Yes";
                } else {
                    confirm_value.value = "No";
                }
                document.forms[0].appendChild(confirm_value);
            }

        }
        var imgUrl = null;
        function confirmCallBackFn(arg) {
            if (arg == true) {
                //alert('Y');

                $('#<%= hdnQuoteTemp.ClientID %>').val("Y");

                doajax();
            }
            else {
                // alert('N');
                $('#<%= hdnQuoteTemp.ClientID %>').val("N");
            }

        }

        function doajax() {
            var notional = $('#<%=txtBoxNotional1.ClientID%>').val();
            notional = notional.replace(/,/g, '');

            var checkbox1 = $("#<%= chkBoxTradedA.ClientID %>");
            var domCheckbox1 = checkbox1[0];
            var isChecked = domCheckbox1.checked;
            $.ajax(
            {
                url: "Default.aspx/InsertRecord?flag=1",
                data: "{BidPrice:" + $('#<%= txtBidPriceA.ClientID %>').val() + ",BidSpread:" + $('#<%=txtBidSpreadA.ClientID%>').val() + ",CounterParty:" + $('#<%=ddlCounterPartyA.ClientID%>').val() + ",LoanName:" + $('#<%=txtLoanNameA.ClientID%>').val() + ",OfferPrice:" + $('#<%=txtOfferPriceA.ClientID%>').val() + ",OfferSpread:" + $('#<%=txtOfferSpreadA.ClientID%>').val() + ",BoxTrade:" + isChecked + ",CFLoanA:" + $('#<%=txtBoxCouponFrequencyLoanA.ClientID%>').val() + ",CurLoanA:" + $('#<%=txtBoxCurrencyLoanA.ClientID%>').val() + ",IntRateA:" + $('#<%=txtBoxInterestRateA.ClientID%>').val() + ",DisMarA:" + $('#<%=txtDiscountMarginA.ClientID%>').val() + ",MatDate:" + $('#<%=txtBoxMaturityDateA.ClientID%>').val() + ",TradeDate:" + $('#<%=txtBoxTradeDate1.ClientID%>').val() + ",AvgLifeDisc:" + $('#<%=txtAveLifDiscA.ClientID%>').val() + ",AvgLifeRiskDisc:" + $('#<%=txtAveLifRiskyDiscA.ClientID%>').val() + ",AvgLifeNonDisc:" + $('#<%=txtAveLifNonDiscA.ClientID%>').val() + ",SettlementDate:" + $('#<%=txtBoxSettlementDate1.ClientID%>').val() + ",Margin:" + $('#<%=txtBoxInterestRateA.ClientID%>').val() + ",Notional:" + notional + ",AverageLife:" + $('#<%=txtAvgLifeA.ClientID%>').val() + "}",
                dataType: "json",
                success: function (msg) {
                    if (msg.d) {
                        alert("Sucess");
                    }
                }
            });
        }

        function confirmCallBackFn2(arg) {
            if (arg == true) {
                //alert('Y');
                $('#<%= hdnQuoteTemp.ClientID %>').val("Y");
                doajax2();
            }
            else {
                // alert('N');
                $('#<%= hdnQuoteTemp.ClientID %>').val("N");
            }

        }

        function doajax2() {
            var notional = $('#<%=txtBoxNotional2.ClientID%>').val();
            notional = notional.replace(/,/g, '');
            var checkbox1 = $("#<%= chkBoxTradedB.ClientID %>");
            var domCheckbox1 = checkbox1[0];
            var isChecked = domCheckbox1.checked;
            $.ajax(
            {
                url: "Default.aspx/InsertRecord?flag=2",
                data: "{BidPrice:" + $('#<%= txtBoxBidPriceB.ClientID %>').val() + ",BidSpread:" + $('#<%=txtBoxBidSpreadB.ClientID%>').val() + ",CounterParty:" + $('#<%=ddlCounterPartyB.ClientID%>').val() + ",LoanName:" + $('#<%=txtLoanNameB.ClientID%>').val() + ",OfferPrice:" + $('#<%=txtBoxOfferPriceB.ClientID%>').val() + ",OfferSpread:" + $('#<%=txtBoxOfferSpreadB.ClientID%>').val() + ",BoxTrade:" + isChecked + ",CFLoanA:" + $('#<%=txtBoxCouponFrequencyLoanB.ClientID%>').val() + ",CurLoanA:" + $('#<%=txtCurrencyB.ClientID%>').val() + ",IntRateA:" + $('#<%=txtBoxIRRB.ClientID%>').val() + ",DisMarA:" + $('#<%=txtDiscountMarginB.ClientID%>').val() + ",MatDate:" + $('#<%=txtBoxMaturityDateB.ClientID%>').val() + ",TradeDate:" + $('#<%=txtBoxTradeDate2.ClientID%>').val() + ",AvgLifeDisc:" + $('#<%=txtBoxAveLifDiscB.ClientID%>').val() + ",AvgLifeRiskDisc:" + $('#<%=txtBoxAveLifRiskyDiscB.ClientID%>').val() + ",AvgLifeNonDisc:" + $('#<%=txtBoxAveLifNonDiscB.ClientID%>').val() + ",SettlementDate:" + $('#<%=txtBoxSettlementDate2.ClientID%>').val() + ",Margin:" + $('#<%=txtInterestRateB.ClientID%>').val() + ",Notional:" + notional + ",AverageLife:" + $('#<%=txtAvgLifeB.ClientID%>').val() + "}",
                dataType: "json",
                success: function (msg) {
                    if (msg.d) {
                        alert("Sucess");
                    }
                }
            });
        }


        function confirmCallBackFn3(arg) {
            if (arg == true) {
                //alert('Y');
                $('#<%= hdnQuoteTemp.ClientID %>').val("Y");
                doajax3();
            }
            else {
                // alert('N');
                $('#<%= hdnQuoteTemp.ClientID %>').val("N");
            }

        }

        function doajax3() {
            var notional = $('#<%=txtBoxNotional3.ClientID%>').val();
            notional = notional.replace(/,/g, '');
            var checkbox1 = $("#<%= chkBoxTradedC.ClientID %>");
            var domCheckbox1 = checkbox1[0];
            var isChecked = domCheckbox1.checked;
            $.ajax(
            {
                url: "Default.aspx/InsertRecord?flag=3",
                data: "{BidPrice:" + $('#<%= txtBoxBidPriceC.ClientID %>').val() + ",BidSpread:" + $('#<%=txtBoxBidSpreadC.ClientID%>').val() + ",CounterParty:" + $('#<%=ddlCounterPartyC.ClientID%>').val() + ",LoanName:" + $('#<%=txtLoanNameC.ClientID%>').val() + ",OfferPrice:" + $('#<%=txtBoxOfferPriceC.ClientID%>').val() + ",OfferSpread:" + $('#<%=txtBoxOfferSpreadC.ClientID%>').val() + ",BoxTrade:" + isChecked + ",CFLoanA:" + $('#<%=txtBoxCouponFrequencyLoanC.ClientID%>').val() + ",CurLoanA:" + $('#<%=txtCurrencyC.ClientID%>').val() + ",IntRateA:" + $('#<%=txtBoxIRRC.ClientID%>').val() + ",DisMarA:" + $('#<%=txtDiscountMarginC.ClientID%>').val() + ",MatDate:" + $('#<%=txtMaturityDateC.ClientID%>').val() + ",TradeDate:" + $('#<%=txtBoxTradeDate3.ClientID%>').val() + ",AvgLifeDisc:" + $('#<%=txtBoxAveLifDiscC.ClientID%>').val() + ",AvgLifeRiskDisc:" + $('#<%=txtBoxAveLifRiskyDiscC.ClientID%>').val() + ",AvgLifeNonDisc:" + $('#<%=txtBoxAveLifNonDiscC.ClientID%>').val() + ",SettlementDate:" + $('#<%=txtBoxSettlementDate3.ClientID%>').val() + ",Margin:" + $('#<%=txtInterestRateC.ClientID%>').val() + ",Notional:" + notional + ",AverageLife:" + $('#<%=txtAvgLifeC.ClientID%>').val() + "}",
                dataType: "json",
                success: function (msg) {
                    if (msg.d) {
                        alert("Sucess");
                    }
                }
            });
        }

        function Showalert() {
            window.alert('Please check values for Trade.');
        }

        function LoanAddalert() {
            window.alert('Loan Added');
        }
    </script>

    <script type="text/javascript">

        function CheckLoan() {
            if ($('#<%= hfLoanID.ClientID %>').val().trim() == "") {
                return true;
            }
            else {
                return confirm('This will overwrite the existing loan.Are you sure?');
            }
        }
    </script>

    <link rel="Stylesheet" type="text/css" href="CSS/uploadify.css" />

    <script type="text/javascript" src="scripts/jquery.uploadify.js"></script>

    <script type="text/javascript">
        function getSelected(id) {
            alert(id);
        }

        $(document).ready(function () {
            $('#ctl00_cphBody_txtBidPriceA').numeric().keypress(function (e) {
                // if enter is pressed

                if (e.which == 13) {
                    BidSpreadCalculationA($(this), e);
                    BidPriceDiscountMarginCalculationA($(this), e);
                }
            });
        });
        $(document).ready(function () {
            $('#ctl00_cphBody_txtBoxBidPriceB').numeric().keypress(function (e) {
                // if enter is pressed

                if (e.which == 13) {
                    BidSpreadCalculationB($(this), e);
                    BidPriceDiscountMarginCalculationB($(this), e);
                }
            });
        });
        $(document).ready(function () {
            $('#ctl00_cphBody_txtBoxBidPriceC').numeric().keypress(function (e) {
                // if enter is pressed
                if (e.which == 13) {

                    BidSpreadCalculationC($(this), e);
                    BidPriceDiscountMarginCalculationC($(this), e);
                }
            });
        });


        function BidSpreadCalculationA(sender, e) {

            var bidPrice = parseFloat($(sender).val());
            if (isNaN(bidPrice)) {
                bidPrice = 0;
            }
            var averageLifeNonDisc = parseFloat($('#ctl00_cphBody_txtAveLifNonDiscA').val());
            if (isNaN(averageLifeNonDisc)) {
                averageLifeNonDisc = 1;
            }
            var discountMargin = parseFloat($('#ctl00_cphBody_txtBoxInterestRateA').val());
            if (isNaN(discountMargin)) {
                discountMargin = 0;
            }

            //var computedBidSpread = (((100 - bidPrice) * 360 * 100) / (averageLifeNonDisc * 365.25)) + discountMargin;
            //  var computedBidSpread = ((360 * 100 * (100 - bidPrice)) / (365.25 * averageLifeNonDisc)) + discountMargin;
            var computedBidSpread = ((360 * 100 * (100 - bidPrice)) / (365.5 * averageLifeNonDisc)) + discountMargin;
            $('#ctl00_cphBody_txtBidSpreadA').val(computedBidSpread.toFixed(2));
            e.preventDefault();

        }

        function BidSpreadCalculationB(sender, e) {


            var bidPrice = parseFloat($(sender).val());
            if (isNaN(bidPrice)) {
                bidPrice = 0;
            }
            var averageLifeNonDisc = parseFloat($('#ctl00_cphBody_txtBoxAveLifNonDiscB').val());
            if (isNaN(averageLifeNonDisc)) {
                averageLifeNonDisc = 1;
            }
            var discountMargin = parseFloat($('#ctl00_cphBody_txtInterestRateB').val());
            if (isNaN(discountMargin)) {
                discountMargin = 0;
            }

            //   var computedBidSpread = (((100 - bidPrice) * 360 * 100) / (averageLifeNonDisc * 365.25)) + discountMargin;
            //  var computedBidSpread = ((360 * 100 * (100 - bidPrice)) / (365.25 * averageLifeNonDisc)) + discountMargin; 
            var computedBidSpread = ((360 * 100 * (100 - bidPrice)) / (365.5 * averageLifeNonDisc)) + discountMargin;//change on 02-04 given by ajay
            $('#ctl00_cphBody_txtBoxBidSpreadB').val(computedBidSpread.toFixed(2));
            e.preventDefault();
        }
        function BidSpreadCalculationC(sender, e) {


            var bidPrice = parseFloat($(sender).val());
            if (isNaN(bidPrice)) {
                bidPrice = 0;
            }
            var averageLifeNonDisc = parseFloat($('#ctl00_cphBody_txtBoxAveLifNonDiscC').val());
            if (isNaN(averageLifeNonDisc)) {
                averageLifeNonDisc = 1;
            }
            var discountMargin = parseFloat($('#ctl00_cphBody_txtInterestRateC').val());
            if (isNaN(discountMargin)) {
                discountMargin = 0;
            }

            // var computedBidSpread = (((100 - bidPrice) * 360 * 100) / (averageLifeNonDisc * 365.25)) + discountMargin;
            // var computedBidSpread = ((360 * 100 * (100 - bidPrice)) / (365.25 * averageLifeNonDisc)) + discountMargin;
            var computedBidSpread = ((360 * 100 * (100 - bidPrice)) / (365.5 * averageLifeNonDisc)) + discountMargin;//change on 02-04 given by ajay
            $('#ctl00_cphBody_txtBoxBidSpreadC').val(computedBidSpread.toFixed(2));
            e.preventDefault();
        }

        // Nik 1503  //

        function BidPriceDiscountMarginCalculationA(sender, e) {

            var bidPrice = parseFloat($(sender).val());
            if (isNaN(bidPrice)) {
                bidPrice = 0;
            }
            var averageLifeNonDisc = parseFloat($('#ctl00_cphBody_txtAveLifNonDiscA').val());
            if (isNaN(averageLifeNonDisc)) {
                averageLifeNonDisc = 1;
            }
            var averageLife = parseFloat($('#ctl00_cphBody_txtAvgLifeA').val());
            if (isNaN(averageLife)) {
                averageLife = 0;
            }
            var discountMargin = parseFloat($('#ctl00_cphBody_txtBoxInterestRateA').val());
            if (isNaN(discountMargin)) {
                discountMargin = 0;
            }
            var bidSpread = parseFloat($('#ctl00_cphBody_txtBidSpreadA').val());
            //var computedBidSpread = (((100 - bidPrice) * 360 * 100) / (averageLifeNonDisc * 365.25)) + discountMargin;
            //var computedBidSpread = ((360 * 100 * (100 - bidPrice)) / (365.25 * averageLifeNonDisc)) + discountMargin;
            if (averageLife > 0) {
                // var computedDiscountMargin = (((100 - (100 - bidPrice)) / averageLifeNonDisc) * 100) + (10000 * bidSpread);
                var computedDiscountMargin = (((100 - bidPrice * 100) / averageLife) * 100) + ((bidSpread / 100) * 10000);
                $('#ctl00_cphBody_txtDiscountMarginA').val(computedDiscountMargin.toFixed(2));
                e.preventDefault();
            }


        }

        function BidPriceDiscountMarginCalculationB(sender, e) {

            var bidPrice = parseFloat($(sender).val());
            if (isNaN(bidPrice)) {
                bidPrice = 0;
            }
            var averageLife = parseFloat($('#ctl00_cphBody_txtAvgLifeB').val());
            if (isNaN(averageLife)) {
                averageLife = 0;
            }
            var averageLifeNonDisc = parseFloat($('#ctl00_cphBody_txtBoxAveLifNonDiscB').val());
            if (isNaN(averageLifeNonDisc)) {
                averageLifeNonDisc = 1;
            }
            var discountMargin = parseFloat($('#ctl00_cphBody_txtInterestRateB').val());
            if (isNaN(discountMargin)) {
                discountMargin = 0;
            }
            var bidSpread = parseFloat($('#ctl00_cphBody_txtBoxBidSpreadB').val());
            //var computedBidSpread = (((100 - bidPrice) * 360 * 100) / (averageLifeNonDisc * 365.25)) + discountMargin;
            //var computedBidSpread = ((360 * 100 * (100 - bidPrice)) / (365.25 * averageLifeNonDisc)) + discountMargin;
            //var computedDiscountMargin = (((100 - (100 - bidPrice)) / averageLifeNonDisc) * 100) + (10000 * bidSpread);
            //$('#ctl00_cphBody_txtDiscountMarginB').val(computedDiscountMargin.toFixed(2));
            //e.preventDefault();
            if (averageLife > 0) {
                // var computedDiscountMargin = (((100 - (100 - bidPrice)) / averageLifeNonDisc) * 100) + (10000 * bidSpread);
                var computedDiscountMargin = (((100 - bidPrice * 100) / averageLife) * 100) + ((bidSpread / 100) * 10000);
                $('#ctl00_cphBody_txtDiscountMarginB').val(computedDiscountMargin.toFixed(2));
                e.preventDefault();
            }

        }

        function BidPriceDiscountMarginCalculationC(sender, e) {

            var bidPrice = parseFloat($(sender).val());
            if (isNaN(bidPrice)) {
                bidPrice = 0;
            }
            var averageLife = parseFloat($('#ctl00_cphBody_txtAvgLifeC').val());
            if (isNaN(averageLife)) {
                averageLife = 0;
            }
            var averageLifeNonDisc = parseFloat($('#ctl00_cphBody_txtBoxAveLifNonDiscC').val());
            if (isNaN(averageLifeNonDisc)) {
                averageLifeNonDisc = 1;
            }
            var discountMargin = parseFloat($('#ctl00_cphBody_txtInterestRateC').val());
            if (isNaN(discountMargin)) {
                discountMargin = 0;
            }
            var bidSpread = parseFloat($('#ctl00_cphBody_txtBoxBidSpreadC').val());
            //var computedBidSpread = (((100 - bidPrice) * 360 * 100) / (averageLifeNonDisc * 365.25)) + discountMargin;
            //var computedBidSpread = ((360 * 100 * (100 - bidPrice)) / (365.25 * averageLifeNonDisc)) + discountMargin;
            //var computedDiscountMargin = (((100 - (100 - bidPrice)) / averageLifeNonDisc) * 100) + (10000 * bidSpread);
            //$('#ctl00_cphBody_txtDiscountMarginC').val(computedDiscountMargin.toFixed(2));
            //e.preventDefault();
            if (averageLife > 0) {
                // var computedDiscountMargin = (((100 - (100 - bidPrice)) / averageLifeNonDisc) * 100) + (10000 * bidSpread);
                var computedDiscountMargin = (((100 - bidPrice * 100) / averageLife) * 100) + ((bidSpread / 100) * 10000);
                $('#ctl00_cphBody_txtDiscountMarginC').val(computedDiscountMargin.toFixed(2));
                e.preventDefault();
            }

        }

        //




        $(document).ready(function () {
            $('#ctl00_cphBody_txtBidSpreadA').numeric().keypress(function (e) {
                // if enter is pressed
                if (e.which == 13) {
                    BidPriceCalculationA($(this), e);
                    BidPriceDiscountMarginCalculationA($(this), e);
                }
            });
        });
        $(document).ready(function () {
            $('#ctl00_cphBody_txtBoxBidSpreadB').numeric().keypress(function (e) {
                // if enter is pressed
                if (e.which == 13) {
                    BidPriceCalculationB($(this), e);
                    BidPriceDiscountMarginCalculationB($(this), e);
                }
            });
        });
        $(document).ready(function () {
            $('#ctl00_cphBody_txtBoxBidSpreadC').numeric().keypress(function (e) {
                // if enter is pressed
                if (e.which == 13) {
                    BidPriceCalculationC($(this), e);
                    BidPriceDiscountMarginCalculationC($(this), e);
                }
            });
        });

        function BidPriceCalculationA(sender, e) {
            // When bidSpread text box changes please update bidPrice to equal
            // BidPrice = (100 - (((bidSpread - Discount_Margin) / (360/365.25) ) / (100/NonDiscountedAverageLife))) ;

            var bidSpread = parseFloat($(sender).val());
            if (isNaN(bidSpread)) {
                bidSpread = 0;
            }
            var averageLifeNonDisc = parseFloat($('#ctl00_cphBody_txtAveLifNonDiscA').val());
            if (isNaN(averageLifeNonDisc)) {
                averageLifeNonDisc = 1;
            }
            var discountMargin = parseFloat($('#ctl00_cphBody_txtBoxInterestRateA').val()); // change 02-04-2014 for discount margin removed and interest rate added
            if (isNaN(discountMargin)) {
                discountMargin = 0;
            }

            //var computedBidPrice = 100 - (((bidSpread - discountMargin) * (averageLifeNonDisc * 365.25)) / 360 / 100); Nik 15-03
            //  var computedBidPrice = 100 - (((bidSpread - discountMargin) * (365.25 * averageLifeNonDisc)) / (360 * 100));
            var computedBidPrice = 100 - (((bidSpread - discountMargin) * (365.25 * averageLifeNonDisc)) / (360 * 100)); // change on 02-04-2014 given by ajay

            $('#ctl00_cphBody_txtBidPriceA').val(computedBidPrice.toFixed(2));

            e.preventDefault();
        }

        function BidPriceCalculationB(sender, e) {
            // When bidSpread text box changes please update bidPrice to equal
            // BidPrice = (100 - (((bidSpread - Discount_Margin) / (360/365.25) ) / (100/NonDiscountedAverageLife))) ;

            var bidSpread = parseFloat($(sender).val());
            if (isNaN(bidSpread)) {
                bidSpread = 0;
            }
            var averageLifeNonDisc = parseFloat($('#ctl00_cphBody_txtBoxAveLifNonDiscB').val());
            if (isNaN(averageLifeNonDisc)) {
                averageLifeNonDisc = 1;
            }
            var discountMargin = parseFloat($('#ctl00_cphBody_txtInterestRateB').val());
            if (isNaN(discountMargin)) {
                discountMargin = 0;
            }

            // var computedBidPrice = 100 - (((bidSpread - discountMargin) * (averageLifeNonDisc * 365.25)) / 360 / 100);
            // var computedBidPrice = 100 - (((bidSpread - discountMargin) * (365.25 * averageLifeNonDisc)) / (360 * 100));
            var computedBidPrice = 100 - (((bidSpread - discountMargin) * (365.25 * averageLifeNonDisc)) / (360 * 100)); // change on 02-04-2014 given by ajay
            $('#ctl00_cphBody_txtBoxBidPriceB').val(computedBidPrice.toFixed(2));

            e.preventDefault();
        }

        function BidPriceCalculationC(sender, e) {
            // When bidSpread text box changes please update bidPrice to equal
            // BidPrice = (100 - (((bidSpread - Discount_Margin) / (360/365.25) ) / (100/NonDiscountedAverageLife))) ;

            var bidSpread = parseFloat($(sender).val());
            if (isNaN(bidSpread)) {
                bidSpread = 0;
            }
            var averageLifeNonDisc = parseFloat($('#ctl00_cphBody_txtBoxAveLifNonDiscC').val());
            if (isNaN(averageLifeNonDisc)) {
                averageLifeNonDisc = 1;
            }
            var discountMargin = parseFloat($('#ctl00_cphBody_txtInterestRateC').val());
            if (isNaN(discountMargin)) {
                discountMargin = 0;
            }

            // var computedBidPrice = 100 - (((bidSpread - discountMargin) * (averageLifeNonDisc * 365.25)) / 360 / 100);
            //   var computedBidPrice = 100 - (((bidSpread - discountMargin) * (365.25 * averageLifeNonDisc)) / (360 * 100));

            var computedBidPrice = 100 - (((bidSpread - discountMargin) * (365.25 * averageLifeNonDisc)) / (360 * 100)); // change on 02-04-2014 given by ajay
            $('#ctl00_cphBody_txtBoxBidPriceC').val(computedBidPrice.toFixed(2));

            e.preventDefault();
        }

        // Nik 1503 //

        function BidPriceDiscountMarginCalculationA(sender, e) {
            // When bidSpread text box changes please update bidPrice to equal
            // BidPrice = (100 - (((bidSpread - Discount_Margin) / (360/365.25) ) / (100/NonDiscountedAverageLife))) ;

            var bidSpread = parseFloat($(sender).val());
            if (isNaN(bidSpread)) {
                bidSpread = 0;
            }
            var averageLife = parseFloat($('#ctl00_cphBody_txtAvgLifeA').val());
            if (isNaN(averageLife)) {
                averageLife = 0;
            }
            var averageLifeNonDisc = parseFloat($('#ctl00_cphBody_txtAveLifNonDiscA').val());
            if (isNaN(averageLifeNonDisc)) {
                averageLifeNonDisc = 1;
            }
            var discountMargin = parseFloat($('#ctl00_cphBody_txtBoxInterestRateA').val()); // change 02-04
            if (isNaN(discountMargin)) {
                discountMargin = 0;
            }
            var bidPrice = parseFloat($('#ctl00_cphBody_txtBidPriceA').val());

            //var computedBidPrice = 100 - (((bidSpread - discountMargin) * (averageLifeNonDisc * 365.25)) / 360 / 100); Nik 15-03
            //var computedDiscountMargin = (((100 - (100 - bidSpread)) / averageLifeNonDisc) * 100) + (10000 * bidPrice);

            //$('#ctl00_cphBody_txtDiscountMarginA').val(computedDiscountMargin.toFixed(2));

            //e.preventDefault();
            if (averageLife > 0) {
                // var computedDiscountMargin = (((100 - (100 - bidPrice)) / averageLifeNonDisc) * 100) + (10000 * bidSpread);
                var computedDiscountMargin = (((100 - bidPrice * 100) / averageLife) * 100) + ((bidSpread / 100) * 10000);
                $('#ctl00_cphBody_txtDiscountMarginA').val(computedDiscountMargin.toFixed(2));
                e.preventDefault();
            }
        }

        function BidPriceDiscountMarginCalculationB(sender, e) {
            // When bidSpread text box changes please update bidPrice to equal
            // BidPrice = (100 - (((bidSpread - Discount_Margin) / (360/365.25) ) / (100/NonDiscountedAverageLife))) ;

            var bidSpread = parseFloat($(sender).val());
            if (isNaN(bidSpread)) {
                bidSpread = 0;
            }
            var averageLife = parseFloat($('#ctl00_cphBody_txtAvgLifeB').val());
            if (isNaN(averageLife)) {
                averageLife = 0;
            }
            var averageLifeNonDisc = parseFloat($('#ctl00_cphBody_txtBoxAveLifNonDiscB').val());
            if (isNaN(averageLifeNonDisc)) {
                averageLifeNonDisc = 1;
            }
            var discountMargin = parseFloat($('#ctl00_cphBody_txtInterestRateB').val());
            if (isNaN(discountMargin)) {
                discountMargin = 0;
            }
            var bidPrice = parseFloat($('#ctl00_cphBody_txtBoxBidPriceB').val());

            //var computedBidPrice = 100 - (((bidSpread - discountMargin) * (averageLifeNonDisc * 365.25)) / 360 / 100); Nik 15-03
            //var computedDiscountMargin = (((100 - (100 - bidSpread)) / averageLifeNonDisc) * 100) + (10000 * bidPrice);

            //$('#ctl00_cphBody_txtDiscountMarginB').val(computedDiscountMargin.toFixed(2));

            //e.preventDefault();
            if (averageLife > 0) {
                // var computedDiscountMargin = (((100 - (100 - bidPrice)) / averageLifeNonDisc) * 100) + (10000 * bidSpread);
                var computedDiscountMargin = (((100 - bidPrice * 100) / averageLife) * 100) + ((bidSpread / 100) * 10000);
                $('#ctl00_cphBody_txtDiscountMarginB').val(computedDiscountMargin.toFixed(2));
                e.preventDefault();
            }
        }

        function BidPriceDiscountMarginCalculationC(sender, e) {
            // When bidSpread text box changes please update bidPrice to equal
            // BidPrice = (100 - (((bidSpread - Discount_Margin) / (360/365.25) ) / (100/NonDiscountedAverageLife))) ;

            var bidSpread = parseFloat($(sender).val());
            if (isNaN(bidSpread)) {
                bidSpread = 0;
            }
            var averageLife = parseFloat($('#ctl00_cphBody_txtAvgLifeC').val());
            if (isNaN(averageLife)) {
                averageLife = 0;
            }
            var averageLifeNonDisc = parseFloat($('#ctl00_cphBody_txtBoxAveLifNonDiscC').val());
            if (isNaN(averageLifeNonDisc)) {
                averageLifeNonDisc = 1;
            }
            var discountMargin = parseFloat($('#ctl00_cphBody_txtInterestRateC').val());
            if (isNaN(discountMargin)) {
                discountMargin = 0;
            }
            var bidPrice = parseFloat($('#ctl00_cphBody_txtBoxBidPriceC').val());

            //var computedBidPrice = 100 - (((bidSpread - discountMargin) * (averageLifeNonDisc * 365.25)) / 360 / 100); Nik 15-03
            //var computedDiscountMargin = (((100 - (100 - bidSpread)) / averageLifeNonDisc) * 100) + (10000 * bidPrice);

            //$('#ctl00_cphBody_txtDiscountMarginC').val(computedDiscountMargin.toFixed(2));

            //e.preventDefault();
            if (averageLife > 0) {
                // var computedDiscountMargin = (((100 - (100 - bidPrice)) / averageLifeNonDisc) * 100) + (10000 * bidSpread);
                var computedDiscountMargin = (((100 - bidPrice * 100) / averageLife) * 100) + ((bidSpread / 100) * 10000);
                $('#ctl00_cphBody_txtDiscountMarginC').val(computedDiscountMargin.toFixed(2));
                e.preventDefault();
            }
        }
        //

        $(document).ready(function () {
            $('#ctl00_cphBody_txtOfferPriceA').numeric().keypress(function (e) {
                // if enter is pressed
                if (e.which == 13) {
                    OfferSpreadCalculationA($(this), e);
                    OfferSpreadDiscountMarginCalculationA($(this), e);
                }
            });
        });
        $(document).ready(function () {
            $('#ctl00_cphBody_txtBoxOfferPriceB').numeric().keypress(function (e) {
                // if enter is pressed
                if (e.which == 13) {
                    OfferSpreadCalculationB($(this), e);
                    OfferSpreadDiscountMarginCalculationB($(this), e);
                }
            });
        });
        $(document).ready(function () {
            $('#ctl00_cphBody_txtBoxOfferPriceC').numeric().keypress(function (e) {
                // if enter is pressed
                if (e.which == 13) {
                    OfferSpreadCalculationC($(this), e);
                    OfferSpreadDiscountMarginCalculationC($(this), e);
                }
            });
        });
        function OfferSpreadCalculationA(sender, e) {

            // (c) When askPrice text box changes please update askSpread to equal
            // Ask = offer
            // OfferSpread = (100 - offerPrice) * (360 / 365.25) * (100 / NonDiscountedAverageLife) + Discount_Margin;

            var offerPrice = parseFloat($(sender).val());
            if (isNaN(offerPrice)) {
                offerPrice = 0;
            }
            var averageLifeNonDisc = parseFloat($('#ctl00_cphBody_txtAveLifNonDiscA').val());
            if (isNaN(averageLifeNonDisc)) {
                averageLifeNonDisc = 1;
            }
            var discountMargin = parseFloat($('#ctl00_cphBody_txtBoxInterestRateA').val());
            if (isNaN(discountMargin)) {
                discountMargin = 0;
            }
            //var computedOfferSpread = (((100 - offerPrice) * 360 * 100) / (averageLifeNonDisc * 365.25)) + discountMargin; Nik 1503
            // var computedOfferSpread = ((360 * 100 * (100 - offerPrice)) / (365.25 * averageLifeNonDisc)) + discountMargin;
            var computedOfferSpread = ((360 * 100 * (100 - offerPrice)) / (365.5 * averageLifeNonDisc)) + discountMargin;// change on 02-04-014 Given by Ajay

            $('#ctl00_cphBody_txtOfferSpreadA').val(computedOfferSpread.toFixed(2));
            e.preventDefault();
        }
        function OfferSpreadCalculationB(sender, e) {

            // (c) When askPrice text box changes please update askSpread to equal
            // Ask = offer
            // OfferSpread = (100 - offerPrice) * (360 / 365.25) * (100 / NonDiscountedAverageLife) + Discount_Margin;

            var offerPrice = parseFloat($(sender).val());
            if (isNaN(offerPrice)) {
                offerPrice = 0;
            }
            var averageLifeNonDisc = parseFloat($('#ctl00_cphBody_txtBoxAveLifNonDiscB').val());
            if (isNaN(averageLifeNonDisc)) {
                averageLifeNonDisc = 1;
            }
            var discountMargin = parseFloat($('#ctl00_cphBody_txtInterestRateB').val());
            if (isNaN(discountMargin)) {
                discountMargin = 0;
            }
            //var computedOfferSpread = (((100 - offerPrice) * 360 * 100) / (averageLifeNonDisc * 365.25)) + discountMargin;
            // var computedOfferSpread = ((360 * 100 * (100 - offerPrice)) / (365.25 * averageLifeNonDisc)) + discountMargin;
            var computedOfferSpread = ((360 * 100 * (100 - offerPrice)) / (365.5 * averageLifeNonDisc)) + discountMargin;// change on 02-04-014 Given by Ajay
            $('#ctl00_cphBody_txtBoxOfferSpreadB').val(computedOfferSpread.toFixed(2));
            e.preventDefault();
        }
        function OfferSpreadCalculationC(sender, e) {

            // (c) When askPrice text box changes please update askSpread to equal
            // Ask = offer
            // OfferSpread = (100 - offerPrice) * (360 / 365.25) * (100 / NonDiscountedAverageLife) + Discount_Margin;

            var offerPrice = parseFloat($(sender).val());
            if (isNaN(offerPrice)) {
                offerPrice = 0;
            }
            var averageLifeNonDisc = parseFloat($('#ctl00_cphBody_txtBoxAveLifNonDiscC').val());
            if (isNaN(averageLifeNonDisc)) {
                averageLifeNonDisc = 1;
            }
            var discountMargin = parseFloat($('#ctl00_cphBody_txtInterestRateC').val());
            if (isNaN(discountMargin)) {
                discountMargin = 0;
            }
            //var computedOfferSpread = (((100 - offerPrice) * 360 * 100) / (averageLifeNonDisc * 365.25)) + discountMargin;
            // var computedOfferSpread = ((360 * 100 * (100 - offerPrice)) / (365.25 * averageLifeNonDisc)) + discountMargin;
            var computedOfferSpread = ((360 * 100 * (100 - offerPrice)) / (365.5 * averageLifeNonDisc)) + discountMargin;// change on 02-04-014 Given by Ajay
            $('#ctl00_cphBody_txtBoxOfferSpreadC').val(computedOfferSpread.toFixed(2));
            e.preventDefault();
        }

        // Nik 1503/ //
        function OfferSpreadDiscountMarginCalculationA(sender, e) {

            // (c) When askPrice text box changes please update askSpread to equal
            // Ask = offer
            // OfferSpread = (100 - offerPrice) * (360 / 365.25) * (100 / NonDiscountedAverageLife) + Discount_Margin;

            var offerPrice = parseFloat($(sender).val());
            if (isNaN(offerPrice)) {
                offerPrice = 0;
            }
            var averageLife = parseFloat($('#ctl00_cphBody_txtAvgLifeA').val());
            if (isNaN(averageLife)) {
                averageLife = 0;
            }
            var averageLifeNonDisc = parseFloat($('#ctl00_cphBody_txtAveLifNonDiscA').val());
            if (isNaN(averageLifeNonDisc)) {
                averageLifeNonDisc = 1;
            }
            var discountMargin = parseFloat($('#ctl00_cphBody_txtBoxInterestRateA').val());
            if (isNaN(discountMargin)) {
                discountMargin = 0;
            }
            var offerSpread = parseFloat($('#ctl00_cphBody_txtOfferSpreadA').val());
            //var computedOfferSpread = (((100 - offerPrice) * 360 * 100) / (averageLifeNonDisc * 365.25)) + discountMargin; Nik 1503
            //var computedOfferSpread = ((360 * 100 * (100 - offerPrice)) / (365.25 * averageLifeNonDisc)) + discountMargin;
            //var computedDiscountMargin = (((100 - (100 - offerPrice)) / averageLifeNonDisc) * 100) + (10000 * offerSpread)

            //$('#ctl00_cphBody_txtDiscountMarginA').val(computedDiscountMargin.toFixed(2));
            //e.preventDefault();
            if (averageLife > 0) {
                // var computedDiscountMargin = (((100 - (100 - bidPrice)) / averageLifeNonDisc) * 100) + (10000 * bidSpread);
                var computedDiscountMargin = (((100 - bidPrice * 100) / averageLife) * 100) + ((bidSpread / 100) * 10000);
                $('#ctl00_cphBody_txtDiscountMarginA').val(computedDiscountMargin.toFixed(2));
                e.preventDefault();
            }
        }
        function OfferSpreadDiscountMarginCalculationB(sender, e) {

            // (c) When askPrice text box changes please update askSpread to equal
            // Ask = offer
            // OfferSpread = (100 - offerPrice) * (360 / 365.25) * (100 / NonDiscountedAverageLife) + Discount_Margin;

            var offerPrice = parseFloat($(sender).val());
            if (isNaN(offerPrice)) {
                offerPrice = 0;
            }
            var averageLife = parseFloat($('#ctl00_cphBody_txtAvgLifeB').val());
            if (isNaN(averageLife)) {
                averageLife = 0;
            }
            var averageLifeNonDisc = parseFloat($('#ctl00_cphBody_txtBoxAveLifNonDiscB').val());
            if (isNaN(averageLifeNonDisc)) {
                averageLifeNonDisc = 1;
            }
            var discountMargin = parseFloat($('#ctl00_cphBody_txtInterestRateB').val());
            if (isNaN(discountMargin)) {
                discountMargin = 0;
            }
            var offerSpread = parseFloat($('#ctl00_cphBody_txtBoxOfferSpreadB').val());
            //var computedOfferSpread = (((100 - offerPrice) * 360 * 100) / (averageLifeNonDisc * 365.25)) + discountMargin; Nik 1503
            //var computedOfferSpread = ((360 * 100 * (100 - offerPrice)) / (365.25 * averageLifeNonDisc)) + discountMargin;
            //var computedDiscountMargin = (((100 - (100 - offerPrice)) / averageLifeNonDisc) * 100) + (10000 * offerSpread)

            //$('#ctl00_cphBody_txtDiscountMarginB').val(computedDiscountMargin.toFixed(2));
            //e.preventDefault();
            if (averageLife > 0) {
                // var computedDiscountMargin = (((100 - (100 - bidPrice)) / averageLifeNonDisc) * 100) + (10000 * bidSpread);
                var computedDiscountMargin = (((100 - bidPrice * 100) / averageLife) * 100) + ((bidSpread / 100) * 10000);
                $('#ctl00_cphBody_txtDiscountMarginB').val(computedDiscountMargin.toFixed(2));
                e.preventDefault();
            }
        }
        function OfferSpreadDiscountMarginCalculationC(sender, e) {

            // (c) When askPrice text box changes please update askSpread to equal
            // Ask = offer
            // OfferSpread = (100 - offerPrice) * (360 / 365.25) * (100 / NonDiscountedAverageLife) + Discount_Margin;

            var offerPrice = parseFloat($(sender).val());
            if (isNaN(offerPrice)) {
                offerPrice = 0;
            }
            var averageLife = parseFloat($('#ctl00_cphBody_txtAvgLifeC').val());
            if (isNaN(averageLife)) {
                averageLife = 0;
            }
            var averageLifeNonDisc = parseFloat($('#ctl00_cphBody_txtBoxAveLifNonDiscC').val());
            if (isNaN(averageLifeNonDisc)) {
                averageLifeNonDisc = 1;
            }
            var discountMargin = parseFloat($('#ctl00_cphBody_txtInterestRateC').val());
            if (isNaN(discountMargin)) {
                discountMargin = 0;
            }
            var offerSpread = parseFloat($('#ctl00_cphBody_txtBoxOfferSpreadC').val());
            //var computedOfferSpread = (((100 - offerPrice) * 360 * 100) / (averageLifeNonDisc * 365.25)) + discountMargin; Nik 1503
            //var computedOfferSpread = ((360 * 100 * (100 - offerPrice)) / (365.25 * averageLifeNonDisc)) + discountMargin;
            //var computedDiscountMargin = (((100 - (100 - offerPrice)) / averageLifeNonDisc) * 100) + (10000 * offerSpread)

            //$('#ctl00_cphBody_txtDiscountMarginC').val(computedDiscountMargin.toFixed(2));
            //e.preventDefault();
            if (averageLife > 0) {
                // var computedDiscountMargin = (((100 - (100 - bidPrice)) / averageLifeNonDisc) * 100) + (10000 * bidSpread);
                var computedDiscountMargin = (((100 - bidPrice * 100) / averageLife) * 100) + ((bidSpread / 100) * 10000);
                $('#ctl00_cphBody_txtDiscountMarginC').val(computedDiscountMargin.toFixed(2));
                e.preventDefault();
            }
        }
        //


        $(document).ready(function () {
            $('#ctl00_cphBody_txtOfferSpreadA').numeric().keypress(function (e) {
                // if enter is pressed
                if (e.which == 13) {
                    OfferPriceCalculationA($(this), e);
                    OfferPriceDiscountMarginCalculationA($(this), e);
                }
            });
        });
        $(document).ready(function () {
            $('#ctl00_cphBody_txtBoxOfferSpreadB').numeric().keypress(function (e) {
                // if enter is pressed
                if (e.which == 13) {
                    OfferPriceCalculationB($(this), e);
                    OfferPriceDiscountMarginCalculationB($(this), e);
                }
            });
        });
        $(document).ready(function () {
            $('#ctl00_cphBody_txtBoxOfferSpreadC').numeric().keypress(function (e) {
                // if enter is pressed
                if (e.which == 13) {
                    OfferPriceCalculationC($(this), e);
                    OfferPriceDiscountMarginCalculationC($(this), e);
                }
            });
        });

        function OfferPriceCalculationA(sender, e) {

            // (c) When askPrice text box changes please update askSpread to equal
            // Ask = offer
            // offerPrice = (100 - (((offerSpread - Discount_Margin) / (360/365.25) ) / (100/NonDiscountedAverageLife))) ;

            var offerSpread = parseFloat($(sender).val());
            if (isNaN(offerSpread)) {
                offerSpread = 0;
            }
            var averageLifeNonDisc = parseFloat($('#ctl00_cphBody_txtAveLifNonDiscA').val());
            if (isNaN(averageLifeNonDisc)) {
                averageLifeNonDisc = 1;
            }
            var discountMargin = parseFloat($('#ctl00_cphBody_txtBoxInterestRateA').val());
            if (isNaN(discountMargin)) {
                discountMargin = 0;
            }

            //var computedOfferPrice = 100 - (((offerSpread - discountMargin) * (averageLifeNonDisc * 365.25)) / 360 / 100);

            // var computedOfferPrice = 100 - (((offerSpread - discountMargin) * (365.25 * averageLifeNonDisc)) / (360 * 100));
            var computedOfferPrice = 100 - (((offerSpread - discountMargin) * (365.25 * averageLifeNonDisc)) / (360 * 100)); // Change on 02-04-2014 given by ajay

            $('#ctl00_cphBody_txtOfferPriceA').val(computedOfferPrice.toFixed(2));
            e.preventDefault();
        }
        function OfferPriceCalculationB(sender, e) {
            var container = $(sender).parent().parent().parent();
            // (c) When askPrice text box changes please update askSpread to equal
            // Ask = offer
            // offerPrice = (100 - (((offerSpread - Discount_Margin) / (360/365.25) ) / (100/NonDiscountedAverageLife))) ;

            var offerSpread = parseFloat($(sender).val());
            if (isNaN(offerSpread)) {
                offerSpread = 0;
            }
            var averageLifeNonDisc = parseFloat($('#ctl00_cphBody_txtBoxAveLifNonDiscB').val());
            if (isNaN(averageLifeNonDisc)) {
                averageLifeNonDisc = 1;
            }
            var discountMargin = parseFloat($('#ctl00_cphBody_txtInterestRateB').val());
            if (isNaN(discountMargin)) {
                discountMargin = 0;
            }

            // var computedOfferPrice = 100 - (((offerSpread - discountMargin) * (averageLifeNonDisc * 365.25)) / 360 / 100);
            var computedOfferPrice = 100 - (((offerSpread - discountMargin) * (365.25 * averageLifeNonDisc)) / (360 * 100)); // Change on 02-04-2014 given by ajay
            $('#ctl00_cphBody_txtBoxOfferPriceB').val(computedOfferPrice.toFixed(2));
            e.preventDefault();
        }
        function OfferPriceCalculationC(sender, e) {

            // (c) When askPrice text box changes please update askSpread to equal
            // Ask = offer
            // offerPrice = (100 - (((offerSpread - Discount_Margin) / (360/365.25) ) / (100/NonDiscountedAverageLife))) ;

            var offerSpread = parseFloat($(sender).val());
            if (isNaN(offerSpread)) {
                offerSpread = 0;
            }
            var averageLifeNonDisc = parseFloat($('#ctl00_cphBody_txtBoxAveLifNonDiscC').val());
            if (isNaN(averageLifeNonDisc)) {
                averageLifeNonDisc = 1;
            }
            var discountMargin = parseFloat($('#ctl00_cphBody_txtInterestRateC').val());
            if (isNaN(discountMargin)) {
                discountMargin = 0;
            }

            //var computedOfferPrice = 100 - (((offerSpread - discountMargin) * (averageLifeNonDisc * 365.25)) / 360 / 100);
            var computedOfferPrice = 100 - (((offerSpread - discountMargin) * (365.25 * averageLifeNonDisc)) / (360 * 100)); // Change on 02-04-2014 given by ajay
            $('#ctl00_cphBody_txtBoxOfferPriceC').val(computedOfferPrice.toFixed(2));
            e.preventDefault();
        }

        function OfferPriceDiscountMarginCalculationA(sender, e) {

            // (c) When askPrice text box changes please update askSpread to equal
            // Ask = offer
            // offerPrice = (100 - (((offerSpread - Discount_Margin) / (360/365.25) ) / (100/NonDiscountedAverageLife))) ;

            var offerSpread = parseFloat($(sender).val());
            if (isNaN(offerSpread)) {
                offerSpread = 0;
            }
            var averageLife = parseFloat($('#ctl00_cphBody_txtAvgLifeA').val());
            if (isNaN(averageLife)) {
                averageLife = 0;
            }
            var averageLifeNonDisc = parseFloat($('#ctl00_cphBody_txtAveLifNonDiscA').val());
            if (isNaN(averageLifeNonDisc)) {
                averageLifeNonDisc = 1;
            }
            var discountMargin = parseFloat($('#ctl00_cphBody_txtBoxInterestRateA').val());
            if (isNaN(discountMargin)) {
                discountMargin = 0;
            }
            var offerPrice = parseFloat($('#ctl00_cphBody_txtOfferPriceA').val());
            //var computedOfferPrice = 100 - (((offerSpread - discountMargin) * (averageLifeNonDisc * 365.25)) / 360 / 100);

            //var computedOfferPrice = 100 - (((offerSpread - discountMargin) * (365.25 * averageLifeNonDisc)) / (360 * 100));
            //var computedDiscountMargin = (((100 - (100 - offerSpread)) / averageLifeNonDisc) * 100) + (10000 * offerPrice)
            //$('#ctl00_cphBody_txtDiscountMarginA').val(computedDiscountMargin.toFixed(2));
            //e.preventDefault();
            if (averageLife > 0) {
                // var computedDiscountMargin = (((100 - (100 - bidPrice)) / averageLifeNonDisc) * 100) + (10000 * bidSpread);
                var computedDiscountMargin = (((100 - bidPrice * 100) / averageLife) * 100) + ((bidSpread / 100) * 10000);
                $('#ctl00_cphBody_txtDiscountMarginA').val(computedDiscountMargin.toFixed(2));
                e.preventDefault();
            }
        }

        function OfferPriceDiscountMarginCalculationB(sender, e) {

            // (c) When askPrice text box changes please update askSpread to equal
            // Ask = offer
            // offerPrice = (100 - (((offerSpread - Discount_Margin) / (360/365.25) ) / (100/NonDiscountedAverageLife))) ;

            var offerSpread = parseFloat($(sender).val());
            if (isNaN(offerSpread)) {
                offerSpread = 0;
            }
            var averageLife = parseFloat($('#ctl00_cphBody_txtAvgLifeB').val());
            if (isNaN(averageLife)) {
                averageLife = 0;
            }
            var averageLifeNonDisc = parseFloat($('#ctl00_cphBody_txtBoxAveLifNonDiscB').val());
            if (isNaN(averageLifeNonDisc)) {
                averageLifeNonDisc = 1;
            }
            var discountMargin = parseFloat($('#ctl00_cphBody_txtInterestRateB').val());
            if (isNaN(discountMargin)) {
                discountMargin = 0;
            }
            var offerPrice = parseFloat($('#ctl00_cphBody_txtBoxOfferPriceB').val());
            //var computedOfferPrice = 100 - (((offerSpread - discountMargin) * (averageLifeNonDisc * 365.25)) / 360 / 100);

            //var computedOfferPrice = 100 - (((offerSpread - discountMargin) * (365.25 * averageLifeNonDisc)) / (360 * 100));
            //var computedDiscountMargin = (((100 - (100 - offerSpread)) / averageLifeNonDisc) * 100) + (10000 * offerPrice)
            //$('#ctl00_cphBody_txtDiscountMarginB').val(computedDiscountMargin.toFixed(2));
            //e.preventDefault();
            if (averageLife > 0) {
                // var computedDiscountMargin = (((100 - (100 - bidPrice)) / averageLifeNonDisc) * 100) + (10000 * bidSpread);
                var computedDiscountMargin = (((100 - bidPrice * 100) / averageLife) * 100) + ((bidSpread / 100) * 10000);
                $('#ctl00_cphBody_txtDiscountMarginB').val(computedDiscountMargin.toFixed(2));
                e.preventDefault();
            }
        }

        function OfferPriceDiscountMarginCalculationC(sender, e) {

            // (c) When askPrice text box changes please update askSpread to equal
            // Ask = offer
            // offerPrice = (100 - (((offerSpread - Discount_Margin) / (360/365.25) ) / (100/NonDiscountedAverageLife))) ;

            var offerSpread = parseFloat($(sender).val());
            if (isNaN(offerSpread)) {
                offerSpread = 0;
            }
            var averageLife = parseFloat($('#ctl00_cphBody_txtAvgLifeC').val());
            if (isNaN(averageLife)) {
                averageLife = 0;
            }
            var averageLifeNonDisc = parseFloat($('#ctl00_cphBody_txtBoxAveLifNonDiscC').val());
            if (isNaN(averageLifeNonDisc)) {
                averageLifeNonDisc = 1;
            }
            var discountMargin = parseFloat($('#ctl00_cphBody_txtInterestRateC').val());
            if (isNaN(discountMargin)) {
                discountMargin = 0;
            }
            var offerPrice = parseFloat($('#ctl00_cphBody_txtBoxOfferPriceC').val());
            //var computedOfferPrice = 100 - (((offerSpread - discountMargin) * (averageLifeNonDisc * 365.25)) / 360 / 100);

            //var computedOfferPrice = 100 - (((offerSpread - discountMargin) * (365.25 * averageLifeNonDisc)) / (360 * 100));
            //var computedDiscountMargin = (((100 - (100 - offerSpread)) / averageLifeNonDisc) * 100) + (10000 * offerPrice)
            //$('#ctl00_cphBody_txtDiscountMarginC').val(computedDiscountMargin.toFixed(2));
            //e.preventDefault();
            if (averageLife > 0) {
                // var computedDiscountMargin = (((100 - (100 - bidPrice)) / averageLifeNonDisc) * 100) + (10000 * bidSpread);
                var computedDiscountMargin = (((100 - bidPrice * 100) / averageLife) * 100) + ((bidSpread / 100) * 10000);
                $('#ctl00_cphBody_txtDiscountMarginC').val(computedDiscountMargin.toFixed(2));
                e.preventDefault();
            }
        }
        //
        function FileExist() {
            if ($("#fuUploaded").val() == "uploaded")
                return true;
            else {
                alert("Please select a file to Import");
                return false;
            }
        }
        function OnSelecting(sender, args) {

            $get("<%= previousTabHidden.ClientID%>").value = sender.get_selectedTab().get_text();
            var savedValue = $get("<%= hdnSaved.ClientID%>").value;

            if ($get("<%= previousTabHidden.ClientID%>").value == ' Add New Loan') {
                if (savedValue == 'N') {

                    timeout = setTimeout(callAlert(), 7000);
                    clearTimeout(timeout);
                }

            }

        }
        function callAlert() {
            //get a reference to the radalert object
            var oWnd = radalert("your data will be lost and a new loan has not been saved");
            //add a closing function to it
            oWnd.add_close(closingFn);
        }

        function closingFn() {
            //execute your code
            alert("closed");
        }


        var imgUrl = null;
        function alertCallBackFn(arg) {

        }


        function InitiateAsyncRequest(argument) {

            return false;
        }


        //Created on 21-04-2014 // For Loan Detail Calcs
        $(document).ready(function () {
            $('#ctl00_cphBody_txtLoanDetailPrice').numeric().keypress(function (e) {
                // if enter is pressed

                if (e.which == 13) {
                    LoanDetailBidSpreadCalculation($(this), e);

                }
            });
        });


        function LoanDetailBidSpreadCalculation(sender, e) {

            var bidPrice = parseFloat($(sender).val());
            if (isNaN(bidPrice)) {
                bidPrice = 0;
            }
            var averageLifeNonDisc = parseFloat($('#ctl00_cphBody_txtAvgLifeNonDisc').val());
            if (isNaN(averageLifeNonDisc)) {
                averageLifeNonDisc = 1;
            }
            var discountMargin = parseFloat($('#ctl00_cphBody_hdnLoanDetailMargin').val());
            if (isNaN(discountMargin)) {
                discountMargin = 0;
            }


            var computedBidSpread = ((360 * 100 * (100 - bidPrice)) / (365.5 * averageLifeNonDisc)) + discountMargin;
            $('#ctl00_cphBody_txtLoanDetailSpread').val(computedBidSpread.toFixed(2));
            e.preventDefault();

        }


        $(document).ready(function () {
            $('#ctl00_cphBody_txtLoanDetailSpread').numeric().keypress(function (e) {
                // if enter is pressed
                if (e.which == 13) {
                    LoanDetailBidPriceCalculation($(this), e);

                }
            });
        });


        function LoanDetailBidPriceCalculation(sender, e) {

            var bidSpread = parseFloat($(sender).val());
            if (isNaN(bidSpread)) {
                bidSpread = 0;
            }
            var averageLifeNonDisc = parseFloat($('#ctl00_cphBody_txtAvgLifeNonDisc').val());
            if (isNaN(averageLifeNonDisc)) {
                averageLifeNonDisc = 1;
            }
            var discountMargin = parseFloat($('#ctl00_cphBody_hdnLoanDetailMargin').val());
            if (isNaN(discountMargin)) {
                discountMargin = 0;
            }


            var computedBidPrice = 100 - (((bidSpread - discountMargin) * (365.25 * averageLifeNonDisc)) / (360 * 100));

            $('#ctl00_cphBody_txtLoanDetailPrice').val(computedBidPrice.toFixed(2));

            e.preventDefault();
        }


        $(document).ready(function () {
            $('#ctl00_cphBody_txtLoanDetailStreet1').numeric().keypress(function (e) {
                // if enter is pressed
                if (e.which == 13) {
                    LoanDetailOfferSpreadCalculation($(this), e);

                }
            });
        });



        function LoanDetailOfferSpreadCalculation(sender, e) {



            var offerPrice = parseFloat($(sender).val());
            if (isNaN(offerPrice)) {
                offerPrice = 0;
            }
            var averageLifeNonDisc = parseFloat($('#ctl00_cphBody_txtAvgLifeNonDisc').val());
            if (isNaN(averageLifeNonDisc)) {
                averageLifeNonDisc = 1;
            }
            var discountMargin = parseFloat($('#ctl00_cphBody_hdnLoanDetailMargin').val());
            if (isNaN(discountMargin)) {
                discountMargin = 0;
            }

            var computedOfferSpread = ((360 * 100 * (100 - offerPrice)) / (365.5 * averageLifeNonDisc)) + discountMargin;

            $('#ctl00_cphBody_txtLoanDetailStreet3').val(computedOfferSpread.toFixed(2));
            e.preventDefault();
        }


        $(document).ready(function () {
            $('#ctl00_cphBody_txtLoanDetailStreet4').numeric().keypress(function (e) {
                // if enter is pressed
                if (e.which == 13) {
                    LoanDetailOfferPriceCalculation($(this), e);

                }
            });
        });


        function LoanDetailOfferPriceCalculation(sender, e) {



            var offerSpread = parseFloat($(sender).val());
            if (isNaN(offerSpread)) {
                offerSpread = 0;
            }
            var averageLifeNonDisc = parseFloat($('#ctl00_cphBody_txtAvgLifeNonDisc').val());
            if (isNaN(averageLifeNonDisc)) {
                averageLifeNonDisc = 1;
            }
            var discountMargin = parseFloat($('#ctl00_cphBody_hdnLoanDetailMargin').val());
            if (isNaN(discountMargin)) {
                discountMargin = 0;
            }

            var computedOfferPrice = 100 - (((offerSpread - discountMargin) * (365.25 * averageLifeNonDisc)) / (360 * 100));

            $('#ctl00_cphBody_txtLoanDetailStreet2').val(computedOfferPrice.toFixed(2));
            e.preventDefault();
        }
    </script>
    <style>
        RadWindow .rwIcon {
            width: 0px;
            margin-left: 0px;
            margin-right: 0px;
            cursor: default;
            margin-top: 0px;
            height: 0!important;
            width: 0!important;
            display: none !important;
        }


        .radHeight {
            height: auto;
        }

        .newclass-addloan {
            width: 50%;
            padding-left: 15px;
            padding-top: 10px;
        }

        .newclass-addloan-btn {
            width: 50%;
            padding-left: 15px;
            padding-top: 10px;
            text-align: right;
            margin-bottom: 10px;
            height: 60px;
        }

        .tableheader {
            width: 125px;
            text-align: center;
            border: 1px solid #4A7EBB;
        }

        .tableBorder {
            border: 1px solid #4A7EBB;
            background-color: #CBDDFF;
        }

        .tdBorder {
            border: 1px solid #4A7EBB;
        }

        .thBorder {
            border: 1px solid #4A7EBB;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" runat="server">
    <asp:HiddenField ID="hfMode" runat="server" />
    <asp:HiddenField ID="hdnLoan" runat="server" />
    <telerik:RadFormDecorator ID="RadFormDecorator1" runat="server" Skin="Web20" DecoratedControls="Buttons" />
    <table width="100%" style="margin-left: 35px">
        <tr>
            <td>
                <asp:HiddenField ID="previousTabHidden" runat="Server" />
                <asp:HiddenField ID="hdnSaved" runat="server" />
                <asp:HiddenField ID="hdnQuoteTemp" runat="server" />
                <telerik:RadWindowManager ID="RadWindowManager1"
                    runat="server" EnableShadow="true" IconUrl="~">
                </telerik:RadWindowManager>
                <telerik:RadTabStrip ID="RadTabStrip1" runat="server" Skin="Web20" MultiPageID="RadMultiPage1" Width="96%" BorderWidth="1px" BorderStyle="Solid"
                    SelectedIndex="0" Align="Left" OnClientTabSelecting="OnSelecting">
                    <Tabs>
                        <telerik:RadTab Text="Compact" NavigateUrl="default.aspx" runat="server" Selected="True" Width="110px">
                        </telerik:RadTab>
                        <telerik:RadTab Text="Graphs" NavigateUrl="default.aspx?page=chart" runat="server" Width="110px">
                        </telerik:RadTab>
                        <telerik:RadTab Text="Loan Details" NavigateUrl="default.aspx?page=loandetails" runat="server" Width="110px">
                        </telerik:RadTab>
                        <telerik:RadTab Text="Add New Loan" NavigateUrl="default.aspx?page=addstaticloan" runat="server" Width="180px">
                        </telerik:RadTab>
                        <telerik:RadTab Text="Historical Quotes DB" NavigateUrl="default.aspx?page=viewhistoricalquotes" runat="server" Width="220px">
                        </telerik:RadTab>
                        <telerik:RadTab Text="Loans DB" NavigateUrl="default.aspx?page=viewloans" runat="server" Width="120px">
                        </telerik:RadTab>

                        <telerik:RadTab Text="Edit Historical Quotes & Trades" NavigateUrl="default.aspx?page=edithistoricalquotes" runat="server" Width="220px" Visible="false">
                        </telerik:RadTab>
                        <telerik:RadTab Text="My Account" NavigateUrl="default.aspx?page=myaccount" runat="server" Width="220px">
                        </telerik:RadTab>
                    </Tabs>
                </telerik:RadTabStrip>

                <telerik:RadMultiPage runat="server" ID="RadMultiPage1" SelectedIndex="0" Width="96%" BorderWidth="1px" BorderStyle="Solid" CssClass="radHeight">
                    <telerik:RadPageView runat="server" ID="RadPageView1" CssClass="radHeight">
                        <%--<p>
                <asp:Label ID='lblCompact' runat="server" ForeColor="#FFFFFF" Font-Bold="true" Font-Size="Large">Compacts</asp:Label>
            </p>--%>
                        <br />


                        <%-- <telerik:RadAjaxPanel ID="pnl1" runat="server" LoadingPanelID="RadAjaxLoadingPanel2">--%>
                        <table width="95%" style="margin-left: 10px; margin-right: 50px">
                            <tr align="right">
                                <td style="width: 95%">
                                    <telerik:RadButton ID="btnClearAll" runat="server" OnClick="btnClearAll_Click" Text="Clear All" Skin="Web20">
                                    </telerik:RadButton>
                                </td>
                            </tr>
                        </table>
                        <table width="100%" style="margin-left: 10px; margin-right: 10px">

                            <tr valign="top">
                                <td width="28%">
                                    <table>
                                        <tr>
                                            <td id="Td117">
                                                <asp:Label runat="server" Text="Loan Name" ID="Label9" BorderStyle="None" Font-Bold="true"></asp:Label>
                                            </td>
                                            <td class="style1" id="Td118">
                                                <script type="text/javascript">
                                                    function requesting(sender, eventArgs) {
                                                        var context = eventArgs.get_context();
                                                        //Data passed to the service.
                                                        context["ClientData"] = context.Text;
                                                    }
                                                </script>
                                                <%--<asp:TextBox runat="server" ID="txtLoanNameA"></asp:TextBox>--%>
                                                <%--<telerik:RadTextBox ID="txtLoanNameA" runat="server"></telerik:RadTextBox>--%>
                                                <telerik:RadComboBox ID="txtLoanNameA" AllowCustomText="true" runat="server" Width="160"
                                                    Height="200px" OnSelectedIndexChanged="txtLoanNameA_SelectedIndexChanged" EmptyMessage="Search for loan..."
                                                    Skin="Web20" AutoPostBack="true">
                                                </telerik:RadComboBox>

                                                <%--    <telerik:RadComboBox runat="server" Skin="Web20" ID="txtLoanNameA" OnTextChanged="txtLoanNameA_TextChanged" AutoPostBack="true">
                                                    <WebServiceSettings Path="~/LoanService.asmx" Method="GetCompletionList" />
                                                </telerik:RadComboBox>--%>

                                                <asp:HiddenField ID="hfSelectedLoanA" runat="server" />
                                            </td>
                                        </tr>
                                        <%--  <tr>
                                            <td>
                                                <label id="lblNameA" runat="server" visible="false">Loan Name</label>
                                            </td>
                                            <td>
                                                <label id="lblLoanNameA" runat="server" visible="false"></label>
                                            </td>
                                        </tr>--%>

                                        <tr>
                                            <td id="Td121">Trade Date
                                            </td>
                                            <td class="style1" id="Td122">
                                                <telerik:RadDateTimePicker ID="txtBoxTradeDate1" runat="server" AutoPostBack="true" OnSelectedDateChanged="txtBoxTradeDate1_SelectedDateChanged" AutoPostBackControl="Both"
                                                    MinDate="01/01/1000" MaxDate="01/01/3000" Skin="Web20" DateInput-DisplayDateFormat="dd/MM/yyyy HH:mm" DateInput-DateFormat="dd/MM/yyyy HH:mm">
                                                    <Calendar>
                                                        <SpecialDays>
                                                            <telerik:RadCalendarDay Repeatable="Today" ItemStyle-CssClass="rcToday">
                                                            </telerik:RadCalendarDay>
                                                        </SpecialDays>
                                                    </Calendar>
                                                </telerik:RadDateTimePicker>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td id="Td123">Settlement Date
                                            </td>
                                            <td class="style1" id="Td124">
                                                <%--<asp:TextBox runat="server" ID="txtBoxSettlementDate1"></asp:TextBox>--%>
                                                <telerik:RadScheduler>
                                                    <telerik:RadDatePicker ID="txtBoxSettlementDate1" runat="server" AutoPostBack="true"
                                                        MinDate="01/01/1000" MaxDate="01/01/3000" Skin="Web20">
                                                        <calendar>
                                                        <SpecialDays>
                                                            <telerik:RadCalendarDay Repeatable="Today" ItemStyle-CssClass="rcToday">
                                                            </telerik:RadCalendarDay>
                                                        </SpecialDays>
                                                    </calendar>
                                                    </telerik:RadDatePicker>
                                                </telerik:RadScheduler>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td id="Td125">Maturity Date
                                            </td>
                                            <td class="style1" id="Td126">
                                                <telerik:RadDatePicker ID="txtBoxMaturityDateA" runat="server" AutoPostBack="true"
                                                    MinDate="01/01/1000" MaxDate="01/01/3000" Skin="Web20" OnSelectedDateChanged="txtBoxMaturityDateA_SelectedDateChanged">
                                                    <Calendar>
                                                        <SpecialDays>
                                                            <telerik:RadCalendarDay Repeatable="Today" ItemStyle-CssClass="rcToday">
                                                            </telerik:RadCalendarDay>
                                                        </SpecialDays>
                                                    </Calendar>
                                                </telerik:RadDatePicker>

                                            </td>
                                        </tr>
                                        <%-- <tr>
                                            <td id="Td9">Country
                                            </td>
                                            <td class="style1" id="Td10">
                                                <telerik:RadComboBox ID="ddlRegionA" AllowCustomText="true" runat="server" Width="160"
                                                    Height="200px" OnSelectedIndexChanged="ddlRegionA_SelectedIndexChanged" EmptyMessage="Country..."
                                                    Skin="Web20" AutoPostBack="true">
                                                </telerik:RadComboBox>

                                            </td>
                                        </tr>--%>
                                        <tr>
                                            <td id="Td129">
                                                <asp:Label runat="server" Text="Currency" BorderStyle="None" ID="Label11"></asp:Label>
                                            </td>
                                            <td class="style1" id="Td130">
                                                <telerik:RadTextBox runat="server" ID="txtBoxCurrencyLoanA"></telerik:RadTextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td id="Td119">
                                                <asp:Label runat="server" Text="Margin" BorderStyle="None" ID="Label10"></asp:Label>
                                            </td>
                                            <td class="style1" id="Td120">
                                                <telerik:RadTextBox runat="server" ID="txtBoxInterestRateA"></telerik:RadTextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td id="Td131">
                                                <asp:Label runat="server" Text="Coupon Frequency" BorderStyle="None" ID="Label12"></asp:Label>
                                            </td>
                                            <td class="style1" id="Td132">
                                                <telerik:RadTextBox runat="server" ID="txtBoxCouponFrequencyLoanA"></telerik:RadTextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td id="Td17">Fixed/Floating
                                            </td>
                                            <td class="style1" id="Td18">
                                                <telerik:RadTextBox runat="server" ID="txtBoxLastFixingA"></telerik:RadTextBox>
                                            </td>
                                        </tr>
                                        <%--<tr>
                                            <td id="Td19">IR/Coupon
                                            </td>
                                            <td class="style1" id="Td20">
                                                
                                                <telerik:RadTextBox ID="txtBoxIRCouponA" runat="server"></telerik:RadTextBox>
                                            </td>
                                        </tr>--%>
                                        <tr>
                                            <td id="Td13">
                                                <asp:Label Text="Bid Price" ID="labelBidPriceA" runat="server" ForeColor="DarkBlue"></asp:Label>
                                            </td>
                                            <td class="style1" id="Td14">
                                                <%--<asp:TextBox runat="server" ID="txtBidPriceA" CssClass="BidPrice"></asp:TextBox>--%>
                                                <telerik:RadTextBox ID="txtBidPriceA" runat="server" CssClass="BidPrice" BackColor="WhiteSmoke">
                                                </telerik:RadTextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td id="Td37">
                                                <asp:Label Text="Bid Spread" ID="lblBidSpread" runat="server" ForeColor="DarkBlue"></asp:Label>
                                            </td>
                                            <td class="style1" id="Td38">
                                                <%--<asp:TextBox runat="server" ID="txtBidSpreadA" CssClass="bidSpread"></asp:TextBox>--%>
                                                <telerik:RadTextBox ID="txtBidSpreadA" runat="server" CssClass="BidSpread1" BackColor="WhiteSmoke"></telerik:RadTextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td id="Td39">
                                                <asp:Label Text="Offer Price" ID="label14" runat="server" ForeColor="DarkBlue"></asp:Label>
                                            </td>
                                            <td class="style1" id="Td40">
                                                <%--<asp:TextBox runat="server" ID="txtOfferPriceA" CssClass="OfferPrice"></asp:TextBox>--%>
                                                <telerik:RadTextBox ID="txtOfferPriceA" runat="server" CssClass="OfferPrice" BackColor="WhiteSmoke"></telerik:RadTextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td id="Td41">
                                                <asp:Label Text="Offer Spread" ID="label15" runat="server" ForeColor="DarkBlue"></asp:Label>
                                            </td>
                                            <td class="style1" id="Td42">
                                                <%--<asp:TextBox runat="server" ID="txtOfferSpreadA" CssClass="OfferSpread"></asp:TextBox>--%>
                                                <telerik:RadTextBox ID="txtOfferSpreadA" runat="server" CssClass="OfferSpread" BackColor="WhiteSmoke"></telerik:RadTextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td id="Td1">Notional
                                            </td>
                                            <td class="style1" id="Td2">
                                                <%--<asp:TextBox runat="server" ID="txtBoxNotional1"></asp:TextBox>--%>
                                                <telerik:RadTextBox ID="txtBoxNotional1" runat="server" Skin="Web20" AutoPostBack="true" OnTextChanged="txtBoxNotional1_TextChanged"></telerik:RadTextBox>
                                            </td>
                                        </tr>
                                        <%-- <tr>
                                            <td id="Td27">Discount Margin
                                            </td>
                                            <td class="style1" id="Td28">

                                                <telerik:RadTextBox ID="txtDiscountMarginA" runat="server" Skin="Web20" CssClass="DiscountMargin"></telerik:RadTextBox>
                                            </td>
                                        </tr>--%>
                                        <telerik:RadTextBox ID="txtDiscountMarginA" Visible="false" runat="server" Skin="Web20" CssClass="DiscountMargin"></telerik:RadTextBox>
                                        <tr style="display: none">
                                            <td id="Td29">IRR
                                            </td>
                                            <td class="style1" id="Td30">
                                                <telerik:RadTextBox ID="txtIRRA" runat="server" Skin="Web20"></telerik:RadTextBox>
                                            </td>
                                        </tr>
                                        <tr style="display: none">
                                            <td id="Td5">Average Life
                                            </td>
                                            <td class="style1" id="Td6">
                                                <%--<asp:TextBox runat="server" ID="txtIRRA"></asp:TextBox>--%>
                                                <telerik:RadTextBox ID="txtAvgLifeA" runat="server" Skin="Web20"></telerik:RadTextBox>
                                            </td>
                                        </tr>

                                        <tr>
                                            <td id="Td31">
                                                <asp:Label Text="Ave.Life NonDisc" ID="label16" runat="server" ForeColor="DarkBlue"></asp:Label>
                                            </td>
                                            <td class="style1" id="Td32">
                                                <%--<asp:TextBox runat="server" ID="txtAveLifNonDiscA" CssClass="AverageLifeNonDisc"></asp:TextBox>--%>
                                                <telerik:RadTextBox ID="txtAveLifNonDiscA" runat="server" Skin="Web20" CssClass="AverageLifeNonDisc" BackColor="WhiteSmoke"></telerik:RadTextBox>
                                            </td>
                                        </tr>
                                        <tr style="display: none">
                                            <td id="Td33">Ave.Life Disc
                                            </td>
                                            <td class="style1" id="Td34">
                                                <%--<asp:TextBox runat="server" ID="txtAveLifDiscA"></asp:TextBox>--%>
                                                <telerik:RadTextBox ID="txtAveLifDiscA" runat="server" Skin="Web20"></telerik:RadTextBox>
                                            </td>
                                        </tr>
                                        <tr style="display: none">
                                            <td id="Td35">Ave.Life Risky Disc
                                            </td>
                                            <td class="style1" id="Td36">
                                                <%--<asp:TextBox runat="server" ID="txtAveLifRiskyDiscA"></asp:TextBox>--%>
                                                <telerik:RadTextBox ID="txtAveLifRiskyDiscA" runat="server"></telerik:RadTextBox>
                                            </td>
                                        </tr>

                                        <tr>
                                            <td id="Td51">CounterParty
                                            </td>
                                            <td class="style1" id="Td52">
                                                <%--<asp:TextBox runat="server" ID="txtBoxCounterPartyA"></asp:TextBox>--%>
                                                <%--<telerik:RadTextBox ID="txtBoxCounterPartyA" runat="server" Skin="Web20"></telerik:RadTextBox>--%>
                                                <telerik:RadDropDownList ID="ddlCounterPartyA" runat="server" Skin="Web20">
                                                </telerik:RadDropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td id="Td49">Check if traded
                                            </td>
                                            <td class="style1" id="Td50">
                                                <asp:CheckBox ID="chkBoxTradedA" runat="server"></asp:CheckBox>
                                            </td>
                                        </tr>
                                        <%--<tr>
                                            <td id="Td23">
                                                <asp:Label runat="server" Text="Curve" BorderStyle="None" ID="lblCurve"></asp:Label>
                                            </td>
                                            <td class="style1" id="Td24">

                                                <telerik:RadDropDownList ID="ddlCureA" runat="server" Skin="Web20">
                                                    <Items>
                                                        <telerik:DropDownListItem Text="Bloomberg" />
                                                        <telerik:DropDownListItem Text="Reuters" />
                                                        <telerik:DropDownListItem Text="RealEdge" />
                                                    </Items>
                                                </telerik:RadDropDownList>
                                            </td>
                                        </tr>--%>
                                        <tr>
                                            <td class="style1" id="Td44">
                                                <%--<asp:Button runat="server" Text="Compute Cash Flow" ID="btnComputeCashFlowA" OnClick="btnSubmit1_Click"></asp:Button>--%>
                                                <telerik:RadButton ID="btnComputeCashFlowA" runat="server" Skin="Web20" Text="Compute Cash Flow" OnClick="btnComputeCashFlowA_Click"></telerik:RadButton>
                                            </td>
                                            <td class="style1" id="Td46">
                                                <telerik:RadPanelBar runat="server" ID="RadPanelBar1" SkinID="Web20">
                                                    <Items>

                                                        <telerik:RadPanelItem Expanded="False" Text="Cash Flow Grid" SkinID="Web20">
                                                            <ContentTemplate>

                                                                <telerik:RadGrid ID="grdCalculatedDates1" runat="server" AutoGenerateColumns="false" Skin="Web20" AllowSorting="True" OnItemCommand="grdCalculatedDates1_ItemCommand" OnSortCommand="grdCalculatedDates1_SortCommand" OnItemDataBound="grdCalculatedDates1_ItemDataBound">
                                                                    <ExportSettings HideStructureColumns="true">
                                                                    </ExportSettings>
                                                                    <GroupingSettings CaseSensitive="false"></GroupingSettings>
                                                                    <MasterTableView Width="100%" CommandItemDisplay="Top" AutoGenerateColumns="false">
                                                                        <CommandItemSettings ShowAddNewRecordButton="false" />
                                                                        <PagerStyle Mode="NextPrevNumericAndAdvanced"></PagerStyle>
                                                                        <CommandItemSettings ShowExportToWordButton="true" ShowExportToCsvButton="true"></CommandItemSettings>
                                                                        <Columns>
                                                                            <telerik:GridBoundColumn DataField="CalculatedDate" HeaderText="Date" DataFormatString="{0:dd-MM-yyyy}">
                                                                            </telerik:GridBoundColumn>
                                                                           <%-- <telerik:GridTemplateColumn HeaderText="Day">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblDay" runat="server"></asp:Label>
                                                                                </ItemTemplate>
                                                                                <ItemStyle Width="50px"></ItemStyle>
                                                                            </telerik:GridTemplateColumn>--%>
                                                                            <telerik:GridTemplateColumn HeaderText="Fraction">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblFraction" runat="server" Text='<%# Eval("Fraction") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                                <ItemStyle Width="50px"></ItemStyle>
                                                                            </telerik:GridTemplateColumn>
                                                                        </Columns>
                                                                    </MasterTableView>
                                                                </telerik:RadGrid>
                                                            </ContentTemplate>
                                                        </telerik:RadPanelItem>
                                                    </Items>
                                                </telerik:RadPanelBar>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <%--<asp:Button runat="server" Text="Clear" ID="btnClearA" OnClick="btnClearA_Click"
                                            Width="100%"></asp:Button>--%>
                                                <telerik:RadButton ID="btnClearA" runat="server" Skin="Web20" OnClick="btnClearA_Click" Text="Clear"></telerik:RadButton>
                                            </td>
                                            <td class="style1" id="Td48">
                                                <%--<asp:Button runat="server" Text="Add Trade/Quote" ID="btnAddTradeQuote" OnClick="btnAddTradeQuote1_Click"></asp:Button>--%>
                                                <telerik:RadButton ID="btnAddTradeQuote" runat="server" Skin="Web20" OnClick="btnAddTradeQuote_Click" Text="Add Trade/Quote"></telerik:RadButton>
                                                <%--<asp:Button ID="btnAddTradeQuote" runat="server" OnClick="btnAddTradeQuote_Click" />--%>
                                                <telerik:RadWindow ID="confirmWindow1" runat="server" VisibleTitlebar="false" VisibleStatusbar="false"
                                                    Modal="true" Behaviors="None" Height="150px" Width="300px">
                                                    <ContentTemplate>
                                                        <div style="padding-left: 30px; padding-top: 20px; width: 200px; float: left; color: black;">
                                                            <asp:Label ID="lblConfirm1" Font-Size="14px" Text="Are you sure you want to add duplicate record to database?"
                                                                runat="server" Skin="Web20"></asp:Label>
                                                            <br />
                                                            <br />
                                                            <telerik:RadButton ID="RadButtonYes1" runat="server" Text="Yes" AutoPostBack="false"
                                                                OnClientClicked="confirmResult1" Skin="Web20">
                                                            </telerik:RadButton>
                                                            <telerik:RadButton ID="RadButtonNo1" runat="server" Text="No" AutoPostBack="false"
                                                                OnClientClicked="confirmResult1" Skin="Web20">
                                                            </telerik:RadButton>
                                                        </div>
                                                    </ContentTemplate>
                                                </telerik:RadWindow>
                                                <br />
                                                <asp:Label ID="lblTradeQuoteStatusA" runat="server"></asp:Label>
                                            </td>
                                        </tr>

                                    </table>


                                </td>
                                <td width="28%">
                                    <table>
                                        <tr>
                                            <td id="Td81">
                                                <asp:Label runat="server" Text="Loan Name" BorderStyle="None" ID="Label1"></asp:Label>
                                            </td>
                                            <td class="style1" id="Td82">
                                                <%--<asp:TextBox runat="server" ID="txtLoanNameB"></asp:TextBox>--%>
                                                <%--<telerik:RadTextBox ID="txtLoanNameB" runat="server" Skin="Web20"></telerik:RadTextBox>--%>
                                                <telerik:RadComboBox ID="txtLoanNameB" AllowCustomText="true" runat="server" Width="160"
                                                    Height="200px" OnSelectedIndexChanged="txtLoanNameB_SelectedIndexChanged" EmptyMessage="Search for loan..."
                                                    Skin="Web20" AutoPostBack="true">
                                                </telerik:RadComboBox>

                                                <asp:HiddenField ID="hfSelectedLoanB" runat="server" />
                                            </td>
                                        </tr>
                                        <%--<tr>
                                            <td>
                                                <label id="lblNameB" runat="server" visible="false">Loan Name</label>
                                            </td>
                                            <td>
                                                <label id="lblLoanNameB" runat="server" visible="false"></label>
                                            </td>
                                        </tr>--%>

                                        <tr>
                                            <td id="Td85">Trade Date
                                            </td>
                                            <td class="style1" id="Td86">
                                                <%--<telerik:RadTextBox runat="server" ID="txtBoxTradeDate2" CssClass="selecte_date"></telerik:RadTextBox>--%>
                                                <telerik:RadDateTimePicker ID="txtBoxTradeDate2" runat="server" AutoPostBack="true" OnSelectedDateChanged="txtBoxTradeDate2_SelectedDateChanged" AutoPostBackControl="Both"
                                                    MinDate="01/01/1000" MaxDate="01/01/3000" Skin="Web20" DateInput-DisplayDateFormat="dd/MM/yyyy HH:mm" DateInput-DateFormat="dd/MM/yyyy HH:mm">
                                                    <Calendar>
                                                        <SpecialDays>
                                                            <telerik:RadCalendarDay Repeatable="Today" ItemStyle-CssClass="rcToday">
                                                            </telerik:RadCalendarDay>
                                                        </SpecialDays>
                                                    </Calendar>
                                                </telerik:RadDateTimePicker>
                                                <%-- <telerik:RadCalendar ID="txtBoxTradeDateExtender2" runat="server" TargetControlID="txtBoxTradeDate2"
                                            Enabled="True">
                                        </telerik:RadCalendar>--%>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td id="Td87">Settlement Date
                                            </td>
                                            <td class="style1" id="Td88">
                                                <%--<asp:TextBox runat="server" ID="txtBoxSettlementDate2"></asp:TextBox>--%>
                                                <telerik:RadDatePicker ID="txtBoxSettlementDate2" runat="server" AutoPostBack="true"
                                                    MinDate="01/01/1000" MaxDate="01/01/3000" Skin="Web20">
                                                    <Calendar>
                                                        <SpecialDays>
                                                            <telerik:RadCalendarDay Repeatable="Today" ItemStyle-CssClass="rcToday">
                                                            </telerik:RadCalendarDay>
                                                        </SpecialDays>
                                                    </Calendar>
                                                </telerik:RadDatePicker>
                                                <%--  <asp:CalendarExtender ID="CalendarExtender3" runat="server" TargetControlID="txtBoxSettlementDate2"
                                            Enabled="True">
                                        </asp:CalendarExtender>--%>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td id="Td89">Maturity Date
                                            </td>
                                            <td class="style1" id="Td90">
                                                <%--<asp:TextBox runat="server" ID="txtBoxMaturityDateB"></asp:TextBox>--%>
                                                <telerik:RadDatePicker ID="txtBoxMaturityDateB" runat="server" AutoPostBack="true"
                                                    MinDate="01/01/1000" MaxDate="01/01/3000" Skin="Web20" OnSelectedDateChanged="txtBoxMaturityDateB_SelectedDateChanged">
                                                    <Calendar>
                                                        <SpecialDays>
                                                            <telerik:RadCalendarDay Repeatable="Today" ItemStyle-CssClass="rcToday">
                                                            </telerik:RadCalendarDay>
                                                        </SpecialDays>
                                                    </Calendar>
                                                </telerik:RadDatePicker>
                                                <%-- <asp:CalendarExtender ID="CalendarExtender4" runat="server" TargetControlID="txtBoxMaturityDateB"
                                            Enabled="True">
                                        </asp:CalendarExtender>--%>
                                            </td>
                                        </tr>
                                        <%--<tr>
                                            <td id="Td47">Country
                                            </td>
                                            <td class="style1" id="Td55">
                                                <telerik:RadComboBox ID="ddlRegionB" AllowCustomText="true" runat="server" Width="160"
                                                    Height="200px" OnSelectedIndexChanged="ddlRegionB_SelectedIndexChanged" EmptyMessage="Country..."
                                                    Skin="Web20" AutoPostBack="true">
                                                </telerik:RadComboBox>

                                            </td>
                                        </tr>--%>
                                        <tr>
                                            <td id="Td93">
                                                <asp:Label runat="server" Text="Currency" BorderStyle="None" ID="Label3"></asp:Label>
                                            </td>
                                            <td class="style1" id="Td94">
                                                <%--<asp:TextBox runat="server" ID="txtCurrencyB"></asp:TextBox>--%>
                                                <telerik:RadTextBox ID="txtCurrencyB" runat="server" Skin="Web20"></telerik:RadTextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td id="Td83">
                                                <asp:Label runat="server" Text="Margin" BorderStyle="None" ID="Label2"></asp:Label>
                                            </td>
                                            <td class="style1" id="Td84">
                                                <%--<asp:TextBox runat="server" ID="txtInterestRateB">
                                        </asp:TextBox>--%>
                                                <telerik:RadTextBox ID="txtInterestRateB" runat="server" Skin="Web20"></telerik:RadTextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td id="Td95">
                                                <asp:Label runat="server" Text="Coupon Frequency" BorderStyle="None" ID="Label4"></asp:Label>
                                            </td>
                                            <td class="style1" id="Td96">
                                                <telerik:RadTextBox runat="server" ID="txtBoxCouponFrequencyLoanB"></telerik:RadTextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td id="Td97">Fixed/Floating
                                            </td>
                                            <td class="style1" id="Td98">
                                                <telerik:RadTextBox runat="server" ID="txtBoxLastFixingB"></telerik:RadTextBox>
                                            </td>
                                        </tr>
                                        <%--<tr>
                                            <td id="Td53">IR/Coupon
                                            </td>
                                            <td class="style1" id="Td54">
                                                <telerik:RadTextBox runat="server" ID="txtBoxIRCouponB"></telerik:RadTextBox>
                                            </td>
                                        </tr>--%>
                                        <tr>
                                            <td id="Td71">
                                                <asp:Label Text="Bid Price" ID="label17" runat="server" ForeColor="DarkBlue"></asp:Label>
                                            </td>
                                            <td class="style1" id="Td72">
                                                <telerik:RadTextBox runat="server" ID="txtBoxBidPriceB" CssClass="BidPrice" BackColor="WhiteSmoke"></telerik:RadTextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td id="Td73">
                                                <asp:Label Text="Bid Spread" ID="label18" runat="server" ForeColor="DarkBlue"></asp:Label>
                                            </td>
                                            <td class="style1" id="Td74">
                                                <telerik:RadTextBox runat="server" ID="txtBoxBidSpreadB" CssClass="bidSpread" BackColor="WhiteSmoke"></telerik:RadTextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td id="Td75">
                                                <asp:Label Text="Offer Price" ID="label19" runat="server" ForeColor="DarkBlue"></asp:Label>
                                            </td>
                                            <td class="style1" id="Td76">
                                                <telerik:RadTextBox runat="server" ID="txtBoxOfferPriceB" CssClass="OfferPrice" BackColor="WhiteSmoke"></telerik:RadTextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td id="Td77">
                                                <asp:Label Text="Offer Spread" ID="label20" runat="server" ForeColor="DarkBlue"></asp:Label>
                                            </td>
                                            <td class="style1" id="Td78">
                                                <telerik:RadTextBox runat="server" ID="txtBoxOfferSpreadB" CssClass="OfferSpread" BackColor="WhiteSmoke"></telerik:RadTextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td id="Td91">Notional
                                            </td>
                                            <td class="style1" id="Td92">
                                                <telerik:RadTextBox runat="server" ID="txtBoxNotional2" OnTextChanged="txtBoxNotional2_TextChanged" AutoPostBack="true"></telerik:RadTextBox>
                                            </td>
                                        </tr>
                                        <%--<tr>
                                            <td id="Td61">Discount Margin
                                            </td>
                                            <td class="style1" id="Td62">
                                                <telerik:RadTextBox runat="server" ID="txtDiscountMarginB" CssClass="DiscountMargin"></telerik:RadTextBox>
                                            </td>
                                        </tr>--%>
                                        <telerik:RadTextBox runat="server" Visible="false" ID="txtDiscountMarginB" CssClass="DiscountMargin"></telerik:RadTextBox>
                                        <tr style="display: none">
                                            <td id="Td63">IRR
                                            </td>
                                            <td class="style1" id="Td64">
                                                <telerik:RadTextBox runat="server" ID="txtBoxIRRB"></telerik:RadTextBox>
                                            </td>
                                        </tr>
                                        <tr style="display: none">
                                            <td id="Td9">Average Life
                                            </td>
                                            <td class="style1" id="Td10">
                                                <%--<asp:TextBox runat="server" ID="txtIRRA"></asp:TextBox>--%>
                                                <telerik:RadTextBox ID="txtAvgLifeB" runat="server" Skin="Web20"></telerik:RadTextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td id="Td65">
                                                <asp:Label Text="Ave.Life NonDisc" ID="label21" runat="server" ForeColor="DarkBlue"></asp:Label>
                                            </td>
                                            <td class="style1" id="Td66">
                                                <%--<asp:TextBox runat="server" ID="txtBoxAveLifNonDiscB" CssClass="AverageLifeNonDisc"></asp:TextBox>--%>
                                                <telerik:RadTextBox ID="txtBoxAveLifNonDiscB" runat="server" Skin="Web20" BackColor="WhiteSmoke"></telerik:RadTextBox>
                                            </td>
                                        </tr>
                                        <tr style="display: none">
                                            <td id="Td67">Ave.Life Disc
                                            </td>
                                            <td class="style1" id="Td68">
                                                <telerik:RadTextBox runat="server" ID="txtBoxAveLifDiscB"></telerik:RadTextBox>
                                            </td>
                                        </tr>
                                        <tr style="display: none">
                                            <td id="Td69">Ave.Life Risky Disc
                                            </td>
                                            <td class="style1" id="Td70">
                                                <telerik:RadTextBox runat="server" ID="txtBoxAveLifRiskyDiscB"></telerik:RadTextBox>
                                            </td>
                                        </tr>

                                        <tr>
                                            <td id="Td105">CounterParty
                                            </td>
                                            <td class="style1" id="Td106">
                                                <%--<telerik:RadTextBox runat="server" ID="txtBoxCounterPartyB"></telerik:RadTextBox>--%>
                                                <telerik:RadDropDownList ID="ddlCounterPartyB" runat="server" Skin="Web20">
                                                </telerik:RadDropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td id="Td107">Check if traded
                                            </td>
                                            <td class="style1" id="Td108">
                                                <asp:CheckBox ID="chkBoxTradedB" runat="server"></asp:CheckBox>
                                            </td>
                                        </tr>
                                        <%--<tr>
                                            <td id="Td5">
                                                <asp:Label runat="server" Text="Curve" BorderStyle="None" ID="Label13"></asp:Label>
                                            </td>
                                            <td class="style1" id="Td6">
                                               
                                                <telerik:RadDropDownList ID="ddlCurveB" runat="server" Skin="Web20">
                                                    <Items>
                                                        <telerik:DropDownListItem Text="Bloomberg" />
                                                        <telerik:DropDownListItem Text="Reuters" />
                                                        <telerik:DropDownListItem Text="RealEdge" />
                                                    </Items>
                                                </telerik:RadDropDownList>
                                            </td>
                                        </tr>--%>
                                        <tr>
                                            <td class="style1" id="Td100">
                                                <%--<asp:Button runat="server" Text="Compute Cash Flow" ID="Button1" OnClick="btnSubmit2_Click"></asp:Button>--%>
                                                <telerik:RadButton ID="btnComputeCashFlowB" runat="server" OnClick="btnComputeCashFlowB_Click" Skin="Web20" Text="Compute Cash Flow"></telerik:RadButton>
                                            </td>
                                            <td class="style1" id="Td102">
                                                <telerik:RadPanelBar runat="server" ID="RadPanelBar2" SkinID="Web20">
                                                    <Items>

                                                        <telerik:RadPanelItem Expanded="False" Text="Cash Flow Grid" SkinID="Web20">
                                                            <ContentTemplate>
                                                                <telerik:RadGrid ID="grdCalculatedDates2" runat="server" AutoGenerateColumns="false" Skin="Web20" AllowSorting="True" OnItemCommand="grdCalculatedDates2_ItemCommand" OnSortCommand="grdCalculatedDates2_SortCommand" OnItemDataBound="grdCalculatedDates2_ItemDataBound">
                                                                    <ExportSettings HideStructureColumns="true">
                                                                    </ExportSettings>
                                                                    <GroupingSettings CaseSensitive="false"></GroupingSettings>
                                                                    <MasterTableView Width="100%" CommandItemDisplay="Top" AutoGenerateColumns="false">
                                                                        <CommandItemSettings ShowAddNewRecordButton="false" />
                                                                        <PagerStyle Mode="NextPrevNumericAndAdvanced"></PagerStyle>
                                                                        <CommandItemSettings ShowExportToWordButton="true" ShowExportToCsvButton="true"></CommandItemSettings>
                                                                        <Columns>
                                                                            <telerik:GridBoundColumn DataField="CalculatedDate" HeaderText="Date" DataFormatString="{0:dd-MM-yyyy}">
                                                                            </telerik:GridBoundColumn>
                                                                            <%--<telerik:GridTemplateColumn HeaderText="Day">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblDay" runat="server"></asp:Label>
                                                                                </ItemTemplate>
                                                                                <ItemStyle Width="50px"></ItemStyle>
                                                                            </telerik:GridTemplateColumn>--%>
                                                                            <telerik:GridTemplateColumn HeaderText="Fraction">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblFraction" runat="server" Text='<%# Eval("Fraction") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                                <ItemStyle Width="50px"></ItemStyle>
                                                                            </telerik:GridTemplateColumn>
                                                                        </Columns>
                                                                    </MasterTableView>
                                                                </telerik:RadGrid>
                                                            </ContentTemplate>
                                                        </telerik:RadPanelItem>
                                                    </Items>
                                                </telerik:RadPanelBar>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td id="Td103">
                                                <%--<asp:Button runat="server" Text="Clear" ID="btnClearB" OnClick="btnClearB_Click"
                                            Width="100%"></asp:Button>--%>
                                                <telerik:RadButton ID="btnClearB" runat="server" Skin="Web20" OnClick="btnClearB_Click" Text="Clear"></telerik:RadButton>
                                            </td>
                                            <td class="style1" id="Td104">
                                                <%--<asp:Button runat="server" Text="Add Trade/Quote" ID="btnAddTradeQuoteB" OnClick="btnAddTradeQuote2_Click"></asp:Button>--%>
                                                <telerik:RadButton ID="btnAddTradeQuoteB" runat="server" Skin="Web20" OnClick="btnAddTradeQuote2_Click" Text="Add Trade/Quote"></telerik:RadButton>
                                                <br />
                                                <asp:Label ID="lblTradeQuoteStatusB" runat="server"></asp:Label>
                                            </td>
                                        </tr>

                                    </table>
                                </td>
                                <td>
                                    <table>
                                        <tr>
                                            <td id="Td109">
                                                <asp:Label runat="server" Text="Loan Name" BorderStyle="None" ID="Label5"></asp:Label>
                                            </td>
                                            <td class="style1" id="Td110">
                                                <%--<asp:TextBox runat="server" ID="txtLoanNameC"></asp:TextBox>--%>
                                                <%--<telerik:RadTextBox ID="txtLoanNameC" runat="server" Skin="Web20"></telerik:RadTextBox>--%>
                                                <telerik:RadComboBox ID="txtLoanNameC" AllowCustomText="true" runat="server" Width="160"
                                                    Height="200px" OnSelectedIndexChanged="txtLoanNameC_SelectedIndexChanged" EmptyMessage="Search for loan..."
                                                    Skin="Web20" AutoPostBack="true">
                                                </telerik:RadComboBox>
                                                <asp:HiddenField ID="hfSelectedLoanC" runat="server" />
                                            </td>
                                        </tr>
                                        <%--<tr>
                                            <td>
                                                <label id="lblNameC" runat="server" visible="false">Loan Name</label>
                                            </td>
                                            <td>
                                                <label id="lblLoanNameC" runat="server" visible="false"></label>
                                            </td>
                                        </tr>--%>

                                        <tr>
                                            <td id="Td113">Trade Date
                                            </td>
                                            <td class="style1" id="Td114">
                                                <%--<telerik:RadTextBox runat="server" ID="txtBoxTradeDate3" CssClass="selecte_date"></telerik:RadTextBox>--%>
                                                <telerik:RadDateTimePicker ID="txtBoxTradeDate3" runat="server" AutoPostBack="true" OnSelectedDateChanged="txtBoxTradeDate3_SelectedDateChanged" AutoPostBackControl="Both"
                                                    MinDate="01/01/1000" MaxDate="01/01/3000" Skin="Web20" DateInput-DisplayDateFormat="dd/MM/yyyy HH:mm" DateInput-DateFormat="dd/MM/yyyy HH:mm">
                                                    <Calendar>
                                                        <SpecialDays>
                                                            <telerik:RadCalendarDay Repeatable="Today" ItemStyle-CssClass="rcToday">
                                                            </telerik:RadCalendarDay>
                                                        </SpecialDays>
                                                    </Calendar>
                                                </telerik:RadDateTimePicker>
                                                <%--<telerik:RadCalendar ID="txtBoxTradeDateExtender3" runat="server" TargetControlID="txtBoxTradeDate3"
                                            >
                                        </telerik:RadCalendar>--%>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td id="Td115">Settlement Date
                                            </td>
                                            <td class="style1" id="Td116">
                                                <%--<asp:TextBox runat="server" ID="txtBoxSettlementDate3"></asp:TextBox>--%>
                                                <telerik:RadDatePicker ID="txtBoxSettlementDate3" runat="server" AutoPostBack="true"
                                                    MinDate="01/01/1000" MaxDate="01/01/3000" Skin="Web20">
                                                    <Calendar>
                                                        <SpecialDays>
                                                            <telerik:RadCalendarDay Repeatable="Today" ItemStyle-CssClass="rcToday">
                                                            </telerik:RadCalendarDay>
                                                        </SpecialDays>
                                                    </Calendar>
                                                </telerik:RadDatePicker>
                                                <%--  <asp:CalendarExtender ID="CalendarExtender5" runat="server" TargetControlID="txtBoxSettlementDate3"
                                            Enabled="True">
                                        </asp:CalendarExtender>--%>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td id="Td3">Maturity Date
                                            </td>
                                            <td class="style1" id="Td4">
                                                <%--<asp:TextBox runat="server" ID="txtMaturityDateC"></asp:TextBox>--%>
                                                <telerik:RadDatePicker ID="txtMaturityDateC" runat="server" AutoPostBack="true"
                                                    MinDate="01/01/1000" MaxDate="01/01/3000" Skin="Web20" OnSelectedDateChanged="txtMaturityDateC_SelectedDateChanged">
                                                    <Calendar>
                                                        <SpecialDays>
                                                            <telerik:RadCalendarDay Repeatable="Today" ItemStyle-CssClass="rcToday">
                                                            </telerik:RadCalendarDay>
                                                        </SpecialDays>
                                                    </Calendar>
                                                </telerik:RadDatePicker>
                                                <%-- <asp:CalendarExtender ID="CalendarExtender6" runat="server" TargetControlID="txtMaturityDateC"
                                            Enabled="True">
                                        </asp:CalendarExtender>--%>
                                            </td>
                                        </tr>
                                        <%--<tr>
                                            <td id="Td43">Country
                                            </td>
                                            <td class="style1" id="Td45">
                                                <telerik:RadComboBox ID="ddlRegionC" AllowCustomText="true" runat="server" Width="160"
                                                    Height="200px" OnSelectedIndexChanged="ddlRegionC_SelectedIndexChanged" EmptyMessage="Country..."
                                                    Skin="Web20" AutoPostBack="true">
                                                </telerik:RadComboBox>

                                            </td>
                                        </tr>--%>
                                        <tr>
                                            <td id="Td7">
                                                <asp:Label runat="server" Text="Currency" BorderStyle="None" ID="Label7"></asp:Label>
                                            </td>
                                            <td class="style1" id="Td8">
                                                <telerik:RadTextBox runat="server" ID="txtCurrencyC"></telerik:RadTextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td id="Td111">
                                                <asp:Label runat="server" Text="Margin" BorderStyle="None" ID="Label6"></asp:Label>
                                            </td>
                                            <td class="style1" id="Td112">
                                                <telerik:RadTextBox runat="server" ID="txtInterestRateC">
                                                </telerik:RadTextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td id="Td11">
                                                <asp:Label runat="server" Text="Coupon Frequency" BorderStyle="None" ID="Label8"></asp:Label>
                                            </td>
                                            <td class="style1" id="Td12">
                                                <telerik:RadTextBox runat="server" ID="txtBoxCouponFrequencyLoanC">
                                                </telerik:RadTextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td id="Td15">Fixed/Floating
                                            </td>
                                            <td class="style1" id="Td16">
                                                <telerik:RadTextBox runat="server" ID="txtLastFixingC"></telerik:RadTextBox>
                                            </td>
                                        </tr>
                                        <%--<tr>
                                            <td id="Td127">IR/Coupon
                                            </td>
                                            <td class="style1" id="Td128">
                                                <telerik:RadTextBox runat="server" ID="txtBoxIRCouponC"></telerik:RadTextBox>
                                            </td>
                                        </tr>--%>
                                        <tr>
                                            <td id="Td145">
                                                <asp:Label Text="Bid Price" ID="label22" runat="server" ForeColor="DarkBlue"></asp:Label>
                                            </td>
                                            <td class="style1" id="Td146">
                                                <telerik:RadTextBox runat="server" ID="txtBoxBidPriceC" CssClass="BidPrice" BackColor="WhiteSmoke"></telerik:RadTextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td id="Td147">
                                                <asp:Label Text="Bid Spread" ID="label23" runat="server" ForeColor="DarkBlue"></asp:Label>
                                            </td>
                                            <td class="style1" id="Td148">
                                                <telerik:RadTextBox runat="server" ID="txtBoxBidSpreadC" CssClass="bidSpread" BackColor="WhiteSmoke"></telerik:RadTextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td id="Td149">
                                                <asp:Label Text="Offer Price" ID="label24" runat="server" ForeColor="DarkBlue"></asp:Label>
                                            </td>
                                            <td class="style1" id="Td150">
                                                <telerik:RadTextBox runat="server" ID="txtBoxOfferPriceC" CssClass="OfferPrice" BackColor="WhiteSmoke"></telerik:RadTextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td id="Td151">
                                                <asp:Label Text="Offer Spread" ID="label25" runat="server" ForeColor="DarkBlue"></asp:Label>
                                            </td>
                                            <td class="style1" id="Td152">
                                                <telerik:RadTextBox runat="server" ID="txtBoxOfferSpreadC" CssClass="OfferSpread" BackColor="WhiteSmoke"></telerik:RadTextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td id="Td21">Notional
                                            </td>
                                            <td class="style1" id="Td22">
                                                <telerik:RadTextBox runat="server" ID="txtBoxNotional3" AutoPostBack="true" OnTextChanged="txtBoxNotional3_TextChanged"></telerik:RadTextBox>
                                            </td>
                                        </tr>
                                        <%--<tr>
                                            <td id="Td135">Discount Margin
                                            </td>
                                            <td class="style1" id="Td136">
                                                <telerik:RadTextBox runat="server" ID="txtDiscountMarginC" CssClass="DiscountMargin"></telerik:RadTextBox>
                                            </td>
                                        </tr>--%>
                                        <telerik:RadTextBox runat="server" Visible="false" ID="txtDiscountMarginC" CssClass="DiscountMargin"></telerik:RadTextBox>
                                        <tr style="display: none">
                                            <td id="Td137">IRR
                                            </td>
                                            <td class="style1" id="Td138">
                                                <telerik:RadTextBox runat="server" ID="txtBoxIRRC"></telerik:RadTextBox>
                                            </td>
                                        </tr>
                                        <tr style="display: none">
                                            <td id="Td19">Average Life
                                            </td>
                                            <td class="style1" id="Td20">
                                                <%--<asp:TextBox runat="server" ID="txtIRRA"></asp:TextBox>--%>
                                                <telerik:RadTextBox ID="txtAvgLifeC" runat="server" Skin="Web20"></telerik:RadTextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td id="Td139">
                                                <asp:Label Text="Ave.Life NonDisc" ID="label26" runat="server" ForeColor="DarkBlue"></asp:Label>
                                            </td>
                                            <td class="style1" id="Td140">
                                                <%--<asp:TextBox runat="server" ID="txtBoxAveLifNonDiscC" CssClass="AverageLifeNonDisc"></asp:TextBox>--%>
                                                <telerik:RadTextBox ID="txtBoxAveLifNonDiscC" runat="server" Skin="Web20" BackColor="WhiteSmoke"></telerik:RadTextBox>
                                            </td>
                                        </tr>
                                        <tr style="display: none">
                                            <td id="Td141">Ave.Life Disc
                                            </td>
                                            <td class="style1" id="Td142">
                                                <telerik:RadTextBox runat="server" ID="txtBoxAveLifDiscC"></telerik:RadTextBox>
                                            </td>
                                        </tr>
                                        <tr style="display: none">
                                            <td id="Td143">Ave.Life Risky Disc
                                            </td>
                                            <td class="style1" id="Td144">
                                                <telerik:RadTextBox runat="server" ID="txtBoxAveLifRiskyDiscC"></telerik:RadTextBox>
                                            </td>
                                        </tr>

                                        <tr>
                                            <td id="Td159">CounterParty
                                            </td>
                                            <td class="style1" id="Td160">
                                                <%--  <telerik:RadTextBox runat="server" ID="txtBoxCounterPartyC"></telerik:RadTextBox>--%>
                                                <telerik:RadDropDownList ID="ddlCounterPartyC" runat="server" Skin="Web20">
                                                </telerik:RadDropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td id="Td161">Check if traded
                                            </td>
                                            <td class="style1" id="Td162">
                                                <asp:CheckBox ID="chkBoxTradedC" runat="server"></asp:CheckBox>
                                            </td>
                                        </tr>
                                        <%--<tr>
                                            <td id="Td25">
                                                <asp:Label runat="server" Text="Curve" BorderStyle="None" ID="lblCurveC"></asp:Label>
                                            </td>
                                            <td class="style1" id="Td26">
                                               
                                                <telerik:RadDropDownList ID="ddlCurveSourceC" runat="server" Skin="Web20">
                                                    <Items>
                                                        <telerik:DropDownListItem Text="Bloomberg" />
                                                        <telerik:DropDownListItem Text="Reuters" />
                                                        <telerik:DropDownListItem Text="RealEdge" />
                                                    </Items>
                                                </telerik:RadDropDownList>
                                            </td>
                                        </tr>--%>
                                        <tr>
                                            <td class="style1" id="Td154">
                                                <%--<asp:Button runat="server" Text="Compute Cash Flow" ID="Button2" OnClick="btnSubmit3_Click"></asp:Button>--%>
                                                <telerik:RadButton ID="btnComputeCashFlowC" runat="server" Text="Compute Cash Flow" Skin="Web20" OnClick="btnComputeCashFlowC_Click"></telerik:RadButton>
                                            </td>
                                            <td class="style1" id="Td156">
                                                <telerik:RadPanelBar runat="server" ID="RadPanelBar3" SkinID="Web20">
                                                    <Items>

                                                        <telerik:RadPanelItem Expanded="False" Text="Cash Flow Grid" SkinID="Web20"  >
                                                            <ContentTemplate>
                                                                <telerik:RadGrid ID="grdCalculatedDates3" runat="server" AutoGenerateColumns="false" Skin="Web20" AllowSorting="True" OnItemCommand="grdCalculatedDates3_ItemCommand" OnSortCommand="grdCalculatedDates3_SortCommand" OnItemDataBound="grdCalculatedDates3_ItemDataBound"  >
                                                                    <ExportSettings HideStructureColumns="true">
                                                                    </ExportSettings>
                                                                    <GroupingSettings CaseSensitive="false"></GroupingSettings>
                                                                    <MasterTableView Width="100%" CommandItemDisplay="Top" AutoGenerateColumns="false">
                                                                        <CommandItemSettings ShowAddNewRecordButton="false" />
                                                                        <PagerStyle Mode="NextPrevNumericAndAdvanced"></PagerStyle>
                                                                        <CommandItemSettings ShowExportToWordButton="true" ShowExportToCsvButton="true"></CommandItemSettings>
                                                                        <Columns>
                                                                            <telerik:GridBoundColumn DataField="CalculatedDate" HeaderText="Date" DataFormatString="{0:dd-MM-yyyy}">
                                                                            </telerik:GridBoundColumn>
                                                                          <%--  <telerik:GridTemplateColumn HeaderText="Day">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblDay" runat="server"></asp:Label>
                                                                                </ItemTemplate>
                                                                                <ItemStyle Width="50px"></ItemStyle>
                                                                            </telerik:GridTemplateColumn>--%>
                                                                            <telerik:GridTemplateColumn HeaderText="Fraction">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblFraction" runat="server" Text='<%# Eval("Fraction") %>'></asp:Label>
                                                                                </ItemTemplate>
                                                                                <ItemStyle Width="50px"></ItemStyle>
                                                                            </telerik:GridTemplateColumn>
                                                                        </Columns>
                                                                    </MasterTableView>
                                                                </telerik:RadGrid>
                                                            </ContentTemplate>
                                                        </telerik:RadPanelItem>
                                                    </Items>
                                                </telerik:RadPanelBar>

                                            </td>
                                        </tr>
                                        <tr>
                                            <td id="Td157">
                                                <%-- <asp:Button runat="server" Text="Clear" ID="btnClearC" OnClick="btnClearC_Click"
                                            Width="100%"></asp:Button>--%>
                                                <telerik:RadButton ID="btnClearC" runat="server" Text="Clear" OnClick="btnClearC_Click" Skin="Web20"></telerik:RadButton>
                                            </td>
                                            <td class="style1" id="Td158">
                                                <%--<asp:Button runat="server" Text="Add Trade/Quote" ID="btnAddTradeQuoteC" OnClick="btnAddTradeQuote3_Click"></asp:Button>--%>
                                                <telerik:RadButton ID="btnAddTradeQuoteC" runat="server" Text="Add Trade/Quote" OnClick="btnAddTradeQuote3_Click" Skin="Web20"></telerik:RadButton>
                                                <br />
                                                <asp:Label ID="lblTradeQuoteStatusC" runat="server"></asp:Label>
                                            </td>
                                        </tr>

                                    </table>
                                </td>
                            </tr>

                        </table>
                        <%-- </telerik:RadAjaxPanel>--%>
                        <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel2" runat="server">
                        </telerik:RadAjaxLoadingPanel>
                    </telerik:RadPageView>
                    <telerik:RadPageView runat="server" ID="RadPageView6" BackColor="White" Width="756px" CssClass="radHeight">
                        <br />
                        <telerik:RadPanelBar ID="pnlSystemGraphs" runat="server" Width="1290px" Height="1200px" Skin="Web20">
                            <Items>
                                <telerik:RadPanelItem runat="server" Text="Compact Loans">
                                    <ContentTemplate>
                                        <table style="width: 100%; margin-left: 10px; margin-right: 10px">
                                            <tr>
                                                <td>

                                                    <div style="text-align: center; font-size: 25px;">
                                                        <asp:Label ID="lblLegend" runat="server" ForeColor="Black" Font-Bold="true" st></asp:Label>
                                                    </div>
                                                    <br />
                                                    <%-- <telerik:RadChart runat="server" ID="RadHtmlChart1" Width="640px" Height="480px">
                        </telerik:RadChart>--%>

                                                    <%-- <telerik:RadChart ID="RadHtmlChart1" runat="server" Width="1000px" Height="400px" SkinID="Web20" Transitions="true">

                                                        <Appearance>
                                                            <FillStyle MainColor="249, 250, 251">
                                                            </FillStyle>
                                                            <Border Color="160, 170, 182" />
                                                        </Appearance>
                                                    </telerik:RadChart>--%>

                                                    <telerik:RadHtmlChart ID="RadHtmlChart1" runat="server" Width="1000px" Height="400px" SkinID="Web20" Transitions="true">
                                                        <PlotArea>
                                                            <XAxis BaseUnit="days">
                                                                <TitleAppearance Position="Center" Text="End Date" />
                                                                <LabelsAppearance DataFormatString="d">
                                                                </LabelsAppearance>
                                                                <MajorGridLines Color="#EFEFEF" Width="1"></MajorGridLines>
                                                                <MinorGridLines Color="#F7F7F7" Width="1"></MinorGridLines>
                                                            </XAxis>
                                                            <YAxis>
                                                                <MajorGridLines Color="#EFEFEF" Width="1" />
                                                                <MinorGridLines Color="#F7F7F7" Width="1" />
                                                                <TitleAppearance Position="Center" Text="Notionals" />
                                                            </YAxis>
                                                        </PlotArea>
                                                        <ChartTitle Text=" Loan Repayment Profile">
                                                            <Appearance Align="Center" Position="Top" />
                                                        </ChartTitle>
                                                    </telerik:RadHtmlChart>
                                                    <telerik:RadToolTipManager ID="RadToolTipManager1" runat="server"
                                                        Width="200px" Animation="None" Position="TopCenter" EnableShadow="true"
                                                        ToolTipZoneID="CHT_AvgHistogramme" AutoTooltipify="true">
                                                    </telerik:RadToolTipManager>
                                                </td>
                                            </tr>
                                        </table>
                                    </ContentTemplate>
                                </telerik:RadPanelItem>
                                <telerik:RadPanelItem runat="server" Text="Database Graphs">
                                    <ContentTemplate>
                                        <table>
                                            <tr>
                                                <td>
                                                    <telerik:RadHtmlChart runat="server" ID="PieChart1" Width="550" Height="400" Transitions="true">
                                                        <Appearance>
                                                            <FillStyle BackgroundColor="White"></FillStyle>
                                                        </Appearance>
                                                        <ChartTitle Text="Percentage of Loans from Regions in Loans Database">
                                                            <Appearance Align="Center" BackgroundColor="White" Position="Top">
                                                            </Appearance>
                                                        </ChartTitle>
                                                        <Legend>
                                                            <Appearance BackgroundColor="White" Position="Right" Visible="true">
                                                            </Appearance>
                                                        </Legend>
                                                        <PlotArea>
                                                            <Appearance>
                                                                <FillStyle BackgroundColor="White"></FillStyle>
                                                            </Appearance>
                                                            <Series>
                                                                <telerik:PieSeries StartAngle="90">
                                                                    <LabelsAppearance DataFormatString="{0} %">
                                                                    </LabelsAppearance>
                                                                    <TooltipsAppearance Color="White" DataFormatString="{0} %"></TooltipsAppearance>

                                                                </telerik:PieSeries>
                                                            </Series>
                                                        </PlotArea>
                                                    </telerik:RadHtmlChart>
                                                </td>
                                                <td>
                                                    <telerik:RadHtmlChart runat="server" ID="PieChart2" Width="550" Height="400" Transitions="true">
                                                        <Appearance>
                                                            <FillStyle BackgroundColor="White"></FillStyle>
                                                        </Appearance>
                                                        <ChartTitle Text="Number of  Quotes and Trades">
                                                            <Appearance Align="Center" BackgroundColor="White" Position="Top">
                                                            </Appearance>
                                                        </ChartTitle>
                                                        <Legend>
                                                            <Appearance BackgroundColor="White" Position="Right" Visible="true">
                                                            </Appearance>
                                                        </Legend>
                                                        <PlotArea>
                                                            <Appearance>
                                                                <FillStyle BackgroundColor="White"></FillStyle>
                                                            </Appearance>
                                                            <Series>
                                                                <telerik:PieSeries StartAngle="90">
                                                                    <LabelsAppearance DataFormatString="{0} %">
                                                                    </LabelsAppearance>
                                                                    <TooltipsAppearance Color="White" DataFormatString="{0} %"></TooltipsAppearance>

                                                                </telerik:PieSeries>
                                                            </Series>
                                                        </PlotArea>
                                                    </telerik:RadHtmlChart>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <telerik:RadHtmlChart runat="server" ID="PieChart3" Width="550" Height="400" Transitions="true">
                                                        <Appearance>
                                                            <FillStyle BackgroundColor="White"></FillStyle>
                                                        </Appearance>
                                                        <ChartTitle Text="Number of historical records with each counterparty">
                                                            <Appearance Align="Center" BackgroundColor="White" Position="Top">
                                                            </Appearance>
                                                        </ChartTitle>
                                                        <Legend>
                                                            <Appearance BackgroundColor="White" Position="Right" Visible="true">
                                                            </Appearance>
                                                        </Legend>
                                                        <PlotArea>
                                                            <Appearance>
                                                                <FillStyle BackgroundColor="White"></FillStyle>
                                                            </Appearance>
                                                            <Series>
                                                                <telerik:PieSeries StartAngle="90">
                                                                    <LabelsAppearance DataFormatString="{0} %">
                                                                    </LabelsAppearance>
                                                                    <TooltipsAppearance Color="White" DataFormatString="{0} %"></TooltipsAppearance>

                                                                </telerik:PieSeries>
                                                            </Series>
                                                        </PlotArea>
                                                    </telerik:RadHtmlChart>
                                                </td>
                                            </tr>
                                        </table>
                                    </ContentTemplate>
                                </telerik:RadPanelItem>
                            </Items>
                        </telerik:RadPanelBar>

                    </telerik:RadPageView>
                    <telerik:RadPageView ID="RadPageView8" runat="server" Height="700px" CssClass="radHeight">
                        <%--      <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel5" runat="server" Skin="Web20">
                        </telerik:RadAjaxLoadingPanel>
                        <telerik:RadAjaxPanel ID="RadAjaxPanel2" runat="server" LoadingPanelID="RadAjaxLoadingPanel5">--%>
                        <table style="width: 100%; margin-left: 10px;">
                            <tr>
                                <td>

                                    <table style="border-color: blue; margin-top: 10px" width="1000px">

                                        <tr>
                                            <td style="margin-top: 10px; width: 150px">
                                                <label>Loan Code</label></td>
                                            <td style="width: 150px">

                                                <telerik:RadComboBox ID="ddlLoanDetailsCode" AllowCustomText="true" runat="server" Width="160"
                                                    Height="200px" OnSelectedIndexChanged="ddlLoanDetailsCode_SelectedIndexChanged" EmptyMessage="Search for loan..."
                                                    Skin="Web20" AutoPostBack="true">
                                                </telerik:RadComboBox>
                                                <asp:HiddenField ID="HiddenField1" runat="server" />
                                                <%--<asp:Button ID="btnHidden" runat="server" Style="display: none;" OnClick="btnHidden_Click" Text="" />--%></td>

                                            <td style="width: 150px">
                                                <label>Fixed / Floating</label>
                                            </td>
                                            <td style="width: 150px">
                                                <telerik:RadTextBox Enabled="false" ID="txtLoanDetailFixedOrFloating" runat="server" Skin="Web20">
                                                </telerik:RadTextBox>
                                            </td>
                                            <td rowspan="7" style="width: 150px; vertical-align: top;">
                                                <table width="80%" style="border: 1px solid black;">
                                                    <tr>
                                                        <td colspan="2" style="width: 150px; text-align: center">
                                                            <label>Street Convention</label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <telerik:RadTextBox Width="100px" ID="txtLoanDetailStreet1" runat="server" Skin="Web20" BackColor="Pink"></telerik:RadTextBox>
                                                        </td>
                                                        <td>
                                                            <telerik:RadTextBox Width="100px" Enabled="false" ID="txtLoanDetailStreet2" runat="server" Skin="Web20"></telerik:RadTextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="vertical-align: top; text-align: center">
                                                            <img src="Images/arrow-down.png" style="height: 50px; width: 50px" />
                                                        </td>
                                                        <td style="vertical-align: bottom; text-align: center">
                                                            <img src="Images/arrow-up.png" style="height: 50px; width: 50px" /></td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <telerik:RadTextBox Width="100px" Enabled="false" ID="txtLoanDetailStreet3" runat="server" Skin="Web20"></telerik:RadTextBox>
                                                        </td>
                                                        <td>
                                                            <telerik:RadTextBox Width="100px" ID="txtLoanDetailStreet4" runat="server" Skin="Web20" BackColor="Pink"></telerik:RadTextBox>
                                                        </td>
                                                    </tr>
                                                </table>

                                            </td>

                                        </tr>
                                        <tr>
                                            <td>
                                                <label>Price</label>
                                            </td>
                                            <td>
                                                <telerik:RadTextBox ID="txtLoanDetailPrice" runat="server" Skin="Web20" AutoPostBack="true" OnTextChanged="txtLoanDetailPrice_TextChanged"></telerik:RadTextBox>

                                            </td>
                                            <td>
                                                <label>Trade Date</label>
                                            </td>
                                            <td>
                                                <%-- <telerik:RadTextBox ID="txtLoanDetailTradeDate" runat="server" Skin="Web20" AutoPostBack="true" OnTextChanged="txtLoanDetailTradeDate_TextChanged"></telerik:RadTextBox>--%>

                                                <telerik:RadDateTimePicker ID="txtLoanDetailTradeDate" runat="server" AutoPostBack="true" OnSelectedDateChanged="txtLoanDetailTradeDate_SelectedDateChanged" AutoPostBackControl="Both"
                                                    MinDate="01/01/1000" MaxDate="01/01/3000" Skin="Web20" Enabled="false" DateInput-DisplayDateFormat="dd/MM/yyyy HH:mm" DateInput-DateFormat="dd/MM/yyyy HH:mm">
                                                    <Calendar>
                                                        <SpecialDays>
                                                            <telerik:RadCalendarDay Repeatable="Today" ItemStyle-CssClass="rcToday">
                                                            </telerik:RadCalendarDay>
                                                        </SpecialDays>
                                                    </Calendar>
                                                </telerik:RadDateTimePicker>
                                            </td>

                                        </tr>
                                        <tr>
                                            <td>
                                                <label>Spread</label></td>
                                            <td>
                                                <telerik:RadTextBox ID="txtLoanDetailSpread" runat="server" Skin="Web20" AutoPostBack="true" OnTextChanged="txtLoanDetailSpread_TextChanged"></telerik:RadTextBox>
                                            </td>
                                            <td>
                                                <label>Settlement Date</label></td>
                                            <td>
                                                <%--   <telerik:RadTextBox ID="txtLoanDetailSettlementDate" runat="server" Skin="Web20" AutoPostBack="true"></telerik:RadTextBox>--%>
                                                <telerik:RadDatePicker ID="txtLoanDetailSettlementDate" runat="server" AutoPostBack="true" Enabled="false"
                                                    MinDate="01/01/1000" MaxDate="01/01/3000" Skin="Web20">
                                                    <Calendar>
                                                        <SpecialDays>
                                                            <telerik:RadCalendarDay Repeatable="Today" ItemStyle-CssClass="rcToday">
                                                            </telerik:RadCalendarDay>
                                                        </SpecialDays>
                                                    </Calendar>
                                                </telerik:RadDatePicker>
                                                <asp:HiddenField ID="hdnLoanDetailMargin" runat="server" />
                                            </td>

                                        </tr>
                                        <tr>
                                            <td>
                                                <label>IRR</label>
                                            </td>
                                            <td>
                                                <telerik:RadTextBox ID="txtLoanDetailIRR" runat="server" Enabled="false" Skin="Web20"></telerik:RadTextBox>
                                            </td>
                                            <td>
                                                <label>Maturity Date</label>
                                            </td>
                                            <td>
                                                <telerik:RadTextBox ID="txtLoanDetailMaturityDate" runat="server" Enabled="false" Skin="Web20"></telerik:RadTextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <label>Average Life</label></td>
                                            <td>
                                                <telerik:RadTextBox ID="txtLoanDetailAvgLife" runat="server" Enabled="false" Skin="Web20"></telerik:RadTextBox>
                                            </td>
                                            <td>
                                                <label>Notional</label></td>
                                            <td>
                                                <telerik:RadTextBox ID="txtLoanDetailNotional" runat="server" Enabled="false" Skin="Web20"></telerik:RadTextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <label>Average Life NonDisc</label>
                                            </td>
                                            <td>
                                                <telerik:RadTextBox ID="txtAvgLifeNonDisc" runat="server" Enabled="false" Skin="Web20"></telerik:RadTextBox>
                                            </td>
                                            <td>
                                                <label>Currency</label>
                                            </td>
                                            <td>
                                                <telerik:RadTextBox ID="txtLoanDetailCurrency" runat="server" Enabled="false" Skin="Web20"></telerik:RadTextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <label>Average Life Disc</label></td>
                                            <td>
                                                <telerik:RadTextBox ID="txtLoanDetailAvgLifeDisc" runat="server" Enabled="false" Skin="Web20"></telerik:RadTextBox>
                                            </td>
                                            <td>
                                                <label>Coupon Frequency </label>
                                            </td>
                                            <td>
                                                <telerik:RadTextBox ID="txtLoanDetailCouponFreq" runat="server" Enabled="false" Skin="Web20"></telerik:RadTextBox>
                                            </td>

                                        </tr>
                                        <tr>
                                            <td>
                                                <label>Average Life RiskDisc</label>
                                            </td>
                                            <td>
                                                <telerik:RadTextBox ID="txtLoanDetailAvgLifeRiskDisc" Enabled="false" runat="server" Skin="Web20"></telerik:RadTextBox>
                                            </td>
                                            <td>
                                                <label>Last Coupon Date</label></td>
                                            <td>
                                                <telerik:RadTextBox ID="txtLoanDetailLastCouponDate" Enabled="false" runat="server" Skin="Web20"></telerik:RadTextBox></td>

                                        </tr>
                                        <tr>
                                            <td></td>
                                            <td></td>
                                            <td>
                                                <label>Consideration</label></td>
                                            <td>
                                                <telerik:RadTextBox ID="txtLoanDetailConsideration" runat="server" Enabled="false" Skin="Web20"></telerik:RadTextBox>
                                            </td>
                                        </tr>
                                    </table>
                                    <table style="margin-top: 20px">
                                        <tr>
                                            <td>
                                                <asp:Panel ID="pnlLoanDetail" runat="server" ScrollBars="Both" Width="1200">

                                                    <telerik:RadGrid ID="grdLoanDetail" runat="server" Skin="Web20" OnItemDataBound="grdLoanDetail_ItemDataBound" OnNeedDataSource="grdLoanDetail_NeedDataSource">
                                                        <MasterTableView AutoGenerateColumns="false">
                                                            <Columns>
                                                                <telerik:GridTemplateColumn>
                                                                    <HeaderTemplate>
                                                                        Start Date
                                                                    </HeaderTemplate>
                                                                    <ItemTemplate>
                                                                        <telerik:RadTextBox ID="txtStartDate" Width="100px" ToolTip='<%# Eval("StartDate") %>' runat="server" Text='<%# Eval("StartDate") %>'></telerik:RadTextBox>
                                                                    </ItemTemplate>
                                                                </telerik:GridTemplateColumn>
                                                                <telerik:GridTemplateColumn>
                                                                    <HeaderTemplate>
                                                                        End Date
                                                                    </HeaderTemplate>
                                                                    <ItemTemplate>
                                                                        <telerik:RadTextBox ID="txtEndDate" Width="100px" ToolTip='<%# Eval("EndDate") %>' runat="server" Text='<%# Eval("EndDate") %>'></telerik:RadTextBox>
                                                                    </ItemTemplate>
                                                                </telerik:GridTemplateColumn>
                                                                <telerik:GridTemplateColumn>
                                                                    <HeaderTemplate>
                                                                        Coup. Frac.
                                                                    </HeaderTemplate>
                                                                    <ItemTemplate>
                                                                        <telerik:RadTextBox ID="txtCoupFrac" runat="server" Width="60px" ToolTip='<%# Eval("CoupFrac") %>' Text='<%# Eval("CoupFrac") %>'></telerik:RadTextBox>
                                                                    </ItemTemplate>
                                                                </telerik:GridTemplateColumn>
                                                                <telerik:GridTemplateColumn>
                                                                    <HeaderTemplate>
                                                                        Notional
                                                                    </HeaderTemplate>
                                                                    <ItemTemplate>
                                                                        <telerik:RadTextBox ID="txtNotation" runat="server" Width="60px" ToolTip='<%# "Notional" + Convert.ToInt16( (Eval("ID"))  ) + "-" + Eval("Notation")%>' Text='<%# Eval("Notation") %>'></telerik:RadTextBox>
                                                                    </ItemTemplate>
                                                                </telerik:GridTemplateColumn>
                                                                <telerik:GridTemplateColumn>
                                                                    <HeaderTemplate>
                                                                        Amortisation
                                                                    </HeaderTemplate>
                                                                    <ItemTemplate>
                                                                        <telerik:RadTextBox ID="txtAmortisation" runat="server" Width="70px" ToolTip='<%# Eval("Amortisation") %>' Text='<%# Eval("Amortisation") %>'></telerik:RadTextBox>
                                                                    </ItemTemplate>
                                                                </telerik:GridTemplateColumn>
                                                                <telerik:GridTemplateColumn>
                                                                    <HeaderTemplate>
                                                                        Pool Factor
                                                                    </HeaderTemplate>
                                                                    <ItemTemplate>
                                                                        <telerik:RadTextBox ID="txtFactor" runat="server" Width="70px" ToolTip='<%# Eval("Factor") %>' Text='<%# Eval("Factor") %>'></telerik:RadTextBox>
                                                                    </ItemTemplate>
                                                                </telerik:GridTemplateColumn>
                                                                <telerik:GridTemplateColumn>
                                                                    <HeaderTemplate>
                                                                        Floating Rate 
                                                                    </HeaderTemplate>
                                                                    <ItemTemplate>
                                                                        <telerik:RadTextBox ID="txtFloatingRate" runat="server" Width="80px" ToolTip='<%# Eval("FloatingRate") %>' Text='<%# Eval("FloatingRate") %>'></telerik:RadTextBox>
                                                                    </ItemTemplate>
                                                                </telerik:GridTemplateColumn>
                                                                <telerik:GridTemplateColumn>
                                                                    <HeaderTemplate>
                                                                        Spread
                                                                    </HeaderTemplate>
                                                                    <ItemTemplate>
                                                                        <telerik:RadTextBox ID="txtSpread" runat="server" Width="60px" ToolTip='<%# Eval("Spread") %>' Text='<%# Eval("Spread") %>'></telerik:RadTextBox>
                                                                    </ItemTemplate>
                                                                </telerik:GridTemplateColumn>
                                                                <telerik:GridTemplateColumn>
                                                                    <HeaderTemplate>
                                                                        AllInRate
                                                                    </HeaderTemplate>
                                                                    <ItemTemplate>
                                                                        <telerik:RadTextBox ID="txtAllInRate" runat="server" Width="60px" ToolTip='<%# Eval("AllInRate") %>' Text='<%# Eval("AllInRate") %>'></telerik:RadTextBox>
                                                                    </ItemTemplate>
                                                                </telerik:GridTemplateColumn>
                                                                <telerik:GridTemplateColumn>
                                                                    <HeaderTemplate>
                                                                        Interest
                                                                    </HeaderTemplate>
                                                                    <ItemTemplate>
                                                                        <telerik:RadTextBox ID="txtInterest" runat="server" Width="60px" ToolTip='<%# Eval("Interest") %>' Text='<%# Eval("Interest") %>'></telerik:RadTextBox>
                                                                    </ItemTemplate>
                                                                </telerik:GridTemplateColumn>
                                                                <telerik:GridTemplateColumn>
                                                                    <HeaderTemplate>
                                                                        Amortisation&Int
                                                                    </HeaderTemplate>
                                                                    <ItemTemplate>
                                                                        <telerik:RadTextBox ID="txtAmortisationInt" runat="server" Width="100px" ToolTip='<%# Eval("AmortisationInt") %>' Text='<%# Eval("AmortisationInt") %>'></telerik:RadTextBox>
                                                                    </ItemTemplate>
                                                                </telerik:GridTemplateColumn>
                                                                <telerik:GridTemplateColumn>
                                                                    <HeaderTemplate>
                                                                        CouponPaymentDate
                                                                    </HeaderTemplate>
                                                                    <ItemTemplate>
                                                                        <telerik:RadTextBox ID="txtCouponPaymentDate" runat="server" Width="100px" ToolTip='<%# Eval("CouponPaymentDate") %>' Text='<%# Eval("CouponPaymentDate") %>'></telerik:RadTextBox>
                                                                    </ItemTemplate>
                                                                </telerik:GridTemplateColumn>
                                                                <telerik:GridTemplateColumn>
                                                                    <HeaderTemplate>
                                                                        RiskFree DP1
                                                                    </HeaderTemplate>
                                                                    <ItemTemplate>
                                                                        <telerik:RadTextBox ID="txtRiskFreeDP1" runat="server" Width="70px" ToolTip='<%# Eval("RiskFreeDP1") %>' Text='<%# Eval("RiskFreeDP1") %>'></telerik:RadTextBox>
                                                                    </ItemTemplate>
                                                                </telerik:GridTemplateColumn>
                                                                <telerik:GridTemplateColumn>
                                                                    <HeaderTemplate>
                                                                        RiskFree DP2
                                                                    </HeaderTemplate>
                                                                    <ItemTemplate>
                                                                        <telerik:RadTextBox ID="txtRiskFreeDP2" runat="server" Width="70px" ToolTip='<%# Eval("RiskFreeDP2") %>' Text='<%# Eval("RiskFreeDP2") %>'></telerik:RadTextBox>
                                                                    </ItemTemplate>
                                                                </telerik:GridTemplateColumn>
                                                            </Columns>
                                                        </MasterTableView>
                                                    </telerik:RadGrid>
                                                </asp:Panel>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                        <%--</telerik:RadAjaxPanel>--%>
                    </telerik:RadPageView>
                    <telerik:RadPageView ID="RadPageView2" runat="server" CssClass="radHeight">
                        <%--    <div class="col-md-12">
                            <div class="tab-content">--%>
                        <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel3" runat="server" Skin="Web20">
                        </telerik:RadAjaxLoadingPanel>
                        <telerik:RadAjaxPanel ID="radPnl1" runat="server" LoadingPanelID="RadAjaxLoadingPanel3">
                            <table style="width: 100%; margin-left: 10px;">
                                <tr>
                                    <td style="width: 35%">
                                        <p>
                                            <asp:Label ID="lblMessage" runat="server" Visible="false"></asp:Label>
                                        </p>

                                        <!-- BEGIN FORM-->

                                        <table style="border-color: blue;" width="450px">
                                            <tr>
                                                <td colspan="2" style="color: #00008B; font-size: 15px; padding-left: 15px">Loan Information
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="newclass-addloan">
                                                    <label>Loan Code</label></td>
                                                <td>
                                                    <asp:TextBox runat="server" ID="TextBox1" Visible="false" class="form-control"></asp:TextBox>
                                                    <telerik:RadTextBox ID="txtBoxAddLoanCode" runat="server" Skin="Web20" class="form-control" Visible="false">
                                                    </telerik:RadTextBox>
                                                    <telerik:RadComboBox ID="ddlAddLoanCode" AllowCustomText="true" runat="server" Width="160"
                                                        Height="200px" OnSelectedIndexChanged="ddlAddLoanCode_SelectedIndexChanged" EmptyMessage="Search for loan..."
                                                        Skin="Web20" AutoPostBack="true">
                                                    </telerik:RadComboBox>
                                                    <asp:HiddenField ID="hfLoanID" runat="server" />
                                                    <%--<asp:Button ID="btnHidden" runat="server" Style="display: none;" OnClick="btnHidden_Click" Text="" />--%></td>
                                            </tr>
                                            <tr>
                                                <td class="newclass-addloan">
                                                    <label>Borrower</label></td>
                                                <td>
                                                    <telerik:RadComboBox ID="ddlBorrower" AllowCustomText="true" runat="server" AutoPostBack="true"
                                                        EmptyMessage="Select Borrower" OnSelectedIndexChanged="ddlBorrower_SelectedIndexChanged"
                                                        Skin="Web20">
                                                    </telerik:RadComboBox>
                                                </td>
                                            </tr>

                                            <tr>
                                                <td class="newclass-addloan">
                                                    <label>Country</label></td>
                                                <td>

                                                    <telerik:RadComboBox ID="ddlCountry" AllowCustomText="true" runat="server" Width="160"
                                                        Height="200px" EmptyMessage="Country..."
                                                        Skin="Web20" AutoPostBack="true">
                                                    </telerik:RadComboBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="newclass-addloan">
                                                    <label>Gurantor</label>
                                                </td>
                                                <td>
                                                    <telerik:RadTextBox ID="txtGurantor" runat="server" Skin="Web20"></telerik:RadTextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="newclass-addloan">
                                                    <label>Grid</label>
                                                </td>
                                                <td>
                                                    <telerik:RadTextBox ID="txtGrid" Enabled="false" runat="server" Skin="Web20"></telerik:RadTextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="newclass-addloan">
                                                    <label>Summit Credit</label>
                                                </td>
                                                <td>
                                                    <telerik:RadTextBox ID="txtSummitCredit" Enabled="false" runat="server" Skin="Web20"></telerik:RadTextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="newclass-addloan">
                                                    <label>Credit Rating</label></td>
                                                <td>

                                                    <telerik:RadDropDownTree runat="server" OnDataBound="tvCreditRating_DataBound" ID="tvCreditRating" OnNodeDataBound="tvCreditRating_NodeDataBound" DefaultMessage="Credit Rating" CheckBoxes="SingleCheck" CssClass="address-dropdown" Skin="Web20">
                                                    </telerik:RadDropDownTree>

                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="newclass-addloan">
                                                    <label>Structure ID</label>
                                                </td>
                                                <td>
                                                    <telerik:RadTextBox ID="txtStructureID" runat="server" Skin="Web20" class="form-control"></telerik:RadTextBox>

                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="newclass-addloan">
                                                    <label>PP</label></td>
                                                <td>
                                                    <telerik:RadTextBox ID="txtPP" runat="server" Skin="Web20" class="form-control"></telerik:RadTextBox>
                                                    <%--<input type="email" class="form-control" placeholder="Email Address">--%>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="newclass-addloan">
                                                    <label>Sector</label>
                                                </td>
                                                <td>
                                                    <%-- <telerik:RadTextBox ID="txtBoxAddSector" runat="server" Skin="Web20" class="form-control"></telerik:RadTextBox>--%>
                                                    <telerik:RadDropDownList ID="ddlSector" runat="server" Skin="Web20">
                                                        <Items>
                                                            <telerik:DropDownListItem Text="Aerospace" Value="Aerospace" />
                                                            <telerik:DropDownListItem Text="Biotechnology & Pharmaceuticals" Value="Biotechnology & Pharmaceuticals" />
                                                            <telerik:DropDownListItem Text="Energy" Value="Energy" />
                                                            <telerik:DropDownListItem Text="Engineering" Value="Engineering" />
                                                            <telerik:DropDownListItem Text="Financial Services" Value="Financial Services" Selected="true" />
                                                            <telerik:DropDownListItem Text="Healthcare and Medical" Value="Healthcare and Medical" />
                                                            <telerik:DropDownListItem Text="Mining" Value="Mining" />
                                                            <telerik:DropDownListItem Text="Oil and Gas" Value="Oil and Gas" />
                                                            <telerik:DropDownListItem Text="Other" Value="Other" />

                                                        </Items>
                                                    </telerik:RadDropDownList>
                                                    <%--<input type="password" class="form-control" placeholder="Password">--%></td>
                                            </tr>
                                            <tr>
                                                <td class="newclass-addloan">
                                                    <label>Facility Size</label></td>
                                                <td>
                                                    <telerik:RadTextBox ID="txtBoxFacilitySize" runat="server" Skin="Web20" class="form-control"></telerik:RadTextBox>
                                                    <%--<input type="text" class="form-control" placeholder="Right icon">--%></td>
                                            </tr>
                                            <tr>
                                                <td class="newclass-addloan">
                                                    <label>Signing Date</label></td>
                                                <td>
                                                    <telerik:RadDatePicker ID="txtBoxAddSigningDate" runat="server" AutoPostBack="true"
                                                        MinDate="01/01/1000" MaxDate="01/01/3000" Skin="Web20">
                                                        <Calendar>
                                                            <SpecialDays>
                                                                <telerik:RadCalendarDay Repeatable="Today" ItemStyle-CssClass="rcToday">
                                                                </telerik:RadCalendarDay>
                                                            </SpecialDays>
                                                        </Calendar>
                                                    </telerik:RadDatePicker>
                                                    <%--  <telerik:RadTextBox ID="txtBoxAddSigningDate" runat="server" Skin="Web20" class="form-control"></telerik:RadTextBox>--%>
                                                    <%--<input type="text" class="form-control" placeholder="Left icon">--%></td>
                                            </tr>

                                            <tr>
                                                <td class="newclass-addloan">
                                                    <label>Maturity Date</label></td>
                                                <td>
                                                    <telerik:RadDatePicker ID="txtBoxAddMaturityDate" runat="server"
                                                        MinDate="01/01/1000" MaxDate="01/01/3000" Skin="Web20">
                                                        <Calendar>
                                                            <SpecialDays>
                                                                <telerik:RadCalendarDay Repeatable="Today" ItemStyle-CssClass="rcToday">
                                                                </telerik:RadCalendarDay>
                                                            </SpecialDays>
                                                        </Calendar>
                                                    </telerik:RadDatePicker>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="newclass-addloan">
                                                    <label>First/Next Coupon Date</label></td>
                                                <td>
                                                    <telerik:RadDatePicker ID="txtCouponDate" runat="server"
                                                        MinDate="01/01/1000" MaxDate="01/01/3000" Skin="Web20">
                                                        <Calendar>
                                                            <SpecialDays>
                                                                <telerik:RadCalendarDay Repeatable="Today" ItemStyle-CssClass="rcToday">
                                                                </telerik:RadCalendarDay>
                                                            </SpecialDays>
                                                        </Calendar>
                                                    </telerik:RadDatePicker>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="newclass-addloan">
                                                    <label>Fixed/Floating</label></td>
                                                <td>
                                                    <telerik:RadDropDownList ID="ddlAddFixedOrFloating" runat="server" Skin="Web20">
                                                        <Items>
                                                            <telerik:DropDownListItem Text="Fixed" Value="Fixed" />
                                                            <telerik:DropDownListItem Selected="true" Text="Floating" Value="Floating" />
                                                        </Items>
                                                    </telerik:RadDropDownList>
                                                    <%--<input type="password" class="form-control spinner" placeholder="Password">--%></td>
                                            </tr>
                                            <tr>
                                                <td class="newclass-addloan">
                                                    <label>Margin</label></td>
                                                <td>
                                                    <telerik:RadTextBox ID="txtBoxAddMargin" runat="server" Skin="Web20"></telerik:RadTextBox></td>
                                            </tr>
                                            <tr>
                                                <td class="newclass-addloan">
                                                    <label>Notional</label></td>
                                                <td>
                                                    <telerik:RadTextBox ID="txtNotional" runat="server" Skin="Web20" AutoPostBack="true" OnTextChanged="txtNotional_TextChanged"></telerik:RadTextBox>
                                                    <%--<telerik:RadNumericTextBox ID="txtNotional" runat="server" Skin="Web20" NumberFormat-DecimalSeparator=","></telerik:RadNumericTextBox>--%>
                                                    <%--<input type="text" class="form-control" placeholder="Right icon">--%></td>
                                            </tr>
                                            <tr>
                                                <td class="newclass-addloan">
                                                    <label>Currency</label></td>
                                                <td>
                                                    <telerik:RadDropDownList ID="ddlAddCurrency" runat="server" Skin="Web20" class="form-control">
                                                        <%--<Items>
                                                            <telerik:DropDownListItem Selected="true" Text="EUR" Value="EUR" />
                                                            <telerik:DropDownListItem Text="USD" Value="USD" />
                                                            <telerik:DropDownListItem Text="GBP" Value="GBP" />
                                                        </Items>--%>
                                                    </telerik:RadDropDownList>
                                                    <%--<input type="email" class="form-control" placeholder="Email Address">--%></td>
                                            </tr>

                                            <tr>
                                                <td class="newclass-addloan">
                                                    <label>Coupon Frequency</label></td>
                                                <td>
                                                    <telerik:RadDropDownList ID="ddlAddCouponFrequency" runat="server" Skin="Web20" class="form-control">
                                                        <Items>
                                                            <telerik:DropDownListItem Text="Monthly" Value="Monthly" />
                                                            <telerik:DropDownListItem Selected="true" Text="Quarterly" Value="Quarterly" />
                                                            <telerik:DropDownListItem Text="Semi-Annual" Value="Semi-Annual" />
                                                            <telerik:DropDownListItem Text="Annual" Value="Annual" />
                                                        </Items>
                                                    </telerik:RadDropDownList>
                                                    <%--<input type="text" class="form-control" placeholder="Left icon">--%></td>
                                            </tr>

                                            <tr>
                                                <td class="newclass-addloan">
                                                    <label>Bilateral</label></td>
                                                <td>
                                                    <telerik:RadDropDownList ID="ddlAddBilateral" runat="server" Skin="Web20" class="form-control">
                                                        <Items>
                                                            <telerik:DropDownListItem Text="Yes" Value="Yes" />
                                                            <telerik:DropDownListItem Selected="true" Text="No" Value="No" />
                                                        </Items>
                                                    </telerik:RadDropDownList>
                                                    <%--<input type="password" class="form-control spinner" placeholder="Password">--%></td>
                                            </tr>

                                            <tr>
                                                <td class="newclass-addloan">
                                                    <label>Amortizing</label></td>
                                                <td>
                                                    <telerik:RadDropDownList ID="ddlAmortizing" runat="server" Skin="Web20" class="form-control" OnSelectedIndexChanged="ddlAmortizing_SelectedIndexChanged" AutoPostBack="true">
                                                        <Items>
                                                            <telerik:DropDownListItem Text="Yes" Value="Yes" />
                                                            <telerik:DropDownListItem Text="No" Value="No" Selected="true" />
                                                        </Items>
                                                    </telerik:RadDropDownList>
                                                </td>
                                            </tr>
                                        </table>
                                        <table style="border-color: blue; width: 450px;" id="tblAmortisation" runat="server">
                                            <tr id="trDate" runat="server">
                                                <td class="newclass-addloan">
                                                    <label>Amortisations Start Point</label>
                                                </td>
                                                <td>
                                                    <telerik:RadDatePicker ID="txtAmortisationsStartDate" runat="server" Skin="Web20"></telerik:RadDatePicker>
                                                </td>
                                            </tr>
                                            <tr id="trNo" runat="server">
                                                <td class="newclass-addloan">
                                                    <label>No Of Amortisation Point</label>
                                                </td>
                                                <td>
                                                    <telerik:RadTextBox ID="txtAmortisations" runat="server" Skin="Web20"></telerik:RadTextBox>
                                                </td>
                                            </tr>
                                            <%-- <tr id="trActiveCoupon" runat="server">
                                                <td class="newclass-addloan">
                                                    <label>No Of Active Coupon</label>
                                                </td>
                                                <td>
                                                    <telerik:RadTextBox ID="txtActiveCoupon" runat="server" Skin="Web20"></telerik:RadTextBox>
                                                </td>
                                            </tr>--%>
                                            <tr>
                                                <td></td>
                                                <td id="tdTemp" runat="server"></td>
                                            </tr>
                                        </table>
                                        <table style="border-color: blue; width: 450px; margin-right: 10px;">
                                            <tr>
                                                <td style="padding-left: 15px">
                                                    <telerik:RadButton ID="btnAddNewLoan" runat="server" Text="Add Loan" Skin="Web20" UseSubmitBehavior="false" ButtonType="StandardButton" OnClientClicking="showConfirmRadWindow" OnClick="btnAddNewLoan_Click">
                                                    </telerik:RadButton>
                                                    <telerik:RadButton ID="btnClearLoan" runat="server" Text="Clear" Skin="Web20" UseSubmitBehavior="false" OnClick="btnClearLoan_Click"></telerik:RadButton>
                                                    <telerik:RadButton ID="btnSchedule" runat="server" Text="Generate Schedule" Skin="Web20" UseSubmitBehavior="false" OnClick="btnSchedule_Click"></telerik:RadButton>
                                                    <telerik:RadWindow ID="confirmWindow" runat="server" VisibleTitlebar="false" VisibleStatusbar="false"
                                                        Modal="true" Behaviors="None" Height="150px" Width="300px">
                                                        <ContentTemplate>
                                                            <div style="padding-left: 30px; padding-top: 20px; width: 200px; float: left; color: black;">
                                                                <asp:Label ID="lblConfirm" Font-Size="14px" Text="Are you sure you want to add this loan to database?"
                                                                    runat="server" Skin="Web20"></asp:Label>
                                                                <br />
                                                                <br />
                                                                <telerik:RadButton ID="RadButtonYes" runat="server" Text="Yes" AutoPostBack="false"
                                                                    OnClientClicked="confirmResultLoan" Skin="Web20">
                                                                </telerik:RadButton>
                                                                <telerik:RadButton ID="RadButtonNo" runat="server" Text="No" AutoPostBack="false"
                                                                    OnClientClicked="confirmResultLoan" Skin="Web20">
                                                                </telerik:RadButton>
                                                            </div>
                                                        </ContentTemplate>
                                                    </telerik:RadWindow>
                                                </td>
                                                <td>
                                                    <br />
                                                    <br />
                                                    <br />
                                                    <br />
                                                    <%-- <button type="submit" class="btn green">Submit</button>
                                <button type="button" class="btn default">Cancel</button>--%>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td style="width: 65%; vertical-align: top; border-left: solid 1px #2B3797; margin-left: 15px">
                                        <asp:Panel ID="pnlAmortizing" runat="server" Width="550px" Visible="false">
                                            <table style="margin-top: 15px; margin-left: 10px; margin-bottom: 8px">
                                                <tr>
                                                    <td style="color: #00008B; font-size: 15px">Amortisation Schedule 
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="margin-top: 10px;">

                                                        <asp:Panel ID="pnl12" runat="server" ScrollBars="Both" Width="720px" Height="678px">

                                                            <%--<asp:Label ID="lblAmortisation" runat="server" Text="Amortisation Schedule" ForeColor="#00008B" Font-Size="15px"></asp:Label>--%>
                                                            <telerik:RadGrid ID="grdAmortizing" Width="530px" runat="server" Skin="Web20" OnItemDataBound="grdAmortizing_ItemDataBound" Visible="false">
                                                                <MasterTableView Width="530px" AutoGenerateColumns="false">

                                                                    <Columns>

                                                                        <telerik:GridTemplateColumn>
                                                                            <HeaderTemplate>
                                                                                Start Date
                                                                            </HeaderTemplate>
                                                                            <ItemTemplate>
                                                                                <telerik:RadTextBox ID="txtStartDate" Width="100px" ToolTip='<%# Eval("StartDate") %>' runat="server" Text='<%# Eval("StartDate") %>'></telerik:RadTextBox>
                                                                            </ItemTemplate>
                                                                        </telerik:GridTemplateColumn>
                                                                        <telerik:GridTemplateColumn>
                                                                            <HeaderTemplate>
                                                                                End Date
                                                                            </HeaderTemplate>
                                                                            <ItemTemplate>
                                                                                <telerik:RadTextBox ID="txtEndDate" Width="100px" ToolTip='<%# Eval("EndDate") %>' runat="server" Text='<%# Eval("EndDate") %>'></telerik:RadTextBox>
                                                                            </ItemTemplate>
                                                                        </telerik:GridTemplateColumn>
                                                                        <telerik:GridTemplateColumn>
                                                                            <HeaderTemplate>
                                                                                Coup. Frac.
                                                                            </HeaderTemplate>
                                                                            <ItemTemplate>
                                                                                <telerik:RadTextBox ID="txtCoupFrac" runat="server" Width="100px" ToolTip='<%# Eval("CoupFrac") %>' Enabled="false" Text='<%# Eval("CoupFrac") %>'></telerik:RadTextBox>
                                                                            </ItemTemplate>
                                                                        </telerik:GridTemplateColumn>
                                                                        <telerik:GridTemplateColumn>
                                                                            <HeaderTemplate>
                                                                                Notional
                                                                            </HeaderTemplate>
                                                                            <ItemTemplate>
                                                                                <telerik:RadTextBox ID="txtNotation" runat="server" Width="100px" ToolTip='<%# "Notional" + Convert.ToInt16( (Eval("ID"))  ) + "-" + Eval("Notation")%>' Text='<%# Eval("Notation") %>'></telerik:RadTextBox>
                                                                            </ItemTemplate>
                                                                        </telerik:GridTemplateColumn>
                                                                        <telerik:GridTemplateColumn>
                                                                            <HeaderTemplate>
                                                                                Amortisation
                                                                            </HeaderTemplate>
                                                                            <ItemTemplate>
                                                                                <telerik:RadTextBox ID="txtAmortisation" runat="server" Width="100px" Enabled="false" ToolTip='<%# Eval("Amortisation") %>' Text='<%# Eval("Amortisation") %>'></telerik:RadTextBox>
                                                                            </ItemTemplate>
                                                                        </telerik:GridTemplateColumn>
                                                                        <telerik:GridTemplateColumn>
                                                                            <HeaderTemplate>
                                                                                Factor
                                                                            </HeaderTemplate>
                                                                            <ItemTemplate>
                                                                                <telerik:RadTextBox ID="txtFactor" runat="server" Width="100px" Enabled="false" ToolTip='<%# Eval("Factor") %>' Text='<%# Eval("Factor") %>'></telerik:RadTextBox>
                                                                            </ItemTemplate>
                                                                        </telerik:GridTemplateColumn>

                                                                    </Columns>
                                                                </MasterTableView>
                                                            </telerik:RadGrid>

                                                        </asp:Panel>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <br />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>

                                                        <telerik:RadButton ID="btnCalculatSchedule" runat="server" OnClick="btnCalculatSchedule_Click" Skin="Web20" Text="Compute Factors"></telerik:RadButton>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <br />
                                                    </td>
                                                </tr>
                                            </table>
                                        </asp:Panel>
                                        <%--<asp:PlaceHolder ID="controlHolder" runat="server">
                                            <asp:Table ID="table1" runat="server" Visible="false" Width="600px" CssClass="tableBorder" BorderStyle="Solid" BorderWidth="1px" BorderColor="Black">
                                                <asp:TableHeaderRow>
                                                    <asp:TableHeaderCell CssClass="tableheader">Start Date</asp:TableHeaderCell>
                                                    <asp:TableHeaderCell CssClass="tableheader">End Date</asp:TableHeaderCell>
                                                    <asp:TableHeaderCell CssClass="tableheader">Notation</asp:TableHeaderCell>
                                                    <asp:TableHeaderCell CssClass="tableheader">Factor</asp:TableHeaderCell>
                                                </asp:TableHeaderRow>
                                            </asp:Table>
                                        </asp:PlaceHolder>--%>
                      
                                    </td>
                                </tr>
                            </table>
                        </telerik:RadAjaxPanel>
                        <!-- END FORM-->

                    </telerik:RadPageView>
                    <telerik:RadPageView ID="RadPageView3" runat="server" CssClass="radHeight" Height="750px">
                        <table style="width: 100%; margin-left: 10px;">
                            <tr>
                                <td>
                                    <%--<asp:Panel ID="Panel1" runat="server" CssClass="horizontal_scroll grdQuotesAndTrades_cnt" ScrollBars="Both" Width="1250" Height="400px">--%>
                                    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel4" runat="server">
                                    </telerik:RadAjaxLoadingPanel>
                                    <%--    <telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server" LoadingPanelID="RadAjaxLoadingPanel2">--%>
                                    <telerik:RadPersistenceManager ID="RadPersistenceManager1" runat="server">
                                        <PersistenceSettings>
                                            <telerik:PersistenceSetting ControlID="grdQuotesAndTrades" />
                                        </PersistenceSettings>
                                    </telerik:RadPersistenceManager>
                                    <telerik:RadButton ID="btnQuotesSaveSettings" runat="server" SkinID="Web20" Text="Save Filter Settings" OnClick="btnQuotesSaveSettings_Click"></telerik:RadButton>
                                    <telerik:RadButton ID="btnQuoteClearSettings" runat="server" SkinID="Web20" Text="Clear Filter Settings" OnClick="btnQuoteClearSettings_Click"></telerik:RadButton>
                                    <telerik:RadGrid ID="grdQuotesAndTrades" Height="700px" Skin="Web20" runat="server" AllowSorting="true" AutoGenerateColumns="false" AllowFilteringByColumn="True" OnSortCommand="grdQuotesAndTrades_SortCommand" OnItemCommand="grdQuotesAndTrades_ItemCommand" Width="1200px" EnableLinqExpressions="false" OnNeedDataSource="grdQuotesAndTrades_NeedDataSource" OnUpdateCommand="grdQuotesAndTrades_UpdateCommand" OnItemDataBound="grdQuotesAndTrades_ItemDataBound" FilterType="Classic">
                                        <ClientSettings>
                                            <Scrolling AllowScroll="true" UseStaticHeaders="true" />

                                        </ClientSettings>
                                        <MasterTableView Width="100%" CommandItemDisplay="Top" AllowFilteringByColumn="True" AllowMultiColumnSorting="true" EditMode="InPlace" DataKeyNames="ID" >
                                            <HeaderStyle Width="130px" />
                                            <CommandItemSettings ShowAddNewRecordButton="false" />
                                            <PagerStyle Mode="NextPrevNumericAndAdvanced"></PagerStyle>
                                            <CommandItemSettings ShowExportToWordButton="true" ShowExportToCsvButton="true"></CommandItemSettings>
                                            <Columns>
                                                <telerik:GridTemplateColumn HeaderText="ID" SortExpression="ID">
                                                    <ItemTemplate>
                                                        <%# DataBinder.Eval(Container.DataItem, "ID") %>
                                                    </ItemTemplate>
                                                </telerik:GridTemplateColumn>
                                                <%--   <telerik:GridBoundColumn DataField="ID" HeaderText="ID">
                                                </telerik:GridBoundColumn>--%>
                                                <telerik:GridTemplateColumn HeaderText="LoanName" SortExpression="LoanName" UniqueName="LoanName" FilterCheckListEnableLoadOnDemand="true" FilterCheckListWebServiceMethod="GetLoanNames">
                                                    <ItemTemplate>
                                                        <%# DataBinder.Eval(Container.DataItem, "LoanName") %>
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <telerik:RadTextBox ID="txtQLoanName" runat="server" Skin="Web20" Width="110px"
                                                            Text='<%# Bind("LoanName")%>'>
                                                        </telerik:RadTextBox>
                                                    </EditItemTemplate>
                                                </telerik:GridTemplateColumn>
                                                <%--<telerik:GridBoundColumn DataField="LoanName" HeaderText="LoanName">
                                                </telerik:GridBoundColumn>--%>
                                                <%--<telerik:GridBoundColumn DataField="TimeStamp" HeaderText="TimeStamp">
                                                </telerik:GridBoundColumn>--%>
                                                <telerik:GridTemplateColumn HeaderText="TradeDate" SortExpression="TradedDate">
                                                    <HeaderStyle Width="165px" />
                                                    <ItemTemplate>
                                                        <%# DataBinder.Eval(Container.DataItem, "TradedDate") %>
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <telerik:RadDateTimePicker ID="txtQTradedDate" runat="server" Skin="Web20" Width="160px"
                                                            SelectedDate='<%# Bind("TradedDate")%>'>
                                                        </telerik:RadDateTimePicker>
                                                    </EditItemTemplate>
                                                </telerik:GridTemplateColumn>
                                                <telerik:GridTemplateColumn HeaderText="TimeStamp" SortExpression="TimeStamp">
                                                    <HeaderStyle Width="165px" />
                                                    <ItemTemplate>
                                                        <%# DataBinder.Eval(Container.DataItem, "TimeStamp") %>
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <telerik:RadDateTimePicker ID="txtQTimeStamp" runat="server" Skin="Web20" Width="160px"
                                                            SelectedDate='<%# Bind("TimeStamp")%>'>
                                                        </telerik:RadDateTimePicker>
                                                    </EditItemTemplate>
                                                </telerik:GridTemplateColumn>
                                                <telerik:GridTemplateColumn HeaderText="CounterParty" SortExpression="CounterParty">
                                                    <ItemTemplate>
                                                        <%# DataBinder.Eval(Container.DataItem, "CounterParty") %>
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <%--<telerik:RadTextBox ID="txtQCounterParty" runat="server" Skin="Web20" Width="110px"
                                                                Text='<%# Bind("CounterParty")%>'>
                                                            </telerik:RadTextBox>--%>
                                                        <telerik:RadDropDownList ID="ddlQCounterParty" runat="server" Skin="Web20" Width="110px">
                                                        </telerik:RadDropDownList>
                                                    </EditItemTemplate>
                                                </telerik:GridTemplateColumn>
                                                <%--<telerik:GridBoundColumn DataField="CounterParty" HeaderText="CounterParty">
                                                </telerik:GridBoundColumn>--%>
                                                <telerik:GridTemplateColumn HeaderText="BidPrice" SortExpression="BidPrice">
                                                    <ItemTemplate>
                                                        <%# DataBinder.Eval(Container.DataItem, "BidPrice") %>
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <telerik:RadTextBox ID="txtQBidPrice" runat="server" Skin="Web20" Width="110px"
                                                            Text='<%# Bind("BidPrice")%>'>
                                                        </telerik:RadTextBox>
                                                    </EditItemTemplate>
                                                </telerik:GridTemplateColumn>
                                                <telerik:GridTemplateColumn HeaderText="OfferPrice" SortExpression="OfferPrice">
                                                    <ItemTemplate>
                                                        <%# DataBinder.Eval(Container.DataItem, "OfferPrice") %>
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <telerik:RadTextBox ID="txtQOfferPrice" runat="server" Skin="Web20" Width="110px"
                                                            Text='<%# Bind("OfferPrice")%>'>
                                                        </telerik:RadTextBox>
                                                    </EditItemTemplate>
                                                </telerik:GridTemplateColumn>
                                                <%--<telerik:GridBoundColumn DataField="BidPrice" HeaderText="BidPrice">
                                                </telerik:GridBoundColumn>--%>
                                                <%--<telerik:GridBoundColumn DataField="OfferPrice" HeaderText="OfferPrice">
                                                </telerik:GridBoundColumn>--%>
                                                <telerik:GridTemplateColumn HeaderText="BidSpread" SortExpression="BidSpread">
                                                    <ItemTemplate>
                                                        <%# DataBinder.Eval(Container.DataItem, "BidSpread") %>
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <telerik:RadTextBox ID="txtQBidSpread" runat="server" Skin="Web20" Width="110px"
                                                            Text='<%# Bind("BidSpread")%>'>
                                                        </telerik:RadTextBox>
                                                    </EditItemTemplate>
                                                </telerik:GridTemplateColumn>

                                                <%--<telerik:GridBoundColumn DataField="BidSpread" HeaderText="BidSpread">
                                                </telerik:GridBoundColumn>--%>
                                                <telerik:GridTemplateColumn HeaderText="OfferSpread" SortExpression="OfferSpread">
                                                    <ItemTemplate>
                                                        <%# DataBinder.Eval(Container.DataItem, "OfferSpread") %>
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <telerik:RadTextBox ID="txtQOfferSpread" runat="server" Skin="Web20" Width="110px"
                                                            Text='<%# Bind("OfferSpread")%>'>
                                                        </telerik:RadTextBox>
                                                    </EditItemTemplate>
                                                </telerik:GridTemplateColumn>
                                                <%--<telerik:GridBoundColumn DataField="OfferSpread" HeaderText="OfferSpread">
                                                </telerik:GridBoundColumn>--%>

                                                <telerik:GridTemplateColumn HeaderText="Traded" SortExpression="Traded">
                                                    <ItemTemplate>
                                                        <%# Convert.ToString(Eval("Traded")).ToLower() == "true" ? "Yes" : "No" %>
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <%--<telerik:radc ID="txtQTraded" runat="server" Skin="Web20" Width="120px"
                                                            Text='<%# Bind("Traded")%>'>
                                                        </telerik:radc>--%>
                                                        <asp:CheckBox ID="txtQTraded" runat="server" Width="110px" Checked='<%# Eval( "Traded") %>' />

                                                    </EditItemTemplate>
                                                </telerik:GridTemplateColumn>
                                                <%--<telerik:GridCheckBoxColumn DataField="Traded" HeaderText="Traded">
                                                </telerik:GridCheckBoxColumn>--%>
                                                <telerik:GridTemplateColumn HeaderText="Notional" SortExpression="MarketValue">
                                                    <ItemTemplate>
                                                        <%# DataBinder.Eval(Container.DataItem, "MarketValue") %>
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <telerik:RadTextBox ID="txtQMarketValue" runat="server" Skin="Web20" Width="110px"
                                                            Text='<%# Bind("MarketValue")%>'>
                                                        </telerik:RadTextBox>
                                                    </EditItemTemplate>
                                                </telerik:GridTemplateColumn>
                                                <%--<telerik:GridBoundColumn DataField="MarketValue" HeaderText="MarketValue">
                                                </telerik:GridBoundColumn>--%>
                                                <%--<telerik:GridBoundColumn DataField="Country" HeaderText="Country">
                                                </telerik:GridBoundColumn>--%>
                                                <telerik:GridTemplateColumn HeaderText="Country" SortExpression="Country">
                                                    <ItemTemplate>
                                                        <%# DataBinder.Eval(Container.DataItem, "Country") %>
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <%--<telerik:RadTextBox ID="txtQCountry" runat="server" Skin="Web20" Width="110px"
                                                                Text='<%# Bind("Country")%>'>
                                                            </telerik:RadTextBox>--%>
                                                        <telerik:RadDropDownList ID="ddlQCountry" runat="server" Skin="Web20" Width="110px">
                                                        </telerik:RadDropDownList>
                                                    </EditItemTemplate>
                                                </telerik:GridTemplateColumn>
                                                <telerik:GridTemplateColumn HeaderText="AvgLifeDisc" SortExpression="AvgLifeDisc">
                                                    <ItemTemplate>
                                                        <%# DataBinder.Eval(Container.DataItem, "AvgLifeDisc") %>
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <telerik:RadTextBox ID="txtQAvgLifeDisc" runat="server" Skin="Web20" Width="110px"
                                                            Text='<%# Bind("AvgLifeDisc")%>'>
                                                        </telerik:RadTextBox>
                                                    </EditItemTemplate>
                                                </telerik:GridTemplateColumn>
                                                <telerik:GridTemplateColumn HeaderText="AvgLifeRiskDisc" SortExpression="AvgLifeRiskDisc">
                                                    <ItemTemplate>
                                                        <%# DataBinder.Eval(Container.DataItem, "AvgLifeRiskDisc") %>
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <telerik:RadTextBox ID="txtQAvgLifeRiskDisc" runat="server" Skin="Web20" Width="110px"
                                                            Text='<%# Bind("AvgLifeRiskDisc")%>'>
                                                        </telerik:RadTextBox>
                                                    </EditItemTemplate>
                                                </telerik:GridTemplateColumn>
                                                <telerik:GridTemplateColumn HeaderText="AvgLifeNonDisc" SortExpression="AvgLifeNonDisc">
                                                    <ItemTemplate>
                                                        <%# DataBinder.Eval(Container.DataItem, "AvgLifeNonDisc") %>
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <telerik:RadTextBox ID="txtQAvgLifeNonDisc" runat="server" Skin="Web20" Width="110px"
                                                            Text='<%# Bind("AvgLifeNonDisc")%>'>
                                                        </telerik:RadTextBox>
                                                    </EditItemTemplate>
                                                </telerik:GridTemplateColumn>
                                                <telerik:GridTemplateColumn HeaderText="SettlementDate" SortExpression="SettlementDate">
                                                    <ItemTemplate>
                                                        <%# Convert.ToDateTime( DataBinder.Eval(Container.DataItem, "SettlementDate")).ToShortDateString() %>
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <telerik:RadDatePicker ID="txtSettlementDate" runat="server" Skin="Web20" Width="110px"
                                                            SelectedDate='<%# Bind("SettlementDate")%>'>
                                                        </telerik:RadDatePicker>
                                                    </EditItemTemplate>
                                                </telerik:GridTemplateColumn>
                                                <telerik:GridTemplateColumn HeaderText="Margin" SortExpression="Margin">
                                                    <ItemTemplate>
                                                        <%# DataBinder.Eval(Container.DataItem, "Margin") %>
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <telerik:RadTextBox ID="txtMargin" runat="server" Skin="Web20" Width="110px"
                                                            Text='<%# Bind("Margin")%>'>
                                                        </telerik:RadTextBox>
                                                    </EditItemTemplate>
                                                </telerik:GridTemplateColumn>
                                                <telerik:GridEditCommandColumn ButtonType="LinkButton" UpdateText="Update" CancelText="Cancel"
                                                    EditText="Edit">
                                                </telerik:GridEditCommandColumn>
                                                <telerik:GridTemplateColumn AllowFiltering="false">
                                                    <HeaderStyle Width="120px" />
                                                    <ItemTemplate>
                                                        <table style="border: 0px">
                                                            <tr style="border: 0px">
                                                                <%-- <td>
                                                                    <telerik:RadButton ID="btnEdit" runat="server" Text="Edit" CommandName="Edit" CommandArgument='<%# Eval("ID") %>' Skin="Web20" ToolTip='<%# Eval("LoanName") %>'></telerik:RadButton>
                                                                </td>--%>
                                                                <td style="border: 0px">
                                                                    <%--<telerik:RadButton ID="btnDelete" runat="server" Text="Delete" CommandName="Delete" CommandArgument='<%# Eval("ID") %>' Skin="Web20" ToolTip='<%# Eval("LoanName") %>' OnClientClicking="showConfirmQuoteDelete"></telerik:RadButton>--%>
                                                                    <asp:Button ID="btnDelete" Text="Delete" runat="server" OnClientClick="javascript:if(!confirm('Are you sure want to delete this record?')){return false;}" CommandName="Delete" CommandArgument='<%# Eval("ID") %>' ToolTip='<%# Eval("LoanName") %>' />
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="120px" />
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
                                    <%-- </telerik:RadAjaxPanel>--%>
                                    <%-- </asp:Panel>--%>
                                 
                                </td>
                            </tr>
                        </table>
                    </telerik:RadPageView>
                    <telerik:RadPageView ID="RadPageView4" runat="server" CssClass="radHeight" Height="750px">
                        <table style="width: 100%; margin-left: 10px">
                            <tr>
                                <td>
                                    <%--<asp:Timer ID="timer1" runat="server" Interval="30000" OnTick="timer1_Tick"></asp:Timer>--%>
                                    <asp:Panel ID="Panel2" runat="server" Width="1250" ScrollBars="Auto" Height="700px">
                                        <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Web20">
                                        </telerik:RadAjaxLoadingPanel>


                                        <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
                                            <AjaxSettings>
                                                <telerik:AjaxSetting AjaxControlID="grdLoans">
                                                    <UpdatedControls>
                                                        <telerik:AjaxUpdatedControl ControlID="grdLoans" />
                                                    </UpdatedControls>
                                                </telerik:AjaxSetting>
                                                <%--     <telerik:AjaxSetting AjaxControlID="timer1">
                                                    <UpdatedControls>
                                                        <telerik:AjaxUpdatedControl ControlID="grdLoans"></telerik:AjaxUpdatedControl>
                                                    </UpdatedControls>
                                                </telerik:AjaxSetting>--%>
                                            </AjaxSettings>

                                        </telerik:RadAjaxManager>


                                        <telerik:RadGrid ID="grdLoans" runat="server" Height="700px" Skin="Web20" AllowSorting="true" AutoGenerateColumns="false" OnSortCommand="grdLoans_SortCommand" OnItemCommand="grdLoans_ItemCommand" AllowFilteringByColumn="True" Width="1200px" FilterType="CheckList" OnFilterCheckListItemsRequested="grdLoans_FilterCheckListItemsRequested" OnNeedDataSource="grdLoans_NeedDataSource">
                                            <ClientSettings>
                                                <Scrolling AllowScroll="true" UseStaticHeaders="true" />

                                            </ClientSettings>
                                            <MasterTableView Width="90%" CommandItemDisplay="Top" AllowFilteringByColumn="True" CheckListWebServicePath="LoanService.asmx">

                                                <CommandItemSettings ShowAddNewRecordButton="false" />
                                                <HeaderStyle Width="120px" />
                                                <PagerStyle Mode="NextPrevNumericAndAdvanced"></PagerStyle>
                                                <CommandItemSettings ShowExportToWordButton="true" ShowExportToCsvButton="true"></CommandItemSettings>
                                                <Columns>
                                                    <telerik:GridBoundColumn DataField="ID" HeaderText="ID">
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn DataField="CodeName" HeaderText="CodeName" UniqueName="CodeName" FilterCheckListEnableLoadOnDemand="true" FilterDelay="200">
                                                    </telerik:GridBoundColumn>

                                                    <telerik:GridBoundColumn DataField="Borrower" HeaderText="Borrower" UniqueName="Borrower" FilterCheckListEnableLoadOnDemand="true" FilterDelay="200">
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn DataField="Country" HeaderText="Country" UniqueName="Country" FilterCheckListEnableLoadOnDemand="true" FilterDelay="200">
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn DataField="Gurantor" HeaderText="Gurantor" UniqueName="Gurantor" FilterCheckListEnableLoadOnDemand="true" CurrentFilterFunction="Contains">
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn DataField="Grid" HeaderText="Grid" UniqueName="Grid" FilterCheckListEnableLoadOnDemand="true" CurrentFilterFunction="Contains">
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn DataField="SummitCreditEntity" HeaderText="SummitCreditEntity" UniqueName="SummitCreditEntity" FilterCheckListEnableLoadOnDemand="true" CurrentFilterFunction="Contains">
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn DataField="Sector" HeaderText="Sector" UniqueName="Sector" FilterCheckListEnableLoadOnDemand="true" CurrentFilterFunction="Contains" FilterDelay="200">
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn DataField="CreditRatingModys" HeaderText="CR-Moodys" UniqueName="CreditRatingModys" FilterCheckListEnableLoadOnDemand="true" FilterDelay="200">
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn DataField="CreditRatingSPs" HeaderText="CR-S&P" UniqueName="CreditRatingSPs" FilterCheckListEnableLoadOnDemand="true" FilterDelay="200">
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn DataField="CreditRatingFitch" HeaderText="CR-Fitch" UniqueName="CreditRatingFitch" FilterCheckListEnableLoadOnDemand="true" FilterDelay="200">
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn DataField="CreditRatingING" HeaderText="CR-ING" UniqueName="CreditRatingING" FilterCheckListEnableLoadOnDemand="true" FilterDelay="200">
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn DataField="PP" HeaderText="PP" UniqueName="PP" FilterCheckListEnableLoadOnDemand="true" FilterDelay="200">
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn DataField="Signing_Date" HeaderText="Signing_Date">
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn DataField="Maturity_Date" HeaderText="Maturity_Date">
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn DataField="FixedOrFloating" HeaderText="FixedOrFloating" FilterCheckListEnableLoadOnDemand="true" UniqueName="FixedOrFloating" FilterDelay="200">
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn DataField="CouponDate" HeaderText="FirstCoupon" FilterCheckListEnableLoadOnDemand="true" UniqueName="FirstCoupon" CurrentFilterFunction="Contains">
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn DataField="Notional" HeaderText="Notional" FilterCheckListEnableLoadOnDemand="true" UniqueName="Notional" FilterDelay="200">
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn DataField="Margin" HeaderText="Margin" FilterCheckListEnableLoadOnDemand="true" UniqueName="Margin" FilterDelay="200">
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn DataField="Currency" HeaderText="Currency" FilterCheckListEnableLoadOnDemand="true" UniqueName="Currency" FilterDelay="200">
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn DataField="CouponFrequency" HeaderText="CouponFrequency" FilterCheckListEnableLoadOnDemand="true" UniqueName="CouponFrequency" FilterDelay="200">
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn DataField="FacilitySize" HeaderText="FacilitySize" FilterCheckListEnableLoadOnDemand="true" UniqueName="FacilitySize" FilterDelay="200">
                                                    </telerik:GridBoundColumn>
                                                    <%--   <telerik:GridCheckBoxColumn DataField="Bilateral" HeaderText="Bilateral">
                                                        <HeaderStyle Width="100px" />
                                                    </telerik:GridCheckBoxColumn>--%>
                                                    <telerik:GridTemplateColumn DataField="Bilateral" HeaderText="Bilateral">
                                                        <ItemTemplate>
                                                            <%# Eval("Bilateral") == "true" ? "Yes" : "No" %>
                                                        </ItemTemplate>
                                                    </telerik:GridTemplateColumn>
                                                    <telerik:GridBoundColumn DataField="Amortizing" HeaderText="Amortizing">
                                                    </telerik:GridBoundColumn>
                                                    <%--<telerik:GridBoundColumn DataField="AmortisationsStartPoint" HeaderText="AmortizationStart">
                                                    </telerik:GridBoundColumn>--%>
                                                    <telerik:GridTemplateColumn DataField="AmortisationsStartPoint" HeaderText="AmortizationStart">
                                                        <ItemTemplate>
                                                            <%# Convert.ToString(Eval("AmortisationsStartPoint")) == string.Empty ? "N/A" : Eval("AmortisationsStartPoint") %>
                                                        </ItemTemplate>
                                                    </telerik:GridTemplateColumn>
                                                    <%--<telerik:GridBoundColumn DataField="NoOfAmortisationPoint" HeaderText="NoOfAmortisation">
                                                    </telerik:GridBoundColumn>--%>
                                                    <telerik:GridTemplateColumn DataField="NoOfAmortisationPoint" HeaderText="NoOfAmortisation" FilterCheckListEnableLoadOnDemand="true" FilterCheckListWebServiceMethod="GetAmortization" FilterDelay="200" UniqueName="NoOfAmortisationPoint">
                                                        <ItemTemplate>
                                                            <%# Convert.ToString(Eval("NoOfAmortisationPoint")) == string.Empty ? "N/A" : Eval("NoOfAmortisationPoint") %>
                                                        </ItemTemplate>
                                                    </telerik:GridTemplateColumn>
                                                    <%--<telerik:GridTemplateColumn DataField="StructureID" HeaderText="StructureID" FilterCheckListEnableLoadOnDemand="true"  CurrentFilterFunction="Contains" UniqueName="StructureID">
                                                        <ItemTemplate>
                                                            <%# Convert.ToString(Eval("StructureID")) == string.Empty ? "" : Eval("StructureID") %>
                                                        </ItemTemplate>
                                                    </telerik:GridTemplateColumn>--%>
                                                    <telerik:GridBoundColumn DataField="StructureID" HeaderText="StructureID" FilterCheckListEnableLoadOnDemand="true" UniqueName="StructureID" FilterDelay="200">
                                                    </telerik:GridBoundColumn>

                                                    <telerik:GridTemplateColumn AllowFiltering="false">

                                                        <HeaderStyle Width="250px" />
                                                        <ItemTemplate>
                                                            <table style="border: 0px">
                                                                <tr style="border: 0px">
                                                                    <td style="border: 0px">

                                                                        <telerik:RadButton ID="btnEdit" runat="server" Text="Edit" CommandName="Edit" CommandArgument='<%# Eval("ID") %>' Skin="Web20" ToolTip='<%# Eval("CodeName") %>'></telerik:RadButton>

                                                                    </td>
                                                                    <td style="border: 0px">
                                                                        <%--<telerik:RadButton ID="btnDelete" runat="server" Text="Delete Loan" CommandName="Delete" CommandArgument='<%# Eval("ID") %>' ToolTip='<%# Eval("CodeName") %>' Skin="Web20"></telerik:RadButton>--%>
                                                                        <asp:Button ID="btnDelete" Text="Delete" runat="server" OnClientClick="javascript:if(!confirm('Are you sure want to delete this record?')){return false;}" CommandName="Delete" CommandArgument='<%# Eval("ID") %>' ToolTip='<%# Eval("CodeName") %>' />


                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </ItemTemplate>
                                                        <ItemStyle Width="180px" />
                                                    </telerik:GridTemplateColumn>
                                                </Columns>
                                            </MasterTableView>
                                        </telerik:RadGrid>


                                    </asp:Panel>
                                </td>
                            </tr>
                        </table>

                    </telerik:RadPageView>
                    <telerik:RadPageView ID="RadPageView7" runat="server" CssClass="radHeight">
                        <table style="width: 100%; margin-left: 10px">
                            <tr>
                                <td>
                                    <p>
                                        <asp:Label ID="lblQuotesTradesMessage" runat="server" Visible="false"></asp:Label>
                                    </p>

                                    <table>
                                        <tr>
                                            <td class="newclass-addloan">
                                                <label>Loan Name</label></td>
                                            <td>

                                                <telerik:RadComboBox ID="ddlQuotesLoanName" AllowCustomText="true" runat="server" Width="160"
                                                    Height="200px" EmptyMessage="Search for loan..."
                                                    Skin="Web20">
                                                </telerik:RadComboBox>

                                            </td>

                                        </tr>
                                        <tr>
                                            <td class="newclass-addloan">
                                                <label>Counterparty</label></td>
                                            <td>
                                                <telerik:RadTextBox runat="server" ID="txtQuotesCounterParty" Skin="Web20" class="form-control"></telerik:RadTextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="newclass-addloan">
                                                <label>Bid Price</label></td>
                                            <td>
                                                <telerik:RadTextBox runat="server" ID="txtQuoteBidPrice" Skin="Web20" class="form-control"></telerik:RadTextBox>
                                            </td>

                                        </tr>
                                        <tr>
                                            <td class="newclass-addloan">
                                                <label>Offer Price</label></td>
                                            <td>
                                                <telerik:RadTextBox runat="server" ID="txtQuoteOfferPrice" Skin="Web20" class="form-control"></telerik:RadTextBox>
                                            </td>

                                        </tr>
                                        <tr>
                                            <td class="newclass-addloan">
                                                <label>Bid Spread</label></td>
                                            <td>
                                                <telerik:RadTextBox runat="server" ID="txtQuoteBidSpreadPrice" Skin="Web20" class="form-control"></telerik:RadTextBox>
                                            </td>

                                        </tr>
                                        <tr>
                                            <td class="newclass-addloan">
                                                <label>Offer Spread</label></td>
                                            <td>
                                                <telerik:RadTextBox runat="server" ID="txtQuoteOfferSpread" Skin="Web20" class="form-control"></telerik:RadTextBox>
                                            </td>

                                        </tr>
                                        <tr>
                                            <td class="newclass-addloan">
                                                <label>Is Traded</label></td>
                                            <td>
                                                <asp:CheckBox ID="chkIsTraded" runat="server" />
                                            </td>

                                        </tr>
                                        <tr>
                                            <td class="newclass-addloan">
                                                <label>Market Value</label></td>
                                            <td>
                                                <telerik:RadTextBox ID="txtQuoteMarketValue" runat="server" Skin="Web20" />
                                            </td>

                                        </tr>
                                        <tr>
                                            <td class="newclass-addloan">
                                                <label>Country</label></td>
                                            <td>
                                                <telerik:RadComboBox ID="ddlQuoteCountry" AllowCustomText="true" runat="server" Width="160"
                                                    Height="200px" EmptyMessage="Country..."
                                                    Skin="Web20">
                                                </telerik:RadComboBox>
                                            </td>

                                        </tr>
                                        <tr>
                                            <td></td>
                                            <td>
                                                <telerik:RadButton ID="btnSaveQuote" runat="server" Text="Save" Skin="Web20" OnClick="btnSaveQuote_Click"></telerik:RadButton>
                                                <telerik:RadButton ID="btnClearQuote" runat="server" Text="Clear" Skin="Web20" OnClick="btnClearQuote_Click"></telerik:RadButton>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>


                    </telerik:RadPageView>
                    <telerik:RadPageView ID="RadPageView5" runat="server" Height="500px" CssClass="radHeight">
                        <telerik:RadPanelBar ID="pnlUserDetails" runat="server" Width="1290px" Height="500px" Skin="Web20">
                            <Items>
                                <telerik:RadPanelItem runat="server" Text="Change Password">
                                    <ContentTemplate>
                                        <br />
                                        <table style="width: 100%; margin-left: 10px;">
                                            <tr>
                                                <td>
                                                    <table style="width: 100%; table-layout: fixed">
                                                        <tr>
                                                            <td class="newclass-addloan" style="width: 150px; table-layout: fixed">
                                                                <label>Enter Old Password</label></td>
                                                            <td style="width: 200px; table-layout: fixed">

                                                                <telerik:RadTextBox runat="server" TextMode="Password" ID="txtOldPassword" Skin="Web20" class="form-control"></telerik:RadTextBox>

                                                            </td>
                                                            <td style="width: 800px; table-layout: fixed">&nbsp;
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="newclass-addloan" style="width: 100px; table-layout: fixed">
                                                                <label>Enter New Password</label></td>
                                                            <td>
                                                                <telerik:RadTextBox runat="server" TextMode="Password" ID="txtNewPassword" Skin="Web20" class="form-control"></telerik:RadTextBox>

                                                            </td>
                                                            <td></td>
                                                        </tr>
                                                        <tr>
                                                            <td class="newclass-addloan">
                                                                <label>Confirm New Password</label></td>
                                                            <td>
                                                                <telerik:RadTextBox runat="server" TextMode="Password" ID="txtConfirmPassword" Skin="Web20" class="form-control"></telerik:RadTextBox>
                                                            </td>
                                                            <td></td>
                                                        </tr>

                                                        <tr>
                                                            <td></td>
                                                            <td>
                                                                <telerik:RadButton ID="btnChangePassword" Width="120px" runat="server" Text="Save" Skin="Web20" OnClick="btnChangePassword_Click"></telerik:RadButton>

                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                        </table>
                                        <br />
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
        function confirmResultLoan(sender, args) {
            var oWnd = $find("<%=confirmWindow.ClientID %>");
            oWnd.close();
            if (sender.get_text() == "Yes") {
                $find("<%=btnAddNewLoan.ClientID %>").click();
            }
        }


        function confirmResult(sender, args) {
            var oWnd = $find("<%=confirmWindow.ClientID %>");
            oWnd.close();
            if (sender.get_text() == "Yes") {
                $find("<%=btnAddNewLoan.ClientID %>").click();
            }
        }


        //Not used anywhere
        function showConfirmRadWindow1(sender, args) {
            $find("<%=confirmWindow1.ClientID %>").show();
            $find("<%=RadButtonYes1.ClientID %>").focus();
            args.set_cancel(true);
        }
        function confirmResult1(sender, args) {
            var oWnd = $find("<%=confirmWindow1.ClientID %>");
            oWnd.close();
            if (sender.get_text() == "Yes") {
                $find("<%=btnAddTradeQuote.ClientID %>").click();
            }
        }
    </script>

    <script type="text/javascript">
        function showConfirmQuoteDelete(sender, args) {
            $find("<%=confirmQuoteDelete.ClientID %>").show();
            $find("<%=rbQuoteSuccess.ClientID %>").focus();
            args.set_cancel(true);
        }
        function confirmResult(sender, args) {
            var oWnd = $find("<%=confirmQuoteDelete.ClientID %>");
            oWnd.close();
            if (sender.get_text() == "Yes") {
                var masterTable = $find("<%=grdQuotesAndTrades.ClientID%>").get_masterTableView();
                var btnDelete = masterTable.get_dataItems()[0].findControl('btnDelete');
                btnDelete.click();
            }
        }
    </script>
</asp:Content>
