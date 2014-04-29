using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;

namespace BLL
{
    public class SettingsBLL
    {
        public void InsertUpdateSettings(Setting data)
        {
            using (LoanPriceEntities context = new LoanPriceEntities())
            {
                Setting setting = context.Settings.SingleOrDefault(s => s.Name == data.Name);
                if (setting != null)
                {
                    setting.Value = data.Value;
                    context.SaveChanges();
                }
                else
                {
                    context.Settings.AddObject(data);
                    context.SaveChanges();
                }
            }
        }
        public Setting GetSettingyear(string name)
        {
            using (LoanPriceEntities context = new LoanPriceEntities())
            {
                Setting setting = context.Settings.SingleOrDefault(s => s.Name == name);
                if (setting != null)
                    return setting;
                else
                    return null;
            }

        }
     
    }
}
