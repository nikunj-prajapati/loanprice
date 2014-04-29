using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
namespace BLL
{
    public class BorrowersBL
    {
        public Borrower GetBorrowers(int id)
        {
            using (LoanPriceEntities context = new LoanPriceEntities())
            {
                return context.Borrowers.FirstOrDefault(c => c.ID == id);
            }
        }

        public List<Borrower> GetBorrowers()
        {
            using (LoanPriceEntities context = new LoanPriceEntities())
            {
                return context.Borrowers.OrderBy(s => s.Name).ToList();
            }
        }

        public bool IsValidName(string borrowerName)
        {
            using (LoanPriceEntities context = new LoanPriceEntities())
            {
                int count = context.Borrowers.Where(s => s.Name == borrowerName).Count();
                if (count == 0)
                    return true;
                else
                    return false;
            }
        }

        public string SaveBorrower(Borrower borrower)
        {
            using (LoanPriceEntities context = new LoanPriceEntities())
            {
                if (borrower.ID <= 0)
                {

                    if (context.Borrowers.Where(s => s.Name == borrower.Name).Count() == 0)
                    {
                        context.AddToBorrowers(borrower);
                        context.SaveChanges();
                        return "Borrower is added successfully";
                    }
                    else
                        return "Entry of the same Name is already exists.";
                }
                else
                {
                    if (context.Borrowers.Where(s => s.Name == borrower.Name && s.ID != borrower.ID).Count() == 0)
                    {
                        context.Borrowers.Attach(borrower);
                        context.ObjectStateManager.ChangeObjectState(borrower, System.Data.EntityState.Modified);
                        context.SaveChanges();
                        return "Borrower is Updated successfully";
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
                Borrower group = context.Borrowers.FirstOrDefault(c => c.ID == id);
                if (group != null)
                {
                    context.Borrowers.DeleteObject(group);
                    context.SaveChanges();
                }
            }
        }
        public Borrower GetByID(int id)
        {
            using (LoanPriceEntities context = new LoanPriceEntities())
            {
                return context.Borrowers.SingleOrDefault(c => c.ID == id);
            }
        }
        public int GetBorrowerFromName(string name)
        {
            using (LoanPriceEntities context = new LoanPriceEntities())
            {
                Borrower borrower = context.Borrowers.FirstOrDefault(c => c.Name == name);
                if (borrower != null)
                    return borrower.ID;
                else
                    return 0;
            }
        }

        public void AddImportedQuotesAndTrades(List<Borrower> lst)
        {
            try
            {
                using (LoanPriceEntities context = new LoanPriceEntities())
                {
                    foreach (var item in lst)
                    {
                        context.AddToBorrowers(item);
                    }
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {

            }
        }

        public Borrower GetBorrower(string name)
        {
            try
            {
                using (LoanPriceEntities context = new LoanPriceEntities())
                {
                    return context.Borrowers.SingleOrDefault(s => s.Name == name);
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

    }
}
