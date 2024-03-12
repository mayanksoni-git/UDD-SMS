using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class CMDashboardProjectUpdate : System.Web.UI.Page
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
            lblZoneH.Text = Session["Default_Zone"].ToString();
            lblCircleH.Text = Session["Default_Circle"].ToString();
            lblDivisionH.Text = Session["Default_Division"].ToString();
            get_tbl_Zone();
            if (Session["UserType"].ToString() == "4" && Convert.ToInt32(Session["PersonJuridiction_ZoneId"].ToString()) > 0)
            {//Zone
                try
                {
                    ddlSearchZone.SelectedValue = Session["PersonJuridiction_ZoneId"].ToString();
                    ddlSearchZone_SelectedIndexChanged(ddlSearchZone, e);
                    ddlSearchZone.Enabled = false;
                }
                catch
                { }
            }
            if (Session["UserType"].ToString() == "6" && Convert.ToInt32(Session["PersonJuridiction_ZoneId"].ToString()) > 0)
            {
                try
                {
                    ddlSearchZone.SelectedValue = Session["PersonJuridiction_ZoneId"].ToString();
                    ddlSearchZone_SelectedIndexChanged(ddlSearchZone, e);
                    ddlSearchZone.Enabled = false;
                    if (Session["UserType"].ToString() == "6" && Convert.ToInt32(Session["PersonJuridiction_CircleId"].ToString()) > 0)
                    {//Circle
                        try
                        {
                            ddlSearchCircle.SelectedValue = Session["PersonJuridiction_CircleId"].ToString();
                            ddlSearchCircle_SelectedIndexChanged(ddlSearchCircle, e);
                            ddlSearchCircle.Enabled = false;
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
                    ddlSearchZone.SelectedValue = Session["PersonJuridiction_ZoneId"].ToString();
                    ddlSearchZone_SelectedIndexChanged(ddlSearchZone, e);
                    ddlSearchZone.Enabled = false;
                    if (Session["UserType"].ToString() == "7" && Convert.ToInt32(Session["PersonJuridiction_CircleId"].ToString()) > 0)
                    {//Circle
                        try
                        {
                            ddlSearchCircle.SelectedValue = Session["PersonJuridiction_CircleId"].ToString();
                            ddlSearchCircle_SelectedIndexChanged(ddlSearchCircle, e);
                            ddlSearchCircle.Enabled = false;
                            if (Session["UserType"].ToString() == "7" && Convert.ToInt32(Session["PersonJuridiction_DivisionId"].ToString()) > 0)
                            {//Circle
                                try
                                {
                                    ddlsearchDivision.SelectedValue = Session["PersonJuridiction_DivisionId"].ToString();
                                    ddlsearchDivision.Enabled = false;
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
            if (ddlSearchZone.SelectedValue != "0")
            {
                get_tbl_ProjectWork();
            }
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

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        get_tbl_ProjectWork();
    }

    protected void get_tbl_ProjectWork()
    {
        int District_Id = 0;
        int Zone_Id = 0;
        int Circle_Id = 0;
        int Division_Id = 0;
        int ULB_Id = 0;
        string Scheme_Id = "1016";

        try
        {
            Zone_Id = Convert.ToInt32(ddlSearchZone.SelectedValue);
        }
        catch
        {
            Zone_Id = 0;
        }
        try
        {
            Circle_Id = Convert.ToInt32(ddlSearchCircle.SelectedValue);
        }
        catch
        {
            Circle_Id = 0;
        }
        try
        {
            Division_Id = Convert.ToInt32(ddlsearchDivision.SelectedValue);
        }
        catch
        {
            Division_Id = 0;
        }
        DataSet ds = new DataSet();
        ds = (new DataLayer()).get_tbl_ProjectWork(Scheme_Id, District_Id, Zone_Id, Circle_Id, Division_Id, ULB_Id, "", 0, txtProjectCode.Text.Trim(), 0);
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
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

    private void get_tbl_Zone()
    {
        DataSet ds = new DataSet();
        ds = (new DataLayer()).get_tbl_Zone();
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            AllClasses.FillDropDown(ds.Tables[0], ddlSearchZone, "Zone_Name", "Zone_Id");
        }
        else
        {
            ddlSearchZone.Items.Clear();
        }
    }

    private void get_tbl_Circle_Search(int Zone_Id)
    {
        DataSet ds = new DataSet();
        ds = (new DataLayer()).get_tbl_Circle(Zone_Id);
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            AllClasses.FillDropDown(ds.Tables[0], ddlSearchCircle, "Circle_Name", "Circle_Id");
        }
        else
        {
            ddlSearchCircle.Items.Clear();
        }
    }
    private void get_tbl_Division_Search(int Circle_Id)
    {
        DataSet ds = new DataSet();
        ds = (new DataLayer()).get_tbl_Division(Circle_Id);
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            AllClasses.FillDropDown(ds.Tables[0], ddlsearchDivision, "Division_Name", "Division_Id");
        }
        else
        {
            ddlsearchDivision.Items.Clear();
        }
    }

    protected void btnEdit_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton lnkUpdate = sender as ImageButton;
        GridViewRow gr = lnkUpdate.Parent.Parent as GridViewRow;
        int ProjectWork_Id = Convert.ToInt32(gr.Cells[0].Text.Trim());
        hf_ProjectWork_Id.Value = ProjectWork_Id.ToString();
        string StartDate = gr.Cells[19].Text.Trim().Replace("&nbsp;", "");
        string EndDate = gr.Cells[22].Text.Trim().Replace("&nbsp;", "");
        string Sanction_Cost = gr.Cells[13].Text.Trim().Replace("&nbsp;", "");
        string TenderCost = gr.Cells[14].Text.Trim().Replace("&nbsp;", "");

        hf_SanctionCost.Value = Sanction_Cost;
        hf_TenderCost.Value = TenderCost;

        if (EndDate == "01/01/1900")
        {
            EndDate = gr.Cells[21].Text.Trim().Replace("&nbsp;", "");
        }
        if (StartDate == "" || StartDate == "01/01/1900" || EndDate == "" || EndDate == "01/01/1900")
        {
            MessageBox.Show("Project Start Date and End Date are Not Properly Configured. Please Update package Details.");
            return;
        }
        get_tbl_ProjectTarget(ProjectWork_Id, StartDate, EndDate);
    }
    protected void get_tbl_ProjectTarget(int ProjectWork_Id, string StartDate, string EndDate)
    {
        DataSet ds = new DataSet();
        ds = (new DataLayer()).get_tbl_ProjectTarget(ProjectWork_Id, StartDate, EndDate);
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            divEntry.Visible = true;
            grdProjectTargets.DataSource = ds.Tables[0];
            grdProjectTargets.DataBind();

            TextBox txtPhysicalTarget = grdProjectTargets.Rows[grdProjectTargets.Rows.Count - 1].FindControl("txtPhysicalTarget") as TextBox;
            //TextBox txtFinancialTarget = grdProjectTargets.Rows[grdProjectTargets.Rows.Count - 1].FindControl("txtFinancialTarget") as TextBox;


            //txtFinancialTarget.Text = hf_TenderCost.Value.ToString();
            //txtFinancialTarget.Enabled = false;

            txtPhysicalTarget.Text = "100";
            txtPhysicalTarget.Enabled = false;
        }
        else
        {
            divEntry.Visible = false;
            grdProjectTargets.DataSource = null;
            grdProjectTargets.DataBind();
        }
    }
    protected void grdPost_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[8].Text = Session["Default_Circle"].ToString();
            e.Row.Cells[9].Text = Session["Default_Division"].ToString();
        }
    }

    protected void ddlSearchZone_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlSearchZone.SelectedValue == "0")
        {
            ddlSearchCircle.Items.Clear();
            ddlsearchDivision.Items.Clear();
        }
        else
        {
            get_tbl_Circle_Search(Convert.ToInt32(ddlSearchZone.SelectedValue));
        }
    }

    protected void ddlSearchCircle_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlSearchCircle.SelectedValue == "0")
        {
            ddlsearchDivision.Items.Clear();
        }
        else
        {
            get_tbl_Division_Search(Convert.ToInt32(ddlSearchCircle.SelectedValue));
        }
    }

    protected void grdProjectTargets_PreRender(object sender, EventArgs e)
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

    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        if (hf_ProjectWork_Id.Value == "" || hf_ProjectWork_Id.Value == "0")
        {
            MessageBox.Show("Please Select A Project To Update Target Details");
            return;
        }
        decimal FinancialTarget_Total = 0;
        decimal Sanction_Cost = 0;
        decimal Tender_Cost = 0;
        try
        {
            Sanction_Cost = Convert.ToDecimal(hf_SanctionCost.Value);
        }
        catch
        {
            Sanction_Cost = 0;
        }
        try
        {
            Tender_Cost = Convert.ToDecimal(hf_TenderCost.Value);
        }
        catch
        {
            Tender_Cost = 0;
        }
        List<tbl_ProjectTarget> obj_tbl_ProjectTarget_Li = new List<tbl_ProjectTarget>();
        decimal PhysicalTarget_Prev = 0;
        for (int i = 0; i < grdProjectTargets.Rows.Count; i++)
        {
            tbl_ProjectTarget obj_tbl_ProjectTarget = new tbl_ProjectTarget();
            int ProjectWork_Id = Convert.ToInt32(hf_ProjectWork_Id.Value);
            TextBox txtPhysicalTarget = grdProjectTargets.Rows[i].FindControl("txtPhysicalTarget") as TextBox;
            TextBox txtFinancialTarget = grdProjectTargets.Rows[i].FindControl("txtFinancialTarget") as TextBox;

            if (i == 0)
            { }
            else
            {
                TextBox txtPhysicalTarget_Prev = grdProjectTargets.Rows[i - 1].FindControl("txtPhysicalTarget") as TextBox;
                try
                {
                    PhysicalTarget_Prev = Convert.ToDecimal(txtPhysicalTarget_Prev.Text.Trim());
                }
                catch
                {
                    PhysicalTarget_Prev = 0;
                }
            }
            obj_tbl_ProjectTarget.ProjectTarget_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
            obj_tbl_ProjectTarget.ProjectTarget_MonthName = grdProjectTargets.Rows[i].Cells[1].Text.Trim();
            obj_tbl_ProjectTarget.ProjectTarget_MonthNumber = Convert.ToInt32(grdProjectTargets.Rows[i].Cells[2].Text.Trim());
            obj_tbl_ProjectTarget.ProjectTarget_MonthYear = Convert.ToInt32(grdProjectTargets.Rows[i].Cells[3].Text.Trim());
            obj_tbl_ProjectTarget.ProjectTarget_LastDayOfMonth = Convert.ToInt32(grdProjectTargets.Rows[i].Cells[4].Text.Trim());
            obj_tbl_ProjectTarget.ProjectTarget_Date = grdProjectTargets.Rows[i].Cells[5].Text.Trim();
            obj_tbl_ProjectTarget.ProjectTarget_ProjectWork_Id = ProjectWork_Id;
            try
            {
                obj_tbl_ProjectTarget.ProjectTarget_PhysicalTarget = Convert.ToDecimal(txtPhysicalTarget.Text.Trim());
            }
            catch
            {
                obj_tbl_ProjectTarget.ProjectTarget_PhysicalTarget = 0;
            }
            try
            {
                obj_tbl_ProjectTarget.ProjectTarget_FinancialTarget = Convert.ToDecimal(txtFinancialTarget.Text.Trim());
            }
            catch
            {
                obj_tbl_ProjectTarget.ProjectTarget_FinancialTarget = 0;
            }
            if (obj_tbl_ProjectTarget.ProjectTarget_PhysicalTarget < PhysicalTarget_Prev)
            {
                MessageBox.Show("Physical Target For Month " + grdProjectTargets.Rows[i].Cells[7].Text.Trim() + " should be equal or more than " + PhysicalTarget_Prev.ToString());
                txtPhysicalTarget.Focus();
                return;
            }
            FinancialTarget_Total += obj_tbl_ProjectTarget.ProjectTarget_FinancialTarget;
            obj_tbl_ProjectTarget.ProjectTarget_Status = 1;
            obj_tbl_ProjectTarget_Li.Add(obj_tbl_ProjectTarget);
        }
        if (FinancialTarget_Total > Sanction_Cost)
        {
            MessageBox.Show("Total Financial Target Should Not Exceed Sanction Cost ie " + Sanction_Cost.ToString());
            return;
        }
        //if (FinancialTarget_Total < Tender_Cost)
        //{
        //    MessageBox.Show("Total Financial Target Should Not be Less Than Tender Cost ie " + Tender_Cost.ToString());
        //    return;
        //}
        if (new DataLayer().Insert_tbl_ProjectTarget(obj_tbl_ProjectTarget_Li))
        {
            MessageBox.Show("Physical and Financial Targets Details Updated Successfully");
            return;
        }
        else
        {
            MessageBox.Show("Error In Updating Details");
            return;
        }
    }
}