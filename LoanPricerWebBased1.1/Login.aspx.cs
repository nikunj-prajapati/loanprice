using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using BLL;
using LoanPricerWebBased.Helpers;

namespace LoanPricerWebBased
{
    public partial class Login : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                txtPassword.Text = "password";
            }
        }
        protected void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {



                LoginBLL bll = new LoginBLL();
                // check if the account is not locked
                bool? isLocked = bll.IsLocked(txtName.Text);
                if (isLocked == null)
                {
                    //lblErrorMesage.Text = "No user found with this Name (" + txtName.Text + ")";

                    RadWindowManager1.RadAlert("No user found with this Name (" + txtName.Text + ")", 330, 180, "realedge associates", "alertCallBackFn");

                    return;
                }
                else if (isLocked.Value)
                {
                    // lblErrorMesage.Text = "Your account has been blocked due to 3 failed login attempts. For unblock please contact administrator";

                    RadWindowManager1.RadAlert("Your account has been blocked due to 3 failed login attempts. For unblock please contact administrator", 330, 180, "realedge associates", "alertCallBackFn");

                    return;
                }

                // account is not locked
                DAL.Login model = bll.Authenticate(txtName.Text, txtPassword.Text);

                if (model != null)
                {
                    // Password will be expire after 60 days
                    if (model.LastPasswordReset.AddDays(60).Date <= DateTime.Now.Date)
                    {
                        // lblErrorMesage.Text = "Your password has been expired. Please contact administrator to reset your password";

                      
                        btnResetPassword_Click(null, null);
                        RadWindowManager1.RadAlert("Your password has been expired. We sent you new password on your email address", 330, 180, "realedge associates", "alertCallBackFn");
                        return;
                    }
                    //else if (model.IsLogin == true && model.Role != "Admin")
                    //{
                    //    RadWindowManager1.RadAlert("This account is already logged on.", 330, 180, "realedge associates", "alertCallBackFn");
                    //    return;
                    //}
                    else
                    {
                        LogActivity("Login Successfull", "User Loged in successfully", string.Empty);
                        bll.SetLogin(model, 1);
                        Session["LogedInUser"] = model;
                        Response.Redirect("Default.aspx");
                    }
                }
                else
                {
                    //   lblErrorMesage.Text = "Please check Name and Password. You account will be blocked after 3 unsuccessfully attempts";
                    LogActivity("Login Error", "Please check Name and Password. You account will be blocked after 3 unsuccessfully attempts", string.Empty);
                    RadWindowManager1.RadAlert("Please check Name and Password. You account will be blocked after 3 unsuccessfully attempts", 330, 180, "realedge associates", "alertCallBackFn");

                }

            }
            catch (Exception ex)
            {
                LogActivity("Login Error", ex.Message, string.Empty);
                RadWindowManager1.RadAlert(ex.Message, 330, 180, "realedge associates", "alertCallBackFn");

            }
        }

        protected void btnResetPassword_Click(object sender, EventArgs e)
        {
            try
            {
                LoginBLL bll = new LoginBLL();
                string password = string.Empty;
                do
                {
                    password = System.Web.Security.Membership.GeneratePassword(10, 3);
                    if (bll.IsValidPasword(password))
                    {
                        break;
                    }
                } while (true);

                string result = string.Empty;
                DAL.Login model = bll.ResetPasswordPublic(txtName.Text, password, out result);
                // Send email with this new password
                if (result == "Password reseted successfully")
                {
                    try
                    {
                        EmailHelper.SendEmail(model.Email, "Hello " + txtName.Text + "," + "Your new password is : " + password, "Password Reseted. <br/>Best Regards,<br/>Realedge Support");
                    }
                    catch (Exception ex)
                    {

                        RadWindowManager1.RadAlert(ex.Message + " :: " + ex.InnerException.Message, 330, 180, "realedge associates", "alertCallBackFn");
                    }

                }

                //lblErrorMesage.Text = result;
                result = result.ToString().Replace("'", "");

                RadWindowManager1.RadAlert(result, 330, 180, "realedge associates", "alertCallBackFn");

                LogActivity("Password Reset", "User password has been reset", string.Empty);
            }
            catch (Exception ex)
            {
                //lblErrorMesage.Text = ex.Message;
                RadWindowManager1.RadAlert(ex.Message, 330, 180, "realedge associates", "alertCallBackFn");
                LogActivity("Password Reset(failure)", "User password has not been reset", ex.ToString());
            }
        }
    }
}








