using Aspose.Pdf;
using CrystalDecisions.CrystalReports.Engine;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.Data;
//using System.Device.Location;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;

public partial class Report_Generate_Final_Docs : System.Web.UI.Page
{
    List<Generate_Final_Doc> obj_Generate_Final_Doc_Li = new List<Generate_Final_Doc>();
    protected void Page_PreInit(object sender, EventArgs e)
    {
        this.MasterPageFile = SetMasterPage.ReturnPage();
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Person_Id"] == null || Session["Login_Id"] == null)
        {
            Response.Redirect("Index.aspx");
        }
        if (!IsPostBack)
        {
            obj_Generate_Final_Doc_Li = new List<Generate_Final_Doc>();
            Session["Generate_Final_Doc"] = obj_Generate_Final_Doc_Li;
        }
    }

    private void render_Download()
    {
        obj_Generate_Final_Doc_Li = (List<Generate_Final_Doc>)Session["Generate_Final_Doc"];
        for (int i = 0; i < obj_Generate_Final_Doc_Li.Count; i++)
        {
            if (obj_Generate_Final_Doc_Li[i].Report_Name == "Traget")
            {
                if (obj_Generate_Final_Doc_Li[i].Report_Summery_PDF_File != "")
                {
                    aTragetPDF.InnerHtml = "<a href='" + obj_Generate_Final_Doc_Li[i].Report_Summery_PDF_File + "' target='_blank'>Download PDF</a>";
                }
                if (obj_Generate_Final_Doc_Li[i].Report_Summery_PPT_File != "")
                {
                    aTragetPPT.InnerHtml = "<a href='" + obj_Generate_Final_Doc_Li[i].Report_Summery_PPT_File + "' target='_blank'>Download PPT</a>";
                }
            }

            else if (obj_Generate_Final_Doc_Li[i].Report_Name == "Physical")
            {
                if (obj_Generate_Final_Doc_Li[i].Report_Summery_PDF_File != "")
                {
                    aPhysicalSPDF.InnerHtml = "<a href='" + obj_Generate_Final_Doc_Li[i].Report_Summery_PDF_File + "' target='_blank'>Summery PDF</a>";
                }
                if (obj_Generate_Final_Doc_Li[i].Report_Summery_PPT_File != "")
                {
                    aPhysicalSPPT.InnerHtml = "<a href='" + obj_Generate_Final_Doc_Li[i].Report_Summery_PPT_File + "' target='_blank'>Summery PPT</a>";
                }
                if (obj_Generate_Final_Doc_Li[i].Report_Details_PDF_File != "")
                {
                    aPhysicalDPDF.InnerHtml = "<a href='" + obj_Generate_Final_Doc_Li[i].Report_Details_PDF_File + "' target='_blank'>Detail PDF</a>";
                }
                if (obj_Generate_Final_Doc_Li[i].Report_Details_PPT_File != "")
                {
                    aPhysicalDPPT.InnerHtml = "<a href='" + obj_Generate_Final_Doc_Li[i].Report_Details_PPT_File + "' target='_blank'>Detail PPT</a>";
                }
            }

            else if (obj_Generate_Final_Doc_Li[i].Report_Name == "Financial")
            {
                if (obj_Generate_Final_Doc_Li[i].Report_Summery_PDF_File != "")
                {
                    aFinancialSPDF.InnerHtml = "<a href='" + obj_Generate_Final_Doc_Li[i].Report_Summery_PDF_File + "' target='_blank'>Summery PDF</a>";
                }
                if (obj_Generate_Final_Doc_Li[i].Report_Summery_PPT_File != "")
                {
                    aFinancialSPPT.InnerHtml = "<a href='" + obj_Generate_Final_Doc_Li[i].Report_Summery_PPT_File + "' target='_blank'>Summery PPT</a>";
                }
                if (obj_Generate_Final_Doc_Li[i].Report_Details_PDF_File != "")
                {
                    aFinancialDPDF.InnerHtml = "<a href='" + obj_Generate_Final_Doc_Li[i].Report_Details_PDF_File + "' target='_blank'>Detail PDF</a>";
                }
                if (obj_Generate_Final_Doc_Li[i].Report_Details_PPT_File != "")
                {
                    aFinancialDPPT.InnerHtml = "<a href='" + obj_Generate_Final_Doc_Li[i].Report_Details_PPT_File + "' target='_blank'>Detail PPT</a>";
                }
            }

            else if (obj_Generate_Final_Doc_Li[i].Report_Name == "Document")
            {
                if (obj_Generate_Final_Doc_Li[i].Report_Summery_PDF_File != "")
                {
                    aDocumentSPDF.InnerHtml = "<a href='" + obj_Generate_Final_Doc_Li[i].Report_Summery_PDF_File + "' target='_blank'>Summery PDF</a>";
                }
                if (obj_Generate_Final_Doc_Li[i].Report_Summery_PPT_File != "")
                {
                    aDocumentSPPT.InnerHtml = "<a href='" + obj_Generate_Final_Doc_Li[i].Report_Summery_PPT_File + "' target='_blank'>Summery PPT</a>";
                }
                if (obj_Generate_Final_Doc_Li[i].Report_Details_PDF_File != "")
                {
                    aDocumentDPDF.InnerHtml = "<a href='" + obj_Generate_Final_Doc_Li[i].Report_Details_PDF_File + "' target='_blank'>Detail PDF</a>";
                }
                if (obj_Generate_Final_Doc_Li[i].Report_Details_PPT_File != "")
                {
                    aDocumentDPPT.InnerHtml = "<a href='" + obj_Generate_Final_Doc_Li[i].Report_Details_PPT_File + "' target='_blank'>Detail PPT</a>";
                }
            }

            else if (obj_Generate_Final_Doc_Li[i].Report_Name == "OverRun")
            {
                if (obj_Generate_Final_Doc_Li[i].Report_Summery_PDF_File != "")
                {
                    aOverRunSPDF.InnerHtml = "<a href='" + obj_Generate_Final_Doc_Li[i].Report_Summery_PDF_File + "' target='_blank'>Summery PDF</a>";
                }
                if (obj_Generate_Final_Doc_Li[i].Report_Summery_PPT_File != "")
                {
                    aOverRunSPPT.InnerHtml = "<a href='" + obj_Generate_Final_Doc_Li[i].Report_Summery_PPT_File + "' target='_blank'>Summery PPT</a>";
                }
                if (obj_Generate_Final_Doc_Li[i].Report_Details_PDF_File != "")
                {
                    aOverRunDPDF.InnerHtml = "<a href='" + obj_Generate_Final_Doc_Li[i].Report_Details_PDF_File + "' target='_blank'>Detail PDF</a>";
                }
                if (obj_Generate_Final_Doc_Li[i].Report_Details_PPT_File != "")
                {
                    aOverRunDPPT.InnerHtml = "<a href='" + obj_Generate_Final_Doc_Li[i].Report_Details_PPT_File + "' target='_blank'>Detail PPT</a>";
                }
            }

            else if (obj_Generate_Final_Doc_Li[i].Report_Name == "OverRun1")
            {
                if (obj_Generate_Final_Doc_Li[i].Report_Summery_PDF_File != "")
                {
                    aOverRunSPDF1.InnerHtml = "<a href='" + obj_Generate_Final_Doc_Li[i].Report_Summery_PDF_File + "' target='_blank'>Summery PDF</a>";
                }
                if (obj_Generate_Final_Doc_Li[i].Report_Summery_PPT_File != "")
                {
                    aOverRunSPPT1.InnerHtml = "<a href='" + obj_Generate_Final_Doc_Li[i].Report_Summery_PPT_File + "' target='_blank'>Summery PPT</a>";
                }
                if (obj_Generate_Final_Doc_Li[i].Report_Details_PDF_File != "")
                {
                    aOverRunDPDF1.InnerHtml = "<a href='" + obj_Generate_Final_Doc_Li[i].Report_Details_PDF_File + "' target='_blank'>Detail PDF</a>";
                }
                if (obj_Generate_Final_Doc_Li[i].Report_Details_PPT_File != "")
                {
                    aOverRunDPPT1.InnerHtml = "<a href='" + obj_Generate_Final_Doc_Li[i].Report_Details_PPT_File + "' target='_blank'>Detail PPT</a>";
                }
            }

            else if (obj_Generate_Final_Doc_Li[i].Report_Name == "SNANotAvailable")
            {
                if (obj_Generate_Final_Doc_Li[i].Report_Summery_PDF_File != "")
                {
                    aSNANotAvailableSPDF.InnerHtml = "<a href='" + obj_Generate_Final_Doc_Li[i].Report_Summery_PDF_File + "' target='_blank'>Summery PDF</a>";
                }
                if (obj_Generate_Final_Doc_Li[i].Report_Summery_PPT_File != "")
                {
                    aSNANotAvailableSPPT.InnerHtml = "<a href='" + obj_Generate_Final_Doc_Li[i].Report_Summery_PPT_File + "' target='_blank'>Summery PPT</a>";
                }
                if (obj_Generate_Final_Doc_Li[i].Report_Details_PDF_File != "")
                {
                    aSNANotAvailableDPDF.InnerHtml = "<a href='" + obj_Generate_Final_Doc_Li[i].Report_Details_PDF_File + "' target='_blank'>Detail PDF</a>";
                }
                if (obj_Generate_Final_Doc_Li[i].Report_Details_PPT_File != "")
                {
                    aSNANotAvailableDPPT.InnerHtml = "<a href='" + obj_Generate_Final_Doc_Li[i].Report_Details_PPT_File + "' target='_blank'>Detail PPT</a>";
                }
            }

            else if (obj_Generate_Final_Doc_Li[i].Report_Name == "SNAAvailable")
            {
                if (obj_Generate_Final_Doc_Li[i].Report_Summery_PDF_File != "")
                {
                    aSNAAvailableSPDF.InnerHtml = "<a href='" + obj_Generate_Final_Doc_Li[i].Report_Summery_PDF_File + "' target='_blank'>Summery PDF</a>";
                }
                if (obj_Generate_Final_Doc_Li[i].Report_Summery_PPT_File != "")
                {
                    aSNAAvailableSPPT.InnerHtml = "<a href='" + obj_Generate_Final_Doc_Li[i].Report_Summery_PPT_File + "' target='_blank'>Summery PPT</a>";
                }
                if (obj_Generate_Final_Doc_Li[i].Report_Details_PDF_File != "")
                {
                    aSNAAvailableDPDF.InnerHtml = "<a href='" + obj_Generate_Final_Doc_Li[i].Report_Details_PDF_File + "' target='_blank'>Detail PDF</a>";
                }
                if (obj_Generate_Final_Doc_Li[i].Report_Details_PPT_File != "")
                {
                    aSNAAvailableDPPT.InnerHtml = "<a href='" + obj_Generate_Final_Doc_Li[i].Report_Details_PPT_File + "' target='_blank'>Detail PPT</a>";
                }
            }

            if (obj_Generate_Final_Doc_Li[i].Report_Name == "Final")
            {
                if (obj_Generate_Final_Doc_Li[i].Report_Summery_PDF_File != "")
                {
                    aFinalPDF.InnerHtml = "<a href='" + obj_Generate_Final_Doc_Li[i].Report_Summery_PDF_File + "' target='_blank'>Download PDF</a>";
                }
                if (obj_Generate_Final_Doc_Li[i].Report_Summery_PPT_File != "")
                {
                    aFinalPPT.InnerHtml = "<a href='" + obj_Generate_Final_Doc_Li[i].Report_Summery_PPT_File + "' target='_blank'>Download PPT</a>";
                }
            }
        }
    }

