using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAL;
using System.Data.Objects.DataClasses;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace BLL
{
    public class LoansBLL
    {
        public List<Loans> GetLoans()
        {
            using (LoanPriceEntities context = new LoanPriceEntities())
            {
                return context.Loans.ToList();
            }
        }
        public List<DuplicateLoan> GetDuplicateLoans()
        {
            using (LoanPriceEntities context = new LoanPriceEntities())
            {
                return context.DuplicateLoans.ToList();
            }
        }

        public object GetBorrower()
        {

            using (LoanPriceEntities context = new LoanPriceEntities())
            {
                //   return context.Loans
                //.Select(m => new { m.Borrower })
                //.AsEnumerable()
                //.Distinct()
                //.ToList();
                return context.Borrowers.OrderBy(s => s.Name).ToList();
            }
        }
        public bool RemoveAllLoans()
        {
            try
            {
                using (LoanPriceEntities context = new LoanPriceEntities())
                {
                    List<Loans> loans = context.Loans.ToList();

                    for (int i = 0; i < loans.Count; i++)
                    {
                        LoanScheduleBL loanScheduleBL = new LoanScheduleBL();
                        loanScheduleBL.RemoveLoanSchedule(loans[i].ID);
                        context.DeleteObject(loans[i]);

                    }

                    context.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public void SaveLoan(Loans model)
        {
            using (LoanPriceEntities context = new LoanPriceEntities())
            {
                if (model.ID > 0)
                {
                    context.AddObject("Loans", model);
                }
                else
                {
                    context.AddToLoans(model);
                }
                context.SaveChanges();
            }
        }

        public Loans GetLoanByCode(string loanCode)
        {
            using (LoanPriceEntities context = new LoanPriceEntities())
            {
                return context.Loans.FirstOrDefault(c => c.CodeName == loanCode);
            }
        }
        public bool CheckForValid(string loanName, string counterParty)
        {
            using (LoanPriceEntities context = new LoanPriceEntities())
            {
                List<QuotesAndTrades> quoteList = context.QuotesAndTrades.Where(s => s.LoanName == loanName && s.CounterParty == counterParty).ToList();
                if (quoteList != null && quoteList.Count > 0)
                {
                    //Session.Add("IsDuplicate", "yes");
                    //btnAddTradeQuote_Click(null,null);
                    //    InsertDuplicateRecord();
                    return true;
                }
                else
                    return false;
            }
        }

        public static DateTime AddBusinessDays(DateTime date, int days)
        {
            if (days == 0) return date;

            if (date.DayOfWeek == DayOfWeek.Saturday)
            {
                date = date.AddDays(2);
                days -= 1;
            }
            else if (date.DayOfWeek == DayOfWeek.Sunday)
            {
                date = date.AddDays(1);
                days -= 1;
            }

            date = date.AddDays(days / 5 * 7);
            int extraDays = days % 5;

            if ((int)date.DayOfWeek + extraDays > 5)
            {
                extraDays += 2;
            }

            return date.AddDays(extraDays);

        }

        public string AddImportedLoans(Loans item)
        {
            LogsBLL logBL = new LogsBLL();
            string str = "";
            try
            {
                using (LoanPriceEntities context = new LoanPriceEntities())
                {
                    string codeName = item.CodeName;
                    if (CheckForLoanCode(item.CodeName))
                    {

                        context.AddToLoans(item);
                        context.SaveChanges();
                        //item = GetLoanByCode(codeName);

                        LoanScheduleBL loanScheduleBL = new LoanScheduleBL();
                        string couponDT = item.CouponDate.ToString();
                        DateTime cpnDT;
                        if (couponDT == string.Empty)
                            cpnDT = AddBusinessDays(DateTime.Now, 10);
                        else
                            cpnDT = Convert.ToDateTime(item.CouponDate);
                        DateTime tradeDate = DateTime.Now;
                        DataTable dtSchedule = loanScheduleBL.GenerateTable(15, Convert.ToInt16(item.NoOfAmortisationPoint), Convert.ToDateTime(item.AmortisationsStartPoint), item.CouponFrequency.ToString(), item.Notional.ToString(), Convert.ToDateTime(item.Maturity_Date), Convert.ToDateTime(item.CouponDate), Convert.ToDecimal(item.Margin), Convert.ToString(item.Currency), Convert.ToDateTime(tradeDate), AddBusinessDays(Convert.ToDateTime(tradeDate), 10));
                        if (dtSchedule != null)
                        {
                            try
                            {
                                foreach (DataRow dr in dtSchedule.Rows)
                                {
                                    LoanSchedule loanSchedule = new LoanSchedule();
                                    loanSchedule.LoanID = item.ID;
                                    loanSchedule.StartDate = Convert.ToDateTime(dr["StartDate"]);
                                    loanSchedule.EndDate = Convert.ToDateTime(dr["EndDate"]);
                                    loanSchedule.Notation = Convert.ToDecimal(dr["Notation"]);
                                    loanSchedule.CoupFrac = Convert.ToDecimal(dr["CoupFrac"]);
                                    loanSchedule.Amortisation = Convert.ToDecimal(dr["Amortisation"]);
                                    loanSchedule.Factor = Convert.ToDecimal(dr["Factor"]);
                                    loanSchedule.CouponPaymentDate = Convert.ToDateTime(dr["CouponPaymentDate"]);
                                    loanSchedule.Spread = Convert.ToDecimal(dr["Spread"]);
                                    loanSchedule.RiskFreeDP1 = Convert.ToDecimal(dr["RiskFreeDP1"]);
                                    loanSchedule.RiskFreeDP2 = Convert.ToDecimal(dr["RiskFreeDP2"]);
                                    loanSchedule.FloatingRate = Convert.ToDecimal(dr["FloatingRate"]);
                                    loanSchedule.AllInRate = Convert.ToDecimal(dr["AllInRate"]);
                                    loanSchedule.Interest = Convert.ToDecimal(dr["Interest"]);
                                    loanSchedule.Days = Convert.ToInt16(dr["Days"]);
                                    loanSchedule.AmortisationInt = Convert.ToDecimal(dr["AmortisationInt"]);
                                    loanScheduleBL.SaveLoanSchedule(loanSchedule);
                                }
                            }
                            catch (Exception ex)
                            {
                                str = ex.Message;
                                //  str = ex.Message;
                            }
                        }
                    }
                    else
                    {
                        DuplicateLoan loan = new DuplicateLoan();
                        loan.CodeName = item.CodeName;
                        loan.Borrower = item.Borrower;
                        loan.Country = item.Country;
                        loan.Sector = item.Sector;
                        loan.Maturity_Date = item.Maturity_Date;
                        loan.Signing_Date = item.Signing_Date;
                        loan.FixedOrFloating = item.FixedOrFloating;
                        loan.Margin = item.Margin;
                        loan.Currency = item.Currency;
                        loan.CouponFrequency = item.CouponFrequency;
                        loan.FacilitySize = item.FacilitySize;
                        loan.Bilateral = item.Bilateral;
                        loan.Amortizing = item.Amortizing;

                        loan.AmortisationsStartPoint = loan.AmortisationsStartPoint;
                        loan.CouponDate = loan.CouponDate;
                        loan.Notional = loan.Notional;
                        loan.NoOfAmortisationPoint = loan.NoOfAmortisationPoint;

                        context.AddToDuplicateLoans(loan); context.SaveChanges();
                    }


                }
            }
            catch (Exception ex)
            {

                str = ex.Message;
                if (ex.InnerException != null)
                {
                    str = str + " :: " + ex.InnerException.Message;
                }
            }
            return str;
        }

        public int GetNoOfAmortisations()
        {
            return 10;
        }
        public List<Loans> BindLoanAData()
        {
            using (LoanPriceEntities context = new LoanPriceEntities())
            {
                var osoba = from o in context.Loans
                            orderby o.CodeName
                            select o;
                return osoba.ToList();
            }

        }
        public Loans GetLoanByID(int loanID)
        {
            using (LoanPriceEntities context = new LoanPriceEntities())
            {
                return context.Loans.FirstOrDefault(c => c.ID == loanID);
            }
        }
        public void EditLoan(Loans loan, int loanID, int type)
        {
            try
            {
                using (LoanPriceEntities context = new LoanPriceEntities())
                {

                    Loans oldLoan = context.Loans.FirstOrDefault(c => c.ID == loanID);
                    oldLoan.CodeName = loan.CodeName;
                    oldLoan.Borrower = loan.Borrower;
                    oldLoan.Sector = loan.Sector;
                    oldLoan.Signing_Date = loan.Signing_Date;
                    oldLoan.Maturity_Date = loan.Maturity_Date;
                    oldLoan.FixedOrFloating = loan.FixedOrFloating;
                    oldLoan.Margin = loan.Margin;
                    oldLoan.Currency = loan.Currency;
                    oldLoan.PP = loan.PP;
                    oldLoan.CreditRating = loan.CreditRating;
                    oldLoan.Country = loan.Country;
                    oldLoan.CouponFrequency = loan.CouponFrequency;
                    oldLoan.FacilitySize = loan.FacilitySize;
                    oldLoan.Bilateral = loan.Bilateral;
                    oldLoan.Amortizing = loan.Amortizing;

                    oldLoan.AmortisationsStartPoint = loan.AmortisationsStartPoint;
                    oldLoan.CouponDate = loan.CouponDate;
                    oldLoan.Notional = loan.Notional;
                    oldLoan.NoOfAmortisationPoint = loan.NoOfAmortisationPoint;

                    oldLoan.CreditRatingFitch = loan.CreditRatingFitch;
                    oldLoan.CreditRatingING = loan.CreditRatingING;
                    oldLoan.CreditRatingModys = loan.CreditRatingModys;
                    oldLoan.CreditRatingSPs = loan.CreditRatingSPs;
                    oldLoan.StructureID = loan.StructureID;

                    oldLoan.LastEdited = loan.LastEdited;
                    oldLoan.CreatedBy = loan.CreatedBy;

                    oldLoan.SummitCreditEntity = loan.SummitCreditEntity;
                    oldLoan.Grid = loan.Grid;
                    oldLoan.Gurantor = loan.Gurantor;
                    context.SaveChanges();

                    //if type == 1 then do not add to DuplicateLoan
                    if (CheckForLoanCode(loan.CodeName, loanID) && type != 1)
                    {
                        DuplicateLoan duplicateLoan = new DuplicateLoan();
                        duplicateLoan.CodeName = loan.CodeName;
                        duplicateLoan.Borrower = loan.Borrower;
                        duplicateLoan.Sector = loan.Sector;
                        duplicateLoan.Signing_Date = loan.Signing_Date;
                        duplicateLoan.Maturity_Date = loan.Maturity_Date;
                        duplicateLoan.FixedOrFloating = loan.FixedOrFloating;
                        duplicateLoan.Margin = loan.Margin;
                        duplicateLoan.Currency = loan.Currency;

                        duplicateLoan.Country = loan.Country;
                        duplicateLoan.CouponFrequency = loan.CouponFrequency;
                        duplicateLoan.FacilitySize = loan.FacilitySize;
                        duplicateLoan.Bilateral = loan.Bilateral;
                        duplicateLoan.Amortizing = loan.Amortizing;

                        duplicateLoan.AmortisationsStartPoint = loan.AmortisationsStartPoint;
                        duplicateLoan.CouponDate = loan.CouponDate;
                        duplicateLoan.Notional = loan.Notional;
                        duplicateLoan.NoOfAmortisationPoint = loan.NoOfAmortisationPoint;

                        context.AddToDuplicateLoans(duplicateLoan);
                        context.SaveChanges();
                    }

                }
            }
            catch (Exception)
            {
            }

        }
        public string GetCoutryIDbyLoanID(string loanCode)
        {
            using (LoanPriceEntities context = new LoanPriceEntities())
            {
                Loans loan = context.Loans.SingleOrDefault(s => s.CodeName == loanCode);
                if (loan != null)
                    return loan.Country;
                else
                    return null;
            }
        }

        /// <summary>
        /// Check for loan name
        /// </summary>
        /// <param name="loanCode"></param>
        /// <returns></returns>
        public Boolean CheckForLoanCode(string loanCode)
        {
            using (LoanPriceEntities context = new LoanPriceEntities())
            {
                var count = context.Loans.Where(s => s.CodeName.Trim().ToLower() == loanCode.ToLower()).Count();
                if (count >= 1)
                    return false;
                else
                    return true;

            }
        }


        public Boolean CheckForLoanCode(string loanCode, int loanID)
        {
            using (LoanPriceEntities context = new LoanPriceEntities())
            {
                var count = context.Loans.Where(s => s.CodeName.Trim().ToLower() == loanCode.ToLower() && s.ID != loanID).Count();
                if (count >= 1)
                    return false;
                else
                    return true;
            }
        }

        ///
        /// Insert Duplicate Records
        ///
        public void SaveDuplicateLoan(DuplicateLoan model)
        {
            using (LoanPriceEntities context = new LoanPriceEntities())
            {
                if (model.ID > 0)
                {
                    context.AddObject("DuplicateLoans", model);
                }
                else
                {
                    context.AddToDuplicateLoans(model);
                }
                context.SaveChanges();
            }
        }

        public bool RemoveLoan(int id)
        {
            using (LoanPriceEntities context = new LoanPriceEntities())
            {
                Loans model = context.Loans.SingleOrDefault(c => c.ID == id);
                if (model != null)
                {
                    // Delete the Password policy of this account

                    LoanScheduleBL loanScheduleBL = new LoanScheduleBL();
                    loanScheduleBL.RemoveLoanSchedule(id);
                    context.Loans.DeleteObject(model);
                    context.SaveChanges();
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public DataSet GetLoanCountryGraphDetails()
        {
            try
            {
                DataSet ds = new DataSet();
                try
                {
                    string sqlconnectionstr = ConfigurationSettings.AppSettings["ConnectionString"].ToString();
                    ds = SqlHelper.SqlHelper.ExecuteDataset(sqlconnectionstr, CommandType.StoredProcedure, "sp_GetCountryLoanGraphData");

                }
                catch (SqlException ex)
                {

                }
                catch (Exception ex)
                {

                }
                return ds;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public int GetLoanCount()
        {
            using (LoanPriceEntities context = new LoanPriceEntities())
            {
                return context.Loans.Count();

            }
        }

        public List<Sectors> GetSector()
        {
            using (LoanPriceEntities context = new LoanPriceEntities())
            {
                var result = context.Loans.Select(s => s.Sector).Distinct().ToList();
                List<Sectors> sector = new List<Sectors>();
                foreach (var item in result)
                {
                    if (item != null)
                    {

                        sector.Add(new Sectors(item.ToString()));
                    }

                }
                return sector;
            }

        }

        public List<PP> GetPP()
        {
            using (LoanPriceEntities context = new LoanPriceEntities())
            {
                var result = context.Loans.Select(s => s.PP).Distinct().ToList();
                List<PP> pp = new List<PP>();
                foreach (var item in result)
                {
                    if (item != null)
                    {

                        pp.Add(new PP(item.ToString()));
                    }

                }
                return pp;
            }

        }

        public List<FixedOrFloating> GetFixedFloating()
        {
            using (LoanPriceEntities context = new LoanPriceEntities())
            {
                var result = context.Loans.Select(s => s.FixedOrFloating).Distinct().ToList();
                List<FixedOrFloating> fof = new List<FixedOrFloating>();
                foreach (var item in result)
                {
                    if (item != null)
                    {

                        fof.Add(new FixedOrFloating(item.ToString()));
                    }

                }
                return fof;
            }

        }

        public List<Notional> GetNotional()
        {
            using (LoanPriceEntities context = new LoanPriceEntities())
            {
                var result = context.Loans.Select(s => s.Notional).Distinct().ToList();
                List<Notional> fof = new List<Notional>();
                foreach (var item in result)
                {
                    if (item != null)
                    {

                        fof.Add(new Notional(item.ToString()));
                    }

                }
                return fof;
            }

        }
        public List<Margin> GetMargin()
        {
            using (LoanPriceEntities context = new LoanPriceEntities())
            {
                var result = context.Loans.Select(s => s.Margin).Distinct().ToList();
                List<Margin> fof = new List<Margin>();
                foreach (var item in result)
                {
                    if (item != null)
                    {

                        fof.Add(new Margin(item.ToString()));
                    }

                }
                return fof;
            }

        }

        public List<CouponFrequency> GetCouponFrequency()
        {
            using (LoanPriceEntities context = new LoanPriceEntities())
            {
                var result = context.Loans.Select(s => s.CouponFrequency).Distinct().ToList();
                List<CouponFrequency> fof = new List<CouponFrequency>();
                foreach (var item in result)
                {
                    if (item != null)
                    {

                        fof.Add(new CouponFrequency(item.ToString()));
                    }

                }
                return fof;
            }

        }


        public List<FacilitySize> GetFacilitySize()
        {
            using (LoanPriceEntities context = new LoanPriceEntities())
            {
                var result = context.Loans.Select(s => s.FacilitySize).Distinct().ToList();
                List<FacilitySize> fof = new List<FacilitySize>();
                foreach (var item in result)
                {
                    if (item != null)
                    {

                        fof.Add(new FacilitySize(item.ToString()));
                    }

                }
                return fof;
            }

        }


        public List<CreditRating> GetCreditRatingModdys()
        {
            using (LoanPriceEntities context = new LoanPriceEntities())
            {
                int agencyID = context.CreditAgencies.SingleOrDefault(s => s.CreditAgency1 == "Moody's").ID;

                var osoba = from o in context.CreditRatings
                            where o.CreditAgencyID == agencyID
                            orderby o.Rating
                            select o;
                return osoba.ToList();
            }
        }

        public List<CreditRating> GetCreditRatingSP()
        {
            using (LoanPriceEntities context = new LoanPriceEntities())
            {
                int agencyID = context.CreditAgencies.SingleOrDefault(s => s.CreditAgency1 == "S&P's").ID;

                var osoba = from o in context.CreditRatings
                            where o.CreditAgencyID == agencyID
                            orderby o.Rating
                            select o;
                return osoba.ToList();
            }
        }

        public List<CreditRating> GetCreditRatingFitch()
        {
            using (LoanPriceEntities context = new LoanPriceEntities())
            {
                int agencyID = context.CreditAgencies.SingleOrDefault(s => s.CreditAgency1 == "Fitch").ID;

                var osoba = from o in context.CreditRatings
                            where o.CreditAgencyID == agencyID
                            orderby o.Rating
                            select o;
                return osoba.ToList();
            }
        }

        public List<CreditRating> GetCreditRatingING()
        {
            using (LoanPriceEntities context = new LoanPriceEntities())
            {
                int agencyID = context.CreditAgencies.SingleOrDefault(s => s.CreditAgency1 == "ING").ID;

                var osoba = from o in context.CreditRatings
                            where o.CreditAgencyID == agencyID
                            orderby o.Rating
                            select o;
                return osoba.ToList();
            }
        }

        public List<NoOfAmortrization> GetNoOfAmortrization()
        {
            using (LoanPriceEntities context = new LoanPriceEntities())
            {
                var result = context.Loans.Select(s => s.NoOfAmortisationPoint).Distinct().ToList();
                List<NoOfAmortrization> fof = new List<NoOfAmortrization>();
                foreach (var item in result)
                {
                    if (item != null)
                    {

                        fof.Add(new NoOfAmortrization(item.ToString()));
                    }

                }
                return fof;
            }

        }

        public List<StructureID> GetStructureID()
        {
            using (LoanPriceEntities context = new LoanPriceEntities())
            {
                var result = context.Loans.Select(s => s.StructureID).Distinct().ToList();
                List<StructureID> fof = new List<StructureID>();
                foreach (var item in result)
                {
                    if (item != null)
                    {

                        fof.Add(new StructureID(item.ToString()));
                    }

                }
                return fof;
            }

        }
        //public bool CheckForExist(string loanName,DateTime tradeDate,string counterParty)
        //{
        //    using (LoanPriceEntities context = new LoanPriceEntities())
        //    {
        //        List<Loans> loanList = context.Loans.Where(s => s.CodeName == loanName && s.Signing_Date == tradeDate && s.cou 
        //    }
        //}
    }

    public class Sectors
    {
        public string Sector { get; set; }

        public Sectors(string name)
        {
            this.Sector = name;
        }
    }

    public class PP
    {
        public string pp { get; set; }
        public PP(string name)
        {
            this.pp = name;
        }
    }

    public class FixedOrFloating
    {
        public string fixedorfloating { get; set; }
        public FixedOrFloating(string name)
        {
            this.fixedorfloating = name;
        }
    }

    public class Notional
    {
        public string notional { get; set; }
        public Notional(string name)
        {
            this.notional = name;
        }
    }

    public class Margin
    {
        public string margin { get; set; }
        public Margin(string name)
        {
            this.margin = name;
        }
    }

    public class CouponFrequency
    {
        public string coupon { get; set; }
        public CouponFrequency(string name)
        {
            this.coupon = name;
        }
    }

    public class FacilitySize
    {
        public string facility { get; set; }
        public FacilitySize(string name)
        {
            this.facility = name;
        }
    }

    //public class CreditRatings
    //{
    //    public string creditRatings { get; set; }
    //    public CreditRatings(string rating)
    //    {
    //        this.creditRatings = rating;
    //    }
    //}

    public class NoOfAmortrization
    {
        public string noofAmort { get; set; }
        public NoOfAmortrization(string name)
        {
            this.noofAmort = name;
        }
    }

    public class StructureID
    {
        public string structureID { get; set; }
        public StructureID(string name)
        {
            this.structureID = name;
        }
    }
}



