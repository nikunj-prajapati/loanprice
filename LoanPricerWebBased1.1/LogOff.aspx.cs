using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

namespace LoanPricerWebBased
{
    public partial class LogOff : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["LogedInUser"] != null && (Session["LogedInUser"] as DAL.Login) != null)
                {
                    LogActivity("Loged Off", "Loged off from the system", string.Empty);
                    DAL.Login login = Session["LogedInUser"] as DAL.Login;
                    if (login != null)
                    {
                        Session["LogedInUser"] = null;
                        Session.Abandon();
                        GC.Collect();
                    }
                    Response.Redirect("~/Login.aspx");
                }
            }
            Session.Clear();
            Session.RemoveAll();
            // Move back to the login page
            Response.Redirect("~/Login.aspx");
        }
    }
}
