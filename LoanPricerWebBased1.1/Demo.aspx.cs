using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

public partial class Demo : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string urlWithSessionID = Response.ApplyAppPathModifier(Request.Url.PathAndQuery);
            RadTab tab = RadTabStrip1.FindTabByUrl(urlWithSessionID);
            if (tab != null)
            {
                tab.SelectParents();
                tab.PageView.Selected = true;
            }
        }

    }
}