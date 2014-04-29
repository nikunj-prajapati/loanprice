using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAL;

namespace BLL
{
    public class PublicHolidayBLL
    {
        public List<tblHoliday> GetPublicHolidays(int id)
        {
            using (LoanPriceEntities context =new LoanPriceEntities())
            {
                return context.tblHoliday.Where(c => c.tblCountry.ID == id).ToList();
            }
        }

    }
}
