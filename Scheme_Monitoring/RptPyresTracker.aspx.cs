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
            MainTracker.DataSource = dt;
            MainTracker.DataBind();
            divData.Visible = true;
        }
        else
        {
            divData.Visible = false;
            MainTracker.DataSource = null;
            MainTracker.DataBind();
            MessageBox.Show("No Records Found");
        }
    }

    protected void btnEdit_Click(object sender, ImageClickEventArgs e)
    {
        GridViewRow gr = (sender as ImageButton).Parent.Parent as GridViewRow;
        int ProjectWork_Id = Convert.ToInt32(gr.Cells[0].Text.Trim());
        int Project_Id = Convert.ToInt32(gr.Cells[1].Text.Trim());
        int District_Id = Convert.ToInt32(gr.Cells[2].Text.Trim());
        string Client = ConfigurationManager.AppSettings.Get("Client");
        if (Client == "CNDS")
        {
            Response.Redirect("MasterProjectWork_DataEntry.aspx?ProjectWork_Id=" + ProjectWork_Id.ToString() + "&Scheme_Id=" + Project_Id.ToString());
        }
        else
        {
            string Mode = "";
            if (Request.QueryString.Count > 0)
            {
                try
                {
                    Mode = Request.QueryString[0].ToString();
                }
                catch
                {
                    Mode = "";
                }
                if (Mode == "GO")
                {
                    Response.Redirect("MasterProjectWorkMIS_2_CNDS.aspx?ProjectWork_Id=" + ProjectWork_Id.ToString() + "&District_Id=" + District_Id + "&Id=" + Project_Id.ToString());
                }
                else if (Mode == "UC")
                {
                    Response.Redirect("MasterProjectWorkMIS_6.aspx?ProjectWork_Id=" + ProjectWork_Id.ToString() + "&Id=" + Project_Id.ToString());
                }
                else if (Mode == "Issue")
                {
                    Response.Redirect("MasterProjectWorkMIS_8.aspx?ProjectWork_Id=" + ProjectWork_Id.ToString() + "&Id=" + Project_Id.ToString());
                }
                else if (Mode == "Comp")
                {
                    Response.Redirect("MasterProjectWorkMIS_4.aspx?ProjectWork_Id=" + ProjectWork_Id.ToString() + "&Id=" + Project_Id.ToString());
                }
                else if (Mode == "SO")
                {
                    Response.Redirect("MasterProjectWork_DataEntrySection.aspx?ProjectWork_Id=" + ProjectWork_Id.ToString() + "&Scheme_Id=" + Project_Id.ToString());
                }
                else if (Mode == "PMU")
                {
                    Response.Redirect("MasterProjectWork_DataEntry2.aspx?ProjectWork_Id=" + ProjectWork_Id.ToString() + "&Scheme_Id=" + Project_Id.ToString());
                }
                else if (Mode == "G")
                {
                    Response.Redirect("ProjectWorkGalleryView.aspx?ProjectWork_Id=" + ProjectWork_Id.ToString() + "&Mode=P&App=false");
                }
                else
                {
                    Response.Redirect("MasterProjectWork_DataEntry2.aspx?ProjectWork_Id=" + ProjectWork_Id.ToString() + "&Scheme_Id=" + Project_Id.ToString());
                }
            }
            else
            {
                Response.Redirect("MasterProjectWork_DataEntry2.aspx?ProjectWork_Id=" + ProjectWork_Id.ToString() + "&Scheme_Id=" + Project_Id.ToString());
            }

        }
    }

    protected void btnCreateNew_Click(object sender, EventArgs e)
    {
        string Mode = "";
        if (Request.QueryString.Count > 0)
        {
            try
            {
                Mode = Request.QueryString[0].ToString();
            }
            catch
            {
                Mode = "";
            }
        }
        string Client = ConfigurationManager.AppSettings.Get("Client");
        if (Client == "CNDS")
        {
            Response.Redirect("PyersTracker.aspx");
        }
        else
        {
            if (Mode == "SO")
            {
                Response.Redirect("PyersTracker.aspx");
            }
            else
            {
                Response.Redirect("PyersTracker.aspx");
            }
        }
    }
}