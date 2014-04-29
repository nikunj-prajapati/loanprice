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
    public partial class MessageBox : BaseUserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public void Show(string header, string message)
        {
            lblHeader.Text = header;
            lblMessage.Text = message;
            mpeMessageBox.Show();
        }
    }
}