using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class LoanHistoryBL
    {
        public string SaveHistory(LoanHistory history)
        {
            using (LoanPriceEntities context = new LoanPriceEntities())
            {
                context.AddToLoanHistories(history);
                context.SaveChanges();
                return "History is added successfully";
            }
        }
    }
}
