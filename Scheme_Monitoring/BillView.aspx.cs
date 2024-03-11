//using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.CrystalReports.Engine;
using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Web;
using System.Web.UI;

public partial class BillView : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Invoice_View"] != null)
        {
            Invoice_View obj_Invoice_View = (Invoice_View)Session["Invoice_View"];
            List<Bill_Info> obj_Bill_Info_Li = obj_Invoice_View.obj_Bill_Info_Li;
            List<PackageInvoiceAdditional> obj_PackageInvoiceAdditional_Li = obj_Invoice_View.obj_PackageInvoiceAdditional_Li;
            ReportDocument crystalReport = new ReportDocument();
            if (obj_Bill_Info_Li[0].ProcessType == "Global" && obj_Bill_Info_Li[0].Bill_Type == "N")
            {
                crystalReport.Load(Server.MapPath("~/Crystal/Invoice_Print_N2.rpt"));
            }
            else if (obj_Bill_Info_Li[0].ProcessType == "Global" && obj_Bill_Info_Li[0].Bill_Type != "N")
            {
                crystalReport.Load(Server.MapPath("~/Crystal/Invoice_Print_2.rpt"));
            }
            else if (obj_Bill_Info_Li[0].ProcessType != "Global" && obj_Bill_Info_Li[0].Bill_Type == "N")
            {
                crystalReport.Load(Server.MapPath("~/Crystal/Invoice_Print_N.rpt"));
            }
            else
            {
                crystalReport.Load(Server.MapPath("~/Crystal/Invoice_Print.rpt"));
            }
            crystalReport.SetDataSource(obj_Bill_Info_Li);
            crystalReport.Subreports[0].SetDataSource(obj_PackageInvoiceAdditional_Li);
            Session["rptDoc"] = crystalReport;
            crvBillView.EnableDrillDown = false;
            crvBillView.ReportSource = crystalReport;
            crvBillView.RefreshReport();
        }
    }
    protected void Page_UnLoad(object sender, EventArgs e)
    {
        if (Session["rptDoc"] != null)
        {
            ReportDocument crystalReport = (ReportDocument)Session["rptDoc"];
            if (crystalReport != null)
            {
                crystalReport.Close();
                crystalReport.Dispose();
            }
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