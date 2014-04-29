using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Drawing;
using System.Text;
using System.ComponentModel;
using BLL;
using DAL;
using System.IO;
using System.Reflection;
using System.Data;
using Microsoft.AspNet.SignalR;
using Telerik.Web.UI;
using System.Globalization;
using Telerik.Charting;
using Telerik.Web.UI.HtmlChart;
using Telerik.Charting.Styles;
using System.Web.Services;
using Telerik.Web.UI.PersistenceFramework;
using Microsoft.VisualBasic;

using LoanPricerWebBased.Helpers;
using Zainco.NewtonRaphson.IRRCalculator.Domain;
using Telerik.Web.UI.HtmlChart.Enums;


namespace LoanPricerWebBased
{
    public class Browser
    {
        public Browser(string name, double marketShare, bool isExploded)
        {
            _name = name;
            _marketShare = marketShare;
            _isExploded = isExploded;
        }
        private string _name;
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }
        private double _marketShare;
        public double MarketShare
        {
            get { return _marketShare; }
            set { _marketShare = value; }
        }
        private bool _isExploded;
        public bool IsExploded
        {
            get { return _isExploded; }
            set { _isExploded = value; }
        }
    }
    public partial class Default : BasePage
    {
        //private static List<CalculatedDates> calculatedList = new List<CalculatedDates>();
        private static List<CalculatedDates> tempCalculatedList1 = new List<CalculatedDates>();
        private static List<CalculatedDates> tempCalculatedList2 = new List<CalculatedDates>();
        private static List<CalculatedDates> tempCalculatedList3 = new List<CalculatedDates>();
        private static string _operation = "Add";
        private static DateTime timeStamp;
        private static int firstTime = 0;
        private static DataTable dt1 = new DataTable();
        private bool firstAmortizingRow = false;
        private static int activeCoupons;

        protected void Page_Init(object sender, EventArgs e)
        {
            RadPersistenceManager1.StorageProvider = new SessionStorageProvider();
        }

        private void BindPieChart()
        {
            List<Browser> browsers = new List<Browser>();

            LoansBLL loansBLL = new LoansBLL();
            DataSet ds = new DataSet();
            ds = loansBLL.GetLoanCountryGraphDetails();
            if (ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    browsers.Add(new Browser(dr["Country"].ToString(), Convert.ToDouble(dr["Percentage"]), false));
                }
            }
            PieSeries ps = new PieSeries();
            foreach (var item in browsers)
            {
                SeriesItem s1 = new SeriesItem();
                s1.Name = item.Name;
                s1.Exploded = item.IsExploded;
                s1.YValue = Convert.ToDecimal(item.MarketShare);
                ps.Items.Add(s1);
            }

            //ps.LabelsAppearance.Position = Telerik.Web.UI.HtmlChart.PieLabelsPosition.Column;
            // removed by Nik 20 03 after upgradation
            //  ps.LabelsAppearance.Position = Telerik.Web.UI.HtmlChart.PieLabelsPosition.Column;
            PieChart1.PlotArea.Series.Add(ps);
            PieChart1.DataBind();
            BindQuotesAndTradesChart();
        }

        private void BindQuotesAndTradesChart()
        {
            List<Browser> browsers = new List<Browser>();

            LoansBLL loansBLL = new LoansBLL();
            DataSet ds = new DataSet();
            ds = loansBLL.GetLoanCountryGraphDetails();
            if (ds.Tables[1].Rows.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[1].Rows)
                {
                    browsers.Add(new Browser("Trades", Convert.ToDouble(dr["Trades"]), false));
                    browsers.Add(new Browser("Quotes", Convert.ToDouble(dr["Quotes"]), false));
                }


            }
            PieSeries ps = new PieSeries();
            foreach (var item in browsers)
            {
                SeriesItem s1 = new SeriesItem();
                s1.Name = item.Name;
                s1.Exploded = item.IsExploded;
                s1.YValue = Convert.ToDecimal(item.MarketShare);
                ps.Items.Add(s1);
            }

            // ps.LabelsAppearance.Position = Telerik.Web.UI.HtmlChart.PieLabelsPosition.Column;
            // removed by Nik 20 03 after upgrade version
            //  ps.LabelsAppearance.Position = Telerik.Web.UI.HtmlChart.PieLabelsPosition.Column;

            PieChart2.PlotArea.Series.Add(ps);
            PieChart2.DataBind();
            BindNuberofCounterPartyChar();
        }

        private void BindNuberofCounterPartyChar()
        {
            List<Browser> browsers = new List<Browser>();

            LoansBLL loansBLL = new LoansBLL();
            DataSet ds = new DataSet();
            ds = loansBLL.GetLoanCountryGraphDetails();
            if (ds.Tables[2].Rows.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[2].Rows)
                {
                    browsers.Add(new Browser(dr["CounterParty"].ToString(), Convert.ToDouble(dr["TotalNo"]), false));
                }
            }
            PieSeries ps = new PieSeries();
            foreach (var item in browsers)
            {
                SeriesItem s1 = new SeriesItem();
                s1.Name = item.Name;
                s1.Exploded = item.IsExploded;
                s1.YValue = Convert.ToDecimal(item.MarketShare);
                ps.Items.Add(s1);
            }

            //    ps.LabelsAppearance.Position = Telerik.Web.UI.HtmlChart.PieLabelsPosition.Column;
            // removed by nik 20 03 after version upgrade
            PieChart3.PlotArea.Series.Add(ps);
            PieChart3.DataBind();
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (firstTime == 0)
                {
                    LogActivity("Dashboard Page", "Session Activity Start", string.Empty);
                    timeStamp = DateTime.Now; firstTime++;
                }
                if (DateTime.Now.Subtract(timeStamp).TotalMinutes <= 60)
                {
                    try
                    {
                        BindPieChart();


                    }
                    catch (Exception ex)
                    {

                        RadWindowManager1.RadAlert(ex.Message, 330, 180, "realedge associates", "alertCallBackFn");
                    }

                    LogActivity("Dashboard Page", "On Page Load In a Particular Session", string.Empty);
                    //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "getSelected", "function getSelected(){__doPostBack(\"" + btnHidden.UniqueID + "\",\"\");}", true);
                    if (Request.QueryString["flag"] != null)
                    { InsertRecord(Request.Params[1].ToString(), Convert.ToInt16(Request.QueryString["flag"])); return; }

                    if (!IsPostBack)
                    {
                        BindLoanDetails();
                        if (Session["LoanDetail"] != null)
                        {
                            ddlLoanDetailsCode.SelectedValue = Session["LoanDetail"].ToString();
                            BindLoanDetailData(Session["LoanDetail"].ToString());
                        }
                        BindLoansData();
                        LogActivity("Dashboard Page", "On Post back of Page Load", string.Empty);
                        Session.Timeout = 60;
                        timeStamp = DateTime.Now;
                        firstTime = 0;
                        firstTime++;
                        dt1.Clear();
                        dt1 = new DataTable();
                        dt1.Columns.Add("ID");
                        dt1.Columns.Add("StartDate");
                        dt1.Columns.Add("EndDate");
                        dt1.Columns.Add("CoupFrac");
                        dt1.Columns.Add("Notation");
                        dt1.Columns.Add("Amortisation");
                        dt1.Columns.Add("Factor");

                        dt1.Columns.Add("Spread");
                        dt1.Columns.Add("CouponPaymentDate");
                        dt1.Columns.Add("RiskFreeDP1");
                        dt1.Columns.Add("RiskFreeDP2");
                        dt1.Columns.Add("FloatingRate");
                        dt1.Columns.Add("AllInRate");
                        dt1.Columns.Add("Interest");
                        dt1.Columns.Add("Days");
                        dt1.Columns.Add("AmortisationInt");

                        BindLoanAData(); BindLoanBData(); BindLoanCData(); BindLoanCode(); BindCounterParty();
                        txtLoanNameA.Filter = (RadComboBoxFilter)Convert.ToInt32(2);
                        txtLoanNameB.Filter = (RadComboBoxFilter)Convert.ToInt32(2);
                        txtLoanNameC.Filter = (RadComboBoxFilter)Convert.ToInt32(2);

                        ddlCountry.Filter = (RadComboBoxFilter)Convert.ToInt32(2);
                        ddlAddLoanCode.Filter = (RadComboBoxFilter)Convert.ToInt32(2);

                        ddlQuotesLoanName.Filter = (RadComboBoxFilter)Convert.ToInt32(2);
                        ddlQuoteCountry.Filter = (RadComboBoxFilter)Convert.ToInt32(2);
                        ddlBorrower.Filter = (RadComboBoxFilter)Convert.ToInt32(2);
                        //ddlRegionB.Filter = (RadComboBoxFilter)Convert.ToInt32(2);
                        //ddlRegionC.Filter = (RadComboBoxFilter)Convert.ToInt32(2);

                        BindRegion();

                        txtBoxTradeDate1.SelectedDate = DateTime.Now;
                        txtBoxTradeDate2.SelectedDate = DateTime.Now;
                        txtBoxTradeDate3.SelectedDate = DateTime.Now;


                        txtBoxSettlementDate1.SelectedDate = AddBusinessDays(txtBoxTradeDate2.SelectedDate.Value, 10);
                        txtBoxSettlementDate2.SelectedDate = AddBusinessDays(txtBoxTradeDate2.SelectedDate.Value, 10);
                        txtBoxSettlementDate3.SelectedDate = AddBusinessDays(txtBoxTradeDate2.SelectedDate.Value, 10);
                        BindCreditRatingTree();

                        //GetCompletionList();
                        string queryString = Path.GetFileName(Request.Url.AbsoluteUri);
                        if (Session["LoanType"] != null && !queryString.Contains("?"))
                        {
                            string loanType = Session["LoanType"].ToString();
                            string loanNameA = string.Empty;
                            string loanNameB = string.Empty;
                            string loanNameC = string.Empty;
                            RadDatePicker dt = new RadDatePicker();
                            dt.SelectedDate = DateTime.Now.Date;
                            if (Session["LoanNameA"] != null)
                            {
                                loanNameA = Session["LoanNameA"].ToString();
                                SetLoanA(loanNameA, dt);
                            }
                            if (Session["LoanNameB"] != null)
                            {
                                loanNameB = Session["LoanNameB"].ToString();
                                SetLoanB(loanNameB, dt);
                            }
                            if (Session["LoanNameC"] != null)
                            {
                                loanNameC = Session["LoanNameC"].ToString();
                                SetLoanC(loanNameC, dt);

                            }
                            // RadDatePicker dt = (RadDatePicker) Session["BoxTradeDate"];
                        }




                        BindChartData();

                        string urlWithSessionID = Response.ApplyAppPathModifier(Request.Url.PathAndQuery);
                        RadTab tab = RadTabStrip1.FindTabByUrl(urlWithSessionID);
                        if (tab != null)
                        {
                            hdnSaved.Value = "N";
                            tab.SelectParents();
                            tab.PageView.Selected = true;
                        }
                        Sorting();
                        DupicateSorting();
                        if (Session["LogedInUser"] != null && (Session["LogedInUser"] as DAL.Login) != null)
                        {
                        }
                        else
                        {
                            // Move back to the login page
                            Response.Redirect("~/Banner.aspx");
                        }
                        BindBorrower(); BindCurrency();
                        // txtBoxTradeDate1.SelectedDate = DateTime.Now.Date;

                        txtBoxNotional1.Text = Convert.ToDecimal(10000000).ToString("N");
                        txtBoxNotional2.Text = Convert.ToDecimal(10000000).ToString("N");
                        txtBoxNotional3.Text = Convert.ToDecimal(10000000).ToString("N");
                        txtNotional.Text = Convert.ToDecimal(10000000).ToString("N");
                        //  txtBoxFacilitySize.Text = Convert.ToDecimal(10000000).ToString("N");
                        //txtBoxCounterPartyA.Text = "CounteryPartyA";
                        //txtBoxCounterPartyB.Text = "CounteryPartyB";
                        //txtBoxCounterPartyC.Text = "CounteryPartyC";

                        //BindQuotesAndTrades();
                        //   txtBoxFacilitySize.Text = Convert.ToDecimal(10000000).ToString("N");

                        BindLoansTab();

                        BindHistoricalQuotesAndTradesTab();

                        bindCountry();
                        ddlCountry.SelectedValue = "Russia";

                        BindQuotesAndGridsFilterSettings();

                        string str = HttpContext.Current.Request.RawUrl;
                        if (str == "/default.aspx?page=addstaticloan" && Session["EditLoanID"] != null)
                            FillData();
                        else
                            Clear();

                        string strQuoteTrade = HttpContext.Current.Request.RawUrl;
                        if (str == "/default.aspx?page=edithistoricalquotes" && Session["EditQuoteID"] != null)
                            FillQuoteData();
                        else
                            ClearQuotes();
                        if (ddlAmortizing.SelectedValue == "No")
                        {
                            trNo.Visible = false;
                            trDate.Visible = false;
                            tblAmortisation.Visible = false;
                            dt1.Clear();
                            btnCalculatSchedule.Visible = false;
                            grdAmortizing.Visible = false;
                            pnlAmortizing.Visible = false;
                        }

                        LogActivity("Dashboard Page", "Page Load Complete", string.Empty);
                    }
                }
                else
                {
                    firstTime = 0;
                    timeStamp = DateTime.Now;
                    Response.Redirect("Logout.aspx");
                }
            }
            catch (Exception ex)
            {
                LogActivity("Dashboard Page", "Page Load Error : " + ex.Message, ex.Message);
            }
        }


        private void BindBorrower()
        {
            ddlBorrower.DataSource = new LoansBLL().GetBorrower();
            ddlBorrower.DataValueField = "Name";
            ddlBorrower.DataTextField = "Name";
            ddlBorrower.DataBind();
        }

        private void BindRegion()
        {
            try
            {
                //ddlRegionA.DataSource = new CountryBL().GetCountries();
                //ddlRegionA.DataValueField = "ID";
                //ddlRegionA.DataTextField = "Name";
                //ddlRegionA.DataBind();
                //ddlRegionA.SelectedValue = "250";

                //ddlRegionB.DataSource = new CountryBL().GetCountries();
                //ddlRegionB.DataValueField = "ID";
                //ddlRegionB.DataTextField = "Name";
                //ddlRegionB.DataBind();
                //ddlRegionB.SelectedValue = "250";

                //ddlRegionC.DataSource = new CountryBL().GetCountries();
                //ddlRegionC.DataValueField = "ID";
                //ddlRegionC.DataTextField = "Name";
                //ddlRegionC.DataBind();
                //ddlRegionC.SelectedValue = "250";
            }
            catch (Exception)
            {


            }

        }
        private void BindLoanAData()
        {
            LoansBLL loanBL = new LoansBLL();
            txtLoanNameA.DataSource = loanBL.BindLoanAData();
            txtLoanNameA.DataTextField = "CodeName";
            txtLoanNameA.DataValueField = "CodeName";
            txtLoanNameA.DataBind();
        }
        private void BindLoanBData()
        {
            LoansBLL loanBL = new LoansBLL();
            txtLoanNameB.DataSource = loanBL.BindLoanAData();
            txtLoanNameB.DataTextField = "CodeName";
            txtLoanNameB.DataValueField = "CodeName";

            txtLoanNameB.DataBind();
        }
        private void BindLoanCData()
        {
            LoansBLL loanBL = new LoansBLL();
            txtLoanNameC.DataSource = loanBL.BindLoanAData();
            txtLoanNameC.DataTextField = "CodeName";
            txtLoanNameC.DataValueField = "CodeName";
            txtLoanNameC.DataBind();
        }
        private void BindCounterParty()
        {
            CounterPartyBL counterPartyBL = new CounterPartyBL();
            ddlCounterPartyA.DataSource = counterPartyBL.GetCounterParty();
            ddlCounterPartyA.DataTextField = "Name";
            ddlCounterPartyA.DataValueField = "Name";
            ddlCounterPartyA.DataBind();

            ddlCounterPartyB.DataSource = counterPartyBL.GetCounterParty();
            ddlCounterPartyB.DataTextField = "Name";
            ddlCounterPartyB.DataValueField = "Name";
            ddlCounterPartyB.DataBind();

            ddlCounterPartyC.DataSource = counterPartyBL.GetCounterParty();
            ddlCounterPartyC.DataTextField = "Name";
            ddlCounterPartyC.DataValueField = "Name";
            ddlCounterPartyC.DataBind();

            ddlCounterPartyA.SelectedValue = "CounterPartyA";
            ddlCounterPartyB.SelectedValue = "CounterPartyB";
            ddlCounterPartyC.SelectedValue = "CounterPartyC";
        }
        private void BindLoanCode()
        {
            LoansBLL loanBL = new LoansBLL();
            ddlAddLoanCode.DataSource = loanBL.BindLoanAData();
            ddlAddLoanCode.DataTextField = "CodeName";
            ddlAddLoanCode.DataValueField = "CodeName";
            ddlAddLoanCode.DataBind();

            ddlQuotesLoanName.DataSource = loanBL.BindLoanAData();
            ddlQuotesLoanName.DataTextField = "CodeName";
            ddlQuotesLoanName.DataValueField = "CodeName";
            ddlQuotesLoanName.DataBind();


        }
        private void BindChartData()
        {
            try
            {
                if (Session["LegendA"] != null)
                {
                    //   lblLegend.Text = "Cashflow for " + Session["LegendA"].ToString();
                    // Session.Remove("Legend");
                }
                if (Session["LegendB"] != null)
                {
                    //if (lblLegend.Text.Length == 0)
                    //    lblLegend.Text = "Cashflow for " + Session["LegendB"].ToString();
                    //else
                    //    lblLegend.Text = lblLegend.Text + "And " + Session["LegendB"].ToString();
                }
                if (Session["LegendC"] != null)
                {
                    //if (lblLegend.Text.Length == 0)
                    //    lblLegend.Text = "Cashflow for " + Session["LegendC"].ToString();
                    //else
                    //    lblLegend.Text = lblLegend.Text + "And " + Session["LegendC"].ToString();
                }

                DataTable dt = new DataTable();



                dt.Columns.Add("ID");
                dt.Columns.Add("EndDate");
                dt.Columns.Add("Notation");

                if (Session["LegendA"] != null)
                {
                    dt = GetCompactChartData(Session["LegendA"].ToString());
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        //        RadHtmlChart1.HttpHandlerUrl = ResolveUrl("ChartImage.axd");

                        AreaSeries chartLoanA = new AreaSeries();

                        chartLoanA.Name = Session["LegendA"].ToString();

                        object maxDate = dt.Compute("MAX(EndDate)", null);
                        // chartLoanA.PlotArea.XAxis.MaxValue = Convert.ToDouble(Convert.ToDateTime(maxDate).ToOADate());

                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            var dr = (dt.Rows[i]).ItemArray;
                            SeriesItem item = new SeriesItem();

                            if (dr[1] != null && dr[2] != null)
                            {
                                CategorySeriesItem seriesItem = new CategorySeriesItem();
                                //item.ActiveRegion.Tooltip = "On Date" + Convert.ToDateTime(dr[1]) + "Fraction Value is" + Convert.ToDouble(dr[2]);
                                AxisItem axisItem = new AxisItem();
                                DateTime date = Convert.ToDateTime(dr[1]);
                                item.XValue = date.Ticks;
                                //axisItem.LabelText = 
                                //RadHtmlChart1.PlotArea.XAxis.LabelsAppearance.RotationAngle = 60;
                                string month = date.Date.Month.ToString();
                                if (month.Length == 1)
                                {
                                    month = "0" + month;
                                }

                                string str = month + date.Date.Year.ToString();
                                RadHtmlChart1.PlotArea.XAxis.Items.Add(str);
                                item.YValue = Convert.ToDecimal(dr[2].ToString());
                                //chartLoanA.Items.Add(item);
                                seriesItem.Y = Convert.ToDecimal(dr[2].ToString());
                                chartLoanA.SeriesItems.Add(seriesItem);

                            }

                        }


                        chartLoanA.LabelsAppearance.Visible = false;
                        chartLoanA.LineAppearance.LineStyle = ExtendedLineStyle.Step;
                        RadHtmlChart1.PlotArea.Series.Add(chartLoanA);

                    }
                }


                if (Session["LegendB"] != null)
                {
                    dt = GetCompactChartData(Session["LegendB"].ToString());
                    if (dt != null && dt.Rows.Count > 0)
                    {

                        AreaSeries chartLoanB = new AreaSeries();

                        chartLoanB.Name = Session["LegendB"].ToString();

                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            var dr = (dt.Rows[i]).ItemArray;
                            SeriesItem item = new SeriesItem();

                            if (dr[1] != null && dr[2] != null)
                            {
                                CategorySeriesItem seriesItem = new CategorySeriesItem();
                                DateTime date = Convert.ToDateTime(dr[1]);
                                //item.XValue = date.ToOADate();
                                //item.YValue = Convert.ToDouble(dr[2]);
                                item.YValue = Convert.ToDecimal(dr[2].ToString());
                                //chartLoanA.Items.Add(item);
                                string month = date.Date.Month.ToString();
                                if (month.Length == 1)
                                {
                                    month = "0" + month;
                                }

                                string str = month + date.Date.Year.ToString();
                                RadHtmlChart1.PlotArea.XAxis.Items.Add(str);
                                seriesItem.Y = Convert.ToDecimal(dr[2].ToString());
                                chartLoanB.SeriesItems.Add(seriesItem);
                            }

                        }
                        chartLoanB.LabelsAppearance.Visible = false;
                        chartLoanB.LineAppearance.LineStyle = ExtendedLineStyle.Step;
                        RadHtmlChart1.PlotArea.Series.Add(chartLoanB);
                    }
                }

                if (Session["LegendC"] != null)
                {
                    dt = GetCompactChartData(Session["LegendC"].ToString());
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        AreaSeries chartLoanC = new AreaSeries();

                        chartLoanC.Name = Session["LegendC"].ToString();

                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            var dr = (dt.Rows[i]).ItemArray;
                            SeriesItem item = new SeriesItem();
                            if (dr[1] != null && dr[2] != null)
                            {
                                CategorySeriesItem seriesItem = new CategorySeriesItem();
                                DateTime date = Convert.ToDateTime(dr[1]);
                                //item.XValue = date.ToOADate();
                                //item.YValue = Convert.ToDouble(dr[2]);
                                item.YValue = Convert.ToDecimal(dr[2].ToString());
                                //chartLoanA.Items.Add(item);
                                string month = date.Date.Month.ToString();
                                if (month.Length == 1)
                                {
                                    month = "0" + month;
                                }

                                string str = month + date.Date.Year.ToString();
                                RadHtmlChart1.PlotArea.XAxis.Items.Add(str);
                                seriesItem.Y = Convert.ToDecimal(dr[2].ToString());
                                chartLoanC.SeriesItems.Add(seriesItem);

                            }
                        }

                        chartLoanC.LabelsAppearance.Visible = false;
                        chartLoanC.LineAppearance.LineStyle = ExtendedLineStyle.Step;
                        RadHtmlChart1.PlotArea.Series.Add(chartLoanC);
                    }
                }
            }
            //RadHtmlChart1.DataSource = GetData();
            //RadHtmlChart1.DataBind();
            // tempCalculatedList = null;

            catch (Exception)
            {

            }
        }

        private void BindCurrency()
        {
            CurrenciesBL currencyBL = new CurrenciesBL();
            ddlAddCurrency.DataSource = currencyBL.GetCurrency();
            ddlAddCurrency.DataValueField = "Currancy";
            ddlAddCurrency.DataTextField = "Currancy";
            ddlAddCurrency.DataBind();
        }

        protected void RadHtmlChart1_ItemDataBound(object sender, ChartItemDataBoundEventArgs e)
        {

            e.SeriesItem.ActiveRegion.Tooltip += "On Date" + e.SeriesItem.XValue + "Fraction Value is" + e.SeriesItem.YValue;


            //  e.SeriesItem.ActiveRegion.Tooltip += ((DataRowView)e.DataItem)["Measurement"].ToString() + ": Temperature: " + e.SeriesItem.YValue; 
        }
        private void BindChartNew()
        {
            //RadHtmlChart1.DefaultView.ChartTitle.Content = "Year 2009";
            //RadHtmlChart1.DefaultView.ChartLegend.Header = "Legend";
            //RadHtmlChart1.DefaultView.ChartLegend.UseAutoGeneratedItems = true;
            ////Axis X
            //RadHtmlChart1.DefaultView.ChartArea.AxisX.Title = "Month";
            //RadHtmlChart1.DefaultView.ChartArea.AxisX.AutoRange = false;
            //RadHtmlChart1.DefaultView.ChartArea.AxisX.MinValue = 1;
            //RadHtmlChart1.DefaultView.ChartArea.AxisX.MaxValue = 12;
            //RadHtmlChart1.DefaultView.ChartArea.AxisX.Step = 1;
            //RadHtmlChart1.DefaultView.ChartArea.AxisX.LayoutMode = AxisLayoutMode.Between;
            ////Axis Y
            //RadHtmlChart1.DefaultView.ChartArea.AxisY.Title = "Quantity";
            //DataSeries series = new DataSeries();
            //series.Definition = new LineSeriesDefinition();
            //series.LegendLabel = "Product Sales";
            //series.Add(new DataPoint(1, 154));
            //series.Add(new DataPoint(2, 138));
            //series.Add(new DataPoint(3, 143));
            //series.Add(new DataPoint(4, 120));
            //series.Add(new DataPoint(5, 135));
            //series.Add(new DataPoint(6, 125));
            //series.Add(new DataPoint(7, 179));
            //series.Add(new DataPoint(8, 170));
            //series.Add(new DataPoint(9, 198));
            //series.Add(new DataPoint(10, 187));
            //series.Add(new DataPoint(11, 193));
            //series.Add(new DataPoint(12, 212));
            //radChart.DefaultView.ChartArea.DataSeries.Add(series);
        }

        protected DataTable GetCompactChartData(string loanName)
        {
            try
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("ID");
                dt.Columns.Add("EndDate");
                dt.Columns.Add("Notation");

                LoansBLL loanBL = new LoansBLL();
                int loanID = loanBL.GetLoanByCode(loanName).ID;
                LoanScheduleBL loanScheduleBL = new LoanScheduleBL();
                List<LoanSchedule> loanSchedule = loanScheduleBL.GetLoanByID(loanID);

                if (loanSchedule != null)
                {
                    for (int i = 0; i < loanSchedule.Count; i++)
                    {
                        dt.Rows.Add((i + 1), new DateTime(loanSchedule[i].EndDate.Value.Year, loanSchedule[i].EndDate.Value.Month, loanSchedule[i].EndDate.Value.Day), loanSchedule[i].Notation.ToString());
                    }
                }
                return dt;
            }
            catch (Exception)
            {
                return null;
            }
        }

        protected DataTable GetData(List<CalculatedDates> tempCalculatedList)
        {
            try
            {


                DataTable dt = new DataTable();

                dt.Columns.Add("ID");
                dt.Columns.Add("CalculatedDate");
                dt.Columns.Add("Fraction");

                if (tempCalculatedList != null)
                {
                    for (int i = 0; i < tempCalculatedList.Count; i++)
                    {
                        dt.Rows.Add((i + 1), new DateTime(tempCalculatedList[i].CalculatedDate.Year, tempCalculatedList[i].CalculatedDate.Month, tempCalculatedList[i].CalculatedDate.Day), tempCalculatedList[i].Fraction.ToString());
                    }
                }

                return dt;
            }
            catch (Exception)
            {

                return null;
            }
        }

        private void BindLoansTab(List<Loans> lst = null)
        {

            if (lst == null)
            {
                LoansBLL bll = new LoansBLL();
                lst = bll.GetLoans();
            }

            //grdLoans.DataSource = lst;
            //grdLoans.DataBind();
        }

        public List<Loans> BindLoansData()
        {
            LoansBLL bll = new LoansBLL();
            List<Loans> lst = bll.GetLoans();
            PopulateLoanGrid = lst;
            return lst;
        }
        public List<Loans> PopulateLoanGrid
        {
            get
            {
                List<Loans> data;
                if (Session["LoanGridData"] == null)
                {
                    data = BindLoansData();
                    Session["LoanGridData"] = data;
                }
                data = (List<Loans>)Session["LoanGridData"];
                return data;
            }
            set
            {
                Session["LoanGridData"] = value;
            }
        }

        private void Sorting()
        {
            GridSortExpression sortExpr = new GridSortExpression();
            sortExpr.FieldName = "ID";
            sortExpr.SortOrder = GridSortOrder.Descending;
            grdQuotesAndTrades.MasterTableView.SortExpressions.AddSortExpression(sortExpr);

            sortExpr = new GridSortExpression();
            sortExpr.FieldName = "CodeName";
            sortExpr.SortOrder = GridSortOrder.Ascending;
            grdLoans.MasterTableView.SortExpressions.AddSortExpression(sortExpr);

        }

        private void DupicateSorting()
        {
            GridSortExpression sortExpr = new GridSortExpression();
            sortExpr.FieldName = "LoanName";
            sortExpr.SortOrder = GridSortOrder.Ascending;
            grdQuotesAndTrades.MasterTableView.SortExpressions.AddSortExpression(sortExpr);



        }
        protected void grdQuotesAndTrades_SortCommand(object sender, GridSortCommandEventArgs e)
        {
            GridSortExpression sortExpr = new GridSortExpression();
            sortExpr.FieldName = e.SortExpression;
            sortExpr.SortOrder = GridSortOrder.Ascending;

            e.Item.OwnerTableView.SortExpressions.AddSortExpression(sortExpr);
        }
        private static string strLoanSort = "ASC";
        private static string strDuplicateLoanSort = "ASC";
        protected void grdLoans_SortCommand(object sender, GridSortCommandEventArgs e)
        {
            // GridTableView tableView = e.Item.OwnerTableView;
            GridSortExpression sortExpr = new GridSortExpression();
            sortExpr.FieldName = e.SortExpression;

            if (strLoanSort == "ASC")
            {
                sortExpr.SortOrder = GridSortOrder.Descending;
                strLoanSort = "DESC";
            }
            else
            {
                sortExpr.SortOrder = GridSortOrder.Ascending;
                strLoanSort = "ASC";
            }
            // sortExpr.SortOrder = GridSortOrder.Ascending;
            e.Item.OwnerTableView.SortExpressions.AddSortExpression(sortExpr);
            //sortExpr.SortOrder = GridSortOrder.Ascending;

        }

        private void BindHistoricalQuotesAndTradesTab(List<QuotesAndTrades> lst = null)
        {
            if (lst == null)
            {
                QuotesAndTradesBLL bll = new QuotesAndTradesBLL();
                lst = bll.GetQuotesAndTrades();
            }

            grdQuotesAndTrades.DataSource = lst;
            grdQuotesAndTrades.DataBind();
        }

        protected void btnHidden_Click(object sender, EventArgs e)
        {
            if (hfMode.Value == "0")
            {
                SetLoan0();
                LogActivity("Search Loan Name in Loan tab", "Search the loan in the loan tab", string.Empty);
            }
            else if (hfMode.Value == "a")
            {
                SetLoanA();
                LogActivity("Search Loan Name A", "Search the loan in the compact view A", string.Empty);
            }
            else if (hfMode.Value == "b")
            {
                SetLoanB();
                LogActivity("Search Loan Name B", "Search the loan in the compact view B", string.Empty);
            }
            else if (hfMode.Value == "c")
            {
                SetLoanC();
                LogActivity("Search Loan Name C", "Search the loan in the compact view C", string.Empty);
            }
        }

        private void SetLoan0()
        {
            //Fill up the detail now
            LoansBLL bll = new LoansBLL();
            Loans loan = bll.GetLoanByCode(txtBoxAddLoanCode.Text);

            hfLoanID.Value = loan.ID.ToString();
            ddlBorrower.SelectedValue = loan.Borrower;
            // txtBoxAddSector.Text = loan.Sector;
            ddlSector.SelectedValue = loan.Sector;
            txtBoxAddSigningDate.SelectedDate = Convert.ToDateTime(loan.Signing_Date);
            txtBoxAddMaturityDate.SelectedDate = Convert.ToDateTime(loan.Maturity_Date);
            DropDownListItem item = ddlAddFixedOrFloating.FindItemByValue(loan.FixedOrFloating);
            if (item != null)
            {
                item.Selected = true;
            }
            txtBoxAddMargin.Text = loan.Margin;
            DropDownListItem currencyItem = ddlAddCurrency.FindItemByValue(loan.Currency);
            if (currencyItem != null)
            {
                currencyItem.Selected = true;
            }
            DropDownListItem frequency = ddlAddCouponFrequency.FindItemByValue(loan.CouponFrequency);
            if (frequency != null)
            {
                frequency.Selected = true;
            }
            txtBoxFacilitySize.Text = Convert.ToDecimal(loan.FacilitySize).ToString("N");
            DropDownListItem yesno;
            if (loan.Bilateral.HasValue && loan.Bilateral.Value)
            {
                yesno = ddlAddBilateral.FindItemByValue("Yes");
            }
            else
            {
                yesno = ddlAddBilateral.FindItemByValue("No");
            }
            yesno.Selected = true;
            DropDownListItem amort = ddlAmortizing.FindItemByValue(loan.Amortizing);
            if (amort != null)
            {
                amort.Selected = true;
            }
        }

        private void SetLoanA()
        {
            //Fill up the detail now
            try
            {


                LoansBLL bll = new LoansBLL();
                string strLoanName = txtLoanNameA.Text.ToString();
                txtBoxTradeDate1.SelectedDate = DateTime.Now;
                if (strLoanName.Contains(';'))
                {
                    strLoanName = strLoanName.Replace(';', ' ');
                }
                Loans loan = bll.GetLoanByCode(strLoanName.Trim());
                //lblNameA.Visible = true;
                //lblLoanNameA.Visible = true;
                //lblLoanNameA.InnerText = strLoanName;
                txtLoanNameA.SelectedValue = strLoanName;
                try
                {
                    QuotesAndTradesBLL quotesBL = new QuotesAndTradesBLL();
                    QuotesAndTrades quotes = quotesBL.GetQuotesAndTrades(strLoanName);
                    if (quotes != null)
                    {
                        if (quotes.BidPrice != null)
                            txtBidPriceA.Text = quotes.BidPrice.Value.ToString();
                        if (quotes.BidSpread != null)
                            txtBidSpreadA.Text = quotes.BidSpread.Value.ToString();
                        if (quotes.OfferPrice != null)
                            txtOfferPriceA.Text = quotes.OfferPrice.Value.ToString();
                        if (quotes.OfferSpread != null)
                            txtOfferSpreadA.Text = quotes.OfferSpread.Value.ToString();
                        if (quotes.AverageLife != null)
                            txtAvgLifeA.Text = quotes.AverageLife.Value.ToString();
                        if (quotes.AvgLifeDisc != null)
                            txtAveLifDiscA.Text = quotes.AvgLifeDisc.Value.ToString();
                        if (quotes.AvgLifeRiskDisc != null)
                            txtAveLifRiskyDiscA.Text = quotes.AvgLifeRiskDisc.Value.ToString();
                        if (quotes.Traded != null)
                            chkBoxTradedA.Checked = quotes.Traded.Value;
                    }
                }
                catch (Exception)
                {

                }

                if (txtBoxTradeDate1.SelectedDate != null)
                {
                    txtBoxSettlementDate1.SelectedDate = AddBusinessDays(txtBoxTradeDate1.SelectedDate.Value, 10);
                }

                Session.Add("LegendA", strLoanName);
                if (loan != null)
                {
                    hfSelectedLoanA.Value = loan.ID.ToString();


                    if (loan.Maturity_Date != string.Empty || loan.Maturity_Date != "")
                    {
                        txtBoxMaturityDateA.SelectedDate = Convert.ToDateTime(loan.Maturity_Date);
                    }

                    txtBoxInterestRateA.Text = loan.Margin;         //by Nidhi
                    // txtLoanNameA.Text = loan.CodeName; / Nik Commented
                    //txtDiscountMarginA.Text = loan.Margin;
                    txtBoxCurrencyLoanA.Text = loan.Currency;
                    txtBoxCouponFrequencyLoanA.Text = loan.CouponFrequency;
                    txtBoxLastFixingA.Text = loan.FixedOrFloating;
                    // txtBoxIRCouponA.Text = loan.Margin;
                    ddlCounterPartyA.SelectedValue = "CounterPartyA"; //nik commneted 02-04


                    if (loan.Maturity_Date != string.Empty && loan.Maturity_Date != null)
                    {// && loan.Signing_Date != null && loan.Signing_Date != string.Empty
                        ComputeCouponDatesAndFractions(txtBoxTradeDate1, txtLoanNameA, txtBoxSettlementDate1, txtBoxMaturityDateA, txtBoxCouponFrequencyLoanA, txtAveLifNonDiscA, grdCalculatedDates1, 1, txtBoxNotional1, txtBoxInterestRateA, txtBoxCurrencyLoanA);
                    }
                    else
                    {
                        grdCalculatedDates1.DataSource = null;
                        grdCalculatedDates1.DataBind();
                        RadWindowManager1.RadAlert("Cashflow generation failed as coupon frequency not valid or blank", 330, 180, "realedge associates", "alertCallBackFn");
                    }

                    LogActivity("Compute the CashFlow A", "Compute the cashflow for compact view A", string.Empty);
                }
                else
                {
                    RadWindowManager1.RadAlert("This loan name does not exist in database", 330, 180, "realedge associates", "alertCallBackFn");
                }
            }
            catch (Exception ex)
            {
                RadWindowManager1.RadAlert("Error in SetLoan A : " + ex.Message, 330, 180, "realedge associates", "alertCallBackFn");

            }
        }
        private void SetLoanA(string loanName, RadDatePicker dt)
        {
            //Fill up the detail now
            try
            {


                LoansBLL bll = new LoansBLL();

                string strLoanName = loanName;
                //  txtBoxTradeDate1.SelectedDate = DateTime.Now;
                if (strLoanName.Contains(';'))
                {
                    strLoanName = strLoanName.Replace(';', ' ');
                }
                Loans loan = bll.GetLoanByCode(strLoanName.Trim());
                try
                {


                    QuotesAndTradesBLL quotesBL = new QuotesAndTradesBLL();
                    QuotesAndTrades quotes = quotesBL.GetQuotesAndTrades(loanName);
                    if (quotes != null)
                    {
                        if (quotes.BidPrice != null)
                            txtBidPriceA.Text = quotes.BidPrice.Value.ToString();
                        if (quotes.BidSpread != null)
                            txtBidSpreadA.Text = quotes.BidSpread.Value.ToString();
                        if (quotes.OfferPrice != null)
                            txtOfferPriceA.Text = quotes.OfferPrice.Value.ToString();
                        if (quotes.OfferSpread != null)
                            txtOfferSpreadA.Text = quotes.OfferSpread.Value.ToString();
                        if (quotes.AverageLife != null)
                            txtAvgLifeA.Text = quotes.AverageLife.Value.ToString();
                        if (quotes.AvgLifeDisc != null)
                            txtAveLifDiscA.Text = quotes.AvgLifeDisc.Value.ToString();
                        if (quotes.AvgLifeRiskDisc != null)
                            txtAveLifRiskyDiscA.Text = quotes.AvgLifeRiskDisc.Value.ToString();
                        if (quotes.Traded != null)
                            chkBoxTradedA.Checked = quotes.Traded.Value;
                    }

                }
                catch (Exception)
                {

                }
                txtBoxTradeDate1.SelectedDate = Convert.ToDateTime(Session["Trade1"]);
                if (txtBoxTradeDate1.SelectedDate != null)
                {
                    txtBoxSettlementDate1.SelectedDate = AddBusinessDays(txtBoxTradeDate1.SelectedDate.Value, 10);
                }

                //lblNameA.Visible = true;
                //lblLoanNameA.Visible = true;
                //lblLoanNameA.InnerText = strLoanName;
                txtLoanNameA.SelectedValue = strLoanName;
                // Session.Add("Legend", strLoanName);
                if (loan != null)
                {
                    hfSelectedLoanA.Value = loan.ID.ToString();
                    //if (loan.Signing_Date != string.Empty || loan.Signing_Date != "")
                    //{
                    //    txtBoxSettlementDate1.SelectedDate = Convert.ToDateTime(loan.Signing_Date);
                    //}
                    if (loan.Maturity_Date != string.Empty || loan.Maturity_Date != "")
                    {
                        txtBoxMaturityDateA.SelectedDate = Convert.ToDateTime(loan.Maturity_Date);
                    }
                    txtBoxInterestRateA.Text = loan.Margin;
                    // txtLoanNameA.Text = loan.CodeName; / Nik Commented
                    //  txtDiscountMarginA.Text = loan.Margin;
                    txtBoxCurrencyLoanA.Text = loan.Currency;
                    txtBoxCouponFrequencyLoanA.Text = loan.CouponFrequency;
                    txtBoxLastFixingA.Text = loan.FixedOrFloating;
                    // txtBoxIRCouponA.Text = loan.Margin;
                    ddlCounterPartyA.SelectedValue = "CounterPartyA";

                    if (loan.Maturity_Date != string.Empty && loan.Maturity_Date != null)
                    {
                        // && loan.Signing_Date != null && loan.Signing_Date != string.Empty
                        ComputeCouponDatesAndFractions(dt, txtLoanNameA, txtBoxSettlementDate1, txtBoxMaturityDateA, txtBoxCouponFrequencyLoanA, txtAveLifNonDiscA, grdCalculatedDates1, 1, txtBoxNotional1, txtBoxInterestRateA, txtBoxCurrencyLoanA);
                    }
                    else
                    {

                        grdCalculatedDates1.DataSource = null;
                        grdCalculatedDates1.DataBind();
                        RadWindowManager1.RadAlert("Cashflow generation failed as coupon frequency not valid or blank", 330, 180, "realedge associates", "alertCallBackFn");
                    }
                    LogActivity("Compute the CashFlow A", "Compute the cashflow for compact view A", string.Empty);
                }
                else
                {
                    RadWindowManager1.RadAlert("This loan name does not exist in database", 330, 180, "realedge associates", "alertCallBackFn");
                }

            }
            catch (Exception)
            {

                throw;
            }

        }
        private void SetLoanB()
        {
            //Fill up the detail now
            try
            {
                LoansBLL bll = new LoansBLL();

                string strLoanName = txtLoanNameB.Text.ToString();
                txtBoxTradeDate2.SelectedDate = DateTime.Now;
                //if (strLoanName.Contains(';'))
                //{
                //    strLoanName = strLoanName.Replace(';', ' ');
                //}
                try
                {


                    QuotesAndTradesBLL quotesBL = new QuotesAndTradesBLL();
                    QuotesAndTrades quotes = quotesBL.GetQuotesAndTrades(strLoanName);
                    if (quotes != null)
                    {
                        if (quotes.BidPrice != null)
                            txtBidPriceA.Text = quotes.BidPrice.Value.ToString();
                        if (quotes.BidSpread != null)
                            txtBidSpreadA.Text = quotes.BidSpread.Value.ToString();
                        if (quotes.OfferPrice != null)
                            txtOfferPriceA.Text = quotes.OfferPrice.Value.ToString();
                        if (quotes.OfferSpread != null)
                            txtOfferSpreadA.Text = quotes.OfferSpread.Value.ToString();
                        if (quotes.AverageLife != null)
                            txtAvgLifeA.Text = quotes.AverageLife.Value.ToString();
                        if (quotes.AvgLifeDisc != null)
                            txtAveLifDiscA.Text = quotes.AvgLifeDisc.Value.ToString();
                        if (quotes.AvgLifeRiskDisc != null)
                            txtAveLifRiskyDiscA.Text = quotes.AvgLifeRiskDisc.Value.ToString();
                        if (quotes.Traded != null)
                            chkBoxTradedA.Checked = quotes.Traded.Value;
                    }
                }
                catch (Exception)
                {

                }
                Loans loan = bll.GetLoanByCode(strLoanName.Trim());
                if (txtBoxTradeDate2.SelectedDate != null)
                {
                    txtBoxSettlementDate2.SelectedDate = AddBusinessDays(txtBoxTradeDate2.SelectedDate.Value, 10);
                }

                if (loan != null)
                {

                    txtLoanNameB.SelectedValue = strLoanName;
                    Session.Add("LegendB", strLoanName);
                    if (loan != null)
                    {
                        hfSelectedLoanB.Value = loan.ID.ToString();
                        //if (loan.Signing_Date != string.Empty || loan.Signing_Date != "")
                        //{
                        //    txtBoxSettlementDate2.SelectedDate = Convert.ToDateTime(loan.Signing_Date);
                        //}
                        if (loan.Maturity_Date != string.Empty || loan.Maturity_Date != "")
                        {
                            txtBoxMaturityDateB.SelectedDate = Convert.ToDateTime(loan.Maturity_Date);
                        }
                        txtInterestRateB.Text = loan.Margin;

                        //txtDiscountMarginB.Text = loan.Margin;
                        txtCurrencyB.Text = loan.Currency;
                        txtBoxCouponFrequencyLoanB.Text = loan.CouponFrequency;
                        txtBoxLastFixingB.Text = loan.FixedOrFloating;
                        // txtBoxIRCouponB.Text = loan.Margin;
                        ddlCounterPartyB.SelectedValue = "CounterPartyB";
                        //  DropDownListItem frequency = ddlAddCouponFrequency.FindItemByValue(loan.CouponFrequency);
                        //if (frequency != null)
                        //{
                        //    frequency.Selected = true;
                        //}

                        if (loan.Maturity_Date != string.Empty && loan.Maturity_Date != null)
                        {// && loan.Signing_Date != null && loan.Signing_Date != string.Empty
                            ComputeCouponDatesAndFractions(txtBoxTradeDate2, txtLoanNameB, txtBoxSettlementDate2, txtBoxMaturityDateB, txtBoxCouponFrequencyLoanB, txtBoxAveLifNonDiscB, grdCalculatedDates2, 2, txtBoxNotional2, txtInterestRateB, txtCurrencyB);
                        }
                        else
                        {

                            grdCalculatedDates2.DataSource = null;
                            grdCalculatedDates2.DataBind();
                            RadWindowManager1.RadAlert("Cashflow generation failed as coupon frequency not valid or blank", 330, 180, "realedge associates", "alertCallBackFn");
                        }
                        LogActivity("Compute the CashFlow B", "Compute the cashflow for compact view B", string.Empty);
                    }
                }
                else
                {
                    RadWindowManager1.RadAlert("This loan name does not exist in database", 330, 180, "realedge associates", "alertCallBackFn");
                }

            }
            catch (Exception)
            {


            }

        }

        private void SetLoanC()
        {
            //Fill up the detail now
            LoansBLL bll = new LoansBLL();
            //Loans loan = bll.GetLoanByCode(txtLoanNameC.Text);
            string strLoanName = txtLoanNameC.Text.ToString();
            txtBoxTradeDate3.SelectedDate = DateTime.Now;
            if (strLoanName.Contains(';'))
            {
                strLoanName = strLoanName.Replace(';', ' ');
            }
            //lblNameC.Visible = true;
            //lblLoanNameC.Visible = true;
            //lblLoanNameC.InnerText = strLoanName;
            txtLoanNameC.SelectedValue = strLoanName;
            try
            {


                QuotesAndTradesBLL quotesBL = new QuotesAndTradesBLL();
                QuotesAndTrades quotes = quotesBL.GetQuotesAndTrades(strLoanName);
                if (quotes != null)
                {
                    if (quotes.BidPrice != null)
                        txtBidPriceA.Text = quotes.BidPrice.Value.ToString();
                    if (quotes.BidSpread != null)
                        txtBidSpreadA.Text = quotes.BidSpread.Value.ToString();
                    if (quotes.OfferPrice != null)
                        txtOfferPriceA.Text = quotes.OfferPrice.Value.ToString();
                    if (quotes.OfferSpread != null)
                        txtOfferSpreadA.Text = quotes.OfferSpread.Value.ToString();
                    if (quotes.AverageLife != null)
                        txtAvgLifeA.Text = quotes.AverageLife.Value.ToString();
                    if (quotes.AvgLifeDisc != null)
                        txtAveLifDiscA.Text = quotes.AvgLifeDisc.Value.ToString();
                    if (quotes.AvgLifeRiskDisc != null)
                        txtAveLifRiskyDiscA.Text = quotes.AvgLifeRiskDisc.Value.ToString();
                    if (quotes.Traded != null)
                        chkBoxTradedA.Checked = quotes.Traded.Value;
                }
            }
            catch (Exception)
            {

            }
            Session.Add("LegendC", strLoanName);
            if (txtBoxTradeDate3.SelectedDate != null)
            {
                txtBoxSettlementDate3.SelectedDate = AddBusinessDays(txtBoxTradeDate3.SelectedDate.Value, 10);
            }

            Loans loan = bll.GetLoanByCode(strLoanName.Trim());
            if (loan != null)
            {
                hfSelectedLoanC.Value = loan.ID.ToString();
                //if (loan.Signing_Date != string.Empty || loan.Signing_Date != "")
                //{
                //    txtBoxSettlementDate3.SelectedDate = Convert.ToDateTime(loan.Signing_Date);
                //}
                if (loan.Maturity_Date != string.Empty || loan.Maturity_Date != "")
                {
                    txtMaturityDateC.SelectedDate = Convert.ToDateTime(loan.Maturity_Date);
                }
                txtInterestRateC.Text = loan.Margin;
                // txtLoanNameC.Text = loan.CodeName; // Nik Commented
                //txtDiscountMarginC.Text = loan.Margin;
                //txtCurrencyC.Text = loan.Currency;
                //txtDiscountMarginC.Text = loan.Margin;
                txtCurrencyC.Text = loan.Currency;
                txtBoxCouponFrequencyLoanC.Text = loan.CouponFrequency;
                txtLastFixingC.Text = loan.FixedOrFloating;
                // txtBoxIRCouponC.Text = loan.Margin;

                // txtBoxCounterPartyC.Text = "CounterPartyC";

                ddlCounterPartyC.SelectedValue = "CounterPartyC";

                //DropDownListItem frequency = ddlAddCouponFrequency.FindItemByValue(loan.CouponFrequency);
                //if (frequency != null)
                //{
                //    frequency.Selected = true;
                //}
                // Session.Add("BoxTradeDate", txtBoxTradeDate3);
                if (loan.Maturity_Date != string.Empty && loan.Maturity_Date != null)
                {// && loan.Signing_Date != null && loan.Signing_Date != string.Empty
                    ComputeCouponDatesAndFractions(txtBoxTradeDate3, txtLoanNameC, txtBoxSettlementDate3, txtMaturityDateC, txtBoxCouponFrequencyLoanC, txtBoxAveLifNonDiscC, grdCalculatedDates3, 3, txtBoxNotional3, txtInterestRateC, txtCurrencyC);
                }
                else
                {

                    grdCalculatedDates3.DataSource = null;
                    grdCalculatedDates3.DataBind();
                    RadWindowManager1.RadAlert("Cashflow generation failed as coupon frequency not valid or blank", 330, 180, "realedge associates", "alertCallBackFn");
                }
                LogActivity("Compute the CashFlow C", "Compute the cashflow for compact view C", string.Empty);
            }
            else
            {
                RadWindowManager1.RadAlert("This loan name does not exist in database", 330, 180, "realedge associates", "alertCallBackFn");
            }

        }
        private void SetLoanB(string loanName, RadDatePicker dt)
        {
            //Fill up the detail now
            LoansBLL bll = new LoansBLL();
            //Loans loan = bll.GetLoanByCode(txtLoanNameB.Text);
            //  txtBoxTradeDate2.SelectedDate = DateTime.Now;
            string strLoanName = loanName;
            if (strLoanName.Contains(';'))
            {
                strLoanName = strLoanName.Replace(';', ' ');
            }
            Loans loan = bll.GetLoanByCode(strLoanName.Trim());
            txtBoxTradeDate2.SelectedDate = Convert.ToDateTime(Session["Trade2"]);
            //lblNameB.Visible = true;
            //lblLoanNameB.Visible = true;
            //lblLoanNameB.InnerText = strLoanName;
            try
            {


                QuotesAndTradesBLL quotesBL = new QuotesAndTradesBLL();
                QuotesAndTrades quotes = quotesBL.GetQuotesAndTrades(loanName);
                if (quotes != null)
                {
                    if (quotes.BidPrice != null)
                        txtBoxBidPriceB.Text = quotes.BidPrice.Value.ToString();
                    if (quotes.BidSpread != null)
                        txtBoxBidSpreadB.Text = quotes.BidSpread.Value.ToString();
                    if (quotes.OfferPrice != null)
                        txtBoxOfferPriceB.Text = quotes.OfferPrice.Value.ToString();
                    if (quotes.OfferSpread != null)
                        txtBoxOfferSpreadB.Text = quotes.OfferSpread.Value.ToString();
                    if (quotes.AverageLife != null)
                        txtAvgLifeB.Text = quotes.AverageLife.Value.ToString();
                    if (quotes.AvgLifeDisc != null)
                        txtBoxAveLifDiscB.Text = quotes.AvgLifeDisc.Value.ToString();
                    if (quotes.AvgLifeRiskDisc != null)
                        txtBoxAveLifRiskyDiscB.Text = quotes.AvgLifeRiskDisc.Value.ToString();
                    if (quotes.Traded != null)
                        chkBoxTradedB.Checked = quotes.Traded.Value;
                }
            }
            catch (Exception)
            {

            }

            txtLoanNameB.SelectedValue = strLoanName;
            if (txtBoxTradeDate2.SelectedDate != null)
            {
                txtBoxSettlementDate2.SelectedDate = AddBusinessDays(txtBoxTradeDate2.SelectedDate.Value, 10);
            }

            if (loan != null)
            {
                hfSelectedLoanB.Value = loan.ID.ToString();
                //if (loan.Signing_Date != string.Empty || loan.Signing_Date != "")
                //{
                if (loan.Signing_Date != string.Empty || loan.Signing_Date != "")
                {
                    txtBoxSettlementDate2.SelectedDate = Convert.ToDateTime(loan.Signing_Date);
                }
                //}
                //if (loan.Maturity_Date != string.Empty || loan.Maturity_Date != "")
                //{
                if (loan.Maturity_Date != string.Empty || loan.Maturity_Date != "")
                {
                    txtBoxMaturityDateB.SelectedDate = Convert.ToDateTime(loan.Maturity_Date);
                }
                //}

                txtInterestRateB.Text = loan.Margin;
                // txtLoanNameB.Text = loan.CodeName; // Nik Commented
                //txtDiscountMarginB.Text = loan.Margin;
                txtCurrencyB.Text = loan.Currency;
                txtBoxCouponFrequencyLoanB.Text = loan.CouponFrequency;
                txtBoxLastFixingB.Text = loan.FixedOrFloating;
                // txtBoxIRCouponB.Text = loan.Margin;
                //DropDownListItem frequency = ddlAddCouponFrequency.FindItemByValue(loan.CouponFrequency);
                //if (frequency != null)
                //{
                //    frequency.Selected = true;
                //}

                if (loan.Maturity_Date != string.Empty && loan.Maturity_Date != null)
                {// && loan.Signing_Date != null && loan.Signing_Date != string.Empty
                    ComputeCouponDatesAndFractions(dt, txtLoanNameB, txtBoxSettlementDate2, txtBoxMaturityDateB, txtBoxCouponFrequencyLoanB, txtBoxAveLifNonDiscB, grdCalculatedDates2, 2, txtBoxNotional2, txtInterestRateB, txtCurrencyB);
                }
                else
                {

                    grdCalculatedDates2.DataSource = null;
                    grdCalculatedDates2.DataBind();
                    RadWindowManager1.RadAlert("Cashflow generation failed as coupon frequency not valid or blank", 330, 180, "realedge associates", "alertCallBackFn");
                }

                LogActivity("Compute the CashFlow B", "Compute the cashflow for compact view B", string.Empty);
            }
            else
            {
                RadWindowManager1.RadAlert("This loan name does not exist in database", 330, 180, "realedge associates", "alertCallBackFn");
            }
        }

        private void SetLoanC(string loanName, RadDatePicker dt)
        {
            //Fill up the detail now
            LoansBLL bll = new LoansBLL();
            //Loans loan = bll.GetLoanByCode(txtLoanNameC.Text);
            //  txtBoxTradeDate3.SelectedDate = DateTime.Now;
            string strLoanName = loanName;
            if (strLoanName.Contains(';'))
            {
                strLoanName = strLoanName.Replace(';', ' ');
            }
            // Session.Add("Legend", strLoanName);
            Loans loan = bll.GetLoanByCode(strLoanName.Trim());
            //lblNameC.Visible = true;
            //lblLoanNameC.Visible = true;
            //lblLoanNameC.InnerText = strLoanName;
            txtBoxTradeDate3.SelectedDate = Convert.ToDateTime(Session["Trade3"]);
            try
            {


                QuotesAndTradesBLL quotesBL = new QuotesAndTradesBLL();
                QuotesAndTrades quotes = quotesBL.GetQuotesAndTrades(loanName);
                if (quotes != null)
                {
                    if (quotes.BidPrice != null)
                        txtBoxBidPriceC.Text = quotes.BidPrice.Value.ToString();
                    if (quotes.BidSpread != null)
                        txtBoxBidSpreadC.Text = quotes.BidSpread.Value.ToString();
                    if (quotes.OfferPrice != null)
                        txtBoxOfferPriceC.Text = quotes.OfferPrice.Value.ToString();
                    if (quotes.OfferSpread != null)
                        txtBoxOfferSpreadC.Text = quotes.OfferSpread.Value.ToString();
                    if (quotes.AverageLife != null)
                        txtAvgLifeC.Text = quotes.AverageLife.Value.ToString();
                    if (quotes.AvgLifeDisc != null)
                        txtBoxAveLifDiscC.Text = quotes.AvgLifeDisc.Value.ToString();
                    if (quotes.AvgLifeRiskDisc != null)
                        txtBoxAveLifRiskyDiscC.Text = quotes.AvgLifeRiskDisc.Value.ToString();
                    if (quotes.Traded != null)
                        chkBoxTradedC.Checked = quotes.Traded.Value;
                }

            }
            catch (Exception)
            {

            }
            txtLoanNameC.SelectedValue = strLoanName;
            if (txtBoxTradeDate3.SelectedDate != null)
            {
                txtBoxSettlementDate3.SelectedDate = AddBusinessDays(txtBoxTradeDate3.SelectedDate.Value, 10);
            }

            if (loan != null)
            {
                hfSelectedLoanC.Value = loan.ID.ToString();
                if (loan.Signing_Date != string.Empty || loan.Signing_Date != "")
                {
                    txtBoxSettlementDate3.SelectedDate = Convert.ToDateTime(loan.Signing_Date);
                }
                if (loan.Maturity_Date != string.Empty || loan.Maturity_Date != "")
                {
                    txtMaturityDateC.SelectedDate = Convert.ToDateTime(loan.Maturity_Date);
                }
                txtInterestRateC.Text = loan.Margin;
                // txtLoanNameC.Text = loan.CodeName; // Nik Commented
                //txtDiscountMarginC.Text = loan.Margin;
                txtCurrencyC.Text = loan.Currency;
                txtBoxCouponFrequencyLoanC.Text = loan.CouponFrequency;
                txtLastFixingC.Text = loan.FixedOrFloating;
                // txtBoxIRCouponC.Text = loan.Margin;
                //DropDownListItem frequency = ddlAddCouponFrequency.FindItemByValue(loan.CouponFrequency);
                //if (frequency != null)
                //{
                //    frequency.Selected = true;
                //}
                if (loan.Maturity_Date != string.Empty && loan.Maturity_Date != null)
                {// && loan.Signing_Date != null && loan.Signing_Date != string.Empty
                    ComputeCouponDatesAndFractions(dt, txtLoanNameC, txtBoxSettlementDate3, txtMaturityDateC, txtBoxCouponFrequencyLoanC, txtBoxAveLifNonDiscC, grdCalculatedDates3, 3, txtBoxNotional3, txtInterestRateC, txtCurrencyC);
                }
                else
                {

                    grdCalculatedDates3.DataSource = null;
                    grdCalculatedDates3.DataBind();
                    RadWindowManager1.RadAlert("Cashflow generation failed as coupon frequency not valid or blank", 330, 180, "realedge associates", "alertCallBackFn");
                }
                LogActivity("Compute the CashFlow C", "Compute the cashflow for compact view C", string.Empty);
            }
            else
            {
                RadWindowManager1.RadAlert("This loan name does not exist in database", 330, 180, "realedge associates", "alertCallBackFn");
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
        private void InsertDuplicateRecord()
        {
            try
            {
                DuplicateRecord model = new DuplicateRecord();

                if (!string.IsNullOrEmpty(txtBidPriceA.Text))
                {
                    model.BidPrice = Convert.ToDecimal(txtBidPriceA.Text);
                }
                if (!string.IsNullOrEmpty(txtBidSpreadA.Text))
                {
                    string strBidSpread = Convert.ToDecimal(txtBidSpreadA.Text).ToString("0.00");
                    model.BidSpread = Convert.ToDecimal(strBidSpread);
                }
                model.CounterParty = ddlCounterPartyA.SelectedValue;
                model.LoanName = txtLoanNameA.Text;
                if (!string.IsNullOrEmpty(txtOfferPriceA.Text))
                {
                    model.OfferPrice = Convert.ToDecimal(txtOfferPriceA.Text);
                }
                if (!string.IsNullOrEmpty(txtOfferSpreadA.Text))
                {
                    string strOfferSpread = Convert.ToDecimal(txtOfferSpreadA.Text).ToString("0.00");
                    model.OfferSpread = Convert.ToDecimal(strOfferSpread);
                }
                // model.MarketValue
                model.TimeStamp = DateTime.UtcNow;
                model.Traded = chkBoxTradedA.Checked;
                if (txtLoanNameA.SelectedValue != string.Empty)
                {
                    LoansBLL loanBLL = new LoansBLL();
                    model.Country = loanBLL.GetCoutryIDbyLoanID(txtLoanNameA.SelectedValue);
                }
                // model.CountryID = Convert.ToInt32(ddlRegionA.SelectedValue);

                //need to ask whether to keep this lines or not?
                //List<QuotesAndTrades> lst = new List<QuotesAndTrades>();
                //lst.Add(model);
                QuotesAndTradesBLL bll = new QuotesAndTradesBLL();

                bll.InsertDuplicateRecord(model);


            }
            catch (Exception ex)
            {
                lblTradeQuoteStatusC.Text = "Error in saving";
                LogActivity("Quote and Loan A added(Unsuccessfull)", "Unable to add the quote and trade", ex.Message);
            }
        }
        protected void btnAddTradeQuote_Click(object sender, EventArgs e)
        {
            try
            {
                if (CheckForValid(txtLoanNameA.Text, ddlCounterPartyA.SelectedValue) == true)
                {
                    RadWindowManager1.RadConfirm("Are you sure want to save duplicate record?", "confirmCallBackFn", 300, 150, null, "realedge associates", "");
                }
                else
                {
                    RadWindowManager1.RadConfirm(txtLoanNameA.Text + " will be added to database?", "confirmCallBackFn", 300, 150, null, "realedge associates", "");
                }
                //    QuotesAndTrades model = new QuotesAndTrades();

                //    if (!string.IsNullOrEmpty(txtBidPriceA.Text))
                //    {
                //        model.BidPrice = Convert.ToDecimal(txtBidPriceA.Text);
                //    }
                //    if (!string.IsNullOrEmpty(txtBidPriceA.Text))
                //    {
                //        model.BidSpread = Convert.ToDecimal(txtBidSpreadA.Text);
                //    }
                //    model.CounterParty = ddlCounterPartyA.SelectedValue;
                //    model.LoanName = txtLoanNameA.Text;
                //    if (!string.IsNullOrEmpty(txtOfferPriceA.Text))
                //    {
                //        model.OfferPrice = Convert.ToDecimal(txtOfferPriceA.Text);
                //    }
                //    if (!string.IsNullOrEmpty(txtOfferSpreadA.Text))
                //    {
                //        model.OfferSpread = Convert.ToDecimal(txtOfferSpreadA.Text);
                //    }
                //    // model.MarketValue
                //    model.TimeStamp = DateTime.UtcNow;
                //    model.Traded = chkBoxTradedA.Checked;
                //    //  model.CountryID = Convert.ToInt32(ddlRegionA.SelectedValue);
                //    if (txtLoanNameA.SelectedValue != string.Empty)
                //    {
                //        LoansBLL loanBLL = new LoansBLL();
                //        model.Country = loanBLL.GetCoutryIDbyLoanID(txtLoanNameA.SelectedValue);
                //    }
                //    List<QuotesAndTrades> lst = new List<QuotesAndTrades>();
                //    lst.Add(model);
                //    QuotesAndTradesBLL bll = new QuotesAndTradesBLL();

                //    bll.AddImportedQuotesAndTrades(lst);

                //    BindHistoricalQuotesAndTradesTab();

                //    // broadcast that quote and trade has been added
                //    ApplicationHub.NewQuotesAndTradeAdded(model);

                //    if (string.IsNullOrEmpty(hfSelectedLoanA.Value))
                //    {
                //        // Add the Loans as well
                //        Loans loanModel = new Loans();
                //        //loanModel.Amortizing
                //        //loanModel.Bilateral
                //        loanModel.CodeName = txtLoanNameA.Text;
                //        //loanModel.Country
                //        loanModel.CouponFrequency = txtBoxCouponFrequencyLoanA.Text;
                //        loanModel.Currency = txtBoxCurrencyLoanA.Text;
                //        //loanModel.FacilitySize
                //        loanModel.Margin = txtBoxInterestRateA.Text;
                //        loanModel.Margin = txtDiscountMarginA.Text;
                //        loanModel.Maturity_Date = txtBoxMaturityDateA.SelectedDate.Value.ToShortDateString();
                //        //loanModel.Sector
                //        loanModel.Signing_Date = txtBoxTradeDate1.SelectedDate.Value.ToString();

                //        // Persist in database
                //        LoansBLL loanBll = new LoansBLL();
                //        loanBll.SaveLoan(loanModel);

                //        //new loan has been added.
                //        //Broadcast it
                //        ApplicationHub.NewLoanAdded(loanModel);
                //    }

                //    BindLoansTab();

                //    ShowMessage("Message", "Quote and Loan added Successfully");

                //    LogActivity("Quote and Loan A added", "Add the quote and trade on compact view A", string.Empty);
                //    Clear();
                //}
            }
            catch (Exception ex)
            {
                lblTradeQuoteStatusC.Text = "Error in saving";
                LogActivity("Quote and Loan A added(Unsuccessfull)", "Unable to add the quote and trade", ex.Message);
            }
        }

        public void InsertRecord(string param, int flag)
        {

            try
            {
                string[] strSplit = param.Split(',');
                QuotesAndTrades model = new QuotesAndTrades();
                if (!string.IsNullOrEmpty(strSplit[0].Remove(0, 10)))
                {
                    model.BidPrice = Convert.ToDecimal(strSplit[0].Remove(0, 10));
                }
                if (!string.IsNullOrEmpty(strSplit[1].Remove(0, 10)))
                {
                    string strBidSpread = Convert.ToDecimal(strSplit[1].Remove(0, 10)).ToString("0.00");
                    model.BidSpread = Convert.ToDecimal(strBidSpread);
                }
                model.CounterParty = strSplit[2].Remove(0, 13);
                model.LoanName = strSplit[3].Remove(0, 9);
                if (!string.IsNullOrEmpty(strSplit[4].Remove(0, 11)))
                {
                    model.OfferPrice = Convert.ToDecimal(strSplit[4].Remove(0, 11));
                }
                if (!string.IsNullOrEmpty(strSplit[5].Remove(0, 12)))
                {
                    string strOfferSpread = Convert.ToDecimal(strSplit[5].Remove(0, 12)).ToString("0.00");
                    model.OfferSpread = Convert.ToDecimal(strOfferSpread);
                }
                // model.MarketValue
                model.TimeStamp = DateTime.Now;
                if (strSplit[6].Remove(0, 9) == "true")
                    model.Traded = true;
                else
                    model.Traded = false;

                if (strSplit[3].Remove(0, 9) != string.Empty)
                {
                    LoansBLL loanBLL = new LoansBLL();
                    model.Country = loanBLL.GetCoutryIDbyLoanID(strSplit[3].Remove(0, 9));
                }
                if (!string.IsNullOrEmpty(strSplit[12].Remove(0, 10)))
                {
                    string str = strSplit[12].Remove(0, 10);
                    DateTime tradeDate = DateTime.Now;
                    if (str.Contains('-'))
                    {
                        string[] strSpl = str.Split('-');
                        string datetime = strSpl[1] + "/" + strSpl[0] + "/" + strSpl[2] + " " + strSpl[3] + ":" + strSpl[4] + ":" + strSpl[5];
                        tradeDate = Convert.ToDateTime(datetime);
                    }
                    else
                        tradeDate = Convert.ToDateTime(str);
                    model.TradedDate = Convert.ToDateTime(tradeDate);
                }
                if (!string.IsNullOrEmpty(strSplit[13].Remove(0, 12)))
                {
                    string avgLifeDisc = Convert.ToDecimal(strSplit[13].Remove(0, 12)).ToString("0.00");
                    model.AvgLifeDisc = Convert.ToDecimal(avgLifeDisc);
                }
                if (!string.IsNullOrEmpty(strSplit[14].Remove(0, 16)))
                {
                    string avgLifeRiscDisc = Convert.ToDecimal(strSplit[14].Remove(0, 16)).ToString("0.00");
                    model.AvgLifeRiskDisc = Convert.ToDecimal(avgLifeRiscDisc);
                }
                if (!string.IsNullOrEmpty(strSplit[15].Remove(0, 15)))
                {
                    string avgLifeNonDisc = Convert.ToDecimal(strSplit[15].Remove(0, 15)).ToString("0.00");
                    model.AvgLifeNonDisc = Convert.ToDecimal(avgLifeNonDisc);
                }
                if (!string.IsNullOrEmpty(strSplit[16].Remove(0, 15)))
                {
                    DateTime settlementDate = Convert.ToDateTime(strSplit[16].Remove(0, 15));
                    model.SettlementDate = settlementDate;
                }
                if (!string.IsNullOrEmpty(strSplit[17].Remove(0, 7)))
                {
                    string margin = Convert.ToString(strSplit[17].Remove(0, 7));
                    model.Margin = margin;
                }
                if (!string.IsNullOrEmpty(strSplit[18].Remove(0, 9)))
                {
                    string notional = Convert.ToDecimal(Convert.ToString(strSplit[18].Remove(0, 9))).ToString("0.00");
                    model.MarketValue = Convert.ToDecimal(notional);
                }
                if (!string.IsNullOrEmpty(strSplit[19].Remove(0, 9)))
                {
                    string averageLife = Convert.ToDecimal(Convert.ToString(strSplit[19].Remove(0, 12).Replace('}', ' '))).ToString("0.00");
                    model.AverageLife = Convert.ToDecimal(averageLife);
                }
                // model.CountryID = Convert.ToInt32(ddlRegionA.SelectedValue);
                List<QuotesAndTrades> lst = new List<QuotesAndTrades>();
                lst.Add(model);
                QuotesAndTradesBLL bll = new QuotesAndTradesBLL();

                bll.AddImportedQuotesAndTrades(lst);

                BindHistoricalQuotesAndTradesTab();

                // broadcast that quote and trade has been added
                ApplicationHub.NewQuotesAndTradeAdded(model);

                if (string.IsNullOrEmpty(hfSelectedLoanA.Value))
                {
                    // Add the Loans as well
                    Loans loanModel = new Loans();
                    //loanModel.Amortizing
                    //loanModel.Bilateral
                    loanModel.CodeName = strSplit[3].Remove(0, 9);
                    //loanModel.Country
                    loanModel.CouponFrequency = strSplit[7].Remove(0, 8);
                    loanModel.Currency = strSplit[8].Remove(0, 9);
                    //loanModel.FacilitySize
                    loanModel.Margin = strSplit[9].Remove(0, 9);
                    //   loanModel.Margin = strSplit[10].Remove(0, 8);
                    loanModel.Maturity_Date = strSplit[11].Remove(0, 8);
                    //loanModel.Sector
                    loanModel.Signing_Date = strSplit[12].Remove(0, 10).ToString().Trim();

                    // Persist in database
                    LoansBLL loanBll = new LoansBLL();

                    if (loanBll.CheckForLoanCode(strSplit[3].Remove(0, 9).Trim()))
                    {
                        loanBll.SaveLoan(loanModel);

                        //new loan has been added.
                        //Broadcast it
                        ApplicationHub.NewLoanAdded(loanModel);
                        BindLoansTab();
                        ShowMessage("Message", "Quote and Loan added Successfully");
                    }
                    else
                    {
                        int loanID = loanBll.GetLoanByCode(strSplit[3].Remove(0, 9)).ID;
                        loanBll.EditLoan(loanModel, loanID, 1);       //by nidhi for update the existing loan.

                        DuplicateLoan duplicateLoan = new DuplicateLoan();
                        //loanModel.Amortizing
                        //loanModel.Bilateral
                        duplicateLoan.CodeName = strSplit[3].Remove(0, 9);
                        //loanModel.Country
                        duplicateLoan.CouponFrequency = strSplit[7].Remove(0, 8);
                        duplicateLoan.Currency = strSplit[8].Remove(0, 9);
                        //loanModel.FacilitySize
                        duplicateLoan.FixedOrFloating = strSplit[9].Remove(0, 9);
                        duplicateLoan.Margin = strSplit[10].Remove(0, 8);
                        duplicateLoan.Maturity_Date = strSplit[11].Remove(0, 8);
                        //loanModel.Sector
                        duplicateLoan.Signing_Date = strSplit[12].Remove(0, 10).ToString().Trim();
                        loanBll.SaveDuplicateLoan(duplicateLoan);
                    }
                }

                hdnQuoteTemp.Value = "N";
                ShowMessage("Message", "Quote and Loan added Successfully");
                switch (flag)
                {
                    case 1:
                        LogActivity("Quote and Loan A added", "Add the quote and trade on compact view A", string.Empty);
                        break;
                    case 2:
                        LogActivity("Quote and Loan B added", "Add the quote and trade on compact view B", string.Empty);
                        break;
                    case 3:
                        LogActivity("Quote and Loan C added", "Add the quote and trade on compact view C", string.Empty);
                        break;
                    default:
                        break;
                }

            }
            catch (Exception)
            {
            }
        }

        protected void btnAddTradeQuote2_Click(object sender, EventArgs e)
        {
            try
            {
                if (CheckForValid(txtLoanNameB.Text, ddlCounterPartyB.SelectedValue) == true)
                {
                    RadWindowManager1.RadConfirm("Are you sure want to save duplicate record?", "confirmCallBackFn2", 350, 230, null, "realedge associates", "");
                }
                else
                {
                    RadWindowManager1.RadConfirm(txtLoanNameB.Text + " will be added to database?", "confirmCallBackFn2", 350, 230, null, "realedge associates", "");
                }
                //QuotesAndTrades model = new QuotesAndTrades();

                //if (!string.IsNullOrEmpty(txtBoxBidPriceB.Text))
                //{
                //    model.BidPrice = Convert.ToDecimal(txtBoxBidPriceB.Text);
                //}
                //if (!string.IsNullOrEmpty(txtBoxBidPriceB.Text))
                //{
                //    model.BidSpread = Convert.ToDecimal(txtBoxBidPriceB.Text);
                //}
                //model.CounterParty = ddlCounterPartyB.SelectedValue;
                //model.LoanName = txtLoanNameB.Text;
                //if (!string.IsNullOrEmpty(txtBoxOfferPriceB.Text))
                //{
                //    model.OfferPrice = Convert.ToDecimal(txtBoxOfferPriceB.Text);
                //}
                //if (!string.IsNullOrEmpty(txtBoxOfferSpreadB.Text))
                //{
                //    model.OfferSpread = Convert.ToDecimal(txtBoxOfferSpreadB.Text);
                //}
                //// model.MarketValue
                //model.TimeStamp = DateTime.UtcNow;
                //model.Traded = chkBoxTradedB.Checked;
                ////  model.CountryID = Convert.ToInt32(ddlRegionA.SelectedValue);
                //if (txtLoanNameB.SelectedValue != string.Empty)
                //{
                //    LoansBLL loanBLL = new LoansBLL();
                //    model.Country = loanBLL.GetCoutryIDbyLoanID(txtLoanNameB.SelectedValue);
                //}
                //List<QuotesAndTrades> lst = new List<QuotesAndTrades>();
                //lst.Add(model);
                //QuotesAndTradesBLL bll = new QuotesAndTradesBLL();

                //bll.AddImportedQuotesAndTrades(lst);

                //BindHistoricalQuotesAndTradesTab();

                //// broadcast that quote and trade has been added
                //ApplicationHub.NewQuotesAndTradeAdded(model);

                //if (string.IsNullOrEmpty(hfSelectedLoanB.Value))
                //{
                //    // Add the Loans as well
                //    Loans loanModel = new Loans();
                //    //loanModel.Amortizing
                //    //loanModel.Bilateral
                //    loanModel.CodeName = txtLoanNameB.Text;
                //    //loanModel.Country
                //    loanModel.CouponFrequency = txtBoxCouponFrequencyLoanB.Text;
                //    loanModel.Currency = txtCurrencyB.Text;
                //    //loanModel.FacilitySize
                //    loanModel.Margin = txtInterestRateB.Text;
                //    loanModel.Margin = txtDiscountMarginB.Text;
                //    loanModel.Maturity_Date = txtBoxMaturityDateB.SelectedDate.Value.ToShortDateString();
                //    //loanModel.Sector
                //    loanModel.Signing_Date = txtBoxTradeDate2.SelectedDate.Value.ToString();

                //    // Persist in database
                //    LoansBLL loanBll = new LoansBLL();
                //    loanBll.SaveLoan(loanModel);

                //    //new loan has been added.
                //    //Broadcast it
                //    ApplicationHub.NewLoanAdded(loanModel);
                //}

                //BindLoansTab();

                //ShowMessage("Message", "Quote and Loan added Successfully");
                //LogActivity("Quote and Loan B added", "Add the quote and trade on compact view B", string.Empty);
                //}
            }
            catch (Exception ex)
            {
                lblTradeQuoteStatusC.Text = "Error in saving";
                LogActivity("Quote and Loan B added(Unsuccessfull)", "Unable to add the quote and trade", ex.Message);
            }
        }

        protected void btnAddTradeQuote3_Click(object sender, EventArgs e)
        {
            try
            {
                if (CheckForValid(txtLoanNameC.Text, ddlCounterPartyC.SelectedValue) == true)
                {
                    RadWindowManager1.RadConfirm("Are you sure want to save duplicate record?", "confirmCallBackFn3", 350, 230, null, "realedge associates", "");
                }
                else
                {
                    RadWindowManager1.RadConfirm(txtLoanNameC.Text + " will be added to database?", "confirmCallBackFn3", 350, 230, null, "realedge associates", "");
                }
                //    QuotesAndTrades model = new QuotesAndTrades();

                //    if (!string.IsNullOrEmpty(txtBoxBidPriceC.Text))
                //    {
                //        model.BidPrice = Convert.ToDecimal(txtBoxBidPriceC.Text);
                //    }
                //    if (!string.IsNullOrEmpty(txtBoxBidPriceC.Text))
                //    {
                //        model.BidSpread = Convert.ToDecimal(txtBoxBidPriceC.Text);
                //    }
                //    model.CounterParty = ddlCounterPartyC.SelectedValue;
                //    model.LoanName = txtLoanNameC.Text;
                //    if (!string.IsNullOrEmpty(txtBoxOfferPriceC.Text))
                //    {
                //        model.OfferPrice = Convert.ToDecimal(txtBoxOfferPriceC.Text);
                //    }
                //    if (!string.IsNullOrEmpty(txtBoxOfferSpreadC.Text))
                //    {
                //        model.OfferSpread = Convert.ToDecimal(txtBoxOfferSpreadC.Text);
                //    }
                //    // model.MarketValue
                //    model.TimeStamp = DateTime.UtcNow;
                //    model.Traded = chkBoxTradedC.Checked;
                //    //model.CountryID = Convert.ToInt32(ddlRegionA.SelectedValue);
                //    if (txtLoanNameC.SelectedValue != string.Empty)
                //    {
                //        LoansBLL loanBLL = new LoansBLL();
                //        model.Country = loanBLL.GetCoutryIDbyLoanID(txtLoanNameC.SelectedValue);
                //    }
                //    List<QuotesAndTrades> lst = new List<QuotesAndTrades>();
                //    lst.Add(model);
                //    QuotesAndTradesBLL bll = new QuotesAndTradesBLL();

                //    bll.AddImportedQuotesAndTrades(lst);

                //    BindHistoricalQuotesAndTradesTab();

                //    // broadcast that quote and trade has been added
                //    ApplicationHub.NewQuotesAndTradeAdded(model);

                //    if (string.IsNullOrEmpty(hfSelectedLoanC.Value))
                //    {
                //        // Add the Loans as well
                //        Loans loanModel = new Loans();
                //        //loanModel.Amortizing
                //        //loanModel.Bilateral
                //        loanModel.CodeName = txtLoanNameC.Text;
                //        //loanModel.Country
                //        loanModel.CouponFrequency = txtBoxCouponFrequencyLoanC.Text;
                //        loanModel.Currency = txtCurrencyC.Text;
                //        //loanModel.FacilitySize
                //        loanModel.Margin = txtInterestRateC.Text;
                //        loanModel.Margin = txtDiscountMarginC.Text;
                //        loanModel.Maturity_Date = txtMaturityDateC.SelectedDate.Value.ToShortDateString();
                //        //loanModel.Sector
                //        loanModel.Signing_Date = txtBoxTradeDate3.SelectedDate.Value.ToString();

                //        // Persist in database
                //        LoansBLL loanBll = new LoansBLL();
                //        loanBll.SaveLoan(loanModel);

                //        //new loan has been added.
                //        //Broadcast it
                //        ApplicationHub.NewLoanAdded(loanModel);
                //    }
                //    BindLoansTab();
                //    ShowMessage("Message", "Quote and Loan added Successfully");
                //    LogActivity("Quote and Loan C added", "Add the quote and trade on compact view C", string.Empty);
                //}
            }
            catch (Exception ex)
            {
                lblTradeQuoteStatusC.Text = "Error in saving";
                LogActivity("Quote and Loan C added(Unsuccessfull)", "Unable to add the quote and trade", ex.Message);
            }
        }

        //protected void btnAddloan_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        // check to see either the loan has been added or updated
        //        bool isNewLoan = false;
        //        Loans loan = new Loans();
        //        if (!string.IsNullOrEmpty(hfLoanID.Value))
        //        {
        //            isNewLoan = false;
        //            loan.ID = Convert.ToInt32(hfLoanID.Value);
        //            LogActivity("Update the Loan", "Update the existing loan", string.Empty);
        //        }
        //        else
        //        {
        //            isNewLoan = true;
        //            LogActivity("New Loan Added", "Add the new loan", string.Empty);
        //        }

        //        loan.CodeName = txtBoxAddLoanCode.Text;
        //        loan.Borrower = txtBoxAddBorrower.Text;
        //        loan.Sector = txtBoxAddSector.Text;
        //        loan.Signing_Date = txtBoxAddSigningDate.Text;
        //        loan.Maturity_Date = txtBoxAddMaturityDate.Text;
        //        loan.FixedOrFloating = ddlAddFixedOrFloating.SelectedItem.Text;

        //        loan.Margin = txtBoxAddMargin.Text;
        //        loan.Currency = ddlAddCurrency.SelectedItem.Text;
        //        loan.Country = ddlCountry.SelectedItem.Text;
        //        loan.CouponFrequency = ddlAddCouponFrequency.SelectedItem.Text;

        //        loan.FacilitySize = txtBoxFacilitySize.Text;
        //        loan.Bilateral = ddlAddBilateral.SelectedItem.Text == "Y";
        //        loan.Amortizing = ddlAmortizing.SelectedItem.Text;

        //        LoansBLL bll = new LoansBLL();
        //        bll.SaveLoan(loan);
        //        lblAddLoanMessage.Visible = true;

        //        // all loans
        //        List<Loans> allLoans = bll.GetLoans();
        //        BindLoansTab(allLoans);
        //        BindHistoricalQuotesAndTradesTab();
        //        // check to see if the loan has been added or updated
        //        if (isNewLoan)
        //        {
        //            ApplicationHub.NewLoanAdded(loan);
        //        }
        //        else
        //        {
        //            ApplicationHub.RefreshLoans(allLoans);
        //        }

        //        ShowMessage("Message", "Loan added Successfully");
        //    }
        //    catch (Exception ex)
        //    {
        //        lblAddLoanMessage.Visible = true;
        //        lblAddLoanMessage.Text = "Loan failed to save";
        //        LogActivity("New Loan(Unsuccessfull)", "Unable to save the loan", ex.Message);
        //    }
        //}

        #region Cash Flow Calculation

        protected void btnSubmit1_Click(object sender, EventArgs e)
        {
            ComputeCouponDatesAndFractions(txtBoxTradeDate1, txtLoanNameA, txtBoxSettlementDate1, txtBoxMaturityDateA, txtBoxCouponFrequencyLoanA, txtAveLifNonDiscA, grdCalculatedDates1, 1, txtBoxNotional1, txtBoxInterestRateA, txtBoxCurrencyLoanA);

            LogActivity("Compute the CashFlow A", "Compute the cashflow for compact view A", string.Empty);
        }

        protected void btnSubmit2_Click(object sender, EventArgs e)
        {
            ComputeCouponDatesAndFractions(txtBoxTradeDate2, txtLoanNameB, txtBoxSettlementDate2, txtBoxMaturityDateB, txtBoxCouponFrequencyLoanB, txtBoxAveLifNonDiscB, grdCalculatedDates2, 2, txtBoxNotional2, txtInterestRateB, txtCurrencyB);

            LogActivity("Compute the CashFlow B", "Compute the cashflow for compact view B", string.Empty);
        }

        protected void btnSubmit3_Click(object sender, EventArgs e)
        {
            ComputeCouponDatesAndFractions(txtBoxTradeDate3, txtLoanNameC, txtBoxSettlementDate3, txtMaturityDateC, txtBoxCouponFrequencyLoanC, txtBoxAveLifNonDiscC, grdCalculatedDates3, 3, txtBoxNotional3, txtInterestRateC, txtCurrencyC);

            LogActivity("Compute the CashFlow C", "Compute the cashflow for compact view C", string.Empty);
        }

        /*void txtBidPriceA_TextChanged(object sender, EventArgs e)
        {
            int price = Convert.ToInt32(txtBidPriceA.Text);
            //averageLifeA = computeAverageLife();
            //txtBidSpreadA.Text = (100-price) * (360/365.25) * (100/AvgLife) + Margin;
            txtBidSpreadA.Text = "100.0";
        }*/

        // Calculate and show dates
        private void ShowCalculatedDates(TextBox txtNumber, TextBox txtBoxTradeDate, GridView grdCalculatedDates)
        {
            PublicHolidayBLL bll = new PublicHolidayBLL();
            List<tblHoliday> publicHolidays = bll.GetPublicHolidays(1);

            List<CalculatedDates> calculatedList = new List<CalculatedDates>();
            DateTime selectedDate;
            if (DateTime.TryParse(txtBoxTradeDate.Text, out selectedDate))
            {
                // I am going to display dates
                for (int i = 0; i < Convert.ToInt32(txtNumber.Text); i++)
                {
                    // Move on to next 3 month
                    selectedDate = selectedDate.AddMonths(3);

                    // Check the selected date is not in the Public holiday list and not a saturday, sunday
                    // If it exist then we need to move on to the next working day
                    int numberOfDaysPassed = 0;
                    while (IsSaturdaySundayDay(selectedDate) || IsPublicHoliday(publicHolidays, selectedDate))
                    {
                        selectedDate = selectedDate.AddDays(1);
                        numberOfDaysPassed = numberOfDaysPassed + 1;
                    }

                    CalculatedDates cal = new CalculatedDates();

                    cal.CalculatedDate = selectedDate;
                    cal.Fraction = String.Format("{0:00.00}", numberOfDaysPassed / 365.25);

                    calculatedList.Add(cal);
                }

                grdCalculatedDates.DataSource = calculatedList;
                grdCalculatedDates.DataBind();
            }
        }

        /// <summary>
        /// Check of working day
        /// </summary>
        /// <param name="selectedDate"></param>
        /// <returns></returns>
        private bool IsSaturdaySundayDay(DateTime selectedDate)
        {
            // if it is holiday
            if (selectedDate.DayOfWeek == DayOfWeek.Sunday || selectedDate.DayOfWeek == DayOfWeek.Saturday)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Check the date is not in the public holidays
        /// </summary>
        /// <param name="publicHolidays"></param>
        /// <param name="selectedDate"></param>
        /// <returns></returns>
        private bool IsPublicHoliday(List<tblHoliday> publicHolidays, DateTime selectedDate)
        {
            // Check the selected date is not in the Public holiday list
            // If it exist then we need to move on to the next working day
            foreach (var item in publicHolidays)
            {
                // Match day and month not the year
                if (item.Date.Day == selectedDate.Day && item.Date.Month == selectedDate.Month)
                {
                    return true;
                }
            }
            return false;
        }

        protected void grdCalculatedDates_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                System.Web.UI.WebControls.Label lblDay = e.Row.FindControl("lblDay") as System.Web.UI.WebControls.Label;
                CalculatedDates model = e.Row.DataItem as CalculatedDates;
                String TheDayOfWeek = model.CalculatedDate.DayOfWeek.ToString();
                lblDay.Text = TheDayOfWeek.Substring(0, 3);
            }
        }

        #endregion


        /// <summary>
        /// Upload the CSV file and then save it's data to database
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void UploadButton_Click(object sender, EventArgs e)
        {

        }

        private void ComputeCouponDatesAndFractions(RadDatePicker txtBoxTradeDate, RadComboBox txtBoxLoanName, RadDatePicker txtBoxSettlementDate,
           RadDatePicker txtBoxMaturityDate, RadTextBox txtBoxCouponFrequency, RadTextBox txtNonDiscountedAverageLife, RadGrid grdCalculatedDates, int type, RadTextBox txtNotional, RadTextBox txtMargin, RadTextBox txtCurrency)
        {
            try
            {


                int countryID = getCountryForLoan(txtBoxLoanName);
                //PublicHolidayBLL bll = new PublicHolidayBLL();
                //List<tblHoliday> publicHolidays = bll.GetPublicHolidays(countryID);


                
                List<CalculatedDates> calculatedList = new List<CalculatedDates>();

                DateTime cpnDT;
                cpnDT = AddBusinessDays(DateTime.Now, 10);
                LoanScheduleBL loanScheduleBL = new LoanScheduleBL();
                DataTable dtSchedule = loanScheduleBL.GenerateTable(15, 10, cpnDT, txtBoxCouponFrequency.Text, txtNotional.Text, txtBoxMaturityDate.SelectedDate.Value, cpnDT, Convert.ToDecimal(txtMargin.Text), Convert.ToString(txtCurrency.Text), Convert.ToDateTime(txtBoxTradeDate.SelectedDate.Value), txtBoxSettlementDate.SelectedDate.Value);
                if (dtSchedule.Rows.Count > 0)
                {
                    LoansBLL loanBL = new LoansBLL();

                    int loanID = loanBL.GetLoanByCode(txtBoxLoanName.Text).ID;
                    loanScheduleBL.EditSchedule(loanID, dtSchedule);
                }
                


                foreach (DataRow dr in dtSchedule.Rows)
                {
                    CalculatedDates cal = new CalculatedDates();
                    cal.Fraction = dr["CoupFrac"].ToString();
                    cal.CalculatedDate = Convert.ToDateTime(dr["EndDate"]);
                    calculatedList.Add(cal);
                }


                grdCalculatedDates.Visible = true;
                grdCalculatedDates.DataSource = calculatedList;
                grdCalculatedDates.DataBind();
               
               


                switch (type)
                {
                    case 1:
                        tempCalculatedList1 = calculatedList;
                        break;
                    case 2:
                        tempCalculatedList2 = calculatedList;
                        break;
                    case 3:
                        tempCalculatedList3 = calculatedList;
                        break;
                    default:
                        break;
                }


            }
            catch (Exception)
            {


            }
        }
        private void ComputeCouponDatesAndFractionsOld(RadDatePicker txtBoxTradeDate, RadComboBox txtBoxLoanName, RadDatePicker txtBoxSettlementDate,
            RadDatePicker txtBoxMaturityDate, RadTextBox txtBoxCouponFrequency, RadTextBox txtNonDiscountedAverageLife, RadGrid grdCalculatedDates, int type, RadTextBox txtNotional, RadTextBox txtMargin, RadTextBox txtCurrency)
        {
            try
            {


                int countryID = getCountryForLoan(txtBoxLoanName);
                PublicHolidayBLL bll = new PublicHolidayBLL();
                List<tblHoliday> publicHolidays = bll.GetPublicHolidays(countryID);
                List<CalculatedDates> calculatedList = new List<CalculatedDates>();

                DateTime cpnDT;
                cpnDT = AddBusinessDays(DateTime.Now, 10);
                LoanScheduleBL loanScheduleBL = new LoanScheduleBL();
                DataTable dtSchedule = loanScheduleBL.GenerateTable(15, 10, cpnDT, txtBoxCouponFrequency.Text, txtNotional.Text, txtBoxMaturityDate.SelectedDate.Value, cpnDT, Convert.ToDecimal(txtMargin.Text), Convert.ToString(txtCurrency.Text), Convert.ToDateTime(txtBoxTradeDate.SelectedDate.Value), txtBoxSettlementDate.SelectedDate.Value);

                DateTime currentCouponDate;
                DateTime previousCouponDate;
                DateTime maturityDate;
                DateTime tradeDate;

                DateTime tradeDate1 = DateTime.Now.Date;
                if (txtBoxTradeDate.SelectedDate != null)
                {
                    tradeDate1 = txtBoxTradeDate.SelectedDate.Value;
                }

                //DateTime settlementDate = DateTime.Now.Date;
                //if (txtBoxSettlementDate.SelectedDate != null)
                //{
                //    settlementDate = AddBusinessDays(txtBoxSettlementDate.SelectedDate.Value, 10);  
                //}
                DateTime boxMaturityDate = DateTime.Now.Date;

                DateTime.TryParse(txtBoxSettlementDate.SelectedDate.Value.ToShortDateString(), out currentCouponDate);
                DateTime.TryParse(txtBoxSettlementDate.SelectedDate.Value.ToShortDateString(), out previousCouponDate);
                if (txtBoxMaturityDate.SelectedDate != null)
                {
                    boxMaturityDate = txtBoxMaturityDate.SelectedDate.Value;
                }
                DateTime.TryParse(boxMaturityDate.ToShortDateString(), out maturityDate);
                DateTime.TryParse(tradeDate1.ToShortDateString(), out tradeDate);

                if (DateTime.TryParse(txtBoxSettlementDate.SelectedDate.Value.ToShortDateString(), out currentCouponDate))
                {
                    int cF = 0;
                    System.Numeric.Frequency f = new System.Numeric.Frequency();
                    while (CurrentCouponDateLessThanMaturityDate(currentCouponDate, maturityDate))
                    {
                        if (txtBoxCouponFrequency.Text == "Annual")
                        {
                            cF = 1;
                            f = System.Numeric.Frequency.Annual;
                        }
                        if (txtBoxCouponFrequency.Text == "Semi-Annual")
                        {
                            cF = 2;
                            f = System.Numeric.Frequency.SemiAnnual;
                        }
                        if (txtBoxCouponFrequency.Text == "Quarterly")
                        {
                            cF = 4;
                            f = System.Numeric.Frequency.Quarterly;
                        }
                        if (txtBoxCouponFrequency.Text == "Monthly")
                        {
                            cF = 12;
                        }



                        int numberOfDaysPassed = 0;
                        // if a weekend date or holiday move forward
                        //while (IsSaturdaySundayDay(currentCouponDate) || IsPublicHoliday(publicHolidays, currentCouponDate))
                        //{
                        //    currentCouponDate = currentCouponDate.AddDays(1);
                        //    numberOfDaysPassed = numberOfDaysPassed + 1;
                        //}


                        // Compute next coupon date using CoupNCD and correct for weekends
                        CalculatedDates cal = new CalculatedDates();
                        System.Numeric.DayCountBasis d = System.Numeric.DayCountBasis.Actual365;
                        previousCouponDate = currentCouponDate;
                        currentCouponDate = System.Numeric.Financial.CoupNCD(currentCouponDate, maturityDate, f, d);
                        DateTime previousBusinessDay = PreviousBusinessDay(currentCouponDate);
                        cal.CalculatedDate = previousBusinessDay;

                        //numberOfDaysPassed = currentCouponDate.Subtract(tradeDate).Days;  // removed 31-03- 2014 asked by avr
                        numberOfDaysPassed = currentCouponDate.Subtract(txtBoxSettlementDate.SelectedDate.Value).Days;

                        //cal.Fraction = String.Format("{0:00.000}", numberOfDaysPassed / 365.25);
                        cal.Fraction = String.Format("{0:00.00}", numberOfDaysPassed / 365.25);
                        cal.ActualFraction = numberOfDaysPassed / 365.25;
                        calculatedList.Add(cal);



                    }
                    if (calculatedList != null && calculatedList.Count > 0)
                    {
                        // When the cashflow is computed take the average of the fractions and set that to the non discounted average life text box. 
                        //  txtNonDiscountedAverageLife.Text =Convert.ToDecimal( calculatedList.Last().ActualFraction).ToString("0.00");
                    }
                    grdCalculatedDates.Visible = true;
                    grdCalculatedDates.DataSource = calculatedList;
                    grdCalculatedDates.DataBind();
                    switch (type)
                    {
                        case 1:
                            tempCalculatedList1 = calculatedList;
                            break;
                        case 2:
                            tempCalculatedList2 = calculatedList;
                            break;
                        case 3:
                            tempCalculatedList3 = calculatedList;
                            break;
                        default:
                            break;
                    }

                }
                // tempLegend = txtBoxLoanName;
            }
            catch (Exception)
            {


            }
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

        private int getCountryForLoan(RadComboBox txtBoxLoanName)
        {
            return 1;
        }

        private int getNoOfMonthsToAdd(int cF)
        {
            //if (cF == 1)
            //{
            //    return 12;
            //}
            if (cF == 1)
            {
                return 1;
            }

            if (cF == 6)
            {
                return 6;
            }

            if (cF == 3)
            {
                return 3;
            }

            return 12;
        }

        private bool CurrentCouponDateLessThanMaturityDate(DateTime currentCouponDate, DateTime maturityDate)
        {
            int result = DateTime.Compare(currentCouponDate, maturityDate);

            if (result < 0)
                return true;
            else if (result == 0)
                return false;
            else
                return false;
        }

        // Clear logic

        private void ClearA()
        {
            txtBoxInterestRateA.Text = string.Empty;
            txtBoxTradeDate1.SelectedDate = DateTime.Now.Date;
            txtBoxSettlementDate1.SelectedDate = DateTime.Now.Date;
            txtBoxMaturityDateA.SelectedDate.GetValueOrDefault();
            txtBoxCurrencyLoanA.Text = string.Empty;
            txtBoxCouponFrequencyLoanA.Text = string.Empty;
            txtBoxLastFixingA.Text = string.Empty;
            //txtBoxIRCouponA.Text = string.Empty;
            txtBidPriceA.Text = string.Empty;
            txtBidSpreadA.Text = string.Empty;
            txtOfferPriceA.Text = string.Empty;
            txtOfferSpreadA.Text = string.Empty;
            txtBoxNotional1.Text = string.Empty;
            txtDiscountMarginA.Text = string.Empty;
            txtIRRA.Text = string.Empty;
            txtAveLifDiscA.Text = string.Empty;
            txtAveLifRiskyDiscA.Text = string.Empty;
            txtAveLifNonDiscA.Text = string.Empty;
            // ddlRegionA.SelectedValue = "250";
            // txtLoanNameA.Entries.Clear();

            grdCalculatedDates1.Visible = false;
            txtLoanNameA.ClearSelection();
            txtBoxTradeDate1.Clear();
            txtBoxSettlementDate1.Clear();
            txtBoxMaturityDateA.Clear();
            ddlCounterPartyA.SelectedValue = "CounterPartyA";
            chkBoxTradedA.Checked = false;
            // ddlCureA.SelectedIndex = 0;
            LogActivity("Loan A Clear", "Clear the compact cashflow A", string.Empty);
        }

        protected void btnClearA_Click(object sender, EventArgs e)
        {
            // txtLoanNameA.Text = string.Empty; // Nik Commented
            ClearA();

        }

        private void ClearB()
        {
            txtInterestRateB.Text = string.Empty;
            txtBoxTradeDate2.SelectedDate = DateTime.Now.Date;
            txtBoxSettlementDate2.SelectedDate = DateTime.Now.Date;
            txtBoxMaturityDateB.SelectedDate.GetValueOrDefault();
            txtCurrencyB.Text = string.Empty;
            txtBoxCouponFrequencyLoanB.Text = string.Empty;
            txtBoxLastFixingB.Text = string.Empty;
            // txtBoxIRCouponB.Text = string.Empty;
            txtBoxBidPriceB.Text = string.Empty;
            txtBoxBidSpreadB.Text = string.Empty;
            txtBoxOfferPriceB.Text = string.Empty;
            txtBoxOfferSpreadB.Text = string.Empty;
            txtBoxNotional2.Text = string.Empty;
            txtDiscountMarginB.Text = string.Empty;
            txtBoxIRRB.Text = string.Empty;
            txtBoxAveLifDiscB.Text = string.Empty;
            txtBoxAveLifRiskyDiscB.Text = string.Empty;
            txtBoxAveLifNonDiscB.Text = string.Empty;
            //ddlRegionB.SelectedValue = "250";

            // txtLoanNameB.Entries.Clear();
            grdCalculatedDates2.Visible = false;

            txtLoanNameB.ClearSelection();
            txtBoxTradeDate2.Clear();
            txtBoxSettlementDate2.Clear();
            txtBoxMaturityDateB.Clear();
            ddlCounterPartyB.SelectedValue = "CounterPartyB";
            chkBoxTradedB.Checked = false;
            //ddlCurveB.SelectedIndex = 0;

            LogActivity("Loan B Clear", "Clear the compact cashflow B", string.Empty);
        }

        protected void btnClearB_Click(object sender, EventArgs e)
        {
            //  txtLoanNameB.Text = string.Empty; Nik Commented
            ClearB();
        }

        private void ClearC()
        {
            txtInterestRateC.Text = string.Empty;
            txtBoxTradeDate3.SelectedDate = DateTime.Now.Date;
            txtBoxSettlementDate3.SelectedDate = DateTime.Now.Date;
            txtMaturityDateC.SelectedDate.GetValueOrDefault();
            txtCurrencyC.Text = string.Empty;
            txtBoxCouponFrequencyLoanC.Text = string.Empty;
            txtLastFixingC.Text = string.Empty;
            //  txtBoxIRCouponC.Text = string.Empty;
            txtBoxBidPriceC.Text = string.Empty;
            txtBoxBidSpreadC.Text = string.Empty;
            txtBoxOfferPriceC.Text = string.Empty;
            txtBoxOfferSpreadC.Text = string.Empty;
            txtBoxNotional3.Text = string.Empty;
            txtDiscountMarginC.Text = string.Empty;
            txtBoxIRRC.Text = string.Empty;
            txtBoxAveLifDiscC.Text = string.Empty;
            txtBoxAveLifRiskyDiscC.Text = string.Empty;
            txtBoxAveLifNonDiscC.Text = string.Empty;
            //ddlRegionC.SelectedValue = "250";

            // txtLoanNameC.Entries.Clear();
            grdCalculatedDates3.Visible = false;


            txtLoanNameC.ClearSelection();
            txtBoxTradeDate3.Clear();
            txtBoxSettlementDate3.Clear();
            txtMaturityDateC.Clear();
            ddlCounterPartyC.SelectedValue = "CounterPartyC";
            chkBoxTradedC.Checked = false;
            //  ddlCurveSourceC.SelectedIndex = 0;
            LogActivity("Loan C Clear", "Clear the compact cashflow C", string.Empty);
        }

        protected void btnClearC_Click(object sender, EventArgs e)
        {
            // txtLoanNameC.Text = string.Empty; Nik Commneted
            ClearC();
        }

        protected void btnAddNewLoan_Click(object sender, EventArgs e)
        {
            try
            {
                LoansBLL bll = new LoansBLL();
                if (txtBoxAddMaturityDate.SelectedDate.ToString() != "" && txtBoxAddSigningDate.SelectedDate.ToString() != "")
                {
                    DateTime dtMaturity = Convert.ToDateTime(txtBoxAddMaturityDate.SelectedDate.Value);
                    DateTime dtSigning = Convert.ToDateTime(txtBoxAddSigningDate.SelectedDate.Value);
                    SettingsBLL settingBL = new SettingsBLL();
                    Setting setting = settingBL.GetSettingyear("Loan Year Settings");
                    if (setting != null)
                    {
                        TimeSpan ts = dtMaturity - dtSigning;
                        int differenceYear = Convert.ToInt32((ts.TotalDays) / 365);
                        int year = Convert.ToInt16(setting.Value);

                        if (dtMaturity.Year > dtSigning.Year && (differenceYear) > year)
                        {
                            lblMessage.Text = "Maturity Date is not Valid";
                            lblMessage.Visible = true;
                            return;
                        }
                    }
                }
                else
                {
                    //lblMessage.Text = "Enter the Details";
                    //lblMessage.Visible = true;
                    RadWindowManager1.RadAlert("Maturity Date and Signing Date required", 300, 150, "realedge associates", "alertCallBackFn");
                    return;
                }

                // check to see either the loan has been added or updated
                bool isNewLoan = false;
                Loans loan = new Loans();
                if (!string.IsNullOrEmpty(hfLoanID.Value))
                {
                    isNewLoan = false;
                    loan.ID = Convert.ToInt32(hfLoanID.Value);
                    LogActivity("Update the Loan", "Update the existing loan", string.Empty);
                }
                else
                {
                    isNewLoan = true;
                    LogActivity("New Loan Added", "Add the new loan", string.Empty);
                }

                List<RatingDetails> ratingDetails = new List<RatingDetails>();

                ratingDetails = ShowCheckedNodes(tvCreditRating);

                foreach (var item in ratingDetails)
                {
                    switch (item.agencyName)
                    {
                        case "Moody's":
                            loan.CreditRatingModys = item.rating;
                            break;
                        case "S&P's":
                            loan.CreditRatingSPs = item.rating;
                            break;
                        case "Fitch":
                            loan.CreditRatingFitch = item.rating;
                            break;
                        case "ING":
                            loan.CreditRatingING = item.rating;
                            break;
                        default:
                            break;
                    }
                }

                loan.CodeName = ddlAddLoanCode.Text;
                loan.Borrower = ddlBorrower.Text;
                loan.Sector = ddlSector.SelectedValue; //txtBoxAddSector.Text;
                loan.Signing_Date = txtBoxAddSigningDate.SelectedDate.Value.ToShortDateString();
                loan.Maturity_Date = txtBoxAddMaturityDate.SelectedDate.Value.ToShortDateString();
                loan.FixedOrFloating = ddlAddFixedOrFloating.SelectedValue;

                loan.Margin = txtBoxAddMargin.Text;
                loan.Currency = ddlAddCurrency.SelectedValue;
                loan.Country = ddlCountry.SelectedValue;
                loan.CouponFrequency = ddlAddCouponFrequency.SelectedValue;
                loan.Gurantor = txtGurantor.Text;
                loan.Grid = txtGrid.Text;
                loan.SummitCreditEntity = txtSummitCredit.Text;
                if (txtBoxFacilitySize.Text != string.Empty)
                {
                    loan.FacilitySize = Convert.ToDecimal(txtBoxFacilitySize.Text).ToString("N");
                }
                loan.Bilateral = ddlAddBilateral.SelectedValue == "Yes";
                loan.Amortizing = ddlAmortizing.SelectedValue;
                loan.PP = txtPP.Text;
                //    loan.CreditRating = txtCRating.Text;
                loan.StructureID = txtStructureID.Text;
                //Coupon Date and Notional are missing.
                if (txtCouponDate.SelectedDate != null)
                {
                    loan.CouponDate = txtCouponDate.SelectedDate.Value.ToShortDateString();
                }
                loan.Notional = Convert.ToDecimal(txtNotional.Text).ToString("N").Trim();
                if (ddlAmortizing.SelectedValue == "Yes")
                {
                    if (txtAmortisationsStartDate.SelectedDate != null)
                    {
                        loan.AmortisationsStartPoint = txtAmortisationsStartDate.SelectedDate.Value.ToShortDateString();
                    }
                    if (txtAmortisations.Text != string.Empty)
                    {
                        loan.NoOfAmortisationPoint = Convert.ToInt16(txtAmortisations.Text);
                    }

                }
                else
                {
                    loan.Amortizing = "Yes";
                    DateTime cpnDT;
                    if (txtCouponDate.SelectedDate.ToString() == string.Empty)
                    {
                        cpnDT = AddBusinessDays(DateTime.Now, 10);
                        loan.AmortisationsStartPoint = cpnDT.ToShortDateString();
                    }
                    else
                    {
                        cpnDT = Convert.ToDateTime(txtCouponDate.SelectedDate);
                        loan.AmortisationsStartPoint = txtCouponDate.SelectedDate.Value.ToShortDateString();
                    }


                    loan.NoOfAmortisationPoint = 10;
                    LoanScheduleBL loanScheduleBL = new LoanScheduleBL();
                    DateTime tradeDate = DateTime.Now;

                    DataTable dtSchedule = loanScheduleBL.GenerateTable(15, Convert.ToInt16(loan.NoOfAmortisationPoint), Convert.ToDateTime(loan.AmortisationsStartPoint), loan.CouponFrequency.ToString(), loan.Notional.ToString(), Convert.ToDateTime(loan.Maturity_Date), Convert.ToDateTime(loan.CouponDate), Convert.ToDecimal(loan.Margin), Convert.ToString(loan.Currency), Convert.ToDateTime(tradeDate), AddBusinessDays(Convert.ToDateTime(tradeDate), 10));
                    Session["LoanSchedule"] = dtSchedule;
                }
                DAL.Login login = Session["LogedInUser"] as DAL.Login;
                loan.CreatedBy = login.Name;
                loan.LastEdited = DateTime.Now;

                switch (_operation)
                {
                    case "Add":
                        if (bll.CheckForLoanCode(ddlAddLoanCode.Text.Trim()))
                        {
                            bll.SaveLoan(loan);
                            login = Session["LogedInUser"] as DAL.Login;
                            LoanHistory loanHistory = new LoanHistory();
                            LoanHistoryBL historyBL = new LoanHistoryBL();
                            loanHistory.Action = "Add";
                            loanHistory.LoanName = loan.CodeName;
                            loanHistory.UserName = login.Name;
                            loanHistory.LastModified = DateTime.Now;
                            historyBL.SaveHistory(loanHistory);
                            // lblAddLoanMessage.Visible = true;          on 7-1
                            CalculateFactors();
                            LoanScheduleBL loanScheduleBL = new LoanScheduleBL();
                            if (Session["LoanSchedule"] != null)
                            {
                                dt1 = (DataTable)Session["LoanSchedule"];
                            }

                            foreach (DataRow dr in dt1.Rows)
                            {
                                LoanSchedule loanSchedule = new LoanSchedule();
                                loanSchedule.LoanID = loan.ID;
                                loanSchedule.StartDate = Convert.ToDateTime(dr["StartDate"]);
                                loanSchedule.EndDate = Convert.ToDateTime(dr["EndDate"]);
                                loanSchedule.CoupFrac = Convert.ToDecimal(dr["CoupFrac"]);
                                loanSchedule.Notation = Convert.ToDecimal(dr["Notation"]);
                                loanSchedule.Amortisation = Convert.ToDecimal(dr["Amortisation"]);
                                loanSchedule.Factor = Convert.ToDecimal(dr["Factor"]);
                                loanSchedule.Spread = Convert.ToDecimal(dr["Spread"]);
                                loanSchedule.AllInRate = Convert.ToDecimal(dr["AllInRate"]);
                                loanSchedule.CouponPaymentDate = Convert.ToDateTime(dr["CouponPaymentDate"]);
                                loanSchedule.RiskFreeDP1 = Convert.ToDecimal(dr["RiskFreeDP1"]);
                                loanSchedule.RiskFreeDP2 = Convert.ToDecimal(dr["RiskFreeDP2"]);
                                loanSchedule.FloatingRate = Convert.ToDecimal(dr["FloatingRate"]);
                                loanSchedule.Interest = Convert.ToDecimal(dr["Interest"]);
                                loanSchedule.Days = Convert.ToInt16(dr["Days"]);
                                loanSchedule.AmortisationInt = Convert.ToDecimal(dr["AmortisationInt"]);
                                loanSchedule.CreatedOn = DateTime.UtcNow;
                                loanScheduleBL.SaveLoanSchedule(loanSchedule);
                            }
                            //lblMessage.Text = "Loan added Successfully";
                            //lblMessage.Visible = true;
                            RadWindowManager1.RadAlert(ddlAddLoanCode.Text + " added Successfully", 300, 150, "realedge associates", "alertCallBackFn");
                        }
                        else
                        {
                            InsertDuplicateLoan();
                        }
                        break;
                    case "Edit":
                        bll.EditLoan(loan, Convert.ToInt16(Session["EditLoanID"]), 2);
                        login = Session["LogedInUser"] as DAL.Login;
                        LoanHistory loanHis = new LoanHistory();
                        LoanHistoryBL historiesBL = new LoanHistoryBL();
                        loanHis.Action = "Edit";
                        loanHis.LoanName = loan.CodeName;
                        loanHis.UserName = login.Name;
                        loanHis.LastModified = DateTime.Now;
                        historiesBL.SaveHistory(loanHis);
                        CalculateFactors();
                        LoanScheduleBL scheduleBL = new LoanScheduleBL();
                        if (Session["LoanSchedule"] != null)
                        {
                            dt1 = (DataTable)Session["LoanSchedule"];
                        }
                        scheduleBL.EditSchedule(Convert.ToInt16(Session["EditLoanID"]), dt1);

                        // Clear();
                        //lblMessage.Text = "Loan updated Successfully";
                        //lblMessage.Visible = true;
                        RadWindowManager1.RadAlert(ddlAddLoanCode.Text + " updated Successfully", 300, 150, "realedge associates", "alertCallBackFn");

                        //lblConfirm.Text = "Are you sure you want to add this loan to database?";
                        //_operation = "Add";
                        //Session.Remove("EditLoanID");
                        break;
                    default:
                        break;
                }

                // all loans
                List<Loans> allLoans = bll.GetLoans();
                BindLoansTab(allLoans);
                BindLoansData();
                BindHistoricalQuotesAndTradesTab();
                // check to see if the loan has been added or updated
                if (isNewLoan)
                {
                    ApplicationHub.NewLoanAdded(loan);
                }
                else
                {
                    ApplicationHub.RefreshLoans(allLoans);
                }
                hdnSaved.Value = "Y";
                BindLoanCode();
            }
            catch (Exception ex)
            {
                //lblAddLoanMessage.Visible = true;                     on 7-1
                //lblAddLoanMessage.Text = "Loan failed to save";           on 7-1
                //  lblMessage.Text = "Loan failed to save";
                lblMessage.Text = ex.Message;
                lblMessage.Text = ex.InnerException.Message;
                lblMessage.Visible = true;
                LogActivity("New Loan(Unsuccessfull)", "Unable to save the loan", ex.Message);
            }
        }

        private void InsertDuplicateLoan()
        {
            try
            {
                DuplicateLoan duplicateLoan = new DuplicateLoan();
                LoansBLL loanBLL = new LoansBLL();
                duplicateLoan.CodeName = ddlAddLoanCode.Text;
                duplicateLoan.Borrower = ddlBorrower.Text;
                duplicateLoan.Sector = ddlSector.SelectedValue;//txtBoxAddSector.Text;
                duplicateLoan.Signing_Date = txtBoxAddSigningDate.SelectedDate.Value.ToShortDateString();
                duplicateLoan.Maturity_Date = txtBoxAddMaturityDate.SelectedDate.Value.ToShortDateString();
                duplicateLoan.FixedOrFloating = ddlAddFixedOrFloating.SelectedItem.Text;

                duplicateLoan.Margin = txtBoxAddMargin.Text;
                duplicateLoan.Currency = ddlAddCurrency.SelectedItem.Text;
                duplicateLoan.Country = ddlCountry.SelectedItem.Text;
                duplicateLoan.CouponFrequency = ddlAddCouponFrequency.SelectedItem.Text;

                duplicateLoan.FacilitySize = Convert.ToDecimal(txtBoxFacilitySize.Text).ToString("N");
                duplicateLoan.Bilateral = ddlAddBilateral.SelectedValue == "Yes";
                duplicateLoan.Amortizing = ddlAmortizing.SelectedItem.Text;
                loanBLL.SaveDuplicateLoan(duplicateLoan);
            }
            catch (Exception)
            {
            }
        }

        private void Clear()
        {
            txtBoxAddLoanCode.Text = string.Empty;
            ddlBorrower.ClearSelection();
            // txtBoxAddSector.Text = string.Empty;
            ddlSector.SelectedValue = string.Empty;
            txtBoxFacilitySize.Text = string.Empty;
            txtBoxAddMargin.Text = string.Empty;
            txtBoxAddLoanCode.Text = string.Empty;

            //ddlAddFixedOrFloating.SelectedItem.Text = "Floating";
            //ddlAddCurrency.SelectedItem.Text = "EUR";
            ddlAddFixedOrFloating.SelectedValue = "Floating";
            ddlAddCurrency.SelectedValue = "EUR";

            // bindCountry();
            ddlAddLoanCode.ClearSelection();
            ddlCountry.SelectedValue = "Russia";
            //ddlAddCouponFrequency.SelectedItem.Text = "Monthly";
            //ddlAddBilateral.SelectedItem.Text = "No";
            //ddlAmortizing.SelectedItem.Text = "No";
            ddlAddCouponFrequency.SelectedValue = "Quarterly";
            ddlAddBilateral.SelectedValue = "No";
            ddlAmortizing.SelectedValue = "No";
            txtBoxAddSigningDate.Clear();
            txtBoxAddMaturityDate.Clear();
            grdAmortizing.Visible = false;
            btnCalculatSchedule.Visible = false;
            pnlAmortizing.Visible = false;
            grdAmortizing.DataSource = null;
            grdAmortizing.DataBind();
            //  txtCRating.Text = string.Empty;
            txtStructureID.Text = string.Empty;
            txtPP.Text = string.Empty;
            txtCouponDate.Clear();
            txtNotional.Text = string.Empty;
            txtAmortisationsStartDate.Clear();
            txtAmortisations.Text = string.Empty;
            txtNotional.Text = Convert.ToDecimal(10000000).ToString("N");
            //  txtBoxFacilitySize.Text = Convert.ToDecimal(10000000).ToString("N");
            trDate.Visible = false;
            trNo.Visible = false;
            tblAmortisation.Visible = false;
        }

        private void bindCountry()
        {
            ddlCountry.DataSource = new CountryBL().GetCountry();
            ddlCountry.DataValueField = "Name";
            ddlCountry.DataTextField = "Name";
            ddlCountry.DataBind();

            ddlQuoteCountry.DataSource = new CountryBL().GetCountry();
            ddlQuoteCountry.DataValueField = "Name";
            ddlQuoteCountry.DataTextField = "Name";
            ddlQuoteCountry.DataBind();

        }

        private void FillData()
        {
            try
            {
                Loans loan = new Loans();
                LoansBLL bll = new LoansBLL();
                loan = bll.GetLoanByID(Convert.ToInt16(Session["EditLoanID"]));
                ddlAddLoanCode.SelectedValue = loan.CodeName;
                txtBoxAddLoanCode.Text = loan.CodeName;
                ddlBorrower.SelectedValue = loan.Borrower;
                //  txtBoxAddSector.Text = loan.Sector;
                ddlSector.SelectedValue = loan.Sector;
                if (loan.Signing_Date != null)
                    txtBoxAddSigningDate.SelectedDate = Convert.ToDateTime(loan.Signing_Date);
                if (loan.Maturity_Date != null)
                    txtBoxAddMaturityDate.SelectedDate = Convert.ToDateTime(loan.Maturity_Date);
                ddlAddFixedOrFloating.SelectedValue = loan.FixedOrFloating;

                txtBoxAddMargin.Text = loan.Margin;
                ddlAddCurrency.SelectedValue = loan.Currency;

                bindCountry();
                if (loan.Country != null)
                {
                    CountryBL countryBL = new CountryBL();
                    int id = countryBL.GetCountryFromName(loan.Country);
                    if (id != 0)
                        ddlCountry.SelectedValue = Convert.ToString(id);
                }
                ddlAddCouponFrequency.SelectedValue = loan.CouponFrequency;

                txtBoxFacilitySize.Text = Convert.ToDecimal(loan.FacilitySize).ToString("N");
                if (loan.Bilateral == true)
                    ddlAddBilateral.SelectedValue = "Yes";
                else
                    ddlAddBilateral.SelectedValue = "No";
                ddlAmortizing.SelectedValue = loan.Amortizing;
                CheckNodes(tvCreditRating, loan);
                //txtCouponDate.
                //txtNotional
                txtPP.Text = loan.PP;
                //     txtCRating.Text = loan.CreditRating;
                txtStructureID.Text = loan.StructureID;
                txtBoxAddMargin.Text = loan.Margin;
                if (loan.CouponDate != null)
                    txtCouponDate.SelectedDate = Convert.ToDateTime(loan.CouponDate);
                txtNotional.Text = Convert.ToDecimal(loan.Notional).ToString("N");
                if (ddlAmortizing.SelectedValue == "Yes")
                {
                    if (loan.AmortisationsStartPoint != null)
                        txtAmortisationsStartDate.SelectedDate = Convert.ToDateTime(loan.AmortisationsStartPoint);
                    txtAmortisations.Text = loan.NoOfAmortisationPoint.ToString();
                    trDate.Visible = true;
                    trNo.Visible = true;
                    tblAmortisation.Visible = true;
                }
                txtSummitCredit.Text = loan.SummitCreditEntity;
                txtGrid.Text = loan.Grid;
                txtGurantor.Text = loan.Gurantor;
                LoanScheduleBL scheduleBL = new LoanScheduleBL();
                List<LoanSchedule> scheduleList = scheduleBL.GetLoanByID(loan.ID);

                if (scheduleList != null && scheduleList.Count > 0)
                {
                    dt1.Clear();
                    //dt1.Columns.Add("ID", typeof(int));
                    //dt1.Columns.Add("StartDate", typeof(DateTime));
                    //dt1.Columns.Add("EndDate", typeof(DateTime));
                    //dt1.Columns.Add("Notional", typeof(long));
                    //dt1.Columns.Add("Factor", typeof(float));

                    for (int i = 0; i < scheduleList.Count; i++)
                    {
                        //  dt1.Rows.Add(scheduleList[i].ID, scheduleList[i].StartDate, scheduleList[i].EndDate, scheduleList[i].Notation, scheduleList[i].Factor);
                        dt1.Rows.Add(i + 1, scheduleList[i].StartDate.Value.ToShortDateString(), scheduleList[i].EndDate.Value.ToShortDateString(), scheduleList[i].CoupFrac, scheduleList[i].Notation, scheduleList[i].Amortisation, scheduleList[i].Factor);
                    }
                    pnlAmortizing.Visible = true;
                    grdAmortizing.Visible = true;
                    btnCalculatSchedule.Visible = true;
                    grdAmortizing.DataSource = dt1;
                    grdAmortizing.DataBind();
                }
                hfLoanID.Value = loan.ID.ToString();
                lblConfirm.Text = "Loan already exists, do you wish to overwrite?";
                _operation = "Edit";
            }
            catch (Exception)
            {
            }
        }

        //protected void txtLoanNameA_TextChanged(object sender, AutoCompleteTextEventArgs e)
        //{
        //    SetLoanA();
        //    Session.Add("LoanType", "A");
        //    Session.Add("LoanNameA", txtLoanNameA.Text);
        //}

        protected void txtLoanNameA_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            try
            {
                Session.Add("Trade1", txtBoxTradeDate1.SelectedDate);
                SetLoanA(); string strIRR = string.Empty;
                //try
                //{
                //    strIRR = Convert.ToString(CalculateIRR(txtLoanNameA.Text.Trim(), txtBidPriceA.Text.Trim()));
                //}
                //catch (Exception ex)
                //{
                //    RadWindowManager1.RadAlert(ex.Message, 330, 180, "realedge associates", "alertCallBackFn");
                //}

                //if (strIRR == "NaN")
                //{
                //    RadWindowManager1.RadAlert("Schedule has not been computed for this loan so IRR cannot be computed", 330, 180, "realedge associates", "alertCallBackFn");
                //}
                //else
                //{ txtIRRA.Text = strIRR; }

                try
                {
                    txtAveLifNonDiscA.Text = AverageLifeNonDiscount(txtLoanNameA.Text);
                    //txtAveLifDiscA.Text = AverageLifeDisc(txtLoanNameA.Text);
                    //txtAveLifRiskyDiscA.Text = AverageLifeRiskDisc(txtLoanNameA.Text);
                    //txtAvgLifeA.Text = AverageLife(txtLoanNameA.Text);
                }
                catch (Exception)
                {


                }

                Session.Add("LoanType", "A");
                Session.Add("LoanNameA", txtLoanNameA.Text);
            }
            catch (Exception ex)
            {
                Session.Add("LoanType", "A");
                Session.Add("LoanNameA", txtLoanNameA.Text);
                RadWindowManager1.RadAlert(ex.Message, 330, 180, "realedge associates", "alertCallBackFn");
            }
        }

        protected void txtLoanNameB_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            try
            {
                Session.Add("Trade2", txtBoxTradeDate2.SelectedDate);
                SetLoanB();
                //string strIRR = Convert.ToString(CalculateIRR(txtLoanNameB.Text.Trim(), txtBoxBidPriceB.Text.Trim()));
                //if (strIRR == "NaN")
                //{
                //    RadWindowManager1.RadAlert("Schedule has not been computed for this loan so IRR cannot be computed", 330, 180, "realedge associates", "alertCallBackFn");
                //}
                //else
                //{ txtBoxIRRB.Text = strIRR; }
                txtBoxAveLifNonDiscB.Text = AverageLifeNonDiscount(txtLoanNameB.Text);
                //txtBoxAveLifDiscB.Text = AverageLifeDisc(txtLoanNameB.Text);

                //txtBoxAveLifRiskyDiscB.Text = AverageLifeRiskDisc(txtLoanNameB.Text);
                //txtAvgLifeB.Text = AverageLife(txtLoanNameB.Text);
                Session.Add("LoanType", "B");
                Session.Add("LoanNameB", txtLoanNameB.Text);
            }
            catch (Exception ex)
            {
                Session.Add("LoanType", "B");
                Session.Add("LoanNameB", txtLoanNameB.Text);
                RadWindowManager1.RadAlert(ex.Message, 330, 180, "realedge associates", "alertCallBackFn");
            }
        }

        protected void txtLoanNameC_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            try
            {
                Session.Add("Trade3", txtBoxTradeDate3.SelectedDate);
                SetLoanC();
                //string strIRR = Convert.ToString(CalculateIRR(txtLoanNameC.Text.Trim(), txtBoxBidPriceC.Text.Trim()));
                //if (strIRR == "NaN")
                //{
                //    RadWindowManager1.RadAlert("Schedule has not been computed for this loan so IRR cannot be computed", 330, 180, "realedge associates", "alertCallBackFn");
                //}
                //else
                //{ txtBoxIRRC.Text = strIRR; }
                txtBoxAveLifNonDiscC.Text = AverageLifeNonDiscount(txtLoanNameC.Text);
                //txtBoxAveLifDiscC.Text = AverageLifeDisc(txtLoanNameC.Text);

                //txtBoxAveLifRiskyDiscC.Text = AverageLifeRiskDisc(txtLoanNameC.Text);
                //txtAvgLifeC.Text = AverageLife(txtLoanNameC.Text);
                Session.Add("LoanType", "C");
                Session.Add("LoanNameC", txtLoanNameC.Text);
            }
            catch (Exception ex)
            {
                Session.Add("LoanType", "C");
                Session.Add("LoanNameC", txtLoanNameC.Text);
                RadWindowManager1.RadAlert(ex.Message, 330, 180, "realedge associates", "alertCallBackFn");
            }
        }

        protected void grdCalculatedDates1_SortCommand(object sender, GridSortCommandEventArgs e)
        {

        }

        protected void grdCalculatedDates1_ItemCommand(object sender, GridCommandEventArgs e)
        {

        }

        protected void grdCalculatedDates2_SortCommand(object sender, GridSortCommandEventArgs e)
        {

        }

        protected void grdCalculatedDates2_ItemCommand(object sender, GridCommandEventArgs e)
        {

        }

        protected void grdCalculatedDates3_SortCommand(object sender, GridSortCommandEventArgs e)
        {

        }

        protected void grdCalculatedDates3_ItemCommand(object sender, GridCommandEventArgs e)
        {

        }

        protected void grdCalculatedDates3_ItemDataBound(object sender, GridItemEventArgs e)
        {
            //if (e.Item.ItemType == GridItemType.AlternatingItem || e.Item.ItemType == GridItemType.Item)
            //{
            //    System.Web.UI.WebControls.Label lblDay = e.Item.FindControl("lblDay") as System.Web.UI.WebControls.Label;
            //    CalculatedDates model = e.Item.DataItem as CalculatedDates;
            //    String TheDayOfWeek = model.CalculatedDate.DayOfWeek.ToString();
            //    lblDay.Text = TheDayOfWeek.Substring(0, 3);
            //}
        }

        protected void grdCalculatedDates2_ItemDataBound(object sender, GridItemEventArgs e)
        {
            //if (e.Item.ItemType == GridItemType.AlternatingItem || e.Item.ItemType == GridItemType.Item)
            //{
            //    System.Web.UI.WebControls.Label lblDay = e.Item.FindControl("lblDay") as System.Web.UI.WebControls.Label;
            //    CalculatedDates model = e.Item.DataItem as CalculatedDates;
            //    String TheDayOfWeek = model.CalculatedDate.DayOfWeek.ToString();
            //    lblDay.Text = TheDayOfWeek.Substring(0, 3);
            //}
        }

        protected void grdCalculatedDates1_ItemDataBound(object sender, GridItemEventArgs e)
        {
            //if (e.Item.ItemType == GridItemType.AlternatingItem || e.Item.ItemType == GridItemType.Item)
            //{
            //    System.Web.UI.WebControls.Label lblDay = e.Item.FindControl("lblDay") as System.Web.UI.WebControls.Label;
            //    CalculatedDates model = e.Item.DataItem as CalculatedDates;
            //    String TheDayOfWeek = model.CalculatedDate.DayOfWeek.ToString();
            //    lblDay.Text = TheDayOfWeek.Substring(0, 3);
            //}
        }

        protected void btnComputeCashFlowA_Click(object sender, EventArgs e)
        {
            ComputeCouponDatesAndFractions(txtBoxTradeDate1, txtLoanNameA, txtBoxSettlementDate1, txtBoxMaturityDateA, txtBoxCouponFrequencyLoanA, txtAveLifNonDiscA, grdCalculatedDates1, 1, txtBoxNotional1, txtBoxInterestRateA, txtBoxCurrencyLoanA);

            LogActivity("Compute the CashFlow A", "Compute the cashflow for compact view A", string.Empty);
        }

        protected void btnComputeCashFlowB_Click(object sender, EventArgs e)
        {
            ComputeCouponDatesAndFractions(txtBoxTradeDate2, txtLoanNameB, txtBoxSettlementDate2, txtBoxMaturityDateB, txtBoxCouponFrequencyLoanB, txtBoxAveLifNonDiscB, grdCalculatedDates2, 2, txtBoxNotional2, txtInterestRateB, txtCurrencyB);

            LogActivity("Compute the CashFlow B", "Compute the cashflow for compact view B", string.Empty);
        }

        protected void btnComputeCashFlowC_Click(object sender, EventArgs e)
        {
            ComputeCouponDatesAndFractions(txtBoxTradeDate3, txtLoanNameC, txtBoxSettlementDate3, txtMaturityDateC, txtBoxCouponFrequencyLoanC, txtBoxAveLifNonDiscC, grdCalculatedDates3, 3, txtBoxNotional3, txtInterestRateC, txtCurrencyC);

            LogActivity("Compute the CashFlow C", "Compute the cashflow for compact view C", string.Empty);
        }

        protected void btnClearC_Click1(object sender, EventArgs e)
        {

        }

        protected void grdQuotesAndTrades_ItemCommand(object sender, GridCommandEventArgs e)
        {
            //if (e.CommandName == "Edit")
            //{
            //    int id = Convert.ToInt32(e.CommandArgument);
            //    RadTab rootTab = RadTabStrip1.FindTabByText("Edit Historical Quotes & Trades");
            //    rootTab.Visible = true;

            //    Session.Add("EditQuoteID", id);
            //    Response.Redirect("default.aspx?page=edithistoricalquotes");
            //} else 
            if (e.CommandName == "Delete")
            {

                int id = Convert.ToInt32(e.CommandArgument);
                QuotesAndTradesBLL quotesBLL = new QuotesAndTradesBLL();
                quotesBLL.RemoveQuote(id);
            }
            BindHistoricalQuotesAndTradesTab();
        }

        protected void grdLoans_ItemCommand(object sender, GridCommandEventArgs e)
        {
            if (e.CommandName == "Edit")
            {
                int id = Convert.ToInt32(e.CommandArgument);
                Session.Add("EditLoanID", id);
                Response.Redirect("default.aspx?page=addstaticloan");
            }
            else if (e.CommandName == "Delete")
            {
                int id = Convert.ToInt32(e.CommandArgument);
                LoansBLL loansBll = new LoansBLL();
                string loanName = Convert.ToString(loansBll.GetLoanByID(id).CodeName);
                loansBll.RemoveLoan(id);

                DAL.Login login = Session["LogedInUser"] as DAL.Login;
                LoanHistory history = new LoanHistory();
                LoanHistoryBL historyBL = new LoanHistoryBL();
                history.Action = "Delete";
                history.LastModified = DateTime.Now;
                history.LoanName = loanName;
                history.UserName = login.Name;
                historyBL.SaveHistory(history);

                RadWindowManager1.RadAlert("Loan removed successfully", 330, 180, "realedge associates", "alertCallBackFn");
            }
            BindLoansData();
        }

        protected void timer1_Tick(object sender, EventArgs e)
        {
            BindLoansTab();

            return;
        }

        protected void txtBoxTradeDate1_SelectedDateChanged(object sender, Telerik.Web.UI.Calendar.SelectedDateChangedEventArgs e)
        {
            if (txtBoxTradeDate1.SelectedDate != null)
            {
                txtBoxSettlementDate1.SelectedDate = AddBusinessDays(txtBoxTradeDate1.SelectedDate.Value, 10);
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

        protected void txtBoxTradeDate2_SelectedDateChanged(object sender, Telerik.Web.UI.Calendar.SelectedDateChangedEventArgs e)
        {
            txtBoxSettlementDate2.SelectedDate = AddBusinessDays(txtBoxTradeDate2.SelectedDate.Value, 10);
        }

        protected void txtBoxTradeDate3_SelectedDateChanged(object sender, Telerik.Web.UI.Calendar.SelectedDateChangedEventArgs e)
        {
            txtBoxSettlementDate3.SelectedDate = AddBusinessDays(txtBoxTradeDate3.SelectedDate.Value, 10);
        }

        protected void ddlAddLoanCode_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            BindLoanCodeDetails();
        }

        //private void BindLoanCodeDetails()
        //{
        //    try
        //    {
        //        LoansBLL bll = new LoansBLL();
        //        string strLoanName = ddlAddLoanCode.Text.ToString();
        //        if (strLoanName.Contains(';'))
        //        {
        //            strLoanName = strLoanName.Replace(';', ' ');
        //        }
        //        Loans loan = bll.GetLoanByCode(strLoanName.Trim());
        //        if (loan != null)
        //        {
        //            txtBoxAddBorrower.Text = Convert.ToString(loan.Borrower);
        //            txtBoxAddSector.Text = Convert.ToString(loan.Sector);
        //            if (loan.Signing_Date != "")
        //                txtBoxAddSigningDate.SelectedDate = Convert.ToDateTime(loan.Signing_Date);
        //            else
        //                txtBoxAddSigningDate.Clear();
        //            if (loan.Maturity_Date != "")
        //                txtBoxAddMaturityDate.SelectedDate = Convert.ToDateTime(loan.Maturity_Date);
        //            else
        //                txtBoxAddMaturityDate.Clear();
        //            txtBoxFacilitySize.Text = loan.FacilitySize;
        //            ddlAddFixedOrFloating.SelectedValue = loan.FixedOrFloating;
        //            txtBoxAddMargin.Text = loan.Margin;
        //            ddlAddCurrency.SelectedValue = loan.Currency;
        //            ddlCountry.SelectedValue = loan.Country;
        //            ddlAddCouponFrequency.SelectedValue = loan.CouponFrequency;
        //            if (loan.Bilateral == true)
        //                ddlAddBilateral.SelectedValue = "Yes";
        //            else
        //                ddlAddBilateral.SelectedValue = "No";
        //            ddlAmortizing.SelectedValue = loan.Amortizing;
        //            lblConfirm.Text = "Loan already exists, do you wish to overwrite?";
        //            _operation = "Edit";
        //        }
        //        else
        //        {
        //            _operation = "Add";                
        //        }
        //        //else
        //        //{
        //        //    Clear();
        //        //}
        //    }
        //    catch (Exception)
        //    {


        //    }
        //}

        private void BindLoanCodeDetails()
        {
            try
            {
                LoansBLL bll = new LoansBLL();
                string strLoanName = ddlAddLoanCode.Text.ToString();
                if (strLoanName.Contains(';'))
                {
                    strLoanName = strLoanName.Replace(';', ' ');
                }
                Loans loan = bll.GetLoanByCode(strLoanName.Trim());
                if (loan != null)
                {
                    if (loan.Borrower != null)
                    {
                        ddlBorrower.SelectedValue = Convert.ToString(loan.Borrower);
                        BorrowersBL borrowerBL = new BorrowersBL();
                        if (ddlBorrower.SelectedValue != string.Empty)
                        {
                            if (ddlBorrower.SelectedItem != null)
                            {
                                Borrower borrower = borrowerBL.GetBorrower(ddlBorrower.SelectedItem.Text);
                                txtGrid.Text = loan.Grid;
                                txtSummitCredit.Text = loan.SummitCreditEntity;
                            }
                        }

                    }

                    //  txtBoxAddSector.Text = Convert.ToString(loan.Sector);
                    if (loan.Sector != null)
                    {
                        ddlSector.SelectedValue = Convert.ToString(loan.Sector);
                    }

                    if (loan.Signing_Date != "" && loan.Signing_Date != null)
                        txtBoxAddSigningDate.SelectedDate = Convert.ToDateTime(loan.Signing_Date);
                    else
                        txtBoxAddSigningDate.Clear();
                    if (loan.Maturity_Date != "" && loan.Maturity_Date != null)
                        txtBoxAddMaturityDate.SelectedDate = Convert.ToDateTime(loan.Maturity_Date);
                    else
                        txtBoxAddMaturityDate.Clear();
                    if (loan.CouponDate != "" && loan.CouponDate != null)
                        txtCouponDate.SelectedDate = Convert.ToDateTime(loan.CouponDate);
                    else
                        txtCouponDate.Clear();
                    if (loan.FacilitySize != null)
                    {
                        txtBoxFacilitySize.Text = Convert.ToDecimal(loan.FacilitySize).ToString("N");

                    }
                    if (loan.Fixed_Floating != null)
                    {
                        ddlAddFixedOrFloating.SelectedValue = loan.FixedOrFloating;
                    }

                    txtBoxAddMargin.Text = loan.Margin;
                    if (loan.Currency != null)
                    {
                        ddlAddCurrency.SelectedValue = loan.Currency;
                    }
                    if (loan.Country != null)
                    {
                        ddlCountry.SelectedValue = loan.Country;
                    }

                    txtPP.Text = loan.PP;
                    //  txtCRating.Text = loan.CreditRating;
                    txtStructureID.Text = loan.StructureID;
                    if (loan.CouponFrequency != null)
                    {
                        ddlAddCouponFrequency.SelectedValue = loan.CouponFrequency;
                    }

                    txtNotional.Text = Convert.ToDecimal(loan.Notional).ToString("N");
                    if (loan.Bilateral == true)
                        ddlAddBilateral.SelectedValue = "Yes";
                    else
                        ddlAddBilateral.SelectedValue = "No";
                    if (loan.Amortizing == "Yes")
                    {
                        ddlAmortizing.SelectedValue = loan.Amortizing;
                        if (ddlAmortizing.SelectedValue == "Yes")
                        {
                            if (loan.AmortisationsStartPoint != null)
                                txtAmortisationsStartDate.SelectedDate = Convert.ToDateTime(loan.AmortisationsStartPoint);
                            txtAmortisations.Text = loan.NoOfAmortisationPoint.ToString();
                            trDate.Visible = true;
                            trNo.Visible = true;
                            tblAmortisation.Visible = true;
                        }
                    }
                    else
                    {
                        ddlAmortizing.SelectedValue = "No";
                        trDate.Visible = false;
                        trNo.Visible = false;
                        tblAmortisation.Visible = false;
                    }
                    txtGurantor.Text = loan.Gurantor;
                    txtGrid.Text = loan.Grid;
                    txtSummitCredit.Text = loan.SummitCreditEntity;

                    CheckNodes(tvCreditRating, loan);

                    txtBoxAddMargin.Text = loan.Margin;

                    LoanScheduleBL scheduleBL = new LoanScheduleBL();
                    List<LoanSchedule> scheduleList = scheduleBL.GetLoanByID(loan.ID);

                    if (scheduleList != null && scheduleList.Count > 0)
                    {
                        dt1.Clear();
                        //dt1.Columns.Add("ID", typeof(int));
                        //dt1.Columns.Add("StartDate", typeof(DateTime));
                        //dt1.Columns.Add("EndDate", typeof(DateTime));
                        //dt1.Columns.Add("Notional", typeof(long));
                        //dt1.Columns.Add("Factor", typeof(float));

                        for (int i = 0; i < scheduleList.Count; i++)
                        {
                            // dt1.Rows.Add(scheduleList[i].ID, scheduleList[i].StartDate, scheduleList[i].EndDate, scheduleList[i].Notation, scheduleList[i].Factor);
                            dt1.Rows.Add(i + 1, scheduleList[i].StartDate.Value.ToShortDateString(), scheduleList[i].EndDate.Value.ToShortDateString(), scheduleList[i].CoupFrac, scheduleList[i].Notation, scheduleList[i].Amortisation, scheduleList[i].Factor, scheduleList[i].Spread, scheduleList[i].CouponPaymentDate, scheduleList[i].RiskFreeDP1, scheduleList[i].RiskFreeDP2, scheduleList[i].FloatingRate, scheduleList[i].AllInRate, scheduleList[i].Interest, scheduleList[i].Days, scheduleList[i].AmortisationInt);
                        }
                        pnlAmortizing.Visible = true;
                        grdAmortizing.Visible = true;
                        btnCalculatSchedule.Visible = true;
                        Session.Add("LoanScheduleData", dt1);
                        grdAmortizing.DataSource = dt1;
                        grdAmortizing.DataBind();
                    }
                    else
                    {
                        Session.Remove("LoanScheduleData");
                        dt1.Clear();
                        pnlAmortizing.Visible = false;
                        btnCalculatSchedule.Visible = false;
                        grdAmortizing.Visible = false;
                    }
                    hfLoanID.Value = loan.ID.ToString();
                    Session.Add("EditLoanID", loan.ID.ToString());
                    lblConfirm.Text = "Loan already exists, do you wish to overwrite?";
                    _operation = "Edit";
                }
                else
                {
                    hfLoanID.Value = string.Empty;
                    _operation = "Add";
                    lblConfirm.Text = "Are you sure you want to add this loan to database?";
                    // Clear();
                }
            }
            catch (Exception)
            {


            }
        }

        protected void RadAjaxManager2_AjaxRequest(object sender, AjaxRequestEventArgs e)
        {
            if (e.Argument == "Insert")
            {

            }
        }

        protected void btnQuotesSaveSettings_Click(object sender, EventArgs e)
        {


            string user = "QuotesAndTrades";
            // InfoImage.Visible = true;
            SessionStorageProvider.StorageProviderKey = user;
            RadPersistenceManager1.SaveState();
            //  StatusLabel.Text = "Settings for " + user + " saved successfully!";
        }

        protected void BindQuotesAndGridsFilterSettings()
        {
            string user = "QuotesAndTrades";
            if (Session[user] == null)
            {
                //     StatusLabel.Text = "No saved settings for this user were found!";
            }
            else
            {
                SessionStorageProvider.StorageProviderKey = user;
                RadPersistenceManager1.LoadState();
                BindHistoricalQuotesAndTradesTab();
                grdQuotesAndTrades.Rebind();
                //StatusLabel.Text = "Settings for " + user + " were restored successfully!";
            }
        }

        protected void btnQuoteClearSettings_Click(object sender, EventArgs e)
        {
            string user = "QuotesAndTrades";
            Session[user] = null;

        }

        protected void btnClearLoan_Click(object sender, EventArgs e)
        {
            Clear();
        }

        protected void btnSaveQuote_Click(object sender, EventArgs e)
        {
            UpdateQuotes();
        }

        protected void btnClearQuote_Click(object sender, EventArgs e)
        {
            ClearQuotes();
        }

        #region Quote Related Edit Fill and Clear

        private void FillQuoteData()
        {
            if (Session["EditQuoteID"] != null)
            {
                QuotesAndTradesBLL quotesAndTradeBLL = new QuotesAndTradesBLL();
                QuotesAndTrades quotes = new QuotesAndTrades();
                quotes = quotesAndTradeBLL.GetQuoteAndTrade(Convert.ToInt32(Session["EditQuoteID"]));
                ddlQuotesLoanName.SelectedValue = quotes.LoanName;
                txtQuotesCounterParty.Text = quotes.CounterParty.ToString();
                if (quotes.BidPrice != null)
                {
                    txtQuoteBidPrice.Text = quotes.BidPrice.Value.ToString();
                }
                if (quotes.OfferPrice != null)
                {
                    txtQuoteOfferPrice.Text = quotes.OfferPrice.Value.ToString();
                }
                if (quotes.BidSpread != null)
                {
                    txtQuoteBidSpreadPrice.Text = quotes.BidSpread.Value.ToString();
                }
                if (quotes.OfferSpread != null)
                {
                    txtQuoteOfferSpread.Text = quotes.OfferSpread.Value.ToString();
                }
                if (quotes.Traded != null)
                {
                    chkIsTraded.Checked = quotes.Traded.Value;

                }
                if (quotes.MarketValue != null)
                {
                    txtQuoteMarketValue.Text = quotes.MarketValue.Value.ToString();
                }
                bindCountry();
                if (quotes.Country != null)
                {
                    // ddlQuoteCountry.SelectedItem.Text = quotes.Country;
                    ddlQuoteCountry.SelectedValue = quotes.Country;
                }

            }
        }

        private void ClearQuotes()
        {
            ddlQuotesLoanName.ClearSelection();
            txtQuotesCounterParty.Text = string.Empty;
            txtQuoteBidPrice.Text = string.Empty;
            txtQuoteOfferPrice.Text = string.Empty;
            txtQuoteBidSpreadPrice.Text = string.Empty;
            txtQuoteOfferSpread.Text = string.Empty;
            chkIsTraded.Checked = false;
            txtQuoteMarketValue.Text = string.Empty;
            ddlQuoteCountry.ClearSelection();
        }

        private void UpdateQuotes()
        {
            try
            {
                QuotesAndTradesBLL quotesAndTradeBLL = new QuotesAndTradesBLL();
                QuotesAndTrades quotesAndTrades = new QuotesAndTrades();
                quotesAndTrades.ID = Convert.ToInt32(Session["EditQuoteID"]);
                quotesAndTrades.LoanName = ddlQuotesLoanName.SelectedValue;
                quotesAndTrades.CounterParty = txtQuotesCounterParty.Text;
                if (txtQuoteBidPrice.Text != "")
                {
                    quotesAndTrades.BidPrice = Convert.ToDecimal(txtQuoteBidPrice.Text);
                }
                if (txtQuoteBidSpreadPrice.Text != "")
                {
                    quotesAndTrades.BidSpread = Convert.ToDecimal(txtQuoteBidSpreadPrice.Text);
                }
                if (txtQuoteOfferPrice.Text != "")
                {
                    quotesAndTrades.OfferPrice = Convert.ToDecimal(txtQuoteOfferPrice.Text);
                }
                if (txtQuoteOfferSpread.Text != "")
                {
                    quotesAndTrades.OfferSpread = Convert.ToDecimal(txtQuoteOfferSpread.Text);
                }
                quotesAndTrades.Traded = chkIsTraded.Checked;
                if (txtQuoteMarketValue.Text != "")
                {
                    quotesAndTrades.MarketValue = Convert.ToDecimal(txtQuoteMarketValue.Text);
                }
                quotesAndTrades.Country = ddlQuoteCountry.Text;
                quotesAndTrades.TimeStamp = DateTime.UtcNow;

                //quotesAndTradeBLL.SaveQuote(quotesAndTrades);
                quotesAndTradeBLL.UpdateQuote(quotesAndTrades);
                lblQuotesTradesMessage.Visible = true;
                Session.Remove("EditQuoteID");
                lblQuotesTradesMessage.Text = "Quotes and Trades Updated Successfully";
                LogActivity("Quotes and Trades Updated Successfully", "Edit Quotes and Trades", string.Empty);
            }
            catch (Exception)
            {


            }
        }
        #endregion

        protected void ddlAmortizing_SelectedIndexChanged(object sender, DropDownListEventArgs e)
        {
            //     timer1.Enabled = false;
            if (ddlAmortizing.SelectedValue == "Yes")
            {
                trNo.Visible = true;
                trDate.Visible = true;
                tblAmortisation.Visible = true;
            }
            else
            {
                txtAmortisationsStartDate.Clear();
                txtAmortisations.Text = string.Empty;
                dt1.Clear();
                Session.Remove("LoanScheduleData");
                pnlAmortizing.Visible = false;
                grdAmortizing.Visible = false;
                btnCalculatSchedule.Visible = false;
                trNo.Visible = false;
                trDate.Visible = false;
                tblAmortisation.Visible = false;
            }
            //  timer1.Enabled = true;
        }

        protected void txtAmortisations_TextChanged(object sender, EventArgs e)
        {

        }


        //private void GenerateTable(int colsCount, int rowsCount)
        //{
        //    try
        //    {
        //        //  timer1.Enabled = false;
        //        dt1.Clear();
        //        Session.Remove("LoanScheduleData");
        //        //Creat the Table and Add it to the Page
        //        System.Text.StringBuilder sb = new System.Text.StringBuilder();

        //        //Page.Form.Controls.Add(table);
        //        //DateTime startDate = txtCouponDate.SelectedDate.Value;        commented on 22/02
        //        DateTime startDate = txtAmortisationsStartDate.SelectedDate.Value;

        //        DateTime previousStartDate = startDate;
        //        string frequency = ddlAddCouponFrequency.SelectedValue.ToString();
        //        Decimal firstNationalRow = Convert.ToDecimal(txtNotional.Text);
        //        Decimal nationalRow = Convert.ToDecimal(txtNotional.Text);


        //        // Now iterate through the table and add your controls 
        //        for (int i = 0; i < rowsCount; i++)
        //        {

        //            DataRow dr = dt1.NewRow();
        //            dr["ID"] = (i + 1).ToString();
        //            //dr["ID"] = (i).ToString();

        //            for (int j = 0; j < colsCount; j++)
        //            {
        //                TableCell cell = new TableCell();
        //                cell.CssClass = "tdBorder";
        //                RadTextBox tb = new RadTextBox();
        //                tb.Skin = "Web20";

        //                // Set a unique ID for each TextBox added
        //                tb.ID = "TextBoxRow_" + i + "Col_" + j;
        //                if (i == 0 && j == 0)
        //                {
        //                    tb.Text = startDate.ToShortDateString();

        //                    dr["StartDate"] = startDate.ToShortDateString();
        //                }
        //                else if (i > 0 && j == 0)
        //                {
        //                    switch (frequency.Trim())
        //                    {
        //                        case "Monthly":
        //                            previousStartDate = startDate;
        //                            startDate = startDate.AddMonths(1).Date;

        //                            tb.Text = startDate.ToShortDateString();
        //                            dr["StartDate"] = startDate.ToShortDateString();
        //                            break;
        //                        case "Quarterly":
        //                            previousStartDate = startDate;
        //                            startDate = startDate.AddMonths(3).Date;
        //                            tb.Text = startDate.ToShortDateString();
        //                            dr["StartDate"] = startDate.ToShortDateString();
        //                            break;
        //                        case "Semi-Annual":
        //                            previousStartDate = startDate;
        //                            startDate = startDate.AddMonths(6).Date;
        //                            tb.Text = startDate.ToShortDateString();
        //                            dr["StartDate"] = startDate.ToShortDateString();
        //                            break;
        //                        case "Annual":
        //                            previousStartDate = startDate;
        //                            startDate = startDate.AddYears(1).Date;
        //                            tb.Text = startDate.ToShortDateString();
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
        //                            tb.Text = endDate.ToShortDateString();
        //                            dr["EndDate"] = endDate.ToShortDateString();
        //                            break;
        //                        case "Quarterly":
        //                            endDate = startDate.AddMonths(3).Date;
        //                            tb.Text = endDate.ToShortDateString();
        //                            dr["EndDate"] = endDate.ToShortDateString();
        //                            break;
        //                        case "Semi-Annual":
        //                            endDate = startDate.AddMonths(6).Date;
        //                            tb.Text = endDate.ToShortDateString();
        //                            dr["EndDate"] = endDate.ToShortDateString();
        //                            break;
        //                        case "Annual":
        //                            endDate = startDate.AddYears(1).Date;
        //                            tb.Text = endDate.ToShortDateString();
        //                            dr["EndDate"] = endDate.ToShortDateString();
        //                            break;
        //                        default:
        //                            break;
        //                    }
        //                }
        //                else if (i > 0 && j == 1)
        //                {
        //                    DateTime endDate;
        //                    switch (frequency.Trim())
        //                    {
        //                        case "Monthly":
        //                            endDate = startDate.AddMonths(1).Date;
        //                            tb.Text = endDate.ToShortDateString();
        //                            dr["EndDate"] = endDate.ToShortDateString();
        //                            break;
        //                        case "Quarterly":
        //                            endDate = startDate.AddMonths(3).Date;
        //                            tb.Text = endDate.ToShortDateString();
        //                            dr["EndDate"] = endDate.ToShortDateString();
        //                            break;
        //                        case "Semi-Annual":
        //                            endDate = startDate.AddMonths(6).Date;
        //                            tb.Text = endDate.ToShortDateString();
        //                            dr["EndDate"] = endDate.ToShortDateString();
        //                            break;
        //                        case "Annual":
        //                            endDate = startDate.AddYears(1).Date;
        //                            tb.Text = endDate.ToShortDateString();
        //                            dr["EndDate"] = endDate.ToShortDateString();
        //                            break;
        //                        default:
        //                            break;
        //                    }
        //                }
        //                if (j == 2)
        //                {

        //                    DateTime couponDT = AddBusinessDays(DateTime.Now, 10);
        //                    if (txtCouponDate.SelectedDate.ToString() != "")
        //                    {
        //                        couponDT = Convert.ToDateTime(txtCouponDate.SelectedDate.Value);
        //                    }

        //                    DateTime endDt = Convert.ToDateTime(dr["EndDate"]);
        //                    decimal days = (endDt - couponDT).Days;
        //                    decimal totalDays = 365;
        //                    decimal coupFreqDays = days / totalDays;
        //                    if (days > 0)
        //                        dr["CoupFrac"] = Convert.ToDecimal(coupFreqDays).ToString("0.00");
        //                    else
        //                        dr["CoupFrac"] = Convert.ToDecimal(0).ToString("0.00");
        //                }
        //                if (j == 3)
        //                {
        //                    tb.Text = txtNotional.Text;
        //                    if (i == 0)
        //                    {
        //                        firstNationalRow = Convert.ToDecimal(txtNotional.Text);
        //                        nationalRow = firstNationalRow;
        //                    }
        //                    else
        //                    {
        //                        //  Convert.ToDecimal(Convert.ToDecimal(dt1.Rows[i - 1]["Notation"]) - Convert.ToDecimal(dt1.Rows[i - 1]["Amortisation"])).ToString("N");
        //                        nationalRow = Convert.ToDecimal(Convert.ToDecimal(dt1.Rows[i - 1]["Notation"]) - Convert.ToDecimal(dt1.Rows[i - 1]["Amortisation"]));
        //                    }
        //                    dr["Notation"] = nationalRow.ToString("N");
        //                }
        //                if (j == 4)
        //                {
        //                    //if (i < 1)
        //                    //    dr["Amortisation"] = 0.00;
        //                    //else
        //                    DateTime dt = Convert.ToDateTime(dr["StartDate"]);
        //                    DateTime couponDT = AddBusinessDays(DateTime.Now, 10);
        //                    if (txtCouponDate.SelectedDate.ToString() != "")
        //                    {
        //                        couponDT = Convert.ToDateTime(txtCouponDate.SelectedDate.Value);
        //                    }
        //                    if (txtCouponDate.SelectedDate != null && dt >= couponDT) // change on 09-04
        //                    {
        //                        //  int activeCoupons = Convert.ToInt16(txtActiveCoupon.Text);             //where to take this value
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
        //                    tb.Text = factorRow.ToString();
        //                    dr["Factor"] = factorRow;
        //                }

        //                if (j == 6)
        //                {
        //                    dr["Spread"] = Convert.ToDecimal(txtBoxAddMargin.Text);
        //                }
        //                if (j == 7)
        //                {
        //                    dr["CouponPaymentDate"] = Convert.ToDateTime(dr["EndDate"]);
        //                }
        //                if (j == 8)
        //                {
        //                    if (ddlAddCurrency.SelectedValue.ToLower() == "eur")
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
        //                        dr["RiskFreeDP1"] = Math.Exp(-((y / 100) * (((Convert.ToDateTime(dr["EndDate"]) - Convert.ToDateTime(txtBoxAddSigningDate.SelectedDate)).Days)) / 365));
        //                    }
        //                    else if (ddlAddCurrency.SelectedValue.ToLower() == "usd")
        //                    {
        //                        USDCurveBL usdCurveBL = new USDCurveBL();
        //                        List<USDCurve> usdCurve = usdCurveBL.GetUSCurve();
        //                        Dictionary<double, double> rateVals = new Dictionary<double, double>();
        //                        foreach (var item in usdCurve)
        //                        {
        //                            rateVals.Add(Convert.ToDouble(item.RateDate.Value.Ticks), Convert.ToDouble(item.Rate));
        //                        }
        //                        var scaler = new SplineInterpolator(rateVals);
        //                        var y = scaler.GetValue(Convert.ToDateTime(dr["EndDate"]).Ticks);
        //                        dr["RiskFreeDP1"] = Math.Exp(-((y / 100) * (((Convert.ToDateTime(dr["EndDate"]) - Convert.ToDateTime(txtBoxAddSigningDate.SelectedDate)).Days)) / 365));
        //                    }
        //                    //  SplineInterpolator interPolation = new  SplineInterpolator();
        //                    // dr["RiskFreeDP1"]=  ;//EXP(-(interpolate(schedule.endDate(i),EurDB.Rates,'EurDB.Rates')/100*(schedule.endDate(i)-loan.settlementDate)/365));
        //                }
        //                if (j == 9)
        //                {
        //                    if (dr["EndDate"].ToString() != string.Empty)
        //                    {
        //                        dr["RiskFreeDP2"] = Convert.ToDouble(dr["RiskFreeDP1"]) / Convert.ToDouble((Math.Pow(Convert.ToDouble((1 + (Convert.ToDouble(txtBoxAddMargin.Text) / 20000))), ((2 * (Convert.ToDateTime(dr["EndDate"]) - Convert.ToDateTime(txtBoxAddSigningDate.SelectedDate)).Days) / 360))));
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
        //                        double d = Convert.ToDouble(dt1.Rows[i - 1]["RiskFreeDP1"]) / Convert.ToDouble(dr["RiskFreeDP1"]) - 1 / ((Convert.ToDateTime(dr["EndDate"]) - Convert.ToDateTime(dt1.Rows[i - 1]["EndDate"])).Days) / 360;
        //                        dr["FloatingRate"] = Convert.ToDouble(d) / 100;
        //                    }
        //                }
        //                if (j == 11)
        //                {
        //                    dr["AllInRate"] = Convert.ToDouble(Convert.ToDouble(dr["FloatingRate"]) + Convert.ToDouble(txtBoxAddMargin.Text)) / 100;
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
        //                // Add the control to the TableCell
        //                cell.Controls.Add(tb);

        //                // Add the TableCell to the TableRow

        //            }
        //            dt1.Rows.Add(dr);
        //            // Add the TableRow to the Table
        //            //  table1.Rows.Add(row);
        //        }
        //        //table1.Visible = true;
        //        Session.Add("LoanScheduleData", dt1);
        //        pnlAmortizing.Visible = true;
        //        grdAmortizing.Visible = true;
        //        btnCalculatSchedule.Visible = true;
        //        grdAmortizing.DataSource = dt1;
        //        grdAmortizing.DataBind();
        //        //  timer1.Enabled = true;
        //    }
        //    catch (Exception)
        //    {
        //    }
        //}


        private void GenerateTable(int colsCount, int rowsCount)
        {
            try
            {
                //  timer1.Enabled = false;
                dt1.Clear();
                Session.Remove("LoanScheduleData");
                //Creat the Table and Add it to the Page
                System.Text.StringBuilder sb = new System.Text.StringBuilder();

                //Page.Form.Controls.Add(table);
                //DateTime startDate = txtCouponDate.SelectedDate.Value;        commented on 22/02
                DateTime startDate = txtAmortisationsStartDate.SelectedDate.Value;

                DateTime previousStartDate = startDate;
                string frequency = ddlAddCouponFrequency.SelectedValue.ToString();
                Decimal firstNotionalRow = Convert.ToDecimal(txtNotional.Text);
                Decimal notionalRow = Convert.ToDecimal(txtNotional.Text);


                // Now iterate through the table and add your controls 
                //for (int i = 0; i < rowsCount; i++)
                //{

                //    DataRow dr = dt1.NewRow();
                //    dr["ID"] = (i + 1).ToString();
                //    //dr["ID"] = (i).ToString();


                //    dt1.Rows.Add(dr);
                //    // Add the TableRow to the Table
                //    //  table1.Rows.Add(row);
                //}
                DateTime settlementDate;
                int i = 0;
                DateTime endDate;
                
                settlementDate = AddBusinessDays(DateTime.Now, 10); //changed 26-04 asked by avr
                endDate = settlementDate;
                DateTime maturityDate = txtBoxAddMaturityDate.SelectedDate.Value;
                Decimal notional = Convert.ToDecimal(txtNotional.Text);
                DateTime couponDate = txtCouponDate.SelectedDate.Value;
                string ccy = ddlAddCurrency.SelectedValue.ToString();
                DateTime tradedDate = txtBoxAddSigningDate.SelectedDate.Value;
                decimal spread = Convert.ToDecimal(txtBoxAddMargin.Text);
                int totalNoOfRows = 0;
                while (endDate < txtBoxAddMaturityDate.SelectedDate)
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
                endDate = AddBusinessDays(txtBoxAddSigningDate.SelectedDate.Value, 10);
                while (endDate < txtBoxAddMaturityDate.SelectedDate)
                {

                    DataRow dr = dt1.NewRow();
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
                                notionalRow = Convert.ToDecimal(Convert.ToDecimal(dt1.Rows[i - 1]["Notation"]) - Convert.ToDecimal(dt1.Rows[i - 1]["Amortisation"]));
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
                            dr["Amortisation"] = Convert.ToDecimal(Convert.ToDecimal(txtNotional.Text) / totalNoOfRows).ToString("0.00");
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
                                double d = Convert.ToDouble(dt1.Rows[i - 1]["RiskFreeDP1"]) / Convert.ToDouble(dr["RiskFreeDP1"]) - 1 / ((Convert.ToDateTime(dr["EndDate"]) - Convert.ToDateTime(dt1.Rows[i - 1]["EndDate"])).Days) / 360;
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
                        dt1.Rows.Add(dr);
                        i++;
                    }
                }


                //for (int x = 0; x < dt1.Rows.Count; x++)
                //{
                //    dt1.Rows[x][5] = Convert.ToDecimal(Convert.ToDecimal(txtNotional.Text) / (dt1.Rows.Count)).ToString("0.00");
                //    if (x == 0)
                //    {
                //        firstNotionalRow = Convert.ToDecimal(notional);
                //        notionalRow = firstNotionalRow;
                //    }
                //    else
                //    {
                //        notionalRow = Math.Round(Convert.ToDecimal(Math.Round(Convert.ToDecimal(dt1.Rows[x - 1]["Notation"]), 2) - Convert.ToDecimal(dt1.Rows[x - 1]["Amortisation"])), 2);
                //    }
                //    dt1.Rows[x]["Notation"] = notionalRow.ToString("N");

                //}


                //table1.Visible = true;
                Session.Add("LoanScheduleData", dt1);
                pnlAmortizing.Visible = true;
                grdAmortizing.Visible = true;
                btnCalculatSchedule.Visible = true;
                grdAmortizing.DataSource = dt1;
                grdAmortizing.DataBind();
                //  timer1.Enabled = true;
            }
            catch (Exception)
            {
            }
        }


        private void GenerateTable(int colsCount, int rowsCount, DateTime startDate)
        {
            dt1.Clear();
            //Creat the Table and Add it to the Page

            //Page.Form.Controls.Add(table);
            //DateTime startDate = txtCouponDate.SelectedDate.Value;
            DateTime previousStartDate = startDate;
            string frequency = ddlAddCouponFrequency.SelectedValue.ToString();
            Decimal firstNationalRow = Convert.ToDecimal(txtNotional.Text);
            Decimal nationalRow = Convert.ToDecimal(txtNotional.Text);


            // Now iterate through the table and add your controls 
            for (int i = 0; i < rowsCount; i++)
            {

                DataRow dr = dt1.NewRow();
                dr["ID"] = i.ToString();
                for (int j = 0; j < colsCount; j++)
                {
                    TableCell cell = new TableCell();
                    cell.CssClass = "tdBorder";
                    RadTextBox tb = new RadTextBox();
                    tb.Skin = "Web20";



                    // Set a unique ID for each TextBox added
                    tb.ID = "TextBoxRow_" + i + "Col_" + j;
                    if (i == 0 && j == 0)
                    {
                        tb.Text = startDate.ToShortDateString();
                        //tb.AutoPostBack = true;
                        //tb.TextChanged += new EventHandler(this.tb_TextChanged);
                        dr["StartDate"] = startDate.ToShortDateString();
                    }
                    else if (i > 0 && j == 0)
                    {
                        switch (frequency.Trim())
                        {
                            case "Monthly":
                                previousStartDate = startDate;
                                startDate = startDate.AddMonths(1).Date;

                                tb.Text = startDate.ToShortDateString();
                                dr["StartDate"] = startDate.ToShortDateString();
                                break;
                            case "Quarterly":
                                previousStartDate = startDate;
                                startDate = startDate.AddMonths(3).Date;
                                tb.Text = startDate.ToShortDateString();
                                dr["StartDate"] = startDate.ToShortDateString();
                                break;
                            case "Semi-Annual":
                                previousStartDate = startDate;
                                startDate = startDate.AddMonths(6).Date;
                                tb.Text = startDate.ToShortDateString();
                                dr["StartDate"] = startDate.ToShortDateString();
                                break;
                            case "Annual":
                                previousStartDate = startDate;
                                startDate = startDate.AddYears(1).Date;
                                tb.Text = startDate.ToShortDateString();
                                dr["StartDate"] = startDate.ToShortDateString();
                                break;
                            default:
                                break;
                        }

                    }
                    if (i == 0 && j == 1)
                    {
                        DateTime endDate;
                        switch (frequency.Trim())
                        {
                            case "Monthly":
                                endDate = startDate.AddMonths(1).Date;
                                tb.Text = endDate.ToShortDateString();
                                dr["EndDate"] = endDate.ToShortDateString();
                                break;
                            case "Quarterly":
                                endDate = startDate.AddMonths(3).Date;
                                tb.Text = endDate.ToShortDateString();
                                dr["EndDate"] = endDate.ToShortDateString();
                                break;
                            case "Semi-Annual":
                                endDate = startDate.AddMonths(6).Date;
                                tb.Text = endDate.ToShortDateString();
                                dr["EndDate"] = endDate.ToShortDateString();
                                break;
                            case "Annual":
                                endDate = startDate.AddYears(1).Date;
                                tb.Text = endDate.ToShortDateString();
                                dr["EndDate"] = endDate.ToShortDateString();
                                break;
                            default:
                                break;
                        }
                    }
                    else if (i > 0 && j == 1)
                    {
                        DateTime endDate;
                        switch (frequency.Trim())
                        {
                            case "Monthly":
                                endDate = startDate.AddMonths(1).Date;
                                tb.Text = endDate.ToShortDateString();
                                dr["EndDate"] = endDate.ToShortDateString();
                                break;
                            case "Quarterly":
                                endDate = startDate.AddMonths(3).Date;
                                tb.Text = endDate.ToShortDateString();
                                dr["EndDate"] = endDate.ToShortDateString();
                                break;
                            case "Semi-Annual":
                                endDate = startDate.AddMonths(6).Date;
                                tb.Text = endDate.ToShortDateString();
                                dr["EndDate"] = endDate.ToShortDateString();
                                break;
                            case "Annual":
                                endDate = startDate.AddYears(1).Date;
                                tb.Text = endDate.ToShortDateString();
                                dr["EndDate"] = endDate.ToShortDateString();
                                break;
                            default:
                                break;
                        }
                    }
                    if (j == 2)
                    {
                        DateTime settlementDt = Convert.ToDateTime(txtBoxAddMaturityDate.SelectedDate);                   //where to take this value
                        DateTime endDt = Convert.ToDateTime(dr["EndDate"]);
                        decimal days = (endDt - settlementDt).Days;
                        decimal totalDays = 365;
                        decimal coupFreqDays = days / totalDays;
                        if (days > 0)
                            dr["CoupFrac"] = Convert.ToDecimal(coupFreqDays).ToString("0.00");
                    }
                    if (j == 3)
                    {
                        tb.Text = txtNotional.Text;
                        if (i == 0)
                        {
                            firstNationalRow = Convert.ToDecimal(txtNotional.Text);
                            nationalRow = firstNationalRow;
                        }
                        else

                            nationalRow = Convert.ToDecimal(tb.Text);
                        dr["Notation"] = nationalRow;
                    }
                    if (j == 4)
                    {
                        DateTime dt = Convert.ToDateTime(dr["StartDate"]);
                        if (txtCouponDate.SelectedDate != null && dt >= txtCouponDate.SelectedDate)
                        {
                            //  int activeCoupons = Convert.ToInt16(txtActiveCoupon.Text);             //where to take this value
                            if (firstAmortizingRow == false)
                                activeCoupons = rowsCount - i;
                            Decimal amortisation = Convert.ToDecimal(firstNationalRow / activeCoupons);
                            dr["Amortisation"] = Convert.ToDecimal(amortisation).ToString("0.00");
                            firstAmortizingRow = true;
                        }
                        else
                            dr["Amortisation"] = "0.00";
                    }
                    if (j == 5)
                    {
                        Decimal factorRow = Convert.ToDecimal(nationalRow / firstNationalRow);
                        tb.Text = factorRow.ToString();
                        dr["Factor"] = factorRow;
                    }
                    // Add the control to the TableCell
                    cell.Controls.Add(tb);

                    // Add the TableCell to the TableRow

                }
                dt1.Rows.Add(dr);
                // Add the TableRow to the Table

            }
            pnlAmortizing.Visible = true;
            grdAmortizing.Visible = true;
            btnCalculatSchedule.Visible = true;
            grdAmortizing.DataSource = dt1;
            grdAmortizing.DataBind();
            // grdAmortizing.Rebind();
        }

        private void GenerateTable(int colsCount, int rowsCount, Decimal Notation, int index)
        {
            try
            {
                Decimal firstNationalRow = Convert.ToDecimal(dt1.Rows[0][3]);
                Decimal nationalRow = Notation;
                Decimal factorRow = Convert.ToDecimal(nationalRow / firstNationalRow);
                dt1.Rows[index][4] = factorRow.ToString("0.00000");
                grdAmortizing.Visible = true;
                btnCalculatSchedule.Visible = true;
                pnlAmortizing.Visible = true;
                Session["LoanScheduleData"] = dt1;
                grdAmortizing.DataSource = dt1;
                grdAmortizing.DataBind();

            }
            catch (Exception)
            {

            }
        }

        private void CalculateSchedule(int colsCount, int rowsCount, Decimal Notation, int index)
        {
            try
            {
                Decimal firstNationalRow = 0;
                dt1.Rows[index][4] = Convert.ToDecimal(Notation);

                firstNationalRow = Convert.ToDecimal(dt1.Rows[0][4]);


                Decimal nationalRow = Notation;
                Decimal factorRow = Convert.ToDecimal(nationalRow / firstNationalRow);
                dt1.Rows[index][6] = factorRow.ToString("0.00000");
            }
            catch (Exception)
            {


            }
        }


        private void CalculateSchedule(int colsCount, int rowsCount, Decimal Notation, int index, DateTime startDate, DateTime endDate)
        {
            try
            {
                Decimal firstNationalRow = 0;
                dt1.Rows[index][1] = startDate;
                dt1.Rows[index][2] = endDate;

                dt1.Rows[index][4] = Convert.ToDecimal(Notation);

                firstNationalRow = Convert.ToDecimal(dt1.Rows[0][4]);


                Decimal nationalRow = Notation;
                Decimal factorRow = Convert.ToDecimal(nationalRow / firstNationalRow);
                dt1.Rows[index][6] = factorRow.ToString("0.00000");

                Session["LoanScheduleData"] = dt1;
                //dt1.Rows[index][7] = Convert.ToDecimal(txtBoxAddMargin.Text);

                //dt1.Rows[index][8] = Convert.ToDateTime(endDate);

                //if (ddlAddCurrency.SelectedValue.ToLower() == "eur")
                //{
                //    EURCurvesBL eurCurveBL = new EURCurvesBL();
                //    List<EURCurve> eurCurve = eurCurveBL.GetEURCurve();
                //    Dictionary<double, double> rateVals = new Dictionary<double, double>();
                //    foreach (var item in eurCurve)
                //    {
                //        rateVals.Add(Convert.ToDouble(item.RateDate.Value.Ticks), Convert.ToDouble(item.Rate));
                //    }
                //    var scaler = new SplineInterpolator(rateVals);
                //    var y = scaler.GetValue(Convert.ToDateTime(endDate).Ticks);
                //    dt1.Rows[index][9] = Math.Exp(-((y / 100) * (((Convert.ToDateTime(endDate) - Convert.ToDateTime(txtBoxAddSigningDate.SelectedDate)).Days)) / 365));
                //}

                //dt1.Rows[index][10] = Convert.ToDouble(dt1.Rows[index][9]) / Convert.ToDouble((Math.Pow(Convert.ToDouble((1 + (Convert.ToDouble(txtBoxAddMargin.Text) / 20000))), ((2 * (Convert.ToDateTime(endDate) - Convert.ToDateTime(txtBoxAddSigningDate.SelectedDate)).Days) / 360))));

                //if (index == 0)
                //{
                //    dt1.Rows[index][10] = 0.5;
                //}
                //else
                //{
                //    double d = Convert.ToDouble(dt1.Rows[index - 1][dt1.Rows[index][9]) / Convert.ToDouble(dr["RiskFreeDP1"]) - 1 / ((Convert.ToDateTime(dr["EndDate"]) - Convert.ToDateTime(dt1.Rows[i - 1]["EndDate"])).Days) / 360;
                //}

                //  SplineInterpolator interPolation = new  SplineInterpolator();
                // dr["RiskFreeDP1"]=  ;//EXP(-(interpolate(schedule.endDate(i),EurDB.Rates,'EurDB.Rates')/100*(schedule.endDate(i)-loan.settlementDate)/365));

            }
            catch (Exception)
            {


            }

        }
        //private void CalculateSchedule(int colsCount, int rowsCount, Decimal Notation, int noOfActiveCoupons, Decimal loanNotational, DateTime amortisationStartDate, int index)
        //{
        //    try
        //    {     Decimal firstNotionalRow = 0;
        //if (dt1.Rows[index][1] > amortisationStartDate) 
        //{

        //            dt1.Rows[index][3] = Convert.ToDecimal(loanNotation);

        //            firstNotionalRow = Convert.ToDecimal(dt1.Rows[0][3]);


        //            Decimal notionalRow = Notation;
        //            Decimal factorRow = Convert.ToDecimal(loanNotational / noOfActiveCoupons);
        //            dt1.Rows[index][4] = factorRow.ToString("0.00000");
        //}
        //else
        //{
        //    firstNotionalRow = Convert.ToDecimal(1);	
        //}

        //    }
        //    catch (Exception)
        //    {


        //    }
        //}


        protected void txtStartDate_TextChanged(object sender, EventArgs e)
        {
            //RadTextBox txt = (RadTextBox)sender;
            //foreach (System.Data.DataRow drow in dt1.Rows)
            //{
            //    if (string.Compare((string)drow["StartDate"], txt.Text, true) != 0)
            //    {
            //        drow["StartDate"] = txt.Text;
            //        // drow["sItemCostAmount"] = int.Parse(drow["itemQuantity"].ToString()) * double.Parse(txt.Text);
            //    }
            //}
            RadTextBox txt = (RadTextBox)sender;
            foreach (System.Data.DataRow drow in dt1.Rows)
            {
                if (string.Compare((string)drow["StartDate"], txt.ToolTip, true) == 0)
                {
                    dt1.Rows[Convert.ToInt16(drow["ID"])]["StartDate"] = txt.Text;
                    GenerateTable(15, Convert.ToInt32(txtAmortisations.Text), Convert.ToDateTime(txt.Text));
                    return;
                    //drow["sItemCostAmount"] = int.Parse(drow["itemQuantity"].ToString()) * double.Parse(txt.Text);
                }
            }
        }

        protected void grdAmortizing_ItemDataBound(object sender, GridItemEventArgs e)
        {
            //DataTable dt = (DataTable)e.Item.DataItem;
            //RadTextBox txtbx = (RadTextBox)e.Item.FindControl("txtStartDate");
            //txtbx.Text = "";
            if (e.Item is GridDataItem)
            {
                GridDataItem item = e.Item as GridDataItem;

                foreach (GridColumn column in item.OwnerTableView.RenderColumns)
                {
                    if (column.UniqueName == "txtStartDate")
                    {
                        RadTextBox txt = new RadTextBox();
                        txt.Text = ((RadTextBox)item.FindControl("txtStartDate")).Text;
                        //txt.AutoPostBack = true;
                        //txt.TextChanged += new EventHandler(txtStartDate_TextChanged);
                        //txt.AutoPostBack = true;

                    }
                }
            }
        }



        protected void txtNotation_TextChanged(object sender, EventArgs e)
        {
            RadTextBox txt = (RadTextBox)sender;
            //  timer1.Enabled = false;
            foreach (System.Data.DataRow drow in dt1.Rows)
            {
                string toolTip = txt.ToolTip;
                string[] strSplit = toolTip.Split('-');
                string strID = strSplit[0].ToString();
                strID = strID.Remove(0, 8);
                int id = Convert.ToInt16(strID) - 1;
                if (string.Compare((string)drow["ID"], id.ToString(), true) == 0)
                {
                    if (dt1.Rows[Convert.ToInt16(drow["ID"])]["Notation"] != txt.Text)
                    {
                        dt1.Rows[Convert.ToInt16(drow["ID"])]["Notation"] = txt.Text;
                        GenerateTable(15, Convert.ToInt32(txtAmortisations.Text), Convert.ToDecimal(txt.Text), Convert.ToInt16(drow["ID"]));
                        return;
                    }


                    //drow["sItemCostAmount"] = int.Parse(drow["itemQuantity"].ToString()) * double.Parse(txt.Text);
                }
            }
            //   timer1.Enabled = true;
        }

        protected void txtCouponDate_SelectedDateChanged(object sender, Telerik.Web.UI.Calendar.SelectedDateChangedEventArgs e)
        {
            //timer1.Enabled = false;
            //if (txtAmortisations.Text != string.Empty && ddlAmortizing.SelectedValue == "Yes")
            //    GenerateTable(6, Convert.ToInt32(txtAmortisations.Text));
            //timer1.Enabled = true;
        }

        protected void txtBoxMaturityDateA_SelectedDateChanged(object sender, Telerik.Web.UI.Calendar.SelectedDateChangedEventArgs e)
        {
            ComputeCouponDatesAndFractions(txtBoxTradeDate1, txtLoanNameA, txtBoxSettlementDate1, txtBoxMaturityDateA, txtBoxCouponFrequencyLoanA, txtAveLifNonDiscA, grdCalculatedDates1, 1, txtBoxNotional1, txtBoxInterestRateA, txtBoxCurrencyLoanA);
        }

        protected void txtBoxMaturityDateB_SelectedDateChanged(object sender, Telerik.Web.UI.Calendar.SelectedDateChangedEventArgs e)
        {
            ComputeCouponDatesAndFractions(txtBoxTradeDate2, txtLoanNameB, txtBoxSettlementDate2, txtBoxMaturityDateB, txtBoxCouponFrequencyLoanB, txtBoxAveLifNonDiscB, grdCalculatedDates2, 2, txtBoxNotional2, txtInterestRateB, txtCurrencyB);
        }

        protected void txtMaturityDateC_SelectedDateChanged(object sender, Telerik.Web.UI.Calendar.SelectedDateChangedEventArgs e)
        {
            ComputeCouponDatesAndFractions(txtBoxTradeDate3, txtLoanNameC, txtBoxSettlementDate3, txtMaturityDateC, txtBoxCouponFrequencyLoanC, txtBoxAveLifNonDiscC, grdCalculatedDates3, 3, txtBoxNotional3, txtInterestRateC, txtCurrencyC);
        }

        protected void btnCalculatSchedule_Click(object sender, EventArgs e)
        {
            CalculateFactors();
        }

        private void CalculateFactors()
        {
            try
            {
                int i = 0;
                foreach (GridDataItem row in grdAmortizing.Items)
                {
                    RadTextBox rt = (RadTextBox)row.FindControl("txtNotation");
                    RadTextBox rStartDate = (RadTextBox)row.FindControl("txtStartDate");
                    RadTextBox rEndDate = (RadTextBox)row.FindControl("txtEndDate");
                    if (rt != null)
                    {
                        //CalculateSchedule(15, Convert.ToInt32(txtAmortisations.Text), Convert.ToDecimal(rt.Text), Convert.ToInt16(i));
                        CalculateSchedule(15, Convert.ToInt32(txtAmortisations.Text), Convert.ToDecimal(rt.Text), Convert.ToInt16(i), Convert.ToDateTime(rStartDate.Text), Convert.ToDateTime(rEndDate.Text));
                    }
                    i++;
                }
                if (Session["LoanScheduleData"] != null)
                {
                    grdAmortizing.DataSource = (DataTable)Session["LoanScheduleData"];
                    grdAmortizing.DataBind();
                }
                //grdAmortizing.DataSource = dt1;
                //grdAmortizing.DataBind();
            }
            catch (Exception)
            {
            }

        }

        protected void btnSchedule_Click(object sender, EventArgs e)
        {
            firstAmortizingRow = false;
            if (txtAmortisations.Text != string.Empty)
            {
                if (txtNotional.Text == "0.00" || txtNotional.Text == "0")
                {
                    RadWindowManager1.RadAlert("Notional must be grater then 0", 330, 180, "realedge associates", "alertCallBackFn");
                }
                else
                {
                    GenerateTable(15, Convert.ToInt32(txtAmortisations.Text));
                }
            }
            else
                RadWindowManager1.RadAlert("Loan schedule cannot be generated as Field no of amortisations is missing", 330, 180, "realedge associates", "alertCallBackFn");
        }

        protected void btnChangePassword_Click(object sender, EventArgs e)
        {
            try
            {
                if (ChangePasswordValidations())
                {
                    DAL.Login login = Session["LogedInUser"] as DAL.Login;
                    if (txtOldPassword.Text.Trim() == login.Password)
                    {
                        LoginBLL bll = new LoginBLL();
                        if (bll.IsValidPasword(txtNewPassword.Text.Trim()))
                        {
                            LoginBLL loginBLL = new LoginBLL();
                            loginBLL.ChangePassword(login.ID, txtNewPassword.Text.Trim());
                            RadWindowManager1.RadAlert("Password has been change successfully", 330, 180, "realedge associates", "alertCallBackFn");
                        }
                        else
                        {
                            RadWindowManager1.RadAlert("Password must be 8 to 15 characters long which contain at least one lowercase letter, one uppercase letter, one numeric digit, and one special character", 330, 180, "realedge associates", "alertCallBackFn");
                        }

                    }
                    else
                        RadWindowManager1.RadAlert("Old Password does not match", 330, 180, "realedge associates", "alertCallBackFn");
                }
            }
            catch (Exception)
            {


            }
        }

        private Boolean ChangePasswordValidations()
        {
            Boolean flag = true;
            if (txtOldPassword.Text.Trim() == string.Empty)
            {
                RadWindowManager1.RadAlert("Old Password required", 330, 180, "realedge associates", "alertCallBackFn");
                return false;
            }
            if (txtNewPassword.Text.Trim() == string.Empty)
            {
                RadWindowManager1.RadAlert("New Password required", 330, 180, "realedge associates", "alertCallBackFn");
                return false;
            }
            if (txtOldPassword.Text.Trim() == string.Empty)
            {
                RadWindowManager1.RadAlert("Confirm Password required", 330, 180, "realedge associates", "alertCallBackFn");
                return false;
            }
            if (txtNewPassword.Text.Trim() != txtConfirmPassword.Text.Trim())
            {
                RadWindowManager1.RadAlert("New Password and Confirm password must be same", 330, 180, "realedge associates", "alertCallBackFn");
                return false;
            }
            return true;
        }

        protected void grdQuotesAndTrades_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            //QuotesAndTradesBLL bll = new QuotesAndTradesBLL();



            grdQuotesAndTrades.DataSource = PopulateQuotes;
        }

        protected void grdQuotesAndTrades_UpdateCommand(object sender, GridCommandEventArgs e)
        {
            GridEditableItem editedItem = e.Item as GridEditableItem;
            QuotesAndTradesBLL quotesAndTradeBLL = new QuotesAndTradesBLL();
            Hashtable newValues = new Hashtable();
            e.Item.OwnerTableView.ExtractValuesFromItem(newValues, editedItem);

            QuotesAndTrades quotesAndTrades = new QuotesAndTrades();
            quotesAndTrades.BidPrice = Convert.ToDecimal(newValues["BidPrice"]);
            quotesAndTrades.BidSpread = Convert.ToDecimal(newValues["BidSpread"]);
            RadDropDownList ddlParty = (RadDropDownList)editedItem.FindControl("ddlQCounterParty");
            quotesAndTrades.CounterParty = ddlParty.SelectedValue;

            RadDropDownList ddlCountry = (RadDropDownList)editedItem.FindControl("ddlQCountry");
            quotesAndTrades.Country = ddlCountry.SelectedValue;
            //quotesAndTrades.Country = Convert.ToString(newValues["Country"]);
            quotesAndTrades.LoanName = Convert.ToString(newValues["LoanName"]);
            quotesAndTrades.MarketValue = Convert.ToDecimal(newValues["MarketValue"]);
            quotesAndTrades.OfferPrice = Convert.ToDecimal(newValues["OfferPrice"]);
            quotesAndTrades.OfferSpread = Convert.ToDecimal(newValues["OfferSpread"]);
            quotesAndTrades.TimeStamp = Convert.ToDateTime(newValues["TimeStamp"]);
            quotesAndTrades.Traded = Convert.ToBoolean(newValues["Traded"]);
            quotesAndTrades.ID = Convert.ToInt32(grdQuotesAndTrades.MasterTableView.Items[0].GetDataKeyValue("ID"));

            quotesAndTrades.AvgLifeNonDisc = Convert.ToDecimal(newValues["AvgLifeNonDisc"]);
            quotesAndTrades.AvgLifeDisc = Convert.ToDecimal(newValues["AvgLifeDisc"]);
            quotesAndTrades.AvgLifeRiskDisc = Convert.ToDecimal(newValues["AvgLifeRiskDisc"]);
            quotesAndTrades.TimeStamp = DateTime.UtcNow;

            quotesAndTrades.SettlementDate = Convert.ToDateTime(newValues["SettlementDate"]);
            quotesAndTrades.Margin = Convert.ToString(newValues["Margin"]);
            quotesAndTrades.TradedDate = Convert.ToDateTime(newValues["TradedDate"]);
            //quotesAndTradeBLL.SaveQuote(quotesAndTrades);
            quotesAndTradeBLL.UpdateQuote(quotesAndTrades);
            BindHistoricalQuotesAndTradesTab();

            LogActivity("Quotes and Trades Updated Successfully", "Edit Quotes and Trades", string.Empty);
        }

        protected void btnClearAll_Click(object sender, EventArgs e)
        {
            ClearA();
            ClearB();
            ClearC();
        }

        protected void txtNotional_TextChanged(object sender, EventArgs e)
        {
            txtNotional.Text = Convert.ToDecimal(txtNotional.Text).ToString("N");
        }


        #region Bind Credit Rating Tree

        public void BindCreditRatingTree()
        {



            CreditAgencyBL agencyBL = new CreditAgencyBL();
            List<CreditAgency> agency = agencyBL.GetAgencies();

            List<TreeViewData> treeData = new List<TreeViewData>();
            //treeData.Add(new TreeViewData(1,null, "Credit Rating","Credit Rating"));
            int i = 1;
            int j = 1;
            foreach (var item in agency)
            {
                treeData.Add(new TreeViewData(i.ToString(), null, item.CreditAgency1, item.CreditAgency1));

                i++;


                CreditRatingsBL ratingbl = new CreditRatingsBL();
                List<CreditRating> rating = ratingbl.GetRatingsByAgencyID(item.ID);
                int innerVal = 0;
                int totalCount = rating.Count;
                foreach (var a in rating)
                {

                    treeData.Add(new TreeViewData(i.ToString(), j.ToString(), a.Rating, a.ID.ToString()));
                    innerVal++;
                    if (innerVal == totalCount)
                    {
                        j = i;
                    }
                    i++;

                }
                j++;
            }



            tvCreditRating.DataTextField = "Text";
            tvCreditRating.DataFieldID = "ID";
            tvCreditRating.DataFieldParentID = "ParentID";

            tvCreditRating.DataValueField = "Val";
            tvCreditRating.DataSource = treeData;

            // tvCreditRating.DataBindings.Add(binding1);
            //tvCreditRating.CheckBoxes = true;
            tvCreditRating.DataBind();

        }

        private static string selectedValues;
        private static List<RatingDetails> ShowCheckedNodes(RadDropDownTree treeView)
        {
            string message = string.Empty;
            //IList<RadTreeNode> nodeCollection = treeView.drop;
            List<RatingDetails> ratingDetails = new List<RatingDetails>();
            //foreach (RadTreeNode node in nodeCollection)
            //{
            //    //message += node.FullPath + "<br/>";
            //    string[] spltArray = node.FullPath.Split('/');
            //    string strAgencyName = spltArray[1].ToString();
            //    string strRating = spltArray[2].ToString();
            //    ratingDetails.Add(new RatingDetails(strAgencyName, strRating));

            //}

            string[] child = new string[100];
            string[] parent = new string[100];
            int i = 0, j = 0;
            foreach (RadTreeNode node in treeView.EmbeddedTree.Nodes)
            {
                parent[j] = node.Text;
                j++;
                if (node.GetAllNodes().Count != 0)
                {
                    foreach (RadTreeNode subnode in node.GetAllNodes())
                    {
                        child[i] = subnode.Text;
                        bool checkedSubNode = subnode.Checked;
                        if (checkedSubNode == true)
                        {

                            //selectedValues = subnode.Value.ToString();
                            //CreditRatingsBL creaditRatingBL = new CreditRatingsBL();
                            //int agencyID = creaditRatingBL.GetByID(Convert.ToInt16(subnode.Value)).CreditAgencyID.Value;
                            //CreditAgencyBL agencyBL = new CreditAgencyBL();
                            //string agencyName = agencyBL.GetByID(agencyID).CreditAgency1.ToString();
                            ratingDetails.Add(new RatingDetails(node.Text, subnode.Text));

                        }
                        i++;
                    }
                }
            }
            return ratingDetails;
        }

        private void CheckNodes(RadDropDownTree treeView, Loans loan)
        {

            //RadTreeNode nodeCollection = treeView.Nodes[0];
            //foreach (var item in nodeCollection.Nodes)
            //{
            //    switch (((Telerik.Web.UI.RadTreeNode)(item)).Text)
            //    {
            //        case "Moody's":
            //            foreach (var node in nodeCollection.Nodes.FindNodeByText("Moody's").Nodes)
            //            {
            //                if (((Telerik.Web.UI.RadTreeNode)(node)).Text == loan.CreditRatingModys)
            //                {
            //                    ((Telerik.Web.UI.RadTreeNode)(node)).Checked = true;
            //                }
            //            }
            //            break;
            //        case "ING":
            //            foreach (var node in nodeCollection.Nodes.FindNodeByText("ING").Nodes)
            //            {
            //                if (((Telerik.Web.UI.RadTreeNode)(node)).Text == loan.CreditRatingING)
            //                {
            //                    ((Telerik.Web.UI.RadTreeNode)(node)).Checked = true;
            //                }
            //            }
            //            break;
            //        case "S&P's":
            //            foreach (var node in nodeCollection.Nodes.FindNodeByText("S&P's").Nodes)
            //            {
            //                if (((Telerik.Web.UI.RadTreeNode)(node)).Text == loan.CreditRatingSPs)
            //                {
            //                    ((Telerik.Web.UI.RadTreeNode)(node)).Checked = true;
            //                }
            //            }
            //            break;
            //        case "Fitch":
            //            foreach (var node in nodeCollection.Nodes.FindNodeByText("Fitch").Nodes)
            //            {
            //                if (((Telerik.Web.UI.RadTreeNode)(node)).Text == loan.CreditRatingFitch)
            //                {
            //                    ((Telerik.Web.UI.RadTreeNode)(node)).Checked = true;
            //                }
            //            }
            //            break;
            //        default:
            //            break;
            //    }
            //}
            string[] child = new string[100];
            string[] parent = new string[100];
            int i = 0, j = 0;
            string strSelectedText = string.Empty;
            foreach (RadTreeNode node in treeView.EmbeddedTree.Nodes)
            {
                parent[j] = node.Text;
                j++;
                switch (node.Text)
                {
                    case "Moody's":
                        if (node.GetAllNodes().Count != 0)
                        {
                            foreach (RadTreeNode subnode in node.GetAllNodes())
                            {
                                child[i] = subnode.Text;
                                if (subnode.Text == loan.CreditRatingModys)
                                {
                                    subnode.Checked = true;
                                    if (strSelectedText == string.Empty)
                                        strSelectedText = subnode.Text;
                                    else
                                        strSelectedText = strSelectedText + ";" + subnode.Text;

                                }

                                i++;
                            }
                        }
                        break;
                    case "ING":
                        if (node.GetAllNodes().Count != 0)
                        {
                            foreach (RadTreeNode subnode in node.GetAllNodes())
                            {
                                child[i] = subnode.Text;
                                if (subnode.Text == loan.CreditRatingING)
                                {
                                    subnode.Checked = true;
                                    if (strSelectedText == string.Empty)
                                        strSelectedText = subnode.Text;
                                    else
                                        strSelectedText = strSelectedText + ";" + subnode.Text;
                                }

                                i++;
                            }
                        }
                        break;
                    case "S&P's":
                        if (node.GetAllNodes().Count != 0)
                        {
                            foreach (RadTreeNode subnode in node.GetAllNodes())
                            {
                                child[i] = subnode.Text;
                                if (subnode.Text == loan.CreditRatingSPs)
                                {
                                    subnode.Checked = true;
                                    if (strSelectedText == string.Empty)
                                        strSelectedText = subnode.Text;
                                    else
                                        strSelectedText = strSelectedText + ";" + subnode.Text;
                                }

                                i++;
                            }
                        }
                        break;
                    case "Fitch":
                        if (node.GetAllNodes().Count != 0)
                        {
                            foreach (RadTreeNode subnode in node.GetAllNodes())
                            {
                                child[i] = subnode.Text;
                                if (subnode.Text == loan.CreditRatingFitch)
                                {
                                    subnode.Checked = true;
                                    if (strSelectedText == string.Empty)
                                        strSelectedText = subnode.Text;
                                    else
                                        strSelectedText = strSelectedText + ";" + subnode.Text;
                                }

                                i++;
                            }
                        }
                        break;
                }

                treeView.SelectedText = strSelectedText;
            }
        }
        #endregion

        internal class RatingDetails
        {
            public string agencyName { get; set; }
            public string rating { get; set; }

            public RatingDetails(string agencyName, string rating)
            {
                this.agencyName = agencyName;
                this.rating = rating;
            }
        }


        internal class TreeViewData
        {
            private string text;
            private string id;
            private string parentId;
            private string val;
            public string Text
            {
                get { return text; }
                set { text = value; }
            }


            public string ID
            {
                get { return id; }
                set { id = value; }
            }

            public string ParentID
            {
                get { return parentId; }
                set { parentId = value; }
            }

            public string Val
            {
                get { return val; }
                set { val = value; }
            }
            public TreeViewData(string id, string parentId, string text, string val)
            {
                this.id = id;
                this.parentId = parentId;
                this.text = text;
                this.val = val;
            }
        }

        protected void grdQuotesAndTrades_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item.IsInEditMode)
            {
                GridEditableItem item = (GridEditableItem)e.Item;
                if (!(e.Item is IGridInsertItem))
                {
                    CounterPartyBL counterPartyBL = new CounterPartyBL();
                    RadDropDownList ddlCounterParty =
                          (RadDropDownList)item.FindControl("ddlQCounterParty");
                    ddlCounterParty.DataSource = counterPartyBL.GetCounterParty();
                    ddlCounterParty.DataTextField = "Name";
                    ddlCounterParty.DataValueField = "Name";
                    ddlCounterParty.DataBind();
                    ddlCounterParty.SelectedValue = ((DAL.QuotesAndTrades)((e.Item).DataItem)).CounterParty;


                    CountryBL countryBL = new CountryBL();
                    RadDropDownList ddlCountry = (RadDropDownList)item.FindControl("ddlQCountry");
                    ddlCountry.DataSource = countryBL.GetCountry();
                    ddlCountry.DataTextField = "Name";
                    ddlCountry.DataValueField = "Name";
                    ddlCountry.DataBind();
                    ddlCountry.SelectedValue = ((DAL.QuotesAndTrades)((e.Item).DataItem)).Country;
                }
            }
        }

        protected void tvCreditRating_NodeDataBound(object sender, DropDownTreeNodeDataBoundEventArguments e)
        {
            if (e.DropDownTreeNode.Level == 0)
                e.DropDownTreeNode.Checkable = false;
        }

        protected void tvCreditRating_DataBound(object sender, EventArgs e)
        {
            tvCreditRating.ExpandAllDropDownNodes();
        }

        protected void grdQuotesAndTrades_FilterCheckListItemsRequested(object sender, GridFilterCheckListItemsRequestedEventArgs e)
        {
            DataTable data = new DataTable();

            switch (e.Column.DataField)
            {
                case "LoanName":
                    data.Columns.Add("CodeName");
                    LoansBLL loansBLL = new LoansBLL();
                    List<Loans> loans = loansBLL.GetLoans();

                    for (int i = 0; i < loans.Count - 1; i++)
                    {
                        data.Rows.Add(loans[i].CodeName);
                    }

                    e.ListBox.DataSource = data;
                    e.ListBox.DataKeyField = "CodeName";
                    e.ListBox.DataTextField = "CodeName";
                    e.ListBox.DataValueField = "CodeName";
                    e.ListBox.DataBind();

                    break;
                default:
                    break;
            }
        }

        protected void grdLoans_FilterCheckListItemsRequested(object sender, GridFilterCheckListItemsRequestedEventArgs e)
        {


            DataTable data = new DataTable();

            switch (e.Column.DataField)
            {
                case "CodeName":
                    data.Columns.Add("CodeName");
                    LoansBLL loansBLL = new LoansBLL();
                    List<Loans> loans = loansBLL.GetLoans();

                    for (int i = 0; i <= loans.Count - 1; i++)
                    {
                        data.Rows.Add(loans[i].CodeName);
                    }

                    e.ListBox.DataSource = data;
                    e.ListBox.DataKeyField = "CodeName";
                    e.ListBox.DataTextField = "CodeName";
                    e.ListBox.DataValueField = "CodeName";
                    e.ListBox.DataBind();

                    break;
                case "Borrower":
                    data = new DataTable();
                    data.Columns.Add("Borrower");

                    BorrowersBL borrowerBL = new BorrowersBL();
                    List<Borrower> borrower = borrowerBL.GetBorrowers();
                    for (int i = 0; i < borrower.Count - 1; i++)
                    {
                        data.Rows.Add(borrower[i].Name);
                    }

                    e.ListBox.DataSource = data;
                    e.ListBox.DataKeyField = "Borrower";
                    e.ListBox.DataTextField = "Borrower";
                    e.ListBox.DataValueField = "Borrower";
                    e.ListBox.DataBind();
                    break;
                case "Country":
                    data = new DataTable();
                    data.Columns.Add("Country");

                    CountryBL CountryBL = new CountryBL();
                    List<tblCountry> country = CountryBL.GetCountry();
                    for (int i = 0; i < country.Count - 1; i++)
                    {
                        data.Rows.Add(country[i].Name);
                    }

                    e.ListBox.DataSource = data;
                    e.ListBox.DataKeyField = "Country";
                    e.ListBox.DataTextField = "Country";
                    e.ListBox.DataValueField = "Country";
                    e.ListBox.DataBind();
                    break;
                case "CreditRating":
                    data = new DataTable();
                    data.Columns.Add("CreditRating");

                    CreditRatingsBL CreditRatingsBL = new CreditRatingsBL();
                    List<CreditRating> CreditRating = CreditRatingsBL.GetRatings();
                    for (int i = 0; i < CreditRating.Count - 1; i++)
                    {
                        data.Rows.Add(CreditRating[i].Rating);
                    }

                    e.ListBox.DataSource = data;
                    e.ListBox.DataKeyField = "CreditRating";
                    e.ListBox.DataTextField = "CreditRating";
                    e.ListBox.DataValueField = "CreditRating";
                    e.ListBox.DataBind();
                    break;
                case "Sector":
                    data = new DataTable();
                    data.Columns.Add("Sector");

                    LoansBLL loansBll = new LoansBLL();
                    List<Sectors> sector = loansBll.GetSector();

                    for (int i = 0; i < sector.Count; i++)
                    {
                        if (sector[i] != null)
                        {
                            data.Rows.Add(sector[i].Sector);
                        }
                    }

                    e.ListBox.DataSource = data;
                    e.ListBox.DataKeyField = "Sector";
                    e.ListBox.DataTextField = "Sector";
                    e.ListBox.DataValueField = "Sector";
                    e.ListBox.DataBind();
                    break;
                case "PP":
                    data = new DataTable();
                    data.Columns.Add("PP");

                    loansBll = new LoansBLL();
                    List<PP> pp = loansBll.GetPP();

                    for (int i = 0; i < pp.Count; i++)
                    {
                        if (pp[i] != null)
                        {
                            data.Rows.Add(pp[i].pp);
                        }
                    }

                    e.ListBox.DataSource = data;
                    e.ListBox.DataKeyField = "PP";
                    e.ListBox.DataTextField = "PP";
                    e.ListBox.DataValueField = "PP";
                    e.ListBox.DataBind();
                    break;
                case "FixedOrFloating":
                    data = new DataTable();
                    data.Columns.Add("FixedOrFloating");

                    loansBll = new LoansBLL();
                    List<BLL.FixedOrFloating> fof = loansBll.GetFixedFloating();

                    for (int i = 0; i < fof.Count; i++)
                    {
                        if (fof[i] != null)
                        {
                            data.Rows.Add(fof[i].fixedorfloating);
                        }
                    }



                    e.ListBox.DataSource = data;
                    e.ListBox.DataKeyField = "FixedOrFloating";
                    e.ListBox.DataTextField = "FixedOrFloating";
                    e.ListBox.DataValueField = "FixedOrFloating";
                    e.ListBox.DataBind();
                    break;
                case "Notional":
                    data = new DataTable();
                    data.Columns.Add("Notional");

                    loansBll = new LoansBLL();
                    List<BLL.Notional> notional = loansBll.GetNotional();

                    for (int i = 0; i < notional.Count; i++)
                    {
                        if (notional[i] != null)
                        {
                            data.Rows.Add(notional[i].notional);
                        }
                    }



                    e.ListBox.DataSource = data;
                    e.ListBox.DataKeyField = "Notional";
                    e.ListBox.DataTextField = "Notional";
                    e.ListBox.DataValueField = "Notional";
                    e.ListBox.DataBind();
                    break;
                case "Margin":
                    data = new DataTable();
                    data.Columns.Add("Margin");

                    loansBll = new LoansBLL();
                    List<BLL.Margin> margin = loansBll.GetMargin();

                    for (int i = 0; i < margin.Count; i++)
                    {
                        if (margin[i] != null)
                        {
                            data.Rows.Add(margin[i].margin);
                        }
                    }



                    e.ListBox.DataSource = data;
                    e.ListBox.DataKeyField = "Margin";
                    e.ListBox.DataTextField = "Margin";
                    e.ListBox.DataValueField = "Margin";
                    e.ListBox.DataBind();
                    break;
                case "Currency":
                    data = new DataTable();
                    data.Columns.Add("Currency");

                    CurrenciesBL currencyBL = new CurrenciesBL();
                    List<Currency> currency = currencyBL.GetCurrency();

                    for (int i = 0; i < currency.Count; i++)
                    {
                        if (currency[i].Currancy != null)
                        {
                            data.Rows.Add(currency[i].Currancy);
                        }
                    }



                    e.ListBox.DataSource = data;
                    e.ListBox.DataKeyField = "Currency";
                    e.ListBox.DataTextField = "Currency";
                    e.ListBox.DataValueField = "Currency";
                    e.ListBox.DataBind();
                    break;
                case "CouponFrequency":
                    data = new DataTable();
                    data.Columns.Add("CouponFrequency");

                    loansBll = new LoansBLL();
                    List<BLL.CouponFrequency> couponFreq = loansBll.GetCouponFrequency();

                    for (int i = 0; i < couponFreq.Count; i++)
                    {
                        if (couponFreq[i] != null)
                        {
                            data.Rows.Add(couponFreq[i].coupon);
                        }
                    }




                    e.ListBox.DataSource = data;
                    e.ListBox.DataKeyField = "CouponFrequency";
                    e.ListBox.DataTextField = "CouponFrequency";
                    e.ListBox.DataValueField = "CouponFrequency";
                    e.ListBox.DataBind();
                    break;
                case "FacilitySize":
                    data = new DataTable();
                    data.Columns.Add("FacilitySize");

                    loansBll = new LoansBLL();
                    List<BLL.FacilitySize> facility = loansBll.GetFacilitySize();

                    for (int i = 0; i < facility.Count; i++)
                    {
                        if (facility[i] != null)
                        {
                            data.Rows.Add(facility[i].facility);
                        }
                    }

                    e.ListBox.DataSource = data;
                    e.ListBox.DataKeyField = "FacilitySize";
                    e.ListBox.DataTextField = "FacilitySize";
                    e.ListBox.DataValueField = "FacilitySize";
                    e.ListBox.DataBind();
                    break;
                case "CreditRatingModys":
                    data = new DataTable();
                    data.Columns.Add("CreditRatings");

                    loansBll = new LoansBLL();


                    List<CreditRating> creditRatingModdys = loansBll.GetCreditRatingModdys();
                    for (int i = 0; i < creditRatingModdys.Count; i++)
                    {
                        data.Rows.Add(creditRatingModdys[i].Rating);
                    }

                    e.ListBox.DataSource = data;
                    e.ListBox.DataKeyField = "CreditRatings";
                    e.ListBox.DataTextField = "CreditRatings";
                    e.ListBox.DataValueField = "CreditRatings";
                    e.ListBox.DataBind();


                    break;
                case "CreditRatingSPs":
                    data = new DataTable();
                    data.Columns.Add("CreditRatings");

                    loansBll = new LoansBLL();


                    List<CreditRating> creditRatingSPs = loansBll.GetCreditRatingSP();
                    for (int i = 0; i < creditRatingSPs.Count; i++)
                    {
                        data.Rows.Add(creditRatingSPs[i].Rating);
                    }

                    e.ListBox.DataSource = data;
                    e.ListBox.DataKeyField = "CreditRatings";
                    e.ListBox.DataTextField = "CreditRatings";
                    e.ListBox.DataValueField = "CreditRatings";
                    e.ListBox.DataBind();


                    break;
                case "CreditRatingFitch":
                    data = new DataTable();
                    data.Columns.Add("CreditRatings");

                    loansBll = new LoansBLL();


                    List<CreditRating> creditRatingFitch = loansBll.GetCreditRatingFitch();
                    for (int i = 0; i < creditRatingFitch.Count; i++)
                    {
                        data.Rows.Add(creditRatingFitch[i].Rating);
                    }

                    e.ListBox.DataSource = data;
                    e.ListBox.DataKeyField = "CreditRatings";
                    e.ListBox.DataTextField = "CreditRatings";
                    e.ListBox.DataValueField = "CreditRatings";
                    e.ListBox.DataBind();


                    break;
                case "CreditRatingING":
                    data = new DataTable();
                    data.Columns.Add("CreditRatings");

                    loansBll = new LoansBLL();


                    List<CreditRating> creditRatingING = loansBll.GetCreditRatingING();
                    for (int i = 0; i < creditRatingING.Count; i++)
                    {
                        data.Rows.Add(creditRatingING[i].Rating);
                    }

                    e.ListBox.DataSource = data;
                    e.ListBox.DataKeyField = "CreditRatings";
                    e.ListBox.DataTextField = "CreditRatings";
                    e.ListBox.DataValueField = "CreditRatings";
                    e.ListBox.DataBind();


                    break;
                //case "NoOfAmortisation":
                //    data = new DataTable();
                //    data.Columns.Add("NoOfAmortrization");

                //    loansBll = new LoansBLL();
                //    List<BLL.NoOfAmortrization> noOfAmort = loansBll.GetNoOfAmortrization();

                //    for (int i = 0; i < noOfAmort.Count; i++)
                //    {
                //        if (noOfAmort[i] != null)
                //        {
                //            data.Rows.Add(noOfAmort[i].noofAmort);
                //        }
                //    }

                //    e.ListBox.DataSource = data;
                //    e.ListBox.DataKeyField = "noofAmort";
                //    e.ListBox.DataTextField = "noofAmort";
                //    e.ListBox.DataValueField = "noofAmort";
                //    e.ListBox.DataBind();
                //    break;
                case "StructureID":
                    data = new DataTable();
                    data.Columns.Add("StructureID");

                    loansBll = new LoansBLL();
                    List<BLL.StructureID> structure = loansBll.GetStructureID();

                    for (int i = 0; i < structure.Count; i++)
                    {
                        if (structure[i] != null)
                        {
                            data.Rows.Add(structure[i].structureID);
                        }
                    }

                    e.ListBox.DataSource = data;
                    e.ListBox.DataKeyField = "StructureID";
                    e.ListBox.DataTextField = "StructureID";
                    e.ListBox.DataValueField = "StructureID";
                    e.ListBox.DataBind();
                    break;
                default:
                    break;
            }





            //// sector 

            //data = new DataTable();
            //data.Columns.Add("CreditRating");

            //CreditRatingsBL CreditRatingsBL = new CreditRatingsBL();
            //List<CreditRating> CreditRating = CreditRatingsBL.GetRatings();
            //for (int i = 0; i < CreditRating.Count - 1; i++)
            //{
            //    data.Rows.Add(CreditRating[i].Rating);
            //}

            //e.ListBox.DataSource = data;
            //e.ListBox.DataKeyField = "CreditRating";
            //e.ListBox.DataTextField = "CreditRating";
            //e.ListBox.DataValueField = "CreditRating";
            //e.ListBox.DataBind();








        }

        protected void grdLoans_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            grdLoans.DataSource = PopulateLoanGrid;
        }

        protected void ddlLoanDetailsCode_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            Session.Add("LoanDetail", ddlLoanDetailsCode.SelectedValue);
            BindLoanDetailData(ddlLoanDetailsCode.SelectedValue);
        }

        private void BindLoanDetailData()
        {
            if (txtLoanDetailPrice.Text != string.Empty)
                txtLoanDetailConsideration.Text = Convert.ToDecimal(Convert.ToDecimal(txtLoanDetailPrice.Text) * Convert.ToDecimal(txtLoanDetailNotional.Text)).ToString("0.00");
            else
                txtLoanDetailConsideration.Text = Convert.ToDecimal(1 * Convert.ToDecimal(txtLoanDetailNotional.Text)).ToString("0.00");


            txtLoanDetailIRR.Text = CalculateIRR(ddlLoanDetailsCode.SelectedItem.Text, txtLoanDetailPrice.Text);
        }

        private void BindLoanDetailData(string loanCode)
        {
            LoanScheduleBL bll = new LoanScheduleBL();
            LoansBLL loanBL = new LoansBLL();
            if (loanCode != string.Empty)
            {
                Loans loan = loanBL.GetLoanByID(Convert.ToInt32(loanCode));
                txtLoanDetailCurrency.Text = loan.Currency;
                txtLoanDetailMaturityDate.Text = loan.Maturity_Date;
                txtLoanDetailTradeDate.SelectedDate = DateTime.Now;//loan.Signing_Date;
                txtLoanDetailSettlementDate.SelectedDate = AddBusinessDays(DateTime.Now.Date, 10);
                txtLoanDetailNotional.Text = Convert.ToDecimal(loan.Notional).ToString("N");
                hdnLoanDetailMargin.Value = loan.Margin.ToString();
                txtLoanDetailCouponFreq.Text = loan.CouponFrequency;
                txtLoanDetailLastCouponDate.Text = loan.CouponDate;
                txtLoanDetailFixedOrFloating.Text = loan.FixedOrFloating;
                txtLoanDetailIRR.Text = CalculateIRR(loan.CodeName, txtLoanDetailPrice.Text);
                txtLoanDetailAvgLife.Text = AverageLife(loan.CodeName);
                txtAvgLifeNonDisc.Text = AverageLifeNonDiscount(loan.CodeName);
                txtLoanDetailAvgLifeRiskDisc.Text = AverageLifeRiskDisc(loan.CodeName);
                txtLoanDetailAvgLifeDisc.Text = AverageLifeDisc(loan.CodeName);
                if (txtLoanDetailPrice.Text != string.Empty)
                    txtLoanDetailConsideration.Text = Convert.ToDecimal(Convert.ToDecimal(txtLoanDetailPrice.Text) * Convert.ToDecimal(loan.Notional)).ToString("0.00");
                else
                    txtLoanDetailConsideration.Text = Convert.ToDecimal(1 * Convert.ToDecimal(loan.Notional)).ToString("0.00");
                //txtLoanDetailDiscountMargin.Text = loan.di
                grdLoanDetail.DataSource = bll.GetLoanByID(Convert.ToInt16(loanCode));
                grdLoanDetail.DataBind();
            }

        }

        protected void grdLoanDetail_ItemDataBound(object sender, GridItemEventArgs e)
        {

        }

        protected void grdLoanDetail_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            // grdLoanDetail.DataSource = PopulateLoanDetailGrid;
        }

        private void BindLoanDetails()
        {
            LoansBLL loanBL = new LoansBLL();
            ddlLoanDetailsCode.DataSource = loanBL.BindLoanAData();
            ddlLoanDetailsCode.DataTextField = "CodeName";
            ddlLoanDetailsCode.DataValueField = "ID";
            ddlLoanDetailsCode.DataBind();
        }

        public List<LoanSchedule> BindLoanScheduleData()
        {
            LoanScheduleBL bll = new LoanScheduleBL();
            if (ddlLoanDetailsCode.SelectedValue == string.Empty)
            {
                BindLoanDetails();
            }
            List<LoanSchedule> lst = bll.GetLoanByID(Convert.ToInt16(ddlLoanDetailsCode.SelectedValue));
            return lst;
        }
        public List<LoanSchedule> PopulateLoanDetailGrid
        {
            get
            {
                List<LoanSchedule> data;
                if (Session["LoanDetailGridData"] == null)
                {
                    data = BindLoanScheduleData();
                    Session["LoanDetailGridData"] = data;
                }
                data = (List<LoanSchedule>)Session["LoanDetailGridData"];
                return data;
            }
            set
            {
                Session["LoanDetailGridData"] = value;
            }
        }

        public List<QuotesAndTrades> BindQuotesAndTradesData()
        {
            QuotesAndTradesBLL bll = new QuotesAndTradesBLL();
            List<QuotesAndTrades> lst = bll.GetQuotesAndTrades();
            return lst;
        }
        public List<QuotesAndTrades> PopulateQuotes
        {
            get
            {
                List<QuotesAndTrades> data;
                if (Session["PopulateQuotes"] == null)
                {
                    data = BindQuotesAndTradesData();
                    Session["PopulateQuotes"] = data;
                }
                data = (List<QuotesAndTrades>)Session["PopulateQuotes"];
                return data;
            }
            set
            {
                Session["PopulateQuotes"] = value;
            }
        }

        protected void grdQuotesAndTrades_FilterCheckListItemsRequested1(object sender, GridFilterCheckListItemsRequestedEventArgs e)
        {

        }

        #region Calculate IRR
        private string CalculateIRR(string loanName, string marketPrice)
        {
            LoansBLL bll = new LoansBLL();
            Loans loan = bll.GetLoanByCode(loanName);
            int loanID = loan.ID;
            double notional = Convert.ToDouble(loan.Notional);
            LoanScheduleBL loanScheduleBL = new LoanScheduleBL();
            List<LoanSchedule> loanSchedule = new List<LoanSchedule>();
            loanSchedule = loanScheduleBL.GetLoanByID(loanID);
            double[] cashFlows;
            double[] days;
            List<DateTime> scheduleDateTime = new List<DateTime>();
            if (loanSchedule != null)
            {
                List<double> cashFlowDouble = new List<double>();
                double mktPrice = 0.99;
                if (marketPrice != string.Empty)
                {
                    mktPrice = Convert.ToDouble(marketPrice) / 100;
                }

                double dFirstCashFlow = (Convert.ToDouble(mktPrice) * Convert.ToDouble(notional)) * -1;
                scheduleDateTime.Add(loanSchedule.First().StartDate.Value);
                cashFlowDouble.Add(dFirstCashFlow);

                List<double> day = new List<double>();
                foreach (var item in loanSchedule)
                {
                    cashFlowDouble.Add(Convert.ToDouble(item.AmortisationInt));
                    day.Add(Convert.ToInt16(item.Days));
                    scheduleDateTime.Add(Convert.ToDateTime(item.EndDate));
                }
                cashFlows = cashFlowDouble.ToArray();

                days = day.ToArray();
            }
            else
            {
                cashFlows = new double[1] { 0.0 };
                days = new double[1] { 0.0 };

            }

            //Zainco.NewtonRaphson.IRRCalculator.Domain.ICalculator calculator = new NewtonRaphsonIRRCalc(cashFlows);
            //Double d = calculator.Execute();
            IRR irr = new IRR();

            //double xirr = irr.Newtons_method(0.1,
            //                           irr.total_f_xirr(cashFlows, days),
            //                           irr.total_df_xirr(cashFlows, days));

            double xirr = System.Numeric.Financial.XIrr(cashFlows, scheduleDateTime) * 100;
            return Convert.ToDecimal(xirr).ToString("0.00");
            // return d;


        }
        #endregion

        #region Average Life

        private string AverageLife(string loanName)
        {
            int loanID = GetLoanByName(loanName).ID;
            List<LoanSchedule> loanSchedule = GetLoanBySchedule(loanID);
            double[] CouponFrec;
            double[] Amortization;
            List<double> amortisation = new List<double>();
            List<double> couponFrac = new List<double>();
            decimal totalAmortisation = 0;
            foreach (var item in loanSchedule)
            {
                amortisation.Add(Convert.ToDouble(item.Amortisation));
                couponFrac.Add(Convert.ToInt16(item.CoupFrac));
                totalAmortisation += item.Amortisation.Value;
            }
            return Convert.ToDecimal(SumProduct(couponFrac.ToArray(), amortisation.ToArray()) / totalAmortisation).ToString("0.00");
        }

        private string AverageLifeRiskDisc(string loanName)
        {
            int loanID = GetLoanByName(loanName).ID;
            List<LoanSchedule> loanSchedule = GetLoanBySchedule(loanID);
            double[] CouponFrec;
            double[] Amortization;
            double[] RiskFree1;
            double[] RiskFree2;
            List<double> amortisation = new List<double>();
            List<double> couponFrac = new List<double>();
            List<double> riskFree1 = new List<double>();
            List<double> riskFree2 = new List<double>();
            decimal totalAmortisation = 0;
            foreach (var item in loanSchedule)
            {
                amortisation.Add(Convert.ToDouble(item.Amortisation));
                couponFrac.Add(Convert.ToDouble(item.CoupFrac));
                riskFree1.Add(Convert.ToDouble(item.RiskFreeDP1));
                riskFree2.Add(Convert.ToDouble(item.RiskFreeDP2));
                totalAmortisation += item.Amortisation.Value;
            }
            return Convert.ToDecimal(SumProduct(couponFrac.ToArray(), amortisation.ToArray(), riskFree1.ToArray(), riskFree2.ToArray()) / totalAmortisation).ToString("0.00");
        }

        private string AverageLifeNonDiscount(string loanName)
        {
            int loanID = GetLoanByName(loanName).ID;
            List<LoanSchedule> loanSchedule = GetLoanBySchedule(loanID);
            double[] CouponFrec;
            double[] Amortization;
            List<double> amortisation = new List<double>();
            List<double> couponFrac = new List<double>();
            decimal totalAmortisation = 0;
            foreach (var item in loanSchedule)
            {
                amortisation.Add(Convert.ToDouble(item.Amortisation));
                couponFrac.Add(Convert.ToInt16(item.CoupFrac));
                totalAmortisation += item.Amortisation.Value;
            }
            return Convert.ToDecimal(SumProduct(couponFrac.ToArray(), amortisation.ToArray()) / totalAmortisation).ToString("0.00");
        }

        private string AverageLifeDisc(string loanName)
        {
            int loanID = GetLoanByName(loanName).ID;
            List<LoanSchedule> loanSchedule = GetLoanBySchedule(loanID);
            double[] CouponFrec;
            double[] Amortization;
            double[] RiskFree1;

            List<double> amortisation = new List<double>();
            List<double> couponFrac = new List<double>();
            List<double> riskFree1 = new List<double>();

            decimal totalAmortisation = 0;
            foreach (var item in loanSchedule)
            {
                amortisation.Add(Convert.ToDouble(item.Amortisation));
                couponFrac.Add(Convert.ToDouble(item.CoupFrac));
                riskFree1.Add(Convert.ToDouble(item.RiskFreeDP1));

                totalAmortisation += item.Amortisation.Value;
            }
            return Convert.ToDecimal(SumProduct(couponFrac.ToArray(), amortisation.ToArray(), riskFree1.ToArray()) / totalAmortisation).ToString("0.00");
        }
        private Loans GetLoanByName(string loanName)
        {
            LoansBLL bll = new LoansBLL();
            Loans loan = bll.GetLoanByCode(loanName);
            return loan;
        }

        private List<LoanSchedule> GetLoanBySchedule(int loanID)
        {
            LoanScheduleBL loanScheduleBL = new LoanScheduleBL();
            return loanScheduleBL.GetLoanByID(loanID);
        }
        #endregion

        protected void ddlBorrower_SelectedIndexChanged(object sender, RadComboBoxSelectedIndexChangedEventArgs e)
        {
            BorrowersBL borrowerBL = new BorrowersBL();
            Borrower borrower = borrowerBL.GetBorrower(ddlBorrower.SelectedItem.Text);
            txtGrid.Text = borrower.Grid;
            txtSummitCredit.Text = borrower.SummitCreditEntity;
            txtGrid.Enabled = false;
            txtSummitCredit.Enabled = false;

        }

        public Decimal SumProduct(double[] digits1, double[] digits2, double[] digits3, double[] digits4)
        {
            Decimal sum = 0;
            for (int i = 0; i < digits1.Length && i < digits2.Length; i++)
            {
                sum += Convert.ToDecimal(digits1[i]) * Convert.ToDecimal(digits2[i]) * Convert.ToDecimal(digits3[i]) * Convert.ToDecimal(digits4[i]);
            }
            return sum;
        }

        public Decimal SumProduct(double[] digits1, double[] digits2)
        {
            Decimal sum = 0;
            for (int i = 0; i < digits1.Length && i < digits2.Length; i++)
            {
                sum += Convert.ToDecimal(digits1[i]) * Convert.ToDecimal(digits2[i]);
            }
            return sum;
        }

        public Decimal SumProduct(double[] digits1, double[] digits2, double[] digits3)
        {
            Decimal sum = 0;
            for (int i = 0; i < digits1.Length && i < digits2.Length; i++)
            {
                sum += Convert.ToDecimal(digits1[i]) * Convert.ToDecimal(digits2[i]) * Convert.ToDecimal(digits3[i]);
            }
            return sum;
        }

        protected void txtBoxNotional3_TextChanged(object sender, EventArgs e)
        {
            txtBoxNotional3.Text = Convert.ToDecimal(txtBoxNotional3.Text).ToString("N");
        }

        protected void txtBoxNotional2_TextChanged(object sender, EventArgs e)
        {
            txtBoxNotional2.Text = Convert.ToDecimal(txtBoxNotional2.Text).ToString("N");
        }

        protected void txtBoxNotional1_TextChanged(object sender, EventArgs e)
        {
            txtBoxNotional1.Text = Convert.ToDecimal(txtBoxNotional1.Text).ToString("N");
        }

        protected void txtLoanDetailPrice_TextChanged(object sender, EventArgs e)
        {
            Session.Add("LoanDetail", ddlLoanDetailsCode.SelectedValue);
            BindLoanDetailData();
        }

        protected void txtLoanDetailSpread_TextChanged(object sender, EventArgs e)
        {
            LoanScheduleBL loanScheduleBL = new LoanScheduleBL();
            loanScheduleBL.UpdateSpread(Convert.ToInt16(ddlLoanDetailsCode.SelectedValue), Convert.ToDecimal(txtLoanDetailSpread.Text));
            Session.Add("LoanDetail", ddlLoanDetailsCode.SelectedValue);
            BindLoanDetailData(ddlLoanDetailsCode.SelectedValue);
        }

        protected void txtLoanDetailTradeDate_TextChanged(object sender, EventArgs e)
        {

        }

        protected void txtLoanDetailTradeDate_SelectedDateChanged(object sender, Telerik.Web.UI.Calendar.SelectedDateChangedEventArgs e)
        {
            AddBusinessDays(Convert.ToDateTime(txtLoanDetailTradeDate.SelectedDate), 10);
        }
    }
}


public class SessionStorageProvider : IStateStorageProvider
{
    private System.Web.SessionState.HttpSessionState session = HttpContext.Current.Session;
    static string storageKey;

    public static string StorageProviderKey
    {
        set { storageKey = value; }
    }

    public void SaveStateToStorage(string key, string serializedState)
    {
        session[storageKey] = serializedState;
    }

    public string LoadStateFromStorage(string key)
    {
        return session[storageKey].ToString();
    }
}

