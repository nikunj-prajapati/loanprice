using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class EmailGroupsBL
    {
        public List<EmailGroup> GetEmailReceiver()
        {
            using (LoanPriceEntities context = new LoanPriceEntities())
            {
                // Get email receiver group
                Group group = context.Groups.FirstOrDefault(c => c.IsEmailReceiver);
                if (group != null)
                {
                    return context.EmailGroups.Where(c => c.GroupName == group.Name).ToList();
                }
                return null;
            }
        }

        public List<EmailGroup> GetEmailReceiver(string group)
        {
            using (LoanPriceEntities context = new LoanPriceEntities())
            {
                // Get email receiver group
                return context.EmailGroups.Where(c => c.GroupName == group).ToList();
                return null;
            }
        }

        public List<EmailGroup> GetAll()
        {
            using (LoanPriceEntities context = new LoanPriceEntities())
            {
                return context.EmailGroups.ToList();
            }
        }

        public EmailGroup GetById(int id)
        {
            using (LoanPriceEntities context = new LoanPriceEntities())
            {
                return context.EmailGroups.FirstOrDefault(c => c.ID == id);
            }
        }

        public EmailGroup Save(EmailGroup model)
        {
            using (LoanPriceEntities context = new LoanPriceEntities())
            {
                if (model.ID <= 0)
                {
                    context.EmailGroups.AddObject(model);
                }
                else
                {
                    context.EmailGroups.Attach(model);
                    context.ObjectStateManager.ChangeObjectState(model, System.Data.EntityState.Modified);
                }

                context.SaveChanges();
                return model;
            }
        }

        public bool Delete(int id)
        {
            using (LoanPriceEntities context = new LoanPriceEntities())
            {
                EmailGroup group = context.EmailGroups.FirstOrDefault(c => c.ID == id);
                if (group != null)
                {
                    context.EmailGroups.DeleteObject(group);
                    context.SaveChanges();
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
    }
}
