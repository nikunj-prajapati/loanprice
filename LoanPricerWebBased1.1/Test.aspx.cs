using BLL;
using DAL;
using iTextSharp.text;
using iTextSharp.text.pdf;
using LoanPricerWebBased.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LoanPricerWebBased
{
    public partial class Test : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            lblInof.Text = Server.MapPath("~/Pdf");
        }
    }
}