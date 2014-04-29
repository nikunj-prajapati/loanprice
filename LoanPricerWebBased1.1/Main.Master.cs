using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BLL;
using DAL;

namespace LoanPricerWebBased
{
    public partial class Main : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["LogedInUser"] != null && (Session["LogedInUser"] as DAL.Login) != null)
                {
                    DAL.Login login = Session["LogedInUser"] as DAL.Login;
                    if (login != null)
                    {
                        lblUserName.Text = login.Name;
                        LoansBLL loanBLL = new LoansBLL();
                        lblDetails.InnerText = DateTime.Now.ToString() + " - " + loanBLL.GetLoanCount() + " Records in Loans DB";
                        // Show the Admin tab only to Administrator role
                        if (login.Role == AppConstants.Roles.Admin.ToString() && !string.IsNullOrEmpty(Request.Path))
                        {
                            hlAdministration.Visible = true;
                            if (Request.Path.Contains("Administration"))
                            {
                                hlAdministration.Text = "Main";
                                hlAdministration.NavigateUrl = "~/Default.aspx";
                                lblAdministration.Visible = true;
                            }
                            else if (Request.Path.Contains("/administration.aspx"))
                            {
                                hlAdministration.Text = "Main";
                                hlAdministration.NavigateUrl = "~/Default.aspx";
                                lblAdministration.Visible = true;
                            }
                            else
                            {
                                hlAdministration.Text = "Administration";
                                hlAdministration.NavigateUrl = "~/Administration.aspx";
                            }
                        }
                        else
                        {
                            hlAdministration.Visible = false;
                            lblAdministration.Visible = false;
                        }
                    }
                }
            }
        }

        protected void tmrLoanDiv_Tick(object sender, EventArgs e)
        {
            LoansBLL loanBLL = new LoansBLL();
            lblDetails.InnerText = DateTime.Now.ToString() + " - " + loanBLL.GetLoanCount() + " Records in Loans DB";
        }
    }
}
