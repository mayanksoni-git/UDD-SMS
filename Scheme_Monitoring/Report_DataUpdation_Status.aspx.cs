using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Report_DataUpdation_Status : System.Web.UI.Page
{
    protected void Page_PreInit(object sender, EventArgs e)
    {
        this.MasterPageFile = SetMasterPage.ReturnPage();
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            lblZoneH.Text = Session["Default_Zone"].ToString();
            lblCircleH.Text = Session["Default_Circle"].ToString();
            lblDivisionH.Text = Session["Default_Division"].ToString();

            get_tbl_Zone();
            //if (Session["UserType"].ToString() != "1")
            //{
            //    try
            //    {
            //        if (Session["PersonJuridiction_Project_Id"].ToString() != "" && Session["PersonJuridiction_Project_Id"].ToString() != "0")
            //        {
            //            ddlSearchScheme.SelectedValue = Session["PersonJuridiction_Project_Id"].ToString();
            //            ddlSearchScheme.Enabled = false;
            //        }
            //    }
            //    catch
            //    {

            //    }

            //}
            if (Session["UserType"].ToString() == "2" && Convert.ToInt32(Session["District_Id"].ToString()) > 0)
            {//District
                try
                {
                    //ddlDistrict.SelectedValue = Session["District_Id"].ToString();
                    //ddlDistrict_SelectedIndexChanged(ddlDistrict, e);
                    //ddlDistrict.Enabled = false;
                }
                catch
                { }
            }
            if (Session["UserType"].ToString() == "3" && Convert.ToInt32(Session["District_Id"].ToString()) > 0)
            {
                try
                {
                    //ddlDistrict.SelectedValue = Session["District_Id"].ToString();
                    //ddlDistrict_SelectedIndexChanged(ddlDistrict, e);
                    //ddlDistrict.Enabled = false;
                    if (Session["UserType"].ToString() == "3" && Convert.ToInt32(Session["ULB_Id"].ToString()) > 0)
                    {//ULB
                        try
                        {
                            //ddlULB.SelectedValue = Session["ULB_Id"].ToString();
                            //ddlULB.Enabled = false;
                        }
                        catch
                        { }
                    }
                }
                catch
                { }
            }
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
            int Zone_Id = 0;
            int Circle_Id = 0;
            int Division_Id = 0;

            if (Request.QueryString.Count > 0)
            {
                try
                {
                    Zone_Id = Convert.ToInt32(Request.QueryString["Zone_Id"].ToString());
                    ddlZone.SelectedValue = Zone_Id.ToString();
                    ddlZone_SelectedIndexChanged(ddlZone, new EventArgs());
                }
                catch
                {
                    Zone_Id = 0;
                }
                try
                {
                    Circle_Id = Convert.ToInt32(Request.QueryString["Circle_Id"].ToString());
                    ddlCircle.SelectedValue = Zone_Id.ToString();
                    ddlCircle_SelectedIndexChanged(ddlCircle, new EventArgs());
                }
                catch
                {
                    Circle_Id = 0;
                }
                try
                {
                    Division_Id = Convert.ToInt32(Request.QueryString["Division_Id"].ToString());
                    ddlDivision.SelectedValue = Division_Id.ToString();
                }
                catch
                {
                    Division_Id = 0;
                }
            }
            get_Data_Updation_Status();
        }
    }
    private void get_Data_Updation_Status()
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
        DataSet ds = new DataSet();
        ds = (new DataLayer()).get_Data_Updation_Status(Zone_Id, Circle_Id, Division_Id);
        if (AllClasses.CheckDataSet(ds))
        {
            grdPost.DataSource = ds.Tables[0];
            grdPost.DataBind();

            grdPost.FooterRow.Cells[4].Text = ds.Tables[0].Compute("sum(Total_Projects)", "").ToString();
            grdPost.FooterRow.Cells[5].Text = ds.Tables[0].Compute("sum(Total_Login)", "").ToString();
            grdPost.FooterRow.Cells[8].Text = ds.Tables[0].Compute("sum(Release_More_Than_Sanction)", "").ToString();
            grdPost.FooterRow.Cells[9].Text = ds.Tables[0].Compute("sum(Expenditure_More_Than_Sanction)", "").ToString();
            grdPost.FooterRow.Cells[10].Text = ds.Tables[0].Compute("sum(Expenditure_More_Than_Release)", "").ToString();
            grdPost.FooterRow.Cells[11].Text = ds.Tables[0].Compute("sum(FP_More_100)", "").ToString();
            grdPost.FooterRow.Cells[12].Text = ds.Tables[0].Compute("sum(Agreement_Date_Less_Than_Start_Date)", "").ToString();
            grdPost.FooterRow.Cells[13].Text = ds.Tables[0].Compute("sum(Due_Date_Less_Than_Start_Date)", "").ToString();
            grdPost.FooterRow.Cells[14].Text = ds.Tables[0].Compute("sum(Due_Date_Less_Than_Agreement_Date)", "").ToString();
            grdPost.FooterRow.Cells[15].Text = ds.Tables[0].Compute("sum(Diff_Physical_Financial_More)", "").ToString();
            grdPost.FooterRow.Cells[16].Text = ds.Tables[0].Compute("sum(Agreement_Date_Invalid)", "").ToString();
            grdPost.FooterRow.Cells[17].Text = ds.Tables[0].Compute("sum(Start_Date_Invalid)", "").ToString();
            grdPost.FooterRow.Cells[18].Text = ds.Tables[0].Compute("sum(Due_Date_Invalid)", "").ToString();
        }
        else
        {
            grdPost.DataSource = null;
            grdPost.DataBind();
            MessageBox.Show("No Data found!!");
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

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        get_Data_Updation_Status();
    }

    protected void grdPost_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[2].Text = Session["Default_Circle"].ToString();
            e.Row.Cells[3].Text = Session["Default_Division"].ToString();
        }
    }

    protected void lnkTotalProjects_Click(object sender, EventArgs e)
    {
        LinkButton lnkAllData = sender as LinkButton;
        GridViewRow gr = lnkAllData.Parent.Parent as GridViewRow;
        int Division_Id = 0;

        try
        {
            Division_Id = Convert.ToInt32(gr.Cells[0].Text.Trim());
        }
        catch
        {
            Division_Id = 0;
        }
        Response.Redirect("MasterProjectWork_DataEntry_View.aspx?Zone_Id=0&Circle_Id=0&Division_Id=" + Division_Id.ToString());
    }

    protected void lnkTotalLogin_Click(object sender, EventArgs e)
    {

    }

    protected void lnkfirstLogin_Click(object sender, EventArgs e)
    {

    }

    protected void lnkLastLogin_Click(object sender, EventArgs e)
    {

    }

    protected void lnkReleaseMoreSanction_Click(object sender, EventArgs e)
    {
        LinkButton lnkAllData = sender as LinkButton;
        GridViewRow gr = lnkAllData.Parent.Parent as GridViewRow;
        int Division_Id = 0;

        try
        {
            Division_Id = Convert.ToInt32(gr.Cells[0].Text.Trim());
        }
        catch
        {
            Division_Id = 0;
        }
        Response.Redirect("MasterProjectWork_DataEntry_View.aspx?Zone_Id=0&Circle_Id=0&Division_Id=" + Division_Id.ToString() + "&Mode=1");
    }

    protected void lnkExpenditureMoreSanction_Click(object sender, EventArgs e)
    {
        LinkButton lnkAllData = sender as LinkButton;
        GridViewRow gr = lnkAllData.Parent.Parent as GridViewRow;
        int Division_Id = 0;

        try
        {
            Division_Id = Convert.ToInt32(gr.Cells[0].Text.Trim());
        }
        catch
        {
            Division_Id = 0;
        }
        Response.Redirect("MasterProjectWork_DataEntry_View.aspx?Zone_Id=0&Circle_Id=0&Division_Id=" + Division_Id.ToString() + "&Mode=2");
    }

    protected void lnkExpenditureMoreRelease_Click(object sender, EventArgs e)
    {
        LinkButton lnkAllData = sender as LinkButton;
        GridViewRow gr = lnkAllData.Parent.Parent as GridViewRow;
        int Division_Id = 0;

        try
        {
            Division_Id = Convert.ToInt32(gr.Cells[0].Text.Trim());
        }
        catch
        {
            Division_Id = 0;
        }
        Response.Redirect("MasterProjectWork_DataEntry_View.aspx?Zone_Id=0&Circle_Id=0&Division_Id=" + Division_Id.ToString() + "&Mode=3");
    }

    protected void lnkAgreementLessStart_Click(object sender, EventArgs e)
    {
        LinkButton lnkAllData = sender as LinkButton;
        GridViewRow gr = lnkAllData.Parent.Parent as GridViewRow;
        int Division_Id = 0;

        try
        {
            Division_Id = Convert.ToInt32(gr.Cells[0].Text.Trim());
        }
        catch
        {
            Division_Id = 0;
        }
        Response.Redirect("MasterProjectWork_DataEntry_View.aspx?Zone_Id=0&Circle_Id=0&Division_Id=" + Division_Id.ToString() + "&Mode=5");
    }

    protected void lnkDueLessStart_Click(object sender, EventArgs e)
    {
        LinkButton lnkAllData = sender as LinkButton;
        GridViewRow gr = lnkAllData.Parent.Parent as GridViewRow;
        int Division_Id = 0;

        try
        {
            Division_Id = Convert.ToInt32(gr.Cells[0].Text.Trim());
        }
        catch
        {
            Division_Id = 0;
        }
        Response.Redirect("MasterProjectWork_DataEntry_View.aspx?Zone_Id=0&Circle_Id=0&Division_Id=" + Division_Id.ToString() + "&Mode=6");
    }

    protected void lnkDueLessAgreement_Click(object sender, EventArgs e)
    {
        LinkButton lnkAllData = sender as LinkButton;
        GridViewRow gr = lnkAllData.Parent.Parent as GridViewRow;
        int Division_Id = 0;

        try
        {
            Division_Id = Convert.ToInt32(gr.Cells[0].Text.Trim());
        }
        catch
        {
            Division_Id = 0;
        }
        Response.Redirect("MasterProjectWork_DataEntry_View.aspx?Zone_Id=0&Circle_Id=0&Division_Id=" + Division_Id.ToString() + "&Mode=7");
    }

    protected void lnkFPMore_Click(object sender, EventArgs e)
    {
        LinkButton lnkAllData = sender as LinkButton;
        GridViewRow gr = lnkAllData.Parent.Parent as GridViewRow;
        int Division_Id = 0;

        try
        {
            Division_Id = Convert.ToInt32(gr.Cells[0].Text.Trim());
        }
        catch
        {
            Division_Id = 0;
        }
        Response.Redirect("MasterProjectWork_DataEntry_View.aspx?Zone_Id=0&Circle_Id=0&Division_Id=" + Division_Id.ToString() + "&Mode=4");
    }

    protected void lnkDiffPhysicalFinancialMore_Click(object sender, EventArgs e)
    {
        LinkButton lnkAllData = sender as LinkButton;
        GridViewRow gr = lnkAllData.Parent.Parent as GridViewRow;
        int Division_Id = 0;

        try
        {
            Division_Id = Convert.ToInt32(gr.Cells[0].Text.Trim());
        }
        catch
        {
            Division_Id = 0;
        }
        Response.Redirect("MasterProjectWork_DataEntry_View.aspx?Zone_Id=0&Circle_Id=0&Division_Id=" + Division_Id.ToString() + "&Mode=8");
    }

    protected void lnkAgreementDateinvalid_Click(object sender, EventArgs e)
    {
        LinkButton lnkAllData = sender as LinkButton;
        GridViewRow gr = lnkAllData.Parent.Parent as GridViewRow;
        int Division_Id = 0;

        try
        {
            Division_Id = Convert.ToInt32(gr.Cells[0].Text.Trim());
        }
        catch
        {
            Division_Id = 0;
        }
        Response.Redirect("MasterProjectWork_DataEntry_View.aspx?Zone_Id=0&Circle_Id=0&Division_Id=" + Division_Id.ToString() + "&Mode=9");
    }

    protected void lnkStartDateinvalid_Click(object sender, EventArgs e)
    {
        LinkButton lnkAllData = sender as LinkButton;
        GridViewRow gr = lnkAllData.Parent.Parent as GridViewRow;
        int Division_Id = 0;

        try
        {
            Division_Id = Convert.ToInt32(gr.Cells[0].Text.Trim());
        }
        catch
        {
            Division_Id = 0;
        }
        Response.Redirect("MasterProjectWork_DataEntry_View.aspx?Zone_Id=0&Circle_Id=0&Division_Id=" + Division_Id.ToString() + "&Mode=10");
    }

    protected void lnkAgreementEndDateinvalid_Click(object sender, EventArgs e)
    {
        LinkButton lnkAllData = sender as LinkButton;
        GridViewRow gr = lnkAllData.Parent.Parent as GridViewRow;
        int Division_Id = 0;

        try
        {
            Division_Id = Convert.ToInt32(gr.Cells[0].Text.Trim());
        }
        catch
        {
            Division_Id = 0;
        }
        Response.Redirect("MasterProjectWork_DataEntry_View.aspx?Zone_Id=0&Circle_Id=0&Division_Id=" + Division_Id.ToString() + "&Mode=11");
    }
}