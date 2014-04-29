using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LoanPricerWebBased
{
    public partial class WebForm3 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
              //  Test();
            }
        }

        protected void Test()
        {
            //string filename = jobSeekerCV.CVPath;
            string filename = "635242810326992843.doc";
            Response.ContentType = "application/octet-stream";
            Response.AppendHeader("Content-Disposition", "attachment;filename=" + filename);
            string aaa = Server.MapPath("~/SavedFolder/" + filename);
            Response.TransmitFile(Server.MapPath("~/SavedFolder/" + filename));
            Response.End();
        }
    }
}