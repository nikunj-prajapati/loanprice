$(document).ready(function() {

    //
    // Bid Spread Calculation
    //
    $('.BidPrice').numeric().keypress(function(e) {
        // if enter is pressed
        if (e.which == 13) {
            alert('a');
            BidSpreadCalculation($(this), e);
        }
    });

    function BidSpreadCalculation(sender, e) {
        var container = $(sender).parent().parent().parent();
        //When bidPrice text box changes please update bidSpread to equal
        //BidSpread = (100 - bidPrice) * (360 / 365.25) * (100 / NonDiscountedAverageLife) + Discount_Margin;

        var bidPrice = parseFloat($(sender).val());
        if (isNaN(bidPrice)) {
            bidPrice = 0;
        }
        var averageLifeNonDisc = parseFloat($(container).find('.AverageLifeNonDisc').val());
        if (isNaN(averageLifeNonDisc)) {
            averageLifeNonDisc = 1;
        }
        var discountMargin = parseFloat($(container).find('.DiscountMargin').val());
        if (isNaN(discountMargin)) {
            discountMargin = 0;
        }

        var computedBidSpread = (((100 - bidPrice) * 360 * 100) / (averageLifeNonDisc * 365.25)) + discountMargin;

        $(container).find('.bidSpread').val(computedBidSpread.toFixed(4));
        e.preventDefault();
    }

    //
    // Bid price calculation
    //

    $('.bidSpread').numeric().keypress(function(e) {
        // if enter is pressed
        if (e.which == 13) {
            BidPriceCalculation($(this), e);
        }
    });

    function BidPriceCalculation(sender, e) {
        var container = $(sender).parent().parent().parent();
        // When bidSpread text box changes please update bidPrice to equal
        // BidPrice = (100 - (((bidSpread - Discount_Margin) / (360/365.25) ) / (100/NonDiscountedAverageLife))) ;

        var bidSpread = parseFloat($(sender).val());
        if (isNaN(bidSpread)) {
            bidSpread = 0;
        }
        var averageLifeNonDisc = parseFloat($(container).find('.AverageLifeNonDisc').val());
        if (isNaN(averageLifeNonDisc)) {
            averageLifeNonDisc = 1;
        }
        var discountMargin = parseFloat($(container).find('.DiscountMargin').val());
        if (isNaN(discountMargin)) {
            discountMargin = 0;
        }

        var computedBidPrice = 100 - (((bidSpread - discountMargin) * (averageLifeNonDisc * 365.25)) / 360 / 100);

        $(container).find('.BidPrice').val(computedBidPrice.toFixed(4));

        e.preventDefault();
    }

    //
    // Offer spread calculation
    //

    $('.OfferPrice').numeric().keypress(function(e) {
        // if enter is pressed
        if (e.which == 13) {
            OfferSpreadCalculation($(this), e);
        }
    });

    function OfferSpreadCalculation(sender, e) {
        var container = $(sender).parent().parent().parent();
        // (c) When askPrice text box changes please update askSpread to equal
        // Ask = offer
        // OfferSpread = (100 - offerPrice) * (360 / 365.25) * (100 / NonDiscountedAverageLife) + Discount_Margin;

        var offerPrice = parseFloat($(sender).val());
        if (isNaN(offerPrice)) {
            offerPrice = 0;
        }
        var averageLifeNonDisc = parseFloat($(container).find('.AverageLifeNonDisc').val());
        if (isNaN(averageLifeNonDisc)) {
            averageLifeNonDisc = 1;
        }
        var discountMargin = parseFloat($(container).find('.DiscountMargin').val());
        if (isNaN(discountMargin)) {
            discountMargin = 0;
        }
        var computedOfferSpread = (((100 - offerPrice) * 360 * 100) / (averageLifeNonDisc * 365.25)) + discountMargin;
        $(container).find('.OfferSpread').val(computedOfferSpread.toFixed(4));
        e.preventDefault();
    }
    //
    // Offer price calculation
    //

    $('.OfferSpread').numeric().keypress(function(e) {
        // if enter is pressed
        if (e.which == 13) {
            OfferPriceCalculation($(this), e);
        }
    });

    function OfferPriceCalculation(sender, e) {
        var container = $(sender).parent().parent().parent();
        // (c) When askPrice text box changes please update askSpread to equal
        // Ask = offer
        // offerPrice = (100 - (((offerSpread - Discount_Margin) / (360/365.25) ) / (100/NonDiscountedAverageLife))) ;

        var offerSpread = parseFloat($(sender).val());
        if (isNaN(offerSpread)) {
            offerSpread = 0;
        }
        var averageLifeNonDisc = parseFloat($(container).find('.AverageLifeNonDisc').val());
        if (isNaN(averageLifeNonDisc)) {
            averageLifeNonDisc = 1;
        }
        var discountMargin = parseFloat($(container).find('.DiscountMargin').val());
        if (isNaN(discountMargin)) {
            discountMargin = 0;
        }

        var computedOfferPrice = 100 - (((offerSpread - discountMargin) * (averageLifeNonDisc * 365.25)) / 360 / 100);

        $(container).find('.OfferPrice').val(computedOfferPrice.toFixed(4));
        e.preventDefault();
    }
    //
    // 1 to 99 restrication on input
    //
    $('.number').numeric().keyup(function(event) {
        var number = parseInt($(this).val());
        if (number <= 0 || number > 100) {
            $(this).val(1);
        }
    });
});
