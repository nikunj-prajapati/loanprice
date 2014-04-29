using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class LogsBLL
    {
        public bool LogActivity(string activity, string message, string ex)
        {
            tblActivityLog model = new tblActivityLog();

            model.Activity = activity;
            model.ActivityDate = DateTime.Now;
            model.Exception = ex;

            model.Message = message;


            ActivityLogBL bl = new ActivityLogBL();
            model = bl.AddActivityLog(model);
            return true;

        }
    }
}
