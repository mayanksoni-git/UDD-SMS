using System;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ChangePasswordMaster : System.Web.UI.Page
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

            get_tbl_ProjectSearch();
            get_tbl_Designation();
            get_tbl_Zone();
        }
    }
    private void get_tbl_Designation()
    {
        DataSet ds = new DataSet();
        ds = (new DataLayer()).get_tbl_Designation();
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            lbDesignationS.DataTextField = "Designation_DesignationName";
            lbDesignationS.DataValueField = "Designation_Id";
            lbDesignationS.DataSource = ds.Tables[0];
            lbDesignationS.DataBind();
        }
        else
        {
            lbDesignationS.Items.Clear();
        }
    }

    private void get_tbl_ProjectSearch()
    {
        DataSet ds = new DataSet();
        if (Session["UserType"].ToString() == "1")
        {
            ds = (new DataLayer()).get_tbl_Project(0);
        }
        else
        {
            ds = (new DataLayer()).get_tbl_Project(Convert.ToInt32(Session["Person_Id"].ToString()));
        }
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            AllClasses.FillDropDown(ds.Tables[0], ddlSearchScheme, "Project_Name", "Project_Id");
            try
            {
                ddlSearchScheme.SelectedValue = Session["Default_Scheme"].ToString();
            }
            catch
            {
                ddlSearchScheme.SelectedIndex = 1;
            }
        }
        else
        {
            ddlSearchScheme.Items.Clear();
        }
    }
    private void get_Employee(int Zone_Id, int Circle_Id, int Division_Id)
    {
        string UserTypeId = "1, 2, 4, 6, 7, 8, 9, 11, 3, 13";
        int District_Id = 0;
        int Project_Id = 0;

        try
        {
            Project_Id = Convert.ToInt32(ddlSearchScheme.SelectedValue);
        }
        catch
        {
            Project_Id = 0;
        }
        string Designation_Ids = "";
        foreach (ListItem listItem in lbDesignationS.Items)
        {
            if (listItem.Selected)
            {
                Designation_Ids += listItem.Value + ", ";
            }
        }
        if (Designation_Ids != "")
            Designation_Ids = Designation_Ids.Trim().Substring(0, Designation_Ids.Trim().Length - 1);
        DataSet ds = new DataSet();
        ds = (new DataLayer()).get_Employee(UserTypeId, 0, 0, District_Id, Zone_Id, Circle_Id, Division_Id, 0, Designation_Ids, txtSearchMobile.Text.Trim(), 0, 0, Project_Id);
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
            AllClasses.FillDropDown_WithOutSelect(ds.Tables[0], ddlZoneS, "Zone_Name", "Zone_Id");
        }
        else
        {
            ddlZoneS.Items.Clear();
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

    private void get_tbl_Circle(int Zone_Id, DropDownList ddlCircle)
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

    private void get_tbl_Division(int Circle_Id, DropDownList ddlDivision)
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

    protected void ddlZoneS_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlZoneS.SelectedValue == "0")
        {
            ddlCircleS.Items.Clear();
            MessageBox.Show("Please Select " + Session["Default_Zone"].ToString() + "");
            return;
        }
        else
        {
            get_tbl_Circle(Convert.ToInt32(ddlZoneS.SelectedValue), ddlCircleS);
            int Zone_Id = 0;
            int Circle_Id = 0;
            int Division_Id = 0;
            try
            {
                Zone_Id = Convert.ToInt32(ddlZoneS.SelectedValue);
            }
            catch
            {
                Zone_Id = 0;
            }
            try
            {
                Circle_Id = Convert.ToInt32(ddlCircleS.SelectedValue);
            }
            catch
            {
                Circle_Id = 0;
            }
            try
            {
                Division_Id = Convert.ToInt32(ddlDivisionS.SelectedValue);
            }
            catch
            {
                Division_Id = 0;
            }
            get_Employee(Zone_Id, Circle_Id, Division_Id);
        }
    }

    protected void ddlCircleS_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlCircleS.SelectedValue == "0")
        {
            ddlDivisionS.Items.Clear();
            MessageBox.Show("Please Select Division");
            return;
        }
        else
        {
            get_tbl_Division(Convert.ToInt32(ddlCircleS.SelectedValue), ddlDivisionS);
            int Zone_Id = 0;
            int Circle_Id = 0;
            int Division_Id = 0;
            try
            {
                Zone_Id = Convert.ToInt32(ddlZoneS.SelectedValue);
            }
            catch
            {
                Zone_Id = 0;
            }
            try
            {
                Circle_Id = Convert.ToInt32(ddlCircleS.SelectedValue);
            }
            catch
            {
                Circle_Id = 0;
            }
            try
            {
                Division_Id = Convert.ToInt32(ddlDivisionS.SelectedValue);
            }
            catch
            {
                Division_Id = 0;
            }
            get_Employee(Zone_Id, Circle_Id, Division_Id);
        }
    }

    protected void ddlDivisionS_SelectedIndexChanged(object sender, EventArgs e)
    {
        int Zone_Id = 0;
        int Circle_Id = 0;
        int Division_Id = 0;
        try
        {
            Zone_Id = Convert.ToInt32(ddlZoneS.SelectedValue);
        }
        catch
        {
            Zone_Id = 0;
        }
        try
        {
            Circle_Id = Convert.ToInt32(ddlCircleS.SelectedValue);
        }
        catch
        {
            Circle_Id = 0;
        }
        try
        {
            Division_Id = Convert.ToInt32(ddlDivisionS.SelectedValue);
        }
        catch
        {
            Division_Id = 0;
        }
        get_Employee(Zone_Id, Circle_Id, Division_Id);
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        if (ddlSearchScheme.SelectedValue == "0")
        {
            MessageBox.Show("Please Select A Scheme");
            return;
        }
        int Zone_Id = 0;
        int Circle_Id = 0;
        int Division_Id = 0;
        try
        {
            Zone_Id = Convert.ToInt32(ddlZoneS.SelectedValue);
        }
        catch
        {
            Zone_Id = 0;
        }
        try
        {
            Circle_Id = Convert.ToInt32(ddlCircleS.SelectedValue);
        }
        catch
        {
            Circle_Id = 0;
        }
        try
        {
            Division_Id = Convert.ToInt32(ddlDivisionS.SelectedValue);
        }
        catch
        {
            Division_Id = 0;
        }
        get_Employee(Zone_Id, Circle_Id, Division_Id);
    }

    protected void lnkUpdate_Click(object sender, EventArgs e)
    {
        ImageButton lnkUpdate = sender as ImageButton;
        for (int i = 0; i < grdPost.Rows.Count; i++)
        {
            grdPost.Rows[i].BackColor = Color.Transparent;
        }
        (lnkUpdate.Parent.Parent as GridViewRow).BackColor = Color.LightGreen;
        if (new DataLayer().Change_Password(Convert.ToInt32((lnkUpdate.Parent.Parent as GridViewRow).Cells[0].Text.Trim())))
        {
            MessageBox.Show("Password Reset Successfully!!");
            return;
        }
        else
        {
            MessageBox.Show("Error!!");
            return;
        }
    }

    protected void grdPost_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[27].Text = Session["Default_Zone"].ToString();
            e.Row.Cells[28].Text = Session["Default_Circle"].ToString();
            e.Row.Cells[29].Text = Session["Default_Division"].ToString();
        }
    }
}