using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Xml.Linq;
using DAL;
using System.Web.Script.Services;
using System.Collections.Generic;
using Telerik.Web.UI;
using BLL;

namespace LoanPricerWebBased
{
    /// <summary>
    /// Summary description for LoanService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]

    [ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class LoanService : System.Web.Services.WebService
    {

        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }

        //[WebMethod]
        //public string[] GetCompletionList(string prefixText)
        //{
        //    prefixText = prefixText.ToLower();
        //    LoanPriceEntities db = new LoanPriceEntities();

        //    var osoba = from o in db.Loans
        //                orderby o.CodeName
        //                select o;

        //    string[] main = new string[0];

        //    foreach (var o in osoba)
        //    {
        //        if (!string.IsNullOrEmpty(o.CodeName) && o.CodeName.ToLower().Contains(prefixText))
        //        {
        //            Array.Resize(ref main, main.Length + 1);
        //            main[main.Length - 1] = o.CodeName;

        //            if (main.Length == 15)
        //            {
        //                break;
        //            }
        //        }
        //    }

        //    Array.Sort(main);
        //    return main;
        //}

        //[WebMethod]
        //public AutoCompleteBoxData GetCompletionList(RadAutoCompleteContext context)
        //{
        //    string clientData = context["ClientData"].ToString();
        //    LoanPriceEntities db = new LoanPriceEntities();
        //    var osoba = from o in db.Loans
        //                orderby o.CodeName
        //                select o;
        //    string[] main = new string[0];
        //    List<AutoCompleteBoxItemData> result = new List<AutoCompleteBoxItemData>();
        //    AutoCompleteBoxData dropDownData = new AutoCompleteBoxData();

        //    result = new List<AutoCompleteBoxItemData>();

        //    foreach (var o in osoba)
        //    {
        //        if (!string.IsNullOrEmpty(o.CodeName) && o.CodeName.Contains(clientData))
        //        {
        //            AutoCompleteBoxItemData itemData = new AutoCompleteBoxItemData();
        //            itemData.Text = o.CodeName.ToString();
        //            itemData.Value = o.CodeName.ToString();

        //            result.Add(itemData);
        //        }
        //    }
        //    dropDownData.Items = result.ToArray();
        //    return dropDownData;
        //}


        //[WebMethod]
        //[ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        //public string CheckForValid(string loanName, string counterParty)
        //{
        //    using (LoanPriceEntities context = new LoanPriceEntities())
        //    {
        //        List<QuotesAndTrades> quoteList = context.QuotesAndTrades.Where(s => s.LoanName == loanName && s.CounterParty == counterParty).ToList();
        //        if (quoteList != null && quoteList.Count > 0)
        //            return "true";
        //        else
        //            return "false";

        //    }
        //}

        [WebMethod]
        public RadListBoxItemData[] GetAmortization()
        {
            using (LoanPriceEntities context = new LoanPriceEntities())
            {
                var result = context.Loans.Select(s => s.NoOfAmortisationPoint).Distinct().ToList();
                List<RadListBoxItemData> res = new List<RadListBoxItemData>();
                foreach (var item in result)
                {
                    if (item != null)
                    {
                        RadListBoxItemData data = new RadListBoxItemData();
                        data.Text = item.ToString();
                        data.Value = item.ToString();
                        res.Add(data);
                    }
                }
                return res.ToArray<RadListBoxItemData>();
            }
        }

        [WebMethod]
        public RadListBoxItemData[] GetLoanNames()
        {
            using (LoanPriceEntities context = new LoanPriceEntities())
            {
                var result = context.QuotesAndTrades.Select(s => s.LoanName).Distinct().ToList();
                List<RadListBoxItemData> res = new List<RadListBoxItemData>();
                foreach (var item in result)
                {
                    if (item != null)
                    {
                        RadListBoxItemData data = new RadListBoxItemData();
                        data.Text = item.ToString();
                        data.Value = item.ToString();
                        res.Add(data);
                    }
                }
                return res.ToArray<RadListBoxItemData>();
            }
        }
    }
}
