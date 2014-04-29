using DAL;
using System;
using System.Collections.Generic;
using System.Data.Objects;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class EmailQueBL
    {
        public tblEmailQue GetEmailQue(int id)
        {
            using (LoanPriceEntities context = new LoanPriceEntities())
            {
                return context.tblEmailQues.FirstOrDefault(c => c.ID == id);
            }
        }

        public bool IsTodayEmailSent()
        {
            using (LoanPriceEntities context = new LoanPriceEntities())
            {
                DateTime date = DateTime.Now.Date;
                tblEmailQue que = context.tblEmailQues.FirstOrDefault(c => EntityFunctions.TruncateTime(c.SendTime) == date);
                if (que == null)
                {
                    return false;
                }
                else
                {
                    return que.IsSent;
                }
            }
        }

        public void AddEmailQue(tblEmailQue model)
        {
            using (LoanPriceEntities context = new LoanPriceEntities())
            {
                tblEmailQue que = context.tblEmailQues.FirstOrDefault(c => EntityFunctions.TruncateTime(c.SendTime) == EntityFunctions.TruncateTime(model.SendTime));
                if (que == null)
                {
                    context.tblEmailQues.AddObject(model);
                    context.SaveChanges();
                }
            }
        }
    }
}
