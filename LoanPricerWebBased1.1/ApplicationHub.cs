using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using BLL;
using DAL;

namespace LoanPricerWebBased
{
    public class ApplicationHub : Hub
    {
        /// <summary>
        /// Refresh the loans on all the connected clients
        /// </summary>
        /// <param name="loan"></param>
        public static void RefreshLoans(List<Loans> loans)
        {
            var context = GlobalHost.ConnectionManager.GetHubContext<ApplicationHub>();
            context.Clients.All.onLoanRefreshed(loans);
        }

        /// <summary>
        /// New loan get added
        /// </summary>
        /// <param name="loan"></param>
        public static void NewLoanAdded(Loans loan)
        {
            var context = GlobalHost.ConnectionManager.GetHubContext<ApplicationHub>();
            context.Clients.All.onLoanAdded(loan);
        }

        /// <summary>
        /// Refresh quote and trade
        /// </summary>
        /// <param name="quotesAndTrades"></param>
        public static void RefreshQuotesAndTrade(List<QuotesAndTrades> quotesAndTrades)
        {
            var context = GlobalHost.ConnectionManager.GetHubContext<ApplicationHub>();
            context.Clients.All.onQuotesAndTradeRefreshed(quotesAndTrades);
        }

        /// <summary>
        /// New Quote and trade added
        /// </summary>
        /// <param name="quoteAndTrade"></param>
        public static void NewQuotesAndTradeAdded(QuotesAndTrades quoteAndTrade)
        {
            var context = GlobalHost.ConnectionManager.GetHubContext<ApplicationHub>();
            context.Clients.All.onQuotesAndTradeAdded(quoteAndTrade);
        }
    }
}