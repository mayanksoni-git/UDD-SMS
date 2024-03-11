using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class One_Pager_Detail : System.Web.UI.Page
{
    int ProjectWork_Id = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.QueryString.Count > 0)
            {
                try
                {
                    ProjectWork_Id = Convert.ToInt32(Request.QueryString[0].ToString());
                }
                catch
                {
                    ProjectWork_Id = 0;
                }

                if (ProjectWork_Id > 0)
                {
                    get_tbl_ProjectWork_One_Pager_Detailed(ProjectWork_Id);
                    get_Package_Wise_Details(ProjectWork_Id);
                    get_tbl_ProjectWorkGO(ProjectWork_Id);
                }
            }
        }
    }

    private void get_tbl_ProjectWork_One_Pager_Detailed(int ProjectWork_Id)
    {
        DataSet ds = new DataSet();
        ds = (new DataLayer()).get_tbl_ProjectWork_One_Pager_Detailed(ProjectWork_Id);

        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            tdScheme.InnerHtml = ds.Tables[0].Rows[0]["Project_Name"].ToString();
            tdContactPerson.InnerHtml = ds.Tables[0].Rows[0]["Person_Contact"].ToString();
            tdProjectWorkName.InnerHtml = ds.Tables[0].Rows[0]["ProjectWork_Name"].ToString() + " - [" + ds.Tables[0].Rows[0]["ProjectWork_ProjectCode"].ToString() + "]";
            tdDistrict.InnerHtml = ds.Tables[0].Rows[0]["Jurisdiction_Name_Eng"].ToString();
            tdZone.InnerHtml = ds.Tables[0].Rows[0]["Zone_Name"].ToString();
            tdCircle.InnerHtml = ds.Tables[0].Rows[0]["Circle_Name"].ToString();
            tdDivision.InnerHtml = ds.Tables[0].Rows[0]["Division_Name"].ToString();
            tdProjectType.InnerHtml = ds.Tables[0].Rows[0]["ProjectType_Name"].ToString();
            tdGONo.InnerHtml = ds.Tables[0].Rows[0]["ProjectWork_GO_No"].ToString();
            tdGODate.InnerHtml = ds.Tables[0].Rows[0]["ProjectWork_GO_Date"].ToString();
            tdSanctionCost.InnerHtml = ds.Tables[0].Rows[0]["ProjectWork_Budget"].ToString();
            tdWorkCost.InnerHtml = ds.Tables[0].Rows[0]["Work_Cost"].ToString();
            tdCentage.InnerHtml = ds.Tables[0].Rows[0]["Centage"].ToString();
            tdTenderCost.InnerHtml = ds.Tables[0].Rows[0]["Tender_Cost_1"].ToString();
            tdTenderCostGST.InnerHtml = ds.Tables[0].Rows[0]["Tender_Cost"].ToString();
            tdPhysicalProgress.InnerHtml = ds.Tables[0].Rows[0]["Physical_Progress"].ToString();
            tdFinancialProgress.InnerHtml = ds.Tables[0].Rows[0]["Financial_Progress"].ToString();
            tdPhysicalHandover.InnerHtml = ds.Tables[0].Rows[0]["HandoverDone"].ToString();
            tdFinancialClosure.InnerHtml = ds.Tables[0].Rows[0]["FinancialClosureApplicable"].ToString();

            decimal Total_Amount = 0;
            decimal Total_Amount_ADP = 0;
            try
            {
                Total_Amount = Convert.ToDecimal(ds.Tables[0].Rows[0]["Total_Amount"].ToString());
            }
            catch
            {
                Total_Amount = 0;
            }
            try
            {
                Total_Amount_ADP = Convert.ToDecimal(ds.Tables[0].Rows[0]["Total_Amount_ADP"].ToString());
            }
            catch
            {
                Total_Amount_ADP = 0;
            }
            tdTotalExpenditure.InnerHtml = (Total_Amount + Total_Amount_ADP).ToString();
            tdTotalExpenditureAMRUT.InnerHtml = ds.Tables[0].Rows[0]["Total_Expenditure_Till_Date"].ToString();
            tdSNALimitAssigned.InnerHtml = ds.Tables[0].Rows[0]["SNAAccountLimit_AssignedLimit"].ToString();
            tdSNALimitUtilized.InnerHtml = ds.Tables[0].Rows[0]["SNAAccountLimitUsed_UsedLimit"].ToString();
            tdSNALimitAvailablePMIS.InnerHtml = ds.Tables[0].Rows[0]["SNAAccountAvailableLimit"].ToString();
            tdSNALimitAvailablePNB.InnerHtml = ds.Tables[0].Rows[0]["Bal_As_PNB"].ToString();
        }
        else
        {
            
        }

        if (ds != null && ds.Tables.Count > 1 && ds.Tables[1].Rows.Count > 0)
        {
            grdFundingPattern.DataSource = ds.Tables[1];
            grdFundingPattern.DataBind();
        }
        else
        {
            grdFundingPattern.DataSource = null;
            grdFundingPattern.DataBind();
        }

        if (ds != null && ds.Tables.Count > 2 && ds.Tables[2].Rows.Count > 0)
        {
            grdPhysicalProgress.DataSource = ds.Tables[2];
            grdPhysicalProgress.DataBind();
        }
        else
        {
            grdPhysicalProgress.DataSource = null;
            grdPhysicalProgress.DataBind();
        }

        if (ds != null && ds.Tables.Count > 3 && ds.Tables[3].Rows.Count > 0)
        {
            grdIssueDetails.DataSource = ds.Tables[3];
            grdIssueDetails.DataBind();
        }
        else
        {
            grdIssueDetails.DataSource = null;
            grdIssueDetails.DataBind();
        }
    }

    private void get_tbl_ProjectWorkGO(int ProjectWork_Id)
    {
        DataSet ds = new DataSet();
        ds = (new DataLayer()).get_tbl_ProjectWorkGO(ProjectWork_Id, "");

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
    }
    protected void get_Package_Wise_Details(int ProjectWork_Id)
    {
        DataSet ds = new DataSet();
        ds = (new DataLayer()).get_tbl_ProjectWorkPkg(ProjectWork_Id, 0, 0, 0, 0, 0, 0, "", "", false, 0);
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            grdPackageDetails.DataSource = ds.Tables[0];
            grdPackageDetails.DataBind();
        }
        else
        {
            grdPackageDetails.DataSource = null;
            grdPackageDetails.DataBind();
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
    
    protected void grdIssueDetails_PreRender(object sender, EventArgs e)
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

    protected void grdPhysicalProgress_RowDataBound(object sender, GridViewRowEventArgs e)
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
}