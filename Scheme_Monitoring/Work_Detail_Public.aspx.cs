using CrystalDecisions.CrystalReports.Engine;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Globalization;
using System.IO;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Work_Detail_Public : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.QueryString.Count > 0)
            {
                int ProjectWork_Id = 0;
                int Scheme_Id = 0;
                string fromdate = "", tilldate = "";
                try
                {
                    ProjectWork_Id = Convert.ToInt32(Request.QueryString["ProjectWork_Id"].Trim());
                    Scheme_Id = Convert.ToInt32(Request.QueryString["Scheme_Id"].Trim());
                    fromdate = Request.QueryString["fromdate"].Trim();
                    tilldate = Request.QueryString["tilldate"].Trim();
                    get_tbl_PackageInvoice_Pipeline(ProjectWork_Id, Scheme_Id, fromdate, tilldate);
                    get_tbl_Package_ADP_Pipeline(ProjectWork_Id, Scheme_Id, fromdate, tilldate);
                    get_tbl_Package_MobilizationAdvance_Pipeline(ProjectWork_Id, Scheme_Id, fromdate, tilldate);
                    get_tbl_Package_DeductionRelease_Pipeline(ProjectWork_Id, Scheme_Id, fromdate, tilldate);
                }
                catch
                {
                    ProjectWork_Id = 0;
                }
            }
        }
    }
    private void get_tbl_PackageInvoice_Pipeline(int ProjectWork_Id, int Scheme_Id, string fromDate, string tillDate)
    {
        DataSet ds = new DataSet();
        ds = (new DataLayer()).get_tbl_PackageInvoice_Pipeline(fromDate, tillDate, ProjectWork_Id, Scheme_Id);
        if (AllClasses.CheckDataSet(ds))
        {
            grdInvoice.DataSource = ds.Tables[0];
            grdInvoice.DataBind();
        }
        else
        {
            grdInvoice.DataSource = null;
            grdInvoice.DataBind();
        }
    }

    private void get_tbl_Package_ADP_Pipeline(int ProjectWork_Id, int Scheme_Id, string fromDate, string tillDate)
    {
        DataSet ds = new DataSet();
        ds = (new DataLayer()).get_tbl_Package_ADP_Pipeline(ProjectWork_Id, fromDate, tillDate, Scheme_Id);
        if (AllClasses.CheckDataSet(ds))
        {
            grdADP.DataSource = ds.Tables[0];
            grdADP.DataBind();
        }
        else
        {
            grdADP.DataSource = null;
            grdADP.DataBind();
        }
    }

    private void get_tbl_Package_MobilizationAdvance_Pipeline(int ProjectWork_Id, int Scheme_Id, string fromDate, string tillDate)
    {
        DataSet ds = new DataSet();
        ds = (new DataLayer()).get_tbl_Package_MobilizationAdvance_Pipeline(ProjectWork_Id, fromDate, tillDate, Scheme_Id);
        if (AllClasses.CheckDataSet(ds))
        {
            grdMA.DataSource = ds.Tables[0];
            grdMA.DataBind();
        }
        else
        {
            grdMA.DataSource = null;
            grdMA.DataBind();
        }
    }

    private void get_tbl_Package_DeductionRelease_Pipeline(int ProjectWork_Id, int Scheme_Id, string fromDate, string tillDate)
    {
        DataSet ds = new DataSet();
        ds = (new DataLayer()).get_tbl_Package_DeductionRelease_Pipeline(ProjectWork_Id, fromDate, tillDate, Scheme_Id);
        if (AllClasses.CheckDataSet(ds))
        {
            grdDeductionRelease.DataSource = ds.Tables[0];
            grdDeductionRelease.DataBind();
        }
        else
        {
            grdDeductionRelease.DataSource = null;
            grdDeductionRelease.DataBind();
        }
    }
    protected void grdTimeLine_PreRender(object sender, EventArgs e)
    {
        GridView gv = (GridView)sender;
        if (gv.Rows.Count > 0)
        {
            //This replaces <td> with <th> and adds the scope attribute
            gv.UseAccessibleHeader = true;
        }
        if ((gv.ShowHeader == true && gv.Rows.Count > 0) || (gv.ShowHeaderWhenEmpty == true))
        {
            gv.HeaderRow.TableSection = TableRowSection.TableHeader;
        }
        if (gv.ShowFooter == true && gv.Rows.Count > 0)
        {
            gv.FooterRow.TableSection = TableRowSection.TableFooter;
        }
    }
    protected void grdInvoice_PreRender(object sender, EventArgs e)
    {
        GridView gv = (GridView)sender;
        if (gv.Rows.Count > 0)
        {
            //This replaces <td> with <th> and adds the scope attribute
            gv.UseAccessibleHeader = true;
        }
        if ((gv.ShowHeader == true && gv.Rows.Count > 0) || (gv.ShowHeaderWhenEmpty == true))
        {
            gv.HeaderRow.TableSection = TableRowSection.TableHeader;
        }
        if (gv.ShowFooter == true && gv.Rows.Count > 0)
        {
            gv.FooterRow.TableSection = TableRowSection.TableFooter;
        }
    }
    protected void btnOpenTimeline1_Click(object sender, ImageClickEventArgs e)
    {
        GridViewRow gr = (sender as ImageButton).Parent.Parent as GridViewRow;
        int Invoice_Id = Convert.ToInt32(gr.Cells[0].Text.Trim());
        DataSet ds = new DataSet();
        ds = (new DataLayer()).get_InvoiceApproval_History_Combined(Invoice_Id);

        if (AllClasses.CheckDataSet(ds))
        {
            DateTime dtPrev;
            DateTime dtStart = DateTime.ParseExact(ds.Tables[0].Rows[0]["Added_On"].ToString(), "dd/MM/yyyy", CultureInfo.InvariantCulture);
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                if (i == 0)
                    dtPrev = dtStart;
                else
                    dtPrev = DateTime.ParseExact(ds.Tables[0].Rows[i - 1]["Added_On"].ToString(), "dd/MM/yyyy", CultureInfo.InvariantCulture);
                DateTime dtCurrent = DateTime.ParseExact(ds.Tables[0].Rows[i]["Added_On"].ToString(), "dd/MM/yyyy", CultureInfo.InvariantCulture);

                ds.Tables[0].Rows[i]["t1"] = dtCurrent.Subtract(dtStart).Days;
                ds.Tables[0].Rows[i]["t2"] = dtCurrent.Subtract(dtPrev).Days;
            }
            grdTimeLine.DataSource = ds.Tables[0];
            grdTimeLine.DataBind();
            mpTimeLine.Show();
        }
        else
        {
            grdTimeLine.DataSource = null;
            grdTimeLine.DataBind();
            MessageBox.Show("No Records Found");
        }
    }

    protected void grdADP_PreRender(object sender, EventArgs e)
    {
        GridView gv = (GridView)sender;
        if (gv.Rows.Count > 0)
        {
            //This replaces <td> with <th> and adds the scope attribute
            gv.UseAccessibleHeader = true;
        }
        if ((gv.ShowHeader == true && gv.Rows.Count > 0) || (gv.ShowHeaderWhenEmpty == true))
        {
            gv.HeaderRow.TableSection = TableRowSection.TableHeader;
        }
        if (gv.ShowFooter == true && gv.Rows.Count > 0)
        {
            gv.FooterRow.TableSection = TableRowSection.TableFooter;
        }
    }

    protected void grdMA_PreRender(object sender, EventArgs e)
    {
        GridView gv = (GridView)sender;
        if (gv.Rows.Count > 0)
        {
            //This replaces <td> with <th> and adds the scope attribute
            gv.UseAccessibleHeader = true;
        }
        if ((gv.ShowHeader == true && gv.Rows.Count > 0) || (gv.ShowHeaderWhenEmpty == true))
        {
            gv.HeaderRow.TableSection = TableRowSection.TableHeader;
        }
        if (gv.ShowFooter == true && gv.Rows.Count > 0)
        {
            gv.FooterRow.TableSection = TableRowSection.TableFooter;
        }
    }

    protected void grdDeductionRelease_PreRender(object sender, EventArgs e)
    {
        GridView gv = (GridView)sender;
        if (gv.Rows.Count > 0)
        {
            //This replaces <td> with <th> and adds the scope attribute
            gv.UseAccessibleHeader = true;
        }
        if ((gv.ShowHeader == true && gv.Rows.Count > 0) || (gv.ShowHeaderWhenEmpty == true))
        {
            gv.HeaderRow.TableSection = TableRowSection.TableHeader;
        }
        if (gv.ShowFooter == true && gv.Rows.Count > 0)
        {
            gv.FooterRow.TableSection = TableRowSection.TableFooter;
        }
    }

    protected void lnkOtherDocsI_Click(object sender, EventArgs e)
    {
        GridViewRow gr = (sender as LinkButton).Parent.Parent as GridViewRow;
        int UsageId = 0;
        string UsageType = "";
        try
        {
            UsageId = Convert.ToInt32(gr.Cells[0].Text.Trim());
        }
        catch
        {
            UsageId = 0;
        }
        UsageType = "I";

        if (UsageId > 0)
        {
            DataSet ds = new DataSet();
            ds = new DataLayer().get_tbl_Invoice_Document_Details(UsageType, UsageId);
            if (AllClasses.CheckDataSet(ds))
            {
                grdMultipleFiles.DataSource = ds.Tables[0];
                grdMultipleFiles.DataBind();
                mp1.Show();
            }
            else
            {
                grdMultipleFiles.DataSource = null;
                grdMultipleFiles.DataBind();
            }
        }
    }

    protected void lnkOtherDocsA_Click(object sender, EventArgs e)
    {
        GridViewRow gr = (sender as LinkButton).Parent.Parent as GridViewRow;
        int UsageId = 0;
        string UsageType = "";
        try
        {
            UsageId = Convert.ToInt32(gr.Cells[3].Text.Trim());
        }
        catch
        {
            UsageId = 0;
        }
        UsageType = "A";

        if (UsageId > 0)
        {
            DataSet ds = new DataSet();
            ds = new DataLayer().get_tbl_Invoice_Document_Details(UsageType, UsageId);
            if (AllClasses.CheckDataSet(ds))
            {
                grdMultipleFiles.DataSource = ds.Tables[0];
                grdMultipleFiles.DataBind();
                mp1.Show();
            }
            else
            {
                grdMultipleFiles.DataSource = null;
                grdMultipleFiles.DataBind();
            }
        }
    }

    protected void lnkOtherDocsM_Click(object sender, EventArgs e)
    {
        GridViewRow gr = (sender as LinkButton).Parent.Parent as GridViewRow;
        int UsageId = 0;
        string UsageType = "";
        try
        {
            UsageId = Convert.ToInt32(gr.Cells[3].Text.Trim());
        }
        catch
        {
            UsageId = 0;
        }
        UsageType = "M";

        if (UsageId > 0)
        {
            DataSet ds = new DataSet();
            ds = new DataLayer().get_tbl_Invoice_Document_Details(UsageType, UsageId);
            if (AllClasses.CheckDataSet(ds))
            {
                grdMultipleFiles.DataSource = ds.Tables[0];
                grdMultipleFiles.DataBind();
                mp1.Show();
            }
            else
            {
                grdMultipleFiles.DataSource = null;
                grdMultipleFiles.DataBind();
            }
        }
    }

    protected void lnkOtherDocsD_Click(object sender, EventArgs e)
    {
        GridViewRow gr = (sender as LinkButton).Parent.Parent as GridViewRow;
        int UsageId = 0;
        string UsageType = "";
        try
        {
            UsageId = Convert.ToInt32(gr.Cells[3].Text.Trim());
        }
        catch
        {
            UsageId = 0;
        }
        UsageType = "D";

        if (UsageId > 0)
        {
            DataSet ds = new DataSet();
            ds = new DataLayer().get_tbl_Invoice_Document_Details(UsageType, UsageId);
            if (AllClasses.CheckDataSet(ds))
            {
                grdMultipleFiles.DataSource = ds.Tables[0];
                grdMultipleFiles.DataBind();
                mp1.Show();
            }
            else
            {
                grdMultipleFiles.DataSource = null;
                grdMultipleFiles.DataBind();
            }
        }
    }

    protected void grdMultipleFiles_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            LinkButton lnkDownload = (e.Row.FindControl("lnkDownload") as LinkButton);

            ScriptManager.GetCurrent(Page).RegisterPostBackControl(lnkDownload);
        }
    }

    protected void grdInvoice_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[11].Text = Session["Default_Zone"].ToString();
            e.Row.Cells[12].Text = Session["Default_Circle"].ToString();
            e.Row.Cells[13].Text = Session["Default_Division"].ToString();
        }
    }

    protected void grdADP_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[8].Text = Session["Default_Zone"].ToString();
            e.Row.Cells[9].Text = Session["Default_Circle"].ToString();
            e.Row.Cells[10].Text = Session["Default_Division"].ToString();
        }
    }

    protected void grdMA_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[8].Text = Session["Default_Zone"].ToString();
            e.Row.Cells[9].Text = Session["Default_Circle"].ToString();
            e.Row.Cells[10].Text = Session["Default_Division"].ToString();
        }
    }

    protected void grdDeductionRelease_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[8].Text = Session["Default_Zone"].ToString();
            e.Row.Cells[9].Text = Session["Default_Circle"].ToString();
            e.Row.Cells[10].Text = Session["Default_Division"].ToString();
        }
    }
}