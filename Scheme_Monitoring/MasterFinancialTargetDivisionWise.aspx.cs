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

public partial class MasterFinancialTargetDivisionWise : System.Web.UI.Page
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
    private void get_tbl_DivisionFinancialTarget(int Year, int Zone_Id, int Circle_Id, int Division_Id)
    {
        DataSet ds = new DataSet();
        ds = (new DataLayer()).get_tbl_DivisionFinancialTarget(Year, Zone_Id, Circle_Id, Division_Id, "Unit");
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            grdPost.DataSource = ds.Tables[0];
            grdPost.DataBind();

            grdPost.FooterRow.Cells[6].Text = ds.Tables[0].Compute("sum(DivisionFinancialTarget_YearTarget)", "").ToString();

            grdPost.FooterRow.Cells[7].Text = ds.Tables[0].Compute("sum(DivisionFinancialTarget_Q1Target)", "").ToString();
            grdPost.FooterRow.Cells[8].Text = ds.Tables[0].Compute("sum(DivisionFinancialTarget_Q1Achivment)", "").ToString();

            grdPost.FooterRow.Cells[9].Text = ds.Tables[0].Compute("sum(DivisionFinancialTarget_Q2Target)", "").ToString();
            grdPost.FooterRow.Cells[10].Text = ds.Tables[0].Compute("sum(DivisionFinancialTarget_Q2Achivment)", "").ToString();

            grdPost.FooterRow.Cells[11].Text = ds.Tables[0].Compute("sum(DivisionFinancialTarget_Q3Target)", "").ToString();
            grdPost.FooterRow.Cells[12].Text = ds.Tables[0].Compute("sum(DivisionFinancialTarget_Q3Achivment)", "").ToString();

            grdPost.FooterRow.Cells[13].Text = ds.Tables[0].Compute("sum(DivisionFinancialTarget_Q4Target)", "").ToString();
            grdPost.FooterRow.Cells[14].Text = ds.Tables[0].Compute("sum(DivisionFinancialTarget_Q4Achivment)", "").ToString();
        }
        else
        {
            grdPost.DataSource = null;
            grdPost.DataBind();
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        List<tbl_DivisionFinancialTarget> obj_tbl_DivisionFinancialTarget_Li = new List<tbl_DivisionFinancialTarget>();

        for (int i = 0; i < grdPost.Rows.Count; i++)
        {
            TextBox txtFinancialTarget = grdPost.Rows[i].FindControl("txtFinancialTarget") as TextBox;
            TextBox txtFinancialTargetQ1 = grdPost.Rows[i].FindControl("txtFinancialTargetQ1") as TextBox;
            TextBox txtFinancialTargetQ2 = grdPost.Rows[i].FindControl("txtFinancialTargetQ2") as TextBox;
            TextBox txtFinancialTargetQ3 = grdPost.Rows[i].FindControl("txtFinancialTargetQ3") as TextBox;
            TextBox txtFinancialTargetQ4 = grdPost.Rows[i].FindControl("txtFinancialTargetQ4") as TextBox;

            TextBox txtFinancialAchivmentQ1 = grdPost.Rows[i].FindControl("txtFinancialAchivmentQ1") as TextBox;
            TextBox txtFinancialAchivmentQ2 = grdPost.Rows[i].FindControl("txtFinancialAchivmentQ2") as TextBox;
            TextBox txtFinancialAchivmentQ3 = grdPost.Rows[i].FindControl("txtFinancialAchivmentQ3") as TextBox;
            TextBox txtFinancialAchivmentQ4 = grdPost.Rows[i].FindControl("txtFinancialAchivmentQ4") as TextBox;

            tbl_DivisionFinancialTarget obj_tbl_DivisionFinancialTarget = new tbl_DivisionFinancialTarget();

            obj_tbl_DivisionFinancialTarget.DivisionFinancialTarget_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
            obj_tbl_DivisionFinancialTarget.DivisionFinancialTarget_DivisionId = Convert.ToInt32(grdPost.Rows[i].Cells[1].Text.Trim());
            obj_tbl_DivisionFinancialTarget.DivisionFinancialTarget_Year = Convert.ToInt32(txtYear.Text.Trim());
            try
            {
                obj_tbl_DivisionFinancialTarget.DivisionFinancialTarget_YearTarget = Convert.ToDecimal(txtFinancialTarget.Text.Trim());
            }
            catch
            {
                obj_tbl_DivisionFinancialTarget.DivisionFinancialTarget_YearTarget = 0;
            }
            try
            {
                obj_tbl_DivisionFinancialTarget.DivisionFinancialTarget_Q1Target = Convert.ToDecimal(txtFinancialTargetQ1.Text.Trim());
            }
            catch
            {
                obj_tbl_DivisionFinancialTarget.DivisionFinancialTarget_Q1Target = 0;
            }
            try
            {
                obj_tbl_DivisionFinancialTarget.DivisionFinancialTarget_Q1Achivment = Convert.ToDecimal(txtFinancialAchivmentQ1.Text.Trim());
            }
            catch
            {
                obj_tbl_DivisionFinancialTarget.DivisionFinancialTarget_Q1Achivment = 0;
            }

            try
            {
                obj_tbl_DivisionFinancialTarget.DivisionFinancialTarget_Q2Target = Convert.ToDecimal(txtFinancialTargetQ2.Text.Trim());
            }
            catch
            {
                obj_tbl_DivisionFinancialTarget.DivisionFinancialTarget_Q2Target = 0;
            }
            try
            {
                obj_tbl_DivisionFinancialTarget.DivisionFinancialTarget_Q2Achivment = Convert.ToDecimal(txtFinancialAchivmentQ2.Text.Trim());
            }
            catch
            {
                obj_tbl_DivisionFinancialTarget.DivisionFinancialTarget_Q2Achivment = 0;
            }

            try
            {
                obj_tbl_DivisionFinancialTarget.DivisionFinancialTarget_Q3Target = Convert.ToDecimal(txtFinancialTargetQ3.Text.Trim());
            }
            catch
            {
                obj_tbl_DivisionFinancialTarget.DivisionFinancialTarget_Q3Target = 0;
            }
            try
            {
                obj_tbl_DivisionFinancialTarget.DivisionFinancialTarget_Q3Achivment = Convert.ToDecimal(txtFinancialAchivmentQ3.Text.Trim());
            }
            catch
            {
                obj_tbl_DivisionFinancialTarget.DivisionFinancialTarget_Q3Achivment = 0;
            }

            try
            {
                obj_tbl_DivisionFinancialTarget.DivisionFinancialTarget_Q4Target = Convert.ToDecimal(txtFinancialTargetQ4.Text.Trim());
            }
            catch
            {
                obj_tbl_DivisionFinancialTarget.DivisionFinancialTarget_Q4Target = 0;
            }
            try
            {
                obj_tbl_DivisionFinancialTarget.DivisionFinancialTarget_Q4Achivment = Convert.ToDecimal(txtFinancialAchivmentQ4.Text.Trim());
            }
            catch
            {
                obj_tbl_DivisionFinancialTarget.DivisionFinancialTarget_Q4Achivment = 0;
            }
            obj_tbl_DivisionFinancialTarget.DivisionFinancialTarget_Status = 1;
            obj_tbl_DivisionFinancialTarget_Li.Add(obj_tbl_DivisionFinancialTarget);
        }
        if (obj_tbl_DivisionFinancialTarget_Li.Count > 0)
        {
            if (new DataLayer().Insert_tbl_DivisionFinancialTarget(obj_tbl_DivisionFinancialTarget_Li))
            {
                MessageBox.Show("Financial Target and Achivment Created Successfully ! ");
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
        get_tbl_DivisionFinancialTarget(Convert.ToInt32(txtYear.Text), Zone_Id, Circle_Id, Division_Id);
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

    protected void txtFinancialTarget_TextChanged(object sender, EventArgs e)
    {
        GridViewRow gr = (sender as TextBox).Parent.Parent as GridViewRow;
        TextBox txtFinancialTarget = gr.FindControl("txtFinancialTarget") as TextBox;
        TextBox txtFinancialTargetQ1 = gr.FindControl("txtFinancialTargetQ1") as TextBox;
        TextBox txtFinancialTargetQ2 = gr.FindControl("txtFinancialTargetQ2") as TextBox;
        TextBox txtFinancialTargetQ3 = gr.FindControl("txtFinancialTargetQ3") as TextBox;
        TextBox txtFinancialTargetQ4 = gr.FindControl("txtFinancialTargetQ4") as TextBox;

        decimal YearTarget = 0;
        try
        {
            YearTarget = Convert.ToDecimal(txtFinancialTarget.Text.Trim());
        }
        catch
        {
            YearTarget = 0;
        }
        txtFinancialTargetQ1.Text = decimal.Round(YearTarget * Convert.ToDecimal(0.15), 2).ToString();
        txtFinancialTargetQ2.Text = decimal.Round(YearTarget * Convert.ToDecimal(0.25), 2).ToString();
        txtFinancialTargetQ3.Text = decimal.Round(YearTarget * Convert.ToDecimal(0.30), 2).ToString();
        txtFinancialTargetQ4.Text = decimal.Round(YearTarget * Convert.ToDecimal(0.30), 2).ToString();
    }
}
