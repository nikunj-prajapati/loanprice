using BLL;
using iTextSharp.text;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;
using LoanPricerWebBased.Helpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace LoanPricerWebBased
{
    public partial class ShowLog : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindLogs();
            }
        }

        public void BindLogs()
        {
            ActivityLogBL bl = new ActivityLogBL();
            grdLogs.DataSource = bl.GetTodayLogs();
            grdLogs.DataBind();

            //GeneratePDF();
        }

        public void GeneratePDF()
        {
            ReportHelper.GenerateAndSendDailyPDFReport();
        }

        protected void btnGeneratePDF_Click(object sender, EventArgs e)
        {
            GeneratePDF();
        }
    }
}