using DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class LoanScheduleBL
    {
        public void SaveLoanSchedule(LoanSchedule model)
        {
            using (LoanPriceEntities context = new LoanPriceEntities())
            {
                if (model.ID > 0)
                {
                    context.AddObject("LoanSchedule", model);
                }
                else
                {
                    context.AddToLoanSchedules(model);
                }
                context.SaveChanges();
            }
        }
        public List<LoanSchedule> GetLoanByID(int loanID)
        {
            try
            {
                using (LoanPriceEntities context = new LoanPriceEntities())
                {
                    List<LoanSchedule> scheduleList = context.LoanSchedules.Where(s => s.LoanID == loanID).ToList();
                    if (scheduleList.Count == 0)
                        return null;
                    else
                        return scheduleList;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public bool RemoveLoanSchedule(int loanID)
        {
            try
            {
                using (LoanPriceEntities context = new LoanPriceEntities())
                {
                    List<LoanSchedule> loans = context.LoanSchedules.Where(s => s.LoanID == loanID).ToList();

                    for (int i = 0; i < loans.Count; i++)
                    {
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
        public void EditSchedule(int loanID, DataTable dt)
        {
            try
            {
                using (LoanPriceEntities context = new LoanPriceEntities())
                {
                    List<LoanSchedule> oldScheduleList = context.LoanSchedules.Where(s => s.LoanID == loanID).ToList();
                    if (oldScheduleList.Count > 0)
                    {
                        LoanScheduleBL loanScheduleBL = new LoanScheduleBL();
                        loanScheduleBL.RemoveLoanSchedule(loanID);

                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            // oldScheduleList[i].ID = Convert.ToInt16(dt.Rows[i][0]);

                            //if (i >= oldScheduleList.Count)
                            //{
                            LoanSchedule loanSchedule = new LoanSchedule();
                            loanSchedule.LoanID = loanID;
                            loanSchedule.StartDate = Convert.ToDateTime(dt.Rows[i][1].ToString());
                            loanSchedule.EndDate = Convert.ToDateTime(dt.Rows[i][2].ToString());
                            loanSchedule.CoupFrac = Convert.ToDecimal(Convert.ToDecimal(dt.Rows[i][3].ToString()).ToString("0.00"));
                            loanSchedule.Notation = Convert.ToDecimal(dt.Rows[i][4].ToString());
                            loanSchedule.Amortisation = Convert.ToDecimal(Convert.ToDecimal(dt.Rows[i][5].ToString()).ToString("0.00"));
                            loanSchedule.Factor = Convert.ToDecimal(Convert.ToDecimal(dt.Rows[i][6].ToString()).ToString("0.00000"));
                            if (dt.Rows[i][7] != null)
                                loanSchedule.Spread = Convert.ToDecimal(Convert.ToDecimal(dt.Rows[i][7].ToString()).ToString("0.00000"));
                            if (dt.Rows[i][8] != null)
                                loanSchedule.CouponPaymentDate = Convert.ToDateTime(dt.Rows[i][8].ToString());
                            if (dt.Rows[i][9] != null)
                                loanSchedule.RiskFreeDP1 = Convert.ToDecimal(Convert.ToDecimal(dt.Rows[i][9].ToString()).ToString("0.0000000"));
                            if (dt.Rows[i][10] != null)
                                loanSchedule.RiskFreeDP2 = Convert.ToDecimal(Convert.ToDecimal(dt.Rows[i][10].ToString()).ToString("0.0000000"));
                            if (dt.Rows[i][11] != null)
                                loanSchedule.FloatingRate = Convert.ToDecimal(Convert.ToDecimal(dt.Rows[i][11].ToString()).ToString("0.0000000"));
                            if (dt.Rows[i][12] != null)
                                loanSchedule.AllInRate = Convert.ToDecimal(Convert.ToDecimal(dt.Rows[i][12].ToString()).ToString("0.00000"));
                            if (dt.Rows[i][13] != null)
                                loanSchedule.Interest = Convert.ToDecimal(Convert.ToDecimal(dt.Rows[i][13].ToString()).ToString("0.00000"));
                            if (dt.Rows[i][14] != null)
                                loanSchedule.Days = Convert.ToInt16(dt.Rows[i][14].ToString());
                            if (dt.Rows[i][15] != null)
                                loanSchedule.AmortisationInt = Convert.ToDecimal(Convert.ToDecimal(dt.Rows[i][15].ToString()).ToString("0.0000000"));
                            SaveLoanSchedule(loanSchedule);
                            // }
                            //else
                            //{
                            //    oldScheduleList[i].StartDate = Convert.ToDateTime(dt.Rows[i][1].ToString());
                            //    oldScheduleList[i].EndDate = Convert.ToDateTime(dt.Rows[i][2].ToString());
                            //    oldScheduleList[i].CoupFrac = Convert.ToDecimal(Convert.ToDecimal(dt.Rows[i][3].ToString()).ToString("0.00"));
                            //    oldScheduleList[i].Notation = Convert.ToDecimal(dt.Rows[i][4].ToString());
                            //    oldScheduleList[i].Amortisation = Convert.ToDecimal(Convert.ToDecimal(dt.Rows[i][5].ToString()).ToString("0.00"));
                            //    oldScheduleList[i].Factor = Convert.ToDecimal(Convert.ToDecimal(dt.Rows[i][6].ToString()).ToString("0.00000"));
                            //}
                        }
                    }
                    else
                    {
                        foreach (DataRow dr in dt.Rows)
                        {
                            LoanSchedule loanSchedule = new LoanSchedule();
                            loanSchedule.LoanID = loanID;
                            loanSchedule.StartDate = Convert.ToDateTime(dr["StartDate"]);
                            loanSchedule.EndDate = Convert.ToDateTime(dr["EndDate"]);
                            loanSchedule.CoupFrac = Convert.ToDecimal(dr["CoupFrac"]);
                            loanSchedule.Notation = Convert.ToDecimal(dr["Notation"]);
                            loanSchedule.Amortisation = Convert.ToDecimal(dr["Amortisation"]);
                            loanSchedule.Factor = Convert.ToDecimal(Convert.ToDecimal(dr["Factor"]).ToString("0.00000"));
                            loanSchedule.Spread = Convert.ToDecimal(dr["Spread"]);
                            loanSchedule.AllInRate = Convert.ToDecimal(dr["AllInRate"]);
                            loanSchedule.CouponPaymentDate = Convert.ToDateTime(dr["CouponPaymentDate"]);
                            loanSchedule.RiskFreeDP1 = Convert.ToDecimal(dr["RiskFreeDP1"]);
                            loanSchedule.RiskFreeDP2 = Convert.ToDecimal(dr["RiskFreeDP2"]);
                            loanSchedule.FloatingRate = Convert.ToDecimal(dr["FloatingRate"]);
                            loanSchedule.Interest = Convert.ToDecimal(dr["Interest"]);
                            loanSchedule.Days = Convert.ToInt16(dr["Days"]);
                            loanSchedule.AmortisationInt = Convert.ToDecimal(dr["AmortisationInt"]);
                            SaveLoanSchedule(loanSchedule);
                        }
                    }
                    context.SaveChanges();
                }
            }
            catch (Exception)
            {

            }
        }

        public void UpdateSpread(int loanID, Decimal spread)
        {
            using (LoanPriceEntities context = new LoanPriceEntities())
            {
                List<LoanSchedule> scheduleList = context.LoanSchedules.Where(s => s.LoanID == loanID).ToList();
                foreach (var item in scheduleList)
                {
                    item.Spread = spread;
                }
                context.SaveChanges();
            }
        }


        #region Generate Loan Schedule

        //public DataTable GenerateTable(int colsCount, int rowsCount, DateTime startDate, string frequency, string notional, DateTime maturityDate, DateTime couponDate, Decimal spread, string ccy, DateTime tradedDate)
        //{
        //    DataTable dtSchedule = new DataTable();

        //    try
        //    {
        //        dtSchedule = new DataTable();
        //        dtSchedule.Columns.Add("ID");
        //        dtSchedule.Columns.Add("StartDate");
        //        dtSchedule.Columns.Add("EndDate");
        //        dtSchedule.Columns.Add("CoupFrac");
        //        dtSchedule.Columns.Add("Notation");
        //        dtSchedule.Columns.Add("Amortisation");
        //        dtSchedule.Columns.Add("Factor");
        //        dtSchedule.Columns.Add("Spread");
        //        dtSchedule.Columns.Add("CouponPaymentDate");
        //        dtSchedule.Columns.Add("RiskFreeDP1");
        //        dtSchedule.Columns.Add("RiskFreeDP2");
        //        dtSchedule.Columns.Add("FloatingRate");
        //        dtSchedule.Columns.Add("AllInRate");
        //        dtSchedule.Columns.Add("Interest");
        //        dtSchedule.Columns.Add("Days");
        //        dtSchedule.Columns.Add("AmortisationInt");
        //        System.Text.StringBuilder sb = new System.Text.StringBuilder();



        //        DateTime previousStartDate = startDate;

        //        Decimal firstNationalRow = Convert.ToDecimal(notional);
        //        Decimal nationalRow = Convert.ToDecimal(notional);
        //        bool firstAmortizingRow = false;
        //        int activeCoupons = 0;

        //        // change this condition to while currCouponDate <= maturityDate

        //        for (int i = 0; i < rowsCount; i++)
        //        {

        //            DataRow dr = dtSchedule.NewRow();
        //            dr["ID"] = (i + 1).ToString();


        //            for (int j = 0; j < colsCount; j++)
        //            {

        //                if (i == 0 && j == 0)
        //                {
        //                    //tb.Text = startDate.ToShortDateString();

        //                    dr["StartDate"] = startDate.ToShortDateString();
        //                }
        //                else if (i > 0 && j == 0)
        //                {
        //                    switch (frequency.Trim())
        //                    {
        //                        case "Monthly":
        //                            previousStartDate = startDate;
        //                            startDate = startDate.AddMonths(1).Date;

        //                            //tb.Text = startDate.ToShortDateString();
        //                            dr["StartDate"] = startDate.ToShortDateString();
        //                            break;
        //                        case "Quarterly":
        //                            previousStartDate = startDate;
        //                            startDate = startDate.AddMonths(3).Date;

        //                            dr["StartDate"] = startDate.ToShortDateString();
        //                            break;
        //                        case "Semi-Annual":
        //                            previousStartDate = startDate;
        //                            startDate = startDate.AddMonths(6).Date;
        //                            dr["StartDate"] = startDate.ToShortDateString();
        //                            break;
        //                        case "Annual":
        //                            previousStartDate = startDate;
        //                            startDate = startDate.AddYears(1).Date;
        //                            dr["StartDate"] = startDate.ToShortDateString();
        //                            break;
        //                        default:
        //                            break;
        //                    }

        //                }
        //                if (i == 0 && j == 1)
        //                {
        //                    DateTime endDate;
        //                    switch (frequency.Trim())
        //                    {
        //                        case "Monthly":
        //                            endDate = startDate.AddMonths(1).Date;
        //                            dr["EndDate"] = endDate.ToShortDateString();
        //                            break;
        //                        case "Quarterly":
        //                            endDate = startDate.AddMonths(3).Date;
        //                            dr["EndDate"] = endDate.ToShortDateString();
        //                            break;
        //                        case "Semi-Annual":
        //                            endDate = startDate.AddMonths(6).Date;
        //                            dr["EndDate"] = endDate.ToShortDateString();
        //                            break;
        //                        case "Annual":
        //                            endDate = startDate.AddYears(1).Date;
        //                            dr["EndDate"] = endDate.ToShortDateString();
        //                            break;
        //                        default:
        //                            break;
        //                    }
        //                }
        //                else if (i > 0 && j == 1)
        //                {
        //                    DateTime endDate;
        //                    // Compute next coupon date using CoupNCD and correct for weekends
        //                    // f define frequency
        //                    // so now end dates should be generated for the total number of coupons
        //                    CalculatedDates cal = new CalculatedDates();
        //                    System.Numeric.DayCountBasis d = System.Numeric.DayCountBasis.Actual365;
        //                    prevCouponDate = currCouponDate;
        //                    currentCouponDate = System.Numeric.Financial.CoupNCD(currentCouponDate, maturityDate, f, d);
        //                    //DateTime previousBusinessDay = PreviousBusinessDay(currentCouponDate);
        //                    //cal.CalculatedDate = previousBusinessDay;
        //                    dr["EndDate"] = currentCouponDate.ToShortDateString();

        //                    //switch (frequency.Trim())
        //                    //{
        //                    //    case "Monthly":
        //                    //        endDate = startDate.AddMonths(1).Date;
        //                    //        dr["EndDate"] = endDate.ToShortDateString();
        //                    //        break;
        //                    //    case "Quarterly":
        //                    //        endDate = startDate.AddMonths(3).Date;
        //                    //        dr["EndDate"] = endDate.ToShortDateString();
        //                    //        break;
        //                    //    case "Semi-Annual":
        //                    //        endDate = startDate.AddMonths(6).Date;
        //                    //        dr["EndDate"] = endDate.ToShortDateString();
        //                    //        break;
        //                    //    case "Annual":
        //                    //        endDate = startDate.AddYears(1).Date;
        //                    //        dr["EndDate"] = endDate.ToShortDateString();
        //                    //        break;
        //                    //    default:
        //                    //        break;
        //                    //}
        //                }
        //                if (j == 2)
        //                {

        //                    if (maturityDate != null)
        //                    {
        //                        DateTime maturityDt = Convert.ToDateTime(maturityDate);
        //                        DateTime endDt = Convert.ToDateTime(dr["EndDate"]);
        //                        decimal days = (endDt - maturityDt).Days;
        //                        decimal totalDays = 365;
        //                        decimal coupFreqDays = days / totalDays;
        //                        if (days > 0)
        //                            dr["CoupFrac"] = Convert.ToDecimal(coupFreqDays).ToString("0.00");
        //                        else
        //                            dr["CoupFrac"] = Convert.ToDecimal(0).ToString("0.00");
        //                    }
        //                }
        //                if (j == 3)
        //                {
        //                    if (i == 0)
        //                    {
        //                        firstNationalRow = Convert.ToDecimal(notional);
        //                        nationalRow = firstNationalRow;
        //                    }
        //                    else
        //                    {
        //                        nationalRow = Convert.ToDecimal(Convert.ToDecimal(dtSchedule.Rows[i - 1]["Notation"]) - Convert.ToDecimal(dtSchedule.Rows[i - 1]["Amortisation"]));
        //                    }
        //                    dr["Notation"] = nationalRow.ToString("N");
        //                }
        //                if (j == 4)
        //                {

        //                    DateTime dt = Convert.ToDateTime(dr["StartDate"]);
        //                    if (couponDate != null && dt >= couponDate)
        //                    {
        //                        if (firstAmortizingRow == false)
        //                            activeCoupons = rowsCount - i;
        //                        Decimal amortisation = Convert.ToDecimal(firstNationalRow / activeCoupons);
        //                        dr["Amortisation"] = Convert.ToDecimal(amortisation).ToString("0.00");
        //                        firstAmortizingRow = true;
        //                    }
        //                    else
        //                        dr["Amortisation"] = "0.00";
        //                }
        //                if (j == 5)
        //                {
        //                    Decimal factorRow = Convert.ToDecimal(nationalRow / firstNationalRow);
        //                    dr["Factor"] = factorRow;
        //                }
        //                if (j == 6)
        //                {
        //                    dr["Spread"] = Convert.ToDecimal(spread);
        //                }
        //                if (j == 7)
        //                {
        //                    dr["CouponPaymentDate"] = Convert.ToDateTime(dr["EndDate"]);
        //                }
        //                if (j == 8)
        //                {
        //                    if (ccy.ToLower() == "eur")
        //                    {
        //                        EURCurvesBL eurCurveBL = new EURCurvesBL();
        //                        List<EURCurve> eurCurve = eurCurveBL.GetEURCurve();
        //                        Dictionary<double, double> rateVals = new Dictionary<double, double>();
        //                        foreach (var item in eurCurve)
        //                        {
        //                            rateVals.Add(Convert.ToDouble(item.RateDate.Value.Ticks), Convert.ToDouble(item.Rate));
        //                        }
        //                        var scaler = new SplineInterpolator(rateVals);
        //                        var y = scaler.GetValue(Convert.ToDateTime(dr["EndDate"]).Ticks);
        //                        dr["RiskFreeDP1"] = Math.Exp(-((y / 100) * (((Convert.ToDateTime(dr["EndDate"]) - Convert.ToDateTime(tradedDate)).Days)) / 365));
        //                    }
        //                    else if (ccy.ToLower() == "usd")
        //                    {
        //                          USDCurveBL usdCurveBL = new USDCurveBL();
        //                          List<USDCurve> usdCurve = usdCurveBL.GetUSCurve();
        //                          Dictionary<double, double> rateVals = new Dictionary<double, double>();
        //                          foreach (var item in usdCurve)
        //                          {
        //                              rateVals.Add(Convert.ToDouble(item.RateDate.Value.Ticks), Convert.ToDouble(item.Rate));
        //                          }
        //                          var scaler = new SplineInterpolator(rateVals);
        //                          var y = scaler.GetValue(Convert.ToDateTime(dr["EndDate"]).Ticks);
        //                          dr["RiskFreeDP1"] = Math.Exp(-((y / 100) * (((Convert.ToDateTime(dr["EndDate"]) - Convert.ToDateTime(tradedDate)).Days)) / 365));
        //                    }
        //                    //  SplineInterpolator interPolation = new  SplineInterpolator();
        //                    // dr["RiskFreeDP1"]=  ;//EXP(-(interpolate(schedule.endDate(i),EurDB.Rates,'EurDB.Rates')/100*(schedule.endDate(i)-loan.settlementDate)/365));
        //                }
        //                if (j == 9)
        //                {
        //                    if (dr["EndDate"].ToString() != string.Empty)
        //                    {
        //                        dr["RiskFreeDP2"] = Convert.ToDouble(dr["RiskFreeDP1"]) / Convert.ToDouble((Math.Pow(Convert.ToDouble((1 + (spread / 20000))), ((2 * (Convert.ToDateTime(dr["EndDate"]) - Convert.ToDateTime(tradedDate)).Days) / 360))));
        //                    }
        //                }

        //                if (j == 10)
        //                {
        //                    if (i == 0)
        //                    {
        //                        dr["FloatingRate"] = 0.5;
        //                    }
        //                    else
        //                    {
        //                        double d = Convert.ToDouble(dtSchedule.Rows[i - 1]["RiskFreeDP1"]) / Convert.ToDouble(dr["RiskFreeDP1"]) - 1 / ((Convert.ToDateTime(dr["EndDate"]) - Convert.ToDateTime(dtSchedule.Rows[i - 1]["EndDate"])).Days) / 360;
        //                        dr["FloatingRate"] = Convert.ToDouble(d) / 100;
        //                    }
        //                }
        //                if (j == 11)
        //                {
        //                    dr["AllInRate"] = Convert.ToDouble(Convert.ToDouble(dr["FloatingRate"]) + Convert.ToDouble(spread)) / 100;
        //                }
        //                if (j == 12)
        //                {
        //                    dr["Interest"] = ((Convert.ToDateTime(dr["EndDate"]) - Convert.ToDateTime(dr["StartDate"])).Days / 360) * Convert.ToDouble(dr["AllInRate"]) * Convert.ToDouble(dr["Notation"]);
        //                }
        //                if (j == 13)
        //                {
        //                    dr["Days"] = (Convert.ToDateTime(dr["EndDate"]) - Convert.ToDateTime(dr["StartDate"])).Days;
        //                }
        //                if (j == 14)
        //                {
        //                    dr["AmortisationInt"] = Convert.ToDouble(dr["Amortisation"]) + Convert.ToDouble(dr["Interest"]);
        //                }

        //            }
        //            dtSchedule.Rows.Add(dr);


        //        }
        //    }
        //    catch (Exception ex)
        //    {


        //    }
        //    return dtSchedule;
        //}


        public DataTable GenerateTable(int colsCount, int rowsCount, DateTime startDate, string frequency, string notional, DateTime maturityDate, DateTime couponDate, Decimal spread, string ccy, DateTime tradedDate, DateTime settlementDate)
        {
            DataTable dtSchedule = new DataTable();

            try
            {
                dtSchedule = new DataTable();
                dtSchedule.Columns.Add("ID");
                dtSchedule.Columns.Add("StartDate");
                dtSchedule.Columns.Add("EndDate");
                dtSchedule.Columns.Add("CoupFrac");
                dtSchedule.Columns.Add("Notation");
                dtSchedule.Columns.Add("Amortisation");
                dtSchedule.Columns.Add("Factor");
                dtSchedule.Columns.Add("Spread");
                dtSchedule.Columns.Add("CouponPaymentDate");
                dtSchedule.Columns.Add("RiskFreeDP1");
                dtSchedule.Columns.Add("RiskFreeDP2");
                dtSchedule.Columns.Add("FloatingRate");
                dtSchedule.Columns.Add("AllInRate");
                dtSchedule.Columns.Add("Interest");
                dtSchedule.Columns.Add("Days");
                dtSchedule.Columns.Add("AmortisationInt");
                System.Text.StringBuilder sb = new System.Text.StringBuilder();



                DateTime previousStartDate = startDate;

                Decimal firstNotionalRow = Convert.ToDecimal(notional);
                Decimal notionalRow = Convert.ToDecimal(notional);
                bool firstAmortizingRow = false;
                int activeCoupons = 0;

                // change this condition to while currCouponDate <= maturityDate
                int i = 0;
                DateTime endDate;
                endDate = settlementDate; // this should come from either ( compact, loan details or add new loan tab )

                int totalNoOfRows = 0;
                while (endDate < maturityDate)
                {

                    System.Numeric.DayCountBasis d = System.Numeric.DayCountBasis.Actual365;
                    System.Numeric.Frequency f = new System.Numeric.Frequency();
                    if (frequency == "Annual")
                    {
                        f = System.Numeric.Frequency.Annual;
                    }
                    if (frequency == "Semi-Annual")
                    {
                        f = System.Numeric.Frequency.SemiAnnual;
                    }
                    if (frequency == "Quarterly")
                    {
                        f = System.Numeric.Frequency.Quarterly;
                    }

                    endDate = System.Numeric.Financial.CoupNCD(endDate, maturityDate, f, d); // end date is the last coupon settlement date
                    totalNoOfRows++;
                    //DateTime newEndDate = PreviousBusinessDay(endDate);
                    //endDate = newEndDate;

                }
                endDate = settlementDate;
                while (endDate < maturityDate)
                {

                    DataRow dr = dtSchedule.NewRow();
                    dr["ID"] = (i + 1).ToString();

                    // for each column in schedule table			
                    for (int j = 0; j < colsCount; j++)
                    {
                        if (i == 0 && j == 0)
                        {
                            dr["StartDate"] = settlementDate.ToShortDateString();
                        }
                        else if (i > 0 && j == 0)
                        {
                            // start date always equals the end date of previous coupon
                            dr["StartDate"] = endDate.ToShortDateString();
                        }
                        if (i == 0 && j == 1)
                        {
                            //DateTime endDate;
                            //switch (frequency.Trim())
                            //{
                            //    case "Monthly":
                            //        endDate = startDate.AddMonths(1).Date;
                            //        tb.Text = endDate.ToShortDateString();
                            //        dr["EndDate"] = endDate.ToShortDateString();
                            //        break;
                            //    case "Quarterly":
                            //        endDate = startDate.AddMonths(3).Date;
                            //        tb.Text = endDate.ToShortDateString();
                            //        dr["EndDate"] = endDate.ToShortDateString();
                            //        break;
                            //    case "Semi-Annual":
                            //        endDate = startDate.AddMonths(6).Date;
                            //        tb.Text = endDate.ToShortDateString();
                            //        dr["EndDate"] = endDate.ToShortDateString();
                            //        break;
                            //    case "Annual":
                            //        endDate = startDate.AddYears(1).Date;
                            //        tb.Text = endDate.ToShortDateString();
                            //        dr["EndDate"] = endDate.ToShortDateString();
                            //        break;
                            //    default:
                            //        break;
                            //}
                            System.Numeric.DayCountBasis d = System.Numeric.DayCountBasis.Actual365;
                            System.Numeric.Frequency f = new System.Numeric.Frequency();
                            if (frequency == "Annual")
                            {
                                f = System.Numeric.Frequency.Annual;
                            }
                            if (frequency == "Semi-Annual")
                            {
                                f = System.Numeric.Frequency.SemiAnnual;
                            }
                            if (frequency == "Quarterly")
                            {
                                f = System.Numeric.Frequency.Quarterly;
                            }

                            endDate = System.Numeric.Financial.CoupNCD(endDate, maturityDate, f, d); // end date is the last coupon settlement date
                            DateTime newEndDate = PreviousBusinessDay(endDate);
                            dr["EndDate"] = newEndDate.ToShortDateString();
                        }
                        else if (i > 0 && j == 1)
                        {
                            System.Numeric.DayCountBasis d = System.Numeric.DayCountBasis.Actual365;
                            System.Numeric.Frequency f = new System.Numeric.Frequency();
                            if (frequency == "Annual")
                            {
                                f = System.Numeric.Frequency.Annual;
                            }
                            if (frequency == "Semi-Annual")
                            {
                                f = System.Numeric.Frequency.SemiAnnual;
                            }
                            if (frequency == "Quarterly")
                            {
                                f = System.Numeric.Frequency.Quarterly;
                            }

                            endDate = System.Numeric.Financial.CoupNCD(endDate, maturityDate, f, d); // end date is the last coupon settlement date
                            DateTime newEndDate = PreviousBusinessDay(endDate);
                            dr["EndDate"] = newEndDate.ToShortDateString();

                        }




                        // coup frac calc
                        if (j == 2)
                        {

                            if (settlementDate != null) // removed maturityDate
                            {

                                decimal days = (endDate - settlementDate).Days;
                                decimal totalDays = 365;
                                decimal coupFreqDays = days / totalDays;
                                if (days > 0)
                                    dr["CoupFrac"] = Convert.ToDecimal(coupFreqDays).ToString("0.00");
                                else
                                    dr["CoupFrac"] = Convert.ToDecimal(0).ToString("0.00");
                            }
                        }

                        if (j == 3)
                        {
                            if (i == 0)
                            {
                                firstNotionalRow = Convert.ToDecimal(notional);
                                notionalRow = firstNotionalRow;
                            }
                            else
                            {
                                notionalRow = Convert.ToDecimal(Convert.ToDecimal(dtSchedule.Rows[i - 1]["Notation"]) - Convert.ToDecimal(dtSchedule.Rows[i - 1]["Amortisation"]));
                            }
                            dr["Notation"] = notionalRow.ToString("N");
                        }

                        if (j == 4)
                        {

                            //DateTime dt = Convert.ToDateTime(dr["StartDate"]);
                            //if (couponDate != null && dt >= couponDate)
                            //{
                            //    if (firstAmortizingRow == false)
                            //        activeCoupons = rowsCount - i;
                            //    Decimal amortisation = Convert.ToDecimal(firstNotionalRow / activeCoupons);
                            //    dr["Amortisation"] = Convert.ToDecimal(amortisation).ToString("0.00");
                            //    firstAmortizingRow = true;
                            //}
                            //else
                            //    dr["Amortisation"] = "0.00";
                            dr["Amortisation"] = Convert.ToDecimal(Convert.ToDecimal(firstNotionalRow) / totalNoOfRows).ToString("0.00");
                        }

                        if (j == 5)
                        {
                            Decimal factorRow = Convert.ToDecimal(notionalRow / firstNotionalRow);
                            dr["Factor"] = factorRow;
                        }

                        if (j == 6)
                        {
                            dr["Spread"] = Convert.ToDecimal(spread);
                        }

                        if (j == 7)
                        {
                            dr["CouponPaymentDate"] = Convert.ToDateTime(dr["EndDate"]);
                        }

                        if (j == 8)
                        {
                            if (ccy.ToLower() == "eur")
                            {
                                EURCurvesBL eurCurveBL = new EURCurvesBL();
                                List<EURCurve> eurCurve = eurCurveBL.GetEURCurve();
                                Dictionary<double, double> rateVals = new Dictionary<double, double>();
                                foreach (var item in eurCurve)
                                {
                                    rateVals.Add(Convert.ToDouble(item.RateDate.Value.Ticks), Convert.ToDouble(item.Rate));
                                }
                                var scaler = new SplineInterpolator(rateVals);
                                var y = scaler.GetValue(Convert.ToDateTime(dr["EndDate"]).Ticks);
                                dr["RiskFreeDP1"] = Math.Exp(-((y / 100) * (((Convert.ToDateTime(dr["EndDate"]) - Convert.ToDateTime(tradedDate)).Days)) / 365));
                            }
                            else if (ccy.ToLower() == "usd")
                            {
                                USDCurveBL usdCurveBL = new USDCurveBL();
                                List<USDCurve> usdCurve = usdCurveBL.GetUSCurve();
                                Dictionary<double, double> rateVals = new Dictionary<double, double>();
                                foreach (var item in usdCurve)
                                {
                                    rateVals.Add(Convert.ToDouble(item.RateDate.Value.Ticks), Convert.ToDouble(item.Rate));
                                }
                                var scaler = new SplineInterpolator(rateVals);
                                var y = scaler.GetValue(Convert.ToDateTime(dr["EndDate"]).Ticks);
                                dr["RiskFreeDP1"] = Math.Exp(-((y / 100) * (((Convert.ToDateTime(dr["EndDate"]) - Convert.ToDateTime(tradedDate)).Days)) / 365));
                            }
                            //  SplineInterpolator interPolation = new  SplineInterpolator();
                            // dr["RiskFreeDP1"]=  ;//EXP(-(interpolate(schedule.endDate(i),EurDB.Rates,'EurDB.Rates')/100*(schedule.endDate(i)-loan.settlementDate)/365));
                        }
                        if (j == 9)
                        {
                            if (dr["EndDate"].ToString() != string.Empty)
                            {
                                dr["RiskFreeDP2"] = Convert.ToDouble(dr["RiskFreeDP1"]) / Convert.ToDouble((Math.Pow(Convert.ToDouble((1 + (spread / 20000))), ((2 * (Convert.ToDateTime(dr["EndDate"]) - Convert.ToDateTime(tradedDate)).Days) / 360))));
                            }
                        }

                        if (j == 10)
                        {
                            if (i == 0)
                            {
                                dr["FloatingRate"] = 0.5;
                            }
                            else
                            {
                                double d = Convert.ToDouble(dtSchedule.Rows[i - 1]["RiskFreeDP1"]) / Convert.ToDouble(dr["RiskFreeDP1"]) - 1 / ((Convert.ToDateTime(dr["EndDate"]) - Convert.ToDateTime(dtSchedule.Rows[i - 1]["EndDate"])).Days) / 360;
                                dr["FloatingRate"] = Convert.ToDouble(d) / 100;
                            }
                        }
                        if (j == 11)
                        {
                            dr["AllInRate"] = Convert.ToDouble(Convert.ToDouble(dr["FloatingRate"]) + Convert.ToDouble(spread)) / 100;
                        }
                        if (j == 12)
                        {
                            dr["Interest"] = ((Convert.ToDateTime(dr["EndDate"]) - Convert.ToDateTime(dr["StartDate"])).Days / 360) * Convert.ToDouble(dr["AllInRate"]) * Convert.ToDouble(dr["Notation"]);
                        }
                        if (j == 13)
                        {
                            dr["Days"] = (Convert.ToDateTime(dr["EndDate"]) - Convert.ToDateTime(dr["StartDate"])).Days;
                        }
                        if (j == 14)
                        {
                            dr["AmortisationInt"] = Convert.ToDouble(dr["Amortisation"]) + Convert.ToDouble(dr["Interest"]);
                        }

                    }
                    if (Convert.ToDecimal(dr[4]) >= 0)
                    {
                        dtSchedule.Rows.Add(dr);
                        i++;
                    }
                }
                //if ( dtSchedule.Rows.Count > 0)
                //{
                //    for (int x = 0; x < dtSchedule.Rows.Count; x++)
                //    {
                //        dtSchedule.Rows[x][5] = Convert.ToDecimal(Convert.ToDecimal(notional) / (dtSchedule.Rows.Count)).ToString("0.00");
                //        if (x == 0)
                //        {
                //            firstNotionalRow = Convert.ToDecimal(notional);
                //            notionalRow = firstNotionalRow;
                //        }
                //        else
                //        {
                //            notionalRow = Math.Round(Convert.ToDecimal(Math.Round(Convert.ToDecimal(dtSchedule.Rows[x - 1]["Notation"]), 2) - Convert.ToDecimal(dtSchedule.Rows[x - 1]["Amortisation"])), 2);
                //        }
                //        dtSchedule.Rows[x]["Notation"] = notionalRow.ToString("N");

                //    }
                //}

               

            }
            catch (Exception ex)
            {


            }
            return dtSchedule;
        }


        public static DateTime PreviousBusinessDay(DateTime today)
        {
            DateTime result;
            switch (today.DayOfWeek)
            {
                case DayOfWeek.Sunday:
                    result = today.AddDays(-2);
                    break;

                case DayOfWeek.Monday:
                    result = today.AddDays(-3);
                    break;

                case DayOfWeek.Tuesday:
                case DayOfWeek.Wednesday:
                case DayOfWeek.Thursday:
                case DayOfWeek.Friday:
                    result = today.AddDays(-1);
                    break;

                case DayOfWeek.Saturday:
                    result = today.AddDays(-1);
                    break;

                default:
                    throw new ArgumentOutOfRangeException("DayOfWeek=" + today.DayOfWeek);
            }
            return ScreenHolidays(result, -1);
        }

        public static DateTime ScreenHolidays(DateTime result, int addValue)
        {
            #region Sanity Checks
            if ((addValue != -1) && (addValue != 1))
                throw new ArgumentOutOfRangeException("addValue must be -1 or 1");
            #endregion

            // holidays on fixed date
            switch (MonthDay(result))
            {
                case "01/01":  // Happy New Year
                case "07/04":  // Independent Day
                case "12/25":  // Christmas
                    return GetBusinessDay(result, addValue);
                default:
                    return result;
            }
        }

        public static string MonthDay(DateTime time)
        {
            return String.Format("{0:00}/{1:00}", time.Month, time.Day);
        }

        public static DateTime GetBusinessDay(DateTime today, int addValue)
        {
            #region Sanity Checks
            if ((addValue != -1) && (addValue != 1))
                throw new ArgumentOutOfRangeException("addValue must be -1 or 1");
            #endregion

            if (addValue > 0)
                return NextBusinessDay(today);
            else
                return PreviousBusinessDay(today);
        }

        public static DateTime NextBusinessDay(DateTime today)
        {
            DateTime result;
            switch (today.DayOfWeek)
            {
                case DayOfWeek.Sunday:
                case DayOfWeek.Monday:
                case DayOfWeek.Tuesday:
                case DayOfWeek.Wednesday:
                case DayOfWeek.Thursday:
                    result = today.AddDays(1);
                    break;

                case DayOfWeek.Friday:
                    result = today.AddDays(3);
                    break;

                case DayOfWeek.Saturday:
                    result = today.AddDays(2);
                    break;

                default:
                    throw new ArgumentOutOfRangeException("DayOfWeek=" + today.DayOfWeek);
            }
            return ScreenHolidays(result, 1);
        }
        private bool IsWeekend(DateTime date)
        {
            return date.DayOfWeek == DayOfWeek.Saturday ||
                   date.DayOfWeek == DayOfWeek.Sunday;
        }

    }
        #endregion



}

