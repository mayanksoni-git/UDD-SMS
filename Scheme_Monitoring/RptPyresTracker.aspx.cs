using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class RptPyresTracker : System.Web.UI.Page
{
    Pyres objPyres = new Pyres();
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
            get_tbl_Zone();
            get_tbl_Month();
            lblZoneH.Text = Session["Default_Zone"].ToString();
            lblCircleH.Text = Session["Default_Circle"].ToString();
            lblDivisionH.Text = Session["Default_Division"].ToString();
            if (Session["UserType"].ToString() == "4" && Convert.ToInt32(Session["PersonJuridiction_ZoneId"].ToString()) > 0)
            {//Zone
                try
                {
                    ddlZone.SelectedValue = Session["PersonJuridiction_ZoneId"].ToString();
                    ddlZone_SelectedIndexChanged(ddlZone, e);
                    ddlZone.Enabled = false;
                }
                catch
                { }
            }
            if (Session["UserType"].ToString() == "6" && Convert.ToInt32(Session["PersonJuridiction_ZoneId"].ToString()) > 0)
            {
                try
                {
                    ddlZone.SelectedValue = Session["PersonJuridiction_ZoneId"].ToString();
                    ddlZone_SelectedIndexChanged(ddlZone, e);
                    ddlZone.Enabled = false;
                    if (Session["UserType"].ToString() == "6" && Convert.ToInt32(Session["PersonJuridiction_CircleId"].ToString()) > 0)
                    {//Circle
                        try
                        {
                            ddlCircle.SelectedValue = Session["PersonJuridiction_CircleId"].ToString();
                            ddlCircle_SelectedIndexChanged(ddlCircle, e);
                            ddlCircle.Enabled = false;
                        }
                        catch
                        { }
                    }
                }
                catch
                { }
            }
            if (Session["UserType"].ToString() == "7" && Convert.ToInt32(Session["PersonJuridiction_ZoneId"].ToString()) > 0)
            {
                try
                {
                    ddlZone.SelectedValue = Session["PersonJuridiction_ZoneId"].ToString();
                    ddlZone_SelectedIndexChanged(ddlZone, e);
                    ddlZone.Enabled = false;
                    if (Session["UserType"].ToString() == "7" && Convert.ToInt32(Session["PersonJuridiction_CircleId"].ToString()) > 0)
                    {//Circle
                        try
                        {
                            ddlCircle.SelectedValue = Session["PersonJuridiction_CircleId"].ToString();
                            ddlCircle_SelectedIndexChanged(ddlCircle, e);
                            ddlCircle.Enabled = false;
                            if (Session["UserType"].ToString() == "7" && Convert.ToInt32(Session["PersonJuridiction_DivisionId"].ToString()) > 0)
                            {//Circle
                                try
                                {
                                    ddlDivision.SelectedValue = Session["PersonJuridiction_DivisionId"].ToString();
                                    ddlDivision.Enabled = false;
                                }
                                catch
                                { }
                            }
                        }
                        catch
                        { }
                    }
                }
                catch
                { }
            }
        }
    }

    private void get_tbl_Zone()
    {
        DataSet ds = new DataSet();
        ds = (new DataLayer()).get_tbl_Zone();
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            AllClasses.FillDropDown(ds.Tables[0], ddlZone, "Zone_Name", "Zone_Id");
            if (ddlZone.SelectedItem.Value != "0")
            {
                get_tbl_Circle(Convert.ToInt32(ddlZone.SelectedValue));
            }
        }
        else
        {
            ddlZone.Items.Clear();
        }
    }
    private void get_tbl_Month()
    {
        DataSet ds = new DataSet();
        ds = (new DataLayer()).get_tbl_Month();
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            AllClasses.FillDropDown(ds.Tables[0], ddlMonth, "Month_MonthName", "Month_Id");
        }
        else
        {
            ddlZone.Items.Clear();
        }
    }
    private void get_tbl_Circle(int Zone_Id)
    {
        DataSet ds = new DataSet();
        ds = (new DataLayer()).get_tbl_Circle(Zone_Id);
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            AllClasses.FillDropDown(ds.Tables[0], ddlCircle, "Circle_Name", "Circle_Id");
        }
        else
        {
            ddlCircle.Items.Clear();
        }
    }
    private void get_tbl_Division(int Circle_Id)
    {
        DataSet ds = new DataSet();
        ds = (new DataLayer()).get_tbl_Division(Circle_Id);
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            AllClasses.FillDropDown(ds.Tables[0], ddlDivision, "Division_Name", "Division_Id");
        }
        else
        {
            ddlDivision.Items.Clear();
        }
    }

    //protected void grdPost_PreRender(object sender, EventArgs e)
    //{
    //    GridView gv = (GridView)sender;
    //    if (gv.Rows.Count > 0)
    //    {
    //        //This replaces <td> with <th> and adds the scope attribute
    //        gv.UseAccessibleHeader = true;
    //    }
    //    if ((gv.ShowHeader == true && gv.Rows.Count > 0) || (gv.ShowHeaderWhenEmpty == true))
    //    {
    //        gv.HeaderRow.TableSection = TableRowSection.TableHeader;
    //    }
    //    if (gv.ShowFooter == true && gv.Rows.Count > 0)
    //    {
    //        gv.FooterRow.TableSection = TableRowSection.TableFooter;
    //    }
    //}

    protected void ddlZone_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlZone.SelectedValue == "0")
        {
            ddlCircle.Items.Clear();
            ddlDivision.Items.Clear();
        }
        else
        {
            get_tbl_Circle(Convert.ToInt32(ddlZone.SelectedValue));
        }
    }

    protected void ddlCircle_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlCircle.SelectedValue == "0")
        {
            ddlDivision.Items.Clear();
        }
        else
        {
            get_tbl_Division(Convert.ToInt32(ddlCircle.SelectedValue));
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        int Zone_Id = 0;
        int Circle_Id = 0;
        int Division_Id = 0;
        int Month_Id = 0;
        int Year_Id = 0;

        try
        {
            Zone_Id = Convert.ToInt32(ddlZone.SelectedValue);
        }
        catch
        {
            Zone_Id = 0;
        }

        try
        {
            Circle_Id = Convert.ToInt32(ddlCircle.SelectedValue);
        }
        catch
        {
            Circle_Id = 0;
        }

        try
        {
            Division_Id = Convert.ToInt32(ddlDivision.SelectedValue);
        }
        catch
        {
            Division_Id = 0;
        }

        try
        {
            Month_Id = Convert.ToInt32(ddlMonth.SelectedValue);
        }
        catch
        {
            Month_Id = 0;
        }

        try
        {
            Year_Id = Convert.ToInt32(ddlYear.SelectedValue);
        }
        catch
        {
            Year_Id = 0;
        }

        SearchPyresTracker obj_Search = new SearchPyresTracker();
        obj_Search.Zone = Zone_Id;
        obj_Search.Circle = Circle_Id;
        obj_Search.Division = Division_Id;
        obj_Search.Year = Year_Id;
        obj_Search.Month = Month_Id;

        //Session["SearchStorage"] = obj_Search;
        DataTable dt = new DataTable();
        dt = objPyres.getPyresTrackerBySearch(obj_Search);

        if (dt != null && dt.Rows.Count > 0)
        {
            Session["GridViewData"] = dt;
            MainTracker.DataSource = dt;
            MainTracker.DataBind();
            divData.Visible = true;
        }
        else
        {
            divData.Visible = true;
            MainTracker.DataSource = null;
            MainTracker.DataBind();
            MessageBox.Show("No Records Found");
        }
    }

    
    protected void MainTracker_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            var dataItem = e.Row.DataItem as DataRowView;
            if (dataItem != null)
            {
                Label lblFundsRequired = e.Row.FindControl("lblFundsRequired") as Label;
                ImageButton btnInfo = e.Row.FindControl("btnInfo") as ImageButton;

                if (lblFundsRequired != null && btnInfo != null)
                {
                    string toolTipText = GetToolTipText(dataItem);
                    lblFundsRequired.ToolTip = toolTipText;
                    btnInfo.ToolTip = toolTipText;
                }
            }
        }
    }

    protected string GetToolTipText(object dataItem)
    {
        var drv = dataItem as DataRowView;
        if (drv != null)
        {
            // Assuming these fields exist in your data source and are of type double or can be parsed as double
            double improvisedWoodCost = Convert.ToDouble(drv["CostImprovisedWood"]);
            double gasCost = Convert.ToDouble(drv["CostGas"]);
            double electricCost = Convert.ToDouble(drv["CostElectric"]);
            double improvisedWoodUpgrade = Convert.ToDouble(drv["UpgradeImprovisedWood"]);
            double gasUpgrade = Convert.ToDouble(drv["UpgradeGas"]);
            double electricUpgrade = Convert.ToDouble(drv["UpgradeElectric"]);
            double fundsRequired = Convert.ToDouble(drv["fundsRequired"]);

            // Construct the tooltip text
            return "("+ improvisedWoodUpgrade + "*"+improvisedWoodCost+")+("+ gasUpgrade + "*"+gasCost+")+("+ electricUpgrade + "*"+electricCost+")="+ fundsRequired;
        }
        return string.Empty;
    }

    protected void btnEdit_Click(object sender, ImageClickEventArgs e)
    {
        GridViewRow gr = (sender as ImageButton).Parent.Parent as GridViewRow;
        int PyresTracker_Id = Convert.ToInt32(gr.Cells[0].Text.Trim());
        if (PyresTracker_Id>0 )
        {
            Response.Redirect("PyersTracker.aspx?PyresTracker_Id=" + PyresTracker_Id.ToString());
        }
        else
        {
            return;
        }
    }

    protected void btnCreateNew_Click(object sender, EventArgs e)
    {
        Response.Redirect("PyersTracker.aspx");
    }

    protected void btnExport_Click(object sender, EventArgs e)
    {
        Response.Redirect("ExportToExcel.aspx");
    }
}