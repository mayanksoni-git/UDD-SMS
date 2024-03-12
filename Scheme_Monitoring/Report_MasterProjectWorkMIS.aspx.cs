using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Report_MasterProjectWorkMIS : System.Web.UI.Page
{
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
            divPreInvoiceDetails.Visible = false;
            
            if (Request.QueryString.Count > 0)
            {
                get_tbl_FundingPattern();                
                int ProjectWork_Id = Convert.ToInt32(Request.QueryString[0].Trim());
                load_Project(ProjectWork_Id);                
            }
        }
    }
    protected void load_Project(int ProjectWork_Id)
    {
        hf_ProjectWork_Id.Value = ProjectWork_Id.ToString();
        DataSet ds = new DataSet();
        ds = (new DataLayer()).get_tbl_ProjectWork_Edit(ProjectWork_Id);
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            hf_Project_Id.Value = ds.Tables[0].Rows[0]["ProjectWork_Project_Id"].ToString();
        }
        else
        {
            hf_Project_Id.Value = "";
        }
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            grdProMIS.DataSource = ds.Tables[0];
            grdProMIS.DataBind();
        }
        else
        {
            grdProMIS.DataSource = null;
            grdProMIS.DataBind();
        }
        get_tbl_ProjectIssue();
        get_Dependency(Convert.ToInt32(hf_Project_Id.Value), 0);
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            txtCentage.Text = ds.Tables[0].Rows[0]["ProjectWork_Centage"].ToString();
            txtContegencytext.Text = ds.Tables[0].Rows[0]["ProjectWork_Contegencytext"].ToString();
            hf_BJT.Value = ds.Tables[0].Rows[0]["ProjectWork_Budget"].ToString();
            calculate_total_cost();
        }
        if (ds != null && ds.Tables.Count > 5 && ds.Tables[5].Rows.Count > 0)
        {
            grdFundingPattern.DataSource = ds.Tables[5];
            grdFundingPattern.DataBind();

            grdFundingPattern.FooterRow.Cells[2].Text = "Work Cost (In Lakhs): ";
            grdFundingPattern.FooterRow.Cells[3].Text = ds.Tables[5].Compute("sum(ProjectWorkFundingPattern_Value)", "").ToString();
        }
        else
        {
            get_tbl_FundingPattern();
        }
        get_Package_Wise_Details(ProjectWork_Id);
        get_tbl_ProjectWorkGO(ProjectWork_Id);
        get_Project_Invoice_Details(ProjectWork_Id);
        get_Package_and_Month_Wise_Invoice_Details(ProjectWork_Id);
        if (ds != null && ds.Tables.Count > 6 && ds.Tables[6].Rows.Count > 0)
        {
            grdPhysicalProgress.DataSource = ds.Tables[6];
            grdPhysicalProgress.DataBind();
        }   
        else
        {
            grdPhysicalProgress.DataSource = null;
            grdPhysicalProgress.DataBind();
        }
        get_Package_Wise_Documents_Details(ProjectWork_Id);
        get_UC_Details(ProjectWork_Id);
        get_tbl_ProjectWorkIssueDetails(ProjectWork_Id);
        PopulatePager(ProjectWork_Id, 0);
        get_Variation_Document_Log();
    }
    protected void grdProMIS_PreRender(object sender, EventArgs e)
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
    protected void grdProMIS_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string filePath = e.Row.Cells[19].Text.Trim().Replace("&nbsp;", "");
            if (filePath.Trim() == "")
            {
                LinkButton lnkBtn = (LinkButton)e.Row.FindControl("lnkGODoc");
                lnkBtn.Visible = false;
            }
        }
        
    }
    private void calculate_total_cost()
    {
        decimal total_budget = 0;
        try
        {
            total_budget = Convert.ToDecimal(hf_BJT.Value);
        }
        catch
        {
            total_budget = 0;
        }

        decimal centage = 0;
        try
        {
            centage = Convert.ToDecimal(txtCentage.Text.Trim());
        }
        catch
        {
            centage = 0;
        }

        decimal Contegency = 0;
        try
        {
            Contegency = Convert.ToDecimal(txtContegencytext.Text.Trim());
        }
        catch
        {
            Contegency = 0;
        }

        decimal Funding_Pattern_Total = 0;
        try
        {
            Funding_Pattern_Total = Convert.ToDecimal(grdFundingPattern.FooterRow.Cells[3].Text);
        }
        catch
        {
            Funding_Pattern_Total = 0;
        }
        txtWorkCost_Centage.Text = (total_budget + centage + Contegency).ToString();
    }
    private void get_tbl_FundingPattern()
    {
        DataSet ds = new DataSet();
        ds = (new DataLayer()).get_tbl_FundingPattern();
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            grdFundingPattern.DataSource = ds.Tables[0];
            grdFundingPattern.DataBind();

            grdFundingPattern.FooterRow.Cells[2].Text = "Work Cost (In Lakhs): ";
            grdFundingPattern.FooterRow.Cells[3].Text = "0.00";
        }
        else
        {
            grdFundingPattern.DataSource = null;
            grdFundingPattern.DataBind();
        }
    }
    protected void grdFundingPattern_PreRender(object sender, EventArgs e)
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

    protected void grdFundingPattern_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            TextBox txtShareV = e.Row.FindControl("txtShareV") as TextBox;
            int FundingPattern_Id = Convert.ToInt32(e.Row.Cells[0].Text.Trim());
            if (FundingPattern_Id == 1 || FundingPattern_Id == 2 || FundingPattern_Id == 3)
            {
                //txtShareV.ReadOnly = true;
            }
        }
    }
    protected void get_Package_Wise_Details(int ProjectWork_Id)
    {
        DataSet ds = new DataSet();
        ds = (new DataLayer()).get_tbl_ProjectWorkPkg(ProjectWork_Id, 0, 0, 0, 0, 0, 0, "", "", false, 0);
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            grdPackageDetails.DataSource = ds.Tables[0];
            grdPackageDetails.DataBind();

            grdPackageDetails.FooterRow.Cells[11].Text = "Agreement Amount (Tender Cost): ";
            grdPackageDetails.FooterRow.Cells[12].Text = ds.Tables[0].Compute("sum(ProjectWorkPkg_AgreementAmount)", "").ToString();
        }
        else
        {
            grdPackageDetails.DataSource = null;
            grdPackageDetails.DataBind();
        }
    }
    protected void grdPackageDetails_PreRender(object sender, EventArgs e)
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
    private void get_tbl_ProjectWorkGO(int ProjectWork_Id)
    {
        hf_ProjectWork_Id.Value = ProjectWork_Id.ToString();
        DataSet ds = new DataSet();
        ds = (new DataLayer()).get_tbl_ProjectWorkGO(ProjectWork_Id, "S");

        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            grdCallProductDtls.DataSource = ds.Tables[0];
            grdCallProductDtls.DataBind();
        }
        else
        {
            grdCallProductDtls.DataSource = null;
            grdCallProductDtls.DataBind();
        }
        ds = new DataSet();
        ds = (new DataLayer()).get_tbl_ProjectWorkGO(ProjectWork_Id, "U");

        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            grdULBShare.DataSource = ds.Tables[0];
            grdULBShare.DataBind();
        }
        else
        {
            grdULBShare.DataSource = null;
            grdULBShare.DataBind();
        }
    }

    protected void grdCallProductDtls_PreRender(object sender, EventArgs e)
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
    protected void grdCallProductDtls_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string filePath = e.Row.Cells[1].Text.Trim();
            if (filePath.Trim().Replace("&nbsp;", "") != "")
            {
                e.Row.Cells[2].BackColor = Color.LightGreen;
            }
            else
            {
                LinkButton lnkBtn = (LinkButton)e.Row.FindControl("lnkSCGO");
                lnkBtn.Visible = false;
            }
        }
    }
    protected void grdULBShare_PreRender(object sender, EventArgs e)
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
    protected void grdULBShare_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string filePath = e.Row.Cells[1].Text.Trim();
            if (filePath.Trim().Replace("&nbsp;", "") != "")
            {
                e.Row.Cells[2].BackColor = Color.LightGreen;
            }
            else
            {
                LinkButton lnkBtn = (LinkButton)e.Row.FindControl("lnkULBGO");
                lnkBtn.Visible = false;
            }
            DropDownList ddlULB = e.Row.FindControl("ddlULB") as DropDownList;
        }
    }
    private void get_Project_Invoice_Details(int ProjectWork_Id)
    {
        hf_ProjectWork_Id.Value = ProjectWork_Id.ToString();
        DataSet ds = new DataSet();
        ds = (new DataLayer()).get_Project_Invoice_Details(ProjectWork_Id);
        if (AllClasses.CheckDataSet(ds))
        {
            grdProjectInvoiceDetails.DataSource = ds.Tables[0];
            grdProjectInvoiceDetails.DataBind();
            get_PreviousInvoiceDetails_ProjectWise(ProjectWork_Id);
        }
        else
        {
            grdProjectInvoiceDetails.DataSource = null;
            grdProjectInvoiceDetails.DataBind();
        }
    }
    private void get_PreviousInvoiceDetails_ProjectWise(int ProjectWork_Id)
    {
        decimal invprev_total = 0;
        hf_ProjectWork_Id.Value = ProjectWork_Id.ToString();
        DataSet ds = new DataSet();
        ds = (new DataLayer()).get_PreviousInvoiceDetails_ProjectWise(ProjectWork_Id);
        if (AllClasses.CheckDataSet(ds))
        {
            try
            {
                invprev_total = decimal.Parse(ds.Tables[0].Compute("sum(Amount)", "").ToString());
            }
            catch
            {
                invprev_total = 0;
            }
            //(grdProjectInvoiceDetails.Rows[0].FindControl("lblPrevInvoiceTotal") as LinkButton).Text = ds.Tables[0].Compute("sum(Amount)", "").ToString();
            grdInvoice.DataSource = ds.Tables[0];
            grdInvoice.DataBind();
        }
        else
        {
            invprev_total = 0;
            //(grdProjectInvoiceDetails.Rows[0].FindControl("lblPrevInvoiceTotal") as LinkButton).Text = "";
            divPreInvoiceDetails.Visible = false;
            grdInvoice.DataSource = null;
            grdInvoice.DataBind();
        }
        decimal adp_total = get_PreviousInvoiceDetailsADP_ProjectWise(ProjectWork_Id);
        (grdProjectInvoiceDetails.Rows[0].FindControl("lblPrevInvoiceTotal") as LinkButton).Text = (invprev_total + adp_total).ToString();

        ds = new DataSet();
        ds = (new DataLayer()).get_Target_ProjectWise(ProjectWork_Id);
        if (AllClasses.CheckDataSet(ds))
        {
            //(grdProjectInvoiceDetails.Rows[0].FindControl("txtTargetMonth") as TextBox).Text = ds.Tables[0].Rows[ds.Tables[0].Rows.Count - 1]["ProjectWorkFinancialTarget_TargetMonth"].ToString();
            (grdProjectInvoiceDetails.Rows[0].FindControl("txtTargetMonth") as TextBox).Text = ds.Tables[0].Rows[ds.Tables[0].Rows.Count - 1]["ProjectWorkFinancialTarget_Year"].ToString();
            (grdProjectInvoiceDetails.Rows[0].FindControl("ddlMonth") as DropDownList).SelectedValue = ds.Tables[0].Rows[ds.Tables[0].Rows.Count - 1]["ProjectWorkFinancialTarget_Month"].ToString();
        }
        else
        {
            //(grdProjectInvoiceDetails.Rows[0].FindControl("txtTargetMonth") as TextBox).Text = "";
            (grdProjectInvoiceDetails.Rows[0].FindControl("txtTargetMonth") as TextBox).Text = DateTime.Now.Year.ToString();
            (grdProjectInvoiceDetails.Rows[0].FindControl("ddlMonth") as DropDownList).SelectedValue = DateTime.Now.Month.ToString();
        }

        if (ds != null && ds.Tables.Count > 1 && ds.Tables[1].Rows.Count > 0)
        {
            grdProjectInvoiceDetails.Rows[0].Cells[10].Text = ds.Tables[1].Rows[ds.Tables[0].Rows.Count - 1]["ProjectWorkPhysicalTarget_Target"].ToString();
        }
        else
        {
            grdProjectInvoiceDetails.Rows[0].Cells[10].Text = "0";
        }
        if (ds != null && ds.Tables.Count > 2 && ds.Tables[2].Rows.Count > 0)
        {
            grdProjectInvoiceDetails.Rows[0].Cells[12].Text = ds.Tables[2].Rows[0]["Financial_Progress"].ToString();
        }
        else
        {
            grdProjectInvoiceDetails.Rows[0].Cells[12].Text = "0.00";
        }

        if (ds != null && ds.Tables.Count > 3 && ds.Tables[3].Rows.Count > 0)
        {
            grdProjectInvoiceDetails.Rows[0].Cells[11].Text = ds.Tables[3].Rows[0]["ProjectWorkFinancialTarget_TargetA"].ToString();
        }
        else
        {
            grdProjectInvoiceDetails.Rows[0].Cells[11].Text = "0.00";
        }
    }
    private decimal get_PreviousInvoiceDetailsADP_ProjectWise(int ProjectWork_Id)
    {
        decimal adp_total = 0;
        hf_ProjectWork_Id.Value = ProjectWork_Id.ToString();
        DataSet ds = new DataSet();
        ds = (new DataLayer()).get_PreviousInvoiceDetailsADP_ProjectWise(ProjectWork_Id);
        if (AllClasses.CheckDataSet(ds))
        {
            try
            {
                adp_total = decimal.Parse(ds.Tables[0].Compute("sum(Amount)", "").ToString());
            }
            catch
            {
                adp_total = 0;
            }
            grdInvoiceADP.DataSource = ds.Tables[0];
            grdInvoiceADP.DataBind();
            grdInvoiceADP.FooterRow.Cells[4].Text = ds.Tables[0].Compute("sum(Total_Amount)", "").ToString();
            grdInvoiceADP.FooterRow.Cells[5].Text = ds.Tables[0].Compute("sum(GST)", "").ToString();
            grdInvoiceADP.FooterRow.Cells[6].Text = ds.Tables[0].Compute("sum(Amount)", "").ToString();
        }
        else
        {
            adp_total = 0;
            divPreInvoiceDetails.Visible = false;
            grdInvoiceADP.DataSource = null;
            grdInvoiceADP.DataBind();
        }
        return adp_total;
    }
    protected void grdProjectInvoiceDetails_PreRender(object sender, EventArgs e)
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
    protected void grdProjectInvoiceDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            string Month_Name_Before = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(DateTime.Now.AddMonths(-2).Month);
            string Year_Before = DateTime.Now.AddMonths(-2).Year.ToString();

            string Month_Name_Prev = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(DateTime.Now.AddMonths(-1).Month);
            string Year_Prev = DateTime.Now.AddMonths(-1).Year.ToString();

            string Month_Name = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(DateTime.Now.Month);
            string Year = DateTime.Now.Year.ToString();

            e.Row.Cells[4].Text = "Financial Achivment Till " + Month_Name_Before + " " + Year_Before + " (Bills Raised) - [In Lakhs]";
            e.Row.Cells[5].Text = "Financial Achivment Till " + Month_Name_Before + " " + Year_Before + " (Bills Approved) - [In Lakhs]";

            e.Row.Cells[6].Text = "Financial Achivment " + Month_Name_Prev + " " + Year_Prev + " (Bills Raised) - [In Lakhs]";
            e.Row.Cells[7].Text = "Financial Achivment " + Month_Name_Prev + " " + Year_Prev + " (Bills Approved) - [In Lakhs]";

            e.Row.Cells[8].Text = "Financial Achivment " + Month_Name + " " + Year + " (Bills Raised) - [In Lakhs]";
            e.Row.Cells[9].Text = "Financial Achivment " + Month_Name + " " + Year + " (Bills Approved) - [In Lakhs]";
        }
    }
    protected void btnViewDeduction_Click(object sender, ImageClickEventArgs e)
    {
        GridViewRow gr = (sender as ImageButton).Parent.Parent as GridViewRow;
        int Invoice_Id = 0;
        try
        {
            Invoice_Id = Convert.ToInt32(gr.Cells[0].Text.Trim());
        }
        catch
        {
            Invoice_Id = 0;
        }


        if (Invoice_Id > 0)
        {
            DataSet ds = new DataSet();
            ds = (new DataLayer()).get_tbl_Deduction(Invoice_Id, 0);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                mpDeduction.Show();
                grdDeductionHistory.DataSource = ds.Tables[0];
                grdDeductionHistory.DataBind();
            }
            else
            {
                grdDeductionHistory.DataSource = null;
                grdDeductionHistory.DataBind();
            }
        }
    }    
    protected void lnkView_Click(object sender, EventArgs e)
    {
        divInvoiceDetails.Visible = true;
        divPreInvoiceDetails.Visible = false;
    }
    protected void btnEdit_Click(object sender, ImageClickEventArgs e)
    {
        divPreInvoiceDetails.Visible = true;
        divInvoiceDetails.Visible = false;
    }
    protected void lblPrevInvoiceTotal_Click(object sender, EventArgs e)
    {
        divPreInvoiceDetails.Visible = true;
        divInvoiceDetails.Visible = false;
    }
    private void get_Package_and_Month_Wise_Invoice_Details(int ProjectWork_Id)
    {
        hf_ProjectWork_Id.Value = ProjectWork_Id.ToString();
        DataSet ds = new DataSet();
        ds = (new DataLayer()).get_Package_and_Month_Wise_Invoice_Details(ProjectWork_Id);
        if (AllClasses.CheckDataSet(ds))
        {
            grdPost.DataSource = ds.Tables[0];
            grdPost.DataBind();
        }
        else
        {
            grdPost.DataSource = null;
            grdPost.DataBind();
        }
    }
    protected void grdPost_PreRender(object sender, EventArgs e)
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
    protected void Show_Hide_ChildGrid(object sender, EventArgs e)
    {
        ImageButton imgShowHide = (sender as ImageButton);
        GridViewRow row = (imgShowHide.NamingContainer as GridViewRow);
        if (imgShowHide.CommandArgument == "Show")
        {
            row.FindControl("pnlOrders").Visible = true;
            row.FindControl("pnlOrdersDiv").Visible = true;
            imgShowHide.CommandArgument = "Hide";
            imgShowHide.ImageUrl = "assets/images/minus.png";

            GridView grdPostBeat = row.FindControl("grdPostBeat") as GridView;
            GridView grdADP = row.FindControl("grdADP") as GridView;
            GridView grdMA = row.FindControl("grdMA") as GridView;
            GridView grdDeductionRelease = row.FindControl("grdDeductionRelease") as GridView;
            int ProjectWorkPkg_Id = Convert.ToInt32(row.Cells[0].Text.Trim().Replace("&nbsp;", ""));

            get_tbl_PackageInvoice(ProjectWorkPkg_Id, grdPostBeat);

            get_tbl_PackageInvoiceADP(ProjectWorkPkg_Id, grdADP);

            get_tbl_PackageInvoiceMA(ProjectWorkPkg_Id, grdMA);

            get_tbl_Package_DeductionRelease(ProjectWorkPkg_Id, grdDeductionRelease);
        }
        else
        {
            row.FindControl("pnlOrders").Visible = false;
            row.FindControl("pnlOrdersDiv").Visible = false;
            imgShowHide.CommandArgument = "Show";
            imgShowHide.ImageUrl = "assets/images/plus.png";
        }
    }
    private void get_tbl_Package_DeductionRelease(int Package_Id, GridView grdPostBeat)
    {
        DataSet ds = new DataSet();
        bool? isDefered = null;
        ds = (new DataLayer()).get_tbl_Package_DeductionRelease(0, "", 0, 0, 0, 0, Package_Id, "", "", 0, 0, true, "", "", 0, 0, 0, false, isDefered, 0);
        if (AllClasses.CheckDataSet(ds))
        {
            grdPostBeat.DataSource = ds.Tables[0];
            grdPostBeat.DataBind();
        }
        else
        {
            grdPostBeat.DataSource = null;
            grdPostBeat.DataBind();
        }
    }
    private void get_tbl_PackageInvoiceMA(int Package_Id, GridView grdPostBeat)
    {
        DataSet ds = new DataSet();
        bool? isDefered = null;
        ds = (new DataLayer()).get_tbl_Package_MobilizationAdvance(0, "", 0, 0, 0, 0, Package_Id, "", "", 0, 0, true, "", "", 0, 0, 0, false, isDefered, 0);
        if (AllClasses.CheckDataSet(ds))
        {
            grdPostBeat.DataSource = ds.Tables[0];
            grdPostBeat.DataBind();
        }
        else
        {
            grdPostBeat.DataSource = null;
            grdPostBeat.DataBind();
        }
    }
    private void get_tbl_PackageInvoiceADP(int Package_Id, GridView grdPostBeat)
    {
        DataSet ds = new DataSet();
        bool? isDefered = null;
        ds = (new DataLayer()).get_tbl_Package_ADP(0, "", 0, 0, 0, 0, Package_Id, "", "", 0, 0, true, "", "", 0, 0, 0, false, isDefered, 0);
        if (AllClasses.CheckDataSet(ds))
        {
            grdPostBeat.DataSource = ds.Tables[0];
            grdPostBeat.DataBind();
        }
        else
        {
            grdPostBeat.DataSource = null;
            grdPostBeat.DataBind();
        }
    }
    private void get_tbl_PackageInvoice(int Package_Id, GridView grdPostBeat)
    {
        DataSet ds = new DataSet();
        bool? isDefered = null;
        if (Session["UserType"].ToString() == "1" || Session["UserType"].ToString() == "8")
        {
            ds = (new DataLayer()).get_tbl_PackageInvoice(Package_Id, 0, 0, 0, "", 0, 0, true, "", "", 0, 0, -1, false, -1, isDefered, "", 0, 0);
        }
        else
        {
            ds = (new DataLayer()).get_tbl_PackageInvoice(Package_Id, 0, 0, 0, "", Convert.ToInt32(Session["Person_BranchOffice_Id"].ToString()), Convert.ToInt32(Session["PersonJuridiction_DesignationId"].ToString()), true, "", "", 0, 0, -1, false, -1, isDefered, "", 0, 0);
        }
        if (AllClasses.CheckDataSet(ds))
        {
            grdPostBeat.DataSource = ds.Tables[0];
            grdPostBeat.DataBind();
        }
    }
    protected void grdPostBeat_PreRender(object sender, EventArgs e)
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
    protected void grdPhysicalProgress_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            CheckBox chkPostPhysicalProgress = (e.Row.FindControl("chkPostPhysicalProgress") as CheckBox);
            if (Convert.ToInt32(e.Row.Cells[1].Text.Trim().Replace("&nbsp;", "")) > 0)
            {
                chkPostPhysicalProgress.Checked = true;
            }
            else
            {
                chkPostPhysicalProgress.Checked = false;
            }
        }
    }
    protected void get_Package_Wise_Documents_Details(int ProjectWork_Id)
    {
        hf_ProjectWork_Id.Value = ProjectWork_Id.ToString();
        DataSet ds = new DataSet();
        ds = (new DataLayer()).get_tbl_ProjectWorkPkg(ProjectWork_Id, 0, 0, 0, 0, 0, 0, "", "", false, 0);
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            grdPackageDetailsDoc.DataSource = ds.Tables[0];
            grdPackageDetailsDoc.DataBind();
        }
        else
        {
            grdPackageDetailsDoc.DataSource = null;
            grdPackageDetailsDoc.DataBind();
        }
    }
    protected void grdPackageDocumentDetails_PreRender(object sender, EventArgs e)
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
    protected void grdPackageDocumentDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string Agreement_Path = e.Row.Cells[2].Text.Trim().Replace("&nbsp;", "");
            string PerformanceSecurity_Path = e.Row.Cells[3].Text.Trim().Replace("&nbsp;", "");
            string BankGurantee_Path = e.Row.Cells[4].Text.Trim().Replace("&nbsp;", "");
            string Mobelization_Path = e.Row.Cells[5].Text.Trim().Replace("&nbsp;", "");
            if (Agreement_Path.Replace("&nbsp;", "") != "")
            {
                e.Row.Cells[9].BackColor = Color.LightSalmon;
            }
            else
            {
                LinkButton lnkBtn = (LinkButton)e.Row.FindControl("flCB");
                lnkBtn.Visible = false;
            }
            if (PerformanceSecurity_Path != "")
            {
                e.Row.Cells[10].BackColor = Color.LightSeaGreen;
            }
            else
            {
                LinkButton lnkBtn = (LinkButton)e.Row.FindControl("flPS");
                lnkBtn.Visible = false;
            }
            if (BankGurantee_Path != "")
            {
                e.Row.Cells[11].BackColor = Color.LightCoral;
            }
            else
            {
                LinkButton lnkBtn = (LinkButton)e.Row.FindControl("flBG");
                lnkBtn.Visible = false;
            }
            if (Mobelization_Path != "")
            {
                e.Row.Cells[12].BackColor = Color.Chartreuse;
            }
            else
            {
                LinkButton lnkBtn = (LinkButton)e.Row.FindControl("flMA");
                lnkBtn.Visible = false;
            }
        }
    }    
    
    private void get_UC_Details(int ProjectWork_Id)
    {
        DataSet ds = new DataSet();
        ds = (new DataLayer()).get_tbl_ProjectUC(ProjectWork_Id);
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            grdULBShareUC.DataSource = ds.Tables[0];
            //grdULBShareUC.DataBind();    //Not Binding gives Error as "Object reference not set to an instance of an object"
        }
        else
        {
            grdULBShareUC.DataSource = null;
            grdULBShareUC.DataBind();
        }
    }
    protected void grdCallProductDtlsUC_PreRender(object sender, EventArgs e)
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
    protected void grdCallProductDtlsUC_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string ProjectUC_Document = e.Row.Cells[1].Text.Trim().Replace("&nbsp;", "");
            if (ProjectUC_Document.Replace("&nbsp;", "") != "")
            {
                e.Row.Cells[2].BackColor = Color.LightGreen;
            }
            else
            {
                LinkButton lnkBtn = (LinkButton)e.Row.FindControl("lnkUCDoc");
                lnkBtn.Visible = false;
            }
        }
    }
    private void get_tbl_ProjectWorkIssueDetails(int ProjectWork_Id)
    {
        List<tbl_ProjectWorkIssueDetails> obj_tbl_ProjectWorkIssueDetails_Li = new List<tbl_ProjectWorkIssueDetails>();
        obj_tbl_ProjectWorkIssueDetails_Li = (new DataLayer()).get_tbl_ProjectWorkIssueDetails(ProjectWork_Id);
        if (obj_tbl_ProjectWorkIssueDetails_Li != null && obj_tbl_ProjectWorkIssueDetails_Li.Count > 0)
        {
            grdIssue.DataSource = obj_tbl_ProjectWorkIssueDetails_Li;
            grdIssue.DataBind();
            ViewState["dtIssue"] = obj_tbl_ProjectWorkIssueDetails_Li;
        }
        else
        {

            grdIssue.DataSource = null;
            grdIssue.DataBind();
        }
    }
    protected void lnkConLog_Click(object sender, EventArgs e)
    {
        GridViewRow gr = (sender as LinkButton).Parent.Parent as GridViewRow;
        divLog.Visible = true;
        List<tbl_PMIS_ProjectWorkIssueHistory> ProjectWorkIssueDetail_History_Li = new List<tbl_PMIS_ProjectWorkIssueHistory>();
        List<tbl_ProjectWorkIssueDetails> obj_tbl_ProjectWorkIssueDetails_Li = new List<tbl_ProjectWorkIssueDetails>();
        if (ViewState["dtIssue"] != null)
        {
            obj_tbl_ProjectWorkIssueDetails_Li = (List<tbl_ProjectWorkIssueDetails>)(ViewState["dtIssue"]);
            if (obj_tbl_ProjectWorkIssueDetails_Li[gr.RowIndex].ProjectWorkIssueDetail_History_Li != null)
            {
                ProjectWorkIssueDetail_History_Li = obj_tbl_ProjectWorkIssueDetails_Li[gr.RowIndex].ProjectWorkIssueDetail_History_Li;
            }
            if (ProjectWorkIssueDetail_History_Li != null && ProjectWorkIssueDetail_History_Li.Count > 0)
            {
                grdLog.DataSource = ProjectWorkIssueDetail_History_Li;
                grdLog.DataBind();
            }
            else
            {
                obj_tbl_ProjectWorkIssueDetails_Li[gr.RowIndex].ProjectWorkIssueDetail_History_Li = ProjectWorkIssueDetail_History_Li;
                grdLog.DataSource = ProjectWorkIssueDetail_History_Li;
                grdLog.DataBind();
                ViewState["dtIssue"] = obj_tbl_ProjectWorkIssueDetails_Li;
            }
            //(grdLog.FooterRow.FindControl("hf_Issue_Indx") as HiddenField).Value = gr.RowIndex.ToString();
        }
    }
    protected void grdULBShareUC_PreRender(object sender, EventArgs e)
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
    protected void grdLog_PreRender(object sender, EventArgs e)
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
    protected void grdIssue_PreRender(object sender, EventArgs e)
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
    protected void grdBOQ_PreRender(object sender, EventArgs e)
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

    protected void grdBOQ_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblSpecification = e.Row.FindControl("lblSpecification") as Label;
            TextBox txtRE = e.Row.FindControl("txtRE") as TextBox;
            TextBox txtRQ = e.Row.FindControl("txtRQ") as TextBox;
            TextBox txtQtyM = e.Row.FindControl("txtQtyM") as TextBox;
            TextBox txtComments = e.Row.FindControl("txtComments") as TextBox;

            txtRE.Enabled = false;
            txtRQ.Enabled = false;
            txtQtyM.Enabled = false;
            //txtComments.Enabled = false;


            lblSpecification.Text = lblSpecification.Text.Replace("\n", "<br />");

            int PackageBOQVariation_Id = 0;
            try
            {
                PackageBOQVariation_Id = Convert.ToInt32(e.Row.Cells[8].Text.Trim());
            }
            catch
            {
                PackageBOQVariation_Id = 0;
            }
            if (PackageBOQVariation_Id > 0)
            {
                e.Row.Cells[1].BackColor = Color.LightPink;
            }
        }
    }
    private void get_tbl_ProjectIssue()
    {
        DataSet ds = new DataSet();
        ds = (new DataLayer()).get_tbl_ProjectIssue(Convert.ToInt32(hf_Project_Id.Value));
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            ViewState["dtProjectIssue"] = ds.Tables[0];
        }
        else
        {
            ViewState["dtProjectIssue"] = null;
        }
    }
    protected void grdIssue_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DropDownList ddlIssueType = e.Row.FindControl("ddlIssueType") as DropDownList;

            DropDownList ddlDependency = e.Row.FindControl("ddlDependency") as DropDownList;

            DataTable dtDependency = (DataTable)ViewState["Dependency"];
            DataTable dtProjectIssue = (DataTable)ViewState["dtProjectIssue"];
            if (AllClasses.CheckDt(dtProjectIssue))
            {
                AllClasses.FillDropDown(dtProjectIssue, ddlIssueType, "ProjectIssue_Name", "ProjectIssue_Id");
            }
            if (AllClasses.CheckDt(dtDependency))
            {
                AllClasses.FillDropDown(dtDependency, ddlDependency, "Dependency_Name", "Dependency_Id");
            }
            int ProjectWorkIssueDetails_Issue_Id = 0;
            try
            {
                ProjectWorkIssueDetails_Issue_Id = Convert.ToInt32(e.Row.Cells[1].Text.Trim());
            }
            catch
            {
                ProjectWorkIssueDetails_Issue_Id = 0;
            }
            if (ProjectWorkIssueDetails_Issue_Id > 0)
            {
                try
                {
                    ddlIssueType.SelectedValue = ProjectWorkIssueDetails_Issue_Id.ToString();
                }
                catch
                {

                }
            }
            int ProjectWorkIssueDetails_Dependency_Id = 0;
            try
            {
                ProjectWorkIssueDetails_Dependency_Id = Convert.ToInt32(e.Row.Cells[2].Text.Trim());
            }
            catch
            {
                ProjectWorkIssueDetails_Dependency_Id = 0;
            }
            if (ProjectWorkIssueDetails_Dependency_Id > 0)
            {
                try
                {
                    ddlDependency.SelectedValue = ProjectWorkIssueDetails_Dependency_Id.ToString();
                }
                catch
                {

                }
            }
            string ProjectIssue_Document = e.Row.Cells[1].Text.Trim().Replace("&nbsp;", "");
            if (ProjectIssue_Document != "")
            {
                e.Row.Cells[10].BackColor = Color.LightGreen;
            }
            else
            {
                LinkButton lnkBtn = (LinkButton)e.Row.FindControl("lnkIssueDoc");
                lnkBtn.Visible = false;
            }
        }
    }
    private void get_Dependency(int Scheme_Id, int Issue_Id)
    {
        DataSet ds = new DataSet();
        ds = (new DataLayer()).get_tbl_Dependency(Scheme_Id, Issue_Id);
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            ViewState["Dependency"] = ds.Tables[0];
        }
        else
        {
            ViewState["Dependency"] = null;
        }
    }
    protected void grdULBShareUC_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DataSet ds = new DataSet();
            ds = (new DataLayer()).get_tbl_ProjectIssue(Convert.ToInt32(hf_Project_Id.Value));
            DropDownList ddlIssueType = e.Row.FindControl("ddlIssueType") as DropDownList;

            DataTable dtProjectIssue = (DataTable)ds.Tables[0];
            if (AllClasses.CheckDt(dtProjectIssue))
            {
                AllClasses.FillDropDown(dtProjectIssue, ddlIssueType, "ProjectIssue_Name", "ProjectIssue_Id");
            }
            int ProjectWorkIssueDetails_Issue_Id = 0;
            try
            {
                ProjectWorkIssueDetails_Issue_Id = Convert.ToInt32(e.Row.Cells[1].Text.Trim());
            }
            catch
            {
                ProjectWorkIssueDetails_Issue_Id = 0;
            }
            if (ProjectWorkIssueDetails_Issue_Id > 0)
            {
                try
                {
                    ddlIssueType.SelectedValue = ProjectWorkIssueDetails_Issue_Id.ToString();
                }
                catch
                {

                }
            }
        }
    }
    //protected void lnkConLog_Click(object sender, EventArgs e)
    //{
    //    divLog.Visible = true;
    //}
    protected void PopulatePager(int ProjectWork_Id, int ProjectWorkPkg_Id)
    {
        hf_ProjectWork_Id.Value = ProjectWork_Id.ToString();
        DataSet ds = new DataSet();
        ds = (new DataLayer()).get_tbl_ProjectWorkPkg(ProjectWork_Id, 0, 0, 0, 0, 0, 0, "", "", false, 0);
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            List<ListItem> pages = new List<ListItem>();
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                pages.Add(new ListItem(" " + ds.Tables[0].Rows[i]["ProjectWorkPkg_Code"].ToString() + " ", ds.Tables[0].Rows[i]["ProjectWorkPkg_Id"].ToString(), Convert.ToInt32(ds.Tables[0].Rows[i]["ProjectWorkPkg_Id"].ToString()) != ProjectWorkPkg_Id));
            }
            rptPager.DataSource = pages;
            rptPager.DataBind();
        }
        else
        {
            rptPager.DataSource = null;
            rptPager.DataBind();
        }
    }
    private void GetCustomersPageWise(int ProjectWork_Id, int ProjectWorkPkg_Id)
    {
        DataSet ds = new DataSet();
        ds = (new DataLayer()).get_tbl_PackageBOQ(0, ProjectWorkPkg_Id);
        if (ds != null && ds.Tables.Count > 0)
        {
            grdBOQ.DataSource = ds.Tables[0];
            grdBOQ.DataBind();
            hf_ProjectWorkPkg_Id.Value = ProjectWorkPkg_Id.ToString();
            this.PopulatePager(ProjectWork_Id, ProjectWorkPkg_Id);
        }
        else
        {
            hf_ProjectWorkPkg_Id.Value = "0";
            grdBOQ.DataSource = null;
            grdBOQ.DataBind();
        }
    }

    protected void Page_Changed(object sender, EventArgs e)
    {
        int ProjectWork_Id = Convert.ToInt32(Request.QueryString[0].Trim());
        int ProjectWorkPkg_Id = int.Parse((sender as LinkButton).CommandArgument);
        this.GetCustomersPageWise(ProjectWork_Id, ProjectWorkPkg_Id);
    }
    protected void btnMisHistory_Click(object sender, ImageClickEventArgs e)
    {
        DataSet ds = new DataSet();
        int ProjectWork_Id = Convert.ToInt32(Request.QueryString[0].Trim());
        ds = (new DataLayer()).get_tbl_MIS_ProjectWork_StepCountHistory(ProjectWork_Id);
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            mpMisStepHistory.Show();
            grdMisStepHistory.DataSource = ds.Tables[0];
            grdMisStepHistory.DataBind();
        }
        else
        {
            grdMisStepHistory.DataSource = null;
            grdMisStepHistory.DataBind();
        }
    }

    protected void grdVariationDocuments_PreRender(object sender, EventArgs e)
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

    private void get_Variation_Document_Log()
    {
        DataSet ds = new DataSet();
        int ProjectWork_Id = Convert.ToInt32(Request.QueryString[0].Trim());
        ds = (new DataLayer()).get_Variation_Document_Log(0, 0, 0, "", ProjectWork_Id);
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            grdVariationDocuments.DataSource = ds.Tables[0];
            grdVariationDocuments.DataBind();
        }
        else
        {
            grdVariationDocuments.DataSource = null;
            grdVariationDocuments.DataBind();
        }
    }
    protected void grdContractBond_PreRender(object sender, EventArgs e)
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

    protected void grdContractBond_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string filePath = e.Row.Cells[2].Text.Trim().Replace("&nbsp;", "");
            if (filePath.Trim() != "")
            {
                e.Row.Cells[3].BackColor = Color.LightGreen;
            }
            else
            {
                LinkButton lnkDownloadCB = (LinkButton)e.Row.FindControl("lnkDownloadCB");
                lnkDownloadCB.Visible = false;
            }
        }
    }

    protected void grdBG_PreRender(object sender, EventArgs e)
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

    protected void grdBG_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string filePath = e.Row.Cells[2].Text.Trim().Replace("&nbsp;", "");
            if (filePath.Trim() != "")
            {
                e.Row.Cells[3].BackColor = Color.LightGreen;
            }
            else
            {
                LinkButton lnkDownloadBG = (LinkButton)e.Row.FindControl("lnkDownloadBG");
                lnkDownloadBG.Visible = false;
            }
        }
    }

    protected void grdPackageDetailsDoc_RowDataBound(object sender, GridViewRowEventArgs e)
    {

    }

    protected void grdPackageDetailsDoc_PreRender(object sender, EventArgs e)
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

    protected void btnPackageDocEdit_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton lnkUpdate = sender as ImageButton;
        string ProjectWorkPkg_Id = (lnkUpdate.Parent.Parent as GridViewRow).Cells[0].Text.Trim();
        divUpload.Visible = true;
        hf_ProjectWorkPkg_Id.Value = ProjectWorkPkg_Id;
        get_Package_Documents_Details_CB(ProjectWorkPkg_Id);
        get_Package_Documents_Details_BG(ProjectWorkPkg_Id);
    }

    protected void get_Package_Documents_Details_CB(string ProjectWorkPkg_Id)
    {
        DataSet ds = new DataSet();
        ds = (new DataLayer()).get_Package_Documents_Details_CB(ProjectWorkPkg_Id);
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            grdContractBond.DataSource = ds.Tables[0];
            grdContractBond.DataBind();
        }
        else
        {
            grdContractBond.DataSource = null;
            grdContractBond.DataBind();
        }
    }
    private void get_Package_Documents_Details_BG(string ProjectWorkPkg_Id)
    {
        DataSet ds = new DataSet();
        ds = (new DataLayer()).get_Package_Documents_Details_BG(ProjectWorkPkg_Id);

        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            grdBG.DataSource = ds.Tables[0];
            grdBG.DataBind();
        }
        else
        {
            grdBG.DataSource = null;
            grdBG.DataBind();
        }
    }

    protected void lnkViewLog_Click(object sender, EventArgs e)
    {
        int ProjectWork_Id = Convert.ToInt32(Request.QueryString[0].Trim());
        DataSet ds = new DataSet();
        ds = (new DataLayer()).get_Target_Log_ProjectWise(ProjectWork_Id);
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            mpLog.Show();
            grdChangeLog.DataSource = ds.Tables[0];
            grdChangeLog.DataBind();
        }
        else
        {
            grdChangeLog.DataSource = null;
            grdChangeLog.DataBind();
        }
    }
    protected void grdPost_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        string Month_Name_Before = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(DateTime.Now.AddMonths(-2).Month);
        string Year_Before = DateTime.Now.AddMonths(-2).Year.ToString();

        string Month_Name_Prev = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(DateTime.Now.AddMonths(-1).Month);
        string Year_Prev = DateTime.Now.AddMonths(-1).Year.ToString();

        string Month_Name = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(DateTime.Now.Month);
        string Year = DateTime.Now.Year.ToString();
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            (e.Row.FindControl("lblHInvTill") as Label).Text = "Invoice Till " + Month_Name_Before + " " + Year_Before + "";
            (e.Row.FindControl("lblHInvPrev") as Label).Text = "Invoice " + Month_Name_Prev + " " + Year_Prev + "";
            (e.Row.FindControl("lblHInvCurr") as Label).Text = "Invoice " + Month_Name + " " + Year + "";

            (e.Row.FindControl("lblHADPTill") as Label).Text = "Other Dept Till " + Month_Name_Before + " " + Year_Before + "";
            (e.Row.FindControl("lblHADPPrev") as Label).Text = "Other Dept " + Month_Name_Prev + " " + Year_Prev + "";
            (e.Row.FindControl("lblHADPCurr") as Label).Text = "Other Dept " + Month_Name + " " + Year + "";

            (e.Row.FindControl("lblHMATill") as Label).Text = "MA Till " + Month_Name_Before + " " + Year_Before + "";
            (e.Row.FindControl("lblHMAPrev") as Label).Text = "MA " + Month_Name_Prev + " " + Year_Prev + "";
            (e.Row.FindControl("lblHMACurr") as Label).Text = "MA " + Month_Name + " " + Year + "";

            (e.Row.FindControl("lblHDRTill") as Label).Text = "DR Till " + Month_Name_Before + " " + Year_Before + "";
            (e.Row.FindControl("lblHDRPrev") as Label).Text = "DR " + Month_Name_Prev + " " + Year_Prev + "";
            (e.Row.FindControl("lblHDRCurr") as Label).Text = "DR " + Month_Name + " " + Year + "";
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

