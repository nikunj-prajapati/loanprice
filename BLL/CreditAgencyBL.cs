using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class CreditAgencyBL
    {
        public CreditAgency GetAgency(int id)
        {
            using (LoanPriceEntities context = new LoanPriceEntities())
            {
                return context.CreditAgencies.FirstOrDefault(c => c.ID == id);
            }
        }

        public List<CreditAgency> GetAgencies()
        {
            using (LoanPriceEntities context = new LoanPriceEntities())
            {
                return context.CreditAgencies.OrderBy(s => s.CreditAgency1).ToList();
            }
        }
        //public bool IsValidName(string name)
        //{
        //    using (LoanPriceEntities context = new LoanPriceEntities())
        //    {
        //        int count = context.CreditRatings.Where(s => s. == name).Count();
        //        if (count == 0)
        //            return true;
        //        else
        //            return false;
        //    }
        //}
        public string SaveAgency(CreditAgency agency)
        {
            using (LoanPriceEntities context = new LoanPriceEntities())
            {
                if (agency.ID <= 0)
                {

                    //if (context.CreditRatings.Where(s => s.CreditAgency == rating.CreditAgency).Count() == 0)
                    //{
                    context.AddToCreditAgencies(agency);
                    context.SaveChanges();
                    return "Credit Agency is added successfully";
                    //}
                    //else
                    //    return "Entry of the same Currency is already exists.";
                }
                else
                {
                    //if (context.Currencies.Where(s => s.Currancy == currency.Currancy && s.ID != currency.ID).Count() == 0)
                    //{
                    context.CreditAgencies.Attach(agency);
                    context.ObjectStateManager.ChangeObjectState(agency, System.Data.EntityState.Modified);
                    context.SaveChanges();
                    return "Credit Agency is Updated successfully";
                    //}
                    //else
                    //    return "Entry of the same Currency is already exists.";
                }
            }
        }
        public void Delete(int id)
        {
            using (LoanPriceEntities context = new LoanPriceEntities())
            {
                CreditAgency group = context.CreditAgencies.FirstOrDefault(c => c.ID == id);
                if (group != null)
                {
                    context.CreditAgencies.DeleteObject(group);
                    context.SaveChanges();
                }
            }
        }
        public CreditAgency GetByID(int id)
        {
            using (LoanPriceEntities context = new LoanPriceEntities())
            {
                return context.CreditAgencies.SingleOrDefault(c => c.ID == id);
            }
        }
    }
}
