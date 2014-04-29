using BLL;
using DAL;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace LoanPricerWebBased.Helpers
{
    public class ReportHelper
    {
        public static string GenerateAndSendPDFReport(string group)
        {
            string path = GenerateReport();

            // Send email
            return EmailHelper.SendEmailToGroup("Hi,<br/>Please find attached the daily activity report from LAPS.<br/>Best Regards,<br/>LAPS Operations", "Loans Analysis and Pricing System : Daily Activity Report (" + DateTime.Now.ToShortDateString() + ")", group, path);
        }

        public static void GenerateAndSendDailyPDFReport()
        {
            System.IO.File.WriteAllText(@"C:\Users\Public\TestFolder\WriteText.txt", " 1 " + "In GenerateAndSendDailyPDFReport" + DateTime.Now.ToShortDateString());
            string path = GenerateReport();
            System.IO.File.WriteAllText(@"C:\Users\Public\TestFolder\WriteText.txt", " 2 " + "In GenerateAndSendDailyPDFReport" + DateTime.Now.ToShortDateString());
            // Send email
            EmailHelper.SendEmailToSelectedGroup("Hi,<br/>Please find attached the daily activity report from LAPS.<br/>Best Regards,<br/>LAPS Operations", "Loans Analysis and Pricing System : Daily Activity Report (" + DateTime.Now.ToShortDateString() + ")", path);
        }
       
        private static string GenerateReport()
        {
            string path = ConfigurationManager.AppSettings["ReportPathOnServer"] + "Daily Activity" + DateTime.Now.ToShortDateString().Replace("/", "_") + ".pdf";
            System.IO.File.WriteAllText(@"C:\Users\Public\TestFolder\WriteText.txt", " 1 " + path);
            Document doc = new Document(iTextSharp.text.PageSize.A4);

            System.IO.FileStream file = new System.IO.FileStream(path, System.IO.FileMode.OpenOrCreate);
            System.IO.File.WriteAllText(@"C:\Users\Public\TestFolder\WriteText.txt"," 1 " +  file.ToString());
            PdfWriter writer = PdfWriter.GetInstance(doc, file);
           
            doc.Open();
            PdfPTable tab = new PdfPTable(5);

            PdfPCell cell = new PdfPCell(new Phrase("Header"));
            cell = ImageCell("~/images/logo.png", 30f, PdfPCell.ALIGN_LEFT);
            cell.Colspan = 5;
            cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
            //Style
            cell.BorderColor = new BaseColor(System.Drawing.Color.Black);
            cell.Border = Rectangle.BOTTOM_BORDER; // | Rectangle.TOP_BORDER;
            cell.BorderWidthBottom = 1f;
            cell.PaddingBottom = 30f;

            tab.AddCell(cell);

            cell = new PdfPCell(new Phrase("DAILY ACTIVITY REPORT(" + DateTime.Now.ToShortDateString() + ")",
                                 new Font(Font.FontFamily.COURIER, 12F,1,BaseColor.RED)));
            
            cell.Colspan = 5;
            
            cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
            //Style
           // cell.BorderColor = new BaseColor(System.Drawing.Color.Black);
            cell.Border = Rectangle.BOTTOM_BORDER; // | Rectangle.TOP_BORDER;
            cell.BorderWidthBottom = 1f;
            cell.PaddingTop = 18f;
            cell.PaddingBottom = 18f;

            tab.AddCell(cell);


            //add header cells here
            tab.AddCell("Date & Time");
            tab.AddCell("Username");
            //  tab.AddCell("Activity");
            tab.AddCell("Message");
            tab.AddCell("IP");
            tab.AddCell("URL");
            
            PdfPCell cellBottom = new PdfPCell();
            cellBottom.Colspan = 5;
            cellBottom.HorizontalAlignment = 1;
            //Style
            cellBottom.BorderColor = new BaseColor(System.Drawing.Color.Black);
            cellBottom.Border = Rectangle.BOTTOM_BORDER;
            cellBottom.BorderWidthBottom = 1f;
            cellBottom.PaddingBottom = 5f;
            

            tab.AddCell(cellBottom);

            // put up the data now
            List<tblActivityLog> lst = new ActivityLogBL().GetTodayLogs();
            foreach (var item in lst)
            {
                tab.AddCell(item.ActivityDate.ToShortDateString());
                tab.AddCell(item.UserName);
               // tab.AddCell(item.Activity);
                tab.AddCell(item.Message);
                tab.AddCell(item.IP);
                tab.AddCell(item.URL);
            }

            doc.Add(tab);
            doc.Close();
            file.Close();
            return path;
        }
        private static PdfPCell ImageCell(string path, float scale, int align)
        {
           
            try
            {
                iTextSharp.text.Image image = iTextSharp.text.Image.GetInstance(HttpContext.Current.Server.MapPath(path));
                image.ScalePercent(scale);
                PdfPCell cell = new PdfPCell(image);
                //cell.BorderColor = Color.WHITE;
                //cell.VerticalAlignment = PdfCell.ALIGN_TOP;
                cell.HorizontalAlignment = align;
                cell.PaddingBottom = 0f;
                cell.PaddingTop = 0f;
                return cell;
            }
            catch (Exception)
            {
                PdfPCell cell = new PdfPCell();
                return cell; 
            }
        }
    }
}