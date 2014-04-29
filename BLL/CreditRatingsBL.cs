using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;

namespace BLL
{
    public class CreditRatingsBL
    {

        public CreditRating  GetRatings(int id)
        {
            using (LoanPriceEntities context = new LoanPriceEntities())
            {
                return context.CreditRatings.FirstOrDefault(c => c.ID == id);
            }
        }

        public List<CreditRating> GetRatings()
        {
            using (LoanPriceEntities context = new LoanPriceEntities())
            {
                return context.CreditRatings.OrderBy(s => s.CreditAgencyID).ToList();
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
        public string SaveRating(CreditRating rating)
        {
            using (LoanPriceEntities context = new LoanPriceEntities())
            {
                if (rating.ID <= 0)
                {

                    //if (context.CreditRatings.Where(s => s.CreditAgency == rating.CreditAgency).Count() == 0)
                    //{
                        context.AddToCreditRatings(rating);
                        context.SaveChanges();
                        return "Credit Rating is added successfully";
                    //}
                    //else
                    //    return "Entry of the same Currency is already exists.";
                }
                else
                {
                    //if (context.Currencies.Where(s => s.Currancy == currency.Currancy && s.ID != currency.ID).Count() == 0)
                    //{
                    context.CreditRatings.Attach(rating);
                    context.ObjectStateManager.ChangeObjectState(rating, System.Data.EntityState.Modified);
                        context.SaveChanges();
                        return "Credit Rating is Updated successfully";
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
                CreditRating group = context.CreditRatings.FirstOrDefault(c => c.ID == id);
                if (group != null)
                {
                    context.CreditRatings.DeleteObject(group);
                    context.SaveChanges();
                }
            }
        }
        public CreditRating GetByID(int id)
        {
            using (LoanPriceEntities context = new LoanPriceEntities())
            {
                return context.CreditRatings.SingleOrDefault(c => c.ID == id);
            }
        }

        public List<CreditRating> GetRatingsByAgencyID(int id)
        {
            using (LoanPriceEntities context = new LoanPriceEntities())
            {
                return context.CreditRatings.Where(c => c.CreditAgencyID == id).ToList();
            }
        }

    }
}
