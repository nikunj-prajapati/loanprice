using System.ComponentModel;
using BLL;
using DAL;
using LoanPricerWebBased.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using System.Configuration;
using System.Data;
using System.IO;
using System.Reflection;
using System.Data.OleDb;

namespace LoanPricerWebBased
{
    public partial class Administration : BasePage
    {

        protected string agenyName = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //txtIdleTime.Text = "15" ;

                var configuration = WebConfigurationManager.OpenWebConfiguration("~");
                var section = (SessionStateSection)WebConfigurationManager.GetSection("system.web/sessionState");


                double timeout = 0;
                if (section != null)
                    timeout = section.Timeout.TotalMinutes;
                txtIdleTime.InnerText = timeout.ToString();
                //    section.Timeout = new TimeSpan(0,Convert.ToInt16( txtIdleTime.Text),0);
                /// Only administrator can view this page
                /// 

                string urlWithSessionID = Response.ApplyAppPathModifier(Request.Url.PathAndQuery);
                RadTab tab = RadTabStrip1.FindTabByUrl(urlWithSessionID);
                if (tab != null)
                {
                    tab.SelectParents();
                    tab.PageView.Selected = true;
                }

                Sorting();

                //on 08-01
                BindCurrentYear();

                if (Session["LogedInUser"] != null && (Session["LogedInUser"] as DAL.Login) != null)
                {
                    // Show the Admin tab only to Administrator role
                    if ((Session["LogedInUser"] as DAL.Login).Role == AppConstants.Roles.Admin.ToString())
                    {
                        BindAccounts();
                        BindEmails(); BindRegions();
                        BindCounterParties(); BindBorrowers();
                        BindCountry(); BindCurrencies(); BindCreditRatings(); BindCreditAgencies(); BindAgencyDropDown();
                        GroupBL bl = new GroupBL();
                        List<Group> groups = bl.GetAll(); ;
                        ddlGroups.DataSource = groups;
                        ddlGroups.DataTextField = "Name";
                        ddlGroups.DataValueField = "ID";
                        ddlGroups.DataBind();

                        ddlGroupReceiver.DataSource = groups;
                        ddlGroupReceiver.DataTextField = "Name";
                        ddlGroupReceiver.DataValueField = "ID";
                        ddlGroupReceiver.DataBind();
                        Group receiver = groups.FirstOrDefault(c => c.IsEmailReceiver);
                        if (receiver != null)
                        {
                            //     DropDownListItem item = ddlGroupReceiver.FindItemByText(receiver.Name);
                            ListItem item = ddlGroupReceiver.Items.FindByText(receiver.Name);

                            if (item != null)
                            {
                                item.Selected = true;
                            }
                        }
                        BindDuplicateLoansTab();
                    }
                    else
                    {
                        // Move back to the login page
                        Response.Redirect("~/Default.aspx");
                    }
                }
                else
                {
                    // Move back to the login page
                    Response.Redirect("~/Banner.aspx");
                }
            }
        }
        private void BindCurrentYear()
        {
            SettingsBLL settingBL = new SettingsBLL();
            Setting setting = settingBL.GetSettingyear("Loan Year Settings");
            if (setting != null)
                txtCurrentLoanYear.InnerText = setting.Value;
            else
                txtCurrentLoanYear.InnerText = "";
        }
        private void Sorting()
        {
            GridSortExpression sortExpr = new GridSortExpression();
            sortExpr.FieldName = "Name";
            sortExpr.SortOrder = GridSortOrder.Ascending;
            grdAccounts.MasterTableView.SortExpressions.AddSortExpression(sortExpr);

            sortExpr = new GridSortExpression();
            sortExpr.FieldName = "Name";
            sortExpr.SortOrder = GridSortOrder.Ascending;
            grdEmail.MasterTableView.SortExpressions.AddSortExpression(sortExpr);

            sortExpr = new GridSortExpression();
            sortExpr.FieldName = "Name";
            sortExpr.SortOrder = GridSortOrder.Ascending;
            grdUsers.MasterTableView.SortExpressions.AddSortExpression(sortExpr);

            sortExpr = new GridSortExpression();
            sortExpr.FieldName = "Name";
            sortExpr.SortOrder = GridSortOrder.Ascending;
            grdCountry.MasterTableView.SortExpressions.AddSortExpression(sortExpr);

            sortExpr = new GridSortExpression();
            sortExpr.FieldName = "CodeName";
            sortExpr.SortOrder = GridSortOrder.Ascending;
            grdDuplicateLoans.MasterTableView.SortExpressions.AddSortExpression(sortExpr);

        }

        private void BindAccounts()
        {
            LoginBLL bll = new LoginBLL();
            List<DAL.Login> lockedAccounts = bll.GetLockedAccounts();
            grdUsers.DataSource = lockedAccounts;
            grdUsers.DataBind();
            if (lockedAccounts == null || lockedAccounts.Count <= 0)
            {
                lblLockedAccountMessage.Text = "No locked accounts.";
            }
            else
            {
                lblLockedAccountMessage.Text = lockedAccounts.Count + " locked accounts.";
            }
            grdAccounts.DataSource = bll.GetAccounts();
            grdAccounts.DataBind();

            grdLockedAcc.DataSource = bll.GetSigninAccounts();
            grdLockedAcc.DataBind();
        }

        private void BindEmails()
        {
            EmailGroupsBL bl = new EmailGroupsBL();
            grdEmail.DataSource = bl.GetAll().OrderBy(c => c.GroupName).ToList();
            grdEmail.DataBind();
        }
        private void BindRegions()
        {
            CountryBL countryBL = new CountryBL();
            txtCPRegion.DataSource = countryBL.GetCountry();
            txtCPRegion.DataTextField = "Name";
            txtCPRegion.DataValueField = "Name";
            txtCPRegion.DataBind();


            ddlBorrowerRegion.DataSource = countryBL.GetCountry();
            ddlBorrowerRegion.DataTextField = "Name";
            ddlBorrowerRegion.DataValueField = "Name";
            ddlBorrowerRegion.DataBind();
        }
        //protected void btnRemoveAllLoan_Click(object sender, EventArgs e)
        //{

        //}

        //protected void btnRemoveAllQuotesAndTrades_Click(object sender, EventArgs e)
        //{
        //    QuotesAndTradesBLL bll = new QuotesAndTradesBLL();
        //    bool result = bll.RemoveAllQuotesAndTrades();
        //    if (result)
        //    {
        //        List<QuotesAndTrades> lst = bll.GetQuotesAndTrades();

        //        ApplicationHub.RefreshQuotesAndTrade(lst);

        //        ShowMessage("Message", "All quotes and trades deleted successfully");
        //        LogActivity("Delete All QuotesAndTrades", "Remove all the quotes and trades", string.Empty);
        //    }
        //    else
        //    {
        //        LogActivity("Delete All QuotesAndTrades(Unsuccessfull)", "Remove all the quotes and trades", string.Empty);
        //        lblRemoveQuotesMessage.Text = "Unable to delete the quotes";
        //    }
        //}

        protected void grdUsers_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Unlock")
            {
                int id = Convert.ToInt32(e.CommandArgument);
                LoginBLL bll = new LoginBLL();
                bll.Unlock(id);
                BindAccounts();
                LogActivity("Unlock user", "User has been unlocked", string.Empty);
            }
        }

        protected void grdAccounts_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int id = Convert.ToInt32(e.CommandArgument);
            LoginBLL bll = new LoginBLL();
            if (e.CommandName == "Reset")
            {
                string password = string.Empty;
                do
                {
                    password = System.Web.Security.Membership.GeneratePassword(10, 3);
                    if (bll.IsValidPasword(password))
                    {
                        break;
                    }
                } while (true);
                bll.ResetPassword(id, password);
                LogActivity("Password Reset", "User password has been reset", string.Empty);
            }
            else if (e.CommandName == "Remove")
            {
                bll.RemoveAccount(id);
                LogActivity("User Deleted", "User has been deleted", string.Empty);
            }
            else if (e.CommandName == "ChangeRole")
            {
                RadGrid gvr = (RadGrid)((Control)e.CommandSource).NamingContainer;
                ListItem item = (gvr.FindControl("ddlChangeRole") as DropDownList).SelectedItem;
                bll.SetRole(id, item.Text);
            }

            BindAccounts();
        }

        //protected void btnClear_Click(object sender, EventArgs e)
        //{
        //    txtName.Text = string.Empty;
        //    txtPassword.Text = string.Empty;
        //    LogActivity("Clear User Detail", "User detail has been cleared", string.Empty);
        //}

        //protected void btnSave_Click(object sender, EventArgs e)
        //{
        //    LoginBLL bll = new LoginBLL();

        //    if (bll.IsValidPasword(txtPassword.Text))
        //    {
        //        DAL.Login model = new DAL.Login();

        //        model.CreatedDate = DateTime.Now;
        //        model.FailedAttempts = 0;
        //        model.IsLocked = false;
        //        model.LastPasswordReset = DateTime.Now;
        //        model.Name = txtName.Text;
        //        model.Password = txtPassword.Text;
        //        model.Role = AppConstants.Roles.Normal.ToString();
        //        model.Status = 1;
        //        model.Email = txtCreatUserEmail.Text;
        //        model = bll.NewAccount(model);

        //        lblError.Visible = false;

        //        if (model == null)
        //        {
        //            ShowMessage("Error", "User with the same name already exist");
        //            return;
        //        }

        //        BindAccounts();

        //        LogActivity("User Created", "New user has been created", string.Empty);
        //    }
        //    else
        //    {
        //        lblError.Visible = true;
        //        lblError.Text = "Password must be 8 to 15 characters long which contain at least one lowercase letter, one uppercase letter, one numeric digit, and one special character";
        //    }
        //}

        //protected void btnSaveEmail_Click(object sender, EventArgs e)
        //{
        //    EmailGroup model = new EmailGroup();
        //    if (!string.IsNullOrEmpty(hfEmailGroupId.Value))
        //    {
        //        model.ID = Convert.ToInt32(hfEmailGroupId.Value);
        //        LogActivity("Email Updated", "Email has been updated", string.Empty);
        //    }
        //    else
        //    {
        //        LogActivity("Email Created", "Email has been created", string.Empty);
        //    }
        //    model.GroupName = ddlGroups.SelectedItem.Text;
        //    model.Name = txtEmail.Text;

        //    EmailGroupsBL bl = new EmailGroupsBL();
        //    bl.Save(model);
        //    BindEmails();
        //    hfEmailGroupId.Value = string.Empty;
        //}

        //protected void grdEmail_RowCommand(object sender, GridViewCommandEventArgs e)
        //{
        //    EmailGroupsBL bll = new EmailGroupsBL();
        //    if (e.CommandName == "Remove")
        //    {
        //        int id = Convert.ToInt32(e.CommandArgument);
        //        bll.Delete(id);
        //        BindEmails();
        //        LogActivity("Email Removed", "Email has been removed", string.Empty);
        //    }
        //    else if (e.CommandName == "EditRow")
        //    {
        //        int id = Convert.ToInt32(e.CommandArgument);
        //        EmailGroup model = bll.GetById(id);
        //        txtEmail.Text = model.Name;
        //        ddlGroups.ClearSelection();
        //        ddlGroups.FindItemByText(model.GroupName).Selected = true;
        //        hfEmailGroupId.Value = model.ID.ToString();
        //    }
        //    else if (e.CommandName == "SendEmail")
        //    {
        //        ReportHelper.GenerateAndSendPDFReport(e.CommandArgument.ToString());
        //        ShowMessage("Message", "Emails has been set to all the addresses in the group(" + e.CommandArgument.ToString() + ")");
        //    }
        //}

        //protected void btnClearEmail_Click(object sender, EventArgs e)
        //{
        //    txtEmail.Text = string.Empty;
        //    hfEmailGroupId.Value = string.Empty;
        //}

        protected void ddlGroupReceiver_SelectedIndexChanged(object sender, EventArgs e)
        {
            GroupBL bl = new GroupBL();
            bl.SetEmailReceiver(Convert.ToInt32(ddlGroupReceiver.SelectedValue));
        }

        protected void grdAccounts_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DropDownList ddlChangeRole = e.Row.FindControl("ddlChangeRole") as DropDownList;

                DAL.Login model = e.Row.DataItem as DAL.Login;

                ddlChangeRole.DataSource = Enum.GetNames(typeof(AppConstants.Roles));
                ddlChangeRole.DataBind();
                ddlChangeRole.Enabled = true;
                // Patch will be removed later
                ListItem item;
                if (model.Role == "Registered" || model.Role == "Register")
                {
                    item = ddlChangeRole.Items.FindByText(AppConstants.Roles.Normal.ToString());
                }
                else
                {
                    item = ddlChangeRole.Items.FindByText(model.Role);
                }

                ddlChangeRole.ClearSelection();
                item.Selected = true;
            }
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            txtName.Text = string.Empty;
            txtPassword.Text = string.Empty;
            LogActivity("Clear User Detail", "User detail has been cleared", string.Empty);
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            LoginBLL bll = new LoginBLL();

            if (bll.IsValidPasword(txtPassword.Text))
            {
                DAL.Login model = new DAL.Login();

                model.CreatedDate = DateTime.Now;
                model.FailedAttempts = 0;
                model.IsLocked = false;
                model.LastPasswordReset = DateTime.Now;
                model.Name = txtName.Text;
                model.Password = txtPassword.Text;
                model.Role = AppConstants.Roles.Normal.ToString();
                model.Status = 1;
                model.Email = txtCreatUserEmail.Text;
                model = bll.NewAccount(model);

                lblError.Visible = false;

                if (model == null)
                {
                    ShowMessage("Error", "User with the same name already exist");
                    return;
                }

                BindAccounts();

                LogActivity("User Created", "New user has been created", string.Empty);
            }
            else
            {
                lblError.Visible = true;
                lblError.Text = "Password must be 8 to 15 characters long which contain at least one lowercase letter, one uppercase letter, one numeric digit, and one special character";
            }
        }

        protected void grdAccounts_ItemCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {
            //int id = Convert.ToInt32(e.CommandArgument);

            LoginBLL bll = new LoginBLL();
            if (e.CommandName == "Reset")
            {
                int id = Convert.ToInt32(e.CommandArgument);
                string password = string.Empty;
                do
                {
                    password = System.Web.Security.Membership.GeneratePassword(10, 3);
                    if (bll.IsValidPasword(password))
                    {
                        break;
                    }
                } while (true);
                bll.ResetPassword(id, password);
                LogActivity("Password Reset", "User password has been reset", string.Empty);
            }
            else if (e.CommandName == "Remove")
            {
                int id = Convert.ToInt32(e.CommandArgument);
                bll.RemoveAccount(id);
                LogActivity("User Deleted", "User has been deleted", string.Empty);
            }
            else if (e.CommandName == "ChangeRole")
            {
                if (e.Item is GridDataItem)
                {
                    int id = Convert.ToInt32(e.CommandArgument);
                    GridDataItem dataItem = (GridDataItem)e.Item;
                    //LinkButton btn = (LinkButton)dataItem.FindControl("LinkButton1");
                    //btn.BackColor = System.Drawing.Color.Red;
                    DropDownListItem item = (dataItem.FindControl("ddlChangeRole") as RadDropDownList).SelectedItem;
                    bll.SetRole(id, item.Text);
                }

                //gvr = (RadGrid)((Control)e.CommandSource).NamingContainer;
                //DropDownListItem item = (gvr.FindControl("ddlChangeRole") as RadDropDownList).SelectedItem;
                //bll.SetRole(id, item.Text);
            }
            //else if (e.CommandName == "LogOff")
            //{
            //    bll.LogOffAccount(Convert.ToInt32(e.CommandArgument));
            //    LogActivity("User LogOff", "User has been log offed", string.Empty);
            //}
            BindAccounts();
        }

        protected void grdAccounts_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
        {
            if (e.Item.ItemType == Telerik.Web.UI.GridItemType.AlternatingItem || e.Item.ItemType == Telerik.Web.UI.GridItemType.Item)
            {
                RadDropDownList ddlChangeRole = e.Item.FindControl("ddlChangeRole") as RadDropDownList;

                DAL.Login model = e.Item.DataItem as DAL.Login;

                ddlChangeRole.DataSource = Enum.GetNames(typeof(AppConstants.Roles));
                ddlChangeRole.DataBind();
                // Patch will be removed later
                DropDownListItem item;
                if (model.Role == "Registered" || model.Role == "Register")
                {
                    item = ddlChangeRole.FindItemByText(AppConstants.Roles.Normal.ToString());
                }
                else
                {
                    item = ddlChangeRole.FindItemByText(model.Role.ToString());
                }

                ddlChangeRole.ClearSelection();
                item.Selected = true;
            }
        }

        protected void btnClearEmail_Click(object sender, EventArgs e)
        {
            txtEmail.Text = string.Empty;
            hfEmailGroupId.Value = string.Empty;
        }

        protected void btnSaveEmail_Click(object sender, EventArgs e)
        {
            EmailGroup model = new EmailGroup();
            if (!string.IsNullOrEmpty(hfEmailGroupId.Value))
            {
                model.ID = Convert.ToInt32(hfEmailGroupId.Value);
                LogActivity("Email Updated", "Email has been updated", string.Empty);
            }
            else
            {
                LogActivity("Email Created", "Email has been created", string.Empty);
            }
            model.GroupName = ddlGroups.SelectedItem.Text;
            model.Name = txtEmail.Text;

            EmailGroupsBL bl = new EmailGroupsBL();
            bl.Save(model);
            BindEmails();
            hfEmailGroupId.Value = string.Empty;
        }
        protected void grdEmail_ItemCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {
            EmailGroupsBL bll = new EmailGroupsBL();
            if (e.CommandName == "Remove")
            {
                int id = Convert.ToInt32(e.CommandArgument);
                bll.Delete(id);
                BindEmails();
                LogActivity("Email Removed", "Email has been removed", string.Empty);
            }
            else if (e.CommandName == "EditRow")
            {
                int id = Convert.ToInt32(e.CommandArgument);
                EmailGroup model = bll.GetById(id);
                txtEmail.Text = model.Name;
                ddlGroups.ClearSelection();
                // ddlGroups.FindItemByText(model.GroupName).Selected = true;
                ddlGroups.Items.FindByText(model.GroupName).Selected = true;
                hfEmailGroupId.Value = model.ID.ToString();
            }
            else if (e.CommandName == "SendEmail")
            {
                string str = ReportHelper.GenerateAndSendPDFReport(e.CommandArgument.ToString());
                if (str != string.Empty)
                {
                    lblEmailError.Visible = true;
                    lblEmailError.InnerText = str;
                }
                ShowMessage("Message", "Emails has been set to all the addresses in the group(" + e.CommandArgument.ToString() + ")");
            }
            BindEmails();
        }

        protected void grdAccounts_SortCommand(object sender, GridSortCommandEventArgs e)
        {
            if (!e.Item.OwnerTableView.SortExpressions.ContainsExpression(e.SortExpression))
            {
                GridSortExpression sortExpr = new GridSortExpression();
                sortExpr.FieldName = e.SortExpression;
                sortExpr.SortOrder = GridSortOrder.Ascending;

                e.Item.OwnerTableView.SortExpressions.AddSortExpression(sortExpr);
            }

        }

        protected void grdEmail_SortCommand(object sender, GridSortCommandEventArgs e)
        {
            if (!e.Item.OwnerTableView.SortExpressions.ContainsExpression(e.SortExpression))
            {
                GridSortExpression sortExpr = new GridSortExpression();
                sortExpr.FieldName = e.SortExpression;
                sortExpr.SortOrder = GridSortOrder.Ascending;

                e.Item.OwnerTableView.SortExpressions.AddSortExpression(sortExpr);
            }
        }
        protected void grdCounterParty_SortCommand(object sender, GridSortCommandEventArgs e)
        {
            if (!e.Item.OwnerTableView.SortExpressions.ContainsExpression(e.SortExpression))
            {
                GridSortExpression sortExpr = new GridSortExpression();
                sortExpr.FieldName = e.SortExpression;
                sortExpr.SortOrder = GridSortOrder.Ascending;

                e.Item.OwnerTableView.SortExpressions.AddSortExpression(sortExpr);
            }
        }
        protected void grdBorrower_SortCommand(object sender, GridSortCommandEventArgs e)
        {
            if (!e.Item.OwnerTableView.SortExpressions.ContainsExpression(e.SortExpression))
            {
                GridSortExpression sortExpr = new GridSortExpression();
                sortExpr.FieldName = e.SortExpression;
                sortExpr.SortOrder = GridSortOrder.Ascending;

                e.Item.OwnerTableView.SortExpressions.AddSortExpression(sortExpr);
            }
        }
        protected void grdUsers_ItemCommand(object sender, GridCommandEventArgs e)
        {
            if (e.CommandName == "Unlock")
            {

                if (e.Item is GridDataItem)
                {
                    int id = Convert.ToInt32(e.CommandArgument);
                    LoginBLL bll = new LoginBLL();
                    bll.Unlock(id);
                    BindAccounts();
                    LogActivity("Unlock user", "User has been unlocked", string.Empty);
                }
            }
            BindAccounts();
        }


        protected void grdUsers_SortCommand(object sender, GridSortCommandEventArgs e)
        {
            GridSortExpression sortExpr = new GridSortExpression();
            sortExpr.FieldName = e.SortExpression;
            sortExpr.SortOrder = GridSortOrder.Ascending;

            e.Item.OwnerTableView.SortExpressions.AddSortExpression(sortExpr);
        }

        protected void btnRemoveLoan_Click(object sender, EventArgs e)
        {


            LoansBLL bll = new LoansBLL();
            bool result = bll.RemoveAllLoans();
            if (result)
            {
                // all loans
                List<Loans> allLoans = bll.GetLoans();

                // check to see if the loan has been added or updated
                ApplicationHub.RefreshLoans(allLoans);

                ShowMessage("Message", "All loans deleted successfully");
                LogActivity("Delete All Loans", "Remove all the quotes", string.Empty);
            }
            else
            {
                LogActivity("Delete All Loans(Unsuccessfull)", "Remove all the quotes", string.Empty);
                lblRemoveLoanMessage.Text = "Unable to delete the loans";
            }
        }

        protected void btnRemoveQuotes_Click(object sender, EventArgs e)
        {
            QuotesAndTradesBLL bll = new QuotesAndTradesBLL();
            bool result = bll.RemoveAllQuotesAndTrades();
            if (result)
            {
                List<QuotesAndTrades> lst = bll.GetQuotesAndTrades();

                ApplicationHub.RefreshQuotesAndTrade(lst);

                ShowMessage("Message", "All quotes and trades deleted successfully");
                LogActivity("Delete All QuotesAndTrades", "Remove all the quotes and trades", string.Empty);
            }
            else
            {
                LogActivity("Delete All QuotesAndTrades(Unsuccessfull)", "Remove all the quotes and trades", string.Empty);
                lblRemoveQuotesMessage.Text = "Unable to delete the quotes";
            }
        }

        //protected void ddlGroupReceiver_SelectedIndexChanged(object sender, DropDownListEventArgs e)
        //{
        //    GroupBL bl = new GroupBL();
        //    bl.SetEmailReceiver(Convert.ToInt32(ddlGroupReceiver.SelectedValue));
        //}

        protected void btnSaveSettings_Click(object sender, EventArgs e)
        {
            Setting setting = new Setting();
            SettingsBLL settingBL = new SettingsBLL();
            if (txtYears.Text.ToString() != string.Empty)
            {
                setting.Name = "Loan Year Settings";
                setting.Value = txtYears.Text.Trim();
                setting.CreatedOn = DateTime.Now;
                settingBL.InsertUpdateSettings(setting);
                lblMessage.Text = "Entry inserted successfully";
                lblMessage.Visible = true;
                txtYears.Text = string.Empty;
                BindCurrentYear();
            }
            else
            {
                lblMessage.Text = "Please Enter Years";
                lblMessage.Visible = true;
            }
        }

        protected void btnCountrySave_Click(object sender, EventArgs e)
        {
            CountryBL countryBL = new CountryBL();
            if (txtCountryName.Text != "" || txtCountryCode.Text != "")
            {

                DAL.tblCountry country = new DAL.tblCountry();
                country.Name = txtCountryName.Text.Trim();
                country.Code = txtCountryCode.Text.Trim();

                if (!string.IsNullOrEmpty(hfCountryID.Value))
                {
                    country.ID = Convert.ToInt32(hfCountryID.Value);
                    LogActivity("Country Updated", "Country has been updated", string.Empty);
                }
                else
                {
                    LogActivity("Country Created", "Country has been created", string.Empty);
                }

                string result = countryBL.SaveCountry(country);

                lblCountryError.Visible = true;
                lblCountryError.Text = result;
                ShowMessage("Message", result);
                BindCountry();
                hfCountryID.Value = string.Empty;
                Clear();
            }
            else
            {
                lblCountryError.Visible = true;
                lblCountryError.Text = "Please Enter The Details First.";
            }
        }

        private void BindCountry()
        {
            CountryBL countryBL = new CountryBL();
            grdCountry.DataSource = countryBL.GetCountry().OrderBy(c => c.Name).ToList();
            grdCountry.DataBind();
        }
        protected void btnCountryClear_Click(object sender, EventArgs e)
        {
            Clear();
            lblCountryError.Visible = false;
        }
        private void Clear()
        {
            txtCountryName.Text = string.Empty;
            txtCountryCode.Text = string.Empty;
            lblCountryError.Text = string.Empty;
            LogActivity("Clear Country Detail", "Country detail has been cleared", string.Empty);
        }
        protected void grdCountry_ItemCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {
            CountryBL countryBL = new CountryBL();
            if (e.CommandName == "RemoveCountry")
            {
                int id = Convert.ToInt32(e.CommandArgument);
                countryBL.Delete(id);

                LogActivity("Country Removed", "Country has been removed", string.Empty);

            }
            else if (e.CommandName == "EditCountry")
            {
                int id = Convert.ToInt32(e.CommandArgument);
                tblCountry country = countryBL.GetByID(id);
                txtCountryName.Text = country.Name;
                txtCountryCode.Text = country.Code;

                hfCountryID.Value = country.ID.ToString();
            }
            BindCountry();
        }

        protected void grdCountry_SortCommand(object sender, GridSortCommandEventArgs e)
        {
            if (!e.Item.OwnerTableView.SortExpressions.ContainsExpression(e.SortExpression))
            {
                GridSortExpression sortExpr = new GridSortExpression();
                sortExpr.FieldName = e.SortExpression;
                sortExpr.SortOrder = GridSortOrder.Ascending;

                e.Item.OwnerTableView.SortExpressions.AddSortExpression(sortExpr);
            }
        }

        #region Duplicate Loans

        private void BindDuplicateLoansTab(List<DuplicateLoan> lst = null)
        {
            if (lst == null)
            {
                LoansBLL bll = new LoansBLL();
                lst = bll.GetDuplicateLoans();
            }

            grdDuplicateLoans.DataSource = lst;
            grdDuplicateLoans.DataBind();
        }
        private static string strLoanSort = "ASC";
        private static string strDuplicateLoanSort = "ASC";

        protected void grdDuplicateLoans_SortCommand(object sender, GridSortCommandEventArgs e)
        {
            // GridTableView tableView = e.Item.OwnerTableView;
            GridSortExpression sortExpr = new GridSortExpression();
            sortExpr.FieldName = e.SortExpression;

            if (strDuplicateLoanSort == "ASC")
            {
                sortExpr.SortOrder = GridSortOrder.Descending;
                strDuplicateLoanSort = "DESC";
            }
            else
            {
                sortExpr.SortOrder = GridSortOrder.Ascending;
                strDuplicateLoanSort = "ASC";
            }
            // sortExpr.SortOrder = GridSortOrder.Ascending;
            e.Item.OwnerTableView.SortExpressions.AddSortExpression(sortExpr);
            //sortExpr.SortOrder = GridSortOrder.Ascending;

        }
        #endregion

        protected void grdDuplicateLoans_ItemCommand(object sender, GridCommandEventArgs e)
        {
            BindDuplicateLoansTab();
        }

        protected void grdLockedAcc_ItemCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {
            LoginBLL bll = new LoginBLL();
            if (e.CommandName == "LogOff")
            {
                bll.LogOffAccount(Convert.ToInt32(e.CommandArgument));
                LogActivity("User LogOff", "User has been log offed", string.Empty);
            }
            BindAccounts();
        }

        protected void grdLockedAcc_SortCommand(object sender, GridSortCommandEventArgs e)
        {
            if (!e.Item.OwnerTableView.SortExpressions.ContainsExpression(e.SortExpression))
            {
                GridSortExpression sortExpr = new GridSortExpression();
                sortExpr.FieldName = e.SortExpression;
                sortExpr.SortOrder = GridSortOrder.Ascending;

                e.Item.OwnerTableView.SortExpressions.AddSortExpression(sortExpr);
            }

        }
        protected void btnCPClear_Click(object sender, EventArgs e)
        {
            txtCPName.Text = string.Empty;
            txtCPRegion.SelectedIndex = 0;
            txtCPType.SelectedValue = "Asset Manager";
            hfCPID.Value = string.Empty;
        }
        private void BindCounterParties()
        {
            CounterPartyBL bl = new CounterPartyBL();
            grdCounterParty.DataSource = bl.GetCounterParty().OrderBy(c => c.Name).ToList();
            grdCounterParty.DataBind();
        }


        protected void btnCPSave_Click(object sender, EventArgs e)
        {
            CounterParty model = new CounterParty();
            CounterPartyBL bl = new CounterPartyBL();

            model.Type = txtCPType.SelectedValue.Trim();
            model.Name = txtCPName.Text.Trim();
            model.Region = txtCPRegion.SelectedValue;
            if (!string.IsNullOrEmpty(hfCPID.Value))
            {
                model.ID = Convert.ToInt32(hfCPID.Value);
                LogActivity("CounterParty Updated", "Email has been updated", string.Empty);
            }
            else
            {
                LogActivity("CounterParty Created", "Email has been created", string.Empty);
            }
            string str = bl.SavecounterParty(model);
            BindCounterParties();
            if (str != "Entry of the same Name is already exists.")
            {
                hfCPID.Value = string.Empty;
            }

            RadWindowManager1.RadAlert(str, 330, 180, "realedge associates", "alertCallBackFn");

        }
        protected void grdCounterParty_ItemCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {
            CounterPartyBL bll = new CounterPartyBL();
            if (e.CommandName == "RemoveCP")
            {
                int id = Convert.ToInt32(e.CommandArgument);
                bll.Delete(id);
                BindCounterParties();
                LogActivity("CounterParty Removed", "CounterParty has been removed", string.Empty);
            }
            else if (e.CommandName == "EditCP")
            {
                int id = Convert.ToInt32(e.CommandArgument);
                CounterParty model = bll.GetByID(id);
                txtCPName.Text = model.Name;
                BindRegions();
                txtCPRegion.SelectedValue = model.Region;
                txtCPType.SelectedValue = model.Type;
                hfCPID.Value = model.ID.ToString();
            }
            //else if (e.CommandName == "SendEmail")
            //{
            //    string str = ReportHelper.GenerateAndSendPDFReport(e.CommandArgument.ToString());
            //    if (str != string.Empty)
            //    {
            //        lblEmailError.Visible = true;
            //        lblEmailError.InnerText = str;
            //    }
            //    ShowMessage("Message", "Emails has been set to all the addresses in the group(" + e.CommandArgument.ToString() + ")");
            //}
            BindCounterParties();
        }

        #region Import / Export

        protected void btnExport_Click(object sender, EventArgs e)
        {
            if (ddlImportExport.SelectedValue == "Export")
            {

                ExportToCSV();

            }
            else if (ddlImportExport.SelectedValue == "Import")
            {

                Import();


            }
        }

        protected void ExportToCSV()
        {
            string csv_file_path = Server.MapPath("~/Temp/tempfile1.csv");
            if (ddlImportType.SelectedValue == "Loan")
            {
                LoansBLL bll = new LoansBLL();

                var loanList = bll.GetLoans().Select(x => new { LoanName = x.CodeName, Borrower = x.Borrower, Country = x.Country, Sector = x.Sector, SigingDate = x.Signing_Date, MaturityDate = x.Maturity_Date, FixedOrFloating = x.FixedOrFloating, Margin = x.Margin, Currency = x.Currency, CouponFrequency = x.CouponFrequency, FacilitySize = x.FacilitySize, Bilateral = x.Bilateral, Amortizing = x.Amortizing, CouponDate = x.CouponDate, Notional = x.Notional, AmortisationsStartPoint = x.AmortisationsStartPoint, NoOfAmortisationPoint = x.NoOfAmortisationPoint, StructureID = x.StructureID, PP = x.PP, FixedFloating = x.Fixed_Floating, CreditRatingModys = x.CreditRatingModys, CreditRatingSPs = x.CreditRatingSPs, CreditRatingFitch = x.CreditRatingFitch, CreditRatingING = x.CreditRatingING, Gurantor = x.Gurantor, Grid = x.Grid, SummitCreditEntity = x.SummitCreditEntity });
                List<LoanTable> loanTable = new List<LoanTable>();
                foreach (var item in loanList)
                {
                    loanTable.Add(new LoanTable(item.LoanName, item.Borrower, item.Country, Convert.ToDateTime(item.SigingDate), Convert.ToDateTime(item.MaturityDate), item.FixedOrFloating, item.Margin, item.Currency, item.CouponFrequency, item.FacilitySize, Convert.ToBoolean(item.Bilateral), item.Amortizing, Convert.ToDateTime(item.CouponDate), item.Notional, item.AmortisationsStartPoint, Convert.ToInt16(item.NoOfAmortisationPoint), item.StructureID, item.PP, item.FixedFloating, item.CreditRatingModys, item.CreditRatingSPs, item.CreditRatingFitch, item.CreditRatingING, item.Gurantor, item.Grid, item.SummitCreditEntity));
                }

                BLL.CsvExport<LoanTable> exportHelper = new CsvExport<LoanTable>(loanTable);

                exportHelper.ExportToFile(csv_file_path);

                LogActivity("Loans Exported", "Export the Loans", string.Empty);
            }
            else if (ddlImportType.SelectedValue == "Quotes")
            {
                QuotesAndTradesBLL bll = new QuotesAndTradesBLL();

                var quotesList = bll.GetQuotesAndTrades().Select(x => new { LoanName = x.LoanName, TimeStamp = x.TimeStamp, CounterParty = x.CounterParty, BidPrice = x.BidPrice, OfferPrice = x.OfferPrice, BidSpread = x.BidSpread, OfferSpread = x.OfferSpread, Traded = x.Traded, MarketValue = x.MarketValue, Country = x.Country, AverageLife = x.AverageLife, AvgLifeDisc = x.AvgLifeDisc, AvgLifeRiskDisc = x.AvgLifeRiskDisc, AvgLifeNonDisc = x.AvgLifeNonDisc, SettlementDate = x.SettlementDate, Margin = x.Margin });
                List<HistoricalQuote> quotes = new List<HistoricalQuote>();

                foreach (var item in quotesList)
                {
                    quotes.Add(new HistoricalQuote(item.LoanName, Convert.ToDateTime(item.TimeStamp.Value), item.CounterParty, Convert.ToDecimal(item.BidPrice), Convert.ToDecimal(item.OfferPrice), Convert.ToDecimal(item.BidSpread), Convert.ToDecimal(item.OfferSpread), Convert.ToBoolean(item.Traded), Convert.ToDecimal(item.MarketValue), item.Country, Convert.ToDecimal(item.AverageLife), Convert.ToDecimal(item.AvgLifeDisc), Convert.ToDecimal(item.AvgLifeRiskDisc), Convert.ToDecimal(item.AvgLifeNonDisc), Convert.ToDateTime(item.SettlementDate), item.Margin));
                }
                BLL.CsvExport<HistoricalQuote> exportHelper = new CsvExport<HistoricalQuote>(quotes);

                exportHelper.ExportToFile(csv_file_path);

                LogActivity("QuotesAndTrades Exported", "Export the quotesAndtrades", string.Empty);
            }
            else if (ddlImportType.SelectedValue == "EUR Curve")
            {
                EURCurvesBL bll = new EURCurvesBL();
                var list = bll.GetEURCurve().Select(x => new { UploadDate = x.UploadDate, SummitGenDate = x.SummitGenDate, CCY = x.CCY, Index = x.CurveIndex, Days = x.Days, Rate = x.Rate, DiscFreq = x.DiscFreq });

                List<EURCurveTable> eurTable = new List<EURCurveTable>();
                foreach (var item in list)
                {
                    eurTable.Add(new EURCurveTable(Convert.ToDateTime(item.UploadDate), Convert.ToDateTime(item.SummitGenDate), item.CCY, item.Index, Convert.ToInt16(item.Days), Convert.ToDecimal(item.Rate), Convert.ToDecimal(item.DiscFreq)));
                }
                BLL.CsvExport<EURCurveTable> exportHelper = new CsvExport<EURCurveTable>(eurTable);
                exportHelper.ExportToFile(csv_file_path);
                LogActivity("EURCurve Exported", "Export the EURCurve", string.Empty);
            }
            else if (ddlImportType.SelectedValue == "US Curve")
            {
                USDCurveBL bll = new USDCurveBL();
                var list = bll.GetUSCurve().Select(x => new { UploadDate = x.UploadDate, SummitGenDate = x.SummitGenDate, CCY = x.CCY, Index = x.CurveIndex, Days = x.Days, Rate = x.Rate, DiscFreq = x.DiscFreq });

                List<USDCurveTable> eurTable = new List<USDCurveTable>();
                foreach (var item in list)
                {
                    eurTable.Add(new USDCurveTable(Convert.ToDateTime(item.UploadDate), Convert.ToDateTime(item.SummitGenDate), item.CCY, item.Index, Convert.ToInt16(item.Days), Convert.ToDecimal(item.Rate), Convert.ToDecimal(item.DiscFreq)));
                }
                BLL.CsvExport<USDCurveTable> exportHelper = new CsvExport<USDCurveTable>(eurTable);
                exportHelper.ExportToFile(csv_file_path);
                LogActivity("USCurve Exported", "Export the USCurve", string.Empty);
            }

            FileInfo file = new FileInfo(csv_file_path);

            if (file.Exists)
            {
                ShowMessage("Message", "Data has been exported successfully");
                Response.Clear();
                Response.ClearHeaders();
                Response.ClearContent();
                Response.AddHeader("Content-Disposition", "attachment; filename=" + file.Name);
                Response.AddHeader("Content-Length", file.Length.ToString());
                Response.ContentType = "text/plain";
                Response.Flush();
                Response.TransmitFile(file.FullName);
                Response.End();
            }

        }

        protected void Import()
        {
            try
            {
                string filename = Path.GetFileName(fuFile.FileName);
                string csv_file_path = Server.MapPath("~/Temp/tempfile.csv");
                //fuFile.SaveAs(csv_file_path);
                DataTable csvData = null;
                List<string> columnNames = null;
                if (ddlImportType.SelectedValue != "EUR Curve" || ddlImportType.SelectedValue != "US Curve")
                {
                    csvData = CSVHelper.GetDataTabletFromCSVFile(csv_file_path, GetDelimeters());
                    // Get Header column names list
                    columnNames = csvData.Columns.Cast<DataColumn>().Select(c => c.ColumnName).ToList();

                    LogActivity("File Uploaded", "Upload the file to import loan/quotesAndtrades", string.Empty);
                }


                if (ddlImportType.SelectedValue == "Loan")
                {
                    //ExtractLoanList(csvData, columnNames);
                    List<Loans> importedList = ExtractLoanList(csvData, columnNames);
                    //   ExtractLoanList(csvData, columnNames);
                    // Save it to database

                    ApplicationHub.RefreshLoans(importedList);
                    LogActivity("Loans Imported", "Import the loans", string.Empty);
                }
                else if (ddlImportType.SelectedValue == "Quotes")
                {
                    List<QuotesAndTrades> importedList = ExtractQuotesAndTradesList(csvData, columnNames);

                    // Save it to database
                    if (importedList.Count > 0)
                    {
                        QuotesAndTradesBLL bll = new QuotesAndTradesBLL();
                        bll.AddImportedQuotesAndTrades(importedList);
                        ApplicationHub.RefreshQuotesAndTrade(importedList);
                        LogActivity("QuotesAndTrades Imported", "Import the quotesAndtrades", string.Empty);
                    }
                    else
                    {
                        RadWindowManager1.RadAlert("No Data found", 330, 180, "realedge associates", "alertCallBackFn");
                        return;
                    }


                }
                else if (ddlImportType.SelectedValue == "Counterparties")
                {
                    List<CounterParty> importedList = ExtractCounterPartyList(csvData, columnNames);
                    if (importedList.Count > 0)
                    {
                        CounterPartyBL bll = new CounterPartyBL();
                        bll.AddImportedQuotesAndTrades(importedList);

                        LogActivity("Counterparties Imported", "Counterparties", string.Empty);
                    }
                    else
                    {
                        RadWindowManager1.RadAlert("No Data found", 330, 180, "realedge associates", "alertCallBackFn");
                        return;
                    }
                }
                else if (ddlImportType.SelectedValue == "EUR Curve")
                {
                    //importdatafromexcel(csv_file_path);
                    List<EURCurve> importedList = ExtractEURCurveList(csvData, columnNames);
                    if (importedList.Count > 0)
                    {
                        EURCurvesBL bll = new EURCurvesBL();
                        bll.UpdateCurve();
                        bll.ImportEURCurves(importedList);

                        LogActivity("EURCurves Imported", "EURCurves", string.Empty);
                    }
                    else
                    {
                        RadWindowManager1.RadAlert("No Data found", 330, 180, "realedge associates", "alertCallBackFn");
                        return;
                    }
                }
                else if (ddlImportType.SelectedValue == "US Curve")
                {
                    List<USDCurve> importedList = ExtractUSDCurveList(csvData, columnNames);
                    if (importedList.Count > 0)
                    {
                        USDCurveBL bll = new USDCurveBL();
                        bll.UpdateCurve();
                        bll.ImportUSDCurves(importedList);

                        LogActivity("USDCurve Imported", "USDCurves", string.Empty);
                    }
                    else
                    {
                        RadWindowManager1.RadAlert("No Data found", 330, 180, "realedge associates", "alertCallBackFn");
                        return;
                    }
                }

                //ShowMessage("Message", "Data has been imported successfully");
                RadWindowManager1.RadAlert("Data has been imported successfully", 330, 180, "realedge associates", "alertCallBackFn");
            }
            catch (Exception ex)
            {
                LogActivity("File Upload(Unsuccessfull)", "Unable to upload the file", ex.Message);
                //lblImportStatus.Text = "Upload status: The file could not be uploaded. The following error occured: " + ex.Message;
                RadWindowManager1.RadAlert("Data import failed", 330, 180, "realedge associates", "alertCallBackFn");
            }
        }

        #region Excel Import

        public void importdatafromexcel(string excelfilepath)
        {
            //declare variables - edit these based on your particular situation

            // make sure your sheet name is correct, here sheet name is sheet1, so you can change your sheet name if have

            string myexceldataquery = "select * from [Sheet1$]";
            try
            {
                string excelConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + excelfilepath + ";Extended Properties=\"Excel 12.0 Xml;HDR=YES;IMEX=1\"";


                //series of commands to bulk copy data from the excel file into our sql table
                OleDbConnection oledbconn = new OleDbConnection(excelConnectionString);
                OleDbCommand oledbcmd = new OleDbCommand(myexceldataquery, oledbconn);
                oledbconn.Open();

                OleDbDataAdapter da = new OleDbDataAdapter(oledbcmd);
                DataSet ds = new DataSet();
                da.Fill(ds);

            }
            catch (Exception ex)
            {
                //handle exception
            }
        }


        #endregion


        private string[] GetDelimeters()
        {
            List<string> delimeters = new List<string>();
            delimeters.Add(",");
            if (ddlSeparator.SelectedValue == "Comma")
            {
                delimeters.Add(",");
            }
            if (ddlSeparator.SelectedValue == "Space")
            {
                delimeters.Add(" ");
            }
            if (ddlSeparator.SelectedValue == "Semicolon")
            {
                delimeters.Add(";");
            }
            if (ddlSeparator.SelectedValue == "Tab")
            {
                delimeters.Add("TabDelimited");
            }


            return delimeters.ToArray();
        }

        private static List<QuotesAndTrades> ExtractQuotesAndTradesList(DataTable csvData, List<string> columnNames)
        {
            List<QuotesAndTrades> importedList = new List<QuotesAndTrades>();
            foreach (DataRow row in csvData.Rows)
            {
                QuotesAndTrades model = new QuotesAndTrades();
                // Get All properties
                PropertyInfo[] properties = model.GetType().GetProperties();
                foreach (PropertyInfo pInfo in properties)
                {
                    if (columnNames.Contains(pInfo.Name))
                    {
                        object value = row[pInfo.Name];
                        TypeConverter typeConverter = TypeDescriptor.GetConverter(pInfo.PropertyType);
                        //object propValue = typeConverter.ConvertFromString(value.ToString());
                        object propValue = null;
                        if (typeConverter.GetType().FullName == "System.ComponentModel.NullableConverter")
                        {
                            if (value.ToString().Trim() == "Yes")
                            {
                                propValue = typeConverter.ConvertFromString(("True"));
                            }
                            else if (value != "0")
                            {
                                propValue = typeConverter.ConvertFromString((value.ToString()));
                            }
                            else
                            {
                                propValue = typeConverter.ConvertFromString(("False"));
                            }
                        }
                        else
                            propValue = typeConverter.ConvertFromString((value.ToString()));


                        // Set value on the property
                        pInfo.SetValue(model, propValue, null);
                    }
                }
                importedList.Add(model);
            }
            return importedList;
        }

        private List<Loans> ExtractLoanList(DataTable csvData, List<string> columnNames)
        {
            List<Loans> importedList = new List<Loans>();
            //  string strMessage = "";
            try
            {



                // Get All properties
                //PropertyInfo[] properties = model.GetType().GetProperties();
                //foreach (PropertyInfo pInfo in properties)
                //{
                //    if (columnNames.Contains(pInfo.Name))
                //    {
                //        object value = row[pInfo.Name];
                //        TypeConverter typeConverter = TypeDescriptor.GetConverter(pInfo.PropertyType);
                //        object propValue = null;
                //        if (typeConverter.GetType().FullName == "System.ComponentModel.NullableConverter")
                //        {
                //            if (value.ToString().Trim() == "1" || value.ToString().Trim() == "Yes")
                //            {
                //                propValue = typeConverter.ConvertFromString(("True"));
                //            }
                //        }
                //        else
                //            propValue = typeConverter.ConvertFromString((value.ToString()));





                //        // Set value on the property
                //        pInfo.SetValue(model, propValue, null);
                //    }

                foreach (DataRow dr in csvData.Rows)
                {
                    Loans model = new Loans();
                    model.CodeName = dr["CodeName"].ToString();

                 //   model.AmortisationsStartPoint = dr["AmortisationsStartPoint"].ToString();
                    model.Borrower = dr["Borrower"].ToString();
                    if (dr["Gurantor"] != null )
                    {
                        model.Gurantor = dr["Gurantor"].ToString();
                    }
                  
                    model.Country = dr["Country"].ToString();
                    model.Grid = dr["Grid"].ToString();
                    model.SummitCreditEntity = dr["SummitCreditEntity"].ToString();
                    model.CreditRatingModys = dr["CreditRatingModys"].ToString();
                    model.CreditRatingSPs = dr["CreditRatingSPs"].ToString();
                    model.CreditRatingFitch = dr["CreditRatingFitch"].ToString();
                    model.CreditRatingING = dr["CreditRatingING"].ToString();
                    model.StructureID = dr["StructureID"].ToString();
                    model.PP = dr["PP"].ToString();
                    model.Sector = dr["Sector"].ToString();
                    model.FacilitySize = dr["FacilitySize"].ToString();
                    model.Signing_Date = dr["Signing_Date"].ToString();
                    model.Maturity_Date = dr["Maturity_Date"].ToString();
                    model.CouponDate = dr["CouponDate"].ToString();
                    model.FixedOrFloating = dr["FixedOrFloating"].ToString();
                    model.Margin = dr["Margin"].ToString();
                    model.Notional = dr["Notional"].ToString();
                    model.Currency = dr["Currency"].ToString();
                    model.CouponFrequency = dr["CouponFrequency"].ToString();
                    if (dr["Bilateral"].ToString() == "Yes")
                        model.Bilateral = true;
                    else
                        model.Bilateral = false;


                    //model.Amortizing = dr["Amortizing"].ToString();
                    model.Amortizing = "Yes";
                    if (dr["AmortisationsStartPoint"].ToString() != string.Empty)
                    {
                        model.AmortisationsStartPoint = dr["AmortisationsStartPoint"].ToString();

                    }
                    else
                        model.AmortisationsStartPoint = dr["CouponDate"].ToString();
                    if (dr["NoOfAmortisationPoint"].ToString().Trim() != string.Empty)
                        model.NoOfAmortisationPoint = Convert.ToInt16(dr["NoOfAmortisationPoint"]);
                    else
                        model.NoOfAmortisationPoint = GetNoOfAmortisations();
                    importedList.Add(model);
                    LoansBLL bll = new LoansBLL();
                    string str = bll.AddImportedLoans(model);
                    if (str != "")
                    {
                        RadWindowManager1.RadAlert(str, 330, 180, "realedge associates", "alertCallBackFn");

                    }
                }

            }
            catch (Exception ex)
            {




            }
            return importedList;
        }
        public int GetNoOfAmortisations()
        {
            return 10;
        }
        private static List<CounterParty> ExtractCounterPartyList(DataTable csvData, List<string> columnNames)
        {
            List<CounterParty> importedList = new List<CounterParty>();
            foreach (DataRow row in csvData.Rows)
            {
                CounterParty model = new CounterParty();
                // Get All properties
                PropertyInfo[] properties = model.GetType().GetProperties();
                foreach (PropertyInfo pInfo in properties)
                {
                    if (columnNames.Contains(pInfo.Name))
                    {
                        object value = row[pInfo.Name];
                        TypeConverter typeConverter = TypeDescriptor.GetConverter(pInfo.PropertyType);
                        object propValue = null;
                        if (typeConverter.GetType().FullName == "System.ComponentModel.NullableConverter")
                        {
                            if (value.ToString().Trim() == "1")
                            {
                                propValue = typeConverter.ConvertFromString(("True"));
                            }
                        }
                        else
                            propValue = typeConverter.ConvertFromString((value.ToString()));





                        // Set value on the property
                        pInfo.SetValue(model, propValue, null);
                    }
                }
                importedList.Add(model);
            }
            return importedList;
        }

        public static List<EURCurve> ExtractEURCurveList(DataTable csvData, List<string> columnNames)
        {
            List<EURCurve> importedList = new List<EURCurve>();
            for (int i = 9; i < csvData.Rows.Count; i++)
            {
                try
                {
                    EURCurve model = new EURCurve();
                    model.UploadDate = DateTime.Now;
                    string strSummitText = csvData.Columns[0].ToString();
                    string[] strSplit = strSummitText.Replace(" ", "").Replace("Runat", " ").Split('/');
                    string strDay = strSplit[1].ToString();
                    string strMonth = strSplit[0].ToString();
                    string[] str = strSplit[2].ToString().Split(' ');
                    string strYear = str[0].ToString();
                    if (Convert.ToInt16(strYear) > 50)
                    {
                        strYear = "19" + strYear;
                    }
                    else if (strYear.Length == 1)
                    {
                        strYear = "200" + strYear;
                    }
                    else if (strYear.Length == 4)
                    {
                        strYear = strYear;
                    }
                    else
                        strYear = "20" + strYear;

                    DateTime summitGenDate = Convert.ToDateTime(strYear + "/" + strMonth + "/" + strDay);
                    model.SummitGenDate = summitGenDate;
                    model.CCY = csvData.Rows[3].ItemArray[0].ToString().Replace("Ccy:", "").Trim();
                    model.CurveIndex = csvData.Rows[3].ItemArray[2].ToString().Replace("Index:", "").Trim();
                    if (csvData.Rows[i].ItemArray[2].ToString() != string.Empty)
                        model.DiscFreq = Convert.ToDecimal(csvData.Rows[i].ItemArray[2].ToString());
                    if (csvData.Rows[i].ItemArray[1].ToString() != string.Empty)
                        model.Rate = Convert.ToDecimal(csvData.Rows[i].ItemArray[1].ToString());
                    if (csvData.Rows[i].ItemArray[0].ToString() != string.Empty)
                        model.RateDate = Convert.ToDateTime(csvData.Rows[i].ItemArray[0].ToString());
                    if (csvData.Rows[i].ItemArray[0].ToString().Trim() != string.Empty)
                    {
                        if (csvData.Rows[i].ItemArray[0].ToString().Contains('*'))
                        {
                        }
                        else
                        {
                            model.Days = (Convert.ToDateTime(csvData.Rows[i].ItemArray[0]) - summitGenDate).Days;
                        }
                    }
                    model.IsNew = true;
                    if ((csvData.Rows[i].ItemArray[0].ToString() != string.Empty && csvData.Rows[i].ItemArray[1].ToString() != string.Empty) && csvData.Rows[i].ItemArray[2].ToString() != string.Empty)
                    {
                        importedList.Add(model);
                    }

                }
                catch (Exception ex)
                {

                }
            }
            //foreach (DataRow row in csvData.Rows)
            //{
            //    EURCurve model = new EURCurve();
            //    // Get All properties
            //    //PropertyInfo[] properties = model.GetType().GetProperties();
            //    //foreach (PropertyInfo pInfo in properties)
            //    //{
            //    //    if (columnNames.Contains(pInfo.Name))
            //    //    {
            //    //        object value = row[pInfo.Name];
            //    //        TypeConverter typeConverter = TypeDescriptor.GetConverter(pInfo.PropertyType);
            //    //        object propValue = null;
            //    //        if (typeConverter.GetType().FullName == "System.ComponentModel.NullableConverter")
            //    //        {
            //    //            if (value.ToString().Trim() == "1")
            //    //            {
            //    //                propValue = typeConverter.ConvertFromString(("True"));
            //    //            }
            //    //        }
            //    //        else
            //    //            propValue = typeConverter.ConvertFromString((value.ToString()));





            //    //        // Set value on the property
            //    //        pInfo.SetValue(model, propValue, null);
            //    //    }
            //    //}
            //    model.UploadDate = DateTime.Now;
            //    string strSummitText = csvData.Columns[0].ToString();



            //}
            return importedList;
        }

        public static List<USDCurve> ExtractUSDCurveList(DataTable csvData, List<string> columnNames)
        {
            List<USDCurve> importedList = new List<USDCurve>();
            for (int i = 9; i < csvData.Rows.Count; i++)
            {
                try
                {
                    USDCurve model = new USDCurve();
                    model.UploadDate = DateTime.Now;
                    string strSummitText = csvData.Columns[0].ToString();
                    string[] strSplit = strSummitText.Replace(" ", "").Replace("Runat", " ").Split('/');
                    string strDay = strSplit[1].ToString();
                    string strMonth = strSplit[0].ToString();
                    string[] str = strSplit[2].ToString().Split(' ');
                    string strYear = str[0].ToString();
                    if (Convert.ToInt16(strYear) > 50)
                    {
                        strYear = "19" + strYear;
                    }
                    else if (strYear.Length == 1)
                    {
                        strYear = "200" + strYear;
                    }
                    else if (strYear.Length == 4)
                    {
                        strYear = strYear;
                    }
                    else
                        strYear = "20" + strYear;

                    DateTime summitGenDate = Convert.ToDateTime(strYear + "/" + strMonth + "/" + strDay);
                    model.SummitGenDate = summitGenDate;
                    model.CCY = csvData.Rows[3].ItemArray[0].ToString().Replace("Ccy:", "").Trim();
                    model.CurveIndex = csvData.Rows[3].ItemArray[2].ToString().Replace("Index:", "").Trim();
                    if (csvData.Rows[i].ItemArray[2].ToString() != string.Empty)
                        model.DiscFreq = Convert.ToDecimal(csvData.Rows[i].ItemArray[2].ToString());
                    if (csvData.Rows[i].ItemArray[0].ToString() != string.Empty)
                        model.RateDate = Convert.ToDateTime(csvData.Rows[i].ItemArray[0].ToString());
                    if (csvData.Rows[i].ItemArray[1].ToString() != string.Empty)
                        model.Rate = Convert.ToDecimal(csvData.Rows[i].ItemArray[1].ToString());
                    if (csvData.Rows[i].ItemArray[0].ToString().Trim() != string.Empty)
                    {
                        if (csvData.Rows[i].ItemArray[0].ToString().Contains('*'))
                        {
                        }
                        else
                        {
                            model.Days = (Convert.ToDateTime(csvData.Rows[i].ItemArray[0]) - summitGenDate).Days;
                        }
                    }
                    model.IsNew = true;
                    if ((csvData.Rows[i].ItemArray[0].ToString() != string.Empty && csvData.Rows[i].ItemArray[1].ToString() != string.Empty) && csvData.Rows[i].ItemArray[2].ToString() != string.Empty)
                    {
                        importedList.Add(model);
                    }

                }
                catch (Exception ex)
                {

                }

            } return importedList;
        }
        #endregion


        private void BindBorrowers()
        {
            BorrowersBL bl = new BorrowersBL();
            grdBorrowers.DataSource = bl.GetBorrowers().OrderBy(c => c.Name).ToList();
            grdBorrowers.DataBind();
        }


        protected void btnBorrowerSave_Click(object sender, EventArgs e)
        {
            Borrower model = new Borrower();
            BorrowersBL bl = new BorrowersBL();
            if (txtBorrowerType.Text.Trim() == string.Empty)
            {
                RadWindowManager1.RadAlert("Name is required", 330, 180, "realedge associates", "alertCallBackFn"); return;
            }
            else
            {
                model.Type = txtBorrowerType.Text.Trim();
                model.Name = txtBorrowerName.Text.Trim();
                model.Region = ddlBorrowerRegion.SelectedValue;
                model.Grid = txtGrid.Text.Trim();
                model.SummitCreditEntity = txtSummitCredit.Text.Trim();
                if (!string.IsNullOrEmpty(hdnBorrower.Value))
                {
                    model.ID = Convert.ToInt32(hdnBorrower.Value);
                    LogActivity("Borrower Updated", "Email has been updated", string.Empty);
                }
                else
                {
                    LogActivity("Borrower Created", "Email has been created", string.Empty);
                }

                string str = bl.SaveBorrower(model);
                BindBorrowers();
                if (str != "Entry of the same Name is already exists.")
                {
                    hdnBorrower.Value = string.Empty;
                }

                RadWindowManager1.RadAlert(str, 330, 180, "realedge associates", "alertCallBackFn");
            }
        }

        protected void btnBorrowerClear_Click(object sender, EventArgs e)
        {
            ClearBorrower();
        }

        protected void ClearBorrower()
        {
            txtBorrowerName.Text = string.Empty;
            ddlBorrowerRegion.SelectedIndex = 0;
            txtBorrowerType.Text = string.Empty;
            hdnBorrower.Value = string.Empty;
            txtGrid.Text = string.Empty;
            txtSummitCredit.Text = string.Empty;

        }

        protected void grdBorrower_ItemCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {
            BorrowersBL bll = new BorrowersBL();
            if (e.CommandName == "RemoveB")
            {
                int id = Convert.ToInt32(e.CommandArgument);
                bll.Delete(id);
                BindBorrowers();
                LogActivity("Borrower Removed", "Borrower has been removed", string.Empty);
            }
            else if (e.CommandName == "EditB")
            {
                int id = Convert.ToInt32(e.CommandArgument);
                Borrower model = bll.GetByID(id);
                txtBorrowerName.Text = model.Name;
                BindRegions();
                ddlBorrowerRegion.SelectedValue = model.Region;
                txtBorrowerType.Text = model.Type;
                txtGrid.Text = model.Grid;
                txtSummitCredit.Text = model.SummitCreditEntity;
                hdnBorrower.Value = model.ID.ToString();
            }

            BindBorrowers();
        }

        #region Manage Currencies

        private void BindCurrencies()
        {
            CurrenciesBL bl = new CurrenciesBL();
            grdCurrency.DataSource = bl.GetCurrency().OrderBy(c => c.Currancy).ToList();
            grdCurrency.DataBind();
        }


        private void ClearCurrencies()
        {
            txtCurrency.Text = string.Empty;
            hdnCurrency.Value = string.Empty;
        }

        protected void btnCurrencySave_Click(object sender, EventArgs e)
        {
            Currency model = new Currency();
            CurrenciesBL bl = new CurrenciesBL();
            if (txtCurrency.Text.Trim() == string.Empty)
            {
                RadWindowManager1.RadAlert("Name is required", 330, 180, "realedge associates", "alertCallBackFn"); return;
            }
            else
            {
                model.Currancy = txtCurrency.Text.Trim();
                if (!string.IsNullOrEmpty(hdnCurrency.Value))
                {
                    model.ID = Convert.ToInt32(hdnCurrency.Value);
                    LogActivity("Currency Updated", "Currency has been updated", string.Empty);
                }
                else
                {
                    LogActivity("Currency Created", "Currency has been created", string.Empty);
                }

                string str = bl.SaveCurrency(model);
                BindCurrencies();
                if (str != "Entry of the same Name is already exists.")
                {
                    hdnBorrower.Value = string.Empty;
                }

                RadWindowManager1.RadAlert(str, 330, 180, "realedge associates", "alertCallBackFn");
            }
        }

        protected void btnCurrencyClear_Click(object sender, EventArgs e)
        {
            ClearCurrencies();
        }

        protected void grdCurrency_ItemCommand(object sender, GridCommandEventArgs e)
        {
            CurrenciesBL bll = new CurrenciesBL();
            if (e.CommandName == "RemoveCurrency")
            {
                int id = Convert.ToInt32(e.CommandArgument);
                bll.Delete(id);
                BindCurrencies();
                LogActivity("Currency Removed", "Currency has been removed", string.Empty);
            }
            else if (e.CommandName == "EditCurrency")
            {
                int id = Convert.ToInt32(e.CommandArgument);
                Currency model = bll.GetByID(id);
                txtCurrency.Text = model.Currancy;
                hdnCurrency.Value = model.ID.ToString();

            }

            BindCurrencies();
        }

        protected void grdCurrency_SortCommand(object sender, GridSortCommandEventArgs e)
        {
            if (!e.Item.OwnerTableView.SortExpressions.ContainsExpression(e.SortExpression))
            {
                GridSortExpression sortExpr = new GridSortExpression();
                sortExpr.FieldName = e.SortExpression;
                sortExpr.SortOrder = GridSortOrder.Ascending;

                e.Item.OwnerTableView.SortExpressions.AddSortExpression(sortExpr);
            }
        }

        #endregion

        #region Manage Credit Ratings
        private void BindCreditRatings()
        {
            CreditRatingsBL bl = new CreditRatingsBL();
            grdCreditRatings.DataSource = bl.GetRatings().OrderBy(c => c.CreditAgencyID).ToList();
            grdCreditRatings.DataBind();
        }

        private void ClearCreditRatings()
        {
            ddlAgency.SelectedIndex = 0;
            txtRatings.Text = string.Empty;
            hdnCreditRatings.Value = string.Empty;
        }

        protected void btnSaveRating_Click(object sender, EventArgs e)
        {
            CreditRating model = new CreditRating();
            CreditRatingsBL bl = new CreditRatingsBL();
            if (txtRatings.Text.Trim() == string.Empty)
            {
                RadWindowManager1.RadAlert("Rating is required", 330, 180, "realedge associates", "alertCallBackFn"); return;
            }
            else
            {
                model.CreditAgencyID = Convert.ToInt16(ddlAgency.SelectedValue);
                model.Rating = txtRatings.Text.Trim();
                if (!string.IsNullOrEmpty(hdnCreditRatings.Value))
                {
                    model.ID = Convert.ToInt32(hdnCreditRatings.Value);
                    LogActivity("Rating Updated", "Currency has been updated", string.Empty);
                }
                else
                {
                    LogActivity("Rating Created", "Currency has been created", string.Empty);
                }

                string str = bl.SaveRating(model);
                BindCreditRatings();
                if (str != "Entry of the same Agency Name is already exists.")
                {
                    hdnCreditRatings.Value = string.Empty;
                }

                RadWindowManager1.RadAlert(str, 330, 180, "realedge associates", "alertCallBackFn");
            }
        }

        protected void btnClearRating_Click(object sender, EventArgs e)
        {
            ClearCreditRatings();
        }

        protected void grdCreditRatings_ItemCommand(object sender, GridCommandEventArgs e)
        {
            CreditRatingsBL bll = new CreditRatingsBL();
            if (e.CommandName == "RemoveCR")
            {
                int id = Convert.ToInt32(e.CommandArgument);
                bll.Delete(id);
                BindCreditRatings();
                LogActivity("Ratings Removed", "Currency has been removed", string.Empty);
            }
            else if (e.CommandName == "EditCR")
            {
                int id = Convert.ToInt32(e.CommandArgument);
                CreditRating model = bll.GetByID(id);
                //txtCreditAgency.Text = model.CreditAgency;
                ddlAgency.SelectedValue = model.CreditAgencyID.Value.ToString();
                txtRatings.Text = model.Rating;
                hdnCreditRatings.Value = model.ID.ToString();

            }

            BindCreditRatings();
        }

        protected void grdCreditRatings_SortCommand(object sender, GridSortCommandEventArgs e)
        {
            if (!e.Item.OwnerTableView.SortExpressions.ContainsExpression(e.SortExpression))
            {
                GridSortExpression sortExpr = new GridSortExpression();
                sortExpr.FieldName = e.SortExpression;
                sortExpr.SortOrder = GridSortOrder.Ascending;

                e.Item.OwnerTableView.SortExpressions.AddSortExpression(sortExpr);
            }
        }

        #endregion

        #region Manage Credit Agencies
        private void BindAgencyDropDown()
        {
            CreditAgencyBL bl = new CreditAgencyBL();
            ddlAgency.DataSource = bl.GetAgencies().OrderBy(c => c.CreditAgency1).ToList();
            ddlAgency.DataTextField = "CreditAgency1";
            ddlAgency.DataValueField = "ID";
            ddlAgency.DataBind();
        }

        private void BindCreditAgencies()
        {
            CreditAgencyBL bl = new CreditAgencyBL();
            grdAgency.DataSource = bl.GetAgencies().OrderBy(c => c.CreditAgency1).ToList();
            grdAgency.DataBind();
        }

        private void ClearCreditAgencies()
        {
            txtCreditAgency.Text = string.Empty;

            hdnCreditAgency.Value = string.Empty;
        }

        #endregion

        protected void btnClearAgency_Click(object sender, EventArgs e)
        {
            ClearCreditAgencies();
        }

        protected void btnSaveAgency_Click(object sender, EventArgs e)
        {
            CreditAgency model = new CreditAgency();
            CreditAgencyBL bl = new CreditAgencyBL();
            if (txtCreditAgency.Text.Trim() == string.Empty)
            {
                RadWindowManager1.RadAlert("Agency name is required", 330, 180, "realedge associates", "alertCallBackFn"); return;
            }

            else
            {
                model.CreditAgency1 = txtCreditAgency.Text.Trim();

                if (!string.IsNullOrEmpty(hdnCreditAgency.Value))
                {
                    model.ID = Convert.ToInt32(hdnCreditAgency.Value);
                    LogActivity("Agency Updated", "Currency has been updated", string.Empty);
                }
                else
                {
                    LogActivity("Agency Created", "Currency has been created", string.Empty);
                }

                string str = bl.SaveAgency(model);
                BindCreditAgencies();
                if (str != "Entry of the same Agency Name is already exists.")
                {
                    hdnCreditAgency.Value = string.Empty;
                }

                RadWindowManager1.RadAlert(str, 330, 180, "realedge associates", "alertCallBackFn");
            }
        }

        protected void grdAgency_ItemCommand(object sender, GridCommandEventArgs e)
        {
            CreditAgencyBL bll = new CreditAgencyBL();
            if (e.CommandName == "RemoveA")
            {
                int id = Convert.ToInt32(e.CommandArgument);
                bll.Delete(id);
                BindCreditRatings();
                LogActivity("Agency Removed", "Currency has been removed", string.Empty);
            }
            else if (e.CommandName == "EditA")
            {
                int id = Convert.ToInt32(e.CommandArgument);
                CreditAgency model = bll.GetByID(id);
                txtCreditAgency.Text = model.CreditAgency1;


                hdnCreditAgency.Value = model.ID.ToString();

            }

            BindCreditAgencies();
        }

        protected void grdAgency_SortCommand(object sender, GridSortCommandEventArgs e)
        {
            if (!e.Item.OwnerTableView.SortExpressions.ContainsExpression(e.SortExpression))
            {
                GridSortExpression sortExpr = new GridSortExpression();
                sortExpr.FieldName = e.SortExpression;
                sortExpr.SortOrder = GridSortOrder.Ascending;

                e.Item.OwnerTableView.SortExpressions.AddSortExpression(sortExpr);
            }
        }

        protected void grdCreditRatings_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if ((e.Item is GridDataItem))
            {
                int id = ((DAL.CreditRating)((e.Item).DataItem)).CreditAgencyID.Value;
                CreditAgencyBL agencyBL = new CreditAgencyBL();
                Label lbl = (Label)e.Item.FindControl("lblAgencyName");
                lbl.Text = agencyBL.GetByID(id).CreditAgency1;
            }
        }




    }
    public class LoanTable
    {
        public string CodeName { get; set; }
        public string Borrower { get; set; }
        public string Country { get; set; }
        public DateTime SigningDate { get; set; }
        public DateTime MaturityDate { get; set; }
        public string FixedOrFloating { get; set; }
        public string Currency { get; set; }
        public string CouponFrequency { get; set; }

        public string Margin { get; set; }
        public string FacilitySize { get; set; }
        public Boolean Bilateral { get; set; }
        public string Amortizing { get; set; }
        public DateTime CouponDate { get; set; }
        public string Notional { get; set; }
        public string AmortisationsStartPoint { get; set; }
        public int NoOfAmortisationPoint { get; set; }
        public string StructureID { get; set; }
        public string PP { get; set; }
        public string FixedFloting { get; set; }
        public string CreditRatingModys { get; set; }
        public string CreditRatingSPs { get; set; }
        public string CreditRatingFitch { get; set; }
        public string CreditRatingING { get; set; }
        public string Gurantor { get; set; }
        public string Grid { get; set; }
        public string SummitCreditEntity { get; set; }
        public LoanTable(string loanName, string borrower, string country, DateTime signingDate, DateTime maturityDate, string fixedOrFloating, string margin, string currency, string couponFrequency, string facilitySize, Boolean bilateral, string amortizing, DateTime couponDate, string notional, string amortisationsStartPoint, int noOfAmortisationPoint, string structureID, string pp, string fixedFloting, string creditRatingModys, string creditRatingSPs, string creditRatingFitch, string creditRatingING, string gurantor, string grid, string summitCredit)
        {
            this.CodeName = loanName;
            this.Borrower = borrower;
            this.Country = country;
            this.SigningDate = signingDate;
            this.MaturityDate = maturityDate;
            this.FixedOrFloating = fixedOrFloating;
            this.Currency = currency;
            this.CouponFrequency = couponFrequency;
            this.Margin = margin;
            this.FacilitySize = facilitySize;
            this.Bilateral = bilateral;
            this.Amortizing = amortizing;
            this.CouponDate = couponDate;
            this.Notional = notional;
            this.AmortisationsStartPoint = amortisationsStartPoint;
            this.NoOfAmortisationPoint = noOfAmortisationPoint;
            this.StructureID = structureID;
            this.PP = pp;
            this.FixedFloting = fixedFloting;
            this.CreditRatingModys = creditRatingModys;
            this.CreditRatingSPs = creditRatingSPs;
            this.CreditRatingFitch = creditRatingFitch;
            this.CreditRatingING = creditRatingING;
            this.Gurantor = gurantor;
            this.SummitCreditEntity = summitCredit;
            this.Grid = grid;
        }
    }

    public class HistoricalQuote
    {
        public string LoanName { get; set; }
        public DateTime TimeStamp { get; set; }
        public string CounterParty { get; set; }
        public decimal BidPrice { get; set; }
        public decimal OfferPrice { get; set; }
        public decimal BidSpread { get; set; }
        public decimal OfferSpread { get; set; }
        public Boolean Traded { get; set; }
        public decimal MarketValue { get; set; }
        public string Country { get; set; }
        public decimal AverageLife { get; set; }
        public decimal AvgLifeDisc { get; set; }
        public decimal AvgLifeRiskDisc { get; set; }
        public decimal AvgLifeNonDisc { get; set; }
        public DateTime SettlementDate { get; set; }
        public string Margin { get; set; }

        public HistoricalQuote(string loanName, DateTime timeStamp, string counterParty, decimal bidPrice, decimal offerPrice, decimal bidSpread, decimal offerSpread, bool isTraded, decimal marketValue, string country, decimal averageLife, decimal avgLifeDisc, decimal avgLifeRiskDisc, decimal avgLifeNonDisc, DateTime settlementDate, string margin)
        {
            this.LoanName = loanName;
            this.TimeStamp = timeStamp;
            this.CounterParty = counterParty;
            this.BidPrice = bidPrice;
            this.OfferPrice = offerPrice;
            this.BidSpread = bidSpread;
            this.OfferSpread = offerSpread;
            this.Traded = isTraded;
            this.MarketValue = marketValue;
            this.Country = country;
            this.AverageLife = averageLife;
            this.AvgLifeDisc = avgLifeDisc;
            this.AvgLifeRiskDisc = avgLifeRiskDisc;
            this.AvgLifeNonDisc = avgLifeNonDisc;
            this.SettlementDate = settlementDate;
            this.Margin = margin;


        }

    }

    public class USDCurveTable
    {
        public DateTime UploadDate { get; set; }
        public DateTime SummitGenDate { get; set; }
        public string CCY { get; set; }
        public string Index { get; set; }
        public int Days { get; set; }
        public decimal Rate { get; set; }
        public decimal DiscFreq { get; set; }

        public USDCurveTable(DateTime uploadDate, DateTime summitGenDate, string ccy, string index, int days, decimal rate, decimal discFreq)
        {
            this.UploadDate = uploadDate;
            this.SummitGenDate = summitGenDate;
            this.CCY = ccy;
            this.Index = index;
            this.Days = days;
            this.Rate = rate;
            this.DiscFreq = discFreq;
        }
    }


    public class EURCurveTable
    {
        public DateTime UploadDate { get; set; }
        public DateTime SummitGenDate { get; set; }
        public string CCY { get; set; }
        public string Index { get; set; }
        public int Days { get; set; }
        public decimal Rate { get; set; }
        public decimal DiscFreq { get; set; }

        public EURCurveTable(DateTime uploadDate, DateTime summitGenDate, string ccy, string index, int days, decimal rate, decimal discFreq)
        {
            this.UploadDate = uploadDate;
            this.SummitGenDate = summitGenDate;
            this.CCY = ccy;
            this.Index = index;
            this.Days = days;
            this.Rate = rate;
            this.DiscFreq = discFreq;
        }
    }
}