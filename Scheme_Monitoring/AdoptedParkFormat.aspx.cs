using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class AdoptedParkFormat : System.Web.UI.Page
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
       
            get_tbl_Mandal();
            get_tbl_Month();
            lblCircleH.Text = Session["Default_Circle"].ToString();
            lblDivisionH.Text = Session["Default_Division"].ToString();
            
            if (Session["UserType"].ToString() == "6")
            {
                try
                {
                 
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
            if (Session["UserType"].ToString() == "7" )
            {
                try
                {
                    
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

            if (Request.QueryString["Division"] != null)
            {
                //int Zone = Convert.ToInt32(Request.QueryString["Zone"].ToString());
                int Circle = Convert.ToInt32(Request.QueryString["Circle"].ToString());
                int Division = Convert.ToInt32(Request.QueryString["Division"].ToString());
                int Year = Convert.ToInt32(Request.QueryString["Year"].ToString());
                int Month = Convert.ToInt32(Request.QueryString["Month"].ToString());
                SearchCrematoriumDetail obj_Search = new SearchCrematoriumDetail();
                //obj_Search.Zone = Zone;
                obj_Search.Circle = Circle;
                obj_Search.Division = Division;
                obj_Search.Year = Year;
                obj_Search.Month = Month;
            }
        }
    }

  
    private void get_tbl_Mandal()
    {
        DataSet ds = (new DataLayer()).get_tbl_Mandal();
        AllClasses.FillDropDown(ds.Tables[0], ddlMandal, "DivName", "DivisionID");
        if (ddlMandal.SelectedItem.Value != "0")
        {
            get_tbl_Circle(Convert.ToInt32(ddlMandal.SelectedValue));
        }
    }

    private void get_tbl_Circle(int DivisionID)
    {
        DataSet ds = new DataSet();
        ds = (new DataLayer()).get_tbl_CircleByDivisionId(DivisionID);
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
            ddlMandal.Items.Clear();
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
   protected void ddlMandal_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlMandal.SelectedValue == "0")
        {
            ddlCircle.Items.Clear();
            ddlDivision.Items.Clear();
        }
        else
        {
            get_tbl_Circle(Convert.ToInt32(ddlMandal.SelectedValue));
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

  
    protected void btnSave_Click(object sender, EventArgs e)
    {

    }

    protected void btnSave_Click1(object sender, EventArgs e)
    {

    }
}