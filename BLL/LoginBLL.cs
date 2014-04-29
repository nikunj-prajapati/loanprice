using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAL;
using System.Data.Objects.DataClasses;
using System.Data;
using System.Text.RegularExpressions;

namespace BLL
{
    public class LoginBLL
    {
        public bool IsValidPasword(string password)
        {
            return Regex.Match(password, @"^(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[^a-zA-Z0-9])(?!.*\s).{8,15}$").Success;
        }

        /// <summary>
        /// Authenticate the user and log the failed attempts and locked the account if failed attemtps reach to 3
        /// </summary>
        /// <param name="name"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public Login Authenticate(string name, string password)
        {
            using (LoanPriceEntities context = new LoanPriceEntities())
            {
                Login login = context.Logins.FirstOrDefault(c => c.Name == name && c.Password == password);
                if (login == null)
                {
                    // Log failed attempt
                    Login log = context.Logins.FirstOrDefault(c => c.Name == name);
                    if (log != null)
                    {
                        log.FailedAttempts = log.FailedAttempts + 1;
                        // if there are 3 failed attempts then Lock the account
                        if (log.FailedAttempts == 3)
                        {
                            log.IsLocked = true;
                        }
                    }
                }
                else
                {
                    // reset failed attempt if login successfully
                    login.FailedAttempts = 0;
                }
                context.SaveChanges();
                return login;
            }
        }

        public List<Login> GetLockedAccounts()
        {
            using (LoanPriceEntities context = new LoanPriceEntities())
            {
                return context.Logins.Where(c => c.IsLocked).ToList();
            }
        }

        public Login NewAccount(Login model)
        {
            using (LoanPriceEntities context = new LoanPriceEntities())
            {
                // User with the same name should not exist in the system
                Login temp = context.Logins.FirstOrDefault(c => c.Name == model.Name);
                if (temp != null)
                {
                    // user with the same name exist
                    return null;
                }
                context.Logins.AddObject(model);
                context.SaveChanges();
                return model;
            }
        }

        public Login ResetPassword(int id, string newPassword)
        {
            using (LoanPriceEntities context = new LoanPriceEntities())
            {
                Login model = context.Logins.FirstOrDefault(c => c.ID == id);

                // Save password history
                PasswordHistory history = new PasswordHistory();
                history.ChangedDate = DateTime.Now;
                history.ChangedPassword = newPassword;
                history.Password = model.Password;
                history.UserId = model.ID;

                context.PasswordHistories.AddObject(history);

                model.Password = newPassword;

                context.SaveChanges();
                return model;
            }
        }

        public Login ResetPasswordPublic(string userName, string newPassword, out string message)
        {
            using (LoanPriceEntities context = new LoanPriceEntities())
            {
                Login model = context.Logins.FirstOrDefault(c => c.Name == userName);
                if (model == null)
                {
                    message = "User with this name doesn't exist";
                    return model;
                }
                else if (model.IsLocked)
                {
                    message = "User account is locked. We cann't reset password.";
                    return model;
                }
                // Save password history
                PasswordHistory history = new PasswordHistory();
                history.ChangedDate = DateTime.Now;
                history.ChangedPassword = newPassword;
                history.Password = model.Password;
                history.UserId = model.ID;
                 
                context.PasswordHistories.AddObject(history);

                model.Password = newPassword;
                model.LastPasswordReset = DateTime.Now;
                context.SaveChanges();
                message = "Password reseted successfully";
                return model;
            }
        }

        public bool RemoveAccount(int id)
        {
            using (LoanPriceEntities context = new LoanPriceEntities())
            {
                Login model = context.Logins.FirstOrDefault(c => c.ID == id);
                if (model != null)
                {
                    // Delete the Password policy of this account
                    List<PasswordHistory> passwordHistory = context.PasswordHistories.Where(c => c.UserId == model.ID).ToList();
                    foreach (var item in passwordHistory)
                    {
                        context.PasswordHistories.DeleteObject(item);
                    }
                    // Delete all activity of this user
                    List<tblActivityLog> userActivityList = context.tblActivityLogs.Where(c => c.UserID == model.ID).ToList();
                    foreach (var item in userActivityList)
                    {
                        context.tblActivityLogs.DeleteObject(item);
                    }

                    context.Logins.DeleteObject(model);
                    context.SaveChanges();
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public List<Login> GetAccounts()
        {
            using (LoanPriceEntities context = new LoanPriceEntities())
            {
                return context.Logins.Where(c => !c.IsLocked).ToList();
            }
        }

        public Login SetRole(int id, string role)
        {
            using (LoanPriceEntities context = new LoanPriceEntities())
            {
                Login login = context.Logins.FirstOrDefault(c => c.ID == id);
                if (login != null)
                {
                    login.Role = role;
                }
                context.SaveChanges();
                return login;
            }
        }

        public Login Unlock(int id)
        {
            using (LoanPriceEntities context = new LoanPriceEntities())
            {
                Login login = context.Logins.FirstOrDefault(c => c.ID == id);
                if (login != null)
                {
                    login.IsLocked = false;
                    login.FailedAttempts = 0;
                }
                context.SaveChanges();
                return login;
            }
        }

        /// <summary>
        /// return the failed attempts if user found or return -1 if user name not found in the database
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public int GetFailedAttempts(string name)
        {
            using (LoanPriceEntities context = new LoanPriceEntities())
            {
                Login login = context.Logins.FirstOrDefault(c => c.Name == name);
                if (login != null)
                {
                    return login.FailedAttempts;
                }
                else
                {
                    return -1;
                }
            }
        }

        /// <summary>
        /// Return the IsLocked check if user found or return Null if no user found
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public bool? IsLocked(string name)
        {
            using (LoanPriceEntities context = new LoanPriceEntities())
            {
                Login login = context.Logins.Where(c => c.Name == name).FirstOrDefault();
                if (login != null)
                {
                    return login.IsLocked;
                }
                else
                {
                    return null;
                }
            }
        }
        public void LogOffAccount(int id)
        {
            using (LoanPriceEntities context = new LoanPriceEntities())
            {
                Login model = context.Logins.FirstOrDefault(c => c.ID == id);
                if (model != null)
                {
                    model.IsLogin = false;
                    context.SaveChanges();
                    // return true;
                }
            }
        }
        public List<Login> GetSigninAccounts()
        {
            using (LoanPriceEntities context = new LoanPriceEntities())
            {
                return context.Logins.Where(c => c.IsLogin == true).ToList();
            }
        }

        public void SetLogin(Login model, int type)
        {
            using (LoanPriceEntities context = new LoanPriceEntities())
            {
                Login login = context.Logins.Where(c => c.ID == model.ID).FirstOrDefault();
                if (login != null)
                {
                    switch (type)
                    {
                        case 1:
                            login.IsLogin = true;
                            context.SaveChanges();
                            break;
                        case 2:
                            login.IsLogin = false;
                            context.SaveChanges();
                            break;
                        default:
                            break;
                    }
                }
            }
        }

        public Login ChangePassword(int id, string newPassword)
        {
            using (LoanPriceEntities context = new LoanPriceEntities())
            {
                Login model = context.Logins.FirstOrDefault(c => c.ID == id);

                // Save password history
                PasswordHistory history = new PasswordHistory();
                history.ChangedDate = DateTime.Now;
                history.ChangedPassword = newPassword;
                history.Password = model.Password;
                history.UserId = model.ID;

                context.PasswordHistories.AddObject(history);

                model.Password = newPassword;

                context.SaveChanges();
                return model;
            }
        }
    }
}
