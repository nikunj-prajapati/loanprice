
$(function () {
    // Declare a proxy to reference the hub. 
    var chat = $.connection.applicationHub;

    // callback to receive loans
    chat.client.onLoanAdded = function (loan) {
        // Html encode display name and message. 
        AddLoanHeaderToDOM();
        var row = GetLoanGridRow(loan);

        $('.grdLoans tr:last').after(row);
        UpdateNotificationArea("New Loan has been added");
    };

    // callback to refresh loans
    chat.client.onLoanRefreshed = function (loans) {
        //clear all loans
        $('.grdLoans_cnt').find('.grdLoans').remove();
        AddLoanHeaderToDOM();

        var markup;
        for (var i = 0; i < loans.length; i++) {
            markup = markup + GetLoanGridRow(loans[i]);
        }

        $('.grdLoans tr:last').after(markup);
        UpdateNotificationArea("Loan has been refreshed");
    };

    // Quotes and Trades

    // callback to receive quotes and trades
    chat.client.onQuotesAndTradeAdded = function (quoteAndTrade) {
        // Html encode display name and message. 
        AddQuoteAndTradeHeaderToDOM();
        var row = GetQuoteAndTradeGridRow(quoteAndTrade);

        $('.grdQuotesAndTrades tr:last').after(row);
        UpdateNotificationArea("New Quote and Trade has been added");
    };

    // callback to refresh quotes and trades
    chat.client.onQuotesAndTradeRefreshed = function (quoteAndTrades) {
        //clear all loans
        $('.grdQuotesAndTrades_cnt').find('.grdQuotesAndTrades').remove();
        AddQuoteAndTradeHeaderToDOM();

        var markup;
        for (var i = 0; i < quoteAndTrades.length; i++) {
            markup = markup + GetQuoteAndTradeGridRow(quoteAndTrades[i]);
        }

        $('.grdQuotesAndTrades tr:last').after(markup);
        UpdateNotificationArea("Quote and Trade has been refreshed");
    };

    // Start the connection.
    $.connection.hub.start().done(function () {
        //$('#sendmessage').click(function () {
        //    // Call the Send method on the hub. 
        //    chat.server.send($('#displayname').val(), $('#message').val());
        //    // Clear text box and reset focus for next comment. 
        //    $('#message').val('').focus();
        //});
    });

    // Loan grid html
    function GetLoanGridRow(loan) {
        var html = '<tr style="color: #8c4510; background-color: rgb(255, 247, 231);"><td>' + loan.ID + '</td><td>' + loan.CodeName + '</td><td>' + loan.Borrower + '</td><td>' + loan.Country + '</td><td>' + loan.Sector + '</td><td>' + loan.Signing_Date + '</td><td>' + loan.Maturity_Date + '</td><td>' + loan.FixedOrFloating + '</td><td>' + loan.Margin + '</td><td>' + loan.Currency + '</td><td>' + loan.CouponFrequency + '</td><td>' + loan.FacilitySize + '</td><td><span title="' + loan.Bilateral + '" disabled="disabled"><input disabled="disabled" type="checkbox"></span></td><td>' + loan.Amortizing + '</td>';
        return html;
    }

    function AddLoanHeaderToDOM() {
        // check to see if the Loans exist
        var count = $('.grdLoans tr').length;
        if (isNaN(count) || count == 0) {
            // if there is no row. We need to add the header
            $('.grdLoans_cnt').append($('<table cellspacing="2" cellpadding="3" border="1" style="background-color:#DEBA84;border-color:#DEBA84;border-width:1px;border-style:None;" class="grdLoans"><tbody><th scope="col"><a>ID</a></th><th scope="col"><a>CodeName</a></th><th scope="col"><a>Borrower</a></th><th scope="col"><a>Country</a></th><th scope="col"><a>Sector</a></th><th scope="col"><a">Signing_Date</a></th><th scope="col"><a>Maturity_Date</a></th><th scope="col"><a>FixedOrFloating</a></th><th scope="col"><a>Margin</a></th><th scope="col"><a>Currency</a></th><th scope="col"><a>CouponFrequency</a></th><th scope="col"><a>FacilitySize</a></th><th scope="col"><a">Bilateral</a></th><th scope="col"><a>Amortizing</a></th></tbody></table>'));
        }
    }


    // quote and trade grid html
    function GetQuoteAndTradeGridRow(quoteAndTrade) {
        var html = '<td>' + quoteAndTrade.ID + '</td><td>' + quoteAndTrade.LoanName + '</td><td>' + quoteAndTrade.TimeStamp + '</td><td>' + quoteAndTrade.CounterParty + '</td><td>' + quoteAndTrade.BidPrice + '</td><td>' + quoteAndTrade.OfferPrice + '</td><td>' + quoteAndTrade.BidSpread + '</td><td>' + quoteAndTrade.OfferSpread + '</td><td><span title="' + quoteAndTrade.Traded + '" disabled="disabled"><input disabled="disabled" type="checkbox"></span></td><td>' + quoteAndTrade.MarketValue + '</td>';
        return html;
    }

    function AddQuoteAndTradeHeaderToDOM() {
        // check to see if the Loans exist
        var count = $('.grdQuotesAndTrades tr').length;
        if (isNaN(count) || count == 0) {
            // if there is no row. We need to add the header
            $('.grdQuotesAndTrades_cnt').append($('<table style="border-top-color: #deba84; border-right-color: #deba84; border-bottom-color: #deba84; border-left-color: #deba84; border-top-width: 1px; border-right-width: 1px; border-bottom-width: 1px; border-left-width: 1px; border-top-style: none; border-right-style: none; border-bottom-style: none; border-left-style: none; background-color: rgb(222, 186, 132);" border="1" rules="all" cellspacing="2" cellpadding="3" class="grdQuotesAndTrades"><tbody><th scope="col"><a style="color: white;">ID</a></th><th scope="col"><a style="color: white;">LoanName</a></th><th scope="col"><a style="color: white;">TimeStamp</a></th><th scope="col"><a style="color: white;" >CounterParty</a></th><th scope="col"><a style="color: white;">BidPrice</a></th><th scope="col"><a style="color: white;">OfferPrice</a></th><th scope="col"><a style="color: white;">BidSpread</a></th><th scope="col"><a style="color: white;" >OfferSpread</a></th><th scope="col"><a style="color: white;">Traded</a></th><th scope="col"><a style="color: white;">MarketValue</a></th></tbody></table>'));
        }
    }

    function UpdateNotificationArea(msg) {
        $('#notification').text(msg);
    }
});