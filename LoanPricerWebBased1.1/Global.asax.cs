using LoanPricerWebBased.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Timers;
using System.Web;
using System.Web.Caching;
using System.Web.Routing;
using System.Web.Security;
using System.Web.SessionState;

namespace LoanPricerWebBased
{
    public class Global : System.Web.HttpApplication
    {
        private static CacheItemRemovedCallback OnCacheRemove = null;

        protected void Application_Start(object sender, EventArgs e)
        {
            // Run after 24 hours
            AddTask("DoStuff", 86400);
        }

        private void AddTask(string name, int seconds)
        {
            OnCacheRemove = new CacheItemRemovedCallback(CacheItemRemoved);
            HttpRuntime.Cache.Insert(name, seconds, null, DateTime.Now.AddSeconds(seconds), Cache.NoSlidingExpiration, CacheItemPriority.NotRemovable, OnCacheRemove);
        }

        public void CacheItemRemoved(string k, object v, CacheItemRemovedReason r)
        {
            // do stuff here if it matches our taskname, like WebRequest
            // re-add our task so it recurs
            ReportHelper.GenerateAndSendDailyPDFReport();
            AddTask(k, Convert.ToInt32(v));
        }
    }
}