    protected void btnExportTarget_Click(object sender, EventArgs e)
    {
        obj_Generate_Final_Doc_Li = (List<Generate_Final_Doc>)Session["Generate_Final_Doc"];
        int Month = DateTime.Now.Month - 1;
        int Year = DateTime.Now.Year;
        DataSet ds = new DataSet();
        ds = (new DataLayer()).get_Project_Work_Financial_Progress_Circle_Wise(1013, 0, 0, Month, Year);
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            string filePath = "\\Downloads\\" + Session["Person_Id"].ToString() + "\\";
            if (!Directory.Exists(Server.MapPath(".") + filePath))
            {
                Directory.CreateDirectory(Server.MapPath(".") + filePath);
            }

            string fileName = "A_Achivment_" + Month.ToString() + "_" + Year.ToString() + ".pdf";

            List<tbl_Achivment> obj_tbl_Achivment_Li = new List<tbl_Achivment>();
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                tbl_Achivment obj_tbl_Achivment = new tbl_Achivment();
                obj_tbl_Achivment.Achivment_Percentage = Convert.ToDecimal(ds.Tables[0].Rows[i]["Percentage_Val"].ToString());
                obj_tbl_Achivment.Achivment_Value = Convert.ToDecimal(ds.Tables[0].Rows[i]["Total_Achivment_In_Current_Month"].ToString());
                obj_tbl_Achivment.ADP_Value = Convert.ToDecimal(ds.Tables[0].Rows[i]["Other_Dept_Current_Month"].ToString());
                obj_tbl_Achivment.Circle_Id = Convert.ToInt32(ds.Tables[0].Rows[i]["Circle_Id"].ToString());
                obj_tbl_Achivment.Zone_Id = Convert.ToInt32(ds.Tables[0].Rows[i]["Zone_Id"].ToString());
                obj_tbl_Achivment.Circle_Name = ds.Tables[0].Rows[i]["Circle_Name"].ToString();
                obj_tbl_Achivment.Zone_Name = ds.Tables[0].Rows[i]["Zone_Name"].ToString();
                obj_tbl_Achivment.Zone_Circle_Name = ds.Tables[0].Rows[i]["Zone_Name"].ToString() + " - " + ds.Tables[0].Rows[i]["Circle_Name"].ToString();
                obj_tbl_Achivment.Deduction_Release_Value = Convert.ToDecimal(ds.Tables[0].Rows[i]["Deduction_Release_Current_Month"].ToString());
                obj_tbl_Achivment.EMB_Value = Convert.ToDecimal(ds.Tables[0].Rows[i]["EMB_Current_Month"].ToString());
                obj_tbl_Achivment.Invoice_Value = Convert.ToDecimal(ds.Tables[0].Rows[i]["Total_Invoice_Value_Current_Month"].ToString());
                obj_tbl_Achivment.MA_Value = Convert.ToDecimal(ds.Tables[0].Rows[i]["Moblization_Adv_Current_Month"].ToString());
                obj_tbl_Achivment.Target_Value = Convert.ToDecimal(ds.Tables[0].Rows[i]["CircleFinancialTarget_Value"].ToString());
                obj_tbl_Achivment.Header_Text = Session["Default_Circle"].ToString() + " Wise Target and Achievement Analysis (" + AllClasses.getmonth(Month) + " - " + Year.ToString() + "): AMRUT";
                obj_tbl_Achivment_Li.Add(obj_tbl_Achivment);
            }

            string webURI = "";
            if (Page.Request.Url.Query.Trim() == "")
            {
                webURI = (Page.Request.Url.AbsoluteUri.Replace(Page.Request.Url.AbsolutePath, "") + filePath + fileName).Replace("\\", "/");
            }
            else
            {
                webURI = (Page.Request.Url.AbsoluteUri.Replace(Page.Request.Url.AbsolutePath, "").Replace(Page.Request.Url.Query, "") + filePath + fileName).Replace("\\", "/");
            }

