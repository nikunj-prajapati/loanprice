using DAL;
using System;
using System.Collections.Generic;
using System.Data.Objects;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class ActivityLogBL
    {
        public tblActivityLog AddActivityLog(tblActivityLog model)
        {
            using (LoanPriceEntities context = new LoanPriceEntities())
            {
                context.tblActivityLogs.AddObject(model);
                context.SaveChanges();
                return model;
            }
        }

        public List<tblActivityLog> GetAllLogs()
        {
            using (LoanPriceEntities context = new LoanPriceEntities())
            {
                return context.tblActivityLogs.ToList();
            }
        }

        public List<tblActivityLog> GetTodayLogs()
        {
            using (LoanPriceEntities context = new LoanPriceEntities())
            {
                DateTime date = DateTime.Now.Date;
                return context.tblActivityLogs.Where(c => EntityFunctions.TruncateTime(c.ActivityDate) == date).ToList();
            }
        }

        public bool RemoveLast60DayLogs()
        {
            try
            {
                using (LoanPriceEntities context = new LoanPriceEntities())
                {
                    var oldRecords = context.tblActivityLogs.Where(c => c.ActivityDate > DateTime.Now.AddDays(-60));
                    foreach (var item in oldRecords)
                    {
                        context.tblActivityLogs.DeleteObject(item);
                    }
                    context.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
