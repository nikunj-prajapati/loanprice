using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class CountryBL
    {
        public tblCountry GetCountry(int id)
        {
            using (LoanPriceEntities context = new LoanPriceEntities())
            {
                return context.tblCountry.FirstOrDefault(c => c.ID == id);
            }
        }

        public List<tblCountry> GetCountry()
        {
            using (LoanPriceEntities context = new LoanPriceEntities())
            {
                return context.tblCountry.OrderBy(s => s.Name).ToList();
            }
        }
        public bool IsValidName(string countryName)
        {
            using (LoanPriceEntities context = new LoanPriceEntities())
            {
                int count = context.tblCountry.Where(s => s.Name == countryName).Count();
                if (count == 0)
                    return true;
                else
                    return false;
            }
        }
        public string SaveCountry(tblCountry country)
        {
            using (LoanPriceEntities context = new LoanPriceEntities())
            {
                if (country.ID <= 0)
                {
                    if (context.tblCountry.Where(s => s.Name == country.Name).Count() == 0)
                    {
                        context.AddTotblCountry(country);
                        context.SaveChanges();
                        return "Country is added successfully";
                    }
                    else
                        return "Entry of the same Name is already exists.";
                }
                else
                {
                    context.tblCountry.Attach(country);
                    context.ObjectStateManager.ChangeObjectState(country, System.Data.EntityState.Modified);
                    context.SaveChanges();
                    return "Country is Updates successfully";
                }
            }
        }
        public void Delete(int id)
        {
            using (LoanPriceEntities context = new LoanPriceEntities())
            {
                tblCountry group = context.tblCountry.FirstOrDefault(c => c.ID == id);
                if (group != null)
                {
                    context.tblCountry.DeleteObject(group);
                    context.SaveChanges();
                }
            }
        }
        public tblCountry GetByID(int id)
        {
            using (LoanPriceEntities context = new LoanPriceEntities())
            {
                return context.tblCountry.SingleOrDefault(c => c.ID == id);
            }
        }
        public int GetCountryFromName(string name)
        {
            using (LoanPriceEntities context = new LoanPriceEntities())
            {
                tblCountry group = context.tblCountry.FirstOrDefault(c => c.Name == name);
                if (group != null)
                    return group.ID;
                else
                    return 0;
            }
        }
    }
}
