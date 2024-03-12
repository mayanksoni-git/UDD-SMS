
using System;
using System.Collections;
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
public partial class MasterUpdatePensioner : System.Web.UI.Page
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
            get_tbl_zone();
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
    private void get_tbl_PensionMaster(int Zone_Id, int Circle_Id, int Division_Id,string Employee_Code, int PensionMaster_Id)
    {
        DataSet ds = new DataSet();
        ds = (new DataLayer()).get_tbl_PensionMaster( Zone_Id, Circle_Id, Division_Id, Employee_Code, PensionMaster_Id);
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
    private void get_tbl_zone()
    {
        DataSet ds = new DataSet();
        ds = (new DataLayer()).get_tbl_Zone();
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            AllClasses.FillDropDown(ds.Tables[0], ddlZone, "Zone_Name", "Zone_Id");

            if (Request.QueryString.Count > 0)
            {
                int Zone_Id = 0;
                try
                {
                    Zone_Id = Convert.ToInt32(Request.QueryString["Zone_Id"].ToString());
                }
                catch
                {
                    Zone_Id = 0;
                }
                if (Zone_Id > 0)
                {
                    ddlZone.SelectedValue = Zone_Id.ToString();
                    ddlZone_SelectedIndexChanged(ddlZone, new EventArgs());
                }
            }
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

            if (Request.QueryString.Count > 0)
            {
                int Circle_Id = 0;
                try
                {
                    Circle_Id = Convert.ToInt32(Request.QueryString["Circle_Id"].ToString());
                }
                catch
                {
                    Circle_Id = 0;
                }
                if (Circle_Id > 0)
                {
                    ddlCircle.SelectedValue = Circle_Id.ToString();
                    ddlCircle_SelectedIndexChanged(ddlCircle, new EventArgs());
                }
            }
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

            if (Request.QueryString.Count > 0)
            {
                int Division_Id = 0;
                try
                {
                    Division_Id = Convert.ToInt32(Request.QueryString["Division_Id"].ToString());
                }
                catch
                {
                    Division_Id = 0;
                }
                if (Division_Id > 0)
                {
                    ddlDivision.SelectedValue = Division_Id.ToString();
                }
            }
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
            ddlDivision.Items.Clear();
        }
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        int Zone_Id = 0;
        int Circle_Id = 0;
        int Division_Id = 0;
        string Employee_Code = "";
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
            Employee_Code =txtEmployeeCode.Text.Trim();
        }
        catch
        {
            Employee_Code = "";
        }
        get_tbl_PensionMaster(Zone_Id, Circle_Id, Division_Id, Employee_Code, 0);
    }
    protected void btnEdit_Click(object sender, ImageClickEventArgs e)
    {
        GridViewRow gr = (sender as ImageButton).Parent.Parent as GridViewRow;
        int PensionMaster_Id = Convert.ToInt32(gr.Cells[0].Text.Trim());
        Response.Redirect("MasterCreatePensioner.aspx?PensionMaster_Id="+PensionMaster_Id.ToString());
    }

    protected void btnDelete_Click(object sender, ImageClickEventArgs e)
    {
        GridViewRow gr = (sender as ImageButton).Parent.Parent as GridViewRow;
        int Person_Id = Convert.ToInt32(Session["Person_Id"].ToString());
        int PensionMaster_Id = Convert.ToInt32(gr.Cells[0].Text.Trim());
        if (new DataLayer().Delete_PensionMaster(PensionMaster_Id, Person_Id))
        {
            int Zone_Id = 0;
            int Circle_Id = 0;
            int Division_Id = 0;
            string Employee_Code = "";
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
                Employee_Code =txtEmployeeCode.Text.Trim();
            }
            catch
            {
                Employee_Code = "";
            }
            MessageBox.Show("Deleted Successfully !!");
            get_tbl_PensionMaster(Zone_Id, Circle_Id, Division_Id, Employee_Code, 0);
            return;
        }
        else
        {
            MessageBox.Show("Error In Deletion !!");
            return;
        }
    }
}