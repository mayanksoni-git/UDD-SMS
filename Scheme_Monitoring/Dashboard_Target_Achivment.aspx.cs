using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

public partial class Dashboard_Target_Achivment : System.Web.UI.Page
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
    protected void btnFilter_Click(object sender, ImageClickEventArgs e)
    {
        mp1.Show();
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
    private void get_tbl_DivisionFinancialTarget(int Year, int Zone_Id, int Circle_Id, int Division_Id, string Order_By)
    {
        DataSet ds = new DataSet();
        ds = (new DataLayer()).get_tbl_DivisionFinancialTarget(Year, Zone_Id, Circle_Id, Division_Id, Order_By);
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            grdPost.DataSource = ds.Tables[0];
            grdPost.DataBind();

            grdPost.FooterRow.Cells[6].Text = ds.Tables[0].Compute("sum(DivisionFinancialTarget_YearTarget)", "").ToString();
            grdPost.FooterRow.Cells[7].Text = ds.Tables[0].Compute("sum(DivisionFinancialTarget_YearAchivment)", "").ToString();
            decimal YearTarget = 0;
            decimal YearAchivment = 0;
            try
            {
                YearTarget = decimal.Parse(grdPost.FooterRow.Cells[6].Text);
            }
            catch
            {
                YearTarget = 0;
            }
            try
            {
                YearAchivment = decimal.Parse(grdPost.FooterRow.Cells[7].Text);
            }
            catch
            {
                YearAchivment = 0;
            }
            decimal YearAchivmentPer = 0;
            if (YearTarget == 0)
            {
                YearAchivmentPer = 0;
            }
            else
            {
                YearAchivmentPer = decimal.Round(YearAchivment * 100 / YearTarget, 2, MidpointRounding.AwayFromZero);
            }
            grdPost.FooterRow.Cells[8].Text = YearAchivmentPer.ToString();

            grdPost.FooterRow.Cells[9].Text = ds.Tables[0].Compute("sum(DivisionFinancialTarget_Q1Target)", "").ToString();
            grdPost.FooterRow.Cells[10].Text = ds.Tables[0].Compute("sum(DivisionFinancialTarget_Q1Achivment)", "").ToString();
            YearTarget = 0;
            YearAchivment = 0;
            try
            {
                YearTarget = decimal.Parse(grdPost.FooterRow.Cells[9].Text);
            }
            catch
            {
                YearTarget = 0;
            }
            try
            {
                YearAchivment = decimal.Parse(grdPost.FooterRow.Cells[10].Text);
            }
            catch
            {
                YearAchivment = 0;
            }
            YearAchivmentPer = 0;
            if (YearTarget == 0)
            {
                YearAchivmentPer = 0;
            }
            else
            {
                YearAchivmentPer = decimal.Round(YearAchivment * 100 / YearTarget, 2, MidpointRounding.AwayFromZero);
            }
            grdPost.FooterRow.Cells[11].Text = YearAchivmentPer.ToString();

            grdPost.FooterRow.Cells[12].Text = ds.Tables[0].Compute("sum(DivisionFinancialTarget_Q2Target)", "").ToString();
            grdPost.FooterRow.Cells[13].Text = ds.Tables[0].Compute("sum(DivisionFinancialTarget_Q2Achivment)", "").ToString();
            YearTarget = 0;
            YearAchivment = 0;
            try
            {
                YearTarget = decimal.Parse(grdPost.FooterRow.Cells[12].Text);
            }
            catch
            {
                YearTarget = 0;
            }
            try
            {
                YearAchivment = decimal.Parse(grdPost.FooterRow.Cells[13].Text);
            }
            catch
            {
                YearAchivment = 0;
            }
            YearAchivmentPer = 0;
            if (YearTarget == 0)
            {
                YearAchivmentPer = 0;
            }
            else
            {
                YearAchivmentPer = decimal.Round(YearAchivment * 100 / YearTarget, 2, MidpointRounding.AwayFromZero);
            }
            grdPost.FooterRow.Cells[14].Text = YearAchivmentPer.ToString();

            grdPost.FooterRow.Cells[15].Text = ds.Tables[0].Compute("sum(DivisionFinancialTarget_Q3Target)", "").ToString();
            grdPost.FooterRow.Cells[16].Text = ds.Tables[0].Compute("sum(DivisionFinancialTarget_Q3Achivment)", "").ToString();
            YearTarget = 0;
            YearAchivment = 0;
            try
            {
                YearTarget = decimal.Parse(grdPost.FooterRow.Cells[15].Text);
            }
            catch
            {
                YearTarget = 0;
            }
            try
            {
                YearAchivment = decimal.Parse(grdPost.FooterRow.Cells[16].Text);
            }
            catch
            {
                YearAchivment = 0;
            }
            YearAchivmentPer = 0;
            if (YearTarget == 0)
            {
                YearAchivmentPer = 0;
            }
            else
            {
                YearAchivmentPer = decimal.Round(YearAchivment * 100 / YearTarget, 2, MidpointRounding.AwayFromZero);
            }
            grdPost.FooterRow.Cells[17].Text = YearAchivmentPer.ToString();

            grdPost.FooterRow.Cells[18].Text = ds.Tables[0].Compute("sum(DivisionFinancialTarget_Q4Target)", "").ToString();
            grdPost.FooterRow.Cells[19].Text = ds.Tables[0].Compute("sum(DivisionFinancialTarget_Q4Achivment)", "").ToString();
            YearTarget = 0;
            YearAchivment = 0;
            try
            {
                YearTarget = decimal.Parse(grdPost.FooterRow.Cells[18].Text);
            }
            catch
            {
                YearTarget = 0;
            }
            try
            {
                YearAchivment = decimal.Parse(grdPost.FooterRow.Cells[19].Text);
            }
            catch
            {
                YearAchivment = 0;
            }
            YearAchivmentPer = 0;
            if (YearTarget == 0)
            {
                YearAchivmentPer = 0;
            }
            else
            {
                YearAchivmentPer = decimal.Round(YearAchivment * 100 / YearTarget, 2, MidpointRounding.AwayFromZero);
            }
            grdPost.FooterRow.Cells[20].Text = YearAchivmentPer.ToString();
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

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        loadData("Unit", "Unit");
    }

    private void loadData(string Order_By_P, string Order_By_F)
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
        get_tbl_DivisionFinancialTarget(Convert.ToInt32(txtYear.Text), Zone_Id, Circle_Id, Division_Id, Order_By_F);
        get_tbl_DivisionPhysicalTarget(Convert.ToInt32(txtYear.Text), Zone_Id, Circle_Id, Division_Id, Order_By_P);
    }

    private void get_tbl_DivisionPhysicalTarget(int Year, int Zone_Id, int Circle_Id, int Division_Id, string Order_By)
    {
        DataSet ds = new DataSet();
        ds = (new DataLayer()).get_tbl_DivisionPhysicalTarget(Year, Zone_Id, Circle_Id, Division_Id, Order_By);
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            grdPhysical.DataSource = ds.Tables[0];
            grdPhysical.DataBind();

            grdPhysical.FooterRow.Cells[6].Text = ds.Tables[0].Compute("sum(DivisionPhysicalTarget_PhysicalCompletionTarget)", "").ToString();
            grdPhysical.FooterRow.Cells[7].Text = ds.Tables[0].Compute("sum(DivisionPhysicalTarget_PhysicalCompletionAchivment)", "").ToString();
            decimal YearTarget = 0;
            decimal YearAchivment = 0;
            try
            {
                YearTarget = decimal.Parse(grdPhysical.FooterRow.Cells[6].Text);
            }
            catch
            {
                YearTarget = 0;
            }
            try
            {
                YearAchivment = decimal.Parse(grdPhysical.FooterRow.Cells[7].Text);
            }
            catch
            {
                YearAchivment = 0;
            }
            decimal YearAchivmentPer = 0;
            if (YearTarget == 0)
            {
                YearAchivmentPer = 0;
            }
            else
            {
                YearAchivmentPer = decimal.Round(YearAchivment * 100 / YearTarget, 2, MidpointRounding.AwayFromZero);
            }
            grdPhysical.FooterRow.Cells[8].Text = YearAchivmentPer.ToString();

            grdPhysical.FooterRow.Cells[9].Text = ds.Tables[0].Compute("sum(DivisionPhysicalTarget_PhysicalHandoverTarget)", "").ToString();
            grdPhysical.FooterRow.Cells[10].Text = ds.Tables[0].Compute("sum(DivisionPhysicalTarget_PhysicalHandoverAchivment)", "").ToString();
            YearTarget = 0;
            YearAchivment = 0;
            try
            {
                YearTarget = decimal.Parse(grdPhysical.FooterRow.Cells[9].Text);
            }
            catch
            {
                YearTarget = 0;
            }
            try
            {
                YearAchivment = decimal.Parse(grdPhysical.FooterRow.Cells[10].Text);
            }
            catch
            {
                YearAchivment = 0;
            }
            YearAchivmentPer = 0;
            if (YearTarget == 0)
            {
                YearAchivmentPer = 0;
            }
            else
            {
                YearAchivmentPer = decimal.Round(YearAchivment * 100 / YearTarget, 2, MidpointRounding.AwayFromZero);
            }
            grdPhysical.FooterRow.Cells[11].Text = YearAchivmentPer.ToString();

            grdPhysical.FooterRow.Cells[12].Text = ds.Tables[0].Compute("sum(DivisionPhysicalTarget_FinancialHandoverTarget)", "").ToString();
            grdPhysical.FooterRow.Cells[13].Text = ds.Tables[0].Compute("sum(DivisionPhysicalTarget_FinancialHandoverAchivment)", "").ToString();
            YearTarget = 0;
            YearAchivment = 0;
            try
            {
                YearTarget = decimal.Parse(grdPhysical.FooterRow.Cells[12].Text);
            }
            catch
            {
                YearTarget = 0;
            }
            try
            {
                YearAchivment = decimal.Parse(grdPhysical.FooterRow.Cells[13].Text);
            }
            catch
            {
                YearAchivment = 0;
            }
            YearAchivmentPer = 0;
            if (YearTarget == 0)
            {
                YearAchivmentPer = 0;
            }
            else
            {
                YearAchivmentPer = decimal.Round(YearAchivment * 100 / YearTarget, 2, MidpointRounding.AwayFromZero);
            }
            grdPhysical.FooterRow.Cells[14].Text = YearAchivmentPer.ToString();
        }
        else
        {
            grdPhysical.DataSource = null;
            grdPhysical.DataBind();
        }
    }
    protected void grdPost_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[3].Text = Session["Default_Zone"].ToString();
            e.Row.Cells[4].Text = Session["Default_Circle"].ToString();
            e.Row.Cells[5].Text = Session["Default_Division"].ToString();

            e.Row.Cells[6].BackColor = ColorTranslator.FromHtml("#f15a25");
            e.Row.Cells[6].ForeColor = Color.White;
            e.Row.Cells[7].BackColor = ColorTranslator.FromHtml("#f15a25");
            e.Row.Cells[7].ForeColor = Color.White;
            e.Row.Cells[8].BackColor = ColorTranslator.FromHtml("#f15a25");
            e.Row.Cells[8].ForeColor = Color.White;

            e.Row.Cells[9].BackColor = ColorTranslator.FromHtml("#39beb9");
            e.Row.Cells[9].ForeColor = Color.White;
            e.Row.Cells[10].BackColor = ColorTranslator.FromHtml("#39beb9");
            e.Row.Cells[10].ForeColor = Color.White;
            e.Row.Cells[11].BackColor = ColorTranslator.FromHtml("#39beb9");
            e.Row.Cells[11].ForeColor = Color.White;

            e.Row.Cells[12].BackColor = ColorTranslator.FromHtml("#f2ad22");
            e.Row.Cells[12].ForeColor = Color.White;
            e.Row.Cells[13].BackColor = ColorTranslator.FromHtml("#f2ad22");
            e.Row.Cells[13].ForeColor = Color.White;
            e.Row.Cells[14].BackColor = ColorTranslator.FromHtml("#f2ad22");
            e.Row.Cells[14].ForeColor = Color.White;

            e.Row.Cells[15].BackColor = ColorTranslator.FromHtml("#bcd63d");
            e.Row.Cells[15].ForeColor = Color.White;
            e.Row.Cells[16].BackColor = ColorTranslator.FromHtml("#bcd63d");
            e.Row.Cells[16].ForeColor = Color.White;
            e.Row.Cells[17].BackColor = ColorTranslator.FromHtml("#bcd63d");
            e.Row.Cells[17].ForeColor = Color.White;

            e.Row.Cells[18].BackColor = ColorTranslator.FromHtml("#5360cf");
            e.Row.Cells[18].ForeColor = Color.White;
            e.Row.Cells[19].BackColor = ColorTranslator.FromHtml("#5360cf");
            e.Row.Cells[19].ForeColor = Color.White;
            e.Row.Cells[20].BackColor = ColorTranslator.FromHtml("#5360cf");
            e.Row.Cells[20].ForeColor = Color.White;
        }
    }

    protected void grdPhysical_PreRender(object sender, EventArgs e)
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

    protected void grdPhysical_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[3].Text = Session["Default_Zone"].ToString();
            e.Row.Cells[4].Text = Session["Default_Circle"].ToString();
            e.Row.Cells[5].Text = Session["Default_Division"].ToString();

            e.Row.Cells[6].BackColor = Color.FromArgb(255, 153, 51);
            e.Row.Cells[6].ForeColor = Color.White;
            e.Row.Cells[7].BackColor = Color.FromArgb(255, 153, 51);
            e.Row.Cells[7].ForeColor = Color.White;
            e.Row.Cells[8].BackColor = Color.FromArgb(255, 153, 51);
            e.Row.Cells[8].ForeColor = Color.White;

            e.Row.Cells[12].BackColor = Color.Green;
            e.Row.Cells[12].ForeColor = Color.White;
            e.Row.Cells[13].BackColor = Color.Green;
            e.Row.Cells[13].ForeColor = Color.White;
            e.Row.Cells[14].BackColor = Color.Green;
            e.Row.Cells[14].ForeColor = Color.White;
        }
        if (e.Row.RowType == DataControlRowType.DataRow)
        {

        }
    }

    protected void btnPhysicalView_Click(object sender, ImageClickEventArgs e)
    {
        btnPhysicalView.Style.Value = "border:6px solid #555;";
        btnFinancialView.Style.Value = "border:2px solid #ff0000;";
        divPhysical.Visible = true;
        divFinancial.Visible = false;

        btnWorkCompletion.Style.Value = "border:2px solid orange;";
        btnWorkPhysicalClosure.Style.Value = "border:2px solid white;";
        btnFinancialHandover.Style.Value = "border:2px solid green;";

        grdPhysical.Columns[6].Visible = true;
        grdPhysical.Columns[7].Visible = true;
        grdPhysical.Columns[8].Visible = true;

        grdPhysical.Columns[9].Visible = true;
        grdPhysical.Columns[10].Visible = true;
        grdPhysical.Columns[11].Visible = true;

        grdPhysical.Columns[12].Visible = true;
        grdPhysical.Columns[13].Visible = true;
        grdPhysical.Columns[14].Visible = true;

        divPhysicalOptions.Visible = true;
        divFinancialOptions.Visible = false;

        loadData("Unit", "Unit");
    }

    protected void btnFinancialView_Click(object sender, ImageClickEventArgs e)
    {
        btnPhysicalView.Style.Value = "border:2px solid #555;";
        btnFinancialView.Style.Value = "border:6px solid #ff0000;";
        divPhysical.Visible = false;
        divFinancial.Visible = true;

        grdPost.Columns[6].Visible = true;
        grdPost.Columns[7].Visible = true;
        grdPost.Columns[8].Visible = true;

        grdPost.Columns[9].Visible = true;
        grdPost.Columns[10].Visible = true;
        grdPost.Columns[11].Visible = true;

        grdPost.Columns[12].Visible = true;
        grdPost.Columns[13].Visible = true;
        grdPost.Columns[14].Visible = true;

        grdPost.Columns[15].Visible = true;
        grdPost.Columns[16].Visible = true;
        grdPost.Columns[17].Visible = true;

        grdPost.Columns[18].Visible = true;
        grdPost.Columns[19].Visible = true;
        grdPost.Columns[20].Visible = true;

        divPhysicalOptions.Visible = false;
        divFinancialOptions.Visible = true;

        loadData("Unit", "Unit");
    }

    protected void btnWorkCompletion_Click(object sender, ImageClickEventArgs e)
    {
        btnWorkCompletion.Style.Value = "border:6px solid orange;";
        btnWorkPhysicalClosure.Style.Value = "border:2px solid white;";
        btnFinancialHandover.Style.Value = "border:2px solid green;";

        grdPhysical.Columns[6].Visible = true;
        grdPhysical.Columns[7].Visible = true;
        grdPhysical.Columns[8].Visible = true;

        grdPhysical.Columns[9].Visible = false;
        grdPhysical.Columns[10].Visible = false;
        grdPhysical.Columns[11].Visible = false;

        grdPhysical.Columns[12].Visible = false;
        grdPhysical.Columns[13].Visible = false;
        grdPhysical.Columns[14].Visible = false;

        loadData("Physical", "Unit");
    }

    protected void btnWorkPhysicalClosure_Click(object sender, ImageClickEventArgs e)
    {
        btnWorkCompletion.Style.Value = "border:2px solid orange;";
        btnWorkPhysicalClosure.Style.Value = "border:6px solid white;";
        btnFinancialHandover.Style.Value = "border:2px solid green;";

        grdPhysical.Columns[6].Visible = false;
        grdPhysical.Columns[7].Visible = false;
        grdPhysical.Columns[8].Visible = false;

        grdPhysical.Columns[9].Visible = true;
        grdPhysical.Columns[10].Visible = true;
        grdPhysical.Columns[11].Visible = true;

        grdPhysical.Columns[12].Visible = false;
        grdPhysical.Columns[13].Visible = false;
        grdPhysical.Columns[14].Visible = false;

        loadData("Handover", "Unit");
    }

    protected void btnFinancialHandover_Click(object sender, ImageClickEventArgs e)
    {
        btnWorkCompletion.Style.Value = "border:2px solid orange;";
        btnWorkPhysicalClosure.Style.Value = "border:2px solid white;";
        btnFinancialHandover.Style.Value = "border:6px solid green;";

        grdPhysical.Columns[6].Visible = false;
        grdPhysical.Columns[7].Visible = false;
        grdPhysical.Columns[8].Visible = false;

        grdPhysical.Columns[9].Visible = false;
        grdPhysical.Columns[10].Visible = false;
        grdPhysical.Columns[11].Visible = false;

        grdPhysical.Columns[12].Visible = true;
        grdPhysical.Columns[13].Visible = true;
        grdPhysical.Columns[14].Visible = true;

        loadData("Financial", "Unit");
    }

    protected void lnkQ1_Click(object sender, EventArgs e)
    {
        grdPost.Columns[6].Visible = false;
        grdPost.Columns[7].Visible = false;
        grdPost.Columns[8].Visible = false;

        grdPost.Columns[9].Visible = true;
        grdPost.Columns[10].Visible = true;
        grdPost.Columns[11].Visible = true;

        grdPost.Columns[12].Visible = false;
        grdPost.Columns[13].Visible = false;
        grdPost.Columns[14].Visible = false;

        grdPost.Columns[15].Visible = false;
        grdPost.Columns[16].Visible = false;
        grdPost.Columns[17].Visible = false;

        grdPost.Columns[18].Visible = false;
        grdPost.Columns[19].Visible = false;
        grdPost.Columns[20].Visible = false;

        loadData("Unit", "Q1");
    }

    protected void lnkQ2_Click(object sender, EventArgs e)
    {
        grdPost.Columns[6].Visible = false;
        grdPost.Columns[7].Visible = false;
        grdPost.Columns[8].Visible = false;

        grdPost.Columns[9].Visible = false;
        grdPost.Columns[10].Visible = false;
        grdPost.Columns[11].Visible = false;

        grdPost.Columns[12].Visible = true;
        grdPost.Columns[13].Visible = true;
        grdPost.Columns[14].Visible = true;

        grdPost.Columns[15].Visible = false;
        grdPost.Columns[16].Visible = false;
        grdPost.Columns[17].Visible = false;

        grdPost.Columns[18].Visible = false;
        grdPost.Columns[19].Visible = false;
        grdPost.Columns[20].Visible = false;
        loadData("Unit", "Q2");
    }

    protected void lnkQ3_Click(object sender, EventArgs e)
    {
        grdPost.Columns[6].Visible = false;
        grdPost.Columns[7].Visible = false;
        grdPost.Columns[8].Visible = false;

        grdPost.Columns[9].Visible = false;
        grdPost.Columns[10].Visible = false;
        grdPost.Columns[11].Visible = false;

        grdPost.Columns[12].Visible = false;
        grdPost.Columns[13].Visible = false;
        grdPost.Columns[14].Visible = false;

        grdPost.Columns[15].Visible = true;
        grdPost.Columns[16].Visible = true;
        grdPost.Columns[17].Visible = true;

        grdPost.Columns[18].Visible = false;
        grdPost.Columns[19].Visible = false;
        grdPost.Columns[20].Visible = false;
        loadData("Unit", "Q3");
    }

    protected void lnkQ4_Click(object sender, EventArgs e)
    {
        grdPost.Columns[6].Visible = false;
        grdPost.Columns[7].Visible = false;
        grdPost.Columns[8].Visible = false;

        grdPost.Columns[9].Visible = false;
        grdPost.Columns[10].Visible = false;
        grdPost.Columns[11].Visible = false;

        grdPost.Columns[12].Visible = false;
        grdPost.Columns[13].Visible = false;
        grdPost.Columns[14].Visible = false;

        grdPost.Columns[15].Visible = false;
        grdPost.Columns[16].Visible = false;
        grdPost.Columns[17].Visible = false;

        grdPost.Columns[18].Visible = true;
        grdPost.Columns[19].Visible = true;
        grdPost.Columns[20].Visible = true;
        loadData("Unit", "Q4");
    }

    protected void lnkOverAll_Click(object sender, EventArgs e)
    {
        grdPost.Columns[6].Visible = true;
        grdPost.Columns[7].Visible = true;
        grdPost.Columns[8].Visible = true;

        grdPost.Columns[9].Visible = false;
        grdPost.Columns[10].Visible = false;
        grdPost.Columns[11].Visible = false;

        grdPost.Columns[12].Visible = false;
        grdPost.Columns[13].Visible = false;
        grdPost.Columns[14].Visible = false;

        grdPost.Columns[15].Visible = false;
        grdPost.Columns[16].Visible = false;
        grdPost.Columns[17].Visible = false;

        grdPost.Columns[18].Visible = false;
        grdPost.Columns[19].Visible = false;
        grdPost.Columns[20].Visible = false;
        loadData("Unit", "YearAchivment");
    }
}
