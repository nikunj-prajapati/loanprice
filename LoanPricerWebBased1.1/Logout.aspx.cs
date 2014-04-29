using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;

namespace LoanPricerWebBased
{
    public partial class Logout : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            LoginBLL bll = new LoginBLL();
             DAL.Login login = Session["LogedInUser"] as DAL.Login;
             if (login != null)
             {
                 bll.SetLogin(login, 2);
                 Session.Remove("LogedInUser");
                 Session.Clear();
                 Session.RemoveAll();
                 GC.Collect();
                 Response.Redirect("Login.aspx");
                 
             }
             Session.Remove("LogedInUser");
             Session.Clear();
             Session.RemoveAll();
             GC.Collect();
             Response.Redirect("Login.aspx");

        }
    }
}