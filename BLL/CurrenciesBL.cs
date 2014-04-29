using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class CurrenciesBL
    {

        public Currency GetCurrency(int id)
        {
            using (LoanPriceEntities context = new LoanPriceEntities())
            {
                return context.Currencies.FirstOrDefault(c => c.ID == id);
            }
        }

        public List<Currency> GetCurrency()
        {
            using (LoanPriceEntities context = new LoanPriceEntities())
            {
                return context.Currencies.OrderBy(s => s.Currancy).ToList();
            }
        }
        public bool IsValidName(string currencyName)
        {
            using (LoanPriceEntities context = new LoanPriceEntities())
            {
                int count = context.Currencies.Where(s => s.Currancy == currencyName).Count();
                if (count == 0)
                    return true;
                else
                    return false;
            }
        }
        public string SaveCurrency(Currency currency)
        {
            using (LoanPriceEntities context = new LoanPriceEntities())
            {
                if (currency.ID <= 0)
                {

                    if (context.Currencies.Where(s => s.Currancy == currency.Currancy).Count() == 0)
                    {
                        context.AddToCurrencies(currency);
                        context.SaveChanges();
                        return "Currency is added successfully";
                    }
                    else
                        return "Entry of the same Currency is already exists.";
                }
                else
                {
                    if (context.Currencies.Where(s => s.Currancy == currency.Currancy && s.ID != currency.ID).Count() == 0)
                    {
                        context.Currencies.Attach(currency);
                        context.ObjectStateManager.ChangeObjectState(currency, System.Data.EntityState.Modified);
                        context.SaveChanges();
                        return "Currency is Updated successfully";
                    }
                    else
                        return "Entry of the same Currency is already exists.";
                }
            }
        }
        public void Delete(int id)
        {
            using (LoanPriceEntities context = new LoanPriceEntities())
            {
                Currency group = context.Currencies.FirstOrDefault(c => c.ID == id);
                if (group != null)
                {
                    context.Currencies.DeleteObject(group);
                    context.SaveChanges();
                }
            }
        }
        public Currency GetByID(int id)
        {
            using (LoanPriceEntities context = new LoanPriceEntities())
            {
                return context.Currencies.SingleOrDefault(c => c.ID == id);
            }
        }

    }
}
