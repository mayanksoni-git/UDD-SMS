using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

public partial class MasterPhysicalTargetDivisionWise : System.Web.UI.Page
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
            txtYear.Text = DateTime.Now.Year.ToString();

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
            btnSearch_Click(btnSearch, e);
        }
    }
    private void get_tbl_Zone()
    {
        DataSet ds = new DataSet();
        ds = (new DataLayer()).get_tbl_Zone();
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            AllClasses.FillDropDown(ds.Tables[0], ddlZone, "Zone_Name", "Zone_Id");
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
    private void get_tbl_DivisionPhysicalTarget(int Year, int Zone_Id, int Circle_Id, int Division_Id)
    {
        DataSet ds = new DataSet();
        ds = (new DataLayer()).get_tbl_DivisionPhysicalTarget(Year, Zone_Id, Circle_Id, Division_Id, "Unit");
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            grdPost.DataSource = ds.Tables[0];
            grdPost.DataBind();

            grdPost.FooterRow.Cells[6].Text = ds.Tables[0].Compute("sum(DivisionPhysicalTarget_PhysicalCompletionTarget)", "").ToString();
            grdPost.FooterRow.Cells[7].Text = ds.Tables[0].Compute("sum(DivisionPhysicalTarget_PhysicalCompletionAchivment)", "").ToString();

            grdPost.FooterRow.Cells[8].Text = ds.Tables[0].Compute("sum(DivisionPhysicalTarget_PhysicalHandoverTarget)", "").ToString();
            grdPost.FooterRow.Cells[9].Text = ds.Tables[0].Compute("sum(DivisionPhysicalTarget_PhysicalHandoverAchivment)", "").ToString();

            grdPost.FooterRow.Cells[10].Text = ds.Tables[0].Compute("sum(DivisionPhysicalTarget_FinancialHandoverTarget)", "").ToString();
            grdPost.FooterRow.Cells[11].Text = ds.Tables[0].Compute("sum(DivisionPhysicalTarget_FinancialHandoverAchivment)", "").ToString();
        }
        else
        {
            grdPost.DataSource = null;
            grdPost.DataBind();
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        List<tbl_DivisionPhysicalTarget> obj_tbl_DivisionPhysicalTarget_Li = new List<tbl_DivisionPhysicalTarget>();

        for (int i = 0; i < grdPost.Rows.Count; i++)
        {
            TextBox txtPhysicalCompletionTarget = grdPost.Rows[i].FindControl("txtPhysicalCompletionTarget") as TextBox;
            TextBox txtPhysicalCompletionAchivment = grdPost.Rows[i].FindControl("txtPhysicalCompletionAchivment") as TextBox;
            TextBox txtPhysicalHandoverTarget = grdPost.Rows[i].FindControl("txtPhysicalHandoverTarget") as TextBox;
            TextBox txtPhysicalHandoverAchivment = grdPost.Rows[i].FindControl("txtPhysicalHandoverAchivment") as TextBox;
            TextBox txtFinancialHandoverTarget = grdPost.Rows[i].FindControl("txtFinancialHandoverTarget") as TextBox;
            TextBox txtFinancialHandoverAchivment = grdPost.Rows[i].FindControl("txtFinancialHandoverAchivment") as TextBox;

            tbl_DivisionPhysicalTarget obj_tbl_DivisionPhysicalTarget = new tbl_DivisionPhysicalTarget();

            obj_tbl_DivisionPhysicalTarget.DivisionPhysicalTarget_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
            obj_tbl_DivisionPhysicalTarget.DivisionPhysicalTarget_DivisionId = Convert.ToInt32(grdPost.Rows[i].Cells[1].Text.Trim());
            obj_tbl_DivisionPhysicalTarget.DivisionPhysicalTarget_Year = Convert.ToInt32(txtYear.Text.Trim());
            try
            {
                obj_tbl_DivisionPhysicalTarget.DivisionPhysicalTarget_PhysicalCompletionTarget = Convert.ToInt32(txtPhysicalCompletionTarget.Text.Trim());
            }
            catch
            {
                obj_tbl_DivisionPhysicalTarget.DivisionPhysicalTarget_PhysicalCompletionTarget = 0;
            }
            try
            {
                obj_tbl_DivisionPhysicalTarget.DivisionPhysicalTarget_PhysicalCompletionAchivment = Convert.ToInt32(txtPhysicalCompletionAchivment.Text.Trim());
            }
            catch
            {
                obj_tbl_DivisionPhysicalTarget.DivisionPhysicalTarget_PhysicalCompletionAchivment = 0;
            }
            try
            {
                obj_tbl_DivisionPhysicalTarget.DivisionPhysicalTarget_PhysicalHandoverTarget = Convert.ToInt32(txtPhysicalHandoverTarget.Text.Trim());
            }
            catch
            {
                obj_tbl_DivisionPhysicalTarget.DivisionPhysicalTarget_PhysicalHandoverTarget = 0;
            }
            try
            {
                obj_tbl_DivisionPhysicalTarget.DivisionPhysicalTarget_PhysicalHandoverAchivment = Convert.ToInt32(txtPhysicalHandoverAchivment.Text.Trim());
            }
            catch
            {
                obj_tbl_DivisionPhysicalTarget.DivisionPhysicalTarget_PhysicalHandoverAchivment = 0;
            }

            try
            {
                obj_tbl_DivisionPhysicalTarget.DivisionPhysicalTarget_FinancialHandoverTarget = Convert.ToInt32(txtFinancialHandoverTarget.Text.Trim());
            }
            catch
            {
                obj_tbl_DivisionPhysicalTarget.DivisionPhysicalTarget_FinancialHandoverTarget = 0;
            }
            try
            {
                obj_tbl_DivisionPhysicalTarget.DivisionPhysicalTarget_FinancialHandoverAchivment = Convert.ToInt32(txtFinancialHandoverAchivment.Text.Trim());
            }
            catch
            {
                obj_tbl_DivisionPhysicalTarget.DivisionPhysicalTarget_FinancialHandoverAchivment = 0;
            }
            obj_tbl_DivisionPhysicalTarget.DivisionPhysicalTarget_Status = 1;
            obj_tbl_DivisionPhysicalTarget_Li.Add(obj_tbl_DivisionPhysicalTarget);
        }
        if (obj_tbl_DivisionPhysicalTarget_Li.Count > 0)
        {
            if (new DataLayer().Insert_tbl_DivisionPhysicalTarget(obj_tbl_DivisionPhysicalTarget_Li))
            {
                MessageBox.Show("Physical Target and Achivment Created Successfully ! ");
                return;
            }
            else
            {
                MessageBox.Show("Error ! ");
                return;
            }
        }
        else
        {
            MessageBox.Show("Nothing To Save");
            return;
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
        int Zone_Id = 0;
        int Circle_Id = 0;
        int Division_Id = 0;
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
        get_tbl_DivisionPhysicalTarget(Convert.ToInt32(txtYear.Text), Zone_Id, Circle_Id, Division_Id);
    }

    protected void grdPost_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[3].Text = Session["Default_Zone"].ToString();
            e.Row.Cells[4].Text = Session["Default_Circle"].ToString();
            e.Row.Cells[5].Text = Session["Default_Division"].ToString();
        }
    }
}
