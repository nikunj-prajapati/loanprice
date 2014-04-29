using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class CounterPartyBL
    {
        public CounterParty GetCounterParty(int id)
        {
            using (LoanPriceEntities context = new LoanPriceEntities())
            {
                return context.CounterParties.FirstOrDefault(c => c.ID == id);
            }
        }
       
        public List<CounterParty> GetCounterParty()
        {
            using (LoanPriceEntities context = new LoanPriceEntities())
            {
                return context.CounterParties.OrderBy(s => s.Name).ToList();
            }
        }
        public bool IsValidName(string counterPartyName)
        {
            using (LoanPriceEntities context = new LoanPriceEntities())
            {
                int count = context.CounterParties.Where(s => s.Name == counterPartyName).Count();
                if (count == 0)
                    return true;
                else
                    return false;
            }
        }
        public string SavecounterParty(CounterParty counterParty)
        {
            using (LoanPriceEntities context = new LoanPriceEntities())
            {
                if (counterParty.ID <= 0)
                {
                    if (context.CounterParties.Where(s => s.Name == counterParty.Name).Count() == 0)
                    {
                        context.AddToCounterParties(counterParty);
                        context.SaveChanges();
                        return "CounterParty is added successfully";
                    }
                    else
                        return "Entry of the same Name is already exists.";
                }
                else
                {
                    if (context.CounterParties.Where(s => s.Name == counterParty.Name && s.ID != counterParty.ID).Count() == 0)
                    {
                        context.CounterParties.Attach(counterParty);
                        context.ObjectStateManager.ChangeObjectState(counterParty, System.Data.EntityState.Modified);
                        context.SaveChanges();
                        return "CounterParty is Updated successfully";
                    }
                    else
                        return "Entry of the same Name is already exists.";
                }
            }
        }
        public void Delete(int id)
        {
            using (LoanPriceEntities context = new LoanPriceEntities())
            {
                CounterParty group = context.CounterParties.FirstOrDefault(c => c.ID == id);
                if (group != null)
                {
                    context.CounterParties.DeleteObject(group);
                    context.SaveChanges();
                }
            }
        }
        public CounterParty GetByID(int id)
        {
            using (LoanPriceEntities context = new LoanPriceEntities())
            {
                return context.CounterParties.SingleOrDefault(c => c.ID == id);
            }
        }
        public int GetcounterPartyFromName(string name)
        {
            using (LoanPriceEntities context = new LoanPriceEntities())
            {
                CounterParty group = context.CounterParties.FirstOrDefault(c => c.Name == name);
                if (group != null)
                    return group.ID;
                else
                    return 0;
            }
        }

        public void AddImportedQuotesAndTrades(List<CounterParty> lst)
        {
            try
            {
                using (LoanPriceEntities context = new LoanPriceEntities())
                {
                    foreach (var item in lst)
                    {
                        context.AddToCounterParties(item);
                    }
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                
            }
        }
    }
}