            ReportDocument crystalReport = new ReportDocument();
            crystalReport.Load(Server.MapPath("~/Crystal/pmis/Achievement.rpt"));
            crystalReport.SetDataSource(obj_tbl_Achivment_Li);
            crystalReport.Refresh();
            //crystalReport.ReportSource = crystalReport;
            //crystalReport.RefreshReport();
            crystalReport.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Server.MapPath(".") + filePath + fileName);

            FileInfo fi = new FileInfo(Server.MapPath(".") + filePath + fileName);
            if (fi.Exists)
            {
                // Load PDF document
                Document pdfDocument = new Document(fi.FullName);
                PptxSaveOptions pptxOptions = new PptxSaveOptions();
                // Save output file
                pdfDocument.Save(fi.FullName.Replace(".pdf", ".pptx"), pptxOptions);

                Generate_Final_Doc obj_Generate_Final_Doc = new Generate_Final_Doc();
                obj_Generate_Final_Doc.Report_Name = "Traget";
                obj_Generate_Final_Doc.Report_Summery_PDF_File = filePath + fileName;
                obj_Generate_Final_Doc.Report_Summery_PPT_File = filePath + fileName.Replace(".pdf", ".pptx");
                obj_Generate_Final_Doc_Li.Add(obj_Generate_Final_Doc);
                Session["Generate_Final_Doc"] = obj_Generate_Final_Doc_Li;
            }
        }
        render_Download();
    }

    protected void btnExportPhysical_Click(object sender, EventArgs e)
    {
        obj_Generate_Final_Doc_Li = (List<Generate_Final_Doc>)Session["Generate_Final_Doc"];
        Generate_Final_Doc obj_Generate_Final_Doc = new Generate_Final_Doc();
        obj_Generate_Final_Doc.Report_Name = "Physical";
        DataSet ds = new DataSet();
        ds = (new DataLayer()).get_Physical_Progress_No_DataChange("1013", 0, 0, 0, 0, 0, true, 60);

        if (ds != null && ds.Tables.Count > 1 && ds.Tables[1].Rows.Count > 0)
        {
            string filePath = "\\Downloads\\" + Session["Person_Id"].ToString() + "\\";
            if (!Directory.Exists(Server.MapPath(".") + filePath))
            {
                Directory.CreateDirectory(Server.MapPath(".") + filePath);
            }

            string fileName = "B_StagnantPhysical_Summery.pdf";

            List<tbl_Stagnant_Progress_Physical> obj_tbl_Stagnant_Progress_Physical_Li = new List<tbl_Stagnant_Progress_Physical>();
            for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
            {
                tbl_Stagnant_Progress_Physical obj_tbl_Stagnant_Progress_Physical = new tbl_Stagnant_Progress_Physical();
                obj_tbl_Stagnant_Progress_Physical.Data_1 = Convert.ToInt32(ds.Tables[1].Rows[i]["Data_1"].ToString());
                obj_tbl_Stagnant_Progress_Physical.Data_2 = Convert.ToInt32(ds.Tables[1].Rows[i]["Data_2"].ToString());
                obj_tbl_Stagnant_Progress_Physical.Data_3 = Convert.ToInt32(ds.Tables[1].Rows[i]["Data_3"].ToString());
                obj_tbl_Stagnant_Progress_Physical.Data_4 = Convert.ToInt32(ds.Tables[1].Rows[i]["Data_4"].ToString());
                obj_tbl_Stagnant_Progress_Physical.Data_5 = Convert.ToInt32(ds.Tables[1].Rows[i]["Data_5"].ToString());
                obj_tbl_Stagnant_Progress_Physical.Total = Convert.ToInt32(ds.Tables[1].Rows[i]["Total"].ToString());
                obj_tbl_Stagnant_Progress_Physical.Circle_Id = Convert.ToInt32(ds.Tables[1].Rows[i]["Circle_Id"].ToString());
                obj_tbl_Stagnant_Progress_Physical.Zone_Id = Convert.ToInt32(ds.Tables[1].Rows[i]["Zone_Id"].ToString());
                obj_tbl_Stagnant_Progress_Physical.Circle_Name = ds.Tables[1].Rows[i]["Circle_Name"].ToString();
                obj_tbl_Stagnant_Progress_Physical.Zone_Name = ds.Tables[1].Rows[i]["Zone_Name"].ToString();
                obj_tbl_Stagnant_Progress_Physical.Zone_Circle_Name = ds.Tables[1].Rows[i]["Zone_Name"].ToString() + " - " + ds.Tables[1].Rows[i]["Circle_Name"].ToString();
                obj_tbl_Stagnant_Progress_Physical.Header_Text = "Stagnant Physical Progress for more than 60 Days - : AMRUT";
                obj_tbl_Stagnant_Progress_Physical_Li.Add(obj_tbl_Stagnant_Progress_Physical);
            }

            string webURI = "";
            if (Page.Request.Url.Query.Trim() == "")
            {
                webURI = (Page.Request.Url.AbsoluteUri.Replace(Page.Request.Url.AbsolutePath, "") + filePath + fileName).Replace("\\", "/");
            }
            else
            {
                webURI = (Page.Request.Url.AbsoluteUri.Replace(Page.Request.Url.AbsolutePath, "").Replace(Page.Request.Url.Query, "") + filePath + fileName).Replace("\\", "/");
            }

            ReportDocument crystalReport = new ReportDocument();
            crystalReport.Load(Server.MapPath("~/Crystal/pmis/Stagnant_Phy_Progress_Summery.rpt"));
            crystalReport.SetDataSource(obj_tbl_Stagnant_Progress_Physical_Li);
            crystalReport.Refresh();
            //crystalReport.ReportSource = crystalReport;
            //crystalReport.RefreshReport();
            crystalReport.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Server.MapPath(".") + filePath + fileName);

            FileInfo fi = new FileInfo(Server.MapPath(".") + filePath + fileName);
            if (fi.Exists)
            {
                Document pdfDocument = new Document(fi.FullName);
                PptxSaveOptions pptxOptions = new PptxSaveOptions();
                // Save output file
                pdfDocument.Save(fi.FullName.Replace(".pdf", ".pptx"), pptxOptions);

                obj_Generate_Final_Doc.Report_Summery_PDF_File = filePath + fileName;
                obj_Generate_Final_Doc.Report_Summery_PPT_File = filePath + fileName.Replace(".pdf", ".pptx");
            }
        }


        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            string filePath = "\\Downloads\\" + Session["Person_Id"].ToString() + "\\";
            if (!Directory.Exists(Server.MapPath(".") + filePath))
            {
                Directory.CreateDirectory(Server.MapPath(".") + filePath);
            }

            string fileName = "C_StagnantPhysical_Detail.pdf";

            List<tbl_Stagnant_Progress_Detail> obj_tbl_Stagnant_Progress_Detail_Li = new List<tbl_Stagnant_Progress_Detail>();
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                tbl_Stagnant_Progress_Detail obj_tbl_Stagnant_Progress_Detail = new tbl_Stagnant_Progress_Detail();
                obj_tbl_Stagnant_Progress_Detail.Circle_Id = Convert.ToInt32(ds.Tables[0].Rows[i]["Circle_Id"].ToString());
                obj_tbl_Stagnant_Progress_Detail.Zone_Id = Convert.ToInt32(ds.Tables[0].Rows[i]["Zone_Id"].ToString());
                obj_tbl_Stagnant_Progress_Detail.Division_Id = Convert.ToInt32(ds.Tables[0].Rows[i]["Division_Id"].ToString());
                obj_tbl_Stagnant_Progress_Detail.Circle_Name = ds.Tables[0].Rows[i]["Circle_Name"].ToString();
                obj_tbl_Stagnant_Progress_Detail.Zone_Name = ds.Tables[0].Rows[i]["Zone_Name"].ToString();
                obj_tbl_Stagnant_Progress_Detail.Division_Name = ds.Tables[0].Rows[i]["Division_Name"].ToString();
                obj_tbl_Stagnant_Progress_Detail.ProjectWork_Name = ds.Tables[0].Rows[i]["ProjectWork_Name"].ToString();
                obj_tbl_Stagnant_Progress_Detail.ProjectWork_Code = ds.Tables[0].Rows[i]["ProjectWork_ProjectCode"].ToString();
                obj_tbl_Stagnant_Progress_Detail.Circle_Division_Name = ds.Tables[0].Rows[i]["Circle_Name"].ToString() + " - " + ds.Tables[0].Rows[i]["Division_Name"].ToString();
                obj_tbl_Stagnant_Progress_Detail.Header_Text = "Stagnant Physical Progress for more than 60 Days - : AMRUT";
                try
                {
                    obj_tbl_Stagnant_Progress_Detail.Financial_Progress = Convert.ToDecimal(ds.Tables[0].Rows[i]["Financial_Progress"].ToString());
                }
                catch
                {
                    obj_tbl_Stagnant_Progress_Detail.Financial_Progress = 0;
                }
                try
                {
                    obj_tbl_Stagnant_Progress_Detail.Physical_Progress = Convert.ToDecimal(ds.Tables[0].Rows[i]["Physical_Progress"].ToString());
                }
                catch
                {
                    obj_tbl_Stagnant_Progress_Detail.Physical_Progress = 0;
                }
                obj_tbl_Stagnant_Progress_Detail.Last_Invoice_Date = ds.Tables[0].Rows[i]["ProjectWorkFinancialTarget_AddedOn"].ToString();
                try
                {
                    obj_tbl_Stagnant_Progress_Detail.Days_Since_Last_Invoice = Convert.ToInt32(ds.Tables[0].Rows[i]["Days_Since_Update"].ToString());
                }
                catch
                {
                    obj_tbl_Stagnant_Progress_Detail.Days_Since_Last_Invoice = 0;
                }
                obj_tbl_Stagnant_Progress_Detail.Issue_Reported = ds.Tables[0].Rows[i]["Issue"].ToString();
                obj_tbl_Stagnant_Progress_Detail_Li.Add(obj_tbl_Stagnant_Progress_Detail);
            }

            string webURI = "";
            if (Page.Request.Url.Query.Trim() == "")
            {
                webURI = (Page.Request.Url.AbsoluteUri.Replace(Page.Request.Url.AbsolutePath, "") + filePath + fileName).Replace("\\", "/");
            }
            else
            {
                webURI = (Page.Request.Url.AbsoluteUri.Replace(Page.Request.Url.AbsolutePath, "").Replace(Page.Request.Url.Query, "") + filePath + fileName).Replace("\\", "/");
            }

            ReportDocument crystalReport = new ReportDocument();
            crystalReport.Load(Server.MapPath("~/Crystal/pmis/Stagnant_Phy_Progress_Detail.rpt"));
            crystalReport.SetDataSource(obj_tbl_Stagnant_Progress_Detail_Li);
            crystalReport.Refresh();
            //crystalReport.ReportSource = crystalReport;
            //crystalReport.RefreshReport();
            crystalReport.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Server.MapPath(".") + filePath + fileName);

            FileInfo fi = new FileInfo(Server.MapPath(".") + filePath + fileName);
            if (fi.Exists)
            {
                Document pdfDocument = new Document(fi.FullName);
                PptxSaveOptions pptxOptions = new PptxSaveOptions();
                // Save output file
                pdfDocument.Save(fi.FullName.Replace(".pdf", ".pptx"), pptxOptions);

                obj_Generate_Final_Doc.Report_Details_PDF_File = filePath + fileName;
                obj_Generate_Final_Doc.Report_Details_PPT_File = filePath + fileName.Replace(".pdf", ".pptx");
            }
        }

        obj_Generate_Final_Doc_Li.Add(obj_Generate_Final_Doc);
        Session["Generate_Final_Doc"] = obj_Generate_Final_Doc_Li;

        render_Download();
    }

    protected void btnExportFinancial_Click(object sender, EventArgs e)
    {
        obj_Generate_Final_Doc_Li = (List<Generate_Final_Doc>)Session["Generate_Final_Doc"];
        Generate_Final_Doc obj_Generate_Final_Doc = new Generate_Final_Doc();
        obj_Generate_Final_Doc.Report_Name = "Financial";
        DataSet ds = new DataSet();
        ds = (new DataLayer()).get_Project_Work_With_No_Invoicing(1013, 0, 0, 0, true, true, 60);
        if (ds != null && ds.Tables.Count > 1 && ds.Tables[1].Rows.Count > 0)
        {
            string filePath = "\\Downloads\\" + Session["Person_Id"].ToString() + "\\";
            if (!Directory.Exists(Server.MapPath(".") + filePath))
            {
                Directory.CreateDirectory(Server.MapPath(".") + filePath);
            }

            string fileName = "D_StagnantFinancial_Summery.pdf";

            List<tbl_Stagnant_Progress> obj_tbl_Stagnant_Progress_Li = new List<tbl_Stagnant_Progress>();
            for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
            {
                tbl_Stagnant_Progress obj_tbl_Stagnant_Progress = new tbl_Stagnant_Progress();
                obj_tbl_Stagnant_Progress.OnGoing = Convert.ToInt32(ds.Tables[1].Rows[i]["OnGoing"].ToString());
                obj_tbl_Stagnant_Progress.Completed = Convert.ToInt32(ds.Tables[1].Rows[i]["Completed"].ToString());
                obj_tbl_Stagnant_Progress.Total = Convert.ToInt32(ds.Tables[1].Rows[i]["Total"].ToString());
                obj_tbl_Stagnant_Progress.Circle_Id = Convert.ToInt32(ds.Tables[1].Rows[i]["Circle_Id"].ToString());
                obj_tbl_Stagnant_Progress.Zone_Id = Convert.ToInt32(ds.Tables[1].Rows[i]["Zone_Id"].ToString());
                obj_tbl_Stagnant_Progress.Circle_Name = ds.Tables[1].Rows[i]["Circle_Name"].ToString();
                obj_tbl_Stagnant_Progress.Zone_Name = ds.Tables[1].Rows[i]["Zone_Name"].ToString();
                obj_tbl_Stagnant_Progress.Zone_Circle_Name = ds.Tables[1].Rows[i]["Zone_Name"].ToString() + " - " + ds.Tables[1].Rows[i]["Circle_Name"].ToString();
                obj_tbl_Stagnant_Progress.Header_Text = "Stagnant Financial Progress for more than 60 Days - : AMRUT";
                obj_tbl_Stagnant_Progress_Li.Add(obj_tbl_Stagnant_Progress);
            }

            string webURI = "";
            if (Page.Request.Url.Query.Trim() == "")
            {
                webURI = (Page.Request.Url.AbsoluteUri.Replace(Page.Request.Url.AbsolutePath, "") + filePath + fileName).Replace("\\", "/");
            }
            else
            {
                webURI = (Page.Request.Url.AbsoluteUri.Replace(Page.Request.Url.AbsolutePath, "").Replace(Page.Request.Url.Query, "") + filePath + fileName).Replace("\\", "/");
            }

            ReportDocument crystalReport = new ReportDocument();
            crystalReport.Load(Server.MapPath("~/Crystal/pmis/Stagnant_Fin_Progress_Summery.rpt"));
            crystalReport.SetDataSource(obj_tbl_Stagnant_Progress_Li);
            crystalReport.Refresh();
            //crystalReport.ReportSource = crystalReport;
            //crystalReport.RefreshReport();
            crystalReport.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Server.MapPath(".") + filePath + fileName);

            FileInfo fi = new FileInfo(Server.MapPath(".") + filePath + fileName);
            if (fi.Exists)
            {
                Document pdfDocument = new Document(fi.FullName);
                PptxSaveOptions pptxOptions = new PptxSaveOptions();
                // Save output file
                pdfDocument.Save(fi.FullName.Replace(".pdf", ".pptx"), pptxOptions);

                obj_Generate_Final_Doc.Report_Summery_PDF_File = filePath + fileName;
                obj_Generate_Final_Doc.Report_Summery_PPT_File = filePath + fileName.Replace(".pdf", ".pptx");
            }
        }

        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            string filePath = "\\Downloads\\" + Session["Person_Id"].ToString() + "\\";
            if (!Directory.Exists(Server.MapPath(".") + filePath))
            {
                Directory.CreateDirectory(Server.MapPath(".") + filePath);
            }

            string fileName = "E_StagnantFinancial_Detail.pdf";

            List<tbl_Stagnant_Progress_Detail> obj_tbl_Stagnant_Progress_Detail_Li = new List<tbl_Stagnant_Progress_Detail>();
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                tbl_Stagnant_Progress_Detail obj_tbl_Stagnant_Progress_Detail = new tbl_Stagnant_Progress_Detail();
                obj_tbl_Stagnant_Progress_Detail.Circle_Id = Convert.ToInt32(ds.Tables[0].Rows[i]["Circle_Id"].ToString());
                obj_tbl_Stagnant_Progress_Detail.Zone_Id = Convert.ToInt32(ds.Tables[0].Rows[i]["Zone_Id"].ToString());
                obj_tbl_Stagnant_Progress_Detail.Division_Id = Convert.ToInt32(ds.Tables[0].Rows[i]["Division_Id"].ToString());
                obj_tbl_Stagnant_Progress_Detail.Circle_Name = ds.Tables[0].Rows[i]["Circle_Name"].ToString();
                obj_tbl_Stagnant_Progress_Detail.Zone_Name = ds.Tables[0].Rows[i]["Zone_Name"].ToString();
                obj_tbl_Stagnant_Progress_Detail.Division_Name = ds.Tables[0].Rows[i]["Division_Name"].ToString();
                obj_tbl_Stagnant_Progress_Detail.ProjectWork_Name = ds.Tables[0].Rows[i]["ProjectWork_Name"].ToString();
                obj_tbl_Stagnant_Progress_Detail.ProjectWork_Code = ds.Tables[0].Rows[i]["ProjectWork_ProjectCode"].ToString();
                obj_tbl_Stagnant_Progress_Detail.Circle_Division_Name = ds.Tables[0].Rows[i]["Circle_Name"].ToString() + " - " + ds.Tables[0].Rows[i]["Division_Name"].ToString();
                obj_tbl_Stagnant_Progress_Detail.Header_Text = "Stagnant Financial Progress for more than 60 Days - : AMRUT";
                try
                {
                    obj_tbl_Stagnant_Progress_Detail.Financial_Progress = Convert.ToDecimal(ds.Tables[0].Rows[i]["Financial_Progress"].ToString());
                }
                catch
                {
                    obj_tbl_Stagnant_Progress_Detail.Financial_Progress = 0;
                }
                try
                {
                    obj_tbl_Stagnant_Progress_Detail.Physical_Progress = Convert.ToDecimal(ds.Tables[0].Rows[i]["Physical_Progress"].ToString());
                }
                catch
                {
                    obj_tbl_Stagnant_Progress_Detail.Physical_Progress = 0;
                }
                obj_tbl_Stagnant_Progress_Detail.Last_Invoice_Date = ds.Tables[0].Rows[i]["Last_Invoice_Date"].ToString();
                try
                {
                    obj_tbl_Stagnant_Progress_Detail.Days_Since_Last_Invoice = Convert.ToInt32(ds.Tables[0].Rows[i]["Days_Diff"].ToString());
                }
                catch
                {
                    obj_tbl_Stagnant_Progress_Detail.Days_Since_Last_Invoice = 0;
                }
                obj_tbl_Stagnant_Progress_Detail.Issue_Reported = ds.Tables[0].Rows[i]["Issue"].ToString();
                obj_tbl_Stagnant_Progress_Detail_Li.Add(obj_tbl_Stagnant_Progress_Detail);
            }

            string webURI = "";
            if (Page.Request.Url.Query.Trim() == "")
            {
                webURI = (Page.Request.Url.AbsoluteUri.Replace(Page.Request.Url.AbsolutePath, "") + filePath + fileName).Replace("\\", "/");
            }
            else
            {
                webURI = (Page.Request.Url.AbsoluteUri.Replace(Page.Request.Url.AbsolutePath, "").Replace(Page.Request.Url.Query, "") + filePath + fileName).Replace("\\", "/");
            }

            ReportDocument crystalReport = new ReportDocument();
            crystalReport.Load(Server.MapPath("~/Crystal/pmis/Stagnant_Fin_Progress_Detail.rpt"));
            crystalReport.SetDataSource(obj_tbl_Stagnant_Progress_Detail_Li);
            crystalReport.Refresh();
            //crystalReport.ReportSource = crystalReport;
            //crystalReport.RefreshReport();
            crystalReport.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Server.MapPath(".") + filePath + fileName);

            FileInfo fi = new FileInfo(Server.MapPath(".") + filePath + fileName);
            if (fi.Exists)
            {
                Document pdfDocument = new Document(fi.FullName);
                PptxSaveOptions pptxOptions = new PptxSaveOptions();
                // Save output file
                pdfDocument.Save(fi.FullName.Replace(".pdf", ".pptx"), pptxOptions);

                obj_Generate_Final_Doc.Report_Details_PDF_File = filePath + fileName;
                obj_Generate_Final_Doc.Report_Details_PPT_File = filePath + fileName.Replace(".pdf", ".pptx");
            }
        }

        obj_Generate_Final_Doc_Li.Add(obj_Generate_Final_Doc);
        Session["Generate_Final_Doc"] = obj_Generate_Final_Doc_Li;

        render_Download();
    }

    protected void btnExportDocument_Click(object sender, EventArgs e)
    {
        obj_Generate_Final_Doc_Li = (List<Generate_Final_Doc>)Session["Generate_Final_Doc"];
        Generate_Final_Doc obj_Generate_Final_Doc = new Generate_Final_Doc();
        obj_Generate_Final_Doc.Report_Name = "Document";
        DataSet ds = new DataSet();
        ds = (new DataLayer()).get_Document_Not_Available_Dashboard("1013", 0, 0, 0, 0, 0, true);
        if (ds != null && ds.Tables.Count > 1 && ds.Tables[1].Rows.Count > 0)
        {
            string filePath = "\\Downloads\\" + Session["Person_Id"].ToString() + "\\";
            if (!Directory.Exists(Server.MapPath(".") + filePath))
            {
                Directory.CreateDirectory(Server.MapPath(".") + filePath);
            }

            string fileName = "F_Document_NA_Summery.pdf";

            List<tbl_Document_NA_Summery> obj_tbl_Document_NA_Summery_Li = new List<tbl_Document_NA_Summery>();
            for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
            {
                tbl_Document_NA_Summery obj_tbl_Document_NA_Summery = new tbl_Document_NA_Summery();
                obj_tbl_Document_NA_Summery.Agreement_Stamp = Convert.ToInt32(ds.Tables[1].Rows[i]["CB_Stamp"].ToString());
                obj_tbl_Document_NA_Summery.BG = Convert.ToInt32(ds.Tables[1].Rows[i]["BG"].ToString());
                obj_tbl_Document_NA_Summery.CB = Convert.ToInt32(ds.Tables[1].Rows[i]["Agreement"].ToString());
                obj_tbl_Document_NA_Summery.Circle_Id = Convert.ToInt32(ds.Tables[1].Rows[i]["Circle_Id"].ToString());
                obj_tbl_Document_NA_Summery.Zone_Id = Convert.ToInt32(ds.Tables[1].Rows[i]["Zone_Id"].ToString());
                obj_tbl_Document_NA_Summery.Circle_Name = ds.Tables[1].Rows[i]["Circle_Name"].ToString();
                obj_tbl_Document_NA_Summery.Zone_Name = ds.Tables[1].Rows[i]["Zone_Name"].ToString();
                obj_tbl_Document_NA_Summery.Zone_Circle_Name = ds.Tables[1].Rows[i]["Zone_Name"].ToString() + " - " + ds.Tables[1].Rows[i]["Circle_Name"].ToString();
                obj_tbl_Document_NA_Summery.CB_Front = Convert.ToInt32(ds.Tables[1].Rows[i]["CB_Front"].ToString());
                obj_tbl_Document_NA_Summery.Financial_Closure = Convert.ToInt32(ds.Tables[1].Rows[i]["FC"].ToString());
                obj_tbl_Document_NA_Summery.First_GO = Convert.ToInt32(ds.Tables[1].Rows[i]["GO_1"].ToString());
                obj_tbl_Document_NA_Summery.LD = Convert.ToInt32(ds.Tables[1].Rows[i]["LD"].ToString());
                obj_tbl_Document_NA_Summery.LOI = Convert.ToInt32(ds.Tables[1].Rows[i]["LOI"].ToString());
                obj_tbl_Document_NA_Summery.MA = Convert.ToInt32(ds.Tables[1].Rows[i]["MA"].ToString());
                obj_tbl_Document_NA_Summery.Package_Count = Convert.ToInt32(ds.Tables[1].Rows[i]["Total_Package"].ToString());
                obj_tbl_Document_NA_Summery.Physical_Closure = 0;
                obj_tbl_Document_NA_Summery.PS = Convert.ToInt32(ds.Tables[1].Rows[i]["PS"].ToString());
                obj_tbl_Document_NA_Summery.Schedule_G = Convert.ToInt32(ds.Tables[1].Rows[i]["Schedule_G"].ToString());
                obj_tbl_Document_NA_Summery.Second_GO = Convert.ToInt32(ds.Tables[1].Rows[i]["GO_2"].ToString());
                obj_tbl_Document_NA_Summery.TE = Convert.ToInt32(ds.Tables[1].Rows[i]["TE"].ToString());
                obj_tbl_Document_NA_Summery.Third_GO = Convert.ToInt32(ds.Tables[1].Rows[i]["GO_3"].ToString());
                obj_tbl_Document_NA_Summery.Variation = Convert.ToInt32(ds.Tables[1].Rows[i]["Variation"].ToString());
                obj_tbl_Document_NA_Summery.Header_Text = "PMIS Documents not available - Number of Packages : AMRUT";
                obj_tbl_Document_NA_Summery_Li.Add(obj_tbl_Document_NA_Summery);
            }

            string webURI = "";
            if (Page.Request.Url.Query.Trim() == "")
            {
                webURI = (Page.Request.Url.AbsoluteUri.Replace(Page.Request.Url.AbsolutePath, "") + filePath + fileName).Replace("\\", "/");
            }
            else
            {
                webURI = (Page.Request.Url.AbsoluteUri.Replace(Page.Request.Url.AbsolutePath, "").Replace(Page.Request.Url.Query, "") + filePath + fileName).Replace("\\", "/");
            }

            ReportDocument crystalReport = new ReportDocument();
            crystalReport.Load(Server.MapPath("~/Crystal/pmis/Document_Miss_Summary.rpt"));
            crystalReport.SetDataSource(obj_tbl_Document_NA_Summery_Li);
            crystalReport.Refresh();
            //crystalReport.ReportSource = crystalReport;
            //crystalReport.RefreshReport();
            crystalReport.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Server.MapPath(".") + filePath + fileName);

            FileInfo fi = new FileInfo(Server.MapPath(".") + filePath + fileName);
            if (fi.Exists)
            {
                Document pdfDocument = new Document(fi.FullName);
                PptxSaveOptions pptxOptions = new PptxSaveOptions();
                // Save output file
                pdfDocument.Save(fi.FullName.Replace(".pdf", ".pptx"), pptxOptions);

                obj_Generate_Final_Doc.Report_Summery_PDF_File = filePath + fileName;
                obj_Generate_Final_Doc.Report_Summery_PPT_File = filePath + fileName.Replace(".pdf", ".pptx");
            }
        }


        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            string filePath = "\\Downloads\\" + Session["Person_Id"].ToString() + "\\";
            if (!Directory.Exists(Server.MapPath(".") + filePath))
            {
                Directory.CreateDirectory(Server.MapPath(".") + filePath);
            }

            string fileName = "G_Document_NA_Detail.pdf";

            List<tbl_Document_NA_Detail> obj_tbl_Document_NA_Detail_Li = new List<tbl_Document_NA_Detail>();
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                tbl_Document_NA_Detail obj_tbl_Document_NA_Detail = new tbl_Document_NA_Detail();
                obj_tbl_Document_NA_Detail.Agreement_Stamp = Convert.ToInt32(ds.Tables[0].Rows[i]["CB_Stamp"].ToString());
                obj_tbl_Document_NA_Detail.BG = Convert.ToInt32(ds.Tables[0].Rows[i]["BG"].ToString());
                obj_tbl_Document_NA_Detail.CB = Convert.ToInt32(ds.Tables[0].Rows[i]["Agreement"].ToString());
                obj_tbl_Document_NA_Detail.Circle_Id = Convert.ToInt32(ds.Tables[0].Rows[i]["Circle_Id"].ToString());
                obj_tbl_Document_NA_Detail.Zone_Id = Convert.ToInt32(ds.Tables[0].Rows[i]["Zone_Id"].ToString());
                obj_tbl_Document_NA_Detail.Division_Id = Convert.ToInt32(ds.Tables[0].Rows[i]["Division_Id"].ToString());
                obj_tbl_Document_NA_Detail.Circle_Name = ds.Tables[0].Rows[i]["Circle_Name"].ToString();
                obj_tbl_Document_NA_Detail.Zone_Name = ds.Tables[0].Rows[i]["Zone_Name"].ToString();
                obj_tbl_Document_NA_Detail.Division_Name = ds.Tables[0].Rows[i]["Division_Name"].ToString();
                obj_tbl_Document_NA_Detail.ProjectWork_Name = ds.Tables[0].Rows[i]["ProjectWork_Name"].ToString();
                obj_tbl_Document_NA_Detail.ProjectWork_Code = ds.Tables[0].Rows[i]["ProjectWork_ProjectCode"].ToString();
                obj_tbl_Document_NA_Detail.Circle_Division_Name = ds.Tables[0].Rows[i]["Circle_Name"].ToString() + " - " + ds.Tables[0].Rows[i]["Division_Name"].ToString();
                obj_tbl_Document_NA_Detail.CB_Front = Convert.ToInt32(ds.Tables[0].Rows[i]["CB_Front"].ToString());
                obj_tbl_Document_NA_Detail.Financial_Closure = Convert.ToInt32(ds.Tables[0].Rows[i]["FC"].ToString());
                obj_tbl_Document_NA_Detail.First_GO = Convert.ToInt32(ds.Tables[0].Rows[i]["GO_1"].ToString());
                obj_tbl_Document_NA_Detail.LD = Convert.ToInt32(ds.Tables[0].Rows[i]["LD"].ToString());
                obj_tbl_Document_NA_Detail.LOI = Convert.ToInt32(ds.Tables[0].Rows[i]["LOI"].ToString());
                obj_tbl_Document_NA_Detail.MA = Convert.ToInt32(ds.Tables[0].Rows[i]["MA"].ToString());
                obj_tbl_Document_NA_Detail.Package_Count = Convert.ToInt32(ds.Tables[0].Rows[i]["Total_Package"].ToString());
                obj_tbl_Document_NA_Detail.Physical_Closure = 0;
                obj_tbl_Document_NA_Detail.PS = Convert.ToInt32(ds.Tables[0].Rows[i]["PS"].ToString());
                obj_tbl_Document_NA_Detail.Schedule_G = Convert.ToInt32(ds.Tables[0].Rows[i]["Schedule_G"].ToString());
                obj_tbl_Document_NA_Detail.Second_GO = Convert.ToInt32(ds.Tables[0].Rows[i]["GO_2"].ToString());
                obj_tbl_Document_NA_Detail.TE = Convert.ToInt32(ds.Tables[0].Rows[i]["TE"].ToString());
                obj_tbl_Document_NA_Detail.Third_GO = Convert.ToInt32(ds.Tables[0].Rows[i]["GO_3"].ToString());
                obj_tbl_Document_NA_Detail.Variation = Convert.ToInt32(ds.Tables[0].Rows[i]["Variation"].ToString());
                obj_tbl_Document_NA_Detail.Header_Text = "PMIS Documents not available - Project Details : AMRUT";
                obj_tbl_Document_NA_Detail_Li.Add(obj_tbl_Document_NA_Detail);
            }

            string webURI = "";
            if (Page.Request.Url.Query.Trim() == "")
            {
                webURI = (Page.Request.Url.AbsoluteUri.Replace(Page.Request.Url.AbsolutePath, "") + filePath + fileName).Replace("\\", "/");
            }
            else
            {
                webURI = (Page.Request.Url.AbsoluteUri.Replace(Page.Request.Url.AbsolutePath, "").Replace(Page.Request.Url.Query, "") + filePath + fileName).Replace("\\", "/");
            }

            ReportDocument crystalReport = new ReportDocument();
            crystalReport.Load(Server.MapPath("~/Crystal/pmis/Document_Miss_Descriptive.rpt"));
            crystalReport.SetDataSource(obj_tbl_Document_NA_Detail_Li);
            crystalReport.Refresh();
            //crystalReport.ReportSource = crystalReport;
            //crystalReport.RefreshReport();
            crystalReport.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Server.MapPath(".") + filePath + fileName);

            FileInfo fi = new FileInfo(Server.MapPath(".") + filePath + fileName);
            if (fi.Exists)
            {
                Document pdfDocument = new Document(fi.FullName);
                PptxSaveOptions pptxOptions = new PptxSaveOptions();
                // Save output file
                pdfDocument.Save(fi.FullName.Replace(".pdf", ".pptx"), pptxOptions);

                obj_Generate_Final_Doc.Report_Details_PDF_File = filePath + fileName;
                obj_Generate_Final_Doc.Report_Details_PPT_File = filePath + fileName.Replace(".pdf", ".pptx");
            }
        }

        obj_Generate_Final_Doc_Li.Add(obj_Generate_Final_Doc);
        Session["Generate_Final_Doc"] = obj_Generate_Final_Doc_Li;

        render_Download();
    }

    protected void btnExportOverRun_Click(object sender, EventArgs e)
    {
        obj_Generate_Final_Doc_Li = (List<Generate_Final_Doc>)Session["Generate_Final_Doc"];
        Generate_Final_Doc obj_Generate_Final_Doc = new Generate_Final_Doc();
        obj_Generate_Final_Doc.Report_Name = "OverRun";
        DataSet ds = new DataSet();
        ds = new DataLayer().get_Time_OverRun_Summery_Dashboard("1013", 0, 0, 0, 0, 0);
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            string filePath = "\\Downloads\\" + Session["Person_Id"].ToString() + "\\";
            if (!Directory.Exists(Server.MapPath(".") + filePath))
            {
                Directory.CreateDirectory(Server.MapPath(".") + filePath);
            }

            string fileName = "H_TimeOverRun_Summery.pdf";

            List<tbl_TimeOverRun> obj_tbl_TimeOverRun_Li = new List<tbl_TimeOverRun>();
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                tbl_TimeOverRun obj_tbl_TimeOverRun = new tbl_TimeOverRun();
                obj_tbl_TimeOverRun.ExtentionExpired = Convert.ToInt32(ds.Tables[0].Rows[i]["Extention_Expired"].ToString());
                obj_tbl_TimeOverRun.RequireExtention = Convert.ToInt32(ds.Tables[0].Rows[i]["Require_Extention"].ToString());
                obj_tbl_TimeOverRun.Circle_Id = Convert.ToInt32(ds.Tables[0].Rows[i]["Circle_Id"].ToString());
                obj_tbl_TimeOverRun.Zone_Id = Convert.ToInt32(ds.Tables[0].Rows[i]["Zone_Id"].ToString());
                obj_tbl_TimeOverRun.Circle_Name = ds.Tables[0].Rows[i]["Circle_Name"].ToString();
                obj_tbl_TimeOverRun.Zone_Name = ds.Tables[0].Rows[i]["Zone_Name"].ToString();
                obj_tbl_TimeOverRun.Zone_Circle_Name = ds.Tables[0].Rows[i]["Zone_Name"].ToString() + " - " + ds.Tables[0].Rows[i]["Circle_Name"].ToString();
                obj_tbl_TimeOverRun.Header_Text = "Time Over run schemes";
                obj_tbl_TimeOverRun_Li.Add(obj_tbl_TimeOverRun);
            }

            string webURI = "";
            if (Page.Request.Url.Query.Trim() == "")
            {
                webURI = (Page.Request.Url.AbsoluteUri.Replace(Page.Request.Url.AbsolutePath, "") + filePath + fileName).Replace("\\", "/");
            }
            else
            {
                webURI = (Page.Request.Url.AbsoluteUri.Replace(Page.Request.Url.AbsolutePath, "").Replace(Page.Request.Url.Query, "") + filePath + fileName).Replace("\\", "/");
            }

            ReportDocument crystalReport = new ReportDocument();
            crystalReport.Load(Server.MapPath("~/Crystal/pmis/Time_Overrun_Summery.rpt"));
            crystalReport.SetDataSource(obj_tbl_TimeOverRun_Li);
            crystalReport.Refresh();
            //crystalReport.ReportSource = crystalReport;
            //crystalReport.RefreshReport();
            crystalReport.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Server.MapPath(".") + filePath + fileName);

            FileInfo fi = new FileInfo(Server.MapPath(".") + filePath + fileName);
            if (fi.Exists)
            {
                Document pdfDocument = new Document(fi.FullName);
                PptxSaveOptions pptxOptions = new PptxSaveOptions();
                // Save output file
                pdfDocument.Save(fi.FullName.Replace(".pdf", ".pptx"), pptxOptions);

                obj_Generate_Final_Doc.Report_Summery_PDF_File = filePath + fileName;
                obj_Generate_Final_Doc.Report_Summery_PPT_File = filePath + fileName.Replace(".pdf", ".pptx");
            }
        }


        ds = new DataSet();
        ds = (new DataLayer()).get_tbl_ProjectWork_PMIS_Dashboard("1013", 0, 0, 0, 0, 0, "", 0, "", 0, 0, 0, "", 0, 0, "", 2, 0, "", -1, -1, "", "", 0, 0, "", "", "", -1, 0);
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            string filePath = "\\Downloads\\" + Session["Person_Id"].ToString() + "\\";
            if (!Directory.Exists(Server.MapPath(".") + filePath))
            {
                Directory.CreateDirectory(Server.MapPath(".") + filePath);
            }
            string fileName = "I_TimeOverRun_Extended_Expired_Detail.pdf";
            
            List<tbl_TimeOverRun_Detail> obj_tbl_TimeOverRun_Detail_Li = new List<tbl_TimeOverRun_Detail>();
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                tbl_TimeOverRun_Detail obj_tbl_TimeOverRun_Detail = new tbl_TimeOverRun_Detail();
                obj_tbl_TimeOverRun_Detail.Circle_Id = Convert.ToInt32(ds.Tables[0].Rows[i]["Circle_Id"].ToString());
                obj_tbl_TimeOverRun_Detail.Zone_Id = Convert.ToInt32(ds.Tables[0].Rows[i]["Zone_Id"].ToString());
                obj_tbl_TimeOverRun_Detail.Division_Id = Convert.ToInt32(ds.Tables[0].Rows[i]["Division_Id"].ToString());
                obj_tbl_TimeOverRun_Detail.Circle_Name = ds.Tables[0].Rows[i]["Circle_Name"].ToString();
                obj_tbl_TimeOverRun_Detail.Zone_Name = ds.Tables[0].Rows[i]["Zone_Name"].ToString();
                obj_tbl_TimeOverRun_Detail.Division_Name = ds.Tables[0].Rows[i]["Division_Name"].ToString();
                obj_tbl_TimeOverRun_Detail.ProjectWork_Name = ds.Tables[0].Rows[i]["ProjectWork_Name"].ToString();
                obj_tbl_TimeOverRun_Detail.ProjectWork_Code = ds.Tables[0].Rows[i]["ProjectWork_ProjectCode"].ToString();
                obj_tbl_TimeOverRun_Detail.Circle_Division_Name = ds.Tables[0].Rows[i]["Circle_Name"].ToString() + " - " + ds.Tables[0].Rows[i]["Division_Name"].ToString();
                obj_tbl_TimeOverRun_Detail.Header_Text = "Time Over run schemes : Extension is Required : AMRUT";
                try
                {
                    obj_tbl_TimeOverRun_Detail.Financial_Progress = Convert.ToDecimal(ds.Tables[0].Rows[i]["Financial_Progress"].ToString());
                }
                catch
                {
                    obj_tbl_TimeOverRun_Detail.Financial_Progress = 0;
                }
                try
                {
                    obj_tbl_TimeOverRun_Detail.Physical_Progress = Convert.ToDecimal(ds.Tables[0].Rows[i]["Physical_Progress"].ToString());
                }
                catch
                {
                    obj_tbl_TimeOverRun_Detail.Physical_Progress = 0;
                }
                obj_tbl_TimeOverRun_Detail.Agreement_Start_Date = ds.Tables[0].Rows[i]["ProjectWorkPkg_Agreement_Date"].ToString();
                obj_tbl_TimeOverRun_Detail.Agreement_End_Date = ds.Tables[0].Rows[i]["ProjectWorkPkg_Due_Date"].ToString();
                obj_tbl_TimeOverRun_Detail.Agreement_Extended_Date = ds.Tables[0].Rows[i]["Target_Date_Agreement_Extended"].ToString();
                obj_tbl_TimeOverRun_Detail_Li.Add(obj_tbl_TimeOverRun_Detail);
            }

            string webURI = "";
            if (Page.Request.Url.Query.Trim() == "")
            {
                webURI = (Page.Request.Url.AbsoluteUri.Replace(Page.Request.Url.AbsolutePath, "") + filePath + fileName).Replace("\\", "/");
            }
            else
            {
                webURI = (Page.Request.Url.AbsoluteUri.Replace(Page.Request.Url.AbsolutePath, "").Replace(Page.Request.Url.Query, "") + filePath + fileName).Replace("\\", "/");
            }

            ReportDocument crystalReport = new ReportDocument();
            crystalReport.Load(Server.MapPath("~/Crystal/pmis/Time_Overrun_Extended_Expired_Detail.rpt"));
            crystalReport.SetDataSource(obj_tbl_TimeOverRun_Detail_Li);
            crystalReport.Refresh();
            //crystalReport.ReportSource = crystalReport;
            //crystalReport.RefreshReport();
            crystalReport.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Server.MapPath(".") + filePath + fileName);

            FileInfo fi = new FileInfo(Server.MapPath(".") + filePath + fileName);
            if (fi.Exists)
            {
                Document pdfDocument = new Document(fi.FullName);
                PptxSaveOptions pptxOptions = new PptxSaveOptions();
                // Save output file
                pdfDocument.Save(fi.FullName.Replace(".pdf", ".pptx"), pptxOptions);

                obj_Generate_Final_Doc.Report_Details_PDF_File = filePath + fileName;
                obj_Generate_Final_Doc.Report_Details_PPT_File = filePath + fileName.Replace(".pdf", ".pptx");
            }
        }

        obj_Generate_Final_Doc_Li.Add(obj_Generate_Final_Doc);
        Session["Generate_Final_Doc"] = obj_Generate_Final_Doc_Li;

        render_Download();
    }

    protected void btnExportOverRun1_Click(object sender, EventArgs e)
    {
        obj_Generate_Final_Doc_Li = (List<Generate_Final_Doc>)Session["Generate_Final_Doc"];
        Generate_Final_Doc obj_Generate_Final_Doc = new Generate_Final_Doc();
        obj_Generate_Final_Doc.Report_Name = "OverRun1";
        DataSet ds = new DataSet();
        ds = new DataLayer().get_Time_OverRun_Summery_Dashboard("1013", 0, 0, 0, 0, 0);
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            string filePath = "\\Downloads\\" + Session["Person_Id"].ToString() + "\\";
            if (!Directory.Exists(Server.MapPath(".") + filePath))
            {
                Directory.CreateDirectory(Server.MapPath(".") + filePath);
            }

            string fileName = "J_TimeOverRun_Summery1.pdf";

            List<tbl_TimeOverRun> obj_tbl_TimeOverRun_Li = new List<tbl_TimeOverRun>();
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                tbl_TimeOverRun obj_tbl_TimeOverRun = new tbl_TimeOverRun();
                obj_tbl_TimeOverRun.ExtentionExpired = Convert.ToInt32(ds.Tables[0].Rows[i]["Extention_Expired"].ToString());
                obj_tbl_TimeOverRun.RequireExtention = Convert.ToInt32(ds.Tables[0].Rows[i]["Require_Extention"].ToString());
                obj_tbl_TimeOverRun.Circle_Id = Convert.ToInt32(ds.Tables[0].Rows[i]["Circle_Id"].ToString());
                obj_tbl_TimeOverRun.Zone_Id = Convert.ToInt32(ds.Tables[0].Rows[i]["Zone_Id"].ToString());
                obj_tbl_TimeOverRun.Circle_Name = ds.Tables[0].Rows[i]["Circle_Name"].ToString();
                obj_tbl_TimeOverRun.Zone_Name = ds.Tables[0].Rows[i]["Zone_Name"].ToString();
                obj_tbl_TimeOverRun.Zone_Circle_Name = ds.Tables[0].Rows[i]["Zone_Name"].ToString() + " - " + ds.Tables[0].Rows[i]["Circle_Name"].ToString();
                obj_tbl_TimeOverRun.Header_Text = "Time Over run schemes";
                obj_tbl_TimeOverRun_Li.Add(obj_tbl_TimeOverRun);
            }

            string webURI = "";
            if (Page.Request.Url.Query.Trim() == "")
            {
                webURI = (Page.Request.Url.AbsoluteUri.Replace(Page.Request.Url.AbsolutePath, "") + filePath + fileName).Replace("\\", "/");
            }
            else
            {
                webURI = (Page.Request.Url.AbsoluteUri.Replace(Page.Request.Url.AbsolutePath, "").Replace(Page.Request.Url.Query, "") + filePath + fileName).Replace("\\", "/");
            }

            ReportDocument crystalReport = new ReportDocument();
            crystalReport.Load(Server.MapPath("~/Crystal/pmis/Time_Overrun_Summery.rpt"));
            crystalReport.SetDataSource(obj_tbl_TimeOverRun_Li);
            crystalReport.Refresh();
            //crystalReport.ReportSource = crystalReport;
            //crystalReport.RefreshReport();
            crystalReport.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Server.MapPath(".") + filePath + fileName);

            FileInfo fi = new FileInfo(Server.MapPath(".") + filePath + fileName);
            if (fi.Exists)
            {
                Document pdfDocument = new Document(fi.FullName);
                PptxSaveOptions pptxOptions = new PptxSaveOptions();
                // Save output file
                pdfDocument.Save(fi.FullName.Replace(".pdf", ".pptx"), pptxOptions);

                obj_Generate_Final_Doc.Report_Summery_PDF_File = filePath + fileName;
                obj_Generate_Final_Doc.Report_Summery_PPT_File = filePath + fileName.Replace(".pdf", ".pptx");
            }
        }

        ds = new DataSet();
        ds = (new DataLayer()).get_tbl_ProjectWork_PMIS_Dashboard("1013", 0, 0, 0, 0, 0, "", 0, "", 0, 0, 0, "", 0, 0, "", 0, 0, "", -1, -1, "", "", 0, 0, "", "", "", -1, 0);
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            string filePath = "\\Downloads\\" + Session["Person_Id"].ToString() + "\\";
            if (!Directory.Exists(Server.MapPath(".") + filePath))
            {
                Directory.CreateDirectory(Server.MapPath(".") + filePath);
            }
            string fileName = "K_TimeOverRun_Expired_Detail.pdf";

            List<tbl_TimeOverRun_Detail> obj_tbl_TimeOverRun_Detail_Li = new List<tbl_TimeOverRun_Detail>();
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                tbl_TimeOverRun_Detail obj_tbl_TimeOverRun_Detail = new tbl_TimeOverRun_Detail();
                obj_tbl_TimeOverRun_Detail.Circle_Id = Convert.ToInt32(ds.Tables[0].Rows[i]["Circle_Id"].ToString());
                obj_tbl_TimeOverRun_Detail.Zone_Id = Convert.ToInt32(ds.Tables[0].Rows[i]["Zone_Id"].ToString());
                obj_tbl_TimeOverRun_Detail.Division_Id = Convert.ToInt32(ds.Tables[0].Rows[i]["Division_Id"].ToString());
                obj_tbl_TimeOverRun_Detail.Circle_Name = ds.Tables[0].Rows[i]["Circle_Name"].ToString();
                obj_tbl_TimeOverRun_Detail.Zone_Name = ds.Tables[0].Rows[i]["Zone_Name"].ToString();
                obj_tbl_TimeOverRun_Detail.Division_Name = ds.Tables[0].Rows[i]["Division_Name"].ToString();
                obj_tbl_TimeOverRun_Detail.ProjectWork_Name = ds.Tables[0].Rows[i]["ProjectWork_Name"].ToString();
                obj_tbl_TimeOverRun_Detail.ProjectWork_Code = ds.Tables[0].Rows[i]["ProjectWork_ProjectCode"].ToString();
                obj_tbl_TimeOverRun_Detail.Circle_Division_Name = ds.Tables[0].Rows[i]["Circle_Name"].ToString() + " - " + ds.Tables[0].Rows[i]["Division_Name"].ToString();
                obj_tbl_TimeOverRun_Detail.Header_Text = "Time Over run schemes : Extension is Over : AMRUT";
                try
                {
                    obj_tbl_TimeOverRun_Detail.Financial_Progress = Convert.ToDecimal(ds.Tables[0].Rows[i]["Financial_Progress"].ToString());
                }
                catch
                {
                    obj_tbl_TimeOverRun_Detail.Financial_Progress = 0;
                }
                try
                {
                    obj_tbl_TimeOverRun_Detail.Physical_Progress = Convert.ToDecimal(ds.Tables[0].Rows[i]["Physical_Progress"].ToString());
                }
                catch
                {
                    obj_tbl_TimeOverRun_Detail.Physical_Progress = 0;
                }
                obj_tbl_TimeOverRun_Detail.Agreement_Start_Date = ds.Tables[0].Rows[i]["ProjectWorkPkg_Agreement_Date"].ToString();
                obj_tbl_TimeOverRun_Detail.Agreement_End_Date = ds.Tables[0].Rows[i]["ProjectWorkPkg_Due_Date"].ToString();
                obj_tbl_TimeOverRun_Detail.Agreement_Extended_Date = ds.Tables[0].Rows[i]["Target_Date_Agreement_Extended"].ToString();
                obj_tbl_TimeOverRun_Detail_Li.Add(obj_tbl_TimeOverRun_Detail);
            }

            string webURI = "";
            if (Page.Request.Url.Query.Trim() == "")
            {
                webURI = (Page.Request.Url.AbsoluteUri.Replace(Page.Request.Url.AbsolutePath, "") + filePath + fileName).Replace("\\", "/");
            }
            else
            {
                webURI = (Page.Request.Url.AbsoluteUri.Replace(Page.Request.Url.AbsolutePath, "").Replace(Page.Request.Url.Query, "") + filePath + fileName).Replace("\\", "/");
            }

            ReportDocument crystalReport = new ReportDocument();
            crystalReport.Load(Server.MapPath("~/Crystal/pmis/Time_Overrun_Expired_Detail.rpt"));
            crystalReport.SetDataSource(obj_tbl_TimeOverRun_Detail_Li);
            crystalReport.Refresh();
            //crystalReport.ReportSource = crystalReport;
            //crystalReport.RefreshReport();
            crystalReport.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Server.MapPath(".") + filePath + fileName);

            FileInfo fi = new FileInfo(Server.MapPath(".") + filePath + fileName);
            if (fi.Exists)
            {
                Document pdfDocument = new Document(fi.FullName);
                PptxSaveOptions pptxOptions = new PptxSaveOptions();
                // Save output file
                pdfDocument.Save(fi.FullName.Replace(".pdf", ".pptx"), pptxOptions);

                obj_Generate_Final_Doc.Report_Details_PDF_File = filePath + fileName;
                obj_Generate_Final_Doc.Report_Details_PPT_File = filePath + fileName.Replace(".pdf", ".pptx");
            }
        }

        obj_Generate_Final_Doc_Li.Add(obj_Generate_Final_Doc);
        Session["Generate_Final_Doc"] = obj_Generate_Final_Doc_Li;

        render_Download();
    }

    protected void btnMergeAllPDF_Click(object sender, EventArgs e)
    {
        string filePath = "\\Downloads\\" + Session["Person_Id"].ToString() + "\\";
        string SourcePdfPath = HttpContext.Current.Server.MapPath(".") + filePath;
        string[] filenames = System.IO.Directory.GetFiles(SourcePdfPath, "*.pdf");
        string outputFileName = "Merge.pdf";
        string outputPath = HttpContext.Current.Server.MapPath(".") + filePath + outputFileName;
        try
        {
            iTextSharp.text.Document doc = new iTextSharp.text.Document();
            PdfCopy writer = new PdfCopy(doc, new FileStream(outputPath, FileMode.Create));
            if (writer == null)
            {
                MessageBox.Show("Error In Merging Document");
                return;
            }
            doc.Open();
            foreach (string filename in filenames)
            {
                PdfReader reader = new PdfReader(filename);
                reader.ConsolidateNamedDestinations();
                for (int i = 1; i <= reader.NumberOfPages; i++)
                {
                    PdfImportedPage page = writer.GetImportedPage(reader, i);
                    writer.AddPage(page);
                }
                reader.Close();
            }
            writer.Close();
            doc.Close();

            Document pdfDocument = new Document(outputPath);
            PptxSaveOptions pptxOptions = new PptxSaveOptions();
            // Save output file
            pdfDocument.Save(outputPath.Replace(".pdf", ".pptx"), pptxOptions);

            obj_Generate_Final_Doc_Li = (List<Generate_Final_Doc>)Session["Generate_Final_Doc"];
            Generate_Final_Doc obj_Generate_Final_Doc = new Generate_Final_Doc();
            obj_Generate_Final_Doc.Report_Name = "Final";
            obj_Generate_Final_Doc.Report_Summery_PDF_File = filePath + outputFileName;
            obj_Generate_Final_Doc.Report_Summery_PPT_File = filePath + outputFileName.Replace(".pdf", ".pptx");

            obj_Generate_Final_Doc_Li.Add(obj_Generate_Final_Doc);
            Session["Generate_Final_Doc"] = obj_Generate_Final_Doc_Li;

            render_Download();
        }
        catch(Exception eee)
        {
            MessageBox.Show(eee.Message);
            return;
        }
    }

    protected void btnExportSNANotAvailable_Click(object sender, EventArgs e)
    {
        obj_Generate_Final_Doc_Li = (List<Generate_Final_Doc>)Session["Generate_Final_Doc"];
        Generate_Final_Doc obj_Generate_Final_Doc = new Generate_Final_Doc();
        obj_Generate_Final_Doc.Report_Name = "SNANotAvailable";
        DataSet ds = new DataSet();
        ds = (new DataLayer()).get_tbl_SNAAccountMaster_SummeryReport("1013", 0, 0, 0, 0, 0, 0, 2);
        if (AllClasses.CheckDataSet(ds))
        {
            string filePath = "\\Downloads\\" + Session["Person_Id"].ToString() + "\\";
            if (!Directory.Exists(Server.MapPath(".") + filePath))
            {
                Directory.CreateDirectory(Server.MapPath(".") + filePath);
            }

            string fileName = "L_SNA_ChildAccount_Limit_NA_Summery.pdf";

            List<tbl_SNA_ChildAccount> obj_tbl_SNA_ChildAccount_Li = new List<tbl_SNA_ChildAccount>();
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                tbl_SNA_ChildAccount obj_tbl_SNA_ChildAccount = new tbl_SNA_ChildAccount();
                obj_tbl_SNA_ChildAccount.Assigned_Limit = Convert.ToDecimal(ds.Tables[0].Rows[i]["SNAAccountLimit_AssignedLimit"].ToString());
                obj_tbl_SNA_ChildAccount.Available_Limit = Convert.ToDecimal(ds.Tables[0].Rows[i]["SNAAccountAvailableLimit"].ToString());
                obj_tbl_SNA_ChildAccount.Available_Limit_PNB = Convert.ToDecimal(ds.Tables[0].Rows[i]["SNAAccountMaster_Balance"].ToString());
                obj_tbl_SNA_ChildAccount.Pipeline_Invoice = Convert.ToDecimal(ds.Tables[0].Rows[i]["SNAAccountPipelineLimitInvoice"].ToString());
                obj_tbl_SNA_ChildAccount.Pipeline_ADP = Convert.ToDecimal(ds.Tables[0].Rows[i]["SNAAccountPipelineLimitADP"].ToString());
                obj_tbl_SNA_ChildAccount.Pipeline_DR = Convert.ToDecimal(ds.Tables[0].Rows[i]["SNAAccountPipelineLimitDR"].ToString());
                obj_tbl_SNA_ChildAccount.Pipeline_MA = Convert.ToDecimal(ds.Tables[0].Rows[i]["SNAAccountPipelineLimitMA"].ToString());
                obj_tbl_SNA_ChildAccount.Pipeline_Total = Convert.ToDecimal(ds.Tables[0].Rows[i]["SNAAccountPipelineLimit"].ToString());
                obj_tbl_SNA_ChildAccount.Total_Invoices = Convert.ToInt32(ds.Tables[0].Rows[i]["Total_Invoice_Count"].ToString());
                obj_tbl_SNA_ChildAccount.Used_Limit = Convert.ToDecimal(ds.Tables[0].Rows[i]["SNAAccountLimitUsed_UsedLimit"].ToString());
                obj_tbl_SNA_ChildAccount.Circle_Id = Convert.ToInt32(ds.Tables[0].Rows[i]["Division_CircleId"].ToString());
                obj_tbl_SNA_ChildAccount.Zone_Id = Convert.ToInt32(ds.Tables[0].Rows[i]["Zone_Id"].ToString());
                obj_tbl_SNA_ChildAccount.Circle_Name = ds.Tables[0].Rows[i]["Circle_Name"].ToString();
                obj_tbl_SNA_ChildAccount.Zone_Name = ds.Tables[0].Rows[i]["Zone_Name"].ToString();
                obj_tbl_SNA_ChildAccount.Zone_Circle_Name = ds.Tables[0].Rows[i]["Zone_Name"].ToString() + " - " + ds.Tables[0].Rows[i]["Circle_Name"].ToString();
                obj_tbl_SNA_ChildAccount.Header_Text = "Payment is Pending Due To Unavailability Of SNA Limit - : AMRUT";
                obj_tbl_SNA_ChildAccount_Li.Add(obj_tbl_SNA_ChildAccount);
            }

            string webURI = "";
            if (Page.Request.Url.Query.Trim() == "")
            {
                webURI = (Page.Request.Url.AbsoluteUri.Replace(Page.Request.Url.AbsolutePath, "") + filePath + fileName).Replace("\\", "/");
            }
            else
            {
                webURI = (Page.Request.Url.AbsoluteUri.Replace(Page.Request.Url.AbsolutePath, "").Replace(Page.Request.Url.Query, "") + filePath + fileName).Replace("\\", "/");
            }

            ReportDocument crystalReport = new ReportDocument();
            crystalReport.Load(Server.MapPath("~/Crystal/pmis/SNA_Child_Account_Summery.rpt"));
            crystalReport.SetDataSource(obj_tbl_SNA_ChildAccount_Li);
            crystalReport.Refresh();
            //crystalReport.ReportSource = crystalReport;
            //crystalReport.RefreshReport();
            crystalReport.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Server.MapPath(".") + filePath + fileName);

            FileInfo fi = new FileInfo(Server.MapPath(".") + filePath + fileName);
            if (fi.Exists)
            {
                Document pdfDocument = new Document(fi.FullName);
                PptxSaveOptions pptxOptions = new PptxSaveOptions();
                // Save output file
                pdfDocument.Save(fi.FullName.Replace(".pdf", ".pptx"), pptxOptions);

                obj_Generate_Final_Doc.Report_Summery_PDF_File = filePath + fileName;
                obj_Generate_Final_Doc.Report_Summery_PPT_File = filePath + fileName.Replace(".pdf", ".pptx");
            }
        }


        ds = (new DataLayer()).get_tbl_SNAAccountMaster("1013", 0, 0, 0, 0, 0, 0, 2);
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            string filePath = "\\Downloads\\" + Session["Person_Id"].ToString() + "\\";
            if (!Directory.Exists(Server.MapPath(".") + filePath))
            {
                Directory.CreateDirectory(Server.MapPath(".") + filePath);
            }

            string fileName = "M_SNA_ChildAccount_Limit_NA_Detail.pdf";

            List<tbl_SNA_ChildAccount_Detail> obj_tbl_SNA_ChildAccount_Detail_Li = new List<tbl_SNA_ChildAccount_Detail>();
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                tbl_SNA_ChildAccount_Detail obj_tbl_SNA_ChildAccount_Detail = new tbl_SNA_ChildAccount_Detail();
                obj_tbl_SNA_ChildAccount_Detail.Circle_Id = Convert.ToInt32(ds.Tables[0].Rows[i]["Division_CircleId"].ToString());
                obj_tbl_SNA_ChildAccount_Detail.Division_Id = Convert.ToInt32(ds.Tables[0].Rows[i]["Division_Id"].ToString());
                obj_tbl_SNA_ChildAccount_Detail.Circle_Name = ds.Tables[0].Rows[i]["Circle_Name"].ToString();
                obj_tbl_SNA_ChildAccount_Detail.Zone_Name = ds.Tables[0].Rows[i]["Zone_Name"].ToString();
                obj_tbl_SNA_ChildAccount_Detail.Zone_Id = Convert.ToInt32(ds.Tables[0].Rows[i]["Zone_Id"].ToString());
                obj_tbl_SNA_ChildAccount_Detail.Division_Name = ds.Tables[0].Rows[i]["Division_Name"].ToString();
                obj_tbl_SNA_ChildAccount_Detail.ProjectWork_Name = ds.Tables[0].Rows[i]["ProjectWork_Name"].ToString();
                obj_tbl_SNA_ChildAccount_Detail.ProjectWork_Code = ds.Tables[0].Rows[i]["ProjectWork_ProjectCode"].ToString();
                obj_tbl_SNA_ChildAccount_Detail.Circle_Division_Name = ds.Tables[0].Rows[i]["Circle_Name"].ToString() + " - " + ds.Tables[0].Rows[i]["Division_Name"].ToString();
                obj_tbl_SNA_ChildAccount_Detail.Header_Text = "Payment is Pending Due To Unavailability Of SNA Limit - : AMRUT";
                try
                {
                    obj_tbl_SNA_ChildAccount_Detail.Assigned_Limit = Convert.ToDecimal(ds.Tables[0].Rows[i]["SNAAccountLimit_AssignedLimit"].ToString());
                }
                catch
                {
                    obj_tbl_SNA_ChildAccount_Detail.Assigned_Limit = 0;
                }
                try
                {
                    obj_tbl_SNA_ChildAccount_Detail.Available_Limit = Convert.ToDecimal(ds.Tables[0].Rows[i]["SNAAccountAvailableLimit"].ToString());
                }
                catch
                {
                    obj_tbl_SNA_ChildAccount_Detail.Available_Limit = 0;
                }
                obj_tbl_SNA_ChildAccount_Detail.Oldest_Invoice_Date = ds.Tables[0].Rows[i]["Min_Date"].ToString();
                try
                {
                    obj_tbl_SNA_ChildAccount_Detail.Available_Limit_PNB = Convert.ToDecimal(ds.Tables[0].Rows[i]["SNAAccountMaster_Balance"].ToString());
                }
                catch
                {
                    obj_tbl_SNA_ChildAccount_Detail.Available_Limit_PNB = 0;
                }
                try
                {
                    obj_tbl_SNA_ChildAccount_Detail.Days_Since_Last_Limit_Assigned = Convert.ToInt32(ds.Tables[0].Rows[i]["Last_Assigned_Day_Diff"].ToString());
                }
                catch
                {
                    obj_tbl_SNA_ChildAccount_Detail.Days_Since_Last_Limit_Assigned = 0;
                }
                try
                {
                    obj_tbl_SNA_ChildAccount_Detail.Pendency_Days = Convert.ToInt32(ds.Tables[0].Rows[i]["Max_Pendency"].ToString());
                }
                catch
                {
                    obj_tbl_SNA_ChildAccount_Detail.Pendency_Days = 0;
                }
                try
                {
                    obj_tbl_SNA_ChildAccount_Detail.Pipeline_ADP = Convert.ToDecimal(ds.Tables[0].Rows[i]["SNAAccountPipelineLimitADP"].ToString());
                }
                catch
                {
                    obj_tbl_SNA_ChildAccount_Detail.Pipeline_ADP = 0;
                }
                try
                {
                    obj_tbl_SNA_ChildAccount_Detail.Pipeline_DR = Convert.ToDecimal(ds.Tables[0].Rows[i]["SNAAccountPipelineLimitDR"].ToString());
                }
                catch
                {
                    obj_tbl_SNA_ChildAccount_Detail.Pipeline_DR = 0;
                }
                try
                {
                    obj_tbl_SNA_ChildAccount_Detail.Pipeline_Invoice = Convert.ToDecimal(ds.Tables[0].Rows[i]["SNAAccountPipelineLimitInvoice"].ToString());
                }
                catch
                {
                    obj_tbl_SNA_ChildAccount_Detail.Pipeline_Invoice = 0;
                }
                try
                {
                    obj_tbl_SNA_ChildAccount_Detail.Pipeline_MA = Convert.ToDecimal(ds.Tables[0].Rows[i]["SNAAccountPipelineLimitMA"].ToString());
                }
                catch
                {
                    obj_tbl_SNA_ChildAccount_Detail.Pipeline_MA = 0;
                }
                try
                {
                    obj_tbl_SNA_ChildAccount_Detail.Pipeline_Total = Convert.ToDecimal(ds.Tables[0].Rows[i]["SNAAccountPipelineLimit"].ToString());
                }
                catch
                {
                    obj_tbl_SNA_ChildAccount_Detail.Pipeline_Total = 0;
                }
                try
                {
                    obj_tbl_SNA_ChildAccount_Detail.Total_Invoices = Convert.ToInt32(ds.Tables[0].Rows[i]["Total_Invoice_Count"].ToString());
                }
                catch
                {
                    obj_tbl_SNA_ChildAccount_Detail.Total_Invoices = 0;
                }
                try
                {
                    obj_tbl_SNA_ChildAccount_Detail.Used_Limit = Convert.ToDecimal(ds.Tables[0].Rows[i]["SNAAccountLimitUsed_UsedLimit"].ToString());
                }
                catch
                {
                    obj_tbl_SNA_ChildAccount_Detail.Used_Limit = 0;
                }
                obj_tbl_SNA_ChildAccount_Detail_Li.Add(obj_tbl_SNA_ChildAccount_Detail);
            }

            string webURI = "";
            if (Page.Request.Url.Query.Trim() == "")
            {
                webURI = (Page.Request.Url.AbsoluteUri.Replace(Page.Request.Url.AbsolutePath, "") + filePath + fileName).Replace("\\", "/");
            }
            else
            {
                webURI = (Page.Request.Url.AbsoluteUri.Replace(Page.Request.Url.AbsolutePath, "").Replace(Page.Request.Url.Query, "") + filePath + fileName).Replace("\\", "/");
            }

            ReportDocument crystalReport = new ReportDocument();
            crystalReport.Load(Server.MapPath("~/Crystal/pmis/SNA_Child_Account_Detail.rpt"));
            crystalReport.SetDataSource(obj_tbl_SNA_ChildAccount_Detail_Li);
            crystalReport.Refresh();
            //crystalReport.ReportSource = crystalReport;
            //crystalReport.RefreshReport();
            crystalReport.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Server.MapPath(".") + filePath + fileName);

            FileInfo fi = new FileInfo(Server.MapPath(".") + filePath + fileName);
            if (fi.Exists)
            {
                Document pdfDocument = new Document(fi.FullName);
                PptxSaveOptions pptxOptions = new PptxSaveOptions();
                // Save output file
                pdfDocument.Save(fi.FullName.Replace(".pdf", ".pptx"), pptxOptions);

                obj_Generate_Final_Doc.Report_Details_PDF_File = filePath + fileName;
                obj_Generate_Final_Doc.Report_Details_PPT_File = filePath + fileName.Replace(".pdf", ".pptx");
            }
        }

        obj_Generate_Final_Doc_Li.Add(obj_Generate_Final_Doc);
        Session["Generate_Final_Doc"] = obj_Generate_Final_Doc_Li;

        render_Download();
    }

    protected void btnExportSNAAvailable_Click(object sender, EventArgs e)
    {
        obj_Generate_Final_Doc_Li = (List<Generate_Final_Doc>)Session["Generate_Final_Doc"];
        Generate_Final_Doc obj_Generate_Final_Doc = new Generate_Final_Doc();
        obj_Generate_Final_Doc.Report_Name = "SNAAvailable";
        DataSet ds = new DataSet();
        ds = (new DataLayer()).get_tbl_SNAAccountMaster_SummeryReport("1013", 0, 0, 0, 0, 0, 0, 1);
        if (AllClasses.CheckDataSet(ds))
        {
            string filePath = "\\Downloads\\" + Session["Person_Id"].ToString() + "\\";
            if (!Directory.Exists(Server.MapPath(".") + filePath))
            {
                Directory.CreateDirectory(Server.MapPath(".") + filePath);
            }

            string fileName = "N_SNA_ChildAccount_Limit_Summery.pdf";

            List<tbl_SNA_ChildAccount> obj_tbl_SNA_ChildAccount_Li = new List<tbl_SNA_ChildAccount>();
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                tbl_SNA_ChildAccount obj_tbl_SNA_ChildAccount = new tbl_SNA_ChildAccount();
                obj_tbl_SNA_ChildAccount.Assigned_Limit = Convert.ToDecimal(ds.Tables[0].Rows[i]["SNAAccountLimit_AssignedLimit"].ToString());
                obj_tbl_SNA_ChildAccount.Available_Limit = Convert.ToDecimal(ds.Tables[0].Rows[i]["SNAAccountAvailableLimit"].ToString());
                obj_tbl_SNA_ChildAccount.Available_Limit_PNB = Convert.ToDecimal(ds.Tables[0].Rows[i]["SNAAccountMaster_Balance"].ToString());
                obj_tbl_SNA_ChildAccount.Pipeline_Invoice = Convert.ToDecimal(ds.Tables[0].Rows[i]["SNAAccountPipelineLimitInvoice"].ToString());
                obj_tbl_SNA_ChildAccount.Pipeline_ADP = Convert.ToDecimal(ds.Tables[0].Rows[i]["SNAAccountPipelineLimitADP"].ToString());
                obj_tbl_SNA_ChildAccount.Pipeline_DR = Convert.ToDecimal(ds.Tables[0].Rows[i]["SNAAccountPipelineLimitDR"].ToString());
                obj_tbl_SNA_ChildAccount.Pipeline_MA = Convert.ToDecimal(ds.Tables[0].Rows[i]["SNAAccountPipelineLimitMA"].ToString());
                obj_tbl_SNA_ChildAccount.Pipeline_Total = Convert.ToDecimal(ds.Tables[0].Rows[i]["SNAAccountPipelineLimit"].ToString());
                obj_tbl_SNA_ChildAccount.Total_Invoices = Convert.ToInt32(ds.Tables[0].Rows[i]["Total_Invoice_Count"].ToString());
                obj_tbl_SNA_ChildAccount.Used_Limit = Convert.ToDecimal(ds.Tables[0].Rows[i]["SNAAccountLimitUsed_UsedLimit"].ToString());
                obj_tbl_SNA_ChildAccount.Circle_Id = Convert.ToInt32(ds.Tables[0].Rows[i]["Division_CircleId"].ToString());
                obj_tbl_SNA_ChildAccount.Zone_Id = Convert.ToInt32(ds.Tables[0].Rows[i]["Zone_Id"].ToString());
                obj_tbl_SNA_ChildAccount.Circle_Name = ds.Tables[0].Rows[i]["Circle_Name"].ToString();
                obj_tbl_SNA_ChildAccount.Zone_Name = ds.Tables[0].Rows[i]["Zone_Name"].ToString();
                obj_tbl_SNA_ChildAccount.Zone_Circle_Name = ds.Tables[0].Rows[i]["Zone_Name"].ToString() + " - " + ds.Tables[0].Rows[i]["Circle_Name"].ToString();
                obj_tbl_SNA_ChildAccount.Header_Text = "Invoices pending where limit is available and payment can be done - : AMRUT";
                obj_tbl_SNA_ChildAccount_Li.Add(obj_tbl_SNA_ChildAccount);
            }

            string webURI = "";
            if (Page.Request.Url.Query.Trim() == "")
            {
                webURI = (Page.Request.Url.AbsoluteUri.Replace(Page.Request.Url.AbsolutePath, "") + filePath + fileName).Replace("\\", "/");
            }
            else
            {
                webURI = (Page.Request.Url.AbsoluteUri.Replace(Page.Request.Url.AbsolutePath, "").Replace(Page.Request.Url.Query, "") + filePath + fileName).Replace("\\", "/");
            }

            ReportDocument crystalReport = new ReportDocument();
            crystalReport.Load(Server.MapPath("~/Crystal/pmis/SNA_Child_Account_Summery.rpt"));
            crystalReport.SetDataSource(obj_tbl_SNA_ChildAccount_Li);
            crystalReport.Refresh();
            //crystalReport.ReportSource = crystalReport;
            //crystalReport.RefreshReport();
            crystalReport.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Server.MapPath(".") + filePath + fileName);

            FileInfo fi = new FileInfo(Server.MapPath(".") + filePath + fileName);
            if (fi.Exists)
            {
                Document pdfDocument = new Document(fi.FullName);
                PptxSaveOptions pptxOptions = new PptxSaveOptions();
                // Save output file
                pdfDocument.Save(fi.FullName.Replace(".pdf", ".pptx"), pptxOptions);

                obj_Generate_Final_Doc.Report_Summery_PDF_File = filePath + fileName;
                obj_Generate_Final_Doc.Report_Summery_PPT_File = filePath + fileName.Replace(".pdf", ".pptx");
            }
        }


        ds = (new DataLayer()).get_tbl_SNAAccountMaster("1013", 0, 0, 0, 0, 0, 0, 1);
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            string filePath = "\\Downloads\\" + Session["Person_Id"].ToString() + "\\";
            if (!Directory.Exists(Server.MapPath(".") + filePath))
            {
                Directory.CreateDirectory(Server.MapPath(".") + filePath);
            }

            string fileName = "O_SNA_ChildAccount_Limit_Detail.pdf";

            List<tbl_SNA_ChildAccount_Detail> obj_tbl_SNA_ChildAccount_Detail_Li = new List<tbl_SNA_ChildAccount_Detail>();
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                tbl_SNA_ChildAccount_Detail obj_tbl_SNA_ChildAccount_Detail = new tbl_SNA_ChildAccount_Detail();
                obj_tbl_SNA_ChildAccount_Detail.Circle_Id = Convert.ToInt32(ds.Tables[0].Rows[i]["Division_CircleId"].ToString());
                obj_tbl_SNA_ChildAccount_Detail.Division_Id = Convert.ToInt32(ds.Tables[0].Rows[i]["Division_Id"].ToString());
                obj_tbl_SNA_ChildAccount_Detail.Circle_Name = ds.Tables[0].Rows[i]["Circle_Name"].ToString();
                obj_tbl_SNA_ChildAccount_Detail.Zone_Name = ds.Tables[0].Rows[i]["Zone_Name"].ToString();
                obj_tbl_SNA_ChildAccount_Detail.Zone_Id = Convert.ToInt32(ds.Tables[0].Rows[i]["Zone_Id"].ToString());
                obj_tbl_SNA_ChildAccount_Detail.Division_Name = ds.Tables[0].Rows[i]["Division_Name"].ToString();
                obj_tbl_SNA_ChildAccount_Detail.ProjectWork_Name = ds.Tables[0].Rows[i]["ProjectWork_Name"].ToString();
                obj_tbl_SNA_ChildAccount_Detail.ProjectWork_Code = ds.Tables[0].Rows[i]["ProjectWork_ProjectCode"].ToString();
                obj_tbl_SNA_ChildAccount_Detail.Circle_Division_Name = ds.Tables[0].Rows[i]["Circle_Name"].ToString() + " - " + ds.Tables[0].Rows[i]["Division_Name"].ToString();
                obj_tbl_SNA_ChildAccount_Detail.Header_Text = "Invoices pending where limit is available and payment can be done - : AMRUT";
                try
                {
                    obj_tbl_SNA_ChildAccount_Detail.Assigned_Limit = Convert.ToDecimal(ds.Tables[0].Rows[i]["SNAAccountLimit_AssignedLimit"].ToString());
                }
                catch
                {
                    obj_tbl_SNA_ChildAccount_Detail.Assigned_Limit = 0;
                }
                try
                {
                    obj_tbl_SNA_ChildAccount_Detail.Available_Limit = Convert.ToDecimal(ds.Tables[0].Rows[i]["SNAAccountAvailableLimit"].ToString());
                }
                catch
                {
                    obj_tbl_SNA_ChildAccount_Detail.Available_Limit = 0;
                }
                obj_tbl_SNA_ChildAccount_Detail.Oldest_Invoice_Date = ds.Tables[0].Rows[i]["Min_Date"].ToString();
                try
                {
                    obj_tbl_SNA_ChildAccount_Detail.Available_Limit_PNB = Convert.ToDecimal(ds.Tables[0].Rows[i]["SNAAccountMaster_Balance"].ToString());
                }
                catch
                {
                    obj_tbl_SNA_ChildAccount_Detail.Available_Limit_PNB = 0;
                }
                try
                {
                    obj_tbl_SNA_ChildAccount_Detail.Days_Since_Last_Limit_Assigned = Convert.ToInt32(ds.Tables[0].Rows[i]["Last_Assigned_Day_Diff"].ToString());
                }
                catch
                {
                    obj_tbl_SNA_ChildAccount_Detail.Days_Since_Last_Limit_Assigned = 0;
                }
                try
                {
                    obj_tbl_SNA_ChildAccount_Detail.Pendency_Days = Convert.ToInt32(ds.Tables[0].Rows[i]["Max_Pendency"].ToString());
                }
                catch
                {
                    obj_tbl_SNA_ChildAccount_Detail.Pendency_Days = 0;
                }
                try
                {
                    obj_tbl_SNA_ChildAccount_Detail.Pipeline_ADP = Convert.ToDecimal(ds.Tables[0].Rows[i]["SNAAccountPipelineLimitADP"].ToString());
                }
                catch
                {
                    obj_tbl_SNA_ChildAccount_Detail.Pipeline_ADP = 0;
                }
                try
                {
                    obj_tbl_SNA_ChildAccount_Detail.Pipeline_DR = Convert.ToDecimal(ds.Tables[0].Rows[i]["SNAAccountPipelineLimitDR"].ToString());
                }
                catch
                {
                    obj_tbl_SNA_ChildAccount_Detail.Pipeline_DR = 0;
                }
                try
                {
                    obj_tbl_SNA_ChildAccount_Detail.Pipeline_Invoice = Convert.ToDecimal(ds.Tables[0].Rows[i]["SNAAccountPipelineLimitInvoice"].ToString());
                }
                catch
                {
                    obj_tbl_SNA_ChildAccount_Detail.Pipeline_Invoice = 0;
                }
                try
                {
                    obj_tbl_SNA_ChildAccount_Detail.Pipeline_MA = Convert.ToDecimal(ds.Tables[0].Rows[i]["SNAAccountPipelineLimitMA"].ToString());
                }
                catch
                {
                    obj_tbl_SNA_ChildAccount_Detail.Pipeline_MA = 0;
                }
                try
                {
                    obj_tbl_SNA_ChildAccount_Detail.Pipeline_Total = Convert.ToDecimal(ds.Tables[0].Rows[i]["SNAAccountPipelineLimit"].ToString());
                }
                catch
                {
                    obj_tbl_SNA_ChildAccount_Detail.Pipeline_Total = 0;
                }
                try
                {
                    obj_tbl_SNA_ChildAccount_Detail.Total_Invoices = Convert.ToInt32(ds.Tables[0].Rows[i]["Total_Invoice_Count"].ToString());
                }
                catch
                {
                    obj_tbl_SNA_ChildAccount_Detail.Total_Invoices = 0;
                }
                try
                {
                    obj_tbl_SNA_ChildAccount_Detail.Used_Limit = Convert.ToDecimal(ds.Tables[0].Rows[i]["SNAAccountLimitUsed_UsedLimit"].ToString());
                }
                catch
                {
                    obj_tbl_SNA_ChildAccount_Detail.Used_Limit = 0;
                }
                obj_tbl_SNA_ChildAccount_Detail_Li.Add(obj_tbl_SNA_ChildAccount_Detail);
            }

            string webURI = "";
            if (Page.Request.Url.Query.Trim() == "")
            {
                webURI = (Page.Request.Url.AbsoluteUri.Replace(Page.Request.Url.AbsolutePath, "") + filePath + fileName).Replace("\\", "/");
            }
            else
            {
                webURI = (Page.Request.Url.AbsoluteUri.Replace(Page.Request.Url.AbsolutePath, "").Replace(Page.Request.Url.Query, "") + filePath + fileName).Replace("\\", "/");
            }

            ReportDocument crystalReport = new ReportDocument();
            crystalReport.Load(Server.MapPath("~/Crystal/pmis/SNA_Child_Account_Detail.rpt"));
            crystalReport.SetDataSource(obj_tbl_SNA_ChildAccount_Detail_Li);
            crystalReport.Refresh();
            //crystalReport.ReportSource = crystalReport;
            //crystalReport.RefreshReport();
            crystalReport.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Server.MapPath(".") + filePath + fileName);

            FileInfo fi = new FileInfo(Server.MapPath(".") + filePath + fileName);
            if (fi.Exists)
            {
                Document pdfDocument = new Document(fi.FullName);
                PptxSaveOptions pptxOptions = new PptxSaveOptions();
                // Save output file
                pdfDocument.Save(fi.FullName.Replace(".pdf", ".pptx"), pptxOptions);

                obj_Generate_Final_Doc.Report_Details_PDF_File = filePath + fileName;
                obj_Generate_Final_Doc.Report_Details_PPT_File = filePath + fileName.Replace(".pdf", ".pptx");
            }
        }

        obj_Generate_Final_Doc_Li.Add(obj_Generate_Final_Doc);
        Session["Generate_Final_Doc"] = obj_Generate_Final_Doc_Li;

        render_Download();
    }
}