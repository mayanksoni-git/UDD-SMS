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

public partial class PhysicalBaseLineData : System.Web.UI.Page
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
            get_tbl_Unit();
            get_tbl_Project();
            get_tbl_PhysicalBaseLineData();
        }
    }

    private void get_tbl_PhysicalBaseLineData()
    {
        DataSet ds = new DataSet();
        if (Session["UserType"].ToString() == "1")
        {
            ds = (new DataLayer()).get_tbl_PhysicalBaseLineData(0, 0);
        }
        else
        {
            ds = (new DataLayer()).get_tbl_PhysicalBaseLineData(0, Convert.ToInt32(Session["Person_Id"].ToString()));
        }
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
    private void get_tbl_Project()
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

    private void get_tbl_Unit()
    {
        DataSet ds = new DataSet();
        ds = (new DataLayer()).get_tbl_Unit();
        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            AllClasses.FillDropDown(ds.Tables[0], ddlUnit, "Unit_Name", "Unit_Id");
        }
        else
        {
            ddlUnit.Items.Clear();
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        string Msg = "";
        tbl_PhysicalBaseLineData obj_tbl_PhysicalBaseLineData = new tbl_PhysicalBaseLineData();
        if (hf_PhysicalBaseLineData_Id.Value == "0" || hf_PhysicalBaseLineData_Id.Value == "")
        {
            obj_tbl_PhysicalBaseLineData.PhysicalBaseLineData_Id = 0;
        }
        else
        {
            obj_tbl_PhysicalBaseLineData.PhysicalBaseLineData_Id = Convert.ToInt32(hf_PhysicalBaseLineData_Id.Value);
        }
        obj_tbl_PhysicalBaseLineData.PhysicalBaseLineData_AddedBy = Convert.ToInt32(Session["Person_Id"].ToString());
        if (txtComponent.Text.Trim() == string.Empty)
        {
            Msg = "Give Base Line Data";
            txtComponent.Focus();
            return;
        }
        if (ddlUnit.SelectedValue ==null || ddlUnit.SelectedValue=="0")
        {
            Msg = "Select Unit";
            ddlUnit.Focus();
            return;
        }
        if (ddlSearchScheme.SelectedValue == null || ddlSearchScheme.SelectedValue == "0")
        {
            Msg = "Select Scheme";
            ddlSearchScheme.Focus();
            return;
        }
        obj_tbl_PhysicalBaseLineData.PhysicalBaseLineData_Component = txtComponent.Text.Trim();
        obj_tbl_PhysicalBaseLineData.PhysicalBaseLineData_Status = 1;
        obj_tbl_PhysicalBaseLineData.PhysicalBaseLineData_Unit_Id = Convert.ToInt32(ddlUnit.SelectedValue);
        obj_tbl_PhysicalBaseLineData.PhysicalBaseLineData_ProjectId = Convert.ToInt32(ddlSearchScheme.SelectedValue);

        if (new DataLayer().Insert_tbl_PhysicalBaseLineData(obj_tbl_PhysicalBaseLineData, obj_tbl_PhysicalBaseLineData.PhysicalBaseLineData_Id, ref Msg))
        {
            MessageBox.Show("Component Created Successfully ! ");
            reset();
            get_tbl_PhysicalBaseLineData();
            return;
        }
        else
        {
            if (Msg == "A")
            {
                MessageBox.Show("This Component Already Exist. Give another! ");
            }
            else
            {
                MessageBox.Show("Error ! ");
            }
            return;
        }
    }

    private void reset()
    {
        txtComponent.Text = "";
        hf_PhysicalBaseLineData_Id.Value = "0";
        ddlUnit.SelectedValue = "0";
        ddlSearchScheme.SelectedValue = "0";
        get_tbl_PhysicalBaseLineData();
        mp1.Hide();
    }

    protected void btnReset_Click(object sender, EventArgs e)
    {
        reset();
    }

    protected void btnEdit_Click(object sender, ImageClickEventArgs e)
    {
        int PhysicalBaseLineData_Id = Convert.ToInt32(((sender as ImageButton).Parent.Parent as GridViewRow).Cells[0].Text.Trim());
        hf_PhysicalBaseLineData_Id.Value = PhysicalBaseLineData_Id.ToString();
        txtComponent.Text = ((sender as ImageButton).Parent.Parent as GridViewRow).Cells[4].Text.Trim();
        try
        {
            ddlUnit.SelectedValue = ((sender as ImageButton).Parent.Parent as GridViewRow).Cells[1].Text.Trim();
        }
        catch
        {
        }
        try
        {
            ddlSearchScheme.SelectedValue = ((sender as ImageButton).Parent.Parent as GridViewRow).Cells[2].Text.Trim();
        }
        catch
        {
        }
        btnDelete.Visible = true;
        mp1.Show();
    }
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        int Person_Id = Convert.ToInt32(Session["Person_Id"].ToString());
        int PhysicalBaseLineData_Id = Convert.ToInt32(hf_PhysicalBaseLineData_Id.Value);
        if (new DataLayer().Delete_PhysicalBaseLineData(PhysicalBaseLineData_Id, Person_Id))
        {
            MessageBox.Show("Deleted Successfully!!");
            reset();
            return;
        }
        else
        {
            MessageBox.Show("Error In Deletion!!");
            reset();
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
    protected void btnAddNew_Click(object sender, EventArgs e)
    {
        txtComponent.Text = "";
        hf_PhysicalBaseLineData_Id.Value = "0";
        ddlUnit.SelectedValue = "0";
        btnDelete.Visible = false;
        mp1.Show();
    }
}