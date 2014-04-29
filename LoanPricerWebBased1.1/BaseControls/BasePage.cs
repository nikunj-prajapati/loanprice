using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using LoanPricerWebBased;
using BLL;
using DAL;

public class BasePage : Page
{
    public string ErrorColor
    {
        get
        {
            return WebUtility.GetErrorColor();
        }
    }

    public DAL.Login LoggedInUser
    {
        get
        {
            if (Session["LogedInUser"] != null)
            {
                return (DAL.Login)Session["LogedInUser"];
            }
            else
            {
                return null;
            }
        }
    }

    public void ShowMessage(string header, string message)
    {
        MessageBox messageBox = (MessageBox)this.Page.Master.FindControl("MyMessageBox");
        if (messageBox != null)
        {
            messageBox.Show(header, message);
        }
    }

    public bool LogActivity(string activity, string message, string ex)
    {
        if (LoggedInUser != null)
        {
            tblActivityLog model = new tblActivityLog();

            model.Activity = activity;
            model.ActivityDate = DateTime.Now;
            model.Exception = ex;
            model.IP = GetUserIP();
            model.Message = message;
            model.SessionID = Session.SessionID;
            model.UserID = LoggedInUser.ID;
            model.UserName = LoggedInUser.Name;
            model.URL = Request.Url.AbsoluteUri;

            ActivityLogBL bl = new ActivityLogBL();
            model = bl.AddActivityLog(model);
            return true;
        }
        else
        {
            return false;
        }
    }

    private string GetUserIP()
    {
        string ipList = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];

        if (!string.IsNullOrEmpty(ipList))
        {
            return ipList.Split(',')[0];
        }

        return Request.ServerVariables["REMOTE_ADDR"];
    }
}
