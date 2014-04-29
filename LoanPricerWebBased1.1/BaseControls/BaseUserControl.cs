using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public class BaseUserControl : UserControl
{
    protected BasePage MyPage
    {
        get
        {
            return (BasePage)this.Page;
        }
    }

    public void ShowMessage(string header, string message)
    {
        MyPage.ShowMessage(header, message);
    }
}
