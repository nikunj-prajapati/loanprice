using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class GroupBL
    {
        public List<Group> GetAll()
        {
            using (LoanPriceEntities context = new LoanPriceEntities())
            {
                return context.Groups.ToList();
            }
        }

        public Group GetById(int id)
        {
            using (LoanPriceEntities context = new LoanPriceEntities())
            {
                return context.Groups.FirstOrDefault(c => c.ID == id);
            }
        }

        public void SetEmailReceiver(int id)
        {
            using (LoanPriceEntities context = new LoanPriceEntities())
            {
               // uncheck all
                List<Group> groups = context.Groups.ToList();
                for (int i = 0; i < groups.Count; i++)
                {
                    groups[i].IsEmailReceiver = false;
                }

                groups.FirstOrDefault(c => c.ID == id).IsEmailReceiver = true;

                context.SaveChanges();
            }
        }

        public bool Delete(int id)
        {
            using (LoanPriceEntities context = new LoanPriceEntities())
            {
                Group group = context.Groups.FirstOrDefault(c => c.ID == id);
                if (group != null)
                {
                    context.Groups.DeleteObject(group);
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
