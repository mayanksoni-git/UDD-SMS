using CrystalDecisions.CrystalReports.Engine;
using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Web;
using System.Web.UI;

public partial class BillPaymentOrderView : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Payment_Order"] != null)
        {
            List<tbl_Report_Payment_Order> obj_tbl_Report_Payment_Order_Li = (List<tbl_Report_Payment_Order>)Session["ProjectSummery"];
            ReportDocument crystalReport = new ReportDocument();
            crystalReport.Load(Server.MapPath("~/Crystal/Payment_Order.rpt"));
            crystalReport.SetDataSource(obj_tbl_Report_Payment_Order_Li);
            crvBillView.ReportSource = crystalReport;
            crvBillView.RefreshReport();
        }
    }

    private string GetDefaultPrinter()
    {
        PrinterSettings settings = new PrinterSettings();
        foreach (string printer in PrinterSettings.InstalledPrinters)
        {
            settings.PrinterName = printer;
            if (settings.IsDefaultPrinter)
            {
                return printer;
            }
        }
        return string.Empty;
    }
}