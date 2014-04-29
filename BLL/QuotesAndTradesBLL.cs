using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAL;

namespace BLL
{
    public class QuotesAndTradesBLL
    {
        public List<QuotesAndTrades> GetQuotesAndTrades()
        {
            using (LoanPriceEntities context = new LoanPriceEntities())
            {
                return context.QuotesAndTrades.OrderByDescending(s => s.ID).ToList();
            }
        }
        public void InsertDuplicateRecord(DuplicateRecord select)
        {
            using (LoanPriceEntities context = new LoanPriceEntities())
            {
                context.AddToDuplicateRecords(select);
            }
        }
        public bool RemoveAllQuotesAndTrades()
        {
            try
            {
                using (LoanPriceEntities context = new LoanPriceEntities())
                {
                    List<QuotesAndTrades> quotesAndTrades = context.QuotesAndTrades.ToList();

                    for (int i = 0; i < quotesAndTrades.Count; i++)
                    {
                        context.DeleteObject(quotesAndTrades[i]);
                    }

                    context.SaveChanges();
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        public void SaveQuotesAndTrades(QuotesAndTrades model)
        {
            using (LoanPriceEntities context = new LoanPriceEntities())
            {
                if (model.ID > 0)
                {
                    context.AddObject("QuotesAndTrades", model);
                }
                else
                {
                    context.AddToQuotesAndTrades(model);
                }
                context.SaveChanges();
            }
        }

        public void AddImportedQuotesAndTrades(List<QuotesAndTrades> lst)
        {
            try
            {
                using (LoanPriceEntities context = new LoanPriceEntities())
                {
                    foreach (var item in lst)
                    {
                        context.AddToQuotesAndTrades(item);
                    }
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool CheckForExist(string loanName, string counterParty)
        {
            using (LoanPriceEntities context = new LoanPriceEntities())
            {
                List<QuotesAndTrades> quoteList = context.QuotesAndTrades.Where(s => s.LoanName == loanName && s.CounterParty == counterParty).ToList();
                if (quoteList != null && quoteList.Count > 0)
                    return true;
                else
                    return false;
            }
        }

        public QuotesAndTrades GetQuoteAndTrade(int id)
        {
            try
            {
                using (LoanPriceEntities context = new LoanPriceEntities())
                {
                    return context.QuotesAndTrades.SingleOrDefault(s => s.ID == id);
                }
            }
            catch (Exception)
            {
                return null;
            }
        }


        public void SaveQuote(QuotesAndTrades model)
        {
            using (LoanPriceEntities context = new LoanPriceEntities())
            {
                if (model.ID > 0)
                {
                    context.AddObject("QuotesAndTrades", model);
                }

                context.SaveChanges();
            }
        }

        public bool RemoveQuote(int id)
        {
            using (LoanPriceEntities context = new LoanPriceEntities())
            {
                QuotesAndTrades model = context.QuotesAndTrades.SingleOrDefault(c => c.ID == id);
                if (model != null)
                {
                    // Delete the Password policy of this account
                    context.QuotesAndTrades.DeleteObject(model);
                    context.SaveChanges();
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
        public void UpdateQuote(QuotesAndTrades quote)
        {
            using (LoanPriceEntities context = new LoanPriceEntities())
            {
                QuotesAndTrades model = context.QuotesAndTrades.SingleOrDefault(c => c.ID == quote.ID);
                if (model != null)
                {
                    model.CounterParty = quote.CounterParty;
                    model.BidPrice = quote.BidPrice;
                    model.OfferPrice = quote.OfferPrice;
                    model.BidSpread = quote.BidSpread;
                    model.OfferSpread = quote.OfferSpread;
                    model.Traded = quote.Traded;
                    model.MarketValue = quote.MarketValue;
                    model.Country = quote.Country;
                    model.TimeStamp = quote.TimeStamp;

                    model.LoanName = quote.LoanName;
                    model.AvgLifeDisc = quote.AvgLifeDisc;
                    model.AvgLifeNonDisc = quote.AvgLifeNonDisc;
                    model.AvgLifeRiskDisc = quote.AvgLifeRiskDisc;
                    if (quote.SettlementDate != Convert.ToDateTime("01/01/0001 00:00:00"))
                    {
                        model.SettlementDate = quote.SettlementDate;
                    }
                    model.TradedDate = quote.TradedDate;
                    model.Margin = quote.Margin;
                    context.SaveChanges();
                }
            }
        }

        public QuotesAndTrades GetQuotesAndTrades(string loanName)
        {
            using (LoanPriceEntities context = new LoanPriceEntities())
            {
                QuotesAndTrades model = context.QuotesAndTrades.OrderByDescending(s => s.ID).First(c => c.LoanName == loanName);
                return model;
            }
        }

    }
}
